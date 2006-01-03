using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    public class BoxScoreOffenseRecord : TableRecordModel
    {
        public const string INTERCEPTIONS = "gain";
        public const string SACKS = "gasa";
        public const string PASSING_YARDS = "gaya";
        public const string RECEIVING_YARDS = "gcya";
        public const string FUMBLES = "gufu";
        public const string RUSHING_ATTEMPTS = "guat";
        public const string RUSHING_YARDS = "guya";
        public const string PLAYER_ID = "PGID";
        public const string SEASON = "SEYR";
        public const string WEEK = "SEWN";
        public const string TEAM_ID = "TGID";

		public BoxScoreOffenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
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

        public int Week
        {
            get
            {
                return GetIntField(WEEK);
            }
            set
            {
                SetField(WEEK, value);
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
    }
}
