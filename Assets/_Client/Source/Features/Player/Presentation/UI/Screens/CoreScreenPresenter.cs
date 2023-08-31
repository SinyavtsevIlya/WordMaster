using System;
using Rules;

namespace WordMaster
{
    public class CoreScreenPresenter : IRule, IDisposable
    {
        private readonly CoreScreen _screen;
        private readonly Score _score;

        private readonly Binding<Score, ScoreWidget> _scoreBinding;

        public CoreScreenPresenter(CoreScreen screen, Score score, 
            Binding<Score, ScoreWidget> scoreBinding)
        {
            _screen = screen;
            _score = score;
            _scoreBinding = scoreBinding;
        }

        public void Initialize()
        {
            _scoreBinding.Bind(_score, _screen.ScoreWidget);
        }

        public void Dispose()
        {
            _scoreBinding.Unbind();
        }
    }
}