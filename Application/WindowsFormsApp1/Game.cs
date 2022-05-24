using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Game
    {
        Market market;
        List<Card> cardList;
        Deck deck;
        GameTable gameTable;
        Trash trash;

        public Game()
        {
            //마켓 초기화
            market = new Market();
            cardList = market.getMarketList();

            //덱 초기화
            //deck = new Deck(market.estatePile, market.MoneyPile);
            gameTable = new GameTable();
            trash = new Trash();
        }
        //게임
        //market deck 생성(10장 할당-card.getAmount(10); 예외:저주카드(2인 10,3인 20, 4인 30) - 소라

        //재화, 부동산 카드 리스트 만들기
        //player에 기본패 나누기

        //deck에서 셔플 후 드로우하기 - 덱 초기화
        //**시작 세팅-- 진우

        //**내 턴 - 경길
        //ABC 초기화 -   1 1 0
        //Action카드사용 - 횟수
        //Buy 사용 - Coin, 횟수
        //덱 초기화 - clear,shuffle, draw


        //플레이어 본인차례 false 턴 종료

        //
        //
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
            int amount = cardList[i].amount;
            market.SellCard(cardList[0]);
            //deck.BuyCard(card);
            //간단하게..?

            return cardList[i];
        }
        //액션 등등
    }
}
