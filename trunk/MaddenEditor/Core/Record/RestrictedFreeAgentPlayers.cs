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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaddenEditor.Core.Record
{
    public class RestrictedFreeAgentPlayers : TableRecordModel
    {
        // RFPL
        // Multiple teams can offer RFA Players

        public const string OFFERED_TEAMID = "FABD";                // 1023 free agent, player was not offered
        public const string FARE = "FARE";
        public const string PLAYER_AGE = "PAGE";
        public const string OFFERED_CONTRACT_LENGTH = "PCON";
        public const string OFFERED_CURRENT_SALARY = "PCSA";
        public const string DRAFTED_POSITION = "PDRP";
        public const string EXPECTED_BONUS = "PEBO";
        public const string EXPECTED_CONTRACT_LENGTH = "PECL";
        public const string EXPECTED_CURRENT_SALARY = "PECS";       // Expected first year salary
        public const string EXPECTED_TOTAL_SALARY = "PESA";
        public const string PFIG = "PFIG";
        public const string PLAYER_ID = "PGID";
        public const string PLAYER_OVERALL = "POVR";
        public const string PLAYER_POSITION = "PPOS";
        public const string PREVIOUS_OFFERED_TOTAL_SALARY = "PRTS";
        public const string OFFERED_BONUS = "PSBO";
        public const string PLAYER_TENDENCY = "PTEN";
        public const string OFFERED_TOTAL_SALARY = "PTSA";
        public const string PLAYER_WEIGHTED_RATING = "PWRA";        //  this will be 0 if the player is not offered, cannot be signed until after RFA
        public const string COMP_PICK_1 = "SDP1";
        public const string COMP_PICK_2 = "SDP2";
        public const string SIPI = "SIPI";
        public const string TEAMID = "TGID";

        #region Teams 0-31
        public const string TF00 = "TF00";
        public const string TF10 = "TF10";
        public const string TF20 = "TF20";
        public const string TF30 = "TF30";
        public const string TF01 = "TF01";
        public const string TF11 = "TF11";
        public const string TF21 = "TF21";
        public const string TF31 = "TF31";
        public const string TF02 = "TF02";
        public const string TF12 = "TF12";
        public const string TF22 = "TF22";
        public const string TF03 = "TF03";
        public const string TF13 = "TF13";
        public const string TF23 = "TF23";
        public const string TF04 = "TF04";
        public const string TF14 = "TF14";
        public const string TF24 = "TF24";
        public const string TF05 = "TF05";
        public const string TF15 = "TF15";
        public const string TF25 = "TF25";
        public const string TF06 = "TF06";
        public const string TF16 = "TF16";
        public const string TF26 = "TF26";
        public const string TF07 = "TF07";
        public const string TF17 = "TF17";
        public const string TF27 = "TF27";
        public const string TF08 = "TF08";
        public const string TF18 = "TF18";
        public const string TF28 = "TF28";
        public const string TF09 = "TF09";
        public const string TF19 = "TF19";
        public const string TF29 = "TF29";
        #endregion

        public RestrictedFreeAgentPlayers(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {
        }

        #region Get/Set

        public int OfferedTeamID
        {
            get { return GetIntField(OFFERED_TEAMID); }
            set { SetField(OFFERED_TEAMID, value); }
        }
        public int Fare
        {
            get { return GetIntField(FARE); }
            set { SetField(FARE, value); }
        }
        public int PlayerAge
        {
            get { return GetIntField(PLAYER_AGE); }
            set { SetField(PLAYER_AGE, value); }
        }
        public int OfferedContractLength
        {
            get { return GetIntField(OFFERED_CONTRACT_LENGTH); }
            set { SetField(OFFERED_CONTRACT_LENGTH, value); }
        }
        public int OfferedCurrentSalary
        {
            get { return GetIntField(OFFERED_CURRENT_SALARY); }
            set { SetField(OFFERED_CURRENT_SALARY, value); }
        }
        public int DraftedPosition
        {
            get { return GetIntField(DRAFTED_POSITION); }
            set { SetField(DRAFTED_POSITION, value); }
        }
        public int ExpectBonus
        {
            get { return GetIntField(EXPECTED_BONUS); }
            set { SetField(EXPECTED_BONUS, value); }
        }
        public int ExpectContractLength
        {
            get { return GetIntField(EXPECTED_CONTRACT_LENGTH); }
            set { SetField(EXPECTED_CONTRACT_LENGTH, value); }
        }
        public int ExpectedCurrentSalary
        {
            get { return GetIntField(EXPECTED_CURRENT_SALARY); }
            set { SetField(EXPECTED_CURRENT_SALARY, value); }
        }
        public int ExpectedTotalSalary
        {
            get { return GetIntField(EXPECTED_TOTAL_SALARY); }
            set { SetField(EXPECTED_TOTAL_SALARY, value); }
        }
        public int Pfig
        {
            get { return GetIntField(PFIG); }
            set { SetField(PFIG, value); }
        }
        public int PlayerId
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int PlayerOverall
        {
            get { return GetIntField(PLAYER_OVERALL); }
            set { SetField(PLAYER_OVERALL, value); }
        }
        public int PlayerPosition
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        }
        public int PreviousOfferedTotalSalary
        {
            get { return GetIntField(PREVIOUS_OFFERED_TOTAL_SALARY); }
            set { SetField(PREVIOUS_OFFERED_TOTAL_SALARY, value); }
        }
        public int OfferedBonus
        {
            get { return GetIntField(OFFERED_BONUS); }
            set { SetField(OFFERED_BONUS, value); }
        }
        public int PlayerTendency
        {
            get { return GetIntField(PLAYER_TENDENCY); }
            set { SetField(PLAYER_TENDENCY, value); }
        }
        public int OfferedTotalSalary
        {
            get { return GetIntField(OFFERED_TOTAL_SALARY); }
            set { SetField(OFFERED_TOTAL_SALARY, value); }
        }       
        public int PlayerWeightedRating
        {
            get { return GetIntField(PLAYER_WEIGHTED_RATING); }
            set { SetField(PLAYER_WEIGHTED_RATING, value); }
        }
        public int CompPick1
        {
            get { return GetIntField(COMP_PICK_1); }
            set { SetField(COMP_PICK_1, value); }
        }
        public int CompPick2
        {
            get { return GetIntField(COMP_PICK_2); }
            set { SetField(COMP_PICK_2, value); }
        }       
        public int Sipi
        {
            get { return GetIntField(SIPI); }
            set { SetField(SIPI, value); }
        }
        public int TeamID
        {
            get { return GetIntField(TEAMID); }
            set { SetField(TEAMID, value); }
        }
        
        #endregion

        #region Get Set  Teams00-31
        public bool Team00
        {
            get { return (GetIntField(TF00) == 1); }
            set { SetField(TF00, Convert.ToInt32(value)); }
        }
        public bool Team01
        {
            get { return (GetIntField(TF01) == 1); }
            set { SetField(TF01, Convert.ToInt32(value)); }
        }
        public bool Team02
        {
            get { return (GetIntField(TF02) == 1); }
            set { SetField(TF02, Convert.ToInt32(value)); }
        }
        public bool Team03
        {
            get { return (GetIntField(TF03) == 1); }
            set { SetField(TF03, Convert.ToInt32(value)); }
        }
        public bool Team04
        {
            get { return (GetIntField(TF04) == 1); }
            set { SetField(TF04, Convert.ToInt32(value)); }
        }
        public bool Team05
        {
            get { return (GetIntField(TF05) == 1); }
            set { SetField(TF05, Convert.ToInt32(value)); }
        }
        public bool Team06
        {
            get { return (GetIntField(TF06) == 1); }
            set { SetField(TF06, Convert.ToInt32(value)); }
        }
        public bool Team07
        {
            get { return (GetIntField(TF07) == 1); }
            set { SetField(TF07, Convert.ToInt32(value)); }
        }
        public bool Team08
        {
            get { return (GetIntField(TF08) == 1); }
            set { SetField(TF08, Convert.ToInt32(value)); }
        }
        public bool Team09
        {
            get { return (GetIntField(TF09) == 1); }
            set { SetField(TF09, Convert.ToInt32(value)); }
        }
        public bool Team10
        {
            get { return (GetIntField(TF10) == 1); }
            set { SetField(TF10, Convert.ToInt32(value)); }
        }
        public bool Team11
        {
            get { return (GetIntField(TF11) == 1); }
            set { SetField(TF11, Convert.ToInt32(value)); }
        }
        public bool Team12
        {
            get { return (GetIntField(TF12) == 1); }
            set { SetField(TF12, Convert.ToInt32(value)); }
        }
        public bool Team13
        {
            get { return (GetIntField(TF13) == 1); }
            set { SetField(TF13, Convert.ToInt32(value)); }
        }
        public bool Team14
        {
            get { return (GetIntField(TF14) == 1); }
            set { SetField(TF14, Convert.ToInt32(value)); }
        }
        public bool Team15
        {
            get { return (GetIntField(TF15) == 1); }
            set { SetField(TF15, Convert.ToInt32(value)); }
        }
        public bool Team16
        {
            get { return (GetIntField(TF16) == 1); }
            set { SetField(TF16, Convert.ToInt32(value)); }
        }
        public bool Team17
        {
            get { return (GetIntField(TF17) == 1); }
            set { SetField(TF17, Convert.ToInt32(value)); }
        }
        public bool Team18
        {
            get { return (GetIntField(TF18) == 1); }
            set { SetField(TF18, Convert.ToInt32(value)); }
        }
        public bool Team19
        {
            get { return (GetIntField(TF19) == 1); }
            set { SetField(TF19, Convert.ToInt32(value)); }
        }
        public bool Team20
        {
            get { return (GetIntField(TF20) == 1); }
            set { SetField(TF20, Convert.ToInt32(value)); }
        }
        public bool Team21
        {
            get { return (GetIntField(TF21) == 1); }
            set { SetField(TF21, Convert.ToInt32(value)); }
        }
        public bool Team22
        {
            get { return (GetIntField(TF22) == 1); }
            set { SetField(TF22, Convert.ToInt32(value)); }
        }
        public bool Team23
        {
            get { return (GetIntField(TF23) == 1); }
            set { SetField(TF23, Convert.ToInt32(value)); }
        }
        public bool Team24
        {
            get { return (GetIntField(TF24) == 1); }
            set { SetField(TF24, Convert.ToInt32(value)); }
        }
        public bool Team25
        {
            get { return (GetIntField(TF25) == 1); }
            set { SetField(TF25, Convert.ToInt32(value)); }
        }
        public bool Team26
        {
            get { return (GetIntField(TF26) == 1); }
            set { SetField(TF26, Convert.ToInt32(value)); }
        }
        public bool Team27
        {
            get { return (GetIntField(TF27) == 1); }
            set { SetField(TF27, Convert.ToInt32(value)); }
        }
        public bool Team28
        {
            get { return (GetIntField(TF28) == 1); }
            set { SetField(TF28, Convert.ToInt32(value)); }
        }
        public bool Team29
        {
            get { return (GetIntField(TF29) == 1); }
            set { SetField(TF29, Convert.ToInt32(value)); }
        }
        public bool Team30
        {
            get { return (GetIntField(TF30) == 1); }
            set { SetField(TF30, Convert.ToInt32(value)); }
        }
        public bool Team31
        {
            get { return (GetIntField(TF31) == 1); }
            set { SetField(TF31, Convert.ToInt32(value)); }
        }
        #endregion


    }
}

