using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyB : ChasingEnemy
    {
        [SerializeField] private Collider thisCollider;
        private int damage = 5; // Damage to player

        public override void OnCollisionWithPlayer(GameObject player)
        {
            StartCoroutine(DisableColliderAndDestroy());
        }

        private IEnumerator DisableColliderAndDestroy()
        {
            thisCollider.enabled = false; // off collider
            yield return new WaitForSeconds(3); // wait for 3 secs
            // put into pool
            SimplePool.Despawn(gameObject);
        }
    }
}