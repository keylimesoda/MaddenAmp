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
	public class TeamRecord : TableRecordModel
	{
		public const string TEAM_NAME = "TDNA";
		public const string TEAM_LONG_NAME = "TLNA";
		public const string TEAM_SHORT_NAME = "TSNA";
		public const string TEAM_NICK_NAME = "TMNC";
		public const string TEAM_ID = "TGID";
		public const string REPUTATION = "TREP";

		//Colours
		public const string SECONDARY_BLUE = "TB2B";
		public const string PRIMARY_BLUE = "TBCB";
		public const string SECONDARY_GREEN = "TB2G";
		public const string PRIMARY_GREEN = "TBCG";
		public const string SECONDARY_RED = "TB2R";
		public const string PRIMARY_RED = "TBCR";
		public const string CUSTOM_ART_FILE = "CART";

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
		public const string OVERALL_RATING = "TROV";

		public const string OFFENSIVE_PLAYBOOK = "TOPB";
		public const string DEFENSIVE_PLAYBOOK = "TDPB";

		public const string DIVISION_ORDER = "DISN";
		public const string TEAM_ROSTER_ORDER = "TORD";
		public const string TEAM_ORDER = "TDRI";
		public const string TEAM_TYPE = "TTYP";

		public const string TEAM_SHOE_COLOR = "TSHO";

		public const string TEAM_RIVAL_1 = "TRV1";
		public const string TEAM_RIVAL_2 = "TRV2";
		public const string TEAM_RIVAL_3 = "TRV3";
        // MADDEN DRAFT EDIT
        private int wins;
        private int playoffExit;
        private int strengthOfSchedule;
        private int effectiveSOS;
        private int con;
        // MADDEN DRAFT EDIT

		public TeamRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}
        // MADDEN DRAFT EDIT
        public enum Defense {
            Front43=0,
            Front34,
            Cover2
        }

        public int DefensiveSystem
        {
            get {
                if (DefensivePlaybook == 1) {
                    return (int)Defense.Front34;
                } else if (DefensivePlaybook == 3) {
                    return (int)Defense.Cover2;
                } else {
                    return (int)Defense.Front43;
                }
            }
        }
            
        public int CON
        {
            get
            {
                return con;
            }
            set
            {
                con = value;
            }
        }

        public int Wins
        {
            get
            {
                return wins;
            }
        }

        public int EffectiveSOS
        {
            get
            {
                return effectiveSOS;
            }
        }

        public int StrengthOfSchedule
        {
            get
            {
                return strengthOfSchedule;
            }
        }

        public int PlayoffExit
        {
            get
            {
                return playoffExit;
            }
            set
            {
                playoffExit = value;
            }
        }

        // In principle I'd prefer a better way of calculating OVR, but in the 
        // interest of saving time, we'll go with this for now.
        public int GetOverall()
        {
            return OverallRating;
        }

		public void SimulateMinicamp()
		{
			bool foundWR;
			bool foundDB;
			bool foundLB;
			bool foundDL;
			bool foundHB;

			List<int> skip = new List<int>();
			PlayerRecord player;
			Random rand = new Random();
			int toAdd;

			// Pocket Presence
			player = bestEORInRange((int)MaddenPositions.QB, (int)MaddenPositions.QB, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double AWRProb = 99 - player.Awareness;
				double THAProb = 99 - player.ThrowAccuracy;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < AWRProb / (AWRProb + THAProb))
					{
						player.Awareness = player.Awareness + 1;
					}
					else
					{
						player.ThrowAccuracy = player.ThrowAccuracy + 1;
					}
				}
			}

			// Precision Passing
			player = bestEORInRange((int)MaddenPositions.QB, (int)MaddenPositions.QB, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double THPProb = 99 - player.ThrowPower;
				double THAProb = 99 - player.ThrowAccuracy;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < THPProb / (THPProb + THAProb))
					{
						player.ThrowPower = player.ThrowPower + 1;
					}
					else
					{
						player.ThrowAccuracy = player.ThrowAccuracy + 1;
					}
				}
			}

			// Field Goal
			player = bestEORInRange((int)MaddenPositions.K, (int)MaddenPositions.K, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double KACProb = 99 - player.KickAccuracy;
				double KPRProb = 99 - player.KickPower;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < KACProb / (KACProb + KPRProb))
					{
						player.KickAccuracy = player.KickAccuracy + 1;
					}
					else
					{
						player.KickPower = player.KickPower + 1;
					}
				}
			}

			// Field Goal
			player = bestEORInRange((int)MaddenPositions.P, (int)MaddenPositions.P, skip);

			if (player != null)
			{
				skip.Add(player.PlayerId);

				toAdd = pointsToAdd(true, rand);

				double KACProb = 99 - player.KickAccuracy;
				double KPRProb = 99 - player.KickPower;

				for (int i = 0; i < toAdd; i++)
				{
					if (rand.NextDouble() < KACProb / (KACProb + KPRProb))
					{
						player.KickAccuracy = player.KickAccuracy + 1;
					}
					else
					{
						player.KickPower = player.KickPower + 1;
					}
				}
			}

			// After this we've got multiple 
		}

		private int pointsToAdd(bool native, Random rand) 
		{
			double test = rand.NextDouble();

			if (native)
			{
				if (test < 0.1)
				{
					return 2;
				}
				else if (test < 0.5)
				{
					return 4;
				}
				else if (test < 0.9)
				{
					return 5;
				}
				else
				{
					return 7;
				}
			}
			else
			{
				if (test < 0.4)
				{
					return 2;
				}
				else if (test < 0.8)
				{
					return 4;
				}
				else if (test < 0.95)
				{
					return 5;
				}
				else
				{
					return 7;
				}
			}
		}

		private PlayerRecord bestEORInRange(int StartPosition, int EndPosition, List<int> skip)
		{
			PlayerRecord bestPlayer = null;
			double bestEOR = 0;
			LocalMath lmath = new LocalMath(editorModel.FileVersion);

			foreach (TableRecordModel record in editorModel.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				PlayerRecord player = (PlayerRecord)record;

				if (player.TeamId == TEAM_ID && player.PositionId >= StartPosition && player.PositionId <= EndPosition
					&& !skip.Contains(player.PlayerId) && player.YearsPro < 3 && (player.Overall + lmath.pointboost(player, CON, 35)) > bestEOR)
				{
					bestPlayer = player;
					bestEOR = player.Overall + lmath.pointboost(player, CON, 35);
				}
			}

			return bestPlayer;
		}

        public void ComputeWins(EditorModel model)
        {
            wins = 0;
            playoffExit = 0;

            foreach (TableRecordModel record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                ScheduleRecord rec = (ScheduleRecord)record;

                if (rec.HomeTeam.TeamId == TeamId || rec.AwayTeam.TeamId == TeamId)
                {
                    if (rec.WeekNumber < 17)
                    {
                        // If it's a regular season week, then add to their number of wins
                        if (rec.Winner() == TeamId)
                        {
                            wins++;
                        }
                    }
                    else if (rec.WeekNumber < 20 && rec.Loser() == TeamId)
                    {
                        // If it's a non-Superbowl playoff game, record the exit value
                        playoffExit = rec.WeekNumber - 16;
                    }
                    else if (rec.WeekNumber == 20)
                    {
                        if (rec.Loser() == TeamId)
                        {
                            playoffExit = 4;
                        }
                        else if (rec.Winner() == TeamId)
                        {
                            playoffExit = 5;
                        }
                    }
                }
            }
        }

        public void ComputeStrengthOfSchedule(EditorModel model)
        {
            strengthOfSchedule = 0;

            foreach (TableRecordModel record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                ScheduleRecord rec = (ScheduleRecord)record;

                if (rec.WeekNumber < 17)
                {

                    if (rec.HomeTeam.TeamId == TeamId)
                    {
                        strengthOfSchedule += rec.AwayTeam.Wins;
                    }
                    else if (rec.AwayTeam.TeamId == TeamId)
                    {
                        strengthOfSchedule += rec.HomeTeam.Wins;
                    }
                }
            }

            effectiveSOS = 1000 * playoffExit + strengthOfSchedule;
        }

        // MADDEN DRAFT EDIT

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

		public int OverallRating
		{
			get
			{
				return GetIntField(OVERALL_RATING);
			}
			set
			{
				SetField(OVERALL_RATING, value);
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

		public int ShoeColor
		{
			get
			{
				return GetIntField(TEAM_SHOE_COLOR);
			}
			set
			{
				SetField(TEAM_SHOE_COLOR, value);
			}
		}

		public System.Drawing.Color PrimaryColor
		{
			get
			{
				return System.Drawing.Color.FromArgb(GetIntField(PRIMARY_RED), GetIntField(PRIMARY_GREEN), GetIntField(PRIMARY_BLUE));
			}
			set
			{
				SetField(PRIMARY_RED, value.R);
				SetField(PRIMARY_GREEN, value.G);
				SetField(PRIMARY_BLUE, value.B);
			}
		}

		public System.Drawing.Color SecondaryColor
		{
			get
			{
				return System.Drawing.Color.FromArgb(GetIntField(SECONDARY_RED), GetIntField(SECONDARY_GREEN), GetIntField(SECONDARY_BLUE));
			}
			set
			{
				SetField(SECONDARY_RED, value.R);
				SetField(SECONDARY_GREEN, value.G);
				SetField(SECONDARY_BLUE, value.B);
			}
		}

		public int TeamRival1
		{
			get
			{
				return GetIntField(TEAM_RIVAL_1);
			}
			set
			{
				SetField(TEAM_RIVAL_1, value);
			}
		}

		public int TeamRival2
		{
			get
			{
				return GetIntField(TEAM_RIVAL_2);
			}
			set
			{
				SetField(TEAM_RIVAL_2, value);
			}
		}

		public int TeamRival3
		{
			get
			{
				return GetIntField(TEAM_RIVAL_3);
			}
			set
			{
				SetField(TEAM_RIVAL_3, value);
			}
		}

		public int TeamType
		{
			get
			{
				return GetIntField(TEAM_TYPE);
			}
			set
			{
				SetField(TEAM_TYPE, value);
			}
		}

		public int DivisionOrder
		{
			get
			{
				return GetIntField(DIVISION_ORDER);
			}
			set
			{
				SetField(DIVISION_ORDER, value);
			}
		}

		public int TeamOrder
		{
			get
			{
				return GetIntField(TEAM_ORDER);
			}
			set
			{
				SetField(TEAM_ORDER, value);
			}
		}

		public int TeamRosterOrder
		{
			get
			{
				return GetIntField(TEAM_ROSTER_ORDER);
			}
			set
			{
				SetField(TEAM_ROSTER_ORDER, value);
			}
		}
	}
}
