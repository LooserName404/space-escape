using SpaceEscape.EventSystem;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "Die", menuName = "Achievements/Die")]
    public class DieAchievement : Achievement
    {
        [SerializeField] private GameEvent onPlayerDie;

        private GameEventListener _eventListener;
        
        public override void Register()
        {
            _eventListener = GameEventListener.Create(onPlayerDie, Check);
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
    }
}