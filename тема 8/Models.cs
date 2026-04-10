using System;
using System.Collections.Generic;

namespace AnimalGuessGame
{
    [Serializable]
    public class AnimalRecord
    {
        public string Name { get; set; }
        public List<int> Traits { get; set; } = new List<int>();
    }

    [Serializable]
    public class GameResult
    {
        public string PlayerLogin { get; set; }
        public DateTime GameDate { get; set; }
        public bool Guessed { get; set; }
        public string AnimalName { get; set; }
        public int QuestionsAsked { get; set; }
    }

    [Serializable]
    public class GameSettings
    {
        public int SessionTimeMinutes { get; set; } = 5;
        public ColorTheme Theme { get; set; } = ColorTheme.Light;
        public string CharacterName { get; set; } = "Default";
    }

    public enum ColorTheme
    {
        Light,
        Dark,
        Blue
    }
}