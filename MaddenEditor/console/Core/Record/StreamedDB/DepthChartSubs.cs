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
    public class DepthChartSubs : TableRecordModel
    {
        public const string PLAYER_POS1 = "PLA1";
        public const string PLAYER_PERC1 = "PLP1";
        public const string PLAYER_POS2 = "PLA2";
        public const string PLAYER_PERC2 = "PLP2";
        public const string PLAYER_POS3 = "PLA3";
        public const string PLAYER_PERC3 = "PLP3";
        public const string PLAYER_SUB_POS = "PLSB";
        public const string DEPTH_POS = "DPTG";
        public const string PLAYER_POSITION = "PPOS";
        
        public DepthChartSubs(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{
		}

        public int PlayerPos1
        {
            get { return GetIntField(PLAYER_POS1); }
            set { SetField(PLAYER_POS1, value); }
        }
        public int PlayerPerc1
        {
            get { return GetIntField(PLAYER_PERC1); }
            set { SetField(PLAYER_PERC1, value); }
        }        
        public int PlayerPos2
        {
            get { return GetIntField(PLAYER_POS2); }
            set { SetField(PLAYER_POS2, value); }
        }
        public int PlayerPerc2
        {
            get { return GetIntField(PLAYER_PERC2); }
            set { SetField(PLAYER_PERC2, value); }
        } 
        public int PlayerPos3
        {
            get { return GetIntField(PLAYER_POS3); }
            set { SetField(PLAYER_POS3, value); }
        }
        public int PlayerPerc3
        {
            get { return GetIntField(PLAYER_PERC3); }
            set { SetField(PLAYER_PERC3, value); }
        }
        public int PlayerSub
        {
            get { return GetIntField(PLAYER_SUB_POS); }
            set { SetField(PLAYER_SUB_POS, value); }
        }
        public int DepthPos
        {
            get { return GetIntField(DEPTH_POS); }
            set { SetField(DEPTH_POS, value); }
        }
        public int Position
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        } 
    }
}
