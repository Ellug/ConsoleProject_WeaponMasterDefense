using System;

namespace WeaponMasterDefense
{
    public class PausedRender
    {
        public void HandleInput()
        {
            if (InputSystem.IsKeyPressed(ConsoleKey.D1))
            {
                Program.ResumeGame();
                return;
            }

            if (InputSystem.IsKeyPressed(ConsoleKey.D2))
            {
                Program.Start();
                return;
            }

            if (InputSystem.IsKeyPressed(ConsoleKey.D3))
            {
                Program.ExitGame();
                return;
            }
        }

        public void DrawPauseMenu()
        {            
            RenderSystem.TextRender("PAUSE", 120, 5, "M", ConsoleColor.Yellow, ConsoleColor.Black);

            RenderSystem.TextRender("PRESS 1 TO RESUME", 90, 25, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 2 TO RESTART", 90, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 3 TO EXIT", 90, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
