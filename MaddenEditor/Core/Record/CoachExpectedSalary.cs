/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 Stingray68
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
using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core.Record
{
    public class CoachExpectedSalary : TableRecordModel
    {
        // OSCE

        public const string COACH_ID = "CCID";
        public const string EXPECTED_CONTRACT_LENGTH = "CECL";
        public const string EXPECTED_SALARY = "CESL";
        public const string POSITION = "COPS";        
        
        public CoachExpectedSalary(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int CoachID
        {
            get { return GetIntField(COACH_ID); }
            set { SetField(COACH_ID, value); }
        }
        public int CoachPosition
        {
            get { return GetIntField(POSITION); }
            set { SetField(POSITION, value); }
        }
        public int ExpectedLength
        {
            get { return GetIntField(EXPECTED_CONTRACT_LENGTH); }
            set { SetField(EXPECTED_CONTRACT_LENGTH,value); }
        }
        public int ExpectedSalary
        {
            get { return GetIntField(EXPECTED_SALARY); }
            set { SetField(EXPECTED_SALARY, value); }
        }

    }
}
