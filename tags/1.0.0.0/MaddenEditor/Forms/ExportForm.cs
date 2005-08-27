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
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	public partial class ExportForm : Form, IEditorForm
	{
		private EditorModel model = null;

		public ExportForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		#region IEditorForm Members

		public EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			foreach (string team in model.TeamModel.GetTeamNames())
			{
				filterTeamCombo.Items.Add(team);
			}

			foreach (string position in Enum.GetNames(typeof(MaddenPositions)))
			{
				filterPositionCombo.Items.Add(position);
			}

			filterPositionCombo.Text = filterPositionCombo.Items[0].ToString();
			filterTeamCombo.Text = filterTeamCombo.Items[0].ToString();
		}

		public void CleanUI()
		{
			filterTeamCombo.Items.Clear();
			filterPositionCombo.Items.Clear();
		}

		#endregion

		private void filterTeamCombo_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void filterPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ExportButton_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			int teamID = -1;
			int positionID = -1;

			if (filterTeamCheckbox.Checked)
			{
				//Get the team id for the team selected in the combobox
				teamID = model.TeamModel.GetTeamIdFromTeamName(filterTeamCombo.SelectedItem.ToString());
			}

			if (filterPositionCheckbox.Checked)
			{
				//Get the position id for the position selected in the combobox
				positionID = filterPositionCombo.SelectedIndex;
			}

			List<PlayerRecord> playerList = new List<PlayerRecord>();
			
			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (record.Deleted)
				{
					continue;
				}

				PlayerRecord playerRecord = (PlayerRecord)record;

				if (teamID != -1 && playerRecord.TeamId != teamID)
				{
					continue;
				}

				if (positionID != -1 && playerRecord.PositionId != positionID)
				{
					continue;
				}

				if (filterDraftClassCheckbox.Checked && playerRecord.YearsPro != 0)
				{
					continue;
				}

				//This player needs to be added to our list for export
				playerList.Add(playerRecord);
			}

			//Bring up a save dialog
			SaveFileDialog fileDialog = new SaveFileDialog();
			Stream myStream = null;

			fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
			fileDialog.RestoreDirectory = true;
		
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = fileDialog.OpenFile()) != null)
					{
						StreamWriter wText = new StreamWriter(myStream);

						//Output the headers first
						StringBuilder hbuilder = new StringBuilder();
						hbuilder.Append("Position,");
						hbuilder.Append("First Name,");
						hbuilder.Append("Last Name,");
						hbuilder.Append("Team,");
						hbuilder.Append("Age,");
						hbuilder.Append("Height,");
						hbuilder.Append("Weight,");
						hbuilder.Append("OVR,");
						hbuilder.Append("Speed,");
						hbuilder.Append("Strength,");
						hbuilder.Append("Awareness,");
						hbuilder.Append("Agility,");
						hbuilder.Append("Acceleration,");
						hbuilder.Append("Catching,");
						hbuilder.Append("Carrying,");
						hbuilder.Append("Jumping,");
						hbuilder.Append("Break Tackle,");
						hbuilder.Append("Tackle,");
						hbuilder.Append("Throw Power,");
						hbuilder.Append("Throw Accuracy,");
						hbuilder.Append("Pass Blocking,");
						hbuilder.Append("Run Blocking,");
						hbuilder.Append("Kick Power,");
						hbuilder.Append("Kick Accuracy,");
						hbuilder.Append("Kick Return,");
						hbuilder.Append("Stamina,");
						hbuilder.Append("Injury,");
						hbuilder.Append("Toughness,");
						hbuilder.Append("Importance,");
						hbuilder.Append("Morale");

						wText.WriteLine(hbuilder.ToString());

						foreach (PlayerRecord rec in playerList)
						{
							StringBuilder builder = new StringBuilder();
							builder.Append(Enum.GetNames(typeof(MaddenPositions))[rec.PositionId].ToString());
							builder.Append(",");
							builder.Append(rec.FirstName);
							builder.Append(",");
							builder.Append(rec.LastName);
							builder.Append(",");
							builder.Append(model.TeamModel.GetTeamNameFromTeamId(rec.TeamId));
							builder.Append(",");
							builder.Append(rec.Age);
							builder.Append(",");
							builder.Append((rec.Height / 12) + "' " + (rec.Height % 12) + "\"");
							builder.Append(",");
							builder.Append(rec.Weight + 160);
							builder.Append(",");
							builder.Append(rec.Overall);
							builder.Append(",");
							builder.Append(rec.Speed);
							builder.Append(",");
							builder.Append(rec.Strength);
							builder.Append(",");
							builder.Append(rec.Awareness);
							builder.Append(",");
							builder.Append(rec.Agility);
							builder.Append(",");
							builder.Append(rec.Acceleration);
							builder.Append(",");
							builder.Append(rec.Catching);
							builder.Append(",");
							builder.Append(rec.Carrying);
							builder.Append(",");
							builder.Append(rec.Jumping);
							builder.Append(",");
							builder.Append(rec.BreakTackle);
							builder.Append(",");
							builder.Append(rec.Tackle);
							builder.Append(",");
							builder.Append(rec.ThrowPower);
							builder.Append(",");
							builder.Append(rec.ThrowAccuracy);
							builder.Append(",");
							builder.Append(rec.PassBlocking);
							builder.Append(",");
							builder.Append(rec.RunBlocking);
							builder.Append(",");
							builder.Append(rec.KickPower);
							builder.Append(",");
							builder.Append(rec.KickAccuracy);
							builder.Append(",");
							builder.Append(rec.KickReturn);
							builder.Append(",");
							builder.Append(rec.Stamina);
							builder.Append(",");
							builder.Append(rec.Injury);
							builder.Append(",");
							builder.Append(rec.Toughness);
							builder.Append(",");
							builder.Append(rec.Importance);
							builder.Append(",");
							builder.Append(rec.Morale);
							wText.WriteLine(builder.ToString());
							wText.Flush();
						}


						myStream.Close();
					}
				}
				catch(IOException err)
				{
					MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			this.Cursor = Cursors.Default;
			DialogResult = DialogResult.OK;
			this.Close();
		}
}
}