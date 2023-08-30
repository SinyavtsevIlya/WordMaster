using System;
using UniRx;
using UnityEngine;

namespace WordMaster
{
    public class ScoreWidgetPresenter : IRule
    {
        private readonly Binding<Score, ScoreWidget> _scoreBinding;
        private CompositeDisposable _disposables;

        public ScoreWidgetPresenter(Binding<Score, ScoreWidget> scoreBinding, CompositeDisposable disposables)
        {
            _scoreBinding = scoreBinding;
            _disposables = disposables;
        }

        public void Initialize()
        {
            _scoreBinding.Subscribe(bind =>
            {
                bind.Model
                    .Subscribe(score => bind.View.DisplayScore(score))
                    .AddTo(bind.Disposables);
            }).AddTo(_disposables);
        }
    }
}