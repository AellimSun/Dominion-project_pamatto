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
        int Qcount = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Qcount = Global.transHandler.Start_Matching();
            if (Qcount == 1)
            {
                //DB에 방 이름 생성 CreateGameName();
            }
            TextNumber.Text = Qcount.ToString();
            WaitingForGameStart();
        }
        async public void WaitingForGameStart()
        {
            await Task.Run(async () =>
            {
                int res = Global.transHandler.Wait_Full_Queue(this);
                if (res == 1)
                {   //게임시작하실?
                    btnStart.Enabled = true;
                    ShowCountDown();
                }
                else if (res == -1)
                {   //큐나가기
                    this.Close();
                    return;
                }
            });
        }
        public void ADD_P()
        {
            Qcount++;
            TextNumber.Text = Qcount.ToString();
        }
        public void SUB_P()
        {
            Qcount--;
            TextNumber.Text = Qcount.ToString();
        }
        public void ShowCountDown()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(count_down);
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void count_down(object sender, EventArgs e)
        {
            if (duration == 0)
            {
                timer1.Stop();
                Global.transHandler.Respond(-1,Global.ID_List);
            }
            else if (duration > 0)
            {
                duration--;
                TimeText.Text = duration.ToString();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //TimeText.Text = DateTime.Now.ToLongTimeString();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Game_Screen game_Screen = new Game_Screen();
            DB_ACCESS dB_ACCESS = new DB_ACCESS();
            int res = Global.transHandler.Respond(1, Global.ID_List);
            if (res == 1)
            {
                dB_ACCESS.SendLog(Global.UserID, "logging in");          //sending game login
                game_Screen.Show();
                this.Close();
            }
            else if (res == -1)
            {
                MessageBox.Show("게임이 취소되었습니다.");
                this.Close();
            }
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            Global.transHandler.Cancle_Matching();
        }
        public TextBox setNumberTextBox()
        {
            return TextNumber;
        }
    }
}
