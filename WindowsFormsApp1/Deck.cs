using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Deck : GameTable
    {
        private List<Card> HandDeck;
        private List<Card> DrawDeck;
        private List<Card> GraveDeck;
        //private List<Card> MerketDeck;

        private Random random = new Random();

        public Deck()
        {
            HandDeck = new List<Card>();
            DrawDeck = new List<Card>();
            GraveDeck = new List<Card>();
        }
        public void Shuffle()
        {
            List<Card> NewCards = new List<Card>();
            while (GraveDeck.Count > 0)
            {
                int CardToMove = random.Next(GraveDeck.Count);
                NewCards.Add(GraveDeck[CardToMove]);
                GraveDeck.RemoveAt(CardToMove);
            }
            GraveDeck = NewCards;
        }
        public void DrawToHand()
        {
            while (HandDeck.Count < 6)
            {
                HandDeck.Add(GraveDeck[0]);
                GraveDeck.RemoveAt(0); //0번이 채ㅣ워지나?
                if (DrawDeck.Count == 0)
                    Shuffle();
            }
        }
        public void DrawToHand(int i)
        {
            while (0 < i)
            {
                HandDeck.Add(GraveDeck[0]);
                GraveDeck.RemoveAt(0);
                if (DrawDeck.Count == 0)
                    Shuffle();
                i--;
            }
        }
        public void Clear()
        {
            //HandToGrave
            for (int i = 0; i < HandDeck.Count; i++)
            {
                GraveDeck.Add(HandDeck[i]);
                HandDeck.RemoveAt(i);
            }
        }
        public void GoToGrave(int number)
        {
            for (int i = 0; i < number; i++)
            {
                GraveDeck.Add(HandDeck[i]);
                HandDeck.RemoveAt(i);
            }
        }
        public void BuyCard(Card card)
        {
            GraveDeck.Add(card);
        }
    }
}
