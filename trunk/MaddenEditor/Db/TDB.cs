using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MaddenEditor.Db
{
    /// <summary>
    /// This class contains the static methods to use the tdbaccess.dll. The instructions
    /// on how to use these function are contained in the help file that comes with the
    /// dll.
    /// </summary>
    static class TDB
    {
        private const string TDBACCESS_DLL = "tdbaccess.dll";

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBOpen(string FileName);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBClose(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBSave(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBDatabaseCompact(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBDatabaseGetTableCount(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBFieldGetProperties(int DBIndex, string TableName, int FieldIndex, ref TdbFieldProperties FieldProperties);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBTableGetProperties(int DBIndex, int TableIndex, ref TdbTableProperties TableProperties);

        //[DllImport(TDBACCESS_DLL)]

        //[DllImport(TDBACCESS_DLL)]

        //[DllImport(TDBACCESS_DLL)]


    }
}
