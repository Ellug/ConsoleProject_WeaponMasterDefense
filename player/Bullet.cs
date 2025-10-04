using System;

namespace WeaponMasterDefense
{
    public class Bullet
    {
        public bool IsActive { get; private set; }

        private double _x, _y;
        private int _prevX = -1, _prevY = -1;

        // 고정된 진행 방향(발사 시 계산)
        private double _dirX, _dirY;

        private Monster _target;
        private int _damage;

        private double _speed;

        public Bullet()
        {
            // 풀에서 재사용
        }

        public void Init(int startX, int startY, Monster target, int damage, double speed = 60.0)
        {
            IsActive = true;

            _x = startX;
            _y = startY;
            _prevX = -1;
            _prevY = -1;

            _target = target;
            _damage = damage;
            _speed = speed;

            // 발사 순간 타겟 중심을 조준하여 "방향"만 고정(호밍 X)
            double tx = target?.CenterX ?? startX;
            double ty = target?.CenterY ?? startY - 1;

            double dx = tx - startX;
            double dy = ty - startY;
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len < 1e-6) { _dirX = 0; _dirY = -1; }
            else { _dirX = dx / len; _dirY = dy / len; }
        }

        public void Update(double dt)
        {
            if (!IsActive) return;

            // 화면 밖으로 나가면 제거
            if (_x < 0 || _y < 0 || _x > FieldRender.GameWidth || _y > FieldRender.GameHeight)
            {
                Deactivate();
                return;
            }

            // 이동
            _x += _dirX * _speed * dt;
            _y += _dirY * _speed * dt;

            // 명중
            if (_target != null && !_target._isDead)
            {
                double dx = _target.CenterX - _x;
                double dy = _target.CenterY - _y;
                if ((dx * dx + dy * dy) <= 3)
                {
                    _target.GetDamaged(_damage);
                    Deactivate();
                    return;
                }
            }

            Draw();
        }

        private void Draw()
        {
            int x = (int)Math.Round(_x);
            int y = (int)Math.Round(_y);

            if (x == _prevX && y == _prevY) return;

            if (_prevX != -1 && _prevY != -1) RenderSystem.FillRect(_prevX, _prevY, 1, 1);

            Console.BackgroundColor = ConsoleColor.Yellow;
            RenderSystem.FillRect(x, y, 1, 1);
            Console.ResetColor();

            _prevX = x;
            _prevY = y;
        }

        private void Deactivate()
        {
            if (_prevX != -1 && _prevY != -1)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                RenderSystem.FillRect(_prevX, _prevY, 1, 1);
                Console.ResetColor();
            }
            IsActive = false;
            _target = null;
        }

        public void Reset()
        {
            IsActive = false;
            _target = null;
            _prevX = _prevY = -1;
        }
    }
}
