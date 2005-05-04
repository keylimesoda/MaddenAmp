using System;
using System.Collections.Generic;
using System.Text;

using MaddenEditor.Core;

namespace MaddenEditor.Forms
{
	interface IEditorForm
	{
		EditorModel Model
		{
			set;
		}

		void InitialiseUI();

		void CleanUI();
	}
}
