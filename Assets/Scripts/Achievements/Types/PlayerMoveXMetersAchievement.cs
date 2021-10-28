using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "PlayerMoveXMeters", menuName = "Achievements/PlayerMoveXMeters")]
    public class PlayerMoveXMetersAchievement : Achievement
    {
        [SerializeField] private GameEvent onPlayerMove;
        [SerializeField] private FloatVariable playerMovedDistance;
        [SerializeField] private float targetDistance;

        private GameEventListener _eventListener;

        public override void Register()
        {
            _eventListener = GameEventListener.Create(onPlayerMove, Check);
        }

        protected override void Check()
        {
            if (playerMovedDistance.Value >= targetDistance)
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