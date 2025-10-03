using System;

namespace WeaponMasterDefense
{
    public static class DrawWord
    {
        // 텍스트를 정의된 패턴에 따라 렌더
        public static void Render(string text, int startX, int startY, string fontType = "Big", ConsoleColor color = ConsoleColor.Blue, ConsoleColor bg = ConsoleColor.White)
        {
            int spacing = 1;

            foreach (char c in text.ToUpper())
            {
                string[] pattern = fontType == "Mid"
                    ? AlphabetMid.GetPattern(c)
                    : AlphabetBig.GetPattern(c);

                DrawPattern(pattern, startX, startY, color, bg);

                startX += (pattern[0].Length) + spacing;
            }
        }

        private static void DrawPattern(string[] pattern, int startX, int startY, ConsoleColor color, ConsoleColor bg)
        {
            for (int y = 0; y < pattern.Length; y++)
            {
                for (int dy = 0; dy < 2; dy++)
                {
                    Console.SetCursorPosition(startX, startY + y * 2 + dy);

                    foreach (char c in pattern[y])
                    {
                        Console.BackgroundColor = (c == '█') ? color : bg;
                        Console.Write(" ");
                    }
                }
            }
            Console.ResetColor();
        }
    }
}