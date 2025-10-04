using System;
using System.Diagnostics;

namespace WeaponMasterDefense
{
    enum GameState { Playing, Paused, LevelUp, GameOver }

    class Program
    {
        static GameState gameState;
        static Stopwatch watch = new Stopwatch();
        static Player player;

        static public bool isPaused = false;

        static void Main(string[] args)
        {
            GetIntro();
            Start();
            while (true)
            {
                Update();
            }
        }

        public static void Start()
        {
            Console.Clear();
            watch.Reset();
            watch.Start();
            FieldRender.Init();
            UIRender.Init();
            // 플레이어, 몬스터, 성벽체력, 스킬, 점수 등도 다 초기화
            player = new Player();
            gameState = GameState.Playing;
        }

        static void Update()
        {
            if (watch.ElapsedMilliseconds >= 20) // 20ms 갱신 => 약 50FPS?
            {
                watch.Restart();

                switch (gameState)
                {
                    case GameState.Playing:
                        UpdatePlaying();
                        break;
                    case GameState.Paused:
                        UpdatePaused();
                        break;
                    case GameState.LevelUp:
                        UpdateLevelUp();
                        break;
                    case GameState.GameOver:
                        UpdateGameOver();
                        break;
                }
            }
        }

        static void GetIntro()
        {
            Console.Title = "Weapon Master Defense";
            ConsoleWindowManager.LockConsoleSize(320, 85);
            ConsoleFontManager.SetFontSize(4, 8, "");
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // bgm 재생
            //Bgm.PlayBattleTheme();

            // 인트로 화면 출력
            var intro = new IntroRender();
            intro.Start();
        }

        static void UpdatePlaying()
        {
            PlayerRender.DrawPlayer(player);
            UIRender.Update();
            InputSystem.HandleInput(player);
        }

        public static void SetPaused()
        {
            isPaused = true;
            gameState = GameState.Paused;
            Console.Clear();
        }

        static void UpdatePaused()
        {
            PausedRender pausedRender = new PausedRender();
            pausedRender.DrawPauseMenu();
            pausedRender.HandleInput();
        }
        static public void ResumeGame()
        {
            isPaused = false;
            Console.Clear();
            FieldRender.Init();
            UIRender.Init();
            gameState = GameState.Playing;
        }

        static public void ExitGame()
        {
            Environment.Exit(0);
        }

        static void UpdateLevelUp()
        {
            LevelUpRender lvUp = new LevelUpRender();
            lvUp.DrawLevelUpMenu();
            lvUp.HandleInput();
        }

        static void UpdateGameOver()
        {
            GameOverRender gameOver = new GameOverRender();
            gameOver.DrawPauseMenu();
            gameOver.HandleInput();
        }
    }
}