using System;

namespace WeaponMasterDefense
{
    public static class Renderer
    {
        public static RenderBuffer RB { get; private set; }

        public static void Init(int width, int height)
        {
            // 호환성 에러 회피용 예외처리
            try
            {
                Console.CursorVisible = false;
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                // 창/버퍼는 한 번만 세팅
                Console.SetWindowSize(Math.Min(width, Console.LargestWindowWidth),
                                      Math.Min(height, Console.LargestWindowHeight));
                Console.SetBufferSize(Math.Max(Console.WindowWidth, width),
                                      Math.Max(Console.WindowHeight, height));
            }
            catch { }

            RB = new RenderBuffer(width, height);
        }

        public static void BeginFrame(ConsoleColor bg) => RB.Clear(bg);

        public static void EndFrame() => RB.Present();
    }
}
