﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class GameTable
    {
        public int ActionNumber { get; set; }
        public int BuyNumber { get; set; }
        public int Coin { get; set; }

        public GameTable()
        {
            ActionNumber = 1;
            BuyNumber = 1;
            Coin = 0;
        }

        public void initGameTable()
        {
            ActionNumber = 1;
            BuyNumber = 1;
            Coin = 0;
        }

    }
}
