/******************************************************************************
 * MaddenAmp 
 * Copyright (C) 2018 stingray68
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
    public class TeamRivalHistory : TableRecordModel
    {
        public const string TOTAL_LOSSES = "TCLO";
        public const string CURRENT_STREAK = "TCLS";
        public const string TOTAL_TIES = "TCTI";
        public const string TOTAL_WINS = "TCWI";
        public const string TEAM_ID = "TGID";
        public const string TEAM_RIVAL_ID = "TRIV";

        public TeamRivalHistory(int record, TableModel tablemodel, EditorModel EditorModel)
            : base(record, tablemodel, EditorModel)
        {

        }

        public int TotalLosses
        {
            get { return GetIntField(TOTAL_LOSSES); }
            set { SetField(TOTAL_LOSSES, value); }
        }
        public int CurrentStreak
        {
            get { return GetIntField(CURRENT_STREAK); }
            set { SetField(CURRENT_STREAK, value); }
        }
        public int TotalTies
        {
            get { return GetIntField(TOTAL_TIES); }
            set { SetField(TOTAL_TIES, value); }
        }
        public int TotalWins
        {
            get { return GetIntField(TOTAL_WINS); }
            set { SetField(TOTAL_WINS, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
        public int RivalID
        {
            get { return GetIntField(TEAM_RIVAL_ID); }
            set { SetField(TEAM_RIVAL_ID, value); }
        }
    }
}
