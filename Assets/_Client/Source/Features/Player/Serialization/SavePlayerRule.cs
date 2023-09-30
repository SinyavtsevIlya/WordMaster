using Plugins.Nanory.SaveLoad;
using Rules;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

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

            var localization = LocalizationSettings.Instance;
            
            Observable.FromEvent<Locale>(
                    h => localization.OnSelectedLocaleChanged += h,
                    h => localization.OnSelectedLocaleChanged -= h)
                .Subscribe(_ =>
                {
                    var locale = localization.GetSelectedLocale();
                    var language = locale.LocaleName == "Russian (ru)"
                        ? SystemLanguage.Russian
                        : SystemLanguage.English;

                    _playerSerializationState.Language = language;
                    SavePlayer();
                }).AddTo(_disposables);
            
            Disposable.Create(SavePlayer).AddTo(_disposables);
            Observable.OnceApplicationQuit().Subscribe(_ => SavePlayer()).AddTo(_disposables);
        }

        private void SavePlayer()
        {
            _saveLoad.Save("player", _playerSerializationState);
        }
    }
}