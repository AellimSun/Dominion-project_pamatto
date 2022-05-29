using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Game
    {
        //int score = 0;
        int silverUsed = 0;             //상인카드를 위해 은 카드를 사용한적 있는지 체크하는 변수
        public bool merchantUsed = false;      //이번턴에 상인카드를 사용했었는지 체크하는 변수 !!!!!!!!!!턴 끝날때 꼭 false로 다시 바꿔줄것!!!!!!!!!!
        Game_Screen form;
        public Market market;
        public List<Card> cardList;
        public Deck deck;
        public GameTable gameTable;

        public Game(Game_Screen form)
        {
            //폼 멤버변수로 저장해두기
            this.form = form;

            //마켓 초기화
            market = new Market();
            cardList = market.getMarketList();
            form.marketImgInit(market.MarketPile);

            //덱 초기화
            deck = new Deck(market.estatePile, market.MoneyPile, form);
            //deck = new Deck(market.estatePile, market.MoneyPile, market.MarketPile);   // 지워야됨
            gameTable = new GameTable();


            //핸드에 들어온 덱 이미지 바꾸기
            form.setHandDeckImg(deck);

            //ABC 초기화
            form.changeABC(gameTable);
        }
        //게임
        //market deck 생성(10장 할당-card.getAmount(10); 예외:저주카드(2인 10,3인 20, 4인 30) - 소라

        //재화, 부동산 카드 리스트 만들기
        //player에 기본패 나누기
        //deck에서 셔플 후 드로우하기 - 덱 초기화
        //**시작 세팅-- 진우

        //**내 턴 - 경길
        //ABC 초기화 -   1 1 0
        //Action카드사용 - 횟수 - 초기덱에 액션카드가 없어서 일단 보류
        //Buy 사용 - Coin, 횟수
        //덱 초기화 - clear,shuffle, draw


        //플레이어 본인차례 false 턴 종료

        //
        //

        public void clickHand(string now, int idx)
        {
            if (deck.HandDeck[idx].kind.Equals("estate"))
            {
                form.printMessageBox("점수 카드는 선택할 수 없습니다.");
                return;
            }
            else if (deck.HandDeck[idx].kind.Equals("money"))
            {
                //상인카드를 위해 은 카드를 사용한적 있는지 체크
                if (silverUsed == 0 && deck.HandDeck[idx].Name.Equals("silver") && merchantUsed)
                {
                    silverUsed++;
                    gameTable.Coin++;
                }

                if (now.Equals("Action End"))
                {
                    form.printMessageBox("지금은 액션 카드만 선택할 수 있습니다. \n액션을 사용하지 않는다면 Action End를 눌러주세요.");
                }
                else
                {
                    bool res = form.pictureBox_SetImg(idx);

                    if (res)
                    {
                        MoneyCard moneyCard = (MoneyCard)deck.HandDeck[idx];

                        gameTable.Coin += moneyCard.money;
                        deck.GoToGrave(idx, "u", form);
                        form.changeABC(gameTable);
                        form.pictureBoxTF();
                        form.setHandDeckImg(deck);
                    }
                }
            }
            else if (deck.HandDeck[idx].kind.Equals("action"))
            {
                if (now.Equals("Action End"))
                {
                    bool res = form.pictureBox_SetImg(idx);

                    if (res)
                    {
                        string cardName = deck.HandDeck[idx].Name;
                        gameTable.ActionNumber -= 1;
                        ActionCard actionCard = (ActionCard)deck.HandDeck[idx];
                        deck.GoToGrave(idx, "u", form);
                        useCard(actionCard);
                        form.changeABC(gameTable);
                        form.pictureBoxTF();
                        form.setHandDeckImg(deck);

                        if (gameTable.ActionNumber <= 0 && !cardName.Equals("workshop") 
                            && !cardName.Equals("remodel") && !cardName.Equals("mine"))
                        {
                            form.turn_button1("Action End");
                        }
                    }
                }
                else
                {
                    form.printMessageBox("지금은 액션 카드만 선택할 수 있습니다. \n액션을 사용하지 않는다면 Action End를 눌러주세요.");
                }
            }
        }
        public void useCard(ActionCard card)
        {
            if (card.add_Action != 0)
                gameTable.ActionNumber += card.add_Action;
            if (card.add_Buy != 0)
                gameTable.BuyNumber += card.add_Buy;
            if (card.add_Money != 0)
                gameTable.Coin += card.add_Money;
            if (card.add_Draw != 0)
                deck.DrawToHand(card.add_Draw, form);
            if (card.attack)
            {
                //어택내용을 서버로 넘기나?
            }

            //예외사항이 있는 카드들

            //상인
            if (card.Name.Equals("merchant"))
            {
                merchantUsed = true;
            }
            //작업
            else if (card.Name.Equals("cellar"))
            {
                form.clickMode = "grave";
                form.turn_button1("Throw End");
            }
            else if (card.Name.Equals("workshop"))
            {
                form.clickMode = "actionEffectMode";
                form.turn_button1("Effect End");
                gameTable.Coin = 4;
            }
            else if (card.Name.Equals("remodel"))
            {
                form.clickMode = "trash";
                form.turn_button1("Scrap End");
            }
            else if (card.Name.Equals("mine"))
            {
                form.clickMode = "moneyTrash";
                form.turn_button1("Scrap End");
            }

            //ShowTable에 보여지는 UI관련 메소드
            form.changeABC(gameTable);
        }
        public Card buyCard(int i)
        {
            if (gameTable.Coin >= cardList[i].price && gameTable.BuyNumber >= 1)
            {
                gameTable.Coin -= cardList[i].price;
                gameTable.BuyNumber -= 1;
                //int amount = cardList[i].amount;
                market.SellCard(cardList[i]);
                deck.BuyCard(cardList[i]);
                form.pictureBoxTF();

                form.changeABC(gameTable);
                form.MakeString(market.MarketPile[i].Name, "m");
                Global.transHandler.Get_Card(market.MarketPile[i].Name);
            }

            return cardList[i];
        }

        public Card notBuyCard(int i)
        {
            if (gameTable.Coin >= cardList[i].price)
            {
                gameTable.Coin = 0;
                market.SellCard(cardList[i]);
                deck.BuyCard(cardList[i]);
                form.pictureBoxTF();
                form.changeABC(gameTable);

                form.clickMode = "market";
                if (gameTable.ActionNumber == 0)
                {
                    form.turn_button1("Action End");
                }
                else
                {
                    form.turn_button1("Action End");
                }
            }

            return cardList[i];
        }

        public Card buyCSCard(int i)
        {
            List<Card> list = null;
            if (i < 3)
            {
                list = market.MoneyPile;
            }
            else
            {
                list = market.estatePile;
                i = i - 3;
            }
            if (gameTable.Coin >= list[i].price && gameTable.BuyNumber >= 1)
            {
               
                gameTable.Coin -= list[i].price;
                gameTable.BuyNumber -= 1;
                //int amount = cardList[i].amount;
                market.SellCard(list[i]);
                deck.BuyCard(list[i]);
                form.pictureBoxTF();
                form.changeABC(gameTable);

                //카드 먹음 alert_msg 전송
                Global.transHandler.Get_Card(list[i].Name);
                //카드 먹음 log 전송
                form.MakeString(list[i].Name, "m");
            }

            //if(gameTable.Coin >=)

            return list[i];
        }
        public Card notBuyCSCSCard(int i)
        {
            List<Card> list = null;
            if (i < 3)
            {
                list = market.MoneyPile;
            }
            else
            {
                list = market.estatePile;
                i = i - 3;
            }
            if (gameTable.Coin >= list[i].price)
            {
                gameTable.Coin = 0;
                market.SellCard(cardList[i]);
                deck.BuyCard(cardList[i]);
                form.pictureBoxTF();
                form.changeABC(gameTable);

                form.clickMode = "market";
                if (gameTable.ActionNumber == 0)
                {
                    form.turn_button1("Action End");
                }
                else
                {
                    form.turn_button1("Action End");
                }
            }

            return list[i];
        }

        public Card gainCurse()
        {
            gameTable.Coin = 0;
            market.SellCard(market.estatePile[3]);
            deck.BuyCard(market.estatePile[3]);
            form.pictureBoxTF();
            form.changeABC(gameTable);
            return market.estatePile[3];
        }

        public Card gainCSCardToHand(int i)
        {
            List<Card> list = null;
            if (i < 3)
            {
                list = market.MoneyPile;
            }
            else
            {
                list = market.estatePile;
                i = i - 3;
            }

            market.SellCard(list[i]);
            deck.gainCardToHand(list[i]);

            return list[i];
        }
        //액션 등등
    }
}
