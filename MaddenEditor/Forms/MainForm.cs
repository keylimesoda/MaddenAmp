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
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class MainForm : Form
    {
		private EditorModel model = null;
		private string fileToLoad;
		private bool isInitialising = false;
		
		private SearchForm searchForm = null;

        public MainForm()
        {
            InitializeComponent();
			
			tabControl.Visible = false;
			searchToolStripMenuItem.Visible = false;
			statusStrip.Visible = false;

			isInitialising = false;
        }

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
			if (CheckSave())
			{
				Application.Exit();
			}
        }

		private bool CheckSave()
		{
			if (model != null && model.Dirty)
			{
				DialogResult result = MessageBox.Show("Do you want to save changes to currently opened file?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					model.Save();
				}
				else if (result == DialogResult.Cancel)
				{
					return false;
				}
			}

			if (model != null)
			{
				model.Shutdown();
			}	
			model = null;

			return true;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!CheckSave())
			{
				return;
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "ros";
			dialog.Filter = "Madden files (*.ros;*.fra)|*.ros;*.fra";
			dialog.Multiselect = false;
			dialog.ShowDialog();
			if (dialog.FileNames.Length > 0)
			{
				foreach (string filename in dialog.FileNames)
				{
					fileToLoad = filename;
					// Insert code here to process the files.
					try
					{
						this.Cursor = Cursors.WaitCursor;

						CleanUI();
						statusStrip.Visible = true;

						rosterFileLoaderThread.DoWork += new DoWorkEventHandler(rosterFileLoaderThread_DoWork);
						rosterFileLoaderThread.RunWorkerAsync();
						
						break;
						//Now the model is opened.

					}
					catch (ApplicationException err)
					{
						MessageBox.Show(err.ToString(), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		void rosterFileLoaderThread_DoWork(object sender, DoWorkEventArgs e)
		{
			model = new EditorModel(fileToLoad, this);
			//once the model is initialised set tell the custom edit
			//controls about it
			playerEditControl.Model = model;
		}

		public void updateProgress(int percentage)
		{
			rosterFileLoaderThread.ReportProgress(percentage);
		}

		private void InitialiseUI()
		{
			playerEditControl.InitialiseUI();
		}
		
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			isInitialising = true;

			CheckSave();

			CleanUI();			
		}

		private void CleanUI()
		{
			//Now clean up ready for reloading
			searchForm = null;

			playerEditControl.CleanUI();
			
			tabControl.Visible = false;
			searchToolStripMenuItem.Visible = false;
		}

		private void rosterFileLoaderThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			isInitialising = true;

			try
			{
				InitialiseUI();
			}
			catch (Exception err)
			{
				Console.WriteLine(err.ToString());
			}

			tabControl.Visible = true;
			searchToolStripMenuItem.Visible = true;
			statusStrip.Visible = false;
			toolStripProgressBar.Value = 0;
			this.Cursor = Cursors.Default;

			isInitialising = false;
		}

		private void rosterFileLoaderThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			toolStripProgressBar.Value = e.ProgressPercentage;
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CheckSave();

			Application.Exit();
		}
		
		public bool Dirty
		{
			set
			{
				saveToolStripMenuItem.Enabled = value;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			model.Save();
		}
						
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox form = new AboutBox();
			form.ShowDialog(this);
		}
		
		private void searchforPlayerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (searchForm == null)
			{
				searchForm = new SearchForm(model);
			}

			searchForm.ShowDialog(this);

			if (searchForm.DialogResult == DialogResult.OK)
			{
				//Found a new player to switch to
				playerEditControl.LoadPlayerInfo(searchForm.SelectedPlayer);
			}
		}
		
		private void testButton_Click(object sender, EventArgs e)
		{
			if (testerWorkerThread.IsBusy)
			{
				testerWorkerThread.CancelAsync();
				
			}
			else
			{
				testerWorkerThread.DoWork += new DoWorkEventHandler(testerWorkerThread_DoWork);
				testerWorkerThread.RunWorkerAsync();
			}
		}

		private void testerWorkerThread_DoWork(object sender, DoWorkEventArgs e)
		{
			// This method will run on a thread other than the UI thread.
			// Be sure not to manipulate any Windows Forms controls created
			// on the UI thread from this method.

		}

		private void testerWorkerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			model.PlayerModel.GetNextPlayerRecord();
			playerEditControl.LoadPlayerInfo(model.PlayerModel.CurrentPlayerRecord);
		}
    }
}