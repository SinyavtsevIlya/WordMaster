using System;
using UniRx;

namespace WordMaster
{
    public class Level : IDisposable
    {
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        
        public CompositeDisposable Disposables { get; }
        

        public Level(LevelSettings settings, IReactiveCollection<Letter> letters,
            CompositeDisposable disposables)
        {
            Settings = settings;
            Letters = letters;
            Disposables = disposables;
        }

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}