using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class EnergyWidget : MonoBehaviour, IFlowTarget
    {
        [SerializeField] private Image _energyBar;
        [SerializeField] private TMP_Text _energyLabel;
        
        public void SetEnergy(float current, float max)
        {
            if (max == 0f)
                throw new ArgumentException("max energy can't be zero", nameof(max));
            
            _energyBar.fillAmount = current / max;
            _energyLabel.SetText($"{(int)current}/{(int)max}");
        }

        public Vector3 GetTargetPosition()
        {
            var sizeDeltaX = _energyBar.rectTransform.rect.width;
            var deltaX = Vector3.right * sizeDeltaX;
            return _energyBar.transform.position - deltaX + deltaX * _energyBar.fillAmount;
        }

        public void SetFlowCompleted()
        {
            
        }
    }
}