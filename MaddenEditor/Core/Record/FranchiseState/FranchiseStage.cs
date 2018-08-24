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
    //  MOIN

    public class FranchiseStageRecord : TableRecordModel
    {
        //  Stages                                          STEP    Previous
        // 5 = Train camp
        // 7 = Preseason
        // 8 = preseason progression
        // 9 = Regular Season
        // 9 = Wildcard PO                                  160     8
        // 12 = advance to offseason                        0
        // 14 = offesason - owner mode, sign coaches        70
        // 15 = offseason - roster mgmt                     70
        // 16 = resign players                              97      15
        // 17 = pre free agency                             157     16
        // 18 = 1st free agency                             731     17
        // 20 = rookie workouts
        // 21 = draft
        // 23 = sign draft picks
        // 24 = sign free agents

        
        public const string CURRENT_STAGE = "MCSA";     // 5bit
        public const string MISB = "MISB";              // 1bit
        public const string STEP = "MNAI";              // 13bit    // 2004 11bit
        public const string PREVIOUS_STAGE = "MPSA";    // 5bit
        public const string MTYP = "MTYP";              // 5bit

        public FranchiseStageRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
        }

        public int CurrentStage
        {
            get
            {
                return GetIntField(CURRENT_STAGE);
            }
            set
            {
                SetField(CURRENT_STAGE, value);
            }
        }

        public int Misb
        {
            get { return GetIntField(MISB); }
            set { SetField(MISB, value); }
        }
        
        public int Step
        {
            get
            {
                return GetIntField(STEP);
            }
            set
            {
                SetField(STEP, value);
            }
        }

        public int PreviousStage
        {
            get
            {
                return GetIntField(PREVIOUS_STAGE);
            }
            set
            {
                SetField(PREVIOUS_STAGE, value);
            }
        }

        public int Mtyp
        {
            get { return GetIntField(MTYP); }
            set { SetField(MTYP, value); }
        }

    }
}
