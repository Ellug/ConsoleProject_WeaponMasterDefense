using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class BulletPool
    {
        private readonly Queue<Bullet> _pool = new Queue<Bullet>();
        private readonly int _maxSize;

        public BulletPool(int initialSize = 128, int maxSize = 512)
        {
            _maxSize = maxSize;
            for (int i = 0; i < initialSize; i++)
                _pool.Enqueue(new Bullet());
        }

        public Bullet Get()
        {
            return _pool.Count > 0 ? _pool.Dequeue() : new Bullet();
        }

        public void Return(Bullet b)
        {
            b.Reset();
            if (_pool.Count < _maxSize) _pool.Enqueue(b);            
        }
    }
}