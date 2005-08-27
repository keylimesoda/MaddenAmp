/******************************************************************************
 * Gommo's Madden Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
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
	public partial class UserControlledTeamForm : Form, IEditorForm
	{
		private EditorModel model = null;

		public UserControlledTeamForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		private void addTeamButton_Click(object sender, EventArgs e)
		{
			if (!teamsList.Items.Contains(teamCombo.SelectedItem))
			{
				teamsList.Items.Add(teamCombo.SelectedItem);
			}

			AddButtonEnable();
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			//The the team combo up
			foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
			{
				teamCombo.Items.Add(team);
			}

			teamCombo.Text = teamCombo.Items[0].ToString();

			foreach (object obj in teamCombo.Items)
			{
				OwnerRecord rec = (OwnerRecord)obj;

				if (rec.UserControlled)
				{
					teamsList.Items.Add(rec);
				}
			}

			AddButtonEnable();
		}

		public void CleanUI()
		{
			teamCombo.Items.Clear();
		}

		#endregion

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void AddButtonEnable()
		{
			if (teamsList.Items.Contains(teamCombo.SelectedItem))
			{
				addTeamButton.Enabled = false;
			}
			else
			{
				addTeamButton.Enabled = true;
			}
		}

		private void teamCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
				AddButtonEnable();
		}

		private void addAllButton_Click(object sender, EventArgs e)
		{
			foreach (Object obj in teamCombo.Items)
			{
				if (!teamsList.Items.Contains(obj))
				{
					teamsList.Items.Add(obj);
				}
			}

			AddButtonEnable();
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			foreach (Object obj in teamsList.Items)
			{
				OwnerRecord record = (OwnerRecord)obj;

				record.UserControlled = true;
				record.ComputerControl1 = false;
				record.ComputerControl2 = false;
				record.ComputerControl3 = false;
				record.ComputerControl4 = false;
				record.ComputerControl5 = false;
				record.ComputerControl6 = false;
				record.ComputerControl7 = false;
				//Need to also change the schedule records for the games for this team
				foreach (TableRecordModel rec in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
				{
					try
					{
						if (((ScheduleRecord)rec).AwayTeam.TeamId == record.TeamId || ((ScheduleRecord)rec).HomeTeam.TeamId == record.TeamId)
						{
							//Then set this game to user controlled
							((ScheduleRecord)rec).HumanControlled = true;
						}
					}
					catch (NullReferenceException err)
					{
						//A null reference exception happens when its trying to find teams that don't
						//exist on the schedule, its ok
						Console.WriteLine("Team id no found when setting user controlled teams. This is ok");
					}
				}
				//Also need to find the User controlled bit in the Head coaches record for this team
				foreach (TableRecordModel rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
				{
					CoachRecord crec = (CoachRecord)rec;
					if (crec.TeamId == record.TeamId && crec.Position == 0) // Position 0 is Head coach
					{
						crec.HumanControlled = true;
					}
				}
			}

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void removeSelectedButton_Click(object sender, EventArgs e)
		{
			List<object> removeList = new List<object>();

			foreach (Object obj in teamsList.SelectedItems)
			{
				OwnerRecord rec = (OwnerRecord)obj;
				removeList.Add(obj);

				rec.UserControlled = false;
				rec.ComputerControl1 = true;
				rec.ComputerControl2 = true;
				rec.ComputerControl3 = true;
				rec.ComputerControl4 = true;
				rec.ComputerControl5 = true;
				rec.ComputerControl6 = true;
				rec.ComputerControl7 = true;

				//Need to also change the schedule records for the games for this team
				foreach (TableRecordModel record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
				{
					try
					{
						if (((ScheduleRecord)record).AwayTeam.TeamId == rec.TeamId || ((ScheduleRecord)record).HomeTeam.TeamId == rec.TeamId)
						{
							//Then set this game to user controlled
							((ScheduleRecord)record).HumanControlled = false;
						}
					}
					catch (NullReferenceException err)
					{
						//A null reference exception happens when its trying to find teams that don't
						//exist on the schedule, its ok
						Console.WriteLine("Team id no found when setting user controlled teams. This is ok");
					}
				}
				//Also need to find the User controlled bit in the Head coaches record for this team
				foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
				{
					CoachRecord crec = (CoachRecord)record;
					if (crec.TeamId == rec.TeamId && crec.Position == 0) // Position 0 is Head coach
					{
						crec.HumanControlled = false;
					}
				}
			}

			foreach (object obj in removeList)
			{
				teamsList.Items.Remove(obj);
			}

			AddButtonEnable();
		}
}
}