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
    public class FreeAgencyStateRecord : TableRecordModel
    {
        // FAIN

        
        public const string DAYS_REMAINING = "PSOD";
        public const string IN_FREE_AGENCY = "SOST";
        public const string TIDE = "TIDE";
        public const string TILD = "TILD";
        public const string TIPD = "TIPD";
        public const string TITL = "TITL";



        public FreeAgencyStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        
        public int DaysRemaining
        {
            get
            {
                return GetIntField(DAYS_REMAINING);
            }
            set
            {
                SetField(DAYS_REMAINING, value);
            }
        }

        public bool AtFreeAgencyStage
        {
            get
            {
                return (GetIntField(IN_FREE_AGENCY) == 1);
            }
            set
            {
                SetField(IN_FREE_AGENCY, (value == true ? 1 : 0));
            }
        }

        public int Tide
        {
            get { return GetIntField(TIDE); }
            set { SetField(TIDE, value); }
        }
        public int Tild
        {
            get { return GetIntField(TILD); }
            set { SetField(TILD, value); }
        }
        public int Tipd
        {
            get { return GetIntField(TIPD); }
            set { SetField(TIPD, value); }
        }
        public int Titl
        {
            get { return GetIntField(TITL); }
            set { SetField(TITL, value); }
        }

    }
}
