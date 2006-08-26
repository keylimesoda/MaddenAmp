/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
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
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;

namespace MaddenEditor.Forms
{
	public partial class FranchiseOptionsForm : Form, IEditorForm
	{
		/** Reference to our editor model */
		private EditorModel model = null;
		/** Boolean to stop events firing when initialising */
		private bool isInitialising = false;

		public FranchiseOptionsForm(EditorModel model)
		{
			isInitialising = true;

			this.model = model;

			InitializeComponent();

			isInitialising = false;

			InitialiseUI();
		}

		#region IEditorForm Members

		public EditorModel Model
		{
			//Dont set anything
			set {  }
		}

		public void InitialiseUI()
		{
			isInitialising = true;

			year1RFA.Value = model.SalaryCapModel.RestrictedFA1;
			year2RFA.Value = model.SalaryCapModel.RestrictedFA2;
			year3RFA.Value = model.SalaryCapModel.RestrictedFA3;
			year4RFA.Value = model.SalaryCapModel.RestrictedFA4;
			salaryCap.Value = model.SalaryCapModel.SalaryCap;

			if (model.GameOptionModel != null)
			{
				cbCapPenalties.Enabled = true;
				cbSalaryCap.Enabled = true;
				cbTradeDeadline.Enabled = true;
				cbOwnerMode.Enabled = true;
				cbCapPenalties.Checked = model.GameOptionModel.CapPenalty;
				cbSalaryCap.Checked = model.GameOptionModel.SalaryCap;
				cbTradeDeadline.Checked = model.GameOptionModel.TradeDeadline;
				cbOwnerMode.Checked = model.GameOptionModel.OwnerMode;
			}
			else
			{
				cbCapPenalties.Enabled = false;
				cbSalaryCap.Enabled = false;
				cbTradeDeadline.Enabled = false;
				cbOwnerMode.Enabled = false;
			}

			isInitialising = false;
		}

		public void CleanUI()
		{
			
		}

		#endregion

		private void okButton_Click(object sender, EventArgs e)
		{
			model.SalaryCapModel.RestrictedFA1 = (int)year1RFA.Value;
			model.SalaryCapModel.RestrictedFA2 = (int)year2RFA.Value;
			model.SalaryCapModel.RestrictedFA3 = (int)year3RFA.Value;
			model.SalaryCapModel.RestrictedFA4 = (int)year4RFA.Value;
			model.SalaryCapModel.SalaryCap = (int)salaryCap.Value;
			model.GameOptionModel.OwnerMode = cbOwnerMode.Checked;
			model.GameOptionModel.TradeDeadline = cbTradeDeadline.Checked;
			model.GameOptionModel.SalaryCap = cbSalaryCap.Checked;
			model.GameOptionModel.CapPenalty = cbCapPenalties.Checked;

			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

	}
}