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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label50 = new System.Windows.Forms.Label();
            this.listBox50 = new System.Windows.Forms.ListBox();
            this.button50 = new System.Windows.Forms.Button();
            this.pictureBox50 = new System.Windows.Forms.PictureBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("굴림", 20F);
            this.label50.Location = new System.Drawing.Point(36, 45);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(129, 27);
            this.label50.TabIndex = 1;
            this.label50.Text = "게임 종료";
            // 
            // listBox50
            // 
            this.listBox50.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox50.FormattingEnabled = true;
            this.listBox50.ItemHeight = 12;
            this.listBox50.Items.AddRange(new object[] {
            " "});
            this.listBox50.Location = new System.Drawing.Point(15, 95);
            this.listBox50.Name = "listBox50";
            this.listBox50.Size = new System.Drawing.Size(175, 192);
            this.listBox50.TabIndex = 2;
            // 
            // button50
            // 
            this.button50.Location = new System.Drawing.Point(104, 304);
            this.button50.Name = "button50";
            this.button50.Size = new System.Drawing.Size(75, 23);
            this.button50.TabIndex = 3;
            this.button50.Text = "돌아가기";
            this.button50.UseVisualStyleBackColor = true;
            this.button50.Click += new System.EventHandler(this.button50_Click);
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
            // chart1
            // 
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(15, 177);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series5.Font = new System.Drawing.Font("Bodoni MT Poster Compressed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series5.Legend = "Legend1";
            series5.Name = "Score";
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(175, 114);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            this.chart1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "그래프";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(15, 131);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(175, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(15, 150);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(175, 14);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 351);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button50);
            this.Controls.Add(this.listBox50);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.pictureBox50);
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "Form7";
            this.Text = "Form7";
            this.Load += new System.EventHandler(this.Form7_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox50;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ListBox listBox50;
        private System.Windows.Forms.Button button50;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}