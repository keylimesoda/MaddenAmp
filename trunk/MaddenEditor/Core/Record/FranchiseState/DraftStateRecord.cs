/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005, 2006 MaddenWishlist.com and spin16
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
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class DraftStateRecord : TableRecordModel
    {
        //DRIN

        public const string DRAFT_PICK_NUM = "DPNM";
        public const string IN_DRAFT = "DRST";
        public const string TEAM_ID = "TGID";
        public const string TIDE = "TIDE";
        public const string TILD = "TILD";
        public const string TIPD = "TIPD";
        public const string TITL = "TITL";

        

        public DraftStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public bool AtDraftStage
        {
            get
            {
                return (GetIntField(IN_DRAFT) == 1);
            }
            set
            {
                SetField(IN_DRAFT, (value == true ? 1 : 0));
            }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }
        public int PickNumber
        {
            get { return GetIntField(DRAFT_PICK_NUM); }
            set { SetField(DRAFT_PICK_NUM, value); }
        }
    }
}
