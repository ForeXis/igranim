using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game_AI
{
    class Pile_Of_Stones
    {
        private int amount_of_stones;
        public int ret_aos()
        {
            return amount_of_stones;
        }
        public Pile_Of_Stones()
        {
            Random a = new Random();
            amount_of_stones = a.Next(10);
        }

        public Pile_Of_Stones(int n)
        {
            //Random a = new Random();
            amount_of_stones = n;
        }

        public int ch_of_st_am(int n)
        {
            amount_of_stones -= n;
            return amount_of_stones;
        }
    }
}
