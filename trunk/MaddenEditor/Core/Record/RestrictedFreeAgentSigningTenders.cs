using MaddenEditor.Core;
/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 Stingray68
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


namespace MaddenEditor.Core.Record
{
    public class RestrictedFreeAgentSigningTenders : TableRecordModel
    {
        // RFST
        public const string FARE = "FARE";
        public const string CONTRACT_LENGTH = "PCON";
        public const string PLAYER_ID = "PGID";
        public const string SIGNING_BONUS = "PSBO";
        public const string PSIS = "PSIS";
        public const string TENDERED_SALARY = "PTSA";
        public const string TEAM_ID = "TGID";

        public RestrictedFreeAgentSigningTenders(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int ContractLength
        {
            get { return GetIntField(CONTRACT_LENGTH); }
            set { SetField(CONTRACT_LENGTH, value); }
        }
        public int PlayerID
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int SigningBonus
        {
            get { return GetIntField(SIGNING_BONUS); }
            set { SetField(SIGNING_BONUS, value); }
        }
        public int TenderedSalary
        {
            get { return GetIntField(TENDERED_SALARY); }
            set { SetField(TENDERED_SALARY, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAM_ID); }
            set { SetField(TEAM_ID, value); }
        }

        public int Fare
        {
            get { return GetIntField(FARE); }
            set { SetField(FARE, value); }
        }
        public int Psis
        {
            get { return GetIntField(PSIS); }
            set { SetField(PSIS, value); }
        }
    }
}
