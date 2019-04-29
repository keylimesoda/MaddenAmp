/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2015 Stingray68
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

namespace MaddenEditor.Core.Record
{
    public class SuperStarStatsRequired : TableRecordModel
    {
        public const string FIELD = "RQCF";
        public const string TABLE = "TREF";
        public const string ROLE_ID = "RLID";
        public const string POSITION_MIN = "PLPM";
        public const string REQUIRED_MIN = "RQMN";
        public const string REQUIRED_MAX = "RQMX";
        public const string POSITION_MAX = "PLPX";


        public SuperStarStatsRequired(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {

        }

        public int Field
        {
            get { return GetIntField(FIELD); }
            set { SetField(FIELD, value); }
        }
        public int Table
        {
            get { return GetIntField(TABLE); }
            set { SetField(TABLE, value); }
        }
        public int RoleID
        {
            get { return GetIntField(ROLE_ID); }
            set { SetField(ROLE_ID, value); }
        }
        public int PosMin
        {
            get { return GetIntField(POSITION_MIN); }
            set { SetField(POSITION_MIN, value); }
        }
        public int PosMax
        {
            get { return GetIntField(POSITION_MAX); }
            set { SetField(POSITION_MAX, value); }
        }
        public int ReqMin
        {
            get { return GetIntField(REQUIRED_MIN); }
            set { SetField(REQUIRED_MIN, value); }
        }
        public int ReqMax
        {
            get { return GetIntField(REQUIRED_MAX); }
            set { SetField(REQUIRED_MAX, value); }
        }
        
        public string Fieldname
        {
            get
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = BitConverter.GetBytes(Field);
                return a.GetString(ba);
            }
            set
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = a.GetBytes(value);
                Field = BitConverter.ToInt32(ba, 0);
            }
        }
        public string Tablename
        {
            get
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = BitConverter.GetBytes(Table);
                return a.GetString(ba);
            }
            set
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = a.GetBytes(value);
                Table = BitConverter.ToInt32(ba, 0);
            }
        }


    }
}
