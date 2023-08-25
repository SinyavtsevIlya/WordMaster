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
        
        public CompositeDisposable Disposables { get; }
        
        public Node(Letter letter, Node next)
        {
            Letter = letter ?? throw new ArgumentNullException(nameof(letter));
            Next = new ReactiveProperty<Node>(next);
        }
    }
}