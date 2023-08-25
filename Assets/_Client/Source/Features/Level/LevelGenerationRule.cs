using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class LevelGenerationRule : IRule
    {
        private static readonly System.Random Random = new System.Random();
        private const string Chars = "абвгдеёжзиклмнопрстуфхцчшщъыьэюя";
        
        private readonly Level _level;
        private readonly LetterFactory _letterFactory;

        public LevelGenerationRule(Level level, LetterFactory letterFactory)
        {
            _level = level;
            _letterFactory = letterFactory;
        }

        public void Initialize()
        {
            Enumerable.Repeat(Chars, _level.Settings.CharactersCount)
                .Select(s => s[Random.Next(s.Length)]).ToList()
                .ForEach(character =>
                {
                    var letter = _letterFactory.Create(character, new Vector2(Random.Next(0, 20), Random.Next(0, 20)));
                    _level.Letters.Add(letter);               
                });
        }
    }
}