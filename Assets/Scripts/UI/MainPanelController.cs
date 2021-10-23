using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceEscape.UI
{
    public class MainPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject loadingPanel;
        
        public void OnPlayButtonPressed()
        {
            StartCoroutine(LoadGame());
        }

        public void OnExitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private IEnumerator LoadGame()
        {
            loadingPanel.SetActive(true);
            gameObject.SetActive(false);
            var operation = SceneManager.LoadSceneAsync("PlayScene");

            while (!operation.isDone)
            {
                yield return null;
            }
            
            loadingPanel.SetActive(false);
        }
    }
}