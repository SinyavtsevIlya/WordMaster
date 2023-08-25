using UniRx;

namespace WordMaster
{
    public class Level
    {
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        public IReactiveCollection<Word> Words { get; }
        

        public Level(LevelSettings settings, IReactiveCollection<Letter> letters, IReactiveCollection<Word> words)
        {
            Settings = settings;
            Letters = letters;
            Words = words;
        }
    }
}