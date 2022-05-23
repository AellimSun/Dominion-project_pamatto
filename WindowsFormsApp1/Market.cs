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
        public json cardInfo = new json();

        public Market()
        {
            //json 파일 읽어 카드정보들 저장할 변수
            JObject card = cardInfo.getJson();
            JToken jtoken = card.First;

            //foreach 조건문 안에서 KeyValuePair 사용하면 오류 없는데 이상하게 안이나 밖에서 쓰면 오류남..
            //jtoken.Next도 bool타입이 아니니 일단 이렇게 작성하는걸로...
            foreach (KeyValuePair<string, JToken> pair in card)
            {
                //key값을 얻기위해 JProperty형식으로 형변환
                JProperty jp = jtoken.ToObject<JProperty>();
                //key값 얻기(카드이름)
                string key = jp.Name;
                //액션카드객체 생성
                ActionCard actionCard = new ActionCard(key, jtoken);
                //마켓 리스트에 넣기
                MarketPile.Add(actionCard);

                //다음 카드로
                jtoken = jtoken.Next;
            }
        }

        public List<Card> getMarketList()
        {
            return MarketPile;
        }

        public void SellCard(Card card)
        {
            card.minusAmount(1);
        }

    }
}
