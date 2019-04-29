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
using System.Windows.Forms;

using MaddenEditor.Core;

namespace MaddenEditor.Core.DatEditor
{
    public class TERF
    {   //  'Terf'    54 45 52 46     1179796820
        public UInt32 terf_id = 1179796820;
        #region Private Members
        private UInt32 _terflength;                     //  Length of the header, sometimes has padding, treat it like filepad just for the header.
        private UInt32 _unknown = 83886594;             //  0x020205 This usually does not change, but have seen it different.  0x020203 for fonts is different.
        private UInt16 _filepad = 0;                    //  File pad is needed to determine the padding on the end of the data files, data file lengths should be a multiple of filepad
        private UInt16 _files;

        private DIR1 _Dir1;
        private COMP _Comp;
        private DATA _Data;

        #endregion

        #region Public members
        public UInt32 headerlength
        {
            get { return _terflength; }
            set { _terflength = value; }
        }
        public UInt32 unknown
        {
            get { return _unknown; }
            set { _unknown = value; }
        }
        public UInt16 filepad
        {
            get { return _filepad; }
            set { _filepad = value; }
        }
        public UInt16 files
        {
            get { return _files; }
            set { _files = value; }
        }

        public DIR1 Dir1
        {
            get { return _Dir1; }
            set { _Dir1 = value; }
        }
        public COMP Comp
        {
            get { return _Comp; }
            set { _Comp = value; }
        }
        public DATA Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        #endregion       
        
        public bool changed = false;
        public bool needsfixed = false;

        public TERF()
        {
            headerlength = 0;                                                                       //  actual Header length is 16 bytes, if it is defined as larger
            unknown = 83886594;                                                                     //  it needs to be padded with null bytes
            filepad = 0;
            files = 0;

            Dir1 = new DIR1();
            Comp = new COMP();
            Data = new DATA();

        }

        public int GetPad(int bytecount)
        {
            if (this.filepad == 0)
                return 0;
            if (this.filepad != 0 && bytecount == 0)              //  Null file is padded out to offset multiple anyway
                return this.filepad;

            decimal multiple = Convert.ToDecimal(bytecount) / Convert.ToDecimal(this.filepad);
            int thispad = Convert.ToInt32(System.Math.Ceiling(multiple)) * (Convert.ToInt32(this.filepad)) - bytecount;
            return thispad;
        }
                
        public int GetSize()
        {
            int size = (int)this.headerlength;

            size += this.Dir1.GetSize(this);
            size += this.Comp.GetSize(this);
            size += this.Data.GetSize(this);

            return size;
        }
        
        public void ImportPortrait(int position, DDS customdds)
        {
            this.Data.DataFiles[position].mmap_data.ImportGraphic(customdds);
            this.changed = true;
            if (this.Data.DataFiles[position].size == this.Data.DataFiles[position].GetSize())
                return;
            else
            {
                this.Data.DataFiles[position].size = this.Data.DataFiles[position].GetSize();
                this.needsfixed = true;
            }            
        }

        public void Fix()
        {            
            for (int c = 0; c < this.Data.DataFiles.Count; c++)
                this.Data.DataFiles[c].Fix(c);
            this.files = (UInt16)this.Data.DataFiles.Count;

            this.Dir1.Fix(this);
            this.Comp.Fix(this);
            this.Data.Fix(this);
        }

        public void Expand(int total)
        {
            if (total > this.files)
            {
                for (int c = 0; c < total - this.files; c++ )
                {
                    this.Data.DataFiles.Add(new Filetype());
                }
            }
        }
        
        //  adjust this for progress bar
        public bool Read(DAT dat)
        {
            if (this.terf_id != dat.binreader.ReadUInt32())
            {
                dat.isterf = false;
                return false;
            }

            dat.isterf = true;

            this.headerlength = dat.binreader.ReadUInt32();
            this.unknown = dat.binreader.ReadUInt32();
            this.filepad = dat.binreader.ReadUInt16();
            this.files = dat.binreader.ReadUInt16();

            dat.binreader.BaseStream.Position += (this.headerlength - 16);                                  //  Advance through any terf header padding.            

            this.Dir1.Read(dat, this);                                                                      //  Read Dir Table            

            this.Comp.Read(dat, this);                                                                      //  Read Comp table            

            this.Data.Read(dat, this);                                                                      //  Read Data header

            if (dat.errormsg != "")
                return false;

            return true;
        }

        public void Write(DAT dat, ProgressBar datprogress)
        {
            dat.binwriter.Write(this.terf_id);
            dat.binwriter.Write(this.headerlength);
            dat.binwriter.Write(this.unknown);
            dat.binwriter.Write(this.filepad);
            dat.binwriter.Write(this.files);
            datprogress.Maximum = this.files;

            dat.WriteNulls((int)this.headerlength - 16);

            this.Dir1.Write(dat, this);
            this.Comp.Write(dat, this);
            this.Data.Write(dat, this, datprogress);
        }

        public void WriteFileNumber(DAT dat, int filenum)
        {
            this.Data.DataFiles[filenum].Write(dat, this, filenum);
        }


    }

   
}
