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
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;



namespace MaddenEditor.Core.Record
{
	public class StadiumRecord : TableRecordModel
	{
		public const string CITY_ID = "CYID";        
        public const string CAPACITY_MAX = "SCAP";
        public const string CAPACITY_CLUBSEATS = "SCCS";
        public const string CAPACITY_LUXURYBOX = "SCLB";
        public const string CAPACITY_LOWERLEVEL = "SCLL";
        public const string CAPACITY_MIDLEVEL = "SCML";
        public const string CAPACITY_UPPERENDZONE = "SCUE";
        public const string CAPACITY_UPPERLEVEL = "SCUL";
        public const string SFBB = "SFBB";
        public const string SFbb = "SFbb";
        public const string SFTI = "SFTI";
        public const string FIELD_TYPE = "SFTY";
        public const string SFWE = "SFWE";
        public const string STADIUM_ID = "SGID";
        public const string SGRT = "SGRT";                              // 2004 2005 2006
        public const string SIOT = "SIOT";                              // 2004 2005
        public const string SKTY = "SKTY";                              // 2005 2006
        public const string STADIUM_NAME = "SNAM";
        public const string SORD = "SORD";
        public const string SORI = "SORI";
        public const string STADPART_NEND_DECK1 = "SP00";
        public const string STADPART_NEND_DECK2 = "SP01";
        public const string STADPART_NEND_DECK3 = "SPO2";
        public const string STADPART_NWCORN_DECK1 = "SP03";
        public const string STADPART_NECORN_DECK1 = "SP04";
        public const string STADPART_SEND_DECK1 = "SP05";
        public const string STADPART_SEND_DECK2 = "SP06";
        public const string STADPART_SEND_DECK3 = "SP07";
        public const string STADPART_SWCORN_DECK1 = "SP08";
        public const string STADPART_SECORN_DECK1 = "SP09";
        public const string STADPART_ESIDE_DECK1 = "SP10";
        public const string STADPART_ESIDE_DECK2 = "SP11";
        public const string STADPART_ESIDE_DECK3 = "SP12";
        public const string STADPART_WSIDE_DECK1 = "SP13";
        public const string STADPART_WSIDE_DECK2 = "SP14";
        public const string STADPART_WSIDE_DECK3 = "SP15";
        public const string STADPART_ROOF_LIGHTS = "SP16";
        public const string STADPART_ROOF_ROOF = "SP17";
        public const string SP18 = "SP18";
        public const string STADPART_ENDZONE_WALLS = "SP19";
        public const string STADPART_SIDELINE_PATTERN = "SP20";
        public const string STADPART_FIELD_TYPE = "SP21";
        public const string SP22 = "SP22";
        public const string SP23 = "SP23";
        public const string STADPART_ENDZONE_BACK = "SP24";
        public const string STADPART_BACKDROP = "SP25";
        public const string STADIUM_RATING = "SRAT";        
        public const string STADIUM_DAT_RESOURCE = "SRES";
        public const string SSI1 = "SSl1";                              // 2005
        public const string SSl2 = "SSl2";                              // 2005
        public const string SSl3 = "SSl3";                              // 2005
        public const string SSlc = "SSlc";                              // 2005
        public const string SPONSOR_YEARS_LEFT = "SSYL";                // 2005
        public const string SSft = "SSft";
        public const string SPECIAL_LOGO = "ST35";                              // 2006-2008
        public const string STcl = "STcl";
        public const string stcr = "stcr";
        public const string STfc = "STfc";
        public const string Stgg = "STgg";                              // 2004 2005
        public const string STlc = "STlc";
        public const string Stlc = "Stlc";
        public const string STll = "STll";
        public const string STlr = "STlr";
        public const string STPR = "STPR";                              // 2005-2008
        public const string STrf = "STrf";
        public const string STri = "STri";
        public const string STrl = "STrl";
        public const string STRr = "STRr";
        public const string STrr = "STrr";
        public const string STADIUM_CLIMATE_CONTROL = "STRY";
        public const string STxt = "STxt";                              // 2004 2005
        public const string STADIUM_TYPE = "STYP";
        public const string SUID = "SUID";
        public const string SUTf = "SUTf";
        public const string STADIUM_YEARS_OLD = "SYRO";
        public const string TIZO = "TIZO";                              //  2004
        public const string TEAM_NAME = "TMNA";        
        
        
        
        public StadiumRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}


        public int CityId
        {
            get { return GetIntField(CITY_ID); }
            set { SetField(CITY_ID, value); }
        }
        public int CapacityMax
        {
            get { return GetIntField(CAPACITY_MAX); }
            set { SetField(CAPACITY_MAX, value); }
        }
        public int CapacityClubSeats
        {
            get { return GetIntField(CAPACITY_CLUBSEATS); }
            set { SetField(CAPACITY_CLUBSEATS, value); }
        }
        public int CapacityLuxuryBox
        {
            get { return GetIntField(CAPACITY_LUXURYBOX); }
            set { SetField(CAPACITY_LUXURYBOX, value); }
        }
        public int CapacityLowerLevel
        {
            get { return GetIntField(CAPACITY_LOWERLEVEL); }
            set { SetField(CAPACITY_LOWERLEVEL, value); }
        }
        public int CapacityMidLevel
        {
            get { return GetIntField(CAPACITY_MIDLEVEL); }
            set { SetField(CAPACITY_MIDLEVEL, value); }
        }
        public int CapacityUpperEndzone
        {
            get { return GetIntField(CAPACITY_UPPERENDZONE); }
            set { SetField(CAPACITY_UPPERENDZONE, value); }
        }
        public int CapacityUpperLevel
        {
            get { return GetIntField(CAPACITY_UPPERLEVEL); }
            set { SetField(CAPACITY_UPPERLEVEL, value); }
        }
        public int FieldType
        {
            get { return GetIntField(FIELD_TYPE); }
            set { SetField(FIELD_TYPE, value); }
        }
        public int StadiumId
        {
            get { return GetIntField(STADIUM_ID); }
            set { SetField(STADIUM_ID, value); }
        }
        public int StadiumRating
        {
            get { return GetIntField(STADIUM_RATING); }
            set { SetField(STADIUM_RATING, value); }
        }        
        public int StadiumAge
        {
            get { return GetIntField(STADIUM_YEARS_OLD); }
            set { SetField(STADIUM_YEARS_OLD, value); }
        }             
        public string StadiumName
        {
            get { return GetStringField(STADIUM_NAME); }
            set { SetField(STADIUM_NAME, value); }
        }
        public int Stadium_DAT_Resource
        {
            get { return GetIntField(STADIUM_DAT_RESOURCE); }
            set { SetField(STADIUM_DAT_RESOURCE, value); }
        }
        public int TeamName
        {
            get { return GetIntField(TEAM_NAME); }
            set { SetField(TEAM_NAME, value); }
        }        
        public int StadiumPart_North_Deck1
        {
            get { return GetIntField(STADPART_NEND_DECK1); }
            set { SetField(STADPART_NEND_DECK1, value); }
        }
        public int StadiumPart_North_Deck2
        {
            get { return GetIntField(STADPART_NEND_DECK2); }
            set { SetField(STADPART_NEND_DECK2, value); }
        }
        public int StadiumPart_North_Deck3
        {
            get { return GetIntField(STADPART_NEND_DECK3); }
            set { SetField(STADPART_NEND_DECK3, value); }
        }
        public int StadiumPart_NW_Corner_Deck1
        {
            get { return GetIntField(STADPART_NWCORN_DECK1); }
            set { SetField(STADPART_NWCORN_DECK1, value); }
        }
        public int StadiumPart_NE_Corner_Deck1
        {
            get { return GetIntField(STADPART_NECORN_DECK1); }
            set { SetField(STADPART_NECORN_DECK1, value); }
        }
        public int StadiumPart_South_Deck1
        {
            get { return GetIntField(STADPART_SEND_DECK1); }
            set { SetField(STADPART_SEND_DECK1, value); }
        }
        public int StadiumPart_South_Deck2
        {
            get { return GetIntField(STADPART_SEND_DECK2); }
            set { SetField(STADPART_SEND_DECK2, value); }
        }
        public int StadiumPart_South_Deck3
        {
            get { return GetIntField(STADPART_SEND_DECK3); }
            set { SetField(STADPART_SEND_DECK3, value); }
        }
        public int StadiumPart_SW_Corner_Deck1
        {
            get { return GetIntField(STADPART_SWCORN_DECK1); }
            set { SetField(STADPART_SWCORN_DECK1, value); }
        }
        public int StadiumPart_SE_Corner_Deck1
        {
            get { return GetIntField(STADPART_SECORN_DECK1); }
            set { SetField(STADPART_SECORN_DECK1, value); }
        }
        public int StadiumPart_WSIDE_Deck1
        {
            get { return GetIntField(STADPART_WSIDE_DECK1); }
            set { SetField(STADPART_WSIDE_DECK1, value); }
        }
        public int StadiumPart_WSIDE_Deck2
        {
            get { return GetIntField(STADPART_WSIDE_DECK2); }
            set { SetField(STADPART_WSIDE_DECK2, value); }
        }
        public int StadiumPart_WSIDE_Deck3
        {
            get { return GetIntField(STADPART_WSIDE_DECK3); }
            set { SetField(STADPART_WSIDE_DECK3, value); }
        }
        public int StadiumPart_ESIDE_Deck1
        {
            get { return GetIntField(STADPART_ESIDE_DECK1); }
            set { SetField(STADPART_ESIDE_DECK1, value); }
        }
        public int StadiumPart_ESIDE_Deck2
        {
            get { return GetIntField(STADPART_ESIDE_DECK2); }
            set { SetField(STADPART_ESIDE_DECK2, value); }
        }
        public int StadiumPart_ESIDE_Deck3
        {
            get { return GetIntField(STADPART_ESIDE_DECK3); }
            set { SetField(STADPART_ESIDE_DECK3, value); }
        }
        
        public int StadiumPart_Roof_Lights
        {
            get { return GetIntField(STADPART_ROOF_LIGHTS); }
            set { SetField(STADPART_ROOF_LIGHTS, value); }
        }
        public int StadiumPart_Roof
        {
            get { return GetIntField(STADPART_ROOF_ROOF); }
            set { SetField(STADPART_ROOF_ROOF, value); }
        }
        public int StadiumPart_Endzone_Walls
        {
            get { return GetIntField(STADPART_ENDZONE_WALLS); }
            set { SetField(STADPART_ENDZONE_WALLS, value); }
        }
        public int StadiumPart_Sideline_Pattern
        {
            get { return GetIntField(STADPART_SIDELINE_PATTERN); }
            set { SetField(STADPART_SIDELINE_PATTERN, value); }
        }
        public int StadiumPart_Field_Type
        {
            get { return GetIntField(STADPART_FIELD_TYPE); }
            set { SetField(STADPART_FIELD_TYPE, value); }
        }
        public int StadiumPart_Endzone_Back
        {
            get { return GetIntField(STADPART_ENDZONE_BACK); }
            set { SetField(STADPART_ENDZONE_BACK, value); }
        }
        public int StadiumPart_Stadium_Backdrop
        {
            get { return GetIntField(STADPART_BACKDROP); }
            set { SetField(STADPART_BACKDROP, value); }
        }       
        
        public int StadiumType
        {
            get { return GetIntField(STADIUM_TYPE); }
            set { SetField(STADIUM_TYPE, value); }
        }
        public bool StadiumClimateControl
        {
            get { return GetIntField( STADIUM_CLIMATE_CONTROL) == 1; }
            set { SetField(STADIUM_CLIMATE_CONTROL, Convert.ToInt32(value)); }
        }
        public int SpecialLogo
        {
            get { return GetIntField(SPECIAL_LOGO); }
            set { SetField(SPECIAL_LOGO, value); }
        }
        public int SponsorYearsLeft
        {
            get { return GetIntField(SPONSOR_YEARS_LEFT); }
            set { SetField(SPONSOR_YEARS_LEFT, value); }
        }
	}
}