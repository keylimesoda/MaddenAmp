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
    public class ct_entry
    {
        #region Private Members
        private UInt32 _file_complevel;
        private UInt32 _file_uncomplength;
        #endregion

        public UInt32 file_complevel
        {
            get { return _file_complevel; }
            set { _file_complevel = value; }
        }
        public UInt32 file_uncomplength
        {
            get { return _file_uncomplength; }
            set { _file_uncomplength = value; }
        }

        public ct_entry()
        {
            file_complevel = 0;
            file_uncomplength = 0;
        }

        public ct_entry(int complvl, int uncomplength)
        {
            file_complevel = (uint)complvl;
            file_uncomplength = (uint)uncomplength;
        }
    }


    public class COMP
    {
        public UInt32 comp_id = 1347243843;
        #region Private Members
        private UInt32 _complength = 0;
        private List<ct_entry> _comptable;
        #endregion

        public UInt32 length
        {
            get { return _complength; }
            set { _complength = value; }
        }
        public List<ct_entry> CompTable
        {
            get { return _comptable; }
            set { _comptable = value; }
        }

        bool valid = false;
        public bool changed = false;
        public bool needsfixed = false;

        // Constructors
        public COMP()
        {
            length = 0;
            CompTable = new List<ct_entry>();
        }

        public int GetSize(TERF terf)
        {
            if (this.CompTable.Count == 0)
                return 0;
            int size = 8 + (int)this.CompTable.Count * 8;
            int pad = terf.GetPad(size);
            size += pad;
            return size;
        }

        public void Init(TERF terf)
        {
            this.length = 0;
            this.CompTable = new List<ct_entry>();
            for (int c = 0; c < terf.files; c++)
                this.CompTable.Add(new ct_entry(0, 0));
        }

        public void Import(TERF terf, int filenum, bool insert, int newsize, int complvl)
        {
            if (!valid && complvl == 0)                                         //  No need to do anything since no other compressed files and the import is uncompressed
                return;

            if (!valid && complvl == 5 && this.CompTable.Count == 0)            //  We have no loaded comp table and user is importing a compressed file into the dat
                Init(terf);

            if (filenum == -1)                                                  //  Append with a new file
                this.CompTable.Add(new ct_entry(complvl, newsize));
            else if (insert == true)                                            //  Insert entry at desired position.
                this.CompTable.Insert(filenum, new ct_entry(complvl, newsize));
            else                                                                //  Rewrite exisiting file.
                this.CompTable[filenum] = new ct_entry(complvl, newsize);

            changed = true;                                                     //  Set changed flag.
            needsfixed = true;
            valid = true;
        }

        // Fix Comp Table from an import
        public void Fix(TERF terf)
        {
            if (this.CompTable.Count == 0)
                return;
            valid = false;
            this.length = 0;

            //  Check file 0 to see if it was still compressed, if not remove it from the comp list as we know what this file is.
            if (terf.Data.DataFiles[0].compressed_data.Count == 199)
                terf.Data.DataFiles[0].Fix(0);

            if (terf.Data.DataFiles[0].mmap_data.Header.Files > 0)
            {
                this.CompTable[0].file_complevel = 0;
                this.CompTable[0].file_uncomplength = 220;
            }

            // We are going to ignore the comp table unless there is a compressed file
            for (int c = 0; c < terf.Data.DataFiles.Count; c++)
            {
                if (c > this.CompTable.Count)
                {
                    this.CompTable.Add(new ct_entry(0, terf.Data.DataFiles[c].size));           //  We have added a file to a terf that still has compression
                    if (terf.Data.DataFiles[c].compressed_data.Count > 0)                       //  This shouldnt happen, but adding a check to make sure the
                        this.CompTable[c].file_complevel = 5;                                   //  added file isnt compressed.  Change it if it is.
                }

                if (this.CompTable[c].file_complevel == 5)
                    valid = true;
            }

            if (valid)
                this.length = (uint)this.GetSize(terf);

            needsfixed = false;
        }

        public void Read(DAT dat, TERF terf)
        {
            this.CompTable = new List<ct_entry>();

            if (this.comp_id != dat.binreader.ReadUInt32())
            {
                dat.binreader.BaseStream.Position -= 4;
                valid = false;
                return;
            }

            valid = true;
            this.length = dat.binreader.ReadUInt32();

            for (int c = 0; c < dat.ParentTerf.files; c++)
            {
                ct_entry ct = new ct_entry();
                ct.file_complevel = dat.binreader.ReadUInt32();
                ct.file_uncomplength = dat.binreader.ReadUInt32();
                dat.bytecount += 8;

                this.CompTable.Add(ct);
            }
        }

        public void Write(DAT dat, TERF terf)
        {
            if (needsfixed)
                Fix(terf);
            if (!valid)
                return;

            dat.binwriter.Write(this.comp_id);
            dat.binwriter.Write(this.length);

            foreach (ct_entry entry in this.CompTable)
            {
                dat.binwriter.Write(entry.file_complevel);
                dat.binwriter.Write(entry.file_uncomplength);
            }

            dat.WriteNulls(terf.GetPad((int)this.length));
        }

    }
    
}
