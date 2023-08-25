using UniRx;

namespace WordMaster
{
    public class Player
    {
        public Sequence Sequence { get; }
        public IReadOnlyReactiveCollection<Word> CompletedWords { get; }

        public Player(Sequence sequence, IReadOnlyReactiveCollection<Word> completedWords)
        {
            Sequence = sequence;
            CompletedWords = completedWords;
        }
    }
}