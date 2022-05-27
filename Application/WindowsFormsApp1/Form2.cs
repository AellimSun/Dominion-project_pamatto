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
            else if(textID.Text == "1111")
            {
                Game_Screen game_Screen = new Game_Screen();
                this.Hide();
                game_Screen.Show();
                //this.Close();

            }
            else
            {

                Global.UserID = textID.Text;
                Global.transHandler = new TransHandler("210.119.12.76", 5542, Global.UserID);
                /*엘림만 사용*/


                Form3 form3 = new Form3();
                this.textID.Enabled = false;
                this.textPW.Enabled = false;
                this.startBTN.Enabled = false;
                
                form3.ShowDialog();                   
                this.textID.Text = "";
                this.textPW.Text = "";
                this.textID.Enabled = true;
                this.textPW.Enabled = true;
                this.startBTN.Enabled = true;
                


                //나중에 지우기
                //Game_Screen game_Screen = new Game_Screen();
                this.Hide();
                //game_Screen.ShowDialog();
                //this.Close();

            }
            //this.Enabled = false;  비활성화
            //this.Visible = false;  안보이게
        }
    }
}
