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

namespace MaddenEditor.Core.Record
{
	/// <summary>
	/// Enumeration describing the coaching positions in this game
	/// </summary>
	public enum MaddenCoachPosition
	{
		HeadCoach = 0,
		OffensiveCoordinator,
		DefensiveCoordinator,
		SpecialTeams
	}

	public class CoachRecord : TableRecordModel
	{
        // COCH
        
        public const string AGE = "CAGE";
        public const string BIGGEST_LOSS_MARGIN = "CBLM";
        public const string BODY_SIZE = "CBSZ";
        public const string BIGGEST_WIN_MARGIN = "CBWM";
        public const string CHEMISTRY = "CCHM";
        public const string COACH_ID = "CCID";
        public const string CONTRACT_LENGTH = "CCLN";
        public const string CAREER_LOSSES = "CCLO";
        public const string CAREER_LONGEST_LOSING_STREAK = "CCLS";
        public const string PLAYOFF_LOSSES = "CCPL";
        public const string PLAYOFFS_MADE = "CCPM";
        public const string CCPR = "CCPR";
        public const string PLAYFF_WINS = "CCPW";
        public const string CCTC = "CCTC";
        public const string CAREER_TIES = "CCTI";
        public const string CAREER_WINS = "CCWI";
        public const string WINNING_SEASONS = "CCWS";
        public const string DEFENSE = "CDEF";
        public const string DEFENSIVE_PLAYBOOK = "CDID";
        public const string DEF_AGGR = "CDTA";
        public const string DEF_STRAT = "CDTR";
        public const string DEFENSE_TYPE = "CDTY";
        public const string CDWS = "CDWS";
        public const string ETHICS = "CETH";
        public const string CFCO = "CFCO";
        public const string DRAFT_PLAYER = "CFDA";
        public const string SIGN_DRAFT_PICKS = "CFDP";
        public const string CFEX = "CFEX";
        public const string SIGN_FREE_AGENTS = "CFFA";
        public const string FILL_ROSTERS = "CFFR";
        public const string CFHL = "CFHL";
        public const string RESIGN_PLAYERS = "CFRP";
        public const string MANAGE_DEPTH = "CFRR";
        public const string CFSH = "CFSH";
        public const string USER_CONTROLLED = "CFUC";
        public const string CHAR = "CHAR";
        public const string HEIGHT = "CHGT";
        public const string HEADHAIR_ID = "CHID";
        public const string CHSD = "CHSD";
        public const string CHTY = "CHTY";
        public const string KNOWLEDGE = "CKNW";
        public const string LAST_TEAM = "CLCT";
        public const string NAME = "CLNA";
        public const string CLTF = "CLTF";
        public const string CLTR = "CLTR";
        public const string MOTIVATION = "CMOT";
        public const string COAP = "COAP";
        public const string COCI = "COCI";
        public const string COCT = "COCT";
        public const string CODA = "CODA";
        public const string CODP = "CODP";
        public const string COEX = "COEX";
        public const string COFA = "COFA";
        public const string OFFENSE = "COFF";
        public const string COFR = "COFR";
        public const string COPL = "COPL";
        public const string POSITION = "COPS";
        public const string CORP = "CORP";
        public const string CORR = "CORR";
        public const string OFF_AGGR = "COTA";
        public const string OFF_STRAT = "COTR";
        public const string COTY = "COTY";
        public const string CPAG = "CPAG";
        public const string OFFENSE_PLAYBOOK_ID = "CPID";
        public const string FACE_ID = "CPSF";
        public const string CPWS = "CPWS";
        public const string APPROVAL_RATING = "CRAT";
        public const string RB_CARRY_DIST = "CRBT";
        public const string DB_RATING = "CRDB";
        public const string DL_RATING = "CRDL";
        public const string KICK_RATING = "CRKS";
        public const string LB_RATING = "CRLB";
        public const string OL_RATING = "CROL";
        public const string PUNT_RATING = "CRPS";
        public const string QB_RATING = "CRQB";
        public const string RB_RATING = "CRRB";
        public const string S_RATING = "CRSA";
        public const string WR_RATING = "CRWR";
        public const string CRWS = "CRWS";
        public const string CONTRACT_YEARS_LEFT = "CRYL";               //  Don't think this is correct
        public const string SALARY = "CSAL";
        public const string SUPERBOWL_LOSES = "CSBL";
        public const string CSBS = "CSBS";
        public const string SUPERBOWL_WINS = "CSBW";
        public const string SKIN_COLOR = "CSKI";
        public const string CSLM = "CSLM";
        public const string SEASON_LOSSES = "CSLO";
        public const string CSLS = "CSLS";
        public const string CSPA = "CSPA";
        public const string CSPC = "CSPC";
        public const string CSPF = "CSPF";
        public const string SEASON_TIES = "CSTI";
        public const string SEASON_WINS = "CSWI";
        public const string SEASON_BIGGEST_WIN_MARGIN = "CSWM";
        public const string SEASON_WINNING_STREAK = "CSWS";
        public const string COACHPIC = "CSXP";
        public const string COACH_GLASSES = "CTgw";
        public const string CTHG = "CThg";
        public const string CWPL = "CWPL";
        public const string CWST = "CWST";
        public const string CWWS = "CWWS";
        public const string TEAM_ID = "TGID";

		
		public CoachRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        #region Get / Set

        public int Age
        {
            get
            {
                return GetIntField(AGE);
            }
            set
            {
                SetField(AGE, value);
            }
        }
        public int BiggestLossMargin
        {
            get { return GetIntField(BIGGEST_LOSS_MARGIN); }
            set { SetField(BIGGEST_LOSS_MARGIN, value); }
        }        
        public int BodySize
        {
            get { return GetIntField(BODY_SIZE); }
            set { SetField(BODY_SIZE, value); }
        }
        public int BiggestWinMargin
        {
            get { return GetIntField(BIGGEST_WIN_MARGIN); }
            set { SetField(BIGGEST_WIN_MARGIN, value); }
        }
        public int Chemistry
        {
            get
            {
                return GetIntField(CHEMISTRY);
            }
            set
            {
                SetField(CHEMISTRY, value);
            }
        }
        public int CoachId
        {
            get
            {
                return GetIntField(COACH_ID);
            }
            set
            {
                SetField(COACH_ID, value);
            }
        }
        public int ContractLength
        {
            get
            {
                return GetIntField(CONTRACT_LENGTH);
            }
            set
            {
                SetField(CONTRACT_LENGTH, value);
            }
        }
        public int CareerLosses
        {
            get
            {
                return GetIntField(CAREER_LOSSES);
            }
            set
            {
                SetField(CAREER_LOSSES, value);
            }
        }
        public int CareerLosingStreak
        {
            get { return GetIntField(CAREER_LONGEST_LOSING_STREAK); }
            set { SetField(CAREER_LONGEST_LOSING_STREAK, value); }
        }
        public int PlayoffLosses
        {
            get
            {
                return GetIntField(PLAYOFF_LOSSES);
            }
            set
            {
                SetField(PLAYOFF_LOSSES, value);
            }
        }
        public int PlayoffsMade
        {
            get { return GetIntField(PLAYOFFS_MADE); }
            set { SetField(PLAYOFFS_MADE, value); }
        }
        public int Ccpr
        {
            get { return GetIntField(CCPR); }
            set { SetField(CCPR, value); }
        }
        public int Cctc
        {
            get { return GetIntField(CCTC); }
            set { SetField(CCTC, value); }
        }
        public int Cdws
        {
            get { return GetIntField(CDWS); }
            set { SetField(CDWS, value); }
        }
        public int DefenseRating
        {
            get { return GetIntField(DEFENSE); }
            set { SetField(DEFENSE, value); }
        }
        public int cfco
        {
            get { return GetIntField(CFCO); }
            set { SetField(CFCO, value); }             
        }
        public bool DraftPlayer
        {
            get { return GetIntField(DRAFT_PLAYER) ==1; }
            set { SetField(DRAFT_PLAYER, Convert.ToInt32(value)); }
        }
        public bool SignDraftPicks
        {
            get { return GetIntField(SIGN_DRAFT_PICKS) == 1; }
            set { SetField(SIGN_DRAFT_PICKS, Convert.ToInt32(value)); }
        }
        public int Cfex
        {
            get { return GetIntField(CFEX); }
            set { SetField(CFEX, value); }
        }
        public bool SignFreeAgents
        {
            get { return GetIntField(SIGN_FREE_AGENTS) == 1; }
            set { SetField(SIGN_FREE_AGENTS, Convert.ToInt32(value)); }
        }
        public bool FillRosters
        {
            get { return GetIntField(FILL_ROSTERS) == 1; }
            set { SetField(FILL_ROSTERS, Convert.ToInt32(value)); }
        }
        public int cfhl
        {
            get { return GetIntField(CFHL); }
            set { SetField(CFHL, value); }
        }
        public bool ResignPlayers
        {
            get { return GetIntField(RESIGN_PLAYERS) == 1; }
            set { SetField(RESIGN_PLAYERS, Convert.ToInt32(value)); }
        }
        public bool ManageDepth
        {
            get { return GetIntField(MANAGE_DEPTH) == 1; }
            set { SetField(MANAGE_DEPTH, Convert.ToInt32(value)); }
        }
        public int Cfsh
        {
            get { return GetIntField(CFSH); }
            set { SetField(CFSH, value); }
        }
        public int Char
        {
            get { return GetIntField(CHAR); }
            set { SetField(CHAR, value); }
        }
        public int height
        {
            get { return GetIntField(HEIGHT); }
            set { SetField(HEIGHT, value); }
        }
        public int HeadHair
        {
            get { return GetIntField(HEADHAIR_ID); }
            set { SetField(HEADHAIR_ID, value); }
        }
        public int Chsd
        {
            get { return GetIntField(CHSD); }
            set { SetField(CHSD, value); }
        }
        public int Chty
        {
            get { return GetIntField(CHTY); }
            set { SetField(CHTY, value); }
        }
        public int LastTeam
        {
            get { return GetIntField(LAST_TEAM); }
            set { SetField(LAST_TEAM, value); }
        }
        public int Cltf
        {
            get { return GetIntField(CLTF); }
            set { SetField(CLTF, value); }
        }
        public int Cltr
        {
            get { return GetIntField(CLTR); }
            set { SetField(CLTR, value); }
        }
        public int Coap
        {
            get { return GetIntField(COAP); }
            set { SetField(COAP, value); }
        }
        public int Coci
        {
            get { return GetIntField(COCI); }
            set { SetField(COCI, value); }
        }
        public int Coct
        {
            get { return GetIntField(COCT); }
            set { SetField(COCT, value); }
        }
        public int Coda
        {
            get { return GetIntField(CODA); }
            set { SetField(CODA, value); }
        }
        public int Codp
        {
            get { return GetIntField(CODP); }
            set { SetField(CODP, value); }
        }
        public int Coex
        {
            get { return GetIntField(COEX); }
            set { SetField(COEX, value); }
        }
        public int Cofa
        {
            get { return GetIntField(COFA); }
            set { SetField(COFA, value); }
        }
        public int Offense
        {
            get { return GetIntField(OFFENSE); }
            set { SetField(OFFENSE, value); }
        }
        public int Cofr
        {
            get { return GetIntField(COFR); }
            set { SetField(COFR, value); }
        }
        public int Copl
        {
            get { return GetIntField(COPL); }
            set { SetField(COPL, value); }
        }
        public int Corp
        {
            get { return GetIntField(CORP); }
            set { SetField(CORP, value); }
        }
        public int Corr
        {
            get { return GetIntField(CORR); }
            set { SetField(CORR, value); }
        }
        public int Coty
        {
            get { return GetIntField(COTY); }
            set { SetField(COTY, value); }
        }
        public int Cpag
        {
            get { return GetIntField(CPAG); }
            set { SetField(CPAG, value); }
        }
        public int FaceId
        {
            get { return GetIntField(FACE_ID); }
            set { SetField(FACE_ID, value); }
        }
        public int Cpws
        {
            get { return GetIntField(CPWS); }
            set { SetField(CPWS, value); }
        }
        public int ApprovalRating
        {
            get { return GetIntField(APPROVAL_RATING); }
            set { SetField(APPROVAL_RATING, value); }
        }
        public int Crsa
        {
            get { return GetIntField(S_RATING); }
            set { SetField(S_RATING, value); }
        }
        public int Crws
        {
            get { return GetIntField(CRWS); }
            set { SetField(CRWS, value); }
        }
        public int ContractYearsLeft
        {
            get { return GetIntField(CONTRACT_YEARS_LEFT); }
            set { SetField(CONTRACT_YEARS_LEFT, value); }
        }
        public int Csbs
        {
            get { return GetIntField(CSBS); }
            set { SetField(CSBS, value); }
        }
        public int Cslm
        {
            get { return GetIntField(CSLM); }
            set { SetField(CSLM, value); }
        }
        public int SeasonLosses
        {
            get { return GetIntField(SEASON_LOSSES); }
            set { SetField(SEASON_LOSSES, value); }
        }
        public int Csls
        {
            get { return GetIntField(CSLS); }
            set { SetField(CSLS, value); }
        }
        public int Cspa
        {
            get { return GetIntField(CSPA); }
            set { SetField(CSPA, value); }
        }
        public int Cspc
        {
            get { return GetIntField(CSPC); }
            set { SetField(CSPC, value); }
        }
        public int Cspf
        {
            get { return GetIntField(CSPF); }
            set { SetField(CSPF, value); }
        }
        public int SeasonTies
        {
            get { return GetIntField(SEASON_TIES); }
            set { SetField(SEASON_TIES, value); }
        }
        public int SeasonWins
        {
            get { return GetIntField(SEASON_WINS); }
            set { SetField(SEASON_WINS, value); }
        }
        public int SeasonBigWin
        {
            get { return GetIntField(SEASON_BIGGEST_WIN_MARGIN); }
            set { SetField(SEASON_BIGGEST_WIN_MARGIN, value); }
        }
        public int SeasonWinStreak
        {
            get { return GetIntField(SEASON_WINNING_STREAK); }
            set { SetField(SEASON_WINNING_STREAK, value); }
        }
        public bool CoachGlasses
        {
            get { return GetIntField(COACH_GLASSES)==1; }
            set { SetField(COACH_GLASSES, Convert.ToInt32(value)); }
        }
        public int Cthg
        {
            get { return GetIntField(CTHG); }
            set { SetField(CTHG, value); }
        }
        public int Cwpl
        {
            get { return GetIntField(CWPL); }
            set { SetField(CWPL, value); }
        }
        public int Cwst
        {
            get { return GetIntField(CWST); }
            set { SetField(CWST, value); }
        }
        public int Cwws
        {
            get { return GetIntField(CWWS); }
            set { SetField(CWWS, value); }
        }

        
        public string Name
		{
			get
			{
				return GetStringField(NAME);
			}
			set
			{
				SetField(NAME, value);
			}
		}
		
		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}
		public int SkinColor
		{
			//The skin color is 0-4 but it is reversed
			get
			{
				return GetIntField(SKIN_COLOR);
			}
			set
			{
				SetField(SKIN_COLOR, value);
			}
		}
        public int Coachpic
        {
            get
            {
                return GetIntField(COACHPIC);
            }
            set
            {
                SetField(COACHPIC, value);
            }
        }
		public int Position
		{
			get
			{
				return GetIntField(POSITION);
			}
			set
			{
				SetField(POSITION, value);
			}
		}
		public int Salary
		{
			get
			{
				return GetIntField(SALARY);
			}
			set
			{
				SetField(SALARY, value);
			}
		}
       
        public int CoachQB
        {
            get
            {
                return GetIntField(QB_RATING);
            }
            set
            {
                SetField(QB_RATING, value);
            }
        }
        public int CoachRB
        {
            get
            {
                return GetIntField(RB_RATING);
            }
            set
            {
                SetField(RB_RATING, value);
            }
        }
        public int CoachWR
        {
            get
            {
                return GetIntField(WR_RATING);
            }
            set
            {
                SetField(WR_RATING, value);
            }
        }
        public int CoachOL
        {
            get
            {
                return GetIntField(OL_RATING);
            }
            set
            {
                SetField(OL_RATING, value);
            }
        }
        public int CoachDL
        {
            get
            {
                return GetIntField(DL_RATING);
            }
            set
            {
                SetField(DL_RATING, value);
            }
        }
        public int CoachLB
        {
            get
            {
                return GetIntField(LB_RATING);
            }
            set
            {
                SetField(LB_RATING, value);
            }
        }
        public int CoachDB
        {
            get
            {
                return GetIntField(DB_RATING);
            }
            set
            {
                SetField(DB_RATING, value);
            }
        }
        public int CoachKS
        {
            get
            {
                return GetIntField(KICK_RATING);
            }
            set
            {
                SetField(KICK_RATING, value);
            }
        }
        
        public int SuperBowlWins
		{
			get
			{
				return GetIntField(SUPERBOWL_WINS);
			}
			set
			{
				SetField(SUPERBOWL_WINS, value);
			}
		}
		public int SuperBowlLoses
		{
			get
			{
				return GetIntField(SUPERBOWL_LOSES);
			}
			set
			{
				SetField(SUPERBOWL_LOSES, value);
			}
		}
		
		public int PlayoffWins
		{
			get
			{
				return GetIntField(PLAYFF_WINS);
			}
			set
			{
				SetField(PLAYFF_WINS, value);
			}
		}
		public int WinningSeasons
		{
			get
			{
				return GetIntField(WINNING_SEASONS);
			}
			set
			{
				SetField(WINNING_SEASONS, value);
			}
		}
		public int CareerWins
		{
			get
			{
				return GetIntField(CAREER_WINS);
			}
			set
			{
				SetField(CAREER_WINS, value);
			}
		}
		
		public int CareerTies
		{
			get
			{
				return GetIntField(CAREER_TIES);
			}
			set
			{
				SetField(CAREER_TIES, value);
			}
		}
		public bool DefenseType
		{
			get
			{
				return (GetIntField(DEFENSE_TYPE) >= 50 ? true : false);
			}
			set
			{
				if (value)
				{
					SetField(DEFENSE_TYPE, 95);
				}
				else
				{
					SetField(DEFENSE_TYPE, 5);
				}
			}
		}
		public int DefensivePlaybook
		{
			get
			{
				return GetIntField(DEFENSIVE_PLAYBOOK);
			}
			set
			{
				SetField(DEFENSIVE_PLAYBOOK, value);
			}
		}		
		public int OffensiveStrategy
		{
			get
			{
				return GetIntField(OFF_STRAT);
			}
			set
			{
				SetField(OFF_STRAT, value);
			}
		}
		public int DefensiveStrategy
		{
			get
			{
				return GetIntField(DEF_STRAT);
			}
			set
			{
				SetField(DEF_STRAT, value);
			}
		}
		public int DefensiveBackRating
		{
			get
			{
				return GetIntField(DB_RATING);
			}
			set
			{
				SetField(DB_RATING, value);
			}
		}
		public int LinebackerRating
		{
			get
			{
				return GetIntField(LB_RATING);
			}
			set
			{
				SetField(LB_RATING, value);
			}
		}
		public int QuarterbackRating
		{
			get
			{
				return GetIntField(QB_RATING);
			}
			set
			{
				SetField(QB_RATING, value);
			}
		}
		public int RunningbackRating
		{
			get
			{
				return GetIntField(RB_RATING);
			}
			set
			{
				SetField(RB_RATING, value);
			}
		}
		public int OffensiveLineRating
		{
			get
			{
				return GetIntField(OL_RATING);
			}
			set
			{
				SetField(OL_RATING, value);
			}
		}
		public int DefensiveLineRating
		{
			get
			{
				return GetIntField(DL_RATING);
			}
			set
			{
				SetField(DL_RATING, value);
			}
		}
		public int WideReceiverRating
		{
			get
			{
				return GetIntField(WR_RATING);
			}
			set
			{
				SetField(WR_RATING, value);
			}
		}
		public int KickerRating
		{
			get
			{
				return GetIntField(KICK_RATING);
			}
			set
			{
				SetField(KICK_RATING, value);
			}
		}
		public int PuntRating
		{
			get
			{
				return GetIntField(PUNT_RATING);
			}
			set
			{
				SetField(PUNT_RATING, value);
			}
		}
        public int Ethics
		{
			get
			{
				return GetIntField(ETHICS);
			}
			set
			{
				SetField(ETHICS, value);
			}
		}
		public int Knowledge
		{
			get
			{
				return GetIntField(KNOWLEDGE);
			}
			set
			{
				SetField(KNOWLEDGE, value);
			}
		}
		public int Motivation
		{
			get
			{
				return GetIntField(MOTIVATION);
			}
			set
			{
				SetField(MOTIVATION, value);
			}
		}
		
		public int RBCarryDist
		{
			get
			{
				return GetIntField(RB_CARRY_DIST);
			}
			set
			{
				SetField(RB_CARRY_DIST, value);
			}
		}
		public int OffensiveAggression
		{
			get
			{
				return GetIntField(OFF_AGGR);
			}
			set
			{
				SetField(OFF_AGGR, value);
			}
		}
		public int DefensiveAggression
		{
			get
			{
				return GetIntField(DEF_AGGR);
			}
			set
			{
				SetField(DEF_AGGR, value);
			}
		}
		public bool HumanControlled
		{
			get
			{
				return (GetIntField(USER_CONTROLLED) == 1);
			}
			set
			{
				SetField(USER_CONTROLLED, Convert.ToInt32(value));
			}
        }







        #endregion
    }
}