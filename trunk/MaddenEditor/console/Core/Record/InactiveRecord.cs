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
    //  table PLIA   v2005+

    public class InactiveRecord : TableRecordModel
    {
        public const string PLAYER_ID = "PGID";
        public const string PIPW = "PIPW";          // 0-31 probably week#
        public const string PIRE = "PIRE";          // 0-15 reason 2= team change, 3 contract
        public const string PISW = "PISW";          // 0-31 probably week#
        public const string PITC = "PITC";          // bool
        public const string TEAM_ID = "TGID";
                
        public InactiveRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}


        public int PlayerID
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int Pipw
        {
            get { return GetIntField(PIPW); }
            set { SetField(PIPW, value); }
        }
        public int Pire
        {
            get { return GetIntField(PIRE); }
            set { SetField(PIRE, value); }
        }
        public int Pisw
        {
            get { return GetIntField(PISW); }
            set { SetField(PISW, value); }
        }
        public int Pitc
        {
            get { return GetIntField(PITC); }
            set { SetField(PITC, value); }
        }
        public int TeamId
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }

    
    }
}
