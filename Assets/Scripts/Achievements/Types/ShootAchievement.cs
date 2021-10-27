using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "ShootXTimes", menuName = "Achievements/ShootXTimes")]
    public class ShootAchievement : Achievement
    {
        [SerializeField] private int target;
        [SerializeField] private IntVariable currentShots;
        
        [SerializeField] private GameEvent onShoot;
        
        private GameEventListener _eventListener;
        
        public override void Register()
        {
            _eventListener = GameEventListener.Create(onShoot, Check);
        }

        protected override void Check()
        {
            currentShots.Value++;

            if (currentShots.Value >= target)
            {
                Trigger();
            }
        }

        protected override void Trigger()
        {
            OnTrigger?.Invoke(titleKey, descriptionKey);
            Destroy(_eventListener.gameObject);
        }
    }
}