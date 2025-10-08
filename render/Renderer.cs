using System;

namespace WeaponMasterDefense
{
    public static class Renderer
    {
        public static RenderBuffer RB { get; private set; }

        public static void Init(int width, int height) => RB = new RenderBuffer(width, height);

        public static void BeginFrame(ConsoleColor bg) => RB.Clear(bg);

        public static void EndFrame() => RB.Present();
    }
}
