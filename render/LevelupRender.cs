using System;
using System.Collections.Generic;

namespace WeaponMasterDefense
{
    public class LevelUpRender
    {
        private Player _player;
        private List<(string label, Action apply)> _options = new List<(string, Action)> ();
        private Random _rnd = new Random ();

        public LevelUpRender(Player player)
        {
            _player = player;
            BuildOptions();
        }

        private void BuildOptions()
        {
            var optionList = new List<(string, Action)>();

            // 스킬 레벨업
            if (_player.skills?.Length >= 4)
            {
                if (_player.skills[0].Level < 9) optionList.Add(($"Q: {_player.skills[0].Name}  LEVEL Up", () => _player.skills[0]?.LevelUp()));
                if (_player.skills[1].Level < 9) optionList.Add(($"W: {_player.skills[1].Name}  LEVEL Up", () => _player.skills[1]?.LevelUp()));
                if (_player.skills[2].Level < 9) optionList.Add(($"E: {_player.skills[2].Name}  LEVEL Up", () => _player.skills[2]?.LevelUp()));
                if (_player.skills[3].Level < 9) optionList.Add(($"R: {_player.skills[3].Name}  LEVEL Up", () => _player.skills[3]?.LevelUp()));
            }

            // 능력치
            if (_player.Atk < 99) optionList.Add(("Increase Atk", () => _player.IncreaseAtk()));
            if (_player.Speed < 3) optionList.Add(("Increase Speed", () => _player.IncreaseSpeed()));
            if (_player.AtkDelay > 0.1) optionList.Add(("Reduce Atk Delay", () => _player.ReduceAtkDelay()));
            if (_player.Range < 160) optionList.Add(("Increase Atk Range", () => _player.IncreaseRange()));

            // HP 회복
            if (_player.HP < 190) optionList.Add(("Repair Castle", () => _player.Heal()));

            // 셔플 후 최대 4개 선택
            Shuffle(optionList);
            int take = Math.Min(4, optionList.Count);
            for (int i = 0; i < take; i++) _options.Add(optionList[i]);
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rnd.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public void DrawLevelUpMenu()
        {
            RenderSystem.TextRender("Level UP", 120, 1, "M", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.TextRender($"LEVEL: {Program.gameLevel}", 250, 1, "S", ConsoleColor.Yellow, ConsoleColor.Black);
            RenderSystem.TextRender("CHOOSE YOUR UPGRADE", 102, 13, "S", ConsoleColor.White, ConsoleColor.Black);
            RenderSystem.TextRender("PRESS NUMBER", 120, 20, "S", ConsoleColor.White, ConsoleColor.Black);

            int y = 32;
            for (int i = 0; i < _options.Count; i++)
            {
                string line = $"{i + 1} : {_options[i].label}";
                RenderSystem.TextRender(line, 8, y, "S", ConsoleColor.White, ConsoleColor.Black);
                y += 10;
            }
        }

        public void HandleInput()
        {
            int idx = -1;

            if (InputSystem.IsKeyPressed(ConsoleKey.D1) || InputSystem.IsKeyPressed(ConsoleKey.NumPad1)) idx = 0;
            else if (InputSystem.IsKeyPressed(ConsoleKey.D2) || InputSystem.IsKeyPressed(ConsoleKey.NumPad2)) idx = 1;
            else if (InputSystem.IsKeyPressed(ConsoleKey.D3) || InputSystem.IsKeyPressed(ConsoleKey.NumPad3)) idx = 2;
            else if (InputSystem.IsKeyPressed(ConsoleKey.D4) || InputSystem.IsKeyPressed(ConsoleKey.NumPad4)) idx = 3;

            if (idx >= 0 && idx < _options.Count)
            {
                _options[idx].apply?.Invoke();
                Program.ResumeGame();
            }
        }
    }
}
