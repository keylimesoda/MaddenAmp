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
    public class RoleInfo : TableRecordModel
    {
        // Tablename = RINF in streameddata.db

        public const string ROLE_NAME = "RLNM";
        public const string ROLE_TEXT = "RLTX";
        public const string FREE_AGENT_ROLE = "RKFA";
        public const string GRAPHIC_SMALL_ID = "RLIC";
        public const string GRAPHIC_LARGE_ID = "RLIG";
        public const string ROLE_RANK = "RLRK";
        public const string PLAYER_ROLE = "PROL";
        public const string VALUE_EFFECT = "POPM";
        public const string ROLE_IS_TEAM_BASED = "RLAP";
        public const string ROLE_EFFECT_TYPE = "RMXP";
        public const string ROLE_IS_ACTIVE = "RLER";


        public RoleInfo(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public string RoleName
        {
            get { return GetStringField(ROLE_NAME); }
            set { SetField(ROLE_NAME, value); }
        }
        public string RoleText
        {
            get { return GetStringField(ROLE_TEXT); }
            set { SetField(ROLE_TEXT, value); }
        }
        public bool FreeAgentRole
        {
            get { return (GetIntField(FREE_AGENT_ROLE) == 1); }
            set { SetField(FREE_AGENT_ROLE, Convert.ToInt32(value)); }
        }
        public int GraphicSmall
        {
            get { return GetIntField(GRAPHIC_SMALL_ID); }
            set { SetField(GRAPHIC_SMALL_ID, value); }
        }
        public int GraphicLarge
        {
            get { return GetIntField(GRAPHIC_LARGE_ID); }
            set { SetField(GRAPHIC_LARGE_ID, value); }
        }
        public int RoleRank
        {
            get { return GetIntField(ROLE_RANK); }
            set { SetField(ROLE_RANK, value); }
        }
        public int PlayerRole
        {
            get { return GetIntField(PLAYER_ROLE); }
            set { SetField(PLAYER_ROLE, value); }
        }
        public int ValueEffect
        {
            get { return GetIntField(VALUE_EFFECT); }
            set { SetField(VALUE_EFFECT, value); }
        }
        public bool RoleIsTeamBased
        {
            get { return (GetIntField(ROLE_IS_TEAM_BASED) == 1); }
            set { SetField(ROLE_IS_TEAM_BASED, Convert.ToInt32(value)); }
        }
        public int RoleEffectType
        {
            get { return GetIntField(ROLE_EFFECT_TYPE); }
            set { SetField(ROLE_EFFECT_TYPE, value); }
        }
        public bool RoleActive
        {
            get { return (GetIntField(ROLE_IS_ACTIVE) == 1); }
            set { SetField(ROLE_IS_ACTIVE, Convert.ToInt32(value)); }
        }


    }
}
