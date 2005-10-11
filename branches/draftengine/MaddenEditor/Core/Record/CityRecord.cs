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

namespace MaddenEditor.Core.Record
{
	public class CityRecord : TableRecordModel
	{
		public const string CITY_ID = "CYID";
		public const string CITY_NAME = "CYNM";

		public CityRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

		public string Name
		{
			get
			{
				return GetStringField(CITY_NAME);
			}
			set
			{
				SetField(CITY_NAME, value);
			}
		}

		public int CityId
		{
			get
			{
				return GetIntField(CITY_ID);
			}
			set
			{
				SetField(CITY_ID, value);
			}
		}
	}
}