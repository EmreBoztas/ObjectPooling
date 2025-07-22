using System;
using UnityEngine;
using Core.Managers;

namespace Entities.Projectile
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] float _speed = 15f;
        [SerializeField] float _lifetime = 2f;

        PoolManager poolManager;

        void Awake()
        {
            poolManager = GetComponentInParent<PoolManager>();
        }

        void FixedUpdate()
        {
            transform.position += transform.up * _speed * Time.fixedDeltaTime;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                ReturnBullet();
            }
        }

        void ReturnBullet()
        {
            if (gameObject.activeInHierarchy)
            {
                CancelInvoke(nameof(ReturnBullet));
                poolManager.ReturnToPool(gameObject);
            }
        }

        public void Respawn()
        {
            Invoke(nameof(ReturnBullet), _lifetime);
        }

    }
}