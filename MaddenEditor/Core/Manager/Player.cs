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
using System.Linq;
using System.Text;
using System.IO;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.FranchiseState;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.Manager;


namespace MaddenEditor.Core.Manager
{
    #region Enums
    public enum Adjust
    {
        Team = 0,
        Coach = 1,
        Player = 2,
        Temp = 3,
        WeekType = 4,
        Time = 5,
        Stadium = 6,
        GrassType = 7,
        CONF = 8,
        DIV = 9,
        Stats = 10,
        Money = 11,
        Wins = 12,
        Streaky = 13,
        Work = 14,
        Char = 15,
    }
    public enum Position
    {   // 21-25, KICK RETURNER, PUNT RETURNER, KICKOFF SPECIALIST, LONG SNAPPER, THIRD DOWN BACK
        QB = 0,
        HB,
        FB,
        WR,
        TE,
        LT,
        LG,
        C,
        RG,
        RT,
        LE,
        RE,
        DT,
        LOLB,
        MLB,
        ROLB,
        CB,
        FS,
        SS,
        K,
        P,
        KR,     
        PR,
        KOS,
        LS,
        TDB,
    }
    public enum Trait
    {
        STR = 0,
        AGI = 1,
        SPD = 2,
        ACC = 3,
        AWR = 4,
        CTH = 5,
        CAR = 6,
        THP = 7,
        THA = 8,
        KPW = 9,
        KAC = 10,
        BTK = 11,
        TAK = 12,
        PBK = 13,
        RBK = 14,
        JMP = 15,
        KRT = 16,
        IMP = 17,
        INJ = 18,
        STA = 19,
        TGH = 20,
        EGO = 21,
        MOR = 22,        
        AGE = 23,
        HT = 24,
        WT = 25,
        POS = 26,
        TEN = 27,
        DOM = 28,
        STY = 29,
        COL = 30,
        OVR = 31,
        NONE = 32
    }
    public enum SkinColor
    {
        Light = 0,
        Medium = 1,
        Dark = 2
    }
    #endregion


    #region Sub Classes for Players

    public class PlayerInfo
    {        
        public string FIRST_NAME = "New";               //"PFNA";
        public string LAST_NAME = "Player";             //"PLNA";
        public int PLAYER_ID = 0;                       //"PGID";
        public int NFL_ID = 0;                          //"POID";
        public int AGE = 21;                            //"PAGE";
        public int HEIGHT = 0;                          //"PHGT";   Add 65 to this.Current_Ratings.for height in inches
        public int WEIGHT = 0;                          //"PWGT";   Add 160 to this.Current_Ratings.for actual weight in pounds
        public int POSITION_ID = 0;                     //"PPOS";
        public int TENDENCY = 0;                        //"PTEN";   0,1,    2 = balanced
        public bool DOMINANT_HAND = true;               //"PHAN";   0=left 1=right
        public bool THROWING_STYLE = false;             //"PSTY";   0=over 1=side
        public int COLLEGE_ID = 0;                      //"PCOL";
        public int EGO = 70;                            //"PEGO";   // 2007+
        public int MORALE = 50;                         //"PMOR";   // 2005+

        #region Custom Members
        public int Streak = -1;
        public int WorkEthic = -1;
        public int character = -1;
        public double self_overall = 70;
        #endregion

        public PlayerInfo()
        {
            PLAYER_ID = 0;
            NFL_ID = 0;                             //"POID";
            FIRST_NAME = "New";                     //"PFNA";
            LAST_NAME = "Player";                   //"PLNA";
            AGE = 21;                               //"PAGE";
            HEIGHT = 0;                             //"PHGT";   Add 65 to this.Current_Ratings.for height in inches
            WEIGHT = 0;                             //"PWGT";   Add 160 to this.Current_Ratings.for actual weight in pounds
            POSITION_ID = 0;                        //"PPOS";
            TENDENCY = 0;                           //"PTEN";   0,1,    2 = balanced
            DOMINANT_HAND = false;                  //"PHAN";   right = false, left = true 
            THROWING_STYLE = false;                 //"PSTY";   over = false, side = true
            COLLEGE_ID = 0;                         //"PCOL";

            EGO = 70;                               //"PEGO";   // 2007-2008
            MORALE = 50;                            //"PMOR";

            Streak = -1;                            // Custom
            WorkEthic = -1;                         // Custom
            character = -1;                         // Custom
            self_overall = 70;                      // Custom 
        }
    
        public PlayerInfo(PlayerRecord rec, EditorModel model)
        {
            PLAYER_ID = rec.PlayerId;
            NFL_ID = rec.NFLID;
            FIRST_NAME = rec.FirstName;
            LAST_NAME = rec.LastName;
            AGE = rec.Age;
            HEIGHT = rec.Height;
            WEIGHT = rec.Weight;
            POSITION_ID = rec.PositionId;
            TENDENCY = rec.Tendency;
            DOMINANT_HAND = rec.DominantHand;
            THROWING_STYLE = rec.ThrowStyle;            
            COLLEGE_ID = rec.CollegeId;

            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                EGO = rec.Pcel;
            else EGO = rec.Ego; 
            if (model.FileVersion >= MaddenFileVersion.Ver2005)
                MORALE = rec.Morale;
            else MORALE = 80;

            Streak = -1;                        // Custom
            WorkEthic = -1;                     // Custom
            character = -1;                     // Custom
            self_overall = 70;                  // Custom
        }

        #region IO
        public void Read(BinaryReader binreader)
        {
            #region Player Record Members
            NFL_ID = binreader.ReadUInt16();                        //"POID";
            PLAYER_ID = binreader.ReadUInt16();                     //"PGID"
            FIRST_NAME = binreader.ReadString();                   //"PFNA"
            LAST_NAME = binreader.ReadString();                    //"PLNA";
            AGE = binreader.ReadByte();                            //"PAGE";
            HEIGHT = binreader.ReadByte();                         //"PHGT";                       Add 65 to for height in inches            
            WEIGHT = binreader.ReadByte();                         //"PWGT";                       Add 160 to for actual weight in pounds
            POSITION_ID = binreader.ReadByte();                    //"PPOS";            
            TENDENCY = binreader.ReadByte();                       //"PTEN";
            DOMINANT_HAND = binreader.ReadBoolean();               //"PHAN";   0=left 1=right
            THROWING_STYLE = binreader.ReadBoolean();              //"PSTY";   0=over 1=side
            COLLEGE_ID = binreader.ReadUInt16();                   //"PCOL";
            EGO = binreader.ReadSByte();
            MORALE = binreader.ReadByte();
            #endregion
            #region Custom
            //  Custom fields
            Streak = binreader.ReadByte();
            WorkEthic = binreader.ReadByte();
            character = binreader.ReadByte();
            self_overall = binreader.ReadDouble();

            #endregion


        }
        
        public void Write(BinaryWriter binwriter)
        {
            #region Player Record Members
            binwriter.Write((UInt16)NFL_ID);
            binwriter.Write((UInt16)PLAYER_ID);
            binwriter.Write(FIRST_NAME);
            binwriter.Write(LAST_NAME);
            binwriter.Write((byte)AGE);
            binwriter.Write((byte)HEIGHT);
            binwriter.Write((byte)WEIGHT);
            binwriter.Write((byte)POSITION_ID);
            binwriter.Write((byte)TENDENCY);
            binwriter.Write((bool)DOMINANT_HAND);
            binwriter.Write(THROWING_STYLE);
            binwriter.Write((UInt16)COLLEGE_ID);
            binwriter.Write((sbyte)EGO);
            binwriter.Write((byte)MORALE);
            #endregion

            #region Custom Members
            binwriter.Write((byte)Streak);
            binwriter.Write((byte)WorkEthic);
            binwriter.Write((byte)character);
            binwriter.Write((double)self_overall);
            #endregion
        }
        #endregion
    }
        
    public class Player_Ratings
    {
        #region Members
        #region Player Record Members
        //  these 21 ratings are common to all versions 04-08
        public int ACCELERATION = 50;                   //"PACC";        
        public int AGILITY = 50;                        //"PAGI";
        public int AWARENESS = 50;                      //"PAWR";
        public int BREAK_TACKLE = 50;                   //"PBTK";
        public int CARRYING = 50;                       //"PCAR";
        public int CATCHING = 50;                       //"PCTH";
        public int IMPORTANCE = 50;                     //"PIMP";     
        public int INJURY = 50;                         //"PINJ";
        public int JUMPING = 50;                        //"PJMP";
        public int KICK_ACCURACY = 50;                  //"PKAC";
        public int KICK_POWER = 50;                     //"PKPR";
        public int KICK_RETURN = 50;                    //"PKRT";        
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
        
        #endregion

        #region Constructors

        public Player_Ratings()
        {
            ACCELERATION = 50;                      //"PACC";        
            AGILITY = 50;                           //"PAGI";
            AWARENESS = 50;                         //"PAWR";
            BREAK_TACKLE = 50;                      //"PBTK";
            CARRYING = 50;                          //"PCAR";
            CATCHING = 50;                          //"PCTH";
            IMPORTANCE = 50;                        //"PIMP;
            INJURY = 50;                            //"PINJ";
            JUMPING = 50;                           //"PJMP";
            KICK_ACCURACY = 50;                     //"PKAC";
            KICK_POWER = 50;                        //"PKPR";
            KICK_RETURN = 50;                       //"PKRT";            
            PASS_BLOCKING = 50;                     //"PPBK";
            RUN_BLOCKING = 50;                      //"PRBK";       
            SPEED = 50;                             //"PSPD";
            STAMINA = 50;                           //"PSTA";
            STRENGTH = 50;                          //"PSTR"; 
            TACKLE = 50;                            //"PTAK";
            TOUGHNESS = 50;                         //"PTGH";
            THROW_ACCURACY = 50;                    //"PTHA";
            THROW_POWER = 50;                       //"PTHP";
        }
        
        public Player_Ratings(PlayerRecord rec, EditorModel model)
        {            
            ACCELERATION = rec.Acceleration;
            AGILITY = rec.Agility;
            AWARENESS = rec.Awareness;
            BREAK_TACKLE = rec.BreakTackle;
            CARRYING = rec.Carrying;            
            CATCHING = rec.Catching;
            IMPORTANCE = rec.Importance;       
            INJURY = rec.Injury;
            JUMPING = rec.Jumping;
            KICK_ACCURACY = rec.KickAccuracy;
            KICK_POWER = rec.KickPower;
            KICK_RETURN = rec.KickReturn;            
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
        public int GetPlayerTrait(Trait trait)
        {
            if (trait == Trait.ACC)
                return this.ACCELERATION;            
            else if (trait == Trait.AGI)
                return this.AGILITY;
            else if (trait == Trait.AWR)
                return this.AWARENESS;
            else if (trait == Trait.BTK)
                return this.BREAK_TACKLE;
            else if (trait == Trait.CAR)
                return this.CARRYING;
            else if (trait == Trait.CTH)
                return this.CATCHING;
            else if (trait == Trait.INJ)
                return this.INJURY;
            else if (trait == Trait.JMP)
                return this.JUMPING;
            else if (trait == Trait.KAC)
                return this.KICK_ACCURACY;
            else if (trait == Trait.KPW)
                return this.KICK_POWER;
            else if (trait == Trait.KRT)
                return this.KICK_RETURN;
            else if (trait == Trait.PBK)
                return this.PASS_BLOCKING;
            else if (trait == Trait.RBK)
                return this.RUN_BLOCKING;
            else if (trait == Trait.SPD)
                return this.SPEED;
            else if (trait == Trait.STA)
                return this.STAMINA;
            else if (trait == Trait.TAK)
                return this.TACKLE;
            else if (trait == Trait.THA)
                return this.THROW_ACCURACY;
            else if (trait == Trait.THP)
                return this.THROW_POWER;
            else if (trait == Trait.TGH)
                return this.TOUGHNESS;
            else return 0;
        }
        public void SetPlayerTrait(Trait trait, int val)
        {
            if (trait == Trait.ACC)
                this.ACCELERATION = val;            
            else if (trait == Trait.AGI)
                this.AGILITY = val;
            else if (trait == Trait.AWR)
                this.AWARENESS = val;
            else if (trait == Trait.BTK)
                this.BREAK_TACKLE = val;
            else if (trait == Trait.CAR)
                this.CARRYING = val;
            else if (trait == Trait.CTH)
                this.CATCHING = val;            
            else if (trait == Trait.INJ)
                this.INJURY = val;
            else if (trait == Trait.JMP)
                this.JUMPING = val;
            else if (trait == Trait.KAC)
                this.KICK_ACCURACY = val;
            else if (trait == Trait.KPW)
                this.KICK_POWER = val;
            else if (trait == Trait.KRT)
                this.KICK_RETURN = val;
            else if (trait == Trait.PBK)
                this.PASS_BLOCKING = val;            
            else if (trait == Trait.RBK)
                this.RUN_BLOCKING = val;
            else if (trait == Trait.SPD)
                this.SPEED = val;
            else if (trait == Trait.STA)
                this.STAMINA = val;            
            else if (trait == Trait.TAK)
                this.TACKLE = val;            
            else if (trait == Trait.THA)
                this.THROW_ACCURACY = val;
            else if (trait == Trait.THP)
                this.THROW_POWER = val;            
            else if (trait == Trait.TGH)
                this.TOUGHNESS = val;            
            else return;
        }
        #endregion

        #region File IO

        public void Read(BinaryReader binreader)
        {
            #region Player Record Members
            ACCELERATION = binreader.ReadByte();
            AGILITY = binreader.ReadByte();
            AWARENESS = binreader.ReadByte();
            BREAK_TACKLE = binreader.ReadByte();
            CARRYING = binreader.ReadByte();
            CATCHING = binreader.ReadByte();
            IMPORTANCE = binreader.ReadByte();
            INJURY = binreader.ReadByte();
            JUMPING = binreader.ReadByte();
            KICK_ACCURACY = binreader.ReadByte();
            KICK_POWER = binreader.ReadByte();
            KICK_RETURN = binreader.ReadByte();            
            PASS_BLOCKING = binreader.ReadByte();
            RUN_BLOCKING = binreader.ReadByte();
            SPEED = binreader.ReadByte();
            STAMINA = binreader.ReadByte();
            STRENGTH = binreader.ReadByte();
            TACKLE = binreader.ReadByte();
            THROW_ACCURACY = binreader.ReadByte();
            THROW_POWER = binreader.ReadByte();
            TOUGHNESS = binreader.ReadByte();
            #endregion
        }
        
        public void Write(BinaryWriter binwriter)
        {
            #region Player Record Members
            binwriter.Write((byte)ACCELERATION);
            binwriter.Write((byte)AGILITY);
            binwriter.Write((byte)AWARENESS);
            binwriter.Write((byte)BREAK_TACKLE);
            binwriter.Write((byte)CARRYING);
            binwriter.Write((byte)CATCHING);            
            binwriter.Write((byte)INJURY);
            binwriter.Write((byte)IMPORTANCE);
            binwriter.Write((byte)JUMPING);
            binwriter.Write((byte)KICK_ACCURACY);
            binwriter.Write((byte)KICK_POWER);
            binwriter.Write((byte)KICK_RETURN);            
            binwriter.Write((byte)PASS_BLOCKING);
            binwriter.Write((byte)RUN_BLOCKING);
            binwriter.Write((byte)SPEED);
            binwriter.Write((byte)STAMINA);
            binwriter.Write((byte)STRENGTH);
            binwriter.Write((byte)TACKLE);
            binwriter.Write((byte)THROW_ACCURACY);
            binwriter.Write((byte)THROW_POWER);
            binwriter.Write((byte)TOUGHNESS);
            #endregion           
        }

        #endregion
    }
    
    public class Potential : Player_Ratings
    {

    }
        
    public class Current_Ratings
    {
        #region Members
        public double ACCELERATION = 50.0;                  //"PACC";        
        public double AGILITY = 50.0;                       //"PAGI";
        public double AWARENESS = 50.0;                     //"PAWR";
        public double BREAK_TACKLE = 50.0;                  //"PBTK";
        public double CARRYING = 50.0;                      //"PCAR";
        public double CATCHING = 50.0;                      //"PCTH";
        public double EGO = 70;                             //"PEGO";   // 2007-2008  "PCEL" 2004-2006
        public double INJURY = 50.0;                        //"PINJ";
        public double JUMPING = 50.0;                       //"PJMP";
        public double KICK_ACCURACY = 50.0;                 //"PKAC";
        public double KICK_POWER = 50.0;                    //"PKPR";
        public double KICK_RETURN = 50.0;                   //"PKRT";
        public double MORALE = 50.0;                        //"PMOR";
        public double PASS_BLOCKING = 50.0;                 //"PPBK";
        public double RUN_BLOCKING = 50.0;                  //"PRBK";       
        public double SPEED = 50.0;                         //"PSPD";
        public double STAMINA = 50.0;                       //"PSTA";
        public double STRENGTH = 50.0;                      //"PSTR"; 
        public double TACKLE = 50.0;                        //"PTAK";
        public double TOUGHNESS = 50.0;                     //"PTGH";
        public double THROW_ACCURACY = 50.0;                //"PTHA";
        public double THROW_POWER = 50.0;                   //"PTHP";

        #endregion

        public void UpdatePlayerRecord(PlayerRecord rec, EditorModel model)
        {
            rec.Acceleration = (int)ACCELERATION;
            rec.Agility = (int)AGILITY;
            rec.Awareness = (int)AWARENESS;
            rec.BreakTackle = (int)BREAK_TACKLE;
            rec.Carrying = (int)CARRYING;
            rec.Catching = (int)CATCHING;
            if (model.FileVersion >= MaddenFileVersion.Ver2007)
                rec.Ego = (int)EGO;
            rec.Injury = (int)INJURY;
            rec.Jumping = (int)JUMPING;
            rec.KickAccuracy = (int)KICK_ACCURACY;
            rec.KickPower = (int)KICK_POWER;
            rec.KickReturn = (int)KICK_RETURN;
            if (model.FileVersion >= MaddenFileVersion.Ver2005)
                rec.Morale = (int)MORALE;
            rec.PassBlocking = (int)PASS_BLOCKING;
            rec.RunBlocking = (int)RUN_BLOCKING;
            rec.Speed = (int)SPEED;
            rec.Stamina = (int)STAMINA;
            rec.Strength = (int)STRENGTH;
            rec.Tackle = (int)TACKLE;
            rec.ThrowAccuracy = (int)THROW_ACCURACY;
            rec.ThrowPower = (int)THROW_POWER;
            rec.Toughness = (int)TOUGHNESS;
        }
        
        #region File IO

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

        #endregion
    }
       
    public class Prog_Rate : Current_Ratings
    {
        public Prog_Rate()
        {
            ACCELERATION = .20;                  //"PACC";        
            AGILITY = .20;                       //"PAGI";
            AWARENESS = .20;                     //"PAWR";
            BREAK_TACKLE = .20;                  //"PBTK";
            CARRYING = .20;                      //"PCAR";
            CATCHING = .20;                      //"PCTH";
            EGO = .20;                           //"PEGO";   // 2007-2008
            INJURY = .20;                        //"PINJ";
            JUMPING = .20;                       //"PJMP";
            KICK_ACCURACY = .20;                 //"PKAC";
            KICK_POWER = .20;                    //"PKPR";
            KICK_RETURN = .20;                   //"PKRT";
            MORALE = .20;                        //"PMOR";
            PASS_BLOCKING = .20;                 //"PPBK";
            RUN_BLOCKING = .20;                  //"PRBK";       
            SPEED = .20;                         //"PSPD";
            STAMINA = .20;                       //"PSTA";
            STRENGTH = .20;                      //"PSTR"; 
            TACKLE = .20;                        //"PTAK";
            TOUGHNESS = .20;                     //"PTGH";
            THROW_ACCURACY = .20;                //"PTHA";
            THROW_POWER = .20;                   //"PTHP";
        }
        
    }
        
    //  fix stats for versions
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

        #region Methods

        public void SetCareerStats(CareerGamesPlayedRecord rec)
        {
            DOWNS_PLAYED = rec.DownsPlayed;        // "cgdp";
            GAMES_PLAYED = rec.GamesPlayed;        // "cgmp";
            GAMES_STARTED = rec.GamesStarted;      // "cgms";
        }

        public void SetCareerStats(CareerStatsOffenseRecord rec)
        {
            //QB Stats
            PASS_ATT = rec.Pass_att;                      //"caat";
            PASS_COMP = rec.Pass_comp;                    //"cacm";
            PASS_INT = rec.Pass_int;                      //"cain";
            PASS_LONG = rec.Pass_long;                    //"calN";
            PASS_SACKED = rec.Pass_sacked;                //"casa";
            PASS_YDS = rec.Pass_yds;                      //"caya";
            PASS_TDS = rec.Pass_tds;                      //"catd";

            // WR Stats
            RECEIVING_RECS = rec.Receiving_recs;          //"ccca";
            RECEIVING_DROPS = rec.Receiving_drops;        //"ccdr";
            RECEIVING_TDS = rec.Receiving_tds;            //"cctd";
            RECEIVING_YARDS = rec.Receiving_yards;        //"ccya";
            RECEIVING_YAC = rec.Receiving_yac;            //"ccyc";
            RECEIVING_LONG = rec.Receiving_long;          //"ccrL";

            //RB Stats
            RUSHING_TDS = rec.Rushing_tds;                //"cutd";
            RUSHING_LONG = rec.Rushing_long;              //"culN";
            FUMBLES = rec.Fumbles;                        //"cufu";
            RUSHING_ATTEMPTS = rec.RushingAttempts;       //"cuat";
            RUSHING_YARDS = rec.RushingYards;             //"cuya";
            RUSHING_YAC = rec.Rushing_yac;                //"cuyh";
            RUSHING_20 = rec.Rushing_20;                  //"cu2y";
            RUSHING_BT = rec.Rushing_bt;                  //"cubt";



        }

        public void SetCareerStats(CareerStatsDefenseRecord rec)
        {
            PASSES_DEFENDED = rec.PassesDefended;             //"cdpd";
            TACKLES = rec.Tackles;                                //"cdta";
            TACKLES_FOR_LOSS = rec.TacklesForLoss;                //"cdtl";
            BLOCKS = rec.Blocks;                                  //"clbl";
            FUMBLES_FORCED = rec.FumblesForced;                   //"clff";
            FUMBLES_RECOVERED = rec.FumblesRecovered;             //"clfr";
            FUMBLES_TD = rec.Fumbles_td;                          //"clft";
            FUMBLE_YARDS = rec.FumbleYards;                       //"clfy";
            SAFETIES = rec.Safeties;                              //"clsa";
            SACKS = rec.Sacks;                                    //"clsk";
            INTERCEPTIONS = rec.Def_int;                          //"csin";
            INTERCEPTION_YARDS = rec.Int_yards;                   //"csiy";
            INTERCEPTION_LONG = rec.Int_long;                     //"cslR";
            INTERCEPTION_TD = rec.Int_td;                         //"csit";
        }

        public void SetCareerStats(CareerStatsOffensiveLineRecord rec)
        {
            PANCAKES = rec.Pancakes;                       //"copa";
            SACKS_ALLOWED = rec.SacksAllowed;                  //"cosa";
        }

        public void SetCareerStats(CareerPKReturnRecord rec)
        {
            KICK_RETURN_ATT = rec.Kra;         //"crka";
            KICK_RETURN_LONG = rec.Krl;        //"crkL";
            KICK_RETURN_TD = rec.Krtd;         //"crkt";
            KICK_RETURN_YARDS = rec.Kryds;     //"crky";
            PUNT_RETURN_ATT = rec.Pra;         //"crpa";
            PUNT_RETURN_LONG = rec.Prl;        //"crpL";
            PUNT_RETURN_TD = rec.Prtd;         //"crpt";
            PUNT_RETURN_YARDS = rec.Pryds;     //"crpy";
        }

        public void SetCareerStats(CareerPuntKickRecord rec)
        {
            FGA = rec.Fga;           //"ckfa";
            FGM = rec.Fgm;             //"ckfm";
            FG_BLOCKED = rec.Fgbl;        //"ckfb";
            FGL = rec.Fgl;                //"ckfL";
            XPA = rec.Xpa;                //"ckea";
            XPM = rec.Xpm;                //"ckem";
            XP_BLOCKED = rec.Xpb;         //"ckeb";
            FGA_129 = rec.Fga_129;        //"ckaa";
            FGA_3039 = rec.Fga_3039;      //"ckac";
            FGA_4049 = rec.Fga_4049;      //"ckad";
            FGA_50 = rec.Fga_50;          //"ckae";
            FGM_129 = rec.Fgm_129;        //"ckma";
            FGM_3039 = rec.Fgm_3039;      //"ckmc";
            FGM_4049 = rec.Fgm_4049;      //"ckmd";
            FGM_50 = rec.Fgm_50;          //"ckme";
            KICK_OFFS = rec.Kickoffs;     //"cknk";
            TOUCHBACKS = rec.Touchbacks;  //"cktb";
            // Punter stats
            PUNT_ATT = rec.Puntatt;       //"cpat";
            PUNT_YDS = rec.Puntyds;       //"cpya";
            PUNT_BLOCKED = rec.Puntblk;  //"cpbl";
            PUNT_LONG = rec.Puntlong;     //"cpIN";
            PUNT_NY = rec.Puntny;         //"cpny";
            PUNT_IN20 = rec.Puntin20;     //"cppt";
            PUNT_TB = rec.Punttb;          //"cptb";

        }

        #endregion

        #region File IO

        public void Read(BinaryReader binreader)
        {
            DOWNS_PLAYED = binreader.ReadInt32();
            GAMES_PLAYED = binreader.ReadInt32();
            GAMES_STARTED = binreader.ReadInt32();

            //QB Stats
            PASS_ATT = binreader.ReadInt32();     //"caat";
            PASS_COMP = binreader.ReadInt32();     //"cacm";
            PASS_INT = binreader.ReadInt32();     //"cain";
            PASS_LONG = binreader.ReadInt32();     //"calN";
            PASS_SACKED = binreader.ReadInt32();     //"casa";
            PASS_YDS = binreader.ReadInt32();     //"caya";
            PASS_TDS = binreader.ReadInt32();     //"catd";

            // WR Stats
            RECEIVING_RECS = binreader.ReadInt32();     //"ccca";
            RECEIVING_DROPS = binreader.ReadInt32();     //"ccdr";
            RECEIVING_TDS = binreader.ReadInt32();     //"cctd";
            RECEIVING_YARDS = binreader.ReadInt32();     //"ccya";
            RECEIVING_YAC = binreader.ReadInt32();     //"ccyc";
            RECEIVING_LONG = binreader.ReadInt32();     //"ccrL";

            //RB Stats
            RUSHING_TDS = binreader.ReadInt32();     //"cutd";
            RUSHING_LONG = binreader.ReadInt32();     //"culN";
            FUMBLES = binreader.ReadInt32();     //"cufu";
            RUSHING_ATTEMPTS = binreader.ReadInt32();     //"cuat";
            RUSHING_YARDS = binreader.ReadInt32();     //"cuya";
            RUSHING_YAC = binreader.ReadInt32();     //"cuyh";
            RUSHING_20 = binreader.ReadInt32();     //"cu2y";
            RUSHING_BT = binreader.ReadInt32();     //"cubt";

            PASSES_DEFENDED = binreader.ReadInt32();     //"cdpd";
            TACKLES = binreader.ReadInt32();     //"cdta";
            TACKLES_FOR_LOSS = binreader.ReadInt32();     //"cdtl";
            BLOCKS = binreader.ReadInt32();     //"clbl";
            FUMBLES_FORCED = binreader.ReadInt32();     //"clff";
            FUMBLES_RECOVERED = binreader.ReadInt32();     //"clfr";
            FUMBLES_TD = binreader.ReadInt32();     //"clft";
            FUMBLE_YARDS = binreader.ReadInt32();     //"clfy";
            SAFETIES = binreader.ReadInt32();     //"clsa";
            SACKS = binreader.ReadInt32();     //"clsk";
            INTERCEPTIONS = binreader.ReadInt32();     //"csin";
            INTERCEPTION_YARDS = binreader.ReadInt32();     //"csiy";
            INTERCEPTION_LONG = binreader.ReadInt32();     //"cslR";
            INTERCEPTION_TD = binreader.ReadInt32();     //"csit";

            PANCAKES = binreader.ReadInt32();     //"copa";
            SACKS_ALLOWED = binreader.ReadInt32();     //"cosa"; 

            KICK_RETURN_ATT = binreader.ReadInt32();     //"crka";
            KICK_RETURN_LONG = binreader.ReadInt32();     //"crkL";
            KICK_RETURN_TD = binreader.ReadInt32();     //"crkt";
            KICK_RETURN_YARDS = binreader.ReadInt32();     //"crky";
            PUNT_RETURN_ATT = binreader.ReadInt32();     //"crpa";
            PUNT_RETURN_LONG = binreader.ReadInt32();     //"crpL";
            PUNT_RETURN_TD = binreader.ReadInt32();     //"crpt";
            PUNT_RETURN_YARDS = binreader.ReadInt32();     //"crpy";

            FGA = binreader.ReadInt32();     //"ckfa";
            FGM = binreader.ReadInt32();     //"ckfm";
            FG_BLOCKED = binreader.ReadInt32();     //"ckfb";
            FGL = binreader.ReadInt32();     //"ckfL";
            XPA = binreader.ReadInt32();     //"ckea";
            XPM = binreader.ReadInt32();     //"ckem";
            XP_BLOCKED = binreader.ReadInt32();     //"ckeb";
            FGA_129 = binreader.ReadInt32();     //"ckaa";
            FGA_3039 = binreader.ReadInt32();     //"ckac";
            FGA_4049 = binreader.ReadInt32();     //"ckad";
            FGA_50 = binreader.ReadInt32();     //"ckae";
            FGM_129 = binreader.ReadInt32();     //"ckma";
            FGM_3039 = binreader.ReadInt32();     //"ckmc";
            FGM_4049 = binreader.ReadInt32();     //"ckmd";
            FGM_50 = binreader.ReadInt32();     //"ckme";
            KICK_OFFS = binreader.ReadInt32();     //"cknk";
            TOUCHBACKS = binreader.ReadInt32();     //"cktb";
            // Punter stats
            PUNT_ATT = binreader.ReadInt32();     //"cpat";
            PUNT_YDS = binreader.ReadInt32();     //"cpya";
            PUNT_BLOCKED = binreader.ReadInt32();     //"cpbl";
            PUNT_LONG = binreader.ReadInt32();     //"cpIN";
            PUNT_NY = binreader.ReadInt32();     //"cpny";
            PUNT_IN20 = binreader.ReadInt32();     //"cppt";
            PUNT_TB = binreader.ReadInt32();     //"cptb";
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

        #endregion
    }

    public class SeasonStats
    {
        #region Members
        public int SEASON = 0;                                    //  "SEYR";

        public int GAMES_DOWNS_PLAYED = 0;                        // "sgdp";
        public int GAMES_PLAYED = 0;                              // "sgmp";
        public int GAMES_STARTED = 0;                             // "sgms";

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
            SEASON = rec.Season;
            GAMES_DOWNS_PLAYED = rec.DownsPlayed;
            GAMES_PLAYED = rec.GamesPlayed;
            GAMES_STARTED = rec.GamesStarted;
        }

        public void SetSeasonStats(SeasonStatsOffenseRecord rec)
        {
            SEA_PASS_ATT = rec.SeaPassAtt;
            SEA_PASS_COMEBACKS = rec.SeaComebacks;
            SEA_COMP = rec.SeaComp;
            SEA_PASS_FIRST_DOWNS = rec.SeaFirstDowns;
            SEA_PASS_INT = rec.SeaPassInt;
            SEA_PASS_LONG = rec.SeaPassLong;
            SEA_SACKED = rec.SeaSacked;
            SEA_PASS_TD = rec.SeaPassTd;
            SEA_PASS_YDS = rec.SeaPassYds;
            SEA_REC = rec.SeaRec;
            SEA_DROPS = rec.SeaDrops;
            SEA_REC_LONG = rec.SeaRecLong;
            SEA_REC_TD = rec.SeaRecTd;
            SEA_REC_YDS = rec.SeaRecYds;
            SEA_REC_YAC = rec.SeaRecYac;
            SEA_RUSH_20 = rec.SeaRush20;
            SEA_RUSH_ATT = rec.SeaRushAtt;
            SEA_RUSH_BTK = rec.SeaRushBtk;
            SEA_FUMBLES = rec.SeaFumbles;
            SEA_RUSH_LONG = rec.SeaRushLong;
            SEA_RUSH_TD = rec.SeaRushTd;
            SEA_RUSH_YDS = rec.SeaRushYds;
            SEA_RUSH_YAC = rec.SeaRushYac;
        }

        public void SetSeasonStats(SeasonStatsDefenseRecord rec)
        {
            PASSES_DEFENDED = rec.PassesDefended;
            TACKLES = rec.Tackles;
            TACKLES_FOR_LOSS = rec.TacklesForLoss;
            BLOCKS = rec.Blocks;
            FUMBLES_FORCED = rec.FumblesForced;
            FUMBLES_RECOVERED = rec.FumblesRecovered;
            FUMBLES_TD = rec.FumbleTDS;
            FUMBLE_YARDS = rec.FumbleYards;
            SAFETIES = rec.Safeties;
            SACKS = rec.Sacks;
            INTERCEPTIONS = rec.Interceptions;
            INTERCEPTION_TD = rec.InterceptionTDS;
            INTERCEPTION_YARDS = rec.InterceptionYards;
            INTERCEPTION_LONG = rec.InterceptionLong;
        }

        public void SetSeasonStats(SeasonStatsOffensiveLineRecord rec)
        {
            PANCAKES = rec.Pancakes;
            SACKS_ALLOWED = rec.SacksAllowed;
        }

        public void SetSeasonStats(SeasonPKReturnRecord rec)
        {
            KICK_RETURN_ATT = rec.Kra;
            KICK_RETURN_LONG = rec.Krl;
            KICK_RETURN_TD = rec.Krtd;
            KICK_RETURN_YARDS = rec.Kryds;
            PUNT_RETURN_ATT = rec.Pra;
            PUNT_RETURN_LONG = rec.Prl;
            PUNT_RETURN_TD = rec.Prtd;
            PUNT_RETURN_YARDS = rec.Pryds;
        }

        public void SetSeasonStats(SeasonPuntKickRecord rec)
        {
            FGA = rec.Fga;
            FGM = rec.Fgm;
            FG_BLOCKED = rec.Fgbl;
            FGL = rec.Fgl;
            XPA = rec.Xpa;
            XPM = rec.Xpm;
            XP_BLOCKED = rec.Xpb;
            FGA_129 = rec.Fga_129;
            FGA_3039 = rec.Fga_3039;
            FGA_4049 = rec.Fga_4049;
            FGA_50 = rec.Fga_50;
            FGM_129 = rec.Fgm_129;
            FGM_3039 = rec.Fgm_3039;
            FGM_4049 = rec.Fgm_4049;
            FGM_50 = rec.Fgm_50;
            KICK_OFFS = rec.Kickoffs;
            TOUCHBACKS = rec.Touchbacks;
            PUNT_ATT = rec.Puntatt;
            PUNT_YDS = rec.Puntyds;
            PUNT_BLOCKED = rec.Puntblk;
            PUNT_LONG = rec.Puntlong;
            PUNT_NY = rec.Puntny;
            PUNT_IN20 = rec.Puntin20;
            PUNT_TB = rec.Punttb;
        }

        #endregion

        #region File IO

        public void Read(BinaryReader binreader, int count)
        {
            SEASON = binreader.ReadInt32();                                      //  "SEYR";

            GAMES_DOWNS_PLAYED = binreader.ReadInt32();                          // "sgdp";
            GAMES_PLAYED = binreader.ReadInt32();                                // "sgmp";
            GAMES_STARTED = binreader.ReadInt32();                               // "sgms";

            SEA_PASS_ATT = binreader.ReadInt32();                              //  "saat";
            SEA_PASS_COMEBACKS = binreader.ReadInt32();                        //  "sacb";         //  2008
            SEA_COMP = binreader.ReadInt32();                                  //  "sacm";
            SEA_PASS_FIRST_DOWNS = binreader.ReadInt32();                      //  "safd";         //  2008
            SEA_PASS_INT = binreader.ReadInt32();                              //  "sain";
            SEA_PASS_LONG = binreader.ReadInt32();                             //  "saln";
            SEA_SACKED = binreader.ReadInt32();                                //  "sasa";
            SEA_PASS_TD = binreader.ReadInt32();                               //  "satd";
            SEA_PASS_YDS = binreader.ReadInt32();                              //  "saya";
            SEA_REC = binreader.ReadInt32();                                   //  "scca";
            SEA_DROPS = binreader.ReadInt32();                                 //  "scdr";
            SEA_REC_LONG = binreader.ReadInt32();                              //  "scrL";
            SEA_REC_TD = binreader.ReadInt32();                                //  "sctd";
            SEA_REC_YDS = binreader.ReadInt32();                               //  "scya";
            SEA_REC_YAC = binreader.ReadInt32();                               //  "scyc";        
            SEA_RUSH_20 = binreader.ReadInt32();                               //  "su2y";
            SEA_RUSH_ATT = binreader.ReadInt32();                              //  "suat";
            SEA_RUSH_BTK = binreader.ReadInt32();                              //  "subt";
            SEA_FUMBLES = binreader.ReadInt32();                               //  "sufu";
            SEA_RUSH_LONG = binreader.ReadInt32();                             //  "suln";
            SEA_RUSH_TD = binreader.ReadInt32();                               //  "sutd";
            SEA_RUSH_YDS = binreader.ReadInt32();                              //  "suya";
            SEA_RUSH_YAC = binreader.ReadInt32();                              //  "suyh";

            PASSES_DEFENDED = binreader.ReadInt32();                             // "sdpd";
            TACKLES = binreader.ReadInt32();                                     // "sdta";
            TACKLES_FOR_LOSS = binreader.ReadInt32();                            // "sdtl";        
            BLOCKS = binreader.ReadInt32();                                      // "slbl";
            FUMBLES_FORCED = binreader.ReadInt32();                              // "slff";
            FUMBLES_RECOVERED = binreader.ReadInt32();                           // "slfr";
            FUMBLES_TD = binreader.ReadInt32();                                  // "slft";
            FUMBLE_YARDS = binreader.ReadInt32();                                // "slfy";
            SAFETIES = binreader.ReadInt32();                                    // "slsa";
            SACKS = binreader.ReadInt32();                                       // "slsk";
            INTERCEPTIONS = binreader.ReadInt32();                               // "ssin";
            INTERCEPTION_TD = binreader.ReadInt32();                             // "ssit";
            INTERCEPTION_YARDS = binreader.ReadInt32();                          // "ssiy";
            INTERCEPTION_LONG = binreader.ReadInt32();                           // "sslR";

            PANCAKES = binreader.ReadInt32();                                    // "sopa";
            SACKS_ALLOWED = binreader.ReadInt32();                               // "sosa";

            KICK_RETURN_ATT = binreader.ReadInt32();                             // "srka";
            KICK_RETURN_LONG = binreader.ReadInt32();                            // "srkL";
            KICK_RETURN_TD = binreader.ReadInt32();                              // "srkt";
            KICK_RETURN_YARDS = binreader.ReadInt32();                           // "srky";
            PUNT_RETURN_ATT = binreader.ReadInt32();                             // "srpa";
            PUNT_RETURN_LONG = binreader.ReadInt32();                            // "srpL";
            PUNT_RETURN_TD = binreader.ReadInt32();                              // "srpt";
            PUNT_RETURN_YARDS = binreader.ReadInt32();                           // "srpy";

            FGA = binreader.ReadInt32();                                         // "skfa";
            FGM = binreader.ReadInt32();                                         // "skfm";
            FG_BLOCKED = binreader.ReadInt32();                                  // "skfb";
            FGL = binreader.ReadInt32();                                         // "skfL";
            XPA = binreader.ReadInt32();                                         // "skea";
            XPM = binreader.ReadInt32();                                         // "skem";
            XP_BLOCKED = binreader.ReadInt32();                                  // "skeb";
            FGA_129 = binreader.ReadInt32();                                     // "skaa";
            FGA_3039 = binreader.ReadInt32();                                    // "skac";
            FGA_4049 = binreader.ReadInt32();                                    // "skad";
            FGA_50 = binreader.ReadInt32();                                      // "skae";
            FGM_129 = binreader.ReadInt32();                                     // "skma";
            FGM_3039 = binreader.ReadInt32();                                    // "skmc";
            FGM_4049 = binreader.ReadInt32();                                    // "skmd";
            FGM_50 = binreader.ReadInt32();                                      // "skme";
            KICK_OFFS = binreader.ReadInt32();                                   // "sknk";
            TOUCHBACKS = binreader.ReadInt32();                                  // "sktb";

            PUNT_ATT = binreader.ReadInt32();                                     // "spat";
            PUNT_YDS = binreader.ReadInt32();                                     // "spya";
            PUNT_BLOCKED = binreader.ReadInt32();                                 // "spbl";
            PUNT_LONG = binreader.ReadInt32();                                    // "spIN";
            PUNT_NY = binreader.ReadInt32();                                      // "spny";
            PUNT_IN20 = binreader.ReadInt32();                                    // "sppt";
            PUNT_TB = binreader.ReadInt32();                                      // "sptb";
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

    public class PlayerAppearance
    {
        #region Non-Used Player Record Members

        public int NASAL_STRIP = 0;                     //"PBRE";        
        public int EQP_PAD_SHELF = 9;                   //"PCHS";
        public int PLAYER_COMMENTARY = 999;             //"PCMT";   // 999 or 1023 for rookie
        
        public int PCTS = 0;                            //"PCTS";   // ?        
        public int EYE_PAINT = 0;                       //"PEYE";
        public int ARMS_FAT = 0;                        //"PFAS";
        public int LEGS_CALF_FAT = 0;                   //"PFCS";
        public int FACE_ID = 0;                         //"PFEx";
        public int FACE_SHAPE = 0;                      //"PFGE";   // 2004-2005
        public int PFGS = 0;                            //"PFGS";          
        public int LEGS_THIGH_FAT = 0;                  //"PFHS";
        public int FACE_MASK = 0;                       //"PFMK";
        public int BODY_FAT = 0;                        //"PFTS";        
        public int SLEEVES_A = 0;                       //"PGSL";
        public int HAIR_COLOR = 0;                      //"PHCL";
        public int HAIR_STYLE = 0;                      //"PHED";        
        public int HELMET_STYLE = 0;                    //"PHLM";           
        public int JERSEY_NUMBER = 50;                  //"PJEN";
        public int JERSEY = 1;                          //"PJER";       
        public int JERSEY_INITIALS = 0;                 //"PJTY";        
        public int LEFT_ELBOW_A = 0;                    //"PLEL";
        public bool PLFH = false;                       //"PLFH";                       // ?
        public int LEFT_HAND_A = 0;                     //"PLHA";
        public int PLPL = 0;                            //"PLPL";                       // ?
        public int LEFT_ANKLE = 0;                      //"PLSH";
        public int EQP_SHOES = 0;                       //"PLSS";
        public bool LEFT_KNEE = false;                  //"PLTH";
        public int LEFT_WRIST_A = 0;                    //"PLWR";
        public int ARMS_MUSCLE = 0;                     //"PMAS";
        public int LEGS_CALF_MUSCLE = 0;                //"PMCS";
        public int REAR_FAT = 0;                        //"PMGS";
        public int LEGS_THIGH_MUSCLE = 0;               //"PMHS";        
        public int MOUTHPIECE = 0;                      //"PMPC";
        public int BODY_WEIGHT = 0;                     //"PMTS";                       
        public int MUSCLE = 0;                          //"PMUS";   //0-2   2008 not used = 0
        public int NECK_ROLL = 0;                       //"PNEK";
        public int PPGA = 0;                            //"PPGA";   // ?        
        public int PPSP = 0;                            //"PPSP";        
        public int REAR_SHAPE = 0;                      //"PQGS";
        public int EQP_FLAK_JACKET = 0;                 //"PQTS";
        public int RIGHT_ELBOW_A = 0;                   //"PREL";
        public int RIGHT_HAND_A = 0;                    //"PRHA";        
        public int RIGHT_ANKLE = 0;                     //"PRSH";
        public bool RIGHT_KNEE = false;                 //"PRTH";
        public int RIGHT_WRIST_A = 0;                   //"PRWR";        
        public int BODY_OVERALL = 0;                    //"PSBS";
        public int PSKI = 0;                            //"PSKI";                               
        public int PSTM = 0;                            //"PSTM";       
        public int LEFT_TATTOO = 0;                     //"PTAL";
        public int RIGHT_TATTOO = 0;                    //"PTAR";        
        public int LEGS_THIGH_PADS = 0;                 //"PTPS";   // 2004-2005        
        public int SLEEVES_B = 0;                       //"PTSL";
        public int EQP_PAD_HEIGHT = 0;                  //"PTSS";
        public int PUCL = 0;                            //"PUCL";                       // ?
        public int BODY_MUSCLE = 0;                     //"PUTS";       
        
        public int VISOR = 0;                           //"PVIS";
        
        public int PWIN = 0;                            // PWIN
        public int EQP_PAD_WIDTH = 0;                   //"PWSS";        
        public int LEFT_ELBOW_B = 0;                    //"TLEL";
        public int LEFT_HAND_B = 0;                     //"TLHA";
        public int LEFT_WRIST_B = 0;                    //"TLWR";
        public int RIGHT_ELBOW_B = 0;                   //"TREL";
        public int RIGHT_HAND_B = 0;                    //"TRHA";
        public int RIGHT_WRIST_B = 0;                   //"TRWR";        

        #endregion

        public PlayerAppearance()
        {

        }
        public PlayerAppearance(PlayerRecord rec, EditorModel model)
        {
            #region Non-Used Player Record Members

            NASAL_STRIP = rec.NasalStrip;               //"PBRE";        
            EQP_PAD_SHELF = rec.EquipmentPadShelf;      //"PCHS";
            PLAYER_COMMENTARY = rec.PlayerComment;      //"PCMT";            
            PCTS = rec.Pcts;                            //"PCTS";   // ?        
            EYE_PAINT = rec.EyePaint;                   //"PEYE";
            ARMS_FAT = rec.ArmsFat;                     //"PFAS";
            LEGS_CALF_FAT = rec.LegsCalfFat;                        //"PFCS";
            FACE_ID = rec.FaceId;                                   //"PFEx";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                FACE_SHAPE = rec.FaceShape;                         //"PFGE";   // 2004-2005
            PFGS = rec.Pfgs;                            //"PFGS";          
            LEGS_THIGH_FAT = rec.LegsThighFat;          //"PFHS";
            FACE_MASK = rec.FaceMask;                   //"PFMK";
            BODY_FAT = rec.BodyFat;                     //"PFTS";
            SLEEVES_A = rec.SleevesLeft;                    //"PGSL";
            HAIR_COLOR = rec.HairColor;                 //"PHCL";
            HAIR_STYLE = rec.HairStyle;                 //"PHED";        
            HELMET_STYLE = rec.Helmet;             //"PHLM";           
            JERSEY_NUMBER = rec.JerseyNumber;           //"PJEN";
            JERSEY = rec.JerseySleeve;                        //"PJER";       
            JERSEY_INITIALS = rec.JerseyInitials;       //"PJTY";        
            LEFT_ELBOW_A = rec.LeftElbow;               //"PLEL";
            PLFH = rec.Plfh;                            //"PLFH";   // ?
            LEFT_HAND_A = rec.LeftHand;                 //"PLHA";
            PLPL = rec.Plpl;                            //"PLPL";   // ?
            LEFT_ANKLE = rec.LeftShoe;                 //"PLSH";
            EQP_SHOES = rec.EquipmentShoes;             //"PLSS";
            //LEFT_KNEE = rec.KneeLeft;                   //"PLTH";
            LEFT_WRIST_A = rec.LeftWrist;               //"PLWR";
            ARMS_MUSCLE = rec.ArmsMuscle;               //"PMAS";
            LEGS_CALF_MUSCLE = rec.LegsCalfMuscle;      //"PMCS";
            REAR_FAT = rec.RearRearFat;                 //"PMGS";
            LEGS_THIGH_MUSCLE = rec.LegsThighMuscle;    //"PMHS";        
            MOUTHPIECE = rec.MouthPiece;                //"PMPC";
            BODY_WEIGHT = rec.BodyWeight;               //"PMTS";                       
            MUSCLE = rec.BodyMuscle;                    //"PMUS";
            NECK_ROLL = rec.NeckRoll;                               //"PNEK";
            PPGA = rec.PlayedGames;                                        //"PPGA";   // ?        
            PPSP = rec.Ppsp;                                        //"PPSP";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
            {
                REAR_SHAPE = rec.RearShape;                         //"PQGS";   //2004-2005
                EQP_FLAK_JACKET = rec.EquipmentFlakJacket;          //"PQTS";   //2004-2005
            }
            RIGHT_ELBOW_A = rec.RightElbow;                         //"PREL";
            RIGHT_HAND_A = rec.RightHand;                           //"PRHA";        
            RIGHT_ANKLE = rec.RightShoe;               //"PRSH";
            //RIGHT_KNEE = rec.ThighRight;                 //"PRTH";
            RIGHT_WRIST_A = rec.RightWrist;             //"PRWR";        
            BODY_OVERALL = rec.BodyOverall;             //"PSBS";
            PSKI = rec.Pski;                            //"PSKI";                               
            PSTM = rec.Pstm;                            //"PSTM";       
            LEFT_TATTOO = rec.LeftTattoo;                           //"PTAL";
            RIGHT_TATTOO = rec.RightTattoo;                         //"PTAR";
            if (model.FileVersion >= MaddenFileVersion.Ver2005)
                LEGS_THIGH_PADS = rec.LegsThighPads;                //"PTPS";   // 2004-2005        
            SLEEVES_B = rec.SleevesLeft;                                //"PTSL";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                EQP_PAD_HEIGHT = rec.EquipmentPadHeight;            //"PTSS";
            PUCL = rec.Pucl;                                        //"PUCL";   // ?
            BODY_MUSCLE = rec.BodyMuscle;                           //"PUTS";                   
            VISOR = rec.Visor;                                      //"PVIS";                
            PWIN = rec.Pwin;                                        // PWIN
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                EQP_PAD_WIDTH = rec.EquipmentPadWidth;              //"PWSS";        
            LEFT_ELBOW_B = rec.LeftElbow;                           //"TLEL";
            LEFT_HAND_B = rec.LeftHand;                             //"TLHA";
            LEFT_WRIST_B = rec.LeftWrist;                           //"TLWR";
            RIGHT_ELBOW_B = rec.RightElbow;                         //"TREL";
            RIGHT_HAND_B = rec.RightHand;                           //"TRHA";
            RIGHT_WRIST_B = rec.RightWrist;                         //"TRWR";        

            #endregion
        }

        // to do : fix
        public void UpdatePlayerRecord(PlayerRecord rec, EditorModel model)
        {
            #region Non-Used Player Record Members

            rec.NasalStrip = NASAL_STRIP;                               //"PBRE";        
            rec.EquipmentPadShelf = EQP_PAD_SHELF;                      //"PCHS";
            rec.PlayerComment = PLAYER_COMMENTARY;                      //"PCMT";  
            rec.Pcts = PCTS;                                            //"PCTS";   // ?        
            rec.EyePaint = EYE_PAINT;                                   //"PEYE";
            rec.ArmsFat = ARMS_FAT;                                     //"PFAS";
            rec.LegsCalfFat = LEGS_CALF_FAT;                            //"PFCS";
            rec.FaceId = FACE_ID;                                       //"PFEx";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                rec.FaceShape = FACE_SHAPE;                             //"PFGE";   // 2004-2005
            rec.Pfgs = PFGS;                                            //"PFGS";          
            rec.LegsThighFat = LEGS_THIGH_FAT;                          //"PFHS";
            rec.FaceMask = FACE_MASK;                                   //"PFMK";
            rec.BodyFat = BODY_FAT;                                     //"PFTS";
            rec.SleevesLeft = SLEEVES_A;                                    //"PGSL";
            rec.HairColor = HAIR_COLOR;                                 //"PHCL";
            rec.HairStyle = HAIR_STYLE;                                 //"PHED";        
            rec.Helmet = HELMET_STYLE;                             //"PHLM";           
            rec.JerseyNumber = JERSEY_NUMBER;                           //"PJEN";
            rec.JerseySleeve = JERSEY;                                        //"PJER";       
            rec.JerseyInitials = JERSEY_INITIALS;                       //"PJTY";        
            rec.LeftElbow = LEFT_ELBOW_A;                               //"PLEL";
            rec.Plfh = PLFH;                                            //"PLFH";   // ?
            rec.LeftHand = LEFT_HAND_A;                                 //"PLHA";
            rec.Plpl = PLPL;                                            //"PLPL";   // ?
            rec.LeftShoe = LEFT_ANKLE;                                 //"PLSH";
            rec.EquipmentShoes = EQP_SHOES;                             //"PLSS";
            //rec.KneeLeft = LEFT_KNEE;                                   //"PLTH";
            rec.LeftWrist = LEFT_WRIST_A;                               //"PLWR";
            rec.ArmsMuscle = ARMS_MUSCLE;                               //"PMAS";
            rec.LegsCalfMuscle = LEGS_CALF_MUSCLE;                      //"PMCS";
            rec.RearRearFat = REAR_FAT;                                 //"PMGS";
            rec.LegsThighMuscle = LEGS_THIGH_MUSCLE;                    //"PMHS";        
            rec.MouthPiece = MOUTHPIECE;                                //"PMPC";
            rec.BodyWeight = BODY_WEIGHT;                               //"PMTS";                       
            rec.BodyMuscle = MUSCLE;                                    //"PMUS";
            rec.NeckRoll = NECK_ROLL;                                   //"PNEK";
            rec.PlayedGames = PPGA;                                            //"PPGA";   // ?        
            rec.Ppsp = PPSP;                                            //"PPSP";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
            {
                rec.RearShape = REAR_SHAPE;                             //"PQGS";   //2004-2005
                rec.EquipmentFlakJacket = EQP_FLAK_JACKET;              //"PQTS";   //2004-2005
            }
            rec.RightElbow = RIGHT_ELBOW_A;                             //"PREL";
            rec.RightHand = RIGHT_HAND_A;                               //"PRHA";        
            rec.RightShoe = RIGHT_ANKLE;                               //"PRSH";
            //rec.ThighRight = RIGHT_KNEE;                                 //"PRTH";
            rec.RightWrist = RIGHT_WRIST_A;                             //"PRWR";        
            rec.BodyOverall = BODY_OVERALL;                             //"PSBS";
            rec.Pski = PSKI;                                            //"PSKI";                               
            rec.Pstm = PSTM;                                            //"PSTM";       
            rec.LeftTattoo = LEFT_TATTOO;                               //"PTAL";
            rec.RightTattoo = RIGHT_TATTOO;                             //"PTAR";
            if (model.FileVersion >= MaddenFileVersion.Ver2005)
                rec.LegsThighPads = LEGS_THIGH_PADS;                    //"PTPS";   // 2004-2005        
            rec.SleevesLeft = SLEEVES_B;                                    //"PTSL";
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                rec.EquipmentPadHeight = EQP_PAD_HEIGHT;                //"PTSS";   // 2004-2005
            rec.Pucl = PUCL;                                            //"PUCL";   // ?
            rec.BodyMuscle = BODY_MUSCLE;                               //"PUTS";                   
            rec.Visor = VISOR;                                          //"PVIS";                
            rec.Pwin = PWIN;                                            // PWIN
            if (model.FileVersion <= MaddenFileVersion.Ver2005)
                rec.EquipmentPadWidth = EQP_PAD_WIDTH;                  //"PWSS"; 
            rec.LeftElbow = LEFT_ELBOW_B;                               //"TLEL";
            rec.LeftHand = LEFT_HAND_B;                                 //"TLHA";
            rec.LeftWrist = LEFT_WRIST_B;                               //"TLWR";
            rec.RightElbow = RIGHT_ELBOW_B;                             //"TREL";
            rec.RightHand = RIGHT_HAND_B;                               //"TRHA";
            rec.RightWrist = RIGHT_WRIST_B;                             //"TRWR";        

            #endregion

        }

        #region File IO

        public void Read(BinaryReader binreader)
        {
            NASAL_STRIP = binreader.ReadByte();
            EQP_PAD_SHELF = binreader.ReadByte();
            PLAYER_COMMENTARY = binreader.ReadUInt16();
            PCTS = binreader.ReadByte();
            EYE_PAINT = binreader.ReadByte();
            ARMS_FAT = binreader.ReadByte();
            LEGS_CALF_FAT = binreader.ReadByte();
            FACE_ID = binreader.ReadByte();
            FACE_SHAPE = binreader.ReadUInt16();
            PFGS = binreader.ReadByte();
            LEGS_THIGH_FAT = binreader.ReadByte();
            FACE_MASK = binreader.ReadByte();
            BODY_FAT = binreader.ReadByte();
            SLEEVES_A = binreader.ReadByte();
            HAIR_COLOR = binreader.ReadByte();
            HAIR_STYLE = binreader.ReadByte();
            HELMET_STYLE = binreader.ReadByte();
            JERSEY_NUMBER = binreader.ReadByte();
            JERSEY = binreader.ReadByte();
            JERSEY_INITIALS = binreader.ReadByte();
            LEFT_ELBOW_A = binreader.ReadByte();
            PLFH = binreader.ReadBoolean();
            LEFT_HAND_A = binreader.ReadByte();
            PLPL = binreader.ReadByte();
            LEFT_ANKLE = binreader.ReadByte();
            EQP_SHOES = binreader.ReadByte();
            LEFT_KNEE = binreader.ReadBoolean();
            LEFT_WRIST_A = binreader.ReadByte();
            ARMS_MUSCLE = binreader.ReadByte();
            LEGS_CALF_MUSCLE = binreader.ReadByte();
            REAR_FAT = binreader.ReadByte();
            LEGS_THIGH_MUSCLE = binreader.ReadByte();
            MOUTHPIECE = binreader.ReadByte();
            BODY_WEIGHT = binreader.ReadByte();
            MUSCLE = binreader.ReadByte();
            NECK_ROLL = binreader.ReadByte();
            PPGA = binreader.ReadByte();
            PPSP = binreader.ReadUInt16();
            REAR_SHAPE = binreader.ReadByte();
            EQP_FLAK_JACKET = binreader.ReadByte();
            RIGHT_ELBOW_A = binreader.ReadByte();
            RIGHT_HAND_A = binreader.ReadByte();
            RIGHT_ANKLE = binreader.ReadByte();
            RIGHT_KNEE = binreader.ReadBoolean();
            RIGHT_WRIST_A = binreader.ReadByte();
            BODY_OVERALL = binreader.ReadByte();
            PSKI = binreader.ReadByte();
            PSTM = binreader.ReadByte();
            LEFT_TATTOO = binreader.ReadByte();
            RIGHT_TATTOO = binreader.ReadByte();
            LEGS_THIGH_PADS = binreader.ReadByte();
            SLEEVES_B = binreader.ReadByte();
            EQP_PAD_HEIGHT = binreader.ReadByte();
            PUCL = binreader.ReadByte();
            BODY_MUSCLE = binreader.ReadByte();
            VISOR = binreader.ReadByte();
            PWIN = binreader.ReadByte();
            EQP_PAD_WIDTH = binreader.ReadByte();
            LEFT_ELBOW_B = binreader.ReadByte();
            LEFT_HAND_B = binreader.ReadByte();
            LEFT_WRIST_B = binreader.ReadByte();
            RIGHT_ELBOW_B = binreader.ReadByte();
            RIGHT_HAND_B = binreader.ReadByte();
            RIGHT_WRIST_B = binreader.ReadByte();
        }
                
        public void Write(BinaryWriter binwriter)
        {
            binwriter.Write((byte)NASAL_STRIP);         // PBRE
            binwriter.Write((UInt16)EQP_PAD_SHELF);     // PCHS
            binwriter.Write((UInt16)PLAYER_COMMENTARY); // PCMT
            binwriter.Write((byte)PCTS);                // PCTS
            binwriter.Write(EYE_PAINT);                 // PEYE
            binwriter.Write((byte)ARMS_FAT);            // PFAS
            binwriter.Write((byte)LEGS_CALF_FAT);       // PFCS
            binwriter.Write((UInt16)FACE_ID);           // PFEx
            binwriter.Write((byte)FACE_SHAPE);          // PFGE
            binwriter.Write((byte)PFGS);                // PFGS
            binwriter.Write((byte)LEGS_THIGH_FAT);
            binwriter.Write((byte)FACE_MASK);
            binwriter.Write((byte)BODY_FAT);
            binwriter.Write((byte)SLEEVES_A);
            binwriter.Write((byte)HAIR_COLOR);
            binwriter.Write((byte)HAIR_STYLE);
            binwriter.Write((byte)HELMET_STYLE);
            binwriter.Write((byte)JERSEY_NUMBER);
            binwriter.Write(JERSEY);
            binwriter.Write((byte)JERSEY_INITIALS);
            binwriter.Write((byte)LEFT_ELBOW_A);
            binwriter.Write(PLFH);
            binwriter.Write((byte)LEFT_HAND_A);
            binwriter.Write((byte)PLPL);
            binwriter.Write((byte)LEFT_ANKLE);
            binwriter.Write((byte)EQP_SHOES);
            binwriter.Write(LEFT_KNEE);
            binwriter.Write((byte)LEFT_WRIST_A);
            binwriter.Write((byte)ARMS_MUSCLE);
            binwriter.Write((byte)LEGS_CALF_MUSCLE);
            binwriter.Write((byte)REAR_FAT);
            binwriter.Write((byte)LEGS_THIGH_MUSCLE);
            binwriter.Write((byte)MOUTHPIECE);
            binwriter.Write((byte)BODY_WEIGHT);
            binwriter.Write((byte)MUSCLE);
            binwriter.Write((byte)NECK_ROLL);
            binwriter.Write((byte)PPGA);
            binwriter.Write((UInt16)PPSP);
            binwriter.Write((byte)REAR_SHAPE);
            binwriter.Write((byte)EQP_FLAK_JACKET);
            binwriter.Write((byte)RIGHT_ELBOW_A);
            binwriter.Write((byte)RIGHT_HAND_A);
            binwriter.Write((byte)RIGHT_ANKLE);
            binwriter.Write(RIGHT_KNEE);
            binwriter.Write((byte)RIGHT_WRIST_A);
            binwriter.Write((byte)BODY_OVERALL);
            binwriter.Write((byte)PSKI);
            binwriter.Write((byte)PSTM);
            binwriter.Write((byte)LEFT_TATTOO);
            binwriter.Write((byte)RIGHT_TATTOO);
            binwriter.Write((byte)LEGS_THIGH_PADS);
            binwriter.Write((byte)SLEEVES_B);
            binwriter.Write((byte)EQP_PAD_HEIGHT);
            binwriter.Write((byte)PUCL);
            binwriter.Write((byte)BODY_MUSCLE);            
            binwriter.Write((byte)VISOR);
            binwriter.Write((byte)PWIN);
            binwriter.Write((byte)EQP_PAD_WIDTH);
            binwriter.Write((byte)LEFT_ELBOW_B);
            binwriter.Write((byte)LEFT_HAND_B);
            binwriter.Write((byte)LEFT_WRIST_B);
            binwriter.Write((byte)RIGHT_ELBOW_B);
            binwriter.Write((byte)RIGHT_HAND_B);
            binwriter.Write((byte)RIGHT_WRIST_B);
        }
        
        #endregion
    }
        
    
        
    
    
    
    public class Player
    {
        #region Members

        #region Ratings and Progression Rate

        public PlayerInfo Info;
        public Player_Ratings Original_Ratings;
        public Player_Ratings Current_Ratings;
        public Potential PlayerPotential;
        public Prog_Rate PlayerProgRate;

        #endregion

        #region Player record members
        
        public int CONTRACT_LENGTH = 0;                 //"PCON";
        public int PHASE = 0;                           //"PCPH";
        public int SALARY_CURRENT = 0;                  //"PCSA";
        public int CONTRACT_YRS_LEFT = 0;               //"PCYL";
        public int DRAFT_ROUND_INDEX = 0;               //"PDPI";
        public int DRAFT_ROUND = 0;                     //"PDRO";        
        public bool PRO_BOWL = false;                   //"PFPB";        
        public bool HOLDOUT = false;                    //"PFHO";   // ?
        public bool NFL_ICON = false;                   //"PICN";        
        public int LAST_HEALTHY_YEAR = -31;             //"PLHY";   // not sure about this
        public int PREVIOUS_POSITION_ID = 0;            //"POPS";
        public int OVERALL = 0;                         //"POVR";        
        public int PREVIOUS_TEAM_ID = 0;                //"PPTI";
        public int PLAYER_ROLE = 0;                     //"PROL";   // 2007
        public int PLAYER_WEAPON = 0;                   //"PRL2";   // 2008
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
        public int TOTAL_SALARY = 0;                    //"PTSA";
        public int PVAL = 50;                           //"PVAL";   // 2007-2008
        public int PREVIOUS_CONTRACT = 0;               //"PVCO";
        public int PREVIOUS_SIGNING_BONUS_TOTAL = 0;    //"PVSB";
        public int PREVIOUS_SALARY = 0;                 // PVTS
        public int YRS_PRO = 0;                         //"PYRP";
        public int YEARS_WITH_TEAM = 0;                 //"PYWT";        
        public int TEAM_ID = 0;                         //"TGID";

        #endregion

        #region Stats

        public CareerStats PlayerCareerStats;
        public List<SeasonStats> PlayerSeasonStats;
        #endregion
        #endregion

        #region Constructors

        public Player()
        {
            Info = new PlayerInfo();
            Original_Ratings = new Player_Ratings();
            Current_Ratings = new Player_Ratings();
            PlayerPotential = new Potential();
            PlayerProgRate = new Prog_Rate();
            PlayerCareerStats = new CareerStats();
            PlayerSeasonStats = new List<SeasonStats>();
        }

        public Player(PlayerRecord rec, EditorModel model)
        {
            Info = new PlayerInfo(rec, model);
            Original_Ratings = new Player_Ratings(rec,model);
            Current_Ratings = new Player_Ratings(rec,model);
            PlayerPotential = new Potential();
            PlayerProgRate = new Prog_Rate();
            
            CONTRACT_LENGTH = rec.ContractLength;
            PHASE = rec.CareerPhase;
            SALARY_CURRENT = rec.CurrentSalary;
            CONTRACT_YRS_LEFT = rec.ContractYearsLeft;
            DRAFT_ROUND_INDEX = rec.DraftRoundIndex;
            DRAFT_ROUND = rec.DraftRound;
            PRO_BOWL = rec.ProBowl;       
            HOLDOUT = rec.Holdout;
            NFL_ICON = rec.NFLIcon;
            LAST_HEALTHY_YEAR = rec.LastHealthy;
            PREVIOUS_POSITION_ID = rec.OriginalPositionId;
            OVERALL = rec.Overall;
            PREVIOUS_TEAM_ID = rec.PreviousTeamId;
            PLAYER_ROLE = rec.PlayerRole;
            PLAYER_WEAPON = rec.PlayerWeapon;
            SALARY_YEAR_0 = rec.Salary0;
            SALARY_YEAR_1 = rec.Salary1;
            SALARY_YEAR_2 = rec.Salary2;
            SALARY_YEAR_3 = rec.Salary3;
            SALARY_YEAR_4 = rec.Salary4;
            SALARY_YEAR_5 = rec.Salary5;
            SALARY_YEAR_6 = rec.Salary6;            
            SIGNING_BONUS_YEAR_0 = rec.Bonus0;
            SIGNING_BONUS_YEAR_1 = rec.Bonus1;
            SIGNING_BONUS_YEAR_2 = rec.Bonus2;
            SIGNING_BONUS_YEAR_3 = rec.Bonus3;
            SIGNING_BONUS_YEAR_4 = rec.Bonus4;
            SIGNING_BONUS_YEAR_5 = rec.Bonus5;
            SIGNING_BONUS_YEAR_6 = rec.Bonus6;
            SIGNING_BONUS = rec.SigningBonus;
            PORTRAIT_ID = rec.PortraitId;
            TOTAL_SALARY = rec.TotalSalary;
            PVAL = rec.PlayerValue;
            //PREVIOUS_CONTRACT = rec.Pvco;
            PREVIOUS_SIGNING_BONUS_TOTAL = rec.PreviousSigningBonus;
            PREVIOUS_SALARY = rec.PreviousTotalSalary;
            YRS_PRO = rec.YearsPro;
            YEARS_WITH_TEAM = rec.YearsWithTeam;            
            TEAM_ID = rec.TeamId;

            PlayerCareerStats = new CareerStats();
            PlayerSeasonStats = new List<SeasonStats>();

            int count = YRS_PRO;
            if (YRS_PRO == 0)
                count = 1;
            for (int c = 0; c < count; c++)
                PlayerSeasonStats.Add(new SeasonStats());
        }

        #endregion

        #region Methods

        public void UpdateSelfValue()
        {   //  The player's ego determines how good he thinks he is.
            double perc = 1.0 + (this.Info.EGO - 75) / 450;
            if (perc < .95)
                perc = .95;
            this.Info.self_overall = CalculateOverallRating(this.Info.POSITION_ID, true, false) * perc;
        }
       
        public int GetPlayerTrait(Trait trait)
        {
            if (trait == Trait.ACC)
                return this.Current_Ratings.ACCELERATION;
            else if (trait == Trait.AGE)
                return this.Info.AGE;
            else if (trait == Trait.AGI)
                return this.Current_Ratings.AGILITY;
            else if (trait == Trait.AWR)
                return this.Current_Ratings.AWARENESS;
            else if (trait == Trait.BTK)
                return this.Current_Ratings.BREAK_TACKLE;
            else if (trait == Trait.CAR)
                return this.Current_Ratings.CARRYING;
            else if (trait == Trait.CTH)
                return this.Current_Ratings.CATCHING;
            else if (trait == Trait.EGO)
                return this.Info.EGO;
            else if (trait == Trait.COL)
                return this.Info.COLLEGE_ID;
            else if (trait == Trait.DOM)
                return Convert.ToInt32(this.Info.DOMINANT_HAND);
            else if (trait == Trait.HT)
                return this.Info.HEIGHT;
            else if (trait == Trait.INJ)
                return this.Current_Ratings.INJURY;
            else if (trait == Trait.JMP)
                return this.Current_Ratings.JUMPING;
            else if (trait == Trait.KAC)
                return this.Current_Ratings.KICK_ACCURACY;
            else if (trait == Trait.KPW)
                return this.Current_Ratings.KICK_POWER;
            else if (trait == Trait.KRT)
                return this.Current_Ratings.KICK_RETURN;
            else if (trait == Trait.MOR)
                return this.Info.MORALE;
            else if (trait == Trait.OVR)
                return (int)CalculateOverallRating(this.Info.PLAYER_ID, false, false);
            else if (trait == Trait.PBK)
                return this.Current_Ratings.PASS_BLOCKING;
            else if (trait == Trait.POS)
                return this.Info.POSITION_ID;
            else if (trait == Trait.RBK)
                return this.Current_Ratings.RUN_BLOCKING;
            else if (trait == Trait.SPD)
                return this.Current_Ratings.SPEED;
            else if (trait == Trait.STA)
                return this.Current_Ratings.STAMINA;
            else if (trait == Trait.STR)
                return this.Info.Streak;
            else if (trait == Trait.TAK)
                return this.Current_Ratings.TACKLE;
            else if (trait == Trait.TEN)
                return this.Info.TENDENCY;
            else if (trait == Trait.THA)
                return this.Current_Ratings.THROW_ACCURACY;
            else if (trait == Trait.THP)
                return this.Current_Ratings.THROW_POWER;
            else if (trait == Trait.STY)
                return Convert.ToInt32(this.Info.THROWING_STYLE);
            else if (trait == Trait.TGH)
                return this.Current_Ratings.TOUGHNESS;
            else if (trait == Trait.WT)
                return this.Info.WEIGHT;
            else return 0;
        }

        public void SetPlayerTrait(Trait trait, int val)
        {
            if (trait == Trait.ACC)
                this.Current_Ratings.ACCELERATION = val;
            else if (trait == Trait.AGE)
                this.Info.AGE = val;
            else if (trait == Trait.AGI)
                this.Current_Ratings.AGILITY = val;
            else if (trait == Trait.AWR)
                this.Current_Ratings.AWARENESS = val;
            else if (trait == Trait.BTK)
                this.Current_Ratings.BREAK_TACKLE = val;
            else if (trait == Trait.CAR)
                this.Current_Ratings.CARRYING = val;
            else if (trait == Trait.CTH)
                this.Current_Ratings.CATCHING = val;
            else if (trait == Trait.EGO)
                this.Info.EGO = val;
            else if (trait == Trait.COL)
                this.Info.COLLEGE_ID = val;
            else if (trait == Trait.DOM)
                this.Info.DOMINANT_HAND = (val == 0);
            else if (trait == Trait.HT)
                this.Info.HEIGHT = val;
            else if (trait == Trait.INJ)
                this.Current_Ratings.INJURY = val;
            else if (trait == Trait.JMP)
                this.Current_Ratings.JUMPING = val;
            else if (trait == Trait.KAC)
                this.Current_Ratings.KICK_ACCURACY = val;
            else if (trait == Trait.KPW)
                this.Current_Ratings.KICK_POWER = val;
            else if (trait == Trait.KRT)
                this.Current_Ratings.KICK_RETURN = val;
            else if (trait == Trait.MOR)
                this.Info.MORALE = val;
            else if (trait == Trait.OVR)
                this.Info.self_overall = val;
            else if (trait == Trait.PBK)
                this.Current_Ratings.PASS_BLOCKING = val;
            else if (trait == Trait.POS)
                this.Info.POSITION_ID = val;
            else if (trait == Trait.RBK)
                this.Current_Ratings.RUN_BLOCKING = val;
            else if (trait == Trait.SPD)
                this.Current_Ratings.SPEED = val;
            else if (trait == Trait.STA)
                this.Current_Ratings.STAMINA = val;
            else if (trait == Trait.STR)
                this.Info.Streak = val;
            else if (trait == Trait.TAK)
                this.Current_Ratings.TACKLE = val;
            else if (trait == Trait.TEN)
                this.Info.TENDENCY = val;
            else if (trait == Trait.THA)
                this.Current_Ratings.THROW_ACCURACY = val;
            else if (trait == Trait.THP)
                this.Current_Ratings.THROW_POWER = val;
            else if (trait == Trait.STY)
                this.Info.THROWING_STYLE = (val == 0);
            else if (trait == Trait.TGH)
                this.Current_Ratings.TOUGHNESS = val;
            else if (trait == Trait.WT)
                this.Info.WEIGHT = val;
            else return;
        }              
       
        public void UpdatePlayerRecord(PlayerRecord rec, EditorModel model)
        {
            rec.NFLID = this.Info.NFL_ID;
            rec.FirstName = this.Info.FIRST_NAME;
            rec.LastName = this.Info.LAST_NAME;
            rec.Age = this.Info.AGE;
            rec.Height = this.Info.HEIGHT;
            rec.Weight = this.Info.WEIGHT;
            rec.PositionId = this.Info.POSITION_ID;
            rec.Tendency = this.Info.TENDENCY;
            rec.DominantHand = this.Info.DOMINANT_HAND;
            rec.ThrowStyle = this.Info.THROWING_STYLE;
            rec.CollegeId = this.Info.COLLEGE_ID;

            rec.Acceleration = this.Current_Ratings.ACCELERATION;
            rec.Agility = this.Current_Ratings.AGILITY;
            rec.Awareness = this.Current_Ratings.AWARENESS;
            rec.BreakTackle = this.Current_Ratings.BREAK_TACKLE;
            rec.Carrying = this.Current_Ratings.CARRYING;
            if (model.FileVersion <= MaddenFileVersion.Ver2006)
                rec.Pcel = this.Info.EGO;
            rec.Catching = this.Current_Ratings.CATCHING;
            if (model.FileVersion >= MaddenFileVersion.Ver2007)
                rec.Ego = this.Info.EGO;
            rec.Injury = this.Current_Ratings.INJURY;
            rec.Jumping = this.Current_Ratings.JUMPING;
            rec.KickAccuracy = this.Current_Ratings.KICK_ACCURACY;
            rec.KickPower = this.Current_Ratings.KICK_POWER;
            rec.KickReturn = this.Current_Ratings.KICK_RETURN;
            if (model.FileVersion >= MaddenFileVersion.Ver2005)
                rec.Morale = this.Info.MORALE;
            rec.PassBlocking = this.Current_Ratings.PASS_BLOCKING;
            rec.RunBlocking = this.Current_Ratings.RUN_BLOCKING;
            rec.Speed = this.Current_Ratings.SPEED;
            rec.Stamina = this.Current_Ratings.STAMINA;
            rec.Strength = this.Current_Ratings.STRENGTH;
            rec.Tackle = this.Current_Ratings.TACKLE;
            rec.ThrowAccuracy = this.Current_Ratings.THROW_ACCURACY;
            rec.ThrowPower = this.Current_Ratings.THROW_POWER;
            rec.Toughness = this.Current_Ratings.TOUGHNESS;
        }
        
        public double CalculateOverallRating(int positionId, bool round, bool update)
        {
            double tempOverall = 0;

            switch (positionId)
            {
                case (int)MaddenPositions.QB:
                    tempOverall += (((double)this.Current_Ratings.THROW_ACCURACY - 50) / 10) * 5.8;
                    tempOverall += (((double)this.Current_Ratings.THROW_POWER - 50) / 10) * 4.9;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 4.0;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 2.0;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 0.8;                    
                    if (round)
                        tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall), 1);
                    tempOverall += 28;
                    break;

                case (int)MaddenPositions.HB:
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 3.3;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 2.8;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.0;
                    tempOverall += (((double)this.Current_Ratings.CARRYING - 50) / 10) * 2.0;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 1.8;
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 1.4;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.6;
                    tempOverall += (((double)this.Current_Ratings.PASS_BLOCKING - 50) / 10) * 0.33;
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
                    tempOverall += (((double)this.Current_Ratings.CATCHING - 50) / 10) * 4.75;
                    tempOverall += (((double)this.Current_Ratings.ACCELERATION - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.AGILITY - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.AWARENESS - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.SPEED - 50) / 10) * 2.3;
                    tempOverall += (((double)this.Current_Ratings.JUMPING - 50) / 10) * 1.4;
                    tempOverall += (((double)this.Current_Ratings.BREAK_TACKLE - 50) / 10) * 0.8;
                    tempOverall += (((double)this.Current_Ratings.STRENGTH - 50) / 10) * 0.8;
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

            return tempOverall;
        }

        public void UpdateCurrentRatings(PlayerRecord rec)
        {
            //this.Current_Ratings.Current_Ratings = new Player_Ratings(rec);
            int update = (int)CalculateOverallRating(this.Info.POSITION_ID, true, true);
            UpdateSelfValue();
        }

        public void UpdateCareerStats(EditorModel model)
        {
            foreach (CareerGamesPlayedRecord rec in model.TableModels[EditorModel.CAREER_GAMES_PLAYED_TABLE].GetRecords())
                if (this.Info.PLAYER_ID == rec.PlayerId)
                {
                    this.PlayerCareerStats.SetCareerStats(rec);
                    break;
                }

            if (this.PlayerCareerStats.GAMES_PLAYED > 0)
            {
                foreach (CareerStatsDefenseRecord rec in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
                    if (this.Info.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerPKReturnRecord rec in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                    if (this.Info.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerPuntKickRecord rec in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_TABLE].GetRecords())
                    if (this.Info.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerStatsOffenseRecord rec in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
                    if (this.Info.PLAYER_ID == rec.PlayerId)
                    {
                        this.PlayerCareerStats.SetCareerStats(rec);
                        break;
                    }
                foreach (CareerStatsOffensiveLineRecord rec in model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                    if (this.Info.PLAYER_ID == rec.PlayerId)
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
                    if (this.Info.PLAYER_ID == rec.PlayerId)
                    {
                        sea.SetSeasonStats(rec);
                        break;
                    }
                }

                if (sea.GAMES_PLAYED > 0)
                {
                    foreach (SeasonPKReturnRecord rec in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                    {
                        if (this.Info.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonPuntKickRecord rec in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_TABLE].GetRecords())
                    {
                        if (this.Info.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsDefenseRecord rec in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
                    {
                        if (this.Info.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsOffenseRecord rec in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
                    {
                        if (this.Info.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
                        {
                            sea.SetSeasonStats(rec);
                            break;
                        }
                    }
                    foreach (SeasonStatsOffensiveLineRecord rec in model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                    {
                        if (this.Info.PLAYER_ID == rec.PlayerId && sea.SEASON == rec.Season)
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
            //  this.needs to be adjusted for what progression points we want, right now its taking the difference between the last 2 stats
            int t = this.PlayerSeasonStats.Count - 1;
            double Participation = (double)(this.PlayerSeasonStats[t].GAMES_DOWNS_PLAYED - 96) / 415;
           
            #region Bonus

             
            #region Coach Bonus

            //  Setup Coaches modifier for position, motivation and knowledge
            double coach_pos_bonus = team.Coaches[this.TEAM_ID].GetPositionProgRating(this.Info.POSITION_ID);
            double coach_mot_bonus = team.Coaches[this.TEAM_ID].GetMotivationProgRating(this.Info.POSITION_ID);
            double coach_awr_bonus = team.Coaches[this.TEAM_ID].GetKnowledgeProgRating(this.Info.POSITION_ID);

            #endregion

            #region Work Bonus

            double Work_Bonus = (double)(this.Info.WorkEthic - 50) / 196;
            if (Work_Bonus > .250)
                Work_Bonus = .250;

            #endregion

            #region Morale Bonus

            double Morale_Bonus = (this.Info.MORALE - 70) / 196;
            if (Morale_Bonus > .250)
                Morale_Bonus = .250;

            #endregion

            #region Years Pro Bonus

            double YearsPro_Bonus = 0;
            if (this.YRS_PRO <= 2)                 
                YearsPro_Bonus = Math.Pow((3 - this.YRS_PRO), 2) / 36;

            #endregion

            #region Peer Bonus

            double Peer_Bonus = 0;
            foreach (Player p in team.Players)
            {
                if (p.Info.PLAYER_ID != this.Info.PLAYER_ID && p.Info.POSITION_ID == this.Info.POSITION_ID && p.YRS_PRO > 3)
                {
                    double ment_awr = ((double)(p.Current_Ratings.AWARENESS - 84) / 120);
                    if (ment_awr < -.125)
                        ment_awr = -.125;
                    if (ment_awr > .125)
                        ment_awr = .125;

                    double ment_mor = (double)(75 - p.Info.EGO + p.Info.MORALE - 80) / 240;
                    if (ment_mor < -.125)
                        ment_mor = -.125;
                    if (ment_mor > .125)
                        ment_mor = .125;

                    Peer_Bonus = ment_awr + ment_mor;
                }
            }

            #endregion

            #region Starter Bonus

            double starter = (double)(this.PlayerSeasonStats[t].GAMES_STARTED / 80);
            if (this.YRS_PRO == 0)
                starter = (double)(this.PlayerSeasonStats[t].GAMES_STARTED / 48);

            #endregion

            #endregion

            double bonus = 0.00;
                


            //  Setup Player's base progression for each ability... in sections
            #region Awareness
            bonus = Participation + coach_awr_bonus + YearsPro_Bonus + Peer_Bonus + starter;
            double AWR = (double)(this.PlayerPotential.AWARENESS - this.Current_Ratings.AWARENESS) * (this.PlayerProgRate.AWARENESS * bonus);

            #endregion

            
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
            double EGO = (double)(this.Info.EGO) * this.PlayerProgRate.EGO;
            double MOR = (double)(this.Info.MORALE) * this.PlayerProgRate.MORALE;

          

 

        }

        public double GetAgeEffect(int min1, int max1, int thresh, double val)
        {
            Random ran = new Random();
            if (ran.Next(0, 100) < thresh)
                return (ran.Next(min1, max1) / 100);
            else return val;
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

            if (this.Info.AGE <= 24)
                effect = GetAgeEffect(0, 100, 90, 0);
            else if (this.Info.AGE >= 25 && this.Info.AGE <= 28)
                effect = GetAgeEffect(0, 25, 50, 0);
            else if (this.Info.AGE >= 29 && this.Info.AGE <= 30)
                effect = GetAgeEffect(0, -25, 50, 0);
            else if (this.Info.AGE >= 31 && this.Info.AGE <= 34)
                effect = GetAgeEffect(-75, 0, 70, 0);
            else effect = GetAgeEffect(-100, -25, 90, -.25);

            if (this.Info.POSITION_ID == (int)MaddenPositions.QB)
            {
            }
            else if (this.Info.POSITION_ID == (int)MaddenPositions.HB)
            {
            }
            else if (this.Info.POSITION_ID == (int)MaddenPositions.WR)
            {
            }
            //  O-Line
            else if (this.Info.POSITION_ID == (int)MaddenPositions.C || this.Info.POSITION_ID == (int)MaddenPositions.LT || this.Info.POSITION_ID == (int)MaddenPositions.LG
                || this.Info.POSITION_ID == (int)MaddenPositions.RG || this.Info.POSITION_ID == (int)MaddenPositions.RT)
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

        #endregion 

        public int GetRandomInt(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);            
        }
        

        #region File IO

        public void Read(BinaryReader binreader)
        {
            //  Ratings and Progression
            Info.Read(binreader);
            Original_Ratings.Read(binreader);
            Current_Ratings.Read(binreader);
            PlayerPotential.Read(binreader);
            PlayerProgRate.Read(binreader);

            this.CONTRACT_LENGTH = binreader.ReadByte();                //"PCON";
            this.PHASE = binreader.ReadByte();                     //"PCPH";
            this.SALARY_CURRENT = binreader.ReadInt16();                //"PCSA";
            this.CONTRACT_YRS_LEFT = binreader.ReadByte();              //"PCYL";
            this.DRAFT_ROUND_INDEX = binreader.ReadByte();              //"PDPI";
            this.DRAFT_ROUND = binreader.ReadByte();                    //"PDRO";            
            this.PRO_BOWL = binreader.ReadBoolean();                    //"PFPB";                       
            this.HOLDOUT = binreader.ReadBoolean();                     //"PFHO";                       
            this.NFL_ICON = binreader.ReadBoolean();                    //"PICN";            
            this.LAST_HEALTHY_YEAR = binreader.ReadSByte();             //"PLHY";                       // not sure about this.one 
            this.PREVIOUS_POSITION_ID = binreader.ReadByte();           //"POPS";
            this.OVERALL = binreader.ReadByte();                        //"POVR";            
            this.PREVIOUS_TEAM_ID = binreader.ReadUInt16();             //"PPTI";            
            this.PLAYER_ROLE = binreader.ReadByte();                    //"PROL";                       // 2007
            this.PLAYER_WEAPON = binreader.ReadByte();                  //"PRL2";                       // 2008
            this.SALARY_YEAR_0 = binreader.ReadUInt16();                //"PSA0";
            this.SALARY_YEAR_1 = binreader.ReadUInt16();                //"PSA1";
            this.SALARY_YEAR_2 = binreader.ReadUInt16();                //"PSA2";
            this.SALARY_YEAR_3 = binreader.ReadUInt16();                //"PSA3";
            this.SALARY_YEAR_4 = binreader.ReadUInt16();                //"PSA4";
            this.SALARY_YEAR_5 = binreader.ReadUInt16();                //"PSA5";
            this.SALARY_YEAR_6 = binreader.ReadUInt16();                //"PSA6";
            this.SIGNING_BONUS_YEAR_0 = binreader.ReadUInt16();         //"PSB0";
            this.SIGNING_BONUS_YEAR_1 = binreader.ReadUInt16();         //"PSB1";
            this.SIGNING_BONUS_YEAR_2 = binreader.ReadUInt16();         //"PSB2";
            this.SIGNING_BONUS_YEAR_3 = binreader.ReadUInt16();         //"PSB3";
            this.SIGNING_BONUS_YEAR_4 = binreader.ReadUInt16();         //"PSB4";
            this.SIGNING_BONUS_YEAR_5 = binreader.ReadUInt16();         //"PSB5";
            this.SIGNING_BONUS_YEAR_6 = binreader.ReadUInt16();         //"PSB6";
            this.SIGNING_BONUS = binreader.ReadUInt16();                //"PSBO";
            this.PORTRAIT_ID = binreader.ReadUInt16();                  //"PSXP";
            this.TOTAL_SALARY = binreader.ReadUInt16();                 //"PTSA";                       // 2008
            this.PVAL = binreader.ReadByte();                           //"PVAL";                       // 2008
            this.PREVIOUS_CONTRACT = binreader.ReadByte();              //"PVCO";
            this.PREVIOUS_SIGNING_BONUS_TOTAL = binreader.ReadUInt16(); //"PVSB";
            this.PREVIOUS_SALARY = binreader.ReadUInt16();              //"PVTS";
            this.YRS_PRO = binreader.ReadByte();                        //"PYRP";
            this.YEARS_WITH_TEAM = binreader.ReadByte();                //"PYWT";            
            this.TEAM_ID = binreader.ReadUInt16();                      //"TGID";

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
            // Ratings and Progression
            Info.Write(binwriter);
            Original_Ratings.Write(binwriter);
            Current_Ratings.Write(binwriter);
            PlayerPotential.Write(binwriter);
            PlayerProgRate.Write(binwriter);

            binwriter.Write((byte)CONTRACT_LENGTH);                 //"PCON";
            binwriter.Write((byte)PHASE);                      //"PCPH";
            binwriter.Write((UInt16)SALARY_CURRENT);                //"PCSA";
            binwriter.Write((byte)CONTRACT_YRS_LEFT);               //"PCYL";
            binwriter.Write((byte)DRAFT_ROUND_INDEX);               //"PDPI";
            binwriter.Write((byte)DRAFT_ROUND);                     //"PDRO";            
            binwriter.Write(PRO_BOWL);                              //"PFPB";            
            binwriter.Write(HOLDOUT);                               //"PFHO";                       // ?
            binwriter.Write(NFL_ICON);                              //"PICN";
            binwriter.Write((sbyte)LAST_HEALTHY_YEAR);              //"PLHY";   // not sure about this.Current_Ratings.one 
            binwriter.Write((byte)PREVIOUS_POSITION_ID);            //"POPS";
            binwriter.Write((byte)OVERALL);                         //"POVR";            
            binwriter.Write((UInt16)PREVIOUS_TEAM_ID);              //"PPTI";
            binwriter.Write((byte)PLAYER_ROLE);                     //"PROL";                       // 2007
            binwriter.Write((byte)PLAYER_WEAPON);                   //"PRL2";                       // 2008
            binwriter.Write((UInt16)SALARY_YEAR_0);                 //"PSA0";
            binwriter.Write((UInt16)SALARY_YEAR_1);                 //"PSA1";
            binwriter.Write((UInt16)SALARY_YEAR_2);                 //"PSA2";
            binwriter.Write((UInt16)SALARY_YEAR_3);                 //"PSA3";
            binwriter.Write((UInt16)SALARY_YEAR_4);                 //"PSA4";
            binwriter.Write((UInt16)SALARY_YEAR_5);                 //"PSA5";
            binwriter.Write((UInt16)SALARY_YEAR_6);                 //"PSA6";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_0);          //"PSB0";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_1);          //"PSB1";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_2);          //"PSB2";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_3);          //"PSB3";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_4);          //"PSB4";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_5);          //"PSB5";
            binwriter.Write((UInt16)SIGNING_BONUS_YEAR_6);          //"PSB6";
            binwriter.Write((UInt16)SIGNING_BONUS);                 //"PSBO";
            binwriter.Write((UInt16)PORTRAIT_ID);                   //"PSXP";
            binwriter.Write((UInt16)TOTAL_SALARY);                  //"PTSA";                       // 2008
            binwriter.Write((byte)PVAL);                            //"PVAL";                       // 2008
            binwriter.Write((UInt16)PREVIOUS_SIGNING_BONUS_TOTAL);  //"PVSB";
            binwriter.Write((UInt16)PREVIOUS_SALARY);               //"PVTS";
            binwriter.Write((byte)YRS_PRO);                         //"PYRP";
            binwriter.Write((byte)YEARS_WITH_TEAM);                 //"PYWT";
            binwriter.Write((UInt16)TEAM_ID);                       //"TGID";

            //  Career Stats
            this.PlayerCareerStats.Write(binwriter);

            //  Season Stats
            binwriter.Write(this.PlayerSeasonStats.Count);
            foreach (SeasonStats sea in this.PlayerSeasonStats)
                sea.Write(binwriter);
        }

        #endregion
    
    }
}
            



            
        



    

