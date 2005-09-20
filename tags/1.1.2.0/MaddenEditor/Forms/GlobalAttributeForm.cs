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
	public partial class GlobalAttributeForm : Form, IEditorForm
	{
		private EditorModel model = null;

		private enum EditableAttributes
		{
			AGE,
			YEARS_EXP,
			SPEED,
			STRENGTH,
			AWARENESS,
			AGILITY,
			ACCELERATION,
			CATCHING,
			CARRYING,
			JUMPING,
			BREAK_TACKLE,
			TACKLE,
			THROW_POWER,
			THROW_ACCURACY,
			PASS_BLOCKING,
			RUN_BLOCKING,
			KICK_POWER,
			KICK_ACCURACY,
			KICK_RETURN,
			STAMINA,
			INJURY,
			TOUGHNESS,
			IMPORTANCE,
			MORALE
		}

		public GlobalAttributeForm(EditorModel model)
		{
			this.model = model;
			InitializeComponent();
		}

		private void filterTeamComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#region IEditorForm Members

		public EditorModel Model
		{
			set {  }
		}

		public void InitialiseUI()
		{
			foreach (TeamRecord team in model.TeamModel.GetTeams())
			{
				filterTeamComboBox.Items.Add(team);
			}

			foreach (string pos in Enum.GetNames(typeof(MaddenPositions)))
			{
				filterPositionComboBox.Items.Add(pos);
			}

			System.Diagnostics.Debug.Assert(attributeCombo.Items.Count == Enum.GetNames(typeof(EditableAttributes)).Length, "Attribute Combo and enum count don't match");

			filterPositionComboBox.Text = filterPositionComboBox.Items[0].ToString();
			filterTeamComboBox.Text = filterTeamComboBox.Items[0].ToString();
			attributeCombo.Text = attributeCombo.Items[0].ToString();
		}

		public void CleanUI()
		{
			filterTeamComboBox.Items.Clear();
			filterPositionComboBox.Items.Clear();
			attributeCombo.Items.Clear();
		}

		#endregion

		private void applyButton_Click(object sender, EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			int count = 0;
			foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
			{
				if (record.Deleted)
					continue;

				PlayerRecord playerRecord = (PlayerRecord)record;

				if (chkTeamFilter.Checked)
				{
					if (playerRecord.TeamId != ((TeamRecord)filterTeamComboBox.SelectedItem).TeamId)
					{
						continue;
					}
				}
				if (chkPositionFilter.Checked)
				{
					if (playerRecord.PositionId != filterPositionComboBox.SelectedIndex)
					{
						continue;
					}
				}
				if (chkAgeFilter.Checked)
				{
					if (rbAgeGreaterThan.Checked)
					{
						if (playerRecord.Age <= (int)nudAgeFilter.Value)
						{
							continue;
						}
					}
					else if (rbAgeLessThan.Checked)
					{
						if (playerRecord.Age >= (int)nudAgeFilter.Value)
						{
							continue;
						}
					}
					else
					{
						if (playerRecord.Age != (int)nudAgeFilter.Value)
						{
							continue;
						}
					}
				}
				if (chkYearsProFilter.Checked)
				{
					if (rbYearsProGreaterThan.Checked)
					{
						if (playerRecord.YearsPro <= (int)nudYearsProFilter.Value)
						{
							continue;
						}
					}
					else if (rbYearsProLessThan.Checked)
					{
						if (playerRecord.YearsPro >= (int)nudYearsProFilter.Value)
						{
							continue;
						}
					}
					else
					{
						if (playerRecord.YearsPro != (int)nudYearsProFilter.Value)
						{
							continue;
						}
					}
				}

				bool absoluteValue = true;
				int value = 0;
				if (setCheckBox.Checked)
				{
					value = (int)setNumeric.Value;
				}
				else if (incrementCheckBox.Checked)
				{
					absoluteValue = false;
					value = (int)incrementNumeric.Value;
				}
				else
				{
					absoluteValue = false;
					value = 0 - (int)decrementNumeric.Value;
				}
				
				ChangeAttribute(playerRecord, (EditableAttributes)attributeCombo.SelectedIndex, absoluteValue, value);
				count++;
				
			}
			this.Cursor = Cursors.Default;
			DialogResult = DialogResult.OK;
			this.Close();
			MessageBox.Show("Attribute " + attributeCombo.SelectedItem + " changed " + count + " players successfully", "Change OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void ChangeAttribute(PlayerRecord record, EditableAttributes attribute, bool absolutevalue, int value)
		{
			switch (attribute)
			{
				case EditableAttributes.ACCELERATION:
					if (absolutevalue)
						record.Acceleration = value;
					else
						record.Acceleration = record.Acceleration + value;

					if (record.Acceleration < 0) record.Acceleration = 0;
					if (record.Acceleration > 99) record.Acceleration = 99;
					break;
				case EditableAttributes.AGE:
					if (absolutevalue)
						record.Age = value;
					else
						record.Age = record.Age + value;
					if (record.Age < 0) record.Age = 0;
					break;
				case EditableAttributes.AGILITY:
					if (absolutevalue)
						record.Agility = value;
					else
						record.Agility = record.Agility + value;
					if (record.Agility < 0) record.Agility = 0;
					if (record.Agility > 99) record.Agility = 99;
					break;
				case EditableAttributes.AWARENESS:
					if (absolutevalue)
						record.Awareness = value;
					else
						record.Awareness = record.Awareness + value;
					if (record.Awareness < 0) record.Awareness = 0;
					if (record.Awareness > 99) record.Awareness = 99;
					break;
				case EditableAttributes.BREAK_TACKLE:
					if (absolutevalue)
						record.BreakTackle = value;
					else
						record.BreakTackle = record.BreakTackle + value;
					if (record.BreakTackle < 0) record.BreakTackle = 0;
					if (record.BreakTackle > 99) record.BreakTackle = 99;
					break;
				case EditableAttributes.CARRYING:
					if (absolutevalue)
						record.Carrying = value;
					else
						record.Carrying = record.Carrying + value;
					if (record.Carrying < 0) record.Carrying = 0;
					if (record.Carrying > 99) record.Carrying = 99;
					break;
				case EditableAttributes.CATCHING:
					if (absolutevalue)
						record.Catching = value;
					else
						record.Catching = record.Catching + value;
					if (record.Catching < 0) record.Catching = 0;
					if (record.Catching > 99) record.Catching = 99;
					break;
				case EditableAttributes.IMPORTANCE:
					if (absolutevalue)
						record.Importance = value;
					else
						record.Importance = record.Importance + value;
					if (record.Importance < 0) record.Importance = 0;
					if (record.Importance > 99) record.Importance = 99;
					break;
				case EditableAttributes.INJURY:
					if (absolutevalue)
						record.Injury = value;
					else
						record.Injury = record.Injury + value;
					if (record.Injury < 0) record.Injury = 0;
					if (record.Injury > 99) record.Injury = 99;
					break;
				case EditableAttributes.JUMPING:
					if (absolutevalue)
						record.Jumping = value;
					else
						record.Jumping = record.Jumping + value;
					if (record.Jumping < 0) record.Jumping = 0;
					if (record.Jumping > 99) record.Jumping = 99;
					break;
				case EditableAttributes.KICK_ACCURACY:
					if (absolutevalue)
						record.KickAccuracy = value;
					else
						record.KickAccuracy = record.KickAccuracy + value;
					if (record.KickAccuracy < 0) record.KickAccuracy = 0;
					if (record.KickAccuracy > 99) record.KickAccuracy = 99;
					break;
				case EditableAttributes.KICK_POWER:
					if (absolutevalue)
						record.KickPower = value;
					else
						record.KickPower = record.KickPower + value;
					if (record.KickPower < 0) record.KickPower = 0;
					if (record.KickPower > 99) record.KickPower = 99;
					break;
				case EditableAttributes.KICK_RETURN:
					if (absolutevalue)
						record.KickReturn = value;
					else
						record.KickReturn = record.KickReturn + value;
					if (record.KickReturn < 0) record.KickReturn = 0;
					if (record.KickReturn > 99) record.KickReturn = 99;
					break;
				case EditableAttributes.MORALE:
					if (absolutevalue)
						record.Morale = value;
					else
						record.Morale = record.Morale + value;
					if (record.Morale < 0) record.Morale = 0;
					if (record.Morale > 99) record.Morale = 99;
					break;
				case EditableAttributes.PASS_BLOCKING:
					if (absolutevalue)
						record.PassBlocking = value;
					else
						record.PassBlocking = record.PassBlocking + value;
					if (record.PassBlocking < 0) record.PassBlocking = 0;
					if (record.PassBlocking > 99) record.PassBlocking = 99;
					break;
				case EditableAttributes.RUN_BLOCKING:
					if (absolutevalue)
						record.RunBlocking = value;
					else
						record.RunBlocking = record.RunBlocking + value;
					if (record.RunBlocking < 0) record.RunBlocking = 0;
					if (record.RunBlocking > 99) record.RunBlocking = 99;
					break;
				case EditableAttributes.SPEED:
					if (absolutevalue)
						record.Speed = value;
					else
						record.Speed = record.Speed + value;
					if (record.Speed < 0) record.Speed = 0;
					if (record.Speed > 99) record.Speed = 99;
					break;
				case EditableAttributes.STAMINA:
					if (absolutevalue)
						record.Stamina = value;
					else
						record.Stamina = record.Stamina + value;
					if (record.Stamina < 0) record.Stamina = 0;
					if (record.Stamina > 99) record.Stamina = 99;
					break;
				case EditableAttributes.STRENGTH:
					if (absolutevalue)
						record.Strength = value;
					else
						record.Strength = record.Strength + value;
					if (record.Strength < 0) record.Strength = 0;
					if (record.Strength > 99) record.Strength = 99;
					break;
				case EditableAttributes.TACKLE:
					if (absolutevalue)
						record.Tackle = value;
					else
						record.Tackle = record.Tackle + value;
					if (record.Tackle < 0) record.Tackle = 0;
					if (record.Tackle > 99) record.Tackle = 99;
					break;
				case EditableAttributes.THROW_ACCURACY:
					if (absolutevalue)
						record.ThrowAccuracy = value;
					else
						record.ThrowAccuracy = record.ThrowAccuracy + value;
					if (record.ThrowAccuracy < 0) record.ThrowAccuracy = 0;
					if (record.ThrowAccuracy > 99) record.ThrowAccuracy = 99;
					break;
				case EditableAttributes.THROW_POWER:
					if (absolutevalue)
						record.ThrowPower = value;
					else
						record.ThrowPower = record.ThrowPower + value;
					if (record.ThrowPower < 0) record.ThrowPower = 0;
					if (record.ThrowPower > 99) record.ThrowPower = 99;
					break;
				case EditableAttributes.TOUGHNESS:
					if (absolutevalue)
						record.Toughness = value;
					else
						record.Toughness = record.Toughness + value;
					if (record.Toughness < 0) record.Toughness = 0;
					if (record.Toughness > 99) record.Toughness = 99;
					break;
				case EditableAttributes.YEARS_EXP:
					if (absolutevalue)
						record.YearsPro = value;
					else
						record.YearsPro = record.YearsPro + value;
					if (record.YearsPro < 0) record.YearsPro = 0;
					break;


			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
}
}