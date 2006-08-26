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
using System.Windows.Forms;

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
			while (true)
			{
				currentCoachIndex++;
				if (currentCoachIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentCoachIndex >= model.TableModels[EditorModel.COACH_TABLE].RecordCount)
				{
					currentCoachIndex = -1;
					continue;
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
					currentCoachIndex = model.TableModels[EditorModel.COACH_TABLE].RecordCount;
					continue;
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
			Trace.WriteLine("Removing Team filter");
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
			Trace.WriteLine("Starting search for " + names.ToString());
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
					try
					{
						results.Add(sliderRecord.PositionId, sliderRecord);
					}
					catch (System.ArgumentException e)
					{
						Trace.Write("Key already exists for coach sliders\n\r" + e.ToString());
					}
				}
			}

			return results;
		}

		/// <summary>
		/// Changes the current coaches position to the new position. This method
		/// also ensures that other table are updated. The important table to change
		/// is the CPSE or Coach Slider table. 
		/// </summary>
		/// <param name="newPosition">The new position of the coach</param>
		/// <returns>Returns false if we cannot correctly update this coaches position</returns>
		public bool ChangeCoachPosition(MaddenCoachPosition newPosition)
		{
			bool result = true;
			if (CurrentCoachRecord.Position == (int)newPosition)
			{
				//Don't worry if we are changing it to the same position
				return true;
			}

			if (newPosition == MaddenCoachPosition.HeadCoach)
			{
				//We need to make sure we have values for this coach in our
				//coach position slider table

				//We need to see if we have values for this coachid in the CPSE table.
				//There also should be CoachRecord.NUMBER_OF_COACHING_POSITIONS
				int count = 0;
				foreach (TableRecordModel recordModel in model.TableModels[EditorModel.COACH_SLIDER_TABLE].GetRecords())
				{
					if (recordModel.Deleted)
					{
						continue;
					}

					CoachPrioritySliderRecord record = (CoachPrioritySliderRecord)recordModel;

					if (CurrentCoachRecord.CoachId == record.CoachId)
					{
						count++;
					}

					if (count == CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS)
					{
						break;
					}
				}

				if (count == 0)
				{
					//There is no values for this coach
					for (int i = 0; i < CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS; i++)
					{
						try
						{
							CoachPrioritySliderRecord record = (CoachPrioritySliderRecord)model.TableModels[EditorModel.COACH_SLIDER_TABLE].CreateNewRecord(false);

							record.CoachId = CurrentCoachRecord.CoachId;
							record.PositionId = i;
							record.Priority = 50;
							record.PriorityType = 0;
						}
						catch (ApplicationException e)
						{
							MessageBox.Show("Error creating record when changing coach position:\r\n" + e.ToString(), "Exception Changing Coach Position", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return false;
						}
					}
					MessageBox.Show("You just moved a coordinator to a Head Coach.\r\nDefault values were assigned to his coaching priorities", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (count > 0 && count < CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS)
				{
					//There are some values for this coach but not enough, we'll have to fix this
					//At the moment return false;
					MessageBox.Show("You just moved a coordinator to a Head Coach.\r\nHowever, there was a problem setting his priority values", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				else if (count == CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS)
				{
					//We have values for this coach so don't worry about it
				}
			}

			if (result)
			{
				//Update the record
				CurrentCoachRecord.Position = (int)newPosition;
			}


			return result;
		}

	}
}
