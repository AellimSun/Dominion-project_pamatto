using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    public class Deck
    {
        public List<Card> HandDeck;
        public List<Card> DrawDeck;
        public List<Card> GraveDeck;
        //private List<Card> MerketDeck;
        List<string> trash = new List<string>();
        private Random random = new Random();

        public void Hand_To_Grave()
        {
            //HandDeck 리스트를 모두 탐색하면서 하나하나씩 GraveDeck 리스트에 추가
            foreach (Card item in HandDeck)
            {
                GraveDeck.Add(item);
            }

            //HanDeck 리스트를 모두 삭제
            HandDeck.Clear();
        }


        public Deck(List<Card> estatelist, List<Card> moneylist, Game_Screen g)
        {
            HandDeck = new List<Card>();
            DrawDeck = new List<Card>();
            GraveDeck = new List<Card>();

            Card copper = null;
            int i;
            for (i = 0; i < 3; i++)
            {
                if (moneylist[i].Name.Equals("copper"))
                {
                    copper = moneylist[i];
                    break;
                }
            }
            Card estate = null;
            int j;
            for (j = 0; j < 4; j++)
            {
                if (estatelist[j].Name.Equals("estate"))
                {
                    estate = estatelist[j];
                    break;
                }
            }

            for (int k = 0; k < 7; k++)
            {
                DrawDeck.Add(copper);
                moneylist[i].amount -= 1;
            }

            for (int k = 0; k < 3; k++)
            {
                DrawDeck.Add(estate);
                estatelist[j].amount -= 1;
            }

            Shuffle(DrawDeck);
            DrawToHand(5, g);
        }

        public void Shuffle(List<Card> Obj)
        {
            List<Card> NewCards = new List<Card>();
            while (Obj.Count > 0)
            {
                int CardToMove = random.Next(Obj.Count);
                NewCards.Add(Obj[CardToMove]);
                Obj.RemoveAt(CardToMove);
            }
            DrawDeck = NewCards;
        }

        public bool ShowDrawDeck()                 //검증필요. 옵저버 패턴은 도저히 모르겠음.
        {

            if (DrawDeck.Count == 0)
                return false;
            else
                return true;
        }

        public bool ShowGraveDeck()
        { 
            if (GraveDeck.Count == 0)
               return false;
            else
                return true;

        }

        public void DrawToHand(int i, Game_Screen g)
        {
            //g.MakeString(i);

            while (i > 0)
            {
                if (DrawDeck.Count == 0)
                    Shuffle(GraveDeck);
                HandDeck.Add(DrawDeck[0]);
                DrawDeck.RemoveAt(0);
                i--;
            }
            g.setHandDeckImg(this);

        }
        
        public void GoToGrave(int number, string mode, Game_Screen g)
        {
            GraveDeck.Add(HandDeck[number]);
            //HandDeck[number] = null;      //주석모임
            if (mode == "u")
            {
                if((HandDeck[number] as ActionCard) != null)
                {
                    if(((ActionCard)HandDeck[number]).attack)
                        Global.transHandler.Attack(HandDeck[number].Name);
                }
                g.MakeString(HandDeck[number].Name, mode);  //매개변수 추가
            }
            else if (mode == "a")
            {
                g.MakeString();
            }
            HandDeck.RemoveAt(number);
            
        }
        public void BuyCard(Card card)
        {
            GraveDeck.Add(card);
        }

        public void gainCardToHand(Card card)
        {
            HandDeck.Add(card);
        }
        public void gotoTrash(string card)
        {
            trash.Add(card);
        }
    }
}
