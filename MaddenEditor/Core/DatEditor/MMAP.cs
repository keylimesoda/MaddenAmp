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
    enum palformat
    {
        //  All graphics that are not 32bit are indexed = 0x20  
        //  Colors are 32bit colors in the ABGR format
        //  If the palette has a valid alpha channel, we add 1
        //  Sometimes a graphic will be one color and only use the alpha channel to determine the blending, some numbers
        //  on uniforms/helemts do this, as well as logos that are on helmets.  Add 2 for this.  I don't know what the
        //  difference is from having a valid alpha channel and the other option that is alpha only.
        //  I think the one color used in the palette may still be valid?  Perhaps the color is passed through from
        //  something else to the palette in that case.

        ALPHAPIXELS = 0x1,
        ALPHAONLY = 0x2,
        PALETTEINDEXED8 = 0x20
    }

    //  This is for making a list of the graphics files in the MMAP and their properties   
    public class mmap_info_entry
    {
        #region Private Members
        private string _entry_name;
        private int _entry_width;
        private int _entry_height;
        private int _entry_depth;
        private int _entry_mipmaps;
        private int _entry_pals;
        private bool _entry_isalpha;

        private string _entry_numcolors;
        private int _pal_num;
        private int _bm_num;
        private List<mmap_info_entry> _child;
        #endregion

        #region Public Members

        public string entry_name
        {
            get { return _entry_name; }
            set { _entry_name = value; }
        }
        public int entry_width
        {
            get { return _entry_width; }
            set { _entry_width = value; }
        }
        public int entry_height
        {
            get { return _entry_height; }
            set { _entry_height = value; }
        }
        public int entry_depth
        {
            get { return _entry_depth; }
            set { _entry_depth = value; }
        }
        public int entry_mipmaps
        {
            get { return _entry_mipmaps; }
            set { _entry_mipmaps = value; }
        }
        public int entry_pals
        {
            get { return _entry_pals; }
            set { _entry_pals = value; }
        }
        public bool entry_isalpha
        {
            get { return _entry_isalpha; }
            set { _entry_isalpha = value; }
        }

        public string entry_numcolors
        {
            get { return _entry_numcolors; }
            set { _entry_numcolors = value; }
        }
        public int pal_num
        {
            get { return _pal_num; }
            set { _pal_num = value; }
        }
        public int bm_num
        {
            get { return _bm_num; }
            set { _bm_num = value; }
        }
        public List<mmap_info_entry> child
        {
            get { return _child; }
            set { _child = value; }
        }

        #endregion

        public mmap_info_entry()
        {
            entry_name = "";
            entry_width = 0;
            entry_height = 0;
            entry_depth = 0;
            entry_mipmaps = 0;
            entry_pals = 0;
            entry_isalpha = false;
            child = new List<mmap_info_entry>();
        }

        public mmap_info_entry(MMAP mmap, int filenum, int palnum)
        {
            int b = (int)mmap.File_Index_Table[filenum].GfxBitmapOffset;
            int p = (int)mmap.File_Index_Table[filenum].GfxPalettes;
            int alphacheck = (int)mmap.File_Index_Table[filenum].GfxPaletteOffset;
            if (palnum != -1)
            {
                p += palnum;
                alphacheck += palnum;
            }

            entry_name = mmap.FileNames[filenum];
            if (palnum != -1)
                entry_name += "_pal_" + palnum.ToString();

            entry_width = mmap.BM_Index_Table[b].BitMapWidth;
            entry_height = mmap.BM_Index_Table[b].BitMapHeight;

            entry_depth = 8;            //  Assume 8 bit            
            entry_numcolors = "256";

            if (mmap.BM_Index_Table[b].BitMapFormat == 32)
            {
                entry_depth = 32;
                entry_numcolors = "32bit";
            }

            if (mmap.BM_Index_Table[b].BitMapFormat == 5)
            {
                entry_depth = 4;
                entry_numcolors = "16";
            }

            entry_mipmaps = mmap.File_Index_Table[filenum].GfxMipmaps;
            List<int> test = mmap.Palettes[alphacheck].AlphaColors();

            entry_isalpha = false;
            if (test.Count != 0)
                entry_isalpha = true;

        }


    }


    #region Sub classes for MMAP

    public class mmap_header                                                    //  MMAP header
    {
        /*  MMAP Files 40 bytes for header, consisting of :
            4 bytes 'MMAP'  4D 4D 41 50
            2 bytes # Elements ?
            2 bytes on/off
            4 bytes name or type?
            2 bytes # files
            2 bytes # bitmaps

            2 bytes # palettes
            2 bytes ?
            4 bytes offset for File Table
            4 bytes header length
            4 bytes offset for Palette Table

            4 bytes offset for FileName Table
            4 bytes offset for Extra Info
        */

        #region Private Members

        private UInt32 _mmap_id;                            //  'MMAP'  0x4D4D4150     1346456909   
        private UInt16 _elements;
        private UInt16 _onoff = 0;                          //  Madden changes this value in memory to 01.  This MUST be 0 in the DAT files or game will crash.
        private UInt32 _type;                               //  Not sure what to call this, 'type' for now... doesn't change so leave it alone
        private UInt16 _mmap_files;
        private UInt16 _mmap_bitmaps;

        private UInt16 _mmap_palettes;
        private UInt16 _mmap_unknown;
        private UInt32 _FileTable_Offset;                   // This is offset before the start of the file table. 
        private UInt32 _BitmapTable_Offset = 0040;
        private UInt32 _palette_table_offset;

        private UInt32 _FileNames_Offset;                   // This is the offset before the filenames list
        private UInt32 _XI_Offset;                          //  This is offset for extra info (if it exists)  Only saw this once, might have been on player faces MMAPs.

        #endregion

        #region Public Members

        public UInt32 mmap_id
        {
            get { return _mmap_id; }
            set { _mmap_id = value; }
        }
        public UInt16 elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
        public UInt16 onoff
        {
            get { return _onoff; }
            set { _onoff = value; }
        }
        public UInt32 type
        {
            get { return _type; }
            set { _type = value; }
        }
        public UInt16 Files
        {
            get { return _mmap_files; }
            set { _mmap_files = value; }
        }
        public UInt16 Bitmaps
        {
            get { return _mmap_bitmaps; }
            set { _mmap_bitmaps = value; }
        }

        public UInt16 Palettes
        {
            get { return _mmap_palettes; }
            set { _mmap_palettes = value; }
        }
        public UInt16 Unknown
        {
            get { return _mmap_unknown; }
            set { _mmap_unknown = value; }
        }
        public UInt32 FileTable_Offset
        {
            get { return _FileTable_Offset; }
            set { _FileTable_Offset = value; }
        }
        public UInt32 BitmapTable_Offset
        {
            get { return _BitmapTable_Offset; }
            set { _BitmapTable_Offset = value; }
        }
        public UInt32 PaletteTable_Offset
        {
            get { return _palette_table_offset; }
            set { _palette_table_offset = value; }
        }

        public UInt32 FileNames_Offset
        {
            get { return _FileNames_Offset; }
            set { _FileNames_Offset = value; }
        }
        public UInt32 XI_Offset
        {
            get { return _XI_Offset; }
            set { _XI_Offset = value; }
        }

        #endregion

        public mmap_header()
        {
            mmap_id = 1346456909;
            elements = 2;
            onoff = 0;
            type = 50462976;
            Files = 0;
            Bitmaps = 0;

            Palettes = 0;
            Unknown = 0;
            FileTable_Offset = 0;
            BitmapTable_Offset = 40;
            PaletteTable_Offset = 0;

            FileNames_Offset = 0;
            XI_Offset = 0;
        }

    }

    public class BM_entry                                                       //  Bitmap data.  Pixels for images are stored top to bottom.
    {
        private List<byte> _bitmap;
        public List<byte> bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public BM_entry()
        {
            bitmap = new List<byte>();
        }

        public Color AlphaBlend32bit(int pixel, Color backcolor)                                    //  32bit, Blend pixel color with background color if alpha exists
        {
            int blue = this.bitmap[(pixel * 4)];                                                    //  ARGB format
            int green = this.bitmap[(pixel * 4) + 1];
            int red = this.bitmap[(pixel * 4) + 2];
            int alpha = this.bitmap[(pixel * 4) + 3];

            if (alpha != 255)
            {
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
                int blend_alpha = 255;

                return Color.FromArgb(blend_alpha, blend_red, blend_green, blend_blue);
            }

            Color Blended = Color.FromArgb(alpha, red, green, blue);
            return Blended;
        }

        public void CleanBitMap(List<int> scrublist)                                //  This might help some imports from having unwanted artifacts.  For Palettized images only.
        {
            for (int c = 0; c < this.bitmap.Count; c++)                         //  Check each pixel 
                foreach (int scrub in scrublist)                                //  against our list of scrub colors references
                {
                    if (this.bitmap[c] == scrub)                                //  If there is a match
                        this.bitmap[c] = 0;                                     //  Set that pixel to background color reference
                }
        }

        public void ConvertAlpha32bit(Color color)                                  //  ARGB format for 32 bit, for both bmp and dds
        {
            byte g = color.G;
            byte b = color.B;
            byte r = color.R;

            for (int c = 0; c < this.bitmap.Count; c += 4)
            {
                if (b == this.bitmap[(c * 4)] && g == this.bitmap[(c * 4) + 1] && r == this.bitmap[(c * 4) + 2])        //  If bytes in the pixel matches the color.
                    this.bitmap[(c * 4) + 3] = 0;                                                                       //  Change the Alpha channel to fully transparent.                
            }
        }

        public bool ContainsAlpha()
        {
            for (int c = 3; c < this.bitmap.Count; c += 4)
                if (this.bitmap[c] != 255)
                    return true;

            return false;
        }
    }

    public class Pal_entry                                                      //  Palette data for indexed graphics.  32bit ABRG format.
    {
        private List<byte> _dds_colors;

        public List<byte> dds_colors
        {
            get { return _dds_colors; }
            set { _dds_colors = value; }
        }

        public Pal_entry()
        {
            dds_colors = new List<byte>();
        }

        public Color GetColor(int colornum)                                     //  Return ARGB color from the DDS palette
        {
            int red = (int)this.dds_colors[colornum * 4];
            int green = (int)this.dds_colors[colornum * 4 + 1];
            int blue = (int)this.dds_colors[colornum * 4 + 2];
            int alpha = (int)this.dds_colors[colornum * 4 + 3];

            Color color = Color.FromArgb(alpha, red, green, blue);
            return color;
        }

        public List<int> AlphaColors()                                          //  Analyze Alpha values, return list of colors that have alpha
        {
            List<int> alphacolors = new List<int>();
            int count = 0;

            while (count < this.dds_colors.Count)
            {
                if (this.dds_colors[count + 3] != 255)                          //  Opaque is 255, anything less than that is using alpha
                    alphacolors.Add(count / 4);
                count += 4;
            }
            return alphacolors;
        }

        public void ConvertToAlpha(Color color)                                 //  Convert a specified color into alpha transparent
        {
            byte g = color.G;
            byte b = color.B;
            byte r = color.R;

            for (int c = 0; c < this.dds_colors.Count; c += 4)
            {
                if (r == this.dds_colors[c * 4] && g == this.dds_colors[(c * 4) + 1] && b == this.dds_colors[(c * 4) + 2])  //  If bytes in the palette match the color.
                    this.dds_colors[(c * 4) + 3] = 0;                                                                       //  Change the Alpha channel to fully transparent.                
            }
        }

        public bool ContainsAlpha()
        {
            for (int c = 3; c < this.dds_colors.Count; c += 4)
                if (this.dds_colors[c] != 255)
                    return true;
            return false;
        }

        public Color AlphaBlend(int entry, Color backcolor)                                         //  Blend palette color with background color if alpha exists
        {
            int red = this.dds_colors[(entry * 4)];                                                 //  ABRG for DDS.  We don't have to worry about the pixel format
            int green = this.dds_colors[(entry * 4) + 1];                                           //  Since we are returning colors instead of data from each channel.
            int blue = this.dds_colors[(entry * 4) + 2];
            int alpha = this.dds_colors[(entry * 4) + 3];
                       
            double fraction = (double)alpha / 255;

            double tred = ((double)red * ((double)alpha / 255)) + ((double)backcolor.R * (1 - ((double)alpha / 255)));
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
            int blend_alpha = 255;

            return Color.FromArgb(blend_alpha, blend_red, blend_green, blend_blue);
        }
    
    }

    public class bitmapindex                                                    //  Bitmap Table entry
    {
        // 16 bytes per entry
        #region Private Members
        private UInt16 _BitMapWidth;
        private UInt16 _BitMapHeight;
        private UInt32 _BitMapFormat;
        private UInt32 _BitMapSize;
        private UInt32 _BitMapOffset;
        #endregion

        #region Public Members

        public UInt16 BitMapWidth
        {
            get { return _BitMapWidth; }
            set { _BitMapWidth = value; }
        }
        public UInt16 BitMapHeight
        {
            get { return _BitMapHeight; }
            set { _BitMapHeight = value; }
        }
        public UInt32 BitMapFormat                                              //  4bit = 5 , 8 bit = 9 , 32bit = 32
        {
            get { return _BitMapFormat; }
            set { _BitMapFormat = value; }
        }
        public UInt32 BitMapSize
        {
            get { return _BitMapSize; }
            set { _BitMapSize = value; }
        }
        public UInt32 BitMapOffset
        {
            get { return _BitMapOffset; }
            set { _BitMapOffset = value; }
        }

        #endregion

        public bitmapindex()
        {
            BitMapWidth = 0;
            BitMapHeight = 0;
            BitMapFormat = 0;
            BitMapSize = 0;
            BitMapOffset = 0;
        }

        public bitmapindex(int width, int height, int color, int size)
        {
            BitMapWidth = (UInt16)width;
            BitMapHeight = (UInt16)height;
            BitMapFormat = (uint)color;
            BitMapSize = (uint)size;
            BitMapOffset = 0;
        }


    }

    public class paletteindex                                                   //  Palette Table entry
    {
        // 12 Bytes per entry
        //  color depth 256 color = 1  16 color = 0
        // palette format = pal format.  always indexed8, +1 if alpha present, +2 is it is alpha only (one color used in palette)

        #region Private Members
        private UInt16 _Palette_Color_Depth;
        private UInt16 _Palette_Format;
        private UInt32 _PaletteSize;
        private UInt32 _PaletteOffset;
        #endregion

        #region Public Members

        public UInt16 Palette_Color_Depth
        {
            get { return _Palette_Color_Depth; }
            set { _Palette_Color_Depth = value; }
        }
        public UInt16 Palette_Format
        {
            get { return _Palette_Format; }
            set { _Palette_Format = value; }
        }
        public UInt32 PaletteSize
        {
            get { return _PaletteSize; }
            set { _PaletteSize = value; }
        }
        public UInt32 PaletteOffset
        {
            get { return _PaletteOffset; }
            set { _PaletteOffset = value; }
        }

        #endregion

        public paletteindex()
        {
            Palette_Color_Depth = 0;
            Palette_Format = 0;
            PaletteSize = 0;
            PaletteOffset = 0;
        }

    }

    public class fileindex                                                      //  File Table entry
    {
        //  12 bytes per entry.
        #region Private Members
        private UInt16 _Gfx_Palettes;
        private UInt16 _Gfx_Mipmaps;
        private UInt32 _Gfx_Bitmap_Offset;
        private UInt32 _Gfx_Palette_Offset;
        #endregion

        #region Public members
        public UInt16 GfxPalettes
        {
            get { return _Gfx_Palettes; }
            set { _Gfx_Palettes = value; }
        }
        public UInt16 GfxMipmaps
        {
            get { return _Gfx_Mipmaps; }
            set { _Gfx_Mipmaps = value; }
        }
        public UInt32 GfxBitmapOffset
        {
            get { return _Gfx_Bitmap_Offset; }
            set { _Gfx_Bitmap_Offset = value; }
        }
        public UInt32 GfxPaletteOffset
        {
            get { return _Gfx_Palette_Offset; }
            set { _Gfx_Palette_Offset = value; }
        }
        #endregion

        public fileindex()
        {
            GfxPalettes = 0;
            GfxMipmaps = 0;
            GfxBitmapOffset = 0;
            GfxPaletteOffset = 0;
        }

        public fileindex(int pals, int bms, int bmoff, int paloff)
        {
            GfxPalettes = (UInt16)pals;
            GfxMipmaps = (UInt16)bms;
            GfxBitmapOffset = (uint)bmoff;
            GfxPaletteOffset = (uint)paloff;
        }

    }

    public class Xtra_Info                                                      //  Extra info, like what is seen on the end of player faces.  Probably Geometry related but not sure.
    {
        #region Private Members
        private UInt32 _xilength;       //  Length of xibytes
        private UInt32 _xioffset;       //  Offset of xibytes
        private List<byte> _xibytes;
        #endregion

        #region Public Members

        public UInt32 xilength
        {
            get { return _xilength; }
            set { _xilength = value; }
        }
        public UInt32 xioffset
        {
            get { return _xioffset; }
            set { _xioffset = value; }
        }
        public List<byte> xibytes
        {
            get { return _xibytes; }
            set { _xibytes = value; }
        }

        #endregion

        public Xtra_Info()
        {
            xilength = 0;
            xioffset = 0;
            xibytes = new List<byte>();
        }
    }

    #endregion

    public class MMAP
    {
        #region Private Members
        private mmap_header _header;
        private List<bitmapindex> _BM_Table;
        private List<BM_entry> _Bitmaps;
        private List<paletteindex> _Pal_Table;
        private List<Pal_entry> _Palettes;
        private List<string> _Filenames;
        private List<fileindex> _File_Table;
        private Xtra_Info _Xinfo;
        private List<mmap_info_entry> _info_list;       
        #endregion

        #region Public members

        public mmap_header Header
        {
            get { return _header; }
            set { _header = value; }
        }
        public List<bitmapindex> BM_Index_Table
        {
            get { return _BM_Table; }
            set { _BM_Table = value; }
        }
        public List<BM_entry> Bitmaps
        {
            get { return _Bitmaps; }
            set { _Bitmaps = value; }
        }
        public List<paletteindex> Pal_Index_Table
        {
            get { return _Pal_Table; }
            set { _Pal_Table = value; }
        }
        public List<Pal_entry> Palettes
        {
            get { return _Palettes; }
            set { _Palettes = value; }
        }
        public List<string> FileNames
        {
            get { return _Filenames; }
            set { _Filenames = value; }
        }
        public List<fileindex> File_Index_Table
        {
            get { return _File_Table; }
            set { _File_Table = value; }
        }
        public Xtra_Info Xinfo
        {
            get { return _Xinfo; }
            set { _Xinfo = value; }
        }
        public List<mmap_info_entry> infolist
        {
            get { return _info_list; }
            set { _info_list = value; }
        }

        #endregion

        public bool bitmaponly = false;
        public int currentPal = 0;
        public int currentBM = 0;
        public int currentpos = 0;
        public bool parentgraphic = false;
        public bool changed = false;
        public bool needsfixed = false;
        public bool valid = true;

        public MMAP()
        {
            Header = new mmap_header();
            BM_Index_Table = new List<bitmapindex>();
            Pal_Index_Table = new List<paletteindex>();
            File_Index_Table = new List<fileindex>();
            Palettes = new List<Pal_entry>();
            Bitmaps = new List<BM_entry>();
            FileNames = new List<string>();
            Xinfo = new Xtra_Info();

            infolist = new List<mmap_info_entry>();                                     //  List of graphics and attributes of the MMAP            
            currentpos = -1;
            parentgraphic = false;
            changed = false;
            needsfixed = false;
            valid = true;
        }



        #region MMAP Functions

        public int GetPad(int bytecount)
        {
            decimal multiple = Convert.ToDecimal(bytecount) / 32;
            int thispad = Convert.ToInt32(System.Math.Ceiling(multiple) * 32) - bytecount;
            return thispad;
        }

        public int GetSize()
        {                                                                       //  MMAPs have their own rules for padding.
            int length = 40;                                                    //  Add header length
            length += ((int)this.Header.Bitmaps * 16);                          //  Bitmap Table length
            length += GetPad(40 + (int)(this.Header.Bitmaps * 16));             //  padding after total of above to a multiple of 32 bytes

            int bitmapbytes = 0;
            foreach (BM_entry bm in this.Bitmaps)                               //  add all bitmap byte counts
                bitmapbytes += bm.bitmap.Count;
            length += bitmapbytes;

            //  total bytes for the bitmaps may be padded to 32, but not entirely sure... this appears to be incorrect, no padding needed
            //  length += GetPad(bitmapbytes);

            length += this.Header.Palettes * 12;                                //  Palette Table length
            length += GetPad(this.Header.Palettes * 12);                        //  padding palette table length to a multiple of 32 bytes

            foreach (Pal_entry pe in this.Palettes)                             //  add all palette byte counts
                length += pe.dds_colors.Count;                                  //  this is always a multiple of 32 anyway, so no need to do any padding here

            length += (this.Header.Files * 16) + (this.Header.Files * 12);      //  filenames @ 16 each + filetable data @ 12 per entry.  No padding?

            if (this.Header.XI_Offset > 0)                                      //  If exists add extra info byte count
                length += 8 + (int)this.Xinfo.xilength;                         //  No padding here, end of the mmap

            return length;
        }

        public void FixMMAP(int count)
        {
            if (count == 26)

            //  Fix total numbers of files, bitmaps, palettes
            this.Header.Bitmaps = (ushort)this.BM_Index_Table.Count;
            this.Header.Palettes = (ushort)this.Pal_Index_Table.Count;
            this.Header.Files = (ushort)this.File_Index_Table.Count;

            int offset = 40;
            //  Bitmap table + any padding
            offset += (this.BM_Index_Table.Count * 16);
            offset += GetPad(offset);

            int bitmapbytes = 0;
            for (int c = 0; c < this.Bitmaps.Count; c++)
                bitmapbytes += this.Bitmaps[c].bitmap.Count;
            offset += bitmapbytes;

            //  looks like no padding needed
            //  offset += GetPad(bitmapbytes);

            //  Palette table + padding if needed
            this.Header.PaletteTable_Offset = (uint)offset;
            if (this.Header.Palettes == 0)
                this.Header.PaletteTable_Offset = 0;
            else
            {
                offset += (this.Pal_Index_Table.Count * 12);
                offset += GetPad(this.Pal_Index_Table.Count * 12);

                int paltablebytes = 0;
                for (int c = 0; c < this.Pal_Index_Table.Count; c++)
                    paltablebytes += this.Palettes[c].dds_colors.Count;        
                offset += paltablebytes;
            }

            //  Filenames offset
            this.Header.FileNames_Offset = (uint)offset;

            //  filetable offset
            offset += this.FileNames.Count * 16;
            this.Header.FileTable_Offset = (uint)offset;

            // xtra info offset, this is used in player faces, and perhaps other graphics
            offset += (this.File_Index_Table.Count * 12);
            
            if (this.Xinfo.xibytes.Count == 0)
                this.Header.XI_Offset = 0;
            else
            {
                this.Header.XI_Offset = (uint)offset;                
            }

            this.needsfixed = false;
        }

        public void InitDC()
        {
            this.Header = new mmap_header();
            this.Header.Bitmaps = 1;
            this.Header.Files = 1;
            this.Header.Palettes = 1;
            this.Header.FileTable_Offset = 208;
            this.Header.BitmapTable_Offset = 40;
            this.Header.PaletteTable_Offset = 96;
            this.Header.FileNames_Offset = 192;
            this.Header.XI_Offset = 0;

            this.BM_Index_Table = new List<bitmapindex>();
            bitmapindex bmi = new bitmapindex();
            bmi.BitMapWidth = 8;
            bmi.BitMapHeight = 8;
            bmi.BitMapFormat = 5;
            bmi.BitMapSize = 32;
            bmi.BitMapOffset = 64;
            this.BM_Index_Table.Add(bmi);

            this.Bitmaps = new List<BM_entry>();
            BM_entry bm = new BM_entry();
            for (int c = 0; c < 32; c++)
                bm.bitmap.Add(0);
            this.Bitmaps.Add(bm);

            this.Pal_Index_Table = new List<paletteindex>();
            paletteindex pi = new paletteindex();
            pi.Palette_Color_Depth = 0;
            pi.Palette_Format = 33;
            pi.PaletteSize = 64;
            pi.PaletteOffset = 128;
            this.Pal_Index_Table.Add(pi);

            this.Palettes = new List<Pal_entry>();
            Pal_entry pal = new Pal_entry();            
            for (int p = 0; p < 64; p += 3)
            {
                if (p == 0)
                {
                    pal.dds_colors.Add(255);
                    pal.dds_colors.Add(255);
                    pal.dds_colors.Add(255);
                    pal.dds_colors.Add(0);
                }
                else
                {
                    pal.dds_colors.Add(0);
                    pal.dds_colors.Add(0);
                    pal.dds_colors.Add(0);
                    pal.dds_colors.Add(255);
                }
            }
            this.Palettes.Add(pal);

            this.FileNames = new List<string>();
            this.FileNames.Add("");

            this.File_Index_Table = new List<fileindex>();
            fileindex fi = new fileindex();
            fi.GfxPalettes = 1;
            fi.GfxMipmaps = 1;
            fi.GfxBitmapOffset = 0;
            fi.GfxPaletteOffset = 0;
            this.File_Index_Table.Add(fi);
        }

        //  to do : fix these
        public void ImportGraphic(DDS customdds)
        {
            bool bmonly = false;
            if (customdds.header.pixel_format.pf_RGB_bitcount >= 32)
                bmonly = true;

            #region File Table Index table

            this.File_Index_Table = new List<fileindex>();
            fileindex fi = new fileindex();
            if (bmonly)
            {
                fi.GfxPalettes = 0;                
            }
            else fi.GfxPalettes = 1;
            fi.GfxMipmaps = 1;

            this.File_Index_Table.Add(fi);
            
            #endregion

            #region Bitmap Index Table

            this.BM_Index_Table = new List<bitmapindex>();            
            bitmapindex index = new bitmapindex();

            index.BitMapFormat = 9;
            if (customdds.header.pixel_format.pf_RGB_bitcount == 4)
                index.BitMapFormat = 5;
            if (customdds.header.pixel_format.pf_RGB_bitcount == 32)
                index.BitMapFormat = 32;
            index.BitMapHeight = (ushort)(customdds.header.height);
            index.BitMapWidth = (ushort)(customdds.header.width);
            index.BitMapSize = (uint)(index.BitMapWidth * index.BitMapHeight * (customdds.header.pixel_format.pf_RGB_bitcount / 8));            

            this.BM_Index_Table.Add(index);

            this.FixBMTable();

            #endregion

            #region Bitmaps

            this.Bitmaps = new List<BM_entry>();
            BM_entry newbm = new BM_entry();
            for (int c = 0; c < customdds.bitmap.Count; c++)
                newbm.bitmap.Add(customdds.bitmap[c]);
            this.Bitmaps.Add(newbm);

            #endregion
            
            #region Palette Index Table

            this.Pal_Index_Table = new List<paletteindex>();
            if (!bmonly)
            {
                paletteindex pi = new paletteindex();
                pi.Palette_Format = (int)palformat.PALETTEINDEXED8 + (int)palformat.ALPHAPIXELS;

                if (customdds.header.pixel_format.pf_RGB_bitcount == 4)
                {
                    pi.Palette_Color_Depth = 0;
                    pi.PaletteSize = 64;
                }
                else
                {
                    pi.Palette_Color_Depth = 1;
                    pi.PaletteSize = 1024;
                }

                this.Pal_Index_Table.Add(pi);

                FixPalTable();
            }
            #endregion

            #region Palette

            this.Palettes = new List<Pal_entry>();
            if (!bmonly)
            {
                Pal_entry pe = new Pal_entry();
                for (int c = 0; c < customdds.palette.Count; c++)
                    pe.dds_colors.Add(customdds.palette[c]);

                this.Palettes.Add(pe);
            }

            #endregion
            this.needsfixed = true;
            this.changed = true;
        }
               
        public void FixBMTable()
        {
            //  Fix offsets
            uint offset = 40;                                                                           //  Header bytes
            offset += (uint)(this.BM_Index_Table.Count * 16);                                           //  Bitmap table length
            offset += (uint)GetPad((int)offset);                                                        //  Pad total to multiple of 32 bytes

            this.BM_Index_Table[0].BitMapOffset = offset;                                               //  Set current to current offset           
        }

        public void FixPalTable()
        {
            //  Fix offsets, starting offset to pal table is last bitmap entry offset + last bitmap entry size
            uint offset = this.BM_Index_Table[0].BitMapOffset + this.BM_Index_Table[0].BitMapSize;
            offset += (uint)GetPad((int)this.BM_Index_Table[0].BitMapSize);                                     //  Add padding for bitmap, if needed
            uint pt_length = (uint)(this.Pal_Index_Table.Count * 12);                                           //  Add Length of Palette Index Table
            pt_length += (uint)GetPad((int)pt_length);                                                          //  Pad this length to multiple of 32 byte
            offset += pt_length;                                                                                //  Add palette index table length            
            this.Pal_Index_Table[0].PaletteOffset = offset;                                                     //  Set current entry offset            
        }
                
        public void Fix_XInfo()
        {
            this.Xinfo.xilength = (uint)this.Xinfo.xibytes.Count;
            this.Xinfo.xioffset = (uint)this.Header.FileTable_Offset;
            this.Xinfo.xioffset += (uint)((this.File_Index_Table.Count * 12) + 8);
        }
        
        #endregion
        
        public Image GetPortraitDisplay()                                                                //  Creates a bmp image for displaying
        {
            Color backcolor = Color.FromArgb(255,255,255,255);
            int pos = 0;
            int BMnum = (int)this.File_Index_Table[pos].GfxBitmapOffset;
            int palnum = (int)this.File_Index_Table[pos].GfxPaletteOffset;
            PixelFormat pf = new PixelFormat();
            pf = PixelFormat.Format8bppIndexed;
            if (this.BM_Index_Table[BMnum].BitMapFormat == 5)
                pf = PixelFormat.Format4bppIndexed;
            if (this.BM_Index_Table[BMnum].BitMapFormat >= 32)
                pf = PixelFormat.Format32bppArgb;

            int w = (int)this.BM_Index_Table[BMnum].BitMapWidth;
            int h = (int)this.BM_Index_Table[BMnum].BitMapHeight;

            byte[] bm = new byte[this.BM_Index_Table[BMnum].BitMapSize];
            List<byte> bitmap_list = new List<byte>();

            if (this.BM_Index_Table[BMnum].BitMapFormat < 32)
            {
                for (int c = 0; c < this.BM_Index_Table[BMnum].BitMapSize; c++)
                {
                    bm[c] = this.Bitmaps[BMnum].bitmap[c];
                }
            }

            if (this.BM_Index_Table[BMnum].BitMapFormat >= 32)
            {
                for (int c = 0; c < this.BM_Index_Table[BMnum].BitMapSize; c += 4)
                {
                    bm[c + 0] = this.Bitmaps[BMnum].bitmap[c + 0];                                              //  Copy RGB straight over and change alpha to opaque
                    bm[c + 1] = this.Bitmaps[BMnum].bitmap[c + 1];                                              //  Don't think I have seen a 32bit graphic in the game with
                    bm[c + 2] = this.Bitmaps[BMnum].bitmap[c + 2];                                              //  valid alpha channel.  Can always call the alpha blend here later if needed
                    bm[c + 3] = this.Bitmaps[BMnum].bitmap[c + 3];
                }
            }

            Bitmap bmp = new Bitmap(w, h, pf);

            BitmapData bmdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
            

            if (this.BM_Index_Table[BMnum].BitMapFormat < 32)
            {
                ColorPalette bmpcolors = bmp.Palette;
                Color[] colors = bmpcolors.Entries;
                Dictionary<int, Color> colorlist = new Dictionary<int, Color>();                //  index #, Newcolor               
                colorlist.Add(0, Color.FromArgb(255, 255, 255, 255));                           //  color 0 = transparent, but will turn this white for display
                
                int numcolors = this.Palettes[palnum].dds_colors.Count()/4;
                                
                for (int p = 0; p < this.Bitmaps[BMnum].bitmap.Count; p++)                      
                {                    
                    int index = this.Bitmaps[BMnum].bitmap[p];
                    if (this.Palettes[palnum].dds_colors[(index * 4) + 3] == 0)                 //  Any color that is completely transparent, change to color0
                        bitmap_list.Add(0);
                    else
                    {
                        Color newcolor = this.Palettes[palnum].AlphaBlend(index, backcolor);    //  get blended color from current pixel
                        if (!colorlist.ContainsValue(newcolor))                                 //  if color isnt in our dictionary, add it
                            colorlist.Add(colorlist.Count, newcolor);   
                        foreach (KeyValuePair<int, Color> pair in colorlist)
                            if (pair.Value == newcolor) 
                                bitmap_list.Add((byte)pair.Key);                                //  change current pixel to match our new index #
                    }
                }

                for (int p = 0; p < bm.Count(); p++)                                            //  copy our fixed bitmap to the array for the image
                    bm[p] = bitmap_list[p];

                for (int c = 0; c < numcolors; c++)
                {
                    if (c >= colorlist.Count)                                                   //  Any missing colors we just fill in with background color
                        colors[c] = backcolor;
                    else
                        colors[c] = colorlist[c];                                               //  Otherwise transfer our fixed color to the image palette
                }
                
                bmp.Palette = bmpcolors;                                                        //  set image to use our fixed palette
            }

            Marshal.Copy(bm, 0, bmdata.Scan0, bm.Length);                                       //  Set image to our fixed bitmap
            bmp.UnlockBits(bmdata);
            
            Image image = (Image)bmp;                                                           //  create image

            return image;
        }
       
        #region Input/Output

        public bool Read(DAT dat)
        {
            int count = 0;
            if (this.Header.mmap_id != dat.binreader.ReadUInt32())
            {
                dat.binreader.BaseStream.Position -= 4;
                return false;
            }            

            else
            {
                this.Header.elements = dat.binreader.ReadUInt16();
                this.Header.onoff = dat.binreader.ReadUInt16();
                this.Header.type = dat.binreader.ReadUInt32();
                this.Header.Files = dat.binreader.ReadUInt16();
                this.Header.Bitmaps = dat.binreader.ReadUInt16();

                this.Header.Palettes = dat.binreader.ReadUInt16();
                this.Header.Unknown = dat.binreader.ReadUInt16();
                this.Header.FileTable_Offset = dat.binreader.ReadUInt32();
                this.Header.BitmapTable_Offset = dat.binreader.ReadUInt32();
                this.Header.PaletteTable_Offset = dat.binreader.ReadUInt32();

                this.Header.FileNames_Offset = dat.binreader.ReadUInt32();
                this.Header.XI_Offset = dat.binreader.ReadUInt32();
                dat.bytecount += 40;
                count = 40;

                if (this.Header.Files > 1 || this.Header.Bitmaps > 1 || this.Header.Palettes > 1)
                {
                    dat.errormsg = "Not a Portrait file";
                    return false;
                }

                // Read Bitmap Table                
                for (int c = 0; c < this.Header.Bitmaps; c++)
                {
                    bitmapindex newbme = new bitmapindex();

                    newbme.BitMapWidth = dat.binreader.ReadUInt16();
                    newbme.BitMapHeight = dat.binreader.ReadUInt16();
                    newbme.BitMapFormat = dat.binreader.ReadUInt32();
                    newbme.BitMapSize = dat.binreader.ReadUInt32();
                    newbme.BitMapOffset = dat.binreader.ReadUInt32();

                    this.BM_Index_Table.Add(newbme);
                    dat.bytecount += 16;
                    count += 16;
                }

                // Read Padding bytes for BitMap table.  Multiple of 32 bytes, plus header
                // of 40 bytes.  These are null bytes.
                dat.binreader.BaseStream.Position += dat.GetPad(32, (int)(this.Header.Bitmaps * 16 + 40));
                dat.bytecount += dat.GetPad(32, (int)(this.Header.Bitmaps * 16 + 40));
                count += dat.GetPad(32, (int)(this.Header.Bitmaps * 16 + 40));

                // Read in Bitmaps.
                this.Bitmaps = new List<BM_entry>();
                int pixelcount = 0;
                for (int c = 0; c < this.Header.Bitmaps; c++)
                {
                    BM_entry bme = new BM_entry();
                    for (int b = 0; b < (int)this.BM_Index_Table[c].BitMapSize; b++)
                        bme.bitmap.Add(dat.binreader.ReadByte());

                    this.Bitmaps.Add(bme);
                    pixelcount += bme.bitmap.Count();
                    count += pixelcount;
                }

                if (count < this.Header.PaletteTable_Offset)
                {
                    dat.binreader.BaseStream.Position += (this.Header.PaletteTable_Offset - count);
                    dat.warning = true;
                }
                //  Read in Palette table.
                if (this.Header.Palettes > 0)
                {
                    this.Pal_Index_Table = new List<paletteindex>();
                    for (int c = 0; c < this.Header.Palettes; c++)
                    {
                        paletteindex pte = new paletteindex();

                        pte.Palette_Color_Depth = dat.binreader.ReadUInt16();
                        pte.Palette_Format = dat.binreader.ReadUInt16();
                        pte.PaletteSize = dat.binreader.ReadUInt32();
                        pte.PaletteOffset = dat.binreader.ReadUInt32();

                        this.Pal_Index_Table.Add(pte);
                        dat.bytecount += 12;
                    }

                    // Read through Palette Padding bytes.  Palette Table 12 bytes per entry.
                    // Multiple of 32 bytes
                    dat.binreader.BaseStream.Position += dat.GetPad(32, (int)(this.Header.Palettes * 12));
                    dat.bytecount += dat.GetPad(32, (int)(this.Header.Palettes * 12));


                    // Read in palettes
                    this.Palettes = new List<Pal_entry>();
                    for (int c = 0; c < this.Header.Palettes; c++)
                    {
                        Pal_entry pal = new Pal_entry();

                        for (int p = 0; p < this.Pal_Index_Table[c].PaletteSize; p++)
                            pal.dds_colors.Add(dat.binreader.ReadByte());

                        this.Palettes.Add(pal);
                        dat.bytecount += pal.dds_colors.Count();
                    }
                }

                // FileNames Listing exists, read 16 bytes per entry
                this.FileNames = new List<string>();
                for (int c = 0; c < this.Header.Files; c++)
                {
                    ASCIIEncoding enc = new ASCIIEncoding();
                    List<byte> bytename = new List<byte>();

                    for (int n = 0; n < 16; n++)
                    {
                        byte b = dat.binreader.ReadByte();
                        if (b != 0)
                            bytename.Add(b);
                        dat.bytecount++;
                    }
                    if (bytename.Count > 0)
                        dat.warning = true;
                    this.FileNames.Add(enc.GetString(bytename.ToArray()));
                }

                int pos = (int)dat.binreader.BaseStream.Position;
                // Filename list is now done.  Now read in the filetable  
                this.File_Index_Table = new List<fileindex>();
                for (int c = 0; c < this.Header.Files; c++)
                {
                    fileindex fte = new fileindex();
                    fte.GfxPalettes = dat.binreader.ReadUInt16();
                    fte.GfxMipmaps = dat.binreader.ReadUInt16();
                    fte.GfxBitmapOffset = dat.binreader.ReadUInt32();
                    fte.GfxPaletteOffset = dat.binreader.ReadUInt32();

                    this.File_Index_Table.Add(fte);
                    dat.bytecount += 12;
                }

                // Need to check for xtra_info as in the face files
                if (this.Header.XI_Offset > 0)
                {
                    this.Xinfo = new Xtra_Info();
                    this.Xinfo.xilength = dat.binreader.ReadUInt32();
                    this.Xinfo.xioffset = dat.binreader.ReadUInt32();

                    for (int x = 0; x < this.Xinfo.xilength; x++)
                        this.Xinfo.xibytes.Add(dat.binreader.ReadByte());

                    dat.bytecount += 8 + this.Xinfo.xibytes.Count();
                }

                return true;
            }
        }

        public void Write(DAT dat, int count)
        {
            #region Header
            dat.binwriter.Write(this.Header.mmap_id);
            dat.binwriter.Write(this.Header.elements);
            dat.binwriter.Write(this.Header.onoff);
            dat.binwriter.Write(this.Header.type);
            dat.binwriter.Write(this.Header.Files);
            dat.binwriter.Write(this.Header.Bitmaps);

            dat.binwriter.Write(this.Header.Palettes);
            dat.binwriter.Write(this.Header.Unknown);
            dat.binwriter.Write(this.Header.FileTable_Offset);
            dat.binwriter.Write(this.Header.BitmapTable_Offset);
            dat.binwriter.Write(this.Header.PaletteTable_Offset);

            dat.binwriter.Write(this.Header.FileNames_Offset);
            dat.binwriter.Write(this.Header.XI_Offset);
            #endregion

            #region Bitmap Table
            for (int c = 0; c < this.Header.Bitmaps; c++)                                                       //  Bitmap Table
            {
                dat.binwriter.Write(this.BM_Index_Table[c].BitMapWidth);
                dat.binwriter.Write(this.BM_Index_Table[c].BitMapHeight);
                dat.binwriter.Write(this.BM_Index_Table[c].BitMapFormat);
                dat.binwriter.Write(this.BM_Index_Table[c].BitMapSize);
                dat.binwriter.Write(this.BM_Index_Table[c].BitMapOffset);
            }
            dat.WriteNulls(dat.GetPad(32, (40 + (int)this.Header.Bitmaps * 16)));                //  Padding
            #endregion

            #region Bitmaps

            int pixelcount = 0;
            for (int c = 0; c < this.Header.Bitmaps; c++)                                                       //  Bitmaps
                for (int pixel = 0; pixel < this.Bitmaps[c].bitmap.Count; pixel++)
                {
                    dat.binwriter.Write(this.Bitmaps[c].bitmap[pixel]);
                    pixelcount += this.Bitmaps[c].bitmap.Count;
                }

            //  bitmaps don't need padding
            //  dat.WriteNulls(GetPad(pixelcount));

            #endregion

            #region Palette Table
            if (this.Header.Palettes > 0)
            {
                for (int c = 0; c < this.Header.Palettes; c++)                                                      //  Palette table
                {
                    dat.binwriter.Write(this.Pal_Index_Table[c].Palette_Color_Depth);
                    dat.binwriter.Write(this.Pal_Index_Table[c].Palette_Format);
                    dat.binwriter.Write(this.Pal_Index_Table[c].PaletteSize);
                    dat.binwriter.Write(this.Pal_Index_Table[c].PaletteOffset);
                }
                dat.WriteNulls(dat.GetPad(32, (int)this.Header.Palettes * 12));                      //  Padding

            #endregion

                #region Palettes

                for (int c = 0; c < this.Header.Palettes; c++)                                                      //  Palettes         
                    for (int b = 0; b < this.Palettes[c].dds_colors.Count(); b++)
                        dat.binwriter.Write(this.Palettes[c].dds_colors[b]);
            }

                #endregion

            #region File Names
            for (int c = 0; c < this.FileNames.Count; c++)
            {
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] name = enc.GetBytes(this.FileNames[c]);

                dat.binwriter.Write(name);

                if (this.FileNames[c].Length < 16)
                    dat.WriteNulls(dat.GetPad(16, this.FileNames[c].Length));
            }
            #endregion

            #region File Table
            for (int c = 0; c < this.File_Index_Table.Count; c++)
            {
                dat.binwriter.Write(this.File_Index_Table[c].GfxPalettes);
                dat.binwriter.Write(this.File_Index_Table[c].GfxMipmaps);
                dat.binwriter.Write(this.File_Index_Table[c].GfxBitmapOffset);
                dat.binwriter.Write(this.File_Index_Table[c].GfxPaletteOffset);
            }
            #endregion

            #region Extra Info
            if (this.Header.XI_Offset > 0)
            {
                dat.binwriter.Write(this.Xinfo.xilength);                                                       //  XI Info
                dat.binwriter.Write(this.Xinfo.xioffset);
                for (int c = 0; c < this.Xinfo.xibytes.Count; c++)
                    dat.binwriter.Write(this.Xinfo.xibytes[c]);
            }
            #endregion

        }

        #endregion

    }
}
