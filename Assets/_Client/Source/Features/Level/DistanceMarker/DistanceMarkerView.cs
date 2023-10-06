using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace WordMaster
{
    public class DistanceMarkerView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private LocalizedString _unitsString;
        
        public void SetDistance(float distance, float height)
        {
            _label.SetText($"{distance} {_unitsString.GetLocalizedStringAsync().Result}");
            var p = transform.position;
            transform.position = new Vector3(distance, height, p.z);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}