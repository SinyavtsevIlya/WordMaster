using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create CameraSettings", fileName = "CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        public float ScrollSpeed;
        public Camera Prefab;
    }
}