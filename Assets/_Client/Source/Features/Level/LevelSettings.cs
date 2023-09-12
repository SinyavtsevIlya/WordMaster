using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LevelSettings", fileName = "LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public Vector2Int RightCharactersPerStep { get; private set; }
        [field: SerializeField] public Vector2Int WrongCharactersPerStep { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField] public int GenerationOffset { get; private set; }
        [field: SerializeField] public int GenerationOffsetRandomization { get; private set; }
        [field: SerializeField] public int StageWidth { get; private set; }
        
        [field: SerializeField] public DistanceMarkerView DistanceMarkerView { get; private set; }
        [field: SerializeField] public BestDistanceMarkerView BestDistanceMarkerView { get; private set; }

        public int LevelHalfWidth => (int) ((float) Screen.width / Screen.height * Height / 2f + GenerationOffset);
    }
}