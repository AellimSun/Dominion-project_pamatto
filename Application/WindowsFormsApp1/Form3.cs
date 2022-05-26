using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private Game_Screen game_Screen;
        public Form3()
        {
            InitializeComponent();
        }
        int Qcount = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Global.ID_List = new string[4];

            Qcount = Global.transHandler.Start_Matching();
            if (Qcount == 1)
            {
                DateTime now = DateTime.Now;

                //DB_ACCESS dB_ACCESS = new DB_ACCESS();
                //dB_ACCESS.SendDBLog();
                //DB에 방 이름 생성 CreateGameName();
            }
            timeLabel.Text = "0";
            numLabel.Text = Qcount.ToString();
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
                    int cnt = 15;
                    while(cnt > 0)
                    {
                        timeLabel.Text = cnt.ToString();
                        await Task.Delay(1000);
                        cnt--;
                    }
                }
                else if (res == -1)
                {   //취소 버튼 클릭해서 응답받은 것
                    this.Close();
                    return;
                }
            });
        }
        public void ADD_P()
        {
            Qcount++;
            numLabel.Text = Qcount.ToString();
        }
        public void SUB_P()
        {
            Qcount--;
            numLabel.Text = Qcount.ToString();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Game_Screen game_Screen = new Game_Screen();
            DB_ACCESS dB_ACCESS = new DB_ACCESS();
            int res = Global.transHandler.Respond(1, Global.ID_List);
            if (res == 1)
            {
                btnCancle.Enabled = false;
                MessageBox.Show("게임이 시작됩니다.");
                //dB_ACCESS.SendDBLog("Game in");          //sending game login
                game_Screen.Show();
                this.Close();
            }
            else if (res == -1)
            {
                MessageBox.Show("게임이 취소되었습니다.");
                this.Close();
            }
        }
        public void starttest()
        {
            game_Screen = new Game_Screen();
            int res = Global.transHandler.Respond(1, Global.ID_List);
            if (res == 1)
            {
                //MessageBox.Show("게임이 시작됩니다.");
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
            if (btnStart.Enabled == true)
                Global.transHandler.Respond(-1, Global.ID_List);
            else
                Global.transHandler.Cancle_Matching();
        }
    }
}
