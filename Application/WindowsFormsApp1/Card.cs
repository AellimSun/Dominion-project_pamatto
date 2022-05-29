using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public class Card
    {
        public int price = 0;
        public int amount = 0;
        public string Name = "";
        public string kind = "";
    }

    public class ActionCard : Card
    {
        //초기값 설정 코드 추가
        public int add_Draw = 0;
        public int goto_Grave = 0;
        public int goto_Trash = 0;
        public int add_Action = 0;
        public int add_Buy = 0;
        public int add_Money = 0;
        public int gain_to_Deck = 0;
        public int gain_to_Hand = 0;
        public bool attack = false;
        public bool protect = false;

        //생성자
        public ActionCard(string key, JToken jtoken)
        {
            Card card = new Card();
            //int 타입 멤버변수 배열
            string[] ops = { "price", "amount", "add_Draw", "goto_Grave", "goto_Trash",
                        "add_Action", "add_Buy", "add_Money", "gain_to_Deck",
                        "gain_to_Hand" };
            Name = key;

            foreach(JToken j in jtoken)
            { 
                foreach(JToken j2 in j)
                {
                    //효과이름(key값) 얻기위해 JProperty로 형변환
                    JProperty jp = j2.ToObject<JProperty>();
                    //효과이름 얻기
                    string subKey = jp.Name;
                    amount = 10;
                    kind = "action";

                    //int 타입 멤버변수일 경우
                    if(Array.IndexOf(ops, subKey) > -1){
                        //int값이 들어가야하는 곳에서 n이 들어갈 때 -> 임시로 0으로 해놓기
                        if (jp.Value.ToString().Equals("n"))
                        {
                            jp.Value = 0;
                        }
                        //subKey라는 스트링 변수의 값을 변수명으로 가진 변수에 값 세팅
                        this.GetType().GetField(subKey).SetValue(this, Convert.ToInt32(jp.Value));
                    }
                    else
                    {
                        //subKey라는 스트링 변수의 값을 변수명으로 가진 변수에 값 세팅
                        this.GetType().GetField(subKey).SetValue(this, Convert.ToBoolean(jp.Value));
                    }
                }
            }
        }
    }

    public class MoneyCard : Card
    {
        public int money;

        public MoneyCard(string key, JToken jtoken)
        {
            Card card = new Card();
            Name = key;
            kind = "money";

            foreach (JToken j in jtoken)
            {
                foreach (JToken j2 in j)
                {
                    //효과이름(key값) 얻기위해 JProperty로 형변환
                    JProperty jp = j2.ToObject<JProperty>();
                    //효과이름 얻기
                    string subKey = jp.Name;

                    //subKey라는 스트링 변수의 값을 변수명으로 가진 변수에 값 세팅
                    this.GetType().GetField(subKey).SetValue(this, Convert.ToInt32(jp.Value));
                }
            }
        }
    }

    public class EstateCard : Card
    {
        public int score;

        public EstateCard(string key, JToken jtoken)
        {
            Card card = new Card();
            Name = key;
            kind = "estate";

            foreach (JToken j in jtoken)
            {
                foreach (JToken j2 in j)
                {
                    //효과이름(key값) 얻기위해 JProperty로 형변환
                    JProperty jp = j2.ToObject<JProperty>();
                    //효과이름 얻기
                    string subKey = jp.Name;

                    //subKey라는 스트링 변수의 값을 변수명으로 가진 변수에 값 세팅
                    this.GetType().GetField(subKey).SetValue(this, Convert.ToInt32(jp.Value));
                }
            }
        }
    }
}
