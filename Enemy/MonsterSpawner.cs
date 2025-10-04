using System;
using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class MonsterSpawner
    {
        private readonly MonsterFactory _factory;
        private readonly List<Monster> _activeMonsters;
        private readonly Random random = new Random();

        private double _spawnTimer = 3;
        private double _spawnDelay = 5;

        public MonsterSpawner(MonsterFactory factory)
        {
            _factory = factory;
            _activeMonsters = new List<Monster>();
        }

        public void Update(double deltaTime)
        {
            _spawnTimer += deltaTime;

            if (_spawnTimer >= _spawnDelay)
            {
                _spawnTimer = 0;
                Spawn();
            }

            UpdateMonsters(deltaTime);
        }

        private void Spawn()
        {
            int spawnX = random.Next(5, FieldRender.GameWidth - 15);
            Monster monster = _factory.CreateRandom(spawnX);
            _activeMonsters.Add(monster);
        }

        private void UpdateMonsters(double deltaTime)
        {
            for (int i = _activeMonsters.Count - 1; i >= 0; i--)
            {
                Monster m = _activeMonsters[i];

                if (m.IsDead)
                {
                    _activeMonsters.RemoveAt(i);
                    continue;
                }

                m._moveTimer += deltaTime;
                if (m._moveTimer >= 1.0)
                {
                    m.Move();
                    m.UpdateAnimation();
                    m.Draw();
                    m._moveTimer = 0;
                }
            }
        }
        public void ClearAll()
        {
            _activeMonsters.Clear();
        }
    }
}
