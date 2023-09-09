using TMPro;
using UnityEngine;

namespace WordMaster
{
    public class BestDistanceMarkerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        
        public void SetDistance(float distance)
        {
            _label.SetText($"{(int)distance} m");
            var p = transform.position;
            transform.position = new Vector3(distance, p.y, p.z);
        }
    }
}