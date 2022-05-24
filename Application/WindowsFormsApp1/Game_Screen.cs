using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Game_Screen : Form
    {
        Game game;
        DB_ACCESS dB;

        public Game_Screen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();

            //플레이어 리스트
            listBox1.Items.Clear();
            listBox1.Items.Add("player1");
            listBox1.Items.Add("player2");
            listBox1.Items.Add("player3");
            listBox1.Items.Add("player4");

            //마켓에 이미지추가하기(Market.cs에서 하려고 하니 이 cs의 것이라 설정이 어려움)
            pictureBox1.BackColor = Color.AliceBlue;
            pictureBox2.BackColor = Color.AliceBlue;
            pictureBox3.BackColor = Color.AliceBlue;
            pictureBox4.BackColor = Color.AliceBlue;
            pictureBox5.BackColor = Color.AliceBlue;
            pictureBox6.BackColor = Color.AliceBlue;
            pictureBox7.BackColor = Color.AliceBlue;
            pictureBox8.BackColor = Color.AliceBlue;
            pictureBox9.BackColor = Color.AliceBlue;
            pictureBox10.BackColor = Color.AliceBlue;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Card res = game.buyCard(0);
            amount1.Text = res.amount.ToString();

            MessageBox.Show(res.Name + " 카드 1개를 구입하여 " + 
                res.amount + "장 남았습니다.");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Card res = game.buyCard(1);
            amount1.Text = res.amount.ToString();

            MessageBox.Show(res.Name + " 카드 1개를 구입하여 " +
                res.amount + "장 남았습니다.");
        }


        private void buttonLog_Click(object sender, EventArgs e)
        {
            dB = new DB_ACCESS();
            dB.RecieveLog(listLog);
        }
    }
    //public class market
    //{
    //    Card[] marketCard;

    //    public void add(int i)
    //    {
    //        marketCard[i].setAmount(10);
    //    }
    //    public Card sell(int i)
    //    {
    //        marketCard[i].minusAmount(1);
    //        return marketCard[i];
    //    }
    //    public void setMarketCardList()
    //    {
    //        marketCard = new Card[10];
    //        ActionCard card = new ActionCard("witch");
    //        marketCard[0] = card;
    //    }
    //}
    //public class hand
    //{
    //    Card[] cardList;
    //    int i = 0;

    //    public void add(Card card)
    //    {
    //        cardList[i++] = card;
    //    }
    //}

    //게임 초기화 -> 10장 cardlist에 추가
    //
}
