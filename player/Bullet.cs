using System;

namespace WeaponMasterDefense
{
    public class Bullet
    {
        public bool IsActive { get; private set; }
        private double _x, _y;

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

            _target = target;
            _damage = damage;
            _speed = speed;

            // 발사 순간 타겟 중심을 조준하여 방향만 고정
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

            int x = (int)Math.Round(_x);
            int y = (int)Math.Round(_y);

            // 플레이 구역 검사, UI 침범/벽 아래 진입 차단
            if (x < FieldRender.PlayLeft || x > FieldRender.PlayRight || y < FieldRender.PlayTop || y > FieldRender.PlayBottom)
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
        }

        public void Draw()
        {
            int x = (int)Math.Round(_x);
            int y = (int)Math.Round(_y);

            if (x < FieldRender.PlayLeft || x > FieldRender.PlayRight || y < FieldRender.PlayTop || y > FieldRender.PlayBottom) return;
            RenderSystem.FillRectChar(x, y, 1, 1, '█', ConsoleColor.Yellow);
        }

        private void Deactivate()
        {
            IsActive = false;
            _target = null;
        }

        public void Reset()
        {
            IsActive = false;
            _target = null;
        }
    }
}
