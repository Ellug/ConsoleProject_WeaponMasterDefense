using System;

namespace WeaponMasterDefense
{
    public struct Cell
    {
        public char Ch;
        public ConsoleColor Fg;
        public ConsoleColor Bg;
    }

    public sealed class RenderBuffer
    {
        public readonly int Width;
        public readonly int Height;

        private readonly Cell[,] _front;    // 이전 프레임
        private readonly Cell[,] _back;     // 다음 프레임
        private readonly char[] _scratch;   // 한 줄 단위 일괄 출력용

        public RenderBuffer(int width, int height)
        {
            if (width <= 0 || height <= 0) throw new ArgumentOutOfRangeException();

            Width = width;
            Height = height;

            _front = new Cell[height, width];
            _back = new Cell[height, width];
            _scratch = new char[width];
        }

        // _back 클리어
        public void Clear(ConsoleColor bg)
        {
            var blank = new Cell { Ch = ' ', Fg = ConsoleColor.Gray, Bg = bg };
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++) _back[y, x] = blank;
            }
        }

        // cell _back 버퍼에 기록
        public void Put(int x, int y, char ch, ConsoleColor fg, ConsoleColor bg)
        {
            if ((uint)x >= (uint)Width || (uint)y >= (uint)Height) return;
            _back[y, x].Ch = ch;
            _back[y, x].Fg = fg;
            _back[y, x].Bg = bg;
        }

        // Rect 영역 _back 에 채우기
        public void FillRect(int x, int y, int w, int h, char ch, ConsoleColor fg, ConsoleColor bg)
        {
            int x0 = Math.Max(0, x);
            int y0 = Math.Max(0, y);
            int x1 = Math.Min(Width, x + w);
            int y1 = Math.Min(Height, y + h);

            for (int yy = y0; yy < y1; yy++)
                for (int xx = x0; xx < x1; xx++)
                {
                    _back[yy, xx].Ch = ch;
                    _back[yy, xx].Fg = fg;
                    _back[yy, xx].Bg = bg;
                }
        }

        // 스프라이트 _back에 그리기
        public void BlitSprite(int x, int y, string[] rows, ConsoleColor fg, ConsoleColor bg)
        {
            if (rows == null) return;
            for (int r = 0; r < rows.Length; r++)
            {
                var row = rows[r];
                int yy = y + r;
                if ((uint)yy >= (uint)Height) continue;

                for (int c = 0; c < row.Length; c++)
                {
                    int xx = x + c;
                    if ((uint)xx >= (uint)Width) continue;
                    char ch = row[c];
                    // 투명 처리 원하면 if (ch==' ') continue;
                    _back[yy, xx].Ch = ch;
                    _back[yy, xx].Fg = fg;
                    _back[yy, xx].Bg = bg;
                }
            }
        }

        // 문자열 한 줄을 _back에 작성 (text)
        public void DrawString(int x, int y, string text, ConsoleColor fg, ConsoleColor bg)
        {
            if (string.IsNullOrEmpty(text)) return;
            for (int i = 0; i < text.Length; i++)
            {
                int xx = x + i;
                if ((uint)xx >= (uint)Width || (uint)y >= (uint)Height) break;
                _back[y, xx].Ch = text[i];
                _back[y, xx].Fg = fg;
                _back[y, xx].Bg = bg;
            }
        }

        // _front와 _back 비교 후 다른 영역 그리기
        public void Present()
        {
            for (int y = 0; y < Height; y++)
            {
                int x = 0;
                while (x < Width)
                {
                    if (!Diff(_front[y, x], _back[y, x])) { x++; continue; }

                    var style = _back[y, x];
                    int start = x;

                    do { x++; }
                    while (x < Width && Diff(_front[y, x], _back[y, x]) && Same(_back[y, x], style));

                    int len = x - start;
                    for (int i = 0; i < len; i++)
                    {
                        var cell = _back[y, start + i];
                        _scratch[i] = cell.Ch;
                        _front[y, start + i] = cell;
                    }

                    Console.SetCursorPosition(start, y);
                    Console.ForegroundColor = style.Fg;
                    Console.BackgroundColor = style.Bg;
                    Console.Write(_scratch, 0, len);
                }
            }

            Console.ResetColor();
        }

        private static bool Diff(Cell a, Cell b) =>
            a.Ch != b.Ch || a.Fg != b.Fg || a.Bg != b.Bg;

        private static bool Same(Cell a, Cell b) =>
            a.Fg == b.Fg && a.Bg == b.Bg;
    }
}