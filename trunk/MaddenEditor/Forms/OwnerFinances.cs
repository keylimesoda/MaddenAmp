/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Db;
using MaddenEditor.Core.DatEditor;
using MaddenEditor.Core.Manager;

namespace MaddenEditor.Forms
{
    public partial class OwnerFinances : UserControl, IEditorForm
    {
        private EditorModel model = null;
        public OwnerRevenue currentowner = null;
        private bool isInitializing = true;
        public LeagueRevenue nflshared = null;
        List<Income> teamincome = new List<Income>();

        #region IEditorForm Members
        public void InitialiseUI()
        {
            isInitializing = true;
            foreach (TeamRecord team in model.TeamModel.GetTeams())
            {
                TeamComboBox.Items.Add(team);
            }

            GetOwner(0);
            TeamComboBox.SelectedItem = model.TeamModel.GetTeamRecord(currentowner.TeamID);
            nflshared = (LeagueRevenue)model.TableModels[EditorModel.LEAGUE_REVENUE_TABLE].GetRecord(0);
            LeagueRevenue_Updown.Value = nflshared.SharedRevenue;
            double total = (double)(LeagueRevenue_Updown.Value) * .05;
            LeagueRevenue_Textbox.Text = total.ToString() + " M";

            LoadFinances();
            InitWeeklyIncome();
            isInitializing = false;
        }
        public void CleanUI()
        {
            
        }
        public EditorModel Model
        {
            set { model = value; }
        }
        #endregion

        public OwnerFinances()
        {
            InitializeComponent();
        }

        public void GetOwner (int teamid)
        {
            foreach (OwnerRevenue rev in model.TableModels[EditorModel.OWNER_REVENUE_TABLE].GetRecords())
            {
                if (rev.TeamID == teamid)
                {
                    currentowner = rev;
                    break;
                }
            }
        }

        public void LoadFinances()
        {
            isInitializing = true;

            TVIncome_Updown.Value = currentowner.TvIncome;
            double tv = (double)currentowner.TvIncome * .05;
            TVIncome_Textbox.Text = tv.ToString() + " M";
            TVContract_Updown.Value = currentowner.TvContract;
            StadiumMaint_Updown.Value = currentowner.StadiumMaintenance;
            double stad = (double)currentowner.StadiumMaintenance * .05;
            StadiumMaint_Textbox.Text = stad.ToString() + " M";
            StadiumUpgrades_Updown.Value = currentowner.StadiumUpgrades;
            double upgrade = (double)currentowner.StadiumUpgrades * .05;
            StadiumUpgrades_Textbox.Text = upgrade.ToString() + " M";

            isInitializing = false;
        }

        public void InitWeeklyIncome()
        {
            isInitializing = true;

            DataGrid_WeeklyIncome.Rows.Clear();
            DataGrid_WeeklyIncome.Columns.Clear();
            DataGrid_WeeklyIncome.MultiSelect = false;
            DataGrid_WeeklyIncome.AutoGenerateColumns = false;
            DataGrid_WeeklyIncome.AllowUserToAddRows = false;
            DataGrid_WeeklyIncome.RowHeadersVisible = false;
            DataGrid_WeeklyIncome.ColumnCount = 6;

            DataGrid_WeeklyIncome.Columns[0].Name = "Week";
            DataGrid_WeeklyIncome.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[0].Width = 50;
            DataGrid_WeeklyIncome.Columns[1].Name = "Fan";
            DataGrid_WeeklyIncome.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[1].Width = 50;
            DataGrid_WeeklyIncome.Columns[2].Name = "Conc";
            DataGrid_WeeklyIncome.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[2].Width = 50;
            DataGrid_WeeklyIncome.Columns[3].Name = "Merc";
            DataGrid_WeeklyIncome.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[3].Width = 50;
            DataGrid_WeeklyIncome.Columns[4].Name = "Park";
            DataGrid_WeeklyIncome.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[4].Width = 50;
            DataGrid_WeeklyIncome.Columns[5].Name = "Tix";
            DataGrid_WeeklyIncome.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGrid_WeeklyIncome.Columns[5].Width = 50;

            int id = currentowner.TeamID;
            teamincome.Clear();

            foreach (Income inc in model.TableModels[EditorModel.WEEKLY_INCOME_TABLE].GetRecords())
            {
                if (inc.TeamID == id)
                    teamincome.Add(inc);
            }
            teamincome.Sort((x, y) => x.SeasonWeek.CompareTo(y.SeasonWeek));
            foreach (Income i in teamincome)
            {
                object[] entry = { i.SeasonWeek, i.Support, (double)i.Concessions * .05, (double)i.Merchandise * .05, (double)i.Parking * .05, (double)i.Tickets * .05 };
                DataGrid_WeeklyIncome.Rows.Add(entry);
            }

            List<double> totals = new List<double>();
            for (int c = 0; c < 4; c++)
                totals.Add(0);
            for (int r = 0; r < DataGrid_WeeklyIncome.Rows.Count; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    totals[c] += (double)DataGrid_WeeklyIncome.Rows[r].Cells[c + 2].Value;
                }
            }

            object[] tot = { "TOT", "TOT", totals[0], totals[1], totals[2], totals[3] };
            DataGrid_WeeklyIncome.Rows.Add(tot);

            isInitializing = false;
        }
        
        private void DataGrid_WeeklyIncome_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0 && e.RowIndex < DataGrid_WeeklyIncome.RowCount -1)
            {
                if (e.ColumnIndex == 1)
                {
                    teamincome[e.RowIndex].Support = Convert.ToInt32(Convert.ToDouble(DataGrid_WeeklyIncome.Rows[e.RowIndex].Cells[1].Value) / .05);
                }
                else if (e.ColumnIndex == 2)
                {
                    teamincome[e.RowIndex].Concessions = Convert.ToInt32(Convert.ToDouble(DataGrid_WeeklyIncome.Rows[e.RowIndex].Cells[2].Value) / .05);
                }
                else if (e.ColumnIndex == 3)
                {
                    teamincome[e.RowIndex].Merchandise = Convert.ToInt32(Convert.ToDouble(DataGrid_WeeklyIncome.Rows[e.RowIndex].Cells[3].Value) / .05);
                }
                else if (e.ColumnIndex == 4)
                {
                    teamincome[e.RowIndex].Parking = Convert.ToInt32(Convert.ToDouble(DataGrid_WeeklyIncome.Rows[e.RowIndex].Cells[4].Value) / .05);
                }
                else if (e.ColumnIndex == 5)
                {
                    teamincome[e.RowIndex].Tickets = Convert.ToInt32(Convert.ToDouble(DataGrid_WeeklyIncome.Rows[e.RowIndex].Cells[5].Value) / .05);
                }

                InitWeeklyIncome();
            }
        }

        private void TeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            isInitializing = true;
            TeamRecord team = (TeamRecord)TeamComboBox.SelectedItem;
            if (team.TeamId != currentowner.TeamID)
            {
                GetOwner(team.TeamId);
                LoadFinances();
                InitWeeklyIncome();
            }
            isInitializing = false;
        }

        private void LeagueRevenue_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                nflshared.LeagueIncome = (int)LeagueRevenue_Updown.Value;
                double total = (double)(LeagueRevenue_Updown.Value) * .05;
                LeagueRevenue_Textbox.Text = total.ToString() + " M";
            }
        }

        private void TVIncome_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentowner.TvIncome = (int)TVIncome_Updown.Value;
                LoadFinances();
            }
        }

        private void TVContract_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentowner.TvContract = (int)TVContract_Updown.Value;
            }
        }

        private void StadiumMaint_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentowner.StadiumMaintenance = (int)StadiumMaint_Updown.Value;
            }
        }

        private void StadiumUpgrades_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                currentowner.StadiumUpgrades = (int)StadiumUpgrades_Updown.Value;
            }
        }

                

    }
}
