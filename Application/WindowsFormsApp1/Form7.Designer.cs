using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    partial class Form7
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
            this.label50 = new System.Windows.Forms.Label();
            this.listBox50 = new System.Windows.Forms.ListBox();
            this.button50 = new System.Windows.Forms.Button();
            this.pictureBox50 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox50)).BeginInit();
            this.SuspendLayout();
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("굴림", 15F);
            this.label50.Location = new System.Drawing.Point(56, 46);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(96, 20);
            this.label50.TabIndex = 1;
            this.label50.Text = "게임 종료";
            // 
            // listBox50
            // 
            this.listBox50.FormattingEnabled = true;
            this.listBox50.ItemHeight = 12;
            this.listBox50.Items.AddRange(new object[] {
            " "});
            this.listBox50.Location = new System.Drawing.Point(15, 95);
            this.listBox50.Name = "listBox50";
            this.listBox50.Size = new System.Drawing.Size(175, 136);
            this.listBox50.TabIndex = 2;
            // 
            // button50
            // 
            this.button50.Location = new System.Drawing.Point(60, 240);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(75, 23);
            this.button50.TabIndex = 3;
            this.button50.Text = "돌아가기";
            this.button50.UseVisualStyleBackColor = true;
            this.button50.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox50
            // 
            this.pictureBox50.Image = global::WindowsFormsApp1.Properties.Resources.배경2;
            this.pictureBox50.Location = new System.Drawing.Point(205, -1);
            this.pictureBox50.Name = "pictureBox50";
            this.pictureBox50.Size = new System.Drawing.Size(264, 356);
            this.pictureBox50.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox50.TabIndex = 0;
            this.pictureBox50.TabStop = false;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 351);
            this.Controls.Add(this.button50);
            this.Controls.Add(this.listBox50);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.pictureBox50);
            this.Name = "Form7";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox50)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox50;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ListBox listBox50;
        private System.Windows.Forms.Button button50;
    }
}