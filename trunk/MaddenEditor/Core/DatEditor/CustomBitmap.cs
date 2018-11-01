/*
    Madden Resource Editor
    Copyright (C) 2014  Stingray68

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
  
    For more information also see  <https://www.gnu.org/licenses/gpl-faq.html>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MaddenEditor.Core;

namespace MaddenEditor.Core.DatEditor
{
    public class AlphaColor
    {
        #region Private Members
        private Color _col;
        private int _color_integer;
        private int _pal_number;
        private int _alpha_value;

        private List<AlphaColor> _child_colors;
        #endregion

        #region Public Members

        public Color col
        {
            get { return _col; }
            set { _col = value; }
        }
        public int color_integer
        {
            get { return _color_integer; }
            set { _color_integer = value; }
        }
        public int pal_number
        {
            get { return _pal_number; }
            set { _pal_number = value; }
        }
        public int alpha_value
        {
            get { return _alpha_value; }
            set { _alpha_value = value; }
        }
        public List<AlphaColor> child_colors
        {
            get { return _child_colors; }
            set { _child_colors = value; }
        }

        #endregion

        public AlphaColor()
        {
            col = Color.Black;
            color_integer = (int)(col.R * 65536) + (int)(col.G * 256) + (int)col.B;
            pal_number = 0;
            alpha_value = 255;
            child_colors = new List<AlphaColor>();
        }
        public AlphaColor(Color color, int palnum)
        {
            col = color;
            color_integer = (int)(color.R * 65536) + (int)(color.G * 256) + (int)color.B;
            pal_number = palnum;
            alpha_value = color.A;
            child_colors = new List<AlphaColor>();
        }

    }

    public class ColorEntry
    {
        #region Private members
        private Color _color;
        private int _occurences;
        private int _pal_index;
        private int _alpha_value;

        #endregion

        #region Public members
        public Color color
        {
            get { return _color; }
            set { _color = value; }
        }
        public int occurences
        {
            get { return _occurences; }
            set { _occurences = value; }
        }
        public int pal_index
        {
            get { return _pal_index; }
            set { _pal_index = value; }
        }
        public int alpha_value
        {
            get { return _alpha_value; }
            set { _alpha_value = value; }
        }

        #endregion

        public ColorEntry()
        {
            color = Color.Black;
            occurences = 0;
            pal_index = -1;
            alpha_value = 255;
        }
        public ColorEntry(Color c)
        {
            color = c;
            occurences = 1;
            pal_index = -1;
            alpha_value = 255;
        }
        public ColorEntry(Color newcolor, int index)
        {
            color = newcolor;
            occurences = 1;
            pal_index = index;
            alpha_value = 255;
        }
        public ColorEntry(Color newcolor, int alpha, bool transparent)
        {
            color = newcolor;
            occurences = 1;
            pal_index = -1;
            if (transparent)
                alpha_value = alpha;
            else alpha_value = 255;
        }
    }
        
    public class ColorCombos
    {
        #region Private Members
        private ColorEntry _color1;
        private ColorEntry _color2;
        private List<ColorEntry> _blends;
        private int _used;

        #endregion

        #region Public Members
        public ColorEntry color1
        {
            get { return _color1; }
            set { _color1 = value; }
        }
        public ColorEntry color2
        {
            get { return _color2; }
            set { _color2 = value; }
        }
        public List<ColorEntry> blends
        {
            get { return _blends; }
            set { _blends = value; }
        }
        public int used
        {
            get { return _used; }
            set { _used = value; }
        }

        #endregion

        public ColorCombos()
        {
            color1 = new ColorEntry();
            color2 = new ColorEntry();
            blends = new List<ColorEntry>();
            used = 0;
        }
        public ColorCombos(Color c1, Color c2)
        {
            color1 = new ColorEntry(c1);
            color2 = new ColorEntry(c2);
            blends = new List<ColorEntry>();
            used = 1;
        }
        public Color BlendAlpha(Color backcolor, Color color, int alpha)
        {
            double fraction = (double)alpha / 255;

            int red = color.R;
            int green = color.G;
            int blue = color.B;

            double tred = (double)((double)color.R * ((double)alpha / 255)) + (double)backcolor.R * (1 - ((double)alpha / 255));
            double tgreen = ((double)color.G * ((double)alpha / 255)) + ((double)backcolor.G * (1 - ((double)alpha / 255)));
            double tblue = ((double)color.B * ((double)alpha / 255)) + ((double)backcolor.B * (1 - ((double)alpha / 255)));

            int blend_red = (int)Math.Round(tred, MidpointRounding.AwayFromZero);
            if (blend_red / fraction > red)
                blend_red = (int)Math.Truncate(tred);

            int blend_green = (int)Math.Round(tgreen, MidpointRounding.AwayFromZero);
            if (blend_green / fraction > green)
                blend_green = (int)Math.Truncate(tgreen);

            int blend_blue = (int)Math.Round(tblue, MidpointRounding.AwayFromZero);
            if (blend_blue / fraction > blue)
                blend_blue = (int)Math.Truncate(tblue);
            int blend_alpha = 255;

            return Color.FromArgb(blend_alpha, blend_red, blend_green, blend_blue);
        }

        public void AddBlend(ColorEntry newce)
        {
            bool exists = false;
            foreach (ColorEntry ce in this.blends)
            {
                if (ce.color == newce.color)
                {
                    exists = true;
                    ce.occurences++;
                }
            }
            if (!exists)
                this.blends.Add(newce);
        }

        public void CreateBlends(bool alpha, int shades)
        {
            this.blends = new List<ColorEntry>();

            if (shades == -1)
                shades = 255;
            int red = this.color1.color.R - this.color2.color.R;
            int green = this.color1.color.G - this.color2.color.G;
            int blue = this.color1.color.B - this.color2.color.B;

            this.AddBlend(new ColorEntry(this.color1.color));

            if (!alpha)
            {
                for (int t = shades - 1; t > 0; t--)
                {
                    int temp_a = 255;
                    int temp_r = this.color1.color.R - (red * t / shades);
                    int temp_g = this.color1.color.G - (green * t / shades);
                    int temp_b = this.color1.color.B - (blue * t / shades);
                    Color newcolor = Color.FromArgb(temp_a, temp_r, temp_g, temp_b);

                    this.AddBlend(new ColorEntry(newcolor));
                }
            }

            if (alpha)
            {
                int div = 256 / shades;
                for (int a = 0; a < 256; a += div)
                {
                    Color alphacolor = BlendAlpha(color1.color, color2.color, a);
                    ColorEntry newce = new ColorEntry(alphacolor, a, true);
                    this.AddBlend(newce);
                }
            }

            this.AddBlend(new ColorEntry(this.color2.color));
        }

    }
       
    public class CustomBitmap
    {
        #region Private Members

        private Bitmap _original_bmp;
        private DDS _original_dds;
        private int _original_width;
        private int _original_height;
        private int _original_depth;

        private Bitmap _fixed_bmp;
        private ColorPalette _fixed_pal;
        private List<byte> _pixel_list;
        private byte[] _pixel_array;

        private DDS _fixed_dds;

        private Dictionary<Color, int> _usedcolors;             

        #endregion

        #region Public Members
        
        public Bitmap original_bmp
        {
            get { return _original_bmp; }
            set { _original_bmp = value; }
        }
        public DDS original_dds
        {
            get { return _original_dds; }
            set { _original_dds = value; }
        }
        public int original_width
        {
            get { return _original_width; }
            set { _original_width = value; }
        }
        public int original_height
        {
            get { return _original_height; }
            set { _original_height = value; }
        }
        public int original_depth
        {
            get { return _original_depth; }
            set { _original_depth = value; }
        }

        public Bitmap fixed_bmp
        {
            get { return _fixed_bmp; }
            set { _fixed_bmp = value; }
        }
        public ColorPalette fixed_pal
        {
            get { return _fixed_pal; }
            set { _fixed_pal = value; }
        }
        public List<byte> pixel_list
        {
            get { return _pixel_list; }
            set { _pixel_list = value; }
        }
        public byte[] pixel_array
        {
            get { return _pixel_array; }
            set { _pixel_array = value; }
        }

        public DDS fixed_dds
        {
            get { return _fixed_dds; }
            set { _fixed_dds = value; }
        }

        public Dictionary<Color, int> usedcolors
        {
            get { return _usedcolors; }
            set { _usedcolors = value; }
        }
        
        #endregion

        #region Constructors
        
        public CustomBitmap()
        {
            original_bmp = new Bitmap(96, 96, PixelFormat.Format8bppIndexed);
            original_dds = new DDS();
            original_width = original_bmp.Width;
            original_height = original_bmp.Height;
            original_depth = 8;

            pixel_array = new byte[9216];
            pixel_list = new List<byte>();            

            fixed_bmp = new Bitmap(96, 96, PixelFormat.Format8bppIndexed);
            fixed_pal = fixed_bmp.Palette;

            fixed_dds = new DDS();

            usedcolors = new Dictionary<Color, int>();
        }

        public CustomBitmap(string filename, Color background)
        {
            original_bmp = new Bitmap(96, 96, PixelFormat.Format8bppIndexed);
            original_dds = new DDS();
            fixed_dds = new DDS();

            if (filename == "")
                return;
                      
            else
            {                
                if (this.original_dds.Read(filename))
                {
                    fixed_dds = original_dds;
                    fixed_bmp = original_dds.ConvertToBMP(background);
                    return;
                }
                else
                {
                    original_bmp = (Bitmap)Image.FromFile(filename, true);
                }

                original_width = original_bmp.Width;
                original_height = original_bmp.Height;

                if (original_bmp.PixelFormat == PixelFormat.Format4bppIndexed)
                    original_depth = 4;
                else if (original_bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    original_depth = 24;
                else if (original_bmp.PixelFormat == PixelFormat.Format32bppArgb)
                    original_depth = 32;
                else original_depth = 8;

                FixBitmap();
                fixed_dds.ConvertFromBMP(this.fixed_bmp);
            }
            this.original_bmp.Dispose();
        }
        
                
        #endregion

        #region Graphic functions

        public void Nibble()
        {
            List<byte> nibble_list = new List<byte>();
            foreach (byte b in this.pixel_array)
            {
                nibble_list.Add((byte)(b & 0x0F));
                nibble_list.Add((byte)((b & 0xF0) >> 4));
            }

            this.pixel_array = new byte[nibble_list.Count()];
            this.pixel_list = new List<byte>();
            for (int c = 0; c < nibble_list.Count; c++)
            {                
                this.pixel_array[c] = nibble_list[c];
                this.pixel_list.Add(nibble_list[c]);
            }
        }

        public void ReverseNibble()
        {
            this.pixel_list = new List<byte>();
            for (int c = 0; c < this.pixel_array.Count(); c += 2)
                this.pixel_list.Add((byte)((this.pixel_array[c + 1] << 4) | this.pixel_array[c]));          //  Create new list of bytes and copy both 4 bit pixels back into a byte

            this.pixel_array = new byte[this.pixel_list.Count];
            for (int c = 0; c < this.pixel_list.Count; c++)
                this.pixel_array[c] = this.pixel_list[c];
        }
        
        public void FixHiDefBitmap()
        {
            if (this.original_bmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                Bitmap hidef = new Bitmap(this.original_bmp.Width, this.original_bmp.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(hidef);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                g.DrawImage(this.original_bmp, new Point(0, 0));
                g.Dispose();
                this.original_bmp = new Bitmap(hidef);
                hidef.Dispose();
            }
            original_depth = 32;

            this.fixed_bmp = new Bitmap(original_bmp.Width, original_bmp.Height, PixelFormat.Format32bppArgb);             
            this.pixel_array = new byte[this.fixed_bmp.Width * this.fixed_bmp.Height * 4];
            BitmapData bmdata = this.original_bmp.LockBits(new Rectangle(0, 0, this.original_bmp.Width, this.original_bmp.Height), ImageLockMode.ReadOnly, this.original_bmp.PixelFormat);
            IntPtr ptr = bmdata.Scan0;
            Marshal.Copy(ptr, this.pixel_array, 0, this.pixel_array.Count());
            this.original_bmp.UnlockBits(bmdata);
            
            this.pixel_list = new List<byte>();
            for (int p = 0; p < this.pixel_array.Count(); p++)            
                this.pixel_list.Add(this.pixel_array[p]);
            
            bmdata = this.fixed_bmp.LockBits(new Rectangle(0, 0, this.fixed_bmp.Width, this.fixed_bmp.Height), ImageLockMode.WriteOnly, this.fixed_bmp.PixelFormat);
            Marshal.Copy(this.pixel_array, 0, bmdata.Scan0, this.pixel_array.Length);
            this.fixed_bmp.UnlockBits(bmdata);
        }

        public void FixBitmap()
        {   
            //  This function is used to fix the formatting problems that are introduced when using various different
            //  Graphics programs to create an image.

            //  24bit/32bit images.
            if (this.original_bmp.PixelFormat == PixelFormat.Format24bppRgb || this.original_bmp.PixelFormat == PixelFormat.Format32bppArgb || this.original_bmp.PixelFormat == PixelFormat.Format32bppRgb)
            {
                FixHiDefBitmap();
                return;
            }

            //  Assume 8bit 256 color indexed image, Change if it isn't
            int colors = 256;
            int depth = 8;
            if (this.original_bmp.PixelFormat == PixelFormat.Format4bppIndexed)
            {
                colors = 16;
                depth = 4;
            }

            int flags = this.original_bmp.Flags;

            //  Copy the original bitmap and transfer all the pixels into a new array
            Bitmap copy = (Bitmap)this.original_bmp.Clone();
            this.pixel_array = new byte[copy.Width * copy.Height * depth / 8];
            BitmapData copy_bmdata = copy.LockBits(new Rectangle(0, 0, copy.Width, copy.Height), ImageLockMode.ReadOnly, copy.PixelFormat);
            IntPtr ptr = copy_bmdata.Scan0;
            Marshal.Copy(ptr, this.pixel_array, 0, this.pixel_array.Count());
            copy.UnlockBits(copy_bmdata);

            //  Create a palette from the original image and create a color array
            ColorPalette copy_pal = copy.Palette;
            Color[] copy_colors = copy_pal.Entries;
            List<Color> copycolors_list = new List<Color>();
            for (int c = 0; c < colors; c++)
            {
                if (c >= copy_colors.Count())
                    copycolors_list.Add(Color.FromArgb(0, 0, 0, 0));
                else copycolors_list.Add(copy_colors[c]);
            }

            //  Create a new bitmap that is the same formatting as the original.  We will eventually use this as our fixed graphic.
            this.fixed_bmp = new Bitmap(this.original_bmp.Width, this.original_bmp.Height, this.original_bmp.PixelFormat);
            BitmapData fixed_bmdata = this.fixed_bmp.LockBits(new Rectangle(0, 0, this.fixed_bmp.Width, this.fixed_bmp.Height), ImageLockMode.WriteOnly, this.fixed_bmp.PixelFormat);
            Marshal.Copy(this.pixel_array, 0, fixed_bmdata.Scan0, this.pixel_array.Length);
            this.fixed_bmp.UnlockBits(copy_bmdata);

            //  Create a new palette for our fixed bitmap and create a new color array
            ColorPalette fixedpal = this.fixed_bmp.Palette;
            Color[] fixedcolors = fixedpal.Entries;

            //  If this is a 4bit graphic we need to split each 8bit index into 2 4bit indexes.  2 pixels per byte here
            if (depth == 4)
                Nibble();

            //  Create a new list of colors.  Make color 0 white, which will end up being processed to become transparent in game.
            usedcolors = new Dictionary<Color, int>();
            usedcolors.Add(Color.FromArgb(255, 255, 255, 255), 0);

            List<Color> make_transparent = new List<Color>();                       //  Make list of colors we are going to turn to transparent
            make_transparent.Add(Color.FromArgb(255,255,0,255));                    //  Magenta is always added to this list            
            int background = this.pixel_array[0];

            List<int> left_bound = new List<int>();
            List<int> right_bound = new List<int>();            

            //  Get actual boundaries of image
            for (int row = 0; row < original_bmp.Height; row++)
            {
                left_bound.Add(original_bmp.Width + 1);
                right_bound.Add(-1);

                for (int pixel = 0; pixel < original_bmp.Width; pixel++)
                {
                    int current = this.pixel_array[(row * original_bmp.Width) + pixel];
                    if (current != background)                                                  //  current color is not the same as background color
                    {
                        left_bound[row] = pixel;                                                //  This is where the actual left side of the image is starting
                        break;
                    }
                }

                if (left_bound[row] > original_bmp.Width)
                    right_bound[row] = -1;
                else
                {
                    for (int pixel = (original_width - 1); pixel >= 0; pixel--)
                    {
                        int current = this.pixel_array[(row * original_bmp.Width) + pixel];
                        if (current != this.pixel_array[0])                                         //  current color is not the same as background color
                        {
                            right_bound[row] = pixel;                                                //  This is where the actual left side of the image is starting                    
                            break;
                        }
                    }
                }
            }
            
            //  Create new pixel list
            pixel_list = new List<byte>();

            //  Doing several things here...  
            //  Creating a list of colors that are used in the bitmap and rearranging the palette colors
            //  Getting rid of any colors that are going to become transparent and changing them to first color in palette
            for (int row = 0; row < original_bmp.Height; row++)
            {
                for (int pixel = 0; pixel < original_bmp.Width; pixel++)
                {
                    int pos = (row * original_bmp.Width) + pixel;
                    Color convert= copycolors_list[pixel_array[pos]];
                    Color testcolor = Color.FromArgb(255, convert.R, convert.G, convert.B);
                    if (testcolor.R == 255 && testcolor.G == 255 && testcolor.B == 255)                         // If it is supposed to be white, alter it slightly
                        testcolor = Color.FromArgb(255, 254, 254, 254);                                         // having an issue with white becoming transparent
                    //if (testcolor.R == 0 && testcolor.G == 0 && testcolor.B == 0)
                    //    testcolor = Color.FromArgb(255, 1, 1, 1);                                               //  Probably same deal with black

                    if (make_transparent.Contains(testcolor)|| pixel <= left_bound[row] || pixel >= right_bound[row])  //  Any color that is to become transparent
                    {
                        pixel_list.Add(0);                                                                      //  Record pixel reference to color 0 which is the
                        continue;                                                                               //  background color to become transparent, then continue
                    }

                    if (!usedcolors.ContainsKey(testcolor))                                                     //  Check current pixel's color, if it isn't already in our
                        usedcolors.Add(testcolor, usedcolors.Count);                                            //  List, add it and give it an index reference

                    pixel_list.Add((byte)usedcolors[testcolor]);                                                //  Add new pixel value according to new color #
                }
            }

            /*
            for (int c = 0; c < this.pixel_array.Count(); c++)
            {
                Color testcolor = copy_colors[pixel_array[c]];

                if (testcolor == Color.FromArgb(255, 255, 0, 255) || testcolor == Color.FromArgb(255, 0, 0, 0))   //  Any color that is to become transparent
                {
                    pixel_list.Add(0);                                                                      //  Record pixel reference to color 0 (Black) which is the
                    continue;                                                                               //  background color to become transparent, then continue
                }

                if (!usedcolors.ContainsKey(testcolor))                                                     //  Check current pixel's color, if it isn't already in our
                    usedcolors.Add(testcolor, usedcolors.Count);                                            //  List, add it and give it an index reference

                pixel_list.Add((byte)usedcolors[testcolor]);                                                //  Add new pixel value according to new color #
            }
            */



            for (int p = 0; p < this.pixel_array.Count(); p++)                                              //  Copy new pixel list value back into the array
                this.pixel_array[p] = this.pixel_list[p];

            //  if 4bit combine the 2 4bit pixels back into bytes
            if (depth == 4)
                ReverseNibble();

            //  copy fixed pixel array back to the fixed image bitmap
            fixed_bmdata = this.fixed_bmp.LockBits(new Rectangle(0, 0, this.fixed_bmp.Width, this.fixed_bmp.Height), ImageLockMode.WriteOnly, this.fixed_bmp.PixelFormat);
            Marshal.Copy(this.pixel_array, 0, fixed_bmdata.Scan0, this.pixel_array.Length);
            this.fixed_bmp.UnlockBits(fixed_bmdata);

            //  Clear out fixed colors, set all to transparent background
            for (int c = 0; c < colors; c++)
                fixedcolors[c] = Color.FromArgb(255, 255, 255, 255);            

            //  Copy the used colors back into the fixed color array at the proper locations
            foreach (KeyValuePair<Color, int> check in usedcolors)
                fixedcolors[check.Value] = check.Key;           

            //  Set the fixed bitmap palette
            this.fixed_bmp.Palette = fixedpal;
            this.fixed_pal = fixedpal;
        }

        public void FixHalo()
        {            
            Color alpha = Color.FromArgb(pixel_list[3], pixel_list[2], pixel_list[1], pixel_list[0]);

            List<int> left_bound = new List<int>();
            List<int> right_bound = new List<int>();            

            //  Get left side boundary of background
            for (int row = 0; row < original_bmp.Height; row++)
            {
                left_bound.Add(-1);
                right_bound.Add(-1);

                for (int pixel = 0; pixel < original_bmp.Width; pixel++)
                {
                    int a = this.pixel_array[(row * pixel * 4) + 3];
                    int r = this.pixel_array[(row * pixel * 4) + 2];
                    int g = this.pixel_array[(row * pixel * 4) + 1];
                    int b = this.pixel_array[row * pixel * 4];
                    
                    if (alpha != Color.FromArgb(a, r, g, b) || a!=0)                              //  current color is not transparent or background color
                    {
                        left_bound[row] = pixel;                                                  //  This is where the actual left side of the image is starting
                        break;
                    }
                }

                if (left_bound[row] == -1)
                    right_bound[row] = -1;
                else
                {
                    for (int pixel = (original_width - 1); pixel >= 0; pixel--)
                    {
                        int a = this.pixel_array[(row * pixel * 4) + 3];
                        int r = this.pixel_array[(row * pixel * 4) + 2];
                        int g = this.pixel_array[(row * pixel * 4) + 1];
                        int b = this.pixel_array[row * pixel * 4];
                        
                        if (alpha != Color.FromArgb(a, r, g, b) && a!=0)                          //  current color is not transparent or background color
                        {
                            right_bound[row] = pixel;                                               //  This is where the actual right side of the image is starting
                            break;
                        }
                    }
                }
            }

                
            

            int c = left_bound.Count;
              
        
        }

        #endregion

        #region IO
        public string GetLoadFile()
        {
            string loadfile = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Title = "Load Graphics";
            dialog.InitialDirectory = @"%USERPROFILE%\My Documents\";
            dialog.Filter = "Graphics |*.BMP;*.DDS;*.JPG;*.PNG";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
               loadfile = dialog.FileName;

            return loadfile;        
        }

        public string SaveFileName()
        {
            string savefile = "";
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Filter = "Graphics |*.BMP;*.DDS;*.JPG;*.PNG";
            savedialog.FilterIndex = 1;
            savedialog.AddExtension = true;
            savedialog.Title = "Save File";
            if (savedialog.ShowDialog() == DialogResult.OK)
                savefile = savedialog.FileName;
            return savefile;
        }

        public void Save(string filename)
        {
            if (filename == "")
                return;

            if (filename.Contains("JPG") || filename.Contains("jpg"))
                this.fixed_bmp.Save(filename, ImageFormat.Jpeg);

            else if (filename.Contains("PNG") || filename.Contains("png"))
                this.fixed_bmp.Save(filename, ImageFormat.Png);

            else if (filename.Contains("DDS") || filename.Contains("dds"))
                this.original_dds.Write(filename);

            else if (filename.Contains("BMP") || filename.Contains("bmp"))
                this.fixed_bmp.Save(filename, ImageFormat.Bmp);

            else return;
        }
    
        #endregion
    }


    


}
