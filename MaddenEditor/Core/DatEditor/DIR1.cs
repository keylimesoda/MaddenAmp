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

using MaddenEditor.Core;


namespace MaddenEditor.Core.DatEditor
{
    public class direntry
    {
        #region Private Members
        private UInt32 _fileoffset;
        private UInt32 _filelength;
        #endregion

        public UInt32 datatable_offset
        {
            get { return _fileoffset; }
            set { _fileoffset = value; }
        }
        public UInt32 filelength
        {
            get { return _filelength; }
            set { _filelength = value; }
        }

        public direntry()
        {
            datatable_offset = 0;
            filelength = 0;
        }
        public direntry(int offset, int length)
        {
            datatable_offset = (uint)offset;
            filelength = (uint)length;
        }
    }

    public class DIR1
    {
        public UInt32 dir1_id = 827476292;

        #region Private Members
        private UInt32 _dirlength = 0;
        private List<direntry> _dirtable;
        #endregion

        public UInt32 length
        {
            get { return _dirlength; }
            set { _dirlength = value; }
        }
        public List<direntry> DirTable
        {
            get { return _dirtable; }
            set { _dirtable = value; }
        }

        public bool changed = false;
        public bool needsfixed = false;

        public DIR1()
        {
            length = 0;
            DirTable = new List<direntry>();
        }

        public void Init(DAT dat)
        {
            this.length = 8 + Convert.ToUInt32(dat.ParentTerf.files * 8);
            int pad = dat.GetPad((int)length, (int)dat.ParentTerf.filepad);
            this.length += (uint)pad;
            this.DirTable = new List<direntry>();

            for (int c = 0; c < dat.ParentTerf.files; c++)
            {
                direntry entry = new direntry();
                entry.filelength = 0;
                entry.datatable_offset = 0;

                this.DirTable.Add(entry);
            }
        }

        public int GetSize(TERF terf)
        {
            int size = 8 + (int)(this.DirTable.Count * 8);              //  Entire size is (8 for header + each dir entry *8) and the result is padded out to a multiple of the terf filepad
            int pad = terf.GetPad(size);                                //  Padding is included in the dir1 size
            size += pad;
            return size;
        }

        public void Import(TERF terf, int filenum, bool insert, int newsize)
        {
            if (filenum == -1)                                          //  Append with a new file
                this.DirTable.Add(new direntry(0, newsize));
            else if (insert == true)                                    //  Insert entry at desired position.
                DirTable.Insert(filenum, new direntry(0, newsize));
            else DirTable[filenum].filelength = (uint)newsize;          //  Replace an exisiting file.

            changed = true;                                             //  Set changed flag.
            needsfixed = true;
        }

        public void Fix(TERF terf)                                      // Fix DIR1, specify terf to account for nested terf in data
        {
            this.DirTable = new List<direntry>();
            uint holder = 8 + (uint)terf.GetPad(8);                     // Holder for current offset

            for (int c = 0; c < terf.Data.DataFiles.Count; c++)
            {
                direntry de = new direntry();
                de.datatable_offset = holder;
                de.filelength = (uint)terf.Data.DataFiles[c].GetSize();

                this.DirTable.Add(de);

                holder += (uint)terf.Data.DataFiles[c].GetSize();
                int pad = terf.GetPad(terf.Data.DataFiles[c].GetSize());
                holder += (uint)pad;
            }
            
            this.length = (uint)(this.GetSize(terf));            
            
            needsfixed = false;
        }

        public void Read(DAT dat, TERF terf)
        {
            if (this.dir1_id != dat.binreader.ReadUInt32())
            {
                dat.binreader.BaseStream.Position -= 4;
                dat.errormsg = "Problem with Terf header";
                return;
            }

            this.DirTable = new List<direntry>();
            this.length = dat.binreader.ReadUInt32();

            for (int c = 0; c < dat.ParentTerf.files; c++)
            {
                direntry de = new direntry();
                de.datatable_offset = dat.binreader.ReadUInt32();
                de.filelength = dat.binreader.ReadUInt32();

                this.DirTable.Add(de);
            }

            dat.binreader.BaseStream.Position += terf.GetPad(8 + (this.DirTable.Count * 8));    //  Padding is part of the dir1 size
        }

        public void Write(DAT dat, TERF terf)
        {
            if (needsfixed)
                this.Fix(terf);

            dat.binwriter.Write(this.dir1_id);
            dat.binwriter.Write(this.length);
            foreach (direntry entry in this.DirTable)
            {
                dat.binwriter.Write(entry.datatable_offset);
                dat.binwriter.Write(entry.filelength);
            }

            dat.WriteNulls(terf.GetPad((this.DirTable.Count * 8) + 8));
        }

    }
    
}
