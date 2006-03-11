using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    public class SeasonStatsOffenseRecord : TableRecordModel
    {
        public const string INTERCEPTIONS = "sain";
        public const string SACKS = "sasa";
        public const string PASSING_YARDS = "saya";
        public const string PASSING_ATTEMPTS = "saat";
        public const string RECEIVING_YARDS = "scya";
        public const string FUMBLES = "sufu";
        public const string RUSHING_ATTEMPTS = "suat";
        public const string RUSHING_YARDS = "suya";
        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";

		public SeasonStatsOffenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int Interceptions
        {
            get
            {
                return GetIntField(INTERCEPTIONS);
            }
            set
            {
                SetField(INTERCEPTIONS, value);
            }
        }

        public int PassingAttempts
        {
            get
            {
                return GetIntField(PASSING_ATTEMPTS);
            }
            set
            {
                SetField(PASSING_ATTEMPTS, value);
            }
        }

        public int Sacks
        {
            get
            {
                return GetIntField(SACKS);
            }
            set
            {
                SetField(SACKS, value);
            }
        }

        public int PassingYards
        {
            get
            {
                return GetIntField(PASSING_YARDS);
            }
            set
            {
                SetField(PASSING_YARDS, value);
            }
        }

        public int ReceivingYards
        {
            get
            {
                return GetIntField(RECEIVING_YARDS);
            }
            set
            {
                SetField(RECEIVING_YARDS, value);
            }
        }

        public int Fumbles
        {
            get
            {
                return GetIntField(FUMBLES);
            }
            set
            {
                SetField(FUMBLES, value);
            }
        }

        public int RushingAttempts
        {
            get
            {
                return GetIntField(RUSHING_ATTEMPTS);
            }
            set
            {
                SetField(RUSHING_ATTEMPTS, value);
            }
        }

        public int RushingYards
        {
            get
            {
                return GetIntField(RUSHING_YARDS);
            }
            set
            {
                SetField(RUSHING_YARDS, value);
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