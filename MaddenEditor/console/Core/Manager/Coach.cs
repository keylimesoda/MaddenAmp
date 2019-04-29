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
        public int KICK_RATING = 50;                        //  "CRKS";
        public int LB_RATING = 50;                          //  "CRLB";
        public int OL_RATING = 50;                          //  "CROL";
        public int PUNT_RATING = 50;                        //  "CRPS";
        public int QB_RATING = 50;                          //  "CRQB";
        public int RB_RATING = 50;                          //  "CRRB";
        public int S_RATING = 50;                           //  "CRSA";
        public int WR_RATING = 50;                          //  "CRWR";

        public CoachRatings()
        {

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
            this.KICK_RATING = binreader.ReadByte();                       //  "CRKS";
            this.LB_RATING = binreader.ReadByte();                         //  "CRLB";
            this.OL_RATING = binreader.ReadByte();                         //  "CROL";
            this.PUNT_RATING = binreader.ReadByte();                       //  "CRPS";
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
            binwriter.Write((byte)this.KICK_RATING);        //crks
            binwriter.Write((byte)this.LB_RATING);          //crlb
            binwriter.Write((byte)this.OL_RATING);          //crol
            binwriter.Write((byte)this.PUNT_RATING);        //crps
            binwriter.Write((byte)this.QB_RATING);          //crqb
            binwriter.Write((byte)this.RB_RATING);          //crrb
            binwriter.Write((byte)this.S_RATING);           //crsa
            binwriter.Write((byte)this.WR_RATING);          //crwr
        }
    }

    public class Coach
    {
        #region Members
        public CoachRatings ratings;

        #region Coach record members
        public int AGE = 0;                             //  "CAGE";
        public int BIGGEST_LOSS_MARGIN = 0;             //  "CBLM";
        public int CBSZ = 0;                            //  "CBSZ";     // body size
        public int BIGGEST_WIN_MARGIN = 0;              //  "CBWM";
        public int COACH_ID = 0;                        //  "CCID";
        public int CONTRACT_LENGTH = 0;                 //  "CCLN";
        public int CAREER_LOSSES = 0;                   //  "CCLO";
        public int CAREER_LONGEST_LOSING_STREAK = 0;    //  "CCLS";
        public int PLAYOFF_LOSSES = 0;                  //  "CCPL";
        public int PLAYOFFS_MADE = 0;                   //  "CCPM";
        public int CCPR = 0;                            //  "CCPR";     // points for
        public int PLAYFF_WINS = 0;                     //  "CCPW";
        public int CCTC = 0;                            //  "CCTC";
        public int CAREER_TIES = 0;                     //  "CCTI";
        public int CAREER_WINS = 0;                     //  "CCWI";
        public int WINNING_SEASONS = 0;                 //  "CCWS";
        public int DEFENSIVE_PLAYBOOK = 0;              //  "CDID";
        public int CDTA = 0;                            //  "CDTA";
        public int CDTR = 0;                            //  "CDTR";
        public int DEFENSE_TYPE = 0;                    //  "CDTY";
        public int CDWS = 0;                            //  "CDWS";     //signed int 6bit
        public bool CFCO = false;                       //  "CFCO";
        public bool DRAFT_PLAYER = false;               //  "CFDA";
        public bool SIGN_DRAFT_PICKS = false;           //  "CFDP";
        public bool CFEX = false;                       //  "CFEX";
        public bool SIGN_FREE_AGENTS = false;           //  "CFFA";
        public bool FILL_ROSTERS = false;               //  "CFFR";
        public bool CFHL = false;                       //  "CFHL";
        public bool RESIGN_PLAYERS = false;             //  "CFRP";
        public bool MANAGE_DEPTH = false;               //  "CFRR";
        public int CFSH = 0;                            //  "CFSH";
        public bool USER_CONTROLLED = false;            //  "CFUC";
        public int CHAR = 0;                            //  "CHAR";
        public int HEIGHT = 0;                          //  "CHGT";
        public int HEADHAIR_ID = 0;                     //  "CHID";
        public int CHSD = 0;                            //  "CHSD";
        public int CHTY = 0;                            //  "CHTY";
        public int LAST_TEAM = 0;                       //  "CLCT";
        public string NAME = "";                        //  "CLNA";
        public int CLTF = 0;                            //  "CLTF";     // last team?
        public int CLTR = 0;                            //  "CLTR";     // last team?
        public int COAP = 0;                            //  "COAP";
        public int COCI = 0;                            //  "COCI";
        public int COCT = 0;                            //  "COCT";        
        public bool CODA = false;                       //  "CODA";
        public bool CODP = false;                       //  "CODP";
        public bool COEX = false;                       //  "COEX";
        public bool COFA = false;                       //  "COFA";
        public int COFF = 0;                            //  "COFF";
        public bool COFR = false;                       //  "COFR";
        public int COPL = 0;                            //  "COPL";
        public int POSITION = 0;                        //  "COPS";
        public bool CORP = false;                       //  "CORP";
        public bool CORR = false;                       //  "CORR";
        public int COACH_OF_THE_YEAR = 0;               //  "COTY";
        public int CPAG = 0;                            //  "CPAG";
        public int OFFENSE_PLAYBOOK_ID = 0;             //  "CPID";
        public int FaceID = 0;                          //  "CPSF";
        public int CPWS = 0;                            //  "CPWS";
        public int APPROVAL_RATING = 0;                 //  "CRAT";
        public int CRWS = 0;                            //  "CRWS";
        public int CRYL = 0;                            //  "CRYL";
        public int SALARY = 0;                          //  "CSAL";
        public int SUPERBOWL_LOSES = 0;                 //  "CSBL";
        public int CSBS = 0;                            //  "CSBS";
        public int SUPERBOWL_WINS = 0;                  //  "CSBW";
        public int SKIN_COLOR = 0;                      //  "CSKI";
        public int CSLM = 0;                            //  "CSLM";
        public int SEASON_LOSSES = 0;                   //  "CSLO";
        public int CSLS = 0;                            //  "CSLS";
        public int CSPA = 0;                            //  "CSPA";
        public bool CSPC = false;                            //  "CSPC";
        public int CSPF = 0;                            //  "CSPF";
        public int SEASON_TIES = 0;                     //  "CSTI";
        public int SEASON_WINS = 0;                     //  "CSWI";
        public int SEASON_BIGGEST_WIN_MARGIN = 0;       //  "CSWM";
        public int SEASON_WINNING_STREAK = 0;           //  "CSWS";
        public int PORTRAIT = 0;                        //  "CSXP";
        public bool COACH_GLASSES = false;              //  "CTgw";
        public int CTHG = 0;                            //  "CThg";
        public bool CWPL = false;                       //  "CWPL";     // was player, bool 1 = yes
        public int CWST = 0;                            //  "CWST";
        public int CWWS = 0;                            //  "CWWS";
        public int TEAM_ID = 0;                         //  "TGID";

        #endregion
        #endregion

        public Coach()
        {
            ratings = new CoachRatings();
        }
        
        public Coach(CoachRecord rec)        
        {
            ratings = new CoachRatings();

            this.AGE = rec.Age;
            this.BIGGEST_LOSS_MARGIN = rec.BiggestLossMargin;
            this.CBSZ = rec.BodySize;
            this.BIGGEST_WIN_MARGIN = rec.BiggestWinMargin;
            this.ratings.CHEMISTRY = rec.Chemistry;
            this.COACH_ID = rec.CoachId;
            this.CONTRACT_LENGTH = rec.ContractLength;
            this.CAREER_LOSSES = rec.CareerLosses;
            this.CAREER_LONGEST_LOSING_STREAK = rec.CareerLosingStreak;
            this.PLAYOFF_LOSSES = rec.PlayoffLosses;
            this.PLAYOFFS_MADE = rec.PlayoffsMade;
            this.CCPR = rec.Ccpr;
            this.PLAYFF_WINS = rec.PlayoffWins;
            this.CCTC = rec.Cctc;
            this.CAREER_TIES = rec.CareerTies;
            this.CAREER_WINS = rec.CareerWins;
            this.WINNING_SEASONS = rec.WinningSeasons;
            this.ratings.DEFENSE = rec.DefenseRating;
            this.DEFENSIVE_PLAYBOOK = rec.DefensivePlaybook;
            this.ratings.DEF_AGGR = rec.DefensiveAggression;
            this.ratings.DEF_STRAT = rec.DefensiveStrategy;            
            this.CDWS = rec.Cdws;
            this.ratings.ETHICS = rec.Ethics;
            this.CFCO = rec.cfco;
            this.DRAFT_PLAYER = rec.CPUDraftPlayer;
            this.SIGN_DRAFT_PICKS = rec.CPUSignDraftPicks;
            this.CFEX = rec.CPUControlled;
            this.SIGN_FREE_AGENTS = rec.CPUSignFreeAgents;
            this.FILL_ROSTERS = rec.CPUFillRosters;
            this.CFHL = rec.cfhl;
            this.RESIGN_PLAYERS = rec.CPUResignPlayers;
            this.MANAGE_DEPTH = rec.CPUManageDepth;
            this.CFSH = rec.Cfsh;
            this.USER_CONTROLLED = rec.UserControlled;
            this.CHAR = rec.Char;
            this.HEIGHT = rec.height;
            this.HEADHAIR_ID = rec.HeadHair;
            this.CHSD = rec.Chsd;
            this.CHTY = rec.Chty;
            this.ratings.KNOWLEDGE = rec.Knowledge;
            this.LAST_TEAM = rec.LastTeam;
            this.NAME = rec.Name;
            this.CLTF = rec.LastTeamFranchise;
            this.CLTR = rec.LastTeamRelocated;
            this.ratings.MOTIVATION = rec.Motivation;
            this.COAP = rec.Coap;
            this.COCI = rec.Coci;
            this.COCT = rec.Coct;
            this.CODA = rec.Coda;
            this.CODP = rec.Codp;
            this.COEX = rec.Coex;
            this.COFA = rec.Cofa;
            this.ratings.OFFENSE = rec.Offense;
            this.COFR = rec.Cofr;
            this.COPL = rec.Copl;
            this.POSITION = rec.Position;
            this.CORP = rec.Corp;
            this.CORR = rec.Corr;
            this.ratings.OFF_AGGR = rec.DefensiveAggression;
            this.ratings.OFF_STRAT = rec.OffensiveStrategy;
            this.COACH_OF_THE_YEAR = rec.CoachOfTheYear;
            this.CPAG = rec.Cpag;
            this.OFFENSE_PLAYBOOK_ID = OFFENSE_PLAYBOOK_ID;
            this.FaceID = rec.FaceId;
            this.CPWS = rec.PlayoffWinStreak;
            this.APPROVAL_RATING = rec.ApprovalRating;
            this.ratings.RB_CARRY_DIST = rec.RBCarryDist;
            this.ratings.DB_RATING = rec.CoachDB;
            this.ratings.DL_RATING = rec.CoachDL;
            this.ratings.KICK_RATING = rec.KickerRating;
            this.ratings.LB_RATING = rec.CoachLB;
            this.ratings.OL_RATING = rec.CoachOL;
            this.ratings.PUNT_RATING = rec.PuntRating;
            this.ratings.QB_RATING = rec.CoachQB;
            this.ratings.RB_RATING = rec.CoachRB;
            this.ratings.S_RATING = rec.CoachSafety;
            this.ratings.WR_RATING = rec.CoachWR;
            this.CRWS = rec.Crws;
            this.CRYL = rec.Cryl;
            this.SALARY = rec.Salary;
            this.SUPERBOWL_LOSES = rec.SuperBowlLoses;
            this.CSBS = rec.SuperBowlStreak;
            this.SUPERBOWL_WINS = rec.SuperBowlWins;
            this.SKIN_COLOR = rec.SkinColor;
            this.CSLM = rec.Cslm;
            this.SEASON_LOSSES = rec.SeasonLosses;
            this.CSLS = rec.Csls;
            this.CSPA = rec.Cspa;
            this.CSPC = rec.Cspc;
            this.CSPF = rec.Cspf;
            this.SEASON_TIES = rec.SeasonTies;
            this.SEASON_WINS = rec.SeasonWins;
            this.SEASON_BIGGEST_WIN_MARGIN = rec.SeasonBigWin;
            this.SEASON_WINNING_STREAK = rec.SeasonWinStreak;
            this.PORTRAIT = rec.Coachpic;
            this.COACH_GLASSES = rec.CoachGlasses;
            this.CTHG = rec.Cthg;
            this.CWPL = rec.WasPlayer;
            this.CWST = rec.Cwst;
            this.CWWS = rec.Cwws;
            this.TEAM_ID = rec.TeamId;
        }

        #region Methods

        public void UpdateCoach(CoachRecord rec)
        {
            this.AGE = rec.Age;
            this.BIGGEST_LOSS_MARGIN = rec.BiggestLossMargin;
            this.CBSZ = rec.BodySize;
            this.BIGGEST_WIN_MARGIN = rec.BiggestWinMargin;
            this.ratings.CHEMISTRY = rec.Chemistry;
            this.COACH_ID = rec.CoachId;
            this.CONTRACT_LENGTH = rec.ContractLength;
            this.CAREER_LOSSES = rec.CareerLosses;
            this.CAREER_LONGEST_LOSING_STREAK = rec.CareerLosingStreak;
            this.PLAYOFF_LOSSES = rec.PlayoffLosses;
            this.PLAYOFFS_MADE = rec.PlayoffsMade;
            this.CCPR = rec.Ccpr;
            this.PLAYFF_WINS = rec.PlayoffWins;
            this.CCTC = rec.Cctc;
            this.CAREER_TIES = rec.CareerTies;
            this.CAREER_WINS = rec.CareerWins;
            this.WINNING_SEASONS = rec.WinningSeasons;
            this.ratings.DEFENSE = rec.DefenseRating;
            this.DEFENSIVE_PLAYBOOK = rec.DefensivePlaybook;
            this.ratings.DEF_AGGR = rec.DefensiveAggression;
            this.ratings.DEF_STRAT = rec.DefensiveStrategy;
            this.DEFENSE_TYPE = rec.DefenseType;
            this.CDWS = rec.Cdws;
            this.ratings.ETHICS = rec.Ethics;
            this.CFCO = rec.cfco;
            this.DRAFT_PLAYER = rec.CPUDraftPlayer;
            this.SIGN_DRAFT_PICKS = rec.CPUSignDraftPicks;
            this.CFEX = rec.CPUControlled;
            this.SIGN_FREE_AGENTS = rec.CPUSignFreeAgents;
            this.FILL_ROSTERS = rec.CPUFillRosters;
            this.CFHL = rec.cfhl;
            this.RESIGN_PLAYERS = rec.CPUResignPlayers;
            this.MANAGE_DEPTH = rec.CPUManageDepth;
            this.CFSH = rec.Cfsh;
            this.USER_CONTROLLED = rec.UserControlled;
            this.CHAR = rec.Char;
            this.HEIGHT = rec.height;
            this.HEADHAIR_ID = rec.HeadHair;
            this.CHSD = rec.Chsd;
            this.CHTY = rec.Chty;
            this.ratings.KNOWLEDGE = rec.Knowledge;
            this.LAST_TEAM = rec.LastTeam;
            this.NAME = rec.Name;
            this.CLTF = rec.LastTeamFranchise;
            this.CLTR = rec.LastTeamRelocated;
            this.ratings.MOTIVATION = rec.Motivation;
            this.COAP = rec.Coap;
            this.COCI = rec.Coci;
            this.COCT = rec.Coct;
            this.CODA = rec.Coda;
            this.CODP = rec.Codp;
            this.COEX = rec.Coex;
            this.COFA = rec.Cofa;
            this.ratings.OFFENSE = rec.Offense;
            this.COFR = rec.Cofr;
            this.COPL = rec.Copl;
            this.POSITION = rec.Position;
            this.CORP = rec.Corp;
            this.CORR = rec.Corr;
            this.ratings.OFF_AGGR = rec.DefensiveAggression;
            this.ratings.OFF_STRAT = rec.OffensiveStrategy;
            this.COACH_OF_THE_YEAR = rec.CoachOfTheYear;
            this.CPAG = rec.Cpag;
            this.OFFENSE_PLAYBOOK_ID = OFFENSE_PLAYBOOK_ID;
            this.FaceID = rec.FaceId;
            this.CPWS = rec.PlayoffWinStreak;
            this.APPROVAL_RATING = rec.ApprovalRating;
            this.ratings.RB_CARRY_DIST = rec.RBCarryDist;
            this.ratings.DB_RATING = rec.CoachDB;
            this.ratings.DL_RATING = rec.CoachDL;
            this.ratings.KICK_RATING = rec.KickerRating;
            this.ratings.LB_RATING = rec.CoachLB;
            this.ratings.OL_RATING = rec.CoachOL;
            this.ratings.PUNT_RATING = rec.PuntRating;
            this.ratings.QB_RATING = rec.CoachQB;
            this.ratings.RB_RATING = rec.CoachRB;
            this.ratings.S_RATING = rec.CoachSafety;
            this.ratings.WR_RATING = rec.CoachWR;
            this.CRWS = rec.Crws;
            this.CRYL = rec.Cryl;
            this.SALARY = rec.Salary;
            this.SUPERBOWL_LOSES = rec.SuperBowlLoses;
            this.CSBS = rec.SuperBowlStreak;
            this.SUPERBOWL_WINS = rec.SuperBowlWins;
            this.SKIN_COLOR = rec.SkinColor;
            this.CSLM = rec.Cslm;
            this.SEASON_LOSSES = rec.SeasonLosses;
            this.CSLS = rec.Csls;
            this.CSPA = rec.Cspa;
            this.CSPC = rec.Cspc;
            this.CSPF = rec.Cspf;
            this.SEASON_TIES = rec.SeasonTies;
            this.SEASON_WINS = rec.SeasonWins;
            this.SEASON_BIGGEST_WIN_MARGIN = rec.SeasonBigWin;
            this.SEASON_WINNING_STREAK = rec.SeasonWinStreak;
            this.PORTRAIT = rec.Coachpic;
            this.COACH_GLASSES = rec.CoachGlasses;
            this.CTHG = rec.Cthg;
            this.CWPL = rec.WasPlayer;
            this.CWST = rec.Cwst;
            this.CWWS = rec.Cwws;
            this.TEAM_ID = rec.TeamId;
        }

        public double GetPositionProgRating(int pos)
        {
            if (pos == (int)MaddenPositions.QB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.QB_RATING * .30);
                    case 1:
                        return (double)(this.ratings.QB_RATING * .60);
                    case 3:
                        return (double)(this.ratings.QB_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.HB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.RB_RATING * .30);
                    case 1:
                        return (double)(this.ratings.RB_RATING * .60);
                    case 3:
                        return (double)(this.ratings.RB_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.WR)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.WR_RATING * .30);
                    case 1:
                        return (double)(this.ratings.WR_RATING * .60);
                    case 3:
                        return (double)(this.ratings.WR_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.FB)
            {
                int rating = ((this.ratings.OL_RATING * 3) + (this.ratings.RB_RATING) + (this.ratings.WR_RATING * 2)) / 6;

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
                int rating = ((this.ratings.OL_RATING * 2) + (this.ratings.WR_RATING * 3)) / 5;

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
                        return (double)(this.ratings.OL_RATING) * .30;
                    case 1:
                        return (double)(this.ratings.OL_RATING) * .60;
                    case 3:
                        return (double)(this.ratings.OL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.DT || pos == (int)MaddenPositions.LE || pos == (int)MaddenPositions.RE)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.DL_RATING) * .30;
                    case 2:
                        return (double)(this.ratings.DL_RATING) * .60;
                    case 3:
                        return (double)(this.ratings.DL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.LOLB || pos == (int)MaddenPositions.MLB || pos == (int)MaddenPositions.ROLB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.LB_RATING) * .30;
                    case 2:
                        return (double)(this.ratings.LB_RATING) * .60;
                    case 3:
                        return (double)(this.ratings.LB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.SS || pos == (int)MaddenPositions.FS)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.S_RATING) * .30;
                    case 2:
                        return (double)(this.ratings.S_RATING) * .60;
                    case 3:
                        return (double)(this.ratings.S_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.CB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.DB_RATING) * .30;
                    case 2:
                        return (double)(this.ratings.DB_RATING) * .60;
                    case 3:
                        return (double)(this.ratings.DB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.K)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.KICK_RATING) * .30;
                    case 3:
                        return (double)(this.ratings.KICK_RATING) * .60;
                    default:
                        return (double)(this.ratings.KICK_RATING) * .10;
                }
            }
            else if (pos == (int)MaddenPositions.P)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.PUNT_RATING) * .30;
                    case 3:
                        return (double)(this.ratings.PUNT_RATING) * .60;
                    default:
                        return (double)(this.ratings.PUNT_RATING) * .10;
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
                        return (double)(this.ratings.MOTIVATION * .30);
                    case 1:
                        return (double)(this.ratings.MOTIVATION * .60);
                    case 3:
                        return (double)(this.ratings.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.MOTIVATION * .30);
                    case 2:
                        return (double)(this.ratings.MOTIVATION * .60);
                    case 3:
                        return (double)(this.ratings.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.MOTIVATION) * .30;
                    case 1:
                        return (double)(this.ratings.MOTIVATION) * .05;
                    case 2:
                        return (double)(this.ratings.MOTIVATION) * .05;
                    case 3:
                        return (double)(this.ratings.MOTIVATION) * .60;
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
                        return (double)(this.ratings.KNOWLEDGE * .30);
                    case 1:
                        return (double)(this.ratings.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.ratings.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.KNOWLEDGE * .30);
                    case 2:
                        return (double)(this.ratings.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.ratings.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.ratings.KNOWLEDGE) * .30;
                    case 1:
                        return (double)(this.ratings.KNOWLEDGE) * .05;
                    case 2:
                        return (double)(this.ratings.KNOWLEDGE) * .05;
                    case 3:
                        return (double)(this.ratings.KNOWLEDGE) * .60;
                    default:
                        return 0;
                }
            }
            else return 0;
        }

        #endregion



        #region File IO
        public void Read(BinaryReader binreader)
        {
            this.ratings.Read(binreader);

            this.AGE = binreader.ReadByte();                                //  "CAGE";
            this.BIGGEST_LOSS_MARGIN = binreader.ReadByte();                //  "CBLM";
            this.CBSZ = binreader.ReadByte();                               //  "CBSZ";
            this.BIGGEST_WIN_MARGIN = binreader.ReadByte();                 //  "CBWM";            
            this.COACH_ID = binreader.ReadInt16();                          //  "CCID";
            this.CONTRACT_LENGTH = binreader.ReadByte();                    //  "CCLN";
            this.CAREER_LOSSES = binreader.ReadInt16();                     //  "CCLO";
            this.CAREER_LONGEST_LOSING_STREAK = binreader.ReadInt16();      //  "CCLS";
            this.PLAYOFF_LOSSES = binreader.ReadInt16();                    //  "CCPL";
            this.PLAYOFFS_MADE = binreader.ReadByte();                      //  "CCPM";
            this.CCPR = binreader.ReadInt16();                              //  "CCPR";
            this.PLAYFF_WINS = binreader.ReadByte();                        //  "CCPW";
            this.CCTC = binreader.ReadByte();                               //  "CCTC";
            this.CAREER_TIES = binreader.ReadInt16();                       //  "CCTI";
            this.CAREER_WINS = binreader.ReadInt16();                       //  "CCWI";
            this.WINNING_SEASONS = binreader.ReadByte();                    //  "CCWS";            
            this.DEFENSIVE_PLAYBOOK = binreader.ReadByte();                 //  "CDID";
            this.CDTA = binreader.ReadByte();                               //  "CDTA";
            this.CDTR = binreader.ReadByte();                               //  "CDTR";
            this.DEFENSE_TYPE = binreader.ReadByte();                       // CDTY
            this.CDWS = binreader.ReadByte();                               //  "CDWS";
            this.CFCO = binreader.ReadBoolean();                            //  "CFCO";
            this.DRAFT_PLAYER = binreader.ReadBoolean();                    //  "CFDA";
            this.SIGN_DRAFT_PICKS = binreader.ReadBoolean();                //  "CFDP";
            this.CFEX = binreader.ReadBoolean();                            //  "CFEX";
            this.SIGN_FREE_AGENTS = binreader.ReadBoolean();                //  "CFFA";
            this.FILL_ROSTERS = binreader.ReadBoolean();                    //  "CFFR";
            this.CFHL = binreader.ReadBoolean();                            //  "CFHL";
            this.RESIGN_PLAYERS = binreader.ReadBoolean();                  //  "CFRP";
            this.MANAGE_DEPTH = binreader.ReadBoolean();                    //  "CFRR";
            this.CFSH = binreader.ReadByte();                               //  "CFSH";
            this.USER_CONTROLLED = binreader.ReadBoolean();                 //  "CFUC";
            this.CHAR = binreader.ReadByte();                               //  "CHAR";
            this.HEIGHT = binreader.ReadByte();                             //  "CHGT";
            this.HEADHAIR_ID = binreader.ReadByte();                        //  "CHID";
            this.CHSD = binreader.ReadByte();                               //  "CHSD";
            this.CHTY = binreader.ReadByte();                               //  "CHTY";            
            this.LAST_TEAM = binreader.ReadInt16();                         //  "CLCT";
            this.NAME = binreader.ReadString();                             //  "CLNA";
            this.CLTF = binreader.ReadInt16();                              //  "CLTF";
            this.CLTR = binreader.ReadInt16();                              //  "CLTR";            
            this.COAP = binreader.ReadByte();                               //  "COAP";
            this.COCI = binreader.ReadByte();                               //  "COCI";
            this.COCT = binreader.ReadByte();                               //  "COCT";
            this.CODA = binreader.ReadBoolean();                            //  "CODA";
            this.CODP = binreader.ReadBoolean();                            //  "CODP";
            this.COEX = binreader.ReadBoolean();                            //  "COEX";
            this.COFA = binreader.ReadBoolean();                            //  "COFA";
            this.COFF = binreader.ReadByte();                               //  "COFF" 
            this.COFR = binreader.ReadBoolean();                            //  "COFR";
            this.COPL = binreader.ReadByte();                               //  "COPL";
            this.POSITION = binreader.ReadByte();                           //  "COPS";
            this.CORP = binreader.ReadBoolean();                            //  "CORP";
            this.CORR = binreader.ReadBoolean();                            //  "CORR";            
            this.COACH_OF_THE_YEAR = binreader.ReadByte();                               //  "COTY";
            this.CPAG = binreader.ReadInt16();                              //  "CPAG";
            this.OFFENSE_PLAYBOOK_ID = binreader.ReadByte();                //  "CPID";
            this.FaceID = binreader.ReadInt16();                            //  "CPSF";
            this.CPWS = binreader.ReadByte();                               //  "CPWS";

            this.APPROVAL_RATING = binreader.ReadByte();                    //  "CRAT";           
            this.CRWS = binreader.ReadInt16();                              //  "CRWS";
            this.CRYL = binreader.ReadByte();                               //  "CRYL";
            this.SALARY = binreader.ReadInt16();                            //  "CSAL";
            this.SUPERBOWL_LOSES = binreader.ReadByte();                    //  "CSBL";
            this.CSBS = binreader.ReadByte();                               //  "CSBS";
            this.SUPERBOWL_WINS = binreader.ReadByte();                     //  "CSBW";
            this.SKIN_COLOR = binreader.ReadByte();                         //  "CSKI";
            this.CSLM = binreader.ReadByte();                               //  "CSLM";
            this.SEASON_LOSSES = binreader.ReadByte();                      //  "CSLO";
            this.CSLS = binreader.ReadByte();                               //  "CSLS";
            this.CSPA = binreader.ReadInt16();                              //  "CSPA";
            this.CSPC = binreader.ReadBoolean();                            //  "CSPC";
            this.CSPF = binreader.ReadInt16();                              //  "CSPF";
            this.SEASON_TIES = binreader.ReadByte();                        //  "CSTI";
            this.SEASON_WINS = binreader.ReadByte();                        //  "CSWI";
            this.SEASON_BIGGEST_WIN_MARGIN = binreader.ReadByte();          //  "CSWM";
            this.SEASON_WINNING_STREAK = binreader.ReadByte();              //  "CSWS";
            this.PORTRAIT = binreader.ReadInt16();                          //  "CSXP";
            this.COACH_GLASSES = binreader.ReadBoolean();                   //  "CTgw";
            this.CTHG = binreader.ReadByte();                               //  "CThg";
            this.CWPL = binreader.ReadBoolean();                            //  "CWPL";
            this.CWST = binreader.ReadByte();                               //  "CWST";
            this.CWWS = binreader.ReadByte();                               //  "CWWS";
            this.TEAM_ID = binreader.ReadInt16();                           //  "TGID";
        }
        
        public void Write(BinaryWriter binwriter)
        {
            this.ratings.Write(binwriter);

            binwriter.Write((byte)AGE);                             //cage
            binwriter.Write((byte)BIGGEST_LOSS_MARGIN);             //cblm
            binwriter.Write((byte)CBSZ);                            //cbsz
            binwriter.Write((byte)BIGGEST_WIN_MARGIN);              //cbwm
            binwriter.Write((UInt16)COACH_ID);                      //ccid
            binwriter.Write((byte)CONTRACT_LENGTH);                 //ccln
            binwriter.Write((UInt16)CAREER_LOSSES);                 //cclo
            binwriter.Write((UInt16)CAREER_LONGEST_LOSING_STREAK);  //ccls
            binwriter.Write((UInt16)PLAYOFF_LOSSES);                //ccpl
            binwriter.Write((byte)PLAYOFFS_MADE);                   //ccpm
            binwriter.Write((UInt16)CCPR);                          //ccpr
            binwriter.Write((byte)PLAYFF_WINS);                     //ccpw
            binwriter.Write((byte)CCTC);                            //cctc
            binwriter.Write((UInt16)CAREER_TIES);                   //ccti
            binwriter.Write((UInt16)CAREER_WINS);                   //ccwi
            binwriter.Write((byte)WINNING_SEASONS);                 //ccws
            binwriter.Write((byte)DEFENSIVE_PLAYBOOK);              //cdid
            binwriter.Write((byte)CDTA);                            //cdta
            binwriter.Write((byte)CDTR);                            //cdtr
            binwriter.Write((byte)DEFENSE_TYPE);                    //cdty
            binwriter.Write((byte)CDWS);                            //cdws
            binwriter.Write(CFCO);
            binwriter.Write(DRAFT_PLAYER);
            binwriter.Write(SIGN_DRAFT_PICKS);
            binwriter.Write(CFEX);
            binwriter.Write(SIGN_FREE_AGENTS);
            binwriter.Write(FILL_ROSTERS);
            binwriter.Write(CFHL);
            binwriter.Write(RESIGN_PLAYERS);
            binwriter.Write(MANAGE_DEPTH);
            binwriter.Write((byte)CFSH);
            binwriter.Write(USER_CONTROLLED);
            binwriter.Write((byte)CHAR);                            //char
            binwriter.Write((byte)HEIGHT);                          //chgt
            binwriter.Write((byte)HEADHAIR_ID);                     //chid
            binwriter.Write((byte)CHSD);                            //chsd
            binwriter.Write((byte)CHTY);                            //chty
            binwriter.Write((byte)LAST_TEAM);                       //clct
            binwriter.Write(NAME);                                  //clna
            binwriter.Write((UInt16)CLTF);
            binwriter.Write((UInt16)CLTR);
            binwriter.Write((byte)COAP);
            binwriter.Write((byte)COCI);
            binwriter.Write((byte)COCT);
            binwriter.Write(CODA);
            binwriter.Write(CODP);
            binwriter.Write(COEX);
            binwriter.Write(COFA);
            binwriter.Write(COFR);
            binwriter.Write((byte)COPL);
            binwriter.Write((byte)POSITION);
            binwriter.Write(CORP);
            binwriter.Write(CORR);
            binwriter.Write((byte)COACH_OF_THE_YEAR);
            binwriter.Write((UInt16)CPAG);
            binwriter.Write((byte)OFFENSE_PLAYBOOK_ID);
            binwriter.Write((UInt16)FaceID);
            binwriter.Write((byte)CPWS);
            binwriter.Write((byte)APPROVAL_RATING);
            binwriter.Write((UInt16)CRWS);
            binwriter.Write((byte)CRYL);
            binwriter.Write((UInt16)SALARY);
            binwriter.Write((byte)SUPERBOWL_LOSES);
            binwriter.Write((byte)CSBS);
            binwriter.Write((byte)SUPERBOWL_WINS);
            binwriter.Write((byte)SKIN_COLOR);
            binwriter.Write((byte)CSLM);
            binwriter.Write((byte)SEASON_LOSSES);
            binwriter.Write((byte)CSLS);
            binwriter.Write((UInt16)CSPA);
            binwriter.Write(CSPC);
            binwriter.Write((UInt16)CSPF);
            binwriter.Write((byte)SEASON_TIES);
            binwriter.Write((byte)SEASON_WINS);
            binwriter.Write((byte)SEASON_BIGGEST_WIN_MARGIN);
            binwriter.Write((byte)SEASON_WINNING_STREAK);
            binwriter.Write((UInt16)PORTRAIT);
            binwriter.Write(COACH_GLASSES);
            binwriter.Write((byte)CTHG);
            binwriter.Write(CWPL);
            binwriter.Write((byte)CWST);
            binwriter.Write((byte)CWWS);
            binwriter.Write((UInt16)TEAM_ID);
        }
        #endregion

        
    }

}
