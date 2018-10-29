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

using MaddenEditor.Core;


namespace MaddenEditor.Core
{
    public class OverallRecord : TableRecordModel
    {
        
        public const string DEFINED_HIGH = "PRDH";
        public const string DEFINED_LOW = "PRDL";
        public const string PLAYER_CATCH = "PCAW";
        public const string PLAYER_KICK_ACCURACY = "PKAW";
        public const string PLAYER_THROW_ACCURACY = "PTAW";
        public const string PLAYER_PASS_BLOCK = "PPBW";
        public const string PLAYER_RUN_BLOCK = "PRBW";
        public const string PLAYER_ACCELERATION = "PACW";
        public const string PLAYER_TACKLE = "PTCW";
        public const string PLAYER_SPEED = "PSEW";
        public const string PLAYER_AGILITY = "PAGW";
        public const string PLAYER_INJURY = "PINW";
        public const string PLAYER_KICK_POWER = "PKPW";
        public const string PLAYER_THROW_POWER = "PTPW";
        public const string PLAYER_CARRY = "PCRW";
        public const string PLAYER_KICK_RETURN = "PKRW";
        public const string PLAYER_TYPE = "PLTY";               //2019
        public const string PLAYER_BREAK_TACKLE = "PBTW";
        public const string PLAYER_STRENGTH = "PSTW";
        public const string PLAYER_JUMP = "PJUW";
        public const string PLAYER_AWARENESS = "PAWW";
        public const string POSITION = "PPOS";


        public OverallRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public float RatingHigh
        {
            get { return GetFloatField(DEFINED_HIGH); }
            set { SetField(DEFINED_HIGH, value); }
        }
        public float RatingLow
        {
            get { return GetFloatField(DEFINED_LOW); }
            set { SetField(DEFINED_LOW, value); }
        }
        public float Catch
        {
            get { return GetFloatField(PLAYER_CATCH); }
            set { SetField(PLAYER_CATCH, value); }
        }
        public float KickAccuracy
        {
            get { return GetFloatField(PLAYER_KICK_ACCURACY); }
            set { SetField(PLAYER_KICK_ACCURACY, value); }
        }
        public float ThrowAccuracy
        {
            get { return GetFloatField(PLAYER_THROW_ACCURACY); }
            set { SetField(PLAYER_THROW_ACCURACY, value); }
        }
        public float PassBlock
        {
            get { return GetFloatField(PLAYER_PASS_BLOCK); }
            set { SetField(PLAYER_PASS_BLOCK, value); }
        }
        public float RunBlock
        {
            get { return GetFloatField(PLAYER_RUN_BLOCK); }
            set { SetField(PLAYER_RUN_BLOCK, value); }
        }
        public float Acceleration
        {
            get { return GetFloatField(PLAYER_ACCELERATION); }
            set { SetField(PLAYER_ACCELERATION, value); }
        }
        public float Tackle
        {
            get { return GetFloatField(PLAYER_TACKLE); }
            set { SetField(PLAYER_TACKLE, value); }
        }
        public float Speed
        {
            get { return GetFloatField(PLAYER_SPEED); }
            set { SetField(PLAYER_SPEED, value); }
        }
        public float Agility
        {
            get { return GetFloatField(PLAYER_AGILITY); }
            set { SetField(PLAYER_AGILITY, value); }
        }
        public float Injury
        {
            get { return GetFloatField(PLAYER_INJURY); }
            set { SetField(PLAYER_INJURY, value); }
        }
        public float KickPower
        {
            get { return GetFloatField(PLAYER_KICK_POWER); }
            set { SetField(PLAYER_KICK_POWER, value); }
        }
        public float ThrowPower
        {
            get { return GetFloatField(PLAYER_THROW_POWER); }
            set { SetField(PLAYER_THROW_POWER, value); }
        }
        public float Carry
        {
            get { return GetFloatField(PLAYER_CARRY); }
            set { SetField(PLAYER_CARRY, value); }
        }
        public float KickReturn
        {
            get { return GetFloatField(PLAYER_KICK_RETURN); }
            set { SetField(PLAYER_KICK_RETURN, value); }
        }
        public float BreakTackle
        {
            get { return GetFloatField(PLAYER_BREAK_TACKLE); }
            set { SetField(PLAYER_BREAK_TACKLE, value); }
        }
        public float Strength
        {
            get { return GetFloatField(PLAYER_STRENGTH); }
            set { SetField(PLAYER_STRENGTH, value); }
        }
        public float Jump
        {
            get { return GetFloatField(PLAYER_JUMP); }
            set { SetField(PLAYER_JUMP, value); }
        }
        public float Awareness
        {
            get { return GetFloatField(PLAYER_AWARENESS); }
            set { SetField(PLAYER_AWARENESS, value); }
        }
        
        public int Position
        {
            get { return GetIntField(POSITION); }
            set { SetField(POSITION, value); }
        }

        public decimal GetTotalWeight()
        {
            float total = Catch + KickAccuracy + ThrowAccuracy + PassBlock + RunBlock + Acceleration + Tackle + Speed + Agility + KickPower
                + ThrowPower + Carry + KickReturn + BreakTackle + Strength + Jump + Awareness;
            return (decimal)total;
        }
       
    }
}
