using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Game_Screen : Form
    {
        Game game;
        DB_ACCESS dB;
        Market market;
        Deck deck;

        PictureBox[] upper = null;
        public PictureBox[] lower = null;
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
            Listen_Method();

            PrivateFontCollection privateFonts = new PrivateFontCollection();

            privateFonts.AddFontFile("TypographerGotischB-Bold.ttf");

            Font font = new Font(privateFonts.Families[0], 12f);

            groupBox1.Font = font;
            groupBox1.Text = "Market";
            groupBox2.Font = font;
            groupBox2.Text = "Action / Buy Count";
            label1.Text = "Action : ";
            label2.Text = "Buy : ";
            label3.Text = "Treasure : ";
            label9.Font = font;
            label9.Text = "Nickname / VP";
            groupBox3.Font = font;
            groupBox3.Text = "Hand";
            groupBox5.Font = font;
            groupBox5.Text = "Chatting";
            groupBox6.Font = font;
            groupBox6.Text = "Player List";
            groupBox7.Font = font;
            groupBox7.Text = "Treasure / Estate";
            groupBox9.Font = font;
            groupBox9.Text = "Deck";
            groupBox10.Font = font;
            groupBox10.Text = "My Action / Buy";
            button1.Font = font;

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
            listBox1.Items.Add(Global.ID_List[0]);
            listBox1.Items.Add(Global.ID_List[1]);
            listBox1.Items.Add(Global.ID_List[2]);
            listBox1.Items.Add(Global.ID_List[3]);

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

            pictureBoxTF();
            button1.Text = "Action End";

            
        }
        /*public void ShowDeck(Deck deck)                 //검증필요. 옵저버 패턴은 도저히 모르겠음.
        {

            this.deck = deck;

            if (deck.DrawDeck.Count == 0)
                pictureBox123.Visible = false;
            else
                pictureBox123.Visible = true;

            if (deck.GraveDeck.Count == 0)
                pictureBox124.Visible = false;
            else
                pictureBox124.Visible = true;

        }*/



        public void pictureBoxTF()
        {
            pictureBox123.Visible = deck.ShowDrawDeck();
            pictureBox124.Visible = deck.ShowGraveDeck();
            label7.Visible = deck.ShowGraveDeck();
            label4.Text = deck.DrawDeck.Count.ToString();
            label7.Text = deck.GraveDeck.Count.ToString();
        }
        public void changeABC(GameTable gameTable)
        {
            label1.Text = "Action : " + gameTable.ActionNumber;
            label2.Text = "Buy : " + gameTable.BuyNumber;
            label3.Text = "Treasure : " + gameTable.Coin;
        }

        //핸드덱 이미지 재정렬(or 초기세팅)하는 메소드
        public void setHandDeckImg(Deck deck)
        {
            this.deck = deck;
            List<Card> handList = deck.HandDeck;

            for (int i = 0; i < lower.Length; i++)
            {
                lower[i].Image = null;
                lower[i].Visible = false;
                lower[i].Enabled = false;
            }

            for (int i = 0; i < handList.Count; i++)
            {
                lower[i].Load(Directory.GetCurrentDirectory() + "\\" + handList[i].Name + ".png");
                lower[i].Visible = true;
                lower[i].Enabled = true;
            }
            //button1.Text = "Action End";
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
                }
                else if (clickMode.Equals("grave"))
                {
                    MessageBox.Show("핸드에서 카드를 골라 버려야 합니다.\n원하지 않을 경우 Effect End 종료를 클릭해 주세요.");
                    return;
                }
                else if (clickMode.Equals("actionEffectMode"))
                {
                    Card res = game.notBuyCard(i);
                    marketAmt[i].Text = res.amount.ToString();
                }
                else if (clickMode.Equals("trash"))
                {
                    MessageBox.Show("핸드에서 카드를 폐기해야 합니다.\n원하지 않을 경우 Scrap End를 클릭해 주세요.");
                    return;
                }
                else if (clickMode.Equals("moneyTrash"))
                {
                    MessageBox.Show("핸드에서 재물 카드를 폐기해야 합니다.\n원하지 않을 경우 Scrap End를 클릭해 주세요.");
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
            else if ((e as MouseEventArgs).Button == MouseButtons.Right)
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

        private int Sum_Score(Card item, int s)
        {
            if (item.Name.Equals("duchy"))
            {
                s += 3;
            }
            else if (item.Name.Equals("estate"))
            {
                s += 1;
            }
            else if (item.Name.Equals("province"))
            {
                s += 6;
            }
            else if (item.Name.Equals("curse"))
            {
                s -= 1;
            }
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string state = button1.Text;
            if (state.Equals("Action End"))
            {
                button1.Text = "Buy End";
                game.gameTable.ActionNumber = 0;
                changeABC(game.gameTable);
                return;
            }
            else if (state.Equals("Throw End"))
            {
                if (selected.Count != 0)
                {
                    for (int j = 0; j < selected.Count; j++)
                    {
                        game.deck.GoToGrave(selected[j], "a", this);
                    }
                    //핸드덱 이미지 재정렬하는 메소드
                    setHandDeckImg(game.deck);

                    game.deck.DrawToHand(selected.Count, this);
                    selected.RemoveRange(0, selected.Count);
                    clickMode = "market";
                    turn_button1("Action End");
                }
                return;
            }
            else if (state.Equals("Effect End") || state.Equals("Scrap End"))
            {
                clickMode = "market";
                game.gameTable.Coin = 0;
                changeABC(game.gameTable);

                if (game.gameTable.ActionNumber == 0)
                {
                    turn_button1("Buy End");
                }
                else
                {
                    turn_button1("Action End");
                }
                return;
            }
            else if (state.Equals("Buy End"))
            {
                //턴 종료
                if (!market.Game_Over())
                {
                    Global.transHandler.Turn_end();
                    button1.Text = "Action End";
                    //버튼 비활성화
                    button1.Enabled = false;
                    deck.Clear();
                    deck.DrawToHand(5, this);
                    pictureBoxTF();

                    foreach (PictureBox item in upper)
                    {
                        item.Image = null;
                        item.Visible = false;
                        item.Enabled = false;
                    }

                    Listen_Method();
                    game.gameTable.initGameTable();
                    changeABC(game.gameTable);
                }
                //게임 종료
                else
                {
                    //내 점수 전달
                    Finish_Game();
                }
                return;
            }
        }
        private int My_Score()
        {
            int myScore = 0;
            //행위 덱은 클릭 즉시 무덤덱으로 보내지므로, AB영역 이미지를 NULL전환만 하면 됨
            //winform 디자인 어쩌구저쩌구 싹다 밀어버리기
            foreach (PictureBox item in lower)
            {
                item.Image = null;
            }

            //핸드 덱 -> 무덤 덱으로 보내기
            deck.Hand_To_Grave();

            //무덤덱에서 승점 구해오기
            foreach (Card item in deck.GraveDeck)
            {
                myScore += Sum_Score(item, myScore);
            }

            //드로우덱에서 승점 구해오기
            foreach (Card item in deck.DrawDeck)
            {
                myScore += Sum_Score(item, myScore);
            }
            return myScore;
        }
        private void Go_to_Main_Form()
        {

            //모든 유저 점수 집계
            int[] All_Player_Score = new int[4];
            Global.transHandler.Recv_Total_Score(All_Player_Score);

            ////모든 유저 점수 및 유저배열 Sort
            int tmp_Sc;
            string tmp_Id;
            for (int i = 3; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {

                    if (All_Player_Score[j] < All_Player_Score[j + 1])
                    {
                        tmp_Sc = All_Player_Score[j];
                        tmp_Id = Global.ID_List[j];

                        All_Player_Score[j] = All_Player_Score[j + 1];
                        Global.ID_List[j] = Global.ID_List[j + 1];

                        All_Player_Score[j + 1] = tmp_Sc;
                        Global.ID_List[j + 1] = tmp_Id;
                    }
                }
            }
            Form7 form7 = new Form7(All_Player_Score);
            form7.Show();
        }

        //내가 게임 종료 시켰음 -> (내 점수 집계) -> Game_End -> (전체 점수 집계 -> 결과 출력 -> Main Form으로 복귀)
        private void Finish_Game()
        {
            int myScore = My_Score();

            Global.transHandler.Game_End(myScore);

            Go_to_Main_Form();
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
                else if (clickMode.Equals("actionEffectMode"))
                {
                    MessageBox.Show("구매하실 카드를 클릭해 주세요.\n원하지 않을 경우 Effect End를 클릭해주세요.");
                }
                else if (clickMode.Equals("trash"))
                {
                    game.gameTable.Coin = deck.HandDeck[i].price + 2;
                    changeABC(game.gameTable);
                    game.trash.gotoTrash(deck.HandDeck[i].Name);
                    deck.HandDeck.RemoveAt(i);

                    setHandDeckImg(deck);
                    clickMode = "actionEffectMode";
                    button1.Text = "Effect End";
                }
                else if (clickMode.Equals("moneyTrash"))
                {
                    string cardName = deck.HandDeck[i].Name;
                    int idx;

                    if (cardName.Equals("copper")) idx = 1;
                    else if (cardName.Equals("silver")) idx = 2;
                    else
                    {
                        MessageBox.Show("동과 은 중에서만 선택 가능합니다.");
                        return;
                    }

                    game.trash.gotoTrash(deck.HandDeck[i].Name);
                    deck.HandDeck.RemoveAt(i);

                    Card res = game.gainCSCardToHand(idx);
                    CSAmt[idx].Text = res.amount.ToString();

                    setHandDeckImg(deck);
                    clickMode = "market";
                    if (game.gameTable.ActionNumber == 0)
                    {
                        turn_button1("Buy End");
                    }
                    else
                    {
                        turn_button1("Action End");
                    }
                }
            }
            else if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                rightclick((PictureBox)sender);
            }

        }

        public void marketImgInit(List<Card> marketlist)
        {
            for (int i = 0; i < marketlist.Count; i++)
            {
                marketPics[i].Load(Directory.GetCurrentDirectory() + "\\" + marketlist[i].Name + ".png");
            }
        }
        private void rightclick(PictureBox sender)
        {
            Form6 f6 = new Form6(sender.Image);

            f6.ShowDialog();
        }
        public void setLogBox(string message)
        {
            if(message != null)
            {
                list_log.Items.Add(message);
            }
        }

        public void Log_Handle(string make)
        {
            if (!Global.ID_List.Contains(""))
            {
                Global.transHandler.Log_Send(make);
                make = Global.transHandler.Log_Receive();
                setLogBox(make);
            }
            else
            {
                setLogBox(make);
                return;
            }
        }

        public void MakeString(string cardname, string cardaction)
        {
            //무덤
            string make = "";
            if (cardaction == "u")
            {
                make = Global.UserID + "(이)가 " + cardname + " 카드 사용.";
            }
            else if (cardaction == "m")
            {
                make = Global.UserID + "(이)가" + cardname + "카드 구입.";
            }
            else if (cardaction == "h") 
            { 
                make = Global.UserID + "(이)가" + cardname + "카드로 방어.";
            }

            Log_Handle(make);
        }

        public void MakeString(int dCount)
        {
            string make = "";
            make = Global.UserID + "(이)가 " + dCount.ToString() + "장 드로우";
            Log_Handle(make);

        }

        public void MakeString()
        {
            string make = "";
            make = Global.UserID + "(이)가" + selected.Count.ToString() + "장 버림";
            Log_Handle(make);

        }

        async private void Listen_Method()
        {
            await Task.Run(() =>
            {
                string Card_Name = null;
                string Log = null;

                while (true)
                {
                    int flag = Global.transHandler.Game_Listener(ref Card_Name, ref Log);

                    if (flag == 1)
                    {
                        button1.Enabled = true;
                        setLogBox("Your Turn!");
                        break;
                    }
                    else
                    {
                        switch (flag)
                        {

                            //상대가 공격했음
                            case 2:
                                //해자가 있냐?
                                bool check_moat = false;

                                foreach (Card item in deck.HandDeck)
                                {
                                    if (item.Name.Equals("moat"))
                                    {
                                        check_moat = true;
                                        break;
                                    }
                                }

                                if (!check_moat)
                                {
                                    //저주 먹었음을 서버에 전송
                                    MakeString("curse", "m");

                                    //무덤덱으로 저주 보내버리기
                                    Card curse = game.gainCurse();

                                    //UI수정
                                    CSAmt[6].Text = curse.amount.ToString();
                                }
                                else
                                {
                                    //해자가 있다고 로그 전달
                                    MakeString("moat", "h");
                                }
                                break;

                            //상대가 먹었음 -> 시장의 카드를 줄임
                            case 3:
                                setLogBox(Card_Name);
                                Label[] Ptmp = new Label[CSAmt.Length + marketAmt.Length];
                                Card[] Ctmp = new Card[market.MarketPile.Count + market.MoneyPile.Count + market.estatePile.Count];
                                //Label 및 Ctmp 정의
                                int Pi = 0, Ci = 0;
                                foreach (Label P in marketAmt)
                                {
                                    Ptmp[Pi++] = P;
                                }
                                foreach (Label P in CSAmt)
                                {
                                    Ptmp[Pi++] = P;
                                }
                                foreach (Card C in market.MarketPile)
                                {
                                    Ctmp[Ci++] = C;
                                }
                                foreach (Card C in market.MoneyPile)
                                {
                                    Ctmp[Ci++] = C;
                                }
                                foreach (Card C in market.estatePile)
                                {
                                    Ctmp[Ci++] = C;
                                }

                                //돌면서 찾고 인덱스 이용해서 숫자감소 및 UI변경
                                for (int i = 0; i < Ctmp.Length; i++)
                                {
                                    if (Ctmp[i].Name.Equals(Card_Name))
                                    {
                                        Ctmp[i].amount--;
                                        Ptmp[i].Text = Ctmp[i].amount.ToString();
                                        break;
                                    }
                                }
                                break;
                            //상대가 폐기했음 -> 시장의 카드를 줄임
                            case 4:
                                //받아온 Card_Name 폐기시키기
                                game.trash.gotoTrash(Card_Name);
                                break;
                            //상대방한테 로그 받음 -> textbox 로그 추가
                            case 5:
                                setLogBox(Log);
                                break;
                            //상대방이 게임 종료 시켰음 -> 내 점수 집계 -> Score_send -> (전체 점수 집계 -> 결과 출력 -> Main Form으로 복귀)
                            case 6:
                                int my_score = My_Score();

                                Global.transHandler.Score_send(my_score);

                                Go_to_Main_Form();

                                break;
                            default:
                                break;
                        }
                    }
                }

            });

            
        }

        //private void Game_Screen_Shown(object sender, EventArgs e)
        //{
        //    string Card_Name = null;
        //    string Log = null;

        //    while (true)
        //    {
        //        int flag = Global.transHandler.Game_Listener(Card_Name, Log);

        //        if (flag == 1)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            switch (flag)
        //            {
        //                //상대가 공격했음
        //                case 2:

        //                    break;
        //                //상대가 먹었음 -> 시장의 카드를 줄임
        //                case 3:
        //                    Label[] Ptmp = new Label[CSAmt.Length + marketAmt.Length];
        //                    Card[] Ctmp = new Card[market.MarketPile.Count + market.MoneyPile.Count + market.estatePile.Count];
        //                    //Label 및 Ctmp 정의
        //                    int Pi = 0, Ci = 0;
        //                    foreach (Label P in marketAmt)
        //                    {
        //                        Ptmp[Pi++] = P;
        //                    }
        //                    foreach (Label P in CSAmt)
        //                    {
        //                        Ptmp[Pi++] = P;
        //                    }
        //                    foreach (Card C in market.MarketPile)
        //                    {
        //                        Ctmp[Ci++] = C;
        //                    }
        //                    foreach (Card C in market.MoneyPile)
        //                    {
        //                        Ctmp[Ci++] = C;
        //                    }
        //                    foreach (Card C in market.estatePile)
        //                    {
        //                        Ctmp[Ci++] = C;
        //                    }

        //                    //돌면서 찾고 인덱스 이용해서 숫자감소 및 UI변경
        //                    for (int i = 0; i < Ctmp.Length; i++)
        //                    {
        //                        if (Ctmp[i].Name.Equals(Card_Name))
        //                        {
        //                            Ctmp[i].amount--;
        //                            Ptmp[i].Text = Ctmp[i].amount.ToString();
        //                            break;
        //                        }
        //                    }

        //                    break;
        //                //상대가 폐기했음 -> 시장의 카드를 줄임
        //                case 4:
        //                    //받아온 Card_Name 폐기시키기
        //                    game.trash.gotoTrash(Card_Name);
        //                    break;
        //                //상대방한테 로그 받음 -> textbox 로그 추가
        //                case 5:
        //                    Log_Handle(Log);
        //                    break;
        //                //상대방이 게임 종료 시켰음 ->
        //                case 6:

        //                    break;
        //                default:
        //                    break;
        //            }
        //        }

        //    }
        //}
    }
}
