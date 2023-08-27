using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public interface IRule
{
}

public static class RuleExtensions
{
    public static void BindRule<TRule>(this DiContainer container, params object[] args) where TRule : IRule
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
