using System;

namespace WeaponMasterDefense
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int HP { get; set; }
        public int Atk { get; private set; }
        public int AtkSpeed { get; private set; }
        public int AtkCount { get; private set; }
        public int Speed { get; private set; }
        public int Range { get; private set; }
        public int Exp { get; set; }
        public int TargetExp { get; set; }

        public Skill[] skills;

        public Player()
        {
            HP = 100;
            Atk = 10;
            AtkSpeed = 1;
            AtkCount = 1;
            Speed = 1;
            Range = 10;
            X = 100;
            Y = 65;
            Exp = 0;
            TargetExp = 100;

            skills = new Skill[4]
            {
                new QSkill(),
                new WSkill(),
                new ESkill(),
                new RSkill(),
            };
        }

        public void Attack()
        {
            Console.WriteLine("Basic Attack!");
            // 오토 어택으로 가자
        }

        public void MoveUp()
        {
            int newY = Y - Speed;
            if (newY < 0) Y = 0;
            else Y = newY;
        }

        public void MoveDown()
        {
            int maxY = FieldRender.GameHeight - FieldRender.wallHeight - Height;
            int newY = Y + Speed;
            if (newY > maxY) Y = maxY;
            else Y = newY;
        }

        public void MoveLeft()
        {
            int newX = X - Speed;
            if (newX < 0) X = 0;
            else X = newX;
        }

        public void MoveRight()
        {
            int maxX = FieldRender.GameWidth - Width - 1;
            int newX = X + Speed;
            if (newX > maxX) X = maxX;
            else X = newX;
        }

        public void UpdateSkills(double deltaTime)
        {
            foreach (var skill in skills)
            {
                skill?.UpdateCooldown(deltaTime);
            }
        }


        private string[] Sprite = new string[]
        {
            "   ██   ",
            " ██████ ",
            "█  ██  █",
            "  █  █  ",
            " █    █ ",
            " █    █ "
        };

        private int Width => Sprite[0].Length;
        private int Height => Sprite.Length;

        private int _prevX = -1;
        private int _prevY = -1;

        public void Draw()
        {
            // prevX,Y 초기값(-1) 아닐시 이전 위치 지우기 실행
            if (_prevX != -1 && _prevY != -1)
            {
                RenderSystem.FillRect(_prevX, _prevY, Width, Height);
            }

            // 새 위치 그리기
            RenderSystem.DrawPattern(Sprite, X, Y, ConsoleColor.Green, ConsoleColor.Black);

            Console.ResetColor();

            // 현재 위치를 다음 프레임을 위한 이전 좌표로 저장
            _prevX = X;
            _prevY = Y;
        }
    }
}
