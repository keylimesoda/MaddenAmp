using MaddenEditor.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaddenEditor.Forms
{
    public partial class DraftReportForm : Form
    {
        private DraftReport _draftReport;
        public DraftReportForm(DraftReport draftReport)
        {
            _draftReport = draftReport;
            InitializeComponent();
        }

        private void buttonDraftReport_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add("OVR: Highest Rated", _draftReport.HighestRated);
            this.dataGridView1.Rows.Add("OVR: Lowest Rated", _draftReport.LowestRated);
            this.dataGridView1.Rows.Add("OVR: 80+", _draftReport.Ovr80plus);
            this.dataGridView1.Rows.Add("OVR: 70-79", _draftReport.Ovr70to79);
            this.dataGridView1.Rows.Add("OVR: 60-69", _draftReport.Ovr60to69);
            this.dataGridView1.Rows.Add("OVR: 50-59", _draftReport.Ovr50to59);
            this.dataGridView1.Rows.Add("OVR: 40-49", _draftReport.Ovr40to49);
            this.dataGridView1.Rows.Add("OVR: 1-39", _draftReport.OvrSub40);
            this.dataGridView1.Rows.Add("DEV: X-Factors", _draftReport.XFactors);
            this.dataGridView1.Rows.Add("DEV: Superstars", _draftReport.Superstars);
            this.dataGridView1.Rows.Add("DEV: Stars", _draftReport.Stars);
            this.dataGridView1.Rows.Add("DEV: Normals", _draftReport.Normals);
            this.dataGridView1.Rows.Add("Total Draft Class Size", _draftReport.DraftSize);

            buttonDraftReport.Enabled = false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
