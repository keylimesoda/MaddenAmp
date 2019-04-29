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

using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{

/*      Current Scheduling Formula

            AFC East 	AFC North 	AFC South 	AFC West
1st Place 	Patriots 	Bengals 	Colts 		Broncos
2nd Place 	Jets 		Steelers 	Titans 		Chiefs
3rd Place 	Dolphins 	Ravens 		Jaguars 	Chargers
4th Place 	Bills 		Browns 		Texans 		Raiders
		
            NFC East 	NFC North 	NFC South 	NFC West
1st Place 	Eagles 		Packers 	Panthers 	Seahawks
2nd Place 	Cowboys 	Bears 		Saints 		49ers
3rd Place 	Giants 		Lions 		Falcons 	Cardinals
4th Place 	Redskins 	Vikings 	Buccaneers 	Rams


This chart of the 2013 season standings displays an application of the NFL scheduling formula. The Seahawks in 2013 finished in first place in the NFC West. 
Thus, in 2014, the Seahawks will play two games against each of the teams in it's division.  One game against each team in the NFC East and AFC West
and one game each against the first-place finishers in the NFC North and NFC South.

Currently, the thirteen opponents each team faces over the 16-game regular season schedule are set using a pre-determined formula:[3]

Each team plays twice against each of the other three teams in its division: once at home, and once on the road (six games).

Each team plays once against each of the four teams from another division within its own conference, with the assigned division based on a three-year rotation: two at home, and two on the road 
(four games).

Each team plays once against one team from each of the other two divisions within its conference, based on the final division standings from the prior season: one at home, one on the road (two games).
Each team plays once against each of the four teams from a division in the other conference, with the assigned division based on a four-year rotation: two at home, and two on the road (four games).

This schedule was designed so all teams are guaranteed to play every other team in their own conference at least once every three years, and to play every team in the other conference exactly once every
four years. Additionally, the schedule guarantees that each team will both host and visit every other team within its conference at least once every six years, and will host and visit every team in the 
other conference exactly once every eight years. Finally, it guarantees a similar schedule for every team in a division each season, as all four teams will play fourteen out of their sixteen games against 
common opponents or each other.

Although this scheduling formula determines each of the thirty-two teams' respective opponents, the league usually does not release the final regular schedule with specific dates and times until the 
spring; the NFL needs several months to coordinate the entire season schedule so that, among other reasons, games are worked around various scheduling conflicts, and that it helps maximize TV ratings.
 
*/  


    public class ScheduleEditingModel
	{
		// I dont like doing this but this will mean a team has not been decided yet
		public const string UNDECIDED_TEAM = "T.B.A.";
		// Number of weeks in an NFL schedule
		public const int NUMBER_OF_WEEKS = 22;
		// Playoff week starting
		public const int PLAYOFF_WEEK = 17;
		// Reference to our editor model
		private EditorModel model = null;
		// This collection holds our schedule (the index is week number)
		private Dictionary<int, SortedList<int, ScheduleRecord>> schedule = null;
		// The maximum week number (starting from 1)
		private int maxWeekNumber;

		public ScheduleEditingModel(EditorModel model)
		{
			this.model = model;
			int currentMaxWeek = 0;

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
					//Check to ensure we aren't adding records with higher than NUMBER_OF_WEEKS weeks
					if (scheduleRecord.WeekNumber >= schedule.Count)
					{
						//Don't add this week
						return;
					}
					//Store the maximum week for this file so we don't try edit past it
					if (scheduleRecord.WeekNumber >= currentMaxWeek)
					{
						currentMaxWeek = scheduleRecord.WeekNumber;
					}
					SortedList<int, ScheduleRecord> list = schedule[scheduleRecord.WeekNumber + 1];
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
					Trace.WriteLine(err.ToString());
				}
			}

			//Make it based from 0
			maxWeekNumber = currentMaxWeek + 1;
		}

		public IList<ScheduleRecord> GetWeek(int weeknumber)
		{
			if (weeknumber > NUMBER_OF_WEEKS || weeknumber < 1)
			{
				return null;
			}

			return schedule[weeknumber].Values;
		}

		/// <summary>
		/// Returns the maximum week number for this franchise based from week 1
		/// </summary>
		/// <returns></returns>
		public int MaxWeek
		{
			get { return maxWeekNumber; }
		}
	}
}
