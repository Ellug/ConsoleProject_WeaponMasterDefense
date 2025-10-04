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
        static MonsterSpawner spawner;
        static public int score = 0;
        static public int gameLevel = 1;

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
            Bgm.Stop();
            watch.Reset();
            watch.Start();
            FieldRender.Init();
            UIRender.Init();
            score = 0;
            gameLevel = 1;
            player = new Player();
            spawner = new MonsterSpawner(new MonsterFactory());
            gameState = GameState.Playing;
        }

        static void Update()
        {
            double ms = watch.ElapsedMilliseconds;
            if (ms >= 20) // 50ms 갱신 => 약 20FPS?
            {
                watch.Restart();

                switch (gameState)
                {
                    case GameState.Playing:
                        UpdatePlaying(ms / 1000.0);
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
            Bgm.PlayBattleTheme();            

            // 인트로 화면 출력
            var intro = new IntroRender();
            intro.Start();
        }

        static void UpdatePlaying(double deltatime)
        {
            InputSystem.HandleInput(player);

            // Game Over
            if (player.HP <= 0)
            {
                Console.Clear();
                gameState = GameState.GameOver;
                return;
            }

            // Level Up
            if (player.Exp >= player.TargetExp)
            {
                Console.Clear();
                player.Exp = 0;
                gameState = GameState.LevelUp;
                return;
            }
            
            spawner.Update(deltatime, player);
            player.Update(spawner.ActiveMonsters, deltatime);

            player.Draw();
            UIRender.Update(player);
            player.UpdateSkills(deltatime);
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