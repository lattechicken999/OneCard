using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    static class PlayingDisplay
    {
        static int DispSelectH;
        static int DispSelectW;
        //static int DispCardW; //굳이 여기 있을 필요 없음

        static List<int> enableIndexs = new List<int>();
        public static void SetEnableCardIndex(List<int> cardIndex)
        {
            enableIndexs = cardIndex;
        }

        //플레이어 카드가 유효한지 체크
        private static bool CheckEnableCard(int cardInedx)
        {
            if (enableIndexs.Contains(cardInedx))
            {
                return true;
            }    
            return false;
        }

        //플레이 화면 출력
        public static void DisplayPlaying(List<Card> userCards)
        {
            Console.Clear();
            Console.WriteLine(CardArt.backGround);
            DiplayGuide();
            DisplayLastCard();

            //카트 목록 출력 위치 설정
            DispSelectH = Console.WindowHeight - Console.WindowHeight/10;
            DispSelectW = (Console.WindowWidth -((userCards.Count/10+1) * 3))/2;
            int DispCardW = (Console.WindowWidth - ((userCards.Count/5+1) * 10)) / 2;
            if (DispSelectW<0) { DispSelectW = 0; }

            Console.SetCursorPosition(DispCardW, DispSelectH - 8);

            for(int i=0;i<userCards.Count; i++)
            {
                if(i%5 == 0)
                {
                    Console.SetCursorPosition(DispCardW, DispSelectH - 8+i/5);
                    //5개마다 1줄 추가
                }
                Console.Write($"  {i}.");
                DisplayCard(userCards[i]);
            }

            
        }

        /// <summary>
        /// 카드 선택 창 출력
        /// </summary>
        /// <param name="cardCount">카드 덱의 개 수</param>
        /// <param name="userSelectCard">선택 중인 카드의 인덱스 (사용 가능한 카드 인덱스만.. 체크 기능없음)</param>
        public static void DisplayUserSelection(int cardCount,int userSelectCard = -1)
        {

            Console.SetCursorPosition(DispSelectW, DispSelectH);

            for (int i = 0; i < cardCount; i++)
            {

                if (i % 10 == 0)
                {
                    Console.SetCursorPosition(DispSelectW, DispSelectH + i / 10);
                    //10개마다 1줄 추가
                }

                if (userSelectCard == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (enableIndexs.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"[{i}] ");
                Console.ResetColor();

            }
        }

        private static void DiplayGuide()
        {
            Console.SetCursorPosition(40,0);
            Console.WriteLine("[ ← → : 낼 카드 선택    Enter : 카드 내기    Esc : 카드 드로우  ]");
        }
        //플레이어 카드 덱 디스플레이
        private static void DisplayCard(Card card)
        {
            switch (card.Pattern)
            {
                case CardPattern.Heart:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♡");
                    break;
                case CardPattern.Spade:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("♤");
                    break;
                case CardPattern.Diamond:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("◇");
                    break;
                case CardPattern.Club:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("♧");
                    break;
                case CardPattern.Black:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("☆");
                    break;
                case CardPattern.Color:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("★");
                    break;
                default:
                    return;
            }
            Console.Write(card.Num + "    ");
            Console.ResetColor();
        }

        private static void DisplayLastCard()
        {
            int DispCenterH = Console.WindowHeight / 2-3;
            int DispCenterW = Console.WindowWidth / 2 -3;
            Console.SetCursorPosition(DispCenterW-2, DispCenterH);
            Console.WriteLine("┏         ┓");
            Console.SetCursorPosition(DispCenterW-2, DispCenterH+1);
            Console.Write("  ");
            DisplayCard(Player.LastCard);
            Console.SetCursorPosition(DispCenterW-2, DispCenterH + 2);
            Console.WriteLine("┗         ┛");
        }
    }
}
