/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005, 2006 MaddenWishlist.com and spin16
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
    public class SeasonStatsDefenseRecord : TableRecordModel
    {
        public const string PASSES_DEFENDED = "sdpd";
        public const string TACKLES = "sdta";
        public const string TACKLES_FOR_LOSS = "sdtl";
        public const string BLOCKS = "slbl";
        public const string FUMBLES_FORCED = "slff";
        public const string FUMBLES_RECOVERED = "slfr";
        public const string FUMBLE_YARDS = "slfy";
        public const string SAFETIES = "slsa";
        public const string SACKS = "slsk";
        public const string INTERCEPTIONS = "ssin";
        public const string INTERCEPTION_YARDS = "ssiy";
        public const string INTERCEPTION_LONG = "sslR";
        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";

        public SeasonStatsDefenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int PassesDefended
        {
            get
            {
                return GetIntField(PASSES_DEFENDED);
            }
            set
            {
                SetField(PASSES_DEFENDED, value);
            }
        }

        public int Tackles
        {
            get
            {
                return GetIntField(TACKLES);
            }
            set
            {
                SetField(TACKLES, value);
            }
        }

        public int TacklesForLoss
        {
            get
            {
                return GetIntField(TACKLES_FOR_LOSS);
            }
            set
            {
                SetField(TACKLES_FOR_LOSS, value);
            }
        }

        public int Blocks
        {
            get
            {
                return GetIntField(BLOCKS);
            }
            set
            {
                SetField(BLOCKS, value);
            }
        }

        public int FumblesRecovered
        {
            get
            {
                return GetIntField(FUMBLES_RECOVERED);
            }
            set
            {
                SetField(FUMBLES_RECOVERED, value);
            }
        }

        public int FumblesForced
        {
            get
            {
                return GetIntField(FUMBLES_FORCED);
            }
            set
            {
                SetField(FUMBLES_FORCED, value);
            }
        }

        public int FumbleYards
        {
            get
            {
                return GetIntField(FUMBLE_YARDS);
            }
            set
            {
                SetField(FUMBLE_YARDS, value);
            }
        }

        public int Safeties
        {
            get
            {
                return GetIntField(SAFETIES);
            }
            set
            {
                SetField(SAFETIES, value);
            }
        }

        public int Sacks
        {
            get
            {
                return GetIntField(SACKS);
            }
            set
            {
                SetField(SACKS, value);
            }
        }

        public int Interceptions
        {
            get
            {
                return GetIntField(INTERCEPTIONS);
            }
            set
            {
                SetField(INTERCEPTIONS, value);
            }
        }

        public int InterceptionYards
        {
            get
            {
                return GetIntField(INTERCEPTION_YARDS);
            }
            set
            {
                SetField(INTERCEPTION_YARDS, value);
            }
        }

        public int InterceptionLong
        {
            get
            {
                return GetIntField(INTERCEPTION_LONG);
            }
            set
            {
                SetField(INTERCEPTION_LONG, value);
            }
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
    }
}
