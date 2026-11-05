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
            SuspendLayout();
            // 
            // btnLoadData
            // 
            btnLoadData.Location = new Point(298, 12);
            btnLoadData.Name = "btnLoadData";
            btnLoadData.Size = new Size(75, 128);
            btnLoadData.TabIndex = 0;
            btnLoadData.Text = "Load Data";
            btnLoadData.UseVisualStyleBackColor = true;
            btnLoadData.Click += btnLoadData_Click;
            // 
            // rtbResult
            // 
            rtbResult.Location = new Point(12, 12);
            rtbResult.Name = "rtbResult";
            rtbResult.Size = new Size(280, 426);
            rtbResult.TabIndex = 1;
            rtbResult.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 450);
            Controls.Add(rtbResult);
            Controls.Add(btnLoadData);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadData;
        private RichTextBox rtbResult;
    }
}
