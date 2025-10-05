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
        public int ExpValue { get; set; } = 10;

        public int X { get; set; }
        public int Y { get; set; }

        public bool _isDead = false;

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

        public int Width => Frames[0][0].Length;
        public int Height => Frames[0].Length;

        public int CenterX => X + Width / 2;
        public int CenterY => Y + Height / 2;

        public int _currentFrame = 0;
        public double _moveTimer = 0;


        public virtual void Move()
        {
            int maxY = FieldRender.GameHeight - FieldRender.wallHeight - Height;
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
            HP -= dmg;
            if (HP <= 0)
            {
                HP = 0;
                Dead();
            }
        }

        public void Dead()
        {
            _isDead = true;
            Console.ResetColor();
            Program.score += 10;
        }

        public void UpdateAnimation()
        {
            _currentFrame++;
            if (_currentFrame >= Frames.Length)
            {
                _currentFrame = 0;
            }
        }

        public void Draw()
        {
            if (_isDead) return;
            RenderSystem.DrawPattern(Frames[_currentFrame], X, Y, ConsoleColor.Red);
        }
    }
}