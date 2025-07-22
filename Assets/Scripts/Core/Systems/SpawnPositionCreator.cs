using UnityEngine;

namespace Core.Systems
{
    public static class SpawnPositionCreator
    {
        const int MAX_ATTEMPTS = 10;

        public static bool TryGetSpawnPosition2D(Transform player, float innerRadius, float outerRadius, float enemyCollision, LayerMask enemyLayerMask, out Vector2 spawnPosition)
        {
            float spawnRadius = Random.Range(innerRadius, outerRadius);
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            for (int i = 0; i < MAX_ATTEMPTS; i++)
            {
                Vector2 potentialPosition = (Vector2)player.position + (randomDirection * spawnRadius);

                if (Physics2D.OverlapCircle(potentialPosition, enemyCollision, enemyLayerMask) == null)
                {
                    spawnPosition = potentialPosition;
                    return true; 
                }

                randomDirection = Quaternion.Euler(0, 0, 360f / MAX_ATTEMPTS) * randomDirection;
            }

            spawnPosition = Vector2.zero;
            return false;
        }
    }
}