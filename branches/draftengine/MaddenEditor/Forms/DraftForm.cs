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

namespace MaddenEditor.Forms
{
    public partial class DraftForm : Form
    {
        DraftModel dm;
        EditorModel model;
        double secondsPerPick;
        int timeRemaining;
        Random random;

        double pickProb;
        LocalMath math = new LocalMath();

        DataTable draftPickData = new DataTable();
        BindingSource draftPickBinding = new BindingSource();

        DataTable depthChartData = new DataTable();
        BindingSource depthChartBinding = new BindingSource();

        DataTable draftBoardData = new DataTable();
        BindingSource draftBoardBinding = new BindingSource();

        DataTable rookieData = new DataTable();
        BindingSource rookieBinding = new BindingSource();

        DataTable wishlistData = new DataTable();
        BindingSource wishlistBinding = new BindingSource();

        int CurrentPick = 0;
        int CurrentSelectingId;
        int HumanTeamId;
        int SelectedPlayer = 0;
        bool sortDirection = true;

        public DraftForm(EditorModel ParentModel, DraftModel draftmodel, int humanId, int seconds)
        {
            dm = draftmodel;
            HumanTeamId = humanId;
            secondsPerPick = seconds;
            model = ParentModel;
            InitializeComponent();
        }

        private void DraftForm_Load(object sender, EventArgs e)
        {
            this.Invalidate();

            pickProb = 1 - Math.Pow(0.5, 1/secondsPerPick);

            /*
            dm = new DraftModel(model);
            dm.InitializeDraft(HumanTeamId);

            dm.FixDraftOrder();
             * */

            InitializeDataGrids();
            InitializeComboBoxes();

            CurrentSelectingId = model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick]["Team"]);
            random = new Random(unchecked((int)DateTime.Now.Ticks));

            if (CurrentSelectingId == HumanTeamId)
            {
                draftButton.Enabled = true;
            }
            else
            {
                timeRemaining = (int)secondsPerPick;
                clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);
                draftTimer.Start();
            }

            /*
            for (int i = 0; i < 32*7; i++)
            {
                if (draftPickData.Rows[i]["Team"].Equals(model.TeamModel.GetTeamNameFromTeamId(HumanTeamId))) {


                RookieRecord drafted = dm.MakeSelection(i, null);

                draftPickData.Rows[i]["Position"] = Enum.GetNames(typeof(MaddenPositions))[drafted.Player.PositionId].ToString();
                draftPickData.Rows[i]["Player"] = drafted.Player.FirstName + " " + drafted.Player.LastName;

                for (int j = 0; j < rookieData.Rows.Count; j++)
                {
                    if (drafted.PlayerId == (int)rookieData.Rows[j]["PGID"])
                    {
                        rookieData.Rows[j]["Drafted By"] = draftPickData.Rows[i]["Team"];
                        break;
                    }
                }
            }
 */           
            
        }

        private void InitializeDataGrids()
        {

            draftPickData.Columns.Add(AddColumn("Pick", "System.Int16"));
            draftPickData.Columns.Add(AddColumn("Team", "System.String"));
            draftPickData.Columns.Add(AddColumn("Position", "System.String"));
            draftPickData.Columns.Add(AddColumn("Player", "System.String"));

            for (int i = 0; i < 32*7; i++) {
                DraftPickRecord dpRecord = null;

                foreach (TableRecordModel rec in model.TableModels[EditorModel.DRAFT_PICK_TABLE].GetRecords())
                {
                    DraftPickRecord record = (DraftPickRecord)rec;

                    if (record.PickNumber == i)
                    {
                        dpRecord = record;
                        break;
                    }
                }

                DataRow dr = draftPickData.NewRow();
                dr["Pick"] = i+1;
                dr["Team"] = model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).Name;
                dr["Position"] = "";
                dr["Player"] = "";
                draftPickData.Rows.Add(dr);
            }

            draftPickBinding.DataSource = draftPickData;
            DraftResults.DataSource = draftPickBinding;
            DraftResults.Columns["Pick"].Width = 30;
            DraftResults.Columns["Position"].Width = 45;
            DraftResults.Columns["Team"].Width = 75;
            DraftResults.Columns["Player"].Width = DraftResults.Width - DraftResults.Columns["Position"].Width
                - DraftResults.Columns["Team"].Width - DraftResults.Columns["Pick"].Width;

            DraftResults.RowHeadersVisible = false;


            depthChartData.Columns.Add(AddColumn("Player", "System.String"));
            depthChartData.Columns.Add(AddColumn("Depth", "System.Int16"));
            depthChartData.Columns.Add(AddColumn("OVR", "System.Int16"));
            depthChartData.Columns.Add(AddColumn("AGE", "System.Int16"));
            depthChartData.Columns.Add(AddColumn("INJ", "System.Int16"));

            depthChartBinding.DataSource = depthChartData;

            DepthChartGrid.DataSource = depthChartBinding;
            DepthChartGrid.Columns["Depth"].Width = 40;
            DepthChartGrid.Columns["OVR"].Width = 35;
            DepthChartGrid.Columns["AGE"].Width = 35;
            DepthChartGrid.Columns["INJ"].Width = 35;
            DepthChartGrid.Columns["Player"].Width = DepthChartGrid.Width - DepthChartGrid.Columns["Depth"].Width - DepthChartGrid.Columns["OVR"].Width
                - DepthChartGrid.Columns["AGE"].Width - DepthChartGrid.Columns["INJ"].Width - 15;

            DepthChartGrid.RowHeadersVisible = false;


            wishlistData.Columns.Add(AddColumn("PGID", "System.Int16"));
            wishlistData.Columns.Add(AddColumn("Pos", "System.String"));
            wishlistData.Columns.Add(AddColumn("Player", "System.String"));

            wishlistBinding.DataSource = wishlistData;

            wishlistGrid.DataSource = wishlistBinding;
            wishlistGrid.Columns["PGID"].Visible = false;
            wishlistGrid.Columns["Pos"].Width = 30;
            wishlistGrid.Columns["Player"].Width = wishlistGrid.Width - wishlistGrid.Columns["Pos"].Width;
            
            wishlistGrid.RowHeadersVisible = false;


            draftBoardData.Columns.Add(AddColumn("Rank", "System.Int16")); 
            draftBoardData.Columns.Add(AddColumn("Player", "System.String"));
            draftBoardData.Columns.Add(AddColumn("Position", "System.String"));
            draftBoardData.Columns.Add(AddColumn("projectedpick", "System.Int16"));
            draftBoardData.Columns.Add(AddColumn("Proj. Rd.", "System.String"));

            draftBoardBinding.DataSource = draftBoardData;

            DraftBoardGrid.DataSource = draftBoardBinding;
            DraftBoardGrid.Columns["Rank"].Width = 34;
            DraftBoardGrid.Columns["Position"].Width = 45;
            DraftBoardGrid.Columns["projectedpick"].Visible = false;
            DraftBoardGrid.Columns["Proj. Rd."].Width = 60;
            DraftBoardGrid.Columns["Player"].Width = DraftBoardGrid.Width - DraftBoardGrid.Columns["Rank"].Width
                - DraftBoardGrid.Columns["Position"].Width - DraftBoardGrid.Columns["Proj. Rd."].Width - 15;


            DraftBoardGrid.RowHeadersVisible = false;

            rookieData.Columns.Add(AddColumn("PGID", "System.Int32"));
            rookieData.Columns.Add(AddColumn("Player", "System.String"));
            rookieData.Columns.Add(AddColumn("Position", "System.String"));
            rookieData.Columns.Add(AddColumn("picknumber", "System.Int16"));
            rookieData.Columns.Add(AddColumn("Drafted By", "System.String"));
            rookieData.Columns.Add(AddColumn("actualproj", "System.Int16"));
            rookieData.Columns.Add(AddColumn("Actual", "System.String"));
            rookieData.Columns.Add(AddColumn("nflproj", "System.Int16"));
            rookieData.Columns.Add(AddColumn("NFL Proj.", "System.String"));
            rookieData.Columns.Add(AddColumn("myproj", "System.Int16"));
            rookieData.Columns.Add(AddColumn("Our Grade", "System.String"));
            rookieData.Columns.Add(AddColumn("Hrs Scouted", "System.Int16"));
            rookieData.Columns.Add(AddColumn("heightnumber", "System.Int16"));
            rookieData.Columns.Add(AddColumn("Height", "System.String"));
            rookieData.Columns.Add(AddColumn("Weight", "System.Int16"));
            rookieData.Columns.Add(AddColumn("40 Time", "System.Double"));
            rookieData.Columns.Add(AddColumn("Shuttle", "System.Double"));
            rookieData.Columns.Add(AddColumn("Cone", "System.Double"));
            rookieData.Columns.Add(AddColumn("Bench", "System.Int16"));
            rookieData.Columns.Add(AddColumn("Vertical", "System.String"));
            rookieData.Columns.Add(AddColumn("Wonderlic", "System.Int16"));
            rookieData.Columns.Add(AddColumn("doctornumber", "System.Double"));
            rookieData.Columns.Add(AddColumn("Doctor", "System.String"));
            rookieData.Columns.Add(AddColumn("primaryskill", "System.Double"));
            rookieData.Columns.Add(AddColumn("1st Skill", "System.String"));
            rookieData.Columns.Add(AddColumn("secondaryskill", "System.Double"));
            rookieData.Columns.Add(AddColumn("2nd Skill", "System.String"));

            RefillRookieGrid();

            rookieBinding.DataSource = rookieData;

            RookieGrid.DataSource = rookieBinding;
            RookieGrid.Columns["nflproj"].Visible = false;
            RookieGrid.Columns["PGID"].Visible = false;
            RookieGrid.Columns["myproj"].Visible = false;
            RookieGrid.Columns["picknumber"].Visible = false;
            RookieGrid.Columns["heightnumber"].Visible = false;
            RookieGrid.Columns["doctornumber"].Visible = false;
            RookieGrid.Columns["primaryskill"].Visible = false;
            RookieGrid.Columns["secondaryskill"].Visible = false;
            RookieGrid.Columns["actualproj"].Visible = false;

            RookieGrid.Columns["Player"].Width = 100;
            RookieGrid.Columns["Position"].Width = 45;
            RookieGrid.Columns["Drafted By"].Width = 75;
            RookieGrid.Columns["Hrs Scouted"].Width = 70;
            RookieGrid.Columns["Actual"].Width = 54;
            RookieGrid.Columns["NFL Proj."].Width = 54;
            RookieGrid.Columns["Our Grade"].Width = 58;
            RookieGrid.Columns["Height"].Width = 40;
            RookieGrid.Columns["Weight"].Width = 42;
            RookieGrid.Columns["40 Time"].Width = 47;
            RookieGrid.Columns["Shuttle"].Width = 40;
            RookieGrid.Columns["Cone"].Width = 32;
            RookieGrid.Columns["Bench"].Width = 40;
            RookieGrid.Columns["Vertical"].Width = 45;
            RookieGrid.Columns["Wonderlic"].Width = 56;
            RookieGrid.Columns["Doctor"].Width = 40;
            RookieGrid.Columns["1st Skill"].Width = 60;
            RookieGrid.Columns["2nd Skill"].Width = 63;

            RookieGrid.RowHeadersVisible = false;
        }

        private void RefillRookieGrid() {
            /*
             * Tried to get it to keep the selected item selected after a sort
             * or refresh, but couldn't figure out how to force selection of a row.
             * 
            int selected;
            DataGridViewColumn sortedColumn;
            ListSortDirection sortDirection;

            if (RookieGrid.SelectedRows.Count > 0) {
                selected = RookieGrid.SelectedRows[0].Cells["PGID"].Value;
            } else {
                selected = -1;
            }

            if (RookieGrid.SortedColumn != null) {
                sortedColumn = RookieGrid.SortedColumn;
                sortDirection = (ListSortDirection)RookieGrid.SortOrder;
            } else {
                sortedColumn = RookieGrid.Columns["myproj"];
                sortDirection = ListSortDirection.Ascending;
            }
            */

            rookieData.Clear();
            foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
            {
                DataRow dr = rookieData.NewRow();
                if (rook.Value.DraftedTeam < 32)
                {
                    dr["Drafted By"] = model.TeamModel.GetTeamNameFromTeamId(rook.Value.DraftedTeam) + " (" + (rook.Value.DraftPickNumber+1) + ")";

                    if (showDraftedPlayers.Checked == false) {
                        continue;
                    }
                }

                dr["picknumber"] = rook.Value.DraftPickNumber;
                dr["PGID"] = rook.Key;
                dr["Player"] = rook.Value.Player.FirstName + " " + rook.Value.Player.LastName;
                dr["Position"] = Enum.GetNames(typeof(MaddenPositions))[rook.Value.Player.PositionId].ToString();
                dr["nflproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Final];
                dr["NFL Proj."] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Final];
                dr["actualproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Actual];
                dr["Actual"] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Actual];

                dr["Hrs Scouted"] = rook.Value.PreCombineScoutedHours[HumanTeamId] + rook.Value.PostCombineScoutedHours[HumanTeamId];

                dr["myproj"] = rook.Value.CombineNumbers[(int)CombineStat.RoundGrade];
                dr["Our Grade"] = rook.Value.CombineWords[(int)CombineStat.RoundGrade];
/*
                dr["myproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Initial];
                dr[7] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Initial];
 * */
                dr["heightnumber"] = rook.Value.Player.Height;
                dr["Height"] = rook.Value.CombineWords[(int)CombineStat.Height];
                dr["Weight"] = rook.Value.Player.Weight + 160;
                dr["40 Time"] = rook.Value.CombineNumbers[(int)CombineStat.Forty];
                dr["Shuttle"] = rook.Value.CombineNumbers[(int)CombineStat.Shuttle];
                dr["Cone"] = rook.Value.CombineNumbers[(int)CombineStat.Cone];
                dr["Bench"] = rook.Value.CombineNumbers[(int)CombineStat.BenchPress];
                dr["Vertical"] = rook.Value.CombineWords[(int)CombineStat.Vertical];
                dr["Wonderlic"] = rook.Value.CombineNumbers[(int)CombineStat.Wonderlic];
                dr["doctornumber"] = rook.Value.CombineNumbers[(int)CombineStat.Doctor];
                dr["Doctor"] = rook.Value.CombineWords[(int)CombineStat.Doctor];

                dr["primaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
                dr["1st Skill"] = math.SkillToGrade((double)dr["primaryskill"]);

                dr["secondaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
                dr["2nd Skill"] = math.SkillToGrade((double)dr["secondaryskill"]);

                rookieData.Rows.Add(dr);
            }

        }

        private DataColumn AddColumn(string ColName, string ColType)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = ColName;
            dc.DataType = System.Type.GetType(ColType);
            return dc;
        }

        private void draftedPositionsFilterChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            draftPickBinding.RemoveFilter();
            if (!(cb.SelectedItem.Equals("All"))) {
                draftPickBinding.Filter = "Position='" + cb.SelectedItem + "'";
            }
        }

        private void draftedTeamsFilterChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            draftPickBinding.RemoveFilter();
            if (!(cb.SelectedItem.Equals("All"))) {
                draftPickBinding.Filter = "Team='" + cb.SelectedItem + "'";
            }
        }

        private void draftBoardTeamChanged(object sender, EventArgs e)
        {
            UpdateDraftBoard(model.TeamModel.GetTeamIdFromTeamName((string)draftBoardTeam.SelectedItem));
        }

        private void rookieFilterChanged(object sender, EventArgs e)
        {
            rookieBinding.RemoveFilter();
            if (!(RookiePositionFilter.SelectedItem.Equals("All")))
            {
                rookieBinding.Filter = "Position='" + RookiePositionFilter.SelectedItem + "'";
            }


            int pos = (int)RookiePositionFilter.SelectedIndex - 1;
            switch (pos)
            {
                case -1:
                    RookieGrid.Columns["1st Skill"].HeaderText = "1st Skill";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "1st Skill";
                    break;
                case (int)MaddenPositions.QB:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Power";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Accuracy";
                    break;
                case (int)MaddenPositions.HB:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Power";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Hands";
                    break;
                case (int)MaddenPositions.FB:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Blocking";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Running";
                    break;
                case (int)MaddenPositions.WR:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Hands";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Running";
                    break;
                case (int)MaddenPositions.TE:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Receiving";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Blocking";
                    break;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                case (int)MaddenPositions.C:
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Run Block";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Pass Block";
                    break;
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.DT:
                case (int)MaddenPositions.RE:
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.MLB:
                case (int)MaddenPositions.ROLB:
                case (int)MaddenPositions.CB:
                case (int)MaddenPositions.FS:
                case (int)MaddenPositions.SS:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Tackling";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Hands";
                    break;
                case (int)MaddenPositions.K:
                case (int)MaddenPositions.P:
                    RookieGrid.Columns["1st Skill"].HeaderText = "Power";
                    RookieGrid.Columns["2nd Skill"].HeaderText = "Accuracy";
                    break;
            }
        }

        private void UpdateDraftBoard(int TeamId)
        {
            List<RookieRecord> db = dm.GetDraftBoard(model.TeamModel.GetTeamRecord(TeamId), CurrentPick);
            draftBoardData.Clear();

            int count = 1;
            foreach (RookieRecord rook in db)
            {
                DataRow dr = draftBoardData.NewRow();
                dr["Rank"] = count;
                dr["Player"] = rook.Player.FirstName + " " + rook.Player.LastName;
                dr["Position"] = Enum.GetNames(typeof(MaddenPositions))[rook.Player.PositionId].ToString();
                dr["projectedpick"] = rook.EstimatedPickNumber[(int)RookieRecord.RatingType.Final];
                dr["Proj. Rd."] = rook.EstimatedRound[(int)RookieRecord.RatingType.Final];
                draftBoardData.Rows.Add(dr);
                count++;
            }
        }


        private void depthChartFilterChanged(object sender, EventArgs e) {
            UpdateDepthChart(model.TeamModel.GetTeamIdFromTeamName((string)depthChartTeam.SelectedItem),depthChartPosition.SelectedIndex);
        }

        private void UpdateDepthChart(int TeamId, int PositionId)
        {
            if (TeamId >= 0 && TeamId < 32 && PositionId >= 0 && PositionId < 21)
            {

                List<PlayerRecord> dc = dm.GetDepthChart(TeamId, PositionId);
                depthChartData.Clear();
                List<int> rookieRows = new List<int>();

                int row = 0;
                int depth = 1;
                foreach (PlayerRecord player in dc)
                {
                    DataRow dr = depthChartData.NewRow();
                    dr["Player"] = player.FirstName + " " + player.LastName;

/*                    if (player.YearsPro > 0)
                    {
*/                        dr["Depth"] = depth;
                        dr["OVR"] = player.Overall;
                        dr["AGE"] = player.Age;
                        dr["INJ"] = player.Injury;
                        depthChartData.Rows.Add(dr);
                        depth++;
/*                    }
                    else
                    {
                        rookieRows.Add(row);
                        dr["Depth"] = DBNull.Value;
                        dr["OVR"] = DBNull.Value;
                        dr["AGE"] = DBNull.Value;
                        dr["INJ"] = DBNull.Value;
                        depthChartData.Rows.Add(dr);
                    }
*/
                    row++;
                }
/*
                foreach (int i in rookieRows)
                {
                    depthChartData.Rows[i]["Depth"] = depth;
                    depth++;
                }
*/
                DepthChartGrid.Sort(DepthChartGrid.Columns["Depth"], ListSortDirection.Ascending);
            }
        }

        private bool MakePick(RookieRecord drafted)
        {
            drafted = dm.MakeSelection(CurrentPick, drafted);

            draftPickData.Rows[CurrentPick]["Position"] = Enum.GetNames(typeof(MaddenPositions))[drafted.Player.PositionId].ToString();
            draftPickData.Rows[CurrentPick]["Player"] = drafted.Player.FirstName + " " + drafted.Player.LastName;

            for (int j = 0; j < rookieData.Rows.Count; j++)
            {
                if (drafted.PlayerId == (int)rookieData.Rows[j]["PGID"])
                {
                    if (showDraftedPlayers.Checked)
                    {
                        rookieData.Rows[j]["picknumber"] = CurrentPick;
                        rookieData.Rows[j]["Drafted By"] = draftPickData.Rows[CurrentPick]["Team"] + " (" + (CurrentPick+1) + ")";
                    }
                    else
                    {
                        rookieData.Rows.Remove(rookieData.Rows[j]);
                    }

                    break;
                }
            }

            if (CurrentSelectingId == HumanTeamId)
            {
                draftTimer.Start();
                draftButton.Enabled = false;
                PlayerToDraft.Text = "";
            }

            CurrentPick++;

            if (CurrentPick == 32 * 7)
            {
                // End the draft.
                draftTimer.Stop();
                SkipButton.Enabled = false;
                return false;
            }

            CurrentSelectingId = model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick]["Team"]);
            timeRemaining = (int)secondsPerPick;
            clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

            if (CurrentSelectingId == HumanTeamId)
            {
                draftTimer.Stop();
                draftButton.Enabled = true;
            }

            return true;
        }

        private void timerOnTick(object sender, EventArgs e)
        {
            timeRemaining--;
            clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

            double test = random.NextDouble();
            //Console.WriteLine(test + " " + pickProb);

            if (test < pickProb || timeRemaining == 0)
            {
                MakePick(null);
            }
        }

        private void fixSort(object sender, EventArgs e) {
            DataGridView dgv = (DataGridView)sender;
            string column = dgv.SortedColumn.Name;
            int columnindex = dgv.SortedColumn.Index;

            if (column.Equals("Proj. Rd.") || column.Equals("Drafted By") || column.Equals("NFL Proj.") || column.Equals("Doctor") || column.Equals("Actual")
                || column.Equals("Height") || column.Equals("Our Grade") || column.Equals("1st Skill") || column.Equals("2nd Skill"))
            {

                if (sortDirection)
                {
                    dgv.Sort(dgv.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Ascending);
                    sortDirection = false;
                }
                else
                {
                    dgv.Sort(dgv.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Descending);
                    sortDirection = true;
                }
            }
        }

        private string seconds(int secs)
        {
            if (secs > 9)
            {
                return secs.ToString();
            }
            else
            {
                return "0" + secs;
            }
        }

        private void InitializeComboBoxes()
        {
            draftedPositionsFilter.Items.Add("All");
            RookiePositionFilter.Items.Add("All");

            for (int i = 0; i < 21; i++)
            {
                draftedPositionsFilter.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
                depthChartPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
                RookiePositionFilter.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
            }

            List<string> teamNames = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                teamNames.Add(model.TeamModel.GetTeamRecord(i).Name);
            }
            teamNames.Sort();

            draftBoardTeam.Items.AddRange(teamNames.ToArray());
            depthChartTeam.Items.AddRange(teamNames.ToArray());

            teamNames.Insert(0, "All");
            draftedTeamsFilter.Items.AddRange(teamNames.ToArray());
        }

        private void RookieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.RowIndex == -1) { return; }

            SelectedPlayer = (int)((DataRowView)dgv.Rows[e.RowIndex].DataBoundItem).Row[0];

            if (HumanTeamId == CurrentSelectingId && !(dm.GetRookies(-1)[SelectedPlayer].DraftedTeam < 32))
            {
                PlayerToDraft.Text = dm.GetRookies(-1)[SelectedPlayer].Player.ToString();
            }
        }

        private void draftButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (PlayerToDraft.Text.Equals("")) {
                return;
            }

            RookieRecord toDraft = dm.GetRookies(-1)[SelectedPlayer];

            DialogResult dr = MessageBox.Show("Are you sure you want to draft " + toDraft.Player.ToString() + "?", "Draft?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                MakePick(toDraft);
                DraftResults.Invalidate();
            }
        }

        private void SkipButton_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < PicksToSkip.Value; i++)
            {
                if (CurrentSelectingId == HumanTeamId)
                {
                    break;
                }

                if (!MakePick(null))
                {
                    break;
                }
            }

            PicksToSkip.Value = 1;
        }

        private void showDraftedPlayers_CheckedChanged(object sender, EventArgs e)
        {
            RefillRookieGrid();
        }

        private void RookieGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*
            foreach (DataRow drd in wishlistData.Rows)
            {
                if ((int)drd["PGID"] == (int)((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"])
                {
                    return;
                }
            }

            DataRow dr = wishlistData.NewRow();
            
            dr["PGID"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"];
            dr["Pos"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Position"];
            dr["Player"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Player"];

            wishlistData.Rows.Add(dr);
             * */
        }

        private void upButton_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}