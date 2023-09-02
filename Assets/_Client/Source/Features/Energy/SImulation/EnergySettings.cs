using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create EnergySettings", fileName = "EnergySettings", order = 0)]
    public class EnergySettings : ScriptableObject
    {
        public float LossPerSecond;
        public float RecoveryPerScorePoint;
        public float InitialEnergyAmount;
    }
}