using System;
using System.Collections.Generic;
using System.Text;

namespace MaddenEditor.Core.Record
{
	/// <summary>
	/// This class describes an object ideal to be placed in a combo box.
	/// The class has a descriptive string to display in the box as well
	/// as an ID that can be used to identify itself
	/// </summary>
	public class ComboRecord
	{
		private string name;
		private int id;

		public ComboRecord(string name, int id)
		{
			this.name = name;
			this.id = id;
		}

		public override string ToString()
		{
			return this.name;
		}

		public int Id
		{
			get
			{
				return this.id;
			}
		}
	}
}
