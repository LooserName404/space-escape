using UnityEngine;

namespace SpaceEscape.UI
{
    public class NavigablePanelController : MonoBehaviour
    {
        [SerializeField] private GameObject targetPanel;
        
        public void OnNavigationPressed()
        {
            targetPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}