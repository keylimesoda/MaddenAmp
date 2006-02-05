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
using System.IO;

namespace MaddenEditor.Forms
{
    public partial class TrainingCampMeeting : Form
    {
        public TrainingCampForm tcform;
        private EditorModel model = null;
        Random random = new Random();
        private bool triggerChangedEvent = true;
        private DepthChartEditingModel depthEditingModel = null;
        private string Mode; int CurTmp = 0; int WindSpd = 0;

        public MaddenEditor.Core.EditorModel Model
        {
            set { this.model = value; }
        }

        public TrainingCampMeeting(EditorModel model, TrainingCampForm trainingForm)
        {
            tcform = trainingForm;
            tcform.trainingCampMeeting = this;
            depthEditingModel = new DepthChartEditingModel(model);
            InitializeComponent();
            label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value) + "%";
            string installDirectory = Application.StartupPath;

            if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 1))
            {

                DialogResult dr = MessageBox.Show("Enable Advanced mode?\n\nSelecting 'Yes' will enable Advanced mode,where you'll make daily changes to your\nCoach Sliders.\n\nIn doing so you'll have more control over managing your teams daily activites\nbut training camp takes longer in advanced mode than is does in basic mode.\n\nSelecting 'No' enables Basic mode where you make only two adjustmants to your\ncoaching sliders; Once at the beginning of week 1 and again at the start of week 2.\n\nPlayer activities will carry over to each day in Basic mode.\n\nRegardless of mode, you can exit Training Camp at any time\nduring the Player Allocation phase. Re-entering the training camp screen\nreloads your progress and allows you to continue...\n\n***NOTE: Advanced mode currently unavaiable in Beta release", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                if (dr == DialogResult.Yes)
                {
                    StreamWriter sw2 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\mode");
                    sw2.Write("advanced");
                    sw2.WriteLine();
                    sw2.Close();
                }
                else if (dr == DialogResult.No)
                {
                    StreamWriter sw2 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\mode");
                    sw2.Write("basic");
                    sw2.WriteLine();
                    sw2.Close();
                }

                infirmaryTxt.Text = "--All players are healthy and ready for camp";

            }
            //temp code for basic 
            StreamWriter sw10 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\mode");
            sw10.Write("basic");
            sw10.WriteLine();
            sw10.Close();

            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\mode");
            Mode = ct.ReadLine();
            ct.Close();


            conditioningUpDown.Enabled = false;
            positiondrillUpDown.Enabled = false;
            downtimeUpDown.Enabled = false;
            filmstudyUpDown.Enabled = false;
            specialteamsUpDown.Enabled = false;
            teamdrillUpDown.Enabled = false;
            filmstudyUpDown.Enabled = false;
            specialteamsUpDown.Enabled = false;
            ConditioningSldr.Enabled = false;
            PositionDrillSldr.Enabled = false;
            DownTimeSldr.Enabled = false;
            TeamDrillSldr.Enabled = false;
            FilmStudySldr.Enabled = false;
            SpecialTeamsSldr.Enabled = false;

            if (((tcform.Stage == "Hell Week") & (Mode == "advanced")) || ((tcform.Stage == "Hell Week") & (Mode == "basic") & (tcform.CurDay == 1)))
            {
                conditioningUpDown.Enabled = true;
                positiondrillUpDown.Enabled = true;
                downtimeUpDown.Enabled = true;
                ConditioningSldr.Enabled = true;
                PositionDrillSldr.Enabled = true;
                DownTimeSldr.Enabled = true;
                ConditioningSldr.Value = 10;
                ConditioningSldr.Minimum = 10;
                conditioningUpDown.Value = 10;
                conditioningUpDown.Minimum = 10;
                PositionDrillSldr.Value = 10;
                PositionDrillSldr.Minimum = 10;
                positiondrillUpDown.Value = 10;
                positiondrillUpDown.Minimum = 10;
                DownTimeSldr.Value = 10;
                DownTimeSldr.Minimum = 10;
                downtimeUpDown.Value = 10;
                downtimeUpDown.Minimum = 10;
                ConditioningSldr.Maximum = 80;
                conditioningUpDown.Maximum = 80;
                PositionDrillSldr.Maximum = 80;
                positiondrillUpDown.Maximum = 80;
                DownTimeSldr.Maximum = 80;
                downtimeUpDown.Maximum = 80;
                label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + downtimeUpDown.Value) + "%";
            }
            else if (((tcform.Stage == "Training Camp") & (tcform.CurDay == 8)))
            {
                conditioningUpDown.Enabled = true;
                positiondrillUpDown.Enabled = true;
                downtimeUpDown.Enabled = true;
                teamdrillUpDown.Enabled = true;
                specialteamsUpDown.Enabled = true;
                filmstudyUpDown.Enabled = true;
                ConditioningSldr.Enabled = true;
                PositionDrillSldr.Enabled = true;
                DownTimeSldr.Enabled = true;
                TeamDrillSldr.Enabled = true;
                DownTimeSldr.Enabled = true;
                FilmStudySldr.Enabled = true;
                SpecialTeamsSldr.Enabled = true;

                ConditioningSldr.Value = 10;
                ConditioningSldr.Minimum = 10;
                conditioningUpDown.Value = 10;
                conditioningUpDown.Minimum = 10;
                PositionDrillSldr.Value = 10;
                PositionDrillSldr.Minimum = 10;
                positiondrillUpDown.Value = 10;
                positiondrillUpDown.Minimum = 10;
                DownTimeSldr.Value = 10;
                DownTimeSldr.Minimum = 10;
                downtimeUpDown.Value = 10;
                downtimeUpDown.Minimum = 10;
                TeamDrillSldr.Minimum = 5;
                teamdrillUpDown.Minimum = 5;
                TeamDrillSldr.Value = 5;
                teamdrillUpDown.Value = 5;
                FilmStudySldr.Minimum = 10;
                filmstudyUpDown.Minimum = 10;
                FilmStudySldr.Value = 10;
                filmstudyUpDown.Value = 10;
                specialteamsUpDown.Minimum = 10;
                SpecialTeamsSldr.Minimum = 10;
                specialteamsUpDown.Value = 10;
                SpecialTeamsSldr.Value = 10;

                ConditioningSldr.Maximum = 55;
                conditioningUpDown.Maximum = 55;
                PositionDrillSldr.Maximum = 55;
                positiondrillUpDown.Maximum = 55;
                DownTimeSldr.Maximum = 55;
                downtimeUpDown.Maximum = 55;
                specialteamsUpDown.Maximum = 55;
                SpecialTeamsSldr.Maximum = 55;
                FilmStudySldr.Maximum = 55;
                filmstudyUpDown.Maximum = 55;
                TeamDrillSldr.Maximum = 50;
                teamdrillUpDown.Maximum = 50;

                label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + downtimeUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value) + "%";
            }
            else if (((tcform.Stage == "Training Camp") & (tcform.CurDay >= 9)))
            {
                if (Mode == "Advanced")
                {
                    conditioningUpDown.Enabled = true;
                    positiondrillUpDown.Enabled = true;
                    downtimeUpDown.Enabled = true;
                    teamdrillUpDown.Enabled = true;
                    specialteamsUpDown.Enabled = true;
                    filmstudyUpDown.Enabled = true;
                    ConditioningSldr.Enabled = true;
                    PositionDrillSldr.Enabled = true;
                    DownTimeSldr.Enabled = true;
                    TeamDrillSldr.Enabled = true;
                    DownTimeSldr.Enabled = true;
                    FilmStudySldr.Enabled = true;
                    SpecialTeamsSldr.Enabled = true;
                }
                else
                {
                    conditioningUpDown.Enabled = false;
                    positiondrillUpDown.Enabled = false;
                    downtimeUpDown.Enabled = false;
                    teamdrillUpDown.Enabled = false;
                    specialteamsUpDown.Enabled = false;
                    filmstudyUpDown.Enabled = false;
                    ConditioningSldr.Enabled = false;
                    PositionDrillSldr.Enabled = false;
                    DownTimeSldr.Enabled = false;
                    TeamDrillSldr.Enabled = false;
                    DownTimeSldr.Enabled = false;
                    FilmStudySldr.Enabled = false;
                    SpecialTeamsSldr.Enabled = false;

                }
                ConditioningSldr.Minimum = 10;
                conditioningUpDown.Minimum = 10;
                PositionDrillSldr.Minimum = 10;
                positiondrillUpDown.Minimum = 10;
                DownTimeSldr.Minimum = 10;
                downtimeUpDown.Minimum = 10;
                TeamDrillSldr.Minimum = 5;
                teamdrillUpDown.Minimum = 5;
                FilmStudySldr.Minimum = 10;
                filmstudyUpDown.Minimum = 10;
                specialteamsUpDown.Minimum = 10;
                SpecialTeamsSldr.Minimum = 10;

                ConditioningSldr.Maximum = 55;
                conditioningUpDown.Maximum = 55;
                PositionDrillSldr.Maximum = 55;
                positiondrillUpDown.Maximum = 55;
                DownTimeSldr.Maximum = 55;
                downtimeUpDown.Maximum = 55;
                specialteamsUpDown.Maximum = 55;
                SpecialTeamsSldr.Maximum = 55;
                FilmStudySldr.Maximum = 55;
                filmstudyUpDown.Maximum = 55;
                TeamDrillSldr.Maximum = 50;
                teamdrillUpDown.Maximum = 50;
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\coachsliders");
                string[] Sliders = sr.ReadLine().Split(',');
                sr.Close();

                ConditioningSldr.Value = int.Parse(Sliders[0]);
                conditioningUpDown.Value = int.Parse(Sliders[0]);
                PositionDrillSldr.Value = int.Parse(Sliders[1]);
                positiondrillUpDown.Value = int.Parse(Sliders[1]);
                TeamDrillSldr.Value = int.Parse(Sliders[2]);
                teamdrillUpDown.Value = int.Parse(Sliders[2]);
                FilmStudySldr.Value = int.Parse(Sliders[3]);
                filmstudyUpDown.Value = int.Parse(Sliders[3]);
                specialteamsUpDown.Value = int.Parse(Sliders[4]);
                SpecialTeamsSldr.Value = int.Parse(Sliders[4]);
                DownTimeSldr.Value = int.Parse(Sliders[5]);
                downtimeUpDown.Value = int.Parse(Sliders[5]);
                label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + downtimeUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value) + "%";
            }
            if ((Mode == "basic") & (tcform.CurDay >= 2) & (tcform.CurDay <= 7))
            {
                StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\coachsliders");
                string[] Sliders = sr.ReadLine().Split(',');
                sr.Close();

                ConditioningSldr.Value = int.Parse(Sliders[0]);
                conditioningUpDown.Value = int.Parse(Sliders[0]);
                PositionDrillSldr.Value = int.Parse(Sliders[1]);
                positiondrillUpDown.Value = int.Parse(Sliders[1]);
                TeamDrillSldr.Value = int.Parse(Sliders[2]);
                teamdrillUpDown.Value = int.Parse(Sliders[2]);
                FilmStudySldr.Value = int.Parse(Sliders[3]);
                filmstudyUpDown.Value = int.Parse(Sliders[3]);
                specialteamsUpDown.Value = int.Parse(Sliders[4]);
                SpecialTeamsSldr.Value = int.Parse(Sliders[4]);
                DownTimeSldr.Value = int.Parse(Sliders[5]);
                downtimeUpDown.Value = int.Parse(Sliders[5]);
            }
            if (tcform.CurDay != 1)
            {
                StreamReader index = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\currentteam");
                int CurTeamIndex = int.Parse(index.ReadLine());
                index.Close();
                int teamId = CurTeamIndex;
                int positionId = 0;
                string Pos = "";
                List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);
                foreach (PlayerRecord valObject in teamPlayers)
                {

                    if (valObject.PositionId == 0)
                    {
                        Pos = "QB";
                    }
                    else if (valObject.PositionId == 1)
                    {
                        Pos = "HB";
                    }
                    else if (valObject.PositionId == 2)
                    {
                        Pos = "FB";
                    }
                    else if (valObject.PositionId == 3)
                    {
                        Pos = "WR";
                    }
                    else if (valObject.PositionId == 4)
                    {
                        Pos = "TE";
                    }
                    else if (valObject.PositionId == 5 | valObject.PositionId == 6 | valObject.PositionId == 7 | valObject.PositionId == 8 | valObject.PositionId == 9)
                    {
                        Pos = "OL";
                    }
                    else if (valObject.PositionId == 10 | valObject.PositionId == 11 | valObject.PositionId == 12)
                    {
                        Pos = "DL";
                    }
                    else if (valObject.PositionId == 13 | valObject.PositionId == 14 | valObject.PositionId == 15)
                    {
                        Pos = "LB";
                    }
                    else if (valObject.PositionId == 16 | valObject.PositionId == 17 | valObject.PositionId == 18)
                    {
                        Pos = "DB";
                    }
                    else if (valObject.PositionId == 19)
                    {
                        Pos = "KP";
                    }
                    else if (valObject.PositionId == 20)
                    {
                        Pos = "KP";
                    }
                    //current totals
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName + " Totals");
                    string TotalsContents = sr.ReadToEnd();
                    sr.Close();
                    string[] TotalsContentsLine = TotalsContents.Split(',');
                    string[] CurInjury = TotalsContentsLine[1].Split(';');
                    /*
                                        if (int.Parse(TotalsContentsLine[2]) >= 7)
                                        {
                                            int wk = int.Parse(TotalsContentsLine[2]) / 7;
                                            TotalsContentsLine[2] = wk + " weeks.";
                                        }
                    */

                    if (TotalsContentsLine[1] != "")
                    {
                        if (int.Parse(TotalsContentsLine[2]) == 91)
                        {
                            infirmaryTxt.Text = infirmaryTxt.Text + "--" + valObject.FirstName + " " + valObject.LastName + " " + CurInjury[1] + " out for the entire season.\r\n\r\n";

                                                    }
                        else if (int.Parse(TotalsContentsLine[2]) != 91)
                        {
                            infirmaryTxt.Text = infirmaryTxt.Text + "--" + valObject.FirstName + " " + valObject.LastName + " " + CurInjury[1] + " out for " + TotalsContentsLine[2] + " day(s).\r\n\r\n";
 
                        }

                    }


                }
               
            }
            OutdoorsRadioButton.Checked = true;
            WeatherGenerator();
        }
        private void WeatherGenerator()
        {
            int MinTmp = 0;
            int MaxTmp = 0;            
            int TmpDeviation = 0;
            string WndDir = "";
            int Weather = (int)(90 * random.NextDouble() + 1);
            string pic = "";
            tcform.HeadCold = 0;
            tcform.TghRainBonus = 0;
            tcform.TghBonus = 0;
            tcform.CatBonus = 0;
            tcform.WthInjIncrease = 0;

            if (Weather <= 25)
            {
                pic = "sunny";               
                TmpDeviation = (int)(10 * random.NextDouble() + 1);
                WindSpd = (int)(7 * random.NextDouble()); 

                if (TmpDeviation <= 4)
                {
                    MinTmp = 89;
                    MaxTmp = 7;
                }
                else if (TmpDeviation <= 6)
                {
                    MinTmp = 87;
                    MaxTmp = 10;
                }
                else if (TmpDeviation <= 8)
                {
                    MinTmp = 85;
                    MaxTmp = 14;
                }
                else if (TmpDeviation <= 9)
                {
                    MinTmp = 83;
                    MaxTmp = 20;
                }
                else if (TmpDeviation == 10)
                {
                    MinTmp = 100;
                    MaxTmp = 7;
                }

                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                if (CurTmp <= 80)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got lots of sun today. Current Temperature is " + CurTmp + ". Very comfortable outside. Perfect day for runnin' 'um into the ground.";
                }
                else if (CurTmp <= 85)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got lots of sun today. Current Temperature is " + CurTmp + ". Fairly comfortable outside. Can't ask for much better weather.";
                }
                else if (CurTmp <= 90)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got lots of sun today but it's a bit humid. Current Temperature is " + CurTmp + ". Kinda sticky outside. We can still push them but we should watch for signs of heat exhaustion.";
                }
                else if (CurTmp <= 95)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got lots of sun today but it's pretty darn hot. Current Temperature is " + CurTmp + ". Very sticky outside. Let's make sure we don't run them into the ground.(Conditioning effects slightly increased as are heat related illnesses)";
                }
                else if (CurTmp <= 99)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. For the love of Pete it's hot outside. Current Temperature is " + CurTmp + ". Glad I'm not runnin' around. We've got to be very careful with how hard we push their conditioning today.(Conditioning effects increased as are heat related illnesses)";
                }
                else if (CurTmp > 99)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. We've got a major heat advisory. Current Temperature is " + CurTmp + ". We should consider moving today's practice indoors and into the air conditioning. The conditioning drills will be a bit less effective indoors than they would be outside but they'll drop like flies if we don't. Would be good for weight loss though.";
                }

            }
            else if (Weather <= 37)
            {
                pic = "PartlyCloudy";
                TmpDeviation = (int)(10 * random.NextDouble() + 1);
                WindSpd = (int)(13 * random.NextDouble()+ 5);
                if (TmpDeviation <= 4)
                {
                    MinTmp = 85;
                    MaxTmp = 7;
                }
                else if (TmpDeviation <= 6)
                {
                    MinTmp = 83;
                    MaxTmp = 10;
                }
                else if (TmpDeviation <= 8)
                {
                    MinTmp = 79;
                    MaxTmp = 14;
                }
                else if (TmpDeviation <= 9)
                {
                    MinTmp = 77;
                    MaxTmp = 20;
                }
                else if (TmpDeviation == 10)
                {
                    MinTmp = 70;
                    MaxTmp = 5;
                }

                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                if (CurTmp <= 75)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got some clouds in the forecast for today. Current Temperature is " + CurTmp + ". A bit on the cool side for this time of year. Great day for football.";
                }
                else if (CurTmp <= 85)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. An excellent forecast for todays practice. Current Temperature is " + CurTmp + ". Comfortable and warm. Can't ask for much better weather.";
                }
                else if (CurTmp <= 90)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Looks we got lots of sun today but it's a bit humid. Current Temperature is " + CurTmp + ". Kinda sticky outside. We can still push them but we should watch for signs of heat exhaustion.";
                }
                else if (CurTmp <= 100)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Even with the partial cloud cover it's still scorching hot outside. Current Temperature is " + CurTmp + ". I don't think we need to move them inside but it's your call.(Conditioning effects slightly increased as are heat related illnesses)";
                }               
              
            }
            else if (Weather <= 48)
            {
                pic = "cloudy";
                TmpDeviation = (int)(10 * random.NextDouble() + 1);
                WindSpd = (int)(15 * random.NextDouble() + 7);
                if (TmpDeviation <= 4)
                {
                    MinTmp = 83;
                    MaxTmp = 7;
                }
                else if (TmpDeviation <= 6)
                {
                    MinTmp = 81;
                    MaxTmp = 10;
                }
                else if (TmpDeviation <= 8)
                {
                    MinTmp = 77;
                    MaxTmp = 14;
                }
                else if (TmpDeviation <= 9)
                {
                    MinTmp = 75;
                    MaxTmp = 20;
                }
                else if (TmpDeviation == 10)
                {
                    MinTmp = 68;
                    MaxTmp = 5;
                }

                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                if (CurTmp <= 73)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. A cloudy forecast for today. Current Temperature is " + CurTmp + ". A bit on the cool side for this time of year. Great day for football but weak armed QBs might struggle along with weaker kickers if it's too windy.";
                }
                else if (CurTmp <= 80)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Lots of clouds in todays forecast. Current Temperature is " + CurTmp + ". A little cool. Great day for football but weak armed QBs might struggle along with weaker kickers if it's too windy.";
                }
                else if (CurTmp <= 95)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Clouds and heat for today. Current Temperature is " + CurTmp + ". At least the air is moving some with a nice breeze. An ok day for football but weak armed QBs might struggle along with weaker kickers if it's too windy.";
                }
                                            
            }
            else if (Weather <= 60)
            {
                pic = "FairWindy";
                TmpDeviation = (int)(10 * random.NextDouble() + 1);
                WindSpd = (int)(20 * random.NextDouble() + 20);
                if (TmpDeviation <= 4)
                {
                    MinTmp = 82;
                    MaxTmp = 7;
                }
                else if (TmpDeviation <= 6)
                {
                    MinTmp = 80;
                    MaxTmp = 10;
                }
                else if (TmpDeviation <= 8)
                {
                    MinTmp = 77;
                    MaxTmp = 14;
                }
                else if (TmpDeviation <= 9)
                {
                    MinTmp = 74;
                    MaxTmp = 15;
                }
                else if (TmpDeviation == 10)
                {
                    MinTmp = 70;
                    MaxTmp = 5;
                }

                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                if (CurTmp <= 75)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. She's blowin' like my ex-w...I mean, uh, it's pretty windy. Current Temperature is " + CurTmp + ". You've got a choice; stay out here and get a good conditioning workout in, or head indoors and watch the QB's and kickers actually throw/kick with accuracy.";
                }
                else if (CurTmp <= 80)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Lots of wind but fair skies. Current Temperature is " + CurTmp + ". A little cool. Good day for conditioning drills but QBs will struggle with accuracy along with the kickers. It'll help some with throwing/kicking power though.";
                }
                else if (CurTmp <= 95)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Fair skies but very windy. Current Temperature is " + CurTmp + ". My clipboard notes keep blowing away. Great day for conditioning drills but QBs will struggle with accuracy along with the kickers. It'll help some with throwing/kicking power though";
                }
            }
            else if (Weather <= 72)
            {
                pic = "Showers";
                TmpDeviation = (int)(10 * random.NextDouble() + 1);
                WindSpd = (int)(11 * random.NextDouble() + 5);
                if (TmpDeviation <= 4)
                {
                    MinTmp = 79;
                    MaxTmp = 7;
                }
                else if (TmpDeviation <= 6)
                {
                    MinTmp = 77;
                    MaxTmp = 10;
                }
                else if (TmpDeviation <= 8)
                {
                    MinTmp = 74;
                    MaxTmp = 14;
                }
                else if (TmpDeviation <= 9)
                {
                    MinTmp = 69;
                    MaxTmp = 20;
                }
                else if (TmpDeviation == 10)
                {
                    MinTmp = 93;
                    MaxTmp = 8;
                }

                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                if (CurTmp <= 80)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Cool and rainy. Current Temperature is " + CurTmp + ". We can keep them outside and won't have to worry about heat exhaustion. Plus the slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). Playing conditions aren't too bad. Shouldn't much affect injury chances. All we're really risking is the occaisional cold.";
                }
                else if (CurTmp <= 88)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. The forecast for today is warm with showers. Current Temperature is " + CurTmp + ". The slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). Playing conditions aren't too bad. Shouldn't much affect injury chances. All we're really risking is the occaisional cold.";
                }
                else if (CurTmp > 88)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. It's hot and humid but there's a cool misty rainfall. Current Temperature is " + CurTmp + ". The slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). Playing conditions aren't too bad. Shouldn't much affect injury chances. All we're really risking is the occaisional cold.";
                }
                tcform.HeadCold = (int)(2 * random.NextDouble() + 1);
                tcform.WthInjIncrease = 1;
                tcform.CatBonus = (decimal).025;
            }
            else if (Weather <= 82)
            {
                pic = "HvyRain";
                WindSpd = (int)(11 * random.NextDouble() + 5);
                    MinTmp = 73;
                    MaxTmp = 10;
                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. My clipboard is soaked! It's coming down in buckets out there. Current Temperature is " + CurTmp + ". It's a monsoon, but there're no reports of lightening so we could practice outside if you so choose. Doing so will benefit nearly every position as they'll have to maintain a sharp focus in the fierce conditions. It'll toughen these patsies up some too. Just be prepared for a guy or two catching a head cold. One more thing, the miserable conditions have made the footing quite slippery, so the chance of injury is elevated.";
                tcform.HeadCold = (int)(5 * random.NextDouble() + 1);
                tcform.TghBonus = (decimal)0.1;
                tcform.CatBonus = (decimal).05;
                tcform.WthInjIncrease = 3;
                tcform.HvyRainAwrBonus = (decimal).5;
            }
            else if (Weather > 82)
            {
                pic = "thunder";
                WindSpd = (int)(12 * random.NextDouble() + 6);                
                    MinTmp = 70;
                    MaxTmp = 10;
                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Thunder and lightening forecast for today. It's coming down in buckets out there. Current Temperature is " + CurTmp + ". We've no choice. We've got to move practice indoors today.";
                OutdoorsRadioButton.Enabled = false;
                IndoorsRadioButton.Checked = true;
            }


            int Wnd = (int)(8 * random.NextDouble() + 1);
            if (Wnd == 1)
            {
                WndDir = "N";
            }
            else if (Wnd == 2)
            {
                WndDir = "NE";
            }
            else if (Wnd == 3)
            {
                WndDir = "E";
            }
            else if (Wnd == 4)
            {
                WndDir = "SE";
            }
            else if (Wnd == 5)
            {
                WndDir = "S";
            }
            else if (Wnd == 6)
            {
                WndDir = "SW";
            }
            else if (Wnd == 7)
            {
                WndDir = "W";
            }
            else if (Wnd == 8)
            {
                WndDir = "NW";
            }

            DialogTxt.SelectionStart = DialogTxt.Text.Length;
            DialogTxt.SelectionLength = 0;
            DialogTxt.ScrollToCaret();
            label1.Text = CurTmp.ToString();
            label4.Text = WindSpd + " mph " + WndDir;
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file =
                thisExe.GetManifestResourceStream("MaddenEditor.Resources." + pic + ".JPG");
            this.CurWeatherPic.Image = Image.FromStream(file);

        }

        private void Injury()
        {

        }
        private void ReloadMeeting()
        {

            if (tcform.Stage == "Hell Week")
            {
                TeamDrillSldr.Enabled = false;
                FilmStudySldr.Enabled = false;
                SpecialTeamsSldr.Enabled = false;
                DownTimeSldr.Enabled = false;
                teamdrillUpDown.Enabled = false;
                filmstudyUpDown.Enabled = false;
                specialteamsUpDown.Enabled = false;
                downtimeUpDown.Enabled = false;
            }
            else if (tcform.Stage == "Training Camp")
            {
                TeamDrillSldr.Enabled = true;
                FilmStudySldr.Enabled = true;
                SpecialTeamsSldr.Enabled = true;
                DownTimeSldr.Enabled = true;
                teamdrillUpDown.Enabled = true;
                filmstudyUpDown.Enabled = true;
                specialteamsUpDown.Enabled = true;
                downtimeUpDown.Enabled = true;
            }




        }

        private void LoadSliders()
        {
            /*
            StreamWriter sw;
            string installDirectory = Application.StartupPath;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\");

            }
            if (!File.Exists(installDirectory + "\\Conditioning\\\\TrainingCamp\\MeetingSliders")) // Create the Slider Profile if not present.
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\\\TrainingCamp\\MeetingSliders");
                sw = new StreamWriter(file);

                sw.WriteLine("Conditioning\t50");
                sw.WriteLine("PositionDrills\t50");
                sw.WriteLine("TeamDrills\t15");
                sw.WriteLine("FilmStudy\t10");
                sw.WriteLine("SpecialTeams\t0");
                sw.WriteLine("DownTime\t0");

                sw.Close();
            }

            */

        }

        private void SliderValueChanged(object s, EventArgs e)
        {

            if (tcform.Stage != "Hell Week")
            {
                if (!triggerChangedEvent)
                {

                    return;
                }
                NumericUpDown UpDownToChange = null;
                TrackBar sender = (TrackBar)s;

                if (sender == ConditioningSldr)
                {
                    UpDownToChange = conditioningUpDown;
                    UpDownToChange.Maximum = 100 - (positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value);// -sender.Value;
                }
                else if (sender == PositionDrillSldr)
                {
                    UpDownToChange = positiondrillUpDown;
                    UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value);// - sender.Value;
                }
                else if (sender == TeamDrillSldr)
                {
                    UpDownToChange = teamdrillUpDown;
                    UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + positiondrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value);// - sender.Value;
                }
                else if (sender == FilmStudySldr)
                {
                    UpDownToChange = filmstudyUpDown;
                    UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value);// - sender.Value;
                }
                else if (sender == SpecialTeamsSldr)
                {
                    UpDownToChange = specialteamsUpDown;
                    UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + downtimeUpDown.Value);// - sender.Value;
                }
                else if (sender == DownTimeSldr)
                {
                    UpDownToChange = downtimeUpDown;
                    UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value);// - sender.Value;
                }


                if (sender.Value <= UpDownToChange.Maximum)
                {
                    triggerChangedEvent = false;
                    UpDownToChange.Value = sender.Value;
                    triggerChangedEvent = true;
                    label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value) + "%";
                }
                else
                {
                    UpDownToChange.Value = UpDownToChange.Maximum;
                    sender.Value = (int)UpDownToChange.Maximum;
                }
                
            }
            else if (tcform.Stage == "Hell Week")
            {               
                    if (!triggerChangedEvent)
                    {

                        return;
                    }
                    NumericUpDown UpDownToChange = null;
                    TrackBar sender = (TrackBar)s;

                    if (sender == ConditioningSldr)
                    {
                        UpDownToChange = conditioningUpDown;
                        UpDownToChange.Maximum = 100 - (positiondrillUpDown.Value + downtimeUpDown.Value);// -sender.Value;
                    }
                    else if (sender == PositionDrillSldr)
                    {
                        UpDownToChange = positiondrillUpDown;
                        UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + downtimeUpDown.Value);// - sender.Value;
                    }
                    else if (sender == DownTimeSldr)
                    {
                        UpDownToChange = downtimeUpDown;
                        UpDownToChange.Maximum = 100 - (conditioningUpDown.Value + positiondrillUpDown.Value);// - sender.Value;
                    }


                    if (sender.Value <= UpDownToChange.Maximum)
                    {
                        triggerChangedEvent = false;
                        UpDownToChange.Value = sender.Value;
                        triggerChangedEvent = true;
                        label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + downtimeUpDown.Value) + "%";
                    }
                    else
                    {
                        UpDownToChange.Value = UpDownToChange.Maximum;
                        sender.Value = (int)UpDownToChange.Maximum;
                    }

                
            }


        }

        private void UpDown_ValueChanged(object s, EventArgs e)
        {
            if (tcform.Stage != "Hell Week")
            {
                if (!triggerChangedEvent)
                {
                    return;
                }

                NumericUpDown sender = (NumericUpDown)s;
                TrackBar sliderToChange = null;

                if (sender == conditioningUpDown)
                {
                    sliderToChange = ConditioningSldr;
                    sliderToChange.Maximum = (int)(100 - (positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == positiondrillUpDown)
                {
                    sliderToChange = PositionDrillSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == teamdrillUpDown)
                {
                    sliderToChange = TeamDrillSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + positiondrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == filmstudyUpDown)
                {
                    sliderToChange = FilmStudySldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == specialteamsUpDown)
                {
                    sliderToChange = SpecialTeamsSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == downtimeUpDown)
                {
                    sliderToChange = DownTimeSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value));// - sender.Value);
                }

                if (sender.Value <= sliderToChange.Maximum)
                {
                    triggerChangedEvent = false;
                    sliderToChange.Value = (int)sender.Value;
                    triggerChangedEvent = true;
                    label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + teamdrillUpDown.Value + filmstudyUpDown.Value + specialteamsUpDown.Value + downtimeUpDown.Value) + "%";
                }
                else
                {
                    sliderToChange.Value = sliderToChange.Maximum;
                    sender.Value = sliderToChange.Maximum;
                }
            }
            else if (tcform.Stage == "Hell Week")
            {
                if (!triggerChangedEvent)
                {
                    return;
                }

                NumericUpDown sender = (NumericUpDown)s;
                TrackBar sliderToChange = null;

                if (sender == conditioningUpDown)
                {
                    sliderToChange = ConditioningSldr;
                    sliderToChange.Maximum = (int)(100 - (positiondrillUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == positiondrillUpDown)
                {
                    sliderToChange = PositionDrillSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + downtimeUpDown.Value));// - sender.Value);
                }
                else if (sender == downtimeUpDown)
                {
                    sliderToChange = DownTimeSldr;
                    sliderToChange.Maximum = (int)(100 - (conditioningUpDown.Value + positiondrillUpDown.Value));// - sender.Value);
                }

                if (sender.Value <= sliderToChange.Maximum)
                {
                    triggerChangedEvent = false;
                    sliderToChange.Value = (int)sender.Value;
                    triggerChangedEvent = true;
                    label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value + downtimeUpDown.Value) + "%";
                }
                else
                {
                    sliderToChange.Value = sliderToChange.Maximum;
                    sender.Value = sliderToChange.Maximum;
                }
            } 

        }
        public void OutputRoster()
        {
            string installDirectory = Application.StartupPath;


            StreamReader ct = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\currentteam");
            int CurTeamIndex = int.Parse(ct.ReadLine());
            ct.Close();
            StreamWriter sw;
            int Con = 0; int PosDrill = 0; int Team = 0; int Film = 0; int Special = 0; int Down = 0;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam);
            }

            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\coachsliders");
            string line = sr.ReadLine();
            sr.Close();
            string[] CoachSliders = line.Split(',');
            Con = int.Parse(CoachSliders[0]);
            PosDrill = int.Parse(CoachSliders[1]);
            Team = int.Parse(CoachSliders[2]);
            Film = int.Parse(CoachSliders[3]);
            Special = int.Parse(CoachSliders[4]);
            Down = int.Parse(CoachSliders[5]);

            int teamId = CurTeamIndex;
            int positionId = 0;
            string Pos = "";
            List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

            foreach (PlayerRecord valObject in teamPlayers)
            {
                if (valObject.PositionId == 0)
                {
                    Pos = "QB";
                }
                else if (valObject.PositionId == 1)
                {
                    Pos = "HB";
                }
                else if (valObject.PositionId == 2)
                {
                    Pos = "FB";
                }
                else if (valObject.PositionId == 3)
                {
                    Pos = "WR";
                }
                else if (valObject.PositionId == 4)
                {
                    Pos = "TE";
                }
                else if ((valObject.PositionId == 5) | (valObject.PositionId == 6) | (valObject.PositionId == 7) | (valObject.PositionId == 8) | (valObject.PositionId == 9))
                {
                    Pos = "OL";
                }
                else if ((valObject.PositionId == 10) | (valObject.PositionId == 11) | (valObject.PositionId == 12))
                {
                    Pos = "DL";
                }
                else if ((valObject.PositionId == 13) | (valObject.PositionId == 14) | (valObject.PositionId == 15))
                {
                    Pos = "LB";
                }
                else if ((valObject.PositionId == 16) | (valObject.PositionId == 17) | (valObject.PositionId == 18))
                {
                    Pos = "DB";
                }
                else if ((valObject.PositionId == 19) | (valObject.PositionId == 20))
                {
                    Pos = "KP";
                }
                if (tcform.CurDay == 1)
                {
                    Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos);
                    FileStream file = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    sw = new StreamWriter(file);
                    sw.Write(Con + "," + PosDrill + "," + Team + "," + Con + "," + Film + "," + Special + "," + Down);
                    sw.WriteLine();
                    sw.Write(valObject.FirstName + " " + valObject.LastName);
                    sw.Write(",");
                    sw.Write(valObject.Weight + 160);
                    sw.Write(",");
                    valObject.Overall = valObject.CalculateOverallRating(valObject.PositionId);
                    //Reload the overall rating
                    valObject.Overall = valObject.Overall;
                    sw.Write(valObject.Overall);
                    sw.Write(",");
                    sw.Write(valObject.Speed);
                    sw.Write(",");
                    sw.Write(valObject.Acceleration);
                    sw.Write(",");
                    sw.Write(valObject.Agility);
                    sw.Write(",");
                    sw.Write(valObject.Strength);
                    sw.Write(",");
                    sw.Write(valObject.Stamina);
                    sw.Write(",");
                    sw.Write(valObject.Injury);
                    sw.Write(",");
                    sw.Write(valObject.Toughness);
                    sw.Write(",");
                    sw.Write(valObject.Morale);
                    sw.Write(",");
                    sw.Write(valObject.Awareness);
                    sw.Write(",");
                    sw.Write(valObject.Catching);
                    sw.Write(",");
                    sw.Write(valObject.Carrying);
                    sw.Write(",");
                    sw.Write(valObject.Jumping);
                    sw.Write(",");
                    sw.Write(valObject.BreakTackle);
                    sw.Write(",");
                    sw.Write(valObject.Tackle);
                    sw.Write(",");
                    sw.Write(valObject.ThrowPower);
                    sw.Write(",");
                    sw.Write(valObject.ThrowAccuracy);
                    sw.Write(",");
                    sw.Write(valObject.PassBlocking);
                    sw.Write(",");
                    sw.Write(valObject.RunBlocking);
                    sw.Write(",");
                    sw.Write(valObject.KickPower);
                    sw.Write(",");
                    sw.Write(valObject.KickAccuracy);
                    sw.Write(",");
                    sw.Write(valObject.KickReturn);
                    sw.WriteLine();
                    sw.Close();
                }
                else if (tcform.CurDay > 1)
                {
                    sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    string OldTotalsContents = sr.ReadToEnd();
                    sr.Close();
                    string[] OldTotalsContentsLine = OldTotalsContents.Split('\n');
                    string[] OldRatings = OldTotalsContentsLine[1].Split(',');
                    
                    FileStream file = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                    sw = new StreamWriter(file);
                    sw.Write(Con + "," + PosDrill + "," + Team + "," + Con + "," + Film + "," + Special + "," + Down);
                    sw.WriteLine();
                    sw.Write(valObject.FirstName + " " + valObject.LastName);
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[1]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[2]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[3]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[4]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[5]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[6]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[7]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[8]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[9]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[10]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[11]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[12]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[13]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[14]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[15]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[16]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[17]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[18]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[19]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[20]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[21]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[22]));
                    sw.Write(",");
                    sw.Write(int.Parse(OldRatings[23]));
                    sw.WriteLine();
                    sw.Close();



                }




            }

        }
        private void CloseCheck()
        {
            if (label6.Text != "100%")
            {
                MessageBox.Show("Coaching allocations not equal to 100%.");
                return;
            }
            else if (label6.Text == "100%")
            {
                DialogResult dr = MessageBox.Show("Start the day with the current coaching allocations?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if (dr == DialogResult.No)
                {
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    string installDirectory = Application.StartupPath;
                    
                    StreamWriter sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\coachsliders");
                    sw.Write(conditioningUpDown.Value + "," + positiondrillUpDown.Value + "," + teamdrillUpDown.Value + "," + filmstudyUpDown.Value + "," + specialteamsUpDown.Value + "," + downtimeUpDown.Value);
                    sw.WriteLine();
                    sw.Close();
                    
                    if (IndoorsRadioButton.Checked == true)
                    {
                        tcform.Facility = "Indoors";
                       // sw.Write("indoors");
                    }
                    else if (OutdoorsRadioButton.Checked == true)
                    {
                        tcform.Facility = "Outdoors";
                       // sw.Write("outdoors");
                    }
                   // sw.Close();
                    if (tcform.CurDay == 1)
                    {
                        MessageBox.Show("Age declination will now be simulated decrementing physical attributes based on age.\nA .txt before/after file is generated in the applications install directory within\nConditioning/TrainingCamp/" + tcform.CurTeam + "/System/AgeDeclination.txt\nSimply minimize Madden Amp and navigate to the file in Windows.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OutputRoster();
                        tcform.AgeDeclination();
                        OutputRoster();
                        
                    }
                    if ((Mode == "advanced") || ((tcform.Stage == "Hell Week") & (Mode == "basic") & (tcform.CurDay == 1)) || ((tcform.Stage == "Training Camp")  & (Mode == "basic") & (tcform.CurDay == 8)))
                    {
                        OutputRoster();
                    }
                   
                    StreamWriter sw1 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\system");
                    if ((tcform.Stage == "Hell Week") & (tcform.CurDay <= 6))
                    {
                        tcform.CurDay++;
                        sw1.Write(tcform.CurTeam + "," + "Hell Week" + "," + tcform.CurDay);
                    }
                    else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 7))
                    {                       
                        sw1.Write(tcform.CurTeam + "," + "Training Camp" + ",8");
                    }
                    else if ((tcform.Stage == "Training Camp") & (tcform.CurDay <= 14))
                    {    
                          tcform.CurDay++;
                        sw1.Write(tcform.CurTeam + "," + "Training Camp" + "," + tcform.CurDay);
                    }
                   


                    sw1.Close();

                    StreamWriter sw2 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\TeamDrills");
                    sw2.Write(teamdrillUpDown.Value + "," + 0 + "," + 0);
                    sw2.WriteLine();
                    sw2.Close();

                    
                    this.Close();
                    TrainingCampForm tc = new TrainingCampForm(model);
                    StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\system");
                    string Allcontents = sr.ReadLine();
                    sr.Close();
                    string[] Line = Allcontents.Split(',');
                    tcform.CurTeam = Line[0];
                    tcform.Stage = Line[1];
                    tcform.CurDay = Int32.Parse(Line[2]);

                    if (tcform.CurDay <= 14)
                    {
                        tcform.label1.Text = "Advance to " + tcform.Stage + " Day " + tcform.CurDay + "...";
                    }
                        /*
                    else if (tcform.CurDay == 7)
                    {
                        tcform.label1.Text = "Process Hell Week and view Progression...";
                    }
                    else if (tcform.CurDay == 14)
                    {
                        tcform.label1.Text = "Finalize Training Camp and view Progression...";
                    }
                   */

                    tcform.groupBox6.Enabled = true;
                    tcform.filterPositionComboBox.SelectedIndex = 0;
                    tcform.ActivityCmb.SelectedIndex = 0;
                  //  tcform.GroupAssign.SelectedIndex = 0;
                    tcform.selectHumanTeam.SelectedText = tcform.CurTeam;
                    tcform.groupBox7.Enabled = false;
                    tcform.WindSpeed = WindSpd;
                    tcform.Temp = CurTmp;
                    if (tcform.CurDay <= 7)
                    {
                        tcform.ActivityCmb.Items.Remove("Team");
                    }
                    if (tcform.CurDay == 8)
                    {
                        tcform.ActivityCmb.Items.Add("Team");
                    }
                    tcform.Show();
                    tcform.RefillRosterView();

                }


                //   WeatherGenerator();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            CloseCheck();

        }

       



           }
}