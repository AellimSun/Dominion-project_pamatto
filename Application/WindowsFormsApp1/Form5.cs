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
    //마녀 카드 등장 이펙트
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Image_Class img = new Image_Class();

            pictureBox1.Image = img.Cow_gif;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
