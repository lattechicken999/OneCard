using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{

    class MenuManager
    {
        public void DisplayMainMenu()
        {
            Console.WriteLine("원카드 게임");
            Console.WriteLine("1. 게임 플레이");
            Console.WriteLine("2. 게임 종료");
        }
        public bool UserCommand()
        {
            var userKey = Console.ReadKey();

            switch (userKey.Key)
            {
                case ConsoleKey.D1:
                    StartPlayGame();
                    break;
                case ConsoleKey.D2:
                    return StopPlayGame();
                default:
                    Console.WriteLine("잘못 입력했습니다.");
                    break;
            }
            return true;
        }
        private void StartPlayGame()
        {

        }
        private bool StopPlayGame() 
        {
            return false;
        }
    }
}
