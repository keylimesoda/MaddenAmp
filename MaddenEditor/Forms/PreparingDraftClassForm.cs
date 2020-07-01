using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
    public partial class PreparingDraftClassForm : Form
    {
        BackgroundWorker excelCalculate_oWorker;
        BackgroundWorker idleStatusUpdate_oWorker;

        Microsoft.Office.Interop.Excel.Application _excelApplication;

        private static List<string> STATUS_UPDATES = new List<string>()
        {
            "Measuring hand length...",
            "Finding personal tragedies to exploit on Draft Day...",
            "Checking for legal troubles...",
            "Looking at their 40 times...",
            "Comparing QB childhoods to Tom Brady's...",
            "Preparing Roger Goodell's M&&M's...",
            "Measuring prospects without letting them stand on their toes...",
            "Preparing cattle scale for the Hog Mollies...",
            "Comparing ability to scramble to Lamar Jackson...",
            "Giving Patriots a bunch of compensatory picks...",
            "Removing Patriots picks for scandals...",
            "Reviewing Pro Day...",
            "Looking at lots and lots of game tape...",
            "Overhyping anybody related to a former star...",
            "Comparing anybody with 95+ speed to Tyreek Hill...",
            "Inserting QB busts into first round..."
        };

        public PreparingDraftClassForm(Microsoft.Office.Interop.Excel.Application excelApplication)
        {
            _excelApplication = excelApplication;
            InitializeComponent();
            InitializeWorkers();
        }

        void InitializeWorkers()
        {
            excelCalculate_oWorker = new BackgroundWorker();
            excelCalculate_oWorker.DoWork += new DoWorkEventHandler(excelCalculate_DoWork);
            excelCalculate_oWorker.WorkerReportsProgress = false;
            excelCalculate_oWorker.WorkerSupportsCancellation = true;
            excelCalculate_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(excelCalculate_RunWorkerCompleted);
            excelCalculate_oWorker.RunWorkerAsync();

            idleStatusUpdate_oWorker = new BackgroundWorker();
            idleStatusUpdate_oWorker.DoWork += new DoWorkEventHandler(idleStatusUpdate_DoWork);
            idleStatusUpdate_oWorker.WorkerReportsProgress = false;
            idleStatusUpdate_oWorker.WorkerSupportsCancellation = true;
            idleStatusUpdate_oWorker.RunWorkerAsync();            
        }

        void excelCalculate_DoWork(object sender, DoWorkEventArgs e)
        {
            _excelApplication.Calculate();
            idleStatusUpdate_oWorker.CancelAsync();
        }

        private void excelCalculate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        void idleStatusUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            var random = new Random();
            while (true)
            {
                if (!idleStatusUpdate_oWorker.CancellationPending)
                {
                    idleTextStatus.Invoke((MethodInvoker)delegate
                    {
                        idleTextStatus.Text = STATUS_UPDATES[random.Next(STATUS_UPDATES.Count)];
                    });

                    Thread.Sleep(5000);
                } else
                {
                    return;
                }
            }
        }
    }
}
