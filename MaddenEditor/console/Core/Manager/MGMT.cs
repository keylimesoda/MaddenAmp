/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2014 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Core
{    
    

    public class Priorities
    {
        public Dictionary<int, decimal> Position_Values;
               
        public Priorities()
        {
            Position_Values = new Dictionary<int, decimal>();
        }
    }
        
    public class TeamAvg
    {
        public TeamRecord teamrecord;
        public SeasonStatsTeamRecord teamstats;
        public List<BoxScoreOffenseRecord> offense;
        public List<BoxScoreDefenseRecord> defense;

        public TeamAvg()
        {
        }
        public void Set_TeamRecord(TeamRecord rec)
        {
            teamrecord = rec;
        }
        public void Set_TeamStats(SeasonStatsTeamRecord rec)
        {
            teamstats = rec;
        }
        public void Set_TeamOffense(BoxScoreOffenseRecord rec)
        {
            if (offense == null)
                offense = new List<BoxScoreOffenseRecord>();
            offense.Add(rec);
        }
        public void Set_TeamDefense(BoxScoreDefenseRecord rec)
        {
            if (defense == null)
                defense = new List<BoxScoreDefenseRecord>();
            defense.Add(rec);
        }

    }
      
    public class PlayAvg
    {
        public int Pos = -1;
        public int PlayerId = -1;
        public int CurrentSalary = -1;
        public int ContractLength = -1;
        public int ContractSalary = -1;
        public int ContractBonus = -1;
        public int DraftRound = -1;
        public int DraftPick = -1;
        

        public PlayAvg()
        {            
        }
        public PlayAvg(Player player)
        {
            Pos = player.Info.POSITION_ID;
            PlayerId = player.Info.PLAYER_ID;
            CurrentSalary = player.SALARY_CURRENT;
            ContractLength = player.CONTRACT_LENGTH;
            ContractSalary = player.TOTAL_SALARY;
            if (player.SIGNING_BONUS == 0)
                ContractBonus = player.PREVIOUS_SIGNING_BONUS_TOTAL;
            else ContractBonus = player.SIGNING_BONUS;
            DraftRound = player.DRAFT_ROUND;
            DraftPick = player.DRAFT_ROUND_INDEX;
        }
    }
    
    public class LeagueAvg
    {
        public Dictionary<int,int> ElitePlayers;
        public Dictionary<int,int> TotalPlayers;

        public List<PlayAvg> PlayerAverages;

        public LeagueAvg()
        {
            ElitePlayers = new Dictionary<int, int>();
            TotalPlayers = new Dictionary<int, int>();           
            PlayerAverages = new List<PlayAvg>();
        }

        public void AddPlayer(Player player)
        {
            if (TotalPlayers.ContainsKey(player.Info.POSITION_ID))
                TotalPlayers[player.Info.POSITION_ID]++;
            else TotalPlayers.Add(player.Info.POSITION_ID, 1);

            if (player.OVERALL >= 90)
            {
                if (ElitePlayers.ContainsKey(player.Info.POSITION_ID))
                    ElitePlayers[player.Info.POSITION_ID]++;
                else ElitePlayers.Add(player.Info.POSITION_ID, 1);
            }
            PlayerAverages.Add(new PlayAvg(player));            
        }
       
        public void Init(List<Player> players)
        {
            ElitePlayers = new Dictionary<int, int>();
            TotalPlayers = new Dictionary<int, int>();
            PlayerAverages = new List<PlayAvg>();
            foreach (Player p in players)
                AddPlayer(p); 
        }
        
        public double GetEliteValue(int position)
        {
            return (double)TotalPlayers[position] / (double)ElitePlayers[position];
        }

    }

    public class MGMT_Config
    {
        private DraftConfig _draftconfig = null;
        public DraftConfig draftconfig
        {
            get { return _draftconfig; }
            set { _draftconfig = value; }
        }
    }
    
    public class MGMT
    {
        #region members

        #region Private

        private EditorModel _model;
        private EditorModel _streamedmodel;
        private MGMT_Config _config;

        #endregion

        public List<Player> Players;
        public DraftClass draftclass;
        public List<Coach> Coaches;
        public List<Owner> OwnersGms;
        public List<Team> Teams;

        public EditorModel model
        {
            get { return _model; }
            set { _model = value; }
        }
        public EditorModel streamed_model
        {
            get { return _streamedmodel; }
            set { _streamedmodel = value; }
        }
        public MGMT_Config config
        {
            get { return _config; }
            set { _config = value; }
        }

        public LeagueAvg LeaguesAverages;
        public static string Manager_filename = Application.StartupPath + @"\MGMT.AMP";

        private BackgroundWorker Main;
        private BackgroundWorker Loader;
        private BackgroundWorker Functions;
        public bool loaded = false;
        public bool workdone = false;
        public bool saved = false;
        
        #endregion

        #region Constructors

        public MGMT()
        {
            Players = new List<Player>();
            draftclass = new DraftClass();
            Coaches = new List<Coach>();
            OwnersGms = new List<Owner>();
            Teams = new List<Team>();

            LeaguesAverages = new LeagueAvg();
            
            Main = new BackgroundWorker();
            Loader = new BackgroundWorker();
            Functions = new BackgroundWorker();

            model = null;
        }

        #endregion

        public void SetModel(EditorModel emodel)
        {
            model = emodel;            
        }
        
        public void SetStreamed(EditorModel streamed)
        {
            streamed_model = streamed;
        }

        public void InitMain()
        {
            if (File.Exists(Manager_filename))
                Load();
            if (!loaded)
                CreateDatabase();

            InitTeams();  

            if (!File.Exists(Manager_filename))
                Save();

            LeaguesAverages.Init(Players);           
        }

        public void CreateDatabase()
        {
            //  Create database for the first time
            Players = new List<Player>();
            for (int c = 0; c < model.TableModels[EditorModel.PLAYER_TABLE].RecordCount; c++)
            {
                PlayerRecord pr = model.PlayerModel.GetPlayerRecord(c);
                if (pr.Deleted || pr.FirstName == "New")
                    continue;
                Players.Add(new Player(pr, model));

                Players[Players.Count-1].UpdateCareerStats(model);

                if (Players[Players.Count-1].PlayerCareerStats.GAMES_PLAYED > 0)
                    Players[Players.Count-1].UpdateSeasonStats(model);
            }

            Coaches = new List<Coach>();
            for (int c = 0; c < model.TableModels[EditorModel.COACH_TABLE].RecordCount; c++)
            {
                Coaches.Add(new Coach(model.CoachModel.GetCoachRecord(c)));
            }
        }
        
        public void UpdatePlayers()
        {
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                bool exists = false;
                foreach (Player p in Players)
                    if (p.Info.PLAYER_ID == rec.PlayerId)
                    {
                        exists = true;
                        p.UpdateCurrentRatings(rec);
                        p.UpdateCareerStats(model);
                        p.UpdateSeasonStats(model);
                        break;
                    }
                if (!exists)
                    Players.Add(new Player(rec, model));

            }   
        }
        
        public void UpdateCoaches()
        {
            foreach (CoachRecord rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                bool exists = false;
                foreach (Coach c in Coaches)
                    if (c.COACH_ID == rec.CoachId)
                    {
                        c.UpdateCoach(rec);
                        exists = true;
                        break;
                    }
                if (!exists)
                    Coaches.Add(new Coach(rec));
            }
        }
        
        public void InitTeams()
        {
            Teams = new List<Team>();
            for (int c = 0; c < 32; c++)
                Teams.Add(new Team(c));
            Teams.Add(new Team(1009));                                      //  32 Free agents or Unemployed
            Teams.Add(new Team(1014));                                      //  33 Retired
            Teams.Add(new Team(1015));                                      //  34 Rookies
            Teams.Add(new Team(1010));                                      //  35 AFC
            Teams.Add(new Team(1011));                                      //  36 NFC
            

            foreach (Player player in Players)
            {
                if (player.TEAM_ID == 1009)
                    Teams[32].Players.Add(player);
                else if (player.TEAM_ID == 1014)
                    Teams[33].Players.Add(player);
                else if (player.TEAM_ID == 1015)
                    Teams[34].Players.Add(player);
                
                else if (player.TEAM_ID >= 0 && player.TEAM_ID <= 31)
                    Teams[player.TEAM_ID].Players.Add(player);
            }
            foreach (Coach coach in Coaches)
            {
                if (coach.TEAM_ID == 1009 || coach.TEAM_ID == 1023)
                    Teams[32].Coaches.Add(coach);
                else if (coach.TEAM_ID == 1014)
                    Teams[33].Coaches.Add(coach);
                else Teams[coach.TEAM_ID].Coaches.Add(coach);
            }

            for (int t = 0; t < 32; t++)
            {
                foreach (TeamRecord tr in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                    if (tr.TeamId == t)
                        Teams[t].TeamAverages.Set_TeamRecord(tr);
                
            }

        }
        
        public List<int> GetTopPlayerSalaries(int position)
        {
            List<int> Top = new List<int>();
            for (int t = 0; t < 32; t++ )
            {
                foreach (Player player in Teams[t].Players)
                    if (player.Info.POSITION_ID == position)
                        Top.Add(player.SALARY_CURRENT);
            }
            Top.Sort();

            return Top;
        }

        public int GetRandInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        
        #region File IO
        public void Load()
        {
            BinaryReader binreader = new BinaryReader(File.Open(Manager_filename, FileMode.Open));

            #region Players
            int NumberOfPlayers = binreader.ReadInt32();
            Players = new List<Player>();
            for (int c = 0; c < NumberOfPlayers; c++)
            {
                Player newplayer = new Player();
                newplayer.Read(binreader);
                if (newplayer.Info.FIRST_NAME != "New")
                    Players.Add(newplayer);
            }
            #endregion

            #region Coaches
            int NumberOfCoaches = binreader.ReadInt32();
            Coaches = new List<Coach>();
            for (int c = 0; c < NumberOfCoaches; c++)
            {
                Coach newcoach = new Coach();
                newcoach.Read(binreader);
                Coaches.Add(newcoach);
            }
            #endregion

            loaded = true;
            binreader.Close();     
        }
        public void Save()
        {
            BinaryWriter binwriter = new BinaryWriter(File.Create(Manager_filename));
            binwriter.Write(Players.Count);
            foreach (Player p in Players)
            {
                p.Write(binwriter);
            }
            binwriter.Write(Coaches.Count);
            foreach (Coach coach in Coaches)
            {
                coach.Write(binwriter);
            }

            binwriter.Close();
        }
        #endregion
        
        #region Background Thread Methods
        
        void ManagerThread_Load(object sender, DoWorkEventArgs e)
        {
           
        }
        void ManagerThread_Save(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_CreateDatabase(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_Init(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_UpdatePlayerList(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_UpdateCoachList(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_InitTeams(object sender, DoWorkEventArgs e)
        {
            
        }
        
        
        #endregion

          
        
        
        
                
        

        
        

    }
}
