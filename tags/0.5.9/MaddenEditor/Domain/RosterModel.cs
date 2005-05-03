/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Db;
using MaddenEditor.Forms;

namespace MaddenEditor.Domain
{
	public enum MaddenFileType 
	{ 
		RosterFile, 
		FranchiseFile 
	}

	/*public enum MaddenTable
	{
		CITY_TABLE = 0,
		CPSE_TABLE = 1,
		COACH_TABLE = 2,
		CTMP_TABLE = 3,
		CTMU_TABLE = 4,
		DEPTH_CHART_TABLE = 5,
		INJURY_TABLE = 6,
		PLAYER_TABLE = 7,
		STADIUM_TABLE = 8,
		UNIFORM_TABLE = 9,
		TEAM_TABLE = 10
	}*/

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

	public class RosterModel
	{
		public const int FREE_AGENT_TEAM_ID = 1009;
		public const string UNKNOWN_TEAM_NAME = "UNKNOWN_TEAM";
		public const string PLAYER_TABLE = "PLAY";
		public const string TEAM_TABLE = "TEAM";
		public const string INJURY_TABLE = "INJY";

		private bool dirty = false;
		private int dbIndex = -1;
		private int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private Dictionary<string, TableModel> tableModels = null;
		private Dictionary<int, string> teamNameList = null;
		private int currentPlayerIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** The current position filter */
		private int currentPositionFilter = -1;
		/** If we are currently filtering for draft class */
		private bool currentDraftClassFilter = false;
		private Dictionary<string, int> tableOrder = null;

		public RosterModel(string filename, MainForm form)
		{
			view = form;
			this.fileName = filename;

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

			tableModels = new Dictionary<string, TableModel>();
			tableOrder = new Dictionary<string, int>();

			//Initialise the tableOrder with the Table names we want to 
			//Process
			tableOrder.Add(TEAM_TABLE, -1);
			tableOrder.Add(PLAYER_TABLE, -1);
			tableOrder.Add(INJURY_TABLE, -1);

			//Process the file
			if (!ProcessFile())
			{
				throw new ApplicationException("Error processing file: " + filename);
			}
		}

		public bool Dirty
		{
			get
			{
				return dirty;
			}
			set
			{
				dirty = value;
				//Set the form into dirty view
				view.Dirty = value;
			}
		}

		public TableModel GetTable(string tableName)
		{
			return tableModels[tableName];
		}

		private bool ProcessFile()
		{
			bool result = true;
			try
			{
				tableCount = TDB.TDBDatabaseGetTableCount(dbIndex);
				Console.WriteLine("Table count in {0} = {1}", fileName, tableCount);

				for (int j = 0; j < tableCount; j++)
				{
					TdbTableProperties tableProps = new TdbTableProperties();
					tableProps.Name = new string((char)0, 5);
					TDB.TDBTableGetProperties(dbIndex, j, ref tableProps);

					Console.WriteLine("File Contains Table: {0}", tableProps.Name);

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

			foreach (int tableNumber in tableOrder.Values)
			{
				if (tableNumber == -1)
				{
					//Something is wrong, we expected to have found a table
					//for this table but we didnt find one, so die
					result = false;
					break;
				}
				result &= ProcessTable(tableNumber);
			}

			return result;
		}

		private bool ProcessTable(int tableNumber)
		{
			//Reset the progress bar
			view.updateProgress(0);

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

			int recordsFound = 0;
			int index = 0;
			float currentProgress = 0.0f;
			float progressInterval = (1.0f / (float)tableProps.RecordCount) * 100.0f;
			while (recordsFound != tableProps.RecordCount)
			{
				bool deleted = TDB.TDBTableRecordDeleted(dbIndex, tableProps.Name, index);

				if (deleted)
				{
					//If this record is deleted advance the index only
					index++;
					continue;
				}

				TableRecordModel record = table.ConstructRecordModel(recordsFound);

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
									TDB.TDBFieldGetValueAsString(dbIndex, tableProps.Name, fieldProps.Name, recordsFound, ref val);
								}
								catch (Exception err)
								{
									Console.WriteLine(err.ToString());
								}
								record.RegisterField(fieldProps.Name, val);
								break;
							case TdbFieldType.tdbUInt:
								UInt32 intval;
								intval = (UInt32)TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, recordsFound);
								record.RegisterField(fieldProps.Name, (int)intval);
								break;
							case TdbFieldType.tdbSInt:
								Int32 signedval;
								signedval = TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, recordsFound);
								record.RegisterField(fieldProps.Name, signedval);
								break;
							default:
								Console.WriteLine("NOT SUPPORTED YET!!!");
								break;
						}
					}
				}

				index++;
				recordsFound++;

				currentProgress += progressInterval;
				view.updateProgress((int)currentProgress);
			}

			tableModels.Add(table.Name, table);
			Console.WriteLine("Finished processing Table: " + table.Name);
			view.updateProgress(100);
			return true;
		}

		public void Shutdown()
		{
			try
			{
				TDB.TDBClose(dbIndex);
			}
			catch (DllNotFoundException e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public ICollection<string> GetTeamNames()
		{
			if (teamNameList == null)
			{
				teamNameList = new Dictionary<int, string>();
				foreach (TableRecordModel record in tableModels[TEAM_TABLE].GetRecords())
				{
					TeamRecord teamRecord = (TeamRecord)record;
					teamNameList.Add(teamRecord.TeamId, teamRecord.Name);
				}
			}

			return teamNameList.Values;
		}

		public string GetTeamNameFromTeamId(int teamid)
		{
			if (teamNameList.ContainsKey(teamid))
				return teamNameList[teamid];
			else
				return UNKNOWN_TEAM_NAME;
		}

		public int GetTeamIdFromTeamName(string teamName)
		{
			if (teamNameList.ContainsValue(teamName))
			{
				//Theres got to be a better way to do this
				int i = 0;
				foreach (string team in teamNameList.Values)
				{
					if (team.Equals(teamName))
					{
						break;
					}
					i++;
				}

				//Now if we iterate through teamName list Key list to 'i' then
				//we have the Team id
				int j = 0;
				int id = 0;
				foreach (int key in teamNameList.Keys)
				{
					if (j == i)
					{
						id = key;
						break;
					}
					j++;
				}

				return id;
			}
			else
			{
				throw new ApplicationException("Error getting TeamID for team name " + teamName);
			}
		}

		public PlayerRecord GetPlayerRecord(int recno)
		{
			return (PlayerRecord)tableModels[PLAYER_TABLE].GetRecord(recno);
		}

		public PlayerRecord CurrentPlayerRecord
		{
			get
			{
				return (PlayerRecord)tableModels[PLAYER_TABLE].GetRecord(currentPlayerIndex);
			}
			/*set
			{
				int i=0;
				foreach (TableRecordModel rec in tableModels[PLAYER_TABLE].GetRecords())
				{
					if (rec == value)
					{
						currentPlayerIndex = i;
						break;
					}
					i++;
				}
			}*/
		}

		public PlayerRecord GetNextPlayerRecord()
		{
			PlayerRecord record = null;

			while (true)
			{
				currentPlayerIndex++;
				if (currentPlayerIndex >= tableModels[PLAYER_TABLE].RecordCount)
				{
					currentPlayerIndex = 0;
				}

				record = (PlayerRecord)tableModels[PLAYER_TABLE].GetRecord(currentPlayerIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
					{
						continue;
					}
				}
				if (currentPositionFilter != -1)
				{
					if (record.PositionId != currentPositionFilter)
					{
						continue;
					}
				}
				if (currentDraftClassFilter)
				{
					if (record.YearsPro != 0)
					{
						continue;
					}
				}
	
				//Found one
				break;
			}

			return record;
		}

		public PlayerRecord GetPreviousPlayerRecord()
		{
			PlayerRecord record = null;

			while (true)
			{
				currentPlayerIndex--;
				if (currentPlayerIndex < 0)
				{
					currentPlayerIndex = tableModels[PLAYER_TABLE].RecordCount - 1;
				}

				record = (PlayerRecord)tableModels[PLAYER_TABLE].GetRecord(currentPlayerIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
					{
						continue;
					}
				}
				if (currentPositionFilter != -1)
				{
					if (record.PositionId != currentPositionFilter)
					{
						continue;
					}
				}
				if (currentDraftClassFilter)
				{
					if (record.YearsPro != 0)
					{
						continue;
					}
				}

				//Found one
				break;
			}

			return record;
		}

		public void SetDraftClassFilter(bool use)
		{
			currentDraftClassFilter = use;
		}

		public void SetTeamFilter(string teamname)
		{
			currentTeamFilter = teamname;
		}

		public void RemoveTeamFilter()
		{
			Console.WriteLine("Removing Team filter");
			currentTeamFilter = null;
		}

		public void SetPositionFilter(int index)
		{
			currentPositionFilter = index;
		}

		public void RemovePositionFilter()
		{
			currentPositionFilter = -1;
		}

		public void Save()
		{
			//To save we have to go through every record in our models and
			//save the dirty ones
			
			//At the moment we are only saving the player table
			tableModels[PLAYER_TABLE].Save();
			tableModels[INJURY_TABLE].Save();

			this.Dirty = false;
		}

		public Dictionary<string, PlayerRecord> SearchForPlayers(String[] names)
		{
			Console.WriteLine("Starting search for " + names.ToString());
			//This is not going to be efficient.
			Dictionary<String, PlayerRecord> results = new Dictionary<String, PlayerRecord>();

			foreach (TableRecordModel record in tableModels[PLAYER_TABLE].GetRecords())
			{
				String firstname = record.GetStringField(PlayerRecord.FIRST_NAME);
				String lastname = record.GetStringField(PlayerRecord.LAST_NAME);

				String firstnameLower = firstname.ToLower();
				String lastnameLower = lastname.ToLower();

				bool gotmatch = true;
				foreach (String searchterm in names)
				{
					if ((firstnameLower.IndexOf(searchterm) == -1) && (lastnameLower.IndexOf(searchterm) == -1))
					{
						//We don't have a match
						gotmatch = false;
						break;
					}
				}
				if (gotmatch)
				{
					results.Add(lastname + ", " + firstname + "   (" + GetTeamNameFromTeamId(record.GetIntField(PlayerRecord.TEAM_ID)) + ")", (PlayerRecord)record);
				}
			}
			return results;
		}

		public InjuryRecord GetPlayersInjuryRecord(int playerId)
		{
			foreach (TableRecordModel record in tableModels[INJURY_TABLE].GetRecords())
			{
				if (record.Deleted)
				{
					continue;
				}

				InjuryRecord injuryRecord = (InjuryRecord)record;
				if (playerId == injuryRecord.PlayerId)
				{
					return injuryRecord;
				}
			}
			return null;
		}

		public InjuryRecord CreateNewInjuryRecord()
		{
			return (InjuryRecord)tableModels[INJURY_TABLE].CreateNewRecord();
		}

		public PlayerRecord CreateNewPlayerRecord()
		{
			return (PlayerRecord)tableModels[PLAYER_TABLE].CreateNewRecord();
		}
	}
}
