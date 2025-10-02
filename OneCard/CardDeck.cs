using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class CardDeck 
    {
        List<Card> cards;
        List<Card> usedCards;

        public Card UsedOneCard
        {
            set
            {
                usedCards.Add(value);
            }
        }
        public List<Card> UsedCardRange
        {
            set
            {
                usedCards.AddRange(value);
            }
        }

        public CardDeck()
        {
            cards = new List<Card>();
            usedCards = new List<Card>();

            // 한장 뽑기 기능 저장
            BasePlayer.TakeCard += Draw;

            for (int i = 0; i < 52; i++)
            {
                cards.Add(new Card((CardPattern)(i % 4 + 1), (CardNum)(i % 13 + 1)));
            }

            //흑백 조커 추가
            Card newBlackJocker = new Card(CardPattern.Black, CardNum._Jocker);
            cards.Add(newBlackJocker);

            //컬러 조커 추가
            Card newCard = new Card(CardPattern.Color, CardNum._Jocker);
            cards.Add(newCard);

            SuffleDeck();
        }
        /// <summary>
        /// 내 카드 덱 셔플
        /// </summary>
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
        /// <summary>
        /// 내부 사용 용도의 셔플 함수
        /// </summary>
        /// <param name="cardList"></param>
        /// <returns></returns>
        private List<Card> SuffleDeck(List<Card> cardList)
        {
            Random rnd = new Random();
            List<Card> returnList = cardList.ToList();
            for (int i = returnList.Count - 1; i > 0; i--)
            {
                int rndIndex = rnd.Next(0, i + 1);
                var temp = returnList[rndIndex];
                returnList[rndIndex] = returnList[i];
                returnList[i] = temp;
            }
            return returnList;
        }

        /// <summary>
        /// 추가할 카드 리스트를 셔플 하고나서 추가 
        /// </summary>
        /// <param name="cardList">추가할 카드 리스트</param>
        public void AddSuffleDeck(List<Card> cardList)
        {
            Random rnd = new Random();
            for (int i = cardList.Count - 1; i > 0; i--)
            {
                int rndIndex = rnd.Next(0, i + 1);
                var temp = cardList[rndIndex];
                cardList[rndIndex] = cardList[i];
                cardList[i] = temp;
            }
            cards.AddRange( cardList);
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void AddCard(List<Card> newCards)
        {
            cards.AddRange(newCards);
        }

        /// <summary>
        /// used Card List를 초기화 하고 삭제된 리스트 반환
        /// </summary>
        /// <returns>삭제된 리스트</returns>
        public List<Card> RestUsedCard()
        {
            List<Card> list = usedCards.ToList();
            usedCards.Clear();
            return list;
        }
    }
}
