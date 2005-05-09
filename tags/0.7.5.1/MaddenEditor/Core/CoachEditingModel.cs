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
	public class CoachEditingModel
	{
		/** The current Coach index */
		private int currentCoachIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** The current position filter */
		private int currentPositionFilter = -1;
		/** Reference to our EditorModel */
		private EditorModel model = null;

		public CoachEditingModel(EditorModel model)
		{
			this.model = model;
		}

		public CoachRecord CurrentCoachRecord
		{
			get
			{
				return (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(currentCoachIndex);
			}
			set
			{
				CoachRecord curr = value;
				//need to set currenPlayerIndex to the correct index
				int index = 0;
				foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
				{
					if (curr == rec)
					{
						currentCoachIndex = index;
						break;
					}

					index++;
				}
			}
		}

		public CoachRecord GetNextCoachRecord()
		{
			CoachRecord record = null;

			int startingindex = currentCoachIndex;
			bool found = false;
			while (true)
			{
				currentCoachIndex++;
				if (currentCoachIndex == startingindex && found == false)
				{
					//We have looped around
					return null;
				}

				if (currentCoachIndex >= model.TableModels[EditorModel.COACH_TABLE].RecordCount)
				{
					currentCoachIndex = 0;
				}

				record = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(currentCoachIndex);

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
					if (record.Position != currentPositionFilter)
					{
						continue;
					}
				}

				found = true;
				//Found one
				break;
			}

			return record;
		}

		public CoachRecord GetPreviousCoachRecord()
		{
			CoachRecord record = null;

			int startingindex = currentCoachIndex;
			while (true)
			{
				currentCoachIndex--;
				if (currentCoachIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentCoachIndex < 0)
				{
					currentCoachIndex = model.TableModels[EditorModel.COACH_TABLE].RecordCount - 1;
				}

				record = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(currentCoachIndex);

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
					if (record.Position != currentPositionFilter)
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

		public Dictionary<string, TableRecordModel> SearchForCoaches(String[] names)
		{
			Console.WriteLine("Starting search for " + names.ToString());
			//This is not going to be efficient.
			Dictionary<String, TableRecordModel> results = new Dictionary<String, TableRecordModel>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
			{
				String firstname = record.GetStringField(CoachRecord.NAME);

				String firstnameLower = firstname.ToLower();

				bool gotmatch = true;
				foreach (String searchterm in names)
				{
					if (firstnameLower.IndexOf(searchterm) == -1)
					{
						//We don't have a match
						gotmatch = false;
						break;
					}
				}
				if (gotmatch)
				{
					String key = firstname + "   (" + model.TeamModel.GetTeamNameFromTeamId(record.GetIntField(CoachRecord.TEAM_ID)) + ")";
					String addkey = key;
					int count = 1;
					while (results.ContainsKey(addkey))
					{
						addkey = key + "(" + count++ + ")";
					}
					results.Add(addkey, (CoachRecord)record);
				}
			}
			return results;
		}

		public SortedList<int, CoachPrioritySliderRecord> GetCurrentCoachSliders()
		{
			SortedList<int, CoachPrioritySliderRecord> results = new SortedList<int, CoachPrioritySliderRecord>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_SLIDER_TABLE].GetRecords())
			{
				if (record.Deleted)
				{
					continue;
				}

				CoachPrioritySliderRecord sliderRecord = (CoachPrioritySliderRecord)record;
				if (sliderRecord.CoachId == model.CoachModel.CurrentCoachRecord.CoachId)
				{
					results.Add(sliderRecord.PositionId, sliderRecord);
				}
			}

			return results;
		}

		

	}
}
