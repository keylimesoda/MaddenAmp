/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 Stingray68
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
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
    static class BETDB
    {
        private const string BE_TDBACCESS_DLL = "BEtdbaccess.dll";

        #region IO
        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBClose(int DBIndex); 
        
        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBOpen(string FileName);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBOpenPS3Save(string FileName);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBOpenXBox360Save(string FileName);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBSave(int DBIndex);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBSetPFDToolPath(string FileName);
        #endregion

        #region Database
        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBDatabaseCompact(int DBIndex);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBDatabaseGetTableCount(int DBIndex);
        #endregion

        #region Tables

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBTableGetProperties(int DBIndex, int TableIndex, ref TdbTableProperties TableProperties);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBTableRecordAdd(int DBIndex, string TableName, bool AllowExpand);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBTableRecordChangeDeleted(int DBIndex, string TableName, int RecNo, bool Deleted);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBTableRecordDeleted(int DBIndex, string TableName, int RecNo);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBTableRecordRemove(int DBIndex, string TableName, int RecNo);

        #endregion

        #region Fields

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldGetProperties(int DBIndex, string TableName, int FieldIndex, ref TdbFieldProperties FieldProperties);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldGetValueAsBinary(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern float TDBFieldGetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern int TDBFieldGetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldGetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, ref string OutBuffer);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldSetValueAsFloat(int DBIndex, string TableName, string FieldName, int RecNo, float NewValue);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldSetValueAsInteger(int DBIndex, string TableName, string FieldName, int RecNo, int NewValue);

        [DllImport(BE_TDBACCESS_DLL)]
        public static extern bool TDBFieldSetValueAsString(int DBIndex, string TableName, string FieldName, int RecNo, string NewValue);

        #endregion

        #region Query

        [DllImport(BE_TDBACCESS_DLL)]
		public static extern int TDBQueryFindUnsignedInt(int DBIndex, string TableName, string FieldName, int Value);

		[DllImport(BE_TDBACCESS_DLL)]
		public static extern int TDBQueryGetResult(int Index);

		[DllImport(BE_TDBACCESS_DLL)]
		public static extern int TDBQueryGetResultSize();

        #endregion





    }
}
