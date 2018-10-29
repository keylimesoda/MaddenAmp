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
    public class PlayerRegression : TableRecordModel
    {
        public const string DRAFTED_POSITION = "PDRP";
        public const string ACCELERATION = "PGAC";
        public const string AGILITY = "PGAG";
        public const string INJURY = "PGIN";
        public const string JUMPING = "PGJU";
        public const string KICK_POWER = "PGKP";
        public const string STAMINA = "PGSM";
        public const string SPEED = "PGSP";
        public const string STRENGTH = "PGST";
        public const string THROW_POWER = "PGTP";
        public const string PROGRESSION_GRADE = "PRGR";
        public const string YEARS_PRO = "PYRP";

        public PlayerRegression(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
		}

        public int DraftedPosition
        {
            get { return GetIntField(DRAFTED_POSITION); }
            set { SetField(DRAFTED_POSITION, value); }
        }
        public int ACC
        {
            get { return GetIntField(ACCELERATION); }
            set { SetField(ACCELERATION, value); }
        }
        public int AGI
        {
            get { return GetIntField(AGILITY); }
            set { SetField(AGILITY, value); }
        }
        public int INJ
        {
            get { return GetIntField(INJURY); }
            set { SetField(INJURY, value); }
        }
        public int JMP
        {
            get { return GetIntField(JUMPING); }
            set { SetField(JUMPING, value); }
        }
        public int KPW
        {
            get { return GetIntField(KICK_POWER); }
            set { SetField(KICK_POWER, value); }
        }
        public int STA
        {
            get { return GetIntField(STAMINA); }
            set { SetField(STAMINA, value); }
        }
        public int SPD
        {
            get { return GetIntField(SPEED); }
            set { SetField(SPEED, value); }
        }
        public int STR
        {
            get { return GetIntField(STRENGTH); }
            set { SetField(STRENGTH, value); }
        }
        public int THP
        {
            get { return GetIntField(THROW_POWER); }
            set { SetField(THROW_POWER, value); }
        }
        public int ProgressionGrade
        {
            get { return GetIntField(PROGRESSION_GRADE); }
            set { SetField(PROGRESSION_GRADE, value); }
        }
        public int YearsPro
        {
            get { return GetIntField(YEARS_PRO); }
            set { SetField(YEARS_PRO, value); }
        }

    }
}
