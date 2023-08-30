using UniRx;
using UnityEngine.SceneManagement;

namespace WordMaster
{
    public class FailSequenceRule : IRule
    {
        private readonly Sequence _sequence;
        private readonly Level _level;

        public FailSequenceRule(Sequence sequence, Level level)
        {
            _sequence = sequence;
            _level = level;
        }
        
        public void Initialize()
        {
            _sequence.Failed.Subscribe(_ =>
            {
                _level.Dispose();
                _sequence.Dispose();
                
                SceneManager.LoadScene("Core");
            }).AddTo(_sequence.Disposables);
        }
    }
}