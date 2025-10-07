using System;
using System.Runtime.InteropServices;

namespace WeaponMasterDefense
{
    public static class InputSystem
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static void HandleInput(Player player)
        {
            if (IsKeyDown(ConsoleKey.Escape)) Program.SetPaused();

            if (IsKeyDown(ConsoleKey.LeftArrow)) player.MoveLeft();
            if (IsKeyDown(ConsoleKey.RightArrow)) player.MoveRight();
            if (IsKeyDown(ConsoleKey.UpArrow)) player.MoveUp();
            if (IsKeyDown(ConsoleKey.DownArrow)) player.MoveDown();

            if (IsKeyPressed(ConsoleKey.Q)) player.skills[0].Activate(player);
            if (IsKeyPressed(ConsoleKey.W)) player.skills[1].Activate(player);
            if (IsKeyPressed(ConsoleKey.E)) player.skills[2].Activate(player);
            if (IsKeyPressed(ConsoleKey.R)) player.skills[3].Activate(player);
        }

        public static bool IsKeyDown(ConsoleKey key)
        {
            return (GetAsyncKeyState((int)key) & 0x8000) != 0;
        }

        private static readonly bool[] prevState = new bool[256];
        public static bool IsKeyPressed(ConsoleKey key)
        {
            int keyCode = (int)key;
            bool isNowPressed = (GetAsyncKeyState(keyCode) & 0x8000) != 0;
            bool pressed = isNowPressed && !prevState[keyCode]; 
            prevState[keyCode] = isNowPressed;
            return pressed;
        }

        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable) Console.ReadKey(true);
        }
    }
}