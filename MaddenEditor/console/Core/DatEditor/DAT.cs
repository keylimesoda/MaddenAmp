/*
    Madden Resource Editor
    Copyright (C) 2014  Steve Gindlesperger

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
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using MaddenEditor.Core;

namespace MaddenEditor.Core.DatEditor
{
    public class DAT
    {
        //  Madden Dat files are structured as follows :
        //  Terf header 16 bytes.
        //  TERF (UINT32)  Header length (UINT32)  Unknown (UINT32)? File pad (UINT16)  # of files (UINT16)
        //  Padding if needed
        //
        //  DIR1 (UINT32)  Directory table length (UINT32)  table length includes any needed padding
        //  For each file, offset from DATA (UINT32)  File length (UINT32)
        //  entire directory structure, is padded with null bytes if necessary equal to multiple of terf filepad
        //        
        //  COMP (UINT32)  Compression table length (UINT32) table length includes any needed padding
        //  For each file,  Compression level (UINT32)  Uncompressed length (UINT32)
        //  entire compression structure is padded equal to multiple of terf filepad
        //        
        //  DATA (UINT32)  Data table length (UINT32)  table length includes any needed padding
        //  Header is padded equal to filepad from terf
        //  For each file, all file data.  Each file length is padded equal to multiple of filepad        

        
        #region Private Members
        private TERF _ParentTerf;
        private CustomBitmap _grfx;
        #endregion

        #region Public Members
        public TERF ParentTerf
        {
            get { return _ParentTerf; }
            set { _ParentTerf = value; }
        }
        public CustomBitmap grfx
        {
            get { return _grfx; }
            set { _grfx = value; }
        }
        public int bytecount = 0;
        public int totalbytes = 0;

        public string loadfile = "";
        public string savefile = "";
        public BinaryReader binreader;
        public BinaryWriter binwriter;
        public bool changed = false;
        public bool isterf = false;
        public string errormsg = "";
        public bool warning = false;

        #endregion

        public DAT()
        {
            ParentTerf = new TERF();
            grfx = new CustomBitmap();
            bytecount = 0;
            isterf = false;
        }

        public int GetPad(int offset, int bytecount)
        {
            if (offset == 0)
                return 0;
            if (offset != 0 && bytecount == 0)              //  Null file is padded out to offset multiple anyway
                return offset;

            decimal multiple = Convert.ToDecimal(bytecount) / Convert.ToDecimal(offset);
            int thispad = Convert.ToInt32(System.Math.Ceiling(multiple)) * (Convert.ToInt32(offset)) - bytecount;
            return thispad;
        }

        public void Check()
        {
            for (int c = 0; c < this.ParentTerf.files; c++)
            {
                if (this.ParentTerf.Data.DataFiles[c].changed)
                    this.changed = true;
            }
        }

        public void Fix()
        {
            this.ParentTerf.Fix();            
        }



        #region I/O

        public void WriteNulls(int nulls)
        {
            for (int t = 0; t < nulls; t++)
            {
                binwriter.Write(Convert.ToByte(0));
            }
        }

        public void LoadFileName()
        {            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Title = "Load Madden DAT";
            dialog.InitialDirectory = @"%USERPROFILE%\My Documents\";            
            dialog.Filter = "Madden DAT File (*.DAT)|*.DAT|All Files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
                this.loadfile = dialog.FileName;
            else this.loadfile = "";
        }

        public void SaveFileName()
        {
            this.savefile = "";
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.DefaultExt = "DAT";
            savedialog.Filter = "Madden DAT File (*.DAT)|*.DAT";
            savedialog.FilterIndex = 1;
            savedialog.AddExtension = true;
            savedialog.Title = "Save Madden DAT";

            if (savedialog.ShowDialog() == DialogResult.OK)
                this.savefile = savedialog.FileName;
        }

        public void Load()
        {   
            if (loadfile == "")
                return;

            this.ParentTerf = new TERF();
            bytecount = 0;
            binreader = new BinaryReader(File.Open(loadfile, FileMode.Open));

            try
            {
                bool done = this.ParentTerf.Read(this);
                this.isterf = done;
                if (!done)
                {
                    this.ParentTerf = new TERF();
                    this.isterf = false;
                }
                if (this.errormsg != "")
                {
                    MessageBox.Show("DAT Portrait Read Error, probably due to custom editing", "DAT Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            catch (EndOfStreamException e)
            {
                MessageBox.Show(e.GetType().Name, "Unexpected end of DATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isterf = false;
            }
            finally
            {
                binreader.Close();                
            }

            if (warning)
                MessageBox.Show("DAT has non-standard formatting, probably due to custom editing.  The file has been loaded, but any customization may or may not cause future problems.", "DAT Format Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Write(ProgressBar datprogress)
        {
            if (this.savefile == "")
                return;

            if (changed)
                this.Fix();

            binwriter = new BinaryWriter(File.Open(savefile, FileMode.Create));

            if (ParentTerf.files == 0)
                return;            

            ParentTerf.Write(this, datprogress);

            binwriter.Close();
        }
         

        #endregion
         
         
    }
     
    
}
