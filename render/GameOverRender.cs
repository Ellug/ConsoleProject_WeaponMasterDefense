using System;

namespace WeaponMasterDefense
{
    public class GameOverRender
    {
        public void HandleInput()
        {
            if (InputSystem.IsKeyPressed(ConsoleKey.D1) || InputSystem.IsKeyPressed(ConsoleKey.NumPad1))
            {
                Program.Start();
                return;
            }

            if (InputSystem.IsKeyPressed(ConsoleKey.D2) || InputSystem.IsKeyPressed(ConsoleKey.NumPad2))
            {
                Program.ExitGame();
                return;
            }        }


        public void DrawPauseMenu()
        {
            RenderSystem.TextRender("GAME OVER", 120, 5, "M", ConsoleColor.Red, ConsoleColor.Black);

            RenderSystem.TextRender("PRESS 1 TO RESTART", 90, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 2 TO EXIT", 90, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
