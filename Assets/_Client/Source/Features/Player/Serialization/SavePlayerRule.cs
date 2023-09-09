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

        public SavePlayerRule(Player player, PlayerSerializationState playerSerializationState, CompositeDisposable disposables)
        {
            _player = player;
            _playerSerializationState = playerSerializationState;
            _disposables = disposables;
        }
        
        public void Initialize()
        {
            Disposable.Create(() => SavePlayer()).AddTo(_disposables);
        }

        private void SavePlayer()
        {
            _playerSerializationState.BestDistancePassed = _player.BestDistancePassed.Value;

            SaveLoadOps.Save("player", _playerSerializationState, saveInfo => { });
        }
    }
}