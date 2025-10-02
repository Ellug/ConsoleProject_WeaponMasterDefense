using System;
using System.Threading;

namespace WeaponMasterDefense
{
    class Intro
    {
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            DrawWord.Render("WEAPON MASTER", 100, 12, "Big", ConsoleColor.Blue);
            DrawWord.Render("DEFENSE", 130, 30, "Big", ConsoleColor.Blue);            

            bool visible = true;

            // 키 입력 감지 + 깜빡이 루프
            while (!Console.KeyAvailable)
            {
                if (visible)
                    DrawWord.Render("PRESS ANY BUTTON", 110, 58, "Mid", ConsoleColor.DarkRed);
                else
                    DrawWord.Render("PRESS ANY BUTTON", 110, 58, "Mid", ConsoleColor.White);

                visible = !visible;
                Thread.Sleep(500);
            }

            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();
            Manual();
        }

        public void Manual()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            DrawWord.Render("MANUAL", 140, 5, "Big", ConsoleColor.Yellow, ConsoleColor.Black);
            DrawWord.Render("MOVE : ← ↑ ↓ →", 80, 23, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("ATTACK : SPACEBAR", 80, 36, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("SKILL : Q W E R", 80, 49, "Mid", ConsoleColor.White, ConsoleColor.Black);

            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();
            Ment();
        }

        public void Ment()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            DrawWord.Render("YOU ARE THE LAST WARRIOR OF THE DEFENSE LINE", 8, 3, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("KILL THE ENEMIES AND HOLD THE DEFENSE LINE", 8, 16, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("OTHERWISE EVERYONE WILL BE BALD", 8, 29, "Mid", ConsoleColor.White, ConsoleColor.Black);

            Console.ReadKey(true);
            Console.ResetColor();
            Console.Clear();

            return;
        }

    }
}
