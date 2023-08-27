using System;
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
                .Where(distancePassed => distancePassed % 10 == 0)
                .Subscribe(GenerateLetters)
                .AddTo(_player.Disposables);
        }

        private void GenerateLetters(int horizontalPosition)
        {
            if (_trie.Search(_player.Sequence.ToString(), out var variants, out var _))
            {
                foreach (var variant in variants.Take(3))
                {
                    var position = new Vector2(horizontalPosition, Random.Next(-10, 10));
                    var letter = _letterFactory.Create(variant, position);
                    _level.Letters.Add(letter);
                }   
            }
            else
            {
                Debug.Log("gameover");
            }
        }
    }
}