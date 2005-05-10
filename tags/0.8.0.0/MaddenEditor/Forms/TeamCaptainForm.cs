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
	public partial class TeamCaptainForm : Form, IEditorForm
	{
		private bool isInitialising = false;
		private EditorModel model = null;
		private TeamCaptainRecord teamCaptainRecord = null;

		public TeamCaptainForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (offensiveCaptainCombo.Text.Equals("") ||
				defensiveCaptainCombo.Text.Equals("") ||
				specialTeamsCaptainCombo.Text.Equals(""))
			{
				MessageBox.Show("You must select 3 captains", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (teamCaptainRecord != null)
			{
				teamCaptainRecord.Captain1 = ((PlayerRecord)offensiveCaptainCombo.SelectedItem).PlayerId;
				teamCaptainRecord.Captain2 = ((PlayerRecord)defensiveCaptainCombo.SelectedItem).PlayerId;
				teamCaptainRecord.Captain3 = ((PlayerRecord)specialTeamsCaptainCombo.SelectedItem).PlayerId;
			}
			else
			{
				teamCaptainRecord = (TeamCaptainRecord)model.TableModels[EditorModel.TEAM_CAPTAIN_TABLE].CreateNewRecord(true);

				teamCaptainRecord.TeamId = ((PlayerRecord)(offensiveCaptainCombo.SelectedItem)).TeamId;

				teamCaptainRecord.Captain1 = ((PlayerRecord)offensiveCaptainCombo.SelectedItem).PlayerId;
				teamCaptainRecord.Captain2 = ((PlayerRecord)defensiveCaptainCombo.SelectedItem).PlayerId;
				teamCaptainRecord.Captain3 = ((PlayerRecord)specialTeamsCaptainCombo.SelectedItem).PlayerId;
			}

			MessageBox.Show("Captains Stored for " + teamComboBox.Text, "Team Captains Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
			
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			isInitialising = true;
			//Load up the team tables
			foreach (string team in model.TeamModel.GetTeamNames())
			{
				teamComboBox.Items.Add(team);
			}

			teamComboBox.Text = teamComboBox.Items[0].ToString();

			isInitialising = false;
			LoadTeamCaptains();
		}

		public void CleanUI()
		{
			teamComboBox.Items.Clear();
		}

		#endregion

		private void LoadTeamCaptains()
		{
			if (teamComboBox.Text.Equals(""))
			{
				return;
			}
			isInitialising = true;
			this.Cursor = Cursors.WaitCursor;
			int teamId = model.TeamModel.GetTeamIdFromTeamName(teamComboBox.Text);

			offensiveCaptainCombo.Items.Clear();
			defensiveCaptainCombo.Items.Clear();
			specialTeamsCaptainCombo.Items.Clear();

			SortedList<string, PlayerRecord> list = new SortedList<string, PlayerRecord>();

			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (record.Deleted)
				{
					continue;
				}
				PlayerRecord player = (PlayerRecord)record;

				if (player.TeamId == teamId)
				{
					try
					{
						list.Add(player.ToString(), player);
					}
					catch (ArgumentException e)
					{
						Console.WriteLine(e.ToString());
					}
				}
			}

			//Now load them into the combo boxes
			foreach (PlayerRecord player in list.Values)
			{
				offensiveCaptainCombo.Items.Add(player);
				defensiveCaptainCombo.Items.Add(player);
				specialTeamsCaptainCombo.Items.Add(player);
			}
			//Lose the reference to the old team captain record
			teamCaptainRecord = null;
			//Now find the team captains if there are some
			foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_CAPTAIN_TABLE].GetRecords())
			{
				if (((TeamCaptainRecord)record).TeamId == teamId)
				{
					teamCaptainRecord = (TeamCaptainRecord)record;
					
					break;
				}
			}

			if (teamCaptainRecord != null)
			{
				foreach (PlayerRecord player in list.Values)
				{
					if (player.PlayerId == teamCaptainRecord.Captain1)
					{
						offensiveCaptainCombo.Text = player.ToString();
					}
					if (player.PlayerId == teamCaptainRecord.Captain2)
					{
						defensiveCaptainCombo.Text = player.ToString();
					}
					if (player.PlayerId == teamCaptainRecord.Captain3)
					{
						specialTeamsCaptainCombo.Text = player.ToString();
					}
				}
			}
			else
			{
				offensiveCaptainCombo.Text = "";
				defensiveCaptainCombo.Text = "";
				specialTeamsCaptainCombo.Text = "";
			}

			this.Cursor = Cursors.Default;
			isInitialising = false;
		}

		private void teamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				LoadTeamCaptains();
			}
		}
}
}