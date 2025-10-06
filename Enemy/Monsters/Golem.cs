namespace WeaponMasterDefense
{
    class Golem : Monster
    {
        public Golem(int spawnX, int gameLevel)
        {
            Name = "Golem";
            HP = 20 + gameLevel;
            Atk = 1;
            Range = 2;
            Speed = 2;
            ExpValue = 40;
            X = spawnX;
            Y = 0;
            Width = Frames[0][0].Length;
            Height = Frames[0].Length;
        }

        protected override string[][] Frames => new string[][]
        {
            new string[]
            {
                "      █████      ",
                " ██   █████   ██ ",
                " ██ █ █ █ █ █ ██ ",
                " ███████████████ ",
                " ███████████████ ",
                "   ███████████   ",
                "    ███   ███    ",
                "    ███   ███    ",
                "   ████   ████   "
            },
            new string[]
            {
                "                 ",
                "      █████      ",
                "      █████      ",
                " ██ █ █ █ █ █ ██ ",
                " ███████████████ ",
                " ███████████████ ",
                "   ███████████   ",
                "    ███   ███    ",
                "   ████   ████   "
            },
            new string[]
            {
                "      █████      ",
                "      █████      ",
                " ██ █ █ █ █ █ ██ ",
                " ███████████████ ",
                "█████████████████",
                "██  ██████████ ██",
                "    ███   ███    ",
                "    ███   ███    ",
                "   ████   ████   "
            },
        };
    }
}