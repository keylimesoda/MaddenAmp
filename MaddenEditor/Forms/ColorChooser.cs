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

namespace MaddenEditor.Forms
{
	public partial class ColorChooser : Form
	{
        private bool isInitializing;
        private Color color;

		public ColorChooser(Color color)
		{
			this.color = color;
			InitializeComponent();

			vsRed.Value = 255 - color.R;
			vsGreen.Value = 255 - color.G;
			vsBlue.Value = 255 - color.B;

			nudRed.Value = color.R;
			nudGreen.Value = color.G;
			nudBlue.Value = color.B;

			constructColor();
		}

		public Color ChosenColor
		{
			get
			{
				return this.color;
			}
		}

		private void constructColor()
		{
			this.color = Color.FromArgb(255 - vsRed.Value, 255 - vsGreen.Value, 255 - vsBlue.Value);

			nudHexadecimal.Value = this.color.ToArgb() & 0xFFFFFF;

			pnlPreview.BackColor = this.color;
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;

			this.Close();
		}

		private void nudHexadecimal_ValueChanged(object sender, EventArgs e)
		{
            if (!isInitializing)
            {
                // s68 blue channel wasn't setting and I'm not good with masking/shifting 
                // so using another method
                //vsRed.Value = 255 - (((int)nudHexadecimal.Value & 0x00FF0000) >> 16);
                //nudRed.Value = (((int)nudHexadecimal.Value & 0x00FF0000) >> 16);
                //vsGreen.Value = 255 - (((int)nudHexadecimal.Value & 0x0000FF00) >> 8);
                //nudGreen.Value = (((int)nudHexadecimal.Value & 0x0000FF00) >> 8);
                //vsBlue.Value = 255 - (((int)nudHexadecimal.Value & 0x000000FF));
                //nudBlue.Value = (((int)nudHexadecimal.Value & 0x000000FF));
                isInitializing = true;
                byte[] col = BitConverter.GetBytes((int)nudHexadecimal.Value);
                vsBlue.Value = 255-col[0];
                nudBlue.Value = col[0];
                vsGreen.Value = 255-col[1];
                nudGreen.Value = col[1];
                vsRed.Value = 255-col[2];
                nudRed.Value = col[2];
                isInitializing = false;
                constructColor();
            }
		}

		private void vsBlue_Scroll(object sender, ScrollEventArgs e)
		{
			nudBlue.Value = 255 - vsBlue.Value;

			constructColor();
		}

		private void vsGreen_Scroll(object sender, ScrollEventArgs e)
		{
			nudGreen.Value = 255 - vsGreen.Value;

			constructColor();
		}

		private void vsRed_Scroll(object sender, ScrollEventArgs e)
		{
			nudRed.Value = 255 - vsRed.Value;

			constructColor();
		}

		private void nudBlue_ValueChanged(object sender, EventArgs e)
		{
			vsBlue.Value = 255 - (int)nudBlue.Value;

			constructColor();
		}

		private void nudGreen_ValueChanged(object sender, EventArgs e)
		{
			vsGreen.Value = 255 - (int)nudGreen.Value;

			constructColor();
		}

		private void nudRed_ValueChanged(object sender, EventArgs e)
		{
			vsRed.Value = 255 - (int)nudRed.Value;

            constructColor();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;

			this.Close();
		}
		
	}
}