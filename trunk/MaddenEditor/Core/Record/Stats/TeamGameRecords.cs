/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
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
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using MaddenEditor.Db;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Record.FranchiseState;

namespace MaddenEditor.Core.Record.Stats
{
    //  TMGR

    public class TeamGameRecords : TableRecordModel
    {
        public const string OPPONENT_ID = "OGID";
        public const string WEEK_NUMBER = "SEWN";
        public const string SEASON = "SEYR";
        public const string AMOUNT = "STVL";
        public const string TEAM_ID = "TGID";
        public const string TYPE = "TSIX";

        public TeamGameRecords(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
        }


        public int Season
        {
            get { return GetIntField(SEASON); }
            set { SetField(SEASON, value); }
        }
        public int Amount
        {
            get { return GetIntField(AMOUNT); }
            set { SetField(AMOUNT, value); }
        }
        public int TeamId
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
        public int RecordType
        {
            get { return GetIntField(TYPE); }
            set { SetField(TYPE, value); }
        }
        public int OpponentId
        {
            get { return GetIntField(OPPONENT_ID); }
            set { SetField(OPPONENT_ID, value); }
        }
        public int Week
        {
            get { return GetIntField(WEEK_NUMBER); }
            set { SetField(WEEK_NUMBER, value); }
        }
    
    
    }
}
