using SpaceEscape.Localizer;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceEscape.UI
{
    public class ScoreTextController : MonoBehaviour
    {
        [SerializeField] private IntVariable score;

        private string _scoreLabel;
        private Text _text;
        
        public void UpdateScoreText()
        {
            _text.text = $"{_scoreLabel}: {score.Value.ToString("0000")}";
        }
        
        private void Awake()
        {
            _text = GetComponent<Text>();
            UpdateScoreText();
        }

        private void OnEnable()
        {
            Localization.OnLanguageChanged += UpdateScoreLabel;
            UpdateScoreLabel();
        }

        private void OnDisable()
        {
            Localization.OnLanguageChanged -= UpdateScoreLabel;
        }

        private void UpdateScoreLabel()
        {
            _scoreLabel = Localization.Localize("score_label");
        }
    }
}