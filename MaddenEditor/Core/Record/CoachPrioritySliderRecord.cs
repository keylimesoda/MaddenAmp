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
				return intFields[COACH_ID];
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
				return intFields[POSITION_ID];
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
				return intFields[PRIORITY];
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
				return intFields[PRIORITY_TYPE];
			}
			set
			{
				SetField(PRIORITY_TYPE, value);
			}
		}
	}
}
