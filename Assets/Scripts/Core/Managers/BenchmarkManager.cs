using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using UI;

namespace Core.Managers
{
    public class BenchmarkManager : MonoBehaviour
    {
        [SerializeField] BenchmarkUiManager _benchmarkUiManager;
        [SerializeField] GameObject _entityPrefab;
        [SerializeField] GameObject _entityPool;

        List<GameObject> _entityList = new List<GameObject>();
        Stopwatch _stopwatch = new Stopwatch();

        Camera _cam;
        Vector3 _bottomLeft;
        Vector3 _topRight;

        void Start()
        {
            InvokeRepeating(nameof(UpdateFPS), 1f, 1f);

            _cam = Camera.main;
            if (_cam == null) return;

            _bottomLeft = _cam.ViewportToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
            _topRight = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.nearClipPlane));
        }

        public void SpawnEntities(int count)
        {
            _stopwatch.Restart();



            for (int i = 0; i < count; i++)
            {
                GameObject entity = Instantiate(_entityPrefab, GetSpawnPosition(), Quaternion.identity);
                entity.transform.parent = _entityPool.transform;
                _entityList.Add(entity);
            }

            _stopwatch.Stop();
            _benchmarkUiManager.UpdateEntityCount(_entityList.Count);
            _benchmarkUiManager.UpdateInfoLabel($"Spawned {count} entities in {ElapsedTime()} ms");
        }
        Vector3 GetSpawnPosition()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(_bottomLeft.x, _topRight.x),
                Random.Range(_bottomLeft.y, _topRight.y),
                0);
            return spawnPosition;
        }

        public void DestroyEntities()
        {
            _stopwatch.Restart();
            foreach (var entity in _entityList)
            {
                Destroy(entity);
            }
            _entityList.Clear();
            _stopwatch.Stop();

            _benchmarkUiManager.UpdateEntityCount(_entityList.Count);
            _benchmarkUiManager.UpdateInfoLabel($"Destroyed all entities in {ElapsedTime()} ms");
        }

        public void DisableEntities()
        {
            _stopwatch.Restart();
            foreach (var entity in _entityList)
            {
                entity.SetActive(false);
            }
            _stopwatch.Stop();

            _benchmarkUiManager.UpdateEntityCount(_entityList.Count);
            _benchmarkUiManager.UpdateInfoLabel($"Disabled {_entityList.Count} entities in {ElapsedTime()} ms");
        }

        public void EnableEntities()
        {
            _stopwatch.Restart();
            foreach (var entity in _entityList)
            {
                entity.SetActive(true);
            }
            _stopwatch.Stop();

            _benchmarkUiManager.UpdateEntityCount(_entityList.Count);
            _benchmarkUiManager.UpdateInfoLabel($"Enabled {_entityList.Count} entities in {ElapsedTime()} ms");
        }

        public void ClearEntities()
        {
            _stopwatch.Restart();
            foreach (var entity in _entityList)
            {
                if (entity != null)
                    Destroy(entity);
            }
            _entityList.Clear();
            _stopwatch.Stop();

            _benchmarkUiManager.UpdateEntityCount(_entityList.Count);
            _benchmarkUiManager.UpdateInfoLabel($"Cleared all entities in {ElapsedTime()} ms");
        }

        string ElapsedTime()
        {
            return _stopwatch.ElapsedMilliseconds.ToString();
        }

        void UpdateFPS()
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _benchmarkUiManager.UpdateFpsLabel(fps.ToString());
        }
    }
}