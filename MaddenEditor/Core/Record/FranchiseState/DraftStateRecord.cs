using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class DraftStateRecord : TableRecordModel
    {
        public const string IN_DRAFT = "DRST";

        public DraftStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public bool AtDraftStage
        {
            get
            {
                return (GetIntField(IN_DRAFT) == 1);
            }
            set
            {
                SetField(IN_DRAFT, (value == true ? 1 : 0));
            }
        }
    }
}
