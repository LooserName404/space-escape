using System;
using SpaceEscape.ScriptableObjectVariables;
using UnityEngine;

namespace SpaceEscape.Achievements
{
    [Serializable]
    public abstract class Achievement : ScriptableObject, IEquatable<Achievement>
    {
        [SerializeField] protected string titleKey;
        public string Title { get; }

        [SerializeField] protected string descriptionKey;

        [SerializeField] protected AchievementsRuntimeSet completed;

        public Action<string, string> OnTrigger;

        private Action<String, String> _completeAction;

        public abstract void Register();

        protected abstract void Check();

        protected abstract void Trigger();

        private void OnEnable()
        {
            _completeAction = delegate { Complete(); };
            OnTrigger += _completeAction;
        }

        private void OnDisable()
        {
            OnTrigger -= _completeAction;
        }

        private void Complete()
        {
            completed.Add(titleKey);
            completed.Save();
        }

        public bool Equals(Achievement other)
        {
            return base.Equals(other) && titleKey == other.titleKey && descriptionKey == other.descriptionKey;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Achievement) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (titleKey != null ? titleKey.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (descriptionKey != null ? descriptionKey.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}