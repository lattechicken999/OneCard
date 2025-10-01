using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class CardDeck
    {
        public List<Card> Draw(int num = 1)
        {
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }

        //공격카드의 기능을 델리게이트에 담기위해 각각 공격카드 별 드로우 카드 제작
        public List<Card> Draw_Attact2()
        {
            int num = 2;
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }

        public List<Card> Draw_AttactA()
        {
            int num = 3;
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }

        public List<Card> Draw_AttactSpadeA()
        {
            int num = 5;
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }
        public List<Card> Draw_AttactBlackJocker()
        {
            int num = 5;
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }
        public List<Card> Draw_AttactColorJocker()
        {
            int num = 7;
            List<Card> returnCard = cards.GetRange(cards.Count - num - 1, num);
            cards.RemoveRange(cards.Count - num - 1, num);
            return returnCard;
        }
    }
}
