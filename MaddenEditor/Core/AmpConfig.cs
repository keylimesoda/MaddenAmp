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
        private uint AmpHeader = 558910785;        
        private string ampconfigname = Application.StartupPath + @"\config.AMP";
        private string _stream_filename;
        private string _db_misc_filename;
        private List<SliderSet> _amp_sliders;
        private int version = 0;        
        private int currentversion = 16;
        #endregion

        #region Public members
        
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
        public byte[] Madden19Serial;
        public string Madden19UserSettingsFilename;
        #endregion
               

        public AmpConfig()
        {            
            StreamFilename = "";
            db_misc_filename = "";           
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
            Madden19Serial = new byte[24];
            Madden19UserSettingsFilename = "";
            
            changed = false;

            for (int c = 0; c < 6; c++)
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
            {
                changed = true;
                return false;
            }
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
            int v = 6;
            if (binreader.ReadUInt32() != AmpHeader)
                return false;


            versioncheck = binreader.ReadByte();
            SkipSplash = binreader.ReadBoolean();
            if (currentversion > versioncheck)
            {
                SkipSplash = false;
            }

            // Not allowing skip for now
            SkipSplash = false;

            if (versioncheck == 6)
                v = 5;
            type = binreader.ReadByte();
            if (type != 0)
                return false;

            for (int n = 0; n < v; n++)
                streamdb_autoload[n] = binreader.ReadBoolean();
            for (int n = 0; n < v; n++)
                stream_names[n] = binreader.ReadString();

            for (int p = 0; p < v; p++)
                this.AutoLoad_PlayerPort[p] = binreader.ReadBoolean();
            for (int p = 0; p < v; p++)
                this.AskCoachSave[p] = binreader.ReadBoolean();
            for (int p = 0; p < v; p++)
                this.PlayerPortFiles[p] = binreader.ReadString();

            for (int c = 0; c < v; c++)
                this.AutoLoad_CoachPort[c] = binreader.ReadBoolean();
            for (int c = 0; c < v; c++)
                this.AskCoachSave[c] = binreader.ReadBoolean();
            for (int c = 0; c < v; c++)
                this.CoachPortFiles[c] = binreader.ReadString();

            for (int m = 0; m < v; m++)
                this.db_misc_autoload[m] = binreader.ReadBoolean();
            for (int m = 0; m < v; m++)
                this.db_misc_names[m] = binreader.ReadString();

            if (versioncheck >= 15)
            {
                Madden19Serial = binreader.ReadBytes(24);
                Madden19UserSettingsFilename = binreader.ReadString();
            }

            return true;
        }    
        
        
        public void Write()
        {   
            BinaryWriter binwriter = new BinaryWriter(File.Open(ampconfigname, FileMode.Create));
            binwriter.Write(AmpHeader);
            binwriter.Write((byte)currentversion);
            binwriter.Write(SkipSplash);
            binwriter.Write((byte)ConfigType.Main);
            //7 bytes so far

            for (int n = 0; n < 6; n++)            
                binwriter.Write(streamdb_autoload[n]);
            for (int n = 0; n < 6; n++)
                binwriter.Write(stream_names[n]);
                            
            for (int p = 0; p < 6; p++)            
                binwriter.Write(AutoLoad_PlayerPort[p]);
            for (int p = 0; p < 6; p++)
                binwriter.Write(AskPlayerSave[p]);
            for (int p = 0; p < 6; p++)
                binwriter.Write(PlayerPortFiles[p]);
            
            for (int c = 0; c < 6; c++)            
                binwriter.Write(AutoLoad_CoachPort[c]);
            for (int c = 0; c < 6; c++)
                binwriter.Write(AskCoachSave[c]);
            for (int c = 0; c < 6; c++)
                binwriter.Write(CoachPortFiles[c]);

            for (int m = 0; m < 6; m++)
                binwriter.Write(db_misc_autoload[m]);
            for (int m = 0; m < 6; m++)
                binwriter.Write(db_misc_names[m]);

            binwriter.Write(Madden19Serial);
            binwriter.Write(Madden19UserSettingsFilename);

            changed = false;
            binwriter.Close();
        }
        
        #endregion

    }
}
