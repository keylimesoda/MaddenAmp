/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2014 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;


using MaddenEditor.Forms;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;

namespace MaddenEditor.Core
{
    #region Sub Classes for Players
        
    public class Player_Ratings
    {
        #region Members

        #region Player Record Members

        public int ACCELERATION = 50;                   //"PACC";        
        public int AGILITY = 50;                        //"PAGI";
        public int AWARENESS = 50;                      //"PAWR";
        public int BREAK_TACKLE = 50;                   //"PBTK";
        public int CARRYING = 50;                       //"PCAR";
        public int CATCHING = 50;                       //"PCTH";
        public int EGO = 70;                            //"PEGO";                       // 2007-2008
        public int INJURY = 50;                         //"PINJ";
        public int JUMPING = 50;                        //"PJMP";
        public int KICK_ACCURACY = 50;                  //"PKAC";
        public int KICK_POWER = 50;                     //"PKPR";
        public int KICK_RETURN = 50;                    //"PKRT";
        public int MORALE = 50;                         //"PMOR";
        public int PASS_BLOCKING = 50;                  //"PPBK";
        public int RUN_BLOCKING = 50;                   //"PRBK";       
        public int SPEED = 50;                          //"PSPD";
        public int STAMINA = 50;                        //"PSTA";
        public int STRENGTH = 50;                       //"PSTR"; 
        public int TACKLE = 50;                         //"PTAK";
        public int TOUGHNESS = 50;                      //"PTGH";
        public int THROW_ACCURACY = 50;                 //"PTHA";
        public int THROW_POWER = 50;                    //"PTHP";

        #endregion

        #region Custom Members

        public int Streak = -1;
        public int WorkEthic = -1;
        public int character = -1;
        public double self_overall = 60;

        #endregion

        #endregion


        #region Constructors

        public Player_Ratings()
        {
            ACCELERATION = 50;                   //"PACC";        
            AGILITY = 50;                        //"PAGI";
            AWARENESS = 50;                      //"PAWR";
            BREAK_TACKLE = 50;                   //"PBTK";
            CARRYING = 50;                       //"PCAR";
            CATCHING = 50;                       //"PCTH";
            EGO = 70;                            //"PEGO";                       // 2007-2008
            INJURY = 50;                         //"PINJ";
            JUMPING = 50;                        //"PJMP";
            KICK_ACCURACY = 50;                  //"PKAC";
            KICK_POWER = 50;                     //"PKPR";
            KICK_RETURN = 50;                    //"PKRT";
            MORALE = 50;                         //"PMOR";
            PASS_BLOCKING = 50;                  //"PPBK";
            RUN_BLOCKING = 50;                   //"PRBK";       
            SPEED = 50;                          //"PSPD";
            STAMINA = 50;                        //"PSTA";
            STRENGTH = 50;                       //"PSTR"; 
            TACKLE = 50;                         //"PTAK";
            TOUGHNESS = 50;                      //"PTGH";
            THROW_ACCURACY = 50;                 //"PTHA";
            THROW_POWER = 50;                    //"PTHP";
           
            Streak = -1;                        // Custom
            WorkEthic = -1;                     // Custom
            character = -1;                     // Custom
            self_overall = 60;                  // Custom
        }

        public Player_Ratings(PlayerRecord rec)
        {
            ACCELERATION = rec.Acceleration;
            AGILITY = rec.Agility;
            AWARENESS = rec.Awareness;
            BREAK_TACKLE = rec.BreakTackle;
            CARRYING = rec.Carrying;
            CATCHING = rec.Catching;
            EGO = rec.Ego;
            INJURY = rec.Injury;
            JUMPING = rec.Jumping;
            KICK_ACCURACY = rec.KickAccuracy;
            KICK_POWER = rec.KickPower;
            KICK_RETURN = rec.KickReturn;
            MORALE = rec.Morale;
            PASS_BLOCKING = rec.PassBlocking;
            RUN_BLOCKING = rec.RunBlocking;
            SPEED = rec.Speed;
            STAMINA = rec.Stamina;
            STRENGTH = rec.Strength;
            TACKLE = rec.Tackle;
            THROW_ACCURACY = rec.ThrowAccuracy;
            THROW_POWER = rec.ThrowPower;
            TOUGHNESS = rec.Toughness;
        }

        #endregion

        #region Methods

        public void UpdatePlayerRecord(PlayerRecord rec)
        {
            rec.Acceleration = this.ACCELERATION;
            rec.Agility = this.AGILITY;
            rec.Awareness = this.AWARENESS;
            rec.BreakTackle = this.BREAK_TACKLE;
            rec.Carrying = this.CARRYING;
            rec.Catching = this.CATCHING;
            rec.Ego = this.EGO;
            rec.Injury = this.INJURY;
            rec.Jumping = this.JUMPING;
            rec.KickAccuracy = this.KICK_ACCURACY;
            rec.KickPower = this.KICK_POWER;
            rec.KickReturn = this.KICK_RETURN;
            rec.Morale = this.MORALE;
            rec.PassBlocking = this.PASS_BLOCKING;
            rec.RunBlocking = this.RUN_BLOCKING;
            rec.Speed = this.SPEED;
            rec.Stamina = this.STAMINA;
            rec.Strength = this.STRENGTH;
            rec.Tackle = this.TACKLE;
            rec.ThrowAccuracy = this.THROW_ACCURACY;
            rec.ThrowPower = this.THROW_POWER;
            rec.Toughness = this.TOUGHNESS;
        }

        #endregion

        #region File IO

        public void Read(BinaryReader binreader)
        {
            #region Player Record Members

            ACCELERATION  = binreader.ReadInt32(); 
            AGILITY  = binreader.ReadInt32(); 
            AWARENESS  = binreader.ReadInt32(); 
            BREAK_TACKLE  = binreader.ReadInt32(); 
            CARRYING  = binreader.ReadInt32(); 
            CATCHING  = binreader.ReadInt32(); 
            EGO  = binreader.ReadInt32(); 
            INJURY  = binreader.ReadInt32(); 
            JUMPING  = binreader.ReadInt32(); 
            KICK_ACCURACY  = binreader.ReadInt32(); 
            KICK_POWER  = binreader.ReadInt32(); 
            KICK_RETURN  = binreader.ReadInt32(); 
            MORALE  = binreader.ReadInt32(); 
            PASS_BLOCKING  = binreader.ReadInt32(); 
            RUN_BLOCKING  = binreader.ReadInt32(); 
            SPEED  = binreader.ReadInt32(); 
            STAMINA  = binreader.ReadInt32(); 
            STRENGTH  = binreader.ReadInt32(); 
            TACKLE  = binreader.ReadInt32(); 
            THROW_ACCURACY  = binreader.ReadInt32(); 
            THROW_POWER  = binreader.ReadInt32(); 
            TOUGHNESS  = binreader.ReadInt32();

            #endregion

            //  Custom fields
            Streak = binreader.ReadInt32();
            WorkEthic = binreader.ReadInt32();
            character = binreader.ReadInt32();
            self_overall = binreader.ReadDouble();
        }
        public void Write(BinaryWriter binwriter)
        {
            #region Player Record Members

            binwriter.Write(ACCELERATION);
            binwriter.Write(AGILITY);
            binwriter.Write(AWARENESS);
            binwriter.Write(BREAK_TACKLE);
            binwriter.Write(CARRYING);
            binwriter.Write(CATCHING);
            binwriter.Write(EGO);
            binwriter.Write(INJURY);
            binwriter.Write(JUMPING);
            binwriter.Write(KICK_ACCURACY);
            binwriter.Write(KICK_POWER);
            binwriter.Write(KICK_RETURN);
            binwriter.Write(MORALE);
            binwriter.Write(PASS_BLOCKING);
            binwriter.Write(RUN_BLOCKING);
            binwriter.Write(SPEED);
            binwriter.Write(STAMINA);
            binwriter.Write(STRENGTH);
            binwriter.Write(TACKLE);
            binwriter.Write(THROW_ACCURACY);
            binwriter.Write(THROW_POWER);
            binwriter.Write(TOUGHNESS);

            #endregion

            //  Custom fields 
            binwriter.Write(Streak);
            binwriter.Write(WorkEthic);
            binwriter.Write(character);
            binwriter.Write(self_overall);
        }

        #endregion
    }

    public class Potential : Player_Ratings
    {
    }

    public class Prog_Rate
    {
        public double ACCELERATION = 50;                   //"PACC";        
        public double AGILITY = 50;                        //"PAGI";
        public double AWARENESS = 50;                      //"PAWR";
        public double BREAK_TACKLE = 50;                   //"PBTK";
        public double CARRYING = 50;                       //"PCAR";
        public double CATCHING = 50;                       //"PCTH";
        public double EGO = 50;                            //"PEGO";                       // 2007-2008
        public double INJURY = 50;                         //"PINJ";
        public double JUMPING = 50;                        //"PJMP";
        public double KICK_ACCURACY = 50;                  //"PKAC";
        public double KICK_POWER = 50;                     //"PKPR";
        public double KICK_RETURN = 50;                    //"PKRT";
        public double MORALE = 50;                         //"PMOR";
        public double PASS_BLOCKING = 50;                  //"PPBK";
        public double RUN_BLOCKING = 50;                   //"PRBK";       
        public double SPEED = 50;                          //"PSPD";
        public double STAMINA = 50;                        //"PSTA";
        public double STRENGTH = 50;                       //"PSTR"; 
        public double TACKLE = 50;                         //"PTAK";
        public double TOUGHNESS = 50;                      //"PTGH";
        public double THROW_ACCURACY = 50;                 //"PTHA";
        public double THROW_POWER = 50;                    //"PTHP";

        public Prog_Rate()
        {
        }
        
        public void Read(BinaryReader binreader)
        {
            ACCELERATION = binreader.ReadDouble();
            AGILITY = binreader.ReadDouble();
            AWARENESS = binreader.ReadDouble();
            BREAK_TACKLE = binreader.ReadDouble();
            CARRYING = binreader.ReadDouble();
            CATCHING = binreader.ReadDouble();
            EGO = binreader.ReadDouble();
            INJURY = binreader.ReadDouble();
            JUMPING = binreader.ReadDouble();
            KICK_ACCURACY = binreader.ReadDouble();
            KICK_POWER = binreader.ReadDouble();
            KICK_RETURN = binreader.ReadDouble();
            MORALE = binreader.ReadDouble();
            PASS_BLOCKING = binreader.ReadDouble();
            RUN_BLOCKING = binreader.ReadDouble();
            SPEED = binreader.ReadDouble();
            STAMINA = binreader.ReadDouble();
            STRENGTH = binreader.ReadDouble();
            TACKLE = binreader.ReadDouble();
            THROW_ACCURACY = binreader.ReadDouble();
            THROW_POWER = binreader.ReadDouble();
            TOUGHNESS = binreader.ReadDouble(); 
        }
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(ACCELERATION);
            binwriter.Write(AGILITY);
            binwriter.Write(AWARENESS);
            binwriter.Write(BREAK_TACKLE);
            binwriter.Write(CARRYING);
            binwriter.Write(CATCHING);
            binwriter.Write(EGO);
            binwriter.Write(INJURY);
            binwriter.Write(JUMPING);
            binwriter.Write(KICK_ACCURACY);
            binwriter.Write(KICK_POWER);
            binwriter.Write(KICK_RETURN);
            binwriter.Write(MORALE);
            binwriter.Write(PASS_BLOCKING);
            binwriter.Write(RUN_BLOCKING);
            binwriter.Write(SPEED);
            binwriter.Write(STAMINA);
            binwriter.Write(STRENGTH);
            binwriter.Write(TACKLE);
            binwriter.Write(THROW_ACCURACY);
            binwriter.Write(THROW_POWER);
            binwriter.Write(TOUGHNESS);
        }

    }
        
    public class CareerStats
    {
        #region members

        // Common and Games Played        
        public int DOWNS_PLAYED = 0;
        public int GAMES_PLAYED = 0;
        public int GAMES_STARTED = 0;

        //QB Stats
        public int PASS_ATT = 0;     //"caat";
        public int PASS_COMP = 0;     //"cacm";
        public int PASS_INT = 0;     //"cain";
        public int PASS_LONG = 0;     //"calN";
        public int PASS_SACKED = 0;     //"casa";
        public int PASS_YDS = 0;     //"caya";
        public int PASS_TDS = 0;     //"catd";

        // WR Stats
        public int RECEIVING_RECS = 0;     //"ccca";
        public int RECEIVING_DROPS = 0;     //"ccdr";
        public int RECEIVING_TDS = 0;     //"cctd";
        public int RECEIVING_YARDS = 0;     //"ccya";
        public int RECEIVING_YAC = 0;     //"ccyc";
        public int RECEIVING_LONG = 0;     //"ccrL";

        // RB Stats
        public int RUSHING_TDS = 0;     //"cutd";
        public int RUSHING_LONG = 0;     //"culN";
        public int FUMBLES = 0;     //"cufu";
        public int RUSHING_ATTEMPTS = 0;     //"cuat";
        public int RUSHING_YARDS = 0;     //"cuya";
        public int RUSHING_YAC = 0;     //"cuyh";
        public int RUSHING_20 = 0;     //"cu2y";
        public int RUSHING_BT = 0;     //"cubt";

        // Defense
        public int PASSES_DEFENDED = 0;     //"cdpd";
        public int TACKLES = 0;     //"cdta";
        public int TACKLES_FOR_LOSS = 0;     //"cdtl";
        public int BLOCKS = 0;     //"clbl";
        public int FUMBLES_FORCED = 0;     //"clff";
        public int FUMBLES_RECOVERED = 0;     //"clfr";
        public int FUMBLES_TD = 0;     //"clft";
        public int FUMBLE_YARDS = 0;     //"clfy";
        public int SAFETIES = 0;     //"clsa";
        public int SACKS = 0;     //"clsk";
        public int INTERCEPTIONS = 0;     //"csin";
        public int INTERCEPTION_YARDS = 0;     //"csiy";
        public int INTERCEPTION_LONG = 0;     //"cslR";
        public int INTERCEPTION_TD = 0;     //"csit";

        // O-Line
        public int PANCAKES = 0;     //"copa";
        public int SACKS_ALLOWED = 0;     //"cosa";  

        // PuntKick Return
        public int KICK_RETURN_ATT = 0;     //"crka";
        public int KICK_RETURN_LONG = 0;     //"crkL";
        public int KICK_RETURN_TD = 0;     //"crkt";
        public int KICK_RETURN_YARDS = 0;     //"crky";
        public int PUNT_RETURN_ATT = 0;     //"crpa";
        public int PUNT_RETURN_LONG = 0;     //"crpL";
        public int PUNT_RETURN_TD = 0;     //"crpt";
        public int PUNT_RETURN_YARDS = 0;     //"crpy";

        // PuntKick
        // Kicker
        public int FGA = 0;     //"ckfa";
        public int FGM = 0;     //"ckfm";
        public int FG_BLOCKED = 0;     //"ckfb";
        public int FGL = 0;     //"ckfL";
        public int XPA = 0;     //"ckea";
        public int XPM = 0;     //"ckem";
        public int XP_BLOCKED = 0;     //"ckeb";
        public int FGA_129 = 0;     //"ckaa";
        public int FGA_3039 = 0;     //"ckac";
        public int FGA_4049 = 0;     //"ckad";
        public int FGA_50 = 0;     //"ckae";
        public int FGM_129 = 0;     //"ckma";
        public int FGM_3039 = 0;     //"ckmc";
        public int FGM_4049 = 0;     //"ckmd";
        public int FGM_50 = 0;     //"ckme";
        public int KICK_OFFS = 0;     //"cknk";
        public int TOUCHBACKS = 0;     //"cktb";
        // Punter stats
        public int PUNT_ATT = 0;     //"cpat";
        public int PUNT_YDS = 0;     //"cpya";
        public int PUNT_BLOCKED = 0;     //"cpbl";
        public int PUNT_LONG = 0;     //"cpIN";
        public int PUNT_NY = 0;     //"cpny";
        public int PUNT_IN20 = 0;     //"cppt";
        public int PUNT_TB = 0;     //"cptb";

        #endregion

        public CareerStats()
        {
        }

        public void SetCareerStats(CareerGamesPlayedRecord rec)
        {            
            this.DOWNS_PLAYED = rec.DownsPlayed;        // "cgdp";
            this.GAMES_PLAYED = rec.GamesPlayed;        // "cgmp";
            this.GAMES_STARTED = rec.GamesStarted;      // "cgms";
        }

        public void SetCareerStats(CareerStatsOffenseRecord rec)
        {
            //QB Stats
            this.PASS_ATT = rec.Pass_att;                      //"caat";
            this.PASS_COMP = rec.Pass_comp;                    //"cacm";
            this.PASS_INT = rec.Pass_int;                      //"cain";
            this.PASS_LONG = rec.Pass_long;                    //"calN";
            this.PASS_SACKED = rec.Pass_sacked;                //"casa";
            this.PASS_YDS = rec.Pass_yds;                      //"caya";
            this.PASS_TDS = rec.Pass_tds;                      //"catd";

            // WR Stats
            this.RECEIVING_RECS = rec.Receiving_recs;          //"ccca";
            this.RECEIVING_DROPS = rec.Receiving_drops;        //"ccdr";
            this.RECEIVING_TDS = rec.Receiving_tds;            //"cctd";
            this.RECEIVING_YARDS = rec.Receiving_yards;        //"ccya";
            this.RECEIVING_YAC = rec.Receiving_yac;            //"ccyc";
            this.RECEIVING_LONG = rec.Receiving_long;          //"ccrL";

            //RB Stats
            this.RUSHING_TDS = rec.Rushing_tds;                //"cutd";
            this.RUSHING_LONG = rec.Rushing_long;              //"culN";
            this.FUMBLES = rec.Fumbles;                        //"cufu";
            this.RUSHING_ATTEMPTS = rec.RushingAttempts;       //"cuat";
            this.RUSHING_YARDS = rec.RushingYards;             //"cuya";
            this.RUSHING_YAC = rec.Rushing_yac;                //"cuyh";
            this.RUSHING_20 = rec.Rushing_20;                  //"cu2y";
            this.RUSHING_BT = rec.Rushing_bt;                  //"cubt";



        }

        public void SetCareerStats(CareerStatsDefenseRecord rec)
        {
            this.PASSES_DEFENDED = rec.PassesDefended;             //"cdpd";
            this.TACKLES = rec.Tackles;                                //"cdta";
            this.TACKLES_FOR_LOSS = rec.TacklesForLoss;                //"cdtl";
            this.BLOCKS = rec.Blocks;                                  //"clbl";
            this.FUMBLES_FORCED = rec.FumblesForced;                   //"clff";
            this.FUMBLES_RECOVERED = rec.FumblesRecovered;             //"clfr";
            this.FUMBLES_TD = rec.Fumbles_td;                          //"clft";
            this.FUMBLE_YARDS = rec.FumbleYards;                       //"clfy";
            this.SAFETIES = rec.Safeties;                              //"clsa";
            this.SACKS = rec.Sacks;                                    //"clsk";
            this.INTERCEPTIONS = rec.Def_int;                          //"csin";
            this.INTERCEPTION_YARDS = rec.Int_yards;                   //"csiy";
            this.INTERCEPTION_LONG = rec.Int_long;                     //"cslR";
            this.INTERCEPTION_TD = rec.Int_td;                         //"csit";
        }

        public void SetCareerStats(CareerStatsOffensiveLineRecord rec)
        {
            this.PANCAKES = rec.Pancakes;                       //"copa";
            this.SACKS_ALLOWED = rec.SacksAllowed;                  //"cosa";
        }

        public void SetCareerStats(CareerPKReturnRecord rec)
        {
            this.KICK_RETURN_ATT = rec.Kra;         //"crka";
            this.KICK_RETURN_LONG = rec.Krl;        //"crkL";
            this.KICK_RETURN_TD = rec.Krtd;         //"crkt";
            this.KICK_RETURN_YARDS = rec.Kryds;     //"crky";
            this.PUNT_RETURN_ATT = rec.Pra;         //"crpa";
            this.PUNT_RETURN_LONG = rec.Prl;        //"crpL";
            this.PUNT_RETURN_TD = rec.Prtd;         //"crpt";
            this.PUNT_RETURN_YARDS = rec.Pryds;     //"crpy";
        }

        public void SetCareerStats(CareerPuntKickRecord rec)
        {
            this.FGA = rec.Fga;           //"ckfa";
            this.FGM = rec.Fgm;             //"ckfm";
            this.FG_BLOCKED = rec.Fgbl;        //"ckfb";
            this.FGL = rec.Fgl;                //"ckfL";
            this.XPA = rec.Xpa;                //"ckea";
            this.XPM = rec.Xpm;                //"ckem";
            this.XP_BLOCKED = rec.Xpb;         //"ckeb";
            this.FGA_129 = rec.Fga_129;        //"ckaa";
            this.FGA_3039 = rec.Fga_3039;      //"ckac";
            this.FGA_4049 = rec.Fga_4049;      //"ckad";
            this.FGA_50 = rec.Fga_50;          //"ckae";
            this.FGM_129 = rec.Fgm_129;        //"ckma";
            this.FGM_3039 = rec.Fgm_3039;      //"ckmc";
            this.FGM_4049 = rec.Fgm_4049;      //"ckmd";
            this.FGM_50 = rec.Fgm_50;          //"ckme";
            this.KICK_OFFS = rec.Kickoffs;     //"cknk";
            this.TOUCHBACKS = rec.Touchbacks;  //"cktb";
            // Punter stats
            this.PUNT_ATT = rec.Puntatt;       //"cpat";
            this.PUNT_YDS = rec.Puntyds;       //"cpya";
            this.PUNT_BLOCKED = rec.Puntblk;  //"cpbl";
            this.PUNT_LONG = rec.Puntlong;     //"cpIN";
            this.PUNT_NY = rec.Puntny;         //"cpny";
            this.PUNT_IN20 = rec.Puntin20;     //"cppt";
            this.PUNT_TB = rec.Punttb;          //"cptb";

        }



        public void Read(BinaryReader binreader)
        {   
            this.DOWNS_PLAYED = binreader.ReadInt32();
            this.GAMES_PLAYED = binreader.ReadInt32();
            this.GAMES_STARTED = binreader.ReadInt32();

            //QB Stats
            this.PASS_ATT = binreader.ReadInt32();     //"caat";
            this.PASS_COMP = binreader.ReadInt32();     //"cacm";
            this.PASS_INT = binreader.ReadInt32();     //"cain";
            this.PASS_LONG = binreader.ReadInt32();     //"calN";
            this.PASS_SACKED = binreader.ReadInt32();     //"casa";
            this.PASS_YDS = binreader.ReadInt32();     //"caya";
            this.PASS_TDS = binreader.ReadInt32();     //"catd";

            // WR Stats
            this.RECEIVING_RECS = binreader.ReadInt32();     //"ccca";
            this.RECEIVING_DROPS = binreader.ReadInt32();     //"ccdr";
            this.RECEIVING_TDS = binreader.ReadInt32();     //"cctd";
            this.RECEIVING_YARDS = binreader.ReadInt32();     //"ccya";
            this.RECEIVING_YAC = binreader.ReadInt32();     //"ccyc";
            this.RECEIVING_LONG = binreader.ReadInt32();     //"ccrL";

            //RB Stats
            this.RUSHING_TDS = binreader.ReadInt32();     //"cutd";
            this.RUSHING_LONG = binreader.ReadInt32();     //"culN";
            this.FUMBLES = binreader.ReadInt32();     //"cufu";
            this.RUSHING_ATTEMPTS = binreader.ReadInt32();     //"cuat";
            this.RUSHING_YARDS = binreader.ReadInt32();     //"cuya";
            this.RUSHING_YAC = binreader.ReadInt32();     //"cuyh";
            this.RUSHING_20 = binreader.ReadInt32();     //"cu2y";
            this.RUSHING_BT = binreader.ReadInt32();     //"cubt";

            this.PASSES_DEFENDED = binreader.ReadInt32();     //"cdpd";
            this.TACKLES = binreader.ReadInt32();     //"cdta";
            this.TACKLES_FOR_LOSS = binreader.ReadInt32();     //"cdtl";
            this.BLOCKS = binreader.ReadInt32();     //"clbl";
            this.FUMBLES_FORCED = binreader.ReadInt32();     //"clff";
            this.FUMBLES_RECOVERED = binreader.ReadInt32();     //"clfr";
            this.FUMBLES_TD = binreader.ReadInt32();     //"clft";
            this.FUMBLE_YARDS = binreader.ReadInt32();     //"clfy";
            this.SAFETIES = binreader.ReadInt32();     //"clsa";
            this.SACKS = binreader.ReadInt32();     //"clsk";
            this.INTERCEPTIONS = binreader.ReadInt32();     //"csin";
            this.INTERCEPTION_YARDS = binreader.ReadInt32();     //"csiy";
            this.INTERCEPTION_LONG = binreader.ReadInt32();     //"cslR";
            this.INTERCEPTION_TD = binreader.ReadInt32();     //"csit";

            this.PANCAKES = binreader.ReadInt32();     //"copa";
            this.SACKS_ALLOWED = binreader.ReadInt32();     //"cosa"; 

            this.KICK_RETURN_ATT = binreader.ReadInt32();     //"crka";
            this.KICK_RETURN_LONG = binreader.ReadInt32();     //"crkL";
            this.KICK_RETURN_TD = binreader.ReadInt32();     //"crkt";
            this.KICK_RETURN_YARDS = binreader.ReadInt32();     //"crky";
            this.PUNT_RETURN_ATT = binreader.ReadInt32();     //"crpa";
            this.PUNT_RETURN_LONG = binreader.ReadInt32();     //"crpL";
            this.PUNT_RETURN_TD = binreader.ReadInt32();     //"crpt";
            this.PUNT_RETURN_YARDS = binreader.ReadInt32();     //"crpy";

            this.FGA = binreader.ReadInt32();     //"ckfa";
            this.FGM = binreader.ReadInt32();     //"ckfm";
            this.FG_BLOCKED = binreader.ReadInt32();     //"ckfb";
            this.FGL = binreader.ReadInt32();     //"ckfL";
            this.XPA = binreader.ReadInt32();     //"ckea";
            this.XPM = binreader.ReadInt32();     //"ckem";
            this.XP_BLOCKED = binreader.ReadInt32();     //"ckeb";
            this.FGA_129 = binreader.ReadInt32();     //"ckaa";
            this.FGA_3039 = binreader.ReadInt32();     //"ckac";
            this.FGA_4049 = binreader.ReadInt32();     //"ckad";
            this.FGA_50 = binreader.ReadInt32();     //"ckae";
            this.FGM_129 = binreader.ReadInt32();     //"ckma";
            this.FGM_3039 = binreader.ReadInt32();     //"ckmc";
            this.FGM_4049 = binreader.ReadInt32();     //"ckmd";
            this.FGM_50 = binreader.ReadInt32();     //"ckme";
            this.KICK_OFFS = binreader.ReadInt32();     //"cknk";
            this.TOUCHBACKS = binreader.ReadInt32();     //"cktb";
            // Punter stats
            this.PUNT_ATT = binreader.ReadInt32();     //"cpat";
            this.PUNT_YDS = binreader.ReadInt32();     //"cpya";
            this.PUNT_BLOCKED = binreader.ReadInt32();     //"cpbl";
            this.PUNT_LONG = binreader.ReadInt32();     //"cpIN";
            this.PUNT_NY = binreader.ReadInt32();     //"cpny";
            this.PUNT_IN20 = binreader.ReadInt32();     //"cppt";
            this.PUNT_TB = binreader.ReadInt32();     //"cptb";
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(DOWNS_PLAYED);
            binwriter.Write(GAMES_PLAYED);
            binwriter.Write(GAMES_STARTED);

            //QB Stats
            binwriter.Write(PASS_ATT);     //"caat";
            binwriter.Write(PASS_COMP);     //"cacm";
            binwriter.Write(PASS_INT);     //"cain";
            binwriter.Write(PASS_LONG);     //"calN";
            binwriter.Write(PASS_SACKED);     //"casa";
            binwriter.Write(PASS_YDS);     //"caya";
            binwriter.Write(PASS_TDS);     //"catd";

            // WR Stats
            binwriter.Write(RECEIVING_RECS);     //"ccca";
            binwriter.Write(RECEIVING_DROPS);     //"ccdr";
            binwriter.Write(RECEIVING_TDS);     //"cctd";
            binwriter.Write(RECEIVING_YARDS);     //"ccya";
            binwriter.Write(RECEIVING_YAC);     //"ccyc";
            binwriter.Write(RECEIVING_LONG);     //"ccrL";

            //RB Stats
            binwriter.Write(RUSHING_TDS);     //"cutd";
            binwriter.Write(RUSHING_LONG);     //"culN";
            binwriter.Write(FUMBLES);     //"cufu";
            binwriter.Write(RUSHING_ATTEMPTS);     //"cuat";
            binwriter.Write(RUSHING_YARDS);     //"cuya";
            binwriter.Write(RUSHING_YAC);     //"cuyh";
            binwriter.Write(RUSHING_20);     //"cu2y";
            binwriter.Write(RUSHING_BT);     //"cubt";

            binwriter.Write(PASSES_DEFENDED);     //"cdpd";
            binwriter.Write(TACKLES);     //"cdta";
            binwriter.Write(TACKLES_FOR_LOSS);     //"cdtl";
            binwriter.Write(BLOCKS);     //"clbl";
            binwriter.Write(FUMBLES_FORCED);     //"clff";
            binwriter.Write(FUMBLES_RECOVERED);     //"clfr";
            binwriter.Write(FUMBLES_TD);     //"clft";
            binwriter.Write(FUMBLE_YARDS);     //"clfy";
            binwriter.Write(SAFETIES);     //"clsa";
            binwriter.Write(SACKS);     //"clsk";
            binwriter.Write(INTERCEPTIONS);     //"csin";
            binwriter.Write(INTERCEPTION_YARDS);     //"csiy";
            binwriter.Write(INTERCEPTION_LONG);     //"cslR";
            binwriter.Write(INTERCEPTION_TD);     //"csit";            

            binwriter.Write(PANCAKES);     //"copa";
            binwriter.Write(SACKS_ALLOWED);     //"cosa";

            binwriter.Write(KICK_RETURN_ATT);     //"crka";
            binwriter.Write(KICK_RETURN_LONG);     //"crkL";
            binwriter.Write(KICK_RETURN_TD);     //"crkt";
            binwriter.Write(KICK_RETURN_YARDS);     //"crky";
            binwriter.Write(PUNT_RETURN_ATT);     //"crpa";
            binwriter.Write(PUNT_RETURN_LONG);     //"crpL";
            binwriter.Write(PUNT_RETURN_TD);     //"crpt";
            binwriter.Write(PUNT_RETURN_YARDS);     //"crpy";

            binwriter.Write(FGA);     //"ckfa";
            binwriter.Write(FGM);     //"ckfm";
            binwriter.Write(FG_BLOCKED);     //"ckfb";
            binwriter.Write(FGL);     //"ckfL";
            binwriter.Write(XPA);     //"ckea";
            binwriter.Write(XPM);     //"ckem";
            binwriter.Write(XP_BLOCKED);     //"ckeb";
            binwriter.Write(FGA_129);     //"ckaa";
            binwriter.Write(FGA_3039);     //"ckac";
            binwriter.Write(FGA_4049);     //"ckad";
            binwriter.Write(FGA_50);     //"ckae";
            binwriter.Write(FGM_129);     //"ckma";
            binwriter.Write(FGM_3039);     //"ckmc";
            binwriter.Write(FGM_4049);     //"ckmd";
            binwriter.Write(FGM_50);     //"ckme";
            binwriter.Write(KICK_OFFS);     //"cknk";
            binwriter.Write(TOUCHBACKS);     //"cktb";
            // Punter stats
            binwriter.Write(PUNT_ATT);     //"cpat";
            binwriter.Write(PUNT_YDS);     //"cpya";
            binwriter.Write(PUNT_BLOCKED);     //"cpbl";
            binwriter.Write(PUNT_LONG);     //"cpIN";
            binwriter.Write(PUNT_NY);     //"cpny";
            binwriter.Write(PUNT_IN20);     //"cppt";
            binwriter.Write(PUNT_TB);     //"cptb";
        }

    }
        
    public class SeasonStats
    {
        #region Members
        public int SEASON = 0;                                      //  "SEYR";
        
        public int GAMES_DOWNS_PLAYED = 0;                          // "sgdp";
        public int GAMES_PLAYED = 0;                                // "sgmp";
        public int GAMES_STARTED = 0;                               // "sgms";
        
        public int SEA_PASS_ATT = 0;                              //  "saat";
        public int SEA_PASS_COMEBACKS = 0;                        //  "sacb";         //  2008
        public int SEA_COMP = 0;                                  //  "sacm";
        public int SEA_PASS_FIRST_DOWNS = 0;                      //  "safd";         //  2008
        public int SEA_PASS_INT = 0;                              //  "sain";
        public int SEA_PASS_LONG = 0;                             //  "saln";
        public int SEA_SACKED = 0;                                //  "sasa";
        public int SEA_PASS_TD = 0;                               //  "satd";
        public int SEA_PASS_YDS = 0;                              //  "saya";
        public int SEA_REC = 0;                                   //  "scca";
        public int SEA_DROPS = 0;                                 //  "scdr";
        public int SEA_REC_LONG = 0;                              //  "scrL";
        public int SEA_REC_TD = 0;                                //  "sctd";
        public int SEA_REC_YDS = 0;                               //  "scya";
        public int SEA_REC_YAC = 0;                               //  "scyc";        
        public int SEA_RUSH_20 = 0;                               //  "su2y";
        public int SEA_RUSH_ATT = 0;                              //  "suat";
        public int SEA_RUSH_BTK = 0;                              //  "subt";
        public int SEA_FUMBLES = 0;                               //  "sufu";
        public int SEA_RUSH_LONG = 0;                             //  "suln";
        public int SEA_RUSH_TD = 0;                               //  "sutd";
        public int SEA_RUSH_YDS = 0;                              //  "suya";
        public int SEA_RUSH_YAC = 0;                              //  "suyh";

        public int PASSES_DEFENDED = 0;                             // "sdpd";
        public int TACKLES = 0;                                     // "sdta";
        public int TACKLES_FOR_LOSS = 0;                            // "sdtl";        
        public int BLOCKS = 0;                                      // "slbl";
        public int FUMBLES_FORCED = 0;                              // "slff";
        public int FUMBLES_RECOVERED = 0;                           // "slfr";
        public int FUMBLES_TD = 0;                                  // "slft";
        public int FUMBLE_YARDS = 0;                                // "slfy";
        public int SAFETIES = 0;                                    // "slsa";
        public int SACKS = 0;                                       // "slsk";
        public int INTERCEPTIONS = 0;                               // "ssin";
        public int INTERCEPTION_TD = 0;                             // "ssit";
        public int INTERCEPTION_YARDS = 0;                          // "ssiy";
        public int INTERCEPTION_LONG = 0;                           // "sslR";

        public int PANCAKES = 0;                                    // "sopa";
        public int SACKS_ALLOWED = 0;                               // "sosa";

        public int KICK_RETURN_ATT = 0;                             // "srka";
        public int KICK_RETURN_LONG = 0;                            // "srkL";
        public int KICK_RETURN_TD = 0;                              // "srkt";
        public int KICK_RETURN_YARDS = 0;                           // "srky";
        public int PUNT_RETURN_ATT = 0;                             // "srpa";
        public int PUNT_RETURN_LONG = 0;                            // "srpL";
        public int PUNT_RETURN_TD = 0;                              // "srpt";
        public int PUNT_RETURN_YARDS = 0;                           // "srpy";

        public int FGA = 0;                                         // "skfa";
        public int FGM = 0;                                         // "skfm";
        public int FG_BLOCKED = 0;                                  // "skfb";
        public int FGL = 0;                                         // "skfL";
        public int XPA = 0;                                         // "skea";
        public int XPM = 0;                                         // "skem";
        public int XP_BLOCKED = 0;                                  // "skeb";
        public int FGA_129 = 0;                                     // "skaa";
        public int FGA_3039 = 0;                                    // "skac";
        public int FGA_4049 = 0;                                    // "skad";
        public int FGA_50 = 0;                                      // "skae";
        public int FGM_129 = 0;                                     // "skma";
        public int FGM_3039 = 0;                                    // "skmc";
        public int FGM_4049 = 0;                                    // "skmd";
        public int FGM_50 = 0;                                      // "skme";
        public int KICK_OFFS = 0;                                   // "sknk";
        public int TOUCHBACKS = 0;                                  // "sktb";

        public int PUNT_ATT = 0;                                     // "spat";
        public int PUNT_YDS = 0;                                     // "spya";
        public int PUNT_BLOCKED = 0;                                 // "spbl";
        public int PUNT_LONG = 0;                                    // "spIN";
        public int PUNT_NY = 0;                                      // "spny";
        public int PUNT_IN20 = 0;                                    // "sppt";
        public int PUNT_TB = 0;                                      // "sptb";

        #endregion


        #region Constructors

        public SeasonStats()
        {
        }

        
        #endregion

        #region Set Sats

        public void SetSeasonStats(SeasonGamesPlayedRecord rec)
        {
            this.SEASON = rec.Season;
            this.GAMES_DOWNS_PLAYED = rec.DownsPlayed;
            this.GAMES_PLAYED = rec.GamesPlayed;
            this.GAMES_STARTED = rec.GamesStarted;            
        }

        public void SetSeasonStats(SeasonStatsOffenseRecord rec)
        {            
            this.SEA_PASS_ATT = rec.SeaPassAtt;
            this.SEA_PASS_COMEBACKS = rec.SeaComebacks;
            this.SEA_COMP = rec.SeaComp;
            this.SEA_PASS_FIRST_DOWNS = rec.SeaFirstDowns;
            this.SEA_PASS_INT = rec.SeaPassInt;
            this.SEA_PASS_LONG = rec.SeaPassLong;
            this.SEA_SACKED = rec.SeaSacked;
            this.SEA_PASS_TD = rec.SeaPassTd;
            this.SEA_PASS_YDS = rec.SeaPassYds;
            this.SEA_REC = rec.SeaRec;
            this.SEA_DROPS = rec.SeaDrops;
            this.SEA_REC_LONG = rec.SeaRecLong;
            this.SEA_REC_TD = rec.SeaRecTd;
            this.SEA_REC_YDS = rec.SeaRecYds;
            this.SEA_REC_YAC = rec.SeaRecYac;            
            this.SEA_RUSH_20 = rec.SeaRush20;
            this.SEA_RUSH_ATT = rec.SeaRushAtt;
            this.SEA_RUSH_BTK = rec.SeaRushBtk;
            this.SEA_FUMBLES = rec.SeaFumbles;
            this.SEA_RUSH_LONG = rec.SeaRushLong;
            this.SEA_RUSH_TD = rec.SeaRushTd;
            this.SEA_RUSH_YDS = rec.SeaRushYds;
            this.SEA_RUSH_YAC = rec.SeaRushYac;
        }

        public void SetSeasonStats(SeasonStatsDefenseRecord rec)
        {
            this.PASSES_DEFENDED = rec.PassesDefended;
            this.TACKLES = rec.Tackles;
            this.TACKLES_FOR_LOSS = rec.TacklesForLoss;
            this.BLOCKS = rec.Blocks;
            this.FUMBLES_FORCED = rec.FumblesForced;
            this.FUMBLES_RECOVERED = rec.FumblesRecovered;
            this.FUMBLES_TD = rec.FumbleTDS;
            this.FUMBLE_YARDS = rec.FumbleYards;
            this.SAFETIES = rec.Safeties;
            this.SACKS = rec.Sacks;
            this.INTERCEPTIONS = rec.Interceptions;
            this.INTERCEPTION_TD = rec.InterceptionTDS;
            this.INTERCEPTION_YARDS = rec.InterceptionYards;
            this.INTERCEPTION_LONG = rec.InterceptionLong;
        }

        public void SetSeasonStats(SeasonStatsOffensiveLineRecord rec)
        {
            this.PANCAKES = rec.Pancakes;
            this.SACKS_ALLOWED = rec.SacksAllowed;
        }
        
        public void SetSeasonStats(SeasonPKReturnRecord rec)
        {
            this.KICK_RETURN_ATT = rec.Kra;
            this.KICK_RETURN_LONG = rec.Krl;
            this.KICK_RETURN_TD = rec.Krtd;
            this.KICK_RETURN_YARDS = rec.Kryds;
            this.PUNT_RETURN_ATT = rec.Pra;
            this.PUNT_RETURN_LONG = rec.Prl;
            this.PUNT_RETURN_TD = rec.Prtd;
            this.PUNT_RETURN_YARDS = rec.Pryds;
        }

        public void SetSeasonStats(SeasonPuntKickRecord rec)
        {
            this.FGA = rec.Fga;
            this.FGM = rec.Fgm;
            this.FG_BLOCKED = rec.Fgbl;
            this.FGL = rec.Fgl;
            this.XPA = rec.Xpa;
            this.XPM = rec.Xpm;
            this.XP_BLOCKED = rec.Xpb;
            this.FGA_129 = rec.Fga_129;
            this.FGA_3039 = rec.Fga_3039;
            this.FGA_4049 = rec.Fga_4049;
            this.FGA_50 = rec.Fga_50;
            this.FGM_129 = rec.Fgm_129;
            this.FGM_3039 = rec.Fgm_3039;
            this.FGM_4049 = rec.Fgm_4049;
            this.FGM_50 = rec.Fgm_50;
            this.KICK_OFFS = rec.Kickoffs;
            this.TOUCHBACKS = rec.Touchbacks;
            this.PUNT_ATT = rec.Puntatt;
            this.PUNT_YDS = rec.Puntyds;
            this.PUNT_BLOCKED = rec.Puntblk;
            this.PUNT_LONG = rec.Puntlong;
            this.PUNT_NY = rec.Puntny;
            this.PUNT_IN20 = rec.Puntin20;
            this.PUNT_TB = rec.Punttb;
        }

        #endregion

        #region File IO

        public void Read(BinaryReader binreader, int count)
        {
            this.SEASON = binreader.ReadInt32();                                      //  "SEYR";

            this.GAMES_DOWNS_PLAYED = binreader.ReadInt32();                          // "sgdp";
            this.GAMES_PLAYED = binreader.ReadInt32();                                // "sgmp";
            this.GAMES_STARTED = binreader.ReadInt32();                               // "sgms";

            this.SEA_PASS_ATT = binreader.ReadInt32();                              //  "saat";
            this.SEA_PASS_COMEBACKS = binreader.ReadInt32();                        //  "sacb";         //  2008
            this.SEA_COMP = binreader.ReadInt32();                                  //  "sacm";
            this.SEA_PASS_FIRST_DOWNS = binreader.ReadInt32();                      //  "safd";         //  2008
            this.SEA_PASS_INT = binreader.ReadInt32();                              //  "sain";
            this.SEA_PASS_LONG = binreader.ReadInt32();                             //  "saln";
            this.SEA_SACKED = binreader.ReadInt32();                                //  "sasa";
            this.SEA_PASS_TD = binreader.ReadInt32();                               //  "satd";
            this.SEA_PASS_YDS = binreader.ReadInt32();                              //  "saya";
            this.SEA_REC = binreader.ReadInt32();                                   //  "scca";
            this.SEA_DROPS = binreader.ReadInt32();                                 //  "scdr";
            this.SEA_REC_LONG = binreader.ReadInt32();                              //  "scrL";
            this.SEA_REC_TD = binreader.ReadInt32();                                //  "sctd";
            this.SEA_REC_YDS = binreader.ReadInt32();                               //  "scya";
            this.SEA_REC_YAC = binreader.ReadInt32();                               //  "scyc";        
            this.SEA_RUSH_20 = binreader.ReadInt32();                               //  "su2y";
            this.SEA_RUSH_ATT = binreader.ReadInt32();                              //  "suat";
            this.SEA_RUSH_BTK = binreader.ReadInt32();                              //  "subt";
            this.SEA_FUMBLES = binreader.ReadInt32();                               //  "sufu";
            this.SEA_RUSH_LONG = binreader.ReadInt32();                             //  "suln";
            this.SEA_RUSH_TD = binreader.ReadInt32();                               //  "sutd";
            this.SEA_RUSH_YDS = binreader.ReadInt32();                              //  "suya";
            this.SEA_RUSH_YAC = binreader.ReadInt32();                              //  "suyh";

            this.PASSES_DEFENDED = binreader.ReadInt32();                             // "sdpd";
            this.TACKLES = binreader.ReadInt32();                                     // "sdta";
            this.TACKLES_FOR_LOSS = binreader.ReadInt32();                            // "sdtl";        
            this.BLOCKS = binreader.ReadInt32();                                      // "slbl";
            this.FUMBLES_FORCED = binreader.ReadInt32();                              // "slff";
            this.FUMBLES_RECOVERED = binreader.ReadInt32();                           // "slfr";
            this.FUMBLES_TD = binreader.ReadInt32();                                  // "slft";
            this.FUMBLE_YARDS = binreader.ReadInt32();                                // "slfy";
            this.SAFETIES = binreader.ReadInt32();                                    // "slsa";
            this.SACKS = binreader.ReadInt32();                                       // "slsk";
            this.INTERCEPTIONS = binreader.ReadInt32();                               // "ssin";
            this.INTERCEPTION_TD = binreader.ReadInt32();                             // "ssit";
            this.INTERCEPTION_YARDS = binreader.ReadInt32();                          // "ssiy";
            this.INTERCEPTION_LONG = binreader.ReadInt32();                           // "sslR";

            this.PANCAKES = binreader.ReadInt32();                                    // "sopa";
            this.SACKS_ALLOWED = binreader.ReadInt32();                               // "sosa";

            this.KICK_RETURN_ATT = binreader.ReadInt32();                             // "srka";
            this.KICK_RETURN_LONG = binreader.ReadInt32();                            // "srkL";
            this.KICK_RETURN_TD = binreader.ReadInt32();                              // "srkt";
            this.KICK_RETURN_YARDS = binreader.ReadInt32();                           // "srky";
            this.PUNT_RETURN_ATT = binreader.ReadInt32();                             // "srpa";
            this.PUNT_RETURN_LONG = binreader.ReadInt32();                            // "srpL";
            this.PUNT_RETURN_TD = binreader.ReadInt32();                              // "srpt";
            this.PUNT_RETURN_YARDS = binreader.ReadInt32();                           // "srpy";

            this.FGA = binreader.ReadInt32();                                         // "skfa";
            this.FGM = binreader.ReadInt32();                                         // "skfm";
            this.FG_BLOCKED = binreader.ReadInt32();                                  // "skfb";
            this.FGL = binreader.ReadInt32();                                         // "skfL";
            this.XPA = binreader.ReadInt32();                                         // "skea";
            this.XPM = binreader.ReadInt32();                                         // "skem";
            this.XP_BLOCKED = binreader.ReadInt32();                                  // "skeb";
            this.FGA_129 = binreader.ReadInt32();                                     // "skaa";
            this.FGA_3039 = binreader.ReadInt32();                                    // "skac";
            this.FGA_4049 = binreader.ReadInt32();                                    // "skad";
            this.FGA_50 = binreader.ReadInt32();                                      // "skae";
            this.FGM_129 = binreader.ReadInt32();                                     // "skma";
            this.FGM_3039 = binreader.ReadInt32();                                    // "skmc";
            this.FGM_4049 = binreader.ReadInt32();                                    // "skmd";
            this.FGM_50 = binreader.ReadInt32();                                      // "skme";
            this.KICK_OFFS = binreader.ReadInt32();                                   // "sknk";
            this.TOUCHBACKS = binreader.ReadInt32();                                  // "sktb";

            this.PUNT_ATT = binreader.ReadInt32();                                     // "spat";
            this.PUNT_YDS = binreader.ReadInt32();                                     // "spya";
            this.PUNT_BLOCKED = binreader.ReadInt32();                                 // "spbl";
            this.PUNT_LONG = binreader.ReadInt32();                                    // "spIN";
            this.PUNT_NY = binreader.ReadInt32();                                      // "spny";
            this.PUNT_IN20 = binreader.ReadInt32();                                    // "sppt";
            this.PUNT_TB = binreader.ReadInt32();                                      // "sptb";
        }
        
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(SEASON);                                      //  "SEYR";

            binwriter.Write(GAMES_DOWNS_PLAYED);                          // "sgdp";
            binwriter.Write(GAMES_PLAYED);                                // "sgmp";
            binwriter.Write(GAMES_STARTED);                               // "sgms";

            binwriter.Write(SEA_PASS_ATT);                              //  "saat";
            binwriter.Write(SEA_PASS_COMEBACKS);                        //  "sacb";         //  2008
            binwriter.Write(SEA_COMP);                                  //  "sacm";
            binwriter.Write(SEA_PASS_FIRST_DOWNS);                      //  "safd";         //  2008
            binwriter.Write(SEA_PASS_INT);                              //  "sain";
            binwriter.Write(SEA_PASS_LONG);                             //  "saln";
            binwriter.Write(SEA_SACKED);                                //  "sasa";
            binwriter.Write(SEA_PASS_TD);                               //  "satd";
            binwriter.Write(SEA_PASS_YDS);                              //  "saya";
            binwriter.Write(SEA_REC);                                   //  "scca";
            binwriter.Write(SEA_DROPS);                                 //  "scdr";
            binwriter.Write(SEA_REC_LONG);                              //  "scrL";
            binwriter.Write(SEA_REC_TD);                                //  "sctd";
            binwriter.Write(SEA_REC_YDS);                               //  "scya";
            binwriter.Write(SEA_REC_YAC);                               //  "scyc";        
            binwriter.Write(SEA_RUSH_20);                               //  "su2y";
            binwriter.Write(SEA_RUSH_ATT);                              //  "suat";
            binwriter.Write(SEA_RUSH_BTK);                              //  "subt";
            binwriter.Write(SEA_FUMBLES);                               //  "sufu";
            binwriter.Write(SEA_RUSH_LONG);                             //  "suln";
            binwriter.Write(SEA_RUSH_TD);                               //  "sutd";
            binwriter.Write(SEA_RUSH_YDS);                              //  "suya";
            binwriter.Write(SEA_RUSH_YAC);                              //  "suyh";

            binwriter.Write(PASSES_DEFENDED);                             // "sdpd";
            binwriter.Write(TACKLES);                                     // "sdta";
            binwriter.Write(TACKLES_FOR_LOSS);                            // "sdtl";        
            binwriter.Write(BLOCKS);                                      // "slbl";
            binwriter.Write(FUMBLES_FORCED);                              // "slff";
            binwriter.Write(FUMBLES_RECOVERED);                           // "slfr";
            binwriter.Write(FUMBLES_TD);                                  // "slft";
            binwriter.Write(FUMBLE_YARDS);                                // "slfy";
            binwriter.Write(SAFETIES);                                    // "slsa";
            binwriter.Write(SACKS);                                       // "slsk";
            binwriter.Write(INTERCEPTIONS);                               // "ssin";
            binwriter.Write(INTERCEPTION_TD);                             // "ssit";
            binwriter.Write(INTERCEPTION_YARDS);                          // "ssiy";
            binwriter.Write(INTERCEPTION_LONG);                           // "sslR";

            binwriter.Write(PANCAKES);                                    // "sopa";
            binwriter.Write(SACKS_ALLOWED);                               // "sosa";

            binwriter.Write(KICK_RETURN_ATT);                             // "srka";
            binwriter.Write(KICK_RETURN_LONG);                            // "srkL";
            binwriter.Write(KICK_RETURN_TD);                              // "srkt";
            binwriter.Write(KICK_RETURN_YARDS);                           // "srky";
            binwriter.Write(PUNT_RETURN_ATT);                             // "srpa";
            binwriter.Write(PUNT_RETURN_LONG);                            // "srpL";
            binwriter.Write(PUNT_RETURN_TD);                              // "srpt";
            binwriter.Write(PUNT_RETURN_YARDS);                           // "srpy";

            binwriter.Write(FGA);                                         // "skfa";
            binwriter.Write(FGM);                                         // "skfm";
            binwriter.Write(FG_BLOCKED);                                  // "skfb";
            binwriter.Write(FGL);                                         // "skfL";
            binwriter.Write(XPA);                                         // "skea";
            binwriter.Write(XPM);                                         // "skem";
            binwriter.Write(XP_BLOCKED);                                  // "skeb";
            binwriter.Write(FGA_129);                                     // "skaa";
            binwriter.Write(FGA_3039);                                    // "skac";
            binwriter.Write(FGA_4049);                                    // "skad";
            binwriter.Write(FGA_50);                                      // "skae";
            binwriter.Write(FGM_129);                                     // "skma";
            binwriter.Write(FGM_3039);                                    // "skmc";
            binwriter.Write(FGM_4049);                                    // "skmd";
            binwriter.Write(FGM_50);                                      // "skme";
            binwriter.Write(KICK_OFFS);                                   // "sknk";
            binwriter.Write(TOUCHBACKS);                                  // "sktb";

            binwriter.Write(PUNT_ATT);                                     // "spat";
            binwriter.Write(PUNT_YDS);                                     // "spya";
            binwriter.Write(PUNT_BLOCKED);                                 // "spbl";
            binwriter.Write(PUNT_LONG);                                    // "spIN";
            binwriter.Write(PUNT_NY);                                      // "spny";
            binwriter.Write(PUNT_IN20);                                    // "sppt";
            binwriter.Write(PUNT_TB);                                      // "sptb";
        }
        

        #endregion
                
    }

    #endregion

    public class Player
    {
        #region Members
        
        public int AGE = 21;                            //"PAGE";
        public int COLLEGE_ID = 0;                      //"PCOL";
        public int CONTRACT_LENGTH = 0;                 //"PCON";
        public int SALARY_CURRENT = 0;                  //"PCSA";
        public int CONTRACT_YRS_LEFT = 0;               //"PCYL";
        public int DRAFT_ROUND_INDEX = 0;               //"PDPI";
        public int DRAFT_ROUND = 0;                     //"PDRO";
        public string FIRST_NAME = "New";               //"PFNA"
        public bool PRO_BOWL = false;                   //"PFPB";
        public int PLAYER_ID = 0;                       //"PGID";
        public int HEIGHT = 0;                          //"PHGT";                       Add 65 to this for height in inches
        public bool HOLDOUT = false;                    //"PFHO";                       // ?
        public bool NFL_ICON = false;                   //"PICN";
        public int IMPORTANCE = 50;                     //"PIMP";     
        public int LAST_HEALTHY_YEAR = 0;               //"PLHY";                       // not sure about this one
        public string LAST_NAME = "Player";             //"PLNA";
        public int NFL_ID = 0;                          //"POID";
        public int PREVIOUS_POSITION_ID = 0;            //"POPS";
        public int OVERALL = 0;                         //"POVR";
        public int POSITION_ID = 0;                     //"PPOS";
        public int PREVIOUS_TEAM_ID = 0;                //"PPTI";
        public int PLAYER_ROLE = 0;                     //"PROL";                       // 2007
        public int PLAYER_WEAPON = 0;                   //"PRL2";                       // 2008
        public int SALARY_YEAR_0 = 0;                   //"PSA0";
        public int SALARY_YEAR_1 = 0;                   //"PSA1";
        public int SALARY_YEAR_2 = 0;                   //"PSA2";
        public int SALARY_YEAR_3 = 0;                   //"PSA3";
        public int SALARY_YEAR_4 = 0;                   //"PSA4";
        public int SALARY_YEAR_5 = 0;                   //"PSA5";
        public int SALARY_YEAR_6 = 0;                   //"PSA6";
        public int SIGNING_BONUS_YEAR_0 = 0;            //"PSB0";
        public int SIGNING_BONUS_YEAR_1 = 0;            //"PSB1";
        public int SIGNING_BONUS_YEAR_2 = 0;            //"PSB2";
        public int SIGNING_BONUS_YEAR_3 = 0;            //"PSB3";
        public int SIGNING_BONUS_YEAR_4 = 0;            //"PSB4";
        public int SIGNING_BONUS_YEAR_5 = 0;            //"PSB5";
        public int SIGNING_BONUS_YEAR_6 = 0;            //"PSB6";
        public int SIGNING_BONUS = 0;                   //"PSBO";
        public int PORTRAIT_ID = 0;                     //"PSXP";
        public int TOTAL_SALARY = 0;                    //"PTSA";                       // 2008
        public int PLAYER_VALUE = 50;                   //"PVAL";                       // 2008
        public int PREVIOUS_SIGNING_BONUS_TOTAL = 0;    //"PVSB";
        public int WEIGHT = 0;                          //"PWGT";                       Add 160 to this for actual weight in pounds
        public int YRS_PRO = 0;                         //"PYRP";
        public int TENDENCY = 0;                        //"PTEN";
        public int TEAM_ID = 0;                         //"TGID";

        #region Ratings and Progression Rate

        public Player_Ratings Original_Ratings;
        public Player_Ratings Current_Ratings;
        public Potential PlayerPotential;
        public Prog_Rate PlayerProgRate;

        #endregion

        #region Non-Used Player Record Members
        /*
        public int NASAL_STRIP = 0;                     //"PBRE";        
        public int PCEL = 0;                            //"PCEL";                       // Not sure what this is
        public int EQP_PAD_SHELF = 0;                   //"PCHS";
        public int PLAYER_COMMENT = 0;                  //"PCMT";
        
        
        public int SKIN_COLOR = 0;                      //"PCPH";
        
        public int PCTS = 0;                            //"PCTS";                       // ?
        
        public int EYE_PAINT = 0;                       //"PEYE";
        public int ARMS_FAT = 0;                        //"PFAS";
        public int LEGS_CALF_FAT = 0;                   //"PFCS";
        public int FACE_ID = 0;                         //"PFEx";
        public int FACE_SHAPE = 0;                      //"PFGE";                       // 2004 field
        public int PFGS = 0;                            //"PFGS";                       // ?
        
        public int LEGS_THIGH_FAT = 0;                  //"PFHS";
        public int FACE_MASK = 0;                       //"PFMK";
        
        
        public int BODY_FAT = 0;                        //"PFTS";
        
        public int SLEEVES_A = 0;                       //"PGSL";
        public bool DOMINANT_HAND = false;              //"PHAN";
        public int HAIR_COLOR = 0;                      //"PHCL";
        public int HAIR_STYLE = 0;                      //"PHED";
        
        public int HELMET_STYLE = 0;                    //"PHLM";
           
        public int JERSEY_NUMBER = 50;                  //"PJEN";
        public bool JERSEY = true;                      //"PJER";       
        public int PLAYER_JERSEY_INITIALS = 0;          //"PJTY";        
        public int LEFT_ELBOW_A = 0;                    //"PLEL";
        public int PLFH = 0;                            //"PLFH";                       // ?
        public int LEFT_HAND_A = 0;                     //"PLHA";
        
        
        public int PLPL = 0;                            //"PLPL";                       // ?
        public int LEFT_ANKLE = 0;                      //"PLSH";
        public int EQP_SHOES = 0;                       //"PLSS";
        public int LEFT_KNEE = 0;                       //"PLTH";
        public int LEFT_WRIST_A = 0;                    //"PLWR";
        public int ARMS_MUSCLE = 0;                     //"PMAS";
        public int LEGS_CALF_MUSCLE = 0;                //"PMCS";
        public int REAR_FAT = 0;                        //"PMGS";
        public int LEGS_THIGH_MUSCLE = 0;               //"PMHS";        
        public int MOUTHPIECE = 0;                      //"PMPC";
        public int BODY_WEIGHT = 0;                     //"PMTS";                       
        public int MUSCLE = 0;                          //"PMUS";
        public int NECK_ROLL = 0;                       //"PNEK";
        
        
        public int PPGA = 0;                            //"PPGA";                       // ?
        
        public int PPSP = 0;                            //"PPSP";
        
        public int REAR_SHAPE = 0;                      //"PQGS";
        public int EQP_FLAK_JACKET = 0;                 //"PQTS";
        public int RIGHT_ELBOW_A = 0;                   //"PREL";
        public int RIGHT_HAND_A = 0;                    // "PRHA";
        
        public int RIGHT_ANKLE = 0;                     //"PRSH";
        public int RIGHT_KNEE = 0;                      //"PRTH";
        public int RIGHT_WRIST_A = 0;                   //"PRWR";
        
        public int BODY_OVERALL = 0;                    //"PSBS";
        public int PSKI = 0;                            //"PSKI";                               
        public int PSTM = 0;                            //"PSTM";
        public int THROWING_STYLE = 0;                  //"PSTY";
        
        public int LEFT_TATTOO = 0;                     //"PTAL";
        public int RIGHT_TATTOO = 0;                    //"PTAR";
        
        public int LEGS_THIGH_PADS = 0;                 //"PTPS";                       // 2004-2005
        
        public int SLEEVES_B = 0;                       //"PTSL";
        public int EQP_PAD_HEIGHT = 0;                  //"PTSS";
        public int PUCL = 0;                            //"PUCL";                       // ?
        public int BODY_MUSCLE = 0;                     //"PUTS";
       
        public int PVCO = 0;                            //"PVCO";                       // ?
        public int VISOR = 0;                           //"PVIS";
        
        public int EQP_PAD_WIDTH = 0;                   //"PWSS";
        
        public int LEFT_ELBOW_B = 0;                    //"TLEL";
        public int LEFT_HAND_B = 0;                     //"TLHA";
        public int LEFT_WRIST_B = 0;                    //"TLWR";
        public int RIGHT_ELBOW_B = 0;                   //"TREL";
        public int RIGHT_HAND_B = 0;                    //"TRHA";
        public int RIGHT_WRIST_B = 0;                   //"TRWR";

         */
        #endregion



        public CareerStats PlayerCareerStats;
        public List<SeasonStats> PlayerSeasonStats;

        #endregion

        #region Constructors

        public Player()
        {
            Original_Ratings = new Player_Ratings();
            Current_Ratings = new Player_Ratings();
            PlayerPotential = new Potential();
            PlayerProgRate = new Prog_Rate();            
            PlayerCareerStats = new CareerStats();
            PlayerSeasonStats = new List<SeasonStats>();
        }
        
        public Player(PlayerRecord rec)
        {
            AGE = rec.Age;
            COLLEGE_ID = rec.CollegeId;
            CONTRACT_LENGTH = rec.ContractLength;
            CONTRACT_YRS_LEFT = rec.ContractYearsLeft;
            DRAFT_ROUND = rec.DraftRound;
            DRAFT_ROUND_INDEX = rec.DraftRoundIndex;
            FIRST_NAME = rec.FirstName;
            HEIGHT = rec.Height;
            HOLDOUT = rec.Holdout;
            IMPORTANCE = rec.Importance;
            LAST_HEALTHY_YEAR = rec.LastHealthy;
            LAST_NAME = rec.LastName;
            NFL_ICON = rec.NFLIcon;
            NFL_ID = rec.NFLID;
            OVERALL = rec.Overall;
            PLAYER_ID = rec.PlayerId;
            PLAYER_ROLE = rec.PlayerRole;
            PLAYER_VALUE = rec.PlayerValue;
            PLAYER_WEAPON = rec.PlayerWeapon;
            PREVIOUS_POSITION_ID = rec.PreviousPositionId;
            PORTRAIT_ID = rec.PortraitId;
            POSITION_ID = rec.PositionId;
            PREVIOUS_SIGNING_BONUS_TOTAL = rec.PreviousSigningBonus;
            PREVIOUS_TEAM_ID = rec.PreviousTeamId;
            PRO_BOWL = rec.ProBowl;
            SALARY_CURRENT = rec.CurrentSalary;
            SALARY_YEAR_0 = rec.Salary0;
            SALARY_YEAR_1 = rec.Salary1;
            SALARY_YEAR_2 = rec.Salary2;
            SALARY_YEAR_3 = rec.Salary3;
            SALARY_YEAR_4 = rec.Salary4;
            SALARY_YEAR_5 = rec.Salary5;
            SALARY_YEAR_6 = rec.Salary6;
            SIGNING_BONUS = rec.SigningBonus;
            SIGNING_BONUS_YEAR_0 = rec.Bonus0;
            SIGNING_BONUS_YEAR_1 = rec.Bonus1;
            SIGNING_BONUS_YEAR_2 = rec.Bonus2;
            SIGNING_BONUS_YEAR_3 = rec.Bonus3;
            SIGNING_BONUS_YEAR_4 = rec.Bonus4;
            SIGNING_BONUS_YEAR_5 = rec.Bonus5;
            SIGNING_BONUS_YEAR_6 = rec.Bonus6;
            TEAM_ID = rec.TeamId;
            TENDENCY = rec.Tendency;
            TOTAL_SALARY = rec.TotalSalary;
            WEIGHT = rec.Weight;
            YRS_PRO = rec.YearsPro;

            Original_Ratings = new Player_Ratings(rec);
            Current_Ratings = new Player_Ratings(rec);
            PlayerPotential = new Potential();
            PlayerProgRate = new Prog_Rate();

            PlayerCareerStats = new CareerStats();
            PlayerSeasonStats = new List<SeasonStats>();

            int count = YRS_PRO;
            if (YRS_PRO == 0)
                count = 1;
            for (int c = 0; c < count; c++)
                PlayerSeasonStats.Add(new SeasonStats());
        }

        #endregion

        public void UpdateSelfValue()
        {
            double perc = 1.0 + (this.Current_Ratings.EGO - 70) / 450;
            if (perc < .95)
                perc = .95;
            this.Current_Ratings.self_overall = this.CalculateOverallRating(this.POSITION_ID, true, false) * perc;
        }

        public int CalculateOverallRating(int positionId, bool round, bool update)
        {
            double tempOverall = 0;

            switch (positionId)
            {
                case (int)MaddenPositions.QB:
                    tempOverall += (((double)this.Current_Ratings.THROW_POWER - 50) / 10) * 4.9;
                    tempOverall += (((double)this.Current_Ratings.THROW_ACCURACY - 50) / 10) * 5.8;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 4.0;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 2.0;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 28;
                    break;

                case (int)MaddenPositions.HB:
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 0.33;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.CARRYING - 50) / 10) * 2.0;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 2.8;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.0;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.6;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 1.4;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 27;
                    break;

                case (int)MaddenPositions.FB:
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 1.0;
                    tempOverall += (((double)this.Current_Ratings.RUN_BLOCKING - 50) / 10) * 7.2;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.CARRYING - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.0;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.8;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 5.2;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 39;
                    break;

                case (int)MaddenPositions.WR:
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 4.75;
                    tempOverall += (((double)this.Current_Ratings.JUMPING - 50) / 10) * 1.4;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 26;
                    break;

                case (int)MaddenPositions.TE:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 2.65;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 2.65;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.65;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.25;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.25;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 5.4;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 1.2;
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 1.2;
                    tempOverall += (((double)this.Current_Ratings.RUN_BLOCKING - 50) / 10) * 5.4;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 35;
                    break;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 4.75;
                    tempOverall += (((double)this.Current_Ratings.RUN_BLOCKING - 50) / 10) * 3.75;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 26;
                    break;

                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                case (int)MaddenPositions.C:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 1.7;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 3.25;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 3.25;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.7;
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 3.25;
                    tempOverall += (((double)this.Current_Ratings.RUN_BLOCKING - 50) / 10) * 4.8;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 28;
                    break;

                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.RE:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.75;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 3.75;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 1.75;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.75;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 3.8;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 5.5;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 30;
                    break;

                case (int)MaddenPositions.DT:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 5.5;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 3.8;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 2.8;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 4.55;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 29;
                    break;

                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.ROLB:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.75;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 2.4;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 3.6;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 2.4;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.3;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 1.3;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 4.8;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 29;
                    break;

                case (int)MaddenPositions.MLB:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 0.75;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 3.4;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 5.2;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.65;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.75;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 5.2;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 27;
                    break;

                case (int)MaddenPositions.CB:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.85;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.9;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 3.85;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.55;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 2.35;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 3;
                    tempOverall += (((double)this.Current_Ratings.JUMPING - 50) / 10) * 1.55;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 1.55;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 28;
                    break;

                case (int)MaddenPositions.FS:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.0;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.9;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 4.85;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.5;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 2.5;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 3.0;
                    tempOverall += (((double)this.Current_Ratings.JUMPING - 50) / 10) * 1.5;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * .5;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 30;
                    break;

                case (int)MaddenPositions.SS:
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.2;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 1.7;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 4.75;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 1.7;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.7;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 3.2;
                    tempOverall += (((double)this.Current_Ratings.JUMPING - 50) / 10) * 0.9;
                    tempOverall += (((double)this.Current_Ratings.TACKLE - 50) / 10) * 3.2;
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 30;
                    break;

                case (int)MaddenPositions.P:
                    tempOverall = (double)(-183 + 0.218 * this.Current_Ratings.AWARENESS + 1.5 * this.Current_Ratings.KICK_POWER + 1.33 * this.Current_Ratings.KICK_ACCURACY);
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
                    break;

                case (int)MaddenPositions.K:
                    tempOverall = (double)(-177 + 0.218 * this.Current_Ratings.AWARENESS + 1.28 * this.Current_Ratings.KICK_POWER + 1.47 * this.Current_Ratings.KICK_ACCURACY);
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
                    break;
            }

            if (round)
            {
                if (tempOverall < 0)
                    tempOverall = 0;

                if (tempOverall > 99)
                    tempOverall = 99;
            }
         
            if (update)
                this.OVERALL = (int)tempOverall;

            return (int)tempOverall;
        }
          		       
        public void UpdateCurrentRatings(PlayerRecord rec)
        {
            this.Current_Ratings = new Player_Ratings(rec);
            int update = CalculateOverallRating(this.POSITION_ID, true, true);
            UpdateSelfValue();
        }

        public void UpdateCareerStats(EditorModel model)
        {
            foreach (CareerGamesPlayedRecord rec in model.TableModels[EditorModel.CAREER_GAMES_PLAYED_TABLE].GetRecords())
                if (this.PLAYER_ID == rec.PlayerId)
                {
                    this.PlayerCareerStats.SetCareerStats(rec);
                    break;
                }

            if (this.PlayerCareerStats.GAMES_PLAYED > 0)
            {
                foreach (CareerStatsDefenseRecord rec in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerPKReturnRecord rec in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerPuntKickRecord rec in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_TABLE].GetRecords())
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerStatsOffenseRecord rec in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerStatsOffensiveLineRecord rec in model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
            }

        }

        public void UpdateSeasonStats(EditorModel model)
        {
            if (this.YRS_PRO > this.PlayerSeasonStats.Count)
            {
                SeasonStats sea = new SeasonStats();

                foreach (SeasonGamesPlayedRecord rec in model.TableModels[EditorModel.SEASON_GAMES_PLAYED_TABLE].GetRecords())
                {
                    if (this.PLAYER_ID == rec.PlayerId)
                    {
                        sea.SetSeasonStats(rec);
                        break;
                    }
                }

                if (sea.GAMES_PLAYED > 0)
                {
                    foreach (SeasonPKReturnRecord rec in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                    {
                        if (this.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonPuntKickRecord rec in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_TABLE].GetRecords())
                    {
                        if (this.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsDefenseRecord rec in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
                    {
                        if (this.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsOffenseRecord rec in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
                    {
                        if (this.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsOffensiveLineRecord rec in model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                    {
                        if (this.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                }

                this.PlayerSeasonStats.Add(sea);
            }
        }

        public void ProgressPlayer(Team team)
        {
            //  Setup Player's base progression for each ability... in sections

            //  Awareness
            double AWR = (double)(this.PlayerPotential.AWARENESS - this.Current_Ratings.AWARENESS) * this.PlayerProgRate.AWARENESS;

            //  Skill sets
            double BTK = (double)(this.PlayerPotential.BREAK_TACKLE - this.Current_Ratings.BREAK_TACKLE) * this.PlayerProgRate.BREAK_TACKLE;
            double CAR = (double)(this.PlayerPotential.CARRYING - this.Current_Ratings.CARRYING) * this.PlayerProgRate.CARRYING;
            double CAT = (double)(this.PlayerPotential.CATCHING - this.Current_Ratings.CATCHING) * this.PlayerProgRate.CATCHING;
            double KAC = (double)(this.PlayerPotential.KICK_ACCURACY - this.Current_Ratings.KICK_ACCURACY) * this.PlayerProgRate.KICK_ACCURACY;
            double KRT = (double)(this.PlayerPotential.KICK_RETURN - this.Current_Ratings.KICK_RETURN) * this.PlayerProgRate.KICK_RETURN;
            double PBK = (double)(this.PlayerPotential.PASS_BLOCKING - this.Current_Ratings.PASS_BLOCKING) * this.PlayerProgRate.PASS_BLOCKING;
            double RBK = (double)(this.PlayerPotential.RUN_BLOCKING - this.Current_Ratings.RUN_BLOCKING) * this.PlayerProgRate.RUN_BLOCKING;
            double TAK = (double)(this.PlayerPotential.TACKLE - this.Current_Ratings.TACKLE) * this.PlayerProgRate.TACKLE;
            double THA = (double)(this.PlayerPotential.THROW_ACCURACY - this.Current_Ratings.THROW_ACCURACY) * this.PlayerProgRate.THROW_ACCURACY;

            //  Physical sets
            double ACC = (double)(this.PlayerPotential.ACCELERATION - this.Current_Ratings.ACCELERATION) * this.PlayerProgRate.ACCELERATION;
            double AGI = (double)(this.PlayerPotential.AGILITY - this.Current_Ratings.AGILITY) * this.PlayerProgRate.AGILITY;
            double JMP = (double)(this.PlayerPotential.JUMPING - this.Current_Ratings.JUMPING) * this.PlayerProgRate.JUMPING;
            double KPW = (double)(this.PlayerPotential.KICK_POWER - this.Current_Ratings.KICK_POWER) * this.PlayerProgRate.KICK_POWER;
            double SPD = (double)(this.PlayerPotential.SPEED - this.Current_Ratings.SPEED) * this.PlayerProgRate.SPEED;
            double STA = (double)(this.PlayerPotential.STAMINA - this.Current_Ratings.STAMINA) * this.PlayerProgRate.STAMINA;
            double STR = (double)(this.PlayerPotential.STRENGTH - this.Current_Ratings.STRENGTH) * this.PlayerProgRate.STRENGTH;
            double THP = (double)(this.PlayerPotential.THROW_POWER - this.Current_Ratings.THROW_POWER) * this.PlayerProgRate.THROW_POWER;

            //  Intanglibles
            double INJ = (double)(this.PlayerPotential.INJURY - this.Current_Ratings.INJURY) * this.PlayerProgRate.INJURY;
            double TGH = (double)(this.PlayerPotential.TOUGHNESS - this.Current_Ratings.TOUGHNESS) * this.PlayerProgRate.TOUGHNESS;

            //  Attitude
            double EGO = (double)(this.PlayerPotential.EGO - this.Current_Ratings.EGO) * this.PlayerProgRate.EGO;
            double MOR = (double)(this.PlayerPotential.MORALE - this.Current_Ratings.MORALE) * this.PlayerProgRate.MORALE;

            //  Setup Coaches modifier for position, motivation and knowledge
            double coach_pos_bonus = team.Coaches[this.TEAM_ID].GetPlayerPositionProgRating(this.POSITION_ID);
            double coach_mot_bonus = team.Coaches[this.TEAM_ID].GetPlayerMotivationProgRating(this.POSITION_ID);
            double coach_awr_bonus = team.Coaches[this.TEAM_ID].GetPlayerKnowledgeProgRating(this.POSITION_ID);

            //  This needs to be adjusted for what progression points we want, right now its taking the difference between the last 2 stats
            int t = this.PlayerSeasonStats.Count - 1;
            double Part = (double)(this.PlayerSeasonStats[t].GAMES_DOWNS_PLAYED - this.PlayerSeasonStats[t - 1].GAMES_DOWNS_PLAYED) / 511;

            #region Bonuses

            #region Work Bonus
            double Work_Bonus = (double)(this.Current_Ratings.WorkEthic - 50) / 196;
            if (Work_Bonus > .250)
                Work_Bonus = .250;
            #endregion

            #region Morale Bonus
            double Morale_Bonus = (MOR - 70) / 196;
            if (Morale_Bonus > .250)
                Morale_Bonus = .250;
            #endregion

            #region Years Pro Bonus
            double YearsPro_Bonus = Math.Pow((3 - this.YRS_PRO), 2) / 36;
            if (YearsPro_Bonus < 0)
                YearsPro_Bonus = 0;
            #endregion

            #region Peer Bonus
            double Peer_Bonus = 0;
            foreach (Player p in team.Players)
            {
                if (p.PLAYER_ID != this.PLAYER_ID && p.POSITION_ID == this.POSITION_ID && p.YRS_PRO > 3)
                {
                    double ment_awr = ((double)(p.Current_Ratings.AWARENESS - 84) / 120);
                    if (ment_awr < -.125)
                        ment_awr = -.125;
                    if (ment_awr > .125)
                        ment_awr = .125;

                    double ment_mor = (double)(75 - p.Current_Ratings.EGO + p.Current_Ratings.MORALE - 80) / 240;
                    if (ment_mor < -.125)
                        ment_mor = -.125;
                    if (ment_mor > .125)
                        ment_mor = .125;

                    Peer_Bonus = ment_awr + ment_mor;
                }
            }

            #endregion

            #endregion



        }

        public double GetAgeEffect(int min1, int max1, int thresh, double val)
        {
            Random ran = new Random();
            if (ran.Next(0, 100) < thresh)
                return (ran.Next(min1, max1) / 100);
            return val;
        }

        public double GetAgeBonus()
        {
            double effect = 0;
            //  Generic effect on physical attributes
            //  21-24   = no change to normal progression                           0 to 1.0
            //  25-28   = No Change or possible progression                         0 to 0.25
            //  29-30   = No change to possible small progression                -.25 to 0
            //  31-34   = No change or possible regression                       -.75 to 0
            //  35-38   = Regression                                             -1.0 to -.25

            if (this.AGE <= 24)
                effect = GetAgeEffect(0, 100, 90, 0);
            else if (this.AGE >= 25 && this.AGE <= 28)
                effect = GetAgeEffect(0, 25, 50, 0);
            else if (this.AGE >= 29 && this.AGE <= 30)
                effect = GetAgeEffect(0, -25, 50, 0);
            else if (this.AGE >= 31 && this.AGE <= 34)
                effect = GetAgeEffect(-75, 0, 70, 0);
            else effect = GetAgeEffect(-100, -25, 90, -.25);

            if (this.POSITION_ID == (int)MaddenPositions.QB)
            {
            }
            else if (this.POSITION_ID == (int)MaddenPositions.HB)
            {
            }
            else if (this.PORTRAIT_ID == (int)MaddenPositions.WR)
            {
            }
            //  O-Line
            else if (this.POSITION_ID == (int)MaddenPositions.C || this.POSITION_ID == (int)MaddenPositions.LT || this.POSITION_ID == (int)MaddenPositions.LG
                || this.POSITION_ID == (int)MaddenPositions.RG || this.POSITION_ID == (int)MaddenPositions.RT)
            {
            }
            // D-Line

            return effect;
        }

        public double GetPasserRating(int season)
        {
            if (this.PlayerSeasonStats[season].GAMES_PLAYED > 0)
            {
                double a = (double)((this.PlayerSeasonStats[season].SEA_COMP / this.PlayerSeasonStats[season].SEA_PASS_ATT) - .3) * 5;
                if (a < 0)
                    a = 0;
                if (a > 2.375)
                    a = 2.375;

                double b = (double)((this.PlayerSeasonStats[season].SEA_PASS_YDS / this.PlayerSeasonStats[season].SEA_PASS_ATT) - 3) * .25;
                if (b < 0)
                    b = 0;
                if (b > 2.375)
                    b = 2.375;

                double c = (double)((this.PlayerSeasonStats[season].SEA_PASS_TD / this.PlayerSeasonStats[season].SEA_PASS_ATT) * 20);
                if (c < 0)
                    c = 0;
                if (c > 2.375)
                    c = 2.375;

                double d = 2.375 - (double)((this.PlayerSeasonStats[season].SEA_PASS_INT / this.PlayerSeasonStats[season].SEA_PASS_ATT) * 25);
                if (d < 0)
                    d = 0;
                if (d > 2.375)
                    d = 2.375;

                double total = ((a + b + c + d) / 6) * 100;
                return Math.Round(total, 1);
            }

            return 0;
        }

        #region File IO

        public void Read(BinaryReader binreader)
        {
            this.AGE = binreader.ReadInt32();                             //"PAGE";
            this.COLLEGE_ID = binreader.ReadInt32();                      //"PCOL";
            this.CONTRACT_LENGTH = binreader.ReadInt32();                 //"PCON";
            this.SALARY_CURRENT = binreader.ReadInt32();                  //"PCSA";
            this.CONTRACT_YRS_LEFT = binreader.ReadInt32();               //"PCYL";
            this.DRAFT_ROUND_INDEX = binreader.ReadInt32();               //"PDPI";
            this.DRAFT_ROUND = binreader.ReadInt32();                     //"PDRO";
            this.FIRST_NAME = binreader.ReadString();                     //"PFNA"
            this.PRO_BOWL = binreader.ReadBoolean();                      //"PFPB";
            this.PLAYER_ID = binreader.ReadInt32();                       //"PGID";
            this.HEIGHT = binreader.ReadInt32();                          //"PHGT";                       Add 65 to this for height in inches
            this.HOLDOUT = binreader.ReadBoolean();                       //"PFHO";                       // ?
            this.NFL_ICON = binreader.ReadBoolean();                      //"PICN";
            this.IMPORTANCE = binreader.ReadInt32();                      //"PIMP";     
            this.LAST_HEALTHY_YEAR = binreader.ReadInt32();               //"PLHY";                       // not sure about this one
            this.LAST_NAME = binreader.ReadString();                      //"PLNA";
            this.NFL_ID = binreader.ReadInt32();                          //"POID";
            this.PREVIOUS_POSITION_ID = binreader.ReadInt32();            //"POPS";
            this.OVERALL = binreader.ReadInt32();                         //"POVR";
            this.POSITION_ID = binreader.ReadInt32();                     //"PPOS";
            this.PREVIOUS_TEAM_ID = binreader.ReadInt32();                //"PPTI";
            this.PLAYER_ROLE = binreader.ReadInt32();                     //"PROL";                       // 2007
            this.PLAYER_WEAPON = binreader.ReadInt32();                   //"PRL2";                       // 2008
            this.SALARY_YEAR_0 = binreader.ReadInt32();                   //"PSA0";
            this.SALARY_YEAR_1 = binreader.ReadInt32();                   //"PSA1";
            this.SALARY_YEAR_2 = binreader.ReadInt32();                   //"PSA2";
            this.SALARY_YEAR_3 = binreader.ReadInt32();                   //"PSA3";
            this.SALARY_YEAR_4 = binreader.ReadInt32();                   //"PSA4";
            this.SALARY_YEAR_5 = binreader.ReadInt32();                   //"PSA5";
            this.SALARY_YEAR_6 = binreader.ReadInt32();                   //"PSA6";
            this.SIGNING_BONUS_YEAR_0 = binreader.ReadInt32();            //"PSB0";
            this.SIGNING_BONUS_YEAR_1 = binreader.ReadInt32();            //"PSB1";
            this.SIGNING_BONUS_YEAR_2 = binreader.ReadInt32();            //"PSB2";
            this.SIGNING_BONUS_YEAR_3 = binreader.ReadInt32();            //"PSB3";
            this.SIGNING_BONUS_YEAR_4 = binreader.ReadInt32();            //"PSB4";
            this.SIGNING_BONUS_YEAR_5 = binreader.ReadInt32();            //"PSB5";
            this.SIGNING_BONUS_YEAR_6 = binreader.ReadInt32();            //"PSB6";
            this.SIGNING_BONUS = binreader.ReadInt32();                   //"PSBO";
            this.PORTRAIT_ID = binreader.ReadInt32();                     //"PSXP";
            this.TOTAL_SALARY = binreader.ReadInt32();                    //"PTSA";                       // 2008
            this.PLAYER_VALUE = binreader.ReadInt32();                    //"PVAL";                       // 2008
            this.PREVIOUS_SIGNING_BONUS_TOTAL = binreader.ReadInt32();    //"PVSB";
            this.WEIGHT = binreader.ReadInt32();                          //"PWGT";                       Add 160 to this for actual weight in pounds
            this.YRS_PRO = binreader.ReadInt32();                         //"PYRP";
            this.TENDENCY = binreader.ReadInt32();                        //"PTEN";
            this.TEAM_ID = binreader.ReadInt32();                         //"TGID";

            //  Ratings and Progression
            Original_Ratings.Read(binreader);
            Current_Ratings.Read(binreader);
            PlayerPotential.Read(binreader);
            PlayerProgRate.Read(binreader);

            
            //  Career Stats
            PlayerCareerStats.Read(binreader);

            //  Season Stats
            int count = binreader.ReadInt32();
            for (int c = 0; c < count; c++)
            {
                SeasonStats sea = new SeasonStats();
                sea.Read(binreader, c);
                this.PlayerSeasonStats.Add(sea);
            }
        }
        
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(AGE);                             //"PAGE";
            binwriter.Write(COLLEGE_ID);                      //"PCOL";
            binwriter.Write(CONTRACT_LENGTH);                 //"PCON";
            binwriter.Write(SALARY_CURRENT);                  //"PCSA";
            binwriter.Write(CONTRACT_YRS_LEFT);               //"PCYL";
            binwriter.Write(DRAFT_ROUND_INDEX);               //"PDPI";
            binwriter.Write(DRAFT_ROUND);                     //"PDRO";
            binwriter.Write(FIRST_NAME);                      //"PFNA"
            binwriter.Write(PRO_BOWL);                        //"PFPB";
            binwriter.Write(PLAYER_ID);                       //"PGID";
            binwriter.Write(HEIGHT);                          //"PHGT";                       Add 65 to this for height in inches
            binwriter.Write(HOLDOUT);                         //"PFHO";                       // ?
            binwriter.Write(NFL_ICON);                        //"PICN";
            binwriter.Write(IMPORTANCE);                      //"PIMP";     
            binwriter.Write(LAST_HEALTHY_YEAR);               //"PLHY";                       // not sure about this one
            binwriter.Write(LAST_NAME);                       //"PLNA";
            binwriter.Write(NFL_ID);                          //"POID";
            binwriter.Write(PREVIOUS_POSITION_ID);            //"POPS";
            binwriter.Write(OVERALL);                         //"POVR";
            binwriter.Write(POSITION_ID);                     //"PPOS";
            binwriter.Write(PREVIOUS_TEAM_ID);                //"PPTI";
            binwriter.Write(PLAYER_ROLE);                     //"PROL";                       // 2007
            binwriter.Write(PLAYER_WEAPON);                   //"PRL2";                       // 2008
            binwriter.Write(SALARY_YEAR_0);                   //"PSA0";
            binwriter.Write(SALARY_YEAR_1);                   //"PSA1";
            binwriter.Write(SALARY_YEAR_2);                   //"PSA2";
            binwriter.Write(SALARY_YEAR_3);                   //"PSA3";
            binwriter.Write(SALARY_YEAR_4);                   //"PSA4";
            binwriter.Write(SALARY_YEAR_5);                   //"PSA5";
            binwriter.Write(SALARY_YEAR_6);                   //"PSA6";
            binwriter.Write(SIGNING_BONUS_YEAR_0);            //"PSB0";
            binwriter.Write(SIGNING_BONUS_YEAR_1);            //"PSB1";
            binwriter.Write(SIGNING_BONUS_YEAR_2);            //"PSB2";
            binwriter.Write(SIGNING_BONUS_YEAR_3);            //"PSB3";
            binwriter.Write(SIGNING_BONUS_YEAR_4);            //"PSB4";
            binwriter.Write(SIGNING_BONUS_YEAR_5);            //"PSB5";
            binwriter.Write(SIGNING_BONUS_YEAR_6);            //"PSB6";
            binwriter.Write(SIGNING_BONUS);                   //"PSBO";
            binwriter.Write(PORTRAIT_ID);                     //"PSXP";
            binwriter.Write(TOTAL_SALARY);                    //"PTSA";                       // 2008
            binwriter.Write(PLAYER_VALUE);                    //"PVAL";                       // 2008
            binwriter.Write(PREVIOUS_SIGNING_BONUS_TOTAL);    //"PVSB";
            binwriter.Write(WEIGHT);                          //"PWGT";                       Add 160 to this for actual weight in pounds
            binwriter.Write(YRS_PRO);                         //"PYRP";
            binwriter.Write(TENDENCY);                        //"PTEN";
            binwriter.Write(TEAM_ID);                         //"TGID";

            // Ratings and Progression
            Original_Ratings.Write(binwriter);
            Current_Ratings.Write(binwriter);
            PlayerPotential.Write(binwriter);
            PlayerProgRate.Write(binwriter);

            //  Career Stats
            this.PlayerCareerStats.Write(binwriter);

            //  Season Stats
            binwriter.Write(this.PlayerSeasonStats.Count);
            foreach (SeasonStats sea in this.PlayerSeasonStats)
                sea.Write(binwriter);
        }



        #endregion
    }

    public class Owner
    {
        public int Ego = 50;
        public int Spending = 50;
        public int Loyalty = 50;
        public int RunPass = 50;
        public int Off_Aggression = 50;
        public int Def_Aggression = 50;
        public int Risk = 50;
        public int Knowledge = 50;
        public int Patience = 50;
        public int FreeAgent_Eval = 50;
        public int Draft_Eval = 50;

        public Owner()
        {
        }

        public void Read(BinaryReader binreader)
        {
            this.Ego = binreader.ReadInt32();
            this.Spending = binreader.ReadInt32();
            this.Loyalty = binreader.ReadInt32();
            this.RunPass = binreader.ReadInt32();
            this.Off_Aggression = binreader.ReadInt32();
            this.Def_Aggression = binreader.ReadInt32();
            this.Risk = binreader.ReadInt32();
            this.Knowledge = binreader.ReadInt32();
            this.Patience = binreader.ReadInt32();
            this.FreeAgent_Eval = binreader.ReadInt32();
            this.Draft_Eval = binreader.ReadInt32();
        }

        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(Ego);
            binwriter.Write(Spending);
            binwriter.Write(Loyalty);
            binwriter.Write(RunPass);
            binwriter.Write(Off_Aggression);
            binwriter.Write(Def_Aggression);
            binwriter.Write(Risk);
            binwriter.Write(Knowledge);
            binwriter.Write(Patience);
            binwriter.Write(FreeAgent_Eval);
            binwriter.Write(Draft_Eval);
        }
        
    }

    public class CoachRatings
    {

    }
    
    public class Coach
    {
        #region Members

        public int AGE = 0;                             //  "CAGE";
        public int BIGGEST_LOSS_MARGIN = 0;             //  "CBLM";
        public int CBSZ = 0;                            //  "CBSZ";
        public int BIGGEST_WIN_MARGIN = 0;              //  "CBWM";
        public int CHEMISTRY = 0;                       //  "CCHM";
        public int COACH_ID = 0;                        //  "CCID";
        public int CONTRACT_LENGTH = 0;                 //  "CCLN";
        public int CAREER_LOSSES = 0;                   //  "CCLO";
        public int CAREER_LONGEST_LOSING_STREAK = 0;    //  "CCLS";
        public int PLAYOFF_LOSSES = 0;                  //  "CCPL";
        public int PLAYOFFS_MADE = 0;                   //  "CCPM";
        public int CCPR = 0;                            //  "CCPR";
        public int PLAYFF_WINS = 0;                     //  "CCPW";
        public int CCTC = 0;                            //  "CCTC";
        public int CAREER_TIES = 0;                     //  "CCTI";
        public int CAREER_WINS = 0;                     //  "CCWI";
        public int WINNING_SEASONS = 0;                 //  "CCWS";
        public int DEFENSE = 0;                         //  "CDEF";
        public int DEFENSIVE_PLAYBOOK = 0;              //  "CDID";
        public int DEF_AGGR = 0;                        //  "CDTA";
        public int DEF_STRAT = 0;                       //  "CDTR";
        public bool DEFENSE_TYPE = false;               //  "CDTY";
        public int CDWS = 0;                            //  "CDWS";
        public int ETHICS = 0;                          //  "CETH";
        public int CFCO = 0;                            //  "CFCO";
        public bool DRAFT_PLAYER = false;               //  "CFDA";
        public bool SIGN_DRAFT_PICKS = false;           //  "CFDP";
        public int CFEX = 0;                            //  "CFEX";
        public bool SIGN_FREE_AGENTS = false;           //  "CFFA";
        public bool FILL_ROSTERS = false;               //  "CFFR";
        public int CFHL = 0;                            //  "CFHL";
        public bool RESIGN_PLAYERS = false;             //  "CFRP";
        public bool MANAGE_DEPTH = false;               //  "CFRR";
        public int CFSH = 0;                            //  "CFSH";
        public bool USER_CONTROLLED = false;            //  "CFUC";
        public int CHAR = 0;                            //  "CHAR";
        public int HEIGHT = 0;                          //  "CHGT";
        public int HEADHAIR_ID = 0;                     //  "CHID";
        public int CHSD = 0;                            //  "CHSD";
        public int CHTY = 0;                            //  "CHTY";
        public int KNOWLEDGE = 0;                       //  "CKNW";
        public int LAST_TEAM = 0;                       //  "CLCT";
        public string NAME = "";                        //  "CLNA";
        public int CLTF = 0;                            //  "CLTF";
        public int CLTR = 0;                            //  "CLTR";
        public int MOTIVATION = 0;                      //  "CMOT";
        public int COAP = 0;                            //  "COAP";
        public int COCI = 0;                            //  "COCI";
        public int COCT = 0;                            //  "COCT";
        public int CODA = 0;                            //  "CODA";
        public int CODP = 0;                            //  "CODP";
        public int COEX = 0;                            //  "COEX";
        public int COFA = 0;                            //  "COFA";
        public int OFFENSE = 0;                         //  "COFF";
        public int COFR = 0;                            //  "COFR";
        public int COPL = 0;                            //  "COPL";
        public int POSITION = 0;                        //  "COPS";
        public int CORP = 0;                            //  "CORP";
        public int CORR = 0;                            //  "CORR";
        public int OFF_AGGR = 0;                        //  "COTA";
        public int OFF_STRAT = 0;                       //  "COTR";
        public int COTY = 0;                            //  "COTY";
        public int CPAG = 0;                            //  "CPAG";
        public int OFFENSE_PLAYBOOK_ID = 0;             //  "CPID";
        public int FaceID = 0;                          //  "CPSF";
        public int CPWS = 0;                            //  "CPWS";
        public int APPROVAL_RATING = 0;                 //  "CRAT";
        public int RB_CARRY_DIST = 0;                   //  "CRBT";
        public int DB_RATING = 0;                       //  "CRDB";
        public int DL_RATING = 0;                       //  "CRDL";
        public int KICK_RATING = 0;                     //  "CRKS";
        public int LB_RATING = 0;                       //  "CRLB";
        public int OL_RATING = 0;                       //  "CROL";
        public int PUNT_RATING = 0;                     //  "CRPS";
        public int QB_RATING = 0;                       //  "CRQB";
        public int RB_RATING = 0;                       //  "CRRB";
        public int S_RATING = 0;                        //  "CRSA";
        public int WR_RATING = 0;                       //  "CRWR";
        public int CRWS = 0;                            //  "CRWS";
        public int CRYL = 0;             //  "CRYL";
        public int SALARY = 0;                          //  "CSAL";
        public int SUPERBOWL_LOSES = 0;                 //  "CSBL";
        public int CSBS = 0;                            //  "CSBS";
        public int SUPERBOWL_WINS = 0;                  //  "CSBW";
        public int SKIN_COLOR = 0;                      //  "CSKI";
        public int CSLM = 0;                            //  "CSLM";
        public int SEASON_LOSSES = 0;                   //  "CSLO";
        public int CSLS = 0;                            //  "CSLS";
        public int CSPA = 0;                            //  "CSPA";
        public int CSPC = 0;                            //  "CSPC";
        public int CSPF = 0;                            //  "CSPF";
        public int SEASON_TIES = 0;                     //  "CSTI";
        public int SEASON_WINS = 0;                     //  "CSWI";
        public int SEASON_BIGGEST_WIN_MARGIN = 0;       //  "CSWM";
        public int SEASON_WINNING_STREAK = 0;           //  "CSWS";
        public int COACHPIC = 0;                        //  "CSXP";
        public bool COACH_GLASSES = false;              //  "CTgw";
        public int CTHG = 0;                            //  "CThg";
        public int CWPL = 0;                            //  "CWPL";
        public int CWST = 0;                            //  "CWST";
        public int CWWS = 0;                            //  "CWWS";
        public int TEAM_ID = 0;                         //  "TGID";

        #endregion

        public Coach()
        {

        }
        public Coach(CoachRecord rec)
        {
            this.AGE = rec.Age;
            this.BIGGEST_LOSS_MARGIN = rec.BiggestLossMargin;
            this.CBSZ = rec.BodySize;
            this.BIGGEST_WIN_MARGIN = rec.BiggestWinMargin;
            this.CHEMISTRY = rec.Chemistry;
            this.COACH_ID = rec.CoachId;
            this.CONTRACT_LENGTH = rec.ContractLength;
            this.CAREER_LOSSES = rec.CareerLosses;
            this.CAREER_LONGEST_LOSING_STREAK = rec.CareerLosingStreak;
            this.PLAYOFF_LOSSES = rec.PlayoffLosses;
            this.PLAYOFFS_MADE = rec.PlayoffsMade;
            this.CCPR = rec.Ccpr;
            this.PLAYFF_WINS = rec.PlayoffWins;
            this.CCTC = rec.Cctc;
            this.CAREER_TIES = rec.CareerTies;
            this.CAREER_WINS = rec.CareerWins;
            this.WINNING_SEASONS = rec.WinningSeasons;
            this.DEFENSE = rec.DefenseRating;
            this.DEFENSIVE_PLAYBOOK = rec.DefensivePlaybook;
            this.DEF_AGGR = rec.DefensiveAggression;
            this.DEF_STRAT = rec.DefensiveStrategy;
            this.DEFENSE_TYPE = rec.DefenseType;
            this.CDWS = rec.Cdws;
            this.ETHICS = rec.Ethics;
            this.CFCO = rec.cfco;
            this.DRAFT_PLAYER = rec.DraftPlayer;
            this.SIGN_DRAFT_PICKS = rec.SignDraftPicks;
            this.CFEX = rec.Cfex;
            this.SIGN_FREE_AGENTS = rec.SignFreeAgents;
            this.FILL_ROSTERS = rec.FillRosters;
            this.CFHL = rec.cfhl;
            this.RESIGN_PLAYERS = rec.ResignPlayers;
            this.MANAGE_DEPTH = rec.ManageDepth;
            this.CFSH = rec.Cfsh;
            this.USER_CONTROLLED = rec.HumanControlled;
            this.CHAR = rec.Char;
            this.HEIGHT = rec.height;
            this.HEADHAIR_ID = rec.HeadHair;
            this.CHSD = rec.Chsd;
            this.CHTY = rec.Chty;
            this.KNOWLEDGE = rec.Knowledge;
            this.LAST_TEAM = rec.LastTeam;
            this.NAME = rec.Name;
            this.CLTF = rec.Cltf;
            this.CLTR = rec.Cltr;
            this.MOTIVATION = rec.Motivation;
            this.COAP = rec.Coap;
            this.COCI = rec.Coci;
            this.COCT = rec.Coct;
            this.CODA = rec.Coda;
            this.CODP = rec.Codp;
            this.COEX = rec.Coex;
            this.COFA = rec.Cofa;
            this.OFFENSE = rec.Offense;
            this.COFR = rec.Cofr;
            this.COPL = rec.Copl;
            this.POSITION = rec.Position;
            this.CORP = rec.Corp;
            this.CORR = rec.Corr;
            this.OFF_AGGR = rec.DefensiveAggression;
            this.OFF_STRAT = rec.OffensiveStrategy;
            this.COTY = rec.Coty;
            this.CPAG = rec.Cpag;
            this.OFFENSE_PLAYBOOK_ID = OFFENSE_PLAYBOOK_ID;
            this.FaceID = rec.FaceId;
            this.CPWS = rec.Cpws;
            this.APPROVAL_RATING = rec.ApprovalRating;
            this.RB_CARRY_DIST = rec.RBCarryDist;
            this.DB_RATING = rec.CoachDB;
            this.DL_RATING = rec.CoachDL;
            this.KICK_RATING = rec.KickerRating;
            this.LB_RATING = rec.CoachLB;
            this.OL_RATING = rec.CoachOL;
            this.PUNT_RATING = rec.PuntRating;
            this.QB_RATING = rec.CoachQB;
            this.RB_RATING = rec.CoachRB;
            this.S_RATING = rec.CoachSafety;
            this.WR_RATING = rec.CoachWR;
            this.CRWS = rec.Crws;
            this.CRYL = rec.Cryl;
            this.SALARY = rec.Salary;
            this.SUPERBOWL_LOSES = rec.SuperBowlLoses;
            this.CSBS = rec.Csbs;
            this.SUPERBOWL_WINS = rec.SuperBowlWins;
            this.SKIN_COLOR = rec.SkinColor;
            this.CSLM = rec.Cslm;
            this.SEASON_LOSSES = rec.SeasonLosses;
            this.CSLS = rec.Csls;
            this.CSPA = rec.Cspa;
            this.CSPC = rec.Cspc;
            this.CSPF = rec.Cspf;
            this.SEASON_TIES = rec.SeasonTies;
            this.SEASON_WINS = rec.SeasonWins;
            this.SEASON_BIGGEST_WIN_MARGIN = rec.SeasonBigWin;
            this.SEASON_WINNING_STREAK = rec.SeasonWinStreak;
            this.COACHPIC = rec.Coachpic;
            this.COACH_GLASSES = rec.CoachGlasses;
            this.CTHG = rec.Cthg;
            this.CWPL = rec.Cwpl;
            this.CWST = rec.Cwst;
            this.CWWS = rec.Cwws;
            this.TEAM_ID = rec.TeamId;
        }

        #region File IO
        public void Read(BinaryReader binreader)
        {
            this.AGE = binreader.ReadInt32();                              //  "CAGE";
            this.BIGGEST_LOSS_MARGIN = binreader.ReadInt32();              //  "CBLM";
            this.CBSZ = binreader.ReadInt32();                             //  "CBSZ";
            this.BIGGEST_WIN_MARGIN = binreader.ReadInt32();               //  "CBWM";
            this.CHEMISTRY = binreader.ReadInt32();                        //  "CCHM";
            this.COACH_ID = binreader.ReadInt32();                         //  "CCID";
            this.CONTRACT_LENGTH = binreader.ReadInt32();                  //  "CCLN";
            this.CAREER_LOSSES = binreader.ReadInt32();                    //  "CCLO";
            this.CAREER_LONGEST_LOSING_STREAK = binreader.ReadInt32();     //  "CCLS";
            this.PLAYOFF_LOSSES = binreader.ReadInt32();                   //  "CCPL";
            this.PLAYOFFS_MADE = binreader.ReadInt32();                    //  "CCPM";
            this.CCPR = binreader.ReadInt32();                             //  "CCPR";
            this.PLAYFF_WINS = binreader.ReadInt32();                      //  "CCPW";
            this.CCTC = binreader.ReadInt32();                             //  "CCTC";
            this.CAREER_TIES = binreader.ReadInt32();                      //  "CCTI";
            this.CAREER_WINS = binreader.ReadInt32();                      //  "CCWI";
            this.WINNING_SEASONS = binreader.ReadInt32();                  //  "CCWS";
            this.DEFENSE = binreader.ReadInt32();                          //  "CDEF";
            this.DEFENSIVE_PLAYBOOK = binreader.ReadInt32();               //  "CDID";
            this.DEF_AGGR = binreader.ReadInt32();                         //  "CDTA";
            this.DEF_STRAT = binreader.ReadInt32();                         //  "CDTR";
            this.DEFENSE_TYPE = binreader.ReadBoolean();                    //  "CDTY";
            this.CDWS = binreader.ReadInt32();                              //  "CDWS";
            this.ETHICS = binreader.ReadInt32();                            //  "CETH";
            this.CFCO = binreader.ReadInt32();                              //  "CFCO";
            this.DRAFT_PLAYER = binreader.ReadBoolean();                    //  "CFDA";
            this.SIGN_DRAFT_PICKS = binreader.ReadBoolean();                //  "CFDP";
            this.CFEX = binreader.ReadInt32();                              //  "CFEX";
            this.SIGN_FREE_AGENTS = binreader.ReadBoolean();                //  "CFFA";
            this.FILL_ROSTERS = binreader.ReadBoolean();                    //  "CFFR";
            this.CFHL = binreader.ReadInt32();                              //  "CFHL";
            this.RESIGN_PLAYERS = binreader.ReadBoolean();                  //  "CFRP";
            this.MANAGE_DEPTH = binreader.ReadBoolean();                    //  "CFRR";
            this.CFSH = binreader.ReadInt32();                              //  "CFSH";
            this.USER_CONTROLLED = binreader.ReadBoolean();                 //  "CFUC";
            this.CHAR = binreader.ReadInt32();                              //  "CHAR";
            this.HEIGHT = binreader.ReadInt32();                             //  "CHGT";
            this.HEADHAIR_ID = binreader.ReadInt32();                       //  "CHID";
            this.CHSD = binreader.ReadInt32();                              //  "CHSD";
            this.CHTY = binreader.ReadInt32();                              //  "CHTY";
            this.KNOWLEDGE = binreader.ReadInt32();                         //  "CKNW";
            this.LAST_TEAM = binreader.ReadInt32();                         //  "CLCT";
            this.NAME = binreader.ReadString();                             //  "CLNA";
            this.CLTF = binreader.ReadInt32();                             //  "CLTF";
            this.CLTR = binreader.ReadInt32();                             //  "CLTR";
            this.MOTIVATION = binreader.ReadInt32();                       //  "CMOT";
            this.COAP = binreader.ReadInt32();                             //  "COAP";
            this.COCI = binreader.ReadInt32();                             //  "COCI";
            this.COCT = binreader.ReadInt32();                             //  "COCT";
            this.CODA = binreader.ReadInt32();                             //  "CODA";
            this.CODP = binreader.ReadInt32();                             //  "CODP";
            this.COEX = binreader.ReadInt32();                             //  "COEX";
            this.COFA = binreader.ReadInt32();                             //  "COFA";
            this.OFFENSE = binreader.ReadInt32();                          //  "COFF";
            this.COFR = binreader.ReadInt32();                             //  "COFR";
            this.COPL = binreader.ReadInt32();                             //  "COPL";
            this.POSITION = binreader.ReadInt32();                         //  "COPS";
            this.CORP = binreader.ReadInt32();                             //  "CORP";
            this.CORR = binreader.ReadInt32();                             //  "CORR";
            this.OFF_AGGR = binreader.ReadInt32();                         //  "COTA";
            this.OFF_STRAT = binreader.ReadInt32();                        //  "COTR";
            this.COTY = binreader.ReadInt32();                             //  "COTY";
            this.CPAG = binreader.ReadInt32();                             //  "CPAG";
            this.OFFENSE_PLAYBOOK_ID = binreader.ReadInt32();              //  "CPID";
            this.FaceID = binreader.ReadInt32();                           //  "CPSF";
            this.CPWS = binreader.ReadInt32();                             //  "CPWS";
            this.APPROVAL_RATING = binreader.ReadInt32();                  //  "CRAT";
            this.RB_CARRY_DIST = binreader.ReadInt32();                    //  "CRBT";
            this.DB_RATING = binreader.ReadInt32();                        //  "CRDB";
            this.DL_RATING = binreader.ReadInt32();                        //  "CRDL";
            this.KICK_RATING = binreader.ReadInt32();                      //  "CRKS";
            this.LB_RATING = binreader.ReadInt32();                        //  "CRLB";
            this.OL_RATING = binreader.ReadInt32();                        //  "CROL";
            this.PUNT_RATING = binreader.ReadInt32();                      //  "CRPS";
            this.QB_RATING = binreader.ReadInt32();                        //  "CRQB";
            this.RB_RATING = binreader.ReadInt32();                        //  "CRRB";
            this.S_RATING = binreader.ReadInt32();                             //  "CRSA";
            this.WR_RATING = binreader.ReadInt32();                        //  "CRWR";
            this.CRWS = binreader.ReadInt32();                             //  "CRWS";
            this.CRYL = binreader.ReadInt32();              //  "CRYL";
            this.SALARY = binreader.ReadInt32();                           //  "CSAL";
            this.SUPERBOWL_LOSES = binreader.ReadInt32();                  //  "CSBL";
            this.CSBS = binreader.ReadInt32();                             //  "CSBS";
            this.SUPERBOWL_WINS = binreader.ReadInt32();                   //  "CSBW";
            this.SKIN_COLOR = binreader.ReadInt32();                       //  "CSKI";
            this.CSLM = binreader.ReadInt32();                             //  "CSLM";
            this.SEASON_LOSSES = binreader.ReadInt32();                    //  "CSLO";
            this.CSLS = binreader.ReadInt32();                             //  "CSLS";
            this.CSPA = binreader.ReadInt32();                             //  "CSPA";
            this.CSPC = binreader.ReadInt32();                             //  "CSPC";
            this.CSPF = binreader.ReadInt32();                             //  "CSPF";
            this.SEASON_TIES = binreader.ReadInt32();                      //  "CSTI";
            this.SEASON_WINS = binreader.ReadInt32();                      //  "CSWI";
            this.SEASON_BIGGEST_WIN_MARGIN = binreader.ReadInt32();        //  "CSWM";
            this.SEASON_WINNING_STREAK = binreader.ReadInt32();            //  "CSWS";
            this.COACHPIC = binreader.ReadInt32();                         //  "CSXP";
            this.COACH_GLASSES = binreader.ReadBoolean();                  //  "CTgw";
            this.CTHG = binreader.ReadInt32();                             //  "CThg";
            this.CWPL = binreader.ReadInt32();                             //  "CWPL";
            this.CWST = binreader.ReadInt32();                             //  "CWST";
            this.CWWS = binreader.ReadInt32();                             //  "CWWS";
            this.TEAM_ID = binreader.ReadInt32();                          //  "TGID";
        }                
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write(AGE);
            binwriter.Write(BIGGEST_LOSS_MARGIN);
            binwriter.Write(CBSZ);
            binwriter.Write(BIGGEST_WIN_MARGIN);
            binwriter.Write(CHEMISTRY);
            binwriter.Write(COACH_ID);
            binwriter.Write(CONTRACT_LENGTH);
            binwriter.Write(CAREER_LOSSES);
            binwriter.Write(CAREER_LONGEST_LOSING_STREAK);
            binwriter.Write(PLAYOFF_LOSSES);
            binwriter.Write(PLAYOFFS_MADE);
            binwriter.Write(CCPR);
            binwriter.Write(PLAYFF_WINS);
            binwriter.Write(CCTC);
            binwriter.Write(CAREER_TIES);
            binwriter.Write(CAREER_WINS);
            binwriter.Write(WINNING_SEASONS);
            binwriter.Write(DEFENSE);
            binwriter.Write(DEFENSIVE_PLAYBOOK);
            binwriter.Write(DEF_AGGR);
            binwriter.Write(DEF_STRAT);
            binwriter.Write(DEFENSE_TYPE);
            binwriter.Write(CDWS);
            binwriter.Write(ETHICS);
            binwriter.Write(CFCO);
            binwriter.Write(DRAFT_PLAYER);
            binwriter.Write(SIGN_DRAFT_PICKS);
            binwriter.Write(CFEX);
            binwriter.Write(SIGN_FREE_AGENTS);
            binwriter.Write(FILL_ROSTERS);
            binwriter.Write(CFHL);
            binwriter.Write(RESIGN_PLAYERS);
            binwriter.Write(MANAGE_DEPTH);
            binwriter.Write(CFSH);
            binwriter.Write(USER_CONTROLLED);
            binwriter.Write(CHAR);
            binwriter.Write(HEIGHT);
            binwriter.Write(HEADHAIR_ID);
            binwriter.Write(CHSD);
            binwriter.Write(CHTY);
            binwriter.Write(KNOWLEDGE);
            binwriter.Write(LAST_TEAM);
            binwriter.Write(NAME);
            binwriter.Write(CLTF);
            binwriter.Write(CLTR);
            binwriter.Write(MOTIVATION);
            binwriter.Write(COAP);
            binwriter.Write(COCI);
            binwriter.Write(COCT);
            binwriter.Write(CODA);
            binwriter.Write(CODP);
            binwriter.Write(COEX);
            binwriter.Write(COFA);
            binwriter.Write(OFFENSE);
            binwriter.Write(COFR);
            binwriter.Write(COPL);
            binwriter.Write(POSITION);
            binwriter.Write(CORP);
            binwriter.Write(CORR);
            binwriter.Write(OFF_AGGR);
            binwriter.Write(OFF_STRAT);
            binwriter.Write(COTY);
            binwriter.Write(CPAG);
            binwriter.Write(OFFENSE_PLAYBOOK_ID);
            binwriter.Write(FaceID);
            binwriter.Write(CPWS);
            binwriter.Write(APPROVAL_RATING);
            binwriter.Write(RB_CARRY_DIST);
            binwriter.Write(DB_RATING);
            binwriter.Write(DL_RATING);
            binwriter.Write(KICK_RATING);
            binwriter.Write(LB_RATING);
            binwriter.Write(OL_RATING);
            binwriter.Write(PUNT_RATING);
            binwriter.Write(QB_RATING);
            binwriter.Write(RB_RATING);
            binwriter.Write(S_RATING);
            binwriter.Write(WR_RATING);
            binwriter.Write(CRWS);
            binwriter.Write(CRYL);
            binwriter.Write(SALARY);
            binwriter.Write(SUPERBOWL_LOSES);
            binwriter.Write(CSBS);
            binwriter.Write(SUPERBOWL_WINS);
            binwriter.Write(SKIN_COLOR);
            binwriter.Write(CSLM);
            binwriter.Write(SEASON_LOSSES);
            binwriter.Write(CSLS);
            binwriter.Write(CSPA);
            binwriter.Write(CSPC);
            binwriter.Write(CSPF);
            binwriter.Write(SEASON_TIES);
            binwriter.Write(SEASON_WINS);
            binwriter.Write(SEASON_BIGGEST_WIN_MARGIN);
            binwriter.Write(SEASON_WINNING_STREAK);
            binwriter.Write(COACHPIC);
            binwriter.Write(COACH_GLASSES);
            binwriter.Write(CTHG);
            binwriter.Write(CWPL);
            binwriter.Write(CWST);
            binwriter.Write(CWWS);
            binwriter.Write(TEAM_ID);
        }
        #endregion

        public void UpdateCoach(CoachRecord rec)
        {
            this.AGE = rec.Age;
            this.BIGGEST_LOSS_MARGIN = rec.BiggestLossMargin;
            this.CBSZ = rec.BodySize;
            this.BIGGEST_WIN_MARGIN = rec.BiggestWinMargin;
            this.CHEMISTRY = rec.Chemistry;
            this.COACH_ID = rec.CoachId;
            this.CONTRACT_LENGTH = rec.ContractLength;
            this.CAREER_LOSSES = rec.CareerLosses;
            this.CAREER_LONGEST_LOSING_STREAK = rec.CareerLosingStreak;
            this.PLAYOFF_LOSSES = rec.PlayoffLosses;
            this.PLAYOFFS_MADE = rec.PlayoffsMade;
            this.CCPR = rec.Ccpr;
            this.PLAYFF_WINS = rec.PlayoffWins;
            this.CCTC = rec.Cctc;
            this.CAREER_TIES = rec.CareerTies;
            this.CAREER_WINS = rec.CareerWins;
            this.WINNING_SEASONS = rec.WinningSeasons;
            this.DEFENSE = rec.DefenseRating;
            this.DEFENSIVE_PLAYBOOK = rec.DefensivePlaybook;
            this.DEF_AGGR = rec.DefensiveAggression;
            this.DEF_STRAT = rec.DefensiveStrategy;
            this.DEFENSE_TYPE = rec.DefenseType;
            this.CDWS = rec.Cdws;
            this.ETHICS = rec.Ethics;
            this.CFCO = rec.cfco;
            this.DRAFT_PLAYER = rec.DraftPlayer;
            this.SIGN_DRAFT_PICKS = rec.SignDraftPicks;
            this.CFEX = rec.Cfex;
            this.SIGN_FREE_AGENTS = rec.SignFreeAgents;
            this.FILL_ROSTERS = rec.FillRosters;
            this.CFHL = rec.cfhl;
            this.RESIGN_PLAYERS = rec.ResignPlayers;
            this.MANAGE_DEPTH = rec.ManageDepth;
            this.CFSH = rec.Cfsh;
            this.USER_CONTROLLED = rec.HumanControlled;
            this.CHAR = rec.Char;
            this.HEIGHT = rec.height;
            this.HEADHAIR_ID = rec.HeadHair;
            this.CHSD = rec.Chsd;
            this.CHTY = rec.Chty;
            this.KNOWLEDGE = rec.Knowledge;
            this.LAST_TEAM = rec.LastTeam;
            this.NAME = rec.Name;
            this.CLTF = rec.Cltf;
            this.CLTR = rec.Cltr;
            this.MOTIVATION = rec.Motivation;
            this.COAP = rec.Coap;
            this.COCI = rec.Coci;
            this.COCT = rec.Coct;
            this.CODA = rec.Coda;
            this.CODP = rec.Codp;
            this.COEX = rec.Coex;
            this.COFA = rec.Cofa;
            this.OFFENSE = rec.Offense;
            this.COFR = rec.Cofr;
            this.COPL = rec.Copl;
            this.POSITION = rec.Position;
            this.CORP = rec.Corp;
            this.CORR = rec.Corr;
            this.OFF_AGGR = rec.DefensiveAggression;
            this.OFF_STRAT = rec.OffensiveStrategy;
            this.COTY = rec.Coty;
            this.CPAG = rec.Cpag;
            this.OFFENSE_PLAYBOOK_ID = OFFENSE_PLAYBOOK_ID;
            this.FaceID = rec.FaceId;
            this.CPWS = rec.Cpws;
            this.APPROVAL_RATING = rec.ApprovalRating;
            this.RB_CARRY_DIST = rec.RBCarryDist;
            this.DB_RATING = rec.CoachDB;
            this.DL_RATING = rec.CoachDL;
            this.KICK_RATING = rec.KickerRating;
            this.LB_RATING = rec.CoachLB;
            this.OL_RATING = rec.CoachOL;
            this.PUNT_RATING = rec.PuntRating;
            this.QB_RATING = rec.CoachQB;
            this.RB_RATING = rec.CoachRB;
            this.S_RATING = rec.CoachSafety;
            this.WR_RATING = rec.CoachWR;
            this.CRWS = rec.Crws;
            this.CRYL = rec.Cryl;
            this.SALARY = rec.Salary;
            this.SUPERBOWL_LOSES = rec.SuperBowlLoses;
            this.CSBS = rec.Csbs;
            this.SUPERBOWL_WINS = rec.SuperBowlWins;
            this.SKIN_COLOR = rec.SkinColor;
            this.CSLM = rec.Cslm;
            this.SEASON_LOSSES = rec.SeasonLosses;
            this.CSLS = rec.Csls;
            this.CSPA = rec.Cspa;
            this.CSPC = rec.Cspc;
            this.CSPF = rec.Cspf;
            this.SEASON_TIES = rec.SeasonTies;
            this.SEASON_WINS = rec.SeasonWins;
            this.SEASON_BIGGEST_WIN_MARGIN = rec.SeasonBigWin;
            this.SEASON_WINNING_STREAK = rec.SeasonWinStreak;
            this.COACHPIC = rec.Coachpic;
            this.COACH_GLASSES = rec.CoachGlasses;
            this.CTHG = rec.Cthg;
            this.CWPL = rec.Cwpl;
            this.CWST = rec.Cwst;
            this.CWWS = rec.Cwws;
            this.TEAM_ID = rec.TeamId;
        }
        
        public double GetPlayerPositionProgRating(int pos)
        {
            if (pos == (int)MaddenPositions.QB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.QB_RATING * .30);                        
                    case 1:
                        return (double)(this.QB_RATING * .60);                    
                    case 3:
                        return (double)(this.QB_RATING * .10);  
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.HB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.RB_RATING * .30);
                    case 1:
                        return (double)(this.RB_RATING * .60);
                    case 3:
                        return (double)(this.RB_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.WR)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.WR_RATING * .30);
                    case 1:
                        return (double)(this.WR_RATING * .60);
                    case 3:
                        return (double)(this.WR_RATING * .10);
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.FB)
            {
                int rating = ((this.OL_RATING * 3) + (this.RB_RATING) + (this.WR_RATING * 2)) / 6;
                
                switch (this.POSITION)
                {
                    case 0:
                        return (double)rating * .30;
                    case 1:
                        return (double)rating * .60;
                    case 3:
                        return (double)rating * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.TE)
            {
                int rating = ((this.OL_RATING * 2) + (this.WR_RATING * 3)) / 5;

                switch (this.POSITION)
                {
                    case 0 :
                        return (double)rating * .30;
                    case 1:
                        return (double)rating * .60;
                    case 3:
                        return (double)rating * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.C || pos == (int)MaddenPositions.LG || pos == (int)MaddenPositions.LT || pos == (int)MaddenPositions.RG || pos == (int)MaddenPositions.RT)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.OL_RATING) * .30;
                    case 1:
                        return (double)(this.OL_RATING) * .60;
                    case 3:
                        return (double)(this.OL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.DT || pos == (int)MaddenPositions.LE || pos == (int)MaddenPositions.RE)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.DL_RATING) * .30;
                    case 2:
                        return (double)(this.DL_RATING) * .60;
                    case 3:
                        return (double)(this.DL_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.LOLB || pos == (int)MaddenPositions.MLB || pos == (int)MaddenPositions.ROLB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.LB_RATING) * .30;
                    case 2:
                        return (double)(this.LB_RATING) * .60;
                    case 3:
                        return (double)(this.LB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.SS || pos == (int)MaddenPositions.FS)
            {
                switch (this.S_RATING)
                {
                    case 0:
                        return (double)(this.S_RATING) * .30;
                    case 2:
                        return (double)(this.S_RATING) * .60;
                    case 3:
                        return (double)(this.S_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.CB)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.DB_RATING) * .30;
                    case 2:
                        return (double)(this.DB_RATING) * .60;
                    case 3:
                        return (double)(this.DB_RATING) * .10;
                    default:
                        return 0;
                }
            }
            else if (pos == (int)MaddenPositions.K)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KICK_RATING) * .20;                    
                    case 3:
                        return (double)(this.KICK_RATING) * .60;
                    default:
                        return (double)(this.KICK_RATING) * .10;
                }
            }
            else if (pos == (int)MaddenPositions.P)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.PUNT_RATING) * .20;                    
                    case 3:
                        return (double)(this.PUNT_RATING) * .60;
                    default :
                        return (double)(this.PUNT_RATING) * .10;
                }
            }

            else return 0;
        }
        
        public double GetPlayerMotivationProgRating(int pos)
        {
            if (pos <= 9)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION * .30);
                    case 1:
                        return (double)(this.MOTIVATION * .60);
                    case 3:
                        return (double)(this.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION * .30);
                    case 2:
                        return (double)(this.MOTIVATION * .60);
                    case 3:
                        return (double)(this.MOTIVATION * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.MOTIVATION) * .30;
                    case 1:
                        return (double)(this.MOTIVATION) * .05;
                    case 2:
                        return (double)(this.MOTIVATION) * .05;
                    case 3:
                        return (double)(this.MOTIVATION) * .60;
                    default:
                        return 0;
                }
            }
            else return 0;
        }
        
        public double GetPlayerKnowledgeProgRating(int pos)
        {
            if (pos <= 9)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE * .30);
                    case 1:
                        return (double)(this.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10 && pos <= 18)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE * .30);
                    case 2:
                        return (double)(this.KNOWLEDGE * .60);
                    case 3:
                        return (double)(this.KNOWLEDGE * .10);
                    default:
                        return 0;
                }
            }
            else if (pos >= 10)
            {
                switch (this.POSITION)
                {
                    case 0:
                        return (double)(this.KNOWLEDGE) * .30;
                    case 1:
                        return (double)(this.KNOWLEDGE) * .05;
                    case 2:
                        return (double)(this.KNOWLEDGE) * .05;
                    case 3:
                        return (double)(this.KNOWLEDGE) * .60;
                    default:
                        return 0;
                }
            }
            else return 0;
        }
    }

    public class Priorities
    {
        public Dictionary<int, decimal> Position_Values;
               
        public Priorities()
        {
            Position_Values = new Dictionary<int, decimal>();
        }
    }
    
    public class Team
    {
        #region Members

        public int Team_Id = 0;
        public List<Player> Players;
        public List<Coach> Coaches;
        public Owner Owner_GM;
        
        public List<Player_Ratings> Evals;
        public List<Player_Ratings> Rookie_Scouting;

        public TeamAvg TeamAverages;

        #endregion

        #region Constructors

        public Team()
        {
            Team_Id = 1009;                          //  Free Agents
            Players = new List<Player>();
            Coaches = new List<Coach>();
            Owner_GM = new Owner();

            Evals = new List<Player_Ratings>();
            Rookie_Scouting = new List<Player_Ratings>();


        }
        public Team(int teamid)
        {
            Team_Id = teamid;
            Players = new List<Player>();
            Coaches = new List<Coach>();
            Owner_GM = new Owner();

            Evals = new List<Player_Ratings>();
            Rookie_Scouting = new List<Player_Ratings>();           
        }

        #endregion

        public double GetCoaches_PlayerSkillBonus(Player player)
        {
            double bonus = 0;
            foreach (Coach coach in Coaches)
                bonus += coach.GetPlayerPositionProgRating(player.POSITION_ID);

            return bonus;
        }
        
        public double GetCoaches_PlayerMotivationBonus(Player player)
        {
            double bonus = 0;
            foreach (Coach coach in Coaches)
                bonus += coach.GetPlayerMotivationProgRating(player.POSITION_ID);

            return bonus;
        }
        
        public double GetCoaches_PlayerKnowledgeBonus(Player player)
        {
            double bonus = 0;
            foreach (Coach coach in Coaches)
                bonus += coach.GetPlayerKnowledgeProgRating(player.POSITION_ID);

            return bonus;
        }

        
        public double GetAvgSalary(int position, LeagueAvg LA)
        {
            int play = 0;
            int total = 0;
            for (int c = 0; c < LA.PlayerAverages.Count; c++)
            {
                if (position == LA.PlayerAverages[c].Pos)
                {
                    play++;
                    total += LA.PlayerAverages[c].CurrentSalary;
                }
            }
            return (double)(total) / play;
        }
        
        public double GetPerceivedValue(Player player, EditorModel emodel)
        {
            double playervalue = 0;

            // Player Position
            // depth at position
            // Player Age
            // Player past stats
            // Caoches' perceived player OVR
            // Player Salary
            // Free Agents available
            // Draft pick positions
            // Offense type
            // Defense type

            return playervalue;
        }
        
        public double GetError(double scout)
        {
            double err = scout * 100;

            int low = (int)(10000 - err) / 2;
            int high = 10000 - low;
            Random random = new Random();
            int check = random.Next(1, 10000);
            if (check <= low)
                return (random.NextDouble() * 7.0) - 10;
            if (check >= high)
                return (random.NextDouble() * 7.0) + 3;
            else
            {
                if (check <= 5000)
                    return (random.NextDouble() * -2.0);
                else return (random.NextDouble() * 2.0);
            }
        }
        public void EvaluatePlayer(Player player)
        {
            Player_Ratings scouted = new Player_Ratings();       

            double pos_error = GetCoaches_PlayerSkillBonus(player);

            scouted.ACCELERATION += (int)(player.Current_Ratings.ACCELERATION * GetError(pos_error)/100);
            scouted.AGILITY += (int)(player.Current_Ratings.AGILITY * GetError(pos_error) / 100);
            scouted.AWARENESS += (int)(player.Current_Ratings.AWARENESS * GetError(pos_error) / 100);
            scouted.BREAK_TACKLE += (int)(player.Current_Ratings.BREAK_TACKLE * GetError(pos_error) / 100);
            scouted.CARRYING += (int)(player.Current_Ratings.CARRYING * GetError(pos_error) / 100);
            scouted.CATCHING += (int)(player.Current_Ratings.CATCHING * GetError(pos_error) / 100);
            scouted.JUMPING += (int)(player.Current_Ratings.JUMPING * GetError(pos_error) / 100);
            scouted.KICK_ACCURACY += (int)(player.Current_Ratings.KICK_ACCURACY * GetError(pos_error) / 100);
            scouted.KICK_POWER += (int)(player.Current_Ratings.KICK_POWER * GetError(pos_error) / 100);
            scouted.KICK_RETURN += (int)(player.Current_Ratings.KICK_RETURN * GetError(pos_error) / 100);
            scouted.PASS_BLOCKING += (int)(player.Current_Ratings.PASS_BLOCKING * GetError(pos_error) / 100);
            scouted.RUN_BLOCKING += (int)(player.Current_Ratings.RUN_BLOCKING * GetError(pos_error) / 100);
            scouted.SPEED += (int)(player.Current_Ratings.SPEED * GetError(pos_error) / 100);
            scouted.STRENGTH += (int)(player.Current_Ratings.STRENGTH * GetError(pos_error) / 100);
            scouted.TACKLE += (int)(player.Current_Ratings.TACKLE * GetError(pos_error) / 100);
            scouted.THROW_ACCURACY += (int)(player.Current_Ratings.THROW_ACCURACY * GetError(pos_error) / 100);
            scouted.THROW_POWER += (int)(player.Current_Ratings.THROW_POWER * GetError(pos_error) / 100);

            this.Evals.Add(scouted);
        }

    }

    public class TeamAvg
    {
        public TeamRecord teamrecord;
        public SeasonStatsTeamRecord teamstats;
        public List<BoxScoreOffenseRecord> offense;
        public List<BoxScoreDefenseRecord> defense;

        public TeamAvg()
        {
        }
        public void Set_TeamRecord(TeamRecord rec)
        {
            teamrecord = rec;
        }
        public void Set_TeamStats(SeasonStatsTeamRecord rec)
        {
            teamstats = rec;
        }
        public void Set_TeamOffense(BoxScoreOffenseRecord rec)
        {
            if (this.offense == null)
                this.offense = new List<BoxScoreOffenseRecord>();
            this.offense.Add(rec);
        }
        public void Set_TeamDefense(BoxScoreDefenseRecord rec)
        {
            if (this.defense == null)
                this.defense = new List<BoxScoreDefenseRecord>();
            this.defense.Add(rec);
        }

    }
      
    public class PlayAvg
    {
        public int Pos = -1;
        public int PlayerId = -1;
        public int CurrentSalary = -1;
        public int ContractLength = -1;
        public int ContractSalary = -1;
        public int ContractBonus = -1;
        public int DraftRound = -1;
        public int DraftPick = -1;
        

        public PlayAvg()
        {            
        }
        public PlayAvg(Player player)
        {
            Pos = player.POSITION_ID;
            PlayerId = player.PLAYER_ID;
            CurrentSalary = player.SALARY_CURRENT;
            ContractLength = player.CONTRACT_LENGTH;
            ContractSalary = player.TOTAL_SALARY;
            if (player.SIGNING_BONUS == 0)
                ContractBonus = player.PREVIOUS_SIGNING_BONUS_TOTAL;
            else ContractBonus = player.SIGNING_BONUS;
            DraftRound = player.DRAFT_ROUND;
            DraftPick = player.DRAFT_ROUND_INDEX;
        }
    }
    
    public class LeagueAvg
    {
        public List<int> ElitePlayers;
        public List<int> TotalPlayers;

        public List<PlayAvg> PlayerAverages;

        public LeagueAvg()
        {
            ElitePlayers = new List<int>();
            for (int c = 0; c < 21; c++)
                ElitePlayers.Add(0);
            TotalPlayers = new List<int>();
            for (int c = 0; c < 21; c++)
                TotalPlayers.Add(0);
            PlayerAverages = new List<PlayAvg>();
        }

        public void AddPlayer(Player player)
        {
            this.TotalPlayers[player.POSITION_ID]++;
            if (player.OVERALL >= 90)            
                this.ElitePlayers[player.POSITION_ID]++;
            PlayerAverages.Add(new PlayAvg(player));            
        }
        public void Init(List<Player> players)
        {
            this.ElitePlayers = new List<int>();
            TotalPlayers = new List<int>();
            this.PlayerAverages = new List<PlayAvg>();
            foreach (Player p in players)
                AddPlayer(p); 
        }
        public double GetEliteValue(int position)
        {
            return (double)TotalPlayers[position] / (double)ElitePlayers[position];
        }

    }



    public class MGMT
    {
        #region members

        public List<Player> Players;
        public List<Coach> Coaches;
        public List<Owner> OwnersGms;
        public List<Team> Teams;        

        public EditorModel model = null;
        public LeagueAvg LeaguesAverages;
        public static string Manager_filename = Application.StartupPath + "MGMT.AMP";

        private BackgroundWorker Main;
        private BackgroundWorker Loader;
        private BackgroundWorker Functions;
        public bool loaded = false;
        public bool workdone = false;
        public bool saved = false;

        #endregion

        public MGMT()
        {
            Players = new List<Player>();            
            Coaches = new List<Coach>();
            OwnersGms = new List<Owner>();
            Teams = new List<Team>();

            LeaguesAverages = new LeagueAvg();
            
            Main = new BackgroundWorker();
            Loader = new BackgroundWorker();
            Functions = new BackgroundWorker();

            model = null;
        }

        public void SetModel(EditorModel emodel)
        {
            this.model = emodel;            
        }
        public void InitMain()
        {
            if (File.Exists(Manager_filename))
                this.Load();
            if (!loaded)
                this.CreateDatabase();
            this.InitTeams();  

            if (!File.Exists(Manager_filename))
                this.Save();

            this.LeaguesAverages.Init(this.Players);           
        }

        public void CreateDatabase()
        {
            //  Create database for the first time
            this.Players = new List<Player>();
            for (int c = 0; c < model.TableModels[EditorModel.PLAYER_TABLE].RecordCount; c++)
            {
                PlayerRecord pr = model.PlayerModel.GetPlayerRecord(c);
                this.Players.Add(new Player(pr));

                this.Players[c].UpdateCareerStats(this.model);

                if (this.Players[c].PlayerCareerStats.GAMES_PLAYED > 0)
                    this.Players[c].UpdateSeasonStats(this.model);
            }

            this.Coaches = new List<Coach>();
            for (int c = 0; c < model.TableModels[EditorModel.COACH_TABLE].RecordCount; c++)
            {
                this.Coaches.Add(new Coach(model.CoachModel.GetCoachRecord(c)));
            }
        }
        public void UpdatePlayers()
        {
            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                bool exists = false;
                foreach (Player p in this.Players)
                    if (p.PLAYER_ID == rec.PlayerId)
                    {
                        exists = true;
                        p.UpdateCurrentRatings(rec);
                        p.UpdateCareerStats(this.model);
                        p.UpdateSeasonStats(this.model);
                        break;
                    }
                if (!exists)
                    this.Players.Add(new Player(rec));

            }   
        }
        public void UpdateCoaches()
        {
            foreach (CoachRecord rec in model.TableModels[EditorModel.COACH_TABLE].GetRecords())
            {
                bool exists = false;
                foreach (Coach c in this.Coaches)
                    if (c.COACH_ID == rec.CoachId)
                    {
                        c.UpdateCoach(rec);
                        exists = true;
                        break;
                    }
                if (!exists)
                    this.Coaches.Add(new Coach(rec));
            }
        }
        public void InitTeams()
        {
            Teams = new List<Team>();
            for (int c = 0; c < 32; c++)
                Teams.Add(new Team(c));
            Teams.Add(new Team(1009));                                      //  32 Free agents or Unemployed
            Teams.Add(new Team(1014));                                      //  33 Retired
            Teams.Add(new Team(1015));                                      //  34 Rookies
            Teams.Add(new Team(1010));                                      //  35 AFC
            Teams.Add(new Team(1011));                                      //  36 NFC
            

            foreach (Player player in this.Players)
            {
                if (player.TEAM_ID == 1009)
                    Teams[32].Players.Add(player);
                else if (player.TEAM_ID == 1014)
                    Teams[33].Players.Add(player);
                else if (player.TEAM_ID == 1015)
                    Teams[34].Players.Add(player);
                
                else if (player.TEAM_ID >= 0 && player.TEAM_ID <= 31)
                    Teams[player.TEAM_ID].Players.Add(player);
            }
            foreach (Coach coach in this.Coaches)
            {
                if (coach.TEAM_ID == 1009 || coach.TEAM_ID == 1023)
                    Teams[32].Coaches.Add(coach);
                else if (coach.TEAM_ID == 1014)
                    Teams[33].Coaches.Add(coach);
                else Teams[coach.TEAM_ID].Coaches.Add(coach);
            }

            for (int t = 0; t < 32; t++)
            {
                foreach (TeamRecord tr in model.TableModels[EditorModel.TEAM_TABLE].GetRecords())
                    if (tr.TeamId == t)
                        this.Teams[t].TeamAverages.Set_TeamRecord(tr);
                
            }

        }
        public List<int> GetTopPlayerSalaries(int position)
        {
            List<int> Top = new List<int>();
            for (int t = 0; t < 32; t++ )
            {
                foreach (Player player in this.Teams[t].Players)
                    if (player.POSITION_ID == position)
                        Top.Add(player.SALARY_CURRENT);
            }
            Top.Sort();

            return Top;
        }
                


        #region File IO
        public void Load()
        {
            BinaryReader binreader = new BinaryReader(File.Open(Manager_filename, FileMode.Open));

            #region Players
            int NumberOfPlayers = binreader.ReadInt32();
            this.Players = new List<Player>();
            for (int c = 0; c < NumberOfPlayers; c++)
            {
                Player newplayer = new Player();
                newplayer.Read(binreader);
                this.Players.Add(newplayer);
            }
            #endregion

            #region Coaches
            int NumberOfCoaches = binreader.ReadInt32();
            this.Coaches = new List<Coach>();
            for (int c = 0; c < NumberOfCoaches; c++)
            {
                Coach newcoach = new Coach();
                newcoach.Read(binreader);
                this.Coaches.Add(newcoach);
            }
            #endregion

            loaded = true;
            binreader.Close();     
        }
        public void Save()
        {
            BinaryWriter binwriter = new BinaryWriter(File.Create(Manager_filename));
            binwriter.Write(this.Players.Count);
            foreach (Player p in this.Players)
            {
                p.Write(binwriter);
            }
            binwriter.Write(this.Coaches.Count);
            foreach (Coach coach in this.Coaches)
            {
                coach.Write(binwriter);
            }

            binwriter.Close();
        }
        #endregion
        
        #region Background Thread Methods
        
        void ManagerThread_Load(object sender, DoWorkEventArgs e)
        {
           
        }
        void ManagerThread_Save(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_CreateDatabase(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_Init(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_UpdatePlayerList(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_UpdateCoachList(object sender, DoWorkEventArgs e)
        {
            
        }
        void ManagerThread_InitTeams(object sender, DoWorkEventArgs e)
        {
            
        }
        
        
        #endregion

          
        
        
        
                
        

        
        

    }
}
