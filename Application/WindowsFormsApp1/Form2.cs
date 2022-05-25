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
                Global.UserID = textID.Text;
                Form3 form3 = new Form3();
                this.textID.Enabled = false;
                this.textPW.Enabled = false;
                this.startBTN.Enabled = false;
                form3.ShowDialog();
                this.Hide();
            }
            //this.Enabled = false;  비활성화
            //this.Visible = false;  안보이게
        }
    }
}
