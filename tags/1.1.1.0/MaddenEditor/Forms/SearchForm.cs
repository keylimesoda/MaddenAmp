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
	public enum SearchType { PLAYER, COACH };

	public partial class SearchForm : Form
	{
		private EditorModel model = null;
		private TableRecordModel selectedTarget = null;
		private Dictionary<String, TableRecordModel> results = null;
		private SearchType searchType = SearchType.PLAYER;

		public SearchForm(EditorModel model, SearchType searchType)
		{
			this.model = model;
			this.searchType = searchType;
			selectedTarget = null;
			InitializeComponent();
		}

		public TableRecordModel SelectedSearchTarget
		{
			get
			{
				return selectedTarget;
			}
		}

		private void searchButton_Click(object sender, EventArgs e)
		{
			if (searchTextBox.Text.Equals(""))
			{
				return;
			}
			char[] delims = { ' ', ',' };
			string searchString = searchTextBox.Text.ToLower();
			string[] searchterms = searchString.Split(delims);
			selectedTarget = null;

			this.Cursor = Cursors.WaitCursor;
			if (results != null)
			{
				results.Clear();
				results = null;
			}
			if (searchType == SearchType.PLAYER)
			{
				results = model.PlayerModel.SearchForPlayers(searchterms);
			}
			else
			{
				results = model.CoachModel.SearchForCoaches(searchterms);
			}
			this.Cursor = Cursors.Default;

			if (results == null)
			{
				resultsListBox.Items.Clear();
				resultsListBox.Items.Add("No results");
			}
			else
			{
				resultsListBox.Items.Clear();
				foreach (String result in results.Keys)
				{
					resultsListBox.Items.Add(result);
				}
			}
		}

		private void resultsListBox_DoubleClick(object sender, EventArgs e)
		{
			string item = resultsListBox.SelectedItem.ToString();

			selectedTarget = results[item];
			this.DialogResult = DialogResult.OK;
			
		}

	}
}