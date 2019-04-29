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
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;

using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;


namespace MaddenEditor.Core.Record
{
    public class ProgressionSchedule : TableRecordModel
    {
        //  SCWE   2005-2008

        public const string PROG_PERIOD = "PPRD";              // progression point
        public const string PROGRESSION_APPLY = "PRAR";        // true, false
        public const string SEASON_TYPE = "SEST";              // 9 = preseason 0 = regular
        public const string SEASON_WEEK_NUMBER = "SEWN";

        public ProgressionSchedule(int record, TableModel tablemodel, EditorModel EditorModel)
            : base(record, tablemodel, EditorModel)
        {

        }

        #region Get/Set

        public int ProgressionPeriod
        {
            get { return GetIntField(PROG_PERIOD); }
            set { SetField(PROG_PERIOD, value); }            
        }

        public bool ProgressionActive
        {
            get { return GetIntField(PROGRESSION_APPLY) == 1; }
            set { SetField(PROGRESSION_APPLY, Convert.ToInt32(value)); }
        }

        public int SeasonType
        {
            get { return GetIntField(SEASON_TYPE); }
            set
            {
                if (value == 0) 
                    SetField(SEASON_TYPE, 0);
                else SetField(SEASON_TYPE, 9);
            }
        }

        public int WeekNum
        {
            get { return GetIntField(SEASON_WEEK_NUMBER); }
            set { SetField (SEASON_WEEK_NUMBER, value); }
        }

        #endregion


    }
}
