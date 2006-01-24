using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record.FranchiseState
{
    public class FranchiseStageRecord : TableRecordModel
    {
        public const string MCSA = "MCSA";
        public const string MNAI = "MNAI";
        public const string MPSA = "MPSA";

        public FranchiseStageRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

        }

        public int Mcsa
        {
            get
            {
                return GetIntField(MCSA);
            }
            set
            {
                SetField(MCSA, value);
            }
        }

        public int Mnai
        {
            get
            {
                return GetIntField(MNAI);
            }
            set
            {
                SetField(MNAI, value);
            }
        }

        public int Mpsa
        {
            get
            {
                return GetIntField(MPSA);
            }
            set
            {
                SetField(MPSA, value);
            }
        }
    }
}
