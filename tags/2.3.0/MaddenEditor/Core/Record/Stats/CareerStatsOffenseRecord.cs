using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    public class CareerStatsOffenseRecord : TableRecordModel
    {
        public const string INTERCEPTIONS = "cain";
        public const string SACKS = "casa";
        public const string PASSING_YARDS = "caya";
        public const string RECEIVING_YARDS = "ccya";
        public const string FUMBLES = "cufu";
        public const string RUSHING_ATTEMPTS = "cuat";
        public const string RUSHING_YARDS = "cuya";
        public const string PLAYER_ID = "PGID";

        public CareerStatsOffenseRecord(int record, TableModel tableModel, EditorModel EditorModel)
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
    }
}
