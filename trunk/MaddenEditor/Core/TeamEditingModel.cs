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

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class TeamEditingModel
	{
        
        public const int RETIRED = 1014;
        public const int NO_TEAM_ID = 1023;
		public const int PROBOWL_TEAM_AFC = 1010; //??
		public const int PROBOWL_TEAM_NFC = 1011; //??

		/** Collection of team names hashed by teamid */
		private Dictionary<int, string> teamNameList = null;
		/** View to the TeamRecords indexed by Teamid */
		private SortedList<int, TeamRecord> teamRecords = null;
		/** View to the OwnedTeam records */
		private List<OwnerRecord> teamOwnerRecords = null;
		/** Reference to our EditorModel */
		private EditorModel model = null;
		/** Current team record number */
		private int currentTeamRecord = 0;
		/** Filters */
		private int currentConferenceFilterId = -1;
		private int currentDivisionFilterId = -1;
		private int currentLeagueFilterId = -1;
		/** Lists of hardcoded values */
		private IList<GenericRecord> divisionList = null;
		private IList<GenericRecord> conferenceList = null;
		private IList<GenericRecord> leagueList = null;
		private IList<GenericRecord> offensivePlayBookList = null;
		private IList<GenericRecord> defensivePlayBookList = null;
		private IList<GenericRecord> teamTypeList = null;
		/** Add the city list here for the time being */
		private IList<GenericRecord> cityList = null;
		/** Editing model for team uniforms */
		private TeamUniformModel teamUniformModel = null;

		public TeamEditingModel(EditorModel model)
		{
			this.model = model;

			teamUniformModel = new TeamUniformModel(model);

			//Initialise the GenericRecord lists
			teamTypeList = new List<GenericRecord>();

			teamTypeList.Add(new GenericRecord("Probowl", 16));
			teamTypeList.Add(new GenericRecord("NFL Europe", 7));
			teamTypeList.Add(new GenericRecord("Created", 5));
			teamTypeList.Add(new GenericRecord("Historical", 2));
			teamTypeList.Add(new GenericRecord("Free Agents", 1));
			teamTypeList.Add(new GenericRecord("NFL", 0));

			divisionList = new List<GenericRecord>();
			divisionList.Add(new GenericRecord("N/A", 31));
			divisionList.Add(new GenericRecord("AFC North", 0));
			divisionList.Add(new GenericRecord("AFC South", 1));
			divisionList.Add(new GenericRecord("AFC East", 2));
			divisionList.Add(new GenericRecord("AFC West", 3));
			divisionList.Add(new GenericRecord("NFC North", 4));
			divisionList.Add(new GenericRecord("NFC South", 5));
			divisionList.Add(new GenericRecord("NFC East", 6));
			divisionList.Add(new GenericRecord("NFC West", 7));
			divisionList.Add(new GenericRecord("NFL Euro", 8));

			conferenceList = new List<GenericRecord>();
			conferenceList.Add(new GenericRecord("N/A", 3));
			conferenceList.Add(new GenericRecord("NFL Euro", 2));
			conferenceList.Add(new GenericRecord("NFC", 1));
			conferenceList.Add(new GenericRecord("AFC", 0));

			leagueList = new List<GenericRecord>();
			leagueList.Add(new GenericRecord("N/A", 3));
			leagueList.Add(new GenericRecord("NFL Euro", 1));
			leagueList.Add(new GenericRecord("NFL", 0));

			cityList = new List<GenericRecord>();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.CITY_TABLE].GetRecords())
			{
				cityList.Add(new GenericRecord(((CityRecord)rec).CityName, ((CityRecord)rec).CityId));
			}

			offensivePlayBookList = new List<GenericRecord>();
			offensivePlayBookList.Add(new GenericRecord("CHI-L.Smith", 0));
			offensivePlayBookList.Add(new GenericRecord("CIN-M.Lewis", 1));
			offensivePlayBookList.Add(new GenericRecord("BUF-M.Mularkey", 2));
			offensivePlayBookList.Add(new GenericRecord("DEN-M.Shanahan", 3));
			offensivePlayBookList.Add(new GenericRecord("CLE-B.Davis", 4));
			offensivePlayBookList.Add(new GenericRecord("TB-J.Gruden", 5));
			offensivePlayBookList.Add(new GenericRecord("ARI-D.Green", 6));
			offensivePlayBookList.Add(new GenericRecord("SD-Schottenheimer", 7));
			offensivePlayBookList.Add(new GenericRecord("KC-D.Vermeil", 8));
			offensivePlayBookList.Add(new GenericRecord("IND-T.Dungy", 9));
			offensivePlayBookList.Add(new GenericRecord("DAL-M.Carthon", 10));
			offensivePlayBookList.Add(new GenericRecord("MIA-D.Wannstedt", 11));
			offensivePlayBookList.Add(new GenericRecord("PHI-A.Reid", 12));
			offensivePlayBookList.Add(new GenericRecord("ATL-J.Mora Jr", 13));
			offensivePlayBookList.Add(new GenericRecord("SF-D.Erickson", 14));
			offensivePlayBookList.Add(new GenericRecord("NYG-T.Coughlin", 15));
			offensivePlayBookList.Add(new GenericRecord("JAX-J.Del Rio", 16));
			offensivePlayBookList.Add(new GenericRecord("NYJ-H.Edwards", 17));
			offensivePlayBookList.Add(new GenericRecord("DET-S.Mariucci", 18));
			offensivePlayBookList.Add(new GenericRecord("GB-M.Sherman", 19));
			offensivePlayBookList.Add(new GenericRecord("CAR-J.Fox", 20));
			offensivePlayBookList.Add(new GenericRecord("NE-B.Belichick", 21));
			offensivePlayBookList.Add(new GenericRecord("OAK-N.Turner", 22));
			offensivePlayBookList.Add(new GenericRecord("STL-M.Martz", 23));
			offensivePlayBookList.Add(new GenericRecord("BAL-B.Billick", 24));
			offensivePlayBookList.Add(new GenericRecord("WAS-J.Gibbs", 25));
			offensivePlayBookList.Add(new GenericRecord("NO-J.Hasslet", 26));
			offensivePlayBookList.Add(new GenericRecord("SEA-M.Holmgren", 27));
			offensivePlayBookList.Add(new GenericRecord("PIT-B.Cowher", 28));
			offensivePlayBookList.Add(new GenericRecord("TEN-J.Fisher", 29));
			offensivePlayBookList.Add(new GenericRecord("MIN-M.Tice", 30));
			offensivePlayBookList.Add(new GenericRecord("HOU-D.Capers", 31));
			offensivePlayBookList.Add(new GenericRecord("Balanced", 32));
			offensivePlayBookList.Add(new GenericRecord("Pass Balanced", 33));
			offensivePlayBookList.Add(new GenericRecord("Run Balanced", 34));
			offensivePlayBookList.Add(new GenericRecord("Run Heavy", 35));
			offensivePlayBookList.Add(new GenericRecord("West Coast", 36));
			offensivePlayBookList.Add(new GenericRecord("Run'n'Gun", 37));
			
			defensivePlayBookList = new List<GenericRecord>();
			defensivePlayBookList.Add(new GenericRecord("4-3", 0));
			defensivePlayBookList.Add(new GenericRecord("3-4", 1));
			defensivePlayBookList.Add(new GenericRecord("4-6", 2));
			defensivePlayBookList.Add(new GenericRecord("Cover 2", 3));
			defensivePlayBookList.Add(new GenericRecord("Balanced D", 4));
			defensivePlayBookList.Add(new GenericRecord("QB Contain", 5));
			defensivePlayBookList.Add(new GenericRecord("6", 6));
			defensivePlayBookList.Add(new GenericRecord("7", 7));
			defensivePlayBookList.Add(new GenericRecord("8", 8));
			defensivePlayBookList.Add(new GenericRecord("9", 9));
			defensivePlayBookList.Add(new GenericRecord("10", 10));
			defensivePlayBookList.Add(new GenericRecord("11", 11));
			defensivePlayBookList.Add(new GenericRecord("12", 12));
			defensivePlayBookList.Add(new GenericRecord("13", 13));
			defensivePlayBookList.Add(new GenericRecord("14", 14));
			defensivePlayBookList.Add(new GenericRecord("15", 15));
			defensivePlayBookList.Add(new GenericRecord("16", 16));
			defensivePlayBookList.Add(new GenericRecord("17", 17));
			defensivePlayBookList.Add(new GenericRecord("18", 18));
			defensivePlayBookList.Add(new GenericRecord("19", 19));
			defensivePlayBookList.Add(new GenericRecord("20", 20));
			defensivePlayBookList.Add(new GenericRecord("21", 21));
			defensivePlayBookList.Add(new GenericRecord("22", 22));
			defensivePlayBookList.Add(new GenericRecord("23", 23));
			defensivePlayBookList.Add(new GenericRecord("24", 24));
			defensivePlayBookList.Add(new GenericRecord("25", 25));
			defensivePlayBookList.Add(new GenericRecord("26", 26));
			defensivePlayBookList.Add(new GenericRecord("27", 27));
			defensivePlayBookList.Add(new GenericRecord("28", 28));
			defensivePlayBookList.Add(new GenericRecord("29", 29));
			defensivePlayBookList.Add(new GenericRecord("30", 30));
			defensivePlayBookList.Add(new GenericRecord("31", 31));
			defensivePlayBookList.Add(new GenericRecord("32", 32));
			defensivePlayBookList.Add(new GenericRecord("33", 33));
			defensivePlayBookList.Add(new GenericRecord("34", 34));
			defensivePlayBookList.Add(new GenericRecord("35", 35));
			defensivePlayBookList.Add(new GenericRecord("36", 36));
			defensivePlayBookList.Add(new GenericRecord("37", 37));
			defensivePlayBookList.Add(new GenericRecord("38", 38));
			defensivePlayBookList.Add(new GenericRecord("39", 39));
			defensivePlayBookList.Add(new GenericRecord("40", 40));
			
		}

		// MADDEN DRAFT EDIT
		public void ComputeCONs()
		{
			List<TeamRecord> teamsTemp = new List<TeamRecord>();
			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				TeamRecord tr = (TeamRecord)record;
				teamsTemp.Add(tr);
			}

			teamsTemp = SortByOverall(teamsTemp);

			List<int> conCutoffs = new List<int>();
			conCutoffs.Add(teamsTemp[28].GetOverall());
			conCutoffs.Add(teamsTemp[22].GetOverall());
			conCutoffs.Add(teamsTemp[14].GetOverall());
			conCutoffs.Add(teamsTemp[6].GetOverall());

			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				TeamRecord tr = (TeamRecord)record;

				if (tr.GetOverall() < conCutoffs[0])
				{
					tr.CON = 1;
				}
				else if (tr.GetOverall() < conCutoffs[1])
				{
					tr.CON = 2;
				}
				else if (tr.GetOverall() < conCutoffs[2])
				{
					tr.CON = 3;
				}
				else if (tr.GetOverall() < conCutoffs[3])
				{
					tr.CON = 4;
				}
				else
				{
					tr.CON = 5;
				}

				// Should add code here that increases CON if they've got an old QB
				// who's above 90 or so and about to retire.
			}
		}

		// Another inefficient sort.  But damn it, I can't figure out this IComparer shit.
		// So, again, we're only sorting 32 items, so N^2 isn't a big deal...
		private List<TeamRecord> SortByOverall(List<TeamRecord> records)
		{
			List<TeamRecord> newList = new List<TeamRecord>();

			while (records.Count > 0)
			{
				int bestOverall = 0;
				int bestTeamIndex = -1;

				for (int i = 0; i < records.Count; i++)
				{
					if (records[i].TeamId >= 32)
					{
						records.RemoveAt(i);
						i--;
						continue;
					}

					if (records[i].GetOverall() > bestOverall)
					{
						bestOverall = records[i].GetOverall();
						bestTeamIndex = i;
					}
				}

				newList.Add(records[bestTeamIndex]);
				records.RemoveAt(bestTeamIndex);
			}

			return newList;
		}

		public void CalculateWins()
		{
			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				((TeamRecord)record).ComputeWins(model);
			}

		}

		public void CalculateStrengthOfSchedule()
		{
			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				((TeamRecord)record).ComputeStrengthOfSchedule(model);
			}
		}

		public SortedList<int, TeamRecord> GetTeamRecords()
		{

			if (teamRecords == null)
			{
				CreateTeamRecords();
			}

			return teamRecords;
		}


		// MADDEN DRAFT EDIT

		public void SetConferenceFilter(int id)
		{
			currentConferenceFilterId = id;
		}

		public void RemoveConferenceFilter()
		{
			currentConferenceFilterId = -1;
		}

		public void SetDivisionFilter(int id)
		{
			currentDivisionFilterId = id;
		}

		public void RemoveDivisionFilter()
		{
			currentDivisionFilterId = -1;
		}

		public void SetLeagueFilter(int id)
		{
			currentLeagueFilterId = id;
		}

		public void RemoveLeagueFilter()
		{
			currentLeagueFilterId = -1;
		}

		public IList<GenericRecord> DivisionList
		{
			get
			{
				return divisionList;
			}
		}

		public IList<GenericRecord> LeagueList
		{
			get
			{
				return leagueList;
			}
		}

		public IList<GenericRecord> ConferenceList
		{
			get
			{
				return conferenceList;
			}
		}

		public IList<GenericRecord> CityList
		{
			get
			{
				return cityList;
			}
		}

		public IList<GenericRecord> OffensivePlaybookList
		{
			get
			{
				return offensivePlayBookList;
			}
		}

		public IList<GenericRecord> DefensivePlaybookList
		{
			get
			{
				return defensivePlayBookList;
			}
		}

		public TeamRecord CurrentTeamRecord
		{
			get
			{
				return (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord(currentTeamRecord);                
			}
			set
			{
				TeamRecord curr = value;
				//need to set currentTeamRecord to the correct index
				int index = 0;
				foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
				{
					if (curr == rec)
					{
						currentTeamRecord = index;
						break;
					}

					index++;
				}
			}
		}

		public TeamRecord GetNextTeamRecord()
		{
			TeamRecord record = null;

			int startingindex = currentTeamRecord;
			while (true)
			{
				currentTeamRecord++;

				if (currentTeamRecord == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentTeamRecord >= model.TableModels[EditorModel.TEAM_TABLE].RecordCount)
				{
					currentTeamRecord = -1;
					continue;
				}

				record = (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord(currentTeamRecord);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentConferenceFilterId != -1)
				{
					if (record.ConferenceId != currentConferenceFilterId)
					{
						continue;
					}
				}
				if (currentDivisionFilterId != -1)
				{
					if (record.DivisionId != currentDivisionFilterId)
					{
						continue;
					}
				}
				if (currentLeagueFilterId != -1)
				{
					if (record.LeagueId != currentLeagueFilterId)
					{
						continue;
					}
				}

				//Found one
				break;
			}

			return record;
		}

		public TeamRecord GetPreviousTeamRecord()
		{
			TeamRecord record = null;

			int startingindex = currentTeamRecord;
			while (true)
			{
				currentTeamRecord--;
				if (currentTeamRecord == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentTeamRecord < 0)
				{
					currentTeamRecord = model.TableModels[EditorModel.TEAM_TABLE].RecordCount;
					continue;
				}

				record = (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord(currentTeamRecord);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}
				if (currentConferenceFilterId != -1)
				{
					if (record.ConferenceId != currentConferenceFilterId)
					{
						continue;
					}
				}
				if (currentDivisionFilterId != -1)
				{
					if (record.DivisionId != currentDivisionFilterId)
					{
						continue;
					}
				}
				if (currentLeagueFilterId != -1)
				{
					if (record.LeagueId != currentLeagueFilterId)
					{
						continue;
					}
				}
				
				//Found one
				break;
			}

			return record;
		}

		private void CreateTeamNameList()
		{
			teamNameList = new Dictionary<int, string>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				TeamRecord teamRecord = (TeamRecord)record;
				teamNameList.Add(teamRecord.TeamId, teamRecord.Name);                
			}
            
            // adding Retired for a team name for players
            teamNameList.Add(EditorModel.RETIRED_TEAM_ID, EditorModel.RETIRED);
		}

		private void CreateTeamRecords()
		{
			teamRecords = new SortedList<int, TeamRecord>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				TeamRecord tr = (TeamRecord)record;                
				teamRecords.Add(tr.TeamId, tr);
			}
		}

		public ICollection<TeamRecord> GetTeams()
		{
			if (teamRecords == null)
			{
				CreateTeamRecords();
			}

			return teamRecords.Values;
		}

		public TeamRecord GetTeamRecord(int teamId)
		{
			if (teamRecords == null)
			{
				CreateTeamRecords();
			}

			if (teamRecords.ContainsKey(teamId))
			{
				return teamRecords[teamId];
			}
			
			return null;
		}

		public string GetTeamNameFromTeamId(int teamid)
		{
			if (teamNameList == null)
			{
				CreateTeamNameList();
			}
			if (teamNameList.ContainsKey(teamid))
				return teamNameList[teamid];
			
            else
				return EditorModel.UNKNOWN_TEAM_NAME;
		}

		public ICollection<OwnerRecord> GetTeamRecordsInOwnerTable()
		{
			if (teamOwnerRecords == null)
			{
				teamOwnerRecords = new List<OwnerRecord>();

				foreach (TableRecordModel record in model.TableModels[EditorModel.OWNER_TABLE].GetRecords())
				{
					teamOwnerRecords.Add((OwnerRecord)record);
				}
			}

			return teamOwnerRecords;
		}

		public TeamUniformModel TeamUniformModel
		{
			get
			{
				return teamUniformModel;
			}
		}

		public int GetTeamIdFromTeamName(string teamName)
		{
			if (teamNameList == null)
			{
				CreateTeamNameList();
			}

			if (teamNameList.ContainsValue(teamName))
			{
				//Theres got to be a better way to do this
				int i = 0;
				foreach (string team in teamNameList.Values)
				{
					if (team.Equals(teamName))
					{
						break;
					}
					i++;
				}

				//Now if we iterate through teamName list Key list to 'i' then
				//we have the Team id
				int j = 0;
				int id = 0;
				foreach (int key in teamNameList.Keys)
				{
					if (j == i)
					{
						id = key;
						break;
					}
					j++;
				}

				return id;
			}
			else if (teamName == ScheduleEditingModel.UNDECIDED_TEAM)
			{
				//we'll return NO_TEAM_ID if the team is an undecided team
				return TeamEditingModel.NO_TEAM_ID;
			}
			else
			{
				throw new ApplicationException("Error getting TeamID for team name " + teamName);
			}
		}
	}
}
