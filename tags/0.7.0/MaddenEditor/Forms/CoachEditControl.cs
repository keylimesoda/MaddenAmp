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
	public partial class CoachEditControl : UserControl, IEditorForm
	{
		private bool isInitialising = false;

		private EditorModel model = null;

		public CoachEditControl()
		{
			isInitialising = true;

			InitializeComponent();

			isInitialising = false;
		}

		public void LoadCoachInfo(CoachRecord record)
		{
			if (record == null)
			{
				model.CoachModel.RemovePositionFilter();
				model.CoachModel.RemoveTeamFilter();
				MessageBox.Show("No Records available. Removing filters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				filterPositionCheckBox.Checked = false;
				filterTeamCheckBox.Checked = false;
				model.CoachModel.GetNextCoachRecord();
				record = model.CoachModel.CurrentCoachRecord;
			}
			isInitialising = true;

			//Load Coach General info
			coachesName.Text = record.Name;
			coachesPositionCombo.Text = coachesPositionCombo.Items[record.Position].ToString(); ;
			string team = model.TeamModel.GetTeamNameFromTeamId(record.TeamId);
			if (team.Equals(EditorModel.UNKNOWN_TEAM_NAME))
			{
				coachTeamCombo.Enabled = false;
			}
			else
			{
				coachTeamCombo.Text = team;
			}
			coachAge.Value = (int)record.Age;
			coachSalary.Value = (decimal)((double)record.Salary / 100.0);

			//Win-Loss Records
			coachPlayoffWins.Value = (int)record.PlayoffWins;
			coachPlayoffLoses.Value = (int)record.PlayoffLoses;
			coachSuperbowlWins.Value = (int)record.SuperBowlWins;
			coachSuperBowlLoses.Value = (int)record.SuperBowlLoses;
			coachWinningSeasons.Value = (int)record.WinningSeasons;

			//Attributes
			coachEthics.Value = (int)record.Ethics;
			coachKnowledge.Value = (int)record.Knowledge;
			coachMotivation.Value = (int)record.Motivation;
			coachChemistry.Value = (int)record.Chemistry;

			coachOffense.Value = (int)record.OffensiveRating;
			coachDefense.Value = (int)record.DefensiveRating;

			//Priorities
			coachQBPriority.Value = (int)record.QuarterbackRating;
			coachRBPriority.Value = (int)record.RunningbackRating;
			coachWRPriority.Value = (int)record.OffensiveLineRating;
			coachDLPriority.Value = (int)record.DefensiveLineRating;
			coachLBPriority.Value = (int)record.LinebackerRating;
			coachDBPriority.Value = (int)record.DefensiveBackRating;
			coachKickerPriority.Value = (int)record.KickerRating;
			coachPunterPriority.Value = (int)record.PuntRating;

			isInitialising = false;
		}

		#region Coaches General Settings

		private void coachesName_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Name = coachesName.Text;
			}
		}

		private void coachesPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Position = coachesPositionCombo.SelectedIndex;
			}
		}

		private void coachTeamCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.TeamId = coachTeamCombo.SelectedIndex;
			}
		}

		#endregion

		#region IEditorForm Members

		public EditorModel Model
		{
			set { model = value; }
		}

		public void InitialiseUI()
		{
			foreach (string teamname in model.TeamModel.GetTeamNames())
			{
				coachTeamCombo.Items.Add(teamname);
				filterTeamComboBox.Items.Add(teamname);
			}

			filterPositionComboBox.Text = filterPositionComboBox.Items[0].ToString();
			filterTeamComboBox.Text = filterTeamComboBox.Items[0].ToString();

			LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
		}

		public void CleanUI()
		{
			coachTeamCombo.Items.Clear();
			filterTeamComboBox.Items.Clear();
		}

		#endregion

		#region Coach Navigate Filter Functions

		private void leftButton_Click(object sender, EventArgs e)
		{
			LoadCoachInfo(model.CoachModel.GetPreviousCoachRecord());
		}

		private void rightButton_Click(object sender, EventArgs e)
		{
			LoadCoachInfo(model.CoachModel.GetNextCoachRecord());
		}

		#endregion

		private void coachAge_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Age = (int)coachAge.Value;
			}
		}

		private void coachPlayoffWins_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.PlayoffWins = (int)coachPlayoffWins.Value;
			}
		}

		private void coachPlayoffLoses_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.PlayoffLoses = (int)coachPlayoffLoses.Value;
			}
		}

		private void coachSuperbowlWins_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.SuperBowlWins = (int)coachSuperbowlWins.Value;
			}
		}

		private void coachSuperBowlLoses_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.SuperBowlLoses = (int)coachSuperBowlLoses.Value;
			}
		}

		private void coachWinningSeasons_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.WinningSeasons = (int)coachWinningSeasons.Value;
			}
		}

		private void coachEthics_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Ethics = (int)coachEthics.Value;
			}
		}

		private void coachMotivation_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Motivation = (int)coachMotivation.Value;
			}
		}

		private void coachKnowledge_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Knowledge = (int)coachKnowledge.Value;
			}
		}

		private void coachChemistry_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.Chemistry = (int)coachChemistry.Value;
			}
		}

		private void coachOffense_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.OffensiveRating = (int)coachOffense.Value;
			}
		}

		private void coachDefense_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveRating = (int)coachDefense.Value;
			}
		}

		private void coachQBPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.QuarterbackRating = (int)coachQBPriority.Value;
			}
		}

		private void coachRBPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.RunningbackRating = (int)coachRBPriority.Value;
			}
		}

		private void coachWRPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.WideReceiverRating = (int)coachWRPriority.Value;
			}
		}

		private void coachOLPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.OffensiveLineRating = (int)coachOLPriority.Value;
			}
		}

		private void coachDLPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveLineRating = (int)coachDLPriority.Value;
			}
		}

		private void coachLBPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.LinebackerRating = (int)coachLBPriority.Value;
			}
		}

		private void coachDBPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveBackRating = (int)coachDBPriority.Value;
			}
		}

		private void coachKickerPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.KickerRating = (int)coachKickerPriority.Value;
			}
		}

		private void coachPunterPriority_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.PuntRating = (int)coachPunterPriority.Value;
			}
		}

		private void filterTeamCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterTeamCheckBox.Checked)
			{
				model.CoachModel.SetTeamFilter(filterTeamComboBox.SelectedItem.ToString());
				//Generate a move next so it will filter
				model.CoachModel.GetNextCoachRecord();
				LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
			}
			else
			{
				model.CoachModel.RemoveTeamFilter();
			}
		}

		private void filterTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (filterTeamCheckBox.Checked)
			{
				model.CoachModel.SetTeamFilter(filterTeamComboBox.SelectedItem.ToString());
				//Generate a move next so it will filter
				model.CoachModel.GetNextCoachRecord();
				LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
			}
		}

		private void filterPositionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterPositionCheckBox.Checked)
			{
				model.CoachModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);

				model.CoachModel.GetNextCoachRecord();
				LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
			}
			else
			{
				model.CoachModel.RemovePositionFilter();
			}
		}

		private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (filterPositionCheckBox.Checked)
			{
				model.CoachModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);

				model.CoachModel.GetNextCoachRecord();
				LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
			}
		}

		

		
	}
}
