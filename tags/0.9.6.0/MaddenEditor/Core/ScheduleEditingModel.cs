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
		// This collection holds our schedule
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
				try
				{
					ScheduleRecord scheduleRecord = (ScheduleRecord)rec;
					
					SortedList<int, ScheduleRecord> list = schedule[scheduleRecord.WeekNumber+1];
					list.Add(scheduleRecord.GameNumber, scheduleRecord);
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
