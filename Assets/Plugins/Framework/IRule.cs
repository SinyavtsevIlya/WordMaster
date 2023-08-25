using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IRule : IInitializable
{
}

public static class RuleExtensions
{
    public static void BindRule<TRule>(this DiContainer container, params object[] args) where TRule : IRule
    {
        container.BindInterfacesTo<TRule>().AsCached().WithArguments(args);
    }
}
