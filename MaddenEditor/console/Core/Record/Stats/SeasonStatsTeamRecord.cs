/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005, 2006 MaddenWishlist.com and spin16
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
    public class SeasonStatsTeamRecord : TableRecordModel
    {
        // tsse        
        
        public const string TEAM_ID = "TGID";
        public const string FIRST_DOWNS = "ts1d";
        public const string OFFENSE_2PT_ATT = "ts2a";
        public const string OFFENSE_2PT_CONV = "ts2c"; 
        public const string THIRD_DOWN_CONVERSIONS = "ts3c";
        public const string THIRD_DOWN_ATTEMPTS = "ts3d";
        public const string FOURTH_DOWN_CONVERSIONS = "ts4c";
        public const string FOURTH_DOWN_ATTEMPTS = "ts4d";  
        public const string DEFENSE_REDZONE_FG = "tsdf";
        public const string DEFENSIVE_INT = "tsDi";
        public const string DEFENSE_PASS_YDS = "tsdp";
        public const string DEFENSE_REDZONE_ATT = "tsdr";
        public const string DEFENSE_REDZONE_TD = "tsdt";
        public const string DEFENSE_RUSH_YDS = "tsdy";
        public const string FUMBLES_LOST = "tsfl";        
        public const string FUMBLES_RECOVERED = "tsfr";       
        public const string OFFENSE_TURNOVERS = "tsga";        
        public const string OFFENSE_REDZONE_FG = "tsof";
        public const string OFFENSE_PASS_YDS = "tsop";
        public const string OFFENSE_RUSH_YARDS = "tsor";
        public const string OFFENSE_REDZONE_TD = "tsot";
        public const string OFFENSE_YARDS = "tsoy";
        public const string OFFENSE_REDZONE_ATT = "tsoz";
        public const string OFFENSE_PASS_ATT = "tspa";
        // tspd not used
        public const string PENALTIES = "tspe";
        //   tsPi not used
        public const string OFFENSE_PASS_INT = "tspi";  
        public const string OFFENSE_PASS_TDS = "tsPt";        
        public const string PENALTY_YARDS = "tsPy"; 
       
        public const string OFFENSE_RUSH_ATT = "tsra";
        public const string OFFENSE_RUSH_TD = "tsrt";
        public const string OFFENSE_SACKS_ALLOWED = "tssa"; 
        public const string DEFENSE_SACKS = "tssk";
        public const string DEFENSE_TURNOVERS = "tsta";
        public const string TOTAL_YARDS = "tsTy";
        // tsty not used
           

        public SeasonStatsTeamRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

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
        public int FirstDowns
        {
            get
            {
                return GetIntField(FIRST_DOWNS);
            }
            set
            {
                SetField(FIRST_DOWNS, value);
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
        public int ThirdDownAttempts
        {
            get
            {
                return GetIntField(THIRD_DOWN_ATTEMPTS);
            }
            set
            {
                SetField(THIRD_DOWN_ATTEMPTS, value);
            }
        }
        public int ThirdDownConversions
        {
            get
            {
                return GetIntField(THIRD_DOWN_CONVERSIONS);
            }
            set
            {
                SetField(THIRD_DOWN_CONVERSIONS, value);
            }
        }
        public int FourthDownAttempts
        {
            get
            {
                return GetIntField(FOURTH_DOWN_ATTEMPTS);
            }
            set
            {
                SetField(FOURTH_DOWN_ATTEMPTS, value);
            }
        }
        public int FourthDownConversions
        {
            get
            {
                return GetIntField(FOURTH_DOWN_CONVERSIONS);
            }
            set
            {
                SetField(FOURTH_DOWN_CONVERSIONS, value);
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
        public int DefensePassYds
        {
            get { return GetIntField(DEFENSE_PASS_YDS); }
            set { SetField(DEFENSE_PASS_YDS, value); }
        }
        public int DefenseRushYds
        {
            get { return GetIntField(DEFENSE_RUSH_YDS); }
            set { SetField(DEFENSE_RUSH_YDS, value); }
        }
        public int defenseRedzoneAtt
        {
            get { return GetIntField(DEFENSE_REDZONE_ATT); }
            set { SetField(DEFENSE_REDZONE_ATT, value); }
        }
        public int DefenseRedzoneTD
        {
            get { return GetIntField(DEFENSE_REDZONE_TD); }
            set { SetField(DEFENSE_REDZONE_TD, value); }
        }       
        public int FumblesLost
        {
            get
            {
                return GetIntField(FUMBLES_LOST);
            }
            set
            {
                SetField(FUMBLES_LOST, value);
            }
        }
        public int FumblesRecovered
        {
            get
            {
                return GetIntField(FUMBLES_RECOVERED);
            }
            set
            {
                SetField(FUMBLES_RECOVERED, value);
            }
        }                
        public int OffenseTurnovers
        {
            get { return GetIntField(OFFENSE_TURNOVERS); }
            set { SetField(OFFENSE_TURNOVERS, value); }
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
                return GetIntField(OFFENSE_PASS_YDS);
            }
            set
            {
                SetField(OFFENSE_PASS_YDS, value);
            }
        }
        public int RushingYards
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
        public int OffensePassTDs
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
        public int PenaltyYds
        {
            get { return GetIntField(PENALTY_YARDS); }
            set { SetField(PENALTY_YARDS, value); }
        }               
        public int RushingAttempts
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
        public int RushingTDs
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
        public int SacksAllowed
        {
            get
            {
                return GetIntField(OFFENSE_SACKS_ALLOWED);
            }
            set
            {
                SetField(OFFENSE_SACKS_ALLOWED, value);
            }
        }        
        public int Sacks
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
