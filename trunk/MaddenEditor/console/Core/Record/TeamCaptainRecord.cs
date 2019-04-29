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
	public class TeamCaptainRecord : TableRecordModel
	{
		//  TCPT

        public const string CAPTAIN_1 = "CPT1";
		public const string CAPTAIN_2 = "CPT2";
		public const string CAPTAIN_3 = "CPT3";
		public const string TEAM_ID = "TGID";

		public TeamCaptainRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

		public int Captain1
		{
			get
			{
				return GetIntField(CAPTAIN_1);
			}
			set
			{
				SetField(CAPTAIN_1, value);
			}
		}

		public int Captain2
		{
			get
			{
				return GetIntField(CAPTAIN_2);
			}
			set
			{
				SetField(CAPTAIN_2, value);
			}
		}

		public int Captain3
		{
			get
			{
				return GetIntField(CAPTAIN_3);
			}
			set
			{
				SetField(CAPTAIN_3, value);
			}
		}

		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}
	}
}
