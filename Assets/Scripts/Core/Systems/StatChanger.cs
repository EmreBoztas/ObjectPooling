using Entities.Enemy;
using ScriptableObjects.EnemyStat;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Systems
{
    public class StatChanger : MonoBehaviour
    {
        [SerializeField] List<EnemyStats> _enemyVariations;

        public void ApplyStatsToEnemy(int level, Enemy enemyToModify)
        {
            if (enemyToModify == null || enemyToModify.HealthSystem == null || enemyToModify.SpriteRenderer == null)
            {
                Debug.LogError("Enemy or its essential components are null.", enemyToModify);
                return;
            }

            if (_enemyVariations == null || level < 0 || level >= _enemyVariations.Count)
            {
                Debug.LogError($"Invalid level index '{level}' or unconfigured Enemy Variations list.", this);
                return;
            }

            EnemyStats newStats = _enemyVariations[level];
            if (newStats == null)
            {
                Debug.LogWarning($"EnemyStats at index {level} is null.", this);
                return;
            }

            enemyToModify.HealthSystem.UpdateMaxHealth(newStats.Health);
            enemyToModify.SpriteRenderer.color = newStats.Color;
            enemyToModify.SetStats(newStats.Level, newStats.Speed);
        }
    }
}