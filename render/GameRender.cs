using System;
using System.Text;

namespace WeaponMasterDefense
{
    public static class GameRender
    {
        private static int gameWidth;
        private static int gameHeight;

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

            gameWidth = width * 2 / 3;
            gameHeight = height;
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
                Console.SetCursorPosition(0, gameHeight - wallPattern.Length + i);

                int written = 0;
                while (written < gameWidth)
                {
                    foreach (char c in wallPattern[i])
                    {
                        if (written >= gameWidth) break;

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
            for (int y = 0; y < gameHeight; y++)
            {
                Console.SetCursorPosition(gameWidth - 1, y);
                Console.Write(" ");
            }
            Console.ResetColor();
        }

        public static void DrawField()
        {
            Console.BackgroundColor = ConsoleColor.Black;

            for (int y = 0; y < gameHeight - wallHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(new string(' ', gameWidth));
            }

            Console.ResetColor();

            DrawRightBorder();

            Console.ResetColor();
        }
    }
}
