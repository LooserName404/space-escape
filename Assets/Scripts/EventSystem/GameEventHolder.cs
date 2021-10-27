using UnityEngine;

namespace SpaceEscape.EventSystem
{
    public class GameEventHolder : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;

        public void Raise()
        {
            gameEvent.Raise();
        }
    }
}