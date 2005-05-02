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

namespace MaddenEditor.Domain
{
	public class TableModel
	{
		protected List<TableRecordModel> recordList = null;
		protected MaddenTable type;
		protected String name;
		protected RosterModel parentModel = null;

		public TableModel(MaddenTable tableType, string name, RosterModel rosterModel)
		{
			parentModel = rosterModel;
			type = tableType;
			this.name = name;
			recordList = new List<TableRecordModel>();
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

		public TableRecordModel CreateRecord(int recno)
		{
			TableRecordModel newRecord = null;

			switch (type)
			{
				case MaddenTable.CITY_TABLE:
					newRecord = new CityRecord(recno, parentModel);
					break;
				case MaddenTable.COACH_TABLE:
					newRecord = new CoachRecord(recno, parentModel);
					break;
				case MaddenTable.CPSE_TABLE:
					return null;
				case MaddenTable.CTMP_TABLE:
					return null;
				case MaddenTable.CTMU_TABLE:
					return null;
				case MaddenTable.DEPTH_CHART_TABLE:
					newRecord = new DepthChartRecord(recno, parentModel);
					break;
				case MaddenTable.INJURY_TABLE:
					newRecord = new InjuryRecord(recno, parentModel);
					break;
				case MaddenTable.PLAYER_TABLE:
					newRecord = new PlayerRecord(recno, parentModel);
					break;
				case MaddenTable.STADIUM_TABLE:
					newRecord = new StadiumTable(recno, parentModel);
					break;
				case MaddenTable.TEAM_TABLE:
					newRecord = new TeamRecord(recno, parentModel);
					break;
				case MaddenTable.UNIFORM_TABLE:
					newRecord = new UniformRecord(recno, parentModel);
					break;
			}

			//Add the new record to our list of records
			recordList.Add(newRecord);

			return newRecord;
		}
	}
}
