using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceEscape.UI
{
    public class MainPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject optionPanel;
        
        public void OnPlayButtonPressed()
        {
            SceneManager.LoadScene("PlayScene");
        }

        public void OnExitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}