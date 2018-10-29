/******************************************************************************
 * Madden Amp
 * Copyright (C) 2018 Stingray68
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
using System.Linq;
using System.Text;


namespace MaddenEditor.Core.Record.Stats
{
    public class ProBowlPlayer : TableRecordModel
    {
        public const string CONFERENCE_ID = "CGID";
        public const string PB_VOTES = "PBvc";
        public const string PLAYER_ID = "PGID";
        public const string PLAYER_ORIGINAL_ID = "POID";
        public const string PLAYER_POSITION = "PPOS";
        public const string TEAM_ID = "TGID";
        
        public ProBowlPlayer(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int ConferenceID
        {
            get { return GetIntField(CONFERENCE_ID); }
            set { SetField(CONFERENCE_ID, value); }
        }
        public int Votes
        {
            get { return GetIntField(PB_VOTES); }
            set { SetField(PB_VOTES, value); }
        }
        public int PlayerID
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int PlayerOrigID
        {
            get { return GetIntField(PLAYER_ORIGINAL_ID); }
            set { SetField(PLAYER_ORIGINAL_ID, value); }
        }
        public int Position
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
    }
}
