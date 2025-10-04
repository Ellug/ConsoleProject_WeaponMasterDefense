using System;

namespace WeaponMasterDefense
{
    public class LevelUpRender
    {
        public void HandleInput()
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.D1: Program.ResumeGame(); break;
                case ConsoleKey.D2: break;
                case ConsoleKey.D3: break;
                case ConsoleKey.D4: break;
            }
        }

        public void DrawLevelUpMenu()
        {
            RenderSystem.Render("Level UP", 120, 1, "M", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.Render("CHOOSE YOUR BUFF", 110, 13, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.Render("PRESS NUMBER", 120, 20, "S", ConsoleColor.White, ConsoleColor.Black);

            // 옵션들 출력
            RenderSystem.Render("PRESS 1 TO RESUME", 5, 32, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.Render("PRESS 2 TO EXIT", 5, 42, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
