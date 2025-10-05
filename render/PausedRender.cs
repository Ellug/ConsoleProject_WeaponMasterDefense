using System;

namespace WeaponMasterDefense
{
    public class PausedRender
    {
        public void HandleInput()
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Escape:     Program.ResumeGame();   break;
                case ConsoleKey.D1:         Program.Start();        break;
                case ConsoleKey.D2:         Program.ExitGame();     break;
            }
        }

        public void DrawPauseMenu()
        {            
            RenderSystem.TextRender("PAUSE", 120, 5, "M", ConsoleColor.Yellow, ConsoleColor.Black);

            RenderSystem.TextRender("PRESS ESC TO RESUME", 90, 25, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 1 TO RESTART", 90, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 2 TO EXIT", 90, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
