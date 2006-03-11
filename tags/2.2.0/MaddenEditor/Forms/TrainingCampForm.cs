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
        public int CurDay;
        public string CurTeam;
        public string Stage;
        public TrainingCampSplashScreen trainingCampSplashScreen = null;
        public TrainingCampMeeting trainingCampMeeting = null;
        public TeamRecord tn;
        DataTable RosterView = new DataTable();
        BindingSource RosterViewBinding = new BindingSource();
        DataTable ActivityView = new DataTable();
        BindingSource ActivityViewBinding = new BindingSource();
        DataTable AllocateTimingView = new DataTable();
        BindingSource AllocateTimingViewBinding = new BindingSource();

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
                    int sdfsd = 0;
                    foreach (string pos in Enum.GetNames(typeof(MaddenPositionGroups)))
                    {
                        filterPositionComboBox.Items.Add(pos);
                        sdfsd++;
                    }

                    //Populate Activity combobox
                    ActivityCmb.Items.Add("Position Drills");
                    ActivityCmb.Items.Add("Aerobic/Cardio");
                    ActivityCmb.Items.Add("Weight Training");
                    ActivityCmb.Items.Add("Dietary");
                    ActivityCmb.Items.Add("Team Drills");
                    //Populate AllocateBy combobox
                    AllocateByCmb.Items.Add("Team");
                    AllocateByCmb.Items.Add("Position");
                    AllocateByCmb.Items.Add("Individual");
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
                string[] splitLine = line.Split('"');
                //Positional
                if (splitLine[1] == Pos + "-P")
                {
                   ac["Activity"] = splitLine[3];
                   ac["Description"] = splitLine[5];
                   ActivityView.Rows.Add(ac);
                }
                else
                {
                    if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                    {
                        if (splitLine[1] == "OL-P")
                        {
                            ac["Activity"] = splitLine[3];
                            ac["Description"] = splitLine[5];
                            ActivityView.Rows.Add(ac);
                        }
                    }
                    else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                    {
                        if (splitLine[1] == "DL-P")
                        {
                            ac["Activity"] = splitLine[3];
                            ac["Description"] = splitLine[5];
                            ActivityView.Rows.Add(ac);
                        }
                    }
                    else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                    {
                        if (splitLine[1] == "LB-P")
                        {
                            ac["Activity"] = splitLine[3];
                            ac["Description"] = splitLine[5];
                            ActivityView.Rows.Add(ac);
                        }
                    }
                    else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                    {
                        if (splitLine[1] == "DB-P")
                        {
                            ac["Activity"] = splitLine[3];
                            ac["Description"] = splitLine[5];
                            ActivityView.Rows.Add(ac);
                        }
                    }
                    else if (Pos == "K" | Pos == "P")
                    {
                        if (splitLine[1] == "KP-P")
                        {
                            ac["Activity"] = splitLine[3];
                            ac["Description"] = splitLine[5];
                            ActivityView.Rows.Add(ac);
                        }
                    }
                    
                }                
            }//end while
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
                string[] splitLine = line.Split('"');
                //Misc                
                if (splitLine[1] == Type)
                {
                    ac["Activity"] = splitLine[3];
                    ac["Description"] = splitLine[5];
                    ActivityView.Rows.Add(ac);
                }               

            }
            //end while
            sr.Close();

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
		private void depthChartDataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			
		}

		private void depthChartDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}

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

        private void RefillRosterView()
        {
        
            isInitialising = true;

            int teamId = ((TeamRecord)selectHumanTeam.SelectedItem).TeamId;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
        
            RosterView.Clear();
            //Ind. Position view
            if ((filterPositionComboBox.SelectedIndex >= 0) & (filterPositionComboBox.SelectedIndex <= 20))
            {
                foreach (PlayerRecord valObject in teamPlayers)
                {
                    if (valObject.PositionId == positionId)
                    {
                        DataRow dr = RosterView.NewRow();

                        // playerHeightComboBox.SelectedIndex = record.Height - 65;

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
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 22)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 5) || (valObject.PositionId == 9))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 23)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 6) || (valObject.PositionId == 8))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 24)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11) || (valObject.PositionId == 12))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 25)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }

                if (filterPositionComboBox.SelectedIndex == 26)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 14) || (valObject.PositionId == 15))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 27)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 15))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 28)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 16) || (valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }
                if (filterPositionComboBox.SelectedIndex == 29)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 17) || (valObject.PositionId == 18))
                        {
                            DataRow dr = RosterView.NewRow();

                            // playerHeightComboBox.SelectedIndex = record.Height - 65;

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

                        }
                    }

                }




               
            }




            // Sort the current view after running foreach loop unless view empty
            if (RosterView.Rows.Count != 0)
            {
                depthChartDataGrid.Sort(depthChartDataGrid.Columns["Ovr"], ListSortDirection.Descending);
              // depthChartDataGrid.CurrentRow = depthChartDataGrid[0, 0];            
            }
            else
            {
                DataRow NoEntries = RosterView.NewRow();
                NoEntries["Name"] = "No Players";
                RosterView.Rows.Add(NoEntries); 
            }

            RefillAllocateTimingView(filterPositionComboBox.Text);

            isInitialising = false;
        }
        private void FillTextBox(string Pos)
        {
            isInitialising = true;

            int teamId = ((TeamRecord)selectHumanTeam.SelectedItem).TeamId;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);


            foreach (PlayerRecord valObject in teamPlayers) 
            {
                if ((valObject.PositionId == positionId) & ((valObject.FirstName + " " + valObject.LastName) == CurName))
                {
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


                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
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

            isInitialising = true;

            int teamId = ((TeamRecord)selectHumanTeam.SelectedItem).TeamId;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            AllocateTimingView.Clear();
            if ((filterPositionComboBox.SelectedIndex >= 0) & (filterPositionComboBox.SelectedIndex <= 20))
            {
               

                foreach (PlayerRecord valObject in teamPlayers)
                {
                    if (valObject.PositionId == positionId)
                    {
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
                        StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 22)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 5) || (valObject.PositionId == 9))
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


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 23)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 6) || (valObject.PositionId == 8))
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


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 24)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11) || (valObject.PositionId == 12))
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

                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 25)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 10) || (valObject.PositionId == 11))
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


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 26)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 14) || (valObject.PositionId == 15))
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
                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 27)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 13) || (valObject.PositionId == 15))
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

                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 28)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 16) || (valObject.PositionId == 17) || (valObject.PositionId == 18))
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


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                        }

                    }

                }
                if (filterPositionComboBox.SelectedIndex == 29)
                {
                    foreach (PlayerRecord valObject in teamPlayers)
                    {
                        if ((valObject.PositionId == 17) || (valObject.PositionId == 18))
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


                            dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                            installDirectory = Application.StartupPath;
                            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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

                tn = (TeamRecord)selectHumanTeam.SelectedItem;
        }
        private void selectHumanTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
                  //      this.Hide();
                        LaunchSplash();
                        selectHumanTeam.Enabled = false;
                        filterPositionComboBox.Enabled = true;
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
                    filterPositionComboBox.SelectedIndex = 0;
                    selectHumanTeam.Enabled = false;
                    filterPositionComboBox.Enabled = true;
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
             //       this.Hide();
                    LaunchSplash();
                    filterPositionComboBox.Enabled = true;
                    selectHumanTeam.Enabled = false;
                    //  AdvanceBtn.Text = "Start...";
                    //  groupBox6.Enabled = true;

                }
                else if (drr == DialogResult.No)
                {
                    return;
                }
               

            }
         
        }
        private void SetTimingCombo()
        {
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
        private void SaveCurrentAllocations(string Pos)
        {

            isInitialising = true;

            int teamId = ((TeamRecord)selectHumanTeam.SelectedItem).TeamId;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            AllocateTimingView.Clear();

            foreach (PlayerRecord valObject in teamPlayers)
            {
                if ((valObject.PositionId == positionId) & ((valObject.FirstName + " " + valObject.LastName) == (CurName)))
                {
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

                    string Allcontents = "";
                    string TotalRemaining = "";
                    int difference = 0;

                    dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                    installDirectory = Application.StartupPath;
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);

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
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    
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
                    sw.Write(CurrentActivity + "," + CurPercent);
                    sw.WriteLine();
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

        private void rightButton_Click(object sender, EventArgs e)
        {
            
            {

               
                LoadPlayerInfo(model.PlayerModel.GetNextPlayerRecord());
            }
            
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            
            {
                LoadPlayerInfo(model.PlayerModel.GetPreviousPlayerRecord());
            }
              
        }

        private void depthChartDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void filterPositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (!isInitialising)
            {
                RefillRosterView();
                ActivityCmb.Enabled = true;
           //     CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
           //     ActivityLbl.Text = CurrentActivity;
            }
            if (ActivityCmb.SelectedIndex == 0)
            {
                RefillRosterView();
                LoadPositionDrills(filterPositionComboBox.Text);
           //     CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
           //     ActivityLbl.Text = CurrentActivity;
            }
            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
            {
                RefillRosterView();
                LoadConditioning();
            //    CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
            //    ActivityLbl.Text = CurrentActivity;
            }
           
        }

        private void ActivityCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActivityCmb.SelectedIndex == 0)
            {
                LoadCampActivityNames();
                CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
                ActivityLbl.Text = CurrentActivity;
            }
            else if ((ActivityCmb.SelectedIndex >= 1) & (ActivityCmb.SelectedIndex <= 3))
            {
                LoadConditioning();
                CurrentActivity = (string)ActivityGrd.Rows[0].Cells["Activity"].Value;
                ActivityLbl.Text = CurrentActivity;
            }

        }

        private void ActivityGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) & (e.ColumnIndex >= 0))
            {
                CurrentActivity = (string)ActivityGrd.Rows[e.RowIndex].Cells["Activity"].Value;
                ActivityLbl.Text = CurrentActivity;
                RefillAllocateTimingView(filterPositionComboBox.Text);
                FillTextBox(filterPositionComboBox.Text);
            }
        }
       
        private void SetTimeGrd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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

        private void SetTimeGrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurName = (string)SetTimeGrd.Rows[e.RowIndex].Cells["Name"].Value;
            FillTextBox(filterPositionComboBox.Text);
        }
        private void LaunchSplash()
        {
            {
                installDirectory = Application.StartupPath;
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + franchiseFilename + "\\" + selectHumanTeam.Text + "\\System\\system");
                string Allcontents = sr.ReadLine();
                sr.Close();
                string[] Line = Allcontents.Split(',');
                CurTeam = Line[0];
                Stage = Line[1];
                CurDay = Int32.Parse(Line[2]);
                groupBox6.Enabled = false;
                TrainingCampSplashScreen form = new TrainingCampSplashScreen(model, this);
                form.Show();

            }

        }
        private void AdvanceBtn_Click(object sender, EventArgs e)
        {
            LaunchSplash();            
        }

  
      

        
    }
}