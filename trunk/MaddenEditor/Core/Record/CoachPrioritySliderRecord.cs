/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{
	public enum CoachSliderPlayerPositions
	{
		QB = 0,
		HB = 1,
		FB = 2,
		WR = 3,
		TE = 4,
		T = 5,
		G = 6,
		C = 7,
		DE = 8,
		DT = 9,
		OLB = 10,
		MLB = 11,
		CB = 12,
		FS = 13,
		SS = 14,
		K = 15,
		P = 16
	}

	public class CoachPrioritySliderRecord : TableRecordModel
	{
		public const string COACH_ID = "CCID";
		public const string PRIORITY = "PPIM";
		public const string POSITION_ID = "PDRP";
		public const string PRIORITY_TYPE = "CDPT";

		public CoachPrioritySliderRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

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
	}
}
