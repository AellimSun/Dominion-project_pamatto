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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void startBTN_Click(object sender, EventArgs e)
        {
            if(textID.Text=="" || textPW.Text == "")
            {
                MessageBox.Show("아이디나 패스워드를 입력해주세요!", "오류");
            }
            else
            {
                SendServer sendServer = new SendServer();
                Game_Screen game_Screen = new Game_Screen();
                DB_ACCESS dB_ACCESS = new DB_ACCESS();
                string ID = textID.Text;
                //sendServer.sendServer_StartSignal(ID);
                dB_ACCESS.SendLog(ID, "게임접속");

                game_Screen.Show();
                this.Hide();
            }
            //this.Enabled = false;  비활성화
            //this.Visible = false;  안보이게
        }
    }
}
