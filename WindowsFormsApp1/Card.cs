using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Card
    {
        public json cardInfo = new json();
        private int price = 0;
        private int amount = 0;
        private string Name = "";

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
        public int goto_Trash { get; set; }
        public int add_Action { get; set; }
        public int add_Buy { get; set; }
        public int add_Money { get; set; }
        public int gain_to_Deck { get; set; }
        public int gain_to_Hand { get; set; }
        public bool attack = false;
        public bool protect = false;

        public ActionCard(string CardName)
        {
            if (CardName.Equals("witch"))
            {
                add_Draw = 2;
                attack = true;
            }
            else if (CardName.Equals("market"))
            {

            }
        }

        public void setIsAttack(bool attack)
        {
            this.attack = attack;
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

    /*public class witch : ActionCard
    {
        public witch()
        {
            setPrice(5);
            setIsAttack(true);
        }
    }*/
}
