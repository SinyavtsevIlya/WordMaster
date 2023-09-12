using System.Linq;
using Rules;
using UniRx;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace WordMaster
{
    public class LevelGenerationRule : IRule, IInitializable
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
                .ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + _level.Settings.LevelHalfWidth)
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

                var verticalOffset = 2;
                var levelHeight = _level.Settings.Height;
                var startPosition = (int)(-levelHeight / 2f) + verticalOffset;
                var length = levelHeight - verticalOffset * 2;
                var letterPlacements = Enumerable.Range(startPosition, length).ToList().Shuffle().Take(shuffledVariants.Count).ToList();

                for (var index = 0; index < shuffledVariants.Count; index++)
                {
                    var variant = shuffledVariants[index];
                    var randomizationRange = _level.Settings.GenerationOffsetRandomization;
                    var randomization = Random.Next(-randomizationRange, randomizationRange);
                    var position = new Vector2(horizontalPosition + randomization, letterPlacements[index]);
                    var letter = _letterFactory.Create(variant, position);
                    
                    _level.Letters.Add(letter);
                    letter.AddTo(_level.Disposables);

                    var culling = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (_player.DistancePassed - letter.Position.Value.x >
                            _level.Settings.LevelHalfWidth)
                        {
                            _level.Letters.Remove(letter);
                            letter.Culled.OnNext(Unit.Default);
                            letter.Dispose();
                        }
                    });

                    letter.IsPicked.Where(isTrue => isTrue).Subscribe(_ => culling.Dispose()).AddTo(letter.Disposables);
                    culling.AddTo(_level.Disposables);
                }
            }
        }
    }
}