using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaddenEditor.Core;
using MaddenEditor.Forms;

namespace MaddenEditor.Forms
{
    public partial class FAQ : UserControl, IEditorForm
    {
        public EditorModel model = null;
        public bool isInitializing = false;
        
        #region IEditorForm Members

        public EditorModel Model
        {
            set { model = value; }
        }

        public void InitialiseUI()
        {
            isInitializing = true;

            

            isInitializing = false;
        }

        public void CleanUI()
        {

        }

        #endregion
        
        public FAQ()
        {
            InitializeComponent();            
        }

        
    }
}
