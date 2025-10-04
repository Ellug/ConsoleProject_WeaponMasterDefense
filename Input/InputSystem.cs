using System;

namespace WeaponMasterDefense
{
    public static class InputSystem
    {
        public static void HandleInput(Player player)
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Escape:     Program.SetPaused();    break;

                case ConsoleKey.LeftArrow:  player.MoveLeft();      break;
                case ConsoleKey.RightArrow: player.MoveRight();     break;
                case ConsoleKey.UpArrow:    player.MoveUp();        break;
                case ConsoleKey.DownArrow:  player.MoveDown();      break;

                case ConsoleKey.Q:
                    // 플레이어 스킬 사용
                    break;
                case ConsoleKey.W:
                    // 플레이어 스킬 사용
                    break;
                case ConsoleKey.E:
                    // 플레이어 스킬 사용
                    break;
                case ConsoleKey.R:
                    // 플레이어 스킬 사용
                    break;
            }
        }
    }
}