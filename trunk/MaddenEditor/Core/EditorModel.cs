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
using System.Runtime.InteropServices;
using System.Text;

using MaddenEditor.Db;
using MaddenEditor.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.FranchiseState;
using MaddenEditor.Core.DatEditor;


namespace MaddenEditor.Core
{
    #region Enums
    /// <summary>
	/// This enumeration helps identify the different kinds of files we can load
	/// with this editor
	/// </summary>
	public enum MaddenFileType 
	{ 
		Roster, 
		Franchise,
        Streameddata,
        Template,
        DBTeam,
        UserConfig,
        DataRam,
        StaticData,
        GameMode,
        Unknown,
	}

    public enum FranchiseState
    {
        RestrictedFreeAgents = 0,
        ResignPlayers
    }

	//  Roster/Franchise versions
    public enum MaddenFileVersion
	{
        Ver2004,    //Franchise contains 136 tables
		Ver2005,	//Franchise contains 159 tables
		Ver2006,	//Franchise contains 183 tables
		Ver2007,    //Franchise contains 185 tables
        Ver2008,    //Franchise contains 191 tables
        Ver2019
	}

	/// <summary>
	/// Enumeration to describe the positions in the Madden game. The order of these is
	/// important as they match up with the position ID's in the database
	/// </summary>
    public enum PlayerDraftedPositions
    {
        QB = 0,
        HB = 1,
        FB = 2,
        WR = 3,
        TE = 4,
        T = 5,
        G = 6,
        C = 7,
        DE = 8,
        DT = 9,
        OLB = 10,
        MLB = 11,
        CB = 12,
        FS = 13,
        SS = 14,
        K = 15,
        P = 16
    }

    public enum MaddenPositions2019
    {
        // 33 positions
        QB = 0,
        HB = 1,
        FB = 2,
        WR = 3,
        TE = 4,
        LT = 5,
        LG = 6,
        C = 7,
        RG = 8,
        RT = 9,
        LE = 10,
        RE = 11,
        DT = 12,
        LOLB = 13,
        MLB = 14,
        ROLB = 15,
        CB = 16,
        FS = 17,
        SS = 18,
        K = 19,
        P = 20,
        KR = 21,
        PR = 22,
        KOS = 23,
        LS = 24,
        TDB = 25,
        PHB = 26,
        SWR = 27,
        RLE = 28,
        RRE = 29,
        RDT = 30,
        SLB = 31,
        SCB = 32,
    }
    
    public enum MaddenPositions
	{
		// 21 positions
        QB = 0,
		HB,
		FB,
		WR,
		TE,
		LT,
		LG,
		C,
		RG,
		RT,
		LE,
		RE,
		DT,
		LOLB,
		MLB,
		ROLB,
		CB,
		FS,
		SS,
		K,
        P, 
        KR = 21,
        PR = 22,
        KOS = 23,
        LS = 24,
        TDB = 25,
}

    public enum PlayerRoles
    {
        // v2008 specific, v2007 is different
        QBFuture = 0,
        ClutchKick = 1,
        TeamCapt = 2,
        NFLIcon = 3,
        UnderAchiever = 4,
        TeamMentor = 5,
        TeamLeader = 6,
        ProjectPlayer = 7,
        TeamDistraction = 8,
        CaptComeback = 9,
        GameManager = 10,
        ReturnSpec = 11,
        FirstRndPick = 12,
        FanFave = 13,
        InjuryProne = 14,
        FumbleProne = 15,
        FutureStar = 16,
        PrecisePasser = 17,
        CanonArm = 18,
        Scrambler = 19,
        FranchiseQB = 20,
        PowerRB = 21,
        ElusiveRB = 22,
        SpeedRB= 23,
        RunBlocker = 24,
        PassBlocker = 25,
        RoadBlock = 26,
        ForceNature = 27,
        HeavyHitter = 28,
        ContainCB = 29,
        QuickCB = 30,
        BigHitter = 31,
        CoverS = 32,
        Hitman = 33,
        FutureRB = 34,
        GotoGuy = 35,
        DeepThreat = 36,
        PossWR = 37,
        ShutdownCB = 38,
        PassRusher = 39,
        RunStopper = 40,
        DefEnforcer = 41,
        Playmaker = 42,
        NONE = 43,
    }
       
    public enum PlayerRating
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
        IMP = 17,
        INJ = 18,
        STA = 19,
        TGH = 20,
        ALL = 31,
    }

    public enum IdType
    {
        TeamID = 0,
        OrigID = 1,
        FranID = 2,
        RosterID = 3,
        DivID = 4,

    }

    #endregion

    public class college_entry
    {
        public string name = "";
        public int orig_id = 0;
        public string conf = "";
        public int rep = 0;

        public college_entry(int year, int id, string name, int rep)
        {
            this.orig_id = id;
            this.name = name;
            this.rep = rep;

            #region Get conferences
            // ACC
            if (id == 20 || id == 38 || id == 52 || id == 64 || id == 71 || id==105 || id==166||id==205
                || id == 115 || id == 135 || id == 138 || id == 240 || id == 241 || id == 249)
            {
                if (year < 2014 && id == 105)
                    conf = "BigE";
                else if (year < 2014 && id == 166)
                    conf = "BigE";
                else if (year < 2013 && id == 205)
                    conf = "BigE";
                else conf = "ACC";
            }

            //Big12
            if (id == 17 || id == 90 || id == 95 || id == 96 || id == 157 || id == 158
                || id == 207 || id == 214 || id == 217 || id == 254)
            {
                if (year < 2012 && id == 207)
                    conf = "WAC";
                else if (year < 2012 && id == 254)
                    conf = "BigE";
                else conf = "BIG12";
            }

            // Big east / AAC   Complicated
            if (id == 36 || id == 227 || id == 105 || id == 166 || id == 232 || id == 205 || id == 208
                || id == 54 || id == 80|| id == 114 || id == 192 || id == 221 || id == 222 || id == 225)
            {
                if (year < 2013)
                {
                    conf = "BigE";
                    if (id == 54 || id == 80 || id == 114 || id == 192 || id == 221 || id == 222 || id == 225)
                        conf = "CUSA";
                }
                else conf = "AAC";
            }
    
            // BIG10
            if (id == 84 || id == 86 || id == 89 || id == 111 || id == 117 || id == 118 || id == 120 
                || id == 142 || id == 151 || id == 156 || id == 164 || id == 169 || id == 175 || id == 258)
            {
                if (year < 2011 && id == 151)
                    conf = "BIG12";
                else if (year < 2014 && id == 111)
                    conf = "ACC";
                else if (year < 2104 && id == 175)
                    conf = "BigE";
                else conf = "BIG10";
            }

            // Sec
            if (id == 3||id==10||id==14||id==62||id==70||id==98|| id==106
                || id == 159|| id ==121||id==122||id == 176||id==211||id==215||id==238)
            {
                if (year < 2012 && id == 122)
                    conf = "BIG12";
                else if (year < 212 && id == 215)
                    conf = "BIG12";
                else conf = "SEC";
            }

            // Pac12
            if (id == 8 || id == 9||id==28||id==41||id==160||1d==161
                ||id==184||id==226||id==231||id==233||id==252||id==251)
            {               
                if (year < 2011 && id == 41)
                    conf = "BIG12";
                else if (year < 2011 && id == 233)
                    conf = "MTNW";
                else if (year < 2011)
                    conf = "PAC10";
                else conf = "PAC12";
            }

            // Mountain West
            if (id == 1||id==19||id==42||id==66||id==76
                ||id==143||id==145||id==180||id==230||id==260)
            {
                conf = "MTN W";
            }
            
            // MAC
            if (id == 2||id==16||id==21||id==24||id==33||id==58
                ||id==97||id==112||id==116||id==137||id==218||id==246)
            {
                conf = "MAC";
            }

            // Sun Belt
            if (id == 12 || id == 119 || id == 148 || id == 220 || id == 245)
            {
                conf = "Sun";
            }
            // WAC
            if (id == 82 || id == 101 || id == 144 || id == 185 || id == 204 || id == 234)
                conf = "WAC";
            #endregion

            #region Get rep
            if (rep == -1)
            {
                if (id == 152 || id == 117 || id == 115 || id == 106 || id == 89 || id == 70 || id == 64
                    || id == 62 || id == 14 || id == 3)
                    this.rep = 50;
                else if (id == 215)
                    this.rep = 45;
                else if (id == 160 || id == 158 || id == 138 || id == 122 || id == 111 || id == 110 || id == 105 || id == 96)
                    this.rep = 40;
                else if (id == 166 || id == 161 || id == 159 || id == 135 || id == 76 || id == 41 || id == 38 || id == 28 || id == 21)
                    this.rep = 35;
                else if (id == 243 || id == 225 || id == 205)
                    this.rep = 30;
                else if (id == 254 || id == 252 || id == 240 || id == 233 || id == 207 || id == 200 || id == 10 || id == 9 || id == 8)
                    this.rep = 25;
                else if (id == 204)
                    this.rep = 20;
                else if (id == 1)
                    this.rep = 15;
                else this.rep = 0;
            }

            #endregion
        }        
    }

    public class tabledefs
    {        
        public Dictionary<string, string> FieldDefs;                // Field and descriptions
        public Dictionary<string, Dictionary<int,string>> Equivs;   // Fields that have equivalents in different versions

        public tabledefs()
        {
            FieldDefs = new Dictionary<string, string>();
            Equivs = new Dictionary<string, Dictionary<int, string>>();
        }
    }

    
    /// <summary>
	/// This class is the main application model class. It is responsible for
	/// creating all editing models that are manipulated by the GUI.
	/// </summary>
	public class EditorModel
    {
        #region Members
        public bool BigEndian = false;
        public const string SUPPORT_EMAIL = "bugs@tributech.com.au";
		public const int FREE_AGENT_TEAM_ID = 1009;
        public const int RETIRED_TEAM_ID = 1014;
        public const int ROOKIES = 1015;
        public const string UNKNOWN_TEAM_NAME = "UNKNOWN_TEAM";        
        public const string RETIRED = "Retired";

        public const int MADDEN_ROS_2004_TABLE_COUNT = 11;
        public const int MADDEN_ROS_2005_TABLE_COUNT = 11;
		public const int MADDEN_ROS_2006_TABLE_COUNT = 11;
		public const int MADDEN_ROS_2007_TABLE_COUNT = 10;
        public const int MADDEN_ROS_2008_TABLE_COUNT = 10;
        public const int MADDEN_ROS_2019_TABLE_COUNT = 5;

		public const int MADDEN_FRA_2004_TABLE_COUNT = 136;
		public const int MADDEN_FRA_2005_TABLE_COUNT = 159;
		public const int MADDEN_FRA_2006_TABLE_COUNT = 183;
		public const int MADDEN_FRA_2007_TABLE_COUNT = 185;
        public const int MADDEN_FRA_2008_TABLE_COUNT = 191;
       
    	public const int MADDEN_ROS_2004_PLAY_FIELD_COUNT = 112;
		public const int MADDEN_ROS_2005_PLAY_FIELD_COUNT = 115;
		public const int MADDEN_ROS_2006_PLAY_FIELD_COUNT = 111;
		public const int MADDEN_ROS_2007_PLAY_FIELD_COUNT = 110;
        public const int MADDEN_ROS_2008_PLAY_FIELD_COUNT = 110;
        public const int MADDEN_ROS_2019_PLAY_FIELD_COUNT = 208;

        public const int MADDEN_2004_STREAMED_COUNT = 72;
        public const int MADDEN_2005_STREAMED_COUNT = 116;
        public const int MADDEN_2006_STREAMED_COUNT = 168;
        public const int MADDEN_2007_STREAMED_COUNT = 203;
        public const int MADDEN_2008_STREAMED_COUNT = 215;
        public const int MADDEN_2019_STREAMED_COUNT = 85;

        public const int MADDEN_2019_USER_CONFIG_COUNT = 16;
        public const int MADDEN_2019_DATA_RAM = 23;
        public const int MADDEN_2019_STATIC_DATA = 16;
        public const int MADDEN_2019_GAMEMODE = 85;

        // New 2019 type
        public const int MADDEN_2019_DBTEAM_COUNT = 8;
        
        #region Franchise / Roster  Table names       
        
        public const string PLAYER_AWARDS_TABLE = "AYPL";
        public const string BOXSCORE_DEFENSE_TABLE = "BDEF";
        public const string BOXSCORE_OFFENSE_TABLE = "BOFF";
        public const string BOXSCORE_OFFENSIVE_LINE_TABLE = "BOLN";
        public const string BOXSCORE_SCORING_SUMMARY = "BSCS";
        public const string BOXSCORE_TEAM_TABLE = "BTES";
        public const string CITY_TABLE = "CITY";
        public const string COACH_TABLE = "COCH";
        public const string COACH_SLIDER_TABLE = "CPSE";
        public const string DEPTH_CHART_TABLE = "DCHT";
        public const string DRAFT_STATE_TABLE = "DRIN";
        public const string DRAFT_PICK_TABLE = "DRPK";
        public const string DRAFTED_PLAYERS_TABLE = "DRPL";
        public const string FREE_AGENCY_STATE_TABLE = "FAIN";
        public const string FREE_AGENT_PLAYERS = "FAPL";
        public const string GAME_OPTIONS_TABLE = "GOPT";
        public const string INJURY_TABLE = "INJY";
        public const string FRANCHISE_STAGE_TABLE = "MOIN";        
        public const string COACHING_HISTORY_TABLE = "OCIS";
        public const string COACHES_EXPECTED_SALARY = "OSCE";
        public const string WEEKLY_INCOME_TABLE = "OTIW";
        public const string TEAM_WIN_LOSS_RECORD = "OTRS";
        public const string LEAGUE_REVENUE_TABLE = "OWMI";
        public const string OWNER_REVENUE_TABLE = "OWTI";
        public const string OWNER_TABLE = "OWNR";
        public const string CAREER_STATS_DEFENSE_TABLE = "PCDE";
        public const string CAREER_STATS_KICKPUNT_TABLE = "PCKI";
        public const string CAREER_STATS_KICKPUNT_RETURN_TABLE = "PCKP";
        public const string CAREER_GAMES_PLAYED_TABLE = "PCNG";
        public const string CAREER_STATS_OFFENSE_TABLE = "PCOF";
        public const string CAREER_STATS_OFFENSIVE_LINE_TABLE = "PCOL";
        public const string PLAYER_TABLE = "PLAY";
        public const string INACTIVE_TABLE = "PLIA";
        public const string PRO_BOWL_PLAYERS = "PPBS";
        // public const string PLAYER_PREVIOUS_RATINGS = "PROR"
        public const string SEASON_STATS_DEFENSE_TABLE = "PSDE";
        public const string SEASON_STATS_KICKPUNT_TABLE = "PSKI";
        public const string SEASON_STATS_KICKPUNT_RETURN_TABLE = "PSKP";
        public const string SEASON_GAMES_PLAYED_TABLE = "PSNG";
        public const string SEASON_STATS_OFFENSE_TABLE = "PSOF";
        public const string SEASON_STATS_OFFENSIVE_LINE_TABLE = "PSOL";
        public const string RESIGN_PLAYERS_STATE_TABLE = "REIN";
        public const string RFA_STATE_TABLE = "RFIN";                       // 2005+
        public const string RFA_PLAYERS = "RFPL";                           // 2005+
        public const string RFA_SALARY_TENDERS = "RFST";
        // public const string COACH_SALARY_DEMAND_TABLE = "SACP";
        public const string PLAYER_SALARY_DEMAND_TABLE = "SADP";
        public const string PLAYER_MINIMUM_SALARY_TABLE = "SAYP";
        public const string SALARY_CAP_INCREASE = "SAYR";
        public const string SCHEDULE_TABLE = "SCHD";
        public const string SCOUTING_STATE_TABLE = "SCIN";
        public const string PROGRESSION_SCHEDULE = "SCWE";                     // 2005+
        public const string FRANCHISE_TIME_TABLE = "SEAI";
        public const string SALARY_CAP_TABLE = "SLRI";
        public const string STADIUM_TABLE = "STAD";
        public const string TEAM_CAPTAIN_TABLE = "TCPT";
		public const string TEAM_TABLE = "TEAM";
        public const string TEAM_GAME_RECORDS = "TMGR";   
        public const string TEAM_SEASON_RECORDS = "TMSR";
        public const string TEAM_SEASON_STATS = "TSSE";
        public const string TEAM_RIVAL_HISTORY = "TSRI";
        public const string UNIFORM_TABLE = "TUNI";
        public const string USER_INFO_TABLE = "UINF";
        public const string USER_OPTIONS_TABLE = "UOPT";

        

        #endregion

        #region Streameddata.db Table names

        public const string COACH_COLLECTIONS_TABLE = "COCH";
        public const string COLLEGES_TABLE = "COLL";
        public const string DEPTH_CHART_SUBS = "DCSB";
        public const string POSITION_SUBS = "DPOS";
        public const string PLAYER_FIRST_NAMES = "SPNM";
        public const string PLAYER_LAST_NAMES = "DRYR";
        public const string ROLES_DEFINE = "PRDF";
        public const string PTCB = "PTCB";
        public const string PTCE = "PTCE";
        public const string PTDE = "PTDE";
        public const string PTDT = "PTDT";
        public const string PTFB = "PTFB";
        public const string PTFS = "PTFS";
        public const string PTGA = "PTGA";
        public const string PTHB = "PTHB";
        public const string PTKI = "PTKI";
        public const string PTKP = "PTKP";
        public const string PTMB = "PTMB";
        public const string PTOB = "PTOB";
        public const string PTPU = "PTPU";
        public const string PTQB = "PTQB";
        public const string PTSS = "PTSS";
        public const string PTTA = "PTTA";
        public const string PTTE = "PTTE";
        public const string PTWR = "PTWR";
        public const string PROGRESSION = "PWRA";
        public const string REGRESSION = "PYRA";
        public const string ROLES_INFO = "RINF";
        public const string ROLES_PLAYER_EFFECTS = "RLPM";
        public const string ROLES_TEAM_EFFECTS = "RLTM";
        public const string STATS_REQUIRED = "STRQ";
        
        #endregion

        #region DB Templates

        public const string PLAYER_OVERALL_CALC = "PORC";
        public const string PLAYBOOK_TABLE = "PLAB";

        #endregion

        private List<string[]> draftClassFields;
        public bool draftStarted = false;
        private bool dirty = false;
		private int dbIndex = -1;
		public int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private TableModelDictionary tableModels = null;
		private MaddenFileType fileType = MaddenFileType.Roster;
        private MaddenFileVersion fileVersion = MaddenFileVersion.Ver2004;
		private Dictionary<string, int> tableOrder = null;
        public Dictionary<string, int> TableNames = new Dictionary<string, int>();
		
		// Editing model objects
		private PlayerEditingModel playerEditingModel = null;
		private CoachEditingModel coachEditingModel = null;
		private TeamEditingModel teamEditingModel = null;
        private StadiumEditingModel stadiumEditingModel = null;
		private SalaryCapRecord salaryCapRecord = null;
		private GameOptionRecord gameOptionsRecord = null;
        private UserOptionRecord useroptions = null;
        private DraftClass draftclassmodel = null;
        private UserInfoRecord userinfo = null;

        // Latest adds       
        public static int totalplayers = 0;        
        public Dictionary<int, college_entry> Colleges = new Dictionary<int, college_entry>();
        public int CurrentYearIndex = 0;
        public int CurrentYear = 0;
        public Dictionary<int,string> PlayerRole = new Dictionary<int,string>();        
        public Dictionary<int, double> LeagueCap = new Dictionary<int, double>();

        public Dictionary<MaddenFileVersion, Dictionary<string, tabledefs>> TableDefs = new Dictionary<MaddenFileVersion, Dictionary<string, tabledefs>>();

        #endregion
        
        
        #region Constructors

        public EditorModel(string filename, MainForm form, bool be, bool ismad19)
		{
            this.BigEndian = be;
            totalplayers = 0;
            view = form;
			this.fileName = filename;            

			// MADDEN DRAFT EDIT
			draftClassFields = new List<string[]>();
			draftClassFields.Add(new string[] { "PFNA", "PLNA", "PPOS", 
				"PCOL", "PAGE", "PWGT", "PHGT", "PHAN", "POVR", "PSPD",
				"PSTR", "PAWR", "PAGI", "PACC", "PCTH", "PCAR", "PJMP",
				"PBTK", "PTAK", "PTHP", "PTHA", "PPBK", "PRBK",	"PKPR",
				"PKAC", "PKRT", "PSTA", "PINJ", "PTGH", "PSTY", "PMOR", 
				"PSBS", /*"PTPS",*/ "PMTS", "PUTS", "PFTS", "PLSS", "PTSS",
				"PWSS", "PCHS", "PQTS", "PMAS", "PFAS", "PMHS", "PFHS", 
				"PMCS", "PFCS", /*"PMGS", "PQGS",*/ "PSKI", "PHCL", "PHED", 
				"PEYE", "PNEK", "PVIS", "PMPC", "PLHA", "TLHA", "PRHA",
				"TRHA", "PLSH", "PRSH", "PLTH", "PRTH", "PLEL", "TLEL",
				"PREL", "TREL", "PGSL", "PTSL", "PLWR", "TLWR", "PRWR",
				"TRWR", "PBRE", "PTAL", "PTAR", "PHLM", "PFMK", "PFEx" });
			// MADDEN DRAFT EDIT

			//Try and open the file
			try
			{
				dbIndex = TDB.TDBOpen(filename);                
                // s68 - the dll usage says if -1 is returned the database load failed.
                // using the passed through string "filename" returns -1
                // using the private string "fileName" returns 0                
			}
			catch (DllNotFoundException e)
			{
				ExceptionDialog.Show(e);
				throw new ApplicationException("Missing: " + e.ToString());
			}
			catch (ApplicationException e) 
			{
				ExceptionDialog.Show(e);
				throw e;
			}

			//We create a collection of database table names that we want to load
			tableOrder = new Dictionary<string, int>();

			//This collection will hold the created TableModel objects for each database table opened
			tableModels = new TableModelDictionary(this, tableOrder);

            // Load in the csv for tables/fields
            InitDefinitions();
            // Init Salary Cap
            InitCap();
            
            //Process the file
			if (!ProcessFile())
			{
				//throw new ApplicationException("Error processing file: " + filename);
                
			}
			
			//Once we've processed the file create our editing models
            if (this.FileType != MaddenFileType.Streameddata && this.fileType != MaddenFileType.Template)
            {
                teamEditingModel = new TeamEditingModel(this);
                playerEditingModel = new PlayerEditingModel(this);
                draftclassmodel = new DraftClass(this);

                if (fileVersion == MaddenFileVersion.Ver2019)
                {
                    if (fileType == MaddenFileType.DBTeam)
                    {
                        coachEditingModel = new CoachEditingModel(this);
                    }
                    else if (fileType == MaddenFileType.UserConfig)
                    {
                        gameOptionsRecord = (GameOptionRecord)TableModels[GAME_OPTIONS_TABLE].GetRecord(0);
                        useroptions = (UserOptionRecord)TableModels[USER_OPTIONS_TABLE].GetRecord(0);
                        userinfo = (UserInfoRecord)TableModels[USER_INFO_TABLE].GetRecord(0);
                    }

                }

                else
                {
                    coachEditingModel = new CoachEditingModel(this);
                    stadiumEditingModel = new StadiumEditingModel(this);
                    if (fileType == MaddenFileType.Franchise)
                    {
                        salaryCapRecord = (SalaryCapRecord)TableModels[SALARY_CAP_TABLE].GetRecord(0);
                        gameOptionsRecord = (GameOptionRecord)TableModels[GAME_OPTIONS_TABLE].GetRecord(0);
                        useroptions = (UserOptionRecord)TableModels[USER_OPTIONS_TABLE].GetRecord(0);
                    }
                } 
            }            
		}

		#endregion

        public string GetDraftPickName(int picknum)
        {
            picknum++;
            if (picknum > 224)
                return "";
            else
            {
                int round = (int)Math.Ceiling((decimal)picknum / 32);
                int pick = picknum - (round - 1) * 32;
                return "R " + round.ToString() + " - " + pick.ToString();
            }
        }
        
        public void InitRoles(EditorModel streamed)
        {
            if (streamed != null)
            {
                 foreach (RoleInfo role in streamed.TableModels[EditorModel.ROLES_INFO].GetRecords())
                     PlayerRole.Add(role.PlayerRole, role.RoleName);
                 if (FileVersion == MaddenFileVersion.Ver2007)
                     if (!PlayerRole.ContainsKey(31))
                         PlayerRole.Add(31, "None");
                     else
                         if (!PlayerRole.ContainsKey(45))
                             PlayerRole.Add(45, "None");
                 return;
            }
            
            PlayerRole.Add(0,"QBFuture");

            if (FileVersion == MaddenFileVersion.Ver2007)
            {
                PlayerRole.Add(31, "None");
                PlayerRole.Add(1, "Feature Back");
                PlayerRole.Add(2, "Franchise");
                PlayerRole.Add(3, "Goto Guy");
                PlayerRole.Add(4, "NFL Starter");
                PlayerRole.Add(5, "Clutch Kicker");
                PlayerRole.Add(6, "Team Captain");
                PlayerRole.Add(7, "NFL Icon");
                PlayerRole.Add(8, "Deep Threat");
                PlayerRole.Add(9, "Possession Receiver");
                PlayerRole.Add(10, "Shutdown Corner");
                PlayerRole.Add(11, "Underachiever");
                PlayerRole.Add(12, "Pass Rusher");
                PlayerRole.Add(13, "Team Mentor");
                PlayerRole.Add(14, "Run Stopper");
                PlayerRole.Add(15, "Team Leader");
                PlayerRole.Add(16, "Project Player");
                PlayerRole.Add(17, "Team Distraction");
                PlayerRole.Add(18, "Captain Comeback");
                PlayerRole.Add(19, "Game Manager");
                PlayerRole.Add(20, "Return Specialist");
                PlayerRole.Add(21, "Offensive Playmaker");
                PlayerRole.Add(22, "1st Round Pick");
                PlayerRole.Add(23, "Defensive Enforcer");
                PlayerRole.Add(24, "Fan Favorite");
                PlayerRole.Add(25, "Injury Prone");
                PlayerRole.Add(26, "Fumble Prone");
                PlayerRole.Add(27, "Defensive Playmaker");
                PlayerRole.Add(28, "Future Star");
            }
            else
            {
                PlayerRole.Add(45, "None");
                PlayerRole.Add(1, "Clutch Kicker");
                PlayerRole.Add(2, "Team Captain");
                PlayerRole.Add(3, "NFL Icon");
                PlayerRole.Add(4, "Underachiever");
                PlayerRole.Add(5, "Team Mentor");
                PlayerRole.Add(6, "Team Leader");
                PlayerRole.Add(7, "Project Player");
                PlayerRole.Add(8, "Team Distraction");
                PlayerRole.Add(9, "Captain Comeback");
                PlayerRole.Add(10, "Game Manager");
                PlayerRole.Add(11, "Return Specialist");
                PlayerRole.Add(12, "1st Round Pick");
                PlayerRole.Add(13, "Fan Favorite");
                PlayerRole.Add(14, "Injury Prone");
                PlayerRole.Add(15, "Fumble Prone");
                PlayerRole.Add(16, "Future Star");
                PlayerRole.Add(17, "Precision Passer");
                PlayerRole.Add(18, "Cannon Arm");
                PlayerRole.Add(19, "Scrambler");
                PlayerRole.Add(20, "Franchise");
                PlayerRole.Add(21, "Power Back");
                PlayerRole.Add(22, "Elusive Back");
                PlayerRole.Add(23, "Speed Back");
                PlayerRole.Add(24, "Run Blocker");
                PlayerRole.Add(25, "Pass Blocker");
                PlayerRole.Add(26, "Road Blocker");
                PlayerRole.Add(27, "Force of Nature");
                PlayerRole.Add(29, "Containment Corner");
                PlayerRole.Add(30, "Quick Corner");
                PlayerRole.Add(31, "Big Hitter");
                PlayerRole.Add(32, "Coverage Safety");
                PlayerRole.Add(33, "Hit Man");
                PlayerRole.Add(34, "Feature Back");
                PlayerRole.Add(35, "Goto Guy");
                PlayerRole.Add(36, "Deep Threat");
                PlayerRole.Add(37, "Possession Receiver");
                PlayerRole.Add(38, "Shutdown Corner");
                PlayerRole.Add(39, "Pass Rusher");
                PlayerRole.Add(40, "Run Stopper");
                PlayerRole.Add(41, "Defensive Enforcer");
                PlayerRole.Add(42, "Playmaker");
                PlayerRole.Add(43, "NW");
            }

        }
        
        public void InitColleges(MGMT man)
        {           
            Colleges = new Dictionary<int,college_entry>();
            if (man.stream_model == null)
            {
                #region 04-08
                if (fileVersion < MaddenFileVersion.Ver2019)
                {
                    Colleges.Add(0, new college_entry(CurrentYearIndex, 0, "Abilene Chr.", -1));
                    Colleges.Add(1, new college_entry(CurrentYearIndex, 1, "Air Force", -1));
                    Colleges.Add(2, new college_entry(CurrentYearIndex, 2, "Akron", -1));
                    Colleges.Add(3, new college_entry(CurrentYearIndex, 3, "Alabama", -1));
                    Colleges.Add(4, new college_entry(CurrentYearIndex, 4, "Alabama A&M", -1));
                    Colleges.Add(5, new college_entry(CurrentYearIndex, 5, "Alabama St.", -1));
                    Colleges.Add(6, new college_entry(CurrentYearIndex, 6, "Alcorn St.", -1));
                    Colleges.Add(7, new college_entry(CurrentYearIndex, 7, "Appalach. St.", -1));
                    Colleges.Add(8, new college_entry(CurrentYearIndex, 8, "Arizona", -1));
                    Colleges.Add(9, new college_entry(CurrentYearIndex, 9, "Arizona St.", -1));
                    Colleges.Add(10, new college_entry(CurrentYearIndex, 10, "Arkansas", -1));
                    Colleges.Add(11, new college_entry(CurrentYearIndex, 11, "Arkansas P.B.", -1));
                    Colleges.Add(12, new college_entry(CurrentYearIndex, 12, "Arkansas St.", -1));
                    Colleges.Add(13, new college_entry(CurrentYearIndex, 13, "Army", -1));
                    Colleges.Add(14, new college_entry(CurrentYearIndex, 14, "Auburn", -1));
                    Colleges.Add(15, new college_entry(CurrentYearIndex, 15, "Austin Peay", -1));
                    Colleges.Add(16, new college_entry(CurrentYearIndex, 16, "Ball State", -1));
                    Colleges.Add(17, new college_entry(CurrentYearIndex, 17, "Baylor", -1));
                    Colleges.Add(18, new college_entry(CurrentYearIndex, 18, "Beth Cookman", -1));
                    Colleges.Add(19, new college_entry(CurrentYearIndex, 19, "Boise State", -1));
                    Colleges.Add(20, new college_entry(CurrentYearIndex, 20, "Boston Coll.", -1));
                    Colleges.Add(21, new college_entry(CurrentYearIndex, 21, "Bowl. Green", -1));
                    Colleges.Add(22, new college_entry(CurrentYearIndex, 22, "Brown", -1));
                    Colleges.Add(23, new college_entry(CurrentYearIndex, 23, "Bucknell", -1));
                    Colleges.Add(24, new college_entry(CurrentYearIndex, 24, "Buffalo", -1));
                    Colleges.Add(25, new college_entry(CurrentYearIndex, 25, "Butler", -1));
                    Colleges.Add(26, new college_entry(CurrentYearIndex, 26, "BYU", -1));
                    Colleges.Add(27, new college_entry(CurrentYearIndex, 27, "Cal Poly SLO", -1));
                    Colleges.Add(28, new college_entry(CurrentYearIndex, 28, "California", -1));
                    Colleges.Add(29, new college_entry(CurrentYearIndex, 29, "Cal-Nrthridge", -1));
                    Colleges.Add(30, new college_entry(CurrentYearIndex, 30, "Cal-Sacrmnto", -1));
                    Colleges.Add(31, new college_entry(CurrentYearIndex, 31, "Canisius", -1));
                    Colleges.Add(32, new college_entry(CurrentYearIndex, 32, "Cent Conn St.", -1));
                    Colleges.Add(33, new college_entry(CurrentYearIndex, 33, "Central MI", -1));
                    Colleges.Add(34, new college_entry(CurrentYearIndex, 34, "Central St Ohio", -1));
                    Colleges.Add(35, new college_entry(CurrentYearIndex, 35, "Charleston S.", -1));
                    Colleges.Add(36, new college_entry(CurrentYearIndex, 36, "Cincinnati", -1));
                    Colleges.Add(37, new college_entry(CurrentYearIndex, 37, "Citadel", -1));
                    Colleges.Add(38, new college_entry(CurrentYearIndex, 38, "Clemson", -1));
                    Colleges.Add(39, new college_entry(CurrentYearIndex, 39, "Clinch Valley", -1));
                    Colleges.Add(40, new college_entry(CurrentYearIndex, 40, "Colgate", -1));
                    Colleges.Add(41, new college_entry(CurrentYearIndex, 41, "Colorado", -1));
                    Colleges.Add(42, new college_entry(CurrentYearIndex, 42, "Colorado St.", -1));
                    Colleges.Add(43, new college_entry(CurrentYearIndex, 43, "Columbia", -1));
                    Colleges.Add(44, new college_entry(CurrentYearIndex, 44, "Cornell", -1));
                    Colleges.Add(45, new college_entry(CurrentYearIndex, 45, "Culver-Stockton", -1));
                    Colleges.Add(46, new college_entry(CurrentYearIndex, 46, "Dartmouth", -1));
                    Colleges.Add(47, new college_entry(CurrentYearIndex, 47, "Davidson", -1));
                    Colleges.Add(48, new college_entry(CurrentYearIndex, 48, "Dayton", -1));
                    Colleges.Add(49, new college_entry(CurrentYearIndex, 49, "Delaware", -1));
                    Colleges.Add(50, new college_entry(CurrentYearIndex, 50, "Delaware St.", -1));
                    Colleges.Add(51, new college_entry(CurrentYearIndex, 51, "Drake", -1));
                    Colleges.Add(52, new college_entry(CurrentYearIndex, 52, "Duke", -1));
                    Colleges.Add(53, new college_entry(CurrentYearIndex, 53, "Duquesne", -1));
                    Colleges.Add(54, new college_entry(CurrentYearIndex, 54, "E. Carolina", -1));
                    Colleges.Add(55, new college_entry(CurrentYearIndex, 55, "E. Illinois", -1));
                    Colleges.Add(56, new college_entry(CurrentYearIndex, 56, "E. Kentucky", -1));
                    Colleges.Add(57, new college_entry(CurrentYearIndex, 57, "E. Tenn. St.", -1));
                    Colleges.Add(58, new college_entry(CurrentYearIndex, 58, "East. Mich.", -1));
                    Colleges.Add(59, new college_entry(CurrentYearIndex, 59, "Eastern Wash.", -1));
                    Colleges.Add(60, new college_entry(CurrentYearIndex, 60, "Elon College", -1));
                    Colleges.Add(61, new college_entry(CurrentYearIndex, 61, "Fairfield", -1));
                    Colleges.Add(62, new college_entry(CurrentYearIndex, 62, "Florida", -1));
                    Colleges.Add(63, new college_entry(CurrentYearIndex, 63, "Florida A&M", -1));
                    Colleges.Add(64, new college_entry(CurrentYearIndex, 64, "Florida State", -1));
                    Colleges.Add(65, new college_entry(CurrentYearIndex, 65, "Fordham", -1));
                    Colleges.Add(66, new college_entry(CurrentYearIndex, 66, "Fresno State", -1));
                    Colleges.Add(67, new college_entry(CurrentYearIndex, 67, "Furman", -1));
                    Colleges.Add(68, new college_entry(CurrentYearIndex, 68, "Ga. Southern", -1));
                    Colleges.Add(69, new college_entry(CurrentYearIndex, 69, "Georgetown", -1));
                    Colleges.Add(70, new college_entry(CurrentYearIndex, 70, "Georgia", -1));
                    Colleges.Add(71, new college_entry(CurrentYearIndex, 71, "Georgia Tech", -1));
                    Colleges.Add(72, new college_entry(CurrentYearIndex, 72, "Grambling St.", -1));
                    Colleges.Add(73, new college_entry(CurrentYearIndex, 73, "Grand Valley St.", -1));
                    Colleges.Add(74, new college_entry(CurrentYearIndex, 74, "Hampton", -1));
                    Colleges.Add(75, new college_entry(CurrentYearIndex, 75, "Harvard", -1));
                    Colleges.Add(76, new college_entry(CurrentYearIndex, 76, "Hawaii", -1));
                    Colleges.Add(77, new college_entry(CurrentYearIndex, 77, "Henderson St.", -1));
                    Colleges.Add(78, new college_entry(CurrentYearIndex, 78, "Hofstra", -1));
                    Colleges.Add(79, new college_entry(CurrentYearIndex, 79, "Holy Cross", -1));
                    Colleges.Add(80, new college_entry(CurrentYearIndex, 80, "Houston", -1));
                    Colleges.Add(81, new college_entry(CurrentYearIndex, 81, "Howard", -1));
                    Colleges.Add(82, new college_entry(CurrentYearIndex, 82, "Idaho", -1));
                    Colleges.Add(83, new college_entry(CurrentYearIndex, 83, "Idaho State", -1));
                    Colleges.Add(84, new college_entry(CurrentYearIndex, 84, "Illinois", -1));
                    Colleges.Add(85, new college_entry(CurrentYearIndex, 85, "Illinois St.", -1));
                    Colleges.Add(86, new college_entry(CurrentYearIndex, 86, "Indiana", -1));
                    Colleges.Add(87, new college_entry(CurrentYearIndex, 87, "Indiana St.", -1));
                    Colleges.Add(88, new college_entry(CurrentYearIndex, 88, "Iona", -1));
                    Colleges.Add(89, new college_entry(CurrentYearIndex, 89, "Iowa", -1));
                    Colleges.Add(90, new college_entry(CurrentYearIndex, 90, "Iowa State", -1));
                    Colleges.Add(91, new college_entry(CurrentYearIndex, 91, "J. Madison", -1));
                    Colleges.Add(92, new college_entry(CurrentYearIndex, 92, "Jackson St.", -1));
                    Colleges.Add(93, new college_entry(CurrentYearIndex, 93, "Jacksonv. St.", -1));
                    Colleges.Add(94, new college_entry(CurrentYearIndex, 94, "John Carroll", -1));
                    Colleges.Add(95, new college_entry(CurrentYearIndex, 95, "Kansas", -1));
                    Colleges.Add(96, new college_entry(CurrentYearIndex, 96, "Kansas State", -1));
                    Colleges.Add(97, new college_entry(CurrentYearIndex, 97, "Kent State", -1));
                    Colleges.Add(98, new college_entry(CurrentYearIndex, 98, "Kentucky", -1));
                    Colleges.Add(99, new college_entry(CurrentYearIndex, 99, "Kutztown", -1));
                    Colleges.Add(100, new college_entry(CurrentYearIndex, 100, "La Salle", -1));
                    Colleges.Add(101, new college_entry(CurrentYearIndex, 101, "LA. Tech", 1));
                    Colleges.Add(102, new college_entry(CurrentYearIndex, 102, "Lambuth", -1));
                    Colleges.Add(103, new college_entry(CurrentYearIndex, 103, "Lehigh", -1));
                    Colleges.Add(104, new college_entry(CurrentYearIndex, 104, "Liberty", -1));
                    Colleges.Add(105, new college_entry(CurrentYearIndex, 105, "Louisville", -1));
                    Colleges.Add(106, new college_entry(CurrentYearIndex, 106, "LSU", -1));
                    Colleges.Add(107, new college_entry(CurrentYearIndex, 107, "M. Valley St.", -1));
                    Colleges.Add(108, new college_entry(CurrentYearIndex, 108, "Maine", -1));
                    Colleges.Add(109, new college_entry(CurrentYearIndex, 109, "Marist", -1));
                    Colleges.Add(110, new college_entry(CurrentYearIndex, 110, "Marshall", -1));
                    Colleges.Add(111, new college_entry(CurrentYearIndex, 111, "Maryland", -1));
                    Colleges.Add(112, new college_entry(CurrentYearIndex, 112, "Massachusetts", -1));
                    Colleges.Add(113, new college_entry(CurrentYearIndex, 113, "McNeese St.", -1));
                    Colleges.Add(114, new college_entry(CurrentYearIndex, 114, "Memphis", 1));
                    Colleges.Add(115, new college_entry(CurrentYearIndex, 115, "Miami", -1));
                    Colleges.Add(116, new college_entry(CurrentYearIndex, 116, "Miami Univ.", -1));
                    Colleges.Add(117, new college_entry(CurrentYearIndex, 117, "Michigan", 1));
                    Colleges.Add(118, new college_entry(CurrentYearIndex, 118, "Michigan St.", 1));
                    Colleges.Add(119, new college_entry(CurrentYearIndex, 119, "Mid Tenn St.", -1));
                    Colleges.Add(120, new college_entry(CurrentYearIndex, 120, "Minnesota", -1));
                    Colleges.Add(121, new college_entry(CurrentYearIndex, 121, "Miss. State", -1));
                    Colleges.Add(122, new college_entry(CurrentYearIndex, 122, "Missouri", -1));
                    Colleges.Add(123, new college_entry(CurrentYearIndex, 123, "Monmouth", -1));
                    Colleges.Add(124, new college_entry(CurrentYearIndex, 124, "Montana", -1));
                    Colleges.Add(125, new college_entry(CurrentYearIndex, 125, "Montana State", -1));
                    Colleges.Add(126, new college_entry(CurrentYearIndex, 126, "Morehead St.", -1));
                    Colleges.Add(127, new college_entry(CurrentYearIndex, 127, "Morehouse", -1));
                    Colleges.Add(128, new college_entry(CurrentYearIndex, 128, "Morgan St.", -1));
                    Colleges.Add(129, new college_entry(CurrentYearIndex, 129, "Morris Brown", -1));
                    Colleges.Add(130, new college_entry(CurrentYearIndex, 130, "Mt S. Antonio", -1));
                    Colleges.Add(131, new college_entry(CurrentYearIndex, 131, "Murray State", -1));
                    Colleges.Add(132, new college_entry(CurrentYearIndex, 132, "N. Alabama", -1));
                    Colleges.Add(133, new college_entry(CurrentYearIndex, 133, "N. Arizona", -1));
                    Colleges.Add(134, new college_entry(CurrentYearIndex, 134, "N. Car A&T", -1));
                    Colleges.Add(135, new college_entry(CurrentYearIndex, 135, "N. Carolina", -1));
                    Colleges.Add(136, new college_entry(CurrentYearIndex, 136, "N. Colorado", -1));
                    Colleges.Add(137, new college_entry(CurrentYearIndex, 137, "N. Illinois", -1));
                    Colleges.Add(138, new college_entry(CurrentYearIndex, 138, "N.C. State", -1));
                    Colleges.Add(139, new college_entry(CurrentYearIndex, 139, "Navy", 1));
                    Colleges.Add(140, new college_entry(CurrentYearIndex, 140, "NC Central", -1));
                    Colleges.Add(141, new college_entry(CurrentYearIndex, 141, "Nebr.-Omaha", -1));
                    Colleges.Add(142, new college_entry(CurrentYearIndex, 142, "Nebraska", -1));
                    Colleges.Add(143, new college_entry(CurrentYearIndex, 143, "Nevada", -1));
                    Colleges.Add(144, new college_entry(CurrentYearIndex, 144, "New Mex. St.", -1));
                    Colleges.Add(145, new college_entry(CurrentYearIndex, 145, "New Mexico", -1));
                    Colleges.Add(146, new college_entry(CurrentYearIndex, 146, "Nicholls St.", -1));
                    Colleges.Add(147, new college_entry(CurrentYearIndex, 147, "Norfolk State", -1));
                    Colleges.Add(148, new college_entry(CurrentYearIndex, 148, "North Texas", -1));
                    Colleges.Add(149, new college_entry(CurrentYearIndex, 149, "Northeastern", -1));
                    Colleges.Add(150, new college_entry(CurrentYearIndex, 150, "Northern Iowa", -1));
                    Colleges.Add(151, new college_entry(CurrentYearIndex, 151, "Northwestern", -1));
                    Colleges.Add(152, new college_entry(CurrentYearIndex, 152, "Notre Dame", -1));
                    Colleges.Add(153, new college_entry(CurrentYearIndex, 153, "NW Oklahoma St.", -1));
                    Colleges.Add(154, new college_entry(CurrentYearIndex, 154, "N'western St.", -1));
                    Colleges.Add(155, new college_entry(CurrentYearIndex, 155, "Ohio", -1));
                    Colleges.Add(156, new college_entry(CurrentYearIndex, 156, "Ohio State", -1));
                    Colleges.Add(157, new college_entry(CurrentYearIndex, 157, "Oklahoma", -1));
                    Colleges.Add(158, new college_entry(CurrentYearIndex, 158, "Oklahoma St.", -1));
                    Colleges.Add(159, new college_entry(CurrentYearIndex, 159, "Ole Miss", -1));
                    Colleges.Add(160, new college_entry(CurrentYearIndex, 160, "Oregon", -1));
                    Colleges.Add(161, new college_entry(CurrentYearIndex, 161, "Oregon State", -1));
                    Colleges.Add(162, new college_entry(CurrentYearIndex, 162, "P. View A&M", -1));
                    Colleges.Add(163, new college_entry(CurrentYearIndex, 163, "Penn", -1));
                    Colleges.Add(164, new college_entry(CurrentYearIndex, 164, "Penn State", -1));
                    Colleges.Add(165, new college_entry(CurrentYearIndex, 165, "Pittsburg St.", -1));
                    Colleges.Add(166, new college_entry(CurrentYearIndex, 166, "Pittsburgh", -1));
                    Colleges.Add(167, new college_entry(CurrentYearIndex, 167, "Portland St.", -1));
                    Colleges.Add(168, new college_entry(CurrentYearIndex, 168, "Princeton", -1));
                    Colleges.Add(169, new college_entry(CurrentYearIndex, 169, "Purdue", -1));
                    Colleges.Add(170, new college_entry(CurrentYearIndex, 170, "Rhode Island", -1));
                    Colleges.Add(171, new college_entry(CurrentYearIndex, 171, "Rice", -1));
                    Colleges.Add(172, new college_entry(CurrentYearIndex, 172, "Richmond", -1));
                    Colleges.Add(173, new college_entry(CurrentYearIndex, 173, "Robert Morris", -1));
                    Colleges.Add(174, new college_entry(CurrentYearIndex, 174, "Rowan", -1));
                    Colleges.Add(175, new college_entry(CurrentYearIndex, 175, "Rutgers", -1));
                    Colleges.Add(176, new college_entry(CurrentYearIndex, 176, "S. Carolina", -1));
                    Colleges.Add(177, new college_entry(CurrentYearIndex, 177, "S. Dakota St.", -1));
                    Colleges.Add(178, new college_entry(CurrentYearIndex, 178, "S. Illinois", -1));
                    Colleges.Add(179, new college_entry(CurrentYearIndex, 179, "S.C. State", -1));
                    Colleges.Add(180, new college_entry(CurrentYearIndex, 180, "S.D. State", -1));
                    Colleges.Add(181, new college_entry(CurrentYearIndex, 181, "S.F. Austin", -1));
                    Colleges.Add(182, new college_entry(CurrentYearIndex, 182, "Sacred Heart", -1));
                    Colleges.Add(183, new college_entry(CurrentYearIndex, 183, "Sam Houston", -1));
                    Colleges.Add(184, new college_entry(CurrentYearIndex, 184, "Samford", -1));
                    Colleges.Add(185, new college_entry(CurrentYearIndex, 185, "San Jose St.", -1));
                    Colleges.Add(186, new college_entry(CurrentYearIndex, 186, "Savannah St.", -1));
                    Colleges.Add(187, new college_entry(CurrentYearIndex, 187, "SE Missouri", -1));
                    Colleges.Add(188, new college_entry(CurrentYearIndex, 188, "SE Missouri St.", -1));
                    Colleges.Add(189, new college_entry(CurrentYearIndex, 189, "Shippensburg", -1));
                    Colleges.Add(190, new college_entry(CurrentYearIndex, 190, "Siena", -1));
                    Colleges.Add(191, new college_entry(CurrentYearIndex, 191, "Simon Fraser", -1));
                    Colleges.Add(192, new college_entry(CurrentYearIndex, 192, "SMU", -1));
                    Colleges.Add(193, new college_entry(CurrentYearIndex, 193, "Southern", -1));
                    Colleges.Add(194, new college_entry(CurrentYearIndex, 194, "Southern Miss", -1));
                    Colleges.Add(195, new college_entry(CurrentYearIndex, 195, "Southern Utah", -1));
                    Colleges.Add(196, new college_entry(CurrentYearIndex, 196, "St. Francis", -1));
                    Colleges.Add(197, new college_entry(CurrentYearIndex, 197, "St. John's", -1));
                    Colleges.Add(198, new college_entry(CurrentYearIndex, 198, "St. Mary's", -1));
                    Colleges.Add(199, new college_entry(CurrentYearIndex, 199, "St. Peters", -1));
                    Colleges.Add(200, new college_entry(CurrentYearIndex, 200, "Stanford", -1));
                    Colleges.Add(201, new college_entry(CurrentYearIndex, 201, "Stony Brook", -1));
                    Colleges.Add(202, new college_entry(CurrentYearIndex, 202, "SUNY Albany", -1));
                    Colleges.Add(203, new college_entry(CurrentYearIndex, 203, "SW Miss St", -1));
                    Colleges.Add(204, new college_entry(CurrentYearIndex, 204, "SW Texas St.", -1));
                    Colleges.Add(205, new college_entry(CurrentYearIndex, 205, "Syracuse", -1));
                    Colleges.Add(206, new college_entry(CurrentYearIndex, 206, "T A&M K'ville", -1));
                    Colleges.Add(207, new college_entry(CurrentYearIndex, 207, "TCU", -1));
                    Colleges.Add(208, new college_entry(CurrentYearIndex, 208, "Temple", -1));
                    Colleges.Add(209, new college_entry(CurrentYearIndex, 209, "Tenn. Tech", -1));
                    Colleges.Add(210, new college_entry(CurrentYearIndex, 210, "Tenn-Chat", -1));
                    Colleges.Add(211, new college_entry(CurrentYearIndex, 211, "Tennessee", -1));
                    Colleges.Add(212, new college_entry(CurrentYearIndex, 212, "Tennessee St.", -1));
                    Colleges.Add(213, new college_entry(CurrentYearIndex, 213, "Tenn-Martin", -1));
                    Colleges.Add(214, new college_entry(CurrentYearIndex, 214, "Texas", -1));
                    Colleges.Add(215, new college_entry(CurrentYearIndex, 215, "Texas A&M", -1));
                    Colleges.Add(216, new college_entry(CurrentYearIndex, 216, "Texas South.", -1));
                    Colleges.Add(217, new college_entry(CurrentYearIndex, 217, "Texas Tech", -1));
                    Colleges.Add(218, new college_entry(CurrentYearIndex, 218, "Toledo", -1));
                    Colleges.Add(219, new college_entry(CurrentYearIndex, 219, "Towson State", -1));
                    Colleges.Add(220, new college_entry(CurrentYearIndex, 220, "Troy State", -1));
                    Colleges.Add(221, new college_entry(CurrentYearIndex, 221, "Tulane", -1));
                    Colleges.Add(222, new college_entry(CurrentYearIndex, 222, "Tulsa", -1));
                    Colleges.Add(223, new college_entry(CurrentYearIndex, 223, "Tuskegee", -1));
                    Colleges.Add(224, new college_entry(CurrentYearIndex, 224, "UAB", -1));
                    Colleges.Add(225, new college_entry(CurrentYearIndex, 225, "UCF", -1));
                    Colleges.Add(226, new college_entry(CurrentYearIndex, 226, "UCLA", -1));
                    Colleges.Add(227, new college_entry(CurrentYearIndex, 227, "UConn", -1));
                    Colleges.Add(228, new college_entry(CurrentYearIndex, 228, "UL Lafayette", -1));
                    Colleges.Add(229, new college_entry(CurrentYearIndex, 229, "UL Monroe", -1));
                    Colleges.Add(230, new college_entry(CurrentYearIndex, 230, "UNLV", -1));
                    Colleges.Add(231, new college_entry(CurrentYearIndex, 231, "USC", -1));
                    Colleges.Add(232, new college_entry(CurrentYearIndex, 232, "USF", -1));
                    Colleges.Add(233, new college_entry(CurrentYearIndex, 233, "Utah", -1));
                    Colleges.Add(234, new college_entry(CurrentYearIndex, 234, "Utah State", -1));
                    Colleges.Add(235, new college_entry(CurrentYearIndex, 235, "UTEP", -1));
                    Colleges.Add(236, new college_entry(CurrentYearIndex, 236, "Valdosta St.", -1));
                    Colleges.Add(237, new college_entry(CurrentYearIndex, 237, "Valparaiso", -1));
                    Colleges.Add(238, new college_entry(CurrentYearIndex, 238, "Vanderbilt", -1));
                    Colleges.Add(239, new college_entry(CurrentYearIndex, 239, "Villanova", -1));
                    Colleges.Add(240, new college_entry(CurrentYearIndex, 240, "Virginia", -1));
                    Colleges.Add(241, new college_entry(CurrentYearIndex, 241, "Virginia Tech", -1));
                    Colleges.Add(242, new college_entry(CurrentYearIndex, 242, "VMI", -1));
                    Colleges.Add(243, new college_entry(CurrentYearIndex, 243, "W. Carolina", -1));
                    Colleges.Add(244, new college_entry(CurrentYearIndex, 244, "W. Illinois", -1));
                    Colleges.Add(245, new college_entry(CurrentYearIndex, 245, "W. Kentucky", -1));
                    Colleges.Add(246, new college_entry(CurrentYearIndex, 246, "W. Michigan", -1));
                    Colleges.Add(247, new college_entry(CurrentYearIndex, 247, "W. Texas A&M", -1));
                    Colleges.Add(248, new college_entry(CurrentYearIndex, 248, "Wagner", -1));
                    Colleges.Add(249, new college_entry(CurrentYearIndex, 249, "Wake Forest", -1));
                    Colleges.Add(250, new college_entry(CurrentYearIndex, 250, "Walla Walla", -1));
                    Colleges.Add(251, new college_entry(CurrentYearIndex, 251, "Wash. St.", -1));
                    Colleges.Add(252, new college_entry(CurrentYearIndex, 252, "Washington", -1));
                    Colleges.Add(253, new college_entry(CurrentYearIndex, 253, "Weber State", -1));
                    Colleges.Add(254, new college_entry(CurrentYearIndex, 254, "West Virginia", -1));
                    Colleges.Add(255, new college_entry(CurrentYearIndex, 255, "Westminster", -1));
                    Colleges.Add(256, new college_entry(CurrentYearIndex, 256, "Will. & Mary", -1));
                    Colleges.Add(257, new college_entry(CurrentYearIndex, 257, "Winston Salem", -1));
                    Colleges.Add(258, new college_entry(CurrentYearIndex, 258, "Wisconsin", -1));
                    Colleges.Add(259, new college_entry(CurrentYearIndex, 259, "Wofford", -1));
                    Colleges.Add(260, new college_entry(CurrentYearIndex, 260, "Wyoming", -1));
                    Colleges.Add(261, new college_entry(CurrentYearIndex, 261, "Yale", -1));
                    Colleges.Add(262, new college_entry(CurrentYearIndex, 262, "Youngstwn St.", -1));
                    Colleges.Add(263, new college_entry(CurrentYearIndex, 263, "Sonoma St.", -1));
                    Colleges.Add(264, new college_entry(CurrentYearIndex, 264, "No College", -1));
                    Colleges.Add(265, new college_entry(CurrentYearIndex, 265, "N/A", -1));
                    Colleges.Add(266, new college_entry(CurrentYearIndex, 266, "New Hampshire", -1));
                    Colleges.Add(267, new college_entry(CurrentYearIndex, 267, "UW Lacrosse", -1));
                    Colleges.Add(268, new college_entry(CurrentYearIndex, 268, "Hastings College", -1));
                    Colleges.Add(269, new college_entry(CurrentYearIndex, 269, "Midwestern St.", -1));
                    Colleges.Add(270, new college_entry(CurrentYearIndex, 270, "North Dakota", -1));
                    Colleges.Add(271, new college_entry(CurrentYearIndex, 271, "Wayne State", -1));
                    Colleges.Add(272, new college_entry(CurrentYearIndex, 272, "UW Stevens Pt.", -1));
                    Colleges.Add(273, new college_entry(CurrentYearIndex, 273, "Indiana(Penn.)", -1));
                    Colleges.Add(274, new college_entry(CurrentYearIndex, 274, "Saginaw Valley", -1));
                    Colleges.Add(275, new college_entry(CurrentYearIndex, 275, "Central St.(OK)", -1));
                    Colleges.Add(276, new college_entry(CurrentYearIndex, 276, "Emporia State", -1));
                }
                #endregion

                #region 2019
                else if (fileVersion == MaddenFileVersion.Ver2019)
                {
                    Colleges.Add(0, new college_entry(CurrentYearIndex, 0, "NA", -1));
                    Colleges.Add(1, new college_entry(CurrentYearIndex, 1, "Abilene Chr.", -1));
                    Colleges.Add(2, new college_entry(CurrentYearIndex, 2, "Air Force", -1));
                    Colleges.Add(3, new college_entry(CurrentYearIndex, 3, "Akron", -1));
                    Colleges.Add(4, new college_entry(CurrentYearIndex, 4, "Alabama", -1));
                    Colleges.Add(5, new college_entry(CurrentYearIndex, 5, "Alabama A&M", -1));
                    Colleges.Add(6, new college_entry(CurrentYearIndex, 6, "Alabama St.", -1));
                    Colleges.Add(7, new college_entry(CurrentYearIndex, 7, "Alcorn St.", -1));
                    Colleges.Add(8, new college_entry(CurrentYearIndex, 8, "Appalach. St.", -1));
                    Colleges.Add(9, new college_entry(CurrentYearIndex, 9, "Arizona", -1));

                    Colleges.Add(10, new college_entry(CurrentYearIndex, 10, "Arizona St.", -1));
                    Colleges.Add(11, new college_entry(CurrentYearIndex, 11, "Arkansas", -1));
                    Colleges.Add(12, new college_entry(CurrentYearIndex, 12, "Arkansas P.B.", -1));
                    Colleges.Add(13, new college_entry(CurrentYearIndex, 13, "Arkansas St.", -1));
                    Colleges.Add(14, new college_entry(CurrentYearIndex, 14, "Army", -1));
                    Colleges.Add(15, new college_entry(CurrentYearIndex, 15, "Auburn", -1));
                    Colleges.Add(16, new college_entry(CurrentYearIndex, 16, "Austin Peay", -1));
                    Colleges.Add(17, new college_entry(CurrentYearIndex, 17, "Ball State", -1));
                    Colleges.Add(18, new college_entry(CurrentYearIndex, 18, "Baylor", -1));
                    Colleges.Add(19, new college_entry(CurrentYearIndex, 19, "Beth Cookman", -1));


                    Colleges.Add(20, new college_entry(CurrentYearIndex, 20, "Boise State", -1));
                    Colleges.Add(21, new college_entry(CurrentYearIndex, 21, "Boston College", -1));
                    Colleges.Add(22, new college_entry(CurrentYearIndex, 22, "Bowling Green St.", -1));
                    Colleges.Add(23, new college_entry(CurrentYearIndex, 23, "Brown", -1));
                    Colleges.Add(24, new college_entry(CurrentYearIndex, 24, "Bucknell", -1));
                    Colleges.Add(25, new college_entry(CurrentYearIndex, 25, "Buffalo", -1));
                    Colleges.Add(26, new college_entry(CurrentYearIndex, 26, "Butler", -1));
                    Colleges.Add(27, new college_entry(CurrentYearIndex, 27, "BYU", -1));
                    Colleges.Add(28, new college_entry(CurrentYearIndex, 28, "Cal Poly SLO", -1));
                    Colleges.Add(29, new college_entry(CurrentYearIndex, 29, "California", -1));
                    
                    Colleges.Add(30, new college_entry(CurrentYearIndex, 30, "Cal-Nrthridge", -1));
                    Colleges.Add(31, new college_entry(CurrentYearIndex, 31, "Cal-Sacramento", -1));
                    Colleges.Add(32, new college_entry(CurrentYearIndex, 32, "Canisius", -1));
                    Colleges.Add(33, new college_entry(CurrentYearIndex, 33, "Cent Conn St.", -1));
                    Colleges.Add(34, new college_entry(CurrentYearIndex, 34, "Eastern Michigan", -1));
                    Colleges.Add(35, new college_entry(CurrentYearIndex, 35, "Central St Ohio", -1));
                    Colleges.Add(36, new college_entry(CurrentYearIndex, 36, "Charleston S.", -1));
                    Colleges.Add(37, new college_entry(CurrentYearIndex, 37, "Cincinnati", -1));
                    Colleges.Add(38, new college_entry(CurrentYearIndex, 38, "Citadel", -1));                    
                    Colleges.Add(39, new college_entry(CurrentYearIndex, 39, "Clemson", -1));

                    Colleges.Add(40, new college_entry(CurrentYearIndex, 40, "Clinch Valley", -1));
                    Colleges.Add(41, new college_entry(CurrentYearIndex, 41, "Colgate", -1));
                    Colleges.Add(42, new college_entry(CurrentYearIndex, 42, "Colorado", -1));
                    Colleges.Add(43, new college_entry(CurrentYearIndex, 43, "Colorado St.", -1));
                    Colleges.Add(44, new college_entry(CurrentYearIndex, 44, "Columbia", -1));
                    Colleges.Add(45, new college_entry(CurrentYearIndex, 45, "Cornell", -1));
                    Colleges.Add(46, new college_entry(CurrentYearIndex, 46, "Culver-Stockton", -1));
                    Colleges.Add(47, new college_entry(CurrentYearIndex, 47, "Dartmouth", -1));
                    Colleges.Add(48, new college_entry(CurrentYearIndex, 48, "Davidson", -1));
                    Colleges.Add(49, new college_entry(CurrentYearIndex, 49, "Dayton", -1));
                    
                    Colleges.Add(50, new college_entry(CurrentYearIndex, 50, "Delaware", -1));
                    Colleges.Add(51, new college_entry(CurrentYearIndex, 51, "Delaware St.", -1));
                    Colleges.Add(52, new college_entry(CurrentYearIndex, 52, "Drake", -1));
                    Colleges.Add(53, new college_entry(CurrentYearIndex, 53, "Duke", -1));
                    Colleges.Add(54, new college_entry(CurrentYearIndex, 54, "Duquesne", -1));
                    Colleges.Add(55, new college_entry(CurrentYearIndex, 55, "Mississippi State", -1));                    
                    Colleges.Add(56, new college_entry(CurrentYearIndex, 56, "E. Illinois", -1));
                    Colleges.Add(57, new college_entry(CurrentYearIndex, 57, "E. Kentucky", -1));
                    Colleges.Add(58, new college_entry(CurrentYearIndex, 58, "E. Tenn. St.", -1));                    
                    Colleges.Add(59, new college_entry(CurrentYearIndex, 59, "East Carolina", -1));                    
                    
                    Colleges.Add(60, new college_entry(CurrentYearIndex, 60, "Eastern Wash.", -1));
                    Colleges.Add(61, new college_entry(CurrentYearIndex, 61, "Elon University", -1));
                    Colleges.Add(62, new college_entry(CurrentYearIndex, 62, "Fairfield", -1));
                    Colleges.Add(63, new college_entry(CurrentYearIndex, 63, "Florida", -1));
                    Colleges.Add(64, new college_entry(CurrentYearIndex, 64, "Florida A&M", -1));
                    Colleges.Add(65, new college_entry(CurrentYearIndex, 65, "Florida State", -1));
                    Colleges.Add(66, new college_entry(CurrentYearIndex, 66, "Fordham", -1));
                    Colleges.Add(67, new college_entry(CurrentYearIndex, 67, "Fresno State", -1));
                    Colleges.Add(68, new college_entry(CurrentYearIndex, 68, "Furman", -1));
                    Colleges.Add(69, new college_entry(CurrentYearIndex, 69, "Ga. Southern", -1));

                    Colleges.Add(70, new college_entry(CurrentYearIndex, 70, "Georgetown", -1));
                    Colleges.Add(71, new college_entry(CurrentYearIndex, 71, "Georgia", -1));
                    Colleges.Add(72, new college_entry(CurrentYearIndex, 72, "Georgia Tech", -1));
                    Colleges.Add(73, new college_entry(CurrentYearIndex, 73, "Grambling St.", -1));
                    Colleges.Add(74, new college_entry(CurrentYearIndex, 74, "Grand Valley St.", -1));
                    Colleges.Add(75, new college_entry(CurrentYearIndex, 75, "Hampton", -1));
                    Colleges.Add(76, new college_entry(CurrentYearIndex, 76, "Harvard", -1));
                    Colleges.Add(77, new college_entry(CurrentYearIndex, 77, "Hawaii", -1));
                    Colleges.Add(78, new college_entry(CurrentYearIndex, 78, "Henderson St.", -1));
                    Colleges.Add(79, new college_entry(CurrentYearIndex, 79, "Hofstra", -1));

                    Colleges.Add(80, new college_entry(CurrentYearIndex, 80, "Holy Cross", -1));
                    Colleges.Add(81, new college_entry(CurrentYearIndex, 81, "Houston", -1));
                    Colleges.Add(82, new college_entry(CurrentYearIndex, 82, "Howard", -1));
                    Colleges.Add(83, new college_entry(CurrentYearIndex, 83, "Idaho", -1));
                    Colleges.Add(84, new college_entry(CurrentYearIndex, 84, "Idaho State", -1));
                    Colleges.Add(85, new college_entry(CurrentYearIndex, 85, "Illinois", -1));
                    Colleges.Add(86, new college_entry(CurrentYearIndex, 86, "Illinois St.", -1));
                    Colleges.Add(87, new college_entry(CurrentYearIndex, 87, "Indiana", -1));
                    Colleges.Add(88, new college_entry(CurrentYearIndex, 88, "Indiana St.", -1));
                    Colleges.Add(89, new college_entry(CurrentYearIndex, 89, "Iona", -1));

                    Colleges.Add(90, new college_entry(CurrentYearIndex, 90, "Iowa", -1));
                    Colleges.Add(91, new college_entry(CurrentYearIndex, 91, "Iowa State", -1));
                    Colleges.Add(92, new college_entry(CurrentYearIndex, 92, "J. Madison", -1));
                    Colleges.Add(93, new college_entry(CurrentYearIndex, 93, "Jackson St.", -1));
                    Colleges.Add(94, new college_entry(CurrentYearIndex, 94, "Jacksonv. St.", -1));
                    Colleges.Add(95, new college_entry(CurrentYearIndex, 95, "John Carroll", -1));
                    Colleges.Add(96, new college_entry(CurrentYearIndex, 96, "Kansas", -1));
                    Colleges.Add(97, new college_entry(CurrentYearIndex, 97, "Kansas State", -1));
                    Colleges.Add(98, new college_entry(CurrentYearIndex, 98, "Kent State", -1));
                    Colleges.Add(99, new college_entry(CurrentYearIndex, 99, "Kentucky", -1));

                    Colleges.Add(100, new college_entry(CurrentYearIndex, 100, "Kutztown", -1));
                    Colleges.Add(101, new college_entry(CurrentYearIndex, 101, "La Salle", -1));
                    Colleges.Add(102, new college_entry(CurrentYearIndex, 102, "LA. Tech", 1));
                    Colleges.Add(103, new college_entry(CurrentYearIndex, 103, "Lambuth", -1));
                    Colleges.Add(104, new college_entry(CurrentYearIndex, 104, "Lehigh", -1));
                    Colleges.Add(105, new college_entry(CurrentYearIndex, 105, "Liberty", -1));
                    Colleges.Add(106, new college_entry(CurrentYearIndex, 106, "Louisville", -1));
                    Colleges.Add(107, new college_entry(CurrentYearIndex, 107, "LSU", -1));
                    Colleges.Add(108, new college_entry(CurrentYearIndex, 108, "M. Valley St.", -1));
                    Colleges.Add(109, new college_entry(CurrentYearIndex, 109, "Maine", -1));

                    Colleges.Add(110, new college_entry(CurrentYearIndex, 110, "Marist", -1));
                    Colleges.Add(111, new college_entry(CurrentYearIndex, 111, "Marshall", -1));
                    Colleges.Add(112, new college_entry(CurrentYearIndex, 112, "Maryland", -1));
                    Colleges.Add(113, new college_entry(CurrentYearIndex, 113, "Massachusetts", -1));
                    Colleges.Add(114, new college_entry(CurrentYearIndex, 114, "McNeese St.", -1));
                    Colleges.Add(115, new college_entry(CurrentYearIndex, 115, "Memphis", 1));
                    Colleges.Add(116, new college_entry(CurrentYearIndex, 116, "Miami", -1));
                    Colleges.Add(117, new college_entry(CurrentYearIndex, 117, "Miami Univ.", -1));
                    Colleges.Add(118, new college_entry(CurrentYearIndex, 118, "Michigan", 1));
                    Colleges.Add(119, new college_entry(CurrentYearIndex, 119, "Michigan St.", 1));

                    Colleges.Add(120, new college_entry(CurrentYearIndex, 120, "Mid Tenn St.", -1));
                    Colleges.Add(121, new college_entry(CurrentYearIndex, 121, "Minnesota", -1));
                    Colleges.Add(122, new college_entry(CurrentYearIndex, 122, "North Carolina", -1));
                    Colleges.Add(123, new college_entry(CurrentYearIndex, 123, "Missouri", -1));
                    Colleges.Add(124, new college_entry(CurrentYearIndex, 124, "Monmouth", -1));
                    Colleges.Add(125, new college_entry(CurrentYearIndex, 125, "Montana", -1));
                    Colleges.Add(126, new college_entry(CurrentYearIndex, 126, "Montana State", -1));
                    Colleges.Add(127, new college_entry(CurrentYearIndex, 127, "Morehead St.", -1));
                    Colleges.Add(128, new college_entry(CurrentYearIndex, 128, "Morehouse", -1));
                    Colleges.Add(129, new college_entry(CurrentYearIndex, 129, "Morgan St.", -1));

                    Colleges.Add(130, new college_entry(CurrentYearIndex, 130, "Morris Brown", -1));
                    Colleges.Add(131, new college_entry(CurrentYearIndex, 131, "Mt S. Antonio", -1));
                    Colleges.Add(132, new college_entry(CurrentYearIndex, 132, "Murray State", -1));
                    Colleges.Add(133, new college_entry(CurrentYearIndex, 133, "N. Alabama", -1));
                    Colleges.Add(134, new college_entry(CurrentYearIndex, 134, "N. Arizona", -1));
                    Colleges.Add(135, new college_entry(CurrentYearIndex, 135, "N.C. A&T", -1));
                    Colleges.Add(136, new college_entry(CurrentYearIndex, 136, "South Carolina", -1));
                    Colleges.Add(137, new college_entry(CurrentYearIndex, 137, "N. Colorado", -1));
                    Colleges.Add(138, new college_entry(CurrentYearIndex, 138, "N. Illinois", -1));
                    Colleges.Add(139, new college_entry(CurrentYearIndex, 139, "N.C. State", -1));

                    Colleges.Add(140, new college_entry(CurrentYearIndex, 140, "Navy", 1));
                    Colleges.Add(141, new college_entry(CurrentYearIndex, 141, "NC Central", -1));
                    Colleges.Add(142, new college_entry(CurrentYearIndex, 142, "Nebr.-Omaha", -1));
                    Colleges.Add(143, new college_entry(CurrentYearIndex, 143, "Nebraska", -1));
                    Colleges.Add(144, new college_entry(CurrentYearIndex, 144, "Nevada", -1));
                    Colleges.Add(145, new college_entry(CurrentYearIndex, 145, "New Mex. St.", -1));
                    Colleges.Add(146, new college_entry(CurrentYearIndex, 146, "New Mexico", -1));
                    Colleges.Add(147, new college_entry(CurrentYearIndex, 147, "Nicholls St.", -1));
                    Colleges.Add(148, new college_entry(CurrentYearIndex, 148, "Norfolk State", -1));
                    Colleges.Add(149, new college_entry(CurrentYearIndex, 149, "North Texas", -1));

                    Colleges.Add(150, new college_entry(CurrentYearIndex, 150, "Northeastern", -1));
                    Colleges.Add(151, new college_entry(CurrentYearIndex, 151, "Northern Iowa", -1));
                    Colleges.Add(152, new college_entry(CurrentYearIndex, 152, "Northwestern", -1));
                    Colleges.Add(153, new college_entry(CurrentYearIndex, 153, "Notre Dame", -1));
                    Colleges.Add(154, new college_entry(CurrentYearIndex, 154, "NW Oklahoma St.", -1));
                    Colleges.Add(155, new college_entry(CurrentYearIndex, 155, "N'western St.", -1));
                    Colleges.Add(156, new college_entry(CurrentYearIndex, 156, "Ohio", -1));
                    Colleges.Add(157, new college_entry(CurrentYearIndex, 157, "Ohio State", -1));
                    Colleges.Add(158, new college_entry(CurrentYearIndex, 158, "Oklahoma", -1));
                    Colleges.Add(159, new college_entry(CurrentYearIndex, 159, "Oklahoma St.", -1));

                    Colleges.Add(160, new college_entry(CurrentYearIndex, 160, "Ole Miss", -1));
                    Colleges.Add(161, new college_entry(CurrentYearIndex, 161, "Oregon", -1));
                    Colleges.Add(162, new college_entry(CurrentYearIndex, 162, "Oregon State", -1));
                    Colleges.Add(163, new college_entry(CurrentYearIndex, 163, "P. View A&M", -1));
                    Colleges.Add(164, new college_entry(CurrentYearIndex, 164, "Penn", -1));
                    Colleges.Add(165, new college_entry(CurrentYearIndex, 165, "Penn State", -1));
                    Colleges.Add(166, new college_entry(CurrentYearIndex, 166, "Pittsburg St.", -1));
                    Colleges.Add(167, new college_entry(CurrentYearIndex, 167, "Pittsburgh", -1));
                    Colleges.Add(168, new college_entry(CurrentYearIndex, 168, "Portland St.", -1));
                    Colleges.Add(169, new college_entry(CurrentYearIndex, 169, "Princeton", -1));

                    Colleges.Add(170, new college_entry(CurrentYearIndex, 170, "Purdue", -1));
                    Colleges.Add(171, new college_entry(CurrentYearIndex, 171, "Rhode Island", -1));
                    Colleges.Add(172, new college_entry(CurrentYearIndex, 172, "Rice", -1));
                    Colleges.Add(173, new college_entry(CurrentYearIndex, 173, "Richmond", -1));
                    Colleges.Add(174, new college_entry(CurrentYearIndex, 174, "Robert Morris", -1));
                    Colleges.Add(175, new college_entry(CurrentYearIndex, 175, "Rowan", -1));
                    Colleges.Add(176, new college_entry(CurrentYearIndex, 176, "Rutgers", -1));
                    Colleges.Add(177, new college_entry(CurrentYearIndex, 177, "Augustana", -1));
                    Colleges.Add(178, new college_entry(CurrentYearIndex, 178, "S. Dakota St.", -1));
                    Colleges.Add(179, new college_entry(CurrentYearIndex, 179, "S. Illinois", -1));

                    Colleges.Add(180, new college_entry(CurrentYearIndex, 180, "S.C. State", -1));
                    Colleges.Add(181, new college_entry(CurrentYearIndex, 181, "San Diego State", -1));
                    Colleges.Add(182, new college_entry(CurrentYearIndex, 182, "Wagner College", -1));
                    Colleges.Add(183, new college_entry(CurrentYearIndex, 183, "Sacred Heart", -1));
                    Colleges.Add(184, new college_entry(CurrentYearIndex, 184, "Sam Houston", -1));
                    Colleges.Add(185, new college_entry(CurrentYearIndex, 185, "Samford", -1));
                    Colleges.Add(186, new college_entry(CurrentYearIndex, 186, "San Jose St.", -1));
                    Colleges.Add(187, new college_entry(CurrentYearIndex, 187, "Savannah St.", -1));
                    Colleges.Add(188, new college_entry(CurrentYearIndex, 188, "SE Missouri", -1));
                    Colleges.Add(189, new college_entry(CurrentYearIndex, 189, "SE Missouri St.", -1));

                    Colleges.Add(190, new college_entry(CurrentYearIndex, 190, "Shippensburg", -1));
                    Colleges.Add(191, new college_entry(CurrentYearIndex, 191, "Siena", -1));
                    Colleges.Add(192, new college_entry(CurrentYearIndex, 192, "Simon Fraser", -1));
                    Colleges.Add(193, new college_entry(CurrentYearIndex, 193, "SMU", -1));
                    Colleges.Add(194, new college_entry(CurrentYearIndex, 194, "Southern", -1));
                    Colleges.Add(195, new college_entry(CurrentYearIndex, 195, "Southern Miss", -1));
                    Colleges.Add(196, new college_entry(CurrentYearIndex, 196, "Southern Utah", -1));
                    Colleges.Add(197, new college_entry(CurrentYearIndex, 197, "St. Francis", -1));
                    Colleges.Add(198, new college_entry(CurrentYearIndex, 198, "St. John's", -1));
                    Colleges.Add(199, new college_entry(CurrentYearIndex, 199, "St. Mary's", -1));

                    Colleges.Add(200, new college_entry(CurrentYearIndex, 200, "St. Peters", -1));
                    Colleges.Add(201, new college_entry(CurrentYearIndex, 201, "Stanford", -1));
                    Colleges.Add(202, new college_entry(CurrentYearIndex, 202, "Stony Brook", -1));
                    Colleges.Add(203, new college_entry(CurrentYearIndex, 203, "Merrimack", -1));
                    Colleges.Add(204, new college_entry(CurrentYearIndex, 204, "SW Miss St", -1));
                    Colleges.Add(205, new college_entry(CurrentYearIndex, 205, "Southern Arkansas", -1));                    
                    Colleges.Add(206, new college_entry(CurrentYearIndex, 206, "Syracuse", -1));
                    Colleges.Add(207, new college_entry(CurrentYearIndex, 207, "T A&M K'ville", -1));
                    Colleges.Add(208, new college_entry(CurrentYearIndex, 208, "TCU", -1));
                    Colleges.Add(209, new college_entry(CurrentYearIndex, 209, "Temple", -1));

                    Colleges.Add(210, new college_entry(CurrentYearIndex, 210, "Tenn. Tech", -1));
                    Colleges.Add(211, new college_entry(CurrentYearIndex, 211, "Tenn-Chat", -1));
                    Colleges.Add(212, new college_entry(CurrentYearIndex, 212, "Tennessee", -1));
                    Colleges.Add(213, new college_entry(CurrentYearIndex, 213, "Tennessee St.", -1));
                    Colleges.Add(214, new college_entry(CurrentYearIndex, 214, "Tenn-Martin", -1));
                    Colleges.Add(215, new college_entry(CurrentYearIndex, 215, "Texas", -1));
                    Colleges.Add(216, new college_entry(CurrentYearIndex, 216, "Texas A&M", -1));
                    Colleges.Add(217, new college_entry(CurrentYearIndex, 217, "Texas South.", -1));
                    Colleges.Add(218, new college_entry(CurrentYearIndex, 218, "Texas Tech", -1));
                    Colleges.Add(219, new college_entry(CurrentYearIndex, 219, "Toledo", -1));

                    Colleges.Add(220, new college_entry(CurrentYearIndex, 220, "Towson State", -1));
                    Colleges.Add(221, new college_entry(CurrentYearIndex, 221, "Troy", -1));
                    Colleges.Add(222, new college_entry(CurrentYearIndex, 222, "Tulane", -1));
                    Colleges.Add(223, new college_entry(CurrentYearIndex, 223, "Tulsa", -1));
                    Colleges.Add(224, new college_entry(CurrentYearIndex, 224, "Tuskegee", -1));
                    Colleges.Add(225, new college_entry(CurrentYearIndex, 225, "UAB", -1));
                    Colleges.Add(226, new college_entry(CurrentYearIndex, 226, "UCF", -1));
                    Colleges.Add(227, new college_entry(CurrentYearIndex, 227, "UCLA", -1));
                    Colleges.Add(228, new college_entry(CurrentYearIndex, 228, "Connecticut", -1));
                    Colleges.Add(229, new college_entry(CurrentYearIndex, 229, "UL Lafayette", -1));

                    Colleges.Add(230, new college_entry(CurrentYearIndex, 230, "UL Monroe", -1));
                    Colleges.Add(231, new college_entry(CurrentYearIndex, 231, "UNLV", -1));
                    Colleges.Add(232, new college_entry(CurrentYearIndex, 232, "USC", -1));
                    Colleges.Add(233, new college_entry(CurrentYearIndex, 233, "USF", -1));
                    Colleges.Add(234, new college_entry(CurrentYearIndex, 234, "Utah", -1));
                    Colleges.Add(235, new college_entry(CurrentYearIndex, 235, "Utah State", -1));
                    Colleges.Add(236, new college_entry(CurrentYearIndex, 236, "UTEP", -1));
                    Colleges.Add(237, new college_entry(CurrentYearIndex, 237, "Valdosta St.", -1));
                    Colleges.Add(238, new college_entry(CurrentYearIndex, 238, "Valparaiso", -1));
                    Colleges.Add(239, new college_entry(CurrentYearIndex, 239, "Vanderbilt", -1));

                    Colleges.Add(240, new college_entry(CurrentYearIndex, 240, "Villanova", -1));
                    Colleges.Add(241, new college_entry(CurrentYearIndex, 241, "Virginia", -1));
                    Colleges.Add(242, new college_entry(CurrentYearIndex, 242, "Virginia Tech", -1));
                    Colleges.Add(243, new college_entry(CurrentYearIndex, 243, "VMI", -1));
                    Colleges.Add(244, new college_entry(CurrentYearIndex, 244, "W. Carolina", -1));
                    Colleges.Add(245, new college_entry(CurrentYearIndex, 245, "W. Illinois", -1));
                    Colleges.Add(246, new college_entry(CurrentYearIndex, 246, "W. Kentucky", -1));
                    Colleges.Add(247, new college_entry(CurrentYearIndex, 247, "W. Michigan", -1));
                    Colleges.Add(248, new college_entry(CurrentYearIndex, 248, "W. Texas A&M", -1));
                    Colleges.Add(249, new college_entry(CurrentYearIndex, 249, "Union College", -1));

                    Colleges.Add(250, new college_entry(CurrentYearIndex, 250, "Wake Forest", -1));
                    Colleges.Add(251, new college_entry(CurrentYearIndex, 251, "Walla Walla", -1));
                    Colleges.Add(252, new college_entry(CurrentYearIndex, 252, "Wash. St.", -1));
                    Colleges.Add(253, new college_entry(CurrentYearIndex, 253, "Washington", -1));
                    Colleges.Add(254, new college_entry(CurrentYearIndex, 254, "Weber State", -1));
                    Colleges.Add(255, new college_entry(CurrentYearIndex, 255, "West Virginia", -1));
                    Colleges.Add(256, new college_entry(CurrentYearIndex, 256, "Westminster", -1));
                    Colleges.Add(257, new college_entry(CurrentYearIndex, 257, "William & Mary", -1));
                    Colleges.Add(258, new college_entry(CurrentYearIndex, 258, "Winston Salem", -1));
                    Colleges.Add(259, new college_entry(CurrentYearIndex, 259, "Wisconsin", -1));

                    Colleges.Add(260, new college_entry(CurrentYearIndex, 260, "Wofford", -1));
                    Colleges.Add(261, new college_entry(CurrentYearIndex, 261, "Wyoming", -1));
                    Colleges.Add(262, new college_entry(CurrentYearIndex, 262, "Yale", -1));
                    Colleges.Add(263, new college_entry(CurrentYearIndex, 263, "Youngstown St.", -1));
                    Colleges.Add(264, new college_entry(CurrentYearIndex, 264, "Sonoma St.", -1));
                    Colleges.Add(265, new college_entry(CurrentYearIndex, 265, "No College", -1));
                    Colleges.Add(266, new college_entry(CurrentYearIndex, 266, "New Hampshire", -1));
                    Colleges.Add(267, new college_entry(CurrentYearIndex, 267, "UW Lacrosse", -1));
                    Colleges.Add(268, new college_entry(CurrentYearIndex, 268, "Hastings College", -1));
                    Colleges.Add(269, new college_entry(CurrentYearIndex, 269, "Midwestern St.", -1));

                    Colleges.Add(270, new college_entry(CurrentYearIndex, 270, "North Dakota", -1));
                    Colleges.Add(271, new college_entry(CurrentYearIndex, 271, "Wayne State", -1));
                    Colleges.Add(272, new college_entry(CurrentYearIndex, 272, "UW Stevens Pt.", -1));
                    Colleges.Add(273, new college_entry(CurrentYearIndex, 273, "IUP", -1));
                    Colleges.Add(274, new college_entry(CurrentYearIndex, 274, "Saginaw Valley", -1));
                    Colleges.Add(275, new college_entry(CurrentYearIndex, 275, "Franklin College", -1));
                    Colleges.Add(276, new college_entry(CurrentYearIndex, 276, "Emporia State", -1));
                    Colleges.Add(277, new college_entry(CurrentYearIndex, 277, "Wingate", -1));
                    Colleges.Add(278, new college_entry(CurrentYearIndex, 278, "Wheaton", -1));
                    Colleges.Add(279, new college_entry(CurrentYearIndex, 279, "W. New Mexico", -1));



                    Colleges.Add(280, new college_entry(CurrentYearIndex, 280, "Albany", -1));
                    Colleges.Add(281, new college_entry(CurrentYearIndex, 281, "Presbyterian", -1));
                    Colleges.Add(282, new college_entry(CurrentYearIndex, 282, "Bloomsburg", -1));
                    Colleges.Add(283, new college_entry(CurrentYearIndex, 283, "Central Michigan", -1));
                    Colleges.Add(284, new college_entry(CurrentYearIndex, 284, "Whitworth", -1));
                    Colleges.Add(285, new college_entry(CurrentYearIndex, 285, "Buffalo State", -1));
                    Colleges.Add(286, new college_entry(CurrentYearIndex, 286, "California-Davis", -1));
                    Colleges.Add(287, new college_entry(CurrentYearIndex, 287, "Carson-Newman", -1));
                    Colleges.Add(288, new college_entry(CurrentYearIndex, 288, "Central Arkansas", -1));
                    Colleges.Add(289, new college_entry(CurrentYearIndex, 289, "Chattanooga", -1));

                    Colleges.Add(290, new college_entry(CurrentYearIndex, 290, "Coastal Carolina", -1));
                    Colleges.Add(291, new college_entry(CurrentYearIndex, 291, "Tusculum College", -1));
                    Colleges.Add(292, new college_entry(CurrentYearIndex, 292, "East Stroudsburg", -1));
                    Colleges.Add(293, new college_entry(CurrentYearIndex, 293, "Cal-Bakersfield", -1));
                    Colleges.Add(294, new college_entry(CurrentYearIndex, 294, "Central Wash.", -1));
                    Colleges.Add(295, new college_entry(CurrentYearIndex, 295, "Bentley College", -1));
                    Colleges.Add(296, new college_entry(CurrentYearIndex, 296, "Ferris St.", -1));
                    Colleges.Add(297, new college_entry(CurrentYearIndex, 297, "FIU", -1));
                    Colleges.Add(298, new college_entry(CurrentYearIndex, 298, "Delta State", -1));
                    Colleges.Add(299, new college_entry(CurrentYearIndex, 299, "Fort Valley St.", -1));

                    Colleges.Add(300, new college_entry(CurrentYearIndex, 300, "Gardner-Webb", -1));
                    Colleges.Add(301, new college_entry(CurrentYearIndex, 301, "Harding", -1));
                    Colleges.Add(302, new college_entry(CurrentYearIndex, 302, "Lafayette", -1));
                    Colleges.Add(303, new college_entry(CurrentYearIndex, 303, "Lane", -1));
                    Colleges.Add(304, new college_entry(CurrentYearIndex, 304, "Carroll Coll.", -1));
                    Colleges.Add(305, new college_entry(CurrentYearIndex, 305, "St.Cloud St.", -1));
                    Colleges.Add(306, new college_entry(CurrentYearIndex, 306, "Mesa State", -1));
                    Colleges.Add(307, new college_entry(CurrentYearIndex, 307, "California (PA)", -1));
                    Colleges.Add(308, new college_entry(CurrentYearIndex, 308, "FAU", -1));
                    Colleges.Add(309, new college_entry(CurrentYearIndex, 309, "Missouri So. State", -1));

                    Colleges.Add(310, new college_entry(CurrentYearIndex, 310, "Missouri State", -1));
                    Colleges.Add(311, new college_entry(CurrentYearIndex, 311, "Missouri W. State", -1)); 
                    Colleges.Add(312, new college_entry(CurrentYearIndex, 312, "Mount Union", -1));
                    Colleges.Add(313, new college_entry(CurrentYearIndex, 313, "Nebraska-Kearney", -1));
                    Colleges.Add(314, new college_entry(CurrentYearIndex, 314, "None", -1));
                    Colleges.Add(315, new college_entry(CurrentYearIndex, 315, "North Dakota St.", -1));
                    Colleges.Add(316, new college_entry(CurrentYearIndex, 316, "Northern State", -1));
                    Colleges.Add(317, new college_entry(CurrentYearIndex, 317, "NW Missouri State", -1));
                    Colleges.Add(318, new college_entry(CurrentYearIndex, 318, "Northwood (MI)", -1));
                    Colleges.Add(319, new college_entry(CurrentYearIndex, 319, "Ohio Northern", -1));

                    Colleges.Add(320, new college_entry(CurrentYearIndex, 320, "Ottawa", -1));
                    Colleges.Add(321, new college_entry(CurrentYearIndex, 321, "Pikeville College", -1));
                    Colleges.Add(322, new college_entry(CurrentYearIndex, 322, "Ramapo", -1));
                    Colleges.Add(323, new college_entry(CurrentYearIndex, 323, "Regina", -1));
                    Colleges.Add(324, new college_entry(CurrentYearIndex, 324, "Lindenwood", -1));
                    Colleges.Add(325, new college_entry(CurrentYearIndex, 325, "Sacramento State", -1));
                    Colleges.Add(326, new college_entry(CurrentYearIndex, 326, "Calgary", -1));
                    Colleges.Add(327, new college_entry(CurrentYearIndex, 327, "San Diego", -1));
                    Colleges.Add(328, new college_entry(CurrentYearIndex, 328, "South Dakota", -1));
                    Colleges.Add(329, new college_entry(CurrentYearIndex, 329, "Coe College", -1));

                    Colleges.Add(330, new college_entry(CurrentYearIndex, 330, "SE Louisiana", -1));
                    Colleges.Add(331, new college_entry(CurrentYearIndex, 331, "Stillman", -1));
                    Colleges.Add(332, new college_entry(CurrentYearIndex, 332, "Texas State", -1));
                    Colleges.Add(333, new college_entry(CurrentYearIndex, 333, "St. Augustine", -1));
                    Colleges.Add(334, new college_entry(CurrentYearIndex, 334, "Tarleton State", -1));
                    Colleges.Add(335, new college_entry(CurrentYearIndex, 335, "Truman State", -1));
                    Colleges.Add(336, new college_entry(CurrentYearIndex, 336, "Bridgewater St.", -1));
                    Colleges.Add(337, new college_entry(CurrentYearIndex, 337, "Virginia Union", -1));
                    Colleges.Add(338, new college_entry(CurrentYearIndex, 338, "Washburn", -1));
                    Colleges.Add(339, new college_entry(CurrentYearIndex, 339, "Western Wash.", -1));

                    Colleges.Add(340, new college_entry(CurrentYearIndex, 340, "St. Paul's", -1));
                    Colleges.Add(341, new college_entry(CurrentYearIndex, 341, "William Penn", -1));
                    Colleges.Add(342, new college_entry(CurrentYearIndex, 342, "West Georgia", -1));
                    Colleges.Add(343, new college_entry(CurrentYearIndex, 343, "Wisc-Whitewater", -1));
                    Colleges.Add(344, new college_entry(CurrentYearIndex, 344, "Eastern Oregon", -1));
                    Colleges.Add(345, new college_entry(CurrentYearIndex, 345, "Knoxville College", -1));
                    Colleges.Add(346, new college_entry(CurrentYearIndex, 346, "Western Ontario", -1));
                    Colleges.Add(347, new college_entry(CurrentYearIndex, 347, "S. Connecticut St.", -1));
                    Colleges.Add(348, new college_entry(CurrentYearIndex, 348, "Manitoba", -1));
                    Colleges.Add(349, new college_entry(CurrentYearIndex, 349, "Clarion", -1));

                    Colleges.Add(350, new college_entry(CurrentYearIndex, 350, "Western Oregon", -1));
                    Colleges.Add(351, new college_entry(CurrentYearIndex, 351, "Tiffin", -1));
                    Colleges.Add(352, new college_entry(CurrentYearIndex, 352, "Trinity", -1));
                    Colleges.Add(353, new college_entry(CurrentYearIndex, 353, "Stephen F. Austin", -1));
                    Colleges.Add(354, new college_entry(CurrentYearIndex, 354, "Hillsdale", -1));
                    Colleges.Add(355, new college_entry(CurrentYearIndex, 355, "UTSA", -1));
                    Colleges.Add(356, new college_entry(CurrentYearIndex, 356, "Belhaven", -1));
                    Colleges.Add(357, new college_entry(CurrentYearIndex, 357, "Salisbury", -1));
                    Colleges.Add(358, new college_entry(CurrentYearIndex, 358, "Albion College", -1));
                    Colleges.Add(359, new college_entry(CurrentYearIndex, 359, "Newberry College", -1));

                    Colleges.Add(360, new college_entry(CurrentYearIndex, 360, "Ashland", -1));
                    Colleges.Add(361, new college_entry(CurrentYearIndex, 361, "Central Oklahoma", -1));
                    Colleges.Add(362, new college_entry(CurrentYearIndex, 362, "Georgia State", -1));
                    Colleges.Add(363, new college_entry(CurrentYearIndex, 363, "Chadron State", -1));
                    Colleges.Add(364, new college_entry(CurrentYearIndex, 364, "Bowie State", -1));
                    Colleges.Add(365, new college_entry(CurrentYearIndex, 365, "Michigan Tech", -1));
                    Colleges.Add(366, new college_entry(CurrentYearIndex, 366, "Slippery Rock", -1));
                    Colleges.Add(367, new college_entry(CurrentYearIndex, 367, "Bethel", -1));
                    Colleges.Add(368, new college_entry(CurrentYearIndex, 368, "Concordia", -1));
                    Colleges.Add(369, new college_entry(CurrentYearIndex, 369, "Queen's Univ.", -1));

                    Colleges.Add(370, new college_entry(CurrentYearIndex, 370, "Ouachita Baptist", -1));
                    Colleges.Add(371, new college_entry(CurrentYearIndex, 371, "Baker", -1));
                    Colleges.Add(372, new college_entry(CurrentYearIndex, 372, "Beloit College", -1));
                    Colleges.Add(373, new college_entry(CurrentYearIndex, 373, "Fort Hays State", -1));
                    Colleges.Add(374, new college_entry(CurrentYearIndex, 374, "Walsh", -1));
                    Colleges.Add(375, new college_entry(CurrentYearIndex, 375, "Huntingdon", -1));
                    Colleges.Add(376, new college_entry(CurrentYearIndex, 376, "Scottsbluff JC", -1));
                    Colleges.Add(377, new college_entry(CurrentYearIndex, 377, "Humboldt State", -1));
                    Colleges.Add(378, new college_entry(CurrentYearIndex, 378, "CSU-Pueblo", -1));
                    Colleges.Add(379, new college_entry(CurrentYearIndex, 379, "South Alabama", -1));

                    Colleges.Add(380, new college_entry(CurrentYearIndex, 380, "Old Dominion", -1));
                    Colleges.Add(381, new college_entry(CurrentYearIndex, 381, "Mary Hardin-Baylor", -1));
                    Colleges.Add(382, new college_entry(CurrentYearIndex, 382, "West Alabama", -1));
                    Colleges.Add(383, new college_entry(CurrentYearIndex, 383, "Catawba College", -1));
                    Colleges.Add(384, new college_entry(CurrentYearIndex, 384, "East Central Univ.", -1));
                    Colleges.Add(385, new college_entry(CurrentYearIndex, 385, "Central Missouri", -1));
                    Colleges.Add(386, new college_entry(CurrentYearIndex, 386, "North Greenville", -1));
                    Colleges.Add(387, new college_entry(CurrentYearIndex, 387, "Palomar College", -1));
                    Colleges.Add(388, new college_entry(CurrentYearIndex, 388, "Heidelberg", -1));
                    Colleges.Add(389, new college_entry(CurrentYearIndex, 389, "Montreal Univ", -1));

                    Colleges.Add(390, new college_entry(CurrentYearIndex, 390, "Minnesota St.", -1));
                    Colleges.Add(391, new college_entry(CurrentYearIndex, 391, "Assumption", -1));
                    Colleges.Add(392, new college_entry(CurrentYearIndex, 392, "Centre College", -1));
                    Colleges.Add(393, new college_entry(CurrentYearIndex, 393, "UW-Milwaukee", -1));
                    Colleges.Add(394, new college_entry(CurrentYearIndex, 394, "Bemidji State", -1));
                    Colleges.Add(395, new college_entry(CurrentYearIndex, 395, "Shepherd Univ.", -1));
                    Colleges.Add(396, new college_entry(CurrentYearIndex, 396, "Northeastern St", -1));
                    Colleges.Add(397, new college_entry(CurrentYearIndex, 397, "UC Irvine", -1));
                    Colleges.Add(398, new college_entry(CurrentYearIndex, 398, "McGill Univ", -1));
                    Colleges.Add(399, new college_entry(CurrentYearIndex, 399, "Hobart", -1));

                    Colleges.Add(400, new college_entry(CurrentYearIndex, 400, "Texas A&M-Commerce", -1));
                    Colleges.Add(401, new college_entry(CurrentYearIndex, 401, "Mars Hill", -1));
                    Colleges.Add(402, new college_entry(CurrentYearIndex, 402, "Louisiana College", -1));
                    Colleges.Add(403, new college_entry(CurrentYearIndex, 403, "Lamar Univ.", -1));
                    Colleges.Add(404, new college_entry(CurrentYearIndex, 404, "SW Oklahoma State", -1));
                    Colleges.Add(405, new college_entry(CurrentYearIndex, 405, "Florida Tech", -1));
                    Colleges.Add(406, new college_entry(CurrentYearIndex, 406, "Fayetteville State", -1));
                    Colleges.Add(407, new college_entry(CurrentYearIndex, 407, "Jacksonville Univ.", -1));
                    Colleges.Add(408, new college_entry(CurrentYearIndex, 408, "Wesley College", -1));
                    Colleges.Add(409, new college_entry(CurrentYearIndex, 409, "Wisconsin-Eau Claire", -1));

                    Colleges.Add(410, new college_entry(CurrentYearIndex, 410, "Campbell Univ.", -1));
                    Colleges.Add(411, new college_entry(CurrentYearIndex, 411, "Northern Michigan", -1));
                    Colleges.Add(412, new college_entry(CurrentYearIndex, 412, "Wisconsin-Oshkosh", -1));
                    Colleges.Add(413, new college_entry(CurrentYearIndex, 413, "Pretoria", -1));
                    Colleges.Add(414, new college_entry(CurrentYearIndex, 414, "Cumberlands", -1));
                    Colleges.Add(415, new college_entry(CurrentYearIndex, 415, "Rensselaer Poly", -1));
                    Colleges.Add(416, new college_entry(CurrentYearIndex, 416, "West Chester", -1));
                    Colleges.Add(417, new college_entry(CurrentYearIndex, 417, "Morningside College", -1));
                    Colleges.Add(418, new college_entry(CurrentYearIndex, 418, "Findlay", -1));
                    Colleges.Add(419, new college_entry(CurrentYearIndex, 419, "Incarnate Word", -1));

                    Colleges.Add(420, new college_entry(CurrentYearIndex, 420, "Northwestern State", -1));
                    Colleges.Add(421, new college_entry(CurrentYearIndex, 421, "Maryville College", -1));
                    Colleges.Add(422, new college_entry(CurrentYearIndex, 422, "Cal Lutheran", -1));
                    Colleges.Add(423, new college_entry(CurrentYearIndex, 423, "Charlotte", -1));
                    Colleges.Add(424, new college_entry(CurrentYearIndex, 424, "Marian", -1));
                    //Colleges.Add(425, new college_entry(CurrentYearIndex, 425, "Northwestern St", -1));
                    Colleges.Add(426, new college_entry(CurrentYearIndex, 426, "Southern Oregon", -1));
                    Colleges.Add(427, new college_entry(CurrentYearIndex, 427, "Faulkner", -1));
                    Colleges.Add(428, new college_entry(CurrentYearIndex, 428, "Globe Tech NY", -1));
                    Colleges.Add(429, new college_entry(CurrentYearIndex, 429, "Greenville College", -1));

                    Colleges.Add(430, new college_entry(CurrentYearIndex, 430, "Lake Erie College", -1));
                    Colleges.Add(431, new college_entry(CurrentYearIndex, 431, "Laval", -1));
                    Colleges.Add(432, new college_entry(CurrentYearIndex, 432, "Mississippi College", -1)); 
                    Colleges.Add(433, new college_entry(CurrentYearIndex, 433, "Seattle", -1));
                    Colleges.Add(434, new college_entry(CurrentYearIndex, 434, "Stetson", -1));                    
                    Colleges.Add(435, new college_entry(CurrentYearIndex, 435, "Texas-Permian Basin", -1));
                    Colleges.Add(436, new college_entry(CurrentYearIndex, 436, "Virginia Commonwealth", -1));
                    Colleges.Add(437, new college_entry(CurrentYearIndex, 437, "Virginia-Lynchburg", -1));
                    Colleges.Add(438, new college_entry(CurrentYearIndex, 438, "Kentucky Wesleyan", -1));
                    Colleges.Add(439, new college_entry(CurrentYearIndex, 439, "Azusa Pacific", -1));

                    Colleges.Add(440, new college_entry(CurrentYearIndex, 440, "Western State Colorado", -1));
                    Colleges.Add(441, new college_entry(CurrentYearIndex, 441, "Dubuque", -1));
                    Colleges.Add(442, new college_entry(CurrentYearIndex, 442, "Virginia State", -1));
                    Colleges.Add(443, new college_entry(CurrentYearIndex, 443, "Notre Dame College", -1));
                    Colleges.Add(444, new college_entry(CurrentYearIndex, 444, "Fairmont State", -1));
                    Colleges.Add(445, new college_entry(CurrentYearIndex, 445, "UBC", -1));
                    Colleges.Add(446, new college_entry(CurrentYearIndex, 446, "Frostburg State", -1));
                    Colleges.Add(447, new college_entry(CurrentYearIndex, 447, "Sioux Falls", -1));
                    Colleges.Add(448, new college_entry(CurrentYearIndex, 448, "East Texas Baptist", -1));
                    Colleges.Add(449, new college_entry(CurrentYearIndex, 449, "Southern Nazarene", -1));
                }
                
                
                #endregion
                return;
            }

            List<CollegesRecord> deleteme = new List<CollegesRecord>();

            foreach (CollegesRecord record in man.stream_model.TableModels[EditorModel.COLLEGES_TABLE].GetRecords())
            {
                int check = -1;
                if (fileVersion >= MaddenFileVersion.Ver2006)
                    check = record.College_pxpc;
                foreach (KeyValuePair<int, college_entry> pair in Colleges)
                {
                    if (pair.Value.name == record.CollegeName || pair.Key == record.CollegeId)                    
                        deleteme.Add(record);                    
                }
                if (!deleteme.Contains(record))
                    Colleges.Add(record.CollegeId, new college_entry(CurrentYearIndex, record.CollegeTeamId, record.CollegeName, check));
            }

            for (int c = 0; c < deleteme.Count; c++)
                deleteme[c].SetDeleteFlag(true);
        }

        public void InitCap()
        {            
            LeagueCap.Add(2003, 75);
            LeagueCap.Add(2004, 80.58);
            LeagueCap.Add(2005, 85.5);
            LeagueCap.Add(2006, 102);
            LeagueCap.Add(2007, 109);
            LeagueCap.Add(2008, 116);
            LeagueCap.Add(2009, 123);
            LeagueCap.Add(2010, 123);       // uncapped year
            LeagueCap.Add(2011, 120);
            LeagueCap.Add(2012, 120.6);
            LeagueCap.Add(2013, 123);
            LeagueCap.Add(2014, 133);
            LeagueCap.Add(2015, 143.28);
            LeagueCap.Add(2016, 155.27);
            LeagueCap.Add(2017, 167.00);
            LeagueCap.Add(2018, 177.20); 
        }
            
        public bool IsDraftStage()
        {
            bool draft = false;
            if (this.FileType == MaddenFileType.Franchise)
            {
                if (this.FileVersion == MaddenFileVersion.Ver2004 && this.FranchiseStage.CurrentStage == 19)
                    draft = true;
                else if (this.FileVersion == MaddenFileVersion.Ver2005 && this.FranchiseStage.CurrentStage == 20)
                    draft = true;
                else if (this.FileVersion == MaddenFileVersion.Ver2006 && this.FranchiseStage.CurrentStage == 20)
                    draft = true;
                else if (this.FileVersion == MaddenFileVersion.Ver2007 && this.FranchiseStage.CurrentStage == 23)
                    draft = true;
                else if (this.FileVersion == MaddenFileVersion.Ver2008 && this.FranchiseStage.CurrentStage == 22)
                    draft = true;
            }

            return draft;
        }

        public string ConvertBE(string name)
        {
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            string rev = new string(charArray);
            return rev;
        }

        public void InitDefinitions()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\DEFINE.csv";
            TableDefs.Clear();

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);

                    // loop here until end of file
                    while (!sr.EndOfStream)
                    {
                        string csvtableline = sr.ReadLine();
                        string[] csvtableinfo = csvtableline.Split(',');

                        string tablename = csvtableinfo[0];
                        MaddenFileVersion ver = MaddenFileVersion.Ver2008;
                        int version = Convert.ToInt32(csvtableinfo[1]);
                        switch (version)
                        {
                            case 2004:
                                ver = MaddenFileVersion.Ver2004;
                                break;
                            case 2005:
                                ver = MaddenFileVersion.Ver2005;
                                break;
                            case 2006:
                                ver = MaddenFileVersion.Ver2006;
                                break;
                            case 2007:
                                ver = MaddenFileVersion.Ver2007;
                                break;
                            case 2019:
                                ver = MaddenFileVersion.Ver2019;
                                break;
                            default:
                                ver = MaddenFileVersion.Ver2008;
                                break;
                        }

                        bool hasdesc = false;
                        if (csvtableinfo[2].ToUpper().Contains("Y"))
                            hasdesc = true;
                        int conversions = 0;
                        if (csvtableinfo[3].ToUpper().Contains("Y"))
                        {
                            conversions = Convert.ToInt32(csvtableinfo[4]);
                        }

                        string csvfieldline = sr.ReadLine();
                        string[] csvfields = csvfieldline.Split(',');

                        string[] csvdesc = null;
                        if (hasdesc)
                        {
                            string csvdescline = sr.ReadLine();
                            csvdesc = csvdescline.Split(',');
                        }

                        Dictionary<string, string> csvdefs = new Dictionary<string, string>();
                        for (int c = 0; c < csvfields.Length; c++)
                        {
                            string fieldname = csvfields[c];
                            if (fieldname == "")
                                continue;

                            string fielddesc = "";
                            if (csvdesc != null)
                            {
                                if (csvdesc.Length > c)
                                    fielddesc = csvdesc[c];
                            }
                            csvdefs.Add(fieldname, fielddesc);
                        }

                        tabledefs table = new tabledefs();
                        table.FieldDefs = csvdefs;

                        Dictionary<int, string> con = new Dictionary<int, string>();
                        for (int z = 0; z < conversions; z++)
                        {
                            string conversion = sr.ReadLine();
                            string[] csvconv = conversion.Split(',');

                            con.Add(Convert.ToInt32(csvconv[1]), csvconv[2]);

                            // Add fieldname and the conversion from other versions, 0 equals all legacy 04-08
                            table.Equivs.Add(csvconv[0], con);
                        }

                        Dictionary<string, tabledefs> currenttable = new Dictionary<string, tabledefs>();
                        if (TableDefs.ContainsKey(ver))
                            currenttable = TableDefs[ver];                        
                        currenttable.Add(tablename, table);

                        if (!TableDefs.ContainsKey(ver))
                            TableDefs.Add(ver, currenttable);
                        else TableDefs[ver] = currenttable;
                    }

                    sr.Close();
                }


                catch (IOException err)
                {
                    err = err;
                }

               
            }
        }

        #region Madden Draft Edit

		public string GetFileName()
        {
            return fileName;
        }

        public void SetFranchiseState(int state)
        {
            DraftState.AtDraftStage = false;
            RFAState.AtRFAStage = false;
            ResignPlayersState.AtResignPlayersStage = false;
            FreeAgencyState.AtFreeAgencyStage = false;

            switch (state)
            {
                case (int)FranchiseState.ResignPlayers:
                    ResignPlayersState.AtResignPlayersStage = true;
                    break;
                case (int)FranchiseState.RestrictedFreeAgents:
                    RFAState.AtRFAStage = true;
                    break;
            }
        }

		public List<string[]> DraftClassFields
		{
			get
			{
				return draftClassFields;
			}
		}

        public FranchiseStageRecord FranchiseStage
        {
            get
            {
                return (FranchiseStageRecord)tableModels[EditorModel.FRANCHISE_STAGE_TABLE].GetRecord(0);
            }
        }

        public FranchiseTimeRecord FranchiseTime
        {
            get
            {
                return (FranchiseTimeRecord)tableModels[EditorModel.FRANCHISE_TIME_TABLE].GetRecord(0);
            }
        }

        public DraftStateRecord DraftState
        {
            get
            {
                return (DraftStateRecord)tableModels[EditorModel.DRAFT_STATE_TABLE].GetRecord(0);
            }
        }

        public FreeAgencyStateRecord FreeAgencyState
        {
            get
            {
                return (FreeAgencyStateRecord)tableModels[EditorModel.FREE_AGENCY_STATE_TABLE].GetRecord(0);
            }
        }

        public ResignPlayersStateRecord ResignPlayersState
        {
            get
            {
                return (ResignPlayersStateRecord)tableModels[EditorModel.RESIGN_PLAYERS_STATE_TABLE].GetRecord(0);
            }
        }

        public RFAStateRecord RFAState
        {
            get
            {
                return (RFAStateRecord)tableModels[EditorModel.RFA_STATE_TABLE].GetRecord(0);
            }
        }

        public ScoutingStateRecord ScoutingState
        {
            get
            {
                return (ScoutingStateRecord)tableModels[EditorModel.SCOUTING_STATE_TABLE].GetRecord(0);
            }
		}
		#endregion

        #region Get/Set
        /// <summary>
		/// The Enumerated Filetype that is currently loaded
		/// </summary>
		public MaddenFileType FileType
		{
			get
			{
				return fileType;
			}
		}
		/// <summary>
		/// The Enumerated FileVersion that is currently loaded
		/// </summary>
		public MaddenFileVersion FileVersion
		{
			get	{ return fileVersion; }
            set { fileVersion = value; }
		}
		/// <summary>
		/// The PlayerEditingModel object to manipulate Player objects
		/// </summary>
		public PlayerEditingModel PlayerModel
		{
			get
			{
				return playerEditingModel;
			}
		}
		/// <summary>
		/// The CoachEditingModel object to manipulate Coach objects
		/// </summary>
		public CoachEditingModel CoachModel
		{
			get
			{
				return coachEditingModel;
			}
		}
		/// <summary>
		/// The TeamEditingModel object to manipulate Team objects
		/// </summary>
		public TeamEditingModel TeamModel
		{
			get
			{
				return teamEditingModel;
			}
		}
        
        public StadiumEditingModel StadiumModel
        {
            get { return stadiumEditingModel; }
        }
		/// <summary>
		/// The SalaryCapRecord object. There is only ever one record in this table
		/// </summary>
		public SalaryCapRecord SalaryCapModel
		{
			get
			{
				return salaryCapRecord;
			}
		}
		/// <summary>
		/// Returns the GameOptionRecord. There is only ever one record in this table.
		/// </summary>
		public GameOptionRecord GameOptionModel
		{
			get
			{
				return gameOptionsRecord;
			}
		}
		/// <summary>
		/// This readonly property returns the tablemodel collection allowing you
		/// to get lowerlevel access to the database records for any table
		/// </summary>
		public TableModelDictionary TableModels
		{
			get
			{
				return tableModels;
			}
		}
		/// <summary>
		/// The Dirty flag indicates wether or not changes have been made to the loaded objects
		/// and therefore these changes need to be saved in order to be persisted
		/// </summary>        
		public bool Dirty
		{
			get
			{
				return dirty;
			}
			set
			{
				dirty = value;
				//Set the scoutingForm into dirty view
                if(view != null)
				    view.Dirty = value;
			}
		}

        public UserOptionRecord UserOptionModel
        {
            get { return useroptions; }
        }
        public UserInfoRecord UserInfo
        {
            get { return userinfo; }
        }

        public Dictionary<string, int> TableOrder
        {
            get { return tableOrder; }
        }
        public DraftClass DraftClassModel
        {
            get { return draftclassmodel; }
        }

        #endregion

        /// <summary>
		/// This is the main function that processes the database file and loads the 
		/// tables into objects in memory
		/// </summary>
		/// <returns>True or False depending on success of loading the database file</returns>
        private bool ProcessFile()
        {
            bool result = true;
            try
            {
                tableCount = TDB.TDBDatabaseGetTableCount(dbIndex);
                if (tableCount == MADDEN_2019_DBTEAM_COUNT)
                {
                    fileVersion = MaddenFileVersion.Ver2019;
                    fileType = MaddenFileType.DBTeam;
                    BigEndian = true;
                }  
                
                if (fileType != MaddenFileType.DBTeam)
                {
                    //  Adding a check for DB Templates first
                    for (int j = 0; j < tableCount; j++)
                    {
                        TdbTableProperties tableProps = new TdbTableProperties();
                        tableProps.Name = new string((char)0, 5);
                        TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

                        if (tableProps.Name == "PORC")
                        {
                            fileType = MaddenFileType.Template;
                            break;
                        }
                    }

                    Trace.WriteLine("Table count in " + fileName + " = " + tableCount);
                    //Set the file type of this loaded file
                    if (fileType != MaddenFileType.Template)
                    {
                        if (tableCount == 23|| tableCount == 72 || tableCount == 85 || tableCount == 116 || tableCount == 168 || tableCount == 203 || tableCount == 215 )
                        {
                            fileType = MaddenFileType.Streameddata;

                            if (tableCount == MADDEN_2004_STREAMED_COUNT)
                                fileVersion = MaddenFileVersion.Ver2004;
                            else if (tableCount == MADDEN_2005_STREAMED_COUNT)
                                fileVersion = MaddenFileVersion.Ver2005;
                            else if (tableCount == MADDEN_2006_STREAMED_COUNT)
                                fileVersion = MaddenFileVersion.Ver2006;
                            else if (tableCount == MADDEN_2007_STREAMED_COUNT)
                                fileVersion = MaddenFileVersion.Ver2007;
                            else if (tableCount == MADDEN_2008_STREAMED_COUNT)
                                fileVersion = MaddenFileVersion.Ver2008;
                            else if (tableCount == MADDEN_2019_STREAMED_COUNT)
                            {
                                for (int j = 0; j < tableCount; j++)
                                {
                                    TdbTableProperties tableProps = new TdbTableProperties();
                                    tableProps.Name = new string((char)0, 5);
                                    TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

                                    if (tableProps.Name == "YTIC")
                                    {
                                        fileType = MaddenFileType.GameMode;
                                        break;
                                    }
                                }
                                
                                fileVersion = MaddenFileVersion.Ver2019;
                                BigEndian = true;
                            }
                            else if (tableCount == MADDEN_2019_DATA_RAM)
                            {
                                fileVersion = MaddenFileVersion.Ver2019;
                                fileType = MaddenFileType.DataRam;
                                BigEndian = true;
                            }
                        }
                        else if (tableCount == 10 || tableCount == 11 || tableCount == 5 || tableCount == 8)
                        {
                            fileType = MaddenFileType.Roster;
                            if (tableCount == MADDEN_ROS_2007_TABLE_COUNT)
                                fileVersion = MaddenFileVersion.Ver2007;
                            else if (tableCount == MADDEN_ROS_2019_TABLE_COUNT)
                                fileVersion = MaddenFileVersion.Ver2019;

                        }
                        else if (tableCount == MADDEN_2019_USER_CONFIG_COUNT)
                        {
                            fileVersion = MaddenFileVersion.Ver2019;
                            for (int j = 0; j < tableCount; j++)
                            {
                                TdbTableProperties tableProps = new TdbTableProperties();
                                tableProps.Name = new string((char)0, 5);
                                TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

                                if (tableProps.Name == "BALP")
                                {
                                    fileType = MaddenFileType.StaticData;
                                    BigEndian = true;
                                    break;
                                }
                                else if (tableProps.Name == "GOPT")
                                {
                                    fileType = MaddenFileType.UserConfig;
                                    break;
                                }
                                
                            }
                        }
                        else
                        {
                            fileType = MaddenFileType.Franchise;
                            Trace.WriteLine("Franchise contains " + tableCount + " tables");
                            switch (tableCount)
                            {
                                case MADDEN_FRA_2008_TABLE_COUNT:
                                    fileVersion = MaddenFileVersion.Ver2008;
                                    break;
                                case MADDEN_FRA_2007_TABLE_COUNT:
                                    fileVersion = MaddenFileVersion.Ver2007;
                                    break;
                                case MADDEN_FRA_2005_TABLE_COUNT:
                                    fileVersion = MaddenFileVersion.Ver2005;
                                    break;
                                case MADDEN_FRA_2006_TABLE_COUNT:
                                    fileVersion = MaddenFileVersion.Ver2006;
                                    break;
                                default:
                                    fileVersion = MaddenFileVersion.Ver2004;
                                    break;
                            }
                        }
                    }
                }

                #region Roster File
                //  10 or 11 tables based on version
                if (FileType == MaddenFileType.Roster)
                {
                    if (fileVersion == MaddenFileVersion.Ver2019)
                    {
                        tableOrder.Add(DEPTH_CHART_TABLE, -1);
                        tableOrder.Add(INJURY_TABLE, -1);
                        tableOrder.Add(PLAYER_TABLE, -1);
                        tableOrder.Add(TEAM_TABLE, -1);
                    }
                    else
                    {
                        tableOrder.Add(CITY_TABLE, -1);
                        if (fileVersion < MaddenFileVersion.Ver2007)
                            tableOrder.Add(COACH_SLIDER_TABLE, -1);
                        tableOrder.Add(COACH_TABLE, -1);
                        // CTMP
                        // CTMU
                        tableOrder.Add(DEPTH_CHART_TABLE, -1);
                        tableOrder.Add(INJURY_TABLE, -1);
                        tableOrder.Add(PLAYER_TABLE, -1);
                        tableOrder.Add(STADIUM_TABLE, -1);
                        tableOrder.Add(UNIFORM_TABLE, -1);
                        tableOrder.Add(TEAM_TABLE, -1);
                    }
                }
                #endregion

                #region Franchise File
                // Make sure we only load some tables if we are a franchise file
                else if (fileType == MaddenFileType.Franchise)
                {
                    tableOrder.Add(CITY_TABLE, -1);
                    if (fileVersion < MaddenFileVersion.Ver2007)
                        tableOrder.Add(COACH_SLIDER_TABLE, -1);
                    tableOrder.Add(COACH_TABLE, -1);
                    // CTMP
                    // CTMU
                    tableOrder.Add(DEPTH_CHART_TABLE, -1);
                    tableOrder.Add(INJURY_TABLE, -1);
                    tableOrder.Add(PLAYER_TABLE, -1);
                    tableOrder.Add(STADIUM_TABLE, -1);
                    tableOrder.Add(UNIFORM_TABLE, -1);
                    tableOrder.Add(TEAM_TABLE, -1);
                    tableOrder.Add(SALARY_CAP_TABLE, -1);
                    tableOrder.Add(WEEKLY_INCOME_TABLE, -1);
                    tableOrder.Add(TEAM_WIN_LOSS_RECORD, -1);
                    tableOrder.Add(LEAGUE_REVENUE_TABLE, -1);
                    tableOrder.Add(OWNER_REVENUE_TABLE, -1);
                    tableOrder.Add(OWNER_TABLE, -1);
                    tableOrder.Add(SCHEDULE_TABLE, -1);
                    tableOrder.Add(DRAFT_PICK_TABLE, -1);
                    tableOrder.Add(DRAFTED_PLAYERS_TABLE, -1);
                    tableOrder.Add(BOXSCORE_OFFENSE_TABLE, -1);
                    tableOrder.Add(BOXSCORE_DEFENSE_TABLE, -1);
                    tableOrder.Add(BOXSCORE_SCORING_SUMMARY, -1);
                    tableOrder.Add(SEASON_STATS_DEFENSE_TABLE, -1);
                    tableOrder.Add(SEASON_STATS_OFFENSE_TABLE, -1);
                    tableOrder.Add(CAREER_STATS_DEFENSE_TABLE, -1);
                    tableOrder.Add(CAREER_STATS_OFFENSE_TABLE, -1);
                    tableOrder.Add(TEAM_SEASON_STATS, -1);
                    tableOrder.Add(BOXSCORE_TEAM_TABLE, -1);
                    tableOrder.Add(BOXSCORE_OFFENSIVE_LINE_TABLE, -1);
                    tableOrder.Add(SEASON_STATS_OFFENSIVE_LINE_TABLE, -1);
                    tableOrder.Add(CAREER_STATS_OFFENSIVE_LINE_TABLE, -1);
                    tableOrder.Add(SEASON_GAMES_PLAYED_TABLE, -1);
                    tableOrder.Add(CAREER_GAMES_PLAYED_TABLE, -1);
                    tableOrder.Add(CAREER_STATS_KICKPUNT_TABLE, -1);
                    tableOrder.Add(SEASON_STATS_KICKPUNT_TABLE, -1);
                    tableOrder.Add(CAREER_STATS_KICKPUNT_RETURN_TABLE, -1);
                    tableOrder.Add(SEASON_STATS_KICKPUNT_RETURN_TABLE, -1);
                    tableOrder.Add(PLAYER_AWARDS_TABLE, -1);
                    tableOrder.Add(PRO_BOWL_PLAYERS, -1);
                    tableOrder.Add(TEAM_SEASON_RECORDS, -1);
                    tableOrder.Add(TEAM_GAME_RECORDS, -1);
                    tableOrder.Add(FRANCHISE_TIME_TABLE, -1);
                    tableOrder.Add(SCOUTING_STATE_TABLE, -1);
                    tableOrder.Add(COACHING_HISTORY_TABLE, -1);
                    tableOrder.Add(COACHES_EXPECTED_SALARY, -1);

                    if (fileVersion > MaddenFileVersion.Ver2004)
                    {
                        tableOrder.Add(INACTIVE_TABLE, -1);
                        tableOrder.Add(RFA_STATE_TABLE, -1);
                        tableOrder.Add(RFA_PLAYERS, -1);
                        tableOrder.Add(RFA_SALARY_TENDERS, -1);
                        tableOrder.Add(PROGRESSION_SCHEDULE, -1);
                        tableOrder.Add(TEAM_CAPTAIN_TABLE, -1);
                        tableOrder.Add(TEAM_RIVAL_HISTORY, -1);
                    }

                    tableOrder.Add(RESIGN_PLAYERS_STATE_TABLE, -1);
                    tableOrder.Add(FREE_AGENCY_STATE_TABLE, -1);
                    tableOrder.Add(DRAFT_STATE_TABLE, -1);
                    tableOrder.Add(FRANCHISE_STAGE_TABLE, -1);

                    tableOrder.Add(GAME_OPTIONS_TABLE, -1);
                    tableOrder.Add(USER_OPTIONS_TABLE, -1);

                    if (fileVersion < MaddenFileVersion.Ver2005)
                        tableOrder.Add(SALARY_CAP_INCREASE, -1);

                    tableOrder.Add(PLAYER_MINIMUM_SALARY_TABLE, -1);
                    tableOrder.Add(PLAYER_SALARY_DEMAND_TABLE, -1);
                }

                #endregion

                #region Streamed DB File
                else if (FileType == MaddenFileType.Streameddata)
                {
                    tableOrder.Add(COACH_COLLECTIONS_TABLE, -1);
                    tableOrder.Add(COLLEGES_TABLE, -1);
                    tableOrder.Add(DEPTH_CHART_SUBS, -1);

                    if (fileVersion != MaddenFileVersion.Ver2019)
                        tableOrder.Add(POSITION_SUBS, -1);

                    tableOrder.Add(PLAYER_LAST_NAMES, -1);
                    tableOrder.Add(PLAYER_FIRST_NAMES, -1);

                    if (fileVersion < MaddenFileVersion.Ver2019)
                    {
                        if (fileVersion >= MaddenFileVersion.Ver2005)
                        {
                            tableOrder.Add(PROGRESSION, -1);
                            tableOrder.Add(REGRESSION, -1);
                            tableOrder.Add(PTCB, -1);
                            tableOrder.Add(PTCE, -1);
                            tableOrder.Add(PTDE, -1);
                            tableOrder.Add(PTDT, -1);
                            tableOrder.Add(PTFB, -1);
                            tableOrder.Add(PTFS, -1);
                            tableOrder.Add(PTGA, -1);
                            tableOrder.Add(PTHB, -1);
                            tableOrder.Add(PTKI, -1);
                            tableOrder.Add(PTKP, -1);
                            tableOrder.Add(PTMB, -1);
                            tableOrder.Add(PTOB, -1);
                            tableOrder.Add(PTPU, -1);
                            tableOrder.Add(PTQB, -1);
                            tableOrder.Add(PTSS, -1);
                            tableOrder.Add(PTTA, -1);
                            tableOrder.Add(PTTE, -1);
                            tableOrder.Add(PTWR, -1);
                        }

                        if (fileVersion >= MaddenFileVersion.Ver2006)
                        {
                            tableOrder.Add(ROLES_DEFINE, -1);
                        }

                        if (fileVersion >= MaddenFileVersion.Ver2007)
                        {
                            tableOrder.Add(ROLES_INFO, -1);
                            tableOrder.Add(ROLES_PLAYER_EFFECTS, -1);
                            tableOrder.Add(ROLES_TEAM_EFFECTS, -1);
                            tableOrder.Add(STATS_REQUIRED, -1);
                        }
                    }
                    else
                    {
                        //2019
                        tableOrder.Add(PLAYER_OVERALL_CALC, -1);
                    }
                }
                #endregion

                #region DB Templates

                else if (fileType == MaddenFileType.Template)
                {
                    tableOrder.Add(PLAYER_OVERALL_CALC, -1);
                    tableOrder.Add(PLAYBOOK_TABLE, -1);
                }

                #endregion

                #region DataRam
                else if (fileType == MaddenFileType.DataRam)
                {

                }

                #endregion

                #region 2019 DB TEAMS
                else if (FileType == MaddenFileType.DBTeam)
                {
                    tableOrder.Add(COACH_TABLE, -1);
                    tableOrder.Add(DEPTH_CHART_TABLE, -1);
                    tableOrder.Add(INJURY_TABLE, -1);
                    //PLIA
                    //PLTR
                    tableOrder.Add(PLAYER_TABLE, -1);
                    tableOrder.Add(UNIFORM_TABLE, -1);
                    tableOrder.Add(TEAM_TABLE, -1);
                }
                else if (fileType == MaddenFileType.UserConfig)
                {
                    tableOrder.Add(GAME_OPTIONS_TABLE, -1);
                    tableOrder.Add(USER_OPTIONS_TABLE, -1);
                    tableOrder.Add(USER_INFO_TABLE, -1);
                }

                #endregion


                for (int j = 0; j < tableCount; j++)
                {
                    TdbTableProperties tableProps = new TdbTableProperties();
                    tableProps.Name = new string((char)0, 5);
                    TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);
                    
                    // Adding table to our dict. not adjusting to bigendian at this point, using
                    // this list for import/export via csv
                    TableNames.Add(tableProps.Name, j); ;

                    if (BigEndian)
                    {
                        tableProps.Name = ConvertBE(tableProps.Name);
                    }

                    #region Get Version from Playertable

                    // We use the player table to work out what version a roster file is.                   
                    if (fileType == MaddenFileType.Roster && tableProps.Name.Equals(PLAYER_TABLE))
                    {
                        switch (tableProps.FieldCount)
                        {
                            case MADDEN_ROS_2004_PLAY_FIELD_COUNT:
                                fileVersion = MaddenFileVersion.Ver2004;
                                break;
                            case MADDEN_ROS_2005_PLAY_FIELD_COUNT:
                                fileVersion = MaddenFileVersion.Ver2005;
                                break;
                            case MADDEN_ROS_2006_PLAY_FIELD_COUNT:
                                fileVersion = MaddenFileVersion.Ver2006;
                                break;
                            case MADDEN_ROS_2019_PLAY_FIELD_COUNT:
                                fileVersion = MaddenFileVersion.Ver2019;
                                break;
                            default:
                                fileVersion = MaddenFileVersion.Ver2007;
                                break;
                        }

                        // We check for field PRL2 (Player Weapon) which is new in v2008
                        if (fileVersion < MaddenFileVersion.Ver2019)
                        {
                            for (int i = 0; i < tableProps.FieldCount; i++)
                            {
                                TdbFieldProperties fieldProps = new TdbFieldProperties();
                                fieldProps.Name = new string((char)0, 5);
                                TDB.TDBFieldGetProperties(dbIndex, tableProps.Name, i, ref fieldProps);

                                if (fieldProps.Name == "PRL2")
                                    fileVersion = MaddenFileVersion.Ver2008;
                            }
                        }
                    }


                    #endregion

                    // If we found a table we want to process, then store its
                    // order number in our tableOrder Hashmap
                    if (tableOrder.ContainsKey(tableProps.Name))
                    {
                        tableOrder[tableProps.Name] = j;
                    }
                }

                if (this.FileType == MaddenFileType.Franchise)
                {
                    CurrentYearIndex += FranchiseTime.Year;
                    CurrentYear = 2003 + (int)FileVersion + FranchiseTime.Year;
                }
                else
                {
                    if (fileVersion == MaddenFileVersion.Ver2004)
                        CurrentYear = 2003;
                    else if (fileVersion == MaddenFileVersion.Ver2005)
                        CurrentYear = 2004;
                    else if (fileVersion == MaddenFileVersion.Ver2006)
                        CurrentYear = 2005;
                    else if (fileVersion == MaddenFileVersion.Ver2007)
                        CurrentYear = 2006;
                    else if (fileVersion == MaddenFileVersion.Ver2008)
                        CurrentYear = 2007;
                    else if (fileVersion == MaddenFileVersion.Ver2019)
                        CurrentYear = 2018;
                }
            }

            catch (DllNotFoundException e)
            {
                ExceptionDialog.Show(e);
            }

            bool unknown = true;
            foreach (KeyValuePair<string, int> pair in tableOrder)
            {
                if (pair.Value == -1)
                {
                    // Something is wrong, we expected to find this table but did not
                    Trace.WriteLine("Something is wrong so we are exiting");
                    //result = false;
                    break;
                }

                unknown = false;                
                //result &= ProcessTable(pair.Value);
            }

            if (unknown && fileType != MaddenFileType.DataRam)
                fileType = MaddenFileType.Unknown;


            Trace.WriteLine("File type is : " + fileType.ToString() + " and version is : " + fileVersion.ToString());
            return result;
        }
		/// <summary>
		/// This function processes a single table and constructs a TableModel object for that
		/// table. This includes loading the fields of the database and creating specific
		/// records for those fields.
		/// </summary>
		/// <param name="tableNumber">The Table number of the table in the database</param>
		/// <returns>True if successful, false otherwise</returns>
		public bool ProcessTable(int tableNumber)
		{
			//Reset the progress bar
            if (view != null)
			    view.updateTableProgress(0, "");

			//Get the table properties
			TdbTableProperties tableProps = new TdbTableProperties();
			tableProps.Name = new string((char)0, 5);
            
			TDB.TDBTableGetProperties(dbIndex, tableNumber, ref tableProps);

			TableModel table = new TableModel(tableProps.Name, this, dbIndex);
			
            Trace.WriteLine("Processing Table: " + table.Name);

			//For each field for this table, find the name and add it to a collection
			//so we can get each of these for each record
			List<TdbFieldProperties> fieldList = new List<TdbFieldProperties>();
			for (int i = 0; i < tableProps.FieldCount; i++)
			{
				TdbFieldProperties fieldProps = new TdbFieldProperties();
				fieldProps.Name = new string((char)0, 5);
				TDB.TDBFieldGetProperties(dbIndex, tableProps.Name, i, ref fieldProps);
                
                //Add this field to the list
				fieldList.Add(fieldProps);                
			}
            
			//Add the field list to the tablemodel
			table.SetFieldList(fieldList); 
            
            float currentProgress = 0.0f;
			float progressInterval = (1.0f / (float)tableProps.RecordCount) * 100.0f;
			
            for (int index = 0; index < tableProps.RecordCount; index++)           
			{
				bool deleted = TDB.TDBTableRecordDeleted(dbIndex, tableProps.Name, index);
				
                // Stingray68 - I'm not really sure we want to do this here as there are values marked as deleted when first starting a franchise.
                if (deleted)
				{
				    // If this record is deleted advance the index only
                    // What this means is that any initial records marked as deleted we will never
                    // use unless the database is compacted.					
					continue;
				}

                TableRecordModel record = table.ConstructRecordModel(index);

				if (record != null)
				{
					foreach (TdbFieldProperties fieldProps in fieldList)
                    {      
                        //Trace.WriteLine("Processing field: " + fieldProps.Name + " of type " + fieldProps.FieldType.ToString());                        
                        switch (fieldProps.FieldType)
						{
							case TdbFieldType.tdbString:
						
								string val = new string((char)0, (fieldProps.Size / 8)+1);
								try
								{
                                    TDB.TDBFieldGetValueAsString(dbIndex, tableProps.Name, fieldProps.Name, index, ref val);
								}
								catch (Exception err)
								{
									Trace.WriteLine(err.ToString());
								}
								record.RegisterField(fieldProps.Name, val);
								break;
							case TdbFieldType.tdbUInt:
								UInt32 intval;
                                intval = (UInt32)TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, index);
								record.RegisterField(fieldProps.Name, (int)intval);
								break;
							case TdbFieldType.tdbSInt:
								Int32 signedval;
                                signedval = TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, index);
								record.RegisterField(fieldProps.Name, signedval);
								break;
                            case TdbFieldType.tdbFloat:
                                float floatval;
                                floatval = TDB.TDBFieldGetValueAsFloat(dbIndex, tableProps.Name, fieldProps.Name, index);
                                record.RegisterField(fieldProps.Name, floatval);
                                break;
							default:
								Trace.WriteLine("NOT SUPPORTED YET!!!");
								break;
						}
					}
				}			

                if (view != null)
                {
                    currentProgress += progressInterval;
                    view.updateTableProgress((int)currentProgress, table.Name);
                }
			}

            // S68 - Adding code here to convert from big endian table/field names.  The tdbaccess.dll that was written to work with BE values
            // doesn't convert those, probably because the author treated them like strings, and at least for Madden, they are UInt32 values
            // so we need to reverse the bytes (characters)
            if (BigEndian)
            {
                string rev = ConvertBE(table.Name);
                table.Name = rev;
                // This function fixes all the table fields
                List<TdbFieldProperties> fields = table.FixBEfields();
                table.SetFieldList(fields);
            }

            // Getting errors here when we run into a file that is similar to the 04-08 files
            // It expects to find a given table and it isn't there
            if (tableModels.ContainsKey(table.Name))
            {   
                // try this for now
                fileType = MaddenFileType.Unknown;
            }
            else tableModels.Add(table.Name, table);
			Trace.WriteLine("Finished processing Table: " + table.Name);
            if (view != null)
			    view.updateTableProgress(100, table.Name);
			return true;
		}

        public void Save()
		{
			//To save we have to go through every record in our models and
			//save the dirty ones
			foreach (TableModel tmodel in tableModels.Values)
			{
				tmodel.Save();
			}
            
            TDB.TDBDatabaseCompact(dbIndex);
            TDB.TDBSave(dbIndex);

			this.Dirty = false;
		}

		public void Shutdown()
		{
			try
			{
                // Before we shutdown compact the database
                // We shouldnt be compacting or saving here, as this is just a shutdown ?
                //TDB.TDBDatabaseCompact(dbIndex);
                //TDB.TDBSave(dbIndex);
                TDB.TDBClose(dbIndex);
			}
			catch (DllNotFoundException e)
			{
				Trace.WriteLine(e.ToString());
			}
		}
       
	}
}
