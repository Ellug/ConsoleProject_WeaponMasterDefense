using System;

namespace WeaponMasterDefense
{


    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int HP { get; private set; } // 성 체력
        public int Atk { get; private set; }

        public Player()
        {
            HP = 100;
            Atk = 10;
            X = 100;
            Y = 60;
        }

        public void Attack()
        {
            Console.WriteLine("Basic Attack!");
            // 오토 어택으로 가자
        }

        public void MoveUp()
        {
            if (Y > 0) Y--;
        }

        public void MoveDown()
        {
            if (Y + PlayerArt.Height < GameRender.GameHeight - 6) Y++;
        }

        public void MoveLeft()
        {
            if (X > 0) X--;
        }

        public void MoveRight()
        {
            if (X + PlayerArt.Width < GameRender.GameWidth - 1) X++;
        }
    }

    public static class PlayerArt
    {
        public static readonly string[] Sprite = new string[]
        {
            "  []  ",
            " /||\\ ",
            "  ||  ",
            " /  \\ ",
            "/    \\"
        };

        public static int Width => Sprite[0].Length; // 6
        public static int Height => Sprite.Length; // 5
    }
}
