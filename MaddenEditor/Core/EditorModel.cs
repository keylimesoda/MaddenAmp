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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public enum MaddenFileType 
	{ 
		RosterFile, 
		FranchiseFile 
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

	/// <summary>
	/// This class is the main application model class. It is responsible for
	/// creating all editing models that are manipulated by the GUI.
	/// 
	/// </summary>
	public class EditorModel
	{
		public const int FREE_AGENT_TEAM_ID = 1009;
		public const string UNKNOWN_TEAM_NAME = "UNKNOWN_TEAM";
		public const string PLAYER_TABLE = "PLAY";
		public const string TEAM_TABLE = "TEAM";
		public const string INJURY_TABLE = "INJY";
		public const string COACH_TABLE = "COCH";
		public const string SALARY_CAP_TABLE = "SLRI";

		private bool dirty = false;
		private int dbIndex = -1;
		private int tableCount = 0;
		private string fileName = "";
		private MainForm view = null;
		private Dictionary<string, TableModel> tableModels = null;
		private MaddenFileType fileType = MaddenFileType.RosterFile;
		private Dictionary<string, int> tableOrder = null;
		/** Editing Models */
		private PlayerEditingModel playerEditingModel = null;
		private CoachEditingModel coachEditingModel = null;
		private TeamEditingModel teamEditingModel = null;
		private SalaryCapRecord salaryCapRecord = null;

		public EditorModel(string filename, MainForm form)
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

		public MaddenFileType FileType
		{
			get
			{
				return fileType;
			}
		}

		public PlayerEditingModel PlayerModel
		{
			get
			{
				return playerEditingModel;
			}
		}

		public CoachEditingModel CoachModel
		{
			get
			{
				return coachEditingModel;
			}
		}

		public TeamEditingModel TeamModel
		{
			get
			{
				return teamEditingModel;
			}
		}

		public SalaryCapRecord SalaryCapModel
		{
			get
			{
				return salaryCapRecord;
			}
		}

		public Dictionary<string, TableModel> TableModels
		{
			get
			{
				return tableModels;
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

				if (tableCount == 11)
				{
					fileType = MaddenFileType.RosterFile;
				}
				else
				{
					fileType = MaddenFileType.FranchiseFile;
				}

				//Initialise the tableOrder with the Table names we want to 
				//Process
				tableOrder.Add(TEAM_TABLE, -1);
				tableOrder.Add(PLAYER_TABLE, -1);
				tableOrder.Add(INJURY_TABLE, -1);
				tableOrder.Add(COACH_TABLE, -1);
				if (fileType == MaddenFileType.FranchiseFile)
				{
					tableOrder.Add(SALARY_CAP_TABLE, -1);
				}

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

			tableModels[PLAYER_TABLE].Save();
			tableModels[INJURY_TABLE].Save();
			tableModels[COACH_TABLE].Save();
			tableModels[TEAM_TABLE].Save();

			this.Dirty = false;
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

		
	}
}
