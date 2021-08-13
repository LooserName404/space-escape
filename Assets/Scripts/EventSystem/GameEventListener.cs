using UnityEngine;
using UnityEngine.Events;

namespace SpaceEscape.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Evento a ser registrado")] public GameEvent Event;

        [Tooltip("Resposta para invocar quando o Evento for disparado")]
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}