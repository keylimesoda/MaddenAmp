using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MaddenEditor.Forms
{
    public partial class TrainingCampFormOptions : Form
    {
        string installDirectory = Application.StartupPath;
        public TrainingCampFormOptions()
        {
            InitializeComponent();

        }
       
        public void SaveSliders()
        {
            StreamWriter sw;
            
            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp");

            }
            if (!File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt")) // Create the Slider Profile if not present.
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt");
                sw = new StreamWriter(file);
            }
            else
            {
                sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt", false);
            }
            sw.WriteLine("AgeDeclination\t" + ageDeclineUpDown.Value);
          
            sw.Close();
        }

        private void ageDeclineUpDown_ValueChanged(object sender, EventArgs e)
        {
            ageDeclineSld.Value = (int)ageDeclineUpDown.Value;
      
        }

        private void ageDeclineSld_ValueChanged(object sender, EventArgs e)
        {
            ageDeclineUpDown.Value = ageDeclineSld.Value;
 
        }

        private void TrainingCampFormOptions_Load(object sender, EventArgs e)
        {
            StreamWriter sw;

            if (!Directory.Exists(installDirectory + "\\Conditioning\\TrainingCamp"))
            {
                Directory.CreateDirectory(installDirectory + "\\Conditioning\\TrainingCamp");              

            }

            if (!File.Exists(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt")) // Create the Slider Profile if not present.
            {
                FileStream file = File.Create(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt");
                sw = new StreamWriter(file);
                sw.Close();
                sw = new StreamWriter(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt", false);
                sw.WriteLine("AgeDeclination\t100");

                sw.Close();
            }
             

              StreamReader sr = new StreamReader(installDirectory + "\\Conditioning\\TrainingCamp\\Options.txt");

              while (!sr.EndOfStream)
              {
                  string line = sr.ReadLine();
                  string[] splitLine = line.Split('\t');

                  if (splitLine[0] == "AgeDeclination")
                  {
                      ageDeclineUpDown.Value = int.Parse(splitLine[1]);
                      ageDeclineSld.Value = int.Parse(splitLine[1]); 
                  }
              }
              sr.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSliders();
        }

       
     
    }
}