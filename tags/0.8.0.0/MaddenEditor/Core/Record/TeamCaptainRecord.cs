using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{
	public class TeamCaptainRecord : TableRecordModel
	{
		public const string CAPTAIN_1 = "CPT1";
		public const string CAPTAIN_2 = "CPT2";
		public const string CAPTAIN_3 = "CPT3";
		public const string TEAM_ID = "TGID";

		public TeamCaptainRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public int Captain1
		{
			get
			{
				return GetIntField(CAPTAIN_1);
			}
			set
			{
				SetField(CAPTAIN_1, value);
			}
		}

		public int Captain2
		{
			get
			{
				return GetIntField(CAPTAIN_2);
			}
			set
			{
				SetField(CAPTAIN_2, value);
			}
		}

		public int Captain3
		{
			get
			{
				return GetIntField(CAPTAIN_3);
			}
			set
			{
				SetField(CAPTAIN_3, value);
			}
		}

		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}
	}
}
