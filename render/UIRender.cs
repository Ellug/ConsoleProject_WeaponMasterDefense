using System;

namespace WeaponMasterDefense
{
    static class UIRender
    {
        private static int uiStartX;
        private static int uiWidth;
        private static int uiHeight;

        private static int lastScore = 0;
        private static int prevHP = -1;
        private static int prevExp = -1;

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

            RenderSystem.Render(" Q", uiStartX + 2, 13, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" W", uiStartX + 2, 23, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" E", uiStartX + 2, 33, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.Render(" R", uiStartX + 2, 43, "S", ConsoleColor.Cyan, ConsoleColor.Black);

            // Init시 DrawScore, DrawWallHP, DrawExp 무조건 한번
            DrawScore(-1);
            DrawWallHp(200);
            DrawExp(200, 100000);
        }

        public static void Update(Player player, int score = 0)
        {
            if (score != lastScore)
                DrawScore(score);

            // 스킬 4개 표시
            int barStartY = 13;

            for (int i = 0; i < player.skills.Length; i++)
            {
                Skill skill = player.skills[i];
                if (skill == null) continue;

                // 쿨타임 비율 계산
                double ratio = 0;
                if (skill.Cooldown > 0) ratio = skill.CurrentCooldown / skill.Cooldown;

                string key = ((char)('Q' + i)).ToString();
                DrawSkill(key, skill.Level, ratio, barStartY + i * 10);
            }

            DrawWallHp(player.HP);
            DrawExp(player.Exp, player.TargetExp);
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
            RenderSystem.FillRect(startX - 9, 2, totalWidth + 7, AlphabetS.GetPattern('0').Length);
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

        private static void DrawWallHp(int wallHP)
        {
            if (prevHP == wallHP) return;

            // 0~100 범위 클램프
            if (wallHP < 0) wallHP = 0;
            if (wallHP > 100) wallHP = 100;

            double percent = wallHP / 100.0;
            
            int barWidth = uiWidth - 4;
            int filled = (int)(barWidth * percent);

            Console.BackgroundColor = ConsoleColor.Black;
            RenderSystem.FillRect(uiStartX + 2, uiHeight - 8, barWidth, 4);

            ConsoleColor hpColor;
            if (percent >= 0.7) hpColor = ConsoleColor.Green;
            else if (percent >= 0.3) hpColor = ConsoleColor.Yellow;
            else hpColor = ConsoleColor.Red;

            Console.BackgroundColor = hpColor;
            RenderSystem.FillRect(uiStartX + 2, uiHeight - 8, filled, 4);

            prevHP = wallHP;
            Console.ResetColor();
        }

        private static void DrawExp(int exp, int targetExp)
        {
            if (prevExp == exp) return;

            // 0~100 범위 클램프
            if (exp > targetExp) exp = targetExp;

            double percent = (double)exp / targetExp;
            
            int barWidth = uiWidth - 4;
            int filled = (int)(barWidth * percent);

            Console.BackgroundColor = ConsoleColor.Black;
            RenderSystem.FillRect(uiStartX + 2, uiHeight - 3, barWidth, 2);


            Console.BackgroundColor = ConsoleColor.Blue;
            RenderSystem.FillRect(uiStartX + 2, uiHeight - 3, filled, 2);

            prevExp = exp;
            Console.ResetColor();
        }
    }
}
