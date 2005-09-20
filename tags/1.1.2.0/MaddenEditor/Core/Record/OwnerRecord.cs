/******************************************************************************
 * Gommo's Madden Editor
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
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
	public class OwnerRecord : TableRecordModel
	{
		public const string TEAM_ID = "TGID";
		public const string USER_CONTROLLED = "CFUC";
		public const string RELOCATION_YEAR = "OFYR";
		public const string COMPUTER_CONTROL_1 = "CFDA";
		public const string COMPUTER_CONTROL_2 = "CFFA";
		public const string COMPUTER_CONTROL_3 = "CFDP";
		public const string COMPUTER_CONTROL_4 = "CFRP";
		public const string COMPUTER_CONTROL_5 = "CFFR";
		public const string COMPUTER_CONTROL_6 = "CFRR";
		public const string COMPUTER_CONTROL_7 = "CFEX";

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

		public int RelocationYear
		{
			get
			{
				return GetIntField(RELOCATION_YEAR);
			}
			set
			{
				SetField(RELOCATION_YEAR, value);
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

		public bool ComputerControl1
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_1) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_1, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl2
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_2) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_2, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl3
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_3) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_3, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl4
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_4) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_4, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl5
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_5) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_5, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl6
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_6) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_6, Convert.ToInt32(value));
			}
		}

		public bool ComputerControl7
		{
			get
			{
				return (GetIntField(COMPUTER_CONTROL_7) == 1);
			}
			set
			{
				SetField(COMPUTER_CONTROL_7, Convert.ToInt32(value));
			}
		}
	}
}
