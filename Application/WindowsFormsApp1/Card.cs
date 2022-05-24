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

        public void setName(string Name)
        {
            this.Name = Name;
        }
        public string getName()
        {
            return this.Name;
        }
        public void setPrice(int price)
        {
            this.price = price;
        }
        public int getPrice()
        {
            return this.price;
        }

        public void setAmount(int amount)
        {
            this.amount = amount;
        }
        public void minusAmount(int amount)
        {
            this.amount -= amount;
        }
        public int getAmount()
        {
            return amount;
        }
        //추가

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

        public void set_add_Draw(string val)
        {
            this.add_Draw = Convert.ToInt32(val);
        }
        public int get_add_Draw()
        {
            return add_Draw;
        }

        public void set_goto_Grave(string val)
        {
            this.goto_Grave = Convert.ToInt32(val);
        }
        public int get_goto_Grave()
        {
            return goto_Grave;
        }

        public void set_goto_Trash(string val)
        {
            this.goto_Trash = Convert.ToInt32(val);
        }
        public int get_goto_Trash()
        {
            return goto_Trash;
        }

        public void set_add_Action(string val)
        {
            this.add_Action = Convert.ToInt32(val);
        }
        public int get_add_Action()
        {
            return add_Action;
        }

        public void set_add_Buy(string val)
        {
            this.add_Buy = Convert.ToInt32(val);
        }
        public int get_add_Buy()
        {
            return add_Buy;
        }

        public void set_add_Money(string val)
        {
            this.add_Money = Convert.ToInt32(val);
        }
        public int get_add_Money()
        {
            return add_Money;
        }

        public void set_gain_to_Deck(string val)
        {
            this.gain_to_Deck = Convert.ToInt32(val);
        }
        public int get_gain_to_Deck()
        {
            return gain_to_Deck;
        }

        public void set_gain_to_Hand(string val)
        {
            this.gain_to_Hand = Convert.ToInt32(val);
        }
        public int get_gain_to_Hand()
        {
            return gain_to_Hand;
        }

        public void set_attack(string val)
        {
            if (val == "true")
            {
                this.attack = true;
            }
            else{
                this.attack = false;
            }
        }
        public bool get_attack()
        {
            return attack;
        }

        public void set_protect(string val)
        {
            if (val == "true")
            {
                this.protect = true;
            }
            else
            {
                this.protect = false;
            }
        }
        public bool get_protect()
        {
            return protect;
        }

        //생성자
        public ActionCard(string key, JToken jtoken)
        {
            Card card = new Card();
            //int 타입 멤버변수 배열
            string[] ops = { "price", "amount", "add_Draw", "goto_Grave", "goto_Trash",
                        "add_Action", "add_Buy", "add_Money", "gain_to_Deck",
                        "gain_to_Hand" };
            setName(key);

            int i = 0;
            foreach(JToken j in jtoken)
            { 
                foreach(JToken j2 in j)
                {
                    //효과이름(key값) 얻기위해 JProperty로 형변환
                    JProperty jp = j2.ToObject<JProperty>();
                    //효과이름 얻기
                    string subKey = jp.Name;
                    setAmount(10);

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
        private int money;
    }

    public class EstateCard : Card
    {
        private int score;
    }
}
