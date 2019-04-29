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
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

using MaddenEditor.Core;
using MaddenEditor.Core.DatEditor;

namespace MaddenEditor.Core
{
    public class AmpDAT
    {
        #region Private Members        
        private DAT _playerDAT;
        private DAT _coachDAT;

        #endregion

        #region Public Members
        public DAT PlayerPort
        {
            get { return _playerDAT; }
            set { _playerDAT = value; }
        }
        public DAT CoachPort
        {
            get { return _coachDAT; }
            set { _coachDAT = value; }
        }
                
        public bool changed = false;

        #endregion

        public AmpDAT()
        {
            PlayerPort = new DAT();
            CoachPort = new DAT();            
            changed = false;
        }

        public void Init(EditorModel model, AmpConfig config)
        {
            PlayerPort.loadfile = config.PlayerPortFiles[(int)model.FileVersion];
            if (PlayerPort.loadfile != "" && config.AutoLoad_PlayerPort[(int)model.FileVersion])
                PlayerPort.Load();

            CoachPort.loadfile = config.CoachPortFiles[(int)model.FileVersion];
            if (CoachPort.loadfile != "" && config.AutoLoad_CoachPort[(int)model.FileVersion])
                CoachPort.Load();
        }        
    }
}
