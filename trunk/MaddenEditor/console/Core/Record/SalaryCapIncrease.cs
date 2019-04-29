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
    public class SalaryCapIncrease: TableRecordModel
    {
        // table name "SAYR"    in franchise for v2004 only, otherwise table is in Db Templates 12

        public const string CAP_INCREASE_PERCENTAGE = "SCIP";
        public const string PLAYER_MINIMUM_SALARY_INCREASE = "SLIP";
        public const string YEAR = "SEYR";

        public SalaryCapIncrease(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public float CapIncrease
        {
            get { return GetFloatField(CAP_INCREASE_PERCENTAGE); }
            set { SetField(CAP_INCREASE_PERCENTAGE, value); }
        }
        public float PlayerMinimumSalaryIncrease
        {
            get { return GetFloatField(PLAYER_MINIMUM_SALARY_INCREASE); }
            set { SetField(PLAYER_MINIMUM_SALARY_INCREASE, value); }
        }
        public int Year
        {
            get { return GetIntField(YEAR); }
            set { SetField(YEAR, value); }
        }



    }
}
