using System;

namespace WeaponMasterDefense
{
    public static class GameRender
    {
        public static int GameWidth { get; private set; }
        public static int GameHeight { get; private set; }

        private static readonly string[] wallPattern = new string[]
        {
            "█████     ",
            "█████     ",
            "██████████",
            "██████████",
            "██████████",
            "██████████"
        };

        private static int wallHeight;

        public static void Init(int width = 320, int height = 85)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            GameWidth = width * 2 / 3;
            GameHeight = height;
            wallHeight = wallPattern.Length;

            // Init 시 성벽, 경계선 그리기
            DrawWall();
            DrawRightBorder();
            Console.ResetColor();
        }
        
        private static void DrawWall()
        {
            for (int i = 0; i < wallPattern.Length; i++)
            {
                Console.SetCursorPosition(0, GameHeight - wallPattern.Length + i);

                int written = 0;
                while (written < GameWidth)
                {
                    foreach (char c in wallPattern[i])
                    {
                        if (written >= GameWidth) break;

                        Console.BackgroundColor = (c == '█') ? ConsoleColor.White : ConsoleColor.Black;
                        Console.Write(" ");
                        written++;
                    }
                }
            }
            Console.ResetColor();
        }

        private static void DrawRightBorder()
        {
            Console.BackgroundColor = ConsoleColor.White;
            for (int y = 0; y < GameHeight; y++)
            {
                Console.SetCursorPosition(GameWidth - 1, y);
                Console.Write(" ");
            }
            Console.ResetColor();
        }

        public static void DrawField()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int y = 0; y < GameHeight - wallHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', GameWidth));
            }

            Console.ResetColor();

            DrawRightBorder();

            Console.ResetColor();
        }

        public static void DrawPlayer(Player player)
        {
            for (int i = 0; i < PlayerArt.Height; i++)
            {
                Console.SetCursorPosition(player.X, player.Y + i);

                string line = PlayerArt.Sprite[i];
                Console.ForegroundColor = ConsoleColor.Green; // 색상 예시
                Console.Write(line);
            }

            Console.ResetColor();
        }

    }
}
