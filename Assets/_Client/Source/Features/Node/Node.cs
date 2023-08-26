using System;
using UniRx;

namespace WordMaster
{
    public class Node
    {
        public NodeSettings Settings { get; }
        
        public Letter Letter { get; }
        
        public ReactiveProperty<Node> Next { get; }
        
        public ReactiveProperty<Node> Prev { get; }

        public CompositeDisposable Disposables => Letter.Disposables;
        
        public Node(Letter letter, NodeSettings settings)
        {
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));
            Settings = settings;
            Prev = new ReactiveProperty<Node>();
            Next = new ReactiveProperty<Node>();
        }
    }
}