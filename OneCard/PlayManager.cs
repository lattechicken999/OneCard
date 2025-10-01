using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class PlayManager
    {
        LinkedList<Player> players; //플레이어 정보
        CardDeck cardDeck;

        public PlayManager()
        {
            cardDeck = new CardDeck();
            players = new LinkedList<Player>();
            players.AddFirst(new Player(cardDeck.Draw(7))); //기본 7장 드로우
            players.AddFirst(new Player(cardDeck.Draw(7))); //기본 7장 드로우
        }

        /// <summary>
        /// 게임이 끝났는지 플레이어 덱을 보고 체크
        /// </summary>
        /// <returns>True면 게인 속행 False면 게임 종료</returns>
        public bool CheckGameEnd()
        {
            return true;
        }

        public void GameTest()
        {

            players.First.Value.MyTurn();

        }
    }

    partial class PlayManager
    {
        //메서드 정의

        //마지막 카드의 공격카드 판단 및 처리
        private void CheckAttackCard()
        {
            Card lastCard = Player.LastCard;
            if (lastCard.Num == CardNum._2 && lastCard.Pattern != CardPattern.None)
            {
                Player.TakePenaltyCard += cardDeck.Draw_Attact2;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern != CardPattern.Spade)
            {
                Player.TakePenaltyCard += cardDeck.Draw_AttactA;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern == CardPattern.Spade)
            {
                Player.TakePenaltyCard += cardDeck.Draw_AttactSpadeA;
            }
            else if(lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Black)
            {
                Player.TakePenaltyCard += cardDeck.Draw_AttactBlackJocker;
            }
            else if (lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Color)
            {
                Player.TakePenaltyCard += cardDeck.Draw_AttactColorJocker;
            }
        }
    }
}
