using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    class CardDeck 
    {
        List<Card> cards;

        public CardDeck()
        {
            cards = new List<Card>();

            for (int i = 0; i<52;i++)
            {
                Card newCard = new Card();
                newCard.Pattern = (CardPattern)(i % 4 + 1);
                newCard.Num = (CardNum)(i % 13);
                cards.Add(newCard);
            }
            SuffleDeck();
        }

        public void SuffleDeck()
        {
            Random rnd = new Random();
            for (int i = cards.Count-1; i >0 ; i--)
            {
                int rndIndex = rnd.Next(0, i+1);
                var temp = cards[rndIndex];
                cards[rndIndex] = cards[i];
                cards[i] = temp;
            }
        }

        public List<Card> Draw(int num = 1)
        {
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }
    }
}
