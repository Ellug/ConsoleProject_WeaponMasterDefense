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
        public BulletPool ActiveBulletsPool => _bulletPool;
        public void RegisterBullet(Bullet bullet) => _activeBullets.Add(bullet);

        public Skill[] skills;
        
        public Player()
        {
            HP = 200;
            Atk = 4;
            AtkDelay = 0.6;
            Speed = 1;
            Range = 60;
            X = 100;
            Y = 65;
            Exp = 0;
            TargetExp = 40;

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

            if (_isAsuraMode)
            {
                if (target != null && _attackTimer >= 0.05)
                {
                    Attack(target);
                    Attack(target);
                    _attackTimer = 0;
                }
            }
            else
            {
                if (target != null && _attackTimer >= AtkDelay)
                {
                    Attack(target);
                    _attackTimer = 0;
                }
            }

            // 탄환 업데이트(비활성은 반환)
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                var bullet = _activeBullets[i];
                bullet.Update(deltaTime, monsters);
                if (!bullet.IsActive)
                {
                    _activeBullets.RemoveAt(i);
                    _bulletPool.Return(bullet);
                }
            }
        }

        private Monster FindNearestTarget(List<Monster> monsters)
        {
            Monster nearest = null;
            double minDist = double.MaxValue;

            foreach (var mon in monsters)
            {
                if (mon._isDead) continue;
                double dx = mon.X - X;
                double dy = mon.Y - Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (_isAsuraMode)
                {
                    if (dist < 240 && dist < minDist)
                    {
                        nearest = mon;
                        minDist = dist;
                    }
                }
                else
                {
                    if (dist < Range && dist < minDist)
                    {
                        nearest = mon;
                        minDist = dist;
                    }
                }
            }
            return nearest;
        }

        public void Attack(Monster target)
        {
            var bullet = _bulletPool.Get();
            bullet.Init(X, Y, target, Atk, 120.0);
            _activeBullets.Add(bullet);
        }

        public void UpdateSkills(double deltaTime)
        {
            if (_isAsuraMode)
            {
                _asuraRemain -= deltaTime;
                if (_asuraRemain <= 0) EndAsura();

                // q w e 스킬 쿨타임 감소
                for (int i = 0; i < 3; i++) skills[i]?.UpdateCooldown(100000);
            }
            else foreach (var skill in skills) skill?.UpdateCooldown(deltaTime);
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

        private string[] AsuraSprite = new string[]
        {
            "  ▒██▒  ",
            "▒██████▒",
            "█  ██  █",
            " ▒█  █▒ ",
            " █ ▒ ▒█▒",
            "▒█▒▒▒▒█▒"
        };

        private int Width => Sprite[0].Length;
        private int Height => Sprite.Length;

        public void Draw()
        {
            if (_isAsuraMode) RenderSystem.DrawPattern(AsuraSprite, X, Y, ConsoleColor.Red);
            else RenderSystem.DrawPattern(Sprite, X, Y, ConsoleColor.Green);
        }

        // Move 4dir
        public void MoveUp()
        {
            int ny = Y - Speed;
            Y = (ny < 0 ? 0 : ny);
        }

        public void MoveLeft()
        {
            int nx = X - Speed;
            X = (nx < 0 ? 0 : nx);
        }

        public void MoveDown()
        {
            int maxY = FieldRender.GameHeight - FieldRender.wallHeight - Height;
            int ny = Y + Speed;
            Y = (ny > maxY ? maxY : ny);
        }

        public void MoveRight()
        {
            int maxX = FieldRender.GameWidth - Width - 1;
            int nx = X + Speed;
            X = (nx > maxX ? maxX : nx);
        }


        // level up options
        public void IncreaseAtk() => Atk += 2;
        public void ReduceAtkDelay() => AtkDelay -= 0.1;
        public void IncreaseSpeed() => Speed += 1;
        public void IncreaseRange() => Range += 20;
        public void Heal() => HP += 40;

        // R스킬 아수라 모드 관련
        private bool _isAsuraMode = false;
        private double _asuraRemain = 0.0;

        public void StartAsura(double duration)
        {
            if (_isAsuraMode) return;
            _isAsuraMode = true;
            _asuraRemain = duration;
        }

        public void EndAsura()
        {
            _isAsuraMode = false;
            _asuraRemain = 0.0;
        }
    }
}
