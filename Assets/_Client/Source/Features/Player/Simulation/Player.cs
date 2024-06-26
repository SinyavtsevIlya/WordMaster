﻿using System;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Player : IDisposable
    {
        private readonly Camera _camera;
        public Sequence Sequence { get; }
        public IReactiveCollection<string> CompletedWords { get; }
        public Score Score { get; }
        
        public Energy Energy { get; }

        public CompositeDisposable Disposables { get; }

        public float DistancePassed => _camera.transform.position.x;
        
        public FloatReactiveProperty BestDistancePassed { get; } 

        public Player(Sequence sequence, 
            IReactiveCollection<string> completedWords, 
            Camera camera, 
            CompositeDisposable disposables,
            Score score, 
            Energy energy,
            FloatReactiveProperty bestDistancePassed)
        {
            _camera = camera;
            Sequence = sequence;
            CompletedWords = completedWords;
            Disposables = disposables;
            Score = score;
            Energy = energy;
            BestDistancePassed = bestDistancePassed;

            sequence.AddTo(Disposables);
        }

        public void Dispose()
        {
            Sequence?.Dispose();
            Score?.Dispose();
            Disposables?.Dispose();
        }
    }
}