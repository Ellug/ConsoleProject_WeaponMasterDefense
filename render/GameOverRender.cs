using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeaponMasterDefense
{
    public class GameOverRender
    {
        private enum Stage
        {
            Loading,
            ShowRanking,
            NameEntry,
            Saving,
            ShowUpdatedRanking,
            RestartMenu
        }

        private Stage _stage = Stage.Loading;

        // 랭킹
        private List<(string Name, int Score)> _top = new List<(string, int)>();
        private bool _isTop10 = false;
        private string _name = "";
        private const int MaxNameLen = 12;

        // 제출 후 내 항목 하이라이트용
        private string _lastName = null;
        private int? _lastScore = null;

        public GameOverRender()
        {
            _ = FetchTop10(); // 진입 즉시 로딩 시작
        }

        // Firestore
        private async Task FetchTop10()
        {
            try
            {
                var list = await FirestorePublicApi.GetTop10Async();

                _top.Clear();
                foreach (var (name, score, _) in list)
                    _top.Add((name, score));

                _isTop10 = _top.Count < 10 || Program.score > _top[_top.Count - 1].Score;
                _stage = Stage.ShowRanking;
            }
            catch (Exception ex)
            {
                _top.Clear();
                _top.Add(($"Err:{ex.GetType().Name}", 0));
                _top.Add(($"Msg:{ex.Message}", 0));
                _isTop10 = false;
                _stage = Stage.ShowRanking;
            }
        }
        // 점수 제출 + 11등 이후 삭제 + 재조회
        private async Task Submit(string name, int score)
        {
            await FirestorePublicApi.AddAsync(name, score);
            await FirestorePublicApi.TrimToTop10Async();

            _lastName = name; _lastScore = score;

            await FetchTop10();
            _stage = Stage.ShowUpdatedRanking;
        }

        // 입력 처리
        public void HandleInput()
        {
            switch (_stage)
            {
                case Stage.Loading: break;

                case Stage.ShowRanking:
                    if (EnterPressed())
                    {
                        if (_isTop10) _stage = Stage.NameEntry;
                        else _stage = Stage.RestartMenu;
                    }
                    break;

                case Stage.NameEntry: HandleNameTyping(); break;

                case Stage.Saving: break;

                case Stage.ShowUpdatedRanking:
                    if (EnterPressed()) _stage = Stage.RestartMenu;
                    break;

                case Stage.RestartMenu:
                    if (InputSystem.IsKeyPressed(ConsoleKey.D1) || InputSystem.IsKeyPressed(ConsoleKey.NumPad1))
                    {
                        Program.Start();
                        return;
                    }
                    if (InputSystem.IsKeyPressed(ConsoleKey.D2) || InputSystem.IsKeyPressed(ConsoleKey.NumPad2))
                    {
                        Program.ExitGame();
                        return;
                    }
                    break;
            }
        }

        private void HandleNameTyping()
        {
            if (InputSystem.IsKeyPressed(ConsoleKey.Backspace) && _name.Length > 0)
            {
                _name = _name.Substring(0, _name.Length - 1);
                return;
            }

            if (InputSystem.IsKeyPressed(ConsoleKey.Enter))
            {
                var name = string.IsNullOrWhiteSpace(_name) ? "PLAYER" : _name;
                _stage = Stage.Saving;
                try
                {
                    Submit(name, Program.score).GetAwaiter().GetResult();
                }
                catch
                {
                    // 실패해도 재시도는 건너뛰고 재시작 메뉴로 보냄
                    _stage = Stage.RestartMenu;
                }
                return;
            }

            // 영문 입력, 최대 길이 제한
            if (_name.Length < MaxNameLen)
            {
                for (ConsoleKey k = ConsoleKey.A; k <= ConsoleKey.Z; k++)
                {
                    if (InputSystem.IsKeyPressed(k))
                    {
                        char c = (char)('A' + (k - ConsoleKey.A));
                        _name += c;
                        break;
                    }
                }
            }
        }

        private bool EnterPressed()
        {
            if (InputSystem.IsKeyPressed(ConsoleKey.Enter) || InputSystem.IsKeyPressed(ConsoleKey.Spacebar)) return true;
            return false;
        }

        public void DrawGameOverMenu()
        {
            switch (_stage)
            {
                case Stage.Loading:            DrawLoading(); break;
                case Stage.ShowRanking:        DrawRankingColumns(); break;
                case Stage.NameEntry:          DrawNameEntry(); break;
                case Stage.Saving:             DrawSaving(); break;
                case Stage.ShowUpdatedRanking: DrawUpdatedRanking(); break;
                case Stage.RestartMenu:        DrawRestartMenu(); break;
            }
        }

        private void DrawLoading()
        {
            RenderSystem.TextRender("GAME OVER", 120, 5, "M", ConsoleColor.Red, ConsoleColor.Black);
            RenderSystem.TextRender($"YOUR SCORE: {Program.score}", 100, 18, "S", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.TextRender("Loading ranking...", 100, 65, "S", ConsoleColor.White, ConsoleColor.Black);
        }


        private void DrawRankingColumns()
        {
            RenderSystem.TextRender("TOP 10 RANKING", 100, 4, "M", ConsoleColor.Red, ConsoleColor.Black);

            int leftX = 10;
            int rightX = 170;
            int y = 20;

            for (int i = 0; i < Math.Min(5, _top.Count); i++)
            {
                var line = $"{i + 1,2}. {_top[i].Name} :  {_top[i].Score}";
                RenderSystem.TextRender(line, leftX, y, "S", ConsoleColor.White, ConsoleColor.Black);
                y += 8;
            }

            y = 18;
            for (int i = 5; i < _top.Count; i++)
            {
                var line = $"{i + 1,2}. {_top[i].Name} :  {_top[i].Score}";
                RenderSystem.TextRender(line, rightX, y, "S", ConsoleColor.White, ConsoleColor.Black);
                y += 8;
            }

            RenderSystem.TextRender("PRESS ENTER OR SPACEBAR TO CONTINUE", 50, 70, "S", ConsoleColor.Gray, ConsoleColor.Black);
        }

        private void DrawNameEntry()
        {
            RenderSystem.TextRender("CONGRATULATIONS! YOU ARE IN TOP 10.", 40, 25, "S", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.TextRender("ENTER YOUR NAME ENG AND PRESS ENTER:", 40, 33, "S", ConsoleColor.White,  ConsoleColor.Black);
            RenderSystem.TextRender($"> {_name}", 40, 40, "S", ConsoleColor.Cyan,   ConsoleColor.Black);
            RenderSystem.TextRender("BACKSPACE to edit. ENTER to submit", 40, 65, "S", ConsoleColor.DarkGray, ConsoleColor.Black);
        }

        private void DrawSaving()
        {
            RenderSystem.TextRender("Saving...", 120, 38, "S", ConsoleColor.White, ConsoleColor.Black);
        }

        private void DrawUpdatedRanking()
        {
            RenderSystem.TextRender("TOP 10 RANKING", 100, 4, "M", ConsoleColor.Red, ConsoleColor.Black);

            int leftX = 10;
            int rightX = 170;
            int y = 20;

            for (int i = 0; i < Math.Min(5, _top.Count); i++)
            {
                bool highlight = (_lastName != null && _lastScore.HasValue && _top[i].Name == _lastName && _top[i].Score == _lastScore.Value);
                var color = highlight ? ConsoleColor.Yellow : ConsoleColor.White;
                var line = $"{i + 1,2}. {_top[i].Name} :  {_top[i].Score}";
                RenderSystem.TextRender(line, leftX, y, "S", color, ConsoleColor.Black);
                y += 8;
            }

            y = 18;
            for (int i = 5; i < _top.Count; i++)
            {
                bool highlight = (_lastName != null && _lastScore.HasValue && _top[i].Name == _lastName && _top[i].Score == _lastScore.Value);
                var color = highlight ? ConsoleColor.Yellow : ConsoleColor.White;
                var line = $"{i + 1,2}. {_top[i].Name} :  {_top[i].Score}";
                RenderSystem.TextRender(line, rightX, y, "S", color, ConsoleColor.Black);
                y += 8;
            }

            RenderSystem.TextRender("PRESS ENTER OR SPACEBAR TO CONTINUE", 50, 70, "S", ConsoleColor.Gray, ConsoleColor.Black);
        }

        private void DrawRestartMenu()
        {
            RenderSystem.TextRender("GAME OVER", 120, 5, "M", ConsoleColor.Red, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 1 TO RESTART", 90, 40, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS 2 TO EXIT", 90, 55, "S", ConsoleColor.White, ConsoleColor.Black);
        }
    }
}
