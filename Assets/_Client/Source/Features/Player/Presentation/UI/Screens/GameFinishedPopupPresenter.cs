using Rules;
using UniRx;

namespace WordMaster
{
    public class GameFinishedPopupPresenter : IRule
    {
        private readonly GameFinishedPopup _gameFinishedPopup;
        private readonly Player _player;
        private readonly RanOutOfEnergyEvent _outOfEnergyEvent;
        private readonly ResumeRequest _resumeRequest;
        private readonly RestartRequest _restartRequest;

        public GameFinishedPopupPresenter(GameFinishedPopup gameFinishedPopup, Player player, 
            RanOutOfEnergyEvent outOfEnergyEvent, ResumeRequest resumeRequest, RestartRequest restartRequest)
        {
            _gameFinishedPopup = gameFinishedPopup;
            _player = player;
            _outOfEnergyEvent = outOfEnergyEvent;
            _resumeRequest = resumeRequest;
            _restartRequest = restartRequest;
        }
        
        public void Initialize()
        {
            _gameFinishedPopup.RestartGameButton.OnClickAsObservable()
                .Subscribe(_ => _restartRequest.Value.OnNext(Unit.Default))
                .AddTo(_player.Disposables);

            _gameFinishedPopup.ResumeGameButton.OnClickAsObservable().Subscribe(_ =>
            {
                YandexGamesSdk.ShowRewardedAd()
                    .OnRewardedAdWatched(() =>
                    {
                        _gameFinishedPopup.Hide();
                        _resumeRequest.Value.OnNext(Unit.Default);
                    })
                    .OnRewardedAdOpen(() =>
                    {
                        // turn off volume
                    })
                    .AddTo(_player.Disposables);
            }).AddTo(_player.Disposables);

            _player.BestDistancePassed.Subscribe(distance =>
                _gameFinishedPopup.DisplayBestDistance(distance))
                .AddTo(_gameFinishedPopup);

            _player.ObserveEveryValueChanged(p => p.DistancePassed)
                .Subscribe(distance => _gameFinishedPopup.DisplayCurrentDistance(distance))
                .AddTo(_gameFinishedPopup);

            _outOfEnergyEvent.Value.Subscribe(_ => _gameFinishedPopup.Show()).AddTo(_gameFinishedPopup);
        }
    }
}