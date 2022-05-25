using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Game
    {
        int score = 0;
        Game_Screen form;
        public Market market;
        public List<Card> cardList;
        public Deck deck;
        public GameTable gameTable;
        public Trash trash;

        public Game(Game_Screen form)
        {
            //폼 멤버변수로 저장해두기
            this.form = form;

            //마켓 초기화
            market = new Market();
            cardList = market.getMarketList();
            form.marketImgInit(market.MarketPile);

            //덱 초기화
            deck = new Deck(market.estatePile, market.MoneyPile);
            gameTable = new GameTable();
            trash = new Trash();

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
                if (now.Equals("액션 종료"))
                {
                    form.printMessageBox("지금은 액션 카드만 선택할 수 있습니다. \n액션을 사용하지 않는다면 액션 종료를 눌러주세요.");
                }
                else
                {
                    bool res = form.pictureBox_SetImg(idx);

                    if (res)
                    {
                        MoneyCard moneyCard = (MoneyCard)deck.HandDeck[idx];

                        gameTable.Coin += moneyCard.money;
                        form.changeABC(gameTable);
                    }
                }
            }
            else if (deck.HandDeck[idx].kind.Equals("action"))
            {
                if (!now.Equals("액션 종료"))
                {
                    bool res = form.pictureBox_SetImg(idx);

                    if (res)
                    {
                        gameTable.ActionNumber -= 1;
                        form.changeABC(gameTable);

                        if (gameTable.ActionNumber <= 0)
                        {
                            form.turn_button1();
                        }
                    }
                }
                else
                {
                    form.printMessageBox("지금은 재화 카드만 선택할 수 있습니다. \n재화를 사용하지 않는다면 구매 종료를 눌러주세요.");
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
            if (card.goto_Grave != 0)
                deck.GoToGrave(card.goto_Grave);
            //if (card.attack == true)

            //ShowTable에 보여지는 UI관련 메소드
        }
        public Card buyCard(int i)
        {
            if (gameTable.Coin >= cardList[i].price)
            {
                gameTable.Coin -= cardList[i].price;
                //int amount = cardList[i].amount;
                market.SellCard(cardList[i]);
                deck.BuyCard(cardList[i]);
            }

            //if(gameTable.Coin >=)

            return cardList[i];
        }
        //액션 등등
    }
}
