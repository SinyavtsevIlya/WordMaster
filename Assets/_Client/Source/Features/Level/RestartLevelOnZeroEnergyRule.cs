using Rules;
using UniRx;
using UnityEngine.SceneManagement;

namespace WordMaster
{
    public class RestartLevelOnZeroEnergyRule : IRule
    {
        private readonly Player _player;
        private readonly Level _level;

        public RestartLevelOnZeroEnergyRule(Player player, Level level)
        {
            _player = player;
            _level = level;
        }
        
        public void Initialize()
        {
            _player.Energy.Current.Where(energy => energy <= 0f).Subscribe(_ =>
                {
                    _player.Dispose();
                    _level.Disposables.Dispose();
                    SceneManager.LoadScene("Core");
                })
                .AddTo(_player.Disposables);
        }
    }
}