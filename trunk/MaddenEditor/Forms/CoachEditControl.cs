/******************************************************************************
 * Madden 2005 Editor
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
	public partial class CoachEditControl : UserControl, IEditorForm
	{
		private bool isInitialising = false;

		private NumericUpDown[] prioritySliders = null;
		private NumericUpDown[] priorityTypeSliders = null;
		private Label[] priorityDescriptionLabels = null;

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
				MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				return;
			}
			isInitialising = true;

			try
			{
				//Load Coach General info
				coachesName.Text = record.Name;
				coachesPositionCombo.Text = coachesPositionCombo.Items[record.Position].ToString(); ;
				string team = model.TeamModel.GetTeamNameFromTeamId(record.TeamId);
				if (team.Equals(EditorModel.UNKNOWN_TEAM_NAME))
				{
					coachTeamCombo.Text = team;
					coachTeamCombo.Enabled = false;
				}
				else
				{
					coachTeamCombo.Enabled = true;
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
				coachCareerWins.Value = (int)record.CareerWins;
				coachCareerLoses.Value = (int)record.CareerLoses;
				coachCareerTies.Value = (int)record.CareerTies;

				if (record.DefensiveAlignment)
				{
					threeFourButton.Checked = false;
					fourThreeButton.Checked = true;
				}
				else
				{
					threeFourButton.Checked = true;
					fourThreeButton.Checked = false;
				}

				//Attributes
				coachEthics.Value = (int)record.Ethics;
				coachKnowledge.Value = (int)record.Knowledge;
				coachMotivation.Value = (int)record.Motivation;
				coachChemistry.Value = (int)record.Chemistry;

				coachPassOff.Value = (int)record.OffensiveStrategy;
				coachRunOff.Value = (int)(100 - record.OffensiveStrategy);
				coachPassDef.Value = (int)record.DefensiveStrategy;
				coachRunDef.Value = (int)(100 - record.DefensiveStrategy);
				rb2.Value = (int)record.RunningBack2Sub;
				rb1.Value = (int)(100 - record.RunningBack2Sub);
				coachDefAggression.Value = record.DefensiveAggression;
				coachOffAggression.Value = record.OffensiveAggression;

				//Priorities
				SortedList<int, CoachPrioritySliderRecord> priorites = model.CoachModel.GetCurrentCoachSliders();
				if (priorites.Count == 0)
				{
					for (int i = 0; i < Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length; i++)
					{
						prioritySliders[i].Value = 0;
						priorityTypeSliders[i].Value = 0;
						priorityDescriptionLabels[i].Text = "";
					}
				}
				else
				{
					int index = 0;
					foreach (CoachPrioritySliderRecord priorRecord in priorites.Values)
					{
						prioritySliders[index].Value = priorRecord.Priority;
						priorityTypeSliders[index].Value = priorRecord.PriorityType;
						priorityDescriptionLabels[index].Text = DecodePriorityType((CoachSliderPlayerPositions)index, priorRecord.PriorityType);
						index++;
					}
				}
			}
			catch (Exception e)
			{
				MessageBox.Show("Exception Occured loading this Coach:\r\n" + e.ToString(), "Exception Loading Coach", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			isInitialising = false;
		}

		private string DecodePriorityType(CoachSliderPlayerPositions pos, int type)
		{
			if (type == 2)
			{
				return "Balanced";
			}

			switch (pos)
			{
				case CoachSliderPlayerPositions.QB:
					return (type == 0 ? "Pocket" : "Scrambling");
				case CoachSliderPlayerPositions.HB:
					return (type == 0 ? "Power" : "Speed");
				case CoachSliderPlayerPositions.FB:
				case CoachSliderPlayerPositions.TE:
					return (type == 0 ? "Blocking" : "Receiving");
				case CoachSliderPlayerPositions.WR:
					return (type == 0 ? "Possession" : "Speed");
				case CoachSliderPlayerPositions.T:
				case CoachSliderPlayerPositions.G:
				case CoachSliderPlayerPositions.C:
					return (type == 0 ? "Run Blocking" : "Pass Blocking");
				case CoachSliderPlayerPositions.DE:
				case CoachSliderPlayerPositions.DT:
					return (type == 0 ? "Pass Rushing" : "Run Stopping");
				case CoachSliderPlayerPositions.OLB:
				case CoachSliderPlayerPositions.MLB:
					return (type == 0 ? "Coverage" : "Run Stopping");
				case CoachSliderPlayerPositions.CB:
				case CoachSliderPlayerPositions.SS:
				case CoachSliderPlayerPositions.FS:
					return (type == 0 ? "Coverage" : "Hard Hitting");
				case CoachSliderPlayerPositions.K:
				case CoachSliderPlayerPositions.P:
					return (type == 0 ? "Power" : "Accurate");
			} 

			return "";
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

			//Create priority controls
			int numPositions = Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length;
			prioritySliders = new NumericUpDown[numPositions];
			priorityTypeSliders = new NumericUpDown[numPositions];
			priorityDescriptionLabels = new Label[numPositions];
			for (int i = 0; i < numPositions; i++)
			{
				prioritySliders[i] = new NumericUpDown();
				prioritySliders[i].Location = new Point(48, 11 + i * 26);
				prioritySliders[i].Size = new Size(76, 20);
				prioritySliders[i].Minimum = 0;
				prioritySliders[i].Maximum = 100;
				prioritySliders[i].Visible = true;
				prioritySliders[i].ValueChanged += new EventHandler(prioritySlider_ValueChanged);

				priorityGroupBox.Controls.Add(prioritySliders[i]);

				priorityTypeSliders[i] = new NumericUpDown();
				priorityTypeSliders[i].Location = new Point(130, 11 + i * 26);
				priorityTypeSliders[i].Size = new Size(56, 20);
				priorityTypeSliders[i].Minimum = 0;
				priorityTypeSliders[i].Maximum = 2;
				priorityTypeSliders[i].Visible = true;
				priorityTypeSliders[i].ValueChanged += new EventHandler(priorityTypeSlider_ValueChanged);

				priorityGroupBox.Controls.Add(priorityTypeSliders[i]);

				priorityDescriptionLabels[i] = new Label();
				priorityDescriptionLabels[i].Location = new Point(195, 13 + i * 26);
				priorityDescriptionLabels[i].Visible = true;
				
				priorityGroupBox.Controls.Add(priorityDescriptionLabels[i]);
			}
			LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
		}

		void priorityTypeSlider_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				int numPositions = Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length;
				int index;
				for (index = 0; index < numPositions; index++)
				{
					if (sender == priorityTypeSliders[index])
					{
						break;
					}
				}

				model.CoachModel.GetCurrentCoachSliders().Values[index].PriorityType = (int)priorityTypeSliders[index].Value;
				priorityDescriptionLabels[index].Text = DecodePriorityType((CoachSliderPlayerPositions)index, (int)priorityTypeSliders[index].Value);
			}
		}

		void prioritySlider_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				int numPositions = Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length;
				int index;
				for (index = 0; index < numPositions; index++)
				{
					if (sender == prioritySliders[index])
					{
						break;
					}
				}

				model.CoachModel.GetCurrentCoachSliders().Values[index].Priority = (int)prioritySliders[index].Value;
				
			}
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

		private void coachCareerWins_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.CareerWins = (int)coachCareerWins.Value;
			}
		}

		private void coachCareerLoses_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.CareerLoses = (int)coachCareerLoses.Value;
			}
		}

		private void coachCareerTies_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.CareerTies = (int)coachCareerTies.Value;
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

		

		private void threeFourButton_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveAlignment = false;
			}
		}

		private void fourThreeButton_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveAlignment = true;
			}
		}

		private void coachPassOff_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.OffensiveStrategy = (int)coachPassOff.Value;
				coachRunOff.Value = (int)(100 - coachPassOff.Value);
			}
		}

		private void coachPassDef_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveStrategy = (int)coachPassDef.Value;
				coachRunDef.Value = (int)(100 - coachPassDef.Value);
			}
		}

		private void rb2_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.RunningBack2Sub = (int)rb2.Value;
				rb1.Value = (int)(100 - rb2.Value);
			}
		}

		private void coachOffAggression_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.OffensiveAggression = (int)coachOffAggression.Value;
			}
		}

		private void coachDefAggression_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensiveAggression = (int)coachDefAggression.Value;
			}
		}
		
	}
}
