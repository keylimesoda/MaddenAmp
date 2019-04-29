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
using System.Windows.Forms;

namespace MaddenEditor.Core.Record
{
	public class UniformRecord : TableRecordModel
	{
		public const string UNIFORM_ID = "UFID";
		public const string TEAM_ID = "TGID";
		public const string TEAM_UNIFORM_COMBO = "TUCO";
		
		private String homeAway;
		private String jerseyColor;
		private String pantsColor;
		private bool throwBack;
		
		public UniformRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
			
		}
		
		public int UniformId
		{
			get
			{
				return GetIntField(UNIFORM_ID);
			}
			set
			{
				SetField(UNIFORM_ID, value);
			}
		}

		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}

		public int TeamUniformCombo
		{
			get
			{
				return GetIntField(TEAM_UNIFORM_COMBO);
			}
			set
			{
				SetField(TEAM_UNIFORM_COMBO, value);
			}
		}

		public String HomeAway
		{
			set
			{
				homeAway = value;
			}
			get
			{
				return (homeAway == "H" ? "Home" : "Away");
			}
		}

		public String JerseyColor
		{
			set
			{
				jerseyColor = value;
			}
			get
			{
				return jerseyColor;
			}
		}

		public String PantsColor
		{
			set
			{
				pantsColor = value;
			}
			get
			{
				return pantsColor;
			}
		}

		public Boolean Throwback
		{
			set
			{
				throwBack = value;
			}
			get
			{
				return throwBack;
			}
		}

		public DataGridViewRow GetDataGridViewRow()
		{
			DataGridViewRow viewRow = new DataGridViewRow();

			DataGridViewTextBoxCell homeAwayTextBox = new DataGridViewTextBoxCell();
			homeAwayTextBox.Value = this.HomeAway;
			viewRow.Cells.Add(homeAwayTextBox);

			DataGridViewTextBoxCell jerseyColorTextBox = new DataGridViewTextBoxCell();
			jerseyColorTextBox.Value = this.JerseyColor;
			viewRow.Cells.Add(jerseyColorTextBox);

			DataGridViewTextBoxCell pantsColorTextBox = new DataGridViewTextBoxCell();
			pantsColorTextBox.Value = this.PantsColor;
			viewRow.Cells.Add(pantsColorTextBox);

			DataGridViewTextBoxCell throwbackTextBox = new DataGridViewTextBoxCell();
			throwbackTextBox.Value = Throwback.ToString();
			viewRow.Cells.Add(throwbackTextBox);

			DataGridViewTextBoxCell objectTextBox = new DataGridViewTextBoxCell();
			objectTextBox.ValueType = typeof(UniformRecord);
			objectTextBox.Value = this;
			viewRow.Cells.Add(objectTextBox);

			return viewRow;
		}
	}
}