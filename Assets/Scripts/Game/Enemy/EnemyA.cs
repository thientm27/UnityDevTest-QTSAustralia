using UnityEngine;

namespace Game.Enemy
{
    public class EnemyA : ChasingEnemy
    {
        public override void OnCollisionWithPlayer(GameObject player)
        {
            // Xử lý logic khi va chạm với nhân vật
            player.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject); // Phá hủy vật cản A
        }

        private int damage = 10; // Sát thương gây ra cho nhân vật
    }
}
