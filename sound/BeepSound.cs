using System;

namespace WeaponMasterDefense
{
    public static class BeepSound
    {
        // 옥타브 2
        public const int C2 = 65;
        public const int D2 = 73;
        public const int E2 = 82;
        public const int F2 = 87;
        public const int G2 = 98;
        public const int A2 = 110;
        public const int B2 = 123;

        // 옥타브 3
        public const int C3 = 131;
        public const int D3 = 147;
        public const int E3 = 165;
        public const int F3 = 175;
        public const int G3 = 196;
        public const int A3 = 220;
        public const int B3 = 247;

        // 옥타브 4
        public const int C4 = 262;
        public const int D4 = 294;
        public const int E4 = 330;
        public const int F4 = 349;
        public const int G4 = 392;
        public const int A4 = 440;
        public const int B4 = 494;

        // 옥타브 5
        public const int C5 = 523;
        public const int D5 = 587;
        public const int E5 = 659;
        public const int F5 = 698;
        public const int G5 = 784;
        public const int A5 = 880;
        public const int B5 = 988;

        // 옥타브 6
        public const int C6 = 1047;
        public const int D6 = 1175;
        public const int E6 = 1319;
        public const int F6 = 1397;
        public const int G6 = 1568;
        public const int A6 = 1760;
        public const int B6 = 1976;
        public static void Play(int frequency, int durationMs)
        {
            Console.Beep(frequency, durationMs);
        }
        public static void PlayMelody((int freq, int dur)[] notes)
        {
            foreach (var note in notes)
            {
                Console.Beep(note.freq, note.dur);
            }
        }
    }
}