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

namespace MaddenEditor.Core.Record
{
	public class CityRecord : TableRecordModel
	{
        public const string CITY_POPULATION = "CPOP";
        public const string REGION = "CREG";
        public const string TIMEZONE = "CTIZ";
        public const string DEMAND = "CTMD";
        public const string TEMP_JAN = "CYAJ";
        public const string TEMP_SEP = "CYAS";
        public const string YEARLY_FAIR_PERC = "CYFP";
        public const string YEARLY_FAIR_WEATHER = "CYFW";
		public const string CITY_ID = "CYID";
		public const string CITY_NAME = "CYNM";
        public const string NEWSPAPER = "CYNP";
        public const string ON_MAP = "CYOM";
        public const string YEARLY_RAIN_PERC = "CYRP";
        public const string YEARLY_SNOW_PERC = "CYSP";
        public const string STATE_NAME = "CYST";
        public const string TELEVISION = "CYTV";
        public const string YEARLY_WIND_PERC = "CYWP";
        public const string CYWW = "CYWW";
        public const string OROT = "OROT";
        public const string OWRC = "OWRC";
        public const string OWRY = "OWRY";



		public CityRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int CityPopulation
        {
            get { return GetIntField(CITY_POPULATION); }
            set { SetField(CITY_POPULATION, value); }
        }
        public int Region
        {
            get { return GetIntField(REGION); }
            set { SetField(REGION, value); }
        }
        public int Timezone
        {
            get { return GetIntField(TIMEZONE); }
            set { SetField(TIMEZONE, value); }
        }
        public int Demand
        {
            get { return GetIntField(DEMAND); }
            set { SetField(DEMAND, value); }
        }
        public int TempJan
        {
            get { return GetIntField(TEMP_JAN); }
            set { SetField(TEMP_JAN, value); }
        }
        public int TempSep
        {
            get { return GetIntField(TEMP_SEP); }
            set { SetField(TEMP_SEP, value); }
        }
        public int YearlyFairPerc
        {
            get { return GetIntField(YEARLY_FAIR_PERC); }
            set { SetField(YEARLY_FAIR_PERC, value); }
        }
        public int YearlyFairWeather
        {
            get { return GetIntField(YEARLY_FAIR_WEATHER); }
            set { SetField(YEARLY_FAIR_WEATHER, value); }
        }
        public int CityId
        {
            get
            {
                return GetIntField(CITY_ID);
            }
            set
            {
                SetField(CITY_ID, value);
            }
        }
        public string CityName
		{
			get
			{
				return GetStringField(CITY_NAME);
			}
			set
			{
				SetField(CITY_NAME, value);
			}
		}
        public string Newspaper
        {
            get { return GetStringField(NEWSPAPER); }
            set { SetField(NEWSPAPER, value); }
        }
        public bool OnMap
        {
            get { return GetIntField(ON_MAP) == 1; }
            set { SetField(ON_MAP, Convert.ToInt32(value)); }
        }
        public int YearlyRainPerc
        {
            get { return GetIntField(YEARLY_RAIN_PERC); }
            set { SetField(YEARLY_RAIN_PERC, value); }
        }
        public int YearlySnowPerc
        {
            get { return GetIntField(YEARLY_SNOW_PERC); }
            set { SetField(YEARLY_SNOW_PERC, value); }
        }
        public string StateName
        {
            get { return GetStringField(STATE_NAME); }
            set { SetField(STATE_NAME, value); }
        }
        public int Television
        {
            get { return GetIntField(TELEVISION); }
            set { SetField(TELEVISION, value); }
        }
        public int YearlyWindPerc
        {
            get { return GetIntField(YEARLY_WIND_PERC); }
            set { SetField(YEARLY_WIND_PERC, value); }
        }
        public int CYww
        {
            get { return GetIntField(CYWW); }
            set { SetField(CYWW, value); }
        }
        public int Orot
        {
            get { return GetIntField(OROT); }
            set { SetField(OROT, value); }
        }
        public int Owrc
        {
            get { return GetIntField(OWRC); }
            set { SetField(OWRC, value); }
        }
        public int Owry
        {
            get { return GetIntField(OWRY); }
            set { SetField(OWRY, value); }
        }




	}
}