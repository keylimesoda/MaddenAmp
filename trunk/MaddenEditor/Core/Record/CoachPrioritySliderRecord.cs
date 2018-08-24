/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
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
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{
	public class CoachPrioritySliderRecord : TableRecordModel
	{
		// CPSE

        public const string COACH_ID = "CCID";
		public const string PRIORITY = "PPIM";
		public const string POSITION_ID = "PDRP";
		public const string PRIORITY_TYPE = "CDPT";

		public const int NUMBER_OF_COACHING_POSITIONS = 17;

		public CoachPrioritySliderRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        #region Get/Set
        public int CoachId
		{
			get
			{
				return GetIntField(COACH_ID);
			}
			set
			{
				SetField(COACH_ID, value);
			}
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

		public int Priority
		{
			get
			{
				return GetIntField(PRIORITY);
			}
			set
			{
				SetField(PRIORITY, value);
			}
		}

		public int PriorityType
		{
			get
			{
				return GetIntField(PRIORITY_TYPE);
			}
			set
			{
				SetField(PRIORITY_TYPE, value);
			}
        }

        #endregion
    }
}
