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
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MaddenEditor.Core.Record;
//using LumenWorks.Framework.IO.Csv;

namespace MaddenEditor.Core
{
	public class TeamUniformModel
	{
		// Name of the data file to load uniform mappings from
		private const string DATA_FILE = "Uniform Mappings.csv";

		// Reference to our main editor model object
		private EditorModel model;
		private bool loadedData = false;

		private SortedList<int, UniformRecord> sortedUniformList;
		
		public TeamUniformModel(EditorModel model)
		{
			this.model = model;

			sortedUniformList = new SortedList<int, UniformRecord>();

			//Create the sorted list to uniform records
			foreach (TableRecordModel rec in model.TableModels[EditorModel.UNIFORM_TABLE].GetRecords())
			{
				UniformRecord record = (UniformRecord)rec;

				try
				{
					sortedUniformList.Add(record.UniformId, record);
				}
				catch (Exception e)
				{
					e = e;
					Trace.WriteLine("Failed to add UniformID:" + record.UniformId);
				}
			}

            /*
			if (!loadedData)
			{
				loadedData = LoadDataFromCSV();
				if (!loadedData)
				{
					System.Windows.Forms.MessageBox.Show("Loading of " + DATA_FILE + " failed. \r\nDefault Uniform Editing is disabled", "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
			}
            */
		}

		public SortedList<int, UniformRecord> GetUniforms(TeamRecord team)
		{
			if (!loadedData)
			{
				return null;
			}

			SortedList<int, UniformRecord> result = new SortedList<int, UniformRecord>();

			foreach (TableRecordModel rec in model.TableModels[EditorModel.UNIFORM_TABLE].GetRecords())
			{
				UniformRecord uniformRec = (UniformRecord)rec;

				if (uniformRec.TeamId == team.TeamId)
				{
					result.Add(uniformRec.TeamUniformCombo, uniformRec);
				}
			}

			return result;
		}

        /*
		private bool LoadDataFromCSV()
		{
			try
			{
				StreamReader streamReader = new StreamReader(Application.StartupPath + @"\res\" + DATA_FILE);

				CsvReader reader = new CsvReader(streamReader, true);

				while (reader.ReadNextRecord())
				{
					String ufidStr = reader["UFID"];
					String homeaway = reader["Home/Away"];
					String jersey = reader["Jersey Color"];
					String pants = reader["Pants Color"];
					String throwback = reader["Throwback"];

					int ufid = Convert.ToInt32(ufidStr);
										
					if (sortedUniformList.ContainsKey(ufid))
					{
						UniformRecord rec = sortedUniformList[ufid];

						rec.HomeAway = homeaway;
						rec.JerseyColor = jersey;
						rec.PantsColor = pants;
						rec.Throwback = (throwback == "Y");
					}
				}

				streamReader.Dispose();
				reader.Dispose();

				streamReader = null;
				reader = null;
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
				return false;
			}

			return true;
		}
        */
	}
}
