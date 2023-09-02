using System;
using Rules;
using Zenject;

namespace WordMaster
{
    public class CoreScreenPresenter : IRule, IDisposable
    {
        private readonly CoreScreen _screen;
        private readonly Score _score;

        public CoreScreenPresenter(CoreScreen screen, Score score)
        {
            _screen = screen;
            _score = score;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}