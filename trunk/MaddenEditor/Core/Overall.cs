/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
    public class ovrdef
    {
        #region Members

        public Dictionary<int, double> Ratings;
        public double totalweight = 0;
        // entries as found in table PORC in dbtemplates or streameddata in 2019
        public double RateHigh = 0;
        public double RateLow = 35;
        public double PACW = 0;
        public double PAGW = 0;
        public double PAWW = 0;
        public double PBKW = 0;
        public double PBSW = 0;
        public double PBTW = 0;
        public double PCAW = 0;
        public double PCIW = 0;
        public double PCNW = 0;
        public double PCRW = 0;
        public double PDRW = 0;
        public double PHTW = 0;
        public double PIJW = 0;
        public double PINW = 0;
        public double PJUW = 0;
        public double PKAW = 0;
        public double PKPW = 0;
        public double PKRW = 0;             //  kick return not used in any OVR for normal positions
        public double PLBW = 0;
        public double PLEW = 0;
        public double PLFW = 0;
        public double PLIW = 0;
        public double PLJW = 0;
        public double PLKW = 0;
        public double PLPW = 0;
        public double PLRW = 0;
        public double PLSW = 0;
        public double PLTW = 0;
        public int PLTY = 0;                // Player type
        public double PLUW = 0;
        public double PLWW = 0;
        public double PMCW = 0;
        public double PMRW = 0;
        public double PPBW = 0;
        public double PPEW = 0;
        public double PPMW = 0;
        public int PPOS = 0;
        public double PPRW = 0;
        public double PPWA = 0;
        public double PPWW = 0;
        public double PRBW = 0;
        public double PRDH = 0;
        public double PRDL = 0;
        public double PRLW = 0;
        public double PRRW = 0;
        public double PRSW = 0;
        public double PSAW = 0;
        public double PSEW = 0;
        public double PSHW = 0;
        public double PSTW = 0;
        public double PTAW = 0;
        public double PTCW = 0;
        public double PTDW = 0;
        public double PTMW = 0;
        public double PTOW = 0;        
        public double PTPW = 0;
        public double PTRW = 0;
        public double PTSW = 0;
        public double PUPW = 0;
        public double PYSW = 0;
        public double PZCW = 0;
        public double SRRW = 0;
        #endregion

        public ovrdef()
        {
            Ratings = new Dictionary<int, double>();
            RateHigh = 0;
            RateLow = 35;
            PACW = 0;
            PAGW = 0;
            PAWW = 0;
            PBTW = 0;
            PCAW = 0;
            PCRW = 0;
            PINW = 0;
            PJUW = 0;
            PKAW = 0;
            PKPW = 0;
            PKRW = 0;
            PPBW = 0;
            PRBW = 0;
            PSEW = 0;
            PSTW = 0;
            PTAW = 0;
            PTCW = 0;            
            PTPW = 0;
           
            PPOS = 0;
            totalweight = 0;
            SetRatings();
        }

        public ovrdef(int pos, int type)
        {
            
            Ratings = new Dictionary<int, double>();
            this.PPOS = pos;
            this.PLTY = type;

            
            if (pos == 0)
            #region QB
            {
                if (type == 0)
                {
                    this.RateLow = 43;
                    this.RateHigh = 96;
                    this.PAWW = 16;
                    this.PPWA = 3;
                    this.PTDW = 12;
                    this.PTMW = 18;
                    this.PTPW = 25;
                    this.PTRW = 3;
                    this.PTSW = 18;
                    this.PUPW = 5;

                }
                else if (type == 1)
                {
                    this.RateLow = 43;
                    this.RateHigh = 95;
                    this.PAWW = 12;
                    this.PBKW = 3;
                    this.PPWA = 2;
                    this.PTDW = 18;
                    this.PTMW = 22;
                    this.PTPW = 33;
                    this.PUPW = 10;

                }
                else if (type == 2)
                {
                    this.RateLow = 43;
                    this.RateHigh = 96;
                    this.PACW = 1;
                    this.PAGW = 2;
                    this.PAWW = 14;
                    this.PPWA = 11;
                    this.PTDW = 3;
                    this.PSEW = 2;
                    this.PTMW = 18;
                    this.PTPW = 15;
                    this.PTRW = 5;
                    this.PTSW = 24;
                    this.PUPW = 5;
                }
                else if (type == 3)
                {
                    this.RateLow = 43;
                    this.RateHigh = 95;
                    this.PACW = 3;
                    this.PAGW = 2;
                    this.PAWW = 7;
                    this.PBKW = 10;
                    this.PCRW = 3;
                    this.PSEW = 6;
                    this.PTMW = 14;
                    this.PTPW = 23;
                    this.PTRW = 10;
                    this.PTSW = 15;
                    this.PUPW = 7;
                }
            }
            #endregion

            else if (pos == 1)
            #region HB
            {
                if (type == 4)
                {
                    // power
                    this.RateLow = 50;
                    this.RateHigh = 94;
                    this.PACW = 9;
                    this.PAGW = 5;
                    this.PAWW = 8;
                    this.PBTW = 12;
                    this.PCRW = 13;
                    this.PLBW = 10;
                    this.PLSW = 11;
                    this.PLTW = 13;
                    this.PSEW = 9;
                    this.PSTW = 10;
                }
                else if (type == 5)
                {
                    // elusive
                    this.RateLow = 50;
                    this.RateHigh = 94;
                    this.PACW = 13;
                    this.PAGW = 11;
                    this.PAWW = 8;
                    this.PBTW = 6;
                    this.PCRW = 11;
                    this.PLBW = 10;
                    this.PLEW = 10;
                    this.PLJW = 9;
                    this.PLPW = 9;
                    this.PSEW = 13;
                }
                else if (type == 6)
                {
                    //receiving
                    this.RateLow = 52;
                    this.RateHigh = 89;
                    this.PACW = 12;
                    this.PAGW = 8;
                    this.PAWW = 9;                    
                    this.PCAW = 14;
                    this.PCIW = 8;
                    this.PCRW = 7;
                    this.PLBW = 5;
                    this.PLEW = 4;
                    this.PLJW = 5;
                    this.PLPW = 5;                    
                    this.PSEW = 12;
                    this.SRRW = 10;
                }

            }
            #endregion
                            
            else if (pos == 2)
            #region FB
            {
                if (type == 7)
                {   
                    //blocking
                    this.RateLow = 33;
                    this.RateHigh = 82;
                    this.PACW = 2;
                    this.PAGW = 2;
                    this.PAWW = 10;
                    this.PBTW = 6;
                    this.PCAW = 2;
                    this.PCIW = 2;
                    this.PCRW = 5;
                    this.PLIW = 20;
                    this.PLKW = 7;
                    this.PLRW = 5;
                    this.PLTW = 7;
                    this.PLWW = 3;
                    this.PPBW = 4;
                    this.PPWW = 1;
                    this.PRBW = 10;
                    this.PSEW = 1;
                    this.PSTW = 9;
                    this.PYSW = 1;
                    this.SRRW = 5;
                }
                else if (type == 8)
                {
                    //utility
                    this.RateLow = 33;
                    this.RateHigh = 85;
                    this.PACW = 5;
                    this.PAGW = 5;
                    this.PAWW = 10;
                    this.PBTW = 4;
                    this.PCAW = 8;
                    this.PCIW = 2;
                    this.PCRW = 7;
                    this.PLBW = 6;
                    this.PLEW = 3;
                    this.PLIW = 11;
                    this.PLKW = 5;
                    this.PLSW = 4;
                    this.PLTW = 5;
                    this.PPBW = 2;                    
                    this.PRBW = 3;
                    this.PSEW = 5;
                    this.PSTW = 6;
                    this.SRRW = 9;
                }
            }
            #endregion

            else if (pos == 3)
            #region WR
            {
                if (type == 9)
                {
                    this.RateLow = 39;
                    this.RateHigh = 96;
                    this.PACW = 12;
                    this.PAGW = 8;
                    this.PAWW = 9;
                    this.PCAW = 12;
                    this.PCIW = 8;
                    this.PDRW = 15;
                    this.PJUW = 1;
                    this.PLEW = 2;
                    this.PLJW = 1;
                    this.PLPW = 1;
                    this.PMRW = 6;
                    this.PRLW = 9;
                    this.PSEW = 12;
                    this.PSHW = 4;
                    this.SRRW = 0;
                }
                else if (type == 10)
                {
                    this.RateLow = 39;
                    this.RateHigh = 95;
                    this.PACW = 8;
                    this.PAGW = 5;
                    this.PAWW = 7;
                    this.PBTW = 1;
                    this.PCAW = 14;
                    this.PCIW = 15;
                    this.PCRW = 2;
                    this.PJUW = 3;
                    this.PLEW = 1;
                    this.PLSW = 2;
                    this.PLTW = 1;
                    this.PMRW = 10;
                    this.PRLW = 5;
                    this.PSEW = 8;
                    this.PSHW = 2;
                    this.SRRW = 13;
                }
                else if (type == 11)
                {
                    this.RateLow = 39;
                    this.RateHigh = 96;
                    this.PACW = 9;
                    this.PAGW = 7;                    
                    this.PAWW = 10;
                    this.PBTW = 1;
                    this.PCAW = 13;
                    this.PCIW = 10;
                    this.PJUW = 5;
                    this.PLSW = 1;
                    this.PLTW = 1;
                    this.PMRW = 5;
                    this.PRLW = 10;
                    this.PSEW = 9;
                    this.PSHW = 9;
                    this.SRRW = 9;                    
                }
                else if (type == 12)
                {
                    this.RateLow = 38;
                    this.RateHigh = 97;
                    this.PACW = 12;
                    this.PAGW = 7;
                    this.PAWW = 10;
                    this.PBTW = 1;
                    this.PCAW = 13;
                    this.PCIW = 14;
                    this.PCRW = 1;
                    this.PDRW = 3;
                    this.PLEW = 2;
                    this.PLJW = 1;
                    this.PLPW = 1;
                    this.PMRW = 9;
                    this.PRLW = 0;
                    this.PSEW = 10;
                    this.PSHW = 0;
                    this.SRRW = 16;
                }
            }
            #endregion

            else if (pos == 4)
            #region TE
            {
                if (type == 13)
                {
                    this.RateLow = 28;
                    this.RateHigh = 82;
                    this.PAWW = 9;
                    this.PBTW = 2;
                    this.PCAW = 3;
                    this.PCIW = 3;
                    this.PLIW = 9;
                    this.PLKW = 8;
                    this.PLRW = 9;
                    this.PLSW = 2;
                    this.PLTW = 1;
                    this.PLWW = 9;
                    this.PMRW = 4;
                    this.PPBW = 8;
                    this.PPWW = 6;
                    this.PRBW = 10;
                    this.PSTW = 6;
                    this.PYSW = 6;
                    this.SRRW = 5;                    
                }
                else if (type == 14)
                {
                    this.RateLow = 26;
                    this.RateHigh = 87;
                    this.PACW = 7;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PBTW = 3;
                    this.PCAW = 11;
                    this.PCIW = 8;
                    this.PDRW = 5;
                    this.PJUW = 3;
                    this.PLBW = 2;
                    this.PLEW = 2;
                    this.PLRW = 3;
                    this.PLSW = 2;
                    this.PLTW = 1;
                    this.PLWW = 3;
                    this.PMRW = 9;
                    this.PPBW = 2;
                    this.PPWW = 1;
                    this.PRBW = 3;
                    this.PRLW = 3;
                    this.PSEW = 7;
                    this.PSHW = 4;
                    this.PYSW = 1;
                    this.SRRW = 7;
                }
                else if (type == 15)
                {
                    this.RateLow = 26;
                    this.RateHigh = 85;
                    this.PACW = 4;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PBTW = 2;
                    this.PCAW = 10;
                    this.PCIW = 14;
                    this.PLBW = 1;
                    this.PLIW = 4;
                    this.PLKW = 2;
                    this.PLRW = 4;
                    this.PLSW = 2;
                    this.PLTW = 1;
                    this.PLWW = 4;
                    this.PMRW = 6;
                    this.PPBW = 3;
                    this.PPWW = 2;
                    this.PRBW = 5;
                    this.PRLW = 2;
                    this.PSEW = 3;
                    this.PSHW = 1;
                    this.PSTW = 3;
                    this.PYSW = 2;
                    this.SRRW = 12;
                }
            }
            #endregion

            else if (pos == 5)
            #region LT
            {
                this.RateLow = 30;
                this.RateHigh = 95;

                if (type == 19)
                {
                    this.RateLow = 32;
                    this.PACW = 2;
                    this.PAGW = 2;
                    this.PAWW = 14;
                    this.PLIW = 2;
                    this.PPBW = 8;
                    this.PPWW = 28;
                    this.PSEW = 2;
                    this.PSTW = 14;
                    this.PYSW = 28;

                }
                else if (type == 20)
                {
                    this.PACW = 2;
                    this.PAGW = 2;
                    this.PAWW = 10;
                    this.PLIW = 4;
                    this.PLKW = 2;
                    this.PLRW = 25;
                    this.PPBW = 4;
                    this.PRBW = 10;
                    this.PSEW = 2;
                    this.PSTW = 15;
                    this.PYSW = 24;

                }
                else if (type == 21)
                {
                    this.PACW = 2;
                    this.PAGW = 3;
                    this.PAWW = 10;
                    this.PLIW = 4;
                    this.PLKW = 2;
                    this.PLWW = 24;
                    this.PPBW = 6;
                    this.PPWW = 25;
                    this.PRBW = 8;
                    this.PSEW = 3;
                    this.PSTW = 13;

                }
            }
            #endregion

            else if (pos == 6)
            #region LG
            {
                this.RateLow = 32;
                this.RateHigh = 94;
                this.PACW = 2;
                this.PSEW = 2;

                if (type == 22)
                {                    
                    this.PAGW = 2;
                    this.PAWW = 14;
                    this.PLIW = 5;
                    this.PPBW = 5;
                    this.PPWW = 28;
                    this.PSTW = 14;
                    this.PYSW = 28;

                }
                else if (type == 23)
                {
                    this.PAGW = 2;
                    this.PAWW = 11;
                    this.PLIW = 7;
                    this.PLKW = 8;
                    this.PLRW = 19;
                    this.PPBW = 2;
                    this.PRBW = 10;
                    this.PSTW = 15;
                    this.PYSW = 22;

                }
                else if (type == 24)
                {                   
                    this.PAGW = 3;
                    this.PAWW = 10;
                    this.PLIW = 7;
                    this.PLKW = 8;
                    this.PLWW = 20;
                    this.PPBW = 4;
                    this.PPWW = 23;
                    this.PSTW = 13;

                }
            }
            #endregion

            else if (pos == 7)
            #region C
            {
                this.RateLow = 34;
                this.RateHigh = 94;
                this.PACW = 2;

                if (type == 16)
                {
                    this.PAGW = 2;
                    this.PAWW = 18;
                    this.PLIW = 4;
                    this.PPBW = 5;
                    this.PPWW = 28;                    
                    this.PSTW = 13;
                    this.PYSW = 28;

                }
                else if (type == 17)
                {
                    this.PAGW = 2;
                    this.PAWW = 15;
                    this.PLIW = 6;
                    this.PLKW = 4;
                    this.PLRW = 22;
                    this.PPBW = 2;
                    this.PRBW = 10;
                    this.PSEW = 2;
                    this.PSTW = 14;
                    this.PYSW = 21;

                }
                else if (type == 18)
                {
                    this.PAGW = 3;
                    this.PAWW = 15;
                    this.PLIW = 6;
                    this.PLKW = 4;
                    this.PLWW = 22;
                    this.PPBW = 4;
                    this.PPWW = 21;
                    this.PRBW = 8;
                    this.PSEW = 3;
                    this.PSTW = 12;

                }
            }
            #endregion

            else if (pos == 8)
            #region RT
            {
                this.RateLow = 32;
                this.RateHigh = 94;
                this.PACW = 2;
                this.PSEW = 2;

                if (type == 22)
                {                    
                    this.PAGW = 2;
                    this.PAWW = 14;
                    this.PLIW = 5;
                    this.PPBW = 5;
                    this.PPWW = 28;
                    this.PSTW = 14;
                    this.PYSW = 28;

                }
                else if (type == 23)
                {
                    this.PAGW = 2;
                    this.PAWW = 11;
                    this.PLIW = 7;
                    this.PLKW = 8;
                    this.PLRW = 19;
                    this.PPBW = 2;
                    this.PRBW = 10;
                    this.PSTW = 15;
                    this.PYSW = 22;

                }
                else if (type == 24)
                {                    
                    this.PAGW = 3;
                    this.PAWW = 10;
                    this.PLIW = 7;
                    this.PLKW = 8;
                    this.PLWW = 20;
                    this.PPBW = 4;
                    this.PPWW = 23;
                    this.PSTW = 13;

                }
            }
            #endregion
                            
            else if (pos == 9)
            #region RG
            {
                this.RateLow = 30;
                this.RateHigh = 95;
                this.PACW = 2;

                if (type == 19)
                {
                    this.RateLow = 32;
                    this.PAGW = 2;
                    this.PAWW = 14;
                    this.PLIW = 2;
                    this.PPBW = 8;
                    this.PPWW = 28;
                    this.PSEW = 2;
                    this.PSTW = 14;
                    this.PYSW = 28;
                }
                else if (type == 20)
                {
                    this.PAGW = 2;
                    this.PAWW = 10;
                    this.PLIW = 4;
                    this.PLKW = 2;
                    this.PLRW = 25;
                    this.PPBW = 4;
                    this.PRBW = 10;
                    this.PSEW = 2;
                    this.PSTW = 15;
                    this.PYSW = 24;
                }
                else if (type == 21)
                {
                    this.PAGW = 3;
                    this.PAWW = 10;
                    this.PLIW = 4;
                    this.PLKW = 2;
                    this.PLWW = 24;
                    this.PPBW = 6;
                    this.PPWW = 25;
                    this.PRBW = 8;
                    this.PSEW = 3;
                    this.PSTW = 13;
                }
            }
            #endregion

            else if (pos == 10)
            #region LE
            {
                this.RateLow = 36;
                this.RateHigh = 92;
                this.PHTW = 3;  // hit power

                if (type == 25)
                {
                    //speed rush
                    this.PACW = 14;
                    this.PAGW = 9;
                    this.PAWW = 7;                    
                    this.PLFW = 23;
                    this.PLUW = 7;
                    this.PPRW = 7;
                    this.PSEW = 10;
                    this.PSTW = 5;
                    this.PTCW = 15;
                }
                else if (type == 26)
                {
                    //power rush
                    this.PACW = 10;
                    this.PAGW = 6;
                    this.PAWW = 7;
                    this.PLUW = 7;
                    this.PPMW = 23;
                    this.PPRW = 7;
                    this.PSEW = 7;
                    this.PSTW = 15;
                    this.PTCW = 15;
                }
                else if (type == 27)
                {
                    //run stop
                    this.PACW = 8;
                    this.PAGW = 5;
                    this.PAWW = 9;
                    this.PBSW = 18; //  shed block?
                    this.PLFW = 2;
                    this.PLUW = 9;
                    this.PPMW = 2;
                    this.PPRW = 9;
                    this.PSEW = 5;
                    this.PSTW = 12;
                    this.PTCW = 18;
                }
            }
            #endregion

            else if (pos == 11)
            #region RE
            {
                this.RateLow = 36;
                this.RateHigh = 92;
                this.PHTW = 3;

                if (type == 25)
                {
                    //speed rush
                    this.PACW = 14;
                    this.PAGW = 9;
                    this.PAWW = 7;
                    this.PLFW = 23;
                    this.PLUW = 7;
                    this.PPRW = 7;
                    this.PSEW = 10;
                    this.PSTW = 5;
                    this.PTCW = 15;
                }
                else if (type == 26)
                {
                    //power rush
                    this.PACW = 10;
                    this.PAGW = 6;
                    this.PAWW = 7;
                    this.PLFW = 0;
                    this.PLUW = 7;
                    this.PPMW = 23;
                    this.PPRW = 7;
                    this.PSEW = 7;
                    this.PSTW = 15;
                    this.PTCW = 15;
                }
                else if (type == 27)
                {
                    //run stop
                    this.PACW = 8;
                    this.PAGW = 5;
                    this.PAWW = 9;
                    this.PBSW = 18; //  ???
                    this.PLFW = 2;
                    this.PLUW = 9;
                    this.PPMW = 2;
                    this.PPRW = 9;
                    this.PSEW = 5;
                    this.PSTW = 12;
                    this.PTCW = 18;
                }
            }
            #endregion

            else if (pos == 12)
            #region DT
            {
                this.RateLow = 35;
                this.RateHigh = 93;
                this.PHTW = 3;

                if (type == 28)
                {
                    //speed rush
                    this.PACW = 6;
                    this.PAGW = 3;
                    this.PAWW = 8;
                    this.PBSW = 22;
                    this.PLFW = 0;
                    this.PLUW = 8;
                    this.PPRW = 8;
                    this.PSEW = 4;
                    this.PSTW = 21;
                    this.PTCW = 17;
                }
                else if (type == 29)
                {
                    //power rush
                    this.PACW = 8;
                    this.PAGW = 4;
                    this.PAWW = 8;
                    this.PBSW = 4;
                    this.PLFW = 22;
                    this.PLUW = 7;
                    this.PPMW = 0;
                    this.PPRW = 8;
                    this.PSEW = 6;
                    this.PSTW = 17;
                    this.PTCW = 13;
                }
                else if (type == 30)
                {
                    //run stop
                    this.PACW = 6;
                    this.PAGW = 3;
                    this.PAWW = 9;
                    this.PBSW = 6;
                    this.PLFW = 0;
                    this.PLUW = 6;
                    this.PPMW = 22;
                    this.PPRW = 9;
                    this.PSEW = 6;
                    this.PSTW = 20;
                    this.PTCW = 13;
                }
            }
            #endregion

            else if (pos == 13)
            #region LOLB
            {
                this.RateLow = 37;
                this.RateHigh = 91;

                if (type == 31)
                {
                    //speed rush
                    this.PACW = 13;
                    this.PAGW = 5;
                    this.PAWW = 7;                    
                    this.PLFW = 22;
                    this.PLUW = 10;
                    this.PMCW = 2;
                    this.PPRW = 7;
                    this.PSEW = 8;
                    this.PSTW = 6;
                    this.PTCW = 10;
                    this.PZCW = 6;
                }
                else if (type == 32)
                {
                    //power rush
                    this.PACW = 10;
                    this.PAGW = 2;
                    this.PAWW = 7;
                    this.PHTW = 2;
                    this.PLFW = 22;
                    this.PLUW = 12;
                    this.PMCW = 2;
                    this.PPMW = 22;
                    this.PPRW = 7;
                    this.PSEW = 5;
                    this.PSTW = 8;
                    this.PTCW = 15;
                    this.PZCW = 6;
                }
                else if (type == 33)
                {
                    this.RateLow = 36;
                    this.RateHigh = 92;
                    //run stop
                    this.PACW = 11;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PLUW = 11;
                    this.PMCW = 6;
                    this.PPRW = 9;
                    this.PSEW = 9;
                    this.PSTW = 2;
                    this.PTCW = 14;
                    this.PZCW = 18;
                }
                else if (type == 34)
                {
                    
                    this.PACW = 9;
                    this.PAGW = 2;
                    this.PAWW = 7;
                    this.PBSW = 16;
                    this.PHTW = 3;
                    this.PLUW = 16;
                    this.PMCW = 2;
                    this.PPRW = 7;
                    this.PSEW = 6;
                    this.PSTW = 20;
                    this.PTCW = 16;
                    this.PZCW = 6;
                }
            }
            #endregion

            else if (pos == 14)
            #region MLB
            {
                this.RateLow = 39;
                this.RateHigh = 92;

                if (type == 35)
                {
                    //speed rush
                    this.PACW = 9;
                    this.PAGW = 7;
                    this.PAWW = 9;
                    this.PBSW = 7;
                    this.PLUW = 14;
                    this.PMCW = 5;
                    this.PPRW = 9;
                    this.PSEW = 5;
                    this.PSTW = 8;
                    this.PTCW = 17;
                    this.PZCW = 10;
                }
                else if (type == 36)
                {
                    //power rush
                    this.PACW = 10;
                    this.PAGW = 8;
                    this.PAWW = 7;
                    this.PLUW = 12;
                    this.PMCW = 10;
                    this.PPRW = 7;
                    this.PSEW = 7;
                    this.PSTW = 10;
                    this.PTCW = 15;
                    this.PZCW = 20;
                }
                else if (type == 37)
                {
                    this.RateLow = 40;
                    
                    //run stop
                    this.PACW = 8;
                    this.PAGW = 7;
                    this.PAWW = 7;
                    this.PBSW = 9;
                    this.PHTW = 2;
                    this.PLFW = 2;
                    this.PLUW = 15;
                    this.PMCW = 2;
                    this.PPMW = 2;
                    this.PPRW = 7;
                    this.PSEW = 4;
                    this.PSTW = 10;
                    this.PTCW = 20;
                    this.PZCW = 5;
                }                
            }
            #endregion

            else if (pos == 15)
            #region ROLB
            {
                this.RateLow = 37;
                this.RateHigh = 91;

                if (type == 31)
                {
                    //speed rush
                    this.PACW = 13;
                    this.PAGW = 5;
                    this.PAWW = 7;
                    this.PLFW = 22;
                    this.PLUW = 10;
                    this.PMCW = 2;
                    this.PPRW = 7;
                    this.PSEW = 8;
                    this.PSTW = 6;
                    this.PTCW = 10;
                    this.PZCW = 6;
                }
                else if (type == 32)
                {
                    //power rush
                    this.PACW = 10;
                    this.PAGW = 2;
                    this.PAWW = 7;
                    this.PHTW = 2;
                    this.PLFW = 22;
                    this.PLUW = 12;
                    this.PMCW = 2;
                    this.PPMW = 22;
                    this.PPRW = 7;
                    this.PSEW = 5;
                    this.PSTW = 8;
                    this.PTCW = 15;
                    this.PZCW = 6;
                }
                else if (type == 33)
                {
                    this.RateLow = 36;
                    this.RateHigh = 92;
                    //run stop
                    this.PACW = 11;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PLUW = 11;
                    this.PMCW = 6;
                    this.PPRW = 9;
                    this.PSEW = 9;
                    this.PSTW = 2;
                    this.PTCW = 14;
                    this.PZCW = 18;
                }
                else if (type == 34)
                {

                    this.PACW = 9;
                    this.PAGW = 2;
                    this.PAWW = 7;
                    this.PBSW = 16;
                    this.PHTW = 3;
                    this.PLUW = 16;
                    this.PMCW = 2;
                    this.PPRW = 7;
                    this.PSEW = 6;
                    this.PSTW = 10;
                    this.PTCW = 16;
                    this.PZCW = 6;
                }
            }
            #endregion

            else if (pos == 16)
            #region CB
            {
                this.RateLow = 38;
                this.RateHigh = 95;

                if (type == 38)
                {
                    //speed rush
                    this.PACW = 18;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PCAW = 2;
                    this.PJUW = 4;
                    this.PMCW = 22;
                    this.PPEW = 7;
                    this.PPRW = 9;
                    this.PSEW = 20;
                    this.PTCW = 3;
                    this.PZCW = 2;
                }
                else if (type == 39)
                {   
                    //slot
                    this.RateLow = 37;
                    this.RateHigh = 92;                    
                    this.PACW = 14;
                    this.PAGW = 10;
                    this.PAWW = 9;
                    this.PBSW = 2;
                    this.PCAW = 2;
                    this.PJUW = 4;
                    this.PLUW = 4;
                    this.PMCW = 14;
                    this.PPEW = 3;
                    this.PPRW = 9;
                    this.PSEW = 12;
                    this.PTCW = 12;
                    this.PZCW = 5;
                }
                else if (type == 40)
                {                    
                    //run stop
                    this.PACW = 20;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PCAW = 2;
                    this.PJUW = 4;
                    this.PMCW = 4;
                    this.PPEW = 5;
                    this.PPRW = 9;
                    this.PSEW = 18;
                    this.PTCW = 5;
                    this.PZCW = 20;
                }                
            }
            #endregion

            else if (pos == 17)
            #region FS
            {
                this.RateLow = 38;
                this.RateHigh = 95;

                if (type == 41)
                {
                    this.RateLow = 35;
                    this.RateHigh = 92;
                    this.PACW = 10;
                    this.PAGW = 8;
                    this.PAWW = 11;
                    this.PJUW = 3;
                    this.PLUW = 6;
                    this.PMCW = 0;
                    this.PPEW = 0;
                    this.PPRW = 11;
                    this.PSEW = 14;
                    this.PSTW = 2;
                    this.PTCW = 11;
                    this.PZCW = 22;
                }
                else if (type == 42)
                {
                    this.RateLow = 36;
                    this.RateHigh = 91;

                    this.PACW = 11;
                    this.PAGW = 8;
                    this.PAWW = 10;
                    this.PJUW = 3;
                    this.PLUW = 4;
                    this.PMCW = 15;
                    this.PPEW = 5;
                    this.PPRW = 10;
                    this.PSEW = 14;
                    this.PSTW = 2;
                    this.PTCW = 11;
                    this.PZCW = 7;
                }
                else if (type == 43)
                {
                    this.RateLow = 35;
                    this.RateHigh = 90;
                    
                    this.PACW = 8;
                    this.PAGW = 6;
                    this.PAWW = 11;
                    this.PBSW = 2;
                    this.PHTW = 24;
                    this.PJUW = 3;
                    this.PLUW = 9;
                    this.PMCW = 6;
                    this.PPEW = 3;
                    this.PPRW = 11;
                    this.PSEW = 9;
                    this.PSTW = 2;
                    this.PTCW = 15;
                    this.PZCW = 8;
                }
            }
            #endregion

            else if (pos == 18)
            #region SS
            {
                this.RateLow = 38;
                this.RateHigh = 95;

                if (type == 41)
                {
                    this.RateLow = 35;
                    this.RateHigh = 92;
                    this.PACW = 10;
                    this.PAGW = 8;
                    this.PAWW = 11;
                    this.PJUW = 3;
                    this.PLUW = 6;
                    this.PMCW = 0;
                    this.PPEW = 0;
                    this.PPRW = 11;
                    this.PSEW = 14;
                    this.PSTW = 2;
                    this.PTCW = 11;
                    this.PZCW = 22;
                }
                else if (type == 42)
                {
                    this.RateLow = 36;
                    this.RateHigh = 91;

                    this.PACW = 11;
                    this.PAGW = 8;
                    this.PAWW = 10;
                    this.PJUW = 3;
                    this.PLUW = 4;
                    this.PMCW = 15;
                    this.PPEW = 5;
                    this.PPRW = 10;
                    this.PSEW = 14;
                    this.PSTW = 2;
                    this.PTCW = 11;
                    this.PZCW = 7;
                }
                else if (type == 43)
                {
                    this.RateLow = 35;
                    this.RateHigh = 90;

                    this.PACW = 8;
                    this.PAGW = 6;
                    this.PAWW = 11;
                    this.PBSW = 2;
                    this.PHTW = 4;
                    this.PJUW = 3;
                    this.PLUW = 9;
                    this.PMCW = 6;
                    this.PPEW = 3;
                    this.PPRW = 11;
                    this.PSEW = 9;
                    this.PSTW = 2;
                    this.PTCW = 15;
                    this.PZCW = 8;
                }
            }
            #endregion

            else if (pos == 19)
            #region K
            {
                this.RateLow = 12;
                this.RateHigh = 99;

                if (type == 44)
                {  
                    this.PAWW = 40;
                    this.PKAW = 60;
                   
                }
                else if (type == 45)
                {                   
                    this.PAWW = 40;
                    this.PKPW = 60;
                    
                }                
            }
            #endregion

            else if (pos == 20)
            #region P
            {
                this.RateLow = 10;
                this.RateHigh = 99;

                if (type == 44)
                {
                    this.PAWW = 40;
                    this.PKAW = 60;
                }
                else if (type == 45)
                {
                    this.PAWW = 40;
                    this.PKPW = 60;
                }
            }
            #endregion

            else if (pos == 21)
            #region KR
            {
                this.RateLow = 0;
                this.RateHigh = 99;
                
                if (type == 12)
                {
                    this.PACW = 8;
                    this.PKRW = 80;
                    this.PSEW = 12;
                }
            }
            #endregion

            else if (pos == 22)
            #region PR
            {
                this.RateLow = 0;
                this.RateHigh = 99;
                if (type == 12)
                {
                    this.PACW = 12;
                    this.PCAW = 5;
                    this.PKRW = 75;
                    this.PSEW = 12;
                }
            }
            #endregion

            else if (pos == 23)
            #region KOS
            {
                this.RateLow = 12;
                this.RateHigh = 99;

                if (type == 45)
                {
                    this.PKAW = 20;
                    this.PKPW = 80;
                }
            }
            #endregion

            else if (pos == 24)
            #region LS
            {
                this.RateLow = 35;
                this.RateHigh = 88;

                if (type == 18)
                {
                    this.PAWW = 5;
                    this.PPBW = 50;
                    this.PSTW = 5;

                }
            }
            #endregion

            else if (pos == 25)
            #region TDB
            {                
                if (type == 6)
                {
                    //receiving
                    this.RateLow = 52;
                    this.RateHigh = 89;
                    this.PACW = 12;
                    this.PAGW = 8;
                    this.PAWW = 9;                    
                    this.PCAW = 14;
                    this.PCIW = 8;
                    this.PCRW = 7;
                    this.PLBW = 5;
                    this.PLEW = 4;
                    this.PLJW = 5;
                    this.PLPW = 5;                    
                    this.PSEW = 12;
                    this.SRRW = 10;
                }                
            }
            #endregion

            else if (pos == 26)
            #region PHB
            {
                if (type == 4)
                {
                    // power
                    this.RateLow = 50;
                    this.RateHigh = 95;
                    this.PACW = 9;
                    this.PAGW = 5;
                    this.PAWW = 8;
                    this.PBTW = 12;
                    this.PCRW = 13;
                    this.PLBW = 10;
                    this.PLSW = 11;
                    this.PLTW = 13;
                    this.PSEW = 9;
                    this.PSTW = 10;
                } 
            }
            #endregion

            else if (pos == 27)
            #region Slot WR
            {
                if (type == 12)
                {
                    this.RateLow = 35;
                    this.RateHigh = 95;
                    this.PACW = 12;
                    this.PAGW = 7;
                    this.PAWW = 10;
                    this.PBTW = 1;
                    this.PCAW = 12;
                    this.PCIW = 13;
                    this.PCRW = 1;
                    this.PDRW = 3;
                    this.PLBW = 1;
                    this.PLEW = 2;
                    this.PLJW = 1;
                    this.PLPW = 1;
                    this.PMRW = 9;
                    this.PRBW = 1;
                    this.PSEW = 10;
                    this.SRRW = 16;
                }
            }
            #endregion

            else if (pos == 28)
            #region RLE
            {                
                if (type == 31)
                {
                    this.RateLow = 37;
                    this.RateHigh = 91;

                    this.PACW = 13;
                    this.PAGW = 7;
                    this.PAWW = 7;
                    this.PLFW = 18;
                    this.PLUW = 10;
                    this.PPMW = 14;
                    this.PPRW = 7;
                    this.PSEW = 8;
                    this.PSTW = 6;
                    this.PTCW = 10;
                }
            }
            #endregion

            else if (pos == 29)
            #region RRE
            {
                if (type == 31)
                {
                    this.RateLow = 37;
                    this.RateHigh = 91;

                    this.PACW = 13;
                    this.PAGW = 7;
                    this.PAWW = 7;
                    this.PLFW = 18;
                    this.PLUW = 10;
                    this.PPMW = 14;
                    this.PPRW = 7;
                    this.PSEW = 8;
                    this.PSTW = 6;
                    this.PTCW = 10;
                }
            }
            #endregion

            else if (pos == 30)
            #region RDT 
            {
                if (type == 26)
                {
                    this.RateLow = 36;
                    this.RateHigh = 92;
                   
                    this.PACW = 10;
                    this.PAGW = 6;
                    this.PAWW = 7;
                    this.PHTW = 3;
                    this.PLFW = 5;
                    this.PLUW = 8;
                    this.PPMW = 18;
                    this.PPRW = 7;
                    this.PSEW = 7;
                    this.PSTW = 15;
                    this.PTCW = 16;
                }
            }
            #endregion

            else if (pos == 31)
            #region SLB
            {
                if (type == 33)
                {
                    this.RateLow = 36;
                    this.RateHigh = 93;
                    this.PACW = 11;
                    this.PAGW = 4;
                    this.PAWW = 9;
                    this.PBSW = 3;
                    this.PLUW = 11;
                    this.PMCW = 6;
                    this.PPRW = 9;
                    this.PSEW = 9;
                    this.PSTW = 2;
                    this.PTCW = 14;
                    this.PZCW = 15;
                }
            }
            #endregion

            else if (pos == 32)
            #region SCB
            {
                if (type == 39)
                {
                    this.RateLow = 37;
                    this.RateHigh = 92;                    
                    this.PACW = 14;
                    this.PAGW = 10;
                    this.PAWW = 9;
                    this.PBSW = 2;
                    this.PCAW = 2;
                    this.PJUW = 4;
                    this.PLUW = 4;
                    this.PMCW = 14;
                    this.PPEW = 3;
                    this.PPRW = 9;
                    this.PSEW = 12;
                    this.PTCW = 12;
                    this.PZCW = 5;
                }
            }
            #endregion

            SetTotalWeight((int)MaddenFileVersion.Ver2019);
            SetRatings();
        }
                
        public ovrdef(int pos)
        {
            Ratings = new Dictionary<int, double>();
            this.PPOS = pos;
            this.PINW = .5;

            switch (pos)
            {
                case 0:
                    {
                        this.RateHigh = 89;
                        this.PTAW = 4;
                        this.PSEW = 1.5;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PTPW = 3.5;
                        this.PBTW = .5;
                        this.PAWW = 3;
                        break;
                    }
                case 1:
                    {
                        this.RateHigh = 90;
                        this.PCAW = 1;
                        this.PPBW = .5;
                        this.PACW = 1;
                        this.PSEW = 2.5;
                        this.PAGW = 2.5;
                        this.PINW = .5;
                        this.PCRW = 1.5;
                        this.PBTW = 2.5;
                        this.PSTW = .5;
                        this.PAWW = 1.5;
                        break;
                    }
                case 2:
                    {
                        this.RateHigh = 73;
                        this.PCAW = 3;
                        this.PPBW = .5;
                        this.PRBW = 4;
                        this.PACW = 1;
                        this.PSEW = 1;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PCRW = 1;
                        this.PBTW = 1;
                        this.PSTW = 1;
                        this.PAWW = 1.5;
                        break;
                    }
                case 3:
                    {
                        this.RateHigh = 93;
                        this.PCAW = 3;
                        this.PACW = 1.5;
                        this.PSEW = 1.5;
                        this.PAGW = 1.5;
                        this.PINW = .5;
                        this.PBTW = .5;
                        this.PSTW = .5;
                        this.PJUW = 1;
                        this.PAWW = 1.5;
                        break;
                    }
                case 4:
                    {
                        this.RateHigh = 77;
                        this.PCAW = 2;
                        this.PPBW = .5;
                        this.PRBW = 2;
                        this.PACW = .5;
                        this.PSEW = 1;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PBTW = .5;
                        this.PSTW = 1;
                        this.PAWW = 1;
                        break;
                    }
                case 5:
                case 9:
                    {
                        this.RateHigh = 92;
                        this.PPBW = 3;
                        this.PRBW = 2.5;
                        this.PACW = .5;
                        this.PSEW = .5;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PSTW = 2;
                        this.PAWW = 2;
                        break;
                    }
                case 6:
                case 7:
                case 8:
                    {
                        this.RateHigh = 88;
                        this.PPBW = 2;
                        this.PRBW = 3;
                        this.PACW = 1;
                        this.PSEW = 1;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PSTW = 2;
                        this.PAWW = 2;
                        break;
                    }
                case 10:
                case 11:
                    {
                        this.RateHigh = 84;
                        this.PACW = 1;
                        this.PTCW = 1.5;
                        this.PSEW = 1;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PSTW = 1;
                        this.PAWW = .5;
                        break;
                    }
                case 12:
                    {
                        this.RateHigh = 86;
                        this.PACW = 1.5;
                        this.PTCW = 2.5;
                        this.PSEW = 1;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PSTW = 3;
                        this.PAWW = 2;
                        break;
                    }
                case 13:
                case 15:
                    {
                        this.RateHigh = 86;
                        this.PCAW = .5;
                        this.PACW = .5;
                        this.PTCW = 2;
                        this.PSEW = 1.5;
                        this.PAGW = .5;
                        this.PSTW = 1;
                        this.PAWW = 1.5;
                        break;
                    }
                case 14:
                    {
                        this.RateHigh = 90;
                        this.PACW = 1;
                        this.PTCW = 3;
                        this.PSEW = .5;
                        this.PAGW = 1;
                        this.PINW = .5;
                        this.PSTW = 2;
                        this.PAWW = 3;
                        break;
                    }
                case 16:
                    {
                        this.RateHigh = 88.5;
                        this.PCAW = 2;
                        this.PACW = 1.5;
                        this.PTCW = 1;
                        this.PSEW = 2.5;
                        this.PAGW = 1;
                        this.PINW = .5;
                        this.PSTW = .5;
                        this.PJUW = 1;
                        this.PAWW = 2.5;
                        break;
                    }
                case 17:
                    {
                        this.RateHigh = 85;
                        this.PCAW = 2;
                        this.PACW = 1.5;
                        this.PTCW = 1.5;
                        this.PSEW = 2;
                        this.PAGW = .5;
                        this.PINW = .5;
                        this.PSTW = .5;
                        this.PJUW = 1;
                        this.PAWW = 3;
                        break;
                    }
                case 18:
                    {
                        this.RateHigh = 84;
                        this.PCAW = 2;
                        this.PACW = 1;
                        this.PTCW = 2;
                        this.PSEW = 2;
                        this.PAGW = 1;
                        this.PINW = 0;
                        this.PSTW = 1;
                        this.PJUW = .5;
                        this.PAWW = 3;
                        break;
                    }
                case 19:
                    {
                        this.RateHigh = 93;
                        this.RateLow = 60;
                        this.PKAW = 3.5;
                        this.PKPW = 3;
                        this.PINW = 0;
                        this.PAWW = .5;
                        break;
                    }
                case 20:
                    {
                        this.RateHigh = 92.5;
                        this.RateLow = 60;
                        this.PINW = 0;
                        this.PKAW = 3;
                        this.PKPW = 3.5;
                        this.PAWW = .5;
                        break;
                    }

                default:
                    break;
            }

            SetTotalWeight((int)MaddenFileVersion.Ver2008);
            SetRatings();
        }
                
        public ovrdef(OverallRecord play)
        {
            Ratings = new Dictionary<int, double>();
            RateHigh = play.RatingHigh;
            RateLow = play.RatingLow;
            PPOS = play.Position;

            PACW = play.Acceleration;
            PAGW = play.Agility;
            PAWW = play.Awareness;
            PBTW = play.BreakTackle;
            PCAW = play.Catch;
            PCRW = play.Carry;
            PINW = play.Injury;
            PJUW = play.Jump;
            PKAW = play.KickAccuracy;
            PKPW = play.KickPower;
            PKRW = play.KickReturn; 
            PPBW = play.PassBlock;
            PRBW = play.RunBlock;
            PSEW = play.Speed;
            PSTW = play.Strength;
            PTAW = play.ThrowAccuracy;
            PTCW = play.Tackle;    
            PTPW = play.ThrowPower;

            SetTotalWeight((int)MaddenFileVersion.Ver2008);
            SetRatings();
        }

        public void SetTotalWeight(int ver)
        {
            if (ver <= (int)MaddenFileVersion.Ver2008)
            {
                totalweight = PCAW + PKAW + PTAW + PPBW + PRBW + PACW + PTCW +
                PSEW + PAGW + PKPW + PTPW + PCRW + PKRW + PBTW + PSTW + PJUW + PAWW;
            }
            else
            {
                totalweight = PACW + PAGW + PAWW + PBKW + PBSW + PBTW + PCAW + PCIW + PCNW + PCRW
                    + PDRW + PHTW + PIJW + PINW + PJUW + PKAW + PKPW + PKRW + PLBW + PLEW
                    + PLFW + PLIW + PLJW + PLPW + PLRW + PLSW + PLTW + PLUW + PLWW + PMCW + PMRW
                    + PPBW + PPEW + PPMW + PPRW + PPWA + PPWW + PRBW + PRDH + PRDL + PRLW + PRRW
                    + PRSW + PSAW + PSEW + PSHW + PSTW + PTAW + PTCW + PTDW + PTMW + PTOW + PTPW
                    + PTRW + PTSW + PUPW + PYSW + PZCW + SRRW;
            }
        }

        public void SetRatings()
        {
            Ratings.Clear();
            Ratings.Add(0, this.PSTW);
            Ratings.Add(1, this.PAGW);
            Ratings.Add(2, this.PSEW);
            Ratings.Add(3, this.PACW);
            Ratings.Add(4, this.PAWW);
            Ratings.Add(5, this.PCAW);
            Ratings.Add(6, this.PCRW);
            Ratings.Add(7, this.PTPW);
            Ratings.Add(8, this.PTAW);
            Ratings.Add(9, this.PKPW);
            Ratings.Add(10, this.PKAW);
            Ratings.Add(11, this.PBTW);
            Ratings.Add(12, this.PTCW);
            Ratings.Add(13, this.PPBW);
            Ratings.Add(14, this.PRBW);
            Ratings.Add(15, this.PJUW);
            Ratings.Add(16, this.PKRW);
            Ratings.Add(18, this.PINW);
            Ratings.Add(19, this.PBKW);
            Ratings.Add(20, this.PPWA);
            Ratings.Add(21, this.PTDW);
            Ratings.Add(22, this.PTMW);
            Ratings.Add(23, this.PTRW);
            Ratings.Add(24, this.PTSW);
            Ratings.Add(25, this.PUPW);
            Ratings.Add(26, this.PLTW); // trucking
            Ratings.Add(27, this.PLEW); // elusive
            Ratings.Add(28, this.PLJW); // juke
            Ratings.Add(29, this.PLPW); // spin move
            Ratings.Add(30, this.PLSW); // stiff arm
            Ratings.Add(31, this.PLBW); // vision
            Ratings.Add(32, this.SRRW); // short route
            Ratings.Add(33, this.PBTW); // 2019, need to add but not using
            Ratings.Add(34, this.PCIW); // catch in traffic
            Ratings.Add(35, this.PRLW); // spec catch
            Ratings.Add(36, this.PLIW); // impact block
            Ratings.Add(37, this.PLKW); // lead block
            Ratings.Add(38, this.PLRW); // run block strength
            Ratings.Add(39, this.PLWW); // run block footwork
            Ratings.Add(40, this.PPWW); // pass block footwork
            Ratings.Add(41, this.PYSW); // pass block strength
            Ratings.Add(42, this.PDRW); // deep route
            Ratings.Add(43, this.PMRW); // medium route
            Ratings.Add(44, this.PSHW); // release for WR
            Ratings.Add(45, this.PBSW); // block shedding
            Ratings.Add(46, this.PHTW); // Hit power
            Ratings.Add(47, this.PLFW); // Finesse Moves
            Ratings.Add(48, this.PPMW); // Power Moves
            Ratings.Add(49, this.PPRW); // play recognition
            Ratings.Add(50, this.PLUW); // pursuit
            Ratings.Add(51, this.PMCW); // man cover
            Ratings.Add(52, this.PZCW); // zone cover
            Ratings.Add(53, this.PKRW); // kick return
            
        }

        public double GetPerc(int trait, double rate)
        {
            if (this.Ratings[trait] == 0)
                return 0;
            double median = (this.RateHigh + this.RateLow) / 2;
            double point = 100 / (this.RateHigh - this.RateLow);
            double perc = point * this.Ratings[trait] / totalweight;
            double result = (rate - median) * perc;

            return result;
        }

        public double GetOverall(PlayerRecord rec)
        {
            double total = 50;

            double str = GetPerc((int)Rating.STR, rec.GetRating((int)Rating.STR));
            double agi = GetPerc((int)Rating.AGI, rec.GetRating((int)Rating.AGI));
            double spd = GetPerc((int)Rating.SPD, rec.GetRating((int)Rating.SPD));
            double acc = GetPerc((int)Rating.ACC, rec.GetRating((int)Rating.ACC));
            double awr = GetPerc((int)Rating.AWR, rec.GetRating((int)Rating.AWR));
            double cth = GetPerc((int)Rating.CTH, rec.GetRating((int)Rating.CTH));
            double car = GetPerc((int)Rating.CAR, rec.GetRating((int)Rating.CAR));
            double thp = GetPerc((int)Rating.THP, rec.GetRating((int)Rating.THP));
            double tha = GetPerc((int)Rating.THA, rec.GetRating((int)Rating.THA));
            double kpw = GetPerc((int)Rating.KPW, rec.GetRating((int)Rating.KPW));
            double kac = GetPerc((int)Rating.KAC, rec.GetRating((int)Rating.KAC));
            double btk = GetPerc((int)Rating.BTK, rec.GetRating((int)Rating.BTK));
            double tak = GetPerc((int)Rating.TAK, rec.GetRating((int)Rating.TAK));
            double pbk = GetPerc((int)Rating.PBK, rec.GetRating((int)Rating.PBK));
            double rbk = GetPerc((int)Rating.RBK, rec.GetRating((int)Rating.RBK));
            double jmp = GetPerc((int)Rating.JMP, rec.GetRating((int)Rating.JMP));

            double physical = str + agi + spd + acc + thp + jmp + kpw;
            //physical = Math.Truncate(physical);


            total += physical + awr + cth + car + btk + tak + pbk + rbk + kac + tha;
            //double round = total - Math.Truncate(total);
            //total = Math.Truncate(total);
            //if (round > .50)
            //    total++;
            return total;
        }

        public double GetOverall19(PlayerRecord rec)
        {
            double total = 50;

            double str = GetPerc((int)Rating.STR, rec.GetRating((int)Rating.STR));
            double agi = GetPerc((int)Rating.AGI, rec.GetRating((int)Rating.AGI));
            double spd = GetPerc((int)Rating.SPD, rec.GetRating((int)Rating.SPD));
            double acc = GetPerc((int)Rating.ACC, rec.GetRating((int)Rating.ACC));
            double awr = GetPerc((int)Rating.AWR, rec.GetRating((int)Rating.AWR));
            double cth = GetPerc((int)Rating.CTH, rec.GetRating((int)Rating.CTH));
            double car = GetPerc((int)Rating.CAR, rec.GetRating((int)Rating.CAR));
            double thp = GetPerc((int)Rating.THP, rec.GetRating((int)Rating.THP));
            double tha = GetPerc((int)Rating.THA, rec.GetRating((int)Rating.THA));
            double kpw = GetPerc((int)Rating.KPW, rec.GetRating((int)Rating.KPW));
            double kac = GetPerc((int)Rating.KAC, rec.GetRating((int)Rating.KAC));
            double btk = 0;
            // s68 have to reverse this fieldname for big endian database
            if (rec.ContainsIntField("TKBP") || rec.ContainsIntField("PBKT"))
                btk = GetPerc((int)Rating.BTK, rec.GetRating((int)Rating.BKT));
            else btk = GetPerc((int)Rating.BTK, rec.GetRating((int)Rating.BTK));
            double tak = GetPerc((int)Rating.TAK, rec.GetRating((int)Rating.TAK));
            double pbk = GetPerc((int)Rating.PBK, rec.GetRating((int)Rating.PBK));
            double rbk = GetPerc((int)Rating.RBK, rec.GetRating((int)Rating.RBK));
            double jmp = GetPerc((int)Rating.JMP, rec.GetRating((int)Rating.JMP));
            double bks = GetPerc((int)Rating.BKS, rec.GetRating((int)Rating.BKS));
            double pwa = GetPerc((int)Rating.PWA, rec.GetRating((int)Rating.PWA));
            double thd = GetPerc((int)Rating.THD, rec.GetRating((int)Rating.THD));
            double thm = GetPerc((int)Rating.THM, rec.GetRating((int)Rating.THM));
            double tor = GetPerc((int)Rating.TOR, rec.GetRating((int)Rating.TOR));
            double ths = GetPerc((int)Rating.THS, rec.GetRating((int)Rating.THS));
            double tup = GetPerc((int)Rating.TUP, rec.GetRating((int)Rating.TUP));
            double tru = GetPerc((int)Rating.TRU, rec.GetRating((int)Rating.TRU));
            double elu = GetPerc((int)Rating.ELU, rec.GetRating((int)Rating.ELU));
            double juk = GetPerc((int)Rating.JUK, rec.GetRating((int)Rating.JUK));
            double spn = GetPerc((int)Rating.SPN, rec.GetRating((int)Rating.SPN));
            double sfa = GetPerc((int)Rating.SFA, rec.GetRating((int)Rating.SFA));
            double vis = GetPerc((int)Rating.VIS, rec.GetRating((int)Rating.VIS));
            double srr = GetPerc((int)Rating.SRR, rec.GetRating((int)Rating.SRR));
            double cit = GetPerc((int)Rating.CIT, rec.GetRating((int)Rating.CIT));
            double spc = GetPerc((int)Rating.SPC, rec.GetRating((int)Rating.SPC));
            double ibk = GetPerc((int)Rating.IBK, rec.GetRating((int)Rating.IBK));
            double lbk = GetPerc((int)Rating.LBK, rec.GetRating((int)Rating.LBK));
            double rbs = GetPerc((int)Rating.RBS, rec.GetRating((int)Rating.RBS));
            double rbf = GetPerc((int)Rating.RBF, rec.GetRating((int)Rating.RBF));
            double pbf = GetPerc((int)Rating.PBF, rec.GetRating((int)Rating.PBF));
            double pbs = GetPerc((int)Rating.PBS, rec.GetRating((int)Rating.PBS));
            double drr = GetPerc((int)Rating.DRR, rec.GetRating((int)Rating.DRR));
            double mrr = GetPerc((int)Rating.MRR, rec.GetRating((int)Rating.MRR));
            double rel = GetPerc((int)Rating.REL, rec.GetRating((int)Rating.REL));
            double shd = GetPerc((int)Rating.SHD, rec.GetRating((int)Rating.SHD));
            double hit = GetPerc((int)Rating.HIT, rec.GetRating((int)Rating.HIT));
            double fin = GetPerc((int)Rating.FNM, rec.GetRating((int)Rating.FNM));
            double pow = GetPerc((int)Rating.PWM, rec.GetRating((int)Rating.PWM));
            double plr = GetPerc((int)Rating.PLR, rec.GetRating((int)Rating.PLR));
            double pur = GetPerc((int)Rating.PUR, rec.GetRating((int)Rating.PUR));
            double man = GetPerc((int)Rating.MAN, rec.GetRating((int)Rating.MAN));
            double zon = GetPerc((int)Rating.ZON, rec.GetRating((int)Rating.ZON));
            double krr = GetPerc((int)Rating.KRR, rec.GetRating((int)Rating.KRR));

            total += str + agi + spd + acc + thp + jmp + kpw + awr + cth + car + btk + tak + pbk + rbk + kac + tha;
            total += bks + pwa + thd + thm + tor + ths + tup + tru + elu + juk + spn + sfa + vis + srr + cit + spc;
            total += ibk + lbk + rbs + rbf + pbf + pbs + drr + mrr + rel;
            total += shd + hit + fin + pow + plr + pur + man + zon + krr;

            //double round = total - Math.Truncate(total);
            total = Math.Truncate(total);
            //if (round > .50)
            //    total++;
            return total;
        }

    }
    
    
    
    public class Overall
    {
        public Dictionary<int, ovrdef> Table;
        public List<ovrdef> OVR19;
        
        public Overall()
        {
            Table = new Dictionary<int, ovrdef>();
            OVR19 = new List<ovrdef>();            
        }

        public void InitRatings(MGMT man)
        {            
            if (man.db_misc_model != null)
            {
                Table.Clear();
                foreach (TableRecordModel rec in man.db_misc_model.TableModels[EditorModel.PLAYER_OVERALL_CALC].GetRecords())
                {
                    OverallRecord ovr = (OverallRecord)rec;
                    Table.Add(ovr.Position, new ovrdef(ovr));
                }
            }
            else
            {
                for (int p = 0; p <= 20; p++)
                {
                    Table.Add(p, new ovrdef(p));
                }
            }
        }

        public void InitRatings19()
        {
            OVR19.Clear();

            for (int pos = 0; pos <= 33; pos++)
            {
                int start = 0;
                int end = 0;
                #region Get start/end types for each position
                switch (pos)
                {
                    case (int)MaddenPositions.QB:
                        end = 3;
                        break;
                    case (int)MaddenPositions.HB:
                        {
                            start = 4;
                            end = 6;
                        }
                        break;
                    case (int)MaddenPositions.FB:
                        {
                            start = 7;
                            end = 8;
                        }
                        break;
                    case (int)MaddenPositions.WR:
                        {
                            start = 9;
                            end = 12;
                        }
                        break;
                    case (int)MaddenPositions.TE:
                        {
                            start = 13;
                            end = 15;
                        }
                        break;
                    case (int)MaddenPositions.C:
                        {
                            start = 16;
                            end = 18;
                        }
                        break;
                    case (int)MaddenPositions.LT:
                    case (int)MaddenPositions.RT:
                        {
                            start = 19;
                            end = 21;
                        }
                        break;
                    case (int)MaddenPositions.LG:
                    case (int)MaddenPositions.RG:
                        {
                            start = 22;
                            end = 24;
                        }
                        break;
                    case (int)MaddenPositions.LE:
                    case (int)MaddenPositions.RE:
                        {
                            start = 25;
                            end = 27;
                        }
                        break;
                    case (int)MaddenPositions.DT:
                        {
                            start = 28;
                            end = 30;
                        }
                        break;
                    case (int)MaddenPositions.LOLB:
                    case (int)MaddenPositions.ROLB:
                        {
                            start = 31;
                            end = 34;
                        }
                        break;
                    case (int)MaddenPositions.MLB:
                        {
                            start = 35;
                            end = 37;
                        }
                        break;
                    case (int)MaddenPositions.CB:
                        {
                            start = 38;
                            end = 40;
                        }
                        break;
                    case (int)MaddenPositions.FS:
                    case (int)MaddenPositions.SS:
                        {
                            start = 41;
                            end = 43;
                        }
                        break;
                    case (int)MaddenPositions.K:
                    case (int)MaddenPositions.P:
                        {
                            start = 44;
                            end = 45;
                        }
                        break;
                    case (int)MaddenPositions.KR:
                        {
                            start = 12;
                            end = 12;
                        }
                        break;
                    case (int)MaddenPositions.PR:
                        {
                            start = 12;
                            end = 12;
                        }
                        break;
                    case (int)MaddenPositions.KOS:
                        {
                            start = 45;
                            end = 45;
                        }
                        break;
                    case (int)MaddenPositions.LS:
                        {
                            start = 18;
                            end = 18;
                        }
                        break;
                    case (int)MaddenPositions.TDB:
                        {
                            start = 6;
                            end = 6;
                        }
                        break;
                    case (int)MaddenPositions2019.PHB:
                        {
                            start = 4;
                            end = 4;
                        }
                        break;
                    case (int)MaddenPositions2019.SWR:
                        {
                            start = 12;
                            end = 12;
                        }
                        break;
                    case (int)MaddenPositions2019.RLE:
                        {
                            start = 31;
                            end = 31;
                        }
                        break;
                    case (int)MaddenPositions2019.RRE:
                        {
                            start = 31;
                            end = 31;
                        }
                        break;
                    case (int)MaddenPositions2019.RDT:
                        {
                            start = 26;
                            end = 26;
                        }
                        break;
                    case (int)MaddenPositions2019.SLB:
                        {
                            start = 33;
                            end = 33;
                        }
                        break;
                    case (int)MaddenPositions2019.SCB:
                        {
                            start = 39;
                            end = 39;
                        }
                        break;
                }
                #endregion

                for (int type = start; type <= end; type++)
                    OVR19.Add(new ovrdef(pos, type));
            }
        }

        public double GetOverall(PlayerRecord rec)
        {
            return Table[rec.PositionId].GetOverall(rec);
        }

        public double GetOverall19(PlayerRecord rec, int pos, int type)
        {
            if (rec.PositionId == pos && type == -1)
                return (double)rec.Overall;
            
            double ovr = 99;

            foreach (ovrdef overdef in OVR19)
            {
                if (type == -1)
                {
                    if (overdef.PPOS == pos)
                    {
                        double check = overdef.GetOverall19(rec);
                        if (check < ovr)
                            ovr = check;
                    }                            
                }
                else
                { 
                    if (overdef.PPOS == pos && overdef.PLTY == type)
                    return overdef.GetOverall19(rec);
                }
            }

            if (type == -1)
                return ovr;            
            else return 0;
        }
        
        public double GetRatingOVR(PlayerRecord rec, int rating)
        {
            return Table[rec.PositionId].GetPerc(rating, rec.GetRating(rating));           
        }
        public double GetRatingOVR19(PlayerRecord rec, int rating)
        {
            return Table[rec.PlayerType].GetPerc(rating, rec.GetRating(rating));
        }
        
        
    }
}
