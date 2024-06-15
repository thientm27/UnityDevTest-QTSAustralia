using UnityEngine;
using UnityEngine.Events;

namespace Game.Component
{
    public class ScoreItem : MonoBehaviour
    {
        [SerializeField] private int scoreValue;
        private UnityAction<int> OnPlayerScore;

        public void Initialize(UnityAction<int> onHitPlayer)
        {
            OnPlayerScore = onHitPlayer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.PlayerTag))
            {
                OnPlayerScore?.Invoke(scoreValue);
                SimplePool.Despawn(gameObject);
            }
        }
    }
}