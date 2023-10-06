using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create ZoomSettings", fileName = "ZoomSettings", order = 0)]
    public class ZoomSettings : ScriptableObject
    {
        [field: SerializeField] public float Value { get; private set; }
    }
}