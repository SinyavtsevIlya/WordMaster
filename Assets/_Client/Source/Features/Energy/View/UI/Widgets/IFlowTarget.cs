using UnityEngine;

namespace WordMaster
{
    public interface IFlowTarget
    {
        public Vector3 GetTargetPosition();

        public void SetFlowCompleted();
    }
}