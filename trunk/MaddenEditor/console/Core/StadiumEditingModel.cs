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

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Core
{
    public class StadiumEditingModel
    {
        private EditorModel model = null;
        private int currentstadiumrecord = 0;

        public List<int> stadiumlist = new List<int>();
        public Dictionary<int, string> stadiumnames = new Dictionary<int, string>();

        private IList<GenericRecord> Cities = null;
        private IList<GenericRecord> Deck1_Types = null;
        private IList<GenericRecord> Deck2_Types = null;
        private IList<GenericRecord> Deck3_Types = null;
        private IList<GenericRecord> Deck4_Types = null;
        private IList<GenericRecord> Deck5_Types = null;
        private IList<GenericRecord> Deck6_Types = null;
        private IList<GenericRecord> Corner_Types = null;
        private IList<GenericRecord> Roof_Lights = null;
        private IList<GenericRecord> Roof_Types = null;
        private IList<GenericRecord> Endzone_Walls = null;
        private IList<GenericRecord> Sideline = null;
        private IList<GenericRecord> Field_Surface = null;
        private IList<GenericRecord> Endzone_Color = null;
        private IList<GenericRecord> Stadium_Backdrop = null;
        private IList<GenericRecord> Stadium_Field_Type = null;

        public StadiumEditingModel(EditorModel model)
        {
            this.model = model;

            Cities = new List<GenericRecord>();
            foreach (TableRecordModel rec in model.TableModels[EditorModel.CITY_TABLE].GetRecords())
                Cities.Add(new GenericRecord(((CityRecord)rec).CityName, ((CityRecord)rec).CityId));

            Deck1_Types = new List<GenericRecord>();
            Deck1_Types.Add(new GenericRecord("Indent", 0));
            Deck1_Types.Add(new GenericRecord("Stairs", 1));
            Deck1_Types.Add(new GenericRecord("Tunnel", 2));
            Deck1_Types.Add(new GenericRecord("Straight", 3));

            Deck2_Types = new List<GenericRecord>();
            Deck2_Types.Add(new GenericRecord("Straight", 0));
            Deck2_Types.Add(new GenericRecord("Scoreboard Cut-in", 1));
            Deck2_Types.Add(new GenericRecord("Scoredboard", 2));
            Deck2_Types.Add(new GenericRecord("Open Ended", 3));

            Deck3_Types = new List<GenericRecord>();
            Deck3_Types.Add(new GenericRecord("Straight", 0));
            Deck3_Types.Add(new GenericRecord("Scoreboard", 1));
            Deck3_Types.Add(new GenericRecord("Split Scoredboard", 2));
            Deck3_Types.Add(new GenericRecord("Open Scoreboard", 3));
            Deck3_Types.Add(new GenericRecord("Closed Scoreboard", 4));

            Corner_Types = new List<GenericRecord>();
            Corner_Types.Add(new GenericRecord("Extended", 0));
            Corner_Types.Add(new GenericRecord("Tunnel", 1));
            Corner_Types.Add(new GenericRecord("Straight", 2));
            Corner_Types.Add(new GenericRecord("Stairs", 3));

            Deck4_Types = new List<GenericRecord>();
            Deck4_Types.Add(new GenericRecord("Straight", 0));
            Deck4_Types.Add(new GenericRecord("Stairs", 1));
            Deck4_Types.Add(new GenericRecord("Tunnel", 2));
            Deck4_Types.Add(new GenericRecord("Indented", 3));

            Deck5_Types = new List<GenericRecord>();
            Deck5_Types.Add(new GenericRecord("Straight", 0));
            Deck5_Types.Add(new GenericRecord("Press & Luxury", 1));
            Deck5_Types.Add(new GenericRecord("Open Single", 2));
            Deck5_Types.Add(new GenericRecord("Double", 3));

            Deck6_Types = new List<GenericRecord>();
            Deck6_Types.Add(new GenericRecord("Straight", 0));
            Deck6_Types.Add(new GenericRecord("Press & Luxury", 1));
            Deck6_Types.Add(new GenericRecord("Press", 2));

            Roof_Lights = new List<GenericRecord>();
            Roof_Lights.Add(new GenericRecord("None", 0));
            Roof_Lights.Add(new GenericRecord("4 Lights", 1));
            Roof_Lights.Add(new GenericRecord("6 Lights", 1));
            Roof_Lights.Add(new GenericRecord("8 Lights", 1));

            Roof_Types = new List<GenericRecord>();
            Roof_Types.Add(new GenericRecord("None", 0));
            Roof_Types.Add(new GenericRecord("Roof 1", 1));
            Roof_Types.Add(new GenericRecord("Roof 2", 2));
            Roof_Types.Add(new GenericRecord("Roof 3", 3));
            Roof_Types.Add(new GenericRecord("Roof 4", 4));
            Roof_Types.Add(new GenericRecord("Roof 5", 5));

            Endzone_Walls = new List<GenericRecord>();
            Endzone_Walls.Add(new GenericRecord("None", 0));
            Endzone_Walls.Add(new GenericRecord("Pattern 1", 1));
            Endzone_Walls.Add(new GenericRecord("Pattern 2", 2));
            Endzone_Walls.Add(new GenericRecord("Pattern 3", 3));
            Endzone_Walls.Add(new GenericRecord("Pattern 4", 4));
            Endzone_Walls.Add(new GenericRecord("Pattern 5", 5));
            Endzone_Walls.Add(new GenericRecord("Pattern 6", 6));
            Endzone_Walls.Add(new GenericRecord("Pattern 7", 7));
            Endzone_Walls.Add(new GenericRecord("Pattern 8", 8));
            Endzone_Walls.Add(new GenericRecord("Pattern 9", 9));
            Endzone_Walls.Add(new GenericRecord("Pattern 10", 10));
            Endzone_Walls.Add(new GenericRecord("Pattern 11", 11));
            Endzone_Walls.Add(new GenericRecord("Pattern 12", 12));
            Endzone_Walls.Add(new GenericRecord("Pattern 13", 13));
            Endzone_Walls.Add(new GenericRecord("Pattern 14", 14));
            Endzone_Walls.Add(new GenericRecord("Pattern 15", 15));

            Sideline = new List<GenericRecord>();
            Sideline.Add(new GenericRecord("None",0));
            Sideline.Add(new GenericRecord("Pattern 1", 1));
            Sideline.Add(new GenericRecord("Pattern 2", 2));
            Sideline.Add(new GenericRecord("Pattern 3", 3));
            Sideline.Add(new GenericRecord("Pattern 4", 4));

            Field_Surface = new List<GenericRecord>();
            Field_Surface.Add(new GenericRecord("Grass", 0));
            Field_Surface.Add(new GenericRecord("Artificial", 1));
            Field_Surface.Add(new GenericRecord("Baseball", 2));
            Field_Surface.Add(new GenericRecord("Grass Turf", 3));

            Endzone_Color = new List<GenericRecord>();
            Endzone_Color.Add(new GenericRecord("Black", 0));
            Endzone_Color.Add(new GenericRecord("Primary", 0));
            Endzone_Color.Add(new GenericRecord("Secondary", 0));
            Endzone_Color.Add(new GenericRecord("None", 3));

            Stadium_Backdrop = new List<GenericRecord>();
            Stadium_Backdrop.Add(new GenericRecord("Metro 1",0));
            Stadium_Backdrop.Add(new GenericRecord("Metro 2", 1));
            Stadium_Backdrop.Add(new GenericRecord("Rural", 2));
            Stadium_Backdrop.Add(new GenericRecord("Mountains", 3));
            Stadium_Backdrop.Add(new GenericRecord("Tropical", 4));
            
            Stadium_Field_Type = new List<GenericRecord>();
            Stadium_Field_Type.Add(new GenericRecord("Grass", 0));
            Stadium_Field_Type.Add(new GenericRecord("Artificial Turf", 1));
            Stadium_Field_Type.Add(new GenericRecord("Deceivers", 2));
            Stadium_Field_Type.Add(new GenericRecord("Atlantis", 3));
            Stadium_Field_Type.Add(new GenericRecord("Dummy", 4));
            Stadium_Field_Type.Add(new GenericRecord("Glacier", 5));
            Stadium_Field_Type.Add(new GenericRecord("Grassy Turf", 6));


        }

        public StadiumRecord CurrentStadiumRecord
        {
            get { return (StadiumRecord)model.TableModels[EditorModel.STADIUM_TABLE].GetRecord(currentstadiumrecord); }

            set
            {
                StadiumRecord curr = value;
                int index = 0;

                foreach (StadiumRecord rec in model.TableModels[EditorModel.STADIUM_TABLE].GetRecords())
                {
                    if (curr == rec)
                    {
                        currentstadiumrecord = index;
                        break;
                    }
                    index++;
                }
            }
        }
        public StadiumRecord GetNextStadiumRecord()
        {
            StadiumRecord record = null;

            int startingindex = currentstadiumrecord;
            while (true)
            {
                currentstadiumrecord++;

                if (currentstadiumrecord == startingindex)                
                    return null;                

                if (currentstadiumrecord >= model.TableModels[EditorModel.TEAM_TABLE].RecordCount)
                {
                    currentstadiumrecord = -1;
                    continue;
                }

                record = (StadiumRecord)model.TableModels[EditorModel.STADIUM_TABLE].GetRecord(currentstadiumrecord);
                                
                if (record.Deleted)                
                    continue;                 
                
                break;
            }

            return record;
        }
        public StadiumRecord GetPreviousStadiumRecord()
        {
            StadiumRecord record = null;

            int startingindex = currentstadiumrecord;
            while (true)
            {
                currentstadiumrecord--;

                if (currentstadiumrecord == startingindex)                
                    return null;
                
                if (currentstadiumrecord < 0)
                {
                    currentstadiumrecord = model.TableModels[EditorModel.STADIUM_TABLE].RecordCount;
                    continue;
                }               

                record = (StadiumRecord)model.TableModels[EditorModel.STADIUM_TABLE].GetRecord(currentstadiumrecord);
                                
                if (record.Deleted)                
                    continue;                 
                
                break;
            }

            return record;
        }

        #region Get/Set Stadium Parts

        public IList<GenericRecord> CityList
        {
            get { return Cities; }
        }
        public IList<GenericRecord> Deck1List
        {
            get { return Deck1_Types; }
        }
        public IList<GenericRecord> Deck2List
        {
            get { return Deck2_Types; }            
        }
        public IList<GenericRecord> Deck3List
        {
            get { return Deck3_Types; }
        }
        public IList<GenericRecord> Deck4List
        { 
            get { return Deck4_Types; } 
        }
        public IList<GenericRecord> Deck5List
        {
            get { return Deck5_Types; }
        }
        public IList<GenericRecord> Deck6List
        {
            get { return Deck6_Types; }
        }
        public IList<GenericRecord> FieldTypes
        {
            get { return Stadium_Field_Type; }
        }
        public IList<GenericRecord> CornerTypes
        {
            get { return Corner_Types; }
        }
        public IList<GenericRecord> RoofTypes
        {
            get { return Roof_Types; }
        }
        public IList<GenericRecord> RoofLights
        {
            get { return Roof_Lights; }
        }
        public IList<GenericRecord> EndzoneWalls
        {
            get { return Endzone_Walls; }
        }
        public IList<GenericRecord> SidelinePattern
        {
            get { return Sideline; }
        }
        public IList<GenericRecord> FieldSurface
        {
            get { return Field_Surface; }
        }
        public IList<GenericRecord> EndzoneColor
        {
            get { return Endzone_Color; }
        }
        public IList<GenericRecord> StadiumBackdrop
        {
            get { return Stadium_Backdrop; }
        }

        #endregion

        public StadiumRecord GetStadiumByID(int id)
        {
            foreach (StadiumRecord rec in model.TableModels[EditorModel.STADIUM_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                StadiumRecord stadium = (StadiumRecord)rec;
                if (stadium.StadiumId == id)
                {
                    currentstadiumrecord = rec.RecNo;
                    return stadium;
                }
            }

            return null;
        }


        public void GetStadiumList()
        {
            if (stadiumlist == null)
                stadiumlist = new List<int>();
            else stadiumlist.Clear();
            if (stadiumnames == null)
                stadiumnames = new Dictionary<int, string>();
            else stadiumnames.Clear();

            foreach (StadiumRecord rec in model.TableModels[EditorModel.STADIUM_TABLE].GetRecords())
            {
                if (rec.Deleted)
                    continue;
                StadiumRecord stadium = (StadiumRecord)rec;
                stadiumlist.Add(stadium.StadiumId);
            }

            stadiumlist.Sort();

            foreach (int i in stadiumlist)
            {
                stadiumnames.Add(i, GetStadiumByID(i).StadiumName);
            }
        }
    }
}
