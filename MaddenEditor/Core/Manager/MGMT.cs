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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using MaddenEditor.Forms;
using MaddenEditor.Core.DatEditor;
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

    public class LeagueStats
    {        
        public double OFFENSE_PASS_YDS;
        public double OFFENSE_RUSH_YARDS;
        public double OFFENSE_YARDS;
        public double OFFENSE_PASS_ATT;
        public double OFFENSE_RUSH_ATT;
        public double OFFENSE_RUSH_TD;
        public double OFFENSE_SACKS_ALLOWED;
        public double OFFENSE_PASS_INT;
        public double OFFENSE_PASS_TDS;
        public double TOTAL_YARDS;
        
        public double FIRST_DOWNS;        
        public double THIRD_DOWN_CONVERSIONS;
        public double THIRD_DOWN_ATTEMPTS;
        public double FOURTH_DOWN_CONVERSIONS;
        public double FOURTH_DOWN_ATTEMPTS;

        public double OFFENSE_REDZONE_ATT;
        public double OFFENSE_REDZONE_TD;
        public double OFFENSE_REDZONE_FG;
        public double OFFENSE_TURNOVERS;
        public double FUMBLES_LOST;
        
        public double DEFENSE_REDZONE_FG;
        public double DEFENSIVE_INT;
        public double DEFENSE_PASS_YDS;
        public double DEFENSE_REDZONE_ATT;
        public double DEFENSE_REDZONE_TD;
        public double DEFENSE_RUSH_YDS;
        
        public double FUMBLES_RECOVERED;
        public double DEFENSE_SACKS;
        public double DEFENSE_TURNOVERS;
       
        public double PENALTIES;
        public double PENALTY_YARDS;

        public LeagueStats()
        {
            OFFENSE_PASS_YDS = 0;
            OFFENSE_RUSH_YARDS = 0;
            OFFENSE_YARDS = 0;
            OFFENSE_PASS_ATT = 0;
            OFFENSE_RUSH_ATT = 0;
            OFFENSE_RUSH_TD = 0;
            OFFENSE_SACKS_ALLOWED = 0;
            OFFENSE_PASS_INT = 0;
            OFFENSE_PASS_TDS = 0;
            TOTAL_YARDS = 0;

            FIRST_DOWNS = 0;
            THIRD_DOWN_CONVERSIONS = 0;
            THIRD_DOWN_ATTEMPTS = 0;
            FOURTH_DOWN_CONVERSIONS = 0;
            FOURTH_DOWN_ATTEMPTS = 0;

            OFFENSE_REDZONE_ATT = 0;
            OFFENSE_REDZONE_TD = 0;
            OFFENSE_REDZONE_FG = 0;
            OFFENSE_TURNOVERS = 0;
            FUMBLES_LOST = 0;

            DEFENSE_REDZONE_FG = 0;
            DEFENSIVE_INT = 0;
            DEFENSE_PASS_YDS = 0;
            DEFENSE_REDZONE_ATT = 0;
            DEFENSE_REDZONE_TD = 0;
            DEFENSE_RUSH_YDS = 0;

            FUMBLES_RECOVERED = 0;
            DEFENSE_SACKS = 0;
            DEFENSE_TURNOVERS = 0;

            PENALTIES = 0;
            PENALTY_YARDS = 0;
        }
        
    }
    
    public class TeamAvg
    {
        public TeamRecord teamrecord;
        public TeamSeasonStatsRecord teamstats;
        public List<BoxScoreOffenseRecord> offense;
        public List<BoxScoreDefenseRecord> defense;

        public TeamAvg()
        {
        }
        public void Set_TeamRecord(TeamRecord rec)
        {
            teamrecord = rec;
        }
        public void Set_TeamStats(TeamSeasonStatsRecord rec)
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
       
    public class MGMT
    {
        #region Variables
        #region Private

        private EditorModel _model = null;        
        private EditorModel _streamed = null;
        private EditorModel _db_misc = null;
        private UserSettingsModel usersettings = null;
        #endregion

        #region Get/Set
        public EditorModel model
        {
            get { return _model; }
            set { _model = value; }
        }
        public EditorModel stream_model
        {
            get { return _streamed; }
            set { _streamed = value; }
        }
        public EditorModel db_misc_model
        {
            get { return _db_misc; }
            set { _db_misc = value; }
        }
        public UserSettingsModel UserSettings
        {
            get { return usersettings; }
            set { usersettings = value; }
        }

        #endregion

        #region Variables

        public AmpConfig config = new AmpConfig();
        public DAT PlayerPortDAT = new DAT();
        public DAT CoachPortDAT = new DAT();
               
        public List<Player> Players;
        public DraftClass draftclass;
        public List<Coach> Coaches;
        public List<Coach> Owners;
        public Dictionary<int,Team> Teams;

        public Dictionary<int, List<Coach>> Scouting = new Dictionary<int, List<Coach>>();
        public LeagueAvg LeaguesAverages;
        public static string Manager_filename = Application.StartupPath + @"\MGMT.AMP";
        public List<TeamSeasonStatsRecord> TeamStats;
        public List<Coach> AvailableCoaches = new List<Coach>();

        private BackgroundWorker Main;
        private BackgroundWorker Loader;
        private BackgroundWorker Functions;
        public bool loaded = false;
        public bool workdone = false;
        public bool saved = false;
        public int currentversion = 1;

        public UInt32 id = 0x544d474d;
        public byte version;
        public int s_week = 0;
        public int s_stage = 0;
        public int s_day = 0;




        #endregion

        #endregion

        #region Constructors

        public MGMT()
        {
            config = new AmpConfig();
            UserSettings = new UserSettingsModel();
            Players = new List<Player>();
            Coaches = new List<Coach>();
            Owners = new List<Coach>();
            Teams = new Dictionary<int, Team>();
            PlayerPortDAT = new DAT();
            CoachPortDAT = new DAT();

            LeaguesAverages = new LeagueAvg();
            
            Main = new BackgroundWorker();
            Loader = new BackgroundWorker();
            Functions = new BackgroundWorker();
        }

        #endregion

        public void SetModel(EditorModel emodel)
        {
            model = emodel;            
        }

        public void Initialize()
        {
            s_week = model.FranchiseTime.Week;
            s_day = 0;
        }
        
        public void InitDB()
        {           
            #region Streamed Data
            if (config.streamdb_autoload[(int)model.FileVersion])
            {
                config.StreamFilename = config.stream_names[(int)model.FileVersion];
            }
            if (config.StreamFilename != "")
            {
                try
                {
                    stream_model = new EditorModel(config.StreamFilename, null, false, false);
                }
                catch (ApplicationException err)
                {
                    stream_model = null;
                }
            }
            
            #endregion

            #region Misc DB Templates
            if (config.db_misc_autoload[(int)model.FileVersion])
            {
                config.db_misc_filename = config.db_misc_names[(int)model.FileVersion];
            }

            if (config.db_misc_filename != "")
            {
                try
                {
                    db_misc_model = new EditorModel(config.db_misc_filename, null, false,false);
                }
                catch (ApplicationException err)
                {
                    db_misc_model = null;
                }
            }
            #endregion


        }
                
        public void InitTeams()
        {
            Teams.Clear();
            foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                TeamRecord tr = (TeamRecord)rec;
                if (tr.TeamId == 1009 || tr.TeamId == 1010 || tr.TeamId == 1011)
                    continue;
               
                if (!Teams.ContainsKey(tr.TeamId))
                {
                    Teams.Add(tr.TeamId, new Team(tr));                    
                }
                
                Team workingteam = Teams[tr.TeamId];
            }

            UpdateTeams();
        }

        public void UpdateTeams()
        {
            if (TeamStats == null)
                TeamStats = new List<TeamSeasonStatsRecord>();
            else TeamStats.Clear();

            #region Update Teams' Season Stats
            foreach (KeyValuePair<int, Team> dt in this.Teams)
            {
                Team cur = dt.Value;

                if (!cur.SeasonStats.ContainsKey(model.CurrentYear))
                {
                    foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_SEASON_STATS].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        TeamSeasonStatsRecord tssr = (TeamSeasonStatsRecord)rec;
                        if (tssr.TeamId != cur.team.TeamId)
                            continue;
                        cur.SeasonStats.Add(model.CurrentYear, tssr);
                        TeamStats.Add(tssr);
                    }
                }
            }
            #endregion

        }
                
        public Coach AddCoach(Coach coach)
        {
            if (coach.TEAM_ID == 1010 || coach.TEAM_ID == 1011) // skip probowl coaches
                    return null;            
            foreach (Coach c in Coaches)
            {               
                if (coach.COACH_ID == c.COACH_ID)              // NO duplicates
                    return null;
            }
            coach.InitHistory(model);
            if (coach.TEAM_ID == 1023)                         // Fix streameddb coaches teamid
                coach.TEAM_ID = 1009;
            Coaches.Add(coach);
            if (coach.CONTRACT_LENGTH > 0)
                return coach;
            else if (coach.History.ContainsKey(model.CurrentYear))
            {
                if (coach.History[model.CurrentYear].TeamID != 1009)
                    return coach;
            }
            return null;
        }

        public void InitCoaches()
        {
            Coaches.Clear();

            List<ScheduleRecord> playoffs = new List<ScheduleRecord>();
            if (model.FranchiseTime.Week >= 17 && model.FranchiseTime.Week <= 20)
            {
                foreach (TableRecordModel rec in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    ScheduleRecord sche = (ScheduleRecord)rec;
                    if (sche.WeekNumber < model.FranchiseTime.Week)
                        continue;
                    playoffs.Add(sche);
                }
            }
            
            foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;

                Coach c = new Coach((CoachRecord)rec);
                foreach (ScheduleRecord sr in playoffs)
                {
                    if (c.TEAM_ID != sr.AwayTeamID && c.TEAM_ID != sr.HomeTeamID)
                        continue;
                    if (sr.WeekNumber < model.FranchiseTime.Week)
                        continue;
                    // at this point the coach's team is still in playoffs, cannot hire
                    c.InPlayoffs = true;
                    c.CanBeHired = false;
                    // currently playing, so cannot interview either
                    if (sr.WeekNumber == model.FranchiseTime.Week)
                        c.CanBeInterviewed = false;                    
                }

                // coach will get kicked back into c if currently employed.
                c = AddCoach(c);
                                
                // add to team's coaching staff
                if (c != null && c.TEAM_ID != 1009)
                {
                    bool del = Teams[c.TEAM_ID].AddCoach(c);                    
                }
                else if (c.LAST_TEAM != 1009)                   // Also add to team if coach has expiring contract
                    Teams[c.LAST_TEAM].AddCoach(c);
            }

            if (stream_model != null)
            {
                foreach (TableRecordModel rec in stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    AddCoach(new Coach((CoachCollection)rec));
                }
            }
        }

        public void InitScouting()
        {
            Scouting.Clear();
            
            foreach (Coach c in Coaches)
            {
                List<Coach> current = new List<Coach>();
                if (Scouting.ContainsKey(c.TEAM_ID))
                {
                    current = Scouting[c.TEAM_ID];
                    current.Add(c);
                    Scouting[c.TEAM_ID] = current;
                }
                else
                {
                    current.Add(c);
                    Scouting.Add(c.TEAM_ID, current);
                }
            }
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
                        //c.UpdateCoachFromDB(rec);
                        exists = true;
                        break;
                    }
                if (!exists)
                    Coaches.Add(new Coach(rec));
            }
        }

        public void InitAvailableCoaches(int pos)
        {            
            AvailableCoaches.Clear();
            foreach (Coach c in Coaches)
            {
                if (AvailableCoaches.Contains(c))
                    continue;
                else if (c.CONTRACT_LENGTH == 0)                                        // No current contract
                    AvailableCoaches.Add(c);
                else if (pos == 0 && c.POSITION != 0)                                   // looking for HC and position is not head coach 
                    AvailableCoaches.Add(c);
                else if (pos == 1 && c.POSITION == 3 || pos == 2 && c.POSITION == 3)    // looking for OC/DC and position is ST
                    AvailableCoaches.Add(c);
            }
        }
        
        public void EvaluateCoach(Coach ecoach, Team eteam)
        {
            double evaluation = 0;
            Team coachteam = null;

            if (ecoach.TEAM_ID != 1009 && ecoach.TEAM_ID != 1010 && ecoach.TEAM_ID != 1011)
            {
                coachteam = this.Teams[ecoach.TEAM_ID];
            }
            CoachRecord coachrec = model.CoachModel.GetCoachById(ecoach.COACH_ID);

            double cw_perc = 0;
            if (coachrec.CareerWins + coachrec.CareerLosses + coachrec.CareerTies != 0)
                cw_perc = (double)(coachrec.CareerWins + coachrec.CareerTies / 2) / (coachrec.CareerWins + coachrec.CareerLosses + coachrec.CareerTies);

            double po_perc = 0;
            if (coachrec.PlayoffWins + coachrec.PlayoffLosses != 0)
                po_perc = (double)(coachrec.PlayoffWins / (coachrec.PlayoffWins + coachrec.PlayoffLosses));

            double sb_perc = 0;
            if ( coachrec.SuperBowlWins + coachrec.SuperBowlLoses !=0)
                sb_perc = (double)(coachrec.SuperBowlWins / (coachrec.SuperBowlWins + coachrec.SuperBowlLoses));

            double po_made = (double)coachrec.PlayoffsMade;

            double po_exp = 0;
            if (ecoach.years_exp != 0)
                po_exp = (double)coachrec.PlayoffsMade / ecoach.years_exp;

            double avg_wins = 0;
            if (ecoach.years_exp != 0)
                avg_wins = (double)(coachrec.CareerWins / ecoach.years_exp);

            double season = 0;
            double previous = 0;
            double season_playoff = 0;
            if (coachteam != null)
            {
                season = (double)(coachteam.team.SeasonWins + coachteam.team.SeasonTies / 2) / (coachteam.team.SeasonLosses + coachteam.team.SeasonTies / 2);
                // Previous Season Record
                previous = (double)(coachteam.team.PreviousSeasonWins + coachteam.team.PreviousSeasonTies / 2) / (coachteam.team.PreviousSeasonLosses +
                    coachteam.team.PreviousSeasonTies / 2);
                season_playoff = 0;
                if (coachteam.team.SeasonConfStanding <= 5)
                    season_playoff += 500;
            }

            LeagueStats league10 = GetTeamStatsAvg(10,-1);
            


            
            double probowlplayers = 0;
            #region Pro Bowl Players Bonus
            foreach (TableRecordModel rec in model.TableModels[EditorModel.PRO_BOWL_PLAYERS].GetRecords())
            {
                if (rec.Deleted)
                    continue;

                ProBowlPlayer pbp = (ProBowlPlayer)rec;
                if (pbp.TeamID != ecoach.TEAM_ID)
                    continue;

                if (ecoach.POSITION == 0)
                    probowlplayers += .5;
                if (ecoach.POSITION == 1 && pbp.Position <= 9)
                    probowlplayers += 1;
                if (ecoach.POSITION == 2 && pbp.Position >= 10 && pbp.Position <= 18)
                    probowlplayers += 1;
                if (ecoach.POSITION == 3 && pbp.Position >= 19)
                    probowlplayers += 1;
                if (ecoach.POSITION == 3 && pbp.Position <= 18)
                    probowlplayers += .25;
            }
            #endregion

            #region Loyalty/Patience for Current Coaches
            double loyalty = 0;
            double patience = 0;            
            if (ecoach.TEAM_ID == eteam.team.TeamId)
            {
                // Adjusting current staff with loyalty and patience
                // If this is not a HC, and there is an employed HC that Acts as GM
                // evaluate the coach using the HC ratings
                bool done = false;
                if (ecoach.POSITION != 0)
                {
                    if (eteam.Coaches.ContainsKey(0))
                    {
                        if (eteam.Coaches[0].CONTRACT_LENGTH > 0 && eteam.Coaches[0].HC_GM)
                        {                            
                            loyalty = (double)eteam.Coaches[0].LOYALTY / 300;        // max bonus of 33% 
                            patience = (double)eteam.Coaches[0].PATIENCE - 50 / 150; // max bonus +/- 33%
                            done = true;
                        }
                    }
                }

                if (!done)
                {
                    // Owner/GM does the eval
                    loyalty = (double)eteam.Owner_GM.LOYALTY / 300;        // max bonus of 33% 
                    patience = (double)eteam.Owner_GM.PATIENCE - 50 / 150; // max bonus +/- 33%
                }
            }
            #endregion

        }

        public void EvalCoachPositionRatings(Coach ecoach, Team eteam)
        {
            bool done = false;
            if (ecoach.POSITION != 0)
            {
                if (eteam.Coaches.ContainsKey(0))
                {
                    if (eteam.Coaches[0].CONTRACT_LENGTH > 0 && eteam.Coaches[0].HC_GM)
                    {

                        done = true;
                    }
                }
            }

            if (!done)
            {
                // Owner/GM does the eval
                
            }
        }
                
        public int GetRandInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public bool CheckOwners(Coach newowner)
        {
            bool exists = false;
            foreach (Coach o in this.Owners)
            {
                if (newowner.NAME == o.NAME)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }

        public LeagueStats GetTeamStatsAvg(int top, int div)
        {
            if (div != -1)
                top = 4;

            List<int> OFFENSE_YARDS = new List<int>();     
            List<int> OFFENSE_PASS_ATT = new List<int>();            
            List<int> OFFENSE_PASS_YDS = new List<int>();

            List<int> OFFENSE_RUSH_YARDS = new List<int>();
            
            
            List<int> OFFENSE_RUSH_ATT = new List<int>();
            List<int> OFFENSE_RUSH_TD = new List<int>();
            List<int> OFFENSE_SACKS_ALLOWED = new List<int>();
            List<int> OFFENSE_PASS_INT = new List<int>();
            List<int> OFFENSE_PASS_TDS = new List<int>();
            List<int> TOTAL_YARDS = new List<int>(); 
            List<int> FIRST_DOWNS = new List<int>();
            List<int> THIRD_DOWN_CONVERSIONS = new List<int>();
            List<int> THIRD_DOWN_ATTEMPTS = new List<int>();
            List<int> FOURTH_DOWN_CONVERSIONS = new List<int>();
            List<int> FOURTH_DOWN_ATTEMPTS = new List<int>();

            List<int> OFFENSE_REDZONE_ATT = new List<int>();
            List<int> OFFENSE_REDZONE_TD = new List<int>();
            List<int> OFFENSE_REDZONE_FG = new List<int>();
            List<int> OFFENSE_TURNOVERS = new List<int>();
            List<int> FUMBLES_LOST = new List<int>();

            List<int> DEFENSE_REDZONE_FG = new List<int>();
            List<int> DEFENSIVE_INT = new List<int>();
            List<int> DEFENSE_PASS_YDS = new List<int>();
            List<int> DEFENSE_REDZONE_ATT = new List<int>();
            List<int> DEFENSE_REDZONE_TD = new List<int>();
            List<int> DEFENSE_RUSH_YDS = new List<int>();

            List<int> FUMBLES_RECOVERED = new List<int>();
            List<int> DEFENSE_SACKS = new List<int>();
            List<int> DEFENSE_TURNOVERS = new List<int>();

            List<int> PENALTIES = new List<int>();
            List<int> PENALTY_YARDS = new List<int>();

            LeagueStats Averages = new LeagueStats();
            //foreach (TeamSeasonStatsRecord rec in TeamStats)
            //{
            //    
            //    TOTAL_YARDS.Add(rec.TotalYards);
            //    OFFENSE_PASS_ATT.Add(rec.OffensePassAtt);
            //    OFFENSE_PASS_YDS.Add(rec.OffensePassYards);                
            //}
            foreach (KeyValuePair<int,Team> kvp in this.Teams)
            {
                Team t = kvp.Value;
                if (div != -1 && div != t.team.DivisionId)
                    continue;
                TeamSeasonStatsRecord tssr = t.SeasonStats[model.CurrentYear];

                OFFENSE_YARDS.Add(tssr.OffenseYards);
                OFFENSE_PASS_ATT.Add(tssr.OffensePassAtt);
                OFFENSE_PASS_YDS.Add(tssr.OffensePassYards);
            }

            // Need to sort these from highest to lowest somehow
            OFFENSE_YARDS.Sort((x, y) => y.CompareTo(x));
            OFFENSE_PASS_ATT.Sort((x, y) => y.CompareTo(x));
            OFFENSE_PASS_YDS.Sort((x, y) => y.CompareTo(x));

            for (int c = 0; c < top; c++)
            {
                Averages.OFFENSE_YARDS += OFFENSE_YARDS[c];
                Averages.OFFENSE_PASS_ATT += OFFENSE_PASS_ATT[c];
                Averages.OFFENSE_PASS_YDS += OFFENSE_PASS_YDS[c];
            }

            Averages.OFFENSE_YARDS = Averages.OFFENSE_YARDS / top;
            Averages.OFFENSE_PASS_ATT = Averages.OFFENSE_PASS_ATT / top;
            Averages.OFFENSE_PASS_YDS = Averages.OFFENSE_PASS_YDS / top;

            return Averages;
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

        public bool LoadOwners()
        {
            string ownersfile = Application.StartupPath + @"\Owners.AMP";
            if (!File.Exists(ownersfile))
            {
                //MessageBox.Show("Cannot Find Owners File.", "File Does Not Exist", MessageBoxButtons.OK);
                return false;
            }

            BinaryReader binreader = new BinaryReader(File.Open(ownersfile, FileMode.Open));
            try
            {
                int count = binreader.ReadUInt16();
                for (int c = 0; c < count; c++)
                {
                    Coach own = new Coach();
                    own.Read(binreader);
                    Owners.Add(own);
                }
            }
            catch (EndOfStreamException err)
            {
                MessageBox.Show(err.GetType().Name, "Corrupted Owners File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                binreader.Close();
            }
            return true;
        }
        public void SaveOwners()
        {
            if (Owners.Count < 1)
                return;
            string ownersfile = Application.StartupPath + @"\Owners.AMP";
            BinaryWriter binwriter = new BinaryWriter(File.Open(ownersfile, FileMode.Create));
            binwriter.Write((UInt16)Owners.Count);
            foreach (Coach own in Owners)
                own.Write(binwriter);
            binwriter.Close();
        }

        public void LoadDATs()
        {
            if (PlayerPortDAT.loadfile == "")
            {
                if (config.AutoLoad_PlayerPort[(int)model.FileVersion])
                {
                    PlayerPortDAT.loadfile = config.PlayerPortFiles[(int)model.FileVersion];
                    PlayerPortDAT.Load();
                }
            }

            if (CoachPortDAT.loadfile == "")
            {
                if (config.AutoLoad_CoachPort[(int)model.FileVersion])
                {
                    CoachPortDAT.loadfile = config.CoachPortFiles[(int)model.FileVersion];
                    CoachPortDAT.Load();
                }
            }
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


        #region On hold
        /*
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
        */

        /*
        public List<int> GetTopPlayerSalaries(int position)
        {
            List<int> Top = new List<int>();
            for (int t = 0; t < 32; t++)
            {
                foreach (Player player in Teams[t].Players)
                    if (player.Info.POSITION_ID == position)
                        Top.Add(player.SALARY_CURRENT);
            }
            Top.Sort();

            return Top;
        }      
        */

        #endregion


    }
}
