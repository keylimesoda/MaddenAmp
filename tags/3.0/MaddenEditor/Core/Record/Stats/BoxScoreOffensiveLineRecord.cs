/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
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
    class BoxScoreOffensiveLineRecord : TableRecordModel
    {
        public const string PANCAKES = "gopa";
        public const string SACKS_ALLOWED = "gosa";
        public const string SEASON = "SEYR";
        public const string WEEK = "SEWN";
        public const string TEAM_ID = "TGID";
        public const string PLAYER_ID = "PGID";

        public BoxScoreOffensiveLineRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int Pancakes
        {
            get
            {
                return GetIntField(PANCAKES);
            }
            set
            {
                SetField(PANCAKES, value);
            }
        }

        public int SacksAllowed
        {
            get
            {
                return GetIntField(SACKS_ALLOWED);
            }
            set
            {
                SetField(SACKS_ALLOWED, value);
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

        public int Week
        {
            get
            {
                return GetIntField(WEEK);
            }
            set
            {
                SetField(WEEK, value);
            }
        }

        public int TeamId
        {
            get
            {
                return GetIntField(TEAM_ID);
            }
            set
            {
                SetField(TEAM_ID, value);
            }
        }
    }
}
