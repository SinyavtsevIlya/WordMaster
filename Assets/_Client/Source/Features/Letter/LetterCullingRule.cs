using Rules;
using UniRx;

namespace WordMaster
{
    public class LetterCullingRule : IRule
    {
        private readonly Level _level;
        private readonly Player _player;

        public LetterCullingRule( Level level, Player player)
        {
            _level = level;
            _player = player;
        }
        
        public void Initialize()
        {
            _level.Letters.ObserveAdd().Subscribe(addEvent =>
            {
                var letter = addEvent.Value;
                
                letter.AddTo(_level.Disposables);

                var culling = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (_player.DistancePassed - letter.Position.Value.x >
                        _level.Settings.LevelHalfWidth)
                    {
                        letter.Culled.OnNext(Unit.Default);
                        letter.Dispose();
                    }
                });

                letter.IsPicked.Where(isTrue => isTrue).Subscribe(_ => culling.Dispose()).AddTo(letter.Disposables);
                culling.AddTo(_level.Disposables);
            }).AddTo(_level.Disposables);
        }
    }
}