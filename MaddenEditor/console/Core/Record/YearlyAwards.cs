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
using System.Text;


namespace MaddenEditor.Core.Record
{
    public enum awardtype
    {
        MVP = 0,
        OFFPLAYER = 1,
        DEFPLAYER = 2,
        OFFROOKIE = 3,
        DEFROOKIE = 4,
        BESTQB = 5,
        BESTRB = 6,
        BESTWR = 7,
        BESTOL = 8,
        BESTDL = 9,
        BESTLB = 10,
        BESTDB = 11,
        BESTKICKER = 12
    }
    
    public class YearlyAwards : TableRecordModel
    {
        public const string CONF_ID = "CGID";
        public const string AWARD_TYPE = "PAat";            
        public const string PLAYER_ID = "PGID";
        public const string SEASON_YEAR = "SEYR";

        public YearlyAwards(int record, TableModel tableModel, EditorModel EditorModel): base(record, tableModel, EditorModel)
		{
		}

        public int conference
        {
            get { return GetIntField(CONF_ID); }
            set { SetField(CONF_ID, value); }
        }
        public int award_type
        {
            get { return GetIntField(AWARD_TYPE); }
            set { SetField(AWARD_TYPE, value); }
        }
        public int PlayerID
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int year
        {
            get { return GetIntField(SEASON_YEAR); }
            set { SetField(SEASON_YEAR, value); }
        }


    }
}
