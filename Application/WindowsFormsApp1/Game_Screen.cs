﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public partial class Game_Screen : Form
    {
        Game game;
        DB_ACCESS dB;
        Market market;
        Deck deck;
        PictureBox[] upper = null;
        PictureBox[] lower = null;
        PictureBox[] marketPics = null;
        PictureBox[] CSPics = null;
        Label[] marketAmt = null;
        Label[] CSAmt = null;

        public string clickMode = "market";
        List<int> selected = new List<int>();

        public Game_Screen()
        {
            InitializeComponent();
        }

        public PictureBox[] getLower() { return lower; }
        private void Form1_Load(object sender, EventArgs e)
        {
            upper = new PictureBox[] { pictureBox27, pictureBox26, pictureBox25, pictureBox30,
                pictureBox29, pictureBox28, pictureBox41, pictureBox42, pictureBox43, pictureBox44,
                pictureBox45, pictureBox46, pictureBox47, pictureBox48, pictureBox49 };

            lower = new PictureBox[] { pictureBox22, pictureBox21, pictureBox20, pictureBox19,
                pictureBox18, pictureBox31, pictureBox32, pictureBox33, pictureBox34, pictureBox35,
                pictureBox36, pictureBox47, pictureBox38, pictureBox39, pictureBox40 };

            marketAmt = new Label[] { amount1, amount2, amount3, amount4, amount5, amount6, amount7,
                amount8, amount9, amount10 };

            marketPics = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4,
                pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10 };

            CSPics = new PictureBox[] { pictureBox12, pictureBox11, pictureBox16, pictureBox14, pictureBox13, pictureBox15, pictureBox17 };

            CSAmt = new Label[] { CSamount1, CSamount2, CSamount3, CSamount4, CSamount5, CSamount6, CSamount7 };
           
            game = new Game(this);
            market = game.market;
            deck = game.deck;

            //플레이어 리스트
            listBox1.Items.Clear();
            listBox1.Items.Add("player1");
            listBox1.Items.Add("player2");
            listBox1.Items.Add("player3");
            listBox1.Items.Add("player4");

            List<Card> moneyList = market.MoneyPile;
            List<Card> estateList = market.estatePile;

            pictureBox12.Load(Directory.GetCurrentDirectory() + "\\copper.png");
            pictureBox11.Load(Directory.GetCurrentDirectory() + "\\silver.png");
            pictureBox16.Load(Directory.GetCurrentDirectory() + "\\gold.png");
            pictureBox14.Load(Directory.GetCurrentDirectory() + "\\estate.png");
            pictureBox13.Load(Directory.GetCurrentDirectory() + "\\duchy.png");
            pictureBox15.Load(Directory.GetCurrentDirectory() + "\\province.png");
            pictureBox17.Load(Directory.GetCurrentDirectory() + "\\curse.png");
            
            CSamount1.Text = moneyList[0].amount.ToString();
            CSamount2.Text = moneyList[1].amount.ToString();
            CSamount3.Text = moneyList[2].amount.ToString();

            CSamount4.Text = estateList[0].amount.ToString();
            CSamount5.Text = estateList[1].amount.ToString();
            CSamount6.Text = estateList[2].amount.ToString();
            CSamount7.Text = estateList[3].amount.ToString();
        }

        public void changeABC(GameTable gameTable)
        {
            label1.Text = "액션 : " + gameTable.ActionNumber;
            label2.Text = "바이 : " + gameTable.BuyNumber;
            label3.Text = "재물 : " + gameTable.Coin;
        }

        //핸드덱 이미지 재정렬(or 초기세팅)하는 메소드
        public void setHandDeckImg(Deck deck)
        {
            this.deck = deck;
            List<Card> handList = deck.HandDeck;

            for(int i = 0; i < handList.Count; i++)
            {
                lower[i].Load(Directory.GetCurrentDirectory() + "\\" + handList[i].Name + ".png");
                lower[i].Visible = true;
                lower[i].Enabled = true;
            }
            button1.Text = "액션 종료";
            for (int j = 0; j < deck.DrawDeck.Count; j++)
            {
                Console.WriteLine(deck.DrawDeck[j].Name);
            }
            Console.WriteLine();
        }

        public void printMessageBox(string content)
        {
            MessageBox.Show(content);
        }

        private void marketClick(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                PictureBox tmp = (PictureBox)sender;
                string name = tmp.Name;

                int i;
                for (i = 0; i < marketPics.Length; i++)
                {
                    if (name.Equals(marketPics[i].Name))
                    {
                        break;
                    }
                }

                if (clickMode.Equals("market"))
                {
                    Card res = game.buyCard(i);
                    marketAmt[i].Text = res.amount.ToString();

                    // MessageBox.Show(res.Name + " 카드 1개를 구입하여 " +
                    //    res.amount + "장 남았습니다.");
                }
                else if (clickMode.Equals("grave"))
                {
                    MessageBox.Show("핸드에서 카드를 골라 버려야 합니다.");
                    return;
                }
            }
            else if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                rightclick((PictureBox)sender);
            }
        }

        private void CSClick(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                PictureBox tmp = (PictureBox)sender;
                string name = tmp.Name;

                int i;
                for (i = 0; i < CSPics.Length; i++)
                {
                    if (name.Equals(CSPics[i].Name))
                        break;
                }

                if (clickMode.Equals("market"))
                {
                    Card res = game.buyCSCard(i);
                    CSAmt[i].Text = res.amount.ToString();
                }
                else if (clickMode.Equals("grave"))
                {
                    MessageBox.Show("핸드에서 카드를 골라 버려야 합니다.");
                    return;
                }
            }
            else if((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                rightclick((PictureBox)sender);
            }
        }

        public bool pictureBox_SetImg(int idx)
        {
            PictureBox obj = upper[idx];
            PictureBox sbj = lower[idx];

            obj.Enabled = true;
            sbj.Enabled = true;
            obj.Visible = true;
            sbj.Visible = true;

            if (sbj.Image != null)
            {
                obj.Image = sbj.Image;
                sbj.Image = null;

                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string state = button1.Text;
            if (state.Equals("액션 종료"))
            {
                button1.Text = "구매 종료";
                game.gameTable.ActionNumber = 0;
                changeABC(game.gameTable);
            }else if(state.Equals("버리기 종료"))
            {
                if(selected.Count != 0)
                {
                    for(int j = 0; j < selected.Count; j++)
                    {
                        game.deck.GoToGrave(selected[j]);
                    }
                    //핸드덱 이미지 재정렬하는 메소드
                    setHandDeckImg(game.deck);

                    game.deck.DrawToHand(selected.Count, this);
                    selected.RemoveRange(0, selected.Count);
                    clickMode = "market";
                    turn_button1("액션 종료");
                }
            }
        }

        public void turn_button1(string content)
        {
            button1.Text = content;
        }

        //private void Market_CS_RightClick(object sender, EventArgs e)
        //{
        //    PictureBox tmp = (PictureBox)sender;
        //    string name = tmp.Name;
           
        //    int i = 0;
        //    for (i = 0; i < marketPics.Length; i++)
        //    {
        //        if (name.Equals(marketPics[i].Name))
        //        {
        //            break;
        //        }
        //        else if(name.Equals(CSPics[i].Name))
        //        {
        //            break;
        //        }
        //    }

        //}

        private void handClick(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                PictureBox tmp = (PictureBox)sender;
                string name = tmp.Name;

                int i;
                for (i = 0; i < lower.Length; i++)
                {
                    if (name.Equals(lower[i].Name))
                    {
                        break;
                    }
                }

                if (clickMode.Equals("market"))
                {
                    string now = button1.Text;

                    game.clickHand(now, i);
                }
                else if (clickMode.Equals("grave"))
                {
                    selected.Add(i);
                    lower[i].Image = null;
                    lower[i].Visible = false;
                    lower[i].Enabled = false;
                }
            }
            else if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                rightclick((PictureBox) sender);
            }
        }

        public void marketImgInit(List<Card> marketlist)
        {
            for (int i = 0; i < marketlist.Count; i++)
            {
                marketPics[i].Load(Directory.GetCurrentDirectory() + "\\" + marketlist[i].Name + ".png");
            }
        }
        private void rightclick (PictureBox sender)
        {
            Form6 f6 = new Form6(sender.Image);

            f6.ShowDialog();
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
