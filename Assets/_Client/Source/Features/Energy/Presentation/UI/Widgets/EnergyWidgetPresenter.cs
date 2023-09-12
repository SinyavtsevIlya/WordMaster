using Rules;
using UniRx;
using Zenject;

namespace WordMaster
{
    public class EnergyWidgetPresenter : IRule, IInitializable
    {
        private readonly Energy _energy;
        private readonly EnergyWidget _energyWidget;
        private readonly CompositeDisposable _disposables;

        public EnergyWidgetPresenter(Energy energy, EnergyWidget energyWidget, CompositeDisposable disposables)
        {
            _energy = energy;
            _energyWidget = energyWidget;
            _disposables = disposables;
        }
        
        public void Initialize()
        {
            _energy.Current
                .Pairwise()
                .Subscribe(energy => _energyWidget.SetEnergy(
                    energy.Current, 
                    _energy.Max.Value, 
                    energy.Current - energy.Previous > 0f ? .5f : 0f))
                .AddTo(_disposables);
        }
    }
}