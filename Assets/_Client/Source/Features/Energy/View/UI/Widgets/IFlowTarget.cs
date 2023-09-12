using UnityEngine;

namespace WordMaster
{
    public interface IFlowTarget
    {
        public Transform GetTarget();

        public void SetFlowCompleted();
    }
}