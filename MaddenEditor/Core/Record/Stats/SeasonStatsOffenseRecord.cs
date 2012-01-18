using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    public class SeasonStatsOffenseRecord : TableRecordModel
    {
        // QB Stats
        public const string SEA_PASS_INT = "sain";
        public const string SEA_SACKED = "sasa";
        public const string SEA_PASS_YDS = "saya";
        public const string SEA_PASS_ATT = "saat";
        public const string SEA_PASS_LONG = "saln";
        public const string SEA_COMP = "sacm";
        public const string SEA_PASS_TD = "satd";

        // WR Stats
        public const string SEA_REC_YDS = "scya";
        public const string SEA_REC = "scca";
        public const string SEA_DROPS = "scdr";
        public const string SEA_REC_LONG = "scrL";
        public const string SEA_REC_TD = "sctd";
        public const string SEA_REC_YAC = "scyc";
        
        // RB Stats
        public const string SEA_FUMBLES = "sufu";
        public const string SEA_RUSH_ATT = "suat";
        public const string SEA_RUSH_YDS = "suya";
        public const string SEA_RUSH_BTK = "subt";
        public const string SEA_RUSH_LONG = "suln";
        public const string SEA_RUSH_TD = "sutd";
        public const string SEA_RUSH_YAC = "suyh";
        public const string SEA_RUSH_20 = "su2y";

        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";

		public SeasonStatsOffenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

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
    }
}
