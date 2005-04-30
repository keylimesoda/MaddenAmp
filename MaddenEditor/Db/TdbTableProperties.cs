using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Db
{
    struct TdbTableProperties
    {
        String Name;
        int FieldCount;
        int Capacity;
        int RecordCount;
        int DeletedCount;
        int NextDeletedRecord;
        bool Flag0;
        bool Flag1;
        bool Flag2;
        bool Flag3;
        bool NonAllocated;
    }

}
