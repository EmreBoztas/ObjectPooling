using UnityEngine;

namespace ScriptableObjects.EnemyStat
{

    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy Data/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public string EnemyName;
        public int Level = 1;
        public int Health = 100;
        public float Speed = 3f;
        public Color Color = Color.white;
    }
}