using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class Player
    {
        protected List<Card> cards;
        //모든 플레이어 객체 공유
        protected static Card lastCard;

        public static Card LastCard
        { get { return lastCard; } }

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
        //내 턴에 수행할 기능
        //1. 내 패에 낼 수 있는 카드 확인
        //2. 있다면 사용 가능한 카드 강조
        //3. 엔터는 카드 내기
        //4. esc는 카드 뽑기
        public void MyTurn()
        {
            //카드 목록 디스플레이
            //DisplayManager 필요
            PlayingDisplay.SetEnableCardIndex(GetEnableCardList());
            PlayingDisplay.DisplayUserPlay(cards);
            var inputKey = Console.ReadKey();
            
        }
        protected List<int> GetEnableCardList()
        {
            List<int> enableIndex = new List<int>();
            for(int i=0; i< cards.Count;i++)
            {
                if ((cards[i].Pattern == lastCard.Pattern) || (cards[i].Num == lastCard.Num))
                {
                    enableIndex.Add(i);
                }
            }
            return enableIndex;
        }

    }
}
