using UnityEngine;
using UnityEngine.Events;

namespace SpaceEscape.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Evento a ser registrado")] public GameEvent Event;

        [Tooltip("Resposta para invocar quando o Evento for disparado")]
        public UnityEvent Response;

        public static GameEventListener Create(GameEvent evt, UnityAction call)
        {
            var go = new GameObject();
            go.SetActive(false);
            var eventListener = go.AddComponent<GameEventListener>();
            eventListener.Event = evt;
            var unityEvent = new UnityEvent();
            unityEvent.AddListener(call);
            eventListener.Response = unityEvent;
            go.SetActive(true);
            return eventListener;
        }

        private void OnEnable()
        {
            if (Event != null)
            {
                Event.RegisterListener(this);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
            {
                Event.UnregisterListener(this);
            }
        }

        public void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}