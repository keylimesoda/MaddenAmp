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
			}

			foreach (object obj in removeList)
			{
				teamsList.Items.Remove(obj);
			}

			AddButtonEnable();
		}
}
}