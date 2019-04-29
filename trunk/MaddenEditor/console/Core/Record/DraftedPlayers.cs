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

namespace MaddenEditor.Core.Record
{
    public class DraftedPlayers: TableRecordModel
    {
        // table name "DRPL"

        public const string DRAFT_GPO = "DGPO";                 //2005+                 
        public const string DRAFT_GENERAL_RATING = "DGRA";      //2005+
        public const string DRAFT_PJR = "DPJR";                 //2005+  4
        public const string DRAFT_PICK_NUMBER = "DPNM";         //  511 NOT YET TAKEN
        public const string DRAFT_PICK_ROUND = "DRPR";          // 6 or 15
        public const string DRAFT_PICK_TEAM = "DRTM";
        public const string PLAYER_DRAFTED_POSITION = "PDRP";
        public const string PLAYER_ID = "PGID";
        public const string PLAYER_OVERALL = "POVR";
        public const string PLAYER_POSITION = "PPOS";           
        public const string PLAYER_TENDENCY = "PTEN";
        public const string PLAYER_WEIGHTED_RATING = "PWRA";    // some type of rating

        #region v2004 only fields
        public const string PLAYER_ACCELERATION = "PACC";
        public const string PLAYER_AGE = "PAGE";
        public const string PLAYER_AGILITY = "PAGI";
        public const string PLAYER_AWARENESS = "PAWR";
        public const string PLAYER_BREAK_TACKLE = "PBTK";
        public const string PLAYER_CARRY = "PCAR";
        public const string PLAYER_CPH = "PCPH";
        public const string PLAYER_CATCH = "PCTH";
        public const string PLAYER_INJURY = "PINJ";
        public const string PLAYER_JUMP = "PJMP";
        public const string PLAYER_KICK_ACCURACY = "PKAC";
        public const string PLAYER_KICK_POWER = "PKPR";
        public const string PLAYER_KICK_RETURN = "PKRT";
        public const string PLAYER_PASS_BLOCK = "PPBK";
        public const string PLAYER_RUN_BLOCK = "PRBK";
        public const string PLAYER_SPEED = "PSPD";
        public const string PLAYER_STAMINA = "PSTA";
        public const string PLAYER_STRENGTH = "PSTR";
        public const string PLAYER_TACKLE = "PTAK";
        public const string PLAYER_TOUGHNESS = "PTGH";
        public const string PLAYER_THROW_ACCURACY = "PTHA";
        public const string PLAYER_THROW_POWER = "PTHP";        
        public const string PLAYER_YEARS_PRO = "PYRP";
        #endregion

        public DraftedPlayers(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        #region Get/Set
        public int DraftGPO
        {
            get { return GetIntField(DRAFT_GPO); }
            set { SetField(DRAFT_GPO, value); }
        }
        public int DraftGeneralRating
        {
            get { return GetIntField(DRAFT_GENERAL_RATING); }
            set { SetField(DRAFT_GENERAL_RATING, value); }
        }
        public int DraftPJR
        {
            get { return GetIntField(DRAFT_PJR); }
            set { SetField(DRAFT_PJR, value); }
        }
        public int DraftPickNumber
        {
            get { return GetIntField(DRAFT_PICK_NUMBER); }
            set { SetField(DRAFT_PICK_NUMBER, value); }
        }
        public int DraftPickRound
        {
            get { return GetIntField(DRAFT_PICK_ROUND); }
            set { SetField(DRAFT_PICK_ROUND, value); }
        }
        public int DraftPickTeam
        {
            get { return GetIntField(DRAFT_PICK_TEAM); }
            set { SetField(DRAFT_PICK_TEAM, value); }
        }
        public int PlayerDraftedPosition
        {
            get { return GetIntField(PLAYER_DRAFTED_POSITION); }
            set { SetField(PLAYER_DRAFTED_POSITION, value); }
        }
        public int PlayerID
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int PlayerOverall
        {
            get { return GetIntField(PLAYER_OVERALL); }
            set { SetField(PLAYER_OVERALL, value); }
        }
        public int PlayerPosition
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        }
        public int PlayerTendency
        {
            get { return GetIntField(PLAYER_TENDENCY); }
            set { SetField(PLAYER_TENDENCY, value); }
        }
        public int PlayerWeightedRating
        {
            get { return GetIntField(PLAYER_WEIGHTED_RATING); }
            set { SetField(PLAYER_WEIGHTED_RATING, value); }
        }
        
        #endregion
    }
}
