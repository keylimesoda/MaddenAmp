/******************************************************************************
 * Gommo's Madden Editor
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
	public partial class DepthChartEditorForm : Form, IEditorForm
	{
		private EditorModel model = null;

		private DepthChartEditorControl depthChartControl = null;

		public DepthChartEditorForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();

			this.Cursor = Cursors.WaitCursor;

			depthChartControl = new DepthChartEditorControl();
			depthChartControl.Model = model;

			this.Controls.Add(depthChartControl);

			depthChartControl.Dock = DockStyle.Fill;

			this.Cursor = Cursors.Default;
		}
		
		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			depthChartControl.InitialiseUI();
		}

		public void CleanUI()
		{
			depthChartControl.CleanUI();
		}

		#endregion

		private void DepthChartEditorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CleanUI();
		}
}
}