using Rules;
using Zenject;
using UniRx;

namespace WordMaster
{
    public class AddEnergyForScoreRule : IRule, IInitializable
    {
        private readonly Player _player;
        private readonly EnergySettings _energySettings;

        public AddEnergyForScoreRule(Player player, EnergySettings energySettings)
        {
            _player = player;
            _energySettings = energySettings;
        }

        public void Initialize()
        {
            _player.Sequence.Completed.Subscribe(word =>
            {
                _player.Energy.Current.Value += word.Count * _energySettings.RecoveryPerScorePoint;
            }).AddTo(_player.Disposables);
        }
    }
}