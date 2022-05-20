using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardNS
{

    public class Card
    {
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
    }

    public class ActionCard : Card
    {
        private int add_Draw = 0;
        private int goto_Grave = 0;
        private int goto_Trash = 0;
        private int add_Action = 0;
        private int add_Buy = 0;
        private int add_Money = 0;
        private int gain_to_Deck = 0;
        private int gain_to_Hand = 0;
        private bool attack = false;
        private bool protect = false;

        public ActionCard(string CardName)
        {
            if (CardName.Equals("witch"))
            {
                add_Draw = 2;
                attack = true;
            }else if (CardName.Equals("market"))
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
