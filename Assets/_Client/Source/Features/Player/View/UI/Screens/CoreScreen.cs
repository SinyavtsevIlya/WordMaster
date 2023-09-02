using UnityEngine;

namespace WordMaster
{
    public class CoreScreen : MonoBehaviour
    {
        [field: SerializeField] public ScoreWidget ScoreWidget { get; private set; }
        [field: SerializeField] public EnergyWidget EnergyWidget { get; private set; }
    }
}