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
			CleanUI();
			scheduleModel = null;
		}

		public DataGridViewRow CreateDataGridRow(ScheduleRecord record)
		{
			//Bloody hell. I can't see how to put actual objects in the
			//datagridviewcomboboxcells so I'm just putting strings.
			//It sucks but I'll fix it later if I find out how
			DataGridViewRow viewRow = new DataGridViewRow();

			DataGridViewComboBoxCell stateCell = new DataGridViewComboBoxCell();
			foreach(GenericRecord val in record.GameStates)
			{
				stateCell.Items.Add(val.ToString());
			}
			stateCell.Value = record.State.ToString();

			DataGridViewComboBoxCell homeCell = new DataGridViewComboBoxCell();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				homeCell.Items.Add(rec.ToString());
			}
			homeCell.Value = record.HomeTeam;

			DataGridViewTextBoxCell homeScoreCell = new DataGridViewTextBoxCell();
			homeScoreCell.Value = record.HomeTeamScore;

			DataGridViewComboBoxCell awayCell = new DataGridViewComboBoxCell();
			foreach (TableRecordModel rec in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
			{
				awayCell.Items.Add(rec.ToString());
			}
			awayCell.Value = record.AwayTeam;

			DataGridViewTextBoxCell awayScoreCell = new DataGridViewTextBoxCell();
			awayScoreCell.Value = record.AwayTeamScore;

			DataGridViewCheckBoxCell overtimeCell = new DataGridViewCheckBoxCell();
			overtimeCell.Value = record.OverTime;

			DataGridViewComboBoxCell dayTypeCell = new DataGridViewComboBoxCell();
			foreach (GenericRecord val in record.GameDayTypes)
			{
				dayTypeCell.Items.Add(val.ToString());
			}
			dayTypeCell.Value = record.DayType;

			DataGridViewComboBoxCell weightingCell = new DataGridViewComboBoxCell();
			foreach (GenericRecord val in record.GameWeightings)
			{
				weightingCell.Items.Add(val.ToString());
			}
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
				cbWeekSelector.SelectedIndex = currentWeekNumber - 1;
				LoadWeek(currentWeekNumber);
			}
		}

		private void btnNextWeek_Click(object sender, EventArgs e)
		{
			if (currentWeekNumber < ScheduleEditingModel.NUMBER_OF_WEEKS)
			{
				currentWeekNumber++;
				cbWeekSelector.SelectedIndex = currentWeekNumber - 1;
				LoadWeek(currentWeekNumber);
			}
		}

		private void cbWeekSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadWeek(cbWeekSelector.SelectedIndex + 1);
		}

		private void dgScheduleView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (!isInitialising)
			{
				switch (e.ColumnIndex)
				{
					case 0: // Game state
						foreach (GenericRecord rec in ScheduleRecord.gameStateList)
						{
							if (rec.ToString().Equals(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
							{
								//Found the rec 
								scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].State = rec;
								break;
							}
						}
						break;
					case 1: // Home Team
						scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].HomeTeam = model.TeamModel.GetTeamRecord(model.TeamModel.GetTeamIdFromTeamName(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
						break;
					case 2: // Home Team Score
						scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].HomeTeamScore = Convert.ToInt32(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
						break;
					case 3: // Away Team
						scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].AwayTeam = model.TeamModel.GetTeamRecord(model.TeamModel.GetTeamIdFromTeamName(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
						break;
					case 4: // Away Team Score
						scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].AwayTeamScore = Convert.ToInt32(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
						break;
					case 5: // Overtime
						scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].OverTime = Convert.ToBoolean(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
						break;
					case 6: // Day
						foreach (GenericRecord rec in ScheduleRecord.gameDayTypeList)
						{
							if (rec.ToString().Equals(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
							{
								//Found the rec
								scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].DayType = rec;
								break;
							}
						}
						break;
					case 7: // Weighting
						foreach (GenericRecord rec in ScheduleRecord.gameWeightings)
						{
							if (rec.ToString().Equals(dgScheduleView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
							{
								//Found the rec
								scheduleModel.GetWeek(currentWeekNumber)[e.RowIndex].Weighting = rec;
								break;
							}
						}
						break;
					default:
						break;
				}
			}
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}