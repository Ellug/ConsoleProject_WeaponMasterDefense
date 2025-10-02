using System;

namespace WeaponMasterDefense
{
    public static class PlayerArt
    {
        public static readonly string[] Sprite = new string[]
        {
            "  []  ",
            " /||\\ ",
            "  ||  ",
            " /  \\ ",
            "/    \\"
        };
    }

    class Player
    {
        public Vector2 Position { get; private set; }

        public void Attack()
        {
            Console.WriteLine("Basic Attack!");
        }
    }
}
