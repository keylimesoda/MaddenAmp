/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 StingRay68
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Db;
using Microsoft.Office.Interop.Excel;

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
        public MaddenFileVersion csvVersion;
        public Dictionary<int, string> import_fields_avail = new Dictionary<int, string>();
        public List<string> import_fields = new List<string>();
        public string currenttablename = "";
        public int linenumber = 0;
        public List<string> errors = new List<string>();        
        public int currentrec;

        List<List<string>> CSVRecords = new List<List<string>>();
        public FB FB_Draft = new FB();
       
        public ExportForm(EditorModel model)
        {
            this.model = model;
            FB_Draft = new FB();
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
        #endregion

        public void InitialiseUI()
        {
            isInitializing = true;
            InitTables();

            if (model.MadVersion >= MaddenFileVersion.Ver2019)
            {
                ExportFilter_Panel.Visible = true;
                filterDraftClassCheckbox.Enabled = false;
                MainSkillsOnly_Checkbox.Enabled = false;
                ExportVersion.SelectedIndex = 0;
                if (model.MadVersion == MaddenFileVersion.Ver2020)
                    ExportVersion.SelectedIndex = 1;
            }
            
            else ExportFilter_Panel.Visible = true;

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

            if (model.FileType != MaddenFileType.Streameddata && model.FileType != MaddenFileType.DataRam && model.FileType != MaddenFileType.StaticData)
            {
                foreach (TeamRecord team in model.TeamModel.GetTeams())
                {
                    filterTeamCombo.Items.Add(team);
                }

                for (int p = 0; p < 21; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions), p);
                    filterPositionCombo.Items.Add(pos);
                }                

                filterPositionCombo.Text = filterPositionCombo.Items[0].ToString();
                filterTeamCombo.Text = filterTeamCombo.Items[0].ToString();
            }
            isInitializing = false;

            ProcessRecords_Button.Enabled = false;
        }

        public void CleanUI()
        {
            AvailTables_ListView.Items.Clear();
            filterTeamCombo.Items.Clear();
            filterPositionCombo.Items.Clear();
        }

        #endregion

        public void SetSerial(FB roster)
        {
            this.FB_Draft.Serial = roster.Serial.ToArray();
        }
        
        public string ConvertBE(string name)
        {
            char[] charArray = name.ToCharArray();
            Array.Reverse(charArray);
            string rev = new string(charArray);
            return rev;
        }

        #region Inits
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
            tables_avail.Sort();
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

        public void InitProcessButton()
        {
            if (UpdateRecs_Checkbox.Checked == false && DeleteCurrentRecs_Checkbox.Checked == false)
                ProcessRecords_Button.Enabled = false;
            else ProcessRecords_Button.Enabled = true;
        }
        
        #endregion


        private string GetDirectory()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Choose DIR for Table Extraction";
            if (folderDialog.ShowDialog() == DialogResult.OK)
                return folderDialog.SelectedPath;
            return "";
        }

        private void ExportPlay_Button_Click(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (tables_export.Count == 0)
                return;
            string dir = "";
            if (ExtractByTableName.Checked)
                dir = GetDirectory();

            foreach (string t in tables_export)
            {
                try
                {
                    string tablename = t;
                    if (model.BigEndian)
                        tablename = ConvertBE(t);

                    string filename = "";
                    if (ExtractByTableName.Checked)
                        filename = Path.Combine(dir, t + ".csv");
                    else
                    {
                        SaveFileDialog fileDialog = new SaveFileDialog();
                        fileDialog.Filter = t +" as csv file (*.csv)|*.csv|All files (*.*)|*.*";
                        fileDialog.RestoreDirectory = true;

                        if (fileDialog.ShowDialog() == DialogResult.OK)
                            filename = fileDialog.FileName;
                    }

                    if (filename == "")
                        continue;

                    StreamWriter writer = new StreamWriter(filename);
                    StringBuilder hbuilder = new StringBuilder();

                    hbuilder.Append(t);
                    hbuilder.Append(",");
                    string version = "2008";
                    if (model.MadVersion == MaddenFileVersion.Ver2004)
                        version = "2004";
                    else if (model.MadVersion == MaddenFileVersion.Ver2005)
                        version = "2005";
                    else if (model.MadVersion == MaddenFileVersion.Ver2006)
                        version = "2006";
                    else if (model.MadVersion == MaddenFileVersion.Ver2007)
                        version = "2007";
                    else if (model.MadVersion == MaddenFileVersion.Ver2008)
                        version = "2008";
                    else if (model.MadVersion == MaddenFileVersion.Ver2019)
                        version = "2019";
                    hbuilder.Append(version);
                    hbuilder.Append(",");
                    hbuilder.Append("No");
                    hbuilder.Append(",");
                    writer.WriteLine(hbuilder.ToString());
                    //writer.Flush();
                    hbuilder.Clear();

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

                    if (Descriptions_Checkbox.Checked)
                    {
                        if (model.TableDefs.ContainsKey(model.MadVersion))
                        {
                            Dictionary<string, tabledefs> currenttable = model.TableDefs[model.MadVersion];
                            if (currenttable.ContainsKey(table.Name))
                            {
                                tabledefs defs = currenttable[table.Name];
                                StringBuilder descbuilder = new StringBuilder();

                                for (int c = 0; c < fields_export[t].Count; c++)
                                {
                                    descbuilder.Append(defs.FieldDefs[fields_export[t][c]]);
                                    descbuilder.Append(",");
                                }

                                writer.WriteLine(descbuilder.ToString());
                            }
                        }
                    }

                    // Setting Filters
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


                    foreach (TableRecordModel rec in table.GetRecords())
                    {
                        if (rec == null)
                            continue;
                        else if (rec.Deleted)
                            continue;
                        else if (table.Name == "PLAY")
                        {
                            if (teamID != -1)
                            {
                                if (teamID != rec.GetIntField("TGID"))
                                    continue;
                            }
                            else if (positionID != -1)
                            {
                                if (positionID != rec.GetIntField("PPOS"))
                                    continue;
                            }

                        }

                        StringBuilder builder = new StringBuilder();

                        foreach (string field in fields_export[t])
                        {
                            foreach (TdbFieldProperties tdb in props)
                            {
                                if (field == tdb.Name)
                                {
                                    if (tdb.FieldType == TdbFieldType.tdbString )
                                    {
                                        string res = rec.GetStringField(tdb.Name);
                                        res = res.Replace(",", " ");
                                        builder.Append(res);
                                    }
                                    else if (tdb.FieldType == TdbFieldType.tdbVarChar)
                                    {
                                        string res = "N/A";                                        
                                        builder.Append(res);
                                    }
                                    else if (tdb.FieldType == TdbFieldType.tdbFloat)
                                    {
                                        
                                        builder.Append(rec.GetFloatField(tdb.Name).ToString("G", culture));
                                    }
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

                    }
                    writer.Flush();
                    writer.Close();
                }

                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
         
        private void ImportCSV_Button_Click(object sender, EventArgs e)
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

                        #region Table/Version info
                        string csvtableinfo = sr.ReadLine();
                        string[] csvtable = csvtableinfo.Split(',');
                        string tablename = csvtable[0];
                        currenttablename = csvtable[0];

                        int ver = Convert.ToInt32(csvtable[1]);
                        bool hasdesc = false;
                        if (csvtable[2].ToUpper().Contains("Y"))
                            hasdesc = true;
                        if (ver == 2004)
                            csvVersion = MaddenFileVersion.Ver2004;
                        else if (ver == 2005)
                            csvVersion = MaddenFileVersion.Ver2005;
                        else if (ver == 2006)
                            csvVersion = MaddenFileVersion.Ver2006;
                        else if (ver == 2007)
                            csvVersion = MaddenFileVersion.Ver2007;
                        else if (ver == 2019)
                            csvVersion = MaddenFileVersion.Ver2019;
                        else csvVersion = MaddenFileVersion.Ver2008;
                        #endregion

                        #region Fields
                        import_fields_avail.Clear();
                        string fieldline = sr.ReadLine();
                        string[] csvfield = fieldline.Split(',');
                        for (int c = 0; c < csvfield.Length; c++)
                        {
                            if (csvfield[c] != "")
                                import_fields_avail.Add(c, csvfield[c]);
                        }
                        #endregion

                        #region Descriptions
                        // We dont need the descripitons for importing, it's just a reference for the roster maker
                        // so just read the line and dont do anything with it.
                        linenumber = 2;
                        if (hasdesc)
                        {
                            sr.ReadLine();
                            linenumber = 3;
                        }
                        #endregion

                        #region Records
                        CSVRecords.Clear();

                        while (!sr.EndOfStream)
                        {
                            List<string> rec_line = new List<string>();                            
                            string csvrecline = sr.ReadLine();
                            if (csvrecline == "")
                                continue;
                            else
                            {
                                string[] csvrec = csvrecline.Split(',');
                                foreach (string s in csvrec)
                                    rec_line.Add(s);
                                CSVRecords.Add(rec_line);
                            }
                        }

                        sr.Close();
                        //Done                        
                        #endregion

                        ImportTableName_Textbox.Text = tablename;
                        ColumnHeader header = new ColumnHeader();
                        header.Text = "Tables";
                        header.Name = "Tables";
                        WrongFields_ListView.Items.Clear();                        
                        WrongFields_ListView.Columns.Add(header);
                        WrongFields_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        WrongFields_ListView.Sorting = SortOrder.Ascending;

                        ColumnHeader header2 = new ColumnHeader();
                        header2.Text = "Tables";
                        header2.Name = "Tables";
                        ImportAvailFields_ListView.Items.Clear();                        
                        ImportAvailFields_ListView.Columns.Add(header2);
                        ImportAvailFields_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        ImportAvailFields_ListView.Sorting = SortOrder.Ascending;

                        ColumnHeader header3 = new ColumnHeader();
                        header3.Text = "Tables";
                        header3.Name = "Tables";
                        ImportSelected_ListView.Items.Clear();
                        ImportSelected_ListView.Columns.Add(header3);
                        ImportSelected_ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        ImportSelected_ListView.Sorting = SortOrder.Ascending;

                        List<string> possible = new List<string>();
                        List<TdbFieldProperties> fplist = model.TableModels[tablename].GetFieldList();
                        foreach (TdbFieldProperties fp in fplist)
                            possible.Add(fp.Name);

                        for (int c = import_fields_avail.Count - 1; c >= 0; c--)
                        {
                            string fieldname = import_fields_avail[c];
                            if (!possible.Contains(fieldname))
                            {
                                WrongFields_ListView.Items.Add(fieldname);
                                import_fields_avail.Remove(c);
                            }
                            else ImportAvailFields_ListView.Items.Add(import_fields_avail[c]);
                        }                       

                        import_fields.Clear();
                        foreach (KeyValuePair<int, string> kvp in import_fields_avail)
                            import_fields.Add(kvp.Value);
                        //import_fields.Sort();

                        foreach (string fld in import_fields)
                            ImportSelected_ListView.Items.Add(fld);                        

                        ImportFieldsCount_Textbox.Text = import_fields_avail.Count.ToString();
                        NotImportableCount_Textbox.Text = WrongFields_ListView.Items.Count.ToString();
                    }                    
                }
                catch (IOException err)
                {
                    err = err;
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (myStream != null)
                myStream.Close();            
        }

        private void ProcessRecords_Button_Click(object sender, EventArgs e)
        {
            currentrec = -1;
           
            foreach (List<string> record in CSVRecords)
            {
                TableRecordModel tablerecord = null;
                linenumber++;
                currentrec++;
                bool fail = false;
                

                if (currenttablename == "PLAY" && UpdateRecs_Checkbox.Checked)
                {
                    // Updating existing record if possible
                    int pgidkey = -1;
                    foreach (KeyValuePair<int, string> kvp in import_fields_avail)
                    {
                        if (kvp.Value == "PGID")
                        {
                            pgidkey = Convert.ToInt32(record[kvp.Key]);
                            break;
                        }
                    }
                    if (pgidkey != -1)
                    {
                        foreach (TableRecordModel trm in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                        {
                            if (trm.Deleted)
                                continue;
                            PlayerRecord player = (PlayerRecord)trm;
                            if (player.PlayerId == pgidkey)
                            {
                                tablerecord = trm;
                                break;
                            }
                        }
                    }                    
                }

                // Not updating PLAY table, so start replacing                    
                if (tablerecord == null)
                {
                    if (currentrec > model.TableModels[currenttablename].capacity - 1)
                        fail = true;
                    else if (currentrec > model.TableModels[currenttablename].RecordCount-1)
                        tablerecord = model.TableModels[currenttablename].CreateNewRecord(true);
                    else tablerecord = model.TableModels[currenttablename].GetRecord(currentrec);                    
                }

                List<TdbFieldProperties> fplist = model.TableModels[currenttablename].GetFieldList();

                if (fail)
                    errors.Add("Line number " + linenumber.ToString() + " Exceeded Capacity");
                
                foreach (KeyValuePair<int, string> import in import_fields_avail)
                {
                    foreach (TdbFieldProperties fp in fplist)
                    {
                        if (fp.Name == import.Value)
                        {
                            try
                            {                                
                                tablerecord.SetFieldCSV(fp.Name, record[import.Key]);
                            }
                            catch
                            {
                                errors.Add("Line number " + linenumber.ToString() + " Field " + fp.Name + " " + import.Value);
                            }
                        }
                    }
                }
            }

            // delete unwanted
            if (DeleteCurrentRecs_Checkbox.Checked)
            {
                for (int c = currentrec; c < model.TableModels[currenttablename].RecordCount - 1; c++)
                {
                    TableRecordModel rec = model.TableModels[currenttablename].GetRecord(currentrec);
                    if (rec.Deleted)
                        continue;
                    rec.SetDeleteFlag(true);
                }
            }

            ColumnHeader head = new ColumnHeader();
            head.Text = "Errors";
            head.Name = "Errors";
            ImportErrors_Listview.Columns.Add(head);
            ImportErrors_Listview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ImportErrors_Listview.Items.Clear();
            if (errors.Count > 0)
            {
                foreach (string error in errors)
                    ImportErrors_Listview.Items.Add(new ListViewItem(error));
            }
            else ImportErrors_Listview.Items.Add(new ListViewItem(linenumber.ToString() + " Lines processed.  No errors"));           
        }

        private void DeleteCurrentRecs_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (DeleteCurrentRecs_Checkbox.Checked)
                {
                    UpdateRecs_Checkbox.Checked = false;
                    UpdateRecs_Checkbox.Enabled = false;
                }
                else
                {
                    UpdateRecs_Checkbox.Enabled = true;                   
                }
            }

            InitProcessButton();
        }

        private void UpdateRecs_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                if (UpdateRecs_Checkbox.Checked)
                {
                    DeleteCurrentRecs_Checkbox.Checked = false;
                    DeleteCurrentRecs_Checkbox.Enabled = false;
                }
                else
                {
                    DeleteCurrentRecs_Checkbox.Enabled = true;
                }
            }

            InitProcessButton();
        }

        private void LoadDraftClass_Button_Click(object sender, EventArgs e)
        {            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "Madden Draft Class (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.ShowDialog();

            string filename = dialog.FileName;
            if (filename == "")
                return;

            try
            {
                if (!model.DraftClassModel.ReadDraftClass(filename))
                {
                    MessageBox.Show("Not a valid Madden Draft Class", "Not a Draft Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {

            }
            
        }

        private void ExportDraftClass_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            Stream myStream = null;

            fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if ((myStream = fileDialog.OpenFile()) != null)
                    {
                        StreamWriter wText = new StreamWriter(myStream);

                        model.DraftClassModel.ExportCSVHeaders(wText, DraftClassDescriptions_Checkbox.Checked);

                        foreach (DraftPlayer player in model.DraftClassModel.draftclassplayers)
                        {
                            StringBuilder build = player.ExportDraftClassPlayerCSV(model.DraftClassModel.RatingDefs, model, DraftClassDescriptions_Checkbox.Checked);
                            wText.WriteLine(build.ToString());
                        }

                        wText.Close();
                    }
                }
                catch (IOException err)
                {
                    MessageBox.Show("Error opening file\r\n\r\n Check that the file is not already opened", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void CreateDraftClass_Button_Click(object sender, EventArgs e)
        {                       
            OpenFileDialog dialog = new OpenFileDialog();
            Stream myStream = null;            
            dialog.Filter = "Draft Class CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {  
                StreamReader sr = null;
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    if ((myStream = dialog.OpenFile()) != null)
                    {
                        sr = new StreamReader(myStream);
                        string csvtableinfo = sr.ReadLine();
                        string[] csvtable = csvtableinfo.Split(',');
                        if (csvtable[0] != "DRAFT")
                        {
                            MessageBox.Show("Not a valid Madden Draft Class", "Not a Draft Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sr.Close();
                            return;
                        }
                        string csvfieldline = sr.ReadLine();
                        string[] csvfields = csvfieldline.Split(',');

                        if (csvtable[2].ToUpper().Contains("Y"))
                        {
                            sr.ReadLine();
                        }

                        List<string> records = new List<string>();
                        while (!sr.EndOfStream)
                        {
                            string csvrecordline = sr.ReadLine();
                            records.Add(csvrecordline);
                        }

                        model.DraftClassModel.ImportCSVDraftClass(records, csvfields);
                    }
                }
                catch
                {

                }

                if (sr != null)
                    sr.Close();

                this.Cursor = Cursors.Default;
            }

            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.RestoreDirectory = true;
            savedialog.Filter = "Madden Draft Class (*.*)|*.*";
            savedialog.ShowDialog();

            string filename = savedialog.FileName;
            if (filename == "")
                return;

            model.DraftClassModel.SaveDraftClass(filename, FB_Draft, ExportVersion.SelectedIndex);
        }

        private void CreateCommentID_Click(object sender, EventArgs e)
        {
            Dictionary<string,int> CommentIDs = new Dictionary<string,int>();

            OpenFileDialog dialog = new OpenFileDialog();
            Stream myStream = null;            
            dialog.Filter = "All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = null;
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    if ((myStream = dialog.OpenFile()) != null)
                    {
                        sr = new StreamReader(myStream);
                        
                        while (!sr.EndOfStream)
                        {
                            bool stop = false;
                            string line = sr.ReadLine();
                            if (line.Contains("SurnameToAudioStore"))
                            {
                                while (!stop)
                                {
                                    string line0 = sr.ReadLine();
                                    if (!line0.Contains("SurnameToAudioID"))
                                        stop = true;

                                    int id = 0;
                                    string name = "";
                                    
                                    string line1 = sr.ReadLine();
                                    if (line1.Contains("value="))
                                    { 
                                        line1 = line1.Replace("\"", "");
                                        string[] ids = line1.Split(' ');
                                        foreach (string s in ids)
                                        {
                                            if (s.Contains("value="))
                                            {
                                                id = Convert.ToInt32(s.Replace("value=", ""));
                                                break;
                                            }
                                        }
                                    }

                                    string line2 = sr.ReadLine();
                                    if (line2.Contains("Surname"))
                                    {
                                        line2 = line2.Replace("\"", "");
                                        string[] ids = line2.Split(' ');
                                        foreach (string s in ids)
                                        {
                                            if (s.Contains("value="))
                                            {
                                                name = s.Replace("value=", "");
                                                break;
                                            }
                                        }
                                    }

                                    string line3 = sr.ReadLine(); // </record>

                                    if (name!="" && !CommentIDs.ContainsKey(name))
                                        CommentIDs.Add(name,id);
                                }
                            }

                        }

                        
                    }

                }
                catch
                {

                }

                foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                {
                    if (rec.LastName != "" && rec.LastName != "Player" && !CommentIDs.ContainsKey(rec.LastName))
                        CommentIDs.Add(rec.LastName,rec.PlayerComment);
                }


                SaveFileDialog savedialog = new SaveFileDialog();
                string direct = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                direct += "\\Madden NFL 19\\settings";
                if (Directory.Exists(direct))
                    dialog.InitialDirectory = direct;
                else savedialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);                
                savedialog.ShowDialog();

                string filename = savedialog.FileName;
                if (filename == "")
                    return;

                StreamWriter wText = new StreamWriter(filename);

                foreach (KeyValuePair<string, int> id in CommentIDs)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(id.Key);
                    builder.Append(",");
                    builder.Append(id.Value.ToString());

                    wText.WriteLine(builder.ToString());                   
                }

                wText.Flush();
                wText.Close();

            }
        }

        private void loadDraftgenXlsx_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This operation will load an XLSX file, run the calculations, and then pull the data from a tab named \"Output\", which should match the Draft Import Format.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.RestoreDirectory = true;
                dialog.Title = "Madden 20 Draft Generator XLSX";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                dialog.Filter = "XLSX Files (*.xlsx)|*.xlsx";
                dialog.FilterIndex = 1;
                dialog.Multiselect = false;
                dialog.ShowDialog();

                if (dialog.FileNames.Length > 0)
                {
                    DialogResult res3 = MessageBox.Show("This will take some time and your application may appear to freeze. On an older/slower computer this could take up to five minutes. Are you sure you want to continue?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (res3 == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    string filename = dialog.FileNames[0];
                    var excel = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = excel.Workbooks.Open(filename);
                    var foundSheet = false;
                    var s = wb.Sheets;
                    foreach (Worksheet sheet in wb.Sheets)
                    {
                        if (sheet.Name.Equals("Output"))
                        {
                            foundSheet = true;
                            // excel.Calculate();
                            var preparingDraftClassForm = new PreparingDraftClassForm(excel);
                            preparingDraftClassForm.ShowDialog(this);
                            preparingDraftClassForm.Dispose();

                            sheet.Select(Type.Missing);
                            var saveFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
                            wb.SaveAs(saveFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange);
                            wb.Close(false);

                            using (StreamReader sr = new StreamReader(saveFile))
                            {

                                try
                                {
                                    string csvtableinfo = sr.ReadLine();
                                    string[] csvtable = csvtableinfo.Split(',');
                                    if (csvtable[0] != "DRAFT")
                                    {
                                        MessageBox.Show("Not a valid Madden Draft Class", "Not a Draft Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    string csvfieldline = sr.ReadLine();
                                    string[] csvfields = csvfieldline.Split(',');

                                    if (csvtable[2].ToUpper().Contains("Y"))
                                    {
                                        sr.ReadLine();
                                    }

                                    List<string> records = new List<string>();
                                    while (!sr.EndOfStream)
                                    {
                                        string csvrecordline = sr.ReadLine();
                                        records.Add(csvrecordline);
                                    }

                                    model.DraftClassModel.ImportCSVDraftClass(records, csvfields);
                                }
                                catch (Exception err)
                                {
                                    MessageBox.Show("ERR: " + err.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            this.Cursor = Cursors.Default;


                            SaveFileDialog savedialog = new SaveFileDialog();
                            savedialog.RestoreDirectory = true;
                            savedialog.Filter = "Madden Draft Class (*.*)|*.*";
                            savedialog.ShowDialog();

                            string saveFilename = savedialog.FileName;
                            if (saveFilename == "")
                                return;

                            model.DraftClassModel.SaveDraftClass(saveFilename, FB_Draft, ExportVersion.SelectedIndex);
                            var draftResult = model.DraftClassModel.OutputDraftClassStats();

                            var draftResultForm = new DraftReportForm(draftResult);
                            draftResultForm.ShowDialog(this);
                            draftResultForm.Dispose();
                        }
                    }
                    this.Cursor = Cursors.Default;
                    if (foundSheet == false)
                    {
                        DialogResult res2 = MessageBox.Show("No sheet named \"Output\" found in " + filename, "Bad Worksheet Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
        
            
        
         
        
