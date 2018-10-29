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
    public class TeamWinLossRecord : TableRecordModel
    {
        // OTRS

        public const string SEASON = "SEYR";
        public const string TEAM_ID = "TGID";
        public const string TIES = "TSRI";
        public const string RESULT = "TRSL";
        public const string LOSSES = "TSRO";
        public const string WINS = "TSRW";

        public TeamWinLossRecord(int record, TableModel tablemodel, EditorModel EditorModel)
            : base(record, tablemodel, EditorModel)
        {

        }

        public int Season
        {            
            get { return GetIntField(SEASON); }
            set { SetField(SEASON, value); }
        }
        public int TeamId
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
        public int Ties
        {
            get { return GetIntField(TIES); }
            set { SetField(TIES, value); }
        }
        public int Result
        {
            get { return GetIntField(RESULT); }
            set { SetField(RESULT, value); }
        }
        public int Losses
        {
            get { return GetIntField(LOSSES); }
            set { SetField(LOSSES, value); }
        }
        public int Wins
        {
            get { return GetIntField(WINS); }
            set { SetField(WINS, value); }
        }
    }
}
