using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Db;

namespace MaddenEditor.Domain
{
	enum MaddenFileType { RosterFile, FranchiseFile };

	class RosterModel
	{
		private bool dirty = false;
		private int dbIndex = -1;
		private MaddenFileType fileType = MaddenFileType.RosterFile;

		public RosterModel(string filename)
		{
			//Try and open the file
			try
			{
				dbIndex = TDB.TDBOpen(filename);
			}
			catch (DllNotFoundException e)
			{
				Console.WriteLine(e.ToString());
				throw new ApplicationException("Can't open file: " + e.ToString());
			}

			//Process the file
			ProcessFile();
		}

		public bool Dirty
		{
			get
			{
				return dirty;
			}
		}

		private void ProcessFile()
		{
			
		}
	}
}
