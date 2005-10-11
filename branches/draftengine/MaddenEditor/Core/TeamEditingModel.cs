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

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class TeamEditingModel
	{
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

        // MADDEN DRAFT EDIT
        public void ComputeCONs() {
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
                    if (records[i].TeamType != 0)
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

        public void CalculateWins() {
            foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords()) 
            {
                ((TeamRecord)record).ComputeWins(model);
            }

        }

        public void CalculateStrengthOfSchedule() {
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


		public TeamEditingModel(EditorModel model)
		{
			this.model = model;

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
				cityList.Add(new GenericRecord(((CityRecord)rec).Name, ((CityRecord)rec).CityId));
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
			else
			{
				throw new ApplicationException("Error getting TeamID for team name " + teamName);
			}
		}
	}
}
