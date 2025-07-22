using System.Collections.Generic;
using UnityEngine;


namespace Core.Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] GameObject _objectContainer;
        GameObject _objectPrefab;
        int _spawnLimit; 
        IPoolable _poolable;
        IPoolableWithDifficulty _poolableWithDifficulty;

        List<GameObject> _allCreatedObjects = new List<GameObject>();
        Queue<GameObject> _objectPool = new Queue<GameObject>();

        [SerializeField] private int _initialPoolSize = 6;

        void Start()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                if (_allCreatedObjects.Count >= _spawnLimit) break;

                var obj = Instantiate(_objectPrefab, _objectContainer.transform);
                obj.SetActive(false);
                _allCreatedObjects.Add(obj);
                _objectPool.Enqueue(obj);
            }
        }

        public void ReturnToPool(GameObject pooledObject)
        {
            if (pooledObject == null) return;
            pooledObject.SetActive(false);
            _objectPool.Enqueue(pooledObject);
        }

        public void Spawn(Pose spawnPoint, int? version = null)
        {
            GameObject objToSpawn = null;

            if (_objectPool.Count > 0)
            {
                objToSpawn = _objectPool.Dequeue();
            }
            else if (_allCreatedObjects.Count < _spawnLimit)
            {
                objToSpawn = Instantiate(_objectPrefab, _objectContainer.transform);
                _allCreatedObjects.Add(objToSpawn);
            }
            else
            {
                return;
            }
            objToSpawn.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            if (version.HasValue && objToSpawn.TryGetComponent(out IPoolableWithDifficulty poolableWithDifficulty))
            {
                poolableWithDifficulty.OnSpawn(version.Value);
            }
            else if (objToSpawn.TryGetComponent(out IPoolable poolable))
            {
                poolable.Respawn();
            }
            objToSpawn.SetActive(true);
        }

        public void SetMaxObjectCount(int maxObjectCount)
        {
            _spawnLimit = maxObjectCount;
        }

        public void ChangePreFab(GameObject obj)
        {
            _objectPrefab = obj;
        }

        public void DeletePool()
        {
            foreach (GameObject obj in _allCreatedObjects)
            {

                if (obj != null)
                {
                    Destroy(obj);
                }
            }

            _allCreatedObjects.Clear();
            _objectPool.Clear();
        }
    }
}