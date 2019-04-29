/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
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
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{    
    public class GameOptionRecord : TableRecordModel
	{
        public const string OFFSIDES = "EPOS";
        public const string PENALTIES = "EPPN";
        public const string INGAME_INJURY_FREQUENCY = "INGI";        
        public const string CPU_QB_ACC = "OAAC";

        public const string CPU_DEF_AWR = "OAAW";
        public const string CPU_BRK_BLOCK = "OADB";
        public const string CPU_DEF_KNOCKDOWNS = "OADK";
        public const string CPU_FG_ACCURACY = "OAFA";
        public const string CPU_FG_LENGTH = "OAFL";
        public const string CPU_INTS = "OAIN";
        public const string CPU_KO_LENGTH = "OAKL";
        public const string CPU_PASS_BLOCK = "OAOP";
        public const string CPU_PUNT_ACC = "OAPA";
        public const string CPU_PUNT_LENGTH = "OAPL";
        public const string CPU_RB_ABILITY = "OARA";
        public const string CPU_RUN_BLOCK = "OARB";
        public const string CPU_CATCHING = "OARC";
        public const string CPU_TACKLE = "OATA";
        public const string CAMERA_VIEW = "OCVW";
        public const string ACCELERATED_CLOCK = "OFAC";
        public const string AUTO_REPLAY = "OFAI";
        public const string FAIR_PLAY = "OFAP";                 //  2006-2008

        public const string FIELD_LINES = "OFDL";
        public const string FATIGUE = "OFFA";

        public const string INJURIES = "OFIN";

        public const string PLAY_CLOCK = "OFPC";
        
        public const string FRANCHIS_LOG_DIR = "OFRD";
        public const string FRANCHISE_LOG_UNIQUE = "OFRF";
        public const string FRANCHISE_LOG_FORMAT = "OFRG";
        public const string SALARY_CAP = "OFSC";
        public const string TRADE_DEADLINE = "OFTD";
        public const string WEATHER_EFFECTS = "OFWF";
        public const string EXHIBITION_LOG_DIR = "OGLD";
        public const string EXHIBITION_LOG_UNIQUE = "OGLF";
        public const string EXHIBITION_LOG_FORMAT = "OGLG";

        public const string HUM_QB_ACC = "OHAC";
        public const string HUM_DEF_AWR = "OHAW";
        public const string HUM_BRK_BLOCK = "OHDB";
        public const string HUM_DEF_KNOCKDOWNS = "OHDK";
        public const string HUM_FG_ACCURACY = "OHFA";
        public const string HUM_FG_LENGTH = "OHFL";
        public const string HUM_INTS = "OHIN";
        public const string HUM_KO_LENGTH = "OHKL";
        public const string HUM_PASS_BLOCK = "OHOP";
        public const string HUM_PUNT_ACC = "OHPA";
        public const string HUM_PUNT_LENGTH = "OHPL";
        public const string HUM_RB_ABILITY = "OHRA";
        public const string HUM_RUN_BLOCK = "OHRB";
        public const string HUM_CATCHING = "OHRC";
        public const string HUM_TACKLE = "OHTA";

        public const string OWNER_MODE = "OOWN";

        public const string PEN_CLIPPING = "OPCL";
        public const string PEN_DEF_PASS_INT = "OPDP";
        public const string PEN_FACEMASK = "OPFM";
        public const string PEN_FALSE_START = "OPFS";
        public const string PEN_HOLDING = "OPHO";
        public const string PEN_GROUNDING = "OPIG";
        public const string PLAY_MODE = "OPLM";
        public const string PEN_OFF_PASS_INT = "OPOP";
        public const string OPOS = "OPOS";
        public const string OPPF = "OPPF";
        public const string PEN_PK_INT = "OPPI";
        public const string PEN_ROUGH_KICKER = "OPRK";
        public const string PEN_ROUGH_PASSER = "OPRP";
        public const string RANDOM_WEATHER = "OPRW";
        public const string PLAYER_DISPLAYS = "OPTI";
        public const string QUARTER_LENGTH = "OQLN";
        public const string ORLO = "ORLO";                          // 2007-2008
        public const string CAP_PENALTY = "OSCP";
        public const string SKILL_LEVEL = "OSLE";
        public const string RIGHT_CLICK_STATS_DIR = "OSRD";
        public const string RIGHT_CLICK_STATS_UNIQUE = "OSRF";
        public const string RIGHT_CLICK_STATS_FORMAT = "OSRM";

        public const string SIM_INJURY_ON = "OSIM";                 //  2006-2008
		public const string SIM_INJURY_FREQUENCY = "SIMI";          // 2006-2008
		
		
		public GameOptionRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public bool Offsides
        {
            get { return (GetIntField(OFFSIDES) == 1); }
            set { SetField(OFFSIDES, (value ? 1 : 0)); }
        }
        public bool Penalties
        {
            get { return (GetIntField(PENALTIES) == 1); }
            set { SetField(PENALTIES, (value ? 1 : 0)); }
        }
        
        public int InGameInjury
		{
			set
			{
				SetField(INGAME_INJURY_FREQUENCY, value);
			}
			get
			{
				return GetIntField(INGAME_INJURY_FREQUENCY);
			}
		}

        #region CPU Settings
        public int CPU_QB_Accuracy
        {
            get { return GetIntField(CPU_QB_ACC); }
            set { SetField(CPU_QB_ACC, value); }
        }
        public int CPU_Def_Awareness
        {
            get { return GetIntField(CPU_DEF_AWR); }
            set { SetField(CPU_DEF_AWR, value); }
        }
        public int CPU_Def_BreakBlock
        {
            get { return GetIntField(CPU_BRK_BLOCK); }
            set { SetField(CPU_BRK_BLOCK, value); }
        }
        public int CPU_Def_Knockdowns
        {
            get { return GetIntField(CPU_DEF_KNOCKDOWNS); }
            set { SetField(CPU_DEF_KNOCKDOWNS, value); }
        }
        public int CPU_FG_Accuracy
        {
            get { return GetIntField(CPU_FG_ACCURACY); }
            set { SetField(CPU_FG_ACCURACY, value); }
        }
        public int CPU_FG_Length
        {
            get { return GetIntField(CPU_FG_LENGTH); }
            set { SetField(CPU_FG_LENGTH, value); }
        }
        public int CPU_Def_Interceptions
        {
            get { return GetIntField(CPU_INTS); }
            set { SetField(CPU_INTS, value); }
        }
        public int CPU_KO_Length
        {
            get { return GetIntField(CPU_KO_LENGTH); }
            set { SetField(CPU_KO_LENGTH, value); }
        }
        public int CPU_PassBlocking
        {
            get { return GetIntField(CPU_PASS_BLOCK); }
            set { SetField(CPU_PASS_BLOCK, value); }
        }
        public int CPU_Punt_Accuracy
        {
            get { return GetIntField(CPU_PUNT_ACC); }
            set { SetField(CPU_PUNT_ACC, value); }
        }
        public int CPU_Punt_Length
        {
            get { return GetIntField(CPU_PUNT_LENGTH); }
            set { SetField(CPU_PUNT_LENGTH, value); }
        }
        public int CPU_RB_Ability
        {
            get { return GetIntField(CPU_RB_ABILITY); }
            set { SetField(CPU_RB_ABILITY, value); }
        }
        public int CPU_RunBlocking
        {
            get { return GetIntField(CPU_RUN_BLOCK); }
            set { SetField(CPU_RUN_BLOCK, value); }
        }
        public int CPU_WR_Catching
        {
            get { return GetIntField(CPU_CATCHING); }
            set { SetField(CPU_CATCHING, value); }
        }
        public int CPU_Tackling
        {
            get { return GetIntField(CPU_TACKLE); }
            set { SetField(CPU_TACKLE, value); }
        }
        #endregion

        #region Human Settings
        public int HUM_QB_Accuracy
        {
            get { return GetIntField(HUM_QB_ACC); }
            set { SetField(HUM_QB_ACC, value); }
        }
        public int HUM_Def_Awareness
        {
            get { return GetIntField(HUM_DEF_AWR); }
            set { SetField(HUM_DEF_AWR, value); }
        }
        public int HUM_Def_BreakBlock
        {
            get { return GetIntField(HUM_BRK_BLOCK); }
            set { SetField(HUM_BRK_BLOCK, value); }
        }
        public int HUM_Def_Knockdowns
        {
            get { return GetIntField(HUM_DEF_KNOCKDOWNS); }
            set { SetField(HUM_DEF_KNOCKDOWNS, value); }
        }
        public int HUM_FG_Accuracy
        {
            get { return GetIntField(HUM_FG_ACCURACY); }
            set { SetField(HUM_FG_ACCURACY, value); }
        }
        public int HUM_FG_Length
        {
            get { return GetIntField(HUM_FG_LENGTH); }
            set { SetField(HUM_FG_LENGTH, value); }
        }
        public int HUM_Def_Interceptions
        {
            get { return GetIntField(HUM_INTS); }
            set { SetField(HUM_INTS, value); }
        }
        public int HUM_KO_Length
        {
            get { return GetIntField(HUM_KO_LENGTH); }
            set { SetField(HUM_KO_LENGTH, value); }
        }
        public int HUM_PassBlocking
        {
            get { return GetIntField(HUM_PASS_BLOCK); }
            set { SetField(HUM_PASS_BLOCK, value); }
        }
        public int HUM_Punt_Accuracy
        {
            get { return GetIntField(HUM_PUNT_ACC); }
            set { SetField(HUM_PUNT_ACC, value); }
        }
        public int HUM_Punt_Length
        {
            get { return GetIntField(HUM_PUNT_LENGTH); }
            set { SetField(HUM_PUNT_LENGTH, value); }
        }
        public int HUM_RB_Ability
        {
            get { return GetIntField(HUM_RB_ABILITY); }
            set { SetField(HUM_RB_ABILITY, value); }
        }
        public int HUM_RunBlocking
        {
            get { return GetIntField(HUM_RUN_BLOCK); }
            set { SetField(HUM_RUN_BLOCK, value); }
        }
        public int HUM_WR_Catching
        {
            get { return GetIntField(HUM_CATCHING); }
            set { SetField(HUM_CATCHING, value); }
        }
        public int HUM_Tackling
        {
            get { return GetIntField(HUM_TACKLE); }
            set { SetField(HUM_TACKLE, value); }
        }
        #endregion

        public int CameraView
        {
            get { return GetIntField(CAMERA_VIEW); }
            set { SetField(CAMERA_VIEW, value); }
        }
        public bool AcceleratedClock
        {
            get { return (GetIntField(ACCELERATED_CLOCK) == 1); }
            set { SetField(ACCELERATED_CLOCK, (value ? 1 : 0)); }
        }
        public bool AutoReplay
        {
            get { return (GetIntField(AUTO_REPLAY) == 1); }
            set { SetField(AUTO_REPLAY, (value ? 1 : 0)); }
        }
        public bool FairPlay
        {
            get { return (GetIntField(FAIR_PLAY) == 1); }
            set { SetField(FAIR_PLAY, (value ? 1 : 0)); }
        }
        public int FieldLines
        {
            get { return GetIntField(FIELD_LINES); }
            set { SetField(FIELD_LINES, value); }
        }
        public bool Fatigue
        {
            get { return (GetIntField(FATIGUE) == 1); }
            set { SetField(FATIGUE, (value ? 1 : 0)); }
        }
        public bool Injuries
        {
            get { return (GetIntField(INJURIES) == 1); }
            set { SetField(INJURIES, (value ? 1 : 0)); }
        }
        public bool PlayClock
        {
            get { return (GetIntField(PLAY_CLOCK) == 1); }
            set { SetField(PLAY_CLOCK, (value ? 1 : 0)); }
        }
        public string FranchiseStatsDir
        {
            get { return GetStringField(FRANCHIS_LOG_DIR); }
            set { SetField(FRANCHIS_LOG_DIR, value); }
        }
        public bool FranchiseStatsOverwrite
        {
            get { return (GetIntField(FRANCHISE_LOG_UNIQUE) == 1); }
            set { SetField(FRANCHISE_LOG_UNIQUE, (value ? 1 : 0)); }
        }
        public int FranchiseStatsFormat
        {
            get { return GetIntField(FRANCHISE_LOG_FORMAT); }
            set { SetField(FRANCHISE_LOG_FORMAT, value); }
        }
        public bool SalaryCap
        {
            set
            {
                SetField(SALARY_CAP, (value ? 1 : 0));
            }
            get
            {
                return (GetIntField(SALARY_CAP) == 1);
            }
        }
        public bool TradeDeadline
        {
            set
            {
                SetField(TRADE_DEADLINE, (value ? 1 : 0));
            }
            get
            {
                return (GetIntField(TRADE_DEADLINE) == 1);
            }
        }
        public int WeatherEffects
        {
            get { return GetIntField(WEATHER_EFFECTS); }
            set { SetField(WEATHER_EFFECTS, value); }
        }
        public string ExhibitionStatsDir
        {
            get { return GetStringField(EXHIBITION_LOG_DIR); }
            set { SetField(EXHIBITION_LOG_DIR, value); }
        }
        public bool ExhibitionStatsOverwrite
        {
            get { return (GetIntField(EXHIBITION_LOG_UNIQUE) == 1); }
            set { SetField(EXHIBITION_LOG_UNIQUE, (value ? 1 : 0)); }
        }
        public int ExhibitionStatsFormat
        {
            get { return GetIntField(EXHIBITION_LOG_FORMAT); }
            set { SetField(EXHIBITION_LOG_FORMAT, value); }
        }
        public bool OwnerMode
        {
            set
            {
                SetField(OWNER_MODE, (value ? 1 : 0));
            }
            get
            {
                return (GetIntField(OWNER_MODE) == 1);
            }
        }

        #region Penalty
        public int Pen_Clipping
        {
            get { return GetIntField(PEN_CLIPPING); }
            set { SetField(PEN_CLIPPING, value); }
        }
        public int Pen_DefPassInterference
        {
            get { return GetIntField(PEN_DEF_PASS_INT); }
            set { SetField(PEN_DEF_PASS_INT, value); }
        }
        public int Pen_FaceMask
        {
            get { return GetIntField(PEN_FACEMASK); }
            set { SetField(PEN_FACEMASK, value); }
        }
        public int Pen_FalseStart
        {
            get { return GetIntField(PEN_FALSE_START); }
            set { SetField(PEN_FALSE_START, value); }
        }
        public int Pen_Holding
        {
            get { return GetIntField(PEN_HOLDING); }
            set { SetField(PEN_HOLDING, value); }
        }
        public int Pen_Grounding
        {
            get { return GetIntField(PEN_GROUNDING); }
            set { SetField(PEN_GROUNDING, value); }
        }
        public int Pen_OffPassInterference
        {
            get { return GetIntField(PEN_OFF_PASS_INT); }
            set { SetField(PEN_OFF_PASS_INT, value); }
        }
        public int Pen_offsides
        {
            get { return GetIntField(OFFSIDES); }
            set { SetField(OFFSIDES, value); }
        }
        public bool Pen_PenaltiesOnOff
        {
            get { return (GetIntField(PENALTIES) == 1); }
            set { SetField(PENALTIES, (value ? 1 : 0)); }
        }
        public int Pen_CatchInterference
        {
            get { return GetIntField(PEN_PK_INT); }
            set { SetField(PEN_PK_INT, value); }
        }
        public int Pen_RoughPasser
        {
            get { return GetIntField(PEN_ROUGH_PASSER); }
            set { SetField(PEN_ROUGH_PASSER, value); }
        }
        public int Pen_RoughKicker
        {
            get { return GetIntField(PEN_ROUGH_KICKER); }
            set { SetField(PEN_ROUGH_KICKER, value); }
        }

        #endregion

        public int Playmode
        {
            get { return GetIntField(PLAY_MODE); }
            set { SetField(PLAY_MODE, value); }
        }
        public bool RandomWeather
        {
            get { return (GetIntField(RANDOM_WEATHER) == 1); }
            set { SetField(RANDOM_WEATHER, (value ? 1 : 0)); }
        }
        public int PlayerDisplays
        {
            get { return GetIntField(PLAYER_DISPLAYS); }
            set { SetField(PLAYER_DISPLAYS, value); }
        }
        public int QuarterLength
        {
            get { return GetIntField(QUARTER_LENGTH); }
            set { SetField(QUARTER_LENGTH, value); }
        }
        public bool SalaryCapPenalty
        {
            set
            {
                SetField(CAP_PENALTY, (value ? 1 : 0));
            }
            get
            {
                return (GetIntField(CAP_PENALTY) == 1);
            }
        }
        public int SimInjury_Freq
		{
			set
			{
				SetField(SIM_INJURY_FREQUENCY, value);
			}
			get
			{
				return GetIntField(SIM_INJURY_FREQUENCY);
			}
		}
        public bool SimInjuryON
        {
            get { return (GetIntField(SIM_INJURY_ON) == 1); }
            set { SetField(SIM_INJURY_ON, (value ? 1 : 0)); }
        }
        
        public int SkillLevel
        {
            get { return GetIntField(SKILL_LEVEL); }
            set { SetField(SKILL_LEVEL, value); }
        }
        public string RightClickStatsDir
        {
            get { return GetStringField(RIGHT_CLICK_STATS_DIR); }
            set { SetField(RIGHT_CLICK_STATS_DIR, value); }
        }
        public bool RightClickStatsOverwrite
        {
            get { return (GetIntField(RIGHT_CLICK_STATS_UNIQUE) == 1); }
            set { SetField(RIGHT_CLICK_STATS_UNIQUE, (value ? 1 : 0)); }
        }
        public int RightClickStatsFormat
        {
            get { return GetIntField(RIGHT_CLICK_STATS_FORMAT); }
            set { SetField(RIGHT_CLICK_STATS_FORMAT, value); }
        }
		
		
	}
}
