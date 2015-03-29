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
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Db;


namespace MaddenEditor.Forms
{
    //  TO DO:  FIX previous bonuses owed, taken from cap room
    
    
    public partial class PlayerEditControl : UserControl, IEditorForm
    {
        private EditorModel model = null;

        private PlayerRecord lastLoadedRecord = null;

        private bool isInitialising = false;

        int year = 0;
                 
        
        public PlayerEditControl()
        {
            isInitialising = true;

            InitializeComponent();

            isInitialising = false;
        }

        private void SetNumericUpDown(NumericUpDown control, int value, string fieldname)
        {
            try
            {
                control.Value = value;
            }
            catch
            {
                string message = "Player's " + fieldname + " (" + value + ") is outside of the allowed range.\n\n";

                if (value > 120)
                {
                    message += "We recommend resetting the value to " + control.Minimum + ".";
                }
                else
                {
                    message += "We recommend resetting the value to " + control.Maximum + ".";
                }

                message += "\n\nHit \"Yes\" to reset to " + control.Maximum + "; hit \"No\" to reset to " + control.Minimum + ".";

                DialogResult dr = MessageBox.Show(message, "Repair Value", MessageBoxButtons.YesNo);

                isInitialising = false;
                if (dr == DialogResult.Yes)
                {
                    control.Value = control.Maximum;
                }
                else
                {
                    control.Value = control.Minimum;
                }
                isInitialising = true;
            }
        }
        
        public void LoadPlayerInfo(PlayerRecord record)
        {
            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            isInitialising = true;
            SuspendLayout();

            try
            {
                firstNameTextBox.Text = record.FirstName;
                lastNameTextBox.Text = record.LastName;

                TeamRecord team = model.TeamModel.GetTeamRecord(record.TeamId);
                teamComboBox.SelectedItem = (object)team;
                if (record.TeamId == 1014)
                    teamComboBox.Text = "Retired";

                positionComboBox.Text = positionComboBox.Items[record.PositionId].ToString();
                collegeComboBox.Text = collegeComboBox.Items[record.CollegeId].ToString();

                SetNumericUpDown(playerAge, record.Age, "Age");
                //playerAge.Value = record.Age;

                if (record.JerseyNumber > 99)
                {
                    //Must be a draft class, disable jersey number editing
                    playerJerseyNumber.Enabled = false;
                }
                else
                {
                    playerJerseyNumber.Value = record.JerseyNumber;
                }

                SetNumericUpDown(playerYearsPro, record.YearsPro, "Years Pro");
                SetNumericUpDown(playerWeight, record.Weight + 160, "Weight");

                playerHeightComboBox.SelectedIndex = record.Height - 65;
                playerDominantHand.Checked = record.DominantHand;

                SetNumericUpDown(playerOverall, record.Overall, "Overall");
                SetNumericUpDown(playerSpeed, record.Speed, "Speed");
                SetNumericUpDown(playerStrength, record.Strength, "Strength");
                SetNumericUpDown(playerAwareness, record.Awareness, "Awareness");
                SetNumericUpDown(playerAgility, record.Agility, "Agility");
                SetNumericUpDown(playerAcceleration, record.Acceleration, "Acceleration");
                SetNumericUpDown(playerCatching, record.Catching, "Catching");
                SetNumericUpDown(playerCarrying, record.Carrying, "Carrying");
                SetNumericUpDown(playerJumping, record.Jumping, "Jumping");
                SetNumericUpDown(playerBreakTackle, record.BreakTackle, "Break Tackle");
                SetNumericUpDown(playerTackle, record.Tackle, "Tackle");
                SetNumericUpDown(playerThrowPower, record.ThrowPower, "Throw Power");
                SetNumericUpDown(playerThrowAccuracy, record.ThrowAccuracy, "Throw Accuracy");
                SetNumericUpDown(playerPassBlocking, record.PassBlocking, "Pass Blocking");
                SetNumericUpDown(playerRunBlocking, record.RunBlocking, "Run Blocking");
                SetNumericUpDown(playerKickPower, record.KickPower, "Kick Power");
                SetNumericUpDown(playerKickAccuracy, record.KickAccuracy, "Kick Accuracy");
                SetNumericUpDown(playerKickReturn, record.KickReturn, "Kick Return");
                SetNumericUpDown(playerStamina, record.Stamina, "Stamina");
                SetNumericUpDown(playerInjury, record.Injury, "Injury");
                SetNumericUpDown(playerToughness, record.Toughness, "Toughness");
                SetNumericUpDown(playerMorale, record.Morale, "Morale");
                SetNumericUpDown(playerImportance, record.Importance, "Importance");

                playerNFLIcon.Checked = record.NFLIcon;

                if (model.FileVersion >= MaddenFileVersion.Ver2005)
                {
                    //Load the player tendancy and reinitialise the combo
                    cbTendancy.Enabled = true;
                    cbTendancy.Items.Clear();
                    for (int i = 0; i < 3; i++)
                    {
                        cbTendancy.Items.Add(DecodeTendancy((MaddenPositions)record.PositionId, i));
                    }

                    cbTendancy.SelectedIndex = record.Tendancy;
                }
                else
                {
                    cbTendancy.Enabled = false;
                }

                if (model.FileVersion >= MaddenFileVersion.Ver2007)
                {
                    lblEgo.Visible = true;
                    playerEgo.Visible = true;
                    lblValue.Visible = true;
                    playerValue.Visible = true;
                    SetNumericUpDown(playerEgo, record.Ego, "Player Ego");
                    SetNumericUpDown(playerValue, record.PlayerValue, "Player Value");


                    if (record.PlayerRole > 42)
                        PlayerRolecomboBox.SelectedIndex = 43;
                    else PlayerRolecomboBox.SelectedIndex = record.PlayerRole;
                }
                else
                {
                    lblEgo.Visible = false;
                    playerEgo.Visible = false;
                    lblValue.Visible = false;
                    playerValue.Visible = false;
                    RoleLabel.Visible = false;
                    PlayerRolecomboBox.Visible = false;
                }

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                {                    
                    if (record.PlayerWeapon > 42)
                        PlayerWeaponcomboBox.SelectedIndex = 43;
                    else PlayerWeaponcomboBox.SelectedIndex = record.PlayerWeapon;
                }
                else
                {
                    WeaponLabel.Visible = false;
                    PlayerWeaponcomboBox.Visible = false;
                }
                

                playerProBowl.Checked = record.ProBowl;
                playerPortraitId.Value = record.PortraitId;
                playerContractLength.Value = record.ContractLength;
                playerContractYearsLeft.Value = record.ContractYearsLeft;
                playerSigningBonus.Value = (decimal)(record.SigningBonus / 100.0);
                playerTotalSalary.Value = (decimal)(record.TotalSalary / 100.0);
                

                try
                {
                    playerDraftRound.Value = (decimal)record.DraftRound;
                }
                catch (Exception error)
                {
                    error = error;
                    playerDraftRound.Value = 15;
                }
                try
                {
                    playerDraftRoundIndex.Value = (decimal)record.DraftRoundIndex;
                }
                catch (Exception error)
                {
                    error = error;
                    playerDraftRoundIndex.Value = 33;
                }

                LoadPlayerSalaries(record);

                //Set player Appearance
                cbFaceId.Value = (int)record.FaceId;
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
                //playerSkinColorCombo.Text = playerSkinColorCombo.Items[record.SkinType].ToString();
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

                //  Setup the Player's Stats
                if (model.FileType == MaddenFileType.FranchiseFile)
                    InitStatsYear(record);

                //  Display player portrait
                

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Occured loading this Player:\r\nCaused by " + e.Source + "\r\n" + e.ToString(), "Exception Loading Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadPlayerInfo(lastLoadedRecord);
                return;
            }
            finally
            {
                ResumeLayout();
                isInitialising = false;
            }
            lastLoadedRecord = record;
        }

        public void InitStatsYear(PlayerRecord record)
        {
            //  Get current Season and week
            int currentyear = model.FranchiseTime.Year;
            int currentweek = model.FranchiseTime.Week;

            // Season/Career stats combobox
            int year = 2003;    
            if (model.FileVersion == MaddenFileVersion.Ver2005)
                year = 2004;
            if (model.FileVersion == MaddenFileVersion.Ver2006)
                year = 2005;
            if (model.FileVersion == MaddenFileVersion.Ver2007)
                year = 2006;
            if (model.FileVersion == MaddenFileVersion.Ver2008)
                year = 2007;
            statsyear.Items.Clear();
            statsyear.Items.Add("Career");
            int endyear = currentyear + year;
            int startyear = endyear - record.YearsPro;
            for (int t = endyear; t > startyear; t--)
                statsyear.Items.Add(t);
            //  Set for Career stats
            statsyear.SelectedIndex = 0;
        }

        //  FIX THIS
        public void LoadPlayerStats(PlayerRecord record, int index)
        {
            isInitialising = true;

            bool career = false;
            if (index == 0)
                career = true;
            year = 0;

            int baseyear = 2003;
            if (model.FileVersion == MaddenFileVersion.Ver2005)
                baseyear = 2004;
            if (model.FileVersion == MaddenFileVersion.Ver2006)
                baseyear = 2005;
            if (model.FileVersion == MaddenFileVersion.Ver2007)
                baseyear = 2006;
            if (model.FileVersion == MaddenFileVersion.Ver2008)
                baseyear = 2007;
       
            if (index > 0)
                year = (int)statsyear.SelectedItem - baseyear;

            #region Offense Stats

            CareerStatsOffenseRecord careeroffensestats = model.PlayerModel.GetPlayersOffenseCareer(record.PlayerId);
            SeasonStatsOffenseRecord seasonoffense = model.PlayerModel.GetOffStats(record.PlayerId, year);

            // Set controls
            CareerOffenseGroupBox.Enabled = true;
            AddOStats.Enabled = false;            
            if (record.YearsPro > 0)
                statsyear.Enabled = true;
            else statsyear.Enabled = false;

            //  Set career stats
            if (career && careeroffensestats != null)
            {
                pass_att.Value = (int)careeroffensestats.Pass_att;
                pass_comp.Value = (int)careeroffensestats.Pass_comp;
                pass_yds.Value = (int)careeroffensestats.Pass_yds;
                pass_int.Value = (int)careeroffensestats.Pass_int;
                pass_long.Value = (int)careeroffensestats.Pass_long;
                pass_tds.Value = (int)careeroffensestats.Pass_tds;
                receiving_recs.Value = (int)careeroffensestats.Receiving_recs;
                receiving_drops.Value = (int)careeroffensestats.Receiving_drops;
                receiving_tds.Value = (int)careeroffensestats.Receiving_tds;
                receiving_yds.Value = (int)careeroffensestats.Receiving_yards;
                receiving_yac.Value = (int)careeroffensestats.Receiving_yac;
                receiving_long.Value = (int)careeroffensestats.Receiving_long;
                fumbles.Value = (int)careeroffensestats.Fumbles;
                rushingattempts.Value = (int)careeroffensestats.RushingAttempts;
                rushingyards.Value = (int)careeroffensestats.RushingYards;
                rushing_tds.Value = (int)careeroffensestats.Rushing_tds;
                rushing_long.Value = (int)careeroffensestats.Rushing_long;
                rushing_yac.Value = (int)careeroffensestats.Rushing_yac;
                rushing_20.Value = (int)careeroffensestats.Rushing_20;
                rushing_bt.Value = (int)careeroffensestats.Rushing_bt;
            }

            // Set season stats
            else if (seasonoffense != null && !career)
            {
                // set all the values of the numericupdown boxes
                pass_att.Value = (int)seasonoffense.SeaPassAtt;
                pass_comp.Value = (int)seasonoffense.SeaComp;
                pass_yds.Value = (int)seasonoffense.SeaPassYds;
                pass_int.Value = (int)seasonoffense.SeaPassInt;
                pass_long.Value = (int)seasonoffense.SeaPassLong;
                pass_tds.Value = (int)seasonoffense.SeaPassTd;
                receiving_recs.Value = (int)seasonoffense.SeaRec;
                receiving_drops.Value = (int)seasonoffense.SeaDrops;
                receiving_tds.Value = (int)seasonoffense.SeaRecTd;
                receiving_yds.Value = (int)seasonoffense.SeaRecYds;
                receiving_yac.Value = (int)seasonoffense.SeaRecYac;
                receiving_long.Value = (int)seasonoffense.SeaRecLong;
                fumbles.Value = (int)seasonoffense.SeaFumbles;
                rushingattempts.Value = (int)seasonoffense.SeaRushAtt;
                rushingyards.Value = (int)seasonoffense.SeaRushYds;
                rushing_tds.Value = (int)seasonoffense.SeaRushTd;
                rushing_long.Value = (int)seasonoffense.SeaRushLong;
                rushing_yac.Value = (int)seasonoffense.SeaRushYac;
                rushing_20.Value = (int)seasonoffense.SeaRush20;
                rushing_bt.Value = (int)seasonoffense.SeaRushBtk;

            }
            
            else
            {
                //  No career/season offense stats
                CareerOffenseGroupBox.Enabled = false;
                if (record.YearsPro > 0)
                    AddOStats.Enabled = true;
                else AddOStats.Enabled = false;                

                pass_att.Value = 0;
                pass_comp.Value = 0;
                pass_yds.Value = 0;
                pass_int.Value = 0;
                pass_long.Value = 0;
                pass_tds.Value = 0;
                receiving_recs.Value = 0;
                receiving_drops.Value = 0;
                receiving_tds.Value = 0;
                receiving_yds.Value = 0;
                receiving_yac.Value = 0;
                receiving_long.Value = 0;
                fumbles.Value = 0;
                rushingattempts.Value = 0;
                rushingyards.Value = 0;
                rushing_tds.Value = 0;
                rushing_long.Value = 0;
                rushing_yac.Value = 0;
                rushing_20.Value = 0;
                rushing_bt.Value = 0;
            }

            #endregion



            #region Offensive Line Stats

            CareerOLGroupBox.Enabled = true;
            AddOLStat.Enabled = false;

            CareerStatsOffensiveLineRecord careerOLstats = model.PlayerModel.GetPlayersOLCareer(record.PlayerId);
            SeasonStatsOffensiveLineRecord seaOLstats = model.PlayerModel.GetOLstats(record.PlayerId, year);

            if (careerOLstats != null && career || !career && seaOLstats != null)
            {
                if (career)
                {
                    pancakes.Value = careerOLstats.Pancakes;
                    sacksallowed.Value = careerOLstats.SacksAllowed;
                }
                else
                {
                    pancakes.Value = seaOLstats.Pancakes;
                    sacksallowed.Value = seaOLstats.SacksAllowed;
                }
            }

            else
            {
                if (record.YearsPro > 0)
                    AddOLStat.Enabled = true;
                CareerOLGroupBox.Enabled = false;
                pancakes.Value = 0;
                sacksallowed.Value = 0;
            }

            #endregion


            //  Defensive Stats

            CareerStatsDefenseRecord careerdefensestats = model.PlayerModel.GetPlayersDefenseCareer(record.PlayerId);
            SeasonStatsDefenseRecord seasondefensestats = model.PlayerModel.GetDefenseStats(record.PlayerId, year);

            CareerDefenseGroupBox.Enabled = true;
            AddDefStats.Enabled = false;

            if (career && careerdefensestats != null)
            {

                passesdefended.Value = careerdefensestats.PassesDefended;
                tackles.Value = careerdefensestats.Tackles;
                tacklesforloss.Value = careerdefensestats.TacklesForLoss;
                sacks.Value = careerdefensestats.Sacks;
                blocks.Value = careerdefensestats.Blocks;
                safeties.Value = careerdefensestats.Safeties;
                fumblesrecovered.Value = careerdefensestats.FumblesRecovered;
                fumblesforced.Value = careerdefensestats.FumblesForced;
                fumbleyards.Value = careerdefensestats.FumbleYards;
                fumbles_td.Value = careerdefensestats.Fumbles_td;
                def_int.Value = careerdefensestats.Def_int;
                int_long.Value = careerdefensestats.Int_long;
                int_td.Value = careerdefensestats.Int_td;
                int_yards.Value = careerdefensestats.Int_yards;
            }

            else if (!career && seasondefensestats != null)
            {
                passesdefended.Value = seasondefensestats.PassesDefended;
                tackles.Value = seasondefensestats.Tackles;
                tacklesforloss.Value = seasondefensestats.TacklesForLoss;
                sacks.Value = seasondefensestats.Sacks;
                blocks.Value = seasondefensestats.Blocks;
                safeties.Value = seasondefensestats.Safeties;
                fumblesrecovered.Value = seasondefensestats.FumblesRecovered;
                fumblesforced.Value = seasondefensestats.FumblesForced;
                fumbleyards.Value = seasondefensestats.FumbleYards;
                fumbles_td.Value = seasondefensestats.FumbleTDS;
                def_int.Value = seasondefensestats.Interceptions;
                int_long.Value = seasondefensestats.InterceptionLong;
                int_td.Value = seasondefensestats.InterceptionTDS;
                int_yards.Value = seasondefensestats.InterceptionYards;
            }

            else
            {
                CareerDefenseGroupBox.Enabled = false;

                if (record.YearsPro > 0)
                    AddDefStats.Enabled = true;
                else AddDefStats.Enabled = false;

                passesdefended.Value = 0;
                tackles.Value = 0;
                tacklesforloss.Value = 0;
                sacks.Value = 0;
                blocks.Value = 0;
                fumblesrecovered.Value = 0;
                fumblesforced.Value = 0;
                fumbleyards.Value = 0;
                fumbles_td.Value = 0;
                safeties.Value = 0;
                def_int.Value = 0;
                int_td.Value = 0;
                int_yards.Value = 0;
                int_long.Value = 0;
            }

            CareerGamesPlayedRecord careergamesplayed = model.PlayerModel.GetPlayersGamesCareer(record.PlayerId);
            SeasonGamesPlayedRecord seasongamesplayed = model.PlayerModel.GetSeasonGames(record.PlayerId, year);

            // Controls
            gamesplayed.Enabled = true;
            gamesstarted.Enabled = true;
            AddGamesStats.Enabled = false;

            if (career && careergamesplayed != null)
            {
                if (model.FileVersion == MaddenFileVersion.Ver2004)
                {
                    gamesplayed.Value = careergamesplayed.GamesPlayed04;
                    gamesstarted.Value = 0;
                    gamesstarted.Enabled = false;
                }
                else
                {
                    gamesplayed.Value = careergamesplayed.GamesPlayed;
                    gamesstarted.Value = careergamesplayed.GamesStarted;
                }

            }
            else if (!career && seasongamesplayed != null)
            {
                gamesplayed.Value = seasongamesplayed.GamesPlayed;
                gamesstarted.Value = seasongamesplayed.GamesStarted;
            }

            else
            {
                if (record.YearsPro > 0)
                    AddGamesStats.Enabled = true;
                else
                {
                    AddGamesStats.Enabled = false;
                    gamesstarted.Enabled = false;
                    gamesplayed.Enabled = false;
                }

                gamesplayed.Value = 0;
                gamesstarted.Value = 0;
            }

            // Career Punt Kick Stats

            CareerPuntKickRecord careerpuntkick = model.PlayerModel.GetPlayersCareerPuntKick(record.PlayerId);
            SeasonPuntKickRecord seasonpuntkick = model.PlayerModel.GetPuntKick(record.PlayerId, year);

            KickPuntGroupBox.Enabled = true;
            AddKPStats.Enabled = false;

            if (career && careerpuntkick != null)
            {
                // set kick punt stats = record
                fga.Value = careerpuntkick.Fga;
                fgm.Value = careerpuntkick.Fgm;
                fgbl.Value = careerpuntkick.Fgbl;
                fgl.Value = careerpuntkick.Fgl;
                xpa.Value = careerpuntkick.Xpa;
                xpm.Value = careerpuntkick.Xpm;
                xpb.Value = careerpuntkick.Xpb;
                fga_129.Value = careerpuntkick.Fga_129;
                fga_3039.Value = careerpuntkick.Fga_3039;
                fga_4049.Value = careerpuntkick.Fga_4049;
                fga_50.Value = careerpuntkick.Fga_50;
                fgm_129.Value = careerpuntkick.Fgm_129;
                fgm_3039.Value = careerpuntkick.Fgm_3039;
                fgm_4049.Value = careerpuntkick.Fgm_4049;
                fgm_50.Value = careerpuntkick.Fgm_50;
                puntatt.Value = careerpuntkick.Puntatt;
                puntblk.Value = careerpuntkick.Puntblk;
                puntin20.Value = careerpuntkick.Puntin20;
                puntlong.Value = careerpuntkick.Puntlong;
                puntny.Value = careerpuntkick.Puntny;
                punttb.Value = careerpuntkick.Punttb;
                puntyds.Value = careerpuntkick.Puntyds;
                touchbacks.Value = careerpuntkick.Touchbacks;
                kickoffs.Value = careerpuntkick.Kickoffs;

            }

            else if (!career && seasonpuntkick != null)
            {
                // set kick punt stats = record
                fga.Value = seasonpuntkick.Fga;
                fgm.Value = seasonpuntkick.Fgm;
                fgbl.Value = seasonpuntkick.Fgbl;
                fgl.Value = seasonpuntkick.Fgl;
                xpa.Value = seasonpuntkick.Xpa;
                xpm.Value = seasonpuntkick.Xpm;
                xpb.Value = seasonpuntkick.Xpb;
                fga_129.Value = seasonpuntkick.Fga_129;
                fga_3039.Value = seasonpuntkick.Fga_3039;
                fga_4049.Value = seasonpuntkick.Fga_4049;
                fga_50.Value = seasonpuntkick.Fga_50;
                fgm_129.Value = seasonpuntkick.Fgm_129;
                fgm_3039.Value = seasonpuntkick.Fgm_3039;
                fgm_4049.Value = seasonpuntkick.Fgm_4049;
                fgm_50.Value = seasonpuntkick.Fgm_50;
                puntatt.Value = seasonpuntkick.Puntatt;
                puntblk.Value = seasonpuntkick.Puntblk;
                puntin20.Value = seasonpuntkick.Puntin20;
                puntlong.Value = seasonpuntkick.Puntlong;
                puntny.Value = seasonpuntkick.Puntny;
                punttb.Value = seasonpuntkick.Punttb;
                puntyds.Value = seasonpuntkick.Puntyds;
                touchbacks.Value = seasonpuntkick.Touchbacks;
                kickoffs.Value = seasonpuntkick.Kickoffs;
            }

            else
            {
                if (record.YearsPro > 0)
                    AddKPStats.Enabled = true;
                //set all kick punt stats =0 and disable
                KickPuntGroupBox.Enabled = false;
                fga.Value = 0;
                fgm.Value = 0;
                fgbl.Value = 0;
                fgl.Value = 0;
                xpa.Value = 0;
                xpm.Value = 0;
                xpb.Value = 0;
                fga_129.Value = 0;
                fga_3039.Value = 0;
                fga_4049.Value = 0;
                fga_50.Value = 0;
                fgm_129.Value = 0;
                fgm_3039.Value = 0;
                fgm_4049.Value = 0;
                fgm_50.Value = 0;
                puntatt.Value = 0;
                puntyds.Value = 0;
                puntlong.Value = 0;
                puntin20.Value = 0;
                puntny.Value = 0;
                punttb.Value = 0;
                puntblk.Value = 0;
                touchbacks.Value = 0;
                kickoffs.Value = 0;
            }


            CareerPKReturnRecord careerpkreturn = model.PlayerModel.GetPlayersCareerPKReturn(record.PlayerId);
            SeasonPKReturnRecord seasonpkreturn = model.PlayerModel.GetPKReturn(record.PlayerId, year);

            KickPuntReturnGroupBox.Enabled = true;
            AddKRPRStats.Enabled = false;

            if (career && careerpkreturn != null)
            {
                // set return values = record
                kra.Value = careerpkreturn.Kra;
                kryds.Value = careerpkreturn.Kryds;
                krl.Value = careerpkreturn.Krl;
                krtd.Value = careerpkreturn.Krtd;
                pra.Value = careerpkreturn.Pra;
                pryds.Value = careerpkreturn.Pryds;
                prl.Value = careerpkreturn.Prl;
                prtd.Value = careerpkreturn.Prtd;
            }

            else if (!career && seasonpkreturn != null)
            {
                // set return values = record
                kra.Value = seasonpkreturn.Kra;
                kryds.Value = seasonpkreturn.Kryds;
                krl.Value = seasonpkreturn.Krl;
                krtd.Value = seasonpkreturn.Krtd;
                pra.Value = seasonpkreturn.Pra;
                pryds.Value = seasonpkreturn.Pryds;
                prl.Value = seasonpkreturn.Prl;
                prtd.Value = seasonpkreturn.Prtd;
            }

            else
            {
                if (record.YearsPro > 0)
                    AddKRPRStats.Enabled = true;

                KickPuntReturnGroupBox.Enabled = false;
                // set return values = 0
                kra.Value = 0;
                kryds.Value = 0;
                krl.Value = 0;
                krtd.Value = 0;
                pra.Value = 0;
                pryds.Value = 0;
                prl.Value = 0;
                prtd.Value = 0;
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
                PlayerRecord rec = model.PlayerModel.CurrentPlayerRecord;
                model.PlayerModel.DeletePlayerRecord(rec);
                LoadPlayerInfo(model.PlayerModel.GetNextPlayerRecord());
            }
        }

        // TO DO : Fix This, need to set all player info to some sort of defaults
        // before it is displayed.  Set player ID #  Need to reset everything to defaults


        private void createPlayerButton_Click(object sender, EventArgs e)
        {
            PlayerRecord newRecord = model.PlayerModel.CreateNewPlayerRecord();
            // Add the player to free agents
            newRecord.TeamId = EditorModel.FREE_AGENT_TEAM_ID;
            // Need to set unique PLAYER ID
            newRecord.PlayerId = EditorModel.totalplayers;
            // This sets unique POID
            newRecord.NFLID = newRecord.PlayerId + 30759;

            //Most variables start off at zero but some can't like height and weight so set them
            newRecord.Height = 72; // 6'0"
            newRecord.Weight = 40; // 200#
            model.PlayerModel.CurrentPlayerRecord = newRecord;
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
            foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {

                // Only add specific teams to the combo box, not AFC and NFC
                if (record.GetIntField("TGID") != 1010 && record.GetIntField("TGID") != 1011)
                {
                    teamComboBox.Items.Add(record);
                    filterTeamComboBox.Items.Add(record);
                }

            }
            // Add Retired to the team lists
            teamComboBox.Items.Add(EditorModel.RETIRED);
            filterTeamComboBox.Items.Add(EditorModel.RETIRED);

            foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
            {
                positionComboBox.Items.Add(pos);
                filterPositionComboBox.Items.Add(pos);
            }

            filterTeamComboBox.SelectedIndex = 0;
            filterPositionComboBox.SelectedIndex = 0;

            if (model.FileVersion >= MaddenFileVersion.Ver2006)
            {
                playerFaceShape.Enabled = false;
            }
            else
            {
                playerFaceShape.Enabled = true;
            }

            foreach (GenericRecord rec in model.PlayerModel.HelmetStyleList)
            {
                playerHelmetStyleCombo.Items.Add(rec);
            }

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

            if (model.FileVersion >= MaddenFileVersion.Ver2007)
            {
                RoleLabel.Visible = true;
                PlayerRolecomboBox.Visible = true;
                foreach (string role in Enum.GetNames(typeof(PlayerRoles)))
                {
                    PlayerRolecomboBox.Items.Add(role);
                }
            }
            else
            {
                RoleLabel.Visible = false;
                PlayerRolecomboBox.Visible = false;
            }

            if (model.FileVersion == MaddenFileVersion.Ver2008)
            {
                WeaponLabel.Visible = true;
                PlayerWeaponcomboBox.Visible = true;
                foreach (string role in Enum.GetNames(typeof(PlayerRoles)))
                {
                    PlayerWeaponcomboBox.Items.Add(role);
                }

            }
            else
            {
                WeaponLabel.Visible = false;
                PlayerWeaponcomboBox.Visible = false;
            }

            LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);


        }

        public void CleanUI()
        {
            teamComboBox.Items.Clear();
            filterPositionComboBox.Items.Clear();
            positionComboBox.Items.Clear();
            filterTeamComboBox.Items.Clear();
            PlayerRolecomboBox.Items.Clear();
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
                model.PlayerModel.SetTeamFilter(filterTeamComboBox.Text);
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
                model.PlayerModel.SetTeamFilter(filterTeamComboBox.Text);
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
                // add code to handle "Retired"
                if ((string)teamComboBox.Text != "Retired")
                    model.PlayerModel.ChangePlayersTeam(((TeamRecord)teamComboBox.SelectedItem));
                if ((string)teamComboBox.Text == "Retired")
                    model.PlayerModel.CurrentPlayerRecord.TeamId = 1014;
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
                //  Add more seasons to the players career
                InitStatsYear(model.PlayerModel.CurrentPlayerRecord);
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

        //  FIX NAME
        private void playerExperiencePoints_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.PortraitId = (int)playerPortraitId.Value;
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
                //model.PlayerModel.CurrentPlayerRecord.SkinType = playerSkinColorCombo.SelectedIndex;
            }
        }

        private void playerFaceShape_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                //model.PlayerModel.CurrentPlayerRecord.FaceShape = (int)playerFaceShape.Value;
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
            if (!isInitialising)
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

        private void playerDraftRound_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.DraftRound = (int)playerDraftRound.Value;
            }
        }

        private void playerDraftRoundIndex_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.DraftRoundIndex = (int)playerDraftRoundIndex.Value;
            }
        }

        private string DecodeTendancy(MaddenPositions pos, int type)
        {
            if (type == 2)
            {
                return "Balanced";
            }

            switch (pos)
            {
                case MaddenPositions.QB:
                    return (type == 0 ? "Pocket" : "Scrambling");
                case MaddenPositions.HB:
                    return (type == 0 ? "Power" : "Speed");
                case MaddenPositions.FB:
                case MaddenPositions.TE:
                    return (type == 0 ? "Blocking" : "Receiving");
                case MaddenPositions.WR:
                    return (type == 0 ? "Possession" : "Speed");
                case MaddenPositions.LT:
                case MaddenPositions.LG:
                case MaddenPositions.C:
                case MaddenPositions.RG:
                case MaddenPositions.RT:
                    return (type == 0 ? "Run Blocking" : "Pass Blocking");
                case MaddenPositions.LE:
                case MaddenPositions.RE:
                case MaddenPositions.DT:
                    return (type == 0 ? "Pass Rushing" : "Run Stopping");
                case MaddenPositions.LOLB:
                case MaddenPositions.MLB:
                case MaddenPositions.ROLB:
                    return (type == 0 ? "Coverage" : "Run Stopping");
                case MaddenPositions.CB:
                case MaddenPositions.SS:
                case MaddenPositions.FS:
                    return (type == 0 ? "Coverage" : "Hard Hitting");
                case MaddenPositions.K:
                case MaddenPositions.P:
                    return (type == 0 ? "Power" : "Accurate");
            }

            return "";
        }

        private void cbTendancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.Tendancy = cbTendancy.SelectedIndex;
            }
        }

        private void cbFaceId_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.FaceId = (int)cbFaceId.Value;
            }
        }


        //  TO DO :  some still need fixed
        #region Stats Functions

        #region Offense Stats

        private void pass_att_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_att = (int)pass_att.Value;
                else
                    model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaPassAtt = (int)pass_att.Value;
            }
        }

        private void pass_comp_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_comp = (int)pass_comp.Value;
                else
                    model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaComp = (int)pass_comp.Value;
            }
        }

        private void pass_yds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_yds = (int)pass_yds.Value;
                else
                    model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaPassYds = (int)pass_yds.Value;
            }
        }

        private void pass_tds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_tds = (int)pass_tds.Value;
                else
                    model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaPassTd = (int)pass_tds.Value;
            }
        }

        private void pass_int_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_int = (int)pass_int.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaPassInt = (int)pass_int.Value;
            }
        }

        private void pass_long_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_long = (int)pass_long.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaPassLong = (int)pass_long.Value;
            }
        }

        private void pass_sacked_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pass_sacked = (int)pass_sacked.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaSacked = (int)pass_sacked.Value;
            }
        }

        private void receiving_recs_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_recs = (int)receiving_recs.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRec = (int)receiving_recs.Value;
            }
        }

        private void receiving_yds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_yards = (int)receiving_yds.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRecYds = (int)receiving_yds.Value;
            }
        }

        private void receiving_tds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_tds = (int)receiving_tds.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRecTd = (int)receiving_tds.Value;
            }
        }

        private void receiving_drops_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_drops = (int)receiving_drops.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaDrops = (int)receiving_drops.Value;
            }
        }

        private void receiving_long_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_long = (int)receiving_long.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRecLong = (int)receiving_long.Value;
            }
        }

        private void receiving_yac_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Receiving_yac = (int)receiving_yac.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRecYac = (int)receiving_yac.Value;
            }
        }

        private void rushingattempts_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).RushingAttempts = (int)rushingattempts.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushAtt = (int)rushingattempts.Value;
            }
        }

        private void rushingyards_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).RushingYards = (int)rushingyards.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushYds = (int)rushingyards.Value;
            }
        }

        private void rushing_tds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Rushing_tds = (int)rushing_tds.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushTd = (int)rushing_tds.Value;
            }
        }

        private void fumbles_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fumbles = (int)fumbles.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaFumbles = (int)fumbles.Value;
            }
        }

        private void rushing_20_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Rushing_20 = (int)rushing_20.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRush20 = (int)rushing_20.Value;
            }
        }

        private void rushing_long_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Rushing_long = (int)rushing_long.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushLong = (int)rushing_long.Value;
            }
        }

        private void rushing_bt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Rushing_bt = (int)rushing_bt.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushBtk = (int)rushing_bt.Value;
            }
        }

        private void rushing_yac_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Rushing_yac = (int)rushing_yac.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaRushYac = (int)rushing_yac.Value;
            }
        }

        #endregion
        
        #region OLine Stats

        private void pancakes_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOLCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pancakes = (int)pancakes.Value;
                else model.PlayerModel.GetOLstats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Pancakes = (int)pancakes.Value;
            }
        }

        private void sacksallowed_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOLCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).SacksAllowed = (int)sacksallowed.Value;
                else model.PlayerModel.GetOLstats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SacksAllowed = (int)sacksallowed.Value;
            }
        }

        #endregion
                
        #region Defense Stats

        private void tackles_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Tackles = (int)tackles.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Tackles = (int)tackles.Value;
            }
        }

        private void tacklesforloss_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).TacklesForLoss = (int)tacklesforloss.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).TacklesForLoss = (int)tacklesforloss.Value;
            }
        }

        private void sacks_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Sacks = (int)sacks.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Sacks = (int)sacks.Value;
            }
        }

        private void fumblesforced_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).FumblesForced = (int)fumblesforced.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).FumblesForced = (int)fumblesforced.Value;
            }
        }

        private void fumblesrecovered_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).FumblesRecovered = (int)fumblesrecovered.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).FumblesRecovered = (int)fumblesrecovered.Value;
            }
        }

        private void fumbles_td_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fumbles_td = (int)fumbles_td.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).FumbleTDS = (int)fumbles_td.Value;
            }
        }

        private void fumbleyards_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).FumbleYards = (int)fumbleyards.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).FumbleYards = (int)fumbleyards.Value;
            }
        }

        private void blocks_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Blocks = (int)blocks.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Blocks = (int)blocks.Value;
            }
        }

        private void safeties_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Safeties = (int)safeties.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Safeties = (int)safeties.Value;
            }
        }

        private void passesdefended_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).PassesDefended = (int)passesdefended.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).PassesDefended = (int)passesdefended.Value;
            }
        }

        private void def_int_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Def_int = (int)def_int.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Interceptions = (int)def_int.Value;
            }
        }

        private void int_td_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Int_td = (int)int_td.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).InterceptionTDS = (int)int_td.Value;
            }
        }

        private void int_yards_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Int_yards = (int)int_yards.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).InterceptionYards = (int)int_yards.Value;
            }
        }

        private void int_long_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Int_long = (int)int_long.Value;
                else model.PlayerModel.GetDefenseStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).InterceptionLong = (int)int_long.Value;
            }
        }

        #endregion
                
        #region Games Played

        private void gamesstarted_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising && model.FileVersion != MaddenFileVersion.Ver2004)
            {
                if (statsyear.Text == "Career")                
                    model.PlayerModel.GetPlayersGamesCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).GamesStarted = (int)gamesstarted.Value;
                else model.PlayerModel.GetSeasonGames(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).GamesStarted = (int)gamesstarted.Value;
            }
        }
        
        private void gamesplayed_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                {
                    if (model.FileVersion == MaddenFileVersion.Ver2004)
                        model.PlayerModel.GetPlayersGamesCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).GamesPlayed04 = (int)gamesplayed.Value;
                    else model.PlayerModel.GetPlayersGamesCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).GamesPlayed = (int)gamesplayed.Value;
                }
                else
                {
                    model.PlayerModel.GetSeasonGames(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).GamesPlayed = (int)gamesplayed.Value;
                }
            }           
        }

        #endregion

        //fix
        #region Punt/Kick

        private void fga_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fga = (int)fga.Value;
                else model.PlayerModel.GetPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).Fga = (int)fga.Value;
            }
        }

        private void fgm_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgm = (int)fgm.Value;
            }
        }

        private void fgbl_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgbl = (int)fgbl.Value;
            }
        }

        private void fgl_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgl = (int)fgl.Value;
            }
        }

        private void xpa_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Xpa = (int)xpa.Value;
            }
        }

        private void xpm_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Xpm = (int)xpm.Value;
            }
        }

        private void xpb_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Xpb = (int)xpb.Value;
            }
        }

        private void fga_129_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fga_129 = (int)fga_129.Value;
            }
        }

        private void fga_3039_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fga_3039 = (int)fga_3039.Value;
            }
        }

        private void fga_4049_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fga_4049 = (int)fga_4049.Value;
            }
        }

        private void fga_50_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fga_50 = (int)fga_50.Value;
            }
        }

        private void fgm_129_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgm_129 = (int)fgm_129.Value;
            }
        }

        private void fgm_3039_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgm_3039 = (int)fgm_3039.Value;
            }
        }

        private void fgm_4049_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgm_4049 = (int)fgm_4049.Value;
            }
        }

        private void fgm_50_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Fgm_50 = (int)fgm_50.Value;
            }
        }

        private void kickoffs_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Kickoffs = (int)kickoffs.Value;
            }
        }

        private void touchbacks_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Touchbacks = (int)touchbacks.Value;
            }
        }

        private void puntatt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntatt = (int)puntatt.Value;
            }
        }

        private void puntyds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntyds = (int)puntyds.Value;
            }
        }

        private void puntlong_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntlong = (int)puntlong.Value;
            }
        }

        private void puntny_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntny = (int)puntny.Value;
            }
        }

        private void puntin20_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntin20 = (int)puntin20.Value;
            }
        }

        private void punttb_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Punttb = (int)punttb.Value;
            }
        }

        private void puntblk_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId).Puntblk = (int)puntblk.Value;
            }
        }

        #endregion

        #region Punt/Kick Returns

        private void kra_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Kra = (int)kra.Value;
            }
        }

        private void kryds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Kryds = (int)kryds.Value;
            }
        }

        private void krl_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Krl = (int)krl.Value;
            }
        }

        private void krtd_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Krtd = (int)krtd.Value;
            }
        }

        private void pra_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pra = (int)pra.Value;
            }
        }

        private void pryds_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Pryds = (int)pryds.Value;
            }
        }

        private void prl_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Prl = (int)prl.Value;
            }
        }

        private void prtd_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId).Prtd = (int)prtd.Value;
            }
        }

        #endregion

        #endregion

        
        
        private void statsyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitialising)
            {
                //  Get career stats on first load
                if (model.FileType == MaddenFileType.FranchiseFile)
                    LoadPlayerStats(model.PlayerModel.CurrentPlayerRecord, 0);
            }

            else
            {
                //  Get Stats for year or career as selected
                if (model.FileType == MaddenFileType.FranchiseFile)
                    LoadPlayerStats(model.PlayerModel.CurrentPlayerRecord, (int)statsyear.SelectedIndex);
            }
        }



        private void AddOStats_Click(object sender, EventArgs e)
        {
            // Set for Career or Season add
            //SeasonStatsOffenseRecord newseaoffrec = model.PlayerModel.CreateNewSeaOR();
        }

        private void enhancementPercentage_ValueChanged(object sender, EventArgs e)
        {            
            
        }

        public void FixCareerStats(PlayerRecord player)
        {            
            int baseyear = 2003;
            if (model.FileVersion == MaddenFileVersion.Ver2005)
                baseyear = 2004;
            if (model.FileVersion == MaddenFileVersion.Ver2006)
                baseyear = 2005;
            if (model.FileVersion == MaddenFileVersion.Ver2007)
                baseyear = 2006;
            if (model.FileVersion == MaddenFileVersion.Ver2008)
                baseyear = 2007;

            //  offense
            int totalpassatt = 0;
            int totalpasscomp = 0;


            for (int count = 0; count < player.YearsPro; count++)
            {
                if ((string)statsyear.Items[count] == "Career")
                    continue;
                else
                {
                    int year = (int)statsyear.Items[0] - baseyear;

                    SeasonStatsOffenseRecord off = model.PlayerModel.GetOffStats(player.PlayerId, year);
                    if (off == null)
                        continue;
                    totalpassatt += off.SeaPassAtt;
                    totalpasscomp += off.SeaComp;
                }
             
                

            }
            
        }





        private void PlayerRolecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (PlayerRolecomboBox.SelectedIndex == 43)
                    model.PlayerModel.CurrentPlayerRecord.PlayerRole = 45;
                else 
                    model.PlayerModel.CurrentPlayerRecord.PlayerRole = PlayerRolecomboBox.SelectedIndex;
            }
        }

        private void PlayerWeaponcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (PlayerWeaponcomboBox.SelectedIndex == 43)
                    model.PlayerModel.CurrentPlayerRecord.PlayerWeapon = 45;
                else
                    model.PlayerModel.CurrentPlayerRecord.PlayerWeapon = PlayerWeaponcomboBox.SelectedIndex;
            }
        }

        
    }
}

        

        

        

        

        







    

