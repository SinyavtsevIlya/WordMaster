using Rules;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace WordMaster
{
    public class LossEnergyRule : IRule, IInitializable
    {
        private readonly Energy _energy;
        private readonly EnergySettings _energySettings;
        private readonly CompositeDisposable _disposables;

        public LossEnergyRule(Energy energy, EnergySettings energySettings, CompositeDisposable disposables)
        {
            _energy = energy;
            _energySettings = energySettings;
            _disposables = disposables;
        }
        
        public void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _energy.Current.Value -= Time.deltaTime * _energySettings.LossPerSecond;
                })
                .AddTo(_disposables);
        }
    }
}