using System;
using Rules;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace WordMaster
{
    public class CompleteLettersRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;

        public CompleteLettersRule(Sequence sequence)
        {
            _sequence = sequence;
        }
        
        public void Initialize()
        {
            _sequence.Completed.Subscribe(sequence =>
            {
                foreach (var node in sequence)
                {
                    node.Letter.IsMatched.OnNext(true);
                    node.Letter.Dispose();
                }
            }).AddTo(_sequence.Disposables);
            
            _sequence.Failed.Subscribe(sequence =>
            {
                foreach (var node in sequence)
                {
                    node.Letter.IsMatched.OnNext(false);
                    node.Letter.Dispose();
                }
            }).AddTo(_sequence.Disposables);
        }
    }
}