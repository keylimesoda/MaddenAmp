using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Forms;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class City : UserControl, IEditorForm
    {
        private EditorModel model = null;
        private bool isInitialising = false;
        private CityRecord currentcity = null;
        private int currentrec = 0;


        #region IEditorForm members

        public MaddenEditor.Core.EditorModel Model
        {
            set { this.model = value; }
        }

        public void InitialiseUI()
        {
            if (Region_Combobox.Items != null)
            {
                Region_Combobox.Items.Clear();
                Region_Combobox.Items.Add("Out Of Area");
                Region_Combobox.Items.Add("Southwest");
                Region_Combobox.Items.Add("Southeast");
                Region_Combobox.Items.Add("Northeast");
                Region_Combobox.Items.Add("Northwest");
                Region_Combobox.Items.Add("Midwest");
            }
            
            if (Timezone_Combobox.Items != null)
            {
                Timezone_Combobox.Items.Clear();
                Timezone_Combobox.Items.Add("Pacific");
                Timezone_Combobox.Items.Add("Mountain");
                Timezone_Combobox.Items.Add("Central");
                Timezone_Combobox.Items.Add("Eastern");
            }

            LoadCity();
        }

        public void CleanUI()
        {
            Region_Combobox.SelectedIndex = -1;
        }

        #endregion

        public City()
        {
            isInitialising = true;

            InitializeComponent();

            isInitialising = false;            
        }
               
        
        public void LoadCity()
        {
            isInitialising = true;
            currentcity = (CityRecord)model.TableModels[EditorModel.CITY_TABLE].GetRecord(currentrec);

            CityID.Value = currentrec;
            CityName_Textbox.Text = currentcity.CityName;
            State_Textbox.Text = currentcity.StateName;
            CityPop.Value = currentcity.CityPopulation;
            TeamDemand.Value = currentcity.Demand;
            Region_Combobox.SelectedIndex = currentcity.Region;
            AvgTempJan.Value = currentcity.TempJan;
            AvgTempSep.Value = currentcity.TempSep;
            Television.Value = currentcity.Television;
            OnMap_Checkbox.Checked = currentcity.OnMap;           
            FairWeatherVariance.Value = currentcity.FairWeather;
            FairWeatherPercentage.Value = currentcity.YearlyFairPerc;
            RainPercentage.Value = currentcity.YearlyRainPerc;
            WindPercentage.Value = currentcity.YearlyWindPerc;
            WinterWeather.Value = currentcity.CYww;
            SnowPercentage.Value = currentcity.YearlySnowPerc;
            Relocate.Value = currentcity.Owrc;            
            RelocateUnknown.Value = currentcity.Orot;

            // Customize the min,max values for relocation year            
            RelocateYear.Minimum = model.CurrentYearIndex + (int)model.MadVersion + currentcity.Owry;
            RelocateYear.Maximum = model.CurrentYearIndex + (int)model.MadVersion + currentcity.Owry + 15;
            RelocateYear.Value = model.CurrentYearIndex + (int)model.MadVersion + currentcity.Owry;

            if (model.MadVersion > MaddenFileVersion.Ver2004)
            {
                Timezone_Combobox.Enabled = true;
                Timezone_Combobox.SelectedIndex = currentcity.Timezone;
                Newspaper.Enabled = true;
                Newspaper.Text = currentcity.Newspaper;
            }
            else
            {
                Timezone_Combobox.Enabled = false;
                Timezone_Combobox.SelectedIndex = -1;
                Newspaper.Enabled = false;
                Newspaper.Text = "";
            }
            isInitialising = false;
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (currentrec == model.TableModels[EditorModel.CITY_TABLE].RecordCount - 1)
                    currentrec = 0;
                else currentrec++;
                LoadCity();
            }
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            if (!isInitialising)
            {
                if (currentrec == 0)
                    currentrec = model.TableModels[EditorModel.CITY_TABLE].RecordCount - 1;
                else currentrec--;
                LoadCity();
            }
        }

        private void Relocate_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Owrc = (int)Relocate.Value;
        }

        private void RelocateYear_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Owry = (int)RelocateYear.Value - model.CurrentYearIndex - (int)model.MadVersion;
        }

        private void RelocateUnknown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Orot = (int)RelocateUnknown.Value;
        }

        private void CityName_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.CityName = CityName_Textbox.Text;
        }

        private void State_Textbox_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.StateName = State_Textbox.Text;
        }

        private void CityPop_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.CityPopulation = (int)CityPop.Value;
        }

        private void Region_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Region = (int)Region_Combobox.SelectedIndex;
        }

        private void Timezone_Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Timezone = (int)Timezone_Combobox.SelectedIndex;
        }

        private void OnMap_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.OnMap = OnMap_Checkbox.Checked;
        }

        private void Newspaper_TextChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Newspaper = Newspaper.Text;
        }

        private void FairWeatherVariance_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.FairWeather = (int)FairWeatherVariance.Value;
        }

        private void FairWeatherPercentage_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.YearlyFairPerc = (int)FairWeatherPercentage.Value;
        }

        private void RainPercentage_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.YearlyRainPerc = (int)RainPercentage.Value;
        }

        private void WindPercentage_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.YearlyWindPerc = (int)WindPercentage.Value;
        }

        private void WinterWeather_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.CYww = (int)WinterWeather.Value;
        }

        private void SnowPercentage_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.YearlySnowPerc = (int)SnowPercentage.Value;
        }

        private void TeamDemand_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Demand = (int)TeamDemand.Value;
        }

        private void AvgTempJan_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.TempJan = (int)AvgTempJan.Value;
        }

        private void AvgTempSep_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.TempSep = (int)AvgTempSep.Value;
        }

        private void Television_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialising)
                currentcity.Television = (int)Television.Value;
        }
       
    
    
    }
}
