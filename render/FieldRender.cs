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

        // 플레이 구역 경계
        public static int PlayLeft => 0;
        public static int PlayTop => 0;
        public static int PlayRight => GameWidth - 2;           // GameWidth-1 은 우측 경계선 열
        public static int WallTop => GameHeight - wallHeight;   // 벽 시작 Y
        public static int PlayBottom => WallTop - 1;            // 벽 바로 위까지

        public static void Init(int width = 320, int height = 85)
        {
            GameWidth = width * 2 / 3;
            GameHeight = height;
            wallHeight = wallPattern.Length;
        }

        public static void Draw()
        {            
            // 필드 영역 클리어 (우측 경계선 제외, 하단 벽 위까지만)
            RenderSystem.FillRectChar(PlayLeft, PlayTop, (PlayRight - PlayLeft + 1), (PlayBottom - PlayTop + 1), ' ', ConsoleColor.Black);

            // 성벽
            int wallTop = WallTop;
            int tileW = wallPattern[0].Length;
            int maxStartX = PlayRight - tileW + 1;

            for (int x = PlayLeft; x <= maxStartX; x += tileW) RenderSystem.DrawPattern(wallPattern, x, wallTop, ConsoleColor.White, ConsoleColor.Black);
            
            int remainder = (PlayRight + 1) % tileW;
            if (remainder > 0)
            {
                string[] cropped = new string[wallPattern.Length];
                for (int i = 0; i < wallPattern.Length; i++) cropped[i] = wallPattern[i].Substring(0, remainder);

                int cropStartX = PlayRight + 1 - remainder;
                RenderSystem.DrawPattern(cropped, cropStartX, wallTop, ConsoleColor.White, ConsoleColor.Black);
            }

            // 우측 경계선
            RenderSystem.FillRectChar(GameWidth - 1, 0, 1, GameHeight, '█', ConsoleColor.White);
        }
    }
}