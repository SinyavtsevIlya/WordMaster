using Rules;
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
            _sequence.Value.ObserveRemove()
                .Where(removeEvent => removeEvent.Index == 0)
                .Subscribe(removeEvent =>
                {
                    var letter = _letterFactory.Create(_alphabet.RandomValue, removeEvent.Value.Letter.Position.Value);
                    var node = _nodeFactory.Create(letter);
                    letter.AddTo(node.Disposables);
                    _sequence.Value.Add(node);
                }).AddTo(_sequence.Disposables);

            _sequence.Value.ObserveAdd().Subscribe(addEvent =>
            {
                if (addEvent.Index == 0)
                    addEvent.Value.Letter.IsPicked.Value = true;
                
                addEvent.Value.AddTo(_sequence.Disposables);
            }).AddTo(_sequence.Disposables);
        }
    }
}