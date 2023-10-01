using System;
using System.Collections.Generic;
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
        private static readonly Random Random = new Random();

        private readonly Level _level;
        private readonly LetterFactory _letterFactory;
        private readonly Player _player;
        private readonly Trie _trie;
        private readonly Alphabet _alphabet;

        private int _TokenlettersAmount;

        public LevelGenerationRule(Level level, LetterFactory letterFactory, Player player, Trie trie, Alphabet alphabet)
        {
            _level = level;
            _letterFactory = letterFactory;
            _player = player;
            _trie = trie;
            _alphabet = alphabet;
        }

        public void Initialize()
        {
            _player
                .ObserveEveryValueChanged(player => Mathf.RoundToInt(player.DistancePassed) + _level.Settings.LevelHalfWidth)
                .Where(distancePassed => distancePassed % 5 == 0)
                .Subscribe(GenerateLetters)
                .AddTo(_level.Disposables);
        }

        private void GenerateLetters(int horizontalPosition)
        {
            if (_trie.Search(_player.Sequence.ToString(), out var variants, out var _))
            {
                var shuffledVariants = GetShuffledVariants(variants);
                var letterPlacements = GetLetterPlacements(shuffledVariants);

                for (var index = 0; index < shuffledVariants.Count; index++)
                {
                    var character = shuffledVariants[index];
                    var position = GetPosition(horizontalPosition, letterPlacements, index);
                    position = _level.EnsurePosition(Vector2Int.RoundToInt(position));
                    var letter = _letterFactory.Create(character, position);
                    _level.Letters.Add(letter);
                    var usedPosition = Vector2Int.RoundToInt(letter.Position.Value);
                    _level.UsedPositions.Add(usedPosition);
                    
                    Disposable.Create(() =>
                    {
                        _level.Letters.Remove(letter);
                        _level.UsedPositions.Remove(usedPosition);
                    }).AddTo(letter.Disposables);
                }
            }
        }

        private Vector2 GetPosition(int horizontalPosition, List<int> letterPlacements, int index)
        {
            var randomizationRange = _level.Settings.GenerationOffsetRandomization;
            var randomization = Random.Next(-randomizationRange, randomizationRange);
            horizontalPosition += randomization;
            
            var position = new Vector2(horizontalPosition, letterPlacements[index]);
            return position;
        }

        private List<char> GetShuffledVariants(List<char> variants)
        {
            var range = _level.Settings.RightCharactersPerStep;
            var takeRange = Random.Next(range.x, range.y);

            var shuffledVariants = variants
                .Select((variant, id) => variants[(_TokenlettersAmount + id) % variants.Count])
                .Take(takeRange).ToList();

            _TokenlettersAmount += takeRange;

            var wrongCharactersCount = _player.DistancePassed < 50 ? 0 : range.y - takeRange;

            shuffledVariants.AddRange(_alphabet.Values.Shuffle().Take(wrongCharactersCount));

            shuffledVariants.Shuffle();
            return shuffledVariants;
        }

        private List<int> GetLetterPlacements(List<char> shuffledVariants)
        {
            var verticalOffset = 2;
            var levelHeight = _level.Settings.Height;
            var startPosition = (int)(-levelHeight / 2f) + verticalOffset;
            var length = levelHeight - verticalOffset * 2;
            var letterPlacements = Enumerable.Range(startPosition, length).ToList().Shuffle().Take(shuffledVariants.Count)
                .ToList();
            return letterPlacements;
        }
    }
}