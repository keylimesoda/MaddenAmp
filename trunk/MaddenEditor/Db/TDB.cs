using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MaddenEditor.Db
{
    static class TDB
    {
        private const string TDBACCESS_DLL = "tdbaccess.dll";

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBOpen(string FileName);

        [DllImport(TDBACCESS_DLL)]
        public static extern bool TDBClose(int DBIndex);

        [DllImport(TDBACCESS_DLL)]
        public static extern int TDBDatabaseGetTableCount(int DBIndex);
    }
}
