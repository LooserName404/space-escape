using System.Collections;
using System.Collections.Generic;
using SpaceEscape.Localizer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceEscape.Achievements
{
    public class AchievementView : MonoBehaviour
    {
        private class PopUp
        {
            public string TitleKey { get; set; }
            public string DescriptionKey { get; set; }
        }

        [SerializeField] private Image panel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;

        private List<PopUp> _queue = new List<PopUp>();

        private bool _isShowing = false;

        private void Start()
        {
            panel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_isShowing && _queue.Count > 0)
            {
                _isShowing = true;
                var popUp = _queue[0];
                _queue.Remove(popUp);
                StartCoroutine(ShowPanel(popUp));
            }
        }

        public void EnqueueAchievement(string titleKey, string descriptionKey)
        {
            _queue.Add(new PopUp {TitleKey = titleKey, DescriptionKey = descriptionKey});
        }

        private IEnumerator ShowPanel(PopUp popUp)
        {
            titleText.SetText(Localization.Localize(popUp.TitleKey));
            descriptionText.SetText(Localization.Localize(popUp.DescriptionKey));
            panel.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            panel.gameObject.SetActive(false);
            _isShowing = false;
        }
    }
}