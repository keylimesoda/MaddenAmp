using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
	public partial class ExceptionDialog : Form
	{
		//The Exception that this dialog displays
		protected Exception exception;
		protected ExceptionDialog(Exception e)
		{
			this.exception = e;
			InitializeComponent();

			tbException.Text = e.ToString();
			
			if (e.InnerException != null) 
			{
				tbException.Text = tbException.Text + e.InnerException.ToString();
			}

			tbException.Select(0, 0);
		}

		public static void Show(Exception e) 
		{
			ExceptionDialog dialog = new ExceptionDialog(e);
			dialog.ShowDialog();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Dispose();
		}
	}
}