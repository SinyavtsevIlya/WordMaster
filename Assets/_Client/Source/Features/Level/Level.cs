using System;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Level : IDisposable
    {
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        public IReactiveCollection<Word> Words { get; }
        
        public CompositeDisposable Disposables { get; }
        

        public Level(LevelSettings settings, IReactiveCollection<Letter> letters, IReactiveCollection<Word> words,
            CompositeDisposable disposables)
        {
            Settings = settings;
            Letters = letters;
            Words = words;
            Disposables = disposables;
        }

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}