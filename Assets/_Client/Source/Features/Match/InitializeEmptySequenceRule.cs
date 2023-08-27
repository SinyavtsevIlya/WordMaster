using UniRx;
using Zenject;

namespace WordMaster
{
    public class InitializeEmptySequenceRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;
        private readonly Alphabet _alphabet;
        private readonly LetterFactory _letterFactory;
        private readonly NodeFactory _nodeFactory;

        public InitializeEmptySequenceRule(Sequence sequence, Alphabet alphabet, LetterFactory letterFactory, NodeFactory nodeFactory)
        {
            _sequence = sequence;
            _alphabet = alphabet;
            _letterFactory = letterFactory;
            _nodeFactory = nodeFactory;
        }
        
        public void Initialize()
        {
            _sequence.Value.ObserveCountChanged()
                .Where(count => count == 0)
                .Subscribe(_ =>
                {
                    var letter = _letterFactory.Create(_alphabet.RandomValue, );
                    _sequence.Value.Add(_nodeFactory.Create(letter));
                }).AddTo(_sequence.Disposables);
        }
    }
}