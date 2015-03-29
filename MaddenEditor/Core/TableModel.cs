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
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Record.FranchiseState;

namespace MaddenEditor.Core
{
	public class TableModel
	{
		//Stores the active records
		protected List<TableRecordModel> recordList = null;
		//Stores the deleted records
		protected List<TableRecordModel> deletedRecordList = null;
		protected String name;
		protected EditorModel parentModel = null;
		List<TdbFieldProperties> fieldList = null;
		protected int dbIndex = -1;
		protected string primaryKeyField = null;

		public TableModel(string name, EditorModel EditorModel, int dbIndex)
		{
			this.dbIndex = dbIndex;
			parentModel = EditorModel;
			this.name = name;
			recordList = new List<TableRecordModel>();
			deletedRecordList = new List<TableRecordModel>();
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

			foreach (TableRecordModel record in recordList)
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

		/// <summary>
		/// This function checks to see that if the record is deleted that it is put
		/// in the deleted list of records, and if not, that it is put in the regular
		/// list
		/// </summary>
		/// <param name="record"></param>
		public void ProcessRecordDeleteness(TableRecordModel record)
		{
			if (record.Dirty)
			{
				recordList.Remove(record);

				if (!deletedRecordList.Contains(record))
				{
					deletedRecordList.Add(record);
				}
			}
			else
			{
				deletedRecordList.Remove(record);

				if (!recordList.Contains(record))
				{
					recordList.Add(record);
				}
			}
		}

		public TableRecordModel CreateNewRecord(bool allowExpand)
		{
			TableRecordModel result = null;
			int newRecNo = TDB.TDBTableRecordAdd(dbIndex, name, allowExpand);
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
						Trace.WriteLine("NOT SUPPORTED YET!!!");
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
				case EditorModel.CITY_TABLE:
					newRecord = new CityRecord(recno, this, parentModel);
					break;
				case EditorModel.COACH_TABLE:
					newRecord = new CoachRecord(recno, this, parentModel);
					break;
				case EditorModel.SALARY_CAP_TABLE:
					newRecord = new SalaryCapRecord(recno, this, parentModel);
					break;
				case EditorModel.COACH_SLIDER_TABLE:
					newRecord = new CoachPrioritySliderRecord(recno, this, parentModel);
					break;
				case EditorModel.TEAM_CAPTAIN_TABLE:
					newRecord = new TeamCaptainRecord(recno, this, parentModel);
					break;
				case EditorModel.OWNER_TABLE:
					newRecord = new OwnerRecord(recno, this, parentModel);
					break;
				case EditorModel.DEPTH_CHART_TABLE:
					newRecord = new DepthChartRecord(recno, this, parentModel);
					break;
				case EditorModel.INJURY_TABLE:
					newRecord = new InjuryRecord(recno, this, parentModel);
					break;
				case EditorModel.PLAYER_TABLE:
					newRecord = new PlayerRecord(recno, this, parentModel);
					break;
				case EditorModel.TEAM_TABLE:
					newRecord = new TeamRecord(recno, this, parentModel);
					break;
				case EditorModel.SCHEDULE_TABLE:
					newRecord = new ScheduleRecord(recno, this, parentModel);
					break;
                
                case EditorModel.STADIUM_TABLE:
                    newRecord = new StadiumRecord(recno, this, parentModel);
                    break;

				case EditorModel.UNIFORM_TABLE:
					newRecord = new UniformRecord(recno, this, parentModel);
					break;

				// MADDEN DRAFT EDIT
				case EditorModel.DRAFT_PICK_TABLE:
					newRecord = new DraftPickRecord(recno, this, parentModel);
					break;

				case EditorModel.DRAFTED_PLAYERS_TABLE:
					newRecord = new RookieRecord(recno, this, parentModel);
					break;
                case EditorModel.BOXSCORE_DEFENSE_TABLE:
                    newRecord = new BoxScoreDefenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.BOXSCORE_OFFENSE_TABLE:
                    newRecord = new BoxScoreOffenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_STATS_DEFENSE_TABLE:
                    newRecord = new CareerStatsDefenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_STATS_OFFENSE_TABLE:
                    newRecord = new CareerStatsOffenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_STATS_DEFENSE_TABLE:
                    newRecord = new SeasonStatsDefenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_STATS_OFFENSE_TABLE:
                    newRecord = new SeasonStatsOffenseRecord(recno, this, parentModel);
                    break;
                case EditorModel.TEAM_STATS_TABLE:
                    newRecord = new SeasonStatsTeamRecord(recno, this, parentModel);
                    break;
                case EditorModel.FRANCHISE_TIME_TABLE:
                    newRecord = new FranchiseTimeRecord(recno, this, parentModel);
                    break;
                case EditorModel.BOXSCORE_TEAM_TABLE:
                    newRecord = new BoxScoreTeamStats(recno, this, parentModel);
                    break;
                case EditorModel.BOXSCORE_OFFENSIVE_LINE_TABLE:
                    newRecord = new BoxScoreOffensiveLineRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE:
                    newRecord = new SeasonStatsOffensiveLineRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE:
                    newRecord = new CareerStatsOffensiveLineRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_GAMES_PLAYED_TABLE:
                    newRecord = new CareerGamesPlayedRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_GAMES_PLAYED_TABLE:
                    newRecord = new SeasonGamesPlayedRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_STATS_KICKPUNT_TABLE:
                    newRecord = new CareerPuntKickRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_STATS_KICKPUNT_TABLE:
                    newRecord = new SeasonPuntKickRecord(recno, this, parentModel);
                    break;
                case EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE:
                    newRecord = new CareerPKReturnRecord(recno, this, parentModel);
                    break;
                case EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE:
                    newRecord = new SeasonPKReturnRecord(recno, this, parentModel);
                    break;
                case EditorModel.SCOUTING_STATE_TABLE:
                    newRecord = new ScoutingStateRecord(recno, this, parentModel);
                    break;
                case EditorModel.RFA_STATE_TABLE:
                    newRecord = new RFAStateRecord(recno, this, parentModel);
                    break;
                case EditorModel.RESIGN_PLAYERS_STATE_TABLE:
                    newRecord = new ResignPlayersStateRecord(recno, this, parentModel);
                    break;
                case EditorModel.FREE_AGENCY_STATE_TABLE:
                    newRecord = new FreeAgencyStateRecord(recno, this, parentModel);
                    break;
                case EditorModel.DRAFT_STATE_TABLE:
                    newRecord = new DraftStateRecord(recno, this, parentModel);
                    break;
                case EditorModel.FRANCHISE_STAGE_TABLE:
                    newRecord = new FranchiseStageRecord(recno, this, parentModel);
                    break;
                // MADDEN DRAFT EDIT
				case EditorModel.GAME_OPTIONS_TABLE:
					newRecord = new GameOptionRecord(recno, this, parentModel);
					break;
                case EditorModel.PLAYER_AWARDS_TABLE:
                    newRecord = new Awards(recno, this, parentModel);                    
                    break;
                case EditorModel.FREE_AGENT_PLAYERS:
                    newRecord = new FreeAgentPlayers(recno, this, parentModel);
                    break;
                case EditorModel.PROGRESSION_TABLE:
                    newRecord = new ProgressionRecord(recno, this, parentModel);
                    break;                    
			}

			//Add the new record to our list of records
			recordList.Add(newRecord);            

			return newRecord;
		}

		/// <summary>
		/// This function saves the changes from the table record models to the database
		/// 
		/// These changes need to be made persistent by calling TDB.Save(). usually you don't
		/// call this function directly. Instead call EditorModel.Save() which will
		/// do it all for you.
		/// </summary>
		public void Save()
		{
			List<TableRecordModel> listToUse = null;

			for (int j = 0; j < 2; j++)
			{
				if (j == 0)
				{
					listToUse = recordList;
				}
				else
				{
					listToUse = deletedRecordList;
				}
				foreach (TableRecordModel record in listToUse)
				{
					if (record.Dirty)
					{
						//First check to see if this record is going to be deleted
						if (record.Deleted)
						{
							Trace.Write("About to mark for deletion record " + record.RecNo);
							//Mark record for deletion in DB

							TDB.TDBTableRecordChangeDeleted(dbIndex, name, record.RecNo, false);
							//TDB.TDBTableRecordRemove(dbIndex, name, record.RecNo);
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
			}

			//TDB.TDBDatabaseCompact(dbIndex);
			//TDB.TDBSave(dbIndex);
		}
	}
}
