using System;
using UnityEngine;

namespace SpaceEscape.Achievements
{
    public abstract class Achievement : ScriptableObject
    {
        [SerializeField] protected string title;

        [TextArea] [SerializeField] protected string description;

        public Action<string, string> OnTrigger;

        public abstract void Register();

        protected abstract void Check();

        protected abstract void Trigger();
    }
}