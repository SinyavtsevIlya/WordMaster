using System;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Letter : IDisposable
    {
        public char Value { get; }
        public ReactiveProperty<Vector2> Position { get; }
        public float Radius { get; }
        
        public float Width { get; set; }
        
        public ReactiveProperty<bool> IsPicked { get; }
        
        public Subject<bool> IsMatched { get; }
        public Subject<Unit> Culled { get; }
        
        public CompositeDisposable Disposables { get; }
        
        public Letter(char value, Vector2 position, 
            float radius, CompositeDisposable disposables)
        {
            Value = value;
            Position = new ReactiveProperty<Vector2>(position);
            IsPicked = new ReactiveProperty<bool>();
            IsMatched = new Subject<bool>();
            Culled = new Subject<Unit>();
            Radius = radius;
            Disposables = disposables;
        }

        public void Dispose()
        {
            Position?.Dispose();
            Disposables?.Dispose();
        }
    }
}