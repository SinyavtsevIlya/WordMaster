using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;
using UniRx;

namespace WordMaster
{
    public class MatchWordRule : IRule, IInitializable
    {
        private readonly Alphabet _alphabet;
        private readonly Sequence _sequence;
        private readonly Trie _trie;

        public MatchWordRule(Alphabet alphabet, Sequence sequence, Trie trie)
        {
            _alphabet = alphabet;
            _sequence = sequence;
            _trie = trie;
        }
        
        public void Initialize()
        {
            _sequence.Value.ObserveAdd()
                .Delay(TimeSpan.FromSeconds(.5f))
                .Subscribe(_ =>
                {
                    if (_trie.Search(_sequence.ToString(), out var _, out var isEndOfTheWord))
                    {
                        if (isEndOfTheWord)
                            _sequence.Complete();
                    }
                    else
                    {
                        _sequence.Fail();
                    }
                }).AddTo(_sequence.Disposables);

            _sequence.Value.ObserveRemove()
                .Subscribe(resetEvent => resetEvent.Value.Dispose())
                .AddTo(_sequence.Disposables);
            
            
        }
    }
}