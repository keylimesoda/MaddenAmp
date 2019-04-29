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
    public class DATA
    {
        // This is the DATA section of the DAT

        public UInt32 Data_id = 1096040772;
        #region Private Members
        private UInt32 _datalength;
        private List<Filetype> _datafiles;
        #endregion

        public UInt32 datalength
        {
            get { return _datalength; }
            set { _datalength = value; }
        }
        public List<Filetype> DataFiles
        {
            get { return _datafiles; }
            set { _datafiles = value; }
        }

        public bool changed = false;

        public DATA()
        {
            datalength = 0;
            DataFiles = new List<Filetype>();
            changed = false;
        }

        //  to do : fix
        public int GetSize(TERF terf)
        {
            int size = 8 + terf.GetPad(8);

            for (int c = 0; c < this.DataFiles.Count; c++)
            {
                int s = this.DataFiles[c].GetSize() + terf.GetPad(this.DataFiles[c].GetSize());
                size += s;
            }

            return size;
        }

        public void Fix(TERF terf)
        {
            for (int c = 0; c < DataFiles.Count; c++)            
                DataFiles[c].Fix(c);
            this.datalength = (uint)GetSize(terf);
        }

        public void Import(int position, TERF terf, MMAP custom)
        {
            Filetype user = new Filetype(terf, custom);
            if (position == -1)
            {
                this.DataFiles.Add(user);
                terf.Dir1.Import(terf, position, false, user.size);
                terf.Comp.Import(terf, position, false, user.size, 0);
            }

            changed = true;
        }

        public void Read(DAT dat, TERF terf)
        {
            if (this.Data_id != dat.binreader.ReadUInt32())
            {
                dat.binreader.BaseStream.Position -= 4;
                dat.errormsg = "Problem with DIR1 / COMP";
                return;
            }

            this.datalength = dat.binreader.ReadUInt32();
            dat.binreader.BaseStream.Position += (terf.GetPad(8));                                  //  Advance through any needed padding

            #region Read Data files
            this.DataFiles = new List<Filetype>();

            for (int c = 0; c < terf.files; c++)
            {
                bool compressed = false;
                int size = -1;

                size = (int)terf.Dir1.DirTable[c].filelength;                                       //  set size from dir info                
                if (terf.Comp.length > 0)                                                           //  If there is a comp table
                {
                    if (terf.Comp.CompTable[c].file_complevel == 5)                                 //  If this file is compressed
                    {
                        compressed = true;                        
                    }
                }                

                Filetype ft = new Filetype();                                                       //  Set up new instance of data type
                ft.Read(dat, terf, compressed, size);                                               //  Read in this data file

                if (dat.errormsg != "")
                    return;

                DataFiles.Add(ft);                                                                  //  Add to datafiles list                
                if (size == 0)
                    ft.nullfile = true;

                int pad = terf.GetPad(size);
                dat.binreader.BaseStream.Position += pad;                                           //  Skip through padding for this file
            }


            #endregion
        }

        public void Write(DAT dat, TERF terf, ProgressBar datprogress)
        {
            dat.binwriter.Write(this.Data_id);
            dat.binwriter.Write(this.datalength);

            dat.WriteNulls(terf.GetPad(8));

            for (int c = 0; c < terf.files; c++)
            {
                DataFiles[c].Write(dat, terf, c);
                dat.WriteNulls(terf.GetPad((int)terf.Dir1.DirTable[c].filelength));
                datprogress.PerformStep();
            }
        }


    }
}
