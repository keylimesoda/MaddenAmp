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

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class FranchiseTimeRecord : TableRecordModel
    {
        // table name = "SEAI"

        public const string SBRW = "SBRW";
        public const string SEFP = "SEFP";
        public const string SEOW = "SEOW";
        public const string SERB = "SERB";
        public const string SERW = "SERW";
        public const string SESI = "SESI";
        public const string SEST = "SEST";
        public const string SESW = "SESW";
        public const string WEEK_NUMBER = "SEWN";
        public const string WEEK_TYPE = "SEWT";
        public const string YEAR = "SEYR";
        public const string SFRW = "SFRW";
        public const string GAME_OFTHE_WEEK = "SGOW";
        public const string SNCP = "SNCP";
        public const string SNDP = "SNDP";
        public const string SNPB = "SNPB";
        public const string SNPW = "SNPW";
        public const string SNWP = "SNWP";
        public const string SCHEDULE_YEAR = "SSYE";

        
        // Week Type 0 preseason, 25 regular season, 50 wildcard, 75 divisional, 100 conference
        // 125 SB, 150 Pro Bowl, 175 offseason 200 Initial ?

		public FranchiseTimeRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        #region Get/Set
        public int Sbrw
        {
            get { return GetIntField(SBRW); }
            set { SetField(SBRW, value); }
        }
        public int Sefp
        {
            get { return GetIntField(SEFP); }
            set { SetField(SEFP, value); }
        }
        public int Seow
        {
            get { return GetIntField(SEOW); }
            set { SetField(SEOW, value); }
        }
        public int Serb
        {
            get { return GetIntField(SERB); }
            set { SetField(SERB, value); }
        }
        public int Serw
        {
            get { return GetIntField(SERW); }
            set { SetField(SERW, value); }
        }
        public int Sesi
        {
            get { return GetIntField(SESI); }
            set { SetField(SESI, value); }            
        }
        public int Sest
        {
            get { return GetIntField(SEST); }
            set { SetField(SEST, value); }
        }
        public int Sesw
        {
            get { return GetIntField(SESW); }
            set { SetField(SESW, value); }
        }
        public int Week
        {
            get
            {
                return GetIntField(WEEK_NUMBER);
            }
            set
            {
                SetField(WEEK_NUMBER, value);
            }
        }
        public int WeekType
        {
            get
            {
                return GetIntField(WEEK_TYPE);
            }
            set
            {
                SetField(WEEK_TYPE, value);
            }
        }
        public int Year
        {
            get
            {
                return GetIntField(YEAR);
            }
            set
            {
                SetField(YEAR, value);
            }
        }
        public int Sfrw
        {
            get { return GetIntField(SFRW); }
            set { SetField(SFRW, value); }
        }
        public int GameofWeek
        {
            get { return GetIntField(GAME_OFTHE_WEEK); }
            set { SetField(GAME_OFTHE_WEEK, value); }
        }
        public int Sncp
        {
            get { return GetIntField(SNCP); }
            set { SetField(SNCP, value); }
        }
        public int Sndp
        {
            get { return GetIntField(SNDP); }
            set { SetField(SNDP, value); }
        }
        public int Snpb
        {
            get { return GetIntField(SNPB); }
            set { SetField(SNPB, value); }
        }
        public int Snpw
        {
            get { return GetIntField(SNPW); }
            set { SetField(SNPW, value); }
        }
        public int Snwp
        {
            get { return GetIntField(SNWP); }
            set { SetField(SNWP, value); }
        }
        public int ScheduleYear
        {
            get { return GetIntField(SCHEDULE_YEAR); }
            set { SetField(SCHEDULE_YEAR, value); }
        }

        #endregion
    }
}
