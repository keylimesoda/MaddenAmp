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

namespace MaddenEditor.Domain
{
	public class TableModel
	{
		protected List<TableRecordModel> recordList = null;
		protected String name;
		protected RosterModel parentModel = null;
		List<TdbFieldProperties> fieldList = null;
		protected int dbIndex = -1;
		protected string primaryKeyField = null;

		public TableModel(string name, RosterModel rosterModel, int dbIndex)
		{
			this.dbIndex = dbIndex;
			parentModel = rosterModel;
			this.name = name;
			recordList = new List<TableRecordModel>();
		}

		public void SetPrimaryKeyField(string key)
		{
			primaryKeyField = key;
		}

		public void SetFieldList(List<TdbFieldProperties> list)
		{
			fieldList = list;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public List<string> GetStringFieldList(string fieldName)
		{
			List<string> result = new List<string>();

			foreach(TableRecordModel record in recordList)
			{
				result.Add(record.GetStringField(fieldName));
			}

			return result;
		}

		public TableRecordModel GetRecord(int index)
		{
			return recordList[index];
		}

		public List<TableRecordModel> GetRecords()
		{
			return recordList;
		}

		public int RecordCount
		{
			get
			{
				return recordList.Count;
			}
		}

		public TableRecordModel CreateNewRecord()
		{
			TableRecordModel result = null;
			int newRecNo = TDB.TDBTableRecordAdd(dbIndex, name, false);
			if (newRecNo == 0xFFFF)
			{
				//We are at max capacity
				//Chuck an exception
				throw new ApplicationException("Table " + name + " has reached max capacity");
			}

			result = ConstructRecordModel(newRecNo);

			result.Dirty = true;
			parentModel.Dirty = true;

			foreach (TdbFieldProperties fieldProps in fieldList)
			{
				switch (fieldProps.FieldType)
				{
					case TdbFieldType.tdbString:

						string val = "Unassigned";
						result.RegisterField(fieldProps.Name, val);
						break;
					case TdbFieldType.tdbUInt:
						UInt32 intval = 0;
						result.RegisterField(fieldProps.Name, (int)intval);
						break;
					case TdbFieldType.tdbSInt:
						Int32 signedval = 0;
						result.RegisterField(fieldProps.Name, signedval);
						break;
					default:
						Console.WriteLine("NOT SUPPORTED YET!!!");
						break;
				}
			}

			return result;
		}

		public TableRecordModel ConstructRecordModel(int recno)
		{
			TableRecordModel newRecord = null;

			switch (name)
			{
				//case RosterModel.CITY_TABLE:
				//	newRecord = new CityRecord(recno, parentModel);
				//	break;
				//case MaddenTable.COACH_TABLE:
				//	newRecord = new CoachRecord(recno, parentModel);
				//	break;
				//case MaddenTable.CPSE_TABLE:
				//	return null;
				//case MaddenTable.CTMP_TABLE:
				//	return null;
				//case MaddenTable.CTMU_TABLE:
				//	return null;
				//case MaddenTable.DEPTH_CHART_TABLE:
				//	newRecord = new DepthChartRecord(recno, parentModel);
				//	break;
				case RosterModel.INJURY_TABLE:
					newRecord = new InjuryRecord(recno, parentModel);
					break;
				case RosterModel.PLAYER_TABLE:
					newRecord = new PlayerRecord(recno, parentModel);
					break;
				//case MaddenTable.STADIUM_TABLE:
				//	newRecord = new StadiumTable(recno, parentModel);
				//	break;
				case RosterModel.TEAM_TABLE:
					newRecord = new TeamRecord(recno, parentModel);
					break;
				//case MaddenTable.UNIFORM_TABLE:
				//	newRecord = new UniformRecord(recno, parentModel);
				//	break;
			}

			//Add the new record to our list of records
			recordList.Add(newRecord);

			return newRecord;
		}

		public void Save()
		{
			foreach (TableRecordModel record in recordList)
			{
				if (record.Dirty)
				{
					//First check to see if this record is going to be deleted
					if (record.Deleted)
					{
						//Mark record for deletion in DB
						TDB.TDBTableRecordChangeDeleted(dbIndex, name, record.RecNo, true);
						continue;
					}

					string[] keyArray = null;
					int[] valueArray = null;
					string[] stringValueArray = null;

					record.GetChangedIntFields(ref keyArray, ref valueArray);

					for (int i = 0; i < keyArray.Length; i++)
					{
						TDB.TDBFieldSetValueAsInteger(dbIndex, name, keyArray[i], record.RecNo, valueArray[i]);
					}

					keyArray = null;

					record.GetChangedStringFields(ref keyArray, ref stringValueArray);

					for (int i = 0; i < keyArray.Length; i++)
					{
						TDB.TDBFieldSetValueAsString(dbIndex, name, keyArray[i], record.RecNo, stringValueArray[i]);
					}

					record.DiscardBackups();
				}
			}

			TDB.TDBSave(dbIndex);
		}
	}
}
