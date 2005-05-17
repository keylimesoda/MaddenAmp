/******************************************************************************
 * Madden 2005 Editor
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
 * colin.goudie@gmail.com
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
	public partial class SalaryCapForm : Form, IEditorForm
	{
		/** Reference to our editor model */
		private EditorModel model = null;
		/** Boolean to stop events firing when initialising */
		private bool isInitialising = false;

		public SalaryCapForm(EditorModel model)
		{
			isInitialising = true;

			this.model = model;

			InitializeComponent();

			isInitialising = false;

			InitialiseUI();
		}

		private void year1RFA_ValueChanged(object sender, EventArgs e)
		{

		}

		private void year2RFA_ValueChanged(object sender, EventArgs e)
		{

		}

		private void year3RFA_ValueChanged(object sender, EventArgs e)
		{

		}

		private void year4RFA_ValueChanged(object sender, EventArgs e)
		{

		}

		private void salaryCap_ValueChanged(object sender, EventArgs e)
		{

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
			DialogResult = DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
}
}