using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Letter
    {
        public char Value { get; }
        public ReactiveProperty<Vector2> Position { get; }
        public int Radius { get; }
        public CompositeDisposable Disposables { get; }
        
        public Letter(char value, Vector2 position, 
            int radius, CompositeDisposable disposables)
        {
            Value = value;
            Position = new ReactiveProperty<Vector2>(position);
            Radius = radius;
            Disposables = disposables;
        }
    }
}