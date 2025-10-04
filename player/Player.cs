using System;

namespace WeaponMasterDefense
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int HP { get; private set; } // 성 체력
        public int Atk { get; private set; }
        public int AtkSpeed { get; private set; }
        public int AtkCount { get; private set; }
        public int Speed { get; private set; }
        public int Range { get; private set; }

        public Player()
        {

            HP = 100;
            Atk = 10;
            Speed = 1;
            X = 100;
            Y = 65;
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
            if (Y + PlayerRender.Height < FieldRender.GameHeight - FieldRender.wallHeight) Y++;
        }

        public void MoveLeft()
        {
            if (X > 0) X--;
        }

        public void MoveRight()
        {
            if (X + PlayerRender.Width < FieldRender.GameWidth - 1) X++;
        }
    }
}
