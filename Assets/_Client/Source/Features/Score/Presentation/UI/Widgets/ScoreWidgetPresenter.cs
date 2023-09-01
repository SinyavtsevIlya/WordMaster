using System;
using Rules;
using UniRx;
using UnityEngine;
using Zenject;

namespace WordMaster
{
    public class ScoreWidgetPresenter : IRule, IInitializable
    {
        private readonly ScoreWidget _scoreWidget;
        private readonly Score _score;
        private readonly CompositeDisposable _disposable;

        public ScoreWidgetPresenter(ScoreWidget scoreWidget, Score score, CompositeDisposable disposable)
        {
            _scoreWidget = scoreWidget;
            _score = score;
            _disposable = disposable;
        }


        public void Initialize()
        {
            _score
                .Subscribe(score => _scoreWidget.DisplayScore(score))
                .AddTo(_disposable);
        }
    }
}