using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class UsedCards
    {
        List<Card> usedCards;

        public void AddUsedCard(Card card)
        {
            usedCards.Add(card);
        }

        public List<Card> CleanCards()
        {
            Card temp = usedCards.Last();
            List<Card> returnCard = usedCards.GetRange(0, usedCards.Count-1);
            usedCards.Clear();
            usedCards.Add(temp);

            return returnCard;
        }

        public Card LastCard
        {
            get { return usedCards.Last(); }
        }
    }
}
