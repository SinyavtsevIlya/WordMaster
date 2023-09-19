using System;

namespace WordMaster
{
    [Serializable]
    public class PlayerSerializationState
    {
        public float BestDistancePassed;
        public bool IsTutorialShown;

        public PlayerSerializationState(float bestDistancePassed, bool isTutorialShown)
        {
            BestDistancePassed = bestDistancePassed;
            IsTutorialShown = isTutorialShown;
        }
    }
}