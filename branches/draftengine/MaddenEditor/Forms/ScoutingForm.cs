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
	public partial class ScoutingForm : Form
	{
		DataTable rookieData = new DataTable();
		BindingSource rookieBinding = new BindingSource();

		EditorModel model;
		DraftModel dm;
		int HumanTeamId;
		int stage = 0;
		int SecondsPerPick;
		int totalHours;

		bool sortDirection = true;
		int previousSortedColumn = -1;

		LocalMath math;

		public ScoutingForm(EditorModel em, int htId, int seconds, DraftModel dm, bool customClass)
		{
			this.dm = dm;
			math = new LocalMath(em.FileVersion);
			SecondsPerPick = seconds;
			HumanTeamId = htId;
			model = em;
			InitializeComponent();
			stage = 0;

			Initialise();
		}

		private void InitializeComboBoxes()
		{
			scoutingHours.ValueType = System.Type.GetType("System.Int32");
			scoutingHours.Items.Add(0);
			scoutingHours.Items.Add(1);
			scoutingHours.Items.Add(2);
			scoutingHours.Items.Add(3);
			scoutingHours.Items.Add(4);
			scoutingHours.Items.Add(5);

			for (int i = 0; i < RookieGrid.Rows.Count; i++)
			{
				RookieGrid.Rows[i].Cells["scoutingHours"].Value = 0;
			}

			setHours.Items.Add(0);
			setHours.Items.Add(1);
			setHours.Items.Add(2);
			setHours.Items.Add(3);
			setHours.Items.Add(4);
			setHours.Items.Add(5);
			setHours.SelectedItem = 0;

			incrementHours.Items.Add("+5");
			incrementHours.Items.Add("+4");
			incrementHours.Items.Add("+3");
			incrementHours.Items.Add("+2");
			incrementHours.Items.Add("+1");
			incrementHours.Items.Add("0");
			incrementHours.Items.Add("-1");
			incrementHours.Items.Add("-2");
			incrementHours.Items.Add("-3");
			incrementHours.Items.Add("-4");
			incrementHours.Items.Add("-5");
			incrementHours.SelectedItem = 0;

			RookiePositionFilter.Items.Add("All");
			setPosition.Items.Add("All");
			incrementPosition.Items.Add("All");

			for (int i = 0; i < 21; i++)
			{
				RookiePositionFilter.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
				setPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
				incrementPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
			}

		}

		private void FillHourData()
		{
			for (int i = 0; i < RookieGrid.Rows.Count; i++)
			{
				if (stage == 0)
				{
					RookieGrid.Rows[i].Cells["scoutingHours"].Value = (int)dm.GetRookies(-1)[(int)RookieGrid.Rows[i].Cells["PGID"].Value].PreCombineScoutedHours[HumanTeamId];
				}
				else if (stage == 1)
				{
					RookieGrid.Rows[i].Cells["scoutingHours"].Value = (int)dm.GetRookies(-1)[(int)RookieGrid.Rows[i].Cells["PGID"].Value].PostCombineScoutedHours[HumanTeamId];
				}
			}
		}

		private void Initialise()
		{
			rookieData.Columns.Add(AddColumn("PGID", "System.Int32"));
			rookieData.Columns.Add(AddColumn("Player", "System.String"));
			rookieData.Columns.Add(AddColumn("Position", "System.String"));
			/*            rookieData.Columns.Add(AddColumn("actualproj", "System.Int16"));
						rookieData.Columns.Add(AddColumn("Actual", "System.String"));*/
			rookieData.Columns.Add(AddColumn("initproj", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Init. Proj.", "System.String"));
			rookieData.Columns.Add(AddColumn("currproj", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Curr. Proj.", "System.String"));
			rookieData.Columns.Add(AddColumn("ourgrade", "System.Int16"));
			rookieData.Columns.Add(AddColumn("Our Grade", "System.String"));
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
			rookieData.Columns.Add(AddColumn("Hours", "System.Int16"));

			rookieBinding.DataSource = rookieData;

			RookieGrid.DataSource = rookieBinding;
		}

		private void RefreshAllocations()
		{
			int QBhours = 0;
			int RBhours = 0;
			int WRhours = 0;
			int TEhours = 0;
			int OLhours = 0;
			int DLhours = 0;
			int LBhours = 0;
			int CBhours = 0;
			int Shours = 0;
			int Khours = 0;
			int Phours = 0;

			if (stage == 0)
			{
				foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
				{
					switch (rook.Value.Player.PositionId)
					{
						case (int)MaddenPositions.QB:
							QBhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.HB:
						case (int)MaddenPositions.FB:
							RBhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.WR:
							WRhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.TE:
							TEhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LT:
						case (int)MaddenPositions.RT:
						case (int)MaddenPositions.C:
						case (int)MaddenPositions.LG:
						case (int)MaddenPositions.RG:
							OLhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LE:
						case (int)MaddenPositions.DT:
						case (int)MaddenPositions.RE:
							DLhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LOLB:
						case (int)MaddenPositions.MLB:
						case (int)MaddenPositions.ROLB:
							LBhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.CB:
							CBhours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.FS:
						case (int)MaddenPositions.SS:
							Shours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.K:
							Khours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.P:
							Phours += (int)rook.Value.PreCombineScoutedHours[HumanTeamId];
							break;
					}
				}
			}
			else if (stage == 1)
			{
				foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
				{
					switch (rook.Value.Player.PositionId)
					{
						case (int)MaddenPositions.QB:
							QBhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.HB:
						case (int)MaddenPositions.FB:
							RBhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.WR:
							WRhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.TE:
							TEhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LT:
						case (int)MaddenPositions.RT:
						case (int)MaddenPositions.C:
						case (int)MaddenPositions.LG:
						case (int)MaddenPositions.RG:
							OLhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LE:
						case (int)MaddenPositions.DT:
						case (int)MaddenPositions.RE:
							DLhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.LOLB:
						case (int)MaddenPositions.MLB:
						case (int)MaddenPositions.ROLB:
							LBhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.CB:
							CBhours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.FS:
						case (int)MaddenPositions.SS:
							Shours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.K:
							Khours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
						case (int)MaddenPositions.P:
							Phours += (int)rook.Value.PostCombineScoutedHours[HumanTeamId];
							break;
					}
				}
			}

			QBhoursLabel.Text = QBhours + ",";
			RBhoursLabel.Text = RBhours + ",";
			WRhoursLabel.Text = WRhours + ",";
			TEhoursLabel.Text = TEhours + ",";
			OLhoursLabel.Text = OLhours + ",";
			DLhoursLabel.Text = DLhours + ",";
			LBhoursLabel.Text = LBhours + ",";
			CBhoursLabel.Text = CBhours + ",";
			ShoursLabel.Text = Shours + ",";
			KhoursLabel.Text = Khours + ",";
			PhoursLabel.Text = Phours.ToString();

			totalHours = QBhours + RBhours + WRhours + TEhours + OLhours + DLhours +
				LBhours + CBhours + Shours + Khours + Phours;

			TotalLabel.Text = totalHours + " / 500";
		}

		private void RefillRookieGrid()
		{

			rookieData.Clear();
			foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
			{
				DataRow dr = rookieData.NewRow();

				dr["PGID"] = rook.Key;
				dr["Player"] = rook.Value.Player.FirstName + " " + rook.Value.Player.LastName;
				dr["Position"] = Enum.GetNames(typeof(MaddenPositions))[rook.Value.Player.PositionId].ToString();

				/*                dr["actualproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Actual];
								dr["Actual"] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Actual];
				*/
				dr["initproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Initial];
				dr["Init. Proj."] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Initial];

				if (stage == 1)
				{
					dr["currproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Combine];
					dr["Curr. Proj."] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Combine];
				}
				else if (stage == 2)
				{
					dr["currproj"] = rook.Value.EstimatedPickNumber[(int)RookieRecord.RatingType.Combine];
					dr["Curr. Proj."] = rook.Value.EstimatedRound[(int)RookieRecord.RatingType.Combine];
				}

				if (stage > 0)
				{
					dr["ourgrade"] = rook.Value.CombineNumbers[(int)CombineStat.RoundGrade];
					dr["Our Grade"] = rook.Value.CombineWords[(int)CombineStat.RoundGrade];
				}

				dr["Age"] = rook.Value.Player.Age;
				dr["heightnumber"] = rook.Value.Player.Height;
				dr["Height"] = rook.Value.CombineWords[(int)CombineStat.Height];
				dr["Weight"] = rook.Value.Player.Weight + 160;

				if (stage > 0)
				{
					dr["40 Time"] = rook.Value.CombineNumbers[(int)CombineStat.Forty].ToString("N2");
					dr["Shuttle"] = rook.Value.CombineNumbers[(int)CombineStat.Shuttle].ToString("N2");
					dr["Cone"] = rook.Value.CombineNumbers[(int)CombineStat.Cone].ToString("N2");
					dr["Bench"] = rook.Value.CombineNumbers[(int)CombineStat.BenchPress];
					dr["Vertical"] = rook.Value.CombineWords[(int)CombineStat.Vertical];
					dr["Wonderlic"] = rook.Value.CombineNumbers[(int)CombineStat.Wonderlic];
					dr["doctornumber"] = rook.Value.CombineNumbers[(int)CombineStat.Doctor];
					dr["Doctor"] = rook.Value.CombineWords[(int)CombineStat.Doctor];
				}

				switch (stage)
				{
					case 0:
						dr["primaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Initial);
						dr["secondaryskill"] = rook.Value.SecondarySkill(HumanTeamId, (int)RookieRecord.RatingType.Initial);
						break;
					case 1:
						dr["primaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Combine);
						dr["secondaryskill"] = rook.Value.SecondarySkill(HumanTeamId, (int)RookieRecord.RatingType.Combine);
						break;
					case 2:
						dr["primaryskill"] = rook.Value.PrimarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
						dr["secondaryskill"] = rook.Value.SecondarySkill(HumanTeamId, (int)RookieRecord.RatingType.Final);
						break;
				}

				dr["1st Skill"] = math.SkillToGrade((double)dr["primaryskill"]);
				dr["2nd Skill"] = math.SkillToGrade((double)dr["secondaryskill"]);

				if (stage == 1)
				{
					dr["Hours"] = rook.Value.PreCombineScoutedHours[HumanTeamId];
				}
				else if (stage == 2)
				{
					dr["Hours"] = rook.Value.PreCombineScoutedHours[HumanTeamId] + rook.Value.PostCombineScoutedHours[HumanTeamId];
				}

				rookieData.Rows.Add(dr);
			}

			if (stage == 0)
			{
				RookieGrid.Sort(RookieGrid.Columns["initproj"], ListSortDirection.Ascending);
			}
			else
			{
				RookieGrid.Sort(RookieGrid.Columns["ourgrade"], ListSortDirection.Ascending);
			}

			RookieGrid.CurrentCell = RookieGrid[1, 0];
		}

		private DataColumn AddColumn(string ColName, string ColType)
		{
			DataColumn dc = new DataColumn();
			dc.ColumnName = ColName;
			dc.DataType = System.Type.GetType(ColType);
			return dc;
		}

		private void RookieGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if ((int)RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value > 5)
			{
				RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value = 5;
			}
			else if ((int)RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value < 0)
			{
				RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value = 0;
			}

			//RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value = Math.Round((double)RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value);

			if (stage == 0)
			{
				dm.GetRookies(-1)[(int)RookieGrid.Rows[e.RowIndex].Cells["PGID"].Value].PreCombineScoutedHours[HumanTeamId] =
					(int)RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value;
			}
			else if (stage == 1)
			{
				dm.GetRookies(-1)[(int)RookieGrid.Rows[e.RowIndex].Cells["PGID"].Value].PostCombineScoutedHours[HumanTeamId] =
					(int)RookieGrid.Rows[e.RowIndex].Cells["scoutingHours"].Value;
			}

			RefreshAllocations();
		}

		private void RookieGrid_Sorted(object sender, EventArgs e)
		{
			string column = RookieGrid.SortedColumn.Name;
			int columnindex = RookieGrid.SortedColumn.Index;

			if (column.Equals("Init. Proj.") || column.Equals("Curr. Proj.") || column.Equals("Doctor")
				|| column.Equals("Height") || column.Equals("Actual") || column.Equals("Our Grade") || column.Equals("1st Skill") || column.Equals("2nd Skill"))
			{
				if ((sortDirection && !((previousSortedColumn + 1) != columnindex && columnindex > 22)) || ((previousSortedColumn + 1) != columnindex && columnindex < 22))
				{
					RookieGrid.Sort(RookieGrid.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Ascending);
					sortDirection = false;
				}
				else
				{
					RookieGrid.Sort(RookieGrid.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Descending);
					sortDirection = true;
				}

				return;
			}

			previousSortedColumn = columnindex;
			/*
				{

							if (sortDirection)
							{
								RookieGrid.Sort(RookieGrid.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Ascending);
								sortDirection = false;
							}
							else
							{
								RookieGrid.Sort(RookieGrid.Columns[columnindex - 1], System.ComponentModel.ListSortDirection.Descending);
								sortDirection = true;
							}
						}
			*/
			FillHourData();
			RookieGrid.CurrentCell = RookieGrid[1, 0];
		}

		private void RookiePositionFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			rookieBinding.RemoveFilter();
			if (!(RookiePositionFilter.SelectedItem.Equals("All")))
			{
				rookieBinding.Filter = "Position='" + RookiePositionFilter.SelectedItem + "'";
			}
			FillHourData();
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

		private void doSet_Click(object sender, EventArgs e)
		{
			foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(setPosition.SelectedIndex - 1))
			{
				if (stage == 0)
				{
					rook.Value.PreCombineScoutedHours[HumanTeamId] = (int)setHours.SelectedItem;
				}
				else if (stage == 1)
				{
					rook.Value.PostCombineScoutedHours[HumanTeamId] = (int)setHours.SelectedItem;
				}
			}

			FillHourData();
			RefreshAllocations();
		}

		private void doIncrement_Click(object sender, EventArgs e)
		{
			foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(incrementPosition.SelectedIndex - 1))
			{
				if (stage == 0)
				{
					rook.Value.PreCombineScoutedHours[HumanTeamId] = rook.Value.PreCombineScoutedHours[HumanTeamId] + (5 - incrementHours.SelectedIndex);

					if (rook.Value.PreCombineScoutedHours[HumanTeamId] > 5)
					{
						rook.Value.PreCombineScoutedHours[HumanTeamId] = 5;
					}
					else if (rook.Value.PreCombineScoutedHours[HumanTeamId] < 0)
					{
						rook.Value.PreCombineScoutedHours[HumanTeamId] = 0;
					}
				}
				else if (stage == 1)
				{
					rook.Value.PostCombineScoutedHours[HumanTeamId] = rook.Value.PostCombineScoutedHours[HumanTeamId] + (5 - incrementHours.SelectedIndex);

					if (rook.Value.PreCombineScoutedHours[HumanTeamId] > 5)
					{
						rook.Value.PreCombineScoutedHours[HumanTeamId] = 5;
					}
					else if (rook.Value.PreCombineScoutedHours[HumanTeamId] < 0)
					{
						rook.Value.PreCombineScoutedHours[HumanTeamId] = 0;
					}
				}
			}

			FillHourData();
			RefreshAllocations();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (stage == 2)
			{
				DialogResult dr = MessageBox.Show("Start Draft?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

				if (dr == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					DraftForm form = new DraftForm(model, dm, HumanTeamId, SecondsPerPick);
					Cursor.Current = Cursors.Arrow;

					this.Close();
					form.Show();
				}

				return;
			}

			if (totalHours > 500)
			{
				MessageBox.Show("You are above your maximum of 500 scouting hours.\nPlease reduce your hours used and try again.", "Over Quota", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (totalHours < 500)
			{
				DialogResult dr = MessageBox.Show("You have not used up all of your 500 scouting hours available.\nAre you sure you want to continue?", "Scouting Hours Remaining", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

				if (dr == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.Invalidate(true);
					this.Update();

					if (stage == 0)
					{
						dm.DoCombine();
						foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
						{
							rook.Value.PostCombineScoutedHours[HumanTeamId] = rook.Value.PreCombineScoutedHours[HumanTeamId];
						}
					}
					else if (stage == 1)
					{
						dm.DoFinal();
						RookieGrid.Columns.Remove(scoutingHours);
						button1.Text = "Advance to Draft";
					}

					stage++;

					RefillRookieGrid();
					FillHourData();
					RefreshAllocations();

					Cursor.Current = Cursors.Arrow;
				}
			}
			else
			{
				DialogResult dr = MessageBox.Show("Continue to next stage of rookie scouting?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (dr == DialogResult.Yes)
				{
					Cursor.Current = Cursors.WaitCursor;
					this.Invalidate(true);
					this.Update();

					if (stage == 0)
					{
						dm.DoCombine();
						foreach (KeyValuePair<int, RookieRecord> rook in dm.GetRookies(-1))
						{
							rook.Value.PostCombineScoutedHours[HumanTeamId] = rook.Value.PreCombineScoutedHours[HumanTeamId];
						}

					}
					else if (stage == 1)
					{
						dm.DoFinal();
						button1.Text = "Advance to Draft";
					}

					stage++;

					RefillRookieGrid();
					FillHourData();
					RefreshAllocations();

					Cursor.Current = Cursors.Arrow;
				}
			}
		}

		private void scoutingHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string helpstring = "Scout players to get more precise estimates of their abilities.  You\n" +
				"have 500 hours per scouting period to distribute among the players, and two scouting periods.\n" +
				"The more you scout a player, the more accurate your attribute estimates will be.\n\n" +
				"You can set the number of hours for each player individually in the right-most column.  You can\n" +
				"also set or increment the number of hours for all players at a certain position (or even\n" +
				"all positions) using the controls at the bottom.\n\n" +
				"When you are finished with the current stage, click the button in the bottom right corner.";

			MessageBox.Show(helpstring, "Help");
		}

		private void ScoutingForm_Load(object sender, EventArgs e)
		{
			stage = 0;

			RookieGrid.Columns["initproj"].Visible = false;
			RookieGrid.Columns["currproj"].Visible = false;
			RookieGrid.Columns["ourgrade"].Visible = false;
			RookieGrid.Columns["PGID"].Visible = false;
			RookieGrid.Columns["heightnumber"].Visible = false;
			RookieGrid.Columns["doctornumber"].Visible = false;
			RookieGrid.Columns["primaryskill"].Visible = false;
			RookieGrid.Columns["secondaryskill"].Visible = false;

			RookieGrid.Columns["Player"].Width = 115;
			RookieGrid.Columns["Position"].Width = 45;
			//            RookieGrid.Columns["Actual"].Width = 54;
			RookieGrid.Columns["Init. Proj."].Width = 54;
			RookieGrid.Columns["Curr. Proj."].Width = 58;
			RookieGrid.Columns["Our Grade"].Width = 58;
			RookieGrid.Columns["Age"].Width = 25;
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
			RookieGrid.Columns["Hours"].Width = 40;
			RookieGrid.Columns.Add(scoutingHours);
			RookieGrid.Columns["Hours"].Resizable = DataGridViewTriState.False;
			RookieGrid.Columns["scoutingHours"].Resizable = DataGridViewTriState.False;

			RookieGrid.Columns["Player"].ReadOnly = true;
			RookieGrid.Columns["Position"].ReadOnly = true;
			RookieGrid.Columns["Init. Proj."].ReadOnly = true;
			RookieGrid.Columns["Curr. Proj."].ReadOnly = true;
			RookieGrid.Columns["Our Grade"].ReadOnly = true;
			RookieGrid.Columns["Height"].ReadOnly = true;
			RookieGrid.Columns["Weight"].ReadOnly = true;
			RookieGrid.Columns["40 Time"].ReadOnly = true;
			RookieGrid.Columns["Shuttle"].ReadOnly = true;
			RookieGrid.Columns["Cone"].ReadOnly = true;
			RookieGrid.Columns["Bench"].ReadOnly = true;
			RookieGrid.Columns["Vertical"].ReadOnly = true;
			RookieGrid.Columns["Wonderlic"].ReadOnly = true;
			RookieGrid.Columns["Doctor"].ReadOnly = true;
			RookieGrid.Columns["1st Skill"].ReadOnly = true;
			RookieGrid.Columns["2nd Skill"].ReadOnly = true;

			RookieGrid.RowHeadersVisible = false;
			RefillRookieGrid();

			InitializeComboBoxes();
		}
	}
}