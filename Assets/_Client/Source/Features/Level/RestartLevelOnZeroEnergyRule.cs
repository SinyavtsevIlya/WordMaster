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
        private readonly LoseEvent _loseEvent;
        private readonly ResumeEvent _resumeEvent;
        private readonly RanOutOfEnergyEvent _outOfEnergyEvent;

        public RestartLevelOnZeroEnergyRule(Player player, Level level, LoseEvent loseEvent, ResumeEvent resumeEvent, 
            RanOutOfEnergyEvent outOfEnergyEvent)
        {
            _player = player;
            _level = level;
            _loseEvent = loseEvent;
            _resumeEvent = resumeEvent;
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

            _loseEvent.Value.Subscribe(_ =>
            {
                Time.timeScale = 1f;
                _player.Dispose();
                _level.Disposables.Dispose();
                SceneManager.LoadScene("Core");
            }).AddTo(_player.Disposables);

            _resumeEvent.Value.Subscribe(_ =>
            {
                Time.timeScale = 1f;
                _player.Energy.Current.Value += _player.Energy.Max.Value;
            }).AddTo(_player.Disposables);
        }
    }
}