using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Core
{
    public class FRAPlayBooks : TableRecordModel
    {
        public const string BOOK_ID = "BGID";
        public const string NAME = "BNAM";
        public const string BOOK_IS_OFFENSE = "BOFF";
        public const string BOOK_ORDER = "BORD";
        public const string IS_34 = "PI34";

        public FRAPlayBooks(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        #region Get/Set

        public int BookID
        {
            get { return GetIntField(BOOK_ID); }
            set { SetField(BOOK_ID, value); }
        }

        public string BookName
        {
            get { return GetStringField(NAME); }
            set { SetField(NAME, value); }
        }

        public bool BookisOffense
        {
            get { return (GetIntField(BOOK_IS_OFFENSE) == 1); }
            set { SetField(BOOK_IS_OFFENSE, Convert.ToInt32(value)); }
        }

        public int BookOrder
        {
            get { return GetIntField(BOOK_ORDER); }
            set { SetField(BOOK_ORDER, value); }
        }

        public bool Defis34
        {
            get { return (GetIntField(IS_34) == 1); }
            set {   SetField(IS_34, Convert.ToInt32(value)); }
        }

        #endregion

    }
}
