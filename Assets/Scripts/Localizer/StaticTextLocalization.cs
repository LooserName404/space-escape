using TMPro;
using UnityEngine;
using SpaceEscape.Localizer;

namespace SpaceEscape.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class StaticTextLocalization : MonoBehaviour
    {
        [SerializeField] private string key;

        private TMP_Text _text;

        private void OnEnable()
        {
            Localization.OnLanguageChanged += SetText;
            SetText();
        }

        private void OnDisable()
        {
            Localization.OnLanguageChanged -= SetText;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            SetText();
        }

        private void SetText()
        {
            _text.SetText(Localization.Localize(key));
        }
    }
}