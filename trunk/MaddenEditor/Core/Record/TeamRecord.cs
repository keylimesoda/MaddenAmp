/******************************************************************************
 * Madden 2005 Editor
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
 * colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{

	public class TeamRecord : TableRecordModel
	{
		public const string TEAM_NAME = "TDNA";
		public const string TEAM_LAST_NAME = "TLNA";
		public const string TEAM_STATE = "TSNA";
		public const string TEAM_ID = "TGID";

		public const string TEAM_SALARY = "TMSA";

		public TeamRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public string Name
		{
			get
			{
				return GetStringField(TEAM_NAME);
			}
			set
			{
				SetField(TEAM_NAME, value);
			}
		}

		public string LastName
		{
			get
			{
				return GetStringField(TEAM_LAST_NAME);
			}
			set
			{
				SetField(TEAM_LAST_NAME, value);
			}
		}

		public string State
		{
			get
			{
				return GetStringField(TEAM_STATE);
			}
			set
			{
				SetField(TEAM_STATE, value);
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

		public int Salary
		{
			get
			{
				return GetIntField(TEAM_SALARY);
			}
			set
			{
				SetField(TEAM_SALARY, value);
			}
		}

	}
}
