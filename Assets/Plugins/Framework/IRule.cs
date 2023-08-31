using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Rules
{
    
public interface IRule : IInitializable
{
}

public struct BindEvent<TModel, TView>
{
    public TModel Model;
    public TView View;
    public CompositeDisposable Disposables;
    
    public BindEvent(TModel model, TView view, CompositeDisposable disposables)
    {
        Model = model;
        View = view;
        Disposables = disposables;
    }
}

public class Binding<TModel, TView> : IObservable<BindEvent<TModel, TView>>
{
    private readonly CompositeDisposable _disposables;
    public Subject<BindEvent<TModel, TView>> Subject { get;}

    public Binding(Subject<BindEvent<TModel, TView>> subject, CompositeDisposable disposables)
    {
        _disposables = disposables;
        Subject = subject;
    }

    public void Bind(TModel model, TView view) => Subject.OnNext(new BindEvent<TModel, TView>(model, view, _disposables));
    public void Unbind() => _disposables.Clear();

    public IDisposable Subscribe(IObserver<BindEvent<TModel, TView>> observer) => Subject.Subscribe(observer);
}

public static class RuleExtensions
{
    public static void BindViewToModel<TModel, TView>(this DiContainer container)
    {
        container.Bind<Binding<TModel, TView>>().AsSingle();
        container.Bind<Subject<BindEvent<TModel, TView>>>().AsSingle();
    }
    
    public static void BindRule<TRule>(this DiContainer container, params object[] args) where TRule : IInitializable
    {
        container.BindInterfacesTo<TRule>().AsCached().WithArguments(args);
    }
}

public static class DiExtensions
{
    public static void BindSubKernel(this DiContainer container)
    {
        container.Bind<Kernel>().AsCached().OnInstantiated<Kernel>((a, b) => b.Initialize()).NonLazy();
    }
}

public class ClearableReactiveCollection<T> : ReactiveCollection<T>
{
    protected override void ClearItems()
    {
        for (int i = Count - 1; i >= 0; i--) 
            RemoveItem(i);
    }
}

public static class LinqExtensions
{
    private static System.Random cachedRandom = new System.Random();  

    public static IList<T> Shuffle<T>(this IList<T> list, System.Random random = null)
    {
        var rnd = random ?? cachedRandom;
        var n = list.Count;  
        while (n > 1) {  
            n--;  
            var k = rnd.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}
}
