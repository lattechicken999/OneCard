using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneCard
{
    abstract partial class BasePlayer
    {
        public static string oneCardChallengerName = "";
        protected static string oneCardInterceptPlayerName = "";

        /// <summary>
        /// 플레이도중인 플레이어 카드가 한장일 때 실행.
        /// 각각 스레드로 Enter를 입력받도록 수행
        /// 엔터를 입력 받으면 oneCardInterceptPlayerName 에 이름 등록
        /// oneCardInterceptPlayerName 와 oneCardChallengerName 의 이름이 같으면 true
        /// 아니면 false
        /// </summary>
        /// <param name="players">게임중인 플레이어들</param>
        /// <returns></returns>
        public static bool StartOneCardCheckThread(LinkedList<BasePlayer> players)
        {
            List<Thread> myThreadList = new List<Thread>();

            foreach(BasePlayer player in players)  
            {
                if (player.Status == PlayerStatus.Playing)
                {
                    myThreadList.Add(new Thread(player.WaitEnterInput));
                }
            }

            foreach (Thread thr in myThreadList)
            {
                thr.Start();
            }

            while (oneCardInterceptPlayerName == "")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                PlayingDisplay.DisplayPlayerStatucNotice(oneCardChallengerName+"가 원카드 시도중 !!!");
            }
            Console.ResetColor();
            Console.Clear();

            if (oneCardInterceptPlayerName != oneCardChallengerName)
            {
                foreach (Thread thr in myThreadList)
                {
                    thr.Abort();
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                PlayingDisplay.DisplayPlayerStatucNotice(oneCardChallengerName + "의 원카드 시도를 " + oneCardInterceptPlayerName+"가 막음!!");
                Console.ResetColor();
                oneCardInterceptPlayerName = "";
                Thread.Sleep(2000);
                return false;
            }
            else
            {
                foreach (Thread thr in myThreadList)
                {
                    thr.Abort();
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                PlayingDisplay.DisplayPlayerStatucNotice(oneCardChallengerName + "는 원카드를 성공했다!!");
                Console.ResetColor();
                Thread.Sleep(2000);
                return true;
            }
        }
        public abstract void WaitEnterInput();
    }

}
