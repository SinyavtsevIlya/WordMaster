using UniRx;

namespace WordMaster
{
    public class LetterPresentRule
    {
        public LetterPresentRule(Letter letter, LetterView view)
        {
            view.SetCharacter(letter.Value);
            letter.Position.Subscribe(view.SetPosition).AddTo(letter.Disposables);
        }
    }
}