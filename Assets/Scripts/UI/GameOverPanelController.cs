using SpaceEscape.Localizer;
using SpaceEscape.ScriptableObjectVariables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceEscape.UI
{
    public class GameOverPanelController : MonoBehaviour
    {
        [SerializeField] private IntVariable score;
        [SerializeField] private TMP_Text highscoreTitle;

        private void OnEnable()
        {
            UpdateHighscoreText();
        }

        public void OnRestartButtonPressed()
        {
            SceneManager.LoadScene("PlayScene");
        }

        public void OnExitButtonPressed()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void UpdateHighscoreText()
        {
            var text = Localization.Localize("new_highscore");
            highscoreTitle.text = $"{text}\nx{score.Value}";
        }
    }
}