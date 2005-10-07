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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
    public class LocalMath
    {
        //public const double ValueScale = 0.28*1.3;
        public const double ValueScale = 1;

        private MaddenFileVersion mfv;

        public LocalMath(MaddenFileVersion version)
        {
            mfv = version;
        }

        public double pointboost(PlayerRecord player, double con, double retireAge)
        {
            double conReverse = 5-con;
            return theta(5-player.YearsPro)*(1.0 - 0.2*(double)player.YearsPro)*(0.5*conReverse + 0.1875*Math.Pow(conReverse,2.0)) -
                theta(player.Age + 5.0 - retireAge)*0.2*Math.Min((double)player.Age + 5.0 - retireAge,5.0)*(0.5*conReverse + 0.1875*Math.Pow(conReverse,2.0));
        }

        public double bellcurve(double cv, double sigma, Random rand)
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

        public string SkillToGrade(double skill)
        {
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


        public double valcurve(double EOVR) {
            double temp = EOVR / 10;

            if (mfv == MaddenFileVersion.Ver2006)
            {
                return 0.8*Math.Exp(-132.4476238 + 9.8668569 * EOVR - 0.310497039 * Math.Pow(EOVR, 2) +
                    0.00499479989 * Math.Pow(EOVR, 3) - 0.0000417310844 * Math.Pow(EOVR, 4) + 
                    1.668293269*Math.Pow(10, -7) * Math.Pow(EOVR, 5) -
                    2.34097*Math.Pow(10, -10) * Math.Pow(EOVR, 6));
            }
            else
            {
                return 0.364*Math.Exp(-77.4609 + 30.4199 * temp - 4.32888 * Math.Pow(temp, 2) + 0.280661 * Math.Pow(temp, 3) - 0.00682204 * Math.Pow(temp, 4));
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

        public double injury(double INJ, double durabilityFactor) {
            return durabilityFactor*(0.318376 * (INJ - 75) - 0.004444 * Math.Pow(INJ - 75, 2) + 0.000066476 * Math.Pow(INJ - 75, 3));
        }
    }
}
