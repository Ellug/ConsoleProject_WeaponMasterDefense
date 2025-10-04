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

                case ConsoleKey.Q:  player.skills[0].Activate(player);  break;
                case ConsoleKey.W:  player.skills[1].Activate(player);  break;
                case ConsoleKey.E:  player.skills[2].Activate(player);  break;
                case ConsoleKey.R:  player.skills[3].Activate(player);  break;
            }
        }
    }
}