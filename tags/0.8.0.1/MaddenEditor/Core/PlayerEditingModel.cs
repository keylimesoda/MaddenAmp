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

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class PlayerEditingModel
	{
		private int currentPlayerIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** The current position filter */
		private int currentPositionFilter = -1;
		/** If we are currently filtering for draft class */
		private bool currentDraftClassFilter = false;
		/** Reference to our EditorModel */
		private EditorModel model = null;

		public PlayerEditingModel(EditorModel model)
		{
			this.model = model;
		}
		
		public PlayerRecord GetPlayerRecord(int recno)
		{
			return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(recno);
		}

		public PlayerRecord GetPlayerByPlayerId(int playerId)
		{
			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (((PlayerRecord)record).PlayerId == playerId)
				{
					return (PlayerRecord)record;
				}
			}
			return null;
		}

		public PlayerRecord CurrentPlayerRecord
		{
			get
			{
				return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);
			}
			set
			{
				PlayerRecord curr = value;
				//need to set currenPlayerIndex to the correct index
				int index = 0;
				foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
				{
					if (curr == rec)
					{
						currentPlayerIndex = index;
						break;
					}

					index++;
				}
			}
		}

		public PlayerRecord GetNextPlayerRecord()
		{
			PlayerRecord record = null;

			while (true)
			{
				currentPlayerIndex++;
				if (currentPlayerIndex >= model.TableModels[EditorModel.PLAYER_TABLE].RecordCount)
				{
					currentPlayerIndex = 0;
				}

				record = (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(model.TeamModel.GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
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
					currentPlayerIndex = model.TableModels[EditorModel.PLAYER_TABLE].RecordCount - 1;
				}

				record = (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(model.TeamModel.GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
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

		public Dictionary<string, TableRecordModel> SearchForPlayers(String[] names)
		{
			Console.WriteLine("Starting search for " + names.ToString());
			//This is not going to be efficient.
			Dictionary<String, TableRecordModel> results = new Dictionary<String, TableRecordModel>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
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
					String key = lastname + ", " + firstname + "   (" + model.TeamModel.GetTeamNameFromTeamId(record.GetIntField(PlayerRecord.TEAM_ID)) + ")";
					String addkey = key;
					int count = 1;
					while(results.ContainsKey(addkey))
					{
						addkey = key + "(" + count++ + ")";
					}
					results.Add(addkey, (PlayerRecord)record);
				}
			}
			return results;
		}

		public InjuryRecord GetPlayersInjuryRecord(int playerId)
		{
			foreach (TableRecordModel record in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
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
			return (InjuryRecord)model.TableModels[EditorModel.INJURY_TABLE].CreateNewRecord(false);
		}

		public PlayerRecord CreateNewPlayerRecord()
		{
			return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].CreateNewRecord(false);
		}
	}
}
