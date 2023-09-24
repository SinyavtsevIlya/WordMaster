using Rules;
using UniRx;

namespace WordMaster
{
    public class GameFinishedPopupPresenter : IRule
    {
        private readonly GameFinishedPopup _gameFinishedPopup;
        private readonly Player _player;
        private readonly RanOutOfEnergyEvent _outOfEnergyEvent;
        private readonly ResumeEvent _resumeEvent;
        private readonly LoseEvent _loseEvent;

        public GameFinishedPopupPresenter(GameFinishedPopup gameFinishedPopup, Player player, 
            RanOutOfEnergyEvent outOfEnergyEvent, ResumeEvent resumeEvent, LoseEvent loseEvent)
        {
            _gameFinishedPopup = gameFinishedPopup;
            _player = player;
            _outOfEnergyEvent = outOfEnergyEvent;
            _resumeEvent = resumeEvent;
            _loseEvent = loseEvent;
        }
        
        public void Initialize()
        {
            _gameFinishedPopup.RestartGameButton.OnClickAsObservable()
                .Subscribe(_ => _loseEvent.Value.OnNext(Unit.Default))
                .AddTo(_player.Disposables);

            _gameFinishedPopup.ResumeGameButton.OnClickAsObservable().Subscribe(_ =>
            {
                _gameFinishedPopup.Hide();
                _resumeEvent.Value.OnNext(Unit.Default);
            }).AddTo(_player.Disposables);

            _player.BestDistancePassed.Subscribe(distance =>
                _gameFinishedPopup.BestDistanceLabel.SetText($"{(int)distance} m"))
                .AddTo(_gameFinishedPopup);

            _player.ObserveEveryValueChanged(p => p.DistancePassed)
                .Subscribe(distance => _gameFinishedPopup.CurrentDistanceLabel.SetText($"{(int)distance} m"))
                .AddTo(_gameFinishedPopup);

            _outOfEnergyEvent.Value.Subscribe(_ => _gameFinishedPopup.Show()).AddTo(_gameFinishedPopup);
        }
    }
}