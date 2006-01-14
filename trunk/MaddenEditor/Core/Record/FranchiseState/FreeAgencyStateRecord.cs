using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class FreeAgencyStateRecord : TableRecordModel
    {
        public const string IN_FREE_AGENCY = "SOST";
        public const string DAYS_REMAINING = "PSOD";

        public FreeAgencyStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int DaysRemaining
        {
            get
            {
                return GetIntField(DAYS_REMAINING);
            }
            set
            {
                SetField(DAYS_REMAINING, value);
            }
        }

        public bool AtFreeAgencyStage
        {
            get
            {
                return (GetIntField(IN_FREE_AGENCY) == 1);
            }
            set
            {
                SetField(IN_FREE_AGENCY, (value == true ? 1 : 0));
            }
        }
    }
}
