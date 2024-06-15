using TMPro;
using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private ProgressBarPro playerHealth;
        [SerializeField] private TextMeshProUGUI lastScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthTxt;

        public void SetLastScore(int lastScore)
        {
            lastScoreText.text = "Last Score: " + lastScore;
        }

        public void SetCurrentScore(int currentScore)
        {
            scoreText.text = "Score: " + currentScore;
        }

        public void DisplayPlayerHealth(int maxHealth, int currentHeath)
        {
            playerHealth.SetValue(currentHeath, maxHealth);
            healthTxt.text = "Your Health: " + currentHeath;
        }
    }
}