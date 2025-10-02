using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    //Player 필수 기능 구상

    partial class Player: BasePlayer
    {
        public Player()
        {
            lastCard = new Card();
            cards = new List<Card>();
            PlayerName = "";
        }
        public Player(List<Card> myDeck,string name)
        {
            lastCard = new Card();
            cards = myDeck;
            PlayerName = name;
        }
    }
}
