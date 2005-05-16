using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
	public class OwnerRecord : TableRecordModel
	{
		public const string TEAM_ID = "TGID";
		public const string USER_CONTROLLED = "CFUC";

		public OwnerRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public override string ToString()
		{
			return TeamName;
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

		public string TeamName
		{
			get
			{
				return parentModel.TeamModel.GetTeamNameFromTeamId(TeamId);
			}
		}

		public bool UserControlled
		{
			get
			{
				return (GetIntField(USER_CONTROLLED) == 1);
			}
			set
			{
				SetField(USER_CONTROLLED, Convert.ToInt32(value));
			}
		}
	}
}
