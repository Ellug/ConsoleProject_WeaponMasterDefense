namespace WeaponMasterDefense
{
    class Bat : Monster
    {
        public Bat(int spawnX, int gameLevel)
        {
            Name = "Bat";
            HP = 30 + (gameLevel);
            Atk = 1;
            Speed = 3;
            ExpValue = 20;
            ScoreVal = 20;
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
