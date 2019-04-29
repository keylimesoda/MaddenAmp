/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using MaddenEditor.Core;

using MaddenEditor.Forms;

namespace MaddenEditor.Core
{
    public enum versiontype
    {
        Major = 0,
        Minor = 1,
        Build = 2,
        Revision = 3
    }

    public enum ConfigType
    {        
        Main = 0,
        Sliders = 1,
        Roles = 2
    }
    
    public class AmpConfig
    {
        #region Private members
        private EditorModel _stream_model;
        private EditorModel _db_misc;
        private AmpDAT _ampdat;
        private uint AmpHeader = 558910785;        
        private string ampconfigname = Application.StartupPath + @"\config.AMP";
        private string _stream_filename;
        private string _db_misc_filename;
        private List<SliderSet> _amp_sliders;
        private int version = 0;
        #endregion

        #region Public members
        public EditorModel streamdb_model
        {
            get { return _stream_model; }
            set { _stream_model = value; }
        }
        public EditorModel db_misc_model
        {
            get { return _db_misc; }
            set { _db_misc = value; }
        }
        public AmpDAT Ampdat
        {
            get { return _ampdat; }
            set { _ampdat = value; }
        }
        public string StreamFilename
        {
            get { return _stream_filename; }
            set { _stream_filename = value; }
        }        
        public List<SliderSet> Amp_Sliders
        {
            get { return _amp_sliders; }
            set { _amp_sliders = value; }
        }
        public string db_misc_filename
        {
            get { return _db_misc_filename; }
            set { _db_misc_filename = value; }
        }

        public List<string> stream_names;       
        public List<bool> streamdb_autoload;
        public List<bool> AutoLoad_PlayerPort;
        public List<bool> AutoLoad_CoachPort;
        public List<bool> AskPlayerSave;
        public List<bool> AskCoachSave;
        public List<string> PlayerPortFiles;
        public List<string> CoachPortFiles;
        public bool SkipSplash = false;
        public bool changed = false;
        public int versioncheck = 0;
        public int type = 0;

        
        public List<bool> db_misc_autoload;
        public List<string> db_misc_names;
        #endregion
               

        public AmpConfig()
        {            
            StreamFilename = "";
            db_misc_filename = "";
            streamdb_model = null;
            db_misc_model = null;
            Ampdat = new AmpDAT();
            versioncheck = 0;
            stream_names = new List<string>();        
            streamdb_autoload = new List<bool>();
            AutoLoad_PlayerPort = new List<bool>();
            AutoLoad_CoachPort = new List<bool>();
            AskPlayerSave = new List<bool>();
            AskCoachSave = new List<bool>();
            PlayerPortFiles = new List<string>();
            CoachPortFiles = new List<string>();
            db_misc_autoload = new List<bool>();
            db_misc_filename = "";
            db_misc_names = new List<string>();            
            
            changed = false;

            for (int c = 0; c < 5; c++)
            {
                stream_names.Add("");
                streamdb_autoload.Add(false);
                AutoLoad_PlayerPort.Add(false);
                AutoLoad_CoachPort.Add(false);
                AskPlayerSave.Add(true);
                AskCoachSave.Add(true);
                PlayerPortFiles.Add("");
                CoachPortFiles.Add("");
                db_misc_autoload.Add(false);
                db_misc_names.Add("");
            }
        }

        public bool Init(EditorModel model)
        { 
            if (this.streamdb_autoload[(int)model.FileVersion])
            {
                StreamFilename = this.stream_names[(int)model.FileVersion];
            }

            if (StreamFilename != "")
            {
                try
                {
                    streamdb_model = new EditorModel(StreamFilename, null);                    
                }
                catch (ApplicationException err)
                {
                    streamdb_model = null;
                }
            }

            if (this.db_misc_autoload[(int)model.FileVersion])
            {
                db_misc_filename = this.db_misc_names[(int)model.FileVersion];
            }

            if (db_misc_filename != "")
            {
                try
                {
                    db_misc_model = new EditorModel(db_misc_filename, null);
                }
                catch (ApplicationException err)
                {
                    db_misc_model = null;
                }
            }

            return true;
        }

        public string GetPlayerPortName(EditorModel model)
        {
            return PlayerPortFiles[(int)model.FileVersion];
        }
        
        public string GetCoachPortName(EditorModel model)
        {
            return CoachPortFiles[(int)model.FileVersion];
        }

        public string GetStreamedDBName(EditorModel model)
        {
            return stream_names[(int)model.FileVersion];
        }
        public string GetDB_MiscName(EditorModel model)
        {
            return db_misc_names[(int)model.FileVersion];
        }
                

        #region File IO
        
        public bool Read()
        {           
            if (!File.Exists(ampconfigname))
                return false;
            bool good = false;
            BinaryReader binreader = new BinaryReader(File.Open(ampconfigname, FileMode.Open));            
            try
            {
                good = ReadMain(binreader);
            }
            catch (Exception ex)
            {
                changed = true;                
            }
            finally
            {
                binreader.Close();
            }

           
            return good;
        }
                
        public bool ReadMain(BinaryReader binreader)
        {
            if (binreader.ReadUInt32() == AmpHeader)
            {
                versioncheck = binreader.ReadByte();
                SkipSplash = binreader.ReadBoolean();
                type = binreader.ReadByte();
                if (type != 0)
                    return false;

                for (int n = 0; n < 5; n++)                
                    streamdb_autoload[n] = binreader.ReadBoolean();
                for (int n = 0; n < 5; n++)  
                    stream_names[n] = binreader.ReadString();
                
                for (int p = 0; p < 5; p++)               
                    this.AutoLoad_PlayerPort[p] = binreader.ReadBoolean();
                for (int p = 0; p < 5; p++) 
                    this.AskCoachSave[p] = binreader.ReadBoolean();
                for (int p = 0; p < 5; p++) 
                    this.PlayerPortFiles[p] = binreader.ReadString();
                
                for (int c = 0; c < 5; c++)                
                    this.AutoLoad_CoachPort[c] = binreader.ReadBoolean();
                for (int c = 0; c < 5; c++)
                    this.AskCoachSave[c] = binreader.ReadBoolean();
                for (int c = 0; c < 5; c++)
                    this.CoachPortFiles[c] = binreader.ReadString();

                for (int m = 0; m < 5; m++)
                    this.db_misc_autoload[m] = binreader.ReadBoolean();
                for (int m = 0; m < 5; m++)
                    this.db_misc_names[m] = binreader.ReadString();

                return true;
            }            

            return false;
        }
        
        public void Write()
        {            
            BinaryWriter binwriter = new BinaryWriter(File.Open(ampconfigname, FileMode.Create));
            binwriter.Write(AmpHeader);
            binwriter.Write((byte)version);
            binwriter.Write(SkipSplash);
            binwriter.Write((byte)ConfigType.Main);
            //7 bytes so far

            for (int n = 0; n < 5; n++)            
                binwriter.Write(streamdb_autoload[n]);
            for (int n = 0; n < 5; n++)
                binwriter.Write(stream_names[n]);
                            
            for (int p = 0; p < 5; p++)            
                binwriter.Write(AutoLoad_PlayerPort[p]);
            for (int p = 0; p < 5; p++)
                binwriter.Write(AskPlayerSave[p]);
            for (int p = 0; p < 5; p++)
                binwriter.Write(PlayerPortFiles[p]);
            
            for (int c = 0; c < 5; c++)            
                binwriter.Write(AutoLoad_CoachPort[c]);
            for (int c = 0; c < 5; c++)
                binwriter.Write(AskCoachSave[c]);
            for (int c = 0; c < 5; c++)
                binwriter.Write(CoachPortFiles[c]);

            for (int m = 0; m < 5; m++)
                binwriter.Write(db_misc_autoload[m]);
            for (int m = 0; m < 5; m++)
                binwriter.Write(db_misc_names[m]);
            
            binwriter.Close();
        }
        
        #endregion

    }
}
