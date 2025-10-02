using System;
using System.Threading;

namespace WeaponMasterDefense
{
    public static class Bgm
    {
        private static bool playing = false;

        public static void PlayBattleTheme()
        {
            playing = true;

            new Thread(() =>
            {
                while (playing)
                {
                    var notes = new (int freq, int dur)[]
                    {
                        // 1절
                        (BeepSound.E3, 200),
                        (BeepSound.G3, 200),
                        (BeepSound.A3, 200),
                        (BeepSound.E4, 400),

                        (BeepSound.D4, 200),
                        (BeepSound.C4, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.A3, 400),

                        // 2절 (변형)
                        (BeepSound.A3, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.C4, 200),
                        (BeepSound.D4, 400),

                        (BeepSound.C4, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.A3, 200),
                        (BeepSound.G3, 400),

                        // 3절 (고조)
                        (BeepSound.E3, 200),
                        (BeepSound.G3, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.E4, 400),

                        (BeepSound.F4, 200),
                        (BeepSound.E4, 200),
                        (BeepSound.D4, 200),
                        (BeepSound.C4, 400),

                        // 4절 (마무리)
                        (BeepSound.D4, 200),
                        (BeepSound.C4, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.A3, 200),

                        (BeepSound.G3, 200),
                        (BeepSound.A3, 200),
                        (BeepSound.B3, 200),
                        (BeepSound.E4, 600),
                    };

                    foreach (var note in notes)
                    {
                        if (!playing) break;
                        Console.Beep(note.freq, note.dur);
                    }
                }
            }).Start();
        }

        public static void Stop()
        {
            playing = false;
        }
    }
}