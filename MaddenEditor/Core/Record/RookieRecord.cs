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
    public class RookieRecord : TableRecordModel
    {
        public const string DRAFTED_TEAM = "DRTM";
        public const string PLAYER_ID = "PGID";
        public const string DRAFT_PICK_NUMBER = "DPNM";

        private PlayerRecord player;

        public Dictionary<int, int> EstimatedPickNumber = new Dictionary<int,int>();
        public Dictionary<int, string> EstimatedRound = new Dictionary<int,string>();

        public Dictionary<int, double> PreCombineScoutedHours = new Dictionary<int,double>();
        public Dictionary<int, double> PostCombineScoutedHours = new Dictionary<int,double>();

        public Dictionary<int, double> CombineNumbers = new Dictionary<int, double>();
        public Dictionary<int, string> CombineWords = new Dictionary<int, string>();

        public Dictionary<int, double> ActualRatings = new Dictionary<int,double>();
        public double ActualValue;

        // Has structure ratings[TeamId][RatingType][Attribute]
        public Dictionary<int, Dictionary<int, Dictionary<int, double>>> ratings = new Dictionary<int, Dictionary<int, Dictionary<int, double>>>();

        // Has structure values[TeamId][ValueType]
        public Dictionary<int, Dictionary<int, double>> values = new Dictionary<int,Dictionary<int,double>>();

        // Has structure needs[TeamId][NeedType]
        public Dictionary<int, Dictionary<int, double>> needs = new Dictionary<int,Dictionary<int,double>>();

        public RookieRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{
            for (int i=0; i < 32; i++) {
                ratings.Add(i, new Dictionary<int,Dictionary<int,double>>());
                values.Add(i, new Dictionary<int, double>());
                needs.Add(i, new Dictionary<int, double>());

                for (int j=0; j < 3; j++) {
                    ratings[i].Add(j, new Dictionary<int,double>());
                }
            }
		}

        public void CalculateNeeds(TeamRecord team, List<PlayerRecord> depthChart, Dictionary<int, Position> positionData)
        {
            double starterNeed = 0;
            double backupNeed = 0;
            double successorNeed = 0;
            LocalMath math = new LocalMath();

            int numStarters = positionData[Player.PositionId].Starters(team.DefensiveSystem);
            int startTemp;

            if (Player.PositionId == 0 && team.TeamId == 20)
            {
                int trash;
            }

            /** First calculate the starter need **/

            if (depthChart.Count < numStarters)
            {
                /*
                needs[team.TeamId][(int)NeedType.Starter] = 1;
                 */

                // If there's no current starter, they can always get a stop-gap guy
                // in free agency, who we'll assume has OVR = 75, INJ = 75, YRP >= 5,
                // so their effective overall is just 75.  This should at least prevent
                // the totally boneheaded picks.
                int stopgapEffectiveOVR = 78;
                double sgValue = LocalMath.ValueScale * positionData[Player.PositionId].Value(team.DefensiveSystem) * math.valcurve(stopgapEffectiveOVR);
                needs[team.TeamId][(int)NeedType.Starter] = math.need(values[team.TeamId][(int)ValueType.WithProg], sgValue);
            }
            else
            {
                needs[team.TeamId][(int)NeedType.Starter] = math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters - 1].Value);
            }

            /** Now calculate backup need. **/

            if (numStarters == 1 && (Player.PositionId != (int)MaddenPositions.TE && Player.PositionId != (int)MaddenPositions.QB && Player.PositionId != (int)MaddenPositions.HB))
            {
                if (depthChart.Count <= 1)
                {
                    backupNeed = 1;
                }
                else
                {
                    backupNeed = math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters].Value);
                }
            } else {
                if (Player.PositionId != (int)MaddenPositions.CB && Player.PositionId != (int)MaddenPositions.WR)
                {
                    if (depthChart.Count <= numStarters)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters].Value);
                    }

                    if (depthChart.Count <= numStarters + 1)
                    {
                        backupNeed += 0.25;
                    }
                    else
                    {
                        backupNeed = 0.25 * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters+1].Value);
                    }
                }
                else
                {
                    if (depthChart.Count <= 2)
                    {
                        backupNeed = 0.75;
                    }
                    else
                    {
                        backupNeed = 0.75 * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters].Value);
                    }

                    if (depthChart.Count <= 3)
                    {
                        backupNeed += 0.35;
                    }
                    else
                    {
                        backupNeed += 0.35 * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters+1].Value);
                    }

                    if (depthChart.Count <= 4)
                    {
                        backupNeed += 0.15;
                    }
                    else
                    {
                        backupNeed += 0.15 * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[numStarters+2].Value);
                    }
                }
            }

            double injuryFactor = 0;

            for (int i = 0; i < Math.Min(numStarters, depthChart.Count); i++)
            {
                injuryFactor += (1 - math.injury(depthChart[i].Injury, positionData[depthChart[i].PositionId].DurabilityNeed) / 10) / Math.Min(numStarters, depthChart.Count);
            }

            double backupTemp;

            // At these positions, backups are useful even if the starter isn't likely
            // to be injured
            if (Player.PositionId == (int)MaddenPositions.HB || Player.PositionId == (int)MaddenPositions.WR || Player.PositionId == (int)MaddenPositions.DT || Player.PositionId == (int)MaddenPositions.CB)
            {
                backupTemp = backupNeed * (0.75 * injuryFactor + 0.25);
            }
            else
            {
                backupTemp = backupNeed * injuryFactor;
            }

            if (backupTemp > 1.25) { backupTemp = 1.25; }

            needs[team.TeamId][(int)NeedType.Backup] = backupTemp * positionData[Player.PositionId].BackupNeed;

            /** Now calculate need for a successor. **/

            int oldest = 0;

            if (depthChart.Count >= numStarters)
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

                double factor = 1 - 0.2 * (positionData[Player.PositionId].RetirementAge - oldest + (1 / 2) * (team.CON - 5));

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
                        if (positionData[Player.PositionId].RetirementAge - depthChart[i].Age + (1/2)*(team.CON - 5) >= 5) {
                            possibleSuccessor = i;
                            break;
                        }
                    }

                    if (possibleSuccessor == -1) 
                    {
                        successorNeed = factor * positionData[Player.PositionId].SuccessorNeed;
                    } 
                    else 
                    {
                        if (values[team.TeamId][(int)ValueType.WithProg] > depthChart[possibleSuccessor].Value)
                        {
                            successorNeed = factor * positionData[Player.PositionId].SuccessorNeed * math.need(values[team.TeamId][(int)ValueType.WithProg], depthChart[possibleSuccessor].Value);
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

            needs[team.TeamId][(int)NeedType.Successor] = successorNeed;
        }

        public double PrimarySkill(int TeamId, int type)
        {

            switch (Player.PositionId)
            {
                case (int)MaddenPositions.QB:
                    return ratings[TeamId][type][(int)Attribute.THA];
                case (int)MaddenPositions.HB:
                    return ratings[TeamId][type][(int)Attribute.BTK];
                case (int)MaddenPositions.FB:
                    return (7.0 / 8.0) * ratings[TeamId][type][(int)Attribute.RBK] + (1 / 8.0) * ratings[TeamId][type][(int)Attribute.PBK];
                case (int)MaddenPositions.WR:
                    return ratings[TeamId][type][(int)Attribute.CTH];
                case (int)MaddenPositions.TE:
                    return (5.0 / 6.0) * ratings[TeamId][type][(int)Attribute.CTH] + (1 / 6.0) * ratings[TeamId][type][(int)Attribute.BTK];
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                case (int)MaddenPositions.C:
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    return ratings[TeamId][type][(int)Attribute.PBK];
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.DT:
                case (int)MaddenPositions.RE:
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.MLB:
                case (int)MaddenPositions.ROLB:
                case (int)MaddenPositions.CB:
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    return ratings[TeamId][type][(int)Attribute.TAK];
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    return ratings[TeamId][type][(int)Attribute.KAC];                    
            }

            return -1;
        }

        public double SecondarySkill(int TeamId, int type)
        {

            switch (Player.PositionId)
            {
                case (int)MaddenPositions.QB:
                    return ratings[TeamId][type][(int)Attribute.THP];
                case (int)MaddenPositions.HB:
                    return (5.0 / 8.0) * ratings[TeamId][type][(int)Attribute.CAR] + (3.0 / 8.0) * ratings[TeamId][type][(int)Attribute.CTH];
                case (int)MaddenPositions.FB:
                    return ratings[TeamId][type][(int)Attribute.BTK];
                case (int)MaddenPositions.WR:
                    return ratings[TeamId][type][(int)Attribute.BTK];
                case (int)MaddenPositions.TE:
                    return (5.0 / 6.0) * ratings[TeamId][type][(int)Attribute.RBK] + (1.0 / 6.0) * ratings[TeamId][type][(int)Attribute.PBK];
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                case (int)MaddenPositions.C:
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    return ratings[TeamId][type][(int)Attribute.RBK];
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.DT:
                case (int)MaddenPositions.RE:
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.MLB:
                case (int)MaddenPositions.ROLB:
                case (int)MaddenPositions.CB:
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    return ratings[TeamId][type][(int)Attribute.CTH];
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    return ratings[TeamId][type][(int)Attribute.KPR];
            }

            return -1;
        }

        public void CalculateOverall(int type)
        {
            for (int i = 0; i < 32; i++) {
                double tempOverall = 0;

                switch (Player.PositionId)
                {
                    case (int)MaddenPositions.QB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.THP] - 50) / 10) * 4.9;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.THA] - 50) / 10) * 5.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.BTK] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 4.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 2.0;
                        tempOverall += 28;
                        break;
                    case (int)MaddenPositions.HB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.PBK] - 50) / 10) * 0.33;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.BTK] - 50) / 10) * 3.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CAR] - 50) / 10) * 2.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 2.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 2.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 0.6;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 1.4;
                        tempOverall += 27;
                        break;
                    case (int)MaddenPositions.FB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.PBK] - 50) / 10) * 1.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.RBK] - 50) / 10) * 7.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.BTK] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CAR] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 2.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 5.2;
                        tempOverall += 39;
                        break;
                    case (int)MaddenPositions.WR:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.BTK] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 2.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 2.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 2.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 2.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 4.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.JMP] - 50) / 10) * 1.4;
                        tempOverall += 26;
                        break;
                    case (int)MaddenPositions.TE:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 2.65;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 2.65;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 2.65;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.25;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.25;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 5.4;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.BTK] - 50) / 10) * 1.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.PBK] - 50) / 10) * 1.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.RBK] - 50) / 10) * 5.4;
                        tempOverall += 35;
                        break;
                    case (int)MaddenPositions.LT:
                    case (int)MaddenPositions.RT:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 3.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 3.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.PBK] - 50) / 10) * 4.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.RBK] - 50) / 10) * 3.75;
                        tempOverall += 26;
                        break;
                    case (int)MaddenPositions.LG:
                    case (int)MaddenPositions.RG:
                    case (int)MaddenPositions.C:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 1.7;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 3.25;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 3.25;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 0.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.7;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.PBK] - 50) / 10) * 3.25;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.RBK] - 50) / 10) * 4.8;
                        tempOverall += 28;
                        break;
                    case (int)MaddenPositions.LE:
                    case (int)MaddenPositions.RE:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 3.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 1.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 3.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 5.5;
                        tempOverall += 30;
                        break;
                    case (int)MaddenPositions.DT:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 1.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 5.5;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 3.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 2.8;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 4.55;
                        tempOverall += 29;
                        break;
                    case (int)MaddenPositions.LOLB:
                    case (int)MaddenPositions.ROLB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 2.4;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 3.6;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 2.4;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 1.3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 4.8;
                        tempOverall += 29;
                        break;
                    case (int)MaddenPositions.MLB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 0.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 3.4;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 5.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.65;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 5.2;
                        tempOverall += 27;
                        break;
                    case (int)MaddenPositions.CB:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.85;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 0.9;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 3.85;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.55;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 2.35;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 3;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.JMP] - 50) / 10) * 1.55;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 1.55;
                        tempOverall += 28;
                        break;
                    case (int)MaddenPositions.FS:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 0.9;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 4.85;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.5;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 2.5;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 3.0;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.JMP] - 50) / 10) * 1.5;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 2.5;
                        tempOverall += 30;
                        break;
                    case (int)MaddenPositions.SS:
                        tempOverall = (((double)ratings[i][type][(int)Attribute.SPD] - 50) / 10) * 3.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.STR] - 50) / 10) * 1.7;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AWR] - 50) / 10) * 4.75;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.AGI] - 50) / 10) * 1.7;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.ACC] - 50) / 10) * 1.7;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.CTH] - 50) / 10) * 3.2;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.JMP] - 50) / 10) * 0.9;
                        tempOverall += (((double)ratings[i][type][(int)Attribute.TAK] - 50) / 10) * 3.2;
                        tempOverall += 30;
                        break;
                    case (int)MaddenPositions.P:
                        tempOverall = (double)(-183 + 0.218 * ratings[i][type][(int)Attribute.AWR] + 1.5 * ratings[i][type][(int)Attribute.KPR] + 1.33 * ratings[i][type][(int)Attribute.KAC]);
                        break;
                    case (int)MaddenPositions.K:
                        tempOverall = (double)(-177 + 0.218 * ratings[i][type][(int)Attribute.AWR] + 1.28 * ratings[i][type][(int)Attribute.KPR] + 1.47 * ratings[i][type][(int)Attribute.KAC]);
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

                ratings[i][type][(int)Attribute.OVR] = tempOverall;
            }
        }

        public double TotalNeed(TeamRecord team, int pickNumber) {
            double ProbabilityOfStarting = Math.Sqrt(needs[team.TeamId][(int)NeedType.Starter]);
            double toReturn =  (needs[team.TeamId][(int)NeedType.Starter] +
                Math.Tanh((double)pickNumber / 45) * (1 - ProbabilityOfStarting) * needs[team.TeamId][(int)NeedType.Backup] +
                Math.Tanh(((double)pickNumber + 5) / (5 * team.CON)) * (1 - ProbabilityOfStarting) * needs[team.TeamId][(int)NeedType.Successor]);
            return toReturn;
        }

        public double EffectiveValue(TeamRecord team, int pickNumber) {
            double valfrac = 0.25 + 0.075 * Math.Floor((double)(pickNumber / 32));
            double needfrac = 1 - valfrac;

            double toReturn =  values[team.TeamId][(int)ValueType.NoProg] * (needfrac * TotalNeed(team, pickNumber) + valfrac);
            return toReturn;
        }

        public double PerceivedEffectiveValue(TeamRecord team, int pickNumber)
        {
            double valfrac = 0.2 + 0.075 * Math.Floor((double)(pickNumber / 32));
            double needfrac = 1 - valfrac;

            double toReturn = values[team.TeamId][(int)ValueType.Perceived] * (needfrac * TotalNeed(team, pickNumber) + valfrac);
            return toReturn;
        }
        
        public int PlayerId
        {
            get
            {
                return GetIntField(PLAYER_ID);
            }
            set
            {
                SetField(PLAYER_ID, value);
            }
        }

        public int DraftedTeam
        {
            get
            {
                return GetIntField(DRAFTED_TEAM);
            }
            set
            {
                SetField(DRAFTED_TEAM, value);
            }
        }

        public int DraftPickNumber
        {
            get
            {
                return GetIntField(DRAFT_PICK_NUMBER);
            }
            set
            {
                SetField(DRAFT_PICK_NUMBER, value);
            }
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

        public enum Attribute
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
            TGH
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
}
