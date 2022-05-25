
namespace WindowsFormsApp1
{
    partial class Form3
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextNumber = new System.Windows.Forms.TextBox();
            this.WaitingLabel = new System.Windows.Forms.Label();
            this.TimeText = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TextNumber
            // 
            this.TextNumber.BackColor = System.Drawing.SystemColors.MenuBar;
            this.TextNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TextNumber.Font = new System.Drawing.Font("돋움체", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TextNumber.Location = new System.Drawing.Point(210, 37);
            this.TextNumber.Name = "TextNumber";
            this.TextNumber.ReadOnly = true;
            this.TextNumber.Size = new System.Drawing.Size(34, 22);
            this.TextNumber.TabIndex = 0;
            this.TextNumber.TabStop = false;
            //this.TextNumber.TextChanged += new System.EventHandler(this.TextNumber_TextChanged);
            // 
            // WaitingLabel
            // 
            this.WaitingLabel.AutoSize = true;
            this.WaitingLabel.Font = new System.Drawing.Font("돋움체", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.WaitingLabel.Location = new System.Drawing.Point(71, 38);
            this.WaitingLabel.Name = "WaitingLabel";
            this.WaitingLabel.Size = new System.Drawing.Size(137, 19);
            this.WaitingLabel.TabIndex = 1;
            this.WaitingLabel.Text = "대기 인원 : ";
            // 
            // TimeText
            // 
            this.TimeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeText.BackColor = System.Drawing.SystemColors.MenuBar;
            this.TimeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TimeText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TimeText.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TimeText.ForeColor = System.Drawing.SystemColors.MenuText;
            this.TimeText.Location = new System.Drawing.Point(143, 77);
            this.TimeText.Multiline = true;
            this.TimeText.Name = "TimeText";
            this.TimeText.ReadOnly = true;
            this.TimeText.Size = new System.Drawing.Size(34, 33);
            this.TimeText.TabIndex = 2;
            this.TimeText.TabStop = false;
            //this.TimeText.TextChanged += new System.EventHandler(this.TimeText_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(61, 129);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "게임 시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(190, 129);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 4;
            this.btnCancle.Text = "취소";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 177);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.TimeText);
            this.Controls.Add(this.WaitingLabel);
            this.Controls.Add(this.TextNumber);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox TextNumber;
        private System.Windows.Forms.Label WaitingLabel;
        private System.Windows.Forms.TextBox TimeText;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Timer timer1;
    }
}