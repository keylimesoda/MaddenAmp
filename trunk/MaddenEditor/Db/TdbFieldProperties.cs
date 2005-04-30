using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Db
{
    enum TdbFieldType { tdbString = 0, tdbBinary = 1, tdbSInt = 2, tdbUInt = 3, tdbFloat = 4, tdbInt = 5 };

    struct TdbFieldProperties
    {
        public String Name;
        public int Size;
        public TdbFieldType FieldType;
    }
}
