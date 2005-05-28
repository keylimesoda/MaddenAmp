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
	public partial class ScheduleEditingForm : Form, IEditorForm
	{
		// Reference to our editor model
		private EditorModel model = null;
		// Reference to our schedule editing model
		private ScheduleEditingModel scheduleModel = null;
		// Flag for editing
		private bool isInitialising = false;
		// Current week number
		private int currentWeekNumber = 1;

		public ScheduleEditingForm(EditorModel model)
		{
			isInitialising = true;
			this.model = model;
			InitializeComponent();

			this.Cursor = Cursors.WaitCursor;
			//Create our model
			scheduleModel = new ScheduleEditingModel(model);

			this.Cursor = Cursors.Default;

			LoadWeek(currentWeekNumber);

			isInitialising = false;
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			for (int i=1; i <= ScheduleEditingModel.NUMBER_OF_WEEKS; i++)
			{
				cbWeekSelector.Items.Add("" + i);
			}
			cbWeekSelector.SelectedIndex = 0;
		}

		public void CleanUI()
		{
			
		}

		#endregion

		private void LoadWeek(int weeknumber)
		{
			isInitialising = true;

			//Clear the current rows
			dgScheduleView.Rows.Clear();
			try
			{
				IList<ScheduleRecord> list = scheduleModel.GetWeek(weeknumber);

				foreach (ScheduleRecord record in list)
				{
					//match the fields in record with those to go in the datagrid
					dgScheduleView.Rows.Add(CreateDataGridRow(record));
				}

				lblTitle.Text = "Week " + weeknumber;
			}
			catch (Exception e)
			{
				MessageBox.Show("Exception occured loading week " + weeknumber + "\r\n" + e.ToString(), "Exception Loading Week", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				isInitialising = false;
			}
		}

		private void ScheduleEditingForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Dispose of our schedule model
			scheduleModel = null;
		}

		public DataGridViewRow CreateDataGridRow(ScheduleRecord record)
		{
			DataGridViewRow viewRow = new DataGridViewRow();

			DataGridViewComboBoxCell stateCell = new DataGridViewComboBoxCell();
			stateCell.DataSource = record.GameStates;
			stateCell.Value = record.State;

			DataGridViewComboBoxCell homeCell = new DataGridViewComboBoxCell();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				homeCell.Items.Add(rec);
			}
			homeCell.Value = record.HomeTeam;

			DataGridViewTextBoxCell homeScoreCell = new DataGridViewTextBoxCell();
			homeScoreCell.Value = record.HomeTeamScore;

			DataGridViewComboBoxCell awayCell = new DataGridViewComboBoxCell();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				awayCell.Items.Add(rec);
			}
			awayCell.Value = record.AwayTeam;

			DataGridViewTextBoxCell awayScoreCell = new DataGridViewTextBoxCell();
			awayScoreCell.Value = record.AwayTeamScore;

			DataGridViewCheckBoxCell overtimeCell = new DataGridViewCheckBoxCell();
			overtimeCell.Value = record.OverTime;

			DataGridViewComboBoxCell dayTypeCell = new DataGridViewComboBoxCell();
			dayTypeCell.DataSource = record.GameDayTypes;
			dayTypeCell.Value = record.DayType;

			DataGridViewComboBoxCell weightingCell = new DataGridViewComboBoxCell();
			weightingCell.DataSource = record.GameWeightings;
			weightingCell.Value = record.Weighting;
			
			viewRow.Cells.Add(stateCell);
			viewRow.Cells.Add(homeCell);
			viewRow.Cells.Add(homeScoreCell);
			viewRow.Cells.Add(awayCell);
			viewRow.Cells.Add(awayScoreCell);
			viewRow.Cells.Add(overtimeCell);
			viewRow.Cells.Add(dayTypeCell);
			viewRow.Cells.Add(weightingCell);

			return viewRow;
		}

		private void btnPreviousWeek_Click(object sender, EventArgs e)
		{
			if (currentWeekNumber > 1)
			{
				currentWeekNumber--;
				LoadWeek(currentWeekNumber);
			}
		}

		private void btnNextWeek_Click(object sender, EventArgs e)
		{
			if (currentWeekNumber < ScheduleEditingModel.NUMBER_OF_WEEKS)
			{
				currentWeekNumber++;
				LoadWeek(currentWeekNumber);
			}
		}

		private void cbWeekSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadWeek(cbWeekSelector.SelectedIndex + 1);
		}
	}
}