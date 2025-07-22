using Core.Systems;
using UnityEngine;

namespace Entities.Enemy
{
    [RequireComponent(typeof(HealthSystem), typeof(SpriteRenderer))]
    public class Enemy : MonoBehaviour, IPoolableWithDifficulty
    {
        public int CurrentEnemyLevel { get; private set; }
        public float Speed { get; private set; }

        public HealthSystem HealthSystem { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        Transform _player;
        StatChanger _statChanger;

        void Awake()
        {
            HealthSystem = GetComponent<HealthSystem>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            _statChanger = FindFirstObjectByType<StatChanger>();

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _player = playerObject.transform;
            }
        }

        public void OnSpawn(int version)
        {
            if (_statChanger != null)
            {
                _statChanger.ApplyStatsToEnemy(version, this);
            }
            else
            {
                Debug.LogError("StatChanger service not found!", this);
            }

            HealthSystem.HealMax();
        }

        public void SetStats(int level, float speed)
        {
            this.CurrentEnemyLevel = level;
            this.Speed = speed;
        }

        void FixedUpdate()
        {
            if (_player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.position, Speed * Time.fixedDeltaTime);
            }
        }
    }
}