/******************************************************************************
 * Gommo's Madden Editor
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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
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
	public class GameOptionRecord : TableRecordModel
	{
		public const string INGAME_INJURY = "INGI";
		public const string SIM_INJURY = "SIMI";
		public const string CAP_PENALTY = "OSCP";
		public const string OWNER_MODE = "OOWN";
		public const string SALARY_CAP = "OFSC";
		public const string TRADE_DEADLINE = "OFTD";

		public GameOptionRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

		public int InGameInjury
		{
			set
			{
				SetField(INGAME_INJURY, value);
			}
			get
			{
				return GetIntField(INGAME_INJURY);
			}
		}

		public int SimInjury
		{
			set
			{
				SetField(SIM_INJURY, value);
			}
			get
			{
				return GetIntField(SIM_INJURY);
			}
		}

		public bool CapPenalty
		{
			set
			{
				SetField(CAP_PENALTY, (value ? 1 : 0));
			}
			get
			{
				return (GetIntField(CAP_PENALTY) == 1);
			}
		}

		public bool OwnerMode
		{
			set
			{
				SetField(OWNER_MODE, (value ? 1 : 0));
			}
			get
			{
				return (GetIntField(OWNER_MODE) == 1);
			}
		}

		public bool SalaryCap
		{
			set
			{
				SetField(SALARY_CAP, (value ? 1 : 0));
			}
			get
			{
				return (GetIntField(SALARY_CAP) == 1);
			}
		}

		public bool TradeDeadline
		{
			set
			{
				SetField(TRADE_DEADLINE, (value ? 1 : 0));
			}
			get
			{
				return (GetIntField(TRADE_DEADLINE) == 1);
			}
		}
	}
}
