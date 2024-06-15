using System.Collections;
using Game.Enemy;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private GameModel gameModel;
        [SerializeField] private PlayerMoveController playerMoveController;

        private void Start()
        {
            StartCoroutine(SpawnEnemyA());
            StartCoroutine(SpawnEnemyB());
        }

        private void Update()
        {
            playerMoveController.HandlePlayerMove();
        }

        #region ENEMY SPAWN

        private IEnumerator SpawnEnemyA()
        {
            while (true)
            {
                float spawnRate = Random.Range(gameModel.EnemyASpawnRateMin, gameModel.EnemyASpawnRateMax);
                yield return new WaitForSeconds(spawnRate);

                ChasingEnemy enemyA = SpawnEnemy(gameModel.EnemyAPrefab);
                enemyA.Initialize(player, OnHitPlayer);
            }
        }

        private IEnumerator SpawnEnemyB()
        {
            while (true)
            {
                float spawnRate = Random.Range(gameModel.EnemyBSpawnRateMin, gameModel.EnemyBSpawnRateMax);
                yield return new WaitForSeconds(spawnRate);

                ChasingEnemy enemyB = SpawnEnemy(gameModel.EnemyBPrefab);
                enemyB.Initialize(player, OnHitPlayer);
            }
        }

        private ChasingEnemy SpawnEnemy(GameObject enemyPrefab)
        {
            var enemy = SimplePool.Spawn(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            return enemy.GetComponent<ChasingEnemy>();
        }

        private Vector3 GetRandomSpawnPosition()
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = Mathf.Cos(angle) * gameModel.EnemySpawnRange;
            float z = Mathf.Sin(angle) * gameModel.EnemySpawnRange;
            return new Vector3(player.position.x + x, player.position.y, player.position.z + z);
        }

        #endregion

        /// <summary>
        /// Callback when enemy hit player
        /// </summary>
        /// <param name="damage">damage that enemy/object that deal to player</param>
        private void OnHitPlayer(int damage)
        {
            Debug.Log($"Player bị tấn công với {damage} sát thương.");
        }
    }
}