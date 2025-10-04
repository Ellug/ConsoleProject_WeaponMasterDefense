using System;

namespace WeaponMasterDefense
{
    public enum MonsterTags
    {
        Slime, Bat, Count
    }

    public abstract class Monster
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Speed { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool IsDead => HP <= 0;

        protected virtual string[][] Frames => new string[][]
        {
            new string[]
            {
                " ???? ",
                "??????",
                " ???? ",
                "  ??  ",
                "  ??  "
            }
        };

        public int _currentFrame = 0;
        public double _moveTimer = 0;


        public virtual void Move()
        {
            int maxY = FieldRender.GameHeight - FieldRender.wallHeight - Frames[0].Length;
            int newY = Y + Speed;
            if (newY > maxY) Y = maxY;
            else Y = newY;
        }                

        public virtual void Attack()
        {
            // 성벽이 사거리 내라면 공격
        }

        public void GetDamaged(int dmg)
        {
            // 대미지 받는 로직 (미완
            HP -= dmg;
            if (HP < 0) HP = 0;
        }

        public void UpdateAnimation()
        {
            _currentFrame++;
            if (_currentFrame >= Frames.Length)
            {
                _currentFrame = 0;
            }
        }

        private int _prevX = -1;
        private int _prevY = -1;

        public void Draw() // Player의 Draw랑 매우 유사한데... 인터페이스로 빼? 그냥 둬?
        {
            if (_prevX != -1 && _prevY != -1)
            {
                RenderSystem.FillRect(_prevX, _prevY, Frames[0][0].Length, Frames[0].Length);
            }

            RenderSystem.DrawPattern(Frames[_currentFrame], X, Y, ConsoleColor.Red);

            Console.ResetColor();

            _prevX = X;
            _prevY = Y;
        }
    }
}