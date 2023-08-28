using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class LevelGenerationRule : IRule, IInitializable
    {
        private static readonly System.Random Random = new System.Random();

        private readonly Level _level;
        private readonly LetterFactory _letterFactory;
        private readonly Player _player;
        private readonly Trie _trie;

        public LevelGenerationRule(Level level, LetterFactory letterFactory, Player player, Trie trie)
        {
            _level = level;
            _letterFactory = letterFactory;
            _player = player;
            _trie = trie;
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
                var shuffledVariants = variants.Shuffle().Take(Random.Next(2,4)).ToList();
                const int levelHeight = 15;
                
                var letterPlacements = Enumerable.Range(0, levelHeight).ToList().Shuffle().Take(shuffledVariants.Count).ToList();

                for (var index = 0; index < shuffledVariants.Count; index++)
                {
                    var variant = shuffledVariants[index];
                    var position = new Vector2(horizontalPosition, letterPlacements[index]);
                    var letter = _letterFactory.Create(variant, position);
                    _level.Letters.Add(letter);
                }
            }
        }
    }
}