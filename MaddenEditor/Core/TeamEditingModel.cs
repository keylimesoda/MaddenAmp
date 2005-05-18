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
		/** Collection of team names hashed by teamid */
		private Dictionary<int, string> teamNameList = null;
		/** View to the TeamRecords indexed by Teamid */
		private Dictionary<int, TeamRecord> teamRecords = null;
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
		private IList<ComboRecord> divisionList = null;
		private IList<ComboRecord> conferenceList = null;
		private IList<ComboRecord> leagueList = null;
		private IList<ComboRecord> offensivePlayBookList = null;
		private IList<ComboRecord> defensivePlayBookList = null;
		/** Add the city list here for the time being */
		private IList<ComboRecord> cityList = null;

		public TeamEditingModel(EditorModel model)
		{
			this.model = model;

			//Initialise the ComboRecord lists
			divisionList = new List<ComboRecord>();
			divisionList.Add(new ComboRecord("N/A", 31));
			divisionList.Add(new ComboRecord("AFC North", 0));
			divisionList.Add(new ComboRecord("AFC South", 1));
			divisionList.Add(new ComboRecord("AFC East", 2));
			divisionList.Add(new ComboRecord("AFC West", 3));
			divisionList.Add(new ComboRecord("NFC North", 4));
			divisionList.Add(new ComboRecord("NFC South", 5));
			divisionList.Add(new ComboRecord("NFC East", 6));
			divisionList.Add(new ComboRecord("NFC West", 7));
			divisionList.Add(new ComboRecord("NFL Euro", 8));

			conferenceList = new List<ComboRecord>();
			conferenceList.Add(new ComboRecord("N/A", 3));
			conferenceList.Add(new ComboRecord("NFL Euro", 2));
			conferenceList.Add(new ComboRecord("NFC", 1));
			conferenceList.Add(new ComboRecord("AFC", 0));

			leagueList = new List<ComboRecord>();
			leagueList.Add(new ComboRecord("N/A", 3));
			leagueList.Add(new ComboRecord("NFL Euro", 1));
			leagueList.Add(new ComboRecord("NFL", 0));

			cityList = new List<ComboRecord>();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.CITY_TABLE].GetRecords())
			{
				cityList.Add(new ComboRecord(((CityRecord)rec).Name, ((CityRecord)rec).CityId));
			}

			offensivePlayBookList = new List<ComboRecord>();
			offensivePlayBookList.Add(new ComboRecord("CHI-L.Smith", 0));
			offensivePlayBookList.Add(new ComboRecord("CIN-M.Lewis", 1));
			offensivePlayBookList.Add(new ComboRecord("BUF-M.Mularkey", 2));
			offensivePlayBookList.Add(new ComboRecord("DEN-M.Shanahan", 3));
			offensivePlayBookList.Add(new ComboRecord("CLE-B.Davis", 4));
			offensivePlayBookList.Add(new ComboRecord("TB-J.Gruden", 5));
			offensivePlayBookList.Add(new ComboRecord("ARI-D.Green", 6));
			offensivePlayBookList.Add(new ComboRecord("SD-Schottenheimer", 7));
			offensivePlayBookList.Add(new ComboRecord("KC-D.Vermeil", 8));
			offensivePlayBookList.Add(new ComboRecord("IND-T.Dungy", 9));
			offensivePlayBookList.Add(new ComboRecord("DAL-M.Carthon", 10));
			offensivePlayBookList.Add(new ComboRecord("MIA-D.Wannstedt", 11));
			offensivePlayBookList.Add(new ComboRecord("PHI-A.Reid", 12));
			offensivePlayBookList.Add(new ComboRecord("ATL-J.Mora Jr", 13));
			offensivePlayBookList.Add(new ComboRecord("SF-D.Erickson", 14));
			offensivePlayBookList.Add(new ComboRecord("NYG-T.Coughlin", 15));
			offensivePlayBookList.Add(new ComboRecord("JAX-J.Del Rio", 16));
			offensivePlayBookList.Add(new ComboRecord("NYJ-H.Edwards", 17));
			offensivePlayBookList.Add(new ComboRecord("DET-S.Mariucci", 18));
			offensivePlayBookList.Add(new ComboRecord("GB-M.Sherman", 19));
			offensivePlayBookList.Add(new ComboRecord("CAR-J.Fox", 20));
			offensivePlayBookList.Add(new ComboRecord("NE-B.Belichick", 21));
			offensivePlayBookList.Add(new ComboRecord("OAK-N.Turner", 22));
			offensivePlayBookList.Add(new ComboRecord("STL-M.Martz", 23));
			offensivePlayBookList.Add(new ComboRecord("BAL-B.Billick", 24));
			offensivePlayBookList.Add(new ComboRecord("WAS-J.Gibbs", 25));
			offensivePlayBookList.Add(new ComboRecord("NO-J.Hasslet", 26));
			offensivePlayBookList.Add(new ComboRecord("SEA-M.Holmgren", 27));
			offensivePlayBookList.Add(new ComboRecord("PIT-B.Cowher", 28));
			offensivePlayBookList.Add(new ComboRecord("TEN-J.Fisher", 29));
			offensivePlayBookList.Add(new ComboRecord("MIN-M.Tice", 30));
			offensivePlayBookList.Add(new ComboRecord("HOU-D.Capers", 31));
			offensivePlayBookList.Add(new ComboRecord("Balanced", 32));
			offensivePlayBookList.Add(new ComboRecord("Pass Balanced", 33));
			offensivePlayBookList.Add(new ComboRecord("Run Balanced", 34));
			offensivePlayBookList.Add(new ComboRecord("Run Heavy", 35));
			offensivePlayBookList.Add(new ComboRecord("West Coast", 36));
			offensivePlayBookList.Add(new ComboRecord("Run'n'Gun", 37));
			

			defensivePlayBookList = new List<ComboRecord>();
			defensivePlayBookList.Add(new ComboRecord("4-3", 0));
			defensivePlayBookList.Add(new ComboRecord("3-4", 1));
			defensivePlayBookList.Add(new ComboRecord("4-6", 2));
			defensivePlayBookList.Add(new ComboRecord("Cover 2", 3));
		}

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

		public IList<ComboRecord> DivisionList
		{
			get
			{
				return divisionList;
			}
		}

		public IList<ComboRecord> LeagueList
		{
			get
			{
				return leagueList;
			}
		}

		public IList<ComboRecord> ConferenceList
		{
			get
			{
				return conferenceList;
			}
		}

		public IList<ComboRecord> CityList
		{
			get
			{
				return cityList;
			}
		}

		public IList<ComboRecord> OffensivePlaybookList
		{
			get
			{
				return offensivePlayBookList;
			}
		}

		public IList<ComboRecord> DefensivePlaybookList
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
					currentTeamRecord = 0;
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
					currentTeamRecord = model.TableModels[EditorModel.TEAM_TABLE].RecordCount - 1;
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

		public TeamRecord GetTeamRecord(int teamId)
		{
			if (teamRecords == null)
			{
				teamRecords = new Dictionary<int, TeamRecord>();
				foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
				{
					TeamRecord tr = (TeamRecord)record;
					teamRecords.Add(tr.TeamId, tr);
				}
			}

			if (teamRecords.ContainsKey(teamId))
			{
				return teamRecords[teamId];
			}
			
			return null;
			
		}

		public ICollection<string> GetTeamNames()
		{
			if (teamNameList == null)
			{
				teamNameList = new Dictionary<int, string>();
				
				foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
				{
					TeamRecord teamRecord = (TeamRecord)record;
					teamNameList.Add(teamRecord.TeamId, teamRecord.Name);
				}
			}

			return teamNameList.Values;
		}

		public string GetTeamNameFromTeamId(int teamid)
		{
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

		public int GetTeamIdFromTeamName(string teamName)
		{
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
			else
			{
				throw new ApplicationException("Error getting TeamID for team name " + teamName);
			}
		}
	}
}
