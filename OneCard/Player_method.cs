using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    partial class Player
    {
        //내 턴에 수행할 기능
        //1. 내 패에 낼 수 있는 카드 확인
        //2. 있다면 사용 가능한 카드 강조
        //3. 엔터는 카드 내기
        //4. esc는 카드 뽑기
        public override void MyTurn()
        {
            //카드 목록 디스플레이
            //DisplayManager 필요
            List<int> enableCardIndex = GetEnableCardList();
            PlayingDisplay.SetEnableCardIndex(enableCardIndex);
            PlayingDisplay.DisplayPlaying(cards);
            int selectedCardIndex;
            UserSelect(enableCardIndex,out selectedCardIndex);

        }

        /// <summary>
        /// 사용자의 카드 선택 기능
        /// </summary>
        /// <param name="enableCardIndex">사용 가능한 카드 인덱스 모음</param>
        /// <param name="selectedCardIndex">선택된 카드 인덱스 (카드 뽑기 시 -1)</param>
        /// <returns>true면 카드 뽑기, false면 카드 내기</returns>
        private bool UserSelect(List<int> enableCardIndex, out int selectedCardIndex)
        {
            int selectIndex = 0;
            bool isSelection = true;
            bool cardDraw = false;
            selectedCardIndex = -1;

            while (isSelection)
            {
                //카드 선택창 출력
                PlayingDisplay.DisplayUserSelection(cards.Count, enableCardIndex[selectIndex]);
                var inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    //키 입력시에만 디스플레이 갱신
                    case ConsoleKey.LeftArrow:
                        selectIndex = (selectIndex > 0)? selectIndex - 1 : 0;
                        break;
                    case ConsoleKey.RightArrow:
                        selectIndex = (selectIndex < enableCardIndex.Count-1)? selectIndex + 1 : selectIndex;
                        break;
                    case ConsoleKey.Enter:
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
