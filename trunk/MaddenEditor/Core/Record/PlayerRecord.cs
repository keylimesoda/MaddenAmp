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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Core.Record
{
    #region Enums
    public enum Rating
    {
        STR = 0,
        AGI = 1,
        SPD = 2,
        ACC = 3,
        AWR = 4,
        CTH = 5,
        CAR = 6,
        THP = 7,
        THA = 8,
        KPW = 9,
        KAC = 10,
        BTK = 11,
        TAK = 12,
        PBK = 13,
        RBK = 14,
        JMP = 15,
        KRT = 16,
        IMP = 17,   // IMportance, not used in OVR
        INJ = 18,
        BKS = 19,   // break sack
        PWA = 20,   // play action
        THD = 21,   // throw deep
        THM = 22,   // throw medium
        TOR = 23,   // Throw on run
        THS = 24,   // throw short
        TUP = 25,   // throw under pressure
        TRU = 26,
        ELU = 27,
        JUK = 28,
        SPN = 29,
        SFA = 30,
        VIS = 31,
        SRR = 32,
        BKT = 33,
        CIT = 34,
        SPC = 35,
        IBK = 36,
        LBK = 37,
        RBS = 38,
        RBF = 39,
        PBF = 40,
        PBS = 41,
        DRR = 42, // deep route
        MRR = 43, // medium route
        REL = 44, // release
        SHD = 45, // shed block
        HIT = 46, // hit power
        FNM = 47, // finesse moves
        PWM = 48, // power moves
        PLR = 49, // play recognition
        PUR = 50, // pursuit
        MAN = 51, // man cover
        ZON = 52, // zone cover
        KRR = 53, // kick return
    }
    #endregion

    public class PlayerRecord : TableRecordModel
	{   
        // table name "PLAY"
        #region Record members
        public const string ARM_DEFN = "BSAA";                  //2019
        public const string ARM_SIZE = "BSAT";                  //2019
        public const string BUTT_DEFN = "BSBA";                 //2019
        public const string BUTT_SIZE = "BSBT";                 //2019
        public const string CALF_DEFN = "BSFA";                 //2019
        public const string CALF_SIZE = "BSFT";                 //2019
        public const string FOOT_DEFN = "BSFA";                 //2019
        public const string FOOT_SIZE = "BSFT";                 //2019
        public const string GUT_DEFN = "BSGA";                  //2019
        public const string GUT_SIZE = "BSGT";                  //2019
        public const string PAD_DEFN = "BSPA";                  //2019
        public const string PAD_SIZE = "BSPT";                  //2019
        public const string SHOULDER_DEFN = "BSSA";             //2019
        public const string SHOULDER_SIZE = "BSST";             //2019
        public const string THIGH_DEFN = "BSTA";                //2019
        public const string THIGH_SIZE = "BSTT";                //2019
        public const string WAIST_DEFN = "BSWA";                //2019
        public const string WAIST_SIZE = "BSWT";                //2019        
        public const string ENDPLAY = "EPAV";                   //2019
        public const string IS_CAPTAIN = "ISCN";                //2019
        public const string ACCELERATION = "PACC";
        public const string AGE = "PAGE";
        public const string AGILITY = "PAGI";
        public const string AWARENESS = "PAWR";
        public const string BALL_CARRIER_VISION = "PBCV";       //2019
        public const string BREAK_TACKLE_19 = "PBKT";           //2019
        public const string PBOT = "PBOT";                      //2019
        public const string NASAL_STRIP = "PBRE";
        public const string BLOCK_SHEDDING = "PBSG";            //2019
        public const string BREAK_SACK = "PBSK";                //2019
        public const string BREAK_TACKLE = "PBTK";
        public const string CARRYING = "PCAR";
        public const string PCEL = "PCEL";                      // 2004-2006 celebration?  replaced by ego 2007-2008
        public const string EQP_PAD_SHELF = "PCHS";
        public const string PLAYER_COMMENT = "PCMT";
        public const string COLLEGE_ID = "PCOL";
        public const string CONTRACT_LENGTH = "PCON";
        public const string CAREER_PHASE = "PCPH";
        public const string SALARY_CURRENT = "PCSA";            // franchise only
        public const string CATCHING = "PCTH";
        public const string PCTS = "PCTS";                      // 2007-2008 ??
        public const string CONTRACT_YRS_LEFT = "PCYL";
        public const string DRAFT_ROUND_INDEX = "PDPI";
        public const string DRAFT_ROUND = "PDRO";
        public const string DEEP_ROUTE = "PDRR";                //2019
        public const string PLAYER_EGO = "PEGO";                // 2007-2008
        public const string ELUSIVE = "PELU";                   // 2019
        public const string PEPS = "PEPS";                      // 2019
        public const string EYE_PAINT = "PEYE";
        public const string ARMS_FAT = "PFAS";        
        public const string LEGS_CALF_FAT = "PFCS";
        public const string FACE_ID = "PFEx";
        public const string FACE_SHAPE = "PFGE";                // 2004 field, 2005
        public const string PFGS = "PFGS";                      // ?
        public const string HOLDOUT = "PFHO";                   // ?
        public const string LEGS_THIGH_FAT = "PFHS";
        public const string FLAK_JACKET = "PFLA";               //2019
        public const string FACE_MASK = "PFMK";
        public const string FINESSE_MOVES = "PFMS";             //2019
        public const string FIRST_NAME = "PFNA";
        public const string PRO_BOWL = "PFPB";
        public const string BODY_FAT = "PFTS";
        public const string FACE_ID_19 = "PGHE";                //2019
        public const string PLAYER_ID = "PGID";
        public const string SLEEVES_LEFT = "PGSL";
        public const string DOMINANT_HAND = "PHAN";
        public const string HAIR_COLOR = "PHCL";
        public const string HAIR_STYLE = "PHED";
        public const string HEIGHT = "PHGT";
        public const string HELMET_STYLE = "PHLM";
        public const string HOMESTATE = "PHSN";                 //2019
        public const string HOMETOWN = "PHTN";                  //2019
        public const string NFL_ICON = "PICN";                  // 2005
        public const string IMPORTANCE = "PIMP";
        public const string INJURY = "PINJ";
        public const string JERSEY_NUMBER = "PJEN";
        public const string JERSEYSLEEVE = "PJER";
        public const string JUMPING = "PJMP";
        public const string PLAYER_JERSEY_INITIALS = "PJTY";
        public const string KICK_ACCURACY = "PKAC";
        public const string KICK_POWER = "PKPR";
        public const string KICK_RETURN = "PKRT";
        public const string BIRTHDAY = "PLBD";                  //2019
        public const string LEAD_BLOCK = "PLBK";                //2019
        public const string BACK_PLATE = "PLBP";                //2019
        public const string CATCH_TRAFFIC = "PLCI";             //2019
        public const string LEFT_ELBOW = "PLEL";
        public const string PLFH = "PLFH";                      // ?
        public const string LEFT_HAND = "PLHA";
        public const string HIT_POWER = "PLHT";                 //2019
        public const string HAND_WARMER = "PLHW";               //2019
        public const string LAST_HEALTHY_YEAR = "PLHY";         // not sure about this one
        public const string IMPACT_BLOCKING = "PLIB";           // 2019
        public const string JUKE_MOVE = "PLJM";                 //2019
        public const string KNEE_LEFT = "PLKN";                 //2019
        public const string MAN_COVERAGE = "PLMC";              // 2019
        public const string LAST_NAME = "PLNA";
        public const string PRESS_COVER = "PLPE";               //2019
        public const string PLPL = "PLPL";                      // progession related?
        public const string MOVES_POWER = "PLPm";               //2019
        public const string PLAYER_POTENTIAL = "PLPO";          //2019
        public const string PLAY_RECOGNITION = "PLPR";          //2019
        public const string PURSUIT = "PLPU";                   //2019
        public const string RELEASE = "PLRL";                   //2019        
        public const string TRUCKING = "PLTR";                  //2019
        public const string STIFF_ARM = "PLSA";                 //2019
        public const string SPEC_CATCH = "PLSC";                //2019
        public const string LEFT_SHOE = "PLSH";
        public const string SPIN_MOVE = "PLSM";                 //2019
        public const string EQP_SHOES = "PLSS";
        public const string LEFT_KNEE = "PLTH";
        public const string PLAYER_TOWEL = "PLTL";              //2019
        public const string PLAYER_TYPE = "PLTY";               //2019
        public const string LEFT_WRIST = "PLWR";
        public const string ZONE_COVERAGE = "PLZC";             // 2019
        public const string ARMS_MUSCLE = "PMAS";
        public const string LEGS_CALF_MUSCLE = "PMCS";
        public const string REAR_FAT = "PMGS";
        public const string LEGS_THIGH_MUSCLE = "PMHS";
        public const string MORALE = "PMOR";
        public const string SLEEVES_RIGHT = "PMOR";             //2019  changed from morale, which doesnt exist anymore
        public const string MOUTHPIECE = "PMPC";
        public const string MEDIUM_ROUTE_RUN = "PMRR";          //2019
        public const string BODY_WEIGHT = "PMTS";
        public const string MUSCLE = "PMUS";
        public const string PNEC = "PNEC";                      // 2019 something to do with generated portraits
        public const string NECK_ROLL = "PNEK";
        public const string NFL_ID = "POID";
        public const string ORIGINAL_POSITION_ID = "POPS";      // 2005+  not in 2019
        public const string OVERALL = "POVR";
        public const string PASSBLOCK_FOOTWORK = "PPBF";        //2019
        public const string PASS_BLOCKING = "PPBK";
        public const string PASSBLOCK_STRENGTH = "PPBS";        //2019
        public const string PLAYED_GAMES = "PPGA";              // progression period games played
        public const string PLAY_ACTION = "PPLA";               //2019
        public const string POSITION_ID = "PPOS";
        public const string PPSP = "PPSP";                      // progression related?
        public const string PREVIOUS_TEAM_ID = "PPTI";
        public const string QB_STYLE = "PQBS";                  //2019
        public const string REAR_SHAPE = "PQGS";
        public const string EQP_FLAK_JACKET = "PQTS";           // 04-06
        public const string RUN_BLOCK_FINESSE = "PRBF";         // 2019
        public const string RUN_BLOCKING = "PRBK";
        public const string RUNBLOCK_STRENGTH = "PRBS";         //2019
        public const string RIGHT_ELBOW = "PREL";
        public const string RIGHT_HAND = "PRHA";
        public const string KNEE_RIGHT = "PRKN";                //2019
        public const string PLAYER_ROLE = "PROL";               // 2007 
        public const string XP_RATE = "PROL";                   // 2019 This isXP Rate
        public const string PLAYER_WEAPON = "PRL2";             // 2008
        public const string REAR_SIZE = "PRSE";                 // 2019
        public const string RIGHT_SHOE = "PRSH";
        public const string RIGHT_KNEE = "PRTH";
        public const string RIGHT_WRIST = "PRWR";
        public const string SALARY_YEAR_0 = "PSA0";             
        public const string SALARY_YEAR_1 = "PSA1";
        public const string SALARY_YEAR_2 = "PSA2";
        public const string SALARY_YEAR_3 = "PSA3";
        public const string SALARY_YEAR_4 = "PSA4";
        public const string SALARY_YEAR_5 = "PSA5";
        public const string SALARY_YEAR_6 = "PSA6";
        public const string SIGNING_BONUS_YEAR_0 = "PSB0";
        public const string SIGNING_BONUS_YEAR_1 = "PSB1";      
        public const string SIGNING_BONUS_YEAR_2 = "PSB2";
        public const string SIGNING_BONUS_YEAR_3 = "PSB3";
        public const string SIGNING_BONUS_YEAR_4 = "PSB4";
        public const string SIGNING_BONUS_YEAR_5 = "PSB5";
        public const string SIGNING_BONUS_YEAR_6 = "PSB6";
        public const string SIGNING_BONUS_TOTAL = "PSBO";
        public const string BODY_OVERALL = "PSBS";
        public const string SIDELINE_HEADGEAR = "PSHG";         //2019
        public const string SOCK_HEIGHT = "PSKH";               //2019
        public const string PSKI = "PSKI";                      // ?
        public const string SPEED = "PSPD";
        public const string ANKLE_LEFT = "PSPL";                //2019
        public const string ANKLE_RIGHT = "PSPR";               //2019
        public const string STAMINA = "PSTA";
        public const string PSTM = "PSTM";                      // sleeves temp?
        public const string STANCE = "PSTN";                    //2019
        public const string STRENGTH = "PSTR";
        public const string THROWING_STYLE = "PSTY";
        public const string PORTRAIT_ID = "PSXP";
        public const string THROW_DEEP = "PTAD";                //2019
        public const string TACKLE = "PTAK";
        public const string LEFT_TATTOO = "PTAL";
        public const string THROW_MEDIUM = "PTAM";              //2019
        public const string RIGHT_TATTOO = "PTAR";
        public const string THROW_SHORT = "PTAS";               //2019
        public const string TENDENCY = "PTEN";
        public const string TOUGHNESS = "PTGH";
        public const string THROW_ACCURACY = "PTHA";
        public const string THROW_POWER = "PTHP";
        public const string THROW_ON_RUN = "PTOR";              //2019
        public const string LEGS_THIGH_PADS = "PTPS";           // 2004-2005
        public const string TOTAL_SALARY = "PTSA";              
        public const string TEMP_SLEEVES = "PTSL";
        public const string EQP_PAD_HEIGHT = "PTSS";
        public const string THROW_PRESSURE = "PTUP";            //2019
        public const string PUCL = "PUCL";                      // ?
        public const string UNDERSHIRT = "PUND";                //2019
        public const string BODY_MUSCLE = "PUTS";
        public const string PLAYER_VALUE = "PVAL";              // 2008, this is 0-7 values
        public const string PREVIOUS_CONTRACT_LENGTH = "PVCO";  // previous contract length?
        public const string VISOR = "PVIS";
        public const string PREVIOUS_SIGNING_BONUS_TOTAL = "PVSB";
        public const string PREVIOUS_TOTAL_SALARY = "PVTS";
        public const string WEIGHT = "PWGT";
        public const string PWIN = "PWIN";
        public const string EQP_PAD_WIDTH = "PWSS";
        public const string CONFIDENCE = "PYCF";                //2019
        public const string YRS_PRO = "PYRP";
        public const string YEARS_WITH_TEAM = "PYWT";
        public const string SHORT_ROUTE_RUN = "SRRN";           //2019
		public const string TEAM_ID = "TGID";
        public const string TEAM_LEFT_ELBOW = "TLEL";           // not in 2019
        public const string TEAM_LEFT_HAND = "TLHA";            // not in 2019
        public const string TEAM_LEFT_WRIST = "TLWR";           // not in 2019
        public const string BIG_HITTER = "TRBH";                //2019
        public const string DL_BULLRUSH = "TRBR";               //2019
        public const string COVERS_BALL = "TRCB";               //2019
        public const string CLUTCH = "TRCL";                    //2019
        public const string POSSESSION_CATCH = "TRCT";          //2019        
        public const string DROP_PASSES = "TRDO";               //2019
        public const string TRDP = "TRDP";                      //2019
        public const string DL_SPINMOVE = "TRDS";               //2019
        public const string TEAM_RIGHT_ELBOW = "TREL";
        public const string KEEP_FEET_IN_BOUNDS = "TRFB";       //2019
        public const string PUMP_FAKE = "TRFK";                 //2019 not sure this is being used
        public const string FORCE_PASSES = "TRFP";              //2019
        public const string FIGHT_FOR_YARDS = "TRFY";           //2019
		public const string TEAM_RIGHT_HAND = "TRHA";
        public const string HIGH_MOTOR = "TRHM";                //2019
        public const string PENALTY = "TRIC";                   //2019
        public const string AGGRESSIVE_CATCH = "TRJR";          //2019 
        public const string PLAYS_BALL = "TRPB";                //2019
        public const string STRIPS_BALL = "TRSB";               //2019
        public const string SIDELINE_CATCH = "TRSC";            //2019
        public const string SENSE_PRESSURE = "TRSP";            //2019
        public const string DL_SWIM = "TRSW";                   //2019
        public const string THROW_AWAY = "TRTA";                //2019
        public const string TACKLE_LOW = "TRTL";                //2019 ?  only a few players have this set in default roster
        public const string TUCK_AND_RUN = "TRTR";              //2019
        public const string THROW_SPIRAL = "TRTS";              //2019
        public const string TEAM_RIGHT_WRIST = "TRWR";          // not in 2019
        public const string RUN_AFTER_CATCH = "TRWU";           //2019 
        public const string PRESSURE_MAX = "TSPM";              //2019

        #endregion

        public PlayerRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}
        
		public override string ToString()
		{
			return FirstName + " " + LastName + " (" + Enum.GetNames(typeof(MaddenPositions))[PositionId].ToString() + ")";
		}

        public int[] YearlySalary = null;
        public int[] YearlyBonus = null;


        #region Get/SET

        public string FirstName
		{
            get { return GetStringField(FIRST_NAME); }
            set	{ SetField(FIRST_NAME, value); }
		}

		public string LastName
		{
			get
			{
				return GetStringField(LAST_NAME);
			}
			set
			{
				SetField(LAST_NAME, value);
			}
		}

		public int PositionId
		{
			get
			{
				return GetIntField(POSITION_ID);
			}
			set
			{
				SetField(POSITION_ID, value);
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

        public int PreviousTeamId
        {
            get { return GetIntField(PREVIOUS_TEAM_ID); }
            set { SetField(PREVIOUS_TEAM_ID, value); }
        }
        
        public int PlayerId
		{
			get
			{
				return GetIntField(PLAYER_ID);
			}
			set
			{
				SetField(PLAYER_ID, value);
			}
		}

        public int NFLID
        {
            get
            {
                return GetIntField(NFL_ID);
            }
            set
            {
                SetField(NFL_ID, value);
            }
        }

		public int CollegeId
		{
			get
			{
				return GetIntField(COLLEGE_ID);
			}
			set
			{
				SetField(COLLEGE_ID, value);
			}
		}

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

		public int YearsPro
		{
			get
			{
				return GetIntField(YRS_PRO);
			}
			set
			{
				SetField(YRS_PRO, value);
			}
		}

		public int PortraitId
		{
			get
			{
				return GetIntField(PORTRAIT_ID);
			}
			set
			{
				SetField(PORTRAIT_ID, value);
			}
		}

		public bool NFLIcon
		{
			get
			{
				return (GetIntField(NFL_ICON) == 1);
			}
			set
			{
				SetField(NFL_ICON, Convert.ToInt32(value));
			}
		}

        public bool ProBowl
        {
            get { return (GetIntField(PRO_BOWL) == 1); }
            set { SetField(PRO_BOWL, Convert.ToInt32(value)); }
        }

		public bool DominantHand
		{
			get
			{
				return (GetIntField(DOMINANT_HAND) == 1);
			}
			set
			{
				SetField(DOMINANT_HAND, Convert.ToInt32(value));
			}
		}

		public int JerseyNumber
		{
			get
			{
				return GetIntField(JERSEY_NUMBER);
			}
			set
			{
				SetField(JERSEY_NUMBER, value);
			}
		}

		public int Overall
		{
			get
			{
				return GetIntField(OVERALL);
			}
			set
			{
				SetField(OVERALL, value);
			}
		}

		public int Speed
		{
			get
			{
				return GetIntField(SPEED);
			}
			set
			{
				SetField(SPEED, value);
			}
		}

		public int Strength
		{
			get
			{
				return GetIntField(STRENGTH);
			}
			set
			{
				SetField(STRENGTH, value);
			}
		}

		public int Awareness
		{
			get
			{
				return GetIntField(AWARENESS);
			}
			set
			{
				SetField(AWARENESS, value);
			}
		}

		public int Agility
		{
			get
			{
				return GetIntField(AGILITY);
			}
			set
			{
				SetField(AGILITY, value);
			}
		}

		public int Acceleration
		{
			get
			{
				return GetIntField(ACCELERATION);
			}
			set
			{
				SetField(ACCELERATION, value);
			}
		}

		public int Catching
		{
			get
			{
				return GetIntField(CATCHING);
			}
			set
			{
				SetField(CATCHING, value);
			}
		}

		public int Carrying
		{
			get
			{
				return GetIntField(CARRYING);
			}
			set
			{
				SetField(CARRYING, value);
			}
		}

		public int Jumping
		{
			get
			{
				return GetIntField(JUMPING);
			}
			set
			{
				SetField(JUMPING, value);
			}
		}

		public int BreakTackle
		{
			get
			{
				return GetIntField(BREAK_TACKLE);
			}
			set
			{
				SetField(BREAK_TACKLE, value);
			}
		}

		public int Tackle
		{
			get
			{
				return GetIntField(TACKLE);
			}
			set
			{
				SetField(TACKLE, value);
			}
		}

		public int ThrowPower
		{
			get
			{
				return GetIntField(THROW_POWER);
			}
			set
			{
				SetField(THROW_POWER, value);
			}
		}

		public int ThrowAccuracy
		{
			get
			{
				return GetIntField(THROW_ACCURACY);
			}
			set
			{
				SetField(THROW_ACCURACY, value);
			}
		}

		public int PassBlocking
		{
			get
			{
				return GetIntField(PASS_BLOCKING);
			}
			set
			{
				SetField(PASS_BLOCKING, value);
			}
		}

		public int RunBlocking
		{
			get
			{
				return GetIntField(RUN_BLOCKING);
			}
			set
			{
				SetField(RUN_BLOCKING, value);
			}
		}

		public int KickPower
		{
			get
			{
				return GetIntField(KICK_POWER);
			}
			set
			{
				SetField(KICK_POWER, value);
			}
		}

		public int KickAccuracy
		{
			get
			{
				return GetIntField(KICK_ACCURACY);
			}
			set
			{
				SetField(KICK_ACCURACY, value);
			}
		}

		public int KickReturn
		{
			get
			{
				return GetIntField(KICK_RETURN);
			}
			set
			{
				SetField(KICK_RETURN, value);
			}
		}

		public int Stamina
		{
			get
			{
				return GetIntField(STAMINA);
			}
			set
			{
				SetField(STAMINA, value);
			}
		}

		public int Injury
		{
			get
			{
				return GetIntField(INJURY);
			}
			set
			{
				SetField(INJURY, value);
			}
		}

		public int Toughness
		{
			get
			{
				return GetIntField(TOUGHNESS);
			}
			set
			{
				SetField(TOUGHNESS, value);
			}
		}

		public int Morale
		{
			get
			{
				return GetIntField(MORALE);
			}
			set
			{
				SetField(MORALE, value);
			}
		}

		public int Importance
		{
			get
			{
				return GetIntField(IMPORTANCE);
			}
			set
			{
				SetField(IMPORTANCE, value);
			}
		}

		public int Weight
		{
			get
			{
				return GetIntField(WEIGHT);
			}
			set
			{
				SetField(WEIGHT, value);
			}
		}

		public int Height
		{
			get
			{
				return GetIntField(HEIGHT);
			}
			set
			{
				SetField(HEIGHT, value);
			}
		}

		public int BodyWeight
		{
			get
			{
				return (GetIntField(BODY_WEIGHT) < 100 ? GetIntField(BODY_WEIGHT) : 99);
			}
			set
			{
				SetField(BODY_WEIGHT, value);
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

		public int ContractYearsLeft
		{
			get
			{
				return GetIntField(CONTRACT_YRS_LEFT);
			}
			set
			{
				SetField(CONTRACT_YRS_LEFT, value);
			}
		}

		public int PreviousSigningBonus
		{
			get
			{
				return GetIntField(PREVIOUS_SIGNING_BONUS_TOTAL);
			}
			set
			{
				SetField(PREVIOUS_SIGNING_BONUS_TOTAL, value);
			}
		}

        public int CurrentSalary
        {
            get { return GetIntField(SALARY_CURRENT); }
            set { SetField(SALARY_CURRENT, value); }        
        }
        	
		public int FaceId
		{
			get
			{
				return GetIntField(FACE_ID);
			}
			set
			{
				SetField(FACE_ID, value);
			}
		}
        		
		public bool ThrowStyle
		{
			get	{ return GetIntField(THROWING_STYLE) == 1; }
            set { SetField(THROWING_STYLE, Convert.ToInt32(value)); }
		}
        
		public int Tendency
		{
			get
			{
				return GetIntField(TENDENCY);
			}
			set
			{
				SetField(TENDENCY, value);
			}
		}

		public int DraftRoundIndex
		{
			get
			{
				return GetIntField(DRAFT_ROUND_INDEX);
			}
			set
			{
				SetField(DRAFT_ROUND_INDEX, value);
			}
		}

		public int DraftRound
		{
			get
			{
				return GetIntField(DRAFT_ROUND);
			}
			set
			{
				SetField(DRAFT_ROUND, value);
			}
		}
        
        public int PlayerComment
        {
            get { return GetIntField(PLAYER_COMMENT); }
            set { SetField(PLAYER_COMMENT, value); }
        }

        public int JerseyInitials
        {
            get { return GetIntField(PLAYER_JERSEY_INITIALS); }
            set { SetField(PLAYER_JERSEY_INITIALS, value); }
        }

        public int TotalSalary
        {
            get { return GetIntField(TOTAL_SALARY); }
            set { SetField(TOTAL_SALARY, value); }
        }

        public int OriginalPositionId
        {
            get { return GetIntField(ORIGINAL_POSITION_ID); }
            set { SetField(ORIGINAL_POSITION_ID, value); }
        }

        public int Pcel
        {
            get { return GetIntField(PCEL); }
            set { SetField(PCEL, value); }
        }

        public int Pfgs

        {
            get { return GetIntField(PFGS); }
            set { SetField(PFGS, value); }
        }

        public bool Holdout
        {
            get { return GetIntField(HOLDOUT) == 1; }
            set { SetField(HOLDOUT, Convert.ToInt32(value)); }
        }

        public int Pcts
        {
            get { return GetIntField(PCTS); }
            set { SetField(PCTS, value); }
        }

        public int JerseySleeve
        {
            get { return GetIntField(JERSEYSLEEVE); }
            set { SetField(JERSEYSLEEVE,value); }
        }

        public bool Plfh
        {
            get { return GetIntField(PLFH)==1; }
            set { SetField(PLFH, Convert.ToInt32(value)); }
        }

        public int LastHealthy
        {
            get { return GetIntField(LAST_HEALTHY_YEAR); }
            set { SetField(LAST_HEALTHY_YEAR, value); }
        }

        public int Plpl
        {
            get { return GetIntField(PLPL); }
            set { SetField(PLPL, value); }
        }

        public int Ppsp
        {
            get { return GetIntField(PPSP); }
            set { SetField(PPSP, value); }
        }

        public int PlayedGames
        {
            get { return GetIntField(PLAYED_GAMES); }
            set { SetField(PLAYED_GAMES, value); }
        }
        
        public int SigningBonus
        {
            get { return GetIntField(SIGNING_BONUS_TOTAL); }
            set { SetField(SIGNING_BONUS_TOTAL, value);
            }
        }

        public int Pski
        {
            get { return GetIntField(PSKI); }
            set { SetField(PSKI, value); }
        }

        public int Pstm
        {
            get { return GetIntField(PSTM); }
            set { SetField(PSTM, value); }
        }
        
        public int Pucl
        {
            get { return GetIntField(PUCL); }
            set { SetField(PUCL, value); }
        }

        public int PreviousContractLength
        {
            get { return GetIntField(PREVIOUS_CONTRACT_LENGTH); }
            set { SetField(PREVIOUS_CONTRACT_LENGTH, value); }
        }

        public int Salary0
        {
            get { return GetIntField(SALARY_YEAR_0); }
            set { SetField(SALARY_YEAR_0, value); }
        }
        public int Salary1
        {
            get { return GetIntField(SALARY_YEAR_1); }
            set { SetField(SALARY_YEAR_1, value); }
        }
        public int Salary2
        {
            get { return GetIntField(SALARY_YEAR_2); }
            set { SetField(SALARY_YEAR_2, value); }
        }
        public int Salary3
        {
            get { return GetIntField(SALARY_YEAR_3); }
            set { SetField(SALARY_YEAR_3, value); }
        }
        public int Salary4
        {
            get { return GetIntField(SALARY_YEAR_4); }
            set { SetField(SALARY_YEAR_4, value); }
        }
        public int Salary5
        {
            get { return GetIntField(SALARY_YEAR_5); }
            set { SetField(SALARY_YEAR_5, value); }                
        }
        public int Salary6
        {
            get { return GetIntField(SALARY_YEAR_6); }
            set { SetField(SALARY_YEAR_6, value); }
        }
        public int Bonus0
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_0); }
            set { SetField(SIGNING_BONUS_YEAR_0, value); }
        }
        public int Bonus1
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_1); }
            set { SetField(SIGNING_BONUS_YEAR_1, value); }
        }
        public int Bonus2
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_2); }
            set { SetField(SIGNING_BONUS_YEAR_2, value); }
        }
        public int Bonus3
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_3); }
            set { SetField(SIGNING_BONUS_YEAR_3, value); }
        }
        public int Bonus4
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_4); }
            set { SetField(SIGNING_BONUS_YEAR_4, value); }
        }
        public int Bonus5
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_5); }
            set { SetField(SIGNING_BONUS_YEAR_5, value); }
        }
        public int Bonus6
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_6); }
            set { SetField(SIGNING_BONUS_YEAR_6, value); }
        }

        public int TotalBonus
        {
            get { return GetIntField(SIGNING_BONUS_TOTAL); }
            set { SetField(SIGNING_BONUS_TOTAL, value); }
        }
        public int PreviousTotalSalary
        {
            get { return GetIntField(PREVIOUS_TOTAL_SALARY); }
            set { SetField(PREVIOUS_TOTAL_SALARY, value); }
        }


        #region Appearance / Equipment
        public bool RightKnee
        {
            get { return GetIntField(RIGHT_KNEE) == 1; }
            set { SetField(RIGHT_KNEE, Convert.ToInt32(value)); }
        }

        public int TempSleeves
        {
            get { return GetIntField(TEMP_SLEEVES); }
            set { SetField(TEMP_SLEEVES, value); }
        }

        public int LeftElbow
        {
            get
            {
                return GetIntField(LEFT_ELBOW);
            }
            set
            {
                SetField(LEFT_ELBOW, value);
            }
        }

        public int RightElbow
        {
            get
            {
                return GetIntField(RIGHT_ELBOW);
            }
            set
            {
                SetField(RIGHT_ELBOW, value);
            }
        }

        public int TeamLeftElbow
        {
            get { return GetIntField(TEAM_LEFT_ELBOW); }
            set { SetField(TEAM_LEFT_ELBOW, value); }
        }
        public int TeamRightElbow
        {
            get { return GetIntField(TEAM_RIGHT_ELBOW); }
            set { SetField(TEAM_RIGHT_ELBOW, value); }
        }


        public int SleevesLeft
        {
            get
            {
                return GetIntField(SLEEVES_LEFT);
            }
            set
            {
                SetField(SLEEVES_LEFT, value);
            }
        }

        public int LeftWrist
        {
            get
            {
                return GetIntField(LEFT_WRIST);
            }
            set
            {
                SetField(LEFT_WRIST, value);                
            }
        }
        public int TeamLeftWrist
        {
            get
            {
                return GetIntField(TEAM_LEFT_WRIST);
            }
            set
            {
                SetField(TEAM_LEFT_WRIST, value);
            }
        }
        public int RightWrist
        {
            get
            {
                return GetIntField(RIGHT_WRIST);
            }
            set
            {
                SetField(RIGHT_WRIST, value);
            }
        }
        public int TeamRightWrist
        {
            get
            {
                return GetIntField(TEAM_RIGHT_WRIST);
            }
            set
            {
                SetField(TEAM_RIGHT_WRIST, value);
            }
        }
        public int NasalStrip
        {
            get
            {
                return GetIntField(NASAL_STRIP);
            }
            set
            {
                SetField(NASAL_STRIP, value);
            }
        }

        public int LeftTattoo
        {
            get
            {
                return GetIntField(LEFT_TATTOO);
            }
            set
            {
                SetField(LEFT_TATTOO, value);
            }
        }

        public int RightTattoo
        {
            get
            {
                return GetIntField(RIGHT_TATTOO);
            }
            set
            {
                SetField(RIGHT_TATTOO, value);
            }
        }

        public int BodyOverall
        {
            get
            {
                return 99 - (GetIntField(BODY_OVERALL) > 99 ? 99 : GetIntField(BODY_OVERALL));
            }
            set
            {
                SetField(BODY_OVERALL, 99 - value);
            }
        }

        public int LegsThighPads
        {
            get
            {
                return 99 - (GetIntField(LEGS_THIGH_PADS) > 99 ? 99 : GetIntField(LEGS_THIGH_PADS));
            }
            set
            {
                SetField(LEGS_THIGH_PADS, 99 - value);
            }
        }

        public int HairColor
        {
            get
            {
                return GetIntField(HAIR_COLOR);
            }
            set
            {
                SetField(HAIR_COLOR, value);
            }
        }

        public int HairStyle
        {
            get
            {
                return GetIntField(HAIR_STYLE);
            }
            set
            {
                SetField(HAIR_STYLE, value);
            }
        }

        public int Helmet
        {
            get
            {
                return GetIntField(HELMET_STYLE);
            }
            set
            {
                SetField(HELMET_STYLE, value);
            }
        }

        public int FaceMask
        {
            get
            {
                return GetIntField(FACE_MASK);
            }
            set
            {
                SetField(FACE_MASK, value);
            }
        }

        public int NeckRoll
        {
            get
            {
                return (GetIntField(NECK_ROLL) < 3 ? GetIntField(NECK_ROLL) : 2);
            }
            set
            {
                SetField(NECK_ROLL, value);
            }
        }

        public int Visor
        {
            get
            {
                return GetIntField(VISOR);
            }
            set
            {
                SetField(VISOR, value);
            }
        }

        public int MouthPiece
        {
            get
            {
                return GetIntField(MOUTHPIECE);
            }
            set
            {
                SetField(MOUTHPIECE, value);
            }
        }

        public int LeftHand
        {
            get
            {
                return GetIntField(LEFT_HAND);
            }
            set
            {
                SetField(LEFT_HAND, value);
            }
        }
        public int TeamLeftHand
        {
            get { return GetIntField(TEAM_LEFT_HAND); }
            set { SetField(TEAM_LEFT_HAND, value); }
        }

        public int RightHand
        {
            get
            {
                return GetIntField(RIGHT_HAND);
            }
            set
            {
                SetField(RIGHT_HAND, value);
            }
        }
        public int TeamRightHand
        {
            get { return GetIntField(TEAM_RIGHT_HAND); }
            set { SetField(TEAM_RIGHT_HAND, value); }
        }

        public int LeftShoe
        {
            get
            {
                return GetIntField(LEFT_SHOE);
            }
            set
            {
                SetField(LEFT_SHOE, value);
            }
        }

        public int RightShoe
        {
            get
            {
                return GetIntField(RIGHT_SHOE);
            }
            set
            {
                SetField(RIGHT_SHOE, value);
            }
        }

        public bool LeftKnee
        {
            get { return GetIntField(LEFT_KNEE) == 1; }
            set { SetField(LEFT_KNEE, Convert.ToInt32(value)); }
        }

        public int BodyMuscle
        {
            get
            {
                return (GetIntField(BODY_MUSCLE) < 100 ? GetIntField(BODY_MUSCLE) : 99);
            }
            set
            {
                SetField(BODY_MUSCLE, value);
            }
        }

        public int BodyFat
        {
            get
            {
                return (GetIntField(BODY_FAT) < 100 ? GetIntField(BODY_FAT) : 99);
            }
            set
            {
                SetField(BODY_FAT, value);
            }
        }

        public int EquipmentShoes
        {
            get
            {
                return (GetIntField(EQP_SHOES) < 100 ? GetIntField(EQP_SHOES) : 99);
            }
            set
            {
                SetField(EQP_SHOES, value);
            }
        }

        public int EquipmentPadHeight
        {
            get
            {
                return (GetIntField(EQP_PAD_HEIGHT) < 100 ? GetIntField(EQP_PAD_HEIGHT) : 99);
            }
            set
            {
                SetField(EQP_PAD_HEIGHT, value);
            }
        }

        public int EquipmentPadWidth
        {
            get
            {
                return (GetIntField(EQP_PAD_WIDTH) < 100 ? GetIntField(EQP_PAD_WIDTH) : 99);
            }
            set
            {
                SetField(EQP_PAD_WIDTH, value);
            }
        }

        public int EquipmentPadShelf
        {
            get
            {
                return (GetIntField(EQP_PAD_SHELF) < 100 ? GetIntField(EQP_PAD_SHELF) : 99);
            }
            set
            {
                SetField(EQP_PAD_SHELF, value);
            }
        }

        public int EquipmentFlakJacket
        {
            get
            {
                return (GetIntField(EQP_FLAK_JACKET) < 100 ? GetIntField(EQP_FLAK_JACKET) : 99);
            }
            set
            {
                SetField(EQP_FLAK_JACKET, value);
            }
        }

        public int ArmsMuscle
        {
            get
            {
                return (GetIntField(ARMS_MUSCLE) < 100 ? GetIntField(ARMS_MUSCLE) : 99);
            }
            set
            {
                SetField(ARMS_MUSCLE, value);
            }
        }

        public int ArmsFat
        {
            get
            {
                return (GetIntField(ARMS_FAT) < 100 ? GetIntField(ARMS_FAT) : 99);
            }
            set
            {
                SetField(ARMS_FAT, value);
            }
        }

        public int LegsThighMuscle
        {
            get
            {
                return (GetIntField(LEGS_THIGH_MUSCLE) < 100 ? GetIntField(LEGS_THIGH_MUSCLE) : 99);
            }
            set
            {
                SetField(LEGS_THIGH_MUSCLE, value);
            }
        }

        public int LegsThighFat
        {
            get
            {
                return (GetIntField(LEGS_THIGH_FAT) < 100 ? GetIntField(LEGS_THIGH_FAT) : 99);
            }
            set
            {
                SetField(LEGS_THIGH_FAT, value);
            }
        }

        public int LegsCalfMuscle
        {
            get
            {
                return (GetIntField(LEGS_CALF_MUSCLE) < 100 ? GetIntField(LEGS_CALF_MUSCLE) : 99);
            }
            set
            {
                SetField(LEGS_CALF_MUSCLE, value);
            }
        }

        public int LegsCalfFat
        {
            get
            {
                return (GetIntField(LEGS_CALF_FAT) < 100 ? GetIntField(LEGS_CALF_FAT) : 99);
            }
            set
            {
                SetField(LEGS_CALF_FAT, value);
            }
        }

        public int RearRearFat
        {
            get
            {
                return (GetIntField(REAR_FAT) < 100 ? GetIntField(REAR_FAT) : 99);
            }
            set
            {
                SetField(REAR_FAT, value);
            }
        }

        public int RearShape
        {
            get
            {
                return (GetIntField(REAR_SHAPE) < 100 ? GetIntField(REAR_SHAPE) : 99);
            }
            set
            {
                SetField(REAR_SHAPE, value);
            }
        }

        public int EyePaint
        {
            get { return GetIntField(EYE_PAINT); }
            set { SetField(EYE_PAINT, value); }            
        }

        public int CareerPhase
        {
            get
            {
                return GetIntField(CAREER_PHASE);
            }
            set
            {
                SetField(CAREER_PHASE, value);
            }
        }

        public int FaceShape
        {
            get
            {
                return (GetIntField(FACE_SHAPE) < 21 ? GetIntField(FACE_SHAPE) : 20);
            }
            set
            {
                SetField(FACE_SHAPE, value);
            }
        }

        #endregion
        
        public int Pwin
        {
            get { return GetIntField(PWIN); }
            set { SetField(PWIN, value); }
        }

        // 2007
        public int Ego
        {
            get
            {
                return GetIntField(PLAYER_EGO);
            }
            set
            {
                SetField(PLAYER_EGO, value);
            }
        }
        public int PlayerValue
        {
            get
            {
                return GetIntField(PLAYER_VALUE);
            }
            set
            {
                SetField(PLAYER_VALUE, value);
            }
        }
        public int PlayerRole
        {
            get
            {
                return GetIntField(PLAYER_ROLE);
            }
            set
            {
                SetField(PLAYER_ROLE, value);
            }
        }        
        // 2008
        public int PlayerWeapon
        {
            get
            {
                return GetIntField(PLAYER_WEAPON);
            }
            set
            {
                SetField(PLAYER_WEAPON, value);
            }
        }
        public int YearsWithTeam
        {
            get { return GetIntField(YEARS_WITH_TEAM); }
            set { SetField(YEARS_WITH_TEAM, value); }
        }

        //2019
        public int ImpactBlocking
        {
            get { return GetIntField(IMPACT_BLOCKING); }
            set { SetField(IMPACT_BLOCKING, value); }
        }
        public int BreakTackle19
        {
            get { return GetIntField(BREAK_TACKLE_19); }
            set { SetField(BREAK_TACKLE_19, value); }
        }
        public int LeadBlock
        {
            get { return GetIntField(LEAD_BLOCK); }
            set { SetField(LEAD_BLOCK, value); }
        }
        public int RunBlockFootwork
        {
            get { return GetIntField(RUN_BLOCK_FINESSE); }
            set { SetField(RUN_BLOCK_FINESSE, value); }
        }
        public int ZoneCoverage
        {
            get { return GetIntField(ZONE_COVERAGE); }
            set { SetField(ZONE_COVERAGE, value); }
        }
        public int ManCoverage
        {
            get { return GetIntField(MAN_COVERAGE); }
            set { SetField(MAN_COVERAGE, value); }
        }
        public int PlayRecognition
        {
            get { return GetIntField(PLAY_RECOGNITION); }
            set { SetField(PLAY_RECOGNITION, value); }
        }
        public int Pursuit
        {
            get { return GetIntField(PURSUIT); }
            set { SetField(PURSUIT, value); }
        }
        public int BlockShedding
        {
            get { return GetIntField(BLOCK_SHEDDING); }
            set { SetField(BLOCK_SHEDDING, value); }
        }
        public int Trucking
        {
            get { return GetIntField(TRUCKING); }
            set { SetField(TRUCKING, value); }
        }
        public int Elusive
        {
            get { return GetIntField(ELUSIVE); }
            set { SetField(ELUSIVE, value); }
        }
        public int RB_Vision
        {
            get { return GetIntField(BALL_CARRIER_VISION); }
            set { SetField(BALL_CARRIER_VISION, value); }
        }
        public int StiffArm
        {
            get { return GetIntField(STIFF_ARM); }
            set { SetField(STIFF_ARM, value); }
        }
        public int SpinMove
        {
            get { return GetIntField(SPIN_MOVE); }
            set { SetField(SPIN_MOVE, value); }
        }
        public int JukeMove
        {
            get { return GetIntField(JUKE_MOVE); }
            set { SetField(JUKE_MOVE, value); }
        }
        public int ShortRoute
        {
            get { return GetIntField(SHORT_ROUTE_RUN); }
            set { SetField(SHORT_ROUTE_RUN, value); }
        }
        public int MediumRoute
        {
            get { return GetIntField(MEDIUM_ROUTE_RUN); }
            set { SetField(MEDIUM_ROUTE_RUN, value); }
        }
        public int DeepRoute
        {
            get { return GetIntField(DEEP_ROUTE); }
            set { SetField(DEEP_ROUTE, value); }
        }
        public int CatchTraffic
        {
            get { return GetIntField(CATCH_TRAFFIC); }
            set { SetField(CATCH_TRAFFIC, value); }
        }
        public int SpecCatch
        {
            get { return GetIntField(SPEC_CATCH); }
            set { SetField(SPEC_CATCH, value); }
        }
        public int Release
        {
            get { return GetIntField(RELEASE); }
            set { SetField(RELEASE, value); }
        }
        public int ThrowShort
        {
            get { return GetIntField(THROW_SHORT); }
            set { SetField(THROW_SHORT, value); }
        }
        public int ThrowMedium
        {
            get { return GetIntField(THROW_MEDIUM); }
            set { SetField(THROW_MEDIUM, value); }
        }
        public int ThrowDeep
        {
            get { return GetIntField(THROW_DEEP); }
            set { SetField(THROW_DEEP, value); }
        }
        public int ThrowOnRun
        {
            get { return GetIntField(THROW_ON_RUN); }
            set { SetField(THROW_ON_RUN, value); }
        }
        public int ThrowPressure
        {
            get { return GetIntField(THROW_PRESSURE); }
            set { SetField(THROW_PRESSURE, value); }
        }
        public int BreakSack
        {
            get { return GetIntField(BREAK_SACK); }
            set { SetField(BREAK_SACK, value); }
        }
        public int PlayAction
        {
            get { return GetIntField(PLAY_ACTION); }
            set { SetField(PLAY_ACTION, value); }
        }
        public int HitPower
        {
            get { return GetIntField(HIT_POWER); }
            set { SetField(HIT_POWER, value); }
        }
        public int PowerMoves
        {
            get { return GetIntField(MOVES_POWER); }
            set { SetField(MOVES_POWER, value); }
        }
        public int FinesseMoves
        {
            get { return GetIntField(FINESSE_MOVES); }
            set { SetField(FINESSE_MOVES, value); }
        }
        public string Hometown
        {
            get { return GetStringField(HOMETOWN); }
            set { SetField(HOMETOWN, value); }
        }
        public int PressCover
        {
            get { return GetIntField(PRESS_COVER); }
            set { SetField(PRESS_COVER, value); }
        }
        public bool FightYards
        {
            get { return (GetIntField(FIGHT_FOR_YARDS) == 1); }
            set { SetField(FIGHT_FOR_YARDS, Convert.ToInt32(value)); }
        }
        public int ForcePasses
        {
            get { return GetIntField(FORCE_PASSES); }
            set { SetField(FORCE_PASSES, value); }
        }
        public int PassBlockFootwork
        {
            get { return GetIntField(PASSBLOCK_FOOTWORK); }
            set { SetField(PASSBLOCK_FOOTWORK, value); }
        }
        public int PassBlockStrength
        {
            get { return GetIntField(PASSBLOCK_STRENGTH); }
            set { SetField(PASSBLOCK_STRENGTH, value); }
        }
        public int PlaysBall
        {
            get { return GetIntField(PLAYS_BALL); }
            set { SetField(PLAYS_BALL, value); }
        }
        public int Potential
        {
            get { return GetIntField(PLAYER_POTENTIAL); }
            set { SetField(PLAYER_POTENTIAL, value); }
        }
        public int Stance
        {
            get { return GetIntField(STANCE); }
            set { SetField(STANCE, value); }
        }
        public int QBStyle
        {
            get { return GetIntField(QB_STYLE); }
            set { SetField(QB_STYLE, value); }
        }       
        public int RunBlockStrength
        {
            get { return GetIntField(RUNBLOCK_STRENGTH); }
            set { SetField(RUNBLOCK_STRENGTH, value); }
        }
        public int SensePressure
        {
            get { return GetIntField(SENSE_PRESSURE); }
            set { SetField(SENSE_PRESSURE, value); }
        }
        public int SidelineCatch
        {
            get { return GetIntField(SIDELINE_CATCH); }
            set { SetField(SIDELINE_CATCH, value); }
        }
        public bool ThrowAway
        {
            get { return (GetIntField(THROW_AWAY) == 1); }
            set { SetField(THROW_AWAY, Convert.ToInt32(value)); }
        }
        public bool ThrowSpiral
        {
            get { return (GetIntField(THROW_SPIRAL) == 1); }
            set { SetField(THROW_SPIRAL, Convert.ToInt32(value)); }
        }
        public int TuckRun
        {
            get { return GetIntField(TUCK_AND_RUN); }
            set { SetField(TUCK_AND_RUN, value); }
        }
        public bool PlayerTowel
        {
            get { return (GetIntField(PLAYER_TOWEL) == 1); }
            set { SetField(PLAYER_TOWEL, Convert.ToInt32(value)); }
        }
        public bool DLBullrush
        {
            get { return (GetIntField(DL_BULLRUSH) == 1); }
            set { SetField(DL_BULLRUSH, Convert.ToInt32(value)); }
        }
        public bool DLSpinmove
        {
            get { return (GetIntField(DL_SPINMOVE) == 1); }
            set { SetField(DL_SPINMOVE, Convert.ToInt32(value)); }
        }
        public bool DLSwim
        {
            get { return (GetIntField(DL_SWIM) == 1); }
            set { SetField(DL_SWIM, Convert.ToInt32(value)); }
        }
        public bool HighMotor
        {
            get { return (GetIntField(HIGH_MOTOR) == 1); }
            set { SetField(HIGH_MOTOR, Convert.ToInt32(value)); }
        }
        public int HomeState
        {
            get { return GetIntField(HOMESTATE); }
            set { SetField(HOMESTATE, value); }
        }
        public bool HandWarmer
        {
            get { return (GetIntField(HAND_WARMER) == 1); }
            set { SetField(HAND_WARMER, Convert.ToInt32(value)); }
        }
        public bool DropPasses
        {
            get { return (GetIntField(DROP_PASSES) == 1); }
            set { SetField(DROP_PASSES, Convert.ToInt32(value)); }
        }
        public bool BigHitter
        {
            get { return (GetIntField(BIG_HITTER) == 1); }
            set { SetField(BIG_HITTER, Convert.ToInt32(value)); }
        }
        public int CoversBall
        {
            get { return GetIntField(COVERS_BALL); }
            set { SetField(COVERS_BALL, value); }
        }
        public bool Clutch
        {
            get { return (GetIntField(CLUTCH) == 1); }
            set { SetField(CLUTCH, Convert.ToInt32(value)); }
        }
        public int FaceID_19
        {
            get { return GetIntField(FACE_ID_19); }
            set { SetField(FACE_ID_19, value); }
        }
        public bool TackleLow
        {
            get { return (GetIntField(TACKLE_LOW) == 1); }
            set { SetField(TACKLE_LOW, Convert.ToInt32(value)); }
        }
        public bool StripsBall
        {
            get { return (GetIntField(STRIPS_BALL) == 1); }
            set { SetField(STRIPS_BALL, Convert.ToInt32(value)); }
        }
        public int PressureMax
        {
            get { return GetIntField(PRESSURE_MAX); }
            set { SetField(PRESSURE_MAX, value); }
        }
        public string Asset
        {
            get { return GetStringField(PEPS); }
            set { SetField(PEPS, value); }
        }
        public bool PossessionCatch
        {
            get { return (GetIntField(POSSESSION_CATCH) == 1); }
            set { SetField(POSSESSION_CATCH, Convert.ToInt32(value)); }
        }
        
        public bool FeetInBounds
        {
            get { return (GetIntField(KEEP_FEET_IN_BOUNDS) == 1); }
            set { SetField(KEEP_FEET_IN_BOUNDS, Convert.ToInt32(value)); }
        }
        public int Penalty
        {
            get { return GetIntField(PENALTY); }
            set { SetField(PENALTY, value); }
        }
        public bool AggressiveCatch
        {
            get { return (GetIntField(AGGRESSIVE_CATCH) == 1); }
            set { SetField(AGGRESSIVE_CATCH, Convert.ToInt32(value)); }
        }
        public bool RunAfterCatch
        {
            get { return (GetIntField(RUN_AFTER_CATCH) == 1); }
            set { SetField(RUN_AFTER_CATCH, Convert.ToInt32(value)); }
        }
        public bool FlakJacket
        {
            get { return (GetIntField(FLAK_JACKET) == 1); }
            set { SetField(FLAK_JACKET, Convert.ToInt32(value)); }
        }
        public bool BackPlate
        {
            get { return (GetIntField(BACK_PLATE) == 1); }
            set { SetField(BACK_PLATE, Convert.ToInt32(value)); }
        }
        
        public int Birthday
        {
            get { return GetIntField(BIRTHDAY); }
            set { SetField(BIRTHDAY, value); }
        }
        public int AnkleLeft
        {
            get { return GetIntField(ANKLE_LEFT); }
            set { SetField(ANKLE_LEFT, value); }
        }
        public int AnkleRight
        {
            get { return GetIntField(ANKLE_RIGHT); }
            set { SetField(ANKLE_RIGHT, value); }
        }
        public int SleevesRight
        {
            get { return GetIntField(SLEEVES_RIGHT); }
            set { SetField(SLEEVES_RIGHT, value); }
        }
        public int EndPlay
        {
            get { return GetIntField(ENDPLAY); }
            set { SetField(ENDPLAY, value); }
        }
        public int SockHeight
        {
            get { return GetIntField(SOCK_HEIGHT); }
            set { SetField(SOCK_HEIGHT, value); }
        }
        public int KneeLeft
        {
            get { return GetIntField(KNEE_LEFT); }
            set { SetField(KNEE_LEFT, value); }
        }
        public int KneeRight
        {
            get { return GetIntField(KNEE_RIGHT); }
            set { SetField(KNEE_RIGHT, value); }
        }
        public int Confidence
        {
            get { return GetIntField(CONFIDENCE); }
            set { SetField(CONFIDENCE, value); }
        }
        public int PlayerType
        {
            get { return GetIntField(PLAYER_TYPE); }
            set { SetField(PLAYER_TYPE, value); }
        }
        public int Trdp
        {
            get { return GetIntField(TRDP); }
            set { SetField(TRDP, value); }
        }
        public float ArmDefn
        {
            get { return GetFloatField(ARM_DEFN); }
            set { SetField(ARM_DEFN, value); }
        }
        public float ArmSize
        {
            get { return GetFloatField(ARM_SIZE); }
            set { SetField(ARM_SIZE, value); }
        }
        public float ButtDefn
        {
            get { return GetFloatField(BUTT_DEFN); }
            set { SetField(BUTT_DEFN, value); }
        }
        public float ButtSize
        {
            get { return GetFloatField(BUTT_SIZE); }
            set { SetField(BUTT_SIZE, value); }
        }
        public float CalfDefn
        {
            get { return GetFloatField(CALF_DEFN); }
            set { SetField(CALF_DEFN, value); }
        }
        public float CalfSize
        {
            get { return GetFloatField(CALF_SIZE); }
            set { SetField(CALF_SIZE, value); }
        }
        public float FootDefn
        {
            get { return GetFloatField(FOOT_DEFN); }
            set { SetField(FOOT_DEFN, value); }
        }
        public float FootSize
        {
            get { return GetFloatField(FOOT_SIZE); }
            set { SetField(FOOT_SIZE, value); }
        }
        public float GutDefn
        {
            get { return GetFloatField(GUT_DEFN); }
            set { SetField(GUT_DEFN, value); }
        }
        public float GutSize
        {
            get { return GetFloatField(GUT_SIZE); }
            set { SetField(GUT_SIZE, value); }
        }
        public float PadDefn
        {
            get { return GetFloatField(PAD_DEFN); }
            set { SetField(PAD_DEFN, value); }
        }
        public float PadSize
        {
            get { return GetFloatField(PAD_SIZE); }
            set { SetField(PAD_SIZE, value); }
        }
        public float ShoulderDefn
        {
            get { return GetFloatField(SHOULDER_DEFN); }
            set { SetField(SHOULDER_DEFN, value); }
        }
        public float ShoulderSize
        {
            get { return GetFloatField(SHOULDER_SIZE); }
            set { SetField(SHOULDER_SIZE, value); }
        }
        public float ThighDefn
        {
            get { return GetFloatField(THIGH_DEFN); }
            set { SetField(THIGH_DEFN, value); }
        }
        public float ThighSize
        {
            get { return GetFloatField(THIGH_SIZE); }
            set { SetField(THIGH_SIZE, value); }
        }
        public float WaistDefn
        {
            get { return GetFloatField(WAIST_DEFN); }
            set { SetField(WAIST_DEFN, value); }
        }
        public float WaistSize
        {
            get { return GetFloatField(WAIST_SIZE); }
            set { SetField(WAIST_SIZE, value); }
        }
        public bool IsCaptain
        {
            get { return (GetIntField(IS_CAPTAIN) == 1); }
            set { SetField(IS_CAPTAIN, Convert.ToInt32(value)); }
        }
        public int UnderShirt
        {
            get { return GetIntField(UNDERSHIRT); }
            set { SetField(UNDERSHIRT,value);}
        }
        public bool PumpFake
        {
            get { return (GetIntField(PUMP_FAKE) == 1); }
            set { SetField(PUMP_FAKE, Convert.ToInt32(value)); }
        }
        public int Pnec
        {
            get { return GetIntField(PNEC); }
            set { SetField(PNEC, value); }
        }
        public int SidelineHeadgear
        {
            get { return GetIntField(SIDELINE_HEADGEAR); }
            set { SetField(SIDELINE_HEADGEAR, value); }
        }
        public int XPRate
        {
            get { return GetIntField(XP_RATE); }
            set { SetField(XP_RATE, value); }
        }
        public string GetBirthday()
        {
            // s68 - I find bit shifting confusing, got this formula from xananthol's madden 360 roster reader
            // and adjusted the year.
            string bd = "";
            int month = ((this.Birthday & 0x00000780) >> 7) + 1;
            int day = ((this.Birthday & 0x0000F800) >> 11);
            int year = (this.Birthday & 0x0000007F) + 1940;

            bd = month.ToString() + "/" + day.ToString() + "/" + year.ToString();
            return bd;
        }

        public void SetBirthday(string bday)
        {
            // s68, Got this function from xanathol's xbox 360 roster editor, adjusted year
            int bd = 0;
            int temp = 0;
            string[] arr = bday.ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (arr.Length != 3)
                return;

            temp = Convert.ToInt32(arr[1]);
            if (temp < 1) temp = 1;
            if (temp > 31) temp = 31;
            bd |= ((0x0000001F & temp) << 11);

            temp = Convert.ToInt32(arr[0]);
            if (temp < 1) temp = 1;
            if (temp > 12) temp = 12;
            bd |= ((0x0000000F & (temp - 1)) << 7);

            temp = Convert.ToInt32(arr[2]);
            if (temp < 1956) temp = 1956;
            temp -= 1940;
            bd |= (0x0000007F & temp);

            this.Birthday = bd;
        }

        #endregion

        #region Functions

        public int GetRating(int AttributeID)
        {
            switch (AttributeID)
            {
                case (int)Rating.ACC:
                    return Acceleration;                
                case (int)Rating.AGI:
                    return Agility;
                case (int)Rating.AWR:
                    return Awareness;
                case (int)Rating.BTK:
                    return BreakTackle;
                case (int)Rating.CAR:
                    return Carrying;
                case (int)Rating.CTH:
                    return Catching;
                case (int)Rating.INJ:
                    return Injury;
                case (int)Rating.JMP:
                    return Jumping;
                case (int)Rating.KAC:
                    return KickAccuracy;
                case (int)Rating.KPW:
                    return KickPower;
                case (int)Rating.KRT:
                    return KickReturn;                
                case (int)Rating.PBK:
                    return PassBlocking;
                case (int)Rating.RBK:
                    return RunBlocking;
                case (int)Rating.SPD:
                    return Speed;                
                case (int)Rating.STR:
                    return Strength;
                case (int)Rating.TAK:
                    return Tackle;                
                case (int)Rating.THA:
                    return ThrowAccuracy;
                case (int)Rating.THP:
                    return ThrowPower;
                case (int)Rating.IMP:
                    return Importance;
                case (int)Rating.BKS:
                    return BreakSack;
                case (int)Rating.PWA:       //  ???
                    return PlayAction;
                case (int)Rating.THD:
                    return ThrowDeep;
                case (int)Rating.THM:
                    return ThrowMedium;
                case (int)Rating.TOR:
                    return ThrowOnRun;
                case (int)Rating.THS:
                    return ThrowShort;
                case (int)Rating.TUP:
                    return ThrowPressure;
                case (int)Rating.TRU:
                    return Trucking;
                case (int)Rating.ELU:
                    return Elusive;
                case (int)Rating.JUK:
                    return JukeMove;
                case (int)Rating.SPN:
                    return SpinMove;
                case (int)Rating.SFA:
                    return StiffArm;
                case (int)Rating.VIS:
                    return RB_Vision;
                case (int)Rating.SRR:
                    return ShortRoute;
                case (int)Rating.BKT:
                    return BreakTackle19;
                case (int)Rating.CIT:
                    return CatchTraffic;
                case (int)Rating.SPC:
                    return SpecCatch;
                case (int)Rating.IBK:
                    return ImpactBlocking;
                case (int)Rating.LBK:
                    return LeadBlock;
                case (int)Rating.RBS:
                    return RunBlockStrength;
                case (int)Rating.RBF:
                    return RunBlockFootwork;
                case (int)Rating.PBF:
                    return PassBlockFootwork;
                case (int)Rating.PBS:
                    return PassBlockStrength;
                case(int)Rating.DRR:
                    return DeepRoute;
                case(int)Rating.MRR:
                    return MediumRoute;
                case (int)Rating.REL:
                    return Release;
                case (int)Rating.SHD:
                    return BlockShedding;
                case (int)Rating.HIT:
                    return HitPower;
                case (int)Rating.FNM:
                    return FinesseMoves;
                case (int)Rating.PWM:
                    return PowerMoves;
                case (int)Rating.PLR:
                    return PlayRecognition;
                case (int)Rating.PUR:
                    return Pursuit;
                case (int)Rating.MAN:
                    return ManCoverage;
                case (int)Rating.ZON:
                    return ZoneCoverage;
                case (int)Rating.KRR:
                    return KickReturn;
            }

            return 0;
        }

        #endregion


        #region Madden Draft Edit

        public int GetAttribute(int AttributeID)
        {
            switch (AttributeID)
            {
                case (int)MaddenAttribute.ACC:
                    return Acceleration;
                case (int)MaddenAttribute.AGE:
                    return Age;
                case (int)MaddenAttribute.AGI:
                    return Agility;
                case (int)MaddenAttribute.AWR:
                    return Awareness;
                case (int)MaddenAttribute.BTK:
                    return BreakTackle;
                case (int)MaddenAttribute.CAR:
                    return Carrying;
                case (int)MaddenAttribute.CTH:
                    return Catching;
                case (int)MaddenAttribute.INJ:
                    return Injury;
                case (int)MaddenAttribute.JMP:
                    return Jumping;
                case (int)MaddenAttribute.KAC:
                    return KickAccuracy;
                case (int)MaddenAttribute.KPR:
                    return KickPower;
                case (int)MaddenAttribute.KRT:
                    return KickReturn;
                case (int)MaddenAttribute.OVR:
                    return Overall;
                case (int)MaddenAttribute.PBK:
                    return PassBlocking;
                case (int)MaddenAttribute.RBK:
                    return RunBlocking;
                case (int)MaddenAttribute.SPD:
                    return Speed;
                case (int)MaddenAttribute.STA:
                    return Stamina;
                case (int)MaddenAttribute.STR:
                    return Strength;
                case (int)MaddenAttribute.TAK:
                    return Tackle;
                case (int)MaddenAttribute.TGH:
                    return Toughness;
                case (int)MaddenAttribute.THA:
                    return ThrowAccuracy;
                case (int)MaddenAttribute.THP:
                    return ThrowPower;
                case (int)MaddenAttribute.YRP:
                    return YearsPro;
                //2007
                case (int)MaddenAttribute.EGO:
                    return Ego;
                case (int)MaddenAttribute.VAL:
                    return PlayerValue;
            }

            return -1;
        }

        public void SetAttribute(int AttributeID, int value)
        {
            value = Math.Min(99, Math.Max(0, value));

            switch (AttributeID)
            {
                case (int)MaddenAttribute.ACC:
                    Acceleration = value;
                    break;
                case (int)MaddenAttribute.AGE:
                    Age = value;
                    break;
                case (int)MaddenAttribute.AGI:
                    Agility = value;
                    break;
                case (int)MaddenAttribute.AWR:
                    Awareness = value;
                    break;
                case (int)MaddenAttribute.BTK:
                    BreakTackle = value;
                    break;
                case (int)MaddenAttribute.CAR:
                    Carrying = value;
                    break;
                case (int)MaddenAttribute.CTH:
                    Catching = value;
                    break;
                case (int)MaddenAttribute.INJ:
                    Injury = value;
                    break;
                case (int)MaddenAttribute.JMP:
                    Jumping = value;
                    break;
                case (int)MaddenAttribute.KAC:
                    KickAccuracy = value;
                    break;
                case (int)MaddenAttribute.KPR:
                    KickPower = value;
                    break;
                case (int)MaddenAttribute.KRT:
                    KickReturn = value;
                    break;
                case (int)MaddenAttribute.OVR:
                    Overall = value;
                    break;
                case (int)MaddenAttribute.PBK:
                    PassBlocking = value;
                    break;
                case (int)MaddenAttribute.RBK:
                    RunBlocking = value;
                    break;
                case (int)MaddenAttribute.SPD:
                    Speed = value;
                    break;
                case (int)MaddenAttribute.STA:
                    Stamina = value;
                    break;
                case (int)MaddenAttribute.STR:
                    Strength = value;
                    break;
                case (int)MaddenAttribute.TAK:
                    Tackle = value;
                    break;
                case (int)MaddenAttribute.TGH:
                    Toughness = value;
                    break;
                case (int)MaddenAttribute.THA:
                    ThrowAccuracy = value;
                    break;
                case (int)MaddenAttribute.THP:
                    ThrowPower = value;
                    break;
                case (int)MaddenAttribute.YRP:
                    YearsPro = value;
                    break;
                //2007
                case (int)MaddenAttribute.EGO:
                    Ego = value;
                    break;
                case (int)MaddenAttribute.VAL:
                    PlayerValue = value;
                    break;

            }
        }

        public string RatingsLine(string[] attributes)
        {
            string toReturn = "";
            string last = attributes[attributes.Length - 1];

            foreach (string s in attributes)
            {
                toReturn += GetIntField(s);

                if (s != last)
                {
                    toReturn += "\t";
                }
            }

            return toReturn;
        }

        public void ImportWeeklyData(string[] attributes, string[] ratings)
        {
            int index = 0;
            foreach (string s in attributes)
            {
                if (s == "PGID") { index++; continue; }
                SetField(s, Int32.Parse(ratings[index]));
                index++;
            }
        }

        public void ImportData(List<string> playerData, int version)
        {
            int index = 0;
            foreach (string s in editorModel.DraftClassFields[version - 1])
            {
                if (ContainsStringField(s))
                {
                    SetField(s, playerData[index]);
                }
                else if (ContainsIntField(s))
                {
                    SetField(s, Int32.Parse(playerData[index]));
                }
                else
                {
                    Trace.WriteLine("Severe Error!  Player does not contain field " + s + "!  Returning...");
                    return;
                }

                index++;
            }
        }

        public int CalculateOverallRating(int positionId)
		{
			return CalculateOverallRating(positionId, false);
		}

		public int CalculateOverallRating(int positionId, bool withHeightAndWeight)
		{
			double tempOverall = 0;

			if (withHeightAndWeight)
			{
				LocalMath lmath = new LocalMath(MaddenFileVersion.Ver2006);
				tempOverall += lmath.HeightWeightAdjust(this, positionId);
			}

			switch (positionId)
			{
				case (int)MaddenPositions.QB:
					tempOverall += (((double)ThrowPower - 50) / 10) * 4.9;
					tempOverall += (((double)ThrowAccuracy - 50) / 10) * 5.8;
					tempOverall += (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Awareness - 50) / 10) * 4.0;
					tempOverall += (((double)Speed - 50) / 10) * 2.0;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.HB:
					tempOverall += (((double)PassBlocking - 50) / 10) * 0.33;
					tempOverall += (((double)BreakTackle - 50) / 10) * 3.3;
					tempOverall += (((double)Carrying - 50) / 10) * 2.0;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall += (((double)Agility - 50) / 10) * 2.8;
					tempOverall += (((double)Awareness - 50) / 10) * 2.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.6;
					tempOverall += (((double)Speed - 50) / 10) * 3.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27,1);
					break;
				case (int)MaddenPositions.FB:
					tempOverall+= (((double)PassBlocking - 50) / 10) * 1.0;
					tempOverall+= (((double)RunBlocking - 50) / 10) * 7.2;
					tempOverall+= (((double)BreakTackle - 50) / 10) * 1.8;
					tempOverall+= (((double)Carrying - 50) / 10) * 1.8;
					tempOverall+= (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall+= (((double)Agility - 50) / 10) * 1.0;
					tempOverall+= (((double)Awareness - 50) / 10) * 2.8;
					tempOverall+= (((double)Strength - 50) / 10) * 1.8;
					tempOverall+= (((double)Speed - 50) / 10) * 1.8;
					tempOverall+= (((double)Catching - 50) / 10) * 5.2;
					tempOverall= (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 39,1);
					break;
				case (int)MaddenPositions.WR:
					tempOverall += (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.3;
					tempOverall += (((double)Agility - 50) / 10) * 2.3;
					tempOverall += (((double)Awareness - 50) / 10) * 2.3;
					tempOverall += (((double)Strength - 50) / 10) * 0.8;
					tempOverall += (((double)Speed - 50) / 10) * 2.3;
					tempOverall += (((double)Catching - 50) / 10) * 4.75;
					tempOverall += (((double)Jumping - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.TE:
					tempOverall += (((double)Speed - 50) / 10) * 2.65;
					tempOverall += (((double)Strength - 50) / 10) * 2.65;
					tempOverall += (((double)Awareness - 50) / 10) * 2.65;
					tempOverall += (((double)Agility - 50) / 10) * 1.25;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.25;
					tempOverall += (((double)Catching - 50) / 10) * 5.4;
					tempOverall += (((double)BreakTackle - 50) / 10) * 1.2;
					tempOverall += (((double)PassBlocking - 50) / 10) * 1.2;
					tempOverall += (((double)RunBlocking - 50) / 10) * 5.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 35, 1);
					break;
				case (int)MaddenPositions.LT:
				case (int)MaddenPositions.RT:
					tempOverall += (((double)Speed - 50) / 10) * 0.8;
					tempOverall += (((double)Strength - 50) / 10) * 3.3;
					tempOverall += (((double)Awareness - 50) / 10) * 3.3;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 0.8;
					tempOverall += (((double)PassBlocking - 50) / 10) * 4.75;
					tempOverall += (((double)RunBlocking - 50) / 10) * 3.75;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.LG:
				case (int)MaddenPositions.RG:
				case (int)MaddenPositions.C:
					tempOverall += (((double)Speed - 50) / 10) * 1.7;
					tempOverall += (((double)Strength - 50) / 10) * 3.25;
					tempOverall += (((double)Awareness - 50) / 10) * 3.25;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)PassBlocking - 50) / 10) * 3.25;
					tempOverall += (((double)RunBlocking - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.LE:
				case (int)MaddenPositions.RE:
					tempOverall += (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.75;
					tempOverall += (((double)Awareness - 50) / 10) * 1.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.75;
					tempOverall += (((double)Acceleration - 50) / 10) * 3.8;
					tempOverall += (((double)Tackle - 50) / 10) * 5.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.DT:
					tempOverall += (((double)Speed - 50) / 10) * 1.8;
					tempOverall += (((double)Strength - 50) / 10) * 5.5;
					tempOverall += (((double)Awareness - 50) / 10) * 3.8;
					tempOverall += (((double)Agility - 50) / 10) * 1;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.8;
					tempOverall += (((double)Tackle - 50) / 10) * 4.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.LOLB:
				case (int)MaddenPositions.ROLB:
					tempOverall += (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 2.4;
					tempOverall += (((double)Awareness - 50) / 10) * 3.6;
					tempOverall += (((double)Agility - 50) / 10) * 2.4;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.3;
					tempOverall += (((double)Tackle - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.MLB:
					tempOverall += (((double)Speed - 50) / 10) * 0.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.4;
					tempOverall += (((double)Awareness - 50) / 10) * 5.2;
					tempOverall += (((double)Agility - 50) / 10) * 1.65;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.75;
					tempOverall += (((double)Tackle - 50) / 10) * 5.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
					break;
				case (int)MaddenPositions.CB:
					tempOverall += (((double)Speed - 50) / 10) * 3.85;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 3.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.55;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.35;
					tempOverall += (((double)Catching - 50) / 10) * 3;
					tempOverall += (((double)Jumping - 50) / 10) * 1.55;
					tempOverall += (((double)Tackle - 50) / 10) * 1.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.FS:
					tempOverall += (((double)Speed - 50) / 10) * 3.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 4.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.5;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.5;
					tempOverall += (((double)Catching - 50) / 10) * 3.0;
					tempOverall += (((double)Jumping - 50) / 10) * 1.5;
					tempOverall += (((double)Tackle - 50) / 10) * 2.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.SS:
					tempOverall += (((double)Speed - 50) / 10) * 3.2;
					tempOverall += (((double)Strength - 50) / 10) * 1.7;
					tempOverall += (((double)Awareness - 50) / 10) * 4.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.7;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)Catching - 50) / 10) * 3.2;
					tempOverall += (((double)Jumping - 50) / 10) * 0.9;
					tempOverall += (((double)Tackle - 50) / 10) * 3.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.P:
					tempOverall = (double)(-183 + 0.218*Awareness + 1.5 * KickPower + 1.33 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;
				case (int)MaddenPositions.K:
					tempOverall = (double)(-177 + 0.218*Awareness + 1.28 * KickPower + 1.47 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;
			}

			if (tempOverall < 0)
			{
				tempOverall = 0;
			}
			if (tempOverall > 99)
			{
				tempOverall = 99;
			}

			return (int)tempOverall;
		}
                
        public DataGridViewRow GetDataRow(int positionId, int overall)
        {
            DataGridViewRow viewRow = new DataGridViewRow();

            DataGridViewTextBoxCell posCell = new DataGridViewTextBoxCell();
            posCell.Value = Enum.GetNames(typeof(MaddenPositions2019))[PositionId];
            DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
            nameCell.Value = FirstName + " " + LastName;
            
            DataGridViewTextBoxCell ovrCell = new DataGridViewTextBoxCell();
            if (overall == -1)
                ovrCell.Value = CalculateOverallRating(positionId);
            else ovrCell.Value = overall;

            DataGridViewTextBoxCell playerCell = new DataGridViewTextBoxCell();
            playerCell.Value = this;
            viewRow.Cells.Add(posCell);
            viewRow.Cells.Add(nameCell);
            viewRow.Cells.Add(ovrCell);
            viewRow.Cells.Add(playerCell);

            posCell.ReadOnly = true;
            nameCell.ReadOnly = true;
            ovrCell.ReadOnly = true;
            playerCell.ReadOnly = true;

            return viewRow;
        }

        #endregion
        
        #region Salary Signing Bonus Functions
        // fix all the salary cap functions
        
        public int GetSalaryAtYear(int year)
		{
			string key = "PSA" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)YearlySalary[year];
			}
		}

		public int GetSigningBonusAtYear(int year)
		{
			string key = "PSB" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)YearlyBonus[year];
			}
		}
           
        public int GetCurrentSalary()
        {
            int year = ContractLength - ContractYearsLeft;
            int tempCurrentSalary = 0;

            if (ContractYearsLeft == 0)
            {
                CurrentSalary = 0;
                return 0;
            }
            else if (ContainsField(SALARY_YEAR_0))
            {
                tempCurrentSalary = GetIntField("PSA" + year) + GetIntField("PSB" + year);
                if (tempCurrentSalary != CurrentSalary)
                    CurrentSalary = (int)tempCurrentSalary;                
            }
            else
            {
                if (YearlySalary == null)
                    SetContract(true, false, .30);
                tempCurrentSalary = YearlySalary[year] + YearlyBonus[year];
            }

            return tempCurrentSalary;
        }
        		
        public void SetContract(bool checkrookie, bool causeDirty, double perc)
        {
            if (ContractLength == 0 || ContractLength > 7)
                return;            

            // Default Contracts are not supposed to increase more than 30% each year, 25% for rookies            
            double x = perc/100 + 1;
            if (checkrookie)
            {
                if (YearsPro == 0 || PreviousContractLength == 0 && ContractLength >= YearsPro)
                    x = 1.25;
            }

            perc = 0;
            for (int t = 0; t < ContractLength; t++)            
                perc += Math.Pow(x, t);

            double tempsal = (double)(TotalSalary - TotalBonus) / perc;

            YearlySalary = new int[7];
            YearlyBonus = new int[7];
            for (int i = 0; i < 7; i++)
            {
                YearlySalary[i] = 0;
                YearlyBonus[i] = 0;
            }

            int lastsal = 0;
            int lastbon = 0;
            for (int i = 0; i < ContractLength; i++)
            {
                if (i < ContractLength - 1)
                {
                    YearlySalary[i] = (int)Math.Round(tempsal * Math.Pow(x, i), 2);
                    YearlyBonus[i] = (int)Math.Round((double)TotalBonus / ContractLength,0);
                }
                else
                {
                    YearlySalary[i] = TotalSalary - TotalBonus - lastsal;
                    YearlyBonus[i] = TotalBonus - lastbon;
                }
                
                lastsal += YearlySalary[i];
                lastbon += YearlyBonus[i];
            }

            if (causeDirty && ContainsField(SALARY_YEAR_0))
            {
                // If updating is requested and yearly salary/bonus fields exist, save
                for (int i = 0; i < 7; i++)
                {
                    string key = "PSA" + i;
                    SetField(key, (int)YearlySalary[i], true);
                    key = "PSB" + i;
                    SetField(key, (int)YearlyBonus[i], true);
                }
            }

            if (causeDirty)
                GetCurrentSalary();
        }

        public void ClearContract()
        {
            if (ContractLength !=0)
                ContractLength = 0;
            if (ContractYearsLeft != 0)
                ContractYearsLeft = 0;
            if (TotalBonus != 0)
                TotalBonus = 0;
            if (TotalSalary != 0)
                TotalSalary = 0;

            // This may need to stay set if a Team released a player and is still responsible for this
            // year's salary
            // if (CurrentSalary != 0)
            //     CurrentSalary = 0;

            if (ContainsField(SALARY_YEAR_0))
            {
                if (Salary0 != 0)
                    Salary0 = 0;
                if (Salary1 != 0)
                    Salary1 = 0;
                if (Salary2 != 0)
                    Salary2 = 0;
                if (Salary3 != 0)
                    Salary3 = 0;
                if (Salary4 != 0)
                    Salary4 = 0;
                if (Salary5 != 0)
                    Salary5 = 0;
                if (Salary6 != 0)
                    Salary6 = 0;
                if (Bonus0 != 0)
                    Bonus0 = 0;
                if (Bonus1 != 0)
                    Bonus1 = 0;
                if (Bonus2 != 0)
                    Bonus2 = 0;
                if (Bonus3 != 0)
                    Bonus3 = 0;
                if (Bonus4 != 0)
                    Bonus4 = 0;
                if (Bonus5 != 0)
                    Bonus5 = 0;
                if (Bonus6 != 0)
                    Bonus6 = 0;
                
            }
        }

        public void FixYearlyContract()
        {
            // Minimum is 10,000 so these are going to be off if EA used actual contract numbers
            // and any year requires less than a multiple of 10,000
            int sal = 0;
            int bon = 0;
            sal = Salary0 + Salary1 + Salary2 + Salary3 + Salary4 + Salary5 + Salary6;
            bon = Bonus0 + Bonus1 + Bonus2 + Bonus3 + Bonus4 + Bonus5 + Bonus6;

            if (bon < TotalBonus)
            {
                // Tack on any owed bonus on the final year
                for (int c = 6; c >= 0; c--)
                {
                    if (GetIntField("PSB" + c.ToString()) > 0)
                    {
                        SetField("PSB" + c.ToString(), GetIntField("PSB" + c.ToString()) + TotalBonus - bon);
                        break;
                    }
                }
            }
            else if (bon > TotalBonus)
                TotalBonus = bon;

            if (sal + TotalBonus < TotalSalary)
            {
                // Tack on any owed salary on the final year
                for (int c = 6; c >= 0; c--)
                {
                    if (GetIntField("PSA" + c.ToString()) > 0)
                    {
                        SetField("PSA" + c.ToString(), GetIntField("PSA" + c.ToString()) + TotalSalary - (TotalBonus + sal));
                        break;
                    }
                }
            }
            else if (sal > TotalSalary)
                TotalSalary = sal + TotalBonus;
        }
        
        #endregion
	}
}