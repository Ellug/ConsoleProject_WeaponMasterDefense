using System;

namespace WeaponMasterDefense
{
    public static class FieldRender
    {
        public static int GameWidth { get; private set; }
        public static int GameHeight { get; private set; }

        private static readonly string[] wallPattern = new string[]
        {
            "█████████         ",
            "█████████         ",
            "█████████         ",
            "█████████         ",
            "██████████████████",
            "██████████████████",
            "██████████████████",
            "██████████████████"
        };

        public static int wallHeight;

        public static void Init(int width = 320, int height = 85)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            GameWidth = width * 2 / 3;
            GameHeight = height;
            wallHeight = wallPattern.Length;

            ClearField();
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
            RenderSystem.FillRect(GameWidth - 1, 0, 1, GameHeight);
            Console.ResetColor();
        }

        public static void ClearField()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            RenderSystem.FillRect(0, 0, GameWidth, GameHeight - wallHeight);
            Console.ResetColor();
        }
    }
}