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
    public class PlayerProgression : TableRecordModel
    {
        // PWRA  Streameddata.db

        public const string CARRY = "PGCA";
        public const string KICK_ACCURACY = "PGKA";
        public const string STRENGTH = "PGSA";                    // HB and all defensive players?
        public const string THROW_ACCURACY = "PGTA";
        public const string PASS_BLOCK = "PGPB";
        public const string RUN_BLOCK = "PGRB";
        public const string TACKLE = "PGTK";
        public const string DRAFTED_POSITION = "PDRP";
        public const string PROGRESSION_GRADE = "PRGR";
        public const string KICK_RETURN = "PGKR";
        public const string BREAK_TACKLE = "PGBT";
        public const string CATCH = "PGCT";
        public const string PLAYER_EVAL = "PLEV";
        public const string AWARENESS = "PGAW";

        public PlayerProgression(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
		}

        #region Get/Set

        public int CAR
        {
            get { return GetIntField(CARRY); }
            set { SetField(CARRY, value); }
        }
        public int KAC
        {
            get { return GetIntField(KICK_ACCURACY); }
            set { SetField(KICK_ACCURACY, value); }
        }
        public int STR
        {
            get { return GetIntField(STRENGTH); }
            set { SetField(STRENGTH, value); }
        }
        public int THA
        {
            get { return GetIntField(THROW_ACCURACY); }
            set { SetField(THROW_ACCURACY, value); }
        }
        public int PBL
        {
            get { return GetIntField(PASS_BLOCK); }
            set { SetField(PASS_BLOCK, value); }
        }
        public int RBL
        {
            get { return GetIntField(RUN_BLOCK); }
            set { SetField(RUN_BLOCK, value); }
        }
        public int TAK
        {
            get { return GetIntField(TACKLE); }
            set { SetField(TACKLE, value); }
        }
        public int DraftedPos
        {
            get { return GetIntField(DRAFTED_POSITION); }
            set { SetField(DRAFTED_POSITION, value); }
        }
        public int ProgressionGrade
        {
            get { return GetIntField(PROGRESSION_GRADE); }
            set { SetField(PROGRESSION_GRADE, value); }
        }
        public int KRT
        {
            get { return GetIntField(KICK_RETURN); }
            set { SetField(KICK_RETURN, value); }
        }
        public int BTK
        {
            get { return GetIntField(BREAK_TACKLE); }
            set { SetField(BREAK_TACKLE, value); }
        }
        public int CAT
        {
            get { return GetIntField(CATCH); }
            set { SetField(CATCH, value); }
        }
        public int PlayerEval
        {
            get { return GetIntField(PLAYER_EVAL); }
            set { SetField(PLAYER_EVAL, value); }
        }

        public int AWR
        {
            get { return GetIntField(AWARENESS); }
            set { SetField(AWARENESS, value); }
        }

        #endregion
    }
}
