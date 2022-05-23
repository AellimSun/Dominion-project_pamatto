using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Market : GameTable
    {
        public List<Card> MarketPile;
        public Market()
        {

        }
        public void SellCard(Card card)
        {
            card.minusAmount(1);
        }

    }
}
