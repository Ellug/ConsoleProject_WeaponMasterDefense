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

        public Monster Create(MonsterTags tag, int spawnX, int gameLevel)
        {
            switch (tag)
            {
                case MonsterTags.Slime:         return new Slime(spawnX, gameLevel);
                case MonsterTags.Bat:           return new Bat(spawnX, gameLevel);
                case MonsterTags.Golem:         return new Golem(spawnX / 4 * 3, gameLevel);

                case MonsterTags.KingSlime:     return new KingSlime(spawnX, gameLevel);
                case MonsterTags.KingBat:       return new KingBat(spawnX, gameLevel);
                case MonsterTags.KingGolem:     return new KingGolem(spawnX, gameLevel);
                
                default: return new Slime(spawnX, gameLevel);
            }
        }

        public Monster CreateRandom(int spawnX, int gameLevel)
        {
            // 10레벨마다 새로운 몬스터 해금
            int unlockCount = Math.Min((gameLevel / 10) + 1, (int)MonsterTags.Count);
            MonsterTags tag = (MonsterTags)random.Next(0, unlockCount);
            return Create(tag, spawnX, gameLevel);
        }

        // 보스 생성
        public Monster CreateBoss(int gameLevel)
        {
            if (gameLevel <= 0 || gameLevel % 10 != 0) return null;

            int tier = gameLevel / 10;
            int idx = (tier - 1) % 3;
            MonsterTags bossTag;

            switch (idx)
            {
                case 0: bossTag = MonsterTags.KingSlime;    break;
                case 1: bossTag = MonsterTags.KingBat;      break;
                default: bossTag = MonsterTags.KingGolem;   break;
            }

            Monster boss = Create(bossTag, 0, gameLevel);
            return boss;
        }
    }
}
