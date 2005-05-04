using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class PlayerEditingModel
	{
		Dictionary<string, TableModel> tableModels = null;
		private Dictionary<int, string> teamNameList = null;
		private int currentPlayerIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** The current position filter */
		private int currentPositionFilter = -1;
		/** If we are currently filtering for draft class */
		private bool currentDraftClassFilter = false;

		public PlayerEditingModel(Dictionary<string, TableModel> tableModels)
		{
			this.tableModels = tableModels;
		}

		public ICollection<string> GetTeamNames()
		{
			if (teamNameList == null)
			{
				teamNameList = new Dictionary<int, string>();
				foreach (TableRecordModel record in tableModels[EditorModel.TEAM_TABLE].GetRecords())
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
				return EditorModel.UNKNOWN_TEAM_NAME;
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
			return (PlayerRecord)tableModels[EditorModel.PLAYER_TABLE].GetRecord(recno);
		}

		public PlayerRecord CurrentPlayerRecord
		{
			get
			{
				return (PlayerRecord)tableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);
			}
		}

		public PlayerRecord GetNextPlayerRecord()
		{
			PlayerRecord record = null;

			while (true)
			{
				currentPlayerIndex++;
				if (currentPlayerIndex >= tableModels[EditorModel.PLAYER_TABLE].RecordCount)
				{
					currentPlayerIndex = 0;
				}

				record = (PlayerRecord)tableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

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
					currentPlayerIndex = tableModels[EditorModel.PLAYER_TABLE].RecordCount - 1;
				}

				record = (PlayerRecord)tableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

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

		public Dictionary<string, PlayerRecord> SearchForPlayers(String[] names)
		{
			Console.WriteLine("Starting search for " + names.ToString());
			//This is not going to be efficient.
			Dictionary<String, PlayerRecord> results = new Dictionary<String, PlayerRecord>();

			foreach (TableRecordModel record in tableModels[EditorModel.PLAYER_TABLE].GetRecords())
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
			foreach (TableRecordModel record in tableModels[EditorModel.INJURY_TABLE].GetRecords())
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
			return (InjuryRecord)tableModels[EditorModel.INJURY_TABLE].CreateNewRecord();
		}

		public PlayerRecord CreateNewPlayerRecord()
		{
			return (PlayerRecord)tableModels[EditorModel.PLAYER_TABLE].CreateNewRecord();
		}
	}
}
