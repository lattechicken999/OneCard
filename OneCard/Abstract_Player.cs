using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
     abstract partial class AbsPlayer
    {
        protected List<Card> cards;
        public string PlayerName
        { get; set; }
        //모든 플레이어는 마지막 카드 공유
        protected static Card lastCard = new Card();

        //외부 디스플레이용 
        //public List<Card> Cards { get { return cards; } }
        // 패널티 카드 공유
        public static event Func< List<Card>> TakePenaltyCard;
        //일반 카드 드로우
        public static event Func<Card> TakeCard;

        public static Card LastCard
        { get { return lastCard; } }

        /// <summary>
        /// 플레이어 턴 시 동작 수행
        /// </summary>
        /// <returns>true 시 카드를 냄. false 시 카드를 먹음</returns>
        public abstract bool MyTurn();

        //protected abstract bool DrawOrUseCard(List<int> enableCardIndex, out int selectedCardIndex);

    }
}
