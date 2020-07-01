/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 StingRay68
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
 * 
 *****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;

using System.Linq;
using System.Text;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Db;


namespace MaddenEditor.Core
{
    public class DraftPlayer
    {
        // 2019 - 328 bytes  2020 - 336 bytes
        // 2020 Adds HairStyle, MorphHead ?

        #region Members
        public string NameFirst = "";
        public string NameLast = "";
        public int HomeState = 0;
        public string HomeTown = "PLACEHOLDER";
        public byte[] padding = new byte[16];

        public int College = 0;
        public int PLBD = 0;
        public string Birthday = "";
        public int Age = 0;
        public int Height = 0;
        public int Weight = 0;
        public int Position = 0;
        public int JerseyNum = 0;
        public int DraftPick = 0;
        public int Undrafted = 0;
        public int DraftRound = 0;
        public int Overall = 0;
        public int Awareness = 0;
        public int Accel = 0;
        public int Agility = 0;
        public int BCVision = 0;
        public int BlockShed = 0;
        public int BreakSack = 0;
        public int BreakTackle = 0;
        public int Carry = 0;
        public int Catching = 0;
        public int CatchTraffic = 0;
        public int Elusive = 0;
        public int MovesFinesse = 0;
        public int HitPower = 0;
        public int ImpactBlock = 0;
        public int Injury = 0;
        public int JukeMove = 0;
        public int Jumping = 0;
        public int KickAccuracy = 0;
        public int KickPower = 0;
        public int KickReturn = 0;
        public int LeadBlock = 0;
        public int CoverMan = 0;
        public int PassBlockFin = 0;
        public int PassBlockPow = 0;
        public int PassBlock = 0;
        public int Personality = 0;
        public int PlayAction = 0;
        public int PlayRecog = 0;
        public int MovesPower = 0;
        public int CoverPress = 0;
        public int Pursuit = 0;
        public int Release = 0;
        public int RouteDeep = 0;
        public int RouteMed = 0;
        public int RouteShort = 0;
        public int RunBlockFin = 0;
        public int RunBlockPow = 0;
        public int RunBlock = 0;
        public int Unknown01 = 0;
        public int SpecCatch = 0;
        public int Speed = 0;
        public int SpinMove = 0;
        public int Stamina = 0;
        public int StiffArm = 0;
        public int Strength = 0;
        public int Tackling = 0;
        public int AccuracyDeep = 0;
        public int AccuracyMed = 0;
        public int ThrowAccuracy = 0;
        public int AccuracyShort = 0;
        public int ThrowOnRun = 0;
        public int ThrowPower = 0;
        public int ThrowPressure = 0;
        public int Toughness = 0;
        public int Trucking = 0;
        public int CoverZone = 0;
        public int Confidence = 0;
        public int BigHitter = 0;
        public int PossCatch = 0;
        public int Clutch = 0;
        public int CoversBall = 0;
        public int DeepBall = 0;
        public int DLBull = 0;
        public int DLSpin = 0;
        public int DLSwim = 0;
        public int DropsOpen = 0;
        public int SideLineCatch = 0;
        public int FightYards = 0;
        public int ForcePass = 0;
        public int HighMotor = 0;
        public int AggCatch = 0;
        public int Penalty = 0;
        public int PlaysBall = 0;
        public int Pumpfake = 0;
        public int LBStyle = 0;
        public int SensePressure = 0;
        public int Unknown02 = 0;
        public int StripsBall = 0;
        public int TackleLow = 0;
        public int ThrowAway = 0;
        public int ThrowSpiral = 0;
        public int QBTend = 0;
        public int RAC = 0;
        public int Development = 0;
        public int Predictable = 0;
        public int BackPlate = 0;

        // body parts
        public Single RearSize = 0;
        public Single RearDef = 0;
        public Single CalfSize = 0;
        public Single CalfDef = 0;
        public Single ChestSize = 0;
        public Single ChestDef = 0;
        public Single WaistSize = 0;
        public Single WaistDef = 0;
        public Single GutSize = 0;
        public Single GutDef = 0;
        public Single ShoulderHeight = 0;
        public Single PadSize = 0;
        public Single ThighSize = 0;
        public Single ThighDef = 0;
        public Single ArmDef = 0;
        public Single ArmSize = 0;
        public Single FootDef = 0;
        public Single FootSize = 0; 

        public int Unknown03 = 0;
        public int EyePaint = 0;
        public int Facemask = 0;
        public int Unknown04 = 0;
        public int FlakJacket = 0;
        public int Unknown05 = 0;
        public UInt16 FaceID = 0;

        public int ElbowLeft = 0;
        public int HandLeft = 0;
        public int WristLeft = 0;
        public int ElbowRight = 0;
        public int HandRight = 0;
        public int WristRight = 0;
        public int LeftSleeve = 0;
        public int RightSleeve = 0;
        public int HairColor = 0;       //2019 only
        public int Handed = 0;

        public int HandWarmer = 0;
        public int Helmet = 0;
        public int JerseySleeve = 0;
        public int Unknown06 = 0;
        public int Unknown07 = 0;
        public int Unknown08 = 0;
        public int Unknown09 = 0;
        public int NeckRoll = 0;
        public int NeckType = 0;
        public int Unknown10 = 0;

        public UInt16 PortraitID = 0;
        public int QBStyle = 0;
        public int LeftKnee = 0;
        public int LeftShoe = 0;
        public int LeftSpat = 0;
        public int LeftThigh = 0;
        public int Unknown11 = 0;
        public int HeadGear = 0;
        public int Unknown12 = 0;

        public int SockHeight = 0;
        public int Unknown13 = 0;
        public int QBStance = 0;
        public int Unknown14 = 0;
        public int Tendency = 0;
        public int Towel = 0;
        public int Unknown15 = 0;
        public int Visor = 0;
        public int Unknown16 = 0;
        public int Unknown17 = 0;

        public int Unknown18 = 0;
        public int Celebration = 0;
        public UInt16 Comment = 8191;   // Generic        
        public int Unknown19 = 0;
        public int Unknown20 = 0;
        public int Unknown21 = 0;
        public int Unknown22 = 0;
        public int Unknown23 = 0;
        public int Unknown24 = 0;

        public int Unknown25 = 0;
        public int Unknown26 = 0;
        public int Unknown27 = 0;
        public int Unknown28 = 0;
        public int Unknown29 = 0;
        public int Unknown30 = 0;
        public int Unknown31 = 0;
        public int Unknown32 = 0;
        public int Unknown33 = 0;
        public int Unknown34 = 0;

        public int Unknown35 = 0;
        public int Unknown36 = 0;
        public int Unknown37 = 0;
        public int Unknown38 = 0;
        public int Unknown39 = 0;
        public int Unknown40 = 0;
        public int Unknown41 = 0;
        public int Unknown42 = 0;
        public int Unknown43 = 0;
        public int Unknown44 = 0;

        public int Unknown45 = 0;
        public int Unknown46 = 0;
        public int Unknown47 = 0;
        public int Unknown48 = 0;
        public int Unknown49 = 0;
        public int Unknown50 = 0;
        public int Unknown51 = 0;
        public int Unknown52 = 0;
        public int Unknown53 = 0;
        public int Unknown54 = 0;

        public int Unknown55 = 0;
        public int Unknown56 = 0;
        public int Unknown57 = 0;
        public int Unknown58 = 0;
        public int Unknown59 = 0;
        public int Unknown60 = 0;
        public int Unknown61 = 0;
        public int Unknown62 = 0;

        public int Unknown63 = 0;
        public int Unknown64 = 0;
        public int Unknown65 = 0;

        public int Archetype = 0;
        public int HairStyle = 0;
        
        #endregion
      
        public DraftPlayer()
        {
        }

        public UInt32 GetUnsignedValue(bool[] bits)
        {
            UInt32 uval = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                    uval += (UInt32)1 << i;
            }
            return uval;
        }

        public string GetBirthday(Int32 val)
        {
            byte[] test = new byte[4];
            test = BitConverter.GetBytes(val);
            BitArray ba = new BitArray(test);
            bool[] convert = new bool[16];
            for (int c = 0; c < 16; c++)
                convert[c] = ba[c];
            convert.Reverse();

            bool[] year = new bool[7];
            Array.Copy(convert, 0, year, 0, 7);
            
            bool[] month = new bool[4];
            Array.Copy(convert, 7, month, 0, 4);            
            
            bool[] day = new bool[5];
            Array.Copy(convert, 11, day, 0, 5);

            UInt32 m = 1 + GetUnsignedValue(month);
            UInt32 y = 1940 + GetUnsignedValue(year);
            UInt32 d = GetUnsignedValue(day);

            return m.ToString() + "/" + d.ToString() + "/" + y.ToString();
        }

        public UInt16 SetBirthday(string dob)
        {
            string[] test = dob.Split('/');
            int m = Convert.ToInt32(test[0]);
            int d = Convert.ToInt32(test[1]);
            int y = Convert.ToInt32(test[2]);
            if (m > 12)
                m = 12;
            if (d > 31)
                d = 31;
            if (y < 1940)
                y = 1940;
            y -= 1940;

            byte[] mon = BitConverter.GetBytes(m);
            byte[] da = BitConverter.GetBytes(d);
            byte[] yea = BitConverter.GetBytes(y);
            BitArray month = new BitArray(mon);
            BitArray day = new BitArray(da);
            BitArray year = new BitArray(yea);            
            bool[] res = new bool[16];
            for (int c = 0; c < 7; c++)
                res[c] = year[c];
            for (int c = 0; c < 4; c++)
                res[7 + c] = month[c];
            for (int c = 0; c < 5; c++)
                res[11 + c] = year[c];
            UInt32 playerbirthday = GetUnsignedValue(res);
            return (UInt16)playerbirthday;
        }

        public void ReadPlayer19(BinaryReader binreader)
        {
            // 2019 328 bytes
           
            long start = binreader.BaseStream.Position;

            #region First Name
            int firstlen = 14;
            
            ASCIIEncoding enc = new ASCIIEncoding();
            List<byte> bytename = new List<byte>();
            for (int c = 0; c < firstlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename.Add(b);
            }
            NameFirst = enc.GetString(bytename.ToArray());
            #endregion

            #region Last Name
            int lastlen = 18;
           
            ASCIIEncoding enc2 = new ASCIIEncoding();
            List<byte> bytename2 = new List<byte>();
            for (int c = 0; c < lastlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename2.Add(b);
            }
            NameLast = enc2.GetString(bytename2.ToArray());
            #endregion

            HomeState = binreader.ReadByte();

            #region Hometown
            // Not sure if hometown will be used but let's read it anyway                        
            ASCIIEncoding enc3 = new ASCIIEncoding();
            List<byte> bytename3 = new List<byte>();
            for (int c = 0; c < 27; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename3.Add(b);
            }            
            HomeTown = enc3.GetString(bytename3.ToArray());
            #endregion
           
            binreader.BaseStream.Position = start + 60;
           
            College = binreader.ReadUInt16();       //60
            PLBD = binreader.ReadUInt16();          //62-63            
            Age = binreader.ReadByte();             //64
            Height = binreader.ReadByte();          //65
            Weight = 160 + binreader.ReadByte();    //66 + 160 pounds            
            Position = binreader.ReadByte();        //67
            Archetype = binreader.ReadByte();       //68            
            JerseyNum = binreader.ReadByte();       //69

            DraftPick = binreader.ReadByte();       //70
            Undrafted = binreader.ReadByte();       //71
            DraftRound = binreader.ReadByte();      //72
            Overall = binreader.ReadByte();         //73
            Accel = binreader.ReadByte();           //74
            Agility = binreader.ReadByte();         //75
            Awareness = binreader.ReadByte();       //76
            BCVision = binreader.ReadByte();        //77
            BlockShed = binreader.ReadByte();       //78
            BreakSack = binreader.ReadByte();       //79

            BreakTackle = binreader.ReadByte();     //80
            Carry = binreader.ReadByte();           //81
            Catching = binreader.ReadByte();        //82
            CatchTraffic = binreader.ReadByte();    //83
            Elusive = binreader.ReadByte();         //84
            MovesFinesse = binreader.ReadByte();    //85
            HitPower = binreader.ReadByte();        //86
            ImpactBlock = binreader.ReadByte();     //87
            Injury = binreader.ReadByte();          //88
            JukeMove = binreader.ReadByte();        //89

            Jumping = binreader.ReadByte();         //90
            KickAccuracy = binreader.ReadByte();    //91
            KickPower = binreader.ReadByte();       //92
            KickReturn = binreader.ReadByte();      //93
            LeadBlock = binreader.ReadByte();       //94
            CoverMan = binreader.ReadByte();        //95
            PassBlockFin = binreader.ReadByte();    //96
            PassBlockPow = binreader.ReadByte();    //97
            PassBlock = binreader.ReadByte();       //98
            Personality = binreader.ReadByte();     //99

            PlayAction = binreader.ReadByte();      //100
            PlayRecog = binreader.ReadByte();       //101
            MovesPower = binreader.ReadByte();      //102
            CoverPress = binreader.ReadByte();      //103
            Pursuit = binreader.ReadByte();         //104
            Release = binreader.ReadByte();         //105
            RouteDeep = binreader.ReadByte();       //106
            RouteMed = binreader.ReadByte();        //107
            RouteShort = binreader.ReadByte();      //108
            RunBlockFin = binreader.ReadByte();     //109

            RunBlockPow = binreader.ReadByte();     //110
            RunBlock = binreader.ReadByte();        //111
            Unknown01 = binreader.ReadByte();      //112            
            SpecCatch = binreader.ReadByte();       //113
            Speed = binreader.ReadByte();           //114
            SpinMove = binreader.ReadByte();        //115
            Stamina = binreader.ReadByte();         //116
            StiffArm = binreader.ReadByte();        //117
            Strength = binreader.ReadByte();        //118
            Tackling = binreader.ReadByte();        //119

            AccuracyDeep = binreader.ReadByte();    //120
            AccuracyMed = binreader.ReadByte();     //121
            ThrowAccuracy = binreader.ReadByte();   //122
            AccuracyShort = binreader.ReadByte();   //123
            ThrowOnRun = binreader.ReadByte();      //124
            ThrowPower = binreader.ReadByte();      //125
            ThrowPressure = binreader.ReadByte();   //126
            Toughness = binreader.ReadByte();       //127
            Trucking = binreader.ReadByte();        //128
            CoverZone = binreader.ReadByte();       //129

            Confidence = binreader.ReadByte();      //130            
            BigHitter = binreader.ReadByte();       //131
            PossCatch = binreader.ReadByte();       //132
            Clutch = binreader.ReadByte();          //133
            CoversBall = binreader.ReadByte();      //134
            DeepBall = binreader.ReadByte();       //135            
            DLBull = binreader.ReadByte();          //136
            DLSpin = binreader.ReadByte();          //137
            DLSwim = binreader.ReadByte();          //138
            DropsOpen = binreader.ReadByte();       //139

            SideLineCatch = binreader.ReadByte();   //140
            FightYards = binreader.ReadByte();      //141
            ForcePass = binreader.ReadByte();       //142
            HighMotor = binreader.ReadByte();       //143
            AggCatch = binreader.ReadByte();        //144
            Penalty = binreader.ReadByte();         //145
            PlaysBall = binreader.ReadByte();       //146
            Pumpfake = binreader.ReadByte();        //147
            LBStyle = binreader.ReadByte();         //148
            SensePressure = binreader.ReadByte();   //149

            Unknown02 = binreader.ReadByte();       //150       //finesse moves? 2020           
            StripsBall = binreader.ReadByte();      //151
            TackleLow = binreader.ReadByte();       //152            
            ThrowAway = binreader.ReadByte();       //153
            ThrowSpiral = binreader.ReadByte();     //154
            QBTend = binreader.ReadByte();          //155
            RAC = binreader.ReadByte();             //156
            Development = binreader.ReadByte();     //157
            Predictable = binreader.ReadByte();     //158
            BackPlate = binreader.ReadByte();       //159            
            
            binreader.BaseStream.Position = start + 160;
            
            RearSize = binreader.ReadSingle();      // 160-163  
            RearDef = binreader.ReadSingle();       // 164-167
            CalfSize = binreader.ReadSingle();      // 168-171
            CalfDef = binreader.ReadSingle();       // 172-175            
            ChestSize = binreader.ReadSingle();     // 176-179  chest              
            ChestDef = binreader.ReadSingle();      // 180-183
            WaistSize = binreader.ReadSingle();     // 184-187
            WaistDef = binreader.ReadSingle();      // 188-191
            GutSize = binreader.ReadSingle();       // 192-195     
            GutDef = binreader.ReadSingle();        // 196-199
            ShoulderHeight = binreader.ReadSingle();// 200-203
            PadSize = binreader.ReadSingle();       // 204-207
            ThighSize = binreader.ReadSingle();     // 208-211    
            ThighDef = binreader.ReadSingle();      // 212-215
            ArmDef = binreader.ReadSingle();        // 216-219
            ArmSize = binreader.ReadSingle();       // 220-223
            FootDef = binreader.ReadSingle();       // 224-227
            FootSize = binreader.ReadSingle();      // 228-231            
            //
            Unknown03 = binreader.ReadByte();       //232
            EyePaint = binreader.ReadByte();        //233
            Facemask = binreader.ReadByte();        //234
            Unknown04 = binreader.ReadByte();       //235
            FlakJacket = binreader.ReadByte();      //236
            Unknown05 = binreader.ReadByte();       //237
            FaceID = binreader.ReadUInt16();        //238-239

            ElbowLeft = binreader.ReadByte();       //240
            HandLeft = binreader.ReadByte();        //241
            WristLeft = binreader.ReadByte();       //242
            ElbowRight = binreader.ReadByte();      //243
            HandRight = binreader.ReadByte();       //244
            WristRight = binreader.ReadByte();      //245
            LeftSleeve = binreader.ReadByte();      //246
            RightSleeve = binreader.ReadByte();     //247
            HairColor = binreader.ReadByte();       //248 
            Handed = binreader.ReadByte();          //249

            HandWarmer = binreader.ReadByte();      //250
            Helmet = binreader.ReadByte();          //251
            JerseySleeve = binreader.ReadByte();    //252
            Unknown06 = binreader.ReadByte();       //253
            Unknown07 = binreader.ReadByte();       //254
            Unknown08 = binreader.ReadByte();       //255
            Unknown09 = binreader.ReadByte();       //256
            NeckRoll = binreader.ReadByte();        //257
            NeckType = binreader.ReadByte();        //258
            Unknown10 = binreader.ReadByte();       //259

            PortraitID = binreader.ReadUInt16();    //260-261
            QBStyle = binreader.ReadByte();         //262
            LeftKnee = binreader.ReadByte();        //263
            LeftShoe = binreader.ReadByte();        //264
            LeftSpat = binreader.ReadByte();        //265
            LeftThigh = binreader.ReadByte();       //266
            Unknown11 = binreader.ReadByte();       //267
            HeadGear = binreader.ReadByte();        //268
            Unknown12 = binreader.ReadByte();       //269

            SockHeight = binreader.ReadByte();      //270
            Unknown13 = binreader.ReadByte();       //271
            QBStance = binreader.ReadByte();        //272
            Unknown14 = binreader.ReadByte();       //273
            Tendency = binreader.ReadByte();        //274
            Towel = binreader.ReadByte();           //275
            Unknown15 = binreader.ReadByte();       //276
            Visor = binreader.ReadByte();           //277
            Unknown16 = binreader.ReadByte();       //278
            Unknown17 = binreader.ReadByte();       //279

            Unknown18 = binreader.ReadByte();            
            Celebration = binreader.ReadByte();
            Comment = binreader.ReadUInt16();       //282-283            
            Unknown19 = binreader.ReadByte();
            Unknown20 = binreader.ReadByte();
            Unknown21 = binreader.ReadByte();
            Unknown22 = binreader.ReadByte();
            Unknown23 = binreader.ReadByte();
            Unknown24 = binreader.ReadByte();

            Unknown25 = binreader.ReadByte();
            Unknown26 = binreader.ReadByte();
            Unknown27 = binreader.ReadByte();
            Unknown28 = binreader.ReadByte();
            Unknown29 = binreader.ReadByte();
            Unknown30 = binreader.ReadByte();
            Unknown31 = binreader.ReadByte();
            Unknown32 = binreader.ReadByte();
            Unknown33 = binreader.ReadByte();
            Unknown34 = binreader.ReadByte();

            Unknown35 = binreader.ReadByte();
            Unknown36 = binreader.ReadByte();
            Unknown37 = binreader.ReadByte();
            Unknown38 = binreader.ReadByte();
            Unknown39 = binreader.ReadByte();
            Unknown40 = binreader.ReadByte();
            Unknown41 = binreader.ReadByte();
            Unknown42 = binreader.ReadByte();
            Unknown43 = binreader.ReadByte();
            Unknown44 = binreader.ReadByte();

            Unknown45 = binreader.ReadByte();
            Unknown46 = binreader.ReadByte();
            Unknown47 = binreader.ReadByte();
            Unknown48 = binreader.ReadByte();
            Unknown49 = binreader.ReadByte();
            Unknown50 = binreader.ReadByte();
            Unknown51 = binreader.ReadByte();
            Unknown52 = binreader.ReadByte();
            Unknown53 = binreader.ReadByte();
            Unknown54 = binreader.ReadByte();

            Unknown55 = binreader.ReadByte();
            Unknown56 = binreader.ReadByte();
            Unknown57 = binreader.ReadByte();
            Unknown58 = binreader.ReadByte();
            Unknown59 = binreader.ReadByte();
            Unknown60 = binreader.ReadByte();
            Unknown61 = binreader.ReadByte();
            Unknown62 = binreader.ReadByte();
        }

        public void ReadPlayer20(BinaryReader binreader)
        {
            // 2020 336 bytes

            long start = binreader.BaseStream.Position;

            #region First Name
            int firstlen = 17;
            ASCIIEncoding enc = new ASCIIEncoding();
            List<byte> bytename = new List<byte>();
            for (int c = 0; c < firstlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename.Add(b);
            }
            NameFirst = enc.GetString(bytename.ToArray());
            #endregion

            #region Last Name
            int lastlen = 21;
            ASCIIEncoding enc2 = new ASCIIEncoding();
            List<byte> bytename2 = new List<byte>();
            for (int c = 0; c < lastlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename2.Add(b);
            }
            NameLast = enc2.GetString(bytename2.ToArray());
            #endregion

            HomeState = binreader.ReadByte();

            #region Hometown
            // Not sure if hometown will be used but let's read it anyway                        
            ASCIIEncoding enc3 = new ASCIIEncoding();
            List<byte> bytename3 = new List<byte>();
            for (int c = 0; c < 27; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename3.Add(b);
            }
            HomeTown = enc3.GetString(bytename3.ToArray());
            #endregion

            binreader.BaseStream.Position = start + 66;
            
            College = binreader.ReadUInt16();       //66
            PLBD = binreader.ReadUInt16();          //68-69            
            Age = binreader.ReadByte();             //70            
            Height = binreader.ReadByte();          //71
            Weight = 160 + binreader.ReadByte();    //72 + 160 pounds            
            Position = binreader.ReadByte();        //73
            Archetype = binreader.ReadByte();       //74            
            JerseyNum = binreader.ReadByte();       //75
            DraftPick = binreader.ReadByte();       //76
            Undrafted = binreader.ReadByte();       //77
            DraftRound = binreader.ReadByte();      //78
            Overall = binreader.ReadByte();         //79
            Accel = binreader.ReadByte();           //80            
            Agility = binreader.ReadByte();         //81
            Awareness = binreader.ReadByte();       //82
            BCVision = binreader.ReadByte();        //83
            BlockShed = binreader.ReadByte();       //84
            BreakSack = binreader.ReadByte();       //85
            BreakTackle = binreader.ReadByte();     //86
            Carry = binreader.ReadByte();           //87
            Catching = binreader.ReadByte();        //88
            CatchTraffic = binreader.ReadByte();    //89
            Elusive = binreader.ReadByte();         //90
            MovesFinesse = binreader.ReadByte();    //91
            HitPower = binreader.ReadByte();        //92
            ImpactBlock = binreader.ReadByte();     //93
            Injury = binreader.ReadByte();          //94
            JukeMove = binreader.ReadByte();        //95
            Jumping = binreader.ReadByte();         //96
            KickAccuracy = binreader.ReadByte();    //97
            KickPower = binreader.ReadByte();       //98
            KickReturn = binreader.ReadByte();      //99
            LeadBlock = binreader.ReadByte();       //100
            CoverMan = binreader.ReadByte();        //101
            PassBlockFin = binreader.ReadByte();    //102
            PassBlockPow = binreader.ReadByte();    //103
            PassBlock = binreader.ReadByte();       //104
            Personality = binreader.ReadByte();     //105
            PlayAction = binreader.ReadByte();      //106
            PlayRecog = binreader.ReadByte();       //107
            MovesPower = binreader.ReadByte();      //108
            CoverPress = binreader.ReadByte();      //109
            Pursuit = binreader.ReadByte();         //110
            Release = binreader.ReadByte();         //111
            RouteDeep = binreader.ReadByte();       //112
            RouteMed = binreader.ReadByte();        //113
            RouteShort = binreader.ReadByte();      //114
            RunBlockFin = binreader.ReadByte();     //115
            RunBlockPow = binreader.ReadByte();     //116
            RunBlock = binreader.ReadByte();        //117
            Unknown01 = binreader.ReadByte();       //118            
            SpecCatch = binreader.ReadByte();       //119
            Speed = binreader.ReadByte();           //120
            SpinMove = binreader.ReadByte();        //121
            Stamina = binreader.ReadByte();         //122
            StiffArm = binreader.ReadByte();        //123
            Strength = binreader.ReadByte();        //124
            Tackling = binreader.ReadByte();        //125
            AccuracyDeep = binreader.ReadByte();    //126
            AccuracyMed = binreader.ReadByte();     //127
            ThrowAccuracy = binreader.ReadByte();   //128
            AccuracyShort = binreader.ReadByte();   //129
            ThrowOnRun = binreader.ReadByte();      //130
            ThrowPower = binreader.ReadByte();      //131
            ThrowPressure = binreader.ReadByte();   //132
            Toughness = binreader.ReadByte();       //133
            Trucking = binreader.ReadByte();        //134
            CoverZone = binreader.ReadByte();       //135
            Confidence = binreader.ReadByte();      //136            
            BigHitter = binreader.ReadByte();       //137
            PossCatch = binreader.ReadByte();       //138
            Clutch = binreader.ReadByte();          //139
            CoversBall = binreader.ReadByte();      //140
            DeepBall = binreader.ReadByte();       //141           
            DLBull = binreader.ReadByte();          //142
            DLSpin = binreader.ReadByte();          //143
            DLSwim = binreader.ReadByte();          //144
            DropsOpen = binreader.ReadByte();       //145
            SideLineCatch = binreader.ReadByte();   //146
            FightYards = binreader.ReadByte();      //147
            ForcePass = binreader.ReadByte();       //148
            HighMotor = binreader.ReadByte();       //149
            AggCatch = binreader.ReadByte();        //150
            Penalty = binreader.ReadByte();         //151
            PlaysBall = binreader.ReadByte();       //152
            Pumpfake = binreader.ReadByte();        //153
            LBStyle = binreader.ReadByte();         //154
            SensePressure = binreader.ReadByte();   //155
            Unknown02 = binreader.ReadByte();       //156 
            StripsBall = binreader.ReadByte();      //157
            TackleLow = binreader.ReadByte();       //158            
            ThrowAway = binreader.ReadByte();       //159
            ThrowSpiral = binreader.ReadByte();     //160
            QBTend = binreader.ReadByte();          //161
            RAC = binreader.ReadByte();             //162
            Development = binreader.ReadByte();     //163
            Predictable = binreader.ReadByte();     //164
            BackPlate = binreader.ReadByte();       //165 
            Unknown63 = binreader.ReadByte();       //165
            Unknown64 = binreader.ReadByte();       //167
            binreader.BaseStream.Position = start + 168;
            RearSize = binreader.ReadSingle();      // 168
            RearDef = binreader.ReadSingle();       // 172
            CalfSize = binreader.ReadSingle();      // 176
            CalfDef = binreader.ReadSingle();       // 180            
            ChestSize = binreader.ReadSingle();     // 184              
            ChestDef = binreader.ReadSingle();      // 188
            WaistSize = binreader.ReadSingle();     // 192
            WaistDef = binreader.ReadSingle();      // 196
            GutSize = binreader.ReadSingle();       // 200
            GutDef = binreader.ReadSingle();        // 204
            ShoulderHeight = binreader.ReadSingle();// 208
            PadSize = binreader.ReadSingle();       // 212
            ThighSize = binreader.ReadSingle();     // 216    
            ThighDef = binreader.ReadSingle();      // 220
            ArmDef = binreader.ReadSingle();        // 224
            ArmSize = binreader.ReadSingle();       // 228
            FootDef = binreader.ReadSingle();       // 232
            FootSize = binreader.ReadSingle();      // 236
            Unknown03 = binreader.ReadByte();       //240
            EyePaint = binreader.ReadByte();        //241
            Facemask = binreader.ReadByte();        //242
            Unknown04 = binreader.ReadByte();       //243
            FlakJacket = binreader.ReadByte();      //244
            Unknown05 = binreader.ReadByte();       //245
            FaceID = binreader.ReadUInt16();        //246            
            ElbowLeft = binreader.ReadByte();       //248
            HandLeft = binreader.ReadByte();        //249
            WristLeft = binreader.ReadByte();       //250
            ElbowRight = binreader.ReadByte();      //251
            HandRight = binreader.ReadByte();       //252
            WristRight = binreader.ReadByte();      //253

            Unknown65 = binreader.ReadByte();       //254
            LeftSleeve = binreader.ReadByte();      //255
            RightSleeve = binreader.ReadByte();     //256
            HairColor = binreader.ReadByte();       //257
            Handed = binreader.ReadByte();          //258
            HandWarmer = binreader.ReadByte();      //259
            Helmet = binreader.ReadByte();          //260
            JerseySleeve = binreader.ReadByte();    //261
            Unknown06 = binreader.ReadByte();       //262
            Unknown07 = binreader.ReadByte();       //263
            Unknown08 = binreader.ReadByte();       //264
            Unknown09 = binreader.ReadByte();       //265
            NeckRoll = binreader.ReadByte();        //266
            NeckType = binreader.ReadByte();        //267

            PortraitID = binreader.ReadUInt16();    //268-269
            QBStyle = binreader.ReadByte();         //270
            LeftKnee = binreader.ReadByte();        //271
            LeftShoe = binreader.ReadByte();        //272
            LeftSpat = binreader.ReadByte();        //273
            LeftThigh = binreader.ReadByte();       //274
            Unknown11 = binreader.ReadByte();      //275
            HeadGear = binreader.ReadByte();       //276
            Unknown12 = binreader.ReadByte();      //277
            SockHeight = binreader.ReadByte();     //278
            Unknown13 = binreader.ReadByte();      //279
            QBStance = binreader.ReadByte();       //280
            Unknown14 = binreader.ReadByte();      //281
            Tendency = binreader.ReadByte();       //282
            Towel = binreader.ReadByte();          //283
            Unknown15 = binreader.ReadByte();      //283
            Visor = binreader.ReadByte();          //284
            Unknown16 = binreader.ReadByte();      //285
            Unknown17 = binreader.ReadByte();      //286
            Unknown18 = binreader.ReadByte();      //287
            Celebration = binreader.ReadByte();    //288
            Comment = binreader.ReadUInt16();      //289-290
            Unknown19 = binreader.ReadByte();      //291
            Unknown20 = binreader.ReadByte();      //292
            Unknown21 = binreader.ReadByte();      //293
            Unknown22 = binreader.ReadByte();      //294
            Unknown23 = binreader.ReadByte();      //295
            Unknown24 = binreader.ReadByte();      //296
            Unknown25 = binreader.ReadByte();      //297
            Unknown26 = binreader.ReadByte();      //298
            Unknown27 = binreader.ReadByte();      //299
            Unknown28 = binreader.ReadByte();      //300
            Unknown29 = binreader.ReadByte();      //301
            Unknown30 = binreader.ReadByte();      //302
            Unknown31 = binreader.ReadByte();      //303
            Unknown32 = binreader.ReadByte();      //304
            Unknown33 = binreader.ReadByte();      //305
            Unknown34 = binreader.ReadByte();      //306
            Unknown35 = binreader.ReadByte();      //307
            Unknown36 = binreader.ReadByte();      //308
            Unknown37 = binreader.ReadByte();      //309
            Unknown38 = binreader.ReadByte();      //310
            Unknown39 = binreader.ReadByte();      //311
            Unknown40 = binreader.ReadByte();      //312
            Unknown41 = binreader.ReadByte();      //313
            Unknown42 = binreader.ReadByte();      //314
            Unknown43 = binreader.ReadByte();      //315
            Unknown44 = binreader.ReadByte();      //316
            Unknown45 = binreader.ReadByte();      //317
            Unknown46 = binreader.ReadByte();      //318
            Unknown47 = binreader.ReadByte();      //319
            Unknown48 = binreader.ReadByte();      //320
            Unknown49 = binreader.ReadByte();      //321
            Unknown50 = binreader.ReadByte();      //322
            Unknown51 = binreader.ReadByte();      //323
            Unknown52 = binreader.ReadByte();      //324
            Unknown53 = binreader.ReadByte();      //325
            Unknown54 = binreader.ReadByte();      //326
            Unknown55 = binreader.ReadByte();      //327
            Unknown56 = binreader.ReadByte();      //328
            Unknown57 = binreader.ReadByte();      //329
            Unknown58 = binreader.ReadByte();      //330
            Unknown59 = binreader.ReadByte();      //331
            Unknown60 = binreader.ReadByte();      //332
            Unknown61 = binreader.ReadByte();      //333
            Unknown62 = binreader.ReadByte();      //334
        }

        public void ReadPlayer(BinaryReader binreader, MaddenFileVersion version)
        {            
            long start = binreader.BaseStream.Position;
            int firstlen = 14;
            int lastlen = 18;
            int homelen = 27;
            if (version == MaddenFileVersion.Ver2020)
            {
                firstlen = 17;
                lastlen = 21;
            }
            #region First Name
            ASCIIEncoding enc = new ASCIIEncoding();
            List<byte> bytename = new List<byte>();
            for (int c = 0; c < firstlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename.Add(b);
            }
            NameFirst = enc.GetString(bytename.ToArray());
            #endregion

            #region Last Name
           
            ASCIIEncoding enc2 = new ASCIIEncoding();
            List<byte> bytename2 = new List<byte>();
            for (int c = 0; c < lastlen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename2.Add(b);
            }
            NameLast = enc2.GetString(bytename2.ToArray());
            #endregion

            HomeState = binreader.ReadByte();

            #region Hometown
            // Not sure if hometown will be used but let's read it anyway                        
            ASCIIEncoding enc3 = new ASCIIEncoding();
            List<byte> bytename3 = new List<byte>();
            for (int c = 0; c < homelen; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename3.Add(b);
            }
            HomeTown = enc3.GetString(bytename3.ToArray());
            #endregion
  
            College = binreader.ReadUInt16();       //66
            PLBD = binreader.ReadUInt16();          //68-69            
            Age = binreader.ReadByte();             //70            
            Height = binreader.ReadByte();          //71
            Weight = 160 + binreader.ReadByte();    //72 + 160 pounds            
            Position = binreader.ReadByte();        //73
            Archetype = binreader.ReadByte();       //74            
            JerseyNum = binreader.ReadByte();       //75
            DraftPick = binreader.ReadByte();       //76
            Undrafted = binreader.ReadByte();       //77
            DraftRound = binreader.ReadByte();      //78
            Overall = binreader.ReadByte();         //79
            Accel = binreader.ReadByte();           //80            
            Agility = binreader.ReadByte();         //81
            Awareness = binreader.ReadByte();       //82
            BCVision = binreader.ReadByte();        //83
            BlockShed = binreader.ReadByte();       //84
            BreakSack = binreader.ReadByte();       //85
            BreakTackle = binreader.ReadByte();     //86
            Carry = binreader.ReadByte();           //87
            Catching = binreader.ReadByte();        //88
            CatchTraffic = binreader.ReadByte();    //89
            Elusive = binreader.ReadByte();         //90
            MovesFinesse = binreader.ReadByte();    //91
            HitPower = binreader.ReadByte();        //92
            ImpactBlock = binreader.ReadByte();     //93
            Injury = binreader.ReadByte();          //94
            JukeMove = binreader.ReadByte();        //95
            Jumping = binreader.ReadByte();         //96
            KickAccuracy = binreader.ReadByte();    //97
            KickPower = binreader.ReadByte();       //98
            KickReturn = binreader.ReadByte();      //99
            LeadBlock = binreader.ReadByte();       //100
            CoverMan = binreader.ReadByte();        //101
            PassBlockFin = binreader.ReadByte();    //102
            PassBlockPow = binreader.ReadByte();    //103
            PassBlock = binreader.ReadByte();       //104
            Personality = binreader.ReadByte();     //105
            PlayAction = binreader.ReadByte();      //106
            PlayRecog = binreader.ReadByte();       //107
            MovesPower = binreader.ReadByte();      //108
            CoverPress = binreader.ReadByte();      //109
            Pursuit = binreader.ReadByte();         //110
            Release = binreader.ReadByte();         //111
            RouteDeep = binreader.ReadByte();       //112
            RouteMed = binreader.ReadByte();        //113
            RouteShort = binreader.ReadByte();      //114
            RunBlockFin = binreader.ReadByte();     //115
            RunBlockPow = binreader.ReadByte();     //116
            RunBlock = binreader.ReadByte();        //117
            Unknown01 = binreader.ReadByte();       //118            
            SpecCatch = binreader.ReadByte();       //119
            Speed = binreader.ReadByte();           //120
            SpinMove = binreader.ReadByte();        //121
            Stamina = binreader.ReadByte();         //122
            StiffArm = binreader.ReadByte();        //123
            Strength = binreader.ReadByte();        //124
            Tackling = binreader.ReadByte();        //125
            AccuracyDeep = binreader.ReadByte();    //126
            AccuracyMed = binreader.ReadByte();     //127
            ThrowAccuracy = binreader.ReadByte();   //128
            AccuracyShort = binreader.ReadByte();   //129
            ThrowOnRun = binreader.ReadByte();      //130
            ThrowPower = binreader.ReadByte();      //131
            ThrowPressure = binreader.ReadByte();   //132
            Toughness = binreader.ReadByte();       //133
            Trucking = binreader.ReadByte();        //134
            CoverZone = binreader.ReadByte();       //135
            Confidence = binreader.ReadByte();      //136            
            BigHitter = binreader.ReadByte();       //137
            PossCatch = binreader.ReadByte();       //138
            Clutch = binreader.ReadByte();          //139
            CoversBall = binreader.ReadByte();      //140
            DeepBall = binreader.ReadByte();        //141           
            DLBull = binreader.ReadByte();          //142
            DLSpin = binreader.ReadByte();          //143
            DLSwim = binreader.ReadByte();          //144
            DropsOpen = binreader.ReadByte();       //145
            SideLineCatch = binreader.ReadByte();   //146
            FightYards = binreader.ReadByte();      //147
            ForcePass = binreader.ReadByte();       //148
            HighMotor = binreader.ReadByte();       //149
            AggCatch = binreader.ReadByte();        //150
            Penalty = binreader.ReadByte();         //151
            PlaysBall = binreader.ReadByte();       //152
            Pumpfake = binreader.ReadByte();        //153
            LBStyle = binreader.ReadByte();         //154
            SensePressure = binreader.ReadByte();   //155
            Unknown02 = binreader.ReadByte();       //156 
            StripsBall = binreader.ReadByte();      //157
            TackleLow = binreader.ReadByte();       //158            
            ThrowAway = binreader.ReadByte();       //159
            ThrowSpiral = binreader.ReadByte();     //160
            QBTend = binreader.ReadByte();          //161
            RAC = binreader.ReadByte();             //162
            Development = binreader.ReadByte();     //163
            Predictable = binreader.ReadByte();     //164
            BackPlate = binreader.ReadByte();       //165 
            Unknown63 = binreader.ReadByte();       //165
            Unknown64 = binreader.ReadByte();       //167
            binreader.BaseStream.Position = start + 168;
            RearSize = binreader.ReadSingle();      // 168
            RearDef = binreader.ReadSingle();       // 172
            CalfSize = binreader.ReadSingle();      // 176
            CalfDef = binreader.ReadSingle();       // 180            
            ChestSize = binreader.ReadSingle();     // 184              
            ChestDef = binreader.ReadSingle();      // 188
            WaistSize = binreader.ReadSingle();     // 192
            WaistDef = binreader.ReadSingle();      // 196
            GutSize = binreader.ReadSingle();       // 200
            GutDef = binreader.ReadSingle();        // 204
            ShoulderHeight = binreader.ReadSingle();// 208
            PadSize = binreader.ReadSingle();       // 212
            ThighSize = binreader.ReadSingle();     // 216    
            ThighDef = binreader.ReadSingle();      // 220
            ArmDef = binreader.ReadSingle();        // 224
            ArmSize = binreader.ReadSingle();       // 228
            FootDef = binreader.ReadSingle();       // 232
            FootSize = binreader.ReadSingle();      // 236
            Unknown03 = binreader.ReadByte();       //240
            EyePaint = binreader.ReadByte();        //241
            Facemask = binreader.ReadByte();        //242
            Unknown04 = binreader.ReadByte();       //243
            FlakJacket = binreader.ReadByte();      //244
            Unknown05 = binreader.ReadByte();       //245
            FaceID = binreader.ReadUInt16();        //246            
            ElbowLeft = binreader.ReadByte();       //248
            HandLeft = binreader.ReadByte();        //249
            WristLeft = binreader.ReadByte();       //250
            ElbowRight = binreader.ReadByte();      //251
            HandRight = binreader.ReadByte();       //252
            WristRight = binreader.ReadByte();      //253
            Unknown65 = binreader.ReadByte();       //254
            LeftSleeve = binreader.ReadByte();      //255
            RightSleeve = binreader.ReadByte();     //256
            HairColor = binreader.ReadByte();       //257
            Handed = binreader.ReadByte();          //258
            HandWarmer = binreader.ReadByte();      //259
            Helmet = binreader.ReadByte();          //260
            JerseySleeve = binreader.ReadByte();    //261
            Unknown06 = binreader.ReadByte();       //262
            Unknown07 = binreader.ReadByte();       //263
            Unknown08 = binreader.ReadByte();       //264
            Unknown09 = binreader.ReadByte();       //265
            NeckRoll = binreader.ReadByte();        //266
            NeckType = binreader.ReadByte();        //267
            PortraitID = binreader.ReadUInt16();    //268-269
            QBStyle = binreader.ReadByte();         //270
            LeftKnee = binreader.ReadByte();        //271
            LeftShoe = binreader.ReadByte();        //272
            LeftSpat = binreader.ReadByte();        //273
            LeftThigh = binreader.ReadByte();       //274
            Unknown11 = binreader.ReadByte();      //275
            HeadGear = binreader.ReadByte();       //276
            Unknown12 = binreader.ReadByte();      //277
            SockHeight = binreader.ReadByte();     //278
            Unknown13 = binreader.ReadByte();      //279
            QBStance = binreader.ReadByte();       //280
            Unknown14 = binreader.ReadByte();      //281
            Tendency = binreader.ReadByte();       //282
            Towel = binreader.ReadByte();          //283
            Unknown15 = binreader.ReadByte();      //283
            Visor = binreader.ReadByte();          //284
            Unknown16 = binreader.ReadByte();      //285
            Unknown17 = binreader.ReadByte();      //286
            Unknown18 = binreader.ReadByte();      //287
            Celebration = binreader.ReadByte();    //288
            Comment = binreader.ReadUInt16();      //289-290
            Unknown19 = binreader.ReadByte();      //291
            Unknown20 = binreader.ReadByte();      //292
            Unknown21 = binreader.ReadByte();      //293
            Unknown22 = binreader.ReadByte();      //294
            Unknown23 = binreader.ReadByte();      //295
            Unknown24 = binreader.ReadByte();      //296
            Unknown25 = binreader.ReadByte();      //297
            Unknown26 = binreader.ReadByte();      //298
            Unknown27 = binreader.ReadByte();      //299
            Unknown28 = binreader.ReadByte();      //300
            Unknown29 = binreader.ReadByte();      //301
            Unknown30 = binreader.ReadByte();      //302
            Unknown31 = binreader.ReadByte();      //303
            Unknown32 = binreader.ReadByte();      //304
            Unknown33 = binreader.ReadByte();      //305
            Unknown34 = binreader.ReadByte();      //306
            Unknown35 = binreader.ReadByte();      //307
            Unknown36 = binreader.ReadByte();      //308
            Unknown37 = binreader.ReadByte();      //309
            Unknown38 = binreader.ReadByte();      //310
            Unknown39 = binreader.ReadByte();      //311
            Unknown40 = binreader.ReadByte();      //312
            Unknown41 = binreader.ReadByte();      //313
            Unknown42 = binreader.ReadByte();      //314
            Unknown43 = binreader.ReadByte();      //315
            Unknown44 = binreader.ReadByte();      //316
            Unknown45 = binreader.ReadByte();      //317
            Unknown46 = binreader.ReadByte();      //318
            Unknown47 = binreader.ReadByte();      //319
            Unknown48 = binreader.ReadByte();      //320
            Unknown49 = binreader.ReadByte();      //321
            Unknown50 = binreader.ReadByte();      //322
            Unknown51 = binreader.ReadByte();      //323
            Unknown52 = binreader.ReadByte();      //324
            Unknown53 = binreader.ReadByte();      //325
            Unknown54 = binreader.ReadByte();      //326
            Unknown55 = binreader.ReadByte();      //327
            Unknown56 = binreader.ReadByte();      //328
            Unknown57 = binreader.ReadByte();      //329
            Unknown58 = binreader.ReadByte();      //330
            Unknown59 = binreader.ReadByte();      //331
            Unknown60 = binreader.ReadByte();      //332
            Unknown61 = binreader.ReadByte();      //333
            Unknown62 = binreader.ReadByte();      //334
        }
        
        public void WritePlayer(BinaryWriter binwriter, MaddenFileVersion version)
        {
            #region First/Last Name
            int firstlen = 14;
            if (version == MaddenFileVersion.Ver2020)
                firstlen = 17;
            byte[] ascii = System.Text.Encoding.ASCII.GetBytes(NameFirst);
            for (int c = 0; c < firstlen; c++)
            {
                if (c < ascii.Length)
                    binwriter.Write(ascii[c]);
                else binwriter.Write((byte)0);
            }
            int lastlen = 18;
            if (version == MaddenFileVersion.Ver2020)
                lastlen = 21;
            byte[] ascii2 = System.Text.Encoding.ASCII.GetBytes(NameLast);
            for (int c = 0; c < lastlen; c++)
            {
                if (c < ascii2.Length)
                    binwriter.Write(ascii2[c]);
                else binwriter.Write((byte)0);
            }
            #endregion

            binwriter.Write((byte)HomeState);

            #region Hometown
            byte[] ascii3 = System.Text.Encoding.ASCII.GetBytes(HomeTown);
            byte[] ph = System.Text.Encoding.ASCII.GetBytes(HomeTown);
            for (int c = 0; c < 27; c++)
            {
                if (c < ascii3.Length)
                    binwriter.Write(ascii3[c]);
                else binwriter.Write((byte)0);
            }
            #endregion

            binwriter.Write((UInt16)College);
            binwriter.Write((UInt16)PLBD);
            binwriter.Write((byte)Age);
            binwriter.Write((byte)Height);
            binwriter.Write((byte)(Weight - 160));
            binwriter.Write((byte)Position);
            binwriter.Write((byte)Archetype);
            binwriter.Write((byte)JerseyNum);
            binwriter.Write((byte)DraftPick);       //70
            binwriter.Write((byte)Undrafted);       //71
            binwriter.Write((byte)DraftRound);      //72
            binwriter.Write((byte)Overall);         //73
            binwriter.Write((byte)Accel);
            binwriter.Write((byte)Agility);
            binwriter.Write((byte)Awareness);
            binwriter.Write((byte)BCVision);
            binwriter.Write((byte)BlockShed);
            binwriter.Write((byte)BreakSack);
            binwriter.Write((byte)BreakTackle);     //80
            binwriter.Write((byte)Carry);
            binwriter.Write((byte)Catching);
            binwriter.Write((byte)CatchTraffic);
            binwriter.Write((byte)Elusive);
            binwriter.Write((byte)MovesFinesse);
            binwriter.Write((byte)HitPower);
            binwriter.Write((byte)ImpactBlock);
            binwriter.Write((byte)Injury);
            binwriter.Write((byte)JukeMove);

            binwriter.Write((byte)Jumping);         //90
            binwriter.Write((byte)KickAccuracy);
            binwriter.Write((byte)KickPower);
            binwriter.Write((byte)KickReturn);
            binwriter.Write((byte)LeadBlock);
            binwriter.Write((byte)CoverMan);
            binwriter.Write((byte)PassBlockFin);
            binwriter.Write((byte)PassBlockPow);
            binwriter.Write((byte)PassBlock);
            binwriter.Write((byte)Personality);

            binwriter.Write((byte)PlayAction);  //100
            binwriter.Write((byte)PlayRecog);
            binwriter.Write((byte)MovesPower);
            binwriter.Write((byte)CoverPress);
            binwriter.Write((byte)Pursuit);
            binwriter.Write((byte)Release);
            binwriter.Write((byte)RouteDeep);
            binwriter.Write((byte)RouteMed);
            binwriter.Write((byte)RouteShort);
            binwriter.Write((byte)RunBlockFin);

            binwriter.Write((byte)RunBlockPow); //110
            binwriter.Write((byte)RunBlock);
            binwriter.Write((byte)Unknown01);
            binwriter.Write((byte)SpecCatch);
            binwriter.Write((byte)Speed);
            binwriter.Write((byte)SpinMove);
            binwriter.Write((byte)Stamina);
            binwriter.Write((byte)StiffArm);
            binwriter.Write((byte)Strength);
            binwriter.Write((byte)Tackling);

            binwriter.Write((byte)AccuracyDeep);    //120
            binwriter.Write((byte)AccuracyMed);
            binwriter.Write((byte)ThrowAccuracy);
            binwriter.Write((byte)AccuracyShort);
            binwriter.Write((byte)ThrowOnRun);
            binwriter.Write((byte)ThrowPower);
            binwriter.Write((byte)ThrowPressure);
            binwriter.Write((byte)Toughness);
            binwriter.Write((byte)Trucking);
            binwriter.Write((byte)CoverZone);

            binwriter.Write((byte)Confidence);      //130
            binwriter.Write((byte)BigHitter);
            binwriter.Write((byte)PossCatch);
            binwriter.Write((byte)Clutch);
            binwriter.Write((byte)CoversBall);
            binwriter.Write((byte)DeepBall);
            binwriter.Write((byte)DLBull);
            binwriter.Write((byte)DLSpin);
            binwriter.Write((byte)DLSwim);
            binwriter.Write((byte)DropsOpen);

            binwriter.Write((byte)SideLineCatch);   //140
            binwriter.Write((byte)FightYards);
            binwriter.Write((byte)ForcePass);
            binwriter.Write((byte)HighMotor);
            binwriter.Write((byte)AggCatch);
            binwriter.Write((byte)Penalty);
            binwriter.Write((byte)PlaysBall);       //146
            binwriter.Write((byte)Pumpfake);
            binwriter.Write((byte)LBStyle);
            binwriter.Write((byte)SensePressure);

            binwriter.Write((byte)Unknown02);
            binwriter.Write((byte)StripsBall);
            binwriter.Write((byte)TackleLow);
            binwriter.Write((byte)ThrowAway);
            binwriter.Write((byte)ThrowSpiral);
            binwriter.Write((byte)QBTend);
            binwriter.Write((byte)RAC);
            binwriter.Write((byte)Development);
            binwriter.Write((byte)Predictable);
            binwriter.Write((byte)BackPlate);
            if (version == MaddenFileVersion.Ver2020)
            {
                binwriter.Write((byte)Unknown63);
                binwriter.Write((byte)Unknown64);
            }
            binwriter.Write(RearSize);              // 160-163
            binwriter.Write(RearDef);               // 164-167
            binwriter.Write(CalfSize);              // 168-171
            binwriter.Write(CalfDef);               // 172-175            
            binwriter.Write(ChestSize);             // 176-179  chest              
            binwriter.Write(ChestDef);              // 180-183
            binwriter.Write(WaistSize);             // 184-187
            binwriter.Write(WaistDef);              // 188-181
            binwriter.Write(GutSize);               // 192-195     
            binwriter.Write(GutDef);                // 196-199
            binwriter.Write(ShoulderHeight);        // 200-203
            binwriter.Write(PadSize);               // 204-207
            binwriter.Write(ThighSize);             // 208-211    
            binwriter.Write(ThighDef);              // 212-215
            binwriter.Write(ArmDef);                // 216-219
            binwriter.Write(ArmSize);               // 220-223
            binwriter.Write(FootDef);               // 224-227
            binwriter.Write(FootSize);              // 228-231
            binwriter.Write((byte)Unknown03);
            binwriter.Write((byte)EyePaint);
            binwriter.Write((byte)Facemask);
            binwriter.Write((byte)Unknown04);
            binwriter.Write((byte)FlakJacket);
            binwriter.Write((byte)Unknown05);
            binwriter.Write(FaceID);
            binwriter.Write((byte)ElbowLeft);
            binwriter.Write((byte)HandLeft);
            binwriter.Write((byte)WristLeft);
            binwriter.Write((byte)ElbowRight);
            binwriter.Write((byte)HandRight);
            binwriter.Write((byte)WristRight);

            if (version == MaddenFileVersion.Ver2020)               
                binwriter.Write((byte)Unknown65);
            
            binwriter.Write((byte)LeftSleeve);
            binwriter.Write((byte)RightSleeve);
            binwriter.Write((byte)HairColor);
            binwriter.Write((byte)Handed);
            binwriter.Write((byte)HandWarmer);
            binwriter.Write((byte)Helmet);
            binwriter.Write((byte)JerseySleeve);
            binwriter.Write((byte)Unknown06);
            binwriter.Write((byte)Unknown07);
            binwriter.Write((byte)Unknown08);
            binwriter.Write((byte)Unknown09);
            binwriter.Write((byte)NeckRoll);
            binwriter.Write((byte)NeckType);

            if (version == MaddenFileVersion.Ver2019)
                binwriter.Write((byte)Unknown10);

            binwriter.Write(PortraitID);            
            binwriter.Write((byte)QBStyle);
            binwriter.Write((byte)LeftKnee);
            binwriter.Write((byte)LeftShoe);
            binwriter.Write((byte)LeftSpat);
            binwriter.Write((byte)LeftThigh);
            binwriter.Write((byte)Unknown11);
            binwriter.Write((byte)HeadGear);
            binwriter.Write((byte)Unknown12);

            binwriter.Write((byte)SockHeight);
            binwriter.Write((byte)Unknown13);
            binwriter.Write((byte)QBStance);
            binwriter.Write((byte)Unknown14);
            binwriter.Write((byte)Tendency);
            binwriter.Write((byte)Towel);
            binwriter.Write((byte)Unknown15);
            binwriter.Write((byte)Visor);
            binwriter.Write((byte)Unknown16);
            binwriter.Write((byte)Unknown17);

            binwriter.Write((byte)Unknown18);
            binwriter.Write((byte)Celebration);
            binwriter.Write(Comment);
            binwriter.Write((byte)Unknown19);
            binwriter.Write((byte)Unknown20);
            binwriter.Write((byte)Unknown21);
            binwriter.Write((byte)Unknown22);
            binwriter.Write((byte)Unknown23);
            binwriter.Write((byte)Unknown24);

            binwriter.Write((byte)Unknown25);
            binwriter.Write((byte)Unknown26);
            binwriter.Write((byte)Unknown27);
            binwriter.Write((byte)Unknown28);
            binwriter.Write((byte)Unknown29);
            binwriter.Write((byte)Unknown30);
            binwriter.Write((byte)Unknown31);
            binwriter.Write((byte)Unknown32);
            binwriter.Write((byte)Unknown33);
            binwriter.Write((byte)Unknown34);

            binwriter.Write((byte)Unknown35);
            binwriter.Write((byte)Unknown36);
            binwriter.Write((byte)Unknown37);
            binwriter.Write((byte)Unknown38);
            binwriter.Write((byte)Unknown39);
            binwriter.Write((byte)Unknown40);
            binwriter.Write((byte)Unknown41);
            binwriter.Write((byte)Unknown42);
            binwriter.Write((byte)Unknown43);
            binwriter.Write((byte)Unknown44);

            binwriter.Write((byte)Unknown45);
            binwriter.Write((byte)Unknown46);
            binwriter.Write((byte)Unknown47);
            binwriter.Write((byte)Unknown48);
            binwriter.Write((byte)Unknown49);
            binwriter.Write((byte)Unknown50);
            binwriter.Write((byte)Unknown51);
            binwriter.Write((byte)Unknown52);
            binwriter.Write((byte)Unknown53);
            binwriter.Write((byte)Unknown54);

            binwriter.Write((byte)Unknown55);
            binwriter.Write((byte)Unknown56);
            binwriter.Write((byte)Unknown57);
            binwriter.Write((byte)Unknown58);
            binwriter.Write((byte)Unknown59);
            binwriter.Write((byte)Unknown60);
            binwriter.Write((byte)Unknown61);
            binwriter.Write((byte)Unknown62);                        
        }

        public string GetCSVAttribute(string field, EditorModel emodel, bool desc)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            if (field == "PFNA")
                return NameFirst;
            else if (field == "PLNA")
                return NameLast;
            else if (field == "PHSN")
            {
                if (emodel.PlayerModel.HomeStates.Count > HomeState)
                    return emodel.PlayerModel.HomeStates[HomeState];
                else return "NA";
            }
            else if (field == "PHTN")
                return HomeTown;
            else if (field == "PCOL")
            {
                if (desc)
                {
                    college_entry ce = emodel.Colleges[College];
                    return ce.name;
                }
                else return College.ToString();
            }
            else if (field == "PLBD")
            {
                Birthday = GetBirthday(PLBD);
                return Birthday.ToString();
                //return PLBD.ToString();
            }
            else if (field == "PAGE")
                return Age.ToString();
            else if (field == "PHGT")
                return Height.ToString();
            else if (field == "PWGT")
                return Weight.ToString();
            else if (field == "PPOS")
            {
                if (desc)
                    return emodel.PlayerModel.PlayerPositions[Position];
                else return Position.ToString();
            }
            else if (field == "PLTY")
            {
                if (desc)
                    return emodel.PlayerModel.GetArchetype(Archetype);
                else return Archetype.ToString();
            }
            else if (field == "PJEN")
                return JerseyNum.ToString();

            else if (field == "PDPI")
                return DraftPick.ToString();
            else if (field == "UDFA")
                return Undrafted.ToString();
            else if (field == "PDRO")
                return DraftRound.ToString();
            else if (field == "POVR")
                return Overall.ToString();
            else if (field == "PACC")
                return Accel.ToString();
            else if (field == "PAGI")
                return Agility.ToString();
            else if (field == "PAWR")
                return Awareness.ToString();
            else if (field == "PBCV")
                return BCVision.ToString();
            else if (field == "PBSG")
                return BlockShed.ToString();
            else if (field == "PBSK")
                return BreakSack.ToString();

            else if (field == "PBKT")
                return BreakTackle.ToString();
            else if (field == "PCAR")
                return Carry.ToString();
            else if (field == "PCTH")
                return Catching.ToString();
            else if (field == "PLCI")
                return CatchTraffic.ToString();
            else if (field == "PELU")
                return Elusive.ToString();
            else if (field == "PFMS")
                return MovesFinesse.ToString();
            else if (field == "PLHT")
                return HitPower.ToString();
            else if (field == "PLIB")
                return ImpactBlock.ToString();
            else if (field == "PINJ")
                return Injury.ToString();
            else if (field == "PLJM")
                return JukeMove.ToString();

            else if (field == "PJMP")
                return Jumping.ToString();
            else if (field == "PKAC")
                return KickAccuracy.ToString();
            else if (field == "PKPR")
                return KickPower.ToString();
            else if (field == "PKRT")
                return KickReturn.ToString();
            else if (field == "PLBK")
                return LeadBlock.ToString();
            else if (field == "PLMC")
                return CoverMan.ToString();
            else if (field == "PPBF")
                return PassBlockFin.ToString();
            else if (field == "PPBS")
                return PassBlockPow.ToString();
            else if (field == "PPBK")
                return PassBlock.ToString();
            else if (field == "PPER")
                return Personality.ToString();

            else if (field == "PPLA")
                return PlayAction.ToString();
            else if (field == "PLPR")
                return PlayRecog.ToString();
            else if (field == "PLPm")
                return MovesPower.ToString();
            else if (field == "PLPE")
                return CoverPress.ToString();
            else if (field == "PLPU")
                return Pursuit.ToString();
            else if (field == "PLRL")
                return Release.ToString();
            else if (field == "PDRR")
                return RouteDeep.ToString();
            else if (field == "PMRR")
                return RouteMed.ToString();
            else if (field == "SRRN")
                return RouteShort.ToString();
            else if (field == "PRBF")
                return RunBlockFin.ToString();

            else if (field == "PRBS")
                return RunBlockPow.ToString();
            else if (field == "PRBK")
                return RunBlock.ToString();
            else if (field == "U01")
                return Unknown01.ToString();
            else if (field == "PLSC")
                return SpecCatch.ToString();
            else if (field == "PSPD")
                return Speed.ToString();
            else if (field == "PLSM")
                return SpinMove.ToString();
            else if (field == "PSTA")
                return Stamina.ToString();
            else if (field == "PLSA")
                return StiffArm.ToString();
            else if (field == "PSTR")
                return Strength.ToString();
            else if (field == "PTAK")
                return Tackling.ToString();

            else if (field == "PTAD")
                return AccuracyDeep.ToString();
            else if (field == "PTAM")
                return AccuracyMed.ToString();
            else if (field == "PTHA")
                return ThrowAccuracy.ToString();
            else if (field == "PTAS")
                return AccuracyShort.ToString();
            else if (field == "PTOR")
                return ThrowOnRun.ToString();
            else if (field == "PTHP")
                return ThrowPower.ToString();
            else if (field == "PTUP")
                return ThrowPressure.ToString();
            else if (field == "PTGH")
                return Toughness.ToString();
            else if (field == "PLTR")
                return Trucking.ToString();
            else if (field == "PLZC")
                return CoverZone.ToString();

            else if (field == "CONF")
                return Confidence.ToString();
            else if (field == "TRBH")
                return BigHitter.ToString();
            else if (field == "TRCT")
                return PossCatch.ToString();
            else if (field == "TRCL")
                return Clutch.ToString();
            else if (field == "TRCB")
                return CoversBall.ToString();
            else if (field == "TRBR")
                return DLBull.ToString();
            else if (field == "TRDS")
                return DLSpin.ToString();
            else if (field == "TRSW")
                return DLSwim.ToString();
            else if (field == "TRDO")
                return DropsOpen.ToString();

            else if (field == "TRSC")
                return SideLineCatch.ToString();
            else if (field == "TRFY")
                return FightYards.ToString();
            else if (field == "TRFP")
                return ForcePass.ToString();
            else if (field == "TRHM")
                return HighMotor.ToString();
            else if (field == "TRJR")
                return AggCatch.ToString();
            else if (field == "TRIC")
                return Penalty.ToString();
            else if (field == "TRPB")
                return PlaysBall.ToString();
            else if (field == "TRFK")
                return Pumpfake.ToString();
            else if (field == "LBST")
                return LBStyle.ToString();
            else if (field == "TRSP")
                return SensePressure.ToString();

            else if (field == "U02")
                return Unknown02.ToString();
            else if (field == "TRSB")
                return StripsBall.ToString();
            else if (field == "TRTL")
                return TackleLow.ToString();
            else if (field == "TRTA")
                return ThrowAway.ToString();
            else if (field == "TRTS")
                return ThrowSpiral.ToString();
            else if (field == "QTEN")
                return QBTend.ToString();
            else if (field == "TRWU")
                return RAC.ToString();
            else if (field == "PROL")
                return Development.ToString();
            else if (field == "Pred")
                return Predictable.ToString();
            else if (field == "PLBP")
                return BackPlate.ToString();

            else if (field == "BSBA")
                return RearSize.ToString("G", culture);
            else if (field == "BSBT")
                return RearDef.ToString("G", culture);
            else if (field == "BSCT")
                return CalfSize.ToString("G", culture);
            else if (field == "BSCA")
                return CalfDef.ToString("G", culture);
            else if (field == "BSST")
                return ChestSize.ToString("G", culture);
            else if (field == "BSSA")
                return ChestDef.ToString("G", culture);
            else if (field == "BSWT")
                return WaistSize.ToString("G", culture);
            else if (field == "BSWA")
                return WaistDef.ToString("G", culture);
            else if (field == "BSGT")
                return GutSize.ToString("G", culture);
            else if (field == "BSGA")
                return GutDef.ToString("G", culture);
            else if (field == "BSPA")
                return ShoulderHeight.ToString("G", culture);
            else if (field == "BSPT")
                return PadSize.ToString("G", culture);
            else if (field == "BSTT")
                return ThighSize.ToString("G", culture);
            else if (field == "BSTA")
                return ThighDef.ToString("G", culture);
            else if (field == "BSAA")
                return ArmDef.ToString("G", culture);
            else if (field == "BSAT")
                return ArmSize.ToString("G", culture);
            else if (field == "BSFA")
                return FootDef.ToString("G", culture);
            else if (field == "BSFT")
                return FootSize.ToString("G", culture);

            else if (field == "U03")
                return Unknown03.ToString();
            else if (field == "PEYE")
                return EyePaint.ToString();
            else if (field == "PFMK")
                return Facemask.ToString();
            else if (field == "U04")
                return Unknown04.ToString();
            else if (field == "PFLA")
                return FlakJacket.ToString();
            else if (field == "U05")
                return Unknown05.ToString();
            else if (field == "PGHE")
                return FaceID.ToString();

            else if (field == "PLEL")
                return ElbowLeft.ToString();
            else if (field == "PLHA")
                return HandLeft.ToString();
            else if (field == "PLWR")
                return WristLeft.ToString();
            else if (field == "PREL")
                return ElbowRight.ToString();
            else if (field == "PRHA")
                return HandRight.ToString();
            else if (field == "PRWR")
                return WristRight.ToString();
            else if (field == "PGSL")
                return LeftSleeve.ToString();
            else if (field == "PMOR")
                return RightSleeve.ToString();
            else if (field == "PHCL")
                return HairColor.ToString();
            else if (field == "PLHS")
                return HairStyle.ToString();
            else if (field == "PHAN")
                return Handed.ToString();

            else if (field == "PLHW")
                return HandWarmer.ToString();
            else if (field == "PHLM")
                return Helmet.ToString();
            else if (field == "PJER")
                return JerseySleeve.ToString();
            else if (field == "U06")
                return Unknown06.ToString();
            else if (field == "U07")
                return Unknown07.ToString();
            else if (field == "U08")
                return Unknown08.ToString();
            else if (field == "U09")
                return Unknown09.ToString();
            else if (field == "PNEK")
                return NeckRoll.ToString();
            else if (field == "PNEC")
                return NeckType.ToString();
            else if (field == "U10")
                return Unknown10.ToString();

            else if (field == "PSXP")
                return PortraitID.ToString();
            else if (field == "PQBS")
                return QBStyle.ToString();
            else if (field == "PLKN")
                return LeftKnee.ToString();
            else if (field == "PLSH")
                return LeftShoe.ToString();
            else if (field == "PSPL")
                return LeftSpat.ToString();
            else if (field == "PLTH")
                return LeftThigh.ToString();
            else if (field == "U11")
                return Unknown11.ToString();
            else if (field == "PSHG")
                return HeadGear.ToString();
            else if (field == "U12")
                return Unknown12.ToString();

            else if (field == "PSKH")
                return SockHeight.ToString();
            else if (field == "U13")
                return Unknown13.ToString();
            else if (field == "PSTN")
                return QBStance.ToString();
            else if (field == "U14")
                return Unknown14.ToString();
            else if (field == "PTEN")
                return Tendency.ToString();
            else if (field == "PLTL")
                return Towel.ToString();
            else if (field == "U15")
                return Unknown15.ToString();
            else if (field == "PVIS")
                return Visor.ToString();
            else if (field == "U16")
                return Unknown16.ToString();
            else if (field == "U17")
                return Unknown17.ToString();

            else if (field == "U18")
                return Unknown18.ToString();
            else if (field == "PCEL")
                return Celebration.ToString();
            else if (field == "PCMT")
                return Comment.ToString();
            else if (field == "U19")
                return Unknown19.ToString();
            else if (field == "U20")
                return Unknown20.ToString();
            else if (field == "U21")
                return Unknown21.ToString();
            else if (field == "U22")
                return Unknown22.ToString();
            else if (field == "U23")
                return Unknown23.ToString();
            else if (field == "U24")
                return Unknown24.ToString();

            else if (field == "U25")
                return Unknown25.ToString();
            else if (field == "U26")
                return Unknown26.ToString();
            else if (field == "U27")
                return Unknown27.ToString();
            else if (field == "U28")
                return Unknown28.ToString();
            else if (field == "U29")
                return Unknown29.ToString();
            else if (field == "U30")
                return Unknown30.ToString();
            else if (field == "U31")
                return Unknown31.ToString();
            else if (field == "U32")
                return Unknown32.ToString();
            else if (field == "U33")
                return Unknown33.ToString();
            else if (field == "U34")
                return Unknown34.ToString();

            else if (field == "U35")
                return Unknown35.ToString();
            else if (field == "U36")
                return Unknown36.ToString();
            else if (field == "U37")
                return Unknown37.ToString();
            else if (field == "U38")
                return Unknown38.ToString();
            else if (field == "U39")
                return Unknown39.ToString();
            else if (field == "U40")
                return Unknown40.ToString();
            else if (field == "U41")
                return Unknown41.ToString();
            else if (field == "U42")
                return Unknown42.ToString();
            else if (field == "U43")
                return Unknown43.ToString();
            else if (field == "U44")
                return Unknown44.ToString();

            else if (field == "U45")
                return Unknown45.ToString();
            else if (field == "U46")
                return Unknown46.ToString();
            else if (field == "U47")
                return Unknown47.ToString();
            else if (field == "U48")
                return Unknown48.ToString();
            else if (field == "U49")
                return Unknown49.ToString();
            else if (field == "U50")
                return Unknown50.ToString();
            else if (field == "U51")
                return Unknown51.ToString();
            else if (field == "U52")
                return Unknown52.ToString();
            else if (field == "U53")
                return Unknown53.ToString();
            else if (field == "U54")
                return Unknown54.ToString();

            else if (field == "U55")
                return Unknown55.ToString();
            else if (field == "U56")
                return Unknown56.ToString();
            else if (field == "U57")
                return Unknown57.ToString();
            else if (field == "U58")
                return Unknown58.ToString();
            else if (field == "U59")
                return Unknown59.ToString();
            else if (field == "U60")
                return Unknown60.ToString();
            else if (field == "U61")
                return Unknown61.ToString();
            else if (field == "U62")
                return Unknown62.ToString();
            // Madden 2020
            else if (field == "U63")
                return Unknown63.ToString();
            else if (field == "U64")
                return Unknown64.ToString();
            else if (field == "U64")
                return Unknown64.ToString();
            else if (field == "U65")
                return Unknown65.ToString();

            else if (field == "TRDB")
                return DeepBall.ToString();

            else
            {
                return "";
            }
        }

        public void SetAttribute(string field, string val, EditorModel emodel)
        {
            if (field == "PFNA")
                NameFirst = val;
            else if (field == "PLNA")
                NameLast = val;
            else if (field == "PHTN")
                HomeTown = val;
            else if (field == "PHSN")
            {
                try
                {
                    HomeState = Convert.ToInt32(val);
                }
                catch (FormatException)
                {
                    for (int c = 0; c < emodel.PlayerModel.HomeStates.Count; c++)
                    {
                        if (val == emodel.PlayerModel.HomeStates[c])
                        {
                            HomeState = c;
                            break;
                        }
                    }
                }
            }
            else if (field == "PCOL")
            {
                try
                {
                    College = Convert.ToInt32(val);
                }
                catch (FormatException)
                {
                    foreach (KeyValuePair<int, college_entry> ce in emodel.Colleges)
                    {
                        if (val == ce.Value.name)
                        {
                            College = ce.Key;
                            break;
                        }
                    }
                }
            }
            else if (field == "PLBD")
            {
                try
                {
                    PLBD = Convert.ToInt32(val);
                }
                catch (FormatException)
                {
                    PLBD = SetBirthday(val);
                }
            }

            else if (field == "PAGE")
                Age = Convert.ToInt32(val);
            else if (field == "PHGT")
                Height = Convert.ToInt32(val);
            else if (field == "PWGT")
                Weight = Convert.ToInt32(val);
            else if (field == "PPOS")
            {
                try
                {
                    Position = Convert.ToInt32(val);
                }
                catch (FormatException)
                {
                    foreach (KeyValuePair<int, string> pos in emodel.PlayerModel.PlayerPositions)
                    {
                        if (val == pos.Value)
                        {
                            Position = pos.Key;
                            break;
                        }
                    }
                }

            }
            else if (field == "PLTY")
            {
                try
                {
                    Archetype = Convert.ToInt32(val);
                }
                catch (FormatException)
                {
                    Archetype = emodel.PlayerModel.GetArchetype(val);
                }
            }
            else if (field == "PJEN")
                JerseyNum = Convert.ToInt32(val);

            else if (field == "PDPI")                   //70
                DraftPick = Convert.ToInt32(val);
            else if (field == "UDFA")
                Undrafted = Convert.ToInt32(val);
            else if (field == "PDRO")
                DraftRound = Convert.ToInt32(val);
            else if (field == "POVR")
                Overall = Convert.ToInt32(val);
            else if (field == "PACC")
                Accel = Convert.ToInt32(val);
            else if (field == "PAGI")
                Agility = Convert.ToInt32(val);
            else if (field == "PAWR")
                Awareness = Convert.ToInt32(val);
            else if (field == "PBCV")
                BCVision = Convert.ToInt32(val);
            else if (field == "PBSG")
                BlockShed = Convert.ToInt32(val);
            else if (field == "PBSK")
                BreakSack = Convert.ToInt32(val);

            else if (field == "PBKT")                   //80
                BreakTackle = Convert.ToInt32(val);
            else if (field == "PCAR")
                Carry = Convert.ToInt32(val);
            else if (field == "PCTH")
                Catching = Convert.ToInt32(val);
            else if (field == "PLCI")
                CatchTraffic = Convert.ToInt32(val);
            else if (field == "PELU")
                Elusive = Convert.ToInt32(val);
            else if (field == "PFMS")
                MovesFinesse = Convert.ToInt32(val);
            else if (field == "PLHT")
                HitPower = Convert.ToInt32(val);
            else if (field == "PLIB")
                ImpactBlock = Convert.ToInt32(val);
            else if (field == "PINJ")
                Injury = Convert.ToInt32(val);
            else if (field == "PLJM")
                JukeMove = Convert.ToInt32(val);

            else if (field == "PJMP")                   //90
                Jumping = Convert.ToInt32(val);
            else if (field == "PKAC")
                KickAccuracy = Convert.ToInt32(val);
            else if (field == "PKPR")
                KickPower = Convert.ToInt32(val);
            else if (field == "PKRT")
                KickReturn = Convert.ToInt32(val);
            else if (field == "PLBK")
                LeadBlock = Convert.ToInt32(val);
            else if (field == "PLMC")
                CoverMan = Convert.ToInt32(val);
            else if (field == "PPBF")
                PassBlockFin = Convert.ToInt32(val);
            else if (field == "PPBS")
                PassBlockPow = Convert.ToInt32(val);
            else if (field == "PPBK")
                PassBlock = Convert.ToInt32(val);
            else if (field == "PPER")
                Personality = Convert.ToInt32(val);

            else if (field == "PPLA")                       //100
                PlayAction = Convert.ToInt32(val);
            else if (field == "PLPR")
                PlayRecog = Convert.ToInt32(val);
            else if (field == "PLPm")
                MovesPower = Convert.ToInt32(val);
            else if (field == "PLPE")
                CoverPress = Convert.ToInt32(val);
            else if (field == "PLPU")
                Pursuit = Convert.ToInt32(val);
            else if (field == "PLRL")
                Release = Convert.ToInt32(val);
            else if (field == "PDRR")
                RouteDeep = Convert.ToInt32(val);
            else if (field == "PMRR")
                RouteMed = Convert.ToInt32(val);
            else if (field == "SRRN")
                RouteShort = Convert.ToInt32(val);
            else if (field == "PRBF")
                RunBlockFin = Convert.ToInt32(val);

            else if (field == "PRBS")                   //110
                RunBlockPow = Convert.ToInt32(val);
            else if (field == "PRBK")
                RunBlock = Convert.ToInt32(val);
            else if (field == "U01")
                Unknown01 = Convert.ToInt32(val);
            else if (field == "PLSC")
                SpecCatch = Convert.ToInt32(val);
            else if (field == "PSPD")
                Speed = Convert.ToInt32(val);
            else if (field == "PLSM")
                SpinMove = Convert.ToInt32(val);
            else if (field == "PSTA")
                Stamina = Convert.ToInt32(val);
            else if (field == "PLSA")
                StiffArm = Convert.ToInt32(val);
            else if (field == "PSTR")
                Strength = Convert.ToInt32(val);
            else if (field == "PTAK")
                Tackling = Convert.ToInt32(val);

            else if (field == "PTAD")                   //120
                AccuracyDeep = Convert.ToInt32(val);
            else if (field == "PTAM")
                AccuracyMed = Convert.ToInt32(val);
            else if (field == "PTHA")
                ThrowAccuracy = Convert.ToInt32(val);
            else if (field == "PTAS")
                AccuracyShort = Convert.ToInt32(val);
            else if (field == "PTOR")
                ThrowOnRun = Convert.ToInt32(val);
            else if (field == "PTHP")
                ThrowPower = Convert.ToInt32(val);
            else if (field == "PTUP")
                ThrowPressure = Convert.ToInt32(val);
            else if (field == "PTGH")
                Toughness = Convert.ToInt32(val);
            else if (field == "PLTR")
                Trucking = Convert.ToInt32(val);
            else if (field == "PLZC")
                CoverZone = Convert.ToInt32(val);

            else if (field == "CONF")                   //130
                Confidence = Convert.ToInt32(val);
            else if (field == "TRBH")
                BigHitter = Convert.ToInt32(val);
            else if (field == "TRCT")
                PossCatch = Convert.ToInt32(val);
            else if (field == "TRCL")
                Clutch = Convert.ToInt32(val);
            else if (field == "TRCB")
                CoversBall = Convert.ToInt32(val);
            else if (field == "TRBR")
                DLBull = Convert.ToInt32(val);
            else if (field == "TRDS")
                DLSpin = Convert.ToInt32(val);
            else if (field == "TRSW")
                DLSwim = Convert.ToInt32(val);
            else if (field == "TRDO")
                DropsOpen = Convert.ToInt32(val);

            else if (field == "TRSC")                   //140
                SideLineCatch = Convert.ToInt32(val);
            else if (field == "TRFY")
                FightYards = Convert.ToInt32(val);
            else if (field == "TRFP")
                ForcePass = Convert.ToInt32(val);
            else if (field == "TRHM")
                HighMotor = Convert.ToInt32(val);
            else if (field == "TRJR")
                AggCatch = Convert.ToInt32(val);
            else if (field == "TRIC")
                Penalty = Convert.ToInt32(val);
            else if (field == "TRPB")
                PlaysBall = Convert.ToInt32(val);
            else if (field == "TRFK")
                Pumpfake = Convert.ToInt32(val);
            else if (field == "LBST")
                LBStyle = Convert.ToInt32(val);
            else if (field == "TRSP")
                SensePressure = Convert.ToInt32(val);

            else if (field == "U02")                   //150
                Unknown02 = Convert.ToInt32(val);
            else if (field == "TRSB")
                StripsBall = Convert.ToInt32(val);
            else if (field == "TRTL")
                TackleLow = Convert.ToInt32(val);
            else if (field == "TRTA")
                ThrowAway = Convert.ToInt32(val);
            else if (field == "TRTS")
                ThrowSpiral = Convert.ToInt32(val);
            else if (field == "QTEN")
                QBTend = Convert.ToInt32(val);
            else if (field == "TRWU")
                RAC = Convert.ToInt32(val);
            else if (field == "PROL")
                Development = Convert.ToInt32(val);
            else if (field == "Pred")
                Predictable = Convert.ToInt32(val);
            else if (field == "PLBP")
                BackPlate = Convert.ToInt32(val);

            else if (field == "BSBT")               //160
                RearSize = Convert.ToSingle(val);
            else if (field == "BSBA")
                RearDef = Convert.ToSingle(val);
            else if (field == "BSCT")
                CalfSize = Convert.ToSingle(val);
            else if (field == "BSCA")
                CalfDef = Convert.ToSingle(val);
            else if (field == "BSST")
                ChestSize = Convert.ToSingle(val);
            else if (field == "BSSA")
                ChestDef = Convert.ToSingle(val);
            else if (field == "BSWT")
                WaistSize = Convert.ToSingle(val);
            else if (field == "BSWA")
                WaistDef = Convert.ToSingle(val);
            else if (field == "BSGT")
                GutSize = Convert.ToSingle(val);
            else if (field == "BSGA")
                GutDef = Convert.ToSingle(val);
            else if (field == "BSPA")
                ShoulderHeight = Convert.ToSingle(val);
            else if (field == "BSPT")
                PadSize = Convert.ToSingle(val);
            else if (field == "BSTT")
                ThighSize = Convert.ToSingle(val);
            else if (field == "BSTA")
                ThighDef = Convert.ToSingle(val);
            else if (field == "BSAA")
                ArmDef = Convert.ToSingle(val);
            else if (field == "BSAT")
                ArmSize = Convert.ToSingle(val);
            else if (field == "BSFA")
                FootDef = Convert.ToSingle(val);
            else if (field == "BSFT")
                FootSize = Convert.ToSingle(val);

            else if (field == "U03")                   //232
                Unknown03 = Convert.ToInt32(val);
            else if (field == "PEYE")
                EyePaint = Convert.ToInt32(val);
            else if (field == "PFMK")
                Facemask = Convert.ToInt32(val);
            else if (field == "U04")
                Unknown04 = Convert.ToInt32(val);
            else if (field == "PFLA")
                FlakJacket = Convert.ToInt32(val);
            else if (field == "U05")
                Unknown05 = Convert.ToInt32(val);
            else if (field == "PGHE")
                FaceID = Convert.ToUInt16(val);

            else if (field == "PLEL")                   //240
                ElbowLeft = Convert.ToInt32(val);
            else if (field == "PLHA")
                HandLeft = Convert.ToInt32(val);
            else if (field == "PLWR")
                WristLeft = Convert.ToInt32(val);
            else if (field == "PREL")
                ElbowRight = Convert.ToInt32(val);
            else if (field == "PRHA")
                HandRight = Convert.ToInt32(val);
            else if (field == "PRWR")
                WristRight = Convert.ToInt32(val);
            else if (field == "PGSL")
                LeftSleeve = Convert.ToInt32(val);
            else if (field == "U247")
                RightSleeve = Convert.ToInt32(val);
            else if (field == "PHCL")
                HairColor = Convert.ToInt32(val);
            else if (field == "PLHS")
                HairStyle = Convert.ToInt32(val);
            else if (field == "PHAN")
                Handed = Convert.ToInt32(val);

            else if (field == "PLHW")                   //250
                HandWarmer = Convert.ToInt32(val);
            else if (field == "PHLM")
                Helmet = Convert.ToInt32(val);
            else if (field == "PJER")
                JerseySleeve = Convert.ToInt32(val);
            else if (field == "U06")
                Unknown06 = Convert.ToInt32(val);
            else if (field == "U07")
                Unknown07 = Convert.ToInt32(val);
            else if (field == "U08")
                Unknown08 = Convert.ToInt32(val);
            else if (field == "U09")
                Unknown09 = Convert.ToInt32(val);
            else if (field == "PNEK")
                NeckRoll = Convert.ToInt32(val);
            else if (field == "PNEC")
                NeckType = Convert.ToInt32(val);
            else if (field == "U10")
                Unknown10 = Convert.ToInt32(val);

            else if (field == "PSXP") //260
                PortraitID = Convert.ToUInt16(val);
            else if (field == "PQBS")
                QBStyle = Convert.ToInt32(val);
            else if (field == "PLKN")
                LeftKnee = Convert.ToInt32(val);
            else if (field == "PLSH")
                LeftShoe = Convert.ToInt32(val);
            else if (field == "PSPL")
                LeftSpat = Convert.ToInt32(val);
            else if (field == "PLTH")
                LeftThigh = Convert.ToInt32(val);
            else if (field == "U11")
                Unknown11 = Convert.ToInt32(val);
            else if (field == "PSHG")
                HeadGear = Convert.ToInt32(val);
            else if (field == "U12")
                Unknown12 = Convert.ToInt32(val);

            else if (field == "PSKH")
                SockHeight = Convert.ToInt32(val);
            else if (field == "U13")
                Unknown13 = Convert.ToInt32(val);
            else if (field == "PSTN")
                QBStance = Convert.ToInt32(val);
            else if (field == "U14")
                Unknown14 = Convert.ToInt32(val);
            else if (field == "PTEN")
                Tendency = Convert.ToInt32(val);
            else if (field == "PLTL")
                Towel = Convert.ToInt32(val);
            else if (field == "U15")
                Unknown15 = Convert.ToInt32(val);
            else if (field == "PVIS")
                Visor = Convert.ToInt32(val);
            else if (field == "U16")
                Unknown16 = Convert.ToInt32(val);
            else if (field == "U17")
                Unknown17 = Convert.ToInt32(val);

            else if (field == "U18")
                Unknown18 = Convert.ToInt32(val);
            else if (field == "PCEL")
                Celebration = Convert.ToInt32(val);
            else if (field == "PCMT")
            {
                Comment = Convert.ToUInt16(val);
            }

            else if (field == "U19")
                Unknown19 = Convert.ToInt32(val);
            else if (field == "U20")
                Unknown20 = Convert.ToInt32(val);
            else if (field == "U21")
                Unknown21 = Convert.ToInt32(val);
            else if (field == "U22")
                Unknown22 = Convert.ToInt32(val);
            else if (field == "U23")
                Unknown23 = Convert.ToInt32(val);
            else if (field == "U24")
                Unknown24 = Convert.ToInt32(val);

            else if (field == "U25")
                Unknown25 = Convert.ToInt32(val);
            else if (field == "U26")
                Unknown26 = Convert.ToInt32(val);
            else if (field == "U27")
                Unknown27 = Convert.ToInt32(val);
            else if (field == "U28")
                Unknown28 = Convert.ToInt32(val);
            else if (field == "U29")
                Unknown29 = Convert.ToInt32(val);
            else if (field == "U30")
                Unknown30 = Convert.ToInt32(val);
            else if (field == "U31")
                Unknown31 = Convert.ToInt32(val);
            else if (field == "U32")
                Unknown32 = Convert.ToInt32(val);
            else if (field == "U33")
                Unknown33 = Convert.ToInt32(val);
            else if (field == "U34")
                Unknown34 = Convert.ToInt32(val);

            else if (field == "U35")
                Unknown35 = Convert.ToInt32(val);
            else if (field == "U36")
                Unknown36 = Convert.ToInt32(val);
            else if (field == "U37")
                Unknown37 = Convert.ToInt32(val);
            else if (field == "U38")
                Unknown38 = Convert.ToInt32(val);
            else if (field == "U39")
                Unknown39 = Convert.ToInt32(val);
            else if (field == "U40")
                Unknown40 = Convert.ToInt32(val);
            else if (field == "U41")
                Unknown41 = Convert.ToInt32(val);
            else if (field == "U42")
                Unknown42 = Convert.ToInt32(val);
            else if (field == "U43")
                Unknown43 = Convert.ToInt32(val);
            else if (field == "U44")
                Unknown44 = Convert.ToInt32(val);

            else if (field == "U45")
                Unknown45 = Convert.ToInt32(val);
            else if (field == "U46")
                Unknown46 = Convert.ToInt32(val);
            else if (field == "U47")
                Unknown47 = Convert.ToInt32(val);
            else if (field == "U48")
                Unknown48 = Convert.ToInt32(val);
            else if (field == "U49")
                Unknown49 = Convert.ToInt32(val);
            else if (field == "U50")
                Unknown50 = Convert.ToInt32(val);
            else if (field == "U51")
                Unknown51 = Convert.ToInt32(val);
            else if (field == "U52")
                Unknown52 = Convert.ToInt32(val);
            else if (field == "U53")
                Unknown53 = Convert.ToInt32(val);
            else if (field == "U54")
                Unknown54 = Convert.ToInt32(val);

            else if (field == "U55")
                Unknown55 = Convert.ToInt32(val);
            else if (field == "U56")
                Unknown56 = Convert.ToInt32(val);
            else if (field == "U57")
                Unknown57 = Convert.ToInt32(val);
            else if (field == "U58")
                Unknown58 = Convert.ToInt32(val);
            else if (field == "U59")
                Unknown59 = Convert.ToInt32(val);
            else if (field == "U60")
                Unknown60 = Convert.ToInt32(val);
            else if (field == "U61")
                Unknown61 = Convert.ToInt32(val);
            else if (field == "U62")
                Unknown62 = Convert.ToInt32(val);
            else if (field == "U63")
                Unknown63 = Convert.ToInt32(val);
            else if (field == "U64")
                Unknown64 = Convert.ToInt32(val);
            else if (field == "U65")
                Unknown65 = Convert.ToInt32(val);

            else if (field == "TRDB")
                DeepBall = Convert.ToInt32(val);
            else if (field == "PMOR")
                RightSleeve = Convert.ToInt32(val);
            else
            {
                bool stop = true;
            }

        }
                
        public StringBuilder ExportDraftClassPlayerCSV(Dictionary<string,string> RatingDefs, EditorModel emodel, bool desc)
        {
            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string, string> field in RatingDefs)
            {
                builder.Append(GetCSVAttribute(field.Key, emodel, desc));
                builder.Append(",");
            }

            return builder;
        }

        public void ImportCSVPlayer(string[] fields, string[] record, EditorModel emodel )
        {
            for (int f = 0; f < fields.Count(); f++)
            {
                if (fields[f] == "")
                    continue;
                try
                {
                    SetAttribute(fields[f], record[f], emodel);
                }
                catch
                {
                    bool stop = true;
                }
            }
        }    
    }
    
    public class DraftClass
    {
        public List<DraftPlayer> draftclassplayers = new List<DraftPlayer>();
        public Dictionary<int, string> playerfields = new Dictionary<int, string>();
        public List<string> records = new List<string>();
        public Dictionary<string, string> RatingDefs = new Dictionary<string, string>();
        public MaddenFileVersion DraftClassVersion;
        private EditorModel model;
        private FB _Frostbyte;


        public FB fb
        {
            get { return _Frostbyte; }
            set { _Frostbyte = value; }
        }

        public DraftClass(EditorModel Model)
        {
            model = Model;
            fb = new FB();
        }

        public void InitRatingDefs(MaddenFileVersion version)
        {
            RatingDefs.Add("PFNA", "First Name");
            RatingDefs.Add("PLNA", "Last Name");
            RatingDefs.Add("PHTN", "HomeTown");                        
            // Placeholder
            RatingDefs.Add("PHSN", "HomeState");
            RatingDefs.Add("PCOL", "College");
            RatingDefs.Add("PLBD", "Birthday");;
            RatingDefs.Add("PAGE", "Age");
            RatingDefs.Add("PHGT", "Height");
            RatingDefs.Add("PWGT", "Weight");
            RatingDefs.Add("PPOS", "Position");
            RatingDefs.Add("PLTY", "Archetype");
            RatingDefs.Add("PJEN", "Jersey Num");

            RatingDefs.Add("PDPI", "Draft Pick");
            RatingDefs.Add("UDFA", "Undrafted");
            RatingDefs.Add("PDRO", "Draft Round");
            RatingDefs.Add("POVR", "Overall");
            RatingDefs.Add("PACC", "Acceleration");
            RatingDefs.Add("PAGI", "Agility");
            RatingDefs.Add("PAWR", "Awareness");
            RatingDefs.Add("PBCV", "Ball Carrier Vision");
            RatingDefs.Add("PBSG", "Block Shed");
            RatingDefs.Add("PBSK", "Break Sack");

            RatingDefs.Add("PBKT", "Break Tackle");
            RatingDefs.Add("PCAR", "Carry");
            RatingDefs.Add("PCTH", "Catch");
            RatingDefs.Add("PLCI", "Catch in Traffic");
            RatingDefs.Add("PELU", "Elusive");
            RatingDefs.Add("PFMS", "Finesse Moves");
            RatingDefs.Add("PLHT", "Hit Power");
            RatingDefs.Add("PLIB", "Impact Block");
            RatingDefs.Add("PINJ", "Injury");
            RatingDefs.Add("PLJM", "Juke Move");

            RatingDefs.Add("PJMP", "Jump");
            RatingDefs.Add("PKAC", "Kick Accuracy");
            RatingDefs.Add("PKPR", "Kick Power");
            RatingDefs.Add("PKRT", "Kick Return");
            RatingDefs.Add("PLBK", "Lead Block");
            RatingDefs.Add("PLMC", "Cover Man");
            RatingDefs.Add("PPBF", "Pass Block Fin");
            RatingDefs.Add("PPBS", "Pass Block Str");
            RatingDefs.Add("PPBK", "Pass Block");
            RatingDefs.Add("PPER", "Personality");

            RatingDefs.Add("PPLA", "Play Action");
            RatingDefs.Add("PLPR", "Play Recognition");
            RatingDefs.Add("PLPm", "Moves");
            RatingDefs.Add("PLPE", "Cover Press");
            RatingDefs.Add("PLPU", "Pursuit");
            RatingDefs.Add("PLRL", "Release");
            RatingDefs.Add("PDRR", "Route Run Deep");
            RatingDefs.Add("PMRR", "Route Run Med");
            RatingDefs.Add("SRRN", "Route Run Short");
            RatingDefs.Add("PRBF", "Run Block Fin");

            RatingDefs.Add("PRBS", "Run Block Str");
            RatingDefs.Add("PRBK", "Run Block");
            RatingDefs.Add("PLSC", "Catch Spec");
            RatingDefs.Add("PSPD", "Speed");
            RatingDefs.Add("PLSM", "DL Spin Move");
            RatingDefs.Add("PSTA", "Stamina");
            RatingDefs.Add("PLSA", "Stiff Arm");
            RatingDefs.Add("PSTR", "Strength");
            RatingDefs.Add("PTAK", "Tackle");

            RatingDefs.Add("PTAD", "Throw Deep");
            RatingDefs.Add("PTAM", "Throw Med");
            RatingDefs.Add("PTHA", "Throw Accuracy");
            RatingDefs.Add("PTAS", "Throw Short");
            RatingDefs.Add("PTOR", "Throw on Run");
            RatingDefs.Add("PTHP", "Throw Power");
            RatingDefs.Add("PTUP", "Throw Pressure");
            RatingDefs.Add("PTGH", "Toughness");
            RatingDefs.Add("PLTR", "Trucking");
            RatingDefs.Add("PLZC", "Cover Zone");

            RatingDefs.Add("CONF", "Confidence");
            RatingDefs.Add("TRBH", "Big Hitter");
            RatingDefs.Add("TRCT", "Poss Catch");
            RatingDefs.Add("TRCL", "Clutch");
            RatingDefs.Add("TRCB", "Covers Ball");
            RatingDefs.Add("TRPB", "PlaysBall");
            RatingDefs.Add("TRBR", "Bull Rush");
            RatingDefs.Add("TRDS", "DL Spin Move");
            RatingDefs.Add("TRSW", "DL Swim Move");
            RatingDefs.Add("TRDO", "Drop Open Pass");

            RatingDefs.Add("TRSC", "Sideline Catch");
            RatingDefs.Add("TRFY", "Fight for Yards");
            RatingDefs.Add("TRFP", "Forces Pass");
            RatingDefs.Add("TRHM", "High Motor");
            RatingDefs.Add("TRJR", "Agg. Catch");
            // playertype
            RatingDefs.Add("TRIC", "Penalty");
            RatingDefs.Add("TRFK", "Pumpfake");
            RatingDefs.Add("LBST", "LB Style");
            RatingDefs.Add("TRSP", "Sense Pressure");

            
            RatingDefs.Add("TRSB", "Strips Ball");
            RatingDefs.Add("TRTL", "TackleLow");
            RatingDefs.Add("TRTA", "Throw Away");
            RatingDefs.Add("TRTS", "Throw Spiral");
            RatingDefs.Add("QTEN", "QB Tend.");
            RatingDefs.Add("TRWU", "Run After Catch");
            RatingDefs.Add("PROL", "Development");
            RatingDefs.Add("Pred", "Predictable");
            RatingDefs.Add("PLBP", "Back Plate");

            RatingDefs.Add("BSBT", "Rear Size");
            RatingDefs.Add("BSBA", "Rear Defn");
            RatingDefs.Add("BSCT", "Calf Size");
            RatingDefs.Add("BSCA", "Calf Defn");
            RatingDefs.Add("BSWT", "Waist Size");
            RatingDefs.Add("BSWA", "Waist Defn");
            RatingDefs.Add("BSGT", "Gut Size");
            RatingDefs.Add("BSGA", "Gut Defn");
            RatingDefs.Add("BSPA", "Pad Defn");
            RatingDefs.Add("BSPT", "Pad Size");
            RatingDefs.Add("BSTT", "Thigh Size");
            RatingDefs.Add("BSTA", "Thigh Defn");
            RatingDefs.Add("BSAA", "Arm Defn");
            RatingDefs.Add("BSAT", "Arm Size");
            RatingDefs.Add("BSSA", "Shoulder Def");
            RatingDefs.Add("BSST", "Shoulder Size");
            RatingDefs.Add("BSFA", "Foot Defn");
            RatingDefs.Add("BSFT", "Foot Size");

            
            RatingDefs.Add("PEYE", "Eye Paint");
            RatingDefs.Add("PFMK", "Face Mask");
            RatingDefs.Add("PFLA", "Flak Jacket");
            RatingDefs.Add("PGHE", "FaceID");

            RatingDefs.Add("PLEL", "Elbow Left");
            RatingDefs.Add("PLHA", "Hand Left");
            RatingDefs.Add("PLWR", "Wrist Left");
            RatingDefs.Add("PREL", "Elbow Right");
            RatingDefs.Add("PRHA", "Hand Right");
            RatingDefs.Add("PRWR", "Wrist Right");
            RatingDefs.Add("PGSL", "Sleeve Left");
            RatingDefs.Add("PMOR", "Sleeve Right");            
            RatingDefs.Add("PHCL", "HairColor");            
            RatingDefs.Add("PHAN", "Handed");

            RatingDefs.Add("PLHW", "HandWarmer");
            RatingDefs.Add("PHLM", "Helmet");
            RatingDefs.Add("PJER", "JerseySlv");
            RatingDefs.Add("PNEK", "Neck Roll");
            RatingDefs.Add("PNEC", "NeckType");

            RatingDefs.Add("PSXP", "PortID");
            RatingDefs.Add("PQBS", "QB Style");
            RatingDefs.Add("PLKN", "Knee Left");
            RatingDefs.Add("PLSH", "Shoe Left");
            RatingDefs.Add("PSPL", "Spat Left");
            RatingDefs.Add("PLTH", "Thigh Left");
            RatingDefs.Add("PSHG", "HeadGear");

            RatingDefs.Add("PSKH", "SockHeight");
            RatingDefs.Add("PSTN", "Stance");
            RatingDefs.Add("PTEN", "Tendency");
            RatingDefs.Add("PLTL", "Towel");
            RatingDefs.Add("PVIS", "Visor");

            
            RatingDefs.Add("PCEL", "Celebration");
            RatingDefs.Add("PCMT", "Comment");

            RatingDefs.Add("U01", "Unknown1");
            RatingDefs.Add("U02", "Unknown2");
            RatingDefs.Add("U03", "Unknown3");
            RatingDefs.Add("U04", "Unknown4");
            RatingDefs.Add("U05", "Unknown5");
            RatingDefs.Add("U06", "Unknown6");
            RatingDefs.Add("U07", "Unknown7");
            RatingDefs.Add("U08", "Unknown8");
            RatingDefs.Add("U09", "Unknown9");
            RatingDefs.Add("U10", "Unknown10");
            RatingDefs.Add("U11", "Unknown11");
            RatingDefs.Add("U12", "Unknown12");
            RatingDefs.Add("U13", "Unknown13");
            RatingDefs.Add("U14", "Unknown14");
            RatingDefs.Add("U15", "Unknown15");
            RatingDefs.Add("U16", "Unknown16");
            RatingDefs.Add("U17", "Unknown17");
            RatingDefs.Add("U18", "Unknown18");
            RatingDefs.Add("U19", "Unknown19");
            RatingDefs.Add("U20", "Unknown20");
            RatingDefs.Add("U21", "Unknown21");
            RatingDefs.Add("U22", "Unknown22");
            RatingDefs.Add("U23", "Unknown23");
            RatingDefs.Add("U24", "Unknown24");
            RatingDefs.Add("U25", "Unknown25");
            RatingDefs.Add("U26", "Unknown26");
            RatingDefs.Add("U27", "Unknown27");
            RatingDefs.Add("U28", "Unknown28");
            RatingDefs.Add("U29", "Unknown29");
            RatingDefs.Add("U30", "Unknown30");
            RatingDefs.Add("U31", "Unknown31");
            RatingDefs.Add("U32", "Unknown32");
            RatingDefs.Add("U33", "Unknown33");
            RatingDefs.Add("U34", "Unknown34");
            RatingDefs.Add("U35", "Unknown35");
            RatingDefs.Add("U36", "Unknown36");
            RatingDefs.Add("U37", "Unknown37");
            RatingDefs.Add("U38", "Unknown38");
            RatingDefs.Add("U39", "Unknown39");
            RatingDefs.Add("U40", "Unknown40");
            RatingDefs.Add("U41", "Unknown41");
            RatingDefs.Add("U42", "Unknown42");
            RatingDefs.Add("U43", "Unknown43");
            RatingDefs.Add("U44", "Unknown44");
            RatingDefs.Add("U45", "Unknown45");
            RatingDefs.Add("U46", "Unknown46");
            RatingDefs.Add("U47", "Unknown47");
            RatingDefs.Add("U48", "Unknown48");
            RatingDefs.Add("U49", "Unknown49");
            RatingDefs.Add("U50", "Unknown50");
            RatingDefs.Add("U51", "Unknown51");
            RatingDefs.Add("U52", "Unknown52");
            RatingDefs.Add("U53", "Unknown53");
            RatingDefs.Add("U54", "Unknown54");
            RatingDefs.Add("U55", "Unknown55");
            RatingDefs.Add("U56", "Unknown56");
            RatingDefs.Add("U57", "Unknown57");
            RatingDefs.Add("U58", "Unknown58");
            RatingDefs.Add("U59", "Unknown59");
            RatingDefs.Add("U60", "Unknown60");
            RatingDefs.Add("U61", "Unknown61");
            RatingDefs.Add("U62", "Unknown62");
            RatingDefs.Add("U63", "Unknown63");
            RatingDefs.Add("U64", "Unknown64");
            RatingDefs.Add("U65", "Unknown65");

            RatingDefs.Add("TRDB", "DeepBall");
        }

        public bool ReadDraftClass(string filename)
        {
            //fb = new FB();
            fb.Extract(filename);

            fb.binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            if (fb.binreader.BaseStream.Length == 152950)
            {
                DraftClassVersion = MaddenFileVersion.Ver2020;
            }
            else if (fb.binreader.BaseStream.Length == 149310)
            {
                DraftClassVersion = MaddenFileVersion.Ver2019;
            }
            else return false;

            fb.binreader.BaseStream.Position = 66;
            fb.DataEntries = fb.binreader.ReadUInt32();

            if (RatingDefs.Count ==0)
                InitRatingDefs(DraftClassVersion);
            draftclassplayers.Clear();

            for (int p = 0; p < fb.DataEntries; p++)
            {
                DraftPlayer player = new DraftPlayer();
                if (DraftClassVersion == MaddenFileVersion.Ver2019)
                    fb.binreader.BaseStream.Position = 70 + (p * 328);
                else if (DraftClassVersion == MaddenFileVersion.Ver2020)
                    fb.binreader.BaseStream.Position = 70 + (p * 336);

                if (DraftClassVersion == MaddenFileVersion.Ver2019)
                    player.ReadPlayer19(fb.binreader);
                else if (DraftClassVersion == MaddenFileVersion.Ver2020)
                    player.ReadPlayer20(fb.binreader);
                draftclassplayers.Add(player);
            }

            fb.binreader.Close();

            return true;
        }

        public void SaveDraftClass(string filename, FB draftfb, int ver)
        {
            MaddenFileVersion version = DraftClassVersion;
            if (ver == 0)
                version = MaddenFileVersion.Ver2019;               
            else if (ver == 1)
                version = MaddenFileVersion.Ver2020;            

            draftfb.ChangeDraftClassVersion(version);
            
            string draftname = Path.GetDirectoryName(filename);

            string name = Path.GetFileName(filename);
            draftname += @"\CAREERDRAFT-" + name;

            BinaryWriter binwriter = new BinaryWriter(File.Open(draftname, FileMode.Create));
            binwriter.Write(draftfb.FBCH);
            binwriter.Write(draftfb.UNKS);
            binwriter.Write(draftfb.FB_Version);
            binwriter.Write(draftfb.FB_InfoLength);
            binwriter.Write(draftfb.DataFileLength);
            binwriter.Write(draftfb.TotalLength);

            #region Date/Time
            DateTime date = DateTime.Today;

            binwriter.Write((UInt16)date.Year);
            binwriter.Write((UInt16)date.Month);
            binwriter.Write((UInt16)date.Day);
            binwriter.Write((UInt16)date.Hour);
            binwriter.Write((UInt16)date.Minute);
            binwriter.Write((UInt16)date.Second);
            #endregion

            binwriter.Write(draftfb.Serial);
            binwriter.Write((UInt32)2);
            binwriter.Write(draftfb.DataEntries);

            for (int p = 0; p < draftclassplayers.Count; p++)
            {
                draftclassplayers[p].WritePlayer(binwriter, version);
            }

            //padding
            if (binwriter.BaseStream.Position < draftfb.TotalLength)
            {
                while (binwriter.BaseStream.Position < draftfb.TotalLength + 18)
                {
                    binwriter.Write((byte)0);
                }
            }

            binwriter.Close();     
        }

        public void ExportCSVHeaders(StreamWriter wText, bool desc)
        {
            StringBuilder hbuilder = new StringBuilder();
            hbuilder.Append("DRAFT,");
            if (DraftClassVersion == MaddenFileVersion.Ver2019)
                hbuilder.Append("2019,");
            else if (DraftClassVersion == MaddenFileVersion.Ver2020)
                hbuilder.Append("2020,");
            if (desc)
                hbuilder.Append("Yes,");
            else hbuilder.Append("No,");
            wText.WriteLine(hbuilder.ToString());
            hbuilder.Clear();

            foreach (KeyValuePair<string, string> field in RatingDefs)
            {
                hbuilder.Append(field.Key);
                hbuilder.Append(",");
            }

            wText.WriteLine(hbuilder.ToString());

            if (desc)
                ExportCSVDescriptions(wText);
        }

        public void ExportCSVDescriptions(StreamWriter wText)
        {
            StringBuilder hbuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> field in RatingDefs)
            {
                hbuilder.Append(field.Value);
                hbuilder.Append(",");
            }

            wText.WriteLine(hbuilder.ToString());
        }

        public DraftReport OutputDraftClassStats()
        {
            return new DraftReport()
            {
                HighestRated = draftclassplayers.OrderByDescending(d => d.Overall).First().Overall,
                LowestRated = draftclassplayers.OrderBy(d => d.Overall).First().Overall,
                Ovr80plus = draftclassplayers.Where(d => d.Overall >= 80).Count(),
                Ovr70to79 = draftclassplayers.Where(d => d.Overall >= 70 && d.Overall < 80).Count(),
                Ovr60to69 = draftclassplayers.Where(d => d.Overall >= 60 && d.Overall < 70).Count(),
                Ovr50to59 = draftclassplayers.Where(d => d.Overall >= 50 && d.Overall < 60).Count(),
                Ovr40to49 = draftclassplayers.Where(d => d.Overall >= 40 && d.Overall < 50).Count(),
                OvrSub40 = draftclassplayers.Where(d => d.Overall < 40).Count(),
                XFactors = draftclassplayers.Where(d => d.Development == 3).Count(),
                Superstars = draftclassplayers.Where(d => d.Development == 2).Count(),
                Stars = draftclassplayers.Where(d => d.Development == 1).Count(),
                Normals = draftclassplayers.Where(d => d.Development == 0).Count(),
                DraftSize = draftclassplayers.Count()
            };
        }

        public void ImportCSVDraftClass(List<string> records, string[] fields)
        {
            for (int c = 0; c < records.Count(); c++)
            {
                if (c >= draftclassplayers.Count)
                    draftclassplayers.Add(new DraftPlayer());

                string[] csvrecord = records[c].Split(',');

                draftclassplayers[c].ImportCSVPlayer(fields, csvrecord, model);
                if (model.PlayerModel.PlayerComments.ContainsKey(draftclassplayers[c].NameLast))
                {
                    draftclassplayers[c].Comment = (UInt16)model.PlayerModel.PlayerComments[draftclassplayers[c].NameLast];
                }
                else draftclassplayers[c].Comment = 8191;
            }
        }

    
    }

}
