using System.Collections;
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
        [SerializeField] protected float lifeTime = 10.0f;
        [SerializeField] protected float warningRange = 3f;
        [SerializeField] protected Rigidbody RigidbodyEnemy;
        [SerializeField] private Renderer enemyRenderer;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private Color closeColor = Color.red;
        private Coroutine _destroyCoroutine;

        public void Initialize(Transform targetPosition, UnityAction<int> onHitPlayer)
        {
            PlayerTf = targetPosition;
            OnHitPlayerEvent = onHitPlayer;
            StartMove();
            if (_destroyCoroutine != null)
            {
                StopCoroutine(_destroyCoroutine);
            }

            _destroyCoroutine = StartCoroutine(DestroyAfterLifetime());
        }

        public virtual void StartMove()
        {
            Vector3 direction = (PlayerTf.position - transform.position).normalized;
            direction.y = 0;
            RigidbodyEnemy.velocity = direction * Speed;
        }

        public abstract void OnCollisionWithPlayer(GameObject player);

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.PlayerTag))
            {
                OnCollisionWithPlayer(other.gameObject);
            }
        }

        private void Update()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, PlayerTf.position);
            if (distanceToPlayer <= warningRange)
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

        private IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(lifeTime);
            SimplePool.Despawn(gameObject);
        }
    }
}