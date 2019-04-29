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
    public class PlayerMinimumSalary : TableRecordModel
    {
        // table name = "SAYP"
        public const string MINIMUM_SALARY = "SMID";
        public const string PLAYER_YEARS_PRO = "PYRP";

        public PlayerMinimumSalary(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {

        }

        public int MinimumSalary
        {
            get { return GetIntField(MINIMUM_SALARY); }
            set { SetField(MINIMUM_SALARY, value); }
        }
        public int YearsPro
        {
            get { return GetIntField(PLAYER_YEARS_PRO); }
            set { SetField(PLAYER_YEARS_PRO, value); }
        }
        
    }
}
