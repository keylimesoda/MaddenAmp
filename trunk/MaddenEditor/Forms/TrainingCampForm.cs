/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using System.IO;
using System.Reflection;

namespace MaddenEditor.Forms
{
    public partial class TrainingCampForm : Form
    {
        private EditorModel model = null;
        private PlayerRecord lastLoadedRecord = null;
        private bool isInitialising = false;
        private DepthChartEditingModel depthEditingModel = null;
        string installDirectory;
        string CurrentActivity = "";
        public string franchiseFilename = "";
        int CurPercent = 0;
        string CurName = "";
        public int CurDay; public int WindSpeed; public int Temp; public string Facility; public int HeadCold; public double TghRainBonus;
        public decimal TghBonus; public decimal CatBonus; public decimal WthInjIncrease; public decimal HvyRainAwrBonus;
        public string CurTeam;
        public string Stage; 
        private string PosStr = "";
        private int SpdMean = 0;
        private int AccMean = 0;
        private int AgiMean = 0;
        private int StrMean = 0;
        private int StmMean = 0;
        private int InjMean = 0;
        private double PosWgtMod = 0; private int Rating = 0; 
        public TrainingCampSplashScreen trainingCampSplashScreen = null;
        public TrainingCampMeeting trainingCampMeeting = null;
        public TeamRecord tn;
        DataTable RosterView = new DataTable();
        BindingSource RosterViewBinding = new BindingSource();
        DataTable ActivityView = new DataTable();
        BindingSource ActivityViewBinding = new BindingSource();
        DataTable AllocateTimingView = new DataTable();
        BindingSource AllocateTimingViewBinding = new BindingSource();
        Random random = new Random();
        OwnerRecord CPUteam;

        public TrainingCampForm(EditorModel model)
        {
            isInitialising = true;
            this.model = model;
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
                    //Create output directory if none exists
                    string installDirectory = Application.StartupPath;
                    if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp"))
                    {
                        Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp");
                    }
                    string fullFranchisePath = model.GetFileName();
                    string[] splitPath = fullFranchisePath.Split('\\');
                    franchiseFilename = splitPath[splitPath.Length - 1];

                    depthEditingModel = new DepthChartEditingModel(model);      
                    isInitialising = true;
                                          
                    foreach (TeamRecord team in model.TeamModel.GetTeams())
                    {
                        selectHumanTeam.Items.Add(team);
                    }
                    selectHumanTeam.Items.RemoveAt(34);//Remove NFC,AFC,FREEAGENT
                    selectHumanTeam.Items.RemoveAt(33);
                    selectHumanTeam.Items.RemoveAt(32);
                    foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
                    {
                        filterPositionComboBox.Items.Add(pos);
                    }
               //     filterPositionComboBox.Items.Add("OL");
               //     filterPositionComboBox.Items.Add("DL");
              //      filterPositionComboBox.Items.Add("LB");
               //     filterPositionComboBox.Items.Add("DB");
              
                    foreach (string pos in Enum.GetNames(typeof(MaddenPositionGroups)))
                    {
                        filterPositionComboBox.Items.Add(pos);
                      
                    }

                    //Populate Activity combobox
                    ActivityCmb.Items.Add("Position Drills");
                    ActivityCmb.Items.Add("Aerobic/Cardio");
                    ActivityCmb.Items.Add("Dietary");
                    ActivityCmb.Items.Add("Weight Training");
                    ActivityCmb.Items.Add("Team");

                    GroupAssign.Items.Add("0%");
                    GroupAssign.Items.Add("1%");
                    GroupAssign.Items.Add("2%");
                    GroupAssign.Items.Add("3%");
                    GroupAssign.Items.Add("4%");
                    GroupAssign.Items.Add("5%");
                    GroupAssign.Items.Add("6%");
                    GroupAssign.Items.Add("7%");
                    GroupAssign.Items.Add("8%");
                    GroupAssign.Items.Add("9%");
                    GroupAssign.Items.Add("10%");

                    massCmb.Items.Add("0%");
                    massCmb.Items.Add("1%");
                    massCmb.Items.Add("2%");
                    massCmb.Items.Add("3%");
                    massCmb.Items.Add("4%");
                    massCmb.Items.Add("5%");
                    massCmb.Items.Add("6%");
                    massCmb.Items.Add("7%");
                    massCmb.Items.Add("8%");
                    massCmb.Items.Add("9%");
                    massCmb.Items.Add("10%");
                  /* 
                    foreach (string s in Enum.GetNames(typeof(MaddenPositionGroups)))
                    {
                        RookiePositionFilter.Items.Add(s);
                        incrementPosition.Items.Add(s);
                    }
            */



                    isInitialising = false;   
                }
                public void CleanUI()
                {
                    selectHumanTeam.Items.Clear();
                    filterPositionComboBox.Items.Clear();
                   //attributeCombo.Items.Clear();
                }
                #endregion

        private void LoadCampActivityNames()
        { 
            LoadPositionDrills(filterPositionComboBox.Text);
            CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
            ActivityLbl.Text = CurrentActivity;

            foreach (DataGridViewColumn i in ActivityGrd.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void LoadPositionDrills(string Pos)
        {
            installDirectory = Application.StartupPath;
            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");       

            ActivityView.Columns.Clear();
            ActivityView.Clear();

            ActivityView.Columns.Add(AddColumn("Activity", "System.String"));
            ActivityView.Columns.Add(AddColumn("Description", "System.String"));

            ActivityViewBinding.DataSource = ActivityView;
            ActivityGrd.DataSource = ActivityViewBinding;

            //Control Column Width
            int ColWidth = 400;

            ActivityGrd.Columns["Activity"].Width = 150;
            ActivityGrd.Columns["Description"].Width = ColWidth;

            while (!sr.EndOfStream)
            {
                DataRow ac = ActivityView.NewRow();
                string line = sr.ReadLine();
                if (line != "")
                {
                    string[] splitLine = line.Split(';');
                    //Positional
                    if (splitLine[1] == Pos + "-P")
                    {
                        ac["Activity"] = splitLine[2];
                        ac["Description"] = splitLine[3];
                        ActivityView.Rows.Add(ac);
                    }
                    else
                    {
                        if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                        {
                            if (splitLine[1] == "OL-P")
                            {
                                ac["Activity"] = splitLine[2];
                                ac["Description"] = splitLine[3];
                                ActivityView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                        {
                            if (splitLine[1] == "DL-P")
                            {
                                ac["Activity"] = splitLine[2];
                                ac["Description"] = splitLine[3];
                                ActivityView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                        {
                            if (splitLine[1] == "LB-P")
                            {
                                ac["Activity"] = splitLine[2];
                                ac["Description"] = splitLine[3];
                                ActivityView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                        {
                            if (splitLine[1] == "DB-P")
                            {
                                ac["Activity"] = splitLine[2];
                                ac["Description"] = splitLine[3];
                                ActivityView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "K" | Pos == "P")
                        {
                            if (splitLine[1] == "KP-P")
                            {
                                ac["Activity"] = splitLine[2];
                                ac["Description"] = splitLine[3];
                                ActivityView.Rows.Add(ac);
                            }
                        }

                    }
                }//end while
            }
            sr.Close(); 
           
        }       
        private void LoadConditioning()
        {
            installDirectory = Application.StartupPath;
            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");

            ActivityView.Columns.Clear();
            ActivityView.Clear();

            ActivityView.Columns.Add(AddColumn("Activity", "System.String"));
            ActivityView.Columns.Add(AddColumn("Description", "System.String"));

            ActivityViewBinding.DataSource = ActivityView;
            ActivityGrd.DataSource = ActivityViewBinding;

            //Control Column Width
            int ColWidth = 400;

            ActivityGrd.Columns["Activity"].Width = 150;
            ActivityGrd.Columns["Description"].Width = ColWidth;
            string Type = "";
            if (ActivityCmb.SelectedIndex == 1)
            {
                Type = "A";
            }
            else if (ActivityCmb.SelectedIndex == 2)
            {
                Type = "D";
            }
            else if (ActivityCmb.SelectedIndex == 3)
            {
                Type = "W";
            }

            while (!sr.EndOfStream)
            {
                DataRow ac = ActivityView.NewRow();
                string line = sr.ReadLine();
                if (line != "")
                {
                    string[] splitLine = line.Split(';');
                    //Misc                
                    if (splitLine[1] == Type)
                    {
                        ac["Activity"] = splitLine[2];
                        ac["Description"] = splitLine[3];
                        ActivityView.Rows.Add(ac);
                    }

                }
            }
            //end while
            sr.Close();

        }
        private void LoadTeamDrills()
        {
           /* 
            "T","7 on 7 Passing Skeleton","++AWR Plus Positional Bonuses  Quarterbacks, Runningbacks, Receivers, Tight Ends vs. Linebackers and Defensive backs to work on coverages and patterns.";Wgt|.02|;Spd|0|;Acc|0|;Agi|0|;Str|.04|;Stm|0|;Inj|0|;Tgh|0|;Mor|0|;Awr|0|;Cat|0|;Car|.04|;Jmp|0|;Btk|0|;Tkl|0|;Thp|0|;Tha|0|;Pbk|.01|;Rbk|.01|;KP|0|;KA|0|;KR|0|;%Inj|1|;
            "T","5 on 5","++AWR Plus Positional Bonuses  Offensive Linemen vs. Defensive Linemen working on protection schemes and pass rushing, run block combinations and run defense.";Wgt|.02|;Spd|0|;Acc|0|;Agi|0|;Str|.04|;Stm|0|;Inj|0|;Tgh|0|;Mor|0|;Awr|0|;Cat|0|;Car|.04|;Jmp|0|;Btk|0|;Tkl|0|;Thp|0|;Tha|0|;Pbk|.01|;Rbk|.01|;KP|0|;KA|0|;KR|0|;%Inj|1|;
            "T","Team Scrimmage half speed","++AWR Plus Positional Bonuses  Entire team walk throughs, designed to allow players to memorize assignments.";Wgt|.02|;Spd|0|;Acc|0|;Agi|0|;Str|.04|;Stm|0|;Inj|0|;Tgh|0|;Mor|0|;Awr|0|;Cat|0|;Car|.04|;Jmp|0|;Btk|0|;Tkl|0|;Thp|0|;Tha|0|;Pbk|.01|;Rbk|.01|;KP|0|;KA|0|;KR|0|;%Inj|1|;
            "T","Team Scrimmage LIVE","+++AWR Plus Positional Bonuses  Live scrimmage between players. QBs do not get hit and thus do not have the chance of injury.";Wgt|.02|;Spd|0|;Acc|0|;Agi|0|;Str|.04|;Stm|0|;Inj|0|;Tgh|0|;Mor|0|;Awr|0|;Cat|0|;Car|.04|;Jmp|0|;Btk|0|;Tkl|0|;Thp|0|;Tha|0|;Pbk|.01|;Rbk|.01|;KP|0|;KA|0|;KR|0|;%Inj|1|;
            */
            
            ActivityView.Columns.Clear();
            ActivityView.Clear();

            ActivityView.Columns.Add(AddColumn("Activity", "System.String"));
            ActivityView.Columns.Add(AddColumn("Description", "System.String"));

            ActivityViewBinding.DataSource = ActivityView;
            ActivityGrd.DataSource = ActivityViewBinding;

            //Control Column Width
            int ColWidth = 400;

            ActivityGrd.Columns["Activity"].Width = 150;
            ActivityGrd.Columns["Description"].Width = ColWidth;



            AllocateTimingView.Clear();

            DataRow ac = ActivityView.NewRow();
                DataRow ab = ActivityView.NewRow();
           //     ac["Activity"] = "7 on 7 Passing Skeleton";
           //     ac["Description"] = "++AWR Plus Positional Bonuses  Quarterbacks, Runningbacks, Receivers, Tight Ends vs. Linebackers and Defensive backs to work on coverages and patterns.";
           //     ac["Activity"] = "5 on 5";
           //     ac["Description"] = "++AWR Plus Positional Bonuses  Offensive Linemen vs. Defensive Linemen working on protection schemes and pass rushing, run block combinations and run defense.";
                ac["Activity"] = "Team Scrimmage half speed";
                ac["Description"] = "++AWR Plus Positional Bonuses  Entire team walk throughs, designed to allow players to memorize assignments.";
                ab["Activity"] = "Team Scrimmage LIVE";
                ab["Description"] = "+++AWR Plus Positional Bonuses  Live scrimmage between players. Strong chance of injury. QBs do not get hit and thus do not have the chance of injury.";

            ActivityView.Rows.Add(ac);
            ActivityView.Rows.Add(ab);

            installDirectory = Application.StartupPath;
            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");

            string[] Total = sr.ReadLine().Split(',');            
            sr.Close();


            DataRow dr = AllocateTimingView.NewRow();
            DataRow db = AllocateTimingView.NewRow();

            dr["Name"] = "Team Scrimmage half speed";
            dr["Remaining Time"] = Total[0] + "%";
            dr["Allocated To"] = Total[1] + "%";
            
            
            db["Name"] = "Team Scrimmage LIVE";
            db["Remaining Time"] = Total[0] + "%";
            db["Allocated To"] = Total[2] + "%";

            AllocateTimingView.Rows.Add(dr);
            AllocateTimingView.Rows.Add(db);
        }
        private void SaveTeamDrills()
        {
            installDirectory = Application.StartupPath;
            StreamReader sr1 = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");
            

            
            if (CurName == "Team Scrimmage half speed")
            {
                               
                string[] tt = (sr1.ReadLine().Split(','));
                int TeamDrillTimeRemaining = int.Parse(tt[0]);
                sr1.Close();
                int Difference = (int.Parse(tt[1])) - CurPercent;
                int Time = TeamDrillTimeRemaining + Difference;

                StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");
                sw.Write(Time + "," + CurPercent + "," + tt[2]);
                sw.Close();
            }
            else if (CurName == "Team Scrimmage LIVE")
            {
                                
                string[] tt = (sr1.ReadLine().Split(','));
                int TeamDrillTimeRemaining = int.Parse(tt[0]);
                sr1.Close();
                int Difference = (int.Parse(tt[2])) - CurPercent;
                int Time = TeamDrillTimeRemaining + Difference;

                StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");
                sw.Write(Time + "," + tt[1] + "," + CurPercent);
                sw.Close();
            }
        }
     	/*	private void availablePlayerDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				availablePlayerDatagrid.ClearSelection();
				availablePlayerDatagrid.Rows[e.RowIndex].Selected = true;
			}
		}

		private void availablePlayerDatagrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				availablePlayerDatagrid.ClearSelection();
				availablePlayerDatagrid.Rows[e.RowIndex].Selected = true;
			}
		}
*/
		

        private enum EditableAttributes
        {
         AGE,
            YEARS_EXP,
            SPEED,
            STRENGTH,
            AWARENESS,
            AGILITY,
            ACCELERATION,
            CATCHING,
            CARRYING,
            JUMPING,
            BREAK_TACKLE,
            TACKLE,
            THROW_POWER,
            THROW_ACCURACY,
            PASS_BLOCKING,
            RUN_BLOCKING,
            KICK_POWER,
            KICK_ACCURACY,
            KICK_RETURN,
            STAMINA,
            INJURY,
            TOUGHNESS,
            IMPORTANCE,
            MORALE
        }

        private DataColumn AddColumn(string ColName, string ColType)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = ColName;
            dc.DataType = System.Type.GetType(ColType);
            return dc;
        }

        public void LoadPlayerInfo(PlayerRecord record)
        {
            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            isInitialising = true;

            try
            {
                
               

                TeamRecord team = model.TeamModel.GetTeamRecord(record.TeamId);
                selectHumanTeam.SelectedItem = (object)team;
                filterPositionComboBox.Text = filterPositionComboBox.Items[record.PositionId].ToString();
            


                string NAME = record.FirstName + " " + record.LastName;
                int AGE = record.Age;
                int YEARS_EXP = record.YearsPro;
                int OVR = record.Overall;
                int SPEED = record.Speed;
                int STRENGTH = record.Strength;
                int AWARENESS = record.Awareness;
                int AGILITY = record.Agility;
                int ACCELERATION = record.Acceleration;
                int CATCHING = record.Catching;
                int CARRYING = record.Carrying;
                int JUMPING = record.Jumping;
                int BREAK_TACKLE = record.BreakTackle;
                int TACKLE = record.Tackle;
                int THROW_POWER = record.ThrowPower;
                int THROW_ACCURACY = record.ThrowAccuracy;
                int PASS_BLOCKING = record.PassBlocking;
                int RUN_BLOCKING = record.RunBlocking;
                int KICK_POWER = record.KickPower;
                int KICK_ACCURACY = record.KickAccuracy;
                int KICK_RETURN = record.KickReturn;
                int STAMINA = record.Stamina;
                int INJURY = record.Injury;
                int TOUGHNESS = record.Toughness;
                int MORALE = record.Morale;
                

            }

 
            catch (Exception e)
            {
                MessageBox.Show("Exception Occured loading this Player:\r\nCaused by " + e.Source + "\r\n" + e.ToString(), "Exception Loading Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadPlayerInfo(lastLoadedRecord);
                return;
            }
            finally
            {
                isInitialising = false;
            }
            lastLoadedRecord = record;
        }


        private void InitializeDataGrids()
        {
            isInitialising = true;
            RosterView.Clear();
            RosterView.Columns.Clear();

            RosterView.Columns.Add(AddColumn("Name", "System.String"));
            RosterView.Columns.Add(AddColumn("Age", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Exp", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Ovr", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Hgt", "System.String"));
            RosterView.Columns.Add(AddColumn("Wgt", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Spd", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Acc", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Agi", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Str", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Stm", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Inj", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Tgh", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Mor", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Awr", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Cat", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Car", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Jmp", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Btk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Tkl", "System.Int16"));
            RosterView.Columns.Add(AddColumn("ThP", "System.Int16"));
            RosterView.Columns.Add(AddColumn("ThA", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Pbk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Rbk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KP", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KA", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KR", "System.Int16"));
           
            RosterViewBinding.DataSource = RosterView;
            depthChartDataGrid.DataSource = RosterViewBinding;            

            //Control Column Width
            int ColWidth = 32;

            depthChartDataGrid.Columns["Name"].Width = 120;
            depthChartDataGrid.Columns["Age"].Width = ColWidth;
            depthChartDataGrid.Columns["Exp"].Width = ColWidth;
            depthChartDataGrid.Columns["Ovr"].Width = ColWidth;
            depthChartDataGrid.Columns["Hgt"].Width = ColWidth;
            depthChartDataGrid.Columns["Wgt"].Width = ColWidth;
            depthChartDataGrid.Columns["Spd"].Width = ColWidth;
            depthChartDataGrid.Columns["Acc"].Width = ColWidth;
            depthChartDataGrid.Columns["Agi"].Width = ColWidth;
            depthChartDataGrid.Columns["Str"].Width = ColWidth;
            depthChartDataGrid.Columns["Stm"].Width = ColWidth;
            depthChartDataGrid.Columns["Inj"].Width = ColWidth;
            depthChartDataGrid.Columns["Tgh"].Width = ColWidth;
            depthChartDataGrid.Columns["Mor"].Width = ColWidth;
            depthChartDataGrid.Columns["Awr"].Width = ColWidth;
            depthChartDataGrid.Columns["Cat"].Width = ColWidth;
            depthChartDataGrid.Columns["Car"].Width = ColWidth;
            depthChartDataGrid.Columns["Jmp"].Width = ColWidth;
            depthChartDataGrid.Columns["Btk"].Width = ColWidth;
            depthChartDataGrid.Columns["Tkl"].Width = ColWidth;
            depthChartDataGrid.Columns["ThP"].Width = ColWidth;
            depthChartDataGrid.Columns["ThA"].Width = ColWidth;
            depthChartDataGrid.Columns["Pbk"].Width = ColWidth;
            depthChartDataGrid.Columns["Rbk"].Width = ColWidth;
            depthChartDataGrid.Columns["KP"].Width = ColWidth;
            depthChartDataGrid.Columns["KA"].Width = ColWidth;
            depthChartDataGrid.Columns["KR"].Width = ColWidth;

            depthChartDataGrid.Columns["Name"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Age"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Exp"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Ovr"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Hgt"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Wgt"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Spd"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Acc"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Agi"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Str"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Stm"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Inj"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Tgh"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Mor"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Awr"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Cat"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Car"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Jmp"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Btk"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Tkl"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["ThP"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["ThA"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Pbk"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["Rbk"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["KP"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["KA"].Resizable = DataGridViewTriState.False;
            depthChartDataGrid.Columns["KR"].Resizable = DataGridViewTriState.False;

            AllocateTimingView.Clear();
            AllocateTimingView.Columns.Clear();

            AllocateTimingView.Columns.Add(AddColumn("Name", "System.String"));
            AllocateTimingView.Columns.Add(AddColumn("Remaining Time", "System.String"));
            AllocateTimingView.Columns.Add(AddColumn("Allocated To", "System.String"));

            AllocateTimingViewBinding.DataSource = AllocateTimingView;
            SetTimeGrd.DataSource = AllocateTimingViewBinding;            

            SetTimeGrd.Columns["Name"].Width = 120;
            SetTimeGrd.Columns["Remaining Time"].Width = 60;
            SetTimeGrd.Columns["Allocated To"].Width = 55;
            SetTimeGrd.Columns["Name"].ReadOnly = true;
            SetTimeGrd.Columns["Remaining Time"].ReadOnly = true;
            SetTimeGrd.Columns["Allocated To"].ReadOnly = true;            
            
            SetTimeGrd.Columns.Add(TrainingTime);
            SetTimeGrd.Columns["TrainingTime"].Width = 50;
            SetTimeGrd.Columns["TrainingTime"].Resizable = DataGridViewTriState.False;
            SetTimeGrd.Columns["Name"].Resizable = DataGridViewTriState.False;
            SetTimeGrd.Columns["Remaining Time"].Resizable = DataGridViewTriState.False;
            SetTimeGrd.Columns["Allocated To"].Resizable = DataGridViewTriState.False;

            SetTimingCombo();
           
            //Set Team combo box to record 0

           // selectHumanTeam.SelectedIndex = 0;
           // filterPositionComboBox.SelectedIndex = 0;

        //    model.PlayerModel.SetTeamFilter(selectHumanTeam.Text);
         //   model.PlayerModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);
         //   model.PlayerModel.GetNextPlayerRecord();
           isInitialising = false;
           
       //     RefillRosterView();
            
        }

        public void RefillRosterView()
        {
            RosterView.Clear();
            string Pos = "";
            int acCounter = 0;
            string installDirectory = Application.StartupPath;
            isInitialising = true;
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;           


            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            
            //Ind. Position view
            if ((filterPositionComboBox.SelectedIndex >= 0) & (filterPositionComboBox.SelectedIndex <= 20))
            {

                foreach (PlayerRecord valObject in teamPlayers)
                {
                    if (valObject.PositionId == positionId)
                    {
                        if (valObject.PositionId == 0)
                        {
                            Pos = "QB";
                        }
                        else if (valObject.PositionId == 1)
                        {
                            Pos = "HB";
                        }
                        else if (valObject.PositionId == 2)
                        {
                            Pos = "FB";
                        }
                        else if (valObject.PositionId == 3)
                        {
                            Pos = "WR";
                        }
                        else if (valObject.PositionId == 4)
                        {
                            Pos = "TE";
                        }
                        else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                        {
                            Pos = "OL";
                        }
                        else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                        {
                            Pos = "DL";
                        }
                        else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                        {
                            Pos = "LB";
                        }
                        else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                        {
                            Pos = "DB";
                        }
                        else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                        {
                            Pos = "KP";
                        }




                        DataRow dr = RosterView.NewRow();
                        // playerHeightComboBox.SelectedIndex = record.Height - 65;
                        valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                        //Reload the overall rating
                        valObject.Overall = valObject.Overall;
                        dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                        dr["Age"] = valObject.Age;
                        dr["Exp"] = valObject.YearsPro;
                        dr["Ovr"] = valObject.Overall;
                        // Would like to include feet/inches
                        dr["Hgt"] = valObject.Height;
                        dr["Wgt"] = valObject.Weight + 160;
                        dr["Spd"] = valObject.Speed;
                        dr["Acc"] = valObject.Acceleration;
                        dr["Agi"] = valObject.Agility;
                        dr["Str"] = valObject.Strength;
                        dr["Stm"] = valObject.Stamina;
                        dr["Inj"] = valObject.Injury;
                        dr["Tgh"] = valObject.Toughness;
                        dr["Mor"] = valObject.Morale;
                        dr["Awr"] = valObject.Awareness;
                        dr["Cat"] = valObject.Catching;
                        dr["Car"] = valObject.Carrying;
                        dr["Jmp"] = valObject.Jumping;
                        dr["Btk"] = valObject.BreakTackle;
                        dr["Tkl"] = valObject.Tackle;
                        dr["ThP"] = valObject.ThrowPower;
                        dr["ThA"] = valObject.ThrowAccuracy;
                        dr["Pbk"] = valObject.PassBlocking;
                        dr["Rbk"] = valObject.RunBlocking;
                        dr["KP"] = valObject.KickPower;
                        dr["KA"] = valObject.KickAccuracy;
                        dr["KR"] = valObject.KickReturn;

                        RosterView.Rows.Add(dr);
                        
                        StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                        string OldTotalsContents = sr.ReadToEnd();
                        sr.Close();
                        string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                        string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                        int ovrO = int.Parse(OldRatings[2]);
                        int wgtO = int.Parse(OldRatings[1]);
                        int spdO = int.Parse(OldRatings[3]);
                        int accO = int.Parse(OldRatings[4]);
                        int agiO = int.Parse(OldRatings[5]);
                        int strO = int.Parse(OldRatings[6]);
                        int stmO = int.Parse(OldRatings[7]);
                        int injO = int.Parse(OldRatings[8]);
                        int tghO = int.Parse(OldRatings[9]);
                        int morO = int.Parse(OldRatings[10]);
                        int awrO = int.Parse(OldRatings[11]);
                        int catO = int.Parse(OldRatings[12]);
                        int carO = int.Parse(OldRatings[13]);
                        int jmpO = int.Parse(OldRatings[14]);
                        int btkO = int.Parse(OldRatings[15]);
                        int tklO = int.Parse(OldRatings[16]);
                        int thpO = int.Parse(OldRatings[17]);
                        int thaO = int.Parse(OldRatings[18]);
                        int pbkO = int.Parse(OldRatings[19]);
                        int rbkO = int.Parse(OldRatings[20]);
                        int kpO = int.Parse(OldRatings[21]);
                        int kaO = int.Parse(OldRatings[22]);
                        int krO = int.Parse(OldRatings[23]);

                        CellColor(3, acCounter, valObject.Overall, ovrO);
                        CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                        CellColor(6, acCounter, valObject.Speed, spdO);
                        CellColor(7, acCounter, valObject.Acceleration, accO);
                        CellColor(8, acCounter, valObject.Agility, agiO);
                        CellColor(9, acCounter, valObject.Strength, strO);
                        CellColor(10, acCounter, valObject.Stamina, stmO);
                        CellColor(11, acCounter, valObject.Injury, injO);
                        CellColor(12, acCounter, valObject.Toughness, tghO);
                        CellColor(13, acCounter, valObject.Morale, morO);
                        CellColor(14, acCounter, valObject.Awareness, awrO);
                        CellColor(15, acCounter, valObject.Catching, catO);
                        CellColor(16, acCounter, valObject.Carrying, carO);
                        CellColor(17, acCounter, valObject.Jumping, jmpO);
                        CellColor(18, acCounter, valObject.BreakTackle, btkO);
                        CellColor(19, acCounter, valObject.Tackle, tklO);
                        CellColor(20, acCounter, valObject.ThrowPower, thpO);
                        CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                        CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                        CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                        CellColor(24, acCounter, valObject.KickPower, kpO);
                        CellColor(25, acCounter, valObject.KickAccuracy, kaO);                        
                        CellColor(26, acCounter, valObject.KickReturn, krO);
                        acCounter = acCounter + 1;

                        

                    }
                }
            }

            else if (filterPositionComboBox.SelectedIndex > 20) 
            {
                acCounter = 0;
                if (filterPositionComboBox.SelectedIndex == 21)
                {
                    acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId >= 5) && (valObject.PositionId <= 9))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);

                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 22)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 5) || (valObject.PositionId == 9))
                        {
                            
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);
                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 23)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 6) || (valObject.PositionId == 8))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);
                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 24)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11) || (valObject.PositionId == 12))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);

                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 25)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);

                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 26)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 14) || (valObject.PositionId == 15))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);

                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 27)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 15))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);

                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 28)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 16) || (valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);
                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 29)
                {
                     acCounter = 0;
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            if (valObject.PositionId == 0)
                            {
                                Pos = "QB";
                            }
                            else if (valObject.PositionId == 1)
                            {
                                Pos = "HB";
                            }
                            else if (valObject.PositionId == 2)
                            {
                                Pos = "FB";
                            }
                            else if (valObject.PositionId == 3)
                            {
                                Pos = "WR";
                            }
                            else if (valObject.PositionId == 4)
                            {
                                Pos = "TE";
                            }
                            else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                            {
                                Pos = "OL";
                            }
                            else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                            {
                                Pos = "DL";
                            }
                            else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                            {
                                Pos = "LB";
                            }
                            else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                            {
                                Pos = "DB";
                            }
                            else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                            {
                                Pos = "KP";
                            }
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                            string OldTotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                            string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                            int ovrO = int.Parse(OldRatings[2]);
                            int wgtO = int.Parse(OldRatings[1]);
                            int spdO = int.Parse(OldRatings[3]);
                            int accO = int.Parse(OldRatings[4]);
                            int agiO = int.Parse(OldRatings[5]);
                            int strO = int.Parse(OldRatings[6]);
                            int stmO = int.Parse(OldRatings[7]);
                            int injO = int.Parse(OldRatings[8]);
                            int tghO = int.Parse(OldRatings[9]);
                            int morO = int.Parse(OldRatings[10]);
                            int awrO = int.Parse(OldRatings[11]);
                            int catO = int.Parse(OldRatings[12]);
                            int carO = int.Parse(OldRatings[13]);
                            int jmpO = int.Parse(OldRatings[14]);
                            int btkO = int.Parse(OldRatings[15]);
                            int tklO = int.Parse(OldRatings[16]);
                            int thpO = int.Parse(OldRatings[17]);
                            int thaO = int.Parse(OldRatings[18]);
                            int pbkO = int.Parse(OldRatings[19]);
                            int rbkO = int.Parse(OldRatings[20]);
                            int kpO = int.Parse(OldRatings[21]);
                            int kaO = int.Parse(OldRatings[22]);
                            int krO = int.Parse(OldRatings[23]);
                            CellColor(3, acCounter, valObject.Overall, ovrO);
                            CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                            CellColor(6, acCounter, valObject.Speed, spdO);
                            CellColor(7, acCounter, valObject.Acceleration, accO);
                            CellColor(8, acCounter, valObject.Agility, agiO);
                            CellColor(9, acCounter, valObject.Strength, strO);
                            CellColor(10, acCounter, valObject.Stamina, stmO);
                            CellColor(11, acCounter, valObject.Injury, injO);
                            CellColor(12, acCounter, valObject.Toughness, tghO);
                            CellColor(13, acCounter, valObject.Morale, morO);
                            CellColor(14, acCounter, valObject.Awareness, awrO);
                            CellColor(15, acCounter, valObject.Catching, catO);
                            CellColor(16, acCounter, valObject.Carrying, carO);
                            CellColor(17, acCounter, valObject.Jumping, jmpO);
                            CellColor(18, acCounter, valObject.BreakTackle, btkO);
                            CellColor(19, acCounter, valObject.Tackle, tklO);
                            CellColor(20, acCounter, valObject.ThrowPower, thpO);
                            CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                            CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                            CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                            CellColor(24, acCounter, valObject.KickAccuracy, kaO);
                            CellColor(25, acCounter, valObject.KickReturn, krO);


                            acCounter = acCounter + 1;
                        }
                    }

                }




               
            }




            // Sort the current view after running foreach loop unless view empty
            //if (RosterView.Rows.Count != 0)
            //{
            //    depthChartDataGrid.Sort(depthChartDataGrid.Columns["Ovr"], ListSortDirection.Descending);
            //  // depthChartDataGrid.CurrentRow = depthChartDataGrid[0, 0];            
            //}
            //else
            //{
            //    DataRow NoEntries = RosterView.NewRow();
            //    NoEntries["Name"] = "No Players";
            //    RosterView.Rows.Add(NoEntries); 
            //}

            RefillAllocateTimingView(filterPositionComboBox.Text);

            isInitialising = false;
        }
        private void FillTextBox(string Pos)
        {
            isInitialising = true;
            string installDirectory = Application.StartupPath;
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);


            foreach (PlayerRecord valObject in teamPlayers) 
            {
                if ((valObject.FirstName + " " + valObject.LastName) == CurName)
                {
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;
                    if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                    {
                        Pos = "OL";
                    }
                    else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                    {
                        Pos = "DL";
                    }
                    else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                    {
                        Pos = "LB";
                    }
                    else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                    {
                        Pos = "DB";
                    }
                    else if (Pos == "K" | Pos == "P")
                    {
                        Pos = "KP";
                    }


                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    string[] Time = sr.ReadLine().Split(',');
                    string Allcontents = sr.ReadToEnd();
                    sr.Close();
                    
                    string TimeRemaining = "";
                    if (ActivityCmb.SelectedIndex == 0)
                    {
                        TimeRemaining = Time[1];
                    }
                    else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                    {
                        TimeRemaining = Time[0];
                    }
                    else if (ActivityCmb.SelectedIndex == 4)
                    {
                        TimeRemaining = Time[2];
                    }
                    
                    
                    
                    string[] Line = Allcontents.Split('\n');
                    int Len = Line.Length;
                    textBox1.Text = "Current Allocations for " + CurName + "\r\n";                   
                    textBox1.Text = textBox1.Text + "----------------------------------------------------------------------\r\n";
                    if (Len > 2) 
                    {
                        for (int i = 1; i < Len; i++)
                        {
                            if (Line[i] != "")
                            {
                                string Delim = "\r\n";
                                string CurrentLine = Line[i].Trim(Delim.ToCharArray());
                                string[] CurrentLineArray = CurrentLine.Split(',');
                                textBox1.Text = textBox1.Text + "---" + CurrentLineArray[0] + ": " + CurrentLineArray[1] + "%\r\n";
                            }
                        }
                    }
                 
                }

            }
        }
        private void RefillAllocateTimingView(string Pos)
        {
            string installDirectory = Application.StartupPath;
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            string fn = "";
            string ln = "";
            isInitialising = true;

            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;
            int drCounter = 0;
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            AllocateTimingView.Clear();
            if ((filterPositionComboBox.SelectedIndex >= 0) & (filterPositionComboBox.SelectedIndex <= 20))
            {
               

                foreach (PlayerRecord valObject in teamPlayers)
                {
                    if (valObject.PositionId == positionId)
                    {
                        fn = valObject.FirstName;
                        ln = valObject.LastName;
                        DataRow dr = AllocateTimingView.NewRow();
                        Pos = filterPositionComboBox.Text;
                        // playerHeightComboBox.SelectedIndex = record.Height - 65;
                        if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT")
                        {
                            Pos = "OL";
                        }
                        else if (Pos == "RE" | Pos == "LE" | Pos == "DT")
                        {
                            Pos = "DL";
                        }
                        else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB")
                        {
                            Pos = "LB";
                        }
                        else if (Pos == "CB" | Pos == "FS" | Pos == "SS")
                        {
                            Pos = "DB";
                        }
                        else if (Pos == "K" | Pos == "P")
                        {
                            Pos = "KP";
                        }


                        dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                        installDirectory = Application.StartupPath;
                        StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                        string[] Time = sr.ReadLine().Split(',');
                        string TimeRemaining = "";
                        if (ActivityCmb.SelectedIndex == 0)
                        {
                            TimeRemaining = Time[1];
                        }
                        else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                        {
                            TimeRemaining = Time[0];
                        }
                        else if (ActivityCmb.SelectedIndex == 4)
                        {
                            TimeRemaining = Time[2];
                        }

                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] splitLine = line.Split(',');
                            if (splitLine.Length > 1)
                            {
                                if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                {
                                    dr["Remaining Time"] = TimeRemaining + "%";
                                }
                                if (splitLine[0] == CurrentActivity)
                                {
                                    dr["Allocated To"] = splitLine[1] + "%";
                                }
                                //    else if (splitLine[0] != CurrentActivity)
                                //   {
                                //      dr["Allocated To"] = 0 + "%";
                                //   }
                            }
                        }
                        sr.Close();

                        AllocateTimingView.Rows.Add(dr);
                        if (CurDay > 2) //check injury status
                        {
                            sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                            string TotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] TotalsContentsLine = TotalsContents.Split(',');
                            if (int.Parse(TotalsContentsLine[2]) > 0)
                            {

                                SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                SetTimeGrd.CurrentRow.ReadOnly = true;
                            }
                            drCounter = drCounter + 1;
                        }
                       
                    }

                }
            }

            else if (filterPositionComboBox.SelectedIndex > 20)
            {
                if (filterPositionComboBox.SelectedIndex == 21)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId >= 5) && (valObject.PositionId <= 9))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 22)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 5) || (valObject.PositionId == 9))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 23)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 6) || (valObject.PositionId == 8))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 24)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11) || (valObject.PositionId == 12))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }

                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 25)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 26)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 14) || (valObject.PositionId == 15))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 27)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 15))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }

                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 28)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 16) || (valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 29)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            fn = valObject.FirstName;
                            ln = valObject.LastName;
                            DataRow dr = AllocateTimingView.NewRow();
                            Pos = filterPositionComboBox.Text;
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                            {
                                Pos = "OL";
                            }
                            else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                            {
                                Pos = "DL";
                            }
                            else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                            {
                                Pos = "LB";
                            }
                            else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                            {
                                Pos = "DB";
                            }
                            else if (Pos == "K" | Pos == "P")
                            {
                                Pos = "KP";
                            }


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                            string[] Time = sr.ReadLine().Split(',');
                            string TimeRemaining = "";
                            if (ActivityCmb.SelectedIndex == 0)
                            {
                                TimeRemaining = Time[1];
                            }
                            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                            {
                                TimeRemaining = Time[0];
                            }
                            else if (ActivityCmb.SelectedIndex == 4)
                            {
                                TimeRemaining = Time[2];
                            }

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] splitLine = line.Split(',');
                                if (splitLine.Length > 1)
                                {
                                    if (splitLine[0] == (valObject.FirstName + " " + valObject.LastName))
                                    {
                                        dr["Remaining Time"] = TimeRemaining + "%";
                                    }
                                    if (splitLine[0] == CurrentActivity)
                                    {
                                        dr["Allocated To"] = splitLine[1] + "%";
                                    }
                                    //    else if (splitLine[0] != CurrentActivity)
                                    //   {
                                    //      dr["Allocated To"] = 0 + "%";
                                    //   }
                                }
                            }
                            sr.Close();

                            AllocateTimingView.Rows.Add(dr);
                            //  SetTimingCombo(); 
                            if (CurDay > 2) //check injury status
                            {
                                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + fn + " " + ln + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                if (int.Parse(TotalsContentsLine[2]) > 0)
                                {

                                    SetTimeGrd.CurrentCell = SetTimeGrd[0, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[1, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[2, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell = SetTimeGrd[3, drCounter];
                                    SetTimeGrd.CurrentCell.Style.SelectionBackColor = Color.Pink;
                                    SetTimeGrd.CurrentCell.Style.BackColor = Color.Pink;

                                    SetTimeGrd.CurrentRow.ReadOnly = true;
                                }
                                drCounter = drCounter + 1;
                            }
                        }

                    }

                }
 

            }
            
            
        }



        private void TrainingCampForm_Load(object sender, EventArgs e)
        {
           InitializeDataGrids();
        }
        private void BeginCamp()
        {

                StreamWriter sw;
                StreamWriter sw1;
                StreamWriter sw2;
                StreamWriter sw3;
                string installDirectory = Application.StartupPath;
                if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text))
                {
                    Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System");
                }

                FileStream system = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\system");
                sw = new StreamWriter(system);
                sw.WriteLine(selectHumanTeam.Text + "," + "Hell Week" + "," + "1");
                sw.Close();
                system = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\coachsliders");
                sw1 = new StreamWriter(system);
                sw1.WriteLine(0 + "," + 0 + "," + 0 + "," + 0 + "," + 0);
                sw1.Close();
                system = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\currentteam");
                sw2 = new StreamWriter(system);
                sw2.WriteLine(selectHumanTeam.SelectedIndex);     
                sw2.Close();
                system = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\TeamDrills");
                sw3 = new StreamWriter(system);
                sw3.WriteLine(0);
                sw3.Close();
                CurTeam = selectHumanTeam.Text;
                tn = (TeamRecord)selectHumanTeam.SelectedItem;
               
                string fileName = model.GetFileName();
                string backupFile = fileName.Substring(0, fileName.LastIndexOf('.')) + "-tcbakup";
                string newFile;
                File.Delete(backupFile + "0.fra");
                int index = 0;
                while (true)
                {
                    newFile = backupFile + index + ".fra";
                    if (!File.Exists(newFile))
                    {
                        break;
                    }
                    index++;
                }

                File.Copy(fileName, newFile);
        }
        private void UpdateCamp()
        {
            string installDirectory = Application.StartupPath;

            StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\system");
            sw.WriteLine(CurTeam + "," + Stage + "," + CurDay);
            sw.Close();

            tn = (TeamRecord)selectHumanTeam.SelectedItem;
        }
        private void selectHumanTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string installDirectory = Application.StartupPath;
            if (Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text))
            {
                DialogResult dr = MessageBox.Show("Continue modifying the " + selectHumanTeam.Text + "?\n\nSelecting 'No' will delete current allocations...", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (dr == DialogResult.No)
                {
                    DialogResult drr = MessageBox.Show("Begin Training Camp for " + selectHumanTeam.Text + "?...", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                    if (drr == DialogResult.Yes)
                    {
                        Directory.Delete((installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text), true);
                        BeginCamp();

                        //   OutputRoster();
                     //   filterPositionComboBox.SelectedIndex = 0;
                     //   ActivityCmb.SelectedIndex = 0;
                    //    RefillRosterView();
                        this.Hide();
                        ActivityCmb.Enabled = true;
                        LaunchSplash();
                        selectHumanTeam.Enabled = false;
                        filterPositionComboBox.Enabled = true;
                        groupBox6.Enabled = true;
                        label1.Text = "Advance to Hell Week Day 2...";
                        
                        //  AdvanceBtn.Text = "Start...";
                        //  groupBox6.Enabled = true;
                    }
                    else if (drr == DialogResult.No)
                {
                    return;
                }

                }
                else if (dr == DialogResult.Yes)
                {
                    
                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\system");
                    string Allcontents = sr.ReadLine();
                    sr.Close();
                    string[] Line = Allcontents.Split(',');
                    CurTeam = Line[0];
                    Stage = Line[1];
                    CurDay = Int32.Parse(Line[2]);
                    label1.Text = "Advance to " + Stage + " Day " + CurDay + "...";
                    filterPositionComboBox.SelectedIndex = 0;
                    selectHumanTeam.Enabled = false;
                    ActivityCmb.Enabled = true;
                    GroupAssign.Enabled = false;
                    SetTimeGrd.Enabled = false;
                 //   ActivityCmb.SelectedIndex = 0;
                    filterPositionComboBox.Enabled = true;
                    groupBox6.Enabled = true;
                    if (CurDay <= 7)
                    {
                        ActivityCmb.Items.Remove("Team");
                    }
                 //   AdvanceBtn.Text = "Continue...";
                 //   groupBox6.Enabled = true;

                }

            }
            else 
            {
                DialogResult drr = MessageBox.Show("Begin Training Camp for " + selectHumanTeam.Text + "?...", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (drr == DialogResult.Yes)
                {
                    BeginCamp();
                //    OutputRoster();
                //    filterPositionComboBox.SelectedIndex = 0;
                //    ActivityCmb.SelectedIndex = 0;
                //    RefillRosterView();
                    this.Hide();
                    LaunchSplash();
                    ActivityCmb.Enabled = true;
                    filterPositionComboBox.Enabled = true;
                    selectHumanTeam.Enabled = false;
                    groupBox6.Enabled = true;
                    label1.Text = "Advance to Hell Week Day 2...";
                    //  AdvanceBtn.Text = "Start...";
                    //  groupBox6.Enabled = true;

                }
                else if (drr == DialogResult.No)
                {
                    return;
                }
               

            }
            Cursor.Current = Cursors.Arrow;
        }
        private void SetTimingCombo()
        {
            TrainingTime.Items.Clear();
            TrainingTime.ValueType = System.Type.GetType("System.String");
            TrainingTime.Items.Add("0%");
            TrainingTime.Items.Add("1%");
            TrainingTime.Items.Add("2%");
            TrainingTime.Items.Add("3%");
            TrainingTime.Items.Add("4%");
            TrainingTime.Items.Add("5%");
            TrainingTime.Items.Add("6%");
            TrainingTime.Items.Add("7%");
            TrainingTime.Items.Add("8%");
            TrainingTime.Items.Add("9%");
            TrainingTime.Items.Add("10%");
           

          //  for (int i = 0; i < SetTimeGrd.Rows.Count; i++)
          //  {
                
          //  }
          //  SetTimeGrd.Columns.Add(TrainingTime);
        }
        private void SetTimingComboTeamDrills()
        {
            TrainingTime.Items.Clear();
            TrainingTime.ValueType = System.Type.GetType("System.String");
            TrainingTime.Items.Add("0%");
            TrainingTime.Items.Add("1%");
            TrainingTime.Items.Add("2%");
            TrainingTime.Items.Add("3%");
            TrainingTime.Items.Add("4%");
            TrainingTime.Items.Add("5%");
            TrainingTime.Items.Add("6%");
            TrainingTime.Items.Add("7%");
            TrainingTime.Items.Add("8%");
            TrainingTime.Items.Add("9%");
            TrainingTime.Items.Add("10%");
            TrainingTime.Items.Add("11%");
            TrainingTime.Items.Add("12%");
            TrainingTime.Items.Add("13%");
            TrainingTime.Items.Add("14%");
            TrainingTime.Items.Add("15%");
            TrainingTime.Items.Add("16%");
            TrainingTime.Items.Add("17%");
            TrainingTime.Items.Add("18%");
            TrainingTime.Items.Add("19%");
            TrainingTime.Items.Add("20%");
            TrainingTime.Items.Add("21%");
            TrainingTime.Items.Add("22%");
            TrainingTime.Items.Add("23%");
            TrainingTime.Items.Add("24%");
            TrainingTime.Items.Add("25%");          

            //  for (int i = 0; i < SetTimeGrd.Rows.Count; i++)
            //  {

            //  }
            //  SetTimeGrd.Columns.Add(TrainingTime);
        }
        private void SaveCurrentAllocations(string Pos)
        {

            
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            AllocateTimingView.Clear();

            foreach (PlayerRecord valObject in teamPlayers)
            {
                if ((valObject.FirstName + " " + valObject.LastName) == (CurName))
                {
                    DataRow dr = AllocateTimingView.NewRow();
                    Pos = filterPositionComboBox.Text;
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;
                    if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                    {
                        Pos = "OL";
                    }
                    else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                    {
                        Pos = "DL";
                    }
                    else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                    {
                        Pos = "LB";
                    }
                    else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                    {
                        Pos = "DB";
                    }
                    else if (Pos == "K" | Pos == "P")
                    {
                        Pos = "KP";
                    }

                    string Allcontents = "";
                    string TotalRemaining = "";
                    int difference = 0;

                    dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

                    string[] Total = sr.ReadLine().Split(',');
                    int SaveRemainingPosition = 0;
                    if (ActivityCmb.SelectedIndex == 0)
                    {
                        TotalRemaining = Total[1].ToString();
                        SaveRemainingPosition = 1;
                    }
                    else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                    {
                        TotalRemaining = Total[0].ToString();
                        SaveRemainingPosition = 0;
                    }
                    else if (ActivityCmb.SelectedIndex == 4)
                    {
                        TotalRemaining = Total[2].ToString();
                        SaveRemainingPosition = 2;
                    }


                    int Time = int.Parse(TotalRemaining);                    
                    Allcontents = sr.ReadToEnd();
                    sr.Close();
                    string[] Line = Allcontents.Split('\n');
                    int Len = Line.Length;
                    int OldValue = 0;
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    
                    for (int i = 0; i < Len; i++)
                    {
                        string CurrentLine = Line[i];
                        string[] CurrentText = Line[i].Split(',');
                        if ((CurrentText[0] != "") & (CurrentActivity != ""))
                        {
                            if (CurrentText[0] == CurrentActivity)
                            {
                                OldValue = Int32.Parse(CurrentText[1]);
                                int Start = Allcontents.IndexOf(CurrentLine) - 1;
                                int NukeLength = CurrentLine.Length + 1;
                                Allcontents = Allcontents.Remove(Start, NukeLength);
                                
                               
                            }
                        }

                    }
                    difference = OldValue - CurPercent;
                    Time = Time + difference;            

                    if (SaveRemainingPosition == 0)
                    {
                        sw.Write(Time + "," + Total[1] + "," + Total[2] + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }
                    else if (SaveRemainingPosition == 1)
                    {
                        sw.Write(Total[0] + "," + Time + "," + Total[2] + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }
                    else if (SaveRemainingPosition == 2)
                    {
                        sw.Write(Total[0] + "," + Total[1] + "," + Time + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }

              
                        //              sw.Write(TotalRemaining);
                        sw.WriteLine();
                        sw.Write(Allcontents);
                        if (CurPercent != 0)
                        {
                            sw.Write(CurrentActivity + "," + CurPercent);
                            sw.WriteLine();
                        }
               //     string[] Line = Allcontents.Split('\n');
               //     int Len = Line.Length;
                    
                    
                    //  string[] TimeRemaining = Allcontents.Split('/');
                  //  int TotalTimeint = TimeRemaining[0].Length;
                  //  string CurContents = Allcontents.Remove(0, TotalTimeint);
                  //  string[] Line = CurContents.Split('\n');
                  //  int Len = Line.Length;
                    
                  
                    
                     
                   
                   sw.Close();
                  
                   
                      AllocateTimingView.Rows.Add(dr);
           //         SetTimingCombo();

                }

            }



        }


        private void SaveCurrentAllocationsMassConditioning(string Pos)
        {               
                   
                    string Allcontents = "";
                    string TotalRemaining = "";
                    int difference = 0;

                    
                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + CurName);

                    string[] Total = sr.ReadLine().Split(',');
                    int SaveRemainingPosition = 0;
                    if (ActivityCmb.SelectedIndex == 0)
                    {
                        TotalRemaining = Total[1].ToString();
                        SaveRemainingPosition = 1;
                    }
                    else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
                    {
                        TotalRemaining = Total[0].ToString();
                        SaveRemainingPosition = 0;
                    }
                    else if (ActivityCmb.SelectedIndex == 4)
                    {
                        TotalRemaining = Total[2].ToString();
                        SaveRemainingPosition = 2;
                    }


                    int Time = int.Parse(TotalRemaining);
                    Allcontents = sr.ReadToEnd();
                    sr.Close();
                    string[] Line = Allcontents.Split('\n');
                    int Len = Line.Length;
                    int OldValue = 0;
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + CurName);

                    for (int i = 0; i < Len; i++)
                    {
                        string CurrentLine = Line[i];
                        string[] CurrentText = Line[i].Split(',');
                        if ((CurrentText[0] != "") & (CurrentActivity != ""))
                        {
                            if (CurrentText[0] == CurrentActivity)
                            {
                                OldValue = Int32.Parse(CurrentText[1]);
                                int Start = Allcontents.IndexOf(CurrentLine) - 1;
                                int NukeLength = CurrentLine.Length + 1;
                                Allcontents = Allcontents.Remove(Start, NukeLength);


                            }
                        }

                    }
                    difference = OldValue - CurPercent;
                    Time = Time + difference;

                    if (SaveRemainingPosition == 0)
                    {
                        sw.Write(Time + "," + Total[1] + "," + Total[2] + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }
                    else if (SaveRemainingPosition == 1)
                    {
                        sw.Write(Total[0] + "," + Time + "," + Total[2] + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }
                    else if (SaveRemainingPosition == 2)
                    {
                        sw.Write(Total[0] + "," + Total[1] + "," + Time + "," + Total[3] + "," + Total[4] + "," + Total[5]);
                    }


                    sw.WriteLine();
                    sw.Write(Allcontents);
                    if (CurPercent != 0)
                    {
                        sw.Write(CurrentActivity + "," + CurPercent);
                        sw.WriteLine();
                    }
                       sw.Close();

        }
        private void PositionView_CheckedChanged(object sender, EventArgs e)
        {
            {
               // if (PositionView.Enabled == true)
                  //  groupBox2.Visible = true;
                   // groupBox3.Visible = false;
            }
       
        }

      

        private void Individual_CheckedChanged(object sender, EventArgs e)
        {
            {
           // if (Individual.Enabled == true)
               //     groupBox2.Visible = false;
             //       groupBox3.Visible = false;
                
              //  model.PlayerModel.SetTeamFilter(selectHumanTeam.Text);

                //Generate a move next so it will filter
              //  model.PlayerModel.GetNextPlayerRecord();
               // LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);                 
            }
        }

             
        private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           // if (!isInitialising)
         //   {
                RefillRosterView();
                ActivityCmb.Enabled = true;
           //     CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
           //     ActivityLbl.Text = CurrentActivity;
                
                RefreshCurrent();
                ClearMassConditioning();
                label2.Text = "Assign to all " + filterPositionComboBox.SelectedItem + "?";

          //  }
            if (ActivityCmb.SelectedIndex == 0)
            {
                RefillRosterView();
                LoadPositionDrills(filterPositionComboBox.Text);
           //     CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
           //     ActivityLbl.Text = CurrentActivity;
                RefreshCurrent();
                label2.Text = "Assign to all " + filterPositionComboBox.SelectedItem + "?";
            }
            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
            {
                RefillRosterView();
                LoadConditioning();
            //    CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
            //    ActivityLbl.Text = CurrentActivity;
                RefreshCurrent();
                label2.Text = "Assign to all " + filterPositionComboBox.SelectedItem + "?";
    
            }
           
        }

        private void ActivityCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterPositionComboBox.Enabled = true;
            GroupAssign.Enabled = true;
            SetTimeGrd.Enabled = true;
            label2.Visible = true;
            GroupAssign.Visible = true;
            SetTimingCombo();
            if (SetTimeGrd.Rows.Count > 0)
            {
                GroupAssign.Enabled = true;
            }
            ClearMassConditioning();
            label2.Text = "Assign to all " + filterPositionComboBox.SelectedItem + "?";
         
            if (ActivityCmb.SelectedIndex == 0)
            {
                LoadCampActivityNames();
                CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
                ActivityLbl.Text = CurrentActivity;
                RefreshCurrent();
            }
            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
            {
                LoadConditioning();
                CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
                ActivityLbl.Text = CurrentActivity;
                groupBox7.Enabled = true;
                EnableMassConditioning();
                RefreshCurrent();
   
            }
            else if (ActivityCmb.SelectedIndex == 4)
            {
                SetTimingComboTeamDrills();
                label2.Visible = false;
                GroupAssign.Visible = false;
                LoadTeamDrills();
                filterPositionComboBox.Enabled = false;
                ActivityLbl.Text = "Team Drills";
                GroupAssign.Enabled = false;
            }


        }
        private void RefreshCurrent()
        {
            if (ActivityGrd.Rows.Count > 0)
            {
                if (SetTimeGrd.Rows.Count > 0)
                {
                    CurName = (string)SetTimeGrd.Rows[0].Cells["Name"].Value;
                    CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
                    ActivityLbl.Text = CurrentActivity;
                    RefillAllocateTimingView(filterPositionComboBox.Text);
                    FillTextBox(filterPositionComboBox.Text);
                }
            }
        }
        private void ActivityGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ActivityCmb.SelectedIndex != 4)
            {
                if ((e.RowIndex >= 0) & (e.ColumnIndex >= 0))
                {
                    CurrentActivity = (string)ActivityGrd.Rows[e.RowIndex].Cells["Activity"].Value;
                    ActivityLbl.Text = CurrentActivity;
                    RefillAllocateTimingView(filterPositionComboBox.Text);
                    FillTextBox(filterPositionComboBox.Text);
                }
            }
        }
       
        private void SetTimeGrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ActivityCmb.SelectedIndex != 4)
            {
                string Time = (string)SetTimeGrd.Rows[e.RowIndex].Cells["TrainingTime"].Value;
                CurName = (string)SetTimeGrd.Rows[e.RowIndex].Cells["Name"].Value;
                if (Time != null)
                {
                    string[] splitLine = Time.Split('%');
                    CurPercent = Int32.Parse(splitLine[0]);

                    SaveCurrentAllocations(filterPositionComboBox.Text);
                    RefillAllocateTimingView(filterPositionComboBox.Text);
                    FillTextBox(filterPositionComboBox.Text);
                }
            }
            else if (ActivityCmb.SelectedIndex == 4)
            {

                string Time = (string)SetTimeGrd.Rows[e.RowIndex].Cells["TrainingTime"].Value;
                CurName = (string)SetTimeGrd.Rows[e.RowIndex].Cells["Name"].Value;
                if (Time != null)
                {
                    string[] splitLine = Time.Split('%');
                    CurPercent = Int32.Parse(splitLine[0]);

                    SaveTeamDrills();
                    LoadTeamDrills();
                }
            }
        }

        private void SetTimeGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) & (e.ColumnIndex >= 0) & (ActivityCmb.SelectedIndex != 4))
            {               
                CurName = (string)SetTimeGrd.Rows[e.RowIndex].Cells["Name"].Value;
                FillTextBox(filterPositionComboBox.Text);
            }
        }
        private void LaunchSplash()
        {
            {
                installDirectory = Application.StartupPath;
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\system");
                string Allcontents = sr.ReadLine();
                sr.Close();
                string[] Line = Allcontents.Split(',');
                CurTeam = Line[0];
                Stage = Line[1];
                CurDay = Int32.Parse(Line[2]);
                groupBox6.Enabled = false;
                this.Hide();
                TrainingCampSplashScreen form = new TrainingCampSplashScreen(model, this);
                form.Show();

            }

        }
        private void AdvanceBtn_Click(object sender, EventArgs e)
        {
            
                textBox1.Text = "";
                ExceptionReporting();

                if (textBox1.Text == "")
                {
                    if (CurDay <= 14)
                    {
                        DialogResult dr = MessageBox.Show("Process current day and proceed?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                        if (dr == DialogResult.No)
                        {
                            return;
                        }
                        else if (dr == DialogResult.Yes)
                        {
                            //textBox1.Text = "";
                            //ExceptionReporting();
                            //  if (textBox1.Text == "")
                            //  {
                            Cursor.Current = Cursors.WaitCursor;

                            ProcessDaily();
                            UpdateCamp();
                            LaunchSplash();
                        }
                    }

                    else if (CurDay == 15)
                    {
                        DialogResult drs = MessageBox.Show("Clicking Advance will immediately process day 14, the final day of camp.\n\nYou'll see the before camp and after camp attributes for each player.\n\nProceed?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                        if (drs == DialogResult.No)
                        {
                            return;
                        }
                        else if (drs == DialogResult.Yes)
                        {
                            // depthChartDataGrid.ReadOnly = true;
                            depthChartDataGrid.AllowUserToResizeColumns = true;
                            AdvanceBtn.Enabled = false;
                            label1.Text = "Final player progression...";
                            SetTimeGrd.Columns.Remove(TrainingTime);
                            ProcessFinal();
                            MessageBox.Show("A file has been generated in the Madden Amp install directory\nwithin /Conditioning/TrainingCamp/" + franchiseFilename + "/" + CurTeam + "\nnamed 'Final Progression.txt'");
                        }





                        Cursor.Current = Cursors.Arrow;
                        if (File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions"))
                        {
                            File.Delete(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions");
                        }
                    }
                }
        }

        private void GroupAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
      
                int Length = SetTimeGrd.RowCount;
                int i = 0;
                while (i < Length)
                {

                    string Time = (string)GroupAssign.SelectedItem;
                    CurName = (string)SetTimeGrd.Rows[i].Cells["Name"].Value;
                        if (Time != null)
                        {
                            string[] splitLine = Time.Split('%');
                            CurPercent = Int32.Parse(splitLine[0]);
                            if (SetTimeGrd.Rows[i].ReadOnly == false)
                            {
                                SaveCurrentAllocations(filterPositionComboBox.Text);
                                RefillAllocateTimingView(filterPositionComboBox.Text);
                                FillTextBox(filterPositionComboBox.Text);
                            }
                        }
                    
                    i++;
                }  

        }
        private void ExceptionReporting()
        {
            //Exception Reporting
            string Pos ="";
            textBox1.Text = "";
            installDirectory = Application.StartupPath;
           
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            //Ind. Position view
            foreach (PlayerRecord valObject in teamPlayers)
            {
                if (valObject.PositionId == 0)
                {
                    Pos = "QB";
                }
                else if (valObject.PositionId == 1)
                {
                    Pos = "HB";
                }
                else if (valObject.PositionId == 2)
                {
                    Pos = "FB";
                }
                else if (valObject.PositionId == 3)
                {
                    Pos = "WR";
                }
                else if (valObject.PositionId == 4)
                {
                    Pos = "TE";
                }
                else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                {
                    Pos = "OL";
                }
                else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                {
                    Pos = "DL";
                }
                else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                {
                    Pos = "LB";
                }
                else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                {
                    Pos = "DB";
                }
                else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                {
                    Pos = "KP";
                }

                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                string[] Time = sr.ReadLine().Split(',');
                string Allcontents = sr.ReadToEnd();
                sr.Close();

                if (((Stage == "Hell Week") & (int.Parse(Time[0]) < 0)) || ((Stage == "Hell Week") & (int.Parse(Time[1]) < 0)) || ((Stage == "Hell Week") & (int.Parse(Time[5]) < 0)))
                {
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions",true);
                    sw.Write("--" + valObject.FirstName + " " + valObject.LastName + ", " + Pos + ", Time Remaining less than zero.");
                    sw.WriteLine();
                    sw.Close();
                }
                else if (((Stage == "Training Camp") & (int.Parse(Time[0]) < 0)) || ((Stage == "Training Camp") & (int.Parse(Time[1]) < 0)) || ((Stage == "Training Camp") & (int.Parse(Time[2]) < 0)) || ((Stage == "Training Camp") & (int.Parse(Time[3]) < 0)) || ((Stage == "Training Camp") & (int.Parse(Time[4]) < 0)) || ((Stage == "Training Camp") & (int.Parse(Time[5]) < 0)))
                {
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions",true);
                    sw.Write("--" + valObject.FirstName + " " + valObject.LastName + ", " + Pos + ", Time Remaining less than zero.");
                    sw.WriteLine();
                    sw.Close();
                }
            }

            if (CurDay >= 8)
            {
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");

                string[] Total = sr.ReadLine().Split(',');
                sr.Close();

                if (int.Parse(Total[0]) != 0)
                {

                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions", true);
                    sw.Write("--Team Drill time still remaining. Please distribute Team Drills by selecting the Activity combo box and selecting Team.");
                    sw.WriteLine();
                    sw.Close();

                }

            }

            if (File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions"))
            {
                StreamReader sr1 = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions");
                string Exception = sr1.ReadToEnd();
                sr1.Close();
                if (Exception != "")
                {
                    textBox1.Text = "";
                    string[] Line = Exception.Split('\n');
                    int Len = Line.Length;
                    int i = 0;
                    while (i < Len)
                    {
                        string CurrentLine = Line[i];                      
                        textBox1.Text = textBox1.Text + CurrentLine + "\r\n";
                        i++;
                    }
                    MessageBox.Show("Exceptions detected. Please see Text Box at bottom of page for details.\nCannot proceed to next day until exceptions handled.");
                   
                }
                File.Delete(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\exceptions");
                return;
            }
        }

        public void AgeDeclination()
        {
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            //Ind. Position view
            foreach (PlayerRecord valObject in teamPlayers)
            {
                if (valObject.Age >= 30)
                {
                    string Pos = "";
                    decimal Drop = Math.Round(((100 - ((decimal)valObject.Age - 29)) / 100),2);

                    valObject.Speed = (int)(valObject.Speed * Drop);
                    valObject.Acceleration = (int)(valObject.Acceleration * Drop);
                    valObject.Agility = (int)(valObject.Agility * Drop);
                    valObject.Strength = (int)(valObject.Strength * Drop);
                    valObject.Injury = (int)(valObject.Injury * Drop);
                    valObject.Jumping = (int)(valObject.Jumping * Drop);
                    valObject.BreakTackle = (int)(valObject.BreakTackle * Drop);
                    valObject.Tackle = (int)(valObject.Tackle * Drop);
                    valObject.ThrowPower = (int)(valObject.ThrowPower * Drop);
                    valObject.KickPower = (int)(valObject.KickPower * Drop);

                    if (valObject.PositionId == 0)
                    {
                        Pos = "QB";
                    }
                    else if (valObject.PositionId == 1)
                    {
                        Pos = "HB";
                    }
                    else if (valObject.PositionId == 2)
                    {
                        Pos = "FB";
                    }
                    else if (valObject.PositionId == 3)
                    {
                        Pos = "WR";
                    }
                    else if (valObject.PositionId == 4)
                    {
                        Pos = "TE";
                    }
                    else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                    {
                        Pos = "OL";
                    }
                    else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                    {
                        Pos = "DL";
                    }
                    else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                    {
                        Pos = "LB";
                    }
                    else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                    {
                        Pos = "DB";
                    }
                    else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                    {
                        Pos = "KP";
                    }
                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    string skip = sr.ReadLine();
                    string Line = sr.ReadLine();
                    string[] OldRatings = Line.Split(',');
                    //A.J. Mitchell,213,74,  52,56,61,41,96,80,86,97,67,15,42,43,47,8,90,87,23,15,16,28,0
                    sr.Close();

                    if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\"))
                    {
                        Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\");
                    }
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\AgeDeclination.txt", true);

                    sw.WriteLine("-Old-" + valObject.FirstName + " " + valObject.LastName + "\tAge " + valObject.Age + "\tSpd=" + int.Parse(OldRatings[3]) + "\tAcc=" + int.Parse(OldRatings[4]) + "\tAgi=" + int.Parse(OldRatings[5]) + "\tStr=" + int.Parse(OldRatings[6]) + "\tStm=" + int.Parse(OldRatings[7]) + "\tInj=" + int.Parse(OldRatings[8]) +
                         " Tgh=" + int.Parse(OldRatings[9]) + "\tMor=" + int.Parse(OldRatings[10]) + "\tAwr=" + int.Parse(OldRatings[11]) + "\tCat=" + int.Parse(OldRatings[12]) + "\tCar=" + int.Parse(OldRatings[13]) + "\tJmp=" + int.Parse(OldRatings[14]) + "\tBtk=" + int.Parse(OldRatings[15]) + "\tTkl=" + int.Parse(OldRatings[16]) +
                         " ThP=" + int.Parse(OldRatings[17]) + "\tThA=" + int.Parse(OldRatings[18]) + "\tPbk=" + int.Parse(OldRatings[19]) + "\tRbk=" + int.Parse(OldRatings[20]) + "\tKP=" + int.Parse(OldRatings[21]) + "\tKA=" + int.Parse(OldRatings[22]) + "\tKR=" + int.Parse(OldRatings[23]));
                    sw.WriteLine("-New-" + valObject.FirstName + " " + valObject.LastName + "\tAge " + valObject.Age + "\tSpd=" + valObject.Speed + "\tAcc=" + valObject.Acceleration + "\tAgi=" + valObject.Agility + "\tStr=" + valObject.Strength + "\tStm=" + valObject.Stamina + "\tInj=" + valObject.Injury +
                         " Tgh=" + valObject.Toughness + "\tMor=" + valObject.Morale + "\tAwr=" + valObject.Awareness + "\tCat=" + valObject.Catching + "\tCar=" + valObject.Carrying + "\tJmp=" + valObject.Jumping + "\tBtk=" + valObject.BreakTackle + "\tTkl=" + valObject.Tackle +
                         " ThP=" + valObject.ThrowPower + "\tThA=" + valObject.ThrowAccuracy + "\tPbk=" + valObject.PassBlocking + "\tRbk=" + valObject.RunBlocking + "\tKP=" + valObject.KickPower + "\tKA=" + valObject.KickAccuracy + "\tKR=" + valObject.KickReturn);
                    sw.WriteLine();
                    sw.Close();



                }


            }


        }
        
        private void ProcessCPU()
        {

            installDirectory = Application.StartupPath;
            if (File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury"))
            {
                File.Delete(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury");
            }
           
            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsim");
            string TuneContents = sr.ReadToEnd();
            sr.Close();
            string[] TuneContentsLine = TuneContents.Split('\n');


            string Pos = "";
            int teamId = ((OwnerRecord)CPUteam).TeamId;
            int positionId = 0;
            
           // int Con = 25;
           // int PosDrill = 25;
           // int Team = 20;
            int Film = 20;
            int Special = 10;
           // int Down = 15;            

            model.CoachModel.SetPositionFilter(0);
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal HCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal HCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal HCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal HCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal HCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal HCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal HCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal HCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal HCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal HClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal HCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal HCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal HCp = model.CoachModel.CurrentCoachRecord.PuntRating;
            //head coach modifier to conditioning
            decimal HeadCoachConditioningMotivation = Math.Round((((HCMot - 70) / 100)),2);


            model.CoachModel.SetPositionFilter(1);
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal OCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal OCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal OCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal OCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal OCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal OCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal OCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal OCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal OCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal OClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal OCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal OCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal OCp = model.CoachModel.CurrentCoachRecord.PuntRating;

            model.CoachModel.SetPositionFilter(2);
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal DCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal DCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal DCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal DCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal DCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal DCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal DCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal DCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal DCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal DClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal DCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal DCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal DCp = model.CoachModel.CurrentCoachRecord.PuntRating;

            model.CoachModel.SetPositionFilter(3);
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal STKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal STMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal STChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal STEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal STqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal STrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal STwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal STol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal STdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal STlb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal STdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal STk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal STp = model.CoachModel.CurrentCoachRecord.PuntRating;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
       
           
            decimal PositionalDrillMod = 0;
            
            //Ind. Position view
            foreach (PlayerRecord valObject in teamPlayers)
            {               
                bool Injured = false;

                if (valObject.PositionId == 0)
                {
                    Pos = "QB";
                    PositionalDrillMod = Math.Round((((HCqb - 70) / 1500) + ((OCqb - 70) / 2000)),2);                    
                }
                else if (valObject.PositionId == 1)
                {
                    Pos = "HB";
                    PositionalDrillMod = Math.Round((((HCrb - 70) / 1500) + ((OCrb - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 2)
                {
                    Pos = "FB";
                    PositionalDrillMod = Math.Round((((HCrb - 70) / 1500) + ((OCrb - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 3)
                {
                    Pos = "WR";
                    PositionalDrillMod = Math.Round((((HCwr - 70) / 1500) + ((OCwr - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 4)
                {
                    Pos = "TE";
                    PositionalDrillMod = Math.Round((((HCwr - 70) / 1500) + ((OCwr - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                {
                    Pos = "OL";
                    PositionalDrillMod = Math.Round((((HCol - 70) / 1500) + ((OCol - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                {
                    Pos = "DL";
                    PositionalDrillMod = Math.Round((((HCdl - 70) / 1500) + ((DCdl - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                {
                    Pos = "LB";
                    PositionalDrillMod = Math.Round((((HClb - 70) / 1500) + ((DClb - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                {
                    Pos = "DB";
                    PositionalDrillMod = Math.Round((((HCdb - 70) / 1500) + ((DCdb - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 19)
                {
                    Pos = "KP";
                    PositionalDrillMod = Math.Round((((HCk - 70) / 1500) + ((STk - 70) / 2000)),2);
                }
                else if (valObject.PositionId == 20)
                {
                    Pos = "KP";
                    PositionalDrillMod = Math.Round((((HCp - 70) / 1500) + ((STp - 70) / 2000)),2);
                }


                //Effect HC and coordinators have on positional drills
              
                decimal CoachPositionalDrillModifier = 0;
                if (valObject.PositionId <= 9)
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((OCKnw - 70) / 1000)) + PositionalDrillMod), 2);
              //      DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((OCChm - 70) / 30)), 0);
                }
                else if ((valObject.PositionId >= 10) & (valObject.PositionId <= 18))
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((DCKnw - 70) / 1000)) + PositionalDrillMod), 2);
               //     DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((DCChm - 70) / 30)), 0);
                }
                else if (valObject.PositionId >= 19)
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((STKnw - 70) / 1000)) + PositionalDrillMod), 2);
              //      DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((STChm - 70) / 30)), 0);
                }
                              
                int DaysActive = 14;
                string InjuryType = "";
                int InjuryLength = 0;
                decimal wgt = 0;
                decimal spd = 0;
                decimal acc = 0;
                decimal agi = 0;
                decimal str = 0;
                decimal stm = 0;
                decimal inj = 0;
                decimal tgh = 0;
                decimal mor = valObject.Morale;
                decimal awr = 0;
                decimal cat = 0;
                decimal car = 0;
                decimal jmp = 0;
                decimal btk = 0;
                decimal tkl = 0;
                decimal thp = 0;
                decimal tha = 0;
                decimal pbk = 0;
                decimal rbk = 0;
                decimal kp = 0;
                decimal ka = 0;
                decimal kr = 0;
               // decimal injchance = 0;


                //age decremation        

                if (valObject.Age >= 30)
                {
                    decimal Drop = Math.Round(((100 - ((decimal)valObject.Age - 29)) / 100),2);

                    valObject.Speed = (int)(valObject.Speed * Drop);
                    valObject.Acceleration = (int)(valObject.Acceleration * Drop);
                    valObject.Agility = (int)(valObject.Agility * Drop);
                    valObject.Strength = (int)(valObject.Strength * Drop);
                    valObject.Injury = (int)(valObject.Injury * Drop);
                    valObject.Jumping = (int)(valObject.Jumping * Drop);
                    valObject.BreakTackle = (int)(valObject.BreakTackle * Drop);
                    valObject.Tackle = (int)(valObject.Tackle * Drop);
                    valObject.ThrowPower = (int)(valObject.ThrowPower * Drop);
                    valObject.KickPower = (int)(valObject.KickPower * Drop);

                }




                //INJURY LOGIC HERE               

                 //   int FinalInjChance = valObject.Injury + (valObject.Toughness / 10);
                bool Minor = false;
                bool Major = false;

                    if ((110 * random.NextDouble() - 10) > ((valObject.Injury) + valObject.Toughness / 10) )                   
                    {
                        Injured = true;

                        int severityroll = (int)((100 * random.NextDouble() + 1) + (valObject.Toughness / 10));
                        if (severityroll <= 10)
                        {
                            Major = true;
                        }
                        if (severityroll > 10)
                        {
                            Minor = true;
                        }

                    }




                if (Injured == true)
                {
                
                        //Injured = true;

                        //QB only minor injuries
                        //string QBminor = "39,Strained forearm,2,3;58,Sprained wrist,2,3;119,Abdominal strain,1,3;115,Pulled groin,3,4;97,Broken finger,1,1;98,Dislocated finger,1,1;" +
                        //"99,Broken thumb,3,5;100,Dislocated thumb,1,2;124,Dislocated shoulder,5,5;156,Bruised shoulder,1,2;157,Strained shoulder,2,3;";
                        string QBmajor = "219,Torn rotator cuff,254;220,Torn shoulder,169;";
                        //General Inj to all players other than qb
                        //string GeneralMinor = "0,Bruised ankle,1,2;1,Sprained ankle,2,5;3,Strained forearm,1,2;4,Strained bicep,1,3;5,Strained tricep,1,4;6,Upper arm buise,1,1;7,Back spasms,2,3;" +
                        //"9,Elbow bursitis,2,3;10,Foot contusion,1,1;11,Foot sprain,1,1;13,Broken finger,1,1;15,Broken thumb,2,3;16,Dislocated thumb,1,1;17,Dislocated wrist,4,5;18,Sprained wrist,1,2;" +
                        //"19,Pinched nerve,1,1;20,Hip bursitis,2,2;22,Knee bursitis,1,3;23,Strained calf,1,2;25,Pulled hamstring,2,5;26,Bruised quadricep,1,1;27,Strained quadricep,1,3;28,Abdominal strain,1,1;" +
                        //"29,Bruised ribs,2,4;30,Bruised sternum,1,2;31,Strained pectoral,2,3;115,Pulled groin,3,6;113,Strained knee,2,5;114,Strained calf,1,4;";
                        string GeneralMajor = "95,Turf toe,152;108,ACL sprain,107;111,MCL sprain,88;112,PCL sprain,88;158,High ankle sprain,145;159,Broken ankle,167;160,Dislocated ankle,88;161,Forearm fracture,68;" +
                        "162,Torn bicep,145;163,Torn tricep,174;180,Torn groin,179;181,Torn hamstring,174;182,Torn quadricep,212;203,Complete ACL tear,254;204,Partially torn ACL,218;205,Knee cartilage tear,84;" +
                        "209,Partially torn MCL,84;210,Complete PCL tear,254;211,Partially torn PCL,204;227,Fractured patella,229;228,Complete MCL tear,254;229,Complete PCL tear,254;";

                       
                        string[] INJURYbreakdown = GeneralMajor.Split(';'); ;
                        string[] INJURYdescription;
                        int InjurySelector = 0;

                        if (Major == true)
                        {
                            if (Pos == "QB")
                            {
                                INJURYbreakdown = QBmajor.Split(';');
                            }
                            else if (Pos != "QB")
                            {
                                INJURYbreakdown = GeneralMajor.Split(';');
                            }

                            InjurySelector = (int)((INJURYbreakdown.Length - 1) * random.NextDouble());
                            INJURYdescription = INJURYbreakdown[InjurySelector].Split(',');

                            InjuryRecord injRec = null;
                            injRec = model.PlayerModel.CreateNewInjuryRecord();

                            injRec.PlayerId = valObject.PlayerId;
                            injRec.TeamId = valObject.TeamId;
                            injRec.InjuryLength = int.Parse(INJURYdescription[2]); ;
                            injRec.InjuryReserve = false;
                            injRec.InjuryType = int.Parse(INJURYdescription[0]);
                            InjuryType = ";" + INJURYdescription[1];

                            if (int.Parse(INJURYdescription[2]) <= 83)
                            {
                                InjuryLength = 21;//"3 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 103)
                            {
                                InjuryLength = 28;// "4 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 123)
                            {
                                InjuryLength = 35;//"5 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 143)
                            {
                                InjuryLength = 42;//"6 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 163)
                            {
                                InjuryLength = 49;//"7 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 183)
                            {
                                InjuryLength = 56;//"8 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 203)
                            {
                                InjuryLength = 63;//"9 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 223)
                            {
                                InjuryLength = 70;//"10 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 243)
                            {
                                InjuryLength = 77;//"11 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 253)
                            {
                                InjuryLength = 84;//"12 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) > 253)
                            {
                                InjuryLength = 91;//"Out for Season";
                            }

                        }
                        else if (Minor == true)
                        {
                            
                        //time missed   

                            DaysActive = (int)(8 * random.NextDouble() + 5);

                            StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury"), true);
                            sw.WriteLine(valObject.FirstName + " " + valObject.LastName + ", missed " + (15 - DaysActive) + " days of camp due to injury.");                            
                            sw.Close();

                        }

                        if (Major == true)
                        {
                            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\"))
                            {
                                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\");

                            }

                            StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury"), true);
                            if (InjuryLength == 91)
                            {
                                sw.WriteLine(valObject.FirstName + " " + valObject.LastName + ", " + InjuryType + ", out for season");

                            }
                            else if (InjuryLength != 91)
                            {
                                sw.WriteLine(valObject.FirstName + " " + valObject.LastName + ", " + InjuryType + ", " + InjuryLength);
                            }
                            sw.Close();
                        }

                    }


                    //string Reload = (CurAct[i] + "," + wgt + "," + spd + "," + acc + "," + agi + "," + str + "," + stm + "," + inj + 
                //             tgh + "," + mor + "," + awr + "," + cat + "," + car + "," + jmp + "," + btk + "," + tkl + "," + thp + "," 
                            // tha + "," + pbk + ","  rbk + "," + kp + "," + ka + "," + kr + "," + injChance);
                //TuneContentsLine[]

                    if (Major == false)
                    {
                        decimal ActiveModifer = Math.Round(((decimal)DaysActive / 14), 2);
                        string[] lineSplit = TuneContentsLine[0].Split(',');
                        //Positional drill effects
                        decimal PCM = 0;


                        PCM = CoachPositionalDrillModifier;

                        if (Pos == "QB")
                        {
                            lineSplit = TuneContentsLine[0].Split(',');
                        }
                        else if (Pos == "HB")
                        {
                            lineSplit = TuneContentsLine[1].Split(',');
                        }
                        else if (Pos == "FB")
                        {
                            lineSplit = TuneContentsLine[2].Split(',');
                        }
                        else if (Pos == "WR")
                        {
                            lineSplit = TuneContentsLine[3].Split(',');
                        }
                        else if (Pos == "TE")
                        {
                            lineSplit = TuneContentsLine[4].Split(',');
                        }
                        else if (Pos == "OL")
                        {
                            lineSplit = TuneContentsLine[5].Split(',');
                        }
                        else if (Pos == "DL")
                        {
                            lineSplit = TuneContentsLine[6].Split(',');
                        }
                        else if (Pos == "LB")
                        {
                            lineSplit = TuneContentsLine[7].Split(',');
                        }
                        else if (Pos == "DB")
                        {
                            lineSplit = TuneContentsLine[8].Split(',');
                        }
                        else if (Pos == "KP")
                        {
                            lineSplit = TuneContentsLine[9].Split(',');
                        }


                        if (decimal.Parse(lineSplit[1]) != 0)
                        {
                            wgt = Math.Round((((((decimal.Parse(lineSplit[1]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[2]) != 0)
                        {
                            spd = Math.Round((((((decimal.Parse(lineSplit[2]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[3]) != 0)
                        {
                            acc = Math.Round((((((decimal.Parse(lineSplit[3]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[4]) != 0)
                        {
                            agi = Math.Round((((((decimal.Parse(lineSplit[4]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[5]) != 0)
                        {
                            str = Math.Round((((((decimal.Parse(lineSplit[5]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[6]) != 0)
                        {
                            stm = Math.Round((((((decimal.Parse(lineSplit[6]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[7]) != 0)
                        {
                            inj = Math.Round((((((decimal.Parse(lineSplit[7]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[8]) != 0)
                        {
                            tgh = Math.Round((((((decimal.Parse(lineSplit[8]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //if (decimal.Parse(lineSplit[9]) != 0)
                        //{
                        //    mor = Math.Round((((((decimal.Parse(lineSplit[9]) / 10) ) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        //}
                        if (decimal.Parse(lineSplit[10]) != 0)
                        {
                            awr = Math.Round((((((decimal.Parse(lineSplit[10]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[11]) != 0)
                        {
                            cat = Math.Round((((((decimal.Parse(lineSplit[11]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[12]) != 0)
                        {
                            car = Math.Round((((((decimal.Parse(lineSplit[12]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[13]) != 0)
                        {
                            jmp = Math.Round((((((decimal.Parse(lineSplit[13]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[14]) != 0)
                        {
                            btk = Math.Round((((((decimal.Parse(lineSplit[14]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[15]) != 0)
                        {
                            tkl = Math.Round((((((decimal.Parse(lineSplit[15]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[16]) != 0)
                        {
                            thp = Math.Round((((((decimal.Parse(lineSplit[16]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[17]) != 0)
                        {
                            tha = Math.Round((((((decimal.Parse(lineSplit[17]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[18]) != 0)
                        {
                            pbk = Math.Round((((((decimal.Parse(lineSplit[18]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[19]) != 0)
                        {
                            rbk = Math.Round((((((decimal.Parse(lineSplit[19]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[20]) != 0)
                        {
                            kp = Math.Round((((((decimal.Parse(lineSplit[20]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[21]) != 0)
                        {
                            ka = Math.Round((((((decimal.Parse(lineSplit[21]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[22]) != 0)
                        {
                            kr = Math.Round((((((decimal.Parse(lineSplit[22]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }

                        //Cardio
                        lineSplit = TuneContentsLine[10].Split(',');
                        if (decimal.Parse(lineSplit[1]) != 0)
                        {
                            wgt = wgt + Math.Round((((((decimal.Parse(lineSplit[1]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[2]) != 0)
                        {
                            spd = spd + Math.Round((((((decimal.Parse(lineSplit[2]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[3]) != 0)
                        {
                            acc = acc + Math.Round((((((decimal.Parse(lineSplit[3]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[4]) != 0)
                        {
                            agi = agi + Math.Round((((((decimal.Parse(lineSplit[4]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[5]) != 0)
                        {
                            str = str + Math.Round((((((decimal.Parse(lineSplit[5]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[6]) != 0)
                        {
                            stm = stm + Math.Round((((((decimal.Parse(lineSplit[6]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[7]) != 0)
                        {
                            inj = inj + Math.Round((((((decimal.Parse(lineSplit[7]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[8]) != 0)
                        {
                            tgh = tgh + Math.Round((((((decimal.Parse(lineSplit[8]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //if (decimal.Parse(lineSplit[9]) != 0)
                        //{
                        //    mor = mor + Math.Round((((((decimal.Parse(lineSplit[9]) / 10) ) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        //}
                        if (decimal.Parse(lineSplit[10]) != 0)
                        {
                            awr = awr + Math.Round((((((decimal.Parse(lineSplit[10]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[11]) != 0)
                        {
                            cat = cat + Math.Round((((((decimal.Parse(lineSplit[11]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[12]) != 0)
                        {
                            car = car + Math.Round((((((decimal.Parse(lineSplit[12]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[13]) != 0)
                        {
                            jmp = jmp + Math.Round((((((decimal.Parse(lineSplit[13]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[14]) != 0)
                        {
                            btk = btk + Math.Round((((((decimal.Parse(lineSplit[14]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[15]) != 0)
                        {
                            tkl = tkl + Math.Round((((((decimal.Parse(lineSplit[15]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[16]) != 0)
                        {
                            thp = thp + Math.Round((((((decimal.Parse(lineSplit[16]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[17]) != 0)
                        {
                            tha = tha + Math.Round((((((decimal.Parse(lineSplit[17]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[18]) != 0)
                        {
                            pbk = pbk + Math.Round((((((decimal.Parse(lineSplit[18]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[19]) != 0)
                        {
                            rbk = rbk + Math.Round((((((decimal.Parse(lineSplit[19]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[20]) != 0)
                        {
                            kp = kp + Math.Round((((((decimal.Parse(lineSplit[20]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[21]) != 0)
                        {
                            ka = ka + Math.Round((((((decimal.Parse(lineSplit[21]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[22]) != 0)
                        {
                            kr = kr + Math.Round((((((decimal.Parse(lineSplit[22]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //diet
                        lineSplit = TuneContentsLine[11].Split(',');
                        if (decimal.Parse(lineSplit[1]) != 0)
                        {
                            wgt = wgt + Math.Round((((((decimal.Parse(lineSplit[1]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[2]) != 0)
                        {
                            spd = spd + Math.Round((((((decimal.Parse(lineSplit[2]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[3]) != 0)
                        {
                            acc = acc + Math.Round((((((decimal.Parse(lineSplit[3]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[4]) != 0)
                        {
                            agi = agi + Math.Round((((((decimal.Parse(lineSplit[4]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[5]) != 0)
                        {
                            str = str + Math.Round((((((decimal.Parse(lineSplit[5]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[6]) != 0)
                        {
                            stm = stm + Math.Round((((((decimal.Parse(lineSplit[6]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[7]) != 0)
                        {
                            inj = inj + Math.Round((((((decimal.Parse(lineSplit[7]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[8]) != 0)
                        {
                            tgh = tgh + Math.Round((((((decimal.Parse(lineSplit[8]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //if (decimal.Parse(lineSplit[9]) != 0)
                        //{
                        //    mor = mor + Math.Round((((((decimal.Parse(lineSplit[9]) / 10)) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        //}
                        if (decimal.Parse(lineSplit[10]) != 0)
                        {
                            awr = awr + Math.Round((((((decimal.Parse(lineSplit[10]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[11]) != 0)
                        {
                            cat = cat + Math.Round((((((decimal.Parse(lineSplit[11]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[12]) != 0)
                        {
                            car = car + Math.Round((((((decimal.Parse(lineSplit[12]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[13]) != 0)
                        {
                            jmp = jmp + Math.Round((((((decimal.Parse(lineSplit[13]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[14]) != 0)
                        {
                            btk = btk + Math.Round((((((decimal.Parse(lineSplit[14]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[15]) != 0)
                        {
                            tkl = tkl + Math.Round((((((decimal.Parse(lineSplit[15]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[16]) != 0)
                        {
                            thp = thp + Math.Round((((((decimal.Parse(lineSplit[16]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[17]) != 0)
                        {
                            tha = tha + Math.Round((((((decimal.Parse(lineSplit[17]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[18]) != 0)
                        {
                            pbk = pbk + Math.Round((((((decimal.Parse(lineSplit[18]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[19]) != 0)
                        {
                            rbk = rbk + Math.Round((((((decimal.Parse(lineSplit[19]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[20]) != 0)
                        {
                            kp = kp + Math.Round((((((decimal.Parse(lineSplit[20]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[21]) != 0)
                        {
                            ka = ka + Math.Round((((((decimal.Parse(lineSplit[21]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[22]) != 0)
                        {
                            kr = kr + Math.Round((((((decimal.Parse(lineSplit[22]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //weights
                        lineSplit = TuneContentsLine[12].Split(',');
                        if (decimal.Parse(lineSplit[1]) != 0)
                        {
                            wgt = wgt + Math.Round((((((decimal.Parse(lineSplit[1]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[2]) != 0)
                        {
                            spd = spd + Math.Round((((((decimal.Parse(lineSplit[2]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[3]) != 0)
                        {
                            acc = acc + Math.Round((((((decimal.Parse(lineSplit[3]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[4]) != 0)
                        {
                            agi = agi + Math.Round((((((decimal.Parse(lineSplit[4]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[5]) != 0)
                        {
                            str = str + Math.Round((((((decimal.Parse(lineSplit[5]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[6]) != 0)
                        {
                            stm = stm + Math.Round((((((decimal.Parse(lineSplit[6]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[7]) != 0)
                        {
                            inj = inj + Math.Round((((((decimal.Parse(lineSplit[7]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[8]) != 0)
                        {
                            tgh = tgh + Math.Round((((((decimal.Parse(lineSplit[8]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        //if (decimal.Parse(lineSplit[9]) != 0)
                        //{
                        //    mor = mor + Math.Round((((((decimal.Parse(lineSplit[9]) / 10)) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        //}
                        if (decimal.Parse(lineSplit[10]) != 0)
                        {
                            awr = awr + Math.Round((((((decimal.Parse(lineSplit[10]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[11]) != 0)
                        {
                            cat = cat + Math.Round((((((decimal.Parse(lineSplit[11]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[12]) != 0)
                        {
                            car = car + Math.Round((((((decimal.Parse(lineSplit[12]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[13]) != 0)
                        {
                            jmp = jmp + Math.Round((((((decimal.Parse(lineSplit[13]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[14]) != 0)
                        {
                            btk = btk + Math.Round((((((decimal.Parse(lineSplit[14]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[15]) != 0)
                        {
                            tkl = tkl + Math.Round((((((decimal.Parse(lineSplit[15]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[16]) != 0)
                        {
                            thp = thp + Math.Round((((((decimal.Parse(lineSplit[16]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[17]) != 0)
                        {
                            tha = tha + Math.Round((((((decimal.Parse(lineSplit[17]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[18]) != 0)
                        {
                            pbk = pbk + Math.Round((((((decimal.Parse(lineSplit[18]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[19]) != 0)
                        {
                            rbk = rbk + Math.Round((((((decimal.Parse(lineSplit[19]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[20]) != 0)
                        {
                            kp = kp + Math.Round((((((decimal.Parse(lineSplit[20]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[21]) != 0)
                        {
                            ka = ka + Math.Round((((((decimal.Parse(lineSplit[21]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }
                        if (decimal.Parse(lineSplit[22]) != 0)
                        {
                            kr = kr + Math.Round((((((decimal.Parse(lineSplit[22]) / 1900) * 25) + ((decimal)PCM)) * 14) * ActiveModifer), 4);
                        }


                        //TeamDrills if day >= 8
                        int TeamDrillHalf = 13;
                        int TeamDrillLive = 7;

                        //Positional Bonuses
                        if (Pos == "QB")
                        {
                            tha = tha + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            tha = tha + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "HB")
                        {
                            car = car + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            btk = btk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            car = car + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            btk = btk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "FB")
                        {
                            rbk = rbk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            pbk = pbk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            rbk = rbk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            pbk = pbk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "TE")
                        {
                            cat = cat + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            rbk = rbk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            cat = cat + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            rbk = rbk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "WR")
                        {
                            cat = cat + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            cat = cat + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "OL")
                        {
                            rbk = rbk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            pbk = pbk + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            rbk = rbk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            pbk = pbk + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "DL")
                        {
                            acc = acc + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            acc = acc + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "LB")
                        {
                            awr = awr + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "DB")
                        {
                            awr = awr + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            awr = awr + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            tkl = tkl + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }
                        else if (Pos == "KP")
                        {
                            kp = kp + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            ka = ka + Math.Round(((((decimal)TeamDrillHalf) / 100) * 7), 4);
                            kp = kp + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                            ka = ka + Math.Round(((((decimal)TeamDrillLive) / 50) * 7), 4);
                        }




                        //Exp modifier  

                        if ((valObject.YearsPro <= 4) & (Pos != "QB"))
                        {
                            if (valObject.YearsPro == 0)
                            {
                                awr = awr + Math.Round((((decimal)Film / 100)), 4);
                            }
                            else if (valObject.YearsPro == 1)
                            {
                                awr = awr + Math.Round((((decimal)Film / 120)), 4);
                            }
                            else if (valObject.YearsPro == 2)
                            {
                                awr = awr + Math.Round((((decimal)Film / 140)), 4);
                            }
                            else if (valObject.YearsPro == 3)
                            {
                                awr = awr + Math.Round((((decimal)Film / 160)), 4);
                            }
                            else if (valObject.YearsPro == 4)
                            {
                                awr = awr + Math.Round((((decimal)Film / 200)), 4);
                            }
                        }
                        else if ((valObject.YearsPro > 4) & (Pos != "QB"))
                        {
                            awr = awr + Math.Round((((decimal)Film / 400)), 4);
                        }

                        if (Pos == "KP")
                        {
                            ka = ka + Math.Round((((decimal)Special / 400)), 4);
                            kp = kp + Math.Round((((decimal)Special / 400)), 4);
                        }


                        if ((valObject.YearsPro <= 4) & (Pos == "QB"))
                        {
                            if (valObject.YearsPro == 0)
                            {
                                awr = awr + Math.Round((((decimal)Film / 1000)), 4);
                            }
                            else if (valObject.YearsPro == 1)
                            {
                                awr = awr + Math.Round((((decimal)Film / 800)), 4);
                            }
                            else if (valObject.YearsPro == 2)
                            {
                                awr = awr + Math.Round((((decimal)Film / 600)), 4);
                            }
                            else if (valObject.YearsPro == 3)
                            {
                                awr = awr + Math.Round((((decimal)Film / 300)), 4);
                            }
                            else if (valObject.YearsPro == 4)
                            {
                                awr = awr + Math.Round((((decimal)Film / 120)), 4);
                            }
                        }
                        else if ((valObject.YearsPro > 4) & (Pos == "QB"))
                        {
                            awr = awr + Math.Round((((decimal)Film / 90)), 4);
                        }


                    }

                    

                        

                       //end player loop and process results

          
                
                //Process daily results
                           
                            // injchance = injchance + ((decimal.Parse(TotalsContentsLine[25]) + decimal.Parse(TuneFileAttributeMods[23]) + CurrentMoraleEffect) + ((decimal)PCM) + WthInjIncrease);
                            //old ratings

                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            int ovrO = valObject.Overall;
                            int wgtO = valObject.Weight + 160;
                            int spdO = valObject.Speed;
                            int accO = valObject.Acceleration;
                            int agiO = valObject.Agility;
                            int strO = valObject.Strength;
                            int stmO = valObject.Stamina;
                            int injO = valObject.Injury;
                            int tghO = valObject.Toughness;
                            int morO = valObject.Morale;
                            int awrO = valObject.Awareness;
                            int catO = valObject.Catching;
                            int carO = valObject.Carrying;
                            int jmpO = valObject.Jumping;
                            int btkO = valObject.BreakTackle;
                            int tklO = valObject.Tackle;
                            int thpO = valObject.ThrowPower;
                            int thaO = valObject.ThrowAccuracy;
                            int pbkO = valObject.PassBlocking;
                            int rbkO = valObject.RunBlocking;
                            int kpO = valObject.KickPower;
                            int kaO = valObject.KickAccuracy;
                            int krO = valObject.KickReturn;



                            //Maximum raise to attribute based on age
                            decimal MaxRaise = 1;
                            if (valObject.Age >= 30)
                            {
                                MaxRaise = Math.Round(((((decimal)valObject.Age - 29) / 100) + 1), 2);
                            }
                            //valObject.Speed = (int)(valObject.Speed * Drop);
                            //valObject.Acceleration = (int)(valObject.Acceleration * Drop);
                            //valObject.Agility = (int)(valObject.Agility * Drop);
                            //valObject.Strength = (int)(valObject.Strength * Drop);
                            //valObject.Injury = (int)(valObject.Injury * Drop);
                            //valObject.Jumping = (int)(valObject.Jumping * Drop);
                            //valObject.BreakTackle = (int)(valObject.BreakTackle * Drop);
                            //valObject.Tackle = (int)(valObject.Tackle * Drop);
                            //valObject.ThrowPower = (int)(valObject.ThrowPower * Drop);
                            //valObject.KickPower = (int)(valObject.KickPower * Drop);


                            AttributeMeans(valObject.PositionId);
                            decimal AttributeDifferential = 0;
                            decimal PercentModMuscle = 0;
                            decimal PercentModFat = 0;
                            int addedwgt = 0;

                            //weight control

                            if (wgt > 0)
                            {
                                addedwgt = (int)(wgtO + (Math.Round(((decimal)wgt * (decimal)PosWgtMod), 0)));
                                if (addedwgt > wgtO)
                                {
                                    wgt = 0;
                                }
                                PercentModFat = (int)(Math.Round(((decimal)wgtO / (decimal)addedwgt), 3));
                                PercentModMuscle = (int)(Math.Round(((decimal)addedwgt / (decimal)wgtO), 3));
                                valObject.Weight = addedwgt - 160;
                                Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                                HighLowCheck(Rating);
                                valObject.BodyMuscle = Rating;
                                Rating = (int)(Math.Ceiling((valObject.BodyFat * (PercentModFat * (decimal)1.35))));
                                HighLowCheck(Rating);
                                valObject.BodyFat = Rating;
                                Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.ArmsMuscle = Rating;
                                Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.ArmsFat = Rating;
                                Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.LegsThighMuscle = Rating;
                                Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.LegsThighFat = Rating;
                                Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.LegsCalfMuscle = Rating;
                                Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.LegsCalfFat = Rating;
                                Rating = (int)(Math.Ceiling((valObject.BodyOverall * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.BodyOverall = Rating;
                            }
                            else if (wgt < 0)
                            {
                                addedwgt = (int)(wgtO + (Math.Round(((decimal)wgt * (decimal)PosWgtMod), 0)));
                                if (addedwgt < wgtO)
                                {
                                    wgt = 0;
                                }
                                PercentModFat = Math.Round(((decimal)addedwgt / (decimal)wgtO), 3);
                                PercentModMuscle = Math.Round(((decimal)wgtO / (decimal)addedwgt), 3);
                                valObject.Weight = addedwgt - 160;
                                Rating = (int)(Math.Ceiling(valObject.BodyMuscle * PercentModMuscle));
                                HighLowCheck(Rating);
                                valObject.BodyMuscle = Rating;
                                Rating = (int)(Math.Floor(valObject.BodyFat * (PercentModFat * (decimal)0.65)));
                                HighLowCheck(Rating);
                                valObject.BodyFat = Rating;
                                Rating = (int)(Math.Ceiling((valObject.ArmsMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.ArmsMuscle = Rating;
                                Rating = (int)(Math.Floor((valObject.ArmsFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.ArmsFat = Rating;
                                Rating = (int)(Math.Ceiling((valObject.LegsThighMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.LegsThighMuscle = Rating;
                                Rating = (int)(Math.Floor((valObject.LegsThighFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.LegsThighFat = Rating;
                                Rating = (int)(Math.Ceiling((valObject.LegsCalfMuscle * PercentModMuscle)));
                                HighLowCheck(Rating);
                                valObject.LegsCalfMuscle = Rating;
                                Rating = (int)(Math.Floor((valObject.LegsCalfFat * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.LegsCalfFat = Rating;
                                Rating = (int)(Math.Floor((valObject.BodyOverall * PercentModFat)));
                                HighLowCheck(Rating);
                                valObject.BodyOverall = Rating;

                            }
                            decimal ExpMod = 1;
                            if (valObject.YearsPro <= 3)
                            {
                                ExpMod = Math.Round((((((decimal)valObject.YearsPro - 4) / 4) / 10) + 1), 2);
                            }
                            else if (valObject.YearsPro >= 4)
                            {
                                ExpMod = Math.Round(((((decimal)valObject.YearsPro - 4) / 100) + 1), 2);
                            }
                            //speed
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Speed / (decimal)SpdMean))), 3);
                            if (valObject.Speed > SpdMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Speed + (Math.Round((AttributeDifferential * (decimal)(spd)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Speed)
                            {
                                spd = 0;
                            }
                            valObject.Speed = Rating;
                            if (valObject.Speed > (int)(Math.Round(((decimal)spdO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Speed = (int)(Math.Round(((decimal)spdO * MaxRaise), 0));
                            }

                            //accel
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Acceleration / (decimal)AccMean))), 3);
                            if (valObject.Acceleration > AccMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Acceleration + (Math.Round((AttributeDifferential * (decimal)(acc)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Acceleration)
                            {
                                acc = 0;
                            }
                            valObject.Acceleration = Rating;
                            if (valObject.Acceleration > (int)(Math.Round(((decimal)accO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Acceleration = (int)(Math.Round(((decimal)accO * MaxRaise), 0));
                            }


                            //agi
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Agility / (decimal)AgiMean))), 3);
                            if (valObject.Agility > AgiMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Agility + (Math.Round((AttributeDifferential * (decimal)(agi)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Agility)
                            {
                                agi = 0;
                            }
                            valObject.Agility = Rating;
                            if (valObject.Agility > (int)(Math.Round(((decimal)agiO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Agility = (int)(Math.Round(((decimal)agiO * MaxRaise), 0));
                            }

                            //str
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Strength / (decimal)StrMean))), 3);
                            if (valObject.Strength > StrMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Strength + (Math.Round((AttributeDifferential * (decimal)(str)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Strength)
                            {
                                str = 0;
                            }
                            valObject.Strength = Rating;
                            if (valObject.Strength > (int)(Math.Round(((decimal)strO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Strength = (int)(Math.Round(((decimal)strO * MaxRaise), 0));
                            }


                            //stm
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Stamina / (decimal)StmMean))), 3);
                            if (valObject.Stamina > StmMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Stamina + (Math.Round((AttributeDifferential * (decimal)(stm)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Stamina)
                            {
                                stm = 0;
                            }
                            valObject.Stamina = Rating;

                            //inj
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Injury / (decimal)InjMean))), 3);
                            if (valObject.Injury > InjMean)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Injury + (Math.Round((AttributeDifferential * (decimal)(inj)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Injury)
                            {
                                inj = 0;
                            }
                            valObject.Injury = Rating;

                            if (valObject.Injury > (int)(Math.Round(((decimal)injO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Injury = (int)(Math.Round(((decimal)injO * MaxRaise), 0));
                            }


                            //tough
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Toughness / (decimal)99))), 3);
                            Rating = (int)(Math.Round((valObject.Toughness + (Math.Round((AttributeDifferential * (decimal)(tgh)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Toughness)
                            {
                                tgh = 0;
                            }
                            valObject.Toughness = Rating;

                            //mor
                            //    AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Acceleration / (decimal)AccMean))), 3);
                            //   Rating = (int)(Math.Round((AttributeDifferential * (decimal)(acc)), 0));
                            //    HighLowCheck(Rating);
                            double MrRand = (15 * random.NextDouble());
                            Rating = valObject.Morale - (int)MrRand;
                            HighLowCheck(Rating);
                            valObject.Morale = Rating;
                            //awar
                            AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Awareness / (decimal)99))), 3);
                            Rating = (int)(Math.Round((valObject.Awareness + (Math.Round((AttributeDifferential * (decimal)(awr)), 0))), 0));
                            //if (valObject.YearsPro <= 3)
                            //{
                            //    valObject.Awareness = Rating + (int)((valObject.YearsPro - 4) * (Math.Round(((double)DaysActive / 7), 2)));
                            //}
                            if (Rating > valObject.Awareness)
                            {
                                awr = 0;
                            }
                            valObject.Awareness = Rating;


                            //cat
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Catching * ExpMod) / (decimal)90))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Catching + (Math.Round((AttributeDifferential * (decimal)(cat)), 0))), 0));
                            HighLowCheck(Rating);
                            valObject.Catching = Rating;

                            //car
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Carrying * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Carrying + (Math.Round((AttributeDifferential * (decimal)(car)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Carrying)
                            {
                                car = 0;
                            }
                            valObject.Carrying = Rating;


                            //jmp
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Jumping * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Jumping + (Math.Round((AttributeDifferential * (decimal)(jmp)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Jumping)
                            {
                                jmp = 0;
                            }
                            valObject.Jumping = Rating;
                            if (valObject.Jumping > (int)(Math.Round(((decimal)jmpO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Jumping = (int)(Math.Round(((decimal)jmpO * MaxRaise), 0));
                            }


                            //btk
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.BreakTackle * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.BreakTackle + (Math.Round((AttributeDifferential * (decimal)(btk)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.BreakTackle)
                            {
                                btk = 0;
                            }
                            valObject.BreakTackle = Rating;
                            if (valObject.BreakTackle > (int)(Math.Round(((decimal)btkO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.BreakTackle = (int)(Math.Round(((decimal)btkO * MaxRaise), 0));
                            }
                            //tkl
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Tackle * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.Tackle + (Math.Round((AttributeDifferential * (decimal)(tkl)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.Tackle)
                            {
                                tkl = 0;
                            }
                            valObject.Tackle = Rating;
                            if (valObject.Tackle > (int)(Math.Round(((decimal)tklO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.Tackle = (int)(Math.Round(((decimal)tklO * MaxRaise), 0));
                            }
                            //thp
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.ThrowPower * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.ThrowPower + (Math.Round((AttributeDifferential * (decimal)(thp)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.ThrowPower)
                            {
                                thp = 0;
                            }
                            valObject.ThrowPower = Rating;
                            if (valObject.ThrowPower > (int)(Math.Round(((decimal)thpO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.ThrowPower = (int)(Math.Round(((decimal)thpO * MaxRaise), 0));
                            }
                            //tha
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.ThrowAccuracy * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.ThrowAccuracy + (Math.Round((AttributeDifferential * (decimal)(tha)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.ThrowAccuracy)
                            {
                                tha = 0;
                            }
                            valObject.ThrowAccuracy = Rating;
                            //pbk
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.PassBlocking * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.PassBlocking + (Math.Round((AttributeDifferential * (decimal)(pbk)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.PassBlocking)
                            {
                                pbk = 0;
                            }
                            valObject.PassBlocking = Rating;
                            //rbk
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.RunBlocking * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.RunBlocking + (Math.Round((AttributeDifferential * (decimal)(rbk)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.RunBlocking)
                            {
                                rbk = 0;
                            }
                            valObject.RunBlocking = Rating;
                            //kp
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickPower * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.KickPower + (Math.Round((AttributeDifferential * (decimal)(kp)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.KickPower)
                            {
                                kp = 0;
                            }
                            valObject.KickPower = Rating;
                            if (valObject.KickPower > (int)(Math.Round(((decimal)kpO * MaxRaise), 0)) & (valObject.Age >= 30))
                            {
                                valObject.KickPower = (int)(Math.Round(((decimal)kpO * MaxRaise), 0));
                            }
                            //ka
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickAccuracy * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.KickAccuracy + (Math.Round((AttributeDifferential * (decimal)(ka)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.KickAccuracy)
                            {
                                ka = 0;
                            }
                            valObject.KickAccuracy = Rating;
                            //kr
                            AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickReturn * ExpMod) / (decimal)99))), 3);
                            if (AttributeDifferential > 1)
                            {
                                AttributeDifferential = (decimal).01;
                            }
                            Rating = (int)(Math.Round((valObject.KickReturn + (Math.Round((AttributeDifferential * (decimal)(kr)), 0))), 0));
                            HighLowCheck(Rating);
                            if (Rating > valObject.KickReturn)
                            {
                                kr = 0;
                            }
                            valObject.KickReturn = Rating;

                           

                            DataRow dr = RosterView.NewRow();
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName + " POST";
                            dr["Age"] = valObject.Age;
                            dr["Exp"] = valObject.YearsPro;
                            dr["Ovr"] = valObject.Overall;
                            // Would like to include feet/inches
                            dr["Hgt"] = valObject.Height;
                            dr["Wgt"] = valObject.Weight + 160;
                            dr["Spd"] = valObject.Speed;
                            dr["Acc"] = valObject.Acceleration;
                            dr["Agi"] = valObject.Agility;
                            dr["Str"] = valObject.Strength;
                            dr["Stm"] = valObject.Stamina;
                            dr["Inj"] = valObject.Injury;
                            dr["Tgh"] = valObject.Toughness;
                            dr["Mor"] = valObject.Morale;
                            dr["Awr"] = valObject.Awareness;
                            dr["Cat"] = valObject.Catching;
                            dr["Car"] = valObject.Carrying;
                            dr["Jmp"] = valObject.Jumping;
                            dr["Btk"] = valObject.BreakTackle;
                            dr["Tkl"] = valObject.Tackle;
                            dr["ThP"] = valObject.ThrowPower;
                            dr["ThA"] = valObject.ThrowAccuracy;
                            dr["Pbk"] = valObject.PassBlocking;
                            dr["Rbk"] = valObject.RunBlocking;
                            dr["KP"] = valObject.KickPower;
                            dr["KA"] = valObject.KickAccuracy;
                            dr["KR"] = valObject.KickReturn;

                            RosterView.Rows.Add(dr);
                         

                            DataRow af = RosterView.NewRow();
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            af["Name"] = valObject.FirstName + " " + valObject.LastName + " PRE";
                            af["Age"] = valObject.Age;
                            af["Exp"] = valObject.YearsPro;
                            af["Ovr"] = ovrO;
                            // Would like to include feet/inches
                            af["Hgt"] = valObject.Height;
                            af["Wgt"] = wgtO;
                            af["Spd"] = spdO;
                            af["Acc"] = accO;
                            af["Agi"] = agiO;
                            af["Str"] = strO;
                            af["Stm"] = stmO;
                            af["Inj"] = injO;
                            af["Tgh"] = tghO;
                            af["Mor"] = morO;
                            af["Awr"] = awrO;
                            af["Cat"] = catO;
                            af["Car"] = carO;
                            af["Jmp"] = jmpO;
                            af["Btk"] = btkO;
                            af["Tkl"] = tklO;
                            af["ThP"] = thpO;
                            af["ThA"] = thaO;
                            af["Pbk"] = pbkO;
                            af["Rbk"] = rbkO;
                            af["KP"] = kpO;
                            af["KA"] = kaO;
                            af["KR"] = krO;

                            RosterView.Rows.Add(af);

                          

                            DataRow di = RosterView.NewRow();
                            // playerHeightComboBox.SelectedIndex = record.Height - 65;
                            valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                            //Reload the overall rating
                            valObject.Overall = valObject.Overall;
                            di["Name"] = valObject.FirstName + " " + valObject.LastName + " VARIANCE";
                            di["Age"] = valObject.Age;
                            di["Exp"] = valObject.YearsPro;
                            di["Ovr"] = valObject.Overall - ovrO;
                            // Would like to include feet/inches
                            di["Hgt"] = valObject.Height;
                            di["Wgt"] = (valObject.Weight + 160) - (wgtO);
                            di["Spd"] = valObject.Speed - spdO;
                            di["Acc"] = valObject.Acceleration - accO;
                            di["Agi"] = valObject.Agility - agiO;
                            di["Str"] = valObject.Strength - strO;
                            di["Stm"] = valObject.Stamina - stmO;
                            di["Inj"] = valObject.Injury - injO;
                            di["Tgh"] = valObject.Toughness - tghO;
                            di["Mor"] = valObject.Morale - morO;
                            di["Awr"] = valObject.Awareness - awrO;
                            di["Cat"] = valObject.Catching - catO;
                            di["Car"] = valObject.Carrying - carO;
                            di["Jmp"] = valObject.Jumping - jmpO;
                            di["Btk"] = valObject.BreakTackle - btkO;
                            di["Tkl"] = valObject.Tackle - tklO;
                            di["ThP"] = valObject.ThrowPower - thpO;
                            di["ThA"] = valObject.ThrowAccuracy - thaO;
                            di["Pbk"] = valObject.PassBlocking - pbkO;
                            di["Rbk"] = valObject.RunBlocking - rbkO;
                            di["KP"] = valObject.KickPower - kpO;
                            di["KA"] = valObject.KickAccuracy - kaO;
                            di["KR"] = valObject.KickReturn - krO;

                            RosterView.Rows.Add(di);


                /*
                            DataRow bg = RosterView.NewRow();

                            bg["Name"] = "--------------------";
                            bg["Age"] = "";
                            bg["Exp"] = "";
                            bg["Ovr"] = "";
                            // Would like to include feet/inches
                            bg["Hgt"] = "";
                            bg["Wgt"] = "";
                            bg["Spd"] = "";
                            bg["Acc"] = "";
                            bg["Agi"] = "";
                            bg["Str"] = "";
                            bg["Stm"] = "";
                            bg["Inj"] = "";
                            bg["Tgh"] = "";
                            bg["Mor"] = "";
                            bg["Awr"] = "";
                            bg["Cat"] = "";
                            bg["Car"] = "";
                            bg["Jmp"] = "";
                            bg["Btk"] = "";
                            bg["Tkl"] = "";
                            bg["ThP"] = "";
                            bg["ThA"] = "";
                            bg["Pbk"] = "";
                            bg["Rbk"] = "";
                            bg["KP"] = "";
                            bg["KA"] = "";
                            bg["KR"] = "";

                            RosterView.Rows.Add(bg);

                */
                          
            }

            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimulation\\"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimulation\\");
            }


            // Create the CSV file to which grid data will be exported.
            StreamWriter sw2 = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimulation\\" + CPUteam + ".txt"), true);
            // First we will write the headers.
            DataTable dt = RosterView;
            int iColCount = dt.Columns.Count;
            sw2.Write(dt.Columns[0]);
            sw2.Write("\t\t\t");
            for (int i = 1; i < iColCount; i++)
            {
                sw2.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw2.Write("\t");
                }
            }
            sw2.Write(sw2.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw2.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw2.Write("\t");
                    }
                }
                sw2.Write(sw2.NewLine);
                sw2.WriteLine();

            }
            sw2.Close();
            RosterView.Clear();
            StreamReader sr1 = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury");
            string injCnts = sr1.ReadToEnd();
            sr1.Close();

            StreamWriter sw3 = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimulation\\" + CPUteam + ".txt"), true);
            sw3.WriteLine(injCnts);
            sw3.Close();

               
                
               
                

        }
          
        private void CPUtuneCompile()
        {
            installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\");
            }

          

            string Act = "QB-P,HB-P,FB-P,WR-P,TE-P,OL-P,DL-P,LB-P,DB-P,KP-P,A,D,W";
            string[] CurAct = Act.Split(',');          


           decimal wgt = 0;
           decimal spd = 0;
           decimal acc = 0;
           decimal agi = 0;
           decimal str = 0;
           decimal stm = 0;
           decimal inj = 0;
           decimal tgh = 0;
           decimal mor = 0;
           decimal awr = 0;
           decimal cat = 0;
           decimal car = 0;
           decimal jmp = 0;
           decimal btk = 0;
           decimal tkl = 0;
           decimal thp = 0;
           decimal tha = 0;
           decimal pbk = 0;
           decimal rbk = 0;
           decimal kp = 0;
           decimal ka = 0;
           decimal kr = 0;
           decimal injChance = 0;

            int wgtCnt = 0;
            int spdCnt = 0;
            int accCnt = 0;
            int agiCnt = 0;
            int strCnt = 0;
            int stmCnt = 0;
            int injCnt = 0;
            int tghCnt = 0;
            int morCnt = 0;
            int awrCnt = 0;
            int catCnt = 0;
            int carCnt = 0;
            int jmpCnt = 0;
            int btkCnt = 0;
            int tklCnt = 0;
            int thpCnt = 0;
            int thaCnt = 0;
            int pbkCnt = 0;
            int rbkCnt = 0;
            int kpCnt = 0;
            int kaCnt = 0;
            int krCnt = 0;
            int InjChanceCnt = 0;

                //tunefile
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");
                string TuneTotalsContents = sr.ReadToEnd();
                sr.Close();
                string[] TuneContentsLine = TuneTotalsContents.Split('\n');
                int TuneFileLen = TuneContentsLine.Length;
               // string Reload = "";  
                   int i = 0;
                   while (i <= 12)
                   {
                       wgt = 0;
                       spd = 0;
                       acc = 0;
                       agi = 0;
                       str = 0;
                       stm = 0;
                       inj = 0;
                       tgh = 0;
                       mor = 0;
                       awr = 0;
                       cat = 0;
                       car = 0;
                       jmp = 0;
                       btk = 0;
                       tkl = 0;
                       thp = 0;
                       tha = 0;
                       pbk = 0;
                       rbk = 0;
                       kp = 0;
                       ka = 0;
                       kr = 0;
                       injChance = 0;

                       wgtCnt = 0;
                       spdCnt = 0;
                       accCnt = 0;
                       agiCnt = 0;
                       strCnt = 0;
                       stmCnt = 0;
                       injCnt = 0;
                       tghCnt = 0;
                       morCnt = 0;
                       awrCnt = 0;
                       catCnt = 0;
                       carCnt = 0;
                       jmpCnt = 0;
                       btkCnt = 0;
                       tklCnt = 0;
                       thpCnt = 0;
                       thaCnt = 0;
                       pbkCnt = 0;
                       rbkCnt = 0;
                       kpCnt = 0;
                       kaCnt = 0;
                       krCnt = 0;
                       InjChanceCnt = 0;

                       for (int tuneLoop = 0; tuneLoop < (TuneFileLen - 1); tuneLoop++) //loop to find correct activity
                       {
                           string[] TuneFileCurrentActivityName = TuneContentsLine[tuneLoop].Split(';');
                           string[] TuneFileAttributeMods = TuneContentsLine[tuneLoop].Split('|');

                           if (TuneFileCurrentActivityName[1] == CurAct[i])
                           {
                               if (decimal.Parse(TuneFileAttributeMods[1]) != 0)
                               {
                                   wgt = wgt + decimal.Parse(TuneFileAttributeMods[1]);
                                   wgtCnt = wgtCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[2]) != 0)
                               {
                                   spd = spd + decimal.Parse(TuneFileAttributeMods[2]);
                                   spdCnt = spdCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[3]) != 0)
                               {
                                   acc = acc + decimal.Parse(TuneFileAttributeMods[3]);
                                   accCnt = accCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[4]) != 0)
                               {
                                   agi = agi + decimal.Parse(TuneFileAttributeMods[4]);
                                   agiCnt = agiCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[5]) != 0)
                               {
                                   str = str + decimal.Parse(TuneFileAttributeMods[5]);
                                   strCnt = strCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[6]) != 0)
                               {
                                   stm = stm + decimal.Parse(TuneFileAttributeMods[6]);
                                   stmCnt = stmCnt + 1;
                               }

                               if (decimal.Parse(TuneFileAttributeMods[7]) != 0)
                               {
                                   inj = inj + decimal.Parse(TuneFileAttributeMods[7]);
                                   injCnt = injCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[8]) != 0)
                               {
                                   tgh = tgh + decimal.Parse(TuneFileAttributeMods[8]);
                                   tghCnt = tghCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[9]) != 0)
                               {
                                   mor = mor + decimal.Parse(TuneFileAttributeMods[9]);
                                   morCnt = morCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[10]) != 0)
                               {
                                   awr = awr + decimal.Parse(TuneFileAttributeMods[10]);
                                   awrCnt = awrCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[11]) != 0)
                               {
                                   cat = cat + decimal.Parse(TuneFileAttributeMods[11]);
                                   catCnt = catCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[12]) != 0)
                               {
                                   car = car + decimal.Parse(TuneFileAttributeMods[12]);
                                   carCnt = carCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[13]) != 0)
                               {
                                   jmp = jmp + decimal.Parse(TuneFileAttributeMods[13]);
                                   jmpCnt = jmpCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[14]) != 0)
                               {
                                   btk = btk + decimal.Parse(TuneFileAttributeMods[14]);
                                   btkCnt = btkCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[15]) != 0)
                               {
                                   tkl = tkl + decimal.Parse(TuneFileAttributeMods[15]);
                                   tklCnt = tklCnt + 1;
                               }  
                               if (decimal.Parse(TuneFileAttributeMods[16]) != 0)
                               {
                                   thp = thp + decimal.Parse(TuneFileAttributeMods[16]);
                                   thpCnt = thpCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[17]) != 0)
                               {
                                   tha = tha + decimal.Parse(TuneFileAttributeMods[17]);
                                   thaCnt = thaCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[18]) != 0)
                               {
                                   pbk = pbk + decimal.Parse(TuneFileAttributeMods[18]);
                                   pbkCnt = pbkCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[19]) != 0)
                               {
                                   rbk = rbk + decimal.Parse(TuneFileAttributeMods[19]);
                                   rbkCnt = rbkCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[20]) != 0)
                               {
                                   kp = kp + decimal.Parse(TuneFileAttributeMods[20]);
                                   kpCnt = kpCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[21]) != 0)
                               {
                                   ka = ka + decimal.Parse(TuneFileAttributeMods[21]);
                                   kaCnt = kaCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[22]) != 0)
                               {
                                   kr = kr + decimal.Parse(TuneFileAttributeMods[22]);
                                   krCnt = krCnt + 1;
                               }
                               if (decimal.Parse(TuneFileAttributeMods[23]) != 0)
                               {
                                   injChance = injChance + decimal.Parse(TuneFileAttributeMods[23]);
                                   InjChanceCnt = InjChanceCnt + 1;
                               }

                           }

                       }

                       //code to process results
                       if (wgtCnt >= 1)
                       {
                           wgt =  Math.Round((wgt / wgtCnt), 4);
                       }
                       if (spdCnt >= 1)
                       {
                           spd =  Math.Round((spd / spdCnt), 4);
                       }
                       if (accCnt >= 1)
                       {
                           acc =  Math.Round((acc / accCnt), 4);
                       }
                       if (agiCnt >= 1)
                       {
                           agi =  Math.Round((agi / agiCnt), 4);
                       }
                       if (strCnt >= 1)
                       {
                           str =  Math.Round((str / strCnt), 4);
                       }
                       if (stmCnt >= 1)
                       {
                           stm =  Math.Round((stm / stmCnt), 4);
                       }
                       if (injCnt >= 1)
                       {
                           inj =  Math.Round((inj / injCnt), 4);
                       }
                       if (tghCnt >= 1)
                       {
                           tgh =  Math.Round((tgh / tghCnt), 4);
                       }
                       if (morCnt >= 1)
                       {
                           mor =  Math.Round((mor / morCnt), 4);
                       }
                       if (awrCnt >= 1)
                       {
                           awr =  Math.Round((awr / awrCnt), 4);
                       }
                       if (catCnt >= 1)
                       {
                           cat =  Math.Round((cat / catCnt), 4);
                       }
                       if (carCnt >= 1)
                       {
                           car =  Math.Round((car / carCnt), 4);
                       }
                         if (jmpCnt >= 1)
                       {
                           jmp =  Math.Round((jmp / jmpCnt), 4);
                       }
                       if (btkCnt >= 1)
                       {
                           btk =  Math.Round((btk / btkCnt), 4);
                       }
                       if (tklCnt >= 1)
                       {
                           tkl =  Math.Round((tkl / tklCnt), 4);
                       }
                       if (thpCnt >= 1)
                       {
                           thp =  Math.Round((thp / thpCnt), 4);
                       }
                       if (thaCnt >= 1)
                       {
                           tha =  Math.Round((tha / thaCnt), 4);
                       }
                       if (pbkCnt >= 1)
                       {
                           pbk = Math.Round((pbk / pbkCnt), 4);
                       }
                       if (rbkCnt >= 1)
                       {
                           rbk = Math.Round((rbk / rbkCnt), 4);
                       }
                       if (kpCnt >= 1)
                       {
                           kp = Math.Round((kp / kpCnt), 4);
                       }
                       if (kaCnt >= 1)
                       {
                           ka = Math.Round((ka / kaCnt), 4);
                       }
                       if (krCnt >= 1)
                       {
                           kr = Math.Round((kr / krCnt), 4);
                       }
                       if (InjChanceCnt >= 1)
                       {
                           injChance = Math.Round((injChance / InjChanceCnt), 4);
                       }

                    string Reload = (CurAct[i] + "," + wgt + "," + spd + "," +
                    acc + "," + agi + "," + str + "," + stm + "," + inj + "," + tgh + "," + mor + "," + awr + "," +
                    cat + "," + car + "," + jmp + "," + btk + "," + tkl + "," + thp + "," + tha + "," + pbk + "," +
                    rbk + "," + kp + "," + ka + "," + kr + "," + injChance);

                       if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\"))
                       {
                           Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\");

                       }

                       StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\CPUsim"), true);
                       sw.WriteLine(Reload);
                       sw.Close();
                       
                       i = i + 1;

                   }//end while

        }
          
        private void ProcessDaily()
        {
            installDirectory = Application.StartupPath;
            string Pos = "";
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;

            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\coachsliders");
            string line = sr.ReadLine();
            sr.Close();
            string[] CoachSliders = line.Split(',');
            int Con = int.Parse(CoachSliders[0]);
            int PosDrill = int.Parse(CoachSliders[1]);
            int Team = int.Parse(CoachSliders[2]);
            int Film = int.Parse(CoachSliders[3]);
            int Special = int.Parse(CoachSliders[4]);
            int Down = int.Parse(CoachSliders[5]);

            model.CoachModel.SetPositionFilter(0);
            model.CoachModel.SetTeamFilter(CurTeam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal HCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal HCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal HCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal HCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal HCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal HCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal HCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal HCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal HCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal HClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal HCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal HCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal HCp = model.CoachModel.CurrentCoachRecord.PuntRating;
            //head coach modifier to conditioning
            decimal HeadCoachConditioningMotivation = Math.Round((((HCMot - 70) / 100)), 2);


            model.CoachModel.SetPositionFilter(1);
            model.CoachModel.SetTeamFilter(CurTeam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal OCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal OCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal OCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal OCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal OCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal OCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal OCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal OCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal OCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal OClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal OCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal OCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal OCp = model.CoachModel.CurrentCoachRecord.PuntRating;

            model.CoachModel.SetPositionFilter(2);
            model.CoachModel.SetTeamFilter(CurTeam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal DCKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal DCMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal DCChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal DCEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal DCqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal DCrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal DCwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal DCol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal DCdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal DClb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal DCdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal DCk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal DCp = model.CoachModel.CurrentCoachRecord.PuntRating;

            model.CoachModel.SetPositionFilter(3);
            model.CoachModel.SetTeamFilter(CurTeam.ToString());
            model.CoachModel.GetNextCoachRecord();
            decimal STKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
            decimal STMot = model.CoachModel.CurrentCoachRecord.Motivation;
            decimal STChm = model.CoachModel.CurrentCoachRecord.Chemistry;
            decimal STEth = model.CoachModel.CurrentCoachRecord.Ethics;
            decimal STqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
            decimal STrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
            decimal STwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
            decimal STol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
            decimal STdl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
            decimal STlb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
            decimal STdb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
            decimal STk = model.CoachModel.CurrentCoachRecord.KickerRating;
            decimal STp = model.CoachModel.CurrentCoachRecord.PuntRating;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            //WindSpeed effect
            decimal WindSpeedModifier = Math.Round((((decimal)WindSpeed - 7) / 1000), 2);
            if (WindSpeedModifier < 0)
            {
                WindSpeedModifier = 0;
            }
            decimal PositionalDrillMod = 0;
            decimal DailyMoraleEffect = 0;
            //Ind. Position view
            foreach (PlayerRecord valObject in teamPlayers)
            {
                bool DietGain = false;
                bool DietLoss = false;
                bool Injured = false;

                if (valObject.PositionId == 0)
                {
                    Pos = "QB";
                    PositionalDrillMod = Math.Round((((HCqb - 70) / 1500) + ((OCqb - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 1)
                {
                    Pos = "HB";
                    PositionalDrillMod = Math.Round((((HCrb - 70) / 1500) + ((OCrb - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 2)
                {
                    Pos = "FB";
                    PositionalDrillMod = Math.Round((((HCrb - 70) / 1500) + ((OCrb - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 3)
                {
                    Pos = "WR";
                    PositionalDrillMod = Math.Round((((HCwr - 70) / 1500) + ((OCwr - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 4)
                {
                    Pos = "TE";
                    PositionalDrillMod = Math.Round((((HCwr - 70) / 1500) + ((OCwr - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                {
                    Pos = "OL";
                    PositionalDrillMod = Math.Round((((HCol - 70) / 1500) + ((OCol - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                {
                    Pos = "DL";
                    PositionalDrillMod = Math.Round((((HCdl - 70) / 1500) + ((DCdl - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                {
                    Pos = "LB";
                    PositionalDrillMod = Math.Round((((HClb - 70) / 1500) + ((DClb - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                {
                    Pos = "DB";
                    PositionalDrillMod = Math.Round((((HCdb - 70) / 1500) + ((DCdb - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 19)
                {
                    Pos = "KP";
                    PositionalDrillMod = Math.Round((((HCk - 70) / 1500) + ((STk - 70) / 2000)), 2);
                }
                else if (valObject.PositionId == 20)
                {
                    Pos = "KP";
                    PositionalDrillMod = Math.Round((((HCp - 70) / 1500) + ((STp - 70) / 2000)), 2);
                }


                //Effect HC and coordinators have on positional drills

                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                string[] advance = sr.ReadLine().Split(',');
                string Allcontents = sr.ReadToEnd();
                sr.Close();
                //Unnasigned % points converted to Morale increase (downtime)
                int PoolUnassigned = 0;
                PoolUnassigned = (int.Parse(advance[0]) + int.Parse(advance[1]) + int.Parse(advance[2]));

                decimal CoachPositionalDrillModifier = 0;
                if (valObject.PositionId <= 9)
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((OCKnw - 70) / 1000)) + PositionalDrillMod), 2);
                    DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((OCChm - 70) / 30)), 0);
                }
                else if ((valObject.PositionId >= 10) & (valObject.PositionId <= 18))
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((DCKnw - 70) / 1000)) + PositionalDrillMod), 2);
                    DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((DCChm - 70) / 30)), 0);
                }
                else if (valObject.PositionId >= 19)
                {
                    CoachPositionalDrillModifier = Math.Round(((((HCKnw - 70) / 1000) + ((STKnw - 70) / 1000)) + PositionalDrillMod), 2);
                    DailyMoraleEffect = Math.Round((((((Down + PoolUnassigned) - Con) / 10) + ((HCChm - 70) / 25)) + ((STChm - 70) / 30)), 0);
                }

                string[] Line = Allcontents.Split('\n');
                int AllocationsLen = Line.Length;

                if (!File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals"))
                {
                    FileStream system = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                    StreamWriter sw = new StreamWriter(system);
                    //Daysactive,injury,injlength,wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                    //    0         1        2     3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21 22 23 24     25   
                    sw.Write(0 + "," + "" + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + valObject.Morale + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0);
                    sw.Close();
                }
                //current totals
                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                string TotalsContents = sr.ReadToEnd();
                sr.Close();
                string[] TotalsContentsLine = TotalsContents.Split(',');
                //morale
                decimal CurrentMoraleEffect = (((decimal.Parse(TotalsContentsLine[11])) - 70) / 1000) + ((decimal)PoolUnassigned / 1000); //Effects players work ethic               
                int DaysActive = int.Parse(TotalsContentsLine[0]);
                string InjuryType = TotalsContentsLine[1];
                int InjuryLength = int.Parse(TotalsContentsLine[2]);
                decimal wgt = 0;
                decimal spd = 0;
                decimal acc = 0;
                decimal agi = 0;
                decimal str = 0;
                decimal stm = 0;
                decimal inj = 0;
                decimal tgh = 0;
                decimal mor = 0;
                decimal awr = 0;
                decimal cat = 0;
                decimal car = 0;
                decimal jmp = 0;
                decimal btk = 0;
                decimal tkl = 0;
                decimal thp = 0;
                decimal tha = 0;
                decimal pbk = 0;
                decimal rbk = 0;
                decimal kp = 0;
                decimal ka = 0;
                decimal kr = 0;
                decimal injchance = 0;



                if (int.Parse(TotalsContentsLine[2]) > 0)
                {
                    //already injured
                    Injured = true;
                    DaysActive = DaysActive;
                    InjuryType = InjuryType;
                    InjuryLength = InjuryLength;
                    wgt = decimal.Parse(TotalsContentsLine[3]);
                    spd = decimal.Parse(TotalsContentsLine[4]);
                    acc = decimal.Parse(TotalsContentsLine[5]);
                    agi = decimal.Parse(TotalsContentsLine[6]);
                    str = decimal.Parse(TotalsContentsLine[7]);
                    stm = decimal.Parse(TotalsContentsLine[8]);
                    inj = decimal.Parse(TotalsContentsLine[9]);
                    tgh = decimal.Parse(TotalsContentsLine[10]);
                    mor = decimal.Parse(TotalsContentsLine[11]);
                    awr = decimal.Parse(TotalsContentsLine[12]);
                    cat = decimal.Parse(TotalsContentsLine[13]);
                    car = decimal.Parse(TotalsContentsLine[14]);
                    jmp = decimal.Parse(TotalsContentsLine[15]);
                    btk = decimal.Parse(TotalsContentsLine[16]);
                    tkl = decimal.Parse(TotalsContentsLine[17]);
                    thp = decimal.Parse(TotalsContentsLine[18]);
                    tha = decimal.Parse(TotalsContentsLine[19]);
                    pbk = decimal.Parse(TotalsContentsLine[20]);
                    rbk = decimal.Parse(TotalsContentsLine[21]);
                    kp = decimal.Parse(TotalsContentsLine[22]);
                    ka = decimal.Parse(TotalsContentsLine[23]);
                    kr = decimal.Parse(TotalsContentsLine[24]);
                    injchance = 0;
                }
                else if (int.Parse(TotalsContentsLine[2]) == 0)
                {
                    //declare variables
                    DaysActive = DaysActive;
                    InjuryType = "";
                    InjuryLength = 0;
                    wgt = decimal.Parse(TotalsContentsLine[3]);
                    spd = decimal.Parse(TotalsContentsLine[4]);
                    acc = decimal.Parse(TotalsContentsLine[5]);
                    agi = decimal.Parse(TotalsContentsLine[6]);
                    str = decimal.Parse(TotalsContentsLine[7]);
                    stm = decimal.Parse(TotalsContentsLine[8]);
                    inj = decimal.Parse(TotalsContentsLine[9]);
                    tgh = decimal.Parse(TotalsContentsLine[10]);
                    mor = decimal.Parse(TotalsContentsLine[11]);
                    awr = decimal.Parse(TotalsContentsLine[12]);
                    cat = decimal.Parse(TotalsContentsLine[13]);
                    car = decimal.Parse(TotalsContentsLine[14]);
                    jmp = decimal.Parse(TotalsContentsLine[15]);
                    btk = decimal.Parse(TotalsContentsLine[16]);
                    tkl = decimal.Parse(TotalsContentsLine[17]);
                    thp = decimal.Parse(TotalsContentsLine[18]);
                    tha = decimal.Parse(TotalsContentsLine[19]);
                    pbk = decimal.Parse(TotalsContentsLine[20]);
                    rbk = decimal.Parse(TotalsContentsLine[21]);
                    kp = decimal.Parse(TotalsContentsLine[22]);
                    ka = decimal.Parse(TotalsContentsLine[23]);
                    kr = decimal.Parse(TotalsContentsLine[24]);
                    injchance = 0;
                }

                //tunefile
                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");
                string TuneTotalsContents = sr.ReadToEnd();
                sr.Close();
                string[] TuneContentsLine = TuneTotalsContents.Split('\n');
                int TuneFileLen = TuneContentsLine.Length;
                string Reload = "";





                for (int i = 0; i < (AllocationsLen - 1); i++) //allocation file
                {
                    string CurrentLine = Line[i];
                    string[] CurrentText = Line[i].Split(',');


                    for (int tuneLoop = 0; tuneLoop < (TuneFileLen - 1); tuneLoop++) //loop to find correct activity
                    {
                        string[] TuneFileCurrentActivityName = TuneContentsLine[tuneLoop].Split(';');
                        string[] TuneFileAttributeMods = TuneContentsLine[tuneLoop].Split('|');

                        string tcActivityName = TuneFileCurrentActivityName[2];
                        if (CurrentText[0] == tcActivityName)
                        {

                            //Add Coach positional drill modifier
                            int ActivityTypeLen = 0;
                            string[] CurrentActivityType = TuneFileCurrentActivityName[1].Split('-');
                            ActivityTypeLen = CurrentActivityType.Length;
                            decimal PCM = 0;

                            if (ActivityTypeLen == 2)
                            {
                                if (CurrentActivityType[1] == "P")
                                {
                                    PCM = CoachPositionalDrillModifier;
                                }
                            }
                            //Logic to determine is diet is healthy

                            if ((ActivityTypeLen == 1) & (Injured == false))
                            {
                                if (CurrentActivityType[0] == "D")
                                {
                                    if (decimal.Parse(TuneFileAttributeMods[1]) > 0)
                                    {
                                        DietGain = true;
                                    }
                                }
                                if (CurrentActivityType[0] == "D")
                                {
                                    if (decimal.Parse(TuneFileAttributeMods[1]) < 0)
                                    {
                                        DietLoss = true;
                                    }
                                }
                            }

                            if (Injured == false) //Player was healthy enough to practice on current day
                            {
                                DaysActive = int.Parse(TotalsContentsLine[0]) + 1;
                                InjuryType = InjuryType;
                                InjuryLength = InjuryLength;
                                decimal Rank = decimal.Parse(CurrentText[1]); //Percent allocation to activity
                                if (decimal.Parse(TuneFileAttributeMods[1]) != 0)
                                {
                                    wgt = wgt + (((decimal.Parse(TuneFileAttributeMods[1]) / 1000) * (decimal)Rank));//+ CurrentMoraleEffect);
                                }
                                if (decimal.Parse(TuneFileAttributeMods[2]) != 0)
                                {
                                    spd = spd + ((((decimal.Parse(TuneFileAttributeMods[2]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[3]) != 0)
                                {
                                    acc = acc + ((((decimal.Parse(TuneFileAttributeMods[3]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[4]) != 0)
                                {
                                    agi = agi + ((((decimal.Parse(TuneFileAttributeMods[4]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[5]) != 0)
                                {
                                    str = str + ((((decimal.Parse(TuneFileAttributeMods[5]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[6]) != 0)
                                {
                                    stm = stm + ((((decimal.Parse(TuneFileAttributeMods[6]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[7]) != 0)
                                {
                                    inj = inj + ((((decimal.Parse(TuneFileAttributeMods[7]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[8]) != 0)
                                {
                                    tgh = tgh + ((((decimal.Parse(TuneFileAttributeMods[8]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM) + TghBonus);
                                }
                                if (decimal.Parse(TuneFileAttributeMods[9]) != 0)
                                {
                                    mor = mor + Math.Round(((decimal.Parse(TuneFileAttributeMods[9]) / 10) * (decimal)Rank), 0);
                                }

                                if (decimal.Parse(TuneFileAttributeMods[10]) != 0)
                                {
                                    awr = awr + ((((decimal.Parse(TuneFileAttributeMods[10]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM) + HvyRainAwrBonus);
                                }
                                if (decimal.Parse(TuneFileAttributeMods[11]) != 0)
                                {
                                    cat = cat + ((((decimal.Parse(TuneFileAttributeMods[11]) / 3000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM) + CatBonus);
                                }
                                if (decimal.Parse(TuneFileAttributeMods[12]) != 0)
                                {
                                    car = car + ((((decimal.Parse(TuneFileAttributeMods[12]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[13]) != 0)
                                {
                                    jmp = jmp + ((((decimal.Parse(TuneFileAttributeMods[13]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[14]) != 0)
                                {
                                    btk = btk + ((((decimal.Parse(TuneFileAttributeMods[14]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[15]) != 0)
                                {
                                    tkl = tkl + ((((decimal.Parse(TuneFileAttributeMods[15]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[16]) != 0)
                                {
                                    thp = thp + (((((decimal.Parse(TuneFileAttributeMods[16]) / 2000) * (decimal)Rank) + WindSpeedModifier) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[17]) != 0)
                                {
                                    tha = tha + (((((decimal.Parse(TuneFileAttributeMods[17]) / 2000) * (decimal)Rank) - WindSpeedModifier) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[18]) != 0)
                                {
                                    pbk = pbk + ((((decimal.Parse(TuneFileAttributeMods[18]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[19]) != 0)
                                {
                                    rbk = rbk + ((((decimal.Parse(TuneFileAttributeMods[19]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[20]) != 0)
                                {
                                    kp = kp + (((((decimal.Parse(TuneFileAttributeMods[20]) / 2000) * (decimal)Rank) + WindSpeedModifier) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[21]) != 0)
                                {
                                    ka = ka + (((((decimal.Parse(TuneFileAttributeMods[21]) / 2000) * (decimal)Rank) - WindSpeedModifier) + CurrentMoraleEffect) + ((decimal)PCM));
                                }
                                if (decimal.Parse(TuneFileAttributeMods[22]) != 0)
                                {
                                    kr = kr + ((((decimal.Parse(TuneFileAttributeMods[22]) / 2000) * (decimal)Rank) + CurrentMoraleEffect) + ((decimal)PCM));
                                    if (CurDay >= 8)
                                    {
                                        kr = kr + (Special / 2000);
                                    }
                                }
                                if (decimal.Parse(TuneFileAttributeMods[23]) != 0)
                                {
                                    injchance = injchance + (((decimal.Parse(TuneFileAttributeMods[23]) / 100) * (decimal)Rank) );
                                }




                            }


                        }


                    }

                    //TeamDrills if day >= 8
                    int TeamDrillHalf = 0;
                    int TeamDrillLive = 0;
                    if (CurDay >= 8)
                    {
                        if (Injured == false)
                        {
                            sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\TeamDrills");
                            string[] Total = sr.ReadLine().Split(',');
                            sr.Close();
                            TeamDrillHalf = int.Parse(Total[1]);
                            TeamDrillLive = int.Parse(Total[2]);


                            //Positional Bonuses
                            if (Pos == "QB")
                            {
                                tha = tha + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                tha = tha + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "HB")
                            {
                                car = car + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                btk = btk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                car = car + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                btk = btk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "FB")
                            {
                                rbk = rbk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                pbk = pbk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                rbk = rbk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                pbk = pbk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "TE")
                            {
                                cat = cat + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                rbk = rbk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                cat = cat + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                rbk = rbk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "WR")
                            {
                                cat = cat + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                cat = cat + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "OL")
                            {
                                rbk = rbk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                pbk = pbk + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                rbk = rbk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                pbk = pbk + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "DL")
                            {
                                acc = acc + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                acc = acc + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "LB")
                            {
                                awr = awr + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "DB")
                            {
                                awr = awr + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                awr = awr + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                tkl = tkl + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }
                            else if (Pos == "KP")
                            {
                                kp = kp + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                ka = ka + Math.Round((((decimal)TeamDrillHalf) / 1000), 4);
                                kp = kp + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                                ka = ka + Math.Round((((decimal)TeamDrillLive) / 500), 4);
                            }



                            if (Pos != "QB")
                            {
                                injchance = injchance + (TeamDrillHalf / 100) + (TeamDrillLive / 25);

                            }



                            //Exp modifier  

                            if ((valObject.YearsPro <= 4) & (Pos != "QB"))
                            {
                                if (valObject.YearsPro == 0)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 700)), 4);
                                }
                                else if (valObject.YearsPro == 1)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 800)), 4);
                                }
                                else if (valObject.YearsPro == 2)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 1000)), 4);
                                }
                                else if (valObject.YearsPro == 3)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 1200)), 4);
                                }
                                else if (valObject.YearsPro == 4)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 1500)), 4);
                                }
                            }
                            else if ((valObject.YearsPro > 4) & (Pos != "QB"))
                            {
                                awr = awr + Math.Round((((decimal)Film / 2000)), 4);
                            }

                            if (Pos == "KP")
                            {
                                ka = ka + Math.Round((((decimal)Special / 2000)), 4);
                                kp = kp + Math.Round((((decimal)Special / 2000)), 4);
                            }


                            if ((valObject.YearsPro <= 4) & (Pos == "QB"))
                            {
                                if (valObject.YearsPro == 0)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 8000)), 4);
                                }
                                else if (valObject.YearsPro == 1)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 6000)), 4);
                                }
                                else if (valObject.YearsPro == 2)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 4000)), 4);
                                }
                                else if (valObject.YearsPro == 3)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 2000)), 4);
                                }
                                else if (valObject.YearsPro == 4)
                                {
                                    awr = awr + Math.Round((((decimal)Film / 1200)), 4);
                                }
                            }
                            else if ((valObject.YearsPro > 4) & (Pos == "QB"))
                            {
                                awr = awr + Math.Round((((decimal)Film / 900)), 4);
                            }

                      





                        }


                    }



                }//end player loop and process results
                if (Injured == true)
                {
                    DaysActive = int.Parse(TotalsContentsLine[0]);
                    InjuryType = InjuryType;
                    if (InjuryLength != 91)
                    {
                        InjuryLength = InjuryLength - 1;
                    }
                    if (InjuryLength == 0)
                    {
                        InjuryLength = 0;
                        InjuryType = "";
                    }
                    //Rust factor
                    if (wgt != 0)
                    {
                        wgt = wgt + (decimal).075;
                    }
                    if (spd != 0)
                    {
                    spd = spd - (decimal).1;
                    }
                    if (acc != 0)
                    {
                        acc = acc - (decimal).1;
                    }
                    if (agi != 0)
                    {
                        agi = agi - (decimal).1;
                    }
                    if (str != 0)
                    {
                        str = str - (decimal).1;
                    }
                    if (stm != 0)
                    {
                        stm = stm - (decimal).1;
                    }
                    if (inj != 0)
                    {
                        inj = inj - (decimal).1;
                    }
                    if (tgh != 0)
                    {
                        tgh = tgh - (decimal).1;
                    }
                   
                        mor = mor;
                    
                    if (awr != 0)
                    {
                        awr = awr - (decimal).1;
                    }
                    if (cat != 0)
                    {
                        cat = cat - (decimal).1;
                    }
                    if (car != 0)
                    {
                        car = car - (decimal).1;
                    }
                    if (jmp != 0)
                    {
                        jmp = jmp - (decimal).1;
                    }
                    if (btk != 0)
                    {
                        btk = btk - (decimal).1;
                    }
                    if (tkl != 0)
                    {
                        tkl = tkl - (decimal).1;
                    }
                    if (thp != 0)
                    {
                        thp = thp - (decimal).1;
                    }
                    if (tha != 0)
                    {
                        tha = tha - (decimal).1;
                    }
                    if (pbk != 0)
                    {
                        pbk = pbk - (decimal).1;
                    }
                    if (rbk != 0)
                    {
                        rbk = rbk - (decimal).1;
                    }
                    if (kp != 0)
                    {
                        kp = kp - (decimal).1;
                    }
                    if (ka != 0)
                    {
                        ka = ka - (decimal).1;
                    }
                    if (kr != 0)
                    {
                        kr = kr - (decimal).1;
                    }
                    injchance = 0;


                }
                if (Facility == "Indoors")
                {
                    injchance = injchance * (decimal)1.3;
                }
                //diet result
                if ((DietGain == true) & (DietLoss == true) & (Injured == false))
                {
                    Injured = true;
                    InjuryType = ";is throwing up all over the place. Why on earth did you mix his diet like that? Either put him on a weight loss diet or a weight gain diet. Not both.";
                    InjuryLength++;
                }

                //INJURY LOGIC HERE
                if ((Injured == false) & (CurDay <= 13))
                {

                    int FinalInjChance = (valObject.Injury - ((int)injchance + (int)WthInjIncrease)) + (valObject.Toughness / 10);

                    if ((55 * random.NextDouble() + 1) > (FinalInjChance))
                    {
                        Injured = true;

                        //QB only minor injuries
                        string QBminor = "39,Strained forearm,2,3;58,Sprained wrist,2,3;119,Abdominal strain,1,3;115,Pulled groin,3,4;97,Broken finger,1,1;98,Dislocated finger,1,1;" +
                        "99,Broken thumb,3,5;100,Dislocated thumb,1,2;124,Dislocated shoulder,5,5;156,Bruised shoulder,1,2;157,Strained shoulder,2,3;";
                        string QBmajor = "219,Torn rotator cuff,254;220,Torn shoulder,169;";
                        //General Inj to all players other than qb
                        string GeneralMinor = "0,Bruised ankle,1,2;1,Sprained ankle,2,5;3,Strained forearm,1,2;4,Strained bicep,1,3;5,Strained tricep,1,4;6,Upper arm buise,1,1;7,Back spasms,2,3;" +
                        "9,Elbow bursitis,2,3;10,Foot contusion,1,1;11,Foot sprain,1,1;13,Broken finger,1,1;15,Broken thumb,2,3;16,Dislocated thumb,1,1;17,Dislocated wrist,4,5;18,Sprained wrist,1,2;" +
                        "19,Pinched nerve,1,1;20,Hip bursitis,2,2;22,Knee bursitis,1,3;23,Strained calf,1,2;25,Pulled hamstring,2,5;26,Bruised quadricep,1,1;27,Strained quadricep,1,3;28,Abdominal strain,1,1;" +
                        "29,Bruised ribs,2,4;30,Bruised sternum,1,2;31,Strained pectoral,2,3;115,Pulled groin,3,6;113,Strained knee,2,5;114,Strained calf,1,4;";
                        string GeneralMajor = "95,Turf toe,152;108,ACL sprain,107;111,MCL sprain,88;112,PCL sprain,88;158,High ankle sprain,145;159,Broken ankle,167;160,Dislocated ankle,88;161,Forearm fracture,68;" +
                        "162,Torn bicep,145;163,Torn tricep,174;180,Torn groin,179;181,Torn hamstring,174;182,Torn quadricep,212;203,Complete ACL tear,254;204,Partially torn ACL,218;205,Knee cartilage tear,84;" +
                        "209,Partially torn MCL,84;210,Complete PCL tear,254;211,Partially torn PCL,204;227,Fractured patella,229;228,Complete MCL tear,254;229,Complete PCL tear,254;";

                        int severityroll = (int)(100 * random.NextDouble() + 1);
                        string[] INJURYbreakdown = GeneralMinor.Split(';'); ;
                        string[] INJURYdescription;
                        int InjurySelector = 0;

                        if (severityroll <= 3)
                        {
                            if (Pos == "QB")
                            {
                                INJURYbreakdown = QBmajor.Split(';');
                            }
                            else if (Pos != "QB")
                            {
                                INJURYbreakdown = GeneralMajor.Split(';');
                            }

                            InjurySelector = (int)((INJURYbreakdown.Length - 1) * random.NextDouble());
                            INJURYdescription = INJURYbreakdown[InjurySelector].Split(',');

                            InjuryRecord injRec = null;
                            injRec = model.PlayerModel.CreateNewInjuryRecord();

                            injRec.PlayerId = valObject.PlayerId;
                            injRec.TeamId = valObject.TeamId;
                            injRec.InjuryLength = int.Parse(INJURYdescription[2]); ;
                            injRec.InjuryReserve = false;
                            injRec.InjuryType = int.Parse(INJURYdescription[0]);
                            InjuryType = ";" + INJURYdescription[1];

                            if (int.Parse(INJURYdescription[2]) <= 83)
                            {
                                InjuryLength = 21;//"3 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 103)
                            {
                                InjuryLength = 28;// "4 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 123)
                            {
                                InjuryLength = 35;//"5 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 143)
                            {
                                InjuryLength = 42;//"6 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 163)
                            {
                                InjuryLength = 49;//"7 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 183)
                            {
                                InjuryLength = 56;//"8 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 203)
                            {
                                InjuryLength = 63;//"9 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 223)
                            {
                                InjuryLength = 70;//"10 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 243)
                            {
                                InjuryLength = 77;//"11 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) <= 253)
                            {
                                InjuryLength = 84;//"12 weeks";
                            }
                            else if (int.Parse(INJURYdescription[2]) > 253)
                            {
                                InjuryLength = 91;//"Out for Season";
                            }

                        }
                        else if (severityroll > 3)
                        {

                            if (Pos == "QB")
                            {
                                INJURYbreakdown = QBminor.Split(';');
                            }
                            else if (Pos != "QB")
                            {
                                INJURYbreakdown = GeneralMinor.Split(';');
                            }

                            InjurySelector = (int)((INJURYbreakdown.Length - 1) * random.NextDouble());
                            INJURYdescription = INJURYbreakdown[InjurySelector].Split(',');

                            InjuryType = ";" + INJURYdescription[1];
                            InjuryLength = (int)((int.Parse(INJURYdescription[3]) - int.Parse(INJURYdescription[2])) * random.NextDouble() + int.Parse(INJURYdescription[2]));

                        }




                    }
                } //end injury




                //Heat Ex. including windspeed reduction
                if ((Injured == false) & (Temp >= 88) & (Facility == "Outdoors") & (HeadCold == 0) & (CurDay <= 13))
                {
                    decimal HeatPercentChance = Math.Round(((((decimal)Temp - 88) - ((decimal)WindSpeed / 10)) * (((decimal)Con / 100) + 1)), 0);
                    if (((int)(120 * random.NextDouble() + 1) < (HeatPercentChance)))
                    {
                        InjuryType = "; is suffering from heat exhaustion and will miss todays practice.";
                        InjuryLength++;
                    }

                }
                if ((Injured == false) & (HeadCold > 0) & (CurDay <= 13) & (Facility == "Outdoors"))
                {
                    if (((int)(100 * random.NextDouble() + 1) <= HeadCold))
                    {
                        InjuryType = "; has a head cold and will miss todays practice.";
                        InjuryLength++;
                    }
                }
                //Morale adjustmants
                mor = mor + (decimal)DailyMoraleEffect;
                HighLowCheck((int)mor);
                mor = Rating;




                //Process daily results

                // injchance = injchance + ((decimal.Parse(TotalsContentsLine[25]) + decimal.Parse(TuneFileAttributeMods[23]) + CurrentMoraleEffect) + ((decimal)PCM) + WthInjIncrease);
                //old ratings

                sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                string OldTotalsContents = sr.ReadToEnd();
                sr.Close();
                string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                int ovrO = int.Parse(OldRatings[2]);
                int wgtO = int.Parse(OldRatings[1]);
                int spdO = int.Parse(OldRatings[3]);
                int accO = int.Parse(OldRatings[4]);
                int agiO = int.Parse(OldRatings[5]);
                int strO = int.Parse(OldRatings[6]);
                int stmO = int.Parse(OldRatings[7]);
                int injO = int.Parse(OldRatings[8]);
                int tghO = int.Parse(OldRatings[9]);
                int morO = int.Parse(OldRatings[10]);
                int awrO = int.Parse(OldRatings[11]);
                int catO = int.Parse(OldRatings[12]);
                int carO = int.Parse(OldRatings[13]);
                int jmpO = int.Parse(OldRatings[14]);
                int btkO = int.Parse(OldRatings[15]);
                int tklO = int.Parse(OldRatings[16]);
                int thpO = int.Parse(OldRatings[17]);
                int thaO = int.Parse(OldRatings[18]);
                int pbkO = int.Parse(OldRatings[19]);
                int rbkO = int.Parse(OldRatings[20]);
                int kpO = int.Parse(OldRatings[21]);
                int kaO = int.Parse(OldRatings[22]);
                int krO = int.Parse(OldRatings[23]);



                //Maximum raise to attribute based on age
                decimal MaxRaise = 1;
                if (valObject.Age >= 30)
                {
                    MaxRaise = Math.Round(((((decimal)valObject.Age - 29) / 100) + 1), 2);
                }
                //valObject.Speed = (int)(valObject.Speed * Drop);
                //valObject.Acceleration = (int)(valObject.Acceleration * Drop);
                //valObject.Agility = (int)(valObject.Agility * Drop);
                //valObject.Strength = (int)(valObject.Strength * Drop);
                //valObject.Injury = (int)(valObject.Injury * Drop);
                //valObject.Jumping = (int)(valObject.Jumping * Drop);
                //valObject.BreakTackle = (int)(valObject.BreakTackle * Drop);
                //valObject.Tackle = (int)(valObject.Tackle * Drop);
                //valObject.ThrowPower = (int)(valObject.ThrowPower * Drop);
                //valObject.KickPower = (int)(valObject.KickPower * Drop);


                AttributeMeans(valObject.PositionId);
                decimal AttributeDifferential = 0;
                decimal PercentModMuscle = 0;
                decimal PercentModFat = 0;
                int addedwgt = 0;

                //weight control

                if (wgt > 0)
                {
                    addedwgt = (int)(Math.Ceiling(((valObject.Weight + 160) + (((decimal)wgt * (decimal)PosWgtMod)))));
                    if (addedwgt > wgtO)
                    {
                        wgt = 0;
                    }
                    PercentModFat = (int)(Math.Round(((decimal)wgtO / (decimal)addedwgt), 3));
                    PercentModMuscle = (int)(Math.Round(((decimal)addedwgt / (decimal)wgtO), 3));
                    valObject.Weight = (addedwgt - 160);
                    Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                    HighLowCheck(Rating);
                    valObject.BodyMuscle = Rating;
                    Rating = (int)(Math.Ceiling((valObject.BodyFat * (PercentModFat * (decimal)1.35))));
                    HighLowCheck(Rating);
                    valObject.BodyFat = Rating;
                    Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.ArmsMuscle = Rating;
                    Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.ArmsFat = Rating;
                    Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.LegsThighMuscle = Rating;
                    Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.LegsThighFat = Rating;
                    Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.LegsCalfMuscle = Rating;
                    Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.LegsCalfFat = Rating;
                    Rating = (int)(Math.Ceiling((valObject.BodyOverall * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.BodyOverall = Rating;
                }
                else if (wgt < 0)
                {
                    addedwgt = (int)(Math.Floor(((valObject.Weight + 160) + (((decimal)wgt * (decimal)PosWgtMod)))));
                    if (addedwgt < wgtO)
                    {
                        wgt = 0;
                    }
                    PercentModFat = Math.Round(((decimal)addedwgt / (decimal)wgtO), 3);
                    PercentModMuscle = Math.Round(((decimal)wgtO / (decimal)addedwgt), 3);
                    valObject.Weight = (addedwgt - 160);
                    Rating = (int)(Math.Ceiling(valObject.BodyMuscle * PercentModMuscle));
                    HighLowCheck(Rating);
                    valObject.BodyMuscle = Rating;
                    Rating = (int)(Math.Floor(valObject.BodyFat * (PercentModFat * (decimal)0.65)));
                    HighLowCheck(Rating);
                    valObject.BodyFat = Rating;
                    Rating = (int)(Math.Ceiling((valObject.ArmsMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.ArmsMuscle = Rating;
                    Rating = (int)(Math.Floor((valObject.ArmsFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.ArmsFat = Rating;
                    Rating = (int)(Math.Ceiling((valObject.LegsThighMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.LegsThighMuscle = Rating;
                    Rating = (int)(Math.Floor((valObject.LegsThighFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.LegsThighFat = Rating;
                    Rating = (int)(Math.Ceiling((valObject.LegsCalfMuscle * PercentModMuscle)));
                    HighLowCheck(Rating);
                    valObject.LegsCalfMuscle = Rating;
                    Rating = (int)(Math.Floor((valObject.LegsCalfFat * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.LegsCalfFat = Rating;
                    Rating = (int)(Math.Floor((valObject.BodyOverall * PercentModFat)));
                    HighLowCheck(Rating);
                    valObject.BodyOverall = Rating;

                }
                decimal ExpMod = 1;
                if (valObject.YearsPro <= 3)
                {
                    ExpMod = Math.Round((((((decimal)valObject.YearsPro - 4) / 4) / 10) + 1), 2);
                }
                else if (valObject.YearsPro >= 4)
                {
                    ExpMod = Math.Round(((((decimal)valObject.YearsPro - 4) / 100) + 1), 2);
                }
                //speed
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Speed / (decimal)SpdMean))), 3);
                if (valObject.Speed > SpdMean)
                {
                    AttributeDifferential = (decimal).25; //was .01
                }
                Rating = (int)(Math.Round((valObject.Speed + (Math.Round((AttributeDifferential * (decimal)(spd)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Speed)
                {
                    spd = 0;
                }
                valObject.Speed = Rating;
                if (valObject.Speed > (int)(Math.Round(((decimal)spdO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Speed = (int)(Math.Round(((decimal)spdO * MaxRaise), 0));
                }

                //accel
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Acceleration / (decimal)AccMean))), 3);
                if (valObject.Acceleration > AccMean)
                {
                    AttributeDifferential = (decimal).35;//was .01
                }
                Rating = (int)(Math.Round((valObject.Acceleration + (Math.Round((AttributeDifferential * (decimal)(acc)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Acceleration)
                {
                    acc = 0;
                }
                valObject.Acceleration = Rating;
                if (valObject.Acceleration > (int)(Math.Round(((decimal)accO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Acceleration = (int)(Math.Round(((decimal)accO * MaxRaise), 0));
                }


                //agi
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Agility / (decimal)AgiMean))), 3);
                if (valObject.Agility > AgiMean)
                {
                    AttributeDifferential = (decimal).35;//was .01
                }
                Rating = (int)(Math.Round((valObject.Agility + (Math.Round((AttributeDifferential * (decimal)(agi)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Agility)
                {
                    agi = 0;
                }
                valObject.Agility = Rating;
                if (valObject.Agility > (int)(Math.Round(((decimal)agiO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Agility = (int)(Math.Round(((decimal)agiO * MaxRaise), 0));
                }

                //str
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Strength / (decimal)StrMean))), 3);
                if (valObject.Strength > StrMean)
                {
                    AttributeDifferential = (decimal).35;//was .01
                }
                Rating = (int)(Math.Round((valObject.Strength + (Math.Round((AttributeDifferential * (decimal)(str)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Strength)
                {
                    str = 0;
                }
                valObject.Strength = Rating;
                if (valObject.Strength > (int)(Math.Round(((decimal)strO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Strength = (int)(Math.Round(((decimal)strO * MaxRaise), 0));
                }


                //stm
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Stamina / (decimal)StmMean))), 3);
                if (valObject.Stamina > StmMean)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.Stamina + (Math.Round((AttributeDifferential * (decimal)(stm)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Stamina)
                {
                    stm = 0;
                }
                valObject.Stamina = Rating;

                //inj
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Injury / (decimal)InjMean))), 3);
                if (valObject.Injury > InjMean)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.Injury + (Math.Round((AttributeDifferential * (decimal)(inj)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Injury)
                {
                    inj = 0;
                }
                valObject.Injury = Rating;

                if (valObject.Injury > (int)(Math.Round(((decimal)injO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Injury = (int)(Math.Round(((decimal)injO * MaxRaise), 0));
                }


                //tough
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Toughness / (decimal)99))), 3);
                Rating = (int)(Math.Round((valObject.Toughness + (Math.Round((AttributeDifferential * (decimal)(tgh)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Toughness)
                {
                    tgh = 0;
                }
                valObject.Toughness = Rating;

                //mor
                //    AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Acceleration / (decimal)AccMean))), 3);
                //   Rating = (int)(Math.Round((AttributeDifferential * (decimal)(acc)), 0));
                //    HighLowCheck(Rating);
                Rating = (int)mor;
                HighLowCheck(Rating);
                valObject.Morale = Rating;
                //awar
                AttributeDifferential = Math.Round(((1 - ((decimal)valObject.Awareness / (decimal)99))), 3);
                Rating = (int)(Math.Round((valObject.Awareness + (Math.Round((AttributeDifferential * (decimal)(awr)), 0))), 0));
                //if (valObject.YearsPro <= 3)
                //{
                //    valObject.Awareness = Rating + (int)((valObject.YearsPro - 4) * (Math.Round(((double)DaysActive / 7), 2)));
                //}
                if (Rating > valObject.Awareness)
                {
                    awr = 0;
                }
                valObject.Awareness = Rating;


                //cat
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Catching * (ExpMod)) / (decimal)87))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).1;
                }
                Rating = (int)(Math.Round((valObject.Catching + (Math.Round((AttributeDifferential * (decimal)(cat)), 0))), 0));
                HighLowCheck(Rating);
                valObject.Catching = Rating;

                //car
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Carrying * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.Carrying + (Math.Round((AttributeDifferential * (decimal)(car)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Carrying)
                {
                    car = 0;
                }
                valObject.Carrying = Rating;


                //jmp
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Jumping * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.Jumping + (Math.Round((AttributeDifferential * (decimal)(jmp)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Jumping)
                {
                    jmp = 0;
                }
                valObject.Jumping = Rating;
                if (valObject.Jumping > (int)(Math.Round(((decimal)jmpO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Jumping = (int)(Math.Round(((decimal)jmpO * MaxRaise), 0));
                }


                //btk
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.BreakTackle * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.BreakTackle + (Math.Round((AttributeDifferential * (decimal)(btk)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.BreakTackle)
                {
                    btk = 0;
                }
                valObject.BreakTackle = Rating;
                if (valObject.BreakTackle > (int)(Math.Round(((decimal)btkO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.BreakTackle = (int)(Math.Round(((decimal)btkO * MaxRaise), 0));
                }
                //tkl
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.Tackle * ExpMod) / (decimal)90))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.Tackle + (Math.Round((AttributeDifferential * (decimal)(tkl)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.Tackle)
                {
                    tkl = 0;
                }
                valObject.Tackle = Rating;
                if (valObject.Tackle > (int)(Math.Round(((decimal)tklO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.Tackle = (int)(Math.Round(((decimal)tklO * MaxRaise), 0));
                }
                //thp
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.ThrowPower * ExpMod) / (decimal)90))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.ThrowPower + (Math.Round((AttributeDifferential * (decimal)(thp)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.ThrowPower)
                {
                    thp = 0;
                }
                valObject.ThrowPower = Rating;
                if (valObject.ThrowPower > (int)(Math.Round(((decimal)thpO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.ThrowPower = (int)(Math.Round(((decimal)thpO * MaxRaise), 0));
                }
                //tha
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.ThrowAccuracy * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.ThrowAccuracy + (Math.Round((AttributeDifferential * (decimal)(tha)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.ThrowAccuracy)
                {
                    tha = 0;
                }
                valObject.ThrowAccuracy = Rating;
                //pbk
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.PassBlocking * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.PassBlocking + (Math.Round((AttributeDifferential * (decimal)(pbk)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.PassBlocking)
                {
                    pbk = 0;
                }
                valObject.PassBlocking = Rating;
                //rbk
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.RunBlocking * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.RunBlocking + (Math.Round((AttributeDifferential * (decimal)(rbk)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.RunBlocking)
                {
                    rbk = 0;
                }
                valObject.RunBlocking = Rating;
                //kp
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickPower * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.KickPower + (Math.Round((AttributeDifferential * (decimal)(kp)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.KickPower)
                {
                    kp = 0;
                }
                valObject.KickPower = Rating;
                if (valObject.KickPower > (int)(Math.Round(((decimal)kpO * MaxRaise), 0)) & (valObject.Age >= 30))
                {
                    valObject.KickPower = (int)(Math.Round(((decimal)kpO * MaxRaise), 0));
                }
                //ka
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickAccuracy * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.KickAccuracy + (Math.Round((AttributeDifferential * (decimal)(ka)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.KickAccuracy)
                {
                    ka = 0;
                }
                valObject.KickAccuracy = Rating;
                //kr
                AttributeDifferential = Math.Round(((1 - (((decimal)valObject.KickReturn * ExpMod) / (decimal)99))), 3);
                if (AttributeDifferential > 1)
                {
                    AttributeDifferential = (decimal).01;
                }
                Rating = (int)(Math.Round((valObject.KickReturn + (Math.Round((AttributeDifferential * (decimal)(kr)), 0))), 0));
                HighLowCheck(Rating);
                if (Rating > valObject.KickReturn)
                {
                    kr = 0;
                }
                valObject.KickReturn = Rating;

                //reset inj. chance
                injchance = 0;
                Reload = (DaysActive + "," + InjuryType + "," + InjuryLength + "," + wgt + "," + spd + "," +
                    acc + "," + agi + "," + str + "," + stm + "," + inj + "," + tgh + "," + mor + "," + awr + "," +
                    cat + "," + car + "," + jmp + "," + btk + "," + tkl + "," + thp + "," + tha + "," + pbk + "," +
                    rbk + "," + kp + "," + ka + "," + kr + "," + injchance);

                StreamWriter sw1 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                sw1.Write(Reload);
                sw1.Close();


            }



        }
        private void CellColor(int Col, int Row, int Attribute, int OldAttribute)
        {
            depthChartDataGrid.CurrentCell = depthChartDataGrid[Col, Row];
            depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Black;

            if (Attribute > OldAttribute)
            { 
                depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Blue;
            }
            else if (Attribute < OldAttribute)
            { 
                depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Red;
            }

            if (Col == 5)
            {
                if (Attribute > OldAttribute)
                { depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Red; }
                else if (Attribute < OldAttribute)
                { depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Blue; }
            }
        }
        private void ProcessFinal()
        {

            string Pos = "";
            int acCounter = 0;
            string installDirectory = Application.StartupPath;
            isInitialising = true;
            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            int teamId = CurTeamIndex;
            int positionId = filterPositionComboBox.SelectedIndex;
            filterPositionComboBox.Enabled = false;
            ActivityCmb.Enabled = false;
            SetTimeGrd.Enabled = false;
            ActivityGrd.Enabled = false;
            depthChartDataGrid.AllowUserToResizeColumns = true;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            InitializeDataGrids();          

                foreach (PlayerRecord valObject in teamPlayers)
                {
                    if (valObject.PositionId == 0)
                    {
                        Pos = "QB";
                    }
                    else if (valObject.PositionId == 1)
                    {
                        Pos = "HB";
                    }
                    else if (valObject.PositionId == 2)
                    {
                        Pos = "FB";
                    }
                    else if (valObject.PositionId == 3)
                    {
                        Pos = "WR";
                    }
                    else if (valObject.PositionId == 4)
                    {
                        Pos = "TE";
                    }
                    else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                    {
                        Pos = "OL";
                    }
                    else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                    {
                        Pos = "DL";
                    }
                    else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                    {
                        Pos = "LB";
                    }
                    else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                    {
                        Pos = "DB";
                    }
                    else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                    {
                        Pos = "KP";
                    }

                        DataRow dr = RosterView.NewRow();
                        // playerHeightComboBox.SelectedIndex = record.Height - 65;
                        valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                        //Reload the overall rating
                        valObject.Overall = valObject.Overall;
                        dr["Name"] = valObject.FirstName + " " + valObject.LastName + " POST";
                        dr["Age"] = valObject.Age;
                        dr["Exp"] = valObject.YearsPro;
                        dr["Ovr"] = valObject.Overall;
                        // Would like to include feet/inches
                        dr["Hgt"] = valObject.Height;
                        dr["Wgt"] = valObject.Weight + 160;
                        dr["Spd"] = valObject.Speed;
                        dr["Acc"] = valObject.Acceleration;
                        dr["Agi"] = valObject.Agility;
                        dr["Str"] = valObject.Strength;
                        dr["Stm"] = valObject.Stamina;
                        dr["Inj"] = valObject.Injury;
                        dr["Tgh"] = valObject.Toughness;
                        dr["Mor"] = valObject.Morale;
                        dr["Awr"] = valObject.Awareness;
                        dr["Cat"] = valObject.Catching;
                        dr["Car"] = valObject.Carrying;
                        dr["Jmp"] = valObject.Jumping;
                        dr["Btk"] = valObject.BreakTackle;
                        dr["Tkl"] = valObject.Tackle;
                        dr["ThP"] = valObject.ThrowPower;
                        dr["ThA"] = valObject.ThrowAccuracy;
                        dr["Pbk"] = valObject.PassBlocking;
                        dr["Rbk"] = valObject.RunBlocking;
                        dr["KP"] = valObject.KickPower;
                        dr["KA"] = valObject.KickAccuracy;
                        dr["KR"] = valObject.KickReturn;

                        RosterView.Rows.Add(dr);
                        

                        StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                        string OldTotalsContents = sr.ReadToEnd();
                        sr.Close();
                        string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                        string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                        int ovrO = int.Parse(OldRatings[2]);
                        int wgtO = int.Parse(OldRatings[1]);
                        int spdO = int.Parse(OldRatings[3]);
                        int accO = int.Parse(OldRatings[4]);
                        int agiO = int.Parse(OldRatings[5]);
                        int strO = int.Parse(OldRatings[6]);
                        int stmO = int.Parse(OldRatings[7]);
                        int injO = int.Parse(OldRatings[8]);
                        int tghO = int.Parse(OldRatings[9]);
                        int morO = int.Parse(OldRatings[10]);
                        int awrO = int.Parse(OldRatings[11]);
                        int catO = int.Parse(OldRatings[12]);
                        int carO = int.Parse(OldRatings[13]);
                        int jmpO = int.Parse(OldRatings[14]);
                        int btkO = int.Parse(OldRatings[15]);
                        int tklO = int.Parse(OldRatings[16]);
                        int thpO = int.Parse(OldRatings[17]);
                        int thaO = int.Parse(OldRatings[18]);
                        int pbkO = int.Parse(OldRatings[19]);
                        int rbkO = int.Parse(OldRatings[20]);
                        int kpO = int.Parse(OldRatings[21]);
                        int kaO = int.Parse(OldRatings[22]);
                        int krO = int.Parse(OldRatings[23]);

                           DataRow af = RosterView.NewRow();
                        // playerHeightComboBox.SelectedIndex = record.Height - 65;
                        valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                        //Reload the overall rating
                        valObject.Overall = valObject.Overall;
                        af["Name"] = valObject.FirstName + " " + valObject.LastName + " PRE";
                        af["Age"] = valObject.Age;
                        af["Exp"] = valObject.YearsPro;
                        af["Ovr"] = ovrO;
                        // Would like to include feet/inches
                        af["Hgt"] = valObject.Height;
                        af["Wgt"] = wgtO;
                        af["Spd"] = spdO;
                        af["Acc"] = accO;
                        af["Agi"] = agiO;
                        af["Str"] = strO;
                        af["Stm"] = stmO;
                        af["Inj"] = injO;
                        af["Tgh"] = tghO;
                        af["Mor"] = morO;
                        af["Awr"] = awrO;
                        af["Cat"] = catO;
                        af["Car"] = carO;
                        af["Jmp"] = jmpO;
                        af["Btk"] = btkO;
                        af["Tkl"] = tklO;
                        af["ThP"] = thpO;
                        af["ThA"] = thaO;
                        af["Pbk"] = pbkO;
                        af["Rbk"] = rbkO;
                        af["KP"] = kpO;
                        af["KA"] = kaO;
                        af["KR"] = krO;

                        RosterView.Rows.Add(af);
                       /*
                        CellColor(3, acCounter, valObject.Overall, ovrO);
                        CellColor(5, acCounter, valObject.Weight, (wgtO - 160));
                        CellColor(6, acCounter, valObject.Speed, spdO);
                        CellColor(7, acCounter, valObject.Acceleration, accO);
                        CellColor(8, acCounter, valObject.Agility, agiO);
                        CellColor(9, acCounter, valObject.Strength, strO);
                        CellColor(10, acCounter, valObject.Stamina, stmO);
                        CellColor(11, acCounter, valObject.Injury, injO);
                        CellColor(12, acCounter, valObject.Toughness, tghO);
                        CellColor(13, acCounter, valObject.Morale, morO);
                        CellColor(14, acCounter, valObject.Awareness, awrO);
                        CellColor(15, acCounter, valObject.Catching, catO);
                        CellColor(16, acCounter, valObject.Carrying, carO);
                        CellColor(17, acCounter, valObject.Jumping, jmpO);
                        CellColor(18, acCounter, valObject.BreakTackle, btkO);
                        CellColor(19, acCounter, valObject.Tackle, tklO);
                        CellColor(20, acCounter, valObject.ThrowPower, thpO);
                        CellColor(21, acCounter, valObject.ThrowAccuracy, thaO);
                        CellColor(22, acCounter, valObject.PassBlocking, pbkO);
                        CellColor(23, acCounter, valObject.RunBlocking, rbkO);
                        CellColor(24, acCounter, valObject.KickPower, kpO);
                        CellColor(25, acCounter, valObject.KickAccuracy, kaO);
                        CellColor(26, acCounter, valObject.KickReturn, krO);
                       */
                        acCounter = acCounter + 1;


                        DataRow di = RosterView.NewRow();
                        // playerHeightComboBox.SelectedIndex = record.Height - 65;
                        valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                        //Reload the overall rating
                        valObject.Overall = valObject.Overall;
                        di["Name"] = valObject.FirstName + " " + valObject.LastName + " VARIANCE";
                        di["Age"] = valObject.Age;
                        di["Exp"] = valObject.YearsPro;
                        di["Ovr"] = valObject.Overall - ovrO;
                        // Would like to include feet/inches
                        di["Hgt"] = valObject.Height;
                        di["Wgt"] = (valObject.Weight + 160) - wgtO;
                        di["Spd"] = valObject.Speed - spdO;
                        di["Acc"] = valObject.Acceleration- accO;
                        di["Agi"] = valObject.Agility - agiO;
                        di["Str"] = valObject.Strength - strO;
                        di["Stm"] = valObject.Stamina - stmO;
                        di["Inj"] = valObject.Injury - injO;
                        di["Tgh"] = valObject.Toughness - tghO;
                        di["Mor"] = valObject.Morale - morO;
                        di["Awr"] = valObject.Awareness - awrO;
                        di["Cat"] = valObject.Catching - catO;
                        di["Car"] = valObject.Carrying - carO;
                        di["Jmp"] = valObject.Jumping - jmpO;
                        di["Btk"] = valObject.BreakTackle - btkO;
                        di["Tkl"] = valObject.Tackle - tklO;
                        di["ThP"] = valObject.ThrowPower - thpO;
                        di["ThA"] = valObject.ThrowAccuracy - thaO;
                        di["Pbk"] = valObject.PassBlocking - pbkO;
                        di["Rbk"] = valObject.RunBlocking - rbkO;
                        di["KP"] = valObject.KickPower - kpO;
                        di["KA"] = valObject.KickAccuracy - kaO;
                        di["KR"] = valObject.KickReturn - krO;

                        RosterView.Rows.Add(di);
                        acCounter = acCounter + 1;


                        //DataRow bg = RosterView.NewRow();
                     /*  
                        bg["Name"] = "--------------------";
                        bg["Age"] = "";
                        bg["Exp"] = "";
                        bg["Ovr"] = "";
                        // Would like to include feet/inches
                        bg["Hgt"] = "";
                        bg["Wgt"] = "";
                        bg["Spd"] = "";
                        bg["Acc"] = "";
                        bg["Agi"] = "";
                        bg["Str"] = "";
                        bg["Stm"] = "";
                        bg["Inj"] = "";
                        bg["Tgh"] = "";
                        bg["Mor"] = "";
                        bg["Awr"] = "";
                        bg["Cat"] = "";
                        bg["Car"] = "";
                        bg["Jmp"] = "";
                        bg["Btk"] = "";
                        bg["Tkl"] = "";
                        bg["ThP"] = "";
                        bg["ThA"] = "";
                        bg["Pbk"] = "";
                        bg["Rbk"] = "";
                        bg["KP"] = "";
                        bg["KA"] = "";
                        bg["KR"] = "";

                        RosterView.Rows.Add(bg);
                        acCounter = acCounter + 1;
*/
                        

                    }
                
          
                
            

            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\Final Progression.txt"), false);
            // First we will write the headers.
            DataTable dt = RosterView;
            int iColCount = dt.Columns.Count;
            sw.Write(dt.Columns[0]);
            sw.Write("\t\t\t");
            for (int i = 1; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write("\t");
                }
            }
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write("\t");
                    }
                }
                sw.Write(sw.NewLine);
                sw.WriteLine();

            }
           
            sw.Write("Current Injuries...");
            sw.WriteLine();

            foreach (PlayerRecord valObject in teamPlayers)
            {
                InjuryRecord injury = model.PlayerModel.GetPlayersInjuryRecord(valObject.PlayerId);

                if (injury != null)
                {
                    if (valObject.PositionId == 0)
                    {
                        Pos = "QB";
                    }
                    else if (valObject.PositionId == 1)
                    {
                        Pos = "HB";
                    }
                    else if (valObject.PositionId == 2)
                    {
                        Pos = "FB";
                    }
                    else if (valObject.PositionId == 3)
                    {
                        Pos = "WR";
                    }
                    else if (valObject.PositionId == 4)
                    {
                        Pos = "TE";
                    }
                    else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                    {
                        Pos = "OL";
                    }
                    else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                    {
                        Pos = "DL";
                    }
                    else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                    {
                        Pos = "LB";
                    }
                    else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                    {
                        Pos = "DB";
                    }
                    else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                    {
                        Pos = "KP";
                    }

                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                    string TotalsContents = sr.ReadToEnd();
                    sr.Close();
                    string[] TotalsContentsLine = TotalsContents.Split(',');
                    int DaysActive = int.Parse(TotalsContentsLine[0]);
                    string InjuryType = TotalsContentsLine[1];
                    int InjuryLength = int.Parse(TotalsContentsLine[2]);
                    string duration = "";
                    if (InjuryLength == 91)
                    {
                        duration = "out for season";
                        sw.WriteLine(valObject.FirstName + " " + valObject.LastName + ", " + InjuryType + ", duration: " + duration);
                    }
                    else if ((InjuryLength != 91) & (InjuryLength != 0))
                    {
                        sw.WriteLine(valObject.FirstName + " " + valObject.LastName + ", " + InjuryType + ", duration: " + InjuryLength);
                        sw.WriteLine();
                    }





                }



            }

       
            sw.Close();
            depthChartDataGrid.CurrentCell = depthChartDataGrid[0, 0];
        }//end
       
        private void HighLowCheck(int Skill)
        {
            Rating = Skill;
            if ((int)Skill > 99)
            { Rating = 99; }
            else if ((int)Skill < 0)
            { Rating = 0; }
        }
        private void AttributeMeans(int PosId)
        {
            SpdMean = 0;
            AccMean = 0;
            AgiMean = 0;
            StrMean = 0;
            StmMean = 0;
            InjMean = 0;
            PosWgtMod = 0;

            if (PosId == 0)
            {
                SpdMean = 63;
                AccMean = 66;
                AgiMean = 66;
                StrMean = 60;
                StmMean = 89;
                InjMean = 83;
                PosWgtMod = 1;
            }
            else if (PosId == 1)
            {
                SpdMean = 90;
                AccMean = 92;
                AgiMean = 90;
                StrMean = 70;
                StmMean = 85;
                InjMean = 85;
                PosWgtMod = .9;
            }
            else if (PosId == 2)
            {
                SpdMean = 72;
                AccMean = 81;
                AgiMean = 68;
                StrMean = 76;
                StmMean = 78;
                InjMean = 74;
                PosWgtMod = 1.15;
            }
            else if (PosId == 3)
            {
                SpdMean = 92;
                AccMean = 94;
                AgiMean = 92;
                StrMean = 56;
                StmMean = 90;
                InjMean = 85;
                PosWgtMod = .5;
            }
            else if (PosId == 4)
            {
                SpdMean = 74;
                AccMean = 81;
                AgiMean = 74;
                StrMean = 72;
                StmMean = 86;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 5)
            {
                SpdMean = 55;
                AccMean = 75;
                AgiMean = 57;
                StrMean = 90;
                StmMean = 80;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 6)
            {
                SpdMean = 55;
                AccMean = 75;
                AgiMean = 57;
                StrMean = 90;
                StmMean = 80;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 7)
            {
                SpdMean = 55;
                AccMean = 75;
                AgiMean = 57;
                StrMean = 90;
                StmMean = 80;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 8)
            {
                SpdMean = 55;
                AccMean = 75;
                AgiMean = 57;
                StrMean = 90;
                StmMean = 80;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 9)
            {
                SpdMean = 55;
                AccMean = 75;
                AgiMean = 57;
                StrMean = 90;
                StmMean = 80;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 10)
            {
                SpdMean = 73;
                AccMean = 79;
                AgiMean = 71;
                StrMean = 84;
                StmMean = 80;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 11)
            {
                SpdMean = 73;
                AccMean = 79;
                AgiMean = 71;
                StrMean = 84;
                StmMean = 80;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 12)
            {
                SpdMean = 64;
                AccMean = 80;
                AgiMean = 71;
                StrMean = 84;
                StmMean = 80;
                InjMean = 80;
                PosWgtMod = 1.2;
            }
            else if (PosId == 13)
            {
                SpdMean = 83;
                AccMean = 85;
                AgiMean = 83;
                StrMean = 75;
                StmMean = 86;
                InjMean = 83;
                PosWgtMod = 1.05;
            }
            else if (PosId == 14)
            {
                SpdMean = 83;
                AccMean = 85;
                AgiMean = 83;
                StrMean = 75;
                StmMean = 86;
                InjMean = 80;
                PosWgtMod = 1.05;
            }
            else if (PosId == 15)
            {
                SpdMean = 83;
                AccMean = 85;
                AgiMean = 83;
                StrMean = 75;
                StmMean = 86;
                InjMean = 83;
                PosWgtMod = 1.05;
            }
            else if (PosId == 16)
            {
                SpdMean = 90;
                AccMean = 92;
                AgiMean = 89;
                StrMean = 57;
                StmMean = 90;
                InjMean = 85;
                PosWgtMod = .5;
            }
            else if (PosId == 17)
            {
                SpdMean = 88;
                AccMean = 90;
                AgiMean = 88;
                StrMean = 63;
                StmMean = 88;
                InjMean = 85;
                PosWgtMod = .75;
            }
            else if (PosId == 18)
            {
                SpdMean = 88;
                AccMean = 90;
                AgiMean = 86;
                StrMean = 63;
                StmMean = 88;
                InjMean = 85;
                PosWgtMod = .8;
            }
            else if (PosId == 19)
            {
                SpdMean = 45;
                AccMean = 48;
                AgiMean = 45;
                StrMean = 26;
                StmMean = 85;
                InjMean = 82;
                PosWgtMod = .25;
            }
            else if (PosId == 20)
            {
                SpdMean = 51;
                AccMean = 57;
                AgiMean = 47;
                StrMean = 33;
                StmMean = 86;
                InjMean = 80;
                PosWgtMod = .25;
            }
        }

        private void ClearMassConditioning()
        {
            enableMassChk.Checked = false;
            teamChk.Checked = false;
            offChk.Checked = false;
            defChk.Checked = false;
            enableMassChk.Enabled = false;
            teamChk.Enabled = false;
            offChk.Enabled = false;
            defChk.Enabled = false;
            massCmb.Enabled = false;
        }
        private void EnableMassConditioning()
        {
            enableMassChk.Enabled = true;

        }


        private void enableMassChk_CheckedChanged(object sender, EventArgs e)
        {
            if (enableMassChk.Checked == true)
            {
                
                    teamChk.Enabled = true;
                    offChk.Enabled = true;
                    defChk.Enabled = true;
                    massCmb.Enabled = true;
               
            }
            else if (enableMassChk.Checked == false)
            {

                teamChk.Enabled = false;
                offChk.Enabled = false;
                defChk.Enabled = false;
                massCmb.Enabled = false;

            }



        }

        private void massCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (teamChk.Checked == true)
            {
                string Pos = ""; 
                StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
                int CurTeamIndex = int.Parse(ct.ReadLine());
                ct.Close();
                int teamId = CurTeamIndex;
                int positionId = filterPositionComboBox.SelectedIndex;
                List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
                //Ind. Position view
                foreach (PlayerRecord valObject in teamPlayers)
                {
                   
                        if (valObject.PositionId == 0)
                        {
                            Pos = "QB";
                        }
                        else if (valObject.PositionId == 1)
                        {
                            Pos = "HB";
                        }
                        else if (valObject.PositionId == 2)
                        {
                            Pos = "FB";
                        }
                        else if (valObject.PositionId == 3)
                        {
                            Pos = "WR";
                        }
                        else if (valObject.PositionId == 4)
                        {
                            Pos = "TE";
                        }
                        else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                        {
                            Pos = "OL";
                        }
                        else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                        {
                            Pos = "DL";
                        }
                        else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                        {
                            Pos = "LB";
                        }
                        else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                        {
                            Pos = "DB";
                        }
                        else if (valObject.PositionId == 19 | valObject.PositionId == 20)
                        {
                            Pos = "KP";
                        }
                        bool proceed = false;
                        if (CurDay > 2)
                        {
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                            string TotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] TotalsContentsLine = TotalsContents.Split(',');
                            if (TotalsContentsLine[1] == "")
                            {
                                proceed = true;
                            }


                        }
                        if ((proceed == true) | (CurDay <= 2))
                        {
                            string Time = (string)massCmb.SelectedItem;
                            CurName = valObject.FirstName + " " + valObject.LastName;
                            if (Time != null)
                            {
                                string[] splitLine = Time.Split('%');
                                CurPercent = Int32.Parse(splitLine[0]);
                                SaveCurrentAllocationsMassConditioning(Pos);
                            }

                        }
                }
                CurName = (string)SetTimeGrd.Rows[0].Cells["Name"].Value;
            }

            else if (offChk.Checked == true)
            {
                string Pos = "";
                StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
                int CurTeamIndex = int.Parse(ct.ReadLine());
                ct.Close();
                int teamId = CurTeamIndex;
                int positionId = filterPositionComboBox.SelectedIndex;
                List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
                //Ind. Position view
                foreach (PlayerRecord valObject in teamPlayers)
                {
                   
                        if (valObject.PositionId == 0)
                        {
                            Pos = "QB";
                        }
                        else if (valObject.PositionId == 1)
                        {
                            Pos = "HB";
                        }
                        else if (valObject.PositionId == 2)
                        {
                            Pos = "FB";
                        }
                        else if (valObject.PositionId == 3)
                        {
                            Pos = "WR";
                        }
                        else if (valObject.PositionId == 4)
                        {
                            Pos = "TE";
                        }
                        else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                        {
                            Pos = "OL";
                        }
                        bool proceed = false;
                        if (CurDay > 2)
                        {
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                            string TotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] TotalsContentsLine = TotalsContents.Split(',');
                            if (TotalsContentsLine[1] == "")
                            {
                                proceed = true;
                            }

                        }
                        if ((proceed == true) | (CurDay <= 2))
                        {
                            string Time = (string)massCmb.SelectedItem;
                            CurName = valObject.FirstName + " " + valObject.LastName;
                            if ((Time != null) & (valObject.PositionId < 10))
                            {
                                string[] splitLine = Time.Split('%');
                                CurPercent = Int32.Parse(splitLine[0]);

                                SaveCurrentAllocationsMassConditioning(Pos);
                            }
                        }
                }
                CurName = (string)SetTimeGrd.Rows[0].Cells["Name"].Value;
            }

            else if (defChk.Checked == true)
            {
                string Pos = "";
                StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
                int CurTeamIndex = int.Parse(ct.ReadLine());
                ct.Close();
                int teamId = CurTeamIndex;
                int positionId = filterPositionComboBox.SelectedIndex;
                List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
                //Ind. Position view
                foreach (PlayerRecord valObject in teamPlayers)
                {
                    
                        if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                        {
                            Pos = "DL";
                        }
                        else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                        {
                            Pos = "LB";
                        }
                        else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                        {
                            Pos = "DB";
                        }
                        bool proceed = false;
                        if (CurDay > 2)
                        {
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                            string TotalsContents = sr.ReadToEnd();
                            sr.Close();
                            string[] TotalsContentsLine = TotalsContents.Split(',');
                            if (TotalsContentsLine[1] == "")
                            {
                                proceed = true;
                            }

                        }
                        if ((proceed == true) | (CurDay <= 2))
                        {
                            string Time = (string)massCmb.SelectedItem;
                            CurName = valObject.FirstName + " " + valObject.LastName;
                            if ((Time != null) & (valObject.PositionId >= 10) & (valObject.PositionId <= 18))
                            {
                                string[] splitLine = Time.Split('%');
                                CurPercent = Int32.Parse(splitLine[0]);
                                SaveCurrentAllocationsMassConditioning(Pos);

                            }

                        }
                }
                CurName = (string)SetTimeGrd.Rows[0].Cells["Name"].Value;
            }

            RefreshCurrent();
        }

        private void offChk_CheckedChanged(object sender, EventArgs e)
        {
            if (teamChk.Checked == true)
            {
                teamChk.Checked = false;
            }
            if (defChk.Checked == true)
            {
                offChk.Checked = false;
                defChk.Checked = false;
                teamChk.Checked = true;
            }
        }

        private void defChk_CheckedChanged(object sender, EventArgs e)
        {
            if (teamChk.Checked == true)
            {
                teamChk.Checked = false;
            }
            if (offChk.Checked == true)
            {
                offChk.Checked = false;
                defChk.Checked = false;
                teamChk.Checked = true;
            }
        }

       
        private void teamChk_CheckedChanged(object sender, EventArgs e)
        {
            if ((offChk.Checked == true) | (defChk.Checked == true))
            {
                offChk.Checked = false;
                defChk.Checked = false;
                teamChk.Checked = true;
            }
        }
        
       
        private void SetTimeGrd_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            {
                if ((SetTimeGrd.Rows.Count > 0) & (e.RowIndex >= 0) & (CurDay > 2)) 
                {
                    
                   
                        string installDirectory = Application.StartupPath;
                        StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\System\\currentteam");
                        int CurTeamIndex = int.Parse(ct.ReadLine());
                        ct.Close();
                        int teamId = CurTeamIndex;
                        int positionId = filterPositionComboBox.SelectedIndex;
                        string Pos = filterPositionComboBox.SelectedText;

                        CurName = (string)SetTimeGrd.Rows[e.RowIndex].Cells["Name"].Value;   //(string)SetTimeGrd.Rows[0].Cells["Name"].Value;
                        List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);


                        foreach (PlayerRecord valObject in teamPlayers)
                        {
                            if ((valObject.FirstName + " " + valObject.LastName) == CurName)
                            {
                                Pos = filterPositionComboBox.Text;
                                // playerHeightComboBox.SelectedIndex = record.Height - 65;
                                if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                                {
                                    Pos = "OL";
                                }
                                else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                                {
                                    Pos = "DL";
                                }
                                else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                                {
                                    Pos = "LB";
                                }
                                else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                                {
                                    Pos = "DB";
                                }
                                else if (Pos == "K" | Pos == "P")
                                {
                                    Pos = "KP";
                                }
                                //current totals
                                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                                string TotalsContents = sr.ReadToEnd();
                                sr.Close();
                                string[] TotalsContentsLine = TotalsContents.Split(',');
                                string inty = "";
                                string[] dump = TotalsContentsLine[1].Split(';');
                                if (TotalsContentsLine[1] == ";is throwing up all over the place. Why on earth did you mix his diet like that? Either put him on a weight loss diet or a weight gain diet. Not both.")
                                {
                                    inty = ("an upset stomach due to unhealthy dieting.");
                                    e.ToolTipText = "The pink background denotes that " + valObject.LastName + " is unable to practice today.\nHe's out " + int.Parse(TotalsContentsLine[2]) + " day(s) and is suffering from " + inty;

                                }
                                else if (TotalsContentsLine[1] ==  "; has a head cold and will miss todays practice.")
                                {
                                    inty = ("a head cold.");
                                    e.ToolTipText = "The pink background denotes that " + valObject.LastName + " is unable to practice today.\nHe's out " + int.Parse(TotalsContentsLine[2]) + " day(s) and is suffering from " + inty;

                                }
                                else if ((dump.Length >= 2) & (int.Parse(TotalsContentsLine[2]) == 91))
                                {
                                    inty = "a " + dump[1];
                                    e.ToolTipText = "The pink background denotes that " + valObject.LastName + " is unable to practice today.\nHe's out the entire season suffering from " + inty;
                                }
                                else if (dump.Length >= 2) 
                                {
                                    inty = "a " + dump[1];
                                    e.ToolTipText = "The pink background denotes that " + valObject.LastName + " is unable to practice today.\nHe's out " + int.Parse(TotalsContentsLine[2]) + " day(s) and is suffering from " + inty;
                                }
                                else
                                {
                                    e.ToolTipText = valObject.LastName + " is healthy and ready to practice.";

                                }
                            //    MessageBox.Show("The pink background denotes that " + valObject.LastName + " is unable to practice today. He's out " + int.Parse(TotalsContentsLine[2]) + " day(s) and is suffering from " + inty);

                                                                
                                /*
                                toolTip1.SetToolTip(SetTimeGrd, "The pink background denotes that " + valObject.LastName + " is unable to practice today. He's out " + int.Parse(TotalsContentsLine[2]) + " day(s) and is suffering from " + inty);
                                toolTip1.Active = true;
                                toolTip1.InitialDelay = 100;
                                 */

                            }
                        }


                    
                }


            }
        }      
       
        private void simCPUCampsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Simulate Training Camp for all CPU teams?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (dr == DialogResult.No)
            {
                return;
            }
            else if (dr == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                CPUtuneCompile();
                SetTimeGrd.Columns.Remove(TrainingTime);
                InitializeDataGrids();
                foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
                {
                    CPUteam = (OwnerRecord)team;

                    if (CPUteam.UserControlled == false)
                    {
                        ProcessCPU();
                        
                    }
                }
                File.Delete(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsim");
                File.Delete(installDirectory + "\\Conditioning\\TrainingCamp\\CPUsimInjury");
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("CPU Simulation is now complete. A .txt file has been generated for all CPU teams in the installation directory within the Conditioning/TrainingCamp/CPUSimulation/ folder.\nYou may now close this form. If you still need to run Training Camp on human teams simply do so now.", "", MessageBoxButtons.OK, MessageBoxIcon.None);


            }


        }

        private void ActivityGrd_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {   
                if ((ActivityGrd.Rows.Count > 0) & (e.RowIndex >= 0))
                {
            string CurName = (string)ActivityGrd.Rows[e.RowIndex].Cells["Activity"].Value;
            string tooltp = "";
            string Pos = filterPositionComboBox.Text;

                    string installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");
                    
                    if (ActivityCmb.Text == "Position Drills")
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line != "")
                            {
                                string[] splitLine = line.Split(';');
                                //Positional
                                if ((splitLine[1] == Pos + "-P") & (splitLine[2] == CurName))
                                {
                                    string[] splitValues = splitLine[4].Split('|');
                                    //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                    tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                        ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                        ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                        ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];
                                }
                                else
                                {
                                    if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                                    {
                                        if ((splitLine[1] == "OL-P") & (splitLine[2] == CurName))
                                        {
                                            string[] splitValues = splitLine[4].Split('|');
                                            //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                            tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                                ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                                ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                                ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];

                                        }
                                    }
                                    else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                                    {
                                        if ((splitLine[1] == "DL-P") & (splitLine[2] == CurName))
                                        {
                                            string[] splitValues = splitLine[4].Split('|');
                                            //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                            tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                                ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                                ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                                ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];

                                        }
                                    }
                                    else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                                    {
                                        if ((splitLine[1] == "LB-P") & (splitLine[2] == CurName))
                                        {
                                            string[] splitValues = splitLine[4].Split('|');
                                            //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                            tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                                ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                                ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                                ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];

                                        }
                                    }
                                    else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                                    {
                                        if ((splitLine[1] == "DB-P") & (splitLine[2] == CurName))
                                        {
                                            string[] splitValues = splitLine[4].Split('|');
                                            //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                            tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                                ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                                ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                                ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];

                                        }
                                    }
                                    else if (Pos == "K" | Pos == "P")
                                    {
                                        if ((splitLine[1] == "KP-P") & (splitLine[2] == CurName))
                                        {
                                            string[] splitValues = splitLine[4].Split('|');
                                            //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                            tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                                ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                                ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                                ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];

                                        }
                                    }

                                }
                            }
                        }//end while
                    }//end position
                    else if ((ActivityCmb.Text == "Dietary") || (ActivityCmb.Text == "Weight Training") || (ActivityCmb.Text == "Aerobic/Cardio"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line != "")
                            {
                                string[] splitLine = line.Split(';');
                                //Positional
                                if ((splitLine[2] == CurName))
                                {
                                    string[] splitValues = splitLine[4].Split('|');
                                    //wgt,spd,acc,agi,str,stm,inj,tgh,mor,awr,cat,car,jmp,btk,tkl,thp,tha,pbk,rbk,kp,ka,kr,inj_chance
                                    tooltp = "Wgt=" + splitValues[1] + ",Spd=" + splitValues[2] + ",Acc=" + splitValues[3] + ",Agi=" + splitValues[4] + ",Str=" + splitValues[5] +
                                        ",Stm=" + splitValues[6] + ",Inj=" + splitValues[7] + ",Tgh=" + splitValues[8] + ",Mor=" + splitValues[9] + ",Awr=" + splitValues[10] + ",Cat=" + splitValues[11] +
                                        ",Car=" + splitValues[12] + ",Jmp=" + splitValues[13] + ",Btk=" + splitValues[14] + ",Tkl=" + splitValues[15] + ",Thp=" + splitValues[16] + ",Tha=" + splitValues[17] +
                                        ",Pbk=" + splitValues[18] + ",Rbk=" + splitValues[19] + ",kp=" + splitValues[20] + ",ka=" + splitValues[21] + ",kr=" + splitValues[22] + ",Inj_chance=" + splitValues[23];
                                }


                            }
                        }
                    }//end while
                    

                    sr.Close();
                   
                    e.ToolTipText = tooltp;


                }
        }


        
 

          
            





















       
       
  
      

        
    }

  }