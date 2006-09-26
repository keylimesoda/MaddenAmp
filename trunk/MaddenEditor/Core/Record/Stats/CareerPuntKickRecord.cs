/******************************************************************************
 * MaddenAmp 
 * Copyright (C) 2006 stingray68
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;


namespace MaddenEditor.Core.Record.Stats
{
    public class CareerPuntKickRecord : TableRecordModel
    {
        // Kicker stats
        public const string PLAYER_ID = "PGID";
        public const string FGA = "ckfa";
        public const string FGM = "ckfm";
        public const string FG_BLOCKED = "ckfb";
        public const string FGL = "ckfL";
        public const string XPA = "ckea";
        public const string XPM = "ckem";
        public const string XP_BLOCKED = "ckeb";
        public const string FGA_129 = "ckaa";
        public const string FGA_3039 = "ckac";
        public const string FGA_4049 = "ckad";
        public const string FGA_50 = "ckae";
        public const string FGM_129 = "ckma";
        public const string FGM_3039 = "ckmc";
        public const string FGM_4049 = "ckmd";
        public const string FGM_50 = "ckme";
        public const string KICK_OFFS = "cknk";
        public const string TOUCHBACKS = "cktb";
        // Punter stats
        public const string PUNT_ATT = "cpat";
        public const string PUNT_YDS = "cpya";
        public const string PUNT_BLOCKED = "cpbl";
        public const string PUNT_LONG = "cpIN";
        public const string PUNT_NY = "cpny";
        public const string PUNT_IN20 = "cppt";
        public const string PUNT_TB = "cptb";

        public CareerPuntKickRecord(int record, TableModel tablemodel, EditorModel EditorModel)
            : base(record, tablemodel, EditorModel)
        {

        }

        public int PlayerId
        {
            get
            {
                return GetIntField(PLAYER_ID);
            }
            set
            {
                SetField(PLAYER_ID, value);
            }
        }

        public int Fga
        {
            get
            {
                return GetIntField(FGA);
            }
            set
            {
                SetField(FGA, value);
            }
        }

        public int Fgm
        {
            get
            {
                return GetIntField(FGM);
            }
            set
            {
                SetField(FGM, value);
            }
        }

        public int Fgbl
        {
            get
            {
                return GetIntField(FG_BLOCKED);
            }
            set
            {
                SetField(FG_BLOCKED, value);
            }
        }

        public int Fgl
        {
            get
            {
                return GetIntField(FGL);
            }
            set
            {
                SetField(FGL, value);
            }
        }

        public int Xpa
        {
            get
            {
                return GetIntField(XPA);
            }
            set
            {
                SetField(XPA, value);
            }
        }

        public int Xpm
        {
            get
            {
                return GetIntField(XPM);
            }
            set
            {
                SetField(XPM, value);
            }
        }

        public int Xpb
        {
            get
            {
                return GetIntField(XP_BLOCKED);
            }
            set
            {
                SetField(XP_BLOCKED, value);
            }
        }

        public int Fga_129
        {
            get
            {
                return GetIntField(FGA_129);
            }
            set
            {
                SetField(FGA_129, value);
            }
        }

        public int Fga_3039
        {
            get
            {
                return GetIntField(FGA_3039);
            }
            set
            {
                SetField(FGA_3039, value);
            }
        }

        public int Fga_4049
        {
            get
            {
                return GetIntField(FGA_4049);
            }
            set
            {
                SetField(FGA_4049, value);
            }
        }

        public int Fga_50
        {
            get
            {
                return GetIntField(FGA_50);
            }
            set
            {
                SetField(FGA_50, value);
            }
        }

        public int Fgm_129
        {
            get
            {
                return GetIntField(FGM_129);
            }
            set
            {
                SetField(FGM_129, value);
            }
        }

        public int Fgm_3039
        {
            get
            {
                return GetIntField(FGM_3039);
            }
            set
            {
                SetField(FGM_3039, value);
            }
        }

        public int Fgm_4049
        {
            get
            {
                return GetIntField(FGM_4049);
            }
            set
            {
                SetField(FGM_4049, value);
            }
        }

        public int Fgm_50
        {
            get
            {
                return GetIntField(FGM_50);
            }
            set
            {
                SetField(FGM_50, value);
            }
        }

        public int Kickoffs
        {
            get
            {
                return GetIntField(KICK_OFFS);
            }
            set
            {
                SetField(KICK_OFFS, value);
            }
        }

        public int Touchbacks
        {
            get
            {
                return GetIntField(TOUCHBACKS);
            }
            set
            {
                SetField(TOUCHBACKS, value);
            }
        }

        public int Puntatt
        {
            get
            {
                return GetIntField(PUNT_ATT);
            }
            set
            {
                SetField(PUNT_ATT, value);
            }
        }

        public int Puntyds
        {
            get
            {
                return GetIntField(PUNT_YDS);
            }
            set
            {
                SetField(PUNT_YDS, value);
            }
        }

        public int Puntblk
        {
            get
            {
                return GetIntField(PUNT_BLOCKED);
            }
            set
            {
                SetField(PUNT_BLOCKED, value);
            }
        }

        public int Puntlong
        {
            get
            {
                return GetIntField(PUNT_LONG);
            }
            set
            {
                SetField(PUNT_LONG, value);
            }
        }

        public int Puntny
        {
            get
            {
                return GetIntField(PUNT_NY);
            }
            set
            {
                SetField(PUNT_NY, value);
            }
        }

        public int Puntin20
        {
            get
            {
                return GetIntField(PUNT_IN20);
            }
            set
            {
                SetField(PUNT_IN20, value);
            }
        }

        public int Punttb
        {
            get
            {
                return GetIntField(PUNT_TB);
            }
            set
            {
                SetField(PUNT_TB, value);
            }
        }

    }
}
