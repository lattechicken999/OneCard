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
            PlayerName = "";
        }
        public AutoPlayer(List<Card> myDeck,string name)
        {
            lastCard = new Card();
            cards = myDeck;
            PlayerName = name;  
        }

        /// <summary>
        /// 오토 플레이어 턴 동작 정의
        /// </summary>
        /// <returns></returns>
        public override bool MyTurn()
        {
            Random rnd = new Random();
            List<int> enableCardIndex;
            bool attFlag = IsAttackTurn();
            if (attFlag)
            {
                enableCardIndex = GetAttactedEnableCardList();
            }
            else
            {
                enableCardIndex = GetEnableCardList();
            }

            if (enableCardIndex.Count > 0)
            {
                //선택 할 수 있는 카드가 있다면 랜덤하게 내기
                var useCard = cards[enableCardIndex[rnd.Next(enableCardIndex.Count)]];
                UseCard(useCard);
                return true;
            }
            else
            {
                //선택할 수 있는 카드가 없다면 카드를 먹음
                if (attFlag)
                {

                    ApplyAttackCard();

                }
                else
                {
                    NormalDrawCard();
                }
                return false;
            }
        }
    }
}
