/******************************************************************************
 * Madden 2005 Editor
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
 * http://gommo.homelinux.net             colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Domain
{
	/// <summary>
	/// A Table Model supports adding fields to the model
	/// </summary>
	public class TableRecordModel
	{
		protected int recordNumber = -1;
		protected Dictionary<string, int> intFields = null;
		protected Dictionary<string, string> stringFields = null;				

		public TableRecordModel(int recordNumber)
		{
			this.recordNumber = recordNumber;
			intFields = new Dictionary<string, int>();
			stringFields = new Dictionary<string, string>();
		}

		public void SetField(string fieldName, string val)
		{
			if (stringFields.ContainsKey(fieldName))
			{
				stringFields[fieldName] = val;
			}
			else
			{
				stringFields.Add(fieldName, val);
			}
			
		}

		public void SetField(string fieldName, int val)
		{
			if (intFields.ContainsKey(fieldName))
			{
				intFields[fieldName] = val;
			}
			else
			{
				intFields.Add(fieldName, val);
			}
		}
	}
}
