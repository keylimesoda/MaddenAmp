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

        DataTable depthChartData = new DataTable();
        BindingSource depthChartBinding = new BindingSource();

        DataTable wishlistData = new DataTable();
        BindingSource wishlistBinding = new BindingSource();

		EditorModel model;
		DraftModel dm;
		int HumanTeamId;
		int stage = 0;
		int SecondsPerPick;
		int totalHours;

		bool sortDirection = true;
		int previousSortedColumn = -1;

		LocalMath math;
        Dictionary<string, int> projections = new Dictionary<string,int>();

		public ScoutingForm(EditorModel em, int htId, int seconds, DraftModel dm, bool customClass)
		{
			this.dm = dm;
			math = new LocalMath(em.FileVersion);
			SecondsPerPick = seconds;
			HumanTeamId = htId;
			model = em;
			InitializeComponent();
			stage = 0;

            projections["Top 5"] = 5;
            projections["Top 10"] = 10;
            projections["1st"] = 28;
            projections["1st-2nd"] = 36;
            projections["2nd"] = 60;
            projections["2nd-3rd"] = 68;
            projections["3rd"] = 92;
            projections["3rd-4th"] = 100;
            projections["4th"] = 128;
            projections["5th"] = 160;
            projections["6th"] = 192;
            projections["7th"] = 224;
            projections["Undrafted"] = 1000;

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

            /*
			setHours.Items.Add(0);
			setHours.Items.Add(1);
			setHours.Items.Add(2);
			setHours.Items.Add(3);
			setHours.Items.Add(4);
			setHours.Items.Add(5);
			setHours.SelectedItem = 0;
            */
            
			incrementHours.Items.Add("5");
			incrementHours.Items.Add("4");
			incrementHours.Items.Add("3");
			incrementHours.Items.Add("2");
			incrementHours.Items.Add("1");
			incrementHours.Items.Add("0");
			incrementHours.Items.Add("-1");
			incrementHours.Items.Add("-2");
			incrementHours.Items.Add("-3");
			incrementHours.Items.Add("-4");
			incrementHours.Items.Add("-5");
			incrementHours.SelectedIndex = 0;

			RookiePositionFilter.Items.Add("All");
//			setPosition.Items.Add("All");
			incrementPosition.Items.Add("All");

			for (int i = 0; i < 21; i++)
			{
				RookiePositionFilter.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
//				setPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
				incrementPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
                depthChartPosition.Items.Add(Enum.GetNames(typeof(MaddenPositions))[i].ToString());
            }

            incrementPosition.Items.Add("");
            RookiePositionFilter.Items.Add("");
            foreach (string s in Enum.GetNames(typeof(MaddenPositionGroups)))
            {
                RookiePositionFilter.Items.Add(s);
                incrementPosition.Items.Add(s);
            }

            incrementPosition.SelectedIndex = 0;
            SetIncrement.SelectedIndex = 0;
            projectionFilter.SelectedIndex = 0;
            projectionCondition.SelectedIndex = 0;

            projectionLevel.Items.Add("Any");
            foreach (string s in projections.Keys)
                projectionLevel.Items.Add(s);
            projectionLevel.SelectedIndex = 0;
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
            rookieData.Columns.Add(AddColumn("group", "System.String"));
            rookieData.Columns.Add(AddColumn("subgroup", "System.String"));
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

                dr["group"] = "";
                dr["subgroup"] = "";

                switch (rook.Value.Player.PositionId)
                {
                    case (int)MaddenPositions.LT:
                    case (int)MaddenPositions.RT:
                        dr["group"] = "OL";
                        dr["subgroup"] = "OT";
                        break;
                    case (int)MaddenPositions.LG:
                    case (int)MaddenPositions.RG:
                        dr["group"] = "OL";
                        dr["subgroup"] = "OG";
                        break;
                    case (int)MaddenPositions.LE:
                    case (int)MaddenPositions.RE:
                        dr["group"] = "DL";
                        dr["subgroup"] = "DE";
                        break;
                    case (int)MaddenPositions.DT:
                        dr["group"] = "DL";
                        break;
                    case (int)MaddenPositions.LOLB:
                    case (int)MaddenPositions.ROLB:
                        dr["group"] = "LB";
                        dr["subgroup"] = "OLB";
                        break;
                    case (int)MaddenPositions.MLB:
                        dr["group"] = "LB";
                        break;
                    case (int)MaddenPositions.CB:
                        dr["group"] = "DB";
                        break;
                    case (int)MaddenPositions.FS:
                    case (int)MaddenPositions.SS:
                        dr["group"] = "DB";
                        dr["subgroup"] = "S";
                        break;
                }

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
            
            if (RookieGrid.Rows.Count > 0)
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
                // no idea why 23 is correct here -- it seems like the "Doctor" column should
                // behave as same as the skills, but this is what works.
				if ((sortDirection && !((previousSortedColumn + 1) != columnindex && columnindex > 23)) || ((previousSortedColumn + 1) != columnindex && columnindex < 23))
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

            if (RookieGrid.Rows.Count > 0)
    			RookieGrid.CurrentCell = RookieGrid[1, 0];
		}

		private void RookiePositionFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			rookieBinding.RemoveFilter();

            if (RookiePositionFilter.SelectedIndex > 0 && RookiePositionFilter.SelectedIndex != 22)
			{
                if (RookiePositionFilter.SelectedIndex < 22)
                    rookieBinding.Filter = "Position='" + RookiePositionFilter.SelectedItem + "'";
                else
                {
                    int group = RookiePositionFilter.SelectedIndex - 23;

                    switch (group)
                    {
                        case (int)MaddenPositionGroups.DB:
                            rookieBinding.Filter = "group='DB'";
                            break;
                        case (int)MaddenPositionGroups.LB:
                            rookieBinding.Filter = "group='LB'";
                            break;
                        case (int)MaddenPositionGroups.DL:
                            rookieBinding.Filter = "group='DL'";
                            break;
                        case (int)MaddenPositionGroups.OL:
                            rookieBinding.Filter = "group='OL'";
                            break;
                        case (int)MaddenPositionGroups.OT:
                            rookieBinding.Filter = "subgroup='OT'";
                            break;
                        case (int)MaddenPositionGroups.OG:
                            rookieBinding.Filter = "subgroup='OG'";
                            break;
                        case (int)MaddenPositionGroups.DE:
                            rookieBinding.Filter = "subgroup='DE'";
                            break;
                        case (int)MaddenPositionGroups.OLB:
                            rookieBinding.Filter = "subgroup='OLB'";
                            break;
                        case (int)MaddenPositionGroups.S:
                            rookieBinding.Filter = "subgroup='S'";
                            break;
                    }
                }
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
            if (incrementPosition.SelectedIndex == 22 || 
                (SetIncrement.SelectedIndex == 0 && incrementHours.SelectedIndex > 5) ||
                (projectionFilter.SelectedIndex > stage+1 && projectionLevel.SelectedIndex != 0))
                return;

			foreach (RookieRecord rook in dm.GetRookies(-1).Values)
			{
                if (!(incrementPosition.SelectedIndex == 0 ||
                    (incrementPosition.SelectedIndex-1) == rook.Player.PositionId ||
                    math.InPositionGroup(rook.Player.PositionId, incrementPosition.SelectedIndex-23)))
                    continue;

                if (projectionLevel.SelectedIndex != 0 && 
                    (projectionFilter.SelectedIndex == 0 || projectionFilter.SelectedIndex <= stage+1) &&
                    !(projectionLevel.SelectedIndex == 1 && projectionCondition.SelectedIndex == 2) &&
                    !(projectionLevel.SelectedIndex == projectionLevel.Items.Count-1 && projectionCondition.SelectedIndex == 0))
                {
                    if (projectionFilter.SelectedIndex == 0)
                    {

                    }
                    else
                    {
                        int proj;

                        if (projectionFilter.SelectedIndex == 1)
                            proj = rook.EstimatedPickNumber[(int)RookieRecord.RatingType.Initial];
                        else if (projectionFilter.SelectedIndex == 2)
                            proj = rook.EstimatedPickNumber[(int)RookieRecord.RatingType.Combine];
                        else
                            proj = rook.EstimatedPickNumber[(int)RookieRecord.RatingType.Final];

                        if (projectionCondition.SelectedIndex == 0)
                        {
                            if (proj >= projections[(string)projectionLevel.Items[projectionLevel.SelectedIndex]])
                                continue;
                        }
                        else if (projectionCondition.SelectedIndex == 1)
                        {
                            if (!((projectionLevel.SelectedIndex == 1 || 
                                proj >= projections[(string)projectionLevel.Items[projectionLevel.SelectedIndex - 1]]) &&
                                proj <  projections[(string)projectionLevel.Items[projectionLevel.SelectedIndex]]))
                                continue;
                        }
                        else
                        {
                            if (proj <  projections[(string)projectionLevel.Items[projectionLevel.SelectedIndex - 1]])
                                continue;
                        }
                    }
                }

                if (SetIncrement.SelectedIndex == 0)
                {
                    if (stage == 0)
                    {
                        rook.PreCombineScoutedHours[HumanTeamId] = 5 - (int)incrementHours.SelectedIndex;
                    }
                    else if (stage == 1)
                    {
                        rook.PostCombineScoutedHours[HumanTeamId] = 5 - (int)incrementHours.SelectedIndex;
                    }
                }
                else
                {
                    if (stage == 0)
                    {
                        rook.PreCombineScoutedHours[HumanTeamId] = rook.PreCombineScoutedHours[HumanTeamId] + (5 - incrementHours.SelectedIndex);

                        if (rook.PreCombineScoutedHours[HumanTeamId] > 5)
                        {
                            rook.PreCombineScoutedHours[HumanTeamId] = 5;
                        }
                        else if (rook.PreCombineScoutedHours[HumanTeamId] < 0)
                        {
                            rook.PreCombineScoutedHours[HumanTeamId] = 0;
                        }
                    }
                    else if (stage == 1)
                    {
                        rook.PostCombineScoutedHours[HumanTeamId] = rook.PostCombineScoutedHours[HumanTeamId] + (5 - incrementHours.SelectedIndex);

                        if (rook.PreCombineScoutedHours[HumanTeamId] > 5)
                        {
                            rook.PreCombineScoutedHours[HumanTeamId] = 5;
                        }
                        else if (rook.PreCombineScoutedHours[HumanTeamId] < 0)
                        {
                            rook.PreCombineScoutedHours[HumanTeamId] = 0;
                        }
                    }
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
                    List<int> initialWishlist = new List<int>();

                    foreach (DataRow drd in wishlistData.Rows)
                        initialWishlist.Add((int)(short)(drd["PGID"]));

					Cursor.Current = Cursors.WaitCursor;
					DraftForm form = new DraftForm(model, dm, HumanTeamId, SecondsPerPick, initialWishlist);
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

                    foreach (DataRow drd in wishlistData.Rows)
                    {
                        drd["ourgrade"] = dm.rookies[(int)(short)drd["PGID"]].CombineNumbers[(int)CombineStat.RoundGrade];
                        drd["Grade"] = dm.rookies[(int)(short)drd["PGID"]].CombineWords[(int)CombineStat.RoundGrade];
                    }

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

                    foreach (DataRow drd in wishlistData.Rows)
                    {
                        drd["ourgrade"] = dm.rookies[(int)(short)drd["PGID"]].CombineNumbers[(int)CombineStat.RoundGrade];
                        drd["Grade"] = dm.rookies[(int)(short)drd["PGID"]].CombineWords[(int)CombineStat.RoundGrade];
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

        private void depthChartFilterChanged(object sender, EventArgs e)
        {
            UpdateDepthChart(depthChartPosition.SelectedIndex);
        }

        private void UpdateDepthChart(int PositionId)
        {
            if (PositionId >= 0 && PositionId < 21)
            {
                List<PlayerRecord> dc = dm.GetDepthChart(HumanTeamId, PositionId);
                depthChartData.Clear();

                int depth = 1;
                foreach (PlayerRecord player in dc)
                {
                    DataRow dr = depthChartData.NewRow();
                    dr["Player"] = player.FirstName + " " + player.LastName;

                    dr["Depth"] = depth;
                    dr["OVR"] = dm.dcr.GetAdjustedOverall(player, PositionId);
                    dr["AGE"] = player.Age;
                    dr["INJ"] = player.Injury;
                    depthChartData.Rows.Add(dr);
                    depth++;
               }

                DepthChartGrid.Sort(DepthChartGrid.Columns["Depth"], ListSortDirection.Ascending);
            }
        }

        private void RookieGrid_DoubleClick(object sender, EventArgs e)
        {
            if (dm.rookies[(int)((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"]].DraftedTeam < 32)
                return;

            foreach (DataRow drd in wishlistData.Rows)
            {
                if ((short)drd["PGID"] == (int)((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"])
                    return;
            }

            DataRow dr = wishlistData.NewRow();

            dr["PGID"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["PGID"];
            dr["Rank"] = wishlistGrid.RowCount + 1;
            dr["Pos"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Position"];

            if (stage == 0)
            {
                dr["ourgrade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["initproj"];
                dr["Grade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Init. Proj."];
            }
            else
            {
                dr["ourgrade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["ourgrade"];
                dr["Grade"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Our Grade"];
            }
            
            dr["Player"] = ((DataRowView)RookieGrid.SelectedRows[0].DataBoundItem).Row["Player"];

            wishlistData.Rows.Add(dr);
        }

        private void RookieGrid_Click(object sender, EventArgs e)
        {
            if (RookieGrid.SelectedRows.Count <= 0 || stickyDepthCharts.Checked == false)
            {
                return;
            }

            string pos = (string)RookieGrid.SelectedRows[0].Cells["Position"].Value;

            if (pos != null)
            {
                depthChartPosition.SelectedItem = pos;
                UpdateDepthChart((int)Enum.Parse(typeof(MaddenPositions), (string)pos, true));
            }
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
            DataGridViewColumn dgvc = wishlistGrid.SortedColumn;
            ListSortDirection lsd = ListSortDirection.Ascending;
            if (wishlistGrid.SortOrder == SortOrder.Ascending)
                lsd = ListSortDirection.Ascending;
            else if (wishlistGrid.SortOrder == SortOrder.Descending)
                lsd = ListSortDirection.Descending;
            else
                dgvc = null;

            if (wishlistGrid.SelectedRows.Count > 0)
            {
                int index = wishlistGrid.SelectedRows[0].Index;
                wishlistGrid.Sort(wishlistGrid.Columns["Rank"], ListSortDirection.Ascending);

                if (index + 1 >= wishlistGrid.RowCount) { return; }

                int currentid = (short)((DataRowView)wishlistGrid.Rows[index].DataBoundItem).Row["PGID"];
                int lowerid = (short)((DataRowView)wishlistGrid.Rows[index + 1].DataBoundItem).Row["PGID"];

                wishlistRow(currentid)["Rank"] = index + 2;
                wishlistRow(lowerid)["Rank"] = index + 1;
            }

            if (dgvc != null)
                wishlistGrid.Sort(dgvc, lsd);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            wishlistGrid.Sort(wishlistGrid.Columns["Rank"], ListSortDirection.Ascending);

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

        private void remove_Click(object sender, EventArgs e)
        {
            wishlistGrid.Sort(wishlistGrid.Columns["Rank"], ListSortDirection.Ascending);
            if (wishlistGrid.SelectedRows.Count > 0)
            {
                wishlistData.Rows.Remove(((DataRowView)wishlistGrid.SelectedRows[0].DataBoundItem).Row);
            }

            rerankBoard();
        }

        private void wishlistFixSort(object sender, EventArgs e)
        {
            /*
            if (preventSortLoop)
            {
                return;
            }
            preventSortLoop = true;
            */

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
//          preventSortLoop = false;
        }

        private void ScoutingForm_Load(object sender, EventArgs e)
		{
			stage = 0;

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

            
            
            RookieGrid.Columns["initproj"].Visible = false;
			RookieGrid.Columns["currproj"].Visible = false;
			RookieGrid.Columns["ourgrade"].Visible = false;
			RookieGrid.Columns["PGID"].Visible = false;
			RookieGrid.Columns["heightnumber"].Visible = false;
			RookieGrid.Columns["doctornumber"].Visible = false;
			RookieGrid.Columns["primaryskill"].Visible = false;
			RookieGrid.Columns["secondaryskill"].Visible = false;
            RookieGrid.Columns["group"].Visible = false;
            RookieGrid.Columns["subgroup"].Visible = false;

			RookieGrid.Columns["Player"].Width = 100;
			RookieGrid.Columns["Position"].Width = 47;
			//            RookieGrid.Columns["Actual"].Width = 54;
			RookieGrid.Columns["Init. Proj."].Width = 52;
			RookieGrid.Columns["Curr. Proj."].Width = 56;
			RookieGrid.Columns["Our Grade"].Width = 58;
			RookieGrid.Columns["Age"].Width = 28;
			RookieGrid.Columns["Height"].Width = 41;
			RookieGrid.Columns["Weight"].Width = 44;
			RookieGrid.Columns["40 Time"].Width = 52;
			RookieGrid.Columns["Shuttle"].Width = 42;
			RookieGrid.Columns["Cone"].Width = 34;
			RookieGrid.Columns["Bench"].Width = 40;
			RookieGrid.Columns["Vertical"].Width = 45;
			RookieGrid.Columns["Wonderlic"].Width = 58;
			RookieGrid.Columns["Doctor"].Width = 42;
			RookieGrid.Columns["1st Skill"].Width = 60;
			RookieGrid.Columns["2nd Skill"].Width = 60;
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