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
        Market market;
        List<Card> cardList;
        Deck deck;
        Game game;
        GameTable gameTable;
        Player player;
        Trash trash;

        public Game_Screen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            market = new Market();
            cardList = market.getMarketList();
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

            deck = new Deck();
            game = new Game();
            gameTable = new GameTable();
            trash = new Trash();
            player = new Player(deck,gameTable,market);

            listBox1.Items.Clear();
            listBox1.Items.Add("player1");
            listBox1.Items.Add("player2");
            listBox1.Items.Add("player3");
            listBox1.Items.Add("player4");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int amount = cardList[0].amount;
            market.SellCard(cardList[0]);
            amount1.Text = cardList[0].amount.ToString();

            MessageBox.Show(cardList[0].Name + " 카드 " + amount + "개 중 1개를 구입하여 " + 
                cardList[0].amount + "장 남았습니다.");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int amount = cardList[1].amount;
            market.SellCard(cardList[1]);
            amount2.Text = cardList[1].amount.ToString();

            MessageBox.Show(cardList[1].Name + " 카드 " + amount + "개 중 1개를 구입하여 " +
                cardList[1].amount + "장 남았습니다.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cc
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void amount1_Click(object sender, EventArgs e)
        {

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
