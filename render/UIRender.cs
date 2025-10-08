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

            // UI 배경 최초 클리어
            RenderSystem.FillRectChar(uiStartX, 0, uiWidth, uiHeight, ' ', ConsoleColor.Black);
        }

        public static void Update(Player player, int score = 0)
        {
            const int skillStartY = 12;
            const int skillLineSpace = 8;

            RenderSystem.TextRender("SCORE", uiStartX + 6, 2, "S", ConsoleColor.Gray, ConsoleColor.Black);
            RenderSystem.TextRender(" Q", uiStartX + 2, skillStartY, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" W", uiStartX + 2, skillStartY + skillLineSpace, "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" E", uiStartX + 2, skillStartY + (skillLineSpace * 2), "S", ConsoleColor.Cyan, ConsoleColor.Black);
            RenderSystem.TextRender(" R", uiStartX + 2, skillStartY + (skillLineSpace * 3), "S", ConsoleColor.Cyan, ConsoleColor.Black);

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
            string scoreText = score.ToString("D7");

            int charWidth = AlphabetS.GetPattern('0')[0].Length;
            int totalWidth = (charWidth + 2) * scoreText.Length;
            int startX = uiStartX + uiWidth - totalWidth;

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
            const int statStartY = 44;
            const int statLineSpace = 8;

            RenderSystem.TextRender($"ATK : {player.Atk}", uiStartX + 8, statStartY, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"ATK DELAY : {player.AtkDelay:0.00}", uiStartX + 8, statStartY + statLineSpace, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"SPEED : {player.Speed}", uiStartX + 8, statStartY + (statLineSpace * 2), "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender($"RANGE : {player.Range}", uiStartX + 8, statStartY + (statLineSpace * 3), "S", ConsoleColor.White, ConsoleColor.Black);
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
