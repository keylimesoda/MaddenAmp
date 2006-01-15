using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.Stats
{
    class CareerStatsOffensiveLineRecord : TableRecordModel
    {
        public const string PANCAKES = "copa";
        public const string SACKS_ALLOWED = "cosa";
        public const string PLAYER_ID = "PGID";

        public CareerStatsOffensiveLineRecord(int record, TableModel tableModel, EditorModel EditorModel)
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
    }
}
