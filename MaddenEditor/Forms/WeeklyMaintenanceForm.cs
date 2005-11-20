using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
    public partial class WeeklyMaintenanceForm : Form
    {
        private bool triggerChangedEvent = true;

        public WeeklyMaintenanceForm()
        {
            InitializeComponent();
        }

        private void SliderValueChanged(object s, EventArgs e)
        {
            if (!triggerChangedEvent)
            {
                return;
            }

            NumericUpDown UpDownToChange = null;
            TrackBar sender = (TrackBar)s;

            if (sender == fumbleSlider)
            {
                UpDownToChange = fumbleUpDown;
            }
            else if (sender == accuracySlider)
            {
                UpDownToChange = accuracyUpDown;
            }
            else if (sender == qbInjurySlider)
            {
                UpDownToChange = qbInjuryUpDown;
            }
            else if (sender == reSacksSlider)
            {
                UpDownToChange = reSacksUpDown;
            }
            else if (sender == speedSpreadSlider)
            {
                UpDownToChange = speedSpreadUpDown;
            }
            else if (sender == fixedSpeedSlider)
            {
                UpDownToChange = fixedSpeedUpDown;
            }
            else if (sender == heightSpreadSlider)
            {
                UpDownToChange = heightSpreadUpDown;
            }
            else if (sender == fixedHeightSlider)
            {
                UpDownToChange = fixedHeightUpDown;
            }
            else if (sender == weightSpreadSlider)
            {
                UpDownToChange = weightSpreadUpDown;
            }
            else if (sender == fixedWeightSlider)
            {
                UpDownToChange = fixedWeightUpDown;
            }

            triggerChangedEvent = false;
            UpDownToChange.Value = sender.Value;
            triggerChangedEvent = true;
        }

        private void UpDown_ValueChanged(object s, EventArgs e)
        {
            if (!triggerChangedEvent)
            {
                return;
            }

            NumericUpDown sender = (NumericUpDown)s;
            TrackBar sliderToChange = null;

            if (sender == fumbleUpDown)
            {
                sliderToChange = fumbleSlider;
            }
            else if (sender == accuracyUpDown)
            {
                sliderToChange = accuracySlider;
            }
            else if (sender == qbInjuryUpDown)
            {
                sliderToChange = qbInjurySlider;
            }
            else if (sender == reSacksUpDown)
            {
                sliderToChange = reSacksSlider;
            }
            else if (sender == speedSpreadUpDown)
            {
                sliderToChange = speedSpreadSlider;
            }
            else if (sender == fixedSpeedUpDown)
            {
                sliderToChange = fixedSpeedSlider;
            }
            else if (sender == heightSpreadUpDown)
            {
                sliderToChange = heightSpreadSlider;
            }
            else if (sender == fixedHeightUpDown)
            {
                sliderToChange = fixedHeightSlider;
            }
            else if (sender == weightSpreadUpDown)
            {
                sliderToChange = weightSpreadSlider;
            }
            else if (sender == fixedWeightUpDown)
            {
                sliderToChange = fixedWeightSlider;
            }

            triggerChangedEvent = false;
            sliderToChange.Value = (int)sender.Value;
            triggerChangedEvent = true;
        }
    }
}