/******************************************************************************
 * Gommo's Madden Editor
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
	public class ScheduleEditingModel
	{
		public const int NUMBER_OF_WEEKS = 17;
		// Reference to our editor model
		private EditorModel model = null;
		// This collection holds our schedule (the index is week number)
		private Dictionary<int, SortedList<int, ScheduleRecord>> schedule = null;

		public ScheduleEditingModel(EditorModel model)
		{
			this.model = model;

			//Create and initialise our schedule collection
			schedule = new Dictionary<int, SortedList<int, ScheduleRecord>>();
			for (int i = 1; i <= NUMBER_OF_WEEKS; i++)
			{
				SortedList<int, ScheduleRecord> list = new SortedList<int, ScheduleRecord>();
				schedule.Add(i, list);
			}

			//Now add the records to these collections
			foreach (TableRecordModel rec in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
			{
				ScheduleRecord scheduleRecord = (ScheduleRecord)rec;
				try
				{
					//Check to ensure we aren't adding records with higher than 17 weeks
					if (scheduleRecord.WeekNumber >= schedule.Count)
					{
						//Don't add this week
						return;
					}
					SortedList<int, ScheduleRecord> list = schedule[scheduleRecord.WeekNumber+1];
					try
					{
						list.Add(scheduleRecord.GameNumber, scheduleRecord);
					}
					catch (ArgumentException err2)
					{
						//Something is wrong with this schedule
						throw err2;						
					}
				}
				catch (KeyNotFoundException err)
				{
					Console.WriteLine(err.ToString());
				}
			}
			
		}

		public IList<ScheduleRecord> GetWeek(int weeknumber)
		{
			if (weeknumber > NUMBER_OF_WEEKS || weeknumber < 1)
			{
				return null;
			}

			return schedule[weeknumber].Values;			
		}
	}
}
