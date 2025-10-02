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
        LinkedList<BasePlayer> players; //플레이어 정보
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

            players = new LinkedList<BasePlayer>();
            players.AddFirst(new Player(cardDeck.Draw(7),"Its Me")); //기본 7장 드로우

            AutoPlayerNum = 3;
            for(int i =0;i<AutoPlayerNum;i++)
            {
                players.AddFirst(new AutoPlayer(cardDeck.Draw(7),$"Auto_{i+1}"));
                PlayerWinsQueue.Enqueue((PlayerStatus)(i + 1));
            }

            
        }

        /// <summary>
        /// 각 플레이어가 플레이 가능한 상황인지 체크
        /// </summary>
        /// <returns></returns>
        public void CheckPlayable(LinkedListNode<BasePlayer> player)
        {
            if (player.Value.PlayerCardNum > 20)
            {
                player.Value.SetStatus(PlayerStatus.Out);
                player.Value.DisplayNotice(player.Value.PlayerName + "는 탈락했습니다.");
                cardDeck.AddSuffleDeck(player.Value.ReturnCardDeck());
            }
            else if (player.Value.PlayerCardNum == 0)
            {
                player.Value.SetStatus(PlayerWinsQueue.Dequeue());
                player.Value.DisplayNotice(player.Value.PlayerName + "는 승리 했습니다.");
            }
        }

        public int CheckPlayableUserCount(LinkedList<BasePlayer> players)
        {
            int playableUserCount = 0;
            foreach (BasePlayer player in players)
            {
                if(player.Status == PlayerStatus.Playing)
                {
                    playableUserCount++;
                }
            }
            return playableUserCount;
        }
        public bool GamePlay()
        {
            var turn = players.Last;
            while (true)
            {
                PlayingDisplay.DisplayBackground();

                if(turn.Value.Status == PlayerStatus.Playing)
                {
                    PlayingDisplay.DisplayAllPlayerRemainingCard(players);
                    PlayingDisplay.DisplayTurnInfo(turn.Value.PlayerName + "의 턴");
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
                    if(turn.Value.PlayerCardNum == 1)
                    {
                        BasePlayer.oneCardChallengerName = turn.Value.PlayerName;
                        if(!BasePlayer.StartOneCardCheckThread(players))
                        {
                            turn.Value.NormalDrawCard();
                        }
                    }
                    CheckPlayable(turn);
                    //Thread.Sleep(2000);
                }
                if (CheckPlayableUserCount(players) == 0  )
                {
                    //승리자를 모두 가려냈다면 종료
                    PlayingDisplay.DisplayGameEnd(players);
                    break;
                }
                //순환 구조용 확장 메서드
                turn = players.CycleNext(turn);
            }

            Console.WriteLine("게임이 종료 되었습니다.");
            Console.WriteLine("메뉴로 돌아 가신다면 Enter, 게임을 종료하신다면 ESC를 눌러주세요");
            while(true)
            {
                var inputKey = Console.ReadKey(true);
                if (inputKey.Key == ConsoleKey.Enter)
                {
                    return true;
                }
                else if(inputKey.Key == ConsoleKey.Escape)
                {
                    return false;
                }
            }

        }
    }





    partial class PlayManager
    {
        //메서드 정의
        
        //마지막 카드의 공격카드 판단 및 처리
        private void CheckSpecialCard()
        {
            Card lastCard = BasePlayer.LastCard;
            //공격 카드일 때 처리
            if (lastCard.Num == CardNum._2 && lastCard.Pattern != CardPattern.None)
            {
                BasePlayer.TakePenaltyCard += cardDeck.Draw_Attact2;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern != CardPattern.Spade)
            {
                BasePlayer.TakePenaltyCard += cardDeck.Draw_AttactA;
            }
            else if(lastCard.Num == CardNum._A && lastCard.Pattern == CardPattern.Spade)
            {
                BasePlayer.TakePenaltyCard += cardDeck.Draw_AttactSpadeA;
            }
            else if(lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Black)
            {
                BasePlayer.TakePenaltyCard += cardDeck.Draw_AttactBlackJocker;
            }
            else if (lastCard.Num == CardNum._Jocker && lastCard.Pattern == CardPattern.Color)
            {
                BasePlayer.TakePenaltyCard += cardDeck.Draw_AttactColorJocker;
            }
            // 특수카드 일 때 처리
        }
    }
}
