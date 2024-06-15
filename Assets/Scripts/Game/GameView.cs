using UnityEngine;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private ProgressBarPro playerHealth;

        public void DisplayPlayerHealth(int maxHealth, int currentHeath)
        {
            playerHealth.SetValue(currentHeath, maxHealth);
        }
    }
}