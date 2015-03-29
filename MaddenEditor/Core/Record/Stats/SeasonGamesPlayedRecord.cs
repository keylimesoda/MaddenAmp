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
    public class SeasonGamesPlayedRecord : TableRecordModel
    {
        // PSNG

        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";
        public const string GAMES_DOWNS_PLAYED = "sgdp";
        public const string GAMES_PLAYED = "sgmp";
        public const string GAMES_STARTED = "sgms";
        public const string PREVIOUS_TEAM = "sgYY";
        

        public SeasonGamesPlayedRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
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

        public int GamesPlayed
        {
            get
            {
                return GetIntField(GAMES_PLAYED);
            }
            set
            {
                SetField(GAMES_PLAYED, value);
            }
        }

        public int GamesStarted
        {
            get
            {
                return GetIntField(GAMES_STARTED);
            }
            set
            {
                SetField(GAMES_STARTED, value);
            }
        }

        public int DownsPlayed
        {
            get { return GetIntField(GAMES_DOWNS_PLAYED); }
            set { SetField(GAMES_DOWNS_PLAYED, value); }
        }

        public int PreviousTeam
        {
            get { return GetIntField(PREVIOUS_TEAM); }
            set { SetField(PREVIOUS_TEAM, value); }
        }

        
        #endregion
    }
}
