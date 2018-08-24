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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Db;

namespace MaddenEditor.Forms
{
    public partial class ExportForm : Form, IEditorForm
    {
        private bool isInitializing = false;
        private EditorModel model = null;
        public List<string> tables_avail = new List<string>();
        public List<string> tables_export = new List<string>();
        public Dictionary<string, List<string>> fields_avail = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> fields_export = new Dictionary<string, List<string>>();
        
        public ExportForm(EditorModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        #region IEditorForm Members

        public EditorModel Model
        {            
            set { }
        }

        # region College List for Export
        // Would like to use the list in PlayerEditControl but how to do it?
        public string[] collegenames = 
            {
            "Abilene Chr.",
            "Air Force",
            "Akron",
            "Alabama",
            "Alabama A&M",
            "Alabama St.",
            "Alcorn St.",
            "Appalach. St.",
            "Arizona    ",
            "Arizona St.",
            "Arkansas",
            "Arkansas P.B.",
            "Arkansas St.",
            "Army",
            "Auburn",
            "Austin Peay",
            "Ball State",
            "Baylor",
            "Beth Cookman",
            "Boise State",
            "Boston Coll.",
            "Bowl. Green",
            "Brown",
            "Bucknell",
            "Buffalo",
            "Butler",
            "BYU",
            "Cal Poly SLO",
            "California",
            "Cal-Nrthridge",
            "Cal-Sacrmnto",
            "Canisius",
            "Cent Conn St.    ",
            "Central MI   ",
            "Central St Ohio    ",
            "Charleston S.    ",
            "Cincinnati   ",
            "Citadel    ",
            "Clemson   ",
            "Clinch Valley    ",
            "Colgate    ",
            "Colorado   ",
            "Colorado St.   ",
            "Columbia  ",
            "Cornell  ",
            "Culver-Stockton    ",
            "Dartmouth  ",
            "Davidson    ",
            "Dayton    ",
            "Delaware    ",
            "Delaware St.  ",
            "Drake    ",
            "Duke   ",
            "Duquesne    ",
            "E. Carolina   ",
            "E. Illinois    ",
            "E. Kentucky    ",
            "E. Tenn. St.   ",
            "East. Mich.  ",
            "Eastern Wash.   ",
            "Elon College ",
            "Fairfield  ",
            "Florida    ",
            "Florida A&M",
            "Florida State",
            "Fordham  ",
            "Fresno State   ",
            "Furman     ",
            "Ga. Southern ",
            "Georgetown  ",
            "Georgia     ",
            "Georgia Tech    ",
            "Grambling St. ",
            "Grand Valley St.    ",
            "Hampton    ",
            "Harvard     ",
            "Hawaii       ",
            "Henderson St.    ",
            "Hofstra          ",
            "Holy Cross       ",
            "Houston       ",
            "Howard        ",
            "Idaho           ",
            "Idaho State   ",
            "Illinois     ",
            "Illinois St.    ",
            "Indiana       ",
            "Indiana St.    ",
            "Iona            ",
            "Iowa           ",
            "Iowa State    ",
            "J. Madison     ",
            "Jackson St.    ",
            "Jacksonv. St.    ",
            "John Carroll    ",
            "Kansas        ",
            "Kansas State   ",
            "Kent State      ",
            "Kentucky      ",
            "Kutztown        ",
            "La Salle        ",
            "LA. Tech        ",
            "Lambuth           ",
            "Lehigh           ",
            "Liberty         ",
            "Louisville      ",
            "LSU              ",
            "M. Valley St.  ",
            "Maine           ",
            "Marist           ",
            "Marshall         ",
            "Maryland         ",
            "Massachusetts    ",
            "McNeese St.     ",
            "Memphis         ",
            "Miami           ",
            "Miami Univ.      ",
            "Michigan         ",
            "Michigan St.     ",
            "Mid Tenn St.     ",
            "Minnesota        ",
            "Miss. State      ",
            "Missouri         ",
            "Monmouth         ",
            "Montana           ",
            "Montana State   ",
            "Morehead St.   ",
            "Morehouse      ",
            "Morgan St.    ",
            "Morris Brown       ",
            "Mt S. Antonio      ",
            "Murray State       ",
            "N. Alabama        ",
            "N. Arizona       ",
            "N. Car A&T       ",
            "N. Carolina       ",
            "N. Colorado       ",
            "N. Illinois       ",
            "N.C. State      ",
            "Navy             ",
            "NC Central        ",
            "Nebr.-Omaha      ",
            "Nebraska        ",
            "Nevada          ",
            "New Mex. St.      ",
            "New Mexico        ",
            "Nicholls St.     ",
            "Norfolk State    ",
            "North Texas     ",
            "Northeastern      ",
            "Northern Iowa      ",
            "Northwestern    ",
            "Notre Dame        ",
            "NW Oklahoma St.   ",
            "N\'western St.     ",
            "Ohio              ",
            "Ohio State        ",
            "Oklahoma         ",
            "Oklahoma St.     ",
            "Ole Miss        ",
            "Oregon           ",
            "Oregon State     ",
            "P. View A&M      ",
            "Penn             ",
            "Penn State      ",
            "Pittsburg St.   ",
            "Pittsburgh       ",
            "Portland St.     ",
            "Princeton       ",
            "Purdue           ",
            "Rhode Island     ",
            "Rice             ",
            "Richmond         ",
            "Robert Morris      ",
            "Rowan             ",
            "Rutgers         ",
            "S. Carolina     ",
            "S. Dakota St.      ",
            "S. Illinois       ",
            "S.C. State       ",
            "S.D. State      ",
            "S.F. Austin        ",
            "Sacred Heart      ",
            "Sam Houston        ",
            "Samford            ",
            "San Jose St.      ",
            "Savannah St.       ",
            "SE Missouri        ",
            "SE Missouri St.    ",
            "Shippensburg       ",
            "Siena              ",
            "Simon Fraser      ",
            "SMU              ",
            "Southern        ",
            "Southern Miss     ",
            "Southern Utah    ",
            "St. Francis      ",
            "St. John\'s        ",
            "St. Mary\'s        ",
            "St. Peters        ",
            "Stanford         ",
            "Stony Brook        ",
            "SUNY Albany        ",
            "SW Miss St        ",
            "SW Texas St.      ",
            "Syracuse         ",
            "T A&M K\'ville     ",
            "TCU              ",
            "Temple          ",
            "Tenn. Tech        ",
            "Tenn-Chat         ",
            "Tennessee         ",
            "Tennessee St.      ",
            "Tenn-Martin        ",
            "Texas             ",
            "Texas A&M         ",
            "Texas South.     ",
            "Texas Tech        ",
            "Toledo           ",
            "Towson State       ",
            "Troy State       ",
            "Tulane            ",
            "Tulsa             ",
            "Tuskegee           ",
            "UAB               ",
            "UCF               ",
            "UCLA              ",
            "UConn            ",
            "UL Lafayette      ",
            "UL Monroe         ",
            "UNLV             ",
            "USC            ",
            "USF             ",
            "Utah             ",
            "Utah State      ",
            "UTEP            ",
            "Valdosta St.    ",
            "Valparaiso       ",
            "Vanderbilt       ",
            "Villanova          ",
            "Virginia        ",
            "Virginia Tech    ",
            "VMI                ",
            "W. Carolina      ",
            "W. Illinois      ",
            "W. Kentucky       ",
            "W. Michigan      ",
            "W. Texas A&M    ",
            "Wagner           ",
            "Wake Forest      ",
            "Walla Walla      ",
            "Wash. St.       ",
            "Washington       ",
            "Weber State      ",
            "West Virginia  ",
            "Westminster      ",
            "Will. & Mary    ",
            "Winston Salem    ",
            "Wisconsin      ",
            "Wofford         ",
            "Wyoming        ",
            "Yale            ",
            "Youngstwn St.    ",
            "Sonoma St.       ",
            "No College       ",
            "N/A               ",
            "New Hampshire      ",
            "UW Lacrosse       ",
            "Hastings College    ",
            "Midwestern St.     ",
            "North Dakota       ",
            "Wayne State        ",
            "UW Stevens Pt.   ",
            "Indiana(Penn.)    ",
            "Saginaw Valley    ",
            "Central St.(OK)   ",
            "Emporia State     "
            };
        # endregion

        public void InitialiseUI()
        {
            isInitializing = true;
            InitTables();

            ColumnHeader header = new ColumnHeader();
            header.Text = "Tables";
            header.Name = "Tables";            
            AvailTables_ListView.Columns.Add(header);
            AvailTables_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            InitAvailableTablesList();            
            
            header = new ColumnHeader();
            header.Text = "Export Tables";
            header.Name = "Export Tables";
            ExportTables_ListView.Columns.Add(header);
            ExportTables_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            InitExportTables();

            header = new ColumnHeader();
            header.Text = "Fields";
            header.Name = "Fields";
            AvailFields_ListView.Columns.Add(header);
            AvailFields_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);            

            header = new ColumnHeader();
            header.Text = "Export Fields";
            header.Name = "Export Fields";
            ExportFields_ListView.Columns.Add(header);
            ExportFields_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            foreach (TeamRecord team in model.TeamModel.GetTeams())
            {
                filterTeamCombo.Items.Add(team);
            }

            foreach (string position in Enum.GetNames(typeof(MaddenPositions)))
            {
                filterPositionCombo.Items.Add(position);
            }

            filterPositionCombo.Text = filterPositionCombo.Items[0].ToString();
            filterTeamCombo.Text = filterTeamCombo.Items[0].ToString();
            isInitializing = false;
        }

        public void CleanUI()
        {
            AvailTables_ListView.Items.Clear();
            filterTeamCombo.Items.Clear();
            filterPositionCombo.Items.Clear();
        }

        #endregion

        public string ConvertBE(string name)
        {
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            string rev = new string(charArray);
            return rev;
        }

        public void InitTables()
        {
            AvailTables_ListView.Items.Clear();
            tables_avail.Clear();
            fields_avail.Clear();
            tables_export.Clear();

            foreach (KeyValuePair<string, int> pair in model.TableNames)
            {
                string name = pair.Key;
                if (model.BigEndian)
                    name = ConvertBE(pair.Key);
                tables_avail.Add(name);
                AvailTables_ListView.Items.Add(name);
                
                if (!model.TableModels.ContainsKey(pair.Key))
                {
                    model.ProcessTable(model.TableNames[pair.Key]);
                }

                TableModel table = model.TableModels[name];
                List<TdbFieldProperties> props = table.GetFieldList();
                List<string> fields = new List<string>();
                foreach (TdbFieldProperties p in props)
                    fields.Add(p.Name);
                fields.Sort();
                fields_avail.Add(name, new List<string>(fields));
                fields_export.Add(name, new List<string>(fields));
            }
        }
        
        public void InitAvailableTablesList()
        {
            AvailTables_ListView.Items.Clear();
            foreach (string s in tables_avail)
            {
                AvailTables_ListView.Items.Add(s);
            }
        }

        public void InitExportTables()
        {
            ExportTables_ListView.Items.Clear();
            foreach (string s in tables_export)
            {
                ExportTables_ListView.Items.Add(s);
            }
        }

        public void InitAvailableFields(string name)
        {
            AvailFields_ListView.Items.Clear();
            foreach (string f in fields_avail[name])
            {
                AvailFields_ListView.Items.Add(f);
            }
        }
       
        public void InitExportFields(string name)
        {
            ExportFields_ListView.Items.Clear();
            foreach (string s in fields_export[name])
                ExportFields_ListView.Items.Add(s);
        }


        private void ExportPlay_Button_Click(object sender, EventArgs e)
        {
            if (tables_export.Count == 0)
                return;

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choose DIR for Table Extraction";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string folder = folderDialog.SelectedPath;

                foreach (string t in tables_export)
                {
                    try
                    {
                        string tablename = t;
                        if (model.BigEndian)
                            tablename = ConvertBE(t);

                        StreamWriter writer = new StreamWriter(Path.Combine(folder, (t + ".csv")));
                        StringBuilder hbuilder = new StringBuilder();

                        if (!model.TableModels.ContainsKey(t))
                        {
                            model.ProcessTable(model.TableNames[tablename]);
                        }

                        TableModel table = model.TableModels[t];

                        List<TdbFieldProperties> props = table.GetFieldList();
                        foreach (string field in fields_export[t])
                        {
                            foreach (TdbFieldProperties tdb in props)
                            {
                                if (field == tdb.Name)
                                {
                                    // These are already fixed for big endian
                                    string fieldname = tdb.Name;
                                    hbuilder.Append(fieldname);
                                    hbuilder.Append(",");
                                }
                            }
                        }

                        writer.WriteLine(hbuilder.ToString());
                        writer.Flush();

                        foreach (TableRecordModel rec in table.GetRecords())
                        {
                            if (rec.Deleted)
                                continue;
                            StringBuilder builder = new StringBuilder();

                            foreach (string field in fields_export[t])
                            {
                                foreach (TdbFieldProperties tdb in props)
                                {
                                    if (field == tdb.Name)
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
                                }
                            }

                            writer.WriteLine(builder.ToString());
                            writer.Flush();
                        }

                        writer.Close();
                    }

                    catch (IOException err)
                    {
                        err = err;
                        MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
        }
       
        private void ExportButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int teamID = -1;
            int positionID = -1;

            if (filterTeamCheckbox.Checked)
            {
                //Get the team id for the team selected in the combobox
                teamID = ((TeamRecord)(filterTeamCombo.SelectedItem)).TeamId;
            }

            if (filterPositionCheckbox.Checked)
            {
                //Get the position id for the position selected in the combobox
                positionID = filterPositionCombo.SelectedIndex;
            }

            List<PlayerRecord> playerList = new List<PlayerRecord>();

            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (record.Deleted)
                {
                    continue;
                }

                PlayerRecord playerRecord = (PlayerRecord)record;

                if (teamID != -1 && playerRecord.TeamId != teamID)
                {
                    continue;
                }

                if (positionID != -1 && playerRecord.PositionId != positionID)
                {
                    continue;
                }

                if (filterDraftClassCheckbox.Checked && playerRecord.YearsPro != 0)
                {
                    continue;
                }
                if (playerRecord.FirstName == "New" && playerRecord.LastName == "Player")
                    continue;

                //This player needs to be added to our list for export
                playerList.Add(playerRecord);
            }

            //Bring up a save dialog
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
                        StreamWriter wText = new StreamWriter(myStream);

                        //Output the headers first
                        StringBuilder hbuilder = new StringBuilder();
                        hbuilder.Append("Position,");
                        hbuilder.Append("First Name,");
                        hbuilder.Append("Last Name,");
                        // If Draft Class use College instead
                        if (filterDraftClassCheckbox.Checked)
                            hbuilder.Append("College,");
                        else
                            hbuilder.Append("Team,");


                        hbuilder.Append("Age,");
                        hbuilder.Append("Height,");
                        hbuilder.Append("Weight,");
                        hbuilder.Append("Tendency,");
                        hbuilder.Append("OVR,");

                        if (MainSkillsOnly_Checkbox.Checked)
                        {
                            switch (positionID)
                            {
                                case (int)MaddenPositions.QB:
                                    hbuilder.Append("THA,");
                                    hbuilder.Append("THP,");
                                    hbuilder.Append("AWR,");
                                    hbuilder.Append("SPD,");
                                    hbuilder.Append("AGI,");
                                    hbuilder.Append("BTK,");
                                    break;
                            }
                        }

                        else
                        {
                            hbuilder.Append("Speed,");
                            hbuilder.Append("Strength,");
                            hbuilder.Append("Awareness,");
                            hbuilder.Append("Agility,");
                            hbuilder.Append("Acceleration,");
                            hbuilder.Append("Catching,");
                            hbuilder.Append("Carrying,");
                            hbuilder.Append("Jumping,");
                            hbuilder.Append("Break Tackle,");
                            hbuilder.Append("Tackle,");
                            hbuilder.Append("Throw Power,");
                            hbuilder.Append("Throw Accuracy,");
                            hbuilder.Append("Pass Blocking,");
                            hbuilder.Append("Run Blocking,");
                            hbuilder.Append("Kick Power,");
                            hbuilder.Append("Kick Accuracy,");
                            hbuilder.Append("Kick Return,");
                            hbuilder.Append("Stamina,");
                            hbuilder.Append("Injury,");
                            hbuilder.Append("Toughness,");
                            hbuilder.Append("Importance,");
                            hbuilder.Append("Morale");
                        }

                        wText.WriteLine(hbuilder.ToString());

                        foreach (PlayerRecord rec in playerList)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append(Enum.GetNames(typeof(MaddenPositions))[rec.PositionId].ToString());
                            builder.Append(",");
                            builder.Append(rec.FirstName);
                            builder.Append(",");
                            builder.Append(rec.LastName);
                            builder.Append(",");
                            // If Draft Class use College Name instead
                            if (rec.YearsPro == 0 && filterDraftClassCheckbox.Checked)
                                builder.Append(collegenames[rec.CollegeId]);
                            else
                                builder.Append(model.TeamModel.GetTeamNameFromTeamId(rec.TeamId));
                            builder.Append(",");
                            builder.Append(rec.Age);
                            builder.Append(",");
                            builder.Append((rec.Height / 12) + "' " + (rec.Height % 12) + "\"");
                            builder.Append(",");
                            builder.Append(rec.Weight + 160);
                            builder.Append(",");

                            if (rec.Tendency == 2)
                                builder.Append("BAL");
                            else
                            {
                                switch (positionID)
                                {
                                    case (int)MaddenPositions.QB:
                                        {
                                            if (rec.Tendency == 0)
                                                builder.Append("POC");
                                            else builder.Append("SCR");
                                            break;
                                        }
                                    case (int)MaddenPositions.HB:
                                        {
                                            if (rec.Tendency == 0)
                                                builder.Append("POW");
                                            else builder.Append("SPD");
                                            break;
                                        }
                                    case (int)MaddenPositions.FB:
                                    case (int)MaddenPositions.TE:
                                        {
                                            if (rec.Tendency == 0)
                                                builder.Append("BLK");
                                            else builder.Append("REC");
                                            break;
                                        }
                                    case (int)MaddenPositions.WR:
                                        {
                                            if (rec.Tendency == 0)
                                                builder.Append("POS");
                                            else builder.Append("SPD");
                                            break;
                                        }
                                    case (int)MaddenPositions.LT:
                                    case (int)MaddenPositions.LG:
                                    case (int)MaddenPositions.C:
                                    case (int)MaddenPositions.RG:
                                    case (int)MaddenPositions.RT:
                                        {
                                            if (rec.Tendency == 0)
                                                builder.Append("RUN");
                                            else builder.Append("PAS");
                                            break;
                                        }
                                }
                            }

                            builder.Append(",");

                            builder.Append(rec.Overall);
                            builder.Append(",");

                            if (MainSkillsOnly_Checkbox.Checked)
                            {
                                switch (positionID)
                                {
                                    case (int)MaddenPositions.QB:
                                        builder.Append(rec.ThrowAccuracy);
                                        builder.Append(",");
                                        builder.Append(rec.ThrowPower);
                                        builder.Append(",");
                                        builder.Append(rec.Awareness);
                                        builder.Append(",");
                                        builder.Append(rec.Speed);
                                        builder.Append(",");
                                        builder.Append(rec.Agility);
                                        builder.Append(",");
                                        builder.Append(rec.BreakTackle);
                                        builder.Append(",");
                                        break;
                                }
                            }

                            else
                            {
                                builder.Append(rec.Speed);
                                builder.Append(",");
                                builder.Append(rec.Strength);
                                builder.Append(",");
                                builder.Append(rec.Awareness);
                                builder.Append(",");
                                builder.Append(rec.Agility);
                                builder.Append(",");
                                builder.Append(rec.Acceleration);
                                builder.Append(",");
                                builder.Append(rec.Catching);
                                builder.Append(",");
                                builder.Append(rec.Carrying);
                                builder.Append(",");
                                builder.Append(rec.Jumping);
                                builder.Append(",");
                                builder.Append(rec.BreakTackle);
                                builder.Append(",");
                                builder.Append(rec.Tackle);
                                builder.Append(",");
                                builder.Append(rec.ThrowPower);
                                builder.Append(",");
                                builder.Append(rec.ThrowAccuracy);
                                builder.Append(",");
                                builder.Append(rec.PassBlocking);
                                builder.Append(",");
                                builder.Append(rec.RunBlocking);
                                builder.Append(",");
                                builder.Append(rec.KickPower);
                                builder.Append(",");
                                builder.Append(rec.KickAccuracy);
                                builder.Append(",");
                                builder.Append(rec.KickReturn);
                                builder.Append(",");
                                builder.Append(rec.Stamina);
                                builder.Append(",");
                                builder.Append(rec.Injury);
                                builder.Append(",");
                                builder.Append(rec.Toughness);
                                builder.Append(",");
                                builder.Append(rec.Importance);
                                builder.Append(",");
                                builder.Append(rec.Morale);
                            }

                            wText.WriteLine(builder.ToString());
                            wText.Flush();
                        }

                        myStream.Close();
                    }

                }
                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.Cursor = Cursors.Default;
            DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void Import_Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            Stream myStream = null;
            fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = fileDialog.OpenFile()) != null)
                    {
                        StreamReader sr = new StreamReader(myStream);
                        string header = sr.ReadLine();                  


                        
                    }
                }
                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Cursor = Cursors.Default;
                DialogResult = DialogResult.OK;
                this.Close();
            }


        }

        private void AddExportTables_Button_Click(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                isInitializing = true;

                foreach (ListViewItem i in AvailTables_ListView.SelectedItems)
                {
                    string name = i.Text;                   

                    if (!tables_export.Contains(name))
                        tables_export.Add(name);                    
                }
                InitExportTables();

                isInitializing = false;
            }
        }

        private void RemoveExportTables_Button_Click(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                isInitializing = true;

                foreach (ListViewItem i in ExportTables_ListView.SelectedItems)
                {
                    string name = i.Text;                    
                    if (tables_export.Contains(name))
                        tables_export.Remove(name);
                }

                InitExportTables();

                isInitializing = false;
            }
        }

        private void filterTeamCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void filterPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ExportTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ExportTables_ListView.SelectedItems.Count == 0)
                    return;
            if (!isInitializing)
            {
                isInitializing = true;
                string tablename = ExportTables_ListView.SelectedItems[0].Text;
                InitAvailableFields(tablename);
                InitExportFields(tablename);
                isInitializing = false;
            }
        }

        private void AddFields_Button_Click(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                isInitializing = true;
                string tablename = ExportTables_ListView.SelectedItems[0].Text;
                List<string> newfields = new List<string>();
                foreach (ListViewItem i in AvailFields_ListView.SelectedItems)
                    newfields.Add(i.Text);
                if (fields_export.ContainsKey(tablename))
                {
                    List<string> existing = new List<string>(fields_export[tablename]);
                    foreach (string s in newfields)
                        if (!existing.Contains(s))
                            existing.Add(s);
                    fields_export[tablename] = existing;
                }
                else fields_export.Add(tablename, newfields);

                InitExportFields(tablename);
                isInitializing = false;
            }
        }
        
        private void RemoveFields_Button_Click(object sender, EventArgs e)
        {
            isInitializing = true;
            string tablename = ExportTables_ListView.SelectedItems[0].Text;
            List<string> exportlist = new List<string>();
            List<string> avail = fields_avail[tablename];

            foreach (ListViewItem x in ExportFields_ListView.SelectedItems)
                exportlist.Add(x.Text);
            foreach (string s in exportlist)
                fields_export[tablename].Remove(s);

            InitExportFields(tablename);

            isInitializing = false;
        }

        

    }
}
        
            
        
         
        
