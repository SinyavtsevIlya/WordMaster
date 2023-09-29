using System;
using UnityEngine;

namespace WordMaster
{
    [Serializable]
    public class PlayerSerializationState
    {
        public float BestDistancePassed;
        public bool IsTutorialShown;
        public SystemLanguage Language;

        public PlayerSerializationState(float bestDistancePassed, bool isTutorialShown, SystemLanguage language)
        {
            BestDistancePassed = bestDistancePassed;
            IsTutorialShown = isTutorialShown;
            Language = language;
        }
    }
}