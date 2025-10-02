using System;
using System.Diagnostics;

namespace WeaponMasterDefense
{
    class Program
    {
        static Stopwatch watch = new Stopwatch();

        static void Main(string[] args)
        {
            Console.Title = "Weapon Master Defense";
            ConsoleWindowManager.LockConsoleSize(320, 85);
            ConsoleFontManager.SetFontSize(4, 8, "");
            Console.CursorVisible = false;

            // bgm 재생
            Bgm.PlayBattleTheme();

            // 인트로 화면 출력
            var intro = new Intro();
            intro.Start();

            Start();
            while (true)
            {
                Update(); // 무한 반복으로 게임 루프 실행
            }
        }

        static void Start()
        {
            watch.Start();
            GameRender.Init();
            UIRender.Init();
            // 플레이어, 몬스터, 성벽체력, 스킬, 점수 등도 다 초기화
        }

        //// 반복 실행 함수 (Unity의 Update와 유사)
        static void Update()
        {
            if (watch.ElapsedMilliseconds >= 50)
            {
                watch.Restart();

                if (!InputSystem.IsPaused)
                {
                    // 게임 영역 렌더링
                    GameRender.DrawField();
                    UIRender.Update();
                }

                InputSystem.HandleInput();

                if (InputSystem.ShouldExit) Environment.Exit(0);
                if (InputSystem.ShouldRestart)
                {
                    InputSystem.ShouldRestart = false;
                    watch.Reset();
                    Console.Clear();
                    Start(); // 재시작
                }

                // 일시정지 중 나머지 업데이트 로직 실행X
                if (InputSystem.IsPaused) return;
            }
        }

    }
}