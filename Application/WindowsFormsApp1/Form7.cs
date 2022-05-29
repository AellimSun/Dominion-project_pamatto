using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        private int[] All_SC = new int[4];
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
                All_SC[i] = US[i];
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();

            privateFonts.AddFontFile("TypographerGotischB-Bold.ttf");

            Font font = new Font(privateFonts.Families[0], 12f);

            label50.Font = font;
            label50.Text = "Game Over";

            textBox1.Text = Global.ID_List[0] + "   **우  승**   ";
            textBox2.Text = "   축하합니다!  ";
        }

        private void button50_Click(object sender, EventArgs e)
        {
            chart1.Visible = false;
            //Application.Run(new Form2());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;

            chart1.Series["Score"].LegendText = "점 수";
            chart1.Series["Score"].Points.Clear();
            chart1.Series[0].Points.AddXY(Global.ID_List[3], All_SC[3]);
            chart1.Series[0].Points.AddXY(Global.ID_List[2], All_SC[2]);
            chart1.Series[0].Points.AddXY(Global.ID_List[1], All_SC[1]);
            chart1.Series[0].Points.AddXY(Global.ID_List[0], All_SC[0]);
        }
    }
}
