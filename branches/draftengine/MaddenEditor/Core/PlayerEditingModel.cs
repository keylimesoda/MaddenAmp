/******************************************************************************
 * Gommo's Madden Editor
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
 * maddeneditor@tributech.com.au
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
		/** Lists of hardcoded values */
		private IList<GenericRecord> helmetStyleList = null;

		public PlayerEditingModel(EditorModel model)
		{
			this.model = model;

			//Initialise the GenericRecord lists
			helmetStyleList = new List<GenericRecord>();

			helmetStyleList.Add(new GenericRecord("Style 1", 0));
			helmetStyleList.Add(new GenericRecord("Style 2", 1));
			helmetStyleList.Add(new GenericRecord("Style 3", 2));
			if (model.FileVersion < MaddenFileVersion.Ver2006)
			{
				helmetStyleList.Add(new GenericRecord("Revolution", 3));
			}
			else
			{
				helmetStyleList.Add(new GenericRecord("Schutt DNA", 3));
				helmetStyleList.Add(new GenericRecord("Revolution", 4));
			}
		}

        // MADDEN DRAFT EDIT
/*
 * ******  DEPRECATED *******
 * 
 * private LocalMath math = new LocalMath();

        public void ComputeEffectiveOVRs(DraftModel dm)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord rec = (PlayerRecord)record;

                if (model.TeamModel.GetTeamRecord(rec.TeamId) == null || model.TeamModel.GetTeamRecord(rec.TeamId).TeamType != 0) { continue; }

                rec.EffectiveOVR = rec.Overall + math.theta(5 - rec.YearsPro) * (5 - rec.YearsPro) * (5 - model.TeamModel.GetTeamRecord(rec.TeamId).CON) / 2
                    + math.injury(rec.Injury, dm.positionData[rec.PositionId].DurabilityNeed);

                rec.Value = LocalMath.ValueScale * dm.positionData[rec.PositionId].Value(model.TeamModel.GetTeamRecord(rec.TeamId).DefensiveSystem) * math.valcurve(rec.EffectiveOVR);
            }
        }
 * */
        // MADDEN DRAFT EDIT
		
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

			int startingindex = currentPlayerIndex;
			while (true)
			{
				currentPlayerIndex++;
				if (currentPlayerIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentPlayerIndex >= model.TableModels[EditorModel.PLAYER_TABLE].RecordCount)
				{
					currentPlayerIndex = -1;
					continue;
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

			int startingindex = currentPlayerIndex;
			while (true)
			{
				currentPlayerIndex--;
				if (currentPlayerIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentPlayerIndex < 0)
				{
					currentPlayerIndex = model.TableModels[EditorModel.PLAYER_TABLE].RecordCount;
					continue;
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

		public void ChangePlayersTeam(TeamRecord newTeam)
		{
			//Don't do anything if the team is same as the current players team
			if (CurrentPlayerRecord.TeamId != newTeam.TeamId)
			{
				CurrentPlayerRecord.TeamId = newTeam.TeamId;
				//Also have to ensure we update this players injuries in the injury table
				//and remove this player form any depth charts
				foreach (TableRecordModel record in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
				{
					if (record.Deleted)
						continue;

					//If this injury record is for this player then update its team field
					InjuryRecord injRecord = (InjuryRecord)record;

					if (injRecord.PlayerId == CurrentPlayerRecord.PlayerId)
					{
						injRecord.TeamId = newTeam.TeamId;
					}
				}

				RemovePlayerFromDepthChart(CurrentPlayerRecord.PlayerId);
			}
		}

		/// <summary>
		/// TODO:
		/// This method should be put into the depth chart editing model at some stage.
		/// The hole depth chart editing functionality has too much logic in the form objects
		/// and it needs to be moved into the depth chart editing model
		/// </summary>
		/// <param name="playerId"></param>
		private void RemovePlayerFromDepthChart(int playerId)
		{
			List<DepthChartRecord> oldDepthChartRecords = new List<DepthChartRecord>();

			//Now at the moment we are just going to remove him from all depth charts
			foreach (TableRecordModel record in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
			{
				if (record.Deleted)
					continue;

				DepthChartRecord depthRecord = (DepthChartRecord)record;

				if (depthRecord.PlayerId == playerId)
				{
					depthRecord.SetDeleteFlag(true);
					//Now record the position and team and depth cause we want to fix up
					//the other players ordering in that same position
					oldDepthChartRecords.Add(depthRecord);
				}
			}

			//Now we have a list of the old depth charts that this player belongs too, we need to fix each 
			//one up. This is not going to be very efficient :)

			foreach (DepthChartRecord record in oldDepthChartRecords)
			{
				foreach (TableRecordModel depthChartRec in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
				{
					if (depthChartRec.Deleted)
						continue;

					DepthChartRecord depthRecord = (DepthChartRecord)depthChartRec;

					if (depthRecord.TeamId == record.TeamId && depthRecord.PositionId == record.PositionId)
					{
						if (depthRecord.DepthOrder > record.DepthOrder)
						{
							depthRecord.DepthOrder--;
						}

						//TODO: We could probably exit early after we found like 6 or something
						//records cause thats the maximum depth chart level anyway. but we'll try this
						//first
					}
				}
			}
		}

		public void DeletePlayerRecord(PlayerRecord record)
		{
			//Mark this record for deletion
			record.SetDeleteFlag(true);

			//Remove this player from any depth charts
			RemovePlayerFromDepthChart(record.PlayerId);
		}

		public IList<GenericRecord> HelmetStyleList
		{
			get
			{
				return helmetStyleList;
			}
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
