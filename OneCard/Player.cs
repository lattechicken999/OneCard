using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    //Player 필수 기능 구상

    partial class Player: AbsPlayer
    {
        public Player()
        {
            lastCard = new Card();
            cards = new List<Card>();
        }
        public Player(List<Card> myDeck)
        {
            lastCard = new Card();
            cards = myDeck;
        }
    }
}
