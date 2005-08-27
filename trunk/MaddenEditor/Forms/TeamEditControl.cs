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

			leagueCombo.SelectedIndex = 0;
			divisionCombo.SelectedIndex = 0;
			conferenceCombo.SelectedIndex = 0;
			filterTeamConferenceCombo.SelectedIndex = 0;
			filterTeamDivisionCombo.SelectedIndex = 0;
			filterTeamLeagueCombo.SelectedIndex = 0;
			cityCombo.SelectedIndex = 0;
			teamDefensivePlaybookCombo.SelectedIndex = 0;
			teamOffensivePlaybookCombo.SelectedIndex = 0;

			//Load a team
			LoadTeamInfo(model.TeamModel.CurrentTeamRecord);

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

				rbWhite.Checked = (record.ShoeColor == 0);
				rbBlack.Checked = (record.ShoeColor == 1);

				//Team colours
				pnlPrimary.BackColor = record.PrimaryColor;
				pnlSecondary.BackColor = record.SecondaryColor;

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
			}
			catch (Exception e)
			{
				MessageBox.Show("Exception Occured loading this Team:\r\n" + e.ToString(), "Exception Loading Team", MessageBoxButtons.OK, MessageBoxIcon.Error);
				LoadTeamInfo(lastLoadedRecord);
				return;
			}
			finally 
			{
				isInitialising = false;
			}
			lastLoadedRecord = record;
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

	}
	
}
