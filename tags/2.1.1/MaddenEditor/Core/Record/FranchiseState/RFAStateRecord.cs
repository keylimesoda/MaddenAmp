using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class RFAStateRecord : TableRecordModel
    {
        public const string IN_RFA_STAGE = "SOST";

        public RFAStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public bool AtRFAStage
        {
            get
            {
                return (GetIntField(IN_RFA_STAGE) == 1);
            }
            set
            {
                SetField(IN_RFA_STAGE, (value == true ? 1 : 0));
            }
        }
    }
}
