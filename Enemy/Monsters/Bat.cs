namespace WeaponMasterDefense
{
    class Bat : Monster
    {
        public Bat(int spawnX, int gameLevel)
        {
            Name = "Bat";
            HP = 10 + (gameLevel / 2);
            Atk = 1;
            Speed = 3;
            ExpValue = 20;
            X = spawnX;
            Y = 0;
            Width = Frames[0][0].Length;
            Height = Frames[0].Length;
        }

        protected override string[][] Frames => new string[][]
        {
            new string[]
            {
                " ██      ██ ",
                "████    ████",
                " ██████████ ",
                "  ████████  ",
                "    ████    ",
                "   ██████   "
            },
            new string[]
            {
                "            ",
                " ███    ███ ",
                "████████████",
                "█ ████████ █",
                "    ████    ",
                "   ██████   "
            },
            new string[]
            {
                "██         █",
                "████    ████",
                " ██████████ ",
                "  ████████  ",
                "    ████    ",
                "   ██████   "
            },
        };
    }
}
