using UnityEngine;

namespace SpaceEscape.UI
{
    public class OptionPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        
        public void OnBackButtonPressed()
        {
            mainPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}