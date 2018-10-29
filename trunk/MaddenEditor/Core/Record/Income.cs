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
    public class Income : TableRecordModel
    {
        //  OTIW

        public const string OWEA = "OWEA";
        public const string ATTENDANCE = "OWFA";
        public const string SUPPORT = "OWFS";
        public const string CONCESSIONS = "OWIC";
        public const string MERCHANDISE = "OWIM";
        public const string PARKING = "OWIP";
        public const string TICKETS = "OWIT";
        public const string SEASON_WEEK = "SEWN";
        public const string TEAM_ID = "TGID";
        
        public Income(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int Owea
        {
            get { return GetIntField(OWEA); }
            set { SetField(OWEA, value); }
        }
        public int Attendance
        {
            get { return GetIntField(ATTENDANCE); }
            set { SetField(ATTENDANCE, value); }
        }
        public int Support
        {
            get { return GetIntField(SUPPORT); }
            set { SetField(SUPPORT, value); }
        }
        public int Concessions
        {
            get { return GetIntField(CONCESSIONS); }
            set { SetField(CONCESSIONS, value); }
        }
        public int Merchandise
        {
            get { return GetIntField(MERCHANDISE); }
            set { SetField(MERCHANDISE,value); }
        }
        public int Parking
        {
            get { return GetIntField(PARKING); }
            set { SetField(PARKING, value); }
        }
        public int Tickets
        {
            get { return GetIntField(TICKETS); }
            set { SetField(TICKETS, value); }
        }
        public int SeasonWeek
        {
            get { return GetIntField(SEASON_WEEK); }
            set { SetField(SEASON_WEEK, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
    
    
    
    
    
    
    
    
    
    
    }
}
