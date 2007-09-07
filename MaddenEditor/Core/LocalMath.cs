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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
	public class LocalMath
	{
		//public const double ValueScale = 0.28*1.3;
		public const double ValueScale = 1;
        Random rand;

        bool extraGaussian;
        double deviate;
        double lastSigma;

		public MaddenFileVersion mfv;

		public LocalMath(MaddenFileVersion version)
		{
			mfv = version;
            rand = new Random();
            extraGaussian = false;
            lastSigma = 0;
		}

        public bool InPositionGroup(int positionId, int groupId)
        {
            switch (groupId)
            {
                case (int)MaddenPositionGroups.DB:
                    if (positionId >= (int)MaddenPositions.CB &&
                        positionId <= (int)MaddenPositions.SS)
                        return true;
                    break;
                case (int)MaddenPositionGroups.DE:
                    if (positionId == (int)MaddenPositions.LE ||
                        positionId == (int)MaddenPositions.RE)
                        return true;
                    break;
                case (int)MaddenPositionGroups.DL:
                    if (positionId >= (int)MaddenPositions.LE &&
                        positionId <= (int)MaddenPositions.DT)
                        return true;
                    break;
                case (int)MaddenPositionGroups.LB:
                    if (positionId >= (int)MaddenPositions.LOLB &&
                        positionId <= (int)MaddenPositions.ROLB)
                        return true;
                    break;
                case (int)MaddenPositionGroups.OG:
                    if (positionId == (int)MaddenPositions.LG ||
                        positionId == (int)MaddenPositions.RG)
                        return true;
                    break;
                case (int)MaddenPositionGroups.OL:
                    if (positionId >= (int)MaddenPositions.LT &&
                        positionId <= (int)MaddenPositions.RT)
                        return true;
                    break;
                case (int)MaddenPositionGroups.OLB:
                    if (positionId == (int)MaddenPositions.LOLB ||
                        positionId == (int)MaddenPositions.ROLB)
                        return true;
                    break;
                case (int)MaddenPositionGroups.OT:
                    if (positionId == (int)MaddenPositions.LT ||
                        positionId == (int)MaddenPositions.RT)
                        return true;
                    break;
                case (int)MaddenPositionGroups.S:
                    if (positionId == (int)MaddenPositions.FS ||
                        positionId == (int)MaddenPositions.SS)
                        return true;
                    break;
            }

            return false;
        }

		public double HeightWeightAdjust(PlayerRecord player, int positionId)
		{
			double adjustment = 0;

			switch (positionId)
			{
				case (int)MaddenPositions.HB:
					adjustment += (player.Weight - 55) * 0.07;
					break;
				case (int)MaddenPositions.FB:
					adjustment += (player.Weight - 90) * 0.04;
					break;
				case (int)MaddenPositions.WR:
					adjustment += (player.Weight - 50) * 0.06;
					adjustment += (player.Height - 74) * 0.8;
					break;
				case (int)MaddenPositions.TE:
					adjustment += (player.Height - 76);
					adjustment += (player.Weight - 100) * 0.1;
					break;
				case (int)MaddenPositions.LT:
				case (int)MaddenPositions.LG:
				case (int)MaddenPositions.RT:
					adjustment += (player.Weight - 160) * 0.1;
					break;
				case (int)MaddenPositions.C:
					adjustment += (player.Weight - 140) * 0.1;
					break;
				case (int)MaddenPositions.RG:
					adjustment += (player.Weight - 145) * 0.1;
					break;
				case (int)MaddenPositions.LE:
					adjustment += (player.Weight - 125) * 0.1;
					break;
				case (int)MaddenPositions.RE:
					adjustment += (player.Weight - 115) * 0.1;
					break;
				case (int)MaddenPositions.DT:
					adjustment += (player.Weight - 150) * 0.15;
					break;
				case (int)MaddenPositions.LOLB:
					adjustment += (player.Height - 74) * 0.5;
					adjustment += (player.Weight - 80) * 0.09;
					break;
				case (int)MaddenPositions.MLB:
					adjustment += (player.Weight - 80) * 0.08;
					break;
				case (int)MaddenPositions.ROLB:
					adjustment += (player.Weight - 80) * 0.06;
					adjustment += (player.Height - 74) * 0.3;
					break;
				case (int)MaddenPositions.CB:
					adjustment += (player.Height - 71) * 0.9;
					adjustment += (player.Weight - 30) * 0.04;
					break;
				case (int)MaddenPositions.FS:
					adjustment += (player.Weight - 50) * 0.05;
					adjustment += (player.Height - 72) * 0.7;
					break;
				case (int)MaddenPositions.SS:
					adjustment += (player.Weight - 55) * 0.06;
					adjustment += (player.Height - 72) * 0.7;
					break;
			}

			return adjustment;
		}

		public double pointboost(PlayerRecord player, double con, double retireAge)
		{
			double conReverse = 5 - con;
			return theta(5 - player.YearsPro) * (1.0 - 0.2 * (double)player.YearsPro) * (0.5 * conReverse + 0.1875 * Math.Pow(conReverse, 2.0)) -
				theta(player.Age + 5.0 - retireAge) * 0.2 * Math.Min((double)player.Age + 5.0 - retireAge, 5.0) * (0.5 * conReverse + 0.1875 * Math.Pow(conReverse, 2.0));
		}

        public double bellcurve(double cv, double sigma)
        {
            double v1;
            double v2;
            double rsq;

            if (extraGaussian && sigma == lastSigma)
            {
                extraGaussian = false;
                return cv + deviate;
            }
            else
            {
                lastSigma = sigma;

                do
                {
                    v1 = 2.0 * rand.NextDouble() - 1.0;
                    v2 = 2.0 * rand.NextDouble() - 1.0;
                    rsq = v1 * v1 + v2 * v2;
                } while (rsq >= 1 || rsq == 0);

                double fac = Math.Sqrt(-2.0 * Math.Log(rsq) / rsq);
                deviate = sigma * v1 * fac;
                extraGaussian = true;
                return cv + sigma * v2 * fac;
            }
        }

/*		public double bellcurve(double cv, double sigma, Random rand)
		{
			int numSteps = 100;
			double stepSize = sigma / Math.Sqrt(numSteps);

			double walked = 0;

			for (int i = 0; i < numSteps; i++)
			{
				double whichway = rand.NextDouble();
				if (whichway < 0.5)
				{
					walked -= stepSize;
				}
				else
				{
					walked += stepSize;
				}
			}

			return cv + walked;
		}
*/
		public string SkillToGrade(double skill)
		{
			if (skill == -1)
			{
				return "";
			}
			if (skill > 95)
			{
				return "A+";
			}
			else if (skill > 90)
			{
				return "A";
			}
			else if (skill > 85)
			{
				return "A-";
			}
			else if (skill > 80)
			{
				return "B+";
			}
			else if (skill > 75)
			{
				return "B";
			}
			else if (skill > 70)
			{
				return "B-";
			}
			else if (skill > 65)
			{
				return "C+";
			}
			else if (skill > 60)
			{
				return "C";
			}
			else if (skill > 55)
			{
				return "C-";
			}
			else if (skill > 50)
			{
				return "D+";
			}
			else if (skill > 45)
			{
				return "D";
			}
			else if (skill > 40)
			{
				return "D-";
			}
			else
			{
				return "F";
			}
		}

		public double need(double rookieValue, double currentValue)
		{
			double startTemp = Math.Tanh((rookieValue - currentValue) / currentValue);

			if (startTemp < 0)
			{
				// This is because the minimum value of starterNeed in the above
				// formula is Tanh(-1), but we want the minimum to be -1.
				startTemp *= 1 / Math.Tanh(1);
			}

			startTemp = (1 + startTemp) / 2;

			return Math.Pow(startTemp, 2);
		}


		public double valcurve(double EOVR)
		{
			double temp = EOVR / 10;

            
            if (mfv == MaddenFileVersion.Ver2006)
			{
				return 0.8 * Math.Exp(-132.4476238 + 9.8668569 * EOVR - 0.310497039 * Math.Pow(EOVR, 2) +
					0.00499479989 * Math.Pow(EOVR, 3) - 0.0000417310844 * Math.Pow(EOVR, 4) +
					1.668293269 * Math.Pow(10, -7) * Math.Pow(EOVR, 5) -
					2.34097 * Math.Pow(10, -10) * Math.Pow(EOVR, 6));
			}
            else
                if (mfv == MaddenFileVersion.Ver2007)
             //   if (mfv == MaddenFileVersion.Ver2007 | mfv == MaddenFileVersion.Ver2008)
            {
                return 0.8 * Math.Exp(-132.4476238 + 9.8668569 * EOVR - 0.310497039 * Math.Pow(EOVR, 2) +
                     0.00499479989 * Math.Pow(EOVR, 3) - 0.0000417310844 * Math.Pow(EOVR, 4) +
                     1.668293269 * Math.Pow(10, -7) * Math.Pow(EOVR, 5) -
                     2.34097 * Math.Pow(10, -10) * Math.Pow(EOVR, 6));
            }
            else
                if (mfv == MaddenFileVersion.Ver2008)
                {

                    return 0.12 * Math.Exp(-132.4476238 + 9.8668569 * EOVR - 0.310497039 * Math.Pow(EOVR, 2) +
                    0.00499479989 * Math.Pow(EOVR, 3) - 0.0000417310844 * Math.Pow(EOVR, 4) +
                    1.668293269 * Math.Pow(10, -7) * Math.Pow(EOVR, 5) -
                    2.34097 * Math.Pow(10, -10) * Math.Pow(EOVR, 6));


                }
			{
				return 0.364 * Math.Exp(-77.4609 + 30.4199 * temp - 4.32888 * Math.Pow(temp, 2) + 0.280661 * Math.Pow(temp, 3) - 0.00682204 * Math.Pow(temp, 4));
			}
		}

		public double theta(double x)
		{
			if (x > 0)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		public double injury(double INJ, double durabilityFactor)
		{
			return durabilityFactor * (0.318376 * (INJ - 75) - 0.004444 * Math.Pow(INJ - 75, 2) + 0.000066476 * Math.Pow(INJ - 75, 3));
		}
	}
}
