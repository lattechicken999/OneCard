using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    enum CardPattern
    {
        None,Diamond, Heart, Club, Spade,Black,Color, end
    }
    enum CardNum
    {
        None,_2, _3, _4, _5, _6, _7, _8, _9, _10, _J, _Q, _K, _A, _Joker
    }
    enum PlayerStatus
    {
        Playing, Win_1, Win_2, Win_3, Win_4, Win_5, Win_6, Out
    }

}
