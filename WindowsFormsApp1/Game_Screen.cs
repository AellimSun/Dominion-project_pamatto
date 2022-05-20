using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardNS;

namespace WindowsFormsApp1
{
    public partial class Game_Screen : Form
    {
        market Market;
        hand Hand;

        public Game_Screen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Market = new market();
            Hand = new hand();

            Market.setMarketCardList();
            pictureBox1.BackColor = Color.AliceBlue;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //대충 어떤카드정본지 알기
            Card card = Market.sell(0);

            //핸드안에 이 카드가 없으면
            Hand.add(card);
        }
    }
    public class market
    {
        Card[] marketCard;

        //public void add(int i)
        //{
        //    marketCard[i].setAmount(10);
        //}
        public Card sell(int i)
        {
            marketCard[i].minusAmount(1);
            return marketCard[i];
        }
        public void setMarketCardList()
        {
            marketCard = new Card[10];
            ActionCard card = new ActionCard("witch");
            marketCard[0] = card;
        }
    }
    public class hand
    {
        Card[] cardList;
        int i = 0;

        public void add(Card card)
        {
            cardList[i++] = card;
        }
    }

    //게임 초기화 -> 10장 cardlist에 추가
    //
}
