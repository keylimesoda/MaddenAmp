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

namespace MaddenEditor.Core.Record
{
	public class OwnerRecord : TableRecordModel
	{
		public const string TEAM_ID = "TGID";
		public const string USER_CONTROLLED = "CFUC";
		public const string RELOCATION_YEAR = "OFYR";
		public const string DRAFT_PLAYERS = "CFDA";
		public const string SIGN_FREE_AGENTS = "CFFA";
		public const string SIGN_DRAFT_PICKS = "CFDP";
		public const string RESIGN_PLAYERS = "CFRP";
		public const string FILL_ROSTERS = "CFFR";
		public const string REORDER_DEPTH_CHARTS = "CFRR";
		public const string COMPUTER_CONTROL_7 = "CFEX";

		public OwnerRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
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
				return editorModel.TeamModel.GetTeamNameFromTeamId(TeamId);
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

		public bool DraftPlayers
		{
			get
			{
				return (GetIntField(DRAFT_PLAYERS) == 1);
			}
			set
			{
				SetField(DRAFT_PLAYERS, Convert.ToInt32(value));
			}
		}

		public bool SignFreeAgents
		{
			get
			{
				return (GetIntField(SIGN_FREE_AGENTS) == 1);
			}
			set
			{
				SetField(SIGN_FREE_AGENTS, Convert.ToInt32(value));
			}
		}

		public bool SignDraftPicks
		{
			get
			{
				return (GetIntField(SIGN_DRAFT_PICKS) == 1);
			}
			set
			{
				SetField(SIGN_DRAFT_PICKS, Convert.ToInt32(value));
			}
		}

		public bool ResignPlayers
		{
			get
			{
				return (GetIntField(RESIGN_PLAYERS) == 1);
			}
			set
			{
				SetField(RESIGN_PLAYERS, Convert.ToInt32(value));
			}
		}

		public bool FillRosters
		{
			get
			{
				return (GetIntField(FILL_ROSTERS) == 1);
			}
			set
			{
				SetField(FILL_ROSTERS, Convert.ToInt32(value));
			}
		}

		public bool ReorderDepthCharts
		{
			get
			{
				return (GetIntField(REORDER_DEPTH_CHARTS) == 1);
			}
			set
			{
				SetField(REORDER_DEPTH_CHARTS, Convert.ToInt32(value));
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
