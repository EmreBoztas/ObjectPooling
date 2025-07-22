using System.Collections.Generic;
using UnityEngine;

namespace Core.Systems
{
    public class TargetingSystem
    {
        Transform _originTransform;
        float _range;
        ActiveObjectRegistry _registry;

        public TargetingSystem(Transform originTransform, float range, ActiveObjectRegistry registry)
        {
            this._originTransform = originTransform;
            this._range = range;
            this._registry = registry;
        }

        public GameObject GetNearestEnemy()
        {
            if (_registry == null)
            {
                Debug.LogError("ActiveObjectRegistry referance wasn't attend to TargetingSystem!");
                return null;
            }

            List<GameObject> enemies = _registry.GetActiveObjectsByTag("Enemy");

            GameObject nearestEnemy = null;
            float minSqrDistance = _range * _range;

            foreach (GameObject enemy in enemies)
            {
                if (enemy == null) continue;

                float sqrDistance = (enemy.transform.position - _originTransform.position).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }

        public bool IsValidTarget(GameObject enemy)
        {
            return enemy != null && enemy.activeInHierarchy && Vector2.Distance(_originTransform.position, enemy.transform.position) <= _range;
        }
    }
}