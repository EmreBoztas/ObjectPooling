using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] Text _label;

        float _totalTimeElapsed = 0f;
        float _timeSinceLastUpdate = 0f;

        void Update()
        {
            _totalTimeElapsed += Time.deltaTime;
            _timeSinceLastUpdate += Time.deltaTime;

            if (_timeSinceLastUpdate >= 1f)
            {
                UpdateTimerText();

                _timeSinceLastUpdate -= 1f;
            }
        }

        void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(_totalTimeElapsed / 60);
            int seconds = Mathf.FloorToInt(_totalTimeElapsed % 60);

            if (_label != null)
            {
                _label.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}