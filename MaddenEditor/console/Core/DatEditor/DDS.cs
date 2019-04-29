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

using MaddenEditor.Core;


namespace MaddenEditor.Core.DatEditor
{
    [Flags]
    enum pformat
    {
        DDPF_ALPHAPIXELS = 0x1,             //  Surface has valid data in the Alpha bitmask
        DDPF_ALPHA = 0x2,                   //  Alpha only.  RGB bitcount contains bitcount for alpha channel.  Alpha bitmask contains valid data.
        DDPF_FOURCC = 0x4,                  //  various compressed modes
        DDPF_PALETTEINDEXED4 = 0x8,         //  4bit color indexed, this is not the setting we need
        DDPF_PALETTEINDEXEDTO8 = 0x10,      //  1,2,4 bit with an 8bit indexed palette    (I think this is the mode we want with 4 bit graphics)
        DDPF_PALETTEINDEXED8 = 0x20,        //  8 bit color indexed
        DDPF_RGB = 0x40,                    //  texture has uncompressed RGB. need to set alphapixels to on if there is valid alpha channel data, and set each channel mask up in pixel format
        DDPF_YUV = 0x200,
        DDPF_LUMINANCE = 0x20000
    }

    [Flags]
    enum format
    {
        DDSD_CAPS = 0x1,
        DDSD_HEIGHT = 0x2,
        DDSD_WIDTH = 0x4,
        DDSD_PITCH = 0X8,
        DDSD_PIXELFORMAT = 0x1000,
        DDSD_MIPMAPCOUNT = 0x20000,
        DDSD_LINEARSIZE = 0x80000,
        DDSD_DEPTH = 0x800000
    }

    [Flags]
    enum capstype
    {
        DDSCAPS_COMPLEX = 0x8,
        DDSCAPS_TEXTURE = 0x1000,
        DDSCAPS_MIPMAP = 0x400000
    }

    public class DDS_PF
    {
        #region Private Members
        private UInt32 _pf_size;
        private UInt32 _pf_flags;
        private UInt32 _pf_fourcc;
        private UInt32 _pf_RGB_bitcount;
        private UInt32 _pf_R_Bitmask;
        private UInt32 _pf_G_Bitmask;
        private UInt32 _pf_B_Bitmask;
        private UInt32 _pf_A_Bitmask;
        #endregion

        #region Public Members
        public UInt32 pf_size
        {
            get { return _pf_size; }
            set { _pf_size = value; }
        }
        public UInt32 pf_flags
        {
            get { return _pf_flags; }
            set { _pf_flags = value; }
        }
        public UInt32 pf_fourcc
        {
            get { return _pf_fourcc; }
            set { _pf_fourcc = value; }
        }
        public UInt32 pf_RGB_bitcount
        {
            get { return _pf_RGB_bitcount; }
            set { _pf_RGB_bitcount = value; }
        }
        public UInt32 pf_R_Bitmask
        {
            get { return _pf_R_Bitmask; }
            set { _pf_R_Bitmask = value; }
        }
        public UInt32 pf_G_Bitmask
        {
            get { return _pf_G_Bitmask; }
            set { _pf_G_Bitmask = value; }
        }
        public UInt32 pf_B_Bitmask
        {
            get { return _pf_B_Bitmask; }
            set { _pf_B_Bitmask = value; }
        }
        public UInt32 pf_A_Bitmask
        {
            get { return _pf_A_Bitmask; }
            set { _pf_A_Bitmask = value; }
        }
        #endregion

        public DDS_PF()
        {
            pf_size = 32;
            pf_flags = 0;
            pf_fourcc = 0;
            pf_RGB_bitcount = 0;
            pf_R_Bitmask = 0;
            pf_G_Bitmask = 0;
            pf_B_Bitmask = 0;
            pf_A_Bitmask = 0;
        }

        public void Read(BinaryReader binreader)
        {
            pf_size = binreader.ReadUInt32();
            pf_flags = binreader.ReadUInt32();
            pf_fourcc = binreader.ReadUInt32();
            pf_RGB_bitcount = binreader.ReadUInt32();
            pf_R_Bitmask = binreader.ReadUInt32();                              //  0x00ff0000
            pf_G_Bitmask = binreader.ReadUInt32();                              //  0x0000ff00
            pf_B_Bitmask = binreader.ReadUInt32();                              //  0x000000ff
            pf_A_Bitmask = binreader.ReadUInt32();                              //  0xff000000
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(pf_size);
            binwriter.Write(pf_flags);
            binwriter.Write(pf_fourcc);
            binwriter.Write(pf_RGB_bitcount);
            binwriter.Write(pf_R_Bitmask);
            binwriter.Write(pf_G_Bitmask);
            binwriter.Write(pf_B_Bitmask);
            binwriter.Write(pf_A_Bitmask);
        }
    }

    public class Res
    {
        public UInt32 res1;
        public UInt32 res2;
        public UInt32 res3;
        public UInt32 res4;
        public UInt32 res5;
        public UInt32 res6;
        public UInt32 res7;
        public UInt32 res8;
        public UInt32 res9;
        public UInt32 resa;
        public UInt32 resb;

        public Res()
        {
            res1 = 0;
            res2 = 0;
            res3 = 0;
            res4 = 0;
            res5 = 0;
            res6 = 0;
            res7 = 0;
            res8 = 0;
            res9 = 0;
            resa = 0;
            resb = 0;
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(res1);
            binwriter.Write(res2);
            binwriter.Write(res3);
            binwriter.Write(res4);
            binwriter.Write(res5);
            binwriter.Write(res6);
            binwriter.Write(res7);
            binwriter.Write(res8);
            binwriter.Write(res9);
            binwriter.Write(resa);
            binwriter.Write(resb);
        }

    }

    public class Header
    {
        //  128 byte structure.  4 bytes for header and 124 for the data.
        public UInt32 dds_id = 542327876;
        public UInt32 size = 124;
        #region Private Members
        private UInt32 _flags;
        private UInt32 _height;
        private UInt32 _width;
        private UInt32 _pitch;
        private UInt32 _depth;
        private UInt32 _mipmap_count;
        private Res _reserved;
        private DDS_PF _pixel_format;
        private UInt32 _caps;
        private UInt32 _caps2;
        private UInt32 _caps3;
        private UInt32 _caps4;
        private UInt32 _reserved2;
        #endregion
        
        #region Public Members
        public UInt32 flags
        {
            get { return _flags; }
            set { _flags = value; }
        }
        public UInt32 height
        {
            get { return _height; }
            set { _height = value; }
        }
        public UInt32 width
        {
            get { return _width; }
            set { _width = value; }
        }
        public UInt32 pitch
        {
            get { return _pitch; }
            set { _pitch = value; }
        }
        public UInt32 depth
        {
            get { return _depth; }
            set { _depth = value; }
        }
        public UInt32 mipmap_count
        {
            get { return _mipmap_count; }
            set { _mipmap_count = value; }
        }
        public Res reserved
        {
            get { return _reserved; }
            set { _reserved = value; }
        }
        public DDS_PF pixel_format
        {
            get { return _pixel_format; }
            set { _pixel_format = value; }
        }
        public UInt32 caps
        {
            get { return _caps; }
            set { _caps = value; }
        }
        public UInt32 caps2
        {
            get { return _caps2; }
            set { _caps2 = value; }
        }
        public UInt32 caps3
        {
            get { return _caps3; }
            set { _caps3 = value; }
        }
        public UInt32 caps4
        {
            get { return _caps4; }
            set { _caps4 = value; }
        }
        public UInt32 reserved2
        {
            get { return _reserved2; }
            set { _reserved2 = value; }
        }
        #endregion

        public Header()
        {
            flags = 0;
            height = 0;
            width = 0;
            pitch = 0;
            depth = 0;
            mipmap_count = 0;
            reserved = new Res();
            pixel_format = new DDS_PF();
            caps = 0;
            caps2 = 0;
            caps3 = 0;
            caps4 = 0;
            reserved2 = 0;
        }

        public Header(int w, int h, int mipmaps, int depth, bool hasalpha)
        {
            flags += (uint)(format.DDSD_CAPS | format.DDSD_HEIGHT | format.DDSD_WIDTH);
            flags += (uint)format.DDSD_PIXELFORMAT;
            flags += (uint)format.DDSD_LINEARSIZE;
            caps = (uint)capstype.DDSCAPS_TEXTURE;

            if (mipmaps > 1)
            {
                flags += (uint)(format.DDSD_MIPMAPCOUNT);
                caps += (uint)(capstype.DDSCAPS_MIPMAP | capstype.DDSCAPS_COMPLEX);
            }

            width = (uint)w;
            height = (uint)h;

            //  compute the pitch as:
            //  ( width * bits-per-pixel + 7 ) / 8    but this shouldnt be done unless we have a compressed texture, and we dont use those with madden.
            pitch = 0;
            mipmap_count = (uint)mipmaps;
            reserved = new Res();
            pixel_format = new DDS_PF();
            pixel_format.pf_RGB_bitcount = (uint)depth;
            if (depth == 24)
                pixel_format.pf_RGB_bitcount = 32;

            pixel_format.pf_flags = (uint)pformat.DDPF_PALETTEINDEXED8;

            if (pixel_format.pf_RGB_bitcount == 4)
                pixel_format.pf_flags = (uint)pformat.DDPF_PALETTEINDEXEDTO8;

            if (pixel_format.pf_RGB_bitcount == 32)
            {
                pixel_format.pf_flags = (uint)(pformat.DDPF_RGB);

                if (hasalpha)
                {
                    pixel_format.pf_flags += (uint)pformat.DDPF_ALPHAPIXELS;
                }

                pixel_format.pf_A_Bitmask = 0xff000000;
                pixel_format.pf_R_Bitmask = 0x00ff0000;
                pixel_format.pf_G_Bitmask = 0x0000ff00;
                pixel_format.pf_B_Bitmask = 0x000000ff;
            }

            pixel_format.pf_fourcc = 0;

            caps2 = 0;
            caps3 = 0;
            caps4 = 0;
            reserved2 = 0;
        }

        public Header(bitmapindex bmt, paletteindex pt, fileindex ft, bool containsalpha)
        {
            flags = (uint)(format.DDSD_CAPS | format.DDSD_HEIGHT | format.DDSD_WIDTH);
            flags += (uint)format.DDSD_PIXELFORMAT;
            caps = (uint)capstype.DDSCAPS_TEXTURE;

            if (ft.GfxMipmaps > 1)
            {
                flags += (uint)(format.DDSD_MIPMAPCOUNT);
                caps += (uint)(capstype.DDSCAPS_MIPMAP | capstype.DDSCAPS_COMPLEX);
            }

            height = bmt.BitMapHeight;
            width = bmt.BitMapWidth;
            pitch = 0;
            depth = 0;
            mipmap_count = ft.GfxMipmaps;
            reserved = new Res();
            pixel_format = new DDS_PF();
            pixel_format.pf_size = 32;
            pixel_format.pf_RGB_bitcount = 8;
            pixel_format.pf_flags = (uint)pformat.DDPF_PALETTEINDEXED8;
            if (bmt.BitMapFormat == 5)
            {
                pixel_format.pf_RGB_bitcount = 4;
                pixel_format.pf_flags = (uint)pformat.DDPF_PALETTEINDEXEDTO8;
            }
            if (bmt.BitMapFormat == 32)
            {
                pixel_format.pf_RGB_bitcount = 32;
                pixel_format.pf_flags = (uint)(pformat.DDPF_RGB);

                if (containsalpha)
                {
                    pixel_format.pf_flags += (uint)pformat.DDPF_ALPHAPIXELS;
                }

                pixel_format.pf_A_Bitmask = 0xff000000;
                pixel_format.pf_R_Bitmask = 0x00ff0000;
                pixel_format.pf_G_Bitmask = 0x0000ff00;
                pixel_format.pf_B_Bitmask = 0x000000ff;

            }

            pixel_format.pf_fourcc = 0;
            caps2 = 0;
            caps3 = 0;
            caps4 = 0;
            reserved2 = 0;
        }

        public bool Read(BinaryReader binreader)
        {
            if (dds_id != binreader.ReadUInt32())
            {
                binreader.BaseStream.Position -= 4;
                return false;
            }

            binreader.BaseStream.Position = 8;                      //  Skip ahead to data
            flags = binreader.ReadUInt32();
            height = binreader.ReadUInt32();
            width = binreader.ReadUInt32();
            pitch = binreader.ReadUInt32();
            depth = binreader.ReadUInt32();
            mipmap_count = binreader.ReadUInt32();
            reserved = new Res();                                   //  This isn't used
            binreader.BaseStream.Position += 44;                    //  Skip ahead
            pixel_format = new DDS_PF();
            pixel_format.Read(binreader);                           //  Pixel format
            caps = binreader.ReadUInt32();
            caps2 = binreader.ReadUInt32();
            caps3 = binreader.ReadUInt32();
            caps4 = binreader.ReadUInt32();
            reserved2 = binreader.ReadUInt32();

            return true;
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(this.dds_id);
            binwriter.Write(this.size);
            binwriter.Write(this.flags);
            binwriter.Write(this.height);
            binwriter.Write(this.width);
            binwriter.Write(this.pitch);
            binwriter.Write(this.depth);
            binwriter.Write(this.mipmap_count);
            this.reserved.Write(binwriter);
            this.pixel_format.Write(binwriter);
            binwriter.Write(caps);
            binwriter.Write(caps2);
            binwriter.Write(caps3);
            binwriter.Write(caps4);
            binwriter.Write(reserved2);
        }
    }

    public class DDS
    {
        #region Private Members
        private Header _header;
        private List<byte> _palette;
        private List<byte> _bitmap;
        private List<int> _bitmap_counts;
        private int _pal_count;
        private bool _processed;
        #endregion

        #region Public Members

        public Header header
        {
            get { return _header; }
            set { _header = value; }
        }
        public List<byte> palette
        {
            get { return _palette; }
            set { _palette = value; }
        }
        public List<byte> bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public List<int> bitmap_counts
        {
            get { return _bitmap_counts; }
            set { _bitmap_counts = value; }
        }
        public int pal_count
        {
            get { return _pal_count; }
            set { _pal_count = value; }
        }
        public bool processed
        {
            get { return _processed; }
            set { _processed = value; }
        }

        #endregion


        public DDS()
        {
            header = new Header();
            palette = new List<byte>();
            bitmap = new List<byte>();

            bitmap_counts = new List<int>();
            pal_count = 0;
            processed = false;
        }

        public void ReplacePalette(ColorPalette pal)
        {
            for (int c = 0; c < pal.Entries.Count(); c++)
            {
                this.palette[c * 4] = pal.Entries[c].R;
                this.palette[(c * 4) + 1] = pal.Entries[c].G;
                this.palette[(c * 4) + 2] = pal.Entries[c].B;
                this.palette[(c * 4) + 3] = pal.Entries[c].A;
            }
        }

        public void FixAllTransparentBMP()
        {
            if (this.palette.Count != 0)
                return;
            bool alltrans = false;
            for (int c = 4; c < this.bitmap.Count() / 4; c++)
            {
                if (this.bitmap[(c * 4) + 3] == 0)
                    alltrans = true;
            }

            if (alltrans)
                for (int c = 4; c < this.bitmap.Count() / 4; c++)
                    this.bitmap[(c * 4) + 3] = 255;
        }

        public bool ContainsAlpha()
        {
            if (this.header.pixel_format.pf_RGB_bitcount != 32)
            {
                for (int c = 3; c < this.palette.Count; c += 4)
                    if (palette[c] != 255)
                        return true;
            }
            if (this.header.pixel_format.pf_RGB_bitcount == 32)
            {
                for (int c = 3; c < this.bitmap.Count; c += 4)
                    if (this.bitmap[c] != 255)
                        return true;
            }
            return false;
        }

        public Color AlphaBlend(int entry, Color backcolor)                                     //  Blend palette color with background color if alpha exists
        {
            int red = this.palette[(entry * 4)];                                                //  RGBA for DDS.  We don't have to worry about the pixel format
            int green = this.palette[(entry * 4) + 1];                                          //  Since we are returning a color instead of data from each channel.
            int blue = this.palette[(entry * 4) + 2];
            int alpha = this.palette[(entry * 4) + 3];
            double fraction = (double)alpha / 255;

            double tred = (double)((double)red * ((double)alpha / 255)) + (double)backcolor.R * (1 - ((double)alpha / 255));
            double tgreen = ((double)green * ((double)alpha / 255)) + ((double)backcolor.G * (1 - ((double)alpha / 255)));
            double tblue = ((double)blue * ((double)alpha / 255)) + ((double)backcolor.B * (1 - ((double)alpha / 255)));

            int blend_red = (int)Math.Round(tred, MidpointRounding.AwayFromZero);
            if (blend_red / fraction > red)
                blend_red = (int)Math.Truncate(tred);

            int blend_green = (int)Math.Round(tgreen, MidpointRounding.AwayFromZero);
            if (blend_green / fraction > green)
                blend_green = (int)Math.Truncate(tgreen);

            int blend_blue = (int)Math.Round(tblue, MidpointRounding.AwayFromZero);
            if (blend_blue / fraction > blue)
                blend_blue = (int)Math.Truncate(tblue);

            return Color.FromArgb(255, blend_red, blend_green, blend_blue);
        }

        public Bitmap ConvertToBMP(Color backcolor)                                             //  This only works for the first level mipmap.
        {
            backcolor = Color.FromArgb(255, 255, 255, 255);                                     //  Backcolor to white
            
            PixelFormat pf = new PixelFormat();            
            if (this.header.pixel_format.pf_RGB_bitcount == 32)
                pf = PixelFormat.Format32bppArgb;
            else if (this.header.pixel_format.pf_RGB_bitcount == 4)
                pf = PixelFormat.Format4bppIndexed;
            else pf = PixelFormat.Format8bppIndexed;

            int w = (int)this.header.width;
            int h = (int)this.header.height;

            Bitmap bmp = new Bitmap(w, h, pf);

            byte[] bm = new byte[this.bitmap.Count];
            for (int c = 0; c < this.bitmap.Count; c++)
                bm[c] = this.bitmap[c];

            BitmapData bmdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(bm, 0, bmdata.Scan0, bm.Length);
            bmp.UnlockBits(bmdata);

            if (this.header.pixel_format.pf_RGB_bitcount <= 8)
            {
                ColorPalette bmpcolors = bmp.Palette;
                Color[] colors = bmpcolors.Entries;

                for (int c = 0; c < this.palette.Count / 4; c++)                    
                    colors[c] = this.AlphaBlend(c, backcolor);

                bmp.Palette = bmpcolors;
            }

            return bmp;
        }

        public bool ConvertFromBMP(Bitmap bmp)
        {
            int depth = 0;
            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                depth = 8;
            else if (bmp.PixelFormat == PixelFormat.Format4bppIndexed)
                depth = 4;
            else if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
            {
                depth = 32;
                Bitmap hidef = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(hidef);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                g.DrawImage(bmp, new Point(0, 0));
                g.Dispose();
                bmp = (Bitmap)hidef.Clone();
            }

            else if (bmp.PixelFormat == PixelFormat.Format32bppArgb || bmp.PixelFormat == PixelFormat.Format32bppRgb)
                depth = 32;
            else return false;

            this.header = new Header(bmp.Width, bmp.Height, 0, depth, false);

            byte[] bm = new byte[bmp.Width * bmp.Height * depth / 8];

            BitmapData bmdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);            
            IntPtr ptr = bmdata.Scan0;
            bmp.UnlockBits(bmdata);
            Marshal.Copy(ptr, bm, 0, bm.Count());
            this.bitmap = new List<byte>();

            for (int c = 0; c < bm.Count(); c++)
                bitmap.Add(bm[c]);

            if (depth <= 8)
            {
                FixAllTransparentBMP();
                ColorPalette bmpcolors = bmp.Palette;
                Color[] colors = bmpcolors.Entries;
                this.palette = new List<byte>();

                for (int c = 0; c < colors.Count(); c++)
                {
                    palette.Add(colors[c].R);
                    palette.Add(colors[c].G);
                    palette.Add(colors[c].B);
                    if (c == 0)
                        palette.Add(0);
                    else palette.Add(colors[c].A);
                }
            }

            return true;
        }
        
        
        #region I/O

        public bool Read(string filename)
        {
            if (filename == "") 
                return false;

            BinaryReader binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            

            if (this.header.Read(binreader) == false)
            {
                binreader.Close();
                return false;
            }

            pal_count = 1024;                                                                               //  256 color, each pixel is indexed to a color.
            if (this.header.pixel_format.pf_RGB_bitcount == 32)                                             //  32 bit color, each pixel is defined as a color.
                pal_count = 0;
            if (this.header.pixel_format.pf_RGB_bitcount == 4)                                              //  16 color, each pixel is indexed to a color.
                pal_count = 64;

            bitmap_counts.Add((int)(this.header.height * this.header.width * this.header.pixel_format.pf_RGB_bitcount / 8));        //  Add bitmap size
            for (int c = 1; c < this.header.mipmap_count; c++)
                bitmap_counts.Add((int)(this.header.height * this.header.width * this.header.pixel_format.pf_RGB_bitcount / 8 * Math.Pow((double)4, (double)c)));    //  Add bitmap sizes for mimpaps, if they exist.

            for (int c = 0; c < pal_count; c++)                                                             //  Read Palette
                palette.Add(binreader.ReadByte());

            for (int count = 0; count < bitmap_counts.Count; count++)                                       //  Read bitmaps
                for (int c = 0; c < bitmap_counts[count]; c++)
                    this.bitmap.Add(binreader.ReadByte());

            binreader.Close();
            return true;
        }

        public void Write(string filename)
        {
            if (filename == "")
                return;

            BinaryWriter binwriter = new BinaryWriter(File.Open(filename, FileMode.Create));
            
            this.header.Write(binwriter);
            for (int c = 0; c < this.palette.Count; c++)
                binwriter.Write(palette[c]);
            for (int c = 0; c < this.bitmap.Count; c++)
                binwriter.Write(bitmap[c]);

            binwriter.Close();
        }

        #endregion
    }


}
