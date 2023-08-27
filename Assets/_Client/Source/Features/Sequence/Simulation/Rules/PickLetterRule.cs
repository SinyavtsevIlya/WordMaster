using System.Linq;
using UniRx;
using Zenject;

namespace WordMaster
{
    public class PickLetterRule : IRule, IInitializable
    {
        private readonly Sequence _sequence;
        private readonly Level _level;
        private readonly NodeFactory _nodeFactory;

        public PickLetterRule(Sequence sequence, Level level, NodeFactory nodeFactory)
        {
            _sequence = sequence;
            _level = level;
            _nodeFactory = nodeFactory;
        }

        public void Initialize()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                var head = _sequence.Head.Value.Letter;

                if (TryGetCollision(_level, head, out var collisionLetter))
                {
                    var prevHead = _sequence.Value.Last();
                    var newHead = _nodeFactory.Create(collisionLetter);
                    
                    newHead.Next.Value = prevHead;
                    prevHead.Prev.Value = newHead;

                    _sequence.Value.Add(newHead);
                    _level.Letters.Remove(collisionLetter);
                }
            }).AddTo(_sequence.Disposables);
        }

        private static bool TryGetCollision(Level level, Letter head, out Letter collisionLetter)
        {
            foreach (var levelLetter in level.Letters)
            {
                if (IsColliding(levelLetter, head))
                {
                    collisionLetter = levelLetter;
                    return true;
                }
            }

            collisionLetter = null;
            return false;
        }

        private static bool IsColliding(Letter a, Letter b) => 
            (a.Position.Value - b.Position.Value).magnitude < a.Radius + b.Radius;
    }
}