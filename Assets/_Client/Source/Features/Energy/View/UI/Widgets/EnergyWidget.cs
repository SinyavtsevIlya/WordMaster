using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordMaster
{
    public class EnergyWidget : MonoBehaviour
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
    }
}