/******************************************************************************
 * Gommo's Madden Editor
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
	public class DepthChartRecord : TableRecordModel
	{
        public const string PLAYER_ID = "PGID";
        public const string TEAM_ID = "TGID";
        public const string POSITION_ID = "PPOS";
        public const string DEPTH_ORDER = "ddep";

		public DepthChartRecord(int record, EditorModel EditorModel)
			: base(record, EditorModel)
		{

		}

        public int PlayerId
        {
            get
            {
                return GetIntField(PLAYER_ID);
            }
            set
            {
                SetField(PLAYER_ID, value);
            }
        }

        public int TeamId
        {
            get
            {
                return GetIntField(TEAM_ID);
            }
            set
            {
                SetField(TEAM_ID, value);
            }
        }

        public int PositionId
        {
            get
            {
                return GetIntField(POSITION_ID);
            }
            set
            {
                SetField(POSITION_ID, value);
            }
        }

        public int DepthOrder
        {
            get
            {
                return GetIntField(DEPTH_ORDER);
            }
            set
            {
                SetField(DEPTH_ORDER, value);
            }
        }
	}
}