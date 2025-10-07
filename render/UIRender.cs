using System;

namespace WeaponMasterDefense
{
    static class UIRender
    {
        private static int uiStartX;
        private static int uiWidth;
        private static int uiHeight;

        public static void Init(int totalWidth = 320, int totalHeight = 85)
        {
            uiStartX = totalWidth * 2 / 3;
            uiWidth = totalWidth - uiStartX;
            uiHeight = totalHeight;

            // UI 배경
            RenderSystem.FillRectChar(uiStartX, 0, uiWidth, uiHeight, ' ', ConsoleColor.Black);

            // 고정 라벨
            RenderSystem.TextRender("SCORE", uiStartX + 6, 2, "S", ConsoleColor.Gray, ConsoleColor.Black);
            RenderSystem.TextRender(" Q", uiStartX + 2, 12, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" W", uiStartX + 2, 20, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" E", uiStartX + 2, 28, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" R", uiStartX + 2, 36, "S", ConsoleColor.Cyan, ConsoleColor.Black);

            // 초기 표시
            DrawScore(-1);
            DrawWallHp(200);
            DrawExp(200, 100000);
        }

        public static void Update(Player player, int score = 0)
        {
            RenderSystem.TextRender("SCORE", uiStartX + 6, 2, "S", ConsoleColor.Gray, ConsoleColor.Black);
            RenderSystem.TextRender(" Q", uiStartX + 2, 12, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" W", uiStartX + 2, 20, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" E", uiStartX + 2, 28, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" R", uiStartX + 2, 36, "S", ConsoleColor.Cyan, ConsoleColor.Black);

            DrawScore(score);

            int barStartY = 12;
            for (int i = 0; i < player.skills.Length; i++)
            {
                Skill sk = player.skills[i];
                if (sk == null) continue;

                double ratio = (sk.Cooldown > 0) ? (sk.CurrentCooldown / sk.Cooldown) : 0.0;
                string key = ((char)('Q' + i)).ToString();
                DrawSkill(key, sk.Level, ratio, barStartY + i * 8);
            }

            DrawWallHp(player.HP);
            DrawStat(player);
            DrawExp(player.Exp, player.TargetExp);
        }

        // 점수
        private static void DrawScore(int score)
        {
            string scoreText = score.ToString("D6");

            int charWidth = AlphabetS.GetPattern('0')[0].Length;
            int totalWidth = (charWidth + 2) * scoreText.Length;
            int startX = uiStartX + uiWidth - totalWidth;

            // 영역 지우고 다시 그림
            RenderSystem.FillRectChar(startX - 7, 2, totalWidth + 7, AlphabetS.GetPattern('0').Length, ' ', ConsoleColor.Black);
            RenderSystem.TextRender(scoreText, startX, 2, "S", ConsoleColor.White, ConsoleColor.Black);
        }

        // 스킬 쿨타임 바
        private static void DrawSkill(string key, int level, double cooldown, int y)
        {
            RenderSystem.TextRender(level.ToString(), uiStartX + 30, y, "S", ConsoleColor.White, ConsoleColor.Black);

            int barWidth = uiWidth - 50;
            int filled = (int)(barWidth * (1 - cooldown));

            // 바 배경색
            RenderSystem.FillRectChar(uiStartX + 45, y + 2, barWidth, 1, ' ', ConsoleColor.Black);

            // 쿨다운 중=Yellow, 완료=DarkCyan
            var barColor = (cooldown > 0) ? ConsoleColor.Yellow : ConsoleColor.DarkCyan;
            RenderSystem.FillRectChar(uiStartX + 45, y + 2, Math.Max(0, filled), 1, '█', barColor);
        }

        // Player Stat
        private static void DrawStat(Player player)
        {
            int statStartY = 44;
            RenderSystem.TextRender($"ATK : {player.Atk}", uiStartX + 8, statStartY, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"ATK DELAY : {player.AtkDelay}", uiStartX + 8, statStartY + 8, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"SPEED : {player.Speed}", uiStartX + 8, statStartY + 16, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"RANGE : {player.Range}", uiStartX + 8, statStartY + 24, "S", ConsoleColor.White, ConsoleColor.Black);
        }

        // HP바
        private static void DrawWallHp(int wallHP)
        {
            double percent = wallHP / 200.0;

            int barWidth = uiWidth - 4;
            int filled = (int)(barWidth * percent);

            var hpColor = percent >= 0.7 ? ConsoleColor.Green : (percent >= 0.3 ? ConsoleColor.Yellow : ConsoleColor.Red);

            RenderSystem.FillRectChar(uiStartX + 2, uiHeight - 8, barWidth, 4, '█', ConsoleColor.Black);
            RenderSystem.FillRectChar(uiStartX + 2, uiHeight - 8, Math.Max(0, filled), 4, '█', hpColor);
        }

        // EXP 바
        private static void DrawExp(int exp, int targetExp)
        {
            if (exp > targetExp) exp = targetExp;

            double percent = (targetExp > 0) ? ((double)exp / targetExp) : 0.0;

            int barWidth = uiWidth - 4;
            int filled = (int)(barWidth * percent);

            RenderSystem.FillRectChar(uiStartX + 2, uiHeight - 3, barWidth, 2, '█', ConsoleColor.Black);
            RenderSystem.FillRectChar(uiStartX + 2, uiHeight - 3, Math.Max(0, filled), 2, '█', ConsoleColor.Blue);
        }
    }
}
