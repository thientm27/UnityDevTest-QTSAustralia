using TMPro;
using UnityEngine;

namespace EndGameScene
{
    public class LeaderScoreItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreValue;
        public TextMeshProUGUI ScoreValue => scoreValue;
    }
}