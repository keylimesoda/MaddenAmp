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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Db;
using MaddenEditor.Core.DatEditor;
using MaddenEditor.Core.Manager;


namespace MaddenEditor.Forms
{
    //  TO DO:  FIX previous bonuses owed, taken from cap room
    
    
    public partial class PlayerEditControl : UserControl, IEditorForm
    {
        private EditorModel model = null;
        private PlayerRecord lastLoadedRecord = null;
        private bool isInitialising = false;
        public Overall playeroverall = new Overall();
        public List<string> CurrentPlayers = new List<string>();
        public int currentplayerrow = 0;        
        int year = 0;
        int selectedyear = -1;
        int baseyear = 2007;
        Dictionary<int, int> teamsalaries = new Dictionary<int, int>();
        
        private MGMT _manager;
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        


        public PlayerEditControl()
        {
            isInitialising = true;

            InitializeComponent();

            isInitialising = false;
        }

        #region IEditorForm Members

        public void InitialiseUI()
        {
            isInitialising = true;
            SuspendLayout();

            calculateOverallButton.Enabled = true;
            SevereLabel.Visible = false;
            ReturnLabel.Visible = false;
            playerInjuryReturn.Visible = false;
            playerInjurySevere.Visible = false;
            Traits_Panel.Visible = false;
            Ratings19_Panel.Visible = false;
            playerOVRArchetypeCombo.Items.Clear();
            playerOVRArchetypeCombo.Enabled = false;
            playerOVRArchetype.Text = "NA";
            playerOVRArchetype.Enabled = false;

            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                calculateOverallButton.Enabled = false;
                SevereLabel.Visible = true;
                ReturnLabel.Visible = true;
                playerInjuryReturn.Visible = true;
                playerInjurySevere.Visible = true;
                Traits_Panel.Visible = true;
                Ratings19_Panel.Visible = true;

                playeroverall.InitRatings19();
            }
            

            #region Tattoos
            if (model.FileVersion == MaddenFileVersion.Ver2004)
            {
                Tattoo_Left.Maximum = 31;
                Tattoo_Right.Maximum = 31;
            }
            else if (model.FileVersion == MaddenFileVersion.Ver2005)
            {
                Tattoo_Left.Maximum = 15;
                Tattoo_Right.Maximum = 15;
            }
            else
            {
                Tattoo_Left.Maximum = 63;
                Tattoo_Right.Maximum = 63;
            }
            #endregion

            string feet = " '";
            string inches = " \"";
            for (int c = 0; c < 12; c++)
            {
                if (c == 0)
                {
                    PlayerHeight_Feet.Items.Add("");
                    PlayerHeight_Inches.Items.Add("");
                }
                if (c !=0 && c < 11)
                    PlayerHeight_Feet.Items.Add(c.ToString() + feet);
                if (c != 0)
                    PlayerHeight_Inches.Items.Add(c.ToString() + inches);
            }

            filterTeamComboBox.Items.Add("ALL");
            foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                // Only add teams and 'Free Agents' to the combo box, not probowl teams (AFC,NFC)
                if (record.GetIntField("TGID") <= 1009)
                {
                    Team_Combo.Items.Add(record);
                    filterTeamComboBox.Items.Add(record);
                }
            }
            // Add Retired to the team lists
            if (model.FileType == MaddenFileType.Franchise)
            {
                Team_Combo.Items.Add(EditorModel.RETIRED);
                filterTeamComboBox.Items.Add(EditorModel.RETIRED);
            }

            InitCollegeList();

            if (model.FileType == MaddenFileType.Franchise)
                model.SalaryCapModel.InitCap();

            filterPositionComboBox.Items.Add("ALL");

            for (int p = 0; p < 21; p++)
            {
                string pos = Enum.GetName(typeof(MaddenPositions), p);
                positionComboBox.Items.Add(pos);
                filterPositionComboBox.Items.Add(pos);
                OriginalPosition_Combo.Items.Add(pos);
            }
            //foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))       
            //{               
            //    positionComboBox.Items.Add(pos);
            //    filterPositionComboBox.Items.Add(pos);
            //    OriginalPosition_Combo.Items.Add(pos);
            //}            

            filterTeamComboBox.SelectedIndex = 0;
            filterPositionComboBox.SelectedIndex = 0;

            

            if (model.FileType != MaddenFileType.Franchise)
            {
                TeamCapRoom.Visible = false;
                capRoomLabel.Visible = false;
            }
            else
            {
                TeamCapRoom.Visible = true;
                capRoomLabel.Visible = true;
            }

            #region Equipment

            #region Helmet
            foreach (GenericRecord rec in model.PlayerModel.HelmetStyleList)
            {
                playerHelmetStyleCombo.Items.Add(rec);
            }
            playerHelmetStyleCombo.Sorted = true;
            #endregion

            #region Shoes
            #region 04-08
            if (model.FileVersion < MaddenFileVersion.Ver2019)
            {
                //0 WT Shoes
                //1 WT Shoes WT Tape
                //2 WT Shoes BK Tape
                //3 WT Shoes TC Tape
                //4 TC Shoes
                //5 TC Shoes WT Tape
                //6 TC Shoes BK Tape
                //7 TC Shoes TC Tape
                //8 BK Shoes
                //9 BK Shoes WT Tape
                //10 BK Shoes Bk Tape
                //11 BK Shoes TC Tape
                playerLeftAnkleCombo.Items.Clear();
                playerRightAnkleCombo.Items.Clear();
                playerLeftAnkleCombo.Items.Add("WT Shoes");
                playerRightAnkleCombo.Items.Add("WT Shoes");
                playerLeftAnkleCombo.Items.Add("WT Shoes WT Tape");
                playerRightAnkleCombo.Items.Add("WT Shoes WT Tape");
                playerLeftAnkleCombo.Items.Add("WT Shoes BK Tape");
                playerRightAnkleCombo.Items.Add("WT Shoes BK Tape");
                playerLeftAnkleCombo.Items.Add("WT Shoes TC Tape");
                playerRightAnkleCombo.Items.Add("WT Shoes TC Tape");

                playerLeftAnkleCombo.Items.Add("TC shoes");
                playerRightAnkleCombo.Items.Add("TC shoes");
                playerLeftAnkleCombo.Items.Add("TC shoes WT Tape");
                playerRightAnkleCombo.Items.Add("TC shoes WT Tape");
                playerLeftAnkleCombo.Items.Add("TC shoes BK Tape");
                playerRightAnkleCombo.Items.Add("TC shoes BK Tape");
                playerLeftAnkleCombo.Items.Add("TC shoes TC Tape");
                playerRightAnkleCombo.Items.Add("TC shoes TC Tape");

                playerLeftAnkleCombo.Items.Add("BK shoes");
                playerRightAnkleCombo.Items.Add("BK shoes");
                playerLeftAnkleCombo.Items.Add("BK shoes WT Tape");
                playerRightAnkleCombo.Items.Add("BK shoes WT Tape");
                playerLeftAnkleCombo.Items.Add("BK shoes BK Tape");
                playerRightAnkleCombo.Items.Add("BK shoes BK Tape");
                playerLeftAnkleCombo.Items.Add("BK shoes TC Tape");
                playerRightAnkleCombo.Items.Add("BK shoes TC Tape");

                playerLeftAnkleCombo.Items.Add("Custom");
                playerRightAnkleCombo.Items.Add("Custom");
            }
            #endregion
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                playerLeftShoeCombo.Items.Clear();
                foreach (GenericRecord rec in model.PlayerModel.ShoeList)
                {
                    playerLeftShoeCombo.Items.Add(rec);
                    playerRightShoeCombo.Items.Add(rec);
                }
                playerLeftShoeCombo.Sorted = true;
                playerLeftShoeCombo.Enabled = true;
                playerRightShoeCombo.Sorted = true;
                playerRightShoeCombo.Enabled = true;
            }
            else
            {
                playerLeftShoeCombo.Enabled = false;
                playerRightShoeCombo.Enabled = false;
            }
            #endregion

            #region FaceMask
            foreach (GenericRecord rec in model.PlayerModel.FacemakeList)
            {
                playerFaceMaskCombo.Items.Add(rec);
            }
            playerFaceMaskCombo.Sorted = true;
            #endregion

            #region Left/Right Hand
            foreach (GenericRecord rec in model.PlayerModel.GloveList)
            {
                playerLeftHandCombo.Items.Add(rec);
                playerRightHandCombo.Items.Add(rec);
            }
            playerLeftHandCombo.Sorted = true;
            playerRightHandCombo.Sorted = true;

            #endregion

            #region Left/Right Wrist
            foreach (GenericRecord rec in model.PlayerModel.WristBandList)
            {
                playerLeftWristCombo.Items.Add(rec);
                playerRightWristCombo.Items.Add(rec);
            }
            playerLeftWristCombo.Sorted = true;
            playerRightWristCombo.Sorted = true;

            #endregion

            #region Left/Right Sleeve
            foreach (GenericRecord rec in model.PlayerModel.SleeveList)
            {
                playerLeftSleeve.Items.Add(rec);
                playerRightSleeve.Items.Add(rec);
            }
            playerLeftSleeve.Sorted = true;
            playerRightSleeve.Sorted = true;
            #endregion

            #region Left/Right Elbow
            foreach (GenericRecord rec in model.PlayerModel.ElbowList)
            {
                playerLeftElbowCombo.Items.Add(rec);
                playerRightElbowCombo.Items.Add(rec);
            }
            playerLeftElbowCombo.Sorted = true;
            playerRightAnkleCombo.Sorted = true;
            #endregion

            #region Left/Right Ankle
            foreach (GenericRecord rec in model.PlayerModel.AnkleList)
            {
                playerLeftAnkleCombo.Items.Add(rec);
                playerRightAnkleCombo.Items.Add(rec);
            }
            playerLeftAnkleCombo.Sorted = true;
            playerRightAnkleCombo.Sorted = true;
            #endregion

            #region EyePaint / Face Marks
            
                foreach (GenericRecord rec in model.PlayerModel.FaceMarkList)
                {
                    playerEyePaintCombo.Items.Add(rec);
                }
            
            playerEyePaintCombo.Sorted = true;
            #endregion

            #region Visor
            foreach (GenericRecord rec in model.PlayerModel.VisorList)
            {
                playerVisorCombo.Items.Add(rec);
            }

            #endregion
                        
            #region NeckRoll
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                playerNeckRollCombo.Items.Clear();
                playerNeckRollCombo.Items.Add("None");
                playerNeckRollCombo.Items.Add("Vintage");
            }
            #endregion

            #region Knee
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                playerLeftKneeCombo.Items.Clear();
                playerRightKneeCombo.Items.Clear();
                playerLeftKneeCombo.Items.Add("Nike");
                playerRightKneeCombo.Items.Add("Nike");
                playerLeftKneeCombo.Items.Add("Regular");
                playerRightKneeCombo.Items.Add("Regular");
            }
            #endregion

            if (model.FileVersion >= MaddenFileVersion.Ver2019)
            {
                LegacyPanel.Visible = false;
                NextGenPanel.Visible = true;
            }
            else
            {
                LegacyPanel.Visible = true;
                NextGenPanel.Visible = false;
            }
            
            #endregion

            #region Injuries
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                if (model.PlayerModel.InjuryList != null)
                {
                    playerInjuryCombo.Items.Clear();
                    foreach (GenericRecord rec in model.PlayerModel.InjuryList)
                    {
                        playerInjuryCombo.Items.Add(rec);
                    }
                }
            }

            #endregion

            #region EndPlay
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                if (model.PlayerModel.EndPlayList != null)
                {
                    playerEndPlay.Items.Clear();
                    foreach (GenericRecord rec in model.PlayerModel.EndPlayList)
                    {
                        playerEndPlay.Items.Add(rec);
                    }
                }
            }

            #endregion

            #region QB Style
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                if (model.PlayerModel.QBStyleList != null)
                {
                    playerQBStyle.Items.Clear();
                    foreach (GenericRecord rec in model.PlayerModel.QBStyleList)
                    {
                        playerQBStyle.Items.Add(rec);
                    }
                }
            }

            #endregion


            #region Roles/Weapons Stats - Comebacks/First Downs

            RoleLabel.Visible = false;
            WeaponLabel.Visible = false;
            PlayerRolecomboBox.Visible = false;
            PlayerWeaponcomboBox.Visible = false;
            label108.Visible = false;
            label109.Visible = false;
            comebacks.Visible = false;
            Firstdowns.Visible = false;
            
            if (model.FileVersion >= MaddenFileVersion.Ver2007 && model.FileVersion < MaddenFileVersion.Ver2019)
            {
                RoleLabel.Visible = true;
                PlayerRolecomboBox.Visible = true;
                label108.Visible = true;
                label109.Visible = true;
                comebacks.Visible = true;
                Firstdowns.Visible = true;

                model.InitRoles(manager.stream_model);
                List<int> roles = model.PlayerRole.Keys.ToList();
                roles.Sort();
                foreach (int r in roles)
                {
                    PlayerRolecomboBox.Items.Add(model.PlayerRole[r]);
                    PlayerWeaponcomboBox.Items.Add(model.PlayerRole[r]);
                }
                
                if (model.FileVersion == MaddenFileVersion.Ver2008)
                {                     
                    WeaponLabel.Visible = true;
                    PlayerWeaponcomboBox.Visible = true;
                }
            }
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                RoleLabel.Visible = true;
                RoleLabel.Text = "XP Rate";
                RoleLabel.Location = new Point(180, 561);
                PlayerRolecomboBox.Items.Add("Normal");
                PlayerRolecomboBox.Items.Add("Quick");
                PlayerRolecomboBox.Items.Add("Star");
                PlayerRolecomboBox.Items.Add("Superstar");
                PlayerRolecomboBox.Visible = true;
            }
            
            #endregion

            EditorModel.totalplayers = 0;
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (rec.PlayerId > EditorModel.totalplayers)
                    EditorModel.totalplayers = rec.PlayerId;
            }

            if (manager.PlayerPortDAT.isterf)
            {
                ImportPlayerPort_Button.Visible = true;
                PlayerPortraitExport_Button.Visible = true;
            }
            else
            {
                ImportPlayerPort_Button.Visible = false;
                PlayerPortraitExport_Button.Visible = false;
            }

            SalaryRankCombo.Items.Add("NFL");
            SalaryRankCombo.Items.Add("Conf");
            SalaryRankCombo.Items.Add("Div");
            SalaryRankCombo.SelectedIndex = 0;
                        
            UseActualNFLSalaryCap_Checkbox.Enabled = false;
            UseActualNFLSalaryCap_Checkbox.Checked = false;
            CalcTeamSalary_Checkbox.Enabled = false;
            CalcTeamSalary_Checkbox.Checked = false;
            
            if (model.FileType == MaddenFileType.Franchise)
            {
                MiscSalary_Panel.Enabled = true;
                UseActualNFLSalaryCap_Checkbox.Enabled = true;
                CalcTeamSalary_Checkbox.Enabled = true;
                CalcTeamSalary_Checkbox.Checked = true;
            }            

            InitPlayerList();

            isInitialising = false;
            ResumeLayout();
        }
        
        public void CleanUI()
        {
            CollegeCombo.Items.Clear();
            Team_Combo.Items.Clear();
            filterPositionComboBox.Items.Clear();
            OriginalPosition_Combo.Items.Clear();
            positionComboBox.Items.Clear();
            filterTeamComboBox.Items.Clear();
            PlayerRolecomboBox.Items.Clear();
        }

        public EditorModel Model
        {
            set
            {
                model = value;
                if (model.FileVersion == MaddenFileVersion.Ver2004)
                    baseyear = 2003;
                else if (model.FileVersion == MaddenFileVersion.Ver2005)
                    baseyear = 2004;
                else if (model.FileVersion == MaddenFileVersion.Ver2006)
                    baseyear = 2005;
                else if (model.FileVersion == MaddenFileVersion.Ver2007)
                    baseyear = 2006;
                else if (model.FileVersion == MaddenFileVersion.Ver2019)
                    baseyear = 2018;
                else if (model.FileVersion == MaddenFileVersion.Ver2008)
                    baseyear = 2007;
            }
        }

        #endregion       

        #region Colleges
        public void InitCollegeList()
        {
            CollegeCombo.Items.Clear();
            foreach (KeyValuePair<int, college_entry> col in model.Colleges)
            {
                string name = col.Value.name;
                CollegeCombo.Items.Add(name);
            }
            CollegeCombo.Sorted = true;
            CollegeCombo.Update();        
        }
        #endregion

        public void InitPlayerList()
        {
            isInitialising = true;

            CurrentPlayers.Clear();
            CurrentPlayers = model.PlayerModel.GetPlayerList();
            
            PlayerGridView.Rows.Clear();
            PlayerGridView.Refresh();
            PlayerGridView.MultiSelect = false;
            PlayerGridView.RowHeadersVisible = false;
            PlayerGridView.AutoGenerateColumns = false;
            PlayerGridView.AllowUserToAddRows = false;
            PlayerGridView.ColumnCount = 2;
            PlayerGridView.Columns[0].Name = "ID";
            PlayerGridView.Columns[0].Width = 35;
            PlayerGridView.Columns[1].Name = "Player";
            PlayerGridView.Columns[1].Width = 100;
            foreach (KeyValuePair<int,string> player in model.PlayerModel.playernames)
            {                
                object[] o = { (int)player.Key, (string)player.Value };
                PlayerGridView.Rows.Add(o);
            }
            if (model.PlayerModel.playernames.Count > 0)
            {
                PlayerGridView.Rows[0].Selected = true;
                int idnum = (int)PlayerGridView.Rows[0].Cells[0].Value;
                LoadPlayerInfo(model.PlayerModel.GetPlayerByPlayerId(idnum));
            }
            else LoadPlayerInfo(null); 
                //MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            isInitialising = false;
        }
        
        public void LoadPlayerInfo(PlayerRecord record)
        {
            model.PlayerModel.CurrentPlayerRecord = record;
            
            bool holder = isInitialising;

            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                filterTeamComboBox.SelectedIndex = 0;
                filterPositionComboBox.SelectedIndex = 0;
                model.PlayerModel.filterteam = -1;
                model.PlayerModel.filterposition = -1;
                InitPlayerList();
                return;
            }
            
            SuspendLayout();
            isInitialising = true;
            deletePlayerButton.Enabled = true;

            try
            {
                firstNameTextBox.Text = record.FirstName;
                lastNameTextBox.Text = record.LastName;
                NFL_Updown.Value = record.NFLID;
                PlayerID_Updown.Value = record.PlayerId;
                playerComment.Value = record.PlayerComment;

                TeamRecord team = model.TeamModel.GetTeamRecord(record.TeamId);

                if (model.FileType != MaddenFileType.Roster)
                {
                    if (record.TeamId == 1014)
                        Team_Combo.Text = "Retired";
                    else Team_Combo.SelectedItem = (object)team;
                }
                else Team_Combo.SelectedItem = (object)team;

                positionComboBox.Text = positionComboBox.Items[record.PositionId].ToString();
                OriginalPosition_Combo.SelectedIndex = record.OriginalPositionId;                

                if (record.CollegeId > CollegeCombo.Items.Count -1)
                    CollegeCombo.SelectedIndex = -1;
                else CollegeCombo.SelectedItem = model.Colleges[record.CollegeId].name;                               
                
                playerThrowingStyle.SelectedIndex = Convert.ToInt32(record.SideArmed);
                CareerPhase_Combo.SelectedIndex = record.CareerPhase;

                SetNumericUpDown(playerAge, record.Age, "Age");                

                if (record.JerseyNumber > 99)
                {
                    //Must be a draft class, disable jersey number editing
                    playerJerseyNumber.Enabled = false;
                }
                else
                {
                    playerJerseyNumber.Enabled = true;
                    playerJerseyNumber.Value = record.JerseyNumber;
                }

                SetNumericUpDown(playerYearsPro, record.YearsPro, "Years Pro");
                SetNumericUpDown(playerWeight, record.Weight + 160, "Weight");

                if (record.Height < 12)
                    PlayerHeight_Feet.SelectedIndex = -1;
                else PlayerHeight_Feet.SelectedIndex = record.Height / 12;
                PlayerHeight_Inches.SelectedIndex = record.Height - (PlayerHeight_Feet.SelectedIndex * 12);

                playerDominantHand.Checked = record.DominantHand;

                SetNumericUpDown(Overall, record.Overall, "Overall");
                SetNumericUpDown(playerSpeed, record.Speed, "Speed");
                SetNumericUpDown(playerStrength, record.Strength, "Strength");
                SetNumericUpDown(playerAwareness, record.Awareness, "Awareness");
                SetNumericUpDown(playerAgility, record.Agility, "Agility");
                SetNumericUpDown(playerAcceleration, record.Acceleration, "Acceleration");
                SetNumericUpDown(playerCatching, record.Catching, "Catching");
                SetNumericUpDown(playerCarrying, record.Carrying, "Carrying");
                SetNumericUpDown(playerJumping, record.Jumping, "Jumping");
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    SetNumericUpDown(playerBreakTackle, record.BreakTackle19, "Break Tackle");
                else
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
                SetNumericUpDown(playerImportance, record.Importance, "Importance");               

                playerMorale.Visible = false;
                label34.Visible = false;
                cbTendancy.Enabled = false;

                if (model.FileVersion >= MaddenFileVersion.Ver2005 && model.FileVersion < MaddenFileVersion.Ver2019)
                {
                    //Load the player tendancy and reinitialise the combo
                    cbTendancy.Enabled = true;
                    cbTendancy.Items.Clear();
                    for (int i = 0; i < 3; i++)
                    {
                        cbTendancy.Items.Add(DecodeTendancy((MaddenPositions)record.PositionId, i));
                    }

                    cbTendancy.SelectedIndex = record.Tendency;
                    
                    playerMorale.Visible = true;
                    label34.Visible = true;
                    SetNumericUpDown(playerMorale, record.Morale, "Morale");
                    playerNFLIcon.Visible = true;
                    playerNFLIcon.Checked = record.NFLIcon;
                }
                else
                {
                    playerNFLIcon.Visible = false;
                    playerMorale.Visible = false;
                }

                if (model.FileVersion >= MaddenFileVersion.Ver2007 && model.FileVersion < MaddenFileVersion.Ver2019)
                {
                    lblValue.Visible = true;
                    playerValue.Visible = true;
                    SetNumericUpDown(playerEgo, record.Ego, "Player Ego");
                    SetNumericUpDown(playerValue, record.PlayerValue, "Player Value");
                    RoleLabel.Visible = true;
                    PlayerRolecomboBox.Visible = true;
                    PlayerRolecomboBox.Text = model.PlayerRole[record.PlayerRole];
                }
                else
                {
                    SetNumericUpDown(playerEgo, record.Pcel, "Player Ego");
                    lblValue.Visible = false;
                    playerValue.Visible = false;
                   
                }

                if (model.FileVersion == MaddenFileVersion.Ver2008)
                {
                    WeaponLabel.Visible = true;
                    PlayerWeaponcomboBox.Visible = true;
                    PlayerWeaponcomboBox.Text = model.PlayerRole[record.PlayerWeapon];
                }                

                playerProBowl.Checked = record.ProBowl;
                playerPortraitId.Value = record.PortraitId;                
                
                // This was done to fix errors people were introducing into rosters using
                // NZA and dbeditor.
                if (record.ContractLength == 0)
                {
                    record.ClearContract();
                }
                else if (record.ContractYearsLeft > record.ContractLength)
                    record.ContractYearsLeft = record.ContractLength;
                
                playerContractLength.Value = record.ContractLength;
                playerContractYearsLeft.Value = record.ContractYearsLeft;                              
                
                if (record.Bonus0 !=0)
                //(model.PlayerModel.CurrentPlayerRecord.Bonus0 != 0)
                {
                    playerSigningBonus.Value = (decimal)(record.SigningBonus / 100.0);
                }
                else
                {
                    playerSigningBonus.Value = 0;
                }
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

                
                //Set player Appearance
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    PlayerFaceId.Value = (int)record.FaceID_19;
                else PlayerFaceId.Value = (int)record.FaceId;

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
                Tattoo_Left.Value = record.LeftTattoo;
                Tattoo_Right.Value = record.RightTattoo;

                //Load Player equipment
                playerHairStyleCombo.Text = playerHairStyleCombo.Items[record.HairStyle].ToString();
                //playerSkinColorCombo.Text = playerSkinColorCombo.Items[record.SkinType].ToString();
                playerHairColorCombo.Text = playerHairColorCombo.Items[record.HairColor].ToString();
                playerNeckRollCombo.Text = playerNeckRollCombo.Items[record.NeckRoll].ToString();
                playerVisorCombo.Text = playerVisorCombo.Items[record.Visor].ToString();
                playerMouthPieceCombo.Text = playerMouthPieceCombo.Items[record.MouthPiece].ToString();                
                playerLeftKneeCombo.Text = playerLeftKneeCombo.Items[Convert.ToInt32(record.LeftKnee)].ToString();
                playerRightKneeCombo.Text = playerRightKneeCombo.Items[Convert.ToInt32(record.RightKnee)].ToString();
                                
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                {
                    playerHelmetStyleCombo.Text = model.PlayerModel.GetHelmet(record.HelmetStyle);                    
                    playerFaceMaskCombo.Text = model.PlayerModel.GetFaceMask(record.FaceMask);
                    playerLeftShoeCombo.Text = model.PlayerModel.GetShoe(record.LeftShoe);
                    playerRightShoeCombo.Text = model.PlayerModel.GetShoe(record.RightShoe);
                    playerLeftHandCombo.Text = model.PlayerModel.GetGloves(record.LeftHand);
                    playerRightHandCombo.Text = model.PlayerModel.GetGloves(record.RightHand);
                    playerLeftWristCombo.Text = model.PlayerModel.GetWrist(record.LeftWrist);
                    playerRightWristCombo.Text = model.PlayerModel.GetWrist(record.RightWrist);
                    playerLeftSleeve.Text = model.PlayerModel.GetSleeve(record.SleevesLeft);
                    playerRightSleeve.Text = model.PlayerModel.GetSleeve(record.SleevesRight);
                    playerLeftElbowCombo.Text = model.PlayerModel.GetElbow(record.LeftElbow);
                    playerRightElbowCombo.Text = model.PlayerModel.GetElbow(record.RightElbow);
                    EyePaintLabel.Text = "Face Marks";
                    EyePaintLabel.Location = new Point(327, 511);
                    playerEyePaintCombo.Text = model.PlayerModel.GetFaceMark(record.EyePaint);                    
                    playerLeftAnkleCombo.Text = model.PlayerModel.GetAnkle(record.AnkleLeft);
                    playerRightAnkleCombo.Text = model.PlayerModel.GetAnkle(record.AnkleRight);
                    playerEndPlay.Text = model.PlayerModel.GetEndPlay(record.EndPlay);
                    playerQBStyle.Text = model.PlayerModel.GetQBStyle(record.QBStyle);
                    playerJerseySleeves.SelectedIndex = model.PlayerModel.CurrentPlayerRecord.Jersey;

                    #region Archetypes
                    playerArchetype.Items.Clear();
                    int start = 0;
                    int end = 0;
                    switch (record.PositionId)
                    {
                        case (int)MaddenPositions.QB:
                            end = 3;
                            break;
                        case (int)MaddenPositions.HB:
                            {
                                start = 4;
                                end = 6;
                            }
                            break;
                        case (int)MaddenPositions.FB:
                            {
                                start = 7;
                                end = 8;
                            }
                            break;
                        case (int)MaddenPositions.WR:
                            {
                                start = 9;
                                end = 12;
                            }
                            break;
                        case (int)MaddenPositions.TE:
                            {
                                start = 13;
                                end = 15;
                            }
                            break;
                        case (int)MaddenPositions.C:
                            {
                                start = 16;
                                end = 18;
                            }
                            break;
                        case (int)MaddenPositions.LT:
                        case (int)MaddenPositions.RT:
                            {
                                start = 19;
                                end = 21;
                            }
                            break;
                        case (int)MaddenPositions.LG:
                        case (int)MaddenPositions.RG:
                            {
                                start = 22;
                                end = 24;
                            }
                            break;
                        case (int)MaddenPositions.LE:
                        case (int)MaddenPositions.RE:
                            {
                                start = 25;
                                end = 27;
                            }
                            break;
                        case (int)MaddenPositions.DT:
                            {
                                start = 28;
                                end = 30;
                            }
                            break;
                        case (int)MaddenPositions.LOLB:
                        case (int)MaddenPositions.ROLB:
                            {
                                start = 31;
                                end = 34;
                            }
                            break;
                        case (int)MaddenPositions.MLB:
                            {
                                start = 35;
                                end = 37;
                            }
                            break;
                        case (int)MaddenPositions.CB:
                            {
                                start = 38;
                                end = 40;
                            }
                            break;
                        case (int)MaddenPositions.FS:
                        case (int)MaddenPositions.SS:
                            {
                                start = 41;
                                end = 43;
                            }
                            break;
                        case (int)MaddenPositions.K:
                        case (int)MaddenPositions.P:
                            {
                                start = 44;
                                end = 45;
                            }
                            break;
                    }

                    playerOVRArchetypeCombo.Items.Clear();
                    playerOVRArchetypeCombo.Enabled = false;
                    playerOVRArchetype.Text = "";
                    for (int c = start; c <= end; c++)
                    {
                        playerArchetype.Items.Add(model.PlayerModel.ArchetypeList[c]);
                        if (record.PositionId <= 32)
                            playerOVRArchetypeCombo.Items.Add(model.PlayerModel.ArchetypeList[c]);
                    }
                    
                    playerArchetype.Text = model.PlayerModel.GetArchetype(record.PlayerType);                   

                    #endregion

                    PlayerRolecomboBox.SelectedIndex = record.PlayerRole;

                    #region Archetype Overalls Madden 19
                    if (record.PositionId <= 32)
                    {
                        playerOVRArchetype.Text = playeroverall.GetOverall19(record, record.PositionId, record.PlayerType).ToString();
                        playerOVRArchetypeCombo.Text = model.PlayerModel.GetArchetype(record.PlayerType);
                        playerOVRArchetypeCombo.Enabled = true;
                    }

                    #endregion
                }
                else
                {
                    playerHelmetStyleCombo.Text = playerHelmetStyleCombo.Items[record.HelmetStyle].ToString();
                    playerFaceMaskCombo.Text = playerFaceMaskCombo.Items[record.FaceMask].ToString();
                    playerLeftHandCombo.Text = playerLeftHandCombo.Items[record.LeftHand].ToString();
                    playerRightHandCombo.Text = playerRightHandCombo.Items[record.RightHand].ToString();
                    playerLeftWristCombo.Text = playerLeftWristCombo.Items[record.LeftWrist].ToString();
                    playerRightWristCombo.Text = playerRightWristCombo.Items[record.RightWrist].ToString();
                    playerSleevesCombo.Text = playerSleevesCombo.Items[record.SleevesLeft].ToString();
                    playerLeftElbowCombo.Text = playerLeftElbowCombo.Items[record.LeftElbow].ToString();
                    playerRightElbowCombo.Text = playerRightElbowCombo.Items[record.RightElbow].ToString();
                    playerLeftAnkleCombo.Text = playerLeftAnkleCombo.Items[record.LeftShoe].ToString();
                    playerRightAnkleCombo.Text = playerRightAnkleCombo.Items[record.RightShoe].ToString();
                    playerEyePaintCombo.Text = playerEyePaintCombo.Items[record.EyePaint].ToString();
                    playerNasalStripCombo.Text = playerNasalStripCombo.Items[record.NasalStrip].ToString();
                    playerEndPlay.Text = "";
                    playerEndPlay.Enabled = false;
                    playerQBStyle.Text = "";
                    playerQBStyle.Enabled = false;
                    // not sure sleeves is legacy
                    playerJerseySleeves.SelectedIndex = model.PlayerModel.CurrentPlayerRecord.Jersey;
                    playerArchetype.SelectedIndex = -1;
                    playerArchetype.Enabled = false;
                }
                


                #region Injury
                InjuryRecord injury = model.PlayerModel.GetPlayersInjuryRecord(record.PlayerId);
                playerInjurySevere.Value = 0;
                playerInjurySevere.Enabled = false;
                playerInjuryReturn.Value = 0;
                playerInjuryReturn.Enabled = false;

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
                    playerInjuryLength.Value = injury.InjuryLength;
                    playerInjuryReserve.Checked = injury.IR;
                    injuryLengthDescriptionTextBox.Text = injury.LengthDescription;

                    if (model.FileVersion < MaddenFileVersion.Ver2019)
                    {
                        playerInjuryCombo.Text = playerInjuryCombo.Items[injury.InjuryType].ToString();
                    }
                    else
                    {
                        playerInjuryCombo.Text = model.PlayerModel.GetInjury(injury.InjuryType);
                        playerInjurySevere.Enabled = true;
                        playerInjurySevere.Value = injury.InjurySeverity;
                        playerInjuryReturn.Enabled = true;
                        playerInjuryReturn.Value = injury.InjuryReturn;;
                    }
                }
                #endregion

                PLGL_Updown.Value = 0;
                PPGA_Updown.Value = 0;
                PPSP_Updown.Value = 0;

                if (model.FileType == MaddenFileType.Franchise)
                {
                    InitStatsYear(record);
                    LoadPlayerStats(record);
                    PLGL_Updown.Value = record.Plpl;
                    PPGA_Updown.Value = record.PlayedGames;
                    PPSP_Updown.Value = record.Ppsp;

                    if (model.FileVersion > MaddenFileVersion.Ver2004)
                    {
                        foreach (InactiveRecord ia in model.TableModels[EditorModel.INACTIVE_TABLE].GetRecords())
                        {
                            InactiveRecord iar = (InactiveRecord)ia;
                            if (iar.PlayerID == record.PlayerId)
                                InactiveCheckbox.Checked = true;
                            else InactiveCheckbox.Checked = false;
                        }

                        InactiveCheckbox.Visible = true;
                    }

                    else InactiveCheckbox.Visible = false;
                }

                if (model.FileVersion == MaddenFileVersion.Ver2019)
                {  
                    playerImpactBlock.Value = (int)record.ImpactBlocking;
                    playerLeadBlock.Value = (int)record.LeadBlock;
                    playerRunBlockFinesse.Value = (int)record.RunBlockFootwork;
                    playerZoneCoverage.Value = (int)record.ZoneCoverage;
                    playerManCover.Value = (int)record.ManCoverage;
                    playerPlayRecog.Value = (int)record.PlayRecognition;
                    playerPursuit.Value = (int)record.Pursuit;
                    playerBlockShed.Value = (int)record.BlockShedding;
                    playerTrucking.Value = (int)record.Trucking;
                    playerElusive.Value = (int)record.Elusive;
                    playerRB_Vision.Value = (int)record.RB_Vision;
                    playerStiffArm.Value = (int)record.StiffArm;
                    playerSpinMove.Value = (int)record.SpinMove;
                    playerJukeMove.Value = (int)record.JukeMove;
                    playerShortRoute.Value = (int)record.ShortRoute;
                    playerMediumRoute.Value = (int)record.MediumRoute;
                    playerDeepRoute.Value = (int)record.DeepRoute;
                    playerCatchTraffic.Value = (int)record.CatchTraffic;
                    playerSpecCatch.Value = (int)record.SpecCatch;
                    playerRelease.Value = (int)record.Release;
                    playerThrowShort.Value = (int)record.ThrowShort;
                    playerThrowMedium.Value = (int)record.ThrowMedium;
                    playerThrowDeep.Value = (int)record.ThrowDeep;
                    playerThrowOnRun.Value = (int)record.ThrowOnRun;
                    playerThrowPressure.Value = (int)record.ThrowPressure;
                    playerBreakSack.Value = (int)record.BreakSack;
                    playerPlayAction.Value = (int)record.PlayAction;
                    playerHitPower.Value = (int)record.HitPower;
                    playerPowerMoves.Value = (int)record.PowerMoves;
                    playerFinesseMoves.Value = (int)record.FinesseMoves;
                    playerHometown.Text = record.Hometown;
                    playerPressCover.Value = (int)record.PressCover;
                    playerFightYards.Checked = record.FightYards;
                    playerForcePass.SelectedIndex = record.ForcePasses;
                    playerPassBlockFootwork.Value = (int)record.PassBlockFootwork;
                    playerPassBlockStr.Value = (int)record.PassBlockStrength;
                    playerPlaysBall.SelectedIndex = record.PlaysBall;
                    playerPotential.Value = (int)record.Potential;
                    playerQBStance.Value = (int)record.Stance;
                    playerRunBlockStrength.Value = (int)record.RunBlockStrength;
                    playerSensePressure.SelectedIndex = record.SensePressure;
                    if (record.SidelineCatch == 1)
                        playerSidelineCatch.Checked = true;
                    else playerSidelineCatch.Checked = false;
                    playerThrowAway.Checked = record.ThrowAway;
                    playerThrowSpiral.Checked = record.ThrowSpiral;
                    playerTowel.Checked = record.PlayerTowel;
                    playerBullrush.Checked = record.DLBullrush;
                    playerDLSpin.Checked = record.DLSpinmove;
                    playerDLSwim.Checked = record.DLSwim;
                    playerHighMotor.Checked = record.HighMotor;
                    playerHandWarmer.Checked = record.HandWarmer;
                    playerDropsPasses.Checked = record.DropPasses;
                    playerBigHitter.Checked = record.BigHitter;
                    playerCoversBall.SelectedIndex = (int)record.CoversBall - 1;
                    playerClutch.Checked = record.Clutch;
                    playerStateCombo.SelectedIndex = record.HomeState;
                    playerHometown.Text = record.Hometown;
                    OriginalPosition_Combo.SelectedIndex = -1;
                    OriginalPosition_Combo.Enabled = false;
                    playerTuckRun.SelectedIndex = record.TuckRun;
                    playerTackleLow.Checked = record.TackleLow;
                    playerStripsBall.Checked = record.StripsBall;
                    playerPressureMax.Value = record.PressureMax;
                    playerPossCatch.Checked = record.PossessionCatch;
                    playerFeetInBounds.Checked = record.FeetInBounds;
                    playerPenalty.SelectedIndex = record.Penalty;
                    playerAggressiveCatch.Checked = record.AggressiveCatch;
                    playerRAC.Checked = record.RunAfterCatch;

                    playerAsset.Enabled = true;
                    playerAsset.Text = record.Asset;
                    
                    playerBirthday.Text = record.GetBirthday();
                    playerSockHeight.SelectedIndex = record.SockHeight;
                    playerConfidence.Value = record.Confidence;
                    playerFlakJacket.Checked = record.FlakJacket;
                    playerBackPlate.Checked = record.BackPlate;
                    playerCaptain.Checked = record.IsCaptain;
                    playerUndershirt.SelectedIndex = record.UnderShirt;

                }
                else
                {                    
                    playerImpactBlock.Value = 0;
                    playerLeadBlock.Value = 0;
                    playerRunBlockFinesse.Value = 0;
                    playerZoneCoverage.Value = 0;
                    playerManCover.Value = 0;
                    playerPlayRecog.Value = 0;
                    playerPursuit.Value = 0;
                    playerBlockShed.Value = 0;
                    playerTrucking.Value = 0;
                    playerElusive.Value = 0;
                    playerRB_Vision.Value = 0;
                    playerStiffArm.Value = 0;
                    playerSpinMove.Value = 0;
                    playerJukeMove.Value = 0;
                    playerShortRoute.Value = 0;
                    playerMediumRoute.Value = 0;
                    playerDeepRoute.Value = 0;
                    playerCatchTraffic.Value = 0;
                    playerSpecCatch.Value = 0;
                    playerRelease.Value = 0;
                    playerThrowShort.Value = 0;
                    playerThrowMedium.Value = 0;
                    playerThrowDeep.Value = 0;
                    playerThrowOnRun.Value = 0;
                    playerThrowPressure.Value = 0;
                    playerBreakSack.Value = 0;
                    playerPlayAction.Value = 0;
                    playerHitPower.Value = 0;
                    playerPowerMoves.Value = 0;
                    playerFinesseMoves.Value = 0;
                    playerHometown.Text = "";
                    playerPressCover.Value = 0;
                    playerFightYards.Checked = false;
                    playerForcePass.SelectedIndex = -1;
                    playerPassBlockFootwork.Value = 0;
                    playerPassBlockStr.Value = 0;
                    playerPlaysBall.SelectedIndex = -1;
                    playerPotential.Value = 0;
                    playerQBStance.Value = 0;
                    playerRunBlockStrength.Value = 0;
                    playerSensePressure.SelectedIndex = -1;
                    playerSidelineCatch.Checked = false;
                    playerThrowAway.Checked = false;
                    playerThrowSpiral.Checked = false;
                    playerTowel.Checked = false;
                    playerBullrush.Checked = false;
                    playerDLSpin.Checked = false;
                    playerDLSwim.Checked = false;
                    playerHighMotor.Checked = false;
                    playerHandWarmer.Checked = false;
                    playerDropsPasses.Checked = false;
                    playerBigHitter.Checked = false;
                    playerCoversBall.SelectedIndex = -1;
                    playerClutch.Checked = false;
                    playerTackleLow.Checked = false;
                    playerStripsBall.Checked = false;
                    playerPressureMax.Value = 0;
                    playerPossCatch.Checked = false;
                    playerFeetInBounds.Checked = false;
                    playerPenalty.SelectedIndex = -1;
                    playerAggressiveCatch.Checked = false;
                    playerRAC.Checked = false;

                    playerStateCombo.SelectedIndex = -1;
                    playerHometown.Text = "";
                    playerAsset.Text = "";
                    playerAsset.Enabled = false;
                    playerBirthday.Enabled = false;
                    playerBirthday.Text = "";
                    playerSockHeight.SelectedIndex = -1;
                    playerSockHeight.Enabled = false;
                    playerConfidence.Enabled = false;
                    playerConfidence.Value = 0;
                    playerBackPlate.Checked = false;
                    playerBackPlate.Enabled = false;
                    playerFlakJacket.Checked = false;
                    playerFlakJacket.Enabled = false;
                    playerCaptain.Checked = false;
                    playerCaptain.Enabled = false;
                    playerUndershirt.Enabled = false;
                    playerUndershirt.SelectedIndex = -1;
                }
                

                LoadPlayerSalaries(record);                
                DisplayPlayerPort();
                model.PlayerModel.SetProgressionRank();

                if (model.FileType == MaddenFileType.Franchise)
                {
                    TopForOVR_Updown.Value = model.PlayerModel.ProgRank[0];
                    TopForPhase_Updown.Value = model.PlayerModel.ProgRank[1];
                    TopAvgOvr_Updown.Value = model.PlayerModel.AvgOVR[0];

                    if (model.PlayerModel.CurrentPlayerRecord.YearsPro > 0 && model.PlayerModel.CurrentPlayerRecord.Plpl > 0)
                    {
                        if ((decimal)model.PlayerModel.CurrentPlayerRecord.Ppsp / ((decimal)model.PlayerModel.CurrentPlayerRecord.Plpl / 100) < 0)
                            BaseRank_Updown.Value = 0;
                        else BaseRank_Updown.Value = (decimal)model.PlayerModel.CurrentPlayerRecord.Ppsp / ((decimal)model.PlayerModel.CurrentPlayerRecord.Plpl / 100);
                    }
                    else BaseRank_Updown.Value = 0;

                    Both_Updown.Value = model.PlayerModel.ProgRank[2];
                }

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
            }

            lastLoadedRecord = record;

            if (holder)
                isInitialising = true;
            else isInitialising = false;
        }
        
        public void DisplayPlayerPort()
        {
            if (PlayerPortBox.Image != null)
                PlayerPortBox.Image = null;
            PlayerPortBox.BackColor = Color.White;                      
            PlayerPortBox.SizeMode = PictureBoxSizeMode.Zoom;

            if (!manager.PlayerPortDAT.isterf)
            {
                return;
            }

            int portid = model.PlayerModel.CurrentPlayerRecord.PortraitId + 1;

            if (manager.PlayerPortDAT.ParentTerf.files >= portid + 1)
            {
                if (manager.PlayerPortDAT.ParentTerf.Data.DataFiles[portid].filetype == "MMAP")
                    PlayerPortBox.Image = manager.PlayerPortDAT.ParentTerf.Data.DataFiles[portid].mmap_data.GetPortraitDisplay();
                else if (manager.PlayerPortDAT.ParentTerf.Data.DataFiles[portid].filetype == "COMP") PlayerPortBox.BackColor = Color.Green;
                else PlayerPortBox.BackColor = Color.Red;
                return;
            }

            PlayerPortBox.BackColor = Color.Black;           
        }

        
        #region Navigation Functions
       
        private void filterTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (filterTeamComboBox.SelectedIndex == 0)
                    model.PlayerModel.filterteam = -1;
                else if (filterTeamComboBox.SelectedItem.ToString() == "Retired")
                    model.PlayerModel.filterteam = 1014;
                else
                {
                    TeamRecord tr = (TeamRecord)filterTeamComboBox.SelectedItem;
                    model.PlayerModel.filterteam = tr.TeamId;
                }
                isInitialising = true;
                InitPlayerList();
                isInitialising = false;
            }
        }

        private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                if (filterPositionComboBox.SelectedIndex == 0)
                    model.PlayerModel.filterposition = -1;
                else model.PlayerModel.filterposition = filterPositionComboBox.SelectedIndex - 1;
                InitPlayerList();
                isInitialising = false;
            }
        }

        private void PlayerGridViewChange()
        {
            if (PlayerGridView.CurrentRow.Index < 0)
                return;

            DataGridViewRow row = PlayerGridView.CurrentRow;
            int r = (int)row.Cells[0].Value;
            LoadPlayerInfo(model.PlayerModel.GetPlayerByPlayerId(r));            
        }

        private void PlayerGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            isInitialising = true;

            PlayerGridViewChange();

            isInitialising = false;            
        }    
        

        private void DraftClass_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.filterdraft = DraftClass_Checkbox.Checked;
                InitPlayerList();
                isInitialising = false;
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
                model.PlayerModel.CurrentPlayerRecord.PreviousTeamId = model.PlayerModel.CurrentPlayerRecord.TeamId;

                if ((string)Team_Combo.Text == "Retired")
                {
                    if (model.FileType == MaddenFileType.Franchise)
                    {
                        model.PlayerModel.CurrentPlayerRecord.TeamId = 1014;
                        model.PlayerModel.RemovePlayerFromDepthChart(model.PlayerModel.CurrentPlayerRecord.PlayerId);
                    }
                    else
                    {
                        model.PlayerModel.DeletePlayerRecord(model.PlayerModel.CurrentPlayerRecord);
                    }
                }
                else
                {
                    model.PlayerModel.ChangePlayersTeam(((TeamRecord)Team_Combo.SelectedItem));
                }

                isInitialising = true;
                InitPlayerList();
                isInitialising = false;
                //s68 
                //Salary cap penalty.  Not for free agents.  Not sure we really want to mess with this, going to comment out and leave it for roster
                // makers to adjust manually
                //if (model.PlayerModel.CurrentPlayerRecord.TeamId == 1009)
                //    return;
                //else if (model.FileType == MaddenFileType.Franchise && model.PlayerModel.CurrentPlayerRecord.ContractYearsLeft > 0)
                //{
                //    int total = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.PreviousTeamId).SalaryCapPenalty1;

                //    for (int t = model.PlayerModel.CurrentPlayerRecord.ContractLength - model.PlayerModel.CurrentPlayerRecord.ContractYearsLeft; t < model.PlayerModel.CurrentPlayerRecord.ContractLength; t++)
                //        total += model.PlayerModel.CurrentPlayerRecord.GetSigningBonusAtYear(t);

                //    model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.PreviousTeamId).SalaryCapPenalty1 = total;
                //}
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

        private void OriginalPosition_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.OriginalPositionId = (int)OriginalPosition_Combo.SelectedIndex;
            }
        }
        
        private void playerYearsPro_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.YearsPro = (int)playerYearsPro.Value;
                //  Add more seasons to the players career
                isInitialising = true;
                if (model.FileVersion <= MaddenFileVersion.Ver2008)
                    InitStatsYear(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void CollegeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                foreach (KeyValuePair<int,college_entry> playcol in model.Colleges)
                {
                    if (playcol.Value.name == CollegeCombo.Text)
                    {
                        if (model.FileVersion == MaddenFileVersion.Ver2019)
                        {
                            if (playcol.Value.name == "No College")
                                model.PlayerModel.CurrentPlayerRecord.CollegeId = 0;
                            else model.PlayerModel.CurrentPlayerRecord.CollegeId = playcol.Value.orig_id + 1;
                        }
                        else
                        {
                            model.PlayerModel.CurrentPlayerRecord.CollegeId = playcol.Value.orig_id;
                        }
                    }
                }
            }
        }

        private void playerProBowl_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.ProBowl = playerProBowl.Checked;
            }
        }

        private void PlayerRolecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion < MaddenFileVersion.Ver2007)
                    return;
                else if (model.FileVersion == MaddenFileVersion.Ver2019)
                {
                    model.PlayerModel.CurrentPlayerRecord.PlayerRole = PlayerRolecomboBox.SelectedIndex;
                }
                else
                {
                    int res = -1;
                    foreach (KeyValuePair<int, string> role in model.PlayerRole)
                        if (role.Value == PlayerRolecomboBox.Text)
                            res = role.Key;
                    if (res != -1)
                        model.PlayerModel.CurrentPlayerRecord.PlayerRole = res;
                }
            }
        }

        private void PlayerWeaponcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                    return;
                int res = -1;
                foreach (KeyValuePair<int, string> role in model.PlayerRole)
                    if (role.Value == PlayerWeaponcomboBox.Text)
                        res = role.Key;
                if (res != -1)
                    model.PlayerModel.CurrentPlayerRecord.PlayerWeapon = res;
            }
        }

        private void deletePlayerButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this player?\r\n\r\nAlthough this player will disappear from the editor\r\nchanges will not take effect until you save.", "About to Delete " + model.PlayerModel.CurrentPlayerRecord.FirstName + " " + model.PlayerModel.CurrentPlayerRecord.LastName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                isInitialising = true;
                int current = PlayerGridView.CurrentRow.Index;
                model.PlayerModel.DeletePlayerRecord(model.PlayerModel.CurrentPlayerRecord);
                PlayerGridView.Rows.RemoveAt(current);
                if (PlayerGridView.Rows.Count > 0)
                {
                    int idnum = (int)PlayerGridView.Rows[0].Cells[0].Value;
                    LoadPlayerInfo(model.PlayerModel.GetPlayerByPlayerId(idnum));
                }
                else
                {
                    // No players left to load, reset filters back to ALL
                    LoadPlayerInfo(null);
                                   
                }

                isInitialising = false;
                
            }
        }

        // TO DO : Fix CreatePlayer, need to set all player info to some sort of defaults
        // before it is displayed.  Set player ID #  Need to reset everything to defaults
        private void createPlayerButton_Click(object sender, EventArgs e)
        {
            PlayerRecord newRecord = model.PlayerModel.CreateNewPlayerRecord();
            // Add the player to free agents
            newRecord.TeamId = EditorModel.FREE_AGENT_TEAM_ID;
            // Need to set unique PLAYER ID
            newRecord.PlayerId = EditorModel.totalplayers + 1;
            // This sets unique POID
            newRecord.NFLID = EditorModel.totalplayers + 1;

            //Most variables start off at zero but some can't like height and weight so set them
            newRecord.Height = 72; // 6'0"
            newRecord.Weight = 40; // 200#
            model.PlayerModel.CurrentPlayerRecord = newRecord;

            isInitialising = true;
            LoadPlayerInfo(newRecord);
            isInitialising = false;
        }

        
        #endregion

        #region Player Ratings Functions
        
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

        public void CalcOverall()
        {  
            Overall.Value = model.PlayerModel.CurrentPlayerRecord.Overall;
        }
               

        private void calculateOverallButton_Click(object sender, EventArgs e)
        {
            if (model.FileVersion != MaddenFileVersion.Ver2019)
            {
                model.PlayerModel.CurrentPlayerRecord.Overall = model.PlayerModel.CurrentPlayerRecord.CalculateOverallRating(model.PlayerModel.CurrentPlayerRecord.PositionId);
                Overall.Value = model.PlayerModel.CurrentPlayerRecord.Overall;
            }
        }

        private void Overall_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.Overall = (int)Overall.Value;
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
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.BreakTackle19 = (int)playerBreakTackle.Value;
                else
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
                // 2019 field isnt morale
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                    return;
                else model.PlayerModel.CurrentPlayerRecord.Morale = (int)playerMorale.Value;
            }
        }

        private void playerEgo_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion <= MaddenFileVersion.Ver2006)
                    model.PlayerModel.CurrentPlayerRecord.Pcel = (int)playerEgo.Value;
                else model.PlayerModel.CurrentPlayerRecord.Ego = (int)playerEgo.Value;
            }
        }



        private void cbTendancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.Tendency = cbTendancy.SelectedIndex;
            }
        }

        private void cbFaceId_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.FaceID_19 = (int)PlayerFaceId.Value;
                else model.PlayerModel.CurrentPlayerRecord.FaceId = (int)PlayerFaceId.Value;
            }
        }

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

        private void CareerPhase_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.CareerPhase = CareerPhase_Combo.SelectedIndex;
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
                model.PlayerModel.CurrentPlayerRecord.SideArmed = (playerThrowingStyle.SelectedIndex == 1);
            }
        }               

        private void playerHairStyleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.HairStyle = playerHairStyleCombo.SelectedIndex;
            }
        }
        
        private void Tattoo_Left_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.LeftTattoo = (int)Tattoo_Left.Value;
        }

        private void Tattoo_Right_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.RightTattoo = (int)Tattoo_Right.Value;
        }

        #endregion

        #region Player Equipment Injury Functions

        #region misc
        private void Overall_ValueChanged_1(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.Overall = (int)Overall.Value;
            }
        }

        private void NFL_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.NFLID = (int)NFL_Updown.Value;
            }
        }
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
            injRec.IR = false;
            injRec.InjuryType = 0;
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                injRec.InjurySeverity = 5;
            }

            isInitialising = true;
            playerInjuryCombo.SelectedIndex = -1;
            LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);            
            isInitialising = false;
        }

        private void playerRemoveInjuryButton_Click(object sender, EventArgs e)
        {
            //Mark the record for deletion
            model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).SetDeleteFlag(true);

            isInitialising = true;
            LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
            isInitialising = false;
        }

        private void playerInjuryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion < MaddenFileVersion.Ver2019)
                    model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryType = playerInjuryCombo.SelectedIndex;
                else
                {
                    int test = model.PlayerModel.GetInjury(playerInjuryCombo.Text);
                    model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryType = test;
                }
            }
        }

        private void playerInjuryReserve_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).IR = playerInjuryReserve.Checked;
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
                model.PlayerModel.CurrentPlayerRecord.Visor = model.PlayerModel.GetVisor(playerVisorCombo.Text);
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
                model.PlayerModel.CurrentPlayerRecord.LeftElbow = model.PlayerModel.GetElbow(playerLeftElbowCombo.Text);
            }
        }

        private void playerRightElbowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.RightElbow = model.PlayerModel.GetElbow(playerRightElbowCombo.Text);
            }
        }

        private void playerLeftWristCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.LeftWrist = model.PlayerModel.GetWrist(playerLeftWristCombo.Text);
                if (model.FileVersion < MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.LeftWristB = model.PlayerModel.GetWrist(playerLeftWristCombo.Text);
            }
        }

        private void playerRightWristCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.RightWrist = model.PlayerModel.GetWrist(playerRightWristCombo.Text);
                if (model.FileVersion < MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.RightWristB = model.PlayerModel.GetWrist(playerRightWristCombo.Text);
            }
        }

        private void playerLeftHandCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.LeftHand = model.PlayerModel.GetGloves(playerLeftHandCombo.Text);
            }
        }

        private void playerRightHandCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.RightHand = model.PlayerModel.GetGloves(playerRightHandCombo.Text);
            }
        }

        private void playerSleevesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion < MaddenFileVersion.Ver2019)
                {
                    model.PlayerModel.CurrentPlayerRecord.SleevesLeft = playerSleevesCombo.SelectedIndex;
                    model.PlayerModel.CurrentPlayerRecord.SleevesB = playerSleevesCombo.SelectedIndex;
                }
            }
        }

        private void playerLeftKneeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.LeftKnee = (playerLeftKneeCombo.SelectedIndex == 1);
            }
        }

        private void playerRightKneeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.RightKnee = (playerRightKneeCombo.SelectedIndex == 1);
            }
        }

        private void playerLeftAnkleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.LeftShoe = playerLeftAnkleCombo.SelectedIndex;
            }
        }

        private void playerRightAnkleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.RightShoe = playerRightAnkleCombo.SelectedIndex;
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
                model.PlayerModel.CurrentPlayerRecord.HelmetStyle = model.PlayerModel.GetHelmet(playerHelmetStyleCombo.Text);
        }

        private void playerFaceMaskCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.FaceMask = model.PlayerModel.GetFaceMask(playerFaceMaskCombo.Text);                
            }
        }

        #endregion
               

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
                    {
                        if (model.FileVersion >= MaddenFileVersion.Ver2019)
                            return (type == 0 ? "Cover LB" : "Pass Rush");
                        else return (type == 0 ? "Pass Rushing" : "Run Stopping");
                    }
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
                 
        private void playerPortraitId_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (manager.PlayerPortDAT.isterf && manager.PlayerPortDAT.ParentTerf.files < playerPortraitId.Value + 1)
                {
                    playerPortraitId.Value = model.PlayerModel.CurrentPlayerRecord.PortraitId;
                    return;
                }
                
                model.PlayerModel.CurrentPlayerRecord.PortraitId = (int)playerPortraitId.Value;
                DisplayPlayerPort();
            }
        }

        private void ImportPlayerPort_Button_Click(object sender, EventArgs e)
        {
            isInitialising = true;
            string custom = manager.PlayerPortDAT.grfx.GetLoadFile();
            if (custom == "")
                return;
            manager.PlayerPortDAT.grfx = new CustomBitmap(custom, Color.White);
            if (model.PlayerModel.CurrentPlayerRecord.PortraitId + 2 > manager.PlayerPortDAT.ParentTerf.files)
                manager.PlayerPortDAT.ParentTerf.Expand(model.PlayerModel.CurrentPlayerRecord.PortraitId + 2);
            manager.PlayerPortDAT.ParentTerf.Data.DataFiles[model.PlayerModel.CurrentPlayerRecord.PortraitId + 1].mmap_data.ImportGraphic(manager.PlayerPortDAT.grfx.fixed_dds);
            manager.PlayerPortDAT.changed = true;
            isInitialising = false;            
            
            DisplayPlayerPort();      
        }

        private void PlayerPortraitExport_Button_Click(object sender, EventArgs e)
        {
            string savefilename = "";
            SaveFileDialog portsavedialog = new SaveFileDialog();
            portsavedialog.Title = "Save Player Portrait";
            portsavedialog.Filter = "BMP Image | *.BMP";
            portsavedialog.CheckPathExists = true;            

            if (portsavedialog.ShowDialog() == DialogResult.OK)            
               savefilename = portsavedialog.FileName;
            if (savefilename == "")
                return;

            Image image = manager.PlayerPortDAT.ParentTerf.Data.DataFiles[model.PlayerModel.CurrentPlayerRecord.PortraitId + 1].mmap_data.GetPortraitDisplay();
            image.Save(savefilename, ImageFormat.Bmp);
        }

        #endregion

        
        
        
        #region Player Contract/Salary Functions

        private void LoadPlayerSalaries(PlayerRecord record)
        {
            //bool orig = isInitialising;
            //isInitialising = true;
            int tempteamsalary = 0;
            bool Madden19 = false;
            if (model.FileVersion == MaddenFileVersion.Ver2019 && model.FileType == MaddenFileType.Roster)
                Madden19 = true;
            if (model.FileType == MaddenFileType.DBTeam)
                Madden19 = true;

            #region Inits
            #region Misc Salary Panel
            UseActualNFLSalaryCap_Checkbox.Checked = false;
            CalcTeamSalary_Checkbox.Checked = false;
            SalaryCap.Value = 0;
            TeamSalary.Text = "";
            CalcTeamSalary.Text = "";            
            TeamCapRoom.Text = "";
            Penalty0.Value = 0;
            Penalty1.Value = 0;
            TeamSalaryRank.Text = "";
            SalaryRankCombo.SelectedIndex = -1;
            Top5.Value = 0;
            Top10.Value = 0;
            LeagueAVG.Value = 0;
            Top5AVG.Value = 0;
            Top10AVG.Value = 0;
            LeagueContAVG.Value = 0;
            UseActualNFLSalaryCap_Checkbox.Enabled = false;
            CalcTeamSalary_Checkbox.Enabled = false;
            SalaryCap.Enabled = false;
            TeamSalary.Enabled = false;
            CalcTeamSalary.Enabled = false;
            TeamCapRoom.Enabled = false;
            Penalty0.Enabled = false;
            Penalty1.Enabled = false;
            TeamSalaryRank.Enabled = false;
            SalaryRankCombo.Enabled = false;
            Top5.Enabled = false;
            Top10.Enabled = false;
            LeagueAVG.Enabled = false;
            Top5AVG.Enabled = false;
            Top10AVG.Enabled = false;
            LeagueContAVG.Enabled = false;

            #endregion

            #region Player Contract
            playerCapHit.Text = "";
            playerCapHit.Enabled = false;
            PlayerHoldOut.Checked = false;
            PlayerHoldOut.Enabled = false;
            InactiveCheckbox.Checked = false;
            InactiveCheckbox.Enabled = false;
            PlayerBonus0.Value = 0;
            PlayerBonus1.Value = 0;
            PlayerBonus2.Value = 0;
            PlayerBonus3.Value = 0;
            PlayerBonus4.Value = 0;
            PlayerBonus5.Value = 0;
            PlayerBonus6.Value = 0;
            PlayerSalary0.Value = 0;
            PlayerSalary1.Value = 0;
            PlayerSalary2.Value = 0;
            PlayerSalary3.Value = 0;
            PlayerSalary4.Value = 0;
            PlayerSalary5.Value = 0;
            PlayerSalary6.Value = 0;
            PlayerBonus0.Enabled = false;
            PlayerBonus1.Enabled = false;
            PlayerBonus2.Enabled = false;
            PlayerBonus3.Enabled = false;
            PlayerBonus4.Enabled = false;
            PlayerBonus5.Enabled = false;
            PlayerBonus6.Enabled = false;
            PlayerSalary0.Enabled = false;
            PlayerSalary1.Enabled = false;
            PlayerSalary2.Enabled = false;
            PlayerSalary3.Enabled = false;
            PlayerSalary4.Enabled = false;
            PlayerSalary5.Enabled = false;
            PlayerSalary6.Enabled = false;
            #endregion

            #region Player Contract Terms
            playerTotalSalary.Value = 0;
            playerTotalSalary.Enabled = true;
            playerSigningBonus.Value = 0;
            playerSigningBonus.Enabled = true;            
            playerContractLength.Value = 0;
            playerContractLength.Enabled = true;            
            playerContractYearsLeft.Value = 0;
            playerContractYearsLeft.Enabled = true;            
            ContractIncrease.Value = 0;
            ContractIncrease.Enabled = false;            
            UseLeagueMinimum.Checked = false;
            UseLeagueMinimum.Enabled = false;            
            YearlyMinimum.Value = 0;
            YearlyMinimum.Enabled = false;            
            SubmitContract_Button.Enabled = false;
            #endregion

            #endregion

            #region Common fields
            playerTotalSalary.Value = (decimal)record.TotalSalary / 100;
            playerSigningBonus.Value = (decimal)record.BonusTotal / 100;
            playerContractLength.Value = (int)record.ContractLength;
            playerContractYearsLeft.Value = (int)record.ContractYearsLeft;
            #endregion

            #region Franchise or Madden 19 Roster/Team DB
            if (model.FileType == MaddenFileType.Franchise || Madden19)
            {                
                // Some of this isnt going to work past year 1 of Madden 19 as there is no
                // way to check current year with the roster.

                SubmitContract_Button.Enabled = true;
                PlayerHoldOut.Enabled = true;
                PlayerHoldOut.Checked = record.Holdout;
                
                ContractIncrease.Enabled = true;
                if (record.YearsPro == 0)
                    ContractIncrease.Value = 25;
                else ContractIncrease.Value = 30;

                #region Player Yearly Salary/Bonus
                PlayerBonus0.Value = (decimal)record.Bonus0 / 100;
                PlayerBonus1.Value = (decimal)record.Bonus1 / 100;
                PlayerBonus2.Value = (decimal)record.Bonus2 / 100;
                PlayerBonus3.Value = (decimal)record.Bonus3 / 100;
                PlayerBonus4.Value = (decimal)record.Bonus4 / 100;
                PlayerBonus5.Value = (decimal)record.Bonus5 / 100;
                PlayerBonus6.Value = (decimal)record.Bonus6 / 100;
                PlayerSalary0.Value = (decimal)record.Salary0 / 100;
                PlayerSalary1.Value = (decimal)record.Salary1 / 100;
                PlayerSalary2.Value = (decimal)record.Salary2 / 100;
                PlayerSalary3.Value = (decimal)record.Salary3 / 100;
                PlayerSalary4.Value = (decimal)record.Salary4 / 100;
                PlayerSalary5.Value = (decimal)record.Salary5 / 100;
                PlayerSalary6.Value = (decimal)record.Salary6 / 100;
                PlayerBonus0.Enabled = true;
                PlayerBonus1.Enabled = true;
                PlayerBonus2.Enabled = true;
                PlayerBonus3.Enabled = true;
                PlayerBonus4.Enabled = true;
                PlayerBonus5.Enabled = true;
                PlayerBonus6.Enabled = true;
                PlayerSalary0.Enabled = true;
                PlayerSalary1.Enabled = true;
                PlayerSalary2.Enabled = true;
                PlayerSalary3.Enabled = true;
                PlayerSalary4.Enabled = true;
                PlayerSalary5.Enabled = true;
                PlayerSalary6.Enabled = true;
                #endregion

                if (Madden19)
                {
                    UseActualNFLSalaryCap_Checkbox.Checked = true;
                    CalcTeamSalary_Checkbox.Checked = false;
                    SalaryCap.Enabled = false;
                }
                else
                {
                    UseLeagueMinimum.Enabled = true;
                    YearlyMinimum.Enabled = true;

                    // Franchise has salary cap penalty for 04-08
                    Penalty0.Enabled = true;
                    Penalty1.Enabled = true;
                    foreach (TeamRecord rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        if (rec.TeamId == record.TeamId)
                        {
                            Penalty0.Value = rec.SalaryCapPenalty0 / 100;
                            Penalty1.Value = rec.SalaryCapPenalty1 / 100;
                        }
                    }

                    UseActualNFLSalaryCap_Checkbox.Enabled = true;
                }

                if (UseActualNFLSalaryCap_Checkbox.Checked)
                {
                    int curyear = model.CurrentYear;

                    if (!Madden19)
                    {
                        if (model.FranchiseStage.CurrentStage > 12)
                            curyear++;
                    }

                    if (model.LeagueCap.ContainsKey(curyear))
                        SalaryCap.Value = (decimal)model.LeagueCap[curyear];
                    else
                    {
                        // We don't have this year's actual Salary cap, so disable it and get value from franchise instead
                        UseActualNFLSalaryCap_Checkbox.Checked = false;
                        UseActualNFLSalaryCap_Checkbox.Enabled = false;
                        if (!Madden19)
                            SalaryCap.Value = SalaryCap.Value = Math.Round((decimal)model.SalaryCapModel.SalaryCap / 1000000, 4);
                    }
                }
                else
                {
                    if (!Madden19)
                        SalaryCap.Value = Math.Round((decimal)model.SalaryCapModel.SalaryCap / 1000000, 4);
                }
                
                
                if (!Madden19 && record.TeamId != 1009 && record.TeamId != 1023 && record.TeamId != 1010 && record.TeamId != 1011)
                {
                    TeamRecord teamRecord = model.TeamModel.GetTeamRecord(record.TeamId);                    
                    TeamSalary.Text = "" + ((double)teamRecord.Salary / 100.0);
                    tempteamsalary = GetTeamSalaryCap(record.TeamId);

                    if (CalcTeamSalary_Checkbox.Checked)
                    {
                        CalcTeamSalary.Text = "" + (decimal)tempteamsalary / 100;
                    }

                    //if (CalcTeamSalary_Checkbox.Checked)
                   // {
                   //     TeamCapRoom.Text = "" + (SalaryCap.Value - (decimal)tempteamsalary / 100).ToString();
                   // }
                   // else
                   // {
                   //     TeamCapRoom.Text = "" + Math.Round((((double)model.SalaryCapModel.SalaryCap / 10000.0 - (double)(model.TeamModel.GetTeamRecord(record.TeamId).Salary)) / 100.0), 2);
                   // }
                    
                    playerCapHit.Text = "" + ((double)record.CurrentSalary / 100.0);

                    TeamNeeds(record);
                    LoadPositionSalaries(record);
                    LoadFreeAgents(record);
                    if (record.TeamId < 32)
                        GetTeamSalaries();
                }
            }
            #endregion

            

            //if (orig)
            //    isInitialising = true;
            //else isInitialising = false;
        }
        
        private void playerContractLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                //Before we set it make sure its ok
                if (playerContractLength.Value < playerContractYearsLeft.Value)
                    playerContractYearsLeft.Value = playerContractLength.Value;

                if (UseLeagueMinimum.Checked)
                {
                    int holder = (int)(YearlyMinimum.Value * playerContractLength.Value * 100);
                    playerTotalSalary.Minimum = (decimal)holder / 100;
                    if ((double)(YearlyMinimum.Value * playerContractLength.Value * 100) > holder)
                        playerTotalSalary.Minimum += (decimal).01;
                    if (playerTotalSalary.Value < playerTotalSalary.Minimum)
                        playerTotalSalary.Value = playerTotalSalary.Minimum;
                }
            }
        }

        private void playerContractYearsLeft_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                //Before we set it make sure its ok
                if (playerContractYearsLeft.Value > playerContractLength.Value)
                {
                    playerContractLength.Value = playerContractYearsLeft.Value;
                }
            }
        }

        private void playerSigningBonus_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.BonusTotal = (int)playerSigningBonus.Value * 100;
            }
        }

        private void playerTotalSalary_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                playerTotalSalary.Minimum = 0;

                if (playerTotalSalary.Value < playerTotalSalary.Minimum)                // dont need this but...             
                    playerTotalSalary.Value = playerTotalSalary.Minimum;

                decimal temp = playerTotalSalary.Value;
                int holder = (int)(playerTotalSalary.Value * 100);
                isInitialising = true;
                playerTotalSalary.Value = (decimal)holder / 100;
                if (temp * 100 > holder)
                {
                    playerTotalSalary.Value += (decimal).01;
                }

                if (UseLeagueMinimum.Checked)
                {
                    holder = (int)(YearlyMinimum.Value * playerContractLength.Value * 100);
                    playerTotalSalary.Minimum = (decimal)holder / 100;
                    if ((double)(YearlyMinimum.Value * playerContractLength.Value * 100) > holder)
                        playerTotalSalary.Minimum += (decimal).01;
                    if (playerTotalSalary.Value < playerTotalSalary.Minimum)
                    {
                        playerTotalSalary.Value = playerTotalSalary.Minimum;
                    }
                }
                isInitialising = false;
            }
        }

        public int GetTeamSalaryCap(int TeamId)
        {           
            int tempcap = 0;
            List<int> PlayerIDs = new List<int>();

            try
            {
                foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    PlayerRecord rec = (PlayerRecord)record;

                    if (rec.TeamId == TeamId)
                    {
                        bool valid = true;

                        if (model.FileType == MaddenFileType.Franchise)
                        {
                            foreach (TableRecordModel injrec in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
                            {
                                InjuryRecord playerinjury = (InjuryRecord)injrec;
                                if (playerinjury.PlayerId == rec.PlayerId)
                                {
                                    if (playerinjury.IR)
                                        valid = false;
                                }
                                // if player is not on injured reserve count his salary for total team salary
                            }
                        }

                        if (valid && !PlayerIDs.Contains(rec.PlayerId))
                            PlayerIDs.Add(rec.PlayerId);

                        if (model.FileType == MaddenFileType.Franchise && valid)
                            tempcap += rec.CurrentSalary;
                        else if (model.FileType == MaddenFileType.Roster)
                        {
                            int currentcap = (int)(CalculateCapHit(rec, 25, false) * 100);
                            tempcap += currentcap;
                        }
                    }
                }
            }
            catch
            {
                
            }

            // subtract any current cap penalties
            tempcap -= model.TeamModel.GetTeamRecord(TeamId).SalaryCapPenalty0;

            return tempcap;
        }

        private decimal CalculateCapHit(PlayerRecord rec, decimal rate, bool causeDirty)
        {
            if (causeDirty)
            {
                rec.ContractLength = (int)playerContractLength.Value;
                rec.ContractYearsLeft = (int)playerContractYearsLeft.Value;
                rec.TotalSalary = (int)(playerTotalSalary.Value * 100);
                rec.BonusTotal = (int)(playerSigningBonus.Value * 100);
            }

            if (rec.ContractLength == 0)
            {
                rec.ContractYearsLeft = 0;
                rec.TotalSalary = 0;
                rec.BonusTotal = 0;
                return 0;
            }

            int totalsal = rec.TotalSalary;
            int totalbonus = rec.BonusTotal;
            int[] estYearlySalary = new int[7];
            int[] estYearlyBonus = new int[7];
            int runtotalsal = 0;
            int runtotalbon = 0;

            int minimum = (int)(YearlyMinimum.Value * 100);
            double perc = 0;
            double x = 0;
            double tempsal = 0;

            // Salaries are not supposed to increase more than 30% each year, for rookies this is 25%
            // Salary and bonus money needs to be multiples of 10,000 which is .01 million, so we need
            // to round all decimal results to 2 places.  We already fixed totalsalary to round up earlier.
            // Will keep track of running totals and just fix it on the last year of the contract to be lazy            

            // several things can happen here, the yearly increase rate can cause the yearly minimum salary to be too low.

            int maxrate = 0;
            if (rec.YearsPro == 0)
                maxrate = 25;
            else maxrate = 30;

            List<double> year1 = new List<double>();
            perc = 0;

            for (int t = 0; t < rec.ContractLength; t++)
            {
                int r = maxrate;
                x = 1 + (double)r / 100; perc += Math.Pow(x, t);
                tempsal = (double)(totalsal - totalbonus) / perc;
                year1.Add(tempsal);
                if (year1[year1.Count - 1] + (double)totalbonus / rec.ContractLength >= minimum)
                {
                    maxrate = r;
                    break;
                }
            }

            if (maxrate < rate)
            {
                rate = maxrate;
                ContractIncrease.Value = maxrate;
            }

            runtotalsal = 0;
            runtotalbon = 0;
            perc = 0;

            for (int count = 0; count < 7; count++)
            {
                estYearlySalary[count] = 0;
                estYearlyBonus[count] = 0;
            }

            x = 1 + (double)rate / 100;
            for (int t = 0; t < rec.ContractLength; t++)
                perc += Math.Pow(x, t);
            tempsal = (double)(totalsal - totalbonus) / perc;

            for (int i = 0; i < rec.ContractLength; i++)
            {
                if (i < rec.ContractLength - 1)
                {
                    estYearlySalary[i] = (int)(tempsal * Math.Pow(x, i));
                    estYearlyBonus[i] = (int)(totalbonus / rec.ContractLength);
                }
                else
                {
                    estYearlySalary[i] = totalsal - totalbonus - runtotalsal;
                    estYearlyBonus[i] = totalbonus - runtotalbon;
                }
                runtotalsal += estYearlySalary[i];
                runtotalbon += estYearlyBonus[i];
            }

            //  save contract to player table
            rec.Salary0 = (int)estYearlySalary[0];
            rec.Salary1 = (int)estYearlySalary[1];
            rec.Salary2 = (int)estYearlySalary[2];
            rec.Salary3 = (int)estYearlySalary[3];
            rec.Salary4 = (int)estYearlySalary[4];
            rec.Salary5 = (int)estYearlySalary[5];
            rec.Salary6 = (int)estYearlySalary[6];
            rec.Bonus0 = (int)estYearlyBonus[0];
            rec.Bonus1 = (int)estYearlyBonus[1];
            rec.Bonus2 = (int)estYearlyBonus[2];
            rec.Bonus3 = (int)estYearlyBonus[3];
            rec.Bonus4 = (int)estYearlyBonus[4];
            rec.Bonus5 = (int)estYearlyBonus[5];
            rec.Bonus6 = (int)estYearlyBonus[6];

            if (causeDirty)
                rec.FixCurrentSalary();

            int currentyear = rec.ContractLength - rec.ContractYearsLeft;
            int currentsalary = estYearlySalary[currentyear] + estYearlyBonus[currentyear];
            return (decimal)currentsalary / 100;
        }       

        #region Specific Years Salary and Bonus
        private void PlayerSalary0_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary0 = (int)(PlayerSalary0.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary1 = (int)(PlayerSalary1.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary2 = (int)(PlayerSalary2.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary3_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary3 = (int)(PlayerSalary3.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary4_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary4 = (int)(PlayerSalary4.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary5_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary5 = (int)(PlayerSalary5.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerSalary6_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Salary6 = (int)(PlayerSalary6.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus0_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus0 = (int)(PlayerBonus0.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus1 = (int)(PlayerBonus1.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus2 = (int)(PlayerBonus2.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus3_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus3 = (int)(PlayerBonus3.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus4_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus4 = (int)(PlayerBonus4.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus5_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus5 = (int)(PlayerBonus5.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void PlayerBonus6_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.PlayerModel.CurrentPlayerRecord.Bonus6 = (int)(PlayerBonus6.Value * 100);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }
        #endregion

        private void SalaryCap_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                isInitialising = true;
                model.SalaryCapModel.SalaryCap = (int)(SalaryCap.Value * 1000000);
                LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }
                
        private void Penalty0_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).SalaryCapPenalty0 = (int)(Penalty0.Value * 100);
            }
        }

        private void Penalty1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId).SalaryCapPenalty1 = (int)(Penalty1.Value * 100);
            }
        }
             
        private void SubmitContract_Click(object sender, EventArgs e)
        {
            isInitialising = true;
            CalculateCapHit(model.PlayerModel.CurrentPlayerRecord, ContractIncrease.Value, true);            
            LoadPlayerSalaries(model.PlayerModel.CurrentPlayerRecord);
            isInitialising = false;
        }

        private void PlayerHoldOut_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                model.PlayerModel.CurrentPlayerRecord.Holdout = PlayerHoldOut.Checked;
            }
        }

        private void InactiveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                List<InactiveRecord> remove = new List<InactiveRecord>();
                if (!InactiveCheckbox.Checked)
                {
                    foreach (InactiveRecord iar in model.TableModels[EditorModel.INACTIVE_TABLE].GetRecords())
                    {
                        InactiveRecord i = (InactiveRecord)iar;
                        if (i.PlayerID == model.PlayerModel.CurrentPlayerRecord.PlayerId)
                            remove.Add(i);
                    }
                    foreach (InactiveRecord test in remove)
                        test.SetDeleteFlag(true);
                }
                else
                {
                    InactiveRecord record = (InactiveRecord)model.TableModels[EditorModel.INACTIVE_TABLE].CreateNewRecord(true);
                    record.PlayerID = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                    record.TeamId = model.PlayerModel.CurrentPlayerRecord.TeamId;
                }
            }
        }

        

        private void GetTeamSalaries()
        {
            teamsalaries.Clear();
            List<int> topsals = new List<int>();
            TeamRecord comp = model.TeamModel.GetTeamRecord(model.PlayerModel.CurrentPlayerRecord.TeamId);

            foreach (TeamRecord tr in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
            {
                TeamRecord rec = (TeamRecord)tr;
                if (rec.TeamId > 31)
                    continue;
                else if (SalaryRankCombo.SelectedIndex == 2 && comp.DivisionId != rec.DivisionId)
                    continue;
                else if (SalaryRankCombo.SelectedIndex == 1 && comp.ConferenceId != rec.ConferenceId)
                    continue;
                else teamsalaries.Add(rec.TeamId, GetTeamSalaryCap(rec.TeamId));
            }
            foreach (KeyValuePair<int, int> pair in teamsalaries)
                topsals.Add(pair.Value);
            topsals.Sort(delegate(int x, int y)
            { return y.CompareTo(x); });

            int rank = 1;
            bool tie = false;
            for (int c = 0; c < topsals.Count; c++)
            {
                if (c < topsals.Count - 1)
                    if (topsals[c + 1] == topsals[c])
                        tie = true;
                    else tie = false;

                if (topsals[c] == teamsalaries[model.PlayerModel.CurrentPlayerRecord.TeamId])
                    break;
                else if (tie)
                    rank += 2;
                else rank++;
            }

            string text = "";
            if (tie)
                text += "T";
            text += rank.ToString();
            TeamSalaryRank.Text = text;
        }

        private void LoadFreeAgents(PlayerRecord record)
        {
            FreeAgents.DataBindings.Clear();
            FreeAgents.RowHeadersVisible = false;
            if (FreeAgents.Columns.Count == 0)
            {
                FreeAgents.Columns.Add("Name", "Name");
                FreeAgents.Columns[0].Width = 80;
                FreeAgents.Columns.Add("OVR", "Ovr");
                FreeAgents.Columns[1].Width = 32;
                FreeAgents.Columns.Add("Age", "Age");
                FreeAgents.Columns[2].Width = 32;
            }
            List<PlayerRecord> fa = new List<PlayerRecord>();
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord newplayer = (PlayerRecord)rec;
                if (record.PositionId == newplayer.PositionId && newplayer.TeamId == 1009)
                    fa.Add(newplayer);
            }

            fa.Sort(delegate(PlayerRecord x, PlayerRecord y)
            { return ((decimal)y.Overall).CompareTo(x.Overall); });

            for (int c = 0; c < fa.Count; c++)
            {
                FreeAgents.Rows.Add();
                FreeAgents.Rows[c].Cells[0].Value = fa[c].FirstName[0] + "." + fa[c].LastName;
                FreeAgents.Rows[c].Cells[1].Value = fa[c].Overall;
                FreeAgents.Rows[c].Cells[2].Value = fa[c].Age;
                FreeAgents.Rows[c].ReadOnly = true;
            }
        }

        private void LoadPositionSalaries(PlayerRecord record)
        {
            PositionSalary.DataBindings.Clear();
            PositionSalary.RowHeadersVisible = false;
            if (PositionSalary.Columns.Count == 0 || PositionSalary.Columns.Count == 5 && IncludeOverall.Checked || PositionSalary.Columns.Count == 6 && !IncludeOverall.Checked)
            {
                PositionSalary.Columns.Clear();
                PositionSalary.Columns.Add("Name", "Name");
                PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 80;
                if (IncludeOverall.Checked)
                {
                    PositionSalary.Columns.Add("OVR", "OVR");
                    PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 40;
                }
                PositionSalary.Columns.Add("Cur", "Cur");
                PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 40;
                PositionSalary.Columns.Add("Total", "Total");
                PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 40;
                PositionSalary.Columns.Add("Len", "Len");
                PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 40;
                PositionSalary.Columns.Add("Avg", "AVG");
                PositionSalary.Columns[PositionSalary.ColumnCount - 1].Width = 40;
            }

            List<PlayerRecord> top = new List<PlayerRecord>();
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord newplayer = (PlayerRecord)rec;
                if (record.PositionId == newplayer.PositionId && newplayer.TeamId >= 0 && newplayer.TeamId < 32 && newplayer.ContractLength != 0)
                    top.Add(newplayer);
            }
            List<int> top10 = new List<int>();
            int current = 0;
            top.Sort(delegate(PlayerRecord x, PlayerRecord y)
            { return ((decimal)y.CurrentSalary).CompareTo(x.CurrentSalary); });
            for (int c = 0; c < top.Count; c++)
            {
                top10.Add(top[c].CurrentSalary);
                current += top[c].CurrentSalary;
                if (c == 4)
                    Top5.Value = (decimal)current / 500;
                if (c == 9)
                    Top10.Value = current / 1000;
            }
            LeagueAVG.Value = current / (100 * top10.Count);

            for (int c = 0; c < top.Count; c++)
            {
                PositionSalary.Rows.Add();
                PositionSalary.Rows[c].Cells[0].Value = top[c].FirstName[0] + "." + top[c].LastName;
                if (IncludeOverall.Checked)
                    PositionSalary.Rows[c].Cells[1].Value = top[c].Overall;
                PositionSalary.Rows[c].Cells[PositionSalary.Rows[c].Cells.Count - 4].Value = (decimal)top[c].CurrentSalary / 100;
                PositionSalary.Rows[c].Cells[PositionSalary.Rows[c].Cells.Count - 3].Value = (decimal)top[c].TotalSalary / 100;
                PositionSalary.Rows[c].Cells[PositionSalary.Rows[c].Cells.Count - 2].Value = top[c].ContractLength;
                PositionSalary.Rows[c].Cells[PositionSalary.Rows[c].Cells.Count - 1].Value = Math.Round((decimal)(top[c].TotalSalary / top[c].ContractLength) / 100, 2);
                PositionSalary.Rows[c].ReadOnly = true;
            }

            // sort list highest avg salary per year
            top.Sort(delegate(PlayerRecord x, PlayerRecord y)
            { return ((decimal)y.TotalSalary / y.ContractLength).CompareTo((decimal)x.TotalSalary / x.ContractLength); });
            List<decimal> topdec = new List<decimal>();
            decimal currentdec = 0;
            for (int c = 0; c < top.Count; c++)
            {
                topdec.Add((decimal)top[c].TotalSalary / top[c].ContractLength);
                currentdec += topdec[c];
                if (c == 4)
                    Top5AVG.Value = currentdec / 500;
                if (c == 9)
                    Top10AVG.Value = currentdec / 1000;
            }
            LeagueContAVG.Value = currentdec / (top.Count * 100);

            if (model.FileType == MaddenFileType.Franchise)
            {
                foreach (SalaryYearsPro pm in model.TableModels[EditorModel.PLAYER_MINIMUM_SALARY_TABLE].GetRecords())
                {
                    SalaryYearsPro min = (SalaryYearsPro)pm;
                    if (min.YearsPro == record.YearsPro)
                        YearlyMinimum.Value = (decimal)min.MinimumSalary / 1000000;
                }
            }
        }

        private void TeamNeeds(PlayerRecord record)
        {
            List<int> roster = new List<int>();
            for (int c = 0; c < 21; c++)
                roster.Add(0);
            int neededplayers = 0;
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord r = (PlayerRecord)rec;
                if (r.TeamId == record.TeamId && r.ContractYearsLeft != 0)
                    roster[r.PositionId]++;
            }
            // QB
            ReqQB.Text = (Math.Abs(3 - roster[0])).ToString();
            if (roster[0] < 3)
            {
                ReqQB.ForeColor = Color.Red;
                neededplayers += 3 - roster[0];
            }
            else if (roster[0] > 3)
                ReqQB.ForeColor = Color.Green;
            else ReqQB.ForeColor = Color.Black;
            ReqHB.Text = (Math.Abs(3 - roster[1])).ToString();
            if (roster[1] < 3)
            {
                ReqHB.ForeColor = Color.Red;
                neededplayers += 3 - roster[1];
            }
            else if (roster[1] > 3)
                ReqHB.ForeColor = Color.Green;
            else ReqHB.ForeColor = Color.Black;
            ReqFB.Text = (Math.Abs(1 - roster[2])).ToString();
            if (roster[2] < 1)
            {
                ReqFB.ForeColor = Color.Red;
                neededplayers++;
            }
            else if (roster[2] > 1)
                ReqFB.ForeColor = Color.Green;
            else ReqFB.ForeColor = Color.Black;
            ReqWR.Text = (Math.Abs(5 - roster[3])).ToString();
            if (roster[3] < 5)
            {
                ReqWR.ForeColor = Color.Red;
                neededplayers += 5 - roster[3];
            }
            else if (roster[3] > 5)
                ReqWR.ForeColor = Color.Green;
            else ReqWR.ForeColor = Color.Black;
            ReqTE.Text = (Math.Abs(3 - roster[4])).ToString();
            if (roster[4] < 3)
            {
                ReqTE.ForeColor = Color.Red;
                neededplayers += 3 - roster[4];
            }
            else if (roster[4] > 3)
                ReqTE.ForeColor = Color.Green;
            else ReqTE.ForeColor = Color.Black;
            ReqP.Text = (Math.Abs(1 - roster[20])).ToString();
            if (roster[20] < 1)
            {
                ReqP.ForeColor = Color.Red;
                neededplayers++;
            }
            else if (roster[20] > 1)
                ReqP.ForeColor = Color.Green;
            else ReqP.ForeColor = Color.Black;
            ReqT.Text = (Math.Abs(4 - (roster[5] + roster[9]))).ToString();
            if (roster[5] + roster[9] < 4)
            {
                ReqT.ForeColor = Color.Red;
                neededplayers += 4 - (roster[5] + roster[9]);
            }
            else if (roster[5] + roster[9] > 4)
                ReqT.ForeColor = Color.Green;
            else ReqT.ForeColor = Color.Black;
            ReqG.Text = (Math.Abs(4 - (roster[6] + roster[8]))).ToString();
            if (roster[6] + roster[8] < 4)
            {
                ReqG.ForeColor = Color.Red;
                neededplayers += 4 - (roster[6] + roster[8]);
            }
            else if (roster[6] + roster[8] > 4)
                ReqG.ForeColor = Color.Green;
            else ReqG.ForeColor = Color.Black;
            ReqC.Text = (Math.Abs(2 - roster[7])).ToString();
            if (roster[7] < 2)
            {
                ReqC.ForeColor = Color.Red;
                neededplayers += 2 - roster[7];
            }
            else if (roster[7] > 2)
                ReqC.ForeColor = Color.Green;
            else ReqC.ForeColor = Color.Black;
            ReqDE.Text = (Math.Abs(4 - (roster[10] + roster[11]))).ToString();
            if (roster[10] + roster[11] < 4)
            {
                ReqDE.ForeColor = Color.Red;
                neededplayers += 4 - (roster[10] + roster[11]);
            }
            else if (roster[10] + roster[11] > 4)
                ReqDE.ForeColor = Color.Green;
            else ReqDE.ForeColor = Color.Black;
            ReqDT.Text = (Math.Abs(3 - roster[12])).ToString();
            if (roster[12] < 3)
            {
                ReqDT.ForeColor = Color.Red;
                neededplayers += 3 - roster[12];
            }
            else if (roster[12] > 3)
                ReqDT.ForeColor = Color.Green;
            else ReqDT.ForeColor = Color.Black;
            ReqK.Text = (Math.Abs(1 - roster[19])).ToString();
            if (roster[19] < 1)
            {
                ReqK.ForeColor = Color.Red;
                neededplayers++;
            }
            else if (roster[19] > 1)
                ReqK.ForeColor = Color.Green;
            else ReqK.ForeColor = Color.Black;
            ReqOLB.Text = (Math.Abs(4 - (roster[13] + roster[15]))).ToString();
            if (roster[13] + roster[15] < 4)
            {
                ReqOLB.ForeColor = Color.Red;
                neededplayers += 4 - (roster[13] + roster[15]);
            }
            else if (roster[13] + roster[15] > 4)
                ReqOLB.ForeColor = Color.Green;
            else ReqOLB.ForeColor = Color.Black;
            ReqMLB.Text = (Math.Abs(2 - roster[14])).ToString();
            if (roster[14] < 2)
            {
                ReqMLB.ForeColor = Color.Red;
                neededplayers += 2 - roster[14];
            }
            else if (roster[14] > 2)
                ReqMLB.ForeColor = Color.Green;
            else ReqMLB.ForeColor = Color.Black;
            ReqCB.Text = (Math.Abs(5 - roster[16])).ToString();
            if (roster[16] < 5)
            {
                ReqCB.ForeColor = Color.Red;
                neededplayers += 5 - roster[16];
            }
            else if (roster[16] > 5)
                ReqCB.ForeColor = Color.Green;
            else ReqCB.ForeColor = Color.Black;
            ReqFS.Text = (Math.Abs(2 - roster[17])).ToString();
            if (roster[17] < 2)
            {
                ReqFS.ForeColor = Color.Red;
                neededplayers += 2 - roster[17];
            }
            else if (roster[17] > 2)
                ReqFS.ForeColor = Color.Green;
            else ReqFS.ForeColor = Color.Black;
            ReqSS.Text = (Math.Abs(2 - roster[18])).ToString();
            if (roster[18] < 2)
            {
                ReqSS.ForeColor = Color.Red;
                neededplayers += 2 - roster[18];
            }
            else if (roster[18] > 2)
                ReqSS.ForeColor = Color.Green;
            else ReqSS.ForeColor = Color.Black;

            int totalplayers = 0;
            foreach (int x in roster)
                totalplayers += x;
            if (totalplayers < 49)
            {
                NeededPlayers_Label.Text = "Need to Sign";
                NeededPlayers.Value = Math.Abs(49 - totalplayers);
                NeededPlayers.ForeColor = Color.Red;
            }
            else if (totalplayers > 55)
            {
                NeededPlayers_Label.Text = "Need to Cut";
                NeededPlayers.Value = Math.Abs(totalplayers - 55);
                NeededPlayers.ForeColor = Color.Green;
            }
            else
            {
                if (neededplayers > 0)
                {
                    NeededPlayers_Label.Text = "Check Pos.";
                    NeededPlayers.Value = neededplayers;
                    NeededPlayers.ForeColor = Color.Red;
                }
                else
                {
                    NeededPlayers_Label.Text = "No needed";
                    NeededPlayers.Value = 0;
                    NeededPlayers.ForeColor = Color.Black;
                }
            }

        }
        
        #endregion

        #region Player Stats
        
        public void InitStatsYear(PlayerRecord record)
        {
            if (!isInitialising)
                return;

            //  Get current Season and week
            int currentyear = model.FranchiseTime.Year;
            
            statsyear.Items.Clear();
            statsyear.Items.Add("Career");
            int endyear = currentyear + baseyear;
            int startyear = endyear - record.YearsPro;
            for (int t = endyear; t > startyear - 1; t--)
                statsyear.Items.Add(t);

            //  Set for last selected year if it is available
            
            if (selectedyear != -1 && statsyear.Items.Contains(selectedyear))
                statsyear.SelectedIndex = selectedyear - baseyear + 1;
            else statsyear.SelectedIndex = 0;
        }
                
        public void LoadPlayerGamesPlayed(PlayerRecord record, int index, bool career)
        {
            CareerGamesPlayedRecord careergamesplayed = model.PlayerModel.GetPlayersGamesCareer(record.PlayerId);
            SeasonGamesPlayedRecord seasongamesplayed = model.PlayerModel.GetSeasonGames(record.PlayerId, year);

            // Controls
            GamesPlayedPanel.Enabled = false;
            gamesplayed.Enabled = true;
            gamesplayed.Value = 0;
            gamesstarted.Value = 0;
            DownsPlayed.Value = 0;

            if (model.FileVersion == MaddenFileVersion.Ver2004)
            {
                gamesstarted.Enabled = false;
                DownsPlayed.Enabled = false;                
            }
            else
            {                
                gamesstarted.Enabled = true;
                DownsPlayed.Enabled = true;                
            }
            
            if (career && careergamesplayed != null)
            {
                GamesPlayedPanel.Enabled = true;

                if (model.FileVersion == MaddenFileVersion.Ver2004)
                {
                    gamesplayed.Value = careergamesplayed.GamesPlayed04;
                }
                else
                {
                    gamesplayed.Value = careergamesplayed.GamesPlayed;
                    gamesstarted.Value = careergamesplayed.GamesStarted;
                    DownsPlayed.Value = careergamesplayed.DownsPlayed;
                }
            }
            else if (!career && seasongamesplayed != null)
            {
                GamesPlayedPanel.Enabled = true;

                gamesplayed.Value = seasongamesplayed.GamesPlayed;

                if (model.FileVersion != MaddenFileVersion.Ver2004)
                {
                    gamesstarted.Value = seasongamesplayed.GamesStarted;
                    DownsPlayed.Value = seasongamesplayed.DownsPlayed;
                }
            }            
        }

        public void LoadPlayerPuntKick(PlayerRecord record, int index, bool career)
        {
            CareerPuntKickRecord careerpuntkick = model.PlayerModel.GetPlayersCareerPuntKick(record.PlayerId);
            SeasonPuntKickRecord seasonpuntkick = model.PlayerModel.GetPuntKick(record.PlayerId, year);
            
            KickPuntPanel.Enabled = false;

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

            if (career && careerpuntkick != null)
            {
                KickPuntPanel.Enabled = true;

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
                KickPuntPanel.Enabled = true;

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

            else return;
        }

        public void LoadPlayerOffense(PlayerRecord record, int index, bool career)
        { 
            CareerStatsOffenseRecord careeroffensestats = model.PlayerModel.GetPlayersOffenseCareer(record.PlayerId);
            SeasonStatsOffenseRecord seasonoffense = model.PlayerModel.GetOffStats(record.PlayerId, year);

            // Set controls
            OffensePanel.Enabled = false;
            if (model.FileVersion >= MaddenFileVersion.Ver2007)
            {                
                comebacks.Enabled = true;
                Firstdowns.Enabled = true;                
            }
            else
            {                
                comebacks.Enabled = false;
                Firstdowns.Enabled = false;
            }

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
            comebacks.Value = 0;
            Firstdowns.Value = 0;

            //  Set career stats
            if (career && careeroffensestats != null)
            {
                OffensePanel.Enabled = true;

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

                if (model.FileVersion >= MaddenFileVersion.Ver2007)
                {                    
                    comebacks.Value = (int)careeroffensestats.Comebacks;
                    Firstdowns.Value = (int)careeroffensestats.FirstDowns;
                }                
            }

            // Set season stats
            else if (seasonoffense != null && !career)
            {
                OffensePanel.Enabled = true;                
                
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

                if (model.FileVersion >= MaddenFileVersion.Ver2007)
                {                    
                    comebacks.Value = (int)seasonoffense.SeaComebacks;
                    Firstdowns.Value = (int)seasonoffense.SeaFirstDowns;
                }                
            }
        }

        public void LoadPlayerDefense(PlayerRecord record, int index, bool career)
        {           
            CareerStatsDefenseRecord careerdefensestats = model.PlayerModel.GetPlayersDefenseCareer(record.PlayerId);
            SeasonStatsDefenseRecord seasondefensestats = model.PlayerModel.GetDefenseStats(record.PlayerId, year);

            DefensePanel.Enabled = false;

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
            CatchesAllowed.Value = 0;
            BigHits.Value = 0;

            if (model.FileVersion >= MaddenFileVersion.Ver2007)
            {
                CatchesAllowed.Enabled = true;
                BigHits.Enabled = true;
            }
            else
            {
                CatchesAllowed.Enabled = false;
                BigHits.Enabled = false;
            }

            if (career && careerdefensestats != null)
            {
                DefensePanel.Enabled = true;

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

                if (model.FileVersion >= MaddenFileVersion.Ver2007)
                {
                    BigHits.Value = careerdefensestats.BigHits;
                    CatchesAllowed.Value = careerdefensestats.CatchesAllowed;
                }
            }

            else if (!career && seasondefensestats != null)
            {
                DefensePanel.Enabled = true;

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

                if (model.FileVersion >= MaddenFileVersion.Ver2007)
                {
                    BigHits.Value = seasondefensestats.BigHits;
                    CatchesAllowed.Value = seasondefensestats.CatchesAllowed;
                }
            }           
        }

        public void LoadPlayerOL(PlayerRecord record, int index, bool career)
        {
            CareerStatsOffensiveLineRecord careerOLstats = model.PlayerModel.GetPlayersOLCareer(record.PlayerId);
            SeasonStatsOffensiveLineRecord seaOLstats = model.PlayerModel.GetOLstats(record.PlayerId, year);

            OLPanel.Enabled = false;
            pancakes.Value = 0;
            sacksallowed.Value = 0;

            if (career && careerOLstats != null)
            {
                OLPanel.Enabled = true;
                pancakes.Value = careerOLstats.Pancakes;
                sacksallowed.Value = careerOLstats.SacksAllowed;
            }

            else if (!career && seaOLstats != null)
            {
                OLPanel.Enabled = true;
                pancakes.Value = seaOLstats.Pancakes;
                sacksallowed.Value = seaOLstats.SacksAllowed;
            }
        }

        public void LoadPlayerPKReturn(PlayerRecord record, int index, bool career)
        {
            CareerPKReturnRecord careerpkreturn = model.PlayerModel.GetPlayersCareerPKReturn(record.PlayerId);
            SeasonPKReturnRecord seasonpkreturn = model.PlayerModel.GetPKReturn(record.PlayerId, year);

            ReturnPanel.Enabled = false;
            kra.Value = 0;
            kryds.Value = 0;
            krl.Value = 0;
            krtd.Value = 0;
            pra.Value = 0;
            pryds.Value = 0;
            prl.Value = 0;
            prtd.Value = 0;

            if (career && careerpkreturn != null)
            {
                ReturnPanel.Enabled = true; 

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
                ReturnPanel.Enabled = true;

                kra.Value = seasonpkreturn.Kra;
                kryds.Value = seasonpkreturn.Kryds;
                krl.Value = seasonpkreturn.Krl;
                krtd.Value = seasonpkreturn.Krtd;
                pra.Value = seasonpkreturn.Pra;
                pryds.Value = seasonpkreturn.Pryds;
                prl.Value = seasonpkreturn.Prl;
                prtd.Value = seasonpkreturn.Prtd;
            }           
        }
        
        public void LoadPlayerStats(PlayerRecord record)
        {
            bool holder = isInitialising;
            isInitialising = true;

            bool career = false;
            if (selectedyear == -1)
                career = true;

            year = GetStatsYear();

            LoadPlayerGamesPlayed(record, year, career);            
            LoadPlayerPuntKick(record, year, career);
            LoadPlayerOffense(record, year, career);
            LoadPlayerDefense(record, year, career);
            LoadPlayerOL(record, year, career);
            LoadPlayerPKReturn(record, year, career);

            if (holder)
                isInitialising = true;
            else isInitialising = false;
        }

        public int GetStatsYear()
        { 
            if (statsyear.SelectedIndex > 0)
                year = (int)statsyear.SelectedItem - baseyear;

            return year;
        }
        
        #region Stats Functions

        private void AddStats_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddStats_Combo.SelectedIndex == -1)
                return;
            
            if (!isInitialising)
            {
                isInitialising = true;

                if (AddStats_Combo.Text == "Games Played")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersGamesCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerGamesPlayedRecord cgp = (CareerGamesPlayedRecord)model.TableModels[EditorModel.CAREER_GAMES_PLAYED_TABLE].CreateNewRecord(true);
                            cgp.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonGamesPlayedRecord sgp = (SeasonGamesPlayedRecord)model.TableModels[EditorModel.SEASON_GAMES_PLAYED_TABLE].CreateNewRecord(true);
                        sgp.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        sgp.Season = GetStatsYear();
                    }
                }
                else if (AddStats_Combo.Text == "Offense")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerStatsOffenseRecord co = (CareerStatsOffenseRecord)model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                            co.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonStatsOffenseRecord so = (SeasonStatsOffenseRecord)model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                        so.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        so.Season = GetStatsYear();
                    }
                }
                else if (AddStats_Combo.Text == "Defense")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersDefenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerStatsDefenseRecord cd = (CareerStatsDefenseRecord)model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].CreateNewRecord(true);
                            cd.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonStatsDefenseRecord sd = (SeasonStatsDefenseRecord)model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].CreateNewRecord(true);
                        sd.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        sd.Season = GetStatsYear();
                    }
                }
                else if (AddStats_Combo.Text == "Punt Kick")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersCareerPuntKick(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerPuntKickRecord cpk = (CareerPuntKickRecord)model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_TABLE].CreateNewRecord(true);
                            cpk.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonPuntKickRecord spk = (SeasonPuntKickRecord)model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_TABLE].CreateNewRecord(true);
                        spk.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        spk.Season = GetStatsYear();
                    }
                }
                else if (AddStats_Combo.Text == "Returns")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersCareerPKReturn(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerPKReturnRecord cr = (CareerPKReturnRecord)model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE].CreateNewRecord(true);
                            cr.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonPuntKickRecord sr = (SeasonPuntKickRecord)model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE].CreateNewRecord(true);
                        sr.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        sr.Season = GetStatsYear();
                    }
                }
                else if (AddStats_Combo.Text == "O-Line")
                {
                    if (statsyear.SelectedIndex == 0)
                    {
                        if (model.PlayerModel.GetPlayersOLCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId) == null)
                        {
                            CareerStatsOffensiveLineRecord col = (CareerStatsOffensiveLineRecord)model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].CreateNewRecord(true);
                            col.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        }
                    }
                    else if (statsyear.SelectedIndex != 0)
                    {
                        SeasonStatsOffensiveLineRecord sol = (SeasonStatsOffensiveLineRecord)model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].CreateNewRecord(true);
                        sol.PlayerId = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                        sol.Season = GetStatsYear();
                    }
                }

                AddStats_Combo.SelectedIndex = -1;
                LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
                isInitialising = false;
            }
        }

        private void statsyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                //  Get Stats for year or career as selected
                isInitialising = true;
                if (model.FileType == MaddenFileType.Franchise)
                {
                    if (statsyear.SelectedIndex == 0)
                        selectedyear = -1;
                    else selectedyear = (int)statsyear.SelectedItem;
                    LoadPlayerStats(model.PlayerModel.CurrentPlayerRecord);
                }
                isInitialising = false;
            }
        }

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

        private void Firstdowns_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).FirstDowns = (int)Firstdowns.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaFirstDowns = (int)Firstdowns.Value;
            }
        }

        private void comebacks_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (statsyear.Text == "Career")
                    model.PlayerModel.GetPlayersOffenseCareer(model.PlayerModel.CurrentPlayerRecord.PlayerId).Comebacks = (int)comebacks.Value;
                else model.PlayerModel.GetOffStats(model.PlayerModel.CurrentPlayerRecord.PlayerId, year).SeaComebacks = (int)comebacks.Value;
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

        private void CatchesAllowed_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BigHits_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Games Played
        
        // fix
        private void gamesstarted_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising )
            
            {
                if (model.FileVersion != MaddenFileVersion.Ver2004 && statsyear.Text == "Career")
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

        private void PlayerHeight_Feet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                SetPlayerHeight();
        }
        private void PlayerHeight_Inches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                SetPlayerHeight();
        }

        private void SetPlayerHeight()
        {
            int height = (int)PlayerHeight_Feet.SelectedIndex * 12 + (int)PlayerHeight_Inches.SelectedIndex;
            if (height > 127)
                height = 127;
            else if (height == 0)
                height = 1;
            model.PlayerModel.CurrentPlayerRecord.Height = height;

            PlayerHeight_Feet.SelectedIndex = (int)model.PlayerModel.CurrentPlayerRecord.Height / 12;
            PlayerHeight_Inches.SelectedIndex = model.PlayerModel.CurrentPlayerRecord.Height - (int)(PlayerHeight_Feet.SelectedIndex * 12);
        }

        #endregion

        private void PLGL_Updown_ValueChanged(object sender, EventArgs e)
        {

        }

        


        #endregion

        
        private void UseActualNFLSalaryCap_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.SalaryCapModel.SalaryCap = (int)(SalaryCap.Value * 1000000);
        }

         
        #region Traits
        

        private void playerThrowAway_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowAway = playerThrowAway.Checked;
        }        

        private void playerThrowSpiral_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowSpiral = playerThrowSpiral.Checked;
        }

        private void playerForcePasses_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ForcePasses = playerForcePass.SelectedIndex;
        }
        private void playerFightYards_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.FightYards = playerFightYards.Checked;
        }

        private void playerPlaysBall_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlaysBall = playerPlaysBall.SelectedIndex;
        }
               

        private void playerSidelineCatch_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (playerSidelineCatch.Checked)
                    model.PlayerModel.CurrentPlayerRecord.SidelineCatch = 1;
                else model.PlayerModel.CurrentPlayerRecord.SidelineCatch = 0;

            }
        }

        private void playerHighMotor_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.HighMotor = playerHighMotor.Checked;
        }

        private void playerDLSwim_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.DLSwim = playerDLSwim.Checked;
        }

        private void playerBullrush_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.DLBullrush = playerBullrush.Checked;
        }

        private void playerDLSpin_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.DLSpinmove = playerDLSpin.Checked;
        }

        #endregion

        private void playerPotential_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Potential = (int)playerPotential.Value;
        }

        private void playerQBStance_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Stance = (int)playerQBStance.Value;
        }       

        private void playerRelease_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Release = (int)playerRelease.Value;
        }

        private void playerThrowShort_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowShort = (int)playerThrowShort.Value;
        }

        private void playerThrowMedium_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowMedium = (int)playerThrowMedium.Value;
        }

        private void playerThrowDeep_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowDeep = (int)playerThrowDeep.Value;
        }

        private void playerThrowOnRun_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowOnRun = (int)playerThrowOnRun.Value;
        }

        private void playerThrowPressure_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ThrowPressure = (int)playerThrowPressure.Value;
        }

        private void playerBreakSack_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.BreakSack = (int)playerBreakSack.Value;
        }

        private void playerPlayAction_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayAction = (int)playerPlayAction.Value;
        }

        private void playerTrucking_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Trucking = (int)playerTrucking.Value;
        }

        private void playerElusive_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Elusive = (int)playerElusive.Value;
        }

        private void playerRB_Vision_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.RB_Vision = (int)playerRB_Vision.Value;
        }

        private void playerStiffArm_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.StiffArm = (int)playerStiffArm.Value;
        }

        private void playerSpinMove_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.SpinMove = (int)playerSpinMove.Value;
        }

        private void playerJukeMove_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.JukeMove = (int)playerJukeMove.Value;
        }

        private void playerImpactBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ImpactBlocking = (int)playerImpactBlock.Value;
        }

        private void playerLeadBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.LeadBlock = (int)playerLeadBlock.Value;
        }

        private void playerMoves_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PowerMoves = (int)playerPowerMoves.Value;
        }

        private void playerFinesseMoves_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.FinesseMoves = (int)playerFinesseMoves.Value;
        }       

        private void playerShortRoute_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ShortRoute = (int)playerShortRoute.Value;
        }

        private void playerMediumRoute_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.MediumRoute = (int)playerMediumRoute.Value;
        }

        private void playerDeepRoute_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.DeepRoute = (int)playerDeepRoute.Value;
        }

        private void playerCatchTraffic_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.CatchTraffic = (int)playerCatchTraffic.Value;
        }

        private void playerSpecCatch_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.SpecCatch = (int)playerSpecCatch.Value;
        }

        private void playerRunBlockFinesse_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.RunBlockFootwork = (int)playerRunBlockFinesse.Value;
        }

        private void playerRunBlockStrength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.RunBlockStrength = (int)playerRunBlockStrength.Value;
        }

        private void playerPassBlockFootwork_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PassBlockFootwork = (int)playerPassBlockFootwork.Value;
        }

        private void playerPassBlockStr_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PassBlockStrength = (int)playerPassBlockStr.Value;
        }

        private void playerPlayRecog_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayRecognition = (int)playerPlayRecog.Value;
        }

        private void playerPursuit_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Pursuit = (int)playerPursuit.Value;
        }

        private void playerBlockShed_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.BlockShedding = (int)playerBlockShed.Value;
        }

        private void playerZoneCoverage_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ZoneCoverage = (int)playerZoneCoverage.Value;
        }

        private void playerManCover_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ManCoverage = (int)playerManCover.Value;
        }

        private void playerPressCover_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PressCover = (int)playerPressCover.Value;
        }

        private void playerHitPower_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.HitPower = (int)playerHitPower.Value;
        }

        private void playerStateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.HomeState = (int)playerStateCombo.SelectedIndex;
        }

        private void playerHometown_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Hometown = playerHometown.Text;
        }

        private void playerTowel_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayerTowel = playerTowel.Checked;
        }

        private void playerHandWarmer_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.HandWarmer = playerHandWarmer.Checked;
        }

        private void playerBigHitter_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.BigHitter = playerBigHitter.Checked;
        }

       

        private void playerClutch_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Clutch = playerClutch.Checked;
        }

        private void playerComment_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayerComment = (int)playerComment.Value;
        }

        private void playerTuckRun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.TuckRun = (int)playerTuckRun.SelectedIndex;
        }              

        private void playerPressureMax_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PressureMax = (int)playerPressureMax.Value;
        }

        private void playerTackleLow_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.TackleLow = playerTackleLow.Checked;
        }

        private void playerStripsBall_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.StripsBall = playerStripsBall.Checked;
        }

        private void playerAsset_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Asset = playerAsset.Text;
        }

        private void playerHighPointCatch_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PossessionCatch = playerPossCatch.Checked;
        }

        private void playerTRFB_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.FeetInBounds = playerFeetInBounds.Checked;
        }        

        private void playerTRIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Penalty = playerPenalty.SelectedIndex;
        }

        private void playerTRJR_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.AggressiveCatch = playerAggressiveCatch.Checked;
        }

        private void playerRAC_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.RunAfterCatch = playerRAC.Checked;
        }

        private void playerDropsPasses_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.DropPasses = playerDropsPasses.Checked;
        }

        private void playerBirthday_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.SetBirthday(playerBirthday.Text);
        }

        private void playerCoversBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.CoversBall = playerCoversBall.SelectedIndex + 1;
        }

        private void playerPlaysBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlaysBall = playerPlaysBall.SelectedIndex;
        }

        private void playerForcePass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.ForcePasses = playerForcePass.SelectedIndex;
        }

        private void playerSensePressure_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.SensePressure = playerSensePressure.SelectedIndex;
        }

        private void playerFightYards_CheckedChanged_1(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.FightYards = playerFightYards.Checked;
        }

        private void playerLeftShoeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!isInitialising)
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                {
                    model.PlayerModel.CurrentPlayerRecord.LeftShoe = model.PlayerModel.GetShoe(playerLeftShoeCombo.Text);
                }
        }

        private void playerRightShoeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                {
                    model.PlayerModel.CurrentPlayerRecord.RightShoe = model.PlayerModel.GetShoe(playerRightShoeCombo.Text);
                }
        }

        private void playerLeftSleeve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                {
                    model.PlayerModel.CurrentPlayerRecord.SleevesLeft = model.PlayerModel.GetSleeve(playerLeftShoeCombo.Text);
                }
        }

        private void playerRightSleeve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                if (model.FileVersion > MaddenFileVersion.Ver2008)
                {
                    model.PlayerModel.CurrentPlayerRecord.SleevesRight = model.PlayerModel.GetSleeve(playerRightShoeCombo.Text);
                }
        }

        private void playerSockHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.SockHeight = (int)playerSockHeight.SelectedIndex;
        }

        private void playerQBStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.QBStyle = model.PlayerModel.GetQBStyle(playerQBStyle.Text);
        }

        private void playerJerseySleeves_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Jersey = playerJerseySleeves.SelectedIndex;
        }

        private void playerConfidence_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.Confidence = (int)playerConfidence.Value;
        }

        private void playerFlakJacket_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.FlakJacket = playerFlakJacket.Checked;
            }
        }

        private void playerBackPlate_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                    model.PlayerModel.CurrentPlayerRecord.BackPlate = playerBackPlate.Checked;
            }
        }

        private void playerArchetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayerType = model.PlayerModel.GetArchetype(playerArchetype.Text);
        }
        
        private void ExportPlayerCSV_Button_Click(object sender, EventArgs e)
        {
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
                        PlayerRecord rec = model.PlayerModel.CurrentPlayerRecord;
                        TableModel table = model.TableModels["PLAY"];
                        List<TdbFieldProperties> props = table.GetFieldList();
                        StringBuilder hbuilder = new StringBuilder();
                        StringBuilder builder = new StringBuilder();
                        StreamWriter writer = new StreamWriter(myStream);
                        hbuilder.Append("PLAY");
                        hbuilder.Append(",");
                        if (model.FileVersion == MaddenFileVersion.Ver2019)
                            hbuilder.Append("2019");
                        hbuilder.Append(",");
                        hbuilder.Append("No");
                        hbuilder.Append(",");
                        writer.WriteLine(hbuilder);
                        writer.Flush();

                        hbuilder.Clear();
                        foreach (TdbFieldProperties tdb in props)
                        {
                            hbuilder.Append(tdb.Name);
                            hbuilder.Append(",");
                        }
                        writer.WriteLine(hbuilder.ToString());
                        writer.Flush();
                        
                        foreach (TdbFieldProperties tdb in props)
                        {
                            if (tdb.FieldType == TdbFieldType.tdbString)
                                builder.Append(rec.GetStringField(tdb.Name));
                            else if (tdb.FieldType == TdbFieldType.tdbFloat)
                                builder.Append(rec.GetFloatField(tdb.Name));
                            else
                            {
                                int test = rec.GetIntField(tdb.Name);
                                builder.Append(test);
                            }
                            builder.Append(",");
                        }
                        writer.WriteLine(builder.ToString());
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void playerInjurySevere_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                {
                    model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjurySeverity = (int)playerInjurySevere.Value;
                }
        }

        private void playerInjuryReturn_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                if (model.FileVersion == MaddenFileVersion.Ver2019)
                {
                    model.PlayerModel.GetPlayersInjuryRecord(model.PlayerModel.CurrentPlayerRecord.PlayerId).InjuryReturn = (int)playerInjuryReturn.Value;
                }
        }

        private void playerOVRArchetypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                int type =  model.PlayerModel.GetArchetype(playerOVRArchetypeCombo.Text);
                playerOVRArchetype.Text = playeroverall.GetOverall19(model.PlayerModel.CurrentPlayerRecord, model.PlayerModel.CurrentPlayerRecord.PositionId, type).ToString();
            }
                
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.FirstName = firstNameTextBox.Text;
        }

        private void PlayerID_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.PlayerId = (int)PlayerID_Updown.Value;
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.LastName = lastNameTextBox.Text;
        }

        private void playerCaptain_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.IsCaptain = playerCaptain.Checked;
        }

        private void playerUndershirt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.PlayerModel.CurrentPlayerRecord.UnderShirt = playerUndershirt.SelectedIndex;
        }

        

    }
}