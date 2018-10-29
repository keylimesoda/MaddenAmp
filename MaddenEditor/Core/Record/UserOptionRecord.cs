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
    public class UserOptionRecord : TableRecordModel
    {
        public const string AUTO_FLIP_DEF_PLAY = "AFDP";        //2019
        public const string DEF_AUTO_STRAFE = "OATE";           //2019
        public const string BC_SPECIAL_MOVE = "OABM";           //2019
        public const string AUTO_SUB_IN = "OAFI";
        public const string AUTO_SUB_OUT = "OAFO";
        public const string COACH_MODE = "OAPS";                //2019
        public const string BALL_HAWK = "OBHK";                 //2019
        public const string HEAT_SEEK = "OHSK";                 //2019
        public const string PLAY_CALL_STYLE = "OPCS";           //2019
        public const string PASS_LEAD_SENSITIVITY = "OPLS";
        public const string SWITCH_ASSIST = "OSAS";             //2019
        public const string SKILL_LEVEL = "OSLE";               //2019
        public const string COIN_FLIP_FIRST = "UOCF";           //2019
        public const string COIN_FLIP_SECONDARY = "UOCS";       //2019

        public UserOptionRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public bool AutoFlipDefPlay
        {
            get { return (GetIntField(AUTO_FLIP_DEF_PLAY) == 1); }
            set { SetField(AUTO_FLIP_DEF_PLAY, (value ? 1 : 0)); }
        }
        public bool DefAutoStrafe
        {
            get { return (GetIntField(DEF_AUTO_STRAFE) == 1); }
            set { SetField(DEF_AUTO_STRAFE, (value ? 1 : 0)); }
        }
        
        public int BSSpecialMove
        {
            get { return GetIntField(BC_SPECIAL_MOVE); }
            set { SetField(BC_SPECIAL_MOVE, value); }
        }
        
        public int AutoSubIn
        {
            get { return GetIntField(AUTO_SUB_IN); }
            set { SetField(AUTO_SUB_IN, value); }
        }
        public int AutoSubOut
        {
            get { return GetIntField(AUTO_SUB_OUT); }
            set { SetField(AUTO_SUB_IN, value); }
        }
        public bool BallHawk
        {
            get { return (GetIntField(BALL_HAWK) == 1); }
            set { SetField(BALL_HAWK, (value ? 1 : 0)); }
        }
        public bool HeatSeek
        {
            get { return (GetIntField(HEAT_SEEK) == 1); }
            set { SetField(HEAT_SEEK, (value ? 1 : 0)); }
        }
        public int PassLead
        {
            get { return GetIntField(PASS_LEAD_SENSITIVITY); }
            set { SetField(PASS_LEAD_SENSITIVITY, value); }
        }
        public int SkillLevel
        {
            get { return GetIntField(SKILL_LEVEL);}
            set { SetField(SKILL_LEVEL,value);}
        }
        public bool SwitchAssist
        {
            get { return (GetIntField(SWITCH_ASSIST)==1); }
            set { SetField(PASS_LEAD_SENSITIVITY, (value ? 1:0)); }
        }
        public bool CoachMode
        {
            get { return (GetIntField(COACH_MODE) == 1); }
            set { SetField(COACH_MODE, (value ? 1 : 0)); }
        }
        public int CoinFlipFirst
        {
            get { return GetIntField(COIN_FLIP_FIRST); }
            set { SetField(COIN_FLIP_FIRST, value); }
        }
        public int CoinFlipSecondary
        {
            get { return GetIntField(COIN_FLIP_SECONDARY); }
            set { SetField(COIN_FLIP_SECONDARY, value); }
        }
        public int PlayCallStyle
        {
            get { return GetIntField(PLAY_CALL_STYLE); }
            set { SetField(PLAY_CALL_STYLE, value); }
        }

    }
}
