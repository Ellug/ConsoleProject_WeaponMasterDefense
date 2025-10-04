using System;

namespace WeaponMasterDefense
{
    public class MonsterFactory
    {
        private Random random;

        public MonsterFactory()
        {
            random = new Random();
        }

        public Monster Create(MonsterTags tag, int spawnX)
        {
            switch (tag)
            {
                case MonsterTags.Slime:
                    return new Slime(spawnX);
                case MonsterTags.Bat:
                    return new Bat(spawnX);
                default:
                    return new Slime(spawnX);
            }
        }

        public Monster CreateRandom(int spawnX)
        {
            MonsterTags tag = (MonsterTags)random.Next(0, (int)MonsterTags.Count);
            return Create(tag, spawnX);
        }
    }
}
