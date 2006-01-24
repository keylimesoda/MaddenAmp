using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    class BoxScoreOffensiveLineRecord : TableRecordModel
    {
        public const string PANCAKES = "gopa";
        public const string SACKS_ALLOWED = "gosa";
        public const string SEASON = "SEYR";
        public const string WEEK = "SEWN";
        public const string TEAM_ID = "TGID";
        public const string PLAYER_ID = "PGID";

        public BoxScoreOffensiveLineRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int Pancakes
        {
            get
            {
                return GetIntField(PANCAKES);
            }
            set
            {
                SetField(PANCAKES, value);
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
