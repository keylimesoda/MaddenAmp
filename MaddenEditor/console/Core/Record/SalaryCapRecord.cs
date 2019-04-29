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

using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{
	public class SalaryCapRecord : TableRecordModel
	{
        // table name = SLRI

		// restricted free agent fields v2005+
        public const string RESTRICTED_FA_1 = "RFA1";
		public const string RESTRICTED_FA_2 = "RFA2";
		public const string RESTRICTED_FA_3 = "RFA3";
		public const string RESTRICTED_FA_4 = "RFA4";
        
        public const string SAIP = "SAIP";
        public const string SAMU = "SAMU";
		public const string SALARY_CAP = "SCAD";
        public const string SIIP = "SIIP";
        public const string SMAD = "SMAD";

		public SalaryCapRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public Dictionary<int, double> LeagueCap = new Dictionary<int, double>();
        
        
        public int RestrictedFA1
		{
			get
			{
				return GetIntField(RESTRICTED_FA_1);
			}
			set
			{
				SetField(RESTRICTED_FA_1, value);
			}
		}

		public int RestrictedFA2
		{
			get
			{
				return GetIntField(RESTRICTED_FA_2);
			}
			set
			{
				SetField(RESTRICTED_FA_2, value);
			}
		}

		public int RestrictedFA3
		{
			get
			{
				return GetIntField(RESTRICTED_FA_3);
			}
			set
			{
				SetField(RESTRICTED_FA_3, value);
			}
		}

		public int RestrictedFA4
		{
			get
			{
				return GetIntField(RESTRICTED_FA_4);
			}
			set
			{
				SetField(RESTRICTED_FA_4, value);
			}
		}

		public int SalaryCap
		{
			get
			{
				return GetIntField(SALARY_CAP);
			}
			set
			{
				SetField(SALARY_CAP, value);
			}
		}
	
        public void InitCap()
        {
            LeagueCap.Add(2003, 75);
            LeagueCap.Add(2004, 80.58);
            LeagueCap.Add(2005, 85.5);
            LeagueCap.Add(2006, 102);
            LeagueCap.Add(2007, 109);
            LeagueCap.Add(2008, 116);
            LeagueCap.Add(2009, 123);
            LeagueCap.Add(2010, 123);       // uncapped year
            LeagueCap.Add(2011, 120);
            LeagueCap.Add(2012, 120.6);
            LeagueCap.Add(2013, 123);
            LeagueCap.Add(2014, 133);
            LeagueCap.Add(2015, 143.28);
            LeagueCap.Add(2016, 155.27);
        }
    
    
    
    }
}
