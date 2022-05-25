using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private int duration = 16;
        private void Form3_Load(object sender, EventArgs e)
        {
            //int count = StartMatching("",UserID);
            //if(count==1) CreateGameName();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(count_down);
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void TimeText_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //TimeText.Text = DateTime.Now.ToLongTimeString();
        }

        private void count_down(object sender, EventArgs e)
        {

            if (duration == 0)
            {
                timer1.Stop();
                //Respon(-1,UserID);
            }
            else if (duration > 0)
            {
                duration--;
                TimeText.Text = duration.ToString();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //SendServer sendServer = new SendServer();
            Game_Screen game_Screen = new Game_Screen();
            DB_ACCESS dB_ACCESS = new DB_ACCESS();

            //Respond(1,UserID);
            dB_ACCESS.SendLog(Global.UserID, "logging in");          //sending game login
            game_Screen.Show();
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //Respond(-1,UserID);
            Close();
        }
    }
}
