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
    public class Team
    {
        #region Members

        public TeamRecord team;
        public List<Player> Players;
        public Dictionary<int,Coach> Coaches;
        public Coach Owner_GM;
        public List<Player_Ratings> Evals;        
        public List<Player_Ratings> Rookie_Scouting;
        public List<int> CoachesInterviewed;
        public TeamAvg TeamAverages;
        public Dictionary<int, TeamSeasonStatsRecord> SeasonStats;
        public Dictionary<int,double> Evals_Coaches;

        public bool inPlayoffs = false;

        #endregion

        #region Constructors

        public Team()
        {
            team = null;
            Players = new List<Player>();
            Coaches = new Dictionary<int, Coach>();
            Owner_GM = new Coach();
            Evals = new List<Player_Ratings>();
            
            Rookie_Scouting = new List<Player_Ratings>();
            CoachesInterviewed = new List<int>();
            TeamAverages = new TeamAvg();
            SeasonStats = new Dictionary<int, TeamSeasonStatsRecord>();
            Evals_Coaches = new Dictionary<int, double>();
        }
        
        public Team(TeamRecord tr)
        {
            team = tr;
            Players = new List<Player>();
            Coaches = new Dictionary<int, Coach>();
            Owner_GM = new Coach();

            Evals = new List<Player_Ratings>();
            Rookie_Scouting = new List<Player_Ratings>();
            CoachesInterviewed = new List<int>();
            TeamAverages = new TeamAvg();
            SeasonStats = new Dictionary<int, TeamSeasonStatsRecord>();
            Evals_Coaches = new Dictionary<int, double>();
        }

        #endregion

        public bool AddCoach(Coach coach)
        {
            // Check for more than one at each position or same coach at different positions
            if (Coaches.ContainsKey(coach.POSITION) || Coaches.ContainsValue(coach))    
                return false;
            else
            {
                Coaches.Add(coach.POSITION, coach);
                return true;
            }
        }

        public double GetCoaches_PlayerSkillBonus(Player player)
        {
            double bonus = 0;
            foreach (KeyValuePair<int, Coach> coach in Coaches )
                bonus += coach.Value.GetPositionProgRating(player.Info.POSITION_ID);

            return bonus;
        }

        public double GetCoaches_PlayerMotivationBonus(Player player)
        {
            double bonus = 0;
            foreach (KeyValuePair<int, Coach> coach in Coaches)
                bonus += coach.Value.GetMotivationProgRating(player.Info.POSITION_ID);

            return bonus;
        }

        public double GetCoaches_PlayerKnowledgeBonus(Player player)
        {
            double bonus = 0;
            foreach (KeyValuePair<int, Coach> coach in Coaches)
                bonus += coach.Value.GetKnowledgeProgRating(player.Info.POSITION_ID);

            return bonus;
        }
        
        public double GetAvgSalary(int position, LeagueAvg LA)
        {
            int play = 0;
            int total = 0;
            for (int c = 0; c < LA.PlayerAverages.Count; c++)
            {
                if (position == LA.PlayerAverages[c].Pos)
                {
                    play++;
                    total += LA.PlayerAverages[c].CurrentSalary;
                }
            }
            return (double)(total) / play;
        }

        public double GetPerceivedValue(Player player, EditorModel model)
        {
            double playervalue = 0;


            // Player Position
            // depth at position
            // Player Age
            // Player past stats
            // Caoches' perceived player OVR
            // Player Salary
            // Free Agents available
            // Draft pick positions
            // Offense type
            // Defense type

            return playervalue;
        }

        public double GetScoutError(double scout)
        {
            double err = scout * 100;

            int low = (int)(10000 - err) / 2;
            int high = 10000 - low;
            Random random = new Random();
            int check = random.Next(1, 10000);                                                      //  Checking for a complete scouting error on the player
            if (check <= low)
                return (random.NextDouble() * (scout - 100));
            if (check >= high)
                return (random.NextDouble() * (100 - scout));                                                                                
            else
            {
                if (check <= 5000)                                                                  // Not a big scouting error, but eval will still be                                                                                                     
                    return (random.NextDouble() * -2.0);                                            // slightly off by +/- 2.0                    
                else return (random.NextDouble() * 2.0);
            }
        }

        public void EvaluatePlayer(Player player, int analyst)
        {
            Player_Ratings scouted = new Player_Ratings();

            double pos_error = analyst;
            if (analyst == -1)
            {
                double coachrate = GetCoaches_PlayerSkillBonus(player);                                 // get coached combined rating for skill
                double ownerrate = 0;
                double coachportion = (100 - Owner_GM.EGO) / 100;                                       // adjust coach portion
                pos_error = ownerrate + (coachrate * coachportion);
            }

            scouted.ACCELERATION = (int)(player.Current_Ratings.ACCELERATION * (100 + GetScoutError(pos_error)) / 100);
            scouted.AGILITY = (int)(player.Current_Ratings.AGILITY * (100 + GetScoutError(pos_error)) / 100);
            scouted.AWARENESS = (int)(player.Current_Ratings.AWARENESS * (100 + GetScoutError(pos_error)) / 100);
            scouted.BREAK_TACKLE = (int)(player.Current_Ratings.BREAK_TACKLE * (100 + GetScoutError(pos_error)) / 100);
            scouted.CARRYING = (int)(player.Current_Ratings.CARRYING * (100 + GetScoutError(pos_error)) / 100);
            scouted.CATCHING = (int)(player.Current_Ratings.CATCHING * (100 + GetScoutError(pos_error)) / 100);
            scouted.JUMPING = (int)(player.Current_Ratings.JUMPING * (100 + GetScoutError(pos_error)) / 100);
            scouted.KICK_ACCURACY = (int)(player.Current_Ratings.KICK_ACCURACY * (100 + GetScoutError(pos_error)) / 100);
            scouted.KICK_POWER = (int)(player.Current_Ratings.KICK_POWER * (100 + GetScoutError(pos_error)) / 100);
            scouted.KICK_RETURN = (int)(player.Current_Ratings.KICK_RETURN * (100 + GetScoutError(pos_error)) / 100);
            scouted.PASS_BLOCKING = (int)(player.Current_Ratings.PASS_BLOCKING * (100 + GetScoutError(pos_error)) / 100);
            scouted.RUN_BLOCKING = (int)(player.Current_Ratings.RUN_BLOCKING * (100 + GetScoutError(pos_error)) / 100);
            scouted.SPEED = (int)(player.Current_Ratings.SPEED * (100 + GetScoutError(pos_error)) / 100);
            scouted.STRENGTH = (int)(player.Current_Ratings.STRENGTH * (100 + GetScoutError(pos_error)) / 100);
            scouted.TACKLE = (int)(player.Current_Ratings.TACKLE * (100 + GetScoutError(pos_error)) / 100);
            scouted.THROW_ACCURACY = (int)(player.Current_Ratings.THROW_ACCURACY * (100 + GetScoutError(pos_error)) / 100);
            scouted.THROW_POWER = (int)(player.Current_Ratings.THROW_POWER * (100 + GetScoutError(pos_error)) / 100);
            
            this.Evals.Add(scouted);
        }






        public List<Player_Ratings> Evals_Player { get; set; }
    }




}
