/*
    MaddenAmp
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
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

using MaddenEditor.Core;
using MaddenEditor.Core.DatEditor;

namespace MaddenEditor.Core
{
    public class DATConfig
    {
        #region Private Members
        
        private bool _auto_load_playerport;
        private bool _auto_load_coachport;
        private string _madden08_playerportfile;
        private string _madden08_coachportfile;
        private string _madden07_playerportfile;
        private string _madden07_coachportfile;
        private string _madden06_playerportfile;
        private string _madden06_coachportfile;
        private string _madden05_playerportfile;
        private string _madden05_coachportfile;
        private string _madden04_playerportfile;
        private string _madden04_coachportfile;

        private bool _askplayersave;
        private bool _askcoachsave;

        private DAT _playerDAT;
        private DAT _coachDAT;
        #endregion

        #region Public Members

        public bool AutoLoad_PlayerPort
        {
            get { return _auto_load_playerport; }
            set { _auto_load_playerport = value; }
        }
        public bool AutoLoad_CoachPort
        {
            get { return _auto_load_coachport; }
            set { _auto_load_coachport = value; }
        }
        public string Madden08_PlayerPortFile
        {
            get { return _madden08_playerportfile; }
            set { _madden08_playerportfile = value; }
        }
        public string Madden08_CoachPortFile
        {
            get { return _madden08_coachportfile; }
            set { _madden08_coachportfile = value; }
        }
        public string Madden07_PlayerPortFile
        {
            get { return _madden07_playerportfile; }
            set { _madden07_playerportfile = value; }
        }
        public string Madden07_CoachPortFile
        {
            get { return _madden07_coachportfile; }
            set { _madden07_coachportfile = value; }
        }
        public string Madden06_PlayerPortFile
        {
            get { return _madden06_playerportfile; }
            set { _madden06_playerportfile = value; }
        }
        public string Madden06_CoachPortFile
        {
            get { return _madden06_coachportfile; }
            set { _madden06_coachportfile = value; }
        }
        public string Madden05_PlayerPortFile
        {
            get { return _madden05_playerportfile; }
            set { _madden05_playerportfile = value; }
        }
        public string Madden05_CoachPortFile
        {
            get { return _madden05_coachportfile; }
            set { _madden05_coachportfile = value; }
        }
        public string Madden04_PlayerPortFile
        {
            get { return _madden04_playerportfile; }
            set { _madden04_playerportfile = value; }
        }
        public string Madden04_CoachPortFile
        {
            get { return _madden04_coachportfile; }
            set { _madden04_coachportfile = value; }
        }
                
        public bool AskPlayerSave
        {
            get { return _askplayersave; }
            set { _askplayersave = value; }
        }
        public bool AskCoachSave
        {
            get { return _askcoachsave; }
            set { _askcoachsave = value; }
        }

        public DAT PlayerDAT
        {
            get { return _playerDAT; }
            set { _playerDAT = value; }
        }
        public DAT CoachDAT
        {
            get { return _coachDAT; }
            set { _coachDAT = value; }
        }

        public string datconfigfile = Application.StartupPath + @"\DAT.config";
        public bool changed = false;

        #endregion

        public DATConfig()
        {
            AutoLoad_PlayerPort = false;
            AutoLoad_CoachPort = false;
            Madden08_PlayerPortFile = "";
            Madden08_CoachPortFile = "";
            Madden07_PlayerPortFile = "";
            Madden07_CoachPortFile = ""; 
            Madden06_PlayerPortFile = "";
            Madden06_CoachPortFile = ""; 
            Madden05_PlayerPortFile = "";
            Madden05_CoachPortFile = ""; 
            Madden04_PlayerPortFile = "";
            Madden04_CoachPortFile = "";
            AskPlayerSave = true;
            AskCoachSave = true;
            PlayerDAT = new DAT();
            CoachDAT = new DAT();
            datconfigfile = Application.StartupPath + @"\DAT.config";
            changed = false;
        }


        public bool Init(EditorModel model)
        {
            if (File.Exists(datconfigfile))
            {
                if (this.Load())
                {
                    if (model.FileVersion == MaddenFileVersion.Ver2008)
                    {
                        PlayerDAT.loadfile = Madden08_PlayerPortFile;
                        CoachDAT.loadfile = Madden08_CoachPortFile;
                    }
                    if (model.FileVersion == MaddenFileVersion.Ver2007)
                    {
                        PlayerDAT.loadfile = Madden07_PlayerPortFile;
                        CoachDAT.loadfile = Madden07_CoachPortFile;
                    }
                    if (model.FileVersion == MaddenFileVersion.Ver2006)
                    {
                        PlayerDAT.loadfile = Madden06_PlayerPortFile;
                        CoachDAT.loadfile = Madden06_CoachPortFile;
                    }
                    if (model.FileVersion == MaddenFileVersion.Ver2005)
                    {
                        PlayerDAT.loadfile = Madden05_PlayerPortFile;
                        CoachDAT.loadfile = Madden05_CoachPortFile;
                    }
                    if (model.FileVersion == MaddenFileVersion.Ver2004)
                    {
                        PlayerDAT.loadfile = Madden04_PlayerPortFile;
                        CoachDAT.loadfile = Madden04_CoachPortFile;
                    }

                    if (this.AutoLoad_PlayerPort)
                        PlayerDAT.Load();
                    if (this.AutoLoad_CoachPort)
                        CoachDAT.Load();
                    return true;
                }
            }

            this.Save();
            return false;
        }
            
        

        public bool Load()
        {
            BinaryReader binreader = new BinaryReader(File.Open(datconfigfile, FileMode.Open));

            try
            {
                this.AutoLoad_PlayerPort = binreader.ReadBoolean();
                this.AutoLoad_CoachPort = binreader.ReadBoolean();
                this.Madden08_PlayerPortFile = binreader.ReadString();
                this.Madden08_CoachPortFile = binreader.ReadString();
                this.Madden07_PlayerPortFile = binreader.ReadString();
                this.Madden07_CoachPortFile = binreader.ReadString();
                this.Madden06_PlayerPortFile = binreader.ReadString();
                this.Madden06_CoachPortFile = binreader.ReadString();
                this.Madden05_PlayerPortFile = binreader.ReadString();
                this.Madden05_CoachPortFile = binreader.ReadString();
                this.Madden04_PlayerPortFile = binreader.ReadString();
                this.Madden04_CoachPortFile = binreader.ReadString();
                this.AskPlayerSave = binreader.ReadBoolean();
                this.AskCoachSave = binreader.ReadBoolean();
            }

            catch (EndOfStreamException)
            {
                changed = true;                
            }

            binreader.Close();
            if (changed)
                return false;
            else return true;
        }

        public void Save()
        {
            BinaryWriter binwriter = new BinaryWriter(File.Open(datconfigfile, FileMode.Create));
            binwriter.Write(this.AutoLoad_PlayerPort);
            binwriter.Write(this.AutoLoad_CoachPort);
            binwriter.Write(Madden08_PlayerPortFile);
            binwriter.Write(Madden08_CoachPortFile);
            binwriter.Write(Madden07_PlayerPortFile);
            binwriter.Write(Madden07_CoachPortFile);
            binwriter.Write(Madden06_PlayerPortFile);
            binwriter.Write(Madden06_CoachPortFile);
            binwriter.Write(Madden05_PlayerPortFile);
            binwriter.Write(Madden05_CoachPortFile);
            binwriter.Write(Madden04_PlayerPortFile);
            binwriter.Write(Madden04_CoachPortFile);
            binwriter.Write(AskPlayerSave);
            binwriter.Write(AskCoachSave);

            binwriter.Close();
        }
    }
}
