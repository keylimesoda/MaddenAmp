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
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MaddenEditor.Core.Manager
{
    public class Contract
    {
        /*
        Transitional Free Agents (TFA)
        A transition player designation gives the club a first refusal right to match an offer sheet given to the player by another club. To designate a transition player, 
        the club must offer a minimum of the average of the top 10 salaries of the previous year at the player's position, or a 20 percent salary increase, whichever is greater. 
        If the player does not get an offer, then he must play for the transition offer. If the player gets an offer, then his previous team has seven days to match the offer 
        or lose the player. If they do not match the offer, the previous team gets no compensation.
         * March-July
         * After July - week 10 only orig team can negotiate.  If not signed by week 10 must sit out

        Franchise Free Agents (FFA)
        A franchise player is offered a minimum of the average of the top five salaries at his position in the previous season, or a 20 percent salary increase, whichever is greater. 
        This type of franchise player may negotiate with other clubs. His original club has seven days to match the offer and retain the player, or receive two first-round draft 
        choices as compensation if the original club elects not to match.

         * March - 10th week of season, if not signed must sit out season
         * 1 year deal worth average of top 5 salaries at his position
         * 
         * 
        Exclusive Franchise Free Agents (EFFA)
        The second type of franchise player is offered a minimum of the average of the top five salaries at his position computed on April 15th at the end of restricted free agency, 
        or a 20 percent salary increase, whichever is greater. This type of franchise player may negotiate with other clubs. Other clubs cannot negotiate with exclusive franchise
        players. 
         
        Restricted Free Agents
         * Before Draft
         * 
          
        */

        public Dictionary<int, double> NFLCAP = new Dictionary<int, double>();
        public Dictionary<int, double> RFA13 = new Dictionary<int, double>();
        public Dictionary<int, double> RFA1 = new Dictionary<int, double>();
        public Dictionary<int, double> RFA2 = new Dictionary<int, double>();
        public Dictionary<int, double> RFA3 = new Dictionary<int, double>();
        public Dictionary<int, double> RFA0 = new Dictionary<int, double>();

        public Dictionary<int, int> Min0 = new Dictionary<int, int>();
        public Dictionary<int, int> Min1 = new Dictionary<int, int>();
        public Dictionary<int, int> Min2 = new Dictionary<int, int>();
        public Dictionary<int, int> Min3 = new Dictionary<int, int>();
        public Dictionary<int, int> Min46 = new Dictionary<int, int>();
        public Dictionary<int, int> Min79 = new Dictionary<int, int>();
        public Dictionary<int, int> Min10 = new Dictionary<int, int>();


        public List<int> TFA = new List<int>();
        public List<int> FFA = new List<int>();
        public List<int> EFFA = new List<int>();
        
        
        public double inflation;




        

        

        public void Init()
        {
            #region Cap
            NFLCAP.Add(1994, 34.608);
            NFLCAP.Add(1995, 37.1);
            NFLCAP.Add(1996,40.753);
            NFLCAP.Add(1997,41.454);
            NFLCAP.Add(1998,52.388);
            NFLCAP.Add(1999,57.288);
            NFLCAP.Add(2000,62.172);
            NFLCAP.Add(2001,67.405);
            NFLCAP.Add(2002,71.101);
            NFLCAP.Add(2003, 75.007);
            NFLCAP.Add(2004, 80.582);
            NFLCAP.Add(2005, 85.5);
            NFLCAP.Add(2006, 102);
            NFLCAP.Add(2007, 109);
            NFLCAP.Add(2008, 116);
            NFLCAP.Add(2009, 123);
            NFLCAP.Add(2010, 123);       // uncapped year
            NFLCAP.Add(2011, 120);
            NFLCAP.Add(2012, 120.6);
            NFLCAP.Add(2013, 123);
            NFLCAP.Add(2014, 133);
            NFLCAP.Add(2015, 143.28);
            NFLCAP.Add(2016, 155.27);
            NFLCAP.Add(2017, 167.00);
            NFLCAP.Add(2018, 177.20);
            #endregion

            #region RFA
            // tenders must be 10% higher pay than previous year, or offical tenders whichever is higher

            RFA13.Add(2004, 1.824);     // R1 + R3
            RFA1.Add(2004, 1.37);       // R1            
            RFA3.Add(2004, .620);       // R3
            RFA0.Add(2004, .450);       // Players original round
            
            RFA13.Add(2005, 1.900);     // R1 + R3 Comp            
            RFA1.Add(2005, 1.43);       // R1            
            RFA0.Add(2005, .656);       // Players original round
            
            RFA13.Add(2006, 2.097);     // R1 + R3 rounded up
            RFA1.Add(2006, 1.573);      // R1            
            RFA0.Add(2006, .722);       // Players original round
                        
            RFA13.Add(2007, 2.350);     // R1 + R3
            RFA1.Add(2007, 1.850);      // R1            
            RFA2.Add(2007, 1.300);      // R2
            RFA0.Add(2007, .850);       // Players original round

            RFA13.Add(2008, 2.562);     // R1 + R3
            RFA1.Add(2008, 2.017);      // R1            
            RFA2.Add(2008, 1.417);      // R2
            RFA0.Add(2008, .927);       // Players original round

            RFA13.Add(2009, 2.792);     // R1 + R3
            RFA1.Add(2009, 2.198);      // R1            
            RFA2.Add(2009, 1.545);      // R2
            RFA0.Add(2009, 1.010);      // Players original round
            
            RFA13.Add(2010, 3.043);     // R1 + R3
            RFA1.Add(2010, 2.396);      // R1
            RFA2.Add(2010, 1.684);      // R2            
            RFA0.Add(2010, 1.101);      // Players original round

            RFA13.Add(2011, 3.317);     // R1 + R3
            RFA1.Add(2011, 2.611);      // R1
            RFA2.Add(2011, 1.835);      // R2            
            RFA0.Add(2010, 1.200);      // Players original round

            RFA13.Add(2012, 3.616);     // R1 + R3
            RFA1.Add(2012, 2.846);      // R1
            RFA2.Add(2012, 2.000);      // R2            
            RFA0.Add(2012, 1.308);      // Players original round
                        
            RFA1.Add(2012, 2.879);      // R1
            RFA2.Add(2012, 2.023);      // R2            
            RFA0.Add(2012, 1.323);      // Players original round

            RFA1.Add(2014, 3.113);      // R1 
            RFA2.Add(2014, 2.187);      // R2                                        
            RFA1.Add(2014, 1.431);      // Players original draft round

            RFA1.Add(2015, 3.354);      // R1 
            RFA2.Add(2015, 2.356);      // R2                                        
            RFA1.Add(2015, 1.542);      // Players original draft round

            RFA1.Add(2016, 3.582);      // R1 
            RFA2.Add(2016, 2.516);      // R2                                        
            RFA1.Add(2016, 1.647);      // Players original draft round

            RFA1.Add(2017, 3.910);      // R1
            RFA2.Add(2017, 2.746);      // R2
            RFA0.Add(2017, 1.797);      // Players original round
            
            RFA1.Add(2018, 4.149);      // R1
            RFA2.Add(2018, 2.914);      // R2
            RFA0.Add(2018, 1.907);      // Players original round
            #endregion

            #region Min Salary
            
            Min0.Add(2006, 200000);
            Min0.Add(2007, 275000);
            Min0.Add(2008, 285000);
            Min0.Add(2009, 310000);
            Min0.Add(2010, 325000);
            Min0.Add(2011, 375000);
            Min0.Add(2012, 390000);
            Min0.Add(2013, 405000);
            Min0.Add(2014, 420000);
            Min0.Add(2015, 435000);
            Min0.Add(2016, 450000);
            Min0.Add(2017, 465000);
            Min0.Add(2018, 480000);
            
            Min1.Add(2006, 300000);
            Min1.Add(2007, 350000);
            Min1.Add(2008, 360000);
            Min1.Add(2009, 385000);
            Min1.Add(2010, 400000);
            Min1.Add(2011, 450000);
            Min1.Add(2012, 465000);
            Min1.Add(2013, 480000);
            Min1.Add(2014, 495000);
            Min1.Add(2015, 510000);
            Min1.Add(2016, 525000);
            Min1.Add(2017, 540000);
            Min1.Add(2018, 555000);
            
            Min2.Add(2006, 400000);
            Min2.Add(2007, 425000);
            Min2.Add(2008, 435000);
            Min2.Add(2009, 460000);
            Min2.Add(2010, 475000);
            Min2.Add(2011, 525000);
            Min2.Add(2012, 540000);
            Min2.Add(2013, 555000);
            Min2.Add(2014, 570000);
            Min2.Add(2015, 585000);
            Min2.Add(2016, 600000);
            Min2.Add(2017, 615000);
            Min2.Add(2018, 630000);
            
            Min3.Add(2006, 450000);
            Min3.Add(2007, 500000);
            Min3.Add(2008, 510000);
            Min3.Add(2009, 535000);
            Min3.Add(2010, 550000);
            Min3.Add(2011, 600000);
            Min3.Add(2012, 615000);
            Min3.Add(2013, 630000);
            Min3.Add(2014, 645000);
            Min3.Add(2015, 660000);
            Min3.Add(2016, 675000);
            Min3.Add(2017, 690000);
            Min3.Add(2018, 705000);
            
            Min46.Add(2006, 550000);
            Min46.Add(2007, 585000);
            Min46.Add(2008, 595000);
            Min46.Add(2009, 620000);
            Min46.Add(2010, 635000);
            Min46.Add(2011, 685000);
            Min46.Add(2012, 700000);
            Min46.Add(2013, 715000);
            Min46.Add(2014, 730000);
            Min46.Add(2015, 745000);
            Min46.Add(2016, 760000);
            Min46.Add(2017, 775000);
            Min46.Add(2018, 790000);
            
            Min79.Add(2006, 650000);
            Min79.Add(2007, 710000);
            Min79.Add(2008, 720000);
            Min79.Add(2009, 745000);
            Min79.Add(2010, 760000);
            Min79.Add(2011, 810000);
            Min79.Add(2012, 825000);
            Min79.Add(2013, 840000);
            Min79.Add(2014, 855000);
            Min79.Add(2015, 870000);
            Min79.Add(2016, 885000);
            Min79.Add(2017, 900000);
            Min79.Add(2018, 915000);

            Min10.Add(2006, 750000);
            Min10.Add(2007, 810000);
            Min10.Add(2008, 820000);
            Min10.Add(2009, 845000);            
            Min10.Add(2010, 860000);
            Min10.Add(2011, 910000);
            Min10.Add(2012, 925000);
            Min10.Add(2013, 940000);
            Min10.Add(2014, 955000);
            Min10.Add(2015, 970000);
            Min10.Add(2016, 985000);
            Min10.Add(2017, 1000000);
            Min10.Add(2018, 1015000);

            #endregion

        }
    }
}
