using System;

namespace WordMaster
{
    [Serializable]
    public class PlayerSerializationState
    {
        public float BestDistancePassed;

        public PlayerSerializationState(float bestDistancePassed)
        {
            BestDistancePassed = bestDistancePassed;
        }
    }
}