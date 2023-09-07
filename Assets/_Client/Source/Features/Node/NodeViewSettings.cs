using DG.Tweening;
using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create NodeViewSettings", fileName = "NodeViewSettings", order = 0)]
    public class NodeViewSettings : ScriptableObject
    {
        [field: SerializeField] public Color MatchedColor {get; private set;}
        [field: SerializeField] public Color FailedColor {get; private set;}
        [field: SerializeField] public Ease ScaleEase {get; private set;}
        [field: SerializeField] public float ScaleDuration {get; private set;}
        [field: SerializeField] public float ScaleEndValue {get; private set;}
        [field: SerializeField] public Ease ColorFadeEase {get; private set;}
        [field: SerializeField] public float ColorDuration {get; private set;}
        [field: SerializeField] public float AppearDuration {get; private set;}
        [field: SerializeField] public Ease AppearEase {get; private set;}
    }
}