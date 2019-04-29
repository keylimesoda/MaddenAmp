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

namespace MaddenEditor.Core.Record
{
    public class PRDF : TableRecordModel
    {
        public const string CAREER_FIELD = "CLRF";        
        public const string SEASON_VALUE = "STVL";
        public const string THROW_ACCURACY = "PTHA";
        public const string KICK_ACCURACY = "PKAC";
        public const string CHANGE_PERC = "pXPC";
        public const string SPEED = "PSPD";
        public const string ROLE = "ROLE";
        public const string TOUGHNESS = "PTGH";
        public const string CATCHING = "PCTH";
        public const string AGILITY = "PAGI";
        public const string POSITION_ID = "PPOS";




        public const string DEPTH_ORDER = "ddep";

        public PRDF(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
		}

        public int CareerField
        {
            get { return GetIntField(CAREER_FIELD); }
            set { SetField(CAREER_FIELD, value); }
        }
        
        public int SeasonValue
        {
            get { return GetIntField(SEASON_VALUE); }
            set { SetField(SEASON_VALUE, value); } 
        }
        public int ThrowAccuracy
        {
            get
            {
                return GetIntField(THROW_ACCURACY);
            }
            set
            {
                SetField(THROW_ACCURACY, value);
            }
        }
        public int KickAccuracy
        {
            get
            {
                return GetIntField(KICK_ACCURACY);
            }
            set
            {
                SetField(KICK_ACCURACY, value);
            }
        }
        public int ChangePerc
        {
            get { return GetIntField(CHANGE_PERC); }
            set { SetField(CHANGE_PERC, value); }
        }
        public int Speed
        {
            get { return GetIntField(SPEED); }
            set { SetField(SPEED, value); }
        }
        public int Role
        {
            get { return GetIntField(ROLE); }
            set { SetField(ROLE, value); }
        }
        public int Toughness
        {
            get
            {
                return GetIntField(TOUGHNESS);
            }
            set
            {
                SetField(TOUGHNESS, value);
            }
        }
        public int Catching
        {
            get
            {
                return GetIntField(CATCHING);
            }
            set
            {
                SetField(CATCHING, value);
            }
        }
        public int Agility
        {
            get { return GetIntField(AGILITY); }
            set { SetField(AGILITY, value); }
        }
        
        
        public int PositionId
        {
            get
            {
                return GetIntField(POSITION_ID);
            }
            set
            {
                SetField(POSITION_ID, value);
            }
        }



        public string Career_Fieldname
        {
            get
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = BitConverter.GetBytes(CareerField);
                return a.GetString(ba);
            }
            set
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = a.GetBytes(value);
                CareerField = BitConverter.ToInt32(ba, 0);
            }
        }

        public int DepthOrder
        {
            get { return GetIntField(DEPTH_ORDER); }
            set { SetField(DEPTH_ORDER, value); }
        }


    }
}
