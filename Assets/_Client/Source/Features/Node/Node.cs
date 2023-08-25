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
        
        public Node(Letter letter, Node next, NodeSettings settings)
        {
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));
            Settings = settings;
            Next = new ReactiveProperty<Node>(next);
        }
    }
}