using System;

namespace WeaponMasterDefense
{
    public static class InputSystem
    {
        public static bool IsPaused { get; set; } = false;
        public static bool IsLevelUpUI { get; set; } = false;
        public static bool ShouldExit { get; set; } = false;
        public static bool ShouldRestart { get; set; } = false;


        public static void HandleInput(Player player)
        {
            if (!Console.KeyAvailable) return;

            var key = Console.ReadKey(true).Key;
                        
            if (!IsPaused && !IsLevelUpUI) // 게임 진행 중 인풋
            {
                switch (key)
                {
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

                    case ConsoleKey.Escape:     PauseMenu();            break;
                }
            }
            else if(IsLevelUpUI) // 레벨업시
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        // 메뉴 위로
                        break;
                    case ConsoleKey.DownArrow:
                        // 메뉴 아래로
                        break;
                    case ConsoleKey.Spacebar:
                        // 선택
                        break;
                }
            }
            else // 일시정지 상태일 때
            {
                switch (key)
                {
                    case ConsoleKey.Escape:     ResumeGame();               break;
                    case ConsoleKey.D1:         ShouldRestart = true;       break;
                    case ConsoleKey.D2:         ShouldExit = true;          break;
                }
            }
            // 레벨업일 때 UI 출력 및 인풋 시스템 정의 필요
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