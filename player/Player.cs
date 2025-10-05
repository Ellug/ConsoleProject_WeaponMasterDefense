using System;
using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int HP { get; set; }
        public int Atk { get; private set; }
        public double AtkDelay { get; private set; }
        private double _attackTimer = 0;
        public int Speed { get; private set; }
        public int Range { get; private set; }
        public int Exp { get; set; }
        public int TargetExp { get; set; }

        // 풀 & 활성 리스트
        private readonly BulletPool _bulletPool = new BulletPool(128, 512);
        private readonly List<Bullet> _activeBullets = new List<Bullet>();
        public IReadOnlyList<Bullet> ActiveBullets => _activeBullets;

        public Skill[] skills;

        public Player()
        {
            HP = 100;
            Atk = 2;
            AtkDelay = 0.1;
            Speed = 1;
            Range = 40;
            X = 100;
            Y = 65;
            Exp = 0;
            TargetExp = 100;

            skills = new Skill[4]
            {
                new QSkill(),
                new WSkill(),
                new ESkill(),
                new RSkill(),
            };
        }

        public void Update(List<Monster> monsters, double deltaTime)
        {
            // 자동 공격 타이머
            _attackTimer += deltaTime;

            // 타겟 탐색 및 발사
            Monster target = FindNearestTarget(monsters);
            if (target != null && _attackTimer >= AtkDelay)
            {
                Attack(target);
                _attackTimer = 0;
            }

            // 탄환 업데이트(비활성은 반환)
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                var b = _activeBullets[i];
                b.Update(deltaTime);
                if (!b.IsActive)
                {
                    _activeBullets.RemoveAt(i);
                    _bulletPool.Return(b);
                }
            }
        }

        private Monster FindNearestTarget(List<Monster> monsters)
        {
            Monster nearest = null;
            double minDist = double.MaxValue;

            foreach (var m in monsters)
            {
                if (m._isDead) continue;
                double dx = m.X - X;
                double dy = m.Y - Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist < Range && dist < minDist)
                {
                    nearest = m;
                    minDist = dist;
                }
            }
            return nearest;
        }

        public void Attack(Monster target)
        {
            var bullet = _bulletPool.Get();
            bullet.Init(X, Y, target, Atk, 60.0); // 60칸/초 기본
            _activeBullets.Add(bullet);
        }

        public void UpdateSkills(double deltaTime)
        {
            foreach (var skill in skills) skill?.UpdateCooldown(deltaTime);
        }

        private string[] Sprite = new string[]
        {
            "   ██   ",
            " ██████ ",
            "█  ██  █",
            "  █  █  ",
            " █    █ ",
            " █    █ "
        };

        private int Width => Sprite[0].Length;
        private int Height => Sprite.Length;

        public void Draw()
        {
            RenderSystem.DrawPattern(Sprite, X, Y, ConsoleColor.Green);
        }

        public void MoveUp() { int ny = Y - Speed; Y = (ny < 0 ? 0 : ny); }
        public void MoveLeft() { int nx = X - Speed; X = (nx < 0 ? 0 : nx); }
        public void MoveDown()
        {
            int maxY = FieldRender.GameHeight - FieldRender.wallHeight - Height;
            int ny = Y + Speed; Y = (ny > maxY ? maxY : ny);
        }
        public void MoveRight()
        {
            int maxX = FieldRender.GameWidth - Width - 1;
            int nx = X + Speed; X = (nx > maxX ? maxX : nx);
        }
    }
}
