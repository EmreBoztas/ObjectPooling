using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BenchmarkUiManager : MonoBehaviour
    {
        [SerializeField] Text _entityCountLabel;
        [SerializeField] Text _infoLabel;
        [SerializeField] Text _fpsLabel;

        public void UpdateEntityCount(int count)
        {
            _entityCountLabel.text = $"Entity Count\n{count}";
        }

        public void UpdateInfoLabel(string text)
        {
            _infoLabel.text = text;
        }

        public void UpdateFpsLabel(string text)
        {
            _fpsLabel.text = text;
        }
    }
}