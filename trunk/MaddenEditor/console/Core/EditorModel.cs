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
	/// This enumeration helps identify the two different kinds of files we can load
	/// with this editor
	/// </summary>
	public enum MaddenFileType 
	{ 
		Roster, 
		Franchise,
        Streameddata,
        Template
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
	}

	/// <summary>
	/// Enumeration to describe the positions in the Madden game. The order of these is
	/// important as they match up with the position ID's in the database
	/// </summary>
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

    /// <summary>
	/// This class is the main application model class. It is responsible for
	/// creating all editing models that are manipulated by the GUI.
	/// </summary>
	public class EditorModel
	{        
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

        public const int MADDEN_2004_STREAMED_COUNT = 72;
        public const int MADDEN_2005_STREAMED_COUNT = 116;
        public const int MADDEN_2006_STREAMED_COUNT = 168;
        public const int MADDEN_2007_STREAMED_COUNT = 203;
        public const int MADDEN_2008_STREAMED_COUNT = 215;
        
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
        public const string WEEKLY_INCOME_TABLE = "OTIW";
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
        public const string SEASON_STATS_DEFENSE_TABLE = "PSDE";
        public const string SEASON_STATS_KICKPUNT_TABLE = "PSKI";
        public const string SEASON_STATS_KICKPUNT_RETURN_TABLE = "PSKP";
        public const string SEASON_GAMES_PLAYED_TABLE = "PSNG";
        public const string SEASON_STATS_OFFENSE_TABLE = "PSOF";
        public const string SEASON_STATS_OFFENSIVE_LINE_TABLE = "PSOL";
        public const string RESIGN_PLAYERS_STATE_TABLE = "REIN";
        public const string RFA_STATE_TABLE = "RFIN";                       // 2005+
        public const string PLAYER_SALARY_DEMAND_TABLE = "SADP";
        public const string MINIMUM_SALARY_TABLE = "SAYP";
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
        public const string TEAM_STATS_TABLE = "TSSE";        
        public const string UNIFORM_TABLE = "TUNI";
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
		private int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private TableModelDictionary tableModels = null;
		private MaddenFileType fileType = MaddenFileType.Roster;
        private MaddenFileVersion fileVersion = MaddenFileVersion.Ver2004;
		private Dictionary<string, int> tableOrder = null;
		
		// Editing model objects
		private PlayerEditingModel playerEditingModel = null;
		private CoachEditingModel coachEditingModel = null;
		private TeamEditingModel teamEditingModel = null;
        private StadiumEditingModel stadiumEditingModel = null;
		private SalaryCapRecord salaryCapRecord = null;
		private GameOptionRecord gameOptionsRecord = null;
        private UserOptionRecord useroptions = null;

        // Latest adds       
        public static int totalplayers = 0;        
        public Dictionary<int, college_entry> Colleges = new Dictionary<int, college_entry>();
        public int CurrentYearIndex = 0;
        public int CurrentYear = 0;

        #region Constructors
		
        public EditorModel(string filename, MainForm form)
		{            
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
			}
			catch (DllNotFoundException e)
			{
				ExceptionDialog.Show(e);
				throw new ApplicationException("Can't open file: " + e.ToString());
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
			
			//Process the file
			if (!ProcessFile())
			{
				throw new ApplicationException("Error processing file: " + filename);
			}
			
			//Once we've processed the file create our editing models
            if (this.FileType != MaddenFileType.Streameddata && this.fileType != MaddenFileType.Template)
            {
                playerEditingModel = new PlayerEditingModel(this);
                teamEditingModel = new TeamEditingModel(this);
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

		#endregion

        
        
        public void InitColleges(EditorModel streamed)
        {
            if (streamed == null)
                return;
                        
            Colleges = new Dictionary<int,college_entry>();
            
            List<CollegesRecord> deleteme = new List<CollegesRecord>();

            foreach (CollegesRecord record in streamed.TableModels[EditorModel.COLLEGES_TABLE].GetRecords())
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
			get
			{
				return fileVersion;
			}
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
                    if (tableCount == 72 || tableCount == 116 || tableCount == 168 || tableCount == 203 || tableCount == 215)
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
                    }
                    else if (tableCount == 10 || tableCount == 11)
                    {
                        fileType = MaddenFileType.Roster;
                        if (tableCount == MADDEN_ROS_2007_TABLE_COUNT)
                            fileVersion = MaddenFileVersion.Ver2007;
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

                #region Roster File
                //  10 or 11 tables based on version
                if (FileType == MaddenFileType.Roster)
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
                    tableOrder.Add(TEAM_STATS_TABLE, -1);
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
                    tableOrder.Add(TEAM_SEASON_RECORDS, -1);
                    tableOrder.Add(TEAM_GAME_RECORDS, -1);
                    tableOrder.Add(FRANCHISE_TIME_TABLE, -1);
                    tableOrder.Add(SCOUTING_STATE_TABLE, -1);
                    tableOrder.Add(COACHING_HISTORY_TABLE, -1);

                    if (fileVersion > MaddenFileVersion.Ver2004)
                    {
                        tableOrder.Add(RFA_STATE_TABLE, -1);
                        tableOrder.Add(PROGRESSION_SCHEDULE, -1);
                        tableOrder.Add(TEAM_CAPTAIN_TABLE, -1);
                        tableOrder.Add(INACTIVE_TABLE, -1);
                    }

                    tableOrder.Add(RESIGN_PLAYERS_STATE_TABLE, -1);
                    tableOrder.Add(FREE_AGENCY_STATE_TABLE, -1);
                    tableOrder.Add(DRAFT_STATE_TABLE, -1);
                    tableOrder.Add(FRANCHISE_STAGE_TABLE, -1);                    
					
					tableOrder.Add(GAME_OPTIONS_TABLE, -1);
                    tableOrder.Add(USER_OPTIONS_TABLE, -1);
                    
                    if (fileVersion < MaddenFileVersion.Ver2005)
                        tableOrder.Add(SALARY_CAP_INCREASE, -1);

                    tableOrder.Add(MINIMUM_SALARY_TABLE, -1);
                    tableOrder.Add(PLAYER_SALARY_DEMAND_TABLE, -1);
                }

                #endregion

                #region Streamed DB File
                else if (FileType == MaddenFileType.Streameddata)
                {
                    tableOrder.Add(COACH_COLLECTIONS_TABLE, -1);
                    tableOrder.Add(COLLEGES_TABLE, -1);
                    tableOrder.Add(DEPTH_CHART_SUBS, -1);
                    tableOrder.Add(POSITION_SUBS, -1);
                    tableOrder.Add(PLAYER_LAST_NAMES, -1);
                    tableOrder.Add(PLAYER_FIRST_NAMES, -1);

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
                #endregion

                #region DB Templates

                else if (fileType == MaddenFileType.Template)
                {
                    tableOrder.Add(PLAYER_OVERALL_CALC, -1);
                    tableOrder.Add(PLAYBOOK_TABLE, -1);
                }

                #endregion


                for (int j = 0; j < tableCount; j++)
                {
                    TdbTableProperties tableProps = new TdbTableProperties();
                    tableProps.Name = new string((char)0, 5);
                    TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

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
                            default:
                                fileVersion = MaddenFileVersion.Ver2007;
                                break;
                        }

                        // We check for field PRL2 (Player Weapon) which is new in v2008
                        for (int i = 0; i < tableProps.FieldCount; i++)
                        {
                            TdbFieldProperties fieldProps = new TdbFieldProperties();
                            fieldProps.Name = new string((char)0, 5);
                            TDB.TDBFieldGetProperties(dbIndex, tableProps.Name, i, ref fieldProps);
                            if (fieldProps.Name == "PRL2")
                                fileVersion = MaddenFileVersion.Ver2008;
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
			}
			catch (DllNotFoundException e)
			{
				ExceptionDialog.Show(e);
			}

			foreach (KeyValuePair<string, int> pair in tableOrder)
			{
				if (pair.Value == -1)
				{		
					// Something is wrong, we expected to find this table but did not
					Trace.WriteLine("Something is wrong so we are exiting");
					result = false;
					break;
				}
				//result &= ProcessTable(pair.Value);
			}

            

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
            if (table.Name == "PSOF")
            {
                bool stop = true;
            }
			
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

			//int recordsFound = 0;
			int index = 0;
			float currentProgress = 0.0f;
			float progressInterval = (1.0f / (float)tableProps.RecordCount) * 100.0f;
			while (index != tableProps.RecordCount)
			{
				bool deleted = TDB.TDBTableRecordDeleted(dbIndex, tableProps.Name, index);
				
                // Stingray68 - I'm not sure we want to do this here as there are values marked as deleted when first starting a franchise.
                if (deleted)
				{
				    // If this record is deleted advance the index only
                    // What this means is that any initial records marked as deleted we will never
                    // use unless the database is compacted.

					index++;
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

				index++;

                if (view != null)
                {
                    currentProgress += progressInterval;
                    view.updateTableProgress((int)currentProgress, table.Name);
                }
			}

			tableModels.Add(table.Name, table);
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
