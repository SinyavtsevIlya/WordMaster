using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace WordMaster
{
    public class BestDistanceMarkerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private LocalizedString _unitsString;
        
        public void SetDistance(float distance)
        {
            _label.SetText($"{(int)distance} {_unitsString.GetLocalizedString()}");
            var p = transform.position;
            transform.position = new Vector3(distance, p.y, p.z);
        }
    }
}