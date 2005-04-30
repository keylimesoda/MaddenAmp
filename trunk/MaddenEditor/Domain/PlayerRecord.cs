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
	public class PlayerRecord : TableRecordModel
	{
		public const string FIRST_NAME = "PFNA";
		public const string LAST_NAME = "PLNA";
		public const string POSITION_ID = "PPOS";

		public const string TEAM_ID = "TGID";
		public const string PLAYER_ID = "PGID";
		public const string COLLEGE_ID = "PCOL";

		public PlayerRecord(int record)
			: base(record)
		{

		}

		public string FirstName
		{
			get
			{
				return stringFields[FIRST_NAME];
			}
			set
			{
				stringFields[FIRST_NAME] = value;
			}
		}

		public string LastName
		{
			get
			{
				return stringFields[LAST_NAME];
			}
			set
			{
				stringFields[LAST_NAME] = value;
			}
		}

		public int PositionId
		{
			get
			{
				return intFields[POSITION_ID];
			}
			set
			{
				intFields[POSITION_ID] = value;
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
				intFields[TEAM_ID] = value;
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
				intFields[PLAYER_ID] = value;
			}
		}

		public int CollegeId
		{
			get
			{
				return intFields[COLLEGE_ID];
			}
			set
			{
				intFields[COLLEGE_ID] = value;
			}
		}
	}
}