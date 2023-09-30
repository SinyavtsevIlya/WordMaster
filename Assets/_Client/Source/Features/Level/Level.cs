using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Level : IDisposable
    {
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        public HashSet<Vector2Int> UsedPositions { get; }
        public CompositeDisposable Disposables { get; }


        public Level(LevelSettings settings, IReactiveCollection<Letter> letters,
            CompositeDisposable disposables, HashSet<Vector2Int> usedPositions)
        {
            Settings = settings;
            Letters = letters;
            Disposables = disposables;
            UsedPositions = usedPositions;
        }

        public void Dispose()
        {
            Disposables?.Dispose();
        }
    }
}