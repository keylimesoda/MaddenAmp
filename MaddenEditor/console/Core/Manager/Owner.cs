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
using System.Linq;
using System.Text;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.FranchiseState;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Core.Manager
{
    public class Owner
    {
        public CoachRatings ratings;

        public int Ego = 25;
        public int Spending = 50;
        public int Loyalty = 50;
        public int Risk = 50;
        public int Patience = 50;
        public int FreeAgent_Eval = 50;
        public int Draft_Eval = 50;

        public string name = "";

        public Owner()
        {
            ratings = new CoachRatings();
        }
        public double GetPositionRating(int pos)
        {
            if (pos == (int)MaddenPositions.QB)
            {
                return (double)ratings.QB_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.HB)
            {
                return (double)ratings.RB_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.WR)
            {
                return (double)ratings.WR_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.FB)
            {
                double rating = ((ratings.OL_RATING * 3) + (ratings.RB_RATING) + (ratings.WR_RATING * 2)) / 6;
                return rating * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.TE)
            {
                double rating = ((ratings.OL_RATING * 2) + (ratings.WR_RATING * 3)) / 5;
                return rating * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.C || pos == (int)MaddenPositions.LG || pos == (int)MaddenPositions.LT || pos == (int)MaddenPositions.RG || pos == (int)MaddenPositions.RT)
            {
                return (double)ratings.OL_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.DT || pos == (int)MaddenPositions.LE || pos == (int)MaddenPositions.RE)
            {
                return (double)ratings.DL_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.LOLB || pos == (int)MaddenPositions.MLB || pos == (int)MaddenPositions.ROLB)
            {
                return (double)ratings.LB_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.SS || pos == (int)MaddenPositions.FS)
            {
                return (double)ratings.S_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.CB)
            {
                return (double)ratings.DB_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.K)
            {
                return (double)ratings.KICK_RATING * Ego / 100;
            }
            else if (pos == (int)MaddenPositions.P)
            {
                return (double)ratings.PUNT_RATING * Ego / 100;
            }

            else return 0;
        }

        public void Read(BinaryReader binreader)
        {
            ratings.Read(binreader);

            Ego = binreader.ReadByte();
            Spending = binreader.ReadByte();
            Loyalty = binreader.ReadByte();
            Risk = binreader.ReadByte();
            Patience = binreader.ReadByte();
            FreeAgent_Eval = binreader.ReadByte();
            Draft_Eval = binreader.ReadByte();
            name = binreader.ReadString();
        }

        public void Write(BinaryWriter binwriter)
        {
            ratings.Write(binwriter);

            binwriter.Write((byte)Ego);
            binwriter.Write((byte)Spending);
            binwriter.Write((byte)Loyalty);
            binwriter.Write((byte)Risk);
            binwriter.Write((byte)Patience);
            binwriter.Write((byte)FreeAgent_Eval);
            binwriter.Write((byte)Draft_Eval);
            binwriter.Write(name);
        }

    }
}
