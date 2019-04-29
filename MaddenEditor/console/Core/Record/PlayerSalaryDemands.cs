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
    public class PlayerSalaryDemands : TableRecordModel
    {
        public const string PLAYER_DRAFTED_POSITION = "PDRP";
        public const string BONUS = "SBID";
        public const string MINIMUM_BONUS_INCREASE = "SBLA";
        public const string MINIMUM_SALARY_INCREASE = "SBMA";
        public const string SFAT = "SFAT";                      // free agent tender?
        public const string INITIAL_DEMAND = "SIDO";
        public const string SALARY_MAX = "SMAD";
        public const string SALARY_MINIMUM = "SMID";
        public const string PLAYER_RATING = "SRRI";

        public PlayerSalaryDemands(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {

        }

        public int PlayerDraftedPosition
        {
            get { return GetIntField(PLAYER_DRAFTED_POSITION); }
            set { SetField(PLAYER_DRAFTED_POSITION, value); }
        }
        public int Bonus
        {
            get { return GetIntField(BONUS); }
            set { SetField(BONUS, value); }
        }
        public int MinimumBonusIncrease
        {
            get { return GetIntField(MINIMUM_BONUS_INCREASE); }
            set { SetField(MINIMUM_BONUS_INCREASE, value); }
        }
        public int MinimumSalaryIncrease
        {
            get { return GetIntField(MINIMUM_SALARY_INCREASE); }
            set { SetField(MINIMUM_SALARY_INCREASE, value); }
        }
        public int Sfat
        {
            get { return GetIntField(SFAT); }
            set { SetField(SFAT, value); }
        }
        public int InitialDemand
        {
            get { return GetIntField(INITIAL_DEMAND); }
            set { SetField(INITIAL_DEMAND, value); }
        }
        public int SalaryMax
        {
            get { return GetIntField(SALARY_MAX); }
            set { SetField(SALARY_MAX, value); }
        }
        public int SalaryMinimum
        {
            get { return GetIntField(SALARY_MINIMUM); }
            set { SetField(SALARY_MINIMUM, value); }
        }
        public int PlayerRating
        {
            get { return GetIntField(PLAYER_RATING); }
            set { SetField(PLAYER_RATING, value); }
        }


    }
}
