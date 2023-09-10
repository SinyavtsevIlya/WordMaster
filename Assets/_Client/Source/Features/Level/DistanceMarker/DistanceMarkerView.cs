using TMPro;
using UnityEngine;

namespace WordMaster
{
    public class DistanceMarkerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        
        public void SetDistance(float distance)
        {
            _label.SetText($"{distance} см.");
            var p = transform.position;
            transform.position = new Vector3(distance, p.y, p.z);
        }
    }
}