using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public enum ScoutingStage
    {
        Initial=0
    }

    public class ScoutingStateRecord : TableRecordModel
    {
        public const string SCOUTING = "SSTA";

        public ScoutingStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int ScoutingStage
        {
            get
            {
                return GetIntField(SCOUTING);
            }
            set
            {
                SetField(SCOUTING, value);
            }
        }
    }
}
