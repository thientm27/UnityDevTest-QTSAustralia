using UnityEngine;

namespace Game
{
    /// <summary>
    /// This class contain config for game
    /// </summary>
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
        [Header("Score Obj")]
        [SerializeField] private GameObject scorePrefab;
        [SerializeField] private float timeSpawnScoreObj = 10f;
        [SerializeField] private float scoreItemMinRangeSpawn = 5f;

        [Header("Other Config")]
        [SerializeField] private float enemySpawnRange;
        [SerializeField] private int playerBaseHealth = 100;
        public float EnemyASpawnRateMin => enemyASpawnRateMin;
        public float EnemyASpawnRateMax => enemyASpawnRateMax;
        public GameObject EnemyAPrefab => enemyAPrefab;
        public float EnemyBSpawnRateMin => enemyBSpawnRateMin;
        public float EnemyBSpawnRateMax => enemyBSpawnRateMax;
        public GameObject EnemyBPrefab => enemyBPrefab;
        public GameObject ScorePrefab => scorePrefab;
        public float TimeSpawnScoreObj => timeSpawnScoreObj;
        public float ScoreItemMinRangeSpawn => scoreItemMinRangeSpawn;
        public float EnemySpawnRange => enemySpawnRange;
        public int PlayerBaseHealth => playerBaseHealth;
    }
}