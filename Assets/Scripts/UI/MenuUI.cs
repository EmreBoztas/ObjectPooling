using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.LogWarning("Invalid scene index: " + sceneIndex);
            }
        }
    }
}