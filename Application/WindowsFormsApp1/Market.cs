using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    class Market : GameTable
    {
        //마켓에 있는 카드 객체들을 저장할 리스트
        public List<Card> MarketPile = new List<Card>();
        public List<Card> MoneyPile = new List<Card>();
        public List<Card> estatePile = new List<Card>();
        public json cardInfo = new json();

        public Market()
        {
            //json 파일 읽어 카드정보들 저장할 변수
            JObject card = cardInfo.getJson();
            JToken card2 = card.First;
            JToken action = card2.First.First;

            //foreach 조건문 안에서 KeyValuePair 사용하면 오류 없는데 이상하게 안이나 밖에서 쓰면 오류남..
            //jtoken.Next도 bool타입이 아니니 일단 이렇게 작성하는걸로...
            for (int i = 0; i < 10; i++)
            {
                //key값을 얻기위해 JProperty형식으로 형변환
                JProperty jp = action.ToObject<JProperty>();
                //key값 얻기(카드이름)
                string key = jp.Name;
                //액션카드객체 생성
                ActionCard actionCard = new ActionCard(key, action);
                //마켓 리스트에 넣기
                MarketPile.Add(actionCard);

                //다음 카드로
                action = action.Next;
            }

            card2 = card2.Next;
            JToken money = card2.First.First;

            for (int i = 0; i < 3; i++)
            {
                {
                    //key값을 얻기위해 JProperty형식으로 형변환
                    JProperty jp = money.ToObject<JProperty>();
                    //key값 얻기(카드이름)
                    string key = jp.Name;
                    //액션카드객체 생성
                    MoneyCard moneyCard = new MoneyCard(key, money);
                    //마켓 리스트에 넣기
                    MoneyPile.Add(moneyCard);

                    //다음 카드로
                    money = money.Next;
                }
            }
            card2 = card2.Next;
            JToken estate = card2.First.First;

            for (int i = 0; i < 4; i++)
            {
                //key값을 얻기위해 JProperty형식으로 형변환
                JProperty jp = estate.ToObject<JProperty>();
                //key값 얻기(카드이름)
                string key = jp.Name;
                //액션카드객체 생성
                EstateCard estateCard = new EstateCard(key, estate);
                //마켓 리스트에 넣기
                estatePile.Add(estateCard);

                //다음 카드로
                estate = estate.Next;
            }

            ShuffleDraw();
        }

        public List<Card> getMarketList()
        {
            return MarketPile;
        }

        public void SellCard(Card card)
        {
            string name = card.Name;
            int amount = card.amount;
            card.amount -= 1;
        }
        public void ShuffleDraw()
        {
            List<Card> NewCards = new List<Card>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int CardToMove = random.Next(MarketPile.Count);
                NewCards.Add(MarketPile[CardToMove]);
                MarketPile.RemoveAt(CardToMove);
            }

            NewCards.Sort((a, b) => (a.price < b.price) ? -1 : 1);
            MarketPile = NewCards;
        }

        public bool Game_Over()
        {
            int Sold_Out_Card = 0;

            //현재 시장의 카드 덱에서 속주가 다 빠져나갔는지 판단.
            foreach (Card item in estatePile)
            {
                if(item.Name.Equals("province"))
                {
                    if (item.amount == 0)
                    {
                        return true;
                    }
                }
            }
            /*
             * MarketPile
             * MoneyPile
             * estatePile
             */
            //현재 시장에서 3개의 덱이 다 빠져나갔는지 판단.
            foreach (Card item in MarketPile)
            {
                if(item.amount == 0)
                {
                    Sold_Out_Card++;
                }
            }
            foreach (Card item in MoneyPile)
            {
                if (item.amount == 0)
                {
                    Sold_Out_Card++;
                }
            }
            foreach (Card item in estatePile)
            {
                if (item.amount == 0)
                {
                    Sold_Out_Card++;
                }
            }
            if(Sold_Out_Card >= 3)
            {
                return true;
            }

            return false;
        }
    }
}
