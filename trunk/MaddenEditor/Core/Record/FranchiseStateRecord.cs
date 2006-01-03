using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
    public class FranchiseStateRecord : TableRecordModel
    {
        public const string SEASON = "SEYR";
        public const string WEEK = "SEWN";

		public FranchiseStateRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

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
    }
}
