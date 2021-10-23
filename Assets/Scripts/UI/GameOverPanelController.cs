using System;
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
        [SerializeField] private TMP_Text highScoreTitle;
        
        private void OnEnable()
        {
            var high = PlayerPrefs.GetInt("highscore");
            if (score.Value > high)
            {
                highScoreTitle.gameObject.SetActive(true);
                UpdateHighScoreText();
            }
        }

        public void OnRestartButtonPressed()
        {
            SceneManager.LoadScene("PlayScene");
        }

        public void OnExitButtonPressed()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void UpdateHighScoreText()
        {
            var text = Localization.Localize("new_highscore");
            highScoreTitle.text = $"{text}\nx{score.Value}";
        }
    }
}