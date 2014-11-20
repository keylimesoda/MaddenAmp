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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	public partial class TeamEditControl : UserControl, IEditorForm
	{
		private const string UNIFORM_OBJ_COL = "clmUniformObj";

		private EditorModel model = null;

		private bool isInitialising = false;

		private TeamRecord lastLoadedRecord = null;

		public TeamEditControl()
		{
			isInitialising = true;

			InitializeComponent();

			isInitialising = false;
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set { this.model = value; }
		}

		public void InitialiseUI()
		{
			isInitialising = true;

			//Fill in the combo boxes
			foreach (GenericRecord rec in model.TeamModel.LeagueList)
			{
				leagueCombo.Items.Add(rec);
				filterTeamLeagueCombo.Items.Add(rec);
			}
			foreach (GenericRecord rec in model.TeamModel.DivisionList)
			{
				divisionCombo.Items.Add(rec);
				filterTeamDivisionCombo.Items.Add(rec);
			}
			foreach (GenericRecord rec in model.TeamModel.ConferenceList)
			{
				conferenceCombo.Items.Add(rec);
				filterTeamConferenceCombo.Items.Add(rec);
			}
			foreach (GenericRecord rec in model.TeamModel.CityList)
			{
				cityCombo.Items.Add(rec);
			}
			foreach (GenericRecord rec in model.TeamModel.OffensivePlaybookList)
			{
				teamOffensivePlaybookCombo.Items.Add(rec);
			}
			foreach (GenericRecord rec in model.TeamModel.DefensivePlaybookList)
			{
				teamDefensivePlaybookCombo.Items.Add(rec);
			}
			foreach (TeamRecord team in model.TeamModel.GetTeams())
			{
				cbTeamRival1.Items.Add(team);
				cbTeamRival2.Items.Add(team);
				cbTeamRival3.Items.Add(team);
			}
			cbTeamRival1.SelectedIndex = 0;
			cbTeamRival2.SelectedIndex = 0;
			cbTeamRival3.SelectedIndex = 0;

			leagueCombo.SelectedIndex = 0;
			divisionCombo.SelectedIndex = 0;
			conferenceCombo.SelectedIndex = 0;
			filterTeamConferenceCombo.SelectedIndex = 0;
			filterTeamDivisionCombo.SelectedIndex = 0;
			filterTeamLeagueCombo.SelectedIndex = 0;
			cityCombo.SelectedIndex = 0;
			teamDefensivePlaybookCombo.SelectedIndex = 0;
			teamOffensivePlaybookCombo.SelectedIndex = 0;

			//Madden 2007 Doesn't have shoe colors anymore
			if (model.FileVersion >= MaddenFileVersion.Ver2007) 
			{
				gbShoeColor.Enabled = false;
			}
			else
			{
				gbShoeColor.Enabled = true;
			}

			//Load a team
			LoadTeamInfo(model.TeamModel.CurrentTeamRecord);

			if (dgDefaultUniforms.Rows.Count > 0)
			{
				dgDefaultUniforms.Rows[0].Selected = true;
			}

			EnableDefaultUniformButtons();

			isInitialising = false;
		}

		public void CleanUI()
		{
			//Clear the combo boxes
			filterTeamLeagueCombo.Items.Clear();
			filterTeamDivisionCombo.Items.Clear();
			filterTeamConferenceCombo.Items.Clear();
			leagueCombo.Items.Clear();
			divisionCombo.Items.Clear();
			conferenceCombo.Items.Clear();
			cityCombo.Items.Clear();
			teamDefensivePlaybookCombo.Items.Clear();
			teamOffensivePlaybookCombo.Items.Clear();
			dgDefaultUniforms.Rows.Clear();
		}

		#endregion

		private void LoadTeamInfo(TeamRecord record)
		{
			if (record == null)
			{
				MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return;
			}

			isInitialising = true;
			try
			{
				nameTextBox.Text = record.Name;
				longNameTextBox.Text = record.LongName;
				shortTeamName.Text = record.ShortName;
				nickNameTextBox.Text = record.NickName;

				teamQBRating.Value = record.QBRating;
				teamRBRating.Value = record.RBRating;
				teamWRRating.Value = record.WRRating;
				teamOLRating.Value = record.OLRating;

				teamDLRating.Value = record.DLRating;
				teamLBRating.Value = record.LBRating;
				teamDBRating.Value = record.DBRating;

				teamSpecialTeamsRating.Value = record.SpecialTeamsRating;
				teamOffensiveRating.Value = record.OffensiveRating;
				teamDefensiveRating.Value = record.DefensiveRating;
				teamOverallRating.Value = record.OverallRating;

				teamReputation.Value = record.Reputation;

				if (model.FileVersion <= MaddenFileVersion.Ver2006)
				{
					rbWhite.Checked = (record.ShoeColor == 0);
					rbBlack.Checked = (record.ShoeColor == 1);
					rbWhite.Enabled = true;
					rbBlack.Enabled = true;
				}
				else
				{
					rbWhite.Enabled = false;
					rbBlack.Enabled = false;
				}

				//Team colours
				pnlPrimary.BackColor = record.PrimaryColor;
				pnlSecondary.BackColor = record.SecondaryColor;

				LoadDefaultUniformDataGrid(record);

				foreach (Object obj in divisionCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.DivisionId)
					{
						divisionCombo.SelectedItem = obj;
						break;
					}
				}
				foreach (Object obj in leagueCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.LeagueId)
					{
						leagueCombo.SelectedItem = obj;
						break;
					}
				}
				foreach (object obj in conferenceCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.ConferenceId)
					{
						conferenceCombo.SelectedItem = obj;
						break;
					}
				}
				foreach (object obj in cityCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.CityId)
					{
						cityCombo.SelectedItem = obj;
						break;
					}
				}
				bool found = false;
				foreach (object obj in teamOffensivePlaybookCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.OffensivePlaybook)
					{
						teamOffensivePlaybookCombo.SelectedItem = obj;
						found = true;
						break;
					}
				}
				teamOffensivePlaybookCombo.Enabled = found;
				foreach (object obj in teamDefensivePlaybookCombo.Items)
				{
					if (((GenericRecord)obj).Id == record.DefensivePlaybook)
					{
						teamDefensivePlaybookCombo.SelectedItem = obj;
						break;
					}
				}
				if (model.FileVersion >= MaddenFileVersion.Ver2005)
				{
					if (model.TeamModel.CurrentTeamRecord.TeamRival1 == TeamEditingModel.NO_TEAM_ID)
					{
						cbTeamRival1.Text = "N/A";
					}
					else
					{
						cbTeamRival1.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival1);
					}

					if (model.TeamModel.CurrentTeamRecord.TeamRival2 == TeamEditingModel.NO_TEAM_ID)
					{
						cbTeamRival2.Text = "N/A";
					}
					else
					{
						cbTeamRival2.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival2);
					}

					if (model.TeamModel.CurrentTeamRecord.TeamRival3 == TeamEditingModel.NO_TEAM_ID)
					{
						cbTeamRival3.Text = "N/A";
					}
					else
					{
						cbTeamRival3.SelectedItem = model.TeamModel.GetTeamRecord(model.TeamModel.CurrentTeamRecord.TeamRival3);
					}
					cbTeamRival1.Enabled = true;
					cbTeamRival2.Enabled = true;
					cbTeamRival3.Enabled = true;
				}
				else
				{
					cbTeamRival1.Enabled = false;
					cbTeamRival2.Enabled = false;
					cbTeamRival3.Enabled = false;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("Exception Occurred loading this Team:\r\n" + e.ToString(), "Exception Loading Team", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LoadTeamInfo(lastLoadedRecord);
				return;
			}
			finally 
			{
				isInitialising = false;
			}

			lastLoadedRecord = record;
		}

		private void LoadDefaultUniformDataGrid(TeamRecord record)
		{
			//Fill in the datagrid for this team's uniforms
			//Clear the current rows
			dgDefaultUniforms.Rows.Clear();

			SortedList<int, UniformRecord> defaultUniforms = model.TeamModel.TeamUniformModel.GetUniforms(record);

			if (defaultUniforms == null)
			{
				//Disable the data grid for this team
				dgDefaultUniforms.Visible = false;
				btnDefaultUniDown.Visible = false;
				btnDefaultUniUp.Visible = false;
				lblDefaultUniforms.Visible = false;
				tbDefaultAway.Visible = false;
				tbDefaultHome.Visible = false;
			}
			else
			{
				dgDefaultUniforms.Visible = true;
				btnDefaultUniDown.Visible = true;
				btnDefaultUniUp.Visible = true;
				lblDefaultUniforms.Visible = true;
				tbDefaultAway.Visible = true;
				tbDefaultHome.Visible = true;

				int row = 0;
				foreach (UniformRecord rec in defaultUniforms.Values)
				{
					DataGridViewRow viewRow = rec.GetDataGridViewRow();

					if (row == 0)
					{
						viewRow.DefaultCellStyle.BackColor = Color.AliceBlue;
					}
					else if (row == 1)
					{
						viewRow.DefaultCellStyle.BackColor = Color.Beige;
					}

					dgDefaultUniforms.Rows.Add(viewRow);
					row++;
				}
			}
		}

		private void nameTextBox_TextChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Name = nameTextBox.Text;
			}
		}

		private void leftButton_Click(object sender, EventArgs e)
		{
			LoadTeamInfo(model.TeamModel.GetPreviousTeamRecord());
		}

		private void rightButton_Click(object sender, EventArgs e)
		{
			LoadTeamInfo(model.TeamModel.GetNextTeamRecord());
		}

		private void filterConferenceCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterConferenceCheckBox.Checked)
			{
				model.TeamModel.SetConferenceFilter(((GenericRecord)filterTeamConferenceCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
			else
			{
				model.TeamModel.RemoveConferenceFilter();
			}
		}

		private void filterTeamConferenceCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (filterConferenceCheckBox.Checked)
			{
				model.TeamModel.SetConferenceFilter(((GenericRecord)filterTeamConferenceCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
		}

		private void filterDivisionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterDivisionCheckBox.Checked)
			{
				model.TeamModel.SetDivisionFilter(((GenericRecord)filterTeamDivisionCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
			else
			{
				model.TeamModel.RemoveDivisionFilter();
			}
		}

		private void filterTeamDivisionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (filterDivisionCheckBox.Checked)
			{
				model.TeamModel.SetDivisionFilter(((GenericRecord)filterTeamDivisionCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
		}

		private void filterLeagueCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterLeagueCheckBox.Checked)
			{
				model.TeamModel.SetLeagueFilter(((GenericRecord)filterTeamLeagueCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
			else
			{
				model.TeamModel.RemoveLeagueFilter();
			}
		}

		private void filterTeamLeagueCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (filterLeagueCheckBox.Checked)
			{
				model.TeamModel.SetLeagueFilter(((GenericRecord)filterTeamLeagueCombo.SelectedItem).Id);

				model.TeamModel.GetNextTeamRecord();
				LoadTeamInfo(model.TeamModel.CurrentTeamRecord);
			}
		}

		private void nameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.Name = nameTextBox.Text;
			}
		}

		private void longNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.LongName = longNameTextBox.Text;
			}
		}

		private void shortTeamName_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.ShortName = shortTeamName.Text;
			}
		}

		private void nickNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.NickName = nickNameTextBox.Text;
			}
		}

		private void conferenceCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.ConferenceId = (((GenericRecord)conferenceCombo.SelectedItem).Id);
			}
		}

		private void divisionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.DivisionId = (((GenericRecord)divisionCombo.SelectedItem).Id);
			}
		}

		private void leagueCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.LeagueId = (((GenericRecord)leagueCombo.SelectedItem).Id);
			}
		}

		private void cityCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.CityId = (((GenericRecord)cityCombo.SelectedItem).Id);
			}
		}

		private void teamQBRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.QBRating = (int)teamQBRating.Value;
			}
		}

		private void teamRBRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.RBRating = (int)teamRBRating.Value;
			}
		}

		private void teamWRRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.WRRating = (int)teamWRRating.Value;
			}
		}

		private void teamOLRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.OLRating = (int)teamOLRating.Value;
			}
		}

		private void teamDLRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.DLRating = (int)teamDLRating.Value;
			}
		}

		private void teamLBRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.LBRating = (int)teamLBRating.Value;
			}
		}

		private void teamDBRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.DBRating = (int)teamDBRating.Value;
			}
		}

		private void teamOffensiveRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.OffensiveRating = (int)teamOffensiveRating.Value;
			}
		}

		private void teamSpecialTeamsRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.SpecialTeamsRating = (int)teamSpecialTeamsRating.Value;
			}
		}

		private void teamDefensiveRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.DefensiveRating = (int)teamDefensiveRating.Value;
			}
		}

		private void teamOverallRating_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.OverallRating = (int)teamOverallRating.Value;
			}
		}

		private void teamOffensivePlaybookCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.OffensivePlaybook = (((GenericRecord)teamOffensivePlaybookCombo.SelectedItem).Id);
			}
		}

		private void teamDefensivePlaybookCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.DefensivePlaybook = (((GenericRecord)teamDefensivePlaybookCombo.SelectedItem).Id);
			}
		}

		private void teamReputation_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.Reputation = (int)teamReputation.Value;
			}
		}

		private void rbWhite_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.ShoeColor = (rbWhite.Checked == true ? 0 : 1);
			}
		}

		private void rbBlack_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.ShoeColor = (rbBlack.Checked == true ? 1 : 0);
			}
		}

		private void btnPrimary_Click(object sender, EventArgs e)
		{
			ColorChooser chooser = new ColorChooser(model.TeamModel.CurrentTeamRecord.PrimaryColor);

			DialogResult result = chooser.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				//Change primary color
				model.TeamModel.CurrentTeamRecord.PrimaryColor = chooser.ChosenColor;
				//Update the panel
				pnlPrimary.BackColor = chooser.ChosenColor;
			}

			chooser.Dispose();

			chooser = null;
		}

		private void btnSecondary_Click(object sender, EventArgs e)
		{
			ColorChooser chooser = new ColorChooser(model.TeamModel.CurrentTeamRecord.SecondaryColor);

			DialogResult result = chooser.ShowDialog(this);

			if (result == DialogResult.OK)
			{
				//Change secondary color
				model.TeamModel.CurrentTeamRecord.SecondaryColor = chooser.ChosenColor;
				//Update the panel
				pnlSecondary.BackColor = chooser.ChosenColor;
			}

			chooser.Dispose();

			chooser = null;
		}

		private void cbTeamRival1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.TeamRival1 = ((TeamRecord)cbTeamRival1.SelectedItem).TeamId;
			}
		}

		private void cbTeamRival2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.TeamRival2 = ((TeamRecord)cbTeamRival2.SelectedItem).TeamId;
			}
		}

		private void cbTeamRival3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.TeamModel.CurrentTeamRecord.TeamRival3 = ((TeamRecord)cbTeamRival3.SelectedItem).TeamId;
			}
		}

		private void btnDefaultUniUp_Click(object sender, EventArgs e)
		{
			//Need to get the 2 records, the one that is selected and the one above this one
			if (dgDefaultUniforms.SelectedRows.Count == 1)
			{
				int selectedRow = dgDefaultUniforms.SelectedRows[0].Index;

				UniformRecord selectedRecord = (UniformRecord)dgDefaultUniforms.Rows[selectedRow].Cells[UNIFORM_OBJ_COL].Value;
				UniformRecord destRecord = (UniformRecord)dgDefaultUniforms.Rows[selectedRow - 1].Cells[UNIFORM_OBJ_COL].Value;

				int temp = selectedRecord.TeamUniformClass;
				selectedRecord.TeamUniformClass = destRecord.TeamUniformClass;
				destRecord.TeamUniformClass = temp;

				LoadDefaultUniformDataGrid(lastLoadedRecord);

				dgDefaultUniforms.Rows[selectedRow - 1].Selected = true;

				EnableDefaultUniformButtons();
			}
		}

		private void btnDefaultUniDown_Click(object sender, EventArgs e)
		{
			//Need to get the 2 records, the one that is selected and the one below this one
			if (dgDefaultUniforms.SelectedRows.Count == 1)
			{
				int selectedRow = dgDefaultUniforms.SelectedRows[0].Index;

				UniformRecord selectedRecord = (UniformRecord)dgDefaultUniforms.Rows[selectedRow].Cells[UNIFORM_OBJ_COL].Value;
				UniformRecord destRecord = (UniformRecord)dgDefaultUniforms.Rows[selectedRow + 1].Cells[UNIFORM_OBJ_COL].Value;

				int temp = selectedRecord.TeamUniformClass;
				selectedRecord.TeamUniformClass = destRecord.TeamUniformClass;
				destRecord.TeamUniformClass = temp;

				LoadDefaultUniformDataGrid(lastLoadedRecord);

				dgDefaultUniforms.Rows[selectedRow + 1].Selected = true;

				EnableDefaultUniformButtons();
			}
		}

		private void dgDefaultUniforms_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (!isInitialising)
			{
				EnableDefaultUniformButtons();
			}
		}

		private void EnableDefaultUniformButtons()
		{
			btnDefaultUniDown.Enabled = false;
			btnDefaultUniUp.Enabled = false;

			if (dgDefaultUniforms.SelectedRows.Count == 1)
			{
				if (dgDefaultUniforms.SelectedRows[0].Index == 0)
				{
					btnDefaultUniUp.Enabled = false;
				}
				else
				{
					btnDefaultUniUp.Enabled = true;
				}

				if (dgDefaultUniforms.SelectedRows[0].Index == dgDefaultUniforms.Rows.Count - 1)
				{
				btnDefaultUniDown.Enabled = false;
				}
				else
				{
					btnDefaultUniDown.Enabled = true;
				}
			}
		}
	}
	
}
