using System;

namespace WeaponMasterDefense
{
    class Monster
    {
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Speed { get; set; }
                

        public void Attack()
        {
            // 성벽을 향해 돌진
            // 성벽 바로 앞 좌표일 경우 성벽을 공격
        }

        public void GetDamaged(int dmg)
        {
            // 대미지 받는 로직
        }
    }
}
