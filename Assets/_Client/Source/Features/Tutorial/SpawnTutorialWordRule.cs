using Rules;
using UnityEngine;

namespace WordMaster
{
    public class SpawnTutorialWordRule : IRule
    {
        private readonly LetterFactory _letterFactory;
        private readonly Level _level;
        private readonly Alphabet _alphabet;

        public SpawnTutorialWordRule(LetterFactory letterFactory, Level level, Alphabet alphabet)
        {
            _letterFactory = letterFactory;
            _level = level;
            _alphabet = alphabet;
        }
        
        public void Initialize()
        {
            for (var characterIndex = 1; characterIndex < _alphabet.InitialWord.Length; characterIndex++)
            {
                var character = _alphabet.InitialWord[characterIndex];
                var letter = _letterFactory.Create(character, new Vector2(characterIndex * 3 + 3, 0f));
                _level.Letters.Add(letter);
            }
        }
    }
}