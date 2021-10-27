using SpaceEscape.EventSystem;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "KillXEnemies", menuName = "Achievements/KillXEnemies")]
    public class KillEnemiesAchievement : Achievement
    {
        public IntVariable currentScore;
        public int targetValue;

        [SerializeField] private GameEvent onEnemyDie;

        private GameEventListener _eventListener;
        
        public override void Register()
        {
            _eventListener = GameEventListener.Create(onEnemyDie, Check);
        }

        protected override void Check()
        {
            if (currentScore.Value >= targetValue) Trigger();
        }

        protected override void Trigger()
        {
            OnTrigger?.Invoke(titleKey, descriptionKey);
            Destroy(_eventListener.gameObject);
        }
    }
}