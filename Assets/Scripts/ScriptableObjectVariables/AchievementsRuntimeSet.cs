using SpaceEscape.Utils;
using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    [CreateAssetMenu]
    public class AchievementsRuntimeSet : RuntimeSet<string>
    {
        public void Save()
        {
            var list = new SerializableStringList {items = Items};
            var json = JsonUtility.ToJson(list);
            PlayerPrefs.SetString("completed_achievements", json);
            PlayerPrefs.Save();
        }
    }
}