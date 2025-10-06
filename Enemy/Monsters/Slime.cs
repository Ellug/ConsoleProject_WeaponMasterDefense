namespace WeaponMasterDefense
{
    class Slime : Monster
    {
        public Slime(int spawnX, int gameLevel)
        {
            Name = "Slime";
            HP = 10 + (gameLevel / 2);
            Atk = 1;
            Speed = 1;
            ExpValue = 10;
            X = spawnX;
            Y = 0;
            Width = Frames[0][0].Length;
            Height = Frames[0].Length;
        }

        protected override string[][] Frames => new string[][]
        {
            new string[]
            {
                "  ████  ",
                " ██████ ",
                "████████",
                "████████",
                " ██████ "
            },
            new string[]
            {
                "        ",
                "  ████  ",
                " ██████ ",
                "████████",
                " ██████ "
            },
            new string[]
            {
                "  ████  ",
                " ██████ ",
                "████████",
                " ██████ ",
                "  ████  "
            }
        };

    }
}