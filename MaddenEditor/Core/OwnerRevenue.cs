/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2016 Stingray68
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

namespace MaddenEditor.Core
{
    public class OwnerRevenue : TableRecordModel
    {
        // OWTI

        public const string OINB = "OINB";
        public const string OSYL = "OSYL";
        public const string OTVN = "OTVN";
        public const string STAFF_SALARIES = "OWEC";
        public const string OWEF = "OWEF";
        public const string STADIUM_MAINTENANCE = "OWEM";
        public const string OWER = "OWER";
        public const string STADIUM_UPGRADES = "OWEU";
        public const string OWIO = "OWIO";
        public const string TV_INCOME = "OWIV";
        public const string OWRC = "OWRC";
        public const string OWRY = "OWRY";
        public const string TV_CONTRACT_LENGTH = "OWTY";
        public const string TEAM_ID = "TGID";

        public OwnerRevenue(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int Oinb
        {
            get { return GetIntField(OINB); }
            set { SetField(OINB, value); }
        }
        public int Osyl
        {
            get { return GetIntField(OSYL); }
            set { SetField(OSYL, value); }
        }
        public int Otvn
        {
            get { return GetIntField(OTVN); }
            set { SetField(OTVN, value); }
        }
        public int StaffSalaries
        {
            get { return GetIntField(STAFF_SALARIES); }
            set { SetField(STAFF_SALARIES, value); }
        }
        public int Owef
        {
            get { return GetIntField(OWEF); }
            set { SetField(OWEF, value); }
        }        
        public int StadiumMaintenance
        {
            get { return GetIntField(STADIUM_MAINTENANCE); }
            set { SetField(STADIUM_MAINTENANCE, value); }
        }
        public int Ower
        {
            get { return GetIntField(OWER); }
            set { SetField(OWER, value); }
        }        
        public int StadiumUpgrades
        {
            get { return GetIntField(STADIUM_UPGRADES); }
            set { SetField(STADIUM_UPGRADES, value); }
        }
        public int Owio
        {
            get { return GetIntField(OWIO); }
            set { SetField(OWIO, value); }
        }
        public int TvIncome
        {
            get { return GetIntField(TV_INCOME); }
            set { SetField(TV_INCOME, value); }
        }
        public int Owrc
        {
            get { return GetIntField(OWRC); }
            set { SetField(OWRC, value); }
        }
        public int Owry
        {
            get { return GetIntField(OWRY); }
            set { SetField(OWRY, value); }
        }
        public int TvContract
        {
            get { return GetIntField(TV_CONTRACT_LENGTH); }
            set { SetField(TV_CONTRACT_LENGTH, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }


    }
}
