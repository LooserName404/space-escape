using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape
{
    public class ScoreManager : MonoBehaviour
    {
        public GameEvent scoreIncreased;

        [SerializeField] private IntVariable score;

        private void IncreaseScore()
        {
            score.Value++;
            scoreIncreased.Raise();
        }
    }
}