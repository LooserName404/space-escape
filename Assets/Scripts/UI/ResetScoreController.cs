using UnityEngine;

namespace SpaceEscape.UI
{
    public class ResetScoreController : MonoBehaviour
    {
        public void ResetScore()
        {
            PlayerPrefs.DeleteKey("highscore");
        }
    }
}