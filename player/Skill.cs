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

        public abstract void Activate(Player player);
        public abstract void DamageUp();
        public abstract void CoolDownUp();

        public void UpdateCooldown(double deltaTime)
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= deltaTime;
                if (CurrentCooldown < 0) CurrentCooldown = 0;
            }
        }
    }

    public class QSkill : Skill
    {
        public override void Activate(Player player)
        {
            if (CurrentCooldown <= 0)
            {                
                CurrentCooldown = Cooldown;
            }
        }

        public override void DamageUp()
        {

        }

        public override void CoolDownUp()
        {

        }
    }

    public class WSkill : Skill
    {
        public override void Activate(Player player)
        {
            if (CurrentCooldown <= 0)
            {
                CurrentCooldown = Cooldown;
            }
        }

        public override void DamageUp()
        {
        }

        public override void CoolDownUp()
        {
        }
    }
    public class ESkill : Skill
    {
        public override void Activate(Player player)
        {
            if (CurrentCooldown <= 0)
            {
                CurrentCooldown = Cooldown;
            }
        }

        public override void DamageUp()
        {

        }

        public override void CoolDownUp()
        {

        }
    }

    public class RSkill : Skill
    {
        public override void Activate(Player player)
        {
            if (CurrentCooldown <= 0)
            {
                CurrentCooldown = Cooldown;
            }
        }

        public override void DamageUp()
        {

        }

        public override void CoolDownUp()
        {

        }
    }
}