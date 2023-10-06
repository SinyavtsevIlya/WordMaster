using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Level : IDisposable
    {
        private readonly ZoomSettings _zoomSettings;
        public LevelSettings Settings { get; }
        public IReactiveCollection<Letter> Letters { get; }
        public HashSet<Vector2Int> UsedPositions { get; }
        public CompositeDisposable Disposables { get; }

        public int Height => (int) (Settings.Height / _zoomSettings.Value);

        public float HalfHeight => Height / 2f;

        public int LevelHalfWidth => (int) ((float) Screen.width / Screen.height * Height / 2f + Settings.GenerationOffset);


        public Level(LevelSettings settings, ZoomSettings zoomSettings, IReactiveCollection<Letter> letters,
            CompositeDisposable disposables, HashSet<Vector2Int> usedPositions)
        {
            _zoomSettings = zoomSettings;
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