/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2014 Stingray68
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class StadiumEditForm : UserControl, IEditorForm
    {
        private EditorModel model = null;
        private StadiumRecord lastLoadedRecord = null;
        private bool isInitialising = false;
        private int previous_stadiumtype = 0;
        public int currentstadiumrow = 0;

        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        public void InitialiseUI()
        {
            isInitialising = true;

            //  Setup combo boxes
            foreach (GenericRecord rec in model.StadiumModel.CityList)
                CityComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.FieldTypes)
                FieldTypeComboBox.Items.Add(rec);

            //  Stadium Design
            foreach (GenericRecord rec in model.StadiumModel.Deck1List)
            {
                NorthDeck1ComboBox.Items.Add(rec);
                SouthDeck1ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.Deck2List)
            {
                NorthDeck2ComboBox.Items.Add(rec);
                SouthDeck2ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.Deck3List)
            {
                NorthDeck3ComboBox.Items.Add(rec);
                SouthDeck3ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.CornerTypes)
            {
                NWCornerDeck1ComboBox.Items.Add(rec);
                NECornerDeck1ComboBox.Items.Add(rec);
                SWCornerDeck1ComboBox.Items.Add(rec);
                SECornerDeck1ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.Deck4List)
            {
                EastDeck1ComboBox.Items.Add(rec);
                WestDeck1ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.Deck5List)
            {
                EastDeck2ComboBox.Items.Add(rec);
                WestDeck2ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.Deck6List)
            {
                EastDeck3ComboBox.Items.Add(rec);
                WestDeck3ComboBox.Items.Add(rec);
            }
            foreach (GenericRecord rec in model.StadiumModel.RoofTypes)
                RoofComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.RoofLights)
                RoofLightsComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.EndzoneWalls)
                EndzoneWallsComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.SidelinePattern)
                SidelinePatternComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.FieldSurface)
                FieldSurfaceComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.EndzoneColor)
                EndzoneBackgroundComboBox.Items.Add(rec);
            foreach (GenericRecord rec in model.StadiumModel.StadiumBackdrop)
                StadiumBackdropComboBox.Items.Add(rec);

            InitStadiumList();
        }

        public void CleanUI()
        {

        }

        #endregion

        public StadiumEditForm()
        {
            isInitialising = true;

            InitializeComponent();

            isInitialising = false;           
        }

        public void InitStadiumList()
        {
            model.StadiumModel.GetStadiumList();

            StadiumGridView.Rows.Clear();
            StadiumGridView.Refresh();
            StadiumGridView.MultiSelect = false;
            StadiumGridView.RowHeadersVisible = false;
            StadiumGridView.AutoGenerateColumns = false;
            StadiumGridView.AllowUserToAddRows = false;
            StadiumGridView.ColumnCount = 2;
            StadiumGridView.Columns[0].Name = "ID";
            StadiumGridView.Columns[0].Width = 35;
            StadiumGridView.Columns[1].Name = "Stadium";
            StadiumGridView.Columns[1].Width = 100;
            foreach (KeyValuePair<int, string> stadium in model.StadiumModel.stadiumnames)
            {
                object[] o = { (int)stadium.Key, (string)stadium.Value };
                StadiumGridView.Rows.Add(o);
            }
            if (model.StadiumModel.stadiumnames.Count > 0)
            {
                StadiumGridView.Rows[0].Selected = true;
                LoadStadium(model.StadiumModel.GetStadiumByID((int)StadiumGridView.Rows[0].Cells[0].Value));
            }
            else MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        public void LoadStadium(StadiumRecord record)
        {
            if (record == null)
            {
                MessageBox.Show("No Records available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            isInitialising = true;

            try
            {
                StadiumNameTextBox.Text = record.StadiumName;
                StadiumAgeNumericUpDown.Value = record.StadiumAge;
                StadiumRatingNumericUpDown.Value = record.StadiumRating;
                StadiumIdNumericUpDown.Value = record.StadiumId;

                SeatingMaxUpDown.Value = record.CapacityMax;
                ClubSeatsUpDown.Value = record.CapacityClubSeats;
                LuxuryBoxUpDown.Value = record.CapacityLuxuryBox;
                UpperLevelUpDown.Value = record.CapacityUpperLevel;
                MidLevelUpDown.Value = record.CapacityMidLevel;
                LowerLevelUpDown.Value = record.CapacityLowerLevel;
                UpperEndzoneUpDown.Value = record.CapacityUpperEndzone;


                //Set Combo Boxes selected index
                foreach (Object obj in CityComboBox.Items)
                    if (((GenericRecord)obj).Id == record.CityId)
                    {
                        CityComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in FieldTypeComboBox.Items)
                    if (((GenericRecord)obj).Id == record.FieldType)
                    {
                        FieldTypeComboBox.SelectedItem = obj;
                        break;
                    }

                StadiumTypeCheckBox.Checked = true;
                StadiumDesignGroupBox.Enabled = false;
                ResourceNumericUpDown.Enabled = true;
                ResourceNumericUpDown.Value = record.Stadium_DAT_Resource;
                ClimateControlCheckBox.Checked = record.StadiumClimateControl;

                if (record.StadiumType > 0)
                {
                    StadiumTypeCheckBox.Checked = false;
                    StadiumDesignGroupBox.Enabled = true;
                    ResourceNumericUpDown.Enabled = false;
                    previous_stadiumtype = record.StadiumType;
                }

                ClimateControlCheckBox.Checked = record.StadiumClimateControl;

                //  Stadium Design Combo Boxes
                foreach (Object obj in NorthDeck1ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_North_Deck1)
                    {
                        NorthDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in NorthDeck2ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_North_Deck2)
                    {
                        NorthDeck2ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in NorthDeck3ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_North_Deck3)
                    {
                        NorthDeck3ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SouthDeck1ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_South_Deck1)
                    {
                        SouthDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SouthDeck2ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_South_Deck2)
                    {
                        SouthDeck2ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SouthDeck3ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_South_Deck3)
                    {
                        SouthDeck3ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in NWCornerDeck1ComboBox.Items)

                    if (((GenericRecord)obj).Id == record.StadiumPart_NW_Corner_Deck1)
                    {
                        NWCornerDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in NECornerDeck1ComboBox.Items)

                    if (((GenericRecord)obj).Id == record.StadiumPart_NE_Corner_Deck1)
                    {
                        NECornerDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SWCornerDeck1ComboBox.Items)

                    if (((GenericRecord)obj).Id == record.StadiumPart_SW_Corner_Deck1)
                    {
                        SWCornerDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SECornerDeck1ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_SE_Corner_Deck1)
                    {
                        SECornerDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in WestDeck1ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_WSIDE_Deck1)
                    {
                        WestDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in WestDeck2ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_WSIDE_Deck2)
                    {
                        WestDeck2ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in WestDeck3ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_WSIDE_Deck3)
                    {
                        WestDeck3ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in EastDeck1ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_ESIDE_Deck1)
                    {
                        EastDeck1ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in EastDeck2ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_ESIDE_Deck2)
                    {
                        EastDeck2ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in EastDeck3ComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_ESIDE_Deck3)
                    {
                        EastDeck3ComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in RoofComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Roof)
                    {
                        RoofComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in RoofLightsComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Roof_Lights)
                    {
                        RoofLightsComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in SidelinePatternComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Sideline_Pattern)
                    {
                        SidelinePatternComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in EndzoneWallsComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Endzone_Walls)
                    {
                        EndzoneWallsComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in FieldSurfaceComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Field_Type)
                    {
                        FieldSurfaceComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in EndzoneBackgroundComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Endzone_Back)
                    {
                        EndzoneBackgroundComboBox.SelectedItem = obj;
                        break;
                    }
                foreach (Object obj in StadiumBackdropComboBox.Items)
                    if (((GenericRecord)obj).Id == record.StadiumPart_Stadium_Backdrop)
                    {
                        StadiumBackdropComboBox.SelectedItem = obj;
                        break;
                    }

                SpecialLogoNumericUpDown.Enabled = false;
                if (model.FileVersion >= MaddenFileVersion.Ver2006)
                {
                    SpecialLogoNumericUpDown.Enabled = true;
                    SpecialLogoNumericUpDown.Value = record.SpecialLogo;
                }

            }

            catch (Exception e)
            {
                MessageBox.Show("Exception Occurred loading this Stadium:\r\n" + e.ToString(), "Exception Loading Stadium", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }
            finally
            {
                isInitialising = false;
            }            
        }

        #region Info Controls
        private void StadiumNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumName = StadiumNameTextBox.Text;
        }

        private void CityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CityId = ((GenericRecord)CityComboBox.SelectedItem).Id;
        }

        private void StadiumAgeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumAge = (int)StadiumAgeNumericUpDown.Value;
        }

        private void StadiumRatingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumRating = (int)StadiumRatingNumericUpDown.Value;
        }

        private void StadiumIdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumId = (int)StadiumIdNumericUpDown.Value;
        }

        #endregion

        #region Seating Capacity Controls

        private void SeatingMaxUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityMax = (int)SeatingMaxUpDown.Value;            
        }
        private void ClubSeatsUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityClubSeats = (int)ClubSeatsUpDown.Value;
        }
        private void LuxuryBoxUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityLuxuryBox = (int)LuxuryBoxUpDown.Value;
        }
        private void UpperLevelUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityUpperLevel = (int)UpperLevelUpDown.Value;
        }
        private void MidLevelUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityMidLevel = (int)MidLevelUpDown.Value;
        }
        private void LowerLevelUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityLowerLevel = (int)LowerLevelUpDown.Value;
        }
        private void UpperEndzoneUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.CapacityUpperEndzone = (int)UpperEndzoneUpDown.Value;
        }

        #endregion
        
        #region Stadium Options Controls

        private void StadiumTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (StadiumTypeCheckBox.Checked == true)
                {
                    StadiumDesignGroupBox.Enabled = false;                    
                }
                else
                {
                    StadiumDesignGroupBox.Enabled = true;
                }
                model.StadiumModel.CurrentStadiumRecord.StadiumType = 0;
                if (StadiumTypeCheckBox.Checked == false)
                    model.StadiumModel.CurrentStadiumRecord.StadiumType = previous_stadiumtype;
            }        
        }
        private void ResourceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.Stadium_DAT_Resource = (int)ResourceNumericUpDown.Value;
        }
        private void ClimateControlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumClimateControl = ClimateControlCheckBox.Checked;
        }
        private void FieldTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.FieldType = ((GenericRecord)FieldTypeComboBox.SelectedItem).Id;
        }
        private void SpecialLogoNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.SpecialLogo = (int)SpecialLogoNumericUpDown.Value;
        }

        #endregion
        
        #region Stadium Design Controls

        private void NorthDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_North_Deck1 = ((GenericRecord)NorthDeck1ComboBox.SelectedItem).Id;
        }
        private void NorthDeck2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_North_Deck2 = ((GenericRecord)NorthDeck2ComboBox.SelectedItem).Id;
        }
        private void NorthDeck3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_North_Deck3 = ((GenericRecord)NorthDeck3ComboBox.SelectedItem).Id;
        }
        private void SouthDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_South_Deck1 = ((GenericRecord)SouthDeck1ComboBox.SelectedItem).Id;
        }
        private void SouthDeck2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_South_Deck2 = ((GenericRecord)SouthDeck2ComboBox.SelectedItem).Id;
        }
        private void SouthDeck3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_South_Deck3 = ((GenericRecord)SouthDeck3ComboBox.SelectedItem).Id;
        }
        private void WestDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_WSIDE_Deck1 = ((GenericRecord)WestDeck1ComboBox.SelectedItem).Id;
        }
        private void WestDeck2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_WSIDE_Deck2 = ((GenericRecord)WestDeck2ComboBox.SelectedItem).Id;
        }
        private void WestDeck3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_WSIDE_Deck3 = ((GenericRecord)WestDeck3ComboBox.SelectedItem).Id;
        }
        private void EastDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_ESIDE_Deck1 = ((GenericRecord)EastDeck1ComboBox.SelectedItem).Id;
        }
        private void EastDeck2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_ESIDE_Deck2 = ((GenericRecord)EastDeck2ComboBox.SelectedItem).Id;
        }
        private void EastDeck3ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_ESIDE_Deck3 = ((GenericRecord)EastDeck3ComboBox.SelectedItem).Id;
        }       
        private void NWCornerDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_NW_Corner_Deck1 = ((GenericRecord)NWCornerDeck1ComboBox.SelectedItem).Id;
        }
        private void NECornerDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_NE_Corner_Deck1 = ((GenericRecord)NECornerDeck1ComboBox.SelectedItem).Id;
        }
        private void SWCornerDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_SW_Corner_Deck1 = ((GenericRecord)SWCornerDeck1ComboBox.SelectedItem).Id;
        }
        private void SECornerDeck1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_SE_Corner_Deck1 = ((GenericRecord)SECornerDeck1ComboBox.SelectedItem).Id;
        }
        private void RoofComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Roof = ((GenericRecord)RoofComboBox.SelectedItem).Id;
        }
        private void RoofLightsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Roof_Lights = ((GenericRecord)RoofLightsComboBox.SelectedItem).Id;
        }
        private void SidelinePatternComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Sideline_Pattern = ((GenericRecord)SidelinePatternComboBox.SelectedItem).Id;
        }
        private void EndzoneWallsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Endzone_Walls = ((GenericRecord)EndzoneWallsComboBox.SelectedItem).Id;
        }
        private void FieldSurfaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Field_Type = ((GenericRecord)FieldSurfaceComboBox.SelectedItem).Id;                
        }
        private void EndzoneBackgroundComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Endzone_Back = ((GenericRecord)EndzoneBackgroundComboBox.SelectedItem).Id;
        }
        private void StadiumBackdropComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                model.StadiumModel.CurrentStadiumRecord.StadiumPart_Stadium_Backdrop = ((GenericRecord)StadiumBackdropComboBox.SelectedItem).Id;
        }
        
        #endregion



        #region Navigation Controls

        private void StadiumGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (!isInitialising)
            {
                DataGridViewRow row = StadiumGridView.Rows[e.RowIndex];
                int r = (int)row.Cells[0].Value;
                if (r == currentstadiumrow)
                    return;
                else
                {
                    StadiumGridView.Rows[currentstadiumrow].Selected = false;
                    LoadStadium(model.StadiumModel.GetStadiumByID(r));
                    StadiumGridView.Rows[e.RowIndex].Selected = true;
                    currentstadiumrow = e.RowIndex;
                }
            }
        }

        #endregion

        

    }
}
