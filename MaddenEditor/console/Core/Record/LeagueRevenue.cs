/*
    Madden Amp
    Copyright (C) 2014  Stingray68

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
  
    For more information also see  <https://www.gnu.org/licenses/gpl-faq.html>
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaddenEditor.Core.Record
{
    public class LeagueRevenue : TableRecordModel
    {
        // OWMI
        public const string LEAGUE_INCOME = "OWLI";
        public const string OWNER_INCOME = "OWOI";
        public const string SHARED_REVENUE = "OWRI";

        public LeagueRevenue(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int SharedRevenue
        {
            get { return GetIntField(SHARED_REVENUE); }
            set { SetField(SHARED_REVENUE, value); }
        }
        public float LeagueIncome
        {
            get { return GetFloatField(LEAGUE_INCOME); }
            set { SetField(LEAGUE_INCOME, value); }
        }
        public float OwnerIncome
        {
            get { return GetFloatField(OWNER_INCOME); }
            set { SetField(OWNER_INCOME, value); }
        }

    }
}
