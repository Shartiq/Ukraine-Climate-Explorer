namespace Ukraine_Climate_Explorer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoadData = new Button();
            rtbResult = new RichTextBox();
            dtpStartDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            grpDataInputs = new GroupBox();
            label3 = new Label();
            cmbCity = new ComboBox();
            dtpEndDate = new DateTimePicker();
            pnlChart = new Panel();
            grpDataInputs.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadData
            // 
            btnLoadData.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnLoadData.Location = new Point(0, 139);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(644, 53);
            btnLoadData.TabIndex = 0;
            btnLoadData.Text = "Load Data";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += btnLoadData_Click;
            // 
            // rtbResult
            // 
            rtbResult.Cursor = Cursors.IBeam;
            rtbResult.Font = new Font("Times New Roman", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            rtbResult.Location = new Point(12, 12);
            rtbResult.Name = "rtbResult";
            rtbResult.ReadOnly = true;
            rtbResult.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtbResult.Size = new Size(390, 504);
            rtbResult.TabIndex = 1;
            rtbResult.Text = "";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(220, 36);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(209, 21);
            dtpStartDate.TabIndex = 2;
            dtpStartDate.Value = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(220, 17);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 4;
            label1.Text = "Start period";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(435, 17);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 5;
            label2.Text = "End Period";
            // 
            // grpDataInputs
            // 
            grpDataInputs.Controls.Add(label3);
            grpDataInputs.Controls.Add(cmbCity);
            grpDataInputs.Controls.Add(dtpEndDate);
            grpDataInputs.Controls.Add(dtpStartDate);
            grpDataInputs.Controls.Add(label2);
            grpDataInputs.Controls.Add(btnLoadData);
            grpDataInputs.Controls.Add(label1);
            grpDataInputs.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            grpDataInputs.Location = new Point(408, 12);
            grpDataInputs.Name = "grpDataInputs";
            grpDataInputs.Size = new Size(650, 198);
            grpDataInputs.TabIndex = 6;
            grpDataInputs.TabStop = false;
            grpDataInputs.Text = "Data Inputs";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label3.Location = new Point(6, 17);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 8;
            label3.Text = "City";
            // 
            // cmbCity
            // 
            cmbCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCity.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cmbCity.FormattingEnabled = true;
            cmbCity.Location = new Point(6, 35);
            cmbCity.Name = "cmbCity";
            cmbCity.Size = new Size(209, 24);
            cmbCity.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(435, 36);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(209, 21);
            dtpEndDate.TabIndex = 6;
            dtpEndDate.Value = new DateTime(2025, 1, 31, 0, 0, 0, 0);
            // 
            // pnlChart
            // 
            pnlChart.Location = new Point(408, 216);
            pnlChart.Name = "pnlChart";
            pnlChart.Size = new Size(650, 300);
            pnlChart.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 527);
            Controls.Add(pnlChart);
            Controls.Add(grpDataInputs);
            Controls.Add(rtbResult);
            Name = "Form1";
            Text = "Form1";
            grpDataInputs.ResumeLayout(false);
            grpDataInputs.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadData;
        private RichTextBox rtbResult;
        private DateTimePicker dtpStartDate;
        private Label label1;
        private Label label2;
        private GroupBox grpDataInputs;
        private DateTimePicker dtpEndDate;
        private ComboBox cmbCity;
        private Label label3;
        private Panel pnlChart;
    }
}
