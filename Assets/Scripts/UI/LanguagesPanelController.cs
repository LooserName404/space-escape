using SpaceEscape.Localizer;
using UnityEngine;

namespace SpaceEscape.UI
{
    public class LanguagesPanelController : MonoBehaviour
    {
        public void SetLanguage(string language)
        {
            var current = Localization.CurrentLanguage;
            if (current != language)
            {
                Localization.CurrentLanguage = language;
            }
        }
    }
}