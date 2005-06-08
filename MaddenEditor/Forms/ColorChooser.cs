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
			vsRed.Value = 255 - (((int)nudHexadecimal.Value & 0x00FF0000) >> 16);
			nudRed.Value = (((int)nudHexadecimal.Value & 0x00FF0000) >> 16);

			vsGreen.Value = 255 - (((int)nudHexadecimal.Value & 0x0000FF00) >> 8);
			nudGreen.Value = (((int)nudHexadecimal.Value & 0x0000FF00) >> 8);

			vsBlue.Value = 255 - (((int)nudHexadecimal.Value & 0x000000FF));
			nudBlue.Value = (((int)nudHexadecimal.Value & 0x000000FF));

			constructColor();
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
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;

			this.Close();
		}
		
	}
}