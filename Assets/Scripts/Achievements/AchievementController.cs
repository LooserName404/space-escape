using System.Collections.Generic;
using System.Linq;
using SpaceEscape.ScriptableObjectVariables;
using SpaceEscape.Utils;
using UnityEngine;

namespace SpaceEscape.Achievements
{
    public class AchievementController : MonoBehaviour
    {
        [SerializeField] private List<Achievement> achievements;
        [SerializeField] private AchievementsRuntimeSet completed;
        
        private void Start()
        {
            completed.Items = JsonUtility.FromJson<SerializableStringList>(PlayerPrefs.GetString("completed_achievements")).items ?? new List<string>();
            var view = FindObjectOfType<AchievementView>();
            foreach (var achievement in achievements.Where(a => !completed.Items.Contains(a.Title)))
            {
                achievement.Register();
                achievement.OnTrigger += view.EnqueueAchievement;
            }
        }
    }
}