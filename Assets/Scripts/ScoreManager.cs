using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape
{
    public class ScoreManager : MonoBehaviour
    {
        public GameEvent scoreIncreased;

        [SerializeField] private IntVariable score;

        public void IncreaseScore()
        {
            score.Value += 1;
            scoreIncreased.Raise();
        }
    }
}