using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyB : ChasingEnemy
    {
        [SerializeField] private Collider thisCollider;
        [SerializeField] private int damage = 10; // Sát thương gây ra cho nhân vật
        public override void OnCollisionWithPlayer(GameObject player)
        {
            StartCoroutine(DisableColliderAndDestroy());
        }

        private IEnumerator DisableColliderAndDestroy()
        {
            OnHitPlayerEvent?.Invoke(damage);
            thisCollider.enabled = false; // off collider
            yield return new WaitForSeconds(3); // wait for 3 secs
            // put into pool
            SimplePool.Despawn(gameObject);
        }
    }
}