using Plugins.Nanory.SaveLoad;
using Rules;
using UniRx;

namespace WordMaster
{
    public class SavePlayerRule : IRule
    {
        private readonly Player _player;
        private readonly PlayerSerializationState _playerSerializationState;
        private readonly CompositeDisposable _disposables;
        private readonly ISaveLoad _saveLoad;

        public SavePlayerRule(Player player, PlayerSerializationState playerSerializationState, CompositeDisposable disposables, ISaveLoad saveLoad)
        {
            _player = player;
            _playerSerializationState = playerSerializationState;
            _disposables = disposables;
            _saveLoad = saveLoad;
        }
        
        public void Initialize()
        {
            _player.BestDistancePassed
                .Where(bestDistancePassed => bestDistancePassed > _playerSerializationState.BestDistancePassed)
                .Subscribe(bestDistancePassed =>
                {
                    _playerSerializationState.BestDistancePassed = bestDistancePassed;
                    SavePlayer();
                })
                .AddTo(_disposables);

            _player.CompletedWords.ObserveAdd().Where(_ => !_playerSerializationState.IsTutorialShown)
                .Subscribe(_ =>
                {
                    _playerSerializationState.IsTutorialShown = true;
                    SavePlayer();
                })
                .AddTo(_disposables);
            
            Disposable.Create(SavePlayer).AddTo(_disposables);
            Observable.OnceApplicationQuit().Subscribe(_ => SavePlayer()).AddTo(_disposables);
        }

        private void SavePlayer()
        {
            _saveLoad.Save("player", _playerSerializationState);
        }
    }
}