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
    public partial class TrainingCampTuneGUI : Form
    {
        DataTable RosterView = new DataTable();
        BindingSource RosterViewBinding = new BindingSource();

        public TrainingCampTuneGUI(EditorModel model)
        {
            InitializeComponent();
            InitializeDataGrids();
            LoadActivities();
        }

        private DataColumn AddColumn(string ColName, string ColType)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = ColName;
            dc.DataType = System.Type.GetType(ColType);
            return dc;
        }

        private void InitializeDataGrids()
        {
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp");
            }

            // isInitialising = true;
            RosterView.Columns.Add(AddColumn("Type", "System.String"));
            RosterView.Columns.Add(AddColumn("Activity", "System.String"));
            RosterView.Columns.Add(AddColumn("Description", "System.String"));
            RosterView.Columns.Add(AddColumn("Wgt", "System.String"));
            RosterView.Columns.Add(AddColumn("Spd", "System.String"));
            RosterView.Columns.Add(AddColumn("Acc", "System.String"));
            RosterView.Columns.Add(AddColumn("Agi", "System.String"));
            RosterView.Columns.Add(AddColumn("Str", "System.String"));
            RosterView.Columns.Add(AddColumn("Stm", "System.String"));
            RosterView.Columns.Add(AddColumn("Inj", "System.String"));
            RosterView.Columns.Add(AddColumn("Tgh", "System.String"));
            RosterView.Columns.Add(AddColumn("Mor", "System.String"));
            RosterView.Columns.Add(AddColumn("Awr", "System.String"));
            RosterView.Columns.Add(AddColumn("Cat", "System.String"));
            RosterView.Columns.Add(AddColumn("Car", "System.String"));
            RosterView.Columns.Add(AddColumn("Jmp", "System.String"));
            RosterView.Columns.Add(AddColumn("Btk", "System.String"));
            RosterView.Columns.Add(AddColumn("Tkl", "System.String"));
            RosterView.Columns.Add(AddColumn("ThP", "System.String"));
            RosterView.Columns.Add(AddColumn("ThA", "System.String"));
            RosterView.Columns.Add(AddColumn("Pbk", "System.String"));
            RosterView.Columns.Add(AddColumn("Rbk", "System.String"));
            RosterView.Columns.Add(AddColumn("KP", "System.String"));
            RosterView.Columns.Add(AddColumn("KA", "System.String"));
            RosterView.Columns.Add(AddColumn("KR", "System.String"));
            RosterView.Columns.Add(AddColumn("Inj_Chance", "System.String"));

            RosterViewBinding.DataSource = RosterView;
            TuneModifierGrd.DataSource = RosterViewBinding;

            //Control Column Width
            int ColWidth = 32;

            TuneModifierGrd.Columns["Type"].Width = 100;
            TuneModifierGrd.Columns["Activity"].Width = 150;
            TuneModifierGrd.Columns["Description"].Width = 250;
            TuneModifierGrd.Columns["Wgt"].Width = ColWidth;
            TuneModifierGrd.Columns["Spd"].Width = ColWidth;
            TuneModifierGrd.Columns["Acc"].Width = ColWidth;
            TuneModifierGrd.Columns["Agi"].Width = ColWidth;
            TuneModifierGrd.Columns["Str"].Width = ColWidth;
            TuneModifierGrd.Columns["Stm"].Width = ColWidth;
            TuneModifierGrd.Columns["Inj"].Width = ColWidth;
            TuneModifierGrd.Columns["Tgh"].Width = ColWidth;
            TuneModifierGrd.Columns["Mor"].Width = ColWidth;
            TuneModifierGrd.Columns["Awr"].Width = ColWidth;
            TuneModifierGrd.Columns["Cat"].Width = ColWidth;
            TuneModifierGrd.Columns["Car"].Width = ColWidth;
            TuneModifierGrd.Columns["Jmp"].Width = ColWidth;
            TuneModifierGrd.Columns["Btk"].Width = ColWidth;
            TuneModifierGrd.Columns["Tkl"].Width = ColWidth;
            TuneModifierGrd.Columns["ThP"].Width = ColWidth;
            TuneModifierGrd.Columns["ThA"].Width = ColWidth;
            TuneModifierGrd.Columns["Pbk"].Width = ColWidth;
            TuneModifierGrd.Columns["Rbk"].Width = ColWidth;
            TuneModifierGrd.Columns["KP"].Width = ColWidth;
            TuneModifierGrd.Columns["KA"].Width = ColWidth;
            TuneModifierGrd.Columns["KR"].Width = ColWidth;
            TuneModifierGrd.Columns["Inj_Chance"].Width = 40;
            TuneModifierGrd.Columns["Type"].ReadOnly = true;

        }
        private void LoadActivities()
        {
            string installDirectory = Application.StartupPath;

            string fileName = (installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");
            string[] splitname = fileName.Split('.');
            string backupFile = splitname[0] + "-backup";
            string newFile;
         //   File.Delete(backupFile + "0.fra");
            int index = 0;
            while (true)
            {
                newFile = backupFile + index + ".txt";
                if (!File.Exists(newFile))
                {
                    break;
                }
                index++;
            }

            File.Copy(fileName, newFile);

            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\tune.txt");

            RosterView.Clear();
            while (!sr.EndOfStream)
            {
                DataRow ac = RosterView.NewRow();
                string line = sr.ReadLine();
                if (line != "")
                {
                    string[] splitLine = line.Split(';');
                    string[] attributes = line.Split('|');
                    //Positional
                    string Pos = "";
                    if (splitLine[1] == "QB-P")
                    {
                        Pos = "Positional-QB";
                    }
                    else  if (splitLine[1] == "HB-P")
                    {
                        Pos = "Positional-HB";
                    }
                    else if (splitLine[1] == "FB-P")
                    {
                        Pos = "Positional-FB";
                    }
                    else if (splitLine[1] == "WR-P")
                    {
                        Pos = "Positional-WR";
                    }
                    else if (splitLine[1] == "TE-P")
                    {
                        Pos = "Positional-TE";
                    }
                    else if (splitLine[1] == "OL-P")
                    {
                        Pos = "Positional-OL";
                    }
                    else if (splitLine[1] == "DL-P")
                    {
                        Pos = "Positional-DL";
                    }
                    else if (splitLine[1] == "LB-P")
                    {
                        Pos = "Positional-LB";
                    }
                    else if (splitLine[1] == "DB-P")
                    {
                        Pos = "Positional-DB";
                    }
                    else if (splitLine[1] == "KP-P")
                    {
                        Pos = "Positional-KP";
                    }
                    else if (splitLine[1] == "W")
                    {
                        Pos = "Weight Training-All";
                    }
                     else if (splitLine[1] == "A")
                    {
                        Pos = "Aerobic/Cardio-All";
                    }
                     else if (splitLine[1] == "D")
                    {
                        Pos = "Diet-All";
                    }

                        ac["Type"] = Pos;
                        ac["Activity"] = splitLine[2];
                        ac["Description"] = splitLine[3];
                        ac["Wgt"] = (attributes[1]);
                        ac["Spd"] = (attributes[2]);
                        ac["Acc"] = (attributes[3]);
                        ac["Agi"] = (attributes[4]);
                        ac["Str"] = (attributes[5]);
                        ac["Stm"] = (attributes[6]);
                        ac["Inj"] = (attributes[7]);
                        ac["Tgh"] = (attributes[8]);
                        ac["Mor"] = (attributes[9]);
                        ac["Awr"] = (attributes[10]);
                        ac["Cat"] = (attributes[11]);
                        ac["Car"] = (attributes[12]);
                        ac["Jmp"] = (attributes[13]);
                        ac["Btk"] = (attributes[14]);
                        ac["Tkl"] = (attributes[15]);
                        ac["ThP"] = (attributes[16]);
                        ac["ThA"] = (attributes[17]);
                        ac["Pbk"] = (attributes[18]);
                        ac["Rbk"] = (attributes[19]);
                        ac["KP"] = (attributes[20]);
                        ac["KA"] = (attributes[21]);
                        ac["KR"] = (attributes[22]);
                        ac["Inj_Chance"] = (attributes[23]);









                        RosterView.Rows.Add(ac);
                    }





                }
            }

        private void refetchTunetxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Refetch last saved Tune.txt?\n\nBy selecting 'Yes' you will loose any unsaved changes...", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

             if (dr == DialogResult.No)
             {
                 return;
             }
             else if (dr == DialogResult.Yes)
             {
                 Cursor.Current = Cursors.WaitCursor; 
                 LoadActivities();
                 Cursor.Current = Cursors.Arrow; 
             }

        }

        private void simAllCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            Save();
            Cursor.Current = Cursors.Arrow;   

        }

        private void Save()
        {
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp");
            }
            StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\Tune.txt");

            int RowCount = TuneModifierGrd.Rows.Count;

            int i = 0;

            while (i < RowCount)
            {
                    string type = (string)TuneModifierGrd.Rows[i].Cells["Type"].Value;
                    string activity = (string)TuneModifierGrd.Rows[i].Cells["Activity"].Value;
                    string desc = (string)TuneModifierGrd.Rows[i].Cells["Description"].Value;
                    string wgt = (string)TuneModifierGrd.Rows[i].Cells["Wgt"].Value;
                    string spd = (string)TuneModifierGrd.Rows[i].Cells["Spd"].Value;
                    string acc = (string)TuneModifierGrd.Rows[i].Cells["Acc"].Value;
                    string agi = (string)TuneModifierGrd.Rows[i].Cells["Agi"].Value;
                    string str = (string)TuneModifierGrd.Rows[i].Cells["Str"].Value;
                    string stm = (string)TuneModifierGrd.Rows[i].Cells["Stm"].Value;
                    string inj = (string)TuneModifierGrd.Rows[i].Cells["Inj"].Value;
                    string tgh = (string)TuneModifierGrd.Rows[i].Cells["Tgh"].Value;
                    string mor = (string)TuneModifierGrd.Rows[i].Cells["Mor"].Value;
                    string awr = (string)TuneModifierGrd.Rows[i].Cells["Awr"].Value;
                    string cat = (string)TuneModifierGrd.Rows[i].Cells["Cat"].Value;
                    string car = (string)TuneModifierGrd.Rows[i].Cells["Car"].Value;
                    string jmp = (string)TuneModifierGrd.Rows[i].Cells["Jmp"].Value;
                    string btk = (string)TuneModifierGrd.Rows[i].Cells["Btk"].Value;
                    string tkl = (string)TuneModifierGrd.Rows[i].Cells["Tkl"].Value;
                    string thp = (string)TuneModifierGrd.Rows[i].Cells["ThP"].Value;
                    string tha = (string)TuneModifierGrd.Rows[i].Cells["ThA"].Value;
                    string pbk = (string)TuneModifierGrd.Rows[i].Cells["Pbk"].Value;
                    string rbk = (string)TuneModifierGrd.Rows[i].Cells["Rbk"].Value;
                    string kp = (string)TuneModifierGrd.Rows[i].Cells["KP"].Value;
                    string ka = (string)TuneModifierGrd.Rows[i].Cells["KA"].Value;
                    string kr = (string)TuneModifierGrd.Rows[i].Cells["KR"].Value;
                    string injchance = (string)TuneModifierGrd.Rows[i].Cells["Inj_Chance"].Value;


                    string[] typeSplit = type.Split('-');
                    string typeDetail = typeSplit[0];
                    string ConvertedType = "";

                    if (typeDetail == "Positional")
                    {
                        ConvertedType = typeSplit[1] + "-P";
                    }
                    else if (typeDetail == "Weight Training")
                    {
                        ConvertedType = "W";
                    }
                    else if (typeDetail == "Aerobic/Cardio")
                    {
                        ConvertedType = "A";
                    }
                    else if (typeDetail == "Diet")
                    {
                        ConvertedType = "D";
                    }


                sw.WriteLine(";" + ConvertedType + ";" + activity + ";" + desc + ";|" + wgt + "|" + spd + "|" + acc + "|" + agi + "|" + str + "|" + stm + "|" + inj + "|" + tgh + "|" + mor +
                    "|" + awr + "|" + cat + "|" + car + "|" + jmp + "|" + btk + "|" + tkl + "|" + thp + "|" + tha + "|" + pbk + "|" + rbk + "|" + kp + "|" + ka + "|" + kr + "|" + injchance);


                i++;
            }

            sw.Close();




        }

        private void OffSeasonHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use this form to modify the effects that Training Camp activites have on your players.\n" +
            "Remember, camp is processed on a daily basis. Keep this in mind when making allocations.\n" +
            "The 'Inj_chance' column denotes the drills 'Danger' effect. The higher the value the more dangerous\n" +
            "the activity. It can be a negative value, which would reduce the players chances of injury.\n" +
            "The same applies for all attributes; negative numbers mean it's lowering the attribute in question.\n" +
            "Weight also functions in this same fashion.\n" +
            "As with the 'OffSeason Conditioning Utility', a players change in weight throughout camp dynamically\n" + 
            "affects his in-game appearance. A guy gains muscle; he will appear more muscular in-game.\n" +
            "A guy gets fat during OffSeason Conditioning, and doesn't loose the weight during training camp,\n" +
            "he'll look fatter in-game...\n\nEnjoy!");

        }




/*
                    else
                    {
                        if (Pos == "LT" | Pos == "LG" | Pos == "C" | Pos == "RG" | Pos == "RT" | Pos == "OL" | Pos == "OT" | Pos == "OG")
                        {
                            if (splitLine[1] == "OL-P")
                            {
                                ac["Activity"] = splitLine[3];
                                ac["Description"] = splitLine[5];
                                RosterView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "RE" | Pos == "LE" | Pos == "DT" | Pos == "DL" | Pos == "DE")
                        {
                            if (splitLine[1] == "DL-P")
                            {
                                ac["Activity"] = splitLine[3];
                                ac["Description"] = splitLine[5];
                                RosterView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "LOLB" | Pos == "MLB" | Pos == "ROLB" | Pos == "LB" | Pos == "OLB")
                        {
                            if (splitLine[1] == "LB-P")
                            {
                                ac["Activity"] = splitLine[3];
                                ac["Description"] = splitLine[5];
                                RosterView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "CB" | Pos == "FS" | Pos == "SS" | Pos == "DB" | Pos == "S")
                        {
                            if (splitLine[1] == "DB-P")
                            {
                                ac["Activity"] = splitLine[3];
                                ac["Description"] = splitLine[5];
                                RosterView.Rows.Add(ac);
                            }
                        }
                        else if (Pos == "K" | Pos == "P")
                        {
                            if (splitLine[1] == "KP-P")
                            {
                                ac["Activity"] = splitLine[3];
                                ac["Description"] = splitLine[5];
                                RosterView.Rows.Add(ac);
                            }
                        }

                    }
                }//end while
            }
            sr.Close(); 


        }



/*
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
           // isInitialising = false;

            //     RefillRosterView();

        }
/*
        private void SetTimingCombo()
        {
            TrainingTime.ValueType = System.Type.GetType("System.Int16");
            TrainingTime.Items.Add("-10");
            TrainingTime.Items.Add("-9");
            TrainingTime.Items.Add("-8");
            TrainingTime.Items.Add("-7");
            TrainingTime.Items.Add("-6");
            TrainingTime.Items.Add("-5");
            TrainingTime.Items.Add("-4");
            TrainingTime.Items.Add("-3");
            TrainingTime.Items.Add("-2");
            TrainingTime.Items.Add("-1");
            TrainingTime.Items.Add("0");
            TrainingTime.Items.Add("1");
            TrainingTime.Items.Add("2");
            TrainingTime.Items.Add("3");
            TrainingTime.Items.Add("4");
            TrainingTime.Items.Add("5");
            TrainingTime.Items.Add("6");
            TrainingTime.Items.Add("7");
            TrainingTime.Items.Add("8");
            TrainingTime.Items.Add("9");
            TrainingTime.Items.Add("10");


            //  for (int i = 0; i < SetTimeGrd.Rows.Count; i++)
            //  {

            //  }
            //  SetTimeGrd.Columns.Add(TrainingTime);
        }

*/










    }
}