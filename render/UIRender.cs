using System;

namespace WeaponMasterDefense
{
    static class UIRender
    {
        private static int uiStartX;
        private static int uiWidth;
        private static int uiHeight;

        private static string lastScoreText = "";

        public static void Init(int totalWidth = 320, int totalHeight = 85)
        {
            uiStartX = totalWidth * 2 / 3;
            uiWidth = totalWidth - uiStartX;
            uiHeight = totalHeight;

            Console.BackgroundColor = ConsoleColor.Black;

            // UI 전체 배경 한번만 초기화
            for (int y = 0; y < uiHeight; y++)
            {
                Console.SetCursorPosition(uiStartX, y);
                Console.Write(new string(' ', uiWidth));
            }

            Console.ResetColor();

            // 고정 라벨들
            DrawWord.Render("SCORE", uiStartX + 6, 2, "Mid", ConsoleColor.Gray, ConsoleColor.Black);

            DrawWord.Render(" Q", uiStartX + 2, 20, "Mid", ConsoleColor.Cyan, ConsoleColor.Black);
            DrawWord.Render("W", uiStartX + 2, 32, "Mid", ConsoleColor.Cyan, ConsoleColor.Black);
            DrawWord.Render(" E", uiStartX + 2, 44, "Mid", ConsoleColor.Cyan, ConsoleColor.Black);
            DrawWord.Render(" R", uiStartX + 2, 56, "Mid", ConsoleColor.Cyan, ConsoleColor.Black);
        }

        public static void Update(int score = 0, int qLevel = 1, int wLevel = 1, int eLevel = 1, int rLevel = 1,
                                  double qCooldown = 0, double wCooldown = 0, double eCooldown = 0, double rCooldown = 0,
                                  double wallHpPercent = 1.0)
        {
            DrawScore(score);

            DrawSkill("Q", qLevel, qCooldown, 20);
            DrawSkill("W", wLevel, wCooldown, 32);
            DrawSkill("E", eLevel, eCooldown, 44);
            DrawSkill("R", rLevel, rCooldown, 56);

            DrawWallHp(wallHpPercent);
        }

        private static void DrawScore(int score)
        {
            string scoreText = score.ToString("D5");

            if (scoreText == lastScoreText) return;
            lastScoreText = scoreText;

            int charWidth = AlphabetMid.GetPattern('0')[0].Length;
            int spacing = 1;
            int totalWidth = (charWidth + spacing) * scoreText.Length;

            int startX = uiStartX + uiWidth - totalWidth - 2;

            // 점수 영역만 클리어
            Console.BackgroundColor = ConsoleColor.Black;
            for (int y = 0; y < AlphabetMid.GetPattern('0').Length * 2; y++)
            {
                Console.SetCursorPosition(startX, 2 + y);
                Console.Write(new string(' ', totalWidth));
            }

            DrawWord.Render(scoreText, startX, 2, "Mid", ConsoleColor.White, ConsoleColor.Black);
            Console.ResetColor();
        }

        private static void DrawSkill(string key, int level, double cooldown, int y)
        {
            // 레벨 숫자 표시
            DrawWord.Render(level.ToString(), uiStartX + 40, y, "Mid", ConsoleColor.White, ConsoleColor.Black);

            // 쿨타임 게이지 (0~1 비율)
            int barWidth = uiWidth - 55;
            int filled = (int)(barWidth * (1 - cooldown));
            // cooldown = 0 → 꽉 참, cooldown = 1 → 빈칸

            Console.SetCursorPosition(uiStartX + 55, y + 1);

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
