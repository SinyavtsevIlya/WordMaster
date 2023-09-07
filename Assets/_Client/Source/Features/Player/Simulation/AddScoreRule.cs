using System.Linq;
using Rules;
using UniRx;
using Zenject;

namespace WordMaster
{
    public class AddScoreRule : IRule, IInitializable
    {
        private readonly Player _player;

        public AddScoreRule(Player player)
        {
            _player = player;
        }
        
        public void Initialize()
        {
            // _player.Sequence.Completed
            //     .Subscribe(word =>
            //     {
            //         _player.Score.Value += word.Count;
            //     })
            //     .AddTo(_player.Disposables);

            _player.Sequence.Value.ObserveAdd().Subscribe(addEvent =>
            {
                addEvent.Value
                    .IsMatched.Where(isMatched => isMatched)
                    .Subscribe(isMatched => _player.Score.Value++)
                    .AddTo(addEvent.Value.Disposables);
            }).AddTo(_player.Disposables);
        }
    }
}