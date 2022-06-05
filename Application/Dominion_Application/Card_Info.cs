using System;
using System.Drawing;
using System.Windows.Forms;

namespace DominionApp
{
    public partial class Card_Info : Form
    {
        
        public Card_Info(Image img)
        {
            
            InitializeComponent();
            pictureBox1.Image = img;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
