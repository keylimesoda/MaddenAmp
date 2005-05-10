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
 * colin.goudie@gmail.com
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
	public partial class PlayerEditControl : UserControl, IEditorForm
	{
		private EditorModel model = null;

		private bool isInitialising = false;

		public PlayerEditControl()
		{
			isInitialising = true;

			InitializeComponent();

			isInitialising = false;
		}

		public void LoadPlayerInfo(PlayerRecord record)
		{
			isInitialising = true;

			firstNameTextBox.Text = record.FirstName;
			lastNameTextBox.Text = record.LastName;
			string team = model.TeamModel.GetTeamNameFromTeamId(record.TeamId);
			if (team.Equals(EditorModel.UNKNOWN_TEAM_NAME))
			{
				teamComboBox.Enabled = false;
			}
			else
			{
				teamComboBox.Text = team;
			}

			positionComboBox.Text = positionComboBox.Items[record.PositionId].ToString();
			collegeComboBox.Text = collegeComboBox.Items[record.CollegeId].ToString();

			playerAge.Value = record.Age;
			if (record.JerseyNumber > 99)
			{
				//Must be a draft class, disable jersey number editing
				playerJerseyNumber.Enabled = false;
			}
			else
			{
				playerJerseyNumber.Value = record.JerseyNumber;
			}
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
			playerThrowingStyle.Text = playerThrowingStyle.Items[record.ThrowingStyle].ToString();

			playerMorale.Value = record.Morale;
			playerNFLIcon.Checked = record.NFLIcon;
			
			playerImportance.Value = record.Importance;
			
			playerProBowl.Checked = record.ProBowl;
			playerExperiencePoints.Value = record.XPPoints;
			playerContractLength.Value = record.ContractLength;
			playerContractYearsLeft.Value = record.ContractYearsLeft;
			playerSigningBonus.Value = (decimal)(record.SigningBonus / 100.0);
			playerTotalSalary.Value = (decimal)(record.TotalSalary / 100.0);

			LoadPlayerSalaries(record);
			
			//Set player Appearance
			playerFaceShape.Value = record.FaceShape;
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
			playerRearMuscle.Value = record.RearRearFat;
			playerBodyOverall.Value = record.BodyOverall;
			playerEquipmentThighPads.Value = (int)record.LegsThighPads;
			
			//Load Player equipment
			playerHairStyleCombo.Text = playerHairStyleCombo.Items[record.HairStyle].ToString();
			playerSkinColorCombo.Text = playerSkinColorCombo.Items[record.SkinType].ToString();
			playerHairColorCombo.Text = playerHairColorCombo.Items[record.HairColor].ToString();
			playerHelmetStyleCombo.Text = playerHelmetStyleCombo.Items[record.HelmetStyle].ToString();
			playerFaceMaskCombo.Text = playerFaceMaskCombo.Items[record.FaceMask].ToString();
			playerEyePaintCombo.Text = playerEyePaintCombo.Items[record.EyePaint].ToString();
			playerNeckRollCombo.Text = playerNeckRollCombo.Items[record.NeckRoll].ToString();
			playerVisorCombo.Text = playerVisorCombo.Items[record.Visor].ToString();
			playerMouthPieceCombo.Text = playerMouthPieceCombo.Items[record.MouthPiece].ToString();
			playerLeftElbowCombo.Text = playerLeftElbowCombo.Items[record.LeftElbow].ToString();
			playerRightElbowCombo.Text = playerRightElbowCombo.Items[record.RightElbow].ToString();
			playerLeftWristCombo.Text = playerLeftWristCombo.Items[record.LeftWrist].ToString();
			playerRightWristCombo.Text = playerRightWristCombo.Items[record.RightWrist].ToString();
			playerLeftHandCombo.Text = playerLeftHandCombo.Items[record.LeftHand].ToString();
			playerRightHandCombo.Text = playerRightHandCombo.Items[record.RightHand].ToString();
			playerSleevesCombo.Text = playerSleevesCombo.Items[record.Sleeves].ToString();
			playerLeftKneeCombo.Text = playerLeftKneeCombo.Items[record.LeftKnee].ToString();
			playerRightKneeCombo.Text = playerRightKneeCombo.Items[record.RightKnee].ToString();
			playerLeftAnkleCombo.Text = playerLeftAnkleCombo.Items[record.LeftAnkle % 4].ToString();
			playerRightAnkleCombo.Text = playerRightAnkleCombo.Items[record.RightAnkle % 4].ToString();
			playerNasalStripCombo.Text = playerNasalStripCombo.Items[record.NasalStrip].ToString();
			
			//Load Injury information
			InjuryRecord injury = model.PlayerModel.GetPlayersInjuryRecord(record.PlayerId);

			if (injury == null)
			{
				playerInjuryCombo.Enabled = false;
				playerInjuryCombo.Text = "";
				playerInjuryLength.Enabled = false;
				playerInjuryLength.Value = 0;
				playerRemoveInjuryButton.Enabled = false;
				playerInjuryReserve.Enabled = false;
				playerAddInjuryButton.Enabled = true;
				injuryLengthDescriptionTextBox.Enabled = false;
				injuryLengthDescriptionTextBox.Text = "";

			}
			else
			{
				playerInjuryCombo.Enabled = true;
				playerInjuryLength.Enabled = true;
				playerRemoveInjuryButton.Enabled = true;
				playerInjuryReserve.Enabled = true;
				playerAddInjuryButton.Enabled = false;
				playerInjuryCombo.Text = playerInjuryCombo.Items[injury.InjuryType].ToString();
				playerInjuryLength.Value = injury.InjuryLength;
				playerInjuryReserve.Checked = injury.InjuryReserve;
				injuryLengthDescriptionTextBox.Text = injury.LengthDescription;
			}

			isInitialising = false;
		}

		private void LoadPlayerSalaries(PlayerRecord record)
		{
			TeamRecord teamRecord = model.TeamModel.GetTeamRecord(record.TeamId);
			if (teamRecord == null)
			{
				playerCapRoom.Enabled = false;
				playerCapRoom.Text = "";
				playerCapHit.Enabled = false;
				playerCapHit.Text = "";
				playerTeamSalary.Enabled = false;
				playerTeamSalary.Text = "";
				playerContractLength.Enabled = false;
				playerContractLength.Value = 0;
				playerContractYearsLeft.Enabled = false;
				playerContractYearsLeft.Value = 0;
				playerSigningBonus.Enabled = false;
				playerSigningBonus.Value = 0;
				playerTotalSalary.Enabled = false;
				playerTotalSalary.Value = 0;
				return;
			}
			else
			{
				playerCapRoom.Enabled = true;
				playerCapHit.Enabled = true;
				playerTeamSalary.Enabled = true;
				playerContractLength.Enabled = true;
				playerContractYearsLeft.Enabled = true;
				playerSigningBonus.Enabled = true;
				playerTotalSalary.Enabled = true;
			}
			playerTeamSalary.Text = "" + ((double)teamRecord.Salary / 100.0);
			playerCapHit.Text = "" + ((double)record.CapHit / 100.0);
			if (model.FileType == MaddenFileType.FranchiseFile)
			{
				playerCapRoom.Text = "" + Math.Round((((double)model.SalaryCapModel.SalaryCap / 10000.0 - (double)(model.TeamModel.GetTeamRecord(record.TeamId).Salary)) / 100.0), 2);
			}
			playerYearlySalary0.Text = "" + ((double)record.GetSalaryAtYear(0) / 100.0);
			playerYearlySalary1.Text = "" + ((double)record.GetSalaryAtYear(1) / 100.0);
			playerYearlySalary2.Text = "" + ((double)record.GetSalaryAtYear(2) / 100.0);
			playerYearlySalary3.Text = "" + ((double)record.GetSalaryAtYear(3) / 100.0);
			playerYearlySalary4.Text = "" + ((double)record.GetSalaryAtYear(4) / 100.0);
			playerYearlySalary5.Text = "" + ((double)record.GetSalaryAtYear(5) / 100.0);
			playerYearlySalary6.Text = "" + ((double)record.GetSalaryAtYear(6) / 100.0);
			playerSigningBonusYear0.Text = "" + ((double)record.GetSigningBonusAtYear(0) / 100.0);
			playerSigningBonusYear1.Text = "" + ((double)record.GetSigningBonusAtYear(1) / 100.0);
			playerSigningBonusYear2.Text = "" + ((double)record.GetSigningBonusAtYear(2) / 100.0);
			playerSigningBonusYear3.Text = "" + ((double)record.GetSigningBonusAtYear(3) / 100.0);
			playerSigningBonusYear4.Text = "" + ((double)record.GetSigningBonusAtYear(4) / 100.0);
			playerSigningBonusYear5.Text = "" + ((double)record.GetSigningBonusAtYear(5) / 100.0);
			playerSigningBonusYear6.Text = "" + ((double)record.GetSigningBonusAtYear(6) / 100.0);
		}

		private void deletePlayerButton_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("Are you sure you want to delete this player?\r\n\r\nAlthough this player will disappear from the editor\r\nchanges will not take effect until you save.", "About to Delete " + model.PlayerModel.CurrentPlayerRecord.FirstName + " " + model.PlayerModel.CurrentPlayerRecord.LastName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				//Mark this record for deletion
				model.PlayerModel.CurrentPlayerRecord.SetDeleteFlag(true);
				LoadPlayerInfo(model.PlayerModel.GetNextPlayerRecord());
			}
		}

		private void createPlayerButton_Click(object sender, EventArgs e)
		{
			PlayerRecord newRecord = model.PlayerModel.CreateNewPlayerRecord();

			//Add the player to free agents
			newRecord.TeamId = EditorModel.FREE_AGENT_TEAM_ID;
			//Need to set unique PLAYER ID

			//Most variables start off at zero but some can't like height and weight so set them
			newRecord.Height = 75;

			LoadPlayerInfo(newRecord);
		}

		private void calculateEnhancement_Click(object sender, EventArgs e)
		{
			int currentOvr = model.PlayerModel.CurrentPlayerRecord.Overall;
			int desiredOvr = currentOvr;
			if (enhancementPercentage.Value > 0)
			{

			}
			else
			{

			}
			
		}

		#region IEditorForm Members

		public void InitialiseUI()
		{
			foreach (string teamname in model.TeamModel.GetTeamNames())
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

			if (model.FileType != MaddenFileType.FranchiseFile)
			{
				playerCapRoom.Visible = false;
				capRoomLabel.Visible = false;
				capRoomUnitLabel.Visible = false;
			}
			else
			{
				playerCapRoom.Visible = true;
				capRoomUnitLabel.Visible = true;
				capRoomLabel.Visible = true;
			}

			LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);

			
		}

		public void CleanUI()
		{
			teamComboBox.Items.Clear();
			filterPositionComboBox.Items.Clear();
			positionComboBox.Items.Clear();
			filterTeamComboBox.Items.Clear();
		}

		public EditorModel Model
		{
			set
			{
				model = value;
			}
		}

		#endregion

		#region Navigation Filter Functions

		private void rightButton_Click(object sender, EventArgs e)
		{
			LoadPlayerInfo(model.PlayerModel.GetNextPlayerRecord());
		}

		private void leftButton_Click(object sender, EventArgs e)
		{
			LoadPlayerInfo(model.PlayerModel.GetPreviousPlayerRecord());
		}

		private void teamCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (teamCheckBox.Checked)
			{
				model.PlayerModel.SetTeamFilter(filterTeamComboBox.SelectedItem.ToString());
				filterDraftClassCheckBox.Checked = false;
				//Generate a move next so it will filter
				model.PlayerModel.GetNextPlayerRecord();
				LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
			}
			else
			{
				model.PlayerModel.RemoveTeamFilter();
			}
		}

		private void positionCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (positionCheckBox.Checked)
			{
				model.PlayerModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);
				//Generate a move next so it will filter
				model.PlayerModel.GetNextPlayerRecord();
				LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
			}
			else
			{
				model.PlayerModel.RemovePositionFilter();
			}
		}
		
		private void filterDraftClassCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (filterDraftClassCheckBox.Checked)
			{
				model.PlayerModel.SetDraftClassFilter(true);
				//Make sure team isn't set
				teamCheckBox.Checked = false;
				//Generate a move next so it will filter
				model.PlayerModel.GetNextPlayerRecord();
				LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
			}
			else
			{
				model.PlayerModel.SetDraftClassFilter(false);
			}
		}

		private void filterTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (teamCheckBox.Checked)
			{
				model.PlayerModel.SetTeamFilter(filterTeamComboBox.SelectedItem.ToString());
				//Generate a move next so it will filter
				model.PlayerModel.GetNextPlayerRecord();
				LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
			}
		}

		private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (positionCheckBox.Checked)
			{
				model.PlayerModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);
				//Generate a move next so it will filter
				model.PlayerModel.GetNextPlayerRecord();
				LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
			}
		}

		#endregion

		#region Player General Functions

		private void firstNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.FirstName = firstNameTextBox.Text;
			}
		}

		private void lastNameTextBox_Leave(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LastName = lastNameTextBox.Text;
			}
		}

		private void playerAge_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Age = (int)playerAge.Value;
			}
		}

		private void teamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.TeamId = model.TeamModel.GetTeamIdFromTeamName(teamComboBox.Text);
			}
		}

		private void playerJerseyNumber_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.JerseyNumber = (int)playerJerseyNumber.Value;
			}
		}

		private void playerDominantHand_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.DominantHand = playerDominantHand.Checked;
			}
		}

		private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.PositionId = (int)positionComboBox.SelectedIndex;
			}
		}

		private void playerYearsPro_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.YearsPro = (int)playerYearsPro.Value;
			}
		}

		private void collegeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.CollegeId = collegeComboBox.SelectedIndex;
			}
		}

		#endregion

		#region Player Ratings Functions

		private void calculateOverallButton_Click(object sender, EventArgs e)
		{
			model.PlayerModel.CurrentPlayerRecord.Overall = model.PlayerModel.CurrentPlayerRecord.CalculateOverallRating(model.PlayerModel.CurrentPlayerRecord.PositionId);
			//Reload the overall rating
			playerOverall.Value = model.PlayerModel.CurrentPlayerRecord.Overall;
		}

		private void playerOverall_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Overall = (int)playerOverall.Value;
			}
		}

		private void playerSpeed_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Speed = (int)playerSpeed.Value;
			}
		}

		private void playerStrength_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Strength = (int)playerStrength.Value;
			}
		}

		private void playerAwareness_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Awareness = (int)playerAwareness.Value;
			}
		}

		private void playerAgility_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Agility = (int)playerAgility.Value;
			}
		}

		private void playerAcceleration_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Acceleration = (int)playerAcceleration.Value;
			}
		}

		private void playerCatching_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Catching = (int)playerCatching.Value;
			}
		}

		private void playerCarrying_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Carrying = (int)playerCarrying.Value;
			}
		}

		private void playerJumping_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Jumping = (int)playerJumping.Value;
			}
		}

		private void playerBreakTackle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.BreakTackle = (int)playerBreakTackle.Value;
			}
		}

		private void playerTackle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Tackle = (int)playerTackle.Value;
			}
		}

		private void playerThrowPower_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ThrowPower = (int)playerThrowPower.Value;
			}
		}

		private void playerThrowAccuracy_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ThrowAccuracy = (int)playerThrowAccuracy.Value;
			}
		}

		private void playerPassBlocking_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.PassBlocking = (int)playerPassBlocking.Value;
			}
		}

		private void playerRunBlocking_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RunBlocking = (int)playerRunBlocking.Value;
			}
		}

		private void playerKickPower_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.KickPower = (int)playerKickPower.Value;
			}
		}

		private void playerKickAccuracy_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.KickAccuracy = (int)playerKickAccuracy.Value;
			}
		}

		private void playerKickReturn_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.KickReturn = (int)playerKickReturn.Value;
			}
		}

		private void playerStamina_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Stamina = (int)playerStamina.Value;
			}
		}

		private void playerInjury_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Injury = (int)playerInjury.Value;
			}
		}

		private void playerToughness_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Toughness = (int)playerToughness.Value;
			}
		}

		private void playerNFLIcon_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.NFLIcon = playerNFLIcon.Checked;
			}
		}

		private void playerExperiencePoints_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.XPPoints = (int)playerExperiencePoints.Value;
			}
		}

		private void playerImportance_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Importance = (int)playerImportance.Value;
			}
		}

		private void playerMorale_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Morale = (int)playerMorale.Value;
			}
		}

		private void playerContractLength_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				//Before we set it make sure its ok
				if (playerContractLength.Value < playerContractYearsLeft.Value)
				{
					//not right
					playerContractLength.Value = playerContractYearsLeft.Value;
					return;
				}
				model.PlayerModel.CurrentPlayerRecord.ContractLength = (int)playerContractLength.Value;
				//The call above has recalculated the cap hit, we need to modify the Teams total salary
				model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary + model.PlayerModel.CurrentPlayerRecord.CapHitDifference;
				LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
			}
		}

		private void playerContractYearsLeft_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				//Before we set it make sure its ok
				if (playerContractYearsLeft.Value > playerContractLength.Value)
				{
					//not right
					playerContractYearsLeft.Value = playerContractLength.Value;
					return;
				}
				model.PlayerModel.CurrentPlayerRecord.ContractYearsLeft = (int)playerContractYearsLeft.Value;
				//The call above has recalculated the cap hit, we need to modify the Teams total salary
				model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary + model.PlayerModel.CurrentPlayerRecord.CapHitDifference;
				LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
			}
		}

		private void playerProBowl_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ProBowl = playerProBowl.Checked;
			}
		}

		private void playerSigningBonus_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.SigningBonus = (int)(playerSigningBonus.Value * 100);
				//The call above has recalculated the cap hit, we need to modify the Teams total salary
				model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary + model.PlayerModel.CurrentPlayerRecord.CapHitDifference;
				LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
				
			}
		}

		private void playerTotalSalary_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.TotalSalary = (int)(playerTotalSalary.Value * 100);
				//The call above has recalculated the cap hit, we need to modify the Teams total salary
				model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).Salary + model.PlayerModel.CurrentPlayerRecord.CapHitDifference;
				LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
			}
		}

		#endregion

		#region Player Appearance Functions

		private void playerHairColorCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.HairColor = playerHairColorCombo.SelectedIndex;
			}
		}

		private void playerWeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Weight = (int)playerWeight.Value - 160;
			}
		}

		private void playerHeightComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Height = (int)playerHeightComboBox.SelectedIndex + 65;
			}
		}

		private void playerBodyOverall_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.BodyOverall = (int)playerBodyOverall.Value;
			}
		}

		private void playerBodyWeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.BodyWeight = (int)playerBodyWeight.Value;
			}
		}

		private void playerBodyMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.BodyMuscle = (int)playerBodyMuscle.Value;
			}
		}

		private void playerBodyFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.BodyFat = (int)playerBodyFat.Value;
			}
		}

		private void playerEquipmentShoes_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EquipmentShoes = (int)playerEquipmentShoes.Value;
			}
		}

		private void playerEquipmentPadHeight_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EquipmentPadHeight = (int)playerEquipmentPadHeight.Value;
			}
		}

		private void playerEquipmentPadWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EquipmentPadWidth = (int)playerEquipmentPadWidth.Value;
			}
		}

		private void playerEquipmentPadShelf_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EquipmentPadShelf = (int)playerEquipmentPadShelf.Value;
			}
		}

		private void playerEquipmentFlakJacket_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EquipmentFlakJacket = (int)playerEquipmentFlakJacket.Value;
			}
		}

		private void playerArmsMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ArmsMuscle = (int)playerArmsMuscle.Value;
			}
		}

		private void playerArmsFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ArmsFat = (int)playerArmsFat.Value;
			}
		}

		private void playerLegsThighMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LegsThighMuscle = (int)playerLegsThighMuscle.Value;
			}
		}

		private void playerLegsThighFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LegsThighFat = (int)playerLegsThighFat.Value;
			}
		}

		private void playerLegsCalfMuscle_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LegsCalfMuscle = (int)playerLegsCalfMuscle.Value;
			}
		}

		private void playerLegsCalfFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LegsCalfFat = (int)playerLegsCalfFat.Value;
			}
		}

		private void playerRearRearFat_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RearRearFat = (int)playerRearRearFat.Value;
			}
		}

		private void playerRearShape_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RearShape = (int)playerRearShape.Value;
			}
		}

		private void playerThrowingStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.ThrowingStyle = playerThrowingStyle.SelectedIndex;
			}
		}

		private void playerSkinColorCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.SkinType = playerSkinColorCombo.SelectedIndex;
			}
		}

		private void playerFaceShape_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.FaceShape = (int)playerFaceShape.Value;
			}
		}

		private void playerHairStyleCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.HairStyle = playerHairStyleCombo.SelectedIndex;
			}
		}

		#endregion

		#region Player Equipment Injury Functions

		private void playerAddInjuryButton_Click(object sender, EventArgs e)
		{
			InjuryRecord injRec = null;
			try
			{
				injRec = model.PlayerModel.CreateNewInjuryRecord();
			}
			catch (ApplicationException err)
			{
				MessageBox.Show("Error adding Injury\r\n" + err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			injRec.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
			injRec.TeamId = model.PlayerModel.CurrentPlayerRecord.TeamId;
			injRec.InjuryLength = 0;
			injRec.InjuryReserve = false;
			injRec.InjuryType = 0;

			LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
		}

		private void playerRemoveInjuryButton_Click(object sender, EventArgs e)
		{
			//Mark the record for deletion
			model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).SetDeleteFlag(true);

			LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
		}

		private void playerInjuryCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryType = playerInjuryCombo.SelectedIndex;
			}
		}

		private void playerInjuryReserve_CheckedChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryReserve = playerInjuryReserve.Checked;
			}
		}

		private void playerInjuryLength_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryLength = (int)playerInjuryLength.Value;
				injuryLengthDescriptionTextBox.Text = model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).LengthDescription;
			}
		}

		private void playerEyePaintCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.EyePaint = playerEyePaintCombo.SelectedIndex;
			}
		}

		private void playerNeckRollCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.NeckRoll = playerNeckRollCombo.SelectedIndex;
			}
		}

		private void playerVisorCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Visor = playerVisorCombo.SelectedIndex;
			}
		}

		private void playerMouthPieceCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.MouthPiece = playerMouthPieceCombo.SelectedIndex;
			}
		}

		private void playerLeftElbowCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LeftElbow = playerLeftElbowCombo.SelectedIndex;
			}
		}

		private void playerRightElbowCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RightElbow = playerRightElbowCombo.SelectedIndex;
			}
		}

		private void playerLeftWristCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LeftWrist = playerLeftWristCombo.SelectedIndex;
			}
		}

		private void playerRightWristCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RightWrist = playerRightWristCombo.SelectedIndex;
			}
		}

		private void playerLeftHandCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LeftHand = playerLeftHandCombo.SelectedIndex;
			}
		}

		private void playerRightHandCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RightHand = playerRightHandCombo.SelectedIndex;
			}
		}

		private void playerSleevesCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.Sleeves = playerSleevesCombo.SelectedIndex;
			}
		}

		private void playerLeftKneeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LeftKnee = playerLeftKneeCombo.SelectedIndex;
			}
		}

		private void playerRightKneeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RightKnee = playerRightKneeCombo.SelectedIndex;
			}
		}

		private void playerLeftAnkleCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LeftAnkle = playerLeftAnkleCombo.SelectedIndex;
			}
		}

		private void playerRightAnkleCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.RightAnkle = playerRightAnkleCombo.SelectedIndex;
			}
		}

		private void playerNasalStripCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.NasalStrip = playerNasalStripCombo.SelectedIndex;
			}
		}
		
		private void playerHelmetStyleCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.HelmetStyle = playerHelmetStyleCombo.SelectedIndex;
			}
		}

		private void playerFaceMaskCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.FaceMask = playerFaceMaskCombo.SelectedIndex;
			}
		}

		#endregion

		private void playerEquipmentThighPads_ValueChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				model.PlayerModel.CurrentPlayerRecord.LegsThighPads = (int)playerEquipmentThighPads.Value;
			}
		}


	}
}
