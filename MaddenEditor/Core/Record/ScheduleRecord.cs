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
	public class ScheduleRecord : TableRecordModel
	{
		public const string HOME_TEAM_ID = "GHTG";
		public const string AWAY_TEAM_ID = "GATG";
		public const string GAME_NUMBER = "SGNM";
		public const string WEEK_NUMBER = "SEWN";
		// Gamestates
        // 0 - unscheduled playoff game
		// 1 - not played
		// 2 - away win
		// 3 - home win
		// 4 - tied
		public const string GAME_STATE = "GSTA";
		// Game day types
		// 0 - Thursday
		// 1 - Friday
		// 2 - Saturday
		// 3 - Sunday
		// 4 - Monday
		public const string GAME_DAY_TYPE = "GDAT";

		// Game Time of day
		public const string GAME_TIME_OF_DAY = "GTOD";

		public const string AWAY_SCORE = "GASC";
		public const string HOME_SCORE = "GHSC";
		public const string OVERTIME = "GFOT";
		public const string TIED_GAME = "GFTG";

		public const string WEIGHTING = "SEWT";

		public const string HUMAN_USER = "GFHU";

		// The List of Game Day Types
		public readonly static List<GenericRecord> gameDayTypeList;
		// The List of Game States
		public readonly static List<GenericRecord> gameStateList;
		// The List of Weightings
		public readonly static List<GenericRecord> gameWeightings;

		static ScheduleRecord() 
		{
			// Initialise the Game Day Types
			gameDayTypeList = new List<GenericRecord>();
			gameDayTypeList.Add(new GenericRecord("Thursday", 0));
			gameDayTypeList.Add(new GenericRecord("Friday", 1));
			gameDayTypeList.Add(new GenericRecord("Saturday", 2));
			gameDayTypeList.Add(new GenericRecord("Sunday", 3));
			gameDayTypeList.Add(new GenericRecord("Monday", 4));
			gameDayTypeList.Add(new GenericRecord("Unknown Day(5)", 5));

			// Initialise the Game States
			gameStateList = new List<GenericRecord>();
            gameStateList.Add(new GenericRecord("Unscheduled Playoff Game", 0));
			gameStateList.Add(new GenericRecord("Not Played", 1));
			gameStateList.Add(new GenericRecord("Away Team Win", 2));
			gameStateList.Add(new GenericRecord("Home Team Win", 3));
			gameStateList.Add(new GenericRecord("Tie", 4));

			// Initialise the Game Weightings
			gameWeightings = new List<GenericRecord>();
			gameWeightings.Add(new GenericRecord("None", 0));
			gameWeightings.Add(new GenericRecord("Season game", 25));
			gameWeightings.Add(new GenericRecord("Wildcard game", 50));
			gameWeightings.Add(new GenericRecord("Divisional game", 75));
			gameWeightings.Add(new GenericRecord("Championship game", 100));
			gameWeightings.Add(new GenericRecord("Superbowl game", 125));
			gameWeightings.Add(new GenericRecord("Probowl game", 150));

		}

		public ScheduleRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        // MADDEN DRAFT EDIT
        public int Winner()
        {
            // I key this off of scores rather than game states in
            // case EA changes the GAME_STATE to 'int' correspondence
            if (GetIntField(AWAY_SCORE) > GetIntField(HOME_SCORE))
            {
                return GetIntField(AWAY_TEAM_ID);
            }
            else if (GetIntField(AWAY_SCORE) < GetIntField(HOME_SCORE))
            {
                return GetIntField(HOME_TEAM_ID);
            }

            // Game resulted in a tie
            return -1;
        }

        public int Loser()
        {
            // I key this off of scores rather than game states in
            // case EA changes the GAME_STATE to 'int' correspondence
            if (GetIntField(AWAY_SCORE) < GetIntField(HOME_SCORE))
            {
                return GetIntField(AWAY_TEAM_ID);
            }
            else if (GetIntField(AWAY_SCORE) > GetIntField(HOME_SCORE))
            {
                return GetIntField(HOME_TEAM_ID);
            }

            // Game resulted in a tie
            return -1;
        }
        // MADDEN DRAFT EDIT
		public IList<GenericRecord> GameStates
		{
			get
			{
				return gameStateList;
			}
		}

		public IList<GenericRecord> GameDayTypes
		{
			get
			{
				return gameDayTypeList;
			}
		}

		public IList<GenericRecord> GameWeightings
		{
			get
			{
				return gameWeightings;
			}
		}

		public GenericRecord State
		{
			get
			{
				int val = GetIntField(GAME_STATE);
				foreach (GenericRecord rec in gameStateList)
				{
					if (rec.Id == val)
					{
						return rec;
					}
				}
				// If we get here throw an exception
				throw new ApplicationException("Game State " + val + " not found in List of Game States");
			}
			set
			{
				SetField(GAME_STATE, value.Id);
			}
		}

		public GenericRecord DayType
		{
			get
			{
				int val = GetIntField(GAME_DAY_TYPE);
				foreach (GenericRecord rec in gameDayTypeList)
				{
					if (rec.Id == val)
					{
						return rec;
					}
				}
				// If we get here throw an exception
				throw new ApplicationException("Game Day Type " + val + " not found in List of Game Day Types");
			}
			set
			{
				SetField(GAME_DAY_TYPE, value.Id);
			}
		}

		public GenericRecord Weighting
		{
			get
			{
				int val = GetIntField(WEIGHTING);
				foreach (GenericRecord rec in gameWeightings)
				{
					if (rec.Id == val)
					{
						return rec;
					}
				}
				// If we get here throw an exception
				throw new ApplicationException("Weighting Type " + val + " not found in List of Weightings");
			}
			set
			{
				SetField(WEIGHTING, value.Id);
			}
		}

		public TeamRecord AwayTeam
		{
			get
			{
				return editorModel.TeamModel.GetTeamRecord(GetIntField(AWAY_TEAM_ID));
			}
			set
			{
				SetField(AWAY_TEAM_ID, value.TeamId);
			}
		}

		public TeamRecord HomeTeam
		{
			get
			{
				return editorModel.TeamModel.GetTeamRecord(GetIntField(HOME_TEAM_ID));
			}
			set
			{
				SetField(HOME_TEAM_ID, value.TeamId);
			}
		}

		public int AwayTeamScore
		{
			get
			{
				return GetIntField(AWAY_SCORE);
			}
			set
			{
				SetField(AWAY_SCORE, value);
			}
		}

		public int HomeTeamScore
		{
			get
			{
				return GetIntField(HOME_SCORE);
			}
			set
			{
				SetField(HOME_SCORE, value);
			}
		}

		public bool GameTied
		{
			get
			{
				return (GetIntField(TIED_GAME) == 1);
			}
			set
			{
				SetField(TIED_GAME, Convert.ToInt32(value));
			}
		}

		public bool OverTime
		{
			get
			{
				return (GetIntField(OVERTIME) == 1);
			}
			set
			{
				SetField(OVERTIME, Convert.ToInt32(value));
			}
		}

		public bool HumanControlled
		{
			get
			{
				return (GetIntField(HUMAN_USER) == 1);
			}
			set
			{
				SetField(HUMAN_USER, Convert.ToInt32(value));
			}
		}

		public int GameNumber
		{
			get
			{
				return GetIntField(GAME_NUMBER);
			}
			set
			{
				SetField(GAME_NUMBER, value);
			}
		}

		public int WeekNumber
		{
			get
			{
				return GetIntField(WEEK_NUMBER);
			}
			set
			{
				SetField(WEEK_NUMBER, value);
			}
		}
	}
}
