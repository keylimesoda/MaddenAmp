using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    class BoxScoreTeamRecord : TableRecordModel
    {
        public const string TOTAL_YARDS = "tsTy";
        public const string TOTAL_OFFENSE = "tsoy";
        public const string PASSING_YARDS = "tsop";
        public const string RUSHING_YARDS = "tsor";
        public const string PASSING_TDS = "tdPt";
        public const string RUSHING_TDS = "tsrt";
        public const string RUSHING_ATTEMPTS = "tsra";
        public const string FIRST_DOWNS = "ts1d";
        public const string THIRD_DOWN_ATTEMPTS = "ts3d";
        public const string THIRD_DOWN_CONVERSIONS = "ts3c";
        public const string FOURTH_DOWN_ATTEMPTS = "ts4d";
        public const string FOURTH_DOWN_CONVERSIONS = "ts4c";
        public const string SACKS_ALLOWED = "tssa";
        public const string SACKS = "tssk";
        public const string INTERCEPTIONS_CAUGHT = "tsDi";
        public const string FUMBLES_RECOVERED = "tsfr";
        public const string INTERCEPTIONS_THROWN = "tspi";
        public const string FUMBLES_LOST = "tsfl";

        public const string TEAM_ID = "TGID";
        public const string SEASON = "SEYR";
        public const string WEEK = "SEWN";

        public BoxScoreTeamRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

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

        public int InterceptionsThrown
        {
            get
            {
                return GetIntField(INTERCEPTIONS_THROWN);
            }
            set
            {
                SetField(INTERCEPTIONS_THROWN, value);
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

        public int InterceptionsCaught
        {
            get
            {
                return GetIntField(INTERCEPTIONS_CAUGHT);
            }
            set
            {
                SetField(INTERCEPTIONS_CAUGHT, value);
            }
        }

        public int TotalYards
        {
            get
            {
                return GetIntField(TOTAL_YARDS);
            }
            set
            {
                SetField(TOTAL_YARDS, value);
            }
        }

        public int TotalOffense
        {
            get
            {
                return GetIntField(TOTAL_OFFENSE);
            }
            set
            {
                SetField(TOTAL_OFFENSE, value);
            }
        }

        public int PassingTDs
        {
            get
            {
                return GetIntField(PASSING_TDS);
            }
            set
            {
                SetField(PASSING_TDS, value);
            }
        }

        public int RushingTDs
        {
            get
            {
                return GetIntField(RUSHING_TDS);
            }
            set
            {
                SetField(RUSHING_TDS, value);
            }
        }

        public int FirstDowns
        {
            get
            {
                return GetIntField(FIRST_DOWNS);
            }
            set
            {
                SetField(FIRST_DOWNS, value);
            }
        }

        public int ThirdDownAttempts
        {
            get
            {
                return GetIntField(THIRD_DOWN_ATTEMPTS);
            }
            set
            {
                SetField(THIRD_DOWN_ATTEMPTS, value);
            }
        }

        public int ThirdDownConversions
        {
            get
            {
                return GetIntField(THIRD_DOWN_CONVERSIONS);
            }
            set
            {
                SetField(THIRD_DOWN_CONVERSIONS, value);
            }
        }

        public int FourthDownAttempts
        {
            get
            {
                return GetIntField(FOURTH_DOWN_ATTEMPTS);
            }
            set
            {
                SetField(FOURTH_DOWN_ATTEMPTS, value);
            }
        }

        public int FourthDownConversions
        {
            get
            {
                return GetIntField(FOURTH_DOWN_CONVERSIONS);
            }
            set
            {
                SetField(FOURTH_DOWN_CONVERSIONS, value);
            }
        }

        public int SacksAllowed
        {
            get
            {
                return GetIntField(SACKS_ALLOWED);
            }
            set
            {
                SetField(SACKS_ALLOWED, value);
            }
        }

        public int FumblesRecovered
        {
            get
            {
                return GetIntField(FUMBLES_RECOVERED);
            }
            set
            {
                SetField(FUMBLES_RECOVERED, value);
            }
        }

        public int FumblesLost
        {
            get
            {
                return GetIntField(FUMBLES_LOST);
            }
            set
            {
                SetField(FUMBLES_LOST, value);
            }
        }
    }
}
