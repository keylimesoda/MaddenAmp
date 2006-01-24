/******************************************************************************
 * Gommo's Madden Editor
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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Db;
using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.FranchiseState;

namespace MaddenEditor.Core
{
	/// <summary>
	/// This enumeration helps identify the two different kinds of files we can load
	/// with this editor
	/// </summary>
	public enum MaddenFileType 
	{ 
		RosterFile, 
		FranchiseFile 
	}

    public enum FranchiseState
    {
        RestrictedFreeAgents = 0,
        ResignPlayers
    }

	public enum MaddenFileVersion
	{
		Ver2004,
		Ver2005,	//Franchise contains 159 tables
		Ver2006		//Franchise contains 183
	}

	/// <summary>
	/// Enumeration to describe the positions in the Madden game. The order of these is
	/// important as they match up with the position ID's in the database
	/// </summary>
	public enum MaddenPositions
	{
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
		P
	}

	/// <summary>
	/// This class is the main application model class. It is responsible for
	/// creating all editing models that are manipulated by the GUI.
	/// </summary>
	public class EditorModel
	{
		public const string SUPPORT_EMAIL = "bugs@tributech.com.au";
		public const int FREE_AGENT_TEAM_ID = 1009;
		public const int MADDEN_FRA_2004_TABLE_COUNT = 136;
		public const int MADDEN_FRA_2005_TABLE_COUNT = 159;
		public const int MADDEN_FRA_2006_TABLE_COUNT = 183;
		public const int MADDEN_ROS_2004_TEAM_FIELD_COUNT = 112;
		public const int MADDEN_ROS_2005_TEAM_FIELD_COUNT = 115;
		public const int MADDEN_ROS_2006_TEAM_FIELD_COUNT = 111;
		public const string UNKNOWN_TEAM_NAME = "UNKNOWN_TEAM";
		public const string PLAYER_TABLE = "PLAY";
		public const string TEAM_TABLE = "TEAM";
		public const string INJURY_TABLE = "INJY";
		public const string COACH_TABLE = "COCH";
		public const string SALARY_CAP_TABLE = "SLRI";
		public const string DEPTH_CHART_TABLE = "DCHT";
		public const string COACH_SLIDER_TABLE = "CPSE";
		public const string TEAM_CAPTAIN_TABLE = "TCPT";
		public const string OWNER_TABLE = "OWNR";
		public const string CITY_TABLE = "CITY";
		public const string SCHEDULE_TABLE = "SCHD";
		// Madden 2006 specific tables
		public const string GAME_OPTIONS_TABLE = "GOPT";
        // MADDEN DRAFT EDIT
        public const string DRAFT_PICK_TABLE = "DRPK";
        public const string DRAFTED_PLAYERS_TABLE = "DRPL";

        // franchise state tables
        public const string FRANCHISE_TIME_TABLE = "SEAI";
        public const string DRAFT_STATE_TABLE = "DRIN";
        public const string FREE_AGENCY_STATE_TABLE = "FAIN";
        public const string RESIGN_PLAYERS_STATE_TABLE = "REIN";
        public const string RFA_STATE_TABLE = "RFIN";
        public const string SCOUTING_STATE_TABLE = "SCIN";
        public const string FRANCHISE_STAGE_TABLE = "MOIN";

        // stats
        public const string BOXSCORE_DEFENSE_TABLE = "BDEF";
        public const string BOXSCORE_OFFENSE_TABLE = "BOFF";
        public const string SEASON_STATS_OFFENSE_TABLE = "PSOF";
        public const string SEASON_STATS_DEFENSE_TABLE = "PSDE";
        public const string CAREER_STATS_OFFENSE_TABLE = "PCOF";
        public const string CAREER_STATS_DEFENSE_TABLE = "PCDE";
        public const string TEAM_STATS_TABLE = "TSSE";
        public const string BOXSCORE_TEAM_TABLE = "BTES";
        public const string BOXSCORE_OFFENSIVE_LINE_TABLE = "BOLN";
        public const string SEASON_STATS_OFFENSIVE_LINE_TABLE = "PSOL";
        public const string CAREER_STATS_OFFENSIVE_LINE_TABLE = "PCOL";
        public const string SEASON_GAMES_PLAYED_TABLE = "PSNG";
        public const string CAREER_GAMES_PLAYED_TABLE = "PCNG";

		private List<string[]> draftClassFields;

        public bool draftStarted = false;
        // MADDEN DRAFT EDIT

		private bool dirty = false;
		private int dbIndex = -1;
		private int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private Dictionary<string, TableModel> tableModels = null;
		private MaddenFileType fileType = MaddenFileType.RosterFile;
		private MaddenFileVersion fileVersion = MaddenFileVersion.Ver2005; //Assume 2005
		private Dictionary<string, int> tableOrder = null;
		
		// Editing model objects
		private PlayerEditingModel playerEditingModel = null;
		private CoachEditingModel coachEditingModel = null;
		private TeamEditingModel teamEditingModel = null;
		private SalaryCapRecord salaryCapRecord = null;

        // MADDEN DRAFT EDIT
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
        // MADDEN DRAFT EDIT
		
		public EditorModel(string filename, MainForm form)
		{
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
				Console.WriteLine(e.ToString());
				throw new ApplicationException("Can't open file: " + e.ToString());
			}
			//This collection will hold the created TableModel objects for each database table opened
			tableModels = new Dictionary<string, TableModel>();
			//We create a collection of database table names that we want to load
			tableOrder = new Dictionary<string, int>();

			//Process the file
			if (!ProcessFile())
			{
				throw new ApplicationException("Error processing file: " + filename);
			}

			//Once we've processed the file create our editing models
			playerEditingModel = new PlayerEditingModel(this);
			teamEditingModel = new TeamEditingModel(this);
			coachEditingModel = new CoachEditingModel(this);
		

			if (fileType == MaddenFileType.FranchiseFile)
			{
				//Get the SalaryCapRecord for its info
				salaryCapRecord = (SalaryCapRecord)TableModels[SALARY_CAP_TABLE].GetRecord(0);
			}
		}
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
		/// This readonly property returns the tablemodel collection allowing you
		/// to get lowerlevel access to the database records for any table
		/// </summary>
		public Dictionary<string, TableModel> TableModels
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
				view.Dirty = value;
			}
		}

		public TableModel GetTable(string tableName)
		{
			return tableModels[tableName];
		}
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
				Console.WriteLine("Table count in {0} = {1}", fileName, tableCount);
				//Set the filetype of this loaded file
				if (tableCount == 11)
				{
					fileType = MaddenFileType.RosterFile;
					//We need to find a way to distinguish between the different madden versions
					//We will do this further down when we get the tables properties
				}
				else
				{
					fileType = MaddenFileType.FranchiseFile;
					Console.WriteLine("Franchise contains {0} tables", tableCount);
					switch (tableCount)
					{
						case MADDEN_FRA_2005_TABLE_COUNT:
							fileVersion = MaddenFileVersion.Ver2005;
							break;
						case MADDEN_FRA_2006_TABLE_COUNT:
							fileVersion = MaddenFileVersion.Ver2006;
							break;
						default:
							//Madden 2004 file
							fileVersion = MaddenFileVersion.Ver2004;
							break;
					}
				}
				//Initialise the tableOrder with the Table names we want to 
				//process
				tableOrder.Add(TEAM_TABLE, -1);
				tableOrder.Add(PLAYER_TABLE, -1);
				tableOrder.Add(INJURY_TABLE, -1);
				tableOrder.Add(COACH_TABLE, -1);
                tableOrder.Add(DEPTH_CHART_TABLE, -1);
				tableOrder.Add(COACH_SLIDER_TABLE, -1);
				tableOrder.Add(CITY_TABLE, -1);
				//Make sure we only load some tables if we are a franchise file
				if (fileType == MaddenFileType.FranchiseFile)
				{
					tableOrder.Add(SALARY_CAP_TABLE, -1);
					tableOrder.Add(OWNER_TABLE, -1);
					tableOrder.Add(SCHEDULE_TABLE, -1);
                    // MADDEN DRAFT EDIT
                    tableOrder.Add(DRAFT_PICK_TABLE, -1);
                    tableOrder.Add(DRAFTED_PLAYERS_TABLE, -1);

                    tableOrder.Add(BOXSCORE_OFFENSE_TABLE, -1);
                    tableOrder.Add(BOXSCORE_DEFENSE_TABLE, -1);
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
                    tableOrder.Add(FRANCHISE_TIME_TABLE, -1);

                    tableOrder.Add(SCOUTING_STATE_TABLE, -1);
                    tableOrder.Add(RFA_STATE_TABLE, -1);
                    tableOrder.Add(RESIGN_PLAYERS_STATE_TABLE, -1);
                    tableOrder.Add(FREE_AGENCY_STATE_TABLE, -1);
                    tableOrder.Add(DRAFT_STATE_TABLE, -1);
                    tableOrder.Add(FRANCHISE_STAGE_TABLE, -1);
                    // MADDEN DRAFT EDIT
					if (fileVersion >= MaddenFileVersion.Ver2005)
					{
						tableOrder.Add(TEAM_CAPTAIN_TABLE, -1);
					}
					if (fileVersion >= MaddenFileVersion.Ver2006)
					{
						tableOrder.Add(GAME_OPTIONS_TABLE, -1);
					}
				}

				for (int j = 0; j < tableCount; j++)
				{
					TdbTableProperties tableProps = new TdbTableProperties();
					tableProps.Name = new string((char)0, 5);
					TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

					//We use the player table to work out what version a roster file is
					if (FileType == MaddenFileType.RosterFile && tableProps.Name.Equals(PLAYER_TABLE))
					{
						switch (tableProps.FieldCount)
						{
							case MADDEN_ROS_2004_TEAM_FIELD_COUNT:
								fileVersion = MaddenFileVersion.Ver2004;
								break;
							case MADDEN_ROS_2005_TEAM_FIELD_COUNT:
								fileVersion = MaddenFileVersion.Ver2005;
								break;
							case MADDEN_ROS_2006_TEAM_FIELD_COUNT:
								fileVersion = MaddenFileVersion.Ver2006;
								break;
							default:
								break;
						}
					}

					//If we found a table we want to process, then store its
					//order number in our tableOrder Hashmap
					if (tableOrder.ContainsKey(tableProps.Name))
					{
						tableOrder[tableProps.Name] = j;
					}
				}
			}
			catch (DllNotFoundException e)
			{
				Console.WriteLine(e.ToString());
			}

			foreach (KeyValuePair<string, int> pair in tableOrder)
			{
				if (pair.Value == -1)
				{
					//This could simply mean we have hit a table 
					//that doesnt exist in a madden 2004 database
					if (pair.Key.Equals(TEAM_CAPTAIN_TABLE))
					{
						//This is ok but mark our file as a Madden2004 database
						fileVersion = MaddenFileVersion.Ver2004;
						continue;
					}
					//Something is wrong, we expected to have found a table
					//for this table but we didnt find one, so die
					result = false;
					break;
				}
				result &= ProcessTable(pair.Value);
			}
						
			return result;
		}
		/// <summary>
		/// This function processes a single table and constructs a TableModel object for that
		/// table. This includes loading the fields of the database and creating specific
		/// records for those fields.
		/// </summary>
		/// <param name="tableNumber">The Table number of the table in the database</param>
		/// <returns>True if successful, false otherwise</returns>
		private bool ProcessTable(int tableNumber)
		{
			//Reset the progress bar
			view.updateProgress(0, "");

			//Get the table properties
			TdbTableProperties tableProps = new TdbTableProperties();
			tableProps.Name = new string((char)0, 5);
			TDB.TDBTableGetProperties(dbIndex, tableNumber, ref tableProps);

			TableModel table = new TableModel(tableProps.Name, this, dbIndex);
			Console.WriteLine("Processing Table: " + table.Name);

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

				if (deleted)
				{
					//If this record is deleted advance the index only
                    //What this means is that any initial records marked as deleted we will never
                    //use unless the database is compacted.

					index++;
					continue;
				}

                TableRecordModel record = table.ConstructRecordModel(index);

				if (record != null)
				{

					foreach (TdbFieldProperties fieldProps in fieldList)
					{
						//Console.WriteLine("Processing field: " + fieldProps.Name + " of type " + fieldProps.FieldType.ToString());

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
									Console.WriteLine(err.ToString());
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
							default:
								Console.WriteLine("NOT SUPPORTED YET!!!");
								break;
						}
					}
				}

				index++;
				//recordsFound++;

				currentProgress += progressInterval;
				view.updateProgress((int)currentProgress, table.Name);
			}

			tableModels.Add(table.Name, table);
			Console.WriteLine("Finished processing Table: " + table.Name);
			view.updateProgress(100, table.Name);
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

            //Now save to database
            TDB.TDBSave(dbIndex);

			this.Dirty = false;
		}

		public void Shutdown()
		{
			try
			{
                //Before we shutdown compact the database
                TDB.TDBDatabaseCompact(dbIndex);
                TDB.TDBSave(dbIndex);
                TDB.TDBClose(dbIndex);
			}
			catch (DllNotFoundException e)
			{
				Console.WriteLine(e.ToString());
			}
		}
	}
}
