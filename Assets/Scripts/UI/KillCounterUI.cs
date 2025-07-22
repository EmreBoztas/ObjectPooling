using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KillCounterUI : MonoBehaviour
    {
        public static KillCounterUI Instance;
        [SerializeField] Text _label;
        int _killCount = 0;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void UpdateKillCount()
        {
            _killCount++;
            _label.text = "Kills: " + _killCount;
        }
    }
}