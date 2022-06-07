
namespace DominionApp
{
    partial class Attack_Witch
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
            this.pbCrow = new System.Windows.Forms.PictureBox();
            this.pbWitch = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWitch)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCrow
            // 
            this.pbCrow.Location = new System.Drawing.Point(0, 0);
            this.pbCrow.Name = "pbCrow";
            this.pbCrow.Size = new System.Drawing.Size(700, 344);
            this.pbCrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCrow.TabIndex = 0;
            this.pbCrow.TabStop = false;
            this.pbCrow.Click += new System.EventHandler(this.pbCrow_Click);
            // 
            // pbWitch
            // 
            this.pbWitch.Location = new System.Drawing.Point(256, 59);
            this.pbWitch.Name = "pbWitch";
            this.pbWitch.Size = new System.Drawing.Size(173, 212);
            this.pbWitch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWitch.TabIndex = 1;
            this.pbWitch.TabStop = false;
            // 
            // Attack_Witch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 344);
            this.Controls.Add(this.pbWitch);
            this.Controls.Add(this.pbCrow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Attack_Witch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWitch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCrow;
        private System.Windows.Forms.PictureBox pbWitch;
    }
}