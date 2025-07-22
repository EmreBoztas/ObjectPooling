using Core.Systems;
using UnityEngine;

namespace Core.Managers
{
    public class DifficultyManager : MonoBehaviour
    {
        EnemySpawnManager _enemySpawnManager;
        StatChanger _statChanger;
        [SerializeField] int _difficulty = 0;
        [SerializeField] int _maxDifficulty = 2;
        [SerializeField] float _difficultyIncreaseInterval = 5f;
        int _maxEnemyCount = 6;

        void Awake()
        {
            _enemySpawnManager = FindFirstObjectByType<EnemySpawnManager>();
            _statChanger = FindFirstObjectByType<StatChanger>();

            if (_enemySpawnManager == null || _statChanger == null)
            {
                Debug.LogError("DifficultyManager or StatChanger referance wasn't attended!", this);
            }
        }

        void Start()
        {
            SetMaxEnemyCount();
            Invoke(nameof(IncreaseDifficulty), _difficultyIncreaseInterval);
            ApplyCurrentDifficulty();
        }
        void IncreaseDifficulty()
        {
            if( _difficulty < _maxDifficulty)
            {
                _difficulty++;
                Invoke("IncreaseDifficulty", 5f);
                ApplyCurrentDifficulty();
                Invoke(nameof(IncreaseDifficulty), _difficultyIncreaseInterval);
            }
        }

        public void MaxEnemyCountCalculator()
        {
            SetMaxEnemyCount();
        }

        void SetMaxEnemyCount()
        {
            _enemySpawnManager.SetMaxEnemyCount(_maxEnemyCount);
        }

        void ApplyCurrentDifficulty()
        {
            _enemySpawnManager.Difficulty = _difficulty;
        }
    }
}