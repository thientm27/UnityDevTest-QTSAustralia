using GameUtils;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyA : ChasingEnemy
    {
       [SerializeField] private int damage = 10; // Sát thương gây ra cho nhân vật

        public override void OnCollisionWithPlayer(GameObject player)
        {
            OnHitPlayerEvent?.Invoke(damage);
            SimplePool.Despawn(gameObject);
        }
    }
}