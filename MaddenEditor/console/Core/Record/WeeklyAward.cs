/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaddenEditor.Core.Record
{
    public class WeeklyAward : TableRecordModel
    {
        
        public const string STAT0 = "PSC7";         // These are UIN32, but are actually fields, so need to convert to/from string to display
        public const string STAT1 = "PAcc";
        public const string STAT2 = "PAsc";
        public const string STAT3 = "PAcs";
        public const string STAT4 = "PAss";
        public const string STAT5 = "PAcv";
        public const string STAT6 = "PAsv";

        public const string STAT0_VALUE = "PSV7";   // signed 16 bit int
        public const string STAT1_VALUE = "PAcC";
        public const string STAT2_VALUE = "PAsC";
        public const string STAT3_VALUE = "PAcS";
        public const string STAT4_VALUE = "PAsS";
        public const string STAT5_VALUE = "PAcV";
        public const string STAT6_VALUE = "PAsV";
        public const string STAT10 = "PAas";

        public const string FIRST_NAME = "PFNA";
        public const string LAST_NAME = "PLNA";
        public const string CONF_ID = "CGID";       
        public const string PLAYER_ID = "PGID";
        public const string TEAM_ID = "TGID";
        public const string WEEK_NUMBER = "SEWN";
        public const string STAT20 = "PAat";    
        

        public WeeklyAward(int record, TableModel tableModel, EditorModel EditorModel): base(record, tableModel, EditorModel)
		{
		}



    }
}
