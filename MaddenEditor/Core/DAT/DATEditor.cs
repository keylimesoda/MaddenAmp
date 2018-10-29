using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using MaddenEditor.Core;
using MaddenEditor.Forms;
using MaddenEditor.Db;
//using LumenWorks.Framework.IO.Csv;

namespace MaddenEditor.Core.DAT
{

    public class DATEditor
    {
        public int totalfiles;

        public static string customfilename = "";
        public static string savefile = "";
        public static string ImportDir = "";
        public Terf newterf = new Terf();
        public DIR1 newdir1 = new DIR1();
        public COMP newcomp = new COMP();
        public DATA newdata = new DATA();
        public MMAP newmmap = new MMAP();
        public MMAP customMMAP;
        public Boolean iscomp = false;
        public Boolean datok = false;
        public Boolean customportok = false;
        public Boolean isbump = false;
        public int customheight = 0;
        public int customwidth = 0;
        public int custommips = 0;
        public byte[] customBM = null;
        public byte[] customPAL = null;
        public int custom256 = 1;

        //public ImageInformation customportinfo;
        public BinaryReader binreader;
        public BinaryWriter binwriter;
        public UInt32 HeaderCheck = 0;
        public int current_pal = 0;
        public int graphic = 0;
        public int CurrentBMWidth = 0;
        public int CurrentBMHeight = 0;

        // BUMP header is 54 bytes, fill in the Palette and Bitmap.
        byte[] BUMPHEAD = {66 , 77, 54, 40, 0 ,0 ,0 ,0, 0, 0, 54, 4, 0, 0, 40, 0,
                           0, 0, 96, 0, 0, 0, 96, 0, 0, 0, 1, 0, 8, 0 ,0 ,0,
                           0, 0, 0, 40, 0, 0, 19, 11, 0, 0, 19, 11, 0 ,0 ,0, 1,
                           0, 0, 0, 1, 0, 0};




        public string GetFileName(string ext, string filter)
        {
            string filename = "";
            OpenFileDialog dialog = new OpenFileDialog();
            if (ext != "")
                dialog.DefaultExt = ext;
            // dialong.DefaultExt = "DAT";
            dialog.Filter = filter;
            //dialog.Filter = "Madden DAT File (*.DAT)|*.DAT";
            dialog.FilterIndex = 2;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
                filename = dialog.FileName;

            return filename;
        }

        public byte[] ConvertPalette(byte[] OPal, Boolean isbmp)
        {
            //  Switch RGB to BGR values for BMP and DDS differences.
            //  If needing alpha, turn the first color to transparent
            //  If not needing alpha, remove all alpha values.
            //  Either for displaying from DAT file, or importing into DAT file.
            byte[] Newpal = new byte[OPal.Length];

            for (int c = 0; c < OPal.Length; c += 4)
            {
                // Red or Blue channel
                Newpal[c] = OPal[c + 2];
                // Green channel
                Newpal[c + 1] = OPal[c + 1];
                // Blue or Red Channel
                Newpal[c + 2] = OPal[c];
                //  Alpha channel                
                Newpal[c + 3] = OPal[c + 3];
            }
            //  BMP file, convert to DDS by turning first color to transparent
            //  For importing file into the DAT.   
            if (isbmp == true)
                Newpal[3] = 255;

            //  DDS file from DAT, convert to BMP by turning off alpha other than first color.
            //  If we are importing a DDS into the DAT, no change needed other than to display it.
            else
            {
                Newpal[3] = 0;
                for (int c = 4; c < Newpal.Length; c += 4)
                    //  Alpha channel                
                    Newpal[c + 3] = 0;
            }

            return Newpal;
        }

        public byte[] ConvertBitmap(byte[] Obmap)
        {
            //  BMP Bitmaps read in reverse order.  
            //  For displaying from DAT file or to import custom BMP into DAT file.            
            //  Need to read in each row, starting from end and reorder the bitmap
            byte[] newbitmap = new byte[Obmap.Length];

            int reverse = Obmap.Length - 1;
            for (int row = 0; row < Obmap.Length; row += 96)
            {
                for (int pixel = 0; pixel < 96; pixel++)
                    newbitmap[row + pixel] = Obmap[reverse - row - pixel];
            }

            return newbitmap;
        }

        public Boolean Datreader(string filename)
        {
            PlayerEditControl.DatErrorMsg = "";
            binreader = new BinaryReader(File.Open(filename, FileMode.Open));

            try
            {
                HeaderCheck = binreader.ReadUInt32();
                if (HeaderCheck == newterf._terf)
                {
                    // The actual handling of the DAT starts here.
                    ProcessTerf();
                    PlayerEditControl.DatErrorMsg = ProcessDir();
                    if (PlayerEditControl.DatErrorMsg != "")
                        return false;
                    newterf.headercheck = binreader.ReadUInt32();
                    if (newterf.headercheck == newcomp._compheader)
                    {
                        ProcessComp();
                    }
                    if (newterf.headercheck == newdata.Dataheader)
                    {
                        newdata.datalength = binreader.ReadUInt32();
                        if (newterf.terf_offset > 16)
                            binreader.ReadBytes(GetPad((int)newterf.terf_offset, 8));
                    }

                    PlayerEditControl.DatErrorMsg = ProcessMMAP();
                    if (PlayerEditControl.DatErrorMsg != "")
                        return false;


                }
                if (HeaderCheck == newmmap.mmap)
                {
                    // This is a MMAP data file.  Read in the MMAP data.
                    totalfiles = 1;
                    binreader.BaseStream.Position = 0;
                    PlayerEditControl.DatErrorMsg = ProcessMMAP();
                }

            }

            catch (EndOfStreamException e)
            {
                MessageBox.Show(e.GetType().Name, "Unexpected end of DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                binreader.Close();
            }

            return true;
           
        }

        public void ProcessTerf()
        {
            // Offset for the header info, these need to be multiples of this #
            // ie 16 means 16 bytes for the header, 128 means you need to add padding
            // to achieve this #, since headers are 16 bytes...
            newterf.terf_offset = binreader.ReadUInt32();
            newterf.unknown = binreader.ReadUInt32();
            // Another offset for individual files, need to be multiples of this #

            newterf.filepad = binreader.ReadUInt16();
            newterf.files = binreader.ReadUInt16();

            // Define counter for the total number of files to setup a loop for the
            // reader for the rest of the DAT
            totalfiles = newterf.files;

            if (newterf.terf_offset > 16)
            {
                // Need to read in some padding bytes to make this work.
                // void this, no need to use up xtra memory
                // newterf.terfpad = binreader.ReadBytes(Convert.ToInt32(newterf.terf_offset) - 16);
                binreader.ReadBytes(Convert.ToInt32(newterf.terf_offset) - 16);
            }
        }

        public string ProcessDir()
        {
            if (binreader.ReadUInt32() == newdir1._dirheader)
            {

                newdir1.dirlength = binreader.ReadUInt32();

                // now set-up the loop for the rest of the dir1 info
                int counter = 0;
                while (counter < totalfiles)
                {
                    newdir1.diroffset.Add(binreader.ReadUInt32());
                    newdir1.filelength.Add(binreader.ReadUInt32());
                    counter++;
                }
                // Now add DIR1 padding read (if any)
                if (newterf.terf_offset > 16)
                {
                    binreader.ReadBytes(GetPad((int)newterf.terf_offset, (int)((newterf.files + 1) * 8)));
                }
                return "";
            }
            return "DIR1 error";
        }

        public void ProcessComp()
        {
            newcomp.complength = binreader.ReadUInt32();
            int counter = 0;
            while (counter < totalfiles)
            {
                newcomp.complvl.Add(binreader.ReadUInt32());
                newcomp.uncomplength.Add(binreader.ReadUInt32());
                counter++;
            }
            // If there is a COMP table, add table padding (if any)
            if (newterf.terf_offset > 16)
                binreader.ReadBytes(GetPad((int)newterf.terf_offset, (int)((newterf.files + 1) * 8)));
            newterf.headercheck = binreader.ReadUInt32();

        }

        public string ProcessMMAP()
        {
            int count = 0;
            while (count < totalfiles)
            {
                newterf.headercheck = 0;
                if (newdir1.dirlength > 0)
                {
                    if (newdir1.filelength[count] == 0)
                    {
                        newmmap.nullfilepad = binreader.ReadBytes((int)newterf.terf_offset);
                        ZeroTables(count);
                    }
                }

                if (newcomp.complength > 0)
                {
                    if (newcomp.complvl[count] == 5)
                    {   // This is a compressed file.  
                        // Read into byte array and be done with it.
                        // Compressed files have variable padding bytes that need to be dealt
                        // with.  
                        newmmap.compfile = binreader.ReadBytes((int)newdir1.filelength[count]);
                        ZeroTables(count);
                        //get file padding
                        binreader.ReadBytes(GetPad((int)newterf.filepad, (int)newdir1.filelength[count]));
                    }
                }

                // Ok this isn't a compressed or null file, so process it.
                if (newmmap.compfile == null && newmmap.nullfilepad == null)
                {
                    newterf.headercheck = binreader.ReadUInt32();

                    if (newterf.headercheck == newmmap.mmap)
                    {
                        // ok, this is an MMAP so continue with the reading of the file.
                        // Half of this stuff is constants, so skip over them, perhaps add a check
                        // to be sure they aren't messed up later on
                        newmmap.elements = binreader.ReadUInt16();
                        newmmap.filelist = binreader.ReadUInt16();
                        newmmap.location = binreader.ReadUInt32();
                        newmmap.MMAPFiles = binreader.ReadUInt16();
                        newmmap.MMAPBitmaps = binreader.ReadUInt16();

                        newmmap.MMAPPalettes = binreader.ReadUInt16();
                        newmmap.MMAPUnknown = binreader.ReadUInt16();
                        newmmap.FileTable_Offset = binreader.ReadUInt32();
                        newmmap.HDR_Length = binreader.ReadUInt32();
                        newmmap.PaletteTable_Offset = binreader.ReadUInt32();

                        newmmap.FileNames_Offset = binreader.ReadUInt32();
                        newmmap.Xinfo.xitableoffset = binreader.ReadUInt32();
                        // MMAP header is now read in.

                        // Set up a loop to read in the tables
                        int c = 0;

                        while (c < newmmap.MMAPBitmaps)
                        {
                            newmmap.BM_Table.BitMapWidth.Add(binreader.ReadUInt16());
                            newmmap.BM_Table.BitMapHeight.Add(binreader.ReadUInt16());
                            newmmap.BM_Table.BitMapColor.Add(binreader.ReadUInt32());
                            newmmap.BM_Table.BitMapSize.Add(binreader.ReadUInt32());
                            newmmap.BM_Table.BitMapOffset.Add(binreader.ReadUInt32());
                            c++;

                        }
                        // BitMap Table complete
                        // Read Padding bytes for BitMap table.  Multiple of 32 bytes, plus header
                        // of 40 bytes.  These are null bytes so just discard them.                        

                        binreader.ReadBytes(GetPad(32, (int)(newmmap.MMAPBitmaps * 16 + 40)));

                        //int readpad = (int) ((newmmap.BM_Table.BitMapOffset[0] - (newmmap.MMAPBitmaps*16 + 40)));
                        //binreader.ReadBytes(readpad);

                        // Load in BitMaps to the list of byte arrays
                        c = 0;
                        while (c < newmmap.MMAPBitmaps)
                        {
                            newmmap.Bitmaps.Add(binreader.ReadBytes(Convert.ToInt32(newmmap.BM_Table.BitMapSize[c])));
                            c++;
                        }
                        // BitMaps are all read in now, continue to palette table

                        c = 0;
                        while (c < newmmap.MMAPPalettes)
                        {
                            newmmap.Pal_Table.Palette_Unknown1.Add(binreader.ReadUInt16());
                            newmmap.Pal_Table.Palette_Unknown2.Add(binreader.ReadUInt16());
                            newmmap.Pal_Table.PaletteSize.Add(binreader.ReadUInt32());
                            newmmap.Pal_Table.PaletteOffset.Add(binreader.ReadUInt32());
                            c++;
                        }
                        // Read in Palette Padding bytes.  Palette Table 12 bytes per entry.
                        // Multiple of 32 bytes again.                        
                        binreader.ReadBytes(GetPad(32, (int)(newmmap.MMAPPalettes * 12)));
                        // Now read in each set of palettes

                        c = 0;
                        while (c < newmmap.MMAPPalettes)
                        {
                            newmmap.Palettes.Add(binreader.ReadBytes((int)newmmap.Pal_Table.PaletteSize[c]));
                            c++;
                        }
                        // Palettes are all done.                        



                        // FileNames Listing exists, read 16 bytes per entry
                        for (int s = 0; s < newmmap.MMAPFiles; s++)
                        {
                            newmmap.FileNames.Add(binreader.ReadBytes(16));
                        }

                        // Filename list is now done.  Now read in the filetable

                        c = 0;
                        while (c < newmmap.MMAPFiles)
                        {
                            newmmap.File_Table.GfxPalettes.Add(binreader.ReadUInt16());
                            newmmap.File_Table.GfxBitmaps.Add(binreader.ReadUInt16());
                            newmmap.File_Table.GfxBitmapOffset.Add(binreader.ReadUInt32());
                            newmmap.File_Table.GfxPaletteOffset.Add(binreader.ReadUInt32());
                            c++;
                        }

                        // File Table is now read in

                        // Need to check for xtra_info as in the face files info                        
                        if ((int)newmmap.Xinfo.xitableoffset > 0)
                        {
                            newmmap.Xinfo.xilength = binreader.ReadUInt32();
                            newmmap.Xinfo.xioffset = binreader.ReadUInt32();
                            newmmap.Xinfo.xibytes = binreader.ReadBytes((int)newmmap.Xinfo.xilength);
                        }

                        // Don't forget to check for file padding.
                        int thislength = 0;
                        thislength += 40 + ((int)newmmap.MMAPBitmaps * 16);
                        thislength += GetPad(32, (int)(newmmap.MMAPBitmaps * 16) + 40);
                        c = 0;
                        while (c < newmmap.MMAPBitmaps)
                        {
                            thislength += newmmap.Bitmaps[c].Length;
                            c++;
                        }
                        thislength += (int)newmmap.MMAPPalettes * 12;
                        thislength += GetPad(32, (int)(newmmap.MMAPPalettes * 12));
                        c = 0;
                        while (c < newmmap.MMAPPalettes)
                        {
                            thislength += newmmap.Palettes[c].Length;
                            c++;
                        }
                        // Add filenames and filetable bytes
                        thislength += (int)(newmmap.MMAPFiles * 16) + (int)(newmmap.MMAPFiles * 12);
                        if ((int)newmmap.Xinfo.xitableoffset > 0)
                            thislength += 8 + newmmap.Xinfo.xibytes.Length;
                        // Finally get any padding from this result                        
                        if (newterf.filepad > 0)
                            binreader.ReadBytes(GetPad((int)newterf.filepad, thislength));
                    }
                    else
                    {
                        // Ok this isn't a compressed file and it isn't an MMAP file                        
                        // Need to fix this to load in PLADATA.DAT files etc...
                        // read it into a non standard byte[] and be done
                        // Need to back up 4 bytes because we read in a header check that
                        // wasn't a valid header

                        // TO DO : FIX THIS !!!

                        long current = binreader.BaseStream.Position;
                        binreader.BaseStream.Position = (current - 4);
                        newmmap.non_std_file = binreader.ReadBytes((int)newdir1.filelength[count]);
                        ZeroTables(count);


                        // Now read file pad
                        binreader.ReadBytes(GetPad((int)newterf.filepad, (int)newdir1.filelength[count]));

                    }
                }


                // Done reading in data for this MMAP, now add it to the
                // collection and go to the next one.

                newdata.datafiles.Add(newmmap);
                newmmap = new MMAP();
                count++;

            }
            return "";
        }

        public int GetPad(int offset, int bytecount)
        {
            if (offset == 0)
                return 0;
            decimal multiple = Convert.ToDecimal(bytecount) / Convert.ToDecimal(offset);
            int thispad = Convert.ToInt32(System.Math.Ceiling(multiple)) * (Convert.ToInt32(offset)) - bytecount;
            return thispad;
        }

        public void ZeroTables(int number)
        {
            // Set tables to zero for non MMAP files
            newmmap.BM_Table.BitMapColor.Add(0);
            newmmap.BM_Table.BitMapHeight.Add(0);
            newmmap.BM_Table.BitMapOffset.Add(0);
            newmmap.BM_Table.BitMapSize.Add(0);
            newmmap.BM_Table.BitMapWidth.Add(0);
            newmmap.File_Table.GfxBitmapOffset.Add(0);
            newmmap.File_Table.GfxBitmaps.Add(0);
            newmmap.File_Table.GfxPaletteOffset.Add(0);
            newmmap.File_Table.GfxPalettes.Add(0);

        }

        public MMAP InitPortrait()
        {
            // Initialize a new MMAP based on Portrait settings
            MMAP customMMAP = new MMAP();
            customMMAP.MMAPFiles = 1;
            customMMAP.MMAPBitmaps = 1;
            customMMAP.MMAPPalettes = 1;
            customMMAP.MMAPUnknown = 0;
            customMMAP.FileTable_Offset = 10352;
            customMMAP.PaletteTable_Offset = 9280;

            customMMAP.FileNames_Offset = 10336;
            customMMAP.Xinfo.xitableoffset = 0;
            customMMAP.BM_Table.BitMapWidth.Add(96);
            customMMAP.BM_Table.BitMapHeight.Add(96);
            customMMAP.BM_Table.BitMapColor.Add(9);

            customMMAP.BM_Table.BitMapOffset.Add(0);
            customMMAP.BM_Table.BitMapSize.Add(9216);
            customMMAP.HDR_Length = 40;

            customMMAP.Pal_Table.Palette_Unknown1.Add(1);
            customMMAP.Pal_Table.Palette_Unknown2.Add(34);
            customMMAP.Pal_Table.PaletteSize.Add(1024);
            customMMAP.Pal_Table.PaletteOffset.Add(9312);
            byte[] name = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            customMMAP.FileNames.Add(name);
            customMMAP.File_Table.GfxPalettes.Add(1);
            customMMAP.File_Table.GfxBitmaps.Add(1);
            customMMAP.File_Table.GfxBitmapOffset.Add(0);
            customMMAP.File_Table.GfxPaletteOffset.Add(0);

            byte[] bm = new byte[9216];
            byte[] pal = new byte[1024];
            customMMAP.Bitmaps.Add(bm);
            customMMAP.Palettes.Add(pal);

            return customMMAP;
        }

        public Stream ProcessGraphic(int portraitid)
        {
            //  Adjust file# to portrait id.  There is a mask that is actually file 0
            //  Before blank pic that is 0 in game.
            portraitid += 1;
            Stream portstream = new MemoryStream();
            byte[] portpal = new byte[1024];
            byte[] portbm = new byte[9216];
            portpal = ConvertPalette(newdata.datafiles[portraitid].Palettes[0], false);
            portbm = ConvertBitmap(newdata.datafiles[portraitid].Bitmaps[0]);

            for (int i = 0; i < 54; i++)
                portstream.WriteByte(BUMPHEAD[i]);
            for (int i = 0; i < 1024; i++)
                portstream.WriteByte(portpal[i]);
            for (int i = 0; i < 9216; i++)
                portstream.WriteByte(portbm[i]);

            return portstream;
        }

        public int GetFileSize(int portnum)
        {
            portnum += 1;
            int c = 0;
            // Return size of file currently selected
            if (newdata.datafiles[portnum].nullfilepad != null)
                return newterf.filepad;
            if (newdata.datafiles[portnum].compfile != null)
                return newdata.datafiles[portnum].compfile.Length;
            if (newdata.datafiles[portnum].non_std_file != null)
                return newdata.datafiles[portnum].non_std_file.Length;
            else
            {
                int thislength = 0;
                thislength += 40 + ((int)newdata.datafiles[portnum].MMAPBitmaps * 16);
                thislength += GetPad(32, (int)(newdata.datafiles[portnum].MMAPBitmaps * 16) + 40);
                c = 0;
                while (c < newdata.datafiles[portnum].MMAPBitmaps)
                {
                    thislength += newdata.datafiles[portnum].Bitmaps[c].Length;
                    c++;
                }
                thislength += (int)newdata.datafiles[portnum].MMAPPalettes * 12;
                thislength += GetPad(32, (int)(newdata.datafiles[portnum].MMAPPalettes * 12));
                c = 0;
                while (c < newdata.datafiles[portnum].MMAPPalettes)
                {
                    thislength += newdata.datafiles[portnum].Palettes[c].Length;
                    c++;
                }
                // Add filenames and filetable bytes
                thislength += (int)(newdata.datafiles[portnum].MMAPFiles * 16) + (int)(newdata.datafiles[portnum].MMAPFiles * 12);
                if ((int)newdata.datafiles[portnum].Xinfo.xitableoffset > 0)
                    thislength += 8 + newdata.datafiles[portnum].Xinfo.xibytes.Length;
                return thislength;
            }

        }

        public void FixDAT(int portnum, int newsize)
        {
            portnum += 1;
            // Fix DAT from import
            // Delete old filesize from the DAT and add the new one.
            newdata.datalength -= newdir1.filelength[portnum];
            // Delete any file padding
            newdata.datalength -= Convert.ToUInt32(GetPad((int)newterf.filepad, (int)newdir1.filelength[portnum]));
            // Add new file size
            newdata.datalength += Convert.ToUInt32(newsize);
            // Add any file padding
            newdata.datalength += Convert.ToUInt32(GetPad((int)newterf.filepad, (int)newsize));
            // Change directory file size to new one
            newdir1.filelength[portnum] = Convert.ToUInt32(newsize);

            // Fix DIR1.
            // directory length stays the same.


            for (int c = portnum; c < (totalfiles - 1); c++)
            {
                newdir1.diroffset[c + 1] = newdir1.diroffset[c] + newdir1.filelength[c] + Convert.ToUInt32(GetPad((int)newterf.filepad, (int)newdir1.filelength[c]));
                // If filelength is 0, it is a null file and must be dealt with.
                if (newdir1.filelength[c] == 0)
                    newdir1.filelength[c + 1] += Convert.ToUInt32(newterf.filepad);
            }
        }

        public void FixCompTable(int portnum, int newsize)
        {
            // Fix COMP table after inserting an uncompressed file.
            // TO DO : Check and see if there are any compressed files, if not
            // just delete the comp table altogether.
            portnum += 1;
            newcomp.complvl[portnum] = 0;
            newcomp.uncomplength[portnum] = Convert.ToUInt32(newsize);
            newdata.datafiles[portnum].comp_pad = null;
            bool deletecomp = true;
            foreach (UInt32 compr in newcomp.complvl)
            {
                if (compr == 5)
                    deletecomp = false;
            }
            if (deletecomp == true)
            {
                newcomp.complength = 0;
                newcomp.complvl.Clear();
                newcomp.uncomplength.Clear();
            }
        }

        public Stream ProcessCustom(string customfilename)
        {
            // Going to set up a memory stream for graphic
            byte[] custombm = new byte[9216];
            byte[] custompal = new byte[1024];
            Stream customstream = new MemoryStream();

            binreader = new BinaryReader(File.Open(customfilename, FileMode.Open));
            Int16 checkhda = binreader.ReadInt16();
            Int16 checkhdb = binreader.ReadInt16();
            //  BMP file
            if (checkhda == 19778)
            {
                binreader.BaseStream.Position = 54;
                for (int i = 0; i < 1024; i++)
                    custompal[i] = binreader.ReadByte();
                for (int i = 0; i < 9216; i++)
                    custombm[i] = binreader.ReadByte();
            }
            //  DDS file
            if (checkhda == 17476 && checkhdb == 8275)
            {
                byte[] DDSbm = new byte[9216];
                byte[] DDSpal = new byte[1024];
                //  DDS header is 128 bytes
                binreader.BaseStream.Position = 128;
                for (int i = 0; i < 9216; i++)
                    DDSbm[i] = binreader.ReadByte();
                for (int i = 0; i < 1024; i++)
                    DDSpal[i] = binreader.ReadByte();

                custompal = ConvertPalette(DDSpal, false);
                custombm = ConvertBitmap(DDSbm);
            }

            else
            {
                binreader.Close();
                return customstream;
            }
            //  Setup BUMP file...header first
            for (int i = 0; i < 54; i++)
                customstream.WriteByte(BUMPHEAD[i]);
            for (int i = 0; i < 1024; i++)
                customstream.WriteByte(custompal[i]);
            for (int i = 0; i < 9216; i++)
                customstream.WriteByte(custombm[i]);

            binreader.Close();
            return customstream;
        }

        public MMAP ProcessCustom(string customfilename, bool mmap)
        {
            // Going to set up a memory stream for graphic
            byte[] custombm = new byte[9216];
            byte[] custompal = new byte[1024];
            MMAP customMMAP = InitPortrait();


            binreader = new BinaryReader(File.Open(customfilename, FileMode.Open));
            Int16 checkhda = binreader.ReadInt16();
            Int16 checkhdb = binreader.ReadInt16();
            //  BMP file... 
            if (checkhda == 19778)
            {
                //  Need temporary holder for bitmap and palette since they need processed.
                byte[] BMPbm = new byte[9216];
                byte[] BMPpal = new byte[1024];
                binreader.BaseStream.Position = 54;
                for (int i = 0; i < 1024; i++)
                    BMPpal[i] = binreader.ReadByte();
                for (int i = 0; i < 9216; i++)
                    BMPbm[i] = binreader.ReadByte();
                // Process the bmp file since this is going into the DAT before it is displayed
                custompal = ConvertPalette(BMPpal, true);
                custombm = ConvertBitmap(BMPbm);
            }
            //  DDS file
            if (checkhda == 17476 && checkhdb == 8275)
            {

                //  DDS header is 128 bytes
                binreader.BaseStream.Position = 128;
                for (int i = 0; i < 9216; i++)
                    custombm[i] = binreader.ReadByte();
                for (int i = 0; i < 1024; i++)
                    custompal[i] = binreader.ReadByte();
            }

            if (checkhda != 19778 && checkhda != 17476 && checkhdb != 8275)
            {
                binreader.Close();
                return null;
            }
            customMMAP.Bitmaps[0] = custombm;
            customMMAP.Palettes[0] = custompal;

            binreader.Close();
            return customMMAP;
        }

        public void ImportCustom(string customfilename, int portnum)
        {
            portnum += 1;
            bool fixcomp = false;
            custom256 = 1;

            // Compressed Files


            if (newcomp.complength > 0)
            {
                if (newcomp.complvl[portnum] == 5)
                {
                    // Need to delete the compfile.  Fix comp table.
                    // fix dir1 table
                    customMMAP = new MMAP();
                    customMMAP = InitPortrait();
                    //  Delete old file
                    newdata.datafiles.RemoveAt(portnum);
                    //  Insert new uncompresed MMAP file
                    newdata.datafiles.Insert(portnum, customMMAP);
                    fixcomp = true;
                }
            }



            // Import into current uncompressed.
            MMAP newportmmap = ProcessCustom(customfilename, true);
            newdata.datafiles[portnum] = newportmmap;

            int newsize = GetFileSize(portnum);
            if (fixcomp)
                FixCompTable(portnum, newsize);
            if (newdata.datalength > 0)
                FixDAT(portnum, newsize);

        }

        public string SaveFileName(string ext, string filter)
        {
            savefile = "";
            SaveFileDialog savedialog = new SaveFileDialog();
            if (ext != "")
                savedialog.DefaultExt = ext;
            savedialog.Filter = filter;
            savedialog.FilterIndex = 1;
            savedialog.AddExtension = true;
            savedialog.Title = "Save File";
            if (savedialog.ShowDialog() == DialogResult.OK)
                savefile = savedialog.FileName;
            return savefile;
        }

        public void SaveDAT()
        {
            // Save DAT, pre check this to be sure this is a DAT and not a "file"           
            string saveDATname = SaveFileName("DAT", "Madden DAT (*.DAT)|*.DAT");
            // OK, now it's time to set up the main loop to save all the DAT info back
            // into correct format so Madden can read it.
            binwriter = new BinaryWriter(File.Open(saveDATname, FileMode.Create));
            // Terf Header = 16 bytes, each write is 4 bytes unless we convert it first
            if (newdata.datafiles.Count > 1)
            {
                binwriter.Write(newterf._terf);
                binwriter.Write(newterf.terf_offset);
                binwriter.Write(newterf.unknown);
                binwriter.Write(newterf.filepad);
                binwriter.Write(Convert.ToInt16(totalfiles));
                WriteNulls((int)newterf.terf_offset - 16);
            }

            // DIR write
            if (newdir1.dirlength > 0)
                WriteDir();

            // COMP write
            if (newcomp.complength > 0)
                WriteComp();

            WriteData();

            // close file
            binwriter.Close();
        }

        public void WriteData()
        {
            if (newdata.datafiles.Count > 1)
            {
                binwriter.Write(newdata.Dataheader);
                binwriter.Write(newdata.datalength);
                if (newterf.terf_offset > 16)
                    WriteNulls(GetPad((int)newterf.terf_offset, 8));
            }

            // Main Loop
            for (int t = 0; t < totalfiles; t++)
            {
                // check for nullfile, compressed file, non/std file
                if (newdata.datafiles[t].nullfilepad != null)
                    WriteNulls(newterf.filepad);
                if (newdata.datafiles[t].compfile != null)
                {
                    binwriter.Write(newdata.datafiles[t].compfile);
                    WriteNulls(GetPad((int)newterf.filepad, newdata.datafiles[t].compfile.Length));
                }
                if (newdata.datafiles[t].non_std_file != null)
                {
                    binwriter.Write(newdata.datafiles[t].non_std_file);
                    WriteNulls(GetPad((int)newterf.filepad, newdata.datafiles[t].non_std_file.Length));
                }
                // This should now be a normal file
                if (newdata.datafiles[t].non_std_file == null && newdata.datafiles[t].compfile == null && newdata.datafiles[t].nullfilepad == null)
                {
                    binwriter.Write(newdata.datafiles[t].mmap);
                    binwriter.Write(newdata.datafiles[t].elements);
                    binwriter.Write(newdata.datafiles[t].filelist);
                    binwriter.Write(newdata.datafiles[t].location);
                    binwriter.Write(newdata.datafiles[t].MMAPFiles);
                    binwriter.Write(newdata.datafiles[t].MMAPBitmaps);

                    binwriter.Write(newdata.datafiles[t].MMAPPalettes);
                    binwriter.Write(newdata.datafiles[t].MMAPUnknown);
                    binwriter.Write(newdata.datafiles[t].FileTable_Offset);
                    binwriter.Write(newdata.datafiles[t].HDR_Length);
                    binwriter.Write(newdata.datafiles[t].PaletteTable_Offset);

                    binwriter.Write(newdata.datafiles[t].FileNames_Offset);
                    binwriter.Write(newdata.datafiles[t].Xinfo.xitableoffset);
                    // Header now complete

                    // BitMap Tables
                    for (int c = 0; c < newdata.datafiles[t].MMAPBitmaps; c++)
                    {
                        binwriter.Write(newdata.datafiles[t].BM_Table.BitMapWidth[c]);
                        binwriter.Write(newdata.datafiles[t].BM_Table.BitMapHeight[c]);
                        binwriter.Write(newdata.datafiles[t].BM_Table.BitMapColor[c]);
                        binwriter.Write(newdata.datafiles[t].BM_Table.BitMapSize[c]);
                        binwriter.Write(newdata.datafiles[t].BM_Table.BitMapOffset[c]);
                    }
                    WriteNulls(GetPad(32, (int)(newdata.datafiles[t].MMAPBitmaps * 16 + 40)));

                    // BitMaps
                    for (int c = 0; c < newdata.datafiles[t].MMAPBitmaps; c++)
                        binwriter.Write(newdata.datafiles[t].Bitmaps[c]);

                    // Palette Tables
                    for (int c = 0; c < newdata.datafiles[t].MMAPPalettes; c++)
                    {
                        binwriter.Write(newdata.datafiles[t].Pal_Table.Palette_Unknown1[c]);
                        binwriter.Write(newdata.datafiles[t].Pal_Table.Palette_Unknown2[c]);
                        binwriter.Write(newdata.datafiles[t].Pal_Table.PaletteSize[c]);
                        binwriter.Write(newdata.datafiles[t].Pal_Table.PaletteOffset[c]);
                    }
                    WriteNulls(GetPad(32, (int)(newdata.datafiles[t].MMAPPalettes * 12)));

                    // Palettes
                    for (int c = 0; c < newdata.datafiles[t].MMAPPalettes; c++)
                        binwriter.Write(newdata.datafiles[t].Palettes[c]);

                    // Filenames List
                    for (int c = 0; c < newdata.datafiles[t].MMAPFiles; c++)
                        binwriter.Write(newdata.datafiles[t].FileNames[c]);

                    // FileTable
                    for (int c = 0; c < newdata.datafiles[t].MMAPFiles; c++)
                    {
                        binwriter.Write(newdata.datafiles[t].File_Table.GfxPalettes[c]);
                        binwriter.Write(newdata.datafiles[t].File_Table.GfxBitmaps[c]);
                        binwriter.Write(newdata.datafiles[t].File_Table.GfxBitmapOffset[c]);
                        binwriter.Write(newdata.datafiles[t].File_Table.GfxPaletteOffset[c]);
                    }

                    // Extra Info, if exists
                    if ((int)newdata.datafiles[t].Xinfo.xitableoffset > 0)
                    {
                        binwriter.Write(newdata.datafiles[t].Xinfo.xilength);
                        binwriter.Write(newdata.datafiles[t].Xinfo.xioffset);
                        binwriter.Write(newdata.datafiles[t].Xinfo.xibytes);
                    }

                    // FilePadding
                    int thislength = 0;
                    thislength += 40 + ((int)newdata.datafiles[t].MMAPBitmaps * 16);
                    thislength += GetPad(32, (int)(newdata.datafiles[t].MMAPBitmaps * 16) + 40);
                    for (int c = 0; c < newdata.datafiles[t].MMAPBitmaps; c++)
                        thislength += newdata.datafiles[t].Bitmaps[c].Length;
                    thislength += (int)newdata.datafiles[t].MMAPPalettes * 12;
                    thislength += GetPad(32, (int)(newdata.datafiles[t].MMAPPalettes * 12));
                    for (int c = 0; c < newdata.datafiles[t].MMAPPalettes; c++)
                        thislength += newdata.datafiles[t].Palettes[c].Length;
                    thislength += (int)(newdata.datafiles[t].MMAPFiles * 16) + (int)(newdata.datafiles[t].MMAPFiles * 12);
                    if ((int)newdata.datafiles[t].Xinfo.xitableoffset > 0)
                        thislength += 8 + newdata.datafiles[t].Xinfo.xibytes.Length;
                    // Write Padding
                    WriteNulls(GetPad((int)newterf.filepad, thislength));
                }
            }
        }

        public void WriteComp()
        {
            binwriter.Write(newcomp._compheader);
            binwriter.Write(newcomp.complength);
            for (int t = 0; t < totalfiles; t++)
            {
                binwriter.Write(newcomp.complvl[t]);
                binwriter.Write(newcomp.uncomplength[t]);
            }
            int total = totalfiles * 8 + 8;
            WriteNulls(GetPad((int)newterf.filepad, total));
        }

        public void WriteDir()
        {
            binwriter.Write(newdir1._dirheader);
            binwriter.Write(newdir1.dirlength);
            for (int t = 0; t < totalfiles; t++)
            {
                binwriter.Write(newdir1.diroffset[t]);
                binwriter.Write(newdir1.filelength[t]);
            }
            int total = totalfiles * 8 + 8;

            // TO DO : Fix this, not right.  Might work off of filepad
            WriteNulls(GetPad((int)newterf.filepad, total));
        }

        public void WriteNulls(int nulls)
        {
            for (int t = 0; t < nulls; t++)
            {
                binwriter.Write(Convert.ToByte(0));
            }
        }


        public void ExportGraphic(string exportname, int portnum)
        {            
            // Export the current graphic to out stream
            portnum++;
            Stream outstream = new MemoryStream();

            outstream = ProcessGraphic(portnum);
            outstream.Position = 0;
            
            if (savefile != "")
            {
                BinaryWriter binwriter = new BinaryWriter(File.Open(savefile, FileMode.Create));

                for (int t = 0; t < outstream.Length; t++)
                    binwriter.Write((byte)outstream.ReadByte());
                binwriter.Close();
            }
        }

        private void ExportAll()
        {
            savefile = SaveFileName("BMP", "Graphics File (*.BMP)|*.BMP");
            int portnum = 0; 
            // Export only the graphics files in the DAT
            for (portnum = 0; portnum < totalfiles; portnum++)
            {
                if (newdata.datafiles[portnum].nullfilepad == null && newdata.datafiles[portnum].non_std_file == null && newdata.datafiles[portnum].compfile == null)
                {
                    if (newdata.datafiles[portnum].Bitmaps[0].Length == 9216)
                    {
                        savefile += portnum.ToString("0000") + ".BMP";
                        ExportGraphic(savefile, portnum);
                    }

                }
            }
        }
    }
}
            
        

        

    
    

    