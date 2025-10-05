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
            Bgm.Stop();
            Renderer.Init(320, 85);

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
                gameState = GameState.GameOver;
                return;
            }

            // Level Up
            if (player.Exp >= player.TargetExp)
            {
                player.Exp = 0;
                gameState = GameState.LevelUp;
                return;
            }

            spawner.Update(deltatime, player);
            player.Update(spawner.ActiveMonsters, deltatime);

            // === Frame ===
            Renderer.BeginFrame(ConsoleColor.Black);
            FieldRender.Draw();

            // 몬스터/탄환/플레이어
            foreach (var m in spawner.ActiveMonsters) m.Draw();
            foreach (var b in player.ActiveBullets) b.Draw();
            player.Draw();
            player.UpdateSkills(deltatime);
            UIRender.Update(player, score);

            Renderer.EndFrame();
            
        }

        public static void SetPaused()
        {
            isPaused = true;
            gameState = GameState.Paused;
        }

        static void UpdatePaused()
        {
            var pausedRender = new PausedRender();
            Renderer.BeginFrame(ConsoleColor.Black);
            pausedRender.DrawPauseMenu();
            Renderer.EndFrame();
            pausedRender.HandleInput();
        }
        static public void ResumeGame()
        {
            isPaused = false;
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
            var lvUp = new LevelUpRender();
            Renderer.BeginFrame(ConsoleColor.Black);
            lvUp.DrawLevelUpMenu();
            Renderer.EndFrame();
            lvUp.HandleInput();
        }

        static void UpdateGameOver()
        {
            var gameOver = new GameOverRender();
            Renderer.BeginFrame(ConsoleColor.Black);
            gameOver.DrawPauseMenu();
            Renderer.EndFrame();
            gameOver.HandleInput();
        }
    }
}