using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class WeeklyMaintenanceForm : Form
    {
        private bool triggerChangedEvent = true;
        private bool dirty = false;
        private EditorModel model;

        string installDirectory;
        string profile;

        List<string[]> ratingsVersions;

        public WeeklyMaintenanceForm(EditorModel em)
        {
            InitializeComponent();

            model = em;
            installDirectory = Application.StartupPath;
            GetProfile();

            LoadSettings();

            ratingsVersions = new List<string[]>();
            ratingsVersions.Add(new string[] { "PGID", "PCAR", "PTHA", "PINJ", 
                "PPBK", "PSPD", "PHGT", "PWGT" });
        }

        private void RevertRatings()
        {
            if (!File.Exists(installDirectory + "\\ratings\\" + profile))
            {
                return;
            }

            StreamReader sr = new StreamReader(installDirectory + "\\ratings\\" + profile);
            string versionline = sr.ReadLine();
            int version = Int32.Parse(versionline.Split(':')[1]);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] splitLine = line.Split('\t');

                model.PlayerModel.GetPlayerByPlayerId(Int32.Parse(splitLine[0])).ImportWeeklyData(ratingsVersions[version-1], splitLine);
            }

            sr.Close();
        }

        private void GetProfile()
        {
            StreamReader sr;
            string fullFranchisePath = model.GetFileName();
            string[] splitPath = fullFranchisePath.Split('\\');
            string franchiseFilename = splitPath[splitPath.Length - 1];

            if (!File.Exists(installDirectory + "\\profiles"))
            {
                FileStream file = File.Create(installDirectory + "\\profiles");
                sr = new StreamReader(file);
            }
            else
            {
                sr = new StreamReader(installDirectory + "\\profiles");
            }

            List<string> profiles = new List<string>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                string[] splitLine = line.Split('\t');
                profiles.Add(splitLine[1]);

                if (splitLine[0] == franchiseFilename)
                {
                    profile = splitLine[1];
                    return;
                }
            }

            sr.Close();

            ProfileDialog pd = new ProfileDialog(profiles);
            pd.ShowDialog();
            profile = pd.profile;

            StreamWriter sw = new StreamWriter(installDirectory + "\\profiles", true);
            sw.WriteLine(franchiseFilename + "\t" + profile);
            sw.Close();
        }

        private void SliderValueChanged(object s, EventArgs e)
        {
            if (!triggerChangedEvent)
            {
                return;
            }

            dirty = true;

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

            dirty = true;

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

        private void loadRecommendedSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LoadSettings()
        {
            if (!File.Exists(installDirectory + "\\settings\\" + profile)) 
            {
                return;
            }

            useSliders.Checked = false;
            usePhysicalSliders.Checked = false;
            reorderDepthCharts.Checked = false;

            StreamReader sr = new StreamReader(installDirectory + "\\settings\\" + profile);

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] splitLine = line.Split('\t');

                if (splitLine[0] == "UseSliders")
                {
                    useSliders.Checked = true;
                }
                else if (splitLine[0] == "UsePhysicalSliders")
                {
                    usePhysicalSliders.Checked = true;
                }
                else if (splitLine[0] == "ReorderDepthCharts")
                {
                    reorderDepthCharts.Checked = true;
                }
                else if (splitLine[0] == "Fumbles")
                {
                    fumbleSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "QBAccuracy")
                {
                    accuracySlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "QBInjury")
                {
                    qbInjurySlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "RESacks")
                {
                    reSacksSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SpeedSpread")
                {
                    speedSpreadSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "FixedSpeed")
                {
                    fixedSpeedSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "HeightSpread")
                {
                    heightSpreadSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "FixedHeight")
                {
                    fixedHeightSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "WeightSpread")
                {
                    weightSpreadSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "FixedWeight")
                {
                    fixedWeightSlider.Value = Int32.Parse(splitLine[1]);
                }
            }

            sr.Close();
        }

        private void SaveSettings()
        {
            StreamWriter sw;

            if (!Directory.Exists(installDirectory + "\\settings"))
            {
                Directory.CreateDirectory(installDirectory + "\\settings");
            }

            if (!File.Exists(installDirectory + "\\settings\\" + profile))
            {
                FileStream file = File.Create(installDirectory + "\\settings\\" + profile);
                sw = new StreamWriter(file);
            }
            else
            {
                sw = new StreamWriter(installDirectory + "\\settings\\" + profile, false);
            }

            if (useSliders.Checked)
            {
                sw.WriteLine("UseSliders");
            }

            if (usePhysicalSliders.Checked)
            {
                sw.WriteLine("UsePhysicalSliders");
            }

            if (reorderDepthCharts.Checked)
            {
                sw.WriteLine("ReorderDepthCharts");
            }

            sw.WriteLine("Fumbles\t" + fumbleSlider.Value);
            sw.WriteLine("QBAccuracy\t" + accuracySlider.Value);
            sw.WriteLine("QBInjury\t" + qbInjurySlider.Value);
            sw.WriteLine("RESacks\t" + reSacksSlider.Value);

            sw.WriteLine("SpeedSpread\t" + speedSpreadSlider.Value);
            sw.WriteLine("FixedSpeed\t" + fixedSpeedSlider.Value);
            sw.WriteLine("HeightSpread\t" + heightSpreadSlider.Value);
            sw.WriteLine("FixedHeight\t" + fixedHeightSlider.Value);
            sw.WriteLine("WeightSpread\t" + weightSpreadSlider.Value);
            sw.WriteLine("FixedWeight\t" + fixedWeightSlider.Value);
            sw.Close();

            dirty = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Load previous settings?", "Continue?", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                LoadSettings();
            }
        }

        private void revertRatingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Revert to previous ratings?", "Continue?", MessageBoxButtons.YesNo);

            if (dr == DialogResult.Yes)
            {
                RevertRatings();
            }
        }

        private void makeAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do weekly maintenance?", "Continue?", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            this.Invalidate(true);
            this.Update();
            Cursor.Current = Cursors.WaitCursor;
            
            RevertRatings();

            if (reorderDepthCharts.Checked)
            {
                DepthChartRepairer dcr = new DepthChartRepairer(model, null);
                
                List<int> toSkip = new List<int>();
                foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
                {
                    if (team.UserControlled)
                    {
                        toSkip.Add(team.TeamId);
                    }
                }

                dcr.ReorderDepthCharts(true, toSkip);
            }

            // Find the teams that need adjustments

            List<int> toAdjust = new List<int>();
            Dictionary<int, int> opponents = new Dictionary<int, int>();

            int currentWeek = 30;
            foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (record.State.Id == 1 && record.WeekNumber < currentWeek)
                {
                    currentWeek = record.WeekNumber;
                }
            }

            foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (record.WeekNumber == currentWeek && record.HumanControlled)
                {
                    toAdjust.Add(record.AwayTeam.TeamId);
                    toAdjust.Add(record.HomeTeam.TeamId);

                    opponents.Add(record.HomeTeam.TeamId, record.AwayTeam.TeamId);
                    opponents.Add(record.AwayTeam.TeamId, record.HomeTeam.TeamId);
                }
            }

            List<int> leftTackles = new List<int>();
            Dictionary<int, double> rightEnds = new Dictionary<int,double>();

            if (useSliders.Checked)
            {
                foreach (TableRecordModel record in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
                {
                    DepthChartRecord dcr = (DepthChartRecord)record;

                    if (dcr.DepthOrder == 0 && toAdjust.Contains(dcr.TeamId))
                    {
                        if (dcr.PositionId == (int)MaddenPositions.LT)
                        {
                            leftTackles.Add(dcr.PlayerId);
                        }
                        else if (dcr.PositionId == (int)MaddenPositions.RE)
                        {
                            // Measure the extent to which the guy's a speed rusher
                            PlayerRecord RE = model.PlayerModel.GetPlayerByPlayerId(dcr.PlayerId);
                            int awareness = RE.Awareness;
                            bool REdirty = RE.Dirty;

                            switch (RE.PositionId) {
                                case (int)MaddenPositions.RE:
                                    RE.Awareness *= 1;
                                    break;
                                case (int)MaddenPositions.LE:
                                    RE.Awareness = (int)Math.Round(0.9*RE.Awareness);
                                    break;
                                case (int)MaddenPositions.DT:
                                    RE.Awareness = (int)Math.Round(0.8 * RE.Awareness);
                                    break;
                                case (int)MaddenPositions.LOLB:
                                case (int)MaddenPositions.MLB:
                                case (int)MaddenPositions.ROLB:
                                    RE.Awareness = (int)Math.Round(0.5 * RE.Awareness);
                                    break;
                                default:
                                    RE.Awareness = 0;
                                    break;
                            }

                            rightEnds[dcr.TeamId] = ((double)RE.CalculateOverallRating((int)MaddenPositions.RE,true)/100.0)*(0.7 * Math.Max(0, Math.Min(1, (85.0 - (double)RE.Strength) / 15.0)) + 0.3 * Math.Max(0, Math.Min(1, (130.0 - (double)RE.Weight) / 30.0))) * Math.Max(0, Math.Min(1, ((double)RE.Speed - 65.0) / 15.0));
                            Console.WriteLine(RE.ToString() + " " + rightEnds[dcr.TeamId]);

                            // Put this guy back as he was
                            RE.Awareness = awareness;
                            RE.Dirty = REdirty;
                        }
                    }
                }
            }

            if (!Directory.Exists(installDirectory + "\\ratings"))
            {
                Directory.CreateDirectory(installDirectory + "\\ratings");
            }

            StreamWriter sw;

            if (!File.Exists(installDirectory + "\\ratings\\" + profile))
            {
                FileStream file = File.Create(installDirectory + "\\ratings\\" + profile);
                sw = new StreamWriter(file);
            }
            else
            {
                sw = new StreamWriter(installDirectory + "\\ratings\\" + profile);
            }

            sw.WriteLine("Ratings Format Version:" + ratingsVersions.Count);

            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord player = (PlayerRecord)record;

                if (!toAdjust.Contains(player.TeamId)) { continue; }

                // First, write this player's attributes to the ratings file
                sw.WriteLine(player.RatingsLine(ratingsVersions[ratingsVersions.Count - 1]));

                if (useSliders.Checked)
                {
                    if (fumbleSlider.Value > 50)
                    {
                        player.Carrying -= (int)Math.Round(((double)fumbleSlider.Value - 50.0) * ((double)player.Carrying / 50.0));
                    }
                    else
                    {
                        player.Carrying += (int)Math.Min((99 - player.Carrying), Math.Round((50.0 - (double)fumbleSlider.Value) * ((double)player.Carrying / 50.0)));
                    }


                    if (accuracySlider.Value < 50)
                    {
                        player.ThrowAccuracy -= (int)Math.Round((50.0 - (double)accuracySlider.Value) * ((double)player.ThrowAccuracy / 50.0));
                    }
                    else
                    {
                        player.ThrowAccuracy += (int)Math.Min((99 - player.ThrowAccuracy), Math.Round(((double)accuracySlider.Value - 50.0) * ((double)player.ThrowAccuracy / 50.0)));
                    }


                    if (player.PositionId == (int)MaddenPositions.QB)
                    {
                        if (qbInjurySlider.Value > 50)
                        {
                            player.Injury -= (int)Math.Round(((double)qbInjurySlider.Value - 50.0) * ((double)player.Injury / 50.0));
                        }
                        else
                        {
                            player.Injury += (int)Math.Min((99 - player.Injury), Math.Round((50.0 - (double)qbInjurySlider.Value) * ((double)player.Injury / 50.0)));
                        }
                    }

                    if (leftTackles.Contains(player.PlayerId))
                    {
                        if (reSacksSlider.Value > 50)
                        {
                            // Subtract pass blocking depending on the extent
                            // to which the RE is a "speed rusher".
                            player.PassBlocking -= (int)Math.Round(rightEnds[opponents[player.TeamId]]*((double)reSacksSlider.Value - 50.0) * ((double)player.PassBlocking / 50.0));
                        }
                        else
                        {
                            player.PassBlocking += (int)Math.Min((99 - player.PassBlocking), Math.Round((50.0 - (double)reSacksSlider.Value) * ((double)player.PassBlocking / 50.0)));
                        }
                    }
                }

                if (usePhysicalSliders.Checked)
                {
                    if ((speedSpreadSlider.Value > 50 && player.Speed > fixedSpeedSlider.Value) || (speedSpreadSlider.Value < 50 && player.Speed < fixedSpeedSlider.Value))
                    {
                        // Here we're increasing the rating
                        player.Speed += (int)Math.Min(99 - player.Speed, Math.Round((double)Math.Abs(50 - speedSpreadSlider.Value) * ((double)Math.Abs(player.Speed - fixedSpeedSlider.Value)) / 50.0));
                    }
                    else
                    {
                        // Here we're decreasing the rating
                        player.Speed -= (int)Math.Min(player.Speed, Math.Round((double)Math.Abs(50 - speedSpreadSlider.Value) * ((double)Math.Abs(player.Speed - fixedSpeedSlider.Value)) / 50.0));
                    }

                    if (player.PositionId == (int)MaddenPositions.WR || player.PositionId == (int)MaddenPositions.TE)
                    {
                        if ((heightSpreadSlider.Value > 50 && player.Height > fixedHeightSlider.Value) || (heightSpreadSlider.Value < 50 && player.Height < fixedHeightSlider.Value))
                        {
                            // Here we're increasing the rating
                            player.Height += (int)Math.Min(84 - player.Height, Math.Round((double)Math.Abs(50 - heightSpreadSlider.Value) * ((double)Math.Abs(player.Height - fixedHeightSlider.Value)) / 50.0));
                        }
                        else
                        {
                            // Here we're decreasing the rating
                            player.Height -= (int)Math.Min(player.Height - 66, Math.Round((double)Math.Abs(50 - heightSpreadSlider.Value) * ((double)Math.Abs(player.Height - fixedHeightSlider.Value)) / 50.0));
                        }
                    }

                    if ((weightSpreadSlider.Value > 50 && (player.Weight + 160) > fixedWeightSlider.Value) || (weightSpreadSlider.Value < 50 && (player.Weight+160) < fixedWeightSlider.Value))
                    {
                        // Here we're increasing the rating
                        player.Weight += (int)Math.Min(255 - player.Weight, Math.Round((double)Math.Abs(50 - weightSpreadSlider.Value) * ((double)Math.Abs((player.Weight + 160) - fixedWeightSlider.Value)) / 50.0));
                    }
                    else
                    {
                        // Here we're decreasing the rating
                        player.Weight -= (int)Math.Min(player.Weight, Math.Round((double)Math.Abs(50 - weightSpreadSlider.Value) * ((double)Math.Abs((player.Weight + 160) - fixedWeightSlider.Value)) / 50.0));
                    }
                }

                // Let's leave OVR's where they're at.
                //player.Overall = player.CalculateOverallRating(player.PositionId, true);
            }

            sw.Close();
            Cursor.Current = Cursors.Arrow;
        }

        private void WeeklyMaintenanceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Do you want to save the changes to your settings?", "Save?", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    SaveSettings();
                }
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            dirty = true;
        }
    }
}