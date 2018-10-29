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

namespace MaddenEditor.Core.DatEditor
{
    public class Filetype
    {
        #region Private Members
        private MMAP _mmap_data;
        private List<byte> _compressed;
        private List<byte> _undefined;

        private string _filetype;


        #endregion

        #region Public Members
                
        public MMAP mmap_data
        {
            get { return _mmap_data; }
            set { _mmap_data = value; }
        }       
        public List<byte> compressed_data
        {
            get { return _compressed; }
            set { _compressed = value; }
        }
        public List<byte> undefined_data
        {
            get { return _undefined; }
            set { _undefined = value; }
        }

        #endregion

        public string filetype
        {
            get { return _filetype; }
            set { _filetype = value; }
        }
        public bool nullfile = false;
        public bool changed = false;
        public bool needsfixed = false;
        public int size = 0;

        #region Constructors

        public Filetype()
        {            
            mmap_data = new MMAP();
            compressed_data = new List<byte>();
            undefined_data = new List<byte>();
            filetype = "";
            nullfile = false;
            changed = false;
            size = 0;
        }

        public Filetype(DAT dat, TERF terf)
        {
            mmap_data = new MMAP();
            compressed_data = new List<byte>();
            undefined_data = new List<byte>();
            filetype = "";
            nullfile = false;
            changed = false;
            needsfixed = false;
            size = 0;

            this.Read(dat, terf, false, -1);
        }

        public Filetype(TERF terf, MMAP custom)
        {
            mmap_data = custom;
            compressed_data = new List<byte>();
            undefined_data = new List<byte>();
            filetype = "";
            nullfile = false;
            changed = false;

            size = this.GetSize();
        }

        #endregion

        public void Fix(int count)
        {
            if (this.compressed_data.Count == 199)            
                this.mmap_data.InitDC();
            
            if (this.mmap_data.Header.Files > 0)
            {
                this.nullfile = false;
                this.compressed_data.Clear();
                this.undefined_data.Clear();
                this.mmap_data.FixMMAP(count);
            }

            size = GetSize();
        }

        public int GetSize()
        {
            //  If MMAP is valid, override any others
            if (mmap_data.Header.Files > 0)
                return mmap_data.GetSize();
            
            else if (nullfile == true)
                return 0;

            else if (compressed_data.Count > 0)
                return compressed_data.Count;

            else if (undefined_data.Count > 0)
                return undefined_data.Count;

            else return -1;
        }


        #region File I/O

        public void Read(DAT dat, TERF terf, bool compressed, int dirsize)
        {
            size = dirsize;

            if (dirsize == 0)
            {
                filetype = "NULL";
                nullfile = true;
                return;
            }

            else if (compressed)
            {
                filetype = "COMP";
                for (int c = 0; c < dirsize; c++)
                    this.compressed_data.Add(dat.binreader.ReadByte());
                return;
            }            

            else if (this.mmap_data.Read(dat) == true)
            {
                filetype = "MMAP";
                return;
            }

            else if (dirsize > 0)
            {
                for (int c = 0; c < dirsize; c++)
                    this.undefined_data.Add(dat.binreader.ReadByte());
                filetype = "????";
                dat.errormsg = "Format";
            }
        }

        public void Write(DAT dat, TERF terf, int count)
        {
            if (nullfile)
                return;

            else if (compressed_data.Count > 0)
            {
                foreach (byte b in this.compressed_data)
                    dat.binwriter.Write(b);
                return;
            }

            else if (undefined_data.Count > 0)
            {
                foreach (byte b in this.undefined_data)
                    dat.binwriter.Write(b);
                return;
            }          

            else if (mmap_data.Header.Files > 0)
            {
                mmap_data.Write(dat, count);
                return;
            }

        }

        #endregion



    }

}
