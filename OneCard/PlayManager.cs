using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OneCard
{
    partial class PlayManager
    {
        LinkedList<AbsPlayer> players; //플레이어 정보
        static CardDeck cardDeck;
        //static CardDeck usedDeck;
        //플레이어가 우승 할 때 마다 Win을 뽑아 갈수 있게
        static Queue<PlayerStatus> PlayerWinsQueue = new Queue<PlayerStatus>();
        public static Card ThrowCard
        { set
            { cardDeck.UsedOneCard = value; }
        }

        public int AutoPlayerNum { get; set; }
        public PlayManager()
        {
            cardDeck = new CardDeck();

            players = new LinkedList<AbsPlayer>();
            players.AddFirst(new Player(cardDeck.Draw(7),"Its Me")); //기본 7장 드로우

            AutoPlayerNum = 3;
            for(int i =0;i<AutoPlayerNum;i++)
            {
                players.AddFirst(new AutoPlayer(cardDeck.Draw(7),$"Auto_{i+1}"));
                PlayerWinsQueue.Enqueue((PlayerStatus)(i + 1));
            }

            
        }

        /// <summary>
        /// 게임이 끝났는지 플레이어 덱을 보고 체크
        /// </summary>
        /// <returns></returns>
        public void CheckGameEnd(LinkedListNode<AbsPlayer> player)
        {
            if (player.Value.PlayerCardNum > 20)
            {
                player.Value.SetStatus(PlayerStatus.Out);
                player.Value.DisplayNotice(player.Value.PlayerName + "는 탈락했습니다.");
                //players.Remove(player);
            }
            else if (player.Value.PlayerCardNum == 0)
            {

            }
        }

        public void GamePlay()
        {
            var turn = players.Last;
            while (true)
            {
                PlayingDisplay.DisplayBackground();
                PlayingDisplay.DisplayAllPlayerRemainingCard(players);
                if(turn.Value.Status == PlayerStatus.Playing)
                {
                    //플레이 가능 할 때만 실행
                    if (turn.Value.MyTurn())
                    {
                        //카드를 냈을 때
                        CheckSpecialCard();
                        turn.Value.DisplayNotice();
                    }
                    else
                    {
                        //카드를 못냈을 때
                        turn.Value.DisplayNotice(turn.Value.PlayerName + "가 카드를 뽑습니다.");
                    }
                    //Thread.Sleep(2000);
                }
                CheckGameEnd(turn);
                //순환 구조용 확장 메서드
                turn = players.CycleNext(turn);
            }
        }
    }





    partial class PlayManager
    {
        //메서드 정의
        
        //마지막 카드의 공격카드 판단 및 처리
        private void CheckSpecialCard()
        {
            Card lastCard = AbsPlayer.LastCard;
            //공격 카드일 때 처리
            if (lastCard.Num == CardNum._2 && lastCard.Pattern != CardPattern.None)
            {
                AbsPlayer.TakePenaltyCard += cardDeck.Draw_Attact2;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern != CardPattern.Spade)
            {
                AbsPlayer.TakePenaltyCard += cardDeck.Draw_AttactA;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern == CardPattern.Spade)
            {
                AbsPlayer.TakePenaltyCard += cardDeck.Draw_AttactSpadeA;
            }
            else if(lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Black)
            {
                AbsPlayer.TakePenaltyCard += cardDeck.Draw_AttactBlackJocker;
            }
            else if (lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Color)
            {
                AbsPlayer.TakePenaltyCard += cardDeck.Draw_AttactColorJocker;
            }
            // 특수카드 일 때 처리
        }
    }
}
