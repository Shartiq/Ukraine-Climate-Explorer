using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ukraine_Climate_Explorer.Controls
{
    public partial class DataCoverageControl : UserControl
    {
        public DataCoverageControl()
        {
            InitializeComponent();
        }

        public void SetCoverage(string cityName, int daysWithData, int totalDays)
        {
            int daysMissing = totalDays - daysWithData;
            double coverage = 100.0 * daysWithData / totalDays;

            pbCoverage.Maximum = totalDays;
            pbCoverage.Value = daysWithData;

            lblCity.Text = $"{cityName} Coverage";
            lblDaysAmount.Text = $"{daysWithData} / {totalDays} days";
            lblDaysMissing.Text = $"{daysMissing} days missing";
            lblCoverage.Text = $"{coverage:F1}% coverage";
        }
    }
}
