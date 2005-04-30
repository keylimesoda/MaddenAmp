using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Domain;

namespace MaddenEditor.Forms
{
    public partial class MainForm : Form
    {
		private RosterModel model = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			CheckSave();

			Application.Exit();
        }

		private void CheckSave()
		{
			if (model != null && model.Dirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to currently opened file?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{

				}
				else
				{

				}
			}

			model = null;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CheckSave();

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "ros";
			dialog.Filter = "Madden Roster files (*.ros)|*.ros";
			dialog.Multiselect = false;
			dialog.ShowDialog();
			if (dialog.FileNames.Length > 0)
			{
				foreach (string filename in dialog.FileNames)
				{
					// Insert code here to process the files.
					try
					{
						Cursor.Current = Cursors.WaitCursor;

						model = new RosterModel(filename);

						Cursor.Current = Cursors.Default;

						//Now the model is opened.

					}
					catch (ApplicationException err)
					{
						MessageBox.Show(err.ToString(), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}
    }
}