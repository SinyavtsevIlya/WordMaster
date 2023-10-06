using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LevelSettings", fileName = "LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public Vector2Int RightCharactersPerStep { get; private set; }
        [field: SerializeField] public Vector2Int WrongCharactersPerStep { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public int VerticalOffset { get; private set; }
        [field: SerializeField] public int GenerationOffset { get; private set; }
        [field: SerializeField] public int GenerationOffsetRandomization { get; private set; }
        [field: SerializeField] public int StageWidth { get; private set; }
        
        [field: SerializeField] public DistanceMarkerView DistanceMarkerView { get; private set; }
        [field: SerializeField] public BestDistanceMarkerView BestDistanceMarkerView { get; private set; }

        [field: SerializeField] public GameObject ConnectionHintLabelPrefab;
        [field: SerializeField] public GameObject ConnectionHintArrowPrefab;
        [field: SerializeField] public PropSettings[] Props { get; private set; }
        [field: SerializeField] public GameObject Background { get; private set; }
    }
}