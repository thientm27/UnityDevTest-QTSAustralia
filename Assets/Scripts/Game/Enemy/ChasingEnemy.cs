using Game.Enemy.Interface;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public abstract class ChasingEnemy : MonoBehaviour, IObstacle
    {
        protected Transform PlayerTf;
        protected UnityAction<int> OnHitPlayerEvent;
        [SerializeField] protected float Speed = 5.0f;
        [SerializeField] protected Rigidbody RigidbodyEnemy;
        [SerializeField] private Renderer enemyRenderer;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color closeColor = Color.red;

        public void Initialize(Transform targetPosition, UnityAction<int> onHitPlayer)
        {
            PlayerTf = targetPosition;
            OnHitPlayerEvent = onHitPlayer;
            StartMove();
        }

        public virtual void StartMove()
        {
            Vector3 direction = (PlayerTf.position - transform.position).normalized;
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

        private void Update()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, PlayerTf.position);
            if (distanceToPlayer <= 1.0f)
            {
                ChangeColor(closeColor);
            }
            else
            {
                ChangeColor(defaultColor);
            }
        }

        private void ChangeColor(Color color)
        {
            if (enemyRenderer != null)
            {
                enemyRenderer.material.color = color;
            }
        }
    }
}