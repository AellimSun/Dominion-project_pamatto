using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Charting;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
       // Game game;
        //DB_ACCESS dB;
       // Market market;
       // Deck deck;
        //private TransHandler transHandler;
        public Form7(int[] US)
        {
            InitializeComponent();
            listBox50.Items.Clear();
            for (int i = 0; i < 4; i++)
            {
               listBox50.Items.Add((i++).ToString() + "등  "+ Global.ID_List[i] +"  점수 :" +US[i].ToString());                
            }
        }

        

        private void Form7_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           //Form2.Show();
        }
    }
}
