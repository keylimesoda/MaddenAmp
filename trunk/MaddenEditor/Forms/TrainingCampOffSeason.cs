/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using System.IO;

namespace MaddenEditor.Forms
{
    public partial class TrainingCampOffSeason : Form
    {
        public string profile;
        private EditorModel model = null;
        private DepthChartEditingModel depthEditingModel = null;
        private bool isInitialising = false;public Boolean WeightGain = false;public Boolean WeightLoss = false;
        public decimal PercentModFat = 0;public decimal PercentModMuscle = 0;
        public int Rating = 0;public int Skill = 0;string PosStr = null;
        int AddWgt = 0; int OldWgt = 0; int SpdMean = 0; int AccMean = 0; int AgiMean = 0; int StrMean = 0; int StmMean = 0; int InjMean = 0;
        double PosWgtMod = 0;
        bool NegativeScenario1 = false; bool NegativeScenario2 = false;
        int HoursLeft = 0;
        int knw = 0; int mot = 0;int chm = 0;int eth = 0;int qb = 0;int rb = 0;int wr = 0;int ol = 0;int dl = 0;int lb = 0;int db = 0;int k = 0;int p = 0;
        //Variables used for random scenarios
        string ScenarioFirstName = "";string ScenarioLastName = "";int ScenarioOvr = 0;string ScenarioPos = "";int CoachMotivation = 0;string CurrentDialog = "";
        int ScenarioCounter = 0;int Sleep = 0;bool isTrue = false;bool BadNews = false;bool GoodNews = false;bool HorribleNews = false;bool GreatNews = false;
        string Captain1 = "";string Captain2 = "";string Captain3 = "";int NewCaptain = 0;string OldCaptain = "";int Diff = 0; //This is any modifier applied to an ability if random scenario = true
        private TeamCaptainRecord teamCaptainRecord = null;
        DataTable RosterView = new DataTable();
        BindingSource RosterViewBinding = new BindingSource();
        Random random = new Random();
        OwnerRecord CPUteam;
        int oldKnw;int OldMot;int oldChm;int OldEth;int oldqb;int Oldrb;int oldwr;int Oldol;int olddl;int Oldlb;int olddb;int Oldk;int Oldp;
        //Tune file attribute Variables
        decimal SpdLoss; decimal AccLoss; decimal AgiLoss; decimal StmLoss; decimal InjLoss; decimal SpdGain; decimal AccGain; decimal AgiGain; decimal StmGain; decimal StrLoss; decimal InjGain; int DisableScenario;
        
        public TrainingCampOffSeason(EditorModel model)
        { 
            this.model = model;
            InitializeComponent();
        }
        private void LoadSliderSettings()
        {
            StreamWriter sw;
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning");
             
            }
            if (!File.Exists(installDirectory + "\\Conditioning\\SliderSettings.txt")) // Create the Slider Profile if not present.
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\SliderSettings.txt");
                sw = new StreamWriter(file);

                sw.WriteLine("GainFrequency\t13");
                sw.WriteLine("LossFrequency\t0");
                sw.WriteLine("AmountGained\t2");
                sw.WriteLine("AmountLost\t4");
                sw.WriteLine("DisableOffseasonScenario\t0");

                sw.Close();
            }
            if (!File.Exists(installDirectory + "\\Conditioning\\ConditioningTune.txt")) // Create the Slider Profile if not present.
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\ConditioningTune.txt");
                sw = new StreamWriter(file);
                sw.WriteLine("**WeightGain");
                sw.WriteLine("SpdLoss\t.25");
                sw.WriteLine("AccLoss\t.35");
                sw.WriteLine("AgiLoss\t.35");
                sw.WriteLine("StmLoss\t1.35");
                sw.WriteLine("InjLoss\t.2");
                sw.WriteLine("**WeightLoss");
                sw.WriteLine("SpdGain\t.16");
                sw.WriteLine("AccGain\t.38");
                sw.WriteLine("AgiGain\t.38");
                sw.WriteLine("StrLoss\t.1");
                sw.WriteLine("StmGain\t.35");
                sw.WriteLine("InjGain\t.5");                
                sw.Close();
            }
            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\ConditioningTune.txt");

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] splitLine = line.Split('\t');

                if (splitLine[0] == "SpdLoss")
                {
                    SpdLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AccLoss")
                {
                    AccLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AgiLoss")
                {
                    AgiLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "StmLoss")
                {
                    StmLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "InjLoss")
                {
                    InjLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "SpdGain")
                {
                    SpdGain = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AccGain")
                {
                    AccGain = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AgiGain")
                {
                    AgiGain = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "StrLoss")
                {
                    StrLoss = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "StmGain")
                {
                    StmGain = decimal.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "InjGain")
                {
                    InjGain = decimal.Parse(splitLine[1]);
                }           
            }
            sr.Close();
            
            StreamReader srt = new StreamReader(installDirectory + "\\Conditioning\\SliderSettings.txt");

            while (!srt.EndOfStream)
            {
                string line = srt.ReadLine();
                string[] splitLine = line.Split('\t');

                if (splitLine[0] == "GainFrequency")
                {
                    GainFreqSld.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "LossFrequency")
                {
                    LossFreqSld.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AmountGained")
                {
                    GainAmountSld.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "AmountLost")
                {
                    LossAmountSld.Value = Int32.Parse(splitLine[1]);
                }
                else if (splitLine[0] == "DisableOffseasonScenario")
                {
                    DisableScenario = Int32.Parse(splitLine[1]);
                } 
                
            }
            srt.Close();
            if (DisableScenario == 1)
            { DisableScenarioChk.Checked = true; } //Check disable offseason scenario box
        }
        public void SaveSliders()
        {
            StreamWriter sw;
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning");
            
            }
            if (!File.Exists(installDirectory + "\\Conditioning\\SliderSettings.txt"))
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\SliderSettings.txt");    // Create the Slider Profile if not present.
                sw = new StreamWriter(file);
            }
            else
            {
                sw = new StreamWriter(installDirectory + "\\Conditioning\\SliderSettings.txt", false);
            }
            sw.WriteLine("GainFrequency\t" + GainFreqSld.Value);
            sw.WriteLine("LossFrequency\t" + LossFreqSld.Value);
            sw.WriteLine("AmountGained\t" + GainAmountSld.Value);
            sw.WriteLine("AmountLost\t" + LossAmountSld.Value);
            if (DisableScenarioChk.Checked == true)
            {sw.WriteLine("DisableOffseasonScenario\t1");}
            else if (DisableScenarioChk.Checked == false)
            { sw.WriteLine("DisableOffseasonScenario\t0"); }
            sw.Close();
        }
        public void LoadDefaults()
        {
            StreamWriter sw;
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning");

            }          
                FileStream file = File.Create(installDirectory + "\\Conditioning\\SliderSettings.txt");
                sw = new StreamWriter(file);

                sw.WriteLine("GainFrequency\t0");
                sw.WriteLine("LossFrequency\t0");
                sw.WriteLine("AmountGained\t15");
                sw.WriteLine("AmountLost\t10");
                sw.WriteLine("DisableOffseasonScenario\t0");

                sw.Close();
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\SliderSettings.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] splitLine = line.Split('\t');

                    if (splitLine[0] == "GainFrequency")
                    {
                        GainFreqSld.Value = Int32.Parse(splitLine[1]);
                    }
                    else if (splitLine[0] == "LossFrequency")
                    {
                        LossFreqSld.Value = Int32.Parse(splitLine[1]);
                    }
                    else if (splitLine[0] == "AmountGained")
                    {
                        GainAmountSld.Value = Int32.Parse(splitLine[1]);
                    }
                    else if (splitLine[0] == "AmountLost")
                    {
                        LossAmountSld.Value = Int32.Parse(splitLine[1]);
                    }
                    else if (splitLine[0] == "DisableOffseasonScenario")
                    {
                        DisableScenario = Int32.Parse(splitLine[1]);
                    } 
                }
                sr.Close();
                if (DisableScenario == 1)
                { DisableScenarioChk.Checked = true; } //Check disable offseason scenario box
        }
        public void initialiseUI()
        {
            LoadSliderSettings();
            SelectHumanTeam.Enabled = false;
            depthEditingModel = new DepthChartEditingModel(model);
            foreach (TeamRecord team in model.TeamModel.GetTeams())
            {
                SelectHumanTeam.Items.Add(team);
            }
            SelectHumanTeam.Items.RemoveAt(34);
            SelectHumanTeam.Items.RemoveAt(33);
            SelectHumanTeam.Items.RemoveAt(32);   
            isInitialising = false;
        } 
        private void CallWeightGain(int Weight, int Age, int Pos, int Cmot, string FirstName, string LastName)
        {           
            int AgeMod = 0;
            int PosMod = 0;
            double WeightMod = 0.0;
            //Find modifier for Age
                if (Age < 23)
                {
                    AgeMod = (int)Math.Floor(-13 * random.NextDouble());
                }
                else if (Age < 25)
                {
                    AgeMod = (int)Math.Floor(-9 * random.NextDouble());
                }
                else if (Age < 28)
                {
                    AgeMod = (int)Math.Floor((-5 * random.NextDouble()));
                }
                else if (Age < 30)
                {
                    AgeMod = (int)Math.Floor((-3 * random.NextDouble()) + 1);
                }
                else if (Age < 32)
                {
                    AgeMod = (int)Math.Floor(((Age - 25) * random.NextDouble()) + 2);
                }
                else if (Age <= 34)
                {
                    AgeMod = (int)Math.Floor((((Age - 25) * (double)1.3) * random.NextDouble()) + 3);
                }
                else if (Age > 34)
                {
                    AgeMod = (int)Math.Floor((((Age - 25) * (double)1.6) * random.NextDouble()) + 4);
                }

                //Find Modifier for weight by position
                if (Pos == 5 || Pos == 6 || Pos == 7 || Pos == 8 || Pos == 9 || Pos == 10 || Pos == 11 || Pos == 12)
                {
                    //Average weight 295-310
                    if (Weight < 295)
                    {
                        WeightMod = ((295 - Weight) / 5);
                    }
                    else if (Weight > 310)
                    {
                        WeightMod = ((310 - Weight) / 5);
                    }
                }
                else if (Pos == 2)
                {
                    //Average weight 220-235
                    if (Weight < 220)
                    {
                        WeightMod = ((220 - Weight) / 5);
                    }
                    else if (Weight > 235)
                    {
                        WeightMod = ((235 - Weight) / 5);
                    }
                }
                else if (Pos == 4)
                {
                    //Average weight 240-260
                    if (Weight < 240)
                    {
                        WeightMod = ((240 - Weight) / 5);
                    }
                    else if (Weight > 260)
                    {
                        WeightMod = ((260 - Weight) / 5);
                    }
                }
                else if (Pos == 13 || Pos == 14 || Pos == 15)
                {
                    //Average weight 220-240
                    if (Weight < 220)
                    {
                        WeightMod = ((220 - Weight) / 5);
                    }
                    else if (Weight > 240)
                    {
                        WeightMod = ((240 - Weight) / 5);
                    }
                }
                else if (Pos == 0)
                {
                    //Average weight 200-220
                    if (Weight < 200)
                    {
                        WeightMod = ((200 - Weight) / 5);
                    }
                    else if (Weight > 220)
                    {
                        WeightMod = ((220 - Weight) / 5);
                    }
                }
                else if (Pos == 1)
                {
                    //Average weight 195-215
                    if (Weight < 195)
                    {
                        WeightMod = ((195 - Weight) / 5);
                    }
                    else if (Weight > 215)
                    {
                        WeightMod = ((215 - Weight) / 5);
                    }
                }
                else if (Pos == 17)
                {
                    //Average weight 195-215
                    if (Weight < 195)
                    {
                        WeightMod = ((195 - Weight) / 5);
                    }
                    else if (Weight > 215)
                    {
                        WeightMod = ((215 - Weight) / 5);
                    }
                }
                else if (Pos == 18)
                {
                    //Average weight 200-220
                    if (Weight < 200)
                    {
                        WeightMod = ((200 - Weight) / 5);
                    }
                    else if (Weight > 220)
                    {
                        WeightMod = ((220 - Weight) / 5);
                    }
                }
                else if (Pos == 16 || Pos == 3)
                {
                    //Average weight 190-210
                    if (Weight < 190)
                    {
                        WeightMod = ((190 - Weight) / 5);
                    }
                    else if (Weight > 210)
                    {
                        WeightMod = ((210 - Weight) / 5);
                    }
                }
                else if (Pos == 19 || Pos == 20)
                {
                    //Average weight 170-190
                    if (Weight < 170)
                    {
                        WeightMod = ((170 - Weight) / 5);
                    }
                    else if (Weight > 190)
                    {
                        WeightMod = ((190 - Weight) / 5);
                    }
                }

                //Find Modifier for Position (OL,DL,TE,FB,LB)
                if (Pos == 5 || Pos == 6 || Pos == 7 || Pos == 8 || Pos == 9 || Pos == 10 || Pos == 11 || Pos == 12 || Pos == 2 || Pos == 4 || Pos == 13 || Pos == 14 || Pos == 15)
                {
                    //Above positions more likely to be affected by weight gain
                    PosMod = (int)Math.Floor((15 * random.NextDouble()) + 5);
                }
                else if (Pos == 0 || Pos == 1 || Pos == 17 || Pos == 18)
                {
                    //Above positions middle of road in terms of weight gain
                    PosMod = (int)Math.Floor((10 * random.NextDouble()) + 1);
                }
                else if (Pos == 3 || Pos == 16 || Pos == 19 || Pos == 20)
                {
                    //Above positions least likely to be affected by weight gain
                    PosMod = (int)Math.Floor(-10 * random.NextDouble());
                }
                //Anything above 70 in motivation helps prevent weight gain. lower than 70 begins to increase chances...
                //double CoachEffect = Math.Round(Cmot / 2.9);
                double CoachEffect = Math.Round(Cmot / 4.0);
                double TempCmot = (double)Math.Round(17 - CoachEffect);

                int TotalWeightGainMod = (int)Math.Floor(AgeMod + PosMod + TempCmot - WeightMod);
                if (TotalWeightGainMod < 0)
                {
                    TotalWeightGainMod = 1;
                }
                int i = (int)(2 * random.NextDouble());
                int j = (int)(11 * random.NextDouble());
                double DetermineWeight = 0;
                if (i == 0)
                {
                    DetermineWeight = Math.Round(((50 * random.NextDouble() + 40) - j) * PosWgtMod);
                }
                else if (i == 1)
                {
                    DetermineWeight = Math.Round(((50 * random.NextDouble() + 40) + j) * PosWgtMod);
                }

               double RandomWeight = Math.Round((DetermineWeight * random.NextDouble()) - TempCmot) - GainFreqSld.Value;
               string CaptainFirst = "";
               string CaptainLast = "";               
               if (OldCaptain != "")
               {
                   CaptainFirst = OldCaptain.Split(' ')[0];
                   CaptainLast = OldCaptain.Split(' ')[1];
               }  
               if ((RandomWeight <= TotalWeightGainMod) & (FirstName != ScenarioFirstName) & (LastName != ScenarioLastName) || (GoodNews == true) & (FirstName == CaptainFirst) & (LastName == CaptainLast))
                {
                    WeightGain = true;
                }
                else if ((FirstName == ScenarioFirstName) & (LastName == ScenarioLastName) & (BadNews == true))
                {
                    WeightGain = true;
                }           
        }
        private void CallWeightLoss(int Weight, int Age, int Pos, int Cmot, string FirstName, string LastName)
        {
            int AgeMod = 0;
            int PosMod = 0;
            double WeightMod = 0.0;
            //Find modifier for Age
            if (Age < 26)
            {
                AgeMod = (int)Math.Floor(3 * random.NextDouble());
            }
            else if (Age < 28)
            {
                AgeMod = (int)Math.Floor(1 * random.NextDouble());
            }
            else if (Age < 30)
            {
                AgeMod = (int)Math.Floor((-5 * random.NextDouble()) + 1);
            }
            else if (Age < 32)
            {
                AgeMod = (int)Math.Floor(((25 - Age) * random.NextDouble()) + 2);
            }
            else if (Age <= 34)
            {
                AgeMod = (int)Math.Floor((((25 - Age) * (double)1.2) * random.NextDouble()) + 3);
            }
            else if (Age > 34)
            {
                AgeMod = (int)Math.Floor((((25 - Age) * (double)1.5 ) * random.NextDouble()) + 4);
            }


            //Find Modifier for weight by position
            if (Pos == 5 || Pos == 6 || Pos == 7 || Pos == 8 || Pos == 9 || Pos == 10 || Pos == 11 || Pos == 12)
            {
                //Average weight 295-310
                if (Weight < 295)
                {
                    WeightMod = ((Weight - 295) / 3);
                }
                else if (Weight > 310)
                {
                    WeightMod = ((310 - Weight) / 3);
                }
            }
            else if (Pos == 2)
            {
                //Average weight 220-235
                if (Weight < 220)
                {
                    WeightMod = ((Weight - 220) / 3);
                }
                else if (Weight > 235)
                {
                    WeightMod = ((235 - Weight) / 3);
                }
            }
            else if (Pos == 4)
            {
                //Average weight 240-260
                if (Weight < 240)
                {
                    WeightMod = ((Weight - 240) / 3);
                }
                else if (Weight > 260)
                {
                    WeightMod = ((260 - Weight) / 3);
                }
            }
            else if (Pos == 13 || Pos == 14 || Pos == 15)
            {
                //Average weight 220-240
                if (Weight < 220)
                {
                    WeightMod = ((Weight - 220) / 3);
                }
                else if (Weight > 240)
                {
                    WeightMod = ((240 - Weight) / 3);
                }
            }
            else if (Pos == 0)
            {
                //Average weight 200-220
                if (Weight < 200)
                {
                    WeightMod = ((Weight - 200) / 3);
                }
                else if (Weight > 220)
                {
                    WeightMod = ((220 - Weight) / 3);
                }
            }
            else if (Pos == 1)
            {
                //Average weight 195-215
                if (Weight < 195)
                {
                    WeightMod = ((Weight - 195) / 3);
                }
                else if (Weight > 215)
                {
                    WeightMod = ((215 - Weight) / 3);
                }
            }
            else if (Pos == 17)
            {
                //Average weight 195-215
                if (Weight < 195)
                {
                    WeightMod = ((Weight - 195) / 3);
                }
                else if (Weight > 215)
                {
                    WeightMod = ((215 - Weight) / 3);
                }
            }
            else if (Pos == 18)
            {
                //Average weight 200-220
                if (Weight < 200)
                {
                    WeightMod = ((Weight - 200) / 3);
                }
                else if (Weight > 220)
                {
                    WeightMod = ((220 - Weight) / 3);
                }
            }
            else if (Pos == 16 || Pos == 3)
            {
                //Average weight 190-210
                if (Weight < 190)
                {
                    WeightMod = ((Weight - 190) / 3);
                }
                else if (Weight > 210)
                {
                    WeightMod = ((210 - Weight) / 3);
                }
            }
            else if (Pos == 19 || Pos == 20)
            {
                //Average weight 170-190
                if (Weight < 170)
                {
                    WeightMod = ((Weight - 170) / 3);
                }
                else if (Weight > 190)
                {
                    WeightMod = ((190 - Weight) / 3);
                }
            }

            //Find Modifier for Position (OL,DL,TE,FB,LB)
            if (Pos == 5 || Pos == 6 || Pos == 7 || Pos == 8 || Pos == 9 || Pos == 10 || Pos == 11 || Pos == 12 || Pos == 2 || Pos == 4 || Pos == 13 || Pos == 14 || Pos == 15)
            {
                //Above positions more likely to be affected by weight 
                PosMod = (int)Math.Floor((15 * random.NextDouble()) + 5);
            }
            else if (Pos == 0 || Pos == 1 || Pos == 17 || Pos == 18) 
            {
                //Above positions middle of road in terms of weight 
                PosMod = (int)Math.Floor((10 * random.NextDouble()) + 1);
            }
            else if (Pos == 3 || Pos == 16 || Pos == 19 || Pos == 20)
            {
                //Above positions least likely to be affected by weight 
                PosMod = (int)Math.Floor(-10 * random.NextDouble());
            }
            //Anything above 70 in motivation helps prevent weight gain. lower than 70 begins to increase chances...
            //double CoachEffect = Math.Round(Cmot / 2.9);
            double CoachEffect = Math.Round(Cmot / 4.0);
            double TempCmot = (double)Math.Round(17 - CoachEffect);

            int TotalLossMod = (int)Math.Floor(AgeMod + PosMod - TempCmot + WeightMod);
            if (TotalLossMod < 0)
            {
                TotalLossMod = 1;
            }
            //Add variance to random roll 
            int i = (int)(2 * random.NextDouble());
            int j = (int)(11 * random.NextDouble());
            double DetermineLoss = 0;
            if (i == 0)
            {
                DetermineLoss = Math.Round(((60 * random.NextDouble() + 100) - j) * (2 - PosWgtMod));             
            }
            else if (i == 1)
            {
                DetermineLoss = Math.Round(((60 * random.NextDouble() + 100) + j) * (2 - PosWgtMod)); 
            }
            string CaptainFirst = "";
            string CaptainLast = "";
            if (OldCaptain != "")
            {
                CaptainFirst = OldCaptain.Split(' ')[0];
                CaptainLast = OldCaptain.Split(' ')[1];
            }
            double RandomLoss = Math.Round((DetermineLoss * random.NextDouble()) + TempCmot) - LossFreqSld.Value;
            if ((RandomLoss <= TotalLossMod) & (FirstName != ScenarioFirstName) & (LastName != ScenarioLastName) || (BadNews == true) & (FirstName == CaptainFirst) & (LastName == CaptainLast))
            {
                WeightLoss = true;
            }
            else if ((FirstName == ScenarioFirstName) & (LastName == ScenarioLastName) & (GoodNews == true))
            {
                WeightLoss = true;
            }  
        }
        private void AttributeMeans(int PosId)
        {
            PosStr = "";
            SpdMean = 0;
            AccMean = 0;
            AgiMean = 0;
            StrMean = 0;
            StmMean = 0;
            InjMean = 0;
            PosWgtMod = 0;

            if (PosId == 0)
            {
                PosStr = "QB";
                SpdMean = 58;
                AccMean = 61;
                AgiMean = 61;
                StrMean = 55;
                StmMean = 89;
                InjMean = 83;
                PosWgtMod = 1;
            }
            else if (PosId == 1)
            {
                PosStr = "HB";
                SpdMean = 88;
                AccMean = 90;
                AgiMean = 88;
                StrMean = 68;
                StmMean = 85;
                InjMean = 85;
                PosWgtMod = .9;
            }
            else if (PosId == 2)
            {
                PosStr = "FB";
                SpdMean = 70;
                AccMean = 79;
                AgiMean = 66;
                StrMean = 71;
                StmMean = 75;
                InjMean = 74;
                PosWgtMod = 1.15;
            }
            else if (PosId == 3)
            {
                PosStr = "WR";
                SpdMean = 90;
                AccMean = 92;
                AgiMean = 90;
                StrMean = 54;
                StmMean = 88;
                InjMean = 85;
                PosWgtMod = .5;
            }
            else if (PosId == 4)
            {
                PosStr = "TE";
                SpdMean = 72;
                AccMean = 79;
                AgiMean = 72;
                StrMean = 70;
                StmMean = 83;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 5)
            {
                PosStr = "LT";
                SpdMean = 50;
                AccMean = 70;
                AgiMean = 52;
                StrMean = 90;
                StmMean = 73;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 6)
            {
                PosStr = "LG";
                SpdMean = 50;
                AccMean = 70;
                AgiMean = 52;
                StrMean = 90;
                StmMean = 73;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 7)
            {
                PosStr = "C ";
                SpdMean = 50;
                AccMean = 70;
                AgiMean = 52;
                StrMean = 90;
                StmMean = 73;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 8)
            {
                PosStr = "RG";
                SpdMean = 50;
                AccMean = 70;
                AgiMean = 52;
                StrMean = 90;
                StmMean = 73;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 9)
            {
                PosStr = "RT";
                SpdMean = 50;
                AccMean = 70;
                AgiMean = 52;
                StrMean = 90;
                StmMean = 73;
                InjMean = 85;
                PosWgtMod = 1.2;
            }
            else if (PosId == 10)
            {
                PosStr = "LE";
                SpdMean = 69;
                AccMean = 77;
                AgiMean = 68;
                StrMean = 79;
                StmMean = 77;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 11)
            {
                PosStr = "RE";
                SpdMean = 69;
                AccMean = 77;
                AgiMean = 68;
                StrMean = 79;
                StmMean = 77;
                InjMean = 80;
                PosWgtMod = 1.15;
            }
            else if (PosId == 12)
            {
                PosStr = "DT";
                SpdMean = 60;
                AccMean = 77;
                AgiMean = 68;
                StrMean = 79;
                StmMean = 77;
                InjMean = 80;
                PosWgtMod = 1.2;
            }
            else if (PosId == 13)
            {
                PosStr = "LOLB";
                SpdMean = 79;
                AccMean = 83;
                AgiMean = 78;
                StrMean = 72;
                StmMean = 83;
                InjMean = 83;
                PosWgtMod = 1.05;
            }
            else if (PosId == 14)
            {
                PosStr = "MLB";
                SpdMean = 76;
                AccMean = 80;
                AgiMean = 76;
                StrMean = 76;
                StmMean = 84;
                InjMean = 80;
                PosWgtMod = 1.05;
            }
            else if (PosId == 15)
            {
                PosStr = "ROLB";
                SpdMean = 79;
                AccMean = 83;
                AgiMean = 78;
                StrMean = 72;
                StmMean = 83;
                InjMean = 83;
                PosWgtMod = 1.05;
            }
            else if (PosId == 16)
            {
                PosStr = "CB";
                SpdMean = 90;
                AccMean = 92;
                AgiMean = 89;
                StrMean = 52;
                StmMean = 85;
                InjMean = 85;
                PosWgtMod = .5;
            }
            else if (PosId == 17)
            {
                PosStr = "FS";
                SpdMean = 86;
                AccMean = 88;
                AgiMean = 86;
                StrMean = 57;
                StmMean = 85;
                InjMean = 85;
                PosWgtMod = .75;
            }
            else if (PosId == 18)
            {
                PosStr = "SS";
                SpdMean = 86;
                AccMean = 88;
                AgiMean = 84;
                StrMean = 60;
                StmMean = 85;
                InjMean = 85;
                PosWgtMod = .8;
            }
            else if (PosId == 19)
            {
                PosStr = "K";
                SpdMean = 45;
                AccMean = 48;
                AgiMean = 45;
                StrMean = 26;
                StmMean = 85;
                InjMean = 82;
                PosWgtMod = .25;
            }
            else if (PosId == 20)
            {
                PosStr = "P";
                SpdMean = 51;
                AccMean = 57;
                AgiMean = 47;
                StrMean = 33;
                StmMean = 86;
                InjMean = 80;
                PosWgtMod = .25;
            }
        }
       private void PhaseOneCPU()
        {
            RosterView.Clear();

            int teamId = ((OwnerRecord)CPUteam).TeamId;
            int positionId = 0;
            int FatCoutner = 0;
            int MuscleCounter = 0;
            int acCounter = 1;            
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            //Load current teams coach
            model.CoachModel.SetPositionFilter(0);           
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            int CoachMotivation = model.CoachModel.CurrentCoachRecord.Motivation;

            foreach (PlayerRecord valObject in teamPlayers)
            {
                WeightGain = false;
                WeightLoss = false;
                Rating = 0;
                Skill = 0;
                AttributeMeans(valObject.PositionId);
                CallWeightGain((valObject.Weight + 160), valObject.Age, valObject.PositionId, CoachMotivation, valObject.FirstName, valObject.LastName);
                CallWeightLoss((valObject.Weight + 160), valObject.Age, valObject.PositionId, CoachMotivation, valObject.FirstName, valObject.LastName);
                string CaptainFirst = "";
                string CaptainLast = "";
                if (OldCaptain != "")
                {
                    CaptainFirst = OldCaptain.Split(' ')[0];
                    CaptainLast = OldCaptain.Split(' ')[1];
                }  
                if ((WeightGain == true & WeightLoss == false) | (WeightGain == false & WeightLoss == true) || ((BadNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName)) || ((GoodNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName)))
                {

                    if (WeightGain == true)
                    {
                        FatCoutner++;
                    }
                    else if (WeightLoss == true)
                    {
                        MuscleCounter++;
                    }

                    //Print Before row to DataGrid
                    DataRow dr = RosterView.NewRow();
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;

                    dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                    dr["Pos"] = PosStr;
                    dr["Age"] = valObject.Age;
                    dr["Exp"] = valObject.YearsPro;
                    valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                    dr["Ovr"] = valObject.Overall;
                    // Would like to include feet/inches
                    dr["Hgt"] = valObject.Height;
                    dr["Wgt"] = valObject.Weight + 160;
                    dr["Spd"] = valObject.Speed;
                    dr["Acc"] = valObject.Acceleration;
                    dr["Agi"] = valObject.Agility;
                    dr["Str"] = valObject.Strength;
                    dr["Stm"] = valObject.Stamina;
                    dr["Inj"] = valObject.Injury;
                    dr["Tgh"] = valObject.Toughness;
                    dr["Mor"] = valObject.Morale;
                    dr["Awr"] = valObject.Awareness;
                    dr["Cat"] = valObject.Catching;
                    dr["Car"] = valObject.Carrying;
                    dr["Jmp"] = valObject.Jumping;
                    dr["Btk"] = valObject.BreakTackle;
                    dr["Tkl"] = valObject.Tackle;
                    dr["ThP"] = valObject.ThrowPower;
                    dr["ThA"] = valObject.ThrowAccuracy;
                    dr["Pbk"] = valObject.PassBlocking;
                    dr["Rbk"] = valObject.RunBlocking;
                    dr["KP"] = valObject.KickPower;
                    dr["KA"] = valObject.KickAccuracy;
                    dr["KR"] = valObject.BodyWeight;
                    // dr["KR"] = valObject.KickReturn;

                    RosterView.Rows.Add(dr);
                    int OldOvr = valObject.Overall;
                    int OldSpeed = valObject.Speed;
                    int OldAcc = valObject.Acceleration;
                    int OldAgi = valObject.Agility;
                    int OldStr = valObject.Strength;
                    int OldStm = valObject.Stamina;
                    int OldInj = valObject.Injury;
                    int OldJmp = valObject.Jumping;
                    int OldBtk = valObject.BreakTackle;
                    OldWgt = valObject.Weight + 160;
                    int OldPbk = valObject.PassBlocking;
                    int OldRbk = valObject.RunBlocking;
                    int OldMor = valObject.Morale;

                    decimal AttributeDifferential;

                    if (WeightGain == true)
                    {
                        AllocateWeight(CoachMotivation, valObject.Age);
                        PercentModFat = (Math.Abs((decimal)PercentModFat - 1) * (decimal)PosWgtMod) + 1;
                        PercentModMuscle = 2 - PercentModFat;
                        AddWgt = (int)(Math.Ceiling((valObject.Weight + 160) * (decimal)PercentModFat));
                        decimal WgtDifferential = decimal.Round((1 - ((decimal)OldWgt / (decimal)AddWgt)), 4); // *(decimal)1.35;

                        PercentModFat = Math.Round(((decimal)AddWgt / (decimal)OldWgt), 6) + (decimal).05;
                        PercentModMuscle = Math.Round(((decimal)OldWgt / (decimal)AddWgt), 6) - (decimal).05;

                        valObject.Weight = (AddWgt - 160);
                        Rating = (int)(Math.Ceiling(valObject.BodyWeight * PercentModFat));
                        HighLowCheck(Rating);
                        valObject.BodyWeight = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Speed / (decimal)SpdMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)SpdMean / (decimal)valObject.Speed), 4))) * 2) + (decimal)SpdLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)SpdLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Speed * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Speed = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Acceleration / (decimal)AccMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)AccMean / (decimal)valObject.Acceleration), 4))) * 2) + (decimal)AccLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AccLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Acceleration * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        OldAcc = valObject.Acceleration;
                        valObject.Acceleration = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Agility / (decimal)AgiMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)AgiMean / (decimal)valObject.Agility), 4))) * 2) + (decimal)AgiLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AgiLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Agility * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        OldAgi = valObject.Agility;
                        if ((valObject.Acceleration < OldAcc) & (Rating < valObject.Agility)) //Increment PassBlk, decrement Run block due to weight loss
                        {
                            int BlockChange = ((OldAcc - valObject.Acceleration) + (OldAgi - Rating)) / 2;
                            valObject.RunBlocking = valObject.RunBlocking + BlockChange;
                            valObject.PassBlocking = valObject.PassBlocking - BlockChange;
                        }
                        valObject.Agility = Rating;


                        AttributeDifferential = decimal.Round(((decimal)valObject.Stamina / (decimal)StmMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StmMean / (decimal)valObject.Stamina), 4))) * 2) + (decimal)StmLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StmLoss; }
                        Rating = (int)Math.Round(valObject.Stamina * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Stamina = Rating;
                        if (OldStm > valObject.Stamina) //Decrement Jump = Stamina loss /2
                        {
                            int JumpChange = (OldStm - valObject.Stamina) / 2;
                            valObject.Jumping = valObject.Jumping - JumpChange;
                            valObject.BreakTackle = valObject.BreakTackle + JumpChange; //BreakTackle drop due to mass loss
                        }
                        AttributeDifferential = decimal.Round(((decimal)valObject.Injury / (decimal)InjMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)InjMean / (decimal)valObject.Injury), 4))) * 2) + (decimal)InjLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)InjLoss; }
                        Rating = (int)Math.Round(valObject.Injury * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Injury = Rating;
                       
                        Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                        HighLowCheck(Rating);
                        valObject.BodyMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.BodyFat * (PercentModFat * (decimal)1.35))));
                        HighLowCheck(Rating);
                        valObject.BodyFat = Rating;
                        Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.ArmsMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.ArmsFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsThighMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsThighFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfFat = Rating;
                        Rating = (int)(Math.Round((valObject.BodyOverall * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.BodyOverall = Rating;


                    }
                    else if (WeightLoss == true)
                    {
                        AllocateLoss(CoachMotivation, valObject.Age);
                        PercentModFat = Math.Abs((Math.Abs((decimal)PercentModFat - 1) * (decimal)PosWgtMod) - 1);
                        PercentModMuscle = 2 - PercentModFat;
                        AddWgt = (int)(Math.Floor((valObject.Weight + 160) * (decimal)PercentModFat));
                        decimal WgtDifferential = decimal.Round((1 - ((decimal)AddWgt / (decimal)OldWgt)), 4) * (decimal)1.5;

                        PercentModFat = Math.Round(((decimal)OldWgt / (decimal)AddWgt), 6) - (decimal).05;
                        PercentModMuscle = Math.Round(((decimal)AddWgt / (decimal)OldWgt), 6) + (decimal).05;

                        valObject.Weight = (AddWgt - 160);
                        Rating = (int)(Math.Ceiling(valObject.BodyWeight * PercentModFat));
                        HighLowCheck(Rating);
                        valObject.BodyWeight = Rating;

                        if (valObject.Speed < SpdMean)//Spd increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Speed / (decimal)SpdMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)SpdMean / (decimal)valObject.Speed), 4))) * 2) + (decimal)SpdGain)* (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * (decimal)1.5) + (decimal)SpdGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Speed * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            valObject.Speed = Rating;
                        }

                        if ((valObject.Acceleration < AccMean) || (valObject.Speed > OldSpeed))//Acc increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Acceleration / (decimal)AccMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)AccMean / (decimal)valObject.Acceleration), 4))) * 2) + (decimal)AccGain) * (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AccGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Acceleration * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            OldAcc = valObject.Acceleration;
                            valObject.Acceleration = Rating;
                        }

                        if ((valObject.Agility < AgiMean) || (valObject.Speed > OldSpeed)) //Agi increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Agility / (decimal)AgiMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)AgiMean / (decimal)valObject.Agility), 4))) * 2) + (decimal)AgiGain)* (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AgiGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Agility * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            OldAgi = valObject.Agility;
                            if ((valObject.Acceleration > OldAcc) & (Rating > valObject.Agility)) //Decrement PassBlk, increment Run block due to weight gain
                            {
                                int BlockChange = ((valObject.Acceleration - OldAcc) + (Rating - OldAgi)) / 2;
                                valObject.RunBlocking = valObject.RunBlocking - BlockChange;
                                valObject.PassBlocking = valObject.PassBlocking + BlockChange;
                            }
                            valObject.Agility = Rating;
                        }

                        AttributeDifferential = decimal.Round(((decimal)valObject.Stamina / (decimal)StmMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StmMean / (decimal)valObject.Stamina), 4))) * 2) + (decimal)StmGain; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StmGain; }
                        Rating = (int)Math.Round(valObject.Stamina * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Stamina = Rating;
                        if (OldStm < valObject.Stamina) //Decrement Jump = Stamina loss /2
                        {
                            int JumpChange = (valObject.Stamina - OldStm) / 2;
                            valObject.Jumping = valObject.Jumping + JumpChange;
                            valObject.BreakTackle = valObject.BreakTackle - JumpChange; //BreakTackle drop due to mass loss
                        }

                        AttributeDifferential = decimal.Round(((decimal)valObject.Strength / (decimal)StrMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StrMean / (decimal)valObject.Strength), 4))) * 2) + (decimal)StrLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StrLoss; }
                        Rating = (int)Math.Round(valObject.Strength * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Strength = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Injury / (decimal)InjMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)InjMean / (decimal)valObject.Injury), 4))) * 2) + (decimal)InjGain; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)InjGain; }
                        Rating = (int)Math.Round(valObject.Injury * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Injury = Rating;

                        Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                        HighLowCheck(Rating);
                        valObject.BodyMuscle = Rating;
                        Rating = (int)(Math.Ceiling(valObject.BodyFat * (PercentModFat * (decimal)0.65)));
                        HighLowCheck(Rating);
                        valObject.BodyFat = Rating;
                        Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.ArmsMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.ArmsFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsThighMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsThighFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfFat = Rating;
                        Rating = (int)(Math.Round((valObject.BodyOverall * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.BodyOverall = Rating;


                    }

                  
                    //Add modified datarow to view before/after changes 
                    DataRow ac = RosterView.NewRow();
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;
                   

                    ac["Name"] = "      CURRENT-" + valObject.LastName;
                    ac["Pos"] = PosStr;
                    ac["Age"] = valObject.Age;
                    ac["Exp"] = valObject.YearsPro;
                    
                    // Would like to include feet/inches
                    ac["Hgt"] = valObject.Height;
                    ac["Wgt"] = valObject.Weight + 160;
                    ac["Spd"] = valObject.Speed;
                    ac["Acc"] = valObject.Acceleration;
                    ac["Agi"] = valObject.Agility;
                    ac["Str"] = valObject.Strength;
                    ac["Stm"] = valObject.Stamina;
                    ac["Inj"] = valObject.Injury;
                    ac["Tgh"] = valObject.Toughness;
                    ac["Mor"] = valObject.Morale;
                    ac["Awr"] = valObject.Awareness;
                    ac["Cat"] = valObject.Catching;
                    ac["Car"] = valObject.Carrying;
                    ac["Jmp"] = valObject.Jumping;
                    ac["Btk"] = valObject.BreakTackle;
                    ac["Tkl"] = valObject.Tackle;
                    ac["ThP"] = valObject.ThrowPower;
                    ac["ThA"] = valObject.ThrowAccuracy;
                    ac["Pbk"] = valObject.PassBlocking;
                    ac["Rbk"] = valObject.RunBlocking;
                    ac["KP"] = valObject.KickPower;
                    ac["KA"] = valObject.KickAccuracy;
                    ac["KR"] = valObject.KickReturn;

                    valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                    //Reload the overall rating
                    valObject.Overall = valObject.Overall;
                    ac["Ovr"] = valObject.Overall;

                    RosterView.Rows.Add(ac);

                    CellColor(4, acCounter, valObject.Overall, OldOvr);
                    CellColor(6, acCounter, valObject.Weight, (OldWgt - 160));
                    CellColor(7, acCounter, valObject.Speed, OldSpeed);
                    CellColor(8, acCounter, valObject.Acceleration, OldAcc);
                    CellColor(9, acCounter, valObject.Agility, OldAgi);
                    CellColor(10, acCounter, valObject.Strength, OldStr);
                    CellColor(11, acCounter, valObject.Stamina, OldStm);
                    CellColor(12, acCounter, valObject.Injury, OldInj);
                    CellColor(18, acCounter, valObject.Jumping, OldJmp);
                    CellColor(19, acCounter, valObject.BreakTackle, OldBtk);
                    CellColor(23, acCounter, valObject.PassBlocking, OldPbk);
                    CellColor(24, acCounter, valObject.RunBlocking, OldRbk);
                    CellColor(14, acCounter, valObject.Morale, OldMor);

                    acCounter = acCounter + 2;


                }


                // Sort the current view after running foreach loop unless view empty
                if (RosterView.Rows.Count != 0)
                {
                    //  depthChartDataGrid.Sort(depthChartDataGrid.Columns["Pos"], ListSortDirection.Descending);

                    depthChartDataGrid.CurrentCell = depthChartDataGrid[0, 0];
                }
                groupBox3.Enabled = true;
                           
            }          
          
            
            isInitialising = false;
         
        
            //Export Conditioning Report
            string installDirectory = Application.StartupPath;
            string fullFranchisePath = model.GetFileName();
            string[] splitPath = fullFranchisePath.Split('\\');
            string franchiseFilename = splitPath[splitPath.Length - 1];
            if (!Directory.Exists(installDirectory + "\\Conditioning\\" + franchiseFilename + "\\"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\" + franchiseFilename + "\\");
            }
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\" + franchiseFilename + "\\" + CPUteam.ToString() + ".txt"), false);
            // First we will write the headers.
            DataTable dt = RosterView;
            int iColCount = dt.Columns.Count;
            sw.Write(dt.Columns[0]);
            sw.Write("\t\t\t");
            for (int i = 1; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write("\t");
                }
            }
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write("\t");
                    }
                }
                sw.Write(sw.NewLine);
                sw.WriteLine();
               
            }
           /*
            if (checkBox2.Checked == false)
            {
                sw.Write("Coach Progression...");
                sw.WriteLine();
                sw.WriteLine("Name\t\tKnw\tMot\tChm\tEth\tQB\tRB\tWR\tOL\tDL\tLB\tDB\tK\tP\t");
                sw.Write(model.CoachModel.CurrentCoachRecord.Name + "\t\t" + oldKnw + "\t" + OldMot + "\t" + oldChm + "\t" + OldEth + "\t" + oldqb + "\t" + Oldrb + "\t" + oldwr + "\t" + Oldol + "\t" + olddl + "\t" + Oldlb + "\t" + olddb + "\t" + Oldk + "\t" + Oldp);
                sw.WriteLine();
                sw.WriteLine(model.CoachModel.CurrentCoachRecord.Name + "\t\t" + model.CoachModel.CurrentCoachRecord.Knowledge + "\t" + model.CoachModel.CurrentCoachRecord.Motivation + "\t" + model.CoachModel.CurrentCoachRecord.Chemistry + "\t" +
                   model.CoachModel.CurrentCoachRecord.Ethics + "\t" + model.CoachModel.CurrentCoachRecord.QuarterbackRating + "\t" + model.CoachModel.CurrentCoachRecord.RunningbackRating + "\t" + model.CoachModel.CurrentCoachRecord.WideReceiverRating + "\t" +
                   model.CoachModel.CurrentCoachRecord.OffensiveLineRating + "\t" + model.CoachModel.CurrentCoachRecord.DefensiveLineRating + "\t" + model.CoachModel.CurrentCoachRecord.LinebackerRating + "\t" + model.CoachModel.CurrentCoachRecord.DefensiveBackRating + "\t" + model.CoachModel.CurrentCoachRecord.KickerRating + "\t" +
                   model.CoachModel.CurrentCoachRecord.PuntRating);
            }
            sw.Close();
           */
        }
        private void PhaseOne()
        {
            InitializeDataGrids();
            int teamId = ((TeamRecord)SelectHumanTeam.SelectedItem).TeamId;
            int positionId = 0;
            int FatCoutner = 0;
            int MuscleCounter = 0;
            int acCounter = 1;
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
            //Load current teams coach
            model.CoachModel.SetPositionFilter(0);
            model.CoachModel.SetTeamFilter(SelectHumanTeam.SelectedItem.ToString());
            model.CoachModel.GetNextCoachRecord();
            int CoachMotivation = model.CoachModel.CurrentCoachRecord.Motivation;
            
            foreach (PlayerRecord valObject in teamPlayers)
            {
                WeightGain = false;
                WeightLoss = false;
                Rating = 0;
                Skill = 0;
                AttributeMeans(valObject.PositionId);
                CallWeightGain((valObject.Weight + 160), valObject.Age, valObject.PositionId, CoachMotivation, valObject.FirstName, valObject.LastName);
                CallWeightLoss((valObject.Weight + 160), valObject.Age, valObject.PositionId, CoachMotivation, valObject.FirstName, valObject.LastName);
                string CaptainFirst = "";
                string CaptainLast = "";
                if (OldCaptain != "")
                {
                    CaptainFirst = OldCaptain.Split(' ')[0];
                    CaptainLast = OldCaptain.Split(' ')[1];
                }
                if ((WeightGain == true & WeightLoss == false) | (WeightGain == false & WeightLoss == true) || ((BadNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName)) || ((GoodNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName)))
                {

                    if (WeightGain == true)
                    {
                        FatCoutner++;
                    }
                    else if (WeightLoss == true)
                    {
                        MuscleCounter++;
                    }

                    //Print Before row to DataGrid
                    DataRow dr = RosterView.NewRow();
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;

                    dr["Name"] = valObject.FirstName + " " + valObject.LastName;
                    dr["Pos"] = PosStr;
                    dr["Age"] = valObject.Age;
                    dr["Exp"] = valObject.YearsPro;
                    valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                    dr["Ovr"] = valObject.Overall;
                    // Would like to include feet/inches
                    dr["Hgt"] = valObject.Height;
                    dr["Wgt"] = valObject.Weight + 160;
                    dr["Spd"] = valObject.Speed;
                    dr["Acc"] = valObject.Acceleration;
                    dr["Agi"] = valObject.Agility;
                    dr["Str"] = valObject.Strength;
                    dr["Stm"] = valObject.Stamina;
                    dr["Inj"] = valObject.Injury;
                    dr["Tgh"] = valObject.Toughness;
                    dr["Mor"] = valObject.Morale;
                    dr["Awr"] = valObject.Awareness;
                    dr["Cat"] = valObject.Catching;
                    dr["Car"] = valObject.Carrying;
                    dr["Jmp"] = valObject.Jumping;
                    dr["Btk"] = valObject.BreakTackle;
                    dr["Tkl"] = valObject.Tackle;
                    dr["ThP"] = valObject.ThrowPower;
                    dr["ThA"] = valObject.ThrowAccuracy;
                    dr["Pbk"] = valObject.PassBlocking;
                    dr["Rbk"] = valObject.RunBlocking;
                    dr["KP"] = valObject.KickPower;
                    dr["KA"] = valObject.KickAccuracy;
                    dr["KR"] = valObject.BodyWeight;
                    // dr["KR"] = valObject.KickReturn;

                    RosterView.Rows.Add(dr);
                    int OldOvr = valObject.Overall;
                    int OldSpeed = valObject.Speed;
                    int OldAcc = valObject.Acceleration;
                    int OldAgi = valObject.Agility;
                    int OldStr = valObject.Strength;
                    int OldStm = valObject.Stamina;
                    int OldInj = valObject.Injury;
                    int OldJmp = valObject.Jumping;
                    int OldBtk = valObject.BreakTackle;
                    OldWgt = valObject.Weight + 160;
                    int OldPbk = valObject.PassBlocking;
                    int OldRbk = valObject.RunBlocking;
                    int OldMor = valObject.Morale;

                    decimal AttributeDifferential;

                    if (WeightGain == true)
                    {
                        AllocateWeight(CoachMotivation, valObject.Age);
                        PercentModFat = (Math.Abs((decimal)PercentModFat - 1) * (decimal)PosWgtMod) + 1;
                        PercentModMuscle = 2 - PercentModFat;
                        AddWgt = (int)(Math.Ceiling((valObject.Weight + 160) * (decimal)PercentModFat));
                        decimal WgtDifferential = decimal.Round((1 - ((decimal)OldWgt / (decimal)AddWgt)), 4); // *(decimal)1.35;

                        PercentModFat = Math.Round(((decimal)AddWgt / (decimal)OldWgt), 6) + (decimal).05;
                        PercentModMuscle = Math.Round(((decimal)OldWgt / (decimal)AddWgt), 6) - (decimal).05;

                        valObject.Weight = (AddWgt - 160);
                        Rating = (int)(Math.Ceiling(valObject.BodyWeight * PercentModFat));
                        HighLowCheck(Rating);
                        valObject.BodyWeight = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Speed / (decimal)SpdMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)SpdMean / (decimal)valObject.Speed), 4))) * 2) + (decimal)SpdLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)SpdLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Speed * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Speed = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Acceleration / (decimal)AccMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)AccMean / (decimal)valObject.Acceleration), 4))) * 2) + (decimal)AccLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AccLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Acceleration * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        OldAcc = valObject.Acceleration;
                        valObject.Acceleration = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Agility / (decimal)AgiMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = (((1 - (decimal.Round(((decimal)AgiMean / (decimal)valObject.Agility), 4))) * 2) + (decimal)AgiLoss) * (decimal)PosWgtMod; }
                        else
                        { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AgiLoss) * (decimal)PosWgtMod; }
                        Rating = (int)Math.Round(valObject.Agility * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        OldAgi = valObject.Agility;
                        if ((valObject.Acceleration < OldAcc) & (Rating < valObject.Agility)) //Increment PassBlk, decrement Run block due to weight loss
                        {
                            int BlockChange = ((OldAcc - valObject.Acceleration) + (OldAgi - Rating)) / 2;
                            valObject.RunBlocking = valObject.RunBlocking + BlockChange;
                            valObject.PassBlocking = valObject.PassBlocking - BlockChange;
                        }
                        valObject.Agility = Rating;


                        AttributeDifferential = decimal.Round(((decimal)valObject.Stamina / (decimal)StmMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StmMean / (decimal)valObject.Stamina), 4))) * 2) + (decimal)StmLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StmLoss; }
                        Rating = (int)Math.Round(valObject.Stamina * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Stamina = Rating;
                        if (OldStm > valObject.Stamina) //Decrement Jump = Stamina loss /2
                        {
                            int JumpChange = (OldStm - valObject.Stamina) / 2;
                            valObject.Jumping = valObject.Jumping - JumpChange;
                            valObject.BreakTackle = valObject.BreakTackle + JumpChange; //BreakTackle drop due to mass loss
                        }
                        AttributeDifferential = decimal.Round(((decimal)valObject.Injury / (decimal)InjMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)InjMean / (decimal)valObject.Injury), 4))) * 2) + (decimal)InjLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)InjLoss; }
                        Rating = (int)Math.Round(valObject.Injury * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Injury = Rating;

                        Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                        HighLowCheck(Rating);
                        valObject.BodyMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.BodyFat * (PercentModFat * (decimal)1.35))));
                        HighLowCheck(Rating);
                        valObject.BodyFat = Rating;
                        Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.ArmsMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.ArmsFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsThighMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsThighFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfFat = Rating;
                        Rating = (int)(Math.Round((valObject.BodyOverall * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.BodyOverall = Rating;


                    }
                    else if (WeightLoss == true)
                    {
                        AllocateLoss(CoachMotivation, valObject.Age);
                        PercentModFat = Math.Abs((Math.Abs((decimal)PercentModFat - 1) * (decimal)PosWgtMod) - 1);
                        PercentModMuscle = 2 - PercentModFat;
                        AddWgt = (int)(Math.Floor((valObject.Weight + 160) * (decimal)PercentModFat));
                        decimal WgtDifferential = decimal.Round((1 - ((decimal)AddWgt / (decimal)OldWgt)), 4) * (decimal)1.5;

                        PercentModFat = Math.Round(((decimal)OldWgt / (decimal)AddWgt), 6) - (decimal).05;
                        PercentModMuscle = Math.Round(((decimal)AddWgt / (decimal)OldWgt), 6) + (decimal).05;

                        valObject.Weight = (AddWgt - 160);
                        Rating = (int)(Math.Ceiling(valObject.BodyWeight * PercentModFat));
                        HighLowCheck(Rating);
                        valObject.BodyWeight = Rating;

                        if (valObject.Speed < SpdMean)//Spd increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Speed / (decimal)SpdMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)SpdMean / (decimal)valObject.Speed), 4))) * 2) + (decimal)SpdGain) * (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * (decimal)1.5) + (decimal)SpdGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Speed * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            valObject.Speed = Rating;
                        }

                        if ((valObject.Acceleration < AccMean) || (valObject.Speed > OldSpeed))//Acc increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Acceleration / (decimal)AccMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)AccMean / (decimal)valObject.Acceleration), 4))) * 2) + (decimal)AccGain) * (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AccGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Acceleration * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            OldAcc = valObject.Acceleration;
                            valObject.Acceleration = Rating;
                        }

                        if ((valObject.Agility < AgiMean) || (valObject.Speed > OldSpeed)) //Agi increase check
                        {
                            AttributeDifferential = decimal.Round(((decimal)valObject.Agility / (decimal)AgiMean), 4);
                            if (AttributeDifferential > 1)
                            { AttributeDifferential = (((1 - (decimal.Round(((decimal)AgiMean / (decimal)valObject.Agility), 4))) * 2) + (decimal)AgiGain) * (decimal)PosWgtMod; }
                            else
                            { AttributeDifferential = (((1 - AttributeDifferential) * 2) + (decimal)AgiGain) * (decimal)PosWgtMod; }
                            Rating = (int)Math.Round(valObject.Agility * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                            HighLowCheck(Rating);
                            OldAgi = valObject.Agility;
                            if ((valObject.Acceleration > OldAcc) & (Rating > valObject.Agility)) //Decrement PassBlk, increment Run block due to weight gain
                            {
                                int BlockChange = ((valObject.Acceleration - OldAcc) + (Rating - OldAgi)) / 2;
                                valObject.RunBlocking = valObject.RunBlocking - BlockChange;
                                valObject.PassBlocking = valObject.PassBlocking + BlockChange;
                            }
                            valObject.Agility = Rating;
                        }

                        AttributeDifferential = decimal.Round(((decimal)valObject.Stamina / (decimal)StmMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StmMean / (decimal)valObject.Stamina), 4))) * 2) + (decimal)StmGain; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StmGain; }
                        Rating = (int)Math.Round(valObject.Stamina * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Stamina = Rating;
                        if (OldStm < valObject.Stamina) //Decrement Jump = Stamina loss /2
                        {
                            int JumpChange = (valObject.Stamina - OldStm) / 2;
                            valObject.Jumping = valObject.Jumping + JumpChange;
                            valObject.BreakTackle = valObject.BreakTackle - JumpChange; //BreakTackle drop due to mass loss
                        }

                        AttributeDifferential = decimal.Round(((decimal)valObject.Strength / (decimal)StrMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)StrMean / (decimal)valObject.Strength), 4))) * 2) + (decimal)StrLoss; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)StrLoss; }
                        Rating = (int)Math.Round(valObject.Strength * (1 - ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Strength = Rating;

                        AttributeDifferential = decimal.Round(((decimal)valObject.Injury / (decimal)InjMean), 4);
                        if (AttributeDifferential > 1)
                        { AttributeDifferential = ((1 - (decimal.Round(((decimal)InjMean / (decimal)valObject.Injury), 4))) * 2) + (decimal)InjGain; }
                        else
                        { AttributeDifferential = ((1 - AttributeDifferential) * 2) + (decimal)InjGain; }
                        Rating = (int)Math.Round(valObject.Injury * (1 + ((decimal)WgtDifferential * (decimal)AttributeDifferential)));
                        HighLowCheck(Rating);
                        valObject.Injury = Rating;

                        Rating = (int)(Math.Floor(valObject.BodyMuscle * PercentModMuscle));
                        HighLowCheck(Rating);
                        valObject.BodyMuscle = Rating;
                        Rating = (int)(Math.Ceiling(valObject.BodyFat * (PercentModFat * (decimal)0.65)));
                        HighLowCheck(Rating);
                        valObject.BodyFat = Rating;
                        Rating = (int)(Math.Floor((valObject.ArmsMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.ArmsMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.ArmsFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.ArmsFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsThighMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsThighMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsThighFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsThighFat = Rating;
                        Rating = (int)(Math.Floor((valObject.LegsCalfMuscle * PercentModMuscle)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfMuscle = Rating;
                        Rating = (int)(Math.Ceiling((valObject.LegsCalfFat * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.LegsCalfFat = Rating;
                        Rating = (int)(Math.Round((valObject.BodyOverall * PercentModFat)));
                        HighLowCheck(Rating);
                        valObject.BodyOverall = Rating;


                    }

                    //Determine if Morale dropped due to offseason scenario
                    if ((valObject.FirstName == ScenarioFirstName) & (valObject.LastName == ScenarioLastName))
                    {
                        if ((BadNews == true) & (HorribleNews == false))
                        {
                            Rating = (valObject.Morale - (int)(11 * random.NextDouble() + (10 + Diff)));
                            HighLowCheck(Rating);
                            valObject.Morale = Rating;
                        }
                        else if (HorribleNews == true)
                        {
                            Rating = (valObject.Morale - ((int)(16 * random.NextDouble() + (20 + Diff))));
                            HighLowCheck(Rating);
                            valObject.Morale = Rating;
                        }
                        if ((GoodNews == true) & (GreatNews == false) & (valObject.Morale != 100))
                        {
                            Rating = (valObject.Morale + ((int)(11 * random.NextDouble() + (5 + Diff))));
                            HighLowCheck(Rating);
                            valObject.Morale = Rating;
                        }
                        else if ((GreatNews == true) & (valObject.Morale != 100))
                        {
                            Rating = (valObject.Morale + ((int)(16 * random.NextDouble() + (15 + Diff))));
                            HighLowCheck(Rating);
                            valObject.Morale = Rating;
                        }
                    }

                    if ((GoodNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName))
                    {
                        Diff = (((model.CoachModel.CurrentCoachRecord.Chemistry - 70) / 6) * -1);
                        Rating = (valObject.Morale - ((int)(16 * random.NextDouble() + (15 + Diff))));
                        HighLowCheck(Rating);
                        valObject.Morale = Rating;
                    }
                    if ((BadNews == true) & (CaptainFirst == valObject.FirstName) & (CaptainLast == valObject.LastName))
                    {
                        Diff = ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 6);
                        Rating = (valObject.Morale + ((int)(16 * random.NextDouble() + (10 + Diff))));
                        HighLowCheck(Rating);
                        valObject.Morale = Rating;
                    }
                    //Add modified datarow to view before/after changes 
                    DataRow ac = RosterView.NewRow();
                    // playerHeightComboBox.SelectedIndex = record.Height - 65;


                    ac["Name"] = "      CURRENT-" + valObject.LastName;
                    ac["Pos"] = PosStr;
                    ac["Age"] = valObject.Age;
                    ac["Exp"] = valObject.YearsPro;

                    // Would like to include feet/inches
                    ac["Hgt"] = valObject.Height;
                    ac["Wgt"] = valObject.Weight + 160;
                    ac["Spd"] = valObject.Speed;
                    ac["Acc"] = valObject.Acceleration;
                    ac["Agi"] = valObject.Agility;
                    ac["Str"] = valObject.Strength;
                    ac["Stm"] = valObject.Stamina;
                    ac["Inj"] = valObject.Injury;
                    ac["Tgh"] = valObject.Toughness;
                    ac["Mor"] = valObject.Morale;
                    ac["Awr"] = valObject.Awareness;
                    ac["Cat"] = valObject.Catching;
                    ac["Car"] = valObject.Carrying;
                    ac["Jmp"] = valObject.Jumping;
                    ac["Btk"] = valObject.BreakTackle;
                    ac["Tkl"] = valObject.Tackle;
                    ac["ThP"] = valObject.ThrowPower;
                    ac["ThA"] = valObject.ThrowAccuracy;
                    ac["Pbk"] = valObject.PassBlocking;
                    ac["Rbk"] = valObject.RunBlocking;
                    ac["KP"] = valObject.KickPower;
                    ac["KA"] = valObject.KickAccuracy;
                    ac["KR"] = valObject.KickReturn;

                    valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                    //Reload the overall rating
                    valObject.Overall = valObject.Overall;
                    ac["Ovr"] = valObject.Overall;

                    RosterView.Rows.Add(ac);

                    CellColor(4, acCounter, valObject.Overall, OldOvr);
                    CellColor(6, acCounter, valObject.Weight, (OldWgt - 160));
                    CellColor(7, acCounter, valObject.Speed, OldSpeed);
                    CellColor(8, acCounter, valObject.Acceleration, OldAcc);
                    CellColor(9, acCounter, valObject.Agility, OldAgi);
                    CellColor(10, acCounter, valObject.Strength, OldStr);
                    CellColor(11, acCounter, valObject.Stamina, OldStm);
                    CellColor(12, acCounter, valObject.Injury, OldInj);
                    CellColor(18, acCounter, valObject.Jumping, OldJmp);
                    CellColor(19, acCounter, valObject.BreakTackle, OldBtk);
                    CellColor(23, acCounter, valObject.PassBlocking, OldPbk);
                    CellColor(24, acCounter, valObject.RunBlocking, OldRbk);
                    CellColor(14, acCounter, valObject.Morale, OldMor);

                    acCounter = acCounter + 2;


                }


                // Sort the current view after running foreach loop unless view empty
                if (RosterView.Rows.Count != 0)
                {
                    //  depthChartDataGrid.Sort(depthChartDataGrid.Columns["Pos"], ListSortDirection.Descending);

                    depthChartDataGrid.CurrentCell = depthChartDataGrid[0, 0];
                }
                groupBox3.Enabled = true;
                label4.Text = FatCoutner + " player(s) are in worse condition than when last season ended.";
                label5.Text = MuscleCounter + " player(s) showed up to camp in better condition than when last season ended.";
            }
            string OwnerFeedback = "";
            textBox1.Visible = true;
            //negative commentary
            if (FatCoutner > MuscleCounter)
            {
                if ((Math.Abs(FatCoutner - MuscleCounter) <= (int)1))
                {
                    OwnerFeedback = ("Message from ownership...We're a little disappointed with head coach " + model.CoachModel.CurrentCoachRecord.Name + "'s inability to motivate our players this past offseason." + FatCoutner + " player(s) reporting to camp overweight isn't too bad but we'd like to see better next offseason.");
                }
                else if ((Math.Abs(FatCoutner - MuscleCounter) <= (int)3))
                {
                    OwnerFeedback = ("Message from ownership...We're disappointed with head coach " + model.CoachModel.CurrentCoachRecord.Name + "'s failure to motivate our players this past offseason." + FatCoutner + " player(s) reporting to camp overweight is unacceptable.");
                }
                else if ((Math.Abs(FatCoutner - MuscleCounter) > (int)3))
                {
                    OwnerFeedback = ("Message from ownership...We're extremely upset with head coach " + model.CoachModel.CurrentCoachRecord.Name + "'s complete failure to motivate our players this past offseason." + FatCoutner + " player(s) reporting to camp overweight is ridiculous. This trend must change.");
                }
            }
            //positive commentary
            if (MuscleCounter > FatCoutner)
            {
                if ((Math.Abs(MuscleCounter - FatCoutner) <= (int)1))
                {
                    OwnerFeedback = ("Message from ownership...At least we had more guys show up to camp in better shape then they were in when last season ended. Head coach " + model.CoachModel.CurrentCoachRecord.Name + " did an acceptable job of motivating our players this past offseason. A " + MuscleCounter + "/" + FatCoutner + " loss/gain is good.");
                }
                else if ((Math.Abs(MuscleCounter - FatCoutner) <= (int)3))
                {
                    OwnerFeedback = ("Message from ownership..." + model.CoachModel.CurrentCoachRecord.Name + " did a great job the past couple months reaching out to our players and encouraging them to not only maintain their conditioning but in some cases even improve upon it." + MuscleCounter + " player(s) reporting to camp in better shape than last year shows us this coach has the team focused on winning.");
                }
                else if ((Math.Abs(MuscleCounter - FatCoutner) > (int)3))
                {
                    OwnerFeedback = ("Message from ownership...You've clearly got an eye for motivational talent." + model.CoachModel.CurrentCoachRecord.Name + " has been supremely focused on staying on top of our guys and we can now use training camp to sharpen the player's skills without having to waste valuable time working the team back into shape.");
                }
            }
            //Neutrel
            if (MuscleCounter == FatCoutner)
            {
                double NeutralRoll = (2 * random.NextDouble()) + 1;
                if (NeutralRoll == 1)
                {
                    OwnerFeedback = ("Message from ownership...We need more motivational spark out of " + model.CoachModel.CurrentCoachRecord.Name + ".But at least it's a wash in terms of loss/gain. We won't have to spend that much camp time working them back into shape...");
                }
                else
                {
                    OwnerFeedback = ("Message from ownership...The number of players reporting to camp heavier than last year equals those that " + model.CoachModel.CurrentCoachRecord.Name + " reached out to this past offseason.\nWe'd like better Motivation next year to get a jump on our division..");
                }
            }

            isInitialising = false;
            textBox1.Text = textBox1.Text + "\r\n\r\n" + OwnerFeedback + CurrentDialog;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
            //Disable checkbox
            checkBox1.Enabled = false;
            SelectHumanTeam.Enabled = false;

            //Export Conditioning Report
            string installDirectory = Application.StartupPath;
            string fullFranchisePath = model.GetFileName();
            string[] splitPath = fullFranchisePath.Split('\\');
            string franchiseFilename = splitPath[splitPath.Length - 1];
            if (!Directory.Exists(installDirectory + "\\Conditioning\\" + franchiseFilename + "\\"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\" + franchiseFilename + "\\");
            }
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter((installDirectory + "\\Conditioning\\" + franchiseFilename + "\\" + SelectHumanTeam.Text + ".txt"), false);
            // First we will write the headers.
            DataTable dt = RosterView;
            int iColCount = dt.Columns.Count;
            sw.Write(dt.Columns[0]);
            sw.Write("\t\t\t");
            for (int i = 1; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write("\t");
                }
            }
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < iColCount - 1)
                    {
                        sw.Write("\t");
                    }
                }
                sw.Write(sw.NewLine);
                sw.WriteLine();
                
            }
            /*
            if (checkBox2.Checked == false)
            {
                sw.Write("Coach Progression...");
                sw.WriteLine();
                sw.WriteLine("Name\t\tKnw\tMot\tChm\tEth\tQB\tRB\tWR\tOL\tDL\tLB\tDB\tK\tP\t");
                sw.Write(model.CoachModel.CurrentCoachRecord.Name + "\t\t" + oldKnw + "\t" + OldMot + "\t" + oldChm + "\t" + OldEth + "\t" + oldqb + "\t" + Oldrb + "\t" + oldwr + "\t" + Oldol + "\t" + olddl + "\t" + Oldlb + "\t" + olddb + "\t" + Oldk + "\t" + Oldp);
                sw.WriteLine();
                sw.WriteLine(model.CoachModel.CurrentCoachRecord.Name + "\t\t" + model.CoachModel.CurrentCoachRecord.Knowledge + "\t" + model.CoachModel.CurrentCoachRecord.Motivation + "\t" + model.CoachModel.CurrentCoachRecord.Chemistry + "\t" +
                   model.CoachModel.CurrentCoachRecord.Ethics + "\t" + model.CoachModel.CurrentCoachRecord.QuarterbackRating + "\t" + model.CoachModel.CurrentCoachRecord.RunningbackRating + "\t" + model.CoachModel.CurrentCoachRecord.WideReceiverRating + "\t" +
                   model.CoachModel.CurrentCoachRecord.OffensiveLineRating + "\t" + model.CoachModel.CurrentCoachRecord.DefensiveLineRating + "\t" + model.CoachModel.CurrentCoachRecord.LinebackerRating + "\t" + model.CoachModel.CurrentCoachRecord.DefensiveBackRating + "\t" + model.CoachModel.CurrentCoachRecord.KickerRating + "\t" +
                   model.CoachModel.CurrentCoachRecord.PuntRating);
            }
            sw.Close();
             */
            if (ScenarioFirstName == "")
            {
             groupBox5.Visible = true;
            }
        }
        private void HighLowCheck(int Skill)
        {
            if ((int)Skill > 99)
            {Rating = 99;}
            else if ((int)Skill < 0)
            {Rating = 0;}                     
        }
        private DataColumn AddColumn(string ColName, string ColType)
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = ColName;
            dc.DataType = System.Type.GetType(ColType);
            return dc;
        }
        private void CellColor(int Col, int Row, int Attribute, int OldAttribute)
        {
            depthChartDataGrid.CurrentCell = depthChartDataGrid[Col, Row];

            if (Attribute > OldAttribute)
            {depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Blue;}
            else if (Attribute < OldAttribute)
            {depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Red;}

            if (Col == 6)
            {
                if (Attribute > OldAttribute)
                { depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Red; }
                else if (Attribute < OldAttribute)
                { depthChartDataGrid.CurrentCell.Style.ForeColor = Color.Blue; }
            }
        }
        private void RandomIssue()
        {
            double Issue = Math.Round((100 * random.NextDouble()) + 1);            
            bool ScenarioTrue = false;
            knwUpDown.Enabled = false;
            motUpDown.Enabled = false;
            chmUpDown.Enabled = false;
            ethUpDown.Enabled = false;
            qbUpDown.Enabled = false;
            rbUpDown.Enabled = false;
            wrUpDown.Enabled = false;
            olUpDown.Enabled = false;
            dlUpDown.Enabled = false;
            lbUpDown.Enabled = false;
            dbUpDown.Enabled = false;
            kUpDown.Enabled = false;
            pUpDown.Enabled = false;

            if (DisableScenarioChk.Checked == false)//Skip OffSeason Scenario mode if check box checked
            {
                if (Issue <= 25)
                {
                    SelectHumanTeam.Enabled = false;
                    checkBox1.Enabled = false;
                    textBox1.Visible = true;
                    string Scenario = "";
                    model.PlayerModel.SetTeamFilter(SelectHumanTeam.Text);
                    model.PlayerModel.GetNextPlayerRecord();
                    int teamId = ((TeamRecord)SelectHumanTeam.SelectedItem).TeamId;
                    int positionId = 0;
                    int OverallRating = 99;
                    List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
                    int PlayerCounter = 0;

                    double GoodBad = Math.Round((10 * random.NextDouble()) + 1);//Determine which scenario is true

                    if (GoodBad <= 6) //Scenario 1; player slacking
                    {
                        while ((model.PlayerModel.CurrentPlayerRecord.Overall < OverallRating) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 19) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 20) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 3) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 16))
                        {
                            model.PlayerModel.SetTeamFilter(SelectHumanTeam.Text);
                            model.PlayerModel.GetNextPlayerRecord();
                            PlayerCounter++;
                            if (PlayerCounter == teamPlayers.Count)
                            {
                                PlayerCounter = 0;
                                OverallRating = OverallRating - 3;
                                model.PlayerModel.SetTeamFilter(SelectHumanTeam.Text);
                                model.PlayerModel.GetNextPlayerRecord();
                            }
                        }

                        model.CoachModel.SetPositionFilter(0);
                        model.CoachModel.SetTeamFilter(SelectHumanTeam.SelectedItem.ToString());
                        model.CoachModel.GetNextCoachRecord();
                        CoachMotivation = model.CoachModel.CurrentCoachRecord.Motivation;
                        ScenarioFirstName = model.PlayerModel.CurrentPlayerRecord.FirstName;
                        ScenarioLastName = model.PlayerModel.CurrentPlayerRecord.LastName;
                        ScenarioOvr = model.PlayerModel.CurrentPlayerRecord.Overall;
                        // ScenarioPos = model.PlayerModel.CurrentPlayerRecord.PositionId.ToString();


                        NegativeScenario1 = true;
                        ScenarioTrue = true;
                        Scenario = ("\n...Sometime during the offseason...\n\n\nLocation...\n\n" + SelectHumanTeam.SelectedItem.ToString() + " Training Camp facility...\n\nYou're approached by a member of the " + SelectHumanTeam.SelectedItem.ToString() + " board of directors.\n''I'm hearing whispers that a certain player on our team has been less than enthusiastic in his\nworkouts.'' He nervously glances around ensuring you're alone...\n\n\n''" + ScenarioFirstName + " " + ScenarioLastName + " (" + ScenarioOvr + " Ovr) he says. You need to address this before it gets out of control.''\n\nRefer to the upper Right corner of this form ('Offseason Scenario...') for further instruction...");
                        groupBox4.Visible = true;
                        radioButton1.Text = "Phone the players agent. (High chance of contacting player but watch out.\nMost agents have agendas that don't always have the team in mind...)";
                        radioButton2.Text = "Anonymously call " + ScenarioLastName + " out in the media. (You'll definetely get his attention, but it may\nalienate him even further if not a solid Motivator...)";
                        radioButton3.Text = "Circumvent player's agent and arrange a sit down meeting with " + ScenarioFirstName + " " + ScenarioLastName + ".(Only the best\nMotivators should try this approach. However if he shows up you'll stand a high chance of success..)";
                        radioButton4.Text = "Ignore situation and do nothing. (Sometimes tough love is best...)";
                        radioButton4.Checked = true;
                        textBox1.Text = textBox1.Text + "Your team's head coach, " + model.CoachModel.CurrentCoachRecord.Name + ", has a Motivation rating of " + model.CoachModel.CurrentCoachRecord.Motivation + "...";
                        MessageBox.Show(Scenario);
                    }
                    else if (GoodBad > 6) //Scenario 2; Captains
                    {
                        NegativeScenario2 = true;
                        ScenarioTrue = true;
                        teamCaptainRecord = null;
                        SortedList<string, PlayerRecord> list = new SortedList<string, PlayerRecord>();
                        foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
                        {
                            if (record.Deleted)
                            {
                                continue;
                            }
                            PlayerRecord player = (PlayerRecord)record;

                            if (player.TeamId == teamId)
                            {
                                try
                                {
                                    list.Add(player.ToString(), player);
                                }
                                catch (ArgumentException e)
                                {
                                    Trace.WriteLine(e.ToString());
                                }
                            }
                        }
                        foreach (TableRecordModel record in model.TableModels[EditorModel.TEAM_CAPTAIN_TABLE].GetRecords())
                        {
                            if (((TeamCaptainRecord)record).TeamId == teamId)
                            {
                                teamCaptainRecord = (TeamCaptainRecord)record;

                                break;
                            }
                        }
                        if (teamCaptainRecord != null)
                        {
                            foreach (PlayerRecord player in list.Values)
                            {
                                if (player.PlayerId == teamCaptainRecord.Captain1)
                                {
                                    Captain1 = player.ToString();
                                }
                                if (player.PlayerId == teamCaptainRecord.Captain2)
                                {
                                    Captain2 = player.ToString();
                                }
                                if (player.PlayerId == teamCaptainRecord.Captain3)
                                {
                                    Captain3 = player.ToString();
                                }
                            }
                        }
                        else
                        {
                            Captain1 = "";
                            Captain2 = "";
                            Captain3 = "";
                        }
                        if ((Captain1 != "") & (Captain2 != ""))
                        {
                            while ((model.PlayerModel.CurrentPlayerRecord.Overall < OverallRating) || (model.PlayerModel.CurrentPlayerRecord.FirstName == Captain1.Split(' ')[0]) || (model.PlayerModel.CurrentPlayerRecord.LastName == Captain1.Split(' ')[1]) || (model.PlayerModel.CurrentPlayerRecord.FirstName == Captain2.Split(' ')[0]) || (model.PlayerModel.CurrentPlayerRecord.LastName == Captain2.Split(' ')[1]) || (model.PlayerModel.CurrentPlayerRecord.FirstName == Captain3.Split(' ')[0]) || (model.PlayerModel.CurrentPlayerRecord.LastName == Captain3.Split(' ')[1]) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 19) || (model.PlayerModel.CurrentPlayerRecord.PositionId == 20))
                            {
                                model.PlayerModel.SetTeamFilter(SelectHumanTeam.Text);
                                model.PlayerModel.GetNextPlayerRecord();
                                PlayerCounter++;
                                if (PlayerCounter == teamPlayers.Count)
                                {
                                    PlayerCounter = 0;
                                    OverallRating = OverallRating - 3;
                                    model.PlayerModel.SetTeamFilter(SelectHumanTeam.Text);
                                    model.PlayerModel.GetNextPlayerRecord();
                                }
                            }
                            groupBox4.Visible = true;
                            model.CoachModel.SetPositionFilter(0);
                            model.CoachModel.SetTeamFilter(SelectHumanTeam.SelectedItem.ToString());
                            model.CoachModel.GetNextCoachRecord();
                            CoachMotivation = model.CoachModel.CurrentCoachRecord.Motivation;
                            ScenarioFirstName = model.PlayerModel.CurrentPlayerRecord.FirstName;
                            ScenarioLastName = model.PlayerModel.CurrentPlayerRecord.LastName;
                            ScenarioOvr = model.PlayerModel.CurrentPlayerRecord.Overall;
                            NewCaptain = model.PlayerModel.CurrentPlayerRecord.PlayerId;
                            if ((model.PlayerModel.CurrentPlayerRecord.PositionId == 0) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 1) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 2) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 3) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 4) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 5) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 6) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 7) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 8) | (model.PlayerModel.CurrentPlayerRecord.PositionId == 9))
                            {
                                PosStr = "Offensive";
                                OldCaptain = Captain1;
                            }
                            else
                            {
                                PosStr = "Defensive";
                                OldCaptain = Captain2;
                            }
                            Scenario = ("\n...Sometime during the offseason...\n\n\nLocation...\n\n" + SelectHumanTeam.SelectedItem.ToString() + " Training Camp facility...\n\nYou're enjoying your morning cup of coffee while going over your plans for running\nthis year's training camp when the door to your office suddenly bursts open.\nStorming into the room is " + ScenarioFirstName + " " + ScenarioLastName + " (" + ScenarioOvr + " Ovr), one of your better players.\nHe doesn't look happy...\n''Coach, I feel...I feel that I deserve to be named " + PosStr + " Captain.''");
                            if ((PosStr == "Offensive") & (Captain1 == ""))
                            {
                                Scenario = Scenario + ("\n\n''I mean we don't even currently have an offensive team captain. C'mon coach,\nname me captain and I'll report to camp in the best shape of my life...''");
                                radioButton2.Text = "Name " + ScenarioFirstName + " " + ScenarioLastName + " your new " + PosStr + " Captain...";
                                radioButton3.Text = "Refuse " + ScenarioLastName + "'s demand and tell him you have someone else in mind. He will be\ndisappointed though Motivation/Chemistry will alter effects...";
                                radioButton4.Text = "Dress " + ScenarioLastName + " down for having the nerve to try forcing the coach's hand. Tell " + ScenarioFirstName + " football is\na game of discipline and he clearly lacks any. Recommended only for excellent Motivator/Chemistry...";
                            }
                            else if ((PosStr == "Defensive") & (Captain2 == ""))
                            {
                                Scenario = Scenario + ("\n\n''I mean we don't even currently have an defensive team captain. C'mon coach,\nname me captain and I'll report to camp in the best shape of my life...''");
                                radioButton2.Text = "Name " + ScenarioFirstName + " " + ScenarioLastName + " your new " + PosStr + " Captain...";
                                radioButton3.Text = "Refuse " + ScenarioLastName + "'s demand and tell him you have someone else in mind. He will be\ndisappointed though Motivation/Chemistry will alter effects...";
                                radioButton4.Text = "Dress " + ScenarioLastName + " down for having the nerve to try forcing the coach's hand. Tell " + ScenarioFirstName + " football is\na game of discipline and he clearly lacks any. Recommended only for excellent Motivator/Chemistry...";

                            }
                            else if ((PosStr == "Offensive") & (Captain1 != ""))
                            {
                                Scenario = Scenario + ("\n\n''I've worked so hard to get to the point I'm at but I still don't feel\nthe " + SelectHumanTeam.SelectedItem.ToString() + " respect me. I want to be a leader and feel I can be more\neffective than our current offensive captain, " + Captain1 + "''");
                                radioButton2.Text = "Name " + ScenarioFirstName + " " + ScenarioLastName + " your new " + PosStr + " Captain. Be prepared for a negative\nreaction from your current " + PosStr + " Captain " + Captain1 + "...";
                                radioButton3.Text = "Refuse " + ScenarioLastName + "'s demand and tell him you have full confidence in your current\n" + PosStr + " Captain " + Captain1 + ". " + ScenarioLastName + " will likely be angry...";
                                radioButton4.Text = "Dress " + ScenarioLastName + " down for having the nerve to try forcing the coach's hand. Tell " + ScenarioFirstName + " football is\na game of discipline and he clearly lacks any. Recommended only for excellent Motivator/Chemistry...";

                            }
                            else if ((PosStr == "Defensive") & (Captain2 != ""))
                            {
                                Scenario = Scenario + ("\n\n''I've worked so hard to get to the point I'm at but I still don't feel\nthe " + SelectHumanTeam.SelectedItem.ToString() + " respect me. I want to be a leader and feel I can be more\neffective than our current defensive captain, " + Captain2 + ".''");
                                radioButton2.Text = "Name " + ScenarioFirstName + " " + ScenarioLastName + " your new " + PosStr + " Captain. Be prepared for a negative\nreaction from your current " + PosStr + " Captain " + Captain2 + "...";
                                radioButton3.Text = "Refuse " + ScenarioLastName + "'s demand and tell him you have full confidence in your current\n" + PosStr + " Captain " + Captain2 + ". " + ScenarioLastName + " will likely be angry...";
                                radioButton4.Text = "Dress " + ScenarioLastName + " down for having the nerve to try forcing the coach's hand. Tell " + ScenarioFirstName + " football is\na game of discipline and he clearly lacks any. Recommended only for excellent Motivator/Chemistry...";

                            }
                            Scenario = Scenario + ("\n\nYou have a decision to make. Please refer to the 'Offseason scenario' section\nof this form for further instruction, located in the upper right hand corner.");
                            radioButton1.Visible = false;
                            radioButton2.Checked = true;
                            textBox1.Text = textBox1.Text + "Your team's head coach, " + model.CoachModel.CurrentCoachRecord.Name + ", has a Motivation rating of " + model.CoachModel.CurrentCoachRecord.Motivation + " and a Team Chemistry rating of " + model.CoachModel.CurrentCoachRecord.Chemistry + "...\r\n...Motivation as well as Team Chemistry will alter the effects of your decision accordingly...";

                            MessageBox.Show(Scenario);
                        }
                        else
                        {
                            ScenarioTrue = false;
                        }
                    }
                }
            }
            if ((ScenarioTrue == false) & ((Captain1 == "") & (Captain2 == "")))
            {
            textBox1.Text = textBox1.Text + "\r\nYour team's head coach, " + model.CoachModel.CurrentCoachRecord.Name + ", has a Motivation rating of " + model.CoachModel.CurrentCoachRecord.Motivation + "...";
             PhaseOne(); 
            }            
        }
        private void AllocateWeight(int Cmot, int Age)
        {

            PercentModFat = 0;
            // record.BodyWeight; record.BodyMuscle; record.BodyFat; record.ArmsMuscle; record.ArmsFat; record.LegsThighMuscle; record.LegsThighFat; record.LegsCalfMuscle; record.LegsCalfFat; record.RearRearFat; record.RearShape; record.BodyOverall;
            int AddedWeight = (int)((95 * random.NextDouble() + GainAmountSld.Value) - (Cmot - 70) + (Age - 25));

            if ((BadNews == true) & (HorribleNews == false))
            {
                AddedWeight = AddedWeight + (int)((15 * random.NextDouble() + (GainAmountSld.Value + 15)));
            }
            else if ((BadNews == true) & (HorribleNews == true))
            {
                AddedWeight = AddedWeight + (int)((15 * random.NextDouble() + (GainAmountSld.Value + 25)));
            }

            if (AddedWeight < 0)
            {
                AddedWeight = 5;
            }

            if (AddedWeight < 20)
            {
                PercentModFat = ((decimal)((int)(4 * random.NextDouble() + 5)) / 1000) + 1;
            }
            else if (AddedWeight < 40)
            {
                PercentModFat = ((decimal)((int)(5 * random.NextDouble() + 7)) / 1000) + 1;
            }
            else if (AddedWeight < 50)
            {
                PercentModFat = ((decimal)((int)(6 * random.NextDouble() + 13)) / 1000) + 1;
            }
            else if (AddedWeight < 60)
            {
                PercentModFat = ((decimal)((int)(6 * random.NextDouble() + 18)) / 1000) + 1;
            }
            else if (AddedWeight < 70)
            {
                PercentModFat = ((decimal)((int)(6 * random.NextDouble() + 22)) / 1000) + 1;
            }
            else if (AddedWeight < 80)
            {
                PercentModFat = ((decimal)((int)(6 * random.NextDouble() + 26)) / 1000) + 1;
            }
            else if (AddedWeight < 85)
            {
                PercentModFat = ((decimal)((int)(6 * random.NextDouble() + 30)) / 1000) + 1;
            }
            else if (AddedWeight < 90)
            {
                PercentModFat = ((decimal)((int)(7 * random.NextDouble() + 33)) / 1000) + 1;
            }
            else if (AddedWeight < 93)
            {
                PercentModFat = ((decimal)((int)(8 * random.NextDouble() + 36)) / 1000) + 1;
            }
            else if (AddedWeight <= 96)
            {
                PercentModFat = ((decimal)((int)(9 * random.NextDouble() + 39)) / 1000) + 1;
            }
            else if (AddedWeight > 96)
            {
                PercentModFat = ((decimal)((int)(9 * random.NextDouble() + 39)) / 1000) + 1;
            }
        }
        private void AllocateLoss(int Cmot, int Age)
        {

            PercentModMuscle = 0;
            // record.BodyWeight; record.BodyMuscle; record.BodyFat; record.ArmsMuscle; record.ArmsFat; record.LegsThighMuscle; record.LegsThighFat; record.LegsCalfMuscle; record.LegsCalfFat; record.RearRearFat; record.RearShape; record.BodyOverall;
            int WeightLost = (int)((95 * random.NextDouble() + LossAmountSld.Value) - (Cmot - 70) - (Age - 25));

            if ((GoodNews == true) & (GreatNews == false))//Single out Scenario player if applicable
            {
                WeightLost = WeightLost + (int)((10 * random.NextDouble() + (LossAmountSld.Value + 12)));
            }
            else if ((GoodNews == true) & (GreatNews == true))
            {
                WeightLost = WeightLost + (int)((10 * random.NextDouble() + (LossAmountSld.Value + 22)));
            }

            if (WeightLost < 0)
            {
                WeightLost = 5;
            }

            if (WeightLost < 20)
            {
                PercentModFat = 1 - ((decimal)((int)(10 * random.NextDouble() + 5)) / 1000);

            }
            else if (WeightLost < 30)
            {
                PercentModFat = 1 - ((decimal)((int)(10 * random.NextDouble() + 6)) / 1000);
            }
            else if (WeightLost < 40)
            {
                PercentModFat = 1 - ((decimal)((int)(10 * random.NextDouble() + 7)) / 1000);
            }
            else if (WeightLost < 55)
            {
                PercentModFat = 1 - ((decimal)((int)(11 * random.NextDouble() + 8)) / 1000);
            }
            else if (WeightLost < 65)
            {
                PercentModFat = 1 - ((decimal)((int)(11 * random.NextDouble() + 10)) / 1000);
            }
            else if (WeightLost < 80)
            {
                PercentModFat = 1 - ((decimal)((int)(12 * random.NextDouble() + 14)) / 1000);
            }
            else if (WeightLost < 85)
            {
                PercentModFat = 1 - ((decimal)((int)(12 * random.NextDouble() + 16)) / 1000);
            }
            else if (WeightLost < 88)
            {
                PercentModFat = 1 - ((decimal)((int)(14 * random.NextDouble() + 18)) / 1000);
            }
            else if (WeightLost < 91)
            {
                PercentModFat = 1 - ((decimal)((int)(14 * random.NextDouble() + 20)) / 1000);
            }
            else if (WeightLost < 94)
            {
                PercentModFat = 1 - ((decimal)((int)(16 * random.NextDouble() + 22)) / 1000);
            }
            else if (WeightLost >= 94)
            {
                PercentModFat = 1 -((decimal)((int)(16 * random.NextDouble() + 24)) / 1000) ;
            }
        }         
        private void TrainingCampForm_Load(object sender, EventArgs e)
        {
            InitializeDataGrids();
        }
        private void SelectHumanTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox2.Enabled = true;
          //  textBox1.Text = ("It's a short offseason in today's NFL and it doesn't leave much time for you, as coach, to prepare for the seemingly endless array of tasks, meetings and film study you're routinely bombarded with. You must begin preparing for Training Camp even though its still months away. You've got about one week to focus on your skills so using what precious few hours of time you have let's get started on your Training Camp planning. Please allocate the alloted time to your Head Coach as all listed coaching ratings play into Training Camp player development including the positional ratings. You have 30 'hours' to distribute. Knowledge,Motivation,Chemistry and Ethics consume 2 hours per rank. Less experienced head coaches are more apt to see attribute increases than those coaches with more experience. Or choose to skip coach progression by simply checking 'Check to skip coach progression' then clicking the 'submit and proceed' button..");
            PopulateCoach();
            btnSimOffseasonConditioning.Enabled = true; 
        }
        private void PopulateCoach()
        {
            model.CoachModel.SetPositionFilter(0);
            model.CoachModel.SetTeamFilter(SelectHumanTeam.SelectedItem.ToString());
            model.CoachModel.GetNextCoachRecord();
            HoursLeft = 30;
            HoursLeftlbl.Text = HoursLeft.ToString();
            hcname.Text = model.CoachModel.CurrentCoachRecord.Name;
            hcage.Text = model.CoachModel.CurrentCoachRecord.Age.ToString();
            hcknw.Text = model.CoachModel.CurrentCoachRecord.Knowledge.ToString();
            hcmot.Text = model.CoachModel.CurrentCoachRecord.Motivation.ToString();
            hcchm.Text = model.CoachModel.CurrentCoachRecord.Chemistry.ToString();
            hceth.Text = model.CoachModel.CurrentCoachRecord.Ethics.ToString();
            hcqb.Text = model.CoachModel.CurrentCoachRecord.QuarterbackRating.ToString();
            hcrb.Text = model.CoachModel.CurrentCoachRecord.RunningbackRating.ToString();
            hcwr.Text = model.CoachModel.CurrentCoachRecord.WideReceiverRating.ToString();
            hcol.Text = model.CoachModel.CurrentCoachRecord.OffensiveLineRating.ToString();
            hcdl.Text = model.CoachModel.CurrentCoachRecord.DefensiveLineRating.ToString();
            hclb.Text = model.CoachModel.CurrentCoachRecord.LinebackerRating.ToString();
            hcdb.Text = model.CoachModel.CurrentCoachRecord.DefensiveBackRating.ToString();
            hck.Text = model.CoachModel.CurrentCoachRecord.KickerRating.ToString();
            hcp.Text = model.CoachModel.CurrentCoachRecord.PuntRating.ToString();
            AddCoachingHours(knwUpDown);
            knwUpDown.Enabled = true;
            AddCoachingHours(motUpDown);
            motUpDown.Enabled = true;
            AddCoachingHours(chmUpDown);
            chmUpDown.Enabled = true;
            AddCoachingHours(ethUpDown);
            ethUpDown.Enabled = true;
            AddCoachingHours(qbUpDown);
            qbUpDown.Enabled = true;
            AddCoachingHours(rbUpDown);
            rbUpDown.Enabled = true;
            AddCoachingHours(wrUpDown);
            wrUpDown.Enabled = true;
            AddCoachingHours(olUpDown);
            olUpDown.Enabled = true;
            AddCoachingHours(dlUpDown);
            dlUpDown.Enabled = true;
            AddCoachingHours(lbUpDown);
            lbUpDown.Enabled = true;
            AddCoachingHours(dbUpDown);
            dbUpDown.Enabled = true;
            AddCoachingHours(kUpDown);
            kUpDown.Enabled = true;
            AddCoachingHours(pUpDown);
            pUpDown.Enabled = true;

        }
        private void CoachProgressionCPU()
        {
            model.CoachModel.SetPositionFilter(0);
            model.CoachModel.SetTeamFilter(CPUteam.ToString());
            model.CoachModel.GetNextCoachRecord();
            int Wins = model.CoachModel.CurrentCoachRecord.CareerLosses;
            int Losses = model.CoachModel.CurrentCoachRecord.CareerWins;            
            int SeasonsExp = (int)((Wins + Losses) / 16);
            double CoachProgMod = 0.0;
            decimal AttributeDeviation = 0;
            decimal RankExp = 0;
             oldKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
             OldMot = model.CoachModel.CurrentCoachRecord.Motivation;
             oldChm = model.CoachModel.CurrentCoachRecord.Chemistry;
             OldEth = model.CoachModel.CurrentCoachRecord.Ethics;
             oldqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
             Oldrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
             oldwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
             Oldol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
             olddl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
             Oldlb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
             olddb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
             Oldk = model.CoachModel.CurrentCoachRecord.KickerRating;
             Oldp = model.CoachModel.CurrentCoachRecord.PuntRating;

             int OffBalance = ((oldqb + Oldrb + oldwr + Oldol) / 4);
             int OffBalanceSum = (oldqb + Oldrb + oldwr + Oldol);
             OffBalance = (int)(OffBalance + (-6 * random.NextDouble() + 10));
             int DefBalance = ((olddl + Oldlb + olddb) / 3);
             int DefBalanceSum = (olddl + Oldlb + olddb);
             DefBalance = (int)(DefBalance + (-6 * random.NextDouble() + 10));
             int CoreRatings = ((oldKnw + OldMot + OldEth + oldChm) / 4);
             int CoreRatingSum = (oldKnw + OldMot + OldEth + oldChm);

             if ((Math.Abs(OffBalance - DefBalance) >= 13))
             {
                 if (OffBalance < DefBalance)
                 {
                     OffBalance = (int)(OffBalance * (double)4);
                     DefBalance = (int)(DefBalance / (double)4);
                 }
                 else if (DefBalance < OffBalance)
                 {
                     DefBalance = (int)(DefBalance * (double)4);
                     OffBalance = (int)(OffBalance / (double)4);
                 }

             }
            
            

             int RatingsSum = (OffBalance + DefBalance + CoreRatings);
             decimal OffBalanceAloc = Math.Round((((decimal)OffBalance / (decimal)RatingsSum) * 30), 0);
             decimal DefBalanceAloc = Math.Round((((decimal)DefBalance / (decimal)RatingsSum) * 30), 0);
             decimal CoreRatingsAloc = Math.Round(((((decimal)CoreRatings / (decimal)RatingsSum) * 30) / 2), 0);

            

             int RnkKnw; int RnkMot; int RnkChm; int RnkEth; int RnkQB; int RnkRB; int RnkWR; int RnkOL; int RnkDL; int RnkLB; int RnkDB;

            RnkQB = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)oldqb) / (decimal)OffBalanceSum), 2))) * (decimal)OffBalanceAloc)), 0));

            if (RnkQB > 5)
            {
                RnkQB = 5;
            }
            RnkRB = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)Oldrb) / (decimal)OffBalanceSum), 2))) * (decimal)OffBalanceAloc)), 0));

            if (RnkRB > 5)
            {
                RnkRB = 5;
            }
            RnkWR = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)oldwr) / (decimal)OffBalanceSum), 2))) * (decimal)OffBalanceAloc)), 0));

            if (RnkWR > 5)
            {
                RnkWR = 5;
            }
            RnkOL = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)Oldol) / (decimal)OffBalanceSum), 2))) * (decimal)OffBalanceAloc)), 0));

            if (RnkOL > 5)
            {
                RnkOL = 5;
            }
            RnkDL = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)olddl) / (decimal)OffBalanceSum), 2))) * (decimal)DefBalanceAloc)), 0));

            if (RnkDL > 5)
            {
                RnkDL = 5;
            }
            RnkLB = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)Oldlb) / (decimal)OffBalanceSum), 2))) * (decimal)DefBalanceAloc)), 0));

            if (RnkLB > 5)
            {
                RnkLB = 5;
            }
            RnkDB = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)olddb) / (decimal)OffBalanceSum), 2))) * (decimal)DefBalanceAloc)), 0));

            if (RnkDB > 5)
            {
                RnkDB = 5;
            }
            RnkKnw = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)oldKnw) / (decimal)OffBalanceSum), 2))) * (decimal)CoreRatingsAloc)), 0));

            if (RnkKnw > 5)
            {
                RnkKnw = 5;
            }
            RnkMot = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)OldMot) / (decimal)OffBalanceSum), 2))) * (decimal)CoreRatingsAloc)), 0));

            if (RnkMot > 5)
            {
                RnkMot = 5;
            }
            RnkChm = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)oldChm) / (decimal)OffBalanceSum), 2))) * (decimal)CoreRatingsAloc)), 0));

            if (RnkChm > 5)
            {
                RnkChm = 5;
            }
            RnkEth = (int)(Math.Round((((1 - (Math.Round((((decimal)OffBalanceSum - (decimal)OldEth) / (decimal)OffBalanceSum), 2))) * (decimal)CoreRatingsAloc)), 0));

            if (RnkEth > 5)
            {
                RnkEth = 5;
            }


            if (SeasonsExp <= 1)
            {
                CoachProgMod = (2.2);
            }
            else if (SeasonsExp == 2)
            {
                CoachProgMod = (2.1);
            }
            else if (SeasonsExp == 3)
            {
                CoachProgMod = (2);
            }
            else if (SeasonsExp == 4)
            {
                CoachProgMod = (1.85);
            }
            else if (SeasonsExp == 5)
            {
                CoachProgMod = (1.75);
            }
            else if (SeasonsExp == 6)
            {
                CoachProgMod = (1.55);
            }
            else if (SeasonsExp == 7)
            {
                CoachProgMod = (1.45);
            }
            else if (SeasonsExp == 8)
            {
                CoachProgMod = (1.35);
            }
            else if (SeasonsExp == 9)
            {
                CoachProgMod = (1.2);
            }
            else if (SeasonsExp == 10)
            {
                CoachProgMod = (1);
            }
            else if (SeasonsExp <= 15)
            {
                CoachProgMod = (.7);
            }
            else if (SeasonsExp <= 20)
            {
                CoachProgMod = (.4);
            }            
            else if (SeasonsExp > 20)
            {
                CoachProgMod = (.3);
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Knowledge)) / 10),3); //Knw
            RankExp = (RnkKnw * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Knowledge = model.CoachModel.CurrentCoachRecord.Knowledge + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Motivation)) / 10), 3); //Knw
            RankExp = (RnkMot * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Motivation = model.CoachModel.CurrentCoachRecord.Motivation + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Chemistry)) / 10), 3); //Knw
            RankExp = (RnkChm * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Chemistry = model.CoachModel.CurrentCoachRecord.Chemistry + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Ethics)) / 10), 3); //Knw
            RankExp = (RnkEth * ((decimal)CoachProgMod * (decimal).7));//(decimal)CoachProgMod);
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Ethics = model.CoachModel.CurrentCoachRecord.Ethics + ((int)(RankExp) - (int)(AttributeDeviation));
            }

            //Pos ratings
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.QuarterbackRating)) / 10), 3); //Knw
            RankExp = (RnkQB * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.QuarterbackRating = model.CoachModel.CurrentCoachRecord.QuarterbackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.RunningbackRating)) / 10), 3); //Knw
            RankExp = (RnkRB * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.RunningbackRating = model.CoachModel.CurrentCoachRecord.RunningbackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.WideReceiverRating)) / 10), 3); //Knw
            RankExp = (RnkWR * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.WideReceiverRating = model.CoachModel.CurrentCoachRecord.WideReceiverRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.OffensiveLineRating)) / 10), 3); //Knw
            RankExp = (RnkOL * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.OffensiveLineRating = model.CoachModel.CurrentCoachRecord.OffensiveLineRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.DefensiveLineRating)) / 10), 3); //Knw
            RankExp = (RnkDL * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveLineRating = model.CoachModel.CurrentCoachRecord.DefensiveLineRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.LinebackerRating)) / 10), 3); //Knw
            RankExp = (RnkLB * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.LinebackerRating = model.CoachModel.CurrentCoachRecord.LinebackerRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.DefensiveBackRating)) / 10), 3); //Knw
            RankExp = (RnkDB * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveBackRating = model.CoachModel.CurrentCoachRecord.DefensiveBackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
           /*
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.KickerRating)) / 10), 3); //Knw
            RankExp = ((decimal)kUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.KickerRating = model.CoachModel.CurrentCoachRecord.KickerRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.PuntRating)) / 10), 3); //Knw
            RankExp = ((decimal)pUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.PuntRating = model.CoachModel.CurrentCoachRecord.PuntRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            
            if (model.CoachModel.CurrentCoachRecord.Knowledge > oldKnw)
            {
                hcknw.Text = model.CoachModel.CurrentCoachRecord.Knowledge.ToString();
                hcknw.ForeColor = Color.Blue;
                Changes = "Knw increased +" + (model.CoachModel.CurrentCoachRecord.Knowledge - oldKnw) + " from " + oldKnw + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Motivation > OldMot)
            {
                hcmot.Text = model.CoachModel.CurrentCoachRecord.Motivation.ToString();
                hcmot.ForeColor = Color.Blue;
                Changes = Changes + "Mot increased +" + (model.CoachModel.CurrentCoachRecord.Motivation - OldMot) + " from " + OldMot + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Chemistry > oldChm)
            {
                hcchm.Text = model.CoachModel.CurrentCoachRecord.Chemistry.ToString();
                hcchm.ForeColor = Color.Blue;
                Changes = Changes + "Chm increased +" + (model.CoachModel.CurrentCoachRecord.Chemistry - oldChm) + " from " + oldChm + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Ethics > OldEth)
            {
                hceth.Text = model.CoachModel.CurrentCoachRecord.Ethics.ToString();
                hceth.ForeColor = Color.Blue;
                Changes = Changes + "Eth increased +" + (model.CoachModel.CurrentCoachRecord.Ethics - OldEth) + " from " + OldEth + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.QuarterbackRating > oldqb)
            {
                hcqb.Text = model.CoachModel.CurrentCoachRecord.QuarterbackRating.ToString();
                hcqb.ForeColor = Color.Blue;
                Changes = Changes + "qb increased +" + (model.CoachModel.CurrentCoachRecord.QuarterbackRating - oldqb) + " from " + oldqb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.RunningbackRating > Oldrb)
            {
                hcrb.Text = model.CoachModel.CurrentCoachRecord.RunningbackRating.ToString();
                hcrb.ForeColor = Color.Blue;
                Changes = Changes + "rb increased +" + (model.CoachModel.CurrentCoachRecord.RunningbackRating - Oldrb) + " from " + Oldrb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.WideReceiverRating > oldwr)
            {
                hcwr.Text = model.CoachModel.CurrentCoachRecord.WideReceiverRating.ToString();
                hcwr.ForeColor = Color.Blue;
                Changes = Changes + "wr increased +" + (model.CoachModel.CurrentCoachRecord.WideReceiverRating - oldwr) + " from " + oldwr + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.OffensiveLineRating > Oldol)
            {
                hcol.Text = model.CoachModel.CurrentCoachRecord.OffensiveLineRating.ToString();
                hcol.ForeColor = Color.Blue;
                Changes = Changes + "ol increased +" + (model.CoachModel.CurrentCoachRecord.OffensiveLineRating - Oldol) + " from " + Oldol + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.DefensiveLineRating > olddl)
            {
                hcdl.Text = model.CoachModel.CurrentCoachRecord.DefensiveLineRating.ToString();
                hcdl.ForeColor = Color.Blue;
                Changes = Changes + "dl increased +" + (model.CoachModel.CurrentCoachRecord.DefensiveLineRating - olddl) + " from " + olddl + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.LinebackerRating > Oldlb)
            {
                hclb.Text = model.CoachModel.CurrentCoachRecord.LinebackerRating.ToString();
                hclb.ForeColor = Color.Blue;
                Changes = Changes + "Lb increased +" + (model.CoachModel.CurrentCoachRecord.LinebackerRating - Oldlb) + " from " + Oldlb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.DefensiveBackRating > olddb)
            {
                hcdb.Text = model.CoachModel.CurrentCoachRecord.DefensiveBackRating.ToString();
                hcdb.ForeColor = Color.Blue;
                Changes = Changes + "db increased +" + (model.CoachModel.CurrentCoachRecord.DefensiveBackRating - olddb) + " from " + olddb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.KickerRating > Oldk)
            {
                hck.Text = model.CoachModel.CurrentCoachRecord.KickerRating.ToString();
                hck.ForeColor = Color.Blue;
                Changes = Changes + "k increased +" + (model.CoachModel.CurrentCoachRecord.LinebackerRating - Oldk) + " from " + Oldk + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.PuntRating > Oldp)
            {
                hcp.Text = model.CoachModel.CurrentCoachRecord.PuntRating.ToString();
                hcp.ForeColor = Color.Blue;
                Changes = Changes + "p increased +" + (model.CoachModel.CurrentCoachRecord.PuntRating - Oldp) + " from " + Oldp + ", ";
            }
            */
            
          
        }
        private void CoachProgression()
        {
            int Wins = model.CoachModel.CurrentCoachRecord.CareerLosses;
            int Losses = model.CoachModel.CurrentCoachRecord.CareerWins;
            int SeasonsExp = (int)((Wins + Losses) / 16);
            double CoachProgMod = 0.0;
            decimal AttributeDeviation = 0;
            decimal RankExp = 0;
            string Changes = "";
             oldKnw = model.CoachModel.CurrentCoachRecord.Knowledge;
             OldMot = model.CoachModel.CurrentCoachRecord.Motivation;
             oldChm = model.CoachModel.CurrentCoachRecord.Chemistry;
             OldEth = model.CoachModel.CurrentCoachRecord.Ethics;
             oldqb = model.CoachModel.CurrentCoachRecord.QuarterbackRating;
             Oldrb = model.CoachModel.CurrentCoachRecord.RunningbackRating;
             oldwr = model.CoachModel.CurrentCoachRecord.WideReceiverRating;
             Oldol = model.CoachModel.CurrentCoachRecord.OffensiveLineRating;
             olddl = model.CoachModel.CurrentCoachRecord.DefensiveLineRating;
             Oldlb = model.CoachModel.CurrentCoachRecord.LinebackerRating;
             olddb = model.CoachModel.CurrentCoachRecord.DefensiveBackRating;
             Oldk = model.CoachModel.CurrentCoachRecord.KickerRating;
             Oldp = model.CoachModel.CurrentCoachRecord.PuntRating;

             if (SeasonsExp <= 1)
             {
                 CoachProgMod = (2.2);
             }
             else if (SeasonsExp == 2)
             {
                 CoachProgMod = (2.1);
             }
             else if (SeasonsExp == 3)
             {
                 CoachProgMod = (2);
             }
             else if (SeasonsExp == 4)
             {
                 CoachProgMod = (1.85);
             }
             else if (SeasonsExp == 5)
             {
                 CoachProgMod = (1.75);
             }
             else if (SeasonsExp == 6)
             {
                 CoachProgMod = (1.55);
             }
             else if (SeasonsExp == 7)
             {
                 CoachProgMod = (1.45);
             }
             else if (SeasonsExp == 8)
             {
                 CoachProgMod = (1.35);
             }
             else if (SeasonsExp == 9)
             {
                 CoachProgMod = (1.2);
             }
             else if (SeasonsExp == 10)
             {
                 CoachProgMod = (1);
             }
             else if (SeasonsExp <= 15)
             {
                 CoachProgMod = (.7);
             }
             else if (SeasonsExp <= 20)
             {
                 CoachProgMod = (.4);
             }
             else if (SeasonsExp > 20)
             {
                 CoachProgMod = (.3);
             }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Knowledge)) / 10), 3); //Knw
            RankExp = ((decimal)knwUpDown.Value * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Knowledge = model.CoachModel.CurrentCoachRecord.Knowledge + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Motivation)) / 10), 3); //Knw
            RankExp = ((decimal)motUpDown.Value * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Motivation = model.CoachModel.CurrentCoachRecord.Motivation + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Chemistry)) / 10), 3); //Knw
            RankExp = ((decimal)chmUpDown.Value * ((decimal)CoachProgMod * (decimal).7));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Chemistry = model.CoachModel.CurrentCoachRecord.Chemistry + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.Ethics)) / 10), 3); //Knw
            RankExp = ((decimal)ethUpDown.Value * ((decimal)CoachProgMod * (decimal).7));//(decimal)CoachProgMod);
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.Ethics = model.CoachModel.CurrentCoachRecord.Ethics + ((int)(RankExp) - (int)(AttributeDeviation));
            }

            //Pos ratings
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.QuarterbackRating)) / 10), 3); //Knw
            RankExp = ((decimal)qbUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.QuarterbackRating = model.CoachModel.CurrentCoachRecord.QuarterbackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.RunningbackRating)) / 10), 3); //Knw
            RankExp = ((decimal)rbUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.RunningbackRating = model.CoachModel.CurrentCoachRecord.RunningbackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.WideReceiverRating)) / 10), 3); //Knw
            RankExp = ((decimal)wrUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.WideReceiverRating = model.CoachModel.CurrentCoachRecord.WideReceiverRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.OffensiveLineRating)) / 10), 3); //Knw
            RankExp = ((decimal)olUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.OffensiveLineRating = model.CoachModel.CurrentCoachRecord.OffensiveLineRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.DefensiveLineRating)) / 10), 3); //Knw
            RankExp = ((decimal)dlUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveLineRating = model.CoachModel.CurrentCoachRecord.DefensiveLineRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.LinebackerRating)) / 10), 3); //Knw
            RankExp = ((decimal)lbUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.LinebackerRating = model.CoachModel.CurrentCoachRecord.LinebackerRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.DefensiveBackRating)) / 10), 3); //Knw
            RankExp = ((decimal)dbUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.DefensiveBackRating = model.CoachModel.CurrentCoachRecord.DefensiveBackRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.KickerRating)) / 10), 3); //Knw
            RankExp = ((decimal)kUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.KickerRating = model.CoachModel.CurrentCoachRecord.KickerRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            AttributeDeviation = Math.Round(((100 - (99 - (decimal)model.CoachModel.CurrentCoachRecord.PuntRating)) / 10), 3); //Knw
            RankExp = ((decimal)pUpDown.Value * ((decimal)CoachProgMod * (decimal).75));
            if (RankExp > AttributeDeviation)
            {
                model.CoachModel.CurrentCoachRecord.PuntRating = model.CoachModel.CurrentCoachRecord.PuntRating + ((int)(RankExp) - (int)(AttributeDeviation));
            }
            if (model.CoachModel.CurrentCoachRecord.Knowledge > oldKnw)
            {
                hcknw.Text = model.CoachModel.CurrentCoachRecord.Knowledge.ToString();
                hcknw.ForeColor = Color.Blue;
                Changes = "Knw increased +" + (model.CoachModel.CurrentCoachRecord.Knowledge - oldKnw) + " from " + oldKnw + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Motivation > OldMot)
            {
                hcmot.Text = model.CoachModel.CurrentCoachRecord.Motivation.ToString();
                hcmot.ForeColor = Color.Blue;
                Changes = Changes + "Mot increased +" + (model.CoachModel.CurrentCoachRecord.Motivation - OldMot) + " from " + OldMot + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Chemistry > oldChm)
            {
                hcchm.Text = model.CoachModel.CurrentCoachRecord.Chemistry.ToString();
                hcchm.ForeColor = Color.Blue;
                Changes = Changes + "Chm increased +" + (model.CoachModel.CurrentCoachRecord.Chemistry - oldChm) + " from " + oldChm + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.Ethics > OldEth)
            {
                hceth.Text = model.CoachModel.CurrentCoachRecord.Ethics.ToString();
                hceth.ForeColor = Color.Blue;
                Changes = Changes + "Eth increased +" + (model.CoachModel.CurrentCoachRecord.Ethics - OldEth) + " from " + OldEth + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.QuarterbackRating > oldqb)
            {
                hcqb.Text = model.CoachModel.CurrentCoachRecord.QuarterbackRating.ToString();
                hcqb.ForeColor = Color.Blue;
                Changes = Changes + "qb increased +" + (model.CoachModel.CurrentCoachRecord.QuarterbackRating - oldqb) + " from " + oldqb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.RunningbackRating > Oldrb)
            {
                hcrb.Text = model.CoachModel.CurrentCoachRecord.RunningbackRating.ToString();
                hcrb.ForeColor = Color.Blue;
                Changes = Changes + "rb increased +" + (model.CoachModel.CurrentCoachRecord.RunningbackRating - Oldrb) + " from " + Oldrb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.WideReceiverRating > oldwr)
            {
                hcwr.Text = model.CoachModel.CurrentCoachRecord.WideReceiverRating.ToString();
                hcwr.ForeColor = Color.Blue;
                Changes = Changes + "wr increased +" + (model.CoachModel.CurrentCoachRecord.WideReceiverRating - oldwr) + " from " + oldwr + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.OffensiveLineRating > Oldol)
            {
                hcol.Text = model.CoachModel.CurrentCoachRecord.OffensiveLineRating.ToString();
                hcol.ForeColor = Color.Blue;
                Changes = Changes + "ol increased +" + (model.CoachModel.CurrentCoachRecord.OffensiveLineRating - Oldol) + " from " + Oldol + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.DefensiveLineRating > olddl)
            {
                hcdl.Text = model.CoachModel.CurrentCoachRecord.DefensiveLineRating.ToString();
                hcdl.ForeColor = Color.Blue;
                Changes = Changes + "dl increased +" + (model.CoachModel.CurrentCoachRecord.DefensiveLineRating - olddl) + " from " + olddl + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.LinebackerRating > Oldlb)
            {
                hclb.Text = model.CoachModel.CurrentCoachRecord.LinebackerRating.ToString();
                hclb.ForeColor = Color.Blue;
                Changes = Changes + "Lb increased +" + (model.CoachModel.CurrentCoachRecord.LinebackerRating - Oldlb) + " from " + Oldlb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.DefensiveBackRating > olddb)
            {
                hcdb.Text = model.CoachModel.CurrentCoachRecord.DefensiveBackRating.ToString();
                hcdb.ForeColor = Color.Blue;
                Changes = Changes + "db increased +" + (model.CoachModel.CurrentCoachRecord.DefensiveBackRating - olddb) + " from " + olddb + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.KickerRating > Oldk)
            {
                hck.Text = model.CoachModel.CurrentCoachRecord.KickerRating.ToString();
                hck.ForeColor = Color.Blue;
                Changes = Changes + "k increased +" + (model.CoachModel.CurrentCoachRecord.LinebackerRating - Oldk) + " from " + Oldk + ", ";
            }
            if (model.CoachModel.CurrentCoachRecord.PuntRating > Oldp)
            {
                hcp.Text = model.CoachModel.CurrentCoachRecord.PuntRating.ToString();
                hcp.ForeColor = Color.Blue;
                Changes = Changes + "p increased +" + (model.CoachModel.CurrentCoachRecord.PuntRating - Oldp) + " from " + Oldp + ", ";
            }


            if (Changes != "")
            {
                Timer1TextDelay(12);//pause to allow user to view coach progression
                textBox1.Text = Changes + "...Now simulating Conditioning...";
            }
            else
            {
                Timer1TextDelay(5);//pause to allow user to view coach progression
                textBox1.Text = "...No change to coaching attributes...Now simulating Conditioning...";
            }
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
            textBox1.ScrollToCaret();
            while (isTrue == true)
            {
                Application.DoEvents();
            }
        }
        private void AddCoachingHours(NumericUpDown control)
        {
            control.Value = 0;
            control.Maximum = 5;
            control.Minimum = 0;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                SelectHumanTeam.Enabled = true;
                button1.Enabled = false;
            }
            else
            {
                SelectHumanTeam.Enabled = false;
                button1.Enabled = true;
                knwUpDown.Enabled = false;
                motUpDown.Enabled = false;
                chmUpDown.Enabled = false;
                ethUpDown.Enabled = false;
                qbUpDown.Enabled = false;
                rbUpDown.Enabled = false;
                wrUpDown.Enabled = false;
                olUpDown.Enabled = false;
                dlUpDown.Enabled = false;
                lbUpDown.Enabled = false;
                dbUpDown.Enabled = false;
                kUpDown.Enabled = false;
                pUpDown.Enabled = false;
                btnSimOffseasonConditioning.Enabled = false;
                checkBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                DialogResult dr = MessageBox.Show("Skip offseason conditioning and proceed to training camp?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (dr == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    TrainingCampForm form = new TrainingCampForm(model);
                    Cursor.Current = Cursors.Arrow;

                    this.Close();
                    form.InitialiseUI();
                    form.Show();
                }

                return;                    
        }
        private void TrainingCampOffSeason_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Proceed with current selection?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

            if (dr == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                button2.Enabled = false;

                ScenarioDialog();

                Cursor.Current = Cursors.Arrow;               
            }
            return;
        }
        private void ScenarioDialog()
        {
            double Roll = 0;
            GoodNews = false;
            BadNews = false;
            GreatNews = false;
            HorribleNews = false;
            if (NegativeScenario1 == true)
            {               

                if (radioButton1.Checked == true)
                {                    
                    Timer1TextDelay(7);
                    textBox1.Text = textBox1.Text + "\r\n...Attempting to phone " + ScenarioLastName + "'s agent...\r\n...Please standby...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(25);
                    textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...After requesting that the agency front desk transfer you to " + ScenarioFirstName + " " + ScenarioLastName + "'s agent a man picks up the phone. ''This is " + ScenarioFirstName + " " + ScenarioLastName + "'s agent. Whose calling and what do you want?'' \r\nYou explain you're concerned that " + ScenarioLastName + " isn't taking his conditioning seriously and that you'd like the agent to talk to him about it..."; 
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(20);
                    textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...The agent pauses in thought...Please stand by while the agent makes a decision...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Roll = Math.Round((84 * random.NextDouble() + 15));
                    double CrabbyAgent = Math.Round((29 * random.NextDouble() + 15));
                    if ((Roll + CrabbyAgent) <= CoachMotivation)
                    {
                        Timer1TextDelay(15);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...''Alright. Alright. I'll talk to " + ScenarioFirstName + "''...\r\n\r\nWell, it's better than nothing you figure. You can expect " + ScenarioLastName + " to at the least curb his weight gain.\r\n...Stand by while Offseason Conditioning is simulated...";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        CurrentDialog = "\r\n..." + ScenarioLastName + "'s agent came through for you and clearly spoke with " + ScenarioFirstName + " about his conditioning. Though it was probably driven more by his fear of loosing a high priced player to weight issues than by a desire to help out the orginization. Still, " + ScenarioFirstName + " is in decent shape and he's in a good mood (" + ScenarioFirstName + "'s Morale has increased due to your handling of the situation)...";
                        GoodNews = true; 
                        PhaseOne();
                         return;
                    }
                    else 
                    {
                        Timer1TextDelay(17);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...''SCREW YOU COACH.'' Stop wasting my time. He's a grown man and is free to make his own decisions. Besides, it's the offseason. He'll work it off in camp. I'm busy; leave me alone.''\r\n...Stand by while Offseason Conditioning is simulated...";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        CurrentDialog = "\r\n...Those damn meddlesome agents...You know you'd have gotten through to " + ScenarioLastName + " if you just could've gotten him on the phone. Now he's in horrible shape and isn't anywhere ready to play football.(" + ScenarioFirstName + "'s morale has dropped due to his unhappiness with the way your handled the situation)...";
                        BadNews = true;
                        PhaseOne();
                         return;
                    }
                  
                }
                else if (radioButton2.Checked == true)
                {                    
                    Timer1TextDelay(6);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Contacting local media...\r\n\r\nPlease stand by...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...''Sports department, how may I help you?''\r\n\r\n...''Yes, this is " + model.CoachModel.CurrentCoachRecord.Name + ". I'd like you to run a story for me in tomorrows paper about " + ScenarioFirstName + " " + ScenarioLastName + "...\r\n\r\nPlease stand by while the article is written...''";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(35);
                    textBox1.Text = textBox1.Text + "\r\n*************************************************************************************************\r\nA.P. It's being reported from an anonymous source that " + SelectHumanTeam.SelectedItem.ToString() + " franchise backbone " + ScenarioFirstName + " " + ScenarioLastName + " has reportedly put on considerable weight since the Pro-Bowl. Rumors also swirl that " + ScenarioLastName + " has been contemplating retirement citing a lack of desire for the game. Clearly " + ScenarioLastName + " is being payed a lot of money to perform at a very high level and if these reports are true, and he does in fact show up to camp in poor shape rest assured coach " + model.CoachModel.CurrentCoachRecord.Name + " won't be pleased...''\r\n...Please stand by while we await word from " + ScenarioFirstName + " " + ScenarioLastName + "..."; 
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Roll = Math.Round((69 * random.NextDouble() + 31));
                    if (Roll <= (CoachMotivation * .7))//Player answers phone
                    {
                        Timer1TextDelay(30);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...Two days later your office phone rings...\r\n...''Hello?''...''Yea coach, it's " + ScenarioFirstName + " " + ScenarioLastName + ". I, uh...I saw the article about me. I'm sure you didn't have anything to do with it but...I want you to know I stepped up my workouts yesterday. I'll be in shape for camp.''\r\n...You thank " + ScenarioFirstName + " and end the call. Running that article was a big gamble but it appears to have payed off...\r\n...Please wait while Offseason Conditioning is simulated..."; 
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        CurrentDialog = "\r\n...The'annoynous'source who leaked the " + ScenarioFirstName + " " + ScenarioLastName + " story to the local news did " + ScenarioLastName + " and the " + SelectHumanTeam.Text + " a great service. Your gamble to print the story payed off as " + ScenarioLastName + " is in good shape. I shudder to think of what kind of shape he'd have been in otherwise. Plus he's in a good mood (" + ScenarioFirstName + "'s Morale has increased due to your handling of the situation)...";
                        GoodNews = true;
                        PhaseOne();
                         return;
                    }
                    else
                    {
                        Timer1TextDelay(30);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...Five days later your office phone rings...\r\n...''Hello?''...''Yea coach, it's " + ScenarioFirstName + " " + ScenarioLastName + ". I saw the article about me. That's BULLSH$T and you know it. Callin' out your star player and embarrasing the HELL out of me. I won't forget this. And don't expect me to bust my ass in the offseason either. I'll get into shape on " + SelectHumanTeam.SelectedItem.ToString() + " time. Not my time.''\r\n...Well that backfired you think to yourself...\r\n...Please wait while Offseason Conditioning is simulated..."; 
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        CurrentDialog = "\r\n...We've been receiving nasty fallout from the little newspaper article incident. The ownership is well aware you were involved in that decision and they're very angry. Look at the poor condition " + ScenarioLastName + " is in? He looks like the Pillsbury Dough Boy and he's in a very bad mood (" + ScenarioFirstName + "'s Morale has plummeted due to your complete miss-handling of the situation)...";
                        BadNews = true;
                        HorribleNews = true;
                        PhaseOne();
                         return;
                    }

                    }                      
                else if (radioButton3.Checked == true)
                {

                    Timer1TextDelay(10);
                    textBox1.Text = textBox1.Text + "\r\n...Attempting to schedule meeting with " + ScenarioLastName + " without involving his agent...\r\n\r\nPlease standby while I attempt to connect you to " + ScenarioLastName + "'s cell phone...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {                        
                        Application.DoEvents();
                    }
                    Timer1TextDelay(10);
                    textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...Please hold...\r\n\r\nStill ringing...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }

                    Roll = Math.Round((99 * random.NextDouble() + 1));
                    if (Roll <= CoachMotivation)//Player answers phone
                    {                        
                        Timer1TextDelay(8);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...Someone answers the phone...''Yo what up?''\r\n\r\n''Is this " + ScenarioFirstName + "?'' you ask.";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {                            
                            Application.DoEvents();
                        }
                        Timer1TextDelay(30);
                        textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...''Uh, yea. Is that you coach?'' ''Yes,'' you say. ''We need to talk " + ScenarioFirstName + " and I want to speak to you face to face. Will you agree to meet with me " + ScenarioFirstName + "?''...\r\n\r\n...Please standby while " + ScenarioFirstName + " makes his decision...";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();
                        while (isTrue == true)
                        {                            
                            Application.DoEvents();
                        }
                        Roll = Math.Round((84 * random.NextDouble() + 15));
                        if (Roll <= (CoachMotivation * .80))
                        {
                            Timer1TextDelay(15);
                            textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...''Ok coach. I'll do it. I'll fly in to meet with you''\r\n\r\nPlease standby to see if " + model.CoachModel.CurrentCoachRecord.Name + " is able to motivate " + ScenarioFirstName + " during their meeting...";
                            textBox1.SelectionStart = textBox1.Text.Length;
                            textBox1.SelectionLength = 0;
                            textBox1.ScrollToCaret();
                            while (isTrue == true)
                            {
                                Application.DoEvents();
                            }
                            Roll = Math.Round((99 * random.NextDouble() + 10));
                            if (Roll <= (CoachMotivation * 1.05))
                            {
                                Timer1TextDelay(30);
                                textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...The meeting date has arrived. And right on schedule " + ScenarioFirstName + " walks into your office. You explain your belief that the team's success this season 'weighs' heavily on " + ScenarioFirstName + ". Your passionate speech really seems to inspire him. You can see a fire in his eyes as he shakes your hand and leaves your office, and you feel confident you were able to motivate him to take his conditioning seriously.\r\n\r\n...Stand by while Offseason Conditioning is simulated...";
                                textBox1.SelectionStart = textBox1.Text.Length;
                                textBox1.SelectionLength = 0;
                                textBox1.ScrollToCaret();
                                while (isTrue == true)
                                {
                                    Application.DoEvents();
                                }
                                CurrentDialog = "\r\n...GREAT JOB " + model.CoachModel.CurrentCoachRecord.Name + "! Have you seen " + ScenarioFirstName + " " + ScenarioLastName + "?! The guy reported to camp in phenomenol shape. He's chomping at the bit to start camp and he's in a great mood(" + ScenarioFirstName + "'s morale has soared due in part to your excellent handling of the situation) We're fortunate as an orginization to have such a great Motivator like yourself...";
                                GoodNews = true;
                                GreatNews = true;
                                PhaseOne();
                                return;
                            }
                            else
                            {                              
                                    Timer1TextDelay(30);
                                    textBox1.Text = textBox1.Text + "\r\n\r\n\r\n...The meeting date has arrived. And right on schedule " + ScenarioFirstName + " walks into your office. You explain your belief that the team's success this season 'weighs' heavily on " + ScenarioFirstName + " but he seems to take exception to you telling him he's got to take his conditioning more seriously. You try informing him you're paying him a lot of money to perform and that seems to make him even madder. He storms out of your office in disgust.\r\n...You've failed to motivate " + ScenarioLastName + "\r\n...Stand by while Offseason Conditioning is simulated...";
                                    textBox1.SelectionStart = textBox1.Text.Length;
                                    textBox1.SelectionLength = 0;
                                    textBox1.ScrollToCaret();
                                    while (isTrue == true)
                                    {
                                        Application.DoEvents();
                                    }
                                    CurrentDialog = "\r\n...Clearly " + ScenarioFirstName + " " + ScenarioLastName + " isn't taking his role as a leader seriously. In addition to severing communication with you " + ScenarioFirstName + " reported to camp in horrible shape. Now, rather than sharpening his skill set in training camp you'll have to spend time conditioning him back into shape in addition to helping rebuild his morale which plummeted as a result of your miss-handling of the situation...";
                                    BadNews = true;
                                    HorribleNews = true;
                                    PhaseOne();
                                    return;                                
                            }

                        }
                        else
                        {
                            Timer1TextDelay(15);
                            textBox1.Text = textBox1.Text + "\r\n\r\n\r\n\r\n...''I'm a busy man coach. And this is the offseason. So that means I'm not at work. And do me a favor. Stop calling me direct. Go through my agent in the future. Later...''\r\nAnd with that " + ScenarioFirstName + " terminates the call...\r\n\r\n...Your attempt to schedule a meeting has failed...\r\n...Stand by while Offseason Conditioning is simulated.";
                            textBox1.SelectionStart = textBox1.Text.Length;
                            textBox1.SelectionLength = 0;
                            textBox1.ScrollToCaret();
                            while (isTrue == true)
                            {
                                Application.DoEvents();
                            }
                            CurrentDialog = "\r\n...Clearly " + ScenarioFirstName + " " + ScenarioLastName + " isn't taking his role as a leader seriously. In addition to severing communication with you " + ScenarioFirstName + "'s reporting to camp in horrible shape. Now, rather than sharpening his skill set in training camp you'll have to spend time conditioning him back into shape in addition to helping rebuild his morale which plummeted as a result of your miss-handling of the situation...";
                            BadNews = true;
                            HorribleNews = true;
                            PhaseOne();
                            return;
                        }
                       
                    }
                    else
                    {
                        Timer1TextDelay(15);
                        textBox1.Text = textBox1.Text + "\r\n\r\n...Bad news coach. " + ScenarioLastName + " isn't even answering your call...";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();                        
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        Timer1TextDelay(12);
                        textBox1.Text = textBox1.Text + "\r\n\r\n...You cannot even imagine what kind of shape " + ScenarioLastName + " is going to show up to camp in. You guess it won't be good...\r\n\r\n...Stand by while Offseason Conditioning is simulated.";
                        textBox1.SelectionStart = textBox1.Text.Length;
                        textBox1.SelectionLength = 0;
                        textBox1.ScrollToCaret();                        
                        while (isTrue == true)
                        {
                            Application.DoEvents();
                        }
                        CurrentDialog = "\r\n...Clearly " + ScenarioFirstName + " " + ScenarioLastName + " isn't taking his role a leader seriously. In addition to severing communication with you " + ScenarioFirstName + "'s reporting to camp in horrible shape. Now, rather than sharpening his skill set in training camp you'll have to spend time conditioning him back into shape in addition to helping rebuild his morale which plummeted as a result of your miss-handling of the situation...";
                        BadNews = true;
                        HorribleNews = true;
                        PhaseOne();
                        return;
                    }
                }
                else if (radioButton4.Checked == true)
                {
                    Timer1TextDelay(5);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Screw " + model.PlayerModel.CurrentPlayerRecord.LastName + "...\r\n...Now simulating offseason conditioning...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();                    
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Roll = Math.Round((95 * random.NextDouble() + 5));
                    if ((Roll <= CoachMotivation ) & (model.CoachModel.CurrentCoachRecord.SuperBowlWins >= 1))
                    {
                        CurrentDialog = "\r\n\r\n...Apparently " + ScenarioFirstName + " " + ScenarioLastName + " was able to motivate himself. Just prior to veterans reporting to camp you received an email from him stating he believes in " + model.CoachModel.CurrentCoachRecord.Name + " and his philosophy. " + model.CoachModel.CurrentCoachRecord.Name + "'s coaching resume includes " + model.CoachModel.CurrentCoachRecord.SuperBowlWins + " win(s). That alone carries a ton of weight. " + ScenarioFirstName + " is in good spirits(" + ScenarioFirstName + "'s Morale has increased)..."; 
                    GoodNews = true;
                    PhaseOne();
                    return;
                    }                    
                    else
                    {
                        CurrentDialog = "\r\n\r\n..." + ScenarioFirstName + " " + ScenarioLastName + "'s agent emailed our board of director's saying his client feels unwanted and ignored. A player of his caliber deserves to be catered too...";
                    BadNews = true;   
                    HorribleNews = true;
                    PhaseOne();
                    return;
                    }
                }

                
            }//End Negative Scen. 1 
            else if (NegativeScenario2 == true)
            {
                if ((radioButton2.Checked == true) & (PosStr == "Offensive") & (Captain1 == "") || (radioButton2.Checked == true) & (PosStr == "Defensive") & (Captain2 == ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on his response...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...''Thanks coach! I won't let you down. I'll work hard this offseason to report to camp in better shape then I'm in now...\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    if (PosStr == "Offensive")
                    {
                        teamCaptainRecord.Captain1 = NewCaptain;
                    }
                    else
                    {
                        teamCaptainRecord.Captain2 = NewCaptain;

                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    GoodNews = true;
                    CurrentDialog = "\r\n..." + ScenarioFirstName + " " + ScenarioLastName + " is in good spirits since his promotion to " + PosStr + " Captain this offseason...";
                    Captain1 = "";
                    Captain2 = "";
                    PhaseOne();
                    return;
                }
                else if ((radioButton2.Checked == true) & (PosStr == "Offensive") & (Captain1 != ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on player responses...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Email from " + ScenarioFirstName + " " + ScenarioLastName + "...''Thanks coach! I won't let you down. I'll work hard this offseason to report to camp in better shape then I'm in now...\r\n...Email from " + Captain1 + "...What was that?! Stripping me of my Captain status? I'm sick of the head games coach...\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    if (PosStr == "Offensive")
                    {
                        teamCaptainRecord.Captain1 = NewCaptain;
                    }
                    else
                    {
                        teamCaptainRecord.Captain2 = NewCaptain;

                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    GoodNews = true;
                    GreatNews = true;
                    CurrentDialog = "\r\n..." + ScenarioFirstName + " " + ScenarioLastName + " is in good spirits since his promotion to " + PosStr + " Captain this offseason, but " + Captain1 + " looks very unhappy...";
                    Captain2 = "";
                    PhaseOne();
                    return;
                }
                else if ((radioButton2.Checked == true) & (PosStr == "Defensive") & (Captain2 != ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on player responses...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Email from " + ScenarioFirstName + " " + ScenarioLastName + "...''Thanks coach! I won't let you down. I'll work hard this offseason to report to camp in better shape then I'm in now...\r\n...Email from " + Captain2 + "...What was that?! Stripping me of my Captain status? I'm sick of the head games coach...\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    if (PosStr == "Offensive")
                    {
                        teamCaptainRecord.Captain1 = NewCaptain;
                    }
                    else
                    {
                        teamCaptainRecord.Captain2 = NewCaptain;

                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    GoodNews = true;
                    GreatNews = true;
                    CurrentDialog = "\r\n..." + ScenarioFirstName + " " + ScenarioLastName + " is in good spirits since his promotion to " + PosStr + " Captain this offseason, but " + Captain2 + " looks very unhappy...";
                    Captain1 = "";
                    PhaseOne();
                    return;
                }

                else if ((radioButton3.Checked == true) & (PosStr == "Offensive") & (Captain1 == "") || (radioButton2.Checked == true) & (PosStr == "Defensive") & (Captain2 == ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on player response...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...What?! You have someone else in mind? Doesn't my telling you I want to be a leader show I'm serious? The position is vacant! Whatever, I really didn't want to be a captain anyway. Screw this, I'm outta here...\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    BadNews = true;
                    Captain1 = "";
                    Captain2 = "";
                    PhaseOne();
                    CurrentDialog = "\r\n...We're receiving reports that " + ScenarioFirstName + " " + ScenarioLastName + " is very unhappy with not being named a team captain...";
                    return;
                }
                else if ((radioButton3.Checked == true) & (PosStr == "Offensive") & (Captain1 != ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on player responses...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...What?! " + Captain1 + " isn't a leader. I'm a leader. I won't forget this. It's a long offseason...\r\n\r\n...Email from " + Captain1 + "...Coach, I heard that you stuck with me as captain. It feels great to know you've got so much confidence in me. Thanks\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    BadNews = true;
                    HorribleNews = true;
                    CurrentDialog = "\r\n...We're receiving reports that " + ScenarioFirstName + " " + ScenarioLastName + " is very unhappy with not being named a team captain...";
                    Captain2 = "";
                    PhaseOne();
                    return;
                }
                else if ((radioButton3.Checked == true) & (PosStr == "Defensive") & (Captain2 != ""))
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...Waiting on player responses...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(15);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...What?! " + Captain2 + " isn't a leader. I'm a leader. I won't forget this. It's a long offseason...\r\n\r\n...Email from " + Captain2 + "...Coach, I heard that you stuck with me as captain. It feels great to know you've got so much confidence in me. Thanks\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    int Diff = 0;
                    Diff = (((model.CoachModel.CurrentCoachRecord.Motivation - 50) / 8) + ((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 8));
                    if (Diff >= 1)
                    {
                        Diff = (int)Math.Round(Diff * random.NextDouble());
                    }
                    else
                    {
                        Diff = 0;
                    }
                    BadNews = true;
                    HorribleNews = true;
                    CurrentDialog = "\r\n...We're receiving reports that " + ScenarioFirstName + " " + ScenarioLastName + " is very unhappy with not being named a team captain...";
                    Captain1 = "";
                    PhaseOne();
                    return;
                }
                else if (radioButton4.Checked == true)
                {
                    Timer1TextDelay(8);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...Please hold...\r\n\r\n...You're getting very, very angry...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Timer1TextDelay(20);
                    textBox1.Text = textBox1.Text + "\r\n\r\n...You lash out at " + ScenarioFirstName + ", ''Who the hell do you think you are barging into MY office, telling ME how to run MY team. You'll be lucky if I don't give your walking papers NOW.''\r\n...''But coa...''\r\n...''Don't BUT me " + ScenarioLastName + ". Get the hell out of my face...''\r\n...Please stand by while Offseason Conditioning is simulated...";
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    textBox1.ScrollToCaret();
                    while (isTrue == true)
                    {
                        Application.DoEvents();
                    }
                    Roll = Math.Round((60 * random.NextDouble() + 40));
                    if (Roll <= CoachMotivation)
                    {
                        Diff = (((model.CoachModel.CurrentCoachRecord.Chemistry - 50) / 6) + 5);
                        if (Diff <= 0)
                        {
                            Diff = 1;
                        }
                        GoodNews = true;
                        GreatNews = true;
                        CurrentDialog = "\r\n...We're receiving reports that " + ScenarioFirstName + " " + ScenarioLastName + " really stepped up his conditioning since the altercation in your office. It seems you made quite an impression on him...";
                        Captain1 = "";
                        Captain2 = "";
                        OldCaptain = "";
                        PhaseOne();
                        return;
                    }
                    else
                    {
                        Diff = (((model.CoachModel.CurrentCoachRecord.Chemistry - 70) / 6) * -1);
                        BadNews = true;
                        HorribleNews = true;
                        CurrentDialog = "\r\n...We're receiving reports that " + ScenarioFirstName + " " + ScenarioLastName + " might not show up to training camp due to your verbal abuse...";
                        Captain1 = "";
                        Captain2 = "";
                        OldCaptain = "";
                        PhaseOne();
                        return;
                    }
                   
                }

            }//End Negative Scen. 2
          groupBox5.Visible = true;
        }
        private void Timer1TextDelay(int SleepValue)
        {
            ScenarioCounter = 0;
            Sleep = SleepValue;            
            timer1.Enabled = true;
            isTrue = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ScenarioCounter++;

            if (ScenarioCounter == Sleep)
            {
                isTrue = false;
                timer1.Enabled = false;
            }           

        }  
        private void InitializeDataGrids()
        {
            isInitialising = true;
            RosterView.Clear();
            RosterView.Columns.Clear();
            RosterView.Columns.Add(AddColumn("Name", "System.String"));
            RosterView.Columns.Add(AddColumn("Pos", "System.String"));
            RosterView.Columns.Add(AddColumn("Age", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Exp", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Ovr", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Hgt", "System.String"));
            RosterView.Columns.Add(AddColumn("Wgt", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Spd", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Acc", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Agi", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Str", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Stm", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Inj", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Tgh", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Mor", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Awr", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Cat", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Car", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Jmp", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Btk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Tkl", "System.Int16"));
            RosterView.Columns.Add(AddColumn("ThP", "System.Int16"));
            RosterView.Columns.Add(AddColumn("ThA", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Pbk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("Rbk", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KP", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KA", "System.Int16"));
            RosterView.Columns.Add(AddColumn("KR", "System.Int16"));

            RosterViewBinding.DataSource = RosterView;
            depthChartDataGrid.DataSource = RosterViewBinding;
            //Control Column Width
            int ColWidth = 32;

            depthChartDataGrid.Columns["Name"].Width = 130;
            depthChartDataGrid.Columns["Pos"].Width = 40;
            depthChartDataGrid.Columns["Age"].Width = ColWidth;
            depthChartDataGrid.Columns["Exp"].Width = ColWidth;
            depthChartDataGrid.Columns["Ovr"].Width = ColWidth;
            depthChartDataGrid.Columns["Hgt"].Width = ColWidth;
            depthChartDataGrid.Columns["Wgt"].Width = ColWidth;
            depthChartDataGrid.Columns["Spd"].Width = ColWidth;
            depthChartDataGrid.Columns["Acc"].Width = ColWidth;
            depthChartDataGrid.Columns["Agi"].Width = ColWidth;
            depthChartDataGrid.Columns["Str"].Width = ColWidth;
            depthChartDataGrid.Columns["Stm"].Width = ColWidth;
            depthChartDataGrid.Columns["Inj"].Width = ColWidth;
            depthChartDataGrid.Columns["Tgh"].Width = ColWidth;
            depthChartDataGrid.Columns["Mor"].Width = ColWidth;
            depthChartDataGrid.Columns["Awr"].Width = ColWidth;
            depthChartDataGrid.Columns["Cat"].Width = ColWidth;
            depthChartDataGrid.Columns["Car"].Width = ColWidth;
            depthChartDataGrid.Columns["Jmp"].Width = ColWidth;
            depthChartDataGrid.Columns["Btk"].Width = ColWidth;
            depthChartDataGrid.Columns["Tkl"].Width = ColWidth;
            depthChartDataGrid.Columns["ThP"].Width = ColWidth;
            depthChartDataGrid.Columns["ThA"].Width = ColWidth;
            depthChartDataGrid.Columns["Pbk"].Width = ColWidth;
            depthChartDataGrid.Columns["Rbk"].Width = ColWidth;
            depthChartDataGrid.Columns["KP"].Width = ColWidth;
            depthChartDataGrid.Columns["KA"].Width = ColWidth;
            depthChartDataGrid.Columns["KR"].Width = ColWidth;

            depthChartDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            isInitialising = false;
        }

        private void OffSeasonHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {         
            string helpstring = "This utility simulates the effects that age has on a player's ability to\n" +
                "maintain weight. Simply click the 'Enable' checkbox and select your team to\n" +
                "proceed.\n\n" +
                "Occasionally you'll be presented with certain offseason scenarios that will require you\n" +
                "to make some choices. Follow the simple on screen prompts when this occurs.\n" +
                "Your head coach's Motivation rating is heavily referenced in determing not only the\n" +
                "frequency of weight gains/losses, but also in exaggerating/reducing the effects of\n" +
                "weight changes.\n\n" +
                "Every position has it's own listing of positional attribute means so if, for example,\n" +
                "a LT with a 49 speed looses 10 lbs his chances of gaining anything in Speed are slim\n" +
                "since the LT positional speed mean is 50. In contrast, a LT with a 35 speed who looses\n" +
                "10 lbs. will have a greater chance of seeing his speed slightly increase.\n\n" +
                "All of the aethestic player appearance categories are modified according to the change\n" +
                "in weight in proporion to the variance of weight gained/lost so significant weight\n" +
                "changes are noticable in-game.\n\n" +
                "All saves are simply handled by Gommo's save on the utilities startup form.\n\n" +
                "A .txt file of your teams changes is generated in the Gommo install directory\n" +
                "within the \\Conditioning folder.\n\n" +
                "This utility is the precursor for the Training Camp utility that is still under development.\n";

            MessageBox.Show(helpstring, "Help");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A .txt copy of your team's changes has been generated in\nthe \\Conditioning folder within the Gommo install directory.");
            TrainingCampForm form = new TrainingCampForm(model);
            form.InitialiseUI();
            this.Close();            
            form.Show();
        }

		private void btnSimOffseasonConditioning_Click(object sender, EventArgs e)
        {
            //if ((HoursLeft == 0) | (checkBox2.Checked == true))
           // {
                DialogResult dr = MessageBox.Show("Proceed with offseason conditioning for the selected team?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

                if (dr == DialogResult.Yes)
                {
                    btnSimOffseasonConditioning.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    /*
                    if (checkBox2.Checked == false)
                    {
                        CoachProgression();

                    }
                     */
                    RandomIssue();
                    Cursor.Current = Cursors.Arrow;                   
                    btnSimOffseasonConditioning.Enabled = false;
                    checkBox2.Enabled = false;
                    if (ScenarioFirstName == "")
                    {
                        MessageBox.Show("A .txt copy of your team's changes has been generated in\nthe \\Conditioning folder within the Madden Amp install directory.");
                    }
                }

                return;
          //  }
          //  else
          //      MessageBox.Show("Coaching Hours not equal to zero. Please re-distribute");
        }

        private void knwUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (knwUpDown.Value > knw)
            {
                HoursLeft = ((int)HoursLeft - (((int)knwUpDown.Value) - knw) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                knw = (int)knwUpDown.Value;
            }
            else if (knwUpDown.Value < knw)
            {
                HoursLeft = ((int)HoursLeft + (knw - ((int)knwUpDown.Value)) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                knw = (int)knwUpDown.Value;
            }
        }

        private void motUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (motUpDown.Value > mot)
            {
                HoursLeft = ((int)HoursLeft - (((int)motUpDown.Value) - mot) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                mot = (int)motUpDown.Value;
            }
            else if (motUpDown.Value < mot)
            {
                HoursLeft = ((int)HoursLeft + (mot - ((int)motUpDown.Value)) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                mot = (int)motUpDown.Value;
            } 
        }

        private void chmUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (chmUpDown.Value > chm)
            {
                HoursLeft = ((int)HoursLeft - (((int)chmUpDown.Value) - chm) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                chm = (int)chmUpDown.Value;
            }
            else if (chmUpDown.Value < chm)
            {
                HoursLeft = ((int)HoursLeft + (chm - ((int)chmUpDown.Value)) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                chm = (int)chmUpDown.Value;
            }
        }

        private void ethUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ethUpDown.Value > eth)
            {
                HoursLeft = ((int)HoursLeft - (((int)ethUpDown.Value) - eth) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                eth = (int)ethUpDown.Value;
            }
            else if (ethUpDown.Value < eth)
            {
                HoursLeft = ((int)HoursLeft + (eth - ((int)ethUpDown.Value)) * 2);
                HoursLeftlbl.Text = HoursLeft.ToString();
                eth = (int)ethUpDown.Value;
            }
        }

        private void qbUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (qbUpDown.Value > qb)
            {
                HoursLeft = ((int)HoursLeft - (((int)qbUpDown.Value) - qb) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                qb = (int)qbUpDown.Value;
            }
            else if (qbUpDown.Value < qb)
            {
                HoursLeft = ((int)HoursLeft + (qb - ((int)qbUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                qb = (int)qbUpDown.Value;
            }  
        }

        private void rbUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (rbUpDown.Value > rb)
            {
                HoursLeft = ((int)HoursLeft - (((int)rbUpDown.Value) - rb) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                rb = (int)rbUpDown.Value;
            }
            else if (rbUpDown.Value < rb)
            {
                HoursLeft = ((int)HoursLeft + (rb - ((int)rbUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                rb = (int)rbUpDown.Value;
            }
        }

        private void wrUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (wrUpDown.Value > wr)
            {
                HoursLeft = ((int)HoursLeft - (((int)wrUpDown.Value) - wr) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                wr = (int)wrUpDown.Value;
            }
            else if (wrUpDown.Value < wr)
            {
                HoursLeft = ((int)HoursLeft + (wr - ((int)wrUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                wr = (int)wrUpDown.Value;
            }    
            
        }

        private void olUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (olUpDown.Value > ol)
            {
                HoursLeft = ((int)HoursLeft - (((int)olUpDown.Value) - ol));
                HoursLeftlbl.Text = HoursLeft.ToString();
                ol = (int)olUpDown.Value;
            }
            else if (olUpDown.Value < ol)
            {
                HoursLeft = ((int)HoursLeft + (ol - ((int)olUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                ol = (int)olUpDown.Value;
            }
        }

        private void dlUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (dlUpDown.Value > dl)
            {
                HoursLeft = ((int)HoursLeft - (((int)dlUpDown.Value) - dl) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                dl = (int)dlUpDown.Value;
            }
            else if (dlUpDown.Value < dl)
            {
                HoursLeft = ((int)HoursLeft + (dl - ((int)dlUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                dl = (int)dlUpDown.Value;
            }
        }

        private void lbUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (lbUpDown.Value > lb)
            {
                HoursLeft = ((int)HoursLeft - (((int)lbUpDown.Value) - lb) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                lb = (int)lbUpDown.Value;
            }
            else if (lbUpDown.Value < lb)
            {
                HoursLeft = ((int)HoursLeft + (lb - ((int)lbUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                lb = (int)lbUpDown.Value;
            }
        }

        private void dbUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (dbUpDown.Value > db)
            {
                HoursLeft = ((int)HoursLeft - (((int)dbUpDown.Value) - db) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                db = (int)dbUpDown.Value;
            }
            else if (dbUpDown.Value < db)
            {
                HoursLeft = ((int)HoursLeft + (db - ((int)dbUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                db = (int)dbUpDown.Value;
            }   
        }

        private void kUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (kUpDown.Value > k)
            {
                HoursLeft = ((int)HoursLeft - (((int)kUpDown.Value) - k) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                k = (int)kUpDown.Value;
            }
            else if (kUpDown.Value < k)
            {
                HoursLeft = ((int)HoursLeft + (k - ((int)kUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                k = (int)kUpDown.Value;
            }    
        }

        private void pUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (pUpDown.Value > p)
            {
                HoursLeft = ((int)HoursLeft - (((int)pUpDown.Value) - p));
                HoursLeftlbl.Text = HoursLeft.ToString();
                p = (int)pUpDown.Value;
            }
            else if (pUpDown.Value < p)
            {
                HoursLeft = ((int)HoursLeft + (p - ((int)pUpDown.Value)) );
                HoursLeftlbl.Text = HoursLeft.ToString();
                p = (int)pUpDown.Value;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void clickToSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSliders();
        }

        private void loadDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDefaults();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
                btnSimOffseasonConditioning.Enabled = true;
        }

        private void SimCPUbutton_Click(object sender, EventArgs e)
        {
           




        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void simAllCPUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Proceed with offseason conditioning for all CPU teams? Please be\npatient. Sim process may take a few moments...\n\n*Note-OffSeason Scenario mode is disabled during CPU Simulation however...", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);

            if (dr == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
              //  checkBox1.Enabled = false;
                InitializeDataGrids();
                foreach (OwnerRecord team in model.TeamModel.GetTeamRecordsInOwnerTable())
                {
                    CPUteam = (OwnerRecord)team;

                    if (CPUteam.UserControlled == false)
                    {
                        if (checkBox2.Checked == false)
                        {
                            CoachProgressionCPU();
                        }
                        PhaseOneCPU();
                    }
                }
                Cursor.Current = Cursors.Arrow;
                MessageBox.Show("CPU Sim is now complete. A .txt file has been generated for all CPU teams in the installation directory within the Conditioning folder.\nYou may now close this form. If you still need to run conditioning on human teams simply re-enter via 'Training Camp'\nunder the 'Tools' menu strip.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        
        
       
    }
}