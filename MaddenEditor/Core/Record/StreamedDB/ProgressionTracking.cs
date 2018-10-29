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
    public class ProgressionTracking : TableRecordModel
    {
        public const string PROGRESSION_PERIOD = "PPRD";    // 2005-2006
        public const string PSIT = "PSIT";                  // 2005-2006
        public const string PROGRESSION_GROUP = "PRGR";     // 2007-2008        
        public const string STAT_POINTS = "PSTP";
        public const string STAT_TYPE = "PSTT";

        public ProgressionTracking(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
		}

        public int ProgressionPeriod
        {
            get { return GetIntField(PROGRESSION_PERIOD); }
            set { SetField(PROGRESSION_PERIOD, value); }
        }
        public int Group
        {
            get { return GetIntField(PROGRESSION_GROUP); }
            set { SetField(PROGRESSION_GROUP, value); }
        }
        public int Psit
        {
            get { return GetIntField(PSIT); }
            set { SetField(PSIT, value); }
        }
        public int Points
        {
            get { return GetIntField(STAT_POINTS); }
            set { SetField(STAT_POINTS, value); }
        }
        public int Type
        {
            get { return GetIntField(STAT_TYPE); }
            set { SetField(STAT_TYPE, value); }
        }
    }
}
