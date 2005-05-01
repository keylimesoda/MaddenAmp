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
 * http://gommo.homelinux.net             colin.goudie@gmail.com
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

	public enum MaddenTable
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
	}

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
		private bool dirty = false;
		private int dbIndex = -1;
		private MaddenFileType fileType = MaddenFileType.RosterFile;
		private int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private Dictionary<MaddenTable, TableModel> tableModels = null;
		private Dictionary<int, string> teamNameList = null;
		private int currentPlayerIndex = 0;
		private string currentTeamFilter = null;
		private int currentPositionFilter = -1;

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

			tableModels = new Dictionary<MaddenTable, TableModel>();

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

		public TableModel GetTable(MaddenTable tableType)
		{
			return tableModels[tableType];
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
				}
			}
			catch (DllNotFoundException e)
			{
				Console.WriteLine(e.ToString());
			}

			if (tableCount != Enum.GetValues(typeof(MaddenTable)).Length)
			{
				Console.WriteLine("Something is wrong, we don't have enough tables");
				return false;
			}

			foreach (MaddenTable madtab in Enum.GetValues(typeof(MaddenTable)))
			{
				result &= ProcessTable(madtab);
			}

			return result;
		}

		private bool ProcessTable(MaddenTable tableType)
		{
			//Reset the progress bar
			view.updateProgress(0);

			//Get the table properties
			TdbTableProperties tableProps = new TdbTableProperties();
			tableProps.Name = new string((char)0, 5);
			TDB.TDBTableGetProperties(dbIndex, (int)tableType, ref tableProps);

			TableModel table = new TableModel(tableType, tableProps.Name, this);
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

				TableRecordModel record = table.CreateRecord(recordsFound);

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
								record.SetField(fieldProps.Name, val);
								break;
							case TdbFieldType.tdbUInt:
								int intval;
								intval = TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, recordsFound);
								record.SetField(fieldProps.Name, intval);
								break;
							case TdbFieldType.tdbSInt:
								int shortval;
								shortval = TDB.TDBFieldGetValueAsInteger(dbIndex, tableProps.Name, fieldProps.Name, recordsFound);
								record.SetField(fieldProps.Name, shortval);
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

			tableModels.Add(tableType, table);
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
				foreach (TableRecordModel record in tableModels[MaddenTable.TEAM_TABLE].GetRecords())
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
				return "UNKNOWNTEAM";
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
			return (PlayerRecord)tableModels[MaddenTable.PLAYER_TABLE].GetRecord(recno);
		}

		public PlayerRecord CurrentPlayerRecord
		{
			get
			{
				return (PlayerRecord)tableModels[MaddenTable.PLAYER_TABLE].GetRecord(currentPlayerIndex);
			}
		}

		public PlayerRecord GetNextPlayerRecord()
		{
			PlayerRecord record = null;

			while (true)
			{
				currentPlayerIndex++;
				if (currentPlayerIndex >= tableModels[MaddenTable.PLAYER_TABLE].RecordCount)
				{
					currentPlayerIndex = 0;
				}

				record = (PlayerRecord)tableModels[MaddenTable.PLAYER_TABLE].GetRecord(currentPlayerIndex);

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
					currentPlayerIndex = tableModels[MaddenTable.PLAYER_TABLE].RecordCount - 1;
				}

				record = (PlayerRecord)tableModels[MaddenTable.PLAYER_TABLE].GetRecord(currentPlayerIndex);

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

				//Found one
				break;
			}

			return record;
		}

		public void SetTeamFilter(string teamname)
		{
			currentTeamFilter = teamname;
		}

		public void RemoveTeamFilter()
		{
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
			TableModel table = tableModels[MaddenTable.PLAYER_TABLE];

			List<TableRecordModel> listRecords = table.GetRecords();

			foreach (TableRecordModel record in listRecords)
			{
				if (record.Dirty)
				{
					string[] keyArray = null;
					int[] valueArray = null;
					string[] stringValueArray = null;

					record.GetChangedIntFields(ref keyArray, ref valueArray);

					for (int i = 0; i < keyArray.Length; i++)
					{
						TDB.TDBFieldSetValueAsInteger(dbIndex, table.Name, keyArray[i], record.RecNo, valueArray[i]);
					}

					keyArray = null;

					record.GetChangedStringFields(ref keyArray, ref stringValueArray);

					for (int i = 0; i < keyArray.Length; i++)
					{
						TDB.TDBFieldSetValueAsString(dbIndex, table.Name, keyArray[i], record.RecNo, stringValueArray[i]);
					}

					record.DiscardBackups();
				}
			}

			TDB.TDBSave(dbIndex);

			this.Dirty = false;
		}
	}
}
