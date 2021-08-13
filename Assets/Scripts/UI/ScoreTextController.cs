using System;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceEscape.UI
{
    public class ScoreTextController : MonoBehaviour
    {
        [SerializeField] private IntVariable score;

        private Text _text;
        
        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void UpdateScoreText()
        {
            _text.text = score.Value.ToString("0000");
        }
    }
}