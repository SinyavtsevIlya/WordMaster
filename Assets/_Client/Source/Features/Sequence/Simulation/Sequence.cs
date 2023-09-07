using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Sequence : IDisposable
    {
        public IReactiveCollection<Node> Value { get; }
        
        public IReadOnlyReactiveProperty<Node> Head { get; }

        public CompositeDisposable Disposables { get; }

        public Subject<IReactiveCollection<Node>> Completed { get; }
        
        public Subject<IReactiveCollection<Node>> Failed { get; }

        public Sequence(IReactiveCollection<Node> value, Node head, CompositeDisposable disposables)
        {
            Value = value;
            Head = Value.ObserveAdd().Select(e => e.Value).ToReadOnlyReactiveProperty();
            Disposables = disposables;
            value.Add(head);
            head.Letter.IsPicked.Value = true;
            Completed = new Subject<IReactiveCollection<Node>>();
            Failed = new Subject<IReactiveCollection<Node>>();
        }

        public override string ToString() => new string(Value.Select(node => node.Letter.Value).ToArray());

        public void Dispose()
        {
            Disposables?.Dispose();
            Completed?.Dispose();
            Failed?.Dispose();
        }
    }

    public static class SequenceExtensions
    {
        public static void Complete(this Sequence sequence)
        {
            for (var index = 0; index < sequence.Value.Count; index++)
            {
                var capturedIndex = index;
                var node = sequence.Value[capturedIndex];
                Observable.Timer(TimeSpan.FromSeconds(capturedIndex * .05f)).Subscribe(_ =>
                {
                    node.IsMatched.OnNext(true);
                    node.Letter.IsMatched.OnNext(true);
                });
            }

            sequence.Completed.OnNext(sequence.Value);
            sequence.Value.Clear();
        }

        public static void Fail(this Sequence sequence)
        {
            foreach (var node in sequence.Value)
                node.IsMatched.OnNext(false);
            
            sequence.Failed.OnNext(sequence.Value);
            sequence.Value.Clear();
        }
    }
}