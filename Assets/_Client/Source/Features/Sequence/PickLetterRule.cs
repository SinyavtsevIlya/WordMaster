using System.Linq;
using UniRx;

namespace WordMaster
{
    public class PickLetterRule
    {
        public PickLetterRule(Sequence sequence, Level level)
        {
            var head = sequence.Head.Value.Letter;
            
            head.Position.Subscribe(position =>
            {
                if (TryGetCollision(level, head, out var collisionLetter))
                {
                    var prevHead = sequence.Value.Last();
                    var newHead = new Node(collisionLetter, prevHead);
                    prevHead.Prev.Value = newHead;
                    
                    sequence.Value.Add(newHead);
                    level.Letters.Remove(collisionLetter);
                }
            }).AddTo(sequence.Disposables);
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