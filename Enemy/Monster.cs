using System;

namespace WeaponMasterDefense
{
    public enum MonsterTags
    {
        Slime, Bat, Golem, Count, KingSlime = 101, KingBat, KingGolem
    }

    public abstract class Monster
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Speed { get; set; }
        public int Range { get; set; } = 0;
        public int ExpValue { get; set; } = 100;

        public int X { get; set; }
        public int Y { get; set; }

        public bool _isDead = false;
        public bool _isAttable => (Y + Height >= FieldRender.GameHeight - FieldRender.wallHeight - Range);

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

        public int Width { get; set; }
        public int Height { get; set; }

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

        public virtual void Attack(Player player)
        {
            if (player == null || _isDead) return;
            player.HP -= Atk;
            if (player.HP < 0) player.HP = 0;
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