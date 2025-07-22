using UnityEngine;
using Core.Managers;
using Core.Systems;

namespace Entities.Turret
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] PoolManager _poolManager;
        [SerializeField] Transform _firePoint;
        [SerializeField] float _range = 5f;
        [SerializeField] float _smooth = 20f;
        [SerializeField] float _reloadTime = 0.6f;
        [SerializeField] float _lockAngleThreshold = 5f;
        [SerializeField] float _fireCountdown = 1f;
        [SerializeField] float _tiltAngle = 90.0f;
        [SerializeField] int _maxBulletCount = 12;

        GameObject _currentTarget;
        bool _isLocked = false;

        TargetingSystem _targetingSystem;
        RotateSelectedPoint _rotateSelectedPoint;
        ActiveObjectRegistry _registry;

        void Start()
        {
            _registry = FindAnyObjectByType<ActiveObjectRegistry>();

            if (_registry == null)
            {
                Debug.LogError("Can't find ActiveObjectRegistry in scene", this);
                this.enabled = false;
                return;
            }

            _targetingSystem = new TargetingSystem(transform, _range, _registry);
            _rotateSelectedPoint = new RotateSelectedPoint(transform, _smooth, _tiltAngle, _lockAngleThreshold);
            _poolManager.SetMaxObjectCount(_maxBulletCount);
            _poolManager.ChangePreFab(_bulletPrefab);
        }

        void Update()
        {
            if (_currentTarget == null || !_targetingSystem.IsValidTarget(_currentTarget))
            {
                _currentTarget = _targetingSystem.GetNearestEnemy();
                _isLocked = false;
            }

            if (_currentTarget != null)
            {
                _rotateSelectedPoint.RotateTowards(_currentTarget.transform);
                _isLocked = _rotateSelectedPoint.IsTargetLocked(_currentTarget.transform);

                _fireCountdown -= Time.deltaTime;

                if (_isLocked && _fireCountdown <= 0f)
                {
                    Pose spawnPose = new Pose(_firePoint.position, _firePoint.rotation);
                    _poolManager.Spawn(spawnPose);
                    _fireCountdown = _reloadTime;
                }
            }
        }
    }
}