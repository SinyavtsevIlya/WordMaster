using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create UISettings", fileName = "UISettings", order = 0)]
    public class UISettings : ScriptableObject
    {
        public Canvas CanvasPrefab;
        public MonoBehaviour[] Screens;
        public CoreScreen CoreScreen;
    }
}