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
using System.Diagnostics;

namespace MaddenEditor.Core
{
	/// <summary>
	/// A Table Model supports adding fields to the model
	/// </summary>
	public class TableRecordModel
	{
		protected bool dirty = false;
		protected bool deleted = false;
		protected int recordNumber = -1;
		private Dictionary<string, int> intFields = null;
		private Dictionary<string, string> stringFields = null;

		private Dictionary<string, int> backupIntFields = null;
		private Dictionary<string, string> backupStringFields = null;

		protected EditorModel parentModel = null;

		public TableRecordModel(int recordNumber, EditorModel EditorModel)
		{
			parentModel = EditorModel;
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

		public void RegisterField(string fieldName, string val)
		{
			Debug.Assert(!stringFields.ContainsKey(fieldName), "Only use RegisterField to register the field and init value\r\nuse SetField to set values");
			
			stringFields.Add(fieldName, val);
		}

		public void RegisterField(string fieldName, int val)
		{
			Debug.Assert(!intFields.ContainsKey(fieldName), "Only use RegisterField to register the field and init value\r\nuse SetField to set values");

			intFields.Add(fieldName, val);
			
		}

		public string GetStringField(string fieldName)
		{
			try
			{
				return stringFields[fieldName];
			}
			catch(KeyNotFoundException err)
			{
				//Console.WriteLine("Error Getting StringField " + fieldName + " :" + err.ToString());
				return "";
			}
		}

		public int GetIntField(string fieldName)
		{
			try
			{
				return intFields[fieldName];
			}
			catch (KeyNotFoundException err)
			{
				//Console.WriteLine("Error Getting IntField " + fieldName + " :" + err.ToString());
				return 0;
			}
		}

		protected bool ContainsField(string fieldName)
		{
			if (intFields.ContainsKey(fieldName))
				return true;

			if (stringFields.ContainsKey(fieldName))
				return true;

			return false;
		}

		protected void SetField(string fieldName, string val)
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

		protected void SetField(string fieldName, string val, bool backup)
		{
			if (backup)
			{
				SetField(fieldName, val);
			}
			else
			{
				stringFields[fieldName] = val;
			}
		}

		protected void SetField(string fieldName, int val)
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

		protected void SetField(string fieldName, int val, bool backup)
		{
			if (backup)
			{
				SetField(fieldName, val);
			}
			else
			{
				intFields[fieldName] = val;
			}
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