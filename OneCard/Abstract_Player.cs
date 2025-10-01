using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    abstract class AbsPlayer
    {
        protected List<Card> cards;
        //모든 플레이어는 마지막 카드 공유
        protected static Card lastCard = new Card();
        // 패널티 카드 공유
        public static event Func< List<Card>> TakePenaltyCard;
        public static Card LastCard
        { get { return lastCard; } }
        public abstract void MyTurn();
        protected void UseCard(int useCardIndex, List<int> enableCards)
        {
            if (enableCards.Contains(useCardIndex))
            {
                lastCard = cards[useCardIndex];
                cards.RemoveAt(useCardIndex);
            }
        }
        protected List<int> GetEnableCardList()
        {
            List<int> enableIndex = new List<int>();
            for (int i = 0; i < cards.Count; i++)
            {
                //마지막 카드가 비어 있다면(초기 값이라면), 모든 카드 가능
                if (lastCard.Pattern == CardPattern.None && lastCard.Num == CardNum._2)
                {
                    enableIndex.Add(i);
                }
                //패턴이나 번호 둘중 하나라도 같으면 가능
                else if ((cards[i].Pattern == lastCard.Pattern) || (cards[i].Num == lastCard.Num))
                {
                    enableIndex.Add(i);
                }
                //마지막 카드가 공격카드라면, 기능 추가해야함
            }
            return enableIndex;
        }
    }
}
