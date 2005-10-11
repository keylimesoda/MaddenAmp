/******************************************************************************
 * Gommo's Madden Editor
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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
	/// <summary>
	/// Enumeration describing the coaching positions in this game
	/// </summary>
	public enum MaddenCoachPosition
	{
		HeadCoach = 0,
		OffensiveCoordinator,
		DefensiveCoordinator,
		SpecialTeams
	}

	public class CoachRecord : TableRecordModel
	{
		public const string NAME = "CLNA";
		public const string COACH_ID = "CCID";
		public const string TEAM_ID = "TGID";
		public const string AGE = "CAGE";
		public const string POSITION = "COPS";
		public const string SKIN_COLOR = "CSKI";

		public const string SALARY = "CSAL";
		public const string SUPERBOWL_LOSES = "CSBL";
		public const string SUPERBOWL_WINS = "CSBW";
		public const string PLAYOFF_LOSES = "CCPL";
		public const string PLAYFF_WINS = "CCPW";
		public const string WINNING_SEASONS = "CCWS";
		public const string CAREER_WINS = "CCWI";
		public const string CAREER_LOSES = "CCLO";
		public const string CAREER_TIES = "CCTI";

		public const string DEFENSE_TYPE = "CDTY";
		public const string DEFENSIVE_PLAYBOOK = "CDID";

		public const string OFF_STRAT = "COTR";
		public const string DEF_STRAT = "CDTR";
		public const string RUNNING_BACK_SUB = "CRBT";
		public const string OFF_AGGR = "COTA";
		public const string DEF_AGGR = "CDTA";
		
		public const string DB_RATING = "CRDB";
		public const string LB_RATING = "CRLB";
		public const string QB_RATING = "CRQB";
		public const string RB_RATING = "CRRB";
		public const string OL_RATING = "CROL";
		public const string DL_RATING = "CRDL";
		public const string WR_RATING = "CRWR";
		public const string KICK_RATING = "CRKS";
		public const string PUNT_RATING = "CRPS";

		public const string ETHICS = "CETH";
		public const string KNOWLEDGE = "CKNW";
		public const string MOTIVATION = "CMOT";
		public const string CHEMISTRY = "CCHM";

		public const string USER_CONTROLLED = "CFUC";

		public CoachRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public string Name
		{
			get
			{
				return GetStringField(NAME);
			}
			set
			{
				SetField(NAME, value);
			}
		}

		public int CoachId
		{
			get
			{
				return GetIntField(COACH_ID);
			}
			set
			{
				SetField(COACH_ID, value);
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

		public int Age
		{
			get
			{
				return GetIntField(AGE);
			}
			set
			{
				SetField(AGE, value);
			}
		}

		public int SkinColor
		{
			//The skin color is 0-4 but it is reversed
			get
			{
				return GetIntField(SKIN_COLOR);
			}
			set
			{
				SetField(SKIN_COLOR, value);
			}
		}

		public int Position
		{
			get
			{
				return GetIntField(POSITION);
			}
			set
			{
				SetField(POSITION, value);
			}
		}

		public int Salary
		{
			get
			{
				return GetIntField(SALARY);
			}
			set
			{
				SetField(SALARY, value);
			}
		}

		public int SuperBowlWins
		{
			get
			{
				return GetIntField(SUPERBOWL_WINS);
			}
			set
			{
				SetField(SUPERBOWL_WINS, value);
			}
		}

		public int SuperBowlLoses
		{
			get
			{
				return GetIntField(SUPERBOWL_LOSES);
			}
			set
			{
				SetField(SUPERBOWL_LOSES, value);
			}
		}

		public int PlayoffLoses
		{
			get
			{
				return GetIntField(PLAYOFF_LOSES);
			}
			set
			{
				SetField(PLAYOFF_LOSES, value);
			}
		}

		public int PlayoffWins
		{
			get
			{
				return GetIntField(PLAYFF_WINS);
			}
			set
			{
				SetField(PLAYFF_WINS, value);
			}
		}

		public int WinningSeasons
		{
			get
			{
				return GetIntField(WINNING_SEASONS);
			}
			set
			{
				SetField(WINNING_SEASONS, value);
			}
		}

		public int CareerWins
		{
			get
			{
				return GetIntField(CAREER_WINS);
			}
			set
			{
				SetField(CAREER_WINS, value);
			}
		}

		public int CareerLoses
		{
			get
			{
				return GetIntField(CAREER_LOSES);
			}
			set
			{
				SetField(CAREER_LOSES, value);
			}
		}

		public int CareerTies
		{
			get
			{
				return GetIntField(CAREER_TIES);
			}
			set
			{
				SetField(CAREER_TIES, value);
			}
		}

		public bool DefensiveAlignment
		{
			get
			{
				return (GetIntField(DEFENSE_TYPE) >= 50 ? true : false);
			}
			set
			{
				if (value)
				{
					SetField(DEFENSE_TYPE, 95);
				}
				else
				{
					SetField(DEFENSE_TYPE, 5);
				}
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
		
		public int OffensiveStrategy
		{
			get
			{
				return GetIntField(OFF_STRAT);
			}
			set
			{
				SetField(OFF_STRAT, value);
			}
		}

		public int DefensiveStrategy
		{
			get
			{
				return GetIntField(DEF_STRAT);
			}
			set
			{
				SetField(DEF_STRAT, value);
			}
		}

		public int DefensiveBackRating
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

		public int LinebackerRating
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

		public int QuarterbackRating
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

		public int RunningbackRating
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

		public int OffensiveLineRating
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

		public int DefensiveLineRating
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

		public int WideReceiverRating
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

		public int KickerRating
		{
			get
			{
				return GetIntField(KICK_RATING);
			}
			set
			{
				SetField(KICK_RATING, value);
			}
		}

		public int PuntRating
		{
			get
			{
				return GetIntField(PUNT_RATING);
			}
			set
			{
				SetField(PUNT_RATING, value);
			}
		}

		public int Ethics
		{
			get
			{
				return GetIntField(ETHICS);
			}
			set
			{
				SetField(ETHICS, value);
			}
		}

		public int Knowledge
		{
			get
			{
				return GetIntField(KNOWLEDGE);
			}
			set
			{
				SetField(KNOWLEDGE, value);
			}
		}

		public int Motivation
		{
			get
			{
				return GetIntField(MOTIVATION);
			}
			set
			{
				SetField(MOTIVATION, value);
			}
		}

		public int Chemistry
		{
			get
			{
				return GetIntField(CHEMISTRY);
			}
			set
			{
				SetField(CHEMISTRY, value);
			}
		}

		public int RunningBack2Sub
		{
			get
			{
				return GetIntField(RUNNING_BACK_SUB);
			}
			set
			{
				SetField(RUNNING_BACK_SUB, value);
			}
		}

		public int OffensiveAggression
		{
			get
			{
				return GetIntField(OFF_AGGR);
			}
			set
			{
				SetField(OFF_AGGR, value);
			}
		}

		public int DefensiveAggression
		{
			get
			{
				return GetIntField(DEF_AGGR);
			}
			set
			{
				SetField(DEF_AGGR, value);
			}
		}

		public bool HumanControlled
		{
			get
			{
				return (GetIntField(USER_CONTROLLED) == 1);
			}
			set
			{
				SetField(USER_CONTROLLED, Convert.ToInt32(value));
			}
		}
	}
}