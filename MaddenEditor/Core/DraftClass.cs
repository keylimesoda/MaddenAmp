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
        // 328 bytes per Player record
        // confidence, personality, size grade, durability grade, physical grade, intangible grade
        // projected round & pick
        //

        #region Members
        public string NameFirst = "";
        public string NameLast = "";
        public int PHUN = 0;
        public string PlaceHolder = "PLACEHOLDER";
        public byte[] padding = new byte[16];

        public int College = 0;
        public int Unknown62 = 0;
        public int Unknown63 = 0;
        public int Age = 0;
        public int Height = 0;
        public int Weight = 0;
        public int Position = 0;
        public int JerseyNum = 0;
        public int DraftPick = 0;
        public int Unknown71 = 0;
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
        public int Unknown112 = 0;
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
        public int Unknown135 = 0;
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
        public int Unknown147 = 0;
        public int LBStyle = 0;
        public int SensePressure = 0;
        public int Unknown150 = 0;
        public int StripsBall = 0;
        public int Unknown152 = 0;
        public int ThrowAway = 0;
        public int ThrowSpiral = 0;
        public int QBTend = 0;
        public int RAC = 0;
        public int Development = 0;
        public int Predictable = 0;
        public int BackPlate = 0;

        // body parts
        public Single Body01 = 0;
        public Single Body02 = 0;
        public Single Body03 = 0;
        public Single Body04 = 0;
        public Single ChestSize = 0;
        public Single Body06 = 0;
        public Single Body07 = 0;
        public Single Body08 = 0;
        public Single Body09 = 0;
        public Single Body10 = 0;
        public Single Body11 = 0;
        public Single Body12 = 0;
        public Single Body13 = 0;
        public Single Body14 = 0;
        public Single Body15 = 0;
        public Single ArmSize = 0;
        public Single Body17 = 0;
        public Single Body18 = 0; 

        public int Unknown232 = 0;
        public int EyePaint = 0;
        public int Facemask = 0;
        public int Unknown235 = 0;
        public int FlakJacket = 0;
        public int Unknown237 = 0;
        public UInt16 FaceID = 0;

        public int ElbowLeft = 0;
        public int HandLeft = 0;
        public int WristLeft = 0;
        public int ElbowRight = 0;
        public int HandRight = 0;
        public int WristRight = 0;
        public int LeftSleeve = 0;
        public int Unknown247 = 0;
        public int Unknown248 = 0;
        public int Handed = 0;

        public int Unknown250 = 0;
        public int Helmet = 0;
        public int RightSleeve = 0;
        public int Unknown253 = 0;
        public int Unknown254 = 0;
        public int Unknown255 = 0;
        public int Unknown256 = 0;
        public int NeckRoll = 0;
        public int Unknown258 = 0;
        public int Unknown259 = 0;

        public UInt16 PortraitID = 0;
        public int QBStyle = 0;
        public int Unknown263 = 0;
        public int Shoes = 0;
        public int Unknown265 = 0;
        public int Unknown266 = 0;
        public int Unknown267 = 0;
        public int Unknown268 = 0;
        public int Unknown269 = 0;

        public int Unknown270 = 0;
        public int Unknown271 = 0;
        public int Unknown272 = 0;
        public int Unknown273 = 0;
        public int Unknown274 = 0;
        public int Unknown275 = 0;
        public int Unknown276 = 0;
        public int Visor = 0;
        public int Unknown278 = 0;
        public int Unknown279 = 0;

        public int Unknown280 = 0;
        public int Unknown281 = 0;
        public int Unknown282 = 0;
        public int Unknown283 = 0;
        public int Unknown284 = 0;
        public int Unknown285 = 0;
        public int Unknown286 = 0;
        public int Unknown287 = 0;
        public int Unknown288 = 0;
        public int Unknown289 = 0;

        public int Unknown290 = 0;
        public int Unknown291 = 0;
        public int Unknown292 = 0;
        public int Unknown293 = 0;
        public int Unknown294 = 0;
        public int Unknown295 = 0;
        public int Unknown296 = 0;
        public int Unknown297 = 0;
        public int Unknown298 = 0;
        public int Unknown299 = 0;

        public int Unknown300 = 0;
        public int Unknown301 = 0;
        public int Unknown302 = 0;
        public int Unknown303 = 0;
        public int Unknown304 = 0;
        public int Unknown305 = 0;
        public int Unknown306 = 0;
        public int Unknown307 = 0;
        public int Unknown308 = 0;
        public int Unknown309 = 0;

        public int Unknown310 = 0;
        public int Unknown311 = 0;
        public int Unknown312 = 0;
        public int Unknown313 = 0;
        public int Unknown314 = 0;
        public int Unknown315 = 0;
        public int Unknown316 = 0;
        public int Unknown317 = 0;
        public int Unknown318 = 0;
        public int Unknown319 = 0;

        public int Unknown320 = 0;
        public int Unknown321 = 0;
        public int Unknown322 = 0;
        public int Unknown323 = 0;
        public int Unknown324 = 0;
        public int Unknown325 = 0;
        public int Unknown326 = 0;
        public int Unknown327 = 0;

        public int PlayerID = 0;
        public int Archetype = 0;
        
        #endregion
         
        public List<UInt32> BodyParts = new List<UInt32>();
        public List<UInt32> faces = new List<UInt32>();
        public List<byte> unknowns = new List<byte>();

        public DraftPlayer()
        {
        }
        
        public void ReadPlayer(BinaryReader binreader)
        {
            long start = binreader.BaseStream.Position;

            #region First Name
            ASCIIEncoding enc = new ASCIIEncoding();
            List<byte> bytename = new List<byte>();
            for (int c = 0; c < 14; c++)
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
            for (int c = 0; c < 18; c++)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename2.Add(b);
            }
            NameLast = enc2.GetString(bytename2.ToArray());
            #endregion

            PHUN = binreader.ReadByte();

            #region Placeholder
            // Not sure if placeholder will ever be used so let's read it anyway
            bool stop = false;
            bytename.Clear();
            enc = new ASCIIEncoding();
            while (!stop)
            {
                byte b = binreader.ReadByte();
                if (b != 0)
                    bytename.Add(b);
                else stop = true;
            }
            PlaceHolder = enc.GetString(bytename.ToArray());
            #endregion

            binreader.BaseStream.Position = start + 60;
            College = binreader.ReadUInt16();       //60
            Unknown62 = binreader.ReadByte();       //62
            Unknown63 = binreader.ReadByte();       //63
            Age = binreader.ReadByte();             //64
            Height = binreader.ReadByte();          //65
            Weight = 160 + binreader.ReadByte();    //66 + 160 pounds            
            Position = binreader.ReadByte();        //67
            Archetype = binreader.ReadByte();       //68            
            JerseyNum = binreader.ReadByte();       //69

            DraftPick = binreader.ReadByte();       //70
            Unknown71 = binreader.ReadByte();       //71
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
            Unknown112 = binreader.ReadByte();      //112            
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
            Unknown135 = binreader.ReadByte();      //135            
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
            Unknown147 = binreader.ReadByte();      //147
            LBStyle = binreader.ReadByte();         //148
            SensePressure = binreader.ReadByte();   //149

            Unknown150 = binreader.ReadByte();      //150            
            StripsBall = binreader.ReadByte();      //151
            Unknown152 = binreader.ReadByte();      //152            
            ThrowAway = binreader.ReadByte();       //153
            ThrowSpiral = binreader.ReadByte();     //154
            QBTend = binreader.ReadByte();          //155
            RAC = binreader.ReadByte();             //156
            Development = binreader.ReadByte();     //157
            Predictable = binreader.ReadByte();     //158
            BackPlate = binreader.ReadByte();       //159            

            Body01 = binreader.ReadSingle();        // 160-163
            Body02 = binreader.ReadSingle();        // 164-167
            Body03 = binreader.ReadSingle();        // 168-171
            Body04 = binreader.ReadSingle();        // 172-175            
            ChestSize = binreader.ReadSingle();     // 176-179  chest              
            Body06 = binreader.ReadSingle();        // 180-183
            Body07 = binreader.ReadSingle();        // 184-187
            Body08 = binreader.ReadSingle();        // 188-181
            Body09 = binreader.ReadSingle();        // 192-195     
            Body10 = binreader.ReadSingle();        // 196-199
            Body11 = binreader.ReadSingle();        // 200-203
            Body12 = binreader.ReadSingle();        // 204-207
            Body13 = binreader.ReadSingle();        // 208-211    
            Body14 = binreader.ReadSingle();        // 212-215
            Body15 = binreader.ReadSingle();        // 216-219
            ArmSize = binreader.ReadSingle();       // 220-223
            Body17 = binreader.ReadSingle();        // 224-227
            Body18 = binreader.ReadSingle();        // 228-231            

            Unknown232 = binreader.ReadByte();      //232
            EyePaint = binreader.ReadByte();        //233
            Facemask = binreader.ReadByte();        //234
            Unknown235 = binreader.ReadByte();      //235
            FlakJacket = binreader.ReadByte();      //236
            Unknown237 = binreader.ReadByte();      //237
            FaceID = binreader.ReadUInt16();        //238-239

            ElbowLeft = binreader.ReadByte();       //240
            HandLeft = binreader.ReadByte();        //241
            WristLeft = binreader.ReadByte();       //242
            ElbowRight = binreader.ReadByte();      //243
            HandRight = binreader.ReadByte();       //244
            WristRight = binreader.ReadByte();      //245
            LeftSleeve = binreader.ReadByte();      //246
            Unknown247 = binreader.ReadByte();      //247
            Unknown248 = binreader.ReadByte();      //248            
            Handed = binreader.ReadByte();          //249

            Unknown250 = binreader.ReadByte();      //250
            Helmet = binreader.ReadByte();          //251
            RightSleeve = binreader.ReadByte();     //252
            Unknown253 = binreader.ReadByte();      //253
            Unknown254 = binreader.ReadByte();      //254
            Unknown255 = binreader.ReadByte();      //255
            Unknown256 = binreader.ReadByte();      //256
            NeckRoll = binreader.ReadByte();        //257
            Unknown258 = binreader.ReadByte();      //258
            Unknown259 = binreader.ReadByte();      //259

            PortraitID = binreader.ReadUInt16();    // 260-261
            QBStyle = binreader.ReadByte();         //262
            Unknown263 = binreader.ReadByte();      //263
            Shoes = binreader.ReadByte();           //264
            Unknown265 = binreader.ReadByte();      //265
            Unknown266 = binreader.ReadByte();      //266
            Unknown267 = binreader.ReadByte();      //267
            Unknown268 = binreader.ReadByte();      //268
            Unknown269 = binreader.ReadByte();      //269

            Unknown270 = binreader.ReadByte();      //270
            Unknown271 = binreader.ReadByte();      //271
            Unknown272 = binreader.ReadByte();      //272
            Unknown273 = binreader.ReadByte();      //273
            Unknown274 = binreader.ReadByte();      //274
            Unknown275 = binreader.ReadByte();      //275
            Unknown276 = binreader.ReadByte();      //276
            Visor = binreader.ReadByte();           //277
            Unknown278 = binreader.ReadByte();      //278
            Unknown279 = binreader.ReadByte();      //279

            Unknown280 = binreader.ReadByte();            
            Unknown281 = binreader.ReadByte();
            Unknown282 = binreader.ReadByte();       //282
            Unknown283 = binreader.ReadByte();
            Unknown284 = binreader.ReadByte();
            Unknown285 = binreader.ReadByte();
            Unknown286 = binreader.ReadByte();
            Unknown287 = binreader.ReadByte();
            Unknown288 = binreader.ReadByte();
            Unknown289 = binreader.ReadByte();

            Unknown290 = binreader.ReadByte();
            Unknown291 = binreader.ReadByte();
            Unknown292 = binreader.ReadByte();
            Unknown293 = binreader.ReadByte();
            Unknown294 = binreader.ReadByte();
            Unknown295 = binreader.ReadByte();
            Unknown296 = binreader.ReadByte();
            Unknown297 = binreader.ReadByte();
            Unknown298 = binreader.ReadByte();
            Unknown299 = binreader.ReadByte();

            Unknown300 = binreader.ReadByte();
            Unknown301 = binreader.ReadByte();
            Unknown302 = binreader.ReadByte();
            Unknown303 = binreader.ReadByte();
            Unknown304 = binreader.ReadByte();
            Unknown305 = binreader.ReadByte();
            Unknown306 = binreader.ReadByte();
            Unknown307 = binreader.ReadByte();
            Unknown308 = binreader.ReadByte();
            Unknown309 = binreader.ReadByte();

            Unknown310 = binreader.ReadByte();
            Unknown311 = binreader.ReadByte();
            Unknown312 = binreader.ReadByte();
            Unknown313 = binreader.ReadByte();
            Unknown314 = binreader.ReadByte();
            Unknown315 = binreader.ReadByte();
            Unknown316 = binreader.ReadByte();
            Unknown317 = binreader.ReadByte();
            Unknown318 = binreader.ReadByte();
            Unknown319 = binreader.ReadByte();

            Unknown320 = binreader.ReadByte();
            Unknown321 = binreader.ReadByte();
            Unknown322 = binreader.ReadByte();
            Unknown323 = binreader.ReadByte();
            Unknown324 = binreader.ReadByte();
            Unknown325 = binreader.ReadByte();
            Unknown326 = binreader.ReadByte();
            Unknown327 = binreader.ReadByte();


        }

        public void WritePlayer(BinaryWriter binwriter)
        {
            #region First/Last Name
            byte[] ascii = System.Text.Encoding.ASCII.GetBytes(NameFirst);
            for (int c = 0; c < 14; c++)
            {
                if (ascii.Length > c)
                    binwriter.Write(ascii[c]);
                else binwriter.Write((byte)0);
            }
            byte[] ascii2 = System.Text.Encoding.ASCII.GetBytes(NameLast);
            for (int c = 0; c < 18; c++)
            {
                if (ascii2.Length > c)
                    binwriter.Write(ascii2[c]);
                else binwriter.Write((byte)0);
            }
            #endregion

            binwriter.Write((byte)PHUN);
            byte[] ph = System.Text.Encoding.ASCII.GetBytes(PlaceHolder);
            for (int c = 0; c < ph.Length; c++)
            {
                binwriter.Write(ph[c]);
            }
            for (int c = 0; c < 16; c++)    // padding
                binwriter.Write((byte)0);

            binwriter.Write((UInt16)College);
            binwriter.Write((byte)Unknown62);
            binwriter.Write((byte)Unknown63);
            binwriter.Write((byte)Age);
            binwriter.Write((byte)Height);
            binwriter.Write((byte)(Weight - 160));
            binwriter.Write((byte)Position);
            binwriter.Write((byte)Archetype);
            binwriter.Write((byte)JerseyNum);

            binwriter.Write((byte)DraftPick);   //70
            binwriter.Write((byte)Unknown71);   //71
            binwriter.Write((byte)DraftRound);   //72
            binwriter.Write((byte)Overall);     //73
            binwriter.Write((byte)Accel);
            binwriter.Write((byte)Agility);
            binwriter.Write((byte)Awareness);
            binwriter.Write((byte)BCVision);
            binwriter.Write((byte)BlockShed);
            binwriter.Write((byte)BreakSack);

            binwriter.Write((byte)BreakTackle); //80
            binwriter.Write((byte)Carry);
            binwriter.Write((byte)Catching);
            binwriter.Write((byte)CatchTraffic);
            binwriter.Write((byte)Elusive);
            binwriter.Write((byte)MovesFinesse);
            binwriter.Write((byte)HitPower);
            binwriter.Write((byte)ImpactBlock);
            binwriter.Write((byte)Injury);
            binwriter.Write((byte)JukeMove);

            binwriter.Write((byte)Jumping);     //90
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
            binwriter.Write((byte)Unknown112);
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
            binwriter.Write((byte)Unknown135);
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
            binwriter.Write((byte)PlaysBall);
            binwriter.Write((byte)Unknown147);
            binwriter.Write((byte)LBStyle);
            binwriter.Write((byte)SensePressure);

            binwriter.Write((byte)Unknown150);
            binwriter.Write((byte)StripsBall);
            binwriter.Write((byte)Unknown152);
            binwriter.Write((byte)ThrowAway);
            binwriter.Write((byte)ThrowSpiral);
            binwriter.Write((byte)QBTend);
            binwriter.Write((byte)RAC);
            binwriter.Write((byte)Development);
            binwriter.Write((byte)Predictable);
            binwriter.Write((byte)BackPlate);

            binwriter.Write(Body01);            // 160-163
            binwriter.Write(Body02);            // 164-167
            binwriter.Write(Body03);            // 168-171
            binwriter.Write(Body04);            // 172-175            
            binwriter.Write(ChestSize);         // 176-179  chest              
            binwriter.Write(Body06);            // 180-183
            binwriter.Write(Body07);            // 184-187
            binwriter.Write(Body08);            // 188-181
            binwriter.Write(Body09);            // 192-195     
            binwriter.Write(Body10);            // 196-199
            binwriter.Write(Body11);            // 200-203
            binwriter.Write(Body12);            // 204-207
            binwriter.Write(Body13);            // 208-211    
            binwriter.Write(Body14);            // 212-215
            binwriter.Write(Body15);            // 216-219
            binwriter.Write(ArmSize);           // 220-223
            binwriter.Write(Body17);            // 224-227
            binwriter.Write(Body18);            // 228-231   

            binwriter.Write((byte)Unknown232);
            binwriter.Write((byte)EyePaint);
            binwriter.Write((byte)Facemask);
            binwriter.Write((byte)Unknown235);
            binwriter.Write((byte)FlakJacket);
            binwriter.Write((byte)Unknown237);
            binwriter.Write(FaceID);

            binwriter.Write((byte)ElbowLeft);
            binwriter.Write((byte)HandLeft);
            binwriter.Write((byte)WristLeft);
            binwriter.Write((byte)ElbowRight);
            binwriter.Write((byte)HandRight);
            binwriter.Write((byte)WristRight);
            binwriter.Write((byte)LeftSleeve);  //246
            binwriter.Write((byte)Unknown247);
            binwriter.Write((byte)Unknown248);
            binwriter.Write((byte)Handed);

            binwriter.Write((byte)Unknown250);
            binwriter.Write((byte)Helmet);
            binwriter.Write((byte)RightSleeve);
            binwriter.Write((byte)Unknown253);
            binwriter.Write((byte)Unknown254);
            binwriter.Write((byte)Unknown255);
            binwriter.Write((byte)Unknown256);
            binwriter.Write((byte)NeckRoll);
            binwriter.Write((byte)Unknown258);
            binwriter.Write((byte)Unknown259);

            binwriter.Write(PortraitID);            
            binwriter.Write((byte)QBStyle);
            binwriter.Write((byte)Unknown263);
            binwriter.Write((byte)Shoes);
            binwriter.Write((byte)Unknown265);
            binwriter.Write((byte)Unknown266);
            binwriter.Write((byte)Unknown267);
            binwriter.Write((byte)Unknown268);
            binwriter.Write((byte)Unknown269);

            binwriter.Write((byte)Unknown270);
            binwriter.Write((byte)Unknown271);
            binwriter.Write((byte)Unknown272);
            binwriter.Write((byte)Unknown273);
            binwriter.Write((byte)Unknown274);
            binwriter.Write((byte)Unknown275);
            binwriter.Write((byte)Unknown276);
            binwriter.Write((byte)Visor);
            binwriter.Write((byte)Unknown278);
            binwriter.Write((byte)Unknown279);

            binwriter.Write((byte)Unknown280);
            binwriter.Write((byte)Unknown281);
            binwriter.Write((byte)Unknown282);
            binwriter.Write((byte)Unknown283);
            binwriter.Write((byte)Unknown284);
            binwriter.Write((byte)Unknown285);
            binwriter.Write((byte)Unknown286);
            binwriter.Write((byte)Unknown287);
            binwriter.Write((byte)Unknown288);
            binwriter.Write((byte)Unknown289);

            binwriter.Write((byte)Unknown290);
            binwriter.Write((byte)Unknown291);
            binwriter.Write((byte)Unknown292);
            binwriter.Write((byte)Unknown293);
            binwriter.Write((byte)Unknown294);
            binwriter.Write((byte)Unknown295);
            binwriter.Write((byte)Unknown296);
            binwriter.Write((byte)Unknown297);
            binwriter.Write((byte)Unknown298);
            binwriter.Write((byte)Unknown299);

            binwriter.Write((byte)Unknown300);
            binwriter.Write((byte)Unknown301);
            binwriter.Write((byte)Unknown302);
            binwriter.Write((byte)Unknown303);
            binwriter.Write((byte)Unknown304);
            binwriter.Write((byte)Unknown305);
            binwriter.Write((byte)Unknown306);
            binwriter.Write((byte)Unknown307);
            binwriter.Write((byte)Unknown308);
            binwriter.Write((byte)Unknown309);

            binwriter.Write((byte)Unknown310);
            binwriter.Write((byte)Unknown311);
            binwriter.Write((byte)Unknown312);
            binwriter.Write((byte)Unknown313);
            binwriter.Write((byte)Unknown314);
            binwriter.Write((byte)Unknown315);
            binwriter.Write((byte)Unknown316);
            binwriter.Write((byte)Unknown317);
            binwriter.Write((byte)Unknown318);
            binwriter.Write((byte)Unknown319);

            binwriter.Write((byte)Unknown320);
            binwriter.Write((byte)Unknown321);
            binwriter.Write((byte)Unknown322);
            binwriter.Write((byte)Unknown323);
            binwriter.Write((byte)Unknown324);
            binwriter.Write((byte)Unknown325);
            binwriter.Write((byte)Unknown326);
            binwriter.Write((byte)Unknown327);
        }

        public string GetCSVAttribute(string field, EditorModel emodel, bool desc)
        {
            if (field == "PFNA")
                return NameFirst;
            else if (field == "PLNA")
                return NameLast;
            else if (field == "PHUN")
                return PHUN.ToString();
            else if (field == "HOLD")
                return PlaceHolder;
            else if (field == "PCOL")
            {
                if (desc)
                {
                    college_entry ce = emodel.Colleges[College];
                    return ce.name;
                }
                else return College.ToString();
            }
            else if (field == "U062")
                return Unknown62.ToString();
            else if (field == "U063")
                return Unknown63.ToString();
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
            else if (field == "U071")
                return Unknown71.ToString();
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
            else if (field == "U112")
                return Unknown112.ToString();
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
            else if (field == "U135")
                return Unknown135.ToString();
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
            else if (field == "U147")
                return Unknown147.ToString();
            else if (field == "LBST")
                return LBStyle.ToString();
            else if (field == "TRSP")
                return SensePressure.ToString();

            else if (field == "U150")
                return Unknown150.ToString();
            else if (field == "TRSB")
                return StripsBall.ToString();
            else if (field == "U152")
                return Unknown152.ToString();
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

            else if (field == "BD01")
                return Body01.ToString();
            else if (field == "BD02")
                return Body02.ToString();
            else if (field == "BD03")
                return Body03.ToString();
            else if (field == "BD04")
                return Body04.ToString();
            else if (field == "PCSZ")
                return ChestSize.ToString();
            else if (field == "BD06")
                return Body06.ToString();
            else if (field == "BD07")
                return Body07.ToString();
            else if (field == "BD08")
                return Body08.ToString();
            else if (field == "BD09")
                return Body09.ToString();
            else if (field == "BD10")
                return Body10.ToString();
            else if (field == "BD11")
                return Body11.ToString();
            else if (field == "BD12")
                return Body12.ToString();
            else if (field == "BD13")
                return Body13.ToString();
            else if (field == "BD14")
                return Body14.ToString();
            else if (field == "BD15")
                return Body15.ToString();
            else if (field == "PASZ")
                return ArmSize.ToString();
            else if (field == "BD17")
                return Body17.ToString();
            else if (field == "BD18")
                return Body18.ToString();

            else if (field == "U232")
                return Unknown232.ToString();
            else if (field == "PEYE")
                return EyePaint.ToString();
            else if (field == "PFMK")
                return Facemask.ToString();
            else if (field == "U235")
                return Unknown235.ToString();
            else if (field == "PFLA")
                return FlakJacket.ToString();
            else if (field == "U237")
                return Unknown237.ToString();
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
            else if (field == "U247")
                return Unknown247.ToString();
            else if (field == "U248")
                return Unknown248.ToString();
            else if (field == "PHAN")
                return Handed.ToString();

            else if (field == "U250")
                return Unknown250.ToString();
            else if (field == "PHLM")
                return Helmet.ToString();
            else if (field == "PMOR")
                return RightSleeve.ToString();
            else if (field == "U253")
                return Unknown253.ToString();
            else if (field == "U254")
                return Unknown254.ToString();
            else if (field == "U255")
                return Unknown255.ToString();
            else if (field == "U256")
                return Unknown256.ToString();
            else if (field == "PNEK")
                return NeckRoll.ToString();
            else if (field == "U258")
                return Unknown258.ToString();
            else if (field == "U259")
                return Unknown259.ToString();

            else if (field == "PSXP")
                return PortraitID.ToString();
            else if (field == "PQBS")
                return QBStyle.ToString();
            else if (field == "U263")
                return Unknown263.ToString();
            else if (field == "U264")
                return Shoes.ToString();
            else if (field == "U265")
                return Unknown265.ToString();
            else if (field == "U266")
                return Unknown266.ToString();
            else if (field == "U267")
                return Unknown267.ToString();
            else if (field == "U268")
                return Unknown268.ToString();
            else if (field == "U269")
                return Unknown269.ToString();

            else if (field == "U270")
                return Unknown270.ToString();
            else if (field == "U271")
                return Unknown271.ToString();
            else if (field == "U272")
                return Unknown272.ToString();
            else if (field == "U273")
                return Unknown273.ToString();
            else if (field == "U274")
                return Unknown274.ToString();
            else if (field == "U275")
                return Unknown275.ToString();
            else if (field == "U276")
                return Unknown276.ToString();
            else if (field == "PVIS")
                return Visor.ToString();
            else if (field == "U278")
                return Unknown278.ToString();
            else if (field == "U279")
                return Unknown279.ToString();

            else if (field == "U280")
                return Unknown280.ToString();
            else if (field == "U281")
                return Unknown281.ToString();
            else if (field == "U282")
                return Unknown282.ToString();
            else if (field == "U283")
                return Unknown283.ToString();
            else if (field == "U284")
                return Unknown284.ToString();
            else if (field == "U285")
                return Unknown285.ToString();
            else if (field == "U286")
                return Unknown286.ToString();
            else if (field == "U287")
                return Unknown287.ToString();
            else if (field == "U288")
                return Unknown288.ToString();
            else if (field == "U289")
                return Unknown289.ToString();

            else if (field == "U290")
                return Unknown290.ToString();
            else if (field == "U291")
                return Unknown291.ToString();
            else if (field == "U292")
                return Unknown292.ToString();
            else if (field == "U293")
                return Unknown293.ToString();
            else if (field == "U294")
                return Unknown294.ToString();
            else if (field == "U295")
                return Unknown295.ToString();
            else if (field == "U296")
                return Unknown296.ToString();
            else if (field == "U297")
                return Unknown297.ToString();
            else if (field == "U298")
                return Unknown298.ToString();
            else if (field == "U299")
                return Unknown299.ToString();

            else if (field == "U300")
                return Unknown300.ToString();
            else if (field == "U301")
                return Unknown301.ToString();
            else if (field == "U302")
                return Unknown302.ToString();
            else if (field == "U303")
                return Unknown303.ToString();
            else if (field == "U304")
                return Unknown304.ToString();
            else if (field == "U305")
                return Unknown305.ToString();
            else if (field == "U306")
                return Unknown306.ToString();
            else if (field == "U307")
                return Unknown307.ToString();
            else if (field == "U308")
                return Unknown308.ToString();
            else if (field == "U309")
                return Unknown309.ToString();

            else if (field == "U310")
                return Unknown310.ToString();
            else if (field == "U311")
                return Unknown311.ToString();
            else if (field == "U312")
                return Unknown312.ToString();
            else if (field == "U313")
                return Unknown313.ToString();
            else if (field == "U314")
                return Unknown314.ToString();
            else if (field == "U315")
                return Unknown315.ToString();
            else if (field == "U316")
                return Unknown316.ToString();
            else if (field == "U317")
                return Unknown317.ToString();
            else if (field == "U318")
                return Unknown318.ToString();
            else if (field == "U319")
                return Unknown319.ToString();

            else if (field == "U320")
                return Unknown320.ToString();
            else if (field == "U321")
                return Unknown321.ToString();
            else if (field == "U322")
                return Unknown322.ToString();
            else if (field == "U323")
                return Unknown323.ToString();
            else if (field == "U324")
                return Unknown324.ToString();
            else if (field == "U325")
                return Unknown325.ToString();
            else if (field == "U326")
                return Unknown326.ToString();
            else if (field == "U327")
                return Unknown327.ToString();

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
            else if (field == "HOLD")
                PlaceHolder = val;
            else if (field == "PHUN")
                PHUN = Convert.ToInt32(val);
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
            else if (field == "U062")
                Unknown62 = Convert.ToInt32(val);
            else if (field == "U063")
                Unknown63 = Convert.ToInt32(val);
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
            else if (field == "U071")
                Unknown71 = Convert.ToInt32(val);
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
            else if (field == "U112")
                Unknown112 = Convert.ToInt32(val);
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
            else if (field == "U135")
                Unknown135 = Convert.ToInt32(val);
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
            else if (field == "U147")
                Unknown147 = Convert.ToInt32(val);
            else if (field == "LBST")
                LBStyle = Convert.ToInt32(val);
            else if (field == "TRSP")
                SensePressure = Convert.ToInt32(val);

            else if (field == "U150")                   //150
                Unknown150 = Convert.ToInt32(val);
            else if (field == "TRSB")
                StripsBall = Convert.ToInt32(val);
            else if (field == "U152")
                Unknown152 = Convert.ToInt32(val);
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

            else if (field == "BD01")               //160
                Body01 = Convert.ToSingle(val);
            else if (field == "BD02")
                Body02 = Convert.ToSingle(val);
            else if (field == "BD03")
                Body03 = Convert.ToSingle(val);
            else if (field == "BD04")
                Body04 = Convert.ToSingle(val);
            else if (field == "PCSZ")
                ChestSize = Convert.ToSingle(val);
            else if (field == "BD06")
                Body06 = Convert.ToSingle(val);
            else if (field == "BD07")
                Body07 = Convert.ToSingle(val);
            else if (field == "BD08")
                Body08 = Convert.ToSingle(val);
            else if (field == "BD09")
                Body09 = Convert.ToSingle(val);
            else if (field == "BD10")
                Body10 = Convert.ToSingle(val);
            else if (field == "BD11")
                Body11 = Convert.ToSingle(val);
            else if (field == "BD12")
                Body12 = Convert.ToSingle(val);
            else if (field == "BD13")
                Body13 = Convert.ToSingle(val);
            else if (field == "BD14")
                Body14 = Convert.ToSingle(val);
            else if (field == "BD15")
                Body15 = Convert.ToSingle(val);
            else if (field == "PASZ")
                ArmSize = Convert.ToSingle(val);
            else if (field == "BD17")
                Body17 = Convert.ToSingle(val);
            else if (field == "BD18")
                Body18 = Convert.ToSingle(val);

            else if (field == "U232")                   //232
                Unknown232 = Convert.ToInt32(val);
            else if (field == "PEYE")
                EyePaint = Convert.ToInt32(val);
            else if (field == "PFMK")
                Facemask = Convert.ToInt32(val);
            else if (field == "U235")
                Unknown235 = Convert.ToInt32(val);
            else if (field == "PFLA")
                FlakJacket = Convert.ToInt32(val);
            else if (field == "U237")
                Unknown237 = Convert.ToInt32(val);
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
                Unknown247 = Convert.ToInt32(val);
            else if (field == "U248")
                Unknown248 = Convert.ToInt32(val);
            else if (field == "PHAN")
                Handed = Convert.ToInt32(val);

            else if (field == "U250")                   //250
                Unknown250 = Convert.ToInt32(val);
            else if (field == "PHLM")
                Helmet = Convert.ToInt32(val);
            else if (field == "PMOR")
                RightSleeve = Convert.ToInt32(val);
            else if (field == "U253")
                Unknown253 = Convert.ToInt32(val);
            else if (field == "U254")
                Unknown254 = Convert.ToInt32(val);
            else if (field == "U255")
                Unknown255 = Convert.ToInt32(val);
            else if (field == "U256")
                Unknown256 = Convert.ToInt32(val);
            else if (field == "PNEK")
                NeckRoll = Convert.ToInt32(val);
            else if (field == "U258")
                Unknown258 = Convert.ToInt32(val);
            else if (field == "U259")
                Unknown259 = Convert.ToInt32(val);

            else if (field == "PSXP") //260
                PortraitID = Convert.ToUInt16(val);
            else if (field == "PQBS")
                QBStyle = Convert.ToInt32(val);
            else if (field == "U263")
                Unknown263 = Convert.ToInt32(val);
            else if (field == "U264")
                Shoes = Convert.ToInt32(val);
            else if (field == "U265")
                Unknown265 = Convert.ToInt32(val);
            else if (field == "U266")
                Unknown266 = Convert.ToInt32(val);
            else if (field == "U267")
                Unknown267 = Convert.ToInt32(val);
            else if (field == "U268")
                Unknown268 = Convert.ToInt32(val);
            else if (field == "U269")
                Unknown269 = Convert.ToInt32(val);

            else if (field == "U270")
                Unknown270 = Convert.ToInt32(val);
            else if (field == "U271")
                Unknown271 = Convert.ToInt32(val);
            else if (field == "U272")
                Unknown272 = Convert.ToInt32(val);
            else if (field == "U273")
                Unknown273 = Convert.ToInt32(val);
            else if (field == "U274")
                Unknown274 = Convert.ToInt32(val);
            else if (field == "U275")
                Unknown275 = Convert.ToInt32(val);
            else if (field == "U276")
                Unknown276 = Convert.ToInt32(val);
            else if (field == "PVIS")
                Visor = Convert.ToInt32(val);
            else if (field == "U278")
                Unknown278 = Convert.ToInt32(val);
            else if (field == "U279")
                Unknown279 = Convert.ToInt32(val);

            else if (field == "U280")
                Unknown280 = Convert.ToInt32(val);
            else if (field == "U281")
                Unknown281 = Convert.ToInt32(val);
            else if (field == "U282")
                Unknown282 = Convert.ToInt32(val);
            else if (field == "U283")
                Unknown283 = Convert.ToInt32(val);
            else if (field == "U284")
                Unknown284 = Convert.ToInt32(val);
            else if (field == "U285")
                Unknown285 = Convert.ToInt32(val);
            else if (field == "U286")
                Unknown286 = Convert.ToInt32(val);
            else if (field == "U287")
                Unknown287 = Convert.ToInt32(val);
            else if (field == "U288")
                Unknown288 = Convert.ToInt32(val);
            else if (field == "U289")
                Unknown289 = Convert.ToInt32(val);

            else if (field == "U290")
                Unknown290 = Convert.ToInt32(val);
            else if (field == "U291")
                Unknown291 = Convert.ToInt32(val);
            else if (field == "U292")
                Unknown292 = Convert.ToInt32(val);
            else if (field == "U293")
                Unknown293 = Convert.ToInt32(val);
            else if (field == "U294")
                Unknown294 = Convert.ToInt32(val);
            else if (field == "U295")
                Unknown295 = Convert.ToInt32(val);
            else if (field == "U296")
                Unknown296 = Convert.ToInt32(val);
            else if (field == "U297")
                Unknown297 = Convert.ToInt32(val);
            else if (field == "U298")
                Unknown298 = Convert.ToInt32(val);
            else if (field == "U299")
                Unknown299 = Convert.ToInt32(val);

            else if (field == "U300")
                Unknown300 = Convert.ToInt32(val);
            else if (field == "U301")
                Unknown301 = Convert.ToInt32(val);
            else if (field == "U302")
                Unknown302 = Convert.ToInt32(val);
            else if (field == "U303")
                Unknown303 = Convert.ToInt32(val);
            else if (field == "U304")
                Unknown304 = Convert.ToInt32(val);
            else if (field == "U305")
                Unknown305 = Convert.ToInt32(val);
            else if (field == "U306")
                Unknown306 = Convert.ToInt32(val);
            else if (field == "U307")
                Unknown307 = Convert.ToInt32(val);
            else if (field == "U308")
                Unknown308 = Convert.ToInt32(val);
            else if (field == "U309")
                Unknown309 = Convert.ToInt32(val);

            else if (field == "U310")
                Unknown310 = Convert.ToInt32(val);
            else if (field == "U311")
                Unknown311 = Convert.ToInt32(val);
            else if (field == "U312")
                Unknown312 = Convert.ToInt32(val);
            else if (field == "U313")
                Unknown313 = Convert.ToInt32(val);
            else if (field == "U314")
                Unknown314 = Convert.ToInt32(val);
            else if (field == "U315")
                Unknown315 = Convert.ToInt32(val);
            else if (field == "U316")
                Unknown316 = Convert.ToInt32(val);
            else if (field == "U317")
                Unknown317 = Convert.ToInt32(val);
            else if (field == "U318")
                Unknown318 = Convert.ToInt32(val);
            else if (field == "U319")
                Unknown319 = Convert.ToInt32(val);

            else if (field == "U320")
                Unknown320 = Convert.ToInt32(val);
            else if (field == "U321")
                Unknown321 = Convert.ToInt32(val);
            else if (field == "U322")
                Unknown322 = Convert.ToInt32(val);
            else if (field == "U323")
                Unknown323 = Convert.ToInt32(val);
            else if (field == "U324")
                Unknown324 = Convert.ToInt32(val);
            else if (field == "U325")
                Unknown325 = Convert.ToInt32(val);
            else if (field == "U326")
                Unknown326 = Convert.ToInt32(val);
            else if (field == "U327")
                Unknown327 = Convert.ToInt32(val);

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
            InitRatingDefs();
            fb = new FB();
        }
        
        public void InitRatingDefs()
        {
            #region Bodyparts
            /*
            RatingDefs.Add("BSAA", "Arm Defn");
            RatingDefs.Add("BSAT", "Arm Size");
            RatingDefs.Add("BSBA", "Butt Defn");
            RatingDefs.Add("BSBT", "Butt Size");
            RatingDefs.Add("BSCA", "Calf Defn");
            RatingDefs.Add("BSCT", "Calf Size");
            RatingDefs.Add("BSFA", "Foot Defn");
            RatingDefs.Add("BSFT", "Foot Size");
            RatingDefs.Add("BSGA", "Gut Defn");
            RatingDefs.Add("BSGT", "Gut Size");
            RatingDefs.Add("BSPA", "Pad Defn");
            RatingDefs.Add("BSPT", "Pad Size");
            RatingDefs.Add("BSSA", "Shoulder Defn");
            RatingDefs.Add("BSST", "Shoulder Size");
            RatingDefs.Add("BSTA", "Thigh Defn");
            RatingDefs.Add("BSTT", "Thigh Size");
            RatingDefs.Add("BSWA", "Waist Defn");
            RatingDefs.Add("BSWT", "Waist Size"); 
          */
            #endregion

            RatingDefs.Add("PFNA", "First Name");
            RatingDefs.Add("PLNA", "Last Name");
            RatingDefs.Add("HOLD", "Placeholder");
            RatingDefs.Add("PHUN", "PH Unknown");
            // Placeholder
            RatingDefs.Add("PCOL", "College");
            RatingDefs.Add("U062", "Unknown62");
            RatingDefs.Add("U063", "Unknown63");
            RatingDefs.Add("PAGE", "Age");
            RatingDefs.Add("PHGT", "Height");
            RatingDefs.Add("PWGT", "Weight");
            RatingDefs.Add("PPOS", "Position");
            RatingDefs.Add("PLTY", "Archetype");
            RatingDefs.Add("PJEN", "Jersey Num");

            RatingDefs.Add("PDPI", "Draft Pick");
            RatingDefs.Add("U071", "Unknown71");
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
            RatingDefs.Add("U112", "Unknown112");
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
            RatingDefs.Add("U135", "Unknown135");
            RatingDefs.Add("TRBR", "Bull Rush");
            RatingDefs.Add("TRDS", "DL Spin Move");
            RatingDefs.Add("TRSW", "DL Swim Move");
            RatingDefs.Add("TRDO", "Drop Open Pass");

            RatingDefs.Add("TRSC", "Sideline Catch");
            RatingDefs.Add("TRFY", "Fight for Yards");
            RatingDefs.Add("TRFP", "Forces Pass");
            RatingDefs.Add("TRHM", "High Motor");
            RatingDefs.Add("TRJR", "Agg. Catch");
            RatingDefs.Add("TRIC", "Penalty");
            RatingDefs.Add("TRPB", "Plays Ball");
            RatingDefs.Add("U147", "Unknown147");
            RatingDefs.Add("LBST", "LB Style");            
            RatingDefs.Add("TRSP", "Sense Pressure");

            RatingDefs.Add("U150", "Unknown150");
            RatingDefs.Add("TRSB", "Strips Ball");
            RatingDefs.Add("U152", "Unknown152");
            RatingDefs.Add("TRTA", "Throw Away");
            RatingDefs.Add("TRTS", "Throw Spiral");
            RatingDefs.Add("QTEN", "QB Tend.");
            RatingDefs.Add("TRWU", "Run After Catch");
            RatingDefs.Add("PROL", "Development");
            RatingDefs.Add("Pred", "Predictable");
            RatingDefs.Add("PLBP", "Back Plate");

            RatingDefs.Add("BD01", "Body01");
            RatingDefs.Add("BD02", "Body02");
            RatingDefs.Add("BD03", "Body03");
            RatingDefs.Add("BD04", "Body04");
            RatingDefs.Add("PCSZ", "Chest Size");
            RatingDefs.Add("BD06", "Body06");
            RatingDefs.Add("BD07", "Body07");
            RatingDefs.Add("BD08", "Body08");
            RatingDefs.Add("BD09", "Body09");
            RatingDefs.Add("BD10", "Body10");
            RatingDefs.Add("BD11", "Body11");
            RatingDefs.Add("BD12", "Body12");
            RatingDefs.Add("BD13", "Body13");
            RatingDefs.Add("BD14", "Body14");
            RatingDefs.Add("BD15", "Body15");
            RatingDefs.Add("PASZ", "Arm Size");            
            RatingDefs.Add("BD17", "Body17");
            RatingDefs.Add("BD18", "Body18");

            RatingDefs.Add("U232", "Unknown232");
            RatingDefs.Add("PEYE", "Eye Paint");
            RatingDefs.Add("PFMK", "Face Mask");
            RatingDefs.Add("U235", "Unknown235");
            RatingDefs.Add("PFLA", "Flak Jacket");   
            RatingDefs.Add("U237", "Unknown237");
            RatingDefs.Add("PGHE", "FaceID");            

            RatingDefs.Add("PLEL", "Elbow Left");
            RatingDefs.Add("PLHA", "Hand Left");
            RatingDefs.Add("PLWR", "Wrist Left");
            RatingDefs.Add("PREL", "Elbow Right");
            RatingDefs.Add("PRHA", "Hand Right");
            RatingDefs.Add("PRWR", "Wrist Right");
            RatingDefs.Add("PGSL", "Sleeve Left");
            RatingDefs.Add("U247", "Unknown247");
            RatingDefs.Add("U248", "Unknown248");
            RatingDefs.Add("PHAN", "Handed");

            RatingDefs.Add("U250", "Unknown250");
            RatingDefs.Add("PHLM", "Helmet");
            RatingDefs.Add("PMOR", "Sleeve Right");
            RatingDefs.Add("U253", "Unknown253");
            RatingDefs.Add("U254", "Unknown254");
            RatingDefs.Add("U255", "Unknown255");
            RatingDefs.Add("U256", "Unknown256");
            RatingDefs.Add("PNEK", "Neck Roll");
            RatingDefs.Add("U258", "Unknown258");
            RatingDefs.Add("U259", "Unknown259");

            RatingDefs.Add("PSXP", "PortID");            
            RatingDefs.Add("PQBS", "QB Style");
            RatingDefs.Add("U263", "Unknown263");
            RatingDefs.Add("U264", "Unknown264");
            RatingDefs.Add("U265", "Unknown265");
            RatingDefs.Add("U266", "Unknown266");
            RatingDefs.Add("U267", "Unknown267");
            RatingDefs.Add("U268", "Unknown268");
            RatingDefs.Add("U269", "Unknown269");

            RatingDefs.Add("U270", "Unknown270");
            RatingDefs.Add("U271", "Unknown271");
            RatingDefs.Add("U272", "Unknown272");
            RatingDefs.Add("U273", "Unknown273");
            RatingDefs.Add("U274", "Unknown274");
            RatingDefs.Add("U275", "Unknown275");
            RatingDefs.Add("U276", "Unknown276");
            RatingDefs.Add("PVIS", "Visor");
            RatingDefs.Add("U278", "Unknown278");
            RatingDefs.Add("U279", "Unknown279");

            RatingDefs.Add("U280", "Unknown280");
            RatingDefs.Add("U281", "Unknown281");
            RatingDefs.Add("U282", "Unknown282");
            RatingDefs.Add("U283", "Unknown283");
            RatingDefs.Add("U284", "Unknown284");
            RatingDefs.Add("U285", "Unknown285");
            RatingDefs.Add("U286", "Unknown286");
            RatingDefs.Add("U287", "Unknown286");
            RatingDefs.Add("U288", "Unknown287");
            RatingDefs.Add("U289", "Unknown289");

            RatingDefs.Add("U290", "Unknown290");
            RatingDefs.Add("U291", "Unknown291");
            RatingDefs.Add("U292", "Unknown292");
            RatingDefs.Add("U293", "Unknown293");
            RatingDefs.Add("U294", "Unknown294");
            RatingDefs.Add("U295", "Unknown295");
            RatingDefs.Add("U296", "Unknown296");
            RatingDefs.Add("U297", "Unknown296");
            RatingDefs.Add("U298", "Unknown297");
            RatingDefs.Add("U299", "Unknown299");

            RatingDefs.Add("U300", "Unknown300");
            RatingDefs.Add("U301", "Unknown301");
            RatingDefs.Add("U302", "Unknown302");
            RatingDefs.Add("U303", "Unknown303");
            RatingDefs.Add("U304", "Unknown304");
            RatingDefs.Add("U305", "Unknown305");
            RatingDefs.Add("U306", "Unknown306");
            RatingDefs.Add("U307", "Unknown306");
            RatingDefs.Add("U308", "Unknown307");
            RatingDefs.Add("U309", "Unknown309");

            RatingDefs.Add("U310", "Unknown310");
            RatingDefs.Add("U311", "Unknown311");
            RatingDefs.Add("U312", "Unknown312");
            RatingDefs.Add("U313", "Unknown313");
            RatingDefs.Add("U314", "Unknown314");
            RatingDefs.Add("U315", "Unknown315");
            RatingDefs.Add("U316", "Unknown316");
            RatingDefs.Add("U317", "Unknown316");
            RatingDefs.Add("U318", "Unknown317");
            RatingDefs.Add("U319", "Unknown319");

            RatingDefs.Add("U320", "Unknown320");
            RatingDefs.Add("U321", "Unknown321");
            RatingDefs.Add("U322", "Unknown322");
            RatingDefs.Add("U323", "Unknown323");
            RatingDefs.Add("U324", "Unknown324");
            RatingDefs.Add("U325", "Unknown325");
            RatingDefs.Add("U326", "Unknown326");
            RatingDefs.Add("U327", "Unknown326");
            
        }

        public bool ReadDraftClass(string filename)
        {
            fb.Extract(filename);
            if (fb.FB_Type != Frostbyte_type.Draft)
                return false;

            if (fb.FB_Type == Frostbyte_type.Draft)
            {
                draftclassplayers.Clear();

                fb.binreader = new BinaryReader(File.Open(filename, FileMode.Open));

                for (int p = 0; p < fb.DataEntries; p++)
                {
                    DraftPlayer player = new DraftPlayer();
                    fb.binreader.BaseStream.Position = 70 + (p * 328);
                    player.ReadPlayer(fb.binreader);
                    draftclassplayers.Add(player);
                }

                fb.binreader.Close();
            }
            return true;
        }

        public void SaveDraftClass(string filename, FB draftfb)
        {
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

            binwriter.Write(draftfb.SerialVersion);
            binwriter.Write(draftfb.Serial);
            binwriter.Write(draftfb.DataType);
            binwriter.Write(draftfb.DataEntries);

            for (int p = 0; p < draftclassplayers.Count; p++)
            {
                draftclassplayers[p].WritePlayer(binwriter);
            }

            //padding
            for (int c = 0; c < 1640; c++)
                binwriter.Write((byte)0);

            binwriter.Close();     
        }

        public void ExportCSVHeaders(StreamWriter wText, bool desc)
        {
            StringBuilder hbuilder = new StringBuilder();
            hbuilder.Append("DRAFT,");
            hbuilder.Append("2019,");
            if (desc)
                hbuilder.Append("Yes");
            else hbuilder.Append("No");
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

        public void ImportCSVDraftClass(List<string> records, string[] fields)
        {
            for (int c = 0; c < records.Count(); c++)
            {
                if (c >= draftclassplayers.Count)
                    draftclassplayers.Add(new DraftPlayer());

                string[] csvrecord = records[c].Split(',');

                draftclassplayers[c].ImportCSVPlayer(fields, csvrecord, model);
            }
        }
    
    }

}
