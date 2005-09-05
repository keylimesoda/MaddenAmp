/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 MaddenWishlist.com
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net/index.php/Projects/MaddenEditor
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
        public const string PICK_NUMBER = "DPNM"; 
        public const string CURRENT_PICK_OWNER = "DPID";
        public const string ORIGINAL_PICK_OWNER = "DPOD";

        public DraftPickRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

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
    }
}
