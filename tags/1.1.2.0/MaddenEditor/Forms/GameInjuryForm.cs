/******************************************************************************
 * Gommo's Madden Editor
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
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	public partial class GameInjuryForm : Form, IEditorForm
	{
		private EditorModel model;
		private int localInGameInjury;
		private int localSimInjury;
		private GameOptionRecord gameRecord;

		public GameInjuryForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			get
			{
				return model;
			}
			set
			{

			}
		}

		public void InitialiseUI()
		{
			//Get the only gameoption record
			TableRecordModel tableRecord = model.TableModels[EditorModel.GAME_OPTIONS_TABLE].GetRecord(0);

			gameRecord = (GameOptionRecord)tableRecord;

			nudInGameInjury.Value = gameRecord.InGameInjury;
			tbInGameInjury.Value = gameRecord.InGameInjury;
			localInGameInjury = gameRecord.InGameInjury;

			nudSimInjury.Value = gameRecord.SimInjury;
			tbSimInjury.Value = gameRecord.SimInjury;
			localSimInjury = gameRecord.SimInjury;
		}

		public void CleanUI()
		{
			
		}

		#endregion

		private void applyButton_Click(object sender, EventArgs e)
		{
			//Apply changes and close
			gameRecord.SimInjury = localSimInjury;
			gameRecord.InGameInjury = localInGameInjury;

			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void tbInGameInjury_Scroll(object sender, EventArgs e)
		{
			localInGameInjury = tbInGameInjury.Value;
			//Set the nud too
			nudInGameInjury.Value = tbInGameInjury.Value;
		}

		private void tbSimInjury_Scroll(object sender, EventArgs e)
		{
			localSimInjury = tbSimInjury.Value;
			//Set the nud too
			nudSimInjury.Value = tbSimInjury.Value;
		}

		private void nudInGameInjury_ValueChanged(object sender, EventArgs e)
		{
			localInGameInjury = (int)nudInGameInjury.Value;
			//set the track bar too
			tbInGameInjury.Value = (int)nudInGameInjury.Value;
		}

		private void nudSimInjury_ValueChanged(object sender, EventArgs e)
		{
			localSimInjury = (int)nudSimInjury.Value;
			//set the track bar too
			tbSimInjury.Value = (int)nudSimInjury.Value;
		}
}
}