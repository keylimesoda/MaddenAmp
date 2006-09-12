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
	public partial class CoachEditControl : UserControl, IEditorForm
	{
		private bool isInitialising = false;

		private NumericUpDown[] prioritySliders = null;
		private NumericUpDown[] priorityTypeSliders = null;
		private Label[] priorityDescriptionLabels = null;

		private EditorModel model = null;

		private CoachRecord lastLoadedRecord = null;

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

                // TO FIX not working right for 2007
                // Shows Unemployed coaches and lists Positions as Head Coaches etc...
                                
                coachesPositionCombo.Text = coachesPositionCombo.Items[record.Position].ToString(); ;

                TeamRecord team = model.TeamModel.GetTeamRecord(record.TeamId);
                                
                cbTeamCombo.SelectedItem = (object)team;

                coachAge.Value = (int)record.Age;
                cbSkinColor.SelectedIndex = (int)record.SkinColor;
                coachpic.Value = (int)record.Coachpic;
                coachSalary.Value = (decimal)((double)record.Salary / 100.0);
                coachyearsleft.Value = (int)record.CoachYL;
                coachQB.Value = (int)record.CoachQB;
                coachRB.Value = (int)record.CoachRB;
                coachWR.Value = (int)record.CoachWR;
                coachOL.Value = (int)record.CoachOL;
                coachDL.Value = (int)record.CoachDL;
                coachLB.Value = (int)record.CoachLB;
                coachDB.Value = (int)record.CoachDB;
                coachKS.Value = (int)record.CoachKS;
                coachPS.Value = (int)record.CoachPS;
                      
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
                rb2.Value = (int)(100 - record.RunningBack2Sub);
                rb1.Value = (int)(record.RunningBack2Sub);
                coachDefAggression.Value = record.DefensiveAggression;
                coachOffAggression.Value = record.OffensiveAggression;

                //Priorities (NOTE: Madden 2007 rosters don't have coach sliders)
                // Temp Fixed for 2007 files and backwards compatible
                
                if (model.FileVersion != MaddenFileVersion.Ver2007)
                                
                    coachDefensivePlaybook.SelectedIndex = (int)record.DefensivePlaybook;

                    bool priorityMatches = false;

                    SortedList<int, CoachPrioritySliderRecord> priorites = null;

                    if (model.FileType != MaddenFileType.RosterFile && model.FileVersion != MaddenFileVersion.Ver2007)
                    {
                        priorites = model.CoachModel.GetCurrentCoachSliders();
                        int priorityCount = Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length;
                        priorityMatches = (priorityCount != priorites.Count);
                    }

                
                    if 
                        (!priorityMatches & model.FileVersion != MaddenFileVersion.Ver2007)
                    {
                        int index = 0;
                        foreach (CoachPrioritySliderRecord priorRecord in priorites.Values)
                        {
                            prioritySliders[index].Value = priorRecord.Priority;
                            priorityTypeSliders[index].Value = priorRecord.PriorityType;
                            priorityDescriptionLabels[index].Text = DecodePriorityType((CoachSliderPlayerPositions)index, priorRecord.PriorityType);
                            prioritySliders[index].Enabled = true;
                            priorityTypeSliders[index].Enabled = true;
                            index++;
                        }
                    }
                                        
                    else
                    {

                        for (int i = 0; i < Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length; i++)
                        {
                            prioritySliders[i].Value = 0;
                            priorityTypeSliders[i].Value = 0;
                            priorityDescriptionLabels[i].Text = "";
                            prioritySliders[i].Enabled = false;
                            priorityTypeSliders[i].Enabled = false;
                        }

                    }

                    }
                    
            catch (Exception e)
            {
                MessageBox.Show("Exception Occured loading this Coach:\r\n" + e.ToString(), "Exception Loading Coach", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadCoachInfo(lastLoadedRecord);
                return;
            }
			finally
			{
				isInitialising = false;
			}
			lastLoadedRecord = record;
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
				if (model.CoachModel.ChangeCoachPosition((MaddenCoachPosition)coachesPositionCombo.SelectedIndex))
				{
					LoadCoachInfo(model.CoachModel.CurrentCoachRecord);
				}
				else
				{
					coachesPositionCombo.SelectedIndex = model.CoachModel.CurrentCoachRecord.Position;
				}
			}
		}

		private void coachTeamCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.TeamId = ((TeamRecord)cbTeamCombo.SelectedItem).TeamId;
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
			isInitialising = true;
			foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				cbTeamCombo.Items.Add(rec);
				filterTeamComboBox.Items.Add(rec);
			}

			filterPositionComboBox.SelectedIndex = 0;
			filterTeamComboBox.SelectedIndex = 0;

			foreach (GenericRecord rec in model.TeamModel.DefensivePlaybookList)
			{
				coachDefensivePlaybook.Items.Add(rec);
			}
                  
            coachDefensivePlaybook.SelectedIndex = 0;

			//Create priority controls
			int numPositions = Enum.GetNames(typeof(CoachSliderPlayerPositions)).Length;
			prioritySliders = new NumericUpDown[numPositions];
			priorityTypeSliders = new NumericUpDown[numPositions];
			priorityDescriptionLabels = new Label[numPositions];
			for (int i = 0; i < numPositions; i++)
			{
				prioritySliders[i] = new NumericUpDown();
				prioritySliders[i].Location = new Point(48, 22 + i * 26);
				prioritySliders[i].Size = new Size(86, 20);
				prioritySliders[i].Minimum = 0;
				prioritySliders[i].Maximum = 100;
				prioritySliders[i].Visible = true;
				prioritySliders[i].ValueChanged += new EventHandler(prioritySlider_ValueChanged);

				priorityGroupBox.Controls.Add(prioritySliders[i]);

				priorityTypeSliders[i] = new NumericUpDown();
				priorityTypeSliders[i].Location = new Point(140, 22 + i * 26);
				priorityTypeSliders[i].Size = new Size(56, 20);
				priorityTypeSliders[i].Minimum = 0;
				priorityTypeSliders[i].Maximum = 2;
				priorityTypeSliders[i].Visible = true;
				priorityTypeSliders[i].ValueChanged += new EventHandler(priorityTypeSlider_ValueChanged);

				priorityGroupBox.Controls.Add(priorityTypeSliders[i]);

				priorityDescriptionLabels[i] = new Label();
				priorityDescriptionLabels[i].Location = new Point(205, 25 + i * 26);
				priorityDescriptionLabels[i].Visible = true;
				
				priorityGroupBox.Controls.Add(priorityDescriptionLabels[i]);
			}
			isInitialising = false;
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

				SortedList<int, CoachPrioritySliderRecord> priorities = model.CoachModel.GetCurrentCoachSliders();
				if (priorities.Count == numPositions)
				{
					priorities.Values[index].PriorityType = (int)priorityTypeSliders[index].Value;
					priorityDescriptionLabels[index].Text = DecodePriorityType((CoachSliderPlayerPositions)index, (int)priorityTypeSliders[index].Value);
				}
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

				SortedList<int, CoachPrioritySliderRecord> priorities = model.CoachModel.GetCurrentCoachSliders();
				if (priorities.Count == numPositions)
				{
					priorities.Values[index].Priority = (int)prioritySliders[index].Value;
				}
			}
		}

		public void CleanUI()
		{
			cbTeamCombo.Items.Clear();
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
				model.CoachModel.CurrentCoachRecord.RunningBack2Sub = (int)(100 - rb2.Value);
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

		private void coachDefensivePlaybook_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.DefensivePlaybook = (int)coachDefensivePlaybook.SelectedIndex;
			}
		}

		private void cbSkinColor_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CoachModel.CurrentCoachRecord.SkinColor = (int)cbSkinColor.SelectedIndex;
			}
		}
		
	}
}
