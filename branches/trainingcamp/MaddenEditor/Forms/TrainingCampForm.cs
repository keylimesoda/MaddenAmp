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

namespace MaddenEditor.Forms
{
    public partial class TrainingCampForm : Form
    {
        private EditorModel model = null;
        private PlayerRecord lastLoadedRecord = null;
        private bool isInitialising = false;
        private DepthChartEditingModel depthEditingModel = null;      

        DataTable RosterView = new DataTable();
        BindingSource RosterViewBinding = new BindingSource();

        string installDirectory;
     //   string profile;
     //   List<string[]> ratingsVersions;

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
                    //Determine OffSeason conditioning
                  

                    //Create output directory if none exists
                    if (!Directory.Exists(installDirectory + "\\TrainingCamp"))
                    {
                        Directory.CreateDirectory(installDirectory + "\\TrainingCamp");
                    }

                    installDirectory = Application.StartupPath;
            
                    depthEditingModel = new DepthChartEditingModel(model);      
                    isInitialising = true;                                      
                    foreach (TeamRecord team in model.TeamModel.GetTeams())
                    {
                        selectHumanTeam.Items.Add(team);
                    }
                  selectHumanTeam.Items.RemoveAt(34);
                  selectHumanTeam.Items.RemoveAt(33);
                  selectHumanTeam.Items.RemoveAt(32);  

                    foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
                    {
                        filterPositionComboBox.Items.Add(pos);
                    }  
                                                          
                    isInitialising = false;

                   
                    
                }
                public void CleanUI()
                {
                    selectHumanTeam.Items.Clear();
                    filterPositionComboBox.Items.Clear();
                   //attributeCombo.Items.Clear();
                }
                #endregion

       

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

           
            //Set Team combo box to record 0
            selectHumanTeam.SelectedIndex = 0;
            filterPositionComboBox.SelectedIndex = 0;

        //    model.PlayerModel.SetTeamFilter(selectHumanTeam.Text);
         //   model.PlayerModel.SetPositionFilter(filterPositionComboBox.SelectedIndex);
         //   model.PlayerModel.GetNextPlayerRecord();
           isInitialising = false;
           
            RefillRosterView();
            
        }

        private void RefillRosterView()
        {
        
            isInitialising = true;

            int teamId = ((TeamRecord)selectHumanTeam.SelectedItem).TeamId;
            int positionId = filterPositionComboBox.SelectedIndex;

            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
        
            RosterView.Clear();

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
            isInitialising = false;
        }
   

        private void TrainingCampForm_Load(object sender, EventArgs e)
        {
            InitializeDataGrids();
        }
       

        private void selectHumanTeam_SelectedIndexChanged(object sender, EventArgs e)
        {              
            if (!isInitialising)
            {
                RefillRosterView();
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
            }
        
          
           
        }

        
    }
}