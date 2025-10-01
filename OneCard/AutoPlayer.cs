using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    class AutoPlayer : AbsPlayer
    {
        public AutoPlayer()
        {
            lastCard = new Card();
            cards = new List<Card>();
        }
        public AutoPlayer(List<Card> myDeck)
        {
            lastCard = new Card();
            cards = myDeck;
        }
        public override bool MyTurn()
        {
            Console.WriteLine("순환구조 완");
            return false;
        }

        protected override bool DrawOrUseCard(List<int> enableCardIndex, out int selectedCardIndex)
        {
            selectedCardIndex = -1;
            return false;
        }
    }
}
