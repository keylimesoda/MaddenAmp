/******************************************************************************
 * Gommo's Madden Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
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
		public const string RESTRICTED_FA_1 = "RFA1";
		public const string RESTRICTED_FA_2 = "RFA2";
		public const string RESTRICTED_FA_3 = "RFA3";
		public const string RESTRICTED_FA_4 = "RFA4";
		public const string SALARY_CAP = "SCAD";

		public SalaryCapRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

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
	}
}
