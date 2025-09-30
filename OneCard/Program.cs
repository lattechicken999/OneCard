using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuManager menuManager = new MenuManager();
            bool isPlaying = true;
            while(isPlaying)
            {
                menuManager.DisplayMainMenu();
                isPlaying = menuManager.UserCommand();
            }
        }
    }
}
