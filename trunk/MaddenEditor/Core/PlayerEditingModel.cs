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
using System.Diagnostics;
using System.Text;
using System.IO;
using MaddenEditor.Core;
using MaddenEditor.Core.Record;
using MaddenEditor.Core.Record.Stats;
using MaddenEditor.Core.DatEditor;

namespace MaddenEditor.Core
{
    public class INJI
    {
        public int InjuryType = 0;
        public string InjuryDesc = "";
        public int InjurySeverity = 0;
        public int InjuryExtremity = 0;
        public int InjuryPart = 0;
    }
    
    public class PlayerEditingModel
    {
        private int currentPlayerIndex = 0;
        /** The current Team Filter */
        private string currentTeamFilter = null;
        /** The current position filter */
        private int currentPositionFilter = -1;
        /** If we are currently filtering for draft class */
        private bool currentDraftClassFilter = false;
        /** Reference to our EditorModel */
        private EditorModel model = null;
        /** Lists of hardcoded values */
        private IList<GenericRecord> helmetlist = null;
        private IList<GenericRecord> facemasklist = null;
        private IList<GenericRecord> shoelist = null;
        private IList<GenericRecord> glovelist = null;
        private IList<GenericRecord> wristbandlist = null;
        private IList<GenericRecord> sleeveslist = null;
        private IList<GenericRecord> anklelist = null;
        private IList<GenericRecord> elbowlist = null;
        private IList<GenericRecord> facemarkslist = null;
        private IList<GenericRecord> visorlist = null;
        private IList<GenericRecord> injurylist = null;
        private IList<GenericRecord> endplaylist = null;
        private IList<GenericRecord> qbstylelist = null;
        private IList<GenericRecord> archetypelist = null;
        public IList<GenericRecord> SidelineHeadGear = null;
        private IList<GenericRecord> SkinTone = null;

        private List<int> _playerlist = null;
        public List<int> playerlist
        {
            get { return _playerlist; }
        }
        public Dictionary<int, string> playernames = new Dictionary<int, string>();
        public Dictionary<int, string> duplicateplayers = new Dictionary<int, string>();
        public Dictionary<int, string> PlayerPositions = new Dictionary<int, string>();

        public int filterteam = -1;
        public int filterposition = -1;
        public bool filterdraft = false;

        public List<int> ProgRank = new List<int>();
        public List<decimal> AvgOVR = new List<decimal>();

        public PlayerEditingModel(EditorModel model)
        {
            this.model = model;

            #region Initialise the GenericRecord lists

            #region Helmets
            helmetlist = new List<GenericRecord>();

            #region 04-08
            if (model.FileVersion < MaddenFileVersion.Ver2019)
            {
                if (model.FileVersion == MaddenFileVersion.Ver2004)
                {
                    helmetlist.Add(new GenericRecord("Standard", 0));
                    helmetlist.Add(new GenericRecord("Adams", 1));
                    helmetlist.Add(new GenericRecord("Schutt", 2));
                    helmetlist.Add(new GenericRecord("Revolution", 3));
                }
                else
                {
                    helmetlist.Add(new GenericRecord("Style 1", 0));
                    helmetlist.Add(new GenericRecord("Style 2", 1));
                    helmetlist.Add(new GenericRecord("Style 3", 2));

                    if (model.FileVersion == MaddenFileVersion.Ver2005)
                        helmetlist.Add(new GenericRecord("Revolution", 3));
                    else if (model.FileVersion == MaddenFileVersion.Ver2006)
                    {
                        helmetlist.Add(new GenericRecord("Schutt DNA", 3));
                        helmetlist.Add(new GenericRecord("Revolution", 4));
                    }
                    else
                    {
                        helmetlist.Add(new GenericRecord("Style 4", 3));
                        helmetlist.Add(new GenericRecord("Revolution", 4));
                    }
                }
            }
            #endregion
            
            #region 19
            else if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                helmetlist.Add(new GenericRecord("Riddell VSR4", 0));
                helmetlist.Add(new GenericRecord("Schutt Air Advantage", 1));
                helmetlist.Add(new GenericRecord("Schutt Air XP Pro VTD", 2));
                helmetlist.Add(new GenericRecord("Vicis Zero1", 3));
                helmetlist.Add(new GenericRecord("Riddell VSR4", 4));                
                helmetlist.Add(new GenericRecord("Schutt Vengeance Pro", 5));
                helmetlist.Add(new GenericRecord("Riddell Revolution", 6));
                helmetlist.Add(new GenericRecord("Riddell Revolution Speed", 7));
                helmetlist.Add(new GenericRecord("Riddell Speed Flex", 8));
                helmetlist.Add(new GenericRecord("Riddell 360", 9));
                helmetlist.Add(new GenericRecord("Xenith X2E", 10));                
                helmetlist.Add(new GenericRecord("Riddell TK", 11));
                helmetlist.Add(new GenericRecord("Schutt Air Advantage", 12));
                helmetlist.Add(new GenericRecord("Vengeance Z10", 13));
                helmetlist.Add(new GenericRecord("Schutt F7", 14));
            }
            #endregion

            #endregion

            #region Facemask
            facemasklist = new List<GenericRecord>();
            #region 04-08
            if (model.FileVersion < MaddenFileVersion.Ver2019)
            {
                facemasklist.Add(new GenericRecord("2-Bar", 0));
                facemasklist.Add(new GenericRecord("3-Bar", 1));
                facemasklist.Add(new GenericRecord("Half Cage", 2));
                facemasklist.Add(new GenericRecord("Full Cage", 3));
                facemasklist.Add(new GenericRecord("1-Bar", 4));
                facemasklist.Add(new GenericRecord("2-Bar Thin", 5));
                facemasklist.Add(new GenericRecord("3-Bar QB", 6));
                facemasklist.Add(new GenericRecord("2-Bar RB", 7));
                facemasklist.Add(new GenericRecord("3-Bar RB", 8));
                facemasklist.Add(new GenericRecord("RB Robot", 9));
                facemasklist.Add(new GenericRecord("RB Bull", 10));
                facemasklist.Add(new GenericRecord("Revo G2B", 11));
                facemasklist.Add(new GenericRecord("Revo G3BDU", 12));
                facemasklist.Add(new GenericRecord("Revo G2EG", 13));
            }
            #endregion
       
            #region 19
            else if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                facemasklist.Add(new GenericRecord("2 Bar", 0));
                facemasklist.Add(new GenericRecord("2 Bar Single", 1));
                facemasklist.Add(new GenericRecord("3 Bar", 2));           
                facemasklist.Add(new GenericRecord("3 Bar Single", 3));
                facemasklist.Add(new GenericRecord("3 Bar QB", 4));
                facemasklist.Add(new GenericRecord("3 Bar Single", 5));
                facemasklist.Add(new GenericRecord("3 Bar RB", 6));
                facemasklist.Add(new GenericRecord("3 Bar RB Jagged", 7));        
                facemasklist.Add(new GenericRecord("BullRB", 8));          
                facemasklist.Add(new GenericRecord("Robot", 9));
          
                facemasklist.Add(new GenericRecord("Robot RB", 10));        
                facemasklist.Add(new GenericRecord("Full Cage Robot", 11));
                facemasklist.Add(new GenericRecord("Full Cage Hook", 12));
                facemasklist.Add(new GenericRecord("Full Cage Single", 13));
                facemasklist.Add(new GenericRecord("Full Cage 2", 14));
                facemasklist.Add(new GenericRecord("Half Cage", 15));
                facemasklist.Add(new GenericRecord("Half Cage 2", 16));
                facemasklist.Add(new GenericRecord("Kicker", 17));
                facemasklist.Add(new GenericRecord("Bulldog", 18));
                facemasklist.Add(new GenericRecord("Revo 2 Bar", 19));

                facemasklist.Add(new GenericRecord("2 Bar Single", 20));
                facemasklist.Add(new GenericRecord("Revo 3 Bar Single", 21));
                facemasklist.Add(new GenericRecord("Revo 3 Bar RB", 22));
                facemasklist.Add(new GenericRecord("Revo 3 Bar LB", 23));
                facemasklist.Add(new GenericRecord("Revo Robot", 24));
                facemasklist.Add(new GenericRecord("Revo Robot 2", 25));
                facemasklist.Add(new GenericRecord("Revo Half Cage", 26));
                facemasklist.Add(new GenericRecord("Revo Full Cage", 27));
                facemasklist.Add(new GenericRecord("Revo Full Cage 2", 28));
                facemasklist.Add(new GenericRecord("Revo Full Cage 3", 29));
                
                facemasklist.Add(new GenericRecord("Revo Kicker", 30));
                facemasklist.Add(new GenericRecord("Revo Speed 2 Bar", 31));
                facemasklist.Add(new GenericRecord("Revo Speed 2 Bar Single", 32)); 
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar", 33));
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar Single", 34));
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar Straight", 35));
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar LB", 36));
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar LB Straight", 37));
                facemasklist.Add(new GenericRecord("Revo Speed Robot", 38));
                facemasklist.Add(new GenericRecord("Revo Speed Robot 2", 39));

                facemasklist.Add(new GenericRecord("Revo Speed Robot RB", 40));
                facemasklist.Add(new GenericRecord("Revo Speed Full Cage", 41));
                facemasklist.Add(new GenericRecord("Revo Speed Full Cage 2", 42));
                facemasklist.Add(new GenericRecord("Revo Speed Cage", 43));
                facemasklist.Add(new GenericRecord("Revo Speed Grid", 44));
                facemasklist.Add(new GenericRecord("Revo Speed Kicker", 45));
                facemasklist.Add(new GenericRecord("Xenith 2 Bar", 46));
                facemasklist.Add(new GenericRecord("Xenith Robot", 47));
                facemasklist.Add(new GenericRecord("Xenith 3 Bar RB", 48));
                facemasklist.Add(new GenericRecord("Xenith Robot RB", 49));

                facemasklist.Add(new GenericRecord("Xenith Full Cage", 50));
                facemasklist.Add(new GenericRecord("Vengeance 2 Bar", 51));
                facemasklist.Add(new GenericRecord("Vengeance Robot", 52));
                facemasklist.Add(new GenericRecord("Vengeance Robot RB", 53));
                facemasklist.Add(new GenericRecord("Vengeance 3 Bar RB", 54));
                facemasklist.Add(new GenericRecord("Vengeance Full Cage", 55));
                facemasklist.Add(new GenericRecord("Speed Flex 2 Bar", 56));
                facemasklist.Add(new GenericRecord("Speed Flex Robot", 57));
                facemasklist.Add(new GenericRecord("Speed Flex Robot RB", 58));
                facemasklist.Add(new GenericRecord("Speed Flex 3 Bar RB", 59));

                facemasklist.Add(new GenericRecord("Speed Flex 3 Bar LB", 60));
                facemasklist.Add(new GenericRecord("Speed Flex Full Cage", 61));
                facemasklist.Add(new GenericRecord("360 Robot", 62));
                facemasklist.Add(new GenericRecord("360 Robot 2", 63));
                facemasklist.Add(new GenericRecord("360 3 Bar LB", 64));
                facemasklist.Add(new GenericRecord("360 Full Cage", 65));
                facemasklist.Add(new GenericRecord("None", 66));
                facemasklist.Add(new GenericRecord("Vintage One Bar", 67));
                facemasklist.Add(new GenericRecord("Vintage Two Bar", 68));
                facemasklist.Add(new GenericRecord("Vintage Standard", 69));

                facemasklist.Add(new GenericRecord("Vintage Long", 70));
                facemasklist.Add(new GenericRecord("Vicis Robot", 71));
                facemasklist.Add(new GenericRecord("Vicis Robot RB", 72));
                facemasklist.Add(new GenericRecord("Vicis 3 Bar LB", 73));
                facemasklist.Add(new GenericRecord("Vicis 2 Bar", 74));
                facemasklist.Add(new GenericRecord("Vicis Full Cage", 75));
                facemasklist.Add(new GenericRecord("Vicis 3 Bar RB", 76));
                facemasklist.Add(new GenericRecord("Vicis Bull RB", 77));
                facemasklist.Add(new GenericRecord("Vicis 3 Bar", 78));
                facemasklist.Add(new GenericRecord("Xenith Predator", 79));

                facemasklist.Add(new GenericRecord("Speed Flex Cage", 80));
                facemasklist.Add(new GenericRecord("Speed Flex 2 Bar QB", 81));
                facemasklist.Add(new GenericRecord("Speed Flex 2 Bar Single", 82));
                facemasklist.Add(new GenericRecord("Speed Flex Robot RB Jag", 83));
                facemasklist.Add(new GenericRecord("Speed Flex 3 Bar Single", 84));
                facemasklist.Add(new GenericRecord("Speed Flex 3 Bar QB", 85));
                facemasklist.Add(new GenericRecord("Revo Speed 3 Bar QB", 86));
                facemasklist.Add(new GenericRecord("Vengeance Full Cage Bulldog", 87));
                facemasklist.Add(new GenericRecord("Vengeance Z10 Robot RB", 88));
                facemasklist.Add(new GenericRecord("Revo Speed 808", 89));

                facemasklist.Add(new GenericRecord("Revo Speed 2 Bar WR", 90));
                facemasklist.Add(new GenericRecord("Revo Speed Full Cage Hook", 91));
                facemasklist.Add(new GenericRecord("Standard 2 Bar WR", 92));
                facemasklist.Add(new GenericRecord("Vengeance Z10 Robot", 93));
                facemasklist.Add(new GenericRecord("Vengeance Z10 2 Bar", 94));
                facemasklist.Add(new GenericRecord("Vengeance Z10 3 Bar LB", 95));
                facemasklist.Add(new GenericRecord("Vengeance Z10 Cage", 96));
                facemasklist.Add(new GenericRecord("Xenith Prism", 97));
                facemasklist.Add(new GenericRecord("Vengeance Kicker", 98));
                facemasklist.Add(new GenericRecord("Schutt F7 2 Bar", 99));

                facemasklist.Add(new GenericRecord("Schutt F7 3 Bar", 100));
                facemasklist.Add(new GenericRecord("Schutt F7 Robot", 101));
                facemasklist.Add(new GenericRecord("Schutt F7 Robot RB", 102));
                facemasklist.Add(new GenericRecord("Schutt F7 3 Bar RB", 103));
                facemasklist.Add(new GenericRecord("Schutt F7 3 Full Cage", 104));
            }
            #endregion
            #endregion

            #region Shoes
            shoelist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                shoelist.Add(new GenericRecord("Adidas Freak X Carbon", 0));
                shoelist.Add(new GenericRecord("Adidas Adizero 7.0", 1));
                shoelist.Add(new GenericRecord("Nike Alpha Pro 2 3/4 TD", 2));
                shoelist.Add(new GenericRecord("Nike Vapor Untouchable", 3));
                shoelist.Add(new GenericRecord("Nike Vapor Carbon Elite TD", 4));
                shoelist.Add(new GenericRecord("Under Armour Spotlight", 5));
                shoelist.Add(new GenericRecord("Nike Alpha Menace Elite", 6));
                shoelist.Add(new GenericRecord("Nike Vapor Untouchable 2", 7));
                shoelist.Add(new GenericRecord("Nike Vintage 70s-80s", 8));
                shoelist.Add(new GenericRecord("Pintos", 9));
                shoelist.Add(new GenericRecord("Nike Lunar Beast", 10));
                shoelist.Add(new GenericRecord("Nike Zoom Code Elite", 11));
                shoelist.Add(new GenericRecord("Under Armour Highlight", 12));
                shoelist.Add(new GenericRecord("Under Armour Exclusive", 13));
                shoelist.Add(new GenericRecord("Player Exclusive Retro", 14));
                shoelist.Add(new GenericRecord("Nike Force Savage Elite", 15));
                shoelist.Add(new GenericRecord("Nike Apha Menace Pro", 16));
                shoelist.Add(new GenericRecord("Nike Force Savage Pro", 17));
                shoelist.Add(new GenericRecord("Nike Vapor Speed 3", 18));
                shoelist.Add(new GenericRecord("Nike Vapor Untouchable Pro 3", 19));
                shoelist.Add(new GenericRecord("Nike Field General", 20));
                shoelist.Add(new GenericRecord("Nike Vapor Untouchable Pro", 21));
                shoelist.Add(new GenericRecord("99 Club Force Savage Elite", 22));
                shoelist.Add(new GenericRecord("99 Club Alpha Menace Pro", 23));
            }
            else
            {                
                shoelist.Add(new GenericRecord("White",0));
                shoelist.Add(new GenericRecord("White/White Tape", 1));
                shoelist.Add(new GenericRecord("White/Black Tape", 2));
                shoelist.Add(new GenericRecord("White/Team Tape", 3));
                shoelist.Add(new GenericRecord("Team Shoes", 4));
                shoelist.Add(new GenericRecord("Team/White Tape", 5));
                shoelist.Add(new GenericRecord("Team/Black Tape", 6));
                shoelist.Add(new GenericRecord("Team/Team Tape", 7));
                shoelist.Add(new GenericRecord("Black", 8));
                shoelist.Add(new GenericRecord("Black/White Tape", 9));
                shoelist.Add(new GenericRecord("Black/Black Tape", 10));
                shoelist.Add(new GenericRecord("Black/Team Tape", 11));
            }
            #endregion

            #region Gloves
            glovelist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                glovelist.Add(new GenericRecord("None", 0));
                glovelist.Add(new GenericRecord("Nike Vapor Jet White", 1));
                glovelist.Add(new GenericRecord("Nike Vapor Jet Black", 2));
                glovelist.Add(new GenericRecord("Nike Vapor Jet Primary", 3));
                glovelist.Add(new GenericRecord("Adidas 5 Star 7 White", 4));
                glovelist.Add(new GenericRecord("Adidas 5 Star 7 Black", 5));
                glovelist.Add(new GenericRecord("Adidas 5 Star 7 Primary", 6));
                glovelist.Add(new GenericRecord("Nike Superbad 3 White", 7));
                glovelist.Add(new GenericRecord("Nike Superbad 3 Black", 8));
                glovelist.Add(new GenericRecord("Nike Superbad 3 Primary", 9));
                glovelist.Add(new GenericRecord("Nike Vapor Knit White", 10));
                glovelist.Add(new GenericRecord("Nike Vapor Knit Black", 11));
                glovelist.Add(new GenericRecord("Nike Vapor Knit Primary", 12));
                glovelist.Add(new GenericRecord("Nike Hyperbeast White", 13));
                glovelist.Add(new GenericRecord("Nike Hyperbeast Black", 14));
                glovelist.Add(new GenericRecord("Taped Fingers White", 15));
                glovelist.Add(new GenericRecord("Taped Fingers Black", 16));
                glovelist.Add(new GenericRecord("Taped Hands", 17));
                glovelist.Add(new GenericRecord("Taped Fingers Team", 18));
                glovelist.Add(new GenericRecord("Taped Hands Max", 19));
                glovelist.Add(new GenericRecord("Taped Hands Combo", 20));
                glovelist.Add(new GenericRecord("Under Armour Spotlight Primary", 21));
                glovelist.Add(new GenericRecord("Under Armour F6 Primary", 22));
                glovelist.Add(new GenericRecord("Nike Vapor Jet Secondary", 23));
                glovelist.Add(new GenericRecord("Adidas 5 Star 7 Secondary", 24));
                glovelist.Add(new GenericRecord("Nike Superbad 3 Secondary", 25));
                glovelist.Add(new GenericRecord("Nike Vapor Knit Secondary", 26));
                glovelist.Add(new GenericRecord("Under Armour Spotlight Secondary", 21));
                glovelist.Add(new GenericRecord("Under Armour F6 Secondary", 28));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 4 Black", 29));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 4 White", 30));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 4 Primary", 31));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 4 Secondary", 32));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 5 Black", 33));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 5 White", 34));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 5 Primary", 35));
                glovelist.Add(new GenericRecord("Nike Vapor Jet 5 Secondary", 36));
                glovelist.Add(new GenericRecord("Nike Superbad 5 Black", 37));
                glovelist.Add(new GenericRecord("Nike Superbad 5 White", 38));
                glovelist.Add(new GenericRecord("Nike Superbad 5 Primary", 39));
                glovelist.Add(new GenericRecord("Nike Superbad 5 Secondary", 40));
                glovelist.Add(new GenericRecord("Nike Vapor Knit 2 Black", 41));
                glovelist.Add(new GenericRecord("Nike Vapor Knit 2 White", 42));
                glovelist.Add(new GenericRecord("Nike Vapor Knit 2 Primary", 43));
                glovelist.Add(new GenericRecord("Nike Vapor Knit 2 Secondary", 44));
                glovelist.Add(new GenericRecord("Player Exclusive White", 45));
                glovelist.Add(new GenericRecord("Player Exclusive Black", 46));
                glovelist.Add(new GenericRecord("Player Exclusive Primary", 47));
                glovelist.Add(new GenericRecord("Player Exclusive Secondary", 48));
                glovelist.Add(new GenericRecord("Under Armour Spotlight White", 49));
                glovelist.Add(new GenericRecord("Under Armour Spotlight Black", 50));
                glovelist.Add(new GenericRecord("Under Armour F6 White", 51));
                glovelist.Add(new GenericRecord("Under Armour F6 Black", 52));
                glovelist.Add(new GenericRecord("No Hand", 53));
                glovelist.Add(new GenericRecord("99 Club Vapor Jet 5", 54));
            }
            else
            {
                glovelist.Add(new GenericRecord("Normal", 0));
                glovelist.Add(new GenericRecord("Taped", 1));
                glovelist.Add(new GenericRecord("Black", 2));
                glovelist.Add(new GenericRecord("White", 3));
                glovelist.Add(new GenericRecord("Team", 4));
                glovelist.Add(new GenericRecord("White RB", 5));
                glovelist.Add(new GenericRecord("Black RB", 6));
                glovelist.Add(new GenericRecord("Team RB", 7));
            }
            #endregion
            
            #region Wristband
            wristbandlist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                wristbandlist.Add(new GenericRecord("None", 0));
                wristbandlist.Add(new GenericRecord("Wristband White", 1));
                wristbandlist.Add(new GenericRecord("Wristband Black", 2));
                wristbandlist.Add(new GenericRecord("Wristband Team Color", 3));
                wristbandlist.Add(new GenericRecord("Double Wristband White", 4));
                wristbandlist.Add(new GenericRecord("Double Wristband Black", 5));
                wristbandlist.Add(new GenericRecord("Double Wristband Team", 6));
                wristbandlist.Add(new GenericRecord("Coach White", 7));
                wristbandlist.Add(new GenericRecord("Coach Black", 8));
                wristbandlist.Add(new GenericRecord("Coach Team Color", 9));
                wristbandlist.Add(new GenericRecord("Taped Wrists Lite", 10));
                wristbandlist.Add(new GenericRecord("Taped Wrist Normal", 11));
                wristbandlist.Add(new GenericRecord("Taped Wrist Heavy", 12));
                wristbandlist.Add(new GenericRecord("Heavy White Taped Glove", 13));
                wristbandlist.Add(new GenericRecord("Normal White Taped Glove", 14));
                wristbandlist.Add(new GenericRecord("Heavy Black Taped Glove", 15));
                wristbandlist.Add(new GenericRecord("Normal Black Taped Glove", 16));
                wristbandlist.Add(new GenericRecord("Wrist Brace", 17));
                wristbandlist.Add(new GenericRecord("Wristband Secondary", 18));
                wristbandlist.Add(new GenericRecord("Double Wristband Secondary", 19));
                wristbandlist.Add(new GenericRecord("Taped Wrists Lite Black", 20));
                wristbandlist.Add(new GenericRecord("Taped Wrists Normal Black", 21));
                wristbandlist.Add(new GenericRecord("Taped Wrist Max Black", 22));
                wristbandlist.Add(new GenericRecord("Taped Wrists Lite Primary", 23));
                wristbandlist.Add(new GenericRecord("Taped Wrist Normal Primary", 24));
                wristbandlist.Add(new GenericRecord("Taped Wrist Max Primary", 25));
                wristbandlist.Add(new GenericRecord("Taped Wrists Lite Secondary", 26));
                wristbandlist.Add(new GenericRecord("Taped Wrists Normal Secondary", 27));
                wristbandlist.Add(new GenericRecord("Taped Wrist Max Secondary", 28));
            }
            else
            {
                wristbandlist.Add(new GenericRecord("Normal",0));
                wristbandlist.Add(new GenericRecord("QB Wrist",1));
                wristbandlist.Add(new GenericRecord("White Wrist",2));
                wristbandlist.Add(new GenericRecord("Black Wrist",3));
                wristbandlist.Add(new GenericRecord("Team Wrist",4));
                wristbandlist.Add(new GenericRecord("White Double",5));
                wristbandlist.Add(new GenericRecord("Black Double",6));
                wristbandlist.Add(new GenericRecord("Team Double",7));
            }


            #endregion

            #region Sleeves
            sleeveslist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                sleeveslist.Add(new GenericRecord("None", 0));
                sleeveslist.Add(new GenericRecord("Full Sleeve White", 1));
                sleeveslist.Add(new GenericRecord("Full Sleeve Black", 2));
                sleeveslist.Add(new GenericRecord("Full Sleeve Team Color", 3));
                sleeveslist.Add(new GenericRecord("Full Sleeve Secondary", 4));
                sleeveslist.Add(new GenericRecord("1/2 Sleeve White", 5));
                sleeveslist.Add(new GenericRecord("1/2 Sleeve Black", 6));
                sleeveslist.Add(new GenericRecord("1/2 Sleeve Team", 7));
                sleeveslist.Add(new GenericRecord("Undershirt White", 8));
                sleeveslist.Add(new GenericRecord("Undershirt Black", 9));
                sleeveslist.Add(new GenericRecord("Undershirt Team Color", 10));
                sleeveslist.Add(new GenericRecord("1/4 Sleeve White", 11));
                sleeveslist.Add(new GenericRecord("1/4 Sleeve Black", 12));
                sleeveslist.Add(new GenericRecord("1/4 Sleeve Team", 13));
                sleeveslist.Add(new GenericRecord("1/4 Sleeve Secondary", 14));
                sleeveslist.Add(new GenericRecord("Shooter Sleeve White", 15));
                sleeveslist.Add(new GenericRecord("Shooter Sleeve Black", 16));
                sleeveslist.Add(new GenericRecord("Shooter Sleeve Team", 17));
                sleeveslist.Add(new GenericRecord("Shooter Sleeve Secondary", 18));
            }
            else
            {
                sleeveslist.Add(new GenericRecord("None", 0));
                sleeveslist.Add(new GenericRecord("Black", 1));
                sleeveslist.Add(new GenericRecord("White", 2));
                sleeveslist.Add(new GenericRecord("Team", 3));
                sleeveslist.Add(new GenericRecord("White Half", 4));
                sleeveslist.Add(new GenericRecord("Black Half", 5));
                sleeveslist.Add(new GenericRecord("Team Half", 6));
            }
            #endregion

            #region Ankles
            anklelist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                anklelist.Add(new GenericRecord("None", 0));
                anklelist.Add(new GenericRecord("White Half Splat", 1));
                anklelist.Add(new GenericRecord("White Full Splat", 2));
                anklelist.Add(new GenericRecord("Black Half Splat", 3));
                anklelist.Add(new GenericRecord("Black Full Splat", 4));
            }
            else
            {
                anklelist.Add(new GenericRecord("Normal", 0));
                anklelist.Add(new GenericRecord("White", 1));
                anklelist.Add(new GenericRecord("Black", 2));
                anklelist.Add(new GenericRecord("Team", 3));
            }
            #endregion
         
            #region Elbow
            elbowlist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                elbowlist.Add(new GenericRecord("None", 0));
                elbowlist.Add(new GenericRecord("Padding White", 1));
                elbowlist.Add(new GenericRecord("Padding Black", 2));
                elbowlist.Add(new GenericRecord("Padding Team", 3));
                elbowlist.Add(new GenericRecord("Padding Team White", 4));
                elbowlist.Add(new GenericRecord("Padding Team Black", 5));
                elbowlist.Add(new GenericRecord("Rubber Pad", 6));
                elbowlist.Add(new GenericRecord("Full Sweat White", 7));
                elbowlist.Add(new GenericRecord("Med. Band White", 8));
                elbowlist.Add(new GenericRecord("Thin Band White", 9));
                elbowlist.Add(new GenericRecord("Full Sweat Black", 10));
                elbowlist.Add(new GenericRecord("Med Band Black", 11));
                elbowlist.Add(new GenericRecord("Thin Band Black", 12));
                elbowlist.Add(new GenericRecord("Full Sweat Team", 13));
                elbowlist.Add(new GenericRecord("Med. Band Team", 14));
                elbowlist.Add(new GenericRecord("Thin Band Team", 15));
                elbowlist.Add(new GenericRecord("Elbow Brace Team", 16));
                //elbowlist.Add(new GenericRecord("Full Sweat Team", 17));
                elbowlist.Add(new GenericRecord("Full Sweat Secondary", 18));
                elbowlist.Add(new GenericRecord("Med. Band Seconday", 19));
                elbowlist.Add(new GenericRecord("Thin Band Secondary", 20));
                elbowlist.Add(new GenericRecord("Arm Wrap", 21));
            }
            else
            {
                elbowlist.Add(new GenericRecord("Normal",0));
                elbowlist.Add(new GenericRecord("Turf Tape",1));
                elbowlist.Add(new GenericRecord("Rubber Pad",2));
                elbowlist.Add(new GenericRecord("Black Pad",3));
                elbowlist.Add(new GenericRecord("White Pad",4));
                elbowlist.Add(new GenericRecord("Black Team Pad",5));
                elbowlist.Add(new GenericRecord("White Team Pad",6));
                elbowlist.Add(new GenericRecord("Black Wrist",7));
                elbowlist.Add(new GenericRecord("White Wrist",8));
                elbowlist.Add(new GenericRecord("Team Wrist",9));
            }

            #endregion
            
            #region Face Marks
            facemarkslist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                facemarkslist.Add(new GenericRecord("None", 0));
                facemarkslist.Add(new GenericRecord("Nose Tape", 1));
                facemarkslist.Add(new GenericRecord("Eye Paint", 2));
                facemarkslist.Add(new GenericRecord("Eye Tape", 3));
                facemarkslist.Add(new GenericRecord("Eye Paint 2", 4));
                facemarkslist.Add(new GenericRecord("Nose Tape + Eye Paint", 5));
                facemarkslist.Add(new GenericRecord("Nose Tape + Eye Tape", 6));
                facemarkslist.Add(new GenericRecord("Eye Paint Cross", 7));
                facemarkslist.Add(new GenericRecord("Eye Paint 3", 8));
                facemarkslist.Add(new GenericRecord("Eye Tape Left", 9));
                facemarkslist.Add(new GenericRecord("Eye Tape Right", 10));
            }
            else
            {                
                facemarkslist.Add(new GenericRecord("None", 0));
                facemarkslist.Add(new GenericRecord("Black", 1));
            }
            #endregion

            #region Visor
            visorlist = new List<GenericRecord>();
            if (model.FileVersion < MaddenFileVersion.Ver2019)
            {
                visorlist.Add(new GenericRecord("None", 0));
                visorlist.Add(new GenericRecord("Clear", 1));
                visorlist.Add(new GenericRecord("Dark", 2));
                visorlist.Add(new GenericRecord("Amber", 3));
            }
            else
            {
                visorlist.Add(new GenericRecord("None", 0));
                visorlist.Add(new GenericRecord("Dark", 1));
                visorlist.Add(new GenericRecord("Smoke", 2));
                visorlist.Add(new GenericRecord("Clear", 3));
            }
            #endregion

            #region Archetypes
            archetypelist = new List<GenericRecord>();
            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                archetypelist.Add(new GenericRecord("Field General", 0));
                archetypelist.Add(new GenericRecord("Strong Arm", 1));
                archetypelist.Add(new GenericRecord("West Coast", 2));
                archetypelist.Add(new GenericRecord("Scrambler", 3));
                archetypelist.Add(new GenericRecord("Power Back", 4));
                archetypelist.Add(new GenericRecord("Elusive Back", 5));
                archetypelist.Add(new GenericRecord("Receiving Back", 6));
                archetypelist.Add(new GenericRecord("Blocking FB", 7));
                archetypelist.Add(new GenericRecord("Utility FB", 8));
                archetypelist.Add(new GenericRecord("Deep Threat", 9));
                archetypelist.Add(new GenericRecord("Possession", 10));
                archetypelist.Add(new GenericRecord("Redzone Threat", 11));
                archetypelist.Add(new GenericRecord("Slot", 12));
                archetypelist.Add(new GenericRecord("Blocking TE", 13));
                archetypelist.Add(new GenericRecord("Vertical Threat TE", 14));
                archetypelist.Add(new GenericRecord("Possession TE", 15));
                archetypelist.Add(new GenericRecord("Pass Prot. C", 16));
                archetypelist.Add(new GenericRecord("Power C", 17));
                archetypelist.Add(new GenericRecord("Agile C", 18));
                archetypelist.Add(new GenericRecord("Pass Prot. T", 19));
                archetypelist.Add(new GenericRecord("Power T", 20));
                archetypelist.Add(new GenericRecord("Agile T", 21));
                archetypelist.Add(new GenericRecord("Pass Prot. G", 22));
                archetypelist.Add(new GenericRecord("Power G", 23));
                archetypelist.Add(new GenericRecord("Agile G", 24));
                archetypelist.Add(new GenericRecord("Speed Rusher DE", 25));
                archetypelist.Add(new GenericRecord("Power Rusher DE", 26));
                archetypelist.Add(new GenericRecord("Run Stopper DE", 27));
                archetypelist.Add(new GenericRecord("Speed Rusher DT", 28));
                archetypelist.Add(new GenericRecord("Run Stopper DT", 29));
                archetypelist.Add(new GenericRecord("Power Rusher DT ", 30));
                archetypelist.Add(new GenericRecord("Speed Rusher LB", 31));
                archetypelist.Add(new GenericRecord("Power Rusher LB", 32));
                archetypelist.Add(new GenericRecord("Run Stopper LB", 33));
                archetypelist.Add(new GenericRecord("Pass Coverage LB", 34));
                archetypelist.Add(new GenericRecord("Field General MLB", 35));
                archetypelist.Add(new GenericRecord("Pass Coverage MLB", 36));
                archetypelist.Add(new GenericRecord("Run Stopper MLB", 37));
                archetypelist.Add(new GenericRecord("Man to Man CB", 38));
                archetypelist.Add(new GenericRecord("Slot CB", 39));
                archetypelist.Add(new GenericRecord("Zone CB", 40));
                archetypelist.Add(new GenericRecord("Zone S", 41));
                archetypelist.Add(new GenericRecord("Hybrid S", 42));
                archetypelist.Add(new GenericRecord("Run Support S", 43));
                archetypelist.Add(new GenericRecord("Accurate K", 44));
                archetypelist.Add(new GenericRecord("Power K", 45));
            }

            #endregion

            #region Sideline Headgear
            SidelineHeadGear = new List<GenericRecord>();
            SidelineHeadGear.Add(new GenericRecord("Helmet", 0));
            SidelineHeadGear.Add(new GenericRecord("Helmet Partial", 1));
            SidelineHeadGear.Add(new GenericRecord("Hat Forward", 2));
            SidelineHeadGear.Add(new GenericRecord("Hat Backward", 3));
            SidelineHeadGear.Add(new GenericRecord("Visor", 4));
            SidelineHeadGear.Add(new GenericRecord("Hat Spec.", 5));
            SidelineHeadGear.Add(new GenericRecord("None", 6));

            #endregion

            #region Skintone
            SkinTone = new List<GenericRecord>();
            SkinTone.Add(new GenericRecord("Lightest", 0));
            SkinTone.Add(new GenericRecord("SkinTone1", 1));
            SkinTone.Add(new GenericRecord("SkinTone2", 2));
            SkinTone.Add(new GenericRecord("SkinTone3", 3));
            SkinTone.Add(new GenericRecord("SkinTone4", 4));
            SkinTone.Add(new GenericRecord("SkinTone5", 5));
            SkinTone.Add(new GenericRecord("Darkest", 6));


            #endregion

            PlayerPositions.Clear();
            PlayerPositions.Add(0, "QB");
            PlayerPositions.Add(1, "HB");
            PlayerPositions.Add(2, "FB");
            PlayerPositions.Add(3, "WR");
            PlayerPositions.Add(4, "TE");
            PlayerPositions.Add(5, "LT");
            PlayerPositions.Add(6, "LG");
            PlayerPositions.Add(7, "C");
            PlayerPositions.Add(8, "RG");
            PlayerPositions.Add(9, "RT");
            PlayerPositions.Add(10, "LE");
            PlayerPositions.Add(11, "RE");
            PlayerPositions.Add(12, "DT");
            PlayerPositions.Add(13, "LOLB");
            PlayerPositions.Add(14, "MLB");
            PlayerPositions.Add(15, "ROLB");
            PlayerPositions.Add(16, "CB");
            PlayerPositions.Add(17, "FS");
            PlayerPositions.Add(18, "SS");
            PlayerPositions.Add(19, "K");
            PlayerPositions.Add(20, "P");
           
            #endregion

            if (model.FileVersion == MaddenFileVersion.Ver2019)
            {
                ImportInjuries();
                ImportEndPlay();
                ImportQBStyle();
            }
            
        }

        public IList<GenericRecord> HelmetStyleList
        {
            get
            {
                return helmetlist;
            }
        }

        public IList<GenericRecord> ShoeList
        {
            get { return shoelist; }
        }

        public IList<GenericRecord> FacemaskList
        {
            get { return facemasklist; }
        }

        public IList<GenericRecord> GloveList
        {
            get { return glovelist; }
        }

        public IList<GenericRecord> WristBandList
        {
            get { return wristbandlist;}
        }

        public IList<GenericRecord> SleeveList
        {
            get { return sleeveslist; }
        }

        public IList<GenericRecord> ElbowList
        {
            get { return elbowlist; }
        }

        public IList<GenericRecord> AnkleList
        {
            get { return anklelist; }
        }

        public IList<GenericRecord> FaceMarkList
        {
            get { return facemarkslist; }
        }

        public IList<GenericRecord> VisorList
        {
            get { return visorlist; }
        }

        public IList<GenericRecord> InjuryList
        {
            get { return injurylist; }
        }

        public IList<GenericRecord> EndPlayList
        {
            get { return endplaylist; }
        }

        public IList<GenericRecord> QBStyleList
        {
            get { return qbstylelist; }
        }

        public IList<GenericRecord> ArchetypeList
        {
            get { return archetypelist; }
        }
        
        public string GetShoe(int id)
        {
            foreach (GenericRecord rec in ShoeList)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetShoe(string name)
        {
            foreach (GenericRecord rec in ShoeList)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetSleeve(int id)
        {
            foreach (GenericRecord rec in sleeveslist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetSleeve(string name)
        {
            foreach (GenericRecord rec in sleeveslist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        } 
        
        public string GetHelmet(int id)
        {
            foreach (GenericRecord rec in helmetlist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";      
        }
        public int GetHelmet(string name)
        {
            foreach (GenericRecord rec in helmetlist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
        
        public string GetFaceMask(int id)
        {
            foreach (GenericRecord rec in facemasklist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetFaceMask(string name)
        {
            foreach (GenericRecord rec in facemasklist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
                
        public string GetGloves(int id)
        {
            foreach (GenericRecord rec in glovelist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetGloves(string name)
        {
            foreach (GenericRecord rec in glovelist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
                
        public string GetWrist(int id)
        {
            foreach (GenericRecord rec in wristbandlist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetWrist(string name)
        {
            foreach (GenericRecord rec in wristbandlist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
              
        public string GetElbow(int id)
        {
            foreach (GenericRecord rec in elbowlist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetElbow(string name)
        {
            foreach (GenericRecord rec in elbowlist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
        
        public string GetAnkle(int id)
        {
            foreach (GenericRecord rec in anklelist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetAnkle(string name)
        {
            foreach (GenericRecord rec in anklelist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
        
        public string GetFaceMark(int id)
        {
            foreach (GenericRecord rec in facemarkslist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetFaceMark(string name)
        {
            foreach (GenericRecord rec in facemarkslist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetVisor(int id)
        {
            foreach (GenericRecord rec in visorlist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }        
        public int GetVisor(string name)
        {
            foreach (GenericRecord rec in visorlist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetInjury(int id)
        {
            foreach (GenericRecord rec in injurylist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetInjury(string name)
        {
            foreach (GenericRecord rec in injurylist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetEndPlay(int id)
        {
            foreach (GenericRecord rec in endplaylist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetEndPlay(string name)
        {
            foreach (GenericRecord rec in endplaylist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetQBStyle(int id)
        {
            foreach (GenericRecord rec in qbstylelist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetQBStyle(string name)
        {
            foreach (GenericRecord rec in qbstylelist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }

        public string GetArchetype(int id)
        {
            foreach (GenericRecord rec in archetypelist)
                if (rec.Id == id)
                    return rec.ToString();
            return "";
        }
        public int GetArchetype(string name)
        {
            foreach (GenericRecord rec in archetypelist)
                if (name == rec.ToString())
                    return rec.Id;
            return -1;
        }
               
        public void ImportQBStyle()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019QBSS.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "QBSS" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        qbstylelist = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string desc = "";
                            string pn = "";

                            if (fields[0] == "ABNK")
                                pn = entry[0];
                            if (fields[1] == "EDIS")
                                desc = entry[1];
                            if (fields[2] == "ENUV")
                                id = Convert.ToInt32(entry[2]);

                            pn = pn.Replace("PasserStyle_", "");
                            string s = desc + " (" + pn + ")";

                            qbstylelist.Add(new GenericRecord(s, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }
        
        public void ImportEndPlay()
        {
            string filedir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019EPAM.csv";

            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "EPAM" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        endplaylist = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string desc = "";

                            if (fields[0] == "EPAS")
                                desc = entry[0];
                            if (fields[1] == "EPAV")
                                id = Convert.ToInt32(entry[1]);
                            desc = desc.Replace("EndPlay_","");
                            
                            endplaylist.Add(new GenericRecord(desc, id));
                        }
                    }

                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }
            }
        }
        
        public void ImportInjuries()
        {
            string filedir = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location);
            string filename = filedir + @"\res\2019INJY.csv";
            
            if (File.Exists(filename))
            {
                try
                {
                    StreamReader sr = new StreamReader(filename);
                    string header = sr.ReadLine();
                    string[] version = header.Split(',');
                    if (version[0] == "INJY" && version[1] == "2019")
                    {
                        if (version[2] == "Yes")
                        {
                            //read Field desciptions
                        }

                        injurylist = new List<GenericRecord>();
                        int total = Convert.ToInt32(version[3]);
                        string row = sr.ReadLine();
                        string[] fields  = row.Split(',');
                        for (int c = 0; c < total; c++)
                        {
                            string e = sr.ReadLine();
                            string[] entry = e.Split(',');
                            int id = -1;
                            string desc = "";

                            if (fields[0] == "INJT")
                                id = Convert.ToInt32(entry[0]);
                            if (fields[1] == "INJD")
                                desc = entry[1];
                            injurylist.Add(new GenericRecord(desc, id));
                        }
                    }
                    
                    sr.Close();
                }
                catch (IOException err)
                {
                    err = err;
                }                
            }
        }


        #region Functions
        public void ExportDraftClass(string filename)
        {
            File.Delete(filename);
            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine("Generated by Madden Amp.  MDC File Version: " + model.DraftClassFields.Count);

            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                PlayerRecord player = (PlayerRecord)rec;

                if (player.YearsPro != 0 || player.Deleted == true || (player.FirstName == "New" && player.LastName == "Player")) { continue; }

                foreach (string s in model.DraftClassFields[model.DraftClassFields.Count - 1])
                {
                    if (s != model.DraftClassFields[model.DraftClassFields.Count - 1][0])
                    {
                        sw.Write("\t");
                    }

                    if (player.ContainsIntField(s))
                    {
                        sw.Write(player.GetIntField(s));
                    }
                    else if (player.ContainsStringField(s))
                    {
                        sw.Write(player.GetStringField(s));
                    }
                    else
                    {
                        Trace.WriteLine("Severe Error!  Player does not contain field " + s + "!  Returning...");
                        sw.Close();
                        return;
                    }
                }

                sw.WriteLine("");
            }

            sw.Close();

        }

        // MADDEN DRAFT EDIT

        public PlayerRecord GetPlayerRecord(int recno)
        {
            return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(recno);
        }

        public PlayerRecord GetPlayerByPlayerId(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (((PlayerRecord)record).PlayerId == playerId)
                {
                    currentPlayerIndex = record.RecNo;
                    return (PlayerRecord)record;
                }
            }
            return null;
        }

        public PlayerRecord CurrentPlayerRecord
        {
            get
            {
                return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);
            }
            set
            {
                PlayerRecord curr = value;

                if (curr != null)
                {
                    // need to set currenPlayerIndex to the correct index                
                    List<TableRecordModel> players = model.TableModels[EditorModel.PLAYER_TABLE].GetRecords();
                    int index = 0;
                    PlayerRecord record = value;
                    for (index = 0; index < players.Count; index++)
                    {
                        PlayerRecord play = (PlayerRecord)players[index];
                        if (play.PlayerId == record.PlayerId)
                        {
                            currentPlayerIndex = index;
                            return;
                        }
                    }
                }

                currentPlayerIndex = -1;
            }
        }

        public PlayerRecord GetNextPlayerRecord()
        {
            PlayerRecord record = null;

            int startingindex = currentPlayerIndex;
            while (true)
            {
                currentPlayerIndex++;
                if (currentPlayerIndex == startingindex)
                {
                    //We have looped around
                    return null;
                }

                if (currentPlayerIndex >= model.TableModels[EditorModel.PLAYER_TABLE].RecordCount)
                {
                    currentPlayerIndex = -1;
                    continue;
                }

                record = (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

                if (record.FirstName == "New")
                    continue;

                //If this record is marked for deletion then skip it
                if (record.Deleted)
                {
                    continue;
                }

                if (currentTeamFilter != null)
                {
                    if (!(model.TeamModel.GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
                    {
                        continue;
                    }
                }
                if (currentPositionFilter != -1)
                {
                    if (record.PositionId != currentPositionFilter)
                    {
                        continue;
                    }
                }
                if (currentDraftClassFilter)
                {
                    if (record.YearsPro != 0)
                    {
                        continue;
                    }
                }

                //Found one
                break;
            }

            return record;
        }

        public PlayerRecord GetPreviousPlayerRecord()
        {
            PlayerRecord record = null;

            int startingindex = currentPlayerIndex;
            while (true)
            {
                currentPlayerIndex--;
                if (currentPlayerIndex == startingindex)
                {
                    //We have looped around
                    return null;
                }

                if (currentPlayerIndex < 0)
                {
                    currentPlayerIndex = model.TableModels[EditorModel.PLAYER_TABLE].RecordCount;
                    continue;
                }

                record = (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].GetRecord(currentPlayerIndex);

                //If this record is marked for deletion then skip it
                if (record.Deleted)
                {
                    continue;
                }

                if (currentTeamFilter != null)
                {
                    if (!(model.TeamModel.GetTeamNameFromTeamId(record.TeamId).Equals(currentTeamFilter)))
                    {
                        continue;
                    }
                }
                if (currentPositionFilter != -1)
                {
                    if (record.PositionId != currentPositionFilter)
                    {
                        continue;
                    }
                }
                if (currentDraftClassFilter)
                {
                    if (record.YearsPro != 0)
                    {
                        continue;
                    }
                }
                if (record.FirstName == "New")
                    continue;

                //Found one
                break;
            }

            return record;
        }

        public void SetDraftClassFilter(bool use)
        {
            currentDraftClassFilter = use;
        }

        public void SetTeamFilter(string teamname)
        {
            currentTeamFilter = teamname;
        }

        public void RemoveTeamFilter()
        {
            currentTeamFilter = null;
        }

        public void SetPositionFilter(int index)
        {
            currentPositionFilter = index;
        }

        public void RemovePositionFilter()
        {
            currentPositionFilter = -1;
        }

        public Dictionary<string, TableRecordModel> SearchForPlayers(String[] names)
        {
            Trace.WriteLine("Starting search for " + names.ToString());
            //This is not going to be efficient.
            Dictionary<String, TableRecordModel> results = new Dictionary<String, TableRecordModel>();

            foreach (TableRecordModel record in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                String firstname = record.GetStringField(PlayerRecord.FIRST_NAME);
                String lastname = record.GetStringField(PlayerRecord.LAST_NAME);

                String firstnameLower = firstname.ToLower();
                String lastnameLower = lastname.ToLower();

                bool gotmatch = true;
                foreach (String searchterm in names)
                {
                    if ((firstnameLower.IndexOf(searchterm) == -1) && (lastnameLower.IndexOf(searchterm) == -1))
                    {
                        //We don't have a match
                        gotmatch = false;
                        break;
                    }
                }
                if (gotmatch)
                {
                    String key = lastname + ", " + firstname + "   (" + model.TeamModel.GetTeamNameFromTeamId(record.GetIntField(PlayerRecord.TEAM_ID)) + ")";
                    String addkey = key;
                    int count = 1;
                    while (results.ContainsKey(addkey))
                    {
                        addkey = key + "(" + count++ + ")";
                    }
                    results.Add(addkey, (PlayerRecord)record);
                }
            }
            return results;
        }

        public InjuryRecord GetPlayersInjuryRecord(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
            {
                InjuryRecord injuryRecord = (InjuryRecord)record;
                if (playerId == injuryRecord.PlayerId)
                {
                    return injuryRecord;
                }
            }
            return null;
        }

        #region Stats


        public CareerStatsOffenseRecord GetPlayersOffenseCareer(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
            {
                CareerStatsOffenseRecord offensecareer = (CareerStatsOffenseRecord)record;
                if (playerId == offensecareer.PlayerId)
                {
                    return offensecareer;
                }
            }
            return null;
        }

        public SeasonStatsOffenseRecord GetOffStats(int playerId, int season)
        {
            int count = 0;
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
            {
                SeasonStatsOffenseRecord offstats = (SeasonStatsOffenseRecord)record;
                if (playerId == offstats.PlayerId)
                {
                    if (offstats.Deleted)
                        count--;
                    count++;
                    if (season == offstats.Season)
                        return offstats;
                }
            }

            return null;
        }

        public CareerStatsDefenseRecord GetPlayersDefenseCareer(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
            {
                CareerStatsDefenseRecord defensecareer = (CareerStatsDefenseRecord)record;
                if (playerId == defensecareer.PlayerId)
                {
                    return defensecareer;
                }
            }
            return null;
        }

        public SeasonStatsDefenseRecord GetDefenseStats(int playerId, int season)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
            {
                SeasonStatsDefenseRecord dstats = (SeasonStatsDefenseRecord)record;
                if (playerId == dstats.PlayerId && season == dstats.Season)
                {
                    return dstats;
                }
            }
            return null;
        }

        public CareerStatsOffensiveLineRecord GetPlayersOLCareer(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
            {
                CareerStatsOffensiveLineRecord offensivelinecareer = (CareerStatsOffensiveLineRecord)record;
                if (playerId == offensivelinecareer.PlayerId)
                {
                    return offensivelinecareer;
                }
            }
            return null;
        }

        public SeasonStatsOffensiveLineRecord GetOLstats(int playerId, int season)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
            {
                SeasonStatsOffensiveLineRecord SeaOL = (SeasonStatsOffensiveLineRecord)record;
                if (playerId == SeaOL.PlayerId && season == SeaOL.Season)
                {
                    return SeaOL;
                }
            }
            return null;
        }

        public CareerGamesPlayedRecord GetPlayersGamesCareer(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_GAMES_PLAYED_TABLE].GetRecords())
            {
                CareerGamesPlayedRecord gamesplayedcareer = (CareerGamesPlayedRecord)record;
                if (playerId == gamesplayedcareer.PlayerId)
                {
                    return gamesplayedcareer;
                }
            }
            return null;
        }

        public SeasonGamesPlayedRecord GetSeasonGames(int playerId, int season)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_GAMES_PLAYED_TABLE].GetRecords())
            {
                SeasonGamesPlayedRecord gamesplayed = (SeasonGamesPlayedRecord)record;
                if (playerId == gamesplayed.PlayerId && season == gamesplayed.Season)
                {
                    return gamesplayed;
                }
            }
            return null;
        }

        public CareerPuntKickRecord GetPlayersCareerPuntKick(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_TABLE].GetRecords())
            {
                CareerPuntKickRecord puntkickrecord = (CareerPuntKickRecord)record;
                if (playerId == puntkickrecord.PlayerId)
                {
                    return puntkickrecord;
                }
            }
            return null;
        }

        public SeasonPuntKickRecord GetPuntKick(int playerId, int season)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_TABLE].GetRecords())
            {
                SeasonPuntKickRecord puntkick = (SeasonPuntKickRecord)record;
                if (playerId == puntkick.PlayerId && season == puntkick.Season)
                {
                    return puntkick;
                }
            }
            return null;
        }

        public CareerPKReturnRecord GetPlayersCareerPKReturn(int playerId)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
            {
                CareerPKReturnRecord pkreturnrecord = (CareerPKReturnRecord)record;
                if (playerId == pkreturnrecord.PlayerId)
                {
                    return pkreturnrecord;
                }
            }
            return null;
        }

        public SeasonPKReturnRecord GetPKReturn(int playerId, int season)
        {
            foreach (TableRecordModel record in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
            {
                SeasonPKReturnRecord pkreturn = (SeasonPKReturnRecord)record;
                if (playerId == pkreturn.PlayerId && season == pkreturn.Season)
                {
                    return pkreturn;
                }
            }
            return null;
        }

        public SeasonStatsOffenseRecord CreateNewSeaOR()
        {
            return (SeasonStatsOffenseRecord)model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].CreateNewRecord(true);
        }

        public void RemoveAllStats(int playerid, bool season, bool career)
        {
            if (season)
            {
                List<SeasonGamesPlayedRecord> remove1 = new List<SeasonGamesPlayedRecord>();
                foreach (SeasonGamesPlayedRecord record in model.TableModels[EditorModel.SEASON_GAMES_PLAYED_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove1.Add(record);
                }
                for (int c = 0; c < remove1.Count; c++)
                    remove1[c].SetDeleteFlag(true);

                List<SeasonPKReturnRecord> remove2 = new List<SeasonPKReturnRecord>();
                foreach (SeasonPKReturnRecord record in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove2.Add(record);
                }
                for (int c = 0; c < remove2.Count; c++)
                    remove2[c].SetDeleteFlag(true);


                List<SeasonPuntKickRecord> remove3 = new List<SeasonPuntKickRecord>();
                foreach (SeasonPuntKickRecord record in model.TableModels[EditorModel.SEASON_STATS_KICKPUNT_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove3.Add(record);
                }
                for (int c = 0; c < remove3.Count; c++)
                    remove3[c].SetDeleteFlag(true);

                List<SeasonStatsDefenseRecord> remove4 = new List<SeasonStatsDefenseRecord>();
                foreach (SeasonStatsDefenseRecord record in model.TableModels[EditorModel.SEASON_STATS_DEFENSE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove4.Add(record);
                }
                for (int c = 0; c < remove4.Count; c++)
                    remove4[c].SetDeleteFlag(true);

                List<SeasonStatsOffenseRecord> remove5 = new List<SeasonStatsOffenseRecord>();
                foreach (SeasonStatsOffenseRecord record in model.TableModels[EditorModel.SEASON_STATS_OFFENSE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove5.Add(record);
                }
                for (int c = 0; c < remove5.Count; c++)
                    remove5[c].SetDeleteFlag(true);

                List<SeasonStatsOffensiveLineRecord> remove6 = new List<SeasonStatsOffensiveLineRecord>();
                foreach (SeasonStatsOffensiveLineRecord record in model.TableModels[EditorModel.SEASON_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                        remove6.Add(record);
                }
                for (int c = 0; c < remove6.Count; c++)
                    remove6[c].SetDeleteFlag(true);
            }

            else if (career)
            {
                foreach (CareerGamesPlayedRecord record in model.TableModels[EditorModel.CAREER_GAMES_PLAYED_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }

                foreach (CareerPKReturnRecord record in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_RETURN_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }

                foreach (CareerPuntKickRecord record in model.TableModels[EditorModel.CAREER_STATS_KICKPUNT_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }

                foreach (CareerStatsDefenseRecord record in model.TableModels[EditorModel.CAREER_STATS_DEFENSE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }

                foreach (CareerStatsOffenseRecord record in model.TableModels[EditorModel.CAREER_STATS_OFFENSE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }

                foreach (CareerStatsOffensiveLineRecord record in model.TableModels[EditorModel.CAREER_STATS_OFFENSIVE_LINE_TABLE].GetRecords())
                {
                    if (record.PlayerId == playerid)
                    {
                        record.SetDeleteFlag(true);
                        break;
                    }
                }
            }
        }

        #endregion

        //  To do, change player's previous team
        //  check for team min/max at position etc
        public void ChangePlayersTeam(TeamRecord newTeam)
        {
            //Don't do anything if the team is same as the current players team
            if (CurrentPlayerRecord.TeamId != newTeam.TeamId)
            {
                //Also have to ensure we update this players injuries in the injury table
                //and remove this player from any depth charts

                foreach (TableRecordModel record in model.TableModels[EditorModel.INJURY_TABLE].GetRecords())
                {
                    if (record.Deleted)
                        continue;
                    //If this injury record is for this player then update its team field
                    InjuryRecord injRecord = (InjuryRecord)record;

                    if (injRecord.PlayerId == CurrentPlayerRecord.PlayerId)
                    {
                        injRecord.TeamId = newTeam.TeamId;
                    }
                }

                if (CurrentPlayerRecord.TeamId < 1009)
                {
                    RemovePlayerFromDepthChart(CurrentPlayerRecord.PlayerId);
                    CurrentPlayerRecord.PreviousTeamId = CurrentPlayerRecord.TeamId;
                }
                else
                {
                    if (CurrentPlayerRecord.PreviousTeamId >= 1009 && newTeam.TeamId < 1009) // Probowl
                        CurrentPlayerRecord.PreviousTeamId = newTeam.TeamId;
                    else CurrentPlayerRecord.PreviousTeamId = CurrentPlayerRecord.TeamId;
                }

                CurrentPlayerRecord.TeamId = newTeam.TeamId;
            }
        }

        /// <summary>
        /// TODO:
        /// This method should be put into the depth chart editing model at some stage.
        /// The whole depth chart editing functionality has too much logic in the scoutingForm objects
        /// and it needs to be moved into the depth chart editing model
        /// </summary>
        /// <param name="playerId"></param>
        public void RemovePlayerFromDepthChart(int playerId)
        {           
            
            List<DepthChartRecord> oldDepthChartRecords = new List<DepthChartRecord>();

            foreach (TableRecordModel record in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
            {
                if (record.Deleted)
                    continue;

                DepthChartRecord depthRecord = (DepthChartRecord)record;

                if (depthRecord.PlayerId == playerId)
                {
                    // Can't delete yet, unless player exists only once in depth chart
                    // Now record the position and team and depth cause we want to fix up
                    // the other players ordering in that same position
                    oldDepthChartRecords.Add(depthRecord);
                }
            }
            // Now delete all the depth chart records for this player.
            for (int p = 0; p < oldDepthChartRecords.Count; p++)
            {
                oldDepthChartRecords[p].SetDeleteFlag(true);
            }

            // Now we have a list of the old depth charts that this player belongs too, we need to fix each 
            // one up. This is not going to be very efficient :)

            foreach (DepthChartRecord record in oldDepthChartRecords)
            {
                foreach (TableRecordModel depthChartRec in model.TableModels[EditorModel.DEPTH_CHART_TABLE].GetRecords())
                {
                    if (depthChartRec.Deleted)
                        continue;

                    DepthChartRecord depthRecord = (DepthChartRecord)depthChartRec;

                    if (depthRecord.TeamId == record.TeamId && depthRecord.PositionId == record.PositionId)
                    {
                        if (depthRecord.DepthOrder > record.DepthOrder)
                        {
                            depthRecord.DepthOrder--;
                        }

                        //TODO: We could probably exit early after we found like 6 or something
                        //records cause thats the maximum depth chart level anyway. but we'll try this
                        //first
                    }
                }
            }
        }

        public void DeletePlayerRecord(PlayerRecord record)
        {
            //Mark this record for deletion
            record.SetDeleteFlag(true);

            //Remove this player from any depth charts
            if (record.TeamId != 1009 && record.TeamId != 1023 && record.TeamId != 1014 && record.TeamId != 1015)
                RemovePlayerFromDepthChart(record.PlayerId);
        }
        
        public InjuryRecord CreateNewInjuryRecord()
        {
            return (InjuryRecord)model.TableModels[EditorModel.INJURY_TABLE].CreateNewRecord(true);
        }

        public PlayerRecord CreateNewPlayerRecord()
        {
            // Either we have to set this bool to true to allow the creation of a
            // new table, or we have to check first if we have one marked for deletion
            // otherwise it replaces one that is already active I think?

            return (PlayerRecord)model.TableModels[EditorModel.PLAYER_TABLE].CreateNewRecord(true);
        }

        public List<string> GetPlayerList()
        {
            if (_playerlist == null)
                _playerlist = new List<int>();
            else _playerlist.Clear();
            List<String> Players = new List<string>();
            playernames.Clear();

            foreach (TableRecordModel rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                PlayerRecord play = (PlayerRecord)rec;
                if (play.FirstName == "New")
                    continue;
                else if (filterdraft && play.YearsPro != 0)
                    continue;
                else if (filterteam == -1 && filterposition != -1)
                {
                    if (play.PositionId != filterposition)
                        continue;
                }
                else if (filterposition == -1 && filterteam != -1)
                {
                    if (play.TeamId != filterteam)
                        continue;
                }
                else if (filterposition != -1 && filterposition != -1)
                    if (filterteam != play.TeamId || filterposition != play.PositionId)
                        continue;
                
                _playerlist.Add(play.PlayerId);
            }            

            _playerlist.Sort();
            foreach (int i in _playerlist)
            {
                PlayerRecord named = GetPlayerByPlayerId(i);
                Players.Add(named.FirstName + " " + named.LastName);
                if (!playernames.ContainsKey(i))
                    playernames.Add(i, named.FirstName + " " + named.LastName);
                else
                {
                    if (!duplicateplayers.ContainsKey(i))
                        duplicateplayers.Add(i, named.FirstName + " " + named.LastName);
                }
            }

            return Players;
        }

        public int GetDraftedPosition()
        {
            switch (CurrentPlayerRecord.OriginalPositionId)
            {
                case (int)MaddenPositions.QB:
                    return 0;
                case (int)MaddenPositions.HB:
                    return 1;
                case (int)MaddenPositions.FB:
                    return 2;
                case (int)MaddenPositions.WR:
                    return 3;
                case (int)MaddenPositions.TE:
                    return 4;
                case (int)MaddenPositions.LT:
                case (int)MaddenPositions.RT:
                    return 5;
                case (int)MaddenPositions.LG:
                case (int)MaddenPositions.RG:
                    return 6;
                case (int)MaddenPositions.C:
                    return 7;
                case (int)MaddenPositions.LE:
                case (int)MaddenPositions.RE:
                    return 8;
                case (int)MaddenPositions.DT:
                    return 9;
                case (int)MaddenPositions.LOLB:
                case (int)MaddenPositions.ROLB:
                    return 10;
                case (int)MaddenPositions.MLB:
                    return 11;
                case (int)MaddenPositions.CB:
                    return 12;
                case (int)MaddenPositions.FS:
                    return 13;
                case (int)MaddenPositions.SS:
                    return 14;
                case (int)MaddenPositions.K:
                    return 15;
                case (int)MaddenPositions.P:
                    return 16;
                default:
                    return -1;
            }
        }

        public void SetProgressionRank()
        {
            ProgRank.Clear();
            AvgOVR.Clear();

            if (model.FileType != MaddenFileType.Franchise)
                return;

            for (int i = 0; i < 3; i++)
            {
                ProgRank.Add(0);
                AvgOVR.Add(0);
            }

            int testgroup = 0;
            if (model.PlayerModel.CurrentPlayerRecord.Overall >= 90)
                testgroup = 0;
            else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 82 && model.PlayerModel.CurrentPlayerRecord.Overall <= 89)
                testgroup = 1;
            else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 76 && model.PlayerModel.CurrentPlayerRecord.Overall <= 81)
                testgroup = 2;
            else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 70 && model.PlayerModel.CurrentPlayerRecord.Overall <= 75)
                testgroup = 3;
            else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 60 && model.PlayerModel.CurrentPlayerRecord.Overall <= 69)
                testgroup = 4;
            else if (model.PlayerModel.CurrentPlayerRecord.Overall >= 0 && model.PlayerModel.CurrentPlayerRecord.Overall <= 59)
                testgroup = 5;

            int totalovr = 0;
            int ovr = 0;

            foreach (PlayerRecord rec in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (rec.PlayedGames == 0)
                    continue;

                if (rec.PositionId == CurrentPlayerRecord.PositionId)
                {
                    bool ck1 = false;
                    bool ck2 = false;

                    int checkgroup = 0;
                    if (rec.Overall >= 90)
                        checkgroup = 0;
                    else if (rec.Overall >= 82 && rec.Overall <= 89)
                        checkgroup = 1;
                    else if (rec.Overall >= 76 && rec.Overall <= 81)
                        checkgroup = 2;
                    else if (rec.Overall >= 70 && rec.Overall <= 75)
                        checkgroup = 3;
                    else if (rec.Overall >= 60 && rec.Overall <= 69)
                        checkgroup = 4;
                    else if (rec.Overall >= 0 && rec.Overall <= 59)
                        checkgroup = 5;

                    if (testgroup == checkgroup)
                    {
                        totalovr += rec.Ppsp;
                        ovr++;
                        if (rec.Ppsp > ProgRank[0])
                            ProgRank[0] = rec.Ppsp;
                        ck1 = true;
                    }

                    if (CurrentPlayerRecord.CareerPhase == rec.CareerPhase)
                    {
                        if (rec.Ppsp > ProgRank[1])
                            ProgRank[1] = rec.Ppsp;
                        ck2 = true;
                    }

                    if (ck1 && ck2)
                    {
                        if (rec.Ppsp > ProgRank[2])
                            ProgRank[2] = rec.Ppsp;
                    }
                }
            }
            if (totalovr > 0 && ovr > 0)
                AvgOVR[0] = (Decimal)totalovr / ovr;


        }

        public void ClearDraftClass(int round, int index, int resumepick)
        {
            foreach (PlayerRecord play in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (play.YearsPro == 0)
                {
                    if (play.DraftRound > round)
                    {
                        if (play.DraftRoundIndex >= index)
                        {
                            play.DraftRound = 0;
                            play.DraftRoundIndex = 0;
                            play.TeamId = 1015;
                            play.PreviousTeamId = 1023;
                        }
                    }
                }
            }

            foreach (DraftedPlayers rook in model.TableModels[EditorModel.DRAFTED_PLAYERS_TABLE].GetRecords())
            {

                if (rook.DraftPickNumber >= resumepick - 1)
                {
                    rook.DraftPickRound = 15;
                    rook.DraftPickNumber = 511;
                    rook.DraftPickTeam = 1023;
                    rook.PlayerWeightedRating = 0;

                    if (model.FileVersion >= MaddenFileVersion.Ver2005)
                    {
                        rook.DraftGeneralRating = 0;
                        rook.DraftGPO = 0;
                        rook.DraftPJR = 4;
                    }
                }               
            }
        }

        public void CalculateRookieRatings()
        {
            foreach (PlayerRecord player in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (player.YearsPro == 0)
                    player.Overall = player.CalculateOverallRating(player.PositionId, false);
            }
        }

        public bool CheckIDExists(int pgid, int poid)
        {
            foreach (PlayerRecord player in model.TableModels[EditorModel.PLAYER_TABLE].GetRecords())
            {
                if (player.Deleted)
                    continue;
                if (pgid != -1)
                {
                    if (pgid == player.PlayerId)
                        return true;
                }
                if (poid != -1)
                {
                    if (poid == player.NFLID)
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
    

