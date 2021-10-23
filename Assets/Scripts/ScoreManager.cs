using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape
{
    public class ScoreManager : MonoBehaviour
    {
        public GameEvent scoreIncreased;
        public GameEvent highscoreIncreased;

        [SerializeField] private IntVariable score;
        [SerializeField] private IntVariable highscore;

        private void Awake()
        {
            score.SetValue(0);
            highscore.SetValue(PlayerPrefs.GetInt("highscore"));
        }

        public void IncreaseScore()
        {
            score.Value += 1;
            scoreIncreased.Raise();
        }

        public void HandleHighscore()
        {
            if (score.Value > highscore.Value)
            {
                highscore.SetValue(score);
                PlayerPrefs.SetInt("highscore", highscore.Value);
                highscoreIncreased.Raise();
            }
        }
    }
}