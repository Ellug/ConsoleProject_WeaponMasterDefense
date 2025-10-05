namespace WeaponMasterDefense
{
    class Slime : Monster
    {
        public Slime(int spawnX)
        {
            Name = "Slime";
            HP = 10;
            Atk = 1;
            Speed = 1;
            X = spawnX;
            Y = 0;
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