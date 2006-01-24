using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class ResignPlayersStateRecord : TableRecordModel
    {
        public const string IN_RESIGN = "ROST";

        public ResignPlayersStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public bool AtResignPlayersStage
        {
            get
            {
                return (GetIntField(IN_RESIGN) == 1);
            }
            set
            {
                SetField(IN_RESIGN, (value == true ? 1 : 0));
            }
        }
    }
}
