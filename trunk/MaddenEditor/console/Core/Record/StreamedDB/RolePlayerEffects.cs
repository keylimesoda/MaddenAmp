/* MaddenAmp
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
    public class RolePlayerEffects : TableRecordModel
    {
        // Tablename RLPM in streameddata.db

        public const string PLAYER_FIELD = "PMOD";
        public const string MODIFIER = "PRMO";
        public const string PLAYER_ROLE = "PROL";
        public const string PLAYER_POSITION = "PPOS";


        public RolePlayerEffects(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {

        }

        public int Player_Field
        {
            get { return GetIntField(PLAYER_FIELD); }
            set { SetField(PLAYER_FIELD, value); }
        }

        public int Modifier
        {
            get { return GetIntField(MODIFIER); }
            set { SetField(MODIFIER, value); }
        }

        public int PlayerRole
        {
            get { return GetIntField(PLAYER_ROLE); }
            set { SetField(PLAYER_ROLE, value); }
        }

        public int PlayerPosition
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        }
        
        public string Player_Fieldname
        {
            get
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = BitConverter.GetBytes(Player_Field);
                byte[] final = new byte[3];
                Array.Copy(ba, 1, final, 0, 3);
                return a.GetString(final);
            }
            set
            {
                ASCIIEncoding a = new ASCIIEncoding();
                byte[] ba = a.GetBytes(value);                
                Player_Field = BitConverter.ToInt32(ba, 0);
            }
        }


    }
}
