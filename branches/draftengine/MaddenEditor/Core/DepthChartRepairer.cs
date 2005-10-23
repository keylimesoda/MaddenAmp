/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
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
using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
    public class DepthChartRepairer
    {
        EditorModel model;
        LocalMath math;
        public Dictionary<int, Position> positionData;

        // awarenessAdjust[ActualPosition][AlternativePosition]
        public Dictionary<int, Dictionary<int, double>> awarenessAdjust = new Dictionary<int,Dictionary<int,double>>();

        // These are "local" in a sense, since they'll be cleared at the top
        // of any function that uses them, but they need to be scoped here
        // for use in the SimpleFill() function.
        private List<List<PlayerRecord>> toReturn;
        private List<int> starters;
        private Dictionary<int, Dictionary<int, double>> valuesByPosition;

        private void ComputeCONs(List<int> teamOveralls)
        {
            List<int> rankedTeams = new List<int>();

            for (int i = 0; i < 32; i++)
            {
                int bestId = 0;
                int bestRating = 0;

                for (int j = 0; j < 32; j++)
                {
                    if (bestRating < teamOveralls[j] && !rankedTeams.Contains(j))
                    {
                        bestRating = teamOveralls[j];
                        bestId = j;
                    }
                }

                rankedTeams.Add(bestId);
            }

            for (int i = 0; i < 32; i++) {
                Console.WriteLine(teamOveralls[rankedTeams[i]]);
            }

            List<int> conCutoffs = new List<int>();
            conCutoffs.Add(teamOveralls[rankedTeams[28]]);
            conCutoffs.Add(teamOveralls[rankedTeams[22]]);
            conCutoffs.Add(teamOveralls[rankedTeams[14]]);
            conCutoffs.Add(teamOveralls[rankedTeams[6]]);

            foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                TeamRecord tr = (TeamRecord)record;

                if (tr.TeamId >= 32) { continue; }

                if (teamOveralls[tr.TeamId] < conCutoffs[0])
                {
                    tr.CON = 1;
                }
                else if (teamOveralls[tr.TeamId] < conCutoffs[1])
                {
                    tr.CON = 2;
                }
                else if (teamOveralls[tr.TeamId] < conCutoffs[2])
                {
                    tr.CON = 3;
                }
                else if (teamOveralls[tr.TeamId] < conCutoffs[3])
                {
                    tr.CON = 4;
                }
                else
                {
                    tr.CON = 5;
                }

                Console.WriteLine(tr.Name + " " + teamOveralls[tr.TeamId] + " " + tr.CON);

                // Should add code here that increases CON if they've got an old QB
                // who's above 90 or so and about to retire.
            }
        }

        public List<int> ComputeTeamOveralls(List<List<List<PlayerRecord>>> depthChart)
        {

            List<int> toReturn = new List<int>();

            for (int i = 0; i < 32; i++)
            {
                double numerator = 0;
                double denominator = 0;
                int defense = model.TeamModel.GetTeamRecord(i).DefensiveSystem;

                for (int j = 0; j < 21; j++)
                {
                    for (int k = 0; k < positionData[j].Starters(defense); k++)
                    {
                        // Assume you can always find a guy rated 75 or above at that position in
                        // free agency.
                        if (k < depthChart[i][j].Count)
                        {
                            numerator += positionData[j].Value(defense) * Math.Max(depthChart[i][j][k].Overall, 75);
                        }
                        else
                        {
                            numerator += positionData[j].Value(defense) * 75;
                        }

                        denominator += positionData[j].Value(defense);
                    }

                    if (j >= 19) { continue; }

                    for (int k = positionData[j].Starters(defense); k < 2*positionData[j].Starters(defense); k++)
                    {
                        // For backups, assume we can always get a guy rated 70 or above in free agency.
                        if (k < depthChart[i][j].Count)
                        {
                            numerator += 0.5 * positionData[j].BackupNeed * positionData[j].Value(defense) * Math.Max(depthChart[i][j][k].Overall, 70);
                        }
                        else
                        {
                            numerator += 0.5 * positionData[j].BackupNeed * positionData[j].Value(defense) * 70;
                        }

                        denominator += 0.5 * positionData[j].BackupNeed * positionData[j].Value(defense);
                    }
                }

                double tempOverall = (double)numerator / denominator;

                toReturn.Add((int)Math.Round(tempOverall + 2.0*(tempOverall - 85.0)));

                Console.WriteLine(model.TeamModel.GetTeamRecord(i).Name + " " + toReturn[i] + " " + model.TeamModel.GetTeamRecord(i).OverallRating);
            }

            return toReturn;
        }

        public DepthChartRepairer(EditorModel em, Dictionary<int, Position> pd)
        {
            model = em;
            positionData = pd;

            math = new LocalMath(model.FileVersion);

            InitializeAwarenessAdjust();

            if (pd == null)
            {
                positionData = new Dictionary<int, Position>();
                InitializePositionData();
            }
        }

        public List<List<List<PlayerRecord>>> ReorderDepthCharts(bool withProgression)
        {
            List<List<List<PlayerRecord>>> depthChart = new List<List<List<PlayerRecord>>>();

            for (int i = 0; i < 32; i++)
            {
                depthChart.Add(SortDepthChart(i, false, null));
            }

            if (withProgression)
            {
                ComputeCONs(ComputeTeamOveralls(depthChart));
                depthChart = new List<List<List<PlayerRecord>>>();

                for (int i = 0; i < 32; i++)
                {
                    depthChart.Add(SortDepthChart(i, true, null));
                }
            }
            
            SaveDepthChartList(depthChart);
            return depthChart;
        }

		//private DepthChartRecord GetDepthChartRecord(int team, int position, int depth) {


        public void SaveDepthChartList(List<List<List<PlayerRecord>>> depthChart)
        {
			// Basically, I want to clear out all existing depth chart entries, 
			// and refill the table with the entries from depthChart
			//
			// The problem is that I set the entries for deletion in this first
			// block of code, but then these deletion flags seem to persist with the
			// new records I create in the 2nd block of code, and all the new entries
			// get deleted.  As a result, the depth charts are all empty when the 
			// user loads it into Madden.
			//
			// If the depth charts start out empty, then the first block of code
			// does nothing, and the 2nd block properly fills the charts.  So, to test
			// that this bug is gone, you need to be sure you're starting with already
			// filled depth charts.
			//
			// Also, as it is now, if you run this code with filled depth charts, it
			// does seem to order them if you go into your depth chart editor.  But,
			// if you open up the file in Madden, the depth charts are empty.  And,
			// if you save the file then reopen it in the editor, the depth charts
			// are empty.
			//
			// So, the question is, can you fix this so that you can open it up with
			// depth charts filled, reorder using this function, close the editor
			// then reopen the file and see that the depth charts remain filled
			// and properly ordered?

            TableModel dcRecords = model.TableModels[EditorModel.DEPTH_CHART_TABLE];

			foreach (TableRecordModel rec in dcRecords.GetRecords())
			{
				rec.SetDeleteFlag(true);
			}

            //dcRecords.Save();

			int index = 0;
			for (int team = 0; team < depthChart.Count; team++)
            {
                for (int pos = 0; pos < depthChart[team].Count; pos++)
                {
                    for (int depth = 0; depth < depthChart[team][pos].Count; depth++)
                    {
                        //DepthChartRecord newRecord = (DepthChartRecord)dcRecords.CreateNewRecord(false);

						DepthChartRecord rec = (DepthChartRecord)model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecord(index++);

						rec.SetDeleteFlag(false);
						rec.TeamId = team;
						rec.PositionId = pos;
						rec.DepthOrder = depth;
						rec.PlayerId = depthChart[team][pos][depth].PlayerId;
                    }
                }
            }

			//dcRecords.Save();
        }

        public List<List<PlayerRecord>> SortDepthChart(int TeamToSort, bool withProgression, Dictionary<int, RookieRecord> rookies)
        {
            toReturn = new List<List<PlayerRecord>>();
            starters = new List<int>();

            // to be passed when we need an empty list
            List<int> empty = new List<int>();

            List<int> groupstarters = new List<int>();
            List<int> groupstartersTemp = new List<int>();
            double bestValue = 0;

            // counts the number of players at various position groupings
            Dictionary<string, int> groupings = new Dictionary<string, int>();
            groupings["RB"] = 0;
            groupings["OL"] = 0;
            groupings["DL"] = 0;
            groupings["LB"] = 0;
            groupings["DB"] = 0;

            double con = 5;

            for (int i = 0; i < 26; i++)
            {
                toReturn.Add(new List<PlayerRecord>());
            }

            if (withProgression)
            {
                con = model.TeamModel.GetTeamRecord(TeamToSort).CON;
            }

            // First calculate each player's value at any position he might play
            valuesByPosition = new Dictionary<int, Dictionary<int, double>>();

            for (int i = 0; i < 26; i++)
            {
                valuesByPosition[i] = new Dictionary<int, double>();
            }

            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord player = (PlayerRecord)rec;

                if (TeamToSort != player.TeamId) { continue; }
                
                foreach (KeyValuePair<int, double> pair in awarenessAdjust[player.PositionId])
                {
                    double value;
                    if (pair.Key < 21)
                    {
                        value = positionData[pair.Key].Value(model.TeamModel.GetTeamRecord(player.TeamId).DefensiveSystem);
                    }
                    else
                    {
                        value = 1;
                    }

                    valuesByPosition[pair.Key][player.PlayerId] = LocalMath.ValueScale * value * math.valcurve(GetAdjustedOverall(player, pair.Key) + math.pointboost(player,con,positionData[player.PositionId].RetirementAge));
                }

                switch (player.PositionId)
                {
                    case (int)MaddenPositions.HB:
                    case (int)MaddenPositions.FB:
                    case (int)MaddenPositions.TE:
                        groupings["RB"]++;
                        break;
                    case (int)MaddenPositions.LT:
                    case (int)MaddenPositions.LG:
                    case (int)MaddenPositions.C:
                    case (int)MaddenPositions.RG:
                    case (int)MaddenPositions.RT:
                        groupings["OL"]++;
                        break;
                    case (int)MaddenPositions.LE:
                    case (int)MaddenPositions.RE:
                    case (int)MaddenPositions.DT:
                        groupings["DL"]++;
                        break;
                    case (int)MaddenPositions.LOLB:
                    case (int)MaddenPositions.MLB:
                    case (int)MaddenPositions.ROLB:
                        groupings["LB"]++;
                        break;
                    case (int)MaddenPositions.CB:
                    case (int)MaddenPositions.FS:
                    case (int)MaddenPositions.SS:
                        groupings["DB"]++;
                        break;
                }
            }

            // If we're in the middle of a draft, process the rookies as well

            if (rookies != null)
            {
                foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                {
                    if (TeamToSort != rook.Value.DraftedTeam) { continue; }

                    foreach (KeyValuePair<int, double> pair in awarenessAdjust[rook.Value.Player.PositionId])
                    {
                        if (pair.Key > 20)
                        {
                            continue;
                        }

                        if (withProgression)
                        {
                            valuesByPosition[pair.Key][rook.Value.PlayerId] = rook.Value.values[TeamToSort][pair.Key][(int)RookieRecord.ValueType.WithProg];
                        }
                        else
                        {
                            valuesByPosition[pair.Key][rook.Value.PlayerId] = rook.Value.values[TeamToSort][pair.Key][(int)RookieRecord.ValueType.NoProg];
                        }
                    }

                    switch (rook.Value.Player.PositionId)
                    {
                        case (int)MaddenPositions.HB:
                        case (int)MaddenPositions.FB:
                        case (int)MaddenPositions.TE:
                            groupings["RB"]++;
                            break;
                        case (int)MaddenPositions.LT:
                        case (int)MaddenPositions.LG:
                        case (int)MaddenPositions.C:
                        case (int)MaddenPositions.RG:
                        case (int)MaddenPositions.RT:
                            groupings["OL"]++;
                            break;
                        case (int)MaddenPositions.LE:
                        case (int)MaddenPositions.RE:
                        case (int)MaddenPositions.DT:
                            groupings["DL"]++;
                            break;
                        case (int)MaddenPositions.LOLB:
                        case (int)MaddenPositions.MLB:
                        case (int)MaddenPositions.ROLB:
                            groupings["LB"]++;
                            break;
                        case (int)MaddenPositions.CB:
                        case (int)MaddenPositions.FS:
                        case (int)MaddenPositions.SS:
                            groupings["DB"]++;
                            break;
                    }
                }
            }

            // First sort the QB's -- this one's easy
            SimpleFill(0, 3, (int)MaddenPositions.QB, -1, starters, TeamToSort);

            // Next do the HB's, FB's, and TE's, since they kind of mix among themselves
            // Pick all possible combinations of players at all positions, and pick the set with
            // the best total value

            if (groupings["RB"] < 4)
            {
                SimpleFill(0, 3, (int)MaddenPositions.HB, (int)MaddenPositions.HB, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.FB, (int)MaddenPositions.FB, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.TE, (int)MaddenPositions.TE, empty, TeamToSort);
            }
            else
            {

                bestValue = 0;

                foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.HB])
                {

                    groupstartersTemp.Add(pair1.Key);

                    foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.HB])
                    {
                        if (groupstartersTemp.Contains(pair2.Key))
                        {
                            continue;
                        }

                        groupstartersTemp.Add(pair2.Key);

                        foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.FB])
                        {
                            if (groupstartersTemp.Contains(pair3.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair3.Key);

                            foreach (KeyValuePair<int, double> pair4 in valuesByPosition[(int)MaddenPositions.TE])
                            {
                                if (groupstartersTemp.Contains(pair4.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair4.Key);

                                if (pair1.Value + 0.5 * pair2.Value + pair3.Value + pair4.Value > bestValue)
                                {
                                    bestValue = pair1.Value + 0.5 * pair2.Value + pair3.Value + pair4.Value;

                                    groupstarters.Clear();

                                    for (int k = 0; k < groupstartersTemp.Count; k++)
                                    {
                                        groupstarters.Add(groupstartersTemp[k]);
                                    }
                                }

                                groupstartersTemp.Remove(pair4.Key);
                            }

                            groupstartersTemp.Remove(pair3.Key);
                        }

                        groupstartersTemp.Remove(pair2.Key);
                    }

                    groupstartersTemp.Remove(pair1.Key);
                }

                if (groupstarters.Count >= 4)
                {
                    toReturn[(int)MaddenPositions.HB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                    toReturn[(int)MaddenPositions.HB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                    toReturn[(int)MaddenPositions.FB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));
                    toReturn[(int)MaddenPositions.TE].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[3]));

                    starters.Add(groupstarters[0]);
                    starters.Add(groupstarters[2]);
                    starters.Add(groupstarters[3]);

                    // Now fill the backups
                    SimpleFill(2, 3, (int)MaddenPositions.HB, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.FB, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.TE, -1, groupstarters, TeamToSort);
                } 
                else
                {
                    // It might be that they have nobody to play TE, or nobody to play HB -- if so, just do simple fills

                    toReturn[(int)MaddenPositions.HB].Clear();
                    toReturn[(int)MaddenPositions.FB].Clear();
                    toReturn[(int)MaddenPositions.TE].Clear();

                    SimpleFill(0, 3, (int)MaddenPositions.HB, (int)MaddenPositions.HB, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.FB, (int)MaddenPositions.FB, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.TE, (int)MaddenPositions.TE, empty, TeamToSort);
                }
            }


            // Get the starting WR's -- only fill with actual WR's
            SimpleFill(0, 4, (int)MaddenPositions.WR, (int)MaddenPositions.WR, empty, TeamToSort);

            // Get the 5th WR.  Since there's no formation with 5 WR's and a TE/HB/FB, fill
            // the 5th slot with the best WR left

            List<int> temp = new List<int>();

            for (int i = 0; i < toReturn[(int)MaddenPositions.WR].Count; i++)
            {
                temp.Add(toReturn[(int)MaddenPositions.WR][i].PlayerId);
            }

            SimpleFill(toReturn[(int)MaddenPositions.WR].Count, toReturn[(int)MaddenPositions.WR].Count+1, (int)MaddenPositions.WR, -1, temp, TeamToSort);
            temp.Add(toReturn[(int)MaddenPositions.WR][toReturn[(int)MaddenPositions.WR].Count-1].PlayerId);

            // Fill the 6th WR -- since this is a guy that might need to fill for an injury
            // to one of the top four, force him to be a WR.
            SimpleFill(toReturn[(int)MaddenPositions.WR].Count, toReturn[(int)MaddenPositions.WR].Count+1, (int)MaddenPositions.WR, (int)MaddenPositions.WR, temp, TeamToSort);

            // O-line

            groupstarters.Clear();
            groupstartersTemp.Clear();
            bestValue = 0;

            if (groupings["OL"] < 5)
            {
                SimpleFill(0, 3, (int)MaddenPositions.LT, (int)MaddenPositions.LT, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.LG, (int)MaddenPositions.LG, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.C, (int)MaddenPositions.C, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.RG, (int)MaddenPositions.RG, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.RT, (int)MaddenPositions.RT, empty, TeamToSort);
            }
            else
            {
                foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.LT])
                {

                    groupstartersTemp.Add(pair1.Key);

                    foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.LG])
                    {
                        if (groupstartersTemp.Contains(pair2.Key))
                        {
                            continue;
                        }

                        groupstartersTemp.Add(pair2.Key);

                        foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.C])
                        {
                            if (groupstartersTemp.Contains(pair3.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair3.Key);

                            foreach (KeyValuePair<int, double> pair4 in valuesByPosition[(int)MaddenPositions.RG])
                            {
                                if (groupstartersTemp.Contains(pair4.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair4.Key);

                                foreach (KeyValuePair<int, double> pair5 in valuesByPosition[(int)MaddenPositions.RT])
                                {
                                    if (groupstartersTemp.Contains(pair5.Key))
                                    {
                                        continue;
                                    }

                                    groupstartersTemp.Add(pair5.Key);


                                    if (pair1.Value + pair2.Value + pair3.Value + pair4.Value + pair5.Value > bestValue)
                                    {
                                        bestValue = pair1.Value + pair2.Value + pair3.Value + pair4.Value + pair5.Value;

                                        groupstarters.Clear();

                                        for (int k = 0; k < groupstartersTemp.Count; k++)
                                        {
                                            groupstarters.Add(groupstartersTemp[k]);
                                        }
                                    }

                                    groupstartersTemp.Remove(pair5.Key);
                                }

                                groupstartersTemp.Remove(pair4.Key);
                            }

                            groupstartersTemp.Remove(pair3.Key);
                        }

                        groupstartersTemp.Remove(pair2.Key);
                    }

                    groupstartersTemp.Remove(pair1.Key);
                }

                toReturn[(int)MaddenPositions.LT].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                toReturn[(int)MaddenPositions.LG].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                toReturn[(int)MaddenPositions.C].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));
                toReturn[(int)MaddenPositions.RG].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[3]));
                toReturn[(int)MaddenPositions.RT].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[4]));

                starters.Add(groupstarters[0]);
                starters.Add(groupstarters[1]);
                starters.Add(groupstarters[2]);
                starters.Add(groupstarters[3]);
                starters.Add(groupstarters[4]);

                SimpleFill(1, 3, (int)MaddenPositions.LT, -1, groupstarters, TeamToSort);
                SimpleFill(1, 3, (int)MaddenPositions.LG, -1, groupstarters, TeamToSort);
                SimpleFill(1, 3, (int)MaddenPositions.C, -1, groupstarters, TeamToSort);
                SimpleFill(1, 3, (int)MaddenPositions.RG, -1, groupstarters, TeamToSort);
                SimpleFill(1, 3, (int)MaddenPositions.RT, -1, groupstarters, TeamToSort);
            }


            // D-Line -- Need to distinguish between 3-4 and 4-3 here.

            groupstarters.Clear();
            groupstartersTemp.Clear();
            bestValue = 0;

            if (model.TeamModel.GetTeamRecord(TeamToSort).DefensiveSystem == (int)TeamRecord.Defense.Front34)
            {
                if (groupings["DL"] < 3)
                {
                    SimpleFill(0, 3, (int)MaddenPositions.LE, (int)MaddenPositions.LE, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.RE, (int)MaddenPositions.RE, empty, TeamToSort);
                    SimpleFill(0, 5, (int)MaddenPositions.DT, (int)MaddenPositions.DT, empty, TeamToSort);
                }
                else
                {

                    foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.LE])
                    {
                        groupstartersTemp.Add(pair1.Key);

                        foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.RE])
                        {
                            if (groupstartersTemp.Contains(pair2.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair2.Key);

                            foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.DT])
                            {
                                if (groupstartersTemp.Contains(pair3.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair3.Key);


                                if (pair1.Value + pair2.Value + pair3.Value > bestValue)
                                {
                                    bestValue = pair1.Value + pair2.Value + pair3.Value;

                                    groupstarters.Clear();

                                    for (int k = 0; k < groupstartersTemp.Count; k++)
                                    {
                                        groupstarters.Add(groupstartersTemp[k]);
                                    }
                                }

                                groupstartersTemp.Remove(pair3.Key);
                            }

                            groupstartersTemp.Remove(pair2.Key);
                        }

                        groupstartersTemp.Remove(pair1.Key);
                    }

                    toReturn[(int)MaddenPositions.LE].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                    toReturn[(int)MaddenPositions.RE].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                    toReturn[(int)MaddenPositions.DT].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));

                    starters.Add(groupstarters[0]);
                    starters.Add(groupstarters[1]);
                    starters.Add(groupstarters[2]);

                    SimpleFill(1, 3, (int)MaddenPositions.LE, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.RE, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 5, (int)MaddenPositions.DT, -1, groupstarters, TeamToSort);
                }
            }
            else
            {
                if (groupings["DL"] < 4)
                {
                    SimpleFill(0, 3, (int)MaddenPositions.LE, (int)MaddenPositions.LE, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.RE, (int)MaddenPositions.RE, empty, TeamToSort);
                    SimpleFill(0, 5, (int)MaddenPositions.DT, (int)MaddenPositions.DT, empty, TeamToSort);
                }
                else
                {

                    foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.LE])
                    {
                        groupstartersTemp.Add(pair1.Key);

                        foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.RE])
                        {
                            if (groupstartersTemp.Contains(pair2.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair2.Key);

                            foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.DT])
                            {
                                if (groupstartersTemp.Contains(pair3.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair3.Key);

                                foreach (KeyValuePair<int, double> pair4 in valuesByPosition[(int)MaddenPositions.DT])
                                {
                                    if (groupstartersTemp.Contains(pair4.Key))
                                    {
                                        continue;
                                    }

                                    groupstartersTemp.Add(pair4.Key);


                                    if (pair1.Value + pair2.Value + pair3.Value + 0.98 * pair4.Value > bestValue)
                                    {
                                        bestValue = pair1.Value + pair2.Value + pair3.Value + 0.98 * pair4.Value;

                                        groupstarters.Clear();

                                        for (int k = 0; k < groupstartersTemp.Count; k++)
                                        {
                                            groupstarters.Add(groupstartersTemp[k]);
                                        }
                                    }

                                    groupstartersTemp.Remove(pair4.Key);
                                }

                                groupstartersTemp.Remove(pair3.Key);
                            }

                            groupstartersTemp.Remove(pair2.Key);
                        }

                        groupstartersTemp.Remove(pair1.Key);
                    }

                    toReturn[(int)MaddenPositions.LE].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                    toReturn[(int)MaddenPositions.RE].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                    toReturn[(int)MaddenPositions.DT].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));
                    toReturn[(int)MaddenPositions.DT].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[3]));

                    starters.Add(groupstarters[0]);
                    starters.Add(groupstarters[1]);
                    starters.Add(groupstarters[2]);
                    starters.Add(groupstarters[3]);

                    SimpleFill(1, 3, (int)MaddenPositions.LE, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.RE, -1, groupstarters, TeamToSort);
                    SimpleFill(2, 5, (int)MaddenPositions.DT, -1, groupstarters, TeamToSort);
                }
            }

            // LB's -- Again need to distinguish between 3-4 and 4-3 here.

            groupstarters.Clear();
            groupstartersTemp.Clear();
            bestValue = 0;

            if (model.TeamModel.GetTeamRecord(TeamToSort).DefensiveSystem != (int)TeamRecord.Defense.Front34)
            {
                if (groupings["LB"] < 3)
                {
                    SimpleFill(0, 3, (int)MaddenPositions.LOLB, (int)MaddenPositions.LOLB, empty, TeamToSort);
                    SimpleFill(0, 4, (int)MaddenPositions.MLB, (int)MaddenPositions.MLB, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.ROLB, (int)MaddenPositions.ROLB, empty, TeamToSort);
                }
                else
                {

                    foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.LOLB])
                    {
                        groupstartersTemp.Add(pair1.Key);

                        foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.ROLB])
                        {
                            if (groupstartersTemp.Contains(pair2.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair2.Key);

                            foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.MLB])
                            {
                                if (groupstartersTemp.Contains(pair3.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair3.Key);


                                if (pair1.Value + pair2.Value + pair3.Value > bestValue)
                                {
                                    bestValue = pair1.Value + pair2.Value + pair3.Value;

                                    groupstarters.Clear();

                                    for (int k = 0; k < groupstartersTemp.Count; k++)
                                    {
                                        groupstarters.Add(groupstartersTemp[k]);
                                    }
                                }

                                groupstartersTemp.Remove(pair3.Key);
                            }

                            groupstartersTemp.Remove(pair2.Key);
                        }

                        groupstartersTemp.Remove(pair1.Key);
                    }

                    toReturn[(int)MaddenPositions.LOLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                    toReturn[(int)MaddenPositions.ROLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                    toReturn[(int)MaddenPositions.MLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));

                    starters.Add(groupstarters[0]);
                    starters.Add(groupstarters[1]);
                    starters.Add(groupstarters[2]);

                    SimpleFill(1, 3, (int)MaddenPositions.LOLB, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.ROLB, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 4, (int)MaddenPositions.MLB, -1, groupstarters, TeamToSort);
                }
            }
            else
            {
                if (groupings["LB"] < 4)
                {
                    SimpleFill(0, 3, (int)MaddenPositions.LOLB, (int)MaddenPositions.LOLB, empty, TeamToSort);
                    SimpleFill(0, 4, (int)MaddenPositions.MLB, (int)MaddenPositions.MLB, empty, TeamToSort);
                    SimpleFill(0, 3, (int)MaddenPositions.ROLB, (int)MaddenPositions.ROLB, empty, TeamToSort);
                }
                else
                {

                    foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.LOLB])
                    {
                        groupstartersTemp.Add(pair1.Key);

                        foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.ROLB])
                        {
                            if (groupstartersTemp.Contains(pair2.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair2.Key);

                            foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.MLB])
                            {
                                if (groupstartersTemp.Contains(pair3.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair3.Key);

                                foreach (KeyValuePair<int, double> pair4 in valuesByPosition[(int)MaddenPositions.MLB])
                                {
                                    if (groupstartersTemp.Contains(pair4.Key))
                                    {
                                        continue;
                                    }

                                    groupstartersTemp.Add(pair4.Key);


                                    if (pair1.Value + pair2.Value + pair3.Value + 0.98 * pair4.Value > bestValue)
                                    {
                                        bestValue = pair1.Value + pair2.Value + pair3.Value + 0.98 * pair4.Value;

                                        groupstarters.Clear();

                                        for (int k = 0; k < groupstartersTemp.Count; k++)
                                        {
                                            groupstarters.Add(groupstartersTemp[k]);
                                        }
                                    }

                                    groupstartersTemp.Remove(pair4.Key);
                                }

                                groupstartersTemp.Remove(pair3.Key);
                            }

                            groupstartersTemp.Remove(pair2.Key);
                        }

                        groupstartersTemp.Remove(pair1.Key);
                    }

                    toReturn[(int)MaddenPositions.LOLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                    toReturn[(int)MaddenPositions.ROLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                    toReturn[(int)MaddenPositions.MLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));
                    toReturn[(int)MaddenPositions.MLB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[3]));

                    starters.Add(groupstarters[0]);
                    starters.Add(groupstarters[1]);
                    starters.Add(groupstarters[2]);
                    starters.Add(groupstarters[3]);

                    SimpleFill(1, 3, (int)MaddenPositions.LOLB, -1, groupstarters, TeamToSort);
                    SimpleFill(1, 3, (int)MaddenPositions.ROLB, -1, groupstarters, TeamToSort);
                    SimpleFill(2, 4, (int)MaddenPositions.MLB, -1, groupstarters, TeamToSort);
                }
            }


            // DB's

            groupstarters.Clear();
            groupstartersTemp.Clear();
            bestValue = 0;

            if (groupings["DB"] < 7)
            {
                SimpleFill(0, 5, (int)MaddenPositions.CB, (int)MaddenPositions.CB, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.FS, (int)MaddenPositions.FS, empty, TeamToSort);
                SimpleFill(0, 3, (int)MaddenPositions.SS, (int)MaddenPositions.SS, empty, TeamToSort);
            }
            else
            {


                foreach (KeyValuePair<int, double> pair1 in valuesByPosition[(int)MaddenPositions.CB])
                {

                    groupstartersTemp.Add(pair1.Key);

                    foreach (KeyValuePair<int, double> pair2 in valuesByPosition[(int)MaddenPositions.CB])
                    {
                        if (groupstartersTemp.Contains(pair2.Key))
                        {
                            continue;
                        }

                        groupstartersTemp.Add(pair2.Key);

                        foreach (KeyValuePair<int, double> pair3 in valuesByPosition[(int)MaddenPositions.CB])
                        {
                            if (groupstartersTemp.Contains(pair3.Key))
                            {
                                continue;
                            }

                            groupstartersTemp.Add(pair3.Key);

                            foreach (KeyValuePair<int, double> pair4 in valuesByPosition[(int)MaddenPositions.CB])
                            {
                                if (groupstartersTemp.Contains(pair4.Key))
                                {
                                    continue;
                                }

                                groupstartersTemp.Add(pair4.Key);

                                foreach (KeyValuePair<int, double> pair5 in valuesByPosition[(int)MaddenPositions.CB])
                                {
                                    if (groupstartersTemp.Contains(pair5.Key))
                                    {
                                        continue;
                                    }

                                    groupstartersTemp.Add(pair5.Key);

                                    foreach (KeyValuePair<int, double> pair6 in valuesByPosition[(int)MaddenPositions.FS])
                                    {
                                        if (groupstartersTemp.Contains(pair6.Key))
                                        {
                                            continue;
                                        }

                                        groupstartersTemp.Add(pair6.Key);

                                        foreach (KeyValuePair<int, double> pair7 in valuesByPosition[(int)MaddenPositions.SS])
                                        {
                                            if (groupstartersTemp.Contains(pair7.Key))
                                            {
                                                continue;
                                            }

                                            groupstartersTemp.Add(pair7.Key);

                                            if (pair1.Value + 0.98 * pair2.Value + 0.3 * pair3.Value + 0.15 * pair4.Value + 0.08 * pair5.Value + pair6.Value + pair7.Value > bestValue)
                                            {
                                                bestValue = pair1.Value + 0.98 * pair2.Value + 0.3 * pair3.Value + 0.15 * pair4.Value + 0.08 * pair5.Value + pair6.Value + pair7.Value;

                                                groupstarters.Clear();

                                                for (int k = 0; k < groupstartersTemp.Count; k++)
                                                {
                                                    groupstarters.Add(groupstartersTemp[k]);
                                                }
                                            }

                                            groupstartersTemp.Remove(pair7.Key);
                                        }

                                        groupstartersTemp.Remove(pair6.Key);
                                    }

                                    groupstartersTemp.Remove(pair5.Key);
                                }

                                groupstartersTemp.Remove(pair4.Key);
                            }

                            groupstartersTemp.Remove(pair3.Key);
                        }

                        groupstartersTemp.Remove(pair2.Key);
                    }

                    groupstartersTemp.Remove(pair1.Key);
                }

                toReturn[(int)MaddenPositions.CB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[0]));
                toReturn[(int)MaddenPositions.CB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[1]));
                toReturn[(int)MaddenPositions.CB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[2]));
                toReturn[(int)MaddenPositions.CB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[3]));
                toReturn[(int)MaddenPositions.CB].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[4]));
                toReturn[(int)MaddenPositions.FS].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[5]));
                toReturn[(int)MaddenPositions.SS].Add(model.PlayerModel.GetPlayerByPlayerId(groupstarters[6]));

                starters.Add(groupstarters[0]);
                starters.Add(groupstarters[1]);
                starters.Add(groupstarters[5]);
                starters.Add(groupstarters[6]);

                // groupstarters contains all 5 CB's, but we might want some of these
                // to backup at safety.  So, just skip over starters instead.
                SimpleFill(1, 3, (int)MaddenPositions.FS, -1, starters, TeamToSort);
                SimpleFill(1, 3, (int)MaddenPositions.SS, -1, starters, TeamToSort);
            }

            // I guess we could put punters at the bottom of kickers and vice versa
            // but that seems like a bit of a hassle -- I'll just assume it'll get figured out
            SimpleFill(0, 3, (int)MaddenPositions.P, -1, empty, TeamToSort);
            SimpleFill(0, 3, (int)MaddenPositions.K, -1, empty, TeamToSort);

            // Just make PR and KR tables the same -- no reason to differentiate really.
            // For now, just fill them, and skip over starters.  Could be made better
            // by allowing starters, but only if they're much better.
            SimpleFill(0, 3, 21, -1, starters, TeamToSort);
            SimpleFill(0, 3, 22, -1, starters, TeamToSort);

            // KOS
            SimpleFill(0, 3, 23, -1, empty, TeamToSort);

            // LS
            SimpleFill(0, 3, 24, -1, starters, TeamToSort);

            // 3DRB
            SimpleFill(0, 3, 25, -1, empty, TeamToSort);

            return toReturn;
        }
        
        private void SimpleFill(int numStart, int totalSpots, int positionToFill, int positionToCount, List<int> toExclude, int teamId) {
            List<int> exclusions = new List<int>();
            for (int i = 0; i < toExclude.Count; i++)
            {
                exclusions.Add(toExclude[i]);
            }
            
            for (int i = numStart; i < totalSpots; i++)
            {
                int bestId = -1;
                double bestValue = 0;

                foreach (KeyValuePair<int, double> pair in valuesByPosition[positionToFill])
                {
                    if (pair.Value > bestValue && !exclusions.Contains(pair.Key) && (positionToCount == -1 || positionToCount == model.PlayerModel.GetPlayerByPlayerId(pair.Key).PositionId))
                    {
                        bestId = pair.Key;
                        bestValue = pair.Value;
                    }
                }

                if (bestId >= 0)
                {
                    toReturn[positionToFill].Add(model.PlayerModel.GetPlayerByPlayerId(bestId));
                    exclusions.Add(bestId);

                    if (positionToFill < 21 && i < positionData[positionToFill].Starters(model.TeamModel.GetTeamRecord(teamId).DefensiveSystem))
                    {
                        starters.Add(bestId);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public double GetAdjustedOverall(PlayerRecord player, int alternatePosition)
        {
            double tempOverall = 0;

            switch (alternatePosition)
            {
                case (int)MaddenPositions.QB:
                    tempOverall = (((double)player.ThrowPower - 50) / 10) * 4.9;
                    tempOverall += (((double)player.ThrowAccuracy - 50) / 10) * 5.8;
                    tempOverall += (((double)player.BreakTackle - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Agility - 50) / 10) * 0.8;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 4.0;
                    tempOverall += (((double)player.Speed - 50) / 10) * 2.0;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
                    break;
                case (int)MaddenPositions.HB:
                    tempOverall = (((double)player.PassBlocking - 50) / 10) * 0.33;
                    tempOverall += (((double)player.BreakTackle - 50) / 10) * 3.3;
                    tempOverall += (((double)player.Carrying - 50) / 10) * 2.0;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Agility - 50) / 10) * 2.8;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.0;
                    tempOverall += (((double)player.Strength - 50) / 10) * 0.6;
                    tempOverall += (((double)player.Speed - 50) / 10) * 3.3;
                    tempOverall += (((double)player.Catching - 50) / 10) * 1.4;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
                    break;
                case (int)MaddenPositions.FB:
                    tempOverall = (((double)player.PassBlocking - 50) / 10) * 1.0;
                    tempOverall += (((double)player.RunBlocking - 50) / 10) * 7.2;
                    tempOverall += (((double)player.BreakTackle - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Carrying - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.0;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.8;
                    tempOverall += (((double)player.Strength - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Speed - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Catching - 50) / 10) * 5.2;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 39, 1);
                    break;
                case (int)MaddenPositions.WR:
                    tempOverall = (((double)player.BreakTackle - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.3;
                    tempOverall += (((double)player.Agility - 50) / 10) * 2.3;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.3;
                    tempOverall += (((double)player.Strength - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Speed - 50) / 10) * 2.3;
                    tempOverall += (((double)player.Catching - 50) / 10) * 4.75;
                    tempOverall += (((double)player.Jumping - 50) / 10) * 1.4;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
                    break;
                case (int)MaddenPositions.TE:
                    tempOverall = (((double)player.Speed - 50) / 10) * 2.65;
                    tempOverall += (((double)player.Strength - 50) / 10) * 2.65;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.65;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.25;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.25;
                    tempOverall += (((double)player.Catching - 50) / 10) * 5.4;
                    tempOverall += (((double)player.BreakTackle - 50) / 10) * 1.2;
                    tempOverall += (((double)player.PassBlocking - 50) / 10) * 1.2;
                    tempOverall += (((double)player.RunBlocking - 50) / 10) * 5.4;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 35, 1);
                    break;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                    tempOverall = (((double)player.Speed - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Strength - 50) / 10) * 3.3;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 3.3;
                    tempOverall += (((double)player.Agility - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 0.8;
                    tempOverall += (((double)player.PassBlocking - 50) / 10) * 4.75;
                    tempOverall += (((double)player.RunBlocking - 50) / 10) * 3.75;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
                    break;
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                case (int)MaddenPositions.C:
                    tempOverall = (((double)player.Speed - 50) / 10) * 1.7;
                    tempOverall += (((double)player.Strength - 50) / 10) * 3.25;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 3.25;
                    tempOverall += (((double)player.Agility - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.7;
                    tempOverall += (((double)player.PassBlocking - 50) / 10) * 3.25;
                    tempOverall += (((double)player.RunBlocking - 50) / 10) * 4.8;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
                    break;
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.RE:
                    tempOverall = (((double)player.Speed - 50) / 10) * 3.75;
                    tempOverall += (((double)player.Strength - 50) / 10) * 3.75;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 1.75;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.75;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 3.8;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 5.5;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
                    break;
                case (int)MaddenPositions.DT:
                    tempOverall = (((double)player.Speed - 50) / 10) * 1.8;
                    tempOverall += (((double)player.Strength - 50) / 10) * 5.5;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 3.8;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.8;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 4.55;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
                    break;
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.ROLB:
                    tempOverall = (((double)player.Speed - 50) / 10) * 3.75;
                    tempOverall += (((double)player.Strength - 50) / 10) * 2.4;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 3.6;
                    tempOverall += (((double)player.Agility - 50) / 10) * 2.4;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.3;
                    tempOverall += (((double)player.Catching - 50) / 10) * 1.3;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 4.8;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
                    break;
                case (int)MaddenPositions.MLB:
                    tempOverall = (((double)player.Speed - 50) / 10) * 0.75;
                    tempOverall += (((double)player.Strength - 50) / 10) * 3.4;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 5.2;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.65;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.75;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 5.2;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
                    break;
                case (int)MaddenPositions.CB:
                    tempOverall = (((double)player.Speed - 50) / 10) * 3.85;
                    tempOverall += (((double)player.Strength - 50) / 10) * 0.9;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 3.85;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.55;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.35;
                    tempOverall += (((double)player.Catching - 50) / 10) * 3;
                    tempOverall += (((double)player.Jumping - 50) / 10) * 1.55;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 1.55;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
                    break;
                case (int)MaddenPositions.FS:
                    tempOverall = (((double)player.Speed - 50) / 10) * 3.0;
                    tempOverall += (((double)player.Strength - 50) / 10) * 0.9;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 4.85;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.5;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.5;
                    tempOverall += (((double)player.Catching - 50) / 10) * 3.0;
                    tempOverall += (((double)player.Jumping - 50) / 10) * 1.5;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 2.5;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
                    break;
                case (int)MaddenPositions.SS:
                    tempOverall = (((double)player.Speed - 50) / 10) * 3.2;
                    tempOverall += (((double)player.Strength - 50) / 10) * 1.7;
                    tempOverall += ((((double)player.Awareness)*awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 4.75;
                    tempOverall += (((double)player.Agility - 50) / 10) * 1.7;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 1.7;
                    tempOverall += (((double)player.Catching - 50) / 10) * 3.2;
                    tempOverall += (((double)player.Jumping - 50) / 10) * 0.9;
                    tempOverall += (((double)player.Tackle - 50) / 10) * 3.2;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
                    break;
                case (int)MaddenPositions.P:
                    tempOverall = (double)(-183 + 0.218 * player.Awareness + 1.5 * player.KickPower + 1.33 * player.KickAccuracy);
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
                    break;
                case (int)MaddenPositions.K:
                    tempOverall = (double)(-177 + 0.218 * player.Awareness + 1.28 * player.KickPower + 1.47 * player.KickAccuracy);
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
                    break;
                case 21: // KR
                case 22: // PR
                    // This is too simple!  It should be improved...
                    tempOverall = (double)player.KickReturn;
                    break;
                case 23: // KOS
                    tempOverall = (double)(-167.67 + 2.556 * player.KickPower + 0.222 * player.KickAccuracy);
                    break;
                case 24: // LS -- no idea what the actual formula is.  Just modified 'C' by making speed, acceleration, and agility more important (to get down field).
                    tempOverall = (((double)player.Speed - 50) / 10) * 2.5;
                    tempOverall += (((double)player.Strength - 50) / 10) * 3;
                    tempOverall += ((((double)player.Awareness) * awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.5;
                    tempOverall += (((double)player.Agility - 50) / 10) * 0.8;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.0;
                    tempOverall += (((double)player.PassBlocking - 50) / 10) * 3;
                    tempOverall += (((double)player.RunBlocking - 50) / 10) * 4.2;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
                    break;
                case 25:
                    // 3DRB -- again, no idea what the formula is.  Took HB formula, made PBK more important, 
                    // CTH more important, ACC more important, CAR less important, BTK less important
                    // Supposed to be kind of an HB/WR hybrid.
                    //
                    // Seems basically right though -- only noticed a couple of small mistakes
                    // in the depth chart by using this algorithm.
                    tempOverall = (((double)player.PassBlocking - 50) / 10) * 0.6;
                    tempOverall += (((double)player.BreakTackle - 50) / 10) * 2.1;
                    tempOverall += (((double)player.Carrying - 50) / 10) * 1.4;
                    tempOverall += (((double)player.Acceleration - 50) / 10) * 2.1;
                    tempOverall += (((double)player.Agility - 50) / 10) * 2.8;
                    tempOverall += ((((double)player.Awareness) * awarenessAdjust[player.PositionId][alternatePosition] - 50) / 10) * 2.0;
                    tempOverall += (((double)player.Strength - 50) / 10) * 0.6;
                    tempOverall += (((double)player.Speed - 50) / 10) * 3.3;
                    tempOverall += (((double)player.Catching - 50) / 10) * 3.0;
                    tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
                    break;
            }

            if (tempOverall < 0)
            {
                tempOverall = 0;
            }
            if (tempOverall > 99)
            {
                tempOverall = 99;
            }

            return tempOverall;
        }

        private void InitializeAwarenessAdjust()
        {
            for (int i = 0; i < 21; i++)
            {
                awarenessAdjust[i] = new Dictionary<int, double>();
                awarenessAdjust[i][i] = 1.0;
            }

            awarenessAdjust[(int)MaddenPositions.HB][(int)MaddenPositions.FB] = 0.65;
            awarenessAdjust[(int)MaddenPositions.HB][(int)MaddenPositions.WR] = 0.80;
            awarenessAdjust[(int)MaddenPositions.FB][(int)MaddenPositions.HB] = 0.90;
            awarenessAdjust[(int)MaddenPositions.FB][(int)MaddenPositions.TE] = 0.85;
            awarenessAdjust[(int)MaddenPositions.TE][(int)MaddenPositions.FB] = 0.85;
            awarenessAdjust[(int)MaddenPositions.TE][(int)MaddenPositions.WR] = 0.9;

            awarenessAdjust[(int)MaddenPositions.LT][(int)MaddenPositions.LG] = 0.8;
            awarenessAdjust[(int)MaddenPositions.LT][(int)MaddenPositions.C] = 0.7;
            awarenessAdjust[(int)MaddenPositions.LT][(int)MaddenPositions.RG] = 0.8;
            awarenessAdjust[(int)MaddenPositions.LT][(int)MaddenPositions.RT] = 0.9;
            awarenessAdjust[(int)MaddenPositions.LG][(int)MaddenPositions.LT] = 0.85;
            awarenessAdjust[(int)MaddenPositions.LG][(int)MaddenPositions.C] = 0.8;
            awarenessAdjust[(int)MaddenPositions.LG][(int)MaddenPositions.RG] = 0.9;
            awarenessAdjust[(int)MaddenPositions.LG][(int)MaddenPositions.RT] = 0.8;
            awarenessAdjust[(int)MaddenPositions.C][(int)MaddenPositions.LT] = 0.8;
            awarenessAdjust[(int)MaddenPositions.C][(int)MaddenPositions.LG] = 0.8;
            awarenessAdjust[(int)MaddenPositions.C][(int)MaddenPositions.RG] = 0.8;
            awarenessAdjust[(int)MaddenPositions.C][(int)MaddenPositions.RT] = 0.8;
            awarenessAdjust[(int)MaddenPositions.RG][(int)MaddenPositions.LT] = 0.8;
            awarenessAdjust[(int)MaddenPositions.RG][(int)MaddenPositions.LG] = 0.9;
            awarenessAdjust[(int)MaddenPositions.RG][(int)MaddenPositions.C] = 0.8;
            awarenessAdjust[(int)MaddenPositions.RG][(int)MaddenPositions.RT] = 0.85;
            awarenessAdjust[(int)MaddenPositions.RT][(int)MaddenPositions.LT] = 0.9;
            awarenessAdjust[(int)MaddenPositions.RT][(int)MaddenPositions.LG] = 0.8;
            awarenessAdjust[(int)MaddenPositions.RT][(int)MaddenPositions.C] = 0.7;
            awarenessAdjust[(int)MaddenPositions.RT][(int)MaddenPositions.RG] = 0.8;


            awarenessAdjust[(int)MaddenPositions.LE][(int)MaddenPositions.RE] = 0.9;
            awarenessAdjust[(int)MaddenPositions.LE][(int)MaddenPositions.DT] = 0.9;
            awarenessAdjust[(int)MaddenPositions.RE][(int)MaddenPositions.LE] = 0.85;
            awarenessAdjust[(int)MaddenPositions.RE][(int)MaddenPositions.DT] = 0.9;
            awarenessAdjust[(int)MaddenPositions.DT][(int)MaddenPositions.LE] = 0.8;
            awarenessAdjust[(int)MaddenPositions.DT][(int)MaddenPositions.RE] = 0.85;

            awarenessAdjust[(int)MaddenPositions.LOLB][(int)MaddenPositions.MLB] = 0.85;
            awarenessAdjust[(int)MaddenPositions.LOLB][(int)MaddenPositions.ROLB] = 0.85;
            awarenessAdjust[(int)MaddenPositions.MLB][(int)MaddenPositions.LOLB] = 0.8;
            awarenessAdjust[(int)MaddenPositions.MLB][(int)MaddenPositions.ROLB] = 0.8;
            awarenessAdjust[(int)MaddenPositions.ROLB][(int)MaddenPositions.LOLB] = 0.85;
            awarenessAdjust[(int)MaddenPositions.ROLB][(int)MaddenPositions.MLB] = 0.85;

            awarenessAdjust[(int)MaddenPositions.CB][(int)MaddenPositions.FS] = 0.50;
            awarenessAdjust[(int)MaddenPositions.CB][(int)MaddenPositions.SS] = 0.50;
            awarenessAdjust[(int)MaddenPositions.FS][(int)MaddenPositions.CB] = 0.80;
            awarenessAdjust[(int)MaddenPositions.FS][(int)MaddenPositions.SS] = 0.85;
            awarenessAdjust[(int)MaddenPositions.SS][(int)MaddenPositions.CB] = 0.80;
            awarenessAdjust[(int)MaddenPositions.SS][(int)MaddenPositions.FS] = 0.85;

            // KR, PR
            awarenessAdjust[(int)MaddenPositions.HB][21] = 1;
            awarenessAdjust[(int)MaddenPositions.WR][21] = 1;
            awarenessAdjust[(int)MaddenPositions.CB][21] = 1;
            awarenessAdjust[(int)MaddenPositions.FS][21] = 1;
            awarenessAdjust[(int)MaddenPositions.SS][21] = 1;

            awarenessAdjust[(int)MaddenPositions.HB][22] = 1;
            awarenessAdjust[(int)MaddenPositions.WR][22] = 1;
            awarenessAdjust[(int)MaddenPositions.CB][22] = 1;
            awarenessAdjust[(int)MaddenPositions.FS][22] = 1;
            awarenessAdjust[(int)MaddenPositions.SS][22] = 1;

            // KOS
            awarenessAdjust[(int)MaddenPositions.K][23] = 1;
            awarenessAdjust[(int)MaddenPositions.P][23] = 1;

            // LS
            awarenessAdjust[(int)MaddenPositions.LT][24] = 1;
            awarenessAdjust[(int)MaddenPositions.LG][24] = 1;
            awarenessAdjust[(int)MaddenPositions.C][24] = 1;
            awarenessAdjust[(int)MaddenPositions.RG][24] = 1;
            awarenessAdjust[(int)MaddenPositions.RT][24] = 1;

            // 3DRB
            awarenessAdjust[(int)MaddenPositions.HB][25] = 1;

        }

        private void InitializePositionData()
        {
            positionData.Add((int)MaddenPositions.QB, new Position(100, 100, 100, 1, 35, 0.7, 0.3, 1, 1, 0.9));
            positionData.Add((int)MaddenPositions.HB, new Position(60, 60, 60, 0.8, 33, 1, 0.05, 1, 1, 1));
            positionData.Add((int)MaddenPositions.FB, new Position(10, 10, 10, 0.3, 32, 0.2, 0.75, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.WR, new Position(65, 65, 65, 0.7, 35, 0.9, 0, 2, 2, 0.6));
            positionData.Add((int)MaddenPositions.TE, new Position(25, 25, 25, 0.6, 32, 0.6, 0.4, 1, 1, 0.7));
            positionData.Add((int)MaddenPositions.LT, new Position(65, 65, 65, 0.7, 36, 0.5, 0, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.LG, new Position(35, 35, 35, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.C, new Position(30, 30, 30, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.RG, new Position(35, 35, 35, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.RT, new Position(55, 55, 55, 0.6, 36, 0.5, 0, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.LE, new Position(50, 45, 55, 0.5, 35, 0.8, 0, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.RE, new Position(85, 55, 85, 0.5, 35, 0.8, 0, 1, 1, 0.8));
            positionData.Add((int)MaddenPositions.DT, new Position(65, 65, 65, 0.4, 35, 0.8, 0, 2, 1, 0.8));
            positionData.Add((int)MaddenPositions.LOLB, new Position(45, 65, 55, 0.5, 34, 0.4, 0.1, 1, 1, 0.7));
            positionData.Add((int)MaddenPositions.MLB, new Position(50, 55, 60, 0.5, 34, 0.4, 0.1, 1, 2, 0.7));
            positionData.Add((int)MaddenPositions.ROLB, new Position(50, 60, 60, 0.5, 34, 0.4, 0.1, 1, 1, 0.7));
            positionData.Add((int)MaddenPositions.CB, new Position(80, 75, 60, 0.7, 32, 0.9, 0, 2, 2, 0.6));
            positionData.Add((int)MaddenPositions.FS, new Position(40, 40, 50, 0.4, 32, 0.6, 0.05, 1, 1, 0.6));
            positionData.Add((int)MaddenPositions.SS, new Position(40, 40, 50, 0.4, 32, 0.6, 0.05, 1, 1, 0.6));
            positionData.Add((int)MaddenPositions.K, new Position(4, 4, 4, 0.1, 38, 0.1, 0.85, 1, 1, 0.2));
            positionData.Add((int)MaddenPositions.P, new Position(1, 1, 1, 0.1, 38, 0.1, 0.85, 1, 1, 0.2));
        }

    }
}
