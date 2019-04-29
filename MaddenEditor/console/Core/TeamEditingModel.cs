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
using System.Linq;
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
        public const int FREE_AGENT = 1009;
        // 1019, 1018 RFA ?

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

        //  New Adds
        private AmpConfig _config = null;
        private List<int> _teamlist = new List<int>();
        public List<int> teamlist
        {
            get { return _teamlist; }
            set { _teamlist = value; }
        }
        public Dictionary<int, string> TeamNames = new Dictionary<int, string>();

        #region Get / Set

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

        public TeamUniformModel TeamUniformModel
        {
            get
            {
                return teamUniformModel;
            }
        }

        public AmpConfig config
        {
            get { return _config; }
            set { _config = value; }
        }

        #endregion
        
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
            conferenceList.Add(new GenericRecord("AFC", 0));
            conferenceList.Add(new GenericRecord("NFC", 1));
            conferenceList.Add(new GenericRecord("NFL Europe", 2));

            leagueList = new List<GenericRecord>();
            leagueList.Add(new GenericRecord("N/A", 3));
            leagueList.Add(new GenericRecord("NFL", 0));
            leagueList.Add(new GenericRecord("NFL Europe", 1));

            cityList = new List<GenericRecord>();
            foreach (TableRecordModel rec in model.TableModels[EditorModel.CITY_TABLE].GetRecords())
                cityList.Add(new GenericRecord(((CityRecord)rec).CityName, ((CityRecord)rec).CityId));


            offensivePlayBookList = new List<GenericRecord>();
	
            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("CHI-D.Jauron", 0));
            else offensivePlayBookList.Add(new GenericRecord("CHI-L.Smith", 0));

            offensivePlayBookList.Add(new GenericRecord("CIN-M.Lewis", 1));

            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("BUF-G.Williams", 2));
            else if (model.FileVersion >= MaddenFileVersion.Ver2007)
                offensivePlayBookList.Add(new GenericRecord("BUF-D.Jauron", 2));
            else offensivePlayBookList.Add(new GenericRecord("BUF-M.Mularkey", 2));

            offensivePlayBookList.Add(new GenericRecord("DEN-M.Shanahan", 3));

            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                offensivePlayBookList.Add(new GenericRecord("CLE-B.Davis", 4));
            else offensivePlayBookList.Add(new GenericRecord("CLE-R.Crennel", 4));

            offensivePlayBookList.Add(new GenericRecord("TB-J.Gruden", 5));

            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("ARI-D.McGinnis", 6));
            else if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("ARI-", 6));
            else offensivePlayBookList.Add(new GenericRecord("ARI-D.Green", 6));

            if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("SD-N.Turner", 7));
            else offensivePlayBookList.Add(new GenericRecord("SD-M.Schottenheimer", 7));

            if (model.FileVersion >= MaddenFileVersion.Ver2007)
                offensivePlayBookList.Add(new GenericRecord("KC-H.Edwards", 8));
            else offensivePlayBookList.Add(new GenericRecord("KC-D.Vermeil", 8));

            offensivePlayBookList.Add(new GenericRecord("IND-T.Dungy", 9));

            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                offensivePlayBookList.Add(new GenericRecord("DAL-M.Carthon", 10));
            else if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("DAL-W.Phillips", 10));
            else offensivePlayBookList.Add(new GenericRecord("DAL-B.Parcells", 10));

            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                offensivePlayBookList.Add(new GenericRecord("MIA-D.Wannstedt", 11));
            else if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("MIA-C.Cameron", 11));
            else offensivePlayBookList.Add(new GenericRecord("MIA-N.Saban", 11));

            offensivePlayBookList.Add(new GenericRecord("PHI-A.Reid", 12));

            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("ATL-D.Reeves", 13));
            else if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("ATL-B.Petrino", 13));
            else offensivePlayBookList.Add(new GenericRecord("ATL-J.Mora Jr", 13));

            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                offensivePlayBookList.Add(new GenericRecord("SF-D.Erickson", 14));
            else offensivePlayBookList.Add(new GenericRecord("SF-M.Nolan", 14));

            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("NYG-J.Fossil", 15));
            else offensivePlayBookList.Add(new GenericRecord("NYG-T.Coughlin", 15));

            offensivePlayBookList.Add(new GenericRecord("JAX-J.Del Rio", 16));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("NYJ-H.Edwards", 17));
            else offensivePlayBookList.Add(new GenericRecord("NYJ-E.Mangini", 17));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("DET-S.Mariucci", 18));
            else offensivePlayBookList.Add(new GenericRecord("DET-R.Marinelli", 18));

            offensivePlayBookList.Add(new GenericRecord("GB-M.Sherman", 19));
            offensivePlayBookList.Add(new GenericRecord("CAR-J.Fox", 20));
            offensivePlayBookList.Add(new GenericRecord("NE-B.Belichick", 21));

            if (model.FileVersion <= MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("OAK-B.Callahan", 22));
            else if (model.FileVersion == MaddenFileVersion.Ver2005 || model.FileVersion == MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("OAK-N.Turner", 22));
            else if (model.FileVersion == MaddenFileVersion.Ver2007)
                offensivePlayBookList.Add(new GenericRecord("OAK-A.Shell", 22));
            else offensivePlayBookList.Add(new GenericRecord("OAK-L.Kiffin", 22));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("STL-M.Martz", 23));
            else offensivePlayBookList.Add(new GenericRecord("STL-S.Linehan", 23));

            offensivePlayBookList.Add(new GenericRecord("BAL-B.Billick", 24));

            if (model.FileVersion == MaddenFileVersion.Ver2004)
                offensivePlayBookList.Add(new GenericRecord("WAS-S.Spurrier", 25));
            else offensivePlayBookList.Add(new GenericRecord("WAS-J.Gibbs", 25));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("NO-J.Hasslet", 26));
            else offensivePlayBookList.Add(new GenericRecord("NO-S.Payton", 26));

            offensivePlayBookList.Add(new GenericRecord("SEA-M.Holmgren", 27));

            if (model.FileVersion == MaddenFileVersion.Ver2007)
                offensivePlayBookList.Add(new GenericRecord("PIT-B.Cowher", 28));
            else offensivePlayBookList.Add(new GenericRecord("PIT-M.Tomlin", 28));

            offensivePlayBookList.Add(new GenericRecord("TEN-J.Fisher", 29));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("MIN-M.Tice", 30));
            else offensivePlayBookList.Add(new GenericRecord("MIN-B.Childress", 30));

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                offensivePlayBookList.Add(new GenericRecord("HOU-D.Capers", 31));
            else offensivePlayBookList.Add(new GenericRecord("HOU-G.Kubiak", 31));

            offensivePlayBookList.Add(new GenericRecord("Balanced", 32));
            offensivePlayBookList.Add(new GenericRecord("Pass Balanced", 33));
            offensivePlayBookList.Add(new GenericRecord("Run Balanced", 34));
            offensivePlayBookList.Add(new GenericRecord("Run Heavy", 35));
            offensivePlayBookList.Add(new GenericRecord("West Coast", 36));
            offensivePlayBookList.Add(new GenericRecord("Run'n'Gun", 37));

            if (model.FileVersion == MaddenFileVersion.Ver2008)
                offensivePlayBookList.Add(new GenericRecord("Trick Plays", 38));

            defensivePlayBookList = new List<GenericRecord>();
            
            if (model.FileVersion <= MaddenFileVersion.Ver2006)
            {
                defensivePlayBookList.Add(new GenericRecord("4-3", 0));
                defensivePlayBookList.Add(new GenericRecord("3-4", 1));
                defensivePlayBookList.Add(new GenericRecord("4-6", 2));
                defensivePlayBookList.Add(new GenericRecord("Cover 2", 3));
                defensivePlayBookList.Add(new GenericRecord("Balanced D", 4));
                defensivePlayBookList.Add(new GenericRecord("QB Contain", 5));
            }
            else
            {
                // 2007-2008 coaches playbooks only
                defensivePlayBookList.Add(new GenericRecord("CHI-L.Smith", 0));
                defensivePlayBookList.Add(new GenericRecord("CIN-M.Lewis", 1));
                defensivePlayBookList.Add(new GenericRecord("BUF-D.Jauron", 2));
                defensivePlayBookList.Add(new GenericRecord("DEN-M.Shanahan", 3));
                defensivePlayBookList.Add(new GenericRecord("CLE-R.Crennel", 4));
                defensivePlayBookList.Add(new GenericRecord("TB-J.Gruden", 5));

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                    defensivePlayBookList.Add(new GenericRecord("ARI-", 6));
                else defensivePlayBookList.Add(new GenericRecord("ARI-D.Green", 6));

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                    defensivePlayBookList.Add(new GenericRecord("SD-N.Turner", 7));
                else defensivePlayBookList.Add(new GenericRecord("SD-M.Schottenheimer", 7));

                defensivePlayBookList.Add(new GenericRecord("KC-H.Edwards", 8));
                defensivePlayBookList.Add(new GenericRecord("IND-T.Dungy", 9));

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                    defensivePlayBookList.Add(new GenericRecord("DAL-W.Phillips", 10));
                else defensivePlayBookList.Add(new GenericRecord("DAL-B.Parcells", 10));

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                    defensivePlayBookList.Add(new GenericRecord("MIA-C.Cameron", 11));
                else defensivePlayBookList.Add(new GenericRecord("MIA-N.Saban", 11));

                defensivePlayBookList.Add(new GenericRecord("PHI-A.Reid", 12));

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                    defensivePlayBookList.Add(new GenericRecord("ATL-B.Petrino", 13));
                else defensivePlayBookList.Add(new GenericRecord("ATL-J.Mora Jr", 13));

                defensivePlayBookList.Add(new GenericRecord("SF-M.Nolan", 14));
                defensivePlayBookList.Add(new GenericRecord("NYG-T.Coughlin", 15));
                defensivePlayBookList.Add(new GenericRecord("JAX-J.Del Rio", 16));
                defensivePlayBookList.Add(new GenericRecord("NYJ-E.Mangini", 17));
                defensivePlayBookList.Add(new GenericRecord("DET-R.Marinelli", 18));
                defensivePlayBookList.Add(new GenericRecord("GB-M.Sherman", 19));
                defensivePlayBookList.Add(new GenericRecord("CAR-J.Fox", 20));
                defensivePlayBookList.Add(new GenericRecord("NE-B.Belichick", 21));

                if (model.FileVersion == MaddenFileVersion.Ver2007)
                    defensivePlayBookList.Add(new GenericRecord("OAK-A.Shell", 22));
                else defensivePlayBookList.Add(new GenericRecord("OAK-L.Kiffin", 22));

                defensivePlayBookList.Add(new GenericRecord("STL-S.Linehan", 23));
                defensivePlayBookList.Add(new GenericRecord("BAL-B.Billick", 24));
                defensivePlayBookList.Add(new GenericRecord("WAS-J.Gibbs", 25));
                defensivePlayBookList.Add(new GenericRecord("NO-S.Payton", 26));
                defensivePlayBookList.Add(new GenericRecord("SEA-M.Holmgren", 27));

                if (model.FileVersion == MaddenFileVersion.Ver2007)
                    defensivePlayBookList.Add(new GenericRecord("PIT-B.Cowher", 28));
                else defensivePlayBookList.Add(new GenericRecord("PIT-M.Tomlin", 28));

                defensivePlayBookList.Add(new GenericRecord("TEN-J.Fisher", 29));
                defensivePlayBookList.Add(new GenericRecord("MIN-B.Childress", 30));
                defensivePlayBookList.Add(new GenericRecord("HOU-G.Kubiak", 31));

                defensivePlayBookList.Add(new GenericRecord("4-3", 32));
                defensivePlayBookList.Add(new GenericRecord("3-4", 33));
                defensivePlayBookList.Add(new GenericRecord("4-6", 34));
                defensivePlayBookList.Add(new GenericRecord("Cover 2", 35));
                defensivePlayBookList.Add(new GenericRecord("Balanced D", 36));
                defensivePlayBookList.Add(new GenericRecord("QB Contain", 37));
            }
        }

        public void InitConfig(AmpConfig ampconfig)
        {
            config = ampconfig;
        }

        public void InitPlaybooks()
        {
            //  Adding playbooks from the db templates file
            
            if (config.db_misc_model != null)
            {
                if (offensivePlayBookList != null)
                    offensivePlayBookList.Clear();
                if (DefensivePlaybookList != null)
                    defensivePlayBookList.Clear();

                // I can't figure out how to sort an IList, so we'll do this instead
                List<FRAPlayBooks> off = new List<FRAPlayBooks>();
                List<FRAPlayBooks> def = new List<FRAPlayBooks>();

                foreach (TableRecordModel rec in config.db_misc_model.TableModels[EditorModel.PLAYBOOK_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    FRAPlayBooks pb = (FRAPlayBooks)rec;

                    if (pb.BookisOffense)
                        off.Add(pb);
                    else def.Add(pb);
                }

                off.Sort((x, y) => x.BookID.CompareTo(y.BookID));
                def.Sort((x, y) => x.BookID.CompareTo(y.BookID));

                foreach (FRAPlayBooks o in off)
                    offensivePlayBookList.Add(new GenericRecord(o.BookName, o.BookID));
                foreach (FRAPlayBooks d in def)
                    defensivePlayBookList.Add(new GenericRecord(d.BookName, d.BookID));
            }
        }

        #region Draft edits

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

        #endregion

        public void GetTeamList()
        {
            if (_teamlist == null)
                _teamlist = new List<int>();
            else _teamlist.Clear();
            if (TeamNames == null)
                TeamNames = new Dictionary<int, string>();
            else TeamNames.Clear();

            foreach (TeamRecord rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                TeamRecord tr = (TeamRecord)rec;
                bool add = false;

                // filters
                if (currentConferenceFilterId == -1 && currentDivisionFilterId == -1 && currentLeagueFilterId == -1)
                    add = true;
                else
                {
                    if (currentLeagueFilterId == -1)
                    {
                        if (currentConferenceFilterId == -1)
                        {
                            if (currentDivisionFilterId == tr.DivisionId)
                                add = true;
                        }
                        else if (currentDivisionFilterId == -1)
                        {
                            if (currentConferenceFilterId == tr.ConferenceId)
                                add = true;
                        }
                        else
                        {
                            if (currentConferenceFilterId == tr.ConferenceId && currentDivisionFilterId == tr.DivisionId)
                                add = true;
                        }                            
                    }                    
                    else if (currentConferenceFilterId == -1)
                    {
                        if (currentLeagueFilterId == -1)
                        {
                            if (currentDivisionFilterId == tr.DivisionId)
                                add = true;
                        }
                        else if (currentDivisionFilterId == -1)
                        {
                            if (currentLeagueFilterId == tr.LeagueId)
                                add = true;
                        }
                        else
                        {
                            if (currentLeagueFilterId == tr.LeagueId && currentDivisionFilterId == tr.DivisionId)
                                add = true;
                        }  
                    }
                    else if (currentDivisionFilterId == -1)
                    {
                        if (currentLeagueFilterId == -1)
                        {
                            if (currentConferenceFilterId == tr.ConferenceId)
                                add = true;
                        }
                        else if (currentConferenceFilterId == -1)
                        {
                            if (currentLeagueFilterId == tr.LeagueId)
                                add = true;
                        }
                        else
                        {
                            if (currentLeagueFilterId == tr.LeagueId && currentConferenceFilterId == tr.ConferenceId)
                                add = true;
                        }  
                    }
                    else
                    {
                        if (currentLeagueFilterId == tr.LeagueId && currentConferenceFilterId == tr.ConferenceId && currentDivisionFilterId == tr.DivisionId)
                            add = true;
                    }
                }

                if (add)
                    _teamlist.Add(tr.RecNo);
            }

            _teamlist.Sort();
            foreach (int i in _teamlist)
            {
                TeamRecord t = (TeamRecord)model.TableModels[EditorModel.TEAM_TABLE].GetRecord(i);
                TeamNames.Add(i, t.LongName);
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
