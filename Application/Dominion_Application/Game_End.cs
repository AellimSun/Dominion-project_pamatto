using System;
using System.Windows.Forms;


namespace DominionApp
{
    public partial class Game_End : Form
    {
        private int[] All_SC = new int[4];
        private int itemp;
        private string stemp;
        public Game_End(int[] US)
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                All_SC[i] = US[i];
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //PrivateFontCollection privateFonts = new PrivateFontCollection();

            //privateFonts.AddFontFile("TypographerGotischB-Bold.ttf");

            //Font font = new Font(privateFonts.Families[0], 12f);

            //label50.Font = font;
            //label50.Text = "Game Over";

            pictureBox50.Image = Properties.Resources.OverBackGround;
            listBox50.Items.Clear();

            for (int i = 3; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (All_SC[j] < All_SC[j + 1])
                    {
                        itemp = All_SC[j];
                        stemp = Global.ID_List[j];
                        All_SC[j] = All_SC[j + 1];
                        Global.ID_List[j] = Global.ID_List[j + 1];
                        All_SC[j + 1] = itemp;
                        Global.ID_List[j + 1] = stemp;
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                listBox50.Items.Add(string.Format($"{i + 1}등  {Global.ID_List[i]}"));
                listBox50.Items.Add(string.Format($"점수 : {All_SC[i]}"));
            }
            textBox1.Text = string.Format("**우  승**");
            textBox2.Text = string.Format($"{Global.ID_List[0]}");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!chart1.Visible)
            {
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                chart1.Series["Score"].LegendText = "점 수";
                chart1.Series["Score"].Points.Clear();
                chart1.Series[0].Points.AddXY(Global.ID_List[3], All_SC[3]);
                chart1.Series[0].Points.AddXY(Global.ID_List[2], All_SC[2]);
                chart1.Series[0].Points.AddXY(Global.ID_List[1], All_SC[1]);
                chart1.Series[0].Points.AddXY(Global.ID_List[0], All_SC[0]);
                chart1.Visible = true;
            }
            else
            {
                chart1.Visible = false;
            }
        }

    }
}
