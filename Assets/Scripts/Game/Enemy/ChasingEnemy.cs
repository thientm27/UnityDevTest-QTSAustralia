using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy
{
    public abstract class ChasingEnemy : MonoBehaviour, IObstacle

    {
        protected Vector3 targetPosition;

        public virtual void Initialize(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
            // Di chuyển vật cản về phía nhân vật
            Vector3 direction = (targetPosition - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = direction * speed;
        }

        public abstract void OnCollisionWithPlayer(GameObject player);

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnCollisionWithPlayer(collision.gameObject);
            }
        }

        protected float speed = 5.0f; // Tốc độ di chuyển của vật cản
    }
}