using System;

namespace WeaponMasterDefense
{
    public abstract class Skill
    {
        public string Name { get; protected set; }
        public int Level { get; protected set; } = 1;
        public int Damage { get; protected set; }
        public double Cooldown { get; protected set; } = 1;
        public double CurrentCooldown { get; protected set; } = 0;
        public bool IsReady => CurrentCooldown <= 0;
        protected double sDt { get; private set; }

        public abstract void Activate(Player player);
        public abstract void LevelUp();

        public void UpdateCooldown(double deltaTime)
        {
            sDt = deltaTime;

            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= deltaTime;
                if (CurrentCooldown < 0) CurrentCooldown = 0;
            }
        }
    }

    public class QSkill : Skill
    {
        private int _bulletCount;

        public QSkill()
        {
            Name = "Nova";
            Damage = 1;
            Cooldown = 4;
            _bulletCount = 16;
        }

        public override void Activate(Player player)
        {
            if (CurrentCooldown > 0) return;
            CurrentCooldown = Cooldown;

            int originX = player.X + 4;
            int originY = player.Y + 3;

            // 사방으로 방사형 발사
            double angleStep = 2 * Math.PI / _bulletCount;
            double speed = 70.0;

            for (int i = 0; i < _bulletCount; i++)
            {
                double angle = i * angleStep;
                double dirX = Math.Cos(angle);
                double dirY = Math.Sin(angle);

                var bullet = player.ActiveBulletsPool.Get();
                bullet.InitFree(originX, originY, dirX, dirY, Damage, speed);
                player.RegisterBullet(bullet);
            }
        }

        public override void LevelUp()
        {
            Level++;
            Damage++;
            Cooldown -= 0.3;
            _bulletCount += 16;
        }
    }

    public class WSkill : Skill
    {
        private int _spreadWidth = 12;

        public WSkill()
        {
            Name = "Cresent Slash";
            Damage = 2;
            Cooldown = 7;
        }

        public override void Activate(Player player)
        {
            if (CurrentCooldown > 0) return;
            CurrentCooldown = Cooldown;

            int half = _spreadWidth;
            int baseY = player.Y - 1;
            double speed = 120.0;
            double R = half + 4;
            int originY = player.Y + 2;

            for (int x = -_spreadWidth; x <= _spreadWidth; x++)
            {
                int yOffset = (int)(Math.Pow(x / 6.0, 2));
                int bulletX = player.X + x;
                int bulletY = originY + yOffset;

                var bullet = player.ActiveBulletsPool.Get();
                bullet.InitFree(bulletX, bulletY, 0, -1, Damage, speed);
                player.RegisterBullet(bullet);
            }
        }

        public override void LevelUp()
        {
            if (Level >= 9) return;
            Level++;
            Damage += 2;
            _spreadWidth += 1;
            Cooldown -= 0.5;
        }
    }

    public class ESkill : Skill
    {
        public ESkill()
        {
            Name = "Archers Fire";
            Damage = 1;
            Cooldown = 14;
        }

        public override void Activate(Player player)
        {
            if (CurrentCooldown > 0) return;
            CurrentCooldown = Cooldown;

            int startX = FieldRender.PlayLeft + 2;
            int endX = FieldRender.PlayRight - 2;
            int step = 6;
            double speed = 80.0;
            int startY = FieldRender.WallTop - 1;

            for (int i = 0; i < 2; i++)
            {
                for (int x = startX + i + 1; x <= endX; x += step)
                {
                    var bullet = player.ActiveBulletsPool.Get();

                    bullet.InitFree(x, startY - (i * 4), 0, -1, Damage, speed);
                    player.RegisterBullet(bullet);
                }
            }
        }

        public override void LevelUp()
        {
            Level++;
            Damage++;
            Cooldown -= 1;
        }
    }

    public class RSkill : Skill
    {
        double _duration = 4.0;

        public RSkill()
        {
            Name = "Asura Mode";
            Cooldown = 30;
            Damage = 0;
        }

        public override void Activate(Player player)
        {            
            if (CurrentCooldown > 0) return;
            CurrentCooldown = Cooldown;

            player.StartAsura(_duration);
        }

        public override void LevelUp()
        {
            if (Level >= 9) return;
            Level++;
            _duration += 1;
            Cooldown -= 1;
        }
    }
}