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

            isTrue = false;
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
            
         //   Splashtxt.SelectionStart = Splashtxt.Text.Length;
         //   Splashtxt.SelectionLength = 0;
         //   Splashtxt.ScrollToCaret();
            string Quote = "";
            int quoteRoller = (int)(36 * random.NextDouble() + 1);
            if (quoteRoller == 1)
            { Quote = "There's only one way to succeed in anything, and that is to give it everything. I do, and I demand that my players do."; }
            else if (quoteRoller == 2)
            { Quote = "If you aren't fired with enthusiasm, you'll be fired with enthusiasm."; }
             else if (quoteRoller == 3)
            { Quote = "Mental toughness is essential to success."; }
             else if (quoteRoller == 4)
            { Quote = "You never win a game unless you beat the guy in front of you. The score on the board doesn't mean a thing. That's for the fans. You've got to win the war with the man in front of you. You've got to get your man."; }
            else if (quoteRoller == 5)
            { Quote = "To achieve success, whatever the job we have, we must pay a price."; }
            else if (quoteRoller == 6)
            { Quote = "Success is like anything worthwhile. It has a price. You have to pay the price to win and you have to pay the price to get to the point where success is possible. Most important, you must pay the price to stay there"; }
            else if (quoteRoller == 7)
            { Quote = "Football is a great deal like life in that it teaches that work, sacrifice, perseverance, competitive drive, selflessness and respect for authority is the price that each and every one of us must pay to achieve any goal that is worthwhile."; }
            else if (quoteRoller == 8)
            { Quote = "Once you agree upon the price you and your family must pay for success, it enables you to ignore the minor hurts, the opponent's pressure, and the temporary failures."; }
            else if (quoteRoller == 9)
            { Quote = "Confidence is contagious and so is lack of confidence, and a customer will recognize both."; }
            else if (quoteRoller == 10)
            { Quote = "If you believe in yourself and have the courage, the determination, the dedication, the competitive drive and if you are willing to sacrifice the little things in life and pay the price for the things that are worthwhile, it can be done."; }
            else if (quoteRoller == 11)
            { Quote = "Unless a man believes in himself and makes a total commitment to his career and puts everything he has into it-his mind, his body and his heart-what is life worth to him? If I were a salesman, I would make this commitment to my company, to the product and most of all, to myself."; }
            else if (quoteRoller == 12)
            { Quote = "The quality of a person's life is in direct proportion to their commitment to excellence, regardless of their chosen field of endeavor."; }
            else if (quoteRoller == 13)
            { Quote = "Once a man has made a commitment to a way of life, he puts the greatest strength in the world behind him. It's something we call heart power. Once a man has made this commitment, nothing will stop him short of success."; }
            else if (quoteRoller == 14)
            { Quote = "Unless a man believes in himself and makes a total commitment to his career and puts everything he has into it-his mind, his body, his heart-what's life worth to him?"; }
            else if (quoteRoller == 15)
            { Quote = "It is essential to understand that battles are primarily won in the hearts of men."; }
            else if (quoteRoller == 16)
            { Quote = "In great attempts, it is glorious even to fail."; }
            else if (quoteRoller == 17)
            { Quote = "They may not love you at the time, but they will later."; }
            else if (quoteRoller == 18)
            { Quote = "Leadership rests not only upon ability, not only upon capacity; having the capacity to lead is not enough. The leader must be willing to use it. His leadership is then based on truth and character. There must be truth in the purpose and will power in the character."; }
            else if (quoteRoller == 19)
            { Quote = "Leadership is based on a spiritual quality; the power to inspire, the power to inspire others to follow."; }
            else if (quoteRoller == 20)
            { Quote = "Having the capacity to lead is not enough. The leader must be willing to use it."; }
            else if (quoteRoller == 21)
            { Quote = "A leader must identify himself with the group, must back up the group, even at the risk of displeasing superiors. He must believe that the group wants from him a sense of approval. If this feeling prevails, production, discipline, morale will be high, and in return, you can demand the cooperation to promote the goals of the company."; }
            else if (quoteRoller == 22)
            { Quote = "Leaders are made, they are not born. They are made by hard effort, which is the price which all of us must pay to achieve any goal that is worthwhile."; }
            else if (quoteRoller == 23)
            { Quote = "They call it coaching but it is teaching. You do not just tell them...you show them the reasons."; }
            else if (quoteRoller == 24)
            { Quote = "The harder you work, the harder it is to surrender."; }
            else if (quoteRoller == 25)
            { Quote = "The difference between a successful person and others is not a lack of strength, not a lack of knowledge, but rather in a lack of will."; }
            else if (quoteRoller == 26)
            { Quote = "The spirit, the will to win and the will to excel-these are the things that endure and these are the qualities that are so much more important than any of the events that occasion them."; }
            else if (quoteRoller == 27)
            { Quote = "It is essential to understand that battles are primarily won in the hearts of men. Men respond to leadership in a most remarkable way and once you have won his heart, he will follow you anywhere."; }
            else if (quoteRoller == 28)
            { Quote = "A man can be as great as he wants to be. If you believe in yourself and have the courage, the determination, the dedication, the competitive drive and if you are willing to sacrifice the little things in life and pay the price for the things that are worthwhile, it can be done."; }
            else if (quoteRoller == 29)
            { Quote = "If you'll not settle for anything less than your best, you will be amazed at what you can accomplish in your lives."; }
            else if (quoteRoller == 30)
            { Quote = "It's not whether you get knocked down, it's whether you get up."; }
            else if (quoteRoller == 31)
            { Quote = "I've never known a man worth his salt who in the long run, deep down in his heart, didn't appreciate the grind, the discipline. There is something good in men that really yearns for discipline."; }
            else if (quoteRoller == 32)
            { Quote = "The good Lord gave you a body that can stand most anything. It's your mind you have to convince"; }
            else if (quoteRoller == 33)
            { Quote = "Mental toughness is many things and rather difficult to explain. Its qualities are sacrifice and self-denial. Also, most importantly, it is combined with a perfectly disciplined will that refuses to give in. It's a state of mind-you could call it character in action."; }
            else if (quoteRoller == 34)
            { Quote = "Once you learn to quit, it becomes a habit."; }
            else if (quoteRoller == 35)
            { Quote = "Perfection is not attainable. But if we chase perfection, we can catch excellence."; }
            else if (quoteRoller == 36)
            { Quote = "Individual commitment to a group effort-that is what makes a team work, a company work, a society work, a civilization work."; }

            string Phrase = "";
            if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 1))
            {
                Phrase = (",.,.,.,L,o,c,a,t,i,o,n,\r\n                                 ,.,.,.," + tcform.CurTeam + ", T,r,a,i,n,i,n,g, C,a,m,p, F,a,c,i,l,i,t,y,\r\n                                 ,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
                
            }

            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 2))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,O,n,l,y, t,h,e, S,t,r,o,n,g, S,u,r,v,i,v,e\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}
            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 3))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,I,t, h,u,r,t,s, m,o,m,m,y,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}
            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 4))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,C,r,o,s,s, R,o,a,d,s,;, 1,/,2, w,a,y,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}
            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 5))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,D,o,g, D,a,y,s,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}
            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 6))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,L,i,g,h,t, a,t, t,h,e, e,n,d, o,f, t,h,e, t,u,n,n,e,l,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}
            //else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 7))
            //{
            //    Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,F,i,n,a,l, P,u,s,h,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            //}

            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 2))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,O,n,l,y, t,h,e, s,t,r,o,n,g, S,u,r,v,i,v,e\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 3))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,I,t, h,u,r,t,s, m,o,m,m,y,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 4))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,H,e,l,l, W,e,e,k, C,r,o,s,s, R,o,a,d,s,;, 1,/,2, w,a,y,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 5))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,D,o,g, D,a,y,s,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 6))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,L,i,g,h,t, a,t, t,h,e, e,n,d, o,f, t,h,e, t,u,n,n,e,l,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 7))
            {
                Phrase = (",,.,.,.,H,e,l,l, W,e,e,k,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,H,e,l,l, W,e,e,k, F,i,n,a,l, P,u,s,h,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 8))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,D,r,e,a,m,s, f,u,l,l,f,i,l,l,e,d, & D,r,e,a,m,s, t,r,a,m,p,l,e,d,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 9))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                                 .,.,.,A,p,t, p,u,p,i,l,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 10))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n                    .,.,.,P,l,a,y, l,i,k,e, y,o,u, j,u,s,t, h,i,t, y,o,u,r, m,o,t,h,e,r, w,i,t,h, a 2x4,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 11))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n    .,.,.,B,a,s,e,b,a,l,l, i,s, w,h,a,t, w,e, w,e,r,e, f,o,o,t,b,a,l,l, i,s, w,h,a,t, w,e, h,a,v,e, b,e,c,o,m,e,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 12))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n  .,.,.,I'd, c,a,t,c,h, a p,u,n,t, n,a,k,e,d, i,n, t,h,e, s,n,o,w, i,n,\r\n  .,.,.,B,u,f,f,a,l,o, f,o,r, a c,h,a,n,c,e, t,o, p,l,a,y, i,n, t,h,e, N,F,L,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 13))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n               .,.,.,I'd, r,u,n, o,v,e,r, m,y, o,w,n, m,o,t,h,e,r, t,o, w,i,n, t,h,e, S,u,p,e,r, B,o,w,l,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
            else if ((tcform.Stage == "Training Camp") & (tcform.CurDay == 14))
            {
                Phrase = (",,.,.,.,Training Camp,.,.,.,D,a,y, " + tcform.CurDay + ",\r\n         .,.,.,P,l,a,y,i,n,g, f,o,o,t,b,a,l,l, i,n, t,h,e, m,o,r,n,i,n,g, i,s,\r\n         .,.,.,l,i,k,e, e,a,t,i,n,g, c,a,b,b,a,g,e, f,o,r, b,r,e,a,k,f,a,s,t,\r\n                                 ,.,.,.,5,:," + xx + ",:,23, A,M");
            }
           

            Splashtxt.Text = Splashtxt.Text + Quote + "\r\n\r\n               -Vince Lombardi\r\n\r\n\r\n\r\n                                 ";
            
            Timer1TextDelay(25);
            while (isTrue == true)
            {
                Application.DoEvents();
            }


            string[] splitLine = Phrase.Split(',');
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
         //   if ((tcform.Stage == "Hell Week") & (tcform.CurDay == 1))
         //   {
                
                Timer1TextDelay(15);
                this.Close();
                while (isTrue == true)
                {
                    Application.DoEvents();
                }
          //  }
           
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

        private void TrainingCampSplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            TrainingCampMeeting form = new TrainingCampMeeting(model, tcform);
            form.Show();
        }

      
        

       
             


    }
}