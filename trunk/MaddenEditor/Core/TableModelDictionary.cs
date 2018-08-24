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
using System.Threading;

namespace MaddenEditor.Core
{
	/// <summary>
	/// This class will override the Dictionary class to perform lazy loading of 
	/// Tablemodels
	/// <author>Colin Goudie</author>
	/// </summary>
	public class TableModelDictionary : Dictionary<String, TableModel>
	{
		private Dictionary<string, int> tableOrder;
		private EditorModel model;
		private Thread tableLoadThread;
		private int tableIndex;
		private String tableName;
        private bool BigEndian = false;

        public TableModelDictionary(EditorModel model, Dictionary<string, int> tableOrder) : base()
		{
			this.BigEndian = model.BigEndian;
            this.tableOrder = tableOrder;
			this.model = model;            
		}

		new public TableModel this[String key]
		{  
            get 
			{
				if (base.ContainsKey(key))
				{
					return base[key];
				}

				tableName = key;

				//This model isnt loaded, lets load it
                bool stop = false;
                if (tableOrder.ContainsKey(key))
				    tableIndex = tableOrder[key];
                else stop = true;

				tableLoadThread = new Thread(new ThreadStart(TableLoadFunction));

				tableLoadThread.Start();

				while (tableLoadThread.IsAlive)
				{
					System.Windows.Forms.Application.DoEvents();
				}

				return this[key];
			 }
		}

		private void TableLoadFunction()
		{
			//We want to process the table in another thread.
			if (!model.ProcessTable(tableIndex))
			{
				Exception e = new ApplicationException("Error Processing table: " + tableName);
				MaddenEditor.Forms.ExceptionDialog.Show(e);
			}
		}
	}
}
