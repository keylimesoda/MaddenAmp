using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	public partial class CoachEditControl : UserControl, IEditorForm
	{
		private bool isInitialising = false;

		private EditorModel model = null;

		public CoachEditControl()
		{
			isInitialising = true;

			InitializeComponent();

			isInitialising = false;
		}

		public void LoadCoachInfo(CoachRecord record)
		{

		}

		#region Coaches General Settings

		private void coachesPositionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		#region IEditorForm Members

		public EditorModel Model
		{
			set { model = value; }
		}

		public void InitialiseUI()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void CleanUI()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
}
}
