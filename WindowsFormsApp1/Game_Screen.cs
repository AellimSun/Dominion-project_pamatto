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
    public partial class Game_Screen : Form
    {
        Market market;
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
            deck = new Deck();
            game = new Game();
            gameTable = new GameTable();
            trash = new Trash();
            player = new Player(deck,gameTable,market,"pamatto");



            pictureBox1.BackColor = Color.AliceBlue;
            pictureBox2.BackColor = Color.GreenYellow;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB_ACCESS db_test = new DB_ACCESS();
            db_test.SendLog(player,"sending LogTest");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DB_ACCESS db_test = new DB_ACCESS();
            db_test.RecieveLog(listLog);
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
