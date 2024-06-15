using UnityEngine;

namespace Game
{
    public class GameModel : MonoBehaviour
    {
        [Header("Enemy A")]
        [SerializeField] private float enemyASpawnRateMin;
        [SerializeField] private float enemyASpawnRateMax;
        [SerializeField] private GameObject enemyAPrefab;
        [Header("Enemy B")]
        [SerializeField] private float enemyBSpawnRateMin;
        [SerializeField] private float enemyBSpawnRateMax;
        [SerializeField] private GameObject enemyBPrefab;
        [Header("Other Config")]
        [SerializeField] private float enemySpawnRange;
        [SerializeField] private int playerBaseHealth = 100;
        public float EnemyASpawnRateMin => enemyASpawnRateMin;
        public float EnemyASpawnRateMax => enemyASpawnRateMax;
        public float EnemyBSpawnRateMin => enemyBSpawnRateMin;
        public float EnemyBSpawnRateMax => enemyBSpawnRateMax;
        public float EnemySpawnRange => enemySpawnRange;
        public int PlayerBaseHealth => playerBaseHealth;
        public GameObject EnemyAPrefab => enemyAPrefab;
        public GameObject EnemyBPrefab => enemyBPrefab;
    }
}