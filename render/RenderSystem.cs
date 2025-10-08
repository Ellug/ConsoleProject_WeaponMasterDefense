using System;

namespace WeaponMasterDefense
{
    public static class RenderSystem
    {
        // Rect 채우기
        public static void FillRectChar(int x, int y, int w, int h, char ch, ConsoleColor fg, ConsoleColor bg = ConsoleColor.Black)
        {
            var rb = Renderer.RB;
            if (rb == null) return;
            rb.FillRect(x, y, w, h, ch, fg, bg);
        }

        // 텍스트 패턴 렌더
        public static void TextRender(string text, int startX, int startY, string fontType = "S", ConsoleColor color = ConsoleColor.Blue, ConsoleColor bg = ConsoleColor.Black)
        {
            foreach (char c in text.ToUpper())
            {
                string[] pattern = (fontType == "S") ? AlphabetS.GetPattern(c) : AlphabetM.GetPattern(c);
                DrawPattern(pattern, startX, startY, color, bg);
                startX += pattern[0].Length + 1;
            }
        }

        // 패턴 단위 렌더
        public static void DrawPattern(string[] pattern, int startX, int startY, ConsoleColor color, ConsoleColor bg = ConsoleColor.Black)
        {
            if (Renderer.RB != null)
            {
                for (int r = 0; r < pattern.Length; r++)
                {
                    var row = pattern[r];
                    for (int c = 0; c < row.Length; c++)
                    {
                        char ch = row[c];
                        if      (ch == ' ') Renderer.RB.Put(startX + c, startY + r, ' ', ConsoleColor.Gray, bg);                        
                        else if (ch == '█') Renderer.RB.Put(startX + c, startY + r, '█', color, bg);
                        else    Renderer.RB.Put(startX + c, startY + r, ch, color, bg);
                    }
                }
                return;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                foreach (char ch in pattern[i])
                {
                    if (ch == '█')
                    {
                        Console.BackgroundColor = color;
                        Console.Write(' ');
                    }
                    else if (ch == ' ')
                    {
                        Console.BackgroundColor = bg;
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.BackgroundColor = bg;
                        Console.ForegroundColor = color;
                        Console.Write(ch);
                    }
                }
            }
            Console.ResetColor();
        }
    }
}