using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Achievements.Types
{
    [CreateAssetMenu(fileName = "KillXEnemies", menuName = "Achievements/KillXEnemies")]
    public class KillEnemiesAchievement : Achievement
    {
        public IntVariable currentScore;
        public int targetValue;
        
        public override void Register()
        {
            EnemyController.OnEnemyDieTrigger += Check;
        }

        protected override void Check()
        {
            if (currentScore.Value >= targetValue) Trigger();
        }

        protected override void Trigger()
        {
            OnTrigger?.Invoke(title, description);
            EnemyController.OnEnemyDieTrigger -= Check;
        }
    }
}