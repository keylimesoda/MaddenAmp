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
    public class UserOptionRecord : TableRecordModel
    {
        public const string AUTO_SUB_IN = "OAFI";
        public const string AUTO_SUB_OUT = "OAFO";
        public const string PASS_LEAD_SENSITIVITY = "OPLS";


        public UserOptionRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int AutoSubIn
        {
            get { return GetIntField(AUTO_SUB_IN); }
            set { SetField(AUTO_SUB_IN, value); }
        }
        public int AutoSubOut
        {
            get { return GetIntField(AUTO_SUB_OUT); }
            set { SetField(AUTO_SUB_IN, value); }
        }
        public int PassLead
        {
            get { return GetIntField(PASS_LEAD_SENSITIVITY); }
            set { SetField(PASS_LEAD_SENSITIVITY, value); }
        }
    
    
    }
}
