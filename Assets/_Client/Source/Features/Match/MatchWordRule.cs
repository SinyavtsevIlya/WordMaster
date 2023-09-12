using System;
using Rules;
using UniRx;
using Zenject;

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
                .Where(addEvent => addEvent.Index > 1)
                .Throttle(TimeSpan.FromSeconds(.35f))
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
        }
    }
}