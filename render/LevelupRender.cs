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
                case ConsoleKey.D1: break;
                case ConsoleKey.D2: break;
                case ConsoleKey.D3: break;
                case ConsoleKey.D4: break;
            }
        }

        public void DrawLevelUpMenu()
        {
            RenderSystem.Render("Level UP", 120, 5, "M", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.Render("CHOOSE YOUR BUFF PRESS 1 2 3 4", 90, 25, "S", ConsoleColor.White, ConsoleColor.Black);

            // 옵션들 출력
            RenderSystem.Render("PRESS 1 TO RESTART", 5, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.Render("PRESS 2 TO EXIT", 5, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
