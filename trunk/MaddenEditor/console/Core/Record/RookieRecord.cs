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
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
    public enum MaddenAttribute
    {
        OVR = 0,
        AGE,
        YRP,
        INJ,
        SPD,
        AGI,
        ACC,
        STR,
        AWR,
        JMP,
        CTH,
        CAR,
        BTK,
        TAK,
        THA,
        THP,
        STA,
        PBK,
        RBK,
        KPR,
        KAC,
        KRT,
        TGH,
        EGO,
        VAL
    }

    public enum CombineStat
    {
        RoundGrade = 0,
        Forty,
        Shuttle,
        Cone,
        BenchPress,
        Doctor,
        Vertical,
        Wonderlic,
        Height
    }
        
    public class RookieRecord : DraftedPlayers
    {
        private PlayerRecord player;
        public DraftModel dm;

        public Dictionary<int, int> EstimatedPickNumber;
        public Dictionary<int, string> EstimatedRound;

        public Dictionary<int, double> PreCombineScoutedHours;
        public Dictionary<int, double> PostCombineScoutedHours;

        public Dictionary<int, double> CombineNumbers;
        public Dictionary<int, string> CombineWords;

        public Dictionary<int, double> ActualRatings;
        public double ActualValue;

        private double power = 10;

        public int changeovr;

        // Has structure ratings[TeamId][RatingType][Attribute]
        public Dictionary<int, Dictionary<int, Dictionary<int, double>>> ratings;

        // Has structure values[TeamId][PositionId][ValueType]
        public Dictionary<int, Dictionary<int, Dictionary<int, double>>> values;

        // Has structure needs[TeamId][PositionId][NeedType]
        public Dictionary<int, Dictionary<int, Dictionary<int, double>>> needs;

        public Dictionary<int, int> starterINJs;

		public RookieRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public void InitializeDictionaries(Dictionary<int, Dictionary<int, double>> awarenessAdjust) 
        {
            EstimatedPickNumber = new Dictionary<int,int>();
            EstimatedRound = new Dictionary<int,string>();

            PreCombineScoutedHours = new Dictionary<int,double>();
            PostCombineScoutedHours = new Dictionary<int,double>();

            CombineNumbers = new Dictionary<int, double>();
            CombineWords = new Dictionary<int, string>();

            ActualRatings = new Dictionary<int,double>();

            starterINJs = new Dictionary<int, int>();

            ratings = new Dictionary<int, Dictionary<int, Dictionary<int, double>>>();
            values = new Dictionary<int, Dictionary<int, Dictionary<int, double>>>();
            needs = new Dictionary<int, Dictionary<int, Dictionary<int, double>>>();

            for (int i = 0; i < 32; i++)
            {
                ratings.Add(i, new Dictionary<int, Dictionary<int, double>>());
                values.Add(i, new Dictionary<int, Dictionary<int, double>>());
                needs.Add(i, new Dictionary<int, Dictionary<int, double>>());

                for (int j = 0; j < 3; j++)
                {
                    ratings[i].Add(j, new Dictionary<int, double>());
                }

                foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
                {
                    values[i][pair.Key] = new Dictionary<int, double>();
                    needs[i][pair.Key] = new Dictionary<int, double>();
                }
            }
        }

        public void CalculateNeeds(TeamRecord team, List<PlayerRecord> depthChart, List<double> depthChartValues, Dictionary<int, Position> positionData, int thePosition)
        {
            double backupNeed = 0;
            double successorNeed = 0;
            LocalMath math = new LocalMath(dm.model.FileVersion);

            int numStarters = positionData[thePosition].Starters(team.DefensiveSystem);

            /** First calculate the starter need **/

            // If there's no current starter, they can always get a stop-gap guy
            // in free agency, who we'll assume has OVR = 78, INJ = 75, YRP >= 5,
            // so their effective overall is just 78.  This should at least prevent
            // the totally boneheaded picks.

            int stopgapEffectiveOVR = 78;
            double sgValue = LocalMath.ValueScale * positionData[thePosition].Value(team.DefensiveSystem) * math.valcurve(stopgapEffectiveOVR);

            if (depthChartValues.Count < numStarters)
            {
                needs[team.TeamId][thePosition][(int)NeedType.Starter] = math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], sgValue);
            }
            else
            {
                needs[team.TeamId][thePosition][(int)NeedType.Starter] = math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], Math.Max(depthChartValues[numStarters - 1], sgValue));
            }

            /** Now calculate backup need. **/

            if (numStarters == 1 && (thePosition != (int)MaddenPositions.TE && thePosition != (int)MaddenPositions.QB && thePosition != (int)MaddenPositions.HB))
            {
                if (depthChartValues.Count <= 1)
                {
                    backupNeed = 1;
                }
                else
                {
                    backupNeed = math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters]);
                }
            } 
            else 
            {
                if (thePosition != (int)MaddenPositions.CB && thePosition != (int)MaddenPositions.WR)
                {
                    if (depthChartValues.Count <= numStarters)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters]);
                    }

                    if (depthChartValues.Count <= numStarters + 1)
                    {
                        backupNeed += 0.25;
                    }
                    else
                    {
                        backupNeed = 0.25 * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters+1]);
                    }
                }
                else
                {
                    if (depthChartValues.Count <= 2)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters]);
                    }

                    if (depthChartValues.Count <= 3)
                    {
                        backupNeed += 0.35;
                    }
                    else
                    {
                        backupNeed += 0.35 * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters+1]);
                    }

                    if (depthChart.Count <= 4)
                    {
                        backupNeed += 0.15;
                    }
                    else
                    {
                        backupNeed += 0.15 * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[numStarters+2]);
                    }
                }
            }

            double injuryFactor = 0;
            starterINJs[thePosition] = 100;

            for (int i = 0; i < Math.Min(numStarters, depthChartValues.Count); i++)
            {
                injuryFactor += (1 - math.injury(depthChart[i].Injury, positionData[thePosition].DurabilityNeed) / 10) / Math.Min(numStarters, depthChartValues.Count);
                starterINJs[thePosition] = Math.Min(depthChart[i].Injury, starterINJs[thePosition]);
            }

            double backupTemp;

            // At these positions, backups are useful even if the starter isn't likely
            // to be injured
            if (thePosition == (int)MaddenPositions.HB || thePosition == (int)MaddenPositions.WR || thePosition == (int)MaddenPositions.DT || thePosition == (int)MaddenPositions.CB)
            {
                backupTemp = backupNeed * (0.75 * injuryFactor + 0.25);
            }
            else
            {
                backupTemp = backupNeed * injuryFactor;
            }

            if (backupTemp > 1.25) { backupTemp = 1.25; }

            needs[team.TeamId][thePosition][(int)NeedType.Backup] = backupTemp * positionData[thePosition].BackupNeed;

            /** Now calculate need for a successor. **/

            int oldest = 0;

            if (depthChartValues.Count >= numStarters)
            {
                if (numStarters == 2)
                {
                    if (depthChart[0].Age > depthChart[1].Age)
                    {
                        oldest = depthChart[0].Age;
                    }
                    else
                    {
                        oldest = depthChart[1].Age;
                    }
                }
                else
                {
                    oldest = depthChart[0].Age;
                }

                double factor = 1 - 0.2 * (positionData[thePosition].RetirementAge - oldest + (1 / 2) * (team.CON - 5));

                if (factor <= 0)
                {
                    successorNeed = 0;
                } 
                else 
                {
                    if (factor > 1) {factor = 1; }

                    int possibleSuccessor = -1;
                    for (int i = numStarters; i < depthChart.Count; i++) 
                    {
                        if (positionData[thePosition].RetirementAge - depthChart[i].Age + (1/2)*(team.CON - 5) >= 5) {
                            possibleSuccessor = i;
                            break;
                        }
                    }

                    if (possibleSuccessor == -1) 
                    {
                        successorNeed = factor * positionData[thePosition].SuccessorNeed;
                    } 
                    else 
                    {
                        if (values[team.TeamId][thePosition][(int)ValueType.WithProg] > depthChartValues[possibleSuccessor])
                        {
                            successorNeed = factor * positionData[thePosition].SuccessorNeed * math.need(values[team.TeamId][thePosition][(int)ValueType.WithProg], depthChartValues[possibleSuccessor]);
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

            needs[team.TeamId][thePosition][(int)NeedType.Successor] = successorNeed;
        }

        public double PrimarySkill(int TeamId, int type)
        {

            switch (Player.PositionId)
            {
                case (int)MaddenPositions.QB:
                    return ratings[TeamId][type][(int)MaddenAttribute.THA];
                case (int)MaddenPositions.HB:
                    return ratings[TeamId][type][(int)MaddenAttribute.BTK];
                case (int)MaddenPositions.FB:
                    return (7.0 / 8.0) * ratings[TeamId][type][(int)MaddenAttribute.RBK] + (1 / 8.0) * ratings[TeamId][type][(int)MaddenAttribute.PBK];
                case (int)MaddenPositions.WR:
                    return ratings[TeamId][type][(int)MaddenAttribute.CTH];
                case (int)MaddenPositions.TE:
                    return (5.0 / 6.0) * ratings[TeamId][type][(int)MaddenAttribute.CTH] + (1 / 6.0) * ratings[TeamId][type][(int)MaddenAttribute.BTK];
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                case (int)MaddenPositions.C:
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    return ratings[TeamId][type][(int)MaddenAttribute.PBK];
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.DT:
                case (int)MaddenPositions.RE:
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.MLB:
                case (int)MaddenPositions.ROLB:
                case (int)MaddenPositions.CB:
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    return ratings[TeamId][type][(int)MaddenAttribute.TAK];
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    return ratings[TeamId][type][(int)MaddenAttribute.KAC];                    
            }

            return -1;
        }

        public double SecondarySkill(int TeamId, int type)
        {

            switch (Player.PositionId)
            {
                case (int)MaddenPositions.QB:
                    return ratings[TeamId][type][(int)MaddenAttribute.THP];
                case (int)MaddenPositions.HB:
                    return (5.0 / 8.0) * ratings[TeamId][type][(int)MaddenAttribute.CAR] + (3.0 / 8.0) * ratings[TeamId][type][(int)MaddenAttribute.CTH];
                case (int)MaddenPositions.FB:
                    return ratings[TeamId][type][(int)MaddenAttribute.BTK];
                case (int)MaddenPositions.WR:
                    return ratings[TeamId][type][(int)MaddenAttribute.BTK];
                case (int)MaddenPositions.TE:
                    return (5.0 / 6.0) * ratings[TeamId][type][(int)MaddenAttribute.RBK] + (1.0 / 6.0) * ratings[TeamId][type][(int)MaddenAttribute.PBK];
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                case (int)MaddenPositions.C:
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    return ratings[TeamId][type][(int)MaddenAttribute.RBK];
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.DT:
                case (int)MaddenPositions.RE:
                    return -1;
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.MLB:
                case (int)MaddenPositions.ROLB:
                case (int)MaddenPositions.CB:
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    return ratings[TeamId][type][(int)MaddenAttribute.CTH];
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    return ratings[TeamId][type][(int)MaddenAttribute.KPR];
            }

            return -1;
        }

        public double GetAdjustedOverall(int TeamId, int type, int position, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
		{
			double tempOverall = 0;

			LocalMath lmath = new LocalMath(MaddenFileVersion.Ver2006);
			tempOverall += lmath.HeightWeightAdjust(Player, position);

            switch (position)
            {
                case (int)MaddenPositions.QB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.THP] - 50) / 10) * 4.9;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.THA] - 50) / 10) * 5.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.BTK] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 4.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.0;
                    tempOverall += 28;
                    break;
                case (int)MaddenPositions.HB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.PBK] - 50) / 10) * 0.33;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.BTK] - 50) / 10) * 3.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CAR] - 50) / 10) * 2.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 2.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.6;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 1.4;
                    tempOverall += 27;
                    break;
                case (int)MaddenPositions.FB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.PBK] - 50) / 10) * 1.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.RBK] - 50) / 10) * 7.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.BTK] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CAR] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 2.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 5.2;
                    tempOverall += 39;
                    break;
                case (int)MaddenPositions.WR:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.BTK] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 2.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 4.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.4;
                    tempOverall += 26;
                    break;
                case (int)MaddenPositions.TE:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.65;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 2.65;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 2.65;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.25;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.25;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 5.4;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.BTK] - 50) / 10) * 1.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.PBK] - 50) / 10) * 1.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.RBK] - 50) / 10) * 5.4;
                    tempOverall += 35;
                    break;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 3.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.PBK] - 50) / 10) * 4.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.RBK] - 50) / 10) * 3.75;
                    tempOverall += 26;
                    break;
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                case (int)MaddenPositions.C:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.7;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.25;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 3.25;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.7;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.PBK] - 50) / 10) * 3.25;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.RBK] - 50) / 10) * 4.8;
                    tempOverall += 28;
                    break;
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.RE:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 1.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 3.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 5.5;
                    tempOverall += 30;
                    break;
                case (int)MaddenPositions.DT:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 5.5;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 3.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.8;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 4.55;
                    tempOverall += 29;
                    break;
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.ROLB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 2.4;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 3.6;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.4;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 1.3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 4.8;
                    tempOverall += 29;
                    break;
                case (int)MaddenPositions.MLB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 0.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.4;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 5.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.65;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 5.2;
                    tempOverall += 27;
                    break;
                case (int)MaddenPositions.CB:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.85;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.9;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 3.85;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.55;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.35;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.55;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 1.55;
                    tempOverall += 28;
                    break;
                case (int)MaddenPositions.FS:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.9;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 4.85;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.5;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.5;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3.0;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.5;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 2.5;
                    tempOverall += 30;
                    break;
                case (int)MaddenPositions.SS:
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.STR] - 50) / 10) * 1.7;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] - 50) / 10) * 4.75;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.7;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.7;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3.2;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.JMP] - 50) / 10) * 0.9;
                    tempOverall += (((double)ratings[TeamId][type][(int)MaddenAttribute.TAK] - 50) / 10) * 3.2;
                    tempOverall += 30;
                    break;
                case (int)MaddenPositions.P:
                    tempOverall = (double)(-183 + 0.218 * ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] + 1.5 * ratings[TeamId][type][(int)MaddenAttribute.KPR] + 1.33 * ratings[TeamId][type][(int)MaddenAttribute.KAC]);
                    break;
                case (int)MaddenPositions.K:
                    tempOverall = (double)(-177 + 0.218 * ratings[TeamId][type][(int)MaddenAttribute.AWR]*awarenessAdjust[Player.PositionId][position] + 1.28 * ratings[TeamId][type][(int)MaddenAttribute.KPR] + 1.47 * ratings[TeamId][type][(int)MaddenAttribute.KAC]);
                    break;
            }

            if (tempOverall < 0)
            {
                tempOverall = 0;
            }

			// No rookie should be rated over 85, so teams shouldn't 
			// perceive them as higher than 85.
            /*
            if (tempOverall > 85)
            {
                tempOverall = 85;
            }
             * */

            double absMin = 85;
            switch (position)
            {
                case (int)MaddenPositions.QB:
                    absMin = 84; break;
                case (int)MaddenPositions.HB:
                    absMin = 88; break;
                case (int)MaddenPositions.FB:
                    absMin = 87; break;
                case (int)MaddenPositions.WR:
                    absMin = 85; break;
                case (int)MaddenPositions.TE:
                    absMin = 87; break;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                    absMin = 86; break;
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    absMin = 87; break;
                case (int)MaddenPositions.C:
                    absMin = 85; break;
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.RE:
                    absMin = 86; break;
                case (int)MaddenPositions.DT:
                    absMin = 85; break;
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.ROLB:
                    absMin = 87; break;
                case (int)MaddenPositions.MLB:
                    absMin = 87; break;
                case (int)MaddenPositions.CB:
                    absMin = 83; break;
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    absMin = 85; break;
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    absMin = 87; break;                   
            }

            return Math.Min(absMin, tempOverall);
        }

        /*
        public void CalculateOverall(int type, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            for (int i = 0; i < 32; i++) {
                double tempOverall = 0;

                foreach (KeyValuePair<int, TeamRecord> team in model.TeamModel.GetTeamRecords())
                {

                    switch (Player.PositionId)
                    {
                        case (int)MaddenPositions.QB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.THP] - 50) / 10) * 4.9;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.THA] - 50) / 10) * 5.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.BTK] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 4.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.0;
                            tempOverall += 28;
                            break;
                        case (int)MaddenPositions.HB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.PBK] - 50) / 10) * 0.33;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.BTK] - 50) / 10) * 3.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CAR] - 50) / 10) * 2.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 2.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.6;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 1.4;
                            tempOverall += 27;
                            break;
                        case (int)MaddenPositions.FB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.PBK] - 50) / 10) * 1.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.RBK] - 50) / 10) * 7.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.BTK] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CAR] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 2.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 5.2;
                            tempOverall += 39;
                            break;
                        case (int)MaddenPositions.WR:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.BTK] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 2.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 4.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.4;
                            tempOverall += 26;
                            break;
                        case (int)MaddenPositions.TE:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 2.65;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 2.65;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 2.65;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.25;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.25;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 5.4;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.BTK] - 50) / 10) * 1.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.PBK] - 50) / 10) * 1.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.RBK] - 50) / 10) * 5.4;
                            tempOverall += 35;
                            break;
                        case (int)MaddenPositions.LT:
                        case (int)MaddenPositions.RT:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 3.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.PBK] - 50) / 10) * 4.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.RBK] - 50) / 10) * 3.75;
                            tempOverall += 26;
                            break;
                        case (int)MaddenPositions.LG:
                        case (int)MaddenPositions.RG:
                        case (int)MaddenPositions.C:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.7;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.25;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 3.25;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 0.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.7;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.PBK] - 50) / 10) * 3.25;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.RBK] - 50) / 10) * 4.8;
                            tempOverall += 28;
                            break;
                        case (int)MaddenPositions.LE:
                        case (int)MaddenPositions.RE:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 1.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 3.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 5.5;
                            tempOverall += 30;
                            break;
                        case (int)MaddenPositions.DT:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 1.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 5.5;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 3.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.8;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 4.55;
                            tempOverall += 29;
                            break;
                        case (int)MaddenPositions.LOLB:
                        case (int)MaddenPositions.ROLB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 2.4;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 3.6;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 2.4;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 1.3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 4.8;
                            tempOverall += 29;
                            break;
                        case (int)MaddenPositions.MLB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 0.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 3.4;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 5.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.65;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 5.2;
                            tempOverall += 27;
                            break;
                        case (int)MaddenPositions.CB:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.85;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.9;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 3.85;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.55;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.35;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.55;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 1.55;
                            tempOverall += 28;
                            break;
                        case (int)MaddenPositions.FS:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 0.9;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 4.85;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.5;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 2.5;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3.0;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.JMP] - 50) / 10) * 1.5;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 2.5;
                            tempOverall += 30;
                            break;
                        case (int)MaddenPositions.SS:
                            tempOverall = (((double)ratings[i][type][(int)MaddenAttribute.SPD] - 50) / 10) * 3.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.STR] - 50) / 10) * 1.7;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AWR] - 50) / 10) * 4.75;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.AGI] - 50) / 10) * 1.7;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.ACC] - 50) / 10) * 1.7;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.CTH] - 50) / 10) * 3.2;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.JMP] - 50) / 10) * 0.9;
                            tempOverall += (((double)ratings[i][type][(int)MaddenAttribute.TAK] - 50) / 10) * 3.2;
                            tempOverall += 30;
                            break;
                        case (int)MaddenPositions.P:
                            tempOverall = (double)(-183 + 0.218 * ratings[i][type][(int)MaddenAttribute.AWR] + 1.5 * ratings[i][type][(int)MaddenAttribute.KPR] + 1.33 * ratings[i][type][(int)MaddenAttribute.KAC]);
                            break;
                        case (int)MaddenPositions.K:
                            tempOverall = (double)(-177 + 0.218 * ratings[i][type][(int)MaddenAttribute.AWR] + 1.28 * ratings[i][type][(int)MaddenAttribute.KPR] + 1.47 * ratings[i][type][(int)MaddenAttribute.KAC]);
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

                    ratings[i][type][(int)MaddenAttribute.OVR] = tempOverall;
                }
            }
        }
        */

        public double TotalNeed(TeamRecord team, int pickNumber, int position) {
            double injFac = 0;
            if (starterINJs[position] < 70)
                injFac = Math.Tanh((70.0 - (double)starterINJs[position]) / 20.0);

            double ProbabilityOfStarting = Math.Sqrt(needs[team.TeamId][position][(int)NeedType.Starter]);
            double toReturn =  (needs[team.TeamId][position][(int)NeedType.Starter] +
                Math.Max(Math.Tanh((double)pickNumber / 45), injFac) * (1 - ProbabilityOfStarting) * needs[team.TeamId][position][(int)NeedType.Backup] +
                Math.Tanh(((double)pickNumber + 5) / (5 * team.CON)) * (1 - ProbabilityOfStarting) * needs[team.TeamId][position][(int)NeedType.Successor]);
            return toReturn;
        }

        public double AverageValue(TeamRecord team, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            // Power essentially determines how we add together effective values
            // at multiple positions.  If power = 1, we just add them.  If power = 2,
            // we square them all, then add, then take a square root.  For now, we'll
            // use power = 5.

            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = values[team.TeamId][pair.Key][(int)ValueType.NoProg];
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }

        public double AverageNeed(TeamRecord team, int pickNumber, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = TotalNeed(team, pickNumber, pair.Key);
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }

        public double AverageStarterNeed(int teamId, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = needs[teamId][pair.Key][(int)NeedType.Starter];
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }

        public double AverageSuccessorNeed(int teamId, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = needs[teamId][pair.Key][(int)NeedType.Successor];
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }

        public double EffectiveValue(TeamRecord team, int pickNumber, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            double valfrac = 0.25 + 0.075 * Math.Floor((double)(pickNumber / 32));
            double needfrac = 1 - valfrac;

            // Power essentially determines how we add together effective values
            // at multiple positions.  If power = 1, we just add them.  If power = 2,
            // we square them all, then add, then take a square root.  For now, we'll
            // use power = 2.

            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = values[team.TeamId][pair.Key][(int)ValueType.NoProg] * (needfrac * TotalNeed(team, pickNumber, pair.Key) + valfrac);
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }

        public double PerceivedEffectiveValue(TeamRecord team, int pickNumber, Dictionary<int, Dictionary<int, double>> awarenessAdjust)
        {
            double valfrac = 0.25 + 0.075 * Math.Floor((double)(pickNumber / 32));
            double needfrac = 1 - valfrac;

            // Power essentially determines how we add together effective values
            // at multiple positions.  If power = 1, we just add them.  If power = 2,
            // we square them all, then add, then take a square root.  For now, we'll
            // use power = 2.

            double valueSubtotal = 0;

            foreach (KeyValuePair<int, double> pair in awarenessAdjust[Player.PositionId])
            {
                if (pair.Key > 20) { continue; }

                double tempValue = values[team.TeamId][pair.Key][(int)ValueType.Perceived] * (needfrac * TotalNeed(team, pickNumber, pair.Key) + valfrac);
                valueSubtotal += Math.Pow(tempValue, power);
            }

            return Math.Pow(valueSubtotal, 1 / power);
        }
          
        public PlayerRecord Player
        {
            get
            {
                return player;
            }
        }

        public void SetPlayerRecord(PlayerRecord pr)
        {
            player = pr;
        }

        public override string ToString()
        {
            return Player.ToString();
        }

        public enum NeedType
        {
            Starter = 0,
            Backup,
            Successor
        }

        public enum ValueType
        {
            NoProg = 0,
            Perceived,
            WithProg
        }

        public enum RatingType
        {
            Final = 0,
            Combine,
            Initial,
            Actual
        }
    }

    
}
