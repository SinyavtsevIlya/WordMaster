using System.Linq;
using UniRx;
using UnityEngine;
using Random = System.Random;

namespace WordMaster
{
    public class LevelGenerationRule : IRule
    {
        private static readonly System.Random Random = new System.Random();

        private readonly Level _level;
        private readonly LetterFactory _letterFactory;
        private readonly Player _player;
        private readonly Trie _trie;
        private readonly Alphabet _alphabet;
        private readonly Random _sessionRelatedRandom;

        private int _TokenlettersAmount;

        public LevelGenerationRule(Level level, LetterFactory letterFactory, Player player, Trie trie, Alphabet alphabet)
        {
            _level = level;
            _letterFactory = letterFactory;
            _player = player;
            _trie = trie;
            _alphabet = alphabet;

            _sessionRelatedRandom = new Random();
        }

        public void Initialize()
        {
            _player
                .ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + 10)
                .Where(distancePassed => distancePassed % 5 == 0)
                .Subscribe(GenerateLetters)
                .AddTo(_level.Disposables);

            _level.Letters.ObserveAdd().Subscribe(addEvent => addEvent.Value.Disposables.AddTo(_level.Disposables));
        }

        private void GenerateLetters(int horizontalPosition)
        {
            if (_trie.Search(_player.Sequence.ToString(), out var variants, out var _))
            {
                var range = _level.Settings.RightCharactersPerStep;
                var takeRange = Random.Next(range.x, range.y);
                
                var shuffledVariants = variants
                    .Select((variant, id) => variants[(_TokenlettersAmount + id) % variants.Count])
                    .Take(takeRange).ToList();

                _TokenlettersAmount += takeRange;

                var wrongCharacters = range.y - takeRange;
                
                shuffledVariants.AddRange(_alphabet.Values.Shuffle().Take(wrongCharacters));
                
                shuffledVariants.Shuffle();

                var letterPlacements = Enumerable.Range(0, _level.Settings.Height).ToList().Shuffle().Take(shuffledVariants.Count).ToList();

                for (var index = 0; index < shuffledVariants.Count; index++)
                {
                    var variant = shuffledVariants[index];
                    var randomizationRange = _level.Settings.GenerationOffsetRandomization;
                    var randomization = Random.Next(-randomizationRange, randomizationRange);
                    var position = new Vector2(horizontalPosition + _level.Settings.GenerationOffset + randomization, letterPlacements[index]);
                    var letter = _letterFactory.Create(variant, position);
                    _level.Letters.Add(letter);
                }
            }
        }
    }
}