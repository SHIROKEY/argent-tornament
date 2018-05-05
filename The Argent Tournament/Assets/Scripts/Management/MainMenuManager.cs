using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Management
{
    class MainMenuManager: MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void EndGame()
        {
            Application.Quit();
        }
    }
}
