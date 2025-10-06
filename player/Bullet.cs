using System;
using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class Bullet
    {
        public bool IsActive { get; private set; }
        private double _x, _y;

        // 탄환 진행 방향(발사 시 계산)
        private double _dirX, _dirY;

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

        public void InitFree(int startX, int startY, double dirX, double dirY, int damage, double speed)
        {
            IsActive = true;
            _x = startX;
            _y = startY;
            _damage = damage;
            _speed = speed;
            _dirX = dirX;
            _dirY = dirY;
        }

        public void Update(double dt, List<Monster> monsters)
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
            Monster hitTarget = null;

            // 모든 활성 몬스터 중 가까운 한 놈만 검사
            double nearest = double.MaxValue;

            foreach (var mon in monsters)
            {
                if (mon._isDead) continue;
                double dx = mon.CenterX - _x;
                double dy = mon.CenterY - _y;
                double distSq = dx * dx + dy * dy;

                if (distSq < 16 && distSq < nearest)
                {
                    nearest = distSq;
                    hitTarget = mon;
                }
            }

            // 명중 시 처리
            if (hitTarget != null)
            {
                hitTarget.GetDamaged(_damage);
                Deactivate();
                return;
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
        }

        public void Reset()
        {
            IsActive = false;
        }
    }
}
