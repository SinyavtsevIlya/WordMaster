using TMPro;
using UnityEngine;

namespace WordMaster
{
    public class ScoreWidget
    {
        [SerializeField] private TMP_Text _scoreLabel;
        
        public void DisplayScore(int score)
        {
            _scoreLabel.SetText(score.ToString());
        }
    }
}