/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com and spin16
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
using System.Linq;
using System.Text;
using System.IO;

using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;

namespace MaddenEditor.Core
{
    public class TradeOffer
    {
        public int status = 0;
        public int HigherTeam;
        public int LowerTeam;
        public int pickNumber;

        public double MinAccept;
        public double MaxGive;

        public bool lastWasStrike = false;

        public int higherStrikes = 0;
        public int lowerStrikes = 0;

        public bool biddingWar = false;
        public bool allowFutureHighPicks = false;
        public bool allowFuturePicksFromLower = true;
        public bool allowFuturePicksFromHigher = true;
        public bool allowMultipleHighPicks = false;
        public int MaxPicksFromLower = 2;

        private DraftModel dm;

        public List<int> PicksFromLower = new List<int>();
        public List<int> PicksFromHigher = new List<int>();

        public List<double> offersFromHigher = new List<double>();
        public List<double> offersFromLower = new List<double>();

        public List<int> higherAvailable = new List<int>();
        public List<int> lowerAvailable = new List<int>();

        List<int> tempPicksFromLower;
        List<int> tempPicksFromHigher;

        double target;

        public TradeOffer(int higherId, int lowerId, DraftModel model)
        {
            HigherTeam = higherId;
            LowerTeam = lowerId;
            dm = model;
        }

        private int numHighPicks()
        {
            int toRet = 0;
            foreach (int pick in tempPicksFromLower)
            {
                if (pick < 1000)
                {
                    if (dm.pickValues[pick] > 200)
                    {
                        toRet++;
                    }
                }
                else
                {
                    if (dm.futureValues(pick - 1000, dm.model.TeamModel.GetTeamRecord(LowerTeam).CON) > 200)
                    {
                        toRet++;
                    }
                }
            }
            return toRet;
        }

        public int AddClosestPick(bool fromLower)
        {
            if (fromLower)
            {
                // Add the pick that gets us closest to the mark.

                int bestPick = -1;
                double bestDifference = target;
                foreach (int pick in lowerAvailable)
                {
                    if (Math.Abs(target - dm.pickValues[pick]) < bestDifference && !tempPicksFromLower.Contains(pick))
                    {
                        if (!allowMultipleHighPicks && dm.pickValues[pick] > 200 && numHighPicks() >= 2)
                        {
                            continue;
                        }

                        bestPick = pick;
                        bestDifference = Math.Abs(target - dm.pickValues[pick]);
                    }
                }
                lowerAvailable.Sort();

                // If the closest pick wasn't our most valuable remaining pick, add this one
                // and return.  Otherwise, allow possibility of adding a future pick.
                int highestAvailablePick = -1;
                foreach (int pick in lowerAvailable)
                {
                    if (!tempPicksFromLower.Contains(pick))
                    {
                        if (!allowMultipleHighPicks && dm.pickValues[pick] > 200 && numHighPicks() >= 2)
                        {
                            continue;
                        }

                        highestAvailablePick = pick;
                        break;
                    }
                }


                if (!allowFuturePicksFromLower || (bestPick != -1 && bestPick != highestAvailablePick))
                {
                    if (bestPick != -1)
                    {
                        tempPicksFromLower.Add(bestPick);
                    }
                    return bestPick;
                }

                // If there was no such pick, add the most valuable pick.
                int round = (int)Math.Floor((double)pickNumber / 32.0) + 1;

                int startRound = -1;
                if (allowFutureHighPicks)
                {
                    startRound = round;
                }
                else
                {
                    startRound = round + 2;
                }

                int con = dm.model.TeamModel.GetTeamRecord(LowerTeam).CON;
                for (int i = startRound; i < 8; i++)
                {
                    if (Math.Abs(target - dm.futureValues(i, con)) < bestDifference && !tempPicksFromLower.Contains(1000 + i)
                        && !dm.futureTradedPicks[LowerTeam].ContainsKey(i))
                    {
                        bestPick = i + 1000;
                        bestDifference = Math.Abs(target - dm.futureValues(i, con));
                    }
                }

                if (bestPick != -1)
                {
                    tempPicksFromLower.Add(bestPick);
                }

                return bestPick;
            }
            else
            {
                // Add the pick that gets us closest to the mark.

                int bestPick = -1;
                double bestDifference = target;
                foreach (int pick in higherAvailable)
                {
                    if (Math.Abs(target - dm.pickValues[pick]) < bestDifference && !tempPicksFromHigher.Contains(pick))
                    {
                        bestPick = pick;
                        bestDifference = Math.Abs(target - dm.pickValues[pick]);
                    }
                }
                higherAvailable.Sort();

                // If the closest pick wasn't our most valuable remaining pick, add this one
                // and return.  Otherwise, allow possibility of adding a future pick.
                int highestAvailablePick = -1;
                foreach (int pick in higherAvailable)
                {
                    if (!tempPicksFromHigher.Contains(pick))
                    {
                        highestAvailablePick = pick;
                        break;
                    }
                }

                if (!allowFuturePicksFromHigher || (bestPick != -1 && bestPick != highestAvailablePick))
                {
                    if (bestPick != -1)
                    {
                        tempPicksFromHigher.Add(bestPick);
                    }
                    return bestPick;
                }

                // If there was no such pick, add the most valuable pick.
                int startRound = (int)Math.Floor((double)pickNumber / 32.0) + 3;

                int con = dm.model.TeamModel.GetTeamRecord(HigherTeam).CON;
                for (int i = startRound; i < 8; i++)
                {
                    if (Math.Abs(target - dm.futureValues(i, con)) < bestDifference && !tempPicksFromHigher.Contains(1000 + i)
                        && !dm.futureTradedPicks[HigherTeam].ContainsKey(i))
                    {
                        bestPick = i + 1000;
                        bestDifference = Math.Abs(target - dm.futureValues(i, con));
                    }
                }

                if (bestPick != -1)
                {
                    tempPicksFromLower.Add(bestPick);
                }

                return bestPick;
            }
        }

        public double SetMinTake()
        {
            if (biddingWar)
            {
                foreach (TradeOffer to in dm.tradeOffers.Values)
                {
                    if (to.status == (int)TradeOfferStatus.PendingAccept)
                    {
                        MinAccept = to.MinAccept;
                        return MinAccept;
                    }
                }

                //Trace.Writeline("\n\nSEVERE ERROR!\n\n");
                return -1;
            }

            // Find minimum value to trade down
            double mintake;
            double favoriteEffectiveValue = dm.favorites[HigherTeam].EffectiveValue(dm.model.TeamModel.GetTeamRecord(HigherTeam), pickNumber, dm.dcr.awarenessAdjust);
            double favoriteValue = dm.favorites[HigherTeam].AverageValue(dm.model.TeamModel.GetTeamRecord(HigherTeam), dm.dcr.awarenessAdjust);

            if (favoriteEffectiveValue * 1.5 < dm.pickValues[pickNumber])
            {
                mintake = favoriteEffectiveValue;
            }
            else
            {
                mintake = favoriteEffectiveValue * Math.Tanh(favoriteValue / dm.pickValues[pickNumber]) / Math.Tanh(1.0);

                double wantfrac = 1;

                PicksFromLower.Sort();
                int nextpick = PicksFromLower[0];

                foreach (KeyValuePair<int, RookieRecord> rook in dm.rookies)
                {
                    if (rook.Value.AverageNeed(dm.model.TeamModel.GetTeamRecord(HigherTeam), pickNumber, dm.dcr.awarenessAdjust) <= dm.positionData[rook.Value.Player.PositionId].Threshold ||
                        // replace with statement on league projected position
                        (nextpick < 1000 && rook.Value.AverageValue(dm.model.TeamModel.GetTeamRecord(HigherTeam), dm.dcr.awarenessAdjust) <= 0.75 * dm.pickValues[nextpick])) { continue; }

                    if (rook.Value.PlayerID == dm.favorites[HigherTeam].PlayerID || rook.Value.DraftPickTeam < 32)
                    {
                        continue;
                    }

                    double tempEV = rook.Value.EffectiveValue(dm.model.TeamModel.GetTeamRecord(HigherTeam), pickNumber, dm.dcr.awarenessAdjust);
                    double tempwantfrac = Math.Tanh((favoriteEffectiveValue - tempEV) / tempEV);

                    if (tempwantfrac > 0.95) { tempwantfrac = 1; }
                    wantfrac -= wantfrac * (1 - tempwantfrac) / 2.0;
                }

                double probabilityTaken = dm.probs[LowerTeam][dm.favorites[HigherTeam].PlayerID];
                double probabilityRemaining = 1 - probabilityTaken;

                foreach (TableRecordModel rec in dm.model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                {
                    DraftPickRecord record = (DraftPickRecord)rec;

                    if (record.PickNumber <= pickNumber) { continue; }
                    if (record.PickNumber == nextpick) { break; }

                    probabilityTaken += dm.probs[record.CurrentTeamId][dm.favorites[HigherTeam].PlayerID] * probabilityRemaining;
                    probabilityRemaining *= (1.0 - dm.probs[record.CurrentTeamId][dm.favorites[HigherTeam].PlayerID]);

                    if (probabilityRemaining == 0) { break; }
                }

                if (probabilityTaken == 1 && wantfrac == 1)
                {
                    mintake = 100000;
                }
                else
                {
                    mintake *= 1.0 / Math.Sqrt(Math.Pow(1.0 - probabilityTaken, 2.0) + Math.Pow(1.0 - wantfrac, 2.0));
                }

                if (Double.IsNaN(mintake))
                {
                    mintake = 10000;
                }
            }

            if (Double.IsNaN(mintake))
            {
                mintake = 10000;
            }

            if (mintake < dm.pickValues[pickNumber + 1] + 1)
            {
                MinAccept = dm.pickValues[pickNumber + 1] + 1;
            }
            else
            {
                MinAccept = mintake;
            }

            return mintake;
        }
        
        public double makeCounterOffer(double value, bool fromHigher)
        {
            target = value;
            tempPicksFromLower = new List<int>();
            tempPicksFromHigher = new List<int>();

            // Start the offer with the most valuable pick from the lower team.
            while (Math.Abs(target) > 0.03 * value &&
                (tempPicksFromHigher.Count <= 2 || tempPicksFromLower.Count <= MaxPicksFromLower) &&
                !((target > 0 && tempPicksFromLower.Count >= MaxPicksFromLower) || (target < 0 && tempPicksFromHigher.Count >= 2)))
            {
                int addedPick = AddClosestPick(target > 0);
                double thisValue = 0;

                if (addedPick == -1)
                {
                    thisValue = 0;
                    break;
                }
                else if (addedPick < 1000)
                {
                    thisValue = dm.pickValues[addedPick];
                }
                else
                {
                    if (target > 0)
                    {
                        thisValue = dm.futureValues(addedPick - 1000, dm.model.TeamModel.GetTeamRecord(LowerTeam).CON);
                    }
                    else
                    {
                        thisValue = dm.futureValues(addedPick - 1000, dm.model.TeamModel.GetTeamRecord(HigherTeam).CON);
                    }
                }

                if (target > 0)
                {
                    target -= thisValue;
                }
                else
                {
                    target += thisValue;
                }
            }

            PicksFromHigher = tempPicksFromHigher;
            PicksFromLower = tempPicksFromLower;

            if (PicksFromLower.Count == 0)
            {
                offersFromLower.Clear();
                MinAccept = 10000;
                return 10000;
            }

            if (PicksFromLower.Count == 1 && PicksFromLower[0] < 1000)
            {
                // Only offering one pick from this draft.  Remove it, make another
                // counter offer, put it back at the top of the stack, and 
                // return whatever the alternate counter offer is.

                lowerAvailable.Sort();
                int pickToPush = lowerAvailable[0];
                lowerAvailable.RemoveAt(0);

                double toReturn = makeCounterOffer(value, fromHigher);

                lowerAvailable.Insert(0, pickToPush);
                return toReturn;
            }



            double tempValue = 0;

            foreach (int pick in PicksFromLower)
            {
                if (pick < 1000)
                {
                    tempValue += dm.pickValues[pick];
                }
                else
                {
                    tempValue += dm.futureValues(pick - 1000, dm.model.TeamModel.GetTeamRecord(LowerTeam).CON);
                }
            }

            foreach (int pick in PicksFromHigher)
            {
                if (pick < 1000)
                {
                    tempValue -= dm.pickValues[pick];
                }
                else
                {
                    tempValue -= dm.futureValues(pick - 1000, dm.model.TeamModel.GetTeamRecord(HigherTeam).CON);
                }
            }

            if (fromHigher)
            {
                offersFromHigher.Add(tempValue);
                return 0;
            }
            else
            {
                offersFromLower.Add(tempValue);
                return SetMinTake();
            }
        }
    }

    public enum MaddenPositionGroups
    {
        OL = 0,
        OT,
        OG,
        DL,
        DE,
        LB,
        OLB,
        DB,
        S
    }

    public enum TradeOfferStatus
    {
        HigherResponsePending = 0,
        LowerResponsePending,
        Rejected,
        Accepted,
        PendingAccept
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
            }
            else
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
	    
    public class DraftModel
	{
		private int currentPickIndex = 0;
		/** The current Team Filter */
		private string currentTeamFilter = null;
		/** Reference to our EditorModel */
		public EditorModel model = null;
		/** Rookies, indexed by PlayerId **/
		public Dictionary<int, RookieRecord> rookies = new Dictionary<int, RookieRecord>();

		public List<double> pickValues = new List<double>();
		public int HumanTeamId;
		public const double TotalScoutingHours = 1000.0;

		public Dictionary<int, Position> positionData = new Dictionary<int, Position>();

		private LocalMath math;
		public DepthChartRepairer dcr;
		int numrooks;
        private int recommendation;

		// true if the team just traded up; false otherwise.
		private bool PreventTrade = false;
		private bool humanRejected = false;
        private bool pickssaved = false;

		public Dictionary<int, TradeOffer> tradeOffers;
		public Dictionary<int, double> BestOffers;
		public Dictionary<int, Dictionary<int, double>> probs;
		public Dictionary<int, RookieRecord> favorites;

		public TradeUpForm tradeUpForm;
		public TradeDownForm tradeDownForm;
		public DraftForm df;

        // futureTradedPicks[fromTeam][round] = toTeam;
		public Dictionary<int, Dictionary<int, int>> futureTradedPicks = new Dictionary<int, Dictionary<int, int>>();

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

		/* To accomodate the fact that players might be put at a position
		 * other than their native one, we need to know what a player's
		 * value is at positions other than his own.  To do this, we
		 * associate values on the current roster with depth chart slots
		 * rather than players.
		 * 
		 * depthChartValues[teamId][PositionId][Depth] */
		private List<List<List<double>>> depthChartValues;

        // Adding values for coaches scouting ability
        // teamscout[teamID][coachposition][coachrecord]
        public static bool enhance = false;
        public List<List<List<CoachRecord>>> teamscout = new List<List<List<CoachRecord>>>();
        
        // New additions
        public Dictionary<int, int> OriginalRatings = new Dictionary<int, int>();       
        
        public int ResumePick = 0;
        public int ResumeRound = 0;
        public int ResumeIndex = 0;

        public DraftModel(EditorModel model)
        {
            this.model = model;
            math = new LocalMath(model.MadVersion);
        }

        
        
        public void InitializeDraft(int htid, DraftConfigForm draftConfigForm, string customclass)
        {
            HumanTeamId = htid;
            /*
                        InitializePositionData();
                        InitializePickValues();
                        dcr = new DepthChartRepairer(model, positionData);
            */
            if (customclass != null)
            {
                ImportRookies(customclass);
            }            

            string analysis = AnalyzeDraftClass(true);

            bool repairRooks = false;

            if (recommendation <= 0)
            {
                int action = draftConfigForm.promptFix(analysis, recommendation);

                if (action == -1)
                {
                    return;
                }

                else if (action == 1)
                {
                    repairRooks = true;
                }
            }

            
            // Record each rookie's starting OVR, and recalc overall based on default values
            foreach (PlayerRecord play in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (play.YearsPro == 0)
                    OriginalRatings.Add(play.PlayerId, play.Overall);
                play.CalculateOverallRating(play.PositionId);
            }

            //  Clear out draft class draft position and ratings info
            model.PlayerModel.ClearDraftClass(ResumeRound, ResumeIndex, ResumePick);

            dcr = new DepthChartRepairer(model, positionData);

            //draftConfigForm.ReportProgress(25);
            ExtractRookies();
            //draftConfigForm.ReportProgress(35);

            if (repairRooks)
            {
                RepairRookies();
            }

            //draftConfigForm.ReportProgress(45);
            //            DumpRookies();

            depthChart = dcr.ReorderDepthCharts(true);

            depthChartValues = new List<List<List<double>>>();

            //We'll say 50% of the loading is done in this part
            for (int i = 0; i < 32; i++)
            {
                futureTradedPicks.Add(i, new Dictionary<int, int>());

                depthChartValues.Add(new List<List<double>>());
                for (int j = 0; j < 21; j++)
                {
                    depthChartValues[i].Add(new List<double>());
                }
                CalculateDepthChartValues(i);
            }

            // model.PlayerModel.ComputeEffectiveOVRs(this);
            CalculateActualProjections();
            DumpRookies();

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

        
        // Setup each team's coaching staff.
        public void InitCoachScouting()
        {
            
            for (int i = 0; i< 32; i++)
            {
                teamscout.Add(new List<List<CoachRecord>>());
                
                for (int j = 0; j < 4; j++)
                {
                    teamscout[i].Add(new List<CoachRecord>());
                }
            }
            // Initialize each team's scouting ability.
            foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                CoachRecord coach = (CoachRecord)rec;

                // Is this a team coach
                if (coach.TeamId >= 0 && coach.TeamId <= 31)
                {
                    // Add to our list of coaches
                    teamscout[coach.TeamId][coach.Position].Add(coach);                    
                }                    
            }
        }

        public void customrookies()
        {
            List<PlayerRecord> customdc = new List<PlayerRecord>();
            int ovrmin = 40;
            int ovrmax = 85;
            int impact = rand.Next(5, 10);
            int grade_a = rand.Next(10, 20);
            int grade_b = 48 - (impact + grade_a);
            int grade_c = 128 - (impact + grade_a + grade_b);
            int grade_d = 192 - (impact + grade_a + grade_b + grade_c);
            int grade_f = 257 - (impact + grade_a + grade_b + grade_c + grade_d);
            int tempovr = 0;



            // Generate customized draft class.
            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord player = (PlayerRecord)rec;
                if (player.YearsPro == 0 && player.FirstName != "New" && player.LastName != "Player")
                    customdc.Add(player);
            }

            // Sort the class from lowest OVR to highest, helps with not having
            // several players of same position rated alike.
           
            customdc.Sort(delegate(PlayerRecord p1, PlayerRecord p2)
                { 
                    return p1.Overall.CompareTo(p2.Overall);
                }
                );
          
            for (int i = 0; i < customdc.Count; i++)
            {

                if (impact == 0)
                    ovrmax = 79;
                if (grade_f == 0)
                    ovrmin = 51;

                tempovr = rand.Next(ovrmin, ovrmax);
                                
                if (customdc[i].Overall != tempovr)
                {
                    // Change the OVR of the player
                    // make separate method for this?
                    customdc[i] = customOVR(customdc[i], tempovr);

                }

                if (tempovr >= 80 && tempovr <= 85)
                    impact--;
                if (tempovr >= 76 && tempovr <= 79)
                    grade_a--;
                if (tempovr >= 68 && tempovr <= 75)
                    grade_b--;
                if (tempovr >= 60 && tempovr <= 67)
                    grade_c--;
                if (tempovr >= 51 && tempovr <= 59)
                    grade_d--;
                if (tempovr <= 50)
                    grade_f--;


            }

        }

        // change to work with rookie record
        public PlayerRecord customOVR(PlayerRecord changeOVR, int newovr)
        {
            decimal tempovr = Convert.ToDecimal(newovr - 28.0);
            decimal totalchange = tempovr;
            decimal thischange = 0;
            int change = rand.Next(1, 100);
            int parts = rand.Next(1, 3);
            switch (changeOVR.PositionId)
            {
                case (int)MaddenPositions.QB:
                    // make type changes 70% of the values
                    // and other ones, 30% total, to a max of 3 changes.
                    // based on random "parts"
                    if (change >= 1 && change <= 33)
                    {
                        changeOVR.Tendency = 1;
                        // scrambler...
                        // to change SPD,AGI,BTK,ACC
                        // speed is around 12% of his total pts. random 9-15% for rookie topped
                        // out at 85 ovr, speed could be as much as 93.
                        // need to keep track of points changed...
                        thischange = tempovr * (Convert.ToDecimal(rand.Next(9, 14)) + Convert.ToDecimal(rand.NextDouble())) / 100;
                        totalchange -= System.Math.Ceiling(thischange);
                        changeOVR.Speed = (int)System.Math.Floor((thischange * 5) + 50);

                        thischange = tempovr * (Convert.ToDecimal(rand.Next(2, 3)) + Convert.ToDecimal(rand.NextDouble())) / 100;
                        totalchange -= System.Math.Ceiling(thischange);
                        changeOVR.BreakTackle = (int)System.Math.Floor((thischange * (decimal)12.5) + 50);

                        thischange = tempovr * (Convert.ToDecimal(rand.Next(4, 5)) + Convert.ToDecimal(rand.NextDouble()) + (decimal).50) / 100;
                        totalchange -= System.Math.Ceiling(thischange);
                        changeOVR.Agility = (int)System.Math.Floor((thischange * (decimal)12.50) + 50);

                        // acceleration doesn't seem to be part of the equation
                        changeOVR.Acceleration = (int)rand.Next(65, 90);
                        //totalchange = System.Math.Floor(totalchange);
                        //Done with the big 3 for scrambler, now use whatever points are left
                        //for the rest of his abilities.
                        // we could be above where we should be for total if we
                        // got top values for all random above, since we already
                        // generated a random (3) we will use it to determine which 
                        // stats lose out to make the difference...
                        // of the points left thp=38% tha=45% awr=17% (max29%) max awr for rookie=80

                        int awr = 0;
                        int tha = 0;
                        int thp = 0;
                        if (parts == 1)
                        {
                            // THP takes the hit
                            awr = rand.Next(5, 29);
                            tha = rand.Next(30, 60);
                            thp = 100 - awr - tha;
                        }
                        if (parts == 2)
                        {
                            // THA takes the hit
                            awr = rand.Next(5, 29);
                            thp = rand.Next(30,55);
                            tha = 100 - awr - thp;
                        }
                        if (parts == 3)
                        {
                            // AWR takes hit...which stat goes first
                            int first = rand.Next(1, 10);
                            if (first <= 5)
                            {
                                thp = rand.Next(30, 55);
                                tha = rand.Next(71 - thp, 94 - thp);
                            }
                            else
                            {
                                tha = rand.Next(30, 60);
                                thp = rand.Next(71 - thp, 94 - thp);
                            }
                            awr = 100 - thp - tha;
                        }

                        thischange = totalchange * (Convert.ToDecimal(thp) + Convert.ToDecimal(rand.NextDouble())) / 100;
                        //totalchange -= thischange;
                        changeOVR.ThrowPower = (int)System.Math.Floor((thischange * 10 / (decimal)4.9) + 50);

                        thischange = tempovr * (Convert.ToDecimal(tha) + Convert.ToDecimal(rand.NextDouble())) / 100;
                        //totalchange -= thischange;
                        changeOVR.ThrowAccuracy = (int)System.Math.Floor((thischange * 10 / (decimal)5.8) + 50);

                        thischange = tempovr * (Convert.ToDecimal(awr) + Convert.ToDecimal(rand.NextDouble())) / 100;
                        //totalchange -= thischange;
                        changeOVR.Awareness = (int)System.Math.Floor((thischange * (decimal)2.5) + 50);
                        changeOVR.Overall = changeOVR.CalculateOverallRating(changeOVR.PositionId, false);
                    }
        


                    if (change >= 34 && change <= 67)
                    {
                        // pocket
                    }
                    if (change >= 68 && change <= 100)
                    {
                        // balanced
                    }
                    








                    break;
                    
                case (int)MaddenPositions.HB:
                    break;

                case (int)MaddenPositions.FB:
                    break;



                    



            }
            return changeOVR;

        }
        
		public bool SavePicks(string saveFile)
		{
            if (pickssaved)
                return false;

            File.Delete(saveFile);
			FileStream fs = new FileStream(saveFile, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);

			foreach (KeyValuePair<int, Dictionary<int, int>> dick in futureTradedPicks)
			{
				foreach (KeyValuePair<int, int> innerdick in dick.Value)
				{
					sw.WriteLine(innerdick.Key + " " + dick.Key + " " + innerdick.Value);
				}
			}

			sw.Close();
			fs.Close();
            pickssaved = true;
            return true;
		}

		private void RepairRookies()
		{
			List<int> found = new List<int>();
			double randMax = 0;

			for (int i = 0; i < rookies.Count; i++)
			{
				double bestEOR = 0;
				RookieRecord rook = null;
				foreach (RookieRecord r in rookies.Values)
				{
					if (RookEOR(r) > bestEOR && !found.Contains(r.PlayerID))
					{
						rook = r;
						bestEOR = RookEOR(r);
					}
				}
				found.Add(rook.PlayerID);

				//Trace.Writeline(i + " " + rook.Player.ToString() + " " + RookEOR(rook));

               
				// FB's are way too low.  INCREASE their ratings./
				if (rook.Player.PositionId == (int)MaddenPositions.FB)
				{
					int bump = 3;

					rook.Player.Speed += Math.Min(rand.Next(bump), 99 - rook.Player.Speed);
					rook.Player.Agility += Math.Min(rand.Next(bump), 99 - rook.Player.Agility);
					rook.Player.Acceleration += Math.Min(rand.Next(bump), 99 - rook.Player.Acceleration);
					rook.Player.Jumping += Math.Min(rand.Next(bump), 99 - rook.Player.Jumping);

					rook.Player.Strength += Math.Min(rand.Next(bump), 99 - rook.Player.Strength);

					rook.Player.Awareness += Math.Min(rand.Next(bump), 99 - rook.Player.Awareness);
					rook.Player.Tackle += Math.Min(rand.Next(bump), 99 - rook.Player.Tackle);

					rook.Player.Catching += Math.Min(rand.Next(bump), 99 - rook.Player.Catching);
					rook.Player.Carrying += Math.Min(rand.Next(bump), 99 - rook.Player.Carrying);
					rook.Player.BreakTackle += Math.Min(rand.Next(bump), 99 - rook.Player.BreakTackle);
					rook.Player.ThrowAccuracy += Math.Min(rand.Next(bump + 2), 99 - rook.Player.ThrowAccuracy);
					rook.Player.ThrowPower += Math.Min(rand.Next(bump), 99 - rook.Player.ThrowPower);
					rook.Player.PassBlocking += Math.Min(rand.Next(bump), 99 - rook.Player.PassBlocking);
					rook.Player.RunBlocking += Math.Min(rand.Next(bump), 99 - rook.Player.RunBlocking);

					rook.Player.KickAccuracy += Math.Min(rand.Next(bump), 99 - rook.Player.KickAccuracy);
					rook.Player.KickPower += Math.Min(rand.Next(bump), 99 - rook.Player.KickPower);

					rook.Player.Overall = rook.Player.CalculateOverallRating(rook.Player.PositionId);
					//Trace.Writeline(i + " " + rook.Player.ToString() + " " + RookEOR(rook));

					rook.ActualValue = LocalMath.ValueScale * positionData[rook.Player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(rook.Player.Overall + math.injury(rook.Player.Injury, positionData[rook.Player.PositionId].DurabilityNeed));
					continue;
				}

                // MLB's are way to high in 2007. Decrease their ratings./
                if ((rook.Player.PositionId == (int)MaddenPositions.MLB) && (math.mfv == MaddenFileVersion.Ver2007))
                {
                    int bump = 4;

                    rook.Player.Speed -= rand.Next(bump) + 3;
                    rook.Player.Agility -= rand.Next(bump) + 3;
                    rook.Player.Acceleration -= rand.Next(bump) + 3;
                    rook.Player.Jumping -= rand.Next(bump) + 3;

                    rook.Player.Strength -= rand.Next(bump) + 3;

                    rook.Player.Awareness -= rand.Next(bump) + 3;
                    rook.Player.Tackle -= rand.Next(bump) + 3;

                    rook.Player.Catching -= rand.Next(bump) + 3;
                    rook.Player.Carrying -= rand.Next(bump) + 3;
                    rook.Player.BreakTackle -= rand.Next(bump) + 3;
                    rook.Player.ThrowAccuracy -= rand.Next(bump) + 3;
                    rook.Player.ThrowPower -= rand.Next(bump) + 3;
                    rook.Player.PassBlocking -= rand.Next(bump) + 3;
                    rook.Player.RunBlocking -= rand.Next(bump) + 3;

                    rook.Player.KickAccuracy -= rand.Next(bump) + 3;
                    rook.Player.KickPower -= rand.Next(bump) + 3;

                    rook.Player.Overall = rook.Player.CalculateOverallRating(rook.Player.PositionId);
                    //Trace.Writeline(i + " " + rook.Player.ToString() + " " + RookEOR(rook));

                    rook.ActualValue = LocalMath.ValueScale * positionData[rook.Player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(rook.Player.Overall + math.injury(rook.Player.Injury, positionData[rook.Player.PositionId].DurabilityNeed));
                    continue;
                }


                if (math.mfv != MaddenFileVersion.Ver2007 | math.mfv != MaddenFileVersion.Ver2008)
                {
                    if (i < 15)
                    {
                        randMax = 3;
                    }
                    else if (i < 25)
                    {
                        randMax = 5;
                    }
                    else
                    {
                        randMax = ((i - 9.0) / 16.0) + 5.0;
                    }
                }
                else
                {
                    if (i < 5)
                    {
                        randMax = 6;
                    }
                    else if (i < 128)
                    {
                        randMax = 7;
                    }
                    else
                    {
                        randMax = ((i - 9.0) / 16.0) + 7.0;
                    }


                }
				// Subtract some random value, or the current value, whichever is less.
				// Prevents players from having negative attributes, which I think will
				// show up in the high 120's since it's probably an unsigned quantity.

				rook.Player.Speed -= Math.Min(rand.Next((int)Math.Round(randMax / 3)), rook.Player.Speed);
				rook.Player.Agility -= Math.Min(rand.Next((int)Math.Round(randMax / 3)), rook.Player.Agility);
				rook.Player.Acceleration -= Math.Min(rand.Next((int)Math.Round(randMax / 3)), rook.Player.Acceleration);
				rook.Player.Jumping -= Math.Min(rand.Next((int)Math.Round(randMax / 2)), rook.Player.Jumping);

				// FS's, ROLB's, TE's and MLB's get hammered by these adjustments, and so make
				// some allowances for them.

				/*
				int factor = 1;
				if ((rook.Player.PositionId == (int)MaddenPositions.FS)
					|| (rook.Player.PositionId == (int)MaddenPositions.ROLB))
				{
					factor = 2;
				}
				else if ((rook.Player.PositionId == (int)MaddenPositions.MLB)
					|| (rook.Player.PositionId == (int)MaddenPositions.TE))
				{
					factor = 4;
				}

				int ssfactor = 1;
				if (rook.Player.PositionId == (int)MaddenPositions.SS)
				{
					ssfactor = 2;
				}

				int extrafactor = 0;
				switch (rook.Player.PositionId)
				{
					case (int)MaddenPositions.QB:
						extrafactor = 3;
						break;
					case (int)MaddenPositions.HB:
						extrafactor = 7;
						break;
					case (int)MaddenPositions.WR:
						extrafactor = 4;
						break;
					case (int)MaddenPositions.C:
					case (int)MaddenPositions.RG:
					case (int)MaddenPositions.LG:
						extrafactor = 3;
						break;
					case (int)MaddenPositions.LT:
						extrafactor = 2;
						break;
					case (int)MaddenPositions.RT:
					case (int)MaddenPositions.DT:
						extrafactor = 1;
						break;
					case (int)MaddenPositions.LE:
						extrafactor = 6;
						break;
					case (int)MaddenPositions.RE:
						extrafactor = 5;
						break;
					case (int)MaddenPositions.FS:
					case (int)MaddenPositions.SS:
						extrafactor = 2;
						break;
				} */

				/*                                     QB,  HB, FB, WR, TE, LT, LG,C,RG, RT,  LE,RE,DT,LL, ML,   RL, CB, FS,  SS,   K, P 
				double[] slopes =     new double[21] { 1.2, 1.2, 1, 1, 0.25, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.25, 0.5, 1, 0.5, 1.5, 0.2, 0.3 };
				double[] intercepts = new double[21] { 0, 5, 1, 1, 0, 0.5, 3, 4, 3, -.75, 8, 9, 2, 0, -0.75, 0, 0, 2, 3, 0, 1 };*/

				/*                                  QB, HB, FB, WR,  TE,   LT,  LG,   C,  RG,  RT,  LE,  RE,   DT,  LL,   ML,  RL,  CB,  FS,  SS,   K, P */
				double[] slopes = new double[21] { 2.4, 2.2, 1, 1.35, 0.8, 1.1, 0.5, 0.6, 0.6, 0.2, 0.7, 0.5, 0.6, 0.9, 0.35, 0.8, 1.2, 0.9, 1.2, 0.3, 0.3 };
				double[] intercepts = new double[21] { -3.6, 2, 1, -.05, -1.2, 0, 4.5, 4.2, 4.2, 5.3, 8.9, 10.5, 3.2, 0.3, -1.05, -.9, -.6, 0.8, 3.9, -.3, 1 };

				rook.Player.Strength -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.Strength);

				rook.Player.Awareness -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.Awareness);
				rook.Player.Tackle -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.Tackle);

				rook.Player.Catching -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.Catching);
				rook.Player.Carrying -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax / 2.0 + intercepts[rook.Player.PositionId])), rook.Player.Carrying);
				rook.Player.BreakTackle -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.BreakTackle);
				rook.Player.ThrowAccuracy -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId]) + 2), rook.Player.ThrowAccuracy);
				rook.Player.ThrowPower -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.ThrowPower);
				rook.Player.PassBlocking -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.PassBlocking);
				rook.Player.RunBlocking -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.RunBlocking);

				rook.Player.KickAccuracy -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.KickAccuracy);
				rook.Player.KickPower -= Math.Min(rand.Next((int)Math.Round(slopes[rook.Player.PositionId] * randMax + intercepts[rook.Player.PositionId])), rook.Player.KickPower);

				rook.Player.Overall = rook.Player.CalculateOverallRating(rook.Player.PositionId);
				//Trace.Writeline(i + " " + rook.Player.ToString() + " " + RookEOR(rook));

				rook.ActualValue = LocalMath.ValueScale * positionData[rook.Player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(rook.Player.Overall + math.injury(rook.Player.Injury, positionData[rook.Player.PositionId].DurabilityNeed));
			}
		}

		public double RookEOR(RookieRecord rook)
		{
			return rook.Player.CalculateOverallRating(rook.Player.PositionId, true) /*+ math.injury(rook.Player.Injury, positionData[rook.Player.PositionId].DurabilityNeed)*/;
		}

		public void DumpRookiesByPosition()
		{
			//Trace.Writeline("\n");
			for (int j = 0; j < 21; j++)
			{
				List<int> found = new List<int>();

				bool stop = false;
				for (int i = 0; i < 250 && !stop; i++)
				{
					double bestRating = 0;
					int bestId = -1;

					foreach (KeyValuePair<int, RookieRecord> rook in rookies)
					{
						if (rook.Value.Player.PositionId == j && rook.Value.Player.Overall > bestRating && !found.Contains(rook.Key))
						{
							bestId = rook.Key;
							bestRating = rook.Value.Player.Overall;
						}
					}

					if (bestId == -1) { stop = true; continue; }

					found.Add(bestId);

					//Trace.Writeline(i + " " + rookies[bestId].Player.ToString() + " " + rookies[bestId].Player.Overall + " " + rookies[bestId].Player.Injury + " " + rookies[bestId].ActualValue + " " + pickValues[i]);
				}

				//Trace.Writeline("");
			}

			//Trace.Writeline("");
		}

		public void DumpRookies()
		{			
			List<int> found = new List<int>();
			string points = "";

            foreach (KeyValuePair<int, RookieRecord> r in rookies)
            {
                found.Add(r.Key);
            }
            for (int i = 0; i < found.Count; i++)
            {
                for (int comp = i + 1; comp < found.Count; comp++)
                {
                    if (rookies[found[comp]].ActualValue > rookies[found[i]].ActualValue)
                    {
                        int hold = found[i];
                        found[i] = found[comp];
                        found[comp] = hold;
                    }
                }
            }
            
            
            
            
            for (int i = 0; i < 257; i++)
            {
                //double bestRating = 0;
                //int bestId = -1;

                //foreach (KeyValuePair<int, RookieRecord> r in rookies)
                //{
                //   if (r.Value.ActualValue > bestRating && !found.Contains(r.Key))
                //    {
                //        bestId = r.Key;
                //        bestRating = r.Value.ActualValue;
                //    }                

                RookieRecord rook = rookies[found[i]];
                points += "{" + RookEOR(rook) + ", " + Math.Log(1.2 * pickValues[i] / positionData[rook.Player.PositionId].Value((int)TeamRecord.Defense.Front43)) + "}, ";

                //Trace.Writeline(i + " " + rookies[bestId].Player.ToString() + " " + rookies[bestId].Player.Overall + " " + rookies[bestId].Player.Injury + " " + rookies[bestId].ActualValue + " " + pickValues[i]);
            }
			

			//Trace.Writeline("");
			//Trace.Writeline("");
			//Trace.Writeline(points);
			//Trace.Writeline("");
			//Trace.Writeline("");
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
					if (rook.Value.PerceivedEffectiveValue(team, pickNumber, dcr.awarenessAdjust) > bestRating &&
						!draftBoard.Contains(rook.Value) && !(rook.Value.DraftPickTeam < 32))
					{

						bestId = rook.Key;
						bestRating = rook.Value.PerceivedEffectiveValue(team, pickNumber, dcr.awarenessAdjust);
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

		private void CalculateActualProjections()
		{
			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				rook.Value.ActualValue = LocalMath.ValueScale * positionData[rook.Value.Player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(rook.Value.Player.Overall + math.injury(rook.Value.Player.Injury, positionData[rook.Value.Player.PositionId].DurabilityNeed));
			}

			List<int> SortedAverageValues = new List<int>();

            foreach (KeyValuePair<int, RookieRecord> rook in rookies)
            {
                SortedAverageValues.Add(rook.Key);
            }

            for (int i = 0; i < SortedAverageValues.Count; i++)
            {
                for (int comp = i+1; comp < SortedAverageValues.Count; comp++)
                {
                    if (rookies[SortedAverageValues[comp]].ActualValue > rookies[SortedAverageValues[i]].ActualValue)
                    {
                        int hold = SortedAverageValues[i];
                        SortedAverageValues[i] = SortedAverageValues[comp];
                        SortedAverageValues[comp] = hold;
                    }
                }
            }
            
			for (int i = 0; i < SortedAverageValues.Count; i++)
			{
                if (SortedAverageValues[i] != -1)
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

				if (SortedAverageValues[i] != -1)
                    rookies[SortedAverageValues[i]].EstimatedRound[(int)RookieRecord.RatingType.Actual] = rankstring;
			}
		}

		public RookieRecord MakeSelection(int pickNumber, RookieRecord toDraft)
		{
			//int Cardinals = model.TeamModel.GetTeamIdFromTeamName("Cardinals");
			DateTime total = DateTime.Now;

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
				favorites = GetFavoriteRookies(pickNumber);
				toDraft = favorites[dpRecord.CurrentTeamId];
			}

            Trace.WriteLine(pickNumber + " " + toDraft.Player.ToString());

			/*
             * Some debugging/diagnostic code
             *              
             
			//Trace.Writeline("\n" + " " + pickNumber + " " + dpRecord.CurrentTeamId + " " + toDraft.Player.ToString() + " " + toDraft.Player.Overall + " " + toDraft.Player.Injury + "\n");

				//Trace.Writeline(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).CON);
				foreach (RookieRecord rook in GetDraftBoard(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), pickNumber))
				{
					if (rook.DraftedTeam < 32) { continue; }
					//Trace.Writeline(rook.Player.PlayerId + " " + rook.Player.ToString() + " " + rook.EffectiveValue(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), pickNumber, dcr.awarenessAdjust) + " " + rook.ActualValue +  " " + rook.values[dpRecord.CurrentTeamId][rook.Player.PositionId][(int)RookieRecord.ValueType.NoProg] + " " + rook.Player.Overall + " " + rook.GetAdjustedOverall(dpRecord.CurrentTeamId, (int)RookieRecord.RatingType.Final, rook.Player.PositionId, dcr.awarenessAdjust) + " " + (rook.PreCombineScoutedHours[dpRecord.CurrentTeamId] + rook.PostCombineScoutedHours[dpRecord.CurrentTeamId]));
				}
			*/


			toDraft.DraftPickTeam = dpRecord.CurrentTeamId;
			toDraft.DraftPickNumber = pickNumber;
			toDraft.Player.DraftRound = pickNumber / 32 + 1;
			toDraft.Player.DraftRoundIndex = pickNumber % 32 + 1;



			// **** NEED TO INCLUDE THIS LOGIC ****
			//
			// For now, they still don't see his actual ratings -- just what they scouted.
			// Not only does this make sense, but it also prevents two picks at the same position
			// (two K's, two P's, etc.)

			depthChart[dpRecord.CurrentTeamId] = dcr.SortDepthChart(dpRecord.CurrentTeamId, true, rookies);
			CalculateDepthChartValues(dpRecord.CurrentTeamId);

			/*
            toDraft.Player.EffectiveOVR = toDraft.ratings[dpRecord.CurrentTeamId][(int)RookieRecord.RatingType.Final][(int)MaddenAttribute.OVR] + 5 * (5 - model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).CON) / 2
                    + math.injury(toDraft.ratings[dpRecord.CurrentTeamId][(int)RookieRecord.RatingType.Final][(int)MaddenAttribute.INJ], positionData[toDraft.Player.PositionId].DurabilityNeed);
			toDraft.Player.Value = LocalMath.ValueScale * positionData[toDraft.Player.PositionId].Value(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).DefensiveSystem) * math.valcurve(toDraft.Player.EffectiveOVR);

			depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId].Add(toDraft.Player);
			depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId] = SortByEffectiveOVR(depthChart[dpRecord.CurrentTeamId][toDraft.Player.PositionId], dpRecord.CurrentTeamId, toDraft.Player.PositionId);
			 */

			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				// Really only need to calculate within the same position grouping.
				// Would be more efficient to skip players not in the grouping of the drafted player....

				if (dcr.awarenessAdjust[toDraft.Player.PositionId].ContainsKey(rook.Value.Player.PositionId))
				{
					foreach (KeyValuePair<int, double> pair in dcr.awarenessAdjust[rook.Value.Player.PositionId])
					{
						if (pair.Key > 20) { continue; }
						rook.Value.CalculateNeeds(model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId), depthChart[dpRecord.CurrentTeamId][pair.Key], depthChartValues[dpRecord.CurrentTeamId][pair.Key], positionData, pair.Key);
					}
				}
			}

			//Trace.Writeline("Total MakeSelection: " + total.Subtract(DateTime.Now));
			return toDraft;
		}

        public void ClearRookieGameRecords()
        {            
            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;

                PlayerRecord player = (PlayerRecord)rec;
                if (player.YearsPro == 0 && player.FirstName != "New")
                    model.PlayerModel.RemoveAllStats(player.PlayerId, true, true);
            }            
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
					//Trace.Writeline("Oops!");
				}

				Trace.Write(i + " " + rookieRecord.PlayerID + " ");
				Trace.Write(model.TeamModel.GetTeamNameFromTeamId(dpRecord.CurrentTeamId) + " ");
				Trace.Write(Enum.GetNames(typeof(MaddenPositions))[model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerID).PositionId].ToString() + " ");
				Trace.Write(model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerID).Overall + " " + model.PlayerModel.GetPlayerByPlayerId(rookieRecord.PlayerID).Injury + " ");
				if (pickValues.Count > 100)
				{
			        //Trace.Writeline(Math.Round(rookieRecord.values[dpRecord.CurrentTeamId][(int)RookieRecord.ValueType.NoProg]) + " " + pickValues[i]);
				}
				else
				{
					Trace.Write("\n");
				}
			}
		}

		public void InitializeScouting()
		{
			SetInitialRookieAttributes();
			SetRookieValues((int)RookieRecord.RatingType.Initial);
			SetNeeds();

			DetermineProjections((int)RookieRecord.RatingType.Initial);
			//SetPerceivedRookieValues();
			SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Initial);

			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				rook.Value.PreCombineScoutedHours[HumanTeamId] = 0;
				rook.Value.PostCombineScoutedHours[HumanTeamId] = 0;
			}
		}

		public void DoCombine()
		{
			DoCPURookieScouting(true);
			SetCombineRookieAttributes();
			SetRookieValues((int)RookieRecord.RatingType.Combine);
			SetNeeds();

			DetermineProjections((int)RookieRecord.RatingType.Combine);
			//SetPerceivedRookieValues();
			SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Combine);
		}

		public void DoFinal()
		{
			DoCPURookieScouting(false);
			SetFinalRookieAttributes();
			SetRookieValues((int)RookieRecord.RatingType.Final);
			SetNeeds();

			DetermineProjections((int)RookieRecord.RatingType.Final);
			SetPerceivedRookieValues();
			SetCombineStats(HumanTeamId, (int)RookieRecord.RatingType.Final);
		}

		private void CalculateDepthChartValues(int TeamId)
		{
			// At each position, calculate the effective value of that player
			// and store it in depthChartValues.  Use this.dcr.GetAdjustedOverall
			// to calculate adjusted overall values.

			for (int i = 0; i < 21; i++)
			{
				depthChartValues[TeamId][i].Clear();

				for (int j = 0; j < depthChart[TeamId][i].Count; j++)
				{
					PlayerRecord rec = depthChart[TeamId][i][j];

					if (rec.YearsPro > 0)
					{
						// INJURY CHANGE
						// Remove injury adjustment for depth chart values

						double tempOverall = (double)dcr.GetAdjustedOverall(rec, i) + /*math.theta(5.0 - rec.YearsPro) * (5.0 - (double)rec.YearsPro) * (5.0 - (double)model.TeamModel.GetTeamRecord(TeamId).CON) / 2.0 */
							math.pointboost(rec, model.TeamModel.GetTeamRecord(TeamId).CON, positionData[i].RetirementAge)/* + math.injury(rec.Injury, positionData[i].DurabilityNeed) */;

						depthChartValues[TeamId][i].Add(LocalMath.ValueScale * positionData[i].Value(model.TeamModel.GetTeamRecord(TeamId).DefensiveSystem) * math.valcurve(tempOverall));
					}
					else
					{
						depthChartValues[TeamId][i].Add(rookies[rec.PlayerId].values[TeamId][i][(int)RookieRecord.ValueType.WithProg]);
					}
				}
			}
		}

		public void GetProbabilities(int pickNumber)
		{
			Dictionary<int, Dictionary<int, double>> toReturn = new Dictionary<int, Dictionary<int, double>>();

			for (int i = 0; i < 32; i++)
			{
				Dictionary<int, double> tempProbs = new Dictionary<int, double>();
				TeamRecord team = model.TeamModel.GetTeamRecord(i);
				RookieRecord favorite = favorites[i];
				double totalProb = 0;

				if (favorite == null)
				{
					foreach (KeyValuePair<int, RookieRecord> rook in rookies)
					{
						toReturn[i][rook.Key] = 0;
					}
					continue;
				}


				toReturn[i] = new Dictionary<int, double>();
				double favoritePerceivedValue = favorite.PerceivedEffectiveValue(team, pickNumber, dcr.awarenessAdjust);

				foreach (KeyValuePair<int, RookieRecord> rook in rookies)
				{
					//                    if (rook.Value.DraftedTeam < 32) { continue; }

					if (rook.Value.PlayerID == favorite.PlayerID)
					{
						tempProbs[rook.Key] = 1;
					}
					else if (rook.Value.AverageNeed(team, pickNumber, dcr.awarenessAdjust) < positionData[rook.Value.Player.PositionId].Threshold || rook.Value.DraftPickTeam < 32)
					{
						tempProbs[rook.Key] = 0;
					}
					else
					{
						tempProbs[rook.Key] = Math.Exp(-3.0 * (favoritePerceivedValue - rook.Value.PerceivedEffectiveValue(team, pickNumber, dcr.awarenessAdjust)) / favoritePerceivedValue);
					}

					if (tempProbs[rook.Key] < 0.33) { tempProbs[rook.Key] = 0; }
					totalProb += tempProbs[rook.Key];
				}

				foreach (KeyValuePair<int, RookieRecord> rook in rookies)
				{
					toReturn[i][rook.Key] = tempProbs[rook.Key] / totalProb;
				}
			}

			probs = toReturn;
		}

		public TradeOffer tradePendingAccept()
		{
			foreach (TradeOffer to in tradeOffers.Values)
			{
				if (to.status == (int)TradeOfferStatus.PendingAccept)
				{
					return to;
				}
			}

			return null;
		}

		public bool tradeExists(int teamId)
		{
			return tradeOffers.ContainsKey(teamId);
		}

		public bool tradePending(int teamId)
		{
			foreach (TradeOffer to in tradeOffers.Values)
			{
				if (to.status != (int)TradeOfferStatus.Rejected && (to.status == (int)TradeOfferStatus.HigherResponsePending || to.status == (int)TradeOfferStatus.LowerResponsePending || to.status == (int)TradeOfferStatus.PendingAccept) && (teamId == -1 || teamId == to.LowerTeam))
				{
					return true;
				}
			}
			return false;
		}

		public RookieRecord SkipHuman(int pickNumber, int humanBackedUp)
		{
			DraftPickRecord currentRecord = GetDraftPickByNumber(pickNumber);
			DraftPickRecord nextRecord = GetDraftPickByNumber(pickNumber + humanBackedUp);

			if (HumanTeamId != currentRecord.CurrentTeamId)
			{
			}

			currentRecord.CurrentTeamId = nextRecord.CurrentTeamId;
			nextRecord.CurrentTeamId = HumanTeamId;

			//Trace.Writeline("Backed Up: " + humanBackedUp + " Picking: " + model.TeamModel.GetTeamNameFromTeamId(currentRecord.CurrentTeamId));

			return MakeSelection(pickNumber, null);
		}

		public void AcceptTrade(TradeOffer to)
		{
			// reassign picks

			foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
			{
				DraftPickRecord record = (DraftPickRecord)rec;

				if (to.PicksFromHigher.Contains(record.PickNumber) || record.PickNumber == to.pickNumber)
				{
					record.CurrentTeamId = to.LowerTeam;
				}
				else if (to.PicksFromLower.Contains(record.PickNumber))
				{
					record.CurrentTeamId = to.HigherTeam;
				}
			}

			foreach (int pick in to.PicksFromLower)
			{
				if (pick > 1000)
				{
					futureTradedPicks[to.LowerTeam][pick - 1000] = to.HigherTeam;
				}
			}

			foreach (int pick in to.PicksFromHigher)
			{
				if (pick > 1000)
				{
					futureTradedPicks[to.HigherTeam][pick - 1000] = to.LowerTeam;
				}
			}
		}

		public TradeOffer setupTradeOffer(int LowerTeamId, int pickNumber)
		{
			int currentSelector = -1;
			foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
			{
				DraftPickRecord record = (DraftPickRecord)rec;

				if (record.PickNumber == pickNumber)
				{
					currentSelector = record.CurrentTeamId;
					break;
				}
			}

			TradeOffer to = new TradeOffer(currentSelector, LowerTeamId, this);
			to.pickNumber = pickNumber;

			foreach (TradeOffer locTO in tradeOffers.Values)
			{
				if (locTO.status == (int)TradeOfferStatus.PendingAccept)
				{
					to.biddingWar = true;
					break;
				}
			}

			foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
			{
				DraftPickRecord record = (DraftPickRecord)rec;

				if (record.CurrentTeamId == LowerTeamId && record.PickNumber > pickNumber)
				{
					to.lowerAvailable.Add(record.PickNumber);
				}
				else if (record.CurrentTeamId == currentSelector && record.PickNumber > pickNumber)
				{
					to.higherAvailable.Add(record.PickNumber);
				}
			}

			to.MaxGive = BestOffers[LowerTeamId];
			//Trace.Writeline("Maximum to give is " + to.MaxGive);

			// Only allow trading of future picks if they've got at least 6 picks left for next year.
			// Could be better about this -- could allow it if the *net* number of picks for next year
			// is still 5 or more -- i.e., allow it if they pick up a future pick from the other team
			// in the trade.  So, we need to count the number of picks they've got.

			int futurePicks = 7;
			for (int i = 0; i < 32; i++)
			{
				if (i == LowerTeamId)
				{
					futurePicks -= futureTradedPicks[i].Count;
				}
				else
				{
					for (int j = 0; j < 7; j++)
					{
						if (futureTradedPicks[i].ContainsKey(j) && futureTradedPicks[i][j] == LowerTeamId)
						{
							futurePicks++;
						}
					}
				}
			}

			if (LowerTeamId != HumanTeamId && futurePicks < 6)
            {
                //s68 - removing this to allow teams to make more mistakes in trading away future picks
				//to.allowFuturePicksFromLower = false;
			}


			futurePicks = 7;
			for (int i = 0; i < 32; i++)
			{
				if (i == currentSelector)
				{
					futurePicks -= futureTradedPicks[i].Count;
				}
				else
				{
					for (int j = 0; j < 7; j++)
					{
						if (futureTradedPicks[i].ContainsKey(j) && futureTradedPicks[i][j] == currentSelector)
						{
							futurePicks++;
						}
					}
				}
			}

			if (futurePicks < 6 && currentSelector != HumanTeamId)
			{
				//s68 - removing this to allow teams to make more mistakes in trading away future picks
                //to.allowFuturePicksFromHigher = false;
			}

			int defense = model.TeamModel.GetTeamRecord(LowerTeamId).DefensiveSystem;

			if (LowerTeamId == HumanTeamId || ((favorites[LowerTeamId].PreCombineScoutedHours[LowerTeamId] + favorites[LowerTeamId].PostCombineScoutedHours[LowerTeamId]) >= 6 && to.MaxGive > 1.2 * pickValues[pickNumber] && (favorites[LowerTeamId].AverageStarterNeed(LowerTeamId, dcr.awarenessAdjust) > 0.7 || favorites[LowerTeamId].AverageSuccessorNeed(LowerTeamId, dcr.awarenessAdjust) > 0.8)))
			{
				to.MaxPicksFromLower = 3;
			}

			if (LowerTeamId == HumanTeamId || ((favorites[LowerTeamId].PreCombineScoutedHours[LowerTeamId] + favorites[LowerTeamId].PostCombineScoutedHours[LowerTeamId]) >= 7 && to.MaxGive > 1.4 * pickValues[pickNumber] && (favorites[LowerTeamId].AverageStarterNeed(LowerTeamId, dcr.awarenessAdjust) > 0.8 || favorites[LowerTeamId].AverageSuccessorNeed(LowerTeamId, dcr.awarenessAdjust) > 0.9)))
			{
				to.allowFutureHighPicks = true;
			}

			if (LowerTeamId == HumanTeamId || ((favorites[LowerTeamId].PreCombineScoutedHours[LowerTeamId] + favorites[LowerTeamId].PostCombineScoutedHours[LowerTeamId]) >= 8 && to.MaxGive > 1.6 * pickValues[pickNumber] && (favorites[LowerTeamId].AverageStarterNeed(LowerTeamId, dcr.awarenessAdjust) > 0.9 || favorites[LowerTeamId].AverageSuccessorNeed(LowerTeamId, dcr.awarenessAdjust) > 0.95)))
			{
				to.allowMultipleHighPicks = true;
			}

			if (LowerTeamId == HumanTeamId)
			{
				to.status = (int)TradeOfferStatus.LowerResponsePending;
				tradeOffers.Add(LowerTeamId, to);
			}
			else if (currentSelector == HumanTeamId)
			{
				to.status = -1;
				tradeOffers[LowerTeamId] = to;
			}

			return to;
		}

		public TradeOffer tradeInitialOffer(int LowerTeamId, int pickNumber, double fracTimeLeft)
		{
			if (LowerTeamId == HumanTeamId && humanRejected)
			{
				return null;
			}

			TradeOffer to = setupTradeOffer(LowerTeamId, pickNumber);

			if (LowerTeamId != HumanTeamId && to.MaxGive < 0.5 * pickValues[pickNumber])
			{
				if (to.HigherTeam != HumanTeamId)
				{
					//Trace.Writeline("Rejecting trade from " + model.TeamModel.GetTeamNameFromTeamId(LowerTeamId) + " outright.\n");
					to.status = (int)TradeOfferStatus.Rejected;
					tradeOffers.Add(LowerTeamId, to);
				}
				else
				{
					tradeOffers.Remove(LowerTeamId);
				}

				return null;
			}

			double offer;
			if (LowerTeamId != HumanTeamId)
			{
				double initialStart = Math.Min(to.MaxGive, pickValues[pickNumber]);
				offer = (5.0 / 6.0 - (1.0 / 6.0) * rand.NextDouble()) * initialStart;
				//Trace.Writeline("Initial attempted offer: " + offer);
			}
			else
			{
				// if this is a potential human offer, give the maximum initial bid
				// we're really just doing this to get a to.MinAccept

				offer = 10000;
			}

			double minaccept = to.makeCounterOffer(offer, false);

			if (to.offersFromLower.Count == 0 || (LowerTeamId != HumanTeamId && to.HigherTeam != HumanTeamId && to.offersFromLower[0] < 0.5 * to.MinAccept))
			{
				//Trace.Writeline("Rejecting trade from " + model.TeamModel.GetTeamNameFromTeamId(LowerTeamId) + " after initial offer.\n");
				to.status = (int)TradeOfferStatus.Rejected;
				tradeOffers[LowerTeamId] = to;
				return null;
			}
			else if (LowerTeamId != HumanTeamId)
			{
				//Trace.Writeline("Initial actual offer:    " + to.offersFromLower[0]);
				//Trace.Writeline("Minimum to take: " + to.MinAccept);

				to.status = (int)TradeOfferStatus.HigherResponsePending;
				tradeOffers[LowerTeamId] = to;
				//Trace.Writeline("");

				if (to.HigherTeam == HumanTeamId)
				{
					if (to.offersFromLower[0] < 0.8 * pickValues[pickNumber])
					{
						tradeOffers.Remove(LowerTeamId);
						return null;
					}
					else
					{
						df.refreshTradeTeams = true;
						return to;
					}
				}

				return null;
			}
			else
			{
				if (minaccept > 0.85 * pickValues[pickNumber])
				{
					humanRejected = true;

					tradeOffers.Remove(HumanTeamId);
					return null;
				}

				//Trace.Writeline("Initial actual offer:    " + to.offersFromLower[0]);
				//Trace.Writeline("Minimum to take: " + to.MinAccept);
				to.offersFromLower = new List<double>();

				to.status = (int)TradeOfferStatus.HigherResponsePending;

				tradeOffers[LowerTeamId] = to;
				tradeCounterOffer(LowerTeamId, fracTimeLeft);

				if (to.status == (int)TradeOfferStatus.Rejected)
				{
					humanRejected = true;

					// Remove it so the human can still make offers, even though the CPU won't accept it.
					tradeOffers.Remove(HumanTeamId);
					return null;
				}

				return to;
			}
		}

		public TradeOffer tradeCounterOffer(int LowerTeamId, double fracTimeLeft)
		{
			TradeOffer to = null;

			foreach (TradeOffer t in tradeOffers.Values)
			{
				if (t.LowerTeam == LowerTeamId)
				{
					to = t;
					break;
				}
			}

			if (to.HigherTeam == HumanTeamId)
			{
				to.lowerStrikes = Math.Min(to.offersFromLower.Count / 2, 2);
			}
			else if (to.LowerTeam == HumanTeamId)
			{
				to.higherStrikes = Math.Min(to.offersFromHigher.Count / 2, 2);
			}

			if (to.status == (int)TradeOfferStatus.HigherResponsePending)
			{
				if (to.HigherTeam == HumanTeamId)
				{
					return null;
				}

				df.refreshTradeTeams = true;

				double ourPreviousOffer;
				double theirCurrentOffer;
				if (to.offersFromLower.Count > 0)
				{
					theirCurrentOffer = to.offersFromLower[to.offersFromLower.Count - 1];
				}
				else
				{
					theirCurrentOffer = 0;
				}

				if (to.offersFromHigher.Count == 0)
				{
					ourPreviousOffer = (1.2 + 0.3 * rand.NextDouble()) * to.MinAccept;
				}
				else
				{
					ourPreviousOffer = to.offersFromHigher[to.offersFromHigher.Count - 1];
				}

				//Trace.Writeline("Higher Team responding...");
				//Trace.Writeline("Our last: " + ourPreviousOffer + " Their last: " + theirCurrentOffer + " Min Accept: " + to.MinAccept + " Max Give: " + to.MaxGive);

				// First determine if we like this offer or if we should counteroffer
				if (theirCurrentOffer == ourPreviousOffer || (theirCurrentOffer > (1.1 - 0.05 * to.higherStrikes) * to.MinAccept) || (to.biddingWar && theirCurrentOffer > to.MinAccept))
				{
					// First see if there's a higher offer out there that we haven't yet replied to.
					// If so, process that one instead, and return whatever it returns.

					int highestTeam = to.LowerTeam;
					double highestOffer = theirCurrentOffer;
					foreach (TradeOffer locTO in tradeOffers.Values)
					{
						if (locTO.LowerTeam == to.LowerTeam || locTO.status == (int)TradeOfferStatus.Rejected || locTO.offersFromLower.Count == 0) { continue; }

						if (locTO.offersFromLower[locTO.offersFromLower.Count - 1] > highestOffer)
						{
							highestTeam = locTO.LowerTeam;
							highestOffer = locTO.offersFromLower[locTO.offersFromLower.Count - 1];
						}
					}

					if (highestTeam != to.LowerTeam)
					{
						return tradeCounterOffer(highestTeam, fracTimeLeft);
					}

					// We'll take the offer.  Let's see if we can drive up the bidding first though.
					//Trace.Writeline("Accepting offer...");

					bool anotherOffer = false;
					to.status = (int)TradeOfferStatus.PendingAccept;
					to.MinAccept = (1.03 + 0.04 * rand.NextDouble()) * theirCurrentOffer;
					to.biddingWar = true;

					foreach (TradeOffer locTO in tradeOffers.Values)
					{
						if (locTO.LowerTeam == to.LowerTeam || locTO.status == (int)TradeOfferStatus.Rejected) { continue; }

						//Trace.Writeline("Bidding war with " + model.TeamModel.GetTeamNameFromTeamId(locTO.LowerTeam) + "...");

						if (locTO.MinAccept < to.MinAccept)
						{
							locTO.MinAccept = to.MinAccept;
						}

						locTO.biddingWar = true;

						locTO.makeCounterOffer((1.05 + 0.05 * rand.NextDouble()) * locTO.MinAccept, true);

						// This should only happen if the top three picks from the lower team
						// still can't get near the minimum acceptance value.
						if (locTO.offersFromHigher[locTO.offersFromHigher.Count - 1] < locTO.MinAccept && locTO.PicksFromHigher.Count == 0)
						{
							//Trace.Writeline(model.TeamModel.GetTeamNameFromTeamId(locTO.LowerTeam) + " can't match offer.  Ending trade talks.");

							locTO.status = (int)TradeOfferStatus.Rejected;

							if (locTO.LowerTeam == HumanTeamId && tradeUpForm != null)
							{
								tradeUpForm.HigherOffer((int)TradeResponse.BiddingWarReject);
							}
						}
						else
						{
							anotherOffer = true;
							locTO.status = (int)TradeOfferStatus.LowerResponsePending;

							if (locTO.LowerTeam == HumanTeamId && tradeUpForm != null)
							{
								tradeUpForm.HigherOffer((int)TradeResponse.BiddingWar);
							}
						}
					}

					// If there's no other offer now, might as well take this one
					// Could improve to wait if there's a lot of time left on the clock
					if (!anotherOffer && fracTimeLeft < 0.5)
					{
						//Trace.Writeline("No other offer around.  Accepting this one.  Returning...\n");

						if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
						{
							tradeUpForm.HigherOffer((int)TradeResponse.Accept);
						}

						AcceptTrade(to);
						return to;
					}
					else if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
					{
						tradeUpForm.HigherOffer((int)TradeResponse.PendingAccept);
					}

					return null;

				}
				else
				{

					// We don't like their offer quite yet.  Let's counteroffer.
					// If they didn't come increase at least 20 percent toward our
					// MinAccept, add a strike, and don't move quite as far.

					//Trace.Writeline("Didn't like last offer...");

					if (to.offersFromLower.Count >= 2 && to.offersFromLower[to.offersFromLower.Count - 2] + (to.MinAccept - to.offersFromLower[to.offersFromLower.Count - 2]) / 5.0
						> theirCurrentOffer && !to.lastWasStrike)
					{
						to.lowerStrikes = to.lowerStrikes + 1;
						to.lastWasStrike = true;

						//Trace.Writeline("Offer too low.  Strike " + to.lowerStrikes + ".");

						if (to.lowerStrikes >= 3)
						{
							//Trace.Writeline("Lower team strikes out.  Returning...\n");

							// they struck out
							to.status = (int)TradeOfferStatus.Rejected;
							if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
							{
								tradeUpForm.HigherOffer((int)TradeResponse.Reject);
							}

							return null;
						}

						double attemptedOffer = ourPreviousOffer - (3.0 - (double)to.lowerStrikes) * rand.NextDouble() / 20.0 * (ourPreviousOffer - to.MinAccept);

						//Trace.Writeline("Attempted counter-offer: " + attemptedOffer);

						to.makeCounterOffer(attemptedOffer, true);
						//Trace.Writeline("Actual counter-offer: " + to.offersFromHigher[to.offersFromHigher.Count - 1]);

						if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
						{
							tradeUpForm.strikeAdded = true;
						}
					}
					else
					{
						// Be a little more compromising
						to.lastWasStrike = false;

						double attemptedOffer = ourPreviousOffer - (0.2 + 0.2 * rand.NextDouble()) * (ourPreviousOffer - to.MinAccept);
						//Trace.Writeline("Attempted counter-offer: " + attemptedOffer);

						to.makeCounterOffer(attemptedOffer, true);
						//Trace.Writeline("Actual counter-offer: " + to.offersFromHigher[to.offersFromHigher.Count - 1]);
					}

					// This should only happen if the top three picks from the lower team
					// still can't get near the minimum acceptance value.
					if (to.offersFromHigher[to.offersFromHigher.Count - 1] < to.MinAccept && to.PicksFromHigher.Count == 0)
					{
						//Trace.Writeline("Lower team can't get up to our minimum acceptance level.  Rejecting offer.");
						to.status = (int)TradeOfferStatus.Rejected;

						if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
						{
							tradeUpForm.HigherOffer((int)TradeResponse.Reject);
						}
					}
					else
					{
						to.status = (int)TradeOfferStatus.LowerResponsePending;

						if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
						{
							tradeUpForm.HigherOffer((int)TradeResponse.CounterOffer);
						}
					}

					//Trace.Writeline("Returning...\n");
					return null;
				}
			}
			else if (to.status == (int)TradeOfferStatus.LowerResponsePending)
			{
				if (to.LowerTeam == HumanTeamId)
				{
					return null;
				}

				df.refreshTradeTeams = true;

				double ourPreviousOffer;
				double theirCurrentOffer = to.offersFromHigher[to.offersFromHigher.Count - 1];

				if (to.offersFromLower.Count == 0)
				{
					ourPreviousOffer = (5.0 / 6.0 - (1.0 / 6.0) * rand.NextDouble()) * to.MaxGive;
				}
				else
				{
					ourPreviousOffer = to.offersFromLower[to.offersFromLower.Count - 1];
				}

				bool fail = false;
				if (to.HigherTeam == HumanTeamId)
				{
					bool futureHighPicks = false;
					bool futurePicks = false;

					int highPicks = 0;
					foreach (int pick in to.PicksFromLower)
					{
						if (pick > 1000)
						{
							futurePicks = true;

							if (pick < 1003)
							{
								futureHighPicks = true;
							}

							if (futureValues(pick - 1000, model.TeamModel.GetTeamRecord(to.LowerTeam).CON) > 200)
							{
								highPicks++;
							}
						}
						else
						{
							if (pickValues[pick] > 200)
							{
								highPicks++;
							}
						}
					}

					if (to.PicksFromLower.Count > to.MaxPicksFromLower || (!to.allowFutureHighPicks && futureHighPicks) || (!to.allowFuturePicksFromLower && futurePicks)
						|| (!to.allowMultipleHighPicks && highPicks > 2))
					{
						fail = true;
					}
				}

				//Trace.Writeline("Lower Team responding...");
				//Trace.Writeline("Our last: " + ourPreviousOffer + " Their last: " + theirCurrentOffer + " Min Accept: " + to.MinAccept + " Max Give: " + to.MaxGive);

				// First determine if we like this offer or if we should counteroffer
				if (!fail && (theirCurrentOffer <= ourPreviousOffer || theirCurrentOffer < (0.9 + 0.05 * to.lowerStrikes) * to.MaxGive))
				{
					if (to.HigherTeam == HumanTeamId)
					{
						to.offersFromLower.Add(to.offersFromHigher[to.offersFromHigher.Count - 1]);
						to.status = (int)TradeOfferStatus.HigherResponsePending;
						tradeDownForm.Message(to, (int)TradeResponse.Accept);
						return null;
					}
					else
					{
						// Lower teams don't get final authority on accepting offers.
						// They accept conditionally, and the higher team has final say.

						//Trace.Writeline("Accepting offer...");

						bool anotherOffer = false;
						to.status = (int)TradeOfferStatus.PendingAccept;
						to.MinAccept = (1.03 + 0.04 * rand.NextDouble()) * theirCurrentOffer;
						to.biddingWar = true;

						foreach (TradeOffer locTO in tradeOffers.Values)
						{
							if (locTO.LowerTeam == to.LowerTeam || locTO.status == (int)TradeOfferStatus.Rejected) { continue; }

							//Trace.Writeline("Bidding war with " + model.TeamModel.GetTeamNameFromTeamId(locTO.LowerTeam) + "...");

							if (locTO.MinAccept < to.MinAccept)
							{
								locTO.MinAccept = to.MinAccept;
							}

							locTO.biddingWar = true;

							locTO.makeCounterOffer((1.05 + 0.05 * rand.NextDouble()) * locTO.MinAccept, true);

							// This should only happen if the top three picks from the lower team
							// still can't get near the minimum acceptance value.
							if (locTO.offersFromHigher[locTO.offersFromHigher.Count - 1] < locTO.MinAccept && locTO.PicksFromHigher.Count == 0)
							{
								//Trace.Writeline(model.TeamModel.GetTeamNameFromTeamId(locTO.LowerTeam) + " can't match offer.  Ending trade talks.");

								locTO.status = (int)TradeOfferStatus.Rejected;

								if (locTO.LowerTeam == HumanTeamId && tradeUpForm != null)
								{
									tradeUpForm.HigherOffer((int)TradeResponse.BiddingWarReject);
								}
							}
							else
							{
								anotherOffer = true;
								locTO.status = (int)TradeOfferStatus.LowerResponsePending;

								if (locTO.LowerTeam == HumanTeamId && tradeUpForm != null)
								{
									tradeUpForm.HigherOffer((int)TradeResponse.BiddingWar);
								}
							}
						}

						// If there's no other offer now, might as well take this one
						// Could improve to wait if there's a lot of time left on the clock
						if (!anotherOffer && fracTimeLeft < 0.5)
						{
							//Trace.Writeline("No other offer around.  Accepting this one.  Returning...\n");

							AcceptTrade(to);
							return to;
						}

						//Trace.Writeline("Returning...\n");
						return null;
					}
				}
				else
				{

					// We don't like their offer quite yet.  Let's counteroffer.
					// If they didn't come increase at least 20 percent toward our
					// MaxGive, add a strike, and don't move quite as far.

					//Trace.Writeline("Didn't like last offer...");

					if (to.offersFromHigher.Count > 1 && to.offersFromHigher[to.offersFromHigher.Count - 2] + (to.MaxGive - to.offersFromHigher[to.offersFromHigher.Count - 2]) / 5.0
						< theirCurrentOffer && !to.lastWasStrike)
					{
						to.higherStrikes = to.higherStrikes + 1;
						to.lastWasStrike = true;

						//Trace.Writeline("Offer too low.  Strike " + to.higherStrikes + ".");

						if (to.higherStrikes >= 3)
						{
							//Trace.Writeline("Higher team strikes out.  Returning...\n");
							// they struck out
							to.status = (int)TradeOfferStatus.Rejected;
							if (to.HigherTeam == HumanTeamId)
							{
								tradeDownForm.Message(to, (int)TradeResponse.Reject);
							}
							return null;
						}

						double attemptedOffer = ourPreviousOffer - (3.0 - (double)to.higherStrikes) * rand.NextDouble() / 20.0 * (ourPreviousOffer - to.MaxGive);
						//Trace.Writeline("Attempted counter-offer: " + attemptedOffer);

						to.makeCounterOffer(attemptedOffer, false);
						if (to.HigherTeam == HumanTeamId)
						{
							if (to.offersFromLower.Count == 0 || to.offersFromLower[to.offersFromLower.Count - 1] < 0.5 * pickValues[to.pickNumber])
							{
								to.status = (int)TradeOfferStatus.Rejected;
								tradeDownForm.Message(to, (int)TradeResponse.Reject);
								return null;
							}

							tradeDownForm.Message(to, (int)TradeResponse.CounterOffer);
						}
						//Trace.Writeline("Actual counter-offer: " + to.offersFromLower[to.offersFromLower.Count - 1]);
					}
					else
					{
						// Be a little more compromising
						to.lastWasStrike = false;

						double attemptedOffer = ourPreviousOffer - (0.2 + 0.2 * rand.NextDouble()) * (ourPreviousOffer - to.MaxGive);
						//Trace.Writeline("Attempted counter-offer: " + attemptedOffer);

						to.makeCounterOffer(attemptedOffer, false);
						if (to.HigherTeam == HumanTeamId)
						{
							if (to.offersFromLower.Count == 0 || to.offersFromLower[to.offersFromLower.Count - 1] < 0.5 * pickValues[to.pickNumber])
							{
								to.status = (int)TradeOfferStatus.Rejected;
								tradeDownForm.Message(to, (int)TradeResponse.Reject);
								return null;
							}

							tradeDownForm.Message(to, (int)TradeResponse.CounterOffer);
						}

						//Trace.Writeline("Actual counter-offer: " + to.offersFromLower[to.offersFromLower.Count - 1]);
					}

					to.status = (int)TradeOfferStatus.HigherResponsePending;

					//Trace.Writeline("Returning...\n");
					return null;
				}
			}
			else if (to.status == (int)TradeOfferStatus.PendingAccept)
			{
				// Could add code here to revoke offer if they wait too long

				// If there's no other offer on the table and they're waiting
				// to accept this one, just accept it and move on.
				bool anotherOffer = false;

				//Trace.Writeline("Processing pending acceptance...");

				foreach (TradeOffer locTO in tradeOffers.Values)
				{
					if (locTO.status != (int)TradeOfferStatus.Rejected && locTO.LowerTeam != to.LowerTeam)
					{
						anotherOffer = true;
					}
				}

				if (!anotherOffer && fracTimeLeft < 0.5)
				{
					//Trace.Writeline("No other trade around -- accept this one.  Returning...\n");

					if (to.LowerTeam == HumanTeamId && tradeUpForm != null)
					{
						tradeUpForm.HigherOffer((int)TradeResponse.Accept);
					}

					AcceptTrade(to);
					return to;
				}
			}

			//Trace.Writeline("Continue waiting.  Returning...\n");
			return null;
		}

		public int GetNextPick(int teamId, int pickNumber)
		{
			int nextpick = -1;
			for (int j = pickNumber + 1; j < model.TableModels[EditorModel.DRAFT_PICK_TABLE].RecordCount; j++)
			{
				if (GetDraftPickByNumber(j).CurrentTeamId == teamId)
				{
					nextpick = j;
					break;
				}
			}

			if (nextpick == -1)
			{
				nextpick = pickValues.Count - 1;
			}

			return nextpick;
		}

		public int GetBestOffer()
		{
			int bestId = -1;
			double bestOffer = 0;

			foreach (KeyValuePair<int, double> pair in BestOffers)
			{
				if (pair.Value > bestOffer)
				{
					bestId = pair.Key;
					bestOffer = pair.Value;
				}
			}

			return bestId;
		}

		// Initialize data structures; find the best offers for making a deal (minimum offer for the team
		// with the pick; maximum offer for all other teams)
		public void SetTradeParameters(int pickNumber)
		{
			DateTime total = DateTime.Now;
			tradeOffers = new Dictionary<int, TradeOffer>();
			BestOffers = new Dictionary<int, double>();

			favorites = GetFavoriteRookies(pickNumber);
			tradeUpForm = null;
			tradeDownForm = null;
			humanRejected = false;


			// Could pass this to the function from DraftForm -- then wouldn't need to do this.

			int currentSelector = -1;
			foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
			{
				DraftPickRecord record = (DraftPickRecord)rec;

				if (record.PickNumber == pickNumber)
				{
					currentSelector = record.CurrentTeamId;
					break;
				}
			}

			DateTime gp = DateTime.Now;
			GetProbabilities(pickNumber);
			//Trace.Writeline("Total GetProbabilities: " + gp.Subtract(DateTime.Now));


			for (int i = 0; i < 32; i++)
			{
				double favoriteEffectiveValue = favorites[i].EffectiveValue(model.TeamModel.GetTeamRecord(i), pickNumber, dcr.awarenessAdjust);
				double favoriteValue = favorites[i].AverageValue(model.TeamModel.GetTeamRecord(i), dcr.awarenessAdjust);

				if (i != currentSelector)
				{
					// Find maximum value to trade up

					// double wantfrac = Math.Tanh(favoriteEffectiveValue / pickValues[pickNumber]) / Math.Tanh(1.0);
					double wantfrac = 1.15 * Math.Tanh(favoriteValue / pickValues[pickNumber]);

					int nextpick = GetNextPick(i, pickNumber);

					foreach (KeyValuePair<int, RookieRecord> rook in rookies)
					{
						if (rook.Value.PlayerID == favorites[i].PlayerID || rook.Value.DraftPickTeam < 32) { continue; }
						if (rook.Value.AverageNeed(model.TeamModel.GetTeamRecord(i), pickNumber, dcr.awarenessAdjust) <= positionData[rook.Value.Player.PositionId].Threshold ||
							// replace with statement on league projected position
							rook.Value.AverageValue(model.TeamModel.GetTeamRecord(i), dcr.awarenessAdjust) <= 0.75 * pickValues[nextpick]) { continue; }

						double tempEV = rook.Value.EffectiveValue(model.TeamModel.GetTeamRecord(i), pickNumber, dcr.awarenessAdjust);
						double wantfracTemp = Math.Tanh((2.0 * favoriteEffectiveValue - tempEV) / tempEV);

						if (wantfracTemp > 0.95) { wantfracTemp = 1; }
						else
						{

						}

						wantfrac *= wantfracTemp;
					}

					double probabilityRemaining = 1;
					double probabilityTaken = 0;

					foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
					{
						DraftPickRecord record = (DraftPickRecord)rec;

						if (record.PickNumber < pickNumber) { continue; }
						if (record.CurrentTeamId == i) { break; }

						double pickfactor = Math.Max(0, (1.0 - 1.014 * Math.Tanh(((double)record.PickNumber - (double)pickNumber - 6.0) / 2.0)));

						probabilityTaken += probs[record.CurrentTeamId][favorites[i].PlayerID] * probabilityRemaining *
							(0.5) * pickfactor;
						probabilityRemaining *= (1.0 - probs[record.CurrentTeamId][favorites[i].PlayerID]);

						if (favoriteEffectiveValue < 0)
						{
							//Trace.Writeline("FEV");
						}
						else if (probs[record.CurrentTeamId][favorites[i].PlayerID] > 1)
						{
							//Trace.Writeline(probs[record.CurrentTeamId][favorites[i].PlayerId]);
						}
						else if (wantfrac < 0)
						{
							//Trace.Writeline("wantfrac");
						}
						else if (probabilityTaken < 0)
						{
							//Trace.Writeline("probtaken");
						}

						if (pickfactor == 0)
						{
							break;
						}
					}

					BestOffers[i] = favoriteEffectiveValue * wantfrac * probabilityTaken;

					/*
                    //Trace.Writeline(model.TeamModel.GetTeamNameFromTeamId(i) + " " + Math.Round(BestOffers[i]) + " " + favorites[i].Player.ToString());

                    foreach (RookieRecord rook in GetDraftBoard(model.TeamModel.GetTeamRecord(i), pickNumber))
                    {
                        if (rook.DraftedTeam < 32) { continue; }
                        //Trace.Writeline(rook.Player.PlayerId + " " + rook.Player.ToString() + " " + rook.EffectiveValue(model.TeamModel.GetTeamRecord(i), pickNumber, dcr.awarenessAdjust) + " " + rook.ActualValue + " " + rook.values[i][rook.Player.PositionId][(int)RookieRecord.ValueType.NoProg] + " " + rook.Player.Overall + " " + rook.GetAdjustedOverall(i, (int)RookieRecord.RatingType.Final, rook.Player.PositionId, dcr.awarenessAdjust) + " " + (rook.PreCombineScoutedHours[i] + rook.PostCombineScoutedHours[i]));
                    }
                    //Trace.Writeline("");
                     * */
				}
			}

			//Trace.Writeline("Total SetTradeParameters: " + total.Subtract(DateTime.Now));
		}

		public string MDCVerify(string filename)
		{
			StreamReader sr = new StreamReader(filename);
			PlayerRecord record = model.PlayerModel.GetNextPlayerRecord();

			int version;
			if (!Int32.TryParse(sr.ReadLine().Split(':')[1].Trim(), out version))
			{
				sr.Close();
				return "Version line incorrectly formatted.";
			}

			int linesread = 0;
			while (sr.EndOfStream == false && linesread < 260)
			{
				string playerLine = sr.ReadLine();
				linesread++;

				List<string> playerData = new List<string>(playerLine.Split('\t'));

				if (playerData[playerData.Count - 1] == "")
				{
					playerData.RemoveAt(playerData.Count - 1);
				}

				if (playerData.Count != (model.DraftClassFields[version - 1].GetLength(0)))
				{
					sr.Close();
					return "Number of fields on line " + (linesread + 1) + " is incorrect.  Was this MDC file generated with the same version of the Madden Editor as you are using?";
				}

				for (int i = 0; i < playerData.Count; i++)
				{
					if (record.ContainsIntField(model.DraftClassFields[version - 1][i]))
					{
						int test;
						if (!Int32.TryParse(playerData[i], out test))
						{
							sr.Close();
							return "Field number " + (i + 1) + " on line " + (linesread + 1) + " is not an integer.";
						}
					}
					else if (!record.ContainsStringField(model.DraftClassFields[version - 1][i]))
					{
						// This should never happen -- it's more debugging code for us.
						//sr.Close();
						//return "Field " + model.DraftClassFields[version][i] + " does not appear in player record.";
					}
				}
			}

			sr.Close();

			if (linesread != 257)
			{
				return "This file contains " + linesread + " rookies.  It needs to contain exactly 257.";
			}

			return null;
		}

		public void ImportRookies(string filename)
		{
			StreamReader sr = new StreamReader(filename);

			int versionNumber = Int32.Parse(sr.ReadLine().Split(':')[1].Trim());

			foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (sr.EndOfStream == true)
				{
					//Trace.Writeline("End of stream.  Breaking...");
					break;
				}

				PlayerRecord player = (PlayerRecord)rec;

				if (player.YearsPro != 0 || (player.FirstName == "New" && player.LastName == "Player")) { continue; }

				//Trace.Writeline("Out: " + player.FirstName + " " + player.LastName);

				// This should be false already, so this shouldn't hurt.
				//player.SetDeleteFlag(false);

				string playerLine = sr.ReadLine();

				List<string> playerData = new List<string>(playerLine.Split('\t'));

				player.ImportData(playerData, versionNumber);
			}

			if (!sr.EndOfStream)
			{
				//Trace.Writeline("Not at end of file!");
				//Trace.Writeline(sr.ReadToEnd());
			}

			sr.Close();

            
		}

		
		private void DumpProjections()
		{
			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				//Trace.Writeline(rook.Value.Player.ToString() + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Initial] + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Combine] + " " + rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Final]);
			}
		}

		private void SetInitialRookieAttributes()
		{
			Dictionary<int, int> initialErrors = new Dictionary<int, int>();
			initialErrors[(int)MaddenAttribute.INJ] = 40;
			initialErrors[(int)MaddenAttribute.SPD] = 20;
			initialErrors[(int)MaddenAttribute.ACC] = 30;
			initialErrors[(int)MaddenAttribute.AGI] = 30;
			initialErrors[(int)MaddenAttribute.JMP] = 20;
			initialErrors[(int)MaddenAttribute.AWR] = 30;
			initialErrors[(int)MaddenAttribute.STR] = 20;
			initialErrors[(int)MaddenAttribute.CTH] = 20;
			initialErrors[(int)MaddenAttribute.CAR] = 30;
			initialErrors[(int)MaddenAttribute.BTK] = 20;
			initialErrors[(int)MaddenAttribute.TAK] = 30;
			initialErrors[(int)MaddenAttribute.THP] = 20;
			initialErrors[(int)MaddenAttribute.THA] = 30;
			initialErrors[(int)MaddenAttribute.PBK] = 30;
			initialErrors[(int)MaddenAttribute.RBK] = 30;
			initialErrors[(int)MaddenAttribute.KPR] = 20;
			initialErrors[(int)MaddenAttribute.KAC] = 20;
			initialErrors[(int)MaddenAttribute.KRT] = 30;
			initialErrors[(int)MaddenAttribute.STA] = 40;
			initialErrors[(int)MaddenAttribute.TGH] = 20;

			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				Dictionary<int, double> baseInitials = new Dictionary<int, double>();

				rook.Value.ActualRatings[(int)MaddenAttribute.TGH] = rook.Value.Player.Toughness;
				rook.Value.ActualRatings[(int)MaddenAttribute.THA] = rook.Value.Player.ThrowAccuracy;
				rook.Value.ActualRatings[(int)MaddenAttribute.THP] = rook.Value.Player.ThrowPower;
				rook.Value.ActualRatings[(int)MaddenAttribute.INJ] = rook.Value.Player.Injury;
				rook.Value.ActualRatings[(int)MaddenAttribute.STA] = rook.Value.Player.Stamina;
				rook.Value.ActualRatings[(int)MaddenAttribute.SPD] = rook.Value.Player.Speed;
				rook.Value.ActualRatings[(int)MaddenAttribute.AGI] = rook.Value.Player.Agility;
				rook.Value.ActualRatings[(int)MaddenAttribute.ACC] = rook.Value.Player.Acceleration;
				rook.Value.ActualRatings[(int)MaddenAttribute.STR] = rook.Value.Player.Strength;
				rook.Value.ActualRatings[(int)MaddenAttribute.JMP] = rook.Value.Player.Jumping;
				rook.Value.ActualRatings[(int)MaddenAttribute.CTH] = rook.Value.Player.Catching;
				rook.Value.ActualRatings[(int)MaddenAttribute.CAR] = rook.Value.Player.Carrying;
				rook.Value.ActualRatings[(int)MaddenAttribute.BTK] = rook.Value.Player.BreakTackle;
				rook.Value.ActualRatings[(int)MaddenAttribute.TAK] = rook.Value.Player.Tackle;
				rook.Value.ActualRatings[(int)MaddenAttribute.PBK] = rook.Value.Player.PassBlocking;
				rook.Value.ActualRatings[(int)MaddenAttribute.RBK] = rook.Value.Player.RunBlocking;
				rook.Value.ActualRatings[(int)MaddenAttribute.KPR] = rook.Value.Player.KickPower;
				rook.Value.ActualRatings[(int)MaddenAttribute.KAC] = rook.Value.Player.KickAccuracy;
				rook.Value.ActualRatings[(int)MaddenAttribute.KRT] = rook.Value.Player.KickReturn;
				rook.Value.ActualRatings[(int)MaddenAttribute.AWR] = rook.Value.Player.Awareness;

				for (int i = 3; i < 23; i++)
				{
					baseInitials[i] = rook.Value.ActualRatings[i] + initialErrors[i] * (rand.NextDouble() - 0.5);
				}

				for (int i = 0; i < 32; i++)
				{
					for (int j = 3; j < 23; j++)
					{
						rook.Value.ratings[i][(int)RookieRecord.RatingType.Initial][j] = baseInitials[j];
					}
				}

				// rook.Value.CalculateOverall((int)RookieRecord.RatingType.Initial);
			}
		}

		private void SetCombineRookieAttributes()
		{
			Dictionary<int, int> measureableErrors = new Dictionary<int, int>();

			measureableErrors[(int)MaddenAttribute.SPD] = 10;
			measureableErrors[(int)MaddenAttribute.INJ] = 20;
			measureableErrors[(int)MaddenAttribute.ACC] = 20;
			measureableErrors[(int)MaddenAttribute.AGI] = 20;
			measureableErrors[(int)MaddenAttribute.JMP] = 10;
			measureableErrors[(int)MaddenAttribute.AWR] = 30;
			measureableErrors[(int)MaddenAttribute.STR] = 10;

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
							math.bellcurve(cv, sigma);
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
							((rook.Value.PreCombineScoutedHours[i] + rook.Value.PostCombineScoutedHours[i]) / 10.0) * (rook.Value.ActualRatings[j] - rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j]);
						double sigma = Math.Abs((0.5) * (rook.Value.ratings[i][(int)RookieRecord.RatingType.Combine][j] - cv));

						rook.Value.ratings[i][(int)RookieRecord.RatingType.Final][j] =
							math.bellcurve(cv, sigma);
					}
				}
			}
		}

		private void DoCPURookieScouting(bool beforeCombine)
		{
			for (int i = 0; i < 32; i++)
			{

				if (i == HumanTeamId)
				{
					continue;
				}

				// First calculate this team's relative need at each position
				Dictionary<int, double> positionNeeds = new Dictionary<int, double>();
				TeamRecord team = model.TeamModel.GetTeamRecord(i);

				/*
                for (int j = 0; j < 21; j++) {
                    positionNeeds[j] = positionData[j].Value(team.DefensiveSystem) * ScoutNeed(team, j);
                }
                 * */

				// Determine relative scouting priority.  For now, we'll see that relative scouting priority
				// is positionNeeds[positionId]*Log[value];
				Dictionary<int, double> scoutingPriority = new Dictionary<int, double>();
				double maxPriority = 0;
				foreach (KeyValuePair<int, RookieRecord> rook in rookies)
				{

					// We used to use the ScoutNeed function to determine
					// general need at positions, then multiply by
					// the log of value to determine scouting priority.
					//
					// This was done because taking a lot of something like
					// RookieRecord.EffectiveValue essentially
					// penalizes low-rated guys twice -- once
					// for low value, and once more since low value
					// translates into low need.
					//
					// With the ability now to draft a guy for a position
					// other than his assigned position, this gets a lot
					// more complicated.  So, let's instead go back to using
					// EffectiveValue and see how it works.  We might want to 
					// change this again later.
					// 
					// We'll compensate by taking the square root of scoutingPriority
					// before the combine, and just use the log afterward.  (We used to
					// use the log before, and then square the log afterward.)

					if (rook.Value.EffectiveValue(team, 25, dcr.awarenessAdjust) > 1)
					{
						scoutingPriority[rook.Key] = Math.Log(rook.Value.EffectiveValue(team, 25, dcr.awarenessAdjust));
					}
					else
					{
						scoutingPriority[rook.Key] = 0;
					}

					if (scoutingPriority[rook.Key] > maxPriority)
					{
						maxPriority = scoutingPriority[rook.Key];
					}

					if (beforeCombine)
					{
						scoutingPriority[rook.Key] = Math.Pow(scoutingPriority[rook.Key], 0.5);
					}
				}

				double total = 0;
				foreach (KeyValuePair<int, double> pair in scoutingPriority)
				{

					if (pair.Value / maxPriority > 0.05)
					{
						total += pair.Value;
					}
				}

				Dictionary<int, double> spcopy = new Dictionary<int, double>();
				foreach (KeyValuePair<int, double> pair in scoutingPriority)
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

				Dictionary<int, int> scoutingHours = new Dictionary<int, int>();
				int TotalToPrint = 0;
				int remaining = scoutingPriority.Count;

				Dictionary<int, double> spcopyOutside = new Dictionary<int, double>();

				foreach (KeyValuePair<int, double> pair in scoutingPriority)
				{
					spcopyOutside[pair.Key] = pair.Value;
				}

				foreach (KeyValuePair<int, double> pair in scoutingPriority)
				{
					if (pair.Value > 5)
					{
						double excess = (pair.Value - 5) / remaining;

						foreach (KeyValuePair<int, double> insidepair in scoutingPriority)
						{
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

				//Trace.Writeline(TotalToPrint);

				foreach (KeyValuePair<int, int> pair in scoutingHours)
				{
					if (beforeCombine)
					{
						rookies[pair.Key].PreCombineScoutedHours[team.TeamId] = pair.Value;
					}
					else
					{
						rookies[pair.Key].PostCombineScoutedHours[team.TeamId] = pair.Value;
					}

					//                    //Trace.Writeline(i + " " + rookies[pair.Key].Player + " " + pair.Key + " " + pair.Value);
				}
			}
		}

		/*
        private double ScoutNeed(TeamRecord team, int PositionId)
        {

            double starterNeed = 0;
            double backupNeed = 0;
            double successorNeed = 0;

            double rookval = positionData[PositionId].Value(team.DefensiveSystem)*
                math.valcurve(80 + 2.5*(5-team.CON));

            int numStarters = positionData[PositionId].Starters(team.DefensiveSystem);
            int startTemp;

            if (depthChart[team.TeamId][PositionId].Count < numStarters)
            {

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
*/

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
				if (record.TeamId >= 32) { continue; }

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
					//Trace.Writeline("SEVERE ERROR!  LINE 115 of DraftModel.cs");
				}

				record.CurrentTeamId = finalDraftOrder[count];
				record.OriginalTeamId = newDraftOrder[count];

				count++;
			}

		}

		public Dictionary<int, RookieRecord> GetFavoriteRookies(int pickNumber)
		{
			Dictionary<int, RookieRecord> toReturn = new Dictionary<int, RookieRecord>();
			int CurrentSelector = GetDraftPickByNumber(pickNumber).CurrentTeamId;

			foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
			{
				//Trace.Writeline(5 - pickNumber / 32);
				if (team.Value.TeamId >= 32) { continue; }

				double HighestValue = 0;
				RookieRecord BestPlayer = null;

				int startvalue = -1;
				if (team.Value.TeamId == CurrentSelector)
				{
					startvalue = pickNumber + 10;
				}
				else
				{
					startvalue = pickNumber + 1;
				}

				int nextpick = GetNextPick(team.Value.TeamId, startvalue);

				foreach (KeyValuePair<int, RookieRecord> rook in rookies)
				{

					if (rook.Value.DraftPickTeam < 32) { continue; }
					if (rook.Value.EffectiveValue(team.Value, pickNumber, dcr.awarenessAdjust) > HighestValue &&
						rook.Value.AverageNeed(team.Value, pickNumber, dcr.awarenessAdjust) > positionData[rook.Value.Player.PositionId].Threshold &&
						(rook.Value.PreCombineScoutedHours[team.Value.TeamId] + rook.Value.PostCombineScoutedHours[team.Value.TeamId]) >= (5 - pickNumber / 32) &&
						// replace with statement on league projected position
						// rook.Value.AverageValue(team.Value, dcr.awarenessAdjust) > 0.75 * pickValues[nextpick])
                        (pickNumber + 16 < nextpick || rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Final] < nextpick || 
                        rand.NextDouble() > (1.0 + Math.Tanh((rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Final] - nextpick - 32.0) / 16.0)) / 2.0))
					{

						HighestValue = rook.Value.EffectiveValue(team.Value, pickNumber, dcr.awarenessAdjust);
						BestPlayer = rook.Value;
					}
				}

				if (BestPlayer == null)
				{
					HighestValue = 0;
					foreach (KeyValuePair<int, RookieRecord> rook in rookies)
					{
						// Take the best player available -- except at certain positions
						if (rook.Value.DraftPickTeam < 32 ||
							rook.Value.Player.PositionId == (int)MaddenPositions.QB ||
							rook.Value.Player.PositionId == (int)MaddenPositions.FB ||
							rook.Value.Player.PositionId == (int)MaddenPositions.P ||
							rook.Value.Player.PositionId == (int)MaddenPositions.K) 
                        { continue; }

						if (rook.Value.AverageValue(team.Value, dcr.awarenessAdjust) > HighestValue)
						{
							HighestValue = rook.Value.AverageValue(team.Value, dcr.awarenessAdjust);
							BestPlayer = rook.Value;
						}
					}
				}

				// If we still don't have a best player, take best available overall,
				// regardless of position.
				if (BestPlayer == null)
				{
					HighestValue = 0;
					foreach (KeyValuePair<int, RookieRecord> rook in rookies)
					{
						if (rook.Value.AverageValue(team.Value, dcr.awarenessAdjust) > HighestValue)
						{
							HighestValue = rook.Value.AverageValue(team.Value, dcr.awarenessAdjust);
							BestPlayer = rook.Value;
						}
					}
				}

				toReturn.Add(team.Value.TeamId, BestPlayer);
			}
			/*
            foreach(KeyValuePair<int,RookieRecord> rook in favorites) {
                //Trace.Writeline(rook.Key + " " + rook.Value.Player.ToString());
            }
            */
			return toReturn;
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
					total += rook.Value.AverageValue(model.TeamModel.GetTeamRecord(i), dcr.awarenessAdjust);
				}
				AverageValues[rook.Key] = total / 32;
			}

			List<int> SortedAverageValues = new List<int>();

			for (int i = 0; i < rookies.Count; i++)
			{
				double bestRating = -1;
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
				double bestRating = -1;
				int bestId = -1;

				foreach (KeyValuePair<int, RookieRecord> rook in rookies)
				{
					if (rook.Value.AverageValue(model.TeamModel.GetTeamRecord(HumanTeamId), dcr.awarenessAdjust) > bestRating && !RookieRanks.ContainsKey(rook.Key))
					{
						bestId = rook.Key;
						bestRating = rook.Value.AverageValue(model.TeamModel.GetTeamRecord(HumanTeamId), dcr.awarenessAdjust);
					}
				}

				RookieRanks.Add(bestId, i);
			}

			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{

				rook.Value.CombineNumbers[(int)CombineStat.Forty] =
					Math.Round(100 * (4.25 + 0.2 * (99 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.SPD]) / 10)) / 100;

				rook.Value.CombineNumbers[(int)CombineStat.Shuttle] =
					Math.Round(100 * (3.75 + 0.18 * (99 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.ACC]) / 10)) / 100;

				rook.Value.CombineNumbers[(int)CombineStat.Cone] =
					Math.Round(100 * (6.60 + 0.23 * (99 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.AGI]) / 10)) / 100;

				rook.Value.CombineNumbers[(int)CombineStat.BenchPress] =
					Math.Max((Math.Round(42 - 5.5 * (99 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.STR]) / 10)), 0);

				rook.Value.CombineNumbers[(int)CombineStat.Vertical] =
					42 - 3 * (99 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.JMP]) / 10;

				rook.Value.CombineNumbers[(int)CombineStat.Doctor] =
					9.9 - rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.INJ] / 10;

				rook.Value.CombineNumbers[(int)CombineStat.RoundGrade] = RookieRanks[rook.Key];
				rook.Value.CombineNumbers[(int)CombineStat.Wonderlic] = Math.Max(Math.Round(rook.Value.ratings[HumanTeamId][type][(int)MaddenAttribute.AWR] / 2), 0);

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

		private void SetNeeds()
		{
			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
				{
					if (team.Value.TeamId >= 32) { continue; }
					foreach (KeyValuePair<int, double> pair in dcr.awarenessAdjust[rook.Value.Player.PositionId])
					{
						if (pair.Key > 20) { continue; }

						rook.Value.CalculateNeeds(team.Value, depthChart[team.Value.TeamId][pair.Key], depthChartValues[team.Value.TeamId][pair.Key], positionData, pair.Key);
					}
				}
			}
		}

		private void SetRookieValues(int type)
		{
			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{

				foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
				{
					if (team.Value.TeamId >= 32) { continue; }

					foreach (KeyValuePair<int, double> pair in dcr.awarenessAdjust[rook.Value.Player.PositionId])
					{
						if (pair.Key > 20) { continue; }

						// INJURY CHANGE
						//
						// For values with progression, take out injury boost -- a player's
						// INJ should have no bearing on their likelihood of starting.

						rook.Value.values[team.Key][pair.Key][(int)RookieRecord.ValueType.NoProg] =
							LocalMath.ValueScale * positionData[pair.Key].Value(team.Value.DefensiveSystem) * math.valcurve(rook.Value.GetAdjustedOverall(team.Value.TeamId, type, pair.Key, dcr.awarenessAdjust) + math.injury(rook.Value.ratings[team.Key][type][(int)MaddenAttribute.INJ], positionData[pair.Key].DurabilityNeed));

						rook.Value.values[team.Key][pair.Key][(int)RookieRecord.ValueType.WithProg] =
							LocalMath.ValueScale * positionData[pair.Key].Value(team.Value.DefensiveSystem) * math.valcurve(/*5.0 * (5.0 - (double)team.Value.CON) / 2*/ math.pointboost(rook.Value.Player, team.Value.CON, 40) + rook.Value.GetAdjustedOverall(team.Value.TeamId, type, pair.Key, dcr.awarenessAdjust) /*+  math.injury(rook.Value.ratings[team.Key][type][(int)MaddenAttribute.INJ], positionData[pair.Key].DurabilityNeed) */);
					}
				}
			}
		}

		private void SetPerceivedRookieValues()
		{
			foreach (KeyValuePair<int, RookieRecord> rook in rookies)
			{
				foreach (KeyValuePair<int, double> pair in dcr.awarenessAdjust[rook.Value.Player.PositionId])
				{
					if (pair.Key > 20) 
                    { continue; }

					double totalAtPosition = 0;

					foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
					{
						if (team.Value.TeamId >= 32) { continue; }
						totalAtPosition += rook.Value.values[team.Key][pair.Key][(int)RookieRecord.ValueType.NoProg];
					}

					foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
					{
						if (team.Value.TeamId >= 32) { continue; }
						rook.Value.values[team.Key][pair.Key][(int)RookieRecord.ValueType.Perceived] = (totalAtPosition / 32.0)
							* (0.9 + 0.2 * rand.NextDouble() + 0.02 * (double)(rook.Value.PreCombineScoutedHours[team.Key] + rook.Value.PostCombineScoutedHours[team.Key]) * rand.NextDouble());
					}
				}
			}
		}

		/*
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

            
			int team = 10;
			//Trace.Writeline(model.TeamModel.GetTeamRecord(team).CON);

			for (int i = 0; i < depthChart[team].Count; i++) {
				for(int j = 0; j < depthChart[team][i].Count; j++) {
					PlayerRecord rec = depthChart[team][i][j];
					//Trace.Writeline(rec.PositionId + " " + j + " " + rec.EffectiveOVR + " " + rec.Overall + " " + rec.Injury + " " + math.injury(rec.Injury) + " " + rec.YearsPro);
				}
			}
            
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
*/

		// Set rookie drafted teams to TeamRecord.NO_TEAM_ID, add to rookies dictionary, 
		// assign corresponding PlayerRecord to RookieRecord
		private void ExtractRookies()
		{
			foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFTED_PLAYERS_TABLE].GetRecords())
			{
				RookieRecord record = (RookieRecord)rec;

				//record.DraftPickTeam = TeamEditingModel.NO_TEAM_ID;
				//record.DraftPickNumber = 511;
				record.dm = this;
                record.model = this.model;
				record.SetPlayerRecord(model.PlayerModel.GetPlayerByPlayerId(record.PlayerID));

				record.InitializeDictionaries(dcr.awarenessAdjust);
                record.Player.Overall = rand.Next(01, 100);
				rookies.Add(record.PlayerID, record);
			}
		}

		// This is an incredibly inefficient sort, but we'll only be sorting 4-5 element
		// in general.  I didn't overload the operator because we may want to use that
		// for something more useful, like TEAM OVR or something.

		private List<TeamRecord> SortByEffectiveSOS(List<TeamRecord> tier)
		{
			List<TeamRecord> toReturn = new List<TeamRecord>();
			while (tier.Count > 0)
			{
				int leastESOS = 10000;
				int leastTeamIndex = -1;

				for (int j = 0; j < tier.Count; j++)
				{
					if (tier[j].EffectiveSOS < leastESOS)
					{
						leastESOS = tier[j].EffectiveSOS;
						leastTeamIndex = j;
					}
				}

				toReturn.Add(tier[leastTeamIndex]);
				tier.RemoveAt(leastTeamIndex);
			}

			return toReturn;
		}

		public int NumRooks
		{
			get
			{
				return numrooks;
			}
		}

		public string AnalyzeDraftClass(bool brief)
		{
			int totalOver80 = 0;
			int totalOver75 = 0;
			int totalOver70 = 0;
			double injuryTotal = 0;

			Dictionary<int, int> over80 = new Dictionary<int, int>();
			Dictionary<int, int> over75 = new Dictionary<int, int>();
			Dictionary<int, int> over70 = new Dictionary<int, int>();
			Dictionary<int, int> total = new Dictionary<int, int>();
			Dictionary<int, double> injury = new Dictionary<int, double>();
			List<double> values = new List<double>();

			InitializePositionData();
			InitializePickValues();

			for (int i = 0; i < 21; i++)
			{
				over70[i] = 0;
				over75[i] = 0;
				over80[i] = 0;
				total[i] = 0;
				injury[i] = 0;
			}

			numrooks = 0;

			foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				PlayerRecord player = (PlayerRecord)rec;

				if (player.YearsPro == 0 && player.Deleted == false && !(player.FirstName == "New" && player.LastName == "Player"))
				{
					numrooks++;
					player.Overall = player.CalculateOverallRating(player.PositionId);
					total[player.PositionId]++;

					if (player.Overall >= 80)
					{
						over80[player.PositionId]++;
						totalOver80++;
					}

					if (player.Overall >= 75)
					{
						over75[player.PositionId]++;
						totalOver75++;
					}

					if (player.Overall >= 70)
					{
						over70[player.PositionId]++;
						totalOver70++;
					}

					injury[player.PositionId] += player.Injury;
					injuryTotal += player.Injury;

					values.Add(LocalMath.ValueScale * positionData[player.PositionId].Value((int)TeamRecord.Defense.Front43) * math.valcurve(player.Overall + math.injury(player.Injury, positionData[player.PositionId].DurabilityNeed)));
				}
			}

			values.Sort();
			values.Reverse();

			double variance = 0;
			double varianceSquared = 0;
			double weightedVariance = 0;
			double weightedVarianceSquared = 0;
			double weightedDenominator = 0;

			for (int i = 0; i < 32 * 7; i++)
			{
				//Trace.Writeline(i + ": " + pickValues[i] + " " + values[i]);
				variance += (values[i] - pickValues[i]) / pickValues[i];
				varianceSquared += Math.Pow((values[i] - pickValues[i]) / pickValues[i], 2);

				weightedVariance += Math.Log(pickValues[i]) * (values[i] - pickValues[i]) / pickValues[i];
				weightedVarianceSquared += Math.Log(pickValues[i]) * Math.Pow((values[i] - pickValues[i]) / pickValues[i], 2);
				weightedDenominator += Math.Log(pickValues[i]);
			}

			double[] ideals = new double[21] { 14, 16, 7, 22, 12, 11, 11, 11, 11, 11, 11, 11, 18, 11, 15, 11, 22, 11, 11, 6, 6 };

			// There's an assumption here that guys will go through minicamps to 
			// improve their ratings.  But, that doesn't apply to FB, TE, and OL's.
			// So, we should have on average higher ratings for them coming out.
			double[] extras = new double[21] { 1, 1, 1.1, 1, 1, 1.2, 1.2, 1.2, 1.2, 1.2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            string toReturn = "";

            if (!brief)
            {
                toReturn += "Number of players at each position, grouped by rating (This class / Average Class):\n\n";

                for (int i = 0; i < 21; i++)
                {
                    ////Trace.Writeline(Enum.GetNames(typeof(MaddenPositions))[i] + "\t" + Math.Round(ideals[i]*extras[i] * 40.0 / 256.0, 1) + "\t" + over75[i]);
                    ////Trace.Writeline(over75[i]);
                    toReturn += Enum.GetNames(typeof(MaddenPositions))[i].ToString() + ":  ";
                    toReturn += "80+ (" + over80[i] + "/" + Math.Round(extras[i] * ideals[i] * 10.0 / 257.0, 1) + "), ";
                    toReturn += "75+ (" + over75[i] + "/" + Math.Round(extras[i] * ideals[i] * 40.0 / 257.0, 1) + "), ";
                    toReturn += "70+ (" + over70[i] + "/" + Math.Round(extras[i] * ideals[i] * 100.0 / 257.0, 1) + "), ";
                    toReturn += "Total (" + total[i] + "/" + ideals[i] + "), ";
                    toReturn += "Injury Average (" + Math.Round(injury[i] / total[i], 1) + "/ 80.0)\n";
                }
            } 
            else
            {
                int toRec = 0;

                if (variance < 257.0 * 0.2)
                {
                    toRec++;
                }
                else if (variance > 257.0 * 3.5 && variance < 257.0 * 5.0)
                {
                    //Trace.Writeline("line 3171");
                    toRec--;
                }

                if (varianceSquared < 257.0 * 0.25)
                {
                    toRec++;
                }
                else if (varianceSquared > 257.0 * 20.25 && varianceSquared < 257.0 * 25.0)
                {
                    //Trace.Writeline("line 3181");
                    toRec--;
                }
            

                if (weightedVariance < weightedDenominator * 0.2)
                {
                    toRec++;
                }
                else if (weightedVariance > weightedDenominator * 3.25 && weightedVariance < 4.75 * weightedDenominator)
                {
                    //Trace.Writeline("line 3192");
                    toRec--;
                }
                
                if (weightedVarianceSquared < weightedDenominator * 0.25)
                {
                    toRec++;
                }
                else if (weightedVarianceSquared > weightedDenominator * 16 && weightedVarianceSquared < weightedDenominator * 30.25)
                {
                    //Trace.Writeline("line 3202");
                    toRec--;
                }

                if (toRec >= 3)
                {
                    recommendation = 1; // Do not adjust, and use
                }
                else if (toRec <= -3)
                {
                    recommendation = -1; // Auto-adjust
                }
                else
                {
                    recommendation = 0; // Do not use
                }

                //Trace.Writeline("Recommendation: " + recommendation);
            }

			toReturn += "\nValue Variance: " + Math.Round(variance / 257.0, 2) + ", Value Variance Squared: " + Math.Round(Math.Pow(varianceSquared / 257.0, 0.5), 2) + "\n";
			toReturn += "\nWeighted Value Variance: " + Math.Round(weightedVariance / weightedDenominator, 2) + ", Weighted Value Variance Squared: " + Math.Round(Math.Pow(weightedVarianceSquared / weightedDenominator, 0.5), 2) + "\n\n";
			toReturn += "\"Value Variance\" indicates the general value of this class compared to the draft pick value chart.  A positive number means stronger than usual, negative number means weaker than usual.\n\n";
			toReturn += "\"Value Variance Squared\" is a strictly positive number that tells you how far this draft class is (in absolute value) compared to the draft pick value chart.  The closer this value is to zero, the better.  Most classes will have this value less than 0.2.  Values greater than 0.5 are unacceptable.\n\n";
			toReturn += "Weighted quantities are analogously defined, with added weight given to picks at the top of the draft.\n\n";

			toReturn += "\nTotals:  80+ (" + totalOver80 + "/10), 75+ (" + totalOver75 + "/40), 70+ (" + totalOver70 + "/100), Injury Average (" + Math.Round(injuryTotal / 256.0, 1) + "/80)\n";

            if (!brief)
            {
                toReturn += "\nExport this draft class?";
            }

			return toReturn;
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
			return (DraftPickRecord)model.TableModels[EditorModel.DRAFT_PICK_TABLE].CreateNewRecord(true);
		}

		private void InitializePositionData()
		{
			positionData.Add((int)MaddenPositions.QB, new Position(100, 100, 100, 1, 35, 0.7, 0.3, 1, 1, 0.9));
			positionData.Add((int)MaddenPositions.HB, new Position(60, 60, 60, 0.8, 33, 1, 0.05, 1, 1, 1));
			positionData.Add((int)MaddenPositions.FB, new Position(10, 10, 10, 0.3, 32, 0.2, 0.75, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.WR, new Position(65, 65, 65, 0.7, 35, 0.9, 0, 2, 2, 0.6));
			positionData.Add((int)MaddenPositions.TE, new Position(30, 30, 30, 0.6, 32, 0.6, 0.4, 1, 1, 0.7));
			positionData.Add((int)MaddenPositions.LT, new Position(70, 70, 70, 0.7, 36, 0.5, 0, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.LG, new Position(35, 35, 35, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.C, new Position(30, 30, 30, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.RG, new Position(35, 35, 35, 0.4, 36, 0.5, 0.1, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.RT, new Position(55, 55, 55, 0.6, 36, 0.5, 0, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.LE, new Position(50, 45, 55, 0.5, 35, 0.8, 0, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.RE, new Position(85, 55, 85, 0.5, 35, 0.8, 0, 1, 1, 0.8));
			positionData.Add((int)MaddenPositions.DT, new Position(65, 75, 65, 0.4, 35, 0.8, 0, 2, 1, 0.8));
			positionData.Add((int)MaddenPositions.LOLB, new Position(45, 65, 55, 0.5, 34, 0.4, 0.1, 1, 1, 0.7));
			positionData.Add((int)MaddenPositions.MLB, new Position(50, 55, 60, 0.5, 34, 0.4, 0.1, 1, 2, 0.7));
			positionData.Add((int)MaddenPositions.ROLB, new Position(50, 65, 60, 0.5, 34, 0.4, 0.1, 1, 1, 0.7));
			positionData.Add((int)MaddenPositions.CB, new Position(80, 75, 70, 0.7, 32, 0.9, 0, 2, 2, 0.6));
			positionData.Add((int)MaddenPositions.FS, new Position(40, 40, 50, 0.4, 32, 0.6, 0.05, 1, 1, 0.6));
			positionData.Add((int)MaddenPositions.SS, new Position(40, 40, 50, 0.4, 32, 0.6, 0.05, 1, 1, 0.6));
			positionData.Add((int)MaddenPositions.K, new Position(5, 5, 5, 0.1, 38, 0.1, 0.85, 1, 1, 0.2));
			positionData.Add((int)MaddenPositions.P, new Position(2, 2, 2, 0.1, 38, 0.1, 0.85, 1, 1, 0.2));
		}

		public double futureValues(int round, int con)
		{
			if (round < 1 || round > 7)
			{
				return 0;
			}

			List<double> startingValues = new List<double>();
			startingValues.Add(800);
			startingValues.Add(360);
			startingValues.Add(150);
			startingValues.Add(60);
			startingValues.Add(26);
			startingValues.Add(16);
			startingValues.Add(8);

			List<double> slope = new List<double>();
			slope.Add(100);
			slope.Add(50);
			slope.Add(20);
			slope.Add(8);
			slope.Add(2);
			slope.Add(1.5);
			slope.Add(1);

			return startingValues[round - 1] + (1 - con) * slope[round - 1];
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
            pickValues.Add(0);
            pickValues.Add(0);
		}



        
	}

	
}
