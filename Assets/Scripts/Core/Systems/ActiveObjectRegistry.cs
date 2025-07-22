using System.Collections.Generic;
using UnityEngine;

namespace Core.Systems
{
    public class ActiveObjectRegistry : MonoBehaviour
    {
        Dictionary<string, List<GameObject>> _activeObjectsByTag = new Dictionary<string, List<GameObject>>();

        public void RegisterObject(GameObject obj)
        {
            string tag = obj.tag;
            if (!_activeObjectsByTag.ContainsKey(tag))
            {
                _activeObjectsByTag[tag] = new List<GameObject>();
            }
            if (!_activeObjectsByTag[tag].Contains(obj))
            {
                _activeObjectsByTag[tag].Add(obj);
            }
        }

        public void UnregisterObject(GameObject obj)
        {
            string tag = obj.tag;
            if (_activeObjectsByTag.ContainsKey(tag) && _activeObjectsByTag[tag].Contains(obj))
            {
                _activeObjectsByTag[tag].Remove(obj);
            }
        }

        public List<GameObject> GetActiveObjectsByTag(string tag)
        {
            if (_activeObjectsByTag.TryGetValue(tag, out List<GameObject> objectList))
            {
                return objectList;
            }
            return new List<GameObject>();
        }
    }
}