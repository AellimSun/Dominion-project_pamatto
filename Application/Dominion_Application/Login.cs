using System;
using System.Windows.Forms;

namespace DominionApp
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void startBTN_Click(object sender, EventArgs e)
        {
            if(textID.Text=="" || textPW.Text == "")
            {
                MessageBox.Show("아이디나 패스워드를 입력해주세요!", "오류");
            }
            else
            {
                Global.UserID = textID.Text;
                Global.transHandler = new TransHandler("127.0.0.1", 5542, Global.UserID);
                /*엘림만 사용*/

                Queue Queue = new Queue();
                this.textID.Enabled = false;
                this.textPW.Enabled = false;
                this.startBTN.Enabled = false;

                this.Hide();
                if (Queue.ShowDialog() == DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    this.textID.Enabled = true;
                    this.textPW.Enabled = true;
                    this.startBTN.Enabled = true;
                    this.Show();
                }
                //this.textID.Text = "";
                //this.textPW.Text = "";
                //this.textID.Enabled = true;
                //this.textPW.Enabled = true;
                //this.startBTN.Enabled = true;
                //this.Show();
            }
            //this.Enabled = false;  비활성화
            //this.Visible = false;  안보이게
        }
    }
}
