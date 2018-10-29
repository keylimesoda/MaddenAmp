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
using System.Diagnostics;

namespace MaddenEditor.Core
{
	/// <summary>
	/// A Table Model supports adding fields to the model
    /// 
	/// </summary>
	public class TableRecordModel
	{
        public bool BigEndian = false;
        protected bool dirty = false;
		protected bool deleted = false;
		protected int recordNumber = -1;
		private Dictionary<string, int> intFields = null;
		private Dictionary<string, string> stringFields = null;
        private Dictionary<string, float> floatFields = null;

		private Dictionary<string, int> backupIntFields = null;
		private Dictionary<string, string> backupStringFields = null;
        private Dictionary<string, float> backupFloatFields = null;

		protected EditorModel editorModel = null;
        protected TableModel tableModel = null;

		public TableRecordModel(int recordNumber, TableModel tableModel, EditorModel editorModel)
		{
            this.BigEndian = editorModel.BigEndian;
            this.tableModel = tableModel;
            this.editorModel = editorModel;
			this.recordNumber = recordNumber;
			intFields = new Dictionary<string, int>();
			stringFields = new Dictionary<string, string>();
            floatFields = new Dictionary<string, float>();
			backupIntFields = new Dictionary<string, int>();
			backupStringFields = new Dictionary<string, string>();
            backupFloatFields = new Dictionary<string, float>();
		}

        public List<string> StringFields()
        {
            List<string> toReturn = new List<string>();
            foreach (string s in stringFields.Keys)
            {
                toReturn.Add(s);
            }
            return toReturn;
        }

        public List<string> IntFields()
        {
            List<string> toReturn = new List<string>();
            foreach (string s in intFields.Keys)
            {
                toReturn.Add(s);
            }
            return toReturn;
        }

        public List<string> FloatFields()
        {
            List<string> toReturn = new List<string>();
            foreach (string s in floatFields.Keys)
            {
                toReturn.Add(s);
            }
            return toReturn;
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

        public string ConvertBE(string name)
        {
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
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

        public void RegisterField(string fieldName, float val)
        {
            Debug.Assert(!floatFields.ContainsKey(fieldName), "Only use RegisterField to register the field and init value\r\nuse SetField to set values");
            floatFields.Add(fieldName, val);
        }

		public string GetStringField(string fieldName)
		{
			try
            {
                if (BigEndian)
                {
                    string rev = ConvertBE(fieldName);
                    return stringFields[rev];
                }
				return stringFields[fieldName];
			}
			catch(KeyNotFoundException err)
			{
				err = err;
				//Trace.WriteLine("Error Getting StringField " + fieldName + " :" + err.ToString());
				return "";
			}
		}

		public int GetIntField(string fieldName)
		{
			try
			{
				// reverse these for big endian
                if (BigEndian)
                {
                    string rev = ConvertBE(fieldName);
                    return intFields[rev];
                }
                else return intFields[fieldName];
			}
			catch (KeyNotFoundException err)
			{
				err = err;
				//Trace.WriteLine("Error Getting IntField " + fieldName + " :" + err.ToString());
				return 0;
			}
		}

        public float GetFloatField(string fieldName)
        {
            try
            {
                if (BigEndian)
                {
                    string rev = ConvertBE(fieldName);
                    return floatFields[rev];
                }
                return floatFields[fieldName];
            }
            catch (KeyNotFoundException err)
            {
                err = err;
                //Trace.WriteLine("Error Getting FloatField " + fieldName + " :" + err.ToString());
                return 0;
            }
        }

		public bool ContainsStringField(string fieldName)
		{
			if (stringFields.ContainsKey(fieldName))
				return true;

			return false;
		}

		public bool ContainsIntField(string fieldName)
		{
			if (intFields.ContainsKey(fieldName))
				return true;

			return false;
		}

        public bool ContainsFloatField(string fieldName)
        {
            if (floatFields.ContainsKey(fieldName))
                return true;
            return false;
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
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }

			if (stringFields[fieldName].Equals(val))
			{
				return;
			}
			//Mark this record as dirty as well as the Full Roster Model
			editorModel.Dirty = true;
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
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }
            
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
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }
            
            //Exit early if the new value is the same
			if (intFields[fieldName] == val)
			{
				return;
			}

			//Mark this record as dirty as well as the Full Roster Model
			editorModel.Dirty = true;
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
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }
            
            if (backup)
			{
				SetField(fieldName, val);
			}
			else
			{
				intFields[fieldName] = val;
			}
		}

        protected void SetField(string fieldName, float val)
        {
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }
            
            //Exit early if the new value is the same
            if (floatFields[fieldName] == val)
            {
                return;
            }

            //Mark this record as dirty as well as the Full Roster Model
            editorModel.Dirty = true;
            this.dirty = true;
            
            if (!backupFloatFields.ContainsKey(fieldName))
            {
                //Backup original value
                backupFloatFields.Add(fieldName, floatFields[fieldName]);
            }

            floatFields[fieldName] = val;
        }
        
        protected void SetField(string fieldName, float val, bool backup)
        {
            if (BigEndian)
            {
                string rev = ConvertBE(fieldName);
                fieldName = rev;
            }
            if (backup)
            {
                SetField(fieldName, val);
            }
            else
            {
                floatFields[fieldName] = val;
            }
        }

        public void SetFieldCSV(string fieldname, string csvstring)
        {
            if (BigEndian)
            {
                string rev = ConvertBE(fieldname);
                fieldname = rev;
            }

            if (stringFields.ContainsKey(fieldname))
            {
                if (!backupStringFields.ContainsKey(fieldname))
                {
                    //Backup original value
                    backupStringFields.Add(fieldname, stringFields[fieldname]);
                }
                stringFields[fieldname] = csvstring;
                editorModel.Dirty = true;
                this.dirty = true;
            }
            else if (intFields.ContainsKey(fieldname))
            {
                if (!backupIntFields.ContainsKey(fieldname))
                {
                    //Backup original value
                    backupIntFields.Add(fieldname, intFields[fieldname]);
                }
                intFields[fieldname] = Convert.ToInt32(csvstring);                
                editorModel.Dirty = true;
                this.dirty = true;
            }
            else if (floatFields.ContainsKey(fieldname))
            {
                if (!backupFloatFields.ContainsKey(fieldname))
                {
                    //Backup original value
                    backupFloatFields.Add(fieldname, floatFields[fieldname]);
                }
                floatFields[fieldname] = float.Parse(csvstring);
                editorModel.Dirty = true;
                this.dirty = true;
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

        public void GetChangedFloatFields(ref string[] keyArray, ref float[] valueArray)
        {
            keyArray = new string[backupFloatFields.Count];
            valueArray = new float[backupFloatFields.Count];

            int i = 0;
            foreach (string key in backupFloatFields.Keys)
            {
                keyArray[i] = key;
                valueArray[i] = floatFields[key];
                i++;
            }

        }
        
        public void DiscardBackups()
		{
			backupStringFields.Clear();
			backupIntFields.Clear();
            backupFloatFields.Clear();

			Dirty = false;
		}

		public void SetDeleteFlag(bool flag)
		{
			// I think this should be alright.
			if (flag == deleted)
			{
				return;
			}

			deleted = flag;
			this.Dirty = true;
			editorModel.Dirty = true;
			tableModel.ProcessRecordDeleteness(this);
		}
	}
}