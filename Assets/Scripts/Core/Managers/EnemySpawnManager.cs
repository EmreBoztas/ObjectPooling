using System.Collections;
using UnityEngine;
using Core.Systems;

namespace Core.Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject _enemyPrefab;
        [SerializeField] Transform _player;
        [SerializeField] LayerMask _enemyLayerMask;
        [SerializeField] PoolManager _poolManager;
        [SerializeField] float _innerRadius = 6f;
        [SerializeField] float _outerRadius = 8f;
        [SerializeField] float _respawnRate = 0.6f;
        WaitForSeconds _respawnWait;
        float _enemyCollision;
        public int Difficulty = 0;

        private void Awake()
        {
            CircleCollider2D collider = _enemyPrefab.GetComponent<CircleCollider2D>();
            if (collider == null)
            {
                Debug.LogError("Enemy collider not found!", this.gameObject);
                return;
            }
            _enemyCollision = Mathf.Max(collider.bounds.extents.x, collider.bounds.extents.y);
        }

        void Start()
        {
            _poolManager.ChangePreFab(_enemyPrefab);
  
            ChangeSpawnRate(_respawnRate);
            StartCoroutine(RespawnEnemies());
        }

        IEnumerator RespawnEnemies()
        {
            while (true)
            {
                SpawnEnemies();
                yield return _respawnWait;
            }
        }

        void SpawnEnemies()
        {
            if (SpawnPositionCreator.TryGetSpawnPosition2D(_player, _innerRadius, _outerRadius, _enemyCollision, _enemyLayerMask, out Vector2 position))
            {
                Pose spawnPose = new Pose(position, Quaternion.Euler(0, 180, 0));
                _poolManager.Spawn(spawnPose, Difficulty);
            }
        }


        public void SetMaxEnemyCount(int maxEnemyCount)
        {
            _poolManager.SetMaxObjectCount(maxEnemyCount);
        }

        public void ChangeSpawnRate(float value)
        {
            _respawnWait = new WaitForSeconds(value);
        }
    }
}