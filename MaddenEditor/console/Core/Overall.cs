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
        // entries as found in table PORC in dbtemplates
        public double RateHigh = 0;
        public double RateLow = 35;
        public double PCAW = 0;
        public double PKAW = 0;
        public double PTAW = 0;
        public double PPBW = 0;
        public double PRBW = 0;
        public double PACW = 0;
        public double PTCW = 0;
        public double PSEW = 0;
        public double PAGW = 0;
        public double PINW = 0;
        public double PKPW = 0;
        public double PTPW = 0;
        public double PCRW = 0;
        public double PKRW = 0;             //  kick return not used in any OVR for normal positions
        public double PBTW = 0;
        public double PSTW = 0;
        public double PJUW = 0;
        public double PAWW = 0;
        public int PPOS = 0;

        #endregion

        public ovrdef()
        {
            Ratings = new Dictionary<int, double>();            
            RateHigh = 0;
            RateLow = 35;
            PCAW = 0;
            PKAW = 0;
            PTAW = 0;
            PPBW = 0;
            PRBW = 0;
            PACW = 0;
            PTCW = 0;
            PSEW = 0;
            PAGW = 0;
            PINW = 0;
            PKPW = 0;
            PTPW = 0;
            PCRW = 0;
            PKRW = 0;
            PBTW = 0;
            PSTW = 0;
            PJUW = 0;
            PAWW = 0;
            PPOS = 0;
            totalweight = 0;
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
                        this.PAGW =.5;
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

            SetTotalWeight();
            SetRatings();
        }
        
        public ovrdef(OverallRecord play)
        {
            Ratings = new Dictionary<int, double>();

            RateHigh = play.RatingHigh;
            RateLow = play.RatingLow;
            PCAW = play.Catch;
            PKAW = play.KickAccuracy;
            PTAW = play.ThrowAccuracy;
            PPBW = play.PassBlock;
            PRBW = play.RunBlock;
            PACW = play.Acceleration;
            PTCW = play.Tackle;
            PSEW = play.Speed;
            PAGW = play.Agility;
            PINW = play.Injury;
            PKPW = play.KickPower;
            PTPW = play.ThrowPower;
            PCRW = play.Carry;
            PKRW = play.KickReturn;
            PBTW = play.BreakTackle;
            PSTW = play.Strength;
            PJUW = play.Jump;
            PAWW = play.Awareness;
            PPOS = play.Position;

            SetTotalWeight();
            SetRatings();
        }

        public void SetTotalWeight()
        {
            totalweight = PCAW + PKAW + PTAW + PPBW + PRBW + PACW + PTCW +
            PSEW + PAGW + PKPW + PTPW + PCRW + PKRW + PBTW + PSTW + PJUW + PAWW;            
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
    }
    
    
    
    public class Overall
    {
        public Dictionary<int, ovrdef> Table;
        
        public Overall()
        {
            Table = new Dictionary<int, ovrdef>();            
        }

        public void InitRatings(AmpConfig config)
        {            
            if (config.db_misc_model != null)
            {
                Table.Clear();
                foreach (TableRecordModel rec in config.db_misc_model.TableModels[EditorModel.PLAYER_OVERALL_CALC].GetRecords())
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

        public double GetOverall(PlayerRecord rec)
        {
            return Table[rec.PositionId].GetOverall(rec);
        }
        
        public double GetRatingOVR(PlayerRecord rec, int rating)
        {
            return Table[rec.PositionId].GetPerc(rating, rec.GetRating(rating));           
        }
        
        
    }
}
