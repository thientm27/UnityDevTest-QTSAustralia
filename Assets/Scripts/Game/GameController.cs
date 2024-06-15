using System.Collections;
using System.Collections.Generic;
using Game.Component;
using Game.Enemy;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Header("System")]
        [SerializeField] private GameModel gameModel;
        [SerializeField] private GameView gameView;
        [SerializeField] private PlayerMoveController playerMoveController;

        [Header("Game")]
        [SerializeField] private Transform player;
        [SerializeField] private Transform mapLimitA;
        [SerializeField] private Transform mapLimitB;

        private readonly Dictionary<GameObject, ChasingEnemy> _trackingEnemyList = new();
        private readonly HashSet<GameObject> _scoreItem = new();

        private int _playerHealth;
        private int _currentScore;
        private int _lastScore;

        private void Start()
        {
            _playerHealth = gameModel.PlayerBaseHealth;
            // Tạo sẵn các object enemy để sẵn sàng sử dụng
            SimplePool.Preload(gameModel.EnemyAPrefab, 20);
            SimplePool.Preload(gameModel.EnemyBPrefab, 20);
            SimplePool.Preload(gameModel.ScorePrefab, 20);
            StartCoroutine(SpawnEnemyA());
            StartCoroutine(SpawnEnemyB());
            StartCoroutine(SpawnScoreItems());
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
            // Kiểm tra xem enemy này mới hay cũ để giảm thiểu "GetComponent" -> tăng performance
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

        #region SCORE ITEM SPAWN

        private IEnumerator SpawnScoreItems()
        {
            while (true)
            {
                yield return new WaitForSeconds(gameModel.TimeSpawnScoreObj);

                Vector3 spawnPosition = GetRandomScoreItemSpawnPosition();
                GameObject scoreItem = Instantiate(gameModel.ScorePrefab, spawnPosition, Quaternion.identity);
                if (!_scoreItem.Contains(scoreItem))
                {
                    scoreItem.GetComponent<ScoreItem>().Initialize(AddScore);
                    _scoreItem.Add(scoreItem);
                }
            }
        }

        private Vector3 GetRandomScoreItemSpawnPosition()
        {
            var spawnPosition = Vector3.zero;
            var playerPosition = player.position;
            var mapLimitAPosition = mapLimitA.position;
            var mapLimitBPosition = mapLimitB.position;
            if (playerPosition.x <= (mapLimitAPosition.x + mapLimitBPosition.x) / 2f)
            {
                // Người chơi ở bên trái hoặc chính giữa theo trục X
                spawnPosition.x =
                    Random.Range(playerPosition.x + gameModel.ScoreItemMinRangeSpawn, mapLimitBPosition.x);
            }
            else
            {
                // Người chơi ở bên phải theo trục X
                spawnPosition.x =
                    Random.Range(mapLimitAPosition.x, playerPosition.x - gameModel.ScoreItemMinRangeSpawn);
            }

            if (playerPosition.z >= (mapLimitAPosition.z + mapLimitBPosition.z) / 2f)
            {
                // Người chơi ở phía trên theo trục Z
                spawnPosition.z =
                    Random.Range(playerPosition.z + gameModel.ScoreItemMinRangeSpawn, mapLimitBPosition.z);
            }
            else
            {
                // Người chơi ở phía dưới theo trục Z
                spawnPosition.z =
                    Random.Range(mapLimitAPosition.z, playerPosition.z - gameModel.ScoreItemMinRangeSpawn);
            }

            return spawnPosition;
        }

        #endregion

        #region CALL BACK

        /// <summary>
        /// Call back when player hit a score item
        /// </summary>
        /// <param name="scoreEarn"></param>
        private void AddScore(int scoreEarn)
        {
            _currentScore += scoreEarn;
            gameView.SetCurrentScore(_currentScore);
        }

        /// <summary>
        /// Callback when enemy hit player
        /// </summary>
        /// <param name="damage">damage that enemy/object that deal to player</param>
        private void OnHitPlayer(int damage)
        {
            _playerHealth -= damage;
            gameView.DisplayPlayerHealth(gameModel.PlayerBaseHealth, _playerHealth);
        }

        #endregion
    }
}