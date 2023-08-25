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
            _letter.Position.Subscribe(_view.SetPosition).AddTo(_letter.Disposables);
        }
    }
}