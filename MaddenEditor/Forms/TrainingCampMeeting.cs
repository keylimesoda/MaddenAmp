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
            if (tcform.Stage == "Hell Week")
            {
                ConditioningSldr.Value = 5;
                ConditioningSldr.Minimum = 5;
                conditioningUpDown.Value = 5;
                conditioningUpDown.Minimum = 5;
                PositionDrillSldr.Value = 5;
                PositionDrillSldr.Minimum = 5;
                positiondrillUpDown.Value = 5;
                positiondrillUpDown.Minimum = 5;
                DownTimeSldr.Value = 5;
                DownTimeSldr.Minimum = 5;
                downtimeUpDown.Value = 5;
                downtimeUpDown.Minimum = 5;
                ConditioningSldr.Maximum = 90;
                conditioningUpDown.Maximum = 90;
                PositionDrillSldr.Maximum = 90;
                positiondrillUpDown.Maximum = 90;
                DownTimeSldr.Maximum = 90;
                downtimeUpDown.Maximum = 90;
                teamdrillUpDown.Enabled = false;
                filmstudyUpDown.Enabled = false;
                specialteamsUpDown.Enabled = false;
                TeamDrillSldr.Enabled = false;
                FilmStudySldr.Enabled = false;
                SpecialTeamsSldr.Enabled = false;
                label6.Text = (conditioningUpDown.Value + positiondrillUpDown.Value  + downtimeUpDown.Value) + "%";

            }

            OutdoorsRadioButton.Checked = true;
            WeatherGenerator();
        }

        private void WeatherGenerator()
        {
            int MinTmp = 0;
            int MaxTmp = 0;
            int CurTmp = 0;
            int WindSpd = 0;
            int TmpDeviation = 0;
            string WndDir = "";
            int Weather = (int)(90 * random.NextDouble() + 1);
            string pic = "";

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
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Lots of wind but fair skies. Current Temperature is " + CurTmp + ". A little cool. Good day for conditioning drills but QBs will struggle along with the kickers.";
                }
                else if (CurTmp <= 95)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Fair skies but very windy. Current Temperature is " + CurTmp + ". My clipboard notes keep blowing away. Great day for conditioning drills but QBs will struggle along with the kickers.";
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
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. Cool and rainy. Current Temperature is " + CurTmp + ". We can keep them outside and won't have to worry about heat exhaustion. Plus the slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). All we're risking is the occaisional cold.";
                }
                else if (CurTmp <= 88)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. The forecast for today is warm with showers. Current Temperature is " + CurTmp + ". The slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). All we're risking is the occaisional cold.";
                }
                else if (CurTmp > 88)
                {
                    DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. It's hot and humid but there's a cool misty rainfall. Current Temperature is " + CurTmp + ". The slick conditions will benefit receivers(catching a slick ball), running backs and QBs(decrease fumbling). All we're risking is the occaisional cold.";
                }
            }
            else if (Weather <= 82)
            {
                pic = "HvyRain";
                WindSpd = (int)(11 * random.NextDouble() + 5);
                    MinTmp = 73;
                    MaxTmp = 10;
                CurTmp = (int)(MaxTmp * random.NextDouble() + MinTmp);
                DialogTxt.Text = DialogTxt.Text + "...'Morning Coach. My clipboard is soaked! It's coming down in buckets out there. Current Temperature is " + CurTmp + ". It's a monsoon, but there're no reports of lightening so we could practice outside if you so choose. Doing so will benefit nearly every position as they'll have to maintain a sharp focus in the fierce conditions. It'll toughen these patsies up some too. Just be prepared for a guy or two catching a head cold.";
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


        private void ReloadMeeting()
        {

            if (tcform.Stage == "Hell Week")
            {
                TeamDrillSldr.Enabled = false;
                FilmStudySldr.Enabled = false;
                SpecialTeamsSldr.Enabled = false;
                DownTimeSldr.Enabled = false;
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
            StreamWriter sw;
            int Con = 0; int PosDrill = 0; int Team = 0; int Film = 0; int Special = 0; int Down = 0;
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam);
            }

            StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\coachsliders");
            string line = sr.ReadLine();
            string[] CoachSliders = line.Split(',');
            Con = int.Parse(CoachSliders[0]);
            PosDrill = int.Parse(CoachSliders[1]);
            Team = int.Parse(CoachSliders[2]);
            Film = int.Parse(CoachSliders[3]);
            Special = int.Parse(CoachSliders[4]);
            Down = int.Parse(CoachSliders[5]);

            int teamId = ((TeamRecord)tcform.tn).TeamId;
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
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos);
                FileStream file = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\" + Pos + "\\" + valObject.FirstName + " " + valObject.LastName);
                sw = new StreamWriter(file);
                sw.Write(Con + "," + PosDrill + "," + Team + "," + Con + "," + Film + "," + Special + "," + Down);
                sw.WriteLine();
                sw.Write(valObject.FirstName + " " + valObject.LastName);
                sw.Write(",");
                sw.Write(valObject.Weight + 160);
                sw.Write(",");
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
                    if (IndoorsRadioButton.Checked == true)
                    {
                        sw.Write("indoors");
                    }
                    else if (OutdoorsRadioButton.Checked == true)
                    {
                        sw.Write("outdoors");
                    }
                    sw.Close();
                    StreamWriter sw1 = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\" + tcform.franchiseFilename + "\\" + tcform.CurTeam + "\\System\\system");
                    if ((tcform.Stage == "Hell Week") & (tcform.CurDay <= 6))
                    {
                        tcform.CurDay++;
                        sw1.Write(tcform.CurTeam + "," + "Hell Week" + "," + tcform.CurDay);
                    }

                    sw1.Close();

                    OutputRoster();
                    this.Close();
                    TrainingCampForm tc = new TrainingCampForm(model);

                    //  tc.Show();


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