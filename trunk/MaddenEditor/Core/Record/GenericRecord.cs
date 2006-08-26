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
	/// <summary>
	/// This class describes an object ideal to be placed in a combo box.
	/// The class has a descriptive string to display in the box as well
	/// as an ID that can be used to identify itself
	/// </summary>
	public class GenericRecord
	{
		private string name;
		private int id;

		public GenericRecord(string name, int id)
		{
			this.name = name;
			this.id = id;
		}

		public override string ToString()
		{
			return this.name;
		}

		public int Id
		{
			get
			{
				return this.id;
			}
		}
	}
}
