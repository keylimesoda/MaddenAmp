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

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class CoachEditingModel
	{
		/** The current Coach index */
        private int currentCoachIndex = 0;
        
        // Current coachid
		private int currentCoachID = 0;
        //private CoachRecord CurrentCoachRecord = null;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** The current position filter */
		private int currentPositionFilter = -1;
		/** Reference to our EditorModel */
		private EditorModel model = null;
        private MGMT _manager;

        private List<List<CoachRecord>> TeamCoaches = null;        
        private List<CoachRecord> employed = new List<CoachRecord>();        
        private IList<GenericRecord> coachskincolor = null;

        private List<int> coachlist = new List<int>();
        public Dictionary<int, string> CoachNames = new Dictionary<int, string>();
        public Dictionary<int, string> Duplicates = new Dictionary<int, string>();
        public int FilterCoachTeam = -1;
        public int FilterCoachPosition = -1;
        public int currentstreamedID = -1;

        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

		public CoachEditingModel(EditorModel model)
		{
			this.model = model;

            coachskincolor = new List<GenericRecord>();
            coachskincolor.Add(new GenericRecord("Light", 0));
            coachskincolor.Add(new GenericRecord("Medium", 1));
            coachskincolor.Add(new GenericRecord("Dark", 2));
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
            {
                coachskincolor.Add(new GenericRecord("Medium Dark 1", 3));
                coachskincolor.Add(new GenericRecord("Medium Dark 2", 4));
                coachskincolor.Add(new GenericRecord("Medium Dark 3", 7));
            }
		}

        public void InitCoachList()
        {
            if (coachlist == null)
                coachlist = new List<int>();
            else coachlist.Clear();
            if (CoachNames == null)
                CoachNames = new Dictionary<int, string>();
            else CoachNames.Clear();
            Duplicates.Clear();
            currentstreamedID = -1;
            currentCoachID = -1;

            if (FilterCoachTeam == -2)
            {
                if (manager.stream_model == null)
                    return;
                foreach (CoachCollection coll in manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                {
                    if (coll.Deleted)
                        continue;
                    else if (coll.TeamId == 1023 && !coachlist.Contains(coll.CoachId))
                        coachlist.Add(coll.CoachId);
                    else continue;
                }

                coachlist.Sort();
                foreach (int i in coachlist)
                {
                    foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;

                        CoachCollection coachcoll = (CoachCollection)rec;
                        if (i == coachcoll.CoachId && !CoachNames.ContainsKey(coachcoll.CoachId))
                            CoachNames.Add(coachcoll.CoachId, coachcoll.Name);
                    }
                }

                bool stop = true;
            }

            else
            {
                foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    else
                    {
                        bool add = false;
                        CoachRecord coach = (CoachRecord)rec;

                        if (FilterCoachTeam == -1 && FilterCoachPosition == -1)
                            add = true;

                        else if (FilterCoachPosition == -1 || FilterCoachTeam == -1)
                        {
                            if (FilterCoachPosition == -1 && FilterCoachTeam == coach.TeamId)
                                add = true;

                            else if (FilterCoachTeam == -1 && FilterCoachPosition == coach.Position)
                            {
                                //if (coach.TeamId >=0 && coach.TeamId <= 31)   // Most coaches are listed as head coaches
                                add = true;
                            }
                            else continue;
                        }

                        else if (FilterCoachPosition == coach.Position && FilterCoachTeam == coach.TeamId)
                            add = true;

                        else continue;

                        if (add)
                        {
                            if (!coachlist.Contains(coach.CoachId))
                                coachlist.Add(coach.CoachId);
                            else
                            {
                                if (!Duplicates.ContainsKey(coach.CoachId))
                                    Duplicates.Add(coach.CoachId, coach.Name);
                            }
                        }
                    }
                }


                coachlist.Sort();

                foreach (int i in coachlist)
                {
                    if (!CoachNames.ContainsKey(i))
                        CoachNames.Add(i, GetCoachById(i).Name);
                    else
                    {
                        if (!Duplicates.ContainsKey(i))
                            Duplicates.Add(i, GetCoachById(i).Name);
                    }
                }
            }
        }
        
        public CoachRecord CurrentCoachRecord
        {
            // TO DO : FIX
            // need to separate the coaches from the streamed coaches
            //fix the set part
            get
            {
                if (currentstreamedID != -1 && manager.stream_model != null)
                {
                    CoachCollection coll = (CoachCollection)manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecord(currentstreamedID);
                    return SetCoachFromCollection(coll);
                }

                else
                {
                    CoachRecord test = this.GetCoachById(currentCoachID);
                    return test;
                }
            }
            set
            {
                CoachRecord curr = value;

                List<TableRecordModel> coaches = model.TableModels[EditorModel.COACH_TABLE].GetRecords();
                int index = 0;
                for (index = 0; index < coaches.Count; index++)
                {
                    CoachRecord coach = (CoachRecord)coaches[index];
                    if (coach.CoachId == curr.CoachId)
                    {
                        currentCoachIndex = index;
                        return;
                    }
                }

                currentCoachIndex = 0;
            }
        }
        
		public CoachRecord GetNextCoachRecord()
		{
			CoachRecord record = null;

			int startingindex = currentCoachID;
			while (true)
			{
				currentCoachID++;
				if (currentCoachID == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentCoachID >= model.TableModels[EditorModel.COACH_TABLE].RecordCount)
				{
					currentCoachID = -1;
					continue;
				}

				record = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(currentCoachID);

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

			int startingindex = currentCoachID;
			while (true)
			{
				currentCoachID--;
				if (currentCoachID == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentCoachID < 0)
				{
					currentCoachID = model.TableModels[EditorModel.COACH_TABLE].RecordCount;
					continue;
				}

				record = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(currentCoachID);

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

        public CoachRecord GetCoachRecord(int recno)
        {
            return (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].GetRecord(recno);
        }

        public CoachRecord GetCoachById(int id)
        {
            currentstreamedID = -1;
            currentCoachID = -1;

            // we want to return the ROS/FRA coach record if it exists
            foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (((CoachRecord)record).CoachId == id)
                {                    
                    currentCoachID = id;
                    CoachRecord cr = (CoachRecord)record;
                    return cr;
                }
            }

            // Doesnt exist in ROS/FRA so let's see if it is in the streameddata coch table
            if (manager.stream_model != null)
            {               
                foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                {
                    if (rec.Deleted)
                        continue;
                    CoachCollection coachc = (CoachCollection)rec;
                    if (coachc.CoachId == id)
                    {
                        currentstreamedID = id;
                        CoachRecord cr = SetCoachFromCollection(coachc);
                        return cr;
                    }
                }
            }

            return null;
        }

        public IList<GenericRecord> CoachSkinColor
        {
            get { return coachskincolor; }
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

        public SortedList<int, CoachHistory> GetCoachHistory()
        {
            SortedList<int, CoachHistory> results = new SortedList<int, CoachHistory>();
            foreach (TableRecordModel record in model.TableModels[EditorModel.COACHING_HISTORY_TABLE].GetRecords())
            {
                if (record.Deleted)
                    continue;

                CoachHistory hist = (CoachHistory)record;
                if (hist.CoachID == CurrentCoachRecord.CoachId)
                {
                    try
                    {
                        results.Add(hist.Season, hist);
                    }
                    catch (System.ArgumentException e)
                    {
                        Trace.Write("Duplicate history exists for coach\n\r" + e.ToString());
                    }
                }

            }

            return results;
        }
        
        public bool CheckCoachSliders()
        {            
            // We need to see if we have values for this coachid in the CPSE table.
            // There also should be CoachRecord.NUMBER_OF_COACHING_POSITIONS
            List<int> positions = new List<int>();
            bool changed = false;
            foreach (TableRecordModel recordModel in model.TableModels[EditorModel.COACH_SLIDER_TABLE].GetRecords())
            {
                if (recordModel.Deleted)               
                    continue;               

                CoachPrioritySliderRecord record = (CoachPrioritySliderRecord)recordModel;
                if (CurrentCoachRecord.CoachId == record.CoachId)                
                    positions.Add(record.PositionId);                

                if (positions.Count == CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS)                
                    break;                
            }

            if (positions.Count < CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS)
            {
                // Coach does not have a complete slider record
                for (int i = 0; i < CoachPrioritySliderRecord.NUMBER_OF_COACHING_POSITIONS; i++)
                {
                    if (!positions.Contains(i))
                    {
                        try
                        {
                            CoachPrioritySliderRecord record = (CoachPrioritySliderRecord)model.TableModels[EditorModel.COACH_SLIDER_TABLE].CreateNewRecord(true);
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
                }

                changed = true;
                MessageBox.Show("You just changed a Head Coach.\r\nDefault values may have been assigned to priorities", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return changed;
        }

        public bool ChangeCoachControl(int newPosition)
        {
            if (CurrentCoachRecord.Position == newPosition)
                return false;
            
            //  Assume coach is all cpu controlled
            CurrentCoachRecord.CPUControlled = true;
            CurrentCoachRecord.CPUSignDraftPicks = true;
            CurrentCoachRecord.CPUDraftPlayer = true;
            CurrentCoachRecord.CPUSignFreeAgents = true;
            CurrentCoachRecord.CPUFillRosters = true;
            CurrentCoachRecord.CPUResignPlayers = true;
            CurrentCoachRecord.CPUManageDepth = true;
            CurrentCoachRecord.UserControlled = false;

            if (newPosition == (int)MaddenCoachPosition.HeadCoach)
			{
                foreach (OwnerRecord rec in model.TableModels[EditorModel.OWNER_TABLE].GetRecords())
                {
                    OwnerRecord owner = (OwnerRecord)rec;
                    if (owner.TeamId != CurrentCoachRecord.TeamId)
                        continue;
                    if (owner.UserControlled)
                    {
                        //  Coach's Team is user owned so set heach coach to thoss preferences
                        CurrentCoachRecord.CPUControlled = owner.CPUControlled;
                        CurrentCoachRecord.CPUSignDraftPicks = owner.SignDraftPicks;
                        CurrentCoachRecord.CPUDraftPlayer = owner.DraftPlayers;
                        CurrentCoachRecord.CPUSignFreeAgents = owner.SignFreeAgents;
                        CurrentCoachRecord.CPUFillRosters = owner.FillRosters;
                        CurrentCoachRecord.CPUResignPlayers = owner.ResignPlayers;
                        CurrentCoachRecord.CPUManageDepth = owner.ReorderDepthCharts;
                        CurrentCoachRecord.UserControlled = owner.UserControlled;
                    }
                    
                    break;
                }
            }

            return true;
        }
                
        /// <summary>
		/// Changes the current coaches position to the new position. This method
		/// also ensures that other table are updated. The important table to change
		/// is the CPSE or Coach Slider table. 
		/// </summary>
		/// <param name="newPosition">The new position of the coach</param>
		/// <returns>Returns false if we cannot correctly update this coaches position</returns>
        public bool ChangeCoachPosition(int newPosition)
        {
            //  No change, so return
            bool changed = false;
            if (CurrentCoachRecord.Position == newPosition)
                return changed;            

            bool exists = CheckForExisitingCoach(newPosition, CurrentCoachRecord.TeamId);

            if (!exists)
            {
                if (model.FileVersion <= MaddenFileVersion.Ver2006)                
                    changed = CheckCoachSliders();
                changed = ChangeCoachControl(newPosition);                
                CurrentCoachRecord.Position = newPosition;
            }

            return changed;
        }

        public bool CheckForExisitingCoach(int newposition, int newteam)
        {
            bool exists = false;
            if (newteam == 1023 || newteam == 1009)    // free agents no need to check
                return false; 

            foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                CoachRecord coach = (CoachRecord)rec;
                if (coach.Position == newposition && coach.TeamId == newteam)
                {
                    exists = true;
                    break;
                }
            }
            
            if (exists)
                MessageBox.Show("Team has an exisiting coach.\r\nPlease remove current coach for desired team", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return exists;
        }
        
        public bool CheckCoaches(int Teamid)
        {
            List<CoachRecord> teamcoaches = new List<CoachRecord>();

            foreach (TableRecordModel recs in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (recs.Deleted)
                    continue;

                CoachRecord c = (CoachRecord)recs;
                if (c.TeamId == Teamid && !teamcoaches.Contains(c))
                {
                    teamcoaches.Add(c);
                }
                else if (teamcoaches.Contains(c))
                    recs.SetDeleteFlag(true);                
            }

            return true;
        }

        public bool ChangeCoachTeam(int teamid)
        {
            if (CurrentCoachRecord.TeamId == teamid)
                return false;
            bool changed = false;
            bool exists = false;
            if (teamid !=1009)
                exists = CheckForExisitingCoach(CurrentCoachRecord.Position, teamid);           

            if (!exists)
            {
                if (model.CoachModel.CurrentCoachRecord.wasinstreamed)
                {
                    CoachRecord cr = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].CreateNewRecord(true);
                    cr = model.CoachModel.CurrentCoachRecord;
                    cr.wasinstreamed = false;
                    int id = cr.CoachId;
                    currentstreamedID = -1;
                    model.CoachModel.CurrentCoachRecord = GetCoachById(id);                    
                }

                model.CoachModel.CurrentCoachRecord.TeamId = teamid;
                model.CoachModel.CurrentCoachRecord.LastTeam = teamid;
                ChangeCoachControl(CurrentCoachRecord.Position);
                changed = true;
            }            

            return changed;
        }
                 
        public bool CheckCoaches()
        {
            #region Setup lists of employed and unemployed coaches
            TeamCoaches = new List<List<CoachRecord>>();
            List<CoachRecord> duplicates = new List<CoachRecord>();
            List<CoachRecord> available = new List<CoachRecord>();
            
            for (int c = 0; c < 33; c++)            
                TeamCoaches.Add(new List<CoachRecord>());
            
            foreach (TableRecordModel thiscoach in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (thiscoach.Deleted)
                    continue;

                bool exists = false;
                foreach (CoachRecord rec in employed)
                    if (((CoachRecord)thiscoach).CoachId == rec.CoachId)
                        exists = true;
                if (exists && !thiscoach.Deleted)
                {
                    duplicates.Add((CoachRecord)thiscoach);
                    continue;
                }

                if (((CoachRecord)thiscoach).TeamId >= 0 && ((CoachRecord)thiscoach).TeamId <= 31 && !exists)
                {
                    employed.Add((CoachRecord)thiscoach);
                    continue;
                }

                if (((CoachRecord)thiscoach).TeamId == 1023 && !exists)
                    available.Add((CoachRecord)thiscoach);
            }
            #endregion

            for (int c = 0; c < employed.Count; c++)            
                TeamCoaches[employed[c].TeamId].Add(employed[c]);

            for (int team = 1; team < 33; team++)
            {
                bool[] hascoach = new bool[4];
                
                for (int pos = 0; pos < 4; pos++)
                {
                    hascoach[pos] = false;

                    foreach (CoachRecord rec in this.TeamCoaches[team])
                    {
                        if (rec.Position == pos)
                        {
                            if (hascoach[pos])
                            {
                                // more than one coach at position
                                rec.TeamId = 1023;
                                this.TeamCoaches[team].Remove(rec);
                                employed.Remove(rec);
                                available.Add(rec);
                            }

                            else hascoach[pos] = true;
                        }
                    }

                    if (!hascoach[pos])
                    {
                        //  No coach at this position.  Find one and add
                        if (available.Count > 0)
                        {
                            available[0].TeamId = team;
                            available[0].Position = pos;
                            TeamCoaches[team].Add(available[0]);
                            available.RemoveAt(0);
                        }
                    }
                }

            }

            //foreach (CoachRecord rec in duplicates)                                                                 //  Remove any duplicates
            //    rec.SetDeleteFlag(true);

            return true;
        }

        public CoachRecord SetCoachFromCollection(CoachCollection collection)
        {
            CoachRecord newcoach = (CoachRecord)model.TableModels[EditorModel.COACH_TABLE].CreateNewRecord(true);
            newcoach.wasinstreamed = true;

            newcoach.Age = collection.Age;            
            newcoach.BodySize = collection.BodySize;
            newcoach.Chemistry = collection.Chemistry;            
            newcoach.CoachId = collection.CoachId;
            newcoach.ContractLength = collection.ContractLength;
            newcoach.CareerLosses = collection.CareerLosses;            
            newcoach.PlayoffLosses = collection.PlayoffLosses;
            newcoach.PlayoffsMade = collection.PlayoffsMade;
            newcoach.PlayoffWins = collection.PlayoffWins;
            newcoach.Cctc = collection.Cctc;
            newcoach.CareerTies = collection.CareerTies;
            newcoach.CareerWins = collection.CareerWins;
            newcoach.WinningSeasons = collection.WinningSeasons;            
            newcoach.DefenseRating = collection.DefenseRating;
            newcoach.DefensivePlaybook = collection.DefensivePlaybook;            
            newcoach.DefensiveAggression = collection.DefensiveAggression;
            newcoach.DefensiveStrategy = collection.DefensiveStrategy;
            newcoach.Ethics = collection.Ethics;
            newcoach.CPUDraftPlayer = collection.CPUDraftPlayer;
            newcoach.CPUSignDraftPicks = collection.CPUSignDraftPicks;
            newcoach.CPUControlled = collection.CPUControlled;            
            newcoach.CPUSignFreeAgents = collection.CPUSignFreeAgents;
            newcoach.CPUFillRosters = collection.CPUFillRosters;
            newcoach.cfhl = collection.cfhl;
            newcoach.CPUResignPlayers= collection.CPUResignPlayers;
            newcoach.CPUManageDepth = collection.CPUManageDepth;
            newcoach.Cfsh = collection.Cfsh;
            newcoach.UserControlled = collection.UserControlled;
            newcoach.Char = collection.Char;
            newcoach.height = collection.height;
            newcoach.HeadHair = collection.HeadHair;
            newcoach.Chsd = collection.Chsd;
            newcoach.Chty = collection.Chty;
            newcoach.Knowledge = collection.Knowledge;            
            newcoach.Name = collection.Name;
            newcoach.LastTeamFranchise = collection.LastTeamFranchise;
            newcoach.LastTeamRelocated = collection.LastTeamRelocated;            
            newcoach.Motivation = collection.Motivation;            
            newcoach.Offense = collection.Offense;            
            newcoach.Position = collection.Position;            
            newcoach.OffensiveAggression = collection.OffensiveAggression;
            newcoach.OffensiveStrategy = collection.OffensiveStrategy;
            newcoach.CoachOfTheYear = collection.CoachOfTheYear;
            newcoach.OffensivePlaybook = collection.OffensivePlaybook;
            newcoach.FaceId = collection.FaceId;            
            newcoach.RBCarryDist = collection.RBCarryDist;
            newcoach.CoachDB = collection.CoachDB;
            newcoach.CoachDL = collection.CoachDL;
            newcoach.KickerRating = collection.CoachKS;
            newcoach.CoachLB = collection.CoachLB;
            newcoach.CoachOL = collection.CoachOL;
            newcoach.CoachPS = collection.CoachPS;
            newcoach.CoachQB = collection.CoachQB;
            newcoach.CoachRB = collection.CoachRB;
            newcoach.CoachS = collection.CoachS;
            newcoach.CoachWR = collection.CoachWR;            
            newcoach.Salary = collection.Salary;
            newcoach.SuperBowlLoses = collection.SuperBowlLoses;            
            newcoach.SuperBowlWins = collection.SuperBowlWins;            
            newcoach.SkinColor = collection.SkinColor;            
            newcoach.Cspc = collection.Cspc;
            newcoach.Coachpic = collection.Coachpic;
            newcoach.CoachGlasses = collection.CoachGlasses;
            newcoach.Cthg = collection.Cthg;
            newcoach.WasPlayer = collection.WasPlayer;            
            newcoach.TeamId = collection.TeamId;
            
            return newcoach;
        }

        public void DeleteCoachRecord(CoachRecord record)
        {
            // TO DO : FINISH THIS
            record.SetDeleteFlag(true);

        }
    
    }
}
