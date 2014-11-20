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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace MaddenEditor.Forms
{
	public partial class DeveloperBioForm : Form
	{
		private Developer david;
		private Developer colin;
		private Developer josh;
        private Developer steve;

		public DeveloperBioForm()
		{
			InitializeComponent();

			//Load up the Bio texts
			david = new Developer("David Boeck", 
								   "bakersville123",
								   "Developer. Designed Offseason Conditioning and Training Camp Features",
								   "MaddenEditor.Resources.DavidBio.txt");

			colin = new Developer("Colin Goudie", 
								  "gommo",
								  "Developer & Maintainer. Original designer of MaddenEditor that has evolved into Madden Amp", 
								  "MaddenEditor.Resources.ColinBio.txt");

			josh = new Developer("Josh",
				                 "Spin16",
								 "Developer. Designed Draft Engine and Logic", 
								 "MaddenEditor.Resources.JoshBio.txt");

            steve = new Developer("Steve", 
                                "Stingray68", 
                                "Developer.  Bug fixes, Stats, Player and Coach Portraits", 
                                "MaddenEditor.Resources.SteveBio.txt");

			LoadDeveloper(colin);
		}

		private static string ReadResource(string file)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			TextReader textReader = new StreamReader(assembly.GetManifestResourceStream(file));
			string result = textReader.ReadToEnd();
			textReader.Close();

			return result;
		}

		private void btnColin_Click(object sender, EventArgs e)
		{
			LoadDeveloper(colin);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnDavid_Click(object sender, EventArgs e)
		{
			LoadDeveloper(david);
		}

		private void btnJosh_Click(object sender, EventArgs e)
		{
			LoadDeveloper(josh);
		}
        private void SteveButton_Click(object sender, EventArgs e)
        {
            LoadDeveloper(steve);
        }  
		private void LoadDeveloper(Developer dev)
		{
			tbBioText.Text = dev.Bio;
			lblName.Text = dev.Name;
			lblNick.Text = dev.Nick;
			tbRole.Text = dev.Role;
		}

		private class Developer
		{
			private String name;
			private String role;
			private String bio;
			private String nick;

			public Developer(String name, String nick, String role, String bioResourceName)
			{
				this.name = name;
				this.nick = nick;
				this.role = role;
				bio = ReadResource(bioResourceName);	
			}

			public String Name
			{
				get { return name; }
			}

			public String Nick
			{
				get { return nick; }
			}

			public String Role
			{
				get { return role; }
			}

			public String Bio
			{
				get { return bio; }
			}
		}

              
    }
}