using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Component
{
    public class EndZoneTrigger : MonoBehaviour
    {
        public UnityAction OnHitPlayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.PlayerTag))
            {
                OnHitPlayer?.Invoke();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(GameConstants.PlayerTag))
            {
                OnHitPlayer?.Invoke();
            }
        }
    }
}