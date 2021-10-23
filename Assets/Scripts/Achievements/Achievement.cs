using System;
using UnityEngine;

namespace SpaceEscape.Achievements
{
    public abstract class Achievement : ScriptableObject
    {
        [SerializeField] protected string titleKey;

        [SerializeField] protected string descriptionKey;

        public Action<string, string> OnTrigger;

        public abstract void Register();

        protected abstract void Check();

        protected abstract void Trigger();
    }
}