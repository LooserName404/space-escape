using System.Collections.Generic;
using UnityEngine;

namespace SpaceEscape.Achievements
{
    public class AchievementController : MonoBehaviour
    {
        [SerializeField] private List<Achievement> achievements;
        private void Start()
        {
            var view = FindObjectOfType<AchievementView>();
            foreach (var achievement in achievements)
            {
                achievement.Register();
                achievement.OnTrigger += view.EnqueueAchievement;
            }
        }
    }
}