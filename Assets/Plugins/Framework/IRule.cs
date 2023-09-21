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

public static class RuleExtensions
{
    public static void BindRule<TRule>(this DiContainer container, params object[] args) where TRule : IInitializable
    {
        container.BindInterfacesAndSelfTo<TRule>().AsCached().WithArguments(args);
    }
}

public static class DiExtensions
{
    public static void BindSubKernel(this DiContainer container)
    {
        container.Bind<Kernel>().AsCached()
            .OnInstantiated<Kernel>((a, b) =>
            {
                b.Initialize();
                foreach (var parentContainer in container.ParentContainers)
                {
                    parentContainer.Resolve<DisposableManager>().Add(b);
                }
            })
            .NonLazy();
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
