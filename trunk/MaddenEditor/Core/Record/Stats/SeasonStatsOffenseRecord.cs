/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2014 Stingray68
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
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    public class SeasonStatsOffenseRecord : TableRecordModel
    {
        //  PSOF

        #region Fields

        public const string PLAYER_ID = "PGID";
        public const string SEA_PASS_ATT = "saat";
        public const string SEA_PASS_COMEBACKS = "sacb";                        //  2007-2008
        public const string SEA_COMP = "sacm";
        public const string SEA_PASS_FIRST_DOWNS = "safd";                      //  2007-2008
        public const string SEA_PASS_INT = "sain";
        public const string SEA_PASS_LONG = "saln";
        public const string SEA_SACKED = "sasa";
        public const string SEA_PASS_TD = "satd"; 
        public const string SEA_PASS_YDS = "saya";
        public const string SEA_REC = "scca";
        public const string SEA_DROPS = "scdr";
        public const string SEA_REC_LONG = "scrL";
        public const string SEA_REC_TD = "sctd";
        public const string SEA_REC_YDS = "scya";
        public const string SEA_REC_YAC = "scyc";
        public const string SEASON = "SEYR";
        public const string SEA_RUSH_20 = "su2y";
        public const string SEA_RUSH_ATT = "suat";
        public const string SEA_RUSH_BTK = "subt";
        public const string SEA_FUMBLES = "sufu";
        public const string SEA_RUSH_LONG = "suln";
        public const string SEA_RUSH_TD = "sutd";
        public const string SEA_RUSH_YDS = "suya";
        public const string SEA_RUSH_YAC = "suyh";

        #endregion

        public SeasonStatsOffenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        #region Get / Set

        #region Passing Stats

        public int SeaPassInt
        {
            get
            {
                return GetIntField(SEA_PASS_INT);
            }
            set
            {
                SetField(SEA_PASS_INT, value);
            }
        }

        public int SeaPassAtt
        {
            get
            {
                return GetIntField(SEA_PASS_ATT);
            }
            set
            {
                SetField(SEA_PASS_ATT, value);
            }
        }

        public int SeaSacked
        {
            get
            {
                return GetIntField(SEA_SACKED);
            }
            set
            {
                SetField(SEA_SACKED, value);
            }
        }

        public int SeaPassYds
        {
            get
            {
                return GetIntField(SEA_PASS_YDS);
            }
            set
            {
                SetField(SEA_PASS_YDS, value);
            }
        }

        public int SeaPassLong
        {
            get
            {
                return GetIntField(SEA_PASS_LONG);
            }
            set
            {
                SetField(SEA_PASS_LONG, value);
            }
        }
        
        public int SeaComp
        {
            get
            {
                return GetIntField(SEA_COMP);
            }
            set
            {
                SetField(SEA_COMP, value);
            }
        }

        public int SeaPassTd
        {
            get
            {
                return GetIntField(SEA_PASS_TD);
            }
            set
            {
                SetField(SEA_PASS_TD, value);
            }
        }

        public int SeaComebacks
        {
            get { return GetIntField(SEA_PASS_COMEBACKS); }
            set { SetField(SEA_PASS_COMEBACKS, value); }
        }
        
        public int SeaFirstDowns
        {
            get { return GetIntField(SEA_PASS_FIRST_DOWNS); }
            set { SetField(SEA_PASS_FIRST_DOWNS, value); }
        }

        #endregion

        #region Receiving Stats

        public int SeaRecYds
        {
            get
            {
                return GetIntField(SEA_REC_YDS);
            }
            set
            {
                SetField(SEA_REC_YDS, value);
            }
        }

        public int SeaRec
        {
            get
            {
                return GetIntField(SEA_REC);
            }
            set
            {
                SetField(SEA_REC, value);
            }
        }

        public int SeaDrops
        {
            get
            {
                return GetIntField(SEA_DROPS);
            }
            set
            {
                SetField(SEA_DROPS, value);
            }
        }

        public int SeaRecLong
        {
            get
            {
                return GetIntField(SEA_REC_LONG);
            }
            set
            {
                SetField(SEA_REC_LONG, value);
            }
        }

        public int SeaRecTd
        {
            get
            {
                return GetIntField(SEA_REC_TD);
            }
            set
            {
                SetField(SEA_REC_TD, value);
            }
        }

        public int SeaRecYac
        {
            get
            {
                return GetIntField(SEA_REC_YAC);
            }
            set
            {
                SetField(SEA_REC_YAC, value);
            }
        }

        #endregion

        #region Rushing Stats

        public int SeaFumbles
        {
            get
            {
                return GetIntField(SEA_FUMBLES);
            }
            set
            {
                SetField(SEA_FUMBLES, value);
            }
        }

        public int SeaRushAtt
        {
            get
            {
                return GetIntField(SEA_RUSH_ATT);
            }
            set
            {
                SetField(SEA_RUSH_ATT, value);
            }
        }

        public int SeaRushYds
        {
            get
            {
                return GetIntField(SEA_RUSH_YDS);
            }
            set
            {
                SetField(SEA_RUSH_YDS, value);
            }
        }

        public int SeaRushBtk
        {
            get
            {
                return GetIntField(SEA_RUSH_BTK);
            }
            set
            {
                SetField(SEA_RUSH_BTK, value);
            }
        }

        public int SeaRushLong
        {
            get
            {
                return GetIntField(SEA_RUSH_LONG);
            }
            set
            {
                SetField(SEA_RUSH_LONG, value);
            }
        }

        public int SeaRushTd
        {
            get
            {
                return GetIntField(SEA_RUSH_TD);
            }
            set
            {
                SetField(SEA_RUSH_TD, value);
            }
        }

        public int SeaRushYac
        {
            get
            {
                return GetIntField(SEA_RUSH_YAC);
            }
            set
            {
                SetField(SEA_RUSH_YAC, value);
            }
        }

        public int SeaRush20
        {
            get
            {
                return GetIntField(SEA_RUSH_20);
            }
            set
            {
                SetField(SEA_RUSH_20, value);
            }
        }

        #endregion

        #region Common Fields

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

        public int Season
        {
            get
            {
                return GetIntField(SEASON);
            }
            set
            {
                SetField(SEASON, value);
            }
        }

        #endregion 

        #endregion

    }
}
