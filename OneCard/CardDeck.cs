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

        public CardDeck()
        {
            // 한장 뽑기 기능 저장
            AbsPlayer.TakeCard += Draw;

            cards = new List<Card>();

            for (int i = 0; i<52;i++)
            {
                cards.Add( new Card( (CardPattern)(i % 4 + 1), (CardNum)(i % 13) ) );
            }

            //흑백 조커 추가
            Card newBlackJocker = new Card(CardPattern.Black, CardNum._Jocker);
            cards.Add(newBlackJocker);

            //컬러 조커 추가
            Card newCard = new Card(CardPattern.Color, CardNum._Jocker);
            cards.Add(newCard);

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
        public List<Card> SuffleDeck(List<Card> cardList)
        {
            Random rnd = new Random();
            for (int i = cardList.Count - 1; i > 0; i--)
            {
                int rndIndex = rnd.Next(0, i + 1);
                var temp = cardList[rndIndex];
                cardList[rndIndex] = cardList[i];
                cardList[i] = temp;
            }
            return cardList;
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void AddCard(List<Card> cards)
        {
            cards.AddRange(cards);
        }
        public List<Card> EmptyCard()
        {
            List<Card> list = cards.ToList();
            cards.Clear();
            return list;
        }
    }
}
