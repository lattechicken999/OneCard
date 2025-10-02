using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class CardDeck
    {
        //카드 드로우 시 덱에 드로우 카드보다 작다면 버린 카드 더미를 가져옴
        private List<Card> RefillCard()
        {
            return SuffleDeck(RestUsedCard());
        }
        //한 장 드로우
        public Card Draw()
        {
            if (cards.Count ==0)
            { cards.AddRange(RefillCard()); }
            Card returnCard = cards.First();
            cards.RemoveAt(0);
            return returnCard;
        }
        //여러장 드로우
        public List<Card> Draw(int num = 1)
        {
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            List<Card> returnCard = cards.GetRange(0, num);
            cards.RemoveRange(0, num);
            return returnCard;
        }

        //공격카드의 기능을 델리게이트에 담기위해 각각 공격카드 별 드로우 카드 제작
        public List<Card> Draw_Attact2()
        {
            const int num = 2;
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            List<Card> returnCard = cards.GetRange(0, num);
            cards.RemoveRange(0, num);
            return returnCard;
        }

        public List<Card> Draw_AttactA()
        {
            const int num = 3;
            List<Card> returnCard;
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            if(cards.Count < num)
            {
                returnCard  = cards.ToList();
                cards.Clear();
            }
            else
            {
                returnCard = cards.GetRange(0, num);
                cards.RemoveRange(0, num);
            }
            return returnCard;
        }

        public List<Card> Draw_AttactSpadeA()
        {
            const int num = 5;
            List<Card> returnCard;
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            if (cards.Count < num)
            {
                returnCard = cards.ToList();
                cards.Clear();
            }
            else
            {
                returnCard = cards.GetRange(0, num);
                cards.RemoveRange(0, num);
            }
            return returnCard;
        }
        public List<Card> Draw_AttactBlackJocker()
        {
            const int num = 5;
            List<Card> returnCard;
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            if (cards.Count < num)
            {
                returnCard = cards.ToList();
                cards.Clear();
            }
            else
            {
                returnCard = cards.GetRange(0, num);
                cards.RemoveRange(0, num);
            }
            return returnCard;
        }
        public List<Card> Draw_AttactColorJocker()
        {
            const int num = 7;
            List<Card> returnCard;
            if (cards.Count < num)
            { cards.AddRange(RefillCard()); }
            if (cards.Count < num)
            {
                returnCard = cards.ToList();
                cards.Clear();
            }
            else
            {
                returnCard = cards.GetRange(0, num);
                cards.RemoveRange(0, num);
            }
            return returnCard;
        }
    }
}
