using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
