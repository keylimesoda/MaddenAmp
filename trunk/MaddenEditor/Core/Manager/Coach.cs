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

namespace MaddenEditor.Core.Manager
{    
    public class CoachRatings
    {
        public int CHEMISTRY = 50;                          //  "CCHM";
        public int DEFENSE = 50;                            //  "CDEF";
        public int DEF_AGGR = 50;                           //  "CDTA";
        public int DEF_STRAT = 50;                          //  "CDTR";        
        public int ETHICS = 50;                             //  "CETH";
        public int KNOWLEDGE = 50;                          //  "CKNW";
        public int MOTIVATION = 50;                         //  "CMOT";
        public int OFFENSE = 50;                            //  "COFF";
        public int OFF_AGGR = 50;                           //  "COTA";
        public int OFF_STRAT = 50;                          //  "COTR";
        public int RB_CARRY_DIST = 50;                      //  "CRBT";
        public int DB_RATING = 50;                          //  "CRDB";
        public int DL_RATING = 50;                          //  "CRDL";
        public int K_RATING = 50;                           //  "CRKS";
        public int LB_RATING = 50;                          //  "CRLB";
        public int OL_RATING = 50;                          //  "CROL";
        public int P_RATING = 50;                           //  "CRPS";
        public int QB_RATING = 50;                          //  "CRQB";
        public int RB_RATING = 50;                          //  "CRRB";
        public int S_RATING = 50;                           //  "CRSA";
        public int WR_RATING = 50;                          //  "CRWR";

        public CoachRatings()
        {
            CHEMISTRY = 50;                          //  "CCHM";
            DEFENSE = 50;                            //  "CDEF";
            DEF_AGGR = 50;                           //  "CDTA";
            DEF_STRAT = 50;                          //  "CDTR";        
            ETHICS = 50;                             //  "CETH";
            KNOWLEDGE = 50;                          //  "CKNW";
            MOTIVATION = 50;                         //  "CMOT";
            OFFENSE = 50;                            //  "COFF";
            OFF_AGGR = 50;                           //  "COTA";
            OFF_STRAT = 50;                          //  "COTR";
            RB_CARRY_DIST = 50;                      //  "CRBT";
            DB_RATING = 50;                          //  "CRDB";
            DL_RATING = 50;                          //  "CRDL";
            K_RATING = 50;                        //  "CRKS";
            LB_RATING = 50;                          //  "CRLB";
            OL_RATING = 50;                          //  "CROL";
            P_RATING = 50;                        //  "CRPS";
            QB_RATING = 50;                          //  "CRQB";
            RB_RATING = 50;                          //  "CRRB";
            S_RATING = 50;                           //  "CRSA";
            WR_RATING = 50;                          //  "CRWR";
        }

        public void Read(BinaryReader binreader)
        {
            this.CHEMISTRY = binreader.ReadByte();          //  "CCHM";
            this.DEFENSE = binreader.ReadByte();                           //  "CDEF";
            this.DEF_AGGR = binreader.ReadByte();                          //  "CDTA";
            this.DEF_STRAT = binreader.ReadByte();                         //  "CDTR";            
            this.ETHICS = binreader.ReadByte();                            //  "CETH";
            this.KNOWLEDGE = binreader.ReadByte();                         //  "CKNW";
            this.MOTIVATION = binreader.ReadByte();                        //  "CMOT";
            this.OFFENSE = binreader.ReadByte();                           //  "COFF";
            this.OFF_AGGR = binreader.ReadByte();                          //  "COTA";
            this.OFF_STRAT = binreader.ReadByte();                         //  "COTR";
            this.RB_CARRY_DIST = binreader.ReadByte();                     //  "CRBT";
            this.DB_RATING = binreader.ReadByte();                         //  "CRDB";
            this.DL_RATING = binreader.ReadByte();                         //  "CRDL";
            this.K_RATING = binreader.ReadByte();                       //  "CRKS";
            this.LB_RATING = binreader.ReadByte();                         //  "CRLB";
            this.OL_RATING = binreader.ReadByte();                         //  "CROL";
            this.P_RATING = binreader.ReadByte();                       //  "CRPS";
            this.QB_RATING = binreader.ReadByte();                         //  "CRQB";
            this.RB_RATING = binreader.ReadByte();                         //  "CRRB";
            this.S_RATING = binreader.ReadByte();                          //  "CRSA";
            this.WR_RATING = binreader.ReadByte();                         //  "CRWR";
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)this.CHEMISTRY);          //CCHM
            binwriter.Write((byte)this.DEFENSE);            //CDEF
            binwriter.Write((byte)this.DEF_AGGR);           //CDTA
            binwriter.Write((byte)this.DEF_STRAT);          //CDTR
            binwriter.Write((byte)this.ETHICS);             //CETH
            binwriter.Write((byte)this.KNOWLEDGE);          //cknw
            binwriter.Write((byte)this.MOTIVATION);         //cmot
            binwriter.Write((byte)this.OFFENSE);            //coff
            binwriter.Write((byte)this.OFF_AGGR);           //cota
            binwriter.Write((byte)this.OFF_STRAT);          //cotr
            binwriter.Write((byte)this.RB_CARRY_DIST);      //crbt
            binwriter.Write((byte)this.DB_RATING);          //crdb
            binwriter.Write((byte)this.DL_RATING);          //crdl
            binwriter.Write((byte)this.K_RATING);        //crks
            binwriter.Write((byte)this.LB_RATING);          //crlb
            binwriter.Write((byte)this.OL_RATING);          //crol
            binwriter.Write((byte)this.P_RATING);        //crps
            binwriter.Write((byte)this.QB_RATING);          //crqb
            binwriter.Write((byte)this.RB_RATING);          //crrb
            binwriter.Write((byte)this.S_RATING);           //crsa
            binwriter.Write((byte)this.WR_RATING);          //crwr
        }
    }

    public class Pri
    {
        public int Importance = 50;
        public int Tendency = 0;
        public Dictionary<int, double> RatingValue;
        
        public Pri()
        {
            Importance = 50;
            Tendency = 0;
            RatingValue = new Dictionary<int, double>();
        }

        public void Read(BinaryReader binreader)
        {
            Importance = binreader.ReadByte();
            Tendency = binreader.ReadByte();
            int count = binreader.ReadByte();
            RatingValue.Clear();
            for (int c = 0; c < count; c++)
            {
                int skill = binreader.ReadByte();
                double skillrate = binreader.ReadDouble();
                RatingValue.Add(skill, skillrate);
            }
        }
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)Importance);
            binwriter.Write((byte)Tendency);
            binwriter.Write((byte)RatingValue.Count);
            foreach (KeyValuePair<int, double> rate in RatingValue)
            {
                binwriter.Write((byte)rate.Key);
                binwriter.Write(rate.Value);
            }
        }
    }
    
    public class Coach
    {
        #region Saved Vars
        public string NAME = "";                        //  "CLNA";
        public int COACH_ID = 0;                        //  "CCID";
        public int TEAM_ID = 1009;                      //  "TGID";
        public int POSITION = 0;                        //  "COPS";
        public int LAST_TEAM = 1009;                    //  "CLCT";
        public int CONTRACT_LENGTH = 0;                 //  "CCLN";
        public int SALARY = 0;                          //  "CSAL";
        public int years_exp;
        public bool USER_CONTROLLED = false;            //  "CFUC";
        public bool CPU_CONTROLLED = false;             //  "CFEX";
        public bool CPU_DRAFT_PLAYER = false;           //  "CFDA";
        public bool CPU_SIGN_DRAFT_PICKS = false;       //  "CFDP";
        public bool CPU_SIGN_FREE_AGENTS = false;       //  "CFFA";
        public bool CPU_FILL_ROSTERS = false;           //  "CFFR";
        public bool CPU_RESIGN_PLAYERS = false;         //  "CFRP";
        public bool CPU_MANAGE_DEPTH = false;           //  "CFRR";
        public bool CPU_SIGN_COACHES = false;           //  this is only applicable for GM/HC
        public int EGO = 50;
        public int PATIENCE = 50;
        public int KNOWLEDGE = 50;                      //  "CKNW";
        public int SPENDING = 50;
        public int LOYALTY = 50;
        public int RISK = 50;
        public int ETHICS = 50;                         //  "CETH";
        public int MORALE = 50;
        public int CHEMISTRY = 50;                      //  "CCHM";
        public int DEFENSE = 50;                        //  "CDEF";
        public int DEF_AGGR = 50;                       //  "CDTA";
        public int DEF_STRAT = 50;                      //  "CDTR";
        public int MOTIVATION = 50;                     //  "CMOT";
        public int OFFENSE = 50;                        //  "COFF";
        public int OFF_AGGR = 50;                       //  "COTA";
        public int OFF_STRAT = 50;                      //  "COTR";
        public int RB_CARRY_DIST = 50;                  //  "CRBT";
        public int DB_RATING = 50;                      //  "CRDB";
        public int DL_RATING = 50;                      //  "CRDL";
        public int K_RATING = 50;                       //  "CRKS";
        public int LB_RATING = 50;                      //  "CRLB";
        public int OL_RATING = 50;                      //  "CROL";
        public int P_RATING = 50;                       //  "CRPS";
        public int QB_RATING = 50;                      //  "CRQB";
        public int RB_RATING = 50;                      //  "CRRB";
        public int S_RATING = 50;                       //  "CRSA";
        public int WR_RATING = 50;                      //  "CRWR";
        public bool HC_GM = false;
        public bool HC_OC = false;
        public bool HC_DC = false;
        public bool CanBeInterviewed = true;
        public bool CanBeHired = true;
        public bool InPlayoffs = false;
        #endregion 

        public Dictionary<int, CoachHistory> History;
        public Dictionary<int, Pri> Priorities;
        public List<int> Interviews;
        public Dictionary<int,Dictionary<int, double>> Offers;
         
        public Coach()
        {
            NAME = "";                        //  "CLNA";
            COACH_ID = 0;                        //  "CCID";
            TEAM_ID = 1009;                      //  "TGID";
            POSITION = 0;                        //  "COPS";
            LAST_TEAM = 1009;                    //  "CLCT";
            CONTRACT_LENGTH = 0;                 //  "CCLN";
            SALARY = 0;                          //  "CSAL";
            years_exp = 0;
            USER_CONTROLLED = false;            //  "CFUC";
            CPU_DRAFT_PLAYER = false;           //  "CFDA";
            CPU_SIGN_DRAFT_PICKS = false;       //  "CFDP";
            CPU_SIGN_FREE_AGENTS = false;       //  "CFFA";
            CPU_FILL_ROSTERS = false;           //  "CFFR";
            CPU_RESIGN_PLAYERS = false;         //  "CFRP";
            CPU_MANAGE_DEPTH = false;           //  "CFRR";
            CPU_SIGN_COACHES = false;           //  this is only applicable for GM/HC
            EGO = 50;
            PATIENCE = 50;
            KNOWLEDGE = 50;                      //  "CKNW";
            SPENDING = 50;
            LOYALTY = 50;
            RISK = 50;
            ETHICS = 50;                         //  "CETH";
            MORALE = 50;

            CHEMISTRY = 50;                          //  "CCHM";
            DEFENSE = 50;                            //  "CDEF";
            DEF_AGGR = 50;                           //  "CDTA";
            DEF_STRAT = 50;                          //  "CDTR";        
            ETHICS = 50;                             //  "CETH";
            KNOWLEDGE = 50;                          //  "CKNW";
            MOTIVATION = 50;                         //  "CMOT";
            OFFENSE = 50;                            //  "COFF";
            OFF_AGGR = 50;                           //  "COTA";
            OFF_STRAT = 50;                          //  "COTR";
            RB_CARRY_DIST = 50;                      //  "CRBT";
            DB_RATING = 50;                          //  "CRDB";
            DL_RATING = 50;                          //  "CRDL";
            K_RATING = 50;                          //  "CRKS";
            LB_RATING = 50;                          //  "CRLB";
            OL_RATING = 50;                          //  "CROL";
            P_RATING = 50;                          //  "CRPS";
            QB_RATING = 50;                          //  "CRQB";
            RB_RATING = 50;                          //  "CRRB";
            S_RATING = 50;                           //  "CRSA";
            WR_RATING = 50;                          //  "CRWR";

            HC_GM = false;
            HC_OC = false;
            HC_DC = false;
            CanBeInterviewed = true;
            CanBeHired = true;
            InPlayoffs = false;

            History = new Dictionary<int, CoachHistory>();
            Priorities = new Dictionary<int, Pri>();
            Interviews = new List<int>();
            Offers = new Dictionary<int, Dictionary<int, double>>();
        }
        
        public Coach(CoachRecord rec)
        {
            NAME = rec.Name;
            COACH_ID = rec.CoachId;
            TEAM_ID = rec.TeamId;
            POSITION = rec.Position;
            LAST_TEAM = rec.LastTeam;
            CONTRACT_LENGTH = rec.ContractLength;
            SALARY = rec.Salary;
            years_exp = 0;
            USER_CONTROLLED = rec.UserControlled;
            CPU_CONTROLLED = rec.CPUControlled;
            CPU_DRAFT_PLAYER = rec.CPUDraftPlayer;
            CPU_SIGN_DRAFT_PICKS = rec.CPUSignDraftPicks;
            CPU_SIGN_FREE_AGENTS = rec.CPUSignFreeAgents;
            CPU_FILL_ROSTERS = rec.CPUFillRosters;
            CPU_RESIGN_PLAYERS = rec.CPUResignPlayers;
            CPU_MANAGE_DEPTH = rec.CPUManageDepth;
            if (POSITION == 0)
                CPU_SIGN_COACHES = true;
            else CPU_SIGN_COACHES = false;
            EGO = 50;
            PATIENCE = 50;
            KNOWLEDGE = rec.Knowledge;
            SPENDING = 50;
            LOYALTY = 50;
            RISK = 50;
            ETHICS = rec.Ethics;
            MORALE = 50;
            CHEMISTRY = rec.Chemistry;
            DEFENSE = rec.DefenseRating;
            DEF_AGGR = rec.DefensiveAggression;
            DEF_STRAT = rec.DefensiveStrategy;
            MOTIVATION = rec.Motivation;
            OFFENSE = rec.Offense;
            OFF_AGGR = rec.OffensiveAggression;
            OFF_STRAT = rec.OffensiveStrategy;
            RB_CARRY_DIST = rec.RBCarryDist;
            DB_RATING = rec.CoachDB;
            DL_RATING = rec.CoachDL;
            K_RATING = rec.CoachKS;
            LB_RATING = rec.CoachLB;
            OL_RATING = rec.CoachOL;
            P_RATING = rec.CoachPS;
            QB_RATING = rec.CoachQB;
            RB_RATING = rec.CoachRB;
            S_RATING = rec.CoachS;
            WR_RATING = rec.CoachWR;
            HC_GM = false;
            HC_OC = false;
            HC_DC = false;
            CanBeInterviewed = true;
            CanBeHired = true;
            InPlayoffs = false;
            
            History = new Dictionary<int, CoachHistory>();
            Priorities = new Dictionary<int, Pri>();
            Interviews = new List<int>();
            Offers = new Dictionary<int, Dictionary<int, double>>();
        }

        public Coach(CoachCollection rec)
        {
            NAME = rec.Name;
            COACH_ID = rec.CoachId;
            TEAM_ID = rec.TeamId;
            POSITION = rec.Position;
            LAST_TEAM = rec.LastTeam;
            CONTRACT_LENGTH = rec.ContractLength;
            SALARY = rec.Salary;
            years_exp = 0;
            USER_CONTROLLED = rec.UserControlled;
            CPU_CONTROLLED = rec.CPUControlled;
            CPU_DRAFT_PLAYER = rec.CPUDraftPlayer;
            CPU_SIGN_DRAFT_PICKS = rec.CPUSignDraftPicks;
            CPU_SIGN_FREE_AGENTS = rec.CPUSignFreeAgents;
            CPU_FILL_ROSTERS = rec.CPUFillRosters;
            CPU_RESIGN_PLAYERS = rec.CPUResignPlayers;
            CPU_MANAGE_DEPTH = rec.CPUManageDepth;
            if (POSITION == 0)
                CPU_SIGN_COACHES = true;
            else CPU_SIGN_COACHES = false;
            EGO = 50;
            PATIENCE = 50;
            KNOWLEDGE = rec.Knowledge;
            SPENDING = 50;
            LOYALTY = 50;
            RISK = 50;
            ETHICS = rec.Ethics;
            MORALE = 50;
            CHEMISTRY = rec.Chemistry;
            DEFENSE = rec.DefenseRating;
            DEF_AGGR = rec.DefensiveAggression;
            DEF_STRAT = rec.DefensiveStrategy;
            MOTIVATION = rec.Motivation;
            OFFENSE = rec.Offense;
            OFF_AGGR = rec.OffensiveAggression;
            OFF_STRAT = rec.OffensiveStrategy;
            RB_CARRY_DIST = rec.RBCarryDist;
            DB_RATING = rec.CoachDB;
            DL_RATING = rec.CoachDL;
            K_RATING = rec.CoachKS;
            LB_RATING = rec.CoachLB;
            OL_RATING = rec.CoachOL;
            P_RATING = rec.CoachPS;
            QB_RATING = rec.CoachQB;
            RB_RATING = rec.CoachRB;
            S_RATING = rec.CoachS;
            WR_RATING = rec.CoachWR;
            HC_GM = false;
            HC_OC = false;
            HC_DC = false;
            CanBeInterviewed = true;
            CanBeHired = true;
            InPlayoffs = false;

            History = new Dictionary<int, CoachHistory>();
            Priorities = new Dictionary<int, Pri>();
            Interviews = new List<int>();
            Offers = new Dictionary<int, Dictionary<int, double>>();
        }
        
        #region Methods

        public void SetOwner(OwnerRecord or)
        {
            this.USER_CONTROLLED = false;
            if (!or.CPUControlled)
                this.USER_CONTROLLED = true;

            this.CPU_DRAFT_PLAYER = or.DraftPlayers;
            this.CPU_SIGN_DRAFT_PICKS = or.SignDraftPicks;
            this.CPU_SIGN_FREE_AGENTS = or.SignFreeAgents;
            this.CPU_FILL_ROSTERS = or.FillRosters;
            this.CPU_RESIGN_PLAYERS = or.ResignPlayers;
            this.CPU_RESIGN_PLAYERS = or.ResignPlayers;
            this.CPU_MANAGE_DEPTH = or.ReorderDepthCharts;
            this.CPU_SIGN_COACHES = false;            
            if (this.POSITION == 0)
                this.CPU_SIGN_COACHES = true;
        }
        
        public double GetPositionProgRating(int pos)
        {
            if (pos == (int)MaddenPositions.QB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.QB_RATING * .30);
                    case 1:
                        return (double)(this.QB_RATING * .60);
                    case 3:
                        return (double)(this.QB_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.HB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.RB_RATING * .30);
                    case 1:
                        return (double)(this.RB_RATING * .60);
                    case 3:
                        return (double)(this.RB_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.WR)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.WR_RATING * .30);
                    case 1:
                        return (double)(this.WR_RATING * .60);
                    case 3:
                        return (double)(this.WR_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.FB)
            {
                int rating = ((this.OL_RATING * 3) + (this.RB_RATING) + (this.WR_RATING * 2)) / 6;

                switch (this.POSITION)
                {
                    case 0:
                        return (double)rating * .30;
                    case 1:
                        return (double)rating * .60;
                    case 3:
                        return (double)rating * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.TE)
            {
                int rating = ((this.OL_RATING * 2) + (this.WR_RATING * 3)) / 5;

                switch (this.POSITION)
                {
                    case 0:
                        return (double)rating * .30;
                    case 1:
                        return (double)rating * .60;
                    case 3:
                        return (double)rating * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.C || pos == (int)MaddenPositions.LG || pos == (int)MaddenPositions.LT || pos == (int)MaddenPositions.RG || pos == (int)MaddenPositions.RT)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.OL_RATING) * .30;
                    case 1:
                        return (double)(this.OL_RATING) * .60;
                    case 3:
                        return (double)(this.OL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.DT || pos == (int)MaddenPositions.LE || pos == (int)MaddenPositions.RE)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.DL_RATING) * .30;
                    case 2:
                        return (double)(this.DL_RATING) * .60;
                    case 3:
                        return (double)(this.DL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.LOLB || pos == (int)MaddenPositions.MLB || pos == (int)MaddenPositions.ROLB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.LB_RATING) * .30;
                    case 2:
                        return (double)(this.LB_RATING) * .60;
                    case 3:
                        return (double)(this.LB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.SS || pos == (int)MaddenPositions.FS)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.S_RATING) * .30;
                    case 2:
                        return (double)(this.S_RATING) * .60;
                    case 3:
                        return (double)(this.S_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.CB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.DB_RATING) * .30;
                    case 2:
                        return (double)(this.DB_RATING) * .60;
                    case 3:
                        return (double)(this.DB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.K)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.K_RATING) * .30;
                    case 3:
                        return (double)(this.K_RATING) * .60;
                    default:
                        return (double)(this.K_RATING) * .10;
                }
            }
            else if (pos == (int)MaddenPositions.P)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.P_RATING) * .30;
                    case 3:
                        return (double)(this.P_RATING) * .60;
                    default:
                        return (double)(this.P_RATING) * .10;
                }
            }

            else return 0;
        }

        public double GetMotivationProgRating(int pos)
        {
            if (pos <= 9)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION * .30);
                    case 1:
                        return (double)(this.MOTIVATION * .60);
                    case 3:
                        return (double)(this.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION * .30);
                    case 2:
                        return (double)(this.MOTIVATION * .60);
                    case 3:
                        return (double)(this.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION) * .30;
                    case 1:
                        return (double)(this.MOTIVATION) * .05;
                    case 2:
                        return (double)(this.MOTIVATION) * .05;
                    case 3:
                        return (double)(this.MOTIVATION) * .60;
                    default:
                        return 0;
                }
            }
            else return 0;
        }

        public double GetKnowledgeProgRating(int pos)
        {
            if (pos <= 9)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE * .30);
                    case 1:
                        return (double)(this.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE * .30);
                    case 2:
                        return (double)(this.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE) * .30;
                    case 1:
                        return (double)(this.KNOWLEDGE) * .05;
                    case 2:
                        return (double)(this.KNOWLEDGE) * .05;
                    case 3:
                        return (double)(this.KNOWLEDGE) * .60;
                    default:
                        return 0;
                }
            }
            else return 0;
        }

        public void InitHistory(EditorModel model)
        {
            History.Clear();

            foreach (TableRecordModel record in model.TableModels[EditorModel.COACHING_HISTORY_TABLE].GetRecords())
            {
                if (record.Deleted)
                    continue;
                CoachHistory hist = (CoachHistory)record;
                if (hist.CoachID != COACH_ID)
                    continue;
                int year = model.CurrentYear;
                year += hist.Season;
                if (!History.ContainsKey(year))
                    History.Add(year, hist);               
            }

            years_exp = History.Count;
        }
        #endregion
        
        #region File IO
        public void Read(BinaryReader binreader)
        {
            NAME = binreader.ReadString();
            COACH_ID = binreader.ReadUInt16();
            TEAM_ID = binreader.ReadUInt16();
            POSITION = binreader.ReadByte();
            LAST_TEAM = binreader.ReadByte();
            CONTRACT_LENGTH = binreader.ReadByte();
            SALARY = binreader.ReadUInt16();
            years_exp = binreader.ReadByte();
            USER_CONTROLLED = binreader.ReadBoolean();
            CPU_CONTROLLED = binreader.ReadBoolean();
            CPU_DRAFT_PLAYER = binreader.ReadBoolean();
            CPU_SIGN_DRAFT_PICKS = binreader.ReadBoolean();
            CPU_SIGN_FREE_AGENTS = binreader.ReadBoolean();
            CPU_FILL_ROSTERS = binreader.ReadBoolean();
            CPU_RESIGN_PLAYERS = binreader.ReadBoolean();
            CPU_MANAGE_DEPTH = binreader.ReadBoolean();
            CPU_SIGN_COACHES = binreader.ReadBoolean();
            EGO = binreader.ReadByte();
            PATIENCE = binreader.ReadByte();
            KNOWLEDGE = binreader.ReadByte();
            SPENDING = binreader.ReadByte();
            LOYALTY = binreader.ReadByte();
            RISK = binreader.ReadByte();
            ETHICS = binreader.ReadByte();
            MORALE = binreader.ReadByte();
            CHEMISTRY = binreader.ReadByte();                          //  "CCHM";
            DEFENSE = binreader.ReadByte();                            //  "CDEF";
            DEF_AGGR = binreader.ReadByte();                           //  "CDTA";
            DEF_STRAT = binreader.ReadByte();                          //  "CDTR";
            MOTIVATION = binreader.ReadByte();                         //  "CMOT";
            OFFENSE = binreader.ReadByte();                            //  "COFF";
            OFF_AGGR = binreader.ReadByte();                           //  "COTA";
            OFF_STRAT = binreader.ReadByte();                          //  "COTR";
            RB_CARRY_DIST = binreader.ReadByte();                      //  "CRBT";
            DB_RATING = binreader.ReadByte();                          //  "CRDB";
            DL_RATING = binreader.ReadByte();                          //  "CRDL";
            K_RATING = binreader.ReadByte();                           //  "CRKS";
            LB_RATING = binreader.ReadByte();                          //  "CRLB";
            OL_RATING = binreader.ReadByte();                          //  "CROL";
            P_RATING = binreader.ReadByte();                           //  "CRPS";
            QB_RATING = binreader.ReadByte();                          //  "CRQB";
            RB_RATING = binreader.ReadByte();                          //  "CRRB";
            S_RATING = binreader.ReadByte();                           //  "CRSA";
            WR_RATING = binreader.ReadByte();                          //  "CRWR";
            HC_GM = binreader.ReadBoolean();
            HC_OC = binreader.ReadBoolean();
            HC_DC = binreader.ReadBoolean();
            CanBeInterviewed = binreader.ReadBoolean();
            CanBeHired = binreader.ReadBoolean();
            InPlayoffs = binreader.ReadBoolean();

            int count = binreader.ReadByte();
            Priorities.Clear();
            for (int c = 0; c < count; c++)
            {
                int prikey = binreader.ReadByte();
                Pri pri = new Pri();
                pri.Read(binreader);
                Priorities.Add(prikey, pri);
            }
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(NAME);
            binwriter.Write((UInt16)COACH_ID);
            binwriter.Write((UInt16)TEAM_ID);
            binwriter.Write((byte)POSITION);
            binwriter.Write((byte)LAST_TEAM);
            binwriter.Write((byte)CONTRACT_LENGTH);
            binwriter.Write((UInt16)SALARY);
            binwriter.Write((byte)years_exp);
            binwriter.Write(USER_CONTROLLED);
            binwriter.Write(CPU_CONTROLLED);
            binwriter.Write(CPU_DRAFT_PLAYER);
            binwriter.Write(CPU_SIGN_DRAFT_PICKS);
            binwriter.Write(CPU_SIGN_FREE_AGENTS);
            binwriter.Write(CPU_FILL_ROSTERS);
            binwriter.Write(CPU_RESIGN_PLAYERS);
            binwriter.Write(CPU_SIGN_COACHES);
            binwriter.Write(CPU_MANAGE_DEPTH);
            binwriter.Write((byte)EGO);
            binwriter.Write((byte)PATIENCE);
            binwriter.Write((byte)KNOWLEDGE);
            binwriter.Write((byte)SPENDING);
            binwriter.Write((byte)LOYALTY);
            binwriter.Write((byte)RISK);
            binwriter.Write((byte)ETHICS);
            binwriter.Write((byte)MORALE);
            binwriter.Write((byte)this.CHEMISTRY);          //CCHM
            binwriter.Write((byte)this.DEFENSE);            //CDEF
            binwriter.Write((byte)this.DEF_AGGR);           //CDTA
            binwriter.Write((byte)this.DEF_STRAT);          //CDTR
            binwriter.Write((byte)this.MOTIVATION);         //cmot
            binwriter.Write((byte)this.OFFENSE);            //coff
            binwriter.Write((byte)this.OFF_AGGR);           //cota
            binwriter.Write((byte)this.OFF_STRAT);          //cotr
            binwriter.Write((byte)this.RB_CARRY_DIST);      //crbt
            binwriter.Write((byte)this.DB_RATING);          //crdb
            binwriter.Write((byte)this.DL_RATING);          //crdl
            binwriter.Write((byte)this.K_RATING);           //crks
            binwriter.Write((byte)this.LB_RATING);          //crlb
            binwriter.Write((byte)this.OL_RATING);          //crol
            binwriter.Write((byte)this.P_RATING);           //crps
            binwriter.Write((byte)this.QB_RATING);          //crqb
            binwriter.Write((byte)this.RB_RATING);          //crrb
            binwriter.Write((byte)this.S_RATING);           //crsa
            binwriter.Write((byte)this.WR_RATING);          //crwr
            binwriter.Write(HC_GM);
            binwriter.Write(HC_OC);
            binwriter.Write(HC_DC);
            binwriter.Write(CanBeInterviewed);
            binwriter.Write(CanBeHired);
            binwriter.Write(InPlayoffs);

            binwriter.Write((byte)Priorities.Count);
            foreach (KeyValuePair<int, Pri> prior in Priorities)
            {
                binwriter.Write(prior.Key);
                prior.Value.Write(binwriter);
            }
        }
        #endregion

        
    }

}
