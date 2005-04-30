/******************************************************************************
 * Madden 2005 Editor
 * Copyright (C) 2005 Colin Goudie
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * http://gommo.homelinux.net             colin.goudie@gmail.com
 * 
 *****************************************************************************/
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

        [DllImport(TDBACCESS_DLL)]
		public static extern bool TDBFieldGetValueAsBinary(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

        [DllImport(TDBACCESS_DLL)]
		public static extern float TDBFieldGetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo);
		
        [DllImport(TDBACCESS_DLL)]
		public static extern int TDBFieldGetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBFieldGetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBFieldSetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo, float NewValue);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBFieldSetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo, int NewValue);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBFieldSetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, string NewValue);

		[DllImport(TDBACCESS_DLL)]
		public static extern int TDBQueryFindUnsignedInt(int DBIndex, string TableName, string FieldName, int Value);

		[DllImport(TDBACCESS_DLL)]
		public static extern int TDBQueryGetResult(int Index);

		[DllImport(TDBACCESS_DLL)]
		public static extern int TDBQueryGetResultSize();

		[DllImport(TDBACCESS_DLL)]
		public static extern int TDBTableRecordAdd(int DBIndex, string TableName, bool AllowExpand);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBTableRecordChangeDeleted(int DBIndex, string TableName, int RecNo, bool Deleted);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBTableRecordDeleted(int DBIndex, string TableName, int RecNo);

		[DllImport(TDBACCESS_DLL)]
		public static extern bool TDBTableRecordRemove(int DBIndex, string TableName, int RecNo);



    }
}
