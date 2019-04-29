/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2014 Stingray68
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
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Core.Manager
{
    public enum Con
    {
        Terrible = 0,
        BelowAvg = 1,
        Avg = 2,
        AboveAvg = 3,
        Elite = 4
    }
    
    public class Combine
    {
        public int RookieId;
        public List<double> shuttle;       // 2 attempts
        public List<double> dash;          // 2 attempts
        public int bench;
        public List<double> vertical;      // 2 attempts
        public double cones;
        public double broadjump;

        public Combine()
        {
            RookieId = 0;
            shuttle = new List<double>();
            dash = new List<double>();
            bench = 0;
            vertical = new List<double>();
            cones = 0;
            broadjump = 0;
        }

        public void Init(Player player)
        {
            //  Need to factor in under/over achieving scores
            Random rand = new Random();

            #region 40yd time
            for (int c = 0; c < 2; c++)
            {
                double variation = rand.Next(98000, 103001) / 100000;
                double totaldist = 36.576;
                double speed = 12.35 * player.Original_Ratings.SPEED / 100;         //  Fastest speed based on Usain Bolt
                double accel = 5.0 * player.Original_Ratings.ACCELERATION / 100;    //  Guesstimate
                double time = speed / accel;                                        //  99 spd and 99 acc would be about 4.20 40yds this should be extremely RARE
                double dist = accel * time * time / 2;
                double result = (Math.Round((time + ((totaldist - dist) / speed * variation)), 2, MidpointRounding.AwayFromZero));
                if (result < 4.17)                                                  // can't be faster than 4.17
                    result = 4.17;
                this.dash.Add(result);
            }
            #endregion

        }
    }

    
    
    public class DraftClass
    {
        #region members
        private DraftConfig _draftconfig;
        private List<Player> _rookies;
        private List<Combine> _scores;

        public DraftConfig draftconfig
        {
            get { return _draftconfig; }
            set { _draftconfig = value; }
        }
        public List<Player> rookies
        {
            get { return _rookies; }
            set { _rookies = value; }
        }
        public List<Combine> scores
        {
            get { return _scores; }
            set { _scores = value; }
        }        
        
        public Dictionary<int, List<string>> comments;
        public Dictionary<int, List<int>> consensus;
        public List<int> skillset;
        #region Positions
        public Dictionary<int,int>qb = new Dictionary<int,int>();
        public Dictionary<int,int>hb = new Dictionary<int,int>();
        public Dictionary<int,int>wr = new Dictionary<int,int>();
        public Dictionary<int,int>fb = new Dictionary<int,int>();
        public Dictionary<int,int>te = new Dictionary<int,int>();
        public Dictionary<int,int>lg = new Dictionary<int,int>();
        public Dictionary<int,int>rg = new Dictionary<int,int>();
        public Dictionary<int,int>lt = new Dictionary<int,int>();
        public Dictionary<int,int>rt = new Dictionary<int,int>();
        public Dictionary<int,int>c = new Dictionary<int,int>();
        public Dictionary<int,int>dt = new Dictionary<int,int>();
        public Dictionary<int,int>le = new Dictionary<int,int>();
        public Dictionary<int,int>re = new Dictionary<int,int>();
        public Dictionary<int,int>cb = new Dictionary<int,int>();
        public Dictionary<int,int>lolb = new Dictionary<int,int>();
        public Dictionary<int,int>rolb = new Dictionary<int,int>();
        public Dictionary<int,int>mlb = new Dictionary<int,int>();
        public Dictionary<int,int>fs = new Dictionary<int,int>();
        public Dictionary<int,int>ss = new Dictionary<int,int>();
        public Dictionary<int,int>p = new Dictionary<int,int>();
        public Dictionary<int,int>k = new Dictionary<int,int>();
        public Dictionary<int,int>total = new Dictionary<int,int>();
        #endregion
        #endregion
        
        public DraftClass()
        {
            draftconfig = new DraftConfig();
            rookies = new List<Player>();
            scores = new List<Combine>();
            comments = new Dictionary<int, List<string>>();
            consensus = new Dictionary<int, List<int>>();
            skillset = new List<int>();

            #region positions
            qb = new Dictionary<int, int>();
            hb = new Dictionary<int, int>();
            wr = new Dictionary<int, int>();
            fb = new Dictionary<int, int>();
            te = new Dictionary<int, int>();
            lg = new Dictionary<int, int>();
            rg = new Dictionary<int, int>();
            lt = new Dictionary<int, int>();
            rt = new Dictionary<int, int>();
            c = new Dictionary<int, int>();
            dt = new Dictionary<int, int>();
            le = new Dictionary<int, int>();
            re = new Dictionary<int, int>();
            cb = new Dictionary<int, int>();
            lolb = new Dictionary<int, int>();
            rolb = new Dictionary<int, int>();
            mlb = new Dictionary<int, int>();
            fs = new Dictionary<int, int>();
            ss = new Dictionary<int, int>();
            p = new Dictionary<int, int>();
            k = new Dictionary<int, int>();
            total = new Dictionary<int, int>();
            #endregion

        }
        
        #region Methods
        public int GetRandomInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public void InitDraftClass()
        {
            rookies = new List<Player>();
            scores = new List<Combine>();
            consensus = new Dictionary<int, List<int>>();
            comments = new Dictionary<int, List<string>>();
            DraftConfig config = new DraftConfig();
            Dictionary<int, int> rookieclass = new Dictionary<int, int>();

            #region positions
            qb = new Dictionary<int, int>();
            hb = new Dictionary<int, int>();
            wr = new Dictionary<int, int>();
            fb = new Dictionary<int, int>();
            te = new Dictionary<int, int>();
            lg = new Dictionary<int, int>();
            rg = new Dictionary<int, int>();
            lt = new Dictionary<int, int>();
            rt = new Dictionary<int, int>();
            c = new Dictionary<int, int>();
            dt = new Dictionary<int, int>();
            le = new Dictionary<int, int>();
            re = new Dictionary<int, int>();
            cb = new Dictionary<int, int>();
            lolb = new Dictionary<int, int>();
            rolb = new Dictionary<int, int>();
            mlb = new Dictionary<int, int>();
            fs = new Dictionary<int, int>();
            ss = new Dictionary<int, int>();
            p = new Dictionary<int, int>();
            k = new Dictionary<int, int>();
            total = new Dictionary<int, int>();
            #endregion

            int eliteplayers = GetRandomInt(5, 11);
            int firstround = 32 - eliteplayers;
            
        }

        
          
        /*      
        public void InitRookie_QB(Player rookie, int overall)
        {            
            List<int> rookrate = new List<int>();
            
            rookie.Original_Ratings.POSITION_ID = 0;
            rookie.Original_Ratings.SKIN_COLOR = draftconfig.SetRace(Position.QB);
            
            #region Tendency
            int tend = GetRandomInt(1, 1000001) / 10000;
            if (tend <= draftconfig.Tendencies[(int)MaddenPositions.QB].tend0)
                rookie.Original_Ratings.TENDENCY = 0;
            else if (tend >= 100-draftconfig.Tendencies[(int)MaddenPositions.QB].tend1)
                rookie.Original_Ratings.TENDENCY = 1;
            else rookie.Original_Ratings.TENDENCY = 2;
            tend = rookie.Original_Ratings.TENDENCY;
            #endregion

            #region Throwstyle  90% over, 10% side
            double r = GetRandomInt(1, 1000001) / 10000;
            if (r < 90.00)
                rookie.Original_Ratings.THROWING_STYLE = false;
            else rookie.Original_Ratings.THROWING_STYLE = true;
            #endregion

            #region Throw Accuracy
            GetSkills(rookie, Trait.THA);
            rookie.Original_Ratings.THROW_ACCURACY = rookrate[0];
            rookie.PlayerPotential.THROW_ACCURACY = rookrate[1];
            #endregion            
            
            #region Throw Power            
            GetSkills(rookie, Trait.THP);
            rookie.Original_Ratings.THROW_POWER = rookrate[0];
            rookie.PlayerPotential.THROW_POWER = rookrate[1];
            #endregion
            
            #region Awareness
            GetSkills(rookie, Trait.AWR);            
            rookie.Original_Ratings.AWARENESS = rookrate[0];
            rookie.PlayerPotential.AWARENESS = rookrate[1];
            #endregion

            #region Speed
            GetSkills(rookie, Trait.SPD);
            rookie.Original_Ratings.SPEED = rookrate[0];
            rookie.PlayerPotential.SPEED = rookrate[1];            
            #endregion            

            #region Break Tackle            
            GetSkills(rookie, Trait.BTK);
            rookie.Original_Ratings.BREAK_TACKLE = rookrate[0];
            rookie.PlayerPotential.BREAK_TACKLE = rookrate[1];
            #endregion

            

        }

        public void RandomHeightWeight(Player player)
        {
            int height_low = 3;     // 5'8"
            int height_high = 17;   // 6'10"
            int weight_low = 0;     // 160#
            int weight_high = 255;  // 415#

            switch (player.Original_Ratings.POSITION_ID)
            {                
                case 0 :
                    {
                        height_low = 6;
                        height_high = 13;
                        weight_low = 30;
                        weight_high = 115;
                    }
                    break;

            }

            player.Original_Ratings.HEIGHT = GetRandomInt(1, 1000001) / 58000;
            if (player.Original_Ratings.HEIGHT < height_low)
                player.Original_Ratings.HEIGHT = height_low;
            if (player.Original_Ratings.HEIGHT > height_high)
                player.Original_Ratings.HEIGHT = height_high;
            player.Original_Ratings.WEIGHT = GetRandomInt(1, 1000001) / 2400;
            if (player.Original_Ratings.WEIGHT < weight_low)
                player.Original_Ratings.WEIGHT = weight_low;
            if (player.Original_Ratings.WEIGHT > weight_high)
                player.Original_Ratings.WEIGHT = weight_high;
        }
        */
        
        #endregion

    }
}
