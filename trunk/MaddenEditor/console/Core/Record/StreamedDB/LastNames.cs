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
    // "DRYR"
    public class LastNames : TableRecordModel
    {
        public const string PLAYER_LAST_NAME = "PLNA";
        public const string SPEECH_SLOT = "SPSL";

        public string FirstName
        {
            get { return GetStringField(PLAYER_LAST_NAME); }
            set { SetField(PLAYER_LAST_NAME, value); }
        }
        public int SpeechNumber
        {
            get { return GetIntField(SPEECH_SLOT); }
            set { SetField(SPEECH_SLOT, value); }
        }

        public LastNames(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

    }
}
