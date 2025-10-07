using System;
using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class MonsterSpawner
    {
        private readonly MonsterFactory _factory;
        private readonly List<Monster> _activeMonsters;
        private readonly Random random = new Random();

        public List<Monster> ActiveMonsters { get { return _activeMonsters; } }

        private int _spawnLevel = 0;
        private double _spawnTimer = 0;
        private double _spawnDelay = 3;
        private int _spawnCount = 2;

        private int _lastBossLevelSpawned = -1;

        public MonsterSpawner(MonsterFactory factory)
        {
            _factory = factory;
            _activeMonsters = new List<Monster>();
        }

        public void Update(double deltaTime, Player player, int gameLevel)
        {
            if (_spawnLevel != gameLevel)
            {
                _spawnDelay = 3.0 - (gameLevel * 0.05);
                _spawnCount = 2 + (gameLevel / 10);
                _spawnLevel = gameLevel;
                
                TrySpawnBoss(gameLevel);
            }
            _spawnTimer += deltaTime;

            if (_spawnTimer >= _spawnDelay)
            {
                _spawnTimer = 0;
                for (int i = 0; i< _spawnCount; i++) Spawn(gameLevel);
            }

            UpdateMonsters(deltaTime, player);
        }

        private void Spawn(int gameLevel)
        {
            Monster monster = _factory.CreateRandom(0, gameLevel);
            int maxX = FieldRender.GameWidth - monster.Width - 5;
            int spawnX = random.Next(5, maxX);
            monster.X = spawnX;

            _activeMonsters.Add(monster);
        }

        private void UpdateMonsters(double deltaTime, Player player)
        {
            for (int i = _activeMonsters.Count - 1; i >= 0; i--)
            {
                Monster mon = _activeMonsters[i];

                if (mon._isDead)
                {
                    player.Exp += mon.ExpValue;
                    _activeMonsters.RemoveAt(i);
                    continue;
                }

                mon._moveTimer += deltaTime;
                if (mon._moveTimer >= .3)
                {
                    if (mon._isAttable) mon.Attack(player);
                    else mon.Move();

                    mon.UpdateAnimation();
                    mon._moveTimer = 0;
                }
            }
        }

        private void TrySpawnBoss(int gameLevel)
        {
            if (gameLevel > 0 && gameLevel % 10 == 0 && _lastBossLevelSpawned != gameLevel)
            {
                SpawnBoss(gameLevel);
                _lastBossLevelSpawned = gameLevel;
            }
        }

        private void SpawnBoss(int gameLevel)
        {
            Monster boss = _factory.CreateBoss(gameLevel);
            if (boss == null) return;

            int centerX = FieldRender.GameWidth / 2;
            boss.X = centerX - (boss.Width / 2);
            if (boss.X < 0) boss.X = 0;
            boss.Y = 0;

            _activeMonsters.Add(boss);
        }

        public void ClearAll() => _activeMonsters.Clear();
    }
}
