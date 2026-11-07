namespace Ukraine_Climate_Explorer.Controls
{
    partial class DataCoverageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pbCoverage = new ProgressBar();
            lblCity = new Label();
            lblDaysAmount = new Label();
            lblDaysMissing = new Label();
            lblCoverage = new Label();
            SuspendLayout();
            // 
            // pbCoverage
            // 
            pbCoverage.ForeColor = Color.LightGreen;
            pbCoverage.Location = new Point(0, 46);
            pbCoverage.Name = "pbCoverage";
            pbCoverage.Size = new Size(390, 30);
            pbCoverage.Style = ProgressBarStyle.Continuous;
            pbCoverage.TabIndex = 0;
            // 
            // lblCity
            // 
            lblCity.Anchor = AnchorStyles.Left;
            lblCity.AutoEllipsis = true;
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblCity.Location = new Point(3, 14);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(119, 16);
            lblCity.TabIndex = 1;
            lblCity.Text = "##### Coverage";
            // 
            // lblDaysAmount
            // 
            lblDaysAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDaysAmount.AutoEllipsis = true;
            lblDaysAmount.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblDaysAmount.Location = new Point(204, 14);
            lblDaysAmount.Name = "lblDaysAmount";
            lblDaysAmount.Size = new Size(183, 16);
            lblDaysAmount.TabIndex = 2;
            lblDaysAmount.Text = "## / ## Days";
            lblDaysAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDaysMissing
            // 
            lblDaysMissing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDaysMissing.AutoEllipsis = true;
            lblDaysMissing.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblDaysMissing.Location = new Point(204, 89);
            lblDaysMissing.Name = "lblDaysMissing";
            lblDaysMissing.Size = new Size(183, 15);
            lblDaysMissing.TabIndex = 3;
            lblDaysMissing.Text = "## Days Missing";
            lblDaysMissing.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCoverage
            // 
            lblCoverage.Anchor = AnchorStyles.Left;
            lblCoverage.AutoEllipsis = true;
            lblCoverage.AutoSize = true;
            lblCoverage.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblCoverage.Location = new Point(3, 89);
            lblCoverage.Name = "lblCoverage";
            lblCoverage.Size = new Size(97, 15);
            lblCoverage.TabIndex = 3;
            lblCoverage.Text = "##.#% Coverage";
            // 
            // DataCoverageControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblCoverage);
            Controls.Add(lblDaysMissing);
            Controls.Add(lblDaysAmount);
            Controls.Add(lblCity);
            Controls.Add(pbCoverage);
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "DataCoverageControl";
            Size = new Size(390, 124);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar pbCoverage;
        private Label lblCity;
        private Label lblDaysAmount;
        private Label lblDaysMissing;
        private Label lblCoverage;
    }
}
