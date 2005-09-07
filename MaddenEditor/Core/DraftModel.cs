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

namespace MaddenEditor.Core.Record
{
    public class DraftModel
    {
        private int currentPickIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** Reference to our EditorModel */
		private EditorModel model = null;
        /** Rookies, indexed by PlayerId **/
        private Dictionary<int, RookieRecord> rookies = new Dictionary<int, RookieRecord>();
        private List<double> pickValues = new List<double>();
        private int HumanTeamId;
        public const double TotalScoutingHours = 1000.0;

        public Dictionary<int,Position> positionData = new Dictionary<int,Position>();

        private LocalMath math = new LocalMath();

        // We ran into problems with seeding of random numbers.  Let's create a single
        // instance of Random, and make repeated calls to it.
        private Random rand = new Random();

        /** Our own internal depth chart.  We want to make some changes
         * to the depth without them being permanent -- in particular,
         * we need to add the drafted rooks to the list.  Plus, some easier
         * access would be nice as well.  This is :
         * 
         * depthChart[TeamId][PositionId][Depth] = PlayerRecord */
        private List<List<List<PlayerRecord>>> depthChart;

        public void DumpRookies(int sort)
        {
            List<int> found = new List<int>();

            for (int i = 0; i < 250; i++)
            {

                double bestRating = 0;
                int bestId = -1;

                foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                {
                    /*
                    if (rook.Value.ratings[0][(int)RookieRecord.RatingType.Final][sort] > bestRating &&
                        !found.Contains(rook.Key))
                     */
                    if (rook.Value.values[0][(int)RookieRecord.ValueType.NoProg] > bestRating &&
                        !found.Contains(rook.Key))
                    {

                        bestId = rook.Key;
                        /* bestRating = rook.Value.ratings[0][(int)RookieRecord.RatingType.Final][sort]; */
                        bestRating = rook.Value.values[0][(int)RookieRecord.ValueType.NoProg];
                    }
                }

                found.Add(bestId);

                Console.WriteLine(i + " " + rookies[bestId].Player.ToString() + " " + rookies[bestId].Player.Overall + " " + rookies[bestId].Player.Injury + " " + rookies[bestId].values[0][(int)RookieRecord.ValueType.NoProg] + " " + pickValues[i]);
            }
        }

        public List<RookieRecord> GetDraftBoard(TeamRecord team, int pickNumber)
        {
            List<RookieRecord> draftBoard = new List<RookieRecord>();

            for (int i = 0; i < 10; i++)
            {
                double bestRating = 0;
                int bestId = -1;

                foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                {
                    if (rook.Value.PerceivedEffectiveValue(team, pickNumber) > bestRating &&
                        !draftBoard.Contains(rook.Value) && !(rook.Value.DraftedTeam < 32))
                    {

                        bestId = rook.Key;
                        bestRating = rook.Value.PerceivedEffectiveValue(team, pickNumber);
                    }
                }

                draftBoard.Add(rookies[bestId]);
            }

            return draftBoard;
        }

        public List<PlayerRecord> GetDepthChart(int TeamId, int PositionId)
        {
            return depthChart[TeamId][PositionId];
        }

        public Dictionary<int, RookieRecord> GetRookies(int PositionId)
        {
            if (PositionId < 0) { return rookies; }

            Dictionary<int, RookieRecord> toReturn = new Dictionary<int, RookieRecord>();
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                if (rook.Value.Player.PositionId == PositionId)
                {
                    toReturn.Add(rook.Key, rook.Value);
                }
            }
            return toReturn;
        }

        private void CalculateActualProjections() {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies) {
                  rook.Value.ActualValue =  LocalMath.ValueScale * positionData[rook.Value.Player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(rook.Value.Player.Overall + math.injury(rook.Value.Player.Injury, positionData[rook.Value.Player.PositionId].DurabilityNeed));
            }

            List<int> SortedAverageValues = new List<int>();

            for (int i = 0; i < rookies.Count; i++)
            {
                double bestRating = 0;
                int bestId = -1;

                foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                {
                    if (rook.Value.ActualValue > bestRating && !SortedAverageValues.Contains(rook.Key))
                    {
                        bestId = rook.Key;
                        bestRating = rook.Value.ActualValue;
                    }
                }

                SortedAverageValues.Add(bestId);
            }

            for (int i = 0; i < SortedAverageValues.Count; i++)
            {
                rookies[SortedAverageValues[i]].EstimatedPickNumber[(int)RookieRecord.RatingType.Actual] = i;

                double rank = i;
                string rankstring;

                if (rank < 5)
                {
                    rankstring = "Top 5";
                }
                else if (rank < 10)
                {
                    rankstring = "Top 10";
                }
                else if (rank < 28)
                {
                    rankstring = "1st";
                }
                else if (rank < 36)
                {
                    rankstring = "1st-2nd";
                }
                else if (rank < 60)
                {
                    rankstring = "2nd";
                }
                else if (rank < 68)
                {
                    rankstring = "2nd-3rd";
                }
                else if (rank < 92)
                {
                    rankstring = "3rd";
                }
                else if (rank < 100)
                {
                    rankstring = "3rd-4th";
                }
                else if (rank < 128)
                {
                    rankstring = "4th";
                }
                else if (rank < 160)
                {
                    rankstring = "5th";
                }
                else if (rank < 192)
                {
                    rankstring = "6th";
                }
                else if (rank < 224)
                {
                    rankstring = "7th";
                }
                else
                {
                    rankstring = "Undrafted";
                }

                rookies[SortedAverageValues[i]].EstimatedRound[(int)RookieRecord.RatingType.Actual] = rankstring;
            }
        }
        
        public RookieRecord MakeSelection(int pickNumber, RookieRecord toDraft)
        {
            DraftPickRecord dpRecord = null;

            foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
            {
                DraftPickRecord record = (DraftPickRecord)rec;

                if (record.PickNumber == pickNumber)
                {
                    dpRecord = record;
                    break;
                }
            }

            if (toDraft == null)
            {
                Dictionary<int, RookieRecord> favorites = GetFavoriteRookies(pickNumber);
                toDraft = favorites[dpRecord.CurrentTeamId];
            }
            
            /*
             * Some debugging/diagnostic code
             */ 
            Console.WriteLine("\n" + " " + pickNumber + " " + dpRecord.CurrentTeamId + " " + toDraft.Player.ToString() + " " + toDraft.Player.Overall + " " + toDraft.Player.Injury + "\n");
            int trash = 2 + 2;

            if (pickNumber == 3)
            {
                Console.WriteLine(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).CON);
                foreach (RookieRecord rook in GetDraftBoard(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), pickNumber))
                {
                    if (rook.DraftedTeam < 32) { continue; }
                    Console.WriteLine(rook.Player.PlayerId + " " + rook.Player.ToString() + " " + rook.EffectiveValue(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), pickNumber) + " " + rook.TotalNeed(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), pickNumber) + " " + rook.needs[dpRecord.CurrentTeamId][(int)RookieRecord.NeedType.Starter] + " " + rook.needs[dpRecord.CurrentTeamId][(int)RookieRecord.NeedType.Backup] + " " + rook.values[dpRecord.CurrentTeamId][(int)RookieRecord.ValueType.WithProg] + " " + rook.Player.Overall + " " + rook.Player.Injury);
                }
            }
            
            toDraft.DraftedTeam = dpRecord.CurrentTeamId;
            toDraft.DraftPickNumber = pickNumber;

            // For now, they still don't see his actual ratings -- just what they scouted.
            // Not only does this make sense, but it also prevents two picks at the same position
            // (two K's, two P's, etc.)
            toDraft.Player.EffectiveOVR = toDraft.ratings[dpRecord.CurrentTeamId][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.OVR] + 5 * (5 - model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).CON) / 2
                    + math.injury(toDraft.ratings[dpRecord.CurrentTeamId][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.INJ], positionData[toDraft.Player.PositionId].DurabilityNeed);
            toDraft.Player.Value = LocalMath.ValueScale * positionData[toDraft.Player.PositionId].Value(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).DefensiveSystem) * math.valcurve(toDraft.Player.EffectiveOVR);

            depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId].Add(toDraft.Player);
            depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId] = SortByEffectiveOVR(depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId], dpRecord.CurrentTeamId, toDraft.Player.PositionId);

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                if (rook.Value.Player.PositionId != toDraft.Player.PositionId) { continue; }
                rook.Value.CalculateNeeds(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId], positionData);
            }

            return toDraft;
        }

        public void DumpDraftResults()
        {

            for (int i = 0; i < model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount; i++)
            {
                DraftPickRecord dpRecord = GetDraftPickByNumber(i);
                RookieRecord rookieRecord = null;
                RookieRecord record = null;

                foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFTED_PLAYERS_TABLE].GetRecords())
                {
                    record = (RookieRecord)rec;

                    if (record.DraftPickNumber == i)
                    {
                        rookieRecord = (RookieRecord)rec;
                        break;
                    }
                }

                if (rookieRecord == null)
                {
                    Console.WriteLine("Oops!");
                }

                Console.Write(i + " " + rookieRecord.PlayerId + " " );
                Console.Write(model.TeamModel.GetTeamNameFromTeamId(dpRecord.CurrentTeamId) + " ");
                Console.Write(Enum.GetNames(typeof(MaddenPositions))[model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerId).PositionId].ToString() + " ");
                Console.Write(model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerId).Overall + " " + model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerId).Injury + " ");
                if (pickValues.Count > 100)
                {
                    Console.WriteLine(Math.Round(rookieRecord.values[dpRecord.CurrentTeamId][(int)RookieRecord.ValueType.NoProg]) + " " + pickValues[i]);

                }
                else
                {
                    Console.Write("\n");
                }
            }
        }

        public void InitializeScouting()
        {
            SetInitialRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Initial);

            SetRookieValues((int)RookieRecord.RatingType.Initial);
            DetermineProjections((int)RookieRecord.RatingType.Initial);
            SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Initial);

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                rook.Value.PreCombineScoutedHours[HumanTeamId] = 0;
                rook.Value.PostCombineScoutedHours[HumanTeamId] = 0;
            }
        }

        public void DoCombine() {
            DoCPURookieScouting(true);
            SetCombineRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Combine);
            SetRookieValues((int)RookieRecord.RatingType.Combine);
            DetermineProjections((int)RookieRecord.RatingType.Combine);
            SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Combine);
        }

        public void DoFinal()
        {
            DoCPURookieScouting(false);
            SetFinalRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Final);
            SetRookieValues((int)RookieRecord.RatingType.Final);
            DetermineProjections((int)RookieRecord.RatingType.Final);

            SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Final);
            SetInitialNeeds();
        }

        public void InitializeDraft(int htid)
        {
            HumanTeamId = htid;

            InitializePositionData();
            InitializePickValues();

            model.TeamModel.ComputeCONs();
            model.PlayerModel.ComputeEffectiveOVRs(this);

            ExtractRookies();
            GenerateInternalDepthChart();
            CalculateActualProjections();

            // This next call shouldn't be needed once the draft scouting
            // apparatus is finished.
            // AssignRookieScoutedAttributes();

            /*
            SetInitialRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Initial);

            SetRookieValues((int)RookieRecord.RatingType.Initial);
            GenerateInternalDepthChart();
            // SetInitialNeeds();
            DetermineProjections((int)RookieRecord.RatingType.Initial);

            DoCPURookieScouting(true);
            SetCombineRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Combine);
            SetRookieValues((int)RookieRecord.RatingType.Combine);
            DetermineProjections((int)RookieRecord.RatingType.Combine);

            DoCPURookieScouting(false);
            SetFinalRookieAttributes();
            CalculateOveralls((int)RookieRecord.RatingType.Final);
            SetRookieValues((int)RookieRecord.RatingType.Final);
            DetermineProjections((int)RookieRecord.RatingType.Final);

            SetCombineStats(HumanTeamId);
            SetInitialNeeds();

            DumpProjections();
             * */
        }

        private void DumpProjections()
        {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                Console.WriteLine(rook.Value.Player.ToString() + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Initial]
                + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Combine] + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Final]);
            }
        }

        private void SetInitialRookieAttributes()
        {
            Dictionary<int, int> initialErrors = new Dictionary<int,int>();
            initialErrors[(int)RookieRecord.Attribute.INJ] = 40;
            initialErrors[(int)RookieRecord.Attribute.SPD] = 20;
            initialErrors[(int)RookieRecord.Attribute.ACC] = 30;
            initialErrors[(int)RookieRecord.Attribute.AGI] = 30;
            initialErrors[(int)RookieRecord.Attribute.JMP] = 20;
            initialErrors[(int)RookieRecord.Attribute.AWR] = 30;
            initialErrors[(int)RookieRecord.Attribute.STR] = 20;
            initialErrors[(int)RookieRecord.Attribute.CTH] = 20;
            initialErrors[(int)RookieRecord.Attribute.CAR] = 30;
            initialErrors[(int)RookieRecord.Attribute.BTK] = 20;
            initialErrors[(int)RookieRecord.Attribute.TAK] = 30;
            initialErrors[(int)RookieRecord.Attribute.THP] = 20;
            initialErrors[(int)RookieRecord.Attribute.THA] = 30;
            initialErrors[(int)RookieRecord.Attribute.PBK] = 30;
            initialErrors[(int)RookieRecord.Attribute.RBK] = 30;
            initialErrors[(int)RookieRecord.Attribute.KPR] = 20;
            initialErrors[(int)RookieRecord.Attribute.KAC] = 20;
            initialErrors[(int)RookieRecord.Attribute.KRT] = 30;
            initialErrors[(int)RookieRecord.Attribute.STA] = 40;
            initialErrors[(int)RookieRecord.Attribute.TGH] = 20;

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                Dictionary<int,double> baseInitials = new Dictionary<int,double>();

                rook.Value.ActualRatings[(int)RookieRecord.Attribute.TGH] = rook.Value.Player.Toughness;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.THA] = rook.Value.Player.ThrowAccuracy;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.THP] = rook.Value.Player.ThrowPower;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.INJ] = rook.Value.Player.Injury;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.STA] = rook.Value.Player.Stamina;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.SPD] = rook.Value.Player.Speed;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.AGI] = rook.Value.Player.Agility;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.ACC] = rook.Value.Player.Acceleration;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.STR] = rook.Value.Player.Strength;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.JMP] = rook.Value.Player.Jumping;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.CTH] = rook.Value.Player.Catching;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.CAR] = rook.Value.Player.Carrying;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.BTK] = rook.Value.Player.BreakTackle;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.TAK] = rook.Value.Player.Tackle;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.PBK] = rook.Value.Player.PassBlocking;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.RBK] = rook.Value.Player.RunBlocking;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.KPR] = rook.Value.Player.KickPower;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.KAC] = rook.Value.Player.KickAccuracy;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.KRT] = rook.Value.Player.KickReturn;
                rook.Value.ActualRatings[(int)RookieRecord.Attribute.AWR] = rook.Value.Player.Awareness;

                for (int i = 3; i < 23; i++) {
                    baseInitials[i] = rook.Value.ActualRatings[i] + initialErrors[i]*(rand.NextDouble() - 0.5);
                }

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 3; j < 23; j++) {
                        rook.Value.ratings[i][(int)RookieRecord.RatingType.Initial][j] = baseInitials[j];
                    }
                }

                rook.Value.CalculateOverall((int)RookieRecord.RatingType.Initial);
            }
        }

        private void SetCombineRookieAttributes()
        {
            Dictionary<int, int> measureableErrors = new Dictionary<int, int>();

            measureableErrors[(int)RookieRecord.Attribute.SPD] = 10;
            measureableErrors[(int)RookieRecord.Attribute.INJ] = 20;
            measureableErrors[(int)RookieRecord.Attribute.ACC] = 20;
            measureableErrors[(int)RookieRecord.Attribute.AGI] = 20;
            measureableErrors[(int)RookieRecord.Attribute.JMP] = 10;
            measureableErrors[(int)RookieRecord.Attribute.AWR] = 30;
            measureableErrors[(int)RookieRecord.Attribute.STR] = 10;

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                Dictionary<int, double> baseCombine = new Dictionary<int, double>();

                for (int i = 3; i < 10; i++)
                {
                    baseCombine[i] = rook.Value.ActualRatings[i] + measureableErrors[i] * (rand.NextDouble() - 0.5);
                }

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 3; j < 10; j++)
                    {
                        rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j] = baseCombine[j];
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 10; j < 23; j++)
                    {
                        double cv = rook.Value.ratings[i][(int)RookieRecord.RatingType.Initial][j] +
                            (rook.Value.PreCombineScoutedHours[i] / 10.0) * (rook.Value.ActualRatings[j] - rook.Value.ratings[i][(int)RookieRecord.RatingType.Initial][j]);
                        double sigma = Math.Abs((0.5) * (rook.Value.ratings[i][(int)RookieRecord.RatingType.Initial][j] - cv));

                        rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j] =
                            math.bellcurve(cv, sigma, rand);
                    }
                }
            }
        }

        private void SetFinalRookieAttributes()
        {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 3; j < 23; j++)
                    {
                        double cv = rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j] +
                            ((rook.Value.PreCombineScoutedHours[i] + rook.Value.PostCombineScoutedHours[i])/ 10.0) * (rook.Value.ActualRatings[j] - rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j]);
                        double sigma = Math.Abs((0.5) * (rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j] - cv));

                        rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][j] =
                            math.bellcurve(cv, sigma, rand);
                    }
                }
            }
        }

        private void DoCPURookieScouting(bool beforeCombine)
        {
            for (int i = 0; i < 32; i++) {

                if (i == HumanTeamId)
                {
                    continue;
                }

                // First calculate this team's relative need at each position
                Dictionary<int, double> positionNeeds = new Dictionary<int,double>();
                TeamRecord team = model.TeamModel.GetTeamRecord(i);

                for (int j = 0; j < 21; j++) {
                    positionNeeds[j] = positionData[j].Value(team.DefensiveSystem) * ScoutNeed(team, j);
                }

                // Determine relative scouting priority.  For now, we'll see that relative scouting priority
                // is positionNeeds[positionId]*Log[value];
                Dictionary<int,double> scoutingPriority = new Dictionary<int,double>();
                double maxPriority = 0;
                foreach(KeyValuePair<int, RookieRecord> rook in rookies) {
                    scoutingPriority[rook.Key] = positionNeeds[rook.Value.Player.PositionId]*Math.Log(rook.Value.values[team.TeamId][(int)RookieRecord.ValueType.NoProg]);

                    if (scoutingPriority[rook.Key] > maxPriority)
                    {
                        maxPriority = scoutingPriority[rook.Key];
                    }

                    if (!beforeCombine) {
                        scoutingPriority[rook.Key] = Math.Pow(scoutingPriority[rook.Key], 2.0);
                    }
                }

                double total = 0;
                foreach(KeyValuePair<int,double> pair in scoutingPriority) {

                    if (pair.Value / maxPriority > 0.05)
                    {
                        total += pair.Value;
                    }
                }

                Dictionary<int,double> spcopy = new Dictionary<int,double>();
                foreach(KeyValuePair<int, double> pair in scoutingPriority)
                {
                    if (pair.Value / maxPriority < 0.05)
                    {
                        spcopy[pair.Key] = 0;
                    }
                    else
                    {
                        spcopy[pair.Key] = (double)(1 / 2.0) * TotalScoutingHours * pair.Value / total;
                    }
                }
                scoutingPriority = spcopy;

                // If there are any guys with more than 5 hours, shave
                // off the excess, and redistribute it.  Store the results in
                // a new structure

                Dictionary<int, int> scoutingHours = new Dictionary<int,int>();
                int TotalToPrint = 0;
                int remaining = scoutingPriority.Count;

                Dictionary<int, double> spcopyOutside = new Dictionary<int,double>();

                foreach(KeyValuePair<int, double> pair in scoutingPriority) {
                    spcopyOutside[pair.Key] = pair.Value;
                }

                foreach(KeyValuePair<int, double> pair in scoutingPriority) {
                    if (pair.Value > 5) {
                        double excess = (pair.Value - 5) / remaining;

                        foreach (KeyValuePair<int, double> insidepair in scoutingPriority) {
                            spcopyOutside[insidepair.Key] = spcopyOutside[insidepair.Key] + excess;
                        }
                        
                        scoutingHours[pair.Key] = 5;
                        remaining--;
                    }
                }

                scoutingPriority = new Dictionary<int, double>();
                foreach (KeyValuePair<int, double> pair in spcopyOutside)
                {
                    if (pair.Value > 5)
                    {
                        scoutingPriority[pair.Key] = 5;
                    }
                    else
                    {
                        scoutingPriority[pair.Key] = pair.Value;
                    }
                }

                // Go through the rest, round to nearest integer, add to scoutingHours.
                // This may result in the CPU getting too many or too few
                // scouting hours, but it'll even out over the long run.

                foreach (KeyValuePair<int, double> pair in scoutingPriority)
                {
                    scoutingHours[pair.Key] = (int)Math.Round(pair.Value);
                    TotalToPrint += scoutingHours[pair.Key];
                }

                Console.WriteLine(TotalToPrint);

                foreach (KeyValuePair<int, int> pair in scoutingHours) {
                    if (beforeCombine) {
                        rookies[pair.Key].PreCombineScoutedHours[team.TeamId] = pair.Value;
                    } else {
                        rookies[pair.Key].PostCombineScoutedHours[team.TeamId] = pair.Value;
                    }
                }
            }
        }

        private double ScoutNeed(TeamRecord team, int PositionId)
        {

            double starterNeed = 0;
            double backupNeed = 0;
            double successorNeed = 0;

            double rookval = positionData[PositionId].Value(team.DefensiveSystem)*
                math.valcurve(80 + 2.5*(5-team.CON));

            int numStarters = positionData[PositionId].Starters(team.DefensiveSystem);
            int startTemp;

            /** First calculate the starter need **/

            if (depthChart[team.TeamId][PositionId].Count < numStarters)
            {
                /*
                needs[team.TeamId][(int)NeedType.Starter] = 1;
                 */

                // If there's no current starter, they can always get a stop-gap guy
                // in free agency, who we'll assume has OVR = 75, INJ = 75, YRP >= 5,
                // so their effective overall is just 75.  This should at least prevent
                // the totally boneheaded picks.
                int stopgapEffectiveOVR = 78;
                double sgValue = LocalMath.ValueScale * positionData[PositionId].Value(team.DefensiveSystem) * math.valcurve(stopgapEffectiveOVR);
                starterNeed = math.need(rookval, sgValue);
            }
            else
            {
                starterNeed = math.need(rookval, depthChart[team.TeamId][PositionId][numStarters - 1].Value);
            }

            /** Now calculate backup need. **/

            if (numStarters == 1 && (PositionId != (int)MaddenPositions.TE && PositionId != (int)MaddenPositions.QB && PositionId != (int)MaddenPositions.HB))
            {
                if (depthChart[team.TeamId][PositionId].Count <= 1)
                {
                    backupNeed = 1;
                }
                else
                {
                    backupNeed = math.need(rookval, depthChart[team.TeamId][PositionId][numStarters].Value);
                }
            } else {
                if (PositionId != (int)MaddenPositions.CB && PositionId != (int)MaddenPositions.WR)
                {
                    if (depthChart[team.TeamId][PositionId].Count <= numStarters)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(rookval, depthChart[team.TeamId][PositionId][numStarters].Value);
                    }

                    if (depthChart[team.TeamId][PositionId].Count <= numStarters + 1)
                    {
                        backupNeed += 0.25;
                    }
                    else
                    {
                        backupNeed = 0.25 * math.need(rookval, depthChart[team.TeamId][PositionId][numStarters+1].Value);
                    }
                }
                else
                {
                    if (depthChart[team.TeamId][PositionId].Count <= 2)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(rookval, depthChart[team.TeamId][PositionId][numStarters].Value);
                    }

                    if (depthChart[team.TeamId][PositionId].Count <= 3)
                    {
                        backupNeed += 0.35;
                    }
                    else
                    {
                        backupNeed += 0.35 * math.need(rookval, depthChart[team.TeamId][PositionId][numStarters+1].Value);
                    }

                    if (depthChart[team.TeamId][PositionId].Count <= 4)
                    {
                        backupNeed += 0.15;
                    }
                    else
                    {
                        backupNeed += 0.15 * math.need(rookval, depthChart[team.TeamId][PositionId][numStarters+2].Value);
                    }
                }
            }

            double injuryFactor = 0;

            for (int i = 0; i < Math.Min(numStarters, depthChart[team.TeamId][PositionId].Count); i++)
            {
                injuryFactor += (1 - math.injury(depthChart[team.TeamId][PositionId][i].Injury, positionData[depthChart[team.TeamId][PositionId][i].PositionId].DurabilityNeed) / 10) / Math.Min(numStarters, depthChart[team.TeamId][PositionId].Count);
            }

            double backupTemp;

            // At these positions, backups are useful even if the starter isn't likely
            // to be injured
            if (PositionId == (int)MaddenPositions.HB || PositionId == (int)MaddenPositions.WR || PositionId == (int)MaddenPositions.DT || PositionId == (int)MaddenPositions.CB)
            {
                backupTemp = backupNeed * (0.75 * injuryFactor + 0.25);
            }
            else
            {
                backupTemp = backupNeed * injuryFactor;
            }

            if (backupTemp > 1.25) { backupTemp = 1.25; }

            backupNeed = backupTemp * positionData[PositionId].BackupNeed;

            /** Now calculate need for a successor. **/

            int oldest = 0;

            if (depthChart[team.TeamId][PositionId].Count >= numStarters)
            {
                if (numStarters == 2)
                {
                    if (depthChart[team.TeamId][PositionId][0].Age > depthChart[team.TeamId][PositionId][1].Age)
                    {
                        oldest = depthChart[team.TeamId][PositionId][0].Age;
                    }
                    else
                    {
                        oldest = depthChart[team.TeamId][PositionId][1].Age;
                    }
                }
                else
                {
                    oldest = depthChart[team.TeamId][PositionId][0].Age;
                }

                double factor = 1 - 0.2 * (positionData[PositionId].RetirementAge - oldest + (1 / 2) * (team.CON - 5));

                if (factor <= 0)
                {
                    successorNeed = 0;
                } 
                else 
                {
                    if (factor > 1) {factor = 1; }

                    int possibleSuccessor = -1;
                    for (int i = numStarters; i < depthChart[team.TeamId][PositionId].Count; i++) 
                    {
                        if (positionData[PositionId].RetirementAge - depthChart[team.TeamId][PositionId][i].Age + (1/2)*(team.CON - 5) >= 5) {
                            possibleSuccessor = i;
                            break;
                        }
                    }

                    if (possibleSuccessor == -1) 
                    {
                        successorNeed = factor * positionData[PositionId].SuccessorNeed;
                    } 
                    else 
                    {
                        if (rookval > depthChart[team.TeamId][PositionId][possibleSuccessor].Value)
                        {
                            successorNeed = factor * positionData[PositionId].SuccessorNeed * math.need(rookval, depthChart[team.TeamId][PositionId][possibleSuccessor].Value);
                        }
                        else
                        {
                            successorNeed = 0;
                        }
                    }
                }
            }
            else 
            {
                successorNeed = 0;
            }

            double ProbabilityOfStarting = Math.Sqrt(starterNeed);
            double toReturn = (starterNeed + (1/2)*(1 - ProbabilityOfStarting) * backupNeed +
                (1/ team.CON) * (1 - ProbabilityOfStarting) * successorNeed);
            return toReturn;

        }

        public void FixDraftOrder()
        {
            List<int> newDraftOrder = new List<int>();
            List<int> finalDraftOrder = new List<int>();
            Dictionary<int, List<TeamRecord>> Tiers = new Dictionary<int, List<TeamRecord>>();
            Dictionary<int, Dictionary<int, int>> picksToMove = new Dictionary<int, Dictionary<int, int>>();

            for (int i = 0; i < 7; i++)
            {
                picksToMove[i] = new Dictionary<int, int>();
            }

            for (int i = 0; i < model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount; i++)
            {
                DraftPickRecord currentPick = GetDraftPickByNumber(i);

                if (currentPick.CurrentTeamId != currentPick.OriginalTeamId)
                {
                    int round = (int)Math.Floor((decimal)(i / 32));
                    picksToMove[round].Add(currentPick.OriginalTeamId, currentPick.CurrentTeamId);
                }
            }

            model.TeamModel.CalculateWins();
            model.TeamModel.CalculateStrengthOfSchedule();

            for (int i = 0; i <= 18; i++)
            {
                Tiers.Add(i, new List<TeamRecord>());
            }

            foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                TeamRecord record = (TeamRecord)rec;
                if (record.TeamType != 0) { continue; }

                if (record.PlayoffExit == 4)
                {
                    Tiers[17].Add(record);
                }
                else if (record.PlayoffExit == 5)
                {
                    Tiers[18].Add(record);
                }
                else
                {
                    Tiers[record.Wins].Add(record);
                }
            }

            for (int i = 0; i <= 18; i++)
            {
                Tiers[i] = SortByEffectiveSOS(Tiers[i]);
            }

            // Now let's refill the draft order.

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j <= 18; j++)
                {
                    for (int k = 0; k < Tiers[j].Count; k++)
                    {
                        newDraftOrder.Add(Tiers[j][k].TeamId);
                    }
                }

                // Rotate the draft order.  Within each tier, the top team goes
                // to the bottom, the rest rotate up.

                for (int k = 0; k <= 18; k++)
                {
                    if (Tiers[k].Count > 0)
                    {
                        Tiers[k].Add(Tiers[k][0]);
                        Tiers[k].RemoveAt(0);
                    }
                }
            }

            // Ok, now we need to put back the picks where they belong from 
            // RFA transactions.

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (picksToMove[i].ContainsKey(newDraftOrder[i * 32 + j]))
                    {
                        finalDraftOrder.Add(picksToMove[i][newDraftOrder[i * 32 + j]]);
                    }
                    else
                    {
                        finalDraftOrder.Add(newDraftOrder[i * 32 + j]);
                    }
                }
            }

            // Now write the new values back to the draft table
            int count = 0;
            foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
            {
                DraftPickRecord record = (DraftPickRecord)rec;
                if (count != record.PickNumber)
                {
                    Console.WriteLine("SEVERE ERROR!  LINE 115 of DraftModel.cs");
                }

                record.CurrentTeamId = finalDraftOrder[count];
                record.OriginalTeamId = newDraftOrder[count];

                count++;
            }

        }

        private Dictionary<int, RookieRecord> GetFavoriteRookies(int pickNumber)
        {

            Dictionary<int, RookieRecord> favorites = new Dictionary<int,RookieRecord>();
            
            foreach (KeyValuePair<int,TeamRecord> team in model.TeamModel.GetTeamRecords())
            {
                if (team.Value.TeamType != 0) { continue; }

                double HighestValue = 0;
                RookieRecord BestPlayer = null;

                int nextpick = 0;
                for (int i = pickNumber + 1; i < model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount; i++)
                {
                    if (GetDraftPickByNumber(i).CurrentTeamId == team.Value.TeamId)
                    {
                        nextpick = i;
                        break;
                    }
                }

                if (pickNumber == 4 && team.Key == 11)
                {
                    int trash;
                }

                foreach (KeyValuePair<int, RookieRecord> rook in rookies) {
                    if(rook.Value.DraftedTeam < 32) { continue; }
                    if (rook.Value.EffectiveValue(team.Value, pickNumber) > HighestValue && 
                        rook.Value.TotalNeed(team.Value, pickNumber) > positionData[rook.Value.Player.PositionId].Threshold &&
                        rook.Value.values[team.Value.TeamId][(int)RookieRecord.ValueType.NoProg] > 0.75*pickValues[nextpick]) {

                        HighestValue = rook.Value.EffectiveValue(team.Value, pickNumber);
                        BestPlayer = rook.Value;
                    }
                }


                if (pickNumber == 4 && team.Key == 11)
                {
                    int trash;
                }

                if (BestPlayer == null)
                {
                    HighestValue = 0;
                    foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                    {
                        if (rook.Value.DraftedTeam < 32 ||
                            rook.Value.Player.PositionId == (int)MaddenPositions.QB ||
                            rook.Value.Player.PositionId == (int)MaddenPositions.FB ||
                            rook.Value.Player.PositionId == (int)MaddenPositions.P ||
                            rook.Value.Player.PositionId == (int)MaddenPositions.K) { continue; }

                        if (rook.Value.values[team.Value.TeamId][(int)RookieRecord.ValueType.NoProg] > HighestValue)
                        {
                            HighestValue = rook.Value.values[team.Value.TeamId][(int)RookieRecord.ValueType.NoProg];
                            BestPlayer = rook.Value;
                        }
                    }
                }

                favorites.Add(team.Value.TeamId, BestPlayer);

            }
            /*
            foreach(KeyValuePair<int,RookieRecord> rook in favorites) {
                Console.WriteLine(rook.Key + " " + rook.Value.Player.ToString());
            }
            */
            return favorites;
        }

        private void DetermineProjections(int type)
        {
            Dictionary<int, double> AverageValues = new Dictionary<int, double>();
            // First determine average value

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                double total = 0;
                for (int i = 0; i < 32; i++)
                {
                    total += rook.Value.values[i][(int)RookieRecord.ValueType.NoProg];
                }
                AverageValues[rook.Key] = total / 32;
            }

            List<int> SortedAverageValues = new List<int>();

            for (int i = 0; i < rookies.Count; i++)
            {
                double bestRating = 0;
                int bestId = -1;

                foreach (KeyValuePair<int, double> val in AverageValues)
                {
                    if (val.Value > bestRating && !SortedAverageValues.Contains(val.Key))
                    {
                        bestId = val.Key;
                        bestRating = val.Value;
                    }
                }

                SortedAverageValues.Add(bestId);
            }

            for (int i = 0; i < SortedAverageValues.Count; i++)
            {
                rookies[SortedAverageValues[i]].EstimatedPickNumber[type] = i;

                double rank = i;
                string rankstring;

                if (rank < 5)
                {
                    rankstring = "Top 5";
                }
                else if (rank < 10)
                {
                    rankstring = "Top 10";
                }
                else if (rank < 28)
                {
                    rankstring = "1st";
                }
                else if (rank < 36)
                {
                    rankstring = "1st-2nd";
                }
                else if (rank < 60)
                {
                    rankstring = "2nd";
                }
                else if (rank < 68)
                {
                    rankstring = "2nd-3rd";
                }
                else if (rank < 92)
                {
                    rankstring = "3rd";
                }
                else if (rank < 100)
                {
                    rankstring = "3rd-4th";
                }
                else if (rank < 128)
                {
                    rankstring = "4th";
                }
                else if (rank < 160)
                {
                    rankstring = "5th";
                }
                else if (rank < 192)
                {
                    rankstring = "6th";
                }
                else if (rank < 224)
                {
                    rankstring = "7th";
                }
                else
                {
                    rankstring = "Undrafted";
                }

                rookies[SortedAverageValues[i]].EstimatedRound[type] = rankstring;
            }
        }

        private void SetCombineStats(int HumanTeamId, int type)
        {
            Dictionary<int, int> RookieRanks = new Dictionary<int, int>();

            for (int i = 0; i < rookies.Count; i++)
            {
                double bestRating = 0;
                int bestId = -1;

                foreach (KeyValuePair<int, RookieRecord> rook in rookies)
                {
                    if (rook.Value.values[HumanTeamId][(int)RookieRecord.ValueType.NoProg] > bestRating && !RookieRanks.ContainsKey(rook.Key))
                    {
                        bestId = rook.Key;
                        bestRating = rook.Value.values[HumanTeamId][(int)RookieRecord.ValueType.NoProg];
                    }
                }

                RookieRanks.Add(bestId, i);
            }

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {

                rook.Value.CombineNumbers[(int)CombineStat.Forty] =
                    Math.Round(100 *(4.25 + 0.2 * (99 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.SPD]) / 10)) / 100;

                rook.Value.CombineNumbers[(int)CombineStat.Shuttle] =
                    Math.Round(100 * (3.75 + 0.18 * (99 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.ACC]) / 10)) / 100;

                rook.Value.CombineNumbers[(int)CombineStat.Cone] =
                    Math.Round(100 * (6.60 + 0.23 * (99 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.AGI]) / 10)) / 100;

                rook.Value.CombineNumbers[(int)CombineStat.BenchPress] =
                    (Math.Round(44 - 7 * (99 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.STR]) / 10));

                rook.Value.CombineNumbers[(int)CombineStat.Vertical] =
                    42 - 3 * (99 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.JMP]) / 10;

                rook.Value.CombineNumbers[(int)CombineStat.Doctor] =
                    9.9 - rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.INJ] / 10;



                rook.Value.CombineNumbers[(int)CombineStat.RoundGrade] = RookieRanks[rook.Key];
                rook.Value.CombineNumbers[(int)CombineStat.Wonderlic] = rook.Value.ratings[HumanTeamId][type][(int)RookieRecord.Attribute.AWR];
                
                rook.Value.CombineWords[(int)CombineStat.Vertical] = (Math.Round(rook.Value.CombineNumbers[(int)CombineStat.Vertical])).ToString() + "\"";
                rook.Value.CombineWords[(int)CombineStat.Height] = Math.Floor((double)rook.Value.Player.Height / 12) + "'" + rook.Value.Player.Height % 12 + "\"";

                double rank = rook.Value.CombineNumbers[(int)CombineStat.RoundGrade];
                string rankstring;

                if (rank < 5)
                {
                    rankstring = "Top 5";
                }
                else if (rank < 10)
                {
                    rankstring = "Top 10";
                }
                else if (rank < 28)
                {
                    rankstring = "1st";
                }
                else if (rank < 36)
                {
                    rankstring = "1st-2nd";
                }
                else if (rank < 60)
                {
                    rankstring = "2nd";
                }
                else if (rank < 68)
                {
                    rankstring = "2nd-3rd";
                }
                else if (rank < 92)
                {
                    rankstring = "3rd";
                }
                else if (rank < 100)
                {
                    rankstring = "3rd-4th";
                }
                else if (rank < 128)
                {
                    rankstring = "4th";
                }
                else if (rank < 160)
                {
                    rankstring = "5th";
                }
                else if (rank < 192)
                {
                    rankstring = "6th";
                }
                else if (rank < 224)
                {
                    rankstring = "7th";
                }
                else
                {
                    rankstring = "Undrafted";
                }

                rook.Value.CombineWords[(int)CombineStat.RoundGrade] = rankstring;

                double doc = rook.Value.CombineNumbers[(int)CombineStat.Doctor];
                string docgrade;

                if (doc <= 0.3)
                {
                    docgrade = "A+";
                }
                else if (doc <= 0.7)
                {
                    docgrade = "A";
                }
                else if (doc <= 1)
                {
                    docgrade = "A-";
                }
                else if (doc <= 1.3)
                {
                    docgrade = "B+";
                }
                else if (doc <= 1.7)
                {
                    docgrade = "B";
                }
                else if (doc <= 2)
                {
                    docgrade = "B-";
                }
                else if (doc <= 2.3)
                {
                    docgrade = "C+";
                }
                else if (doc <= 2.7)
                {
                    docgrade = "C";
                }
                else if (doc <= 3)
                {
                    docgrade = "C-";
                }
                else if (doc <= 3.3)
                {
                    docgrade = "D+";
                }
                else if (doc <= 3.7)
                {
                    docgrade = "D";
                }
                else if (doc <= 4)
                {
                    docgrade = "D-";
                }
                else
                {
                    docgrade = "F";
                }

                rook.Value.CombineWords[(int)CombineStat.Doctor] = docgrade;
            }
        }

        private void SetInitialNeeds()
        {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                foreach (KeyValuePair<int,TeamRecord> team in model.TeamModel.GetTeamRecords())
                {
                    if (team.Value.TeamType != 0) { continue; }
                    rook.Value.CalculateNeeds(team.Value, depthChart[team.Value.TeamId][rook.Value.Player.PositionId], positionData);
                }
            }
        }

        private void SetRookieValues(int type) {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies) {

                foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords()) {
                    if (team.Value.TeamType != 0) { continue; }

                    rook.Value.values[team.Key][(int)RookieRecord.ValueType.NoProg] =
                        LocalMath.ValueScale * positionData[rook.Value.Player.PositionId].Value(team.Value.DefensiveSystem) * math.valcurve(rook.Value.ratings[team.Key][type][(int)RookieRecord.Attribute.OVR] + math.injury(rook.Value.ratings[team.Key][type][(int)RookieRecord.Attribute.INJ], positionData[rook.Value.Player.PositionId].DurabilityNeed));

                    rook.Value.values[team.Key][(int)RookieRecord.ValueType.WithProg] =
                        LocalMath.ValueScale * positionData[rook.Value.Player.PositionId].Value(team.Value.DefensiveSystem) * math.valcurve(5 * (5 - team.Value.CON) / 2 + rook.Value.ratings[team.Key][type][(int)RookieRecord.Attribute.OVR] + math.injury(rook.Value.ratings[team.Key][type][(int)RookieRecord.Attribute.INJ], positionData[rook.Value.Player.PositionId].DurabilityNeed));

                    // SHOULD BE IMPROVED LATER
                    rook.Value.values[team.Key][(int)RookieRecord.ValueType.Perceived] =
                        rook.Value.values[team.Key][(int)RookieRecord.ValueType.NoProg];
                }
            }
        }

        public void CalculateOveralls(int type)
        {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                rook.Value.CalculateOverall(type);
            }
        }

        private void GenerateInternalDepthChart()
        {
            // Empty out our depth chart
            depthChart = new List<List<List<PlayerRecord>>>();

            // We've got 32 teams
            for (int i = 0; i < 32; i++) {
                depthChart.Add(new List<List<PlayerRecord>>());

                // And 21 positions for each team
                for (int j =0; j < 21; j++) {
                    depthChart[i].Add(new List<PlayerRecord>());
                }
            }
            
            // There seems to be some error with the depth charts.
            // Let's just reconstruct straight from rosters.
            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord record = (PlayerRecord)rec;

                if (model.TeamModel.GetTeamRecord(record.TeamId) == null || model.TeamModel.GetTeamRecord(record.TeamId).TeamType != 0)
                {
                    continue;
                }

                depthChart[record.TeamId][record.PositionId].Add(record);
            }

            for (int i = 0; i < depthChart.Count; i++)
            {
                for (int j = 0; j < depthChart[i].Count; j++)
                {
                    depthChart[i][j] = SortByEffectiveOVR(depthChart[i][j], i, j);
                }
            }

            /*
            int team = 10;
            Console.WriteLine(model.TeamModel.GetTeamRecord(team).CON);

            for (int i = 0; i < depthChart[team].Count; i++) {
                for(int j = 0; j < depthChart[team][i].Count; j++) {
                    PlayerRecord rec = depthChart[team][i][j];
                    Console.WriteLine(rec.PositionId + " " + j + " " + rec.EffectiveOVR + " " + rec.Overall + " " + rec.Injury + " " + math.injury(rec.Injury) + " " + rec.YearsPro);
                }
            }
            */
        }

        private List<PlayerRecord> SortByEffectiveOVR(List<PlayerRecord> records, int teamId, int positionId)
        {
            List<PlayerRecord> newList = new List<PlayerRecord>();

            while (records.Count > 0)
            {
                double bestOverall = 0;
                int bestPlayerIndex = -1;

                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].EffectiveOVR > bestOverall)
                    {
                        bestOverall = records[i].EffectiveOVR;
                        bestPlayerIndex = i;
                    }
                }

                newList.Add(records[bestPlayerIndex]);
                records.RemoveAt(bestPlayerIndex);
            }

            return newList;
        }

        // Set rookie drafted teams to 1023, add to rookies dictionary, 
        // assign corresponding PlayerRecord to RookieRecord
        private void ExtractRookies()
        {
            foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFTED_PLAYERS_TABLE].GetRecords()) {
                RookieRecord record = (RookieRecord)rec;

                record.DraftedTeam = 1023;
                record.DraftPickNumber = 255;

                rookies.Add(record.PlayerId, record);

                record.SetPlayerRecord(model.PlayerModel.GetPlayerByPlayerId(record.PlayerId));
            }
        }

        // This is an incredibly inefficient sort, but we'll only be sorting 4-5 element
        // in general.  I didn't overload the operator because we may want to use that
        // for something more useful, like TEAM OVR or something.

        private List<TeamRecord> SortByEffectiveSOS(List<TeamRecord> tier)
        {
            List<TeamRecord> toReturn = new List<TeamRecord>();
            while(tier.Count > 0) {
                int leastESOS = 10000;
                int leastTeamIndex = -1;

                for(int j = 0; j < tier.Count; j++) {
                    if (tier[j].EffectiveSOS < leastESOS) {
                        leastESOS = tier[j].EffectiveSOS;
                        leastTeamIndex = j;
                    }
                }

                toReturn.Add(tier[leastTeamIndex]);
                tier.RemoveAt(leastTeamIndex);
            }
            
            return toReturn;
        }

        public DraftModel(EditorModel model)
		{
			this.model = model;
		}

        public DraftPickRecord GetDraftPickRecord(int recno)
		{
			return (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecord(recno);
		}

		public DraftPickRecord GetDraftPickByNumber(int pickNumber)
		{
			foreach (TableRecordModel record in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
			{
				if (((DraftPickRecord)record).PickNumber == pickNumber)
				{
					return (DraftPickRecord)record;
				}
			}
			return null;
		}

		public DraftPickRecord CurrentDraftPickRecord
		{
			get
			{
				return (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecord(currentPickIndex);
			}
			set
			{
				DraftPickRecord curr = value;
				//need to set currentPickIndex to the correct index
				int index = 0;
				foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
				{
					if (curr == rec)
					{
						currentPickIndex = index;
						break;
					}

					index++;
				}
			}
		}

		public DraftPickRecord GetNextDraftPickRecord()
		{
			DraftPickRecord record = null;

			int startingindex = currentPickIndex;
			while (true)
			{
				currentPickIndex++;
				if (currentPickIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentPickIndex >= model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount)
				{
					currentPickIndex = -1;
					continue;
				}

				record = (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecord(currentPickIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(model.TeamModel.GetTeamNameFromTeamId(record.CurrentTeamId).Equals(currentTeamFilter)))
					{
						continue;
					}
				}

				//Found one
				break;
			}

			return record;
		}

		public DraftPickRecord GetPreviousDraftPickRecord()
		{
			DraftPickRecord record = null;

			int startingindex = currentPickIndex;
			while (true)
			{
				currentPickIndex--;
				if (currentPickIndex == startingindex)
				{
					//We have looped around
					return null;
				}

				if (currentPickIndex < 0)
				{
					currentPickIndex = model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount;
					continue;
				}

				record = (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecord(currentPickIndex);

				//If this record is marked for deletion then skip it
				if (record.Deleted)
				{
					continue;
				}

				if (currentTeamFilter != null)
				{
					if (!(model.TeamModel.GetTeamNameFromTeamId(record.CurrentTeamId).Equals(currentTeamFilter)))
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
			currentTeamFilter = null;
		}

		public DraftPickRecord CreateNewDraftPickRecord()
		{
			return (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].CreateNewRecord(false);
		}

        private void InitializePositionData() {
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

        private void InitializePickValues()
        {
            pickValues.Add(3000);
            pickValues.Add(2600);
            pickValues.Add(2200);
            pickValues.Add(1900);
            pickValues.Add(1750);
            pickValues.Add(1600);
            pickValues.Add(1500);
            pickValues.Add(1400);
            pickValues.Add(1300);
            pickValues.Add(1250);
            pickValues.Add(1200);
            pickValues.Add(1150);
            pickValues.Add(1100);
            pickValues.Add(1050);
            pickValues.Add(1000);
            pickValues.Add(950);
            pickValues.Add(900);
            pickValues.Add(870);
            pickValues.Add(840);
            pickValues.Add(815);
            pickValues.Add(790);
            pickValues.Add(765);
            pickValues.Add(740);
            pickValues.Add(720);
            pickValues.Add(700);
            pickValues.Add(680);
            pickValues.Add(660);
            pickValues.Add(645);
            pickValues.Add(630);
            pickValues.Add(615);
            pickValues.Add(600);
            pickValues.Add(585);
            pickValues.Add(570);
            pickValues.Add(555);
            pickValues.Add(540);
            pickValues.Add(525);
            pickValues.Add(510);
            pickValues.Add(495);
            pickValues.Add(480);
            pickValues.Add(465);
            pickValues.Add(455);
            pickValues.Add(445);
            pickValues.Add(435);
            pickValues.Add(425);
            pickValues.Add(415);
            pickValues.Add(405);
            pickValues.Add(395);
            pickValues.Add(385);
            pickValues.Add(375);
            pickValues.Add(365);
            pickValues.Add(355);
            pickValues.Add(345);
            pickValues.Add(335);
            pickValues.Add(325);
            pickValues.Add(315);
            pickValues.Add(305);
            pickValues.Add(295);
            pickValues.Add(285);
            pickValues.Add(275);
            pickValues.Add(265);
            pickValues.Add(260);
            pickValues.Add(255);
            pickValues.Add(250);
            pickValues.Add(245);
            pickValues.Add(240);
            pickValues.Add(235);
            pickValues.Add(230);
            pickValues.Add(225);
            pickValues.Add(220);
            pickValues.Add(215);
            pickValues.Add(210);
            pickValues.Add(205);
            pickValues.Add(200);
            pickValues.Add(196);
            pickValues.Add(192);
            pickValues.Add(188);
            pickValues.Add(184);
            pickValues.Add(178);
            pickValues.Add(174);
            pickValues.Add(170);
            pickValues.Add(166);
            pickValues.Add(162);
            pickValues.Add(158);
            pickValues.Add(154);
            pickValues.Add(150);
            pickValues.Add(146);
            pickValues.Add(142);
            pickValues.Add(138);
            pickValues.Add(134);
            pickValues.Add(130);
            pickValues.Add(126);
            pickValues.Add(122);
            pickValues.Add(118);
            pickValues.Add(114);
            pickValues.Add(110);
            pickValues.Add(106);
            pickValues.Add(102);
            pickValues.Add(98);
            pickValues.Add(94);
            pickValues.Add(92);
            pickValues.Add(90);
            pickValues.Add(88);
            pickValues.Add(86);
            pickValues.Add(84);
            pickValues.Add(82);
            pickValues.Add(80);
            pickValues.Add(78);
            pickValues.Add(76);
            pickValues.Add(74);
            pickValues.Add(72);
            pickValues.Add(70);
            pickValues.Add(68);
            pickValues.Add(66);
            pickValues.Add(64);
            pickValues.Add(62);
            pickValues.Add(60);
            pickValues.Add(58);
            pickValues.Add(56);
            pickValues.Add(54);
            pickValues.Add(52);
            pickValues.Add(50);
            pickValues.Add(48);
            pickValues.Add(46);
            pickValues.Add(44);
            pickValues.Add(43);
            pickValues.Add(42);
            pickValues.Add(41);
            pickValues.Add(40);
            pickValues.Add(40);
            pickValues.Add(39.5);
            pickValues.Add(39);
            pickValues.Add(38.5);
            pickValues.Add(38);
            pickValues.Add(37.5);
            pickValues.Add(37);
            pickValues.Add(36.5);
            pickValues.Add(36);
            pickValues.Add(35.5);
            pickValues.Add(35);
            pickValues.Add(34.5);
            pickValues.Add(34.1);
            pickValues.Add(33.7);
            pickValues.Add(33.3);
            pickValues.Add(32.8);
            pickValues.Add(32.4);
            pickValues.Add(32);
            pickValues.Add(31.6);
            pickValues.Add(31.2);
            pickValues.Add(30.8);
            pickValues.Add(30.4);
            pickValues.Add(30);
            pickValues.Add(29.6);
            pickValues.Add(29.2);
            pickValues.Add(28.8);
            pickValues.Add(28.4);
            pickValues.Add(28);
            pickValues.Add(27.6);
            pickValues.Add(27.2);
            pickValues.Add(26.8);
            pickValues.Add(26.4);
            pickValues.Add(26);
            pickValues.Add(25.6);
            pickValues.Add(25.2);
            pickValues.Add(24.8);
            pickValues.Add(24.4);
            pickValues.Add(24);
            pickValues.Add(23.6);
            pickValues.Add(23.2);
            pickValues.Add(22.8);
            pickValues.Add(22.4);
            pickValues.Add(22);
            pickValues.Add(21.6);
            pickValues.Add(21.2);
            pickValues.Add(20.8);
            pickValues.Add(20.4);
            pickValues.Add(20);
            pickValues.Add(19.6);
            pickValues.Add(19.2);
            pickValues.Add(18.8);
            pickValues.Add(18.4);
            pickValues.Add(18);
            pickValues.Add(17.6);
            pickValues.Add(17.2);
            pickValues.Add(16.8);
            pickValues.Add(16.4);
            pickValues.Add(16);
            pickValues.Add(15.75);
            pickValues.Add(15.5);
            pickValues.Add(15.25);
            pickValues.Add(15);
            pickValues.Add(14.75);
            pickValues.Add(14.5);
            pickValues.Add(14);
            pickValues.Add(13.7);
            pickValues.Add(13.4);
            pickValues.Add(13.1);
            pickValues.Add(12.9);
            pickValues.Add(12.6);
            pickValues.Add(12.3);
            pickValues.Add(12);
            pickValues.Add(11.7);
            pickValues.Add(11.4);
            pickValues.Add(11.1);
            pickValues.Add(10.8);
            pickValues.Add(10.5);
            pickValues.Add(10.2);
            pickValues.Add(9.9);
            pickValues.Add(9.6);
            pickValues.Add(9.3);
            pickValues.Add(9);
            pickValues.Add(8.7);
            pickValues.Add(8.4);
            pickValues.Add(8.1);
            pickValues.Add(7.8);
            pickValues.Add(7.5);
            pickValues.Add(7.2);
            pickValues.Add(6.9);
            pickValues.Add(6.6);
            pickValues.Add(6.3);
            pickValues.Add(6);
            pickValues.Add(5.7);
            pickValues.Add(5.4);
            pickValues.Add(5.1);
            pickValues.Add(4.8);
            pickValues.Add(4.5);
            pickValues.Add(4.2);
            pickValues.Add(4);
            pickValues.Add(3.8);
            pickValues.Add(3.6);
            pickValues.Add(3.4);
            pickValues.Add(3.2);
            pickValues.Add(3);
            pickValues.Add(2.8);
            pickValues.Add(2.6);
            pickValues.Add(2.5);
            pickValues.Add(2.4);
            pickValues.Add(2.3);
            pickValues.Add(2.2);
            pickValues.Add(2.1);
            pickValues.Add(2.0);
            pickValues.Add(1.9);
            pickValues.Add(1.8);
            pickValues.Add(1.7);
            pickValues.Add(1.6);
            pickValues.Add(1.5);
            pickValues.Add(1.4);
            pickValues.Add(1.3);
            pickValues.Add(1.2);
            pickValues.Add(1.1);
            pickValues.Add(1.0);
            pickValues.Add(0.9);
            pickValues.Add(0.8);
            pickValues.Add(0.7);
            pickValues.Add(0.6);
            pickValues.Add(0.5);
        }

        private void AssignRookieScoutedAttributes()
        {
            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                for (int i = 0; i < 32; i++)
                {
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.OVR] =
                        rook.Value.Player.Overall;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.INJ] =
                        rook.Value.Player.Injury;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.SPD] =
                        rook.Value.Player.Speed;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.ACC] =
                        rook.Value.Player.Acceleration;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.AGI] =
                        rook.Value.Player.Agility;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.JMP] =
                        rook.Value.Player.Jumping;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.AWR] =
                        rook.Value.Player.Awareness;
                    rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][(int)RookieRecord.Attribute.STR] =
                        rook.Value.Player.Strength;
                }
            }
        }
    }

    public class Position
    {
        private int value34;
        private int value43;
        private int valueCover2;
        private int starters43;
        private int starters34;

        public int RetirementAge;
        public double SuccessorNeed;
        public double BackupNeed;
        public double Threshold;
        public double DurabilityNeed;

        public Position(int v43, int v34, int c2, double suc, int ra, double back, double thresh, int s43, int s34, double dn)
        {
            value43 = v43;
            value34 = v34;
            valueCover2 = c2;
            RetirementAge = ra;
            SuccessorNeed = suc;
            BackupNeed = back;
            Threshold = thresh;
            starters43 = s43;
            starters34 = s34;
            DurabilityNeed = dn;
        }

        public int Starters(int system)
        {
           if (system == (int)TeamRecord.Defense.Front34)
            {
                return starters34;
            } else
            {
                return starters43;
            }
        }

        public int Value(int system)
        {
            if (system == (int)TeamRecord.Defense.Front43)
            {
                return value43;
            }
            else if (system == (int)TeamRecord.Defense.Front34)
            {
                return value34;
            }
            else if (system == (int)TeamRecord.Defense.Cover2)
            {
                return valueCover2;
            }
            return -1;
        }
    }
}
