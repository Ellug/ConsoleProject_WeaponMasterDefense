using System;

namespace WeaponMasterDefense
{
    static class UIRender
    {
        private static int uiStartX;
        private static int uiWidth;
        private static int uiHeight;

        private static int lastScore = 0;

        public static void Init(int totalWidth = 320, int totalHeight = 85)
        {
            uiStartX = totalWidth * 2 / 3;
            uiWidth = totalWidth - uiStartX;
            uiHeight = totalHeight;

            // UI 전체 배경 한번만 초기화
            Console.BackgroundColor = ConsoleColor.Black;
            RenderSystem.FillRect(uiStartX, 0, uiWidth, uiHeight);
            Console.ResetColor();

            // 고정
            RenderSystem.Render("SCORE", uiStartX + 6, 2, "S", ConsoleColor.Gray, ConsoleColor.Black);

            RenderSystem.Render(" Q", uiStartX + 2, 20, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" W", uiStartX + 2, 32, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" E", uiStartX + 2, 44, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" R", uiStartX + 2, 56, "S", ConsoleColor.Cyan, ConsoleColor.Black);

            // Init시 DrawScore 무조건 한번
            DrawScore(-1);
        }

        public static void Update(int score = 0, int qLevel = 1, int wLevel = 1, int eLevel = 1, int rLevel = 1,
                                  double qCooldown = 0, double wCooldown = 0, double eCooldown = 0, double rCooldown = 0,
                                  double wallHpPercent = 1.0)
        {

            if (score != lastScore) DrawScore(score);

            int barStartY = 20;
            DrawSkill("Q", qLevel, qCooldown, barStartY);
            DrawSkill("W", wLevel, wCooldown, barStartY + 12);
            DrawSkill("E", eLevel, eCooldown, barStartY + 24);
            DrawSkill("R", rLevel, rCooldown, barStartY + 36);

            DrawWallHp(wallHpPercent);
        }

        private static void DrawScore(int score)
        {
            string scoreText = score.ToString("D5");
            lastScore = score;

            int charWidth = AlphabetS.GetPattern('0')[0].Length;
            int totalWidth = (charWidth + 2) * scoreText.Length;

            int startX = uiStartX + uiWidth - totalWidth;

            // 점수 영역 갱신
            Console.BackgroundColor = ConsoleColor.Black;
            RenderSystem.FillRect(startX - 7, 2, totalWidth + 7, AlphabetS.GetPattern('0').Length);
            RenderSystem.Render(scoreText, startX, 2, "S", ConsoleColor.White, ConsoleColor.Black);
            Console.ResetColor();
        }

        private static void DrawSkill(string key, int level, double cooldown, int y)
        {
            // 레벨 숫자 표시
            RenderSystem.Render(level.ToString(), uiStartX + 30, y, "S", ConsoleColor.White, ConsoleColor.Black);

            // 쿨타임 게이지 (0~1 비율)
            int barWidth = uiWidth - 50;
            int filled = (int)(barWidth * (1 - cooldown));
            // cooldown = 0 → 꽉 참, cooldown = 1 → 빈칸

            Console.SetCursorPosition(uiStartX + 45, y + 2);

            for (int i = 0; i < barWidth; i++)
            {
                if (i < filled)
                {
                    // 쿨다운 진행 중이면 노란색, 끝났으면 다크싸이언
                    if (cooldown > 0)
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(" ");
            }

            Console.ResetColor();
        }

        private static void DrawWallHp(double percent)
        {
            int barWidth = uiWidth - 4;
            int filled = (int)(barWidth * percent);

            ConsoleColor hpColor;
            if (percent >= 0.7) hpColor = ConsoleColor.Green;
            else if (percent >= 0.3) hpColor = ConsoleColor.Yellow;
            else hpColor = ConsoleColor.Red;

            for (int row = 0; row < 3; row++)
            {
                Console.SetCursorPosition(uiStartX + 2, uiHeight - (4 - row));
                for (int i = 0; i < barWidth; i++)
                {
                    Console.BackgroundColor = (i < filled) ? hpColor : ConsoleColor.DarkGray;
                    Console.Write(" ");
                }
            }

            Console.ResetColor();
        }

    }
}
