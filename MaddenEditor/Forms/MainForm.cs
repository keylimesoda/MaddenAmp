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
 * http://gommo.homelinux.net             colin.goudie@gmail.com
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Domain;

namespace MaddenEditor.Forms
{
    public partial class MainForm : Form
    {
		private RosterModel model = null;
		private string fileToLoad;
		private bool isInitialising = false;

        public MainForm()
        {
			isInitialising = true;
            InitializeComponent();
			
			tabControl.Visible = false;
			searchToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;

			isInitialising = false;
        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			CheckSave();

			Application.Exit();
        }

		private void CheckSave()
		{
			if (model != null && model.Dirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to currently opened file?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					model.Save();
				}
				else
				{

				}
			}

			if (model != null)
			{
				model.Shutdown();
			}	
			model = null;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckSave();

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "ros";
			dialog.Filter = "Madden Roster files (*.ros)|*.ros|Madden Franchise files (*.fra)|*.fra";
			dialog.Multiselect = false;
			dialog.ShowDialog();
			if (dialog.FileNames.Length > 0)
			{
				foreach (string filename in dialog.FileNames)
				{
					if (filename.Contains("fra"))
					{
						MessageBox.Show("This version currently does not support loading of Franchise files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					fileToLoad = filename;
					// Insert code here to process the files.
					try
					{
						this.Cursor = Cursors.WaitCursor;

						statusStrip.Visible = true;

						rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);
						rosterFileLoaderThread.RunWorkerAsync();
						
						break;
						//Now the model is opened.

					}
					catch (ApplicationException err)
					{
						MessageBox.Show(err.ToString(), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		void rosterFileLoaderThread_DoWork(object sender, DoWorkEventArgs e)
		{
			model = new RosterModel(fileToLoad, this);
		}

		public void updateProgress(int percentage)
		{
			rosterFileLoaderThread.ReportProgress(percentage);
		}

		private void InitialiseData()
		{
			foreach(string teamname in model.GetTeamNames())
			{
				teamComboBox.Items.Add(teamname);
				filterTeamComboBox.Items.Add(teamname);
			}

			foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
			{
				positionComboBox.Items.Add(pos);
				filterPositionComboBox.Items.Add(pos);
			}

			filterPositionComboBox.Text = filterPositionComboBox.Items[0].ToString();
			filterTeamComboBox.Text = filterTeamComboBox.Items[0].ToString();

			LoadPlayerInfo(model.CurrentPlayerRecord);
		}

		private void LoadPlayerInfo(PlayerRecord record)
		{
			isInitialising = true;

			firstNameTextBox.Text = record.FirstName;
			lastNameTextBox.Text = record.LastName;
			teamComboBox.Text = model.GetTeamNameFromTeamId(record.TeamId);
			positionComboBox.Text = positionComboBox.Items[record.PositionId].ToString();
			//collegeComboBox.Text = collegeComboBox.Items[record.CollegeId + 4].ToString();
			Console.WriteLine("number of colleges = " + collegeComboBox.Items.Count);

			playerAge.Value = record.Age;
			playerJerseyNumber.Value = record.JerseyNumber;
			playerYearsPro.Value = record.YearsPro;
			playerWeight.Value = record.Weight + 160;
			playerHeightComboBox.SelectedIndex = record.Height - 65;
			playerOverall.Value = record.Overall;
			playerDominantHand.Checked = record.DominantHand;
			
			playerSpeed.Value = record.Speed;
			playerStrength.Value = record.Strength;
			playerAwareness.Value = record.Awareness;
			playerAgility.Value = record.Agility;
			playerAcceleration.Value = record.Acceleration;
			playerCatching.Value = record.Catching;
			playerCarrying.Value = record.Carrying;
			playerJumping.Value = record.Jumping;
			playerBreakTackle.Value = record.BreakTackle;
			playerTackle.Value = record.Tackle;
			playerThrowPower.Value = record.ThrowPower;
			playerThrowAccuracy.Value = record.ThrowAccuracy;
			playerPassBlocking.Value = record.PassBlocking;
			playerRunBlocking.Value = record.RunBlocking;
			playerKickPower.Value = record.KickPower;
			playerKickAccuracy.Value = record.KickAccuracy;
			playerKickReturn.Value = record.KickReturn;
			playerStamina.Value = record.Stamina;
			playerInjury.Value = record.Injury;
			playerToughness.Value = record.Toughness;

			playerMorale.Value = record.Morale;
			playerImportance.Value = record.Importance;
			playerNFLIcon.Checked = record.NFLIcon;
			playerProBowl.Checked = record.ProBowl;
			playerExperiencePoints.Value = record.XPPoints;
			playerContractLength.Value = record.ContractLength;
			playerContractYearsLeft.Value = record.ContractYearsLeft;
			playerSigningBonus.Value = (decimal)(record.SigningBonus/100.0);

			//Set player Appearance
			playerBodyWeight.Value = record.BodyWeight;
			playerBodyMuscle.Value = record.BodyMuscle;
			playerBodyFat.Value = record.BodyFat;
			playerEquipmentShoes.Value = record.EquipmentShoes;
			playerEquipmentPadHeight.Value = record.EquipmentPadHeight;
			playerEquipmentPadWidth.Value = record.EquipmentPadWidth;
			playerEquipmentPadShelf.Value = record.EquipmentPadShelf;
			playerEquipmentFlakJacket.Value = record.EquipmentFlakJacket;
			playerArmsMuscle.Value = record.ArmsMuscle;
			playerArmsFat.Value = record.ArmsFat;
			playerLegsThighMuscle.Value = record.LegsThighMuscle;
			playerLegsThighFat.Value = record.LegsThighFat;
			playerLegsCalfMuscle.Value = record.LegsCalfMuscle;
			playerLegsCalfFat.Value = record.LegsCalfFat;
			playerRearRearFat.Value = record.RearRearFat;
			playerRearShape.Value = record.RearShape;

			isInitialising = false;
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckSave();

			//Now clean up ready for reloading
			teamComboBox.Items.Clear();
			filterPositionComboBox.Items.Clear();
			positionComboBox.Items.Clear();
			filterTeamComboBox.Items.Clear();

			tabControl.Visible = false;
			searchToolStripMenuItem.Visible = false;

		}

		private void rosterFileLoaderThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			isInitialising = true;

			try
			{
				InitialiseData();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}

			tabControl.Visible = true;
			searchToolStripMenuItem.Visible = true;
			statusStrip.Visible = false;
			toolStripProgressBar.Value = 0;
			this.Cursor = Cursors.Default;

			isInitialising = false;
		}

		private void rosterFileLoaderThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			toolStripProgressBar.Value = e.ProgressPercentage;
		}

		private void rightButton_Click(object sender, EventArgs e)
		{
			LoadPlayerInfo(model.GetNextPlayerRecord());
		}

		private void leftButton_Click(object sender, EventArgs e)
		{
			LoadPlayerInfo(model.GetPreviousPlayerRecord());
		}

		private void teamCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (teamCheckBox.Checked)
			{
				model.SetTeamFilter(filterTeamComboBox.SelectedItem.ToString());
			}
			else
			{
				model.RemoveTeamFilter();
			}

			LoadPlayerInfo(model.CurrentPlayerRecord);
		}

		private void positionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (positionCheckBox.Checked)
			{
				model.SetPositionFilter(filterPositionComboBox.SelectedIndex);
			}
			else
			{
				model.RemovePositionFilter();
			}

			LoadPlayerInfo(model.CurrentPlayerRecord);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CheckSave();

			Application.Exit();
		}
		
		public bool Dirty
		{
			set
			{
				saveToolStripMenuItem.Enabled = value;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			model.Save();
		}

		private void playerSpeed_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Speed = (int)playerSpeed.Value;
			}
		}

		private void firstNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.FirstName = firstNameTextBox.Text;
			}
		}

		private void lastNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.LastName = lastNameTextBox.Text;
			}
		}

		private void playerAge_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Age = (int)playerAge.Value;
			}
		}

		private void teamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.TeamId = model.GetTeamIdFromTeamName(teamComboBox.Text);
			}
		}

		private void playerJerseyNumber_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.JerseyNumber = (int)playerJerseyNumber.Value;
			}
		}

		private void playerDominantHand_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.DominantHand = playerDominantHand.Checked;
			}
		}

		private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.PositionId = (int)positionComboBox.SelectedIndex;
			}
		}

		private void playerYearsPro_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.YearsPro = (int)playerYearsPro.Value;
			}
		}

		private void playerOverall_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Overall = (int)playerOverall.Value;
			}
		}

		private void playerStrength_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Strength = (int)playerStrength.Value;
			}
		}

		private void playerAwareness_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Awareness = (int)playerAwareness.Value;
			}
		}

		private void playerAgility_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Agility = (int)playerAgility.Value;
			}
		}

		private void playerAcceleration_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Acceleration = (int)playerAcceleration.Value;
			}
		}

		private void playerCatching_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Catching = (int)playerCatching.Value;
			}
		}

		private void playerCarrying_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Carrying = (int)playerCarrying.Value;
			}
		}

		private void playerJumping_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Jumping = (int)playerJumping.Value;
			}
		}

		private void playerBreakTackle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.BreakTackle = (int)playerBreakTackle.Value;
			}
		}

		private void playerTackle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Tackle = (int)playerTackle.Value;
			}
		}

		private void playerThrowPower_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.ThrowPower = (int)playerThrowPower.Value;
			}
		}

		private void playerThrowAccuracy_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.ThrowAccuracy = (int)playerThrowAccuracy.Value;
			}
		}

		private void playerPassBlocking_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.PassBlocking = (int)playerPassBlocking.Value;
			}
		}

		private void playerRunBlocking_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.RunBlocking = (int)playerRunBlocking.Value;
			}
		}

		private void playerKickPower_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.KickPower = (int)playerKickPower.Value;
			}
		}

		private void playerKickAccuracy_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.KickAccuracy = (int)playerKickAccuracy.Value;
			}
		}

		private void playerKickReturn_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.KickReturn = (int)playerKickReturn.Value;
			}
		}

		private void playerStamina_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Stamina = (int)playerStamina.Value;
			}
		}

		private void playerInjury_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Injury = (int)playerInjury.Value;
			}
		}

		private void playerToughness_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Toughness = (int)playerToughness.Value;
			}
		}

		private void playerWeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Weight = (int)playerWeight.Value - 160;
			}
		}

		private void playerHeightComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Height = (int)playerHeightComboBox.SelectedIndex + 65;
			}
		}

		private void playerNFLIcon_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.NFLIcon = playerNFLIcon.Checked;
			}
		}

		private void playerExperiencePoints_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.XPPoints = (int)playerExperiencePoints.Value;
			}
		}

		private void playerImportance_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Importance = (int)playerImportance.Value;
			}
		}

		private void playerMorale_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.Morale = (int)playerMorale.Value;
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox form = new AboutBox();
			form.Show(this);
		}

		private void playerBodyOverall_ValueChanged(object sender, EventArgs e)
		{

		}

		private void playerBodyWeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.BodyWeight = (int)playerBodyWeight.Value;
			}
		}

		private void playerBodyMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.BodyMuscle = (int)playerBodyMuscle.Value;
			}
		}

		private void playerBodyFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.BodyFat = (int)playerBodyFat.Value;
			}
		}

		private void playerEquipmentShoes_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.EquipmentShoes = (int)playerEquipmentShoes.Value;
			}
		}

		private void playerEquipmentPadHeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.EquipmentPadHeight = (int)playerEquipmentPadHeight.Value;
			}
		}

		private void playerEquipmentPadWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.EquipmentPadWidth = (int)playerEquipmentPadWidth.Value;
			}
		}

		private void playerEquipmentPadShelf_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.EquipmentPadShelf = (int)playerEquipmentPadShelf.Value;
			}
		}

		private void playerEquipmentFlakJacket_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.EquipmentFlakJacket = (int)playerEquipmentFlakJacket.Value;
			}
		}

		private void playerArmsMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.ArmsMuscle = (int)playerArmsMuscle.Value;
			}
		}

		private void playerArmsFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.ArmsFat = (int)playerArmsFat.Value;
			}
		}

		private void playerLegsThighMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.LegsThighMuscle = (int)playerLegsThighMuscle.Value;
			}
		}

		private void playerLegsThighFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.LegsThighFat = (int)playerLegsThighFat.Value;
			}
		}

		private void playerLegsCalfMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.LegsCalfMuscle = (int)playerLegsCalfMuscle.Value;
			}
		}

		private void playerLegsCalfFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.LegsCalfFat = (int)playerLegsCalfFat.Value;
			}
		}

		private void playerRearRearFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.RearRearFat = (int)playerRearRearFat.Value;
			}
		}

		private void playerRearShape_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.CurrentPlayerRecord.RearShape = (int)playerRearShape.Value;
			}
		}


	


    }
}