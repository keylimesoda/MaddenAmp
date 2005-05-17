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
		public const string TEAM_LONG_NAME = "TLNA";
		public const string TEAM_SHORT_NAME = "TSNA";
		public const string TEAM_NICK_NAME = "TMNC";
		public const string TEAM_ID = "TGID";
		public const string REPUTATION = "TREP";

		public const string CITY_ID = "CYID";
		public const string CONFERENCE_ID = "CGID";
		public const string DIVISION_ID = "DGID";
		public const string LEAGUE_ID = "LGID";

		public const string TEAM_SALARY = "TMSA";

		public const string QB_RATING = "TRQB";
		public const string RB_RATING = "TRRB";
		public const string OL_RATING = "TROL";
		public const string WR_RATING = "TWRR";
		
		public const string DB_RATING = "TRDB";
		public const string LB_RATING = "TRLB";
		public const string DL_RATING = "TRDL";
		public const string ST_RATING = "TRST";

		public const string DEFENSIVE_RATING = "TRDE";
		public const string OFFENSIVE_RATING = "TROF";

		public const string OFFENSIVE_PLAYBOOK = "TOPB";
		public const string DEFENSIVE_PLAYBOOK = "TDPB";

		public TeamRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public override string ToString()
		{
			return Name;
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

		public string LongName
		{
			get
			{
				return GetStringField(TEAM_LONG_NAME);
			}
			set
			{
				SetField(TEAM_LONG_NAME, value);
			}
		}

		public string ShortName
		{
			get
			{
				return GetStringField(TEAM_SHORT_NAME);
			}
			set
			{
				SetField(TEAM_SHORT_NAME, value);
			}
		}

		public string NickName
		{
			get
			{
				return GetStringField(TEAM_NICK_NAME);
			}
			set
			{
				SetField(TEAM_NICK_NAME, value);
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

		public int DivisionId
		{
			get
			{
				return GetIntField(DIVISION_ID);
			}
			set
			{
				SetField(DIVISION_ID, value);
			}
		}

		public int ConferenceId
		{
			get
			{
				return GetIntField(CONFERENCE_ID);
			}
			set
			{
				SetField(CONFERENCE_ID, value);
			}
		}

		public int LeagueId
		{
			get
			{
				return GetIntField(LEAGUE_ID);
			}
			set
			{
				SetField(LEAGUE_ID, value);
			}
		}

		public int CityId
		{
			get
			{
				return GetIntField(CITY_ID);
			}
			set
			{
				SetField(CITY_ID, value);
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

		public int Reputation
		{
			//Max size 1023
			get
			{
				return GetIntField(REPUTATION);
			}
			set
			{
				SetField(REPUTATION, value);
			}
		}

		public int QBRating
		{
			get
			{
				return GetIntField(QB_RATING);
			}
			set
			{
				SetField(QB_RATING, value);
			}
		}

		public int RBRating
		{
			get
			{
				return GetIntField(RB_RATING);
			}
			set
			{
				SetField(RB_RATING, value);
			}
		}

		public int OLRating
		{
			get
			{
				return GetIntField(OL_RATING);
			}
			set
			{
				SetField(OL_RATING, value);
			}
		}

		public int WRRating
		{
			get
			{
				return GetIntField(WR_RATING);
			}
			set
			{
				SetField(WR_RATING, value);
			}
		}

		public int DLRating
		{
			get
			{
				return GetIntField(DL_RATING);
			}
			set
			{
				SetField(DL_RATING, value);
			}
		}

		public int LBRating
		{
			get
			{
				return GetIntField(LB_RATING);
			}
			set
			{
				SetField(LB_RATING, value);
			}
		}

		public int DBRating
		{
			get
			{
				return GetIntField(DB_RATING);
			}
			set
			{
				SetField(DB_RATING, value);
			}
		}

		public int SpecialTeamsRating
		{
			get
			{
				return GetIntField(ST_RATING);
			}
			set
			{
				SetField(ST_RATING, value);
			}
		}

		public int OffensiveRating
		{
			get
			{
				return GetIntField(OFFENSIVE_RATING);
			}
			set
			{
				SetField(OFFENSIVE_RATING, value);
			}
		}

		public int DefensiveRating
		{
			get
			{
				return GetIntField(DEFENSIVE_RATING);
			}
			set
			{
				SetField(DEFENSIVE_RATING, value);
			}
		}

		public int OffensivePlaybook
		{
			get
			{
				return GetIntField(OFFENSIVE_PLAYBOOK);
			}
			set
			{
				SetField(OFFENSIVE_PLAYBOOK, value);
			}
		}

		public int DefensivePlaybook
		{
			get
			{
				return GetIntField(DEFENSIVE_PLAYBOOK);
			}
			set
			{
				SetField(DEFENSIVE_PLAYBOOK, value);
			}
		}
	}
}
