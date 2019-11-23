using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
    public partial class GameOptions2019 : UserControl, IEditorForm
    {
        private EditorModel model = null;
        public bool isInitializing = true;
        private MGMT _manager;

        public MGMT manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public GameOptions2019()
        {
            InitializeComponent();
        }

        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        public void InitialiseUI()
        {
            isInitializing = true;
            OffPlaybook.Items.Clear();
            foreach (GenericRecord rec in model.TeamModel.OffensivePlaybookList)
                OffPlaybook.Items.Add(rec);
            OffPlaybook.Items.Add("Team Specific");
            foreach (GenericRecord rec in model.TeamModel.DefensivePlaybookList)
                DEFPlaybook.Items.Add(rec);
            DEFPlaybook.Items.Add("Team Specific");
            Init();

            isInitializing = false;
        }

        public void CleanUI()
        {
            isInitializing = true;

            isInitializing = false;
        }
        
        #endregion

        public void Init()
        {
            isInitializing = true;

            CUR_Version_Textbox.Text = model.MadVersion.ToString();
            CUR_GameMode_Combo.SelectedIndex = model.GameOptionModel.GameStyle;
            CUR_Skill_Combo.SelectedIndex = model.UserOptionModel.SkillLevel;
            if (model.UserInfo.PlaybookOFF == 255)
                OffPlaybook.Text = "Team Specific";
            else OffPlaybook.Text = model.TeamModel.GetOFFPlaybook(model.UserInfo.PlaybookOFF);
            if (model.UserInfo.PlaybookDEF == 255)
                DEFPlaybook.Text = "Team Specific";
            else DEFPlaybook.Text = model.TeamModel.GetDEFPlaybook(model.UserInfo.PlaybookDEF);            
            
            CUR_Qtr_Updown.Value = model.GameOptionModel.QuarterLength;            
            CUR_EvenTeams.Checked = model.GameOptionModel.EvenTeams;
            CUR_PlayClock.Checked = model.GameOptionModel.PlayClock;
            AutoFlipDefPlay.Checked = model.UserOptionModel.AutoFlipDefPlay;
            GameSpeed.SelectedIndex = model.GameOptionModel.GameSpeed;
            BCSpecMove.SelectedIndex = model.UserOptionModel.BSSpecialMove;
            DEFAutoStrafe.Checked = model.UserOptionModel.DefAutoStrafe;
            DefBallHawk.Checked = model.UserOptionModel.BallHawk;
            DefHeatSeek.Checked = model.UserOptionModel.HeatSeek;
            SwitchAssist.Checked = model.UserOptionModel.SwitchAssist;
            CoachMode.Checked = model.UserOptionModel.CoachMode;
            SpeedParity.Value = model.GameOptionModel.SpeedParity;
            AccelClock.Checked = model.GameOptionModel.AccelClockOnOff;
            CUR_InjuryFreq.Value = model.GameOptionModel.Injuries;
            Fatigue.Value = model.GameOptionModel.Fatigue;

            if (model.GameOptionModel.AccelClockOnOff)
            {
                MinPlayClock.Enabled = true;
                MinPlayClock.Value = model.GameOptionModel.AcceleratedClock;
            }
            else
            {
                MinPlayClock.Enabled = false;
                MinPlayClock.Value = 25;
            }

            CoinTossFirst.SelectedIndex = model.UserOptionModel.CoinFlipFirst;
            SetCoinFlipOptions(model.UserOptionModel.CoinFlipFirst);

            // Visual Feedback
            PlayCallStyle.SelectedIndex = model.UserOptionModel.PlayCallStyle;

            // Graphics



            #region CPU Sliders
            CUR_CPU_QBAcc.Value = model.GameOptionModel.CPU_QB_Accuracy;            
            CUR_CPU_PassBlock.Value = model.GameOptionModel.CPU_PassBlocking_19;            
            CUR_CPU_WRCatch.Value = model.GameOptionModel.CPU_WR_Catching;
            CUR_CPU_RunBlock.Value = model.GameOptionModel.CPU_RunBlocking;
            CUR_CPU_Fumbles.Value = model.GameOptionModel.CPU_Fumbles;
            CUR_CPU_Reaction.Value = model.GameOptionModel.CPU_Reaction;
            CUR_CPU_Ints.Value = model.GameOptionModel.CPU_Def_Interceptions;            
            CUR_CPU_Knockdown.Value = model.GameOptionModel.CPU_Def_Knockdowns;            
            CUR_CPU_Tackle.Value = model.GameOptionModel.CPU_Tackling;
            CUR_CPU_FGLength.Value = model.GameOptionModel.CPU_FG_Length;
            CUR_CPU_FGAcc.Value = model.GameOptionModel.CPU_FG_Accuracy;
            CUR_CPU_PuntLength.Value = model.GameOptionModel.CPU_Punt_Length;
            CUR_CPU_PuntAcc.Value = model.GameOptionModel.CPU_Punt_Accuracy;
            CUR_CPU_Kickoff.Value = model.GameOptionModel.CPU_KO_Length;
            #endregion

            #region Human Sliders
            CUR_HUM_QBAcc.Value = model.GameOptionModel.HUM_QB_Accuracy;
            CUR_HUM_PassBlock.Value = model.GameOptionModel.HUM_PassBlocking;
            CUR_HUM_WRCatch.Value = model.GameOptionModel.HUM_WR_Catching;
            CUR_HUM_RunBlock.Value = model.GameOptionModel.HUM_RunBlocking;
            CUR_HUM_Fumbles.Value = model.GameOptionModel.HUM_Fumbles;
            CUR_HUM_Reaction.Value = model.GameOptionModel.HUM_Reaction;
            CUR_HUM_Ints.Value = model.GameOptionModel.HUM_Def_Interceptions;
            CUR_HUM_Knockdown.Value = model.GameOptionModel.HUM_Def_Knockdowns;
            CUR_HUM_Tackle.Value = model.GameOptionModel.HUM_Tackling;
            CUR_HUM_FGLength.Value = model.GameOptionModel.HUM_FG_Length;
            CUR_HUM_FGAcc.Value = model.GameOptionModel.HUM_FG_Accuracy;
            CUR_HUM_PuntLength.Value = model.GameOptionModel.HUM_Punt_Length;
            CUR_HUM_PuntAcc.Value = model.GameOptionModel.HUM_Punt_Accuracy;
            CUR_HUM_Kickoff.Value = model.GameOptionModel.HUM_KO_Length;
            #endregion

            #region Penalties            
            CUR_Offside.Value = model.GameOptionModel.Pen_Offside;            
            CUR_FalseStart.Value = model.GameOptionModel.Pen_FalseStart;            
            CUR_Holding.Value = model.GameOptionModel.Pen_Holding;
            CUR_DefHolding.Value = model.GameOptionModel.Pen_DEFHolding;
            CUR_Facemask.Value = model.GameOptionModel.Pen_FaceMask;            
            CUR_Clipping.Value = model.GameOptionModel.Pen_Clipping;
            CUR_RoughPasser.Value = model.GameOptionModel.Pen_RoughPasser;
            CUR_DefPassInt.Value = model.GameOptionModel.Pen_DefPassInterference;
            CUR_OffPassInt.Checked = model.GameOptionModel.Pen_OffPassInterference==1;
            CUR_CatchInt.Checked = model.GameOptionModel.Pen_CatchInterference==1;
            CUR_Grounding.Checked = model.GameOptionModel.Pen_Grounding == 1;  
            CUR_RoughKicker.Checked = model.GameOptionModel.Pen_RoughKicker==1;
            CUR_RunIntoKicker.Checked = model.GameOptionModel.Pen_RunIntoKicker;            
            #endregion


        }

        public void SetCoinFlipOptions(int choice)
        {
            isInitializing = true;
            CoinTossSecond.Items.Clear();
            if (choice < 2)
            {
                CoinTossSecond.Items.Add("With Wind");
                CoinTossSecond.Items.Add("Against Wind");
            }
            else
            {
                CoinTossSecond.Items.Add("Receive");
                CoinTossSecond.Items.Add("Kick");
            }
            isInitializing = false;

            CoinTossSecond.SelectedIndex = 0;            
        }
        
        private void CUR_GameMode_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.GameStyle = CUR_GameMode_Combo.SelectedIndex;
        }

        private void CoinTossFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no change
            if (CoinTossFirst.SelectedIndex == model.UserOptionModel.CoinFlipFirst)
                return;

            if (!isInitializing)
            {                
                model.UserOptionModel.CoinFlipFirst = CoinTossFirst.SelectedIndex;
                SetCoinFlipOptions(model.UserOptionModel.CoinFlipFirst);
            }

        }

        private void CoinTossSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.UserOptionModel.CoinFlipSecondary = CoinTossSecond.SelectedIndex;
            }
        }
            
    }
}
         

    

