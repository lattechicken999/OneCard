using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class Player : BasePlayer
    {

        /// <summary>
        /// 턴 수행 기능
        /// 1. 공격을 받았는지 확인
        /// 2. 그 상황에 맞게 낼 수 있는 카드 확인
        /// 3. Display 출력
        /// 4. 사용자 입력 대기
        /// </summary>
        /// <returns></returns>
        public override bool MyTurn()
        {
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
            PlayingDisplay.SetEnableCardIndex(enableCardIndex);
            PlayingDisplay.DisplayPlaying(cards);

            int selectedCardIndex;

            if (DrawOrUseCard(enableCardIndex, out selectedCardIndex))
            {
                if (attFlag)
                {
                    ApplyAttackCard();
                }
                else
                {
                    NormalDrawCard();
                }
                //사용 후 갱신
                PlayingDisplay.DisplayPlaying(cards);
                return false;
            }
            else
            {
                UseCard(selectedCardIndex, enableCardIndex);
                //사용후 갱신
                PlayingDisplay.DisplayPlaying(cards);
                return true;
            }

        }

        public override void SelectCardPattern()
        {
            while (true)
            {
                Console.Clear();
                PlayingDisplay.DisplayPlayerStatucNotice("7을 내셨습니다. 문양을 선택하세요. 1. ◆  2.♥ 3.♣ 4.♠");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1)
                {
                    lastCard.Pattern = CardPattern.Diamond;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    lastCard.Pattern = CardPattern.Heart;
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    lastCard.Pattern = CardPattern.Club;
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    lastCard.Pattern = CardPattern.Spade;
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        public override void WaitEnterInput()
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    if (oneCardInterceptPlayerName == "")
                    {
                        oneCardInterceptPlayerName = this.PlayerName;

                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 사용자의 카드 선택 기능
        /// </summary>
        /// <param name="enableCardIndex">사용 가능한 카드 인덱스 모음</param>
        /// <param name="selectedCardIndex">선택된 카드 인덱스 (카드 뽑기 시 -1)</param>
        /// <returns>true면 카드 뽑기, false면 카드 내기</returns>
        private bool DrawOrUseCard(List<int> enableCardIndex, out int selectedCardIndex)
        {
            int selectIndex = 0;
            bool isSelection = true;
            bool cardDraw = false;
            selectedCardIndex = -1;

            while (isSelection)
            {
                //카드 선택창 출력
                if (enableCardIndex.Count > 0)
                {
                    PlayingDisplay.DisplayUserSelection(cards.Count, enableCardIndex[selectIndex]);
                }
                else
                {
                    //선택할 것이 없을 때.
                    PlayingDisplay.DisplayUserSelection(cards.Count);
                }
                var inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    //키 입력시에만 디스플레이 갱신
                    case ConsoleKey.LeftArrow:
                        selectIndex = (selectIndex > 0) ? selectIndex - 1 : 0;
                        break;
                    case ConsoleKey.RightArrow:
                        selectIndex = (selectIndex < enableCardIndex.Count - 1) ? selectIndex + 1 : selectIndex;
                        break;
                    case ConsoleKey.Enter:
                        if (enableCardIndex.Count == 0)
                        {
                            //선택할게 없으면 무시함ㅇ
                            break;
                        }
                        isSelection = false;
                        selectedCardIndex = enableCardIndex[selectIndex];
                        break;
                    case ConsoleKey.Escape:
                        isSelection = false;
                        cardDraw = true;
                        break;
                    default:
                        break;
                }

            }
            return cardDraw;
        }



    }
}
