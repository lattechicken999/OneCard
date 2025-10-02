using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    abstract partial class AbsPlayer
    {
        /// <summary>
        /// 카드 사용시 호출 메서드
        /// </summary>
        /// <param name="useCardIndex"></param>
        /// <param name="enableCards"></param>
        protected void UseCard(int useCardIndex, List<int> enableCards)
        {
            if (enableCards.Contains(useCardIndex))
            {
                lastCard = cards[useCardIndex].DeepCopy();
                PlayManager.ThrowCard = cards[useCardIndex];
                cards.RemoveAt(useCardIndex);
            }
        }
        /// <summary>
        /// 카드 사용시 호출 메서드
        /// Auto 용
        /// </summary>
        /// <param name="useCardIndex"></param>
        /// <param name="enableCards"></param>
        protected void UseCard(Card usingCard)
        {
            lastCard = usingCard.DeepCopy();
            PlayManager.ThrowCard = usingCard;
            cards.Remove(usingCard);
        }

        /// <summary>
        /// 보통 상황에서 사용가능한 카드 체크
        /// </summary>
        /// <returns></returns>
        protected List<int> GetEnableCardList()
        {
            List<int> enableIndex = new List<int>();
            for (int i = 0; i < cards.Count; i++)
            {
                //마지막 카드가 비어 있다면(초기 값이라면), 혹은 조커카드라면 모든 카드 가능
                if (lastCard.Num == CardNum._Jocker || (lastCard.Pattern == CardPattern.None && lastCard.Num == CardNum._2))
                {
                    enableIndex.Add(i);
                }
                //조커카드는 언제나 가능
                else if (lastCard.Num == CardNum._Jocker)
                {
                    enableIndex.Add(i);
                }
                //패턴이나 번호 둘중 하나라도 같으면 가능
                else if ((cards[i].Pattern == lastCard.Pattern) || (cards[i].Num == lastCard.Num))
                {
                    enableIndex.Add(i);
                }

            }
            return enableIndex;
        }

        /// <summary>
        /// 공격 당했을 때 호출하는 카드 체크
        /// </summary>
        /// <param name="attackCard">공격중인 카드</param>
        /// <returns></returns>
        protected List<int> GetAttactedEnableCardList()
        {
            List<int> enableIndex = new List<int>();
            for (int i = 0; i < cards.Count; i++)
            {
                //카드가 2라면, 2,A,Jocker 가능
                if (lastCard.Num == CardNum._2 &&
                    (cards[i].Num == CardNum._2 ||
                     cards[i].Num == CardNum._A ||
                     cards[i].Num == CardNum._Jocker))

                {
                    enableIndex.Add(i);
                }
                //카드가 A(스페이드 제외) 라면 A와 조커 가능
                else if ((lastCard.Num == CardNum._A && lastCard.Pattern != CardPattern.Spade) &&
                         (cards[i].Num == CardNum._A ||
                          cards[i].Num == CardNum._Jocker))
                {
                    enableIndex.Add(i);
                }
                //카드가 스페이드 A라면 초커만 가능
                else if ((lastCard.Num == CardNum._A && lastCard.Pattern == CardPattern.Spade) &&
                         (cards[i].Num == CardNum._Jocker))
                {
                    enableIndex.Add(i);
                }
                //카드가 블랙 조커라면 컬러조커만 가능(그치만 블랙카드가 나왔다면 컬러조커밖에 없을 것)
                else if ((lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Black) &&
                           (cards[i].Num == CardNum._Jocker))
                {
                    enableIndex.Add(i);
                }

            }
            return enableIndex;
        }

        /// <summary>
        /// 턴 시작시 공격받았는지 확인
        /// 마지막 카드가 공격이어도 공격을 안받았을 수 있기 때문에 델리게이트 확인
        /// </summary>
        /// <returns></returns>
        protected bool IsAttackTurn()
        {
            if (lastCard.Pattern != CardPattern.None && TakePenaltyCard != null &&
               (lastCard.Num == CardNum._2 ||
                lastCard.Num == CardNum._A ||
                lastCard.Num == CardNum._Jocker))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 누적된 공격 카드를 내 카드 덱에 넣음
        /// </summary>
        protected void ApplyAttackCard()
        {
            //var 을 쓰면 확장자가 delegate로 됨. 
            //Func<List<Card>> 타입으로 케스팅을 해주거나, 애초에 타입을 지정하자.
            foreach (Func<List<Card>> penaltyCard in TakePenaltyCard.GetInvocationList())
            {
                cards.AddRange(penaltyCard.Invoke());
            }
            TakePenaltyCard = null;
        }
        protected void NormalDrawCard()
        {
            cards.Add(TakeCard.Invoke());
        }
        public void DisplayRemainingCard()
        {
            PlayingDisplay.DisplayPlayerRemainingCard(cards.Count, PlayerName);
        }
    }
}
