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
 * http://gommo.homelinux.net             colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Domain
{
	public class InjuryRecord : TableRecordModel
	{
		public const string INJURY_LENGTH = "INJL";
		public const string INJURY_TYPE = "INJT";
		public const string PLAYER_ID = "PGID";
		public const string TEAM_ID = "TGID";
		public const string INJURY_RSV = "INIR";

		public InjuryRecord(int record, RosterModel rosterModel)	: base(record, rosterModel)
		{

		}

		public int InjuryLength
		{
			get
			{
				return intFields[INJURY_LENGTH];
			}
			set
			{
				SetFieldWithBackup(INJURY_LENGTH, value);
			}
		}

		public int InjuryType
		{
			get
			{
				return intFields[INJURY_TYPE];
			}
			set
			{
				SetFieldWithBackup(INJURY_TYPE, value);
			}
		}

		public int PlayerId
		{
			get
			{
				return intFields[PLAYER_ID];
			}
			set
			{
				SetFieldWithBackup(PLAYER_ID, value);
			}
		}

		public int TeamId
		{
			get
			{
				return intFields[TEAM_ID];
			}
			set
			{
				SetFieldWithBackup(TEAM_ID, value);
			}
		}

		public bool InjuryReserver
		{
			get
			{
				return (intFields[INJURY_RSV] == 1);
			}
			set
			{
				SetFieldWithBackup(INJURY_RSV, Convert.ToInt32(value));
			}
		}
	}
}
