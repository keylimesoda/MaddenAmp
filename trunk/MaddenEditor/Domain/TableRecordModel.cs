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
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
 * 
 * colin.goudie@gmail.com
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
		protected bool dirty = false;
		protected bool deleted = false;
		protected int recordNumber = -1;
		protected Dictionary<string, int> intFields = null;
		protected Dictionary<string, string> stringFields = null;

		protected Dictionary<string, int> backupIntFields = null;
		protected Dictionary<string, string> backupStringFields = null;

		protected RosterModel parentModel = null;

		public TableRecordModel(int recordNumber, RosterModel rosterModel)
		{
			parentModel = rosterModel;
			this.recordNumber = recordNumber;
			intFields = new Dictionary<string, int>();
			stringFields = new Dictionary<string, string>();
			backupIntFields = new Dictionary<string, int>();
			backupStringFields = new Dictionary<string, string>();
		}

		public int RecNo
		{
			get
			{
				return recordNumber;
			}
		}

		public bool Deleted
		{
			get
			{
				return deleted;
			}
		}

		public bool Dirty
		{
			get
			{
				return dirty;
			}
			set
			{
				dirty = value;
			}
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

		public string GetStringField(string fieldName)
		{
			return stringFields[fieldName];
		}

		public int GetIntField(string fieldName)
		{
			return intFields[fieldName];
		}

		protected void SetFieldWithBackup(string fieldName, string val)
		{
			//Exit early if the new value is the same
			if (stringFields[fieldName].Equals(val))
			{
				return;
			}
			//Mark this record as dirty as well as the Full Roster Model
			parentModel.Dirty = true;
			this.dirty = true;

			//If the string backup dictionary already contains a key for
			//this fieldName, then don't back up
			if (!backupStringFields.ContainsKey(fieldName))
			{
				//Backup original value
				backupStringFields.Add(fieldName, stringFields[fieldName]);
			}

			stringFields[fieldName] = val;
		}

		protected void SetFieldWithBackup(string fieldName, int val)
		{
			//Exit early if the new value is the same
			if (intFields[fieldName] == val)
			{
				return;
			}

			//Mark this record as dirty as well as the Full Roster Model
			parentModel.Dirty = true;
			this.dirty = true;

			//If the int backup dictionary already contains a key for
			//this fieldName, then don't back up
			if (!backupIntFields.ContainsKey(fieldName))
			{
				//Backup original value
				backupIntFields.Add(fieldName, intFields[fieldName]);
			}

			intFields[fieldName] = val;
		}

		public void GetChangedIntFields(ref string[] keyArray, ref int[] valueArray)
		{
			keyArray = new string[backupIntFields.Count];
			valueArray = new int[backupIntFields.Count];

			int i = 0;
			foreach (string key in backupIntFields.Keys)
			{
				keyArray[i] = key;
				valueArray[i] = intFields[key];
				i++;
			}

		}

		public void GetChangedStringFields(ref string[] keyArray, ref string[] valueArray)
		{
			keyArray = new string[backupStringFields.Count];
			valueArray = new string[backupStringFields.Count];

			int i = 0;
			foreach (string key in backupStringFields.Keys)
			{
				keyArray[i] = key;
				valueArray[i] = stringFields[key];
				i++;
			}
		}

		public void DiscardBackups()
		{
			backupStringFields.Clear();
			backupIntFields.Clear();

			Dirty = false;
		}

		public void SetDeleteFlag(bool flag)
		{
			deleted = flag;
			this.Dirty = true;
			parentModel.Dirty = true;
		}
	}
}