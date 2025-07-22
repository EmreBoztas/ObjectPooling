using UnityEngine;
using Core.Systems;

namespace Utilities
{
    public class TrackableObject : MonoBehaviour
    {
        ActiveObjectRegistry _registry;

        void Awake()
        {
            _registry = FindAnyObjectByType<ActiveObjectRegistry>();
        }

        void OnEnable()
        {
            _registry?.RegisterObject(this.gameObject);
        }

        void OnDisable()
        {
            _registry?.UnregisterObject(this.gameObject);
        }
    }
}