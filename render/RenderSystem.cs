using System;

namespace WeaponMasterDefense
{
    public static class RenderSystem
    {
        // 특정 영역 채우기
        public static void FillRect(int startX, int startY, int width, int height)
        {
            // 버퍼 초과 방지
            int maxY = Math.Min(startY + height, Console.BufferHeight);
            int maxX = Math.Min(startX + width, Console.BufferWidth);

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(new string(' ', width));
            }
        }

        // 텍스트를 정의된 패턴에 따라 렌더
        public static void Render(string text, int startX, int startY, string fontType = "M", ConsoleColor color = ConsoleColor.Blue, ConsoleColor bg = ConsoleColor.White)
        {
            foreach (char c in text.ToUpper())
            {
                string[] pattern;

                switch (fontType)
                {
                    case "M":
                        pattern = AlphabetM.GetPattern(c);
                        break;
                    case "S":
                        pattern = AlphabetS.GetPattern(c);
                        break;
                    default:
                        pattern = AlphabetM.GetPattern(c);
                        break;
                }

                DrawPattern(pattern, startX, startY, color, bg);
                startX += pattern[0].Length + 1;
            }
        }


        public static void DrawPattern(string[] pattern, int startX, int startY, ConsoleColor color, ConsoleColor bg = ConsoleColor.Black)
        {
            for (int i = 0; i < pattern.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);

                foreach (char c in pattern[i])
                {
                    if (c == '█')
                    {
                        Console.BackgroundColor = color;
                        Console.Write(" ");
                    }
                    else if (c == ' ')
                    {
                        Console.BackgroundColor = bg;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = color;
                        Console.Write(c);
                    }
                }
            }

            Console.ResetColor();
        }
    }
}