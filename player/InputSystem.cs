using System;

namespace WeaponMasterDefense
{
    public static class InputSystem
    {
        public static bool IsPaused { get; set; } = false;
        public static bool ShouldExit { get; set; } = false;
        public static bool ShouldRestart { get; set; } = false;


        public static void HandleInput()
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;

            // 일시정지 상태가 아닐 때 인풋 시스템
            if (!IsPaused)
            {
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        // 플레이어 좌로 이동
                        break;

                    case ConsoleKey.RightArrow:
                        // 플레이어 좌로 이동
                        break;

                    case ConsoleKey.UpArrow:
                        // 플레이어 위로 이동
                        break;

                    case ConsoleKey.DownArrow:
                        // 플레이어 아래로 이동
                        break;

                    case ConsoleKey.Spacebar:
                        // 플레이어 공격
                        break;

                    case ConsoleKey.Q:
                    case ConsoleKey.W:
                    case ConsoleKey.E:
                    case ConsoleKey.R:
                        // 플레이어 스킬 사용
                        break;

                    case ConsoleKey.Escape:
                        PauseMenu();
                        break;
                }
            }
            else // 일시정지 상태일 때
            {
                switch (key)
                {
                    case ConsoleKey.Escape: // 재개
                        ResumeGame();
                        break;

                    case ConsoleKey.D1: // Restart
                        ShouldRestart = true;
                        break;

                    case ConsoleKey.D2: // Exit
                        ShouldExit = true;
                        break;
                }
            }
        }

        private static void PauseMenu()
        {
            IsPaused = true;
            Console.Clear();
            DrawWord.Render("PAUSE", 120, 5, "Big", ConsoleColor.Yellow, ConsoleColor.Black);

            DrawWord.Render("PRESS ESC TO RESUME", 90, 25, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("PRESS 1 TO RESTART", 90, 40, "Mid", ConsoleColor.White, ConsoleColor.Black);
            DrawWord.Render("PRESS 2 TO EXIT", 90, 55, "Mid", ConsoleColor.White, ConsoleColor.Black);
        }

        private static void ResumeGame()
        {
            IsPaused = false;
            Console.Clear();
            GameRender.Init();
        }
    }
}