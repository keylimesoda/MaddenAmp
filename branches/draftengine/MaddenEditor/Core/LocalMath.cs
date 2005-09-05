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

namespace MaddenEditor.Core
{
    public class LocalMath
    {
        public const double ValueScale = 0.28*1.3;

        public LocalMath()
        {

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

            return Math.Exp(-77.4609 + 30.4199*temp - 4.32888*Math.Pow(temp, 2) + 0.280661*Math.Pow(temp, 3) - 0.00682204 * Math.Pow(temp, 4));
        }

        public int theta(int x)
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
