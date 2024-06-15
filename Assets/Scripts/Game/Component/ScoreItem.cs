using UnityEngine;
using UnityEngine.Events;

namespace Game.Component
{
    public class ScoreItem : MonoBehaviour
    {
        [SerializeField] private int scoreValue;
        private UnityAction<int> _onPlayerScore;

        public void Initialize(UnityAction<int> onHitPlayer)
        {
            _onPlayerScore = onHitPlayer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.PlayerTag))
            {
                _onPlayerScore?.Invoke(scoreValue);
                SimplePool.Despawn(gameObject);
            }
        }
    }
}