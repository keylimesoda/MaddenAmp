using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
    public partial class TrainingCampMeeting : Form
    {
        Random random = new Random();


        public TrainingCampMeeting()
        {
            InitializeComponent();
        }

        private void WeatherGenerator()
        {
            int CurTmp = 88;

            DialogTxt.Text = "...'Morning Coach. Looks we got lots of sun today.\r\nCurrent Temperature is " + CurTmp + ".";
            //  CurWeatherPic.Image = ;

            int Weather = (int)(7 * random.NextDouble() + 1);
            string pic = "";

            if (Weather == 1)
            {
                pic = "sunny";
            }
            else if (Weather == 2)
            {
                pic = "cloudy";
            }
            else if (Weather == 3)
            {
                pic = "FairWindy";
            }
            else if (Weather == 4)
            {
                pic = "HvyRain";
            }
            else if (Weather == 5)
            {
                pic = "PartlyCloudy";
            }
            else if (Weather == 6)
            {
                pic = "scattered_Tstorms";
            }
            else if (Weather == 7)
            {
                pic = "Showers";
            }
            else if (Weather == 8)
            {
                pic = "thunder";
            }


            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream file =
                thisExe.GetManifestResourceStream("MaddenEditor.Resources." + pic + ".JPG");
            this.CurWeatherPic.Image = Image.FromStream(file);

        }


        private void ReloadMeeting()
        {

        //    if (Stage == "HellWeek")
        //    {
                ConditioningSldr.Minimum = 25;
                ConditioningSldr.Maximum = 75;
                PositionDrillSldr.Minimum = 25;
                PositionDrillSldr.Maximum = 75;
                TeamDrillSldr.Visible = false;
                FilmStudySldr.Visible = false;
                SpecialTeamsSldr.Visible = false;
                DownTimeSldr.Visible = false;
       //     }





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





    }
}