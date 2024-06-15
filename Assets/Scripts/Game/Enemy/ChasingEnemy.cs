using System;
using Game.Enemy.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public abstract class ChasingEnemy : MonoBehaviour, IObstacle
    {
        protected Vector3 TargetPosition;
        protected UnityAction<int> OnHitPlayerEvent;
        protected float Speed = 5.0f; // enemy move speed
        [SerializeField] protected Rigidbody RigidbodyEnemy;

        public void Initialize(Vector3 targetPosition, UnityAction<int> onHitPlayer)
        {
            TargetPosition = targetPosition;
            OnHitPlayerEvent = onHitPlayer;
            StartMove();
        }

        public virtual void StartMove()
        {
            Vector3 direction = (TargetPosition - transform.position).normalized;
            RigidbodyEnemy.velocity = direction * Speed;
        }

        public abstract void OnCollisionWithPlayer(GameObject player);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnCollisionWithPlayer(other.gameObject);
            }

        }

        // protected virtual void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.CompareTag("Player"))
        //     {
        //         OnCollisionWithPlayer(collision.gameObject);
        //     }
        // }
    }
}