using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private GameModel gameModel;
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerMoveController playerMoveController;
        
        private readonly Dictionary<GameObject, ChasingEnemy> _trackingEnemyList = new();
        private int _playerHealth;

        private void Start()
        {
            _playerHealth = gameModel.PlayerBaseHealth;
            // Tạo sẵn các object enemy để sẵn sàng sử dụng
            SimplePool.Preload(gameModel.EnemyAPrefab, 20);
            SimplePool.Preload(gameModel.EnemyBPrefab, 20);
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
            // Kiểm tra xem enemy này mưới hay cũ để giảm thiểu "GetComponent" tăng performance
            if (_trackingEnemyList.TryGetValue(enemy, out var result))
            {
                return result;
            }

            // nếu enemy mới thì add vạo dics
            var newChasingEnemy = enemy.GetComponent<ChasingEnemy>();
            _trackingEnemyList.Add(enemy, newChasingEnemy);
            return newChasingEnemy;
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var angle = Random.Range(0f, Mathf.PI * 2);
            var x = Mathf.Cos(angle) * gameModel.EnemySpawnRange;
            var z = Mathf.Sin(angle) * gameModel.EnemySpawnRange;
            return new Vector3(player.position.x + x, player.position.y, player.position.z + z);
        }

        #endregion

        /// <summary>
        /// Callback when enemy hit player
        /// </summary>
        /// <param name="damage">damage that enemy/object that deal to player</param>
        private void OnHitPlayer(int damage)
        {
            _playerHealth -= damage;
            gameView.DisplayPlayerHealth(gameModel.PlayerBaseHealth, _playerHealth);
        }
    }
}