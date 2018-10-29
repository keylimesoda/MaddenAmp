/******************************************************************************
 * MaddenAmp
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
	public class InjuryRecord : TableRecordModel
	{
        public const string INJURY_IR = "INIR";        
		public const string INJURY_LENGTH = "INJL";
        public const string INJURY_RETURN = "INJR";         //2019
        public const string INJURY_SEVERITY = "INJS";       //2019
		public const string INJURY_TYPE = "INJT";
        public const string INTW = "INTW";                  //2019
		public const string PLAYER_ID = "PGID";
		public const string TEAM_ID = "TGID";
		

		public InjuryRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

		public int InjuryLength
		{
			get
			{
				return GetIntField(INJURY_LENGTH);
			}
			set
			{
				SetField(INJURY_LENGTH, value);
			}
		}

		public int InjuryType
		{
			get
			{
				return (GetIntField(INJURY_TYPE) < 230 ? GetIntField(INJURY_TYPE) : 229);
			}
			set
			{
				SetField(INJURY_TYPE, value);
			}
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

		public bool IR
		{
            get { return (GetIntField(INJURY_IR) == 1); }
			set { SetField(INJURY_IR, Convert.ToInt32(value)); }
		}

        public int InjuryReturn
        {
            get { return GetIntField(INJURY_RETURN); }
            set { SetField(INJURY_RETURN, value); }
        }
        
        public int InjurySeverity
        {
            get { return GetIntField(INJURY_SEVERITY); }
            set { SetField(INJURY_SEVERITY, value); }
        }
        
        public int Intw
        {
            get { return GetIntField(INTW); }
            set { SetField(INTW, value); }
        }
        
        
        public String LengthDescription
		{
			get
			{
				int length = GetIntField(INJURY_LENGTH)%256;

				if (length == 0)
				{
					return "Ready To Play";
				}
				else if ((length >= 1 && length <= 9) || (length >= 257 && length <= 265))
				{
					return "Doubtful";
				}
				else if (length >= 10 && length <= 19)
				{
					return "Will return soon";
				}
				else if (length == 20)
				{
					return "1 quarter";
				}
				else if (length == 21)
				{
					return "2 quarters";
				}
				else if (length == 22)
				{
					return "3 quarters";
				}
				else if (length == 23)
				{
					return "Out for game";
				}
				else if (length >= 24 && length <= 243)
				{
					return (((length - 24)/20) + 1) + " week/s";
				}
				else if (length >= 244 && length < 254)
				{
					return "12 weeks";
				}
				else if (length == 254)
				{
					return "Out for season";
				}
				else if (length == 255)
				{
					return "Career ending";
				}
				return "";
			}
		}
	}
}
