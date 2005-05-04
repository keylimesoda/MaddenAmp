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
	public class CoachRecord : TableRecordModel
	{
		public const string NAME = "CLNA";
		public const string COACH_ID = "CCID";
		public const string TEAM_ID = "TGID";
		public const string AGE = "CAGE";
		public const string POSITION = "COPS";

		public const string SALARY = "CSAL";
		public const string SUPERBOWL_LOSES = "CSBL";
		public const string SUPERBOWL_WINS = "CSBW";

		public const string OFF_RATING = "COFF";
		public const string DEF_RATING = "CDEF";
		public const string DB_RATING = "CRDB";
		public const string LB_RATING = "CRLB";
		public const string QB_RATING = "CRQB";
		public const string RB_RATING = "CRRB";
		public const string OL_RATING = "CROL";
		public const string WR_RATING = "CRWR";
		public const string KICK_RATING = "CRKS";
		public const string PUNT_RATING = "CRPS";

		public const string ETHICS = "CETH";
		public const string KNOWLEDGE = "CKNW";
		public const string MOTIVATION = "CMOT";
		

		public CoachRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public string Name
		{
			get
			{
				return stringFields[NAME];
			}
			set
			{
				SetFieldWithBackup(NAME, value);
			}
		}

		public int CoachId
		{
			get
			{
				return intFields[COACH_ID];
			}
			set
			{
				SetFieldWithBackup(COACH_ID, value);
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

		public int Age
		{
			get
			{
				return intFields[AGE];
			}
			set
			{
				SetFieldWithBackup(AGE, value);
			}
		}

		public int Position
		{
			get
			{
				return intFields[POSITION];
			}
			set
			{
				SetFieldWithBackup(POSITION, value);
			}
		}

		public int Salary
		{
			get
			{
				return intFields[SALARY];
			}
			set
			{
				SetFieldWithBackup(SALARY, value);
			}
		}

		public int SuperBowlWins
		{
			get
			{
				return intFields[SUPERBOWL_WINS];
			}
			set
			{
				SetFieldWithBackup(SUPERBOWL_WINS, value);
			}
		}

		public int SuperBowlLoses
		{
			get
			{
				return intFields[SUPERBOWL_LOSES];
			}
			set
			{
				SetFieldWithBackup(SUPERBOWL_LOSES, value);
			}
		}

		public int OffensiveRating
		{
			get
			{
				return intFields[OFF_RATING];
			}
			set
			{
				SetFieldWithBackup(OFF_RATING, value);
			}
		}

		public int DefensiveRating
		{
			get
			{
				return intFields[DEF_RATING];
			}
			set
			{
				SetFieldWithBackup(DEF_RATING, value);
			}
		}

		public int DefensiveBackRating
		{
			get
			{
				return intFields[DB_RATING];
			}
			set
			{
				SetFieldWithBackup(DB_RATING, value);
			}
		}

		public int LinebackerRating
		{
			get
			{
				return intFields[LB_RATING];
			}
			set
			{
				SetFieldWithBackup(LB_RATING, value);
			}
		}

		public int QuarterbackRating
		{
			get
			{
				return intFields[QB_RATING];
			}
			set
			{
				SetFieldWithBackup(QB_RATING, value);
			}
		}

		public int RunningbackRating
		{
			get
			{
				return intFields[RB_RATING];
			}
			set
			{
				SetFieldWithBackup(RB_RATING, value);
			}
		}

		public int OffensiveLineRating
		{
			get
			{
				return intFields[OL_RATING];
			}
			set
			{
				SetFieldWithBackup(OL_RATING, value);
			}
		}

		public int WideReceiverRating
		{
			get
			{
				return intFields[WR_RATING];
			}
			set
			{
				SetFieldWithBackup(WR_RATING, value);
			}
		}

		public int KickerRating
		{
			get
			{
				return intFields[KICK_RATING];
			}
			set
			{
				SetFieldWithBackup(KICK_RATING, value);
			}
		}

		public int PuntRating
		{
			get
			{
				return intFields[PUNT_RATING];
			}
			set
			{
				SetFieldWithBackup(PUNT_RATING, value);
			}
		}

		public int Ethics
		{
			get
			{
				return intFields[ETHICS];
			}
			set
			{
				SetFieldWithBackup(ETHICS, value);
			}
		}

		public int Knowledge
		{
			get
			{
				return intFields[KNOWLEDGE];
			}
			set
			{
				SetFieldWithBackup(KNOWLEDGE, value);
			}
		}

		public int Motivation
		{
			get
			{
				return intFields[MOTIVATION];
			}
			set
			{
				SetFieldWithBackup(MOTIVATION, value);
			}
		}
	}
}