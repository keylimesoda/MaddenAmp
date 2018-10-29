/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.DatEditor;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    // TO DO : Add phase, etc editing options for team effects for roles

    
    public partial class OptionsForm : UserControl, IEditorForm
    {        
        private EditorModel model;
        private MGMT _manager;
        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        
        public DataGridView CollegeView;
        public DataGridView RoleInfoView;
        public DataGridView TeamEffectView;
        public DataGridView PlayerEffectView;
        public DataGridView DefineRolesView;
        public DataGridView StatsReqView;
        public DataGridView OVR_View;
        public DataGridView RegressionView;

        public bool isInitializing = true;
        public int currentrolerow = 0;
        public RoleInfo currentroleinfo = null;
        public int currentplayereffectrow = 0;
        public RolePlayerEffects currentplayereffect = null;
        public int currentteameffectrow = 0;
        public RoleTeamEffects currentteameffect = null;
        public int currentcollegerow = 0;
        public OverallRecord current_ovr = null;
        public int current_ovr_row = 0;
        public PlayerRegression currentregression = null;
        public int currentregressionrow = 0;
        public string currentpttable = "PTQB";
        public int currentptpos = 0;
        public int currentptgroup = -1;
        public int currentptstat = -1;


        public List<CollegesRecord> delete_these = new List<CollegesRecord>();

        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        public void InitialiseUI()
        {
            isInitializing = true;

            
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                StreamControl.TabPages.Clear();
                InitGameOptions();
            }
            else
            {
                InitGameOptions();
                InitPortraitsUI();
                InitCollegesUI();
                InitOVRUI();
                InitProgressionUI();
                InitRolesUI();
                InitPlayerEffectsUI();
                InitTeamEffectsUI();

                StreamedFileName_Textbox.Text = manager.config.StreamFilename;
                Streameddb_Autoload.Checked = manager.config.streamdb_autoload[(int)model.FileVersion];
                if (Streameddb_Autoload.Checked)
                    StreamedFileName_Textbox.Text = manager.config.stream_names[(int)model.FileVersion];

                DB_Template_Name_Textbox.Text = manager.config.db_misc_filename;
                DB_Misc_Autoload.Checked = manager.config.db_misc_autoload[(int)model.FileVersion];
                if (DB_Misc_Autoload.Checked)
                    DB_Template_Name_Textbox.Text = manager.config.db_misc_names[(int)model.FileVersion];
            }
            

            isInitializing = false;
        }

        public void CleanUI()
        {

        }
        #endregion

        #region InitUI
        public void InitGameOptions()
        {
            if (StreamControl.TabPages.Contains(Game))
                StreamControl.TabPages.Remove(Game);
            if (model.FileType == MaddenFileType.Franchise || model.FileType == MaddenFileType.UserConfig)
            {
                StreamControl.TabPages.Add(Game);
                Game.Controls.Clear();
                if (model.FileVersion < MaddenFileVersion.Ver2019)
                {
                    GameOptions form = new GameOptions();
                    form.Model = model;
                    form.Init();
                    Game.Controls.Add(form);
                }
                else
                {
                    GameOptions2019 form = new GameOptions2019();
                    form.Model = model;
                    form.manager = manager;
                    form.InitialiseUI();
                    Game.Controls.Add(form);
                }
            }            
        }        

        public void InitPortraitsUI()
        {
            if (StreamControl.TabPages.Contains(Portraits))
                StreamControl.TabPages.Remove(Portraits);

            if (!StreamControl.TabPages.Contains(Portraits))
                StreamControl.TabPages.Add(Portraits);

            InitDatOptionsUI();
        }

        public void InitCollegesUI()
        {
            if (StreamControl.TabPages.Contains(Colleges))
                StreamControl.TabPages.Remove(Colleges);
            if (manager.stream_model != null)
            {
                if (!StreamControl.TabPages.Contains(Colleges))
                    StreamControl.TabPages.Add(Colleges);

                InitCollegesDisplay();
                CollegeID.Maximum = 511;
                MasterID.Maximum = 255;
            }
        }

        public void InitRegressionUI()
        {
            if (StreamControl.TabPages.Contains(RegressionTab))
                StreamControl.TabPages.Remove(RegressionTab);

            if (manager.stream_model != null && model.FileVersion > MaddenFileVersion.Ver2004)
            {

                if (!StreamControl.TabPages.Contains(RegressionTab))
                    StreamControl.TabPages.Add(RegressionTab);

                InitRegressionView(-1);

                DraftedPositionFilter_Combobox.Items.Clear();            
                DraftedPositionFilter_Combobox.Items.Add("ALL");
                foreach (string pos in Enum.GetNames(typeof(PlayerDraftedPositions)))
                {
                    DraftedPositionFilter_Combobox.Items.Add(pos);
                }

                Sort1_Filter.Items.Add("None");
                Sort2_Filter.Items.Add("None");
                Sort3_Filter.Items.Add("None");
                Sort1_Filter.Items.Add("Draft Position");
                Sort2_Filter.Items.Add("Draft Position");
                Sort3_Filter.Items.Add("Draft Position");
                Sort1_Filter.Items.Add("Group");
                Sort2_Filter.Items.Add("Group");
                Sort3_Filter.Items.Add("Group");
                Sort1_Filter.Items.Add("Years Pro");
                Sort2_Filter.Items.Add("Years Pro");
                Sort3_Filter.Items.Add("Years Pro");
            }
        }

        public void InitOVRUI()
        {
            if (StreamControl.TabPages.Contains(OVR))
                StreamControl.TabPages.Remove(OVR);
            
            if (manager.db_misc_model != null)
            {
                if (!StreamControl.TabPages.Contains(OVR))
                    StreamControl.TabPages.Add(OVR);

                OVR_Position_Combo.Items.Clear();

                for (int p = 0; p < 21; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions), p);
                    OVR_Position_Combo.Items.Add(pos);
                }
               
                InitOVRView();
            }

        }

        public void InitRolesUI()
        {
            if (StreamControl.TabPages.Contains(RolesTab))            
                StreamControl.TabPages.Remove(RolesTab);

            if (manager.stream_model != null && model.FileVersion >= MaddenFileVersion.Ver2007)
            {
                RoleAddNew_Button.Enabled = false;

                if (!StreamControl.TabPages.Contains(RolesTab))
                    StreamControl.TabPages.Add(RolesTab);
                
                InitRolesDisplay();

                if (manager.stream_model.TableModels[EditorModel.ROLES_INFO].RecordCount < 64)     // Role + Weapon are limited to 0-63
                    RoleAddNew_Button.Enabled = true;
            }
        }
        
        public void InitPlayerEffectsUI()
        {
            if (StreamControl.TabPages.Contains(PlayerEffect))
                StreamControl.TabPages.Remove(PlayerEffect);
            if (manager.stream_model != null && model.FileVersion >= MaddenFileVersion.Ver2007)
            {
                if (!StreamControl.TabPages.Contains(PlayerEffect))
                    StreamControl.TabPages.Add(PlayerEffect);

                PlayerEffect_Position.Items.Clear();
                for (int p = 0; p < 21; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions), p);
                    PlayerEffect_Position.Items.Add(pos);
                }

                PlayerEffect_Position.Items.Add("ALL");

                PlayerEffect_Role.Items.Clear();
                for (int c = 0; c < manager.stream_model.TableModels[EditorModel.ROLES_INFO].RecordCount; c++)
                {
                    foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
                    {
                        RoleInfo ri = (RoleInfo)rec;
                        if (ri.PlayerRole == c)
                        {
                            PlayerEffect_Role.Items.Add(ri.RoleName);                            
                        }
                    }
                }                

                PlayerEffect_Rating.Items.Clear();
                foreach (string rat in Enum.GetNames(typeof(PlayerRating)))
                {
                    PlayerEffect_Rating.Items.Add(rat);
                }


                InitPlayerEffectDisplay();
            }
        }

        public void InitTeamEffectsUI()
        {
            if (StreamControl.TabPages.Contains(TeamEffects))
                StreamControl.TabPages.Remove(TeamEffects);
            if (manager.stream_model != null && model.FileVersion >= MaddenFileVersion.Ver2007)
            {
                if (!StreamControl.TabPages.Contains(TeamEffects))
                    StreamControl.TabPages.Add(TeamEffects);

                TeamEffect_Pos.Items.Clear();
                for (int p = 0; p < 21; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions), p);
                    TeamEffect_Pos.Items.Add(pos);
                }                  
                TeamEffect_Pos.Items.Add("ALL");

                TeamEffect_Rating.Items.Clear();
                foreach (string rat in Enum.GetNames(typeof(PlayerRating)))
                {
                    TeamEffect_Rating.Items.Add(rat);
                }

                TeamEffect_Role.Items.Clear();
                for (int c = 0; c < manager.stream_model.TableModels[EditorModel.ROLES_INFO].RecordCount; c++)
                {
                    foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
                    {
                        RoleInfo ri = (RoleInfo)rec;
                        if (ri.PlayerRole == c)
                        {                            
                            TeamEffect_Role.Items.Add(ri.RoleName);
                        }
                    }
                }

                InitTeamEffectDisplay();
            }
        }
       
        public void InitProgressionUI()
        {
            if (model.FileType != MaddenFileType.Franchise || model.FileVersion == MaddenFileVersion.Ver2004 || manager.stream_model == null)
            {                
                if (StreamControl.TabPages.Contains(ProgressionTab))
                    StreamControl.TabPages.Remove(ProgressionTab);
                return;
            }
            else
            {                
                if (!StreamControl.TabPages.Contains(ProgressionTab))
                    StreamControl.TabPages.Add(ProgressionTab);
                

                if (model.PlayerModel == null || manager.stream_model == null)
                    LinkPlayer_Checkbox.Enabled = false;
                else LinkPlayer_Checkbox.Enabled = true;

                ScoringPosition_Combobox.Items.Clear();
                foreach (string pos in Enum.GetNames(typeof(PlayerDraftedPositions)))
                {
                    ScoringPosition_Combobox.Items.Add(pos);
                }
                ScoringPosition_Combobox.Items.Add("KP");
                
                ScoringPosition_Combobox.SelectedIndex = 0;
                ScoringGroup_Combobox.SelectedIndex = 0;

                InitScoringStatType();
               
                ProgressionView.Visible = true;
                InitProgressionView();

                ProgressionScoringView.Visible = true;
                InitProgressionScoring();

                ProgressionScheduleView.Visible = true;
                InitProgressionSchedule();                
            }

            if (model.FileType != MaddenFileType.Franchise || model.FileVersion == MaddenFileVersion.Ver2004)
            {
                if (model.PlayerModel != null && LinkPlayer_Checkbox.Checked && model.PlayerModel.CurrentPlayerRecord.FirstName != "New")
                {
                    ScoringPosition_Combobox.SelectedIndex = model.PlayerModel.CurrentPlayerRecord.OriginalPositionId + 1;
                    if (model.PlayerModel.CurrentPlayerRecord.Overall >= 90)
                        ScoringGroup_Combobox.SelectedIndex = 1;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 82 && model.PlayerModel.CurrentPlayerRecord.Overall <= 89)
                        ScoringGroup_Combobox.SelectedIndex = 2;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 76 && model.PlayerModel.CurrentPlayerRecord.Overall <= 81)
                        ScoringGroup_Combobox.SelectedIndex = 3;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 70 && model.PlayerModel.CurrentPlayerRecord.Overall <= 75)
                        ScoringGroup_Combobox.SelectedIndex = 4;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 60 && model.PlayerModel.CurrentPlayerRecord.Overall <= 69)
                        ScoringGroup_Combobox.SelectedIndex = 5;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 0 && model.PlayerModel.CurrentPlayerRecord.Overall <= 59)
                        ScoringGroup_Combobox.SelectedIndex = 6;
                }
            }
        }

        #endregion


        #region Options Form
        public OptionsForm()
        {
            InitializeComponent();
        }

        public void Init()
        {

        }
        
        #region Options Form Events
        private void StreamControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == ProgressionTab)
            {
                if (model.FileType == MaddenFileType.Franchise && model.FileVersion != MaddenFileVersion.Ver2004)
                {
                    if (model.PlayerModel != null && LinkPlayer_Checkbox.Checked && model.PlayerModel.CurrentPlayerRecord.FirstName != "New")
                    {
                        ScoringPosition_Combobox.SelectedIndex = model.PlayerModel.GetDraftedPosition();
                        if (model.PlayerModel.CurrentPlayerRecord.Overall >= 90)
                            ScoringGroup_Combobox.SelectedIndex = 1;
                        else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 82 && model.PlayerModel.CurrentPlayerRecord.Overall <= 89)
                            ScoringGroup_Combobox.SelectedIndex = 2;
                        else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 76 && model.PlayerModel.CurrentPlayerRecord.Overall <= 81)
                            ScoringGroup_Combobox.SelectedIndex = 3;
                        else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 70 && model.PlayerModel.CurrentPlayerRecord.Overall <= 75)
                            ScoringGroup_Combobox.SelectedIndex = 4;
                        else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 60 && model.PlayerModel.CurrentPlayerRecord.Overall <= 69)
                            ScoringGroup_Combobox.SelectedIndex = 5;
                        else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 0 && model.PlayerModel.CurrentPlayerRecord.Overall <= 59)
                            ScoringGroup_Combobox.SelectedIndex = 6;
                    }
                }
            }

        }


        #endregion

        #endregion

        #region Config Events

        private void Streameddb_Autoload_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.streamdb_autoload[(int)model.FileVersion] = Streameddb_Autoload.Checked;
                manager.config.Write();
            }
        }

        private void LoadStreamed_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.DefaultExt = "ros";
            dialog.Filter = "Streamed DB file (*.db)|*.db";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == "")
                return;            
            manager.stream_model = new EditorModel(filename, null,false,false);

            if (manager.stream_model != null)
            {
                manager.config.StreamFilename = filename;
                manager.config.stream_names[(int)model.FileVersion] = manager.config.StreamFilename;
                StreamedFileName_Textbox.Text = manager.config.StreamFilename;
                manager.config.Write();
                InitialiseUI();
            }
            
            else StreamedFileName_Textbox.Text = "Not Loaded";            
        }

        private void Load_DB_Template_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.DefaultExt = "ros";
            dialog.Filter = "DB Template file (*.db)|*.db";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string filename = dialog.FileName;
            if (filename == "")
                return;
            manager.db_misc_model = new EditorModel(filename, null,false,false);

            if (manager.db_misc_model != null)
            {
                manager.config.db_misc_filename = filename;
                manager.config.db_misc_names[(int)model.FileVersion] = manager.config.db_misc_filename;
                DB_Template_Name_Textbox.Text = manager.config.db_misc_filename;
                manager.config.Write();
                InitOVRUI();                
            }

            else DB_Template_Name_Textbox.Text = "Not Loaded";
        }

        private void DB_Misc_Autoload_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.db_misc_autoload[(int)model.FileVersion] = DB_Misc_Autoload.Checked;                
                manager.config.Write();
            }
        }

        #endregion
          
        #region Role Info

        public void GetCurrentRoleInfo(int selected)
        {
            foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
            {
                RoleInfo role = (RoleInfo)rec;
                if (role.PlayerRole == selected)
                {
                    currentroleinfo = role;
                    break;
                }
            }
        }
                
        public void InitRoleEditingPanel()
        {
            isInitializing = true;
            GetCurrentRoleInfo(currentrolerow);

            RoleDescription.Clear();
            RoleDescription.Text = currentroleinfo.RoleText;
            RoleName.Clear();
            RoleName.Text = currentroleinfo.RoleName;
            RoleID.Value = currentroleinfo.PlayerRole;
            RoleValue.Value = currentroleinfo.ValueEffect;
            RoleEffectType.Value = currentroleinfo.RoleRank;
            RoleRank.Value = currentroleinfo.RoleEffectType;
            RoleSmallIcon.Value = currentroleinfo.GraphicSmall;
            RoleLargeIcon.Value = currentroleinfo.GraphicLarge;
            RoleFreeAgent.Checked = currentroleinfo.FreeAgentRole;
            RoleTeamOnly.Checked = currentroleinfo.RoleIsTeamBased;
            RoleHasBonus.Checked = currentroleinfo.RoleActive; 

            isInitializing = false;
        }
        
        public void InitRolesDisplay()
        {
            if (RoleInfoView != null)
                RoleInfoView.Dispose();

            RoleInfoView = new DataGridView();
            RoleInfoView.BackgroundColor = Color.Silver;
            RoleInfoView.Bounds = new Rectangle(new Point(2, 2), new Size(658, 378));
            RoleInfoView.MultiSelect = false;
            RoleInfoView.AutoGenerateColumns = false;
            RoleInfoView.AllowUserToAddRows = false;
            RoleInfoView.RowHeadersVisible = false;
            RoleInfoView.ColumnCount = 10;

            RoleInfoView.Columns[0].Name = "ID";
            RoleInfoView.Columns[0].Width = 24;
            RoleInfoView.Columns[1].Name = "Name";
            RoleInfoView.Columns[1].Width = 120;
            //RoleInfoView.Columns[2].Name = "Desc";
            //RoleInfoView.Columns[2].Width = 525;
            RoleInfoView.Columns[2].Name = "FA";
            RoleInfoView.Columns[2].Width = 30;
            RoleInfoView.Columns[2].ToolTipText = "Usable with Free Agents";
            RoleInfoView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[3].Name = "IC";
            RoleInfoView.Columns[3].Width = 30;
            RoleInfoView.Columns[3].ToolTipText = "Small Graphic ID #";
            RoleInfoView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[4].Name = "IG";
            RoleInfoView.Columns[4].Width = 30;
            RoleInfoView.Columns[4].ToolTipText = "Large Graphic ID #";
            RoleInfoView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[5].Name = "RK";
            RoleInfoView.Columns[5].Width = 30;
            RoleInfoView.Columns[5].ToolTipText = "Rank";
            RoleInfoView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[6].Name = "VAL";
            RoleInfoView.Columns[6].Width = 40;
            RoleInfoView.Columns[6].ToolTipText = "Player Value Modifier";
            RoleInfoView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[7].Name = "TM";
            RoleInfoView.Columns[7].Width = 30;
            RoleInfoView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[7].ToolTipText = "Selected By Team";
            RoleInfoView.Columns[8].Name = "TY";
            RoleInfoView.Columns[8].Width = 30;
            RoleInfoView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RoleInfoView.Columns[8].ToolTipText = "Role Effects Type";
            RoleInfoView.Columns[9].Name = "RA";
            RoleInfoView.Columns[9].Width = 30;
            RoleInfoView.Columns[9].ToolTipText = "Role Has Effects";
            RoleInfoView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int total = manager.stream_model.TableModels[EditorModel.ROLES_INFO].RecordCount;

            foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
            {
                RoleInfo ri = (RoleInfo)rec;
                string fa = "";
                if (ri.FreeAgentRole == true)
                    fa = "Y";
                else fa = "N";
                string ef = "";
                if (ri.RoleActive == true)
                    ef = "Y";
                else ef = "N";
                string ts = "";
                if (ri.RoleIsTeamBased == true)
                    ts = "Y";
                else ts = "N";

                object[] entry = { ri.PlayerRole, ri.RoleName, fa, ri.GraphicSmall, ri.GraphicLarge, ri.RoleRank, ri.ValueEffect, ts, ri.RoleEffectType, ef };
                RoleInfoView.Rows.Add(entry);
            }

            RoleInfoView.Sort(RoleInfoView.Columns[0], ListSortDirection.Ascending);
            RoleInfoView.CellClick += RoleView_CellClick;
            RolesTab.Controls.Add(RoleInfoView);
            RoleInfoView.Rows[currentrolerow].Selected = true;            
            InitRoleEditingPanel();
        }

        #region Events etc

        private void RoleView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            currentrolerow = e.RowIndex;
            DataGridViewRow row = RoleInfoView.Rows[currentrolerow];
            int r = (int)row.Cells[0].Value;
            RoleInfoView.Rows[currentrolerow].Selected = false;
            currentrolerow = r;
            RoleInfoView.Rows[currentrolerow].Selected = true;
            InitRoleEditingPanel();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (currentrolerow != -1)
                RoleInfoView.Rows[currentrolerow].Selected = false;

            if (currentrolerow == RoleInfoView.Rows.Count - 1)
            {
                currentrolerow = 0;
            }
            else currentrolerow++;
            RoleInfoView.Rows[currentrolerow].Selected = true;
            RoleInfoView.FirstDisplayedScrollingRowIndex = currentrolerow;
            InitRoleEditingPanel();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (currentrolerow != -1)
                RoleInfoView.Rows[currentrolerow].Selected = false;

            if (currentrolerow <= 0)
            {
                currentrolerow = RoleInfoView.Rows.Count - 1;
            }
            else currentrolerow--;
            RoleInfoView.Rows[currentrolerow].Selected = true;
            RoleInfoView.FirstDisplayedScrollingRowIndex = currentrolerow;

            InitRoleEditingPanel();
        }

        private void RoleValue_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.ValueEffect = (int)RoleValue.Value;
                InitRolesDisplay();
            }
        }

        private void RoleEffectType_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.RoleEffectType = (int)RoleEffectType.Value;
                InitRolesDisplay();
            }
        }

        private void RoleRank_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.RoleRank = (int)RoleRank.Value;
                InitRolesDisplay();
            }
        }

        private void RoleSmallIcon_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.GraphicSmall = (int)RoleSmallIcon.Value;
                InitRolesDisplay();
            }
        }

        private void RoleLargeIcon_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.GraphicLarge = (int)RoleLargeIcon.Value;
                InitRolesDisplay();
            }
        }

        private void RoleFreeAgent_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.FreeAgentRole = RoleFreeAgent.Checked;
                InitRolesDisplay();
            }
        }

        private void RoleTeamOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.RoleIsTeamBased = RoleTeamOnly.Checked;
                InitRolesDisplay();
            }
        }

        private void RoleHasBonus_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentroleinfo.RoleActive = RoleHasBonus.Checked;
                InitRolesDisplay();
            }
        }

        private void RoleName_Leave(object sender, EventArgs e)
        {

        }

        private void RoleDescription_TextChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (currentroleinfo.RoleText != RoleDescription.Text)
                {
                    currentroleinfo.RoleText = RoleDescription.Text;
                    InitRolesDisplay();
                }
            }
        }
        
        private void RoleAddNew_Button_Click(object sender, EventArgs e)
        {
            TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_INFO].CreateNewRecord(true);
            RoleInfo ri = (RoleInfo)rec;
            ri.PlayerRole = RoleInfoView.RowCount;
            ri.RoleActive = false;
            ri.FreeAgentRole = true;
            ri.RoleIsTeamBased = false;
            ri.GraphicSmall = 35;
            ri.GraphicLarge = 74;

            RoleInfoView.Rows[currentrolerow].Selected = false;
            currentrolerow = ri.PlayerRole;
            InitRolesDisplay();
            InitRoleEditingPanel();
            RoleInfoView.Rows[currentrolerow].Selected = true;
            RoleInfoView.FirstDisplayedScrollingRowIndex = currentrolerow;
        }
        
        
        #endregion

        #endregion
        
        #region Role Player Effects        
        
        public void InitPlayerEffectDisplay()
        {
            if (PlayerEffectView != null)
                PlayerEffectView.Dispose();

            PlayerEffectView = new DataGridView();
            PlayerEffectView.Bounds = new Rectangle(new Point(2, 2), new Size(658, 378));
            PlayerEffectView.BackgroundColor = Color.Silver;
            PlayerEffectView.MultiSelect = false;
            PlayerEffectView.AutoGenerateColumns = false;
            PlayerEffectView.AllowUserToAddRows = false;
            PlayerEffectView.RowHeadersVisible = false;
            PlayerEffectView.ColumnCount = 5;

            PlayerEffectView.Columns[0].Name = "Rec";
            PlayerEffectView.Columns[0].Width = 50;
            PlayerEffectView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerEffectView.Columns[1].Name = "Role";
            PlayerEffectView.Columns[1].Width = 120;
            PlayerEffectView.Columns[2].Name = "Field";
            PlayerEffectView.Columns[2].Width = 50;
            PlayerEffectView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerEffectView.Columns[3].Name = "MOD";
            PlayerEffectView.Columns[3].Width = 50;
            PlayerEffectView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            PlayerEffectView.Columns[4].Name = "POS";
            PlayerEffectView.Columns[4].Width = 50;
            PlayerEffectView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int count = -1;
            foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_PLAYER_EFFECTS].GetRecords())
            {
                RolePlayerEffects rpe = (RolePlayerEffects)rec;
                string role = "";
                count++;

                foreach (TableRecordModel record in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
                {
                    RoleInfo ri = (RoleInfo)record;
                    if (ri.PlayerRole == rpe.PlayerRole)
                    {
                        role = ri.RoleName;
                        break;
                    }
                }
                string pp = "ALL";
                if (rpe.PlayerPosition <= 20)
                    pp = Enum.GetName(typeof(MaddenPositions), rpe.PlayerPosition);

                object[] entry = { count, role, rpe.Player_Fieldname, rpe.Modifier, pp };

                PlayerEffectView.Rows.Add(entry);
            }

            PlayerEffectView.Sort(PlayerEffectView.Columns[0], ListSortDirection.Ascending);
            PlayerEffectView.CellClick += PlayerEffectView_CellClick;
            PlayerEffect.Controls.Add(PlayerEffectView);
            InitPlayerEffectEdit();
        }

        public void InitPlayerEffectEdit()
        {
            isInitializing = true;

            TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_PLAYER_EFFECTS].GetRecord(currentplayereffectrow);
            currentplayereffect = (RolePlayerEffects)rec;

            PlayerEffectRecord.Value = currentplayereffectrow;
            PlayerEffect_Role.SelectedIndex = currentplayereffect.PlayerRole;
            PlayerEffect_Modifier.Value = currentplayereffect.Modifier;
            if (currentplayereffect.PlayerPosition == 31)
                PlayerEffect_Position.SelectedIndex = 21;
            else PlayerEffect_Position.SelectedIndex = currentplayereffect.PlayerPosition;
            PlayerEffect_Rating.Text = currentplayereffect.Player_Fieldname;
            PlayerEffectView.Rows[currentplayereffectrow].Selected = true;

            isInitializing = false;
        }

        private void PlayerEffectView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            currentplayereffectrow = e.RowIndex;
            DataGridViewRow row = PlayerEffectView.Rows[currentplayereffectrow];
            int r = (int)row.Cells[0].Value;
            PlayerEffectView.Rows[currentplayereffectrow].Selected = false;            
            currentplayereffectrow = r;
            InitPlayerEffectEdit();
            
        }

        #region Buttons etc

        private void PlayerEffectAddNew_Click(object sender, EventArgs e)
        {
            TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_PLAYER_EFFECTS].CreateNewRecord(true);
            RolePlayerEffects rpe = (RolePlayerEffects)rec;
            
            PlayerEffectView.Rows[currentplayereffectrow].Selected = false;
            currentplayereffectrow = rpe.RecNo;
            InitPlayerEffectDisplay();
            InitPlayerEffectEdit();
            PlayerEffectView.Rows[currentplayereffectrow].Selected = true;
            PlayerEffectView.FirstDisplayedScrollingRowIndex = currentplayereffectrow;
        }
        
        private void PlayerRoleRight_Button_Click(object sender, EventArgs e)
        {
            if (currentplayereffectrow != -1)

                PlayerEffectView.Rows[currentplayereffectrow].Selected = false;

            if (currentplayereffectrow == PlayerEffectView.Rows.Count - 1)
            {
                currentplayereffectrow = 0;
            }
            else currentplayereffectrow++;

            PlayerEffectView.FirstDisplayedScrollingRowIndex = currentplayereffectrow;
            InitPlayerEffectEdit();            
        }

        private void PlayerRoleLeft_Button_Click(object sender, EventArgs e)
        {
            if (currentplayereffectrow != -1)

                PlayerEffectView.Rows[currentplayereffectrow].Selected = false;

            if (currentplayereffectrow <= 0)
            {
                currentplayereffectrow = PlayerEffectView.RowCount - 1;
            }
            else currentplayereffectrow--;

            PlayerEffectView.FirstDisplayedScrollingRowIndex = currentplayereffectrow;
            InitPlayerEffectEdit();
        }

        private void PlayerEffectRecord_ValueChanged(object sender, EventArgs e)
        {
            // void
        }

        private void PlayerEffect_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentplayereffect.PlayerRole = PlayerEffect_Role.SelectedIndex;
                InitPlayerEffectDisplay();
            }
        }

        private void PlayerEffect_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (PlayerEffect_Position.SelectedIndex == 21)
                    currentplayereffect.PlayerPosition = 31;
                else currentplayereffect.PlayerPosition = PlayerEffect_Position.SelectedIndex;
                InitPlayerEffectDisplay();
            }
        }

        private void PlayerEffect_Rating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                string result = "P" + ((PlayerRating)PlayerEffect_Rating.SelectedIndex).ToString();
                currentplayereffect.Player_Fieldname = result;
                InitPlayerEffectDisplay();
            }
        }

        private void PlayerEffect_Modifier_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {                
                currentplayereffect.Modifier = (int)PlayerEffect_Modifier.Value;
                InitPlayerEffectDisplay();
            }
        }

        #endregion

        #endregion

        #region Role Team Effects

        public void InitTeamEffectDisplay()
        {
            if (TeamEffectView != null)
                TeamEffectView.Dispose();

            TeamEffectView = new DataGridView();
            TeamEffectView.Bounds = new Rectangle(new Point(2, 2), new Size(658, 378));
            TeamEffectView.MultiSelect = false;
            TeamEffectView.AutoGenerateColumns = false;
            TeamEffectView.AllowUserToAddRows = false;
            TeamEffectView.RowHeadersVisible = false;
            TeamEffectView.ColumnCount = 8;

            TeamEffectView.Columns[0].Name = "Rec";
            TeamEffectView.Columns[0].Width = 40;
            TeamEffectView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamEffectView.Columns[1].Name = "IOFF";
            TeamEffectView.Columns[1].Width = 40;
            TeamEffectView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamEffectView.Columns[1].ToolTipText = "Unknown";
            TeamEffectView.Columns[2].Name = "Role";
            TeamEffectView.Columns[2].Width = 120;
            TeamEffectView.Columns[3].Name = "POS";
            TeamEffectView.Columns[3].Width = 50;
            TeamEffectView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamEffectView.Columns[4].Name = "Depth";
            TeamEffectView.Columns[4].Width = 50;
            TeamEffectView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamEffectView.Columns[5].Name = "Phase";
            TeamEffectView.Columns[5].Width = 60;
            TeamEffectView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TeamEffectView.Columns[5].ToolTipText = "Player's Current Career Phase";
            TeamEffectView.Columns[6].Name = "Field";
            TeamEffectView.Columns[6].Width = 50;
            TeamEffectView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            TeamEffectView.Columns[7].Name = "MOD";
            TeamEffectView.Columns[7].Width = 40;
            TeamEffectView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            int count = -1;
            for (count = 0; count < manager.stream_model.TableModels[EditorModel.ROLES_TEAM_EFFECTS].RecordCount; count++)
            {
                TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_TEAM_EFFECTS].GetRecord(count);
                RoleTeamEffects eff = (RoleTeamEffects)rec;
                string role = "";
                string pp = "ALL";
                foreach (TableRecordModel record in manager.stream_model.TableModels[EditorModel.ROLES_INFO].GetRecords())
                {
                    RoleInfo ri = (RoleInfo)record;
                    if (ri.PlayerRole == eff.PlayerRole)
                    {
                        role = ri.RoleName;
                        break;
                    }
                }

                if (eff.PlayerPosition < 21)
                    pp = Enum.GetName(typeof(MaddenPositions), eff.PlayerPosition);

                object[] entry = { count, eff.Ioff, role, pp, eff.DepthOrder, Enum.GetName(typeof(PlayerPhase), eff.PlayerCurrentPH), eff.Player_Fieldname, eff.Modifier };
                TeamEffectView.Rows.Add(entry);
            }         

            TeamEffectView.Sort(TeamEffectView.Columns[0], ListSortDirection.Ascending);
            TeamEffectView.CellClick += TeamEffectView_CellClick;
            InitTeamEffectEdit();
            TeamEffects.Controls.Add(TeamEffectView);
        }

        public void InitTeamEffectEdit()
        {
            isInitializing = true;

            TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_TEAM_EFFECTS].GetRecord(currentteameffectrow);
            currentteameffect = (RoleTeamEffects)rec;

            TeamEffect_Rec.Value = currentteameffectrow;
            TeamEffect_Role.SelectedIndex = currentteameffect.PlayerRole;
            TeamEffect_Mod.Value = currentteameffect.Modifier;
            if (currentteameffect.PlayerPosition == 31)
                TeamEffect_Pos.SelectedIndex = 21;
            else TeamEffect_Pos.SelectedIndex = currentteameffect.PlayerPosition;
            TeamEffect_Rating.Text = currentteameffect.Player_Fieldname;
            TeamEffectView.Rows[currentteameffectrow].Selected = true;

            isInitializing = false;
        }

        private void TeamEffectView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            currentteameffectrow = e.RowIndex;
            DataGridViewRow row = TeamEffectView.Rows[currentteameffectrow];
            int r = (int)row.Cells[0].Value;
            TeamEffectView.Rows[currentteameffectrow].Selected = false;
            currentteameffectrow = r;
            InitTeamEffectEdit();
        }
                
        #region Buttons etc
        
        private void TeamEffectCreate_Button_Click(object sender, EventArgs e)
        {
            TableRecordModel rec = manager.stream_model.TableModels[EditorModel.ROLES_TEAM_EFFECTS].CreateNewRecord(true);
            RoleTeamEffects rte = (RoleTeamEffects)rec;

            TeamEffectView.Rows[currentteameffectrow].Selected = false;
            currentteameffectrow = rte.RecNo;
            InitTeamEffectDisplay();
            InitTeamEffectEdit();
            TeamEffectView.Rows[currentteameffectrow].Selected = true;
            TeamEffectView.FirstDisplayedScrollingRowIndex = currentteameffectrow;
        }

        private void TeamEffectRight_Button_Click(object sender, EventArgs e)
        {
            if (currentteameffectrow != -1)

                TeamEffectView.Rows[currentteameffectrow].Selected = false;

            if (currentteameffectrow == TeamEffectView.Rows.Count - 1)
            {
                currentteameffectrow = 0;
            }
            else currentteameffectrow++;

            TeamEffectView.FirstDisplayedScrollingRowIndex = currentteameffectrow;
            InitTeamEffectEdit();            
        }
        
        private void TeamEffectLeft_Button_Click(object sender, EventArgs e)
        {
            if (currentteameffectrow != -1)

                TeamEffectView.Rows[currentteameffectrow].Selected = false;

            if (currentteameffectrow <= 0)
            {
                currentteameffectrow = TeamEffectView.RowCount - 1;
            }
            else currentteameffectrow--;

            TeamEffectView.FirstDisplayedScrollingRowIndex = currentteameffectrow;
            InitTeamEffectEdit();
        }
                
        private void TeamEffect_Rec_ValueChanged(object sender, EventArgs e)
        {
            //void
        }

        private void TeamEffect_Role_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentteameffect.PlayerRole = TeamEffect_Role.SelectedIndex;
                InitTeamEffectDisplay();
            }
        }

        private void TeamEffect_Pos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (TeamEffect_Pos.SelectedIndex == 21)
                    currentteameffect.PlayerPosition = 31;
                else currentteameffect.PlayerPosition = TeamEffect_Pos.SelectedIndex;
                InitTeamEffectDisplay();
            }
        }

        private void TeamEffect_Rating_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                string result = "P" + ((PlayerRating)TeamEffect_Rating.SelectedIndex).ToString();
                currentteameffect.Player_Fieldname = result;
                InitTeamEffectDisplay();
            }
        }

        private void TeamEffect_Mod_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentteameffect.Modifier = (int)TeamEffect_Mod.Value;
                InitTeamEffectDisplay();
            }
        }

        



        #endregion
        #endregion

        #region Colleges

        public void InitCollegesDisplay()
        {
            if (CollegeView != null)
                CollegeView.Dispose();

            CollegeView = new DataGridView();
            CollegeView.BackgroundColor = Color.Silver;
            CollegeView.Bounds = new Rectangle(new Point(2, 2), new Size(900, 378));
            CollegeView.MultiSelect = false;

            if (manager.stream_model.FileVersion >= MaddenFileVersion.Ver2006)
                CollegeView.ColumnCount = 4;
            else CollegeView.ColumnCount = 3;
            CollegeView.Columns[0].Name = "ID";
            CollegeView.Columns[0].Width = 40;
            CollegeView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CollegeView.Columns[1].Name = "Team";
            CollegeView.Columns[1].Width = 40;
            CollegeView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CollegeView.Columns[2].Name = "College Name";
            CollegeView.Columns[2].Width = 160;

            int total = manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].RecordCount;

            for (int c = 0; c < total; c++)
            {
                CollegesRecord record = (CollegesRecord)manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].GetRecord(c);

                object[] entry = { record.CollegeId, record.CollegeTeamId, record.CollegeName };

                CollegeView.Rows.Add(entry);
            }

            CollegeView.Sort(CollegeView.Columns[0], ListSortDirection.Ascending);
            CollegeView.CellClick += CollegeView_CellClick;
            //this.Controls.Add(CollegeView);
            Colleges.Controls.Add(CollegeView);
            InitCollegeEditing(currentcollegerow);
        }

        private void CollegeView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            currentcollegerow = e.RowIndex;
            DataGridViewRow row = CollegeView.Rows[currentcollegerow];
            int r = (int)row.Cells[0].Value;
            InitCollegeEditing(r);
        }

        public void InitCollegeEditing(int id)
        {
            isInitializing = true;
            foreach (CollegesRecord col in manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].GetRecords())
            {
                CollegesRecord college = (CollegesRecord)col;
                if (college.CollegeId == id)
                {
                    CollegeID.Value = college.CollegeId;
                    MasterID.Value = college.CollegeTeamId;
                    CollegeName.Text = college.CollegeName;
                }
            }
            isInitializing = false;
        }

        public bool AddNewCollege()
        {
            if (CollegeName.Text == "")
                return false;

            foreach (CollegesRecord rec in manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].GetRecords())
            {
                if (rec.CollegeName == CollegeName.Text)
                {
                    MessageBox.Show("Please use a unique college name", "College Already Exists !", MessageBoxButtons.OK);
                    return false;
                }
            }

            int number = manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].RecordCount;
            manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].CreateNewRecord(true);

            CollegesRecord record = (CollegesRecord)manager.stream_model.TableModels[EditorModel.COLLEGES_TABLE].GetRecord(number);

            record.CollegeId = number;
            record.CollegeTeamId = 0;
            record.CollegeName = CollegeName.Text;

            if (manager.stream_model.FileVersion >= MaddenFileVersion.Ver2006)
            {


            }
            if (manager.stream_model.FileVersion >= MaddenFileVersion.Ver2007)
            {

            }

            return true;
        }

        #endregion

        #region Needs work

        public void InitDefineRolesDisplay()
        {
            if (DefineRolesView != null)
                DefineRolesView.Dispose();

            DefineRolesView = new DataGridView();
            DefineRolesView.Bounds = new Rectangle(new Point(5, 85), new Size(1000, 350));
            DefineRolesView.MultiSelect = false;
            DefineRolesView.AutoGenerateColumns = false;
            DefineRolesView.ColumnCount = 4;

            DefineRolesView.Columns[0].Name = "POS";
            DefineRolesView.Columns[0].Width = 40;
            DefineRolesView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefineRolesView.Columns[1].Name = "Field";
            DefineRolesView.Columns[1].Width = 50;
            DefineRolesView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefineRolesView.Columns[2].Name = "Total";
            DefineRolesView.Columns[2].Width = 60;
            DefineRolesView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefineRolesView.Columns[3].Name = "Role";
            DefineRolesView.Columns[3].Width = 50;
            DefineRolesView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.ROLES_DEFINE].GetRecords())
            {
                PRDF def = (PRDF)rec;

                object[] entry = { Enum.GetName(typeof(MaddenPositions), def.PositionId), def.Career_Fieldname, def.SeasonValue, def.Role };
                DefineRolesView.Rows.Add(entry);
            }

            this.Controls.Add(DefineRolesView);
        }

        public void InitStatsReqDisplay()
        {
            //currentrow = 0;
            if (StatsReqView != null)
                StatsReqView.Dispose();

            StatsReqView = new DataGridView();
            StatsReqView.Bounds = new Rectangle(new Point(2, 2), new Size(900, 378));
            StatsReqView.MultiSelect = false;
            StatsReqView.AutoGenerateColumns = false;
            StatsReqView.ColumnCount = 7;

            StatsReqView.Columns[0].Name = "Role";
            StatsReqView.Columns[0].Width = 50;
            StatsReqView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StatsReqView.Columns[1].Name = "Pos1";
            StatsReqView.Columns[1].Width = 50;
            StatsReqView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StatsReqView.Columns[2].Name = "Pos2";
            StatsReqView.Columns[2].Width = 50;
            StatsReqView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StatsReqView.Columns[3].Name = "Table";
            StatsReqView.Columns[3].Width = 50;
            StatsReqView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StatsReqView.Columns[4].Name = "Field";
            StatsReqView.Columns[4].Width = 50;
            StatsReqView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StatsReqView.Columns[5].Name = "Min";
            StatsReqView.Columns[5].Width = 60;
            StatsReqView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            StatsReqView.Columns[6].Name = "Max";
            StatsReqView.Columns[6].Width = 60;
            StatsReqView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            foreach (TableRecordModel rec in manager.stream_model.TableModels[EditorModel.STATS_REQUIRED].GetRecords())
            {
                SuperStarStatsRequired sr = (SuperStarStatsRequired)rec;

                object[] entry = { sr.RoleID, sr.PosMin, sr.PosMax, sr.Tablename, sr.Fieldname, sr.ReqMin, sr.ReqMax };

                StatsReqView.Rows.Add(entry);
            }

            this.Controls.Add(StatsReqView);
        }

        #endregion

        #region DAT options

        public void InitDatOptionsUI()
        {
            CurrentPlayerPort_Textbox.Text = manager.config.PlayerPortFiles[(int)model.FileVersion];
            CurrentCoachPort_Textbox.Text = manager.config.CoachPortFiles[(int)model.FileVersion];
            AutoLoadPlayerPorts.Checked = manager.config.AutoLoad_PlayerPort[(int)model.FileVersion];
            AutoLoadCoachPorts.Checked = manager.config.AutoLoad_CoachPort[(int)model.FileVersion];
            AskForPlayerSave_Checkbox.Checked = manager.config.AskPlayerSave[(int)model.FileVersion];
            AskForCoachSave_Checkbox.Checked = manager.config.AskCoachSave[(int)model.FileVersion];

            if (manager.PlayerPortDAT.isterf)
            {
                SavePlayerDAT_Button.Enabled = true;
                PlayerPack_Panel.Visible = true;
            }
            else
            {
                SavePlayerDAT_Button.Enabled = false;
                PlayerPack_Panel.Visible = false;
            }
            if (manager.CoachPortDAT.isterf)
            {
                SaveCoachDat_Button.Enabled = true;
                CoachPack_Panel.Visible = true;
            }
            else
            {
                SaveCoachDat_Button.Enabled = false;
                CoachPack_Panel.Visible = false;
            }
        }

        public void Faq()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.Portfaq.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }
        public void PortPackFaq()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.PackFaq.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }
        public void Defaultdat()
        {
            PortFax_Box.Clear();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string filename = "MaddenEditor.Resources.DefaultDat.txt";
            using (Stream stream = assembly.GetManifestResourceStream(filename))
            using (StreamReader reader = new StreamReader(stream))
            {
                PortFax_Box.Text = reader.ReadToEnd();
            }
        }
        
        private void DATFaq_Button_Click(object sender, EventArgs e)
        {
            Faq();
        }
        private void PortPackFaq_Button_Click(object sender, EventArgs e)
        {
            PortPackFaq();
        }
        private void PortDefaultDAT_Button_Click(object sender, EventArgs e)
        {
            Defaultdat();
        }
        private void LoadPlayerDAT_Button_Click(object sender, EventArgs e)
        {
            manager.PlayerPortDAT.LoadFileName();
            manager.PlayerPortDAT.Load();
            if (manager.PlayerPortDAT.isterf)
            {
                manager.config.PlayerPortFiles[(int)model.FileVersion] = manager.PlayerPortDAT.loadfile;
                InitDatOptionsUI();
                manager.config.changed = true;
            }
        }
        private void LoadCoachDAT_Button_Click(object sender, EventArgs e)
        {
            manager.CoachPortDAT.LoadFileName();
            manager.CoachPortDAT.Load();
            if (manager.CoachPortDAT.isterf)
            {
                manager.config.CoachPortFiles[(int)model.FileVersion] = manager.CoachPortDAT.loadfile;
                InitDatOptionsUI();
                manager.config.changed = true;
            }
        }
        private void AutoLoadPlayerPorts_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.AutoLoad_PlayerPort[(int)model.FileVersion] = AutoLoadPlayerPorts.Checked;
                manager.config.changed = true;
            }
        }
        private void AutoLoadCoachPorts_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.AutoLoad_CoachPort[(int)model.FileVersion] = AutoLoadCoachPorts.Checked;
                manager.config.changed = true;
            }
        }
        private void AskForPlayerSave_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.AskPlayerSave[(int)model.FileVersion] = AskForPlayerSave_Checkbox.Checked;
                manager.config.changed = true;
            }
        }
        private void AskForCoachSave_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                manager.config.AskCoachSave[(int)model.FileVersion] = AskForCoachSave_Checkbox.Checked;
                manager.config.changed = true;
            }
        }
        private void ExportPlayerID_Checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ExportPlayerPortPack_Button_Click(object sender, EventArgs e)
        {
            int datsize = manager.PlayerPortDAT.ParentTerf.Data.GetSize(manager.PlayerPortDAT.ParentTerf);

            if (!isInitializing)
            {
                string dirname = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Choose Player Portraits Save Folder";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    dirname = dialog.SelectedPath;
                if (dirname == "")
                    return;

                DATComment.Text = "Exporting Player Portraits, this will take some time.";
                DATProgress.Visible = true;
                DATProgress.Minimum = 1;
                DATProgress.Maximum = model.TableModels[EditorModel.PLAYER_TABLE].RecordCount;
                DATProgress.Value = 1;
                DATProgress.Step = 1;

                foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    if (rec.FirstName == "New")
                        continue;
                    string filename = dirname + "\\" + rec.FirstName + rec.LastName;
                    if (ExportPlayerID_Checkbox.Checked)
                        filename += "_" + rec.PortraitId.ToString("00000") + "." + rec.PlayerId.ToString("00000") + ".BMP";
                    else filename += "_" + rec.NFLID.ToString("00000") + ".BMP";

                    Image image = manager.PlayerPortDAT.ParentTerf.Data.DataFiles[rec.PortraitId + 1].mmap_data.GetPortraitDisplay();

                    image.Save(filename, ImageFormat.Bmp);
                    DATProgress.PerformStep();
                }                

                DATComment.Text = "Done Exporting Player Portraits.";
                DATProgress.Visible = false;
            }
        }
        private void ImportPlayerPortPack_Button_Click(object sender, EventArgs e)
        {
            if (!manager.PlayerPortDAT.isterf)
            {
                MessageBox.Show("No Player Portrait DAT loaded", "Invalid DAT", MessageBoxButtons.OK);
                return;
            }
            string dirname = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Choose Player Portraits Location";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                dirname = dialog.SelectedPath;

            if (dirname == "")
                return;

            List<string> playernames = new List<string>();
            List<string> lastnames = new List<string>();
            List<int> playerids = new List<int>();
            List<int> Portraitids = new List<int>();

            string[] files = Directory.GetFiles(dirname);
            List<string> filenames = new List<string>();
            Dictionary<int, string> name_errors = new Dictionary<int, string>();

            for (int c = 0; c < files.Count(); c++)
                filenames.Add(files[c].Remove(0, dirname.Count() + 1));

            char[] underscore = { '_' };
            char[] period = { '.' };

            DATComment.Text = "Importing Portrait Pack, this will take some time.";
            DATProgress.Visible = true;
            DATProgress.Minimum = 1;
            DATProgress.Maximum = files.Count();
            DATProgress.Value = 1;
            DATProgress.Step = 1;            
            
            for (int c = 0; c < filenames.Count; c++)
            {
                try
                {
                    string[] origwords = filenames[c].Split(underscore);

                    playernames.Add(origwords[0]);

                    string[] subwords = origwords[origwords.Count() - 1].Split(period);

                    if (subwords.Count() == 3)
                    {
                        int portid = Convert.ToInt32(subwords[0]);
                        int playid = Convert.ToInt32(subwords[1]);
                        Portraitids.Add(portid);
                        playerids.Add(playid);
                    }
                    else
                    {
                        Portraitids.Add(Convert.ToInt32(subwords[0]));
                    }
                }
                catch (Exception)
                {
                    name_errors.Add(c, "Invalid Filename");
                    continue;
                }
            }

            int total = 0;
            foreach (int high in Portraitids)
                if (high > total)
                    total = high;
            
            manager.PlayerPortDAT.ParentTerf.Expand(total);
            

            for (int c = 0; c < filenames.Count; c++)
            {
                if (name_errors.ContainsKey(c))
                    continue;
                else
                {
                    manager.PlayerPortDAT.grfx = new CustomBitmap(dirname + "\\" + filenames[c], Color.FromArgb(255, 255, 255, 255));
                    manager.PlayerPortDAT.ParentTerf.Data.DataFiles[Portraitids[c] + 1].mmap_data.ImportGraphic(manager.PlayerPortDAT.grfx.fixed_dds);
                }

                DATProgress.PerformStep();
            }

            if (playerids.Count > 0)
            {
                for (int pos = 0; pos < playerids.Count; pos++)
                {
                    foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        if (rec.NFLID == playerids[pos])
                            if (rec.PortraitId != Portraitids[pos])
                                rec.PortraitId = Portraitids[pos];
                    }
                }
            }

            manager.PlayerPortDAT.changed = true;
            DATComment.Text = "Finished Importing Portrait Pack";
            DATProgress.Visible = false;
        }
        private void ExportCoachID_Checkbox_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void ExportCoachPortPack_Button_Click(object sender, EventArgs e)
        {
            int datsize = manager.CoachPortDAT.ParentTerf.Data.GetSize(manager.CoachPortDAT.ParentTerf);

            if (!isInitializing)
            {
                string dirname = "";
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Choose Coach Portraits Save Folder";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                    dirname = dialog.SelectedPath;
                if (dirname == "")
                    return;
                DATComment.Text = "Exporting Coach Portraits, this will take some time.";
                DATProgress.Visible = true;
                DATProgress.Minimum = 1;
                DATProgress.Maximum = model.TableModels[EditorModel.COACH_TABLE].RecordCount;
                if (ExportStreamedCoaches_Checkbox.Checked)
                {
                    if (manager.CoachPortDAT != null)
                        DATProgress.Maximum += manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].RecordCount;
                }

                DATProgress.Value = 1;
                DATProgress.Step = 1;

                foreach (CoachRecord rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    string filename = dirname + "\\" + rec.Name;
                    if (ExportCoachID_Checkbox.Checked)
                        filename += "_" + rec.Coachpic.ToString("00000") + "." + rec.CoachId.ToString("00000") + ".BMP";
                    else filename += "_" + rec.Coachpic.ToString("00000") + ".BMP";

                    Image image = manager.CoachPortDAT.ParentTerf.Data.DataFiles[rec.Coachpic + 1].mmap_data.GetPortraitDisplay();

                    image.Save(filename, ImageFormat.Bmp); 
                    DATProgress.PerformStep();
                }

                if (ExportStreamedCoaches_Checkbox.Checked && manager.CoachPortDAT != null)
                {
                    foreach (CoachCollection rec in manager.stream_model.TableModels[EditorModel.COACH_COLLECTIONS_TABLE].GetRecords())
                    {
                        string filename = dirname + "\\" + rec.Name;
                        if (ExportCoachID_Checkbox.Checked)
                            filename += "_" + rec.Coachpic.ToString("00000") + "." + rec.CoachId.ToString("00000") + ".BMP";
                        else filename += "_" + rec.Coachpic.ToString("00000") + ".BMP";

                        Image image = manager.CoachPortDAT.ParentTerf.Data.DataFiles[rec.Coachpic + 1].mmap_data.GetPortraitDisplay();

                        image.Save(filename, ImageFormat.Bmp);
                        DATProgress.PerformStep();
                    }
                }

                DATComment.Text = "Done Exporting Coach Portraits.";
                DATProgress.Visible = false;
            }
        }
        private void ImportCoachPortPack_Button_Click(object sender, EventArgs e)
        {
            if (!manager.CoachPortDAT.isterf)
            {
                MessageBox.Show("No DAT loaded", "Invalid DAT", MessageBoxButtons.OK);
                return;
            }
            string dirname = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Choose Coach Portraits Location";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                dirname = dialog.SelectedPath;

            if (dirname == "")
                return;

            List<string> coachnames = new List<string>();
            List<int> coachids = new List<int>();
            List<int> Portraitids = new List<int>();

            string[] files = Directory.GetFiles(dirname);
            List<string> filenames = new List<string>();
            Dictionary<int, string> name_errors = new Dictionary<int, string>();

            for (int c = 0; c < files.Count(); c++)
                filenames.Add(files[c].Remove(0, dirname.Count() + 1));

            char[] underscore = { '_' };
            char[] period = { '.' };

            DATComment.Text = "Importing Portrait Pack, this will take some time.";
            DATProgress.Visible = true;
            DATProgress.Minimum = 1;
            DATProgress.Maximum = files.Count();
            DATProgress.Value = 1;
            DATProgress.Step = 1;

            for (int c = 0; c < filenames.Count; c++)
            {
                try
                {
                    string[] origwords = filenames[c].Split(underscore);
                    coachnames.Add(origwords[0]);

                    string[] subwords = origwords[origwords.Count() - 1].Split(period);

                    if (subwords.Count() == 3)
                    {
                        int portid = Convert.ToInt32(subwords[0]);
                        int playid = Convert.ToInt32(subwords[1]);
                        Portraitids.Add(portid);
                        coachids.Add(playid);
                    }
                    else
                    {
                        Portraitids.Add(Convert.ToInt32(subwords[0]));
                    }
                }
                catch (Exception)
                {
                    name_errors.Add(c, "Invalid Filename");
                    continue;
                }
            }

            int total = 0;
            foreach (int high in Portraitids)
                if (high > total)
                    total = high;
            manager.CoachPortDAT.ParentTerf.Expand(total);

            for (int c = 0; c < filenames.Count; c++)
            {
                if (name_errors.ContainsKey(c))
                    continue;
                else
                {
                    manager.CoachPortDAT.grfx = new CustomBitmap(dirname + "\\" + filenames[c], Color.FromArgb(255, 255, 255, 255));
                    manager.CoachPortDAT.ParentTerf.Data.DataFiles[Portraitids[c] + 1].mmap_data.ImportGraphic(manager.CoachPortDAT.grfx.fixed_dds);
                }

                DATProgress.PerformStep();
            }
            if (coachids.Count > 0)
            {
                for (int pos = 0; pos < coachids.Count; pos++)
                {
                    foreach (CoachRecord rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                    {
                        if (rec.Deleted)
                            continue;
                        if (rec.CoachId == coachids[pos])
                            if (rec.Coachpic != Portraitids[pos])
                                rec.Coachpic = Portraitids[pos];
                    }
                }
            }

            manager.CoachPortDAT.changed = true;
            DATComment.Text = "Finished Importing Portrait Pack";
            DATProgress.Visible = false;
        }
                
        #endregion

        #region OVR

        public void InitOVRView()
        {
            if (OVR_View != null)
                OVR_View.Dispose();
            OVR_View = new DataGridView();
            OVR_View.BackgroundColor = Color.Silver;
            OVR_View.Bounds = new Rectangle(new Point(2, 2), new Size(810, 490));
            OVR_View.MultiSelect = false;
            OVR_View.AutoGenerateColumns = false;
            OVR_View.AllowUserToAddRows = false;
            OVR_View.RowHeadersVisible = false;
            OVR_View.ColumnCount = 22;

            OVR_View.Columns[0].Name = "Rec";
            OVR_View.Columns[0].Width = 35;
            OVR_View.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[1].Name = "POS";
            OVR_View.Columns[1].Width = 50;
            OVR_View.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[2].Name = "High";
            OVR_View.Columns[2].Width = 45;
            OVR_View.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[3].Name = "Low";
            OVR_View.Columns[3].Width = 35;            
            OVR_View.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[4].Name = "STR";
            OVR_View.Columns[4].Width = 35;            
            OVR_View.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[5].Name = "AGI";
            OVR_View.Columns[5].Width = 35;            
            OVR_View.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[6].Name = "SPD";
            OVR_View.Columns[6].Width = 35;
            OVR_View.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;           
            OVR_View.Columns[7].Name = "ACC";
            OVR_View.Columns[7].Width = 35;            
            OVR_View.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[8].Name = "AWR";
            OVR_View.Columns[8].Width = 35;
            OVR_View.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            OVR_View.Columns[9].Name = "CTH";
            OVR_View.Columns[9].Width = 35;
            OVR_View.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            OVR_View.Columns[10].Name = "CAR";
            OVR_View.Columns[10].Width = 35;            
            OVR_View.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            OVR_View.Columns[11].Name = "THP";
            OVR_View.Columns[11].Width = 35;
            OVR_View.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[12].Name = "THA";
            OVR_View.Columns[12].Width = 35;
            OVR_View.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[13].Name = "KPW";
            OVR_View.Columns[13].Width = 35;
            OVR_View.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[14].Name = "KAC";
            OVR_View.Columns[14].Width = 35;
            OVR_View.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[15].Name = "BTK";
            OVR_View.Columns[15].Width = 35;
            OVR_View.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[16].Name = "TAK";
            OVR_View.Columns[16].Width = 35;
            OVR_View.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[17].Name = "PBK";
            OVR_View.Columns[17].Width = 35;
            OVR_View.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[18].Name = "RBK";
            OVR_View.Columns[18].Width = 35;
            OVR_View.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[19].Name = "JMP";
            OVR_View.Columns[19].Width = 35;
            OVR_View.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[20].Name = "KRT";
            OVR_View.Columns[20].Width = 35;
            OVR_View.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OVR_View.Columns[21].Name = "INJ";
            OVR_View.Columns[21].Width = 35;
            OVR_View.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (TableRecordModel rec in manager.db_misc_model.TableModels[EditorModel.PLAYER_OVERALL_CALC].GetRecords())
            {
                OverallRecord overall = (OverallRecord)rec;
                string pn = Enum.GetName(typeof(MaddenPositions), overall.Position);

                object[] entry = { overall.RecNo, pn, overall.RatingHigh, overall.RatingLow, overall.Strength, overall.Agility, overall.Speed, overall.Acceleration, overall.Awareness, overall.Catch,
                                 overall.Carry, overall.ThrowPower, overall.ThrowAccuracy, overall.KickPower, overall.KickAccuracy, overall.BreakTackle, overall.Tackle,
                                 overall.PassBlock, overall.RunBlock, overall.Jump, overall.KickReturn, overall.Injury};
                OVR_View.Rows.Add(entry);
            }

            OVR_View.CellClick += OVR_View_CellClick;
            OVR.Controls.Add(OVR_View);
            InitOVREditing(current_ovr_row);
        }

        private void OVR_View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            current_ovr_row = e.RowIndex;
            DataGridViewRow row = OVR_View.Rows[current_ovr_row];
            int r = (int)row.Cells[0].Value;
            InitOVREditing(r);
        }

        public void InitOVREditing(int position)
        {
            isInitializing = true;
            foreach (TableRecordModel rec in manager.db_misc_model.TableModels[EditorModel.PLAYER_OVERALL_CALC].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                OverallRecord o = (OverallRecord)rec;
                if (o.RecNo == position)
                {
                    current_ovr = o;
                    break;
                }
            }

            OVR_Position_Combo.SelectedIndex = position;
            InitOVR_Updown();

            isInitializing = false;
        }

        public void InitOVR_Updown()
        {
            TotalWeight_Updown.Value = current_ovr.GetTotalWeight();
            OVR_High_Needed.Value = (decimal)current_ovr.RatingHigh;
            OVR_Median_Needed.Value = (decimal)(current_ovr.RatingHigh + current_ovr.RatingLow) / 2;
            OVR_Low_Needed.Value = (decimal)current_ovr.RatingLow;

            OVR_Rating0.Value = (decimal)current_ovr.Strength;
            OVR_Rating1.Value = (decimal)current_ovr.Agility;
            OVR_Rating2.Value = (decimal)current_ovr.Speed;
            OVR_Rating3.Value = (decimal)current_ovr.Acceleration;
            OVR_Rating4.Value = (decimal)current_ovr.Awareness;
            OVR_Rating5.Value = (decimal)current_ovr.Catch;
            OVR_Rating6.Value = (decimal)current_ovr.Carry;
            OVR_Rating7.Value = (decimal)current_ovr.ThrowPower;
            OVR_Rating8.Value = (decimal)current_ovr.ThrowAccuracy;
            OVR_Rating9.Value = (decimal)current_ovr.KickPower;
            OVR_Rating10.Value = (decimal)current_ovr.KickAccuracy;
            OVR_Rating11.Value = (decimal)current_ovr.BreakTackle;
            OVR_Rating12.Value = (decimal)current_ovr.Tackle;
            OVR_Rating13.Value = (decimal)current_ovr.PassBlock;
            OVR_Rating14.Value = (decimal)current_ovr.RunBlock;
            OVR_Rating15.Value = (decimal)current_ovr.Jump;
            OVR_Rating16.Value = (decimal)current_ovr.KickReturn;
            OVR_Rating18.Value = (decimal)current_ovr.Injury;
        }
        private void OVR_Rating0_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Strength = (float)OVR_Rating0.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Agility = (float)OVR_Rating1.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Speed = (float)OVR_Rating2.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating3_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Acceleration = (float)OVR_Rating3.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating4_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Awareness = (float)OVR_Rating4.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating5_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Catch = (float)OVR_Rating5.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating6_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Carry = (float)OVR_Rating6.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating7_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.ThrowPower = (float)OVR_Rating7.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating8_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.ThrowAccuracy = (float)OVR_Rating8.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating9_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.KickPower = (float)OVR_Rating9.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating10_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.KickAccuracy = (float)OVR_Rating10.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating11_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.BreakTackle = (float)OVR_Rating11.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating12_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Tackle = (float)OVR_Rating12.Value;
            }
        }

        private void OVR_Rating13_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.PassBlock = (float)OVR_Rating13.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating14_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.RunBlock = (float)OVR_Rating14.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating15_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Jump = (float)OVR_Rating15.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating16_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.KickReturn = (float)OVR_Rating16.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Rating18_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.Injury = (float)OVR_Rating18.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_High_Needed_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.RatingHigh = (float)OVR_High_Needed.Value;
                InitOVR_Updown();
            }
        }

        private void OVR_Low_Needed_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                current_ovr.RatingLow = (float)OVR_Low_Needed.Value;
                InitOVR_Updown();
            }
        }

        
        #endregion        
    
        #region Regression

        public void InitRegressionView(int pdrp)
        {
            if (RegressionView != null)
                RegressionView.Rows.Clear();
            else
            {
                RegressionView = new DataGridView();
                RegressionView.BackgroundColor = Color.Silver;
                RegressionView.Bounds = new Rectangle(new Point(2, 2), new Size(658, 378));
                RegressionView.MultiSelect = false;
                RegressionView.AutoGenerateColumns = false;
                RegressionView.AllowUserToAddRows = false;
                RegressionView.RowHeadersVisible = false;
                RegressionView.ColumnCount = 13;

                RegressionView.Columns[0].Name = "Rec";
                RegressionView.Columns[0].Width = 50;
                RegressionView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[1].Name = "Pos";
                RegressionView.Columns[1].Width = 50;
                RegressionView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[2].Name = "OVR";
                RegressionView.Columns[2].Width = 50;
                RegressionView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[3].Name = "YRP";
                RegressionView.Columns[3].Width = 40;
                RegressionView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                
                RegressionView.Columns[4].Name = "ACC";
                RegressionView.Columns[4].Width = 40;
                RegressionView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[5].Name = "AGI";
                RegressionView.Columns[5].Width = 40;
                RegressionView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[6].Name = "INJ";
                RegressionView.Columns[6].Width = 40;
                RegressionView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[7].Name = "JMP";
                RegressionView.Columns[7].Width = 40;
                RegressionView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[8].Name = "KPW";
                RegressionView.Columns[8].Width = 40;
                RegressionView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[9].Name = "STA";
                RegressionView.Columns[9].Width = 40;
                RegressionView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[10].Name = "SPD";
                RegressionView.Columns[10].Width = 40;
                RegressionView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[11].Name = "STR";
                RegressionView.Columns[11].Width = 40;
                RegressionView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RegressionView.Columns[12].Name = "THP";
                RegressionView.Columns[12].Width = 40;
                RegressionView.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
            }

            foreach (PlayerRegression reg in manager.stream_model.TableModels[EditorModel.REGRESSION].GetRecords())
            {  
                if (pdrp != -1 && reg.DraftedPosition != pdrp)
                    continue;
                else
                {
                    string ovr = "";
                    if (reg.ProgressionGrade == 0)
                        ovr = "90-99";
                    else if (reg.ProgressionGrade == 1)
                        ovr = "82-89";
                    else if (reg.ProgressionGrade == 2)
                        ovr = "76-81";
                    else if (reg.ProgressionGrade == 3)
                        ovr = "70-75";
                    else if (reg.ProgressionGrade == 4)
                        ovr = "60-69";
                    else if (reg.ProgressionGrade == 5)
                        ovr = "0-59";
                    else ovr = "???";

                    string dp = Enum.GetName(typeof(PlayerDraftedPositions), reg.DraftedPosition);

                    object[] entry = { reg.RecNo, dp, ovr, reg.YearsPro, reg.ACC, reg.AGI, reg.INJ, reg.JMP, reg.KPW, reg.STA, reg.SPD, reg.STR, reg.THP };
                    RegressionView.Rows.Add(entry);
                }
            }
            
            RegressionTab.Controls.Add(RegressionView);
        }




        private void DraftedPositionFilter_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (DraftedPositionFilter_Combobox.SelectedIndex == 0)
                    InitRegressionView(-1);
                else InitRegressionView(DraftedPositionFilter_Combobox.SelectedIndex - 1);
            }
        }



        #endregion

        #region Progression
        public void InitPlayerProgression()
        {
            if (model.FileType == MaddenFileType.Franchise && model.FileVersion != MaddenFileVersion.Ver2004)
            {
                if (model.PlayerModel != null && LinkPlayer_Checkbox.Checked)
                {
                    currentptpos = model.PlayerModel.GetDraftedPosition();
                    ScoringPosition_Combobox.SelectedIndex = currentptpos;

                    int testgroup = 0;
                    if (model.PlayerModel.CurrentPlayerRecord.Overall >= 90)
                        testgroup = 0;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 82 && model.PlayerModel.CurrentPlayerRecord.Overall <= 89)
                        testgroup = 1;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 76 && model.PlayerModel.CurrentPlayerRecord.Overall <= 81)
                        testgroup = 2;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 70 && model.PlayerModel.CurrentPlayerRecord.Overall <= 75)
                        testgroup = 3;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 60 && model.PlayerModel.CurrentPlayerRecord.Overall <= 69)
                        testgroup = 4;
                    else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 0 && model.PlayerModel.CurrentPlayerRecord.Overall <= 59)
                        testgroup = 5;
                    if (currentptgroup != testgroup)
                    {
                        currentptgroup = testgroup;
                        ScoringGroup_Combobox.SelectedIndex = currentptgroup + 1;
                    }
                }

                InitProgressionScoring();
            }
        }

        #region Progression Schedule
        public void InitProgressionSchedule()
        {
            ProgressionScheduleView.Rows.Clear();
            ProgressionScheduleView.Columns.Clear();
            ProgressionScheduleView.MultiSelect = false;
            ProgressionScheduleView.AutoGenerateColumns = false;
            ProgressionScheduleView.AllowUserToAddRows = false;
            ProgressionScheduleView.RowHeadersVisible = false;
            ProgressionScheduleView.ColumnCount = 4;
            ProgressionScheduleView.Columns[0].Name = "Rec";
            ProgressionScheduleView.Columns[0].Width = 30;
            ProgressionScheduleView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionScheduleView.Columns[1].Name = "Period";
            ProgressionScheduleView.Columns[1].Width = 60;
            ProgressionScheduleView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionScheduleView.Columns[2].Name = "Type";
            ProgressionScheduleView.Columns[2].Width = 100;
            ProgressionScheduleView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionScheduleView.Columns[3].Name = "Week";
            ProgressionScheduleView.Columns[3].Width = 40;
            ProgressionScheduleView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            foreach (ProgressionSchedule sch in model.TableModels[EditorModel.PROGRESSION_SCHEDULE].GetRecords())
            {
                if (!sch.ProgressionActive)
                    continue;

                string type = "Regular";
                if (sch.SeasonType == 9)
                    type = "PreSeason";                
                object[] entry = { sch.RecNo, sch.ProgressionPeriod, type, sch.WeekNum + 1 };

                ProgressionScheduleView.Rows.Add(entry);
            }

        }

        #endregion

        #region Progression Table

        public void InitProgressionView()
        {
            ProgressionView.Rows.Clear();
            ProgressionView.Columns.Clear();
            ProgressionView.MultiSelect = false;
            ProgressionView.AutoGenerateColumns = false;
            ProgressionView.AllowUserToAddRows = false;
            ProgressionView.RowHeadersVisible = false;
            ProgressionView.ColumnCount = 15;
            ProgressionView.Columns[0].Name = "Rec";
            ProgressionView.Columns[0].Width = 40;
            ProgressionView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[1].Name = "Pos";
            ProgressionView.Columns[1].Width = 40;
            ProgressionView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            ProgressionView.Columns[2].Name = "Group";
            ProgressionView.Columns[2].Width = 50;
            ProgressionView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[3].Name = "LEV";
            ProgressionView.Columns[3].Width = 40;
            ProgressionView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[4].Name = "AWR";
            ProgressionView.Columns[4].Width = 40;
            ProgressionView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[5].Name = "BTK";
            ProgressionView.Columns[5].Width = 40;
            ProgressionView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[6].Name = "CAR";
            ProgressionView.Columns[6].Width = 40;
            ProgressionView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[7].Name = "CAT";
            ProgressionView.Columns[7].Width = 40;
            ProgressionView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[8].Name = "KAC";
            ProgressionView.Columns[8].Width = 40;
            ProgressionView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[9].Name = "KR";
            ProgressionView.Columns[9].Width = 40;
            ProgressionView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[10].Name = "PBK";
            ProgressionView.Columns[10].Width = 40;
            ProgressionView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[11].Name = "RBK";
            ProgressionView.Columns[11].Width = 40;
            ProgressionView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;           
            ProgressionView.Columns[12].Name = "STR";
            ProgressionView.Columns[12].Width = 40;
            ProgressionView.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[13].Name = "THA";
            ProgressionView.Columns[13].Width = 40;
            ProgressionView.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ProgressionView.Columns[14].Name = "TAK";
            ProgressionView.Columns[14].Width = 40;
            ProgressionView.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (PlayerProgression prog in manager.stream_model.TableModels[EditorModel.PROGRESSION].GetRecords())
            {
                string dp = Enum.GetName(typeof(PlayerDraftedPositions), prog.DraftedPos);
                string ovr = "";
                if (prog.ProgressionGrade == 0)
                        ovr = "90-99";
                    else if (prog.ProgressionGrade == 1)
                        ovr = "82-89";
                    else if (prog.ProgressionGrade == 2)
                        ovr = "76-81";
                    else if (prog.ProgressionGrade == 3)
                        ovr = "70-75";
                    else if (prog.ProgressionGrade == 4)
                        ovr = "60-69";
                    else if (prog.ProgressionGrade == 5)
                        ovr = "0-59";
                    else ovr = "???";
                object[] entry = { prog.RecNo, dp, ovr, prog.PlayerEval, prog.AWR, prog.BTK, prog.CAR, prog.CAT, prog.KAC, prog.KRT, prog.PBL, prog.RBL, 
                                     prog.STR, prog.THA, prog.TAK };

                ProgressionView.Rows.Add(entry);
            }
        }

        #endregion

        #region PT Tables
        public void InitProgressionScoring()
        {
            ProgressionScoringView.Rows.Clear();
            ProgressionScoringView.Columns.Clear();
            ProgressionScoringView.MultiSelect = false;
            ProgressionScoringView.AutoGenerateColumns = false;
            ProgressionScoringView.AllowUserToAddRows = false;
            ProgressionScoringView.RowHeadersVisible = false;
            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                ProgressionScoringView.ColumnCount = 5;
            else ProgressionScoringView.ColumnCount = 4;
            int c = 0;
            ProgressionScoringView.Columns[c].Name = "Rec";
            ProgressionScoringView.Columns[c].Width = 30;
            ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c++;
            if (model.FileVersion <= MaddenFileVersion.Ver2006)
            {
                ProgressionScoringView.Columns[c].Name = "Period";
                ProgressionScoringView.Columns[c].Width = 50;
                ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c++;
                ProgressionScoringView.Columns[c].Name = "Psit";
                ProgressionScoringView.Columns[c].Width = 40;
                ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c++;
            }
            else
            {
                ProgressionScoringView.Columns[c].Name = "Group";
                ProgressionScoringView.Columns[c].Width = 50;
                ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c++;
            }

            ProgressionScoringView.Columns[c].Name = "Stat";
            ProgressionScoringView.Columns[c].Width = 40;
            ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            c++;
            ProgressionScoringView.Columns[c].Name = "Points";
            ProgressionScoringView.Columns[c].Width = 50;
            ProgressionScoringView.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            List<ProgressionTracking> created = new List<ProgressionTracking>();
            
            foreach (ProgressionTracking t in manager.stream_model.TableModels[currentpttable].GetRecords())
            {
                bool add = false;
                bool add2 = false;

                if (model.FileVersion <= MaddenFileVersion.Ver2006)
                {
                    if (ScoringStat_Combobox.Items.Count > 0)
                    {
                                               
                    }
                }
                else
                {
                    if (currentptgroup >= 0)
                    {
                        if (t.Group == currentptgroup)
                            add = true;
                    }
                    else add = true;

                    if (ScoringStat_Combobox.Items.Count > 0)
                    {
                        if (currentptstat < 0)
                            add2 = true;
                        else if (t.Type == currentptstat)
                            add2 = true;
                        else add2 = false;
                    }
                }

                if (add && add2)
                    created.Add(t);                
            }

            created.Sort((x, y) => x.Type.CompareTo(y.Type));

            foreach (ProgressionTracking t in created)
            {
                if (model.FileVersion <= MaddenFileVersion.Ver2006)
                {
                    object[] entry = { t.RecNo, t.ProgressionPeriod, t.Psit, t.Type, t.Points };
                    ProgressionScoringView.Rows.Add(entry);
                }
                else
                {                    
                    string ovr = "";
                    if (t.Group == 0)
                        ovr = "90-99";
                    else if (t.Group == 1)
                        ovr = "82-89";
                    else if (t.Group == 2)
                        ovr = "76-81";
                    else if (t.Group == 3)
                        ovr = "70-75";
                    else if (t.Group == 4)
                        ovr = "60-69";
                    else if (t.Group == 5)
                        ovr = "0-59";
                    else ovr = "???";
                    object[] entry = { t.RecNo, ovr, t.Type, t.Points };
                    ProgressionScoringView.Rows.Add(entry);
                }
            }
        }
       
        private void ScoringPosition_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                int check = 0;
                if (ScoringPosition_Combobox.SelectedIndex <= 0)
                    check = 0;
                else check = ScoringPosition_Combobox.SelectedIndex;
                
                if (currentptpos == check)
                    return;
                else currentptpos = check;

                InitScoringStatType();
                InitProgressionScoring();
                
            }
        }
        
        private void ScoringGroup_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (currentptgroup == ScoringGroup_Combobox.SelectedIndex - 1)
                    return;
                else if (ScoringGroup_Combobox.SelectedIndex == 0)
                    currentptgroup = -1;
                else currentptgroup = ScoringGroup_Combobox.SelectedIndex - 1;

                InitProgressionScoring();
            }
        }

        private void ProgressionScoringView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DataGridViewRow row = ProgressionScoringView.Rows[e.RowIndex];
                int entry = (int)row.Cells[0].Value;
                int change = (int)e.ColumnIndex;

                if (ProgressionScoringView.Columns[change].Name == "Points")
                {
                    ProgressionTracking pt = (ProgressionTracking)manager.stream_model.TableModels[currentpttable].GetRecord(entry);
                    pt.Points = Convert.ToInt32(row.Cells[change].Value);
                }                    
            }

            InitProgressionView();
        }

        public void InitScoringStatType()
        {
            ScoringStat_Combobox.Items.Clear();
            ScoringStat_Combobox.Items.Add("ALL");
            if (currentptpos <= 0)
            {
                currentpttable = EditorModel.PTQB;
            }
            else if (currentptpos == 1)
                currentpttable = EditorModel.PTHB;
            else if (currentptpos == 2)
                currentpttable = EditorModel.PTFB;
            else if (currentptpos == 3)
                currentpttable = EditorModel.PTWR;
            else if (currentptpos == 4)
                currentpttable = EditorModel.PTTE;
            else if (currentptpos == 5)
                currentpttable = EditorModel.PTTA;
            else if (currentptpos == 6)
                currentpttable = EditorModel.PTGA;
            else if (currentptpos == 7)
                currentpttable = EditorModel.PTCE;
            else if (currentptpos == 8)
                currentpttable = EditorModel.PTDE;
            else if (currentptpos == 9)
                currentpttable = EditorModel.PTDT;
            else if (currentptpos == 10)
                currentpttable = EditorModel.PTOB;
            else if (currentptpos == 11)
                currentpttable = EditorModel.PTMB;
            else if (currentptpos == 12)
                currentpttable = EditorModel.PTCB;
            else if (currentptpos == 13)
                currentpttable = EditorModel.PTFS;
            else if (currentptpos == 14)
                currentpttable = EditorModel.PTSS;
            else if (currentptpos == 15)
                currentpttable = EditorModel.PTKI;
            else if (currentptpos == 16)
                currentpttable = EditorModel.PTPU;
            else if (currentptpos == 17)
                currentpttable = EditorModel.PTKP;

            int total = (manager.stream_model.TableModels[currentpttable].RecordCount / 5);

            for (int c = 0; c < total; c++)
            {
                ScoringStat_Combobox.Items.Add(c);
            }

            if (currentptstat == -1)
                ScoringStat_Combobox.SelectedIndex = 0;
            else if (currentptstat < ScoringStat_Combobox.Items.Count - 1)
            {                
                ScoringStat_Combobox.SelectedIndex = currentptstat+1;
            }
            else
            {
                ScoringStat_Combobox.SelectedIndex = 0;
                currentptstat = 0;
            }
        }

        #endregion

        private void ClearScoringPoints_Button_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ProgressionScoringView.Rows)
            {
                int rec = (int)row.Cells[0].Value;
                foreach (ProgressionTracking pt in manager.stream_model.TableModels[currentpttable].GetRecords())
                {
                    if (pt.RecNo != rec)
                        continue;
                    else
                    {
                        pt.Points = 0;
                    }
                }
            }

            InitProgressionScoring();
        }

        private void ScoringStat_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (currentptstat == -1 && ScoringStat_Combobox.SelectedIndex == 0)
                    return;
                else if (currentptstat == ScoringStat_Combobox.SelectedIndex - 1)
                    return;
                else if (ScoringStat_Combobox.SelectedIndex == 0)
                    currentptstat = -1;
                else currentptstat = ScoringStat_Combobox.SelectedIndex - 1;

                InitProgressionScoring();
            }
        }

        private void TestStat_Button_Click(object sender, EventArgs e)
        {
            if (ScoringStat_Combobox.SelectedIndex <= 0)
                return;

            foreach (ProgressionTracking pt in manager.stream_model.TableModels[currentpttable].GetRecords())
            {
                if (pt.Type == ScoringStat_Combobox.SelectedIndex - 1)
                {
                    if (pt.Group == 0)
                        pt.Points = 1;
                    else if (pt.Group == 1)
                        pt.Points = 2;
                    else if (pt.Group == 2)
                        pt.Points = 3;
                    else if (pt.Group == 3)
                        pt.Points = 4;
                    else if (pt.Group == 5)
                        pt.Points = 6;
                }

                InitProgressionScoring();
            }
        }

        private void ClearALLScoringPoints_Button_Click(object sender, EventArgs e)
        {
            for (int pt = 0; pt < 18; pt++)
            {
                if (pt == 0)
                    currentpttable = EditorModel.PTQB;
                else if (pt == 1)
                    currentpttable = EditorModel.PTHB;
                else if (pt == 2)
                    currentpttable = EditorModel.PTFB;
                else if (pt == 3)
                    currentpttable = EditorModel.PTWR;
                else if (pt == 4)
                    currentpttable = EditorModel.PTTE;
                else if (pt == 5)
                    currentpttable = EditorModel.PTTA;
                else if (pt == 6)
                    currentpttable = EditorModel.PTGA;
                else if (pt == 7)
                    currentpttable = EditorModel.PTCE;
                else if (pt == 8)
                    currentpttable = EditorModel.PTDE;
                else if (pt == 9)
                    currentpttable = EditorModel.PTDT;
                else if (pt == 10)
                    currentpttable = EditorModel.PTOB;
                else if (pt == 11)
                    currentpttable = EditorModel.PTMB;
                else if (pt == 12)
                    currentpttable = EditorModel.PTCB;
                else if (pt == 13)
                    currentpttable = EditorModel.PTFS;
                else if (pt == 14)
                    currentpttable = EditorModel.PTSS;
                else if (pt == 15)
                    currentpttable = EditorModel.PTKI;
                else if (pt == 16)
                    currentpttable = EditorModel.PTPU;
                else if (pt == 17)
                    currentpttable = EditorModel.PTKP;

                foreach (DataGridViewRow row in ProgressionScoringView.Rows)
                {
                    int rec = (int)row.Cells[0].Value;
                    foreach (ProgressionTracking track in manager.stream_model.TableModels[currentpttable].GetRecords())
                    {
                        if (track.RecNo != rec)
                            continue;
                        else
                        {
                            track.Points = 0;
                        }
                    }
                }

                InitProgressionScoring();
            }
        }

        private void TestAllStats_Button_Click(object sender, EventArgs e)
        {
            if (ScoringStat_Combobox.SelectedIndex <= 0)
                return;

            for (int pt = 0; pt < 17; pt++)
            {
                if (pt == 0)
                    currentpttable = EditorModel.PTQB;
                else if (pt == 1)
                    currentpttable = EditorModel.PTHB;
                else if (pt == 2)
                    currentpttable = EditorModel.PTFB;
                else if (pt == 3)
                    currentpttable = EditorModel.PTWR;
                else if (pt == 4)
                    currentpttable = EditorModel.PTTE;
                else if (pt == 5)
                    currentpttable = EditorModel.PTTA;
                else if (pt == 6)
                    currentpttable = EditorModel.PTGA;
                else if (pt == 7)
                    currentpttable = EditorModel.PTCE;
                else if (pt == 8)
                    currentpttable = EditorModel.PTDE;
                else if (pt == 9)
                    currentpttable = EditorModel.PTDT;
                else if (pt == 10)
                    currentpttable = EditorModel.PTOB;
                else if (pt == 11)
                    currentpttable = EditorModel.PTMB;
                else if (pt == 12)
                    currentpttable = EditorModel.PTCB;
                else if (pt == 13)
                    currentpttable = EditorModel.PTFS;
                else if (pt == 14)
                    currentpttable = EditorModel.PTSS;
                else if (pt == 15)
                    currentpttable = EditorModel.PTKI;
                else if (pt == 16)
                    currentpttable = EditorModel.PTPU;

                //  Skip this, not sure if it is kicker punter or kick returner
                else if (pt == 17)
                    currentpttable = EditorModel.PTKP;

                foreach (ProgressionTracking track in manager.stream_model.TableModels[currentpttable].GetRecords())
                {
                    if (ScoringStat_Combobox.SelectedIndex - 1 == track.Type)
                    {
                        if (track.Group == 0)
                            track.Points = 1;
                        else if (track.Group == 1)
                            track.Points = 2;
                        else if (track.Group == 2)
                            track.Points = 3;
                        else if (track.Group == 3)
                            track.Points = 4;
                        else if (track.Group == 5)
                            track.Points = 6;
                    }
                }
            }

            InitProgressionScoring();
        }
        
        #endregion

        
        
       
    }
    

}
