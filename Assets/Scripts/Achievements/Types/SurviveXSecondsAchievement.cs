using System.Collections;
using SpaceEscape.EventSystem;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "SurviveXSeconds", menuName = "Achievements/SurviveXSeconds")]
    public class SurviveXSecondsAchievement : Achievement
    {
        [SerializeField] private GameEvent onPlayerDie;
        [SerializeField] private int secondsToWait;
        
        private GameEventListener _eventListener;
        private IEnumerator _coroutine;
        
        public override void Register()
        {
            _eventListener = GameEventListener.Create(onPlayerDie, StopCounter);
            _coroutine = StartCounter();
            _eventListener.StartCoroutine(_coroutine);
        }

        protected override void Check()
        {
            Trigger();
        }

        protected override void Trigger()
        {
            OnTrigger?.Invoke(titleKey, descriptionKey);
            Destroy(_eventListener.gameObject);
        }

        private IEnumerator StartCounter()
        {
            yield return new WaitForSeconds(secondsToWait);
            Check();
        }

        private void StopCounter()
        {
            _eventListener.StopCoroutine(_coroutine);
        }
    }
}