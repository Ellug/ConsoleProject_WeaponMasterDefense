using System;

namespace WeaponMasterDefense
{
    static public class PlayerRender
    {
        public static readonly string[] Sprite = new string[]
        {
            "   ██   ",
            " ██████ ",
            "█  ██  █",
            "  █  █  ",
            " █    █ ",
            " █    █ "
        };

        public static int Width => Sprite[0].Length;
        public static int Height => Sprite.Length;

        private static int prevX = -1;
        private static int prevY = -1;

        public static void DrawPlayer(Player player)
        {
            // prevX,Y 초기값(-1) 아닐시 이전 위치 지우기 실행
            if (prevX != -1 && prevY != -1)
            {
                RenderSystem.FillRect(prevX, prevY, Width, Height);
            }

            // 새 위치 그리기
            RenderSystem.DrawPattern(Sprite, player.X, player.Y, ConsoleColor.Green, ConsoleColor.Black);

            Console.ResetColor();

            // 현재 위치를 다음 프레임을 위한 이전 좌표로 저장
            prevX = player.X;
            prevY = player.Y;
        }        
    }
}
