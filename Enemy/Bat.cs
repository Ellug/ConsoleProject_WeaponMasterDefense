namespace WeaponMasterDefense
{
    class Bat : Monster
    {
        public Bat(int spawnX)
        {
            Name = "Slime";
            HP = 10;
            Atk = 1;
            Speed = 2;
            X = spawnX;
            Y = 0;
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
