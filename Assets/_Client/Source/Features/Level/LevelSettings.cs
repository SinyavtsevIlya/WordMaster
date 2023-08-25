using UnityEngine;

namespace WordMaster
{
    [CreateAssetMenu(menuName = "Create LevelSettings", fileName = "LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        public int CharactersCount;
    }
}