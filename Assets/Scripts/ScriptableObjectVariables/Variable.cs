using System;
using UnityEngine;

namespace SpaceEscape.ScriptableObjectVariables
{
    public class Variable<T> : ScriptableObject
    {
        #if UNITY_EDITOR
            [Multiline]
            public string DeveloperDescription = "";
        #endif
        
        public T Value;

        public bool ResetOnRestart;
        
        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(Variable<T> value)
        {
            Value = value.Value;
        }

        private void OnDisable()
        {
            if (ResetOnRestart)
            {
                Value = default;
            }
        }
    }
}