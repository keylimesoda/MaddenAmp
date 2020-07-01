/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 StingRay68
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
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Db;
namespace MaddenEditor.Core
{
    public class UserSettingsModel
    {
        public string UserSettingFile = "";
        public bool WindowedMode = false;
        
        
        public UserSettingsModel()
        {

        }

        public string ReadUserSettings(string filename, FBVersion fbVersion)
        {
            if (filename == "")
            {
                filename = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (fbVersion == FBVersion.Madden19) filename += @"\Madden NFL 19\UserSettings.dat";
                if (fbVersion == FBVersion.Madden20) filename += @"\Madden NFL 20\UserSettings.dat";
            }

            if (!File.Exists(filename))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "Madden User Settings(*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.Multiselect = false;
                dialog.ShowDialog();

                if (dialog.FileName == "")
                    return "";
                filename = dialog.FileName;
            }

            BinaryReader binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            binreader.BaseStream.Position = 60;
            WindowedMode = binreader.ReadBoolean();

            binreader.Close();
            UserSettingFile = filename;
            return filename;
        }

    }
}
