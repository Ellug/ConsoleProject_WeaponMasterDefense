using System;

namespace WeaponMasterDefense
{
    public class GameOverRender
    {
        public void HandleInput()
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1: Program.Start(); break;
                case ConsoleKey.D2: Program.ExitGame(); break;
            }
        }

        public void DrawPauseMenu()
        {
            RenderSystem.TextRender("GAME OVER", 120, 5, "M", ConsoleColor.Red, ConsoleColor.Black);

            RenderSystem.TextRender("PRESS 1 TO RESTART", 90, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 2 TO EXIT", 90, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
