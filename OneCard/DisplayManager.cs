using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    static class PlayingDisplay
    {
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
        public static void DisplayUserPlay(List<Card> userCards)
        {
            Console.Clear();
            Console.WriteLine(CardArt.backGround);
            DisplayLastCard();

            //카트 목록 출력 위치 설정
            int DispSelectH = Console.WindowHeight - Console.WindowHeight/10;
            int DispSelectW = (Console.WindowWidth -(userCards.Count * 3))/2;
            int DispCardW = (Console.WindowWidth - (userCards.Count * 7)) / 2;
            if (DispSelectW<0) { DispSelectW = 0; }

            Console.SetCursorPosition(DispCardW, DispSelectH - 5);

            for(int i=0;i<userCards.Count; i++)
            {
                if(i%5 == 0)
                {
                    Console.SetCursorPosition(DispCardW, DispSelectH - 5+i/5);
                    //5개마다 1줄 추가
                }
                Console.Write($"  {i}.");
                DisplayCard(userCards[i]);
            }

            Console.SetCursorPosition(DispSelectW, DispSelectH);

            for(int i = 0; i < userCards.Count; i++) 
            {

                if (i % 10 == 0)
                {
                    Console.SetCursorPosition(DispSelectW, DispSelectH  + i / 5);
                    //5개마다 1줄 추가
                }

                if (enableIndexs.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write($"[{i}] ");
                Console.ResetColor();
                
            }
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
            Console.SetCursorPosition(DispCenterW, DispCenterH);
            Console.WriteLine("       ");
            Console.SetCursorPosition(DispCenterW, DispCenterH+1);
            Console.Write(" ");
            DisplayCard(Player.LastCard);
            Console.WriteLine(" ");
            Console.SetCursorPosition(DispCenterW, DispCenterH + 2);
            Console.WriteLine("       ");
        }
    }
}
