using Rules;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class SpawnTutorialWordRule : IRule
    {
        private readonly LetterFactory _letterFactory;
        private readonly Level _level;
        private readonly Alphabet _alphabet;
        private readonly Player _player;

        public SpawnTutorialWordRule(LetterFactory letterFactory, Level level, Alphabet alphabet, Player player)
        {
            _letterFactory = letterFactory;
            _level = level;
            _alphabet = alphabet;
            _player = player;
        }
        
        public void Initialize()
        {
            
            
            var hintLabel = Object.Instantiate(_level.Settings.ConnectionHintLabelPrefab);

            for (var characterIndex = 1; characterIndex < _alphabet.InitialWord.Length; characterIndex++)
            {
                var character = _alphabet.InitialWord[characterIndex];
                var position = new Vector2(characterIndex * 3 + 3, 0f);
                var letter = _letterFactory.Create(character, position);
                _level.Letters.Add(letter);

                var arrow = Object.Instantiate(_level.Settings.ConnectionHintArrowPrefab);
                arrow.transform.position = position + Vector2.left;
                letter.IsPicked.Where(isTrue => isTrue).Subscribe(_ => Object.Destroy(arrow)).AddTo(_level.Disposables);

                if (characterIndex == _alphabet.InitialWord.Length / 2)
                    hintLabel.transform.position += Vector3.right * characterIndex;
            }
        }
    }
}