/******************************************************************************
 * Gommo's Madden Editor
 * Copyright (C) 2005 Spin16
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
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Record.FranchiseState;

namespace MaddenEditor.Forms
{
    public partial class WeeklyMaintenanceForm : Form
    {
        const double sackProbability = 0.725;
        const double passingYardsMultiplier = 1.104;
        const double rushingYardsMultiplier = 1.023;
        const double sackedYardsMultiplier = 0.903;
        const double rushingAttemptsMultiplier = 0.9285;

        const double FBcarriesProbability = 0.19;

        const double interceptionsProbability = 0.86;
        const double passesDefendedProbability = 0.9345;

        const double fumbleProbability = 0.9889;
        const double fumbleForcedProbability = 0.9301;
        const double fumbleRecoveredProbability = .8833;

        const double pancakeProbability = 0.45;

        const double firstDownMultiplier = 1.145;
        const double thirdDownAttemptsProbability = 0.83;

        const double thirdDownConversionsProbability = 0.175;
        const double fourthDownAttemptsProbability = 0.41;
        const double fourthDownConversionsProbability = 0.8;

        /* These numbers look correct, but for some reason they don't give
         * accurate stats
         * 
        const double fourthDownAttemptsProbability = 0.5764;
         * */

        Random rand = new Random();

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
            ratingsVersions.Add(new string[] { "PGID", "PCAR", "PTHA", "PINJ", 
                "PPBK", "PSPD", "PHGT", "PWGT", "PSTA" });

            dirty = false;
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

                try
                {
                    model.PlayerModel.GetPlayerByPlayerId(Int32.Parse(splitLine[0])).ImportWeeklyData(ratingsVersions[version - 1], splitLine);
                }
                catch
                {

                }
            }

            sr.Close();
            File.Delete(installDirectory + "\\ratings\\" + profile);
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
            else if (sender == staminaSlider)
            {
                UpDownToChange = staminaUpDown;
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
            else if (sender == simOffRPSlider)
            {
                UpDownToChange = simOffRPUpDown;
            }
            else if (sender == simOffAggSlider)
            {
                UpDownToChange = simOffAggUpDown;
            }
            else if (sender == simHBSlider)
            {
                UpDownToChange = simHBUpDown;
            }
            else if (sender == simDefRPSlider)
            {
                UpDownToChange = simDefRPUpDown;
            }
            else if (sender == simDefAggSlider)
            {
                UpDownToChange = simDefAggUpDown;
            }
            else if (sender == offMaxAdjustSlider)
            {
                UpDownToChange = offMaxAdjustUpDown;
            }
            else if (sender == defMaxAdjustSlider)
            {
                UpDownToChange = defMaxAdjustUpDown;
            }
            else if (sender == gameOffRPSlider)
            {
                UpDownToChange = gameOffRPUpDown;
            }
            else if (sender == gameOffAggSlider)
            {
                UpDownToChange = gameOffAggUpDown;
            }
            else if (sender == gameHBSlider)
            {
                UpDownToChange = gameHBUpDown;
            }
            else if (sender == gameDefRPSlider)
            {
                UpDownToChange = gameDefRPUpDown;
            }
            else if (sender == gameDefAggSlider)
            {
                UpDownToChange = gameDefAggUpDown;
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
            else if (sender == staminaUpDown)
            {
                sliderToChange = staminaSlider;
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
            else if (sender == simOffRPUpDown)
            {
                sliderToChange = simOffRPSlider;
            }
            else if (sender == simOffAggUpDown)
            {
                sliderToChange = simOffAggSlider;
            }
            else if (sender == simHBUpDown)
            {
                sliderToChange = simHBSlider;
            }
            else if (sender == simDefRPUpDown)
            {
                sliderToChange = simDefRPSlider;
            }
            else if (sender == simDefAggUpDown)
            {
                sliderToChange = simDefAggSlider;
            }
            else if (sender == defMaxAdjustUpDown)
            {
                sliderToChange = defMaxAdjustSlider;
            }
            else if (sender == offMaxAdjustUpDown)
            {
                sliderToChange = offMaxAdjustSlider;
            }
            else if (sender == gameOffRPUpDown)
            {
                sliderToChange = gameOffRPSlider;
            }
            else if (sender == gameOffAggUpDown)
            {
                sliderToChange = gameOffAggSlider;
            }
            else if (sender == gameHBUpDown)
            {
                sliderToChange = gameHBSlider;
            }
            else if (sender == gameDefRPUpDown)
            {
                sliderToChange = gameDefRPSlider;
            }
            else if (sender == gameDefAggUpDown)
            {
                sliderToChange = gameDefAggSlider;
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
            coachSim.Checked = false;
            coachGame.Checked = false;
            fixSimEngine.Checked = false;

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
                else if (splitLine[0] == "FixSimEngine")
                {
                    fixSimEngine.Checked = true;
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
                else if (splitLine[0] == "Stamina")
                {
                    staminaSlider.Value = Int32.Parse(splitLine[1]);
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
                else if (splitLine[0] == "UseCoachSim")
                {
                    coachSim.Checked = true;
                }
                else if (splitLine[0] == "UseCoachGame")
                {
                    coachGame.Checked = true;
                }
                else if (splitLine[0] == "SimOffRP")
                {
                    simOffRPSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "GameOffRP")
                {
                    gameOffRPSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SimOffAgg")
                {
                    simOffAggSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "GameOffAgg")
                {
                    gameOffAggSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SimHB")
                {
                    simHBSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "GameHB")
                {
                    gameHBSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SimDefRP")
                {
                    simDefRPSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "GameDefRP")
                {
                    gameDefRPSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SimDefAgg")
                {
                    simDefAggSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "GameDefAgg")
                {
                    gameDefAggSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "MaxOffAdjust")
                {
                    offMaxAdjustSlider.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "MaxDefAdjust")
                {
                    defMaxAdjustSlider.Value = Int32.Parse(splitLine[1]);
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

            if (coachGame.Checked)
            {
                sw.WriteLine("UseCoachGame");
            }

            if (coachSim.Checked)
            {
                sw.WriteLine("UseCoachSim");
            }

            if (fixSimEngine.Checked)
            {
                sw.WriteLine("FixSimEngine");
            }

            sw.WriteLine("Fumbles\t" + fumbleSlider.Value);
            sw.WriteLine("QBAccuracy\t" + accuracySlider.Value);
            sw.WriteLine("QBInjury\t" + qbInjurySlider.Value);
            sw.WriteLine("RESacks\t" + reSacksSlider.Value);
            sw.WriteLine("Stamina\t" + staminaSlider.Value);

            sw.WriteLine("SpeedSpread\t" + speedSpreadSlider.Value);
            sw.WriteLine("FixedSpeed\t" + fixedSpeedSlider.Value);
            sw.WriteLine("HeightSpread\t" + heightSpreadSlider.Value);
            sw.WriteLine("FixedHeight\t" + fixedHeightSlider.Value);
            sw.WriteLine("WeightSpread\t" + weightSpreadSlider.Value);
            sw.WriteLine("FixedWeight\t" + fixedWeightSlider.Value);

            sw.WriteLine("SimOffRP\t" + simOffRPSlider.Value);
            sw.WriteLine("SimOffAgg\t" + simOffAggSlider.Value);
            sw.WriteLine("SimHB\t" + simHBSlider.Value);
            sw.WriteLine("SimDefRP\t" + simDefRPSlider.Value);
            sw.WriteLine("SimDefAgg\t" + simDefAggSlider.Value);
            sw.WriteLine("MaxOffAdjust\t" + offMaxAdjustSlider.Value);
            sw.WriteLine("MaxDefAdjust\t" + defMaxAdjustSlider.Value);

            sw.WriteLine("GameOffRP\t" + gameOffRPSlider.Value);
            sw.WriteLine("GameOffAgg\t" + gameOffAggSlider.Value);
            sw.WriteLine("GameHB\t" + gameHBSlider.Value);
            sw.WriteLine("GameDefRP\t" + gameDefRPSlider.Value);
            sw.WriteLine("GameDefAgg\t" + gameDefAggSlider.Value);

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
/*
            int HCOffRP = 0;
            int HCOffAgg = 0;
            int HCDefRP = 0;
            int HCDefAgg = 0;

            int OffRP = 0;
            int OffAgg = 0;
            int DefRP = 0;
            int DefAgg = 0;

            int numCoaches = 0;

            foreach (TableRecordModel record in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                CoachRecord coach = (CoachRecord)record;

                if (!(coach.TeamId >= 0 && coach.TeamId < 32))
                    continue;
                
                numCoaches++;

                OffRP += coach.OffensiveStrategy;
                OffAgg += coach.OffensiveAggression;
                DefRP += coach.DefensiveStrategy;
                DefAgg += coach.DefensiveAggression;

                if (coach.Position == 0)
                {
                    HCOffRP += coach.OffensiveStrategy;
                    HCOffAgg += coach.OffensiveAggression;
                    HCDefRP += coach.DefensiveStrategy;
                    HCDefAgg += coach.DefensiveAggression;
                }
            }
*/
            DialogResult dr = MessageBox.Show("Do weekly maintenance?", "Continue?", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            this.Invalidate(true);
            this.Update();
            Cursor.Current = Cursors.WaitCursor;
            
            int currentWeek = model.FranchiseTime.Week;

            /*
            int currentWeek = 30;
            foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (record.State.Id == 1 && record.WeekNumber < currentWeek)
                {
                    currentWeek = record.WeekNumber;
                }
            }
             */


            // Fix the previous week's statistics if so desired
            if (fixSimEngine.Checked && currentWeek > 0)
                RepairWeek(currentWeek);

            /*
                Dictionary<int, int> previousOpponents = new Dictionary<int, int>();
                Random rand = new Random();
                List<int> humanControlled = new List<int>();
                int currentSeason = model.FranchiseTime.Season;

                // season and career stats indexed by PGID
                Dictionary<int, SeasonStatsOffenseRecord> seasonStatsOffense = new Dictionary<int, SeasonStatsOffenseRecord>();
                Dictionary<int, SeasonStatsDefenseRecord> seasonStatsDefense = new Dictionary<int, SeasonStatsDefenseRecord>();
                Dictionary<int, CareerStatsOffenseRecord> careerStatsOffense = new Dictionary<int, CareerStatsOffenseRecord>();
                Dictionary<int, CareerStatsDefenseRecord> careerStatsDefense = new Dictionary<int, CareerStatsDefenseRecord>();
                Dictionary<int, TeamStatsRecord> teamStats = new Dictionary<int, TeamStatsRecord>();

                foreach (SeasonStatsDefenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
                {
                    if (stat.Season == currentSeason)
                        seasonStatsDefense[stat.PlayerId] = stat;
                }

                foreach (SeasonStatsOffenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
                {
                    if (stat.Season == currentSeason)
                        seasonStatsOffense[stat.PlayerId] = stat;
                }

                foreach (CareerStatsDefenseRecord stat in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
                    careerStatsDefense[stat.PlayerId] = stat;

                foreach (CareerStatsOffenseRecord stat in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
                    careerStatsOffense[stat.PlayerId] = stat;

                foreach (TeamStatsRecord stat in model.TableModels[EditorModel.TEAM_STATS_TABLE].GetRecords())
                    teamStats[stat.TeamId] = stat;

                foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
                {
                    if (record.WeekNumber == currentWeek - 1 && record.HumanControlled)
                    {
                        humanControlled.Add(record.AwayTeam.TeamId);
                        humanControlled.Add(record.HomeTeam.TeamId);
                        continue;
                    }

                    if (record.WeekNumber == currentWeek - 1)
                    {
                        previousOpponents[record.AwayTeam.TeamId] = record.HomeTeam.TeamId;
                        previousOpponents[record.HomeTeam.TeamId] = record.AwayTeam.TeamId;
                    }
                }

                Dictionary<int, List<BoxScoreOffenseRecord>> interceptees = new Dictionary<int,List<BoxScoreOffenseRecord>>();
                Dictionary<int, List<BoxScoreOffenseRecord>> sackees = new Dictionary<int, List<BoxScoreOffenseRecord>>();
                Dictionary<int, List<BoxScoreOffenseRecord>> OLsackees = new Dictionary<int, List<BoxScoreOffenseRecord>>();
                Dictionary<int, List<BoxScoreOffenseRecord>> fumblers = new Dictionary<int, List<BoxScoreOffenseRecord>>();
                
                Dictionary<int, BoxScoreOffenseRecord> maxPassers = new Dictionary<int, BoxScoreOffenseRecord>();
                Dictionary<int, BoxScoreOffenseRecord> startingRBs = new Dictionary<int, BoxScoreOffenseRecord>();
                Dictionary<int, BoxScoreOffenseRecord> backupRBs = new Dictionary<int, BoxScoreOffenseRecord>();

                Dictionary<int, int> startingRBIDs = new Dictionary<int, int>();
                Dictionary<int, int> backupRBIDs = new Dictionary<int, int>();

                foreach (DepthChartRecord dcr in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
                {
                    if (dcr.PositionId == (int)MaddenPositions.HB)
                    {
                        if (dcr.DepthOrder == 0)
                            startingRBIDs[dcr.TeamId] = dcr.PlayerId;
                        else if (dcr.DepthOrder == 1)
                            backupRBIDs[dcr.TeamId] = dcr.PlayerId;
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    interceptees[i] = new List<BoxScoreOffenseRecord>();
                    fumblers[i] = new List<BoxScoreOffenseRecord>();
                    sackees[i] = new List<BoxScoreOffenseRecord>();
                }

                foreach (BoxScoreOffenseRecord stat in model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].GetRecords())
                {
                    if (stat.Week == currentWeek - 1 && !humanControlled.Contains(stat.TeamId) && stat.Season == currentSeason)
                    {
                        if (startingRBIDs.ContainsValue(stat.PlayerId))
                            startingRBs[stat.TeamId] = stat;
                        else if (backupRBIDs.ContainsValue(stat.PlayerId))
                            backupRBs[stat.TeamId] = stat;

                        if (stat.Interceptions > 0)
                            interceptees[stat.TeamId].Add(stat);

                        if (stat.Fumbles > 0)
                            fumblers[stat.TeamId].Add(stat);

                        if (stat.Sacks > 0)
                            sackees[stat.TeamId].Add(stat);

                        if (stat.PassingYards > 0)
                        {
                            if (!maxPassers.ContainsKey(stat.TeamId) || maxPassers[stat.TeamId].PassingYards < stat.PassingYards)
                                maxPassers[stat.TeamId] = stat;
                        }
                    }
                }

                /*
                 * // fix sack yardage lost
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < sackees[i].Count; j++)
                    {
                        int yardsToSubtract = (int)Math.Round((double)sackees[i][j]
                    }
                }
                 * */
            /*
                // fix defense
                foreach (BoxScoreDefenseRecord stat in model.TableModels[EditorModel.BOXSCORE_DEFENSE_TABLE].GetRecords())
                {
                    if (stat.Week != currentWeek - 1 || humanControlled.Contains(stat.TeamId) || stat.Season != currentSeason)
                        continue;

                    for (int i = 0; i < stat.Interceptions; i++)
                    {
                        if (rand.NextDouble() > interceptionsProbability)
                        {
                            // should also subtract from int yards, long int, defensive TDs?
                            stat.Interceptions--;
                            seasonStatsDefense[stat.PlayerId].Interceptions--;
                            careerStatsDefense[stat.PlayerId].Interceptions--;
                            
                            int subtractFrom = rand.Next(interceptees[previousOpponents[stat.TeamId]].Count);
                            int QBPGID = interceptees[previousOpponents[stat.TeamId]][subtractFrom].PlayerId;
                            interceptees[previousOpponents[stat.TeamId]][subtractFrom].Interceptions--;

                            if (interceptees[previousOpponents[stat.TeamId]][subtractFrom].Interceptions == 0)
                                interceptees[previousOpponents[stat.TeamId]].RemoveAt(subtractFrom);

                            seasonStatsOffense[QBPGID].Interceptions--;
                            careerStatsOffense[QBPGID].Interceptions--;

                            teamStats[stat.TeamId].InterceptionsCaught--;
                            teamStats[previousOpponents[stat.TeamId]].InterceptionsThrown--;
                        }
                    }

                    for (int i = 0; i < stat.Sacks; i++)
                    {
                        if (rand.NextDouble() > sackProbability)
                        {
                            stat.Sacks--;
                            seasonStatsDefense[stat.PlayerId].Sacks--;
                            careerStatsDefense[stat.PlayerId].Sacks--;


                            int subtractFrom = rand.Next(sackees[previousOpponents[stat.TeamId]].Count);
                            int QBPGID = sackees[previousOpponents[stat.TeamId]][subtractFrom].PlayerId;

                            sackees[previousOpponents[stat.TeamId]][subtractFrom].Sacks--;
                            if (sackees[previousOpponents[stat.TeamId]][subtractFrom].Sacks == 0)
                                sackees[previousOpponents[stat.TeamId]].RemoveAt(subtractFrom);

                            seasonStatsOffense[QBPGID].Sacks--;
                            careerStatsOffense[QBPGID].Sacks--;

                            teamStats[stat.TeamId].Sacks--;
                            teamStats[previousOpponents[stat.TeamId]].SacksAllowed--;

                            // we need to add back about 9 yards for every sack we take away.
                            teamStats[previousOpponents[stat.TeamId]].PassingYards += 9;
                            teamStats[previousOpponents[stat.TeamId]].TotalOffense += 9;
                            teamStats[previousOpponents[stat.TeamId]].TotalYards += 9;
                        }
                    }
                }

                // fix passing yards and rushing yards / attempts
                foreach (BoxScoreOffenseRecord stat in model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].GetRecords())
                {
                    if (stat.Week != currentWeek - 1 || humanControlled.Contains(stat.TeamId) || stat.Season != currentSeason ||
                        model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId).PositionId < (int)MaddenPositions.HB ||
                        model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId).PositionId > (int)MaddenPositions.TE)
                        continue;

                    int yardsToAdd = (int)Math.Round((double)stat.ReceivingYards * (passingYardsMultiplier - 1.0));
                    stat.ReceivingYards += yardsToAdd;
                    seasonStatsOffense[stat.PlayerId].ReceivingYards += yardsToAdd;
                    careerStatsOffense[stat.PlayerId].ReceivingYards += yardsToAdd;

                    maxPassers[stat.TeamId].PassingYards += yardsToAdd;
                    seasonStatsOffense[maxPassers[stat.TeamId].PlayerId].PassingYards += yardsToAdd;
                    careerStatsOffense[maxPassers[stat.TeamId].PlayerId].PassingYards += yardsToAdd;

                    teamStats[stat.TeamId].TotalYards += yardsToAdd;
                    teamStats[stat.TeamId].TotalOffense += yardsToAdd;
                    teamStats[stat.TeamId].PassingYards += yardsToAdd;
                    teamStats[previousOpponents[stat.TeamId]].PassingYardsAllowed += yardsToAdd;

                    int rushAttemptsSubtracted = (int)Math.Round((double)stat.RushingAttempts * (1.0 - rushingAttemptsMultiplier));
                    stat.RushingAttempts -= rushAttemptsSubtracted;
                    seasonStatsOffense[stat.PlayerId].RushingAttempts -= rushAttemptsSubtracted;
                    careerStatsOffense[stat.PlayerId].RushingAttempts -= rushAttemptsSubtracted;

                    int rushYardsToAdd = (int)Math.Round((double)stat.RushingYards * (rushingYardsMultiplier - 1.0));
                    stat.RushingYards += rushYardsToAdd;
                    seasonStatsOffense[stat.PlayerId].RushingYards += rushYardsToAdd;
                    careerStatsOffense[stat.PlayerId].RushingYards += rushYardsToAdd;

                    teamStats[stat.TeamId].TotalYards += rushYardsToAdd;
                    teamStats[stat.TeamId].TotalOffense += rushYardsToAdd;
                    teamStats[stat.TeamId].RushingYards += rushYardsToAdd;
                    teamStats[previousOpponents[stat.TeamId]].RushingYards += rushYardsToAdd;
                }

                // fix RB stats
                for (int i = 0; i < 32; i++)
                {
                    // this shouldn't really happen if they're using Weekly
                    // Maintenance properly, but this at least prevents
                    // an exception.
                    if (!startingRBs.ContainsKey(i))
                        continue;

                    double HB2frac = 1.0 - ((double)RBSlider(i)) / 100.0;
                    double yardageMultipler = ((double)model.PlayerModel.GetPlayerByPlayerId(backupRBIDs[i]).Overall / (double)model.PlayerModel.GetPlayerByPlayerId(startingRBIDs[i]).Overall);

                    int carries = (int)Math.Round(HB2frac * (double)startingRBs[i].RushingAttempts);
                    int yards = (int)Math.Round(HB2frac * (double)startingRBs[i].RushingYards);

                    startingRBs[i].RushingAttempts -= carries;
                    startingRBs[i].RushingYards -= yards;
                    seasonStatsOffense[startingRBs[i].PlayerId].RushingAttempts -= carries;
                    seasonStatsOffense[startingRBs[i].PlayerId].RushingYards -= yards;
                    careerStatsOffense[startingRBs[i].PlayerId].RushingAttempts -= carries;
                    careerStatsOffense[startingRBs[i].PlayerId].RushingYards -= yards;

                    teamStats[i].TotalYards -= yards;
                    teamStats[i].TotalOffense -= yards;
                    teamStats[i].RushingYards -= yards;
                    teamStats[previousOpponents[i]].RushingYardsAllowed -= yards;

                    BoxScoreOffenseRecord backupRBBox = null;
                    SeasonStatsOffenseRecord backupRBSeason = null;
                    CareerStatsOffenseRecord backupRBCareer = null;
                    
                    if (backupRBs.ContainsKey(i))
                        backupRBBox = backupRBs[i];
                    else
                    {
                        backupRBBox = (BoxScoreOffenseRecord)model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBBox.PlayerId = backupRBIDs[i];
                        backupRBBox.TeamId = i;
                        backupRBBox.Week = currentWeek - 1;
                        backupRBBox.Season = currentSeason;
                    }

                    if (seasonStatsOffense.ContainsKey(backupRBIDs[i]))
                        backupRBSeason = seasonStatsOffense[backupRBIDs[i]];
                    else
                    {
                        backupRBSeason = (SeasonStatsOffenseRecord)model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBSeason.PlayerId = backupRBIDs[i];
                        backupRBSeason.Season = currentSeason;
                    }

                    if (careerStatsOffense.ContainsKey(backupRBIDs[i]))
                        backupRBCareer = careerStatsOffense[backupRBIDs[i]];
                    else
                    {
                        backupRBCareer = (CareerStatsOffenseRecord)model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBCareer.PlayerId = backupRBIDs[i];
                    }

                    backupRBBox.RushingAttempts += carries;
                    backupRBSeason.RushingAttempts += carries;
                    backupRBCareer.RushingAttempts += carries;

                    int backupYards = (int)Math.Round((double)yards * yardageMultipler);
                    backupRBBox.RushingYards += backupYards;
                    backupRBSeason.RushingYards += backupYards;
                    backupRBCareer.RushingYards += backupYards;

                    teamStats[i].TotalYards += backupYards;
                    teamStats[i].TotalOffense += backupYards;
                    teamStats[i].RushingYards += backupYards;
                    teamStats[previousOpponents[i]].RushingYardsAllowed += backupYards;
                }
            }
            */

            if (currentWeek != 6 || currentWeek != 12 || !(currentWeek > 17))
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

            foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (record.WeekNumber == currentWeek)
                {
                    if (record.HumanControlled)
                    {
                        toAdjust.Add(record.AwayTeam.TeamId);
                        toAdjust.Add(record.HomeTeam.TeamId);
                    }

                    opponents.Add(record.HomeTeam.TeamId, record.AwayTeam.TeamId);
                    opponents.Add(record.AwayTeam.TeamId, record.HomeTeam.TeamId);
                }
            }

            List<List<double>> squadRatings = null;
            if (coachGame.Checked || coachSim.Checked)
            {
                squadRatings = CalculateSquadRatings();

                for (int j = 0; j < 4; j++)
                {
                    double max=0;
                    double min=0;

                    /*
                    for (int i = 0; i < 32; i++)
                    {
                        max = Math.Max(squadRatings[i][j], max);
                        min = Math.Min(squadRatings[i][j], min);
                    }
                    */

                    switch (j)
                    {
                        case 0:
                            max = 2340;
                            min = 2040;
                            break;
                        case 1:
                            max = 2320;
                            min = 2080;
                            break;
                        case 2:
                            max = 2730;
                            min = 2430;
                            break;
                        case 3:
                            max = 2400;
                            min = 2060;
                            break;
                    }

                    for (int i = 0; i < 32; i++)
                    {
                        squadRatings[i][j] = Math.Min(Math.Max(10 * (squadRatings[i][j] - min) / (max - min), 0), 10);
                    }
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

            if (useSliders.Checked || usePhysicalSliders.Checked)
            {
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

                        if (staminaSlider.Value < 50)
                        {
                            player.Stamina -= (int)Math.Round((50.0 - (double)staminaSlider.Value) * ((double)player.Stamina / 50.0));
                        }
                        else
                        {
                            player.Stamina += (int)Math.Min((99 - player.Stamina), Math.Round(((double)staminaSlider.Value - 50.0) * ((double)player.Stamina / 50.0)));
                        }

                        if (leftTackles.Contains(player.PlayerId))
                        {
                            if (reSacksSlider.Value > 50)
                            {
                                // Subtract pass blocking depending on the extent
                                // to which the RE is a "speed rusher".
                                player.PassBlocking -= (int)Math.Round(rightEnds[opponents[player.TeamId]] * ((double)reSacksSlider.Value - 50.0) * ((double)player.PassBlocking / 50.0));
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

                        if ((weightSpreadSlider.Value > 50 && (player.Weight + 160) > fixedWeightSlider.Value) || (weightSpreadSlider.Value < 50 && (player.Weight + 160) < fixedWeightSlider.Value))
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
            }

            Console.WriteLine("OP: " + squadRatings[19][(int)MaddenSquadRatings.OffensivePassing]);
            Console.WriteLine("OR: " + squadRatings[19][(int)MaddenSquadRatings.OffensiveRunning]);
            Console.WriteLine("DP: " + squadRatings[19][(int)MaddenSquadRatings.DefensivePassing]);
            Console.WriteLine("DR: " + squadRatings[19][(int)MaddenSquadRatings.DefensiveRunning]);

            if (coachSim.Checked || coachGame.Checked)
            {
                foreach (CoachRecord coach in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
                {
                    if (!opponents.ContainsKey(coach.TeamId))
                        continue;

                    int offRPbase;
                    int offAggbase;
                    int defRPbase;
                    int defAggbase;

                    if (toAdjust.Contains(coach.TeamId) && coachGame.Checked)
                    {
                        offRPbase = gameOffRPSlider.Value;
                        offAggbase = gameOffAggSlider.Value;
                        defRPbase = gameDefRPSlider.Value;
                        defAggbase = gameDefAggSlider.Value;
                    }
                    else if (!toAdjust.Contains(coach.TeamId) && coachSim.Checked)
                    {
                        offRPbase = simOffRPSlider.Value;
                        offAggbase = simOffAggSlider.Value;
                        defRPbase = simDefRPSlider.Value;
                        defAggbase = simDefAggSlider.Value;
                    }
                    else
                        continue;

                    coach.OffensiveStrategy = (int)Math.Round(Math.Max(Math.Min((double)offRPbase + ((double)offMaxAdjustSlider.Value)*(squadRatings[coach.TeamId][(int)MaddenSquadRatings.OffensivePassing] - squadRatings[coach.TeamId][(int)MaddenSquadRatings.OffensiveRunning] - squadRatings[opponents[coach.TeamId]][(int)MaddenSquadRatings.DefensivePassing] + squadRatings[opponents[coach.TeamId]][(int)MaddenSquadRatings.DefensiveRunning]) / 20.0, 79), 21));
                    coach.DefensiveStrategy = (int)Math.Round(Math.Max(Math.Min((double)defRPbase + ((double)defMaxAdjustSlider.Value)*(squadRatings[opponents[coach.TeamId]][(int)MaddenSquadRatings.OffensivePassing] - squadRatings[opponents[coach.TeamId]][(int)MaddenSquadRatings.OffensiveRunning]) / 20.0, 79), 21));

                    coach.OffensiveAggression = offAggbase;
                    coach.DefensiveAggression = defAggbase;
                    coach.RunningBack2Sub = 60 + (int)Math.Min(Math.Max(40 * Math.Tanh((squadRatings[coach.TeamId][4] - squadRatings[coach.TeamId][5]) / 12), 0), 30);

                    /*
                    coach.OffensiveStrategy = 53;
                    coach.OffensiveAggression = 53;
                    coach.DefensiveStrategy = 50;
                    coach.DefensiveAggression = 51;

                    if (coach.Position == (int)MaddenCoachPosition.HeadCoach)
                        Console.WriteLine(model.TeamModel.GetTeamNameFromTeamId(coach.TeamId) + " " + coach.RunningBack2Sub);
                     * */
                }
            }

            Cursor.Current = Cursors.Arrow;
        }

        int RBSlider(int TeamId)
        {
            foreach (CoachRecord coach in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                if (coach.TeamId == TeamId && coach.Position == (int)MaddenCoachPosition.HeadCoach)
                    return coach.RunningBack2Sub;
            }

            return -1;
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

        List<List<double>> CalculateSquadRatings()
        {
            List<List<double>> toReturn = new List<List<double>>();

            for (int i = 0; i < 32; i++)
            {
                toReturn.Add(new List<double>());

                for (int j = 0; j < 6; j++)
                {
                    toReturn[i].Add(0);
                }
            }

            foreach (TableRecordModel record in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
            {
                DepthChartRecord dcrecord = (DepthChartRecord)record;

                if (dcrecord.TeamId > 31 || (dcrecord.DepthOrder > 0 &&
                    !(dcrecord.PositionId == (int)MaddenPositions.HB ||
                    dcrecord.PositionId == (int)MaddenPositions.WR ||
                    dcrecord.PositionId == (int)MaddenPositions.CB)))
                    continue;

                PlayerRecord player = model.PlayerModel.GetPlayerByPlayerId(dcrecord.PlayerId);

                if (dcrecord.PositionId == (int)MaddenPositions.HB)
                {
                    if (dcrecord.DepthOrder > 1)
                        continue;

                    toReturn[dcrecord.TeamId][dcrecord.DepthOrder + 4] = player.Overall;
                }

                switch (dcrecord.PositionId)
                {
                    case (int)MaddenPositions.QB:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensivePassing] += 5.0 * (double)player.Overall;
                        break;
                    case (int)MaddenPositions.HB:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensivePassing] += (double)player.Overall / Math.Pow((1.0 + (double)dcrecord.DepthOrder), 2);
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensiveRunning] += 5.0 * (double)player.Overall / (1.0 + (double)dcrecord.DepthOrder);
                        break;
                    case (int)MaddenPositions.FB:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensiveRunning] += 2.0 * (double)player.Overall;
                        break;
                    case (int)MaddenPositions.WR:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensivePassing] += 3.0 * (double)player.Overall / Math.Pow(Math.Max(1.0, (double)dcrecord.DepthOrder), 2);
                        break;
                    case (int)MaddenPositions.TE:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensivePassing] += 2.0 * ((double)player.Overall - 5 + (player.Tendancy == 1 ? 10.0 : 0) - (player.Tendancy == 0 ? 5.0 : 0));
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensiveRunning] += ((double)player.Overall - (player.Tendancy == 1 ? 5.0 : 0) + (player.Tendancy == 0 ? 5.0 : 0));
                        break;
                    case (int)MaddenPositions.LT:
                    case (int)MaddenPositions.LG:
                    case (int)MaddenPositions.C:
                    case (int)MaddenPositions.RG:
                    case (int)MaddenPositions.RT:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensivePassing] += 2.0 * ((double)player.Overall + (player.Tendancy == 1 ? 5.0 : 0) - (player.Tendancy == 0 ? 5.0 : 0));
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.OffensiveRunning] += 3.0 * ((double)player.Overall - (player.Tendancy == 1 ? 5.0 : 0) + (player.Tendancy == 0 ? 5.0 : 0));
                        break;
                    case (int)MaddenPositions.LE:
                    case (int)MaddenPositions.RE:
                    case (int)MaddenPositions.DT:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensiveRunning] += 4.0 * ((double)player.Overall + (player.Tendancy == 1 ? 10.0 : 0) - (player.Tendancy == 0 ? 10.0 : 0));
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensivePassing] += 3.0 * ((double)player.Overall - (player.Tendancy == 1 ? 10.0 : 0) + (player.Tendancy == 0 ? 10.0 : 0));
                        break;
                    case (int)MaddenPositions.LOLB:
                    case (int)MaddenPositions.MLB:
                    case (int)MaddenPositions.ROLB:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensiveRunning] += 4.0 * ((double)player.Overall + (player.Tendancy == 1 ? 5.0 : 0) - (player.Tendancy == 0 ? 5.0 : 0));
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensivePassing] += ((double)player.Overall - (player.Tendancy == 1 ? 5.0 : 0) + (player.Tendancy == 0 ? 5.0 : 0));
                        break;
                    case (int)MaddenPositions.CB:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensivePassing] += 5.0 * (double)player.Overall / Math.Pow(Math.Max(1.0, (double)dcrecord.DepthOrder), 2);
                        break;
                    case (int)MaddenPositions.FS:
                    case (int)MaddenPositions.SS:
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensiveRunning] += (double)player.Overall;
                        toReturn[dcrecord.TeamId][(int)MaddenSquadRatings.DefensivePassing] += 3.0 * (double)player.Overall;
                        break;
                }
            }

            return toReturn;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            dirty = true;

            if (sender == fixSimEngine)
            {
                if (fixSimEngine.Checked)
                    fixHumanPancakes.Enabled = true;
                else
                {
                    fixHumanPancakes.Enabled = false;
                    fixHumanPancakes.Checked = false;
                }
            }
        }

        private void dumpStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentSeason = model.FranchiseTime.Season;

            int passingOffense = 0;
            int rushingOffense = 0;
            int passingYards = 0;
            int sacks = 0;
            int interceptions = 0;
            int passingAttempts = 0;
            int rushingAttempts = 0;
            int fumbles = 0;
            int fumblesRecovered = 0;
            int fumblesForced = 0;
            int firstDowns = 0;
            int thirdDownAttempts = 0;
            int thirdDownConversions = 0;
            int fourthDownAttempts = 0;
            int fourthDownConversions = 0;
            int passesDefended = 0;

            List<int> top10passingYards = new List<int>();
            List<int> top10rushingYards = new List<int>();
            List<int> top10rushingAttempts = new List<int>();
            List<double> top10rushingAverage = new List<double>();
            List<int> top10teamSacks = new List<int>();

            foreach (SeasonStatsTeamRecord stat in model.TableModels[EditorModel.TEAM_STATS_TABLE].GetRecords())
            {
                passingOffense += stat.PassingYards;
                rushingOffense += stat.RushingYards;
                sacks += stat.Sacks;
                interceptions += stat.InterceptionsCaught;
                fumblesRecovered += stat.FumblesRecovered;
                firstDowns += stat.FirstDowns;
                thirdDownAttempts += stat.ThirdDownAttempts;
                thirdDownConversions += stat.ThirdDownConversions;
                fourthDownAttempts += stat.FourthDownAttempts;
                fourthDownConversions += stat.FourthDownConversions;

                if (top10teamSacks.Count < 10)
                {
                    top10teamSacks.Add(stat.Sacks);
                    top10teamSacks.Sort();
                }
                else if (top10teamSacks[0] < stat.Sacks)
                {
                    top10teamSacks.Add(stat.Sacks);
                    top10teamSacks.Sort();
                    top10teamSacks.RemoveAt(0);
                }
            }

            foreach (SeasonStatsDefenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
            {
                if (stat.Season != currentSeason)
                    continue;

                fumblesForced += stat.FumblesForced;
                passesDefended += stat.PassesDefended;
            }

            foreach (SeasonStatsOffenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
            {
                if (stat.Season != currentSeason)
                    continue;

                passingYards += stat.PassingYards;
                passingAttempts += stat.PassingAttempts;
                rushingAttempts += stat.RushingAttempts;
                fumbles += stat.Fumbles;

                if (top10passingYards.Count < 10)
                {
                    top10passingYards.Add(stat.PassingYards);
                    top10passingYards.Sort();
                }
                else if (top10passingYards[0] < stat.PassingYards)
                {
                    top10passingYards.Add(stat.PassingYards);
                    top10passingYards.Sort();
                    top10passingYards.RemoveAt(0);
                }

                if (top10rushingYards.Count < 10)
                {
                    top10rushingYards.Add(stat.RushingYards);
                    top10rushingYards.Sort();
                }
                else if (top10rushingYards[0] < stat.RushingYards)
                {
                    top10rushingYards.Add(stat.RushingYards);
                    top10rushingYards.Sort();
                    top10rushingYards.RemoveAt(0);
                }

                if (top10rushingAttempts.Count < 10)
                {
                    top10rushingAttempts.Add(stat.RushingAttempts);
                    top10rushingAttempts.Sort();
                }
                else if (top10rushingAttempts[0] < stat.RushingAttempts)
                {
                    top10rushingAttempts.Add(stat.RushingAttempts);
                    top10rushingAttempts.Sort();
                    top10rushingAttempts.RemoveAt(0);
                }

                if (stat.PassingAttempts < 25 && stat.RushingAttempts > 100 && top10rushingAverage.Count < 10)
                {
                    top10rushingAverage.Add((double)stat.RushingYards / (double)stat.RushingAttempts);
                    top10rushingAverage.Sort();
                }
                else if (stat.PassingAttempts < 25 && stat.RushingAttempts > 100 && top10rushingAverage[0] < (double)stat.RushingYards / (double)stat.RushingAttempts)
                {
                    top10rushingAverage.Add((double)stat.RushingYards / (double)stat.RushingAttempts);
                    top10rushingAverage.Sort();
                    top10rushingAverage.RemoveAt(0);
                }
            }

            StreamWriter sw = new StreamWriter(installDirectory + "\\stats_output.txt");

            sw.WriteLine("Total Offense: " + (passingOffense + rushingOffense));
            sw.WriteLine("\tPassing Offense: " + passingOffense);
            sw.WriteLine("\tRushing Offense: " + rushingOffense);
            sw.WriteLine("1st Downs: " + firstDowns);
            sw.WriteLine("3rd Down Attempts: " + thirdDownAttempts);
            sw.WriteLine("3rd Down Conversions: " + thirdDownConversions);
            sw.WriteLine("4th Down Attempts: " + fourthDownAttempts);
            sw.WriteLine("4th Down Conversions: " + fourthDownConversions);
            sw.WriteLine("Passing Attempts: " + passingAttempts);
            sw.WriteLine("Rushing Attempts: " + rushingAttempts);
            sw.WriteLine("Passing Yards: " + passingYards);
            sw.WriteLine("Sacks: " + sacks);
            sw.WriteLine("Sack Yards Lost: " + (passingYards - passingOffense));
            sw.WriteLine("Interceptions: " + interceptions);
            sw.WriteLine("Passes Defended: " + passesDefended);
            sw.WriteLine("Fumbles: " + fumbles);
            sw.WriteLine("Fumbles Forced: " + fumblesForced);
            sw.WriteLine("Fumbles Recovered: " + fumblesRecovered);

            sw.WriteLine("\nTop 10 Passers:\n");
            for (int i = 9; i >= 0; i--)
                sw.WriteLine((10-i) + ". " + top10passingYards[i]);

            sw.WriteLine("\nTop 10 Rushing Attempts:\n");
            for (int i = 9; i >= 0; i--)
            {
                PlayerRecord player = null;
                foreach (SeasonStatsOffenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
                {
                    if (stat.Season != currentSeason)
                        continue;

                    if (stat.RushingAttempts == top10rushingAttempts[i])
                    {
                        player = model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId);
                        break;
                    }
                }

                sw.WriteLine((10 - i) + ". " + top10rushingAttempts[i] + " (" + RBSlider(player.TeamId) + ")");
            }

            sw.WriteLine("\nTop 10 Rushing Yards:\n");
            for (int i = 9; i >= 0; i--)
                sw.WriteLine((10 - i) + ". " + top10rushingYards[i]);

            sw.WriteLine("\nTop 10 Rushing Average:\n");
            for (int i = top10rushingAverage.Count-1; i >= 0; i--)
                sw.WriteLine((10 - i) + ". " + Math.Round(top10rushingAverage[i], 2));

            sw.WriteLine("\nTop 10 Team Sacks:\n");
            for (int i = 9; i >= 0; i--)
                sw.WriteLine((10 - i) + ". " + top10teamSacks[i]);

            sw.Close();
        }

        private void fixAllStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < model.FranchiseTime.Week; i++)
                RepairWeek(i);
        }

        private void RepairWeek(int currentWeek)
        {
            int currentSeason = model.FranchiseTime.Season;

            bool preseason = true;
            foreach (ScheduleRecord rec in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (rec.WeekNumber > 3)
                {
                    preseason = false;
                    break;
                }
            }

            Dictionary<int, int> previousOpponents = new Dictionary<int, int>();
            List<int> humanControlled = new List<int>();

            // season and career stats indexed by PGID
            Dictionary<int, SeasonStatsOffenseRecord> seasonStatsOffense = new Dictionary<int, SeasonStatsOffenseRecord>();
            Dictionary<int, SeasonStatsDefenseRecord> seasonStatsDefense = new Dictionary<int, SeasonStatsDefenseRecord>();
            Dictionary<int, SeasonStatsOffensiveLineRecord> seasonStatsOffensiveLine = new Dictionary<int,SeasonStatsOffensiveLineRecord>();
            Dictionary<int, CareerStatsOffenseRecord> careerStatsOffense = new Dictionary<int, CareerStatsOffenseRecord>();
            Dictionary<int, CareerStatsDefenseRecord> careerStatsDefense = new Dictionary<int, CareerStatsDefenseRecord>();
            Dictionary<int, CareerStatsOffensiveLineRecord> careerStatsOffensiveLine = new Dictionary<int,CareerStatsOffensiveLineRecord>();
            Dictionary<int, SeasonStatsTeamRecord> teamStats = new Dictionary<int, SeasonStatsTeamRecord>();
            Dictionary<int, BoxScoreTeamRecord> teamBoxScores = new Dictionary<int, BoxScoreTeamRecord>();

            foreach (SeasonStatsDefenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
            {
                if (stat.Season == currentSeason)
                    seasonStatsDefense[stat.PlayerId] = stat;
            }

            foreach (SeasonStatsOffenseRecord stat in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
            {
                if (stat.Season == currentSeason)
                    seasonStatsOffense[stat.PlayerId] = stat;
            }

            foreach (SeasonStatsOffensiveLineRecord stat in model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
            {
                if (stat.Season == currentSeason)
                    seasonStatsOffensiveLine[stat.PlayerId] = stat;
            }

            foreach (CareerStatsDefenseRecord stat in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
                careerStatsDefense[stat.PlayerId] = stat;

            foreach (CareerStatsOffenseRecord stat in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
                careerStatsOffense[stat.PlayerId] = stat;

            foreach (CareerStatsOffensiveLineRecord stat in model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                careerStatsOffensiveLine[stat.PlayerId] = stat;

            foreach (SeasonStatsTeamRecord stat in model.TableModels[EditorModel.TEAM_STATS_TABLE].GetRecords())
                teamStats[stat.TeamId] = stat;

            foreach (ScheduleRecord record in model.TableModels[EditorModel.SCHEDULE_TABLE].GetRecords())
            {
                if (record.WeekNumber == currentWeek - 1 && record.HumanControlled)
                {
                    humanControlled.Add(record.AwayTeam.TeamId);
                    humanControlled.Add(record.HomeTeam.TeamId);
                    continue;
                }

                if (record.WeekNumber == currentWeek - 1)
                {
                    previousOpponents[record.AwayTeam.TeamId] = record.HomeTeam.TeamId;
                    previousOpponents[record.HomeTeam.TeamId] = record.AwayTeam.TeamId;
                }
            }

            foreach (BoxScoreTeamRecord stat in model.TableModels[EditorModel.BOXSCORE_TEAM_TABLE].GetRecords())
            {
                if (stat.Season == currentSeason && stat.Week == currentWeek - 1 && !humanControlled.Contains(stat.TeamId))
                    teamBoxScores[stat.TeamId] = stat;
            }

            Dictionary<int, List<BoxScoreOffenseRecord>> interceptees = new Dictionary<int, List<BoxScoreOffenseRecord>>();
            Dictionary<int, List<BoxScoreOffenseRecord>> sackees = new Dictionary<int, List<BoxScoreOffenseRecord>>();
            Dictionary<int, List<BoxScoreOffensiveLineRecord>> OLsackees = new Dictionary<int, List<BoxScoreOffensiveLineRecord>>();

            /*
            Dictionary<int, List<BoxScoreDefenseRecord>> forcers = new Dictionary<int, List<BoxScoreDefenseRecord>>();
            Dictionary<int, List<BoxScoreDefenseRecord>> recoverers = new Dictionary<int, List<BoxScoreDefenseRecord>>();
            */

            Dictionary<int, BoxScoreOffenseRecord> maxPassers = new Dictionary<int, BoxScoreOffenseRecord>();
            Dictionary<int, BoxScoreOffenseRecord> startingRBs = new Dictionary<int, BoxScoreOffenseRecord>();
            Dictionary<int, BoxScoreOffenseRecord> backupRBs = new Dictionary<int, BoxScoreOffenseRecord>();
            Dictionary<int, BoxScoreOffenseRecord> startingFBs = new Dictionary<int, BoxScoreOffenseRecord>();

            Dictionary<int, int> startingRBIDs = new Dictionary<int, int>();
            Dictionary<int, int> backupRBIDs = new Dictionary<int, int>();
            Dictionary<int, int> startingFBIDs = new Dictionary<int, int>();

            foreach (DepthChartRecord dcr in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
            {
                if (dcr.PositionId == (int)MaddenPositions.HB)
                {
                    if (dcr.DepthOrder == 0)
                        startingRBIDs[dcr.TeamId] = dcr.PlayerId;
                    else if (dcr.DepthOrder == 1)
                        backupRBIDs[dcr.TeamId] = dcr.PlayerId;
                }
                else if (dcr.PositionId == (int)MaddenPositions.FB)
                {
                    if (dcr.DepthOrder == 0)
                        startingFBIDs[dcr.TeamId] = dcr.PlayerId;
                }
            }

            for (int i = 0; i < 32; i++)
            {
                interceptees[i] = new List<BoxScoreOffenseRecord>();
                sackees[i] = new List<BoxScoreOffenseRecord>();
                OLsackees[i] = new List<BoxScoreOffensiveLineRecord>();
            }

            foreach (BoxScoreOffenseRecord stat in model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].GetRecords())
            {
                if (stat.Week == currentWeek - 1 && !humanControlled.Contains(stat.TeamId) && stat.Season == currentSeason)
                {
                    if (startingRBIDs.ContainsValue(stat.PlayerId))
                        startingRBs[stat.TeamId] = stat;
                    else if (backupRBIDs.ContainsValue(stat.PlayerId))
                        backupRBs[stat.TeamId] = stat;
                    else if (startingFBIDs.ContainsValue(stat.PlayerId))
                        startingFBs[stat.TeamId] = stat;

                    if (stat.Interceptions > 0)
                        interceptees[stat.TeamId].Add(stat);

                    if (stat.Sacks > 0)
                        sackees[stat.TeamId].Add(stat);
                    if (stat.PassingYards > 0)
                    {
                        if (!maxPassers.ContainsKey(stat.TeamId) || maxPassers[stat.TeamId].PassingYards < stat.PassingYards)
                            maxPassers[stat.TeamId] = stat;
                    }
                }
            }

            /*
                foreach (BoxScoreDefenseRecord stat in model.TableModels[EditorModel.BOXSCORE_DEFENSE_TABLE].GetRecords())
                {
                    if (stat.Week == currentWeek - 1 && !humanControlled.Contains(stat.TeamId) && stat.Season == currentSeason)
                    {
                        if (stat.FumblesForced > 0)
                            forcers[stat.TeamId].Add(stat);

                        if (stat.FumblesRecovered > 0)
                            recoverers[stat.TeamId].Add(stat);
                    }
                }

                /*
                 * // fix sack yardage lost
                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < sackees[i].Count; j++)
                    {
                        int yardsToSubtract = (int)Math.Round((double)sackees[i][j]
                    }
                }
                 * */

            foreach (BoxScoreOffensiveLineRecord stat in model.TableModels[EditorModel.BOXSCORE_OFFENSIVE_LINE_TABLE].GetRecords())
            {
                if (stat.Week != currentWeek - 1 || stat.Season != currentSeason)
                    continue;

                if (!humanControlled.Contains(stat.TeamId))
                {
                    if (stat.SacksAllowed > 0)
                        OLsackees[stat.TeamId].Add(stat);
                    continue;
                }

                if (!fixHumanPancakes.Checked)
                    continue;

                int pancakes = stat.Pancakes;
                for (int i = 0; i < pancakes; i++)
                {
                    if (rand.NextDouble() > pancakeProbability)
                    {
                        stat.Pancakes--;

                        if (currentWeek < 19)
                        {
                            seasonStatsOffensiveLine[stat.PlayerId].Pancakes--;
                            if (!preseason)
                                careerStatsOffensiveLine[stat.PlayerId].Pancakes--;
                        }
                    }
                }
            }

                // fix first downs, etc.
                foreach (int i in teamBoxScores.Keys)
                {
                    int firstDownsAdd = (int)Math.Round((double)teamBoxScores[i].FirstDowns * (firstDownMultiplier - 1.0));
                    teamBoxScores[i].FirstDowns += firstDownsAdd;

                    if (currentWeek < 19)
                        teamStats[i].FirstDowns += firstDownsAdd;

                    for (int j = 0; j < teamBoxScores[i].ThirdDownAttempts; j++)
                    {
                        if (rand.NextDouble() > thirdDownAttemptsProbability)
                        {
                            teamBoxScores[i].ThirdDownAttempts--;

                            if (currentWeek < 19)
                                teamStats[i].ThirdDownAttempts--;

                            if (teamBoxScores[i].ThirdDownAttempts < teamBoxScores[i].ThirdDownConversions ||
                                rand.NextDouble() > thirdDownConversionsProbability)
                            {
                                teamBoxScores[i].ThirdDownConversions--;

                                if (currentWeek < 19)
                                    teamStats[i].ThirdDownConversions--;
                            }
                        }
                    }

                    for (int j = 0; j < teamBoxScores[i].FourthDownAttempts; j++)
                    {
                        if (rand.NextDouble() > fourthDownAttemptsProbability)
                        {
                            teamBoxScores[i].FourthDownAttempts--;

                            if (currentWeek < 19)
                                teamStats[i].FourthDownAttempts--;

                            if (teamBoxScores[i].FourthDownAttempts < teamBoxScores[i].FourthDownConversions ||
                                rand.NextDouble() > fourthDownConversionsProbability)
                            {
                                teamBoxScores[i].FourthDownConversions--;

                                if (currentWeek < 19)
                                    teamStats[i].FourthDownConversions--;
                            }
                        }
                    }
                }

                // fix defense
                foreach (BoxScoreDefenseRecord stat in model.TableModels[EditorModel.BOXSCORE_DEFENSE_TABLE].GetRecords())
                {
                    if (stat.Week != currentWeek - 1 || humanControlled.Contains(stat.TeamId) || stat.Season != currentSeason)
                        continue;

                    for (int i = 0; i < stat.PassesDefended; i++)
                    {
                        // this one's easy -- just subtract a pass defended
                        // and the play looks as if the QB just missed the throw.
                        if (rand.NextDouble() > passesDefendedProbability)
                        {
                            stat.PassesDefended--;
                            if (currentWeek < 19)
                            {
                                seasonStatsDefense[stat.PlayerId].PassesDefended--;
                                if (!preseason)
                                    careerStatsDefense[stat.PlayerId].PassesDefended--;
                            }
                        }
                    }

                    for (int i = 0; i < stat.FumblesForced; i++)
                    {
                        if (rand.NextDouble() > fumbleForcedProbability)
                        {
                            stat.FumblesForced--;
                            if (currentWeek < 19)
                            {
                                seasonStatsDefense[stat.PlayerId].FumblesForced--;
                                if (!preseason)
                                    careerStatsDefense[stat.PlayerId].FumblesForced--;
                            }
                        }
                    }

                    if (model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId).PositionId >= (int)MaddenPositions.LE)
                    {
                        int frs = stat.FumblesRecovered;

                        for (int i = 0; i < frs && teamBoxScores[previousOpponents[stat.TeamId]].FumblesLost > 0 && teamBoxScores[stat.TeamId].FumblesRecovered > 0; i++)
                        {
                            if (rand.NextDouble() > fumbleRecoveredProbability)
                            {
                                stat.FumblesRecovered--;
                                teamBoxScores[stat.TeamId].FumblesRecovered--;
                                teamBoxScores[previousOpponents[stat.TeamId]].FumblesLost--;

                                if (currentWeek < 19)
                                {
                                    seasonStatsDefense[stat.PlayerId].FumblesRecovered--;
                                    if (!preseason)
                                        careerStatsDefense[stat.PlayerId].FumblesRecovered--;

                                    teamStats[stat.TeamId].FumblesRecovered--;
                                    teamStats[previousOpponents[stat.TeamId]].FumblesLost--;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < stat.Interceptions; i++)
                    {
                        if (rand.NextDouble() > interceptionsProbability)
                        {
                            // should also subtract from int yards, long int, defensive TDs?
                            stat.Interceptions--;
                            teamBoxScores[stat.TeamId].InterceptionsCaught--;
                            teamBoxScores[previousOpponents[stat.TeamId]].InterceptionsThrown--;

                            if (currentWeek < 19)
                            {
                                seasonStatsDefense[stat.PlayerId].Interceptions--;
                                if (!preseason)
                                    careerStatsDefense[stat.PlayerId].Interceptions--;
                            }

                            int subtractFrom = rand.Next(interceptees[previousOpponents[stat.TeamId]].Count);
                            int QBPGID = interceptees[previousOpponents[stat.TeamId]][subtractFrom].PlayerId;
                            interceptees[previousOpponents[stat.TeamId]][subtractFrom].Interceptions--;

                            if (interceptees[previousOpponents[stat.TeamId]][subtractFrom].Interceptions == 0)
                                interceptees[previousOpponents[stat.TeamId]].RemoveAt(subtractFrom);

                            if (currentWeek < 19)
                            {
                                seasonStatsOffense[QBPGID].Interceptions--;
                                if (!preseason)
                                    careerStatsOffense[QBPGID].Interceptions--;

                                teamStats[stat.TeamId].InterceptionsCaught--;
                                teamStats[previousOpponents[stat.TeamId]].InterceptionsThrown--;
                            }
                        }
                    }

                    for (int i = 0; i < stat.Sacks; i++)
                    {
                        if (rand.NextDouble() > sackProbability)
                        {
                            stat.Sacks--;
                            teamBoxScores[stat.TeamId].Sacks--;
                            //teamBoxScores[previousOpponents[stat.TeamId]].SacksAllowed--;

                            if (currentWeek < 19)
                            {
                                seasonStatsDefense[stat.PlayerId].Sacks--;
                                if (!preseason)
                                    careerStatsDefense[stat.PlayerId].Sacks--;
                            }

                            int subtractFrom = rand.Next(sackees[previousOpponents[stat.TeamId]].Count);
                            int QBPGID = sackees[previousOpponents[stat.TeamId]][subtractFrom].PlayerId;

                            sackees[previousOpponents[stat.TeamId]][subtractFrom].Sacks--;
                            if (sackees[previousOpponents[stat.TeamId]][subtractFrom].Sacks == 0)
                                sackees[previousOpponents[stat.TeamId]].RemoveAt(subtractFrom);

                            int OLPGID = -1;
                            if (OLsackees[previousOpponents[stat.TeamId]].Count > 0)
                            {
                                subtractFrom = rand.Next(OLsackees[previousOpponents[stat.TeamId]].Count);
                                OLPGID = OLsackees[previousOpponents[stat.TeamId]][subtractFrom].PlayerId;

                                OLsackees[previousOpponents[stat.TeamId]][subtractFrom].SacksAllowed--;
                                if (OLsackees[previousOpponents[stat.TeamId]][subtractFrom].SacksAllowed == 0)
                                    OLsackees[previousOpponents[stat.TeamId]].RemoveAt(subtractFrom);
                            }

                            if (currentWeek < 19)
                            {
                                seasonStatsOffense[QBPGID].Sacks--;
                                if (!preseason)
                                    careerStatsOffense[QBPGID].Sacks--;

                                if (OLPGID >= 0)
                                {
                                    seasonStatsOffensiveLine[OLPGID].SacksAllowed--;
                                    if (!preseason)
                                        careerStatsOffensiveLine[OLPGID].SacksAllowed--;
                                }

                                teamStats[stat.TeamId].Sacks--;
                                teamStats[previousOpponents[stat.TeamId]].SacksAllowed--;

                                // we need to add back about 9 yards for every sack we take away.
                                teamStats[previousOpponents[stat.TeamId]].PassingYards += 9;
                                teamStats[previousOpponents[stat.TeamId]].TotalOffense += 9;
                                teamStats[previousOpponents[stat.TeamId]].TotalYards += 9;
                            }
                        }
                    }
                }

                // fix passing yards and rushing yards / attempts
                foreach (BoxScoreOffenseRecord stat in model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].GetRecords())
                {
                    if (stat.Week != currentWeek - 1 || humanControlled.Contains(stat.TeamId) || stat.Season != currentSeason)
                        continue;

//                    if (stat.Fumbles > 0)

                    // if it's a receiver of some kind, then continue so we can increase their rushing/receiving yards
                    if (model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId).PositionId < (int)MaddenPositions.HB ||
                        model.PlayerModel.GetPlayerByPlayerId(stat.PlayerId).PositionId > (int)MaddenPositions.TE)
                        continue;

                    int yardsToAdd = (int)Math.Round((double)stat.ReceivingYards * (passingYardsMultiplier - 1.0));
                    stat.ReceivingYards += yardsToAdd;
                    teamBoxScores[stat.TeamId].PassingYards += yardsToAdd;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[stat.PlayerId].ReceivingYards += yardsToAdd;
                        if (!preseason)
                            careerStatsOffense[stat.PlayerId].ReceivingYards += yardsToAdd;
                    }

                    maxPassers[stat.TeamId].PassingYards += yardsToAdd;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[maxPassers[stat.TeamId].PlayerId].PassingYards += yardsToAdd;
                        if (!preseason)
                            careerStatsOffense[maxPassers[stat.TeamId].PlayerId].PassingYards += yardsToAdd;

                        teamStats[stat.TeamId].TotalYards += yardsToAdd;
                        teamStats[stat.TeamId].TotalOffense += yardsToAdd;
                        teamStats[stat.TeamId].PassingYards += yardsToAdd;
                        teamStats[previousOpponents[stat.TeamId]].PassingYardsAllowed += yardsToAdd;
                    }

                    int rushAttemptsSubtracted = (int)Math.Round((double)stat.RushingAttempts * (1.0 - rushingAttemptsMultiplier));
                    stat.RushingAttempts -= rushAttemptsSubtracted;
                    teamBoxScores[stat.TeamId].RushingAttempts -= rushAttemptsSubtracted;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[stat.PlayerId].RushingAttempts -= rushAttemptsSubtracted;
                        if (!preseason)
                            careerStatsOffense[stat.PlayerId].RushingAttempts -= rushAttemptsSubtracted;
                    }

                    int rushYardsToAdd = (int)Math.Round((double)stat.RushingYards * (rushingYardsMultiplier - 1.0));
                    stat.RushingYards += rushYardsToAdd;
                    teamBoxScores[stat.TeamId].RushingYards += rushYardsToAdd;
//                    teamBoxScores[previousOpponents[stat.TeamId]].RushingYardsAllowed += rushYardsToAdd;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[stat.PlayerId].RushingYards += rushYardsToAdd;
                        if (!preseason)
                            careerStatsOffense[stat.PlayerId].RushingYards += rushYardsToAdd;

                        teamStats[stat.TeamId].TotalYards += rushYardsToAdd;
                        teamStats[stat.TeamId].TotalOffense += rushYardsToAdd;
                        teamStats[stat.TeamId].RushingYards += rushYardsToAdd;
                        teamStats[previousOpponents[stat.TeamId]].RushingYardsAllowed += rushYardsToAdd;
                    }
                }

            // give back some large number of FB carries to the HB1.  Some of these
            // will then trickle down to the HB2 in the next block
                for (int i = 0; i < 32; i++)
                {
                    if (!startingRBs.ContainsKey(i) || !startingFBs.ContainsKey(i) || !previousOpponents.ContainsKey(i))
                        continue;

                    int carriesToMove = 0;

                    for (int j = 0; j < startingFBs[i].RushingAttempts; j++)
                    {
                        if (rand.NextDouble() > FBcarriesProbability)
                            carriesToMove++;
                    }

                    int HBYardsToAdd = (int)Math.Round((double)carriesToMove*(double)startingRBs[i].RushingYards / (double)startingRBs[i].RushingAttempts);
                    int FBYardsToSubtract = (int)Math.Round((double)carriesToMove * (double)startingFBs[i].RushingYards / (double)startingFBs[i].RushingAttempts);

                    startingRBs[i].RushingAttempts += carriesToMove;
                    startingRBs[i].RushingYards += HBYardsToAdd;

                    startingFBs[i].RushingAttempts -= carriesToMove;
                    startingFBs[i].RushingYards -= FBYardsToSubtract;

                    teamBoxScores[i].RushingYards += HBYardsToAdd - FBYardsToSubtract;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[startingRBs[i].PlayerId].RushingAttempts += carriesToMove;
                        seasonStatsOffense[startingRBs[i].PlayerId].RushingYards += HBYardsToAdd;

                        seasonStatsOffense[startingFBs[i].PlayerId].RushingAttempts -= carriesToMove;
                        seasonStatsOffense[startingFBs[i].PlayerId].RushingYards -= FBYardsToSubtract;

                        if (!preseason)
                        {
                            careerStatsOffense[startingRBs[i].PlayerId].RushingAttempts += carriesToMove;
                            careerStatsOffense[startingRBs[i].PlayerId].RushingYards += HBYardsToAdd;

                            careerStatsOffense[startingFBs[i].PlayerId].RushingAttempts -= carriesToMove;
                            careerStatsOffense[startingFBs[i].PlayerId].RushingYards -= FBYardsToSubtract;
                        }

                        teamStats[i].TotalYards += HBYardsToAdd - FBYardsToSubtract;
                        teamStats[i].TotalOffense += HBYardsToAdd - FBYardsToSubtract;
                        teamStats[i].RushingYards += HBYardsToAdd - FBYardsToSubtract;
                        teamStats[previousOpponents[i]].RushingYardsAllowed += HBYardsToAdd - FBYardsToSubtract;
                    }
                }

                // fix RB stats
                for (int i = 0; i < 32; i++)
                {
                    // this shouldn't really happen if they're using Weekly
                    // Maintenance properly, but this at least prevents
                    // an exception.
                    if (!backupRBIDs.ContainsKey(i) || !startingRBs.ContainsKey(i) || !previousOpponents.ContainsKey(i))
                        continue;

                    double HB2frac = 1.0 - ((double)RBSlider(i)) / 100.0;
                    double yardageMultipler = ((double)model.PlayerModel.GetPlayerByPlayerId(backupRBIDs[i]).Overall / (double)model.PlayerModel.GetPlayerByPlayerId(startingRBIDs[i]).Overall);

                    int carries = (int)Math.Round(HB2frac * (double)startingRBs[i].RushingAttempts);
                    int yards = (int)Math.Round(HB2frac * (double)startingRBs[i].RushingYards);

                    startingRBs[i].RushingAttempts -= carries;
                    startingRBs[i].RushingYards -= yards;
                    teamBoxScores[i].RushingAttempts -= carries;
                    teamBoxScores[i].RushingYards -= yards;
                    //teamBoxScores[previousOpponents[i]].RushingYardsAllowed -= yards;

                    if (currentWeek < 19)
                    {
                        seasonStatsOffense[startingRBs[i].PlayerId].RushingAttempts -= carries;
                        seasonStatsOffense[startingRBs[i].PlayerId].RushingYards -= yards;

                        if (!preseason)
                        {
                            careerStatsOffense[startingRBs[i].PlayerId].RushingAttempts -= carries;
                            careerStatsOffense[startingRBs[i].PlayerId].RushingYards -= yards;
                        }

                        teamStats[i].TotalYards -= yards;
                        teamStats[i].TotalOffense -= yards;
                        teamStats[i].RushingYards -= yards;
                        teamStats[previousOpponents[i]].RushingYardsAllowed -= yards;
                    }

                    BoxScoreOffenseRecord backupRBBox = null;
                    SeasonStatsOffenseRecord backupRBSeason = null;
                    CareerStatsOffenseRecord backupRBCareer = null;

                    if (backupRBs.ContainsKey(i))
                        backupRBBox = backupRBs[i];
                    else
                    {
                        backupRBBox = (BoxScoreOffenseRecord)model.TableModels[EditorModel.BOXSCORE_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBBox.PlayerId = backupRBIDs[i];
                        backupRBBox.TeamId = i;
                        backupRBBox.Week = currentWeek - 1;
                        backupRBBox.Season = currentSeason;
                        backupRBBox.GameNumber = startingRBs[i].GameNumber;
                        backupRBBox.Weight = startingRBs[i].Weight;
                    }

                    if (seasonStatsOffense.ContainsKey(backupRBIDs[i]))
                        backupRBSeason = seasonStatsOffense[backupRBIDs[i]];
                    else
                    {
                        backupRBSeason = (SeasonStatsOffenseRecord)model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBSeason.PlayerId = backupRBIDs[i];
                        backupRBSeason.Season = currentSeason;
                    }

                    if (careerStatsOffense.ContainsKey(backupRBIDs[i]))
                        backupRBCareer = careerStatsOffense[backupRBIDs[i]];
                    else
                    {
                        backupRBCareer = (CareerStatsOffenseRecord)model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].CreateNewRecord(true);
                        backupRBCareer.PlayerId = backupRBIDs[i];
                    }

                    backupRBBox.RushingAttempts += carries;
                    teamBoxScores[backupRBBox.TeamId].RushingAttempts += carries;

                    if (currentWeek < 19)
                    {
                        backupRBSeason.RushingAttempts += carries;
                        if (!preseason)
                            backupRBCareer.RushingAttempts += carries;
                    }

                    int backupYards = (int)Math.Round((double)yards * yardageMultipler);
                    backupRBBox.RushingYards += backupYards;
                    teamBoxScores[backupRBBox.TeamId].RushingYards += backupYards;
                    //teamBoxScores[previousOpponents[backupRBBox.TeamId]].RushingYardsAllowed += backupYards;

                    if (currentWeek < 19)
                    {
                        backupRBSeason.RushingYards += backupYards;
                        if (!preseason)
                            backupRBCareer.RushingYards += backupYards;

                        teamStats[i].TotalYards += backupYards;
                        teamStats[i].TotalOffense += backupYards;
                        teamStats[i].RushingYards += backupYards;
                        teamStats[previousOpponents[i]].RushingYardsAllowed += backupYards;
                    }
                }
        }
    }

    public enum MaddenSquadRatings
    {
        OffensivePassing = 0,
        OffensiveRunning,
        DefensivePassing,
        DefensiveRunning,
        HB1,
        HB2
    }
}