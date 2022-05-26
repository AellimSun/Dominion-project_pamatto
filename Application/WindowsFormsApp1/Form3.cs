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
        public Form3()
        {
            InitializeComponent();
        }
        int Qcount = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Qcount = Global.transHandler.Start_Matching();
            if (Qcount == 1)
            {
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
                    //int cnt = 8;
                    //while(cnt > 0)
                    //{
                    //    timeLabel.Text = cnt.ToString();
                    //    await Task.Delay(1000);
                    //    cnt--;
                    //}
                    int res2 = Global.transHandler.Respond(1, Global.ID_List);
                    if (res2 == 1)
                    {
                        MessageBox.Show(Global.ID_List[0],Global.ID_List[1]);
                        MessageBox.Show("게임이 시작됩니다.");
                        this.Close();
                    }
                    else if (res2 == -1)
                    {
                        MessageBox.Show("게임이 취소되었습니다.");
                        this.Close();
                    }
                    //starttest();
                }
                else if (res == -1)
                {   //취소 버튼 클릭해서 응답받은 것
                    MessageBox.Show("wait full queue 게임이 취소되었습니다.");
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
            Game_Screen game_Screen = new Game_Screen();
            DB_ACCESS dB_ACCESS = new DB_ACCESS();
            int res = Global.transHandler.Respond(1, Global.ID_List);
            if (res == 1)
            {
                MessageBox.Show("게임이 시작됩니다.");
                dB_ACCESS.SendDBLog(Global.UserID, "Game in");          //sending game login
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
            Game_Screen game_Screen = new Game_Screen();
            int res = Global.transHandler.Respond(1, Global.ID_List);
            if (res == 1)
            {
                MessageBox.Show("게임이 시작됩니다.");
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
