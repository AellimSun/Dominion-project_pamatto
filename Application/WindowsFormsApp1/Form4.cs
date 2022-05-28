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

namespace WindowsFormsApp1
{
    // 마녀 카드 서서히 등장
    public partial class Form4 : Form
    {
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            FormEffect(this);
        }

        private void FormEffect(Form fm)
        {
            double[] opacity = new double[] { 0.05d, 0.1d, 0.15d, 0.2d, 0.25d, 0.3d, 0.4d, 0.5d, 0.7d, 0.8d, 0.9d, 1.0d };
            int cnt = 0;
            Timer tm = new Timer();
            {
                fm.RightToLeftLayout = false;
                fm.Opacity = 0d;
                tm.Interval = 70;
                tm.Tick += delegate (object obj, EventArgs e)
                {
                    if (cnt + 1 > opacity.Length || fm == null)
                    {
                        tm.Stop();
                        tm.Dispose();
                        tm = null;
                        return;
                    }
                    else
                        fm.Opacity = opacity[cnt++];
                };
            }
            tm.Start();
        }
  
        public Form4()
        {
            InitializeComponent();
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
        //마녀 카드 등장 이펙트 폼
        Form5 f5 = new Form5();

        private void Form4_Load(object sender, EventArgs e)
        {
            f5.Show();
            //pictureBox1.Load(Directory.GetCurrentDirectory() + "\\witch.png");
            pictureBox1.Image = new Bitmap(Directory.GetCurrentDirectory() + "\\witch.png");
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }
    }
}
