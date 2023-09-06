using System;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Node : IDisposable
    {
        public NodeSettings Settings { get; }
        
        public Letter Letter { get; }
        
        public ReactiveProperty<Node> Next { get; }
        
        public ReactiveProperty<Node> Prev { get; }

        public CompositeDisposable Disposables => Letter.Disposables;
        
        public Subject<bool> IsMatched { get; }

        public Node(Letter letter, NodeSettings settings)
        {
            Letter = letter;
            Settings = settings;
            Prev = new ReactiveProperty<Node>();
            Next = new ReactiveProperty<Node>();
            IsMatched = new Subject<bool>();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}