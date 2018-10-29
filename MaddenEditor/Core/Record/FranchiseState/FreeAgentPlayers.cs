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
using System.Linq;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class FreeAgentPlayers : TableRecordModel
    {
        // FAPL

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
        public const string PWRA = "PWRA";
        public const string PCSA = "PCSA";
        public const string PESA = "PESA";
        public const string PTSA = "PTSA";
        public const string SCWB = "SCWB";
        public const string FABD = "FABD";
        public const string FAHD = "FAHD";
        public const string PLAYER_ID = "PGID";
        public const string PLAYER_AGE = "PAGE";
        public const string FARE = "FARE";
        public const string PFIG = "PFIG";
        public const string SIPI = "SIPI";
        public const string PECL = "PECL";
        public const string PTEN = "PTEN";
        public const string PCON = "PCON";
        public const string PEBO = "PEBO";
        public const string PSBO = "PSBO";
        public const string PDRP = "PDRP";
        public const string PYRP = "PYRP";
        public const string PSRR = "PSRR";
        public const string POVR = "POVR";
        public const string PECS = "PECS";
        public const string PLAYER_POSITION = "PPOS";

        public int Pwra
        {
            get { return GetIntField(PWRA); }
            set { SetField(PWRA, value); }
        }
        public int Pcsa
        {
            get { return GetIntField(PCSA); }
            set { SetField(PCSA, value); }
        }
        public int Pesa
        {
            get { return GetIntField(PESA); }
            set { SetField(PESA, value); }
        }
        public int Ptsa
        {
            get { return GetIntField(PTSA); }
            set { SetField(PTSA, value); }
        }
        public int Scwb
        {
            get { return GetIntField(SCWB); }
            set { SetField(SCWB, value); }
        }
        public int Fabd
        {
            get { return GetIntField(FABD); }
            set { SetField(FABD, value); }
        }
        public int Fahd
        {
            get { return GetIntField(FAHD); }
            set { SetField(FAHD, value); }
        }
        public int PlayerId
        {
            get { return GetIntField(PLAYER_ID); }
            set { SetField(PLAYER_ID, value); }
        }
        public int PlayerAge
        {
            get { return GetIntField(PLAYER_AGE); }
            set { SetField(PLAYER_AGE, value); }
        }
        public int Fare
        {
            get { return GetIntField(FARE); }
            set { SetField(FARE, value); }
        }
        public int Pfig
        {
            get { return GetIntField(PFIG); }
            set { SetField(PFIG, value); }
        }
        public int Sipi
        {
            get { return GetIntField(SIPI); }
            set { SetField(SIPI, value); }
        }
        public int Pecl
        {
            get { return GetIntField(PECL); }
            set { SetField(PECL, value); }
        }
        public int Pten
        {
            get { return GetIntField(PTEN); }
            set { SetField(PTEN, value); }
        }
        public int Pcon
        {
            get { return GetIntField(PCON); }
            set { SetField(PCON, value); }
        }
        public int Pebo
        {
            get { return GetIntField(PEBO); }
            set { SetField(PEBO, value); }
        }
        public int Psbo
        {
            get { return GetIntField(PSBO); }
            set { SetField(PSBO, value); }
        }
        public int Pdrp
        {
            get { return GetIntField(PDRP); }
            set { SetField(PDRP, value); }
        }
        public int Pyrp
        {
            get { return GetIntField(PYRP); }
            set { SetField(PYRP, value); }
        }
        public int Psrr
        {
            get { return GetIntField(PSRR); }
            set { SetField(PSRR, value); }
        }
        public int Povr
        {
            get { return GetIntField(POVR); }
            set { SetField(POVR, value); }
        }
        public int Pecs
        {
            get { return GetIntField(PECS); }
            set { SetField(PECS, value); }
        }
        public int PlayerPosition
        {
            get { return GetIntField(PLAYER_POSITION); }
            set { SetField(PLAYER_POSITION, value); }
        }
        
        
        public int Tf00
        {
            get { return GetIntField(TF00); }
            set { SetField(TF00, value); }
        }
        public int Tf10
        {
            get { return GetIntField(TF10); }
            set { SetField(TF10, value); }
        }
        public int Tf20
        {
            get { return GetIntField(TF20); }
            set { SetField(TF20, value); }
        }
        public int Tf30
        {
            get { return GetIntField(TF30); }
            set { SetField(TF30, value); }
        }
        public int Tf01
        {
            get { return GetIntField(TF01); }
            set { SetField(TF01, value); }
        }
        public int Tf11
        {
            get { return GetIntField(TF11); }
            set { SetField(TF11, value); }
        }
        public int Tf21
        {
            get { return GetIntField(TF21); }
            set { SetField(TF21, value); }
        }
        public int Tf31
        {
            get { return GetIntField(TF31); }
            set { SetField(TF31, value); }
        }
        public int Tf02
        {
            get { return GetIntField(TF02); }
            set { SetField(TF02, value); }
        }
        public int Tf12
        {
            get { return GetIntField(TF12); }
            set { SetField(TF12, value); }
        }
        public int Tf22
        {
            get { return GetIntField(TF22); }
            set { SetField(TF22, value); }
        }
        public int Tf03
        {
            get { return GetIntField(TF03); }
            set { SetField(TF03, value); }
        }
        public int Tf13
        {
            get { return GetIntField(TF13); }
            set { SetField(TF13, value); }
        }
        public int Tf23
        {
            get { return GetIntField(TF23); }
            set { SetField(TF23, value); }
        }
        public int Tf04
        {
            get { return GetIntField(TF04); }
            set { SetField(TF04, value); }
        }
        public int Tf14
        {
            get { return GetIntField(TF14); }
            set { SetField(TF14, value); }
        }
        public int Tf24
        {
            get { return GetIntField(TF24); }
            set { SetField(TF24, value); }
        }
        public int Tf05
        {
            get { return GetIntField(TF00); }
            set { SetField(TF00, value); }
        }
        public int Tf15
        {
            get { return GetIntField(TF15); }
            set { SetField(TF15, value); }
        }
        public int Tf25
        {
            get { return GetIntField(TF25); }
            set { SetField(TF25, value); }
        }
        public int Tf06
        {
            get { return GetIntField(TF06); }
            set { SetField(TF06, value); }
        }
        public int Tf16
        {
            get { return GetIntField(TF16); }
            set { SetField(TF16, value); }
        }
        public int Tf26
        {
            get { return GetIntField(TF26); }
            set { SetField(TF26, value); }
        }
        public int Tf07
        {
            get { return GetIntField(TF07); }
            set { SetField(TF07, value); }
        }
        public int Tf17
        {
            get { return GetIntField(TF17); }
            set { SetField(TF17, value); }
        }
        public int Tf27
        {
            get { return GetIntField(TF27); }
            set { SetField(TF27, value); }
        }
        public int Tf08
        {
            get { return GetIntField(TF08); }
            set { SetField(TF08, value); }
        }
        public int Tf18
        {
            get { return GetIntField(TF18); }
            set { SetField(TF18, value); }
        }
        public int Tf28
        {
            get { return GetIntField(TF28); }
            set { SetField(TF28, value); }
        }
        public int Tf09
        {
            get { return GetIntField(TF09); }
            set { SetField(TF09, value); }
        }
        public int Tf19
        {
            get { return GetIntField(TF19); }
            set { SetField(TF19, value); }
        }
        public int Tf29
        {
            get { return GetIntField(TF29); }
            set { SetField(TF29, value); }
        }
        
        public FreeAgentPlayers(int record, TableModel tableModel, EditorModel EditorModel)
            : base(record, tableModel, EditorModel)
        {
        }
		

    }
}
