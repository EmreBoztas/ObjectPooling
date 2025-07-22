using UnityEngine;

namespace Core.Systems
{
    public class RotateSelectedPoint
    {
        Transform _transformToRotate;
        float _smooth;
        float _tiltAngle;
        float _lockAngleThreshold;

        public RotateSelectedPoint(Transform transformToRotate, float smooth, float tiltAngle, float lockAngleThreshold)
        {
            this._transformToRotate = transformToRotate;
            this._smooth = smooth;
            this._tiltAngle = tiltAngle;
            this._lockAngleThreshold = lockAngleThreshold;
        }

        public void RotateTowards(Transform target)
        {
            Vector3 direction = target.position - _transformToRotate.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle - _tiltAngle);
            float smoothFactor = 1 - Mathf.Exp(-_smooth * Time.deltaTime);
            _transformToRotate.rotation = Quaternion.Slerp(_transformToRotate.rotation, rotation, smoothFactor);
        }

        public bool IsTargetLocked(Transform target)
        {
            Vector3 directionToTarget = target.position - _transformToRotate.position;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(_transformToRotate.rotation.eulerAngles.z, angleToTarget));

            return angleDifference <= _lockAngleThreshold;
        }
    }
}