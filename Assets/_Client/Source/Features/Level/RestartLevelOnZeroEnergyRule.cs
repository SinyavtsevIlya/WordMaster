using Rules;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordMaster
{
    public class RestartLevelOnZeroEnergyRule : IRule
    {
        private readonly Player _player;
        private readonly Level _level;
        private readonly RestartRequest _restartRequest;
        private readonly ResumeRequest _resumeRequest;
        private readonly RanOutOfEnergyEvent _outOfEnergyEvent;

        public RestartLevelOnZeroEnergyRule(Player player, Level level, RestartRequest restartRequest, ResumeRequest resumeRequest, 
            RanOutOfEnergyEvent outOfEnergyEvent)
        {
            _player = player;
            _level = level;
            _restartRequest = restartRequest;
            _resumeRequest = resumeRequest;
            _outOfEnergyEvent = outOfEnergyEvent;
        }
        
        public void Initialize()
        {
            _player.Energy.Current.Where(energy => energy <= 0f)
                .Subscribe(_ =>
                {
                    Time.timeScale = 0f;
                    _outOfEnergyEvent.Value.OnNext(Unit.Default);
                })
                .AddTo(_player.Disposables);

            _restartRequest.Value.Subscribe(_ =>
            {
                Time.timeScale = 1f;
                _player.Dispose();
                _level.Disposables.Dispose();
                SceneManager.LoadScene("Core");
            }).AddTo(_player.Disposables);

            _resumeRequest.Value.Subscribe(_ =>
            {
                Time.timeScale = 1f;
                _player.Energy.Current.Value += _player.Energy.Max.Value;
            }).AddTo(_player.Disposables);
        }
    }
}