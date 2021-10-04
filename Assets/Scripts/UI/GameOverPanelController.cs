using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceEscape.UI
{
    public class GameOverPanelController : MonoBehaviour
    {
        public void OnRestartButtonPressed()
        {
            SceneManager.LoadScene("PlayScene");
        }

        public void OnExitButtonPressed()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}