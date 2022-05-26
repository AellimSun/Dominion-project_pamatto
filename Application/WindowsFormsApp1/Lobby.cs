using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LobbyForm
{
    public partial class Lobby : Form
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void btnMatching_Click(object sender, EventArgs e)
        {
            Matching m = new Matching();
            panel1.Controls.Clear();
            m.TopLevel = false;
            panel1.Controls.Add(m);
            m.Show();
            m.Dock = DockStyle.Fill;
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            Ranking r = new Ranking();
            panel1.Controls.Clear();
            r.TopLevel = false;
            panel1.Controls.Add(r);
            r.Show();
            r.Dock = DockStyle.Fill;
        }

        private void btnRuleBook_Click(object sender, EventArgs e)
        {
            RuleBook r = new RuleBook();
            panel1.Controls.Clear();
            r.TopLevel = false;
            panel1.Controls.Add(r);
            r.Show();
            r.Dock = DockStyle.Fill;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Modify m = new Modify();
            panel1.Controls.Clear();
            m.TopLevel = false;
            panel1.Controls.Add(m);
            m.Show();
            m.Dock = DockStyle.Fill;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
