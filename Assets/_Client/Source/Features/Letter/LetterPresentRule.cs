using Rules;
using UniRx;
using Zenject;

namespace WordMaster
{
    public class LetterPresentRule : IRule, IInitializable
    {
        private readonly Letter _letter;
        private readonly LetterView _view;

        public LetterPresentRule(Letter letter, LetterView view)
        {
            _letter = letter;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetCharacter(_letter.Value);
            Observable.EveryUpdate().Subscribe(_ => _letter.Width = _view.GetWidth());
            _letter.Position.Subscribe(_view.SetPosition).AddTo(_letter.Disposables);
            _letter.IsPicked.Where(isTrue => isTrue).Subscribe(_ => _view.SetAsPicked()).AddTo(_letter.Disposables);
            _letter.IsMatched.Subscribe(_view.SetMatchState).AddTo(_letter.Disposables);
            _letter.Culled.Subscribe(_ => _view.Dispose()).AddTo(_letter.Disposables);
            _view.RandomizeRotation();
        }
    }
}