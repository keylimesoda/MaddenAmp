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
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    class BoxScoreTeamStats : TableRecordModel
    {
        public const string WEEK_NUMBER = "SEWN";
        public const string WEEKTYPE = "SEWT";
        public const string SEASON = "SEYR";
        public const string GAME_NUMBER = "SGNM";
        //public const string T31C = "t31c";
        //public const string T31Y = "t31y";
        //public const string T33C = "t33c";
        //public const string T33Y = "t33y";
        //public const string T34C = "t34c";
        //public const string T34Y = "t34y";
        public const string TEAM_ID = "TGID";
        public const string OFFENSE_FIRST_DOWNS = "ts1d";
        //public const string TS1L = "ts1l";
        //public const string TS1P = "ts1p";
        //public const string TS1R = "ts1r";
        //public const string TS1Y = "ts1y";
        public const string OFFENSE_2PT_ATT = "ts2a";
        public const string OFFENSE_2PT_CONV = "ts2c";
        //public const string TS2P = "ts2p";
        //public const string TS2R = "ts2r";
        public const string OFFENSE_3RD_CONV = "ts3c";
        public const string OFFENSE_3RD_ATT = "ts3d";
        //public const string TS3P = "ts3p";
        //public const string TS3R = "ts3r";
        public const string OFFENSE_4TH_CONV = "ts4c";
        public const string OFFENSE_4TH_ATT = "ts4d";
        //public const string TS4F = "ts4f";
        //public const string TS4P = "ts4p";
        //public const string TS4R = "ts4r";
        //public const string TSBZ = "tsbz";
        //public const string TSDD = "tsdd";
        public const string DEFENSE_REDZONE_FG = "tsdf";
        public const string DEFENSIVE_INT = "tsDi";
        public const string DEFENSE_REDZONE_ATT = "tsdr";
        public const string DEFENSE_REDZONE_TD = "tsdt";
        //public const string TSFA = "tsfa";
        public const string OFFENSE_FUMBLES_LOST = "tsfl";
        //public const string TSFM = "tsfm";
        public const string DEFENSE_FUMBLES_RECOVERED = "tsfr";
        public const string OFFENSE_FUMBLES = "tsfu";
        public const string OFFENSE_TURNOVERS = "tsga";
        //public const string TSHR = "tshr";
        public const string KICK_RET_YARDS = "tskr";
        //public const string TSNH = "tsnh";
        //public const string TSOA = "tsoa";
        public const string OFFENSE_REDZONE_FG = "tsof";
        public const string OFFENSE_PASS_YARDS = "tsop";
        public const string OFFENSE_RUSH_YARDS = "tsor";
        public const string OFFENSE_REDZONE_TD = "tsot";
        public const string OFFENSE_YARDS = "tsoy";
        public const string OFFENSE_REDZONE_ATT = "tsoz";
        public const string OFFENSE_PASS_ATT = "tspa";
        public const string OFFENSE_PASS_COMP = "tspc";
        public const string DEFENSE_PASS_YARDS = "tsPd";
        //public const string Tspd = "tspd";
        public const string PENALTIES = "tspe";
        //public const string DEFENSE_INT = "tsPi";
        public const string OFFENSE_PASS_INT = "tspi";
        //public const string TSPO = "tspo";
        public const string PUNT_RET_YARDS = "tspr";
        //public const string TSPS = "tsps";
        public const string POSSESSION_TIME = "tspt";
        public const string OFFENSE_PASS_TDS = "tsPt";
        public const string PUNTS = "tspu";
        public const string PENALTY_YARDS = "tsPy";
        //public const string TSQH = "tsqh";
        //public const string TSQK = "tsqk";
        //public const string TSQS = "tsqs";
        public const string OFFENSE_RUSH_ATT = "tsra";
        public const string OFFENSE_RUSH_TD = "tsrt";        
        //public const string TSSF = "tssf";
        //public const string TSSG = "tssg";
        public const string DEFENSE_SACKS = "tssk";
        //public const string TSST = "tsst";
        public const string DEFENSE_TURNOVERS = "tsta";
        public const string TOTAL_YARDS = "tsTy";
        //public const string Tsty = "tsty";                      // NOT USED
           

        public BoxScoreTeamStats(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }


        public int WeekNumber
        {
            get
            {
                return GetIntField(WEEK_NUMBER);
            }
            set
            {
                SetField(WEEK_NUMBER, value);
            }
        }
        public int WeekType
        {
            get { return GetIntField(WEEKTYPE); }
            set { SetField(WEEKTYPE, value); }
        }
        public int Season
        {
            get
            {
                return GetIntField(SEASON);
            }
            set
            {
                SetField(SEASON, value);
            }
        }
        public int GameNumber
        {
            get { return GetIntField(GAME_NUMBER); }
            set { SetField(GAME_NUMBER, value); }
        }

        public int TeamId
        {
            get
            {
                return GetIntField(TEAM_ID);
            }
            set
            {
                SetField(TEAM_ID, value);
            }
        }        
        public int OffenseFirstDowns
        {
            get
            {
                return GetIntField(OFFENSE_FIRST_DOWNS);
            }
            set
            {
                SetField(OFFENSE_FIRST_DOWNS, value);
            }
        }

        public int Offense2ptAtt
        {
            get { return GetIntField(OFFENSE_2PT_ATT); }
            set { SetField(OFFENSE_2PT_ATT, value); }
        }
        public int Offense2ptConv
        {
            get { return GetIntField(OFFENSE_2PT_CONV); }
            set { SetField(OFFENSE_2PT_CONV, value); }
        }

        public int Offense3rd_Conv
        {
            get
            {
                return GetIntField(OFFENSE_3RD_CONV);
            }
            set
            {
                SetField(OFFENSE_3RD_CONV, value);
            }
        }
        public int Offense3rd_Att
        {
            get
            {
                return GetIntField(OFFENSE_3RD_ATT);
            }
            set
            {
                SetField(OFFENSE_3RD_ATT, value);
            }
        }

        public int Offense4th_Conv
        {
            get
            {
                return GetIntField(OFFENSE_4TH_CONV);
            }
            set
            {
                SetField(OFFENSE_4TH_CONV, value);
            }
        }
        public int Offense4th_Att
        {
            get
            {
                return GetIntField(OFFENSE_4TH_ATT);
            }
            set
            {
                SetField(OFFENSE_4TH_ATT, value);
            }
        }

        public int DefenseRedzoneFG
        {
            get { return GetIntField(DEFENSE_REDZONE_FG); }
            set { SetField(DEFENSE_REDZONE_FG, value); }
        }
        public int DefenseInt
        {
            get
            {
                return GetIntField(DEFENSIVE_INT);
            }
            set
            {
                SetField(DEFENSIVE_INT, value);
            }
        }
        public int DefenseRedzoneAtt
        {
            get { return GetIntField(DEFENSE_REDZONE_ATT); }
            set { SetField(DEFENSE_REDZONE_ATT, value); }
        }
        public int DefenseRedzoneTD
        {
            get { return GetIntField(DEFENSE_REDZONE_TD); }
            set { SetField(DEFENSE_REDZONE_TD, value); }
        }

        public int OffenseFumblesLost
        {
            get
            {
                return GetIntField(OFFENSE_FUMBLES_LOST);
            }
            set
            {
                SetField(OFFENSE_FUMBLES_LOST, value);
            }
        }

        public int DefenseFumblesRecovered
        {
            get
            {
                return GetIntField(DEFENSE_FUMBLES_RECOVERED);
            }
            set
            {
                SetField(DEFENSE_FUMBLES_RECOVERED, value);
            }
        }
        public int OffenseFumbles
        {
            get { return GetIntField(OFFENSE_FUMBLES); }
            set { SetField(OFFENSE_FUMBLES, value); }
        }
        public int OffenseTurnovers
        {
            get { return GetIntField(OFFENSE_TURNOVERS); }
            set { SetField(OFFENSE_TURNOVERS, value); }
        }

        public int KickRetYards
        {
            get { return GetIntField(KICK_RET_YARDS); }
            set { SetField(KICK_RET_YARDS, value); }
        }

        public int OffenseRedzoneFG
        {
            get { return GetIntField(OFFENSE_REDZONE_FG); }
            set { SetField(OFFENSE_REDZONE_FG, value); }
        }
        public int OffensePassYards
        {
            get
            {
                return GetIntField(OFFENSE_PASS_YARDS);
            }
            set
            {
                SetField(OFFENSE_PASS_YARDS, value);
            }
        }
        public int OffenseRushYards
        {
            get
            {
                return GetIntField(OFFENSE_RUSH_YARDS);
            }
            set
            {
                SetField(OFFENSE_RUSH_YARDS, value);
            }
        }
        public int OffenseRedzoneTD
        {
            get { return GetIntField(OFFENSE_REDZONE_TD); }
            set { SetField(OFFENSE_REDZONE_TD, value); }
        }
        public int OffenseYards
        {
            get
            {
                return GetIntField(OFFENSE_YARDS);
            }
            set
            {
                SetField(OFFENSE_YARDS, value);
            }
        }
        public int OffenseRedzoneAtt
        {
            get { return GetIntField(OFFENSE_REDZONE_ATT); }
            set { SetField(OFFENSE_REDZONE_ATT, value); }
        }
        public int OffensePassAtt
        {
            get { return GetIntField(OFFENSE_PASS_ATT); }
            set { SetField(OFFENSE_PASS_ATT, value); }
        }
        public int OffensePassComp
        {
            get { return GetIntField(OFFENSE_PASS_COMP); }
            set { SetField(OFFENSE_PASS_COMP, value); }
        }
        public int DefensePassYards
        {
            get { return GetIntField(DEFENSE_PASS_YARDS); }
            set { SetField(DEFENSE_PASS_YARDS, value); }
        }

        public int Penalties
        {
            get { return GetIntField(PENALTIES); }
            set { SetField(PENALTIES, value); }
        }

        public int OffensePassInt
        {
            get
            {
                return GetIntField(OFFENSE_PASS_INT);
            }
            set
            {
                SetField(OFFENSE_PASS_INT, value);
            }
        }

        public int PuntRetYards
        {
            get { return GetIntField(PUNT_RET_YARDS); }
            set { SetField(PUNT_RET_YARDS, value); }
        }

        public int PossessionTime
        {
            get { return GetIntField(POSSESSION_TIME); }
            set { SetField(POSSESSION_TIME, value); }
        }
        public int OffensePassingTDs
        {
            get
            {
                return GetIntField(OFFENSE_PASS_TDS);
            }
            set
            {
                SetField(OFFENSE_PASS_TDS, value);
            }
        }
        public int Punts
        {
            get { return GetIntField(PUNTS); }
            set { SetField(PUNTS, value); }
        }
        public int PenaltyYds
        {
            get { return GetIntField(PENALTY_YARDS); }
            set { SetField(PENALTY_YARDS, value); }
        }               
                
        public int OffenseRushAtt
        {
            get
            {
                return GetIntField(OFFENSE_RUSH_ATT);
            }
            set
            {
                SetField(OFFENSE_RUSH_ATT, value);
            }
        }
        public int OffenseRushTDs
        {
            get
            {
                return GetIntField(OFFENSE_RUSH_TD);
            }
            set
            {
                SetField(OFFENSE_RUSH_TD, value);
            }
        }

        public int DefenseSacks
        {
            get
            {
                return GetIntField(DEFENSE_SACKS);
            }
            set
            {
                SetField(DEFENSE_SACKS, value);
            }
        }

        public int DefenseTurnovers
        {
            get { return GetIntField(DEFENSE_TURNOVERS); }
            set { SetField(DEFENSE_TURNOVERS, value); }
        }        
        public int TotalYards
        {
            get
            {
                return GetIntField(TOTAL_YARDS);
            }
            set
            {
                SetField(TOTAL_YARDS, value);
            }
        }
         
        
        
        
    }
}
