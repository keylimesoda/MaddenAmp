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
    public partial class TrainingCampSplashScreen : Form
    {
        public TrainingCampForm tcform;
        private EditorModel model = null;
        private bool m_fadeInFlag = false;
        private bool isTrue = false;
        private int Counter = 0;
        private int Sleep = 0;
        Random random = new Random();

        public TrainingCampSplashScreen(EditorModel model, TrainingCampForm trainingForm)
        {
            tcform = trainingForm;
            tcform.trainingCampSplashScreen = this;
            this.model = model;
            InitializeComponent();            
        }

        protected override void OnLoad(EventArgs e)
        {           

            base.OnLoad(e);

            // Should we start fading?
            if (!DesignMode)
            {
                m_fadeInFlag = true;
                Opacity = 0;

                m_fadeInOutTimer.Enabled = true;

            } // End if we should start the fading process.

        } // End OnLoad()

        private void m_fadeInOutTimer_Tick(object sender, System.EventArgs e)
        {
            // How should we fade?
            if (m_fadeInFlag == false)
            {
                Opacity -= (m_fadeInOutTimer.Interval / 1000.0);

                // Should we continue to fade?
                if (this.Opacity > 0)
                    m_fadeInOutTimer.Enabled = true;
                else
                {
                    m_fadeInOutTimer.Enabled = false;
                    Close();
                } // End else we should close the form.

            } // End if we should fade in.
            else
            {
                Opacity += (m_fadeInOutTimer.Interval / 1000.0);
                m_fadeInOutTimer.Enabled = (Opacity < 1.0);
                m_fadeInFlag = (Opacity < 1.0);
                if (Opacity == 1.0)
                {
                    TextWriter();

                }

            } // End else we should fade out.

        } // End m_fadeInOutTimer_Tick()


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            // If the user canceled then don't fade anything.
            if (e.Cancel == true)
                return;

            // Should we fade instead of closing?
            if (Opacity > 0)
            {
                m_fadeInFlag = false;
                m_fadeInOutTimer.Enabled = true;
                e.Cancel = true;
            } // End if we should fade instead of closing.

        }

       

        
       
        
        private void Timer1TextDelay(int SleepValue)
        {
            Counter = 0;
            Sleep = SleepValue;
            m_SplashTextTimer.Enabled = true;
            isTrue = true;
        }
        private void TextWriter()
        {
            //int X = 1;
            int xx = (int)(49 * random.NextDouble() + 10);
            Splashtxt.Text = Splashtxt.Text + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n                                 ";
            Splashtxt.SelectionStart = Splashtxt.Text.Length;
            Splashtxt.SelectionLength = 0;
            Splashtxt.ScrollToCaret();

            string HellWeek = (",.,.,.,L,o,c,a,t,i,o,n,\r\n                                 ,.,.,.," + tcform.CurTeam + ", T,r,a,i,n,i,n,g, C,a,m,p, F,a,c,i,l,i,t,y,\r\n                                 ,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            string[] splitLine = HellWeek.Split(',');
            int Len = splitLine.Length;
 
            for (int a = 0; a < Len; a++)
            {
                Timer1TextDelay(1);
                Splashtxt.Text = Splashtxt.Text + splitLine[a];
                while (isTrue == true)
                {
                    Application.DoEvents();
                }

            }


            Timer1TextDelay(15);
            Splashtxt.Text = Splashtxt.Text;
            while (isTrue == true)
            {
                Application.DoEvents();
            }
            if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 1))
            {
                
                Timer1TextDelay(10);
                this.Close();
                while (isTrue == true)
                {
                    Application.DoEvents();
                }
                TrainingCampMeeting form = new TrainingCampMeeting(model, tcform);
                form.Show();

            }
            else
            {
                this.Close();
            }
        }

        private void m_SplashTextTimer_Tick(object sender, EventArgs e)
        {
            Counter++;

            if (Counter == Sleep)
            {
                isTrue = false;
                m_SplashTextTimer.Enabled = false;
            }

        }
        

       
             


    }
}