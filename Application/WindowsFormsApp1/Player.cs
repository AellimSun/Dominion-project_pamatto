using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Player
    {
        //private Card card;
        private Deck deck;
        private GameTable gameTable;
        private Market market;
        string playerName { get; set; }
        public string PlayerName { get => playerName; }
        public Player(Deck deck, GameTable gameTable, Market market, string nickName)
        {
            this.deck = deck;
            this.gameTable = gameTable;
            this.market = market;
            this.playerName = nickName;
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
        public void buyCard(Card card)
        {
            deck.BuyCard(card);
            market.SellCard(card);
            //간단하게..?
        }
        //액션 등등

    }
}
