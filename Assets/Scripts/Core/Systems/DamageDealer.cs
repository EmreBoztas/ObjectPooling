using UnityEngine;

namespace Core.Systems
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] int _damageAmount = 50;
        [SerializeField] LayerMask _damageableLayers;

        bool _canDamage = true;

        void OnTriggerExit2D(Collider2D other)
        {
            if (_canDamage && IsInLayerMask(other.gameObject.layer, _damageableLayers) && other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damageAmount);
            }
        }

        bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}