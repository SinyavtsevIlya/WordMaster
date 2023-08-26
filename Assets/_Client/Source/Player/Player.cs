using UniRx;

namespace WordMaster
{
    public class Player
    {
        public Sequence Sequence { get; }
        public IReactiveCollection<Word> CompletedWords { get; }

        public Player(Sequence sequence, IReactiveCollection<Word> completedWords)
        {
            Sequence = sequence;
            CompletedWords = completedWords;
        }
    }
}