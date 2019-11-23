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

namespace MaddenEditor.Forms
{
    public partial class GameOptions : UserControl, IEditorForm
    {
        private BinaryReader binreader;
        private BinaryWriter binwriter;
        private EditorModel model = null;
        private List<SliderSet> _amp_sliders;

        public bool isInitializing = true;
        public UInt32 Header = 1346720339;
        public int entries = 0;
        public int currentset = 0;
        public bool search = false;

        public List<SliderSet> Amp_Sliders
        {
            get { return _amp_sliders; }
            set { _amp_sliders = value; }
        }

        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        public void InitialiseUI()
        {
            
        }

        public void CleanUI()
        {
            isInitializing = true;

            REPO_SliderName.Text = "";
            REPO_Version_Combo.SelectedIndex = -1;
            REPO_Mode_Combo.SelectedIndex = -1;
            REPO_Skill_Combo.SelectedIndex = -1;
            REPO_QtrLength.Value = 1;
            REPO_AccelClock.Checked = false;

            REPO_CPU_QBAcc.Value = 0;
            REPO_CPU_Passblock.Value = 0;
            REPO_CPU_WRCatch.Value = 0;
            REPO_CPU_RBAbility.Value = 0;
            REPO_CPU_Runblock.Value = 0;
            REPO_CPU_Aware.Value = 0;
            REPO_CPU_Knockdown.Value = 0;
            REPO_CPU_Int.Value = 0;
            REPO_CPU_Breakblock.Value = 0;
            REPO_CPU_Tackle.Value = 0;
            REPO_CPU_FGLength.Value = 0;
            REPO_CPU_FGAcc.Value = 0;
            REPO_CPU_PuntLength.Value = 0;
            REPO_CPU_PuntAcc.Value = 0;
            REPO_CPU_KOLength.Value = 0;

            REPO_HUM_QBAcc.Value = 0;
            REPO_HUM_Passblock.Value = 0;
            REPO_HUM_WRcatch.Value = 0;
            REPO_HUM_RBAbility.Value = 0;
            REPO_HUM_Runblock.Value = 0;
            REPO_HUM_Aware.Value = 0;
            REPO_HUM_Knockdown.Value = 0;
            REPO_HUM_Int.Value = 0;
            REPO_HUM_Breakblock.Value = 0;
            REPO_HUM_Tackle.Value = 0;
            REPO_HUM_FGLength.Value = 0;
            REPO_HUM_FGAcc.Value = 0;
            REPO_HUM_PuntLength.Value = 0;
            REPO_HUM_PuntAcc.Value = 0;
            REPO_HUM_KOLength.Value = 0;

            REPO_OffsidesON.Enabled = false;
            REPO_OffsidesON.Checked = false;
            REPO_FalseStart.Enabled = false;
            REPO_FalseStart.Value = 0;
            REPO_Holding.Enabled = false;
            REPO_Holding.Value = 0;
            REPO_Facemask.Enabled = false;
            REPO_Facemask.Value = 0;
            REPO_Clipping.Enabled = false;
            REPO_Clipping.Value = 0;
            REPO_Grounding.Enabled = false;
            REPO_Grounding.Value = 0;
            REPO_RoughKicker.Enabled = false;
            REPO_RoughKicker.Value = 0;
            REPO_RoughPasser.Enabled = false;
            REPO_RoughPasser.Value = 0;
            REPO_OffPassInt.Enabled = false;
            REPO_OffPassInt.Value = 0;
            REPO_DefPassInt.Enabled = false;
            REPO_DefPassInt.Value = 0;
            REPO_CatchInt.Enabled = false;
            REPO_CatchInt.Value = 0;
            REPO_Ingame_InjuriesON.Checked = false;
            REPO_IngameInjuries_Freq.Enabled = false;
            REPO_IngameInjuries_Freq.Value = 0;
            REPO_SimInjury_Freq.Enabled = false;
            REPO_SimInjury_Freq.Value = 0;
            REPO_SimInjury_Freq.Value = 0;            
            REPO_PassLead.Value = 0;
            REPO_SubIn.Value = 0;
            REPO_SubOut.Value = 0;

            isInitializing = false;
        }
                
        

        #endregion
       
        
        public GameOptions()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Amp_Sliders = new List<SliderSet>();
            InitializeUI();
            InitCurrentOptions();            
        }

        public void InitializeUI()
        {
            isInitializing = true;
            #region PlayMode
            CUR_GameMode_Combo.Items.Add("Classic");
            REPO_Mode_Combo.Items.Add("Classic");
            REPO_Search_Mode_Combo.Items.Add("Classic");

            CUR_GameMode_Combo.Items.Add("Player");
            REPO_Mode_Combo.Items.Add("Player");
            REPO_Search_Mode_Combo.Items.Add("Player");

            CUR_GameMode_Combo.Items.Add("Coach");
            REPO_Mode_Combo.Items.Add("Coach");
            REPO_Search_Mode_Combo.Items.Add("Coach");
            REPO_Search_Mode_Combo.Items.Add("ANY");

            #endregion
            #region Skill
            CUR_Skill_Combo.Items.Add("Rookie");
            REPO_Skill_Combo.Items.Add("Rookie");
            REPO_Search_Skill_Combo.Items.Add("Rookie");

            CUR_Skill_Combo.Items.Add("Pro");
            REPO_Skill_Combo.Items.Add("Pro");
            REPO_Search_Skill_Combo.Items.Add("Pro");

            CUR_Skill_Combo.Items.Add("All-Pro");
            REPO_Skill_Combo.Items.Add("All-Pro");
            REPO_Search_Skill_Combo.Items.Add("All-Pro");

            CUR_Skill_Combo.Items.Add("All Madden");
            REPO_Skill_Combo.Items.Add("All Madden");
            REPO_Search_Skill_Combo.Items.Add("All Madden");
            REPO_Search_Skill_Combo.Items.Add("ANY");

            #endregion
            #region Version
            REPO_Search_Version_Combo.Items.Add(MaddenFileVersion.Ver2004.ToString());
            REPO_Version_Combo.Items.Add(MaddenFileVersion.Ver2004.ToString());
            REPO_Search_Version_Combo.Items.Add(MaddenFileVersion.Ver2005.ToString());
            REPO_Version_Combo.Items.Add(MaddenFileVersion.Ver2005.ToString());
            REPO_Search_Version_Combo.Items.Add(MaddenFileVersion.Ver2006.ToString());
            REPO_Version_Combo.Items.Add(MaddenFileVersion.Ver2006.ToString());
            REPO_Search_Version_Combo.Items.Add(MaddenFileVersion.Ver2007.ToString());
            REPO_Version_Combo.Items.Add(MaddenFileVersion.Ver2007.ToString());
            REPO_Search_Version_Combo.Items.Add(MaddenFileVersion.Ver2008.ToString());
            REPO_Version_Combo.Items.Add(MaddenFileVersion.Ver2008.ToString());
            REPO_Search_Version_Combo.Items.Add("ANY");
                
            #endregion

            CUR_FieldLines.Items.Add("None");
            CUR_FieldLines.Items.Add("LOS");
            CUR_FieldLines.Items.Add("Down");
            CUR_FieldLines.Items.Add("Both");

            CUR_CameraView.Items.Add("Madden");
            CUR_CameraView.Items.Add("Classic");
            CUR_CameraView.Items.Add("Zoom");
            CUR_CameraView.Items.Add("Long");
            CUR_CameraView.Items.Add("Wide");

            REPO_Search_Mode_Combo.SelectedIndex = 3;
            REPO_Search_Skill_Combo.SelectedIndex = 4;
            REPO_Search_Version_Combo.SelectedIndex = 5;
        }

        public void InitCurrentOptions()
        {
            if (model.FileType != MaddenFileType.Franchise && model.FileType != MaddenFileType.UserConfig)
                return;
            if (model.MadVersion == MaddenFileVersion.Ver2019)
                panel1.Visible = true;

            isInitializing = true;

            CUR_Version_Textbox.Text = model.MadVersion.ToString();
            CUR_GameMode_Combo.SelectedIndex = model.GameOptionModel.Playmode;
            CUR_Skill_Combo.SelectedIndex = model.GameOptionModel.SkillLevel;
            CUR_Qtr_Updown.Value = model.GameOptionModel.QuarterLength;
            if (model.GameOptionModel.AcceleratedClock == 1)
                CUR_AcceClock_Checkbox.Checked = true;
            else CUR_AcceClock_Checkbox.Checked = false;
            #region CPU AI
            CUR_CPU_QBAcc.Value = model.GameOptionModel.CPU_QB_Accuracy;
            if (model.MadVersion == MaddenFileVersion.Ver2019)
                CUR_CPU_PassBlock.Value = model.GameOptionModel.CPU_PassBlocking_19;
            else CUR_CPU_PassBlock.Value = model.GameOptionModel.CPU_PassBlocking;
            
            CUR_CPU_WRCatch.Value = model.GameOptionModel.CPU_WR_Catching;
            CUR_CPU_RBAbility.Value = model.GameOptionModel.CPU_RB_Ability;
            CUR_CPU_RunBlock.Value = model.GameOptionModel.CPU_RunBlocking;
            CUR_CPU_Aware.Value = model.GameOptionModel.CPU_Def_Awareness;
            CUR_CPU_Knockdown.Value = model.GameOptionModel.CPU_Def_Knockdowns;
            CUR_CPU_Ints.Value = model.GameOptionModel.CPU_Def_Interceptions;
            CUR_CPU_BreakBlock.Value = model.GameOptionModel.CPU_Def_BreakBlock;
            CUR_CPU_Tackle.Value = model.GameOptionModel.CPU_Tackling;
            CUR_CPU_FGLength.Value = model.GameOptionModel.CPU_FG_Length;
            CUR_CPU_FGAcc.Value = model.GameOptionModel.CPU_FG_Accuracy;
            CUR_CPU_PuntLength.Value = model.GameOptionModel.CPU_Punt_Length;
            CUR_CPU_PuntAcc.Value = model.GameOptionModel.CPU_Punt_Accuracy;
            CUR_CPU_Kickoff.Value = model.GameOptionModel.CPU_KO_Length;
            #endregion

            #region Human AI
            CUR_HUM_QBAcc.Value = model.GameOptionModel.HUM_QB_Accuracy;
            CUR_HUM_PassBlock.Value = model.GameOptionModel.HUM_PassBlocking;
            CUR_HUM_WRCatch.Value = model.GameOptionModel.HUM_WR_Catching;
            CUR_HUM_RBAbility.Value = model.GameOptionModel.HUM_RB_Ability;
            CUR_HUM_RunBlock.Value = model.GameOptionModel.HUM_RunBlocking;
            CUR_HUM_Aware.Value = model.GameOptionModel.HUM_Def_Awareness;
            CUR_HUM_Knockdown.Value = model.GameOptionModel.HUM_Def_Knockdowns;
            CUR_HUM_Ints.Value = model.GameOptionModel.HUM_Def_Interceptions;
            CUR_HUM_BreakBlock.Value = model.GameOptionModel.HUM_Def_BreakBlock;
            CUR_HUM_Tackle.Value = model.GameOptionModel.HUM_Tackling;
            CUR_HUM_FGLength.Value = model.GameOptionModel.HUM_FG_Length;
            CUR_HUM_FGAcc.Value = model.GameOptionModel.HUM_FG_Accuracy;
            CUR_HUM_PuntLength.Value = model.GameOptionModel.HUM_Punt_Length;
            CUR_HUM_PuntAcc.Value = model.GameOptionModel.HUM_Punt_Accuracy;
            CUR_HUM_Kickoff.Value = model.GameOptionModel.HUM_KO_Length;
            #endregion

            CUR_InjuriesON.Checked = (model.GameOptionModel.Injuries == 1);
            if (model.GameOptionModel.Injuries==1)
            {
                CUR_InjuryFreq.Enabled = true;
                CUR_InjuryFreq.Value = model.GameOptionModel.InGameInjury;
            }
            else
            {
                CUR_InjuryFreq.Value = 0;
                CUR_InjuryFreq.Enabled = false;
            }

            CUR_SimInjury.Checked = model.GameOptionModel.SimInjuryON;
            if (model.GameOptionModel.SimInjuryON)
            {
                CUR_SimInjuryFreq.Enabled = true;
                CUR_SimInjuryFreq.Value = model.GameOptionModel.SimInjury_Freq;
            }
            else
            {
                CUR_SimInjuryFreq.Value = 0;
                CUR_SimInjuryFreq.Enabled = false;
            }

            #region Penalties
            CUR_PenaltiesON.Checked = model.GameOptionModel.Penalties;
            if (model.GameOptionModel.Penalties)
            {
                CUR_OffsidesON.Enabled = true;
                CUR_OffsidesON.Checked = model.GameOptionModel.Offsides;
                CUR_FalseStart.Enabled = true;
                CUR_FalseStart.Value = model.GameOptionModel.Pen_FalseStart;
                CUR_Holding.Enabled = true;
                CUR_Holding.Value = model.GameOptionModel.Pen_Holding;
                CUR_Facemask.Enabled = true;
                CUR_Facemask.Value = model.GameOptionModel.Pen_FaceMask;
                CUR_Clipping.Enabled = true;
                CUR_Clipping.Value = model.GameOptionModel.Pen_Clipping;
                CUR_Grounding.Enabled = true;
                CUR_Grounding.Value = model.GameOptionModel.Pen_Grounding;
                CUR_RoughPasser.Enabled = true;
                CUR_RoughPasser.Value = model.GameOptionModel.Pen_RoughPasser;
                CUR_RoughKicker.Enabled = true;
                CUR_RoughKicker.Value = model.GameOptionModel.Pen_RoughKicker;
                CUR_OffPassInt.Enabled = true;
                CUR_OffPassInt.Value = model.GameOptionModel.Pen_OffPassInterference;
                CUR_DefPassInt.Enabled = true;
                CUR_DefPassInt.Value = model.GameOptionModel.Pen_DefPassInterference;
                CUR_CatchInt.Enabled = true;
                CUR_CatchInt.Value = model.GameOptionModel.Pen_CatchInterference;
            }
            else
            {
                CUR_OffsidesON.Enabled = false;
                CUR_OffsidesON.Checked = false;
                CUR_FalseStart.Enabled = false;
                CUR_FalseStart.Value = 0;
                CUR_Holding.Enabled = false;
                CUR_Holding.Value = 0;
                CUR_Facemask.Enabled = false;
                CUR_Facemask.Value = 0;
                CUR_Clipping.Enabled = false;
                CUR_Clipping.Value = 0;
                CUR_Grounding.Enabled = false;
                CUR_Grounding.Value = 0;
                CUR_RoughPasser.Enabled = false;
                CUR_RoughPasser.Value = 0;
                CUR_RoughKicker.Enabled = false;
                CUR_RoughKicker.Value = 0;
                CUR_OffPassInt.Enabled = false;
                CUR_OffPassInt.Value = 0;
                CUR_DefPassInt.Enabled = false;
                CUR_DefPassInt.Value = 0;
                CUR_CatchInt.Enabled = false;
                CUR_CatchInt.Value = 0;
            }



            #endregion

            CUR_WeatherON.Checked = model.GameOptionModel.RandomWeather;
            if (model.GameOptionModel.RandomWeather)
            {
                CUR_WeatherEffects.Enabled = true;
                CUR_WeatherEffects.Value = model.GameOptionModel.WeatherEffects;
            }
            else
            {
                CUR_WeatherEffects.Enabled = false;
                CUR_WeatherEffects.Value = 0;
            }

            CUR_TradeDeadline.Checked = model.GameOptionModel.TradeDeadline;
            CUR_SalaryCap.Checked = model.GameOptionModel.SalaryCap;
            CUR_CameraView.SelectedIndex = model.GameOptionModel.CameraView;
            CUR_FieldLines.SelectedIndex = model.GameOptionModel.FieldLines;
            CUR_PlayClock.Checked = model.GameOptionModel.PlayClock;
            CUR_CapPenalty.Checked = model.GameOptionModel.SalaryCapPenalty;
            CUR_AutoReplay.Checked = model.GameOptionModel.AutoReplay;
            CUR_Fatigue.Checked = (model.GameOptionModel.Fatigue == 1);

            if (model.MadVersion >= MaddenFileVersion.Ver2006)
            {
                CUR_FairPlay.Enabled = true;
                CUR_FairPlay.Checked = model.GameOptionModel.FairPlay;
            }
            else
            {
                CUR_FairPlay.Enabled = false;
                CUR_FairPlay.Checked = false;
            }

            CUR_PassLead.Value = model.UserOptionModel.PassLead/10;
            CUR_SubIn.Value = model.UserOptionModel.AutoSubIn;
            CUR_SubOut.Value = model.UserOptionModel.AutoSubOut;

            isInitializing = false;
        }

        public void InitRepoSet(int number)
        {
            isInitializing = true;

            SliderSet set = Amp_Sliders[number];

            REPO_SliderName.Text = set.SliderName;
            REPO_Version_Combo.SelectedIndex = set.FileVersion;
            REPO_Mode_Combo.SelectedIndex = set.GameMode;
            REPO_Skill_Combo.SelectedIndex = set.Skill;
            REPO_QtrLength.Value = set.QtrLength;
            REPO_AccelClock.Checked = set.AccelClock;

            #region Slider AI
            #region CPU AI
            REPO_CPU_QBAcc.Value = set.CPU_QB_Accuracy;
            REPO_CPU_Passblock.Value = set.CPU_PassBlocking;
            REPO_CPU_WRCatch.Value = set.CPU_WR_Catching;
            REPO_CPU_RBAbility.Value = set.CPU_RB_Ability;
            REPO_CPU_Runblock.Value = set.CPU_RunBlocking;
            REPO_CPU_Aware.Value = set.CPU_Def_Awareness;
            REPO_CPU_Knockdown.Value = set.CPU_Def_Knockdowns;
            REPO_CPU_Int.Value = set.CPU_Def_Interceptions;
            REPO_CPU_Breakblock.Value = set.CPU_Def_BreakBlock;
            REPO_CPU_Tackle.Value = set.CPU_Tackling;
            REPO_CPU_FGLength.Value = set.CPU_FG_Length;
            REPO_CPU_FGAcc.Value = set.CPU_FG_Accuracy;
            REPO_CPU_PuntLength.Value = set.CPU_Punt_Length;
            REPO_CPU_PuntAcc.Value = set.CPU_Punt_Accuracy;
            REPO_CPU_KOLength.Value = set.CPU_KO_Length;
            #endregion

            #region Human AI
            REPO_HUM_QBAcc.Value = set.HUM_QB_Accuracy;
            REPO_HUM_Passblock.Value = set.HUM_PassBlocking;
            REPO_HUM_WRcatch.Value = set.HUM_WR_Catching;
            REPO_HUM_RBAbility.Value = set.HUM_RB_Ability;
            REPO_HUM_Runblock.Value = set.HUM_RunBlocking;
            REPO_HUM_Aware.Value = set.HUM_Def_Awareness;
            REPO_HUM_Knockdown.Value = set.HUM_Def_Knockdowns;
            REPO_HUM_Int.Value = set.HUM_Def_Interceptions;
            REPO_HUM_Breakblock.Value = set.HUM_Def_BreakBlock;
            REPO_HUM_Tackle.Value = set.HUM_Tackling;
            REPO_HUM_FGLength.Value = set.HUM_FG_Length;
            REPO_HUM_FGAcc.Value = set.HUM_FG_Accuracy;
            REPO_HUM_PuntLength.Value = set.HUM_Punt_Length;
            REPO_HUM_PuntAcc.Value = set.HUM_Punt_Accuracy;
            REPO_HUM_KOLength.Value = set.HUM_KO_Length;
            #endregion
            #endregion

            #region Penalties
            REPO_PenaltiesON.Checked = set.PenaltiesON;
            if (set.PenaltiesON)
            {
                REPO_OffsidesON.Enabled = true;
                REPO_OffsidesON.Checked = set.OffsidesON;
                REPO_FalseStart.Enabled = true;
                REPO_FalseStart.Value = set.FalseStart;
                REPO_Holding.Enabled = true;
                REPO_Holding.Value = set.Holding;
                REPO_Facemask.Enabled = true;
                REPO_Facemask.Value = set.faceMask;
                REPO_Clipping.Enabled = true;
                REPO_Clipping.Value = set.Clipping;
                REPO_Grounding.Enabled = true;
                REPO_Grounding.Value = set.Grounding;
                REPO_RoughKicker.Enabled = true;
                REPO_RoughKicker.Value = set.RoughKicker;
                REPO_RoughPasser.Enabled = true;
                REPO_RoughPasser.Value = set.RoughPasser;
                REPO_OffPassInt.Enabled = true;
                REPO_OffPassInt.Value = set.OFFPassInt;
                REPO_DefPassInt.Enabled = true;
                REPO_DefPassInt.Value = set.DEFPassInt;
                REPO_CatchInt.Enabled = true;
                REPO_CatchInt.Value = set.CatchInt;
            }
            else
            {
                REPO_OffsidesON.Enabled = false;
                REPO_OffsidesON.Checked = false;
                REPO_FalseStart.Enabled = false;
                REPO_FalseStart.Value = 0;
                REPO_Holding.Enabled = false;
                REPO_Holding.Value = 0;
                REPO_Facemask.Enabled = false;
                REPO_Facemask.Value = 0;
                REPO_Clipping.Enabled = false;
                REPO_Clipping.Value = 0;
                REPO_Grounding.Enabled = false;
                REPO_Grounding.Value = 0;
                REPO_RoughKicker.Enabled = false;
                REPO_RoughKicker.Value = 0;
                REPO_RoughPasser.Enabled = false;
                REPO_RoughPasser.Value = 0;
                REPO_OffPassInt.Enabled = false;
                REPO_OffPassInt.Value = 0;
                REPO_DefPassInt.Enabled = false;
                REPO_DefPassInt.Value = 0;
                REPO_CatchInt.Enabled = false;
                REPO_CatchInt.Value = 0;
            }
            #endregion

            #region Injuries

            REPO_Ingame_InjuriesON.Checked = set.InjuriesON;
            if (set.InjuriesON)
            {
                REPO_IngameInjuries_Freq.Enabled = true;
                if (set.FileVersion >= 2)
                {
                    REPO_IngameInjuries_Freq.Value = set.InjuryFrequency;
                    REPO_IngameInjuries_Freq.Enabled = true;
                }
                else
                {
                    if (set.FileVersion <= 1)  
                    {
                        set.SimInjuryON = false;   
                        set.InjuryFrequency = 0;
                    }
                    REPO_IngameInjuries_Freq.Enabled = false;
                    REPO_IngameInjuries_Freq.Value = set.InjuryFrequency;
                }
            }
            else
            {
                REPO_IngameInjuries_Freq.Enabled = false;
                REPO_IngameInjuries_Freq.Value = 0;
            }
           

            REPO_SimInjuryON.Checked = set.SimInjuryON;
            REPO_SimInjuryON.Enabled = set.SimInjuryON;

            if (set.SimInjuryON && set.FileVersion >=2)
            {
                REPO_SimInjury_Freq.Enabled = true;
                REPO_SimInjury_Freq.Value = set.SimInjuryFrequency;
            }
            else
            {
              REPO_SimInjury_Freq.Enabled = false;
              if (set.FileVersion <= 1)
              {
                  REPO_SimInjuryON.Enabled = false;
                  REPO_SimInjury_Freq.Enabled = false;
                  set.SimInjuryFrequency = 0;
                  set.SimInjuryON = false;
              }
              
              REPO_SimInjury_Freq.Value = set.SimInjuryFrequency;
            }
            #endregion

            REPO_PassLead.Value = set.PassLeadSensitivity;
            REPO_SubIn.Value = set.AutoSub_in;
            REPO_SubOut.Value = set.AutoSub_out;

            isInitializing = false;
        }
                
        public void SetSlidersFromRepo(SliderSet repo, bool AI, bool penalties, bool options, bool misc)
        {
            if (options)
            {
                model.GameOptionModel.Playmode = repo.GameMode;
                model.GameOptionModel.SkillLevel = repo.Skill;
                model.GameOptionModel.QuarterLength = repo.QtrLength;
                if (repo.AccelClock)
                    model.GameOptionModel.AcceleratedClock = 1;
                else model.GameOptionModel.AcceleratedClock = 0;

                model.UserOptionModel.PassLead = repo.PassLeadSensitivity*10;
                model.UserOptionModel.AutoSubIn = repo.AutoSub_in;
                model.UserOptionModel.AutoSubOut = repo.AutoSub_out;
            }
            if (AI)
            {
                #region CPU AI
                model.GameOptionModel.CPU_Def_Awareness = repo.CPU_Def_Awareness;
                model.GameOptionModel.CPU_Def_BreakBlock = repo.CPU_Def_BreakBlock;
                model.GameOptionModel.CPU_Def_Interceptions = repo.CPU_Def_Interceptions;
                model.GameOptionModel.CPU_Def_Knockdowns = repo.CPU_Def_Knockdowns;
                model.GameOptionModel.CPU_FG_Accuracy = repo.CPU_FG_Accuracy;
                model.GameOptionModel.CPU_FG_Length = repo.CPU_FG_Length;
                model.GameOptionModel.CPU_KO_Length = repo.CPU_KO_Length;
                model.GameOptionModel.CPU_PassBlocking = repo.CPU_PassBlocking;
                model.GameOptionModel.CPU_Punt_Accuracy = repo.CPU_Punt_Accuracy;
                model.GameOptionModel.CPU_Punt_Length = repo.CPU_Punt_Length;
                model.GameOptionModel.CPU_QB_Accuracy = repo.CPU_QB_Accuracy;
                model.GameOptionModel.CPU_RB_Ability = repo.CPU_RB_Ability;
                model.GameOptionModel.CPU_RunBlocking = repo.CPU_RunBlocking;
                model.GameOptionModel.CPU_Tackling = repo.CPU_Tackling;
                model.GameOptionModel.CPU_WR_Catching = repo.CPU_WR_Catching;
                #endregion
                #region Human AI
                model.GameOptionModel.HUM_Def_Awareness = repo.HUM_Def_Awareness;
                model.GameOptionModel.HUM_Def_BreakBlock = repo.HUM_Def_BreakBlock;
                model.GameOptionModel.HUM_Def_Interceptions = repo.HUM_Def_Interceptions;
                model.GameOptionModel.HUM_Def_Knockdowns = repo.HUM_Def_Knockdowns;
                model.GameOptionModel.HUM_FG_Accuracy = repo.HUM_FG_Accuracy;
                model.GameOptionModel.HUM_FG_Length = repo.HUM_FG_Length;
                model.GameOptionModel.HUM_KO_Length = repo.HUM_KO_Length;
                model.GameOptionModel.HUM_PassBlocking = repo.HUM_PassBlocking;
                model.GameOptionModel.HUM_Punt_Accuracy = repo.HUM_Punt_Accuracy;
                model.GameOptionModel.HUM_Punt_Length = repo.HUM_Punt_Length;
                model.GameOptionModel.HUM_QB_Accuracy = repo.HUM_QB_Accuracy;
                model.GameOptionModel.HUM_RB_Ability = repo.HUM_RB_Ability;
                model.GameOptionModel.HUM_RunBlocking = repo.HUM_RunBlocking;
                model.GameOptionModel.HUM_Tackling = repo.HUM_Tackling;
                model.GameOptionModel.HUM_WR_Catching = repo.HUM_WR_Catching;
                #endregion
            }

            if (penalties)
            {
                model.GameOptionModel.Offsides = repo.OffsidesON;
                model.GameOptionModel.Pen_FalseStart = repo.FalseStart;
                model.GameOptionModel.Pen_Holding = repo.Holding;
                model.GameOptionModel.Pen_FaceMask = repo.faceMask;
                model.GameOptionModel.Pen_Clipping = repo.Clipping;
                model.GameOptionModel.Pen_Grounding = repo.Grounding;
                model.GameOptionModel.Pen_RoughPasser = repo.RoughPasser;
                model.GameOptionModel.Pen_RoughKicker = repo.RoughKicker;
                model.GameOptionModel.Pen_OffPassInterference = repo.OFFPassInt;
                model.GameOptionModel.Pen_DefPassInterference = repo.DEFPassInt;
                model.GameOptionModel.Pen_CatchInterference = repo.CatchInt;
            }

            InitCurrentOptions();
        }
                
        public void LoadRepo()
        {
            List<SliderSet> sliders = new List<SliderSet>();

            string ampsliderfile = Application.StartupPath + @"\Sliders.REP";
            if (!File.Exists(ampsliderfile))
            {
                MessageBox.Show("Cannot Find Slider Repo File.", "File Does Not Exist", MessageBoxButtons.OK);
                return;
            }            

            binreader = new BinaryReader(File.Open(ampsliderfile, FileMode.Open));
            try
            {
                if (binreader.ReadUInt32() != this.Header)
                {
                    MessageBox.Show("Not a valid Slider Repo File.", "File Not Valid", MessageBoxButtons.OK);
                    binreader.Close();
                    return;
                }

                this.entries = binreader.ReadUInt16();

                for (int c = 0; c < this.entries; c++)
                {
                    SliderSet set = new SliderSet();

                    set.SliderName = binreader.ReadString();
                    set.FileVersion = binreader.ReadByte();
                    set.GameMode = binreader.ReadByte();
                    set.Skill = binreader.ReadByte();
                    set.QtrLength = binreader.ReadByte();
                    set.AccelClock = binreader.ReadBoolean();

                    #region CPU Settings
                    set.CPU_Def_Awareness = binreader.ReadByte();
                    set.CPU_Def_BreakBlock = binreader.ReadByte();
                    set.CPU_Def_Interceptions = binreader.ReadByte();
                    set.CPU_Def_Knockdowns = binreader.ReadByte();
                    set.CPU_FG_Accuracy = binreader.ReadByte();
                    set.CPU_FG_Length = binreader.ReadByte();
                    set.CPU_KO_Length = binreader.ReadByte();
                    set.CPU_PassBlocking = binreader.ReadByte();
                    set.CPU_Punt_Accuracy = binreader.ReadByte();
                    set.CPU_Punt_Length = binreader.ReadByte();
                    set.CPU_QB_Accuracy = binreader.ReadByte();
                    set.CPU_RB_Ability = binreader.ReadByte();
                    set.CPU_RunBlocking = binreader.ReadByte();
                    set.CPU_Tackling = binreader.ReadByte();
                    set.CPU_WR_Catching = binreader.ReadByte();
                    #endregion

                    #region HUMAN Settings
                    set.HUM_Def_Awareness = binreader.ReadByte();
                    set.HUM_Def_BreakBlock = binreader.ReadByte();
                    set.HUM_Def_Interceptions = binreader.ReadByte();
                    set.HUM_Def_Knockdowns = binreader.ReadByte();
                    set.HUM_FG_Accuracy = binreader.ReadByte();
                    set.HUM_FG_Length = binreader.ReadByte();
                    set.HUM_KO_Length = binreader.ReadByte();
                    set.HUM_PassBlocking = binreader.ReadByte();
                    set.HUM_Punt_Accuracy = binreader.ReadByte();
                    set.HUM_Punt_Length = binreader.ReadByte();
                    set.HUM_QB_Accuracy = binreader.ReadByte();
                    set.HUM_RB_Ability = binreader.ReadByte();
                    set.HUM_RunBlocking = binreader.ReadByte();
                    set.HUM_Tackling = binreader.ReadByte();
                    set.HUM_WR_Catching = binreader.ReadByte();
                    #endregion

                    #region Penalties
                    set.PenaltiesON = binreader.ReadBoolean();
                    set.OffsidesON = binreader.ReadBoolean();
                    set.FalseStart = binreader.ReadByte();
                    set.Holding = binreader.ReadByte();
                    set.faceMask = binreader.ReadByte();
                    set.Grounding = binreader.ReadByte();
                    set.Clipping = binreader.ReadByte();
                    set.RoughPasser = binreader.ReadByte();
                    set.RoughKicker = binreader.ReadByte();
                    set.DEFPassInt = binreader.ReadByte();
                    set.OFFPassInt = binreader.ReadByte();
                    set.CatchInt = binreader.ReadByte();
                    #endregion

                    set.InjuriesON = binreader.ReadBoolean();
                    set.InjuryFrequency = binreader.ReadByte();
                    set.SimInjuryON = binreader.ReadBoolean();
                    set.SimInjuryFrequency = binreader.ReadByte();

                    set.PassLeadSensitivity = binreader.ReadByte();
                    set.AutoSub_in = binreader.ReadByte();
                    set.AutoSub_out = binreader.ReadByte();

                    sliders.Add(set);
                }
            }

            catch (EndOfStreamException e)
            {
                MessageBox.Show(e.GetType().Name, "Corrupted Slider File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                binreader.Close();
            }

            for (int entry = 0; entry < sliders.Count; entry++)
            {
                bool exists = false;
                for (int c = 0; c < Amp_Sliders.Count; c++)
                {
                    if (sliders[entry].SliderName == Amp_Sliders[c].SliderName)
                        exists = true;
                }

                if (!exists)
                    Amp_Sliders.Add(sliders[entry]);
            }

            entries = Amp_Sliders.Count;
            currentset = 0;
            InitRepoSet(currentset);
        }

        public void SaveRepo()
        {
            string ampsliderfile = Application.StartupPath + @"\Sliders.AMP";
            binwriter = new BinaryWriter(File.Open(ampsliderfile, FileMode.Create));

            binwriter.Write(Header);

            binwriter.Write((UInt16)Amp_Sliders.Count);

            for (int c = 0; c < Amp_Sliders.Count; c++)
            {
                binwriter.Write(Amp_Sliders[c].SliderName);
                binwriter.Write((byte)Amp_Sliders[c].FileVersion);
                binwriter.Write((byte)Amp_Sliders[c].GameMode);
                binwriter.Write((byte)Amp_Sliders[c].Skill);
                binwriter.Write((byte)Amp_Sliders[c].QtrLength);
                binwriter.Write(Amp_Sliders[c].AccelClock);

                #region CPU Settings
                binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Awareness);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Def_BreakBlock);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Interceptions);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Knockdowns);
                binwriter.Write((byte)Amp_Sliders[c].CPU_FG_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].CPU_FG_Length);
                binwriter.Write((byte)Amp_Sliders[c].CPU_KO_Length);
                binwriter.Write((byte)Amp_Sliders[c].CPU_PassBlocking);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Punt_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Punt_Length);
                binwriter.Write((byte)Amp_Sliders[c].CPU_QB_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].CPU_RB_Ability);
                binwriter.Write((byte)Amp_Sliders[c].CPU_RunBlocking);
                binwriter.Write((byte)Amp_Sliders[c].CPU_Tackling);
                binwriter.Write((byte)Amp_Sliders[c].CPU_WR_Catching);
                #endregion
               
                #region HUMAN Settings
                binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Awareness);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Def_BreakBlock);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Interceptions);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Knockdowns);
                binwriter.Write((byte)Amp_Sliders[c].HUM_FG_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].HUM_FG_Length);
                binwriter.Write((byte)Amp_Sliders[c].HUM_KO_Length);
                binwriter.Write((byte)Amp_Sliders[c].HUM_PassBlocking);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Punt_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Punt_Length);
                binwriter.Write((byte)Amp_Sliders[c].HUM_QB_Accuracy);
                binwriter.Write((byte)Amp_Sliders[c].HUM_RB_Ability);
                binwriter.Write((byte)Amp_Sliders[c].HUM_RunBlocking);
                binwriter.Write((byte)Amp_Sliders[c].HUM_Tackling);
                binwriter.Write((byte)Amp_Sliders[c].HUM_WR_Catching);
                #endregion

                #region Penalties
                binwriter.Write(Amp_Sliders[c].PenaltiesON);
                binwriter.Write(Amp_Sliders[c].OffsidesON);
                binwriter.Write((byte)Amp_Sliders[c].FalseStart);
                binwriter.Write((byte)Amp_Sliders[c].Holding);
                binwriter.Write((byte)Amp_Sliders[c].faceMask);
                binwriter.Write((byte)Amp_Sliders[c].Grounding);
                binwriter.Write((byte)Amp_Sliders[c].Clipping);
                binwriter.Write((byte)Amp_Sliders[c].RoughPasser);
                binwriter.Write((byte)Amp_Sliders[c].RoughKicker);
                binwriter.Write((byte)Amp_Sliders[c].DEFPassInt);
                binwriter.Write((byte)Amp_Sliders[c].OFFPassInt);
                binwriter.Write((byte)Amp_Sliders[c].CatchInt);
                #endregion

                binwriter.Write(Amp_Sliders[c].InjuriesON);
                binwriter.Write((byte)Amp_Sliders[c].InjuryFrequency);
                binwriter.Write(Amp_Sliders[c].SimInjuryON);
                binwriter.Write((byte)Amp_Sliders[c].SimInjuryFrequency);

                binwriter.Write((byte)Amp_Sliders[c].PassLeadSensitivity);
                binwriter.Write((byte)Amp_Sliders[c].AutoSub_in);
                binwriter.Write((byte)Amp_Sliders[c].AutoSub_out);
            }            

            binwriter.Close();
            InitRepoSet(currentset);
        }

        public void LoadSliders()
        {
            string loadfile = "";
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.DefaultExt = "REP";
            opendialog.Filter = "Madden Slider File (*.REP)|*.REP";
            opendialog.FilterIndex = 1;
            opendialog.AddExtension = true;
            opendialog.Title = "Load Madden Sliders";

            if (opendialog.ShowDialog() == DialogResult.OK)
                loadfile = opendialog.FileName;

            List<SliderSet> sliders = new List<SliderSet>();
            binreader = new BinaryReader(File.Open(loadfile, FileMode.Open));
            try
            {
                if (binreader.ReadUInt32() != this.Header)
                {
                    MessageBox.Show("Not a valid Slider File.", "File Not Valid", MessageBoxButtons.OK);
                    binreader.Close();
                    return;
                }

                this.entries = binreader.ReadUInt16();

                for (int c = 0; c < this.entries; c++)
                {
                    SliderSet set = new SliderSet();

                    set.SliderName = binreader.ReadString();
                    set.FileVersion = binreader.ReadByte();
                    set.GameMode = binreader.ReadByte();
                    set.Skill = binreader.ReadByte();
                    set.QtrLength = binreader.ReadByte();
                    set.AccelClock = binreader.ReadBoolean();

                    #region CPU Settings
                    set.CPU_Def_Awareness = binreader.ReadByte();
                    set.CPU_Def_BreakBlock = binreader.ReadByte();
                    set.CPU_Def_Interceptions = binreader.ReadByte();
                    set.CPU_Def_Knockdowns = binreader.ReadByte();
                    set.CPU_FG_Accuracy = binreader.ReadByte();
                    set.CPU_FG_Length = binreader.ReadByte();
                    set.CPU_KO_Length = binreader.ReadByte();
                    set.CPU_PassBlocking = binreader.ReadByte();
                    set.CPU_Punt_Accuracy = binreader.ReadByte();
                    set.CPU_Punt_Length = binreader.ReadByte();
                    set.CPU_QB_Accuracy = binreader.ReadByte();
                    set.CPU_RB_Ability = binreader.ReadByte();
                    set.CPU_RunBlocking = binreader.ReadByte();
                    set.CPU_Tackling = binreader.ReadByte();
                    set.CPU_WR_Catching = binreader.ReadByte();
                    #endregion

                    #region HUMAN Settings
                    set.HUM_Def_Awareness = binreader.ReadByte();
                    set.HUM_Def_BreakBlock = binreader.ReadByte();
                    set.HUM_Def_Interceptions = binreader.ReadByte();
                    set.HUM_Def_Knockdowns = binreader.ReadByte();
                    set.HUM_FG_Accuracy = binreader.ReadByte();
                    set.HUM_FG_Length = binreader.ReadByte();
                    set.HUM_KO_Length = binreader.ReadByte();
                    set.HUM_PassBlocking = binreader.ReadByte();
                    set.HUM_Punt_Accuracy = binreader.ReadByte();
                    set.HUM_Punt_Length = binreader.ReadByte();
                    set.HUM_QB_Accuracy = binreader.ReadByte();
                    set.HUM_RB_Ability = binreader.ReadByte();
                    set.HUM_RunBlocking = binreader.ReadByte();
                    set.HUM_Tackling = binreader.ReadByte();
                    set.HUM_WR_Catching = binreader.ReadByte();
                    #endregion

                    #region Penalties
                    set.PenaltiesON = binreader.ReadBoolean();
                    set.OffsidesON = binreader.ReadBoolean();
                    set.FalseStart = binreader.ReadByte();
                    set.Holding = binreader.ReadByte();
                    set.faceMask = binreader.ReadByte();
                    set.Grounding = binreader.ReadByte();
                    set.Clipping = binreader.ReadByte();
                    set.RoughPasser = binreader.ReadByte();
                    set.RoughKicker = binreader.ReadByte();
                    set.DEFPassInt = binreader.ReadByte();
                    set.OFFPassInt = binreader.ReadByte();
                    set.CatchInt = binreader.ReadByte();
                    #endregion

                    set.InjuriesON = binreader.ReadBoolean();
                    set.InjuryFrequency = binreader.ReadByte();
                    set.SimInjuryON = binreader.ReadBoolean();
                    set.SimInjuryFrequency = binreader.ReadByte();
                    set.PassLeadSensitivity = binreader.ReadByte();
                    set.AutoSub_in = binreader.ReadByte();
                    set.AutoSub_out = binreader.ReadByte();
                    sliders.Add(set);
                }
            }

            catch (EndOfStreamException e)
            {
                MessageBox.Show(e.GetType().Name, "Corrupted Slider File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                binreader.Close();
            }

            for (int entry = 0; entry < sliders.Count; entry++)
            {
                bool exists = false;
                for (int c = 0; c < Amp_Sliders.Count; c++)
                {
                    if (sliders[entry].SliderName == Amp_Sliders[c].SliderName)
                        exists = true;
                }

                if (!exists)
                    Amp_Sliders.Add(sliders[entry]);
            }

            entries = Amp_Sliders.Count;
            currentset = 0;
            InitRepoSet(currentset);
        }
        
        public void SaveSlider()
        {
            string savefile = "";
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.DefaultExt = "REP";
            savedialog.Filter = "Madden Slider File (*.REP)|*.REP";
            savedialog.FilterIndex = 1;
            savedialog.AddExtension = true;
            savedialog.Title = "Save Madden Sliders";

            if (savedialog.ShowDialog() == DialogResult.OK)
                savefile = savedialog.FileName;

            binwriter = new BinaryWriter(File.Open(savefile, FileMode.Create));

            binwriter.Write(Header);

            int c = currentset;

            binwriter.Write((UInt16)1);

            binwriter.Write(Amp_Sliders[c].SliderName);
            binwriter.Write((byte)Amp_Sliders[c].FileVersion);
            binwriter.Write((byte)Amp_Sliders[c].GameMode);
            binwriter.Write((byte)Amp_Sliders[c].Skill);
            binwriter.Write((byte)Amp_Sliders[c].QtrLength);
            binwriter.Write(Amp_Sliders[c].AccelClock);

            #region CPU Settings
            binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Awareness);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Def_BreakBlock);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Interceptions);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Def_Knockdowns);
            binwriter.Write((byte)Amp_Sliders[c].CPU_FG_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].CPU_FG_Length);
            binwriter.Write((byte)Amp_Sliders[c].CPU_KO_Length);
            binwriter.Write((byte)Amp_Sliders[c].CPU_PassBlocking);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Punt_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Punt_Length);
            binwriter.Write((byte)Amp_Sliders[c].CPU_QB_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].CPU_RB_Ability);
            binwriter.Write((byte)Amp_Sliders[c].CPU_RunBlocking);
            binwriter.Write((byte)Amp_Sliders[c].CPU_Tackling);
            binwriter.Write((byte)Amp_Sliders[c].CPU_WR_Catching);
            #endregion
            #region HUMAN Settings
            binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Awareness);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Def_BreakBlock);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Interceptions);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Def_Knockdowns);
            binwriter.Write((byte)Amp_Sliders[c].HUM_FG_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].HUM_FG_Length);
            binwriter.Write((byte)Amp_Sliders[c].HUM_KO_Length);
            binwriter.Write((byte)Amp_Sliders[c].HUM_PassBlocking);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Punt_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Punt_Length);
            binwriter.Write((byte)Amp_Sliders[c].HUM_QB_Accuracy);
            binwriter.Write((byte)Amp_Sliders[c].HUM_RB_Ability);
            binwriter.Write((byte)Amp_Sliders[c].HUM_RunBlocking);
            binwriter.Write((byte)Amp_Sliders[c].HUM_Tackling);
            binwriter.Write((byte)Amp_Sliders[c].HUM_WR_Catching);
            #endregion

            #region Penalties
            binwriter.Write(Amp_Sliders[c].PenaltiesON);
            binwriter.Write(Amp_Sliders[c].OffsidesON);
            binwriter.Write((byte)Amp_Sliders[c].FalseStart);
            binwriter.Write((byte)Amp_Sliders[c].Holding);
            binwriter.Write((byte)Amp_Sliders[c].faceMask);
            binwriter.Write((byte)Amp_Sliders[c].Grounding);
            binwriter.Write((byte)Amp_Sliders[c].Clipping);
            binwriter.Write((byte)Amp_Sliders[c].RoughPasser);
            binwriter.Write((byte)Amp_Sliders[c].RoughKicker);
            binwriter.Write((byte)Amp_Sliders[c].DEFPassInt);
            binwriter.Write((byte)Amp_Sliders[c].OFFPassInt);
            binwriter.Write((byte)Amp_Sliders[c].CatchInt);
            #endregion

            binwriter.Write(Amp_Sliders[c].InjuriesON);
            binwriter.Write((byte)Amp_Sliders[c].InjuryFrequency);
            binwriter.Write(Amp_Sliders[c].SimInjuryON);
            binwriter.Write((byte)Amp_Sliders[c].SimInjuryFrequency);

            binwriter.Write((byte)Amp_Sliders[c].PassLeadSensitivity);
            binwriter.Write((byte)Amp_Sliders[c].AutoSub_in);
            binwriter.Write((byte)Amp_Sliders[c].AutoSub_out);
            binwriter.Close();
        }

        public void InitSliderView(bool search)
        {            
            SliderView.Clear();
            SliderView.View = View.Details;
            SliderView.GridLines = true;
            SliderView.FullRowSelect = true;

            SliderView.Columns.Add("Slider Set Name", 100,HorizontalAlignment.Left);
            SliderView.Columns.Add("Version", 60, HorizontalAlignment.Left);
            SliderView.Columns.Add("Mode", 60, HorizontalAlignment.Left);
            SliderView.Columns.Add("Skill", 60, HorizontalAlignment.Left);

            foreach (SliderSet set in Amp_Sliders)
            {
                if (search)
                {
                    if (set.FileVersion == REPO_Search_Version_Combo.SelectedIndex || REPO_Search_Version_Combo.SelectedIndex == 5)
                        if (set.GameMode == REPO_Search_Mode_Combo.SelectedIndex || REPO_Search_Mode_Combo.SelectedIndex == 3)
                            if (set.Skill == REPO_Search_Skill_Combo.SelectedIndex || REPO_Search_Skill_Combo.SelectedIndex == 4)
                            {
                                string[] arr = new string[4];
                                arr[0] = set.SliderName;
                                arr[1] = REPO_Version_Combo.Items[set.FileVersion].ToString();
                                arr[2] = REPO_Mode_Combo.Items[set.GameMode].ToString();
                                arr[3] = REPO_Skill_Combo.Items[set.Skill].ToString();
                                ListViewItem item = new ListViewItem(arr);
                                SliderView.Items.Add(item);
                            }
                }

                else
                {
                    string[] arr = new string[4];
                    arr[0] = set.SliderName;
                    arr[1] = REPO_Version_Combo.Items[set.FileVersion].ToString();
                    arr[2] = REPO_Mode_Combo.Items[set.GameMode].ToString();
                    arr[3] = REPO_Skill_Combo.Items[set.Skill].ToString();
                    ListViewItem item = new ListViewItem(arr);
                    SliderView.Items.Add(item);
                }
            }

            search = false;
        }


        #region Form Functions

        #region Current Slider Setting Functions

        private void CUR_GameMode_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.Playmode = CUR_GameMode_Combo.SelectedIndex;
            }
        }
        
        private void CUR_Skill_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.SkillLevel = CUR_Skill_Combo.SelectedIndex;
        }
        
        private void CUR_Qtr_Updown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.QuarterLength = (int)CUR_Qtr_Updown.Value;
        }

        private void CUR_AcceClock_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.AcceleratedClock = Convert.ToInt32(CUR_AcceClock_Checkbox.Checked);
        }
        #region AI Sliders
        #region CPU AI
        private void CUR_CPU_QBAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_QB_Accuracy = (int)CUR_CPU_QBAcc.Value;
        }

        private void CUR_CPU_PassBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                if (model.MadVersion == MaddenFileVersion.Ver2019)
                    model.GameOptionModel.CPU_PassBlocking_19 = (int)CUR_CPU_PassBlock.Value;
                else model.GameOptionModel.CPU_PassBlocking = (int)CUR_CPU_PassBlock.Value;
        }

        private void CUR_CPU_WRCatch_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_WR_Catching = (int)CUR_CPU_WRCatch.Value;
        }

        private void CUR_CPU_RBAbility_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_RB_Ability = (int)CUR_CPU_RBAbility.Value;
        }

        private void CUR_CPU_RunBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_RunBlocking = (int)CUR_CPU_RunBlock.Value;
        }

        private void CUR_CPU_Aware_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Def_Awareness = (int)CUR_CPU_Aware.Value;
        }

        private void CUR_CPU_Knockdown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Def_Knockdowns = (int)CUR_CPU_Knockdown.Value;
        }

        private void CUR_CPU_Ints_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Def_Interceptions = (int)CUR_CPU_Ints.Value;
        }

        private void CUR_CPU_BreakBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Def_BreakBlock = (int)CUR_CPU_BreakBlock.Value;
        }

        private void CUR_CPU_Tackle_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Tackling = (int)CUR_CPU_Tackle.Value;
        }

        private void CUR_CPU_FGLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_FG_Length = (int)CUR_CPU_FGLength.Value;
        }

        private void CUR_CPU_FGAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_FG_Accuracy = (int)CUR_CPU_FGAcc.Value;
        }

        private void CUR_CPU_PuntLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Punt_Length = (int)CUR_CPU_PuntLength.Value;
        }

        private void CUR_CPU_PuntAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_Punt_Accuracy = (int)CUR_CPU_PuntAcc.Value;
        }

        private void CUR_CPU_Kickoff_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CPU_KO_Length = (int)CUR_CPU_Kickoff.Value;
        }
        #endregion
        #region Human AI
        private void CUR_HUM_QBAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_QB_Accuracy = (int)CUR_HUM_QBAcc.Value;
        }

        private void CUR_HUM_PassBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_PassBlocking = (int)CUR_HUM_PassBlock.Value;
        }

        private void CUR_HUM_WRCatch_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_WR_Catching = (int)CUR_HUM_WRCatch.Value;
        }

        private void CUR_HUM_RBAbility_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_RB_Ability = (int)CUR_HUM_RBAbility.Value;
        }

        private void CUR_HUM_RunBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_RunBlocking = (int)CUR_HUM_RunBlock.Value;
        }

        private void CUR_HUM_Aware_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Def_Awareness = (int)CUR_HUM_Aware.Value;
        }

        private void CUR_HUM_Knockdown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Def_Knockdowns = (int)CUR_HUM_Knockdown.Value;
        }

        private void CUR_HUM_Ints_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Def_Interceptions = (int)CUR_HUM_Ints.Value;
        }

        private void CUR_HUM_BreakBlock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Def_BreakBlock = (int)CUR_HUM_BreakBlock.Value;
        }

        private void CUR_HUM_Tackle_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Tackling = (int)CUR_HUM_Tackle.Value;
        }

        private void CUR_HUM_FGLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_FG_Length = (int)CUR_HUM_FGLength.Value;
        }

        private void CUR_HUM_FGAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_FG_Accuracy = (int)CUR_HUM_FGAcc.Value;
        }

        private void CUR_HUM_PuntLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Punt_Length = (int)CUR_HUM_PuntLength.Value;
        }

        private void CUR_HUM_PuntAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_Punt_Accuracy = (int)CUR_HUM_PuntAcc.Value;
        }

        private void CUR_HUM_Kickoff_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.HUM_KO_Length = (int)CUR_HUM_Kickoff.Value;
        }
        #endregion
        #endregion

        #region Penalties
        private void CUR_PenaltiesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.Penalties = CUR_PenaltiesON.Checked;
                InitCurrentOptions();
            }
        }

        private void CUR_OffsidesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Offsides = CUR_OffsidesON.Checked;
        }

        private void CUR_FalseStart_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_FalseStart = (int)CUR_FalseStart.Value;
        }

        private void CUR_Holding_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_Holding = (int)CUR_Holding.Value;
        }

        private void CUR_Facemask_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_FaceMask = (int)CUR_Facemask.Value;
        }

        private void CUR_Clipping_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_Clipping = (int)CUR_Clipping.Value;
        }

        private void CUR_Grounding_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_Grounding = (int)CUR_Grounding.Value;
        }

        private void CUR_RoughPasser_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_RoughPasser = (int)CUR_RoughPasser.Value;
        }

        private void CUR_RoughKicker_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_RoughKicker = (int)CUR_RoughKicker.Value;
        }

        private void CUR_OffPassInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_OffPassInterference = (int)CUR_OffPassInt.Value;
        }

        private void CUR_DefPassInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_DefPassInterference = (int)CUR_DefPassInt.Value;
        }

        private void CUR_CatchInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Pen_CatchInterference = (int)CUR_CatchInt.Value;
        }
        #endregion

        private void CUR_FieldLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.FieldLines = CUR_FieldLines.SelectedIndex;
        }

        private void CUR_CameraView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.CameraView = CUR_CameraView.SelectedIndex;
        }

        private void CUR_InjuriesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.Injuries = Convert.ToInt32(CUR_InjuriesON.Checked);
                InitCurrentOptions();
            }
        }

        private void CUR_SimInjury_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.SimInjuryON = CUR_SimInjury.Checked;
                InitCurrentOptions();
            }
        }

        private void CUR_WeatherON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.RandomWeather = CUR_WeatherON.Checked;
                InitCurrentOptions();
            }
        }

        private void CUR_TradeDeadline_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.TradeDeadline = CUR_TradeDeadline.Checked;
        }

        private void CUR_SalaryCap_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.SalaryCap = CUR_SalaryCap.Checked;
                InitCurrentOptions();
            }
        }

        private void CUR_InjuryFreq_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.InGameInjury = (int)CUR_InjuryFreq.Value;
        }

        private void CUR_SimInjuryFreq_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.SimInjury_Freq = (int)CUR_SimInjuryFreq.Value;
        }

        private void CUR_WeatherEffects_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.WeatherEffects = (int)CUR_WeatherEffects.Value;
        }

        private void CUR_PlayClock_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.PlayClock = CUR_PlayClock.Checked;
        }

        private void CUR_CapPenalty_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.SalaryCapPenalty = CUR_CapPenalty.Checked;
        }

        private void CUR_AutoReplay_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.AutoReplay = CUR_AutoReplay.Checked;
        }

        private void CUR_Fatigue_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.GameOptionModel.Fatigue = Convert.ToInt32(CUR_Fatigue.Checked);
        }

        private void CUR_FairPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                model.GameOptionModel.FairPlay = CUR_FairPlay.Checked;
                InitCurrentOptions();
            }
        }
        
        private void CUR_PassLead_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.UserOptionModel.PassLead = (int)(CUR_PassLead.Value * 10);
        }

        private void CUR_SubOut_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.UserOptionModel.AutoSubOut = (int)CUR_SubOut.Value;
        }

        private void CUR_SubIn_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                model.UserOptionModel.AutoSubIn = (int)CUR_SubIn.Value;
        }
        #endregion
        
        #region REPO Slider setting functions

        private void REPO_SliderName_TextChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].SliderName = REPO_SliderName.Text;
        }

        private void REPO_QtrLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].QtrLength = (int)REPO_QtrLength.Value;
        }

        private void REPO_AccelClock_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].AccelClock = REPO_AccelClock.Checked;
        }

        private void REPO_Version_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].FileVersion = REPO_Version_Combo.SelectedIndex;
        }

        private void REPO_Mode_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].GameMode = REPO_Mode_Combo.SelectedIndex;
        }

        private void REPO_Skill_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].Skill = REPO_Skill_Combo.SelectedIndex;
        }


        #region CPU AI
        private void REPO_CPU_QBAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_QB_Accuracy = (int)REPO_CPU_QBAcc.Value;
        }

        private void REPO_CPU_Passblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_PassBlocking = (int)REPO_CPU_Passblock.Value;
        }

        private void REPO_CPU_WRCatch_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_WR_Catching = (int)REPO_CPU_WRCatch.Value;
        }

        private void REPO_CPU_RBAbility_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_RB_Ability = (int)REPO_CPU_RBAbility.Value;
        }

        private void REPO_CPU_Runblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_RunBlocking = (int)REPO_CPU_Runblock.Value;
        }

        private void REPO_CPU_Aware_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Def_Awareness = (int)REPO_CPU_Aware.Value;
        }

        private void REPO_CPU_Knockdown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Def_Knockdowns = (int)REPO_CPU_Knockdown.Value;
        }

        private void REPO_CPU_Int_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Def_Interceptions = (int)REPO_DefPassInt.Value;
        }

        private void REPO_CPU_Breakblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Def_BreakBlock = (int)REPO_CPU_Breakblock.Value;
        }

        private void REPO_CPU_Tackle_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Tackling = (int)REPO_CPU_Tackle.Value;
        }

        private void REPO_CPU_FGLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_FG_Length = (int)REPO_CPU_FGLength.Value;
        }

        private void REPO_CPU_FGAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_FG_Accuracy = (int)REPO_CPU_FGAcc.Value;
        }

        private void REPO_CPU_PuntLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Punt_Length = (int)REPO_CPU_PuntLength.Value;
        }

        private void REPO_CPU_PuntAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_Punt_Accuracy = (int)REPO_CPU_PuntAcc.Value;
        }

        private void REPO_CPU_KOLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CPU_KO_Length = (int)REPO_CPU_KOLength.Value;
        }
        #endregion

        #region HUMAN AI
        private void REPO_HUM_QBAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_QB_Accuracy = (int)REPO_HUM_QBAcc.Value;
        }

        private void REPO_HUM_Passblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_PassBlocking = (int)REPO_HUM_Passblock.Value;
        }

        private void REPO_HUM_WRcatch_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_WR_Catching = (int)REPO_HUM_WRcatch.Value;
        }

        private void REPO_HUM_RBAbility_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_RB_Ability = (int)REPO_HUM_RBAbility.Value;
        }

        private void REPO_HUM_Runblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_RunBlocking = (int)REPO_HUM_Runblock.Value;
        }
        
        private void REPO_HUM_Aware_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Def_Awareness = (int)REPO_HUM_Aware.Value;
        }

        private void REPO_HUM_Knockdown_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Def_Knockdowns = (int)REPO_HUM_Knockdown.Value;
        }

        private void REPO_HUM_Int_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Def_Interceptions = (int)REPO_HUM_Int.Value;
        }

        private void REPO_HUM_Breakblock_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Def_BreakBlock = (int)REPO_HUM_Breakblock.Value;
        }

        private void REPO_HUM_Tackle_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Tackling = (int)REPO_HUM_Tackle.Value;
        }
        
        private void REPO_HUM_FGLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_FG_Length = (int)REPO_HUM_FGLength.Value;
        }

        private void REPO_HUM_FGAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_FG_Accuracy = (int)REPO_HUM_FGAcc.Value;
        }

        private void REPO_HUMPuntLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Punt_Length = (int)REPO_HUM_PuntLength.Value;
        }

        private void REPO_HUM_PuntAcc_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_Punt_Accuracy = (int)REPO_HUM_PuntAcc.Value;
        }

        private void REPO_HUM_KOLength_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].HUM_KO_Length = (int)REPO_HUM_KOLength.Value;
        }
        #endregion

        
        
        private void REPO_PenaltiesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                Amp_Sliders[currentset].PenaltiesON = REPO_PenaltiesON.Checked;
                InitRepoSet(currentset);
            }
        }

        private void REPO_OffsidesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].OffsidesON = REPO_OffsidesON.Checked;
        }

        private void REPO_Ingame_InjuriesON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                Amp_Sliders[currentset].InjuriesON = REPO_Ingame_InjuriesON.Checked;
                InitRepoSet(currentset);
            }
        }

        private void REPO_IngameInjuries_Freq_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].InjuryFrequency = (int)REPO_IngameInjuries_Freq.Value;
        }

        private void REPO_SimInjuryON_CheckedChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
            {
                Amp_Sliders[currentset].SimInjuryON = REPO_SimInjuryON.Checked;
                InitRepoSet(currentset);
            }
        }

        private void REPO_SimInjury_Freq_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].SimInjuryFrequency = (int)REPO_SimInjury_Freq.Value;
        }

        #region Penalties
        private void REPO_FalseStart_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].FalseStart = (int)REPO_FalseStart.Value;
        }

        private void REPO_RoughKicker_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].RoughKicker = (int)REPO_RoughKicker.Value;
        }

        private void REPO_RoughPasser_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].RoughPasser = (int)REPO_RoughPasser.Value;
        }

        private void REPO_Grounding_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].Grounding = (int)REPO_Grounding.Value;
        }

        private void REPO_Clipping_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].Clipping = (int)REPO_Clipping.Value;
        }

        private void REPO_OffPassInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].OFFPassInt = (int)REPO_OffPassInt.Value;
        }

        private void REPO_DefPassInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].DEFPassInt = (int)REPO_DefPassInt.Value;
        }

        private void REPO_CatchInt_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].CatchInt = (int)REPO_CatchInt.Value;
        }

        private void REPO_Holding_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].Holding = (int)REPO_Holding.Value;
        }

        private void REPO_Facemask_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].faceMask = (int)REPO_Facemask.Value;
        }
        #endregion

        private void REPO_PassLead_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].PassLeadSensitivity = (int)(REPO_PassLead.Value);
        }

        private void REPO_SubOut_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].AutoSub_out = (int)REPO_SubOut.Value;
        }

        private void REPO_SubIn_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitializing)
                Amp_Sliders[currentset].AutoSub_in = (int)REPO_SubIn.Value;
        }
        #endregion

        

        private void CreateNew_Button_Click(object sender, EventArgs e)
        {
            SliderSet newset = new SliderSet();
            newset.SliderName = "Test";
            Amp_Sliders.Add(newset);
            if (Amp_Sliders.Count == 0)
                currentset = 0;
            else currentset = Amp_Sliders.Count-1;
            InitRepoSet(currentset);
            InitSliderView(search);
        }
        
        private void LoadRepo_Button_Click(object sender, EventArgs e)
        {
            LoadRepo();
            if(Amp_Sliders.Count == 0)
            {
                Amp_Sliders.Add(new SliderSet());
                SaveRepo();
            }

            InitSliderView(search);
        }
         
        private void Save_Repo_Button_Click(object sender, EventArgs e)
        {
            SaveRepo();
        }

        private void SliderView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int number = -1;
            if (!isInitializing && SliderView.SelectedItems.Count > 0)
            {
                ListViewItem item = SliderView.SelectedItems[0];
                for (int c = 0; c < Amp_Sliders.Count;c++ )
                {
                    if (item.Text.Contains(Amp_Sliders[c].SliderName))
                        number = c;
                }
                if (number >= 0)
                {
                    currentset = number;
                    InitRepoSet(currentset);
                }
            }
        }

        #endregion

        private void CopyRepoToCurrent_Button_Click(object sender, EventArgs e)
        {
            if (!isInitializing)
                SetSlidersFromRepo(Amp_Sliders[currentset], true, true, true, true);
        }

        private void ImportRepo_Button_Click(object sender, EventArgs e)
        {

        }

        private void REPO_Search_Button_Click(object sender, EventArgs e)
        {
            search = true;
            InitSliderView(search);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Amp_Sliders.Count == 0)
                return;            
            if (!isInitializing)
            {                
                Amp_Sliders.RemoveAt(currentset);
                if (Amp_Sliders.Count != 0)
                {
                    currentset--;
                    SetSlidersFromRepo(Amp_Sliders[currentset], true, true, true, true);
                }
                
                else CleanUI();
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

               

        
    }

    public class SliderSet
    {        
        public string SliderName = "Default";
        public int FileVersion = 4;
        public int GameMode = 0;
        public int Skill = 0;
        public int QtrLength = 5;
        public bool AccelClock = false;

        public bool InjuriesON = true;        
        public int InjuryFrequency = 50;
        public bool SimInjuryON = true;
        public int SimInjuryFrequency = 50;

        public bool PenaltiesON = true;
        public bool OffsidesON = true;
        public int FalseStart = 50;
        public int Holding = 50;
        public int faceMask = 50;
        public int Grounding = 50;
        public int Clipping = 50;
        public int RoughPasser = 50;
        public int RoughKicker = 50;
        public int DEFPassInt = 50;
        public int OFFPassInt = 50;
        public int CatchInt = 50;

        public bool WeatherON = true;
        public int WeatherEffects = 1;          //  check this
        public bool SalaryCap = true;
        public bool CapPenalty = true;
        public bool AutoReplay = true;
        public bool Fatigue = true;
        public bool FairPlay = true;
        public bool PlayClock = true;
        
        public bool TradeDeadline = true;

        #region CPU Settings
        public int CPU_Def_Awareness = 50;
        public int CPU_Def_BreakBlock = 50;
        public int CPU_Def_Interceptions = 50;
        public int CPU_Def_Knockdowns = 50;
        public int CPU_FG_Accuracy = 50;
        public int CPU_FG_Length = 50;
        public int CPU_KO_Length = 50;
        public int CPU_PassBlocking = 50;
        public int CPU_Punt_Accuracy = 50;
        public int CPU_Punt_Length = 50;
        public int CPU_QB_Accuracy = 50;
        public int CPU_RB_Ability = 50;
        public int CPU_RunBlocking = 50;
        public int CPU_Tackling = 50;
        public int CPU_WR_Catching = 50;
        #endregion

        #region HUMAN Settings
        public int HUM_Def_Awareness = 50;
        public int HUM_Def_BreakBlock = 50;
        public int HUM_Def_Interceptions = 50;
        public int HUM_Def_Knockdowns = 50;
        public int HUM_FG_Accuracy = 50;
        public int HUM_FG_Length = 50;
        public int HUM_KO_Length = 50;
        public int HUM_PassBlocking = 50;
        public int HUM_Punt_Accuracy = 50;
        public int HUM_Punt_Length = 50;
        public int HUM_QB_Accuracy = 50;
        public int HUM_RB_Ability = 50;
        public int HUM_RunBlocking = 50;
        public int HUM_Tackling = 50;
        public int HUM_WR_Catching = 50;
        #endregion

        public int PassLeadSensitivity = 5;
        public int AutoSub_in = 85;
        public int AutoSub_out = 80;
        
        public SliderSet()
        {

        }
    }
}
