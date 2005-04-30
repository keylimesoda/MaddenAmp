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

	public class RosterModel
	{
		private bool dirty = false;
		private int dbIndex = -1;
		private MaddenFileType fileType = MaddenFileType.RosterFile;
		private int tableCount = 0;
		private string fileName = "";
		private Dictionary<MaddenTable, TableModel> tableModels = null;

		public RosterModel(string filename)
		{
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
		}

		private bool ProcessFile()
		{
			bool result = true;
			try
			{
				tableCount = TDB.TDBDatabaseGetTableCount(dbIndex);
				Console.WriteLine("Table count in {0} = {1}", fileName, tableCount);
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

			//Get the table properties
			TdbTableProperties teamTable = new TdbTableProperties();
			teamTable.Name = new string((char)0, 5);
			TDB.TDBTableGetProperties(dbIndex, (int)tableType, ref teamTable);

			TableModel table = new TableModel(tableType, teamTable.Name);
			Console.WriteLine("Processing Table: " + table.Name);

			//For each field for this table, find the name and add it to a collection
			//so we can get each of these for each record
			List<TdbFieldProperties> fieldList = new List<TdbFieldProperties>();
			for (int i=0; i < teamTable.FieldCount; i++)
			{
				TdbFieldProperties fieldProps = new TdbFieldProperties();
				fieldProps.Name = new string((char)0, 5);
				TDB.TDBFieldGetProperties(dbIndex, teamTable.Name, i, ref fieldProps);
				//Add this field to the list
				fieldList.Add(fieldProps);
			}

			int recordsFound = 0;
			int index = 0;
			while (recordsFound != teamTable.RecordCount)
			{
				bool deleted = TDB.TDBTableRecordDeleted(dbIndex, teamTable.Name, index);

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
									TDB.TDBFieldGetValueAsString(dbIndex, teamTable.Name, fieldProps.Name, recordsFound, ref val);
								}
								catch (Exception err)
								{
									Console.WriteLine(err.ToString());
								}
								record.SetField(fieldProps.Name, val);
								break;
							case TdbFieldType.tdbUInt:
								int intval;
								intval = TDB.TDBFieldGetValueAsInteger(dbIndex, teamTable.Name, fieldProps.Name, recordsFound);
								record.SetField(fieldProps.Name, intval);
								break;
							case TdbFieldType.tdbSInt:
								int shortval;
								shortval = TDB.TDBFieldGetValueAsInteger(dbIndex, teamTable.Name, fieldProps.Name, recordsFound);
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
			}

			tableModels.Add(tableType, table);
			Console.WriteLine("Finished processing Table: " + table.Name);
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
	}
}
