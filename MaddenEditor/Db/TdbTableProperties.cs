using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Db
{
    struct TdbTableProperties
    {
        public String Name;
        public int FieldCount;
        public int Capacity;
        public int RecordCount;
        public int DeletedCount;
        public int NextDeletedRecord;
        public bool Flag0;
        public bool Flag1;
        public bool Flag2;
        public bool Flag3;
        public bool NonAllocated;
    }

}
