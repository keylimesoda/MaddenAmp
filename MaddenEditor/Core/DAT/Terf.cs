using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MaddenEditor.Core;



namespace MaddenEditor.Core
{ 
    public class Terf
    {
        //  Madden Dat files are structured as follows (assuming portrait file):
        //  Terf header 16 bytes total, consisting of :
        //  'TERF' 4 Bytes UINT32 = 1179796820
        //  Header length  4 bytes UINT32 = 16
        //  Unknown 4 bytes  UINT32 = 83886594
        //  File Pad 2 bytes UNIT16 = 4
        //  Number of files 2 bytes  UINT16        
        
        //  Directory table consisting of:
        //  DIR1 4 bytes  UINT32 = 827476292
        //  Directory length 4 bytes  UINT32
        //  Followed by each file's starting offset and length 8 bytes total for each file
        //  Each file's offset 4 bytes  UINT32
        //  Each file's length 4 bytes  UINT32 
        
        //  Compression table consisting of:
        //  'COMP' 4 bytes  UINT32 = 1347243843
        //  Compression table length  4 bytes  UINT32
        //  For each file,  Compression level (4 bytes)  UINT32
        //  Uncompressed length (4 bytes)  UINT32
        
        //  Data table and file data consisting of:
        //  'DATA' (4 bytes)  UINT32
        //  Data total length (4 bytes)  UINT32
        //  For each file, all file data.  byte[] based on file size from directory
        //  Each file : standard MMAP type, compressed or non standard (not handled yet)
        
        // Need to define some constants that do not change in the DAT's
        public static UInt32 max4byte = 4294967295;
        public static UInt16 max2byte = 65535;
        // Use headercheck for reading in a value and check it against a constant
        public UInt32 headercheck;

        // Now define the Terf structure
        public UInt32 _terf = 1179796820;
        private UInt32 _terf_offset;
        // This does not change for portraits, but have seen it different for other DATs
        private UInt32 _unknown = 83886594;
        //  File pad is needed to determine the padding on the end of the data files
        //  data file lengths should be a multiple of filepad..?
        private UInt16 _filepad = 0;
        private UInt16 _files;
        private byte[] _terfpad = null;

        public byte[] terfpad
        {
            get { return _terfpad; }
            set { _terfpad = value; }
        }
            

        public UInt32 terf_offset
        {
            get { return _terf_offset; }
            set
            {
                if (value > max4byte)
                    _terf_offset = 16;
                else _terf_offset = value;
            }
        }

        public UInt32 unknown
        {
            get { return _unknown; }
            set
            {
                if (value > max4byte)
                    _unknown = max4byte;
                else _unknown = value;
            }
        }

        public UInt16 filepad
        {
            get { return _filepad; }
            set
            {
                if (value > max2byte)
                    _filepad = max2byte;
                else _filepad = value;
            }
        }

        public UInt16 files
        {
            get { return _files; }
            set
            {
                if (value > max2byte)
                    _files = max2byte;
                else _files = value;
            }
        }

        public Terf()
        {
            // No need to define _terf, it is constant.
            _terf_offset = 16;
            _unknown = 83886594;
            _filepad = 0;
            _files = 0;
            _terfpad = null;
        }
    }

    public class DIR1
    {
        public UInt32 _dirheader = 827476292;
        private UInt32 _dirlength = 0;
        private List<UInt32> _diroffset;
        private List<UInt32> _filelength;

        private byte[] _dir1pad = null;

        public byte[] dir1pad
        {
            get { return _dir1pad; }
            set { _dir1pad = value; }
        }

        
        public UInt32 dirlength
        {
            get { return _dirlength; }
            set
            {
                if (value > Terf.max4byte)
                    _dirlength = Terf.max4byte;
                else _dirlength = value;
            }
        }

        public List<UInt32> diroffset
        {
            get { return _diroffset; }
            set
            { _diroffset = value; }
        }

        public List<UInt32> filelength
        {
            get { return _filelength; }
            set { _filelength = value; }
        }

        public DIR1()
        {
            _dirlength = 0;
            _diroffset = new List<UInt32>();
            _filelength = new List<UInt32>();
        }

       

    }
   
    public class COMP
    {
        public UInt32 _compheader = 1347243843;
        private UInt32 _complength = 0;
        private List<UInt32> _complevel;
        private List<UInt32> _uncomplength;

        public UInt32 complength
        {
            get { return _complength; }
            set { _complength = value; }
        }

   

        public List<UInt32> complvl
        {
            get { return _complevel; }
            set { _complevel = value; }
        }

        public List<UInt32> uncomplength
        {
            get { return _uncomplength; }
            set { _uncomplength = value; }
        }


        public COMP()
        {
            _complength = 0;
            _complevel = new List<UInt32>();
            _uncomplength = new List<UInt32>();
        }

    }

    public class DATA
    {
        // This is the DATA section of the DAT.  These are the actual graphics files.

        public UInt32 Dataheader = 1096040772;
        private UInt32 _datalength;
        private List<MMAP> _DataFiles;

        private byte[] _datapad = null;

        public byte[] datapad
        {
            get { return _datapad; }
            set { _datapad = value; }
        }

        public UInt32 datalength
        {
            get { return _datalength; }
            set
            {
                if (value > Terf.max4byte)
                    _datalength = Terf.max4byte;
                else _datalength = value;
            }
        }

        public List<MMAP> datafiles
        {
            get { return _DataFiles; }
            set { _DataFiles = value; }
        }

        public DATA()
        {
            _datalength = 0;
            _DataFiles = new List<MMAP>();
        }

    }



    public class BitMapTable
    {
        // 16 bytes per entry
        private List<UInt16> _BitMapWidth;
        private List<UInt16> _BitMapHeight;
        private List<UInt32> _BitMapColor;
        private List<UInt32> _BitMapSize;
        private List<UInt32> _BitMapOffset;

        public List<UInt16> BitMapWidth
        {
            get { return _BitMapWidth; }
            set { _BitMapWidth = value; }
        }

        public List<UInt16> BitMapHeight
        {
            get { return _BitMapHeight; }
            set { _BitMapHeight = value; }
        }

        public List<UInt32> BitMapColor
        {
            get { return _BitMapColor; }
            set { _BitMapColor = value; }
        }

        public List<UInt32> BitMapSize
        {
            get { return _BitMapSize; }
            set { _BitMapSize = value; }
        }

        public List<UInt32> BitMapOffset
        {
            get { return _BitMapOffset; }
            set { _BitMapOffset = value; }
        }

        public BitMapTable()
        {
            BitMapWidth = new List<UInt16>();
            BitMapHeight = new List<UInt16>();
            BitMapColor = new List<UInt32>();
            BitMapSize = new List<UInt32>();
            BitMapOffset = new List<UInt32>();
        }

    }

    public class PaletteTable
    {
        // 12 Bytes per entry
        private List<UInt16> _Palette_Unknown1;
        private List<UInt16> _Palette_Unknown2;
        private List<UInt32> _PaletteSize;
        private List<UInt32> _PaletteOffset;

        public List<UInt16> Palette_Unknown1
        {
            get { return _Palette_Unknown1; }
            set { _Palette_Unknown1 = value; }
        }

        public List<UInt16> Palette_Unknown2
        {
            get { return _Palette_Unknown2; }
            set { _Palette_Unknown2 = value; }
        }

        public List<UInt32> PaletteSize
        {
            get { return _PaletteSize; }
            set { _PaletteSize = value; }
        }

        public List<UInt32> PaletteOffset
        {
            get { return _PaletteOffset; }
            set { _PaletteOffset = value; }
        }

        public PaletteTable()
        {
            _Palette_Unknown1 = new List<UInt16>();
            _Palette_Unknown2 = new List<UInt16>();
            _PaletteSize = new List<UInt32>();
            _PaletteOffset = new List<UInt32>();
        }

    }
        
    public class FileTable
    {
        private List<UInt16> _Gfx_Palettes;
        private List<UInt16> _Gfx_Bitmaps;
        private List<UInt32> _Gfx_Bitmap_Offset;
        private List<UInt32> _Gfx_Palette_Offset;

        public List<UInt16> GfxPalettes
        {
            get { return _Gfx_Palettes; }
            set { _Gfx_Palettes = value; }
        }

        public List<UInt16> GfxBitmaps
        {
            get { return _Gfx_Bitmaps; }
            set { _Gfx_Bitmaps = value; }
        }

        public List<UInt32> GfxBitmapOffset
        {
            get { return _Gfx_Bitmap_Offset; }
            set { _Gfx_Bitmap_Offset = value; }
        }

        public List<UInt32> GfxPaletteOffset
        {
            get { return _Gfx_Palette_Offset; }
            set { _Gfx_Palette_Offset = value; }
        }

        public FileTable()
        {
            _Gfx_Palettes = new List<UInt16>();
            _Gfx_Bitmaps = new List<UInt16>();
            _Gfx_Bitmap_Offset = new List<UInt32>();
            _Gfx_Palette_Offset = new List<UInt32>();
        }

    }

    public class Xtra_Info
    {
        private UInt32 _xitableoffset;
        private UInt32 _xilength;
        private UInt32 _xioffset;
        private byte[] _xibytes;
        public UInt32 xitableoffset
        {
            get { return _xitableoffset; }
            set { _xitableoffset = value; }
        }
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
        public byte[] xibytes
        {
            get { return _xibytes; }
            set { _xibytes = value; }
        }
        public Xtra_Info()
        {
            xitableoffset = 0;
            xilength = 0;
            xioffset = 0;
        }
    }        
    public class MMAP
    {
       /*  MMAP Files 40 bytes for header, consisting of :
           4 bytes MMAP
           2 bytes # Elements ?
           2 bytes # file listing?
           4 bytes 00 01 02 03 Unknown
           2 bytes # files
           2 bytes # bitmaps

           2 bytes # palettes
           2 bytes ?
           4 bytes offset for File Table
           4 bytes header length
           4 bytes offset for Palette Table

           4 bytes offset for FileName Table
           4 bytes offset for extra info?
       */

        
        
        private UInt32 _mmap = 1346456909;
        private UInt16 _elements = 02;
        private UInt16 _filelist = 0;
        // location doesn't change so leave it alone for now
        public UInt32 location = 50462976;
        private UInt16 mmap_files;
        private UInt16 mmap_bitmaps;
        // First 16 bytes --------------------------------------------------------
        private UInt16 mmap_palettes;
        private UInt16 mmap_unknown;
        // This marks the beginning of the FileNames
        private UInt32 _FileTable_Offset;
        // This is offset before the start of the file table.
        private UInt32 hdr_length = 0040;
        private UInt32 _palette_table_offset;
        // -----------------------------------------------------------------------
        // This is the offset before the filename list starts, if there is one.
        private UInt32 _FileNames_Offset;
        
        // End of header ---------------------------------------------------------
        // Start of BitMapTable.  Each consists of 16 bytes per bitmap
        private BitMapTable _BM_Table;
                
        private List<byte[]> _Bitmaps;
        
        private PaletteTable _Pal_Table;

        private List<byte[]> _Palettes;

        private List<byte[]> _Filenames;

        private FileTable _File_Table;

        private Xtra_Info _Xinfo;

        // This ends the file, instead of adding a byte array for the padding,
        // we'll set up the reader/writer to handle it.
        // still need to use byte array for null files and compressed files.
        
        private byte[] _mmap_pad = null;

        private byte[] _nullfilepad = null;

        private byte[] _compfile = null;

        private byte[] _comp_pad = null;

        private byte[] _non_std_file = null;
        // ------------------------------------------------------------------------

      
        
        public UInt32 mmap
        {
            get { return _mmap; }
            set { _mmap = value; }
        }

        public UInt16 elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public UInt16 filelist
        {
            get { return _filelist; }
            set { _filelist = value; }
        }

        public UInt16 MMAPFiles
        {
            get { return mmap_files; }
            set { mmap_files = value; }
        }

        public UInt16 MMAPBitmaps
        {
            get { return mmap_bitmaps; }
            set { mmap_bitmaps = value; }
        }

        public UInt16 MMAPPalettes
        {
            get { return mmap_palettes; }
            set { mmap_palettes = value; }
        }

        public UInt16 MMAPUnknown
        {
            get { return mmap_unknown; }
            set { mmap_unknown = value; }
        }

        public UInt32 FileTable_Offset
        {
            get { return _FileTable_Offset; }
            set { _FileTable_Offset = value; }
        }

        public UInt32 HDR_Length
        {
            get { return hdr_length; }
            set { hdr_length = value; }
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
                
        public Xtra_Info Xinfo
        {
            get { return _Xinfo; }
            set { _Xinfo = value; }
        }


        // Adding the new lists for BitMapTable, PaletteTable, FileList, FileTable
        public BitMapTable BM_Table
        {
            get { return _BM_Table; }
            set { _BM_Table = value; }
        }

        public List<byte[]> Bitmaps
        {
            get { return _Bitmaps; }
            set { _Bitmaps = value; }
        }

        public PaletteTable Pal_Table
        {
            get { return _Pal_Table; }
            set { _Pal_Table = value; }
        }
  
        public List<byte[]> Palettes
        {
            get { return _Palettes; }
            set { _Palettes = value; }
        }

        public List<byte[]> FileNames
        {
            get { return _Filenames; }
            set { _Filenames = value; }
        }

        public FileTable File_Table
        {
            get { return _File_Table; }
            set { _File_Table = value; }
        }
        
        public byte[] comp_pad
        {
            get { return _comp_pad; }
            set { _comp_pad = value; }
        }

        public byte[] nullfilepad
        {
            get { return _nullfilepad; }
            set { _nullfilepad = value; }
        }

        public byte[] mmap_pad
        {
            get { return _mmap_pad; }
            set { _mmap_pad = value; }
        }

        public byte[] compfile
        {
            get { return _compfile; }
            set { _compfile = value; }
        }

        public byte[] non_std_file
        {
            get { return _non_std_file; }
            set { _non_std_file = value; }
        }
   
        // Set up a new instance of MMAP, initialize lists for tables and filenames
        public MMAP()
        {            
            BM_Table = new BitMapTable();
            Pal_Table = new PaletteTable();
            File_Table = new FileTable();
            Palettes = new List<byte[]>();
            Bitmaps = new List<byte[]>();
            FileNames = new List<byte[]>();
            Xinfo = new Xtra_Info();
        }

        
 
    }

}



        

    

               

            




    


