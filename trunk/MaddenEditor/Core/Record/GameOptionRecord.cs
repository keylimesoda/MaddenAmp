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
	public class GameOptionRecord : TableRecordModel
	{
		public const string INGAME_INJURY = "INGI";
        
        public const string CPU_QB_ACC = "OAAC";
        public const string CPU_DEF_AWR = "OAAW";
        public const string CPU_BRK_BLOCK = "OADB";
        public const string CPU_DEF_KNOCKDOWNS = "OADK";
        public const string CPU_FG_ACCURACY = "OAFA";
        public const string CPU_FG_LENGTH = "OAFL";
        public const string CPU_INTS = "OAIN";
        public const string CPU_KO_LENGTH = "OAKL";
        public const string CPU_PASS_BLOCK = "OAOP";
        public const string CPU_PUNT_ACC = "OAPA";
        public const string CPU_PUNT_LENGTH = "OAPL";
        public const string CPU_RB_ABILITY = "OARA";
        public const string CPU_RUN_BLOCK = "OARB";
        public const string CPU_CATCHING = "OARC";
        public const string CPU_TACKLE = "OATA";

        public const string FAIR_PLAY = "OFAP";
        public const string STATS_DIR = "OFRD";
        public const string SALARY_CAP = "OFSC";
        public const string TRADE_DEADLINE = "OFTD";

        public const string HUM_QB_ACC = "OHAC";
        public const string HUM_DEF_AWR = "OHAW";
        public const string HUM_BRK_BLOCK = "OHDB";
        public const string HUM_DEF_KNOCKDOWNS = "OHDK";
        public const string HUM_FG_ACCURACY = "OHFA";
        public const string HUM_FG_LENGTH = "OHFL";
        public const string HUM_INTS = "OHIN";
        public const string HUM_KO_LENGTH = "OHKL";
        public const string HUM_PASS_BLOCK = "OHOP";
        public const string HUM_PUNT_ACC = "OHPA";
        public const string HUM_PUNT_LENGTH = "OHPL";
        public const string HUM_RB_ABILITY = "OHRA";
        public const string HUM_RUN_BLOCK = "OHRB";
        public const string HUM_CATCHING = "OHRC";
        public const string HUM_TACKLE = "OHTA";

        public const string OWNER_MODE = "OOWN";

        public const string PEN_CLIPPING = "OPCL";
        public const string PEN_DEF_PASS_INT = "OPDP";
        public const string PEN_FACEMASK = "OPFM";
        public const string PEN_FALSE_START = "OPFS";
        public const string PEN_HOLDING = "OPHO";
        public const string PEN_GROUNDING = "OPIG";
        public const string PLAY_MODE = "OPLM";
        public const string PEN_OFF_PASS_INT = "OPOP";
        public const string PEN_PK_INT = "OPPI";
        public const string PEN_ROUGH_KICKER = "OPRK";
        public const string PEN_ROUGH_PASSER = "OPRP";

        public const string QUARTER_LENGTH = "OQLN";



        public const string CAP_PENALTY = "OSCP";
        public const string SKILL_LEVEL = "OSLE";
        public const string RIGHT_CLICK_STATS_DIR = "OSRD";
		public const string SIM_INJURY = "SIMI";
		
		
		
		

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
