using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create NodeSettings", fileName = "NodeSettings", order = 0)]
    public class NodeSettings : ScriptableObject
    {
        public float VerticalMovementSmoothness;
        public float LettersSpacing;
    }
}