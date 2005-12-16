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

		bool done = false;
		bool quitSkipping = false;
		bool skipping = false;
		bool noNotify = false;
		bool disableHumanPick = false;
		int humanBackedUp = 0;

		double pickProb;
		double fastPickProb = 0.1;

		double tradeProbPerm;
		double tradeProb;
		bool preventTrades = false;
		string tradeLog;

		LocalMath math;

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
        DataRow rowToRemove;

		public int CurrentPick = 0;
		public int CurrentSelectingId;
		int HumanTeamId;
		int SelectedPlayer = 0;

		bool sortDirection = true;
		bool preventSortLoop = false;
		int previousSortedColumn = -1;
		public bool refreshTradeTeams = false;

		public TradeUpForm tradeUpForm = null;
		public TradeDownForm tradeDownForm = null;
		TradeOffer globalTradeOffer = null;

		int threadToDo = -1;

		public DraftForm(EditorModel ParentModel, DraftModel draftmodel, int humanId, int seconds)
		{
			dm = draftmodel;
			dm.df = this;

			math = new LocalMath(dm.model.FileVersion);

			HumanTeamId = humanId;
			secondsPerPick = seconds;
			model = ParentModel;

			model.draftStarted = true;

			InitializeComponent();

			statusLabel.Text = "Ready.";


			autoPickBackgroundWorker.WorkerReportsProgress = true;
            autoPickBackgroundWorker.WorkerSupportsCancellation = true;

			autoPickBackgroundWorker.DoWork += new DoWorkEventHandler(SkipButton_Thread);
			autoPickBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(skippingFinished);
			autoPickBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(autoPickBackgroundWorker_ProgressChanged);
		}

		private void DraftForm_Load(object sender, EventArgs e)
		{
			this.Invalidate();

			pickProb = 1 - Math.Pow(0.5, 1.0 / secondsPerPick);
			tradeProb = 1 - Math.Pow(0.5, 60.0 / secondsPerPick);
			tradeProbPerm = tradeProb;

			/*
			dm = new DraftModel(model);
			dm.InitializeDraft(HumanTeamId);

			dm.FixDraftOrder();
			 * */

			InitializeDataGrids();
			InitializeComboBoxes();

			CurrentSelectingId = model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick]["Team"]);
			selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
			selectingLabel.TextAlign = ContentAlignment.MiddleCenter;

			if (CurrentSelectingId == HumanTeamId)
			{
				selectingLabel.ForeColor = System.Drawing.Color.Red;
				SkipButton.Enabled = false;
			}
			else
			{
				selectingLabel.ForeColor = System.Drawing.Color.Black;
			}

			random = new Random(unchecked((int)DateTime.Now.Ticks));

			dm.SetTradeParameters(CurrentPick);

			if (CurrentSelectingId == HumanTeamId)
			{
				draftButton.Enabled = true;
			}

			timeRemaining = (int)secondsPerPick;
			clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);
			draftTimer.Start();

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

			for (int i = 0; i < 32 * 7; i++)
			{
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
				dr["Pick"] = i + 1;
				dr["Team"] = model.TeamModel.GetTeamRecord(dpRecord.CurrentTeamId).Name;
				dr["Position"] = "";
				dr["Player"] = "";
				draftPickData.Rows.Add(dr);
			}

			draftPickBinding.DataSource = draftPickData;
			DraftResults.DataSource = draftPickBinding;
			DraftResults.Columns["Pick"].Width = 34;
			DraftResults.Columns["Position"].Width = 47;
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
			wishlistData.Columns.Add(AddColumn("Rank", "System.Int16"));
			wishlistData.Columns.Add(AddColumn("Pos", "System.String"));
			wishlistData.Columns.Add(AddColumn("ourgrade", "System.Int16"));
			wishlistData.Columns.Add(AddColumn("Grade", "System.String"));
			wishlistData.Columns.Add(AddColumn("Player", "System.String"));

			wishlistBinding.DataSource = wishlistData;

			wishlistGrid.DataSource = wishlistBinding;
			wishlistGrid.Columns["PGID"].Visible = false;
			wishlistGrid.Columns["Rank"].Width = 38;
			wishlistGrid.Columns["Pos"].Width = 38;
			wishlistGrid.Columns["ourgrade"].Visible = false;
			wishlistGrid.Columns["Grade"].Width = 45;
			wishlistGrid.Columns["Player"].Width = wishlistGrid.Width - wishlistGrid.Columns["Pos"].Width;

			wishlistGrid.RowHeadersVisible = false;


			draftBoardData.Columns.Add(AddColumn("Rank", "System.Int16"));
			draftBoardData.Columns.Add(AddColumn("Player", "System.String"));
			draftBoardData.Columns.Add(AddColumn("Position", "System.String"));
			draftBoardData.Columns.Add(AddColumn("projectedpick", "System.Int16"));
			draftBoardData.Columns.Add(AddColumn("Proj. Rd.", "System.String"));

			draftBoardBinding.DataSource = draftBoardData;

			DraftBoardGrid.DataSource = draftBoardBinding;
			DraftBoardGrid.Columns["Rank"].Width = 38;
			DraftBoardGrid.Columns["Position"].Width = 47;
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
			rookieData.Columns.Add(AddColumn("allproj", "System.Int16"));
			rookieData.Columns.Add(AddColumn("All Proj.", "System.String"));
			rookieData.Columns.Add(AddColumn("myproj", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Our Grade", "System.String"));
			rookieData.Columns.Add(AddColumn("Hrs Scouted", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Age", "System.Int16"));
			rookieData.Columns.Add(AddColumn("heightnumber", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Height", "System.String"));
			rookieData.Columns.Add(AddColumn("Weight", "System.Int16"));
			rookieData.Columns.Add(AddColumn("40 Time", "System.String"));
			rookieData.Columns.Add(AddColumn("Shuttle", "System.String"));
			rookieData.Columns.Add(AddColumn("Cone", "System.String"));
			rookieData.Columns.Add(AddColumn("Bench", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Vertical", "System.String"));
			rookieData.Columns.Add(AddColumn("Wonderlic", "System.Int16"));
			rookieData.Columns.Add(AddColumn("doctornumber", "System.Double"));
			rookieData.Columns.Add(AddColumn("Doctor", "System.String"));
			rookieData.Columns.Add(AddColumn("primaryskill", "System.Double"));
			rookieData.Columns.Add(AddColumn("1st Skill", "System.String"));
			rookieData.Columns.Add(AddColumn("secondaryskill", "System.Double"));
			rookieData.Columns.Add(AddColumn("2nd Skill", "System.String"));

			rookieBinding.DataSource = rookieData;

			RookieGrid.DataSource = rookieBinding;
			RookieGrid.Columns["allproj"].Visible = false;
			RookieGrid.Columns["PGID"].Visible = false;
			RookieGrid.Columns["myproj"].Visible = false;
			RookieGrid.Columns["picknumber"].Visible = false;
			RookieGrid.Columns["heightnumber"].Visible = false;
			RookieGrid.Columns["doctornumber"].Visible = false;
			RookieGrid.Columns["primaryskill"].Visible = false;
			RookieGrid.Columns["secondaryskill"].Visible = false;

			RookieGrid.Columns["Player"].Width = 86;
			RookieGrid.Columns["Position"].Width = 47;
			RookieGrid.Columns["Drafted By"].Width = 89;
			RookieGrid.Columns["Hrs Scouted"].Width = 74;
			RookieGrid.Columns["All Proj."].Width = 50;
			RookieGrid.Columns["Our Grade"].Width = 62;
			RookieGrid.Columns["Age"].Width = 30;
			RookieGrid.Columns["Height"].Width = 40;
			RookieGrid.Columns["Weight"].Width = 43;
			RookieGrid.Columns["40 Time"].Width = 52;
			RookieGrid.Columns["Shuttle"].Width = 42;
			RookieGrid.Columns["Cone"].Width = 34;
			RookieGrid.Columns["Bench"].Width = 40;
			RookieGrid.Columns["Vertical"].Width = 45;
			RookieGrid.Columns["Wonderlic"].Width = 58;
			RookieGrid.Columns["Doctor"].Width = 41;
			RookieGrid.Columns["1st Skill"].Width = 62;
			RookieGrid.Columns["2nd Skill"].Width = 65;

			RookieGrid.RowHeadersVisible = false;

			RefillRookieGrid();
		}

		private void RefillRookieGrid()
		{
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
					dr["Drafted By"] = model.TeamModel.GetTeamNameFromTeamId(rook.Value.DraftedTeam) + " (" + (rook.Value.DraftPickNumber + 1) + ")";

					if (showDraftedPlayers.Checked == false)
					{
						continue;
					}
				}

				dr["picknumber"] = rook.Value.DraftPickNumber;
				dr["PGID"] = rook.Key;
				dr["Player"] = rook.Value.Player.FirstName + " " + rook.Value.Player.LastName;
				dr["Position"] = Enum.GetNames(typeof(MaddenPositions))[rook.Value.Player.PositionId].ToString();
				dr["allproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Final];
				dr["All Proj."] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Final];
				/*                dr["actualproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Actual];
								dr["Actual"] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Actual];
				*/
				dr["Hrs Scouted"] = rook.Value.PreCombineScoutedHours[HumanTeamId] + rook.Value.PostCombineScoutedHours[HumanTeamId];

				dr["myproj"] = rook.Value.CombineNumbers[(int)CombineStat.RoundGrade];
				dr["Our Grade"] = rook.Value.CombineWords[(int)CombineStat.RoundGrade];
				/*
								dr["myproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Initial];
								dr[7] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Initial];
				 * */
				dr["heightnumber"] = rook.Value.Player.Height;
				dr["Age"] = rook.Value.Player.Age;
				dr["Height"] = rook.Value.CombineWords[(int)CombineStat.Height];
				dr["Weight"] = rook.Value.Player.Weight + 160;
				dr["40 Time"] = rook.Value.CombineNumbers[(int)CombineStat.Forty].ToString("N2");
				dr["Shuttle"] = rook.Value.CombineNumbers[(int)CombineStat.Shuttle].ToString("N2");
				dr["Cone"] = rook.Value.CombineNumbers[(int)CombineStat.Cone].ToString("N2");
				dr["Bench"] = rook.Value.CombineNumbers[(int)CombineStat.BenchPress];
				dr["Vertical"] = rook.Value.CombineWords[(int)CombineStat.Vertical];
				dr["Wonderlic"] = rook.Value.CombineNumbers[(int)CombineStat.Wonderlic];
				dr["doctornumber"] = rook.Value.CombineNumbers[(int)CombineStat.Doctor];
				dr["Doctor"] = rook.Value.CombineWords[(int)CombineStat.Doctor];

				dr["primaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
				dr["1st Skill"] = math.SkillToGrade((double)dr["primaryskill"]);

				dr["secondaryskill"] = rook.Value.SecondarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
				dr["2nd Skill"] = math.SkillToGrade((double)dr["secondaryskill"]);

				rookieData.Rows.Add(dr);
			}

			RookieGrid.Sort(RookieGrid.Columns["myproj"], ListSortDirection.Ascending);
			RookieGrid.CurrentCell = RookieGrid[1, 0];
		}

		public string pickToString(int pick, int con)
		{
			if (pick < 1000)
			{
				int round = pick / 32 + 1;
				int pickInRound = pick % 32 + 1;

				return "Round " + round + ", Pick " + pickInRound + " (" + dm.pickValues[pick] + ")";
			}
			else
			{
				int round = pick - 1000;
				return "Round " + (pick - 1000) + ", Next Year" + " (" + dm.futureValues(round, con) + ")";
			}
		}

		public double pickvalue(int pick, int con)
		{
			if (pick < 1000)
			{
				return dm.pickValues[pick];
			}
			else
			{
				return dm.futureValues(pick - 1000, con);
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
			if (!(cb.SelectedItem.Equals("All")))
			{
				draftPickBinding.Filter = "Position='" + cb.SelectedItem + "'";
			}
		}

		private void draftedTeamsFilterChanged(object sender, EventArgs e)
		{
			ComboBox cb = (ComboBox)sender;
			draftPickBinding.RemoveFilter();
			if (cb.SelectedIndex > -1 && !(cb.SelectedItem.Equals("All")))
			{
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
					RookieGrid.Columns["2nd Skill"].HeaderText = "2nd Skill";
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


		private void depthChartFilterChanged(object sender, EventArgs e)
		{
			UpdateDepthChart(model.TeamModel.GetTeamIdFromTeamName((string)depthChartTeam.SelectedItem), depthChartPosition.SelectedIndex);
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

					if (player.YearsPro > 0)
					{
						dr["Depth"] = depth;
						dr["OVR"] = dm.dcr.GetAdjustedOverall(player, PositionId);
						dr["AGE"] = player.Age;
						dr["INJ"] = player.Injury;
						depthChartData.Rows.Add(dr);
						depth++;
					}
					else
					{
						rookieRows.Add(row);
						/*
						dr["Depth"] = depth;
						dr["OVR"] = dm.rookies[player.PlayerId].GetAdjustedOverall(TeamId, (int)RookieRecord.RatingType.Final, PositionId, dm.dcr.awarenessAdjust);
						dr["AGE"] = dm.dcr.GetAdjustedOverall(player, PositionId);
						dr["INJ"] = player.Injury;
						 * */

						dr["Depth"] = DBNull.Value;
						dr["OVR"] = DBNull.Value;
						dr["AGE"] = player.Age;
						dr["INJ"] = DBNull.Value;

						depthChartData.Rows.Add(dr);
						//depth++; // COMMENT OUT LATER
					}

					row++;
				}

				foreach (int i in rookieRows)
				{
					depthChartData.Rows[i]["Depth"] = depth;
					depth++;
				}

				DepthChartGrid.Sort(DepthChartGrid.Columns["Depth"], ListSortDirection.Ascending);
			}
		}

		void autoPickBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (threadToDo == 1)
			{
				draftTimer.Stop();

                if (tradeDownForm != null)
                {
                    tradeDownForm.Close();
                    tradeDownForm = null;
                }

                if (tradeUpForm != null)
                {
                    tradeUpForm.Close();
                    tradeUpForm = null;
                }
            }
			else if (threadToDo == 2)
			{
				SkipButton.Enabled = true;
				draftButton.Enabled = false;
				PlayerToDraft.Text = "";
			}
			else if (threadToDo == 3)
			{
				draftTimer.Stop();

				SkipButton.Enabled = false;
				statusLabel.Text = "Done.";
				string filename = model.GetFileName();
				string path = filename.Substring(0, filename.LastIndexOf('\\'));
				string file = filename.Substring(filename.LastIndexOf('\\') + 1);
				string fileNoExt = file.Substring(0, file.LastIndexOf('.'));

				MessageBox.Show("Draft complete.\n\nYou will now be prompted to save the file needed\nto transfer future draft picks.");

				SaveFileDialog sfd = new SaveFileDialog();
				sfd.InitialDirectory = path + "\\";
				sfd.FileName = fileNoExt + ".dpd";
				sfd.AddExtension = true;
				sfd.DefaultExt = "dpd";
				sfd.Filter = "Draft pick data files (*.dpd)|*.dpd";

				DialogResult dr = sfd.ShowDialog();

				Console.WriteLine(sfd.FileName);

				dm.SavePicks(sfd.FileName);

				MessageBox.Show("Draft pick data saved.\n\nTo load this data, save your franchise file within Madden\nat the \"Training Camp\" stage, load your franchise file in Madden Editor,\ngo to the \"Franchise\" menu, choose \"Move Traded Draft Picks\",\nand load the file you just saved.\n\nYou may close this form now and save your file from the main editor screen.");
			}
			else if (threadToDo == 4)
			{
				selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
				selectingLabel.TextAlign = ContentAlignment.MiddleCenter;

				if (CurrentSelectingId == HumanTeamId)
				{
					selectingLabel.ForeColor = System.Drawing.Color.Red;

					if (wishlistData.Rows.Count > 0)
					{
						SelectedPlayer = (short)wishlistGrid["PGID", 0].Value;
						PlayerToDraft.Text = dm.GetRookies(-1)[SelectedPlayer].Player.ToString();
					}
				}
				else
				{
					selectingLabel.ForeColor = System.Drawing.Color.Black;
				}

				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				if (CurrentSelectingId == HumanTeamId)
				{
					SkipButton.Enabled = false;
					draftButton.Enabled = true;
				}
			}
			else if (threadToDo == 5)
			{
				selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
				selectingLabel.TextAlign = ContentAlignment.MiddleCenter;

				if (CurrentSelectingId == HumanTeamId)
				{
					selectingLabel.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					selectingLabel.ForeColor = System.Drawing.Color.Black;
				}

				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				if (CurrentSelectingId == HumanTeamId)
				{
					draftButton.Enabled = true;
				}
			}
			else if (threadToDo == 6)
			{
				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);
			}
			else if (threadToDo == 7)
			{
				progressBar.Value = e.ProgressPercentage;
			}
			else if (threadToDo == 8)
			{
				tradeButton.Enabled = false;
			}
			else if (threadToDo == 9)
			{
				tradeUpForm = new TradeUpForm(dm, this, globalTradeOffer);
				tradeUpForm.Show();
				tradeButton.Enabled = false;
			}
			else if (threadToDo == 10)
			{
                wishlistData.Rows.Remove(rowToRemove);
                rerankBoard();
                
                wishlistGrid.Invalidate(true);
				wishlistGrid.Update();
			}
            else if (threadToDo == 11)
            {
                RookieGrid.Invalidate(true);
                RookieGrid.Update();
            }
		}

		private bool MakePick(RookieRecord drafted)
		{
			// Stop the timer while we process\
			DateTime total = DateTime.Now;

			if (skipping)
			{
				threadToDo = 1;
				autoPickBackgroundWorker.ReportProgress(0);
			}
			else
			{
				draftTimer.Stop();

                if (tradeDownForm != null)
                {
                    tradeDownForm.Close();
                    tradeDownForm = null;
                }

                if (tradeUpForm != null)
                {
                    tradeUpForm.Close();
                    tradeUpForm = null;
                }
            }

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
						rookieData.Rows[j]["Drafted By"] = draftPickData.Rows[CurrentPick]["Team"] + " (" + (CurrentPick + 1) + ")";
					}
					else
					{
						rookieData.Rows.Remove(rookieData.Rows[j]);

                        if (skipping)
                        {
                            threadToDo = 11;
                            autoPickBackgroundWorker.ReportProgress(0);
                        }
                        else
                        {
                            RookieGrid.Invalidate(true);
                            RookieGrid.Update();
                        }
                    }

					break;
				}
			}

            /*
            DataRow wishrow;
            try
            {
                wishrow = wishlistRow(drafted.PlayerId);
            }
            catch
            {
                Console.WriteLine("Line 796");
                while (true)
                { }
            }
             * */

            rowToRemove = wishlistRow(drafted.PlayerId);
            if (rowToRemove != null)
			{
				if (skipping)
				{
                    threadToDo = 10;
                    autoPickBackgroundWorker.ReportProgress(0);
				}
				else
				{
                    wishlistData.Rows.Remove(rowToRemove);
                    rerankBoard();

                    wishlistGrid.Invalidate(true);
					wishlistGrid.Update();
				}
			}

			if (CurrentSelectingId == HumanTeamId && humanBackedUp <= 1)
			{
				if (skipping)
				{
					threadToDo = 2;
					autoPickBackgroundWorker.ReportProgress(0);
				}
				else
				{
					SkipButton.Enabled = true;
					draftButton.Enabled = false;
					PlayerToDraft.Text = "";
				}
			}

			CurrentPick++;

			if (CurrentPick >= 32 * 7)
			{
				done = true;

				// End the draft.
				if (skipping)
				{
					threadToDo = 3;
					autoPickBackgroundWorker.ReportProgress(0);
				}
				else
				{
					draftTimer.Stop();
					SkipButton.Enabled = false;
					statusLabel.Text = "Done.";
					string filename = model.GetFileName();
					string path = filename.Substring(0, filename.LastIndexOf('\\'));
					string file = filename.Substring(filename.LastIndexOf('\\') + 1);
					string fileNoExt = file.Substring(0, file.LastIndexOf('.'));

					MessageBox.Show("Draft complete.\n\nYou will now be prompted to save the file needed\nto transfer future draft picks.");

					SaveFileDialog sfd = new SaveFileDialog();
					sfd.InitialDirectory = path + "\\";
					sfd.FileName = fileNoExt + ".dpd";
					sfd.AddExtension = true;
					sfd.DefaultExt = "dpd";
					sfd.Filter = "Draft pick data files (*.dpd)|*.dpd";

					DialogResult dr = sfd.ShowDialog();

					Console.WriteLine(sfd.FileName);

					dm.SavePicks(sfd.FileName);

					MessageBox.Show("Draft pick data saved.\n\nTo load this data, save your franchise file within Madden\nat the \"Training Camp\" stage, load your franchise file in Madden Editor,\ngo to the \"Franchise\" menu, choose \"Move Traded Draft Picks\",\nand load the file you just saved.\n\nYou may close this form now and save your file from the main editor screen.");
				}

				return false;
			}

			if (humanBackedUp <= 1)
			{
				dm.SetTradeParameters(CurrentPick);
				preventTrades = false;
			}

			if (!skipping && humanBackedUp <= 1)
			{
				tradeButton.Enabled = true;
			}

			if (tradeUpForm != null)
			{
				tradeUpForm = null;
			}

			CurrentSelectingId = model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick]["Team"]);

			if (humanBackedUp <= 0)
			{
				timeRemaining = (int)secondsPerPick;
			}

			if (skipping)
			{
				threadToDo = 4;
				autoPickBackgroundWorker.ReportProgress(0);
			}
			else
			{
				selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
				selectingLabel.TextAlign = ContentAlignment.MiddleCenter;

				if (CurrentSelectingId == HumanTeamId)
				{
					selectingLabel.ForeColor = System.Drawing.Color.Red;

					if (wishlistData.Rows.Count > 0)
					{
						SelectedPlayer = (short)wishlistGrid["PGID", 0].Value;
						PlayerToDraft.Text = dm.GetRookies(-1)[SelectedPlayer].Player.ToString();
					}
				}
				else
				{
					selectingLabel.ForeColor = System.Drawing.Color.Black;
				}

				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				if (CurrentSelectingId == HumanTeamId)
				{
					draftButton.Enabled = true;
					SkipButton.Enabled = false;
				}

				draftTimer.Start();
			}

			Console.WriteLine("Total MakePick: " + total.Subtract(DateTime.Now));
			return true;
		}

		private void timerOnTick(object sender, EventArgs e)
		{
			if (!skipping)
			{
				tick(false);
			}
			else
			{
			}
		}

		public void DisableTradeButton()
		{
			tradeButton.Enabled = false;
		}

		private string suffix(int i)
		{
			int tens = i % 100;
			int ones = i % 10;
			if (tens == 11 || tens == 12 || tens == 13) { return "th"; }
			else
			{
				switch (ones)
				{
					case 1: return "st";
					case 2: return "nd";
					case 3: return "rd";
					default: return "th";
				}
			}
		}

		public void ProcessTrade(TradeOffer to)
		{
			if (skipping && !noNotify)
			{
				threadToDo = 1;
				autoPickBackgroundWorker.ReportProgress(0);
			}
			else
			{
				draftTimer.Stop();
			}

			string highergets = "";
			string lowergets = "";

			to.PicksFromHigher.Add(CurrentPick);

			for (int i = 0; i < 32 * 7; i++)
			{
				if (to.PicksFromHigher.Contains(i))
				{
					if (lowergets.Length > 0) { lowergets += ", "; }
					draftPickData.Rows[i]["Team"] = model.TeamModel.GetTeamNameFromTeamId(to.LowerTeam);
					lowergets += (i + 1) + suffix(i + 1) + " Overall Pick (" + dm.pickValues[i] + " value)";
				}
				else if (to.PicksFromLower.Contains(i))
				{
					if (highergets.Length > 0) { highergets += ", "; }
					draftPickData.Rows[i]["Team"] = model.TeamModel.GetTeamNameFromTeamId(to.HigherTeam);
					highergets += (i + 1) + suffix(i + 1) + " Overall Pick (" + dm.pickValues[i] + " value)";
				}
			}

			foreach (int pick in to.PicksFromHigher)
			{
				if (pick > 1000)
				{
					if (lowergets.Length > 0) { lowergets += ", "; }
					lowergets += (pick - 1000) + suffix(pick - 1000) + " Round Pick Next Year (" + dm.futureValues(pick - 1000, model.TeamModel.GetTeamRecord(to.HigherTeam).CON) + " value)";
				}
			}

			foreach (int pick in to.PicksFromLower)
			{
				if (pick > 1000)
				{
					if (highergets.Length > 0) { highergets += ", "; }
					highergets += (pick - 1000) + suffix(pick - 1000) + " Round Pick Next Year (" + dm.futureValues(pick - 1000, model.TeamModel.GetTeamRecord(to.HigherTeam).CON) + " value)";
				}
			}

			if (tradeDownForm != null)
			{
				tradeDownForm.Close();
				tradeDownForm = null;
			}

			if (tradeUpForm != null)
			{
				tradeUpForm.Close();
				tradeUpForm = null;
			}

			tradeLog += model.TeamModel.GetTeamNameFromTeamId(to.LowerTeam) + " get " + lowergets + "\n" + model.TeamModel.GetTeamNameFromTeamId(to.HigherTeam) + " get " + highergets + "\n\n\n";

			if (!noNotify)
			{
				MessageBox.Show("Trade!\n\n" + tradeLog.Trim(), "", MessageBoxButtons.OKCancel);
				tradeLog = "";
			}

			//this.Invalidate(true);
			//this.Update();

			tradeButton.Enabled = false;

			if (CurrentSelectingId == HumanTeamId)
			{
				draftButton.Enabled = false;
				SkipButton.Enabled = true;
				PlayerToDraft.Text = "";
			}

			dm.SetTradeParameters(CurrentPick);

			CurrentSelectingId = model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick]["Team"]);
			timeRemaining = (int)secondsPerPick;
			preventTrades = true;

			if (skipping)
			{
				threadToDo = 5;
				autoPickBackgroundWorker.ReportProgress(0);
			}
			else
			{
				selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
				selectingLabel.TextAlign = ContentAlignment.MiddleCenter;

				if (CurrentSelectingId == HumanTeamId)
				{
					selectingLabel.ForeColor = System.Drawing.Color.Red;
				}
				else
				{
					selectingLabel.ForeColor = System.Drawing.Color.Black;
				}

				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				if (CurrentSelectingId == HumanTeamId)
				{
					draftButton.Enabled = true;
				}

				draftTimer.Start();
			}
		}

		private void tick(bool refresh)
		{
			timeRemaining--;

			if (humanBackedUp > 0)
			{
				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				if (timeRemaining % 2 == 0)
				{
					selectingLabel.Text = "";
				}
				else
				{
					selectingLabel.Text = "On the Clock: " + (string)draftPickData.Rows[CurrentPick]["Team"];
				}

				if (timeRemaining > 0 && (random.NextDouble() > fastPickProb || NextSelectingId() == HumanTeamId))
				{
					return;
				}
				else if (timeRemaining <= 0 && NextSelectingId() == HumanTeamId)
				{
					humanBackedUp++;

					timeRemaining = (int)secondsPerPick;
					return;
				}

				// (1) disable draft button

				draftButton.Enabled = false;
				disableHumanPick = true;
				draftTimer.Stop();

				// (2) force trade with next highest CPU team

				RookieRecord drafted = dm.SkipHuman(CurrentPick, humanBackedUp);

				string currentteam = (string)draftPickData.Rows[CurrentPick]["Team"];
				string nextteam = (string)draftPickData.Rows[CurrentPick + humanBackedUp]["Team"];

				draftPickData.Rows[CurrentPick]["Team"] = nextteam;
				draftPickData.Rows[CurrentPick + humanBackedUp]["Team"] = currentteam;


				// (3) CPU team makes pick

				draftPickData.Rows[CurrentPick]["Position"] = Enum.GetNames(typeof(MaddenPositions))[drafted.Player.PositionId].ToString();
				draftPickData.Rows[CurrentPick]["Player"] = drafted.Player.FirstName + " " + drafted.Player.LastName;

				for (int j = 0; j < rookieData.Rows.Count; j++)
				{
					if (drafted.PlayerId == (int)rookieData.Rows[j]["PGID"])
					{
						if (showDraftedPlayers.Checked)
						{
							rookieData.Rows[j]["picknumber"] = CurrentPick;
							rookieData.Rows[j]["Drafted By"] = draftPickData.Rows[CurrentPick]["Team"] + " (" + (CurrentPick + 1) + ")";
						}
						else
						{
							rookieData.Rows.Remove(rookieData.Rows[j]);
						}

						break;
					}
				}

				CurrentPick++;

				timeRemaining = (int)secondsPerPick;
				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

				// (4) enable draft button

				CurrentSelectingId = HumanTeamId;
				draftButton.Enabled = true;
				disableHumanPick = false;
				draftTimer.Start();
			}

			if (skipping)
			{
				threadToDo = 6;
				autoPickBackgroundWorker.ReportProgress(0);
			}
			else
			{
				clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);
			}

			double test = random.NextDouble();
			refreshTradeTeams = false;

			if (timeRemaining <= 0)
			{
				if (CurrentSelectingId == HumanTeamId)
				{
					if (CurrentPick == 32 * 7 - 1) { return; }

					if (tradeDownForm != null)
					{
						draftTimer.Stop();
						tradeDownForm.Close();
						tradeDownForm = null;
						draftTimer.Start();
					}

					tradeButton.Enabled = false;

					humanBackedUp++;
					preventTrades = true;

					foreach (TradeOffer locTO in dm.tradeOffers.Values)
					{
						locTO.status = (int)TradeOfferStatus.Rejected;
					}

					timeRemaining = (int)secondsPerPick;
					clock.Text = Math.Floor((double)timeRemaining / 60) + ":" + seconds(timeRemaining % 60);

					return;
				}

				TradeOffer to = dm.tradePendingAccept();

				if (to == null)
				{
					MakePick(null);
					return;
				}
				else
				{
					dm.AcceptTrade(to);
					ProcessTrade(to);
					return;
				}
			}
			else if (CurrentSelectingId != HumanTeamId && !dm.tradePending(-1) && (test < pickProb || ((dm.tradeOffers.Count == 31 && skipping) || (skipping && preventTrades) || (skipping && dm.tradeOffers.Count == 30 && !dm.tradeExists(HumanTeamId)))))
			{
				MakePick(null);
				return;
			}
			else if (!preventTrades)
			{
				Console.WriteLine(dm.tradeOffers.Count);
				if (skipping)
				{
					Console.WriteLine("skipping " + tradeProb);
				}
				if (!dm.tradeExists(HumanTeamId))
				{
					Console.WriteLine("No Human trade");
				}

				// randomize the team that starts the trade bidding.
				int i = (int)Math.Floor(32 * random.NextDouble());

				// j just counts iterations; current team should be 'i'.
				for (int j = 0; j < 32; j++)
				{
					if (i == CurrentSelectingId) { continue; }

					if (skipping && noNotify && i == HumanTeamId) { continue; }

					test = random.NextDouble();

					if (skipping || test < tradeProb)
					{
						if (!dm.tradeExists(i))
						{
							Console.WriteLine("Initiating trade talks with " + model.TeamModel.GetTeamNameFromTeamId(i) + "...");
							TradeOffer to = dm.tradeInitialOffer(i, CurrentPick, (double)timeRemaining / (double)secondsPerPick);

							if (to != null)
							{
								if (CurrentSelectingId == HumanTeamId && tradeDownForm == null)
								{
									string teamName = dm.model.TeamModel.GetTeamNameFromTeamId(to.LowerTeam);
									draftTimer.Stop();
									DialogResult dr = MessageBox.Show("The " + teamName + " have a trade offer for you.\nDo you want to start trade discussions with them?\nIf not, you will not be able to negotiate with them again on this pick.", "Trade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
									draftTimer.Start();

									if (dr == DialogResult.Yes)
									{
										tradeDownForm = new TradeDownForm(dm, this, to);
										tradeDownForm.Show();
										tradeButton.Enabled = false;
									}
									else
									{
										to.status = (int)TradeOfferStatus.Rejected;
									}
								}
								else if (CurrentSelectingId != HumanTeamId)
								{
									if (!noNotify)
									{
										DialogResult dr = MessageBox.Show("The " + dm.model.TeamModel.GetTeamNameFromTeamId(CurrentSelectingId) + " have a trade offer for you.\nDo you want to start trade discussions with them?", "Trade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

										if (dr == DialogResult.Yes)
										{
											if (skipping)
											{
												threadToDo = 9;
												globalTradeOffer = to;
												autoPickBackgroundWorker.ReportProgress(0);
											}
											else
											{
												tradeUpForm = new TradeUpForm(dm, this, to);
												tradeUpForm.Show();
												tradeButton.Enabled = false;
											}

											quitSkipping = true;
										}
										else
										{
											to.status = (int)TradeOfferStatus.Rejected;
											if (skipping)
											{
												threadToDo = 8;
												autoPickBackgroundWorker.ReportProgress(0);
											}
											else
											{
												tradeButton.Enabled = false;
											}
										}
									}
								}
							}
						}
						else if (dm.tradePending(i))
						{
							Console.WriteLine("Continuing trade offer with " + model.TeamModel.GetTeamNameFromTeamId(i) + "...");
							TradeOffer to = dm.tradeCounterOffer(i, (double)timeRemaining / (double)secondsPerPick);

							if (to != null && CurrentSelectingId != HumanTeamId)
							{
								ProcessTrade(to);
								return;
							}
						}
						else if (dm.tradeOffers[i].status == (int)TradeOfferStatus.PendingAccept && ((double)timeRemaining / (double)secondsPerPick < 0.5 || skipping))
						{
							bool otherOffer = false;
							for (int k = 0; k < 32; k++)
							{
								if (dm.tradePending(k))
								{
									otherOffer = true;
									break;
								}
							}

							if (!otherOffer)
							{
								dm.AcceptTrade(dm.tradeOffers[i]);
								ProcessTrade(dm.tradeOffers[i]);
								return;
							}
						}
					}

					i++;
					if (i == 32) { i = 0; }
				}

				if (tradeDownForm != null && refreshTradeTeams)
				{
					tradeDownForm.FillTeamBoxes();
				}
			}
		}

		private void fixSort(object sender, EventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			string column = dgv.SortedColumn.Name;
			int columnindex = dgv.SortedColumn.Index;

			if (column.Equals("Proj. Rd.") || column.Equals("Drafted By") || column.Equals("All Proj.") || column.Equals("Doctor") || column.Equals("Actual")
				|| column.Equals("Height") || column.Equals("Our Grade") || column.Equals("1st Skill") || column.Equals("2nd Skill"))
			{

				if ((sortDirection && !((previousSortedColumn + 1) != columnindex && columnindex > 23)) || ((previousSortedColumn + 1) != columnindex && columnindex < 23))
				{
					dgv.Sort(dgv.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Ascending);
					sortDirection = false;
				}
				else
				{
					dgv.Sort(dgv.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Descending);
					sortDirection = true;
				}

				return;
			}

			previousSortedColumn = columnindex;
			RookieGrid.CurrentCell = RookieGrid[1, 0];
		}

		private void wishlistFixSort(object sender, EventArgs e)
		{
			if (preventSortLoop)
			{
				return;
			}

			preventSortLoop = true;

			string column = wishlistGrid.SortedColumn.Name;

			if (column.Equals("Grade"))
			{
				wishlistGrid.Sort(wishlistGrid.Columns["ourgrade"], ListSortDirection.Ascending);
			}
			else if (!column.Equals("PGID"))
			{
				wishlistGrid.Sort(wishlistGrid.Columns[1], ListSortDirection.Ascending);
			}

			rerankBoard();
			preventSortLoop = false;
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

		private void draftButton_Click(object sender, EventArgs e)
		{
			if (PlayerToDraft.Text.Equals(""))
			{
				return;
			}

			RookieRecord toDraft = dm.GetRookies(-1)[SelectedPlayer];

			DialogResult dr = MessageBox.Show("Are you sure you want to draft " + toDraft.Player.ToString() + "?", "Draft?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (dr == DialogResult.Yes)
			{
				draftTimer.Stop();

				// We should only get caught in this loop if the human is
				// getting skipped, and the CPU is in the middle of picking.

				while (disableHumanPick)
				{
				}

				if (!(toDraft.DraftedTeam < 32))
				{
					MakePick(toDraft);
					if (humanBackedUp > 0)
					{
						humanBackedUp--;
					}

					if (humanBackedUp <= 0)
					{
						preventTrades = false;
						tradeButton.Enabled = true;
					}

					DraftResults.Invalidate();
				}
				else
				{
					draftTimer.Start();
				}
			}
		}

		private void SkipButton_Click(object sender, EventArgs e)
		{
			progressBar.Value = 0;
			SkipButton.Enabled = false;
			PicksToSkip.Enabled = false;
			tradeButton.Enabled = false;
            
			draftTimer.Stop();
			statusLabel.Text = "Skipping Picks...";

			autoPickBackgroundWorker.RunWorkerAsync();
		}

		private void SkipButton_Thread(object sender, DoWorkEventArgs e)
		{
			int initialPick = CurrentPick;
			skipping = true;
			tradeProb = 1;
			tradeLog = "";

			int nextHumanPick = dm.GetNextPick(HumanTeamId, CurrentPick);

			int totalToSkip = (int)Math.Min((double)PicksToSkip.Value, (double)nextHumanPick - CurrentPick);
			totalToSkip = (int)Math.Min((double)totalToSkip, 32.0 * 7 - CurrentPick);

			DialogResult dr = MessageBox.Show("Interrupt skipping to receive trade offers?", "Interupt?", MessageBoxButtons.YesNoCancel);

			while (dr != DialogResult.Cancel && !autoPickBackgroundWorker.CancellationPending && !quitSkipping && CurrentPick < initialPick + PicksToSkip.Value && CurrentSelectingId != HumanTeamId && CurrentPick < 32 * 7)
			{
				if (dr == DialogResult.No)
				{
					noNotify = true;
					TradeOffer to = null;
					while (!preventTrades)
					{
						int bestId = dm.GetBestOffer();

						if (bestId == HumanTeamId || bestId == CurrentSelectingId) { dm.BestOffers.Remove(bestId); continue; }

						if (bestId == -1)
						{
							break;
						}
						else if (dm.BestOffers[bestId] < dm.pickValues[CurrentPick + 1] + 1)
						{
							break;
						}
						else
						{
							TradeOffer temp = dm.setupTradeOffer(bestId, CurrentPick);

							double tempOffer = 10000;
							temp.makeCounterOffer(tempOffer, false);

							if (temp.MaxGive > temp.MinAccept)
							{
								tempOffer = 0.25 * (3 * temp.MinAccept + temp.MaxGive);

								temp.makeCounterOffer(tempOffer, false);

								if (temp.offersFromLower.Count >= 2 && temp.offersFromLower[1] > temp.MinAccept && temp.offersFromLower[1] < temp.MaxGive)
								{
									to = temp;
									break;
								}
							}
						}

						dm.BestOffers.Remove(bestId);
					}

					if (to != null)
					{
						dm.AcceptTrade(to);
						ProcessTrade(to);
					}
					else
					{
						if (!MakePick(null))
						{
							break;
						}
					}

					//                    this.Invalidate(true);
					//                  this.Update();

					threadToDo = 7;
					autoPickBackgroundWorker.ReportProgress(100 * (CurrentPick - initialPick) / totalToSkip);
				}
				else
				{
					tick(true);
				}
			}

			tradeProb = tradeProbPerm;

			skipping = false;
			quitSkipping = false;
		}

		private void skippingFinished(object sender, RunWorkerCompletedEventArgs e)
		{
			if (noNotify && tradeLog.Length > 0)
			{
				MessageBox.Show(tradeLog.Trim(), "Trade Log", MessageBoxButtons.OK);
			}
			tradeLog = "";

			noNotify = false;

			if (CurrentPick < 32 * 7)
			{
				if (CurrentSelectingId != HumanTeamId)
				{
					SkipButton.Enabled = true;
				}
				PicksToSkip.Enabled = true;

				if (!preventTrades && tradeUpForm == null)
				{
					tradeButton.Enabled = true;
				}

				draftTimer.Start();
				statusLabel.Text = "Ready.";
			}
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

		private void tradeButton_Click(object sender, EventArgs e)
		{
			if (CurrentSelectingId == HumanTeamId)
			{
				int pick = (int)DraftResults.CurrentRow.Index;
				if (pick <= CurrentPick)
				{
					return;
				}

				string teamName = (string)DraftResults.Rows[pick].Cells["Team"].Value;
				int teamId = dm.model.TeamModel.GetTeamIdFromTeamName(teamName);

				if (dm.tradeExists(teamId))
				{
					MessageBox.Show("You have already rejected a trade offer from this team.");
					return;
				}

				DialogResult dr = MessageBox.Show("Are you sure you want to make a trade offer to the " + teamName + "?  If you start, you can always cancel, but you can't restart after you've cancelled.  You WILL be able to scout rookies while you negotiate the trade.", "Trade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (dr == DialogResult.Yes)
				{
					tradeDownForm = new TradeDownForm(dm, this, dm.setupTradeOffer(teamId, CurrentPick));
					tradeDownForm.Show();
					tradeButton.Enabled = false;
				}
			}
			else
			{
				DialogResult dr = MessageBox.Show("Are you sure you want to make a trade offer?  If you start, you can always cancel, but you can't restart after you've cancelled.  You WILL be able to scout rookies while you negotiate the trade.", "Trade?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (dr == DialogResult.Yes)
				{
					tradeUpForm = new TradeUpForm(dm, this, dm.setupTradeOffer(HumanTeamId, CurrentPick));
					tradeUpForm.Show();
					tradeButton.Enabled = false;
				}
			}
		}

		private void draftHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string helpstring = "When it's your turn to pick, click on the player you want in the list\n" +
				"of rookies, then click \"Draft\".\n\n" +
				"If you want to trade down instead, choose the team you want to make an offer to, and\n" +
				"click \"Make Trade Offer\".\n\n" +
				"When the CPU is picking, you can make a trade offer to the current team by clicking\n" +
				"\"Make Trade Offer\".  If they are interested, the CPU might offer you a trade as well.\n\n" +
				"You can also skip a set number of picks by choosing a number in the \"Advance\" field,\n" +
				"then clicking \"Advance\".  You will be asked if you want to still receive trade offers while\n" +
				"skipping.  If you choose \"No\", skipping will move faster.\n\n" +
				"The top box on the right allows you to look at a team's draft board -- who that team is\n" +
				"likely to draft.  The lower box on the right allows you to look at a team's depth chart\n" +
				"at the position you choose.";

			MessageBox.Show(helpstring, "Help");
		}

		private int NextSelectingId()
		{
			if (CurrentPick + humanBackedUp >= 32 * 7)
			{
				return HumanTeamId;
			}
			else
			{
				return model.TeamModel.GetTeamIdFromTeamName((string)draftPickData.Rows[CurrentPick + humanBackedUp]["Team"]);
			}
		}

		private void DraftResults_DoubleClick(object sender, EventArgs e)
		{
			if (DraftResults.SelectedRows.Count <= 0)
			{
				return;
			}

			if ((short)DraftResults.SelectedRows[0].Cells["Pick"].Value > CurrentPick && stickyDraftBoards.Checked)
			{
				draftBoardTeam.SelectedItem = (string)DraftResults.SelectedRows[0].Cells["Team"].Value;
				UpdateDraftBoard(model.TeamModel.GetTeamIdFromTeamName((string)DraftResults.SelectedRows[0].Cells["Team"].Value));
			}
			else if ((short)DraftResults.SelectedRows[0].Cells["Pick"].Value <= CurrentPick && stickyDepthCharts.Checked)
			{
				depthChartTeam.SelectedItem = (string)DraftResults.SelectedRows[0].Cells["Team"].Value;
				depthChartPosition.SelectedItem = (string)DraftResults.SelectedRows[0].Cells["Position"].Value;

				try
				{
					UpdateDepthChart(model.TeamModel.GetTeamIdFromTeamName((string)DraftResults.SelectedRows[0].Cells["Team"].Value), (int)Enum.Parse(typeof(MaddenPositions), (string)DraftResults.SelectedRows[0].Cells["Position"].Value, true));
				}
				catch
				{

				}
			}
		}

		private void DraftBoardGrid_DoubleClick(object sender, EventArgs e)
		{
			if (DraftBoardGrid.SelectedRows.Count <= 0)
			{
				return;
			}

			if (stickyDepthCharts.Checked)
			{
				depthChartTeam.SelectedItem = (string)draftBoardTeam.SelectedItem;
				depthChartPosition.SelectedItem = (string)DraftBoardGrid.SelectedRows[0].Cells["Position"].Value;
				UpdateDepthChart(model.TeamModel.GetTeamIdFromTeamName((string)draftBoardTeam.SelectedItem), (int)Enum.Parse(typeof(MaddenPositions), (string)DraftBoardGrid.SelectedRows[0].Cells["Position"].Value, true));
			}
		}

		private void RookieGrid_Click(object sender, EventArgs e)
		{
			if (RookieGrid.SelectedRows.Count <= 0)
			{
				return;
			}

			if (stickyDepthCharts.Checked)
			{
				string draftedby;
				try { draftedby = (string)RookieGrid.SelectedRows[0].Cells["Drafted By"].Value; }
				catch { draftedby = ""; }

				if (draftedby.Length > 0)
				{
					draftedby = draftedby.Split(' ')[0];

					depthChartTeam.SelectedItem = draftedby;
					depthChartPosition.SelectedItem = (string)RookieGrid.SelectedRows[0].Cells["Position"].Value;
					UpdateDepthChart(model.TeamModel.GetTeamIdFromTeamName(draftedby), (int)Enum.Parse(typeof(MaddenPositions), (string)RookieGrid.SelectedRows[0].Cells["Position"].Value, true));
				}
				else
				{
					depthChartTeam.SelectedItem = model.TeamModel.GetTeamNameFromTeamId(HumanTeamId);
					depthChartPosition.SelectedItem = (string)RookieGrid.SelectedRows[0].Cells["Position"].Value;
					UpdateDepthChart(HumanTeamId, (int)Enum.Parse(typeof(MaddenPositions), (string)RookieGrid.SelectedRows[0].Cells["Position"].Value, true));
				}
			}
		}

		private void RookieGrid_DoubleClick(object sender, EventArgs e)
		{
			if (dm.rookies[(int)((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"]].DraftedTeam < 32)
			{
				return;
			}

			foreach (DataRow drd in wishlistData.Rows)
			{
				if ((short)drd["PGID"] == (int)((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"])
				{
					return;
				}
			}

			DataRow dr = wishlistData.NewRow();

			dr["PGID"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"];
			dr["Rank"] = wishlistGrid.RowCount + 1;
			dr["Pos"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Position"];
			dr["ourgrade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["myproj"];
			dr["Grade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Our Grade"];
			dr["Player"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Player"];

			wishlistData.Rows.Add(dr);

			if (CurrentSelectingId == HumanTeamId)
			{
				SelectedPlayer = (short)dr["PGID"];
				PlayerToDraft.Text = dm.GetRookies(-1)[SelectedPlayer].Player.ToString();
			}
		}

        private void removeWishlistRow(DataRow toRemove)
        {
            DataTable newDataTable = wishlistData.Copy();
            newDataTable.Clear();

            foreach (DataRow dr in wishlistData.Rows)
            {
                if (dr != toRemove)
                {
                    DataRow newRow = newDataTable.NewRow();
                    dr.ItemArray.CopyTo(newRow.ItemArray, 0);
                    newDataTable.Rows.Add(newRow);
                }
            }

            wishlistBinding.DataSource = newDataTable;
        }

		private void rerankBoard()
		{
			int i = 1;
			foreach (DataGridViewRow row in wishlistGrid.Rows)
			{
				((DataRowView)row.DataBoundItem).Row["Rank"] = i;
				i++;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (wishlistGrid.SelectedRows.Count > 0)
			{
				wishlistData.Rows.Remove(((DataRowView)wishlistGrid.SelectedRows[0].DataBoundItem).Row);
			}

			rerankBoard();
		}

		private DataRow wishlistRow(int PGID)
		{
			foreach (DataRow dr in wishlistData.Rows)
			{
				if ((short)dr["PGID"] == PGID)
				{
					return dr;
				}
			}
			return null;
		}

		private void downButton_Click(object sender, EventArgs e)
		{
			if (wishlistGrid.SelectedRows.Count > 0)
			{
				int index = wishlistGrid.SelectedRows[0].Index;

				if (index + 1 >= wishlistGrid.RowCount) { return; }

				int currentid = (short)((DataRowView)wishlistGrid.SelectedRows[0].DataBoundItem).Row["PGID"];
				int lowerid = (short)((DataRowView)wishlistGrid.Rows[index + 1].DataBoundItem).Row["PGID"];

				wishlistRow(currentid)["Rank"] = index + 2;
				wishlistRow(lowerid)["Rank"] = index + 1;
			}

			wishlistGrid.Sort(wishlistGrid.Columns[1], ListSortDirection.Ascending);
		}

		private void upButton_Click(object sender, EventArgs e)
		{
			if (wishlistGrid.SelectedRows.Count > 0)
			{
				int index = wishlistGrid.SelectedRows[0].Index;

				if (index == 0) { return; }

				int currentid = (short)((DataRowView)wishlistGrid.SelectedRows[0].DataBoundItem).Row["PGID"];
				int higherid = (short)((DataRowView)wishlistGrid.Rows[index - 1].DataBoundItem).Row["PGID"];

				wishlistRow(currentid)["Rank"] = index;
				wishlistRow(higherid)["Rank"] = index + 1;
			}

			wishlistGrid.Sort(wishlistGrid.Columns[1], ListSortDirection.Ascending);
		}

		private void wishlistGrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (wishlistGrid.SelectedRows.Count <= 0)
			{
				return;
			}

			if (stickyDepthCharts.Checked)
			{
				depthChartTeam.SelectedItem = (string)model.TeamModel.GetTeamNameFromTeamId(HumanTeamId);
				depthChartPosition.SelectedItem = (string)wishlistGrid.SelectedRows[0].Cells["Pos"].Value;
				UpdateDepthChart(HumanTeamId, (int)Enum.Parse(typeof(MaddenPositions), (string)depthChartPosition.SelectedItem, true));
			}
		}

		private void wishlistGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (CurrentSelectingId == HumanTeamId)
			{
				SelectedPlayer = (short)wishlistGrid["PGID", e.RowIndex].Value;
				PlayerToDraft.Text = dm.GetRookies(-1)[SelectedPlayer].Player.ToString();
			}
		}

		private void DraftForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            autoPickBackgroundWorker.CancelAsync();
			if (!done)
			{
				MessageBox.Show("You must reopen this file if you want to restart the draft.\n\nYou should NOT save this file before reopening.");
			}
		}

		private void DraftResults_Scroll(object sender, ScrollEventArgs e)
		{

		}
	}
}