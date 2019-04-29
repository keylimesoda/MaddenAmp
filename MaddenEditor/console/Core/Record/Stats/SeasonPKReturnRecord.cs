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
    public class SeasonPKReturnRecord : TableRecordModel
    {
        //  PSKP

        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";
        public const string KICK_RETURN_ATT = "srka";
        public const string KICK_RETURN_LONG = "srkL";
        public const string KICK_RETURN_TD = "srkt";
        public const string KICK_RETURN_YARDS = "srky";
        public const string PUNT_RETURN_ATT = "srpa";
        public const string PUNT_RETURN_LONG = "srpL";
        public const string PUNT_RETURN_TD = "srpt";
        public const string PUNT_RETURN_YARDS = "srpy";

        public SeasonPKReturnRecord(int record, TableModel tablemodel, EditorModel EditorModel)
            : base(record, tablemodel, EditorModel)
        {

        }

        #region Get / Set

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

        public int Season
        {
            get
            {
                return GetIntField(SEASON);
            }
            set
            {
                SetField(SEASON, value);
            }
        }

        public int Kra
        {
            get
            {
                return GetIntField(KICK_RETURN_ATT);
            }
            set
            {
                SetField(KICK_RETURN_ATT, value);
            }
        }

        public int Krl
        {
            get
            {
                return GetIntField(KICK_RETURN_LONG);
            }
            set
            {
                SetField(KICK_RETURN_LONG, value);
            }
        }

        public int Krtd
        {
            get
            {
                return GetIntField(KICK_RETURN_TD);
            }
            set
            {
                SetField(KICK_RETURN_TD, value);
            }
        }

        public int Kryds
        {
            get
            {
                return GetIntField(KICK_RETURN_YARDS);
            }
            set
            {
                SetField(KICK_RETURN_YARDS, value);
            }
        }

        public int Pra
        {
            get
            {
                return GetIntField(PUNT_RETURN_ATT);
            }
            set
            {
                SetField(PUNT_RETURN_ATT, value);
            }
        }

        public int Prl
        {
            get
            {
                return GetIntField(PUNT_RETURN_LONG);
            }
            set
            {
                SetField(PUNT_RETURN_LONG, value);
            }
        }

        public int Prtd
        {
            get
            {
                return GetIntField(PUNT_RETURN_TD);
            }
            set
            {
                SetField(PUNT_RETURN_TD, value);
            }
        }

        public int Pryds
        {
            get
            {
                return GetIntField(PUNT_RETURN_YARDS);
            }
            set
            {
                SetField(PUNT_RETURN_YARDS, value);
            }
        }

        #endregion

    }
}

