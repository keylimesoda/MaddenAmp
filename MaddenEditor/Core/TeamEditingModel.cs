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
using System.IO;
using System.Linq;
using System.Text;

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class TeamEditingModel
	{  
        public const int FREE_AGENT = 1009;
		public const int PROBOWL_TEAM_AFC = 1010; //??
		public const int PROBOWL_TEAM_NFC = 1011; //??
        public const int RETIRED = 1014;
        public const int NO_TEAM_ID = 1023;
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

        #region Generic Lists
        private IList<GenericRecord> divisionList = null;
		private IList<GenericRecord> conferenceList = null;
		private IList<GenericRecord> leagueList = null;
		private IList<GenericRecord> offensivePlayBookList = null;
		private IList<GenericRecord> defensivePlayBookList = null;
		private IList<GenericRecord> teamTypeList = null;
        private IList<GenericRecord> stadiumlist = null;		
		private IList<GenericRecord> cityList = null;
        private IList<GenericRecord> endplaylist = null;

        #endregion

        private TeamUniformModel teamUniformModel = null;

        //  New Adds
        private MGMT _manager = null;
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

        public IList<GenericRecord> StadiumList
        {
            get { return stadiumlist; }
        }

        public IList<GenericRecord> EndPlayList
        {
            get { return endplaylist; }
        }
        
        public TeamUniformModel TeamUniformModel
        {
            get
            {
                return teamUniformModel;
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
        
        public string GetStadium(int id)
        {
            foreach (GenericRecord rec in stadiumlist)
            {
                if (rec.Id == id)
                    return rec.ToString();                
            }
            return "";
        }
        public int GetStadium(string name)
        {
            foreach (GenericRecord rec in stadiumlist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
       
        public string GetEndPlay(int id)
        {
            foreach (GenericRecord rec in endplaylist)
            {
                if (rec.Id == id)
                    return rec.ToString();
            }
            return "";
        }
        public int GetEndPlay(string name)
        {
            foreach (GenericRecord rec in endplaylist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetOFFPlaybook(int id)
        {
            foreach (GenericRecord rec in offensivePlayBookList)
            {
                if (rec.Id == id)
                    return rec.ToString();
            }
            return "";
        }
        public int GetOFFPlaybook(string name)
        {
            foreach (GenericRecord rec in offensivePlayBookList)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetDEFPlaybook(int id)
        {
            foreach (GenericRecord rec in defensivePlayBookList)
            {
                if (rec.Id == id)
                    return rec.ToString();
            }
            return "";
        }
        public int GetDEFPlaybook(string name)
        {
            foreach (GenericRecord rec in defensivePlayBookList)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetCity(int id)
        {
            foreach (GenericRecord rec in cityList)
            {
                if (rec.Id == id)
                    return rec.ToString();
            }
            return "";
        }
        public int GetCity(string name)
        {
            foreach (GenericRecord rec in cityList)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
       
        
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        #endregion
        
        public TeamEditingModel(EditorModel model)
        {
            this.model = model;

            if (model.MadVersion < MaddenFileVersion.Ver2019)
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

            
            if (model.MadVersion < MaddenFileVersion.Ver2019)
            {
                foreach (TableRecordModel rec in model.TableModels[EditorModel.CITY_TABLE].GetRecords())
                    cityList.Add(new GenericRecord(((CityRecord)rec).CityName, ((CityRecord)rec).CityId));

                #region 04-08 Playbooks
                offensivePlayBookList = new List<GenericRecord>();

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("CHI-D.Jauron");
                else AddOFFPB("CHI-L.Smith");                

                AddOFFPB("CIN-M.Lewis");

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("BUF-G.Williams");
                else if (model.MadVersion >= MaddenFileVersion.Ver2007)
                    AddOFFPB("BUF-D.Jauron");
                else AddOFFPB("BUF-M.Mularkey");

                AddOFFPB("DEN-M.Shanahan");

                if (model.MadVersion <= MaddenFileVersion.Ver2005)
                    AddOFFPB("CLE-B.Davis");
                else AddOFFPB("CLE-R.Crennel");

                AddOFFPB("TB-J.Gruden");

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("ARI-D.McGinnis");
                else if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("ARI-");
                else AddOFFPB("ARI-D.Green");

                if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("SD-N.Turner");
                else AddOFFPB("SD-M.Schottenheimer");

                if (model.MadVersion >= MaddenFileVersion.Ver2007)
                    AddOFFPB("KC-H.Edwards");
                else AddOFFPB("KC-D.Vermeil");

                AddOFFPB("IND-T.Dungy");

                if (model.MadVersion <= MaddenFileVersion.Ver2005)
                    AddOFFPB("DAL-M.Carthon");
                else if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("DAL-W.Phillips");
                else AddOFFPB("DAL-B.Parcells");

                if (model.MadVersion <= MaddenFileVersion.Ver2005)
                    AddOFFPB("MIA-D.Wannstedt");
                else if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("MIA-C.Cameron");
                else AddOFFPB("MIA-N.Saban");

                AddOFFPB("PHI-A.Reid");                

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("ATL-D.Reeves");
                else if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("ATL-B.Petrino");
                else AddOFFPB("ATL-J.Mora Jr");

                if (model.MadVersion <= MaddenFileVersion.Ver2005)
                    AddOFFPB("SF-D.Erickson");
                else AddOFFPB("SF-M.Nolan");

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("NYG-J.Fossil");
                else AddOFFPB("NYG-T.Coughlin");

                AddOFFPB("JAX-J.Del Rio");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("NYJ-H.Edwards");
                else AddOFFPB("NYJ-E.Mangini");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("DET-S.Mariucci");
                else AddOFFPB("DET-R.Marinelli");

                AddOFFPB("GB-M.Sherman");
                AddOFFPB("CAR-J.Fox");
                AddOFFPB("NE-B.Belichick");                

                if (model.MadVersion <= MaddenFileVersion.Ver2004)
                    AddOFFPB("OAK-B.Callahan");
                else if (model.MadVersion == MaddenFileVersion.Ver2005 || model.MadVersion == MaddenFileVersion.Ver2006)
                    AddOFFPB("OAK-N.Turner");
                else if (model.MadVersion == MaddenFileVersion.Ver2007)
                    AddOFFPB("OAK-A.Shell");
                else AddOFFPB("OAK-L.Kiffin");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("STL-M.Martz");
                else AddOFFPB("STL-S.Linehan");

                AddOFFPB("BAL-B.Billick");

                if (model.MadVersion == MaddenFileVersion.Ver2004)
                    AddOFFPB("WAS-S.Spurrier");
                else AddOFFPB("WAS-J.Gibbs");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("NO-J.Hasslet");
                else AddOFFPB("NO-S.Payton");

                AddOFFPB("SEA-M.Holmgren");                               

                if (model.MadVersion == MaddenFileVersion.Ver2007)
                    AddOFFPB("PIT-B.Cowher");
                else AddOFFPB("PIT-M.Tomlin");

                AddOFFPB("TEN-J.Fisher");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("MIN-M.Tice");
                else AddOFFPB("MIN-B.Childress");

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                    AddOFFPB("HOU-D.Capers");
                else AddOFFPB("HOU-G.Kubiak");

                AddOFFPB("Balanced");
                AddOFFPB("Pass Balanced");
                AddOFFPB("Run Balanced");
                AddOFFPB("Run Heavy");
                AddOFFPB("West Coast");
                AddOFFPB("Run'n'Gun");

                if (model.MadVersion == MaddenFileVersion.Ver2008)
                    AddOFFPB("Trick Plays");

                defensivePlayBookList = new List<GenericRecord>();

                if (model.MadVersion <= MaddenFileVersion.Ver2006)
                {                    
                    AddDEFPB("4-3");
                    AddDEFPB("3-4");
                    AddDEFPB("4-6");
                    AddDEFPB("Cover 2");
                    AddDEFPB("Balanced D");
                    AddDEFPB("QB Contain");                    
                }
                else
                {
                    // 2007-2008 coaches playbooks only
                    AddDEFPB("CHI-L.Smith");
                    AddDEFPB("CIN-M.Lewis");
                    AddDEFPB("BUF-D.Jauron");
                    AddDEFPB("DEN-M.Shanahan");
                    AddDEFPB("CLE-R.Crennel");
                    AddDEFPB("TB-J.Gruden");

                    if (model.MadVersion == MaddenFileVersion.Ver2008)
                        AddDEFPB("ARI-");
                    else AddDEFPB("ARI-D.Green");

                    if (model.MadVersion == MaddenFileVersion.Ver2008)
                        AddDEFPB("SD-N.Turner");
                    else AddDEFPB("SD-M.Schottenheimer");

                    AddDEFPB("KC-H.Edwards");
                    AddDEFPB("IND-T.Dungy");

                    if (model.MadVersion == MaddenFileVersion.Ver2008)
                        AddDEFPB("DAL-W.Phillips");
                    else AddDEFPB("DAL-B.Parcells");

                    if (model.MadVersion == MaddenFileVersion.Ver2008)
                        AddDEFPB("MIA-C.Cameron");
                    else AddDEFPB("MIA-N.Saban");

                    AddDEFPB("PHI-A.Reid");

                    if (model.MadVersion == MaddenFileVersion.Ver2008)
                        AddDEFPB("ATL-B.Petrino");
                    else AddDEFPB("ATL-J.Mora Jr");

                    AddDEFPB("SF-M.Nolan");
                    AddDEFPB("NYG-T.Coughlin");
                    AddDEFPB("JAX-J.Del Rio");
                    AddDEFPB("NYJ-E.Mangini");
                    AddDEFPB("DET-R.Marinelli");
                    AddDEFPB("GB-M.Sherman");
                    AddDEFPB("CAR-J.Fox");
                    AddDEFPB("NE-B.Belichick");                    

                    if (model.MadVersion == MaddenFileVersion.Ver2007)
                        AddDEFPB("OAK-A.Shell");
                    else AddDEFPB("OAK-L.Kiffin");

                    AddDEFPB("STL-S.Linehan");
                    AddDEFPB("BAL-B.Billick");
                    AddDEFPB("WAS-J.Gibbs");
                    AddDEFPB("NO-S.Payton");
                    AddDEFPB("SEA-M.Holmgren");                    

                    if (model.MadVersion == MaddenFileVersion.Ver2007)
                        AddDEFPB("PIT-B.Cowher");
                    else AddDEFPB("PIT-M.Tomlin");

                    AddDEFPB("TEN-J.Fisher");
                    AddDEFPB("MIN-B.Childress");
                    AddDEFPB("HOU-G.Kubiak");

                    AddDEFPB("4-3");
                    AddDEFPB("3-4");
                    AddDEFPB("4-6");
                    AddDEFPB("Cover 2");
                    AddDEFPB("Balanced D");
                    AddDEFPB("QB Contain");

                #endregion
                }
            }
            else 
            {
                ImportStadiums();
                ImportEndPlay();
                ImportPlayBook();
                ImportCities();
            }
            
        }

        public void InitConfig(MGMT man)
        {
            manager = man;
            if (model.MadVersion < MaddenFileVersion.Ver2019)
                model.TeamModel.InitPlaybooks();
        }

        public void ImportCities()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019CITY.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "CITY" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        cityList = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string city = "";
                            string state = "";

                            if (fields[0] == "CYID")
                                id = Convert.ToInt32(entry[0]);
                            if (fields[1] == "CYNM")
                                city = entry[1];
                            if (fields[2] == "CYST")
                                state = entry[2];
                            
                           cityList.Add(new GenericRecord(city, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }
        
        public void ImportStadiums()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019STAD.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "STAD" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        stadiumlist = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string desc = "";
                            string type = "";

                            if (fields[0] == "SGID")
                                id = Convert.ToInt32(entry[0]);
                            if (fields[1] == "SNAM")
                                desc = entry[1];
                            if (fields[2] == "SDNA")
                                type = entry[2];

                            string name = String.Copy(type);
                            if (desc != "")
                                name += " - " + desc;
                            stadiumlist.Add(new GenericRecord(name, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }

        public void ImportEndPlay()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019EPTA.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "EPTA" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        endplaylist = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string desc = "";
                            
                            if (fields[0] == "TEAS")
                                desc = entry[0];
                            if (fields[1] == "TEAV")
                                id = Convert.ToInt32(entry[1]);

                            string name = desc.Replace("EndPlay_", "");
                            endplaylist.Add(new GenericRecord(name, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }

        public void ImportPlayBook()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019PLAB.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "PLAB" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        offensivePlayBookList = new List<GenericRecord>();
                        defensivePlayBookList = new List<GenericRecord>();

                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            int book = -1;
                            string desc = "";

                            if (fields[0] == "BGID")
                                id = Convert.ToInt32(entry[0]);
                            if (fields[1] == "BOFF")
                                book = Convert.ToInt32(entry[1]);                            
                            
                            desc = entry[2];
                            string[] test = desc.Split('-');
                            string name = test[test.Count() - 1];

                            if (book == 1)
                                offensivePlayBookList.Add(new GenericRecord(name, id));
                            else if (book == 0)
                                defensivePlayBookList.Add(new GenericRecord(name, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }

        public void InitPlaybooks()
        {
            //  Adding playbooks from the db templates file
            
            if (manager.db_misc_model != null)
            {
                if (offensivePlayBookList != null)
                    offensivePlayBookList.Clear();
                if (DefensivePlaybookList != null)
                    defensivePlayBookList.Clear();

                // I can't figure out how to sort an IList, so we'll do this instead
                List<FRAPlayBooks> off = new List<FRAPlayBooks>();
                List<FRAPlayBooks> def = new List<FRAPlayBooks>();

                foreach (TableRecordModel rec in manager.db_misc_model.TableModels[EditorModel.PLAYBOOK_TABLE].GetRecords())
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

                offensivePlayBookList.Clear();
                defensivePlayBookList.Clear();

                foreach (FRAPlayBooks o in off)
                    offensivePlayBookList.Add(new GenericRecord(o.BookName, o.BookID));
                foreach (FRAPlayBooks d in def)
                    defensivePlayBookList.Add(new GenericRecord(d.BookName, d.BookID));
            }
        }

        public void AddOFFPB(string name)
        {
            int total = offensivePlayBookList.Count;
            offensivePlayBookList.Add(new GenericRecord(name, total)); 
        }
        public void AddDEFPB(string name)
        {
            int total = defensivePlayBookList.Count;
            defensivePlayBookList.Add(new GenericRecord(name, total));
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
                TeamNames.Add(i, t.Name);
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

        #region Filters
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
        #endregion

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
