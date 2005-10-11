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
	class GameOptionRecord : TableRecordModel
	{
		public const string INGAME_INJURY = "INGI";
		public const string SIM_INJURY = "SIMI";

		public GameOptionRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
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
	}
}
