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
    // "COLL"
    public class CollegesRecord : TableRecordModel
    {
        public const string COLLEGE_ID = "COID";
        public const string COLLEGE_COIW = "COIW";                          // 2007-2008        true/false 
        public const string COLLEGE_NAME = "CONA";                          // 16 character limit
        public const string COLLEGE_TEAM_ID = "COTI";                       // if not already assigned needs to be 0
        public const string COLLEGE_ENUN = "ENUN";                          // 2007-2008        when COIW is true this is set to "Yes" else "No"
        public const string COLLEGE_ENUV = "ENUV";                          // 2007-2008
        public const string COLLEGE_PEXP = "pEXP";                          // 2006-2008                        same value as pPOP
        public const string COLLEGE_PPOP = "pPOP";                          // 2006-2008                        same value as pEXP
        public const string COLLEGE_PXPC = "pXPC";                          // 2006-2008
        
        
        public string CollegeName
        {
            get { return GetStringField(COLLEGE_NAME); }
            set { SetField(COLLEGE_NAME, value); }
        }
        
        public int CollegeId
        {
            get { return GetIntField(COLLEGE_ID); }
            set { SetField(COLLEGE_ID, value); }
        }

        public int CollegeTeamId
        {
            get { return GetIntField(COLLEGE_TEAM_ID); }
            set { SetField(COLLEGE_TEAM_ID, value); }
        }

        public bool College_coiw
        {
            get { return (GetIntField(COLLEGE_COIW) == 1); }

            set { SetField(COLLEGE_COIW, Convert.ToInt32(value)); }	
        }

        public string College_enun
        {
            get { return GetStringField(COLLEGE_ENUN); }
            set { SetField(COLLEGE_ENUN, value); }
        }

        public int College_enuv
        {
            get { return GetIntField(COLLEGE_ENUV); }
            set { SetField(COLLEGE_ENUV, value); }
        }

        public int College_pexp
        {
            get { return GetIntField(COLLEGE_PEXP); }
            set { SetField(COLLEGE_PEXP, value); }
        }

        public int College_ppop
        {
            get { return GetIntField(COLLEGE_PPOP); }
            set { SetField(COLLEGE_PPOP, value); }
        }

        public int College_pxpc
        {
            get { return GetIntField(COLLEGE_PXPC); }
            set { SetField(COLLEGE_PXPC, value); }
        }


        public CollegesRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}
    }
}
