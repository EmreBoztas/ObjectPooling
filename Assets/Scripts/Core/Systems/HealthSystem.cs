using UnityEngine;
using Core.Managers;
using UI;

namespace Core.Systems
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        int _maxHealth = 100;
        int _health;

        PoolManager _poolManager;


        void Awake()
        {
            _poolManager = GetComponentInParent<PoolManager>();
            Heal(_maxHealth);
        }

        public void HealMax()
        {
            Heal(_maxHealth);
        }

        public void Heal(int healthNumber)
        {
            if (healthNumber > _maxHealth)
                _health = _maxHealth;
            else
                _health = healthNumber;
        }

        public void UpdateMaxHealth(int  healthNumber)
        {
            if (healthNumber > _maxHealth &&  healthNumber >= 0 && healthNumber < 150)
                _maxHealth = healthNumber;
        }

        public void TakeDamage(int damageAmount)
        {
            _health -= damageAmount;

            if (_health <= 0)
            {
                _poolManager.ReturnToPool(gameObject);
                KillCounterUI.Instance.UpdateKillCount();

            }
        }
    }
}