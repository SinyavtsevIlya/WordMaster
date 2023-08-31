using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class Player
    {
        private readonly Camera _camera;
        public Sequence Sequence { get; }
        public IReactiveCollection<Word> CompletedWords { get; }
        public Score Score { get; }
        
        public Energy Energy { get; }
        public Energy MaxEnergy { get; }
        
        public CompositeDisposable Disposables { get; }

        public float DistancePassed => _camera.transform.position.x;

        public Player(Sequence sequence, 
            IReactiveCollection<Word> completedWords, 
            Camera camera, 
            CompositeDisposable disposables, Score score)
        {
            _camera = camera;
            Sequence = sequence;
            CompletedWords = completedWords;
            Disposables = disposables;
            Score = score;
        }
    }
}