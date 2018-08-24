/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
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
    public class DraftPickRecord : TableRecordModel
    {
        //drpk

        public const string TRADED_DOWN = "DPDN";
        public const string CURRENT_PICK_OWNER = "DPID";
        public const string PICK_NUMBER = "DPNM";        
        public const string ORIGINAL_PICK_OWNER = "DPOD";
        public const string PICK_TU = "DPTU";
        public const string TRADED_UP = "DPUD";

        public DraftPickRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public bool TradedDown
        {
            get
            {
                return (GetIntField(TRADED_DOWN) == 1);
            }
            set
            {
                SetField(TRADED_DOWN, Convert.ToInt32(value));
            }
        }

        public int CurrentTeamId
        {
            get
            {
                return GetIntField(CURRENT_PICK_OWNER);
            }
            set
            {
                SetField(CURRENT_PICK_OWNER, value);
            }
        }
                
        public int PickNumber
        {
            get
            {
                return GetIntField(PICK_NUMBER);
            }
            set
            {
                SetField(PICK_NUMBER, value);
            }
        }
                
        public int OriginalTeamId
        {
            get
            {
                return GetIntField(ORIGINAL_PICK_OWNER);
            }
            set
            {
                SetField(ORIGINAL_PICK_OWNER, value);
            }
        }

        public int PickTU
        {
            get { return GetIntField(PICK_TU); }
            set { SetField(PICK_TU, value); }
        }

        public bool TradedUP
        {
            get
            {
                return (GetIntField(TRADED_UP) == 1);
            }
            set
            {
                SetField(TRADED_UP, Convert.ToInt32(value));
            }
        }

    }
}
